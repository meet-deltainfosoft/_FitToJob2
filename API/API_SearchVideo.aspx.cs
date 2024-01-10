using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.Configuration;

public partial class API_SearchVideo : System.Web.UI.Page
{
    string ChapterId;
    string SubjectId;
    string NameHashatagLike;

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

                if (Request.Form["SubjectId"] != null && Request.Form["SubjectId"] != "")
                    SubjectId = Request.Form["SubjectId"].ToString();
                else
                    SubjectId = null;

                if (Request.Form["NameHashatagLike"] != null && Request.Form["NameHashatagLike"] != "")
                    NameHashatagLike = Request.Form["NameHashatagLike"].ToString();
                else
                    NameHashatagLike = null;

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

    public string selectdata()
    {
        DataSet ds = new DataSet();

        DataTable da = new DataTable();
        DataTable da1 = new DataTable();
        DataTable da2 = new DataTable();

        StringBuilder st = new StringBuilder();
        string ReturnVal = "";
        string Where = "";
        try
        {
            if (ChapterId != "" && ChapterId != null)
                Where = " and ChapterId = '" + ChapterId.ToString() + "' ";

            da = _aPI_BLL.returnDataTable(" select * from ChapterVideos where SubId = '" + SubjectId.ToString() + "' " + Where +
                                          " and (VideoName like '%" + NameHashatagLike + "%' or Hashtag like '%" + NameHashatagLike + "%') order by VideoName ");

            da1 = _aPI_BLL.returnDataTable(" select ChapterPDFId, ChapterId, SrNo, FileName,  Remarks, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, StandardTextListId, SubId,replace(FileLink, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'FileLink' from ChapterPDFs where SubId = '" + SubjectId.ToString() + "' " + Where +
                " and (FileName like '%" + NameHashatagLike + "%' or Hashtag like '%" + NameHashatagLike + "%') order by FileName ");

            da2 = _aPI_BLL.returnDataTable(" select * from ChapterLinks where SubId = '" + SubjectId.ToString() + "' " + Where +
                                           " and (LinkName like '%" + NameHashatagLike + "%' or Hashtag like '%" + NameHashatagLike + "%') order by LinkName ");

            ds.Tables.Add(da);
            ds.Tables.Add(da1);
            ds.Tables.Add(da2);

            st.Append(DataTableToJsonObj(ds));

            if (ds == null)
            {
                ReturnVal = GetReturnValue("209", "No Record Found", st);
            }

            if (st.ToString() == "[[],[],[]]" || st.ToString() == "")
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
            JsonString.Append("[");
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
            JsonString.Append("]");

            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }
}