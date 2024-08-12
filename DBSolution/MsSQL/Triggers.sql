-- 1. Trigger to Update Stock After an Order is Placed

CREATE TRIGGER after_order_insert
ON orders
AFTER INSERT 
AS
BEGIN
    -- Update stock for each item in the order
    UPDATE p
    SET p.stock = p.stock - oi.quantity
    FROM products p
    INNER JOIN order_items oi ON p.id = oi.item_id
    INNER JOIN inserted i ON oi.order_id = i.id;
END;


CREATE TRIGGER before_product_delete
ON products
INSTEAD OF DELETE
AS
BEGIN
    -- Check if the product is present in any order
    IF EXISTS (
        SELECT 1
        FROM order_items oi
        WHERE oi.item_id IN (SELECT id FROM deleted)
    )
    BEGIN
        -- Raise an error if the product is in any order
        THROW 50000, 'Cannot delete product with existing orders.', 1;
    END
    ELSE
    BEGIN
        -- If no related order items, proceed with delete
        DELETE FROM products
        WHERE id IN (SELECT id FROM deleted);
    END;
END;


-- 3. Trigger to Automatically Set Order Status to 'Shipped' After Shipping Date is Updated

CREATE TRIGGER update_order_status_on_shipping_date
ON orders
AFTER UPDATE
AS
BEGIN
    -- Update order status if the shipping date is set and was previously null
    UPDATE orders
    SET status = 'Shipped'
    FROM inserted i
    JOIN deleted d ON i.id = d.id
    WHERE i.shipping_date IS NOT NULL
      AND d.shipping_date IS NULL;
END;

-- 4. Trigger to Log Changes to Product Prices

CREATE TRIGGER after_product_priceupdate
ON products
AFTER UPDATE 
AS
BEGIN
    -- Insert a record into the price_changes table
    INSERT INTO price_changes (product_id, old_price, new_price, change_date)
    VALUES (1, 1199.99, 1149.99, GETDATE());
END



--5 Trigger to Automatically Apply Discount to Orders Over a Certain Amount

CREATE TRIGGER trg_before_order_insert
ON orders
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @total_amount DECIMAL(10, 2);
    SELECT @total_amount = total_amount FROM inserted;

    IF @total_amount > 1000
    BEGIN
        INSERT INTO orders (order_date, customer_id, total_amount, status)
        SELECT order_date, customer_id, total_amount * 0.9, status
        FROM inserted;
    END
    ELSE
    BEGIN
        INSERT INTO orders (order_date, customer_id, total_amount, status)
        SELECT order_date, customer_id, total_amount, status
        FROM inserted;
    END
END


--6 Trigger for Insert on Products 

CREATE TRIGGER trg_after_product_insert
ON inventory
AFTER INSERT
AS
BEGIN
    INSERT INTO product_audit(Product_id, action_type, new_stock_quantity, action_timestamp)
    SELECT product_id, 'INSERT', stock_quantity, GETDATE()
    FROM inserted;
END


-- 7 Trigger for UPDATE on Products (Audit)
CREATE TRIGGER trg_before_product_update
ON inventory
FOR UPDATE
AS
BEGIN
    INSERT INTO product_audit(product_id, action_type, old_stock_quantity, new_stock_quantity)
    SELECT old.product_id, 'UPDATE', old.stock_quantity, new.stock_quantity
    FROM deleted old
    JOIN inserted new ON old.product_id = new.product_id;
END

--9  Trigger for DELETE on Products (Audit)

CREATE TRIGGER trg_after_product_delete
ON inventory
AFTER DELETE
AS
BEGIN
    INSERT INTO product_audit(product_id, action_type, old_stock_quantity)
    SELECT product_id, 'DELETE', stock_quantity
    FROM deleted;
END

-- 10. Trigger to Automatically Update User Points Based on Order Total

CREATE TRIGGER update_user_points
ON orders -- Replace 'orders' with your actual table name
AFTER INSERT 
AS
BEGIN
    -- Ensure the points column exists in the users table
    IF COL_LENGTH('users', 'points') IS NOT NULL
    BEGIN
        UPDATE users
        SET points = points + FLOOR(INSERTED.total_amount / 10)
        FROM INSERTED
        WHERE users.id = INSERTED.customer_id;
    END
    ELSE
    BEGIN
        PRINT 'Column "points" does not exist in the "users" table.';
    END
END;
