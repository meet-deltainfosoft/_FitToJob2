select * From Ques


alter table Ques add AnsSelection varchar(10) null


update Ques set AnsSelection  = 'SINGLE' where QueType = 'MCQ'


alter table Exams add AnsSelection varchar(10) null

 