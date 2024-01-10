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

public partial class API_UpdateJobOfferingIdRegistrations : System.Web.UI.Page
{
    private GeneralDAL _generalDAL = new GeneralDAL();
    API_BLL _aPI_BLL = new API_BLL();

    #region Declare Variable

    string RegistrationId;
    string JobOfferingId;

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

                if (Request.Form["JobOfferingId"] != null)
                    JobOfferingId = Request.Form["JobOfferingId"].ToString();
                else
                    JobOfferingId = null;

                #endregion

                Response.ContentType = "application/json";
                Response.Write(InsertRegistrations(RegistrationId, JobOfferingId));
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

    private string InsertRegistrations(string RegistrationId, string JobOfferingId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        StringBuilder st = new StringBuilder();
        DataTable da = new DataTable();

        string RegistrationJobProfileLnId = "";
        string ReturnVal = "";
        _generalDAL.OpenSQLConnection();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;

        try
        {
            //string k = "";
            int i = 1;
            if (JobOfferingId != null)
            {
                string[] JobId;
                JobId = JobOfferingId.ToString().Split(',');
                foreach (string s in JobId)
                {
                    string subject;
                    subject = s.ToString();

                    sqlCmd.CommandText = ("DECLARE @RegistrationJobProfileLnId  uniqueidentifier;" +
                                                     " SET @RegistrationJobProfileLnId = NEWID() " +
                                                     " INSERT INTO RegistrationJobProfileLns (RegistrationJobProfileLnId,RegistrationId,JobOfferingId,LnNo , InsertedOn, LastUpdatedOn,InsertedByUserId ,LastUpdatedByUserId ) " +
                                                     " VALUES (@RegistrationJobProfileLnId " +
                                                     "," + ((RegistrationId == null || RegistrationId.ToString() == "") ? "NULL" : "'" + RegistrationId.ToString().Replace("'", "''") + "'") +
                                                     "," + ((subject == null || subject.ToString() == "") ? "NULL" : "'" + subject.ToString().Replace("'", "''") + "'") +
                                                     " , '" + i  + "' " +
                                                     ",GETDATE()" +
                                                     ",GetDate() ," +
                                                     " null , null " +
                                                     " );" +
                                                     "Select @RegistrationJobProfileLnId");

                    RegistrationJobProfileLnId = sqlCmd.ExecuteScalar().ToString();
                    i++;
                }
            }
            //"SELECT @@IDENTITY ";



            sqlCmd.CommandText = (" update Registrations set JobOfferingId = '" + JobOfferingId.ToString() + "' where RegistrationId = '" + RegistrationId.ToString() + "'  ");
            sqlCmd.ExecuteNonQuery();


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
