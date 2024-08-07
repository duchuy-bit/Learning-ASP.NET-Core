USE [master]
GO
/****** Object:  Database [DH_Shop]    Script Date: 7/27/2024 1:59:15 PM ******/
CREATE DATABASE [DH_Shop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DH_Shop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.DUCHUY\MSSQL\DATA\DH_Shop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DH_Shop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.DUCHUY\MSSQL\DATA\DH_Shop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DH_Shop] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DH_Shop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DH_Shop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DH_Shop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DH_Shop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DH_Shop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DH_Shop] SET ARITHABORT OFF 
GO
ALTER DATABASE [DH_Shop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DH_Shop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DH_Shop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DH_Shop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DH_Shop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DH_Shop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DH_Shop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DH_Shop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DH_Shop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DH_Shop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DH_Shop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DH_Shop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DH_Shop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DH_Shop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DH_Shop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DH_Shop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DH_Shop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DH_Shop] SET RECOVERY FULL 
GO
ALTER DATABASE [DH_Shop] SET  MULTI_USER 
GO
ALTER DATABASE [DH_Shop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DH_Shop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DH_Shop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DH_Shop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DH_Shop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DH_Shop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DH_Shop', N'ON'
GO
ALTER DATABASE [DH_Shop] SET QUERY_STORE = OFF
GO
USE [DH_Shop]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 7/27/2024 1:59:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customerId] [int] NULL,
	[createAt] [datetime] NULL,
	[productId] [int] NULL,
	[quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 7/27/2024 1:59:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NULL,
	[content] [nvarchar](500) NULL,
	[createAt] [datetime] NULL,
	[updateat] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 7/27/2024 1:59:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](100) NULL,
	[lastName] [nvarchar](100) NULL,
	[address] [nvarchar](500) NULL,
	[phone] [nvarchar](15) NULL,
	[email] [nvarchar](50) NULL,
	[img] [nvarchar](200) NULL,
	[registeredAt] [datetime] NULL,
	[updateAt] [datetime] NULL,
	[dateOfBirth] [date] NULL,
	[password] [nvarchar](200) NULL,
	[randomKey] [varchar](50) NULL,
	[isActive] [bit] NULL,
	[role] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[menu]    Script Date: 7/27/2024 1:59:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[menu](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[parentId] [int] NULL,
	[title] [nvarchar](100) NULL,
	[menuUrl] [nvarchar](100) NULL,
	[menuIndex] [int] NULL,
	[isVisible] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[payment]    Script Date: 7/27/2024 1:59:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[payment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customerId] [int] NULL,
	[firstName] [nvarchar](100) NULL,
	[lastName] [nvarchar](100) NULL,
	[phone] [nvarchar](15) NULL,
	[email] [nvarchar](50) NULL,
	[createAt] [datetime] NULL,
	[total] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[paymentDetail]    Script Date: 7/27/2024 1:59:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[paymentDetail](
	[productId] [int] NULL,
	[paymentId] [int] NULL,
	[price] [int] NULL,
	[quantity] [int] NULL,
	[total] [float] NULL,
	[createAt] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 7/27/2024 1:59:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[title] [nvarchar](100) NULL,
	[content] [nvarchar](500) NULL,
	[img] [nvarchar](200) NULL,
	[price] [int] NULL,
	[rate] [float] NULL,
	[createAt] [datetime] NULL,
	[updateAt] [datetime] NULL,
	[categoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[menu] ADD  DEFAULT ((1)) FOR [isVisible]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD FOREIGN KEY([customerId])
REFERENCES [dbo].[customer] ([id])
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD FOREIGN KEY([productId])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[payment]  WITH CHECK ADD FOREIGN KEY([customerId])
REFERENCES [dbo].[customer] ([id])
GO
ALTER TABLE [dbo].[paymentDetail]  WITH CHECK ADD FOREIGN KEY([paymentId])
REFERENCES [dbo].[payment] ([id])
GO
ALTER TABLE [dbo].[paymentDetail]  WITH CHECK ADD FOREIGN KEY([productId])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD FOREIGN KEY([categoryId])
REFERENCES [dbo].[category] ([id])
GO
USE [master]
GO
ALTER DATABASE [DH_Shop] SET  READ_WRITE 
GO
