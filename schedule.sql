USE [master]
GO
/****** Object:  Database [PRN221Project]    Script Date: 3/13/2024 5:31:14 PM ******/
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
/****** Object:  Table [dbo].[CourseSession]    Script Date: 3/13/2024 5:31:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseSession](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[SessionId] [int] NOT NULL,
	[SessionDate] [date] NOT NULL,
	[Teacher] [nvarchar](10) NOT NULL,
	[Room] [nvarchar](7) NOT NULL,
	[Slot] [int] NOT NULL,
 CONSTRAINT [PK_CourseSession] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schedule]    Script Date: 3/13/2024 5:31:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schedule](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [nvarchar](7) NOT NULL,
	[SubjectId] [nvarchar](7) NOT NULL,
	[Room] [nvarchar](7) NOT NULL,
	[Teacher] [nvarchar](10) NOT NULL,
	[SlotId] [nvarchar](3) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[Season] [nvarchar](10) NULL,
	[Year] [int] NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[TypeOfSlot] [nvarchar](20) NOT NULL,
	[Slot1] [int] NOT NULL,
	[Slot2] [int] NOT NULL,
	[hasSessionYet] [bit] NOT NULL,
 CONSTRAINT [PK_Schedule] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 3/13/2024 5:31:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectId] [nvarchar](7) NOT NULL,
	[SubjectName] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](350) NOT NULL,
	[NumberOfSessions] [int] NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 3/13/2024 5:31:15 PM ******/
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
/****** Object:  Table [dbo].[TimeSlot]    Script Date: 3/13/2024 5:31:15 PM ******/
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
/****** Object:  Table [dbo].[UniversityClass]    Script Date: 3/13/2024 5:31:15 PM ******/
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
SET IDENTITY_INSERT [dbo].[CourseSession] ON 

INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (1, 1, 1, CAST(N'2024-01-01' AS Date), N'ChiLP', N'DE-110', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (2, 1, 2, CAST(N'2024-01-03' AS Date), N'ChiLP', N'DE-110', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (3, 1, 3, CAST(N'2024-01-08' AS Date), N'ChiLP', N'DE-110', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (4, 1, 4, CAST(N'2024-01-10' AS Date), N'ChiLP', N'DE-110', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (5, 1, 5, CAST(N'2024-01-15' AS Date), N'ChiLP', N'DE-110', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (6, 1, 6, CAST(N'2024-01-17' AS Date), N'ChiLP', N'DE-110', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (7, 1, 7, CAST(N'2024-01-22' AS Date), N'ChiLP', N'DE-110', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (8, 1, 8, CAST(N'2024-01-24' AS Date), N'ChiLP', N'DE-110', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (9, 1, 9, CAST(N'2024-01-29' AS Date), N'ChiLP', N'DE-110', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (10, 1, 10, CAST(N'2024-01-31' AS Date), N'ChiLP', N'DE-110', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (11, 1, 11, CAST(N'2024-02-05' AS Date), N'ChiLP', N'DE-110', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (12, 1, 12, CAST(N'2024-02-07' AS Date), N'ChiLP', N'DE-110', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (13, 1, 13, CAST(N'2024-02-12' AS Date), N'ChiLP', N'DE-110', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (14, 1, 14, CAST(N'2024-02-14' AS Date), N'ChiLP', N'DE-110', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (15, 1, 15, CAST(N'2024-02-19' AS Date), N'ChiLP', N'DE-110', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (16, 1, 16, CAST(N'2024-02-21' AS Date), N'ChiLP', N'DE-110', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (17, 1, 17, CAST(N'2024-02-26' AS Date), N'ChiLP', N'DE-110', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (18, 1, 18, CAST(N'2024-02-28' AS Date), N'ChiLP', N'DE-110', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (19, 1, 19, CAST(N'2024-03-04' AS Date), N'ChiLP', N'DE-110', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (20, 1, 20, CAST(N'2024-03-06' AS Date), N'ChiLP', N'DE-110', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (41, 2, 1, CAST(N'2024-01-03' AS Date), N'KhuongPD', N'DE-105', 5)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (42, 2, 2, CAST(N'2024-01-05' AS Date), N'KhuongPD', N'DE-105', 6)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (43, 2, 3, CAST(N'2024-01-10' AS Date), N'KhuongPD', N'DE-105', 5)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (44, 2, 4, CAST(N'2024-01-12' AS Date), N'KhuongPD', N'DE-105', 6)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (45, 2, 5, CAST(N'2024-01-17' AS Date), N'KhuongPD', N'DE-105', 5)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (46, 2, 6, CAST(N'2024-01-19' AS Date), N'KhuongPD', N'DE-105', 6)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (47, 2, 7, CAST(N'2024-01-24' AS Date), N'KhuongPD', N'DE-105', 5)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (48, 2, 8, CAST(N'2024-01-26' AS Date), N'KhuongPD', N'DE-105', 6)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (49, 2, 9, CAST(N'2024-01-31' AS Date), N'KhuongPD', N'DE-105', 5)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (50, 2, 10, CAST(N'2024-02-02' AS Date), N'KhuongPD', N'DE-105', 6)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (51, 2, 11, CAST(N'2024-02-07' AS Date), N'KhuongPD', N'DE-105', 5)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (52, 2, 12, CAST(N'2024-02-09' AS Date), N'KhuongPD', N'DE-105', 6)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (53, 2, 13, CAST(N'2024-02-14' AS Date), N'KhuongPD', N'DE-105', 5)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (54, 2, 14, CAST(N'2024-02-16' AS Date), N'KhuongPD', N'DE-105', 6)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (55, 2, 15, CAST(N'2024-02-21' AS Date), N'KhuongPD', N'DE-105', 5)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (56, 2, 16, CAST(N'2024-02-23' AS Date), N'KhuongPD', N'DE-105', 6)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (57, 2, 17, CAST(N'2024-02-28' AS Date), N'KhuongPD', N'DE-105', 5)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (58, 2, 18, CAST(N'2024-03-01' AS Date), N'KhuongPD', N'DE-105', 6)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (59, 2, 19, CAST(N'2024-03-06' AS Date), N'KhuongPD', N'DE-105', 5)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (60, 2, 20, CAST(N'2024-03-08' AS Date), N'KhuongPD', N'DE-105', 6)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (61, 6, 1, CAST(N'2024-01-02' AS Date), N'KhuongPD', N'BE-113', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (62, 6, 2, CAST(N'2024-01-05' AS Date), N'KhuongPD', N'BE-113', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (63, 6, 3, CAST(N'2024-01-09' AS Date), N'KhuongPD', N'BE-113', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (64, 6, 4, CAST(N'2024-01-12' AS Date), N'KhuongPD', N'BE-113', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (65, 6, 5, CAST(N'2024-01-16' AS Date), N'KhuongPD', N'BE-113', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (66, 6, 6, CAST(N'2024-01-19' AS Date), N'KhuongPD', N'BE-113', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (67, 6, 7, CAST(N'2024-01-23' AS Date), N'KhuongPD', N'BE-113', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (68, 6, 8, CAST(N'2024-01-26' AS Date), N'KhuongPD', N'BE-113', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (69, 6, 9, CAST(N'2024-01-30' AS Date), N'KhuongPD', N'BE-113', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (70, 6, 10, CAST(N'2024-02-02' AS Date), N'KhuongPD', N'BE-113', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (71, 6, 11, CAST(N'2024-02-06' AS Date), N'KhuongPD', N'BE-113', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (72, 6, 12, CAST(N'2024-02-09' AS Date), N'KhuongPD', N'BE-113', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (73, 6, 13, CAST(N'2024-02-13' AS Date), N'KhuongPD', N'BE-113', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (74, 6, 14, CAST(N'2024-02-16' AS Date), N'KhuongPD', N'BE-113', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (75, 6, 15, CAST(N'2024-02-20' AS Date), N'KhuongPD', N'BE-113', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (76, 6, 16, CAST(N'2024-02-23' AS Date), N'KhuongPD', N'BE-113', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (77, 6, 17, CAST(N'2024-02-27' AS Date), N'KhuongPD', N'BE-113', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (78, 6, 18, CAST(N'2024-03-01' AS Date), N'KhuongPD', N'BE-113', 2)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (79, 6, 19, CAST(N'2024-03-05' AS Date), N'KhuongPD', N'BE-113', 1)
INSERT [dbo].[CourseSession] ([Id], [CourseId], [SessionId], [SessionDate], [Teacher], [Room], [Slot]) VALUES (80, 6, 20, CAST(N'2024-03-08' AS Date), N'KhuongPD', N'BE-113', 2)
SET IDENTITY_INSERT [dbo].[CourseSession] OFF
GO
SET IDENTITY_INSERT [dbo].[Schedule] ON 

INSERT [dbo].[Schedule] ([Id], [ClassId], [SubjectId], [Room], [Teacher], [SlotId], [DateCreated], [Season], [Year], [StartDate], [EndDate], [TypeOfSlot], [Slot1], [Slot2], [hasSessionYet]) VALUES (1, N'SE1704', N'PRN211', N'DE-110', N'ChiLP', N'A24', CAST(N'2023-12-29T00:00:00.000' AS DateTime), N'Spring', 2024, CAST(N'2024-01-01T00:00:00.000' AS DateTime), CAST(N'2024-03-23T00:00:00.000' AS DateTime), N'Morning', 2, 4, 1)
INSERT [dbo].[Schedule] ([Id], [ClassId], [SubjectId], [Room], [Teacher], [SlotId], [DateCreated], [Season], [Year], [StartDate], [EndDate], [TypeOfSlot], [Slot1], [Slot2], [hasSessionYet]) VALUES (2, N'SE1705', N'PRU212', N'DE-105', N'KhuongPD', N'C46', CAST(N'2023-12-29T00:00:00.000' AS DateTime), N'Spring', 2024, CAST(N'2024-01-03T00:00:00.000' AS DateTime), CAST(N'2024-03-22T00:00:00.000' AS DateTime), N'Evening', 4, 6, 1)
INSERT [dbo].[Schedule] ([Id], [ClassId], [SubjectId], [Room], [Teacher], [SlotId], [DateCreated], [Season], [Year], [StartDate], [EndDate], [TypeOfSlot], [Slot1], [Slot2], [hasSessionYet]) VALUES (6, N'SE1702', N'PRN221', N'BE-113', N'KhuongPD', N'A36', CAST(N'2024-03-13T17:23:19.863' AS DateTime), N'Spring', 2024, CAST(N'2024-01-01T00:00:00.000' AS DateTime), CAST(N'2024-03-23T00:00:00.000' AS DateTime), N'Morning', 3, 6, 1)
SET IDENTITY_INSERT [dbo].[Schedule] OFF
GO
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [Description], [NumberOfSessions]) VALUES (N'CSD201', N'Data Structure And Algorithms', N'haha', 20)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [Description], [NumberOfSessions]) VALUES (N'DBI201', N'Database Management', N'ajsldjflkasdjfkl', 20)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [Description], [NumberOfSessions]) VALUES (N'EXE101', N'Experiential Entrepreneurship 1', N'Cute', 16)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [Description], [NumberOfSessions]) VALUES (N'PRF192', N'Fundamental C Programming', N'C# everyday', 20)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [Description], [NumberOfSessions]) VALUES (N'PRM392', N'Mobile Programming', N'Cute', 20)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [Description], [NumberOfSessions]) VALUES (N'PRN211', N'Advanced Cross-Platform Application Programming With .NET', N'Cute', 22)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [Description], [NumberOfSessions]) VALUES (N'PRN221', N'C# Advanced', N'Cute', 20)
INSERT [dbo].[Subject] ([SubjectId], [SubjectName], [Description], [NumberOfSessions]) VALUES (N'PRU212', N'C# Programming and Unity_Lập trình C# và Unity', N'Cute', 20)
GO
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Rating]) VALUES (N'BanTQ', N'Tran Quy Ban', 1)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Rating]) VALUES (N'ChiLP', N'Le Phuong Chi', 5)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Rating]) VALUES (N'KhuongPD', N'Phung Duy Khuong ', 2)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Rating]) VALUES (N'SonNT', N'Ngo Tung Son', 3)
INSERT [dbo].[Teacher] ([TeacherID], [TeacherName], [Rating]) VALUES (N'TuanVM', N'Vu Minh Tuan', 1)
GO
INSERT [dbo].[TimeSlot] ([SlotTimeId], [StartTime], [EndTime]) VALUES (1, CAST(N'07:30:00' AS Time), CAST(N'09:50:00' AS Time))
INSERT [dbo].[TimeSlot] ([SlotTimeId], [StartTime], [EndTime]) VALUES (2, CAST(N'10:00:00' AS Time), CAST(N'12:20:00' AS Time))
INSERT [dbo].[TimeSlot] ([SlotTimeId], [StartTime], [EndTime]) VALUES (3, CAST(N'12:50:00' AS Time), CAST(N'15:10:00' AS Time))
INSERT [dbo].[TimeSlot] ([SlotTimeId], [StartTime], [EndTime]) VALUES (4, CAST(N'15:20:00' AS Time), CAST(N'17:40:00' AS Time))
INSERT [dbo].[TimeSlot] ([SlotTimeId], [StartTime], [EndTime]) VALUES (5, CAST(N'18:00:00' AS Time), CAST(N'20:20:00' AS Time))
INSERT [dbo].[TimeSlot] ([SlotTimeId], [StartTime], [EndTime]) VALUES (6, CAST(N'20:00:00' AS Time), CAST(N'22:20:00' AS Time))
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
ALTER TABLE [dbo].[CourseSession]  WITH CHECK ADD  CONSTRAINT [FK_CourseSession_Schedule] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Schedule] ([Id])
GO
ALTER TABLE [dbo].[CourseSession] CHECK CONSTRAINT [FK_CourseSession_Schedule]
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
