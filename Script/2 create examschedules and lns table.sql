USE [VVMExam]
GO

/****** Object:  Table [dbo].[Subs]    Script Date: 31-Mar-2020 14:21:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].ExamSchedules(
	ExamScheduleId [uniqueidentifier] NOT NULL,
	StandardTextListId [uniqueidentifier] NOT NULL,
	SubId [uniqueidentifier] NOT NULL,
	TestId [uniqueidentifier] NOT NULL,
	TotalQuestions int NULL,
	ExamDate datetime NULL,
	ExamFromTime datetime NULL,
	ExamToTime datetime NULL,
	TotalMins int NULL,
	PerQueMins int NULL,	
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL
 CONSTRAINT [PK_ExamSchedules] PRIMARY KEY CLUSTERED 
(
	ExamScheduleId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO 


CREATE TABLE [dbo].ExamScheduleLns(
	ExamScheduleLnId [uniqueidentifier] NOT NULL,
	LnNo int null,
	RegistrationId [uniqueidentifier] NOT NULL, 
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL
 CONSTRAINT [PK_ExamScheduleLns] PRIMARY KEY CLUSTERED 
(
	ExamScheduleLnId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO 