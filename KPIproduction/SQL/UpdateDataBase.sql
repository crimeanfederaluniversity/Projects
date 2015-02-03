--------------------------------------------------------------
-- Evgeniy Potapov
-- 02.02.2015
-- Add Table [BasicParametersTable]
--------------------------------------------------------------
CREATE TABLE [dbo].[BasicParametersTable](
	[BasicParametersTableID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[Name] [varchar](1000) NOT NULL,
	[AbbreviationEN] [varchar](20) NOT NULL,
	[AbbreviationRU] [varchar](20) NOT NULL,
	[Measure] [varchar](10) NOT NULL,
CONSTRAINT [PK__BasicParametersTable] PRIMARY KEY CLUSTERED 
(
	[BasicParametersTableID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------------------
-- Evgeniy Potapov
-- 02.02.2015
-- Add Table [RolesTable]
--------------------------------------------------------------
CREATE TABLE [dbo].[RolesTable](
	[RolesTableID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[Name] [varchar](1000) NOT NULL,
CONSTRAINT [PK__RolesTable] PRIMARY KEY CLUSTERED 
(
	[RolesTableID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------------------
-- Evgeniy Potapov
-- 02.02.2015
-- Add Table [FirstLevelSubdivisionTable]
--------------------------------------------------------------
CREATE TABLE [dbo].[FirstLevelSubdivisionTable](
	[FirstLevelSubdivisionTableID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[Name] [varchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FirstLevelSubdivisionTableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------------------
-- Evgeniy Potapov
-- 02.02.2015
-- Add Table [SecondLevelSubdivisionTable]
--------------------------------------------------------------
CREATE TABLE [dbo].[SecondLevelSubdivisionTable](
	[SecondLevelSubdivisionTableID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NULL,
	[Name] [varchar](500) NOT NULL,
	[FK_FirstLevelSubdivisionTable] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SecondLevelSubdivisionTableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SecondLevelSubdivisionTable]  WITH CHECK ADD  CONSTRAINT [FK_FirstLevelSubdivisionTable_SecondLevelSubdivisionTable] FOREIGN KEY([FK_FirstLevelSubdivisionTable])
REFERENCES [dbo].[FirstLevelSubdivisionTable] ([FirstLevelSubdivisionTableID])
GO

ALTER TABLE [dbo].[SecondLevelSubdivisionTable] CHECK CONSTRAINT [FK_FirstLevelSubdivisionTable_SecondLevelSubdivisionTable]
GO

--------------------------------------------------------------
-- Evgeniy Potapov
-- 02.02.2015
-- Add Table [ThirdLevelSubdivisionTable]
--------------------------------------------------------------
CREATE TABLE [dbo].[ThirdLevelSubdivisionTable](
	[ThirdLevelSubdivisionTableID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[FK_SecondLevelSubdivisionTable] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ThirdLevelSubdivisionTableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ThirdLevelSubdivisionTable]  WITH CHECK ADD  CONSTRAINT [FK_ThirdLevelSubdivisionTable_SecondLevelSubdivisionTable] FOREIGN KEY([FK_SecondLevelSubdivisionTable])
REFERENCES [dbo].[SecondLevelSubdivisionTable] ([SecondLevelSubdivisionTableID])
GO

ALTER TABLE [dbo].[ThirdLevelSubdivisionTable] CHECK CONSTRAINT [FK_ThirdLevelSubdivisionTable_SecondLevelSubdivisionTable]
GO

--------------------------------------------------------------
-- Evgeniy Potapov
-- 02.02.2015
-- Add Table [FourthLevelSubdivisionTable]
--------------------------------------------------------------
CREATE TABLE [dbo].[FourthLevelSubdivisionTable](
	[FourthLevelSubdivisionTableID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[FK_ThirdLevelSubdivisionTable] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FourthLevelSubdivisionTableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[FourthLevelSubdivisionTable]  WITH CHECK ADD  CONSTRAINT [FK_FourthLevelSubdivisionTable_ThirdLevelSubdivisionTable] FOREIGN KEY([FK_ThirdLevelSubdivisionTable])
REFERENCES [dbo].[ThirdLevelSubdivisionTable] ([ThirdLevelSubdivisionTableID])
GO

ALTER TABLE [dbo].[FourthLevelSubdivisionTable] CHECK CONSTRAINT [FK_FourthLevelSubdivisionTable_ThirdLevelSubdivisionTable]
GO

--------------------------------------------------------------
-- Evgeniy Potapov
-- 02.02.2015
-- Add Table [FifthLevelSubdivisionTable]
--------------------------------------------------------------
CREATE TABLE [dbo].[FifthLevelSubdivisionTable](
	[FifthLevelSubdivisionTableID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[FK_FourthLevelSubdivisionTable] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FifthLevelSubdivisionTableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[FifthLevelSubdivisionTable]  WITH CHECK ADD  CONSTRAINT [FK_FifthLevelSubdivisionTable_FourthLevelSubdivisionTable] FOREIGN KEY([FK_FourthLevelSubdivisionTable])
REFERENCES [dbo].[FourthLevelSubdivisionTable] ([FourthLevelSubdivisionTableID])
GO

ALTER TABLE [dbo].[FifthLevelSubdivisionTable] CHECK CONSTRAINT [FK_FifthLevelSubdivisionTable_FourthLevelSubdivisionTable]
GO

--------------------------------------------------------------
-- Evgeniy Potapov
-- 02.02.2015
-- Add Table [UsersTable]
--------------------------------------------------------------
CREATE TABLE [dbo].[UsersTable](
	[UsersTableID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[FK_RolesTable] [int] NOT NULL,
	[FK_FirstLevelSubdivisionTable] [int] NULL,
	[FK_SecondLevelSubdivisionTable] [int] NULL,
	[FK_ThirdLevelSubdivisionTable] [int] NULL,
	[FK_FourthLevelSubdivisionTable] [int] NULL,
	[FK_FifthLevelSubdivisionTable] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[UsersTableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UsersTable]  WITH CHECK ADD  CONSTRAINT [FK_UsersTable_RolesTable] FOREIGN KEY([FK_RolesTable])
REFERENCES [dbo].[RolesTable] ([RolesTableID])
GO

ALTER TABLE [dbo].[UsersTable] CHECK CONSTRAINT [FK_UsersTable_RolesTable]
GO

ALTER TABLE [dbo].[UsersTable]  WITH CHECK ADD  CONSTRAINT [FK_UsersTable_FifthLevelSubdivisionTable] FOREIGN KEY([FK_FifthLevelSubdivisionTable])
REFERENCES [dbo].[FifthLevelSubdivisionTable] ([FifthLevelSubdivisionTableID])
GO

ALTER TABLE [dbo].[UsersTable] CHECK CONSTRAINT [FK_UsersTable_FifthLevelSubdivisionTable]
GO

ALTER TABLE [dbo].[UsersTable]  WITH CHECK ADD  CONSTRAINT [FK_UsersTable_FirstLevelSubdivisionTable] FOREIGN KEY([FK_FirstLevelSubdivisionTable])
REFERENCES [dbo].[FirstLevelSubdivisionTable] ([FirstLevelSubdivisionTableID])
GO

ALTER TABLE [dbo].[UsersTable] CHECK CONSTRAINT [FK_UsersTable_FirstLevelSubdivisionTable]
GO

ALTER TABLE [dbo].[UsersTable]  WITH CHECK ADD  CONSTRAINT [FK_UsersTable_FourthLevelSubdivisionTable] FOREIGN KEY([FK_FourthLevelSubdivisionTable])
REFERENCES [dbo].[FourthLevelSubdivisionTable] ([FourthLevelSubdivisionTableID])
GO

ALTER TABLE [dbo].[UsersTable] CHECK CONSTRAINT [FK_UsersTable_FourthLevelSubdivisionTable]
GO

ALTER TABLE [dbo].[UsersTable]  WITH CHECK ADD  CONSTRAINT [FK_UsersTable_SecondLevelSubdivisionTable] FOREIGN KEY([FK_SecondLevelSubdivisionTable])
REFERENCES [dbo].[SecondLevelSubdivisionTable] ([SecondLevelSubdivisionTableID])
GO

ALTER TABLE [dbo].[UsersTable] CHECK CONSTRAINT [FK_UsersTable_SecondLevelSubdivisionTable]
GO

ALTER TABLE [dbo].[UsersTable]  WITH CHECK ADD  CONSTRAINT [FK_UsersTable_ThirdLevelSubdivisionTable] FOREIGN KEY([FK_ThirdLevelSubdivisionTable])
REFERENCES [dbo].[ThirdLevelSubdivisionTable] ([ThirdLevelSubdivisionTableID])
GO

ALTER TABLE [dbo].[UsersTable] CHECK CONSTRAINT [FK_UsersTable_ThirdLevelSubdivisionTable]
GO

--------------------------------------------------------------
-- Evgeniy Potapov
-- 02.02.2015
-- Add Table [BasicParametersAndRolesMappingTable]
--------------------------------------------------------------
CREATE TABLE [dbo].[BasicParametersAndRolesMappingTable](
	[BasicParametersAndRolesMappingTableID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[FK_RolesTable] [int] NOT NULL,
	[FK_BasicParametersTable] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BasicParametersAndRolesMappingTableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BasicParametersAndRolesMappingTable]  WITH CHECK ADD  CONSTRAINT [FK_BasicParametersAndRolesMappingTable_RolesTable] FOREIGN KEY([FK_RolesTable])
REFERENCES [dbo].[RolesTable] ([RolesTableID])
GO

ALTER TABLE [dbo].[BasicParametersAndRolesMappingTable] CHECK CONSTRAINT [FK_BasicParametersAndRolesMappingTable_RolesTable]
GO

ALTER TABLE [dbo].[BasicParametersAndRolesMappingTable]  WITH CHECK ADD  CONSTRAINT [FK_BasicParametersAndRolesMappingTable_BasicParametersTable] FOREIGN KEY([FK_BasicParametersTable])
REFERENCES [dbo].[BasicParametersTable] ([BasicParametersTableID])
GO

ALTER TABLE [dbo].[BasicParametersAndRolesMappingTable] CHECK CONSTRAINT [FK_BasicParametersAndRolesMappingTable_BasicParametersTable]
GO

--------------------------------------------------------------
-- Evgeniy Potapov
-- 02.02.2015
-- Add Table [ReportArchiveTable]
--------------------------------------------------------------
CREATE TABLE [dbo].[ReportArchiveTable](
	[ReportArchiveTableID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[StartDateTime] [datetime] NOT NULL,
	[EndDateTime] [datetime] NOT NULL,
	[DateToSend] [datetime] NOT NULL,
	[Calculeted] [bit] NOT NULL,
	[Sent] [bit] NOT NULL,
	[SentDateTime] [datetime] NULL,
	[RecipientConfirmed] [bit] NOT NULL,
CONSTRAINT [PK__ReportArchiveTable] PRIMARY KEY CLUSTERED 
(
	[ReportArchiveTableID] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--------------------------------------------------------------
-- Evgeniy Potapov
-- 02.02.2015
-- Add Table [CollectedBasicParametersTable]
--------------------------------------------------------------
CREATE TABLE [dbo].[CollectedBasicParametersTable](
	[CollectedBasicParametersTableID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[FK_UsersTable] [int] NOT NULL,
	[FK_BasicParametersTable] [int] NOT NULL,
	[FK_ReportArchiveTable] [int] NOT NULL,
	[LastChangeDateTime] [datetime] NOT NULL,
	[SavedDateTime] [datetime] NULL,
	[UserIP] [varchar](15) NULL,
	[CollectedValue] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[CollectedBasicParametersTableID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CollectedBasicParametersTable]  WITH CHECK ADD  CONSTRAINT [FK_CollectedBasicParametersTable_UsersTable] FOREIGN KEY([FK_UsersTable])
REFERENCES [dbo].[UsersTable] ([UsersTableID])
GO

ALTER TABLE [dbo].[CollectedBasicParametersTable] CHECK CONSTRAINT [FK_CollectedBasicParametersTable_UsersTable]
GO

ALTER TABLE [dbo].[CollectedBasicParametersTable]  WITH CHECK ADD  CONSTRAINT [FK_CollectedBasicParametersTable_BasicParametersTable] FOREIGN KEY([FK_BasicParametersTable])
REFERENCES [dbo].[BasicParametersTable] ([BasicParametersTableID])
GO

ALTER TABLE [dbo].[CollectedBasicParametersTable] CHECK CONSTRAINT [FK_CollectedBasicParametersTable_BasicParametersTable]
GO

ALTER TABLE [dbo].[CollectedBasicParametersTable]  WITH CHECK ADD  CONSTRAINT [FK_CollectedBasicParametersTable_ReportArchiveTable] FOREIGN KEY([FK_ReportArchiveTable])
REFERENCES [dbo].[ReportArchiveTable] ([ReportArchiveTableID])
GO

ALTER TABLE [dbo].[CollectedBasicParametersTable] CHECK CONSTRAINT [FK_CollectedBasicParametersTable_ReportArchiveTable]
GO

--------------------------------------------------------------
-- Evgeniy Potapov
-- 02.02.2015
-- Add Table [ReportAndRolesMapping]
--------------------------------------------------------------
CREATE TABLE [dbo].[ReportAndRolesMapping](
	[ReportAndRolesMappingID] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[FK_RolesTable] [int] NOT NULL,
	[FK_ReportArchiveTable] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReportAndRolesMappingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ReportAndRolesMapping]  WITH CHECK ADD  CONSTRAINT [FK_ReportAndRolesMapping_RolesTable] FOREIGN KEY([FK_RolesTable])
REFERENCES [dbo].[RolesTable] ([RolesTableID])
GO

ALTER TABLE [dbo].[ReportAndRolesMapping] CHECK CONSTRAINT [FK_ReportAndRolesMapping_RolesTable]
GO

ALTER TABLE [dbo].[ReportAndRolesMapping]  WITH CHECK ADD  CONSTRAINT [FK_ReportAndRolesMapping_ReportArchiveTable] FOREIGN KEY([FK_ReportArchiveTable])
REFERENCES [dbo].[ReportArchiveTable] ([ReportArchiveTableID])
GO

ALTER TABLE [dbo].[ReportAndRolesMapping] CHECK CONSTRAINT [FK_ReportAndRolesMapping_ReportArchiveTable]
GO