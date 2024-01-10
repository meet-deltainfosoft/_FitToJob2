GO

/****** Object:  Index [Ix_ExamQueID]    Script Date: 19-07-2020 08:42:46 ******/
CREATE NONCLUSTERED INDEX [Ix_ExamQueID] ON [dbo].[Exams]
(
	[QueId] ASC
)
INCLUDE ( 	[RegistrationId],
	[ExamScheduleId]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO


