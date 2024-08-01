
<<<<<<< HEAD
=======
DROP database IF EXISTS tflecommerce;
create database tflecommerce;

use tflecommerce;


>>>>>>> 7127b43fe2ce6efd6ee18258370ed9d46e450832
CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    address VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE categories (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    description TEXT
);

CREATE TABLE products (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    description TEXT,
    price DECIMAL(10, 2) NOT NULL,
    stock INT NOT NULL,
    category_id INT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (category_id) REFERENCES categories(id)
);

CREATE TABLE product_audit (
    audit_id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT,
    action_type ENUM('INSERT', 'UPDATE', 'DELETE'),
    old_stock_quantity INT,
    new_stock_quantity INT,
    action_timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (product_id) REFERENCES inventory(product_id)
);

CREATE TABLE inventory (
    product_id INT PRIMARY KEY,
    stock_quantity INT
);


CREATE TABLE orders (
    id INT AUTO_INCREMENT PRIMARY KEY,
    customer_id INT NOT NULL,
    order_date DATE NOT NULL,
    shipping_address VARCHAR(255) NOT NULL,
    total_amount DECIMAL(10, 2),
    shipping_date DATE,  -- Add the shipping_date column
    status VARCHAR(50),  -- Add the status column
    FOREIGN KEY (customer_id) REFERENCES users(id)
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
    FOREIGN KEY (order_id) REFERENCES orders(id),
    FOREIGN KEY (discount_code) REFERENCES discount_codes(code)
);

CREATE TABLE price_changes (
    change_id INT AUTO_INCREMENT PRIMARY KEY,  -- Unique identifier for each price change record
    product_id INT NOT NULL,                   -- ID of the product whose price has changed
    old_price DECIMAL(10, 2) NOT NULL,         -- Old price of the product before the update
    new_price DECIMAL(10, 2) NOT NULL,         -- New price of the product after the update
    change_date DATETIME NOT NULL,             -- Date and time when the price change occurred
    FOREIGN KEY (product_id) REFERENCES products(id)  -- Foreign key to reference the product in the products table
);


CREATE TABLE order_items (
    id INT AUTO_INCREMENT PRIMARY KEY,
    order_id INT NOT NULL,
    item_id INT NOT NULL,
    quantity INT NOT NULL,
    FOREIGN KEY (order_id) REFERENCES orders(id),
    FOREIGN KEY (item_id) REFERENCES products(id)
);

CREATE TABLE reviews (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    user_id INT NOT NULL,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    review_text TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (product_id) REFERENCES products(id),
    FOREIGN KEY (user_id) REFERENCES users(id)
);



-- Index on username for faster user lookups
CREATE INDEX idx_username ON users(username);

-- Index on product name for faster product searches
CREATE INDEX idx_product_name ON products(name);

-- Index on order_date for quicker order history lookups
CREATE INDEX idx_order_date ON orders(order_date);

-- Composite index for faster order items retrieval
CREATE INDEX idx_order_item ON order_items(order_id, item_id);

