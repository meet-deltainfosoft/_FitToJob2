
CREATE TABLE [dbo].[LiveClasses](
	[LiveClassId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Link] [varchar](250) NULL,
	[SubId] [uniqueidentifier] NULL,
	[SubName] [nvarchar](100) NULL,
	[TopicName] [nvarchar](250) NULL,
	[FromTime] [datetime] NULL,
	[ToTime] [datetime] NULL,
	[Remarks] [nvarchar](500) NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_LiveClasses] PRIMARY KEY CLUSTERED 
(
	[LiveClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO





CREATE TABLE [dbo].[ChapterVideos](
	[ChapterVideoId] [bigint] IDENTITY(1,1) NOT NULL,
	[ChapterId] [uniqueidentifier] NOT NULL,
	[SrNo] [int] NULL,
	[VideoName] [nvarchar](150) NOT NULL,
	[VideoLink] [nvarchar](500) NOT NULL,
	[Remarks] [nvarchar](500) NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
	[SubId] [uniqueidentifier] NULL,
	[StandardTextListId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ChapterVideo] PRIMARY KEY CLUSTERED 
(
	[ChapterVideoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[Chapters](
	[ChapterId] [uniqueidentifier] NOT NULL,
	[SrNo] [int] NULL,
	[ChapterName] [nvarchar](150) NOT NULL,
	[SubId] [uniqueidentifier] NOT NULL,
	[Remarks] [nvarchar](500) NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
	[StandardTextListId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Chapters] PRIMARY KEY CLUSTERED 
(
	[ChapterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[ChapterPDFs](
	[ChapterPDFId] [bigint] IDENTITY(1,1) NOT NULL,
	[ChapterId] [uniqueidentifier] NOT NULL,
	[SrNo] [int] NULL,
	[FileName] [nvarchar](150) NOT NULL,
	[FileLink] [nvarchar](500) NOT NULL,
	[Remarks] [nvarchar](500) NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
	[StandardTextListId] [uniqueidentifier] NULL,
	[SubId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ChapterPDFs] PRIMARY KEY CLUSTERED 
(
	[ChapterPDFId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO



CREATE TABLE [dbo].[ChapterLinks](
	[ChapterLinkId] [bigint] IDENTITY(1,1) NOT NULL,
	[ChapterId] [uniqueidentifier] NOT NULL,
	[SrNo] [int] NULL,
	[LinkName] [nvarchar](150) NOT NULL,
	[Link] [nvarchar](500) NOT NULL,
	[Remarks] [nvarchar](500) NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
	[SubId] [uniqueidentifier] NULL,
	[StandardTextListId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ChapterLinks] PRIMARY KEY CLUSTERED 
(
	[ChapterLinkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO


