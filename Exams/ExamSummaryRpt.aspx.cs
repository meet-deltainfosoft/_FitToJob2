using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Exams_ExamSummaryRpt : System.Web.UI.Page
{
    private ExamSummaryRptBLL _ExamSummaryRptBLL = new ExamSummaryRptBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        btnShowAllRecords.Visible = false;
        if (!Page.IsPostBack)
        {
            LoadStandard();
            btnExamRpt_Click(btnExamRpt, new EventArgs());
        }
    }

    #region Grid Row Data Bound
    protected void gdvExamSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[12].Controls[0];
            hl.NavigateUrl = "../Exams/ExamSummaryDetailRpt.aspx?StandardId=" + drv["StandardId"].ToString() + "&SubjectId=" + drv["SubId"].ToString() + "&TestId=" + drv["TestId"].ToString() + "&Standard=" + drv["Standard"].ToString() + "&Subject=" + drv["Subject"].ToString() + "&TestName=" + drv["TestName"].ToString() + "&ExamScheduleId=" + drv["ExamScheduleId"].ToString() + "&Schedule=" + drv["Schedule"].ToString() + "";
        }
    }
    #endregion

    #region Load Dropdown Data
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

            foreach (DataRow dtr in _ExamSummaryRptBLL.LoadStandard().Rows)
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
    private void LoadSubject()
    {
        try
        {
            ListItem li = new ListItem();

            ddlSubject.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlSubject.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _ExamSummaryRptBLL.LoadSubject().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSubject.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    private void LoadTest()
    {
        try
        {
            ListItem li = new ListItem();

            ddlTest.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlTest.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _ExamSummaryRptBLL.LoadTest().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlTest.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    #endregion

    #region Show and Hide Error
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
            ShowErrors("err", ex.Message);
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
            ShowErrors("err", ex.Message);
        }
    }
    #endregion

    #region Change Event
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject.SelectedIndex > 0)
            {
                _ExamSummaryRptBLL.SubjectId = ddlSubject.SelectedValue;
                LoadTest();
            }
            else
            {
                _ExamSummaryRptBLL.SubjectId = null;
                ddlTest.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
            {
                _ExamSummaryRptBLL.StandardId = ddlStandard.SelectedValue;
                LoadSubject();
            }
            else
            {
                _ExamSummaryRptBLL.StandardId = null;
                ddlSubject.Items.Clear();
                ddlTest.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    #endregion

    #region Filter Criteria
    public void FilterCriteria()
    {
        try
        {
            if (txtScheduleFromDt.Text.Trim().Length > 0)
                _ExamSummaryRptBLL.FromScheduleDt = Convert.ToDateTime(txtScheduleFromDt.Text.Trim());
            else
                _ExamSummaryRptBLL.FromScheduleDt = null;

            if (txtScheduleToDt.Text.Trim().Length > 0)
                _ExamSummaryRptBLL.ToScheduleDt = Convert.ToDateTime(txtScheduleToDt.Text.Trim());
            else
                _ExamSummaryRptBLL.ToScheduleDt = null;

            if (ddlStandard.SelectedIndex > 0)
                _ExamSummaryRptBLL.StandardId = ddlStandard.SelectedValue;
            else
                _ExamSummaryRptBLL.StandardId = null;

            if (ddlSubject.SelectedIndex > 0)
                _ExamSummaryRptBLL.SubjectId = ddlSubject.SelectedValue;
            else
                _ExamSummaryRptBLL.SubjectId = null;

            if (ddlTest.SelectedIndex > 0)
                _ExamSummaryRptBLL.TestId = ddlTest.SelectedValue;
            else
                _ExamSummaryRptBLL.TestId = null;
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    #endregion

    #region Click event
    protected void btnExamRpt_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();
            _ExamSummaryRptBLL.AllRecords = false;
            DataTable dt = new DataTable();
            dt = _ExamSummaryRptBLL.ExamSummaryDetail();
            gdvExamSummary.DataSource = dt;
            gdvExamSummary.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void btnShowAllRecords_Click(object sender, EventArgs e)
    {
        FilterCriteria();

        _ExamSummaryRptBLL.AllRecords = true;
        DataTable dt = new DataTable();
        dt = _ExamSummaryRptBLL.ExamSummaryDetail();
        gdvExamSummary.DataSource = dt;
        lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";
        gdvExamSummary.DataBind();
    }
    #endregion

    #region Export Data
    //protected void btnExcel_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        FilterCriteria();
    //        ExportToExcel();
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowErrors("error", ex.Message);
    //    }
    //}

    //protected void ExportToExcel()
    //{
    //    try
    //    {
    //        FilterCriteria();
    //        DataTable dtExport = new DataTable();
    //        dtExport = _ExamSummaryRptBLL.ResultDetail();

    //        StringWriter tw = new StringWriter();
    //        HtmlTextWriter hw = new HtmlTextWriter(tw);
    //        DataGrid dgGrid = new DataGrid();

    //        DataView dv = new DataView(dtExport);
    //        dgGrid.DataSource = dv;
    //        if (dtExport.Rows.Count > 0)
    //        {
    //            hw.WriteLine("<table cellspacing='0' cellpadding='4' border='1' style='font-size:10pt;width:100%;border-collapse:collapse;'>");
    //            hw.WriteLine("<tr>");
    //            hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:21px;font-family:Verdana;background-color:#c5d9f1;'><b> Result Detail </b> </td>");
    //            hw.WriteLine("</tr>");
    //            hw.WriteLine("<tr>");
    //            foreach (DataColumn Columns in dtExport.Columns)
    //            {
    //                hw.WriteLine("<td align='center' valign='top' style='border-color:black;font-size:14px;font-family:Verdana;'><b> " + Columns.ColumnName.ToString() + "</b></td>");
    //            }
    //            hw.WriteLine("</tr>");

    //            for (int i = 0; i < dtExport.Rows.Count; i++)
    //            {
    //                hw.WriteLine("<tr>");
    //                for (int j = 0; j < dtExport.Columns.Count; j++)
    //                {
    //                    if (dtExport.Rows[i][j] != DBNull.Value)
    //                    {
    //                        if (dtExport.Columns[j].DataType == typeof(System.Decimal))
    //                            hw.WriteLine("<td align='right' valign='top' style='border-color:black;font-size:13px;font-family:Verdana;'>" + Convert.ToDecimal(dtExport.Rows[i][j]).ToString("0.00") + "</td>");
    //                        else
    //                            hw.WriteLine("<td align='left' valign='top' style='border-color:black;font-size:13px;font-family:Verdana;'>" + dtExport.Rows[i][j].ToString() + "</td>");
    //                    }
    //                    else
    //                        hw.WriteLine("<td align='left' valign='top' style='border-color:black;font-size:13px;font-family:Verdana;'></td>");
    //                }
    //                hw.WriteLine("</tr>");
    //            }
    //        }
    //        else
    //        {
    //            hw.WriteLine("<tr>");
    //            hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:13px;font-family:Verdana;'> No Records Found.</td>");
    //            hw.WriteLine("</tr>");
    //        }
    //        hw.WriteLine("</table>");
    //        string attachment = "attachment; filename=ResultDetail.xls";
    //        Response.ClearContent();
    //        Response.AddHeader("content-disposition", attachment);
    //        Response.ContentType = "application/ms-excel";
    //        Response.Write(tw.ToString());
    //        Response.End();
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowErrors("error", ex.Message);
    //    }
    //}
    #endregion
}