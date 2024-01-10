
alter table Chapters add PeriodNo decimal(18,2)

alter table ChapterLns add PeriodNo decimal(18,2)

alter table ChapterPdfs add PeriodNo decimal(18,2)

alter table ChapterVideos add PeriodNo decimal(18,2)

alter table ChapterLinks add PeriodNo decimal(18,2)

alter table Registration add ExtraMobileNo varchar(15)

alter table DeltaSchooliERP12052020..Students add ExtraMobileNo varchar(20)
alter table DeltaSchooliERP12052020..Students_History add ExtraMobileNo varchar(20)

alter table QueBanks add PeriodNo decimal(18,2)

Alter view   [dbo].[Registration]  
as  
select StudentId as RegistrationId,  upper(cast (FirstName + ' ' + isnull(LastName, MiddleName) as varchar(300)) collate SQL_Latin1_General_CP1_CI_AS) as FirstName, NULL as LastName, null as FatherFirstName, NULL as FatherLastName, NULL as EmailId, NULL as Password, NULL as ConfirmPassword, NULL as Month, NULL as Day, NULL as Year, Sex as Gender, convert(varchar(15),FMobno) as MobileNo,ExtraMobileNo, StreetAddress as AddressLine1, NULL as CollegeId, NULL as CollAddress, NULL as QualificationId, NULL as BranchId, NULL as AggPercentage, NULL as State, NULL as Country, NULL as ElectricityBill, NULL as SaveEnergyWay1, NULL as SaveEnergyWay2, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, NULL as ConvenientDt, NULL as StartDt, NULL as EndDt, NULL as IsCerificate, NULL as IdProofImageName, NULL as CollegeIdProofImageName, NULL as CollegeRespectiveSir, NULL as CollegeRespectiveSirCoNo  
, OTP as OTP, OTPGeneratedOn, CourseLnId as StandardId, convert(bit, 0) as IsDeActive, convert(varchar(15),rollno) as ExamNo, 'Vidyamandir Trust, Palanpur' as SchoolName,  'PALANPUR' as City, IMEI, FCMId from DeltaSchooliERP12052020..Students where isnull
(IsActive, 0) = 0

GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VideoNos](
	[VideoNoId] [bigint] IDENTITY(1,1) NOT NULL,
	[ChapterId] [uniqueidentifier] NOT NULL,
	[InsertedOn] [datetime] NOT NULL,
	[LastUpdatedOn] [datetime] NOT NULL,
	[InsertedByUserId] [uniqueidentifier] NULL,
	[LastUpdatedByUserId] [uniqueidentifier] NULL,
	[SubId] [uniqueidentifier] NULL,
	[StandardTextListId] [uniqueidentifier] NULL,
	[PeriodNo] [decimal](18, 2) NULL,
	[PersonName1] [varchar](150) NULL,
	[Ratio1] [varchar](150) NULL,
	[PersonName2] [varchar](150) NULL,
	[Ratio2] [varchar](150) NULL,
	[PersonName3] [varchar](150) NULL,
	[Ratio3] [varchar](150) NULL,
	[PersonName4] [varchar](150) NULL,
	[Ratio4] [varchar](150) NULL,
	[PersonName5] [varchar](150) NULL,
	[Ratio5] [varchar](150) NULL,
 CONSTRAINT [PK_VideoNos] PRIMARY KEY CLUSTERED 
(
	[VideoNoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO


---------------

-----VVMWR

alter table dds add iExam_PeriodNo decimal(18,2)

alter table DeltaSchoolWRDeleted..DDsHistory add iExam_PeriodNo decimal(18,2)

--alter trigger ALTER TRIGGER [dbo].[trg_DDsUpdate]  
-----------



select * into TextLists_BkpStd From TextLists where [Group] = 'Standard'

delete from TextLists where [Group] = 'Standard'

--ERP DB
Insert into TextLists (TextListId,[Group],[Text],InsertedOn,LastUpdatedOn)
select TextListId,[Group],[Text],getdate(),getdate() From DeltaSchooliERP12052020..TextLists where [Group] = 'Standard'
--
select * into Subs_Bkp from Subs

delete From Subs

select Distinct cl.StandardTextListId,s.Name into #Temp from DeltaSchooliERP12052020..Subjects s inner join DeltaSchooliERP12052020..CourseLns cl on cl.CourseLnId = s.CourseLnId order by cl.StandardTextListId,s.Name

Insert into Subs
select NewID(),Name,NULL,Getdate(),Getdate(),NULL,NULL,StandardTextListId,NULL,1,NULL from #Temp

-------------




ALTER view   [dbo].[Registration]  
as  
select StudentId as RegistrationId,  upper(cast (FirstName + ' ' + isnull(LastName, MiddleName) as varchar(300)) collate SQL_Latin1_General_CP1_CI_AS) as FirstName, NULL as LastName, null as FatherFirstName, NULL as FatherLastName, NULL as EmailId, NULL as Password, NULL as ConfirmPassword, NULL as Month, NULL as Day, NULL as Year, Sex as Gender, convert(varchar(15),FMobno) as MobileNo,ExtraMobileNo, StreetAddress as AddressLine1, NULL as CollegeId, NULL as CollAddress, NULL as QualificationId, NULL as BranchId, NULL as AggPercentage, NULL as State, NULL as Country, NULL as ElectricityBill, NULL as SaveEnergyWay1, NULL as SaveEnergyWay2, s.InsertedOn, s.LastUpdatedOn, s.InsertedByUserId, s.LastUpdatedByUserId, NULL as ConvenientDt, NULL as StartDt, NULL as EndDt, NULL as IsCerificate, NULL as IdProofImageName, NULL as CollegeIdProofImageName, NULL as CollegeRespectiveSir, NULL as CollegeRespectiveSirCoNo  
, OTP as OTP, OTPGeneratedOn, cl.StandardTextListId as StandardId,DivisionTextListId, convert(bit, 0) as IsDeActive, convert(varchar(15),rollno) as ExamNo, 'Vidyamandir Trust, Palanpur' as SchoolName,  'PALANPUR' as City, IMEI, FCMId from DeltaSchooliERP12052020..Students s inner join DeltaSchooliERP12052020..CourseLns cl on cl.CourseLnId = s.CourseLnId where isnull
(IsActive, 0) = 0
GO

Insert into TextLists (TextListId,[Group],[Text],InsertedOn,LastUpdatedOn)
select TextListId,[Group],[Text],getdate(),getdate() From DeltaSchooliERP12052020..TextLists where [Group] = 'Division' 




-------------------




alter table HomeWorkAns add AnsImage5 varchar(max)
alter table HomeWorkAns add AnsImage6 varchar(max)
alter table HomeWorkAns add AnsImage7 varchar(max)
alter table HomeWorkAns add AnsImage8 varchar(max)
alter table HomeWorkAns add AnsImage9 varchar(max)
alter table HomeWorkAns add AnsImage10 varchar(max)
alter table HomeWorkAns add AnsImage11 varchar(max)
alter table HomeWorkAns add AnsImage12 varchar(max)
alter table HomeWorkAns add AnsImage13 varchar(max)
alter table HomeWorkAns add AnsImage14 varchar(max)
alter table HomeWorkAns add AnsImage15 varchar(max)
alter table HomeWorkAns add AnsImage16 varchar(max)
alter table HomeWorkAns add AnsImage17 varchar(max)
alter table HomeWorkAns add AnsImage18 varchar(max)
alter table HomeWorkAns add AnsImage19 varchar(max)
alter table HomeWorkAns add AnsImage20 varchar(max)
alter table HomeWorkAns add AnsImage21 varchar(max)
alter table HomeWorkAns add AnsImage22 varchar(max)
alter table HomeWorkAns add AnsImage23 varchar(max)
alter table HomeWorkAns add AnsImage24 varchar(max)
alter table HomeWorkAns add AnsImage25 varchar(max)
alter table HomeWorkAns add AnsImage26 varchar(max)
alter table HomeWorkAns add AnsImage27 varchar(max)
alter table HomeWorkAns add AnsImage28 varchar(max)
alter table HomeWorkAns add AnsImage29 varchar(max)
alter table HomeWorkAns add AnsImage30 varchar(max)
alter table HomeWorkAns add AnsImage31 varchar(max)
alter table HomeWorkAns add AnsImage32 varchar(max)
alter table HomeWorkAns add AnsImage33 varchar(max)
alter table HomeWorkAns add AnsImage34 varchar(max)
alter table HomeWorkAns add AnsImage35 varchar(max)
alter table HomeWorkAns add AnsImage36 varchar(max)
alter table HomeWorkAns add AnsImage37 varchar(max)
alter table HomeWorkAns add AnsImage38 varchar(max)
alter table HomeWorkAns add AnsImage39 varchar(max)
alter table HomeWorkAns add AnsImage40 varchar(max)

---------------------------------------------------

Alter table ItemWiseDetails add TotalFiftyPercentShowCnt int