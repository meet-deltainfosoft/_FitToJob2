using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;

public partial class API_Timetable : System.Web.UI.Page
{
    DateTime? date;
    string RegistrationId;
    //bool? IsDefaultTest;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Form["date"] != null && Request.Form["date"] != "")
                    date = Convert.ToDateTime(Request.Form["date"].ToString());
                else
                    date = null;

                if (Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "")
                    RegistrationId = Request.Form["RegistrationId"].ToString();
                else
                    RegistrationId = null;

                //if (Request.Form["IsDefaultTest"] != null && Request.Form["IsDefaultTest"] != "")
                //    IsDefaultTest = Convert.ToBoolean(Request.Form["IsDefaultTest"].ToString());
                //else
                //    IsDefaultTest = null;

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
        DataTable da2 = new DataTable();

        StringBuilder st = new StringBuilder();
        string ReturnVal = "";
        try
        {
            
                da = _aPI_BLL.returnDataTable(" select *,FORMAT(FromTime,'hh:mm tt') as FromTime,FORMAT(ToTime,'hh:mm tt') as ToTime  From LiveClasses a where " +
                                              " convert(date, FromTime) = convert(date,'" + Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + "') and  " +
                                              " SubId in (select SubId from Subs where StandardTextListId in " +
                                              " (select StandardId from Registration where RegistrationId = '" + RegistrationId + "')) order by  a.FromTime ");

                da1 = _aPI_BLL.returnDataTable(" Select t.TestId,t.TestName,t.SubId,e.ExamDate,e.ExamscheduleId  " +
                                                 " , ISNULL(e.IsSelfieAllowed,0) as 'IsSelfieAllowed', " +
                                                 " ISNULL(e.IsSignatureAllowed,0) as 'IsSignatureAllowed' , " +
                                                 " FORMAT(e.ExamFromTime, 'hh:mm tt') as ExamFromTime,FORMAT(e.ExamToTime, 'hh:mm tt') as ExamToTime " +
                                                 " ,ss.Name as SubName,q.LevelofQue As 'Test Level' ,e.TotalQuestions  As 'TotalQuestions' " +
                                                 " ,SUM(Case when ex.Ans = '~SKIPPED~' then qu.NonMarks when ex.Ans = qu.Ans then qu.RightMarks when ex.Ans <> qu.Ans then case when isnull(e.NegativeMarks, 0) = 1 then (qu.WrongMarks) else 0 end end) as TotalMarks  " +
                                                 " , ES.IsFinalSubmit from Tests t " +
                                                 " Inner join ExamSchedules e on e.TestId = t.TestId and e.SubId = t.SubId " +
                                                 " inner join Subs ss on t.SubId = ss.SubId " +
                                                 " left join QueBanks q on q.SubId = ss.SubId " +
                                                 " left join Exams ex  on  ex.ExamScheduleId = e.ExamScheduleId " +
                                                 " left Join Examstartstoptimes ES on ES.ExamScheduleId = e.ExamScheduleId " +
                                                 " left join Ques qu on qu.QueId = ex.QueId " +
                                                 " Where t.SubId in (select SubId from Subs where StandardTextListId in " +
                                                 " (select J.DepartmentId As 'StandardId' from RegistrationJobProfileLns R inner join Jobofferings J on J.JobOfferingId = R.JobOfferingId where R.RegistrationId = '" + RegistrationId + "')) " +
                                                 " and e.ExamDate = '" + Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + "' " +
                                                 " Group By t.TestId,t.TestName,t.SubId,e.ExamDate,e.ExamscheduleId   , ISNULL(e.IsSelfieAllowed,0) ,ISNULL(e.IsSignatureAllowed,0) ,e.ExamFromTime,e.ExamToTime ,ss.Name ,q.LevelofQue ,e.TotalQuestions,ES.IsFinalSubmit  " +
                    //" and e.ExamScheduleId not in (Select ExamScheduleId from Examstartstoptimes " +
                    //" where RegistrationId = '" + RegistrationId.ToString() + "' and ISNULL(IsFinalSubmit, 0) = 1)  " +
                                                 " Union " +
                                                 " Select t.TestId,t.TestName,t.PatternId,e.ExamDate,e.ExamscheduleId  " +
                                                 " , ISNULL(e.IsSelfieAllowed,0) as 'IsSelfieAllowed', " +
                                                 " ISNULL(e.IsSignatureAllowed,0) as 'IsSignatureAllowed' , " +
                                                 " FORMAT(e.ExamFromTime, 'hh:mm tt') as ExamFromTime,FORMAT(e.ExamToTime, 'hh:mm tt') as ExamToTime " +
                                                 " ,ss.Name as SubName ,q.LevelofQue As 'Test Level',e.TotalQuestions  As 'TotalQuestions' " +
                                                 " ,SUM(Case when ex.Ans = '~SKIPPED~' then qu.NonMarks when ex.Ans = qu.Ans then qu.RightMarks when ex.Ans <> qu.Ans then case when isnull(e.NegativeMarks, 0) = 1 then (qu.WrongMarks) else 0 end end) as TotalMarks  " +
                                                 " ,ES.IsFinalSubmit from Tests t " +
                                                 " Inner join ExamSchedules e on e.TestId = t.TestId and e.PatternId = t.PatternId " +
                                                 " inner join Subs ss on t.SubId = ss.SubId " +
                                                 " left join QueBanks q on q.SubId = ss.SubId " +
                                                 " left join Exams ex  on  ex.ExamScheduleId = e.ExamScheduleId " +
                                                 " left Join Examstartstoptimes ES on ES.ExamScheduleId = e.ExamScheduleId " +
                                                 " left join Ques qu on qu.QueId = ex.QueId  " +
                                                 " and e.ExamDate = '" + Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + "' " +
                                                 " and isnull(IsDefaultTest, 0) = 0 " +
                                                 " and isnull(ES.IsFinalSubmit, 0) = 0 " +
                                                 " Group By t.TestId,t.TestName,t.PatternId,e.ExamDate,e.ExamscheduleId   , ISNULL(e.IsSelfieAllowed,0) ,ISNULL(e.IsSignatureAllowed,0) ,e.ExamFromTime,e.ExamToTime ,ss.Name ,q.LevelofQue ,e.TotalQuestions,ES.IsFinalSubmit   " +
                                                 " union " +
                                                 " Select t.TestId,t.TestName,t.SubId,e.ExamDate,e.ExamscheduleId  " +
                                                 " , ISNULL(e.IsSelfieAllowed,0) as 'IsSelfieAllowed', " +
                                                 " ISNULL(e.IsSignatureAllowed,0) as 'IsSignatureAllowed' , " +
                                                 " FORMAT(e.ExamFromTime, 'hh:mm tt') as ExamFromTime,FORMAT(e.ExamToTime, 'hh:mm tt') as ExamToTime " +
                                                 " ,ss.Name as SubName,q.LevelofQue As 'Test Level',e.TotalQuestions  As 'TotalQuestions' " +
                                                 " ,SUM(Case when ex.Ans = '~SKIPPED~' then qu.NonMarks when ex.Ans = qu.Ans then qu.RightMarks when ex.Ans <> qu.Ans then case when isnull(e.NegativeMarks, 0) = 1 then (qu.WrongMarks) else 0 end end) as TotalMarks " +
                                                 " ,ES.IsFinalSubmit from Tests t " +
                                                 " Inner join ExamSchedules e on e.TestId = t.TestId and e.SubId = t.SubId " +
                                                 " inner join Subs ss on t.SubId = ss.SubId " +
                                                 " left join QueBanks q on q.SubId = ss.SubId " +
                                                 " left join Exams ex  on  ex.ExamScheduleId = e.ExamScheduleId " +
                                                 " left Join Examstartstoptimes ES on ES.ExamScheduleId = e.ExamScheduleId " +
                                                 " left join Ques qu on qu.QueId = ex.QueId  " +
                                                 " Where t.SubId in (select SubId from Subs where StandardTextListId in " +
                                                 " (select J.DepartmentId As 'StandardId' from RegistrationJobProfileLns V inner join Jobofferings J on J.JobOfferingId = V.JobOfferingId  where V.RegistrationId = '" + RegistrationId + "')) " +
                                                 " and isnull(IsDefaultTest, 0) = 1 " +
                                                 " and isnull(ES.IsFinalSubmit, 0) = 1 " +
                                                 " Group By t.TestId,t.TestName,t.SubId,e.ExamDate,e.ExamscheduleId   , ISNULL(e.IsSelfieAllowed,0) ,ISNULL(e.IsSignatureAllowed,0) ,e.ExamFromTime,e.ExamToTime ,ss.Name ,q.LevelofQue ,e.TotalQuestions,ES.IsFinalSubmit  " +
                                                 " order by TestName ");
                //da1 = _aPI_BLL.returnDataTable(" Select t.TestId,t.TestName,t.SubId,e.ExamDate,e.ExamscheduleId  " +
                //                             " , ISNULL(e.IsSelfieAllowed,0) as 'IsSelfieAllowed', " +
                //                             " ISNULL(e.IsSignatureAllowed,0) as 'IsSignatureAllowed' , " +
                //                             " FORMAT(e.ExamFromTime, 'hh:mm tt') as ExamFromTime,FORMAT(e.ExamToTime, 'hh:mm tt') as ExamToTime " +
                //                             " ,ss.Name as SubName,q.LevelofQue As 'Test Level' " +
                //                             " from Tests t " +
                //                             " Inner join ExamSchedules e on e.TestId = t.TestId and e.SubId = t.SubId " +
                //                             " inner join Subs ss on t.SubId = ss.SubId " +
                //                             " left join QueBanks q on q.SubId = ss.SubId " +
                //                             " Where t.SubId in (select SubId from Subs where StandardTextListId in " +
                //                             " (select StandardId from Registration where RegistrationId = '" + RegistrationId + "')) " +
                //                             " and e.ExamDate = '" + Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + "' " +
                //    //" and e.ExamScheduleId not in (Select ExamScheduleId from Examstartstoptimes " +
                //    //" where RegistrationId = '" + RegistrationId.ToString() + "' and ISNULL(IsFinalSubmit, 0) = 1)  " +
                //                             " Union " +
                //                             " Select t.TestId,t.TestName,t.PatternId,e.ExamDate,e.ExamscheduleId  " +
                //                             " , ISNULL(e.IsSelfieAllowed,0) as 'IsSelfieAllowed', " +
                //                             " ISNULL(e.IsSignatureAllowed,0) as 'IsSignatureAllowed' , " +
                //                             " FORMAT(e.ExamFromTime, 'hh:mm tt') as ExamFromTime,FORMAT(e.ExamToTime, 'hh:mm tt') as ExamToTime " +
                //                             " ,ss.Name as SubName ,q.LevelofQue As 'Test Level' " +
                //                             " from Tests t " +
                //                             " Inner join ExamSchedules e on e.TestId = t.TestId and e.PatternId = t.PatternId " +
                //                             " inner join Subs ss on t.SubId = ss.SubId " +
                //                             " left join QueBanks q on q.SubId = ss.SubId " +
                //                             " and e.ExamDate = '" + Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + "' " +
                //                             " and isnull(IsDefaultTest, 0) = 0 " +
                //                             " union " +
                //                             " Select t.TestId,t.TestName,t.SubId,e.ExamDate,e.ExamscheduleId  " +
                //                             " , ISNULL(e.IsSelfieAllowed,0) as 'IsSelfieAllowed', " +
                //                             " ISNULL(e.IsSignatureAllowed,0) as 'IsSignatureAllowed' , " +
                //                             " FORMAT(e.ExamFromTime, 'hh:mm tt') as ExamFromTime,FORMAT(e.ExamToTime, 'hh:mm tt') as ExamToTime " +
                //                             " ,ss.Name as SubName,q.LevelofQue As 'Test Level' " +
                //                             " from Tests t " +
                //                             " Inner join ExamSchedules e on e.TestId = t.TestId and e.SubId = t.SubId " +
                //                             " inner join Subs ss on t.SubId = ss.SubId " +
                //                             " left join QueBanks q on q.SubId = ss.SubId " +
                //                             " Where t.SubId in (select SubId from Subs where StandardTextListId in " +
                //                             " (select StandardId from Registration where RegistrationId = '" + RegistrationId + "')) " +
                //                             " and isnull(IsDefaultTest, 0) = 1 " +
                //                             " order by TestName ");

                //da1 = _aPI_BLL.returnDataTable(" Select t.TestId,t.TestName,t.SubId,e.ExamDate,e.ExamscheduleId  " +
                //                              " , ISNULL(e.IsSelfieAllowed,0) as 'IsSelfieAllowed', " +
                //                              " ISNULL(e.IsSignatureAllowed,0) as 'IsSignatureAllowed' , " +
                //                              " FORMAT(e.ExamFromTime, 'hh:mm tt') as ExamFromTime,FORMAT(e.ExamToTime, 'hh:mm tt') as ExamToTime " +
                //                              " ,ss.Name as SubName " +
                //                              " from Tests t " +
                //                              " Inner join ExamSchedules e on e.TestId = t.TestId and e.SubId = t.SubId " +
                //                              " inner join Subs ss on t.SubId = ss.SubId " +
                //                              " Where t.SubId in (select SubId from Subs where StandardTextListId in " +
                //                              " (select StandardId from Registration where RegistrationId = '" + RegistrationId + "')) " +
                //                              " and e.ExamDate = '" + Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + "' " +
                //    //" and e.ExamScheduleId not in (Select ExamScheduleId from Examstartstoptimes " +
                //    //" where RegistrationId = '" + RegistrationId.ToString() + "' and ISNULL(IsFinalSubmit, 0) = 1)  " +
                //                              " Union " +
                //                              " Select t.TestId,t.TestName,t.PatternId,e.ExamDate,e.ExamscheduleId  " +
                //                              " , ISNULL(e.IsSelfieAllowed,0) as 'IsSelfieAllowed', " +
                //                              " ISNULL(e.IsSignatureAllowed,0) as 'IsSignatureAllowed' , " +
                //                              " FORMAT(e.ExamFromTime, 'hh:mm tt') as ExamFromTime,FORMAT(e.ExamToTime, 'hh:mm tt') as ExamToTime " +
                //                              " ,ss.Name as SubName " +
                //                              " from Tests t " +
                //                              " Inner join ExamSchedules e on e.TestId = t.TestId and e.PatternId = t.PatternId " +
                //                              " inner join Subs ss on t.SubId = ss.SubId " +
                //                              " and e.ExamDate = '" + Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + "' " +
                //                              " order by TestName ");

                da2 = _aPI_BLL.returnDataTable(" Select h.HomeWorkId,s.SubId,c.ChapterId,h.Dt,s.Name as 'Subject',c.ChapterName " +
                                               " from HomeWorks h " +
                                               " inner join subs s on s.SubId = h.SubId " +
                                               " inner join Chapters c on c.SubId = s.SubId and h.ChapterId = c.ChapterId " +
                                               " Where s.SubId in (select SubId from Subs where StandardTextListId in  " +
                                               " (select StandardId from Registration where RegistrationId = '" + RegistrationId + "')) " +
                                               " and h.Dt = '" + Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + "'  order by h.Dt ");
            
           

            //da2 = _aPI_BLL.returnDataTable(" Select null ");

            //ds.Tables.Add(da);
            //ds.Tables.Add(da1);
            //ds.Tables.Add(da2);

            if (da.Rows.Count > 0)
                ds.Tables.Add(da);

            if (da1.Rows.Count > 0)
                ds.Tables.Add(da1);

            if (da2.Rows.Count > 0)
                ds.Tables.Add(da2);

            st.Append(DataTableToJsonObj(ds));

            if (ds == null)
            {
                ReturnVal = GetReturnValue("209", "No Record Found", st);
            }
            
            if (st.ToString() == "[]" || st.ToString() == "")
            {
                ReturnVal = GetReturnValue("209", "No Record Found", st);
            }

            if (da.Rows.Count > 0 || da1.Rows.Count > 0 || da2.Rows.Count > 0)
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
            //JsonString.Append("[");
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
            //JsonString.Append("]");

            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }
}