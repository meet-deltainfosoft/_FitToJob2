using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class General_Designation : System.Web.UI.Page
{
    #region Declarations
    private DesignationBLL _designationBLL;
    #endregion

    #region Page Events
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["DesignationId"] == null)
                {
                    _designationBLL = new DesignationBLL();
                }
                else
                {
                    _designationBLL = new DesignationBLL(Request.QueryString["DesignationId"].ToString());
                }

                Session["_designationBLL"] = _designationBLL;
            }
            else
            {
                _designationBLL = (DesignationBLL)Session["_designationBLL"];
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            try
            {
                LoadDepts();
                LoadReportingDesign();
                if (Request.QueryString["DesignationId"] != null)
                {
                    lblTitle.Text = " Edit Mode";
                    btnDelete.Enabled = true;
                    LoadWebForm();
                }
                else //New Mode
                {
                    btnDelete.Visible = false;
                    txtName.Focus();
                }
            }
            catch (Exception ex)
            {
                ShowErrors("error", ex.Message);
            }
        }
        else
        {
            try
            {
                HideErrors();
            }
            catch (Exception ex)
            {
                ShowErrors("error", ex.Message);
            }
        }

    }
    #endregion

    #region Button Click Events
    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtName.Text.Trim().Length > 0)
                _designationBLL.Name = txtName.Text.Trim();
            else
                _designationBLL.Name = null;

            if (ddlDeptId.SelectedIndex > 0)
                _designationBLL.DeptId = ddlDeptId.SelectedValue;
            else
                _designationBLL.DeptId = null;

            if (ddlReportingDesignId.SelectedIndex > 0)
            {
                _designationBLL.ReportDesignId = ddlReportingDesignId.SelectedValue;
                _designationBLL.ReportDesignName = ddlReportingDesignId.SelectedItem.Text;
            }
            else
            {
                _designationBLL.ReportDesignId = null;
                _designationBLL.ReportDesignName = null;
            }

            bool isValid = Validate();
            if (isValid == true)
            {
                _designationBLL.Save();
                if (Request.QueryString["DesignationId"] == null)
                {
                    Reset();
                    ShowErrors("Success", "Record saved successfully.");
                    Session["_designationBLL"] = null;
                    Session["_designationBLL"] = new DesignationBLL();
                }
                else
                {
                    Session["_designationBLL"] = null;
                    Response.Redirect("Designations.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
        txtName.Focus();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _designationBLL.Delete(Request.QueryString["DesignationId"]);
            Session["_designationBLL"] = null;
            Response.Redirect("Designations.aspx");
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Session["_designationBLL"] = null;

            if (Request.QueryString["DesignationId"] == null)
                Response.Redirect("Default.aspx");
            else
                Response.Redirect("Designation.aspx");
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }
    #endregion

    #region Functions
    private bool Validate()
    {
        try
        {
            HideErrors();

            SortedList sl = _designationBLL.Validate();

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
            ShowErrors("Error", ex.Message);
            return false;
        }
    }
    private void ShowErrors(string key, string value)
    {
        try
        {
            if (key == "Success")
                pnlErr.CssClass = "errors alert alert-success";
            else
                pnlErr.CssClass = "errors alert alert-danger";

            pnlErr.Visible = true;
            blErrs.Items.Add(new ListItem(value));

            if (key == "DeptId")
            {
                //lblDept.CssClass = "error form-control";
                //ddlDeptId.CssClass = "error form-control";
            }

            if (key == "Name")
            {
                lblName.CssClass = "error form-control";
                txtName.CssClass = "error form-control";
            }

            if (key == "ReportingDesign")
            {
                //lblReportingDesign.CssClass = "error form-control";
                //ddlReportingDesignId.CssClass = "error form-control";
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }
    private void HideErrors()
    {
        try
        {
            pnlErr.Visible = false;
            blErrs.Items.Clear();

            lblName.CssClass = "";
            txtName.CssClass = "form-control";

            lblDept.CssClass = "";
            ddlDeptId.CssClass = "form-control";

            lblReportingDesign.CssClass = "";
            ddlReportingDesignId.CssClass = "form-control";
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }
    private void Reset()
    {
        try
        {
            txtName.Text = "";
            ddlDeptId.SelectedIndex = 0;
            ddlReportingDesignId.SelectedIndex = 0;
            LoadReportingDesign();
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }
    private void LoadWebForm()
    {
        try
        {
            if (_designationBLL.Name != null)
                txtName.Text = _designationBLL.Name;
            else
                txtName.Text = null;

            if (_designationBLL.DeptId != null)
                ddlDeptId.SelectedValue = _designationBLL.DeptId;

            if (_designationBLL.ReportDesignId != null)
                ddlReportingDesignId.SelectedValue = _designationBLL.ReportDesignId;
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }
    #endregion

    #region Load Dropdown
    private void LoadDepts()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDeptId.Items.Clear();
            li.Text = "<Select>";
            li.Value = "0";
            ddlDeptId.Items.Add(li);
            li = null;

            foreach (DataRow dtr in _designationBLL.GetDepts().Rows)
            {
                li = new ListItem();
                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlDeptId.Items.Add(li);
                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }
    private void LoadReportingDesign()
    {
        try
        {
            ListItem li = new ListItem();

            ddlReportingDesignId.Items.Clear();
            li.Text = "<Select>";
            li.Value = "0";
            ddlReportingDesignId.Items.Add(li);
            li = null;

            foreach (DataRow dtr in _designationBLL.GetReportingDesign().Rows)
            {
                li = new ListItem();
                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlReportingDesignId.Items.Add(li);
                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }
    #endregion

}