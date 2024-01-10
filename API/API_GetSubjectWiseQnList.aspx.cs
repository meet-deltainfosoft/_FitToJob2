using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.Configuration;

public partial class API_GetSubjectWiseQnList : System.Web.UI.Page
{
    string TestId;
    string ExamScheduleId;
    string RegistrationId;

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

            da = _aPI_BLL.returnDataTable(" select q.SubId,q.QueId,q.Que as Question ,q.A1,q.A2,q.A3,q.A4  ,s.Name as SubjectName, newid() as 'OrderBy' " +
                                          " , replace(q.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                          " , replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                          " , replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                          " , replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                          " , replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                          " , q.QueType, q.QueDataType, q.RightMarks, q.WrongMarks, q.NonMarks, q.NoOfFile,'False' as PerQuestionTime " +
                                          " ,q.Srno as QueNo,e.PerQueMins as QnTime,e.TotalMins, ts.TestId,q.Language,s.Name as Subject,ee.Ans,ee.AnsStatus " +
                                          " , replace(ee.AnsImage1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage1' " +
                                          " , replace(ee.AnsImage2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage2' " +
                                          " , replace(ee.AnsImage3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage3' " +
                                          " , replace(ee.AnsImage4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage4',q.Ans as 'TrueAns' " +
                                          " into #tmp From ExamSchedules e " +
                                          " inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
                                          //" inner join PatternLns pl on pl.PatternId = e.PatternId " +
                                          " inner join Subs s on s.SubId = e.SubId " +
                                          " inner join Tests ts on ts.TestId = e.TestId " +
                                          " inner join Ques q on s.SubId = q.SubId and ts.TestId = q.TestId " +
                                          " left join Exams ee on ee.QueId = q.QueId and ee.RegistrationId = el.RegistrationId "+
                                          " and ee.ExamScheduleId = e.ExamScheduleId and ee.TestId = ts.TestId "+
                                          " where e.TestId = '" + TestId.ToString() + "' and el.RegistrationId = '" + RegistrationId.ToString() + "' " +
                                          " and e.ExamScheduleId = '" + ExamScheduleId.ToString() + "' "+
                                          " select SubId,QueId,Question ,A1,A2,A3,A4,SubjectName, ROW_NUMBER() OVER(ORDER BY OrderBy) as SrNo, OrderBy " +
                                          " , ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3,  ImageNameA4 " +
                                          " , QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, PerQuestionTime, QueNo, QnTime, TotalMins, TestId,Language,Subject,Ans,AnsStatus,AnsImage1,AnsImage2,AnsImage3,AnsImage4,TrueAns " +
                                          " from #tmp  " +
                                          " ORDER BY Subject,QueNo  drop table #tmp ");

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