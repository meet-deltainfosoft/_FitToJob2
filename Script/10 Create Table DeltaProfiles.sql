
CREATE TABLE [dbo].[DeltaProfiles](
	[DeltaProfileId] [uniqueidentifier] NOT NULL,
	[EmailId] [varchar](50) NULL,
	[ContactNo] [varchar](15) NULL,
	[WebSite] [varchar](50) NULL,
	[SalesEmailId] [varchar](50) NULL,
	[SalesContactNo] [varchar](15) NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL
 CONSTRAINT [PK_DeltaProfiles] PRIMARY KEY CLUSTERED 
(
	[DeltaProfileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO



Insert Into DeltaProfiles(DeltaProfileId,EmailId,ContactNo,WebSite
,SalesEmailId,SalesContactNo,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId)
VAlues(NewID(),'support@deltainfosoft.com','99106497803','www.schoolierp.com','sales@ierp.in','9925412368',getdate(),getdate(),null,null)
Go


