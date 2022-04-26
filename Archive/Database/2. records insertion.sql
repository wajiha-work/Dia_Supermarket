USE db_dia_supermarket;

-- admins

INSERT INTO tb_Admins (first_name, last_name, contact_num, email_address, admin_password, user_address, city, country, postal_code, gender, DOB, nationality, NIC_num) VALUES ('Wajiha', 'Muzaffar Ali', '0334-3215576', 'wajihamuzaffarali@gmail.com', '123', 'Gulshan-e-Iqbal', 'Karachi', 'Pakistan', 75300, 'female', '15-JAN-1995', 'Pakistani', '48745-8965147-8');

INSERT INTO tb_Admins (first_name, last_name, contact_num, email_address, admin_password, user_address, city, country, postal_code, gender, DOB, nationality, NIC_num, join_date) VALUES ('Edward', 'Elric', '0334-3215576', 'edward@gmail.com', 'edward', 'Central', 'Islamabad', 'Pakistan', 75300, 'male', '01-MAR-2000', 'Japanese', '48745-8965147-8', '03-SEP-2021');

INSERT INTO tb_Admins (first_name, last_name, contact_num, email_address, admin_password, user_address, city, country, postal_code, gender, DOB, nationality, NIC_num, join_date) VALUES ('Alphonse', 'Elric', '0334-3289576', 'alphonse@gmail.com', 'alphonse', 'Western Park', 'Lahore', 'Pakistan', 75378, 'male', '15-JAN-2001', 'Japanese', '48745-894847-8', '19-JUN-2021');


-- users
INSERT INTO tb_Users (first_name, last_name, contact_num, email_address, user_password, user_address, city, country, postal_code, gender, join_date) VALUES ('Winry', 'Rockbell', '0320-2089576', 'winry@gmail.com', 'winry', 'Central', 'Islamabad', 'Pakistan', 75300, 'female', '04-SEP-2001');

INSERT INTO tb_Users (first_name, last_name, contact_num, email_address, user_password, user_address, city, country, postal_code, gender, join_date) VALUES ('Ling', 'Yao', '0320-2789576', 'ling@gmail.com', 'ling', 'Gulshan', 'Karachi', 'Pakistan', 78952, 'male', '20-JUN-2001');

-- catergories
INSERT INTO tb_Categories (cat_name, updated_by_admin) VALUES ('Fruits', 1);
INSERT INTO tb_Categories (cat_name, updated_by_admin) VALUES ('Vegetables', 1);
INSERT INTO tb_Categories (cat_name, updated_by_admin) VALUES ('Dairy Products', 2);
INSERT INTO tb_Categories (cat_name, updated_by_admin) VALUES ('Breakfast items', 1);

	

-- products
INSERT INTO tb_Products ( product_name, sku, product_image, long_description, price, stock, category_id, updated_by_admin) VALUES ('Potatoes 1KG', '97-0002-000003', '', 'Potato is 79% water, 17% carbohydrates (88% is starch), 2% protein, and contains negligible fat. In a 100-gram portion, raw potato provides 77 kilocalories of food energy and is a rich source of vitamin B6 and vitamin C.', 30, 5, 2, 1);

INSERT INTO tb_Products ( product_name, sku, product_image, long_description, price, stock, category_id, updated_by_admin) VALUES ('Carrots 1KG', '55-0004-000016', '', 'Raw carrots are 88% water, 9% carbohydrates, 0.9% protein, 2.8% dietary fiber, 1% ash and 0.2% fat. Carrot dietary fiber comprises mostly cellulose. Free sugars in carrot include sucrose, glucose, and fructose.', 130, 10, 2, 1);


INSERT INTO tb_Products ( product_name, sku, product_image, long_description, price, stock, category_id, updated_by_admin) VALUES ('Full Cream Milk 1.5 Litre', '52-0001-000335', '', 'One cup (250 mL) of 2%-fat cow milk contains 285 mg of calcium, which represents 22% to 29% of the daily recommended intake of calcium for an adult. Depending on its age, milk contains 8 grams of protein, and a number of other nutrients', 235, 7, 3, 2);

INSERT INTO tb_Products ( product_name, sku, product_image, long_description, price, stock, category_id, updated_by_admin) VALUES ('Full Cream Milk 250ml', '52-0001-000315', '', 'One cup (250 mL) of 2%-fat cow milk contains 285 mg of calcium, which represents 22% to 29% of the daily recommended intake of calcium for an adult. Depending on its age, milk contains 8 grams of protein, and a number of other nutrients', 42, 15, 3, 2);

INSERT INTO tb_Products ( product_name, sku, product_image, long_description, price, stock, category_id, updated_by_admin) VALUES ('Honey 500gm', '36-0002-000062', '', 'Honey is mainly fructose (38%) and glucose (32%), with remaining sugars including maltose, sucrose, and other complex carbohydrates. The color, aroma, and flavor of honey depend on the flowers foraged by bees.', 1250, 10, 4, 1);
