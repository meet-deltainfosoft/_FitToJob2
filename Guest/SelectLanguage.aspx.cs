using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Globalization;  

public partial class Guest_SelectLanguage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void lnkBtnLanguage_Click(object sender, EventArgs e)
    {
        Session["Language"] = ddlSelectLanguage.SelectedValue.ToString();
        Response.Redirect("~/Guest/SignIn.aspx");
    }
}