------ Products API -------

-- 1. Retrieve All Products
-- Endpoint: /api/products
-- Method: GET


-- 2. Retrieve Products in a Specific Category
-- Endpoint: /api/products/category/{categoryId}
-- Method: GET

-- 3. Retrieve Products and Their Categories 
-- Endpoint: /api/products/categories
-- Method: GET


-- 4. Find Products with Low Stock
-- Endpoint: /api/products/low-stock?threshold={threshold}
-- Method: GET


-- 5. Retrieve Product Reviews
-- Endpoint: /api/products/{productId}/reviews
-- Method: GET


-- 6. Find Most Expensive Products in Each Category
-- Endpoint: /api/products/most-expensive-per-category
-- Method: GET


-- 7. Add a new Product  POST /api/products
-- Method: POST


-- 8. Update Stock for a Product
-- Endpoint: /api/products/{productId}/stock
-- Method: PUT


-- 9. List Top 3 Products by Total Sales
-- Endpoint: /api/products/top-sales
-- Method: GET


-- 10. Count Products in Each Category
-- Endpoint: /api/products/count-by-category
-- Method: GET


-- 11. Find Products That Have Never Been Ordered
-- Endpoint: /api/products/never-ordered
-- Method: GET


-- 12. Retrieve Products with More than a Specified Stock
-- Endpoint: /api/orders/date-range?startDate={startDate}&endDate={endDate}
-- Method: GET


------ Orders API ------

-- 1. Retrieve Users with More Than One Order
-- Endpoint: /api/users/multiple-orders
-- Method: GET

-- 2. Delete an Order
-- Endpoint: /api/orders/{orderId}/
-- Method: DELETE


-- 3. Calculate Total Sales for a Given Month
-- Endpoint: /api/sales/total?month={month}&year={year}
-- Method: GET


-- 4. Retrieve a User's Order History
-- Endpoint: /api/users/{userId}/orders
-- Method: GET

-- 5. Retrieve Orders Placed Within a Specific Date Range
-- Endpoint: /api/orders/date-range
-- Method: GET


-- 6. Apply a Discount Code
		-- First, check if the discount code is valid
		-- Then apply the discount to an order
		-- Assuming the discount percentage was retrieved and is valid
-- Endpoint: /api/orders/{orderId}/apply-discount
-- Method: POST


-- 7. Retrieve Monthly Sales Report
-- Endpoint: /api/sales/report/monthly?month={month}&year={year}
-- Method: GET


-- 8. Retrieve Orders with Their Total Amounts
-- Endpoint: /api/orders/total-amount
-- Method: GET


-- 9. Get Total Revenue Per Product 
-- Endpoint: /api/products/revenue
-- Method: GET


-- 10. Find Top Rated Products
-- Endpoint: /api/products/top-rated
-- Method: GET


-- 11. Retrieve Latest Orders
-- Endpoint: /api/orders/latest?limit={limit}
-- Method: GET


-- 12. Find Orders with Products Above a Certain Price 
-- Endpoint: /api/orders/above-price
-- Method: GET


-- 13. Retrieve Order Details Including User Information
-- Endpoint: /api/orders/{orderId}/details
-- Method: GET


-- 14. Get All Orders with Their Items and Prices
-- Endpoint:/api/orders/items-prices
-- Method: GET

-- 15. Retrieve Orders with Discounts Applied
-- Endpoint:/api/orders/with-discounts
-- Method: GET


               
------ Users API -----

-- 1. Find Users with the Most Orders
-- Endpoint: /api/users/most-orders?limit={limit}
-- Method: GET


-- 2. Get Total Orders and Total Amount Spent by Each User
-- Endpoint: /api/users/orders-total
-- Method: GET


-- 3. Retrieve Orders with Items and Their Prices Above a Certain Amount
-- Endpoint: /api/orders/with-total-price
-- Method: GET



-- 4. Retrieve Orders with Discounts Applied
-- Endpoint: /api/orders/with-discounts
-- Method: GET


-- 5. Find Average Order Amount Per User
-- Endpoint: /api/users/average-order-amount
-- Method: GET


-- 6. Find Customers Who Have Never Made a Purchase
-- Endpoint: /api/users/no-purchase
-- Method: GET

------- Reviews API--------

--1 Retrieve Products with the Most Positive Reviews
-- Endpoint: /api/products/top-reviewed
-- Method: GET

