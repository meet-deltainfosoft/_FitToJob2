using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections;

public partial class Exams_ExamCheckAllotment : System.Web.UI.Page
{
    private ExamCheckAllotmentBLL _examCheckAllotmentBLL = new ExamCheckAllotmentBLL();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["ExamCheckAllotmentId"] == null)
                {
                    _examCheckAllotmentBLL = new ExamCheckAllotmentBLL();
                }
                else
                {
                    _examCheckAllotmentBLL = new ExamCheckAllotmentBLL(Request.QueryString["ExamCheckAllotmentId"].ToString());
                }
                Session["_examCheckAllotmentBLL"] = _examCheckAllotmentBLL;
            }
            else
            {
                _examCheckAllotmentBLL = (ExamCheckAllotmentBLL)Session["_examCheckAllotmentBLL"];
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
                lblCounter.Text = "";

                if (Request.QueryString["ExamCheckAllotmentId"] != null)
                {
                    LoadWebForm();
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
            ShowErrors("err", ex.Message.ToString());
        }
    }

    private void LoadWebForm()
    {
        try
        {
            if (_examCheckAllotmentBLL.StandardTextListId != null)
            {
                ddlStandard.SelectedValue = _examCheckAllotmentBLL.StandardTextListId.ToString();
                ddlStandard_SelectedIndexChanged(null, null);
                ddlStandard.Enabled = false;
            }

            if (_examCheckAllotmentBLL.SubId != null)
            {
                ddlSubject.SelectedValue = _examCheckAllotmentBLL.SubId;
                ddlSubject_SelectedIndexChanged(null, null);
                ddlSubject.Enabled = false;
            }

            if (_examCheckAllotmentBLL.TestId != null)
            {
                ddlTest.SelectedValue = _examCheckAllotmentBLL.TestId;
                ddlTest_SelectedIndexChanged(null, null);
                ddlTest.Enabled = false;
            }

            if (_examCheckAllotmentBLL.ExamScheduleId != null)
            {
                ddlExamSchedule.SelectedValue = _examCheckAllotmentBLL.ExamScheduleId;
                ddlExamSchedule.Enabled = false;
            }

            DataTable dtEmplyee = new DataTable();
            dtEmplyee = _examCheckAllotmentBLL.LoadEmployee();
            Session["dtEmplyee"] = dtEmplyee;

            if (_examCheckAllotmentBLL.dtExamCheckAllotmentLns != null)
            {
                gdvResultDetail.DataSource = _examCheckAllotmentBLL.dtExamCheckAllotmentLns;
                gdvResultDetail.DataBind();

                lblCounter.Text = "Total No Of Records [" + _examCheckAllotmentBLL.dtExamCheckAllotmentLns.Rows.Count + "]";
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }


    protected void gdvResultDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;

                Label lblQues = (Label)e.Row.FindControl("lblQues");
                HyperLink hlImageQus = (HyperLink)e.Row.FindControl("hlImageQus");
                Image imgqusPics = (Image)e.Row.FindControl("imgqusPics");
                LinkButton lnkDownloadQues = (LinkButton)e.Row.FindControl("lnkDownloadQues");
                DropDownList ddlUserName = (DropDownList)e.Row.FindControl("ddlUserName");

                if (drv["Que"] != DBNull.Value)
                {
                    lblQues.Text = drv["Que"].ToString();
                    hlImageQus.Visible = false;
                    imgqusPics.Visible = false;
                }
                else
                {
                    if (drv["ImageNameQus"] != DBNull.Value)
                    {
                        lblQues.Visible = false;
                        hlImageQus.NavigateUrl = drv["ImageNameQus"].ToString();
                        imgqusPics.ImageUrl = drv["ImageNameQus"].ToString();
                        if (drv["QueType"] != null)
                        {
                            if (drv["QueType"].ToString().ToUpper() == "PDF")
                            {
                                lnkDownloadQues.CommandArgument = drv["ImageNameQus"].ToString();
                                lnkDownloadQues.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        imgqusPics.ImageUrl = "";
                        hlImageQus.NavigateUrl = "";
                        lnkDownloadQues.Visible = false;
                    }
                }

                if (Session["dtEmplyee"] != null)
                {
                    LoadEmployee(ddlUserName);

                    if (drv["UserId"] != DBNull.Value)
                    {
                        ddlUserName.SelectedValue = drv["UserId"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
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

            foreach (DataRow dtr in _examCheckAllotmentBLL.LoadStandard().Rows)
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

            foreach (DataRow dtr in _examCheckAllotmentBLL.LoadSubject().Rows)
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

            foreach (DataRow dtr in _examCheckAllotmentBLL.LoadTest().Rows)
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
                _examCheckAllotmentBLL.SubId = ddlSubject.SelectedValue;
                LoadTest();
            }
            else
            {
                _examCheckAllotmentBLL.SubId = null;
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
                _examCheckAllotmentBLL.StandardTextListId = ddlStandard.SelectedValue;
                LoadSubject();
            }
            else
            {
                _examCheckAllotmentBLL.StandardTextListId = null;
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
            if (ddlStandard.SelectedIndex > 0)
                _examCheckAllotmentBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _examCheckAllotmentBLL.StandardTextListId = null;

            if (ddlSubject.SelectedIndex > 0)
                _examCheckAllotmentBLL.SubId = ddlSubject.SelectedValue;
            else
                _examCheckAllotmentBLL.SubId = null;

            if (ddlTest.SelectedIndex > 0)
                _examCheckAllotmentBLL.TestId = ddlTest.SelectedValue;
            else
                _examCheckAllotmentBLL.TestId = null;

            if (ddlExamSchedule.SelectedIndex > 0)
                _examCheckAllotmentBLL.ExamScheduleId = ddlExamSchedule.SelectedValue.ToString();
            else
                _examCheckAllotmentBLL.ExamScheduleId = null;
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

            bool isValid = Validate();

            if (isValid)
            {
                DataTable dt = new DataTable();
                dt = _examCheckAllotmentBLL.LoadQuestions();

                DataTable dtEmplyee = new DataTable();
                dtEmplyee = _examCheckAllotmentBLL.LoadEmployee();
                Session["dtEmplyee"] = dtEmplyee;

                gdvResultDetail.DataSource = dt;
                gdvResultDetail.DataBind();

                lblCounter.Text = "Total No Of Records [" + dt.Rows.Count + "]";
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    private bool Validate()
    {
        HideErrors();

        SortedList sl = _examCheckAllotmentBLL.Validate();

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

    protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject.SelectedIndex > 0)
            {
                _examCheckAllotmentBLL.SubId = ddlSubject.SelectedValue;
            }
            else
            {
                _examCheckAllotmentBLL.SubId = null;
            }

            if (ddlTest.SelectedIndex > 0)
            {
                _examCheckAllotmentBLL.TestId = ddlTest.SelectedValue;
            }
            else
            {
                _examCheckAllotmentBLL.TestId = null;
            }

            if (_examCheckAllotmentBLL.SubId != null && _examCheckAllotmentBLL.TestId != null)
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

            foreach (DataRow dtr in _examCheckAllotmentBLL.LoadSchedule().Rows)
            {
                li = new ListItem();

                li.Text = dtr["SubName"].ToString() + "-" + dtr["TestName"].ToString() + "-" + Convert.ToDateTime(dtr["ExamFromTime"]).ToString("dd-MMM-yyyy hh:mm tt");
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

    private void LoadEmployee(DropDownList ddl)
    {
        try
        {
            ListItem li = new ListItem();

            ddl.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddl.Items.Add(li);

            li = null;

            DataTable dt = (DataTable)Session["dtEmplyee"];

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString() + " " + dtr[2].ToString();
                li.Value = dtr[0].ToString();
                ddl.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
                _examCheckAllotmentBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _examCheckAllotmentBLL.StandardTextListId = null;

            if (ddlSubject.SelectedIndex > 0)
                _examCheckAllotmentBLL.SubId = ddlSubject.SelectedValue;
            else
                _examCheckAllotmentBLL.SubId = null;

            if (ddlTest.SelectedIndex > 0)
                _examCheckAllotmentBLL.TestId = ddlTest.SelectedValue;
            else
                _examCheckAllotmentBLL.TestId = null;

            if (ddlExamSchedule.SelectedIndex > 0)
                _examCheckAllotmentBLL.ExamScheduleId = ddlExamSchedule.SelectedValue;
            else
                _examCheckAllotmentBLL.ExamScheduleId = null;

            for (int c = 0; c <= _examCheckAllotmentBLL.dtExamCheckAllotmentLns.Columns.Count - 1; c++)
            {
                _examCheckAllotmentBLL.dtExamCheckAllotmentLns.Columns[c].ReadOnly = false;
            }

            for (int i = 0; i <= gdvResultDetail.Rows.Count - 1; i++)
            {
                GridViewRow gvr = gdvResultDetail.Rows[i];

                DropDownList ddlUserName = (DropDownList)gdvResultDetail.Rows[i].Cells[0].FindControl("ddlUserName");

                if (ddlUserName.SelectedIndex > 0)
                    _examCheckAllotmentBLL.dtExamCheckAllotmentLns.Rows[i]["UserId"] = ddlUserName.SelectedValue;
                else
                    _examCheckAllotmentBLL.dtExamCheckAllotmentLns.Rows[i]["UserId"] = DBNull.Value;
            }

            bool isValid = Validate();

            if (isValid == true)
            {
                _examCheckAllotmentBLL.Save();

                ShowErrors("Success", "Employee assigned for exam paper checking.");

                Reset();
                Session["_examCheckAllotmentBLL"] = null;
                Session["_examCheckAllotmentBLL"] = new ExamCheckAllotmentBLL();
                _examCheckAllotmentBLL = (ExamCheckAllotmentBLL)Session["_examCheckAllotmentBLL"];

                if (Request.QueryString["ExamCheckAllotmentId"] != null)
                {
                    Session["_examCheckAllotmentBLL"] = null;
                    Response.Redirect("ExamCheckAllotments.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }

    private void Reset()
    {
        try
        {
            ddlStandard.SelectedIndex = 0;
            ddlSubject.SelectedIndex = 0;
            ddlTest.SelectedIndex = 0;
            ddlExamSchedule.SelectedIndex = 0;

            gdvResultDetail.DataSource = null;
            gdvResultDetail.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _examCheckAllotmentBLL.Delete(Request.QueryString["ExamCheckAllotmentId"].ToString());
            HideErrors();

            Reset();
            Response.Redirect("ExamCheckAllotments.aspx");
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }
}