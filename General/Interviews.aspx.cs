using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class General_Interviews : System.Web.UI.Page
{
    private InterviewsBLL _InterviewsBLL = new InterviewsBLL();
    private GeneralBLL _GeneralBLL = new GeneralBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HideErrors();
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["RptId"] != null)
                {
                    string criteria;

                    criteria = "?RptId=" + Request.QueryString["RptId"];
                    criteria += "&RptType=OfferLatter";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../Reports/Career/CRViewer.aspx" + criteria + "', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type=\"text/javascript\">CallPostBack();</script>", false);
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type=\"text/javascript\">CallPostBack();</script>", false);
        }
    }

    private void FilterCriteria()
    {
        try
        {
            if (txtFromDt.Text.Trim().Length > 0)
                _InterviewsBLL.FromDt = Convert.ToDateTime(txtFromDt.Text);
            else
                _InterviewsBLL.FromDt = null;

            if (txtToDt.Text.Trim().Length > 0)
                _InterviewsBLL.ToDt = Convert.ToDateTime(txtToDt.Text);
            else
                _InterviewsBLL.ToDt = null;

            if (txtName.Text.Trim().Length > 0)
                _InterviewsBLL.Name = txtName.Text.Trim();
            else
                _InterviewsBLL.Name = null;

            if (txtUser.Text.Trim().Length > 0)
            {
                if (hfUserId.Value != "" && hfUserId.Value != null)
                    _InterviewsBLL.UserId = hfUserId.Value;
            }
            else
            {
                _InterviewsBLL.UserId = null;
            }

            if (rdbSelected.Checked == true)
                _InterviewsBLL.Status = rdbSelected.Text;
            else if (rdbHold.Checked == true)
                _InterviewsBLL.Status = rdbHold.Text;
            else if (rdbRejected.Checked == true)
                _InterviewsBLL.Status = rdbRejected.Text;
            else if (rdbALL.Checked == true)
                _InterviewsBLL.Status = null;
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
            HideErrors();
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();
            DataTable dt = new DataTable();
            dt = _InterviewsBLL.InterviewDetail();
            gdvInterview.DataSource = dt;
            lblRecordStatus.Text = "Total No. Of Interviews : [ " + dt.Rows.Count.ToString() + " ]";
            gdvInterview.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
            HideErrors();
        }
    }
    protected void gdvInterview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                HyperLink hlReport = (HyperLink)e.Row.Cells[1].Controls[0];
                //HyperLink hlPrint = (HyperLink)e.Row.Cells[2].Controls[0];
                //HyperLink hl = (HyperLink)e.Row.Cells[3].Controls[0];
                HyperLink hlInterView = (HyperLink)e.Row.Cells[6].Controls[0];


                hlReport.NavigateUrl = "Interview.aspx?InterviewId=" + drv["InterviewId"].ToString();
                //hlReport.NavigateUrl = "../Reports/Career/CRViewer.aspx?RptType=CareerRpt&RptId=" + drv["CareerId"].ToString();
                //hlPrint.NavigateUrl = "../Reports/Career/CRViewer.aspx?RptType=OfferLatter&RptId=" + drv["InterviewId"].ToString();
                //hl.NavigateUrl = "Interview.aspx?InterviewId=" + drv[0].ToString();
                hlInterView.NavigateUrl = "../General/HODInterview.aspx?CandidateName=" + drv["Name"].ToString() + "&RegistrationId=" + drv["RegistrationId"].ToString() + "&Status=" + drv["Status"].ToString() + "";
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
            HideErrors();
        }
    }
    protected void gdvInterview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            FilterCriteria();
            gdvInterview.PageIndex = e.NewPageIndex;
            gdvInterview.DataSource = _InterviewsBLL.InterviewDetail();
            gdvInterview.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
            HideErrors();
        }
    }

    private void ShowErrors(string key, string value)
    {

        if (key == "Success")
            pnlErr.CssClass = "errors alert alert-success";
        else
            pnlErr.CssClass = "errors alert alert-danger";

        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();
    }
}