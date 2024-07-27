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
	updateAt DATETIME
)
GO

CREATE TABLE product(
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY ,
	title NVARCHAR(100),
	content NVARCHAR(500),
	img NVARCHAR(200),
	price INT,
	rate FLOAT,
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
	address NVARCHAR(500),
	phone NVARCHAR(15),
	email NVARCHAR(50),
	img NVARCHAR(200),
	registeredAt DATETIME,
	updateAt DATETIME,
	dateOfBirth DATE null,	
	password NVARCHAR(200),
	randomKey varchar (50) null,
	isActive BIT,
	role INT  --  0: user, 1: admin,
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
	total FLOAT,
	FOREIGN KEY (customerId) REFERENCES customer(id),
)
GO

CREATE TABLE paymentDetail (
	productId INT,
	paymentId INT,
	price INT,
	quantity INT,
	total FLOAT,
	createAt DATETIME,
	FOREIGN KEY (productId) REFERENCES product(id),
	FOREIGN KEY (paymentId) REFERENCES payment(id)
)
GO

--CREATE TABLE menu(
--	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY ,
--	parentId INT null,
--	title NVARCHAR(100),
--	menuArea NVARCHAR(100) null,
--	menuController NVARCHAR(100) null,
--	menuAction NVARCHAR(100) null,
--	menuParams INT null,
--	menuIndex int,
--	isVisible BIT DEFAULT 1 , -- 1: display || 0: hidden 
--)
--GO
CREATE TABLE menu(
	id INT NOT NULL IDENTITY(1,1) PRIMARY KEY ,
	parentId INT NULL,
	title NVARCHAR(100),
	menuUrl NVARCHAR(100),
	menuIndex int,
	isVisible BIT DEFAULT 1, --1: DISPLAY || 0: HIDDEN
)
GO



insert into menu (title, parentId,menuUrl, menuIndex, isVisible )
values 
(N'Trang chủ', NULL, N'/',1, 1),
(N'Cửa hàng', NULL, N'/Product',2, 1),
(N'Danh mục', NULL, NULL ,3, 1),
(N'Điện thoại', 3, N'/Product?idcategory=1',4, 1),
(N'Laptop', 3, N'/Product?idcategory=2',5, 1),
(N'PC', 3, N'/Product?idcategory=3',6, 1),
(N'Tablet', 3, N'/Product?idcategory=4',7, 1),
(N'Phụ kiện', 3, N'/Product?idcategory=5',8, 1),
(N'Smart Watch', 3, N'/Product?idcategory=6',9, 1),
(N'Liên hệ', NULL, N'/',10, 1),
(N'Trang quản trị', NULL, N'/Admin/ProductAdmin',11, 1)
go


select * from menu
order by menuIndex
--values
--(N'Trang chủ',null ,N'',N'Home',N'Index', null , 1, 1 ),
--(N'Cửa hàng', null ,N'',N'Product',N'Index',null, 2, 1),
--(N'Loại sản phẩm',null, null, null, null, null, 3, 1),
--(N'Điện Thoại', 3, N'',N'Product',N'Index',1 , 1 , 1),
--(N'Laptop', 3, N'',N'Product',N'Index',2 , 1 , 1),
--(N'PC', 3, N'',N'Product',N'Index',3 , 1 , 1),
--(N'Tablet', 3, N'',N'Product',N'Index',4 , 1 , 1),
--(N'Smart Watch', 3, N'',N'Product',N'Index',5 , 1 , 1),
--(N'Contact', null , N'',N'Product',N'Index',5 , 1 , 1),
--(N'Admin', null , N'Admin',N'Product',N'Index',5 , 1 , 1)
--go


