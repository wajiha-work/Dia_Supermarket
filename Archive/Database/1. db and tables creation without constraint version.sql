USE master;
DROP DATABASE db_dia_supermarket;

CREATE DATABASE db_dia_supermarket;
USE db_dia_supermarket;

CREATE TABLE tb_Admins
(
	admin_id INT PRIMARY KEY IDENTITY(1,1),
	first_name VARCHAR(100) /*NOT NULL*/,
	last_name VARCHAR(100),
	contact_num VARCHAR(15) /*NOT NULL*/,
	email_address VARCHAR(50) UNIQUE /*NOT NULL*/,
	admin_password VARCHAR(20) /*NOT NULL*/,
	user_address VARCHAR(50) /*NOT NULL*/,
	city VARCHAR(50) /*NOT NULL*/,
	country VARCHAR(50) /*NOT NULL*/,
	postal_code INT /*NOT NULL*/,
	gender VARCHAR(10) /*NOT NULL*/ CHECK (gender IN ('male', 'female')),
	DOB DATE /*NOT NULL*/,
	nationality VARCHAR(15) /*NOT NULL*/,
	NIC_num VARCHAR(30) /*NOT NULL*/,
	join_date DATETIME /*NOT NULL*/ DEFAULT GETDATE(),
	updated_at DATETIME /*NOT NULL*/ DEFAULT GETDATE()
);

ALTER TABLE tb_Admins
	ADD verified_user bit /*NOT NULL*/ DEFAULT 0;

CREATE TABLE tb_Users
(
	user_id INT PRIMARY KEY IDENTITY(1,1),
	first_name VARCHAR(100) /*NOT NULL*/,
	last_name VARCHAR(100) /*NOT NULL*/,
	contact_num VARCHAR(15) /*NOT NULL*/,
	email_address VARCHAR(50) UNIQUE /*NOT NULL*/,
	user_password VARCHAR(20),
	user_address VARCHAR(50) /*NOT NULL*/,
	city VARCHAR(50) /*NOT NULL*/,
	country VARCHAR(50) /*NOT NULL*/,
	postal_code INT /*NOT NULL*/,
	gender VARCHAR(10) CHECK (gender IN ('male', 'female')),
	join_date DATETIME /*NOT NULL*/ DEFAULT GETDATE(),
	updated_at DATETIME /*NOT NULL*/ DEFAULT GETDATE(),
	updated_by_admin INT FOREIGN KEY REFERENCES tb_Admins(admin_id) 
);

CREATE TABLE tb_Categories
(
	cat_id INT PRIMARY KEY IDENTITY(1,1),
	cat_name VARCHAR(100) /*NOT NULL*/ /*UNIQUE*/,
	inserted_at DATETIME /*NOT NULL*/ DEFAULT GETDATE(),
	updated_at DATETIME /*NOT NULL*/ DEFAULT GETDATE(),
	updated_by_admin INT /*NOT NULL*/ FOREIGN KEY REFERENCES tb_Admins(admin_id) 
);

ALTER TABLE tb_Categories
	ADD cat_image VARCHAR(MAX) /*NOT NULL*/; 

CREATE TABLE tb_Products
(
	product_id INT PRIMARY KEY IDENTITY(1,1),
	product_name VARCHAR(100) /*NOT NULL*/ /*UNIQUE*/,
	sku VARCHAR(100) /*NOT NULL*/ /*UNIQUE*/,
	product_image VARCHAR(MAX) /*NOT NULL*/,
	long_description VARCHAR(250),
	price DECIMAL(7,2) /*NOT NULL*/, -- (decimal total 7 numbers including 2 decimal points, limit of decimal numbers is set to 2)
	stock INT /*NOT NULL*/ DEFAULT 0,
	category_id INT /*NOT NULL*/ FOREIGN KEY REFERENCES tb_Categories(cat_id),
	inserted_at DATETIME /*NOT NULL*/ DEFAULT GETDATE(),
	updated_at DATETIME /*NOT NULL*/ DEFAULT GETDATE(),
	updated_by_admin INT /*NOT NULL*/ FOREIGN KEY REFERENCES tb_Admins(admin_id)
);

CREATE TABLE tb_Orders
(
	order_id INT PRIMARY KEY IDENTITY(100,1),
	user_id INT /*NOT NULL*/ FOREIGN KEY REFERENCES tb_Users(user_id),
	order_date DATETIME /*NOT NULL*/ DEFAULT GETDATE(),
	sub_total DECIMAL(7,2) /*NOT NULL*/,
	shipping_cost DECIMAL(6,2) /*NOT NULL*/ DEFAULT 0,
	order_status VARCHAR(20) /*NOT NULL*/ DEFAULT 'waiting' CHECK (order_status IN ('waiting', 'confirmed', 'shipped', 'delivered', 'cancelled')),
	status_date DATETIME /*NOT NULL*/ DEFAULT GETDATE(),
	remarks VARCHAR(100), -- in case of cancellation admin can store why order was cancelled
	updated_by_admin INT FOREIGN KEY REFERENCES tb_Admins(admin_id)
);

-- relation b/w order and products table M-M
CREATE TABLE tb_Orders_Summary
(
	order_detail_id INT PRIMARY KEY IDENTITY(1,1),
	order_id INT /*NOT NULL*/ FOREIGN KEY REFERENCES tb_Orders(order_id),
	product_id INT /*NOT NULL*/ FOREIGN KEY REFERENCES tb_Products(product_id),
	unit_price DECIMAL(7,2) /*NOT NULL*/ DEFAULT 0,
	quantity INT /*NOT NULL*/,
);

-- relation b/w User and Products M-M
CREATE TABLE tb_Wishlist
(
	wish_id INT PRIMARY KEY IDENTITY(1,1),
	user_id INT /*NOT NULL*/ FOREIGN KEY REFERENCES tb_Users(user_id),
	product_id INT /*NOT NULL*/ FOREIGN KEY REFERENCES tb_Products(product_id),
);

-- relation b/w User and Products M-M
CREATE TABLE tb_Products_Rating
(
	rate_id INT PRIMARY KEY IDENTITY(1,1),
	user_id INT /*NOT NULL*/ FOREIGN KEY REFERENCES tb_Users(user_id),
	product_id INT /*NOT NULL*/ FOREIGN KEY REFERENCES tb_Products(product_id),
	rating INT /*NOT NULL*/ CHECK (rating BETWEEN 1 AND 5),
	rated_at DATETIME /*NOT NULL*/ DEFAULT GETDATE(),
);