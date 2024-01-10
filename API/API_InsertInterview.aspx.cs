using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.Collections;
using System.Configuration;
using Newtonsoft.Json;
using System.Data.SqlClient;


public partial class API_API_InsertInterview : System.Web.UI.Page
{
    private GeneralDAL _generalDAL = new GeneralDAL();
    API_BLL _aPI_BLL = new API_BLL();

    #region Declare Variable

    public string RegistrationId;
    public HttpPostedFile ProfilePhoto;
    public string FullName;
    public string PresentAddress;
    public string PresentPost;
    public string PresentVillage;
    public string PresentDistrict;
    public string PresentPinCode;
    public string PresentMobileNo;
    public string PermanentAddress;
    public string PermanentPost;
    public string PermanentVillage;
    public string PermanentDistrict;
    public string PermanentPinCode;
    public string PermanentMobileNo;
    public DateTime? DOB;
    public string BloodGroup;
    public string AadharCardNo;
    public string PanCardNo;
    public string ElectionCardNo;
    public string Category;
    public string Email;
    public string FatherName;
    public string FatherOccupation;
    public string FatherEducation;
    public string FatherMobileNo;
    public string MotherName;
    public string MotherOccupation;
    public string MotherEducation;
    public string MotherMobileNo;
    public string WifeName;
    public string WifeOccupation;
    public string WifeEducation;
    public string WifeMobileNo;
    public string BrotherName;
    public string BrotherOccupation;
    public string BrotherEducation;
    public string BrotherMobileNo;
    public string NomineeName;
    public DateTime? NomineeDOB;
    public string NomineeRelation;
    public int? NomineeAge;
    public string Standanrd10Subject;
    public int? Standanrd10PassingYear;
    public decimal? Standanrd10Percentage;
    public string Standanrd12Subject;
    public int? Standanrd12PassingYear;
    public decimal? Standanrd12Percentage;
    public string GraduateSubject;
    public int? GraduatePassingYear;
    public decimal? GraduatePercentage;
    public string PostGraduateSubject;
    public int? PostGraduatePassingYear;
    public decimal? PostGraduatePercentage;
    public string OtherSubject;
    public int? OtherPassingYear;
    public decimal? OtherPercentage;
    public string CertificateCourseName;
    public string CertificateCourseYear;
    public string TrainingName;
    public int? TrainingYear;
    public string MedalName;
    public int? MedalYear;
    public string FirstCompanyName;
    public string FirstCompanyDesignation;
    public decimal? FirstCompanyExp;
    public decimal? FirstCompanySalary;
    public string SecondCompanyName;
    public string SecondCompanyDesignation;
    public decimal? SecondCompanyExp;
    public decimal? SecondCompanySalary;
    public string ThirdCompanyName;
    public string ThirdCompanyDesignation;
    public decimal? ThirdCompanyExp;
    public decimal? ThirdCompanySalary;
    public string OtherExpNoExpDetails;
    public string Document1;
    public string Document2;
    public string Document3;
    public string Document4;
    public string Document5;
    public string Document6;
    public HttpPostedFile Document1Path1;
    public HttpPostedFile Document1Path2;
    public HttpPostedFile Document2Path1;
    public HttpPostedFile Document2Path2;
    public HttpPostedFile Document3Path1;
    public HttpPostedFile Document3Path2;
    public HttpPostedFile Document4Path1;
    public HttpPostedFile Document4Path2;
    public HttpPostedFile Document5Path1;
    public HttpPostedFile Document5Path2;
    public HttpPostedFile Document6Path1;
    public HttpPostedFile Document6Path2;
    public string DukeReferenceName;
    public HttpPostedFile DukeReferenceSign;
    public string OtherReferenceName;
    public HttpPostedFile OtherReferenceSign;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                #region Argument values paased in variable

