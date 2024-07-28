-- Insert categories
INSERT INTO categories (name, description) VALUES
('Electronics', 'Devices and gadgets including phones, computers, and accessories.'),
('Books', 'A variety of books from different genres and authors.'),
('Clothing', 'Apparel including shirts, pants, and accessories.');

-- Insert users
INSERT INTO users (username, password, email, address) VALUES
('arun_kumar', 'password123', 'arun@example.com', '12 MG Road, Delhi'),
('meera_nair', 'securepass', 'meera@example.com', '34 Gandhi Street, Mumbai'),
('vikas_patel', 'mypassword', 'vikas@example.com', '56 Nehru Avenue, Ahmedabad'),
('sunita_verma', 'strongpass', 'sunita@example.com', '78 Patel Nagar, Pune'),
('raj_sharma', 'pass1234', 'raj@example.com', '90 Ambedkar Road, Bangalore'),
('anita_singh', 'secure123', 'anita@example.com', '123 Ashok Marg, Jaipur'),
('suresh_reddy', 'mypassword123', 'suresh@example.com', '456 Charminar, Hyderabad'),
('laxmi_menon', 'passwordsecure', 'laxmi@example.com', '789 MG Road, Chennai'),
('rahul_gupta', 'securepass456', 'rahul@example.com', '101 Nehru Place, Delhi'),
('geeta_kapoor', 'mypassword789', 'geeta@example.com', '123 Banjara Hills, Hyderabad'),
('amit_tiwari', 'strongpass123', 'amit@example.com', '456 Brigade Road, Bangalore'),
('rekha_das', 'pass9876', 'rekha@example.com', '789 Park Street, Kolkata'),
('ajay_mehta', 'securepass789', 'ajay@example.com', '101 Connaught Place, Delhi'),
('divya_bhatt', 'mypassword456', 'divya@example.com', '123 Marine Drive, Mumbai'),
('vivek_agarwal', 'passwordstrong', 'vivek@example.com', '456 Lalbagh, Lucknow'),
('priya_joshi', 'securepass1234', 'priya@example.com', '789 Residency Road, Bangalore'),
('rohit_saxena', 'mypasswordstrong', 'rohit@example.com', '101 MG Road, Pune'),
('kiran_rao', 'password456', 'kiran@example.com', '123 Race Course Road, Chennai'),
('suman_kumar', 'securepass987', 'suman@example.com', '456 MG Road, Bhopal'),
('nisha_shah', 'mypassword1234', 'nisha@example.com', '789 MG Road, Coimbatore');

-- Insert products
INSERT INTO products (name, description, price, stock, category_id) VALUES
('Smartphone', 'Latest model with high-resolution camera.', 699.99, 50, 1),
('Laptop', 'Powerful laptop with 16GB RAM and 512GB SSD.', 1199.99, 30, 1),
('Fiction Novel', 'Bestselling fiction novel by renowned author.', 19.99, 100, 2),
('Jeans', 'Stylish jeans available in various sizes.', 39.99, 75, 3),
('Smartwatch', 'Smartwatch with health tracking features.', 199.99, 60, 1),
('Tablet', 'Tablet with 10-inch display and 128GB storage.', 329.99, 40, 1),
('E-book Reader', 'E-book reader with 8GB storage.', 89.99, 150, 2),
('Shirt', 'Cotton shirt available in various colors.', 29.99, 90, 3),
('Headphones', 'Noise-cancelling wireless headphones.', 149.99, 80, 1),
('Bluetooth Speaker', 'Portable Bluetooth speaker with high bass.', 49.99, 100, 1),
('Historical Novel', 'Historical novel by famous author.', 25.99, 120, 2),
('Trousers', 'Comfortable trousers available in different sizes.', 34.99, 70, 3),
('Camera', 'Digital camera with 20MP lens.', 299.99, 20, 1),
('Smart TV', '55-inch 4K Smart TV with HDR.', 599.99, 25, 1),
('Cookbook', 'Cookbook with Indian recipes.', 15.99, 80, 2),
('Dress', 'Elegant dress available in various sizes.', 49.99, 60, 3),
('Wireless Charger', 'Fast wireless charger for smartphones.', 29.99, 110, 1),
('Mystery Novel', 'Gripping mystery novel by top author.', 21.99, 90, 2),
('Sneakers', 'Comfortable sneakers for running.', 59.99, 50, 3),
('Bluetooth Earbuds', 'True wireless Bluetooth earbuds.', 69.99, 75, 1);

