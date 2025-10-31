-- use in case of set update mode error
set SQL_SAFE_UPDATES=0;

-- insert into categories table
insert into categories values(1,"flowers");
insert into categories values(2, "Electronic Devices");
insert into categories values(3, "Fashion");
insert into categories values(4, "Home & Furniture");
insert into categories values(5, "Beauty & Personal care");
insert into categories values(6, "Toys & Books");
insert into categories values(7, "Grocery");


INSERT INTO Products (Id, name, description, Price, stock, Image, Category_id)
VALUES (1, 'rose', 'valentine flower', 10, 452, '/images/flowers/Rose.png', 1);

INSERT INTO Products (Id, name, description, Price, stock, Image, Category_id)
VALUES (3, 'lotus', 'unique flower', 50, 62, '/images/flowers/Lotus.png', 1);

INSERT INTO Products(Id, name, description, Price, stock, Image, Category_id)
VALUES (6, 'Gerberra', 'merriage flower', 18, 5723, '/images/flowers/Gerbera.png', 1);

INSERT INTO Products (Id, name, description, Price, stock, Image, Category_id)
VALUES (10, 'aster', 'festival Flower', 6, 5000, '/images/flowers/Aster.png', 1);

INSERT INTO Products (Id, name, description, Price, stock, Image, Category_id)
VALUES (15, 'mogra', 'smelly flower', 2, 4563, '/images/flowers/Mogra.png', 1);

INSERT INTO Products (Id, name, description, Price, stock, Image, Category_id)
VALUES (16, 'Lily', 'Cute flower', 4, 57230, '/images/flowers/lily.png', 1);

INSERT INTO Products  (Id, name, description, Price, stock, Image, Category_id)
VALUES (17, 'hibiscus', 'perfect flower', 20, 853, '/images/flowers/Hibiscus.png', 1);

INSERT INTO Products (Id, name, description, Price, stock, Image, Category_id)
VALUES (20, 'Tulip', 'Colorful flower', 12, 89, '/images/flowers/Tulip.png', 1);



-- data for Electronics having category 2
INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (19, "Laptop", "High-performance laptop", 75000, 30, "/images/ElectronicDevices/laptop.jpeg", 2);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (11, "mobile", "favrourite of all", 25000, 853, "/images/ElectronicDevices/mobile.png", 2);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (21, "fan", "Efficient and powerful fan", 25000, 853, "/images/ElectronicDevices/fan.jpeg", 2);


-- data for Fashion  having category 3
INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (2, "topware", "Stylish and comfortable for ladies", 500, 10, "/images/Fashion/topware.png", 3);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (5, "Shoes", "Trendy and durable shoes", 1230, 25, "/images/Fashion/shoes.png", 3);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (14, "Jewellery", "Elegant and timeless jewellery", 2560, 5, "/images/Fashion/jewellery.png", 3);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (18, "Watch", "sophisticated watches", 350, 13, "/images/Fashion/watch.jpeg", 3);


-- data for Home & Furniture  having category 4
INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (4, "BedSheet", "Soft and luxurious bedsheets", 250, 16, "/images/Home_Farniture/BedSheet.jpeg", 4);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (7, "Farniture", "Stylish and functional furniture", 45600, 3, "/images/Home_Farniture/farniture.jpeg", 4);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (13, "Wall Lamp", "Modern and decorative wall lamps", 780, 9, "/images/Home_Farniture/lamp.jpeg", 4);


-- data for Beauty & Personal Care   having category 5
INSERT INTO  products (Id, name, description, Price, stock, Image, Category_id)
VALUES (8, "Beauty Products", "Premium beauty products", 9860, 5, "/images/Beauty_PersonalCare/Beautyset.jpeg", 5);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (22, "Cosmetic Products", "Quality cosmetic products", 7999, 9, "/images/Beauty_PersonalCare/cosmetic.jpeg", 5);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (28, "aloe vera gel", "aloe vera gel for natural skin and hair care", 120, 78, "/images/Beauty_PersonalCare/aloe_vera_gel.jpeg", 5);



