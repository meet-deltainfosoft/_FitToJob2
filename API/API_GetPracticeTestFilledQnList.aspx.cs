using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.Configuration;

public partial class API_GetPracticeTestFilledQnList : System.Web.UI.Page
{
    string ChapterId;
    string RegistrationId;
    string ChapterVideoId;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Form["ChapterId"] != null && Request.Form["ChapterId"] != "")
                    ChapterId = Request.Form["ChapterId"].ToString();
                else
                    ChapterId = null;

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

            //da = _aPI_BLL.returnDataTable(" select q.SubId,q.QueId,q.Que as Question ,q.A1,q.A2,q.A3,q.A4  ,s.Name as SubjectName, newid() as 'OrderBy' " +
            //                              " , replace(q.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
            //                              " , replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
            //                              " , replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
            //                              " , replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
            //                              " , replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
            //                              " , q.QueType, q.QueDataType, q.RightMarks, q.WrongMarks, q.NonMarks, q.NoOfFile,'False' as PerQuestionTime " +
            //                              " ,q.Srno as QueNo,e.PerQueMins as QnTime,e.TotalMins, ts.TestId,q.Language,s.Name as Subject,ee.Ans,ee.AnsStatus " +
            //                              " , replace(ee.AnsImage1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage1' " +
            //                              " , replace(ee.AnsImage2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage2' " +
            //                              " , replace(ee.AnsImage3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage3' " +
            //                              " , replace(ee.AnsImage4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage4',q.Ans as 'TrueAns' " +
            //                              " into #tmp From ExamSchedules e " +
            //                              " inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +                
            //                              " inner join Subs s on s.SubId = e.SubId " +
            //                              " inner join Tests ts on ts.TestId = e.TestId " +
            //                              " inner join Ques q on s.SubId = q.SubId and ts.TestId = q.TestId " +
            //                              " left join Exams ee on ee.QueId = q.QueId and ee.RegistrationId = el.RegistrationId " +
            //                              " and ee.ExamScheduleId = e.ExamScheduleId and ee.TestId = ts.TestId " +
            //                              " where e.TestId = '" + TestId.ToString() + "' and el.RegistrationId = '" + RegistrationId.ToString() + "' " +
            //                              " and e.ExamScheduleId = '" + ExamScheduleId.ToString() + "' " +
            //                              " select SubId,QueId,Question ,A1,A2,A3,A4,SubjectName, ROW_NUMBER() OVER(ORDER BY OrderBy) as SrNo, OrderBy " +
            //                              " , ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3,  ImageNameA4 " +
            //                              " , QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, PerQuestionTime, QueNo, QnTime, TotalMins, TestId,Language,Subject,Ans,AnsStatus,AnsImage1,AnsImage2,AnsImage3,AnsImage4,TrueAns " +
            //                              " from #tmp  " +
            //                              " ORDER BY Subject,QueNo  drop table #tmp ");

            da = _aPI_BLL.returnDataTable(" select q.SubId,q.QueBankId,q.Que as Question ,q.A1,q.A2,q.A3,q.A4  ,s.Name as SubjectName, newid() as 'OrderBy' " +
                                          " , replace(q.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                          " , replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                          " , replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                          " , replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                          " , replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                          " , q.QueType, q.QueDataType, q.RightMarks, q.WrongMarks, q.NonMarks, q.NoOfFile,'False' as PerQuestionTime " +
                                          " , q.Srno as QueNo,0 as QnTime, '60' as TotalMins, NULL as TestId,q.Language,s.Name as Subject,qb.Ans,NULL as AnsStatus " +
                                          " , replace(qb.AnsImage1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage1' " +
                                          " , replace(qb.AnsImage2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage2' " +
                                          " , replace(qb.AnsImage3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage3' " +
                                          " , replace(qb.AnsImage4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage4' " +
                                          " , replace(qb.AnsImage5, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage5' " +
                                          " , replace(qb.AnsImage6, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage6' " +
                                          " , replace(qb.AnsImage7, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage7' " +
                                          " , replace(qb.AnsImage8, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage8' " +
                                          " , replace(qb.AnsImage9, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage9' " +
                                          " , replace(qb.AnsImage10, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage10' " +
                                          " , replace(qb.AnsImage11, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage11' " +
                                          " , replace(qb.AnsImage12, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage12' " +
                                          " , replace(qb.AnsImage13, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage13' " +
                                          " , replace(qb.AnsImage14, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage14' " +
                                          " , replace(qb.AnsImage15, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage15' " +
                                          " , replace(qb.AnsImage16, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage16' " +
                                          " , replace(qb.AnsImage17, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage17' " +
                                          " , replace(qb.AnsImage18, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage18' " +
                                          " , replace(qb.AnsImage19, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage19' " +
                                          " , replace(qb.AnsImage20, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage20' " +
                                          " , replace(qb.AnsImage21, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage21' " +
                                          " , replace(qb.AnsImage22, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage22' " +
                                          " , replace(qb.AnsImage23, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage23' " +
                                          " , replace(qb.AnsImage24, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage24' " +
                                          " , replace(qb.AnsImage25, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage25' " +
                                          " , replace(qb.AnsImage26, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage26' " +
                                          " , replace(qb.AnsImage27, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage27' " +
                                          " , replace(qb.AnsImage28, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage28' " +
                                          " , replace(qb.AnsImage29, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage29' " +
                                          " , replace(qb.AnsImage30, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage30' " +
                                          " , replace(qb.AnsImage31, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage31' " +
                                          " , replace(qb.AnsImage32, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage32' " +
                                          " , replace(qb.AnsImage33, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage33' " +
                                          " , replace(qb.AnsImage34, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage34' " +
                                          " , replace(qb.AnsImage35, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage35' " +
                                          " , replace(qb.AnsImage36, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage36' " +
                                          " , replace(qb.AnsImage37, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage37' " +
                                          " , replace(qb.AnsImage38, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage38' " +
                                          " , replace(qb.AnsImage39, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage39' " +
                                          " , replace(qb.AnsImage40, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage40' " +
                                          " , q.Ans as 'TrueAns' " +
                                          " from QueBanks q " +
                                          " Inner Join Subs s on s.SubId = q.SubId  " +
                                          " Inner Join Chapters c on c.ChapterId = q.ChapterId " +
                                          " Inner Join TextLists t on t.TextListId = s.StandardTextListId " +
                                          " left join QueBankAns qb on qb.QueBankId = q.QueBankId and qb.RegistrationId = '" + RegistrationId + "' " +
                                          " where q.ChapterId = '" + ChapterId + "' and q.ChapterVideoId = '" + ChapterVideoId.ToString() + "' order by q.SrNo ");

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