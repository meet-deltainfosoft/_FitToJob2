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


public partial class General_LiveClass : System.Web.UI.Page
{
    LiveClassBLL _LiveClassBLL = new LiveClassBLL();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["LiveClassId"] == null)
                {
                    _LiveClassBLL = new LiveClassBLL();
                }
                else
                {
                    _LiveClassBLL = new LiveClassBLL(Request.QueryString["LiveClassId"].ToString());
                }
                Session["_LiveClassBLL"] = _LiveClassBLL;
            }
            else
            {
                _LiveClassBLL = (LiveClassBLL)Session["_LiveClassBLL"];
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
                if (Request.QueryString["LiveClassId"] != null)
                {
                    lblTitle.Text = " - [Edit Mode]";
                    LoadWebForm();
                    btnDelete.Enabled = true;
                    btnOK.Visible = true;
                }
                //else
                //{
                //    if (ddlStandard.SelectedIndex > 0 && ddlSubs.SelectedIndex > 0)
                //    {
                //        txtSrNo.Text = _LiveClassBLL.SrNo;
                //    }
                //}
                ddlStandard.Focus();
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
    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        lblStandard.CssClass = "";
        ddlStandard.CssClass = "";

        lblSubs.CssClass = "";
        ddlSubs.CssClass = "";

        lblTitle.CssClass = "";
        txtTitle.CssClass = "";

        lblLink.CssClass = "";
        txtLink.CssClass = "";

        lblDt.CssClass = "";
        txtDate.CssClass = "";

        lblFromTime.CssClass = "";
        txtFromTime.CssClass = "";

        lblToTime.CssClass = "";
        txtToTime.CssClass = "";

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

        if (key == "Title")
        {
            lblTitle.CssClass = "error";
            txtTitle.CssClass = "error";
        }

        if (key == "Link")
        {
            lblLink.CssClass = "error";
            txtLink.CssClass = "error";
        }

        if (key == "Date")
        {
            lblDt.CssClass = "error";
            txtDate.CssClass = "error";

        }

        if (key == "FromTime")
        {
            lblFromTime.CssClass = "error";
            txtFromTime.CssClass = "error";
        }

        if (key == "ToTime" || key == "ToTime1")
        {
            lblToTime.CssClass = "error";
            txtToTime.CssClass = "error";
        }

    }
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
            dt = _LiveClassBLL.LoadStandard();

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
            dt = _LiveClassBLL.LoadSubjects();

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

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
                _LiveClassBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _LiveClassBLL.StandardTextListId = null;

            if (ddlSubs.SelectedIndex > 0)
            {
                _LiveClassBLL.SubId = ddlSubs.SelectedValue;
                _LiveClassBLL.SubName = ddlSubs.SelectedItem.Text;
            }
            else
            {
                _LiveClassBLL.SubId = null;
                _LiveClassBLL.SubName = null;
            }

            if (txtTitle.Text.Trim().Length > 0)
                _LiveClassBLL.Title = txtTitle.Text.Trim();
            else
                _LiveClassBLL.Title = null;

            if (txtTopicName.Text.Trim().Length > 0)
                _LiveClassBLL.TopicName = txtTopicName.Text.Trim();
            else
                _LiveClassBLL.TopicName = null;

            if (txtLink.Text.Trim().Length > 0)
                _LiveClassBLL.Link = txtLink.Text.Trim();
            else
                _LiveClassBLL.Link = null;

            if (txtDate.Text.Trim().Length > 0)
                _LiveClassBLL.Date= Convert.ToDateTime(txtDate.Text.Trim());
            else
                _LiveClassBLL.Date = null;

            if (txtFromTime.Text.Trim().Length > 0)
                _LiveClassBLL.FromTime = Convert.ToDateTime(txtDate.Text + " " + txtFromTime.Text);
            else
                _LiveClassBLL.FromTime = null;

            if (txtToTime.Text.Trim().Length > 0)
                _LiveClassBLL.ToTime = Convert.ToDateTime(txtDate.Text + " " + txtToTime.Text);
            else
                _LiveClassBLL.ToTime = null;

            if (txtRemarks.Text.Trim().Length > 0)
                _LiveClassBLL.Remarks = txtRemarks.Text.Trim();
            else
                _LiveClassBLL.Remarks = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _LiveClassBLL.Save();

                if (Request.QueryString["LiveClassId"] == null)
                {
                    Reset();
                    Session["_LiveClassBLL"] = null;
                    Session["_LiveClassBLL"] = new LiveClassBLL();
                    _LiveClassBLL = (LiveClassBLL)Session["_LiveClassBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_LiveClassBLL"] = null;
                    Response.Redirect("LiveClasses.aspx");
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
            Session["_LiveClassBLL"] = null;

            if (Request.QueryString["LiveClassId"] == null)
            {
                Response.Redirect("~/General/Default.aspx");
            }
            else
            {
                Response.Redirect("LiveClasses.aspx");
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
            _LiveClassBLL.Delete(Request.QueryString["LiveClassId"]);
            Session["_LiveClassBLL"] = null;
            Response.Redirect("LiveClasses.aspx");
        }
        catch (Exception ex)
        {

            ShowErrors("Error", ex.Message);
        }
    }

    private bool Validate()
    {
        try
        {
            HideErrors();

            SortedList sl = _LiveClassBLL.Validate();

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

    private void Reset()
    {
        try
        {
            ddlStandard.SelectedIndex = 0;
            ddlSubs.SelectedIndex = 0;
            txtTitle.Text = "";
            txtTopicName.Text = "";
            txtLink.Text = "";
            txtFromTime.Text = "";
            txtToTime.Text = "";
            txtRemarks.Text = "";
            txtDate.Text = "";
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }

    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
            {
                _LiveClassBLL.StandardTextListId = ddlStandard.SelectedValue.ToString();
                LoadSubjects();
                ddlSubs.Focus();
            }
            else
            {
                _LiveClassBLL.StandardTextListId = null;

            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadWebForm()
    {
        try
        {
            if (_LiveClassBLL.StandardTextListId != null)
            {
                ddlStandard.SelectedValue = _LiveClassBLL.StandardTextListId.ToString();
                LoadSubjects();
                //ddlStandard_SelectedIndexChanged(null, null);
                //ddlStandard.Enabled = false;
            }
            if (_LiveClassBLL.SubId != null)
            {
                ddlSubs.SelectedValue = _LiveClassBLL.SubId.ToString();
            }

            if (_LiveClassBLL.Title != null)
                txtTitle.Text = _LiveClassBLL.Title.ToString();
            else
                txtTitle.Text = null;

            if (_LiveClassBLL.TopicName != null)
                txtTopicName.Text = _LiveClassBLL.TopicName.ToString();
            else
                txtTopicName.Text = null;

            if (_LiveClassBLL.Link != null)
                txtLink.Text = _LiveClassBLL.Link.ToString();
            else
                txtLink.Text = null;

            if (_LiveClassBLL.Date!= null)
                txtDate.Text = Convert.ToDateTime(_LiveClassBLL.Date).ToString("dd-MMM-yyyy");
            else
                txtDate.Text = null;

            if (_LiveClassBLL.FromTime != null)
                txtFromTime.Text = Convert.ToDateTime(_LiveClassBLL.FromTime).ToString("hh:mm tt");
            else
                txtFromTime.Text = null;

            if (_LiveClassBLL.ToTime != null)
                txtToTime.Text = Convert.ToDateTime(_LiveClassBLL.ToTime).ToString("hh:mm tt");
            else
                txtToTime.Text = null;

            if (_LiveClassBLL.Remarks != null)
                txtRemarks.Text = _LiveClassBLL.Remarks.ToString();
            else
                txtRemarks.Text = null;

        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }

}