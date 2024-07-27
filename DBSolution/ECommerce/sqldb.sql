-- 1. Retrieve All Products

SELECT * FROM products;

-- 2. Retrieve Products in a Specific Category
SELECT p.id, p.name, p.price, p.stock
FROM products p
JOIN categories c ON p.category_id = c.id
WHERE c.name = 'Electronics';

-- 3. Retrieve a User's Order History
SELECT o.id AS order_id, o.order_date, o.total_amount, oi.item_id, p.name AS product_name, oi.quantity
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE o.customer_id = 1;  -- Replace with the user ID you're interested in

-- 4. Find Products with Low Stock
SELECT id, name, stock
FROM products
WHERE stock < 10;  -- Replace with the desired stock threshold

-- 5. Retrieve Product Reviews
SELECT r.rating, r.review_text, u.username
FROM reviews r
JOIN users u ON r.user_id = u.id
WHERE r.product_id = 1;  -- Replace with the product ID you're interested in



-- 6. Add a new Product
INSERT INTO products (name, description, price, stock, category_id)
VALUES ('Wireless Headphones', 'Over-ear wireless headphones with noise cancellation.', 299.99, 20, 1);

--7 Update Stock for a Product
UPDATE products
SET stock = 50
WHERE id = 1;  -- Replace with the product ID you want to update


-- 8. Delete an Order

DELETE FROM orders
WHERE id = 1;  -- Replace with the order ID you want to delete



-- 9. Calculate Total Sales for a Given Month

SELECT SUM(oi.quantity * p.price) AS total_sales
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE YEAR(o.order_date) = 2024 AND MONTH(o.order_date) = 7;  -- Replace with the desired year and month


--10. Apply a Discount Code

-- First, check if the discount code is valid
SELECT discount_percentage
FROM discount_codes
WHERE code = 'SUMMER21' AND NOW() BETWEEN start_date AND end_date;

-- Then apply the discount to an order
-- Assuming the discount percentage was retrieved and is valid
SET @discount_percentage = 10.00;  -- Replace with the discount percentage retrieved
UPDATE orders
SET total_amount = total_amount - (total_amount * @discount_percentage / 100)
WHERE id = 1;  -- Replace with the order ID you want to apply the discount to

-- 11. Retrieve Monthly Sales Report
SELECT p.name AS product_name, SUM(oi.quantity) AS total_quantity_sold, SUM(oi.quantity * p.price) AS total_sales
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE YEAR(o.order_date) = 2024 AND MONTH(o.order_date) = 7  -- Replace with desired year and month
GROUP BY p.id, p.name;


-- 12. Retrieve Orders with Their Total Amounts

SELECT o.id AS order_id, o.order_date, o.total_amount, u.username
FROM orders o
JOIN users u ON o.customer_id = u.id;

--  13. Retrieve Users with More Than One Order

SELECT u.id, u.username, COUNT(o.id) AS order_count
FROM users u
JOIN orders o ON u.id = o.customer_id
GROUP BY u.id, u.username
HAVING COUNT(o.id) > 1;


-- 14. Find Top Rated Products

SELECT p.id, p.name, AVG(r.rating) AS average_rating
FROM products p
JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name
HAVING AVG(r.rating) >= 4;  -- Replace with the minimum average rating desired

-- 15. Retrieve Latest Orders
SELECT id, order_date, total_amount
FROM orders
ORDER BY order_date DESC
LIMIT 10;  -- Retrieves the 10 most recent orders

-- 16. Count Products in Each Category

SELECT c.name AS category_name, COUNT(p.id) AS product_count
FROM products p
JOIN categories c ON p.category_id = c.id
GROUP BY c.id, c.name;

-- 17. Find Orders with Products Above a Certain Price

SELECT o.id AS order_id, p.name AS product_name, oi.quantity, p.price
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE p.price > 100;  -- Replace with the desired price threshold


-- 17. Retrieve Products with More than a Specified Stock
SELECT id, name, stock
FROM products
WHERE stock > 50;  -- Replace with the desired stock threshold


-- 2. Get All Orders with Their Items and Prices
SELECT o.id AS order_id, o.order_date, p.name AS product_name, oi.quantity, p.price, (oi.quantity * p.price) AS total_price
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
ORDER BY o.order_date DESC;

-- 3. Find Users with the Most Orders

SELECT u.id, u.username, COUNT(o.id) AS order_count
FROM users u
JOIN orders o ON u.id = o.customer_id
GROUP BY u.id, u.username
ORDER BY order_count DESC
LIMIT 5;  -- Top 5 users with the most orders

