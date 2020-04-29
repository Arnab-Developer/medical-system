USE [master]
GO

CREATE DATABASE [ConsultationDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ConsultationDb', FILENAME = N'' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ConsultationDb_log', FILENAME = N'' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [ConsultationDb] SET COMPATIBILITY_LEVEL = 130
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ConsultationDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ConsultationDb] SET ANSI_NULL_DEFAULT ON 
GO

ALTER DATABASE [ConsultationDb] SET ANSI_NULLS ON 
GO

ALTER DATABASE [ConsultationDb] SET ANSI_PADDING ON 
GO

ALTER DATABASE [ConsultationDb] SET ANSI_WARNINGS ON 
GO

ALTER DATABASE [ConsultationDb] SET ARITHABORT ON 
GO

ALTER DATABASE [ConsultationDb] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ConsultationDb] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ConsultationDb] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ConsultationDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ConsultationDb] SET CURSOR_DEFAULT  LOCAL 
GO

ALTER DATABASE [ConsultationDb] SET CONCAT_NULL_YIELDS_NULL ON 
GO

ALTER DATABASE [ConsultationDb] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ConsultationDb] SET QUOTED_IDENTIFIER ON 
GO

ALTER DATABASE [ConsultationDb] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ConsultationDb] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ConsultationDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ConsultationDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ConsultationDb] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ConsultationDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ConsultationDb] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ConsultationDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ConsultationDb] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ConsultationDb] SET RECOVERY FULL 
GO

ALTER DATABASE [ConsultationDb] SET  MULTI_USER 
GO

ALTER DATABASE [ConsultationDb] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ConsultationDb] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ConsultationDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ConsultationDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [ConsultationDb] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ConsultationDb] SET QUERY_STORE = OFF
GO

USE [ConsultationDb]
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE [ConsultationDb] SET  READ_WRITE 
GO


USE [ConsultationDb]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Doctors](
	[Id] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Patents](
	[Id] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Consultations](
	[Id] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Place_Country] [varchar](50) NOT NULL,
	[Place_State] [varchar](50) NOT NULL,
	[Place_City] [varchar](50) NOT NULL,
	[Place_PinCode] [varchar](50) NOT NULL,
	[Problem] [varchar](50) NOT NULL,
	[Medicine] [varchar](50) NOT NULL,
	[DoctorId] [int] NOT NULL,
	[PatentId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