-- data for Books & Toys  having category 6
INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (9, "Let Us C", "Simple to understand C programmming", 580, 47, "/images/Toys_Books/letusc.jpeg", 6);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (26, "Clean Code", "used for writing readable, maintainable, and efficient software.", 799, 99, "/images/Toys_Books/cleancode.jpeg", 6);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (27, "Teddy", "Soft and cuddly teddy bear", 199, 35, "/images/Toys_Books/teddy.jpeg", 6);



-- data for Grocery  having category 7
INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (12, "Wheat flour", "Fresh and finely milled wheat flour", 99, 120, "/images/Grocery/flour.jpeg", 7);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (23, "Cooking oil", "Pure and healthy cooking oil", 149, 80, "/images/Grocery/oil.jpeg", 7);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (24, "Tomato Sauce", "Rich and tangy tomato sauce", 79, 79, "/images/Grocery/tomato_souse.jpeg", 7);

INSERT INTO products (Id, name, description, Price, stock, Image, Category_id)
VALUES (25, "cheese", "Creamy and delicious cheese", 39, 65, "/images/Grocery/cheese.jpeg", 7);


-- insert into subcategories table 
insert into subcategories(product_id,category_id,categoryName) values (5,3,"shoes");
insert into subcategories(product_id,category_id,categoryName) values (14,3,"jewellery");
insert into subcategories(product_id,category_id,categoryName) values (6,1,"Gerberra");
insert into subcategories(product_id,category_id,categoryName) values (1,1,"Rose");
insert into subcategories(product_id,category_id,categoryName) values (2,3,"Topware");
insert into subcategories(product_id,category_id,categoryName) values (18,3,"watch");
insert into subcategories(product_id,category_id,categoryName) values (11,2,"mobile");
insert into subcategories(product_id,category_id,categoryName) values (19,2,"laptop");
insert into subcategories(product_id,category_id,categoryName) values (21,2,"fan");
insert into subcategories(product_id,category_id,categoryName) values (8,5,"Beauty Products");
insert into subcategories(product_id,category_id,categoryName) values (22,5,"Cosmetic Products");
insert into subcategories(product_id,category_id,categoryName) values (28,5,"aloe vera gel");
insert into subcategories(product_id,category_id,categoryName) values (12,7,"Wheat flour");
insert into subcategories(product_id,category_id,categoryName) values (23,7,"Cooking Oil");
insert into subcategories(product_id,category_id,categoryName) values (24,7,"Tomato souce");
insert into subcategories(product_id,category_id,categoryName) values (25,7,"Cheese");


-- insert into categories product table 

-- shoes
insert into  CategoryProduct values (1,"Crocs","Lightweight and comfortable Crocs" ,250,30,"/images/Fashion/Crocs.jpeg",3,1);
insert into  CategoryProduct values (2,"High heels","Elegant and stylish high heels " ,590,11,"/images/Fashion/highheels.jpeg",3,1);
insert into  CategoryProduct values (3,"sneaker","Trendy and comfortable sneakers" ,450,20,"/images/Fashion/sneakar.jpeg",3,1);
insert into  CategoryProduct values (4,"necklace","Beautifully crafted necklace" ,199,25,"/images/Fashion/necklace.jpeg",3,2);

-- flower
insert into  CategoryProduct values (5,"Pink Elegence","A vibrant and charming bloom " ,20,853,"/images/flowers/pinkelegence.jpeg",1,3);
insert into  CategoryProduct values (6,"Double dutch"," multi-layered flower" ,45,630,"/images/flowers/doubledutch.jpg",1,3);
insert into  CategoryProduct values (7,"Pink Rose","  A symbol of grace, admiration, and gentle affection in a timeless bloom." ,16,369,"/images/flowers/pinkrose.jpg",1,4);
insert into  CategoryProduct values (8,"Black Rose","Black Roses – Mysterious and captivating blooms symbolizing elegance" ,85,20,"/images/flowers/blackrose.jpeg",1,4);
insert into  CategoryProduct values (9,"Rainbow Sorbet","A dazzling blend of vibrant colors" ,16,369,"/images/flowers/RainbowSorbet.jpeg",1,4);
insert into  CategoryProduct values (10,"French Rose","French Rose – A delicate and fragrant bloom that embodies elegance," ,45,369,"/images/flowers/frenchrose.jpg",1,4);
insert into  CategoryProduct values (11,"Gown","A gown is a long, formal or elegant woman's dress," ,9800,5,"/images/Fashion/gowns.jpeg",3,5);
insert into  CategoryProduct values (12,"Tank top"," tank top is a sleeveless shirt with wide straps," ,750,36,"/images/Fashion/tanktop.jpeg",3,5);
insert into  CategoryProduct values (13,"T-Shirt","A T-shirt is a casual, collarless top with short sleeves" ,300,45,"/images/Fashion/tshirt.jpeg",3,5);