-- 4. Retrieve Products and Their Categories

SELECT p.id AS product_id, p.name AS product_name, c.name AS category_name
FROM products p
JOIN categories c ON p.category_id = c.id;


-- 5. Get Total Revenue Per Product
SELECT p.id AS product_id, p.name AS product_name, SUM(oi.quantity * p.price) AS total_revenue
FROM order_items oi
JOIN products p ON oi.item_id = p.id
GROUP BY p.id, p.name;

-- 6. Find Most Expensive Products in Each Category
SELECT c.name AS category_name, p.name AS product_name, MAX(p.price) AS highest_price
FROM products p
JOIN categories c ON p.category_id = c.id
GROUP BY c.name;

-- 7. Retrieve Orders Placed Within a Specific Date Range

SELECT id, order_date, total_amount
FROM orders
WHERE order_date BETWEEN '2024-01-01' AND '2024-12-31';  -- Replace with desired date range


-- 8. List Top 3 Products by Total Sales

SELECT p.id AS product_id, p.name AS product_name, SUM(oi.quantity * p.price) AS total_sales
FROM order_items oi
JOIN products p ON oi.item_id = p.id
GROUP BY p.id, p.name
ORDER BY total_sales DESC
LIMIT 3;

-- 9. Retrieve Orders with Discounts Applied
SELECT o.id AS order_id, o.order_date, o.total_amount, d.code AS discount_code
FROM orders o
JOIN discount_codes d ON o.id = d.code
WHERE NOW() BETWEEN d.start_date AND d.end_date;


-- 10. Get Average Rating for Each Product

SELECT p.id AS product_id, p.name AS product_name, AVG(r.rating) AS average_rating
FROM products p
LEFT JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name;

-- 11. Find Customers Who Have Never Made a Purchase

SELECT u.id, u.username, u.email
FROM users u
LEFT JOIN orders o ON u.id = o.customer_id
WHERE o.id IS NULL;


--12. Retrieve Top 5 Most Reviewed Products

SELECT p.id AS product_id, p.name AS product_name, COUNT(r.id) AS review_count
FROM products p
LEFT JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name
ORDER BY review_count DESC
LIMIT 5;

-- 13. Retrieve Order Details Including User Information

SELECT o.id AS order_id, o.order_date, u.username, u.email, o.shipping_address, o.total_amount
FROM orders o
JOIN users u ON o.customer_id = u.id;


-- 14. Find Average Order Amount Per User

SELECT u.id AS user_id, u.username, AVG(o.total_amount) AS average_order_amount
FROM users u
JOIN orders o ON u.id = o.customer_id
GROUP BY u.id, u.username;

-- 15. Retrieve All Products with Reviews and Their Average Rating
SELECT p.id AS product_id, p.name AS product_name, AVG(r.rating) AS average_rating
FROM products p
LEFT JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name;

-- 16. Find the Most Recent Review for Each Product

SELECT p.id AS product_id, p.name AS product_name, r.review_text, r.created_at
FROM products p
JOIN reviews r ON p.id = r.product_id
WHERE r.created_at = (
    SELECT MAX(created_at)
    FROM reviews
    WHERE product_id = p.id
);


-- 17. Retrieve Orders with Items and Their Prices Above a Certain Amount

SELECT o.id AS order_id, p.name AS product_name, oi.quantity, p.price, (oi.quantity * p.price) AS total_price
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE (oi.quantity * p.price) > 100;  -- Replace with the desired price threshold

-- 18. Find Products That Have Never Been Ordered
SELECT p.id AS product_id, p.name AS product_name
FROM products p
LEFT JOIN order_items oi ON p.id = oi.item_id
WHERE oi.item_id IS NULL;

-- 19. Get Total Orders and Total Amount Spent by Each User

SELECT u.id AS user_id, u.username, COUNT(o.id) AS total_orders, SUM(o.total_amount) AS total_spent
FROM users u
LEFT JOIN orders o ON u.id = o.customer_id
GROUP BY u.id, u.username;

-- 20. Retrieve Products with the Most Positive Reviews

SELECT p.id AS product_id, p.name AS product_name, AVG(r.rating) AS average_rating
FROM products p
JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name
HAVING AVG(r.rating) >= 4;  -- Replace with the minimum average rating desired



-- JOIN Queries

-- 1. Inner Join: Retrieve Orders with Their Items and Product Details
SELECT o.id AS order_id, o.order_date, p.name AS product_name, oi.quantity, p.price, (oi.quantity * p.price) AS total_price
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id;

