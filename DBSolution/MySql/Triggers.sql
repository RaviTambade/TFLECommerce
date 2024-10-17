
-- triggers
-- 1. Trigger to Update Stock After an Order is Placed
-- 2. Trigger to Prevent Deletion of a Product with Existing Orders
-- 3. Trigger to Automatically Set Order Status to 'Shipped' After Shipping Date is Updated
-- 4. Trigger to Log Changes to Product Prices
-- 5. Trigger to Automatically Apply Discount to Orders Over a Certain Amount
-- 6. Trigger to Update the Last Modified Date on Product Changes
-- 7. Trigger to Archive Orders Before Deletion
-- 8. Trigger to Validate User Email Format
-- 9. Trigger to Prevent Orders from Being Placed on Closed Dates
-- 10. Trigger to Automatically Update User Points Based on Order Total


-- 1. Trigger to Update Stock After an Order is Placed
DELIMITER //

CREATE TRIGGER after_order_items_insert
AFTER INSERT ON order_items
FOR EACH ROW
BEGIN
    -- Update stock for each item in the order
    UPDATE products p
    JOIN order_items oi ON p.id = oi.item_id
    SET p.stock = p.stock - NEW.quantity
    WHERE p.id= NEW.item_id;
END//

DELIMITER ;


-- 2. Trigger to Prevent Deletion of a Product with Existing Orders

DELIMITER //

CREATE TRIGGER before_product_delete
BEFORE DELETE ON products
FOR EACH ROW
BEGIN
    -- Check if the product is present in any order
    IF EXISTS (
        SELECT 1
        FROM order_items oi
        WHERE oi.item_id = OLD.id
    ) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Cannot delete product with existing orders.';
    END IF;
END//

DELIMITER ;


-- 3. Trigger to Automatically Set Order Status to 'Shipped' After Shipping Date is Updated

DELIMITER //

CREATE TRIGGER after_shipping_date_update
AFTER UPDATE ON orders
FOR EACH ROW
BEGIN
    -- Update order status if the shipping date is set
    IF NEW.shipping_date IS NOT NULL AND OLD.shipping_date IS NULL THEN
        UPDATE orders
        SET status = 'Shipped'
        WHERE id = NEW.id;
    END IF;
END//

DELIMITER ;


-- 4. Trigger to Log Changes to Product Prices

DELIMITER //

CREATE TRIGGER after_product_price_update
AFTER UPDATE ON products
FOR EACH ROW
BEGIN
    -- Insert a record into the price_changes table
    INSERT INTO price_changes (product_id, old_price, new_price, change_date)
    VALUES (OLD.id, OLD.price, NEW.price, NOW());
END//

DELIMITER ;


UPDATE products
SET price = 733.99
WHERE id = 1;

SELECT * FROM price_changes
WHERE product_id = 1
ORDER BY change_date DESC;


-- 5. Trigger to Automatically Apply Discount to Orders Over a Certain Amount

DELIMITER //

CREATE TRIGGER before_order_insert
BEFORE INSERT ON orders
FOR EACH ROW
BEGIN
    -- Apply discount if order total exceeds 1000
    IF NEW.total_amount > 1000 THEN
        SET NEW.total_amount = NEW.total_amount * 0.9; -- Apply 10% discount
    END IF;
END//

DELIMITER ;


-- 6. Trigger to Update the Last Modified Date on Product Changes

DELIMITER //

CREATE TRIGGER before_product_update
BEFORE UPDATE ON products
FOR EACH ROW
BEGIN
    -- Set the last_modified field to the current timestamp
    SET NEW.last_modified = NOW();
END//

DELIMITER ;


-- 7. Trigger to Archive Orders Before Deletion

DELIMITER //

CREATE TRIGGER before_order_delete
BEFORE DELETE ON orders
FOR EACH ROW
BEGIN
    -- Insert the order into the archieved_orders table
    INSERT INTO archieved_orders (order_date, customer_id, total_amount, status)
    VALUES (OLD.order_date, OLD.customer_id, OLD.total_amount, OLD.status);
