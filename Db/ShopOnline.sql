USE [ShopOnline]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 21/12/2015 4:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](max) NOT NULL,
	[CategoryLevel] [int] NOT NULL,
	[ParentCategoryId] [int] NULL,
	[CreatedUserId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Color]    Script Date: 21/12/2015 4:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Color](
	[ColorId] [int] IDENTITY(1,1) NOT NULL,
	[ColorName] [nvarchar](max) NOT NULL,
	[ColorValue] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED 
(
	[ColorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comment]    Script Date: 21/12/2015 4:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[CommentDetail] [nvarchar](max) NOT NULL,
	[CommentDate] [datetime] NOT NULL,
	[CommentUserId] [int] NOT NULL,
	[ProductId] [int] NULL,
	[ParentCommentId] [int] NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Config]    Script Date: 21/12/2015 4:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Config](
	[ConfigId] [int] IDENTITY(1,1) NOT NULL,
	[ConfigCode] [nvarchar](200) NOT NULL,
	[ConfigValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_Config] PRIMARY KEY CLUSTERED 
(
	[ConfigId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Location]    Script Date: 21/12/2015 4:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[LocationId] [int] NOT NULL,
	[LocationName] [nvarchar](max) NOT NULL,
	[LocationLevel] [int] NOT NULL,
	[ParentId] [int] NULL,
 CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Order]    Script Date: 21/12/2015 4:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[ShippingAddress] [nvarchar](max) NULL,
	[UserId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[ShippingTelephone] [nvarchar](max) NULL,
	[LocationCityId] [int] NULL,
	[LocationDistrictId] [int] NULL,
	[OrderStatus] [int] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 21/12/2015 4:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 21/12/2015 4:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductCode] [nvarchar](max) NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[Price] [money] NULL,
	[SaleOffPrice] [money] NULL,
	[IsSaleOff] [bit] NULL,
	[IsNew] [bit] NULL,
	[ProductShortDescription] [nvarchar](max) NULL,
	[ProductDetailDescription] [nvarchar](max) NULL,
	[BrandId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductBrand]    Script Date: 21/12/2015 4:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductBrand](
	[BrandId] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [date] NOT NULL,
 CONSTRAINT [PK_ProductBrand] PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductCategory]    Script Date: 21/12/2015 4:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategory](
	[ProductCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_ProductCategory] PRIMARY KEY CLUSTERED 
(
	[ProductCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductColor]    Script Date: 21/12/2015 4:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductColor](
	[ProductColorId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[ColorId] [int] NOT NULL,
 CONSTRAINT [PK_ProductColor] PRIMARY KEY CLUSTERED 
(
	[ProductColorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductImage]    Script Date: 21/12/2015 4:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductImage](
	[ProductImageId] [int] IDENTITY(1,1) NOT NULL,
	[ProductImageUrl] [nvarchar](max) NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_ProductImage] PRIMARY KEY CLUSTERED 
(
	[ProductImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 21/12/2015 4:17:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[RealName] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NULL,
	[Role] [int] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Telephone] [nvarchar](max) NULL,
	[LocationDistrictId] [int] NULL,
	[LocationCityId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Category] ON 

GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryLevel], [ParentCategoryId], [CreatedUserId], [CreatedDate], [UpdatedDate]) VALUES (3, N'Quần áo Nam', 0, NULL, 1, CAST(0x0000A55C00E6AFD0 AS DateTime), CAST(0x0000A5640150DD76 AS DateTime))
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryLevel], [ParentCategoryId], [CreatedUserId], [CreatedDate], [UpdatedDate]) VALUES (10, N'Áo Sơ Mi', 1, 3, 1, CAST(0x0000A55D0166E78E AS DateTime), CAST(0x0000A55D0166E78E AS DateTime))
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryLevel], [ParentCategoryId], [CreatedUserId], [CreatedDate], [UpdatedDate]) VALUES (14, N'Đồ Gia Dụng', 0, NULL, 1, CAST(0x0000A564015100BE AS DateTime), CAST(0x0000A56900F6C3A8 AS DateTime))
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryLevel], [ParentCategoryId], [CreatedUserId], [CreatedDate], [UpdatedDate]) VALUES (20, N'Bàn ủi', 1, 14, 1, CAST(0x0000A5640153E3DE AS DateTime), CAST(0x0000A5640153E3DE AS DateTime))
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryLevel], [ParentCategoryId], [CreatedUserId], [CreatedDate], [UpdatedDate]) VALUES (21, N'Chảo', 1, 14, 1, CAST(0x0000A5640153ED52 AS DateTime), CAST(0x0000A5640153ED52 AS DateTime))
GO
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryLevel], [ParentCategoryId], [CreatedUserId], [CreatedDate], [UpdatedDate]) VALUES (1022, N'Áo thun', 1, 3, 1, CAST(0x0000A56A0174ACCF AS DateTime), CAST(0x0000A56A0174ACCF AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Color] ON 

GO
INSERT [dbo].[Color] ([ColorId], [ColorName], [ColorValue], [CreatedDate], [UpdatedDate]) VALUES (1, N'Vàng', N'#fbff30', CAST(0x0000A56A0183593E AS DateTime), CAST(0x0000A56B0002FFB8 AS DateTime))
GO
INSERT [dbo].[Color] ([ColorId], [ColorName], [ColorValue], [CreatedDate], [UpdatedDate]) VALUES (2, N'Xanh Dương', N'#0047ff', CAST(0x0000A56D017734B9 AS DateTime), CAST(0x0000A56D017734B9 AS DateTime))
GO
INSERT [dbo].[Color] ([ColorId], [ColorName], [ColorValue], [CreatedDate], [UpdatedDate]) VALUES (3, N'Đỏ', N'#ff0000', CAST(0x0000A56D017742BA AS DateTime), CAST(0x0000A56D017742BA AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Color] OFF
GO
SET IDENTITY_INSERT [dbo].[Comment] ON 

GO
INSERT [dbo].[Comment] ([CommentId], [CommentDetail], [CommentDate], [CommentUserId], [ProductId], [ParentCommentId]) VALUES (8, N'd', CAST(0x0000A57500F8FAC8 AS DateTime), 5, 1034, NULL)
GO
INSERT [dbo].[Comment] ([CommentId], [CommentDetail], [CommentDate], [CommentUserId], [ProductId], [ParentCommentId]) VALUES (9, N'f', CAST(0x0000A57500FA32DD AS DateTime), 5, 1034, NULL)
GO
INSERT [dbo].[Comment] ([CommentId], [CommentDetail], [CommentDate], [CommentUserId], [ProductId], [ParentCommentId]) VALUES (12, N'c', CAST(0x0000A5750107B28F AS DateTime), 5, NULL, 9)
GO
INSERT [dbo].[Comment] ([CommentId], [CommentDetail], [CommentDate], [CommentUserId], [ProductId], [ParentCommentId]) VALUES (13, N'd', CAST(0x0000A575010891BF AS DateTime), 5, NULL, 9)
GO
SET IDENTITY_INSERT [dbo].[Comment] OFF
GO
SET IDENTITY_INSERT [dbo].[Config] ON 

GO
INSERT [dbo].[Config] ([ConfigId], [ConfigCode], [ConfigValue]) VALUES (1002, N'Logo', N'~/Images/Logo/logo_635852625998749343.png')
GO
INSERT [dbo].[Config] ([ConfigId], [ConfigCode], [ConfigValue]) VALUES (1003, N'Phone', N'01285591685')
GO
INSERT [dbo].[Config] ([ConfigId], [ConfigCode], [ConfigValue]) VALUES (1004, N'Address', N'<p>Shop sỉ</p><p>Số 1 cách mạng tháng 8</p><p>Quận 1</p><p>Sdt: 099999999</p>')
GO
INSERT [dbo].[Config] ([ConfigId], [ConfigCode], [ConfigValue]) VALUES (1005, N'Contact', N'<h2 style="font-family: ''Open Sans'', Helvetica, Arial, sans-serif; line-height: 1.2; color: rgb(41, 44, 46); margin-top: 8px; margin-bottom: 24px; font-size: 1.75em;">Liên hệ</h2><h3 style="font-family: ''Open Sans'', Helvetica, Arial, sans-serif; line-height: 1.2; color: rgb(41, 44, 46); margin-top: 8px; margin-bottom: 24px; font-size: 1.5em;">Hotline: 0938020091</h3><div style="color: rgb(255, 87, 34); font-family: ''Open Sans'', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 22.8571px;">Chi nhánh LVS:</div><div class="cont-info-widget" style="width: 1140px; margin-bottom: 36px; color: rgb(255, 87, 34); font-family: ''Open Sans'', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 22.8571px;"><ul style="margin-right: 0px; margin-bottom: 0px; margin-left: 0px; font-size: 0.95em; line-height: 1.7; color: rgb(41, 44, 46); list-style: none; padding: 0px;"><li style="margin-bottom: 8px;"><span class="fa fa-home" style="transform: translate(0px, 0px); margin-right: 8px; color: rgb(255, 87, 34) !important;"></span>&nbsp;262/6 Lê Văn Sỹ, P.14, Q.3, TP.HCM</li><li style="margin-bottom: 8px;"><span class="fa fa-phone" style="font-size: 1.1em; transform: translate(0px, 0px); margin-right: 8px; color: rgb(255, 87, 34) !important;"></span>&nbsp;<a href="tel:0934020091" style="color: rgb(41, 44, 46); transition: all 0.3s;">0934020091</a></li></ul></div><div style="color: rgb(255, 87, 34); font-family: ''Open Sans'', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 22.8571px;">Chi nhánh TBT:</div><div class="cont-info-widget" style="width: 1140px; margin-bottom: 36px; color: rgb(255, 87, 34); font-family: ''Open Sans'', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 22.8571px;"><ul style="margin-right: 0px; margin-bottom: 0px; margin-left: 0px; font-size: 0.95em; line-height: 1.7; color: rgb(41, 44, 46); list-style: none; padding: 0px;"><li style="margin-bottom: 8px;"><span class="fa fa-home" style="transform: translate(0px, 0px); margin-right: 8px; color: rgb(255, 87, 34) !important;"></span>&nbsp;190A Trần Bình Trọng, P.3, Q.5, TP.HCM</li><li style="margin-bottom: 8px;"><span class="fa fa-phone" style="font-size: 1.1em; transform: translate(0px, 0px); margin-right: 8px; color: rgb(255, 87, 34) !important;"></span>&nbsp;<a href="tel:0938030091" style="color: rgb(41, 44, 46); transition: all 0.3s;">0938030091</a></li></ul></div><div style="color: rgb(255, 87, 34); font-family: ''Open Sans'', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 22.8571px;">Chi nhánh XVNT:</div><div class="cont-info-widget" style="width: 1140px; margin-bottom: 36px; color: rgb(255, 87, 34); font-family: ''Open Sans'', Helvetica, Arial, sans-serif; font-size: 16px; line-height: 22.8571px;"><ul style="margin-right: 0px; margin-bottom: 0px; margin-left: 0px; font-size: 0.95em; line-height: 1.7; color: rgb(41, 44, 46); list-style: none; padding: 0px;"><li style="margin-bottom: 8px;"><span class="fa fa-home" style="transform: translate(0px, 0px); margin-right: 8px; color: rgb(255, 87, 34) !important;"></span>&nbsp;133/26A Xô Viết Nghệ Tĩnh, P.17, Q.Bình Thạnh, TP.HCM</li><li style="margin-bottom: 8px;"><span class="fa fa-phone" style="font-size: 1.1em; transform: translate(0px, 0px); margin-right: 8px; color: rgb(255, 87, 34) !important;"></span>&nbsp;<a href="tel:0902992100" style="color: rgb(41, 44, 46); transition: all 0.3s;">0902992100</a></li></ul></div>')
GO
INSERT [dbo].[Config] ([ConfigId], [ConfigCode], [ConfigValue]) VALUES (1006, N'Email', N'akai777@gmail.com')
GO
INSERT [dbo].[Config] ([ConfigId], [ConfigCode], [ConfigValue]) VALUES (1007, N'Intro', N'Shop Sĩ chuyên cung cấp các sản phẩm dân dụng hàng ngày với giá sỉ.')
GO
INSERT [dbo].[Config] ([ConfigId], [ConfigCode], [ConfigValue]) VALUES (1008, N'Employee1', N'pk_lilo777')
GO
INSERT [dbo].[Config] ([ConfigId], [ConfigCode], [ConfigValue]) VALUES (1009, N'Employee2', N'pk_lilo777')
GO
INSERT [dbo].[Config] ([ConfigId], [ConfigCode], [ConfigValue]) VALUES (1010, N'Employee3', N'pk_lilo777')
GO
SET IDENTITY_INSERT [dbo].[Config] OFF
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (1, N'Việt Nam', 0, 0)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (2, N'Hà Nội', 1, 1)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (3, N'TP.Hồ Chí Minh', 1, 1)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (5, N'Quận 1', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (6, N'Quận 2', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (7, N'Quận 3', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (8, N'Quận 7', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (9, N'Quận Bình Thạnh', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (10, N'Quận Gò Vấp', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (14, N'Quận 4', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (15, N'Quận 5', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (16, N'Quận 6', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (17, N'Quận 8', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (18, N'Quận 9', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (19, N'Quận 10', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (20, N'Quận 11', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (21, N'Quận 12', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (22, N'Quận Tân Bình', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (23, N'Quận Tân Phú', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (24, N'Quận Phú Nhuận', 2, 3)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (25, N'Ba Đình', 2, 2)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (26, N'Cầu Giấy', 2, 2)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (27, N'Đống Đa', 2, 2)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (28, N'Hai Bà Trưng', 2, 2)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (29, N'Hà Đông', 2, 2)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (30, N'Hoàng Mai', 2, 2)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (31, N'Hoàn Kiếm', 2, 2)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (32, N'Long Biên', 2, 2)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (33, N'Tây Hồ ', 2, 2)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (34, N'Thanh Xuân', 2, 2)
GO
INSERT [dbo].[Location] ([LocationId], [LocationName], [LocationLevel], [ParentId]) VALUES (35, N'Từ Liêm', 2, 2)
GO
SET IDENTITY_INSERT [dbo].[Order] ON 

GO
INSERT [dbo].[Order] ([OrderId], [ShippingAddress], [UserId], [CreatedDate], [UpdatedDate], [ShippingTelephone], [LocationCityId], [LocationDistrictId], [OrderStatus]) VALUES (1003, N'144 cmt8', 5, CAST(0x0000A57100801696 AS DateTime), CAST(0x0000A57100801696 AS DateTime), N'01285591685', 3, 10, 0)
GO
SET IDENTITY_INSERT [dbo].[Order] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] ON 

GO
INSERT [dbo].[OrderDetail] ([OrderDetailId], [OrderId], [ProductId], [Price], [Quantity]) VALUES (2004, 1003, 1041, 12000.0000, 2)
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

GO
INSERT [dbo].[Product] ([ProductId], [ProductCode], [ProductName], [Price], [SaleOffPrice], [IsSaleOff], [IsNew], [ProductShortDescription], [ProductDetailDescription], [BrandId], [CreatedDate], [UpdatedDate]) VALUES (1029, N'ASM2017', N'áo sơ mi', 200000.0000, NULL, 0, 0, N'<p>Miêu tả ngắn</p>', N'<p>Miêu tả chi tiết</p>', NULL, CAST(0x0000A5640156C24C AS DateTime), CAST(0x0000A5640156C24C AS DateTime))
GO
INSERT [dbo].[Product] ([ProductId], [ProductCode], [ProductName], [Price], [SaleOffPrice], [IsSaleOff], [IsNew], [ProductShortDescription], [ProductDetailDescription], [BrandId], [CreatedDate], [UpdatedDate]) VALUES (1030, N'BUX', N'Bàn ủi xanh', 100000.0000, NULL, 0, 0, N'<p>bàn ủi màu xanh</p>', N'<p>bàn ủi <span style="font-weight: bold;">màu xanh</span><br></p>', NULL, CAST(0x0000A5640159D404 AS DateTime), CAST(0x0000A5640159D404 AS DateTime))
GO
INSERT [dbo].[Product] ([ProductId], [ProductCode], [ProductName], [Price], [SaleOffPrice], [IsSaleOff], [IsNew], [ProductShortDescription], [ProductDetailDescription], [BrandId], [CreatedDate], [UpdatedDate]) VALUES (1031, N'BUN', N'Bàn ủi nâu', 200000.0000, NULL, 0, 0, N'<p>MTN</p>', N'<p>MTCC</p>', NULL, CAST(0x0000A564015AB791 AS DateTime), CAST(0x0000A564015AB791 AS DateTime))
GO
INSERT [dbo].[Product] ([ProductId], [ProductCode], [ProductName], [Price], [SaleOffPrice], [IsSaleOff], [IsNew], [ProductShortDescription], [ProductDetailDescription], [BrandId], [CreatedDate], [UpdatedDate]) VALUES (1032, N'BUMX', N'Bàn ủi màu xanh', 120000.0000, NULL, 0, 0, N'<p>MTN</p>', N'<p>MTCT</p>', NULL, CAST(0x0000A564015CBA86 AS DateTime), CAST(0x0000A564015CBA86 AS DateTime))
GO
INSERT [dbo].[Product] ([ProductId], [ProductCode], [ProductName], [Price], [SaleOffPrice], [IsSaleOff], [IsNew], [ProductShortDescription], [ProductDetailDescription], [BrandId], [CreatedDate], [UpdatedDate]) VALUES (1033, N'CHAO', N'Chảo 2017', 70000.0000, NULL, NULL, 1, N'<p>MTN</p>', N'<p>MTCT</p>', NULL, CAST(0x0000A56900376121 AS DateTime), CAST(0x0000A569003C5619 AS DateTime))
GO
INSERT [dbo].[Product] ([ProductId], [ProductCode], [ProductName], [Price], [SaleOffPrice], [IsSaleOff], [IsNew], [ProductShortDescription], [ProductDetailDescription], [BrandId], [CreatedDate], [UpdatedDate]) VALUES (1034, N'CKD', N'Chao khong dinh', 100000.0000, NULL, NULL, 0, N'<p>MTN</p>', N'<p>MTCT</p>', NULL, CAST(0x0000A569005606A4 AS DateTime), CAST(0x0000A569005606A4 AS DateTime))
GO
INSERT [dbo].[Product] ([ProductId], [ProductCode], [ProductName], [Price], [SaleOffPrice], [IsSaleOff], [IsNew], [ProductShortDescription], [ProductDetailDescription], [BrandId], [CreatedDate], [UpdatedDate]) VALUES (1041, N'SPa', N'Sản phẩm a', 12000.0000, NULL, NULL, 0, N'<p><br></p>', N'<p><br></p>', 1, CAST(0x0000A56D01848C44 AS DateTime), CAST(0x0000A56F00F4A88B AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductBrand] ON 

GO
INSERT [dbo].[ProductBrand] ([BrandId], [BrandName], [CreatedDate], [UpdatedDate]) VALUES (1, N'Kim Hằng', CAST(0x0000A56A016C942D AS DateTime), CAST(0xC53A0B00 AS Date))
GO
SET IDENTITY_INSERT [dbo].[ProductBrand] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] ON 

GO
INSERT [dbo].[ProductCategory] ([ProductCategoryId], [CategoryId], [ProductId]) VALUES (1005, 10, 1029)
GO
INSERT [dbo].[ProductCategory] ([ProductCategoryId], [CategoryId], [ProductId]) VALUES (1006, 20, 1030)
GO
INSERT [dbo].[ProductCategory] ([ProductCategoryId], [CategoryId], [ProductId]) VALUES (1007, 20, 1031)
GO
INSERT [dbo].[ProductCategory] ([ProductCategoryId], [CategoryId], [ProductId]) VALUES (1008, 21, 1033)
GO
INSERT [dbo].[ProductCategory] ([ProductCategoryId], [CategoryId], [ProductId]) VALUES (3013, 21, 1041)
GO
SET IDENTITY_INSERT [dbo].[ProductCategory] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductColor] ON 

GO
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (6, 1041, 2)
GO
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (7, 1041, 1)
GO
SET IDENTITY_INSERT [dbo].[ProductColor] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductImage] ON 

GO
INSERT [dbo].[ProductImage] ([ProductImageId], [ProductImageUrl], [ProductId]) VALUES (1071, N'~/images/product/somi_635848588768414017.jpg', 1029)
GO
INSERT [dbo].[ProductImage] ([ProductImageId], [ProductImageUrl], [ProductId]) VALUES (1072, N'~/images/product/somi2_635848588768434018.jpg', 1029)
GO
INSERT [dbo].[ProductImage] ([ProductImageId], [ProductImageUrl], [ProductId]) VALUES (1073, N'~/images/product/banuixanh_635848595473198875.jpg', 1030)
GO
INSERT [dbo].[ProductImage] ([ProductImageId], [ProductImageUrl], [ProductId]) VALUES (1074, N'~/images/product/banuinau_635848597414979939.jpg', 1031)
GO
INSERT [dbo].[ProductImage] ([ProductImageId], [ProductImageUrl], [ProductId]) VALUES (1075, N'~/images/product/banuixanh_635848601809271278.jpg', 1032)
GO
INSERT [dbo].[ProductImage] ([ProductImageId], [ProductImageUrl], [ProductId]) VALUES (1076, N'~/images/product/chaovang_635852280978165722.jpg', 1033)
GO
INSERT [dbo].[ProductImage] ([ProductImageId], [ProductImageUrl], [ProductId]) VALUES (1078, N'~/images/product/chaovang_635852347926530037.jpg', 1034)
GO
INSERT [dbo].[ProductImage] ([ProductImageId], [ProductImageUrl], [ProductId]) VALUES (3079, N'~/images/product/Hydrangeas_635856464795876173.jpg', 1041)
GO
SET IDENTITY_INSERT [dbo].[ProductImage] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

GO
INSERT [dbo].[User] ([UserId], [UserName], [RealName], [Password], [Email], [Role], [Address], [Telephone], [LocationDistrictId], [LocationCityId], [CreatedDate]) VALUES (1, N'admin', NULL, N'admin', NULL, 0, NULL, NULL, NULL, NULL, CAST(0x0000A56800000000 AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [RealName], [Password], [Email], [Role], [Address], [Telephone], [LocationDistrictId], [LocationCityId], [CreatedDate]) VALUES (5, N'user1', N'Tú', N'user1', N'akai777@gmail.com', 1, N'144 cmt8', N'01285591685', 10, 3, CAST(0x0000A568010F5B26 AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [RealName], [Password], [Email], [Role], [Address], [Telephone], [LocationDistrictId], [LocationCityId], [CreatedDate]) VALUES (1005, N'user2', N'', N'user2', N'', 1, N'', N'', NULL, NULL, CAST(0x0000A5690124854F AS DateTime))
GO
INSERT [dbo].[User] ([UserId], [UserName], [RealName], [Password], [Email], [Role], [Address], [Telephone], [LocationDistrictId], [LocationCityId], [CreatedDate]) VALUES (1006, N'user3', N'', N'user3', N'', 1, N'', N'', NULL, NULL, CAST(0x0000A569012558C0 AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_Category] FOREIGN KEY([ParentCategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_Category]
GO
ALTER TABLE [dbo].[Category]  WITH CHECK ADD  CONSTRAINT [FK_Category_User] FOREIGN KEY([CreatedUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Category] CHECK CONSTRAINT [FK_Category_User]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Comment] FOREIGN KEY([ParentCommentId])
REFERENCES [dbo].[Comment] ([CommentId])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Comment]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Product]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_User] FOREIGN KEY([CommentUserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_User]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Location] FOREIGN KEY([LocationCityId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Location]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Location1] FOREIGN KEY([LocationDistrictId])
REFERENCES [dbo].[Location] ([LocationId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Location1]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_ProductBrand] FOREIGN KEY([BrandId])
REFERENCES [dbo].[ProductBrand] ([BrandId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_ProductBrand]
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[ProductCategory] CHECK CONSTRAINT [FK_ProductCategory_Category]
GO
ALTER TABLE [dbo].[ProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_ProductCategory_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductCategory] CHECK CONSTRAINT [FK_ProductCategory_Product]
GO
ALTER TABLE [dbo].[ProductColor]  WITH CHECK ADD  CONSTRAINT [FK_ProductColor_Color] FOREIGN KEY([ColorId])
REFERENCES [dbo].[Color] ([ColorId])
GO
ALTER TABLE [dbo].[ProductColor] CHECK CONSTRAINT [FK_ProductColor_Color]
GO
ALTER TABLE [dbo].[ProductColor]  WITH CHECK ADD  CONSTRAINT [FK_ProductColor_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductColor] CHECK CONSTRAINT [FK_ProductColor_Product]
GO
ALTER TABLE [dbo].[ProductImage]  WITH CHECK ADD  CONSTRAINT [FK_ProductImage_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ProductImage] CHECK CONSTRAINT [FK_ProductImage_Product]
GO
