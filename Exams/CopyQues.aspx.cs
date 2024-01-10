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

public partial class Exams_CopyQues : System.Web.UI.Page
{
    private QuesBLL _QuesBLL = new QuesBLL();
    private QueBLL _queBLL = new QueBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadStandard();
            LoadStandardTo();
            btnShowAllRecords.Visible = false;
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (txtQue.Text.Trim().Length > 0)
            _QuesBLL.Question = txtQue.Text.Trim();

        if (ddlStandardTextListId.SelectedIndex > 0)
            _QuesBLL.StandardId = ddlStandardTextListId.SelectedValue;
        else
            _QuesBLL.StandardId = null;

        if (ddlSubId.SelectedIndex > 0)
            _QuesBLL.SubId = ddlSubId.SelectedValue;
        else
            _QuesBLL.SubId = null;

        if (ddlTestId.SelectedIndex > 0)
            _QuesBLL.TestId = ddlTestId.SelectedValue;
        else
            _QuesBLL.TestId = null;

        _QuesBLL.AllRecord = true;

        if (ddlStandardTextListId.SelectedIndex > 0 && ddlSubId.SelectedIndex > 0)
        {
            DataTable dt = new DataTable();
            dt = _QuesBLL.Filter();
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
        else
        {
            ShowErrors("err", "Select mandatory field for filter data.");
        }
    }

    protected void btnShowAllRecords_Click(object sender, EventArgs e)
    {
        if (txtQue.Text.Trim().Length > 0)
            _QuesBLL.Question = txtQue.Text.Trim();

        if (ddlStandardTextListId.SelectedIndex > 0)
            _QuesBLL.StandardId = ddlStandardTextListId.SelectedValue;
        else
            _QuesBLL.StandardId = null;

        if (ddlSubId.SelectedIndex > 0)
            _QuesBLL.SubId = ddlSubId.SelectedValue;
        else
            _QuesBLL.SubId = null;

        if (ddlTestId.SelectedIndex > 0)
            _QuesBLL.TestId = ddlTestId.SelectedValue;
        else
            _QuesBLL.TestId = null;

        _QuesBLL.AllRecord = true;

        DataTable dt = new DataTable();
        dt = _QuesBLL.Filter();
        gdvQues.DataSource = dt;

        lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";

        gdvQues.DataBind();

    }

    protected void gdvQues_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[0].Controls[0];

            HyperLink hlA1 = (HyperLink)e.Row.Cells[2].Controls[0];
            HyperLink hlA2 = (HyperLink)e.Row.Cells[3].Controls[0];
            HyperLink hlA3 = (HyperLink)e.Row.Cells[4].Controls[0];
            HyperLink hlA4 = (HyperLink)e.Row.Cells[5].Controls[0];

            hl.NavigateUrl = "Que.aspx?QueId=" + drv[0].ToString();

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

            CheckBox chkSendPendingFeeSMS = (CheckBox)e.Row.FindControl("chkSendPendingFeeSMS");
            CheckBox chkSendPendingFeeSMSAll = (CheckBox)gdvQues.HeaderRow.FindControl("chkSendPendingFeeSMSAll");

            chkSendPendingFeeSMS.Attributes.Add("onclick", "javascript:Selectchildcheckboxes('" + chkSendPendingFeeSMSAll.ClientID + "','" + gdvQues.ClientID + "')");
        }
    }

    protected void ddlStandardTextListId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandardTextListId.SelectedIndex > 0)
            {
                LoadSubjects();
                HideErrors();
            }
            else
            {
                ShowErrors("err", "You have to select Standard");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }
    protected void ddlStandardTextListIdTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandardTextListIdTo.SelectedIndex > 0)
            {
                LoadSubjectsTo();
            }
            else
            {
                ShowErrors("err", "You have to select Standard");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
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
            dt = _queBLL.LoadSubjects(ddlStandardTextListId.SelectedValue.ToString());

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
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadSubjectsTo()
    {
        try
        {
            ListItem li = new ListItem();

            ddlSubIdTo.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlSubIdTo.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _queBLL.LoadSubjects(ddlStandardTextListIdTo.SelectedValue.ToString());

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSubIdTo.Items.Add(li);

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
            dt = _queBLL.LoadTest();

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
    private void LoadTestTo()
    {
        try
        {
            ListItem li = new ListItem();

            ddlTestIdTo.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlTestIdTo.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _queBLL.LoadTestTo();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlTestIdTo.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

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
            dt = _queBLL.LoadStandard();

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
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadStandardTo()
    {
        try
        {
            ListItem li = new ListItem();

            ddlStandardTextListIdTo.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlStandardTextListIdTo.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _queBLL.LoadStandard();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlStandardTextListIdTo.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlSubId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubId.SelectedIndex > 0)
            {
                _queBLL.SubId = ddlSubId.SelectedValue;
                LoadTest();
            }
            else
            {
                _queBLL.SubId = null;
                ShowErrors("err", "You have to select Subject");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }
    protected void ddlSubIdTo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubIdTo.SelectedIndex > 0)
            {
                _queBLL.SubIdTo = ddlSubIdTo.SelectedValue;
                LoadTestTo();
            }
            else
            {
                _queBLL.SubIdTo = null;
                ShowErrors("err", "You have to select Subject");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }
    protected void BtnCopyTo_Click(object sender, EventArgs e)
    {
        try
        {
            ArrayList al = new ArrayList();

            foreach (GridViewRow gvr in gdvQues.Rows)
            {
                CheckBox chkSendPendingFeeSMS = (CheckBox)gvr.FindControl("chkSendPendingFeeSMS");

                if (chkSendPendingFeeSMS.Checked)
                    al.Add(gdvQues.DataKeys[gvr.RowIndex].Values[0].ToString());
            }

            if (al.Count > 0)
            {
                _queBLL.SubIdTo = ddlSubIdTo.SelectedValue;
                _queBLL.TestIdTo = ddlTestIdTo.SelectedValue;
                _queBLL.PasteQuestionFrom(al);
                ShowErrors("err", "Selected question is Copy successfully.");

                btnFilter_Click(null, null);
            }
            else
            {
                ShowErrors("err", "Please select atleast one question to Copy record.");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }
    protected void ddlTestIdTo_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}