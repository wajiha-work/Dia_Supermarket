USE db_dia_supermarket;

-- how much a product is sold // added in 1011 and 1015
SELECT SUM(OS.quantity) 
	FROM tb_Orders_Summary OS inner join tb_Orders O
		ON o.order_id = os.order_id
	WHERE OS.product_id = 1011 and o.order_status = 'delivered';

-- avg product rating
SELECT AVG(rating) FROM tb_Products_Rating WHERE product_id = 1011;

-- how much total ratings of a product
SELECT COUNT(rating) FROM tb_Products_Rating WHERE product_id = 1011;

-- PENDING orders
SELECT COUNT(order_id) FROM tb_Orders WHERE order_status IN ('waiting', 'confirmed');

-- Total Earnings
SELECT SUM(sub_total) FROM tb_Orders WHERE order_status = 'delivered';


-- best selling products
SELECT P.product_name, P.product_image, P.price, P.stock, C.cat_name, SUM(OS.quantity) AS 'SALES'
	FROM tb_Categories C INNER JOIN tb_Products P 
		ON P.category_id = C.cat_id
	INNER JOIN tb_Orders_Summary OS 
		ON P.product_id = OS.product_id
	GROUP BY P.product_name, P.product_image, P.price, P.stock, C.cat_name
	ORDER BY SUM(OS.quantity) DESC;	

	