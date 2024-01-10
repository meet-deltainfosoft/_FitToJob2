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

public partial class General_LiveClasses : System.Web.UI.Page
{
    private LiveClassesBLL _liveClassesBLL = new LiveClassesBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        btnShowAllRecords.Visible = false;
        if (Page.IsPostBack == false)
        {
            LoadStandard();
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

            foreach (DataRow dtr in _liveClassesBLL.LoadStandard().Rows)
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
    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
            {
                _liveClassesBLL.StandardId = ddlStandard.SelectedValue.ToString();
                LoadSubjects();
            }
            else
            {
                _liveClassesBLL.StandardId = null;
                ShowErrors("", "Please select standard to generate schedule");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
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

    private void LoadSubjects()
    {
        try
        {
            ListItem li = new ListItem();

            ddlSubs.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlSubs.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _liveClassesBLL.LoadSubjects();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSubs.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        FilterCriteria();
        _liveClassesBLL.AllRecord = false;

        DataTable dt = new DataTable();
        dt = _liveClassesBLL.LiveClassList();
        gdvLiveClass.DataSource = dt;

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

        gdvLiveClass.DataBind();
    }
    protected void btnShowAllRecords_Click(object sender, EventArgs e)
    {
        FilterCriteria();

        _liveClassesBLL.AllRecord = true;

        DataTable dt = new DataTable();
        dt = _liveClassesBLL.LiveClassList();
        gdvLiveClass.DataSource = dt;

        lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";

        gdvLiveClass.DataBind();

    }
    protected void gdvLiveClass_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[0].Controls[0];

            hl.NavigateUrl = "LiveClass.aspx?LiveClassId=" + drv[0].ToString();
        }
    }

    private void FilterCriteria()
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
                _liveClassesBLL.StandardId = ddlStandard.SelectedValue;
            else
                _liveClassesBLL.StandardId = null;

            if (ddlSubs.SelectedIndex > 0)
                _liveClassesBLL.SubId = ddlSubs.SelectedValue;
            else
                _liveClassesBLL.SubId = null;
           
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

    protected void ExportToExcel()
    {
        try
        {
            DataTable dtExport = new DataTable();
            _liveClassesBLL.AllRecord = true;
            dtExport = _liveClassesBLL.LiveClassList();
            dtExport.Columns.Remove("LiveClassId");
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();

            DataView dv = new DataView(dtExport);
            dgGrid.DataSource = dv;
            dgGrid.DataSource = dv;
            if (dtExport.Rows.Count > 0)
            {
                hw.WriteLine("<table cellspacing='0' cellpadding='4' border='1' style='font-size:10pt;width:100%;border-collapse:collapse;'>");
                hw.WriteLine("<tr>");
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:21px;font-family:Verdana;background-color:#c5d9f1;'><b> Live Class Detail </b> </td>");
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
                                hw.WriteLine("<td align='right' valign='top' style='border-color:black;font-size:13px;font-family:Verdana;'>" + dtExport.Rows[i][j].ToString() + "</td>");
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
            string attachment = "attachment; filename=LiveClassList.xls";
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
  
}