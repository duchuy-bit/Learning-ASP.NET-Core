USE [DH_Shop]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 8/3/2024 5:20:18 PM ******/
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
/****** Object:  Table [dbo].[category]    Script Date: 8/3/2024 5:20:18 PM ******/
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
/****** Object:  Table [dbo].[customer]    Script Date: 8/3/2024 5:20:18 PM ******/
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
/****** Object:  Table [dbo].[menu]    Script Date: 8/3/2024 5:20:18 PM ******/
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
/****** Object:  Table [dbo].[payment]    Script Date: 8/3/2024 5:20:18 PM ******/
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
/****** Object:  Table [dbo].[paymentDetail]    Script Date: 8/3/2024 5:20:18 PM ******/
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
/****** Object:  Table [dbo].[product]    Script Date: 8/3/2024 5:20:18 PM ******/
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
