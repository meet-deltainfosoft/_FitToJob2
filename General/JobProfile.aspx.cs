using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class General_JobProfile : System.Web.UI.Page
{
    #region Declarations
    private JobProfileBLL _JobProfileBLL;
    #endregion


    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["JobOfferingId"] == null)
                {
                    _JobProfileBLL = new JobProfileBLL();
                }
                else
                {
                    _JobProfileBLL = new JobProfileBLL(Request.QueryString["JobOfferingId"].ToString());
                }

                Session["_JobProfileBLL"] = _JobProfileBLL;
            }
            else
            {
                _JobProfileBLL = (JobProfileBLL)Session["_JobProfileBLL"];
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
                //LoadDesignations();
                LoadDivision();
                LoadDepartment();
                LoadStafcategory();

                if (Request.QueryString["JobOfferingId"] != null)
                {
                    lblTitle.Text = " - Edit Mode";
                    btnDelete.Enabled = true;
                    
                    
                    LoadWebForm();
                }
                else //New Mode
                {
                    btnDelete.Visible = false;
                    ddlDepartmentId.Focus();
                   
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
                ShowErrors("err", ex.Message);
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _JobProfileBLL.Delete(Request.QueryString["JobOfferingId"]);
            Session["_JobProfileBLL"] = null;
            Response.Redirect("JobProfiles.aspx");
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {

            string StaffCategoryId = null;

            if (chkStaffCategory.Items.Count > 0)
            {
                for (int i = 0; i < chkStaffCategory.Items.Count; i++)
                {
                    if (chkStaffCategory.Items[i].Selected)
                    {

                        if (StaffCategoryId != null && StaffCategoryId != "")
                            StaffCategoryId = StaffCategoryId + "," + chkStaffCategory.Items[i].Value;
                        else
                            StaffCategoryId = StaffCategoryId + chkStaffCategory.Items[i].Value;
                    }
                }
                if (StaffCategoryId != null)
                {
                    _JobProfileBLL.StaffCategoryTextListId = StaffCategoryId;
                }
                else if (ddlStaffCategoryId.SelectedIndex > 0)
                {
                    _JobProfileBLL.StaffCategoryTextListId = ddlStaffCategoryId.SelectedValue;
                }
                else
                {
                    _JobProfileBLL.StaffCategoryTextListId = null;
                }
            }

            if (ddlDepartmentId.SelectedIndex > 0)
                _JobProfileBLL.DepartmentId = ddlDepartmentId.SelectedValue;
            else
                _JobProfileBLL.DepartmentId = null;

            if (ddlDivisionId.SelectedIndex > 0)
                _JobProfileBLL.DivisionId = ddlDivisionId.SelectedValue;
            else
                _JobProfileBLL.DivisionId = null;

            if (ddlDesignationId.SelectedIndex > 0)
                _JobProfileBLL.DesignationId = ddlDesignationId.SelectedValue;
            else
                _JobProfileBLL.DesignationId = null;

            //if (ddlStaffCategoryId.SelectedIndex > 0)
            //    _JobProfileBLL.StaffCategoryTextListId = ddlStaffCategoryId.SelectedValue;
            //else
            //    _JobProfileBLL.StaffCategoryTextListId = null;

            if (txtNoOfSeats.Text.Trim().Length > 0)
                _JobProfileBLL.NoOfSeats = txtNoOfSeats.Text.ToString();
            else
                _JobProfileBLL.NoOfSeats = null;

            if (txtValidfrom.Text.Trim().Length > 0)
                _JobProfileBLL.ValidFrom = Convert.ToDateTime(txtValidfrom.Text.Trim());
            else
                _JobProfileBLL.ValidFrom = null;

            if (txtValidto.Text.Trim().Length > 0)
                _JobProfileBLL.ValidTo = Convert.ToDateTime(txtValidto.Text.Trim());
            else
                _JobProfileBLL.ValidTo = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _JobProfileBLL.Save();

                if (Request.QueryString["RegistrationId"] == null)
                {
                    //ShowErrors("Success", "Record Saved Succsessfully.");
                    Session["_JobProfileBLL"] = null;
                    Session["_JobProfileBLL"] = new JobProfileBLL();
                    _JobProfileBLL = (JobProfileBLL)Session["_JobProfileBLL"];
                    Reset();
                }
                else
                {
                    Session["_JobProfileBLL"] = null;
                    Response.Redirect("Registartions.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
        ddlDepartmentId.Focus();
    }



    protected void chkallStaffCategory_CheckedChanged(object sender, EventArgs e)
    {
        if (chkallStaffCategory.Checked == true)
        {
            foreach (ListItem chkall in chkStaffCategory.Items)
            {
                chkall.Selected = true;
                string k = "";
                for (int i = 0; i < chkStaffCategory.Items.Count; i++)
                {
                    if (chkStaffCategory.Items[i].Selected)
                    {
                        if (k != null && k != "")
                            k = k + "," + chkStaffCategory.Items[i].Value;
                        else
                            k = k + chkStaffCategory.Items[i].Value;
                    }
                }


            }
        }
        else
        {
            foreach (ListItem chkall in chkStaffCategory.Items)
            {
                chkall.Selected = false;
            }
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

            if (key == "DepartmentId")
            {
                lblDepartmentId.CssClass = "";
                ddlDepartmentId.CssClass = "error form-control";
            }

            //if (key == "DivisionId")
            //{
            //    lblDivisionId.CssClass = "";
            //    ddlDivisionId.CssClass = "error form-control";
            //}

            if (key == "StaffCategoryId")
            {
                lblStaffCategoryId.CssClass = "";
                ddlStaffCategoryId.CssClass = "error form-control";
            }
          
            if (key == "DesignationId")
            {
                lblDesignationId.CssClass = "";
                ddlDesignationId.CssClass = "error form-control";
            }

            if (key == "NoOfSeats")
            {
                lblNoOfSeats.CssClass = "";
                txtNoOfSeats.CssClass = "error form-control";
            }

            if (key == "Validfrom")
            {
                lblValidfrom.CssClass = "";
                txtValidfrom.CssClass = "error form-control";
            }

            if (key == "Validto")
            {
                lblValidto.CssClass = "";
                txtValidto.CssClass = "error form-control";
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

            lblValidfrom.CssClass = "";
            txtValidfrom.CssClass = "";

            lblValidto.CssClass = "";
            txtValidto.CssClass = "";

            lblNoOfSeats.CssClass = "";
            txtNoOfSeats.CssClass = "";

            lblDesignationId.CssClass = "";
            ddlDesignationId.CssClass = "form-control";

            lblDepartmentId.CssClass = "";
            ddlDepartmentId.CssClass = "form-control";

            //lblDivisionId.CssClass = "";
            //ddlDivisionId.CssClass = "form-control";

            lblStaffCategoryId.CssClass = "";
            ddlStaffCategoryId.CssClass = "form-control";
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
            txtNoOfSeats.Text = "";
            ddlDepartmentId.SelectedIndex = 0;
            ddlDesignationId.SelectedIndex = 0;
            ddlDivisionId.SelectedIndex = 0;
            chkStaffCategory.SelectedValue = null;
            txtValidfrom.Text = "";
            txtValidto.Text = "";
            chkallStaffCategory.Checked = false;
            //ddlStaffCategoryId.SelectedIndex = 0;

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

            SortedList sl = _JobProfileBLL.Validate();

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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Session["_JobProfileBLL"] = null;

            if (Request.QueryString["JobOfferingId"] == null)
                Response.Redirect("Default.aspx");
            else
                Response.Redirect("Registation.aspx");
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
            

            if (_JobProfileBLL.DivisionId != null)
                ddlDivisionId.SelectedValue = _JobProfileBLL.DivisionId.ToString();

            if (_JobProfileBLL.DepartmentId != null)
            {
                ddlDepartmentId.SelectedValue = _JobProfileBLL.DepartmentId.ToString();
                ddlStdId_SelectedIndexChanged(null,null);
            }

            if (_JobProfileBLL.DesignationId != null)
                ddlDesignationId.SelectedValue = _JobProfileBLL.DesignationId.ToString();

            //if (_JobProfileBLL.StaffCategoryTextListId != null)
            //    ddlStaffCategoryId.SelectedValue = _JobProfileBLL.StaffCategoryTextListId.ToString();

            if (_JobProfileBLL.StaffCategoryTextListId != null)
                chkStaffCategory.SelectedValue = _JobProfileBLL.StaffCategoryTextListId;

            if (_JobProfileBLL.ValidFrom != null)
                txtValidfrom.Text = Convert.ToDateTime(_JobProfileBLL.ValidFrom).ToString("dd-MMM-yyyy");

            if (_JobProfileBLL.ValidTo != null)
                txtValidto.Text = Convert.ToDateTime(_JobProfileBLL.ValidTo).ToString("dd-MMM-yyyy");

            if (_JobProfileBLL.NoOfSeats != null)
                txtNoOfSeats.Text = _JobProfileBLL.NoOfSeats;
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    #region Load Dropdown

    private void LoadDesignations()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDesignationId.Items.Clear();
            li.Text = "<Select>";
            li.Value = "0";
            ddlDesignationId.Items.Add(li);
            li = null;

            DataTable dt = new DataTable();
            dt = _JobProfileBLL.GetDesignations(((ddlDepartmentId.SelectedIndex > 0) ? ddlDepartmentId.SelectedValue : null));

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();
                li.Text = dtr["Name"].ToString();
                li.Value = dtr["SubId"].ToString();
                ddlDesignationId.Items.Add(li);
                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    private void LoadDivision()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDivisionId.Items.Clear();
            li.Text = "<Select>";
            li.Value = "0";
            ddlDivisionId.Items.Add(li);
            li = null;

            foreach (DataRow dtr in _JobProfileBLL.GetDivision().Rows)
            {
                li = new ListItem();
                li.Text = dtr["Text"].ToString();
                li.Value = dtr["TextListId"].ToString();
                ddlDivisionId.Items.Add(li);
                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    private void LoadDepartment()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDepartmentId.Items.Clear();
            li.Text = "<Select>";
            li.Value = "0";
            ddlDepartmentId.Items.Add(li);
            li = null;

            foreach (DataRow dtr in _JobProfileBLL.GetDepartment().Rows)
            {
                li = new ListItem();
                li.Text = dtr["Text"].ToString();
                li.Value = dtr["TextListId"].ToString();
                ddlDepartmentId.Items.Add(li);
                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    private void LoadStafcategory()
    {
        try
        {
            ListItem li = new ListItem();
            // ddlStaffCategoryId.Items.Clear();
            chkStaffCategory.Items.Clear();
            //  li.Text = "<select>";
            //  li.Value = "0";
            ////  ddlStaffCategoryId.Items.Add(li);
            //  li = null;


            //DataTable dt = new DataTable();
            //dt = _JobProfileBLL.GetStafcategory();

            //foreach (DataRow dtr in _JobProfileBLL.GetStafcategory().Rows)
            //{
            //    li = new ListItem();
            //    li.Text = dtr["Text"].ToString();
            //    li.Value = dtr["TextListId"].ToString();
            //  //  ddlStaffCategoryId.Items.Add(li);
            //    chkStaffCategory.Items.Clear();

            //    li = null;
            //}
            DataTable dt = new DataTable();
            dt = _JobProfileBLL.GetStafcategory();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                chkStaffCategory.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    #endregion

    protected void ddlStdId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartmentId.SelectedIndex > 0)
            LoadDesignations();
        else
            ShowErrors("err", "Please select standard");
    }
}