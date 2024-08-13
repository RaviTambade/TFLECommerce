------ Products API -------

-- 1. Retrieve All Products                          GET/api/products
-- 2. Retrieve Products in a Specific Category       GET/api/products/category/{categoryId}
-- 3. Retrieve Products and Their Categories         GET/api/products/categories
-- 4. Find Products with Low Stock         GET/api/products/low-stock?threshold={threshold}
-- 5. Retrieve Product Reviews             GET/api/products/{productId}/reviews
-- 6. Find Most Expensive Products in Each Category        GET/api/products/most-expensive-per-category
-- 7. Add a new Product                     POST/api/products
-- 8. Update Stock for a Product            PUT/api/products/{productId}/stock   
-- 9. List Top 3 Products by Total Sales    GET/api/products/top-sales
-- 10. Count Products in Each Category      GET/api/products/count-by-category
-- 11. Find Products That Have Never Been Ordered     GET/api/products/never-ordered
-- 12. Retrieve Products with More than a Specified Stock      GET/api/orders/date-range?startDate={startDate}&endDate={endDate}

------ Orders API ------

-- 1. Retrieve Users with More Than One Order        GET/api/users/multiple-orders
-- 2. Delete an Order                                DELETE/api/orders/{orderId}
-- 3. Calculate Total Sales for a Given Month        GET/api/sales/total?month={month}&year={year}
-- 4. Retrieve a User's Order History                GET/api/users/{userId}/orders
-- 5. Retrieve Orders Placed Within a Specific Date Range       GET/api/orders/date-range
-- 6. Apply a Discount Code                      POST/api/orders/{orderId}/apply-discount
		-- First, check if the discount code is valid
		-- Then apply the discount to an order
		-- Assuming the discount percentage was retrieved and is valid
-- 7. Retrieve Monthly Sales Report        GET/api/sales/report/monthly?month={month}&year={year}
-- 8. Retrieve Orders with Their Total Amounts      GET/api/orders/total-amount
-- 9. Get Total Revenue Per Product                 GET/api/products/revenue
-- 10. Find Top Rated Products                      GET/api/products/top-rated
-- 11. Retrieve Latest Orders                       GET/api/orders/latest?limit={limit}
-- 12. Find Orders with Products Above a Certain Price     GET/api/orders/above-price
-- 13. Retrieve Order Details Including User Information   GET/api/orders/{orderId}/details
-- 14. Get All Orders with Their Items and Prices          GET/api/orders/items-prices
-- 15. Retrieve Orders with Discounts Applied              GET/api/orders/with-discounts
              
------ Users API -----

-- 1. Find Users with the Most Orders          GET/api/users/most-orders?limit={limit}
-- 2. Get Total Orders and Total Amount Spent by Each User       GET/api/users/orders-total
-- 3. Retrieve Orders with Items and Their Prices Above a Certain Amount    GET/api/orders/with-total-price
-- 4. Retrieve Orders with Discounts Applied          GET/api/orders/with-discounts
-- 5. Find Average Order Amount Per User              GET/api/users/average-order-amount
-- 6. Find Customers Who Have Never Made a Purchase   GET/api/users/no-purchase

------- Reviews API--------

--1 Retrieve Products with the Most Positive Reviews       GET/api/products/top-reviewed
--2 Retrieve All Products with Reviews and Their Average Rating      GET/api/products/average-rating
-- 3. Retrieve Top 5 Most Reviewed Products         GET/api/products/top-reviewed?limit=5
-- 4. Find the Most Recent Review for Each Product  GET/api/products/{productId}/latest-review
-- 5. Get Average Rating for Each Product           GET/api/products/average-rating

-------- Shopping Cart ----------

-- 1. Add Item to Cart          POST/api/cart
--2. View Cart                  GET/cart/{user_id}
--3. Update Item Quantity       PUT/cart/{user_id}/item/{product_id}
--4. Remove Item from Cart      DELETE/cart/{user_id}/item/{product_id}
--5. Clear Cart                 DELETE/cart/{user_id}

------ Shipment API --------

--1. Create a Shipment          POST/shipments
--2. Get Shipment Details       GET/shipments/{shipment_id}
--3. Update Shipment Status     PUT/shipments/{shipment_id}/status
--4. Add Shipment Items         POST/shipments/{shipment_id}/items
--5. Get Shipping Methods       GET/shipment/shipping-methods

-- JOIN Queries API

-- 1. Inner Join/Retrieve Orders with Their Items and Product Details   GET/api/orders/items/products
-- 2. Left Join/Retrieve All Products and Their Categories              GET/api/products/categories
-- 3.Right Join/Retrieve All Categories and Products in Each Category   GET/api/categories/products
-- 4. Full Outer Join/Retrieve All Products and Reviews, Even If Some Products Have No Reviews                         GET/api/products/reviews
-- 5. Self Join/Retrieve Products and Their Similar Products Based on Category    GET/api/products/similar-products
-- 6. Join with Aggregation/Retrieve Total Sales Per Product         GET/api/products{productID}/total-sales
-- 7. Join with Filtering/Retrieve Orders for a Specific User with Item Details        GET/api/orders/users/{userID}/item-details
-- 8. Join with Subquery/Retrieve Users Who Have Purchased Products in a Specific Category               GET/api/users/{userID}/categories{categoryID}
-- 9. Complex Join/Retrieve Orders with Product Details and Discount Information       GET/api/orders/products/{productID}/discount-Applied
-- 10. Join for Data Consistency/Retrieve Orders and Verify Product Availability       GET/api/orders/products/{productID}/stock-available
-- 11. Join to Retrieve High-Rated Products with Their Categories                      GET/api/products/{productID}/categories

ECommerce Solution:

	(Modules)
		
	Product catalog
		api's-------------------------rest api

	1. Retrieve All Products			 GET /api/products
 	2. Retrieve Products in a Specific Category	 GET /api/products/category/{categoryId}
 	3. Retrieve Products and Their Categories 	 GET /api/products/categories
 	4. Find Products with Low Stock	     GET /api/products/low-stock?threshold={threshold}
 	5. Retrieve Product Reviews			 GET /api/products/{productId}/reviews
    6. Find Most Expensive Products in Each Category         GET/api/products/most-expensive-per-category
    7. Add a new Product                   POST/api/products
    8. Update Stock for a Product          PUT/api/products/{productId}/stock
    9. List Top 3 Products by Total Sales  GET/api/products/top-sales
    10. Count Products in Each Category    GET/api/products/count-by-category
    11. Find Products That Have Never Been Ordered           GET/api/products/never-ordered
    12. Retrieve Products with More than a Specified Stock   GET/api/orders/date-range?startDate={startDate}&endDate={endDate}

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
			2. Get Product Comments	    GET /api/products/{productId}/comments	
			3. Get Comment Details		GET /api/comments/{commentId}	
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
			1. Create Invoice	    POST /api/orders/{orderId}/invoice
			2. Get Invoice Details	GET /api/invoices/{invoiceId}
			3. Process Payment	    POST /api/orders/{orderId}/payment
			4. Get Payment Status	GET /api/payments/{transactionId}	
			5. Initiate Refund	    POST /api/payments/{transactionId}/refund
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
			4. Cancel Shipment		    DELETE /api/shipments/{shipmentId}
			5. Track Shipment		    GET /api/shipments/track/{trackingNumber}
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
			3. Process Return	    PUT /api/returns/{returnId}/process
			4. Issue Refund		    POST /api/returns/{returnId}/refund
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