-- Insert orders
-- Insert orders with varying statuses
INSERT INTO orders (customer_id, order_date, shipping_address, total_amount, shipping_date, status) VALUES
(1, '2024-07-25', '12 MG Road, Delhi', 719.98, '2024-07-26', 'Shipped'), -- Order with Smartphone and Jeans
(2, '2024-07-26', '34 Gandhi Street, Mumbai', 1259.98, '2024-07-27', 'Shipped'), -- Order with Laptop and Fiction Novel
(3, '2024-07-27', '56 Nehru Avenue, Ahmedabad', 239.98, '2024-07-28', 'Delivered'), -- Order with Smartwatch and Shirt
(4, '2024-07-28', '78 Patel Nagar, Pune', 529.98, '2024-07-29', 'Processing'), -- Order with Tablet and Jeans
(5, '2024-07-29', '90 Ambedkar Road, Bangalore', 179.98, '2024-07-30', 'Cancelled'), -- Order with E-book Reader and Shirt
(6, '2024-07-30', '123 Ashok Marg, Jaipur', 179.98, '2024-07-31', 'Shipped'), -- Order with Headphones and Cookbook
(7, '2024-07-31', '456 Charminar, Hyderabad', 99.98, '2024-08-01', 'Delivered'), -- Order with Bluetooth Speaker and Cookbook
(8, '2024-08-01', '789 MG Road, Chennai', 499.98, '2024-08-02', 'Processing'), -- Order with Historical Novel and Trousers
(9, '2024-08-02', '101 Nehru Place, Delhi', 449.98, '2024-08-03', 'Shipped'), -- Order with Camera and Dress
(10, '2024-08-03', '123 Banjara Hills, Hyderabad', 389.98, '2024-08-04', 'Processing'), -- Order with Smart TV and Shirt
(11, '2024-08-04', '456 Brigade Road, Bangalore', 149.98, '2024-08-05', 'Shipped'), -- Order with Wireless Charger and Cookbook
(12, '2024-08-05', '789 Park Street, Kolkata', 49.98, '2024-08-06', 'Cancelled'), -- Order with Mystery Novel
(13, '2024-08-06', '101 Connaught Place, Delhi', 129.98, '2024-08-07', 'Processing'), -- Order with Sneakers and Cookbook
(14, '2024-08-07', '123 Marine Drive, Mumbai', 99.98, '2024-08-08', 'Delivered'), -- Order with Bluetooth Earbuds
(15, '2024-08-08', '456 Lalbagh, Lucknow', 99.98, '2024-08-09', 'Shipped'), -- Order with Bluetooth Earbuds
(16, '2024-08-09', '789 Residency Road, Bangalore', 179.98, '2024-08-10', 'Delivered'), -- Order with Headphones and Cookbook
(17, '2024-08-10', '101 MG Road, Pune', 239.98, '2024-08-11', 'Processing'), -- Order with Smartwatch and Shirt
(18, '2024-08-11', '123 Race Course Road, Chennai', 529.98, '2024-08-12', 'Shipped'), -- Order with Tablet and Jeans
(19, '2024-08-12', '456 MG Road, Bhopal', 449.98, '2024-08-13', 'Delivered'), -- Order with Camera and Dress
(20, '2024-08-13', '789 MG Road, Coimbatore', 69.98, '2024-08-14', 'Processing'); -- Order with Bluetooth Earbuds

