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


public partial class API_FinalSubmit : System.Web.UI.Page
{
    string RegistrationId;
    string ExamScheduleId;
    string StopLat;
    string StopLong;
    string InsertedByUserId;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                RegistrationId = (((Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "")) ? Request.Form["RegistrationId"].ToString() : null);
                ExamScheduleId = (((Request.Form["ExamScheduleId"] != null && Request.Form["ExamScheduleId"] != "")) ? Request.Form["ExamScheduleId"].ToString() : null);
                StopLat = (((Request.Form["StopLat"] != null && Request.Form["StopLat"] != "")) ? Request.Form["StopLat"].ToString() : null);
                StopLong = (((Request.Form["StopLong"] != null && Request.Form["StopLong"] != "")) ? Request.Form["StopLong"].ToString() : null);
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
        string ExamStartStopId = "";
        try
        {
            try
            {
                ExamStartStopId = _aPI_BLL.InsertUpdateWithReturnIdentity(" select Top 1 ExamStartStopTimeId from Examstartstoptimes where StopTime is null and EndDt is null " +
                                              " and RegistrationId = '" + RegistrationId.ToString() + "' " +
                                              " and ExamScheduleId = '" + ExamScheduleId.ToString() + "'" +
                                              " Order by InsertedOn desc ");
            }
            catch
            {
                ExamStartStopId = "";
            }

            if (ExamStartStopId != null && ExamStartStopId != "")
            {
                _aPI_BLL.InsertUpdateNonQuery(" UPDATE Examstartstoptimes SET " +
                                              " EndDt=GETDATE() " +
                                              ",IsFinalSubmit = 1 " +
                                              ",StopTime = GETDATE()" +
                                              ",StopLat =" + ((StopLat == null) ? "NULL" : "'" + StopLat.ToString() + "'") +
                                              ",StopLong=" + ((StopLong == null) ? "NULL" : "'" + StopLong.ToString() + "'") + "" +
                                              ",LastUpdatedOn=GETDATE() " +
                                              ",LastUpdatedByUserId = '" + InsertedByUserId + "'" +
                                              " WHERE ExamStartStopTimeId='" + ExamStartStopId.ToString() + "'");

                da = _aPI_BLL.returnDataTable(" select ExamStartStopTimeId from Examstartstoptimes where ExamStartStopTimeId = '" + ExamStartStopId.ToString() + "'  ");
                st.Append(DataTableToJsonObj(da));
            }
            else
            {
                da = null;
            }

            if (da == null)
            {
                ReturnVal = GetReturnValue("209", "No Records Found To Stop Exam", st);
            }

            if (st.ToString() == "[]" || st.ToString() == "")
            {
                ReturnVal = GetReturnValue("209", "No Records Found", st);
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