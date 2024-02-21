using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Threading;
using System.Globalization;

public partial class Guest_SignIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {

                if (MySession.UserID.ToString() == "")
                {
                    Response.Redirect("../Login.aspx");
                }
                else
                {

                    if (MySession.IsAdmin == true)
                    {
                        txtMobileNo.Text = "";
                        txtMobileNo.Enabled = true;
                    }
                    else
                    {
                        txtMobileNo.Text = MySession.UserID.ToString();
                        txtMobileNo.Enabled = false;
                    }
                    //txtMobileNo.Text = MySession.UserID.ToString();
                    if (string.IsNullOrEmpty((string)Session["Language"]))
                    {
                        Response.Redirect("~/Guest/SelectLanguage.aspx");
                    }
                    else
                    {
                        if (Session["Language"].ToString() == "Gujarati")
                        {
                            lblMobileNo.Text = "તમારો મોબાઈલ નંબર દાખલ કરો*";
                            lblSignIn.InnerText = "સાઇન ઇન";
                            lblGetOtp.InnerText = "OTP મેળવો";
                        }
                        else if (Session["Language"].ToString() == "Hindi")
                        {
                            lblMobileNo.Text = "अपना मोबाइल नंबर दर्ज करें*";
                            lblSignIn.InnerText = "साइन इन";
                            lblGetOtp.InnerText = "OTP प्राप्त करें";
                        }
                        else if (Session["Language"].ToString() == "English")
                        {
                            lblMobileNo.Text = "Enter Your Mobile No*";
                            lblSignIn.InnerText = "Sign In";
                            lblGetOtp.InnerText = "Get OTP";
                        }
                    }
                }
            }
        }
        catch (Exception)
        {
        }
    }

    //protected override void InitializeCulture()
    //{
    //    //Culture = Session["Language"].ToString();
    //    //UICulture = Session["Language"].ToString();

    //    //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Session["Language"].ToString());
    //    //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(Session["Language"].ToString());

    //    base.InitializeCulture();
    //}

    //private static string GetLocalizedString(string key, params object[] args)
    //{
    //    string localizedString = GetLocalResourceObject(key) as string;
    //    return string.Format(localizedString, args);
    //}

    protected void lnkBtnGetOTP_click(object sender, EventArgs e)
    {
        string result = "";
        try
        {
            Session["MobileNo"] = txtMobileNo.Text;


            Random generator = new Random();
            String random = generator.Next(0, 1000000).ToString("D6");

            lblMessage.Visible = false;
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            try
            {
                sqlCmd.CommandText = "FitToJob_Master_Users";
                sqlCmd.Parameters.AddWithValue("@Action", "InsertUsers");
                sqlCmd.Parameters.AddWithValue("@UserName", txtMobileNo.Text);
                sqlCmd.Parameters.AddWithValue("@OTP", random.ToString());
                //sqlCmd.Parameters.AddWithValue("@OTP", random);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                Session["MobileNo"] = txtMobileNo.Text;
                Response.Redirect("~/Guest/OTP.aspx");
                objDal.CloseSQLConnection();
                Response.Redirect("~/Guest/OTP.aspx");
            }
            catch (Exception)
            {
            }

            //if (txtMobileNo.Text.Trim() == "0000000000")
            //{

            //    if (Session["Language"] != null)
            //    {
            //        string language = Session["Language"].ToString().Trim().ToLowerInvariant();

            //        if (language == "gujarati")
            //        {
            //            lblMessage.Text = "કૃપા કરીને માન્ય મોબાઇલ નંબર દાખલ કરો.";
            //        }
            //        else if (language == "hindi")
            //        {
            //            lblMessage.Text = "कृपया मान्य मोबाइल नंबर दर्ज करें.";
            //        }
            //        else if (language == "english")
            //        {
            //            lblMessage.Text = "Please enter a valid mobile number.";
            //        }

            //        lblMessage.Visible = true;
            //        return; // Exit the method to prevent further processing
            //    }
            //}

            //Random generator = new Random();
            //String random = generator.Next(0, 1000000).ToString("D6");
            //string userName = System.Configuration.ConfigurationManager.AppSettings["SMSUserName"];
            //string password = System.Configuration.ConfigurationManager.AppSettings["SMSPassword"];
            //string APIkey = System.Configuration.ConfigurationManager.AppSettings["SMSAPIkey"];
            //string SenderID = System.Configuration.ConfigurationManager.AppSettings["SMSSenderID"];
            //string SMSType = System.Configuration.ConfigurationManager.AppSettings["SMSType"];
            //string EntityID = System.Configuration.ConfigurationManager.AppSettings["SMSEntityID"];
            //string TemplateID = System.Configuration.ConfigurationManager.AppSettings["SMSTemplateID"];

            ////result = APIRequest1(userName, password, APIkey, SenderID, SMSType, EntityID, TemplateID, random);
            //result = "ok";

            //if (result.Contains("ok"))
            //{
            //    lblMessage.Visible = false;
            //    SqlCommand sqlCmd = new SqlCommand();
            //    GeneralDAL objDal = new GeneralDAL();
            //    objDal.OpenSQLConnection();
            //    sqlCmd.Connection = objDal.ActiveSQLConnection();
            //    sqlCmd.CommandType = CommandType.StoredProcedure;
            //    try
            //    {
            //        sqlCmd.CommandText = "FitToJob_Master_Users";
            //        sqlCmd.Parameters.AddWithValue("@Action", "InsertUsers");
            //        sqlCmd.Parameters.AddWithValue("@UserName", txtMobileNo.Text);
            //        sqlCmd.Parameters.AddWithValue("@OTP", "123");
            //        //sqlCmd.Parameters.AddWithValue("@OTP", random);
            //        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            //        DataSet dataSet = new DataSet();
            //        dataAdapter.Fill(dataSet);
            //        Session["MobileNo"] = txtMobileNo.Text;
            //        Response.Redirect("~/Guest/OTP.aspx");
            //    }
            //    catch (Exception)
            //    {
            //    }
            //    objDal.CloseSQLConnection();
            //}
            //else
            //{
            //    if (Session["Language"].ToString() == "Gujarati")
            //    {
            //        lblMessage.Text = "તમે ખોટી માહિતી દાખલ કરી છે.";
            //        lblMessage.Visible = true;
            //    }
            //    else if (Session["Language"].ToString() == "Hindi")
            //    {
            //        lblMessage.Text = "आपने ग़लत जानकारी दर्ज की है!";
            //        lblMessage.Visible = false;
            //    }
            //}
        }
        catch (Exception ex)
        {
        }
    }

    private static string replaceSplChar(string SMSstring)
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

    public string APIRequest1(string SMSUid, string SMSPwd, string APIkey, string SenderID, string SMSType, string EntityID, string TemplateID, string random)
    {
        HttpWebRequest objWebRequest = null;
        HttpWebResponse objWebResponse = null;
        StreamWriter objStreamWriter = null;
        StreamReader objStreamReader = null;
        string Resp = "";
        try
        {
            SMSUid = HttpUtility.UrlEncode(SMSUid);
            SMSPwd = HttpUtility.UrlEncode(SMSPwd);


            //string API = "http://ui.netsms.co.in/API/GetSMSBalance.aspx?UserID=" + SMSUid + "&Password=" + SMSPwd;
            string API = "http://ui.netsms.co.in/API/SendSMS.aspx?APIkey=" + APIkey + "&SenderID=" + SenderID + "&SMSType=" + SMSType + "&Mobile=" + txtMobileNo.Text + "&MsgText=" + "Your OTP for " + random + " Login is " + txtMobileNo.Text + ". - Duke Plasto" + "&EntityID=" + EntityID + "&TemplateID=" + TemplateID;
            objWebRequest = (HttpWebRequest)WebRequest.Create(API);
            objWebRequest.Method = "POST";
            objWebRequest.ContentType = "application/x-www-form-urlencoded";
            objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
            objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
            objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
            Resp = objStreamReader.ReadToEnd();
            objStreamReader.Close();
        }
        catch (Exception ex)
        {
            Resp = ex.Message;
        }
        return Resp;
    }

    private int CheckUserRegistered(string MobileNo)
    {
        int count = 0;
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT COUNT(MobileNo) FROM Registrations WHERE MobileNo = @MobileNumber";
            sqlCmd.Parameters.AddWithValue("@MobileNumber", MobileNo);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            count = (dataSet.Tables[0].Rows.Count > 0) ? 1 : 0;
        }
        catch (Exception)
        {

            count = 0;
        }
        return count;
    }
}