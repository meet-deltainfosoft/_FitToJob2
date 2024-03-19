using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;

public partial class API_GetTestTime : System.Web.UI.Page
{
    string ExamScheduleId;
    string RegistrationId;
    string TestId;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Form["ExamScheduleId"] != null && Request.Form["ExamScheduleId"] != "")
                    ExamScheduleId = Request.Form["ExamScheduleId"].ToString();
                else
                    ExamScheduleId = null;

                if (Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "")
                    RegistrationId = Request.Form["RegistrationId"].ToString();
                else
                    RegistrationId = null;

                if (Request.Form["SubId"] != null && Request.Form["SubId"] != "")
                    TestId = Request.Form["SubId"].ToString();
                else
                    TestId = null;

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
        bool IsValid = false;
        string msg = "";

        try
        {
            da = _aPI_BLL.returnDataTable(" Select e.ExamscheduleId, e.TestId, e.SubId, e.ExamDate, e.ExamFromTime, e.ExamToTime " +
                                          " from ExamSchedules e " +
                                          " Inner Join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
                                          " where e.ExamScheduleId = '" + ExamScheduleId.ToString() + "' and e.SubId = '" + TestId.ToString() + "' " +
                                          " and el.RegistrationId = '" + RegistrationId.ToString() + "'");
            if (da != null)
            {
                if (da.Rows.Count > 0)
                {
                    DateTime now = Convert.ToDateTime(System.DateTime.Now);
                    //if (now.Date == Convert.ToDateTime(da.Rows[0]["ExamDate"]).Date)
                    //{
                    if (da.Rows.Count > 0)
                    {
                        //if (now.TimeOfDay >= Convert.ToDateTime(da.Rows[0]["ExamFromTime"]).TimeOfDay && now.TimeOfDay < Convert.ToDateTime(da.Rows[0]["ExamToTime"]).TimeOfDay)
                        //{
                        IsValid = true;
                        msg = "OK";
                        //}
                        //else
                        //{
                        //    IsValid = false;
                        //    msg = "Opps !! Exam time is " + Convert.ToDateTime(da.Rows[0]["ExamFromTime"]).ToString("hh:mm tt") + " to " + Convert.ToDateTime(da.Rows[0]["ExamToTime"]).ToString("hh:mm tt") + ".";
                        //}
                    }
                    else
                    {
                        IsValid = false;
                        msg = "Opps !! Exam time is " + Convert.ToDateTime(da.Rows[0]["ExamFromTime"]).ToString("hh:mm tt") + " to " + Convert.ToDateTime(da.Rows[0]["ExamToTime"]).ToString("hh:mm tt") + ".";
                    }
                    da = _aPI_BLL.returnDataTable("Select IsValid = '" + Convert.ToBoolean(IsValid) + "', MSG='" + msg.ToString() + "'");
                    st.Append(DataTableToJsonObj(da));
                }
            }

            if (da == null)
            {
                ReturnVal = GetReturnValue("209", "No Record Found", st);
            }

            if (st.ToString() == "[]" || st.ToString() == "")
            {
                ReturnVal = GetReturnValue("209", "No Record Found", st);
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