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

public partial class API_CaptureSelfi : System.Web.UI.Page
{
    string RegistrationId;

    HttpPostedFile fileStudentPhoto;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                RegistrationId = (((Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "")) ? Request.Form["RegistrationId"].ToString() : null);
                fileStudentPhoto = (((Request.Files["fileStudentPhoto"] != null && Request.Files["fileStudentPhoto"].FileName != "")) ? Request.Files["fileStudentPhoto"] : null);

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
            string path1 = null;

            path1 = saveFile(fileStudentPhoto, 1);

            Int64? AutoId = 0;
            AutoId = Convert.ToInt64(_aPI_BLL.InsertUpdateWithReturnIdentity(" insert into ExamSelfies (RegistrationId, PhotoPath " +
                                                                     " ) " +
                                                                     " values (" + ((RegistrationId == null) ? "NULL" : "'" + RegistrationId.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path1 == null) ? "NULL" : "'" + path1.ToString().Replace("'", "''") + "'") + " " +
                                                                     "); Select @@Identity;"));

            ReturnVal = GetReturnValue("200", "Data Saved.", st);

            if (st.ToString() != "[]")
                return ReturnVal.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]");
            else
                return ReturnVal.Replace("\\", "").Replace("\"[]\"", "[]");
        }
        catch (Exception ex)
        {
            StringBuilder s = new StringBuilder();
            s.Append(ex.Message);
            ReturnVal = GetReturnValue("209", "Data Save Issue", s);
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

            string msExcelFilePathOnServer = ConfigurationSettings.AppSettings["FolderPath"] + "" + Convert.ToDateTime(System.DateTime.Now).ToString("ddMMMyyyy") + "/Selfi/";

            bool exists = System.IO.Directory.Exists(msExcelFilePathOnServer);

            if (!exists)
                System.IO.Directory.CreateDirectory(msExcelFilePathOnServer);

            path = msExcelFilePathOnServer + uploadedImage.FileName.Replace("." + oExtension, "") + "" + "." + oExtension;
            uploadedImage.SaveAs(path);
        }

        return path;
    }
}