
CREATE TABLE [dbo].[Patterns](
	[PatternId] [uniqueidentifier] NOT NULL,
	[StandardTextlistId] [uniqueidentifier] NOT NULL,
	[PatternName] [nvarchar](max) NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL
CONSTRAINT [PK_Patterns] PRIMARY KEY CLUSTERED 
(
	[PatternId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


