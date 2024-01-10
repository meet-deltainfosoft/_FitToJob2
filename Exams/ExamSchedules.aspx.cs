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

public partial class Exams_ExamSchedules : System.Web.UI.Page
{
    private ExamScheduleBLL _examScheduleBLL = new ExamScheduleBLL();

    #region "Page Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                LoadStandard();

                txtFromExamDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("01-MMM-yyyy");
                txtToExamDate.Text = Convert.ToDateTime(System.DateTime.Now).ToString("dd-MMM-yyyy");
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
            }
            else
            {
                _examScheduleBLL.SubId = null;
                ShowErrors("", "Please select subject to generate schedule");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = _examScheduleBLL.LoadScheduleList(((ddlStandard.SelectedIndex > 0) ? ddlStandard.SelectedValue.ToString() : null)
                , ((ddlSubs.SelectedIndex > 0) ? ddlSubs.SelectedValue.ToString() : null)
                , ((ddlTestId.SelectedIndex > 0) ? ddlTestId.SelectedValue.ToString() : null)
                , Convert.ToDateTime(txtFromExamDate.Text)
                , Convert.ToDateTime(txtToExamDate.Text));

            if (dt.Rows.Count > 0)
            {
                gdvRegistrations.DataSource = dt;
                gdvRegistrations.DataBind();

                lblStudentCount.Text = "Total No Of Records [" + dt.Rows.Count + "]";
            }
            else
            {
                lblStudentCount.Text = "Total No Of Records [" + 0 + "]";
                ShowErrors("", "No record found for selected filter");
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
                HyperLink hl = (HyperLink)e.Row.Cells[0].Controls[0];

                hl.NavigateUrl = "ExamSchedule.aspx?ExamScheduleId=" + drv[0].ToString();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }
}
