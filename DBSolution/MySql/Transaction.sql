-- Certainly! Here are more business scenarios where transactions in MySQL are crucial for maintaining data integrity and consistency:

-- 1. Inventory Reorder
-- Scenario: When inventory levels fall below a certain threshold, reorder items and update stock levels accordingly.

DROP PROCEDURE IF EXISTS reorder_inventory;

DELIMITER //

CREATE PROCEDURE reorder_inventory(IN p_product_id INT)
BEGIN
    DECLARE current_stock INT;

    -- Start a transaction
    START TRANSACTION;

    -- Check inventory levels
    SELECT stock_quantity INTO current_stock FROM inventory WHERE product_id = p_product_id;

    -- Reorder if stock is low
    IF current_stock < 100 THEN
        -- Place a reorder
        INSERT INTO purchase_orders (product_id, quantity, order_date)
        VALUES (p_product_id, 100, NOW());

        -- Update inventory with the new stock
        UPDATE inventory
        SET stock_quantity = stock_quantity + 100
        WHERE product_id = p_product_id;
    END IF;

    -- Commit the transaction
    COMMIT;
END //

DELIMITER ;


CALL reorder_inventory(1);

SELECT * FROM purchase_orders;

SELECT * FROM inventory ;


-- 2. E-commerce Order Fulfillment
-- Scenario: Process an order, update stock levels, and manage shipping details. Ensure all steps are successful before finalizing the order.

-- Wrapping Transaction in the Procedure
DELIMITER //

CREATE PROCEDURE update_order_fulfillment(IN p_product_id INT,IN orderId INT,IN o_f_tracking_number varchar(20))
BEGIN
START TRANSACTION;

-- Update stock levels
UPDATE inventory SET stock_quantity = stock_quantity - 1 WHERE product_id = p_product_id;

-- Record order fulfillment
INSERT INTO order_fulfillment (order_id, fulfillment_date, status, tracking_number, carrier_name, fulfillment_notes)
VALUES (orderId, NOW(), 'Packed', o_f_tracking_number, 'Carrier Shipment', 'Order packed ');

-- Update shipment details
UPDATE shipments
SET status = 'Shipped', shipment_date = NOW(), tracking_number = o_f_tracking_number
WHERE order_id = orderId;

-- Commit the transaction
COMMIT;
END //

DELIMITER ;


-- CALL update_order_fulfillment
Call update_order_fulfillment(1,5,'TRACK011');
-- Check the stock level
SELECT product_id, stock_quantity
FROM inventory
WHERE product_id = '5';

-- Expected result: The stock level should be decreased by 1.

-- Check the order fulfillment record
SELECT fulfillment_id, order_id, fulfillment_date, status, tracking_number, carrier_name, fulfillment_notes
FROM order_fulfillment
WHERE order_id = '5';

-- Expected result: There should be a new record with the given order_id, product_id, quantity 5, and a recent fulfillment_date.

-- Check the shipping details
SELECT shipment_id, order_id, shipping_method_id, shipment_date, tracking_number, status
FROM shipments
WHERE order_id = '5';

-- Expected result: The status should be 'Shipped' and shipped_date should be recent.


-- 3. Product Returns and Refunds
-- Scenario: Process a product return and issue a refund, 
-- Start a transaction
START TRANSACTION;

-- Update inventory to add the returned product
UPDATE inventory 
SET stock_quantity = stock_quantity + 1 
WHERE product_id = '5';

-- Process refund
INSERT INTO refunds (order_id, product_id, refund_amount, refund_date) 
VALUES ('3', '5', 199.99, NOW());

-- Update return status
UPDATE returns 
SET status = 'Processed' 
WHERE return_id = '3';

-- Commit the transaction
COMMIT;


SELECT product_id, stock_quantity 
FROM inventory 
WHERE product_id = '5';

SELECT * 
FROM refunds 
WHERE order_id = '3' AND product_id = '5';

SELECT * 
FROM returns 
WHERE return_id = '3';

select * from refunds ;
select * from returns ;

-- 4 Transaction to Update Subscription
-- Start a transaction
START TRANSACTION;

-- Update subscription plan
UPDATE subscriptions
SET plan = 'Premium', start_date = NOW(),status = 'Active',end_date= DATE_ADD(Now(), INTERVAL 30 DAY)
WHERE user_id = 1;

-- Adjust billing
-- Assuming you have a table to record billing adjustments
INSERT INTO billing_adjustments (user_id, adjustment_amount, adjustment_date,reason)
VALUES (1, 20.00, NOW(),'Bought New Plan');

-- Commit the transaction
COMMIT;
-- Rollback if either update fails
-- Proper error handling should be implemented here



