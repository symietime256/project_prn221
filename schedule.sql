USE [master]
GO
/****** Object:  Database [PRN221Project]    Script Date: 3/1/2024 12:09:04 PM ******/
CREATE DATABASE [PRN221Project]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PRN221Project', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PRN221Project.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PRN221Project_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\PRN221Project_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [PRN221Project] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PRN221Project].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PRN221Project] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PRN221Project] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PRN221Project] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PRN221Project] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PRN221Project] SET ARITHABORT OFF 
GO
ALTER DATABASE [PRN221Project] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PRN221Project] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PRN221Project] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PRN221Project] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PRN221Project] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PRN221Project] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PRN221Project] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PRN221Project] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PRN221Project] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PRN221Project] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PRN221Project] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PRN221Project] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PRN221Project] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PRN221Project] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PRN221Project] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PRN221Project] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PRN221Project] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PRN221Project] SET RECOVERY FULL 
GO
ALTER DATABASE [PRN221Project] SET  MULTI_USER 
GO
ALTER DATABASE [PRN221Project] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PRN221Project] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PRN221Project] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PRN221Project] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PRN221Project] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PRN221Project] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'PRN221Project', N'ON'
GO
ALTER DATABASE [PRN221Project] SET QUERY_STORE = ON
GO
ALTER DATABASE [PRN221Project] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [PRN221Project]
GO
/****** Object:  Table [dbo].[RoomBuilding]    Script Date: 3/1/2024 12:09:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomBuilding](
	[BuildingType] [nvarchar](10) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[NumberOfRooms] [int] NOT NULL,
 CONSTRAINT [PK_RoomBuilding] PRIMARY KEY CLUSTERED 
(
	[BuildingType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 3/1/2024 12:09:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[Id] [nvarchar](10) NOT NULL,
	[ClassId] [nvarchar](7) NOT NULL,
	[SubjectId] [nvarchar](7) NOT NULL,
	[Room] [nvarchar](7) NOT NULL,
	[Teacher] [nvarchar](10) NOT NULL,
	[SlotId] [nvarchar](3) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Season] [nvarchar](10) NOT NULL,
	[Year] [int] NOT NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 3/1/2024 12:09:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectId] [nvarchar](7) NOT NULL,
	[SubjectName] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](350) NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 3/1/2024 12:09:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[TeacherID] [nvarchar](10) NOT NULL,
	[TeacherName] [nvarchar](50) NOT NULL,
	[Rating] [float] NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[TeacherID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeSlot]    Script Date: 3/1/2024 12:09:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSlot](
	[SlotTimeId] [int] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
 CONSTRAINT [PK_TimeSlot] PRIMARY KEY CLUSTERED 
(
	[SlotTimeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UniversityClass]    Script Date: 3/1/2024 12:09:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UniversityClass](
	[ClassId] [nvarchar](7) NOT NULL,
	[Description] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[RoomBuilding] ([BuildingType], [Description], [NumberOfRooms]) VALUES (N'AL', N'Alpha Building', 40)
INSERT [dbo].[RoomBuilding] ([BuildingType], [Description], [NumberOfRooms]) VALUES (N'BE', N'Beta Building', 75)
INSERT [dbo].[RoomBuilding] ([BuildingType], [Description], [NumberOfRooms]) VALUES (N'DE', N'Delta Building', 50)
INSERT [dbo].[RoomBuilding] ([BuildingType], [Description], [NumberOfRooms]) VALUES (N'GA', N'Gamma', 60)
GO
INSERT [dbo].[Schedule] ([Id], [ClassId], [SubjectId], [Room], [Teacher], [SlotId], [DateCreated], [Season], [Year]) VALUES (N'1', N'SE1704', N'PRN211', N'DE-110', N'ChiLP', N'A24', CAST(N'2023-12-29T00:00:00.000' AS DateTime), N'Spring', 2024)
INSERT [dbo].[Schedule] ([Id], [ClassId], [SubjectId], [Room], [Teacher], [SlotId], [DateCreated], [Season], [Year]) VALUES (N'2', N'SE1705', N'PRU212', N'DE-105', N'KhuongPD', N'A46', CAST(N'2023-12-29T00:00:00.000' AS DateTime), N'Spring', 2024)
GO
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [Description]) VALUES (N'EXE101', N'Experiential Entrepreneurship 1', N'Cute')
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [Description]) VALUES (N'PRM392', N'Mobile Programming', N'Cute')
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [Description]) VALUES (N'PRN211', N'Advanced Cross-Platform Application Programming With .NET', N'Cute')
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [Description]) VALUES (N'PRU212', N'C# Programming and Unity_Lập trình C# và Unity', N'Cute')
GO
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Rating]) VALUES (N'BanTQ', N'Tran Quy Ban', 1)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Rating]) VALUES (N'ChiLP', N'Le Phuong Chi', 5)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Rating]) VALUES (N'KhuongPD', N'Phung Duy Khuong ', 2)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Rating]) VALUES (N'SonNT', N'Ngo Tung Son', 3)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Rating]) VALUES (N'TuanVM', N'Vu Minh Tuan', 1)
GO
INSERT [dbo].[UniversityClass] ([ClassId], [Description]) VALUES (N'MC1654', N'Media Class')
INSERT [dbo].[UniversityClass] ([ClassId], [Description]) VALUES (N'MC1705', N'Media Class')
INSERT [dbo].[UniversityClass] ([ClassId], [Description]) VALUES (N'SE1701', N'Technology Class')
INSERT [dbo].[UniversityClass] ([ClassId], [Description]) VALUES (N'SE1702', N'Technology Class')
INSERT [dbo].[UniversityClass] ([ClassId], [Description]) VALUES (N'SE1703', N'Technology Class')
INSERT [dbo].[UniversityClass] ([ClassId], [Description]) VALUES (N'SE1704', N'Technology Class')
INSERT [dbo].[UniversityClass] ([ClassId], [Description]) VALUES (N'SE1705', N'Technology Class')
INSERT [dbo].[UniversityClass] ([ClassId], [Description]) VALUES (N'SE1706', N'Technology Class')
INSERT [dbo].[UniversityClass] ([ClassId], [Description]) VALUES (N'SE1707', N'Technology Class')
INSERT [dbo].[UniversityClass] ([ClassId], [Description]) VALUES (N'SE1708', N'Technology Class')
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Class] FOREIGN KEY([ClassId])
REFERENCES [dbo].[UniversityClass] ([ClassId])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Class]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Subject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[Subject] ([SubjectId])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Subject]
GO
ALTER TABLE [dbo].[Schedule]  WITH CHECK ADD  CONSTRAINT [FK_Schedule_Teacher] FOREIGN KEY([Teacher])
REFERENCES [dbo].[Teacher] ([TeacherID])
GO
ALTER TABLE [dbo].[Schedule] CHECK CONSTRAINT [FK_Schedule_Teacher]
GO
USE [master]
GO
ALTER DATABASE [PRN221Project] SET  READ_WRITE 
GO