-- 2. Left Join: Retrieve All Products and Their Categories

SELECT p.id AS product_id, p.name AS product_name, c.name AS category_name
FROM products p
LEFT JOIN categories c ON p.category_id = c.id;


--3. Right Join: Retrieve All Categories and Products in Each Category
SELECT c.id AS category_id, c.name AS category_name, p.name AS product_name
FROM categories c
RIGHT JOIN products p ON c.id = p.category_id;

-- 4. Full Outer Join: Retrieve All Products and Reviews, Even If Some Products Have No Reviews
SELECT p.id AS product_id, p.name AS product_name, r.rating, r.review_text
FROM products p
LEFT JOIN reviews r ON p.id = r.product_id
UNION
SELECT p.id AS product_id, p.name AS product_name, r.rating, r.review_text
FROM reviews r
RIGHT JOIN products p ON r.product_id = p.id;


-- 5. Self Join: Retrieve Products and Their Similar Products Based on Category

SELECT p1.id AS product_id, p1.name AS product_name, p2.name AS similar_product_name
FROM products p1
JOIN products p2 ON p1.category_id = p2.category_id AND p1.id <> p2.id;

--6. Join with Aggregation: Retrieve Total Sales Per Product
SELECT p.id AS product_id, p.name AS product_name, SUM(oi.quantity * p.price) AS total_sales
FROM products p
JOIN order_items oi ON p.id = oi.item_id
GROUP BY p.id, p.name;

-- 7. Join with Filtering: Retrieve Orders for a Specific User with Item Details
SELECT o.id AS order_id, o.order_date, oi.item_id, p.name AS product_name, oi.quantity, p.price
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE o.customer_id = 1;  -- Replace with the user ID


-- 8. Join with Subquery: Retrieve Users Who Have Purchased Products in a Specific Category
SELECT DISTINCT u.id, u.username
FROM users u
JOIN orders o ON u.id = o.customer_id
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE p.category_id = (SELECT id FROM categories WHERE name = 'Electronics');

-- 9. Complex Join: Retrieve Orders with Product Details and Discount Information

SELECT o.id AS order_id, o.order_date, p.name AS product_name, oi.quantity, p.price, d.code AS discount_code, (oi.quantity * p.price) AS total_price
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
LEFT JOIN discount_codes d ON d.code = o.id  -- Assuming discount code is related to order ID for this example
WHERE o.order_date BETWEEN '2024-01-01' AND '2024-12-31';

-- 10. Join for Data Consistency: Retrieve Orders and Verify Product Availability

SELECT o.id AS order_id, o.order_date, p.name AS product_name, oi.quantity, p.stock
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE oi.quantity <= p.stock;


-- 11. Join to Retrieve High-Rated Products with Their Categories

SELECT p.id AS product_id, p.name AS product_name, c.name AS category_name, AVG(r.rating) AS average_rating
FROM products p
JOIN categories c ON p.category_id = c.id
LEFT JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name, c.name
HAVING AVG(r.rating) >= 4;  -- Replace with the desired minimum average rating

-- 12. Join to Get Users and Their Recent Orders

SELECT u.id AS user_id, u.username, o.id AS order_id, o.order_date
FROM users u
JOIN orders o ON u.id = o.customer_id
WHERE o.order_date > NOW() - INTERVAL 30 DAY;  -- Orders placed in the last 30 days



-- 1. Retrieve Orders with Shipping Address and User Information
SELECT o.id AS order_id, o.order_date, o.shipping_address, u.username, u.email
FROM orders o
JOIN users u ON o.customer_id = u.id;


-- 2. List Products with Their Latest Review

SELECT p.id AS product_id, p.name AS product_name, r.rating, r.review_text, r.created_at
FROM products p
LEFT JOIN reviews r ON p.id = r.product_id
WHERE r.created_at = (
    SELECT MAX(created_at)
    FROM reviews
    WHERE product_id = p.id
);

-- 3. Retrieve Orders with Items and Calculate Total Price for Each Order

SELECT o.id AS order_id, o.order_date, o.shipping_address, SUM(oi.quantity * p.price) AS total_price
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
GROUP BY o.id, o.order_date, o.shipping_address;

-- 4. Find Products That Are Out of Stock and Their Categories

SELECT p.id AS product_id, p.name AS product_name, c.name AS category_name
FROM products p
JOIN categories c ON p.category_id = c.id
WHERE p.stock = 0;


-- 5. Retrieve Users Who Have Left Reviews for Products

