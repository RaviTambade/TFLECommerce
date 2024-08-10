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