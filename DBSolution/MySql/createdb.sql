CREATE DATABASE tflecommerce;
USE tflecommerce;

select * from tflecommerce.users;
select * from tflecommerce.categories;
select * from tflecommerce.orders;
select * from tflecommerce.inventory;
SELECT * FROM inventory WHERE product_id = 5;

-- Create users table
CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    address VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create categories table
CREATE TABLE categories (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    description TEXT
);

-- Create products table
CREATE TABLE products (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    description TEXT,
    price DECIMAL(10, 2) NOT NULL,
    stock INT NOT NULL,
    category_id INT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    last_modified TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (category_id) REFERENCES categories(id)
        ON UPDATE CASCADE
        ON DELETE SET NULL
);


-- Create orders table
CREATE TABLE orders (
    id INT AUTO_INCREMENT PRIMARY KEY,
    customer_id INT NOT NULL,
    order_date DATE NOT NULL,
    shipping_address VARCHAR(255) NOT NULL,
    total_amount DECIMAL(10, 2),
    shipping_date DATE,
    status VARCHAR(50),
    FOREIGN KEY (customer_id) REFERENCES users(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

CREATE TABLE order_status (
    order_id INT NOT NULL,
    status ENUM('Pending', 'Processing', 'Shipped', 'Delivered', 'Cancelled') NOT NULL,
    status_date DATE NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    PRIMARY KEY (order_id, status_date)
);

-- Create refunds table
CREATE TABLE refunds (
    refund_id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    refund_amount DECIMAL(10, 2) NOT NULL,
    refund_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

-- Create order_fulfillment table
CREATE TABLE order_fulfillment (
    fulfillment_id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    fulfillment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status ENUM('Pending', 'Packed', 'Shipped', 'Delivered', 'Returned') DEFAULT 'Pending',
    tracking_number VARCHAR(100),
    carrier_name VARCHAR(100),
    fulfillment_notes TEXT,
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

-- Create payments table
CREATE TABLE payments (
    payment_id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    payment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    payment_amount DECIMAL(10, 2) NOT NULL,
    payment_method ENUM('Credit Card', 'Debit Card', 'PayPal', 'Bank Transfer') NOT NULL,
    payment_status ENUM('Completed', 'Pending', 'Failed') DEFAULT 'Pending',
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

-- Create inventory table
CREATE TABLE inventory (
    inventory_id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT ,
    stock_quantity INT,
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

CREATE TABLE purchase_orders (
    order_id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    order_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

-- Create product_audit table
CREATE TABLE product_audit (
    audit_id INT AUTO_INCREMENT PRIMARY KEY,
    inventory_id INT,
    action_type ENUM('INSERT', 'UPDATE', 'DELETE'),
    old_stock_quantity INT,
    new_stock_quantity INT,
    action_timestamp DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Create discount_codes table
CREATE TABLE discount_codes (
    code VARCHAR(50) PRIMARY KEY,
    discount_percentage DECIMAL(5, 2) NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL
);

-- Create order_discounts table
CREATE TABLE order_discounts (
    order_id INT,
    discount_code VARCHAR(50),
    PRIMARY KEY (order_id, discount_code),
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    FOREIGN KEY (discount_code) REFERENCES discount_codes(code)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

-- Create price_changes table
CREATE TABLE price_changes (
    change_id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    old_price DECIMAL(10, 2) NOT NULL,
    new_price DECIMAL(10, 2) NOT NULL,
    change_date DATETIME NOT NULL,
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

-- Create order_items table
CREATE TABLE order_items (
    id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    item_id INT NOT NULL,
    quantity INT NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    FOREIGN KEY (item_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

-- Create reviews table
CREATE TABLE reviews (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    user_id INT NOT NULL,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    review_text TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

-- Create cart table
CREATE TABLE cart (
    cart_id INT AUTO_INCREMENT PRIMARY KEY,
    customer_id INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (customer_id) REFERENCES users(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

-- Create cart_items table
CREATE TABLE cart_items (
    cart_item_id INT AUTO_INCREMENT PRIMARY KEY,
    cart_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    FOREIGN KEY (cart_id) REFERENCES cart(cart_id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

-- Create shipping_methods table
CREATE TABLE shipping_methods (
    shipping_method_id INT AUTO_INCREMENT PRIMARY KEY,
    method_name VARCHAR(100) NOT NULL,
    description TEXT,
    cost DECIMAL(10, 2) NOT NULL
);


-- Create shipments table
CREATE TABLE shipments (
    shipment_id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    shipping_method_id INT NOT NULL,
    shipment_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    tracking_number VARCHAR(100),
    status ENUM('Pending', 'Shipped', 'In Transit', 'Delivered', 'Failed') DEFAULT 'Pending',
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    FOREIGN KEY (shipping_method_id) REFERENCES shipping_methods(shipping_method_id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);


-- Create shipment_items table
CREATE TABLE shipment_items (
    shipment_item_id INT AUTO_INCREMENT PRIMARY KEY,
    shipment_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    FOREIGN KEY (shipment_id) REFERENCES shipments(shipment_id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);



-- Create shipping_addresses table
CREATE TABLE shipping_addresses (
    shipping_address_id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    address VARCHAR(255) NOT NULL,
    city VARCHAR(50) NOT NULL,
    state VARCHAR(50) NOT NULL,
    zip_code VARCHAR(10) NOT NULL,
    country VARCHAR(50) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

-- Create subscriptions table

CREATE TABLE subscriptions (
    subscription_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    plan VARCHAR(50) NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE,
    status ENUM('Active', 'Expired', 'Cancelled') DEFAULT 'Active',
    FOREIGN KEY (user_id) REFERENCES users(id) ON UPDATE CASCADE ON DELETE CASCADE
);

-- Create table loyalty_redemption
CREATE TABLE loyalty_redemptions (
    redemption_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    points_redeemed INT NOT NULL,
    redemption_date DATETIME NOT NULL,
    status ENUM('Pending', 'Completed', 'Failed') DEFAULT 'Pending',
    FOREIGN KEY (user_id) REFERENCES users(id) ON UPDATE CASCADE ON DELETE CASCADE
);                                   -- ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;


-- Create billing_adjustments table
CREATE TABLE billing_adjustments (
    adjustment_id INT AUTO_INCREMENT PRIMARY KEY,
    user_id INT NOT NULL,
    adjustment_amount DECIMAL(10, 2) NOT NULL,
    adjustment_date DATETIME NOT NULL,
    reason VARCHAR(255),
    FOREIGN KEY (user_id) REFERENCES users(id) ON UPDATE CASCADE ON DELETE CASCADE
);


CREATE TABLE IF NOT EXISTS returns (
    return_id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    return_reason VARCHAR(255),
    return_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status ENUM('Pending', 'Processed', 'Rejected') DEFAULT 'Pending',
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

-- create table archieved_orders:
create table archieved_orders (
	order_id INT AUTO_INCREMENT PRIMARY KEY,
    order_date DATETIME NOT NULL,
    customer_id INT NOT NULL,
    total_amount DECIMAL(10,2) NOT NULL,
    STATUS VARCHAR(50)
);

create table closed_dates(
    close_id INT AUTO_INCREMENT PRIMARY KEY,
    closed_dates DATETIME NOT NULL,
    events VARCHAR(255)
);

-- Indexes for performance improvement
CREATE INDEX idx_username ON users(username);
CREATE INDEX idx_product_name ON products(name);
CREATE INDEX idx_order_date ON orders(order_date);
CREATE INDEX idx_order_item ON order_items(order_id, item_id);