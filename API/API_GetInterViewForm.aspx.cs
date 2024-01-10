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

public partial class API_API_GetInterViewForm : System.Web.UI.Page
{
    string RegistrationId;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
               
                RegistrationId = ((Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "") ? Request.Form["RegistrationId"].ToString() : null);

               
                Response.ContentType = "application/json";

                Response.Write(selectdata(RegistrationId));

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

    public class ResponseData
    {
        public int? status { get; set; }
        public string message { get; set; }
        public ArrayList Result { get; set; }
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
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                    }
                    else if (j == ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
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

    public string selectdata(string RegistrationId)
    {
        DataTable da = new DataTable();
        DataTable da1 = new DataTable();
        StringBuilder st = new StringBuilder();
        string ReturnVal = "";
        try
        {
            da = _aPI_BLL.returnDataTable(" select Top 1 * from InterviewForms "+
                                          " Where RegistrationId = '" + RegistrationId + "'");

            st.Append(DataTableToJsonObj(da));
            if (da.Rows.Count == 0)
            {
                da1 = _aPI_BLL.returnDataTable(" select top 1 RegistrationId ,upper(cast (FirstName + ' ' + isnull(LastName, MiddleName) as varchar(300)) collate SQL_Latin1_General_CP1_CI_AS) as FullName , Address As PresentAddress ,MobileNo As PresentMobileNo , District As PresentDistrict ,AadharCardNo AS AadharCardNo , City As PresentVillage ,null As  PresentPinCode , null As PresentPost "+
                                               " ,null As  PermanentAddress , null As PermanentPost,null As  PermanentVillage , null As PermanentDistrict,null As  PermanentPinCode , null As PermanentMobileNo   " +
                                               " ,null As  DOB , null As BloodGroup,null As  PanCardNo , null As ElectionCardNo,null As  Category , null As Email   " +
                                               " ,null As  FatherName , null As FatherOccupation,null As  FatherEducation , null As FatherMobileNo   " +
                                               " ,null As  MotherName , null As MotherOccupation,null As  MotherEducation , null As MotherMobileNo   " +
                                               " ,null As  WifeName , null As WifeOccupation,null As  WifeEducation , null As WifeMobileNo   " +
                                               " ,null As  BrotherName , null As BrotherOccupation,null As  BrotherEducation , null As BrotherMobileNo   " +
                                               " ,null As  NomineeName , null As NomineeDOB,null As  NomineeRelation , null As NomineeAge   " +
                                               " ,null As  Standanrd10Subject , null As Standanrd10PassingYear,null As  Standanrd10Percentage , null As Standanrd12Subject,null As  Standanrd12PassingYear , null As Standanrd12Percentage   " +
                                               " ,null As  GraduateSubject , null As GraduatePassingYear,null As  GraduatePercentage , null As PostGraduateSubject,null As  PostGraduatePassingYear , null As PostGraduatePercentage   " +
                                               " ,null As  OtherSubject , null As OtherPassingYear,null As  OtherPercentage , null As CertificateCourseName,null As  CertificateCourseYear , null As TrainingName   " +
                                               " ,null As  TrainingYear , null As MedalName,null As  MedalYear , null As FirstCompanyName,null As  FirstCompanyDesignation , null As FirstCompanyExp, null As FirstCompanySalary   " +
                                               " ,null As  SecondCompanyName , null As SecondCompanyDesignation,null As  SecondCompanyExp , null As SecondCompanySalary,null As  ThirdCompanyName , null As ThirdCompanyDesignation, null As ThirdCompanyExp, null As ThirdCompanySalary,null As OtherExpNoExpDetails   " +
                                               " From Registrations " +
                                               " Where RegistrationId = '" + RegistrationId + "'");
                st.Append(DataTableToJsonObj(da1));
            }

            

            //if (da == null)
            //{
            //    ReturnVal = GetReturnValue("209", "No Record Found", st);
            //}

            //if (st.ToString() == "[]" || st.ToString() == "")
            //{
            //    ReturnVal = GetReturnValue("209", "No Record Found", st);
            //}

            if (da1.Rows.Count > 0)
            {
                ReturnVal = GetReturnValue("200", "Data Get", st);
            }
            if (da.Rows.Count > 0)
            {
                ReturnVal = GetReturnValue("200", "Data Get", st);
            }

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
            return ReturnVal.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]");
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
}