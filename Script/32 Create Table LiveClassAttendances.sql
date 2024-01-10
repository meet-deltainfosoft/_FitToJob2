
CREATE TABLE [dbo].[LiveClassAttendances](
	[LiveClassAttendanceId] [uniqueidentifier] NOT NULL,
	[RegistrationId] [uniqueidentifier] NOT NULL,
	[SubId] [uniqueidentifier] NOT NULL,
	[LiveClassId] [uniqueidentifier] NULL,
	[InTime] [datetime]  NULL,
	[OutTime] [datetime]  NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NULL,
	[InsertedByUserId] [uniqueidentifier] NOT NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL
) ON [PRIMARY]
GO


