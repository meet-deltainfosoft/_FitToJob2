using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class General_InterViewForms : System.Web.UI.Page
{

    #region Declaration
    InterViewFormsBLL InterViewFormsBLL = new InterViewFormsBLL();
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                txtAadharNo.Focus();
            }
            else
            {
                HideErrors();
            }
            btnShowAllRecords.Visible = false;
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    #endregion

    #region Button CLick Events
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();
            InterViewFormsBLL.AllRecords = false;

            DataTable dt = new DataTable();
            dt = InterViewFormsBLL.InterView();
            gdvInterview.DataSource = dt;
            gdvInterview.DataBind();
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
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    protected void btnShowAllRecords_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();
            InterViewFormsBLL.AllRecords = true;

            DataTable dt = new DataTable();
            dt = InterViewFormsBLL.InterView();
            gdvInterview.DataSource = dt;
            gdvInterview.DataBind();
            lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
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
    #endregion

    #region Functions
    private void FilterCriteria()
    {
        try
        {

            if (txtAadharNo.Text.Trim().Length > 0)
                InterViewFormsBLL.AadharNo = txtAadharNo.Text.Trim();
            else
                InterViewFormsBLL.AadharNo = null;

            if (txtFirstname.Text.Trim().Length > 0)
                InterViewFormsBLL.FirstName = txtFirstname.Text.Trim();
            else
                InterViewFormsBLL.FirstName = null;

            if (txtDOB.Text.Trim().Length > 0)
                InterViewFormsBLL.DOB = Convert.ToDateTime(txtDOB.Text.Trim());
            else
                InterViewFormsBLL.DOB = null;

            if (txtPanCardNo.Text.Trim().Length > 0)
                InterViewFormsBLL.PanCardNo = txtPanCardNo.Text.Trim();
            else
                InterViewFormsBLL.PanCardNo = null;

            if (txtMobileNo.Text.Trim().Length > 0)
                InterViewFormsBLL.MobileNo = txtMobileNo.Text.Trim();
            else
                InterViewFormsBLL.MobileNo = null;

        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
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
            ShowErrors("error", ex.Message);
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
            ShowErrors("error", ex.Message);
        }
    }
    protected void ExportToExcel()
    {
        try
        {
            DataTable dtExport = new DataTable();
            InterViewFormsBLL.AllRecords = true;
            dtExport = InterViewFormsBLL.InterView();
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            DataView dv = new DataView(dtExport);
            dtExport.Columns.Remove("InterviewFormId");
            dgGrid.DataSource = dv;
            hw.WriteLine("<table cellspacing='0' cellpadding='4' border='1' style='font-size:10pt;width:100%;border-collapse:collapse;'>");
            hw.WriteLine("<tr>");
            hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:21px;font-family:Verdana;background-color:#c5d9f1;'><b> Interview Form Report </b> </td>");
            hw.WriteLine("</tr>");
            if (txtAadharNo.Text.Trim().Length > 0)
            {
                hw.WriteLine("<tr>");
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:16px;'><b> AadharCardNo:" + txtAadharNo.Text + "</b> </td>");
                hw.WriteLine("</tr>");
            }
            if (txtFirstname.Text.Trim().Length > 0)
            {
                hw.WriteLine("<tr>");
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:16px;'><b> FirstName:" + txtFirstname.Text + "</b> </td>");
                hw.WriteLine("</tr>");
            }
            if (txtPanCardNo.Text.Trim().Length > 0)
            {
                hw.WriteLine("<tr>");
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:16px;'><b> MiddleName:" + txtPanCardNo.Text + "</b> </td>");
                hw.WriteLine("</tr>");
            }
            if (txtDOB.Text.Trim().Length > 0)
            {
                hw.WriteLine("<tr>");
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:16px;'><b> LastName:" + txtDOB.Text + "</b> </td>");
                hw.WriteLine("</tr>");
            }
            if (txtMobileNo.Text.Trim().Length > 0)
            {
                hw.WriteLine("<tr>");
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:16px;'><b> MobileNo:" + txtMobileNo.Text + "</b> </td>");
                hw.WriteLine("</tr>");
            }
            if (dtExport.Rows.Count > 0)
            {
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
            string attachment = "attachment; filename=InteViewFromList.xls";
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
    #endregion

    #region Grid Events
    protected void gdvInterview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                HyperLink hl = (HyperLink)e.Row.Cells[2].Controls[0];
                HyperLink hlPrint = (HyperLink)e.Row.Cells[1].Controls[0];
                hl.NavigateUrl = "InterViewForm.aspx?InterviewFormId=" + drv["InterviewFormId"].ToString();
                hlPrint.NavigateUrl = "../Report/Exam/CRViewer.aspx?RptType=InterViewForm&RptId=" + drv["RegistrationId"].ToString();
                HyperLink hlSalarySetup = (HyperLink)e.Row.Cells[3].Controls[0];
                hlSalarySetup.NavigateUrl = "../Report/Exam/CRViewer.aspx?RptType=OfferLatter&RptId=" + drv["RegistrationId"].ToString();
                //HyperLink hlLeaveAllocation = (HyperLink)e.Row.Cells[10].Controls[0];
                //hlLeaveAllocation.NavigateUrl = "../HR/EmpLeaveAllocation.aspx?EmployeeId=" + drv["EmployeeId"].ToString();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }

    }
    protected void gdvInterview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            FilterCriteria();
            InterViewFormsBLL.AllRecords = true;

            gdvInterview.PageIndex = e.NewPageIndex;
            gdvInterview.DataSource = InterViewFormsBLL.InterView();
            gdvInterview.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    #endregion
}