                RegistrationId = (((Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "")) ? (Request.Form["RegistrationId"].ToString()) : null);
                ProfilePhoto = ((Request.Files["ProfilePhoto"] != null && Request.Files["ProfilePhoto"].ToString() != "") ? Request.Files["ProfilePhoto"] : null);
                FullName = (((Request.Form["FullName"] != null && Request.Form["FullName"] != "")) ? (Request.Form["FullName"].ToString()) : null);
                PresentAddress = (((Request.Form["PresentAddress"] != null && Request.Form["PresentAddress"] != "")) ? (Request.Form["PresentAddress"].ToString()) : null);
                PresentPost = (((Request.Form["PresentPost"] != null && Request.Form["PresentPost"] != "")) ? (Request.Form["PresentPost"].ToString()) : null);
                PresentVillage = (((Request.Form["PresentVillage"] != null && Request.Form["PresentVillage"] != "")) ? (Request.Form["PresentVillage"].ToString()) : null);
                PresentDistrict = (((Request.Form["PresentDistrict"] != null && Request.Form["PresentDistrict"] != "")) ? (Request.Form["PresentDistrict"].ToString()) : null);
                PresentPinCode = (((Request.Form["PresentPinCode"] != null && Request.Form["PresentPinCode"] != "")) ? (Request.Form["PresentPinCode"].ToString()) : null);
                PresentMobileNo = (((Request.Form["PresentMobileNo"] != null && Request.Form["PresentMobileNo"] != "")) ? (Request.Form["PresentMobileNo"].ToString()) : null);
                PermanentAddress = (((Request.Form["PermanentAddress"] != null && Request.Form["PermanentAddress"] != "")) ? (Request.Form["PermanentAddress"].ToString()) : null);
                PermanentPost = (((Request.Form["PermanentPost"] != null && Request.Form["PermanentPost"] != "")) ? (Request.Form["PermanentPost"].ToString()) : null);
                PermanentVillage = (((Request.Form["PermanentVillage"] != null && Request.Form["PermanentVillage"] != "")) ? (Request.Form["PermanentVillage"].ToString()) : null);
                PermanentDistrict = (((Request.Form["PermanentDistrict"] != null && Request.Form["PermanentDistrict"] != "")) ? (Request.Form["PermanentDistrict"].ToString()) : null);
                PermanentPinCode = (((Request.Form["PermanentPinCode"] != null && Request.Form["PermanentPinCode"] != "")) ? (Request.Form["PermanentPinCode"].ToString()) : null);
                PermanentMobileNo = (((Request.Form["PermanentMobileNo"] != null && Request.Form["PermanentMobileNo"] != "")) ? (Request.Form["PermanentMobileNo"].ToString()) : null);
                DOB = (((Request.Form["DOB"] != null && Request.Form["DOB"] != "")) ? (Convert.ToDateTime(Request.Form["DOB"].ToString())) : (DateTime?)null);
                BloodGroup = (((Request.Form["BloodGroup"] != null && Request.Form["BloodGroup"] != "")) ? (Request.Form["BloodGroup"].ToString()) : null);
                AadharCardNo = (((Request.Form["AadharCardNo"] != null && Request.Form["AadharCardNo"] != "")) ? (Request.Form["AadharCardNo"].ToString()) : null);
                PanCardNo = (((Request.Form["PanCardNo"] != null && Request.Form["PanCardNo"] != "")) ? (Request.Form["PanCardNo"].ToString()) : null);
                ElectionCardNo = (((Request.Form["ElectionCardNo"] != null && Request.Form["ElectionCardNo"] != "")) ? (Request.Form["ElectionCardNo"].ToString()) : null);
                Category = (((Request.Form["Category"] != null && Request.Form["Category"] != "")) ? (Request.Form["Category"].ToString()) : null);
                Email = (((Request.Form["Email"] != null && Request.Form["Email"] != "")) ? (Request.Form["Email"].ToString()) : null);
                FatherName = (((Request.Form["FatherName"] != null && Request.Form["FatherName"] != "")) ? (Request.Form["FatherName"].ToString()) : null);
                FatherOccupation = (((Request.Form["FatherOccupation"] != null && Request.Form["FatherOccupation"] != "")) ? (Request.Form["FatherOccupation"].ToString()) : null);
                FatherEducation = (((Request.Form["FatherEducation"] != null && Request.Form["FatherEducation"] != "")) ? (Request.Form["FatherEducation"].ToString()) : null);
                FatherMobileNo = (((Request.Form["FatherMobileNo"] != null && Request.Form["FatherMobileNo"] != "")) ? (Request.Form["FatherMobileNo"].ToString()) : null);
                MotherName = (((Request.Form["MotherName"] != null && Request.Form["MotherName"] != "")) ? (Request.Form["MotherName"].ToString()) : null);
                MotherOccupation = (((Request.Form["MotherOccupation"] != null && Request.Form["MotherOccupation"] != "")) ? (Request.Form["MotherOccupation"].ToString()) : null);
                MotherEducation = (((Request.Form["MotherEducation"] != null && Request.Form["MotherEducation"] != "")) ? (Request.Form["MotherEducation"].ToString()) : null);
                MotherMobileNo = (((Request.Form["MotherMobileNo"] != null && Request.Form["MotherMobileNo"] != "")) ? (Request.Form["MotherMobileNo"].ToString()) : null);
                WifeName = (((Request.Form["WifeName"] != null && Request.Form["WifeName"] != "")) ? (Request.Form["WifeName"].ToString()) : null);
                WifeOccupation = (((Request.Form["WifeOccupation"] != null && Request.Form["WifeOccupation"] != "")) ? (Request.Form["WifeOccupation"].ToString()) : null);
                WifeEducation = (((Request.Form["WifeEducation"] != null && Request.Form["WifeEducation"] != "")) ? (Request.Form["WifeEducation"].ToString()) : null);
                WifeMobileNo = (((Request.Form["WifeMobileNo"] != null && Request.Form["WifeMobileNo"] != "")) ? (Request.Form["WifeMobileNo"].ToString()) : null);
                BrotherName = (((Request.Form["BrotherName"] != null && Request.Form["BrotherName"] != "")) ? (Request.Form["BrotherName"].ToString()) : null);
                BrotherOccupation = (((Request.Form["BrotherOccupation"] != null && Request.Form["BrotherOccupation"] != "")) ? (Request.Form["BrotherOccupation"].ToString()) : null);
                BrotherEducation = (((Request.Form["BrotherEducation"] != null && Request.Form["BrotherEducation"] != "")) ? (Request.Form["BrotherEducation"].ToString()) : null);
                BrotherMobileNo = (((Request.Form["BrotherMobileNo"] != null && Request.Form["BrotherMobileNo"] != "")) ? (Request.Form["BrotherMobileNo"].ToString()) : null);
                NomineeName = (((Request.Form["NomineeName"] != null && Request.Form["NomineeName"] != "")) ? (Request.Form["NomineeName"].ToString()) : null);
                NomineeDOB = (((Request.Form["NomineeDOB"] != null && Request.Form["NomineeDOB"] != "")) ? Convert.ToDateTime(Request.Form["NomineeDOB"].ToString()) : (DateTime?)null);
                NomineeRelation = (((Request.Form["NomineeRelation"] != null && Request.Form["NomineeRelation"] != "")) ? (Request.Form["NomineeRelation"].ToString()) : null);
                NomineeAge = (((Request.Form["NomineeAge"] != null && Request.Form["NomineeAge"] != "")) ? Convert.ToInt16(Request.Form["NomineeAge"].ToString()) : (int?)null);
                Standanrd10Subject = (((Request.Form["Standanrd10Subject"] != null && Request.Form["Standanrd10Subject"] != "")) ? (Request.Form["Standanrd10Subject"].ToString()) : null);
                Standanrd10PassingYear = (((Request.Form["Standanrd10PassingYear"] != null && Request.Form["Standanrd10PassingYear"] != "")) ? Convert.ToInt16(Request.Form["Standanrd10PassingYear"].ToString()) : (int?)null);
                Standanrd10Percentage = (((Request.Form["Standanrd10Percentage"] != null && Request.Form["Standanrd10Percentage"] != "")) ? Convert.ToDecimal(Request.Form["Standanrd10Percentage"].ToString()) : (decimal?)null);
                Standanrd12Subject = (((Request.Form["Standanrd12Subject"] != null && Request.Form["Standanrd12Subject"] != "")) ? (Request.Form["Standanrd12Subject"].ToString()) : null);
                Standanrd12PassingYear = (((Request.Form["Standanrd12PassingYear"] != null && Request.Form["Standanrd12PassingYear"] != "")) ? Convert.ToInt16(Request.Form["Standanrd12PassingYear"].ToString()) : (int?)null);
                Standanrd12Percentage = (((Request.Form["Standanrd12Percentage"] != null && Request.Form["Standanrd12Percentage"] != "")) ? Convert.ToDecimal(Request.Form["Standanrd12Percentage"].ToString()) : (decimal?)null);
                GraduateSubject = (((Request.Form["GraduateSubject"] != null && Request.Form["GraduateSubject"] != "")) ? (Request.Form["GraduateSubject"].ToString()) : null);
                GraduatePassingYear = (((Request.Form["GraduatePassingYear"] != null && Request.Form["GraduatePassingYear"] != "")) ? Convert.ToInt16(Request.Form["GraduatePassingYear"].ToString()) : (int?)null);
                GraduatePercentage = (((Request.Form["GraduatePercentage"] != null && Request.Form["GraduatePercentage"] != "")) ? Convert.ToDecimal(Request.Form["GraduatePercentage"].ToString()) : (decimal?)null);
                PostGraduateSubject = (((Request.Form["PostGraduateSubject"] != null && Request.Form["PostGraduateSubject"] != "")) ? (Request.Form["PostGraduateSubject"].ToString()) : null);
                PostGraduatePassingYear = (((Request.Form["PostGraduatePassingYear"] != null && Request.Form["PostGraduatePassingYear"] != "")) ? Convert.ToInt16(Request.Form["PostGraduatePassingYear"].ToString()) : (int?)null);
                PostGraduatePercentage = (((Request.Form["PostGraduatePercentage"] != null && Request.Form["PostGraduatePercentage"] != "")) ? Convert.ToDecimal(Request.Form["PostGraduatePercentage"].ToString()) : (decimal?)null);
                OtherSubject = (((Request.Form["OtherSubject"] != null && Request.Form["OtherSubject"] != "")) ? (Request.Form["OtherSubject"].ToString()) : null);
                OtherPassingYear = (((Request.Form["OtherPassingYear"] != null && Request.Form["OtherPassingYear"] != "")) ? Convert.ToInt16(Request.Form["OtherPassingYear"].ToString()) : (int?)null);
                OtherPercentage = (((Request.Form["OtherPercentage"] != null && Request.Form["OtherPercentage"] != "")) ? Convert.ToDecimal(Request.Form["OtherPercentage"].ToString()) : (decimal?)null);
                CertificateCourseName = (((Request.Form["CertificateCourseName"] != null && Request.Form["CertificateCourseName"] != "")) ? (Request.Form["CertificateCourseName"].ToString()) : null);
                CertificateCourseYear = (((Request.Form["CertificateCourseYear"] != null && Request.Form["CertificateCourseYear"] != "")) ? (Request.Form["CertificateCourseYear"].ToString()) : null);
                TrainingName = (((Request.Form["TrainingName"] != null && Request.Form["TrainingName"] != "")) ? (Request.Form["TrainingName"].ToString()) : null);
                TrainingYear = (((Request.Form["TrainingYear"] != null && Request.Form["TrainingYear"] != "")) ? Convert.ToInt16(Request.Form["TrainingYear"].ToString()) : (int?)null);
                MedalName = (((Request.Form["MedalName"] != null && Request.Form["MedalName"] != "")) ? (Request.Form["MedalName"].ToString()) : null);
                MedalYear = (((Request.Form["MedalYear"] != null && Request.Form["MedalYear"] != "")) ? Convert.ToInt16(Request.Form["MedalYear"].ToString()) : (int?)null);
                FirstCompanyName = (((Request.Form["FirstCompanyName"] != null && Request.Form["FirstCompanyName"] != "")) ? (Request.Form["FirstCompanyName"].ToString()) : null);
                FirstCompanyDesignation = (((Request.Form["FirstCompanyDesignation"] != null && Request.Form["FirstCompanyDesignation"] != "")) ? (Request.Form["FirstCompanyDesignation"].ToString()) : null);
                FirstCompanyExp = (((Request.Form["FirstCompanyExp"] != null && Request.Form["FirstCompanyExp"] != "")) ? Convert.ToDecimal(Request.Form["FirstCompanyExp"].ToString()) : (decimal?)null);
                FirstCompanySalary = (((Request.Form["FirstCompanySalary"] != null && Request.Form["FirstCompanySalary"] != "")) ? Convert.ToDecimal(Request.Form["FirstCompanySalary"].ToString()) : (decimal?)null);
                SecondCompanyName = (((Request.Form["SecondCompanyName"] != null && Request.Form["SecondCompanyName"] != "")) ? (Request.Form["SecondCompanyName"].ToString()) : null);
                SecondCompanyDesignation = (((Request.Form["SecondCompanyDesignation"] != null && Request.Form["SecondCompanyDesignation"] != "")) ? (Request.Form["SecondCompanyDesignation"].ToString()) : null);
                SecondCompanyExp = (((Request.Form["SecondCompanyExp"] != null && Request.Form["SecondCompanyExp"] != "")) ? Convert.ToDecimal(Request.Form["SecondCompanyExp"].ToString()) : (decimal?)null);
                SecondCompanySalary = (((Request.Form["SecondCompanySalary"] != null && Request.Form["SecondCompanySalary"] != "")) ? Convert.ToDecimal(Request.Form["SecondCompanySalary"].ToString()) : (decimal?)null);
                ThirdCompanyName = (((Request.Form["ThirdCompanyName"] != null && Request.Form["ThirdCompanyName"] != "")) ? (Request.Form["ThirdCompanyName"].ToString()) : null);
                ThirdCompanyDesignation = (((Request.Form["ThirdCompanyDesignation"] != null && Request.Form["ThirdCompanyDesignation"] != "")) ? (Request.Form["ThirdCompanyDesignation"].ToString()) : null);
                ThirdCompanyExp = (((Request.Form["ThirdCompanyExp"] != null && Request.Form["ThirdCompanyExp"] != "")) ? Convert.ToDecimal(Request.Form["ThirdCompanyExp"].ToString()) : (decimal?)null);
                ThirdCompanySalary = (((Request.Form["ThirdCompanySalary"] != null && Request.Form["ThirdCompanySalary"] != "")) ? Convert.ToDecimal(Request.Form["ThirdCompanySalary"].ToString()) : (decimal?)null);
                OtherExpNoExpDetails = (((Request.Form["OtherExpNoExpDetails"] != null && Request.Form["OtherExpNoExpDetails"] != "")) ? (Request.Form["OtherExpNoExpDetails"].ToString()) : null);
                Document1 = (((Request.Form["Document1"] != null && Request.Form["Document1"] != "")) ? (Request.Form["Document1"].ToString()) : null);
                Document2 = (((Request.Form["Document2"] != null && Request.Form["Document2"] != "")) ? (Request.Form["Document2"].ToString()) : null);
                Document3 = (((Request.Form["Document3"] != null && Request.Form["Document3"] != "")) ? (Request.Form["Document3"].ToString()) : null);
                Document4 = (((Request.Form["Document4"] != null && Request.Form["Document4"] != "")) ? (Request.Form["Document4"].ToString()) : null);
                Document5 = (((Request.Form["Document5"] != null && Request.Form["Document5"] != "")) ? (Request.Form["Document5"].ToString()) : null);
                Document6 = (((Request.Form["Document6"] != null && Request.Form["Document6"] != "")) ? (Request.Form["Document6"].ToString()) : null);
                Document1Path1 = (((Request.Files["Document1Path1"] != null && Request.Files["Document1Path1"].ToString() != "")) ? (Request.Files["Document1Path1"]) : null);
                Document1Path2 = (((Request.Files["Document1Path2"] != null && Request.Files["Document1Path2"].ToString() != "")) ? (Request.Files["Document1Path2"]) : null);
                Document2Path1 = (((Request.Files["Document2Path1"] != null && Request.Files["Document2Path1"].ToString() != "")) ? (Request.Files["Document2Path1"]) : null);
                Document2Path2 = (((Request.Files["Document2Path2"] != null && Request.Files["Document2Path2"].ToString() != "")) ? (Request.Files["Document2Path2"]) : null);
                Document3Path1 = (((Request.Files["Document3Path1"] != null && Request.Files["Document3Path1"].ToString() != "")) ? (Request.Files["Document3Path1"]) : null);
                Document3Path2 = (((Request.Files["Document3Path2"] != null && Request.Files["Document3Path2"].ToString() != "")) ? (Request.Files["Document3Path2"]) : null);
                Document4Path1 = (((Request.Files["Document4Path1"] != null && Request.Files["Document4Path1"].ToString() != "")) ? (Request.Files["Document4Path1"]) : null);
                Document4Path2 = (((Request.Files["Document4Path2"] != null && Request.Files["Document4Path2"].ToString() != "")) ? (Request.Files["Document4Path2"]) : null);
                Document5Path1 = (((Request.Files["Document5Path1"] != null && Request.Files["Document5Path1"].ToString() != "")) ? (Request.Files["Document5Path1"]) : null);
                Document5Path2 = (((Request.Files["Document5Path2"] != null && Request.Files["Document5Path2"].ToString() != "")) ? (Request.Files["Document5Path2"]) : null);
                Document6Path1 = (((Request.Files["Document6Path1"] != null && Request.Files["Document6Path1"].ToString() != "")) ? (Request.Files["Document6Path1"]) : null);
                Document6Path2 = (((Request.Files["Document6Path2"] != null && Request.Files["Document6Path2"].ToString() != "")) ? (Request.Files["Document6Path2"]) : null);
                DukeReferenceName = (((Request.Form["DukeReferenceName"] != null && Request.Form["DukeReferenceName"] != "")) ? (Request.Form["DukeReferenceName"].ToString()) : null);
                DukeReferenceSign = (((Request.Files["DukeReferenceSign"] != null && Request.Files["DukeReferenceSign"].ToString() != "")) ? (Request.Files["DukeReferenceSign"]) : null);
                OtherReferenceName = (((Request.Form["OtherReferenceName"] != null && Request.Form["OtherReferenceName"] != "")) ? (Request.Form["OtherReferenceName"].ToString()) : null);
                OtherReferenceSign = (((Request.Files["OtherReferenceSign"] != null && Request.Files["OtherReferenceSign"].ToString() != "")) ? (Request.Files["OtherReferenceSign"]) : null);


