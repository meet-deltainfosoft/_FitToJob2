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

public partial class Exams_Subs : System.Web.UI.Page
{
    private SubsBLL _SubsBLL = new SubsBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        btnShowAllRecords.Visible = false;
        if (Page.IsPostBack == false)
        {
            LoadStandard();
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
            dt = _SubsBLL.LoadStandard();

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
        }
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (txtName.Text.Trim().Length > 0)
            _SubsBLL.Name = txtName.Text;
        else
            _SubsBLL.Name = null;

        if (ddlStandardTextListId.SelectedIndex > 0)
            _SubsBLL.StandardTextListId = ddlStandardTextListId.SelectedValue;
        else
            _SubsBLL.StandardTextListId = null;

        _SubsBLL.AllRecord = false;

        DataTable dt = new DataTable();
        dt = _SubsBLL.Subs();
        gdvSubs.DataSource = dt;

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

        gdvSubs.DataBind();
    }
    protected void btnShowAllRecords_Click(object sender, EventArgs e)
    {
        if (txtName.Text.Trim().Length > 0)
            _SubsBLL.Name = txtName.Text;

        _SubsBLL.AllRecord = true;

        DataTable dt = new DataTable();
        dt = _SubsBLL.Subs();
        gdvSubs.DataSource = dt;

        lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";

        gdvSubs.DataBind();

    }
    protected void gdvSubs_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[0].Controls[0];

            hl.NavigateUrl = "Sub.aspx?SubId=" + drv[0].ToString();
        }
    }
}
