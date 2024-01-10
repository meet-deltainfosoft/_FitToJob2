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

public partial class Exams_GenerateCall : System.Web.UI.Page
{
    private GenerateCallBLL _generateCallBLL = new GenerateCallBLL();
    private StringBuilder sbjQueryNumeric = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        btnShowAllRecords.Visible = false;
        if (Page.IsPostBack == false)
        {
            LoadDepartment();
           // LoadDesignation();
            LoadDivision();
        }
    }

    private void LoadDivision()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDivision.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlDivision.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _generateCallBLL.LoadDivision().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlDivision.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }

    private void LoadDepartment()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDepartment.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlDepartment.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _generateCallBLL.LoadDepartment().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlDepartment.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }

    private void LoadDesignation()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDesignation.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlDesignation.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _generateCallBLL.LoadDesignation().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlDesignation.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        FilterCriteria();

        _generateCallBLL.AllRecord = false;

        DataTable dt = new DataTable();
        dt = _generateCallBLL.GenerateCall();
        gdvGenerateCall.DataSource = dt;

        if (dt.Rows.Count >= 30)
        {
            btnShowAllRecords.Visible = true;
            lblRecordStatus.Text = "Top  [ " + dt.Rows.Count.ToString() + " ]" + " Records ";
        }
        else
        {
            btnShowAllRecords.Visible = false;
            lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";
        }

        gdvGenerateCall.DataBind();

    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();
            ExportToExcel();
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }

    protected void ExportToExcel()
    {
        try
        {
            DataTable dtExport = new DataTable();
            _generateCallBLL.AllRecord = true;
            dtExport = _generateCallBLL.GenerateCall();

            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();

            DataView dv = new DataView(dtExport);
            dgGrid.DataSource = dv;
            dtExport.Columns.Remove("RegistrationId");
            //dtExport.Columns.Remove("StandardId");
            if (dtExport.Rows.Count > 0)
            {
                hw.WriteLine("<table cellspacing='0' cellpadding='4' border='1' style='font-size:10pt;width:100%;border-collapse:collapse;'>");
                hw.WriteLine("<tr>");
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:21px;font-family:Verdana;background-color:#c5d9f1;'><b> Generate Call Report </b> </td>");
                hw.WriteLine("</tr>");
                hw.WriteLine("<tr>");
                foreach (DataColumn Columns in dtExport.Columns)
                {
                    hw.WriteLine("<td align='center' valign='top' style='border-color:black;font-size:14px;font-family:Verdana;'><b> " + Columns.ColumnName.ToString() + "</b></td>");
                }
                hw.WriteLine("</tr>");

                for (int i = 0; i < dtExport.Rows.Count; i++)
                {
                    hw.WriteLine("<tr>");
                    for (int j = 0; j < dtExport.Columns.Count; j++)
                    {
                        if (dtExport.Rows[i][j] != DBNull.Value)
                        {
                            if (dtExport.Columns[j].DataType == typeof(System.Decimal))
                                hw.WriteLine("<td align='right' valign='top' style='border-color:black;font-size:13px;font-family:Verdana;'>" + Convert.ToDecimal(dtExport.Rows[i][j]).ToString("0.00") + "</td>");
                            else
                                hw.WriteLine("<td align='left' valign='top' style='border-color:black;font-size:13px;font-family:Verdana;'>" + dtExport.Rows[i][j].ToString() + "</td>");
                        }
                        else
                            hw.WriteLine("<td align='left' valign='top' style='border-color:black;font-size:13px;font-family:Verdana;'></td>");
                    }
                    hw.WriteLine("</tr>");
                }
            }
            else
            {
                hw.WriteLine("<tr>");
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:13px;font-family:Verdana;'> No Records Found.</td>");
                hw.WriteLine("</tr>");
            }
            hw.WriteLine("</table>");
            string attachment = "attachment; filename=GenerateCall.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            Response.Write(tw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    protected void btnShowAllRecords_Click(object sender, EventArgs e)
    {
        FilterCriteria();

        _generateCallBLL.AllRecord = true;

        DataTable dt = new DataTable();
        dt = _generateCallBLL.GenerateCall();
        gdvGenerateCall.DataSource = dt;

        lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";

        gdvGenerateCall.DataBind();

    }
    protected void gdvGenerateCall_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            //HyperLink hlPrint = (HyperLink)e.Row.Cells[0].Controls[0];
            //HyperLink hlSendMail = (HyperLink)e.Row.Cells[11].Controls[0];
            //btnExcel_Click(null, null);
            //string RegistrationId;
            //RegistrationId = drv[0].ToString();
            //_generateCallBLL.Update(RegistrationId);
            //hlPrint.NavigateUrl = "../Report/Exam/CRViewer.aspx?RptType=OfferLatter&RptId=" + drv[0].ToString();

           // hlPrint.NavigateUrl = "../Report/Exam/CRViewer.aspx?RptType=OfferLatter&RptId=" + drv["RegistrationId"].ToString() + " + &Date=" + txtFromDt.Text + " + &JobProfile=" + ddlDesignation.SelectedItem.Text + "";
            // hlSendMail.NavigateUrl = "../Report/Exam/CRViewer.aspx?RptType=OfferLatter&RptId=" + drv["RegistrationId"].ToString() + " + &Date=" + txtFromDt.Text + " + &JobProfile=" + ddlDesignation.SelectedItem.Text + "";
        }
    }

    private void ShowErrors(string key, string value)
    {
        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();
    }



    private void FilterCriteria()
    {
        try
        {
            if (ddlDepartment.SelectedIndex > 0)
                _generateCallBLL.DepartmentId = ddlDepartment.SelectedValue;
            else
                _generateCallBLL.DepartmentId = null;

            if (ddlDivision.SelectedIndex > 0)
                _generateCallBLL.DivisionId = ddlDivision.SelectedValue;
            else
                _generateCallBLL.DivisionId = null;

            if (ddlDesignation.SelectedIndex > 0)
                _generateCallBLL.DesignationId = ddlDesignation.SelectedValue;
            else
                _generateCallBLL.DesignationId = null;

            if (txtName.Text.Trim().Length > 0)
                _generateCallBLL.Name = txtName.Text.Trim();
            else
                _generateCallBLL.Name = null;

            if (txtMobileNo.Text.Trim().Length > 0)
                _generateCallBLL.MobileNo = txtMobileNo.Text.Trim();
            else
                _generateCallBLL.MobileNo = null;

            if (txtCity.Text.Trim().Length > 0)
                _generateCallBLL.City = txtCity.Text.Trim();
            else
                _generateCallBLL.City = null;

        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    protected void btnGenerateCall_OnClick(object sender, EventArgs e)
    {
        Button btnGenerateCall = (Button)sender;
        GridViewRow Paste = btnGenerateCall.NamingContainer as GridViewRow;
        int Pasteindex = Paste.DataItemIndex;

        string RegistrationId = gdvGenerateCall.DataKeys[Pasteindex].Value.ToString();
        
        _generateCallBLL.Update(RegistrationId);
        MailMessage mm = new MailMessage();
        try
        {
            GeneralDAL generalDAL = new GeneralDAL();
            try
            {
                _generateCallBLL.SendEmail(RegistrationId);
            }
            catch (Exception ex)
            {
                if (ex.Message.Length > 100)
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>ShowStatus('Mail Not Send. Reason :" + ex.Message.Substring(0, 90) + "...');</script>", false);
                else
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>ShowStatus('Mail Not Send. Reason :" + ex.Message + "');</script>", false);
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>ShowStatus('Mail Not Send because No Parect Party's Contact Person's Email Found.');</script>", false);
                }
            }
        }
        catch (Exception ex)
        {
            if (mm.Attachments.Count > 0)
            {
                mm.Attachments.Dispose();
            }
            throw new Exception("Sending Mail failed...");
        }

        //string criteria;

        //criteria = "?RptId=" + RegistrationId;
        //criteria += "&RptType=OfferLatter";

        //string redirect = "";
        //redirect = "../Report/Exam/CRViewer.aspx?RptType=OfferLatter&RptId='" + RegistrationId + "'";
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + redirect + "&Excel=0','_newtab');", true);
        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script> window.open('../Report/Exam/CRViewer.aspx" + criteria + "','null','height=600px,width=800px,toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars=1,copyhistory=0,resizable =1,left=250,top=80');</script>", false);
    }
    protected void btnSendMail_OnClick(object sender, EventArgs e)
    {
        ImageButton btnSendMail = (ImageButton)sender;
        GridViewRow Paste = btnSendMail.NamingContainer as GridViewRow;
        int Pasteindex = Paste.DataItemIndex;

        string RegistrationId = gdvGenerateCall.DataKeys[Pasteindex].Value.ToString();


        try
        {
            HideErrors();
            {
                MailMessage mm = new MailMessage();
                try
                {
                    GeneralDAL generalDAL = new GeneralDAL();

                    try
                    {
                        _generateCallBLL.SendEmail(RegistrationId);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Length > 100)
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>ShowStatus('Mail Not Send. Reason :" + ex.Message.Substring(0, 90) + "...');</script>", false);
                        else
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>ShowStatus('Mail Not Send. Reason :" + ex.Message + "');</script>", false);
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>ShowStatus('Mail Not Send because No Parect Party's Contact Person's Email Found.');</script>", false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (mm.Attachments.Count > 0)
                    {
                        mm.Attachments.Dispose();
                    }
                    throw new Exception("Sending Mail failed...");
                }
            }

        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    protected void ddlStdId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedIndex > 0)
            LoadSubjects();
        else
            ShowErrors("err", "Please select standard");
    }

    private void LoadSubjects()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDesignation.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlDesignation.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _generateCallBLL.LoadSubjects(((ddlDepartment.SelectedIndex > 0) ? ddlDepartment.SelectedValue : null));

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlDesignation.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

}