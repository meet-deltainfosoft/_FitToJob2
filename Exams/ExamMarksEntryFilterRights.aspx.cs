using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class Exams_ExamMarksEntryFilterRights : System.Web.UI.Page
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

                if (Session["_examMarksEntryFilterBLL"] != null)
                {
                    _examMarksEntryBLL = (ExamMarksEntryBLL)Session["_examMarksEntryFilterBLL"];

                    if (_examMarksEntryBLL.StandardId != null)
                    {
                        ddlStandard.SelectedValue = _examMarksEntryBLL.StandardId.ToString();
                        ddlStandard_SelectedIndexChanged(null, null);
                    }

                    if (_examMarksEntryBLL.SubjectId != null)
                    {
                        ddlSubject.SelectedValue = _examMarksEntryBLL.SubjectId.ToString();
                        ddlSubject_SelectedIndexChanged(null, null);
                    }

                    if (_examMarksEntryBLL.TestId != null)
                    {
                        ddlTest.SelectedValue = _examMarksEntryBLL.TestId.ToString();
                        ddlTest_SelectedIndexChanged(null, null);
                    }

                    if (_examMarksEntryBLL.ExamScheduleId != null)
                    {
                        ddlSchedule.SelectedValue = _examMarksEntryBLL.ExamScheduleId.ToString();
                        ddlSchedule_SelectedIndexChanged(null, null);
                    }

                    if (_examMarksEntryBLL.UserId != null)
                    {
                        ddlUserId.SelectedValue = _examMarksEntryBLL.UserId.ToString();
                        ddlUserId_SelectedIndexChanged(null, null);
                    }

                    if (_examMarksEntryBLL.QueId != null)
                    {
                        ddlQue.SelectedValue = _examMarksEntryBLL.QueId.ToString();
                    }

                    btnSearch_Click(null, null);
                }
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

            if (ddlUserId.SelectedIndex > 0)
                _examMarksEntryBLL.UserId = ddlUserId.SelectedValue;
            else
                _examMarksEntryBLL.UserId = null;

            if (ddlQue.SelectedIndex > 0)
                _examMarksEntryBLL.QueId = ddlQue.SelectedValue.ToString();
            else
                _examMarksEntryBLL.QueId = null;
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
                HyperLink hlViewDetail = (HyperLink)e.Row.Cells[9].Controls[0];

                hlViewDetail.NavigateUrl = "../Exams/ExamEvaluation.aspx?QueId=" + drv["QueId"].ToString() +
                                           "&ExamId=" + drv["ExamId"].ToString() + "&RegistrationId=" + drv["RegistrationId"].ToString() +
                                           "&ExamScheduleId=" + drv["ExamScheduleId"].ToString() + "&UserId=" + ddlUserId.SelectedValue.ToString() +
                                           "&EmpName=" + ddlUserId.SelectedItem.Text.ToString() + "&ImageNo=1";

                if (MySession.UserID.ToString().ToUpper() == "aaa".ToString().ToUpper())
                {
                    e.Row.Cells[1].ToolTip = drv["FirstName"].ToString() + " - " + drv["MobileNo"].ToString();
                }

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
    protected void ddlSchedule_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSchedule.SelectedIndex > 0)
            {
                _examMarksEntryBLL.ExamScheduleId = ddlSchedule.SelectedValue.ToString();
                //LoadQuestions();
                LoadEmployee();
            }
            else
            {
                _examMarksEntryBLL.ExamScheduleId = null;
                ddlUserId.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }

    private void LoadQuestions()
    {
        try
        {
            ListItem li = new ListItem();

            ddlQue.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlQue.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _examMarksEntryBLL.LoadQuestions().Rows)
            {
                li = new ListItem();

                li.Text = "Question No : " + dtr["SrNo"].ToString();
                li.Value = dtr["QueId"].ToString();
                ddlQue.Items.Add(li);

                li = null;
            }

            if (ddlQue.Items.Count == 2)
            {
                ddlQue.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }

    private void LoadEmployee()
    {
        try
        {
            ListItem li = new ListItem();

            ddlUserId.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlUserId.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _examMarksEntryBLL.LoadEmployee().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString() + " - " + dtr["UserName"].ToString();
                li.Value = dtr[0].ToString();
                ddlUserId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    protected void ddlUserId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSchedule.SelectedIndex > 0 && ddlUserId.SelectedIndex > 0)
            {
                _examMarksEntryBLL.ExamScheduleId = ddlSchedule.SelectedValue.ToString();
                _examMarksEntryBLL.UserId = ddlUserId.SelectedValue.ToString();

                LoadQuestions();
            }
            else
            {
                _examMarksEntryBLL.ExamScheduleId = null;
                _examMarksEntryBLL.UserId = null;

                ddlQue.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            FilterCriteria();

            if (ddlQue.SelectedIndex > 0 && ddlUserId.SelectedIndex > 0)
            {
                DataTable dt = new DataTable();
                dt = _examMarksEntryBLL.LoadStudentWiseQuestionAns();
                gdvResultDetail.DataSource = dt;
                gdvResultDetail.DataBind();
            }
            else
            {
                ShowErrors("err", "Please select compulsary field for getting list of registrations");
            }

            Session["_examMarksEntryFilterBLL"] = _examMarksEntryBLL;
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
}