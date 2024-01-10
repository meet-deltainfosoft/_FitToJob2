
CREATE TABLE [dbo].[ExamMarks](
	[ExamMarksId] [uniqueidentifier] NOT NULL,
	[ExamScheduleId] [uniqueidentifier] NULL,
	[QueId] [uniqueidentifier] NOT NULL,
	[RegistrationId] [uniqueidentifier] NOT NULL,
	[Marks] decimal(18,5),
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ExamMarks] PRIMARY KEY CLUSTERED 
(
	[ExamMarksId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO


