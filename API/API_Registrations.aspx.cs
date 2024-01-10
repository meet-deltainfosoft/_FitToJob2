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


public partial class API_API_Registrations : System.Web.UI.Page
{
    private GeneralDAL _generalDAL = new GeneralDAL();
    API_BLL _aPI_BLL = new API_BLL();

    #region Declare Variable


    string AadharCardNo;
    string FirstName;
    string MiddleName;
    string LastName;
    string MobileNo;
    string City;
    string Taluka;
    string District;
    string State;
    string Address;
    string FCMId;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                #region Argument values paased in variable



                AadharCardNo = (((Request.Form["AadharCardNo"] != null && Request.Form["AadharCardNo"] != "")) ? (Request.Form["AadharCardNo"].ToString()) : null);
                FirstName = (((Request.Form["FirstName"] != null && Request.Form["FirstName"] != "")) ? (Request.Form["FirstName"].ToString()) : null);
                MiddleName = (((Request.Form["MiddleName"] != null && Request.Form["MiddleName"] != "")) ? (Request.Form["MiddleName"].ToString()) : null);
                LastName = (((Request.Form["LastName"] != null && Request.Form["LastName"] != "")) ? (Request.Form["LastName"].ToString()) : null);
                MobileNo = (((Request.Form["MobileNo"] != null && Request.Form["MobileNo"] != "")) ? (Request.Form["MobileNo"].ToString()) : null);
                City = (((Request.Form["City"] != null && Request.Form["City"] != "")) ? (Request.Form["City"].ToString()) : null);
                Taluka = (((Request.Form["Taluka"] != null && Request.Form["Taluka"] != "")) ? (Request.Form["Taluka"].ToString()) : null);
                District = (((Request.Form["District"] != null && Request.Form["District"] != "")) ? (Request.Form["District"].ToString()) : null);
                State = (((Request.Form["State"] != null && Request.Form["State"] != "")) ? (Request.Form["State"].ToString()) : null);
                Address = (((Request.Form["Address"] != null && Request.Form["Address"] != "")) ? (Request.Form["Address"].ToString()) : null);
                FCMId = (((Request.Form["FCMId"] != null && Request.Form["FCMId"] != "")) ? (Request.Form["FCMId"].ToString()) : null);

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

        string RegistrationId = "";
        string ReturnVal = "";
        _generalDAL.OpenSQLConnection();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;

        try
        {
            da = _aPI_BLL.returnDataTable(" select * from Registrations where Mobileno = '" + MobileNo + "' ");

            if (da.Rows.Count == 0)
            {
                sqlCmd.CommandText = ("DECLARE @RegistrationId  uniqueidentifier;" +
                                         " SET @RegistrationId = NEWID() " +
                                         " INSERT INTO Registrations (RegistrationId,AadharCardNo, FirstName, MiddleName, LastName, MobileNo, City, Taluka, District, State, InsertedOn, LastUpdatedOn, Address, FCMId) " +
                                         " VALUES (@RegistrationId " +
                                         "," + ((AadharCardNo == null || AadharCardNo.ToString() == "") ? "NULL" : "'" + AadharCardNo.ToString().Replace("'", "''") + "'") +
                                         "," + ((FirstName == null || FirstName.ToString() == "") ? "NULL" : "'" + FirstName.ToString().Replace("'", "''") + "'") +
                                         "," + ((MiddleName == null || MiddleName == "") ? "NULL" : "'" + MiddleName.Replace("'", "''") + "'") +
                                         "," + ((LastName == null || LastName == "") ? "NULL" : "'" + LastName.Replace("'", "''") + "'") +
                                         "," + ((MobileNo == null || MobileNo == "") ? "NULL" : "'" + MobileNo.Replace("'", "''") + "'") +
                                         "," + ((City == null || City == "") ? "NULL" : "'" + City.Replace("'", "''") + "'") +
                                         "," + ((Taluka == null || Taluka == "") ? "NULL" : "'" + Taluka.Replace("'", "''") + "'") +
                                         "," + ((District == null || District == "") ? "NULL" : "'" + District.Replace("'", "''") + "'") +
                                         "," + ((State == null || State == "") ? "NULL" : "'" + State.Replace("'", "''") + "'") +
                                         ",GETDATE()" +
                                         ",GetDate()" +
                                         "," + ((Address == null || Address == "") ? "NULL" : "'" + Address.Replace("'", "''") + "'") +
                                         "," + ((FCMId == null || FCMId == "") ? "NULL" : "'" + FCMId.Replace("'", "''") + "'") +
                                         " );" +
                                         "Select @RegistrationId");
                //"SELECT @@IDENTITY ";

                RegistrationId = sqlCmd.ExecuteScalar().ToString();

                // code for adding ExamScheduleLns insert

                DataTable dtExamSchedules = new DataTable();

                sqlCmd.CommandText = " select * from " + ConfigurationSettings.AppSettings["ERPDBName"].ToString() + "..ExamSchedules where isnull(IsDefaultTest, 0) = 1 ";
                dtExamSchedules.Load(sqlCmd.ExecuteReader());

                for (int i = 0; i < dtExamSchedules.Rows.Count; i++)
                {
                    sqlCmd.CommandText = " insert into " + ConfigurationSettings.AppSettings["ERPDBName"].ToString() + "..ExamScheduleLns values (newid(), '" + dtExamSchedules.Rows[i]["ExamScheduleId"] + "', (select max(LnNo + 1)  from " + ConfigurationSettings.AppSettings["ERPDBName"].ToString() + " ..ExamScheduleLns  where ExamScheduleId = '" + dtExamSchedules.Rows[i]["ExamScheduleId"] + "'), '"+ RegistrationId +"', getdate(), getdate(),  '399C5CC2-0D66-440F-8E9C-01BA701EEB82', NULL, NULL)";
                    sqlCmd.ExecuteScalar();
                    //excecute
                }

                // code for adding ExamScheduleLns insert

                st.Append(RegistrationId);


                ReturnVal = GetReturnValue("200", "Data Saved Successfully", st);

                _generalDAL.CloseSQLConnection();

            }
            else
            {
                StringBuilder s = new StringBuilder();
                ReturnVal = GetReturnValue("209", "Mobile Number Allready Reistered Try Defferent", s);
                return ReturnVal.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]");
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
