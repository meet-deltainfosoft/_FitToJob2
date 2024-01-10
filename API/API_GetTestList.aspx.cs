using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;

public partial class API_GetTestList : System.Web.UI.Page
{
    string SubId;
    DateTime? Date;
    string ChapterId;
    string ChapterVideoId;

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

                if (Request.Form["Date"] != null && Request.Form["Date"] != "")
                    Date = Convert.ToDateTime(Request.Form["Date"]);
                else
                    Date = null;

                if (Request.Form["ChapterId"] != null && Request.Form["ChapterId"] != "")
                    ChapterId = Request.Form["ChapterId"].ToString();
                else
                    ChapterId = null;

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
        try
        {
            string where = "";

            if (ChapterId != null)
                where += " And c.ChapterId = '" + ChapterId + "'";

            da = _aPI_BLL.returnDataTable(" Select t.TestId,t.TestName,t.SubId,e.ExamDate,e.ExamscheduleId, 'Exam' as 'Type'  " +
                                          " from Tests t " +
                                          " Inner join ExamSchedules e on e.TestId = t.TestId and e.SubId = t.SubId " +
                                          " Where t.SubId = '" + SubId.ToString() + "' " +
                                          ((Date == null) ? "" : " and e.ExamDate = '" + Convert.ToDateTime(Date).ToString("dd-MMM-yyyy") + "' ") +
                                          " union all " +
                                          " Select h.HomeWorkId, isnull(h.HomeWork, s.Name + ' ' + c.ChapterName) as ChapterName,s.SubId,h.Dt, c.ChapterId, 'Homework' as 'Type' " +
                                          " from HomeWorks h " +
                                          " inner join subs s on s.SubId = h.SubId " +
                                          " inner join Chapters c on c.SubId = s.SubId and h.ChapterId = c.ChapterId " +
                                          " Where s.SubId in ('" + SubId.ToString() + "') " +
                                          ((ChapterVideoId == null) ? "" : " and h.ChapterVideoId = '" + ChapterVideoId + "' ") +
                //" and h.Dt = '" + Convert.ToDateTime(Date).ToString("dd-MMM-yyyy") + "' " +
                                          " " + where + "  order by ExamDate");

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