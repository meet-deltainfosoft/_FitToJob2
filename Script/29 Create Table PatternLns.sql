
CREATE TABLE [dbo].[PatternLns](
	[PatternLnId] [uniqueidentifier] NOT NULL,
	[PatternId] [uniqueidentifier] NOT NULL,
	[SubId] [uniqueidentifier]  NULL,
	[NoOfMCQ] [int] NULL,
	[MCQRightMarks] [decimal] NULL,
	[MCQWrongMarks] [decimal] NULL,
	[MCQSkippedMarks] [decimal] NULL,
	[NoOfNonMCQ] [int] NULL,
	[NONMCQRightMarks] [decimal] NULL,
	[NONMCQWrongMarks] [decimal] NULL,
	[NONMCQSkippedMarks] [decimal] NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL
CONSTRAINT [PK_PatternLns] PRIMARY KEY CLUSTERED 
(
	[PatternLnId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] 
GO


