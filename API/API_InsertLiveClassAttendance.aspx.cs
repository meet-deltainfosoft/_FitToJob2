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


public partial class API_InsertLiveClassAttendance : System.Web.UI.Page
{
    string RegistrationId;
    string SubId;
    string LiveClassId;
    string InsertedByUserId;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                RegistrationId = (((Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "")) ? Request.Form["RegistrationId"].ToString() : null);
                SubId = (((Request.Form["SubId"] != null && Request.Form["SubId"] != "")) ? Request.Form["SubId"].ToString() : null);
                LiveClassId = (((Request.Form["LiveClassId"] != null && Request.Form["LiveClassId"] != "")) ? Request.Form["LiveClassId"].ToString() : null);
                InsertedByUserId = (((Request.Form["InsertedByUserId"] != null && Request.Form["InsertedByUserId"] != "")) ? Request.Form["InsertedByUserId"].ToString() : null);

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
        string LiveClassAttendanceId = "";
        try
        {


            LiveClassAttendanceId = _aPI_BLL.InsertUpdateWithReturnIdentity(" DECLARE @LiveClassAttendanceId as uniqueidentifier" +
                                                             " SET @LiveClassAttendanceId = NEWID() " +
                                                             " insert into LiveClassAttendances (LiveClassAttendanceId, RegistrationId, SubId, LiveClassId,InTime, " +
                                                             " InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId) " +
                                                             " values (@LiveClassAttendanceId " +
                                                             " , " + ((RegistrationId == null) ? "NULL" : "'" + RegistrationId.ToString().Replace("'", "''") + "'") + " " +
                                                             " , " + ((SubId == null) ? "NULL" : "'" + SubId.ToString().Replace("'", "''") + "'") + " " +
                                                             " , " + ((LiveClassId == null) ? "NULL" : "'" + LiveClassId.ToString().Replace("'", "''") + "'") + " " +
                                                             " , GETDATE(), GETDATE(), GETDATE(), '" + InsertedByUserId + "', NULL " +

                                                             "); Select @LiveClassAttendanceId;");

            da = _aPI_BLL.returnDataTable(" select LiveClassAttendanceId from LiveClassAttendances where LiveClassAttendanceId = '" + LiveClassAttendanceId.ToString() + "'  ");
            st.Append(DataTableToJsonObj(da));

            if (da == null)
            {
                ReturnVal = GetReturnValue("209", "No Record Found", st);
            }

            if (st.ToString() == "[]" || st.ToString() == "")
            {
                ReturnVal = GetReturnValue("209", "No Record Found", st);
            }

            if (da != null)
            {
                if (da.Rows.Count > 0)
                {
                    ReturnVal = GetReturnValue("200", "Ok", st);
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
}