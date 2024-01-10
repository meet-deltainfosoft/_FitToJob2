using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.Net;
using System.IO;


public partial class API_ResendOTP : System.Web.UI.Page
{
    string MobileNo;
    string Key;

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

                if (Request.Form["Key"] != null && Request.Form["Key"] != "")
                    Key = Request.Form["Key"].ToString();
                else
                    Key = null;

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
            da = _aPI_BLL.returnDataTable(" Select * From RegistrationDeltaAdmin where MobileNo = '" + MobileNo.ToString() + "' and isnull(IsDeActive, 0) = 0 ");

            if (da.Rows.Count > 0)
            {
                string OTP = null;

                if (da.Rows[0]["OTP"] != DBNull.Value)
                {
                    OTP = da.Rows[0]["OTP"].ToString();
                }
                else
                {
                    OTP = GenerateOTP();
                }

                SendSMS(MobileNo, Key, OTP, da.Rows[0]["DBName"].ToString());

                foreach (DataRow dtr in da.Rows)
                {
                    _aPI_BLL.InsertUpdateNonQuery(" Update " + dtr["DBName"] + "..Registration set OTP = '" + OTP.ToString() + "' " +
                                                  " ,OTPGeneratedOn = GETDATE() " +
                                                  " where MobileNo = '" + da.Rows[0]["MobileNo"].ToString() + "' ");
                }
                da = _aPI_BLL.returnDataTable(" select * from RegistrationDeltaAdmin where MobileNo = '" + MobileNo.ToString() + "'" +
                                              " and isnull(IsDeActive, 0) = 0 ");

                st.Append(DataTableToJsonObj(da));
            }
            else
            {
                st.Append(DataTableToJsonObj(da));
            }

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

    protected string GenerateOTP()
    {
        string ReturnVal = "";
        try
        {
            string numbers = "123456789";
            string characters = numbers;
            characters += numbers;

            string otp = string.Empty;

            for (int i = 0; i < 4; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }
        catch (Exception ex)
        {
            StringBuilder s = new StringBuilder();
            s.Append(ex.Message);
            ReturnVal = GetReturnValue("209", "OTP Generate Issue", s);
            return ReturnVal.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]");
        }
    }

    private string replaceSplChar(string SMSstring)
    {
        SMSstring = SMSstring.Replace("%", "%25");
        SMSstring = SMSstring.Replace("<", "%3C");
        SMSstring = SMSstring.Replace(">", "%3E");
        SMSstring = SMSstring.Replace("&", "%26");
        SMSstring = SMSstring.Replace("+", "%2B");
        SMSstring = SMSstring.Replace("#", "%23");
        SMSstring = SMSstring.Replace("*", "%2A");
        SMSstring = SMSstring.Replace("!", "%21");
        SMSstring = SMSstring.Replace(",", "%2C");
        SMSstring = SMSstring.Replace("'", "%27");
        SMSstring = SMSstring.Replace("=", "%3D");
        SMSstring = SMSstring.Replace("â‚¬", "%E2%82%AC");
        SMSstring = SMSstring.Replace("?", "%3F");
        SMSstring = SMSstring.Replace(" ", "+");
        SMSstring = SMSstring.Replace("$", "%24");
        return SMSstring;
    }

    protected void SendSMS(string MobileNo, string Key, string OTP, string DBName)
    {
        string Message = "";
        Message = "<#> " + OTP + " is OTP for Digital iClass App Login. " + Key + "";

        Message = replaceSplChar(Message);

        string strUrl = _aPI_BLL.SMSString(DBName);
        strUrl = strUrl.Replace("[MOBILENO]", MobileNo);
        strUrl = strUrl.Replace("[MESSAGE]", Message);
        WebRequest request = HttpWebRequest.Create(strUrl);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Stream s = (Stream)response.GetResponseStream();
        StreamReader readStream = new StreamReader(s);
        string dataString = readStream.ReadToEnd();
        response.Close();
        s.Close();
        readStream.Close();
    }
}