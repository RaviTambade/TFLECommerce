
-- get the cart item
select ci.cart_item_id as cart_item_id,
p.name as productname,
ci.itemimage as itemimage,
ci.quantity as stock ,
p.price as price ,
p.id as productid ,
c.customer_id as customerid
from cart c
join cart_items ci on c.cart_id=ci.cart_id
join categoryproduct p on p.id=ci.product_id
where c.customer_id=2; 


-- get cart id and customer id for that particular product
select ci.cart_id, c.customer_id from cart_items ci
join cart c on c.cart_id=ci.cart_id
where ci.product_id=3;

-- get product quntity and price for a particular cart user
select ci.quantity,p.price,c.customer_id,(p.price*ci.quantity) as totalprice 
from cart c
join cart_items ci on ci.cart_id=c.cart_id
join products p on p.id=ci.product_id
where c.customer_id=2;  


-- get cart item count
select count(ci.cart_item_id) from cart_items ci
join cart c on ci.cart_id=c.cart_id
group by c.customer_id
having c.customer_id=2;

-- get total amount of order of a customer
select sum(p.price*ci.quantity) as totalprice 
from cart c
join cart_items ci on ci.cart_id=c.cart_id
join products p on p.id=ci.product_id
group by customer_id
having c.customer_id=2;

-- get cart items by order id
select oi.id, cp.name, cp.image,cp.Price,oi.quantity
from order_items oi
join categoryproduct cp on cp.id=oi.item_id
where oi.order_id=10;