using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.Configuration;

public partial class API_GetSubjectWiseQnListWithAns : System.Web.UI.Page
{
    string TestId;
    string ExamScheduleId;
    string RegistrationId;
    bool IsJeeNeet;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Form["TestId"] != null && Request.Form["TestId"] != "")
                    TestId = Request.Form["TestId"].ToString();
                else
                    TestId = null;

                if (Request.Form["ExamScheduleId"] != null && Request.Form["ExamScheduleId"] != "")
                    ExamScheduleId = Request.Form["ExamScheduleId"].ToString();
                else
                    ExamScheduleId = null;

                if (Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "")
                    RegistrationId = Request.Form["RegistrationId"].ToString();
                else
                    RegistrationId = null;

                if (Request.Form["IsJeeNeet"] != null && Request.Form["IsJeeNeet"] != "")
                    IsJeeNeet = Convert.ToBoolean(Request.Form["IsJeeNeet"]);
                else
                    IsJeeNeet = false;

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
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "'").Replace("\\", "\\/") + "\",");
                    }
                    else if (j == ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "'").Replace("\\", "\\/") + "\"");
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
            if (IsJeeNeet)
            {
                da = _aPI_BLL.returnDataTable(" select r.RegistrationId ,ess.ExamScheduleId, r.ExamNo, r.MobileNo,r.FirstName ,(Select count(*) from ques where SubId = " + " su.subid and TestId = ts.TestId) as TotalQuestions, " +
                                          " SUM(Case When((e.Ans is not null or e.AnsImage1 is not null or e.AnsImage2 " +
                                          " is not null or e.AnsImage3 is not null or e.AnsImage4 is not null) " +
                                          " and  ISNULL(e.Ans, '') <> '~SKIPPED~' and e.AnsStatus is null) then 1 else 0 end) as TotalAnswered , " +
                                          " SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as TotalSkipped, " +
                                          " SUM(Case when e.Ans is null and e.AnsImage1 is null And e.AnsImage2 is null " +
                                          " And e.AnsImage3 is null And e.AnsImage4 is null and e.AnsStatus is null then 1 else 0 end) as 'TotalNotVisited', " +
                                          " SUM(Case when((e.Ans is null and e.AnsImage1 is null And e.AnsImage2 is null " +
                                          " And e.AnsImage3 is null And e.AnsImage4 is null)and e.AnsStatus='~REVIEW~') then 1 else 0 end) as 'TotalMarkedforReview',  " +
                                          " SUM(Case when((e.Ans is not null or e.AnsImage1 is not null or e.AnsImage2 " +
                                          " is not null or e.AnsImage3 is not null or e.AnsImage4 is not null) and " +
                                          " e.AnsStatus = '~REVIEW~') then 1 else 0 end) as 'TotalAnsandMarkedForReview', " +
                                          " SUM(case when e.QueType = 'MCQ' then(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then" +
                                          " q.RightMarks " +
                                          " when e.Ans <> q.Ans then ISNULL(q.WrongMarks, 0) else 0  end)  " +
                                          " else (Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <>   q.Ans        then    " +
                                          " isnull(q.WrongMarks, 0) else 0 end) end) as TotalObtainedMarks  , " +
                                          " SUM(Case when q.QueType = 'MCQ' then ISNULL(q.RightMarks, 0) else  ISNULL(q.RightMarks, 0) end) as TotalMarks " +
                                          " ,SUM(Case when e.Ans <> '~SKIPPED~' and ([dbo].[GetUniqueAnsInNumber](e.Ans) = q.Ans ) then 1 else 0 end) as NoOfTrueAnswers " +
                                          " ,SUM(Case when e.Ans <> '~SKIPPED~' and ([dbo].[GetUniqueAnsInNumber](e.Ans) <> q.Ans ) then 1 else 0 end) as NoOfWrongAnswers " +
                                          " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions" +
                                          " ,tst.[text] as Standard,su.Name as Subject,ts.TestId,ts.TestName" +
                                          " , replace(ep.AnsKeyFilePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsKeyPDF' " +
                                          " , case when isnull(ep.AnsKeyFilePath, '') = '' then '0' else '1' end as 'ViewAnsKey' " +
                                          " , case when isnull(ep.IsResultPublished, 0) = 1 then '1' else '0' end as 'ViewCheckedSheet' " +
                                          " , 'Good' as 'Smiley' " +
                                          " from Registration r" +
                                          " inner join ExamScheduleLns es on es.RegistrationId = r.RegistrationId" +
                                          " inner join ExamSchedules ess on ess.ExamScheduleId = es.ExamScheduleId" +
                    //" inner join PatternLns pl on pl.PatternId = ess.PatternId " +
                                          " inner join Subs su on su.SubId = ess.SubId" +
                                          " left join Exams e on e.RegistrationId = r.RegistrationId and e.ExamScheduleId = ess.ExamScheduleId and e.SubId = ess.SubId" +
                                          " left join (select max(StartDt) as StartDt, max(EndDt) as EndDt, RegistrationId, ExamScheduleId " +
                                          " from ExamStartStopTimes group by RegistrationId, ExamScheduleId) s on s.RegistrationId = r.RegistrationId" +
                                          " and s.ExamScheduleId = ess.ExamScheduleId" +
                                          " left join Ques q on q.QueId = e.QueId " +
                                          " left join Tests ts on ts.TestId = ess.TestId" +
                                          " left join Textlists tst on tst.textListId = r.standardId " +
                                          " left join ExamResultPublish ep on ep.ExamScheduleId = ess.ExamScheduleId " +
                                          " where r.RegistrationId = '" + RegistrationId.ToString() + "'" +
                                          " and ts.TestId = '" + TestId.ToString() + "'" +
                                          " and ess.ExamScheduleId = '" + ExamScheduleId.ToString() + "'" +
                                          " group by r.RegistrationId,ess.ExamScheduleId,r.ExamNo, r.MobileNo,r.FirstName,tst.[text],su.Name,ts.TestId,ts.TestName,su.SUbId " +
                                          " , replace(ep.AnsKeyFilePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                                          " , case when isnull(ep.AnsKeyFilePath, '') = '' then '0' else '1' end, case when isnull(ep.IsResultPublished, 0) = 1 then '1' else '0' end " +
                                          " order by  TotalMarks desc  ");

            }
            else
            {
                da = _aPI_BLL.returnDataTable(" select r.RegistrationId ,ess.ExamScheduleId, r.ExamNo, r.MobileNo,r.FirstName ,(Select count(*) from ques where SubId = " + " su.subid and TestId = ts.TestId) as TotalQuestions, " +
                                          " SUM(Case When((e.Ans is not null or e.AnsImage1 is not null or e.AnsImage2 " +
                                          " is not null or e.AnsImage3 is not null or e.AnsImage4 is not null) " +
                                          " and  ISNULL(e.Ans, '') <> '~SKIPPED~' ) then 1 else 0 end) as TotalAnswered , " +
                                          " SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as TotalSkipped, " +
                                          " SUM(Case when e.Ans is null and e.AnsImage1 is null And e.AnsImage2 is null " +
                                          " And e.AnsImage3 is null And e.AnsImage4 is null then 1 else 0 end) as 'TotalNotVisited', " +
                                          " SUM(Case when((e.Ans is null and e.AnsImage1 is null And e.AnsImage2 is null " +
                                          " And e.AnsImage3 is null And e.AnsImage4 is null)and e.AnsStatus='~REVIEW~') then 1 else 0 end) as 'TotalMarkedforReview',  " +
                                          " SUM(Case when((e.Ans is not null or e.AnsImage1 is not null or e.AnsImage2 " +
                                          " is not null or e.AnsImage3 is not null or e.AnsImage4 is not null) and " +
                                          " e.AnsStatus = '~REVIEW~') then 1 else 0 end) as 'TotalAnsandMarkedForReview', " +
                                          "SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when[dbo].NumericOrStringCompare(q.QueDataType, q.Ans, ([dbo].[GetUniqueAnsInNumber](e.Ans))) = 1  then  " +
                                          " case when q.AnsSelection = 'SINGLE' then q.RightMarks when q.AnsSelection = 'MULTIPLE' then[dbo].[GetTrueCount] " +
                                          " (q.Ans, e.Ans) * q.RightMarks else q.RightMarks end when ([dbo].[GetUniqueAnsInNumber](e.Ans)) <> q.Ans then case when q.AnsSelection = 'SINGLE' then " +
                                          " case when isnull(ess.NegativeMarks, 0) = 1 then(q.WrongMarks) else 0 end " +
                                          " when q.AnsSelection = 'MULTIPLE' then[dbo].[GetFalseCount](q.Ans, e.Ans) * " +
                                          " (q.WrongMarks)Else(q.WrongMarks) End end) as TotalObtainedMarks, " +
                                          " SUM(ISNULL(q.RightMarks, 0)) as TotalMarks  " +
                                          " ,SUM(Case when e.Ans <> '~SKIPPED~' and ([dbo].[GetUniqueAnsInNumber](e.Ans) = q.Ans ) then 1 else 0 end) as NoOfTrueAnswers " +
                                          " ,SUM(Case when e.Ans <> '~SKIPPED~' and ([dbo].[GetUniqueAnsInNumber](e.Ans) <> q.Ans ) then 1 else 0 end) as NoOfWrongAnswers " +
                                          " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions" +
                                          " ,tst.[text] as Standard,su.Name as Subject,ts.TestId,ts.TestName" +
                                          " , replace(ep.AnsKeyFilePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsKeyPDF' " +
                                          " , case when isnull(ep.AnsKeyFilePath, '') = '' then '0' else '1' end as 'ViewAnsKey' " +
                                          " , case when isnull(ep.IsResultPublished, 0) = 1 then '1' else '0' end as 'ViewCheckedSheet' " +
                                          " , 'Good' as 'Smiley' " +
                                          " from Registration r" +
                                          " inner join ExamScheduleLns es on es.RegistrationId = r.RegistrationId" +
                                          " inner join ExamSchedules ess on ess.ExamScheduleId = es.ExamScheduleId" +
                                          " inner join Subs su on su.SubId = ess.SubId" +
                                          " left join Exams e on e.RegistrationId = r.RegistrationId and e.ExamScheduleId = ess.ExamScheduleId and e.SubId = ess.SubId" +
                                          " left join (select max(StartDt) as StartDt, max(EndDt) as EndDt, RegistrationId, ExamScheduleId " +
                                          " from ExamStartStopTimes group by RegistrationId, ExamScheduleId) s on s.RegistrationId = r.RegistrationId" +
                                          " and s.ExamScheduleId = ess.ExamScheduleId" +
                                          " left join Ques q on q.QueId = e.QueId " +
                                          " left join Tests ts on ts.TestId = ess.TestId" +
                                          " left join Textlists tst on tst.textListId = r.standardId " +
                                          " left join ExamResultPublish ep on ep.ExamScheduleId = ess.ExamScheduleId " +
                                          " where r.RegistrationId = '" + RegistrationId.ToString() + "'" +
                                          " and ts.TestId = '" + TestId.ToString() + "'" +
                                          " and ess.ExamScheduleId = '" + ExamScheduleId.ToString() + "'" +
                                          " group by r.RegistrationId,ess.ExamScheduleId,r.ExamNo, r.MobileNo,r.FirstName,tst.[text],su.Name,ts.TestId,ts.TestName,su.SUbId " +
                                          " , replace(ep.AnsKeyFilePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                                          " , case when isnull(ep.AnsKeyFilePath, '') = '' then '0' else '1' end, case when isnull(ep.IsResultPublished, 0) = 1 then '1' else '0' end " +
                                          " order by  TotalMarks desc  ");

            }

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
}