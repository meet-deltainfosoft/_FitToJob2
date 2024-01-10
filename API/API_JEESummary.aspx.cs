using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.Configuration;

public partial class API_JEESummary : System.Web.UI.Page
{
    string SubId;
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
                if (Request.Form["SubId"] != null && Request.Form["SubId"] != "")
                    SubId = Request.Form["SubId"].ToString();
                else
                    SubId = null;

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
        string where = "";
        try
        {
            if (SubId != null && SubId != "")
                where = " and s.SubId  = '" + SubId.ToString() + "'";
            if (IsJeeNeet)
            {
                da = _aPI_BLL.returnDataTable(" Select q.QueId, q.Que as Question, ts.TestId, q.Language, s.Name as Subject, " +
                                            " Case When ((ee.Ans is not null or ee.AnsImage1 is not null or ee.AnsImage2 is not null or ee.AnsImage3 is not null or ee.AnsImage4 is not null) and  ISNULL(ee.Ans,'') <> '~SKIPPED~' and ee.AnsStatus is null) then '1' " +
                                            " when ee.Ans = '~SKIPPED~' then '2' when ee.Ans is null and ee.AnsImage1 is null And ee.AnsImage2 is null " +
                                            " And ee.AnsImage3 is null And ee.AnsImage4 is null and ee.AnsSTatus is null then '3'    " +
                                            " when((ee.Ans is null and ee.AnsImage1 is null And ee.AnsImage2 is null And ee.AnsImage3 is null " +
                                            " And ee.AnsImage4 is null)and  ee.AnsStatus = '~REVIEW~') then '4' " +
                                            " when((ee.Ans is not null or ee.AnsImage1 is not null or ee.AnsImage2 is not null or ee.AnsImage3 is not null " +
                                            " or ee.AnsImage4 is not null) and ee.AnsStatus = '~REVIEW~') then '5'   end as 'QueType',q.Srno as QueNo " +
                                            " From ExamSchedules e " +
                                            " inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
                    //" inner join PatternLns pl on pl.PatternId = e.PatternId " +
                                            " inner join Subs s on s.SubId = e.SubId " +
                                            " inner join Tests ts on ts.TestId = e.TestId " +
                                            " inner join Ques q on s.SubId = q.SubId and ts.TestId = q.TestId " +
                                            " left join Exams ee on ee.QueId = q.QueId and q.TestId = ee.TestId " +
                                            " and e.ExamScheduleId = ee.ExamScheduleId and ee.RegistrationId = el.RegistrationId " +
                                            " Where e.TestId = '" + TestId.ToString() + "' " +
                                            " and el.RegistrationId = '" + RegistrationId.ToString() + "' " +
                                            " and e.ExamScheduleId = '" + ExamScheduleId.ToString() + "' " +
                                            where +
                                            " Order by Subject,q.Srno ");

            }
            else
            {
                da = _aPI_BLL.returnDataTable(" Select q.QueId, q.Que as Question, ts.TestId, q.Language, s.Name as Subject, " +
                                         " Case When ((ee.Ans is not null or ee.AnsImage1 is not null or ee.AnsImage2 is not null or ee.AnsImage3 is not null or ee.AnsImage4 is not null) and  ISNULL(ee.Ans,'') <> '~SKIPPED~') then '1' " +
                                         " when ee.Ans = '~SKIPPED~' then '2' when ee.Ans is null and ee.AnsImage1 is null And ee.AnsImage2 is null " +
                                         " And ee.AnsImage3 is null And ee.AnsImage4 is null then '3' end as 'QueType',q.Srno as QueNo " +
                                         " From ExamSchedules e " +
                                         " inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
                                         " inner join Tests ts on ts.TestId = e.TestId " +
                                         " inner join Subs s on s.SubId = ts.SubId " +
                                         " inner join Ques q on s.SubId = q.SubId and ts.TestId = q.TestId " +
                                         " left join Exams ee on ee.QueId = q.QueId and q.TestId = ee.TestId " +
                                         " and e.ExamScheduleId = ee.ExamScheduleId and ee.RegistrationId = el.RegistrationId " +
                                         " Where e.TestId = '" + TestId.ToString() + "' " +
                                         " and el.RegistrationId = '" + RegistrationId.ToString() + "' " +
                                         " and e.ExamScheduleId = '" + ExamScheduleId.ToString() + "' " +
                                         where +
                                         " Order by s.Name,q.Srno ");
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