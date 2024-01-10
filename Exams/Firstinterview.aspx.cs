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


public partial class Exams_First_interview : System.Web.UI.Page
{
    private FirstInterViewBLL _firstInterViewBLL = new FirstInterViewBLL();
    private StringBuilder sbjQueryNumeric = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        btnShowAllRecords.Visible = false;
        if (Page.IsPostBack == false)
        {
            LoadDepartment();
            //LoadDesignation();
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

            foreach (DataRow dtr in _firstInterViewBLL.LoadDivision().Rows)
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

            foreach (DataRow dtr in _firstInterViewBLL.LoadDepartment().Rows)
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

            foreach (DataRow dtr in _firstInterViewBLL.LoadDesignation().Rows)
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

        _firstInterViewBLL.AllRecord = false;

        DataTable dt = new DataTable();
        dt = _firstInterViewBLL.FirstInterview();
        gdvFirstInterview.DataSource = dt;

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

        gdvFirstInterview.DataBind();

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
            _firstInterViewBLL.AllRecord = true;
            dtExport = _firstInterViewBLL.FirstInterview();

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
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:21px;font-family:Verdana;background-color:#c5d9f1;'><b> First Interview Report </b> </td>");
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
            string attachment = "attachment; filename=FirstInterviewList.xls";
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

        _firstInterViewBLL.AllRecord = true;

        DataTable dt = new DataTable();
        dt = _firstInterViewBLL.FirstInterview();
        gdvFirstInterview.DataSource = dt;

        lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";

        gdvFirstInterview.DataBind();

    }
    protected void gdvFirstInterview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hlPhoto = (HyperLink)e.Row.Cells[10].Controls[0];
            HyperLink hlSelfIntor = (HyperLink)e.Row.Cells[11].Controls[0];
            HyperLink hlDownload = (HyperLink)e.Row.Cells[12].Controls[0];
            HyperLink hlInterView = (HyperLink)e.Row.Cells[13].Controls[0];
            hlPhoto.NavigateUrl = drv["Photo"].ToString().Replace("" + ConfigurationSettings.AppSettings["CandiPath"] + "", "" + ConfigurationSettings.AppSettings["CandiPathShow"] + "");
            hlSelfIntor.NavigateUrl = drv["SelfIntro"].ToString().Replace("" + ConfigurationSettings.AppSettings["CandiPath"] + "", "" + ConfigurationSettings.AppSettings["CandiPathShow"] + "");
            hlDownload.NavigateUrl = drv["Resume"].ToString().Replace("" + ConfigurationSettings.AppSettings["CandiPath"] + "", "" + ConfigurationSettings.AppSettings["CandiPathShow"] + "");
            hlInterView.NavigateUrl = "../General/Interview.aspx?CandidateName=" + drv["Name"].ToString() + "&RegistrationId=" + drv["RegistrationId"].ToString() + "";
            DropDownList ddlApprovedDisapproved = (DropDownList)e.Row.FindControl("ddlApprovedDisapproved");
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
                _firstInterViewBLL.DepartmentId = ddlDepartment.SelectedValue;
            else
                _firstInterViewBLL.DepartmentId = null;

            if (ddlDivision.SelectedIndex > 0)
                _firstInterViewBLL.DivisionId = ddlDivision.SelectedValue;
            else
                _firstInterViewBLL.DivisionId = null;

            if (ddlDesignation.SelectedIndex > 0)
                _firstInterViewBLL.DesignationId = ddlDesignation.SelectedValue;
            else
                _firstInterViewBLL.DesignationId = null;

            if (txtName.Text.Trim().Length > 0)
                _firstInterViewBLL.Name = txtName.Text.Trim();
            else
                _firstInterViewBLL.Name = null;

            if (txtMobileNo.Text.Trim().Length > 0)
                _firstInterViewBLL.MobileNo = txtMobileNo.Text.Trim();
            else
                _firstInterViewBLL.MobileNo = null;

            if (txtCity.Text.Trim().Length > 0)
                _firstInterViewBLL.City = txtCity.Text.Trim();
            else
                _firstInterViewBLL.City = null;

            if (rdbSelected.Checked == true)
                _firstInterViewBLL.Status = "A";
            else if (rdbHold.Checked == true)
                _firstInterViewBLL.Status = "D";
            else if (rdbALL.Checked == true)
                _firstInterViewBLL.Status = null;

        }
        catch (Exception ex)
        {
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
            dt = _firstInterViewBLL.LoadSubjects(((ddlDepartment.SelectedIndex > 0) ? ddlDepartment.SelectedValue : null));

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