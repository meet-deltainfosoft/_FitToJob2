using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.IO;

public partial class General_Registration1 : System.Web.UI.Page
{
    #region Declarations
    private Registration1BLL _registrationBLL;
    #endregion


    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["RegistrationId"] == null)
                {
                    _registrationBLL = new Registration1BLL();
                }
                else
                {
                    _registrationBLL = new Registration1BLL(Request.QueryString["RegistrationId"].ToString());
                }

                Session["_registrationBLL"] = _registrationBLL;
            }
            else
            {
                _registrationBLL = (Registration1BLL)Session["_registrationBLL"];
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
                if (Request.QueryString["RegistrationId"] != null)
                {
                    lblTitle.Text = " - Edit Mode";
                    btnDelete.Enabled = true;
                    LoadWebForm();
                }
                else //New Mode
                {
                    btnDelete.Visible = false;
                    txtAadharNo.Focus();
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
            _registrationBLL.Delete(Request.QueryString["RegistrationId"]);
            Session["_registrationBLL"] = null;
            Response.Redirect("Registartions1.aspx");
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

            if (txtAadharNo.Text.Trim().Length > 0)
                _registrationBLL.AadharCardNo = txtAadharNo.Text.Trim();
            else
                _registrationBLL.AadharCardNo = null;

            if (txtFirstname.Text.Trim().Length > 0)
                _registrationBLL.FirstName = txtFirstname.Text.Trim();
            else
                _registrationBLL.FirstName = null;

            if (txtMiddlename.Text.Trim().Length > 0)
                _registrationBLL.MiddleName = txtMiddlename.Text.Trim();
            else
                _registrationBLL.MiddleName = null;

            if (txtLastname.Text.Trim().Length > 0)
                _registrationBLL.LastName = txtLastname.Text.Trim();
            else
                _registrationBLL.LastName = null;

            if (txtMobileNo.Text.Trim().Length > 0)
                _registrationBLL.MobileNo = txtMobileNo.Text.Trim();
            else
                _registrationBLL.MobileNo = null;

            if (txtCity.Text.Trim().Length > 0)
                _registrationBLL.City = txtCity.Text.Trim();
            else
                _registrationBLL.City = null;

            if (txtTaluka.Text.Trim().Length > 0)
                _registrationBLL.Taluka = txtTaluka.Text.Trim();
            else
                _registrationBLL.Taluka = null;

            if (txtDistrict.Text.Trim().Length > 0)
                _registrationBLL.District = txtDistrict.Text.Trim();
            else
                _registrationBLL.District = null;

            if (txtState.Text.Trim().Length > 0)
                _registrationBLL.State = txtState.Text.Trim();
            else
                _registrationBLL.State = null;

            if (txtAddress.Text.Trim().Length > 0)
                _registrationBLL.Address = txtAddress.Text.Trim();
            else
                _registrationBLL.Address = null;

            if (fuPhoto.PostedFile != null && fuPhoto.PostedFile.FileName != "")
            {

                string[] getExtenstion = fuPhoto.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = fuPhoto.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _registrationBLL.MobileNo.ToString() + "-STU." + oExtension;

                _registrationBLL.PhotoPath = filePath;

                //_registrationBLL.PhotoName = _registrationBLL.MobileNo.ToString() + "-STU." + oExtension;
            }

            if (fuSelfintravideo.PostedFile != null && fuSelfintravideo.PostedFile.FileName != "")
            {
                string[] getExtenstion = fuSelfintravideo.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = fuSelfintravideo.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _registrationBLL.MobileNo.ToString() + "-F." + oExtension;

                _registrationBLL.SelfIntroVideoPath = filePath;
                // _registrationBLL.FatherPhotoName = _registrationBLL.MobileNo.ToString() + "-F." + oExtension;
            }

            if (fuResumeUpload.PostedFile != null && fuResumeUpload.PostedFile.FileName != "")
            {
               
                    string[] getExtenstion = fuResumeUpload.FileName.Split('.');
                    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                    string FileNameForInsert = fuResumeUpload.FileName.Replace("." + oExtension, "");
                    var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                    var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _registrationBLL.MobileNo.ToString() + "-STU." + oExtension;

                    _registrationBLL.Resume = filePath;
                
                //_registrationBLL.PhotoName = _registrationBLL.MobileNo.ToString() + "-STU." + oExtension;
            }

            bool isValid = Validate();

            if (isValid == true)
            {
                _registrationBLL.Save();

                if (fuPhoto.PostedFile != null && fuPhoto.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = fuPhoto.PostedFile;
                    uploadedImage.SaveAs(_registrationBLL.PhotoPath);
                }

                if (fuSelfintravideo.PostedFile != null && fuSelfintravideo.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = fuSelfintravideo.PostedFile;
                    uploadedImage.SaveAs(_registrationBLL.SelfIntroVideoPath);
                }

                if (Request.QueryString["RegistrationId"] == null)
                {
                    ShowErrors("Success", "Record Saved Succsessfully.");
                    Session["_registrationBLL"] = null;
                    Session["_registrationBLL"] = new Registration1BLL();
                    _registrationBLL = (Registration1BLL)Session["_registrationBLL"];
                    Reset();
                }
                else
                {
                    Session["_registrationBLL"] = null;
                    Response.Redirect("Registration1.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
        txtAadharNo.Focus();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Session["_registrationBLL"] = null;

            if (Request.QueryString["RegistrationId"] == null)
                Response.Redirect("Default.aspx");
            else
                Response.Redirect("Registation.aspx");
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
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

            if (key == "AadharNo")
            {
                lblAadharNo.CssClass = "";
                txtAadharNo.CssClass = "error form-control";
            }

            if (key == "Firstname")
            {
                lblFirstname.CssClass = "";
                txtFirstname.CssClass = "error form-control";
            }

            if (key == "Middlename")
            {
                lblMiddlename.CssClass = "";
                txtMiddlename.CssClass = "error form-control";
            }

            if (key == "LastName")
            {
                lblLastName.CssClass = "";
                txtLastname.CssClass = "error form-control";
            }

            if (key == "MobileNo")
            {
                lblMobileNo.CssClass = "";
                txtMobileNo.CssClass = "error form-control";
            }

            if (key == "City")
            {
                lblCity.CssClass = "";
                txtCity.CssClass = "error form-control";
            }

            if (key == "Taluka")
            {
                lblTaluka.CssClass = "";
                txtTaluka.CssClass = "error form-control";
            }

            if (key == "District")
            {
                lblDistrict.CssClass = "";
                txtDistrict.CssClass = "error form-control";
            }

            if (key == "State")
            {
                lblState.CssClass = "";
                txtState.CssClass = "error form-control";
            }
            if (key == "Address")
            {
                lblAddress.CssClass = "";
                txtAddress.CssClass = "error form-control";
            }
            if (key == "Resume")
            {
                lblResume.CssClass = "";
                fuResumeUpload.CssClass = "error";
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

            lblAadharNo.CssClass = "";
            txtAadharNo.CssClass = "form-control";

            lblFirstname.CssClass = "";
            txtFirstname.CssClass = "form-control";

            lblMiddlename.CssClass = "";
            txtMiddlename.CssClass = "form-control";

            lblLastName.CssClass = "";
            txtLastname.CssClass = "form-control";

            lblMobileNo.CssClass = "";
            txtMobileNo.CssClass = "form-control";

            lblCity.CssClass = "";
            txtCity.CssClass = "form-control";

            lblTaluka.CssClass = "";
            txtTaluka.CssClass = "form-control";

            lblDistrict.CssClass = "";
            txtDistrict.CssClass = "form-control";

            lblState.CssClass = "";
            txtState.CssClass = "form-control";

            lblAddress.CssClass = "";
            txtAddress.CssClass = "form-control";

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
            txtAadharNo.Text = "";
            txtFirstname.Text = "";
            txtMiddlename.Text = "";
            txtLastname.Text = "";
            txtMobileNo.Text = "";
            txtCity.Text = "";
            txtTaluka.Text = "";
            txtDistrict.Text = "";
            txtState.Text = "";
            txtAddress.Text = "";

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
            if (_registrationBLL.AadharCardNo != null)
                txtAadharNo.Text = _registrationBLL.AadharCardNo;

            if (_registrationBLL.FirstName != null)
                txtFirstname.Text = _registrationBLL.FirstName;

            if (_registrationBLL.MiddleName != null)
                txtMiddlename.Text = _registrationBLL.MiddleName;

            if (_registrationBLL.LastName != null)
                txtLastname.Text = _registrationBLL.LastName;

            if (_registrationBLL.MobileNo != null)
                txtMobileNo.Text = _registrationBLL.MobileNo;

            if (_registrationBLL.City != null)
                txtCity.Text = _registrationBLL.City;

            if (_registrationBLL.Taluka != null)
                txtTaluka.Text = _registrationBLL.Taluka;

            if (_registrationBLL.District != null)
                txtDistrict.Text = _registrationBLL.District;

            if (_registrationBLL.State != null)
                txtState.Text = _registrationBLL.State;

            if (_registrationBLL.Address != null)
                txtAddress.Text = _registrationBLL.Address;

            if (_registrationBLL.PhotoPath != null)
            {
                imgPhoto.ImageUrl = _registrationBLL.PhotoPath;
                imgPhoto.Visible = true;
            }

            if (_registrationBLL.SelfIntroVideoPath != null)
            {
                imgSelfintravideo.ImageUrl = _registrationBLL.SelfIntroVideoPath;
                imgSelfintravideo.Visible = true;
            }
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
            SortedList sl = _registrationBLL.Validate();

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
}