using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.IO;

public partial class Guest_OTP : System.Web.UI.Page
{

    string MobileNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GeneralDAL objDal = null;
        try
        {
            objDal = new GeneralDAL();

            if (string.IsNullOrEmpty((string)Session["MobileNo"]) || string.IsNullOrEmpty((string)Session["Language"]))
            {
                Response.Redirect("~/Guest/SignIn.aspx");
            }
            else
            {

                string IsRegistered = "";
                SqlCommand Cmd = new SqlCommand();
                objDal.OpenSQLConnection();
                Cmd.Connection = objDal.ActiveSQLConnection();
                Cmd.CommandType = CommandType.Text;
                Cmd.CommandText = "select Isnull(Email,'') as DigitSignaturePath from Registrations where MobileNo = '" + Session["MobileNo"].ToString() + "'";
                try
                {
                    IsRegistered = Cmd.ExecuteScalar().ToString();
                }
                catch
                {
                    IsRegistered = null;
                }
                if (IsRegistered == "" || IsRegistered == null)
                {
                    lnkInterviewstatus.Visible = false;
                }


                if (Session["Language"].ToString() == "Gujarati")
                {
                    lblVerifyOtp.InnerText = "આગામી ફોર્મ પર જાઓ";
                    lblMessage.Text = "અમાન્ય OTP";
                }
                else if (Session["Language"].ToString() == "Hindi")
                {
                    lblVerifyOtp.InnerText = "अगला फॉर्म जाओ";
                    lblMessage.Text = "अमान्य OTP";
                }
                else
                {
                    lblVerifyOtp.InnerText = "Go To Next Form";
                    lblMessage.Text = "Invalid OTP";
                }

                if (IsMobileNumberRegistered(Session["MobileNo"].ToString()))
                {
                    btnprint.Enabled = true;
                }
                else
                {
                    btnprint.Enabled = false;
                }


                DataTable dt = ViewDataFrom_MobileNo();

                btnprint.Visible = (bool)dt.Rows[0]["IsCandidateRegistred"];
                lnkInterviewstatus.Visible = (bool)dt.Rows[0]["IsInterviewStatusDone"];
                lnkInterviewUploadDocuments.Visible = (bool)dt.Rows[0]["IsUploadCoumentAllows"];
                lnkbtnOffer.Visible = (bool)dt.Rows[0]["IsOfferGenerate"];

            }

        }
        catch (Exception)
        {
        }

    }

    private bool IsMobileNumberRegistered(string mobileNumber)
    {
        GeneralDAL objDal = new GeneralDAL();
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            // GeneralDAL objDal = new GeneralDAL();

            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Users_ WHERE UserName = @MobileNumber";
            sqlCmd.Parameters.AddWithValue("@MobileNumber", mobileNumber);
            int count = Convert.ToInt32(sqlCmd.ExecuteScalar());

            return count > 0;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            objDal.CloseSQLConnection();
        }
    }
    protected void lnkBtnGetOTP_click(object sender, EventArgs e)
    {
        try
        {
            //SqlCommand sqlCmd = new SqlCommand();
            //GeneralDAL objDal = new GeneralDAL();
            //objDal.OpenSQLConnection();
            //sqlCmd.Connection = objDal.ActiveSQLConnection();
            //sqlCmd.CommandType = CommandType.StoredProcedure;
            //try
            //{
            //    sqlCmd.CommandText = "FitToJob_Master_Users";
            //    sqlCmd.Parameters.AddWithValue("@Action", "VerfiyOTP");
            //    sqlCmd.Parameters.AddWithValue("@UserName", Session["MobileNo"].ToString());
            //    sqlCmd.Parameters.AddWithValue("@OTP", txtOTP.Text);
            //    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            //    DataSet dataSet = new DataSet();
            //    dataAdapter.Fill(dataSet);
            //    objDal.CloseSQLConnection();
            //    if (dataSet.Tables[0].Rows.Count > 0)
            //    {
            //        if (dataSet.Tables[0].Rows[0]["IsSuccessful"].ToString() == "1")
            //        {
            //            SqlCommand Cmd = new SqlCommand();
            //            objDal.OpenSQLConnection();
            //            Cmd.Connection = objDal.ActiveSQLConnection();
            //            Cmd.CommandType = CommandType.Text;
            //            Cmd.CommandText = "select case when isnull(Status,'P') = 'P' then 0 else 1 end as Status from Registrations where MobileNo = '" + Session["MobileNo"].ToString() + "'";
            //            bool IsApproved = Convert.ToBoolean(Cmd.ExecuteScalar());
            //            //Session["IsApproved"] = IsApproved.ToString();
            //            objDal.CloseSQLConnection();
            //            Response.Redirect("~/Guest/Registration1.aspx");
            //        }
            //        else
            //        {
            //            lblMessage.Visible = true;
            //            lblMessage.Style["color"] = "Red";
            //            if (Session["Language"].ToString() == "Gujarati")
            //            {
            //                lblMessage.Text = "અમાન્ય OTP!";
            //                lblMessage.Visible = true;
            //            }
            //            else if (Session["Language"].ToString() == "Hindi")
            //            {
            //                lblMessage.Text = "अमान्य OTP!";
            //                lblMessage.Visible = true;
            //            }
            //            else
            //            {
            //                lblMessage.Text = dataSet.Tables[0].Rows[0]["Message"].ToString();
            //            }

            //        }
            //    }

            Response.Redirect("~/Guest/Registration1.aspx");
        }
        catch (Exception ex)
        {
        }
    }

    protected void lnkResendOTP_click(object sender, EventArgs e)
    {
        try
        {
            Random generator = new Random();
            String random = generator.Next(0, 1000000).ToString("D6");
            string userName = System.Configuration.ConfigurationManager.AppSettings["SMSUserName"];
            string password = System.Configuration.ConfigurationManager.AppSettings["SMSPassword"];
            string APIkey = System.Configuration.ConfigurationManager.AppSettings["SMSAPIkey"];
            string SenderID = System.Configuration.ConfigurationManager.AppSettings["SMSSenderID"];
            string SMSType = System.Configuration.ConfigurationManager.AppSettings["SMSType"];
            string EntityID = System.Configuration.ConfigurationManager.AppSettings["SMSEntityID"];
            string TemplateID = System.Configuration.ConfigurationManager.AppSettings["SMSTemplateID"];
            string result = APIRequest1(userName, password, APIkey, SenderID, SMSType, EntityID, TemplateID, random);
            if (result.Contains("ok"))
            {
                SqlCommand sqlCmd = new SqlCommand();
                GeneralDAL objDal = new GeneralDAL();
                objDal.OpenSQLConnection();
                //sqlTrans = objDal.BeginTransaction();
                sqlCmd.Connection = objDal.ActiveSQLConnection();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "FitToJob_Master_Users";
                sqlCmd.Parameters.AddWithValue("@Action", "InsertUsers");
                sqlCmd.Parameters.AddWithValue("@UserName", Session["MobileNo"].ToString());
                sqlCmd.Parameters.AddWithValue("@OTP", random);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
            }
        }
        catch (Exception)
        {
        }
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
            string API = "http://ui.netsms.co.in/API/SendSMS.aspx?APIkey=" + APIkey + "&SenderID=" + SenderID + "&SMSType=" + SMSType + "&Mobile=" + Session["MobileNo"].ToString() + "&MsgText=" + "Your OTP for " + random + " Login is " + Session["MobileNo"].ToString() + ". - Duke Plasto" + "&EntityID=" + EntityID + "&TemplateID=" + TemplateID;
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
    protected void lnkPrint_click(object sender, EventArgs e)
    {
        try
        {
            string MobileNo = Session["MobileNo"].ToString();

            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select Top 1 userId From Users_ Where UserName = '" + MobileNo + "'";
            string UserId = sqlCmd.ExecuteScalar().ToString();

            Response.Redirect("../Report/Exam/CRViewer.aspx?RptType=InterviewFormdetial&RptId=" + UserId.ToString());
        }
        catch (Exception)
        {
        }
    }

    protected void lnkInterviewstatus_click(object sender, EventArgs e)
    {
        try
        {
            string MobileNo = Session["MobileNo"].ToString();
            Response.Redirect("../Guest/InterviewStatus.aspx?MobileNo=" + MobileNo.ToString());
        }
        catch (Exception)
        {
        }
    }

    protected void lnkInterviewUploadDocuments_click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Guest/DocumentUpload.aspx?MobileNo=" + MobileNo.ToString());
        }
        catch (Exception)
        {
        }
    }

    public DataTable ViewDataFrom_MobileNo()
    {

        SqlCommand sqlCmd = new SqlCommand();
        GeneralDAL objDal = new GeneralDAL();
        objDal.OpenSQLConnection();
        sqlCmd.Connection = objDal.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.CommandText = "FitToJob_Master_Users";
        sqlCmd.Parameters.AddWithValue("@Action", "ViewDataFrom_MobileNo");
        sqlCmd.Parameters.AddWithValue("@UserName", Session["MobileNo"]);
        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet);
        Session["IsApproved"] = dataSet.Tables[0].Rows[0]["IsInterviewStatusDone"];
        return dataSet.Tables[0];

    }

    protected void lnkbtnOffer_click(object sender, EventArgs e)
    {
        try
        {
            string MobileNo = Session["MobileNo"].ToString();
            Response.Redirect("../Guest/CheckJobStatus.aspx?MobileNo=" + MobileNo.ToString());
        }
        catch (Exception)
        {
        }
    }
}