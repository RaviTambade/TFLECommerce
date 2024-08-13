------ Products API -------

-- 1. Retrieve All Products
-- Endpoint: /api/products
-- Method: GET


-- 2. Retrieve Products in a Specific Category
-- Endpoint: /api/products/category/{categoryId}
-- Method: GET

-- 3. Retrieve Products and Their Categories 
-- Endpoint: /api/products/categories
-- Method: GET

-- 4. Find Products with Low Stock
-- Endpoint: /api/products/low-stock?threshold={threshold}
-- Method: GET

-- 5. Retrieve Product Reviews
-- Endpoint: /api/products/{productId}/reviews
-- Method: GET

-- 6. Find Most Expensive Products in Each Category
-- Endpoint: /api/products/most-expensive-per-category
-- Method: GET


-- 7. Add a new Product
-- Endpoint: /api/products
-- Method: POST


-- 8. Update Stock for a Product
-- Endpoint: /api/products/{productId}/stock
-- Method: PUT


-- 9. List Top 3 Products by Total Sales
-- Endpoint: /api/products/top-sales
-- Method: GET


-- 10. Count Products in Each Category
-- Endpoint: /api/products/count-by-category
-- Method: GET


-- 11. Find Products That Have Never Been Ordered
-- Endpoint: /api/products/never-ordered
-- Method: GET


-- 12. Retrieve Products with More than a Specified Stock
-- Endpoint: /api/orders/date-range?startDate={startDate}&endDate={endDate}
-- Method: GET


------ Orders API ------

-- 1. Retrieve Users with More Than One Order
-- Endpoint: /api/users/multiple-orders
-- Method: GET

-- 2. Delete an Order
-- Endpoint: /api/orders/{orderId}/
-- Method: DELETE


-- 3. Calculate Total Sales for a Given Month
-- Endpoint: /api/sales/total?month={month}&year={year}
-- Method: GET


-- 4. Retrieve a User's Order History
-- Endpoint: /api/users/{userId}/orders
-- Method: GET

-- 5. Retrieve Orders Placed Within a Specific Date Range
-- Endpoint: /api/orders/date-range
-- Method: GET


-- 6. Apply a Discount Code
		-- First, check if the discount code is valid
		-- Then apply the discount to an order
		-- Assuming the discount percentage was retrieved and is valid
-- Endpoint: /api/orders/{orderId}/apply-discount
-- Method: POST


-- 7. Retrieve Monthly Sales Report
-- Endpoint: /api/sales/report/monthly?month={month}&year={year}
-- Method: GET


-- 8. Retrieve Orders with Their Total Amounts
-- Endpoint: /api/orders/total-amount
-- Method: GET


-- 9. Get Total Revenue Per Product 
-- Endpoint: /api/products/revenue
-- Method: GET


-- 10. Find Top Rated Products
-- Endpoint: /api/products/top-rated
-- Method: GET


-- 11. Retrieve Latest Orders
-- Endpoint: /api/orders/latest?limit={limit}
-- Method: GET


-- 12. Find Orders with Products Above a Certain Price 
-- Endpoint: /api/orders/above-price
-- Method: GET


-- 13. Retrieve Order Details Including User Information
-- Endpoint: /api/orders/{orderId}/details
-- Method: GET


-- 14. Get All Orders with Their Items and Prices
-- Endpoint:/api/orders/items-prices
-- Method: GET

-- 15. Retrieve Orders with Discounts Applied
-- Endpoint:/api/orders/with-discounts
-- Method: GET


               
------ Users API -----

-- 1. Find Users with the Most Orders
-- Endpoint: /api/users/most-orders?limit={limit}
-- Method: GET


-- 2. Get Total Orders and Total Amount Spent by Each User
-- Endpoint: /api/users/orders-total
-- Method: GET


-- 3. Retrieve Orders with Items and Their Prices Above a Certain Amount
-- Endpoint: /api/orders/with-total-price
-- Method: GET



-- 4. Retrieve Orders with Discounts Applied
-- Endpoint: /api/orders/with-discounts
-- Method: GET


-- 5. Find Average Order Amount Per User
-- Endpoint: /api/users/average-order-amount
-- Method: GET


-- 6. Find Customers Who Have Never Made a Purchase
-- Endpoint: /api/users/no-purchase
-- Method: GET

------- Reviews API--------

--1 Retrieve Products with the Most Positive Reviews
-- Endpoint: /api/products/top-reviewed
-- Method: GET

--2 Retrieve All Products with Reviews and Their Average Rating
-- Endpoint: /api/products/average-rating
-- Method: GET


