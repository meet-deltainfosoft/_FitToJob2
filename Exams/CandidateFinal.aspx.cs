using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Net.Mail;


public partial class Exams_CandidateFinal : System.Web.UI.Page
{
    private CandidateFinalBll _candidateFinalBll = new CandidateFinalBll();
    private StringBuilder sbjQueryNumeric = new StringBuilder();
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
                _candidateFinalBll.FromDt = Convert.ToDateTime(txtFromDt.Text);
            else
                _candidateFinalBll.FromDt = null;

            if (txtToDt.Text.Trim().Length > 0)
                _candidateFinalBll.ToDt = Convert.ToDateTime(txtToDt.Text);
            else
                _candidateFinalBll.ToDt = null;

            if (txtName.Text.Trim().Length > 0)
                _candidateFinalBll.Name = txtName.Text.Trim();
            else
                _candidateFinalBll.Name = null;

            if (txtUser.Text.Trim().Length > 0)
            {
                if (hfUserId.Value != "" && hfUserId.Value != null)
                    _candidateFinalBll.UserId = hfUserId.Value;
            }
            else
            {
                _candidateFinalBll.UserId = null;
            }

            if (rdbSelected.Checked == true)
                _candidateFinalBll.Status = "A";
            else if (rdbHold.Checked == true)
                _candidateFinalBll.Status = "D";
            else if (rdbALL.Checked == true)
                _candidateFinalBll.Status = null;
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
            dt = _candidateFinalBll.CandidateFinal();
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
                
                 // HyperLink hlPrint = (HyperLink)e.Row.Cells[0].Controls[0];
                 // HyperLink hl = (HyperLink)e.Row.Cells[1].Controls[0];
                

               
                //  hlPrint.NavigateUrl = "../Report/Exam/CRViewer.aspx?RptType=OfferLatter&RptId=" + drv["RegistrationId"].ToString();
                //  hl.NavigateUrl = "../Report/Exam/CRViewer.aspx?RptType=AppointmentLatter&RptId=" + drv["RegistrationId"].ToString();
              
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
            gdvInterview.DataSource = _candidateFinalBll.CandidateFinal();
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

    protected void ddlApprovedDisapproved_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void btnUpdateStatus_Click(object sender, EventArgs e)
    {
        //chkSelectAll1.Checked = false;
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        HideErrors();

        ArrayList alIndents = new ArrayList();
        bool disaprvl = false;
        bool approv = false;
        bool reject = false;

        try
        {
            DataTable dtEmpList = new DataTable();
            DataTable dtEmpVoucherDetail = new DataTable();

            dtEmpList.Columns.Add("UserId");


            dtEmpVoucherDetail.Columns.Add("UserId");
            dtEmpVoucherDetail.Columns.Add("VoucherNo");
            dtEmpVoucherDetail.Columns.Add("VoucherDt");
            dtEmpVoucherDetail.Columns.Add("MobileNo");
            dtEmpVoucherDetail.Columns.Add("Status");

            foreach (GridViewRow gvr in gdvInterview.Rows)
            {
                DropDownList ddlApprovedDisapproved = (DropDownList)gvr.FindControl("ddlApprovedDisapproved");
                TextBox txtRemarks = (TextBox)gvr.FindControl("txtRemarks");
                //TextBox txtRejRemarks = (TextBox)gvr.FindControl("txtReject");

                if (ddlApprovedDisapproved.Visible == true)
                {
                    if (ddlApprovedDisapproved.SelectedIndex > 0)
                    {
                        alIndents.Add(gdvInterview.DataKeys[gvr.RowIndex].Values[0].ToString() + "|" + ddlApprovedDisapproved.SelectedValue + "|" + txtRemarks.Text);
                        approv = true;
                    }
                }
            }
            if (alIndents.Count > 0)
            {
                _candidateFinalBll.Update(alIndents);

                sbjQueryNumeric.AppendLine("<script type='text/javascript'>");
                sbjQueryNumeric.AppendLine("j(document).ready(function() {");

                gdvInterview.DataSource = _candidateFinalBll.CandidateFinalNot();
                gdvInterview.DataBind();

                sbjQueryNumeric.AppendLine(" });");
                sbjQueryNumeric.AppendLine("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "A", sbjQueryNumeric.ToString());

                if (disaprvl == true && approv == false)
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>ShowStatus('Record has been DisApproved...');</script>", false);
                else if (approv == true && disaprvl == false)
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>ShowStatus('Record has been Approved...');</script>", false);
                else if (approv == true && disaprvl == true)
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>ShowStatus('Record has been Approved & DisApproved...');</script>", false);
            }
            else
            {
                sbjQueryNumeric.AppendLine("<script type='text/javascript'>");
                sbjQueryNumeric.AppendLine("j(document).ready(function() {");

                gdvInterview.DataSource = _candidateFinalBll.CandidateFinalNot();
                gdvInterview.DataBind();

                sbjQueryNumeric.AppendLine(" });");
                sbjQueryNumeric.AppendLine("</script>");
                ClientScript.RegisterClientScriptBlock(this.GetType(), "A", sbjQueryNumeric.ToString());

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>ShowStatus('Select atleast one record...');</script>", false);
            }
        }
        catch (Exception ex)
        {
            pnlErr.Visible = true;
            blErrs.Items.Add(ex.Message);
        }
    }
}