-- watch
insert into  CategoryProduct values (14,"Rolex Watch","A Rolex watch is a symbol of precision, prestige, and timeless elegance" ,560,5,"/images/Fashion/rolexwatch.jpeg",3,6);
insert into  CategoryProduct values (15,"Girls Watch"," A curated collection of timepieces, offering quality and style for every wrist." ,350,36,"/images/Fashion/girlswatch.jpeg",3,6);
insert into  CategoryProduct values (16,"Smart Watch","our smartphone's stylish companion, offering notifications, fitness tracking" ,1200,45,"/images/Fashion/smartwatch.jpeg",3,6);
insert into  CategoryProduct values (17,"Kids Watch","Delightful and durable watches that inspire learning and independence in your little ones" ,200,45,"/images/Fashion/kidswatch.jpeg",3,6);

-- mobile
insert into  CategoryProduct values (19,"IPhone","A sleek, powerful smartphone that combines cutting-edge technology with elegant design." ,109000,10,"/images/ElectronicDevices/iphone.webp",2,7);
insert into  CategoryProduct values (20,"Samsung","Innovative smartphones and electronics known for performance, style, and advanced features." ,75000,15,"/images/ElectronicDevices/samsung.jpg",2,7);
insert into  CategoryProduct values (21,"poco"," Performance-driven smartphones offering powerful features at an unbeatable value." ,35000,25,"/images/ElectronicDevices/poco.jpg",2,7);
insert into  CategoryProduct values (22,"redmi"," Affordable and feature-rich smartphones designed for reliable performance and everyday use." ,40000,36,"/images/ElectronicDevices/redmi.webp",2,7);

-- laptop
insert into  CategoryProduct values (23,"Lenovo","Dependable and innovative devices built for productivity, performance, and versatility." ,120000,9,"/images/ElectronicDevices/lenovo.jpeg",2,8);
insert into  CategoryProduct values (24,"HP","Dependable and innovative devices built for productivity, performance, and versatility." ,115000,7,"/images/ElectronicDevices/hp.webp",2,8);

-- fan
insert into  CategoryProduct values (25,"Ceiling Fan","Efficient and stylish cooling solution for comfortable airflow in any room." ,5000,26,"/images/ElectronicDevices/ceilingfan.jpeg",2,9);
insert into  CategoryProduct values (26,"Mini Table Fan"," Compact and portable fan for quick, targeted cooling anywhere you need it." ,7000,26,"/images/ElectronicDevices/minitablefan.webp",2,9);

-- face wash
insert into  CategoryProduct values (27,"Face Wash","Gentle and refreshing face wash that cleanses, hydrates, and revitalizes your skin." ,250,100,"/images/Beauty_PersonalCare/facewash.jpeg",5,10);
insert into  CategoryProduct values (28,"Moisturizer","Nourishing moisturizer that keeps your skin soft, smooth, and hydrated all day long." ,320,55,"/images/Beauty_PersonalCare/Moisturizer.jpeg",5,10);
insert into  CategoryProduct values (29,"Shampoo","Revitalizing shampoo that gently cleanses and strengthens hair for a fresh, healthy shine." ,50,78,"/images/Beauty_PersonalCare/Shampoo.jpeg",5,10);
insert into  CategoryProduct values (30,"Hair Oil","Enriching hair oil that strengthens, nourishes, and promotes healthy, shiny hair." ,75,89,"/images/Beauty_PersonalCare/Hair Oil.jpeg",5,10);

