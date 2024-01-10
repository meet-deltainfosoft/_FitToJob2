use OnlineExamVVM


alter table Exams add AnsImage5 varchar(max) null
alter table Exams add AnsImage6 varchar(max) null
alter table Exams add AnsImage7 varchar(max) null
alter table Exams add AnsImage8 varchar(max) null
alter table Exams add AnsImage9 varchar(max) null
alter table Exams add AnsImage10 varchar(max) null
alter table Exams add AnsImage11 varchar(max) null
alter table Exams add AnsImage12 varchar(max) null
alter table Exams add AnsImage13 varchar(max) null
alter table Exams add AnsImage14 varchar(max) null
alter table Exams add AnsImage15 varchar(max) null
alter table Exams add AnsImage16 varchar(max) null
alter table Exams add AnsImage17 varchar(max) null
alter table Exams add AnsImage18 varchar(max) null
alter table Exams add AnsImage19 varchar(max) null
alter table Exams add AnsImage20 varchar(max) null
alter table Exams add AnsImage21 varchar(max) null
alter table Exams add AnsImage22 varchar(max) null
alter table Exams add AnsImage23 varchar(max) null
alter table Exams add AnsImage24 varchar(max) null
alter table Exams add AnsImage25 varchar(max) null
alter table Exams add AnsImage26 varchar(max) null
alter table Exams add AnsImage27 varchar(max) null
alter table Exams add AnsImage28 varchar(max) null
alter table Exams add AnsImage29 varchar(max) null
alter table Exams add AnsImage30 varchar(max) null
alter table Exams add AnsImage31 varchar(max) null
alter table Exams add AnsImage32 varchar(max) null
alter table Exams add AnsImage33 varchar(max) null
alter table Exams add AnsImage34 varchar(max) null
alter table Exams add AnsImage35 varchar(max) null
alter table Exams add AnsImage36 varchar(max) null
alter table Exams add AnsImage37 varchar(max) null
alter table Exams add AnsImage38 varchar(max) null
alter table Exams add AnsImage39 varchar(max) null
alter table Exams add AnsImage40 varchar(max) null

alter table ExamSchedules add No	varchar	(50) null

alter table Ques add NoOfSubQues int null

--change name of "Users" to "User_" in table name

create view Users
as
select UserId, FirstName, LastName, EmailId, RptsToUserId, UserName, Password, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, IsDisabled, IsAdmin, DeptId from Users_
union all
select EmpId, FirstName collate SQL_Latin1_General_CP1_CI_AS as FirstName, LastName collate SQL_Latin1_General_CP1_CI_AS as LastName, email collate SQL_Latin1_General_CP1_CI_AS as email, NULL, EmpLoginId collate SQL_Latin1_General_CP1_CI_AS, Password collate SQL_Latin1_General_CP1_CI_AS, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, IsDisabled, IsAdmin, NULL from DeltaSchoolIERP03012019..Emps where EmpLoginId is not null and EmpLoginId <> 'aaa'




alter table Users_ alter column FirstName varchar(50)
alter table Users_ alter column LastName varchar(50)
alter table Users_ alter column UserName varchar(100)
alter table Users_ alter column Password varchar(100)




CREATE TABLE [dbo].[ExamCheckAllotments](
	ExamCheckAllotmentId [uniqueidentifier] NOT NULL,
	[StandardTextListId] [uniqueidentifier] NOT NULL,
	SubId [uniqueidentifier] NOT NULL,
	TestId [uniqueidentifier] NOT NULL,
	ExamScheduleId [uniqueidentifier] NOT NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ExamCheckAllotments] PRIMARY KEY CLUSTERED 
(
	ExamCheckAllotmentId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO




CREATE TABLE [dbo].[ExamCheckAllotmentLns](
	ExamCheckAllotmentLnId [uniqueidentifier] NOT NULL,
	ExamCheckAllotmentId [uniqueidentifier] NOT NULL,
	UserId [uniqueidentifier] NOT NULL,
	QueId [uniqueidentifier] NOT NULL,	
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ExamCheckAllotmentLns] PRIMARY KEY CLUSTERED 
(
	ExamCheckAllotmentLnId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO







CREATE TABLE [dbo].ExamEvaluations(
	ExamEvaluationId [uniqueidentifier] NOT NULL,	
	QueId [uniqueidentifier] NOT NULL,	
	UserId [uniqueidentifier] NOT NULL,
	ExamId [uniqueidentifier] NOT NULL,
	ImageNo int null,
	ImagePath varchar(max) null,
	TotalObtMark decimal(18,2) null,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ExamEvaluations] PRIMARY KEY CLUSTERED 
(
	ExamEvaluationId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO




CREATE TABLE [dbo].ExamEvaluationLns(
	ExamEvaluationLnId [uniqueidentifier] NOT NULL,	
	ExamEvaluationId [uniqueidentifier] NOT NULL,	
	LnNo int null,
	ObtMark decimal(18,2) null,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ExamEvaluationLns] PRIMARY KEY CLUSTERED 
(
	ExamEvaluationLnId ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO


