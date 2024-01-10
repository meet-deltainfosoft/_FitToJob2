using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

public partial class Exams_ExamCheckAllotments : System.Web.UI.Page
{
    ExamCheckAllotmentsBLL _examCheckAllotmentsBLL = new ExamCheckAllotmentsBLL();

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

            foreach (DataRow dtr in _examCheckAllotmentsBLL.LoadStandard().Rows)
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

            foreach (DataRow dtr in _examCheckAllotmentsBLL.LoadSubject().Rows)
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

            foreach (DataRow dtr in _examCheckAllotmentsBLL.LoadTest().Rows)
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

            foreach (DataRow dtr in _examCheckAllotmentsBLL.LoadSchedule().Rows)
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

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();

            DataTable dt = new DataTable();
            dt = _examCheckAllotmentsBLL.ExamCheckAllotments();
            gdvMasterValues.DataSource = dt;
            gdvMasterValues.DataBind();

            lblRecordStatus.Text = "Total No. Of Master Values : [ " + dt.Rows.Count.ToString() + " ]";
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

                hl.NavigateUrl = "../Exams/ExamCheckAllotment.aspx?ExamCheckAllotmentId=" + drv[0].ToString();
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
            btnFilter_Click(null, null);

            gdvMasterValues.PageIndex = e.NewPageIndex;
            gdvMasterValues.DataSource = _examCheckAllotmentsBLL.ExamCheckAllotments();
            gdvMasterValues.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();
    }

    private void ShowErrors(string key, string value)
    {
        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject.SelectedIndex > 0)
            {
                _examCheckAllotmentsBLL.SubId = ddlSubject.SelectedValue;
                LoadTest();
            }
            else
            {
                _examCheckAllotmentsBLL.SubId = null;
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
                _examCheckAllotmentsBLL.StandardTextListId = ddlStandard.SelectedValue;
                LoadSubject();
            }
            else
            {
                _examCheckAllotmentsBLL.StandardTextListId = null;
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
            if (ddlSubject.SelectedIndex > 0)
            {
                _examCheckAllotmentsBLL.SubId = ddlSubject.SelectedValue;
            }
            else
            {
                _examCheckAllotmentsBLL.SubId = null;
            }

            if (ddlTest.SelectedIndex > 0)
            {
                _examCheckAllotmentsBLL.TestId = ddlTest.SelectedValue;
            }
            else
            {
                _examCheckAllotmentsBLL.TestId = null;
            }

            if (ddlSubject.SelectedIndex > 0 && ddlTest.SelectedIndex > 0)
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

    public void FilterCriteria()
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
                _examCheckAllotmentsBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _examCheckAllotmentsBLL.StandardTextListId = null;

            if (ddlSubject.SelectedIndex > 0)
                _examCheckAllotmentsBLL.SubId = ddlSubject.SelectedValue;
            else
                _examCheckAllotmentsBLL.SubId = null;

            if (ddlTest.SelectedIndex > 0)
                _examCheckAllotmentsBLL.TestId = ddlTest.SelectedValue;
            else
                _examCheckAllotmentsBLL.TestId = null;

            if (ddlExamSchedule.SelectedIndex > 0)
                _examCheckAllotmentsBLL.ExamScheduleId = ddlExamSchedule.SelectedValue.ToString();
            else
                _examCheckAllotmentsBLL.ExamScheduleId = null;
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
}
