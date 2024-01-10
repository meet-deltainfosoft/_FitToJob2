
alter table Ques add ChapterId uniqueidentifier null
alter table Ques add LevelOfQue varchar(10) null

 

CREATE TABLE [dbo].[TestPatterns](
	[TestPatternId] [uniqueidentifier] NOT NULL,
	StandardId [uniqueidentifier] NOT NULL,
	TestPatternName nvarchar(100) not null,
	Remarks nvarchar(500) not null,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
	[StandardTextListId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_TestPatterns] PRIMARY KEY CLUSTERED 
(
	[TestPatternId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO

 

CREATE TABLE [dbo].[TestPatternLns](
	[TestPatternLnId] [uniqueidentifier] NOT NULL,
	[TestPatternId] [uniqueidentifier] NOT NULL,
	LnNo int null,
	SubId [uniqueidentifier] NOT NULL,
	NoOfMCQQue int null,
	NoOfMCQRightMarks decimal(18,2) null,
	NoOfMCQWrongMarks decimal(18,2) null,
	NoOfMCQNonMarks decimal(18,2) null,
	NoOfNONMCQQue int null,
	NoOfNONMCQRightMarks decimal(18,2) null,
	NoOfNONMCQWrongMarks decimal(18,2) null,
	NoOfNONMCQNonMarks decimal(18,2) null,
	NoOfFILEQue int null,
	NoOfFILERightMarks decimal(18,2) null,
	NoOfFILEWrongMarks decimal(18,2) null,
	NoOfFILENonMarks decimal(18,2) null,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
	[StandardTextListId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_TestPatternLns] PRIMARY KEY CLUSTERED 
(
	[TestPatternLnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO


 
CREATE TABLE [dbo].[QuestionBanks](
	QuestionBankId [uniqueidentifier] NOT NULL,
	[SubId] [uniqueidentifier] NOT NULL,
	[Que] [nvarchar](max) NULL,
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
	[TestId] [uniqueidentifier] NULL,
	[Hashtag] [nvarchar](100) NULL,
	[SrNo] [int] NULL,
	[QueType] [varchar](10) NULL,
	[QueDataType] [varchar](10) NULL,
	[RightMarks] [decimal](18, 2) NULL,
	[WrongMarks] [decimal](18, 2) NULL,
	[NonMarks] [decimal](18, 2) NULL,
	[NoOfFile] [int] NULL,
	[SampleAns1] [varchar](max) NULL,
	[SampleAns2] [varchar](max) NULL,
	[SampleAns3] [varchar](max) NULL,
	[SampleAns4] [varchar](max) NULL,
	[AnsSelection] [varchar](10) NULL,
	[ChapterId] [uniqueidentifier] NULL,
	[LevelOfQue] [varchar](10) NULL,
 CONSTRAINT [PK_QuestionBanks] PRIMARY KEY CLUSTERED 
(
	QuestionBankId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO




alter table Tests add StandardTextListId uniqueidentifier null
alter table Tests add EasyCount int null
alter table Tests add MediumCount int null
alter table Tests add HardCount int null



alter table ExamSchedules add [TestPatternId] [uniqueidentifier] null