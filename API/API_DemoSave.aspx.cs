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


public partial class API_DemoSave : System.Web.UI.Page
{
    string StudentName;
    string MobileNo;

    HttpPostedFile ProfileImage;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                StudentName = (((Request.Form["StudentName"] != null && Request.Form["StudentName"] != "")) ? Request.Form["StudentName"].ToString() : null);
                MobileNo = (((Request.Form["MobileNo"] != null && Request.Form["MobileNo"] != "")) ? Request.Form["MobileNo"].ToString() : null);

                ProfileImage = (((Request.Files["ProfileImage"] != null && Request.Files["ProfileImage"].FileName != "")) ? Request.Files["ProfileImage"] : null);

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
        string ExamId = "";
        try
        {

            string path1 = null;

            path1 = saveFile(ProfileImage, 1);

            ExamId = _aPI_BLL.InsertUpdateWithReturnIdentity(" DECLARE @Id as uniqueidentifier" +
                                                             " SET @Id = NEWID() " +
                                                             " insert into DemoSaves (Id, StudentName, MobileNo, ProfileImage, InsertedOn, LastUpdatedOn) " +
                                                             " values ( @Id " +
                                                             " , " + ((StudentName == null) ? "NULL" : "'" + StudentName.ToString().Replace("'", "''") + "'") + " " +
                                                             " , " + ((MobileNo == null) ? "NULL" : "'" + MobileNo.ToString().Replace("'", "''") + "'") + " " +
                                                             " , " + ((ProfileImage == null) ? "NULL" : "'" + ProfileImage.ToString().Replace("'", "''") + "'") + " " +
                                                             " , GETDATE(), GETDATE()); Select @Id;");

            da = _aPI_BLL.returnDataTable(" select Id from DemoSaves where Id = '" + ExamId.ToString() + "'  ");
            st.Append(DataTableToJsonObj(da));

            if (da == null)
            {
                ReturnVal = GetReturnValue("209", "Entry Exist", st);
            }

            if (st.ToString() == "[]" || st.ToString() == "")
            {
                ReturnVal = GetReturnValue("209", "Entry Exist", st);
            }

            if (da != null)
            {
                if (da.Rows.Count > 0)
                {
                    ReturnVal = GetReturnValue("200", "Saved", st);
                }
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

    public string saveFile(HttpPostedFile fu, int srno)
    {
        string path = null;

        if (fu != null && fu.FileName != "")
        {
            byte[] imageSize = new byte[fu.ContentLength];
            HttpPostedFile uploadedImage = fu;

            string[] getExtenstion = fu.FileName.Split('.');
            string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

            path = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "DEMOSAVE_F" + srno + "" + "." + oExtension;
            uploadedImage.SaveAs(path);
        }

        return path;
    }
}