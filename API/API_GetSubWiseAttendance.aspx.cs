using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;

public partial class API_GetSubWiseAttendance : System.Web.UI.Page
{
    string RegistrationId;
    string Type;
    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "")
                    RegistrationId = Request.Form["RegistrationId"].ToString();
                else
                    RegistrationId = null;

                if (Request.Form["Type"] != null && Request.Form["Type"] != "")
                    Type = Request.Form["Type"].ToString();
                else
                    Type = null;

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
        DataSet ds = new DataSet();

        DataTable da = new DataTable();
        DataTable da1 = new DataTable();

        StringBuilder st = new StringBuilder();
        string ReturnVal = "";
        try
        {
            if (Type != null && Type != "")
            {

                if (Type.ToString().ToUpper() == "EXAM")
                {
                    da = _aPI_BLL.returnDataTable("Select Name,SUM(ISNULL(aa.TotalExams,0)) as Total,SUM(ISNULL (aa.TotalPresent, 0)) as Present,SUM(ISNULL(aa.TotalAbsent, 0)) as Absent " +
                                                  " from( " +
                                                  " Select s.Name, e.ExamScheduleId, Count(*) as TotalExams, (Select Count(distinct " +
                                                  " ExamScheduleId) from Exams where Registrationid = el.RegistrationId and " +
                                                  " ExamscheduleId = e.ExamscheduleId) as TotalPresent, " +
                                                  " (Select Count(Distinct ExamScheduleId) from ExamSchedules where ExamScheduleId " +
                                                  " not in (Select ExamScheduleId from Exams where Registrationid = el.RegistrationId and ExamscheduleId = e.ExamscheduleId) and Registrationid = el.RegistrationId and ExamscheduleId = e.ExamscheduleId) as TotalAbsent " +
                                                  " from examschedulelns el " +
                                                  " inner join ExamSchedules e on e.ExamScheduleId = el.ExamscheduleId " +
                                                  " inner join subs s on s.SubId = e.SubId " +
                                                  " where RegistrationId = '" + RegistrationId.ToString() + "' " +
                                                  " group by s.Name,el.Registrationid,e.ExamscheduleId " +
                                                  " UNION " +
                                                  " Select p.PatternName,e.ExamScheduleId,Count(*) as TotalExams,(Select Count(distinct ExamScheduleId) from Exams where Registrationid = el.RegistrationId " +
                                                  " and ExamscheduleId = e.ExamscheduleId) as TotalPresent, " +
                                                  " (Select Count(Distinct ExamScheduleId) from ExamSchedules where ExamScheduleId " +
                                                  " not in (Select ExamScheduleId from Exams where Registrationid = el.RegistrationId and ExamscheduleId = e.ExamscheduleId) and Registrationid = el.RegistrationId and ExamscheduleId = e.ExamscheduleId) as TotalAbsent " +
                                                  " from examschedulelns el " +
                                                  " inner join ExamSchedules e on e.ExamScheduleId = el.ExamscheduleId " +
                                                  " inner join Patterns p on p.PatternId = e.PatternId " +
                                                  " where RegistrationId = '" + RegistrationId.ToString() + "' " +
                                                  " group by p.PatternName,el.Registrationid,e.ExamscheduleId) as aa " +
                                                  " Group by aa.Name ");
                    st.Append(DataTableToJsonObj(da));
                }
                else
                {
                    da = _aPI_BLL.returnDataTable(" Select s.Name,Count(*) as TotalLiveClass, " +
                                                  " (Select Count(distinct LiveClassId) from LiveClassAttendances where RegistrationId = r.RegistrationId and SubId = s.SubId) as TotalPresent, " +
                                                  " (Select Count(*) from LiveClasses where LiveClassId not in (Select LiveClassId " +
                                                  " from LiveClassAttendances where RegistrationId = r.RegistrationId) and SubId = s.SubId ) as TotalAbsent " +
                                                  " from LiveClasses l " +
                                                  " inner join subs s on s.subid = l.subid " +
                                                  " inner join Registration r on r.StandardId = s.Standardtextlistid " +
                                                  " where r.RegistrationId = '" + RegistrationId.ToString() + "' " +
                                                  " Group by s.SubId,s.Name,r.RegistrationId ");
                    st.Append(DataTableToJsonObj(da));
                }
            }
            else
            {
                da = null;
            }

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
                    ReturnVal = GetReturnValue("200", "Data Get", st);
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

    public string DataTableToJsonObj(DataSet ds)
    {
        StringBuilder JsonString = new StringBuilder();
        if (ds != null && ds.Tables.Count > 0)
        {
            JsonString.Append("[");
            for (int d = 0; d <= ds.Tables.Count - 1; d++)
            {
                JsonString.Append("[");
                for (int i = 0; i < ds.Tables[d].Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < ds.Tables[d].Columns.Count; j++)
                    {
                        if (j < ds.Tables[d].Columns.Count - 1)
                        {
                            JsonString.Append("\"" + ds.Tables[d].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[d].Rows[i][j].ToString() + "\",");
                        }
                        else if (j == ds.Tables[d].Columns.Count - 1)
                        {
                            JsonString.Append("\"" + ds.Tables[d].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[d].Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == ds.Tables[d].Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }

                if (d == ds.Tables.Count - 1)
                    JsonString.Append("]");
                else
                    JsonString.Append("],");
            }
            JsonString.Append("]");

            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }
}