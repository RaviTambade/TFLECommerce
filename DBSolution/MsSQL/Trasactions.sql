-- 1. Inventory Reorder
-- Scenario: When inventory levels fall below a certain threshold, reorder items and update stock levels accordingly.

IF OBJECT_ID('reorder_inventory', 'P') IS NOT NULL
    DROP PROCEDURE reorder_inventory;
GO

CREATE PROCEDURE reorder_inventory
    @p_product_id INT
AS
BEGIN
    DECLARE @current_stock INT;

    -- Start a transaction
    BEGIN TRANSACTION;

    -- Check inventory levels
    SELECT @current_stock = stock_quantity FROM inventory WHERE product_id = @p_product_id;

    -- Reorder if stock is low
    IF @current_stock < 100
    BEGIN
        -- Place a reorder
        INSERT INTO purchase_orders (product_id, quantity, order_date)
        VALUES (@p_product_id, 100, GETDATE());

        -- Update inventory with the new stock
        UPDATE inventory
        SET stock_quantity = stock_quantity + 100
        WHERE product_id = @p_product_id;
    END;

    -- Commit the transaction
    COMMIT TRANSACTION;
END;


-- Execute the stored procedure
EXEC reorder_inventory @p_product_id = 7;

-- Check the purchase_orders table
SELECT * FROM purchase_orders;

-- Check the inventory table
SELECT * FROM inventory;


-- 2. E-commerce Order Fulfillment
-- Scenario: Process an order, update stock levels, and manage shipping details. Ensure all steps are successful before finalizing the order.

BEGIN TRANSACTION;

-- Update stock levels
UPDATE inventory
SET stock_quantity = stock_quantity - 1
WHERE product_id = 5;

-- Record order fulfillment
INSERT INTO order_fulfillment (order_id, fulfillment_date, status, tracking_number, carrier_name, fulfillment_notes)
VALUES (5, GETDATE(), 'Packed', 'TRK123', 'Carrier Shipment', 'Order packed');

-- Update shipment details
UPDATE shipments
SET status = 'Shipped', shipment_date = GETDATE(), tracking_number = 'TRK123456'
WHERE order_id = 5;

-- Commit the transaction
COMMIT TRANSACTION;

-- Check the stock level
SELECT product_id, stock_quantity
FROM inventory
WHERE product_id = 5;

-- Expected result: The stock level should be decreased by 1.

-- Check the order fulfillment record
SELECT fulfillment_id, order_id, fulfillment_date, status, tracking_number, carrier_name, fulfillment_notes
FROM order_fulfillment
WHERE order_id = 5;

-- Expected result: There should be a new record with the given order_id, product_id, quantity 5, and a recent fulfillment_date.

-- Check the shipping details
SELECT shipment_id, order_id, shipping_method_id, shipment_date, tracking_number, status
FROM shipments
WHERE order_id = 5;

-- Expected result: The status should be 'Shipped' and shipment_date should be recent.


-- 3. Product Returns and Refunds
-- Scenario: Process a product return and issue a refund, 
-- Start a transaction
BEGIN TRANSACTION;

-- Update inventory to add the returned product
UPDATE inventory 
SET stock_quantity = stock_quantity + 1 
WHERE product_id = '5';

-- Process refund
INSERT INTO refunds (order_id, product_id, refund_amount, refund_date) 
VALUES ('3', '5', 199.99, GETDATE());

-- Update return status
UPDATE returns 
SET status = 'Processed' 
WHERE return_id = '3';

-- Commit the transaction
COMMIT;

SELECT product_id, stock_quantity 
FROM inventory 
WHERE product_id = '5';

SELECT * FROM refunds 
WHERE order_id = '3' AND product_id = '5';

SELECT * FROM returns 
WHERE return_id = '3';

select * from refunds ;
select * from returns ;

-- 4 Transaction to Update Subscription
-- Start a transaction
BEGIN TRANSACTION;

-- Update subscription plan
UPDATE subscriptions
SET subscription_plan = 'Premium',
    start_date = GETDATE(),
    status = 'Active',
    end_date = DATEADD(DAY, 30, GETDATE())

WHERE user_id = 2;

-- Adjust billing
-- Assuming you have a table to record billing adjustments
INSERT INTO billing_adjustments (user_id, adjustment_amount, adjustment_date,reason)
VALUES (2, 20.00, GETDATE(),'Bought New Plan');

-- Commit the transaction
COMMIT;
select*from billing_adjustments;
select*from users;


