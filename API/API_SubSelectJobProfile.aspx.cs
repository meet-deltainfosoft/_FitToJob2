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

public partial class API_API_SubSelectJobProfile : System.Web.UI.Page
{
    //string JobOfferingId;
    //string StaffCategoryTextListId;
    //string Language;

    string StaffCategory;

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

                StaffCategory = ((Request.Form["StaffCategory"] != null && Request.Form["StaffCategory"] != "") ? Request.Form["StaffCategory"].ToString() : null);

                //if (Request.Form["PDFFile"] != null && Request.Form["PDFFile"] != "")
                //    PDFFile = Request.Form["PDFFile"].ToString();
                //else
                //    PDFFile = null;

                Response.ContentType = "application/json";

                Response.Write(selectdata(StaffCategory));

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

    public string selectdata(string StaffCategory)
    {
        DataTable da = new DataTable();
        StringBuilder st = new StringBuilder();
        string ReturnVal = "";
        try
        {
            da = _aPI_BLL.returnDataTable(" select j.*, t.[text] as 'StaffCategory', d.text as 'DeptName', ds.Name as 'Designation' from JobOfferings j " +
                                          " left join Textlists t on t.TextListId = j.StaffCategoryTextListId " +
                                          " left join TextLists d on d.TextListId = j.DepartmentId " +
                                          " left join Designations ds on ds.DesignationId = j.DesignationId " +
                                          " where t.[Text] In ('" + StaffCategory.Replace(",","','") + "') order by d.text, ds.Name ");

            //da = _aPI_BLL.returnDataTable(" select j.*, t.[text] as 'StaffCategory', d.text as 'DeptName', ds.Name as 'Designation' from JobOfferings j " +
            //                              " left join Textlists t on t.TextListId = j.StaffCategoryTextListId " +
            //                              " left join TextLists d on d.TextListId = j.DepartmentId " +
            //                              " left join Designations ds on ds.DesignationId = j.DesignationId " +
            //                              " where t.[Text] = N'" + StaffCategory + "' And T.Language='" + Language + "' order by d.text, ds.Name ");

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