--2 Retrieve All Products with Reviews and Their Average Rating
-- Endpoint: /api/products/average-rating
-- Method: GET


-- 3. Retrieve Top 5 Most Reviewed Products
-- Endpoint: /api/products/top-reviewed?limit=5
-- Method: GET


-- 4. Find the Most Recent Review for Each Product
-- Endpoint: /api/products/{productId}/latest-review
-- Method: GET


-- 5. Get Average Rating for Each Product
-- Endpoint: /api/products/average-rating
-- Method: GET



-------- Shopping Cart ----------

-- 1. Add Item to Cart
-- Endpoint: /api/cart
-- Method: POST


--2. View Cart
-- Endpoint: /cart/{user_id}
-- Method: GET


--3. Update Item Quantity
-- Endpoint: /cart/{user_id}/item/{product_id}
-- Method: PUT


--4. Remove Item from Cart
-- Endpoint: /cart/{user_id}/item/{product_id}
-- Method: DELETE


--5. Clear Cart
-- Endpoint: /cart/{user_id}
-- Method: DELETE


------ Shipment API --------

--1. Create a Shipment
-- Endpoint: /shipments/
-- Method: POST


--2. Get Shipment Details
-- Endpoint: /shipments/{shipment_id}
-- Method: GET

--3. Update Shipment Status
-- Endpoint: /shipments/{shipment_id}/status
-- Method: PUT

--4. Add Shipment Items
-- Endpoint: /shipments/{shipment_id}/items
-- Method: POST

--5. Get Shipping Methods
-- Endpoint: shipment/shipping-methods
-- Method: GET


-- JOIN Queries API

-- 1. Inner Join: Retrieve Orders with Their Items and Product Details
-- Endpoint: /api/orders/items/products
-- Method: GET


-- 2. Left Join: Retrieve All Products and Their Categories
-- Endpoint: /api/products/categories
-- Method: GET


-- 3.Right Join: Retrieve All Categories and Products in Each Category
-- Endpoint: /api/categories/products
-- Method: GET

-- 4. Full Outer Join: Retrieve All Products and Reviews, Even If Some Products Have No Reviews
-- Endpoint: /api/products/reviews
-- Method: GET

-- 5. Self Join: Retrieve Products and Their Similar Products Based on Category
-- Endpoint: /api/products/similar-products
-- Method: GET

-- 6. Join with Aggregation: Retrieve Total Sales Per Product
-- Endpoint: /api/products{productID}/total-sales
-- Method: GET

-- 7. Join with Filtering: Retrieve Orders for a Specific User with Item Details
-- Endpoint: /api/orders/users/{userID}/item-details
-- Method: GET

-- 8. Join with Subquery: Retrieve Users Who Have Purchased Products in a Specific Category
-- Endpoint: /api/users/{userID}/categories{categoryID}
-- Method: GET

-- 9. Complex Join: Retrieve Orders with Product Details and Discount Information
-- Endpoint: /api/orders/products/{productID}/discount-Applied
-- Method: GET

-- 10. Join for Data Consistency: Retrieve Orders and Verify Product Availability
-- Endpoint: /api/orders/products/{productID}/stock-available
-- Method: GET

-- 11. Join to Retrieve High-Rated Products with Their Categories
-- Endpoint: /api/products/{productID}/categories
-- Method: GET





ECommerce Solution:

	(Modules)
		
		
	Product catalog
		api's-------------------------rest api

	1. Retrieve All Products			 GET /api/products
 	2. Retrieve Products in a Specific Category	 GET /api/products/category/{categoryId}
 	3. Retrieve Products and Their Categories 	 GET /api/products/categories
 	4. Find Products with Low Stock			 GET /api/products/low-stock?threshold={threshold}
 	5. Retrieve Product Reviews			 GET /api/products/{productId}/reviews
 

-- 6. Find Most Expensive Products in Each Category
-- Endpoint: /api/products/most-expensive-per-category
-- Method: GET


-- 7. Add a new Product
-- Endpoint: /api/products
-- Method: POST


-- 8. Update Stock for a Product
-- Endpoint: /api/products/{productId}/stock
-- Method: PUT


-- 9. List Top 3 Products by Total Sales
-- Endpoint: /api/products/top-sales
-- Method: GET


-- 10. Count Products in Each Category
-- Endpoint: /api/products/count-by-category
-- Method: GET


-- 11. Find Products That Have Never Been Ordered
-- Endpoint: /api/products/never-ordered
-- Method: GET


