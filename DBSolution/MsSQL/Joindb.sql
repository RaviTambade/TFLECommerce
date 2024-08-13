

-- JOIN Queries
-- 1. Inner Join: Retrieve Orders with Their Items and Product Details
-- Endpoint: /api/orders/items/products
-- Method: GET


-- 2. Left Join: Retrieve All Products and Their Categories
-- Endpoint: /api/products/categories
-- Method: GET


-- 3.Right Join: Retrieve All Categories and Products in Each Category
-- Endpoint: /api/categories/products
-- Method: GET

-- 4. Full Outer Join: Retrieve All Products and Reviews, Even If Some Products Have No Reviews
-- Endpoint: /api/products/reviews
-- Method: GET

-- 5. Self Join: Retrieve Products and Their Similar Products Based on Category
-- Endpoint: /api/products/similar-products
-- Method: GET

-- 6. Join with Aggregation: Retrieve Total Sales Per Product
-- Endpoint: /api/products{productID}/total-sales
-- Method: GET


-- 7. Join with Filtering: Retrieve Orders for a Specific User with Item Details
-- 8. Join with Subquery: Retrieve Users Who Have Purchased Products in a Specific Category
-- 9. Complex Join: Retrieve Orders with Product Details and Discount Information
-- 10. Join for Data Consistency: Retrieve Orders and Verify Product Availability
-- 11. Join to Retrieve High-Rated Products with Their Categories
-- 12. Join to Get Users and Their Recent Orders





-- 1. Retrieve Orders with Shipping Address and User Information
-- 2. List Products with Their Latest Review
-- 3. Retrieve Orders with Items and Calculate Total Price for Each Order
-- 4. Find Products That Are Out of Stock and Their Categories
-- 5. Retrieve Users Who Have Left Reviews for Products
-- 6. List Categories and the Number of Products in Each
-- 7. Retrieve Orders with Products and Check for Discounts Applied
-- 8. Get Users and Their Total Spending
-- 9. Retrieve Products with Reviews and Average Rating
-- 10. Find Orders Placed by Users with a Specific Email Domain
-- 11. List Products and the Number of Times They Have Been Ordered
-- 12. Get Orders and Their Shipping Status Based on Product Availability
-- 13. Find Users Who Have Not Purchased Any Products
-- 14. Retrieve All Orders and Check If Any Discount Code Was Used
-- 15. List Products with Their Average Review Rating and Number of Review
-- 16. Retrieve Orders and Their Shipping Address for Users in a Specific City
-- 17. Get Total Revenue from Each Category
-- 18. List Orders and Their Associated Review Ratings
-- 19. Find Most Frequently Purchased Products


------ JOIN QUERIES ----------

-- 1. Inner Join: Retrieve Orders with Their Items and Product Details
SELECT o.id AS order_id, o.order_date, p.name AS product_name, oi.quantity, p.price, (oi.quantity * p.price) AS total_price
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id;

-- 2. Left Join: Retrieve All Products and Their Categories
SELECT p.id AS product_id, p.name AS product_name, c.name AS category_name
FROM products p
LEFT JOIN categories c ON p.category_id = c.id;

 -- 3.Right Join: Retrieve All Categories and Products in Each Category
SELECT c.id AS category_id, c.name AS category_name, p.name AS product_name
FROM categories c
RIGHT JOIN products p ON c.id = p.category_id;

-- 5. Self Join: Retrieve Products and Their Similar Products Based on Category
SELECT p1.id AS product_id, p1.name AS product_name, p2.name AS similar_product_name
FROM products p1
JOIN products p2 ON p1.category_id = p2.category_id AND p1.id <> p2.id;

-- 6. Join with Aggregation: Retrieve Total Sales Per Product
SELECT p.id AS product_id, p.name AS product_name, SUM(oi.quantity * p.price) AS total_sales
FROM products p
JOIN order_items oi ON p.id = oi.item_id
GROUP BY p.id, p.name;

-- 7. Join with Filtering: Retrieve Orders for a Specific User with Item Details
SELECT o.id AS order_id, o.order_date, oi.item_id, p.name AS product_name, oi.quantity, p.price
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE o.customer_id = 2;  -- Replace with the user ID

-- 8. Join with Subquery: Retrieve Users Who Have Purchased Products in a Specific Category
SELECT DISTINCT u.id, u.username
FROM users u
JOIN orders o ON u.id = o.customer_id
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE p.category_id = (SELECT id FROM categories WHERE name = 'Clothing');

-- 9. Complex Join: Retrieve Orders with Product Details and Discount Information
SELECT  o.id AS order_id, o.order_date, p.name AS product_name,  oi.quantity, p.price, d.code AS discount_code, (oi.quantity * p.price) AS total_price
FROM orders o
INNER JOIN order_items oi ON o.id = oi.order_id
INNER JOIN products p ON oi.item_id = p.id
LEFT JOIN order_discounts od ON o.id = od.order_id  -- Join orders with order_discounts
LEFT JOIN  discount_codes d ON od.discount_code = d.code  -- Join order_discounts with discount_codes
WHERE  o.order_date BETWEEN '2024-01-01' AND '2024-07-31';

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
HAVING AVG(r.rating) >= 4; 

