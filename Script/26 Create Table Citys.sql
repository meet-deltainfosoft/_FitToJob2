
CREATE TABLE [dbo].[Citys](
	[CityId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](150) NOT NULL,
	[TypeOFCity] [char](1) NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
	[DivTextListId] [uniqueidentifier] NULL,
	[StateId] [uniqueidentifier] NULL
) ON [PRIMARY]
GO


