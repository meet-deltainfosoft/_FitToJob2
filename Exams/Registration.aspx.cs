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

public partial class Exams_Registration : System.Web.UI.Page
{
    private RegistrationBLL _RegistrationBLL;

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["RegistrationId"] == null)
            {
                _RegistrationBLL = new RegistrationBLL();
            }
            else
            {
                _RegistrationBLL = new RegistrationBLL(Request.QueryString["RegistrationId"].ToString());
            }
            Session["_RegistrationBLL"] = _RegistrationBLL;
        }
        else
        {
            _RegistrationBLL = (RegistrationBLL)Session["_RegistrationBLL"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadStandard(); LoadDivision();
            if (Request.QueryString["RegistrationId"] != null)
            {
                lblTitle.Text = " - [Edit Mode]";
                LoadWebForm();
                btnDelete.Enabled = true;
            }
            ddlStandard.Focus();
        }
    }
    #endregion

    #region "Registrations Functions"

    private bool Validate()
    {
        HideErrors();

        SortedList sl = _RegistrationBLL.Validate();

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
        if (key == "StandardId")
        {
            lblStandard.CssClass = "error";
            ddlStandard.CssClass = "error";
        }
        if (key == "RegistrationName")
        {
            lblRegistrationName.CssClass = "error";
            txtRegistrationName.CssClass = "error";
        }
        if (key == "MobileNo")
        {
            lblMobileNo.CssClass = "error";
            txtMobileNo.CssClass = "error";
        }
        //Name
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        lblStandard.CssClass = "";
        ddlStandard.CssClass = "";

        lblRegistrationName.CssClass = "";
        txtRegistrationName.CssClass = "";

        lblMobileNo.CssClass = "";
        txtMobileNo.CssClass = "";
    }

    private void Reset()
    {
        LoadStandard();
        txtRegistrationName.Text = "";
        txtMobileNo.Text = "";
        txtExtraMobNo.Text = "";
        chkIsDeactive.Checked = false;
        txtExamNo.Text = "";
        txtSchoolName.Text = "";
        txtCity.Text = "";
        txtEmailId.Text = "";
    }
    private void LoadWebForm()
    {
        if (_RegistrationBLL.StandardId != null)
            ddlStandard.SelectedValue = _RegistrationBLL.StandardId;

        if (_RegistrationBLL.DivisionTextListId != null)
            ddlDivision.SelectedValue = _RegistrationBLL.DivisionTextListId;

        if (_RegistrationBLL.RegistrationName != null)
            txtRegistrationName.Text = _RegistrationBLL.RegistrationName;

        if (_RegistrationBLL.MobileNo != null)
            txtMobileNo.Text = _RegistrationBLL.MobileNo;

        if (_RegistrationBLL.ExtraMobileNo != null)
            txtExtraMobNo.Text = _RegistrationBLL.ExtraMobileNo;

        if (_RegistrationBLL.IsDeActive != null)
            chkIsDeactive.Checked = Convert.ToBoolean(_RegistrationBLL.IsDeActive);

        if (_RegistrationBLL.ExamNo != null)
            txtExamNo.Text = _RegistrationBLL.ExamNo.ToString();

        if (_RegistrationBLL.SchoolName != null)
            txtSchoolName.Text = _RegistrationBLL.SchoolName;

        if (_RegistrationBLL.City != null)
            txtCity.Text = _RegistrationBLL.City;

        if (_RegistrationBLL.EmailId != null)
            txtEmailId.Text = _RegistrationBLL.EmailId;


    }

    #endregion

    #region "Registrations Events"
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
                _RegistrationBLL.StandardId = ddlStandard.SelectedValue;
            else
                _RegistrationBLL.StandardId = null;

            if (txtRegistrationName.Text.Trim().Length > 0)
                _RegistrationBLL.RegistrationName = txtRegistrationName.Text.Trim();
            else
                _RegistrationBLL.RegistrationName = null;

            if (txtMobileNo.Text.Trim().Length > 0)
                _RegistrationBLL.MobileNo = txtMobileNo.Text.Trim();
            else
                _RegistrationBLL.MobileNo = null;

            if (txtExtraMobNo.Text.Trim().Length > 0)
                _RegistrationBLL.ExtraMobileNo = txtExtraMobNo.Text.Trim();
            else
                _RegistrationBLL.ExtraMobileNo = null;

            _RegistrationBLL.IsDeActive = chkIsDeactive.Checked;

            if (txtExamNo.Text.Trim().Length > 0)
                _RegistrationBLL.ExamNo = (txtExamNo.Text.Trim().ToString());
            else
                _RegistrationBLL.ExamNo = null;

            if (txtSchoolName.Text.Trim().Length > 0)
                _RegistrationBLL.SchoolName = txtSchoolName.Text.Trim();
            else
                _RegistrationBLL.SchoolName = null;

            if (txtCity.Text.Trim().Length > 0)
                _RegistrationBLL.City = txtCity.Text.Trim();
            else
                _RegistrationBLL.City = null;

            if (txtEmailId.Text.Trim().Length > 0)
                _RegistrationBLL.EmailId = txtEmailId.Text.Trim();
            else
                _RegistrationBLL.EmailId = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _RegistrationBLL.Save();

                if (Request.QueryString["RegistrationId"] == null)
                {
                    Reset();
                    Session["_RegistrationBLL"] = null;
                    Session["_RegistrationBLL"] = new RegistrationBLL();
                    _RegistrationBLL = (RegistrationBLL)Session["_RegistrationBLL"];
                    ShowErrors("Success", "Student Registered Successfully.");
                }
                else
                {
                    Session["_RegistrationBLL"] = null;
                    Response.Redirect("Registrations.aspx");
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
        Session["_RegistrationBLL"] = null;

        if (Request.QueryString["RegistrationId"] == null)
        {
            Response.Redirect("~/General/Default.aspx");
        }
        else
        {
            Response.Redirect("Registrations.aspx");
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _RegistrationBLL.Delete(Request.QueryString["RegistrationId"]);
            Session["_RegistrationBLL"] = null;
            Response.Redirect("Registrations.aspx");
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }
    #endregion
    private void LoadStandard()
    {
        try
        {
            ListItem li = new ListItem();

            ddlStandard.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlStandard.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _RegistrationBLL.LoadStandard().Rows)
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
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }

    private void LoadDivision()
    {
        try
        {
            ListItem li = new ListItem();

            ddlDivision.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlDivision.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _RegistrationBLL.LoadDivision().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlDivision.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
}
