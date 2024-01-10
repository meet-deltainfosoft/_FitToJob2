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

public partial class Exams_QueBanks : System.Web.UI.Page
{
    private QueBanksBLL _queBanksBLL = new QueBanksBLL();
    private QueBankBLL _queBankBLL = new QueBankBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadStandard();
                btnShowAllRecords.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtQue.Text.Trim().Length > 0)
                _queBanksBLL.Que = txtQue.Text.Trim();

            if (ddlStandardTextListId.SelectedIndex > 0)
                _queBanksBLL.StandardId = ddlStandardTextListId.SelectedValue;
            else
                _queBanksBLL.StandardId = null;

            if (ddlSubId.SelectedIndex > 0)
                _queBanksBLL.SubId = ddlSubId.SelectedValue;
            else
                _queBanksBLL.SubId = null;

            if (ddlChapterId.SelectedIndex > 0)
                _queBanksBLL.ChapterId = ddlChapterId.SelectedValue;
            else
                _queBanksBLL.ChapterId = null;

            if (ddlPeriodNo.SelectedIndex > 0)
                _queBanksBLL.PeriodNo = ddlPeriodNo.SelectedValue;
            else
                _queBanksBLL.PeriodNo = null;

            _queBanksBLL.AllRecord = false;

            DataTable dt = new DataTable();
            dt = _queBanksBLL.Filter();
            gdvQues.DataSource = dt;

            if (dt.Rows.Count >= 30)
            {
                btnShowAllRecords.Visible = true;
                lblRecordStatus.Text = "Top  [ " + dt.Rows.Count.ToString() + " ]" + " Records ";
            }
            else
            {
                btnShowAllRecords.Visible = false;
                lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";
            }

            gdvQues.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }

    protected void btnShowAllRecords_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtQue.Text.Trim().Length > 0)
                _queBanksBLL._Que = txtQue.Text.Trim();

            if (ddlStandardTextListId.SelectedIndex > 0)
                _queBanksBLL.StandardId = ddlStandardTextListId.SelectedValue;
            else
                _queBanksBLL.StandardId = null;

            if (ddlSubId.SelectedIndex > 0)
                _queBanksBLL.SubId = ddlSubId.SelectedValue;
            else
                _queBanksBLL.SubId = null;

            if (ddlChapterId.SelectedIndex > 0)
                _queBanksBLL.ChapterId = ddlChapterId.SelectedValue;
            else
                _queBanksBLL.ChapterId = null;

            _queBanksBLL.AllRecord = true;

            DataTable dt = new DataTable();
            dt = _queBanksBLL.Filter();
            gdvQues.DataSource = dt;

            lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";

            gdvQues.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }

    }

    protected void gdvQues_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                HyperLink hl = (HyperLink)e.Row.Cells[0].Controls[0];

                HyperLink hlA1 = (HyperLink)e.Row.Cells[2].Controls[0];
                HyperLink hlA2 = (HyperLink)e.Row.Cells[3].Controls[0];
                HyperLink hlA3 = (HyperLink)e.Row.Cells[4].Controls[0];
                HyperLink hlA4 = (HyperLink)e.Row.Cells[5].Controls[0];

                hl.NavigateUrl = "QueBank.aspx?QueBankId=" + drv[0].ToString();

                if (drv["ImageNameA1"] != DBNull.Value)
                {
                    hlA1.NavigateUrl = drv["ImageNameA1"].ToString();
                    hlA1.Target = "_blank";
                }

                if (drv["ImageNameA2"] != DBNull.Value)
                {
                    hlA2.NavigateUrl = drv["ImageNameA2"].ToString();
                    hlA2.Target = "_blank";
                }

                if (drv["ImageNameA3"] != DBNull.Value)
                {
                    hlA3.NavigateUrl = drv["ImageNameA3"].ToString();
                    hlA3.Target = "_blank";
                }

                if (drv["ImageNameA4"] != DBNull.Value)
                {
                    hlA4.NavigateUrl = drv["ImageNameA4"].ToString();
                    hlA4.Target = "_blank";
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }

    protected void ddlStandardTextListId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandardTextListId.SelectedIndex > 0)
            {
                LoadSubjects();
            }
            else
            {
                ShowErrors("error", "You have to select Standard");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    private void LoadSubjects()
    {
        try
        {
            ListItem li = new ListItem();

            ddlSubId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlSubId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _queBankBLL.LoadSubjects(ddlStandardTextListId.SelectedValue.ToString());

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSubId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    private void LoadTest()
    {
        try
        {
            ListItem li = new ListItem();

            ddlChapterId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlChapterId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _queBankBLL.LoadChapter();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlChapterId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }

    private void ShowErrors(string key, string value)
    {
        try
        {
            pnlErr.Visible = true;
            blErrs.Items.Add(new ListItem(value));
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
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
            ShowErrors("error", ex.Message);
        }
    }
    private void LoadStandard()
    {
        try
        {
            ListItem li = new ListItem();

            ddlStandardTextListId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlStandardTextListId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _queBankBLL.LoadStandard();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlStandardTextListId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    protected void ddlSubId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubId.SelectedIndex > 0)
            {
                _queBankBLL.SubId = ddlSubId.SelectedValue;
                LoadTest();
            }
            else
            {
                _queBankBLL.SubId = null;
                ShowErrors("err", "You have to select Subject");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    protected void ddlChapterId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlChapterId.SelectedIndex > 0 && ddlSubId.SelectedIndex >0)
            {
                _queBanksBLL.ChapterId = ddlChapterId.SelectedValue;

                _queBanksBLL.SubId = ddlSubId.SelectedValue;
                LoadPeriodNo();
            }
            else
            {
                ddlPeriodNo.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }

    private void LoadPeriodNo()
    {
        try
        {
            ListItem li = new ListItem();

            ddlPeriodNo.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlPeriodNo.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _queBanksBLL.LoadPeriodNo();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[0].ToString();
                li.Value = dtr[0].ToString();
                ddlPeriodNo.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
}