END//

DELIMITER ;

-- 8. Trigger to Validate User Email Format

DELIMITER //

CREATE TRIGGER before_user_insert
BEFORE INSERT ON users
FOR EACH ROW
BEGIN
    -- Check if email contains '@' and '.'
    IF NEW.email NOT REGEXP '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid email format.';
    END IF;
END//

DELIMITER ;

DELIMITER //

CREATE TRIGGER before_user_update
BEFORE UPDATE ON users
FOR EACH ROW
BEGIN
    -- Check if email contains '@' and '.'
    IF NEW.email NOT REGEXP '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$' THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Invalid email format.';
    END IF;
END//

DELIMITER ;


-- DELIMITER //

-- CREATE TRIGGER before_user_insert_or_update
-- BEFORE INSERT OR UPDATE ON users
-- FOR EACH ROW
-- BEGIN
--     -- Check if email contains '@' and '.'
--     IF NEW.email NOT REGEXP '^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$' THEN
--         SIGNAL SQLSTATE '45000'
--         SET MESSAGE_TEXT = 'Invalid email format.';
--     END IF;
-- END//

-- DELIMITER ;


-- 9. Trigger to Prevent Orders from Being Placed on Closed Dates

DELIMITER //

CREATE TRIGGER prevent_order_insert
BEFORE INSERT ON orders
FOR EACH ROW
BEGIN
    -- Assuming store closed dates are stored in a `closed_dates` table
    IF EXISTS (
        SELECT 1
        FROM closed_dates
        WHERE close_date = NEW.order_date
    ) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Cannot place orders on closed dates.';
    END IF;
END//

DELIMITER ;

-- 10. Trigger to Automatically Update User Points Based on Order Total
DELIMITER //
CREATE TRIGGER after_order_insert
AFTER INSERT ON orders
FOR EACH ROW
BEGIN
    DECLARE calculate_points int default 0;

    SET calculate_points = FLOOR(NEW.total_amount / 10);
    -- Assuming 1 point per 10 units of order total
    IF EXISTS(
        Select 1 from loyalty_redemptions 
        where user_id= NEW.customer_id
        )
        THEN
            UPDATE loyalty_redemptions
            SET points_redeemed = points_redeemed + calculate_points
            WHERE user_id = NEW.customer_id;
    ELSE
        INSERT INTO loyalty_redemptions (user_id, points_redeemed, redemption_date, status) VALUES
         (NEW.customer_id, calculate_points, NOW(), 'Pending');
    END IF ;
END//
DELIMITER ;

INSERT INTO orders(customer_id, order_date, shipping_address, total_amount, shipping_date, status) VALUES
(2, '2024-07-23', 'Manchar, pune', 1019.98, '2024-07-30', 'Shipped');

DROP  TRIGGER after_order_insert;

-- 11. Trigger for Insert on Products 
-- DROP TRIGGER after_product_insert;

	DELIMITER //

	CREATE TRIGGER after_product_insert
	AFTER INSERT ON inventory
	FOR EACH ROW 
	BEGIN
	INSERT INTO product_audit(inventory_id, action_type, new_stock_quantity, action_timestamp )
	VALUES(NEW.inventory_id, 'INSERT', NEW.stock_quantity , NOW());
	END//

	DELIMITER ; 

INSERT INTO inventory(product_id, stock_quantity) VALUES
(17, 25);

-- 12 Trigger for UPDATE on Products


-- DROP TRIGGER before_product_update;

DELIMITER //

CREATE TRIGGER product_update
BEFORE UPDATE ON inventory
FOR EACH ROW
BEGIN
    INSERT INTO product_audit (inventory_id, action_type, old_stock_quantity, new_stock_quantity)
    VALUES (OLD.inventory_id, 'UPDATE', OLD.stock_quantity, NEW.stock_quantity);
END//

DELIMITER ;


update inventory
SET stock_quantity = 700
WHERE product_id=2;