-- Insert order_items
INSERT INTO order_items (order_id, item_id, quantity) VALUES
(1, 1, 1), -- 1 Smartphone
(1, 4, 1), -- 1 Jeans
(2, 2, 1), -- 1 Laptop
(2, 3, 1), -- 1 Fiction Novel
(3, 5, 1), -- 1 Smartwatch
(3, 8, 1), -- 1 Shirt
(4, 6, 1), -- 1 Tablet
(4, 4, 1), -- 1 Jeans
(5, 7, 1), -- 1 E-book Reader
(5, 8, 1), -- 1 Shirt
(6, 9, 1), -- 1 Headphones
(6, 15, 1), -- 1 Cookbook
(7, 10, 1), -- 1 Bluetooth Speaker
(7, 15, 1), -- 1 Cookbook
(8, 11, 1), -- 1 Historical Novel
(8, 12, 1), -- 1 Trousers
(9, 13, 1), -- 1 Camera
(9, 16, 1), -- 1 Dress
(10, 14, 1), -- 1 Smart TV
(10, 8, 1), -- 1 Shirt
(11, 17, 1), -- 1 Wireless Charger
(11, 15, 1), -- 1 Cookbook
(12, 18, 1), -- 1 Mystery Novel
(13, 19, 1), -- 1 Sneakers
(13, 15, 1), -- 1 Cookbook
(14, 20, 1), -- 1 Bluetooth Earbuds
(15, 20, 1), -- 1 Bluetooth Earbuds
(16, 9, 1), -- 1 Headphones
(16, 15, 1), -- 1 Cookbook
(17, 5, 1), -- 1 Smartwatch
(17, 8, 1), -- 1 Shirt
(18, 6, 1), -- 1 Tablet
(18, 4, 1), -- 1 Jeans
(19, 13, 1), -- 1 Camera
(19, 16, 1), -- 1 Dress
(20, 20, 1); -- 1 Bluetooth Earbuds

-- Insert reviews
INSERT INTO reviews (product_id, user_id, rating, review_text) VALUES
(1, 1, 5, 'Fantastic smartphone with amazing features!'),
(2, 2, 4, 'Very powerful laptop, but a bit heavy.'),
(3, 3, 5, 'Great book, couldn\'t put it down!'),
(4, 4, 3, 'Jeans are good, but the fit was not perfect.'),
(5, 5, 5, 'Smartwatch with excellent features.'),
(6, 6, 4, 'Tablet is good but battery life could be better.'),
(7, 7, 5, 'E-book reader is very convenient.'),
(8, 8, 4, 'Shirt material is good but color faded after wash.'),
(9, 9, 5, 'Headphones have great sound quality.'),
(10, 10, 4, 'Bluetooth speaker is portable and has good bass.'),
(11, 11, 5, 'Historical novel was very engaging.'),
(12, 12, 3, 'Trousers are comfortable but the size was a bit off.'),
(13, 13, 5, 'Camera quality is superb.'),
(14, 14, 4, 'Smart TV has excellent picture quality.'),
(15, 15, 5, 'Cookbook has some amazing recipes.'),
(16, 16, 3, 'Dress is nice but fitting could be better.'),
(17, 17, 5, 'Wireless charger is very convenient.'),
(18, 18, 5, 'Mystery novel kept me hooked till the end.'),
(19, 19, 4, 'Sneakers are comfortable for running.'),
(20, 20, 5, 'Bluetooth earbuds have excellent sound quality.');

-- Insert discount codes
INSERT INTO discount_codes (code, discount_percentage, start_date, end_date) VALUES
('SUMMER21', 10.00, '2024-07-01', '2024-07-31'),
('WINTER21', 15.00, '2024-12-01', '2024-12-31'),
('DIWALI21', 20.00, '2024-11-01', '2024-11-15'),
('HOLI21', 18.00, '2024-03-01', '2024-03-10'),
('NEWYEAR21', 25.00, '2024-12-31', '2025-01-01'),
('INDEPENDENCE21', 15.00, '2024-08-15', '2024-08-15'),
('REPUBLIC21', 12.00, '2024-01-26', '2024-01-26'),
('SUMMER22', 10.00, '2025-07-01', '2025-07-31'),
('WINTER22', 15.00, '2025-12-01', '2025-12-31'),
('DIWALI22', 20.00, '2025-11-01', '2025-11-15'),
('HOLI22', 18.00, '2025-03-01', '2025-03-10'),
('NEWYEAR22', 25.00, '2025-12-31', '2026-01-01'),
('INDEPENDENCE22', 15.00, '2025-08-15', '2025-08-15'),
('REPUBLIC22', 12.00, '2025-01-26', '2025-01-26'),
('SUMMER23', 10.00, '2026-07-01', '2026-07-31'),
('WINTER23', 15.00, '2026-12-01', '2026-12-31'),
('DIWALI23', 20.00, '2026-11-01', '2026-11-15'),
('HOLI23', 18.00, '2026-03-01', '2026-03-10'),
('NEWYEAR23', 25.00, '2026-12-31', '2027-01-01'),
('INDEPENDENCE23', 15.00, '2026-08-15', '2026-08-15');
