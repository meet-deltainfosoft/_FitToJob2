using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class General_ResultDetailAdv : System.Web.UI.Page
{
    private ResultDetailBLL _ResultDetailBLL = new ResultDetailBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadStandard();

        }
    }
    protected void gdvResultDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[1].Controls[0];
            hl.NavigateUrl = "../Exams/ViewResultDetailAdv.aspx?RegistrationId=" + drv[0].ToString() + "&ExamScheduleId=" + drv[1].ToString() + "";

            for (int i = 0; i < gdvResultDetail.Rows.Count; i++)
            {
                if (i == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.AliceBlue;
                }
                else if (gdvResultDetail.Rows.Count % 2 == 0)
                {
                    e.Row.BackColor = System.Drawing.Color.Beige;
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.AliceBlue;
                }
            }
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

            foreach (DataRow dtr in _ResultDetailBLL.LoadStandard().Rows)
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

            foreach (DataRow dtr in _ResultDetailBLL.LoadSubject().Rows)
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

            foreach (DataRow dtr in _ResultDetailBLL.LoadTest().Rows)
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
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject.SelectedIndex > 0)
            {
                _ResultDetailBLL.SubjectId = ddlSubject.SelectedValue;
                LoadTest();
            }
            else
            {
                _ResultDetailBLL.SubjectId = null;
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
                _ResultDetailBLL.StandardId = ddlStandard.SelectedValue;
                LoadSubject();
            }
            else
            {
                _ResultDetailBLL.StandardId = null;
                ddlSubject.Items.Clear();
                ddlTest.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    public void FilterCriteria()
    {
        try
        {
            if (txtMobileNo.Text.Trim().Length > 0)
                _ResultDetailBLL.MobileNo = txtMobileNo.Text.Trim();
            else
                _ResultDetailBLL.MobileNo = null;

            if (txtStudentName.Text.Trim().Length > 0)
                _ResultDetailBLL.StudentName = txtStudentName.Text.Trim();
            else
                _ResultDetailBLL.StudentName = null;

            if (txtScheduleFromDt.Text.Trim().Length > 0)
                _ResultDetailBLL.FromScheduleDt = Convert.ToDateTime(txtScheduleFromDt.Text.Trim());
            else
                _ResultDetailBLL.FromScheduleDt = null;

            if (txtScheduleToDt.Text.Trim().Length > 0)
                _ResultDetailBLL.ToScheduleDt = Convert.ToDateTime(txtScheduleToDt.Text.Trim());
            else
                _ResultDetailBLL.ToScheduleDt = null;

            if (ddlStandard.SelectedIndex > 0)
                _ResultDetailBLL.StandardId = ddlStandard.SelectedValue;
            else
                _ResultDetailBLL.StandardId = null;

            if (ddlSubject.SelectedIndex > 0)
                _ResultDetailBLL.SubjectId = ddlSubject.SelectedValue;
            else
                _ResultDetailBLL.SubjectId = null;

            if (ddlTest.SelectedIndex > 0)
                _ResultDetailBLL.TestId = ddlTest.SelectedValue;
            else
                _ResultDetailBLL.TestId = null;

            if (ddlSchedule.SelectedIndex > 0)
                _ResultDetailBLL.ExamScheduleId = ddlSchedule.SelectedValue;
            else
                _ResultDetailBLL.ExamScheduleId = null;

        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();
            DataTable dt = new DataTable();
            dt = _ResultDetailBLL.ResultDetailAdv();
            gdvResultDetail.DataSource = dt;
            gdvResultDetail.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
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
            FilterCriteria();
            DataTable dtExport = new DataTable();
            _ResultDetailBLL.IsExcel = true;
            dtExport = _ResultDetailBLL.ResultDetailAdv();

            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();

            DataView dv = new DataView(dtExport);
            dgGrid.DataSource = dv;
            if (dtExport.Rows.Count > 0)
            {
                hw.WriteLine("<table cellspacing='0' cellpadding='4' border='1' style='font-size:10pt;width:100%;border-collapse:collapse;'>");
                hw.WriteLine("<tr>");
                hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:21px;font-family:Verdana;background-color:#c5d9f1;'><b> Result Detail </b> </td>");
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
            string attachment = "attachment; filename=ResultDetail.xls";
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
    protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTest.SelectedIndex > 0)
            {
                _ResultDetailBLL.TestId = ddlTest.SelectedValue;
                LoadSChedule();
            }
            else
            {
                _ResultDetailBLL.TestId = null;
                ddlSchedule.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    private void LoadSChedule()
    {
        try
        {
            ListItem li = new ListItem();

            ddlSchedule.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlSchedule.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _ResultDetailBLL.LoadSChedule().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSchedule.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    protected void btnERPExcel_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();
            DataTable dtExport = new DataTable();
            _ResultDetailBLL.IsExcel = true;
            dtExport = _ResultDetailBLL.ERPExportData();

            if (dtExport.Rows.Count > 0)
            {
                //ExportTableData(dtExport, "erp_mark_excel_" +
                //    ddlStandard.SelectedItem.Text.ToString().Replace(" ", "_") + "_" +
                //    ddlSubject.SelectedItem.Text.ToString().Replace(" ", "_") + "_" +
                //    ddlTest.SelectedItem.Text.ToString().Replace(" ", "_"));


                StringWriter tw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();

                DataView dv = new DataView(dtExport);
                dgGrid.DataSource = dv;
                if (dtExport.Rows.Count > 0)
                {
                    hw.WriteLine("<table cellspacing='0' cellpadding='4' border='1' style='font-size:10pt;width:100%;border-collapse:collapse;'>");
                    //hw.WriteLine("<tr>");
                    //hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:21px;font-family:Verdana;background-color:#c5d9f1;'><b> Result Detail </b> </td>");
                    //hw.WriteLine("</tr>");
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

                string attachment = "attachment; filename=erp_mark_excel_" +
                                     ddlStandard.SelectedItem.Text.ToString().Replace(" ", "_") + "_" +
                                     ddlSubject.SelectedItem.Text.ToString().Replace(" ", "_") + "_" +
                                     ddlTest.SelectedItem.Text.ToString().Replace(" ", "_") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                Response.Write(tw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }

    public void ExportTableData(DataTable dtdata, string fileName)
    {
        try
        {
            string attach = "attachment;filename=" + fileName + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attach);
            Response.ContentType = "application/ms-excel";
            if (dtdata != null)
            {
                foreach (DataColumn dc in dtdata.Columns)
                {
                    Response.Write(dc.ColumnName + "\t");
                }
                Response.Write(System.Environment.NewLine);
                foreach (DataRow dr in dtdata.Rows)
                {
                    for (int i = 0; i < dtdata.Columns.Count; i++)
                    {
                        Response.Write(dr[i].ToString() + "\t");
                    }
                    Response.Write("\n");
                }
                Response.End();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void btnERPExcelQuestionWise_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();
            DataTable dtExport = new DataTable();
            _ResultDetailBLL.IsExcel = true;
            dtExport = _ResultDetailBLL.ERPExportDataQueWise();

            if (dtExport.Rows.Count > 0)
            {
                StringWriter tw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(tw);
                DataGrid dgGrid = new DataGrid();

                DataView dv = new DataView(dtExport);
                dgGrid.DataSource = dv;
                if (dtExport.Rows.Count > 0)
                {
                    hw.WriteLine("<table cellspacing='0' cellpadding='4' border='1' style='font-size:10pt;width:100%;border-collapse:collapse;'>");
                    //hw.WriteLine("<tr>");
                    //hw.WriteLine("<td colspan='" + dtExport.Columns.Count + "' align='center' valign='top' style='border-color:black;font-size:21px;font-family:Verdana;background-color:#c5d9f1;'><b> Result Detail </b> </td>");
                    //hw.WriteLine("</tr>");
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

                string attachment = "attachment; filename=erp_mark_excel_" +
                                     ddlStandard.SelectedItem.Text.ToString().Replace(" ", "_") + "_" +
                                     ddlSubject.SelectedItem.Text.ToString().Replace(" ", "_") + "_" +
                                     ddlTest.SelectedItem.Text.ToString().Replace(" ", "_") + ".xls";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";
                Response.Write(tw.ToString());
                Response.End();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
}