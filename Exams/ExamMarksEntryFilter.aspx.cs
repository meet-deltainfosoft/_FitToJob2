using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Exams_ExamMarksEntryFilter : System.Web.UI.Page
{
    private ExamMarksEntryBLL _examMarksEntryBLL = new ExamMarksEntryBLL();

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                LoadStandard();
            }
            else
            {
                HideErrors();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    #endregion

    #region Click Events
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();
            DataTable dt = new DataTable();
            dt = _examMarksEntryBLL.ResultDetail();
            gdvResultDetail.DataSource = dt;
            gdvResultDetail.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    #endregion

    #region Load Dropdown
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

            foreach (DataRow dtr in _examMarksEntryBLL.LoadStandard().Rows)
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

            foreach (DataRow dtr in _examMarksEntryBLL.LoadSubject().Rows)
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

            foreach (DataRow dtr in _examMarksEntryBLL.LoadTest().Rows)
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

            foreach (DataRow dtr in _examMarksEntryBLL.LoadSChedule().Rows)
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
    #endregion

    #region Change Events
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject.SelectedIndex > 0)
            {
                _examMarksEntryBLL.SubjectId = ddlSubject.SelectedValue;
                LoadTest();
            }
            else
            {
                _examMarksEntryBLL.SubjectId = null;
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
                _examMarksEntryBLL.StandardId = ddlStandard.SelectedValue;
                LoadSubject();
            }
            else
            {
                _examMarksEntryBLL.StandardId = null;
                ddlSubject.Items.Clear();
                ddlTest.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTest.SelectedIndex > 0)
            {
                _examMarksEntryBLL.TestId = ddlTest.SelectedValue;
                LoadSChedule();
            }
            else
            {
                _examMarksEntryBLL.TestId = null;
                ddlSchedule.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    #endregion

    #region Other Functions
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
    public void FilterCriteria()
    {
        try
        {
            if (txtMobileNo.Text.Trim().Length > 0)
                _examMarksEntryBLL.MobileNo = txtMobileNo.Text.Trim();
            else
                _examMarksEntryBLL.MobileNo = null;

            if (txtStudentName.Text.Trim().Length > 0)
                _examMarksEntryBLL.StudentName = txtStudentName.Text.Trim();
            else
                _examMarksEntryBLL.StudentName = null;

            if (txtScheduleFromDt.Text.Trim().Length > 0)
                _examMarksEntryBLL.FromScheduleDt = Convert.ToDateTime(txtScheduleFromDt.Text.Trim());
            else
                _examMarksEntryBLL.FromScheduleDt = null;

            if (txtScheduleToDt.Text.Trim().Length > 0)
                _examMarksEntryBLL.ToScheduleDt = Convert.ToDateTime(txtScheduleToDt.Text.Trim());
            else
                _examMarksEntryBLL.ToScheduleDt = null;

            if (ddlStandard.SelectedIndex > 0)
                _examMarksEntryBLL.StandardId = ddlStandard.SelectedValue;
            else
                _examMarksEntryBLL.StandardId = null;

            if (ddlSubject.SelectedIndex > 0)
                _examMarksEntryBLL.SubjectId = ddlSubject.SelectedValue;
            else
                _examMarksEntryBLL.SubjectId = null;

            if (ddlTest.SelectedIndex > 0)
                _examMarksEntryBLL.TestId = ddlTest.SelectedValue;
            else
                _examMarksEntryBLL.TestId = null;

            if (ddlSchedule.SelectedIndex > 0)
                _examMarksEntryBLL.ExamScheduleId = ddlSchedule.SelectedValue;
            else
                _examMarksEntryBLL.ExamScheduleId = null;

        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    #endregion

    #region Grid Events
    protected void gdvResultDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                HyperLink hlViewDetail = (HyperLink)e.Row.Cells[11].Controls[0];
                hlViewDetail.NavigateUrl = "../Exams/ExamMarksEntry.aspx?RegistrationId=" + drv[0].ToString() + "&ExamScheduleId=" + drv[1].ToString() + "&StandardId=" + ((ddlStandard.SelectedIndex == 0) ? "" : "" + ddlStandard.SelectedValue + "") + "&SubjectId=" + ((ddlSubject.SelectedIndex == 0) ? "" : "" + ddlSubject.SelectedValue + "") + "&TestId=" + ((ddlTest.SelectedIndex == 0) ? "" : "" + ddlTest.SelectedValue + "") + "";
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
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    #endregion
}