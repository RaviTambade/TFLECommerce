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


