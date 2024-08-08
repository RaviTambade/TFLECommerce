-- Insert categories
INSERT INTO categories (name, description) VALUES
('Electronics', 'Devices and gadgets including phones, computers, and accessories.'),
('Books', 'A variety of books from different genres and authors.'),
('Clothing', 'Apparel including shirts, pants, and accessories.');

-- Insert users
INSERT INTO users (username, password, email, address) VALUES
('aj_boss', 'ajpassword1234', 'aj@example.com', '789 Pune, Pune'),
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
INSERT INTO products (id, name, description, price, stock, category_id) VALUES
(1, 'Smartphone', 'Latest model with high-resolution camera.', 699.99, 50, 1),
(2, 'Laptop', 'Powerful laptop with 16GB RAM and 512GB SSD.', 1199.99, 30, 1),
(3, 'Fiction Novel', 'Bestselling fiction novel by renowned author.', 19.99, 100, 2),
(4, 'Jeans', 'Stylish jeans available in various sizes.', 39.99, 100, 3),
(5, 'Smartwatch', 'Smartwatch with health tracking features.', 199.99, 60, 1),
(6, 'Tablet', 'Tablet with 10-inch display and 128GB storage.', 329.99, 40, 1),
(7, 'E-book Reader', 'E-book reader with 8GB storage.', 89.99, 150, 2),
(8, 'Shirt', 'Cotton shirt available in various colors.', 29.99, 90, 3),
(9, 'Headphones', 'Noise-cancelling wireless headphones.', 149.99, 80, 1),
(10, 'Bluetooth Speaker', 'Portable Bluetooth speaker with high bass.', 49.99, 100, 1),
(11, 'Historical Novel', 'Historical novel by famous author.', 25.99, 120, 2),
(12, 'Trousers', 'Comfortable trousers available in different sizes.', 34.99, 70, 3),
(13, 'Camera', 'Digital camera with 20MP lens.', 299.99, 20, 1),
(14, 'Smart TV', '55-inch 4K Smart TV with HDR.', 599.99, 25, 1),
(15, 'Cookbook', 'Cookbook with Indian recipes.', 15.99, 80, 2),
(16, 'Dress', 'Elegant dress available in various sizes.', 49.99, 60, 3),
(17, 'Wireless Charger', 'Fast wireless charger for smartphones.', 29.99, 110, 1),
(18, 'Mystery Novel', 'Gripping mystery novel by top author.', 21.99, 90, 2),
(19, 'Sneakers', 'Comfortable sneakers for running.', 59.99, 50, 3),
(20, 'Bluetooth Earbuds', 'True wireless Bluetooth earbuds.', 69.99, 75, 1);

-- Insert orders with varying statuses
INSERT INTO orders (customer_id, order_date, shipping_address, total_amount, shipping_date, status) VALUES
(1, '2024-07-25', '12 MG Road, Delhi', 719.98, '2024-07-26', 'Shipped'),
(2, '2024-07-26', '34 Gandhi Street, Mumbai', 1259.98, '2024-07-27', 'Shipped'),
(3, '2024-07-27', '56 Nehru Avenue, Ahmedabad', 239.98, '2024-07-28', 'Delivered'),
(4, '2024-07-28', '78 Patel Nagar, Pune', 529.98, '2024-07-29', 'Processing'),
(5, '2024-07-29', '90 Ambedkar Road, Bangalore', 179.98, '2024-07-30', 'Cancelled'),
(6, '2024-07-30', '123 Ashok Marg, Jaipur', 179.98, '2024-07-31', 'Shipped'),
(7, '2024-07-31', '456 Charminar, Hyderabad', 99.98, '2024-08-01', 'Delivered'),
(8, '2024-08-01', '789 MG Road, Chennai', 499.98, '2024-08-02', 'Processing'),
(9, '2024-08-02', '101 Nehru Place, Delhi', 449.98, '2024-08-03', 'Shipped'),
(10, '2024-08-03', '123 Banjara Hills, Hyderabad', 389.98, '2024-08-04', 'Processing'),
(11, '2024-08-04', '456 Brigade Road, Bangalore', 149.98, '2024-08-05', 'Shipped'),
(12, '2024-08-05', '789 Park Street, Kolkata', 49.98, '2024-08-06', 'Cancelled'),
(13, '2024-08-06', '101 Connaught Place, Delhi', 129.98, '2024-08-07', 'Processing'),
(14, '2024-08-07', '123 Marine Drive, Mumbai', 99.98, '2024-08-08', 'Delivered'),
(15, '2024-08-08', '456 Lalbagh, Lucknow', 99.98, '2024-08-09', 'Shipped'),
(16, '2024-08-09', '789 Residency Road, Bangalore', 179.98, '2024-08-10', 'Delivered'),
(17, '2024-08-10', '101 MG Road, Pune', 239.98, '2024-08-11', 'Processing'),
(18, '2024-08-11', '123 Race Course Road, Chennai', 529.98, '2024-08-12', 'Shipped'),
(19, '2024-08-12', '456 MG Road, Bhopal', 449.98, '2024-08-13', 'Delivered'),
(20, '2024-08-13', '789 MG Road, Coimbatore', 69.98, '2024-08-14', 'Processing');