-- 3. Retrieve Top 5 Most Reviewed Products
-- Endpoint: /api/products/top-reviewed?limit=5
-- Method: GET


-- 4. Find the Most Recent Review for Each Product
-- Endpoint: /api/products/{productId}/latest-review
-- Method: GET


-- 5. Get Average Rating for Each Product
-- Endpoint: /api/products/average-rating
-- Method: GET


------------------------------------------------------------------------------------------------------------

------ Products Queries ------


-- 1. Retrieve All Products


-- 2. Retrieve Products in a Specific Category
SELECT p.id, p.name, p.price, p.stock
FROM products p
JOIN categories c ON p.category_id = c.id
WHERE c.name = 'Books';


-- 3. Retrieve Products and Their Categories 
SELECT p.id AS product_id, p.name AS product_name, c.name AS category_name
FROM products p
JOIN categories c ON p.category_id = c.id;



-- 4. Find Products with Low Stock
SELECT id, name, stock
FROM products
WHERE stock > 10;  -- Replace with the desired stock threshold

-- 5. Retrieve Product Reviews
SELECT r.rating, r.review_text, u.username
FROM reviews r
JOIN users u ON r.user_id = u.id
WHERE r.product_id = 1;  -- Replace with the product ID you're interested in


-- 6. Find Most Expensive Products in Each Category
SELECT c.name AS category_name, p.name AS product_name, p.price AS highest_price
FROM products p
JOIN categories c ON p.category_id = c.id
WHERE p.price = (
    SELECT MAX(p2.price)
    FROM products p2
    WHERE p2.category_id = p.category_id
);
--5. Get Product Reviews 
SELECT r.rating, r.review_text, u.username
FROM reviews r ,users u
WHERE r.product_id = 1;


-- 7. Add a new Product
INSERT INTO products (name, description, price, stock, category_id)
VALUES ('Wireless Headphones', 'Over-ear wireless headphones with noise cancellation.', 299.99, 20, 1);


-- 8. Update Stock for a Product
UPDATE products SET stock = 50 WHERE id = 1;  -- Replace with the product ID you want to update

-- 9. List Top 3 Products by Total Sales
SELECT p.id AS product_id, p.name AS product_name, SUM(oi.quantity * p.price) AS total_sales
FROM order_items oi
JOIN products p ON oi.item_id = p.id
GROUP BY p.id, p.name
ORDER BY total_sales DESC;

-- 10. Count Products in Each Category
SELECT c.name AS category_name, COUNT(p.id) AS product_count
FROM products p
JOIN categories c ON p.category_id = c.id
GROUP BY c.id, c.name;

-- 11. Find Products That Have Never Been Ordered
SELECT p.id AS product_id, p.name AS product_name
FROM products p
LEFT JOIN order_items oi ON p.id = oi.item_id
WHERE oi.item_id IS NULL;

-- 12. Retrieve Products with More than a Specified Stock
SELECT id, name, stock
FROM products
WHERE stock > 50;  -- Replace with the desired stock threshold

--------------------------------------------------------------------------------------------------------------

------ Orders Queries ------


-- 1. Retrieve Users with More Than One Order
SELECT u.id, u.username, COUNT(o.id) AS order_count
FROM users u
JOIN orders o ON u.id = o.customer_id
GROUP BY u.id, u.username
HAVING COUNT(o.id) > 0;


-- 2. Delete an Order

-- 3. Calculate Total Sales for a Given Month
SELECT SUM(oi.quantity * p.price) AS total_sales
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE YEAR(o.order_date) = 2024 AND MONTH(o.order_date) = 8;  -- Replace with the desired year and month


-- 4. Retrieve a User's Order History
SELECT o.id AS order_id, o.order_date, o.total_amount, oi.item_id, p.name AS product_name, oi.quantity
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE o.customer_id = 3;  -- Replace with the user ID you're interested in


-- 5. Retrieve Orders Placed Within a Specific Date Range
SELECT id, order_date, total_amount
FROM orders
WHERE order_date BETWEEN '2024-01-01' AND '2024-07-31' 
order by order_date desc;  -- Replace with desired date range


-- 6. Apply a Discount Code
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


-- 7. Retrieve Monthly Sales Report
SELECT p.name AS product_name, SUM(oi.quantity) AS total_quantity_sold, SUM(oi.quantity * p.price) AS total_sales
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE YEAR(o.order_date) = 2024 AND MONTH(o.order_date) = 7  -- Replace with desired year and month
GROUP BY p.id, p.name;


-- 8. Retrieve Orders with Their Total Amounts
SELECT o.id AS order_id, o.order_date, o.total_amount, u.username
FROM orders o
JOIN users u ON o.customer_id = u.id;


