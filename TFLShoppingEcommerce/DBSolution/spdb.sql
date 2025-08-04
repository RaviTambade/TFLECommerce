-- add to cart
DELIMITER //
CREATE PROCEDURE AddToCart
(
IN uid INT,
IN pid INT,
IN stock INT,
IN img Varchar(255))
begin
	DECLARE cartid int;
    DECLARE item int;
    DECLARE userId int;
    
    SET item=(select product_id from cart_items where product_id=pid limit 1);
    SET userId=(select c.customer_id from cart c join cart_items ci on ci.cart_id=c.cart_id where customer_id=uid limit 1);
    if item is not null and userId is not null then
		SIGNAL SQLSTATE '45000'
       SET MESSAGE_TEXT = 'PLEASE CHECK!! ITEM IS ALREADY IN CART ';
	else
    INSERT INTO cart(Customer_id) values(uid);
			set cartid=last_insert_id();
			insert into cart_items(cart_id,product_id,quantity,Itemimage)values(cartid,pid,stock,img);
	
	end if;

end //
DELIMITER ;

CALL AddToCart(2,13,1,"/images/Fashion/tshirt.jpeg");

-- remove from cart
DELIMITER //
CREATE PROCEDURE RemoveFromCart
(
in pid int
)
BEGIN
DECLARE cartid int;
DECLARE uid int;
SET uid=( 
			select  c.customer_id from cart_items ci
			join cart c on c.cart_id=ci.cart_id
			where ci.product_id=pid
		);

set cartid=(
				select ci.cart_id 
				from cart_items ci
				join cart c on c.cart_id=ci.cart_id
				where ci.product_id=pid AND customer_id=uid
			);
            

delete from cart_items where product_id=pid;
delete from cart where cart_id=cartid;
END//
DELIMITER ;

CALL RemoveFromCart(11);





drop procedure AddToCart;
drop procedure RemoveFromCart;
