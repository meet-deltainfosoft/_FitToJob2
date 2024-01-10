alter view  [dbo].[vwInterviewForms]  
as  
select InterviewFormId as InterviewFormId,FullName as FullName ,PresentAddress as Address ,PresentPost as Post ,PresentVillage as Village , PresentDistrict as District ,PresentPinCode as PinCode , PresentMobileNo As MobileNo ,Email as EmailId , I.RegistrationId  , D.Name As JobProfile
 from FitToJob..InterviewForms  I
 inner join Registration R on R.RegistrationId = I.RegistrationId  
 inner Join FitToJob..JobOfferings J on J.JobOfferingId = R.JobOfferingId 
 Inner join FitToJob..Designations D on D.DesignationId = J.DesignationId





