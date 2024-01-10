using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.Configuration;

public partial class API_HelpChapterList : System.Web.UI.Page
{
    string ChapterId;
    string ChapterVideoId;

    API_BLL _aPI_BLL = new API_BLL();
    private GeneralDAL _generalDAL = new GeneralDAL();

    #region "Code For HMAC"
    string DK;
    string configAppId = ConfigurationManager.AppSettings["APPId"].ToString();
    string UserId;
    string HashHMACHex;
    string key = ConfigurationManager.AppSettings["HashKey"].ToString();
    #endregion "Code For HMAC"
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

                if (Request.Form["ChapterVideoId"] != null && Request.Form["ChapterVideoId"] != "")
                    ChapterVideoId = Request.Form["ChapterVideoId"].ToString();
                else
                    ChapterVideoId = null;

                //#region "Code For HMAC"
                //if (Request.Form["UserId"] != null)
                //{
                //    UserId = Request.Form["UserId"].ToString();
                //}
                //else
                //{
                //    UserId = null;
                //}

                //if (Request.Form["DK"] != null && Request.Form["DK"] != "")
                //{
                //    DK = Request.Form["DK"].ToString();
                //}
                //else
                //{
                //    DK = null;
                //}

                //string AndroidKeyIdFromDb = _generalDAL.GetAndroidKeyFromDB(UserId.Replace("'", "','"));

                //HashHMACHex = _generalDAL.HashHMACHex(key, configAppId + ":" + AndroidKeyIdFromDb);
                //#endregion "Code For HMAC"

                //if (HashHMACHex == DK) //Check HMAC of Mobile and API if match then perform below code else send message "Authentication Key is wrong. Please relogin in Mobile application."
                //{
                Response.ContentType = "application/json";
                Response.Write(selectdata());
                //}
                //else
                //{
                //    string sw = "";
                //    StringBuilder s = new StringBuilder();
                //    s.Append("Authentication Key is wrong. Please relogin in Mobile application.");
                //    sw = GetReturnValue("209", "Authentication Key is wrong. Please relogin in Mobile application.", s);
                //    Response.ContentType = "application/json";
                //    Response.Write(sw.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]"));
                //}
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
            //da = _aPI_BLL.returnDataTable(" select c1.*, VideoCnt = (select count(*) from ChapterVideos x where x.ChapterId = c1.ChapterId) + (select count(*) from ChapterLinks x where x.ChapterId = c1.ChapterId) " +
            //                              " , PDFCnt = (select count(*) from ChapterPDFs x where x.ChapterId = c1.ChapterId) " +
            //                              " , t.[Text] as 'StandardName', s.Name as 'SubName', cv.ChapterVideoId, cv.VideoName " +
            //                              " , replace(cv.VideoLink, 'https://vimeo.com/', '') as 'VideoID' " +
            //                              " from Chapters c " +
            //                              " inner join ChapterLns cl on cl.ChapterId = c.ChapterId " +
            //                              " inner join Chapters c1 on c1.ChapterId = cl.ChapterTextListId " +
            //                              " inner join TextLists t on t.TextListId = c1.StandardTextListId " +
            //                              " inner join Subs s on s.SubId = c1.SubId " +
            //                              " inner join ChapterVideos cv on cv.ChapterVideoId = cl.ChapterVideoId " +
            //                              " where c.ChapterId = '" + ChapterId.ToString() + "' " +                
            //                              " order by c1.SrNo ");

            da = _aPI_BLL.returnDataTable(" select c1.*, VideoCnt = (select count(*) from ChapterVideos x where x.ChapterId = c1.ChapterId) + (select count(*) from ChapterLinks x where x.ChapterId = c1.ChapterId) " +
                                          " , PDFCnt = (select count(*) from ChapterPDFs x where x.ChapterId = c1.ChapterId) " +
                                          " , t.[Text] as 'StandardName', s.Name as 'SubName', cv.ChapterVideoId, cv.VideoName " +
                                          " , replace(cv.VideoLink, 'https://vimeo.com/', '') as 'VideoID' " +
                                          " from ChapterVideos cv1 " +
                                          " inner join ChapterLns cl on cl.ChapterVideoHeaderId = cv1.ChapterVideoId " +
                                          " inner join Chapters c1 on c1.ChapterId = cl.ChapterTextListId " +
                                          " inner join TextLists t on t.TextListId = c1.StandardTextListId " +
                                          " inner join Subs s on s.SubId = c1.SubId " +
                                          " inner join ChapterVideos cv on cv.ChapterVideoId = cl.ChapterVideoId " +
                                          " where cl.ChapterVideoHeaderId = '" + ChapterVideoId.ToString() + "' " +
                                          " order by c1.SrNo ");

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