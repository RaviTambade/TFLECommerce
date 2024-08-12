select * from product_audit;

truncate table product_audit;

drop table product_audit;

drop table inventory;

drop table products;

drop table order_items;

SELECT * FROM products;

SELECT * FROM categories;

SELECT * FROM discount_codes;

SELECT * FROM order_items where item_id=2;

SELECT * FROM reviews;

SELECT * FROM users;

SELECT * FROM orders;

SELECT * FROM inventory;

SELECT * FROM shipment_items;

DELETE FROM users WHERE id = 1;

SELECT * FROM inventory WHERE product_id = 1;

SELECT * FROM products WHERE id IN (7, 8);

SELECT * FROM subscriptions;
SELECT * FROM discount_codes;

SELECT * FROM loyalty_redemptions;

-- Drop tables if they exist (optional)
DROP TABLE IF EXISTS shipment_items;

DROP TABLE IF EXISTS shipments;

SELECT oi.order_id, SUM(p.price * oi.quantity) AS total_value
FROM order_items oi
JOIN products p ON oi.item_id = p.id
WHERE oi.order_id = 1
GROUP BY oi.order_id;


SELECT oi.order_id, SUM(p.price * oi.quantity) AS total_value
FROM order_items oi
JOIN products p ON oi.item_id = p.id
WHERE oi.order_id = 2
GROUP BY oi.order_id;

SELECT oi.order_id, p.name, p.price, oi.quantity
FROM order_items oi
JOIN products p ON oi.item_id = p.id
WHERE oi.order_id = 1;

SELECT oi.order_id, p.name, p.price, oi.quantity,
       (p.price * oi.quantity) AS item_total,
       SUM(p.price * oi.quantity) AS total_order_value
FROM order_items oi
JOIN products p ON oi.item_id = p.id
WHERE oi.order_id = 13
GROUP BY oi.order_id, p.name, p.price, oi.quantity;


-- truncate table purchase_orders;

-- INSERT INTO purchase_orders (product_id, quantity, order_date)
-- VALUES
--     (1, 10, '2024-08-06'),
--     (2, 150, '2024-08-06'),
--     (3, 200, '2024-08-06'),
--     (4, 250, '2024-08-06'),
--     (5, 300, '2024-08-06'),
--     (6, 30, '2024-08-06'),
--     (7, 400, '2024-08-06'),
--     (8, 450, '2024-08-06'),
--     (9, 500, '2024-08-06'),
--     (10, 550, '2024-08-06');


