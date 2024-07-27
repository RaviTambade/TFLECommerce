-- Insert categories
INSERT INTO categories (name, description) VALUES
('Electronics', 'Devices and gadgets including phones, computers, and accessories.'),
('Books', 'A variety of books from different genres and authors.'),
('Clothing', 'Apparel including shirts, pants, and accessories.');

-- Insert users
INSERT INTO users (username, password, email, address) VALUES
('john_doe', 'password123', 'john@example.com', '123 Elm Street'),
('jane_smith', 'securepass', 'jane@example.com', '456 Oak Avenue'),
('bob_johnson', 'mypassword', 'bob@example.com', '789 Pine Road');

-- Insert products
INSERT INTO products (name, description, price, stock, category_id) VALUES
('Smartphone', 'Latest model with high-resolution camera.', 699.99, 50, 1),
('Laptop', 'Powerful laptop with 16GB RAM and 512GB SSD.', 1199.99, 30, 1),
('Fiction Novel', 'Bestselling fiction novel by renowned author.', 19.99, 100, 2),
('Jeans', 'Stylish jeans available in various sizes.', 39.99, 75, 3);

-- Insert orders
INSERT INTO orders (customer_id, order_date, shipping_address, total_amount) VALUES
(1, '2024-07-25', '123 Elm Street', 719.98), -- Order with Smartphone and Jeans
(2, '2024-07-26', '456 Oak Avenue', 1259.98); -- Order with Laptop and Fiction Novel

-- Insert order_items
INSERT INTO order_items (order_id, item_id, quantity) VALUES
(1, 1, 1), -- 1 Smartphone
(1, 4, 1), -- 1 Jeans
(2, 2, 1), -- 1 Laptop
(2, 3, 1); -- 1 Fiction Novel

-- Insert reviews
INSERT INTO reviews (product_id, user_id, rating, review_text) VALUES
(1, 1, 5, 'Fantastic smartphone with amazing features!'),
(2, 2, 4, 'Very powerful laptop, but a bit heavy.'),
(3, 3, 5, 'Great book, couldn\'t put it down!'),
(4, 1, 3, 'Jeans are good, but the fit was not perfect.');

-- Insert discount codes
INSERT INTO discount_codes (code, discount_percentage, start_date, end_date) VALUES
('SUMMER21', 10.00, '2024-07-01', '2024-07-31'),
('WINTER21', 15.00, '2024-12-01', '2024-12-31');