-- 12. Retrieve Products with More than a Specified Stock
-- Endpoint: /api/orders/date-range?startDate={startDate}&endDate={endDate}
-- Method: GET
		controllers (MVC framework)
		services
		Repositories
			ADO.NET, Entity Framework, Dapper	
		RDBMS elements
			tables, queries, txns, Stored procedures, Triggers--------done

	Shopping Cart
		api's
			Add Item to Cart
				POST /api/cart
			View Cart
				GET /api/cart
			Update Item Quantity	
				PUT /api/cart/items/{itemId}
			Remove Item from Cart
				DELETE /api/cart/items/{itemId}
			Clear Cart
				DELETE /api/cart


		C#
			controllers
			services
			Repositories
			ADO.NET, Entity Framework, Dapper	
	
			DDL,DML,SQL

			tables, queries, txns, Stored procedures, Triggers-----------------done


	OrderProcessing
		api's
		

		controllers
		services
		Repositories
		ADO.NET, Entity Framework, Dapper	
		tables, queries, txns, Stored procedures, Triggers----------------done
		
	reviews, comments
		api's

			
			

			1. Add a Comment			POST /api/products/{productId}/comments
			2. Get Product Comments			GET /api/products/{productId}/comments	
			3. Get Comment Details			GET /api/comments/{commentId}	
			4. Update a Comment			PUT /api/comments/{commentId}	
			5. Delete a Comment			DELETE /api/comments/{commentId}		
			6. Get Comment Count for Product	GET /api/products/{productId}/comments/count
		controllers
		services
		Repositories
		ADO.NET, Entity Framework, Dapper	
		tables, queries, txns, Stored procedures, Triggers------------------done

	Customer Relationship Management
		api's
			POST /api/crm/customers
			GET  /api/crm/customers/{customerId}
			POST /api/crm/orders
			GET  /api/crm/orders/{orderId}
			POST /api/crm/campaigns
			GET /api/crm/campaigns/{campaignId}
		controllers
		services
		Repositories
		ADO.NET, Entity Framework, Dapper	
		tables, queries, txns, Stored procedures, Triggers------------------done


	Billing and Payment processing

		api's
			1. Create Invoice	POST /api/orders/{orderId}/invoice
			2. Get Invoice Details	GET /api/invoices/{invoiceId}
			3. Process Payment	POST /api/orders/{orderId}/payment
			4. Get Payment Status	GET /api/payments/{transactionId}	
			5. Initiate Refund	POST /api/payments/{transactionId}/refund
			6. Get Refund Status	GET /api/refunds/{refundId}
			7. Add Payment Method	POST /api/customers/{customerId}/payment-methods
			8. Get Payment Methods 	GET /api/customers/{customerId}/payment-methods
		controllers
		services
		Repositories
		ADO.NET, Entity Framework, Dapper	
		tables, queries, txns, Stored procedures, Triggers-------------------done

	Shipment
		api's
			1. Create Shipment  		POST /api/orders/{orderId}/shipments
			2. Get Shipment Details 	GET /api/shipments/{shipmentId}
			3. Update Shipment Status 	PUT /api/shipments/{shipmentId}/status
			4. Cancel Shipment		DELETE /api/shipments/{shipmentId}
			5. Track Shipment		GET /api/shipments/track/{trackingNumber}
			6. Generate Shipping Label	POST /api/shipments/{shipmentId}/label   
			7. Get Shipping Policies	GET /api/shipping/policies	  	
			
		
		controllers
		services
		Repositories
		ADO.NET, Entity Framework, Dapper	
		tables, queries, txns, Stored procedures, Triggers-------------------done

	Refund and Return Policy
		api's
			1. Initiate a Return 	POST /api/orders/{orderId}/return--------- class controller------service class-------repository
			2. Get Return Status    GET /api/returns/{returnId}
			3. Process Return	PUT /api/returns/{returnId}/process
			4. Issue Refund		POST /api/returns/{returnId}/refund
			5. Get Refund Status	GET /api/refunds/{refundId}
			6. Get Return Policies  GET /api/returns/policy
	

		controllers
		services
		Repositories
		ADO.NET, Entity Framework, Dapper	
		tables, queries, txns, Stored procedures, Triggers--------------------done






		INSERT INTO	  --------SQL-------------------database
		UPDATE emloye set   ------SQL
		DELETE from --------------SQL