SELECT DISTINCT u.id AS user_id, u.username, u.email
FROM users u
JOIN reviews r ON u.id = r.user_id;

-- 6. List Categories and the Number of Products in Each

SELECT c.id AS category_id, c.name AS category_name, COUNT(p.id) AS product_count
FROM categories c
LEFT JOIN products p ON c.id = p.category_id
GROUP BY c.id, c.name;

-- 7. Retrieve Orders with Products and Check for Discounts Applied

SELECT o.id AS order_id, o.order_date, p.name AS product_name, oi.quantity, p.price, d.code AS discount_code
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
LEFT JOIN discount_codes d ON o.id = d.code  -- Assuming discount codes are associated with orders
WHERE o.total_amount > 100;  -- Example condition to filter orders with significant total amount

-- 8. Get Users and Their Total Spending

SELECT u.id AS user_id, u.username, SUM(o.total_amount) AS total_spent
FROM users u
JOIN orders o ON u.id = o.customer_id
GROUP BY u.id, u.username;

--9. Retrieve Products with Reviews and Average Rating

SELECT p.id AS product_id, p.name AS product_name, AVG(r.rating) AS average_rating
FROM products p
LEFT JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name;

-- 10. Find Orders Placed by Users with a Specific Email Domain

SELECT o.id AS order_id, o.order_date, u.username, u.email
FROM orders o
JOIN users u ON o.customer_id = u.id
WHERE u.email LIKE '%@example.com';  -- Replace with the desired email domain


-- 11. List Products and the Number of Times They Have Been Ordered
SELECT p.id AS product_id, p.name AS product_name, COUNT(oi.item_id) AS times_ordered
FROM products p
LEFT JOIN order_items oi ON p.id = oi.item_id
GROUP BY p.id, p.name;


-- 12. Get Orders and Their Shipping Status Based on Product Availability

SELECT o.id AS order_id, o.order_date, p.name AS product_name, oi.quantity, CASE 
    WHEN p.stock >= oi.quantity THEN 'Available'
    ELSE 'Out of Stock'
END AS stock_status
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id;

-- 13. Find Users Who Have Not Purchased Any Products
SELECT u.id, u.username, u.email
FROM users u
LEFT JOIN orders o ON u.id = o.customer_id
WHERE o.id IS NULL;

-- 14. Retrieve All Orders and Check If Any Discount Code Was Used

SELECT o.id AS order_id, o.order_date, o.total_amount, d.code AS discount_code
FROM orders o
LEFT JOIN discount_codes d ON o.id = d.code  -- Assuming discount codes are linked to orders
WHERE d.code IS NOT NULL;

-- 15. List Products with Their Average Review Rating and Number of Review

SELECT p.id AS product_id, p.name AS product_name, AVG(r.rating) AS average_rating, COUNT(r.id) AS review_count
FROM products p
LEFT JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name;

-- 16. Retrieve Orders and Their Shipping Address for Users in a Specific City
SELECT o.id AS order_id, o.order_date, o.shipping_address, u.username
FROM orders o
JOIN users u ON o.customer_id = u.id
WHERE u.address LIKE '%New York%';  -- Replace with the desired city or condition


-- 17. Find Products Purchased by Users with Specific Membership Status
SELECT p.id AS product_id, p.name AS product_name, u.username, u.membership_status
FROM products p
JOIN order_items oi ON p.id = oi.item_id
JOIN orders o ON oi.order_id = o.id
JOIN users u ON o.customer_id = u.id
WHERE u.membership_status = 'Gold';  -- Replace with the desired membership status

-- 18. Get Total Revenue from Each Category

SELECT c.id AS category_id, c.name AS category_name, SUM(oi.quantity * p.price) AS total_revenue
FROM categories c
LEFT JOIN products p ON c.id = p.category_id
LEFT JOIN order_items oi ON p.id = oi.item_id
GROUP BY c.id, c.name;

-- 19. List Orders and Their Associated Review Ratings
SELECT o.id AS order_id, o.order_date, p.name AS product_name, AVG(r.rating) AS average_review_rating
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
LEFT JOIN reviews r ON p.id = r.product_id
GROUP BY o.id, o.order_date, p.name;

-- 20. Find Most Frequently Purchased Products
SELECT p.id AS product_id, p.name AS product_name, COUNT(oi.item_id) AS purchase_count
FROM products p
JOIN order_items oi ON p.id = oi.item_id
GROUP BY p.id, p.name
ORDER BY purchase_count DESC;




