-- 13 Trigger for DELETE on Products
DROP TRIGGER after_product_delete;

DELIMITER //

CREATE TRIGGER after_product_delete
AFTER DELETE ON inventory
FOR EACH ROW
BEGIN
    INSERT INTO product_audit(inventory_id, action_type, old_stock_quantity,new_stock_quantity)
    VALUES(OLD.inventory_id, 'DELETE', OLD.stock_quantity, 0);
END //

DELIMITER ;

-- drop trigger after_product_delete;
select * from product_audit;
DELETE FROM inventory WHERE id=3;


-- 14 Trigger for DELETE on `order_items
-- DROP TRIGGER after_order_item_delete;

DELIMITER //

CREATE TRIGGER after_order_item_delete
AFTER DELETE ON order_items
FOR EACH ROW
BEGIN
    UPDATE inventory
    SET stock_quantity = stock_quantity + OLD.quantity
    WHERE product_id = OLD.item_id;
END//

DELIMITER ;

DELETE FROM order_items WHERE order_id = 10 AND item_id = 8;
SELECT * FROM inventory WHERE product_id = 2;

--  15 Automatically Set Order Status Based on Inventory Levels
-- Trigger for AFTER INSERT on order_items

DROP TRIGGER IF EXISTS after_order_item_insert;

DELIMITER //

CREATE TRIGGER after_order_item_insert
AFTER INSERT ON order_items
FOR EACH ROW
BEGIN
    DECLARE available_stock INT;

    -- Check the stock for the newly inserted item
    SELECT stock INTO available_stock
    FROM products
    WHERE id = NEW.item_id;

    -- If stock is insufficient, set the order status to "Cancelled"
    IF available_stock < NEW.quantity THEN
        UPDATE orders
        SET status = 'Cancelled'
        WHERE id = NEW.order_id;
    END IF;
END//

DELIMITER ;


SELECT * FROM orders WHERE id = 2;
SELECT stock FROM products WHERE id = 2;

INSERT INTO order_items (order_id, item_id, quantity) VALUES (2, 2, 50);


-- 16 Update Total Order Price
-- Trigger for AFTER INSERT on order_items

DROP TRIGGER IF EXISTS after_order_item_insert;

DELIMITER //

CREATE TRIGGER after_order_item_insert
AFTER INSERT ON order_items
FOR EACH ROW
BEGIN
    DECLARE total_amount DECIMAL(10, 2);

    -- Calculate the new total price for the order using the price from products table
    SELECT SUM(p.price * oi.quantity) INTO total_amount
    FROM order_items oi
    JOIN products p ON oi.item_id = p.id
    WHERE oi.order_id = NEW.order_id;

    -- Update the order's total price
    UPDATE orders
    SET total_amount = total_amount
    WHERE id = NEW.order_id;
END//

DELIMITER ;


INSERT INTO order_items (order_id, item_id, quantity) VALUES
(5, 5, 5); -- 2 Smartphones

SELECT * FROM orders WHERE id = 5;



-- 17 Automatically Update total_amount in orders Table After Deleting an Item from order_items.

DROP TRIGGER IF EXISTS after_order_item_deleted;

DELIMITER //

CREATE TRIGGER after_order_item_deleted
AFTER DELETE ON order_items
FOR EACH ROW
BEGIN
    DECLARE total_amount DECIMAL(10, 2);

    -- Calculate the new total amount for the order
    SELECT SUM(p.price * oi.quantity) INTO total_amount
    FROM order_items oi
    JOIN products p ON oi.item_id = p.id
    WHERE oi.order_id = OLD.order_id;

    -- Update the order's total amount
    UPDATE orders
    SET total_amount = total_amount
    WHERE id = OLD.order_id;
END//

DELIMITER ;


-- Delete a row from the order_items table
DELETE FROM order_items WHERE order_id = 1 AND item_id = 4;

-- Check the updated state of the orders table
SELECT * FROM orders;

-- Check the updated state of the order_items table
SELECT * FROM order_items;

truncate table order_items;


