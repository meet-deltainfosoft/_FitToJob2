

CREATE TABLE [dbo].[Country](
	[CountryId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[InsertedOn] [datetime] NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserID] [uniqueidentifier] NULL,
	[CountryCode] [varchar](20) NULL
) ON [PRIMARY]
GO


