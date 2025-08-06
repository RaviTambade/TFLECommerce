-- add to cart
DELIMITER //
CREATE PROCEDURE AddToCart
(
IN uid INT,
IN pid INT,
IN stock INT,
IN img Varchar(255)
)
begin
	DECLARE cartid int;
    DECLARE item_exists int;
    DECLARE userId int;
    declare existing_cart_id int;
    

     -- Check if the product is already in the user's cart
    SELECT COUNT(*) INTO item_exists
    FROM cart_items ci
    JOIN cart c ON ci.cart_id = c.cart_id
    WHERE c.customer_id = uid AND ci.product_id = pid;

    
     -- Check if user already has a cart
	  SELECT cart_id INTO existing_cart_id
	  FROM cart
	  WHERE customer_id = uid
	  LIMIT 1;


    if item_exists > 0 then
		SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'PLEASE CHECK!! ITEM IS ALREADY IN CART ';
	
    else
	     if existing_cart_id is not null then
					set cartid=existing_cart_id;
					insert into cart_items(cart_id,product_id,quantity,Itemimage)values(cartid,pid,stock,img);
		else
			-- create new cart for user
			INSERT INTO cart(Customer_id) values(uid);
		
			-- get new cart id
			 set cartid=last_insert_id();
			 
			 -- insert items into cart_items 
				insert into cart_items(cart_id,product_id,quantity,Itemimage)values(cartid,pid,stock,img);
			
		end if;
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
DECLARE countofcart int;
-- get user id of that product that we have to delete
SET uid=( 
			select  c.customer_id from cart_items ci
			join cart c on c.cart_id=ci.cart_id
			where ci.product_id=pid
		);

-- get cartid of the product that we have to delete also check its user
set cartid=(
				select ci.cart_id 
				from cart_items ci
				join cart c on c.cart_id=ci.cart_id
				where ci.product_id=pid AND customer_id=uid
			);
            
-- delete that product from cart_item
delete from cart_items where product_id=pid;

-- check the  remaining product count of the user cart 
select count(*) into countofcart 
from cart_items where cart_id=cartid;

-- if remaining products are zero the delete cart
if countofcart = 0 then 
	delete from cart where cart_id=cartid;
end if;
END//
DELIMITER ;

CALL RemoveFromCart(11);



-- place order
DELIMITER //
CREATE PROCEDURE Place_Order
(
 IN userid INT,
 IN odate DATE,
 IN shipdate date,
 IN shipId int
)
BEGIN
		DECLARE total_price double;
		declare cartitemcount int;
        declare cartid int;
		declare count int;
        Declare orderid int;
		set count=0;
		set total_price=0;
        
        -- get user cart
         SELECT cart_id INTO cartid
		FROM cart
		WHERE customer_id = userid
		LIMIT 1;

        -- get cart item count
		SET cartitemcount=(select count(ci.cart_item_id) 
						from cart_items ci
						join cart c on ci.cart_id=c.cart_id
						group by c.customer_id
						having c.customer_id=userid);
						

		-- get totalamount of cart items
		select sum(p.price*ci.quantity) into total_price
		from cart c
		join cart_items ci on ci.cart_id=c.cart_id
		join products p on p.id=ci.product_id
		group by customer_id
		having c.customer_id=userid;


		-- insert order into table
		insert into orders(customer_id,order_date,total_amount,shipping_date,status,shipping_address_id) values (userid,odate, total_price,shipdate,"processing",shipid);
        
        -- get last inserted order
		set orderid=last_insert_id();
        
     -- insert cart_item into order_items table
			INSERT INTO order_items (order_id, item_id, quantity) select orderid, product_id,quantity from cart_items where cart_id=cartid;

        

		-- Clear cart items
		DELETE FROM cart_items WHERE cart_id = cartid;

        -- Delete cart
		DELETE FROM cart WHERE cart_id = cartid;
END //
DELIMITER ;

CALL Place_Order(2, '2025-08-06', '2025-09-07', 3);


drop procedure AddToCart;
drop procedure RemoveFromCart;
drop procedure Place_Order;
