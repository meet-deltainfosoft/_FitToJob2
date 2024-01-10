using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;

public partial class API_API_SelectJobProfile : System.Web.UI.Page
{
    //string Language;
    //string StaffCategoryTextListId;
    //string PDFFile;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //if (Request.Form["Language"] != null && Request.Form["Language"] != "")
                //    Language = Request.Form["Language"].ToString();
                //else
                //    Language = null;

                //if (Request.Form["StaffCategoryTextListId"] != null && Request.Form["StaffCategoryTextListId"] != "")
                //    StaffCategoryTextListId = Request.Form["StaffCategoryTextListId"].ToString();
                //else
                //    StaffCategoryTextListId = null;

                //if (Request.Form["PDFFile"] != null && Request.Form["PDFFile"] != "")
                //    PDFFile = Request.Form["PDFFile"].ToString();
                //else
                //    PDFFile = null;

                Response.ContentType = "application/json";

                Response.Write(selectdata());
                
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

    public string selectdata()
    {
        DataTable da = new DataTable();
        StringBuilder st = new StringBuilder();
        string ReturnVal = "";
        try
        {
            //da = _aPI_BLL.returnDataTable("select J.JobOfferingId , J.StaffCategoryTextListId , J.PDFFile from JobOfferings J " +
            //                              " Inner Join Textlists T on T.TextListId = J.StaffCategoryTextListId");

            //,JobOfferingId,StaffCategoryTextListId ,DepartmentId,DivisionId,DesignationId,PDFFile, NoOfSeats,ValidFrom , ValidTo 

            da = _aPI_BLL.returnDataTable("select distinct T.[Text] as StaffCategory from JobOfferings a" +
                                           " Left join TextLists T on T.TextListId = a.StaffCategoryTextListId order by T.[Text] ");

            //da = _aPI_BLL.returnDataTable("select distinct T.[Text] as StaffCategory from JobOfferings a" +
            //                             " Left join TextLists T on T.TextListId = a.StaffCategoryTextListId  " +
            //                             " where T.Language = '" + Language + "'order by T.[Text] ");

            st.Append(DataTableToJsonObj(da));

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