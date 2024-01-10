USE [iEXAM]
GO

/****** Object:  Table [dbo].[Exams]    Script Date: 30-05-2020 12:14:57 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HomeWorkAns](
	[HomeWorkAnsId] [uniqueidentifier] NOT NULL,
	[RegistrationId] [uniqueidentifier] NOT NULL,
	[SubId] [uniqueidentifier] NOT NULL,
	[HomeWorkId] [uniqueidentifier] NOT NULL,
	[Ans] [nvarchar](max) NULL,
	[ChapterId] [uniqueidentifier] NULL,
	[HomeWorkType] [varchar](10) NULL,
	[AnsImage1] [varchar](max) NULL,
	[AnsImage2] [varchar](max) NULL,
	[AnsImage3] [varchar](max) NULL,
	[AnsImage4] [varchar](max) NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_HomeWorkAns] PRIMARY KEY CLUSTERED 
(
	[HomeWorkAnsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


