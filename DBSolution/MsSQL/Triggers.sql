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

CREATE TRIGGER after_product_price_update
ON products
AFTER UPDATE 
AS
BEGIN
    -- Insert a record into the price_changes table
    INSERT INTO price_changes (product_id, old_price, new_price, change_date)
    VALUES (1, 1199.99, 1149.99, GETDATE());
END


CREATE TRIGGER before_order_insert
ON orders
BEFORE INSERT 

BEGIN
    -- Apply discount if order total exceeds 1000
    IF NEW.total_amount > 1000 THEN
        SET NEW.total_amount = NEW.total_amount * 0.9; -- Apply 10% discount
    END IF;
END//


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


--9  Trigger for DELETE on order_items
CREATE TRIGGER trg_after_order_item_delete
ON order_items
AFTER DELETE
AS
BEGIN
    UPDATE inventory
    SET stock_quantity = stock_quantity + deleted.quantity
    FROM deleted
    WHERE inventory.product_id = deleted.item_id;
END


