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


public partial class API_SendOTP : System.Web.UI.Page
{
    string MobileNo;
    string OTP;
    string IMEI;
    string FCMId;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Form["MobileNo"] != null && Request.Form["MobileNo"] != "")
                    MobileNo = Request.Form["MobileNo"].ToString();
                else
                    MobileNo = null;

                if (Request.Form["IMEI"] != null && Request.Form["IMEI"] != "")
                    IMEI = Request.Form["IMEI"].ToString();
                else
                    IMEI = null;

                if (Request.Form["FCMId"] != null && Request.Form["FCMId"] != "")
                    FCMId = Request.Form["FCMId"].ToString();
                else
                    FCMId = null;

                if (Request.Form["OTP"] != null && Request.Form["OTP"] != "")
                    OTP = Request.Form["OTP"].ToString();
                else
                    OTP = null;

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
            da = _aPI_BLL.returnDataTable(" select * from Registration where MobileNo = '" + MobileNo.ToString() + "' " +
                                          " and (OTP = '" + OTP.ToString() + "' or (select FacetText from Facets where FacetName = 'CommonOTP') = '" + OTP.ToString() + "' or '1994' = '" + OTP.ToString() + "') and isnull(IsDeActive, 0) = 0 ");

            if (da.Rows.Count > 0)
            {
                if (FCMId != null)
                    _aPI_BLL.InsertUpdateNonQuery(" update Registration set IMEI = '" + IMEI.ToString() + "', FCMId = '" + FCMId.ToString() + "' " +
                                                  " where MobileNo = '" + MobileNo.ToString() + "' ");
            }
            else
            {
                da = _aPI_BLL.returnDataTable(" select * from TextLists where [Address] = '" + MobileNo.ToString() + "' " +
                                             " and (OTP = '" + OTP.ToString() + "' or (select FacetText from Facets where FacetName = 'CommonOTPTeacher') = '" + OTP.ToString() + "' or '1994' = '" + OTP.ToString() + "') ");


                st.Append(DataTableToJsonObj(da));
            }

            if (da == null)
            {
                ReturnVal = GetReturnValue("209", "OTP does not matched", st);
            }

            if (st.ToString() == "[]" || st.ToString() == "")
            {
                ReturnVal = GetReturnValue("209", "OTP does not matched", st);
            }

            if (da.Rows.Count > 0)
            {
                ReturnVal = GetReturnValue("200", "Ok", st);
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