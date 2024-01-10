
CREATE TABLE [dbo].[Batchs](
	[BatchId] [uniqueidentifier] NOT NULL,
	[StandardTextListId] [uniqueidentifier] NOT NULL,
	[BatchName] [nvarchar](500) NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NULL,
	[InsertedByUserId] [uniqueidentifier] NOT NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL
) ON [PRIMARY]
GO