-- Insert data into the order_items table
INSERT INTO order_items (order_id, item_id, quantity) VALUES
(1, 1, 2),
(1, 4, 5),
(2, 2, 4),
(2, 3, 8),
(3, 5, 1),
(3, 8, 1),
(4, 6, 1),
(4, 4, 5),
(5, 7, 6),
(5, 9, 2),
(6, 10, 1),
(6, 13, 1),
(7, 12, 3),
(7, 14, 1),
(8, 16, 2),
(8, 15, 3),
(9, 17, 2),
(9, 18, 1),
(10, 19, 2),
(10, 20, 1),
(11, 3, 4),
(11, 11, 1),
(12, 5, 1),
(12, 8, 1),
(13, 2, 2),
(13, 10, 2),
(14, 9, 1),
(14, 20, 1),
(15, 18, 2),
(15, 6, 1),
(16, 7, 1),
(16, 19, 1),
(17, 4, 3),
(17, 5, 1),
(18, 15, 2),
(18, 14, 1),
(19, 12, 1),
(19, 16, 1),
(20, 20, 2),
(20, 1, 1);

-- Insert reviews
INSERT INTO reviews (product_id, user_id, rating, review_text) VALUES
(1, 1, 4, 'Great phone with a good camera quality.'),
(2, 2, 5, 'Amazing laptop for the price. Highly recommended.'),
(3, 3, 3, 'Decent read but could be better.'),
(4, 4, 4, 'Comfortable jeans and good quality.'),
(5, 5, 4, 'Very useful smartwatch with excellent features.'),
(6, 6, 5, 'The tablet is very responsive and lightweight.'),
(7, 7, 4, 'Good e-book reader with a decent screen.'),
(8, 8, 4, 'Nice shirt, good fit and color.'),
(9, 9, 5, 'Excellent headphones with superb sound quality.'),
(10, 10, 3, 'Average Bluetooth speaker with okay sound.'),
(11, 11, 4, 'Interesting historical novel, well written.'),
(12, 12, 5, 'Great trousers, very comfortable.'),
(13, 13, 4, 'Good camera, takes high-quality pictures.'),
(14, 14, 5, 'Excellent TV with great picture quality.'),
(15, 15, 3, 'Decent cookbook, but not much variety.'),
(16, 16, 4, 'Elegant dress, fits well and looks great.'),
(17, 17, 5, 'Fast wireless charger, very convenient.'),
(18, 18, 4, 'Gripping mystery novel, couldn�t put it down.'),
(19, 19, 5, 'Comfortable sneakers, perfect for running.'),
(20, 20, 4, 'Good Bluetooth earbuds with decent battery life.');

-- Insert payments
INSERT INTO payments (order_id, payment_date, amount, payment_method) VALUES
(1, '2024-07-25', 719.98, 'Credit Card'),
(2, '2024-07-26', 1259.98, 'Credit Card'),
(3, '2024-07-27', 239.98, 'PayPal'),
(4, '2024-07-28', 529.98, 'Credit Card'),
(5, '2024-07-29', 179.98, 'Credit Card'),
(6, '2024-07-30', 179.98, 'PayPal'),
(7, '2024-07-31', 99.98, 'Credit Card'),
(8, '2024-08-01', 499.98, 'Credit Card'),
(9, '2024-08-02', 449.98, 'Debit Card'),
(10, '2024-08-03', 389.98, 'PayPal'),
(11, '2024-08-04', 149.98, 'Credit Card'),
(12, '2024-08-05', 49.98, 'Credit Card'),
(13, '2024-08-06', 129.98, 'Debit Card'),
(14, '2024-08-07', 99.98, 'Credit Card'),
(15, '2024-08-08', 99.98, 'PayPal'),
(16, '2024-08-09', 179.98, 'Credit Card'),
(17, '2024-08-10', 239.98, 'Debit Card'),
(18, '2024-08-11', 529.98, 'Credit Card'),
(19, '2024-08-12', 449.98, 'PayPal'),
(20, '2024-08-13', 69.98, 'Credit Card');

-- Insert shipments
INSERT INTO shipments (order_id, shipment_date, carrier, tracking_number) VALUES
(1, '2024-07-26', 'DHL', 'DHL123456789'),
(2, '2024-07-27', 'FedEx', 'FED123456789'),
(3, '2024-07-28', 'Blue Dart', 'BD123456789'),
(4, '2024-07-29', 'DHL', 'DHL987654321'),
(5, '2024-07-30', 'FedEx', 'FED987654321'),
(6, '2024-07-31', 'Blue Dart', 'BD987654321'),
(7, '2024-08-01', 'DHL', 'DHL111111111'),
(8, '2024-08-02', 'FedEx', 'FED111111111'),
(9, '2024-08-03', 'Blue Dart', 'BD111111111'),
(10, '2024-08-04', 'DHL', 'DHL222222222'),
(11, '2024-08-05', 'FedEx', 'FED222222222'),
(12, '2024-08-06', 'Blue Dart', 'BD222222222'),
(13, '2024-08-07', 'DHL', 'DHL333333333'),
(14, '2024-08-08', 'FedEx', 'FED333333333'),
(15, '2024-08-09', 'Blue Dart', 'BD333333333'),
(16, '2024-08-10', 'DHL', 'DHL444444444'),
(17, '2024-08-11', 'FedEx', 'FED444444444'),
(18, '2024-08-12', 'Blue Dart', 'BD444444444'),
(19, '2024-08-13', 'DHL', 'DHL555555555'),
(20, '2024-08-14', 'FedEx', 'FED555555555');