-- cosmetic
insert into  CategoryProduct values (31,"Foundation","Flawless foundation that provides smooth coverage and a natural, radiant finish." ,350,78,"/images/Beauty_PersonalCare/Foundation.jpeg",5,11);
insert into  CategoryProduct values (32,"Kajal","Intense and smudge-proof kajal for bold, defined eyes that last all day." ,150,96,"/images/Beauty_PersonalCare/kajal.png",5,11);
insert into  CategoryProduct values (33,"Lipstick ","Vibrant and long-lasting lipstick to add a pop of color and confidence to your smile." ,150,23,"/images/Beauty_PersonalCare/Lipstick.jpeg",5,11);
insert into  CategoryProduct values (34,"Nail Polish","Glossy and chip-resistant nail polish for vibrant, salon-quality nails at home." ,60,12,"/images/Beauty_PersonalCare/NailPolish.jpeg",5,11);

-- aloe vera gel
insert into  CategoryProduct values (35,"aloe vera gel","Natural soothing gel that hydrates, heals, and refreshes skin and hair." ,60,12,"/images/Beauty_PersonalCare/aloe_vera_gel.jpeg",5,12);

-- grocery
insert into  CategoryProduct values (36,"aashirvaad","High-quality whole wheat flour for soft, tasty, and nutritious rotis every day" ,125,12,"/images/Grocery/aashirvaad.jpeg",7,13);
insert into  CategoryProduct values (37,"Fortune","Freshly milled whole wheat flour for soft, fluffy rotis full of natural goodness" ,120,23,"/images/Grocery/Fortune.jpeg",7,13);

-- cooking oil
insert into  CategoryProduct values (38,"Gemini","Pure and healthy oil that enhances the taste of your everyday meals" ,149,50,"/images/Grocery/Gemini.jpeg",7,14);
insert into  CategoryProduct values (39,"SunFlower ","Light and heart-healthy oil, perfect for everyday cooking and frying" ,120,50,"/images/Grocery/SunFlower.jpeg",7,14);
insert into  CategoryProduct values (40,"Dhara ","Nutritious and light oil crafted for healthy and flavorful cooking" ,175,50,"/images/Grocery/Dhara.jpeg",7,14);

-- floor
insert into  CategoryProduct values (41,"Kissan ","Rich, tangy, and flavorful tomato ketchup that adds a delicious twist to every snack" ,75,36,"/images/Grocery/kisan.jpeg",7,15);

-- chesse
insert into  CategoryProduct values (42,"Amul","Creamy and delicious cheese made from pure milk, perfect for cooking and snacking" ,49,36,"/images/Grocery/Amul.jpeg",7,16);


-- insert into users
INSERT INTO Users (username, password, Email, address)
VALUES ('Sanika Bhor', '12345',  'sanika.bhor@example.com', 'Pune');

INSERT INTO Users (username, password, Email, address)
VALUES ('Sumit Bhor','12345',  'sumit.bhor@example.com',  'Pune');

INSERT INTO Users (username, password, Email, address)
VALUES ('Rishika Narwade','12345',  'Rishika.Narwade@example.com',  'Pune');

INSERT INTO Users (username, password, Email, address)
VALUES ('Sarthak kadam','12345',  'sarthak.kadam@example.com',  'Pune');

INSERT INTO Users (username, password, Email, address)
VALUES ('Kajal Ghule','12345',  'kajal.ghule@example.com',  'Pune');

INSERT INTO Users (username, password, Email, address)
VALUES ('pankaj bhor','12345',  'pankaj.bhor@example.com',  'Pune');

INSERT INTO Users (username, password, Email, address)
VALUES ('nikita bansode','12345',  'nikita.bansode@example.com',  'Pune');



-- insert into cart
INSERT INTO cart (customer_id, created_at)
 VALUES (1, '2025-07-25 10:00:00'); -- Cart for user 1
 
 INSERT INTO cart (customer_id, created_at) 
 VALUES (2, '2025-07-26 11:00:00');-- Cart for user 2

INSERT INTO cart (customer_id, created_at) 
VALUES  (3, '2025-07-27 12:00:00'); -- Cart for user 3


