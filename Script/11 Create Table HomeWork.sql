
CREATE TABLE [dbo].[HomeWorks](
	[HomeWorkId] [uniqueidentifier] NOT NULL,
	[SubId] [uniqueidentifier] NOT NULL,
	[Dt] datetime NOT NULL,
	[HomeWork] [nvarchar](max) NULL,
	[A1] [nvarchar](max) NULL,
	[A2] [nvarchar](max) NULL,
	[A3] [nvarchar](max) NULL,
	[A4] [nvarchar](max) NULL,
	[Ans] [nvarchar](max) NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
	[ImageNameQus] [varchar](max) NULL,
	[ImageNameA1] [varchar](max) NULL,
	[ImageNameA2] [varchar](max) NULL,
	[ImageNameA3] [varchar](max) NULL,
	[ImageNameA4] [varchar](max) NULL,
	[ChapterId] [uniqueidentifier] NULL,
	[Hashtag] [nvarchar](100) NULL,
	[SrNo] [int] NULL,
	[HomeWorkType] [varchar](10) NULL,
	[HomeWorkDataType] [varchar](10) NULL,
	[NoOfFile] [int] NULL,
	[SampleAns1] [varchar](max) NULL,
	[SampleAns2] [varchar](max) NULL,
	[SampleAns3] [varchar](max) NULL,
	[SampleAns4] [varchar](max) NULL,
	[AnsSelection] [varchar](max) NULL
 CONSTRAINT [PK_HomeWorks] PRIMARY KEY CLUSTERED 
(
	[HomeWorkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


