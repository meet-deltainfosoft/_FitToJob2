
CREATE TABLE [dbo].[ExamResultPublish](
	ExamResultPublishId [uniqueidentifier] NOT NULL,
	[StandardTextListId] [uniqueidentifier] NOT NULL,
	[SubId] [uniqueidentifier] NOT NULL,
	[TestId] [uniqueidentifier] NOT NULL,
	[ExamScheduleId] [uniqueidentifier] NOT NULL,
	AnsKeyFilePath varchar(max) null,
	IsResultPublished bit null,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ExamResultPublish] PRIMARY KEY CLUSTERED 
(
	ExamResultPublishId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO


