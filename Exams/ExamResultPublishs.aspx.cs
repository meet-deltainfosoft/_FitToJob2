using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections;
using System.Configuration;

public partial class Exams_ExamResultPublishs : System.Web.UI.Page
{
    private ExamResultPublishBLL _examResultPublishBLL = new ExamResultPublishBLL();

    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                LoadStandard();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message);
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

            foreach (DataRow dtr in _examResultPublishBLL.LoadStandard().Rows)
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

            foreach (DataRow dtr in _examResultPublishBLL.LoadSubject().Rows)
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

            foreach (DataRow dtr in _examResultPublishBLL.LoadTest().Rows)
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

            foreach (DataRow dtr in _examResultPublishBLL.LoadSChedule().Rows)
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
                _examResultPublishBLL.SubId = ddlSubject.SelectedValue;
                LoadTest();
            }
            else
            {
                _examResultPublishBLL.SubId = null;
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
                _examResultPublishBLL.StandardTextListId = ddlStandard.SelectedValue;
                LoadSubject();
            }
            else
            {
                _examResultPublishBLL.StandardTextListId = null;
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
                _examResultPublishBLL.TestId = ddlTest.SelectedValue;
                LoadSChedule();
            }
            else
            {
                _examResultPublishBLL.TestId = null;
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
    #endregion

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();

            DataTable dt = new DataTable();
            dt = _examResultPublishBLL.ExamResultPublish();
            gdvMasterValues.DataSource = dt;
            gdvMasterValues.DataBind();

            lblRecordStatus.Text = "Total No. : [ " + dt.Rows.Count.ToString() + " ]";
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
            if (ddlStandard.SelectedIndex > 0)
                _examResultPublishBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _examResultPublishBLL.StandardTextListId = null;

            if (ddlSubject.SelectedIndex > 0)
                _examResultPublishBLL.SubId = ddlSubject.SelectedValue;
            else
                _examResultPublishBLL.SubId = null;

            if (ddlTest.SelectedIndex > 0)
                _examResultPublishBLL.TestId = ddlTest.SelectedValue;
            else
                _examResultPublishBLL.TestId = null;

            if (ddlSchedule.SelectedIndex > 0)
                _examResultPublishBLL.ExamScheduleId = ddlSchedule.SelectedValue.ToString();
            else
                _examResultPublishBLL.ExamScheduleId = null;
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    protected void gdvMasterValues_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                HyperLink hl = (HyperLink)e.Row.Cells[1].Controls[0];

                hl.NavigateUrl = "../Exams/ExamResultPublish.aspx?ExamResultPublishId=" + drv[0].ToString();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    protected void gdvMasterValues_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            btnSearch_Click(null, null);

            gdvMasterValues.PageIndex = e.NewPageIndex;
            gdvMasterValues.DataSource = _examResultPublishBLL.ExamResultPublish();
            gdvMasterValues.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
}