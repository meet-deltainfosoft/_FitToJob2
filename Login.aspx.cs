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

            lblVersion.Text = "Version : 03.03.23 Dated : 03-Mar-2023 SkpALAdv";

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
        if (Page.IsValid)
        {
            _LoginBLL.Login = txtUserName.Text.Trim();
            _LoginBLL.Password = txtPassword.Text.Trim();

            if (_LoginBLL.ValidateUser)
            {
                Session["UserLogin"] = txtUserName.Text;
                MySession.UserID = txtUserName.Text;
                MySession.UserUnique = _LoginBLL.GetUniqueCode;
                MySession.IsAdmin = _LoginBLL.GetIsAdmin;

                FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, false);
            }
            else
            {
                System.Web.HttpContext.Current.Session["UserLogin"] = "";
                txtUserName.Text = "";
                lblMessage.Text = "Invalid Login, please try again!";
                txtUserName.Focus();
            }
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
}