-- For customer 1's cart (cart_id = 1)
INSERT INTO cart_items (cart_id, product_id, quantity, ItemImage)
VALUES  (1, 1, 4, '/images/Fashion/Crocs.jpeg'); -- crocs

INSERT INTO cart_items (cart_id, product_id, quantity, ItemImage)
VALUES  (1, 4, 3, '/images/Fashion/necklace.jpeg'); -- necklace

-- For customer 2's cart (cart_id = 2)
INSERT INTO cart_items (cart_id, product_id, quantity, ItemImage)
VALUES  (2, 9, 2, '/images/flowers/RainbowSorbet.jpeg');        -- RainbowSorbet

-- For customer 3's cart (cart_id = 3)
INSERT INTO cart_items (cart_id, product_id, quantity, ItemImage)
VALUES  (3, 27, 1, '/images/Beauty_PersonalCare/facewash.jpeg');        -- face wash

INSERT INTO shipping_addresses (order_id, address, city, state, zip_code, country) VALUES
(1, '123 Elm Street', 'Springfield', 'IL', '62701', 'USA'),
(2, '456 Oak Avenue', 'Metropolis', 'NY', '10001', 'USA'),
(3, '789 Pine Road', 'Gotham', 'NJ', '07001', 'USA'),
(4, '101 Maple Lane', 'Smallville', 'KS', '66101', 'USA'),
(5, '202 Birch Boulevard', 'Star City', 'CA', '90210', 'USA'),
(6, '303 Cedar Drive', 'Haven', 'PA', '19001', 'USA');


-- insert into orders
INSERT INTO orders (customer_id, order_date, shipping_address, total_amount, shipping_date, status) VALUES
(1, '2025-07-28', '78 Patel Nagar, Pune', 529.98, '2024-07-29', 'Processing'), -- Order with Tablet and Jeans
(2, '2025-07-29', '90 Ambedkar Road, Bangalore', 179.98, '2024-07-30', 'Cancelled'), -- Order with E-book Reader and Shirt
(3, '2025-07-30', '123 Ashok Marg, Jaipur', 179.98, '2024-07-31', 'Shipped'), -- Order with Headphones and Cookbook
(4, '2025-07-31', '456 Charminar, Hyderabad', 99.98, '2024-08-01', 'Delivered'), -- Order with Bluetooth Speaker and Cookbook
(5, '2025-08-01', '789 MG Road, Chennai', 499.98, '2024-08-02', 'Processing'), -- Order with Historical Novel and Trousers
(6, '2025-08-02', '101 Nehru Place, Delhi', 449.98, '2024-08-03', 'Shipped'), -- Order with Camera and Dress
(7, '2025-08-03', '123 Banjara Hills, Hyderabad', 389.98, '2024-08-04', 'Processing'); -- Order with Smart TV and Shirt

update orders set shipping_address_id=1 where id=1;
update orders set shipping_address_id=2 where id=2;
update orders set shipping_address_id=3 where id=3;
update orders set shipping_address_id=4 where id=4;
update orders set shipping_address_id=5 where id=5;
update orders set shipping_address_id=6 where id=6;
update orders set shipping_address_id=4 where id=7;

-- insert into order status
INSERT INTO order_status (order_id, status, status_date) VALUES
(1, 'Shipped', '2025-07-26'),
(2, 'Shipped', '2025-07-27'),
(3, 'Delivered', '2025-07-28'),
(4, 'Processing', '2025-07-28'),
(5, 'Cancelled', '2025-07-29'),
(6, 'Shipped', '2025-07-30');

-- insert into order fulfillment
INSERT INTO order_fulfillment (order_id, fulfillment_date, status, tracking_number, carrier_name, fulfillment_notes) VALUES
(1, '2025-08-04 15:00:00', 'Pending', NULL, NULL, 'Awaiting packing.'),
(2, '2025-08-05 16:00:00', 'Shipped', 'TRACK123459', 'UPS', 'Shipped and on its way.'),
(3, '2025-08-06 17:00:00', 'Packed', 'TRACK123460', 'FedEx', 'Packed and awaiting pickup.'),
(4, '2025-08-07 18:00:00', 'Delivered', 'TRACK123461', 'DHL', 'Delivered successfully.'),
(5, '2025-08-08 19:00:00', 'Shipped', 'TRACK123462', 'UPS', 'Shipped with UPS.'),
(6, '2025-08-09 20:00:00', 'Pending', NULL, NULL, 'Packing in progress.'),
(7, '2025-08-10 21:00:00', 'Shipped', 'TRACK123463', 'FedEx', 'Shipped via FedEx.');