-- 12. Join to Get Users and Their Recent Orders
SELECT u.id AS user_id, u.username, o.id AS order_id, o.order_date
FROM users u
JOIN orders o ON u.id = o.customer_id
WHERE o.order_date > DATEADD(DAY, -1, GETDATE());
 


 
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
SELECT o.id AS order_id, o.order_date, o.shipping_address, p.name AS product_name, oi.quantity, (oi.quantity * p.price) AS total_price_per_item
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
ORDER BY o.id, p.name;

-- 4. Find Products That Are Out of Stock and Their Categories
SELECT p.id AS product_id, p.name AS product_name, c.name AS category_name
FROM products p
JOIN categories c ON p.category_id = c.id
WHERE p.stock = 0;

-- 5. Retrieve Users Who Have given Reviews for Products
SELECT DISTINCT u.id AS user_id, u.username, u.email
FROM users u
JOIN reviews r ON u.id = r.user_id;

-- 6. List Categories and the Number of Products
SELECT c.id AS category_id, c.name AS category_name, COUNT(p.id) AS product_count
FROM categories c
LEFT JOIN products p ON c.id = p.category_id
GROUP BY c.id, c.name;

-- 7. Retrieve Orders with Products and Check for Discounts Applied
SELECT o.id AS order_id, o.order_date, o.total_amount, p.name AS product_name, oi.quantity, p.price, od.discount_code
FROM orders AS o
JOIN order_items AS oi ON o.id = oi.order_id
JOIN products AS p ON oi.item_id = p.id
LEFT JOIN order_discounts AS od ON o.id = od.order_id
WHERE o.total_amount >= 500;

-- 8. Get Users and Their Total Expenses
SELECT u.id AS user_id, u.username, SUM(o.total_amount) AS total_spent
FROM users u
JOIN orders o ON u.id = o.customer_id
GROUP BY u.id, u.username;

-- 9. Retrieve Products with Reviews and Average Rating
SELECT p.id AS product_id, p.name AS product_name, AVG(r.rating) AS average_rating
FROM products p
LEFT JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name;

-- 10. Find Orders Placed by Users with a Specific Email Domain
SELECT o.id AS order_id, o.order_date, u.username, u.email
FROM orders o
JOIN users u ON o.customer_id = u.id
WHERE u.email LIKE 'priya@example.com'; 

-- 11. Count the Number of Orders Placed by Users with a Specific Email Domain
SELECT COUNT(o.id) AS order_count
FROM orders o
JOIN users u ON o.customer_id = u.id
WHERE u.email LIKE 'priya@example.com';

-- 12. List Products and the Number of Times They Have Been Ordered
SELECT p.id AS product_id, p.name AS product_name, COUNT(oi.item_id) AS times_ordered
FROM products p
LEFT JOIN order_items oi ON p.id = oi.item_id
GROUP BY p.id, p.name;

-- 13. Get Orders and Their Shipping Status Based on Product Availability
SELECT o.id AS order_id, o.order_date, p.name AS product_name, oi.quantity, 
    CASE 
        WHEN oi.quantity = 0 THEN 'Out of Stock'  -- Explicitly handle quantity 0
        WHEN p.stock >= oi.quantity THEN 'Available'
        ELSE 'Out of Stock'
    END AS stock_status
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id;

-- 14. Find Users Who Have Not Purchased Any Products
SELECT u.id, u.username, u.email
FROM users u
LEFT JOIN orders o ON u.id = o.customer_id
WHERE o.id IS NULL;

-- 15. Retrieve All Orders and Check If Any Discount Code Was Used
SELECT o.id AS order_id, o.order_date, o.total_amount, d.code AS discount_code
FROM orders o
LEFT JOIN order_discounts od ON o.id = od.order_id  -- Join with the order_discounts table
LEFT JOIN discount_codes d ON od.discount_code = d.code  -- Join with the discount_codes table
WHERE d.code IS NOT NULL;

-- 16. List Products with Their Average Review Rating and Number of Review
SELECT p.id AS product_id, p.name AS product_name, AVG(r.rating) AS average_rating, COUNT(r.id) AS review_count
FROM products p
LEFT JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name;

-- 17. Retrieve Orders and Their Shipping Address for Users in a Specific City
SELECT o.id AS order_id, o.order_date, o.shipping_address, u.username
FROM orders o
JOIN users u ON o.customer_id = u.id
WHERE u.address LIKE '%Pune%';

-- 18. Get Total Revenue from Each Category
SELECT c.id AS category_id, c.name AS category_name, SUM(oi.quantity * p.price) AS total_revenue
FROM categories c
LEFT JOIN products p ON c.id = p.category_id
LEFT JOIN order_items oi ON p.id = oi.item_id
GROUP BY c.id, c.name;

-- 19. List Orders and Their Associated Ratings
SELECT o.id AS order_id, o.order_date, p.name AS product_name, AVG(r.rating) AS average_rating
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
LEFT JOIN reviews r ON p.id = r.product_id
GROUP BY o.id, o.order_date, p.name;

-- 20. Find Most Frequently Sailed Products
SELECT p.id AS product_id, p.name AS product_name, COUNT(oi.item_id) AS sales_count
FROM products p
JOIN order_items oi ON p.id = oi.item_id
GROUP BY p.id, p.name
ORDER BY sales_count DESC;