-- 9. Get Total Revenue Per Product
SELECT p.id AS product_id, p.name AS product_name,sum(oi.quantity), SUM(oi.quantity * p.price) AS total_revenue
FROM order_items oi
JOIN products p ON oi.item_id = p.id
GROUP BY p.id, p.name;


-- 10. Find Top Rated Products
SELECT p.id, p.name, AVG(r.rating) AS average_rating
FROM products p
JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name 
HAVING AVG(r.rating) >= 4;  -- Replace with the minimum average rating desired


-- 11. Retrieve Latest Orders
SELECT TOP 5 id, order_date, total_amount
FROM orders
ORDER BY order_date DESC;


-- 12. Find Orders with Products Above a Certain Price
SELECT o.id as order_id, p.name AS product_name, oi.quantity, p.price
FROM orders as o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE p.price > 100;  -- Replace with the desired price threshold


-- 13. Retrieve Order Details Including User Information
SELECT o.id AS order_id, o.order_date, u.username, u.email, o.shipping_address, o.total_amount
FROM orders o
JOIN users u ON o.customer_id = u.id;


-- 14. Get All Orders with Their Items and Prices
SELECT o.id AS order_id, o.order_date, p.name AS product_name, oi.quantity, p.price, (oi.quantity * p.price) AS total_price
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
ORDER BY o.order_date DESC;

-- 15. Retrieve Orders with Discounts Applied
SELECT o.id AS order_id, o.order_date, o.total_amount, d.code AS discount_code, d.discount_percentage
FROM orders o, discount_codes d
WHERE o.order_date BETWEEN d.start_date AND d.end_date;


--------------------------------------------------------------------------------------------------------------


------ Users API -----


-- 1. Find Users with the Most Orders
SELECT u.id, u.username, COUNT(o.id) AS order_count
FROM users u
JOIN orders o ON u.id = o.customer_id
GROUP BY u.id, u.username
ORDER BY order_count DESC;  -- Top 5 users with the most orders


-- 2. Get Total Orders and Total Amount Spent by Each User
SELECT u.id AS user_id, u.username, COUNT(o.id) AS total_orders, SUM(o.total_amount) AS total_spent
FROM users u
LEFT JOIN orders o ON u.id = o.customer_id
GROUP BY u.id, u.username;


-- 3. Retrieve Orders with Items and Their Prices Above a Certain Amount
SELECT o.id AS order_id, p.name AS product_name, oi.quantity, p.price, (oi.quantity * p.price) AS total_price
FROM orders o
JOIN order_items oi ON o.id = oi.order_id
JOIN products p ON oi.item_id = p.id
WHERE (oi.quantity * p.price) > 300
ORDER BY total_price ASC; -- Replace with the desired price threshold



-- 4. Find Average Order Amount Per User
SELECT u.id AS user_id, u.username, AVG(o.total_amount) AS average_order_amount
FROM users u
JOIN orders o ON u.id = o.customer_id
GROUP BY u.id, u.username;


-- 6. Find Customers Who Have Never Made a Purchase
SELECT u.id, u.username, u.email
FROM users u
LEFT JOIN orders o ON u.id = o.customer_id
WHERE o.id IS NULL;

--------------------------------------------------------------------------------------------------------------


------- Reviews API--------


--1 Retrieve Products with the Most Positive Reviews
SELECT p.id AS product_id, p.name AS product_name, AVG(r.rating) AS average_rating
FROM products p
JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name
HAVING AVG(r.rating) >= 4;  


--2 Retrieve All Products with Reviews and Their Average Rating
SELECT p.id AS product_id, p.name AS product_name, AVG(r.rating) AS average_rating
FROM products p
LEFT JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name;


-- 3. Retrieve Top 5 Most Reviewed Products
SELECT p.id AS product_id, p.name AS product_name, COUNT(r.id) AS review_count
FROM products p
LEFT JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name
ORDER BY review_count DESC;


-- 4. Find the Most Recent Review for Each Product
SELECT p.id AS product_id, p.name AS product_name, r.review_text, r.created_at
FROM products p
JOIN reviews r ON p.id = r.product_id
WHERE r.created_at = (
    SELECT MAX(created_at)
    FROM reviews
    WHERE product_id = p.id
);


--5. Get Product Reviews 
SELECT r.rating, r.review_text, u.username
FROM reviews r ,users u
WHERE r.product_id = 1;


-- 6. Get Average Rating for Each Product
SELECT p.id AS product_id, p.name AS product_name, AVG(r.rating) AS average_rating
FROM products p
JOIN reviews r ON p.id = r.product_id
GROUP BY p.id, p.name;

