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

public partial class General_ChapterPDFs : System.Web.UI.Page
{
    private ChapterPDFsBLL _ChapterPDFsBLL = new ChapterPDFsBLL();

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

            foreach (DataRow dtr in _ChapterPDFsBLL.LoadStandard().Rows)
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
                _ChapterPDFsBLL.StandardId = ddlStandard.SelectedValue.ToString();
                LoadSubjects();
            }
            else
            {
                _ChapterPDFsBLL.StandardId = null;
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
            dt = _ChapterPDFsBLL.LoadSubjects();

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

    private void LoadChapter()
    {
        try
        {
            ListItem li = new ListItem();

            ddlChapter.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlChapter.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterPDFsBLL.LoadChepter();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlChapter.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }


    //private void LoadPeriodNo()
    //{
    //    try
    //    {
    //        ListItem li = new ListItem();

    //        ddlPeriodNo.Items.Clear();

    //        li.Text = "<Select>";
    //        li.Value = "0";
    //        ddlPeriodNo.Items.Add(li);

    //        li = null;

    //        DataTable dt = new DataTable();
    //        dt = _ChapterPDFsBLL.LoadPeriod();

    //        foreach (DataRow dtr in dt.Rows)
    //        {
    //            li = new ListItem();

    //            li.Text = dtr[0].ToString();
    //            li.Value = dtr[0].ToString();
    //            ddlPeriodNo.Items.Add(li);

    //            li = null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowErrors("err", ex.Message);
    //    }
    //}

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        FilterCriteria();



        _ChapterPDFsBLL.AllRecord = false;

        DataTable dt = new DataTable();
        dt = _ChapterPDFsBLL.ChapterPDFList();
        gdvChapterPDF.DataSource = dt;

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

        gdvChapterPDF.DataBind();
    }
    protected void btnShowAllRecords_Click(object sender, EventArgs e)
    {
        FilterCriteria();

        _ChapterPDFsBLL.AllRecord = true;

        DataTable dt = new DataTable();
        dt = _ChapterPDFsBLL.ChapterPDFList();
        gdvChapterPDF.DataSource = dt;

        lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";

        gdvChapterPDF.DataBind();

    }
    protected void gdvChapterPDF_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[0].Controls[0];

            hl.NavigateUrl = "ChapterPDF.aspx?ChapterPDFId=" + drv["ChapterPDFId"].ToString();
        }
    }

    private void FilterCriteria()
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
                _ChapterPDFsBLL.StandardId = ddlStandard.SelectedValue;
            else
                _ChapterPDFsBLL.StandardId = null;

            if (ddlSubs.SelectedIndex > 0)
                _ChapterPDFsBLL.SubId = ddlSubs.SelectedValue;
            else
                _ChapterPDFsBLL.SubId = null;

            if (ddlChapter.SelectedIndex > 0)
                _ChapterPDFsBLL.ChapterId = ddlChapter.SelectedValue;
            else
                _ChapterPDFsBLL.ChapterId = null;

            //if (ddlPeriodNo.SelectedIndex > 0)
            //    _ChapterPDFsBLL.PeriodNo = ddlPeriodNo.SelectedValue;
            //else
            //    _ChapterPDFsBLL.PeriodNo = null;
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
            _ChapterPDFsBLL.AllRecord = true;
            dtExport = _ChapterPDFsBLL.ChapterPDFList();

            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();

            DataView dv = new DataView(dtExport);
            dgGrid.DataSource = dv;
            if (dtExport.Rows.Count > 0)
            {
                hw.WriteLine("<table cellspacing='0' cellpadding='4' border='1' style='font-size:10pt;width:100%;border-collapse:collapse;'>");
                hw.WriteLine("<tr>");
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:21px;font-family:Verdana;background-color:#c5d9f1;'><b> Chapter PDF Detail </b> </td>");
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
            string attachment = "attachment; filename=ChapterPDFList.xls";
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
    protected void ddlSubs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubs.SelectedIndex > 0)
            {
                _ChapterPDFsBLL.SubId = ddlSubs.SelectedValue.ToString();
                LoadChapter();
            }
            else
            {
                _ChapterPDFsBLL.StandardId = null;
                //ShowErrors("", "Please select standard to generate schedule");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlChapter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlChapter.SelectedIndex > 0 && ddlSubs.SelectedIndex > 0)
            {
                _ChapterPDFsBLL.ChapterId = ddlChapter.SelectedValue.ToString();
                _ChapterPDFsBLL.SubId = ddlSubs.SelectedValue.ToString();

                //LoadPeriodNo();
            }
            else
            {
                _ChapterPDFsBLL.ChapterId = null;
                //ddlPeriodNo.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
}