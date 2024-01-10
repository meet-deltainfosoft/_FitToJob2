

CREATE TABLE [dbo].[ItemWiseDetails](
	[ItemWiseDetailId] [uniqueidentifier] NOT NULL,
	[ItemType] [varchar](100) NULL,
	[ItemId] [varchar](500) NULL,
	[Name] [varchar](500) NULL,
	[URL] [varchar](500) NULL,
	[TotalCount] [int] NULL,
	[LastEnteredIn] [datetime] NULL,
	[UserId] [uniqueidentifier] NULL
) ON [PRIMARY]
GO