                #endregion

                Response.ContentType = "application/json";
                Response.Write(InsertRegistrations());
            }
            catch (Exception ex)
            {
                string sw = "";
                StringBuilder s = new StringBuilder();
                s.Append(ex.Message);
                sw = GetReturnValue("209", "Data Get Issue", s);
                Response.ContentType = "application/json";
                Response.Write(sw.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]"));
            }
        }
    }

    public string DataTableToJSONWithJSONNet(DataTable table)
    {
        string JSONString = string.Empty;
        JSONString = JsonConvert.SerializeObject(table);
        return JSONString;
    }

    public class ReturnValue
    {
        public string status { get; set; }
        public string message { get; set; }
        public string result { get; set; }
    }

    public string DataTableToJsonObj(DataTable dt)
    {
        DataSet ds = new DataSet();
        ds.Merge(dt);
        StringBuilder JsonString = new StringBuilder();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            JsonString.Append("[");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                JsonString.Append("{");
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    if (j < ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString().Replace("\"", "''") + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "''") + "\",");
                    }
                    else if (j == ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString().Replace("\"", "''") + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "''") + "\"");
                    }
                }
                if (i == ds.Tables[0].Rows.Count - 1)
                {
                    JsonString.Append("}");
                }
                else
                {
                    JsonString.Append("},");
                }
            }
            JsonString.Append("]");
            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }

    public string GetReturnValue(string Status, string Message, StringBuilder PassStringDataTable)
    {
        var r = new ReturnValue
        {
            status = Status,
            message = Message,
            result = PassStringDataTable.ToString()
        };
        return new JavaScriptSerializer().Serialize(r);
    }

    private string InsertRegistrations()
    {
        SqlCommand sqlCmd = new SqlCommand();
        StringBuilder st = new StringBuilder();
        DataTable da = new DataTable();
        DataTable da2 = new DataTable();
        DataTable da3 = new DataTable();

        string InterviewFormId = "";
        string ReturnVal = "";
        _generalDAL.OpenSQLConnection();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;


        try
        {
            string Photopath = "";
            string Document1PathOne = "";
            string Document1PathTwo = "";
            string Document2PathOne = "";
            string Document2PathTwo = "";
            string Document3PathOne = "";
            string Document3PathTwo = "";
            string Document4PathOne = "";
            string Document4PathTwo = "";
            string Document5PathOne = "";
            string Document5PathTwo = "";
            string Document6PathOne = "";
            string Document6PathTwo = "";
            string DukeReffernceSignOne = "";
            string DukeReffernceSignOther = "";
            //da = _aPI_BLL.returnDataTable(" select * from Registrations where Mobileno = '" + MobileNo + "' ");

            if (ProfilePhoto != null && ProfilePhoto.FileName != "")
            {
                string UploadFileName1 = ProfilePhoto.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                Photopath = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                ProfilePhoto.SaveAs(Photopath);

            }
            if (Document1Path1 != null && Document1Path1.FileName != "")
            {
                string UploadFileName1 = Document1Path1.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                Document1PathOne = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                Document1Path1.SaveAs(Document1PathOne);

            }
            if (Document1Path2 != null && Document1Path2.FileName != "")
            {
                string UploadFileName1 = Document1Path2.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                Document1PathTwo = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                Document1Path2.SaveAs(Document1PathTwo);
            }
            if (Document2Path1 != null && Document2Path1.FileName != "")
            {
                string UploadFileName1 = Document2Path1.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                Document2PathOne = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                Document2Path1.SaveAs(Document2PathOne);
            }
            if (Document2Path2 != null && Document2Path2.FileName != "")
            {
                string UploadFileName1 = Document2Path2.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                Document2PathTwo = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                Document2Path2.SaveAs(Document2PathTwo);
            }
            if (Document3Path1 != null && Document3Path1.FileName != "")
            {
                string UploadFileName1 = Document3Path1.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                Document3PathOne = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                Document3Path1.SaveAs(Document3PathOne);
            }
            if (Document3Path2 != null && Document3Path2.FileName != "")
            {
                string UploadFileName1 = Document3Path2.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                Document3PathTwo = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                Document3Path2.SaveAs(Document3PathTwo);
            }
            if (Document4Path1 != null && Document4Path1.FileName != "")
            {
                string UploadFileName1 = Document4Path1.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                Document4PathOne = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                Document4Path1.SaveAs(Document4PathOne);
            }
            if (Document4Path2 != null && Document4Path2.FileName != "")
            {
                string UploadFileName1 = Document4Path2.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                Document4PathTwo = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                Document4Path2.SaveAs(Document4PathTwo);
            }
            if (Document5Path1 != null && Document5Path1.FileName != "")
            {
                string UploadFileName1 = Document5Path1.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                Document5PathOne = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                Document5Path1.SaveAs(Document5PathOne);
            }
            if (Document5Path2 != null && Document5Path2.FileName != "")
            {
                string UploadFileName1 = Document5Path2.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                Document5PathTwo = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                Document5Path2.SaveAs(Document5PathTwo);
            }
            if (Document6Path1 != null && Document6Path1.FileName != "")
            {
                string UploadFileName1 = Document6Path1.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                Document6PathOne = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                Document6Path1.SaveAs(Document6PathOne);
            }
            if (Document6Path2 != null && Document6Path2.FileName != "")
            {
                string UploadFileName1 = Document6Path2.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                Document6PathTwo = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                Document6Path2.SaveAs(Document6PathTwo);
            }
            if (DukeReferenceSign != null && DukeReferenceSign.FileName != "")
            {
                string UploadFileName1 = DukeReferenceSign.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                DukeReffernceSignOne = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                DukeReferenceSign.SaveAs(DukeReffernceSignOne);

            }
            if (OtherReferenceSign != null && OtherReferenceSign.FileName != "")
            {
                string UploadFileName1 = OtherReferenceSign.FileName;
                int lastSlash1 = UploadFileName1.LastIndexOf("\\");
                string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
                DukeReffernceSignOther = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
                OtherReferenceSign.SaveAs(DukeReffernceSignOther);

            }

            da2 = _aPI_BLL.returnDataTable("select * from InterviewForms where RegistrationId = '" + RegistrationId + "' ");

            if (da2.Rows.Count > 0)
            {
                sqlCmd.CommandText = " UPDATE InterviewForms SET" +
                                  " FullName  = " + ((FullName == null) ? "NULL" : "'" + FullName.Replace("'", "''") + "'") + "," +
                                  " PresentAddress = " + ((PresentAddress == null) ? "NULL" : "'" + PresentAddress.Replace("'", "''") + "'") + "," +
                                  " PresentPost  = " + ((PresentPost == null) ? "NULL" : "'" + PresentPost.Replace("'", "''") + "'") + "," +
                                  " PresentVillage  = " + ((PresentVillage == null) ? "NULL" : "'" + PresentVillage.Replace("'", "''") + "'") + "," +
                                  " PresentDistrict  = " + ((PresentDistrict == null) ? "NULL" : "'" + PresentDistrict.Replace("'", "''") + "'") + "," +
                                  " PresentPinCode  = " + ((PresentPinCode == null) ? "NULL" : "'" + PresentPinCode.Replace("'", "''") + "'") + "," +
                                  " PresentMobileNo  = " + ((PresentMobileNo == null) ? "NULL" : "'" + PresentMobileNo.Replace("'", "''") + "'") + "," +
                                  " PermanentAddress  = " + ((PermanentAddress == null) ? "NULL" : "'" + PermanentAddress.Replace("'", "''") + "'") + "," +
                                  " PermanentPost  = " + ((PermanentPost == null) ? "NULL" : "'" + PermanentPost.Replace("'", "''") + "'") + "," +
                                  " PermanentVillage  = " + ((PermanentVillage == null) ? "NULL" : "'" + PermanentVillage.Replace("'", "''") + "'") + "," +
                                  " PermanentDistrict  = " + ((PermanentDistrict == null) ? "NULL" : "'" + PermanentDistrict.Replace("'", "''") + "'") + "," +
                                  " PermanentPinCode  = " + ((PermanentPinCode == null) ? "NULL" : "'" + PermanentPinCode.Replace("'", "''") + "'") + "," +
                                  " PermanentMobileNo  = " + ((PermanentMobileNo == null) ? "NULL" : "'" + PermanentMobileNo.Replace("'", "''") + "'") + "," +
                                  " DOB  = " + ((DOB == null) ? "NULL" : "'" + ((DateTime)DOB).ToString("dd-MMM-yyyy") + "'") + "," +
                                  " BloodGroup  = " + ((BloodGroup == null) ? "NULL" : "'" + BloodGroup.Replace("'", "''") + "'") + "," +
                                  " AadharCardNo  = " + ((AadharCardNo == null) ? "NULL" : "'" + AadharCardNo.Replace("'", "''") + "'") + "," +
                                  " PanCardNo  = " + ((PanCardNo == null) ? "NULL" : "'" + PanCardNo.Replace("'", "''") + "'") + "," +
                                  " ElectionCardNo  = " + ((ElectionCardNo == null) ? "NULL" : "'" + ElectionCardNo.Replace("'", "''") + "'") + "," +
                                  " Category  = " + ((Category == null) ? "NULL" : "'" + Category.Replace("'", "''") + "'") + "," +
                                  " Email  = " + ((Email == null) ? "NULL" : "'" + Email.Replace("'", "''") + "'") + "," +
                                  " FatherName  = " + ((FatherName == null) ? "NULL" : "'" + FatherName.Replace("'", "''") + "'") + "," +
                                  " FatherOccupation  = " + ((FatherOccupation == null) ? "NULL" : "'" + FatherOccupation.Replace("'", "''") + "'") + "," +
                                  " FatherEducation  = " + ((FatherEducation == null) ? "NULL" : "'" + FatherEducation.Replace("'", "''") + "'") + "," +
                                  " FatherMobileNo  = " + ((FatherMobileNo == null) ? "NULL" : "'" + FatherMobileNo.Replace("'", "''") + "'") + "," +
                                  " MotherName  = " + ((MotherName == null) ? "NULL" : "'" + MotherName.Replace("'", "''") + "'") + "," +
                                  " MotherOccupation  = " + ((MotherOccupation == null) ? "NULL" : "'" + MotherOccupation.Replace("'", "''") + "'") + "," +
                                  " MotherEducation  = " + ((MotherEducation == null) ? "NULL" : "'" + MotherEducation.Replace("'", "''") + "'") + "," +
                                  " MotherMobileNo  = " + ((MotherMobileNo == null) ? "NULL" : "'" + MotherMobileNo.Replace("'", "''") + "'") + "," +
                                  " WifeName  = " + ((WifeName == null) ? "NULL" : "'" + WifeName.Replace("'", "''") + "'") + "," +
                                  " WifeOccupation  = " + ((WifeOccupation == null) ? "NULL" : "'" + WifeOccupation.Replace("'", "''") + "'") + "," +
                                  " WifeEducation  = " + ((WifeEducation == null) ? "NULL" : "'" + WifeEducation.Replace("'", "''") + "'") + "," +
                                  " WifeMobileNo  = " + ((WifeMobileNo == null) ? "NULL" : "'" + WifeMobileNo.Replace("'", "''") + "'") + "," +
                                  " BrotherName  = " + ((BrotherName == null) ? "NULL" : "'" + BrotherName.Replace("'", "''") + "'") + "," +
                                  " BrotherOccupation  = " + ((BrotherOccupation == null) ? "NULL" : "'" + BrotherOccupation.Replace("'", "''") + "'") + "," +
                                  " BrotherEducation  = " + ((BrotherEducation == null) ? "NULL" : "'" + BrotherEducation.Replace("'", "''") + "'") + "," +
                                  " BrotherMobileNo  = " + ((BrotherMobileNo == null) ? "NULL" : "'" + BrotherMobileNo.Replace("'", "''") + "'") + "," +
                                  " NomineeName  = " + ((NomineeName == null) ? "NULL" : "'" + NomineeName.Replace("'", "''") + "'") + "," +
                                  " NomineeDOB  = " + ((NomineeDOB == null) ? "NULL" : "'" + ((DateTime)NomineeDOB).ToString("dd-MMM-yyyy") + "'") + "," +
                                  " NomineeRelation  = " + ((NomineeRelation == null) ? "NULL" : "'" + NomineeRelation.Replace("'", "''") + "'") + "," +
                                  " NomineeAge  = " + ((NomineeAge == null) ? "NULL" : "'" + Convert.ToInt16(NomineeAge) + "'") + "," +
                                  " Standanrd10Subject  = " + ((Standanrd10Subject == null) ? "NULL" : "'" + Standanrd10Subject.Replace("'", "''") + "'") + "," +
                                  " Standanrd10PassingYear  = " + ((Standanrd10PassingYear == null) ? "NULL" : "'" + Convert.ToInt16(Standanrd10PassingYear) + "'") + "," +
                                  " Standanrd10Percentage  = " + ((Standanrd10Percentage == null) ? "NULL" : "'" + Convert.ToDecimal(Standanrd10Percentage) + "'") + "," +
                                  " Standanrd12Subject  = " + ((Standanrd12Subject == null) ? "NULL" : "'" + Standanrd12Subject.Replace("'", "''") + "'") + "," +
                                  " Standanrd12PassingYear  = " + ((Standanrd12PassingYear == null) ? "NULL" : "'" + Convert.ToInt16(Standanrd12PassingYear) + "'") + "," +
                                  " Standanrd12Percentage  = " + ((Standanrd12Percentage == null) ? "NULL" : "'" + Convert.ToDecimal(Standanrd12Percentage) + "'") + "," +
                                  " GraduateSubject  = " + ((GraduateSubject == null) ? "NULL" : "'" + GraduateSubject.Replace("'", "''") + "'") + "," +
                                  " GraduatePassingYear  = " + ((GraduatePassingYear == null) ? "NULL" : "'" + Convert.ToInt16(GraduatePassingYear) + "'") + "," +
                                  " GraduatePercentage  = " + ((GraduatePercentage == null) ? "NULL" : "'" + Convert.ToDecimal(GraduatePercentage) + "'") + "," +
                                  " PostGraduateSubject  = " + ((PostGraduateSubject == null) ? "NULL" : "'" + PostGraduateSubject.Replace("'", "''") + "'") + "," +
                                  " PostGraduatePassingYear  = " + ((PostGraduatePassingYear == null) ? "NULL" : "'" + Convert.ToInt16(PostGraduatePassingYear) + "'") + "," +
                                  " PostGraduatePercentage  = " + ((PostGraduatePercentage == null) ? "NULL" : "'" + Convert.ToDecimal(PostGraduatePercentage) + "'") + "," +
                                  " OtherSubject  = " + ((OtherSubject == null) ? "NULL" : "'" + OtherSubject.Replace("'", "''") + "'") + "," +
                                  " OtherPassingYear  = " + ((OtherPassingYear == null) ? "NULL" : "'" + Convert.ToInt16(OtherPassingYear) + "'") + "," +
                                  " OtherPercentage  = " + ((OtherPercentage == null) ? "NULL" : "'" + Convert.ToDecimal(OtherPercentage) + "'") + "," +
                                  " CertificateCourseName  = " + ((CertificateCourseName == null) ? "NULL" : "'" + CertificateCourseName.Replace("'", "''") + "'") + "," +
                                  " CertificateCourseYear  = " + ((CertificateCourseYear == null) ? "NULL" : "'" + Convert.ToInt16(CertificateCourseYear) + "'") + "," +
                                  " TrainingName  = " + ((TrainingName == null) ? "NULL" : "'" + TrainingName.Replace("'", "''") + "'") + "," +
                                  " TrainingYear  = " + ((TrainingYear == null) ? "NULL" : "'" + Convert.ToInt16(TrainingYear) + "'") + "," +
                                  " MedalName  = " + ((MedalName == null) ? "NULL" : "'" + MedalName.Replace("'", "''") + "'") + "," +
                                  " MedalYear  = " + ((MedalYear == null) ? "NULL" : "'" + Convert.ToInt16(MedalYear) + "'") + "," +
                                  " FirstCompanyName  = " + ((FirstCompanyName == null) ? "NULL" : "'" + FirstCompanyName.Replace("'", "''") + "'") + "," +
                                  " FirstCompanyDesignation  = " + ((FirstCompanyDesignation == null) ? "NULL" : "'" + FirstCompanyDesignation.Replace("'", "''") + "'") + "," +
                                  " FirstCompanyExp  = " + ((FirstCompanyExp == null) ? "NULL" : "'" + Convert.ToDecimal(FirstCompanyExp) + "'") + "," +
                                  " FirstCompanySalary  = " + ((FirstCompanySalary == null) ? "NULL" : "'" + Convert.ToDecimal(FirstCompanySalary) + "'") + "," +
                                  " SecondCompanyName  = " + ((SecondCompanyName == null) ? "NULL" : "'" + SecondCompanyName.Replace("'", "''") + "'") + "," +
                                  " SecondCompanyDesignation  = " + ((SecondCompanyDesignation == null) ? "NULL" : "'" + SecondCompanyDesignation.Replace("'", "''") + "'") + "," +
                                  " SecondCompanyExp  = " + ((SecondCompanyExp == null) ? "NULL" : "'" + Convert.ToDecimal(SecondCompanyExp) + "'") + "," +
                                  " SecondCompanySalary  = " + ((SecondCompanySalary == null) ? "NULL" : "'" + Convert.ToDecimal(SecondCompanySalary) + "'") + "," +
                                  " ThirdCompanyName  = " + ((ThirdCompanyName == null) ? "NULL" : "'" + ThirdCompanyName.Replace("'", "''") + "'") + "," +
                                  " ThirdCompanyDesignation  = " + ((ThirdCompanyDesignation == null) ? "NULL" : "'" + ThirdCompanyDesignation.Replace("'", "''") + "'") + "," +
                                  " ThirdCompanyExp  = " + ((ThirdCompanyExp == null) ? "NULL" : "'" + Convert.ToDecimal(ThirdCompanyExp) + "'") + "," +
                                  " ThirdCompanySalary  = " + ((ThirdCompanySalary == null) ? "NULL" : "'" + Convert.ToDecimal(ThirdCompanySalary) + "'") + "," +
                                  " OtherExpNoExpDetails  = " + ((OtherExpNoExpDetails == null) ? "NULL" : "'" + OtherExpNoExpDetails.Replace("'", "''") + "'") + "," +
                                  " LastUpdatedOn = getdate() " +
                                  ", LastUpdatedByUserId = " + ((MySession.UserUnique == null) ? "NULL" : "'" + MySession.UserUnique + "'") +
                                  ",Document1Path1 = " + ((Document1PathOne == null) ? "NULL" : "'" + Document1PathOne.Replace("'", "''") + "'") + "," +
                                  " Document1Path2 = " + ((Document1PathTwo == null) ? "NULL" : "'" + Document1PathTwo.Replace("'", "''") + "'") + "," +
                                  " Document2Path1 = " + ((Document2PathOne == null) ? "NULL" : "'" + Document2PathOne.Replace("'", "''") + "'") + "," +
                                  " Document2Path2 = " + ((Document2PathTwo == null) ? "NULL" : "'" + Document2PathTwo.Replace("'", "''") + "'") + "," +
                                  " Document3Path1 = " + ((Document3PathOne == null) ? "NULL" : "'" + Document3PathOne.Replace("'", "''") + "'") + "," +
                                  " Document3Path2 = " + ((Document3PathTwo == null) ? "NULL" : "'" + Document3PathTwo.Replace("'", "''") + "'") + "," +
                                  " Document4Path1 = " + ((Document4PathOne == null) ? "NULL" : "'" + Document4PathOne.Replace("'", "''") + "'") + "," +
                                  " Document4Path2 = " + ((Document4PathTwo == null) ? "NULL" : "'" + Document4PathTwo.Replace("'", "''") + "'") + "," +
                                  " Document5Path1 = " + ((Document5PathOne == null) ? "NULL" : "'" + Document5PathOne.Replace("'", "''") + "'") + "," +
                                  " Document5Path2 = " + ((Document5PathTwo == null) ? "NULL" : "'" + Document5PathTwo.Replace("'", "''") + "'") + "," +
                                  " Document6Path1 = " + ((Document6PathOne == null) ? "NULL" : "'" + Document6PathOne.Replace("'", "''") + "'") + "," +
                                  " Document6Path2 = " + ((Document6PathTwo == null) ? "NULL" : "'" + Document6PathTwo.Replace("'", "''") + "'") +
                                  " WHERE RegistrationId = '" + RegistrationId + "'";

                sqlCmd.ExecuteNonQuery();

            }
            else
            {
                sqlCmd.CommandText = ("DECLARE @InterviewFormId  uniqueidentifier;" +
                                         " SET @InterviewFormId = NEWID() " +
                                         " INSERT INTO InterviewForms (InterviewFormId,RegistrationId,ProfilePhoto,FullName,PresentAddress,PresentPost,PresentVillage,PresentDistrict,PresentPinCode,PresentMobileNo,PermanentAddress,PermanentPost,PermanentVillage,PermanentDistrict,PermanentPinCode,PermanentMobileNo,DOB,BloodGroup,AadharCardNo,PanCardNo,ElectionCardNo,Category,Email,FatherName,FatherOccupation,FatherEducation,FatherMobileNo,MotherName,MotherOccupation,MotherEducation,MotherMobileNo,WifeName,WifeOccupation,WifeEducation,WifeMobileNo,BrotherName,BrotherOccupation,BrotherEducation,BrotherMobileNo,NomineeName,NomineeDOB,NomineeRelation,NomineeAge,Standanrd10Subject,Standanrd10PassingYear,Standanrd10Percentage,Standanrd12Subject,Standanrd12PassingYear,Standanrd12Percentage,GraduateSubject,GraduatePassingYear,GraduatePercentage,PostGraduateSubject,PostGraduatePassingYear,PostGraduatePercentage,OtherSubject,OtherPassingYear,OtherPercentage,CertificateCourseName,CertificateCourseYear,TrainingName,TrainingYear,MedalName,MedalYear,FirstCompanyName,FirstCompanyDesignation,FirstCompanyExp,FirstCompanySalary,SecondCompanyName,SecondCompanyDesignation,SecondCompanyExp,SecondCompanySalary,ThirdCompanyName,ThirdCompanyDesignation,ThirdCompanyExp,ThirdCompanySalary,OtherExpNoExpDetails,Document1,Document2,Document3,Document4,Document5,Document6,Document1Path1,Document1Path2,Document2Path1,Document2Path2,Document3Path1,Document3Path2,Document4Path1,Document4Path2,Document5Path1,Document5Path2,Document6Path1,Document6Path2,DukeReferenceName,DukeReferenceSign,OtherReferenceName,OtherReferenceSign,InsertedOn,LastUpdatedOn) " +
                                         " VALUES (@InterviewFormId " +
                                         "," + ((RegistrationId == null || RegistrationId.ToString() == "") ? "NULL" : "'" + RegistrationId.ToString().Replace("'", "''") + "'") +
                                         "," + ((Photopath == null || Photopath.ToString() == "") ? "NULL" : "'" + Photopath.ToString().Replace("'", "''") + "'") +
                                         "," + ((FullName == null || FullName.ToString() == "") ? "NULL" : "'" + FullName.ToString().Replace("'", "''") + "'") +
                                         "," + ((PresentAddress == null || PresentAddress.ToString() == "") ? "NULL" : "'" + PresentAddress.ToString().Replace("'", "''") + "'") +
                                         "," + ((PresentPost == null || PresentPost.ToString() == "") ? "NULL" : "'" + PresentPost.ToString().Replace("'", "''") + "'") +
                                         "," + ((PresentVillage == null || PresentVillage.ToString() == "") ? "NULL" : "'" + PresentVillage.ToString().Replace("'", "''") + "'") +
                                         "," + ((PresentDistrict == null || PresentDistrict.ToString() == "") ? "NULL" : "'" + PresentDistrict.ToString().Replace("'", "''") + "'") +
                                         "," + ((PresentPinCode == null || PresentPinCode.ToString() == "") ? "NULL" : "'" + PresentPinCode.ToString().Replace("'", "''") + "'") +
                                         "," + ((PresentMobileNo == null || PresentMobileNo.ToString() == "") ? "NULL" : "'" + PresentMobileNo.ToString().Replace("'", "''") + "'") +
                                         "," + ((PermanentAddress == null || PermanentAddress.ToString() == "") ? "NULL" : "'" + PermanentAddress.ToString().Replace("'", "''") + "'") +
                                         "," + ((PermanentPost == null || PermanentPost.ToString() == "") ? "NULL" : "'" + PermanentPost.ToString().Replace("'", "''") + "'") +
                                         "," + ((PermanentVillage == null || PermanentVillage.ToString() == "") ? "NULL" : "'" + PermanentVillage.ToString().Replace("'", "''") + "'") +
                                         "," + ((PermanentDistrict == null || PermanentDistrict.ToString() == "") ? "NULL" : "'" + PermanentDistrict.ToString().Replace("'", "''") + "'") +
                                         "," + ((PermanentPinCode == null || PermanentPinCode.ToString() == "") ? "NULL" : "'" + PermanentPinCode.ToString().Replace("'", "''") + "'") +
                                         "," + ((PermanentMobileNo == null || PermanentMobileNo.ToString() == "") ? "NULL" : "'" + PermanentMobileNo.ToString().Replace("'", "''") + "'") +
                                         "," + ((DOB == null || DOB.ToString() == "") ? "NULL" : "'" + Convert.ToDateTime(DOB).ToString("dd-MMM-yyyy") + "'") +
                                         "," + ((BloodGroup == null || BloodGroup.ToString() == "") ? "NULL" : "'" + BloodGroup.ToString().Replace("'", "''") + "'") +
                                         "," + ((AadharCardNo == null || AadharCardNo.ToString() == "") ? "NULL" : "'" + AadharCardNo.ToString().Replace("'", "''") + "'") +
                                         "," + ((PanCardNo == null || PanCardNo.ToString() == "") ? "NULL" : "'" + PanCardNo.ToString().Replace("'", "''") + "'") +
                                         "," + ((ElectionCardNo == null || ElectionCardNo.ToString() == "") ? "NULL" : "'" + ElectionCardNo.ToString().Replace("'", "''") + "'") +
                                         "," + ((Category == null || Category.ToString() == "") ? "NULL" : "'" + Category.ToString().Replace("'", "''") + "'") +
                                         "," + ((Email == null || Email.ToString() == "") ? "NULL" : "'" + Email.ToString().Replace("'", "''") + "'") +
                                         "," + ((FatherName == null || FatherName.ToString() == "") ? "NULL" : "'" + FatherName.ToString().Replace("'", "''") + "'") +
                                         "," + ((FatherOccupation == null || FatherOccupation.ToString() == "") ? "NULL" : "'" + FatherOccupation.ToString().Replace("'", "''") + "'") +
                                         "," + ((FatherEducation == null || FatherEducation.ToString() == "") ? "NULL" : "'" + FatherEducation.ToString().Replace("'", "''") + "'") +
                                         "," + ((FatherMobileNo == null || FatherMobileNo.ToString() == "") ? "NULL" : "'" + FatherMobileNo.ToString().Replace("'", "''") + "'") +
                                         "," + ((MotherName == null || MotherName.ToString() == "") ? "NULL" : "'" + MotherName.ToString().Replace("'", "''") + "'") +
                                         "," + ((MotherOccupation == null || MotherOccupation.ToString() == "") ? "NULL" : "'" + MotherOccupation.ToString().Replace("'", "''") + "'") +
                                         "," + ((MotherEducation == null || MotherEducation.ToString() == "") ? "NULL" : "'" + MotherEducation.ToString().Replace("'", "''") + "'") +
                                         "," + ((MotherMobileNo == null || MotherMobileNo.ToString() == "") ? "NULL" : "'" + MotherMobileNo.ToString().Replace("'", "''") + "'") +
                                         "," + ((WifeName == null || WifeName.ToString() == "") ? "NULL" : "'" + WifeName.ToString().Replace("'", "''") + "'") +
                                         "," + ((WifeOccupation == null || WifeOccupation.ToString() == "") ? "NULL" : "'" + WifeOccupation.ToString().Replace("'", "''") + "'") +
                                         "," + ((WifeEducation == null || WifeEducation.ToString() == "") ? "NULL" : "'" + WifeEducation.ToString().Replace("'", "''") + "'") +
                                         "," + ((WifeMobileNo == null || WifeMobileNo.ToString() == "") ? "NULL" : "'" + WifeMobileNo.ToString().Replace("'", "''") + "'") +
                                         "," + ((BrotherName == null || BrotherName.ToString() == "") ? "NULL" : "'" + BrotherName.ToString().Replace("'", "''") + "'") +
                                         "," + ((BrotherOccupation == null || BrotherOccupation.ToString() == "") ? "NULL" : "'" + BrotherOccupation.ToString().Replace("'", "''") + "'") +
                                         "," + ((BrotherEducation == null || BrotherEducation.ToString() == "") ? "NULL" : "'" + BrotherEducation.ToString().Replace("'", "''") + "'") +
                                         "," + ((BrotherMobileNo == null || BrotherMobileNo.ToString() == "") ? "NULL" : "'" + BrotherMobileNo.ToString().Replace("'", "''") + "'") +
                                         "," + ((NomineeName == null || NomineeName.ToString() == "") ? "NULL" : "'" + NomineeName.ToString().Replace("'", "''") + "'") +
                                         "," + ((NomineeDOB == null || NomineeDOB.ToString() == "") ? "NULL" : "'" + Convert.ToDateTime(NomineeDOB).ToString("dd-MMM-yyyy") + "'") +
                                         "," + ((NomineeRelation == null || NomineeRelation.ToString() == "") ? "NULL" : "'" + NomineeRelation.ToString().Replace("'", "''") + "'") +
                                         "," + ((NomineeAge == null || NomineeAge.ToString() == "") ? "NULL" : "'" + Convert.ToInt16(NomineeAge.ToString()) + "'") +
                                         "," + ((Standanrd10Subject == null || Standanrd10Subject.ToString() == "") ? "NULL" : "'" + Standanrd10Subject.ToString().Replace("'", "''") + "'") +
                                         "," + ((Standanrd10PassingYear == null || Standanrd10PassingYear.ToString() == "") ? "NULL" : "'" + Convert.ToInt16(Standanrd10PassingYear.ToString()) + "'") +
                                         "," + ((Standanrd10Percentage == null || Standanrd10Percentage.ToString() == "") ? "NULL" : "'" + Convert.ToDecimal(Standanrd10Percentage.ToString()) + "'") +
                                         "," + ((Standanrd12Subject == null || Standanrd12Subject.ToString() == "") ? "NULL" : "'" + Standanrd12Subject.ToString().Replace("'", "''") + "'") +
                                         "," + ((Standanrd12PassingYear == null || Standanrd12PassingYear.ToString() == "") ? "NULL" : "'" + Convert.ToInt16(Standanrd12PassingYear.ToString()) + "'") +
                                         "," + ((Standanrd12Percentage == null || Standanrd12Percentage.ToString() == "") ? "NULL" : "'" + Convert.ToDecimal(Standanrd12Percentage.ToString()) + "'") +
                                         "," + ((GraduateSubject == null || GraduateSubject.ToString() == "") ? "NULL" : "'" + GraduateSubject.ToString().Replace("'", "''") + "'") +
                                         "," + ((GraduatePassingYear == null || GraduatePassingYear.ToString() == "") ? "NULL" : "'" + Convert.ToInt16(GraduatePassingYear.ToString()) + "'") +
                                         "," + ((GraduatePercentage == null || GraduatePercentage.ToString() == "") ? "NULL" : "'" + Convert.ToDecimal(GraduatePercentage.ToString()) + "'") +
                                         "," + ((PostGraduateSubject == null || PostGraduateSubject.ToString() == "") ? "NULL" : "'" + PostGraduateSubject.ToString().Replace("'", "''") + "'") +
                                         "," + ((PostGraduatePassingYear == null || PostGraduatePassingYear.ToString() == "") ? "NULL" : "'" + Convert.ToInt16(PostGraduatePassingYear.ToString()) + "'") +
                                         "," + ((PostGraduatePercentage == null || PostGraduatePercentage.ToString() == "") ? "NULL" : "'" + Convert.ToDecimal(PostGraduatePercentage.ToString()) + "'") +
                                         "," + ((OtherSubject == null || OtherSubject.ToString() == "") ? "NULL" : "'" + OtherSubject.ToString().Replace("'", "''") + "'") +
                                         "," + ((OtherPassingYear == null || OtherPassingYear.ToString() == "") ? "NULL" : "'" + Convert.ToInt16(OtherPassingYear.ToString()) + "'") +
                                         "," + ((OtherPercentage == null || OtherPercentage.ToString() == "") ? "NULL" : "'" + Convert.ToDecimal(OtherPercentage.ToString()) + "'") +
                                         "," + ((CertificateCourseName == null || CertificateCourseName.ToString() == "") ? "NULL" : "'" + CertificateCourseName.ToString().Replace("'", "''") + "'") +
                                         "," + ((CertificateCourseYear == null || CertificateCourseYear.ToString() == "") ? "NULL" : "'" + CertificateCourseYear.ToString().Replace("'", "''") + "'") +
                                         "," + ((TrainingName == null || TrainingName.ToString() == "") ? "NULL" : "'" + TrainingName.ToString().Replace("'", "''") + "'") +
                                         "," + ((TrainingYear == null || TrainingYear.ToString() == "") ? "NULL" : "'" + Convert.ToInt16(TrainingYear.ToString()) + "'") +
                                         "," + ((MedalName == null || MedalName.ToString() == "") ? "NULL" : "'" + MedalName.ToString().Replace("'", "''") + "'") +
                                         "," + ((MedalYear == null || MedalYear.ToString() == "") ? "NULL" : "'" + Convert.ToInt16(MedalYear.ToString()) + "'") +
                                         "," + ((FirstCompanyName == null || FirstCompanyName.ToString() == "") ? "NULL" : "'" + FirstCompanyName.ToString().Replace("'", "''") + "'") +
                                         "," + ((FirstCompanyDesignation == null || FirstCompanyDesignation.ToString() == "") ? "NULL" : "'" + FirstCompanyDesignation.ToString().Replace("'", "''") + "'") +
                                         "," + ((FirstCompanyExp == null || FirstCompanyExp.ToString() == "") ? "NULL" : "'" + Convert.ToDecimal(FirstCompanyExp.ToString()) + "'") +
                                         "," + ((FirstCompanySalary == null || FirstCompanySalary.ToString() == "") ? "NULL" : "'" + Convert.ToDecimal(FirstCompanySalary.ToString()) + "'") +
                                         "," + ((SecondCompanyName == null || SecondCompanyName.ToString() == "") ? "NULL" : "'" + SecondCompanyName.ToString().Replace("'", "''") + "'") +
                                         "," + ((SecondCompanyDesignation == null || SecondCompanyDesignation.ToString() == "") ? "NULL" : "'" + SecondCompanyDesignation.ToString().Replace("'", "''") + "'") +
                                         "," + ((SecondCompanyExp == null || SecondCompanyExp.ToString() == "") ? "NULL" : "'" + Convert.ToDecimal(SecondCompanyExp.ToString()) + "'") +
                                         "," + ((SecondCompanySalary == null || SecondCompanySalary.ToString() == "") ? "NULL" : "'" + Convert.ToDecimal(SecondCompanySalary.ToString()) + "'") +
                                         "," + ((ThirdCompanyName == null || ThirdCompanyName.ToString() == "") ? "NULL" : "'" + ThirdCompanyName.ToString().Replace("'", "''") + "'") +
                                         "," + ((ThirdCompanyDesignation == null || ThirdCompanyDesignation.ToString() == "") ? "NULL" : "'" + ThirdCompanyDesignation.ToString().Replace("'", "''") + "'") +
                                         "," + ((ThirdCompanyExp == null || ThirdCompanyExp.ToString() == "") ? "NULL" : "'" + Convert.ToDecimal(ThirdCompanyExp.ToString()) + "'") +
                                         "," + ((ThirdCompanySalary == null || ThirdCompanySalary.ToString() == "") ? "NULL" : "'" + Convert.ToDecimal(ThirdCompanySalary.ToString()) + "'") +
                                         "," + ((OtherExpNoExpDetails == null || OtherExpNoExpDetails.ToString() == "") ? "NULL" : "'" + OtherExpNoExpDetails.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document1 == null || Document1.ToString() == "") ? "NULL" : "'" + Document1.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document2 == null || Document2.ToString() == "") ? "NULL" : "'" + Document2.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document3 == null || Document3.ToString() == "") ? "NULL" : "'" + Document3.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document4 == null || Document4.ToString() == "") ? "NULL" : "'" + Document4.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document5 == null || Document5.ToString() == "") ? "NULL" : "'" + Document5.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document6 == null || Document6.ToString() == "") ? "NULL" : "'" + Document6.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document1PathOne == null || Document1PathOne.ToString() == "") ? "NULL" : "'" + Document1PathOne.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document1PathTwo == null || Document1PathTwo.ToString() == "") ? "NULL" : "'" + Document1PathTwo.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document2PathOne == null || Document2PathOne.ToString() == "") ? "NULL" : "'" + Document2PathOne.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document2PathTwo == null || Document2PathTwo.ToString() == "") ? "NULL" : "'" + Document2PathTwo.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document3PathOne == null || Document3PathOne.ToString() == "") ? "NULL" : "'" + Document3PathOne.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document3PathTwo == null || Document3PathTwo.ToString() == "") ? "NULL" : "'" + Document3PathTwo.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document4PathOne == null || Document4PathOne.ToString() == "") ? "NULL" : "'" + Document4PathOne.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document4PathTwo == null || Document4PathTwo.ToString() == "") ? "NULL" : "'" + Document4PathTwo.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document5PathOne == null || Document5PathOne.ToString() == "") ? "NULL" : "'" + Document5PathOne.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document5PathTwo == null || Document5PathTwo.ToString() == "") ? "NULL" : "'" + Document5PathTwo.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document6PathOne == null || Document6PathOne.ToString() == "") ? "NULL" : "'" + Document6PathOne.ToString().Replace("'", "''") + "'") +
                                         "," + ((Document6PathTwo == null || Document6PathTwo.ToString() == "") ? "NULL" : "'" + Document6PathTwo.ToString().Replace("'", "''") + "'") +
                                         "," + ((DukeReferenceName == null || DukeReferenceName.ToString() == "") ? "NULL" : "'" + DukeReferenceName.ToString().Replace("'", "''") + "'") +
                                         "," + ((DukeReffernceSignOne == null || DukeReffernceSignOne.ToString() == "") ? "NULL" : "'" + DukeReffernceSignOne.ToString().Replace("'", "''") + "'") +
                                         "," + ((OtherReferenceName == null || OtherReferenceName.ToString() == "") ? "NULL" : "'" + OtherReferenceName.ToString().Replace("'", "''") + "'") +
                                         "," + ((DukeReffernceSignOther == null || DukeReffernceSignOther.ToString() == "") ? "NULL" : "'" + DukeReffernceSignOther.ToString().Replace("'", "''") + "'") +
                                         ", GETDATE(),GETDATE() " +
                                         " );" +
                                         "Select @InterviewFormId");
                //"SELECT @@IDENTITY ";
                sqlCmd.ExecuteNonQuery();

            }

           
            var names = FullName.Split(' ');
            string FirstName = names[0];
            string MiddleName = names[1];
            string LastName = names[2];

            sqlCmd.CommandText = " UPDATE Registrations SET" +
                                 " FirstName  = " + ((FirstName == null) ? "NULL" : "'" + FirstName.Replace("'", "''") + "'") + "," +
                                 " MiddleName  = " + ((MiddleName == null) ? "NULL" : "'" + MiddleName.Replace("'", "''") + "'") + "," +
                                 " LastName  = " + ((LastName == null) ? "NULL" : "'" + LastName.Replace("'", "''") + "'") + "," +
                                 " Address = " + ((PresentAddress == null) ? "NULL" : "'" + PresentAddress.Replace("'", "''") + "'") + "," +
                                 " City  = " + ((PresentVillage == null) ? "NULL" : "'" + PresentVillage.Replace("'", "''") + "'") + "," +
                                 " District  = " + ((PresentDistrict == null) ? "NULL" : "'" + PresentDistrict.Replace("'", "''") + "'") + "," +
                                 " MobileNo  = " + ((PresentMobileNo == null) ? "NULL" : "'" + PresentMobileNo.Replace("'", "''") + "'") + "," +
                                 " AadharCardNo  = " + ((AadharCardNo == null) ? "NULL" : "'" + AadharCardNo.Replace("'", "''") + "'") + "," +
                                 " LastUpdatedOn = getdate() " +
                                 " WHERE RegistrationId = '" + RegistrationId + "'";

            sqlCmd.ExecuteNonQuery();

            st.Append(InterviewFormId);

            //da3 = _aPI_BLL.returnDataTable("select * from InterviewForms where RegistrationId = '" + RegistrationId + "' ");

            //st.Append(DataTableToJsonObj(da3));
            //if (da3 == null)
            //{
            //    ReturnVal = GetReturnValue("209", "No Record Found", st);
            //}

            //if (st.ToString() == "[]" || st.ToString() == "")
            //{
            //    ReturnVal = GetReturnValue("209", "No Record Found", st);
            //}

            //if (da3.Rows.Count > 0)
            //{
            //    ReturnVal = GetReturnValue("200", "Data Get", st);
            //}

            _generalDAL.CloseSQLConnection();

            ReturnVal = GetReturnValue("200", "Data Saved Successfully.", st);

            if (st.ToString() != "[]")
                return ReturnVal.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]");
            else
                return ReturnVal.Replace("\\", "").Replace("\"[]\"", "[]");

        }
        catch (Exception ex)
        {
            StringBuilder s = new StringBuilder();
            s.Append(ex.Message);
            ReturnVal = GetReturnValue("209", "Data Get Issue", s);
            _generalDAL.CloseSQLConnection();
            return ReturnVal.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]");

        }
    }

    public class ClassFinalUpdate
    {
        public string status { get; set; }
        public string message { get; set; }
        public string result { get; set; }

        public static ClassFinalUpdate FromDataRow(DataRow row)
        {
            ClassFinalUpdate2 result1 = new ClassFinalUpdate2();
            if (row["AuditId"] != DBNull.Value)
            {
                result1.AuditId = (string)row["AuditId"];
            }
            else
            {
                result1.AuditId = "-";
            }

            var ClassFinalUpdate = new ClassFinalUpdate();

            if (result1.AuditId == "-")
            {
                ClassFinalUpdate = new ClassFinalUpdate
                {
                    status = (string)row["status"],
                    message = (string)row["message"],
                    result = result1.AuditId
                };
            }
            else
            {
                ClassFinalUpdate = new ClassFinalUpdate
                {
                    status = (string)row["status"],
                    message = (string)row["message"],
                    result = (string)row["AuditId"]
                };
            }

            return ClassFinalUpdate;
        }
    }


    public class ClassFinalUpdate2
    {
        public string AuditId { get; set; }
    }
}
