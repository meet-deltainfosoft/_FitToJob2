using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Net;
using System.IO;

public partial class Login : System.Web.UI.Page
{
    private LoginBLL _LoginBLL = new LoginBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            //<%--Version : 17.07.01 V1 Dated : 17-Jul-2020 DT+QB--%>
            //      <%--10.07.01 V3 Dated : 10-Jul-2020 for 100--%>
            //       <%--Version : 11.05.03 V3 Dated : 12-May-2020--%>

            ///lblVersion.Text = "Version : 03.03.23 Dated : 20-Feb-2024 SkpALAdv";

            LoadYear();
            ddlYear_SelectedIndexChanged(null, null);
            txtUserName.Focus();
        }
    }

    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: This call is required by the ASP.NET Web Form Designer.
        //
        //InitializeComponent();
        //base.OnInit(e);
    }

    private void InitializeComponent()
    {
        this.cmdSubmit.Click += new System.EventHandler(this.cmdSubmit_Click);
        this.Load += new System.EventHandler(this.Page_Load);

    }

    protected void cmdSubmit_Click(object sender, System.EventArgs e)
    {
        string result = "";
        Random generator = new Random();
        String random = generator.Next(0, 1000000).ToString("D6");
        string userName = System.Configuration.ConfigurationManager.AppSettings["SMSUserName"];
        string password = System.Configuration.ConfigurationManager.AppSettings["SMSPassword"];
        string APIkey = System.Configuration.ConfigurationManager.AppSettings["SMSAPIkey"];
        string SenderID = System.Configuration.ConfigurationManager.AppSettings["SMSSenderID"];
        string SMSType = System.Configuration.ConfigurationManager.AppSettings["SMSType"];
        string EntityID = System.Configuration.ConfigurationManager.AppSettings["SMSEntityID"];
        string TemplateID = System.Configuration.ConfigurationManager.AppSettings["SMSTemplateID"];
        result = APIRequest1(userName, password, APIkey, SenderID, SMSType, EntityID, TemplateID, random);
        if (result.Contains("ok"))
        {

            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Master_Users";
            sqlCmd.Parameters.AddWithValue("@Action", "InsertUsers");
            sqlCmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
            sqlCmd.Parameters.AddWithValue("@OTP", random.ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                txtOTP.Style["display"] = "block";
                txtUserName.Enabled = false;
                cmdSubmit.Style["display"] = "none";
                btnVerifyOTP.Style["display"] = "block";
                lblEnterOTP.Style["display"] = "block";
                //Response.Redirect("VerifyOTP.aspx");
            }
            objDal.CloseSQLConnection();
        }
        else
        {
            lblMessage.Text = "Please Enter Valid Mobile Number";
        }
    }

    private void LoadYear()
    {
        ListItem li = new ListItem();

        ddlYear.Items.Clear();
        foreach (DataRow dtr in _LoginBLL.GetYear().Rows)
        {
            li = new ListItem();

            li.Text = dtr[1].ToString();
            li.Value = dtr[0].ToString();
            ddlYear.Items.Add(li);

            li = null;
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        foreach (DataRow dtr in _LoginBLL.getFrmDateToDate(ddlYear.SelectedItem.ToString()).Rows)
        {
            MySession.FromDate = Convert.ToDateTime(dtr[0].ToString());
            MySession.ToDate = Convert.ToDateTime(dtr[1].ToString());
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
            string API = "http://ui.netsms.co.in/API/SendSMS.aspx?APIkey=" + APIkey + "&SenderID=" + SenderID + "&SMSType=" + SMSType + "&Mobile=" + txtUserName.Text + "&MsgText=" + "Your OTP for " + random + " Login is " + txtUserName.Text + ". - Duke Plasto" + "&EntityID=" + EntityID + "&TemplateID=" + TemplateID;
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

    protected void btnVerifyOTP_Click(object sender, System.EventArgs e)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandText = "FitToJob_Master_Users";
            sqlCmd.Parameters.AddWithValue("@Action", "VerfiyOTP");
            sqlCmd.Parameters.AddWithValue("@UserName", txtUserName.Text);
            sqlCmd.Parameters.AddWithValue("@OTP", txtOTP.Text);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                if (dataSet.Tables[0].Rows[0]["IsSuccessful"].ToString() == "1")
                {
                    Session["UserName"] = txtUserName.Text;
                    Response.Redirect("VerifyOTP.aspx");
                }
            }
            objDal.CloseSQLConnection();
        }

        catch (Exception)
        {
        }
    }
}
