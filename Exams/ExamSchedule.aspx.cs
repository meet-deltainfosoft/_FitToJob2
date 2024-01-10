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

public partial class Exams_ExamSchedule : System.Web.UI.Page
{
    private ExamScheduleBLL _examScheduleBLL;

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["ExamScheduleId"] == null)
                {
                    _examScheduleBLL = new ExamScheduleBLL();
                }
                else
                {
                    _examScheduleBLL = new ExamScheduleBLL(Request.QueryString["ExamScheduleId"].ToString());
                }
                Session["_examScheduleBLL"] = _examScheduleBLL;
            }
            else
            {
                _examScheduleBLL = (ExamScheduleBLL)Session["_examScheduleBLL"];
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                LoadStandard();
                LoadDivision();

                if (Request.QueryString["ExamScheduleId"] != null)
                {
                    lblTitle.Text = " - [Edit Mode]";
                    LoadWebForm();
                    btnDelete.Enabled = true;
                    ShowErrors("err", "You cannot edit this entry. You have to delete this schedule and generate again.");
                    //btnOK.Visible = false;
                }
                else
                {

                }
            }
            else
            {
                HideErrors();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    #endregion

    #region "Subs Functions"

    private bool Validate()
    {
        try
        {
            HideErrors();

            SortedList sl = _examScheduleBLL.Validate();

            if (sl.Count > 0)
            {
                for (int i = 0; i < sl.Count; i++)
                {
                    string key = (string)sl.GetKey(i);
                    string value = (string)sl[key];

                    ShowErrors(key, value);
                }
            }
            return (sl.Count == 0) ? true : false;
        }
        catch (Exception ex)
        {
            return false;
            ShowErrors("", ex.Message.ToString());
        }
    }

    private void ShowErrors(string key, string value)
    {
        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));

        if (key == "StandardTextListId")
        {
            lblStandard.CssClass = "error";
            ddlStandard.CssClass = "error";
        }

        if (key == "SubId")
        {
            lblSubs.CssClass = "error";
            ddlSubs.CssClass = "error";
        }

        if (key == "TestId")
        {
            lblTestId.CssClass = "error";
            ddlTestId.CssClass = "error";
        }

        if (key == "TotalQuestions")
        {
            lblTotalQuestion.CssClass = "error";
            lblTotalQuestionlabel.CssClass = "error";
        }

        if (key == "ExamDate")
        {
            lblExamDate.CssClass = "error";
            txtExamDate.CssClass = "error";
        }

        if (key == "ExamFromTime")
        {
            lblExamFromTime.CssClass = "error";
            txtExamFromTime.CssClass = "error";
        }

        if (key == "ExamToTime")
        {
            lblExamToTime.CssClass = "error";
            txtExamToTime.CssClass = "error";
        }

        if (key == "TotalMins" || key == "QuestionsTime")
        {
            lblTotalExamMinutes.CssClass = "error";
            lblTotalExamMinuteslabel.CssClass = "error";
        }

        if (key == "PerQueMins")
        {
            lblPerQuestionMinutes.CssClass = "error";
            lblPerQuestionMinuteslabel.CssClass = "error";
        }
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        lblStandard.CssClass = "";
        ddlStandard.CssClass = "";

        lblSubs.CssClass = "";
        ddlSubs.CssClass = "";

        lblTestId.CssClass = "";
        ddlTestId.CssClass = "";

        lblTotalQuestion.CssClass = "";
        lblTotalQuestionlabel.CssClass = "";

        lblExamDate.CssClass = "";
        txtExamDate.CssClass = "";

        lblExamFromTime.CssClass = "";
        txtExamFromTime.CssClass = "";

        lblExamToTime.CssClass = "";
        txtExamToTime.CssClass = "";

        lblTotalExamMinutes.CssClass = "";
        lblTotalExamMinuteslabel.CssClass = "";

        lblPerQuestionMinutes.CssClass = "";
        lblPerQuestionMinuteslabel.CssClass = "";
    }

    private void Reset()
    {
        try
        {
            ddlStandard.SelectedIndex = 0;
            ddlSubs.SelectedIndex = 0;
            ddlTestId.SelectedIndex = 0;
            lblTotalQuestion.Text = "";
            txtExamDate.Text = "";
            txtExamFromTime.Text = "";
            txtExamToTime.Text = "";
            lblTotalExamMinutes.Text = "";
            lblPerQuestionMinutes.Text = "";

            gdvRegistrations.DataSource = null;
            gdvRegistrations.DataBind();

            chkIsDefaultTest.Checked = false;
            chkNegativeMarks.Checked = true;
            ddlPatternId.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }
    private void LoadWebForm()
    {
        try
        {
            if (_examScheduleBLL.StandardTextListId != null)
            {
                ddlStandard.SelectedValue = _examScheduleBLL.StandardTextListId.ToString();
                ddlStandard_SelectedIndexChanged(null, null);
                ddlStandard.Enabled = false;
            }

            if (_examScheduleBLL.SubId != null)
            {
                ddlSubs.SelectedValue = _examScheduleBLL.SubId;
                ddlSubs_SelectedIndexChanged(null, null);
                ddlSubs.Enabled = false;
            }

            if (_examScheduleBLL.PatternId != null)
            {
                ddlPatternId.SelectedValue = _examScheduleBLL.PatternId;
                ddlPatternId_SelectedIndexChanged(null, null);
                ddlPatternId.Enabled = false;
            }

            if (_examScheduleBLL.TestId != null)
            {
                ddlTestId.SelectedValue = _examScheduleBLL.TestId;
                ddlTestId_SelectedIndexChanged(null, null);
                ddlTestId.Enabled = false;
            }

            if (_examScheduleBLL.TotalQuestions != null)
                lblTotalQuestion.Text = Convert.ToInt16(_examScheduleBLL.TotalQuestions).ToString("0.##");

            if (_examScheduleBLL.ExamDate != null)
                txtExamDate.Text = Convert.ToDateTime(_examScheduleBLL.ExamDate).ToString("dd-MMM-yyyy");

            if (_examScheduleBLL.ExamFromTime != null)
                txtExamFromTime.Text = Convert.ToDateTime(_examScheduleBLL.ExamFromTime).ToString("hh:mm tt");

            if (_examScheduleBLL.ExamToTime != null)
                txtExamToTime.Text = Convert.ToDateTime(_examScheduleBLL.ExamToTime).ToString("hh:mm tt");

            if (_examScheduleBLL.TotalMins != null)
                lblTotalExamMinutes.Text = Convert.ToInt16(_examScheduleBLL.TotalMins).ToString("0.##");

            if (_examScheduleBLL.PerQueMins != null)
                lblPerQuestionMinutes.Text = Math.Round(Convert.ToDecimal(_examScheduleBLL.PerQueMins), 0).ToString("0.##");

            if (_examScheduleBLL.dtRestrations != null)
            {
                gdvRegistrations.DataSource = _examScheduleBLL.dtRestrations;
                gdvRegistrations.DataBind();

                lblStudentCount.Text = "Total No Of Records [" + _examScheduleBLL.dtRestrations.Rows.Count + "]";
            }

            if (_examScheduleBLL.NegativeMarks != null)
            {
                chkNegativeMarks.Checked = Convert.ToBoolean(_examScheduleBLL.NegativeMarks);
            }

            if (_examScheduleBLL.PerQuestionTime != null)
            {
                chkPerQuestionTime.Checked = Convert.ToBoolean(_examScheduleBLL.PerQuestionTime);
            }

            if (_examScheduleBLL.AllowReview != null)
            {
                chkAllowReview.Checked = Convert.ToBoolean(_examScheduleBLL.AllowReview);
            }
            if (_examScheduleBLL.ShowResult != null)
            {
                ChkShowResult.Checked = Convert.ToBoolean(_examScheduleBLL.ShowResult);
            }

            if (_examScheduleBLL.IsDefaultTest != null)
            {
                chkIsDefaultTest.Checked = Convert.ToBoolean(_examScheduleBLL.IsDefaultTest);
            }

            if (_examScheduleBLL.MinsforResultShow != null)
                txtMinsforResultShow.Text = Convert.ToInt16(_examScheduleBLL.MinsforResultShow).ToString("0.##");


            //txtExamDate.Enabled = false;
            //txtExamFromTime.Enabled = false;
            //txtExamToTime.Enabled = false;
            btnSearch.Visible = false;
            //chkNegativeMarks.Enabled = false;
            //chkPerQuestionTime.Enabled = false;
            //chkAllowReview.Enabled = false; 
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }

    #endregion

    #region "Subs Events"
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (_examScheduleBLL.dtRestrations == null)
            {
                if (chkIsDefaultTest.Checked)
                {
                    _examScheduleBLL.IsDefaultTest = true;
                }
                else
                {
                    _examScheduleBLL.IsDefaultTest = false;
                }

                if (ddlStandard.SelectedIndex > 0)
                    _examScheduleBLL.StandardTextListId = ddlStandard.SelectedValue;
                else
                    _examScheduleBLL.StandardTextListId = null;

                if (ddlSubs.SelectedIndex > 0)
                    _examScheduleBLL.SubId = ddlSubs.SelectedValue;
                else
                    _examScheduleBLL.SubId = null;

                if (ddlTestId.SelectedIndex > 0)
                    _examScheduleBLL.TestId = ddlTestId.SelectedValue;
                else
                    _examScheduleBLL.TestId = null;

                if (lblTotalQuestion.Text.Trim().Length > 0)
                    _examScheduleBLL.TotalQuestions = Convert.ToInt16(lblTotalQuestion.Text.ToString());
                else
                    _examScheduleBLL.TotalQuestions = null;

                if (txtExamDate.Text.Trim().Length > 0)
                    _examScheduleBLL.ExamDate = Convert.ToDateTime(txtExamDate.Text.Trim().ToString());
                else
                    _examScheduleBLL.ExamDate = null;

                if (txtExamFromTime.Text.Trim().Length > 0 && txtExamDate.Text.Trim().Length > 0)
                    _examScheduleBLL.ExamFromTime = Convert.ToDateTime(txtExamDate.Text + " " + txtExamFromTime.Text);
                else
                    _examScheduleBLL.ExamFromTime = null;

                if (txtExamToTime.Text.Trim().Length > 0 && txtExamDate.Text.Trim().Length > 0)
                    _examScheduleBLL.ExamToTime = Convert.ToDateTime(txtExamDate.Text + " " + txtExamToTime.Text);
                else
                    _examScheduleBLL.ExamToTime = null;

                if (lblTotalExamMinutes.Text.Trim().Length > 0)
                    _examScheduleBLL.TotalMins = Convert.ToInt16(lblTotalExamMinutes.Text.Trim().ToString());
                else
                    _examScheduleBLL.TotalMins = null;

                if (lblPerQuestionMinutes.Text.Trim().Length > 0)
                    _examScheduleBLL.PerQueMins = Convert.ToDecimal(lblPerQuestionMinutes.Text.Trim().ToString());
                else
                    _examScheduleBLL.PerQueMins = null;

                if (ChkShowResult.Checked)
                {
                    _examScheduleBLL.ShowResult = true;
                    _examScheduleBLL.MinsforResultShow = Convert.ToInt16(txtMinsforResultShow.Text.Trim().ToString());
                }
                else
                {
                    _examScheduleBLL.ShowResult = false;
                    _examScheduleBLL.MinsforResultShow = null;
                }

                //if (Request.QueryString["ExamScheduleId"] == null)
                //{
                //    for (int c = 0; c <= _examScheduleBLL.dtRestrations.Columns.Count - 1; c++)
                //    {
                //        _examScheduleBLL.dtRestrations.Columns[c].ReadOnly = false;
                //    }
                //    for (int i = 0; i <= gdvRegistrations.Rows.Count - 1; i++)
                //    {
                //        GridViewRow gvr = gdvRegistrations.Rows[i];

                //        CheckBox chkSendPendingFeeSMS = (CheckBox)gdvRegistrations.Rows[i].Cells[0].FindControl("chkSendPendingFeeSMS");

                //        _examScheduleBLL.dtRestrations.Rows[i]["Tick"] = chkSendPendingFeeSMS.Checked;

                //        if (chkSendPendingFeeSMS.Checked)
                //            _examScheduleBLL.SkipMobile += gdvRegistrations.Rows[i].Cells[0].ToString();
                //    }
                //}

                //SkipMobile
                //for (int i = 0; i <= gdvRegistrations.Rows.Count - 1; i++)
                //{
                //    GridViewRow gvr = gdvRegistrations.Rows[i];

                //    CheckBox chkSendPendingFeeSMS = (CheckBox)gdvRegistrations.Rows[i].Cells[0].FindControl("chkSendPendingFeeSMS");

                //    _examScheduleBLL.dtRestrations.Rows[i]["Tick"] = chkSendPendingFeeSMS.Checked;
                //}

                _examScheduleBLL.NegativeMarks = chkNegativeMarks.Checked;

                _examScheduleBLL.PerQuestionTime = chkPerQuestionTime.Checked;
                _examScheduleBLL.AllowReview = chkAllowReview.Checked;
                if (ddlPatternId.SelectedIndex > 0)
                    _examScheduleBLL.PatternId = ddlPatternId.SelectedValue;
                else
                    _examScheduleBLL.PatternId = null;

                _examScheduleBLL.SendNotification = chkSendNotification.Checked;

                bool isValid = Validate();

                if (isValid == true)
                {
                    _examScheduleBLL.Save();

                    if (Request.QueryString["ExamScheduleId"] == null)
                    {
                        Reset();
                        Session["_examScheduleBLL"] = null;
                        Session["_examScheduleBLL"] = new ExamScheduleBLL();
                        _examScheduleBLL = (ExamScheduleBLL)Session["_examScheduleBLL"];
                    }
                    else
                    {
                        Session["_examScheduleBLL"] = null;
                        Response.Redirect("ExamSchedules.aspx");
                    }
                }
                //ShowErrors("", "Please Select Students");
            }
            else
            {
                if (chkIsDefaultTest.Checked)
                {
                    _examScheduleBLL.IsDefaultTest = true;
                }
                else
                {
                    _examScheduleBLL.IsDefaultTest = false;
                }

                if (ddlStandard.SelectedIndex > 0)
                    _examScheduleBLL.StandardTextListId = ddlStandard.SelectedValue;
                else
                    _examScheduleBLL.StandardTextListId = null;

                if (ddlSubs.SelectedIndex > 0)
                    _examScheduleBLL.SubId = ddlSubs.SelectedValue;
                else
                    _examScheduleBLL.SubId = null;

                if (ddlTestId.SelectedIndex > 0)
                    _examScheduleBLL.TestId = ddlTestId.SelectedValue;
                else
                    _examScheduleBLL.TestId = null;

                if (lblTotalQuestion.Text.Trim().Length > 0)
                    _examScheduleBLL.TotalQuestions = Convert.ToInt16(lblTotalQuestion.Text.ToString());
                else
                    _examScheduleBLL.TotalQuestions = null;

                if (txtExamDate.Text.Trim().Length > 0)
                    _examScheduleBLL.ExamDate = Convert.ToDateTime(txtExamDate.Text.Trim().ToString());
                else
                    _examScheduleBLL.ExamDate = null;

                if (txtExamFromTime.Text.Trim().Length > 0 && txtExamDate.Text.Trim().Length > 0)
                    _examScheduleBLL.ExamFromTime = Convert.ToDateTime(txtExamDate.Text + " " + txtExamFromTime.Text);
                else
                    _examScheduleBLL.ExamFromTime = null;

                if (txtExamToTime.Text.Trim().Length > 0 && txtExamDate.Text.Trim().Length > 0)
                    _examScheduleBLL.ExamToTime = Convert.ToDateTime(txtExamDate.Text + " " + txtExamToTime.Text);
                else
                    _examScheduleBLL.ExamToTime = null;

                if (lblTotalExamMinutes.Text.Trim().Length > 0)
                    _examScheduleBLL.TotalMins = Convert.ToInt16(lblTotalExamMinutes.Text.Trim().ToString());
                else
                    _examScheduleBLL.TotalMins = null;

                if (lblPerQuestionMinutes.Text.Trim().Length > 0)
                    _examScheduleBLL.PerQueMins = Convert.ToDecimal(lblPerQuestionMinutes.Text.Trim().ToString());
                else
                    _examScheduleBLL.PerQueMins = null;

                if (ChkShowResult.Checked)
                {
                    _examScheduleBLL.ShowResult = true;
                    _examScheduleBLL.MinsforResultShow = Convert.ToInt16(txtMinsforResultShow.Text.Trim().ToString());
                }
                else
                {
                    _examScheduleBLL.ShowResult = false;
                    _examScheduleBLL.MinsforResultShow = null;
                }

                if (Request.QueryString["ExamScheduleId"] == null)
                {
                    for (int c = 0; c <= _examScheduleBLL.dtRestrations.Columns.Count - 1; c++)
                    {
                        _examScheduleBLL.dtRestrations.Columns[c].ReadOnly = false;
                    }
                    for (int i = 0; i <= gdvRegistrations.Rows.Count - 1; i++)
                    {
                        GridViewRow gvr = gdvRegistrations.Rows[i];

                        CheckBox chkSendPendingFeeSMS = (CheckBox)gdvRegistrations.Rows[i].Cells[0].FindControl("chkSendPendingFeeSMS");

                        _examScheduleBLL.dtRestrations.Rows[i]["Tick"] = chkSendPendingFeeSMS.Checked;

                        if (chkSendPendingFeeSMS.Checked)
                            _examScheduleBLL.SkipMobile += gdvRegistrations.Rows[i].Cells[0].ToString();
                    }
                }

                //SkipMobile
                //for (int i = 0; i <= gdvRegistrations.Rows.Count - 1; i++)
                //{
                //    GridViewRow gvr = gdvRegistrations.Rows[i];

                //    CheckBox chkSendPendingFeeSMS = (CheckBox)gdvRegistrations.Rows[i].Cells[0].FindControl("chkSendPendingFeeSMS");

                //    _examScheduleBLL.dtRestrations.Rows[i]["Tick"] = chkSendPendingFeeSMS.Checked;
                //}

                _examScheduleBLL.NegativeMarks = chkNegativeMarks.Checked;

                _examScheduleBLL.PerQuestionTime = chkPerQuestionTime.Checked;
                _examScheduleBLL.AllowReview = chkAllowReview.Checked;
                if (ddlPatternId.SelectedIndex > 0)
                    _examScheduleBLL.PatternId = ddlPatternId.SelectedValue;
                else
                    _examScheduleBLL.PatternId = null;

                _examScheduleBLL.SendNotification = chkSendNotification.Checked;

                bool isValid = Validate();

                if (isValid == true)
                {
                    _examScheduleBLL.Save();

                    if (Request.QueryString["ExamScheduleId"] == null)
                    {
                        Reset();
                        Session["_examScheduleBLL"] = null;
                        Session["_examScheduleBLL"] = new ExamScheduleBLL();
                        _examScheduleBLL = (ExamScheduleBLL)Session["_examScheduleBLL"];
                    }
                    else
                    {
                        Session["_examScheduleBLL"] = null;
                        Response.Redirect("ExamSchedules.aspx");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Session["_examScheduleBLL"] = null;

            if (Request.QueryString["ExamScheduleId"] == null)
            {
                Response.Redirect("~/General/Default.aspx");
            }
            else
            {
                Response.Redirect("ExamSchedules.aspx");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _examScheduleBLL.Delete(Request.QueryString["ExamScheduleId"]);
            Session["_examScheduleBLL"] = null;
            Response.Redirect("ExamSchedules.aspx");
        }
        catch (Exception ex)
        {

            ShowErrors("Error", ex.Message);
        }
    }
    #endregion

    private void LoadStandard()
    {
        try
        {
            ListItem li = new ListItem();

            ddlStandard.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlStandard.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _examScheduleBLL.LoadStandard();

            foreach (DataRow dtr in dt.Rows)
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
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadDivision()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDivision.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlDivision.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _examScheduleBLL.LoadDivision();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlDivision.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
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
            dt = _examScheduleBLL.LoadSubjects();

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
    private void LoadTest()
    {
        try
        {
            ListItem li = new ListItem();

            ddlTestId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlTestId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _examScheduleBLL.LoadTest();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlTestId.Items.Add(li);

                li = null;
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
                _examScheduleBLL.StandardTextListId = ddlStandard.SelectedValue.ToString();
                LoadSubjects();
                LoadPatterns();
            }
            else
            {
                _examScheduleBLL.StandardTextListId = null;
                ShowErrors("", "Please select standard to generate schedule");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlSubs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubs.SelectedIndex > 0)
            {
                _examScheduleBLL.SubId = ddlSubs.SelectedValue.ToString();
                LoadTest();
                ddlPatternId.Enabled = false;
                ddlPatternId.SelectedIndex = 0;
            }
            else
            {
                ddlPatternId.Enabled = true;
                _examScheduleBLL.SubId = null;
                ShowErrors("", "Please select subject to generate schedule");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlTestId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTestId.SelectedIndex > 0)
            {
                _examScheduleBLL.TestId = ddlTestId.SelectedValue;
                DataTable dt = new DataTable();

                dt = _examScheduleBLL.LoadQuestions();

                if (dt.Rows.Count > 0)
                {
                    lblTotalQuestion.Text = dt.Rows[0][0].ToString();
                }

                hlTest.NavigateUrl = "../Exams/ViewResultDetailMasterSheet.aspx?TestId=" + _examScheduleBLL.TestId;
                hlTest.Target = "_blank";
            }
            else
            {
                _examScheduleBLL.TestId = null;
                ShowErrors("", "Please select test to generate schedule");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void txtExamDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtExamDate.Text.Trim().Length > 0)
            {
                _examScheduleBLL.ExamDate = Convert.ToDateTime(txtExamDate.Text.Trim().ToString());
                DataTable dt = new DataTable();

                dt = _examScheduleBLL.LoadExamsOnSameDate();

                if (dt.Rows.Count > 0)
                {
                    printHTML(dt);
                }
            }
            else
            {
                _examScheduleBLL.ExamDate = null;
                ShowErrors("", "Exam date can not be blank");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    public void printHTML(DataTable dt)
    {
        try
        {
            GeneralBLL _generalBLL = new GeneralBLL();
            Literal ltrHTMLReportView = new Literal();
            string Style1ColumnNumbers = "";
            string Style1RowNumbers = "";
            string Style1Css = "";
            string StyleCssHeader = "";
            string style = "";

            StyleCssHeader = "style='Color:blueViolet;font-size:bold;background-color:#E0F8E6;" + style + ";'";
            Style1Css = "style='Color:blueViolet;font-size:bold;background-color:#E0F8E6;'";

            int ShowRowTotal_StartColumnNo = 0;
            int ShowGrandTotal_StartRowNo = 0;
            decimal TotalAmt = 0;
            ltrHTMLReportView.Text = _generalBLL.GetHTMLfromDataTable(dt, "History", 2, StyleCssHeader, Style1Css, Style1ColumnNumbers, Style1RowNumbers, ShowRowTotal_StartColumnNo, ShowGrandTotal_StartRowNo, false, false, out TotalAmt, null);

            phHistory.Controls.Add(ltrHTMLReportView);
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }

    protected void txtExamFromTime_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtExamFromTime.Text.Trim().Length > 0 && txtExamToTime.Text.Trim().Length > 0)
            {
                DateTime dt2, dt1;

                dt1 = Convert.ToDateTime(txtExamFromTime.Text.ToString());
                dt2 = Convert.ToDateTime(txtExamToTime.Text.ToString());

                lblTotalExamMinutes.Text = Convert.ToInt64(dt2.Subtract(dt1).TotalMinutes).ToString();

                if (lblTotalExamMinutes.Text.Trim().Length > 0 && lblTotalQuestion.Text.Trim().Length > 0)
                {
                    lblPerQuestionMinutes.Text = Math.Round((Convert.ToDecimal(lblTotalExamMinutes.Text) / Convert.ToDecimal(lblTotalQuestion.Text)), 0).ToString("0.##");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }

    protected void txtExamToTime_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtExamFromTime.Text.Trim().Length > 0 && txtExamToTime.Text.Trim().Length > 0)
            {
                DateTime dt2, dt1;

                dt1 = Convert.ToDateTime(txtExamFromTime.Text.ToString());
                dt2 = Convert.ToDateTime(txtExamToTime.Text.ToString());

                lblTotalExamMinutes.Text = Convert.ToInt64(dt2.Subtract(dt1).TotalMinutes).ToString();

                if (lblTotalExamMinutes.Text.Trim().Length > 0 && lblTotalQuestion.Text.Trim().Length > 0)
                {
                    lblPerQuestionMinutes.Text = Math.Round((Convert.ToDecimal(lblTotalExamMinutes.Text) / Convert.ToDecimal(lblTotalQuestion.Text))).ToString("0.##");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
            {
                _examScheduleBLL.StandardTextListId = ddlStandard.SelectedValue.ToString();

                if (ddlDivision.SelectedIndex > 0)
                    _examScheduleBLL.DivisionTextListId = ddlDivision.SelectedValue;
                else
                    _examScheduleBLL.DivisionTextListId = null;

                _examScheduleBLL.LoadStudent();

                if (_examScheduleBLL.dtRestrations.Rows.Count > 0)
                {
                    gdvRegistrations.DataSource = _examScheduleBLL.dtRestrations;
                    gdvRegistrations.DataBind();

                    lblStudentCount.Text = "Total No Of Records [" + _examScheduleBLL.dtRestrations.Rows.Count + "]";
                }
                else
                {
                    lblStudentCount.Text = "Total No Of Records [" + 0 + "]";
                    ShowErrors("", "No student added in this standard.");
                }
            }
            else
            {
                lblStudentCount.Text = "Total No Of Records [" + 0 + "]";
                ShowErrors("", "You must have to select standard for searching student data");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }

    protected void gdvRegistrations_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;

                CheckBox chkSendPendingFeeSMS = (CheckBox)e.Row.FindControl("chkSendPendingFeeSMS");
                CheckBox chkSendPendingFeeSMSAll = (CheckBox)gdvRegistrations.HeaderRow.FindControl("chkSendPendingFeeSMSAll");

                chkSendPendingFeeSMS.Attributes.Add("onclick", "javascript:Selectchildcheckboxes('" + chkSendPendingFeeSMSAll.ClientID + "','" + gdvRegistrations.ClientID + "')");

                if (Request.QueryString["ExamScheduleId"] == null)
                {
                    chkSendPendingFeeSMS.Checked = true;
                    chkSendPendingFeeSMS.Visible = false;
                }
                else
                {
                    chkSendPendingFeeSMS.Checked = true;
                    chkSendPendingFeeSMS.Enabled = false;
                }
                chkSendPendingFeeSMS.Visible = true;
                chkSendPendingFeeSMSAll.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }

    protected void chkPerQuestionTime_CheckedChanged(object sender, EventArgs e)
    {
        if (chkPerQuestionTime.Checked)
        {
            lblPerQuestionMinuteslabel.Visible = true;
            lblPerQuestionMinutes.Visible = true;
            spanPerQuestionMinutes.Visible = true;
            //chkAllowReview.Visible = false;
            //lblAllowReview.Visible = false;
        }
        else
        {
            lblPerQuestionMinuteslabel.Visible = false;
            lblPerQuestionMinutes.Visible = false;
            spanPerQuestionMinutes.Visible = false;
            //chkAllowReview.Visible = true;
            //lblAllowReview.Visible = true;
        }
    }

    private void LoadPatterns()
    {
        try
        {
            ListItem li = new ListItem();

            ddlPatternId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlPatternId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _examScheduleBLL.LoadPatterns();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[2].ToString();
                li.Value = dtr[0].ToString();
                ddlPatternId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlPatternId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPatternId.SelectedIndex > 0)
            {
                _examScheduleBLL.PatternId = ddlPatternId.SelectedValue;
                LoadTestFromPatterns();
                ddlTestId.Focus();
                ddlSubs.Enabled = false;
                ddlSubs.SelectedIndex = 0;
            }
            else
            {
                ddlSubs.Enabled = true;
                _examScheduleBLL.PatternId = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }

    private void LoadTestFromPatterns()
    {
        try
        {
            ListItem li = new ListItem();

            ddlTestId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlTestId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _examScheduleBLL.LoadTestFromPatterns();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlTestId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
}
