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

public partial class Exams_Test : System.Web.UI.Page
{
    private TestBLL _testBLL;

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["TestId"] == null)
            {
                _testBLL = new TestBLL();
            }
            else
            {
                _testBLL = new TestBLL(Request.QueryString["TestId"].ToString());
            }
            Session["_testBLL"] = _testBLL;
        }
        else
        {
            _testBLL = (TestBLL)Session["_testBLL"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadStandard();
           // LoadSubjects();

            if (Request.QueryString["TestId"] != null)
            {
                lblTitle.Text = " - [Edit Mode]";
                LoadWebForm();
                btnDelete.Enabled = true;
            }
            else
            {

            }
            txtTestName.Focus();
        }
    }
    #endregion

    #region "Subs Functions"

    private bool Validate()
    {
        HideErrors();

        SortedList sl = _testBLL.Validate();

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

    private void ShowErrors(string key, string value)
    {
        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));

        //Name
        if (key == "Names")
        {
            lblTestName.CssClass = "error";
            txtTestName.CssClass = "error";
        }
        //Name

        if (key == "SubId")
        {
            lblSubId.CssClass = "error";
            ddlSubId.CssClass = "error";
        }
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        lblTestName.CssClass = "";
        txtTestName.CssClass = "";

        lblSubId.CssClass = "";
        ddlSubId.CssClass = "";
    }

    private void Reset()
    {
        ddlSubId.SelectedIndex = 0;
        txtTestName.Text = "";
        txtRemark.Text = "";
    }

    private void LoadWebForm()
    {
        if (_testBLL.TestName != null)
            txtTestName.Text = _testBLL.TestName;

        if (_testBLL.Remarks != null)
            txtRemark.Text = _testBLL.Remarks;

        if (_testBLL.StandardTextListId != null)
        {
            ddlStdId.SelectedValue = _testBLL.StandardTextListId;
            ddlStdId_SelectedIndexChanged(null, null);
            ddlStdId.Enabled = false;
        }

        if (_testBLL.SubId != null)
        {
            ddlSubId.SelectedValue = _testBLL.SubId;
        }
    }

    #endregion

    #region "Subs Events"
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTestName.Text.Trim().Length > 0)
                _testBLL.TestName = txtTestName.Text.Trim();
            else
                _testBLL.TestName = null;

            if (txtRemark.Text.Trim().Length > 0)
                _testBLL.Remarks = txtRemark.Text.Trim();
            else
                _testBLL.Remarks = null;

            if (ddlSubId.SelectedIndex > 0)
                _testBLL.SubId = ddlSubId.SelectedValue.ToString();
            else
                _testBLL.SubId = null;

            if (ddlStdId.SelectedIndex > 0)
                _testBLL.StandardTextListId = ddlStdId.SelectedValue.ToString();
            else
                _testBLL.StandardTextListId = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _testBLL.Save();

                if (Request.QueryString["TestId"] == null)
                {
                    Reset();
                    Session["_testBLL"] = null;
                    Session["_testBLL"] = new TestBLL();
                    _testBLL = (TestBLL)Session["_testBLL"];
                }
                else
                {
                    Session["_testBLL"] = null;
                    Response.Redirect("Tests.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["_testBLL"] = null;

        if (Request.QueryString["TestId"] == null)
        {
            Response.Redirect("~/General/Default.aspx");
        }
        else
        {
            Response.Redirect("Tests.aspx");
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            //_testBLL.Delete(Request.QueryString["TestId"]);
            //Session["_testBLL"] = null;
            //Response.Redirect("Subs.aspx");
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }
    #endregion

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
            dt = _testBLL.LoadSubjects(((ddlStdId.SelectedIndex > 0) ? ddlStdId.SelectedValue : null));

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
    //private void LoadSubjects()
    //{
    //    try
    //    {
    //        ListItem li = new ListItem();

    //        ddlSubId.Items.Clear();

    //        li.Text = "<Select>";
    //        li.Value = "0";
    //        ddlSubId.Items.Add(li);

    //        li = null;

    //        DataTable dt = new DataTable();
    //        dt = _testBLL.LoadSubjects();

    //        foreach (DataRow dtr in dt.Rows)
    //        {
    //            li = new ListItem();

    //            li.Text = dtr[1].ToString();
    //            li.Value = dtr[0].ToString();
    //            ddlSubId.Items.Add(li);

    //            li = null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowErrors("err", ex.Message);
    //    }
    //}
    private void LoadStandard()
    {
        try
        {
            ListItem li = new ListItem();

            ddlStdId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlStdId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _testBLL.LoadStandard();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlStdId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlStdId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStdId.SelectedIndex > 0)
            LoadSubjects();
        else
            ShowErrors("err", "Please select standard");
    }
}
