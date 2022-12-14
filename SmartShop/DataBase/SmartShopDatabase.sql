USE [master]
GO
/****** Object:  Database [SmartShop]    Script Date: 10/27/2022 4:12:11 PM ******/
CREATE DATABASE [SmartShop]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SmartShop', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SmartShop.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SmartShop_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SmartShop_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SmartShop] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SmartShop].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SmartShop] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SmartShop] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SmartShop] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SmartShop] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SmartShop] SET ARITHABORT OFF 
GO
ALTER DATABASE [SmartShop] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SmartShop] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SmartShop] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SmartShop] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SmartShop] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SmartShop] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SmartShop] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SmartShop] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SmartShop] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SmartShop] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SmartShop] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SmartShop] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SmartShop] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SmartShop] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SmartShop] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SmartShop] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SmartShop] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SmartShop] SET RECOVERY FULL 
GO
ALTER DATABASE [SmartShop] SET  MULTI_USER 
GO
ALTER DATABASE [SmartShop] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SmartShop] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SmartShop] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SmartShop] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SmartShop] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SmartShop] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SmartShop', N'ON'
GO
ALTER DATABASE [SmartShop] SET QUERY_STORE = OFF
GO
USE [SmartShop]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccName] [nvarchar](max) NULL,
	[AccPhone] [nvarchar](11) NULL,
	[AccAddress] [nvarchar](max) NULL,
	[AccType] [nvarchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CatName] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerPayments]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerPayments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccId] [int] NULL,
	[Amount] [float] NULL,
	[Note] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EName] [nvarchar](max) NULL,
	[Phone] [nvarchar](11) NULL,
	[Address] [nvarchar](max) NULL,
	[Salary] [float] NULL,
	[Job] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeesWithdraws]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeesWithdraws](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmpId] [int] NULL,
	[Date] [datetime] NULL,
	[Amount] [float] NULL,
	[Note] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expencess]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expencess](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[Amount] [float] NULL,
	[Note] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Items]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CatId] [int] NULL,
	[ItemName] [nvarchar](max) NULL,
	[Barcode] [nvarchar](max) NULL,
	[Company] [nvarchar](max) NULL,
	[PricePur] [float] NULL,
	[PriceSell] [float] NULL,
	[FirstBalance] [float] NULL,
	[FirstBalancePrice] [float] NULL,
	[Description] [nvarchar](max) NULL,
	[Img] [image] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jops]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jops](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JopName] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[AccId] [int] NULL,
	[StkId] [int] NULL,
	[ShiftId] [int] NULL,
	[UserId] [int] NULL,
	[IsCash] [bit] NULL,
	[Total] [float] NULL,
	[Descount] [float] NULL,
	[Final] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseDetails]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvId] [int] NULL,
	[ItemId] [int] NULL,
	[Price] [float] NULL,
	[Count] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseRe]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseRe](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[AccId] [int] NULL,
	[StkId] [int] NULL,
	[ShiftId] [int] NULL,
	[UserId] [int] NULL,
	[IsCash] [bit] NULL,
	[Total] [float] NULL,
	[Descount] [float] NULL,
	[Final] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseReDetails]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseReDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvId] [int] NULL,
	[ItemId] [int] NULL,
	[Price] [float] NULL,
	[Count] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sales]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[AccId] [int] NULL,
	[StkId] [int] NULL,
	[ShiftId] [int] NULL,
	[UserId] [int] NULL,
	[IsCash] [bit] NULL,
	[Total] [float] NULL,
	[Descount] [float] NULL,
	[Final] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesDetails]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvId] [int] NULL,
	[ItemId] [int] NULL,
	[Price] [float] NULL,
	[Count] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesRe]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesRe](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[AccId] [int] NULL,
	[StkId] [int] NULL,
	[ShiftId] [int] NULL,
	[UserId] [int] NULL,
	[IsCash] [bit] NULL,
	[Total] [float] NULL,
	[Descount] [float] NULL,
	[Final] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SalesReDetails]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesReDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvId] [int] NULL,
	[ItemId] [int] NULL,
	[Price] [float] NULL,
	[Count] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shifts]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shifts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Useropen] [int] NULL,
	[UserClose] [int] NULL,
	[DateOpen] [datetime] NULL,
	[DateClose] [datetime] NULL,
	[AmountOpen] [float] NULL,
	[AmountClose] [float] NULL,
	[IsCloses] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Stores]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreName] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreTransfer]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreTransfer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[StkFromId] [int] NULL,
	[StkToId] [int] NULL,
	[ShiftId] [int] NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoreTransferDetails]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreTransferDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransId] [int] NULL,
	[ItemId] [int] NULL,
	[Count] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierPayments]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierPayments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccId] [int] NULL,
	[Amount] [float] NULL,
	[Note] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
	[UserPassword] [nvarchar](max) NULL,
	[UserScreens] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerPayments]  WITH CHECK ADD FOREIGN KEY([AccId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[EmployeesWithdraws]  WITH CHECK ADD FOREIGN KEY([EmpId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[Items]  WITH CHECK ADD FOREIGN KEY([CatId])
REFERENCES [dbo].[Categories] ([Id])
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD FOREIGN KEY([AccId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD FOREIGN KEY([ShiftId])
REFERENCES [dbo].[Shifts] ([Id])
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD FOREIGN KEY([StkId])
REFERENCES [dbo].[Stores] ([Id])
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[PurchaseDetails]  WITH CHECK ADD FOREIGN KEY([InvId])
REFERENCES [dbo].[Sales] ([Id])
GO
ALTER TABLE [dbo].[PurchaseDetails]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([Id])
GO
ALTER TABLE [dbo].[PurchaseRe]  WITH CHECK ADD FOREIGN KEY([AccId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[PurchaseRe]  WITH CHECK ADD FOREIGN KEY([ShiftId])
REFERENCES [dbo].[Shifts] ([Id])
GO
ALTER TABLE [dbo].[PurchaseRe]  WITH CHECK ADD FOREIGN KEY([StkId])
REFERENCES [dbo].[Stores] ([Id])
GO
ALTER TABLE [dbo].[PurchaseRe]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[PurchaseReDetails]  WITH CHECK ADD FOREIGN KEY([InvId])
REFERENCES [dbo].[Sales] ([Id])
GO
ALTER TABLE [dbo].[PurchaseReDetails]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([Id])
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD FOREIGN KEY([AccId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD FOREIGN KEY([ShiftId])
REFERENCES [dbo].[Shifts] ([Id])
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD FOREIGN KEY([StkId])
REFERENCES [dbo].[Stores] ([Id])
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SalesDetails]  WITH CHECK ADD FOREIGN KEY([InvId])
REFERENCES [dbo].[Sales] ([Id])
GO
ALTER TABLE [dbo].[SalesDetails]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([Id])
GO
ALTER TABLE [dbo].[SalesRe]  WITH CHECK ADD FOREIGN KEY([AccId])
REFERENCES [dbo].[Accounts] ([Id])
GO
ALTER TABLE [dbo].[SalesRe]  WITH CHECK ADD FOREIGN KEY([ShiftId])
REFERENCES [dbo].[Shifts] ([Id])
GO
ALTER TABLE [dbo].[SalesRe]  WITH CHECK ADD FOREIGN KEY([StkId])
REFERENCES [dbo].[Stores] ([Id])
GO
ALTER TABLE [dbo].[SalesRe]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[SalesReDetails]  WITH CHECK ADD FOREIGN KEY([InvId])
REFERENCES [dbo].[Sales] ([Id])
GO
ALTER TABLE [dbo].[SalesReDetails]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([Id])
GO
ALTER TABLE [dbo].[StoreTransfer]  WITH CHECK ADD FOREIGN KEY([ShiftId])
REFERENCES [dbo].[Shifts] ([Id])
GO
ALTER TABLE [dbo].[StoreTransfer]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[StoreTransferDetails]  WITH CHECK ADD FOREIGN KEY([ItemId])
REFERENCES [dbo].[Items] ([Id])
GO
ALTER TABLE [dbo].[StoreTransferDetails]  WITH CHECK ADD FOREIGN KEY([TransId])
REFERENCES [dbo].[StoreTransfer] ([Id])
GO
ALTER TABLE [dbo].[SupplierPayments]  WITH CHECK ADD FOREIGN KEY([AccId])
REFERENCES [dbo].[Accounts] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[Get_PersonAccountCredit]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Get_PersonAccountCredit] (@acc_id int)
as
SELECT In_P_tot,Out_P_tot,Pur_tot,PurRe_tot,Sal_tot,SalRe_tot,
            round((COALESCE(In_P_tot,0)+COALESCE(Pur_tot,0)+COALESCE(SalRe_tot,0))-(COALESCE(Out_P_tot,0)+COALESCE(PurRe_tot,0)+COALESCE(Sal_tot,0)),2) as final 
            FROM (select sum(Amount) as In_P_tot from CustomerPayments where AccId=@acc_id
             )t1 cross join(select sum(Amount) as Out_P_tot from SupplierPayments where AccId=@acc_id 
             )t4 cross join(select sum(Final) as Sal_tot from Sales where IsCash='false' and AccId=@acc_id
             )t5 cross join(select sum(Final) as SalRe_tot from SalesRe where IsCash='false' and AccId=@acc_id 
             )t6 cross join(select sum(Final) as Pur_tot from Purchase where IsCash='false' and AccId=@acc_id 
             )t7 cross join(select sum(Final) as PurRe_tot from PurchaseRe where IsCash='false' and AccId=@acc_id 
             
             )t10
GO
/****** Object:  StoredProcedure [dbo].[Get_PersonAccountCredit_before]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Get_PersonAccountCredit_before] (@acc_id int,@BeforeDte datetime)
as
SELECT In_P_tot,Out_P_tot,Pur_tot,PurRe_tot,Sal_tot,SalRe_tot,
            round((COALESCE(In_P_tot,0)+COALESCE(Pur_tot,0)+COALESCE(SalRe_tot,0))-(COALESCE(Out_P_tot,0)+COALESCE(PurRe_tot,0)+COALESCE(Sal_tot,0)),2) as final 
            FROM (select sum(Amount) as In_P_tot from CustomerPayments where  AccId=@acc_id and Date < @BeforeDte
             )t1 cross join(select sum(Amount) as Out_P_tot from SupplierPayments where  AccId=@acc_id and Date < @BeforeDte
             )t2 cross join(select sum(Final) as Sal_tot from Sales where IsCash='false' and AccId=@acc_id and Date < @BeforeDte
             )t5 cross join(select sum(Final) as SalRe_tot from SalesRe where IsCash='false' and AccId=@acc_id and Date < @BeforeDte
             )t6 cross join(select sum(Final) as Pur_tot from Purchase where IsCash='false' and AccId=@acc_id and Date < @BeforeDte
             )t7 cross join(select sum(Final) as PurRe_tot from PurchaseRe where IsCash='false' and AccId=@acc_id and Date < @BeforeDte
             )t8 
GO
/****** Object:  StoredProcedure [dbo].[GetAccountStatement]    Script Date: 10/27/2022 4:12:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[GetAccountStatement] @acc_id int,@dte_f datetime,@dte_t datetime
as
select * from (
        (select Date [date],Final [creditActions],'0' [debitActions],'0'[balanceAfter],'0'[balanceType] ,'فاتورة مشتريات رقم ' +convert(nvarchar,Id) [notes],'pu' [typ] from Purchase where AccId  = @acc_id  and IsCash = 0)
        union all
        (select Date [date],'0' [creditActions],Final [debitActions],'0'[balanceAfter],'0'[balanceType] ,'فاتورة مرتجع مشتريات رقم ' +convert(nvarchar,Id) [notes],'pu_re' [typ]  from PurchaseRe where AccId  =  @acc_id and IsCash = 0) union
        (select Date [date],'0' [creditActions],Final [debitActions],'0'[balanceAfter],'0'[balanceType] ,'فاتورة مبيعات رقم ' +convert(nvarchar,Id) [notes],'sa' [typ] from Sales where AccId  = @acc_id and IsCash = 0)union
        (select Date [date],Final [creditActions] ,'0'[debitActions],'0'[balanceAfter],'0'[balanceType],'فاتورة مرتجع مبيعات رقم ' +convert(nvarchar,Id) [notes],'sa_re' [typ] from SalesRe where AccId  =  @acc_id and IsCash = 0) union
  
(select Date [date],Amount  [creditActions] ,'0'[debitActions],'0'[balanceAfter],'0'[balanceType],N'تحصيل نقدى ' +convert(nvarchar,Id)[notes]  ,'in_py' [typ] from CustomerPayments  where AccId  =  @acc_id  and isnull(Amount,0)>0 ) union
      
(select Date [date],'0' [creditActions],Amount  [debitActions],'0'[balanceAfter],'0'[balanceType],N'سداد نقدى ' +convert(nvarchar,Id) [notes] ,'ot_py' [typ] from SupplierPayments  where AccId  =  @acc_id  and isnull(Amount,0)>0) 


)qw where qw.date between @dte_f and @dte_t order by qw.date
GO
USE [master]
GO
ALTER DATABASE [SmartShop] SET  READ_WRITE 
GO
