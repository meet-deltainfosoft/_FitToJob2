using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Admin_Form : System.Web.UI.Page
{
    //This form is used for Editing Form detail Description only. All the new entries are supposed to be entered by 
    //System Administration. So application admin need not to enter any details.
    //Change is done in this form to available new menu entry -- on 04/April/2011 by Urvi Sn.

    FormBLL _formBLL;

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["FormId"] == null)
            {
               // Response.Redirect("~/General/Default.aspx");
                _formBLL = new FormBLL();
            }
            else
            {
                _formBLL = new FormBLL(Request.QueryString["FormId"].ToString());
            }

            Session["_formBLL"] = _formBLL;
        }
        else
        {
            _formBLL = (FormBLL)Session["_formBLL"];
        }
    } 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            LoadModules();
            if (Request.QueryString["FormId"] != null) //Edit Mode
            {
                lblTitle.Text = " - [Edit Mode]";

                LoadWebForm();
            }
        }
    }    

    #endregion

    #region "Form Events"

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            //Name
            if (txtName.Text.Trim() != "")
                _formBLL.Name = txtName.Text;
            else
                _formBLL.Name = null;
            //Desc

            if (ddlModuleName.SelectedIndex > 0)
                _formBLL.ModuleTextListId = ddlModuleName.SelectedValue;
            else
                _formBLL.ModuleTextListId = null;

            //Desc
            if (txtDesc.Text.Trim() != "")
                _formBLL.Desc = txtDesc.Text;
            else
                _formBLL.Desc = null;
            //Desc

            bool isValid = Validate();

            if (isValid == true)
            {
                _formBLL.Save();

                Session["_formBLL"] = null;
                Response.Redirect("Forms.aspx");
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
        Session["_formBLL"] = null;
        Response.Redirect("Forms.aspx");
    }

    #endregion

    #region "Form Functions"

    private void LoadWebForm()
    {
       //Name
        txtName.Text= _formBLL.Name;
        
        //Module
        ddlModuleName.SelectedValue = _formBLL.ModuleTextListId;
        
//        txtModuleName.Text = _formBLL.ModuleName;
        
        //Desc
        if (_formBLL.Desc != null)
            txtDesc.Text = _formBLL.Desc;
    }

    private bool Validate()
    {
        HideErrors();

        SortedList sl = _formBLL.Validate();

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

        //Desc
            lblDesc.CssClass = "";
            txtDesc.CssClass = "";
        //Desc
    }

    private void LoadModules()
    {
        ListItem li = new ListItem();

        ddlModuleName.Items.Clear();

        li.Text = "<Select>";
        li.Value = "0";
        ddlModuleName.Items.Add(li);

        li = null;

        foreach (DataRow dtr in _formBLL.Modules().Rows)
        {
            li = new ListItem();

            li.Text = dtr[1].ToString();
            li.Value = dtr[0].ToString();
            ddlModuleName.Items.Add(li);

            li = null;
        }
    }

    #endregion
}
