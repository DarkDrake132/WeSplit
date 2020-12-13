USE [master]
GO
/****** Object:  Database [WESPLIT]    Script Date: 12/8/2020 2:54:03 PM ******/
CREATE DATABASE [WESPLIT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WESPLIT', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\WESPLIT.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WESPLIT_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\WESPLIT_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [WESPLIT] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WESPLIT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WESPLIT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WESPLIT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WESPLIT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WESPLIT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WESPLIT] SET ARITHABORT OFF 
GO
ALTER DATABASE [WESPLIT] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [WESPLIT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WESPLIT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WESPLIT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WESPLIT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WESPLIT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WESPLIT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WESPLIT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WESPLIT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WESPLIT] SET  ENABLE_BROKER 
GO
ALTER DATABASE [WESPLIT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WESPLIT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WESPLIT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WESPLIT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WESPLIT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WESPLIT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WESPLIT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WESPLIT] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WESPLIT] SET  MULTI_USER 
GO
ALTER DATABASE [WESPLIT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WESPLIT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WESPLIT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WESPLIT] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WESPLIT] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WESPLIT] SET QUERY_STORE = OFF
GO
USE [WESPLIT]
GO
/****** Object:  Table [dbo].[EXPENSE]    Script Date: 12/8/2020 2:54:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EXPENSE](
	[idJourney] [int] NOT NULL,
	[idMember] [int] NOT NULL,
	[id] [int] NOT NULL,
	[objectPay] [nvarchar](max) NULL,
	[cost] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idJourney] ASC,
	[idMember] ASC,
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IMAGE_DESTINATION]    Script Date: 12/8/2020 2:54:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IMAGE_DESTINATION](
	[idJourney] [int] NOT NULL,
	[id] [int] NOT NULL,
	[imageLink] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[idJourney] ASC,
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JOURNEY]    Script Date: 12/8/2020 2:54:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JOURNEY](
	[id] [int] NOT NULL,
	[_location] [nvarchar](max) NULL,
	[title] [nvarchar](max) NULL,
	[isFinish] [int] NULL,
	[thumbnailLink] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MEMBER]    Script Date: 12/8/2020 2:54:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MEMBER](
	[idJourney] [int] NOT NULL,
	[id] [int] NOT NULL,
	[_name] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[idJourney] ASC,
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[EXPENSE] ([idJourney], [idMember], [id], [objectPay], [cost]) VALUES (1, 1, 1, N'Nước uống', 100000)
INSERT [dbo].[EXPENSE] ([idJourney], [idMember], [id], [objectPay], [cost]) VALUES (1, 1, 2, N'Snack', 25000)
INSERT [dbo].[EXPENSE] ([idJourney], [idMember], [id], [objectPay], [cost]) VALUES (1, 2, 1, N'Thuốc say xe', 50000)
INSERT [dbo].[EXPENSE] ([idJourney], [idMember], [id], [objectPay], [cost]) VALUES (1, 2, 2, N'Hộp quẹt', 10000)
INSERT [dbo].[EXPENSE] ([idJourney], [idMember], [id], [objectPay], [cost]) VALUES (1, 2, 3, N'Đèn pin', 35000)
INSERT [dbo].[EXPENSE] ([idJourney], [idMember], [id], [objectPay], [cost]) VALUES (1, 3, 1, N'Khăn giấy', 25000)
INSERT [dbo].[EXPENSE] ([idJourney], [idMember], [id], [objectPay], [cost]) VALUES (2, 1, 1, N'Nước uống', 30000)
INSERT [dbo].[EXPENSE] ([idJourney], [idMember], [id], [objectPay], [cost]) VALUES (2, 1, 2, N'Đèn pin', 50000)
INSERT [dbo].[EXPENSE] ([idJourney], [idMember], [id], [objectPay], [cost]) VALUES (2, 2, 1, N'Bánh', 25000)
INSERT [dbo].[EXPENSE] ([idJourney], [idMember], [id], [objectPay], [cost]) VALUES (2, 4, 1, N'Túi ngủ', 300000)
INSERT [dbo].[EXPENSE] ([idJourney], [idMember], [id], [objectPay], [cost]) VALUES (2, 5, 1, N'Túi giữ ấm', 125000)
INSERT [dbo].[EXPENSE] ([idJourney], [idMember], [id], [objectPay], [cost]) VALUES (2, 5, 2, N'Chăn', 50000)
INSERT [dbo].[EXPENSE] ([idJourney], [idMember], [id], [objectPay], [cost]) VALUES (2, 5, 3, N'Thuốc hạ sốt', 25000)
INSERT [dbo].[IMAGE_DESTINATION] ([idJourney], [id], [imageLink]) VALUES (1, 1, N'780_crop_phu-quoc-he-2020.jpg')
INSERT [dbo].[IMAGE_DESTINATION] ([idJourney], [id], [imageLink]) VALUES (1, 2, N'shutterstock-1355875478-3291-1-5462-7597-1590571669.jpg')
INSERT [dbo].[IMAGE_DESTINATION] ([idJourney], [id], [imageLink]) VALUES (2, 1, N'langbiang-du-lich-da-lat.jpg')
INSERT [dbo].[IMAGE_DESTINATION] ([idJourney], [id], [imageLink]) VALUES (2, 2, N'doi-co-hong-da-lat-3.jpg')
INSERT [dbo].[IMAGE_DESTINATION] ([idJourney], [id], [imageLink]) VALUES (2, 3, N'dalat-dia-diem-du-lich-da-lat-01-1.jpg')
INSERT [dbo].[JOURNEY] ([id], [_location], [title], [isFinish], [thumbnailLink]) VALUES (1, N'Phú Quốc', N'Thiên đường nghĩ dưỡng', 1, N'du-lich-phu-quoc-3-ngay-2-dem.jpg')
INSERT [dbo].[JOURNEY] ([id], [_location], [title], [isFinish], [thumbnailLink]) VALUES (2, N'Đà lạt', N'Tận hưởng niềm vui', 0, N'thanh-pho-da-lat.jpg')
INSERT [dbo].[MEMBER] ([idJourney], [id], [_name]) VALUES (1, 1, N'Bách')
INSERT [dbo].[MEMBER] ([idJourney], [id], [_name]) VALUES (1, 2, N'Hà')
INSERT [dbo].[MEMBER] ([idJourney], [id], [_name]) VALUES (1, 3, N'Khôi')
INSERT [dbo].[MEMBER] ([idJourney], [id], [_name]) VALUES (2, 1, N'Dương')
INSERT [dbo].[MEMBER] ([idJourney], [id], [_name]) VALUES (2, 2, N'Bảo')
INSERT [dbo].[MEMBER] ([idJourney], [id], [_name]) VALUES (2, 3, N'Đức')
INSERT [dbo].[MEMBER] ([idJourney], [id], [_name]) VALUES (2, 4, N'Dũng')
INSERT [dbo].[MEMBER] ([idJourney], [id], [_name]) VALUES (2, 5, N'Khôi')
ALTER TABLE [dbo].[EXPENSE]  WITH CHECK ADD FOREIGN KEY([idJourney], [idMember])
REFERENCES [dbo].[MEMBER] ([idJourney], [id])
GO
ALTER TABLE [dbo].[IMAGE_DESTINATION]  WITH CHECK ADD FOREIGN KEY([idJourney])
REFERENCES [dbo].[JOURNEY] ([id])
GO
ALTER TABLE [dbo].[MEMBER]  WITH CHECK ADD FOREIGN KEY([idJourney])
REFERENCES [dbo].[JOURNEY] ([id])
GO
USE [master]
GO
ALTER DATABASE [WESPLIT] SET  READ_WRITE 
GO

select* from journey
select* from member
select* from expense
select* from  Image_destination