-- insert into payments
INSERT INTO payments (order_id, payment_amount, payment_method, payment_status) VALUES
(1, 529.98, 'Bank Transfer', 'Pending'), -- Payment for order 1
(2, 179.98, 'Credit Card', 'Failed'), -- Payment for order 2
(3, 179.98, 'PayPal', 'Completed'), -- Payment for order 3
(4, 99.98, 'Debit Card', 'Completed'), -- Payment for order 4
(5, 499.98, 'Credit Card', 'Pending'), -- Payment for order 5
(6, 449.98, 'Bank Transfer', 'Completed'), -- Payment for order 6
(7, 389.98, 'PayPal', 'Failed'); -- Payment for order 7

INSERT INTO purchase_orders(order_id,quantity,order_date,product_id)values
(1,50,'2025-07-25',1),
(2,50,'2025-07-25',1),
(3,50,'2025-07-25',1),
(4,50,'2025-07-25',1),
(5,50,'2025-07-25',1),
(6,50,'2025-07-25',1);

insert into product_audit(inventory_id, action_type,old_stock_quantity,new_stock_quantity,action_timestamp)values
(2,'INSERT',40,20,'2025-08-21'),
(3,'UPDATE',40,20,'2025-08-21'),
(4,'DELETE',40,20,'2025-08-21'),
(5,'INSERT',40,20,'2025-08-21'),
(6,'UPDATE',40,20,'2025-08-21');

INSERT INTO discount_codes (code, discount_percentage, start_date, end_date) VALUES
('SUMMER21', 10.00, '2025-07-01', '2025-07-31'),
('WINTER21', 15.00, '2025-12-01', '2025-12-31'),
('DIWALI21', 20.00, '2021-11-01', '2025-11-15'),
('HOLI21', 18.00, '2025-03-01', '2025-03-10'),
('NEWYEAR21', 25.00, '2025-12-31', '2025-01-01'),
('INDEPENDENCE21', 15.00, '2022-08-15', '2025-08-15'),
('REPUBLIC21', 12.00, '2023-01-26', '2025-01-26'),
('SUMMER22', 10.00, '2025-07-01', '2025-07-31'),
('WINTER22', 15.00, '2025-12-01', '2025-12-31'),
('DIWALI22', 20.00, '2025-11-01', '2025-11-15'),
('HOLI22', 18.00, '2025-03-01', '2025-03-10'),
('NEWYEAR22', 25.00, '2025-05-31', '2026-01-01'),
('INDEPENDENCE22', 15.00, '2023-08-15', '2025-08-15'),
('REPUBLIC22', 12.00, '2022-01-26', '2025-01-26'),
('SUMMER23', 10.00, '2021-07-01', '2026-07-31'),
('WINTER23', 15.00, '2020-12-01', '2026-12-31'),
('DIWALI23', 20.00, '2021-11-01', '2026-11-15'),
('HOLI23', 18.00, '2022-03-01', '2026-03-10'),
('NEWYEAR23', 25.00, '2023-12-31', '2027-01-01'),
('INDEPENDENCE23', 15.00, '2022-08-15', '2026-08-15');

INSERT INTO order_discounts (order_id, discount_code) VALUES
(1, 'SUMMER21'),     -- Assuming order_id 1 used SUMMER21 discount
(2, 'WINTER21'),     -- Assuming order_id 2 used WINTER21 discount
(3, 'DIWALI21'),     -- Assuming order_id 3 used DIWALI21 discount
(4, 'HOLI21'),       -- Assuming order_id 4 used HOLI21 discount
(5, 'NEWYEAR21'),    -- Assuming order_id 5 used NEWYEAR21 discount
(6, 'INDEPENDENCE21'), -- Assuming order_id 6 used INDEPENDENCE21 discount
(7, 'REPUBLIC21');   -- Assuming order_id 7 used REPUBLIC21 discount

