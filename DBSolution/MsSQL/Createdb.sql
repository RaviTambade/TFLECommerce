CREATE DATABASE tflecommerce;
Use tflecommerce;

ALTER DATABASE tflecommerce SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

DROP DATABASE tflecommerce;

 CREATE TABLE users (
    id INT Identity (1,1)PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    address VARCHAR(255),
    created_at DATETIME DEFAULT GETDATE()
);
 
CREATE TABLE categories (
    id INT Identity(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    description TEXT
);

CREATE TABLE products (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    description TEXT,
    price DECIMAL(10, 2) NOT NULL,
    stock INT NOT NULL,
    category_id INT NULL,
    created_at DATETIME DEFAULT GETDATE(),
    last_modified DATETIME DEFAULT GETDATE(),  -- new column
    FOREIGN KEY (category_id) REFERENCES categories(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);


CREATE TABLE orders (
    id INT Identity(1,1) PRIMARY KEY,
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
	status VARCHAR(50) CHECK(
    status IN('Pending', 'Processing', 'Shipped', 'Delivered', 'Cancelled') )NOT NULL,
    status_date DATE NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    PRIMARY KEY (order_id, status_date)
);

 CREATE TABLE refunds (
    refund_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    refund_amount DECIMAL(10, 2) NOT NULL,
    refund_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);
 
CREATE TABLE order_fulfillment (
    fulfillment_id INT Identity(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    fulfillment_date DATETIME DEFAULT GETDATE(),
    status VARCHAR(50) CHECK(
    status IN('Pending', 'Processing', 'Shipped', 'Delivered', 'Cancelled')) DEFAULT 'Pending',
    tracking_number VARCHAR(100),
    carrier_name VARCHAR(100),
    fulfillment_notes TEXT,
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);
 
 CREATE TABLE payments (
    payment_id INT Identity(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    payment_date DATETIME DEFAULT GETDATE(),
    payment_amount DECIMAL(10, 2) NOT NULL,
    payment_method VARCHAR(50) CHECK( payment_method IN ('Credit Card', 'Debit Card', 'PayPal', 'Bank Transfer')) NOT NULL,
    payment_status VARCHAR(50) CHECK( payment_status IN ('Completed', 'Pending', 'Failed')) DEFAULT 'Pending',
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);


CREATE TABLE inventory (
    inventory_id INT  Identity(1,1) PRIMARY KEY,
    product_id INT,
    stock_quantity INT,
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

CREATE TABLE purchase_orders (
    order_id INT Identity(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    order_date DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);


CREATE TABLE product_audit (
    audit_id INT Identity(1,1) PRIMARY KEY,
    product_id INT,
    action_type VARCHAR(50) CHECK( action_type IN ('INSERT', 'UPDATE', 'DELETE') ),
    old_stock_quantity INT,
    new_stock_quantity INT,
    action_timestamp DATETIME DEFAULT GETDATE()
);
 
CREATE TABLE discount_codes (
    code VARCHAR(50) PRIMARY KEY,
    discount_percentage DECIMAL(5, 2) NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL
);
 
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

CREATE TABLE price_changes (
    change_id INT Identity(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    old_price DECIMAL(10, 2) NOT NULL,
    new_price DECIMAL(10, 2) NOT NULL,
    change_date DATETIME NOT NULL,
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);
 
CREATE TABLE order_items (
    id INT Identity(1,1) PRIMARY KEY,
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

CREATE TABLE reviews (
    id INT Identity(1,1) PRIMARY KEY,
    product_id INT NOT NULL,
    user_id INT NOT NULL,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    review_text TEXT,
    created_at DateTime DEFAULT GetDate(),
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    FOREIGN KEY (user_id) REFERENCES users(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
); 

CREATE TABLE cart (
    cart_id INT Identity(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    created_at DateTime DEFAULT GetDate(),
    FOREIGN KEY (customer_id) REFERENCES users(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

CREATE TABLE cart_items (
    cart_item_id INT Identity(1,1) PRIMARY KEY,
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

CREATE TABLE shipping_methods (
    shipping_method_id INT Identity(1,1) PRIMARY KEY,
    method_name VARCHAR(100) NOT NULL,
    description TEXT,
    cost DECIMAL(10, 2) NOT NULL
);

CREATE TABLE shipments (
    shipment_id INT Identity(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    shipping_method_id INT NOT NULL,
    shipment_date DateTime DEFAULT GetDate(),
    tracking_number VARCHAR(100),
    status varchar(50) CHECK(status IN ('Pending', 'Shipped', 'In Transit', 'Delivered', 'Failed')) DEFAULT 'Pending',
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    FOREIGN KEY (shipping_method_id) REFERENCES shipping_methods(shipping_method_id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

CREATE TABLE shipment_items (
    shipment_item_id INT IDENTITY(1,1) PRIMARY KEY,
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

CREATE TABLE shipping_addresses (
    shipping_address_id INT Identity(1,1) PRIMARY KEY,
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

CREATE TABLE subscriptions (
    subscription_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    subscription_plan VARCHAR(50) NOT NULL,
    start_date DATE NOT NULL,
    end_date DATE,
    status VARCHAR(50) CHECK (status IN ('Active', 'Expired', 'Cancelled')) DEFAULT 'Active',
    FOREIGN KEY (user_id) REFERENCES users(id) ON UPDATE CASCADE ON DELETE CASCADE
);


CREATE TABLE loyalty_redemptions (
    redemption_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    points_redeemed INT NOT NULL,
    redemption_date DATETIME NOT NULL,
    status varchar(50) CHECK(status IN ('Pending', 'Completed', 'Failed')) DEFAULT 'Pending',
    FOREIGN KEY (user_id) REFERENCES users(id) ON UPDATE CASCADE ON DELETE CASCADE
);                                   -- ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
 

CREATE TABLE billing_adjustments (
    adjustment_id INT IDENTITY(1,1) PRIMARY KEY,
    user_id INT NOT NULL,
    adjustment_amount DECIMAL(10, 2) NOT NULL,
    adjustment_date DATETIME NOT NULL,
    reason VARCHAR(255),
    FOREIGN KEY (user_id) REFERENCES users(id) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE returns (
    return_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    return_reason VARCHAR(255) NOT NULL,
    return_date DATETIME DEFAULT GETDATE(),
    status VARCHAR(50) NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES products(id)
        ON UPDATE CASCADE
        ON DELETE CASCADE
);

CREATE TABLE archived_orders (
    order_id INT IDENTITY(1,1) PRIMARY KEY,
    order_date DATETIME NOT NULL,
    customer_id INT NOT NULL,
    total_amount DECIMAL(10,2) NOT NULL,
    status NVARCHAR(50)
);

CREATE TABLE closed_dates (
    close_id INT IDENTITY(1,1) PRIMARY KEY,
    closed_date DATETIME NOT NULL,
    events NVARCHAR(255)
);


CREATE INDEX idx_username ON users(username);
CREATE INDEX idx_product_name ON products(name);
CREATE INDEX idx_order_date ON orders(order_date);
CREATE INDEX idx_order_item ON order_items(order_id, item_id);



 

 

 

 

-- Create shipping_addresses table

 
-- Create subscriptions table
 

 
 
-- Create table loyalty_redemption

