using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class General_Designations : System.Web.UI.Page
{
    #region Declaration
    DesignationsBLL _designationsBLL = new DesignationsBLL();
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadDepts();
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
            _designationsBLL.AllRecords = false;

            DataTable dt = new DataTable();
            dt = _designationsBLL.Designations();
            gdvDesignations.DataSource = dt;
           
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
            gdvDesignations.DataBind();
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
            _designationsBLL.AllRecords = true;

            DataTable dt = new DataTable();
            dt = _designationsBLL.Designations();
            gdvDesignations.DataSource = dt;
            gdvDesignations.DataBind();
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
            if (txtName.Text.Trim().Length > 0)
                _designationsBLL.Name = txtName.Text.Trim();
            else
                _designationsBLL.Name = null;

            if (ddlDeptId.SelectedIndex > 0)
                _designationsBLL.DeptId = ddlDeptId.SelectedValue;
            else
                _designationsBLL.DeptId = null;
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
            _designationsBLL.AllRecords = true;
            dtExport = _designationsBLL.Designations();

            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();

            DataView dv = new DataView(dtExport);
            dgGrid.DataSource = dv;
            dtExport.Columns.Remove("DesignationId");
            hw.WriteLine("<table cellspacing='0' cellpadding='4' border='1' style='font-size:10pt;width:100%;border-collapse:collapse;'>");
            hw.WriteLine("<tr>");
            hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:21px;font-family:Verdana;background-color:#c5d9f1;'><b> Designation Report </b> </td>");
            hw.WriteLine("</tr>");
            if (txtName.Text.Trim().Length > 0)
            {
                hw.WriteLine("<tr>");
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:16px;'><b> Designation Name:" + txtName.Text + "</b> </td>");
                hw.WriteLine("</tr>");
            }
            if (ddlDeptId.SelectedIndex > 0)
            {
                hw.WriteLine("<tr>");
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:16px;'><b> Department Name:" + ddlDeptId.SelectedItem.Text + "</b> </td>");
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
            string attachment = "attachment; filename=DesignationList.xls";
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

    #region Load Dropdown
    private void LoadDepts()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDeptId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlDeptId.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _designationsBLL.GetDepts().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlDeptId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    #endregion

    #region Grid Events
    protected void gdvDesignations_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                HyperLink hl = (HyperLink)e.Row.Cells[1].Controls[0];
                hl.NavigateUrl = "Designation.aspx?DesignationId=" + drv["DesignationId"].ToString();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    protected void gdvDesignations_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            FilterCriteria();
            _designationsBLL.AllRecords = true;

            gdvDesignations.PageIndex = e.NewPageIndex;
            gdvDesignations.DataSource = _designationsBLL.Designations();
            gdvDesignations.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    #endregion

}