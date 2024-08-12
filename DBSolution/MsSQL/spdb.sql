-- 1. Creating a Stored Procedure for User Registration

CREATE PROCEDURE RegisterUser
    @p_username NVARCHAR(50),
    @p_password NVARCHAR(255),
    @p_email NVARCHAR(100),
    @p_address NVARCHAR(255)
AS
BEGIN
    INSERT INTO users (username, password, email, address)
    VALUES (@p_username, @p_password, @p_email, @p_address);
END


 -- 2. Creating a Stored Procedure for User Login

CREATE PROCEDURE LoginUser
    @p_username VARCHAR(50),
    @p_password VARCHAR(255)
AS
BEGIN
    SELECT id, username, email
    FROM users
    WHERE username = p_username AND password = p_password;
END



-- 3. Creating a Stored Procedure for Updating User Information

DROP PROCEDURE IF EXISTS UpdateUserInfo;

CREATE PROCEDURE UpdateUserInfo
    @p_user_id INT,
    @p_email VARCHAR(100),
    @p_address VARCHAR(255)
AS
BEGIN
    UPDATE users
    SET email = @p_email, address = @p_address
    WHERE id = @p_user_id;
END



EXEC UpdateUserInfo @p_user_id = 1, @p_email = 'ajinkya@gmail.com', @p_address = 'Sahakar Nagar 1';


select * from users;



-- 4. Creating a Stored Procedure for Retrieving Order Details

DROP PROCEDURE IF EXISTS GetOrderDetails;


CREATE PROCEDURE GetOrderDetails
    @p_order_id INT
AS
BEGIN
    SELECT o.id AS order_id, o.order_date, o.shipping_address, o.total_amount,
           oi.item_id, p.name AS product_name, oi.quantity, p.price
    FROM orders o
    JOIN order_items oi ON o.id = oi.order_id
    JOIN products p ON oi.item_id = p.id
    WHERE o.id = @p_order_id;
END 

EXEC GetOrderDetails @p_order_id = 2

