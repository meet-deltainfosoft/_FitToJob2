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


public partial class API_InsertScreenWiseDetails : System.Web.UI.Page
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
        string ScreenWiseDetailId = "";

        try
        {
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dtr in dt.Rows)
                {
                    try
                    {
                        ScreenWiseDetailId = _aPI_BLL.InsertUpdateWithReturnIdentity(" select ScreenWiseDetailId from ScreenWiseDetails " +
                                                                               " Where Screen = '" + dtr["Screen"].ToString() + "' ");
                    }
                    catch
                    {
                        ScreenWiseDetailId = "";
                    }

                    if (ScreenWiseDetailId != null && ScreenWiseDetailId != "")
                    {
                        _aPI_BLL.InsertUpdateNonQuery(" Update ScreenWiseDetails Set " +
                                                      " TotalViews = ISNULL(TotalViews,0) + " + ((dtr["TotalViews"] == DBNull.Value || dtr["TotalViews"].ToString() == "".ToString()) ? "NULL" : "" + Convert.ToDecimal(dtr["TotalViews"]) + "") +
                                                      " where ScreenWiseDetailId = '" + ScreenWiseDetailId.ToString() + "'");

                        da = _aPI_BLL.returnDataTable(" select ScreenWiseDetailId from ScreenWiseDetails where ScreenWiseDetailId = '" + ScreenWiseDetailId.ToString() + "'  ");
                        st.Append(DataTableToJsonObj(da));
                    }
                    else
                    {
                        ScreenWiseDetailId = _aPI_BLL.InsertUpdateWithReturnIdentity(" DECLARE  @ScreenWiseDetailId uniqueidentifier;" +
                                               " SET @ScreenWiseDetailId = NewId()" +
                                               " INSERT INTO ScreenWiseDetails ( " +
                                               " ScreenWiseDetailId, Screen, TotalViews, LastScreenInTime, UserId)" +
                                               " VALUES " +
                                               " (@ScreenWiseDetailId" +
                                               "," + ((dtr["Screen"] == DBNull.Value || dtr["Screen"] == "") ? "NULL" : "'" + dtr["Screen"].ToString() + "'") +
                                               "," + ((dtr["TotalViews"] == DBNull.Value || dtr["TotalViews"] == "") ? "NULL" : "" + dtr["TotalViews"].ToString() + "") +
                                               "," + ((dtr["LastScreenInTime"] == DBNull.Value || dtr["LastScreenInTime"] == "") ? "NULL" : "'" + Convert.ToDateTime(dtr["LastScreenInTime"]).ToString("dd-MMM-yyyy hh:mm") + "'") +
                                               "," + ((dtr["UserId"] == DBNull.Value || dtr["UserId"] == "") ? "NULL" : "'" + dtr["UserId"].ToString() + "'") +
                                               ");" +
                                               " SELECT @ScreenWiseDetailId;");

                        da = _aPI_BLL.returnDataTable(" select ScreenWiseDetailId from ScreenWiseDetails where ScreenWiseDetailId = '" + ScreenWiseDetailId.ToString() + "'  ");
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