using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Admin_AppRole : System.Web.UI.Page
{
    AppRoleBLL _appRoleBLL;
 
    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["AppRoleId"] == null)
            {
                _appRoleBLL = new AppRoleBLL();
            }
            else
            {
                _appRoleBLL = new AppRoleBLL(Request.QueryString["AppRoleId"].ToString());
            }

            Session["_appRoleBLL"] = _appRoleBLL;
        }
        else
        {
            _appRoleBLL = (AppRoleBLL)Session["_appRoleBLL"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["AppRoleId"] != null) //Edit Mode
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

    #endregion

    #region "Indent Events"

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            //Name
            if (txtName.Text.Trim().Length > 0)
                _appRoleBLL.Name = txtName.Text.Trim();
            else
                _appRoleBLL.Name = null;

            //Desc
            if (txtDesc.Text.Trim().Length > 0)
                _appRoleBLL.Desc = txtDesc.Text.Trim();
            else
                _appRoleBLL.Desc = null;


            //Add Selected FormIds to ArrayList in BLL

            //Clear Existing Form from ArrayList
            _appRoleBLL.AppRoleForms.Clear();

           string formId;
           CheckBox chkSelectedForm;

           foreach (GridViewRow gvr in gdvForms.Rows)
           {
               chkSelectedForm = (CheckBox)(gvr.FindControl("chkSelectForm"));
               if (chkSelectedForm.Checked == true)
               {
                   formId = gdvForms.DataKeys[gvr.RowIndex].Value.ToString();
                   _appRoleBLL.AppRoleForms.Add(formId);
               }
           }
           //Add Selected FormIds to ArrayList in BLL

            bool isValid = Validate();

            if (isValid == true)
            {
                _appRoleBLL.Save();

                if (Request.QueryString["AppRoleId"] == null) //New Mode
                {
                    Session["_appRoleBLL"] = null;
                    Session["_appRoleBLL"] = new AppRoleBLL();
                    _appRoleBLL = (AppRoleBLL)Session["_appRoleBLL"];
                    Reset();
                }
                else //Edit Mode
                {
                    Session["_appRoleBLL"] = null;
                    Response.Redirect("AppRoles.aspx");
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
        Session["_appRoleBLL"] = null;

        if (Request.QueryString["AppRoleId"] == null)
        {
            Response.Redirect("~/General/Default.aspx");
        }
        else
        {
            Response.Redirect("AppRoles.aspx");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _appRoleBLL.Delete(Request.QueryString["AppRoleId"]);
            Session["_appRoleBLL"] = null;
            Response.Redirect("AppRoles.aspx");
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
            _appRoleBLL.GetAllForms();
        }
        else
        {
            txtName.Text= _appRoleBLL.Name;
            txtDesc.Text = _appRoleBLL.Desc;
        }

        //Bind Forms to Gridview
        gdvForms.Enabled = true;
        gdvForms.DataSource = _appRoleBLL.AllForms;
        gdvForms.DataBind();
    }

    private bool Validate()
    {
        HideErrors();

        SortedList sl = _appRoleBLL.Validate();

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
        if (key == "Name")
        {
            lblName.CssClass = "error";
            txtName.CssClass = "error";
        }
        //Name
        
        //Desc
        if (key == "Desc")
        {
            lblDesc.CssClass = "error";
            txtDesc.CssClass = "error";
        }
        //Desc
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        //Name
        lblName.CssClass = "";
        txtName.CssClass = "";
        //Name

        //Desc
        lblDesc.CssClass = "";
        txtDesc.CssClass = "";
        //Desc

    }

    private void Reset()
    {
        txtName.Text = "";
        txtDesc.Text = "";

        //clear Grid Details & show all forms
        _appRoleBLL.GetAllForms();
        gdvForms.DataSource = _appRoleBLL.AllForms;
        gdvForms.DataBind();
    }

    #endregion
}
