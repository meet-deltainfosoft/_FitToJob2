using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.Data.SqlClient;

public partial class API_TestDetails : System.Web.UI.Page
{
    string ExamScheduleId;
    string RegistrationId;
    string TestId;
    string SubjectId;

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

                if (Request.Form["TestId"] != null && Request.Form["TestId"] != "")
                    TestId = Request.Form["TestId"].ToString();
                else
                    TestId = null;

                if (Request.Form["SubjectId"] != null && Request.Form["SubjectId"] != "")
                    SubjectId = Request.Form["SubjectId"].ToString();
                else
                    SubjectId = null;

              

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
        string ReturnVal = "";
        DataTable da = new DataTable();
        StringBuilder st = new StringBuilder();
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Exam_Module";
            sqlCmd.Parameters.AddWithValue("@Action", "API_TestDetails");
            sqlCmd.Parameters.AddWithValue("@RegistrationId", RegistrationId);
            sqlCmd.Parameters.AddWithValue("@ExamScheduleId", ExamScheduleId);
            sqlCmd.Parameters.AddWithValue("@TestId", TestId);
            sqlCmd.Parameters.AddWithValue("@SubjectId", SubjectId);
       

            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            //Da dataSet = new DataSet();
            dataAdapter.Fill(da);
            st.Append(DataTableToJsonObj(da));

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



    public string selectdata_old()
    {

        DataTable da = new DataTable();
        StringBuilder st = new StringBuilder();
        string ReturnVal = "";
        try
        {
            da = _aPI_BLL.returnDataTable(" select e.ExamScheduleId,e.SubId,e.TestId,el.RegistrationId,r.SchoolName, null as SchoolLogoImagePath, " +
                                          " e.No as AssesmentCode, e.TotalMins as Duration, " +
                                          " REPLACE(CONVERT(CHAR(11), ExamDate, 106), ' ', '-') + ' ' + FORMAT(ExamFromTime,'hh:mm:ss tt')  as AssesmentDateTime,  " +
                                          " e.TotalQuestions as TotalQn, (select count(*) from Exams x where x.ExamScheduleId = e.ExamScheduleId and " +
                                          " x.RegistrationId = el.RegistrationId and ISNULL(x.Ans,'') <> '~SKIPPED~' ) as 'AnsweredQn',(select count(*) from Ques q where q.TestId = ts.TestId ) -" +
                                          " (select count(*) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId)  as 'PendingQn', " +
                                          " (select count(*) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId and Ans = '~SKIPPED~' ) as 'SkippedQn', " +
                //" Case when e.PatternId is not null then 'True' else 'False' end as 'IsJeeNeetTest', " +
                                          " Case when isnull(e.AllowReview, 0) = 1 then 'True' else 'False' end as 'IsJeeNeetTest', " +
                                          " case when isnull(e.ShowResult, 0) = 1 then case when conveRT(int, datediff(MINUTE, e.ExamToTime, getdate())) > convert(int, e.MinsforResultShow) then 'true' else 'false' end else 'false' end as 'IsResultAvailable' " +
                                          " ,e.ExamDate" +
                                          " From ExamSchedules e " +
                                          " inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
                                          " inner join TextLists t on t.TextListId = e.StandardTextListId " +
                                          " inner join Subs s on s.SubId = e.SubId " +
                                          " inner join Tests ts on ts.TestId = e.TestId " +
                                          " inner join Registration r on r.RegistrationId = el.RegistrationId " +
                                          " where e.ExamScheduleId = '" + ExamScheduleId.ToString() + "' "+
                                          " and r.RegistrationId = '" + RegistrationId.ToString() + "' " +
                                          " and ts.TestId = '" + TestId.ToString() + "'" +
                                          " Union " +
                                          " select e.ExamScheduleId,e.SubId,e.TestId,el.RegistrationId,r.SchoolName, null as SchoolLogoImagePath, " +
                                          " e.No as AssesmentCode, e.TotalMins as Duration, " +
                                          " REPLACE(CONVERT(CHAR(11), ExamDate, 106), ' ', '-') + ' ' + FORMAT(ExamFromTime,'hh:mm:ss tt')  as AssesmentDateTime,  " +
                                          " e.TotalQuestions as TotalQn, (select count(*) from Exams x where x.ExamScheduleId = e.ExamScheduleId and " +
                                          " x.RegistrationId = el.RegistrationId and ISNULL(x.Ans,'') <> '~SKIPPED~' ) as 'AnsweredQn',(select count(*) from Ques q where q.TestId = ts.TestId ) -" +
                                          " (select count(*) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId)  as 'PendingQn', " +
                                          " (select count(*) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId and Ans = '~SKIPPED~' ) as 'SkippedQn', " +
                //" Case when e.PatternId is not null then 'True' else 'False' end as 'IsJeeNeetTest', " +
                                          " Case when isnull(e.AllowReview, 0) = 1 then 'True' else 'False' end as 'IsJeeNeetTest', " +
                                          " case when isnull(e.ShowResult, 0) = 1 then case when conveRT(int, datediff(MINUTE, e.ExamToTime, getdate())) > convert(int, e.MinsforResultShow) then 'true' else 'false' end else 'false' end as 'IsResultAvailable' " +
                                          ",e.ExamDate" +
                                          " From ExamSchedules e " +
                                          " inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
                                          " inner join TextLists t on t.TextListId = e.StandardTextListId " +
                                          " Left join PatternLns pl on pl.PatternId = e.PatternId" +
                                          " Left join Subs s on s.SubId = pl.SubId " +
                                          " inner join Tests ts on ts.TestId = e.TestId " +
                                          " inner join Registration r on r.RegistrationId = el.RegistrationId " +
                                          " where e.ExamScheduleId = '" + ExamScheduleId.ToString() + "' and r.RegistrationId = '" + RegistrationId.ToString() + "' " +
                                          " and ts.TestId = '" + TestId.ToString() + "'" +
                                          " Order By ExamDate "
                                          );

            st.Append(DataTableToJsonObj(da));

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