INSERT INTO categories (name, description) VALUES
('Electronics', 'Devices and gadgets including phones, computers, and accessories.'),
('Books', 'A variety of books from different genres and authors.'),
('Clothing', 'Apparel including shirts, pants, and accessories.');

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

INSERT INTO products (name, description, price, stock, category_id) VALUES
('Smartphone', 'Latest model with high-resolution camera.', 699.99, 50, 1),
('Laptop', 'Powerful laptop with 16GB RAM and 512GB SSD.', 1199.99, 30, 1),
('Fiction Novel', 'Bestselling fiction novel by renowned author.', 19.99, 100, 2),
('Jeans', 'Stylish jeans available in various sizes.', 39.99, 100, 3),
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
(10, '2024-08-03', '123 Banjara Hills, Hyderabad', 389.98, '2024-08-04', 'Processing');


INSERT INTO order_items (order_id, item_id, quantity) VALUES
(1, 1, 2), -- 2 Smartphones
(1, 4, 5), -- 5 Jeans
(2, 2, 4), -- 4 Laptops
(2, 3, 8), -- 8 Fiction Novels
(3, 5, 1), -- 1 Smartwatch
(3, 8, 11), -- 11 Shirts
(4, 6, 10), -- 10 Tablets
(4, 4, 15), -- 15 Jeans
(5, 7, 16), -- 16 E-book Readers
(5, 8, 17), -- 17 Shirts
(6, 9, 1), -- 1 Headphones
(6, 15, 5), -- 5 Cookbooks
(7, 10, 1), -- 1 Bluetooth Speaker
(7, 15, 4), -- 4 Cookbooks
(8, 11, 1), -- 1 Historical Novel
(8, 12, 1), -- 1 Trousers
(9, 13, 1), -- 1 Camera
(9, 16, 1), -- 1 Dress
(10, 14, 1), -- 1 Smart TV
(10, 8, 1); -- 1 Shirt


INSERT INTO reviews (product_id, user_id, rating, review_text) VALUES
(1, 1, 5, 'Fantastic smartphone with amazing features!'),
(1, 2, 3, 'Good'),
(2, 2, 4, 'Very powerful laptop, but a bit heavy.'),
(3, 3, 5, 'Great read!'),
(4, 4, 4, 'Nice quality, fits well.'),
(5, 5, 5, 'Absolutely love the smartwatch, especially the health tracking features.'),
(6, 6, 3, 'Decent performance, but overpriced.'),
(7, 7, 5, 'Very convenient for reading on the go.'),
(8, 8, 4, 'Comfortable, good quality shirt.'),
(9, 9, 5, 'Amazing sound quality, worth every penny.'),
(10, 10, 5, 'Love the speaker, excellent bass and portability.');


INSERT INTO shipping_addresses (order_id, address, city, state, zip_code, country) VALUES
(1, '123 Maple Street', 'Springfield', 'IL', '62701', 'USA'),
(2, '456 Oak Avenue', 'Shelbyville', 'IL', '62565', 'USA'),
(3, '789 Pine Road', 'Capital City', 'IL', '62703', 'USA'),
(4, '101 Birch Lane', 'Ogdenville', 'IL', '62629', 'USA'),
(5, '202 Cedar Street', 'North Haverbrook', 'IL', '62704', 'USA'),
(6, '303 Elm Drive', 'Brockway', 'IL', '62558', 'USA'),
(7, '404 Fir Avenue', 'Evergreen Terrace', 'IL', '62705', 'USA'),
(8, '505 Willow Way', 'Lanford', 'IL', '62706', 'USA'),
(9, '606 Maple Grove', 'Arlen', 'TX', '76001', 'USA'),
(10, '707 Ash Court', 'Hill Valley', 'CA', '94501', 'USA');