INSERT INTO order_items (order_id, item_id, quantity) VALUES
(1, 1, 2), -- 2 crocs
(1, 4, 5), -- 5 necklace
(2, 2, 4), -- 4 high heel
(2, 7, 8), -- 8 pink rose
(3, 13, 1), -- 1 t shirt
(3, 16, 1), -- 11 smart watch
(4, 19, 1), -- 10 iphone
(4, 4, 5), -- 5 high heels
(5, 23, 6), -- 6 lenovo laptop
(5, 29, 7), -- 7 Shampoo
(6, 16, 1), -- 1 smart watch
(6, 15, 5), -- 5 girls watch
(7, 39, 1), -- 1 sunflower oil
(7, 33, 4); -- 4 lipsticks

INSERT INTO reviews (product_id, user_id, rating, review_text) VALUES
(1, 1, 5, 'Fantastic smartphone with amazing features!'),
(1, 2, 3, 'Good'),
(2, 2, 4, 'Very powerful laptop, but a bit heavy.'),
(3, 3, 5, 'Great book, couldn\'t put it down!'),
(4, 4, 3, 'Jeans are good, but the fit was not perfect.'),
(5, 5, 5, 'Smartwatch with excellent features.'),
(6, 6, 4, 'Tablet is good but battery life could be better.'),
(7, 7, 5, 'E-book reader is very convenient.');


INSERT INTO shipping_methods (method_name, description, cost) VALUES
('Standard Shipping', 'Delivery within 5-7 business days', 5.99),
('Express Shipping', 'Delivery within 2-3 business days', 12.99),
('Overnight Shipping', 'Next day delivery', 24.99),
('International Shipping', 'Delivery within 10-15 business days', 39.99),
('Economy Shipping', 'Delivery within 7-10 business days', 4.99),
('Priority Shipping', 'Delivery within 3-5 business days', 9.99),
('Two-Day Shipping', 'Guaranteed delivery in 2 business days', 15.99),
('Same-Day Shipping', 'Delivery on the same day', 29.99),
('Next-Day Air', 'Next day delivery by air', 19.99),
('Standard International', 'Standard international delivery', 49.99),
('Expedited International', 'Expedited international delivery', 69.99),
('Courier Service', 'Courier delivery within 1-2 business days', 14.99),
('Flat Rate Shipping', 'Flat rate shipping cost', 6.99),
('Freight Shipping', 'Freight delivery for large items', 99.99),
('Delivery by Sea', 'Sea freight delivery', 29.99),
('Delivery by Rail', 'Rail freight delivery', 39.99),
('Delivery by Air Cargo', 'Air cargo delivery', 89.99),
('Local Delivery', 'Local delivery within the same city', 7.99),
('Regional Delivery', 'Delivery within the region', 8.99),
('Express International', 'Express international delivery', 59.99);

INSERT INTO shipments (order_id, shipping_method_id, shipment_date, tracking_number, status) VALUES
(1, 1, '2025-07-25', 'TRACK001', 'Shipped'),
(2, 2, '2025-07-26', 'TRACK002', 'Shipped'),
(3, 3, '2025-07-27', 'TRACK003', 'Delivered'),
(4, 4, '2025-07-28', 'TRACK004', 'Pending'), -- Changed 'Processing' to 'Pending'
(5, 5, '2025-07-29', 'TRACK005', 'Failed'),   -- Changed 'Cancelled' to 'Failed'
(6, 6, '2025-07-30', 'TRACK006', 'Shipped'),
(7, 7, '2025-07-31', 'TRACK007', 'Delivered');


-- Sample data for subscriptions
INSERT INTO subscriptions (user_id, plan, start_date, end_date, status) VALUES
(1, 'Basic Plan', '2025-01-15', '2025-07-15', 'Expired'),
(2, 'Premium Plan', '2025-03-01', NULL, 'Active'),
(3, 'Standard Plan', '2025-05-20', '2025-11-20', 'Active'),
(4, 'Basic Plan', '2025-02-10', '2025-08-10', 'Expired');


