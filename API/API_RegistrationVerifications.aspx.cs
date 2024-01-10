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


public partial class API_RegistrationVerifications : System.Web.UI.Page
{
    private GeneralDAL _generalDAL = new GeneralDAL();
    API_BLL _aPI_BLL = new API_BLL();

    #region Declare Variable


    string RegistrationId;
    HttpPostedFile PhotoPath;
    //HttpPostedFile SelfIntroVideoPath;
    //HttpPostedFile ResumeUpload;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                #region Argument values paased in variable



                if (Request.Form["RegistrationId"] != null)
                    RegistrationId = Request.Form["RegistrationId"].ToString();
                else
                    RegistrationId = null;

                if (Request.Files["PhotoPath"] != null && Request.Files["PhotoPath"].ToString() != "")
                    PhotoPath = Request.Files["PhotoPath"];
                else
                    PhotoPath = null;

                //if (Request.Files["SelfIntroVideoPath"] != null && Request.Files["SelfIntroVideoPath"].ToString() != "")
                //    SelfIntroVideoPath = Request.Files["SelfIntroVideoPath"];
                //else
                //    SelfIntroVideoPath = null;

                //if (Request.Files["ResumeUpload"] != null && Request.Files["ResumeUpload"].ToString() != "")
                //    ResumeUpload = Request.Files["ResumeUpload"];
                //else
                //    ResumeUpload = null;

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
        StringBuilder st1 = new StringBuilder();
        DataTable da = new DataTable();

        string RegistrationVerificationId = "";
        string ReturnVal = "";
        _generalDAL.OpenSQLConnection();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;

        try
        {
            string fullPath = "";
            string fullPath1 = "";
            string fullPath2 = "";
            string UploadFileName2 = "";

            if (PhotoPath != null && PhotoPath.FileName != "")
            {
                string UploadFileName = PhotoPath.FileName;
                int lastSlash = UploadFileName.LastIndexOf("\\");
                string trailingPath = UploadFileName.Substring(lastSlash + 1);
                fullPath = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath;
                PhotoPath.SaveAs(fullPath);
            }
            //if (SelfIntroVideoPath != null && SelfIntroVideoPath.FileName != "")
            //{
            //    string UploadFileName1 = SelfIntroVideoPath.FileName;
            //    int lastSlash1 = UploadFileName1.LastIndexOf("\\");
            //    string trailingPath1 = UploadFileName1.Substring(lastSlash1 + 1);
            //    fullPath1 = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath1;
            //    SelfIntroVideoPath.SaveAs(fullPath1);
            //}
            //if (ResumeUpload != null && ResumeUpload.FileName != "")
            //{
            //    UploadFileName2 = ResumeUpload.FileName;
            //    int lastSlash2 = UploadFileName2.LastIndexOf("\\");
            //    string trailingPath2 = UploadFileName2.Substring(lastSlash2 + 1);
            //    fullPath2 = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath2;
            //    ResumeUpload.SaveAs(fullPath2);
            //}

            da = _aPI_BLL.returnDataTable(" select Top 1 * from RegistrationVerifications " +
                                          " Where RegistrationId = '" + RegistrationId + "'");

            st1.Append(DataTableToJsonObj(da));

            if (da.Rows.Count >= 1)
            {
                sqlCmd.CommandText = (" update RegistrationVerifications set " +
                                      " PhotoPath = " + ((fullPath == null) ? "NULL" : "'" + fullPath.ToString() + "'") + " " +
                                      " , LastUpdatedOn = GETDATE()" +
                                      " where RegistrationId = '" + RegistrationId.ToString() + "' ");

                sqlCmd.ExecuteNonQuery();
            }
            else
            {
                sqlCmd.CommandText = (" DECLARE  @RegistrationVerificationId uniqueidentifier;" +
                                      " SET @RegistrationVerificationId = NewId()" +
                                      " INSERT INTO RegistrationVerifications (RegistrationVerificationId,RegistrationId,PhotoPath,InsertedOn,LastUpdatedOn)" +
                                      " VALUES(@RegistrationVerificationId " +
                                      "," + ((RegistrationId == null || RegistrationId.ToString() == "") ? "NULL" : "'" + RegistrationId.ToString().Replace("'", "''") + "'") +
                                      "," + ((fullPath == null || fullPath.ToString() == "") ? "NULL" : "'" + fullPath.ToString() + "'") + "," +
                                      " GETDATE(),GETDATE()" +
                                      ");" +
                                      "Select @RegistrationVerificationId");
                //"SELECT @@IDENTITY ";

                RegistrationId = sqlCmd.ExecuteScalar().ToString();
            }
           

            st.Append(RegistrationId);


            ReturnVal = GetReturnValue("200", "Data Saved Successfully", st);

            _generalDAL.CloseSQLConnection();

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
