using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;

public partial class API_GetAttendance : System.Web.UI.Page
{
    string RegistrationId;

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
            da = _aPI_BLL.returnDataTable(" Select SUM(ISNULL(TotalLiveClass,0)) as TotalLiveClass,SUM(ISNULL(TotalPresent,0)) as TotalPresent,"+
                                          " SUM(ISNULL(TotalAbsent,0)) as TotalAbsent From( "+
                                          " Select Count(*) as TotalLiveClass, (Select Count(distinct LiveClassId) from LiveClassAttendances where " +
                                          " RegistrationId = r.RegistrationId and SubId = s.SubId) as TotalPresent, (Select Count(*) from LiveClasses where LiveClassId not in " +
                                          " (Select LiveClassId from LiveClassAttendances where RegistrationId = r.RegistrationId) and SubId = s.SubId ) as TotalAbsent " +
                                          " from LiveClasses l " +
                                          " inner join subs s on s.subid = l.subid " +
                                          " inner join Registration r on r.StandardId = s.Standardtextlistid " +
                                          " where r.RegistrationId = '" + RegistrationId.ToString() + "' " +
                                          " Group by r.RegistrationId,s.SubId ) as aa ");

            da1 = _aPI_BLL.returnDataTable("Select SUM(ISNULL(aa.TotalExams,0)) as Total,SUM(ISNULL(aa.TotalPresent,0)) as  Present, " +
                                           " SUM(ISNULL(aa.TotalAbsent, 0)) as Absent from( Select e.ExamScheduleId, Count(*) as TotalExams, (Select Count(distinct " +
                                           " ExamScheduleId) from Exams where Registrationid = el.RegistrationId and ExamscheduleId = e.ExamscheduleId) " +
                                           " as TotalPresent, (Select Count(Distinct ExamScheduleId) from ExamSchedules where ExamScheduleId " +
                                           " not in (Select ExamScheduleId  from Exams where Registrationid = el.RegistrationId and ExamscheduleId = e.ExamscheduleId) " +
                                           " and Registrationid = el.RegistrationId and ExamscheduleId = e.ExamscheduleId) as TotalAbsent " +
                                           " from examschedulelns el " +
                                           " inner join ExamSchedules e on e.ExamScheduleId = el.ExamscheduleId " +
                                           " where RegistrationId = '" + RegistrationId.ToString() + "' " +
                                           " group by el.Registrationid,e.ExamscheduleId) as aa ");

            ds.Tables.Add(da);
            ds.Tables.Add(da1);

            st.Append(DataTableToJsonObj(ds));

            if (ds == null)
            {
                ReturnVal = GetReturnValue("209", "No Record Found", st);
            }
            
            if (st.ToString() == "[[],[],[]]" || st.ToString() == "")
            {
                ReturnVal = GetReturnValue("209", "No Record Found", st);
            }

            if (da.Rows.Count > 0 || da1.Rows.Count > 0)
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