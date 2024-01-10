using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class General_MasterValue : System.Web.UI.Page
{
    private MasterValueBLL _masterValueBLL;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {

            if (Request.QueryString["TextListId"] == null)
            {
                _masterValueBLL = new MasterValueBLL();
            }

            else
            {
                _masterValueBLL = new MasterValueBLL(Request.QueryString["TextListId"].ToString());
            }
            Session["_masterValueBLL"] = _masterValueBLL;
        }
        else
        {
            _masterValueBLL = (MasterValueBLL)Session["_masterValueBLL"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Load Dropdown List
            LoadGroup();

            if (Request.QueryString["TextListId"] != null)
            {
                lblTitle.Text = " - [Edit Mode]";

                LoadWebForm();
                btnDelete.Enabled = true;
            }
            else if (Request.QueryString["GroupName"] != null)
            {
                LoadGroup();
                ddlGroup.Items.FindByText(Request.QueryString["GroupName"].ToString()).Selected = true;
            }
        }
        ddlGroup.Focus();
    }

    private void LoadGroup()
    {
        ListItem li = new ListItem();

        ddlGroup.Items.Clear();

        li.Text = "<Select>";
        li.Value = "0";
        ddlGroup.Items.Add(li);

        li = null;

        foreach (DataRow dtr in _masterValueBLL.Groups().Rows)
        {
            li = new ListItem();

            li.Text = dtr[1].ToString();
            li.Value = dtr[0].ToString();
            ddlGroup.Items.Add(li);

            li = null;
        }
    }

    private bool Validate()
    {
        HideErrors();

        SortedList sl = _masterValueBLL.Validate();

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
        if (key == "Success")
            pnlErr.CssClass = "errors alert alert-success";
        else
            pnlErr.CssClass = "errors alert alert-danger";

        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));

        //Division
        if (key == "Group")
        {
            lblGroup.CssClass = "error";
            ddlGroup.CssClass = "error";
        }
        //Division

        //Text
        if (key == "Text")
        {
            lblText.CssClass = "error";
            txtText.CssClass = "error";
        }
        //Text

    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        //Group
        lblGroup.CssClass = "";
        ddlGroup.CssClass = "";

        //Text
        lblText.CssClass = "";
        txtText.CssClass = "";

    }

    private void Reset()
    {
        ddlGroup.SelectedIndex = 0;
        txtText.Text = "";

        //After new details are saved, it should refresh the dropdown of group
        LoadGroup();

        txtAddress.Text = "";
    }

    private void LoadWebForm()
    {
        ddlGroup.SelectedValue = _masterValueBLL.Group;
        txtText.Text = _masterValueBLL.Text;

        if (_masterValueBLL.Address != null)
            txtAddress.Text = _masterValueBLL.Address.ToString();
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlGroup.SelectedIndex > 0)
                _masterValueBLL.Group = ddlGroup.SelectedValue;
            else
                _masterValueBLL.Group = null;

            if (txtText.Text.Trim().Length > 0)
                _masterValueBLL.Text = txtText.Text;
            else
                _masterValueBLL.Text = null;

            if (txtAddress.Text.Trim().Length > 0)
                _masterValueBLL.Address = txtAddress.Text.ToString();
            else
                _masterValueBLL.Address = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _masterValueBLL.Save();

                if (Request.QueryString["TextListId"] == null)
                {
                    string strTextListId, strTextListName;
                    strTextListId = _masterValueBLL.TextListId;
                    strTextListName = _masterValueBLL.Text;

                    Reset();
                    ShowErrors("Success", "Record Saved Succsessfully.");
                    Session["_masterValueBLL"] = null;
                    Session["_masterValueBLL"] = new MasterValueBLL();

                    if (Request.QueryString["RowIndex"] != null && Request.QueryString["FormType"] != null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>SetRowValue2('" + ((Request["ctrl"] != null) ? (Request["ctrl"].ToString()) : ("")) + "','" + strTextListId + "','" + strTextListName + "','" + Request.QueryString["RowIndex"].ToString() + "','" + ((Request["hfTextListId"] != null) ? (Request["hfTextListId"].ToString()) : ("")) + "','" + ((Request["hfTextListName"] != null) ? (Request["hfTextListName"].ToString()) : ("")) + "','" + ((Request["hfRowIndex"] != null) ? (Request["hfRowIndex"].ToString()) : ("")) + "');</script>", false);
                    }
                    else if (Request.QueryString["FormType"] != null)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>SetRowValue('" + ((Request["ctrl"] != null) ? (Request["ctrl"].ToString()) : ("")) + "','" + strTextListId + "','" + strTextListName + "','" + ((Request["hfTextListId"] != null) ? (Request["hfTextListId"].ToString()) : ("")) + "','" + ((Request["hfTextListName"] != null) ? (Request["hfTextListName"].ToString()) : ("")) + "');</script>", false);
                    }
                }
                else
                {
                    Session["_masterValueBLL"] = null;
                    Response.Redirect("MasterValues.aspx");
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
        Session["_masterValueBLL"] = null;

        if (Request.QueryString["TextListId"] == null)
        {
            Response.Redirect("~/General/Default.aspx");
        }
        else
        {
            Response.Redirect("MasterValues.aspx");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _masterValueBLL.Delete(Request.QueryString["TextListId"]);
            Session["_masterValueBLL"] = null;
            Response.Redirect("MasterValues.aspx");
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGroup.SelectedIndex > 0)
        {
            if (ddlGroup.SelectedIndex > 0)
                _masterValueBLL.Group = ddlGroup.SelectedValue;
            else
                _masterValueBLL.Group = null;

            if (ddlGroup.SelectedItem.Text.ToString().ToUpper() == "Standard".ToString().ToUpper())
            {
                txtAddress.Visible = true;
                lblAddress.Text = "Mobile Number : ";
            }
            else
            {
                txtAddress.Visible = false;
                lblAddress.Text = "Other : ";
            }
        }
    }
}