-- Sample data for loyalty_redemptions
INSERT INTO loyalty_redemptions (user_id, points_redeemed, redemption_date, status) VALUES
(1, 150, '2025-06-05 14:30:00', 'Completed'),
(2, 300, '2025-07-01 09:15:00', 'Pending'),
(3, 200, '2025-05-25 16:45:00', 'Completed'),
(4, 50, '2025-07-15 11:00:00', 'Failed');

-- Sample data for billing_adjustments
INSERT INTO billing_adjustments (user_id, adjustment_amount, adjustment_date, reason) VALUES
(1, -20.00, '2025-06-10 10:00:00', 'Discount applied'),
(2, 15.75, '2025-07-05 13:00:00', 'Billing error correction'),
(3, -5.50, '2025-05-30 08:00:00', 'Refund for overcharge'),
(4, 25.00, '2025-07-10 12:00:00', 'Late fee');

-- Insert sample data into the `returns` table
INSERT INTO returns (order_id, product_id, return_reason, return_date, status) VALUES
(1, 1, 'Defective item', '2024-07-27', 'Pending'),  -- Return for Smartphone in Order 1
(2, 3, 'Incorrect item received', '2024-07-28', 'Pending'),  -- Return for Fiction Novel in Order 2
(3, 5, 'Product not as described', '2024-07-29', 'Pending'),  -- Return for Smartwatch in Order 3
(4, 6, 'Wrong item received', '2024-07-30', 'Pending'),  -- Return for Tablet in Order 4
(5, 8, 'Changed mind', '2024-07-31', 'Pending'),  -- Return for Shirt in Order 5
(6, 10, 'Defective item', '2024-08-01', 'Pending'),  -- Return for Bluetooth Speaker in Order 6
(7, 10, 'Product not as described', '2024-08-02', 'Pending'); -- Return for Bluetooth Speaker in Order 7

-- Insert sample data into the closed_dates table:
INSERT INTO closed_dates(close_date, event_name) values
('2025-10-15', "Diwali"),
('2025-09-18', "Dussehra"); 

INSERT INTO inventory(product_id , stock_quantity)values(1,90);
INSERT INTO inventory(product_id , stock_quantity)values(2,200);
INSERT INTO inventory(product_id , stock_quantity)values(3,300);
INSERT INTO inventory(product_id , stock_quantity)values(4,400);
INSERT INTO inventory(product_id , stock_quantity)values(5,500);
INSERT INTO inventory(product_id , stock_quantity)values(6,600);
INSERT INTO inventory(product_id , stock_quantity)values(7,70);
INSERT INTO inventory(product_id , stock_quantity)values(8,200);
INSERT INTO inventory(product_id , stock_quantity)values(9,100);
INSERT INTO inventory(product_id , stock_quantity)values(10,200);
INSERT INTO inventory(product_id , stock_quantity)values(11,100);
INSERT INTO inventory(product_id , stock_quantity)values(12,200);
INSERT INTO inventory(product_id , stock_quantity)values(13,100);
INSERT INTO inventory(product_id , stock_quantity)values(14,200);
INSERT INTO inventory(product_id , stock_quantity)values(15,100);

INSERT INTO refunds (order_id, product_id, refund_amount, refund_date) VALUES
(1, 1, 250, '2025-08-01 10:00:00'), -- crocs
(2, 2, 590, '2025-08-02 11:00:00'), -- hign heels
(3, 3, 450, '2025-08-03 09:00:00'),   -- sneaker
(4, 4, 199, '2025-08-04 10:00:00'),   -- necklace
(5, 5, 20, '2025-08-05 14:00:00');  -- pink rose

INSERT INTO price_changes (product_id, old_price, new_price, change_date) VALUES
(1, 250, 200, '2025-08-01 10:00:00'),  -- crocs
(2, 590, 500, '2025-08-01 11:00:00'), -- high heels
(3, 450, 420, '2025-08-02 09:00:00');    -- sneaker



update shipping_addresses set userid=2 where shipping_address_id=6;