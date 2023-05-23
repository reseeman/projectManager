USE [master]
GO
/****** Object:  Database [projdb]    Script Date: 27.01.2022 6:40:52 ******/
CREATE DATABASE [projdb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'projdb', FILENAME = N'D:\Programs\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\projdb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'projdb_log', FILENAME = N'D:\Programs\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\projdb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [projdb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [projdb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [projdb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [projdb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [projdb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [projdb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [projdb] SET ARITHABORT OFF 
GO
ALTER DATABASE [projdb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [projdb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [projdb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [projdb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [projdb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [projdb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [projdb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [projdb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [projdb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [projdb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [projdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [projdb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [projdb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [projdb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [projdb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [projdb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [projdb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [projdb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [projdb] SET  MULTI_USER 
GO
ALTER DATABASE [projdb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [projdb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [projdb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [projdb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [projdb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [projdb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [projdb] SET QUERY_STORE = OFF
GO
USE [projdb]
GO
/****** Object:  Table [dbo].[projects]    Script Date: 27.01.2022 6:40:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[projects](
	[idProject] [int] IDENTITY(1,1) NOT NULL,
	[nameProject] [nvarchar](50) NOT NULL,
	[dateStart] [date] NOT NULL,
	[dateFinish] [date] NOT NULL,
	[projectPeriodDays] [int] NOT NULL,
	[description] [nvarchar](50) NOT NULL,
	[isProjectReady] [bit] NOT NULL,
 CONSTRAINT [PK_projects] PRIMARY KEY CLUSTERED 
(
	[idProject] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[status]    Script Date: 27.01.2022 6:40:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[status](
	[idStatus] [int] IDENTITY(1,1) NOT NULL,
	[nameStatus] [nvarchar](9) NOT NULL,
 CONSTRAINT [PK_isReady] PRIMARY KEY CLUSTERED 
(
	[idStatus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tasks]    Script Date: 27.01.2022 6:40:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tasks](
	[idTask] [int] IDENTITY(1,1) NOT NULL,
	[nameTask] [nvarchar](50) NOT NULL,
	[taskStart] [date] NOT NULL,
	[taskFinish] [date] NOT NULL,
	[taskPeriodDays] [int] NOT NULL,
	[idWorker] [int] NOT NULL,
	[idProject] [int] NOT NULL,
 CONSTRAINT [PK_tasks] PRIMARY KEY CLUSTERED 
(
	[idTask] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[workers]    Script Date: 27.01.2022 6:40:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[workers](
	[idWorker] [int] IDENTITY(1,1) NOT NULL,
	[nameWorker] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_workers] PRIMARY KEY CLUSTERED 
(
	[idWorker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[projects] ON 

INSERT [dbo].[projects] ([idProject], [nameProject], [dateStart], [dateFinish], [projectPeriodDays], [description], [isProjectReady]) VALUES (1010, N'Разработка на Wordpress', CAST(N'2022-01-17' AS Date), CAST(N'2022-01-23' AS Date), 6, N'Разработка блога на Wordpress', 1)
INSERT [dbo].[projects] ([idProject], [nameProject], [dateStart], [dateFinish], [projectPeriodDays], [description], [isProjectReady]) VALUES (1016, N'Разработка на Joomla', CAST(N'2022-01-03' AS Date), CAST(N'2022-01-27' AS Date), 24, N'Разработка интернет магазина', 0)
SET IDENTITY_INSERT [dbo].[projects] OFF
GO
SET IDENTITY_INSERT [dbo].[status] ON 

INSERT [dbo].[status] ([idStatus], [nameStatus]) VALUES (3, N'Готово')
INSERT [dbo].[status] ([idStatus], [nameStatus]) VALUES (4, N'Не готово')
SET IDENTITY_INSERT [dbo].[status] OFF
GO
SET IDENTITY_INSERT [dbo].[tasks] ON 

INSERT [dbo].[tasks] ([idTask], [nameTask], [taskStart], [taskFinish], [taskPeriodDays], [idWorker], [idProject]) VALUES (1006, N'Вёрстка в Figma', CAST(N'2022-01-18' AS Date), CAST(N'2022-01-20' AS Date), 2, 7, 1010)
INSERT [dbo].[tasks] ([idTask], [nameTask], [taskStart], [taskFinish], [taskPeriodDays], [idWorker], [idProject]) VALUES (1007, N'Поиск изображений', CAST(N'2022-01-03' AS Date), CAST(N'2022-01-04' AS Date), 1, 4, 1016)
SET IDENTITY_INSERT [dbo].[tasks] OFF
GO
SET IDENTITY_INSERT [dbo].[workers] ON 

INSERT [dbo].[workers] ([idWorker], [nameWorker]) VALUES (1, N'Беляков Михаил Матвеевич')
INSERT [dbo].[workers] ([idWorker], [nameWorker]) VALUES (2, N'Тимофеева Анастасия Алексеевна')
INSERT [dbo].[workers] ([idWorker], [nameWorker]) VALUES (3, N'Воробьев Дмитрий Дмитриевич')
INSERT [dbo].[workers] ([idWorker], [nameWorker]) VALUES (4, N'Степанова Валерия Алексеевна')
INSERT [dbo].[workers] ([idWorker], [nameWorker]) VALUES (5, N'Колесов Владимир Александрович')
INSERT [dbo].[workers] ([idWorker], [nameWorker]) VALUES (6, N'Соловьева Виктория Георгиевна')
INSERT [dbo].[workers] ([idWorker], [nameWorker]) VALUES (7, N'Попов Ярослав Григорьевич')
INSERT [dbo].[workers] ([idWorker], [nameWorker]) VALUES (8, N'
Севастьянова Александра Владимировна')
SET IDENTITY_INSERT [dbo].[workers] OFF
GO
ALTER TABLE [dbo].[tasks]  WITH CHECK ADD  CONSTRAINT [FK_tasks_projects] FOREIGN KEY([idProject])
REFERENCES [dbo].[projects] ([idProject])
GO
ALTER TABLE [dbo].[tasks] CHECK CONSTRAINT [FK_tasks_projects]
GO
ALTER TABLE [dbo].[tasks]  WITH CHECK ADD  CONSTRAINT [FK_tasks_workers] FOREIGN KEY([idWorker])
REFERENCES [dbo].[workers] ([idWorker])
GO
ALTER TABLE [dbo].[tasks] CHECK CONSTRAINT [FK_tasks_workers]
GO
USE [master]
GO
ALTER DATABASE [projdb] SET  READ_WRITE 
GO
