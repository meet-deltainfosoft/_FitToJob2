using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;


public partial class API_InsertItemWiseDetails : System.Web.UI.Page
{
    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Form.Count > 0)
                {
                    DataTable dt = new DataTable();
                    try
                    {
                        string JSONstr = Request.Form["Analyzer"];
                        dt = JsonConvert.DeserializeObject<DataTable>(JSONstr);
                    }
                    catch (Exception ex)
                    {
                        string sw = "";
                        StringBuilder s = new StringBuilder();
                        s.Append(ex.Message);
                        sw = GetReturnValue("207", "Request Form Issue.", s);
                        Response.ContentType = "application/json";
                        Response.Write(sw.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]"));
                    }
                    if (dt.Rows.Count > 0)
                    {
                        Response.ContentType = "application/json";
                        Response.Write(InsertData(dt));
                    }
                }
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


    public string InsertData(DataTable dt)
    {
        DataTable da = new DataTable();
        StringBuilder st = new StringBuilder();
        string ReturnVal = "";
        string ItemWiseDetailId = "";

        try
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dtr in dt.Rows)
                {
                    try
                    {
                        ItemWiseDetailId = _aPI_BLL.InsertUpdateWithReturnIdentity(" select ItemWiseDetailId from ItemWiseDetails " +
                                                                               " Where ItemId = '" + dtr["ItemId"].ToString() + "' ");
                    }
                    catch
                    {
                        ItemWiseDetailId = "";
                    }

                    if (ItemWiseDetailId != null && ItemWiseDetailId != "")
                    {
                        _aPI_BLL.InsertUpdateNonQuery(" Update ItemWiseDetails Set " +
                                                      " TotalCount = ISNULL(TotalCount,0) + " + ((dtr["TotalCount"] == DBNull.Value || dtr["TotalCount"] == "") ? "NULL" : "" + Convert.ToDecimal(dtr["TotalCount"]) + "") +
                                                      " ,TotalFiftyPercentShowCnt = ISNULL(TotalFiftyPercentShowCnt,0) + " + ((dtr["TotalFiftyPercentShowCnt"] == DBNull.Value || dtr["TotalFiftyPercentShowCnt"] == "") ? "NULL" : "" + Convert.ToDecimal(dtr["TotalFiftyPercentShowCnt"]) + "") +
                                                      " where ItemWiseDetailId = '" + ItemWiseDetailId.ToString() + "'");

                        da = _aPI_BLL.returnDataTable(" select ItemWiseDetailId from ItemWiseDetails where ItemWiseDetailId = '" + ItemWiseDetailId.ToString() + "'  ");
                        st.Append(DataTableToJsonObj(da));
                    }
                    else
                    {
                        ItemWiseDetailId = _aPI_BLL.InsertUpdateWithReturnIdentity(" DECLARE  @ItemWiseDetailId uniqueidentifier;" +
                                                                           " SET @ItemWiseDetailId = NewId()" +
                                                                           " INSERT INTO ItemWiseDetails ( " +
                                                                           " ItemWiseDetailId, ItemType, ItemId, [Name], [URL], TotalCount,TotalFiftyPercentShowCnt, LastEnteredIn, UserId)" +
                                                                           " VALUES " +
                                                                           " (@ItemWiseDetailId" +
                                                                           "," + ((dtr["ItemType"] == DBNull.Value || dtr["ItemType"] == "") ? "NULL" : "'" + dtr["ItemType"].ToString() + "'") +
                                                                           "," + ((dtr["ItemId"] == DBNull.Value || dtr["ItemId"] == "") ? "NULL" : "'" + dtr["ItemId"].ToString() + "'") +
                                                                           "," + ((dtr["Name"] == DBNull.Value || dtr["Name"] == "") ? "NULL" : "'" + dtr["Name"].ToString() + "'") +
                                                                           "," + ((dtr["URL"] == DBNull.Value || dtr["URL"] == "") ? "NULL" : "'" + dtr["URL"].ToString() + "'") +
                                                                           "," + ((dtr["TotalCount"] == DBNull.Value || dtr["TotalCount"] == "") ? "NULL" : "" + dtr["TotalCount"].ToString() + "") + "," + ((dtr["TotalFiftyPercentShowCnt"] == DBNull.Value || dtr["TotalFiftyPercentShowCnt"] == "") ? "NULL" : "" + dtr["TotalFiftyPercentShowCnt"].ToString() + "") +
                                               "," + ((dtr["LastEnteredIn"] == DBNull.Value || dtr["LastEnteredIn"] == "") ? "NULL" : "'" + dtr["LastEnteredIn"].ToString() + "'") +
                                               "," + ((dtr["UserId"] == DBNull.Value || dtr["UserId"] == "") ? "NULL" : "'" + dtr["UserId"].ToString() + "'") +
                                               ");" +
                                               " SELECT @ItemWiseDetailId;");

                        da = _aPI_BLL.returnDataTable(" select ItemWiseDetailId from ItemWiseDetails where ItemWiseDetailId = '" + ItemWiseDetailId.ToString() + "'  ");
                        st.Append(DataTableToJsonObj(da));
                    }

                }
            }
            else
            {
                da = null;
            }

            if (da == null)
            {
                ReturnVal = GetReturnValue("209", "No Record Found", st);
            }

            if (st.ToString() == "[]" || st.ToString() == "")
            {
                ReturnVal = GetReturnValue("209", "Data Insert Issue", st);
            }

            if (da != null)
            {
                if (da.Rows.Count > 0)
                {
                    StringBuilder s = new StringBuilder();
                    s.Append("Saved");
                    ReturnVal = GetReturnValue("200", "Ok", s);
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