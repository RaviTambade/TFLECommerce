-- Stored_Procedures
-- 1. Creating a Stored Procedure for User Registration
-- 2. Creating a Stored Procedure for User Login
-- 3. Creating a Stored Procedure for Updating User Information
-- 4. Creating a Stored Procedure for Applying a Discount Code
-- 5. Creating a Stored Procedure for Retrieving Order Details
-- 6. Creating a Stored Procedure for Low Stock Alerts
-- 7. Creating a Stored Procedure for Product Reviews
-- 8. Creating a Stored Procedure for Monthly Sales Aggregation

-- Triggers
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



-- 1. Creating a Stored Procedure for User Registration
DELIMITER //

CREATE PROCEDURE RegisterUser(
    IN p_username VARCHAR(50),
    IN p_password VARCHAR(255),
    IN p_email VARCHAR(100),
    IN p_address VARCHAR(255)
)
BEGIN
    INSERT INTO users (username, password, email, address)
    VALUES (p_username, p_password, p_email, p_address);
END //

DELIMITER ;



-- 2. Creating a Stored Procedure for User Login
DELIMITER //

CREATE PROCEDURE LoginUser(
    IN p_username VARCHAR(50),
    IN p_password VARCHAR(255)
)
BEGIN
    SELECT id, username, email
    FROM users
    WHERE username = p_username AND password = p_password;
END //

DELIMITER ;


-- 3. Creating a Stored Procedure for Updating User Information
DELIMITER //

CREATE PROCEDURE UpdateUserInfo(
    IN p_user_id INT,
    IN p_email VARCHAR(100),
    IN p_address VARCHAR(255)
)
BEGIN
    UPDATE users
    SET email = p_email, address = p_address
    WHERE id = p_user_id;
END //

DELIMITER ;


-- 4. Creating a Stored Procedure for Applying a Discount Code
DELIMITER //

CREATE PROCEDURE ApplyDiscount(
    IN p_order_id INT,
    IN p_discount_code VARCHAR(50)
)
BEGIN
    DECLARE v_discount DECIMAL(5, 2);
    DECLARE v_total DECIMAL(10, 2);
    
    -- Retrieve discount percentage from the discount_codes table
    SELECT discount_percentage INTO v_discount
    FROM discount_codes
    WHERE code = p_discount_code AND NOW() BETWEEN start_date AND end_date;
    
    IF v_discount IS NOT NULL THEN
        -- Get the current total amount of the order
        SELECT total_amount INTO v_total
        FROM orders
        WHERE id = p_order_id;
        
        -- Apply the discount
        SET v_total = v_total - (v_total * (v_discount / 100));
        
        -- Update the order with the discounted amount
        UPDATE orders
        SET total_amount = v_total
        WHERE id = p_order_id;
    ELSE
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Invalid or expired discount code.';
    END IF;
END //

DELIMITER ;

-- Call the stored procedure with test parameters
CALL ApplyDiscount(1, 'SUMMER21');




-- 5. Creating a Stored Procedure for Retrieving Order Details

DELIMITER //

CREATE PROCEDURE GetOrderDetails(
    IN p_order_id INT
)
BEGIN
    SELECT o.id AS order_id, o.order_date, o.shipping_address, o.total_amount,
           oi.item_id, p.name AS product_name, oi.quantity, p.price
    FROM orders o
    JOIN order_items oi ON o.id = oi.order_id
    JOIN products p ON oi.item_id = p.id
    WHERE o.id = p_order_id;
END //

DELIMITER ;

CALL GetOrderDetails(1);



-- 6. Creating a Stored Procedure for Low Stock Alerts
DELIMITER //

CREATE PROCEDURE LowStockAlert(
    IN p_threshold INT
)
BEGIN
    SELECT id, name, stock
    FROM products
    WHERE stock < p_threshold;
END //

DELIMITER ;

CALL LowStockAlert(10);


-- 7. Creating a Stored Procedure for Product Reviews
DELIMITER //

CREATE PROCEDURE AddProductReview(
    IN p_product_id INT,
    IN p_user_id INT,
    IN p_rating INT,
    IN p_review TEXT
)
BEGIN
    INSERT INTO reviews (product_id, user_id, rating, review_text)
    VALUES (p_product_id, p_user_id, p_rating, p_review);
END //

DELIMITER ;

CALL AddProductReview(1, 2, 5, 'Great product!');

-- 8. Creating a Stored Procedure for Monthly Sales Aggregation

DELIMITER //

CREATE PROCEDURE MonthlySalesReport(
    IN p_year INT,
    IN p_month INT
)
BEGIN
    SELECT p.id AS product_id, p.name AS product_name,
           SUM(oi.quantity) AS total_quantity_sold,
           SUM(oi.quantity * p.price) AS total_sales
    FROM orders o
    JOIN order_items oi ON o.id = oi.order_id
    JOIN products p ON oi.item_id = p.id
    WHERE YEAR(o.order_date) = p_year AND MONTH(o.order_date) = p_month
    GROUP BY p.id, p.name;
END //

DELIMITER ;

CALL MonthlySalesReport(2024, 8);


-- triggers

-- 1. Trigger to Update Stock After an Order is Placed
DELIMITER //

CREATE TRIGGER after_order_insert
AFTER INSERT ON orders
FOR EACH ROW
BEGIN
    -- Update stock for each item in the order
    UPDATE products p
    JOIN order_items oi ON p.id = oi.item_id
    SET p.stock = p.stock - oi.quantity
    WHERE oi.order_id = NEW.id;
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
    -- Insert the order into the archived_orders table
    INSERT INTO archived_orders (id, order_date, customer_id, total_amount, status)
    VALUES (OLD.id, OLD.order_date, OLD.customer_id, OLD.total_amount, OLD.status);
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

CREATE TRIGGER before_order_insert
BEFORE INSERT ON orders
FOR EACH ROW
BEGIN
    -- Assuming store closed dates are stored in a `closed_dates` table
    IF EXISTS (
        SELECT 1
        FROM closed_dates
        WHERE date = NEW.order_date
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
    -- Assuming 1 point per 10 units of order total
    UPDATE users
    SET points = points + FLOOR(NEW.total_amount / 10)
    WHERE id = NEW.customer_id;
END//

DELIMITER ;








