select * from product_audit;

truncate table product_audit;

drop table product_audit;

drop table inventory;

drop table products;

drop table order_items;

SELECT * FROM products;

SELECT * FROM categories;

SELECT * FROM discount_codes;

SELECT * FROM order_items;

SELECT * FROM reviews;

SELECT * FROM users;

SELECT * FROM orders;

SELECT * FROM inventory;


DELETE FROM order_items WHERE order_id = 1 AND item_id = 1;

SELECT * FROM inventory WHERE product_id = 1;

SELECT * FROM products WHERE id IN (7, 8);




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


