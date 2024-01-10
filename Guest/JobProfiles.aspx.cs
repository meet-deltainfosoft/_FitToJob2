using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class General_JobProfiles : System.Web.UI.Page
{
    #region Declaration
    JobProfilesBLL _jobProfilesBLL = new JobProfilesBLL();
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                //LoadDesignations();
                LoadDivision();
                LoadDepartment();
                LoadStafcategory();
                ddlDepartmentId.Focus();
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
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();
            _jobProfilesBLL.AllRecords = false;

            DataTable dt = new DataTable();
            dt = _jobProfilesBLL.JobProfiles();
            gdvEmps.DataSource = dt;
            gdvEmps.DataBind();
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

    #region Functions
    private void FilterCriteria()
    {
        try
        {
            if (ddlDepartmentId.SelectedIndex > 0)
                _jobProfilesBLL.DepartmentId = ddlDepartmentId.SelectedValue;
            else
                _jobProfilesBLL.DepartmentId = null;

            if (ddlDivisionId.SelectedIndex > 0)
                _jobProfilesBLL.DivisionId = ddlDivisionId.SelectedValue;
            else
                _jobProfilesBLL.DivisionId = null;

            if (ddlDesignationId.SelectedIndex > 0)
                _jobProfilesBLL.DesignationId = ddlDesignationId.SelectedValue;
            else
                _jobProfilesBLL.DesignationId = null;

            if (ddlStaffCategoryId.SelectedIndex > 0)
                _jobProfilesBLL.StaffCategoryTextListId = ddlStaffCategoryId.SelectedValue;
            else
                _jobProfilesBLL.StaffCategoryTextListId = null;


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
            _jobProfilesBLL.AllRecords = true;
            dtExport = _jobProfilesBLL.JobProfiles();
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();
            DataView dv = new DataView(dtExport);
            dtExport.Columns.Remove("JobOfferingId");
            dtExport.Columns.Remove("NoOfSeats");
            dtExport.Columns.Remove("ValidFrom");
            dtExport.Columns.Remove("ValidTo");
            dgGrid.DataSource = dv;
            hw.WriteLine("<table cellspacing='0' cellpadding='4' border='1' style='font-size:10pt;width:100%;border-collapse:collapse;'>");
            hw.WriteLine("<tr>");
            hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:21px;font-family:Verdana;background-color:#c5d9f1;'><b> Job Profile Report </b> </td>");
            hw.WriteLine("</tr>");
            //if (txtAadharNo.Text.Trim().Length > 0)
            //{
            //    hw.WriteLine("<tr>");
            //    hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:16px;'><b> AadharCardNo:" + txtAadharNo.Text + "</b> </td>");
            //    hw.WriteLine("</tr>");
            //}
            //if (txtFirstname.Text.Trim().Length > 0)
            //{
            //    hw.WriteLine("<tr>");
            //    hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:16px;'><b> FirstName:" + txtFirstname.Text + "</b> </td>");
            //    hw.WriteLine("</tr>");
            //}
            //if (txtMiddlename.Text.Trim().Length > 0)
            //{
            //    hw.WriteLine("<tr>");
            //    hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:16px;'><b> MiddleName:" + txtMiddlename.Text + "</b> </td>");
            //    hw.WriteLine("</tr>");
            //}
            //if (txtLastname.Text.Trim().Length > 0)
            //{
            //    hw.WriteLine("<tr>");
            //    hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:16px;'><b> LastName:" + txtLastname.Text + "</b> </td>");
            //    hw.WriteLine("</tr>");
            //}
            //if (txtMobileNo.Text.Trim().Length > 0)
            //{
            //    hw.WriteLine("<tr>");
            //    hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:16px;'><b> MobileNo:" + txtMobileNo.Text + "</b> </td>");
            //    hw.WriteLine("</tr>");
            //}
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
            string attachment = "attachment; filename=JobProfilesList.xls";
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

    protected void btnShowAllRecords_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();
            _jobProfilesBLL.AllRecords = true;

            DataTable dt = new DataTable();
            dt = _jobProfilesBLL.JobProfiles();
            gdvEmps.DataSource = dt;
            gdvEmps.DataBind();
            lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }

    #region Grid Events
    protected void gdvEmps_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                HyperLink hl = (HyperLink)e.Row.Cells[1].Controls[0];
                hl.NavigateUrl = "JobProfile.aspx?JobOfferingId=" + drv["JobOfferingId"].ToString();
                //HyperLink hlSalarySetup = (HyperLink)e.Row.Cells[9].Controls[0];
                //hlSalarySetup.NavigateUrl = "../General/EmployeeSalary.aspx?EmployeeId=" + drv["EmployeeId"].ToString();
                //HyperLink hlLeaveAllocation = (HyperLink)e.Row.Cells[10].Controls[0];
                //hlLeaveAllocation.NavigateUrl = "../HR/EmpLeaveAllocation.aspx?EmployeeId=" + drv["EmployeeId"].ToString();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }

    }
    protected void gdvEmps_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            FilterCriteria();
            _jobProfilesBLL.AllRecords = true;

            gdvEmps.PageIndex = e.NewPageIndex;
            gdvEmps.DataSource = _jobProfilesBLL.JobProfiles();
            gdvEmps.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    #endregion

    #region Load Dropdown

    private void LoadDesignations()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDesignationId.Items.Clear();
            li.Text = "<Select>";
            li.Value = "0";
            ddlDesignationId.Items.Add(li);
            li = null;

            DataTable dt = new DataTable();
            dt = _jobProfilesBLL.GetDesignations(((ddlDepartmentId.SelectedIndex > 0) ? ddlDepartmentId.SelectedValue : null));

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();
                li.Text = dtr["Name"].ToString();
                li.Value = dtr["SubId"].ToString();
                ddlDesignationId.Items.Add(li);
                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    private void LoadDivision()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDivisionId.Items.Clear();
            li.Text = "<Select>";
            li.Value = "0";
            ddlDivisionId.Items.Add(li);
            li = null;

            foreach (DataRow dtr in _jobProfilesBLL.GetDivision().Rows)
            {
                li = new ListItem();
                li.Text = dtr["Text"].ToString();
                li.Value = dtr["TextListId"].ToString();
                ddlDivisionId.Items.Add(li);
                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    private void LoadDepartment()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDepartmentId.Items.Clear();
            li.Text = "<Select>";
            li.Value = "0";
            ddlDepartmentId.Items.Add(li);
            li = null;

            foreach (DataRow dtr in _jobProfilesBLL.GetDepartment().Rows)
            {
                li = new ListItem();
                li.Text = dtr["Text"].ToString();
                li.Value = dtr["TextListId"].ToString();
                ddlDepartmentId.Items.Add(li);
                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    private void LoadStafcategory()
    {
        try
        {
            ListItem li = new ListItem();

            ddlStaffCategoryId.Items.Clear();
            li.Text = "<Select>";
            li.Value = "0";
            ddlStaffCategoryId.Items.Add(li);
            li = null;

            foreach (DataRow dtr in _jobProfilesBLL.GetStafcategory().Rows)
            {
                li = new ListItem();
                li.Text = dtr["Text"].ToString();
                li.Value = dtr["TextListId"].ToString();
                ddlStaffCategoryId.Items.Add(li);
                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    #endregion

    protected void ddlStdId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartmentId.SelectedIndex > 0)
            LoadDesignations();
        else
            ShowErrors("err", "Please select standard");
    }
}