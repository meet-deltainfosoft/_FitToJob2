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

        try
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                lblMessage.Text = "Password and Confirm Password Not Matched";
            }
            else
            {

                SqlCommand sqlCmd = new SqlCommand();
                GeneralDAL objDal = new GeneralDAL();
                objDal.OpenSQLConnection();
                sqlCmd.Connection = objDal.ActiveSQLConnection();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "FitToJob_Android_Application";
                sqlCmd.Parameters.AddWithValue("@Action", "UpdatePassword");
                sqlCmd.Parameters.AddWithValue("@MobileNo", Session["UserName"].ToString());
                sqlCmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
        catch (Exception)
        {
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
        return "";

    }
}
