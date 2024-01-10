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

public partial class General_HomeWorkView : System.Web.UI.Page
{
    private HomeWorksBLL _homeWorksBLL = new HomeWorksBLL();
    private HomeWorkBLL _homeWorkBLL = new HomeWorkBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadStandard();
        }
        else
        {
            HideErrors();
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (txtQue.Text.Trim().Length > 0)
            _homeWorksBLL.Question = txtQue.Text.Trim();

        if (ddlStandardTextListId.SelectedIndex > 0)
            _homeWorksBLL.StandardId = ddlStandardTextListId.SelectedValue;
        else
            _homeWorksBLL.StandardId = null;

        if (ddlSubId.SelectedIndex > 0)
            _homeWorksBLL.SubId = ddlSubId.SelectedValue;
        else
            _homeWorksBLL.SubId = null;

        if (ddlChapterId.SelectedIndex > 0)
            _homeWorksBLL.ChapterId = ddlChapterId.SelectedValue;
        else
            _homeWorksBLL.ChapterId = null;

        if (txtStudentName.Text.Trim().Length > 0)
            _homeWorksBLL.StudentName = txtStudentName.Text.Trim().ToString();
        else
            _homeWorksBLL.StudentName = null;

        if (txtMobileNo.Text.Trim().Length > 0)
            _homeWorksBLL.MobileNumber = txtMobileNo.Text.Trim().ToString();
        else
            _homeWorksBLL.MobileNumber = null;

        _homeWorksBLL.AllRecord = false;

        DataTable dt = new DataTable();
        dt = _homeWorksBLL.FilterForStudentHomework();
        gdvhomeworks.DataSource = dt;

        if (dt.Rows.Count >= 30)
        {
            lblRecordStatus.Text = "Top  [ " + dt.Rows.Count.ToString() + " ]" + " Records ";
        }
        else
        {
            lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";
        }

        gdvhomeworks.DataBind();
    }

    protected void gdvhomeworks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[9].Controls[0];

            //HyperLink hlA1 = (HyperLink)e.Row.Cells[3].Controls[0];
            //HyperLink hlA2 = (HyperLink)e.Row.Cells[4].Controls[0];
            //HyperLink hlA3 = (HyperLink)e.Row.Cells[5].Controls[0];
            //HyperLink hlA4 = (HyperLink)e.Row.Cells[6].Controls[0];

            hl.NavigateUrl = "../Exams/ViewResultDetail.aspx?RegistrationId=" + drv["RegistrationId"].ToString() + "&HomeWorkId=" + drv["HomeWorkId"].ToString();
            hl.Target = "_blank";

            //if (drv["ImageNameA1"] != DBNull.Value)
            //{
            //    hlA1.NavigateUrl = drv["ImageNameA1"].ToString();
            //    hlA1.Target = "_blank";
            //}

            //if (drv["ImageNameA2"] != DBNull.Value)
            //{
            //    hlA2.NavigateUrl = drv["ImageNameA2"].ToString();
            //    hlA2.Target = "_blank";
            //}

            //if (drv["ImageNameA3"] != DBNull.Value)
            //{
            //    hlA3.NavigateUrl = drv["ImageNameA3"].ToString();
            //    hlA3.Target = "_blank";
            //}

            //if (drv["ImageNameA4"] != DBNull.Value)
            //{
            //    hlA4.NavigateUrl = drv["ImageNameA4"].ToString();
            //    hlA4.Target = "_blank";
            //}
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
            dt = _homeWorkBLL.LoadSubjects(ddlStandardTextListId.SelectedValue.ToString());

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
    private void LoadChapter()
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
            dt = _homeWorkBLL.LoadChapters();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[2].ToString();
                li.Value = dtr[0].ToString();
                ddlChapterId.Items.Add(li);

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
            dt = _homeWorkBLL.LoadStandard();

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
    protected void ddlSubId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubId.SelectedIndex > 0)
            {
                _homeWorkBLL.SubId = ddlSubId.SelectedValue;
                LoadChapter();
            }
            else
            {
                _homeWorkBLL.SubId = null;
                //   ShowErrors("err", "You have to select Subject");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }
}

