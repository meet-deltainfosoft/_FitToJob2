using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Admin_User : System.Web.UI.Page
{
    UserBLL _userBLL;

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["UserId"] == null)
            {
                _userBLL = new UserBLL();
            }
            else
            {
                _userBLL = new UserBLL(Request.QueryString["UserId"].ToString());
            }

            Session["_userBLL"] = _userBLL;
        }
        else
        {
            _userBLL = (UserBLL)Session["_userBLL"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Form.DefaultButton = this.btnOK.UniqueID;
        if (Page.IsPostBack == false)
        {
            //LoadDept();
            if (Request.QueryString["UserId"] != null) //Edit Mode
            {
                lblTitle.Text = " - [Edit Mode]";

                LoadWebForm(false);
                btnDelete.Enabled = true;
            }
            else //New Mode
            {
                LoadWebForm(true);
            }
        }
    }
    private void LoadDept()
    {
        ListItem li = new ListItem();

        ddlDept.Items.Clear();

        li.Text = "<Select>";
        li.Value = "0";
        ddlDept.Items.Add(li);

        li = null;

        foreach (DataRow dtr in _userBLL.LoadDept().Rows)
        {
            li = new ListItem();

            li.Text = dtr[1].ToString();
            li.Value = dtr[0].ToString();
            ddlDept.Items.Add(li);

            li = null;
        }
    }

    #endregion

    #region "Indent Events"

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            //First Name
            if (txtFirstName.Text.Trim().Length > 0)
                _userBLL.FirstName = txtFirstName.Text.Trim();
            else
                _userBLL.FirstName = null;

            //Last Name
            if (txtLastName.Text.Trim().Length > 0)
                _userBLL.LastName = txtLastName.Text.Trim();
            else
                _userBLL.LastName = null;

            //UserName
            if (txtUserName.Text.Trim().Length > 0)
                _userBLL.UserName = txtUserName.Text.Trim();
            else
                _userBLL.UserName = null;

            //Password
            if (txtPassword.Text.Trim().Length > 0)
                _userBLL.Password = txtPassword.Text.Trim();
            else
                _userBLL.Password = null;

            if (ddlDept.SelectedIndex > 0)
            {
                _userBLL.DeptId = ddlDept.SelectedValue;
            }

            //IsDisabled
            _userBLL.IsDisabled = chkIsDisabled.Checked;


            //Add Selected AppRoleIds to ArrayList in BLL

            //Clear Existing Form from ArrayList
            _userBLL.UserAppRoles.Clear();

            string aapRoleId;
            CheckBox chkSelectAppRole;

            foreach (GridViewRow gvr in gdvAppRoles.Rows)
            {
                chkSelectAppRole = (CheckBox)(gvr.FindControl("chkSelectAppRole"));
                if (chkSelectAppRole.Checked == true)
                {
                    aapRoleId = gdvAppRoles.DataKeys[gvr.RowIndex].Value.ToString();
                    _userBLL.UserAppRoles.Add(aapRoleId);
                }
            }
            //Add Selected AppRoleIds to ArrayList in BLL

            bool isValid = Validate();

            if (isValid == true)
            {
                _userBLL.Save();

                if (Request.QueryString["UserId"] == null) //New Mode
                {
                    Session["_userBLL"] = null;
                    Session["_userBLL"] = new UserBLL();
                    _userBLL = (UserBLL)Session["_userBLL"];
                    Reset();
                }
                else //Edit Mode
                {
                    Session["_userBLL"] = null;
                    Response.Redirect("Users.aspx");
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
        Session["_userBLL"] = null;

        if (Request.QueryString["UserId"] == null)
        {
            Response.Redirect("~/General/Default.aspx");
        }
        else
        {
            Response.Redirect("Users.aspx");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _userBLL.Delete(Request.QueryString["UserId"]);
            Session["_userBLL"] = null;
            Response.Redirect("Users.aspx");
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }

    #endregion

    #region "Indent Functions"

    private void LoadWebForm(bool isNew)
    {
        if (isNew == true)
        {
            _userBLL.GetAllAppRoles();
        }
        else
        {
            txtFirstName.Text = _userBLL.FirstName;
            txtLastName.Text = _userBLL.LastName;
            txtUserName.Text = _userBLL.UserName;
            txtPassword.Text = _userBLL.Password;
            txtPassword.Attributes.Add("value", _userBLL.Password);
            ddlDept.SelectedValue = _userBLL.DeptId;
            chkIsDisabled.Checked = _userBLL.IsDisabled;
        }

        //Bind Forms to Gridview
        gdvAppRoles.Enabled = true;
        gdvAppRoles.DataSource = _userBLL.AllAppRoles;
        gdvAppRoles.DataBind();
    }

    private bool Validate()
    {
        HideErrors();

        SortedList sl = _userBLL.Validate();

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

        //FirstName
        if (key == "FirstName")
        {
            lblFirstName.CssClass = "error";
            txtFirstName.CssClass = "error";
        }
        //FirstName

        //LastName
        if (key == "LastName")
        {
            lblLastName.CssClass = "error";
            txtLastName.CssClass = "error";
        }
        //LastName

        //UserName
        if (key == "UserName")
        {
            lblUserName.CssClass = "error";
            txtUserName.CssClass = "error";
        }
        //UserName

        //Password
        if (key == "Password")
        {
            lblPassword.CssClass = "error";
            txtPassword.CssClass = "error";
        }
        //Password
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        //FirstName
        lblFirstName.CssClass = "";
        txtFirstName.CssClass = "";
        //FirstName

        //LastName
        lblLastName.CssClass = "";
        txtLastName.CssClass = "";
        //LastName

        //UserName
        lblUserName.CssClass = "";
        txtUserName.CssClass = "";
        //UserName

        //Password
        lblPassword.CssClass = "";
        txtPassword.CssClass = "";
        //Password
    }

    private void Reset()
    {
        txtFirstName.Text = "";
        txtLastName.Text = "";
        txtUserName.Text = "";
        txtPassword.Text = "";
        chkIsDisabled.Checked = false;

        //clear Grid Details
        _userBLL.GetAllAppRoles();
        gdvAppRoles.DataSource = _userBLL.AllAppRoles;
        gdvAppRoles.DataBind();
    }

    #endregion
}
