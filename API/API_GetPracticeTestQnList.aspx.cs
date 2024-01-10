using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.Configuration;

public partial class API_GetPracticeTestQnList : System.Web.UI.Page
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
            da = _aPI_BLL.returnDataTable(" Select h.QueBankId,h.SubId,h.ChapterId, getdate() as Dt, h.Que,h.A1,h.A2,h.A3,h.A4,s.Name as 'SubJect' " +
                                          " , c.ChapterName, newid() as 'OrderBy' " +
                                          " , replace(h.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                          " , replace(h.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                          " , replace(h.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                          " , replace(h.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                          " , replace(h.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                          " , h.QueType, h.QueDataType, h.NoOfFile, h.AnsSelection, h.Srno as QueNo " +
                                          " , replace(h.SampleAns1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns1' " +
                                          " , replace(h.SampleAns2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns2' " +
                                          " , replace(h.SampleAns3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns3' " +
                                          " , replace(h.SampleAns4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns4' " +
                                          " into #tmp " +
                                          " From QueBanks h " +
                                          " Inner Join Subs s on s.SubId = h.SubId " +
                                          " Inner Join Chapters c on c.ChapterId = h.ChapterId" +
                                          " Inner Join TextLists t on t.TextListId = s.StandardTextListId " +
                                          " Where h.ChapterId = '" + ChapterId + "' and h.ChapterVideoId = '" + ChapterVideoId.ToString() + "' " +
                                          " and h.QueBankId not in " +
                                          " (select QueBankId from QueBankAns where RegistrationId = '" + RegistrationId.ToString() + "' and ChapterId = '" + ChapterId + "' and ChapterVideoId = '" + ChapterVideoId.ToString() + "') ; " +
                                          " Select SubId, ChapterId, QueBankId, Que, A1, A2, A3, A4,[Subject], ChapterName, " +
                                          " ROW_NUMBER() OVER(ORDER BY OrderBy) as SrNo, OrderBy ,ImageNameQus, ImageNameA1, ImageNameA2,  " +
                                          " ImageNameA3,  ImageNameA4 ,QueType, QueDataType, NoOfFile, QueNo, " +
                                          " SampleAns1,SampleAns2,SampleAns3,SampleAns4" +
                                          " from #tmp  " +
                                          " ORDER BY ROW_NUMBER() OVER(ORDER BY OrderBy)  drop table #tmp ; ");

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