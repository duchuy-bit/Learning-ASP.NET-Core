IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DH_Shop')
BEGIN
  CREATE DATABASE DH_Shop;
END;
GO


USE  DH_Shop
GO


CREATE TABLE category(
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY ,
	title NVARCHAR(100),
	content NVARCHAR(500),
	createAt DATETIME,
	updateat DATETIME
)
GO

CREATE TABLE product(
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY ,
	title NVARCHAR(100),
	content NVARCHAR(500),
	img NVARCHAR(200),
	price INT,
	discount FLOAT,
	createAt DATETIME,
	updateAt DATETIME,
	categoryId int,
	FOREIGN KEY (categoryId) REFERENCES category(id)
)
GO

CREATE TABLE customer(
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY ,
	firstName NVARCHAR(100),
	lastName NVARCHAR(100),
	phone NVARCHAR(15),
	email NVARCHAR(50),
	password NVARCHAR(200),
	img NVARCHAR(200),
	registeredAt DATETIME,
	dateOfBirth DATETIME,
	isActive BIT,
	role INT  --  0: user, 1: admin
)
GO

CREATE TABLE  cart (
	id INT  NOT NULL IDENTITY(1,1) PRIMARY KEY ,
	customerId INT,
	createAt DATETIME,
	productId INT,
	quantity INT,
	FOREIGN KEY (customerId) REFERENCES customer(id),
	FOREIGN KEY (productId) REFERENCES product(id)
)
GO

CREATE TABLE  payment (
	id INT  NOT NULL IDENTITY(1,1) PRIMARY KEY ,
	customerId INT,
	firstName NVARCHAR(100),
	lastName NVARCHAR(100),
	phone NVARCHAR(15),
	email NVARCHAR(50),
	createAt DATETIME,
	discount FLOAT,
	total FLOAT,
	FOREIGN KEY (customerId) REFERENCES customer(id),
)
GO

CREATE TABLE paymentDetail (
	productId INT,
	paymentId INT,
	price INT,
	quantity INT,
	discount FLOAT,
	total FLOAT,
	createAt DATETIME,
	FOREIGN KEY (productId) REFERENCES product(id),
	FOREIGN KEY (paymentId) REFERENCES payment(id)
)
GO


CREATE TABLE menu(
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY ,
	parentId INT,
	menuName NVARCHAR(100),
	description NVARCHAR(100),
	url NVARCHAR(100),
	menuIndex int,
	isVisible BIT,
)
GO