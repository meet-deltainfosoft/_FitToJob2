
alter table Ques add QueType varchar(10) null
alter table Ques add QueDataType varchar(10) null
alter table Ques add RightMarks decimal(18,2) null
alter table Ques add WrongMarks decimal(18,2) null
alter table Ques add NonMarks decimal(18,2) null
alter table Ques alter column Ans nvarchar(max) null


update Ques set QueType = 'MCQ', QueDataType = NULL, RightMarks = '1', WrongMarks = '-0.25', NonMarks = '0'


alter table ExamSchedules add NegativeMarks bit

update ExamSchedules set NegativeMarks = '1'

alter table Exams alter column Ans nvarchar(max) null

alter table Ques add NoOfFile int null
alter table Ques add SampleAns1 varchar(max) null
alter table Ques add SampleAns2 varchar(max) null
alter table Ques add SampleAns3 varchar(max) null
alter table Ques add SampleAns4 varchar(max) null

alter table Exams alter column Ans nvarchar(max)
alter table Exams add QueType varchar (10) null


update Exams set Ans = '~SKIPPED~' where Ans ='9'
UPDATE Exams SET QueType = 'MCQ'


alter table Exams add AnsImage1 varchar(max) null
alter table Exams add AnsImage2 varchar(max) null
alter table Exams add AnsImage3 varchar(max) null
alter table Exams add AnsImage4 varchar(max) null


alter table ExamSchedules add PerQuestionTime bit null
update ExamSchedules  set PerQuestionTime  = 1 