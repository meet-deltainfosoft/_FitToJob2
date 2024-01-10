using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Summary description for InterViewFormDAL
/// </summary>
public class InterViewFormDAL
{
    public GeneralDAL _generalDAL;

    public InterViewFormDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~InterViewFormDAL()
    {
        _generalDAL = null;
    }

    public void Insert(InterViewFromDTO InterViewFromDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string InterviewFormId;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = "DECLARE  @RegistrationId uniqueidentifier;" +
                                 " SET @RegistrationId = NewId()" +
                                 " INSERT INTO Registrations(RegistrationId,FirstName,MobileNo,AadharCardNo,Address,District, " +
                                 " InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId )" +
                                 " VALUES(@RegistrationId," + ((InterViewFromDTO.FullName == null) ? "NULL" : "'" + InterViewFromDTO.FullName.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PresentMobileNo == null) ? "NULL" : "'" + InterViewFromDTO.PresentMobileNo.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.AadharCardNo == null) ? "NULL" : "'" + InterViewFromDTO.AadharCardNo.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PresentAddress == null) ? "NULL" : "'" + InterViewFromDTO.PresentAddress.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PresentDistrict == null) ? "NULL" : "'" + InterViewFromDTO.PresentDistrict.Replace("'", "''") + "'") + "," +
                                 " GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                 ");" +
                                 " SELECT @RegistrationId";


            string RegistrationId = sqlCmd.ExecuteScalar().ToString();
            //sqlTrans.Commit();

            sqlCmd.CommandText = "DECLARE  @InterviewFormId uniqueidentifier;" +
                                 " SET @InterviewFormId = NewId()" +
                                 " INSERT INTO InterviewForms(InterviewFormId,RegistrationId,FullName,PresentAddress,PresentPost,PresentVillage,PresentDistrict,PresentPinCode,PresentMobileNo, " +
                                 " PermanentAddress,PermanentPost,PermanentVillage,PermanentDistrict,PermanentPinCode,PermanentMobileNo,DOB,BloodGroup,AadharCardNo,PanCardNo,ElectionCardNo,Category,Email, " +
                                 " FatherName,FatherOccupation,FatherEducation,FatherMobileNo,MotherName,MotherOccupation,MotherEducation,MotherMobileNo, " +
                                 " WifeName,WifeOccupation,WifeEducation,WifeMobileNo,BrotherName,BrotherOccupation,BrotherEducation,BrotherMobileNo,  " +
                                 " NomineeName,NomineeDOB,NomineeRelation,NomineeAge,Standanrd10Subject,Standanrd10PassingYear,Standanrd10Percentage , " +
                                 " Standanrd12Subject,Standanrd12PassingYear,Standanrd12Percentage,GraduateSubject,GraduatePassingYear,GraduatePercentage , " +
                                 " PostGraduateSubject,PostGraduatePassingYear,PostGraduatePercentage,OtherSubject,OtherPassingYear,OtherPercentage,CertificateCourseName,CertificateCourseYear,TrainingName,TrainingYear,MedalName,MedalYear, " +
                                 " FirstCompanyName,FirstCompanyDesignation,FirstCompanyExp,FirstCompanySalary,SecondCompanyName,SecondCompanyDesignation,SecondCompanyExp,SecondCompanySalary,ThirdCompanyName,ThirdCompanyDesignation,ThirdCompanyExp,ThirdCompanySalary,  " +
                                 " OtherExpNoExpDetails,Document1Path1,Document1Path2,Document2Path1,Document2Path2,Document3Path1,Document3Path2,Document4Path1,Document4Path2,Document5Path1,Document5Path2,Document6Path1,Document6Path2,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId )" +
                                 " VALUES(@InterviewFormId,'" + RegistrationId  + "',"
                                 + ((InterViewFromDTO.FullName == null) ? "NULL" : "'" + InterViewFromDTO.FullName.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PresentAddress == null) ? "NULL" : "'" + InterViewFromDTO.PresentAddress.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PresentPost == null) ? "NULL" : "'" + InterViewFromDTO.PresentPost.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PresentVillage == null) ? "NULL" : "'" + InterViewFromDTO.PresentVillage.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PresentDistrict == null) ? "NULL" : "'" + InterViewFromDTO.PresentDistrict.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PresentPinCode == null) ? "NULL" : "'" + InterViewFromDTO.PresentPinCode.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PresentMobileNo == null) ? "NULL" : "'" + InterViewFromDTO.PresentMobileNo.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PermanentAddress == null) ? "NULL" : "'" + InterViewFromDTO.PermanentAddress.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PermanentPost == null) ? "NULL" : "'" + InterViewFromDTO.PermanentPost.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PermanentVillage == null) ? "NULL" : "'" + InterViewFromDTO.PermanentVillage.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PermanentDistrict == null) ? "NULL" : "'" + InterViewFromDTO.PermanentDistrict.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PermanentPinCode == null) ? "NULL" : "'" + InterViewFromDTO.PermanentPinCode.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PermanentMobileNo == null) ? "NULL" : "'" + InterViewFromDTO.PermanentMobileNo.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.DOB == null) ? "NULL" : "'" + ((DateTime)InterViewFromDTO.DOB).ToString("dd-MMM-yyyy") + "'") + "," +
                                 "" + ((InterViewFromDTO.BloodGroup == null) ? "NULL" : "'" + InterViewFromDTO.BloodGroup.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.AadharCardNo == null) ? "NULL" : "'" + InterViewFromDTO.AadharCardNo.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PanCardNo == null) ? "NULL" : "'" + InterViewFromDTO.PanCardNo.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.ElectionCardNo == null) ? "NULL" : "'" + InterViewFromDTO.ElectionCardNo.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.Category == null) ? "NULL" : "'" + InterViewFromDTO.Category.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.mailto == null) ? "NULL" : "'" + InterViewFromDTO.mailto.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.FatherName == null) ? "NULL" : "'" + InterViewFromDTO.FatherName.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.FatherOccupation == null) ? "NULL" : "'" + InterViewFromDTO.FatherOccupation.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.FatherEducation == null) ? "NULL" : "'" + InterViewFromDTO.FatherEducation.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.FatherMobileNo == null) ? "NULL" : "'" + InterViewFromDTO.FatherMobileNo.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.MotherName == null) ? "NULL" : "'" + InterViewFromDTO.MotherName.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.MotherOccupation == null) ? "NULL" : "'" + InterViewFromDTO.MotherOccupation.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.MotherEducation == null) ? "NULL" : "'" + InterViewFromDTO.MotherEducation.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.MotherMobileNo == null) ? "NULL" : "'" + InterViewFromDTO.MotherMobileNo.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.WifeName == null) ? "NULL" : "'" + InterViewFromDTO.WifeName.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.WifeOccupation == null) ? "NULL" : "'" + InterViewFromDTO.WifeOccupation.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.WifeEducation == null) ? "NULL" : "'" + InterViewFromDTO.WifeEducation.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.WifeMobileNo == null) ? "NULL" : "'" + InterViewFromDTO.WifeMobileNo.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.BrotherName == null) ? "NULL" : "'" + InterViewFromDTO.BrotherName.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.BrotherOccupation == null) ? "NULL" : "'" + InterViewFromDTO.BrotherOccupation.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.BrotherEducation == null) ? "NULL" : "'" + InterViewFromDTO.BrotherEducation.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.BrotherMobileNo == null) ? "NULL" : "'" + InterViewFromDTO.BrotherMobileNo.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.NomineeName == null) ? "NULL" : "'" + InterViewFromDTO.NomineeName.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.NomineeDOB == null) ? "NULL" : "'" + ((DateTime)InterViewFromDTO.NomineeDOB).ToString("dd-MMM-yyyy") + "'") + "," +
                                 "" + ((InterViewFromDTO.NomineeRelation == null) ? "NULL" : "'" + InterViewFromDTO.NomineeRelation.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.NomineeAge == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.NomineeAge) + "'") + "," +
                                 "" + ((InterViewFromDTO.Standanrd10Subject == null) ? "NULL" : "'" + InterViewFromDTO.Standanrd10Subject.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.Standanrd10PassingYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.Standanrd10PassingYear) + "'") + "," +
                                 "" + ((InterViewFromDTO.Standanrd10Percentage == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.Standanrd10Percentage) + "'") + "," +
                                 "" + ((InterViewFromDTO.Standanrd12Subject == null) ? "NULL" : "'" + InterViewFromDTO.Standanrd12Subject.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.Standanrd12PassingYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.Standanrd12PassingYear) + "'") + "," +
                                 "" + ((InterViewFromDTO.Standanrd12Percentage == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.Standanrd12Percentage) + "'") + "," +
                                 "" + ((InterViewFromDTO.GraduateSubject == null) ? "NULL" : "'" + InterViewFromDTO.GraduateSubject.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.GraduatePassingYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.GraduatePassingYear) + "'") + "," +
                                 "" + ((InterViewFromDTO.GraduatePercentage == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.GraduatePercentage) + "'") + "," +
                                 "" + ((InterViewFromDTO.PostGraduateSubject == null) ? "NULL" : "'" + InterViewFromDTO.PostGraduateSubject.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.PostGraduatePassingYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.PostGraduatePassingYear) + "'") + "," +
                                 "" + ((InterViewFromDTO.PostGraduatePercentage == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.PostGraduatePercentage) + "'") + "," +
                                 "" + ((InterViewFromDTO.OtherSubject == null) ? "NULL" : "'" + InterViewFromDTO.OtherSubject.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.OtherPassingYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.OtherPassingYear) + "'") + "," +
                                 "" + ((InterViewFromDTO.OtherPercentage == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.OtherPercentage) + "'") + "," +
                                 "" + ((InterViewFromDTO.CertificateCourseName == null) ? "NULL" : "'" + InterViewFromDTO.CertificateCourseName.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.CertificateCourseYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.CertificateCourseYear) + "'") + "," +
                                 "" + ((InterViewFromDTO.TrainingName == null) ? "NULL" : "'" + InterViewFromDTO.TrainingName.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.TrainingYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.TrainingYear) + "'") + "," +
                                 "" + ((InterViewFromDTO.MedalName == null) ? "NULL" : "'" + InterViewFromDTO.MedalName.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.MedalYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.MedalYear) + "'") + "," +
                                 "" + ((InterViewFromDTO.FirstCompanyName == null) ? "NULL" : "'" + InterViewFromDTO.FirstCompanyName.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.FirstCompanyDesignation == null) ? "NULL" : "'" + InterViewFromDTO.FirstCompanyDesignation.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.FirstCompanyExp == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.FirstCompanyExp) + "'") + "," +
                                 "" + ((InterViewFromDTO.FirstCompanySalary == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.FirstCompanySalary) + "'") + "," +
                                 "" + ((InterViewFromDTO.SecondCompanyName == null) ? "NULL" : "'" + InterViewFromDTO.SecondCompanyName.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.SecondCompanyDesignation == null) ? "NULL" : "'" + InterViewFromDTO.SecondCompanyDesignation.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.SecondCompanyExp == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.SecondCompanyExp) + "'") + "," +
                                 "" + ((InterViewFromDTO.SecondCompanySalary == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.SecondCompanySalary) + "'") + "," +
                                 "" + ((InterViewFromDTO.ThirdCompanyName == null) ? "NULL" : "'" + InterViewFromDTO.ThirdCompanyName.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.ThirdCompanyDesignation == null) ? "NULL" : "'" + InterViewFromDTO.ThirdCompanyDesignation.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.ThirdCompanyExp == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.ThirdCompanyExp) + "'") + "," +
                                 "" + ((InterViewFromDTO.ThirdCompanySalary == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.ThirdCompanySalary) + "'") + "," +
                                 "" + ((InterViewFromDTO.OtherExpNoExpDetails == null) ? "NULL" : "'" + InterViewFromDTO.OtherExpNoExpDetails.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.adharcard == null) ? "NULL" : "'" + InterViewFromDTO.adharcard.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.electioncard == null) ? "NULL" : "'" + InterViewFromDTO.electioncard.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.rationcard1 == null) ? "NULL" : "'" + InterViewFromDTO.rationcard1.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.rationcard2 == null) ? "NULL" : "'" + InterViewFromDTO.rationcard2.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.pancard == null) ? "NULL" : "'" + InterViewFromDTO.pancard.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.photo == null) ? "NULL" : "'" + InterViewFromDTO.photo.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.marksheet == null) ? "NULL" : "'" + InterViewFromDTO.marksheet.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.certificate == null) ? "NULL" : "'" + InterViewFromDTO.certificate.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.leavingcertificate1 == null) ? "NULL" : "'" + InterViewFromDTO.leavingcertificate1.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.leavingcertificate2 == null) ? "NULL" : "'" + InterViewFromDTO.leavingcertificate2.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.salaryslip == null) ? "NULL" : "'" + InterViewFromDTO.salaryslip.Replace("'", "''") + "'") + "," +
                                 "" + ((InterViewFromDTO.appointmentletter == null) ? "NULL" : "'" + InterViewFromDTO.appointmentletter.Replace("'", "''") + "'") + "," +
                                 " GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                 ");" +
                                 " SELECT @InterviewFormId";

            InterviewFormId = sqlCmd.ExecuteScalar().ToString();

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }

    public InterViewFromDTO Select(string InterviewFormId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        InterViewFromDTO InterViewFromDTO = new InterViewFromDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "select *  from InterviewForms  WHERE InterviewFormId='" + InterviewFormId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["InterviewFormId"] != DBNull.Value)
                InterViewFromDTO.InterviewFormId = sqlDr["InterviewFormId"].ToString();
            else
                InterViewFromDTO.InterviewFormId = null;

            if (sqlDr["FullName"] != DBNull.Value)
                InterViewFromDTO.FullName = sqlDr["FullName"].ToString();
            else
                InterViewFromDTO.FullName = null;

            if (sqlDr["PresentAddress"] != DBNull.Value)
                InterViewFromDTO.PresentAddress = sqlDr["PresentAddress"].ToString();
            else
                InterViewFromDTO.PresentAddress = null;

            if (sqlDr["PresentPost"] != DBNull.Value)
                InterViewFromDTO.PresentPost = sqlDr["PresentPost"].ToString();
            else
                InterViewFromDTO.PresentPost = null;

            if (sqlDr["PresentVillage"] != DBNull.Value)
                InterViewFromDTO.PresentVillage = sqlDr["PresentVillage"].ToString();
            else
                InterViewFromDTO.PresentVillage = null;

            if (sqlDr["PresentDistrict"] != DBNull.Value)
                InterViewFromDTO.PresentDistrict = sqlDr["PresentDistrict"].ToString();
            else
                InterViewFromDTO.PresentDistrict = null;

            if (sqlDr["PresentPinCode"] != DBNull.Value)
                InterViewFromDTO.PresentPinCode = sqlDr["PresentPinCode"].ToString();
            else
                InterViewFromDTO.PresentPinCode = null;

            if (sqlDr["PresentMobileNo"] != DBNull.Value)
                InterViewFromDTO.PresentMobileNo = sqlDr["PresentMobileNo"].ToString();
            else
                InterViewFromDTO.PresentMobileNo = null;

            if (sqlDr["PermanentAddress"] != DBNull.Value)
                InterViewFromDTO.PermanentAddress = sqlDr["PermanentAddress"].ToString();
            else
                InterViewFromDTO.PermanentAddress = null;

            if (sqlDr["PermanentPost"] != DBNull.Value)
                InterViewFromDTO.PermanentPost = sqlDr["PermanentPost"].ToString();
            else
                InterViewFromDTO.PermanentPost = null;

            if (sqlDr["PermanentVillage"] != DBNull.Value)
                InterViewFromDTO.PermanentVillage = sqlDr["PermanentVillage"].ToString();
            else
                InterViewFromDTO.PermanentVillage = null;

            if (sqlDr["PermanentDistrict"] != DBNull.Value)
                InterViewFromDTO.PermanentDistrict = sqlDr["PermanentDistrict"].ToString();
            else
                InterViewFromDTO.PermanentDistrict = null;

            if (sqlDr["PermanentPinCode"] != DBNull.Value)
                InterViewFromDTO.PermanentPinCode = sqlDr["PermanentPinCode"].ToString();
            else
                InterViewFromDTO.PermanentPinCode = null;

            if (sqlDr["PermanentMobileNo"] != DBNull.Value)
                InterViewFromDTO.PermanentMobileNo = sqlDr["PermanentMobileNo"].ToString();
            else
                InterViewFromDTO.PermanentMobileNo = null;

            if (sqlDr["DOB"] != DBNull.Value)
                InterViewFromDTO.DOB = Convert.ToDateTime(sqlDr["DOB"]);
            else
                InterViewFromDTO.DOB = null;

            if (sqlDr["BloodGroup"] != DBNull.Value)
                InterViewFromDTO.BloodGroup = sqlDr["BloodGroup"].ToString();
            else
                InterViewFromDTO.BloodGroup = null;

            if (sqlDr["AadharCardNo"] != DBNull.Value)
                InterViewFromDTO.AadharCardNo = sqlDr["AadharCardNo"].ToString();
            else
                InterViewFromDTO.AadharCardNo = null;

            if (sqlDr["PanCardNo"] != DBNull.Value)
                InterViewFromDTO.PanCardNo = sqlDr["PanCardNo"].ToString();
            else
                InterViewFromDTO.PanCardNo = null;

            if (sqlDr["ElectionCardNo"] != DBNull.Value)
                InterViewFromDTO.ElectionCardNo = sqlDr["ElectionCardNo"].ToString();
            else
                InterViewFromDTO.ElectionCardNo = null;

            if (sqlDr["Category"] != DBNull.Value)
                InterViewFromDTO.Category = sqlDr["Category"].ToString();
            else
                InterViewFromDTO.Category = null;

            if (sqlDr["Email"] != DBNull.Value)
                InterViewFromDTO.mailto = sqlDr["Email"].ToString();
            else
                InterViewFromDTO.mailto = null;

            if (sqlDr["FatherName"] != DBNull.Value)
                InterViewFromDTO.FatherName = sqlDr["FatherName"].ToString();
            else
                InterViewFromDTO.FatherName = null;

            if (sqlDr["FatherOccupation"] != DBNull.Value)
                InterViewFromDTO.FatherOccupation = sqlDr["FatherOccupation"].ToString();
            else
                InterViewFromDTO.FatherOccupation = null;

            if (sqlDr["FatherEducation"] != DBNull.Value)
                InterViewFromDTO.FatherEducation = sqlDr["FatherEducation"].ToString();
            else
                InterViewFromDTO.FatherEducation = null;

            if (sqlDr["FatherMobileNo"] != DBNull.Value)
                InterViewFromDTO.FatherMobileNo = sqlDr["FatherMobileNo"].ToString();
            else
                InterViewFromDTO.FatherMobileNo = null;

            if (sqlDr["MotherName"] != DBNull.Value)
                InterViewFromDTO.MotherName = sqlDr["MotherName"].ToString();
            else
                InterViewFromDTO.MotherName = null;

            if (sqlDr["MotherOccupation"] != DBNull.Value)
                InterViewFromDTO.MotherOccupation = sqlDr["MotherOccupation"].ToString();
            else
                InterViewFromDTO.MotherOccupation = null;

            if (sqlDr["MotherEducation"] != DBNull.Value)
                InterViewFromDTO.MotherEducation = sqlDr["MotherEducation"].ToString();
            else
                InterViewFromDTO.MotherEducation = null;

            if (sqlDr["MotherMobileNo"] != DBNull.Value)
                InterViewFromDTO.MotherMobileNo = sqlDr["MotherMobileNo"].ToString();
            else
                InterViewFromDTO.MotherMobileNo = null;
            if (sqlDr["WifeName"] != DBNull.Value)
                InterViewFromDTO.WifeName = sqlDr["WifeName"].ToString();
            else
                InterViewFromDTO.WifeName = null;

            if (sqlDr["WifeOccupation"] != DBNull.Value)
                InterViewFromDTO.WifeOccupation = sqlDr["WifeOccupation"].ToString();
            else
                InterViewFromDTO.WifeOccupation = null;

            if (sqlDr["WifeEducation"] != DBNull.Value)
                InterViewFromDTO.WifeEducation = sqlDr["WifeEducation"].ToString();
            else
                InterViewFromDTO.WifeEducation = null;

            if (sqlDr["WifeMobileNo"] != DBNull.Value)
                InterViewFromDTO.WifeMobileNo = sqlDr["WifeMobileNo"].ToString();
            else
                InterViewFromDTO.WifeMobileNo = null;

            if (sqlDr["BrotherName"] != DBNull.Value)
                InterViewFromDTO.BrotherName = sqlDr["BrotherName"].ToString();
            else
                InterViewFromDTO.BrotherName = null;

            if (sqlDr["BrotherOccupation"] != DBNull.Value)
                InterViewFromDTO.BrotherOccupation = sqlDr["BrotherOccupation"].ToString();
            else
                InterViewFromDTO.BrotherOccupation = null;

            if (sqlDr["BrotherEducation"] != DBNull.Value)
                InterViewFromDTO.BrotherEducation = sqlDr["BrotherEducation"].ToString();
            else
                InterViewFromDTO.BrotherEducation = null;

            if (sqlDr["BrotherMobileNo"] != DBNull.Value)
                InterViewFromDTO.BrotherMobileNo = sqlDr["BrotherMobileNo"].ToString();
            else
                InterViewFromDTO.BrotherMobileNo = null;

            if (sqlDr["NomineeName"] != DBNull.Value)
                InterViewFromDTO.NomineeName = sqlDr["NomineeName"].ToString();
            else
                InterViewFromDTO.NomineeName = null;

            if (sqlDr["NomineeDOB"] != DBNull.Value)
                InterViewFromDTO.NomineeDOB = Convert.ToDateTime(sqlDr["NomineeDOB"]);
            else
                InterViewFromDTO.NomineeDOB = null;

            if (sqlDr["NomineeRelation"] != DBNull.Value)
                InterViewFromDTO.NomineeRelation = sqlDr["NomineeRelation"].ToString();
            else
                InterViewFromDTO.NomineeRelation = null;

            if (sqlDr["NomineeAge"] != DBNull.Value)
                InterViewFromDTO.NomineeAge = Convert.ToInt16(sqlDr["NomineeAge"]);
            else
                InterViewFromDTO.NomineeAge = null;

            if (sqlDr["Standanrd10Subject"] != DBNull.Value)
                InterViewFromDTO.Standanrd10Subject = sqlDr["Standanrd10Subject"].ToString();
            else
                InterViewFromDTO.Standanrd10Subject = null;

            if (sqlDr["Standanrd10PassingYear"] != DBNull.Value)
                InterViewFromDTO.Standanrd10PassingYear = Convert.ToInt16(sqlDr["Standanrd10PassingYear"]);
            else
                InterViewFromDTO.Standanrd10PassingYear = null;

            if (sqlDr["Standanrd10Percentage"] != DBNull.Value)
                InterViewFromDTO.Standanrd10Percentage = Convert.ToDecimal(sqlDr["Standanrd10Percentage"]);
            else
                InterViewFromDTO.Standanrd10Percentage = null;

            if (sqlDr["Standanrd12Subject"] != DBNull.Value)
                InterViewFromDTO.Standanrd12Subject = sqlDr["Standanrd12Subject"].ToString();
            else
                InterViewFromDTO.Standanrd12Subject = null;

            if (sqlDr["Standanrd12PassingYear"] != DBNull.Value)
                InterViewFromDTO.Standanrd12PassingYear = Convert.ToInt16(sqlDr["Standanrd12PassingYear"]);
            else
                InterViewFromDTO.Standanrd12PassingYear = null;

            if (sqlDr["Standanrd12Percentage"] != DBNull.Value)
                InterViewFromDTO.Standanrd12Percentage = Convert.ToDecimal(sqlDr["Standanrd12Percentage"]);
            else
                InterViewFromDTO.Standanrd12Percentage = null;

            if (sqlDr["GraduateSubject"] != DBNull.Value)
                InterViewFromDTO.GraduateSubject = sqlDr["GraduateSubject"].ToString();
            else
                InterViewFromDTO.GraduateSubject = null;

            if (sqlDr["GraduatePassingYear"] != DBNull.Value)
                InterViewFromDTO.GraduatePassingYear = Convert.ToInt16(sqlDr["GraduatePassingYear"]);
            else
                InterViewFromDTO.GraduatePassingYear = null;

            if (sqlDr["GraduatePercentage"] != DBNull.Value)
                InterViewFromDTO.GraduatePercentage = Convert.ToDecimal(sqlDr["GraduatePercentage"]);
            else
                InterViewFromDTO.GraduatePercentage = null;

            if (sqlDr["PostGraduateSubject"] != DBNull.Value)
                InterViewFromDTO.PostGraduateSubject = sqlDr["PostGraduateSubject"].ToString();
            else
                InterViewFromDTO.PostGraduateSubject = null;

            if (sqlDr["PostGraduatePassingYear"] != DBNull.Value)
                InterViewFromDTO.PostGraduatePassingYear = Convert.ToInt16(sqlDr["PostGraduatePassingYear"]);
            else
                InterViewFromDTO.PostGraduatePassingYear = null;

            if (sqlDr["PostGraduatePercentage"] != DBNull.Value)
                InterViewFromDTO.PostGraduatePercentage = Convert.ToDecimal(sqlDr["PostGraduatePercentage"]);
            else
                InterViewFromDTO.PostGraduatePercentage = null;

            if (sqlDr["OtherSubject"] != DBNull.Value)
                InterViewFromDTO.OtherSubject = sqlDr["OtherSubject"].ToString();
            else
                InterViewFromDTO.OtherSubject = null;

            if (sqlDr["OtherPassingYear"] != DBNull.Value)
                InterViewFromDTO.OtherPassingYear = Convert.ToInt16(sqlDr["OtherPassingYear"]);
            else
                InterViewFromDTO.OtherPassingYear = null;

            if (sqlDr["OtherPercentage"] != DBNull.Value)
                InterViewFromDTO.OtherPercentage = Convert.ToDecimal(sqlDr["OtherPercentage"]);
            else
                InterViewFromDTO.OtherPercentage = null;

            if (sqlDr["CertificateCourseName"] != DBNull.Value)
                InterViewFromDTO.CertificateCourseName = sqlDr["CertificateCourseName"].ToString();
            else
                InterViewFromDTO.CertificateCourseName = null;

            if (sqlDr["CertificateCourseYear"] != DBNull.Value)
                InterViewFromDTO.CertificateCourseYear = Convert.ToInt16(sqlDr["CertificateCourseYear"]);
            else
                InterViewFromDTO.CertificateCourseYear = null;

            if (sqlDr["TrainingName"] != DBNull.Value)
                InterViewFromDTO.TrainingName = sqlDr["TrainingName"].ToString();
            else
                InterViewFromDTO.TrainingName = null;

            if (sqlDr["TrainingYear"] != DBNull.Value)
                InterViewFromDTO.TrainingYear = Convert.ToInt16(sqlDr["TrainingYear"]);
            else
                InterViewFromDTO.TrainingYear = null;

            if (sqlDr["MedalName"] != DBNull.Value)
                InterViewFromDTO.MedalName = sqlDr["MedalName"].ToString();
            else
                InterViewFromDTO.MedalName = null;

            if (sqlDr["MedalYear"] != DBNull.Value)
                InterViewFromDTO.MedalYear = Convert.ToInt16(sqlDr["MedalYear"]);
            else
                InterViewFromDTO.MedalYear = null;

            if (sqlDr["FirstCompanyName"] != DBNull.Value)
                InterViewFromDTO.FirstCompanyName = sqlDr["FirstCompanyName"].ToString();
            else
                InterViewFromDTO.FirstCompanyName = null;

            if (sqlDr["FirstCompanyDesignation"] != DBNull.Value)
                InterViewFromDTO.FirstCompanyDesignation = sqlDr["FirstCompanyDesignation"].ToString();
            else
                InterViewFromDTO.FirstCompanyDesignation = null;

            if (sqlDr["FirstCompanyExp"] != DBNull.Value)
                InterViewFromDTO.FirstCompanyExp = Convert.ToDecimal(sqlDr["FirstCompanyExp"]);
            else
                InterViewFromDTO.FirstCompanyExp = null;

            if (sqlDr["FirstCompanySalary"] != DBNull.Value)
                InterViewFromDTO.FirstCompanySalary = Convert.ToDecimal(sqlDr["FirstCompanySalary"]);
            else
                InterViewFromDTO.FirstCompanySalary = null;

            if (sqlDr["SecondCompanyName"] != DBNull.Value)
                InterViewFromDTO.SecondCompanyName = sqlDr["SecondCompanyName"].ToString();
            else
                InterViewFromDTO.SecondCompanyName = null;

            if (sqlDr["SecondCompanyDesignation"] != DBNull.Value)
                InterViewFromDTO.FirstCompanyDesignation = sqlDr["SecondCompanyDesignation"].ToString();
            else
                InterViewFromDTO.FirstCompanyDesignation = null;

            if (sqlDr["SecondCompanyExp"] != DBNull.Value)
                InterViewFromDTO.SecondCompanyExp = Convert.ToDecimal(sqlDr["SecondCompanyExp"]);
            else
                InterViewFromDTO.SecondCompanyExp = null;

            if (sqlDr["SecondCompanySalary"] != DBNull.Value)
                InterViewFromDTO.SecondCompanySalary = Convert.ToDecimal(sqlDr["SecondCompanySalary"]);
            else
                InterViewFromDTO.SecondCompanySalary = null;

            if (sqlDr["ThirdCompanyName"] != DBNull.Value)
                InterViewFromDTO.ThirdCompanyName = sqlDr["ThirdCompanyName"].ToString();
            else
                InterViewFromDTO.ThirdCompanyName = null;

            if (sqlDr["ThirdCompanyDesignation"] != DBNull.Value)
                InterViewFromDTO.ThirdCompanyDesignation = sqlDr["ThirdCompanyDesignation"].ToString();
            else
                InterViewFromDTO.ThirdCompanyDesignation = null;

            if (sqlDr["ThirdCompanyExp"] != DBNull.Value)
                InterViewFromDTO.ThirdCompanyExp = Convert.ToDecimal(sqlDr["ThirdCompanyExp"]);
            else
                InterViewFromDTO.ThirdCompanyExp = null;

            if (sqlDr["ThirdCompanySalary"] != DBNull.Value)
                InterViewFromDTO.ThirdCompanySalary = Convert.ToDecimal(sqlDr["ThirdCompanySalary"]);
            else
                InterViewFromDTO.ThirdCompanySalary = null;

            if (sqlDr["OtherExpNoExpDetails"] != DBNull.Value)
                InterViewFromDTO.OtherExpNoExpDetails = sqlDr["OtherExpNoExpDetails"].ToString();
            else
                InterViewFromDTO.OtherExpNoExpDetails = null;

        }

        sqlDr.Close();

        _generalDAL.CloseSQLConnection();

        return InterViewFromDTO;
    }

    public void Update(InterViewFromDTO InterViewFromDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = " UPDATE InterviewForms SET" +
                                  " FullName  = " + ((InterViewFromDTO.FullName == null) ? "NULL" : "'" + InterViewFromDTO.FullName.Replace("'", "''") + "'") + "," +
                                  " PresentAddress = " + ((InterViewFromDTO.PresentAddress == null) ? "NULL" : "'" + InterViewFromDTO.PresentAddress.Replace("'", "''") + "'") + "," +
                                  " PresentPost  = " + ((InterViewFromDTO.PresentPost == null) ? "NULL" : "'" + InterViewFromDTO.PresentPost.Replace("'", "''") + "'") + "," +
                                  " PresentVillage  = " + ((InterViewFromDTO.PresentVillage == null) ? "NULL" : "'" + InterViewFromDTO.PresentVillage.Replace("'", "''") + "'") + "," +
                                  " PresentDistrict  = " + ((InterViewFromDTO.PresentDistrict == null) ? "NULL" : "'" + InterViewFromDTO.PresentDistrict.Replace("'", "''") + "'") + "," +
                                  " PresentPinCode  = " + ((InterViewFromDTO.PresentPinCode == null) ? "NULL" : "'" + InterViewFromDTO.PresentPinCode.Replace("'", "''") + "'") + "," +
                                  " PresentMobileNo  = " + ((InterViewFromDTO.PresentMobileNo == null) ? "NULL" : "'" + InterViewFromDTO.PresentMobileNo.Replace("'", "''") + "'") + "," +
                                  " PermanentAddress  = " + ((InterViewFromDTO.PermanentAddress == null) ? "NULL" : "'" + InterViewFromDTO.PermanentAddress.Replace("'", "''") + "'") + "," +
                                  " PermanentPost  = " + ((InterViewFromDTO.PermanentPost == null) ? "NULL" : "'" + InterViewFromDTO.PermanentPost.Replace("'", "''") + "'") + "," +
                                  " PermanentVillage  = " + ((InterViewFromDTO.PermanentVillage == null) ? "NULL" : "'" + InterViewFromDTO.PermanentVillage.Replace("'", "''") + "'") + "," +
                                  " PermanentDistrict  = " + ((InterViewFromDTO.PermanentDistrict == null) ? "NULL" : "'" + InterViewFromDTO.PermanentDistrict.Replace("'", "''") + "'") + "," +
                                  " PermanentPinCode  = " + ((InterViewFromDTO.PermanentPinCode == null) ? "NULL" : "'" + InterViewFromDTO.PermanentPinCode.Replace("'", "''") + "'") + "," +
                                  " PermanentMobileNo  = " + ((InterViewFromDTO.PermanentMobileNo == null) ? "NULL" : "'" + InterViewFromDTO.PermanentMobileNo.Replace("'", "''") + "'") + "," +
                                  " DOB  = " + ((InterViewFromDTO.DOB == null) ? "NULL" : "'" + ((DateTime)InterViewFromDTO.DOB).ToString("dd-MMM-yyyy") + "'") + "," +
                                  " BloodGroup  = " + ((InterViewFromDTO.BloodGroup == null) ? "NULL" : "'" + InterViewFromDTO.BloodGroup.Replace("'", "''") + "'") + "," +
                                  " AadharCardNo  = " + ((InterViewFromDTO.AadharCardNo == null) ? "NULL" : "'" + InterViewFromDTO.AadharCardNo.Replace("'", "''") + "'") + "," +
                                  " PanCardNo  = " + ((InterViewFromDTO.PanCardNo == null) ? "NULL" : "'" + InterViewFromDTO.PanCardNo.Replace("'", "''") + "'") + "," +
                                  " ElectionCardNo  = " + ((InterViewFromDTO.ElectionCardNo == null) ? "NULL" : "'" + InterViewFromDTO.ElectionCardNo.Replace("'", "''") + "'") + "," +
                                  " Category  = " + ((InterViewFromDTO.Category == null) ? "NULL" : "'" + InterViewFromDTO.Category.Replace("'", "''") + "'") + "," +
                                  " Email  = " + ((InterViewFromDTO.mailto == null) ? "NULL" : "'" + InterViewFromDTO.mailto.Replace("'", "''") + "'") + "," +
                                  " FatherName  = " + ((InterViewFromDTO.FatherName == null) ? "NULL" : "'" + InterViewFromDTO.FatherName.Replace("'", "''") + "'") + "," +
                                  " FatherOccupation  = " + ((InterViewFromDTO.FatherOccupation == null) ? "NULL" : "'" + InterViewFromDTO.FatherOccupation.Replace("'", "''") + "'") + "," +
                                  " FatherEducation  = " + ((InterViewFromDTO.FatherEducation == null) ? "NULL" : "'" + InterViewFromDTO.FatherEducation.Replace("'", "''") + "'") + "," +
                                  " FatherMobileNo  = " + ((InterViewFromDTO.FatherMobileNo == null) ? "NULL" : "'" + InterViewFromDTO.FatherMobileNo.Replace("'", "''") + "'") + "," +
                                  " MotherName  = " + ((InterViewFromDTO.MotherName == null) ? "NULL" : "'" + InterViewFromDTO.MotherName.Replace("'", "''") + "'") + "," +
                                  " MotherOccupation  = " + ((InterViewFromDTO.MotherOccupation == null) ? "NULL" : "'" + InterViewFromDTO.MotherOccupation.Replace("'", "''") + "'") + "," +
                                  " MotherEducation  = " + ((InterViewFromDTO.MotherEducation == null) ? "NULL" : "'" + InterViewFromDTO.MotherEducation.Replace("'", "''") + "'") + "," +
                                  " MotherMobileNo  = " + ((InterViewFromDTO.MotherMobileNo == null) ? "NULL" : "'" + InterViewFromDTO.MotherMobileNo.Replace("'", "''") + "'") + "," +
                                  " WifeName  = " + ((InterViewFromDTO.WifeName == null) ? "NULL" : "'" + InterViewFromDTO.WifeName.Replace("'", "''") + "'") + "," +
                                  " WifeOccupation  = " + ((InterViewFromDTO.WifeOccupation == null) ? "NULL" : "'" + InterViewFromDTO.WifeOccupation.Replace("'", "''") + "'") + "," +
                                  " WifeEducation  = " + ((InterViewFromDTO.WifeEducation == null) ? "NULL" : "'" + InterViewFromDTO.WifeEducation.Replace("'", "''") + "'") + "," +
                                  " WifeMobileNo  = " + ((InterViewFromDTO.WifeMobileNo == null) ? "NULL" : "'" + InterViewFromDTO.WifeMobileNo.Replace("'", "''") + "'") + "," +
                                  " BrotherName  = " + ((InterViewFromDTO.BrotherName == null) ? "NULL" : "'" + InterViewFromDTO.BrotherName.Replace("'", "''") + "'") + "," +
                                  " BrotherOccupation  = " + ((InterViewFromDTO.BrotherOccupation == null) ? "NULL" : "'" + InterViewFromDTO.BrotherOccupation.Replace("'", "''") + "'") + "," +
                                  " BrotherEducation  = " + ((InterViewFromDTO.BrotherEducation == null) ? "NULL" : "'" + InterViewFromDTO.BrotherEducation.Replace("'", "''") + "'") + "," +
                                  " BrotherMobileNo  = " + ((InterViewFromDTO.BrotherMobileNo == null) ? "NULL" : "'" + InterViewFromDTO.BrotherMobileNo.Replace("'", "''") + "'") + "," +
                                  " NomineeName  = " + ((InterViewFromDTO.NomineeName == null) ? "NULL" : "'" + InterViewFromDTO.NomineeName.Replace("'", "''") + "'") + "," +
                                  " NomineeDOB  = " + ((InterViewFromDTO.NomineeDOB == null) ? "NULL" : "'" + ((DateTime)InterViewFromDTO.NomineeDOB).ToString("dd-MMM-yyyy") + "'") + "," +
                                  " NomineeRelation  = " + ((InterViewFromDTO.NomineeRelation == null) ? "NULL" : "'" + InterViewFromDTO.NomineeRelation.Replace("'", "''") + "'") + "," +
                                  " NomineeAge  = " + ((InterViewFromDTO.NomineeAge == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.NomineeAge) + "'") + "," +
                                  " Standanrd10Subject  = " + ((InterViewFromDTO.Standanrd10Subject == null) ? "NULL" : "'" + InterViewFromDTO.Standanrd10Subject.Replace("'", "''") + "'") + "," +
                                  " Standanrd10PassingYear  = " + ((InterViewFromDTO.Standanrd10PassingYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.Standanrd10PassingYear) + "'") + "," +
                                  " Standanrd10Percentage  = " + ((InterViewFromDTO.Standanrd10Percentage == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.Standanrd10Percentage) + "'") + "," +
                                  " Standanrd12Subject  = " + ((InterViewFromDTO.Standanrd12Subject == null) ? "NULL" : "'" + InterViewFromDTO.Standanrd12Subject.Replace("'", "''") + "'") + "," +
                                  " Standanrd12PassingYear  = " + ((InterViewFromDTO.Standanrd12PassingYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.Standanrd12PassingYear) + "'") + "," +
                                  " Standanrd12Percentage  = " + ((InterViewFromDTO.Standanrd12Percentage == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.Standanrd12Percentage) + "'") + "," +
                                  " GraduateSubject  = " + ((InterViewFromDTO.GraduateSubject == null) ? "NULL" : "'" + InterViewFromDTO.GraduateSubject.Replace("'", "''") + "'") + "," +
                                  " GraduatePassingYear  = " + ((InterViewFromDTO.GraduatePassingYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.GraduatePassingYear) + "'") + "," +
                                  " GraduatePercentage  = " + ((InterViewFromDTO.GraduatePercentage == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.GraduatePercentage) + "'") + "," +
                                  " PostGraduateSubject  = " + ((InterViewFromDTO.PostGraduateSubject == null) ? "NULL" : "'" + InterViewFromDTO.PostGraduateSubject.Replace("'", "''") + "'") + "," +
                                  " PostGraduatePassingYear  = " + ((InterViewFromDTO.PostGraduatePassingYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.PostGraduatePassingYear) + "'") + "," +
                                  " PostGraduatePercentage  = " + ((InterViewFromDTO.PostGraduatePercentage == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.PostGraduatePercentage) + "'") + "," +
                                  " OtherSubject  = " + ((InterViewFromDTO.OtherSubject == null) ? "NULL" : "'" + InterViewFromDTO.OtherSubject.Replace("'", "''") + "'") + "," +
                                  " OtherPassingYear  = " + ((InterViewFromDTO.OtherPassingYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.OtherPassingYear) + "'") + "," +
                                  " OtherPercentage  = " + ((InterViewFromDTO.OtherPercentage == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.OtherPercentage) + "'") + "," +
                                  " CertificateCourseName  = " + ((InterViewFromDTO.CertificateCourseName == null) ? "NULL" : "'" + InterViewFromDTO.CertificateCourseName.Replace("'", "''") + "'") + "," +
                                  " CertificateCourseYear  = " + ((InterViewFromDTO.CertificateCourseYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.CertificateCourseYear) + "'") + "," +
                                  " TrainingName  = " + ((InterViewFromDTO.TrainingName == null) ? "NULL" : "'" + InterViewFromDTO.TrainingName.Replace("'", "''") + "'") + "," +
                                  " TrainingYear  = " + ((InterViewFromDTO.TrainingYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.TrainingYear) + "'") + "," +
                                  " MedalName  = " + ((InterViewFromDTO.MedalName == null) ? "NULL" : "'" + InterViewFromDTO.MedalName.Replace("'", "''") + "'") + "," +
                                  " MedalYear  = " + ((InterViewFromDTO.MedalYear == null) ? "NULL" : "'" + Convert.ToInt16(InterViewFromDTO.MedalYear) + "'") + "," +
                                  " FirstCompanyName  = " + ((InterViewFromDTO.FirstCompanyName == null) ? "NULL" : "'" + InterViewFromDTO.FirstCompanyName.Replace("'", "''") + "'") + "," +
                                  " FirstCompanyDesignation  = " + ((InterViewFromDTO.FirstCompanyDesignation == null) ? "NULL" : "'" + InterViewFromDTO.FirstCompanyDesignation.Replace("'", "''") + "'") + "," +
                                  " FirstCompanyExp  = " + ((InterViewFromDTO.FirstCompanyExp == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.FirstCompanyExp) + "'") + "," +
                                  " FirstCompanySalary  = " + ((InterViewFromDTO.FirstCompanySalary == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.FirstCompanySalary) + "'") + "," +
                                  " SecondCompanyName  = " + ((InterViewFromDTO.SecondCompanyName == null) ? "NULL" : "'" + InterViewFromDTO.SecondCompanyName.Replace("'", "''") + "'") + "," +
                                  " SecondCompanyDesignation  = " + ((InterViewFromDTO.SecondCompanyDesignation == null) ? "NULL" : "'" + InterViewFromDTO.SecondCompanyDesignation.Replace("'", "''") + "'") + "," +
                                  " SecondCompanyExp  = " + ((InterViewFromDTO.SecondCompanyExp == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.SecondCompanyExp) + "'") + "," +
                                  " SecondCompanySalary  = " + ((InterViewFromDTO.SecondCompanySalary == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.SecondCompanySalary) + "'") + "," +
                                  " ThirdCompanyName  = " + ((InterViewFromDTO.ThirdCompanyName == null) ? "NULL" : "'" + InterViewFromDTO.ThirdCompanyName.Replace("'", "''") + "'") + "," +
                                  " ThirdCompanyDesignation  = " + ((InterViewFromDTO.ThirdCompanyDesignation == null) ? "NULL" : "'" + InterViewFromDTO.ThirdCompanyDesignation.Replace("'", "''") + "'") + "," +
                                  " ThirdCompanyExp  = " + ((InterViewFromDTO.ThirdCompanyExp == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.ThirdCompanyExp) + "'") + "," +
                                  " ThirdCompanySalary  = " + ((InterViewFromDTO.ThirdCompanySalary == null) ? "NULL" : "'" + Convert.ToDecimal(InterViewFromDTO.ThirdCompanySalary) + "'") + "," +
                                  " OtherExpNoExpDetails  = " + ((InterViewFromDTO.OtherExpNoExpDetails == null) ? "NULL" : "'" + InterViewFromDTO.OtherExpNoExpDetails.Replace("'", "''") + "'") + "," +
                                  " LastUpdatedOn = getdate() " +
                                  ", LastUpdatedByUserId = " + ((MySession.UserUnique == null) ? "NULL" : "'" + MySession.UserUnique + "'") +
                                  ",Document1Path1 = " + ((InterViewFromDTO.adharcard == null) ? "NULL" : "'" + InterViewFromDTO.adharcard.Replace("'", "''") + "'") + "," +
                                  " Document1Path2 = " + ((InterViewFromDTO.electioncard == null) ? "NULL" : "'" + InterViewFromDTO.electioncard.Replace("'", "''") + "'") + "," +
                                  " Document2Path1 = " + ((InterViewFromDTO.rationcard1 == null) ? "NULL" : "'" + InterViewFromDTO.rationcard1.Replace("'", "''") + "'") + "," +
                                  " Document2Path2 = " + ((InterViewFromDTO.rationcard2 == null) ? "NULL" : "'" + InterViewFromDTO.rationcard2.Replace("'", "''") + "'") + "," +
                                  " Document3Path1 = " + ((InterViewFromDTO.pancard == null) ? "NULL" : "'" + InterViewFromDTO.pancard.Replace("'", "''") + "'") + "," +
                                  " Document3Path2 = " + ((InterViewFromDTO.photo == null) ? "NULL" : "'" + InterViewFromDTO.photo.Replace("'", "''") + "'") + "," +
                                  " Document4Path1 = " + ((InterViewFromDTO.marksheet == null) ? "NULL" : "'" + InterViewFromDTO.marksheet.Replace("'", "''") + "'") + "," +
                                  " Document4Path2 = " + ((InterViewFromDTO.certificate == null) ? "NULL" : "'" + InterViewFromDTO.certificate.Replace("'", "''") + "'") + "," +
                                  " Document5Path1 = " + ((InterViewFromDTO.leavingcertificate1 == null) ? "NULL" : "'" + InterViewFromDTO.leavingcertificate1.Replace("'", "''") + "'") + "," +
                                  " Document5Path2 = " + ((InterViewFromDTO.leavingcertificate2 == null) ? "NULL" : "'" + InterViewFromDTO.leavingcertificate2.Replace("'", "''") + "'") + "," +
                                  " Document6Path1 = " + ((InterViewFromDTO.salaryslip == null) ? "NULL" : "'" + InterViewFromDTO.salaryslip.Replace("'", "''") + "'") + "," +
                                  " Document6Path2 = " + ((InterViewFromDTO.appointmentletter == null) ? "NULL" : "'" + InterViewFromDTO.appointmentletter.Replace("'", "''") + "'") +
                                  " WHERE InterviewFormId = '" + InterViewFromDTO.InterviewFormId + "'";

            sqlCmd.ExecuteNonQuery();

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }

    public void Delete(string InterviewFormId)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;
        try
        {
            string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..InterviewForms(InterviewFormId,FullName,PresentAddress,PresentPost,PresentVillage,PresentDistrict,PresentPinCode,PresentMobileNo, " +
                                 " PermanentAddress,PermanentPost,PermanentVillage,PermanentDistrict,PermanentPinCode,PermanentMobileNo,DOB,BloodGroup,AadharCardNo,PanCardNo,ElectionCardNo,Category,Email, " +
                                 " FatherName,FatherOccupation,FatherEducation,FatherMobileNo,MotherName,MotherOccupation,MotherEducation,MotherMobileNo, " +
                                 " WifeName,WifeOccupation,WifeEducation,WifeMobileNo,BrotherName,BrotherOccupation,BrotherEducation,BrotherMobileNo,  " +
                                 " NomineeName,NomineeDOB,NomineeRelation,NomineeAge,Standanrd10Subject,Standanrd10PassingYear,Standanrd10Percentage , " +
                                 " Standanrd12Subject,Standanrd12PassingYear,Standanrd12Percentage,GraduateSubject,GraduatePassingYear,GraduatePercentage , " +
                                 " PostGraduateSubject,PostGraduatePassingYear,PostGraduatePercentage,OtherSubject,OtherPassingYear,OtherPercentage,CertificateCourseName,CertificateCourseYear,TrainingName,TrainingYear,MedalName,MedalYear, " +
                                 " FirstCompanyName,FirstCompanyDesignation,FirstCompanyExp,FirstCompanySalary,SecondCompanyName,SecondCompanyDesignation,SecondCompanyExp,SecondCompanySalary,ThirdCompanyName,ThirdCompanyDesignation,ThirdCompanyExp,ThirdCompanySalary,  " +
                                 " OtherExpNoExpDetails,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId ,Document1Path1,Document1Path2,Document2Path1,Document2Path2,Document3Path1,Document3Path2,Document4Path1,Document4Path2,Document5Path1,Document5Path2,Document6Path1,Document6Path2)" +
                                 " Select InterviewFormId,FullName,PresentAddress,PresentPost,PresentVillage,PresentDistrict,PresentPinCode,PresentMobileNo, " +
                                 " PermanentAddress,PermanentPost,PermanentVillage,PermanentDistrict,PermanentPinCode,PermanentMobileNo,DOB,BloodGroup,AadharCardNo,PanCardNo,ElectionCardNo,Category,Email, " +
                                 " FatherName,FatherOccupation,FatherEducation,FatherMobileNo,MotherName,MotherOccupation,MotherEducation,MotherMobileNo, " +
                                 " WifeName,WifeOccupation,WifeEducation,WifeMobileNo,BrotherName,BrotherOccupation,BrotherEducation,BrotherMobileNo,  " +
                                 " NomineeName,NomineeDOB,NomineeRelation,NomineeAge,Standanrd10Subject,Standanrd10PassingYear,Standanrd10Percentage , " +
                                 " Standanrd12Subject,Standanrd12PassingYear,Standanrd12Percentage,GraduateSubject,GraduatePassingYear,GraduatePercentage , " +
                                 " PostGraduateSubject,PostGraduatePassingYear,PostGraduatePercentage,OtherSubject,OtherPassingYear,OtherPercentage,CertificateCourseName,CertificateCourseYear,TrainingName,TrainingYear,MedalName,MedalYear, " +
                                 " FirstCompanyName,FirstCompanyDesignation,FirstCompanyExp,FirstCompanySalary,SecondCompanyName,SecondCompanyDesignation,SecondCompanyExp,SecondCompanySalary,ThirdCompanyName,ThirdCompanyDesignation,ThirdCompanyExp,ThirdCompanySalary,  " +
                                 " OtherExpNoExpDetails,getdate(),LastUpdatedOn,'" + MySession.UserUnique + "',LastUpdatedByUserId, Document1Path1,Document1Path2,Document2Path1,Document2Path2,Document3Path1,Document3Path2,Document4Path1,Document4Path2,Document5Path1,Document5Path2,Document6Path1,Document6Path2 " +
                                 " FROM InterviewForms WHERE InterviewFormId='" + InterviewFormId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM InterviewForms WHERE InterviewFormId='" + InterviewFormId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public bool NamesExists(string StandardId, string StudentName, string MobileNo)
    {
        //Escape single quote
        StudentName = StudentName.Replace("'", "''");
        MobileNo = MobileNo.Replace("'", "''");
        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Registration WHERE [FirstName] = '" + StudentName + "' and StandardId = '" + StandardId + "' and MobileNo = '" + MobileNo + "'";

            if (Convert.ToInt32(sqlCmd.ExecuteScalar()) > 0)
            {
                _generalDAL.CloseSQLConnection();
                return true;
            }
            else
            {
                _generalDAL.CloseSQLConnection();
                return false;
            }
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }
    public bool NamesExists(string StandardId, string StudentName, string MobileNo, string RegistrationId)
    {
        try
        {
            //Escape single quote
            StudentName = StudentName.Replace("'", "''");
            MobileNo = MobileNo.Replace("'", "''");
            //Escape single quote

            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Registration WHERE [FirstName] = '" + StudentName + "' and StandardId = '" + StandardId + "' and MobileNo = '" + MobileNo + "' AND NOT RegistrationId='" + RegistrationId + "'";

            if (Convert.ToInt32(sqlCmd.ExecuteScalar()) > 0)
            {
                _generalDAL.CloseSQLConnection();
                return true;
            }
            else
            {
                _generalDAL.CloseSQLConnection();
                return false;
            }
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public void DeleteRegistration(ArrayList al)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlcmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlcmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlcmd.CommandType = CommandType.Text;
        sqlcmd.Transaction = sqlTrans;

        string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');

        try
        {
            foreach (string id in al)
            {
                sqlcmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..Registration(RegistrationId,FirstName,MobileNo,ExtraMobileNo,StandardId,Schoolname,City,EmailId,IsDeActive,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId, ExamNo)" +
            " Select RegistrationId,FirstName,MobileNo,ExtraMobileNo,StandardId,Schoolname,City,EmailId,IsDeActive,getdate(),LastUpdatedOn,'" + MySession.UserUnique + "',LastUpdatedByUserId, ExamNo" +
            " FROM Registration WHERE RegistrationId='" + id + "'";
                sqlcmd.ExecuteNonQuery();

                sqlcmd.CommandText = "DELETE FROM Registration WHERE RegistrationId='" + id + "'";
                sqlcmd.ExecuteNonQuery();
            }
            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }
}