USE db_dia_supermarket;


-- admins
INSERT INTO tb_Admins (first_name, last_name, contact_num, email_address, admin_password, user_address, city, country, postal_code, gender, DOB, nationality, NIC_num, verified_user) VALUES ('Wajiha', 'Muzaffar Ali', '0334-3215576', 'wajihamuzaffarali@gmail.com', '123', 'Gulshan-e-Iqbal', 'Karachi', 'Pakistan', 75300, 'female', '15-JAN-1995', 'Pakistani', '48745-8965147-8', 1);

INSERT INTO tb_Admins (first_name, last_name, contact_num, email_address, admin_password, user_address, city, country, postal_code, gender, DOB, nationality, NIC_num, join_date,  verified_user) VALUES ('Edward', 'Elric', '0334-3215576', 'edward@gmail.com', 'edward', 'Central', 'Islamabad', 'Pakistan', 75300, 'male', '01-MAR-2000', 'Japanese', '48745-8965147-8', '03-SEP-2021', 1);

INSERT INTO tb_Admins (first_name, last_name, contact_num, email_address, admin_password, user_address, city, country, postal_code, gender, DOB, nationality, NIC_num, join_date) VALUES ('Alphonse', 'Elric', '0334-3289576', 'alphonse@gmail.com', 'alphonse', 'Western Park', 'Lahore', 'Pakistan', 75378, 'male', '15-JAN-2001', 'Japanese', '48745-894847-8', '19-JUN-2021');



-- users
INSERT INTO tb_Users (first_name, last_name, contact_num, email_address, user_password, user_address, city, country, postal_code, gender, join_date) VALUES ('Winry', 'Rockbell', '0320-2089576', 'winry@gmail.com', 'winry', 'Central', 'Islamabad', 'Pakistan', 75300, 'female', '04-SEP-2001');

INSERT INTO tb_Users (first_name, last_name, contact_num, email_address, user_password, user_address, city, country, postal_code, gender, join_date) VALUES ('Ling', 'Yao', '0320-2789576', 'ling@gmail.com', 'ling', 'Gulshan', 'Karachi', 'Pakistan', 78952, 'male', '20-JUN-2001');



-- catergories
-- catergories
INSERT INTO tb_Categories (cat_name, updated_by_admin, cat_image) VALUES ('Fruits', 1, '/Templates/UploadedFiles/cherries-1503974_640.jpg');
INSERT INTO tb_Categories (cat_name, updated_by_admin, cat_image) VALUES ('Vegetables', 1, '/Templates/UploadedFiles/image_6.jpg');
INSERT INTO tb_Categories (cat_name, updated_by_admin, cat_image) VALUES ('Dairy Products', 2, '/Templates/UploadedFiles/milk-2474993_640.jpg');
INSERT INTO tb_Categories (cat_name, updated_by_admin, cat_image) VALUES ('Breakfast items', 1, '/Templates/UploadedFiles/bread-5671124_640.jpg');
	

-- products
INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Potatoes 1KG', '97-0002-000003', 'Potato is 79% water, 17% carbohydrates (88% is starch), 2% protein, and contains negligible fat. In a 100-gram portion, raw potato provides 77 kilocalories of food energy and is a rich source of vitamin B6 and vitamin C.', 30, 5, 2, 1, '/Templates/UploadedFiles/potatoes-2795_640.jpg');

INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Tomatoes 1KG', '97-0002-000583', NULL, 35, 15, 2, 1, '/Templates/UploadedFiles/product-5.jpg');

INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Carrots 1KG', '55-0004-000016', 'Raw carrots are 88% water, 9% carbohydrates, 0.9% protein, 2.8% dietary fiber, 1% ash and 0.2% fat. Carrot dietary fiber comprises mostly cellulose. Free sugars in carrot include sucrose, glucose, and fructose.', 130, 10, 2, 1, '/Templates/UploadedFiles/carrots-673184_640.jpg');

INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Purple Cabbage 1KG', '97-0152-048583', NULL, 315, 15, 2, 1, '/Templates/UploadedFiles/product-4.jpg');

INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Capsicum 1KG', '97-0152-148583', NULL, 150, 15, 2, 1, '/Templates/UploadedFiles/product-1.jpg');

INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Red Chillies 1KG', '77-0152-148583', NULL, 30, 15, 2, 1, '/Templates/UploadedFiles/product-12.jpg');

INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Peas 1KG', '97-0352-148583', NULL, 50, 15, 2, 1, '/Templates/UploadedFiles/product-3.jpg');

INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Broccoli 250gm', '97-0152-000583', NULL, 315, 15, 2, 1, '/Templates/UploadedFiles/product-6.jpg');


INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Apple 1KG', '55-0004-000023', NULL, 250, 50, 1, 2, '/Templates/UploadedFiles/apples-805124_640.jpg');

INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Peach 1KG', '55-0004-045023', NULL, 500, 50, 1, 2, '/Templates/UploadedFiles/peach-1074997_640.jpg');

INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Banana 1dozen', '55-9404-045023', NULL, 200, 50, 1, 2, '/Templates/UploadedFiles/fruit-1218133_640.png');

INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Full Cream Milk 250ml', '52-0001-000315', 'One cup (250 mL) of 2%-fat cow milk contains 285 mg of calcium, which represents 22% to 29% of the daily recommended intake of calcium for an adult. Depending on its age, milk contains 8 grams of protein, and a number of other nutrients', 42, 15, 3, 2, '/Templates/UploadedFiles/milk-1223800_640.jpg');

INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Butter 250gm', '52-0111-000315', NULL, 200, 150, 3, 2, '/Templates/UploadedFiles/food-3179853_640.jpg');

INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Honey 500gm', '36-0002-000062', 'Honey is mainly fructose (38%) and glucose (32%), with remaining sugars including maltose, sucrose, and other complex carbohydrates. The color, aroma, and flavor of honey depend on the flowers foraged by bees.', 1250, 10, 4, 1, '/Templates/UploadedFiles/nuns-2304009_640.jpg');

INSERT INTO tb_Products ( product_name, sku, long_description, price, stock, category_id, updated_by_admin, product_image) VALUES ('Bread Large', '39-0002-000062', NULL, 150, 100, 4, 1, '/Templates/UploadedFiles/bread-2864703_640.jpg');



-- orders
INSERT INTO [dbo].[tb_Orders] ([user_id] ,[sub_total] ,[shipping_cost] ,[updated_by_admin], [order_status]) VALUES (1 ,500  ,200, NULL, 'waiting');
INSERT INTO [dbo].[tb_Orders] ([user_id] ,[sub_total] ,[shipping_cost] ,[updated_by_admin], [order_status]) VALUES (2 ,300  ,200, NULL, 'cancelled');
INSERT INTO [dbo].[tb_Orders] ([user_id] ,[sub_total] ,[shipping_cost] ,[updated_by_admin], [order_status]) VALUES (1 ,3000  ,200, NULL, 'delivered');



-- order summary
INSERT INTO [dbo].[tb_Orders_Summary] ([order_id]  ,[product_id]  ,[unit_price]  ,[quantity]) VALUES   (101 ,1 ,30 ,4);
INSERT INTO [dbo].[tb_Orders_Summary] ([order_id]  ,[product_id]  ,[unit_price]  ,[quantity]) VALUES   (101 ,2 ,30 ,2);
INSERT INTO [dbo].[tb_Orders_Summary] ([order_id]  ,[product_id]  ,[unit_price]  ,[quantity]) VALUES   (101 ,7 ,300 ,3);

INSERT INTO [dbo].[tb_Orders_Summary] ([order_id]  ,[product_id]  ,[unit_price]  ,[quantity]) VALUES   (102 ,1 ,300 ,4);

INSERT INTO [dbo].[tb_Orders_Summary] ([order_id]  ,[product_id]  ,[unit_price]  ,[quantity]) VALUES   (103 ,1 ,500 ,2);
INSERT INTO [dbo].[tb_Orders_Summary] ([order_id]  ,[product_id]  ,[unit_price]  ,[quantity]) VALUES   (101 ,2 ,900 ,8);