 

alter table Subs add PhotoPath varchar(500) null
 
GO

/****** Object:  Table [dbo].[Chapters]    Script Date: 25-May-2020 14:58:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



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


