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
using System.IO;

public partial class Exams_Registrations : System.Web.UI.Page
{
    private RegistrationsBLL _RegistrationsBLL = new RegistrationsBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        btnShowAllRecords.Visible = false;
        if (Page.IsPostBack == false)
        {
            LoadStandard();
            CountStudent();
            LoadDivision();
        }
    }

    private void LoadStandard()
    {
        try
        {
            ListItem li = new ListItem();

            ddlStandard.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlStandard.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _RegistrationsBLL.LoadStandard().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlStandard.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
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

            foreach (DataRow dtr in _RegistrationsBLL.LoadDivision().Rows)
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

    private void CountStudent()
    {
        try
        {
            lblCountStudents.Text = _RegistrationsBLL.CountStudent().ToString();
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
        CountStudent();


        _RegistrationsBLL.AllRecord = false;

        DataTable dt = new DataTable();
        dt = _RegistrationsBLL.Registrations();
        gdvRegistrations.DataSource = dt;

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

        gdvRegistrations.DataBind();
    }
    protected void btnShowAllRecords_Click(object sender, EventArgs e)
    {
        FilterCriteria();

        _RegistrationsBLL.AllRecord = true;

        DataTable dt = new DataTable();
        dt = _RegistrationsBLL.Registrations();
        gdvRegistrations.DataSource = dt;

        lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";

        gdvRegistrations.DataBind();

    }
    protected void gdvRegistrations_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[0].Controls[0];

            hl.NavigateUrl = "Registration.aspx?RegistrationId=" + drv[0].ToString();
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
            _RegistrationsBLL.AllRecord = true;
            dtExport = _RegistrationsBLL.Registrations();

            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();

            DataView dv = new DataView(dtExport);
            dgGrid.DataSource = dv;
            dtExport.Columns.Remove("RegistrationId");
            dtExport.Columns.Remove("StandardId");
            if (dtExport.Rows.Count > 0)
            {
                hw.WriteLine("<table cellspacing='0' cellpadding='4' border='1' style='font-size:10pt;width:100%;border-collapse:collapse;'>");
                hw.WriteLine("<tr>");
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:21px;font-family:Verdana;background-color:#c5d9f1;'><b> Registration Report </b> </td>");
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
            string attachment = "attachment; filename=Registrations.xls";
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

    private void FilterCriteria()
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
                _RegistrationsBLL.StandardId = ddlStandard.SelectedValue;
            else
                _RegistrationsBLL.StandardId = null;

            if (ddlDivision.SelectedIndex > 0)
                _RegistrationsBLL.DivisionTextListId = ddlDivision.SelectedValue;
            else
                _RegistrationsBLL.DivisionTextListId = null;

            if (txtRegistrationName.Text.Trim().Length > 0)
                _RegistrationsBLL.RegistrationName = txtRegistrationName.Text.Trim();
            else
                _RegistrationsBLL.RegistrationName = null;

            if (txtSchoolName.Text.Trim().Length > 0)
                _RegistrationsBLL.SchoolName = txtSchoolName.Text.Trim();
            else
                _RegistrationsBLL.SchoolName = null;

            if (txtCity.Text.Trim().Length > 0)
                _RegistrationsBLL.City = txtCity.Text.Trim();
            else
                _RegistrationsBLL.City = null;

            if (txtMobileNo.Text.Trim().Length > 0)
                _RegistrationsBLL.MobileNo = txtMobileNo.Text.Trim();
            else
                _RegistrationsBLL.MobileNo = null;

            _RegistrationsBLL.IsDeActive = chkIsDeactive.Checked;
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
}
