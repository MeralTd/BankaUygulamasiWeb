USE [Banka]
GO
/****** Object:  Table [dbo].[Havale]    Script Date: 12.11.2019 23:04:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Havale](
	[HavaleId] [int] IDENTITY(1,1) NOT NULL,
	[Miktar] [decimal](18, 2) NOT NULL,
	[GidenHesapNo] [nvarchar](11) NOT NULL,
	[GelenHesapNo] [nvarchar](11) NOT NULL,
	[MusteriId] [nvarchar](11) NOT NULL,
	[Tarih] [datetime] NULL,
	[Kanal] [nvarchar](6) NULL,
 CONSTRAINT [PK_Havale] PRIMARY KEY CLUSTERED 
(
	[HavaleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hesaplar]    Script Date: 12.11.2019 23:04:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hesaplar](
	[HesapID] [int] IDENTITY(1,1) NOT NULL,
	[MusteriTc] [nvarchar](11) NOT NULL,
	[HesapNo] [nvarchar](50) NOT NULL,
	[Bakiye] [decimal](18, 2) NOT NULL,
	[EkNumara] [int] NOT NULL,
	[HesapDurum] [bit] NOT NULL,
	[Kanal] [nvarchar](6) NULL,
 CONSTRAINT [PK_Hesaplar] PRIMARY KEY CLUSTERED 
(
	[HesapID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hgs]    Script Date: 12.11.2019 23:04:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hgs](
	[HgsId] [int] IDENTITY(1,1) NOT NULL,
	[HgsHesap] [int] NOT NULL,
	[MusteriTc] [nvarchar](11) NOT NULL,
	[Tutar] [decimal](18, 2) NOT NULL,
	[Tarih] [datetime] NULL,
	[Kanal] [nvarchar](6) NULL,
 CONSTRAINT [PK_Hgs] PRIMARY KEY CLUSTERED 
(
	[HgsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kredi]    Script Date: 12.11.2019 23:04:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kredi](
	[KrediId] [int] IDENTITY(1,1) NOT NULL,
	[KrediTutar] [money] NOT NULL,
	[KrediDurum] [bit] NOT NULL,
	[MusteriId] [nvarchar](11) NOT NULL,
 CONSTRAINT [PK_Kredi] PRIMARY KEY CLUSTERED 
(
	[KrediId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Musteriler]    Script Date: 12.11.2019 23:04:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Musteriler](
	[TC] [nvarchar](11) NOT NULL,
	[Ad] [nvarchar](50) NOT NULL,
	[Soyad] [nvarchar](50) NOT NULL,
	[Cinsiyet] [nvarchar](50) NOT NULL,
	[DoğumTarihi] [nvarchar](50) NOT NULL,
	[Telefon] [nvarchar](11) NOT NULL,
	[EMail] [nvarchar](50) NOT NULL,
	[Adres] [nvarchar](50) NOT NULL,
	[Sifre] [nvarchar](50) NOT NULL,
	[Kanal] [nvarchar](6) NULL,
 CONSTRAINT [PK_Musteriler] PRIMARY KEY CLUSTERED 
(
	[TC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Virman]    Script Date: 12.11.2019 23:04:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Virman](
	[VirmanId] [int] IDENTITY(1,1) NOT NULL,
	[Miktar] [decimal](18, 2) NOT NULL,
	[GidenHesapNo] [nvarchar](11) NOT NULL,
	[GelenHesapNo] [nvarchar](11) NOT NULL,
	[MusteriId] [nvarchar](11) NOT NULL,
	[Tarih] [datetime] NULL,
	[Kanal] [nvarchar](6) NULL,
 CONSTRAINT [PK_Virman] PRIMARY KEY CLUSTERED 
(
	[VirmanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Havale] ON 

INSERT [dbo].[Havale] ([HavaleId], [Miktar], [GidenHesapNo], [GelenHesapNo], [MusteriId], [Tarih], [Kanal]) VALUES (1, CAST(100.00 AS Decimal(18, 2)), N'87515 -1001', N'81001-1001', N'11111111111', CAST(N'2019-11-12T22:39:20.227' AS DateTime), NULL)
INSERT [dbo].[Havale] ([HavaleId], [Miktar], [GidenHesapNo], [GelenHesapNo], [MusteriId], [Tarih], [Kanal]) VALUES (2, CAST(1000.00 AS Decimal(18, 2)), N'87515 -1001', N'85230-1001', N'14725836914', CAST(N'2019-11-12T22:50:19.120' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Havale] OFF
SET IDENTITY_INSERT [dbo].[Hesaplar] ON 

INSERT [dbo].[Hesaplar] ([HesapID], [MusteriTc], [HesapNo], [Bakiye], [EkNumara], [HesapDurum], [Kanal]) VALUES (1, N'12345678911', N'87515', CAST(1107.87 AS Decimal(18, 2)), 1001, 1, NULL)
INSERT [dbo].[Hesaplar] ([HesapID], [MusteriTc], [HesapNo], [Bakiye], [EkNumara], [HesapDurum], [Kanal]) VALUES (2, N'12345678911', N'87515', CAST(14.13 AS Decimal(18, 2)), 1002, 1, NULL)
INSERT [dbo].[Hesaplar] ([HesapID], [MusteriTc], [HesapNo], [Bakiye], [EkNumara], [HesapDurum], [Kanal]) VALUES (3, N'11111111111', N'81001', CAST(1200.00 AS Decimal(18, 2)), 1001, 1, NULL)
INSERT [dbo].[Hesaplar] ([HesapID], [MusteriTc], [HesapNo], [Bakiye], [EkNumara], [HesapDurum], [Kanal]) VALUES (4, N'14725836914', N'85230', CAST(300.40 AS Decimal(18, 2)), 1001, 1, NULL)
INSERT [dbo].[Hesaplar] ([HesapID], [MusteriTc], [HesapNo], [Bakiye], [EkNumara], [HesapDurum], [Kanal]) VALUES (5, N'12345678911', N'87515', CAST(120.00 AS Decimal(18, 2)), 1003, 1, NULL)
SET IDENTITY_INSERT [dbo].[Hesaplar] OFF
SET IDENTITY_INSERT [dbo].[Hgs] ON 

INSERT [dbo].[Hgs] ([HgsId], [HgsHesap], [MusteriTc], [Tutar], [Tarih], [Kanal]) VALUES (1, 61362, N'12345678911', CAST(150.50 AS Decimal(18, 2)), CAST(N'2019-11-12T22:54:13.093' AS DateTime), N'Web')
INSERT [dbo].[Hgs] ([HgsId], [HgsHesap], [MusteriTc], [Tutar], [Tarih], [Kanal]) VALUES (2, 18653, N'11111111111', CAST(100.00 AS Decimal(18, 2)), CAST(N'2019-11-12T22:36:28.067' AS DateTime), N'Web')
INSERT [dbo].[Hgs] ([HgsId], [HgsHesap], [MusteriTc], [Tutar], [Tarih], [Kanal]) VALUES (3, 52811, N'11111111111', CAST(100.00 AS Decimal(18, 2)), CAST(N'2019-11-12T22:39:34.520' AS DateTime), N'Web')
INSERT [dbo].[Hgs] ([HgsId], [HgsHesap], [MusteriTc], [Tutar], [Tarih], [Kanal]) VALUES (4, 32326, N'14725836914', CAST(180.10 AS Decimal(18, 2)), CAST(N'2019-11-12T22:53:00.640' AS DateTime), N'Web')
SET IDENTITY_INSERT [dbo].[Hgs] OFF
INSERT [dbo].[Musteriler] ([TC], [Ad], [Soyad], [Cinsiyet], [DoğumTarihi], [Telefon], [EMail], [Adres], [Sifre], [Kanal]) VALUES (N'11111111111', N'Test', N'Test', N'0', N'1998-01-01', N'012345678999', N'test@gmail.com', N'sf', N'123456', N'web')
INSERT [dbo].[Musteriler] ([TC], [Ad], [Soyad], [Cinsiyet], [DoğumTarihi], [Telefon], [EMail], [Adres], [Sifre], [Kanal]) VALUES (N'12345678911', N'Meral', N'Taşdemir', N'0', N'1999-01-31', N'02514746559', N'meral@gmail.com', N'aSADİİİ', N'123', N'web')
INSERT [dbo].[Musteriler] ([TC], [Ad], [Soyad], [Cinsiyet], [DoğumTarihi], [Telefon], [EMail], [Adres], [Sifre], [Kanal]) VALUES (N'14725836914', N'şevval', N'barut', N'0', N'1999-05-04', N'02581473695', N's@hotmail.com', N'ldsjlkjlk', N'147', N'web')
SET IDENTITY_INSERT [dbo].[Virman] ON 

INSERT [dbo].[Virman] ([VirmanId], [Miktar], [GidenHesapNo], [GelenHesapNo], [MusteriId], [Tarih], [Kanal]) VALUES (1, CAST(15.87 AS Decimal(18, 2)), N'87515 -1001', N'87515-1002', N'12345678911', CAST(N'2019-11-12T22:15:05.443' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Virman] OFF
ALTER TABLE [dbo].[Havale]  WITH CHECK ADD  CONSTRAINT [FK_Havale_Musteriler] FOREIGN KEY([MusteriId])
REFERENCES [dbo].[Musteriler] ([TC])
GO
ALTER TABLE [dbo].[Havale] CHECK CONSTRAINT [FK_Havale_Musteriler]
GO
ALTER TABLE [dbo].[Hesaplar]  WITH CHECK ADD  CONSTRAINT [FK_Hesaplar_Musteriler] FOREIGN KEY([MusteriTc])
REFERENCES [dbo].[Musteriler] ([TC])
GO
ALTER TABLE [dbo].[Hesaplar] CHECK CONSTRAINT [FK_Hesaplar_Musteriler]
GO
ALTER TABLE [dbo].[Hgs]  WITH CHECK ADD  CONSTRAINT [FK_Hgs_Musteriler] FOREIGN KEY([HgsId])
REFERENCES [dbo].[Hgs] ([HgsId])
GO
ALTER TABLE [dbo].[Hgs] CHECK CONSTRAINT [FK_Hgs_Musteriler]
GO
ALTER TABLE [dbo].[Kredi]  WITH CHECK ADD  CONSTRAINT [FK_Kredi_Musteriler] FOREIGN KEY([MusteriId])
REFERENCES [dbo].[Musteriler] ([TC])
GO
ALTER TABLE [dbo].[Kredi] CHECK CONSTRAINT [FK_Kredi_Musteriler]
GO
ALTER TABLE [dbo].[Virman]  WITH CHECK ADD  CONSTRAINT [FK_Virman_Musteriler] FOREIGN KEY([MusteriId])
REFERENCES [dbo].[Musteriler] ([TC])
GO
ALTER TABLE [dbo].[Virman] CHECK CONSTRAINT [FK_Virman_Musteriler]
GO