---------------------- INSERT category -----------------
insert into category(title, content, createAt, updateAt)
values 
(N'Điện Thoại',N'Cung cấp tất cả những sản phẩm chính hãng của thương hiệu Apple như là: iPhone, iPad, Watch, Mac và cả những sản phẩm Samsung cùng với phụ Kiện Xiaomi.',GETDATE(),GETDATE()),
(N'Laptop',N'Tự hào là một trong những đơn vị hàng đầu trong lĩnh vực kinh doanh laptop, linh kiện laptop tại Khánh Hòa, Với trên 10 năm kinh nghiệm, theo phương châm "Uy tín trên từng sản phẩm"  cùng hơn 50.000 Khách Hàng thân thiết, chúng tôi cam kết tất cả các sản phẩm laptop bán ra đều có chất lượng tốt nhất trên thị trường hiện nay. Tất cả laptop, linh kiện tại showroom đều được bảo hành chuẩn chỉ theo quy chế của các hãng.',GETDATE(),GETDATE()),
(N'PC',N'Chuyên cung cấp các sản phẩm laptop xách tay Mỹ, máy tính để bàn chính hãng zin 100%, các loại linh kiện vi tính chính hãng... Đến với Worklap, người dùng sẽ được trải nghiệm các thiết bị máy tính laptop xách tay chất lượng với nhiều phân khúc giá, thương hiệu, phù hợp với nhiều nhu cầu sử dụng khác nhau.',GETDATE(),GETDATE()),
(N'Tablet',N'Khi mua một chiếc tablet, chắc hẳn bạn sẽ phải tìm hiểu rất kỹ mới có thể đưa ra quyết định, vì con số chi trả cho nó không phải nhỏ. Và không biết thiết bị đó có thể hỗ trợ liên lạc, học tập, giải trí,... Vậy hãy cùng tìm hiểu kĩ về tablet và cùng tham khảo các thương hiệu Tablet HOT nhất hiện nay để có được sự lựa chọn phù hợp bạn nhé!',GETDATE(),GETDATE()),
(N'Phụ kiện',N'Cung cấp nhiều phụ kiện mới, nổi bật khác như: Phụ Kiện Apple Watch, Củ Sạc - Sạc Không Dây, Cáp Sạc - Đầu Chuyển, Pin - Sạc Dự Phòng - Ốp Sạc, Tai Nghe - Tai Nghe Không Dây, Loa Không Dây - Tv Box, Bàn Phím - Gậy Chụp Ảnh, Tb.Lưu Trữ - Đ.C Công Nghệ, Túi Đeo - Bao Da - Sim Ghép, Phụ Kiện Công Nghệ Xiaomi , Vòng Đeo Tay Thông Minh.',GETDATE(),GETDATE()),
(N'Smart Watch',N'Nếu bạn đang tìm kiếm một chiếc smartwatch chính hãng có thiết kế được ưa chuộng cho bản thân hoặc dành tặng cho người quen hãy tham khảo ngay top 7 các hãng đồng hồ thông minh chính hãng rất được ưa chuộng tại website của chúng tôi để tha hồ lựa chọn.',GETDATE(),GETDATE())
go

---------------------- INSERT product -----------------
--insert into product (title, content , createAt, updateAt, img, price, rate , categoryId)
--values
--(N'',N'',GETDATE(),GETDATE(),N'',14, 5, 1),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.5, 1),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4, 2),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 5, 1),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.5, 1),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.3, 1),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4, 3),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.9, 2),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 5, 1),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.5, 3),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.9, 2),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.7, 4),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.8, 5),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.7, 2),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 5, 3),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.9, 4),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.5, 5),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 5, 4),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 5, 3),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 5, 5),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.5, 4),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 5, 4),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 5, 5),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 5, 2),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.8, 3),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.8, 4),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 4.9, 5),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 5, 3),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 5, 2),
--(N'',N'',GETDATE(),GETDATE(),N'',14, 5, 4)
--go
---------------------- INSERT customer -----------------
insert into customer (lastName, firstName, phone, email, dateOfBirth, img, address, password, randomKey, isActive, role)
values 
(N'Nguyễn',N'Admin',N'0987654321',N'admin@gmail.com','2001-01-16', 'avatar1.png', 'Nha Trang - Khanh Hoa','123', 'random', 1, 1),
(N'Nguyễn Đức',N'Huy',N'0987654321',N'email@gmail.com','2001-01-16', 'avatar2.jpg','Ninh Hoa - Khanh Hoa', '123', 'random', 1, 0)
go

---------------------- INSERT cart -----------------

---------------------- INSERT payment -----------------

---------------------- INSERT paymentDetail -----------------

---------------------- INSERT menu -----------------
--insert into menu (title, parentId, menuArea,MenuController,MenuAction,MenuParams, menuIndex, isVisible )
--values
--(N'Trang chủ',null ,N'',N'Home',N'Index', null , 1, 1 ),
--(N'Cửa hàng', null ,N'',N'Product',N'Index',null, 2, 1),
--(N'Loại sản phẩm',null, null, null, null, null, 3, 1),
--(N'Điện Thoại', 3, N'',N'Product',N'Index',1 , 1 , 1),
--(N'Laptop', 3, N'',N'Product',N'Index',2 , 1 , 1),
--(N'PC', 3, N'',N'Product',N'Index',3 , 1 , 1),
--(N'Tablet', 3, N'',N'Product',N'Index',4 , 1 , 1),
--(N'Smart Watch', 3, N'',N'Product',N'Index',5 , 1 , 1),
--(N'Contact', null , N'',N'Product',N'Index',5 , 1 , 1),
--(N'Admin', null , N'Admin',N'Product',N'Index',5 , 1 , 1)
--go

