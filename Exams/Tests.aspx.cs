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

public partial class Exams_Tests : System.Web.UI.Page
{
    private TestsBLL _testsBLL = new TestsBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        btnShowAllRecords.Visible = false;
        if (Page.IsPostBack == false)
        {

        }

        DataTable dt = new DataTable();
        dt.Columns.Add("TestName");
        dt.Columns.Add("Remarks");
        dt.Columns.Add("InsertedOn");
        dt.Columns.Add("LastUpdatedOn");
        DataRow dr = null;

        for (int i = 0; i < 10; i++)
        {
            dr = dt.NewRow(); // have new row on each iteration
            dr["TestName"] = i.ToString();
            dr["Remarks"] = (i * 1000).ToString();
            dr["InsertedOn"] = DateTime.Now;
            dr["LastUpdatedOn"] = DateTime.Now;
            dt.Rows.Add(dr);
        }
        gdvSubs.DataSource = dt;
        gdvSubs.DataBind();
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        if (txtName.Text.Trim().Length > 0)
            _testsBLL.Name = txtName.Text;

        _testsBLL.AllRecord = false;

        DataTable dt = new DataTable();
        dt = _testsBLL.Subs();
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
            _testsBLL.Name = txtName.Text;

        _testsBLL.AllRecord = true;

        DataTable dt = new DataTable();
        dt = _testsBLL.Subs();
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
            HyperLink hl1 = (HyperLink)e.Row.Cells[4].Controls[0];

            hl.NavigateUrl = "Test.aspx?TestId=" + drv[0].ToString();
            hl1.NavigateUrl = "ViewResultDetailMasterSheet.aspx?TestId=" + drv[0].ToString();

            hl1.Target = "_blank";
        }
    }
}
