
CREATE TABLE [dbo].[State](
	[StateId] [uniqueidentifier] NOT NULL,
	[StateCode] [varchar](100) NULL,
	[Name] [varchar](100) NOT NULL,
	[InsertedOn] [datetime] NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserID] [uniqueidentifier] NULL,
	[COuntryId] [uniqueidentifier] NULL,
	[DivTextListId] [uniqueidentifier] NULL,
	[GSTStateCode] [smallint] NULL,
	[AngelStateCode] [varchar](100) NULL
) ON [PRIMARY]
GO


