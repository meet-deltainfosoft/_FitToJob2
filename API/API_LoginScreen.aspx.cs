using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Configuration;

public partial class API_API_LoginScreen : System.Web.UI.Page
{
    string AadharCardNo;
    string MobileNo;
    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Form["AadharCardNo"] != null && Request.Form["AadharCardNo"] != "")
                    AadharCardNo = Request.Form["AadharCardNo"].ToString();
                else
                    AadharCardNo = null;

                if (Request.Form["MobileNo"] != null && Request.Form["MobileNo"] != "")
                    MobileNo = Request.Form["MobileNo"].ToString();
                else
                    MobileNo = null;

                Response.ContentType = "application/json";
                Response.Write(SelectData());
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

    public string SelectData()
    {
        DataTable da = new DataTable();
        StringBuilder st = new StringBuilder();
        string ReturnVal = "";

        try
        {
            
            da = _aPI_BLL.returnDataTable("select * from Registrations where MobileNo = '" + MobileNo.ToString() + "' " +
                                          " and AadharCardNo= '" + AadharCardNo.ToString() + "'");
            if (da.Rows.Count > 0)
            {
                string OTP = ConfigurationSettings.AppSettings["FixOtp"].ToString();
                //string OTP = GenerateOTP();
                //string Message = "" + OTP + "  is SECRET OTP (One Time Password) for Forgot Password Fit To Job. - Delta iERP";
                ////string strUrl = "http://ui.netsms.co.in/API/SendSMS.aspx?APIkey=Wc9TRy7f6OTQmPrcrT4YXdCb4I&SenderID=DUKEPT&SMSType=OTP&Mobile=<MobileNo>&MsgText=<MsgText>&EntityID=1301159922245082311&TemplateID=1307165613875688264";
                //string strUrl = "http://ui.netsms.co.in/API/SendSMS.aspx?APIkey=TPjeJbwWIHW1KvHqSLAiCI6JLI&SenderID=iERPin&SMSType=4&Mobile=~MOBILENO~&MsgText=~TEXT~&EntityID=1301159922635362488&TemplateID=1307163255766488183";
                //strUrl = strUrl.Replace("~MOBILENO~", MobileNo);
                //strUrl = strUrl.Replace("~TEXT~", Message);
                //WebRequest request = HttpWebRequest.Create(strUrl);
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Stream s = (Stream)response.GetResponseStream();
                //StreamReader readStream = new StreamReader(s);
                //string dataString = readStream.ReadToEnd();
                //response.Close();
                //s.Close();
                //readStream.Close();
                _aPI_BLL.InsertUpdateNonQuery("update Registrations set OTP = '" + OTP.ToString() + "'  where RegistrationId = '" + da.Rows[0]["RegistrationId"].ToString() + "' ");

                da = _aPI_BLL.returnDataTable("select OTP from Registrations where MobileNo = '" + MobileNo.ToString() + "' " +
                                          " and AadharCardNo= '" + AadharCardNo.ToString() + "'");


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
                ReturnVal = GetReturnValue("209", "Please Check Your AadharCardNo And MobileNo", st);
            }

            if (da.Rows.Count > 0)
            {
                ReturnVal = GetReturnValue("200", "OTP Send Successfully", st);
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
        string numbers = "1234567890";

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
}