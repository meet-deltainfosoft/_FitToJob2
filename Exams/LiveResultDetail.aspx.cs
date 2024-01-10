using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class General_LiveResultDetail : System.Web.UI.Page
{
    private ResultDetailBLL _ResultDetailBLL = new ResultDetailBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadStandard();
            lblCounter.Text = "";
        }
    }
    protected void gdvResultDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            //HyperLink hl = (HyperLink)e.Row.Cells[0].Controls[0];
            //hl.NavigateUrl = "../Exams/ViewResultDetail.aspx?RegistrationId=" + drv[0].ToString() + "&ExamScheduleId=" + drv[1].ToString() + "";
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

            if (ddlExamSchedule.SelectedIndex > 0)
                _ResultDetailBLL.ExamScheduleId = ddlExamSchedule.SelectedValue.ToString();
            else
                _ResultDetailBLL.ExamScheduleId = null;

            if (txtExamFromTime.Text.Trim().Length > 0 && txtFromExamDate.Text.Trim().Length > 0)
                _ResultDetailBLL._FromScheduleDt = Convert.ToDateTime(txtFromExamDate.Text.ToString() + " " + txtExamFromTime.Text.ToString());
            else
                _ResultDetailBLL._FromScheduleDt = null;

            if (txtExamToTime.Text.Trim().Length > 0 && txtToExamDate.Text.Trim().Length > 0)
                _ResultDetailBLL._ToScheduleDt = Convert.ToDateTime(txtToExamDate.Text.ToString() + " " + txtExamToTime.Text.ToString());
            else
                _ResultDetailBLL._ToScheduleDt = null;
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
            dt = _ResultDetailBLL.LiveResultDetail("");
            gdvResultDetail.DataSource = dt;
            gdvResultDetail.DataBind();

            lblCounter.Text = "Total Recorda [" + dt.Rows.Count + "].";
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
            dtExport = _ResultDetailBLL.LiveResultDetail("");

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
                _ResultDetailBLL.SubjectId = ddlSubject.SelectedValue;
            }
            else
            {
                _ResultDetailBLL.SubjectId = null;
            }

            if (ddlTest.SelectedIndex > 0)
            {
                _ResultDetailBLL.TestId = ddlTest.SelectedValue;
            }
            else
            {
                _ResultDetailBLL.TestId = null;
            }

            if (_ResultDetailBLL.SubjectId != null && _ResultDetailBLL.TestId != null)
            {
                LoadSchedule();
            }
            else
            {
                ddlExamSchedule.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message);
        }
    }
    private void LoadSchedule()
    {
        try
        {
            ListItem li = new ListItem();

            ddlExamSchedule.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlExamSchedule.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _ResultDetailBLL.LoadSchedule().Rows)
            {
                li = new ListItem();

                li.Text = dtr["SubName"].ToString() + "-" + dtr["TestName"].ToString() + "-" + Convert.ToDateTime(dtr["ExamFromTime"]).ToString("hh:mm tt");
                li.Value = dtr[0].ToString();
                ddlExamSchedule.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    protected void btnAbsent_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();
            DataTable dt = new DataTable();
            dt = _ResultDetailBLL.LiveResultDetail("Absent");
            gdvResultDetail.DataSource = dt;
            gdvResultDetail.DataBind();

            lblCounter.Text = "Total Absent Records [" + dt.Rows.Count + "].";
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void btnPresent_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();
            DataTable dt = new DataTable();
            dt = _ResultDetailBLL.LiveResultDetail("Present");
            gdvResultDetail.DataSource = dt;
            gdvResultDetail.DataBind();

            lblCounter.Text = "Total Present Record [" + dt.Rows.Count + "].";
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
}