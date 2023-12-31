USE [master]
GO
/****** Object:  Database [Pariks]    Script Date: 06.05.2023 21:50:07 ******/
CREATE DATABASE [Pariks]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Pariks', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS01\MSSQL\DATA\Pariks.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Pariks_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS01\MSSQL\DATA\Pariks_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Pariks] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Pariks].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Pariks] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Pariks] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Pariks] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Pariks] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Pariks] SET ARITHABORT OFF 
GO
ALTER DATABASE [Pariks] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Pariks] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Pariks] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Pariks] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Pariks] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Pariks] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Pariks] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Pariks] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Pariks] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Pariks] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Pariks] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Pariks] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Pariks] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Pariks] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Pariks] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Pariks] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Pariks] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Pariks] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Pariks] SET  MULTI_USER 
GO
ALTER DATABASE [Pariks] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Pariks] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Pariks] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Pariks] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Pariks] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Pariks] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Pariks] SET QUERY_STORE = OFF
GO
USE [Pariks]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 06.05.2023 21:50:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[categoryId] [int] NOT NULL,
	[categoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[categoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 06.05.2023 21:50:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[productId] [int] NOT NULL,
	[productName] [nvarchar](50) NOT NULL,
	[productCost] [int] NOT NULL,
	[productCategoryId] [int] NOT NULL,
	[productPathImage] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[productId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Categories] ([categoryId], [categoryName]) VALUES (1, N'Парики')
INSERT [dbo].[Categories] ([categoryId], [categoryName]) VALUES (2, N'Накладки')
INSERT [dbo].[Categories] ([categoryId], [categoryName]) VALUES (3, N'Карнавальные парики')
GO
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (1, N'Парик Juvia', 103999, 1, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (2, N'Парик Flavour', 66000, 1, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (3, N'Парик Brad', 14999, 1, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (4, N'Парик Gary', 25499, 1, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (5, N'Парик 307', 3400, 1, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (6, N'Парик 8039+33', 3200, 1, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (7, N'Парик DW 1078', 3000, 1, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (8, N'Парик M 223 hh mono', 14500, 1, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (9, N'Парик Bloom', 28400, 1, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (10, N'Парик Cri', 16600, 1, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (11, N'Наращивание HHW 22 (tress)', 7900, 2, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (12, N'Наращивание TRESS 20 HH', 9500, 2, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (13, N'Наращивание Syn', 1400, 2, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (14, N'Наращивание N 222 B', 1200, 2, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (15, N'Дед Мороз 100 см', 4300, 3, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (16, N'Парик Снегурочка Kar 02160', 3100, 3, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (17, N'Парик 24515-124', 3100, 3, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (18, N'Парик 81145white (Men)', 2800, 3, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (19, N'Парик 51712613 A', 3200, 3, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (20, N'Парик F 158101 (Придворная)', 3200, 3, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (21, N'Парик 14651 Афро', 2700, 3, N'null')
INSERT [dbo].[Products] ([productId], [productName], [productCost], [productCategoryId], [productPathImage]) VALUES (22, N'Парик 46167 AKAF 7 (Клоун Макдональдс)', 2500, 3, N'null')
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories1] FOREIGN KEY([productCategoryId])
REFERENCES [dbo].[Categories] ([categoryId])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories1]
GO
USE [master]
GO
ALTER DATABASE [Pariks] SET  READ_WRITE 
GO
