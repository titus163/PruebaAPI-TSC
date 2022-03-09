USE [master]
GO

/****** Object:  Database [ISO3166DB]    Script Date: 06/03/2022 11:40:56 p. m. ******/
CREATE DATABASE [ISO3166DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ISO3166DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ISO3166DB.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ISO3166DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ISO3166DB_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ISO3166DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [ISO3166DB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [ISO3166DB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [ISO3166DB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [ISO3166DB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [ISO3166DB] SET ARITHABORT OFF 
GO

ALTER DATABASE [ISO3166DB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [ISO3166DB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [ISO3166DB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [ISO3166DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [ISO3166DB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [ISO3166DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [ISO3166DB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [ISO3166DB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [ISO3166DB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [ISO3166DB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [ISO3166DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [ISO3166DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [ISO3166DB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [ISO3166DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [ISO3166DB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [ISO3166DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [ISO3166DB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [ISO3166DB] SET RECOVERY FULL 
GO

ALTER DATABASE [ISO3166DB] SET  MULTI_USER 
GO

ALTER DATABASE [ISO3166DB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [ISO3166DB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [ISO3166DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [ISO3166DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [ISO3166DB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [ISO3166DB] SET QUERY_STORE = OFF
GO

ALTER DATABASE [ISO3166DB] SET  READ_WRITE 
GO


USE [ISO3166DB]
GO

/****** Object:  Table [dbo].[Cities]    Script Date: 06/03/2022 11:41:37 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cities](
	[CityId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](29) NULL,
	[stateId] [int] NULL,
	[latitude] [nvarchar](31) NULL,
	[longitude] [nvarchar](31) NULL,
	[wikiDataId] [nvarchar](28) NULL,
 CONSTRAINT [PK__Cities__F2D21B760B72D6B3] PRIMARY KEY CLUSTERED 
(
	[CityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Countries]    Script Date: 06/03/2022 11:41:37 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Countries](
	[CountryId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](31) NULL,
	[iso3] [nvarchar](23) NULL,
	[iso2] [nvarchar](22) NULL,
	[numeric_code] [nvarchar](23) NULL,
	[phone_code] [nvarchar](22) NULL,
	[capital] [nvarchar](25) NULL,
	[currency] [nvarchar](23) NULL,
	[currency_name] [nvarchar](34) NULL,
	[currency_symbol] [nvarchar](21) NULL,
	[tld] [nvarchar](23) NULL,
	[native] [nvarchar](29) NULL,
	[region] [nvarchar](24) NULL,
	[subregion] [nvarchar](33) NULL,
	[latitude] [nvarchar](31) NULL,
	[longitude] [nvarchar](31) NULL,
	[emoji] [nvarchar](24) NULL,
	[emojiU] [nvarchar](35) NULL,
 CONSTRAINT [PK__AppInsta__662A1D2B99D169EE] PRIMARY KEY CLUSTERED 
(
	[CountryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[States]    Script Date: 06/03/2022 11:41:37 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[States](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](30) NULL,
	[countryId] [int] NULL,
	[state_code] [nvarchar](23) NULL,
	[latitude] [nvarchar](31) NULL,
	[longitude] [nvarchar](31) NULL,
 CONSTRAINT [PK__Sates__C3BA3B3A15DFEBEB] PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[timezones]    Script Date: 06/03/2022 11:41:37 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[timezones](
	[timezonesId] [int] IDENTITY(1,1) NOT NULL,
	[CountryId] [int] NULL,
	[zoneName] [nvarchar](30) NULL,
	[gmtOffset] [int] NULL,
	[gmtOffsetName] [nvarchar](29) NULL,
	[abbreviation] [nvarchar](23) NULL,
	[tzName] [nvarchar](36) NULL,
PRIMARY KEY CLUSTERED 
(
	[timezonesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[translations]    Script Date: 06/03/2022 11:41:37 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[translations](
	[translationsId] [INT] IDENTITY(1,1) NOT NULL,
	[CountryId] [INT] NULL,
	[kr] [NVARCHAR](26) NULL,
	[br] [NVARCHAR](31) NULL,
	[pt] [NVARCHAR](31) NULL,
	[nl] [NVARCHAR](31) NULL,
	[hr] [NVARCHAR](30) NULL,
	[fa] [NVARCHAR](29) NULL,
	[de] [NVARCHAR](31) NULL,
	[es] [NVARCHAR](30) NULL,
	[fr] [NVARCHAR](31) NULL,
	[ja] [NVARCHAR](27) NULL,
	[it] [NVARCHAR](31) NULL,
	[cn] [NVARCHAR](23) NULL,
PRIMARY KEY CLUSTERED 
(
	[translationsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Cities]  WITH CHECK ADD  CONSTRAINT [FK_Cities_Sates] FOREIGN KEY([stateId])
REFERENCES [dbo].[States] ([StateId])
GO

ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_Sates]
GO

ALTER TABLE [dbo].[States]  WITH CHECK ADD  CONSTRAINT [FK_Sates_Countries] FOREIGN KEY([countryId])
REFERENCES [dbo].[Countries] ([CountryId])
GO

ALTER TABLE [dbo].[States] CHECK CONSTRAINT [FK_Sates_Countries]
GO

ALTER TABLE [dbo].[timezones]  WITH CHECK ADD  CONSTRAINT [FK_timezones_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([CountryId])
GO

ALTER TABLE [dbo].[timezones] CHECK CONSTRAINT [FK_timezones_Countries]
GO

ALTER TABLE [dbo].[translations]  WITH CHECK ADD  CONSTRAINT [FK_translations_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([CountryId])
GO

ALTER TABLE [dbo].[translations] CHECK CONSTRAINT [FK_translations_Countries]
GO


