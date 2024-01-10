using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.Configuration;

public partial class API_GetQuizQnList : System.Web.UI.Page
{
    string RegistrationId;
    //string ChapterName;
    string PeriodNo;
    string ChapterId;
    string ChapterVideoId;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //if (Request.Form["ChapterName"] != null && Request.Form["ChapterName"] != "")
                //    ChapterName = Request.Form["ChapterName"].ToString();
                //else
                //    ChapterName = null;

                if (Request.Form["ChapterId"] != null && Request.Form["ChapterId"] != "")
                    ChapterId = Request.Form["ChapterId"].ToString();
                else
                    ChapterId = null;

                if (Request.Form["PeriodNo"] != null && Request.Form["PeriodNo"] != "")
                    PeriodNo = Request.Form["PeriodNo"].ToString();
                else
                    PeriodNo = null;

                if (Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "")
                    RegistrationId = Request.Form["RegistrationId"].ToString();
                else
                    RegistrationId = null;

                if (Request.Form["ChapterVideoId"] != null && Request.Form["ChapterVideoId"] != "")
                    ChapterVideoId = Request.Form["ChapterVideoId"].ToString();
                else
                    ChapterVideoId = null;

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
                    //if (j < ds.Tables[0].Columns.Count - 1)
                    //{
                    //    if (ds.Tables[0].Rows[i][j].ToString().Contains("["))
                    //    {
                    //        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"'" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "'").Replace("\\", "\\/") + "'\",");
                    //    }
                    //    else
                    //    {
                    //        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "'").Replace("\\", "\\/") + "\",");
                    //    }

                    //    //JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "'").Replace("\\", "\\/") + "\",");
                    //}
                    //else if (j == ds.Tables[0].Columns.Count - 1)
                    //{
                    //    if (ds.Tables[0].Rows[i][j].ToString().Contains("["))
                    //    {
                    //        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"'" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "'").Replace("\\", "\\/") + "'\"");
                    //    }
                    //    else
                    //    {
                    //        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "'").Replace("\\", "\\/") + "\"");
                    //    }

                    //    //JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "'").Replace("\\", "\\/") + "\"");
                    //}

                    if (j < ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "'").Replace("\\", "\\/").Replace("[", "U+005B").Replace("]", "U+005D") + "\",");
                    }
                    else if (j == ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "'").Replace("\\", "\\/").Replace("[", "U+005B").Replace("]", "U+005D") + "\"");
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
            //da = _aPI_BLL.returnDataTable(" select q.SubId,q.QueId,q.Srno as QueNo,e.PerQueMins as QnTime,e.TotalMins,q.Que as Question ,q.A1,q.A2,q.A3,q.A4  ," +
            //                              " s.Name as Subject, newid() as 'OrderBy' , q.ImageNameQus as 'ImageNameQus', q.ImageNameA1 as 'ImageNameA1' " +
            //                              " , q.ImageNameA2 as 'ImageNameA2', q.ImageNameA3 as 'ImageNameA3', q.ImageNameA4 as 'ImageNameA4' " +
            //                              " , InsertedOn = (select top 1 InsertedOn from Exams where RegistrationId = el.RegistrationId " +
            //                              " and ExamScheduleId =  e.ExamScheduleId order by insertedon asc)  , " +
            //                              " StartDt = (select top 1 StartDt from ExamStartStopTimes where RegistrationId = el.RegistrationId " +
            //                              " and ExamScheduleId = e.ExamScheduleId and EndDt is null order by InsertedOn desc) " +
            //                              " ,q.QueType,q.Hashtag,q.QueDataType,ts.TestId,e.ExamScheduleId " +
            //                              " into #tmp From ExamSchedules e  " +
            //                              " inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
            //                              " inner join TextLists t on t.TextListId = e.StandardTextListId " +
            //                              " inner join Subs s on s.SubId = e.SubId " +
            //                              " inner join Tests ts on ts.TestId = e.TestId " +
            //                              " inner join Ques q on s.SubId = q.SubId and ts.TestId = q.TestId " +
            //                              " where e.SubId = '" + SubId.ToString() + "' and e.TestId = '" + TestId.ToString() + "' " +
            //                              " and el.RegistrationId = '" + RegistrationId.ToString() + "' " +
            //                              " and e.ExamScheduleId = '" + ExamScheduleId.ToString() + "' and q.QueId not in  " +
            //                              " (select QueId from Exams where RegistrationId = '" + RegistrationId.ToString() + "' " +
            //                              " and ExamScheduleId = '" + ExamScheduleId.ToString() + "') " +
            //                              " select SubId, QueId, QueNo, QnTime, TotalMins, Question, A1, A2, A3, A4, Subject, " +
            //                              " ROW_NUMBER() OVER(ORDER BY OrderBy) as SrNo, OrderBy " +
            //                              " ,ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3,  ImageNameA4,Insertedon,StartDt,DATEDIFF(Minute, StartDt, InsertedOn) as DiffTime " +
            //                              " ,QueType,Hashtag,QueDataType,TestId,ExamScheduleId " +
            //                              " from #tmp  " +
            //                              " ORDER BY ROW_NUMBER() OVER(ORDER BY OrderBy)" +
            //                              " Drop table #tmp ;");

            da = _aPI_BLL.returnDataTable(" select q.SubId,q.QueBankId,q.Que as Question ,q.A1,q.A2,q.A3,q.A4  ,s.Name as Subject, newid() as 'OrderBy' " +
                                          " , replace(q.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                          " , replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                          " , replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                          " , replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                          " , replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                          " , q.QueType, q.QueDataType, q.RightMarks, q.WrongMarks, q.NonMarks, q.NoOfFile, isnull(1, 0) as PerQuestionTime " +
                                          " ,q.Srno as QueNo, 1 as QnTime, q.Ans, q.ChapterId, q.PeriodNo " +
                                          " into #tmp From QueBanks q " +
                                          " inner join Subs s on s.SubId = q.SubId " +
                                          " inner join TextLists t on t.TextListId = s.StandardTextListId " +
                                          " inner join Chapters c on c.ChapterId = q.ChapterId " +
                                          " inner join Registration r on r.StandardId = s.StandardTextListId " +
                                          " where " +
                                          " q.QueBankId not in (select QueBankId from QueBankAns where RegistrationId = '" + RegistrationId + "' "+
                                          " and ChapterId = '" + ChapterId.ToString() + "' and ChapterVideoId = '" + ChapterVideoId.ToString() + "') " +
                                          " and c.ChapterId = '" + ChapterId.ToString() + "' and convert(decimal, c.PeriodNo) = '" + PeriodNo + "' " +
                                          " and q.ChapterVideoId = '" + ChapterVideoId.ToString() + "' " +
                                          " and r.RegistrationId  = '" + RegistrationId + "' " + //and q.QueType = 'MCQ'
                                          " select SubId,QueBankId,Question ,A1,A2,A3,A4,Subject, ROW_NUMBER() OVER(ORDER BY OrderBy) as SrNo, OrderBy " +
                                          " , ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3,  ImageNameA4 " +
                                          " , QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, PerQuestionTime, QueNo, QnTime, Ans, ChapterId, PeriodNo " +
                                          " from #tmp  " +
                                          " ORDER BY ROW_NUMBER() OVER(ORDER BY OrderBy)  drop table #tmp ");

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
            //return ReturnVal.Replace("\\", "").Replace("\"[", "[").Replace("u0027", "").Replace("\"]", "]");
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