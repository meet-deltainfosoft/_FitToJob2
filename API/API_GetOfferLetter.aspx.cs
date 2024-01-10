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
using System.Data.SqlClient;

public partial class API_API_GetOfferLetter : System.Web.UI.Page
{
    string RegistrationId;
    string IsOfferLetter;
    private GeneralDAL _generalDAL = new GeneralDAL();
    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Form["IsOfferLetter"] != null && Request.Form["IsOfferLetter"] != "")
                    IsOfferLetter = Request.Form["IsOfferLetter"].ToString();
                else
                    IsOfferLetter = null;

                //if (Request.Form["StaffCategoryTextListId"] != null && Request.Form["StaffCategoryTextListId"] != "")
                //    StaffCategoryTextListId = Request.Form["StaffCategoryTextListId"].ToString();
                //else
                //    StaffCategoryTextListId = null;

                RegistrationId = ((Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "") ? Request.Form["RegistrationId"].ToString() : null);

                //if (Request.Form["PDFFile"] != null && Request.Form["PDFFile"] != "")
                //    PDFFile = Request.Form["PDFFile"].ToString();
                //else
                //    PDFFile = null;

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
        SqlCommand sqlCmd = new SqlCommand();
        StringBuilder st = new StringBuilder();
        DataTable da = new DataTable();

        // string RegistrationVerificationId = "";
        string ReturnVal = "";
        _generalDAL.OpenSQLConnection();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        try
        {
            //da = _aPI_BLL.returnDataTable(" select R.RegistrationId ,R.AadharCardNo,R.FirstName1,R.MobileNo,R.JobOfferingId  from Registration R " +
            //                              " where R.RegistrationId = '" + RegistrationId + "'");

            //sqlCmd.CommandText = " UPDATE HODInterViews SET " +
            //                     " IsOfferLetter ='A' " +
            //                     ",GOfferLetterOn=GetDate() " +
            //                     ",OfferLetterByUserId='399C5CC2-0D66-440F-8E9C-01BA701EEB82' " +
            //                     " WHERE RegistrationId = '" + RegistrationId + "'";

            //sqlCmd.ExecuteNonQuery();

            da = _aPI_BLL.returnDataTable(" select Top 1 Name , Remarks ,RegistrationId  from HODInterViews" +
                                          " where "+
                                         // " ApprovedDisapproved ='A' " +
                                          "  RegistrationId = '" + RegistrationId + "'");

            st.Append(DataTableToJsonObj(da));

            sqlCmd.CommandText = " UPDATE HODInterViews SET " +
                                 " IsOfferLetter ='" + IsOfferLetter + "' " +
                                 ",GOfferLetterOn=GetDate() " +
                                 ",OfferLetterByUserId='399C5CC2-0D66-440F-8E9C-01BA701EEB82' " +
                                 " WHERE RegistrationId = '" + RegistrationId + "'";

            sqlCmd.ExecuteNonQuery();
            

           // st.Append(RegistrationId);

            if (da == null)
            {
                //if (Language == "English")
                //    ReturnVal = GetReturnValue("209", "No Record Found.", st);
                //else if (Language == "Gujarati")
                //    ReturnVal = GetReturnValue("209", "કોઈ રેકોર્ડ મળ્યો નથી.", st);
                //else if (Language == "Hindi")
                //    ReturnVal = GetReturnValue("209", "कोई रिकॉर्ड नहीं मिला.", st);
                //else
                ReturnVal = GetReturnValue("209", "No Record Found.", st);
            }

            if (st.ToString() == "[]" || st.ToString() == "")
            {
                //if (Language == "English")
                //    ReturnVal = GetReturnValue("209", "No Record Found.", st);
                //else if (Language == "Gujarati")
                //    ReturnVal = GetReturnValue("209", "કોઈ રેકોર્ડ મળ્યો નથી.", st);
                //else if (Language == "Hindi")
                //    ReturnVal = GetReturnValue("209", "कोई रिकॉर्ड नहीं मिला.", st);
                //else
                ReturnVal = GetReturnValue("209", "No Record Found.", st);
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