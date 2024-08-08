-- 1. Retrieve Products in a Specific Category
SELECT p.id, p.name, p.price, p.stock
FROM products p
JOIN categories c ON p.category_id = c.id
WHERE c.name = 'Books';


-- 2. Retrieve a User's Order History
SELECT o.id AS order_id, o.order_date, o.total_amount, oi.item_id, p.name AS product_name, oi.quantity
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE o.customer_id = 3;  -- Replace with the user ID you're interested in

-- 3. Find Products with Low Stock
SELECT id, name, stock
FROM products
WHERE stock > 10;  -- Replace with the desired stock threshold

-- 4. Retrieve Product Reviews
SELECT r.rating, r.review_text, u.username
FROM reviews r
JOIN users u ON r.user_id = u.id
WHERE r.product_id = 1;  -- Replace with the product ID you're interested in

--5. Get Product Reviews 
SELECT r.rating, r.review_text, u.username
FROM reviews r ,users u
WHERE r.product_id = 1;


-- 6. Add a new Product
INSERT INTO products (name, description, price, stock, category_id)
VALUES ('Wireless Headphones', 'Over-ear wireless headphones with noise cancellation.', 299.99, 20, 1);

-- 7 Update Stock for a Product
UPDATE products SET stock = 50 WHERE id = 1;  -- Replace with the product ID you want to update

-- 8. Calculate Total Sales for a Given Month

SELECT SUM(oi.quantity * p.price) AS total_sales
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE YEAR(o.order_date) = 2024 AND MONTH(o.order_date) = 8;  -- Replace with the desired year and month

-- 9. Apply a Discount Code

-- Declare and initialize variables
DECLARE @discount_percentage DECIMAL(5,2);
DECLARE @order_id INT = 2;  -- Replace with the actual order ID you want to apply the discount to

-- Check if the discount code is valid
SELECT @discount_percentage = discount_percentage
FROM discount_codes
WHERE code = 'DIWALI23' AND GETDATE() BETWEEN start_date AND end_date;

-- Apply the discount if it's valid
IF @discount_percentage IS NOT NULL
BEGIN
    UPDATE orders
    SET total_amount = total_amount - (total_amount * @discount_percentage / 100)
    WHERE id = @order_id;

    PRINT 'Discount applied successfully.';
END
ELSE
BEGIN
    PRINT 'Discount code is not valid or expired.';
END



-- 10. Retrieve Monthly Sales Report
SELECT p.name AS product_name, SUM(oi.quantity) AS total_quantity_sold, SUM(oi.quantity * p.price) AS total_sales
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE YEAR(o.order_date) = 2024 AND MONTH(o.order_date) = 7  -- Replace with desired year and month
GROUP BY p.id, p.name;
   

 -- 11. Retrieve Orders with Their Total Amounts

SELECT o.id AS order_id, o.order_date, o.total_amount, u.username
FROM orders o
JOIN users u ON o.customer_id = u.id;



-- 12. Retrieve Users with More Than One Order

SELECT u.id, u.username, COUNT(o.id) AS order_count
FROM users u
JOIN orders o ON u.id = o.customer_id
GROUP BY u.id, u.username
HAVING COUNT(o.id) > 0;

-- 14. Find Top Rated Products

SELECT p.id, p.name, AVG(r.rating) AS average_rating
FROM products p
JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name 
HAVING AVG(r.rating) >= 4;  -- Replace with the minimum average rating desired


