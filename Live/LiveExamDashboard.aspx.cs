using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Live_LiveExamDashboard : System.Web.UI.Page
{
    private ResultDetailBLL _ResultDetailBLL = new ResultDetailBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
        else
        {
        }
    }
    private void ShowErrors(string key, string value)
    {
        try
        {
            if (key == "Success")
                pnlErr.CssClass = "errors alert alert-success";
            else
                pnlErr.CssClass = "errors alert alert-danger";

            pnlErr.Visible = true;
            blErrs.Items.Add(new ListItem(value));
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    private void HideErrors()
    {
        try
        {
            pnlErr.Visible = false;
            blErrs.Items.Clear();
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
}