using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data;

public partial class General_Registration1 : System.Web.UI.Page
{
    #region Declarations
    private Registration1BLL _registrationBLL;

    #endregion
    string PhotoPath = "";
    string SelfIntroVideoPath = "";
    string Resume = "";
    HiddenField HDPhotoPath = new HiddenField();
    HiddenField HDSelfIntroVideoPath = new HiddenField();
    HiddenField HDResume = new HiddenField();



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

            if (Session["MobileNo"].ToString() == null || Session["Language"].ToString() == null)
            {
                Response.Redirect("~/Guest/SignIn.aspx");
            }
            else
            {
                if (Session["Language"].ToString() == "Gujarati")
                {
                    lblAadharNo.Text = "આધાર કાર્ડ નંબર*";
                    lblFirstname.Text = "પ્રથમ નામ*";
                    lblMiddlename.Text = "પિતાનું નામ*";
                    lblLastName.Text = "છેલ્લું નામ*";
                    lblMobileNo.Text = "મોબાઇલ નંબર*";
                    lblCity.Text = "શહેર/ગામ*";
                    lblTaluka.Text = "તાલુકો*";
                    lblDistrict.Text = "જિલ્લો*";
                    lblState.Text = "રાજ્ય*";
                    lblAddress.Text = "સરનામું";
                    lblSelfintravideo.Text = "સ્વ પરિચય વિડિઓ";
                    lblResume.Text = "બાયોડેટા અપલોડ કરો";
                    lblPhoto.Text = "પાસપોર્ટ સાઇઝ ફોટો";
                    lblTitle.Text = " નોંધણી એન્ટ્રી -મૂલ્ય - [નવો મોડ]";
                    btnOk.Text = "સાચવો";
                    btnCancel.Text = "રદ કરો";

                }
                else if (Session["Language"].ToString() == "Hindi")
                {
                    lblAadharNo.Text = "आधार कार्ड नंबर*";
                    lblFirstname.Text = "पहला नाम*";
                    lblMiddlename.Text = "पिता का नाम*";
                    lblLastName.Text = "उपनाम*";
                    lblMobileNo.Text = "मोबाइल नंबर*";
                    lblCity.Text = "शहर/गांव*";
                    lblTaluka.Text = "तालुका*";
                    lblDistrict.Text = "जिला*";
                    lblState.Text = "राज्य*";
                    lblAddress.Text = "पता";
                    lblSelfintravideo.Text = "आत्म परिचय वीडियो";
                    lblResume.Text = "अपलोड बायोडाटा";
                    lblPhoto.Text = "पासपोर्ट साइज फोटो";
                    lblTitle.Text = "पंजीकरण प्रविष्टि -मूल्य - [नया मोड]";
                    btnOk.Text = "सहेजें";
                    btnCancel.Text = "रद्द करें";
                }
            }

        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["MobileNo"].ToString() != "" || Session["Language"].ToString() != "")
                {
                    txtMobileNo.Text = Session["MobileNo"].ToString();
                    txtMobileNo.ReadOnly = true;
                    GetUserRegistrationData();

                    if (Session["IsApproved"].ToString() != "" || Session["IsApproved"] != null)
                    {
                        if (Convert.ToBoolean(Session["IsApproved"]) == true)
                        {
                            txtAadharNo.Enabled = false;
                            txtFirstname.Enabled = false;
                            txtMiddlename.Enabled = false;
                            txtLastname.Enabled = false;
                            txtMobileNo.Enabled = false;
                            txtCity.Enabled = false;
                            txtTaluka.Enabled = false;
                            txtDistrict.Enabled = false;
                            txtState.Enabled = false;
                            txtAddress.Enabled = false;
                            fuPhoto.Enabled = false;
                            fuSelfintravideo.Enabled = false;
                            fuResumeUpload.Enabled = false;
                            ShowErrors("error", "Your Profile is reviwed for change please contact administrator.");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Guest/SignIn.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Guest/SignIn.aspx");
                }
            }
        }
        catch (Exception)
        {

        }
    }

    //    private bool IsParentPathValid()
    //{
    //    Uri Rreferrer = Request.UrlReferrer;
    //    string parentPath = Rreferrer.AbsolutePath;

    //    // Replace '/Default.aspx' with the actual login page path
    //    return !parentPath.EndsWith("/Registration1.aspx", StringComparison.OrdinalIgnoreCase);

    //}
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
    protected void btnOLd_Click(object sender, EventArgs e)
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


            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select Top 1 RegistrationId From registrations Where MobileNo = '" + txtMobileNo.Text.Trim() + "'";
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            Registration1DTO _registrationDTO = new Registration1DTO();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                _registrationDTO.IsNew = false;
            }
            else
            {
                _registrationDTO.IsNew = true;
            }




            if (fuPhoto.HasFile)
            {
                string uniqueFileName = Guid.NewGuid().ToString();
                string fileExtension = Path.GetExtension(fuPhoto.FileName);
                string newFileName = uniqueFileName + fileExtension;
                string uploadFolder = Server.MapPath("~/images/");
                string fullPath = Path.Combine(uploadFolder, newFileName);
                _registrationBLL.PhotoPath = "/images/" + newFileName;
                fuPhoto.SaveAs(fullPath);



                //string[] getExtenstion = fuPhoto.FileName.Split('.');
                //string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                //string FileNameForInsert = fuPhoto.FileName.Replace("." + oExtension, "");
                //var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                //var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _registrationBLL.MobileNo.ToString() + "-STU." + oExtension;

                //_registrationBLL.PhotoPath = filePath;

                //_registrationBLL.PhotoName = _registrationBLL.MobileNo.ToString() + "-STU." + oExtension;
            }

            if (fuSelfintravideo.PostedFile != null && fuSelfintravideo.PostedFile.FileName != "")
            {
                string uniqueFileName = Guid.NewGuid().ToString();
                string fileExtension = Path.GetExtension(fuSelfintravideo.FileName);
                string newFileName = uniqueFileName + fileExtension;
                string uploadFolder = Server.MapPath("~/images/");
                string fullPath = Path.Combine(uploadFolder, newFileName);
                _registrationBLL.SelfIntroVideoPath = "/images/" + newFileName;
                fuSelfintravideo.SaveAs(fullPath);


                //string[] getExtenstion = fuSelfintravideo.FileName.Split('.');
                //string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                //string FileNameForInsert = fuSelfintravideo.FileName.Replace("." + oExtension, "");
                //var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                //var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _registrationBLL.MobileNo.ToString() + "-F." + oExtension;

                //_registrationBLL.SelfIntroVideoPath = filePath;
                // _registrationBLL.FatherPhotoName = _registrationBLL.MobileNo.ToString() + "-F." + oExtension;
            }

            if (fuResumeUpload.PostedFile != null && fuResumeUpload.PostedFile.FileName != "")
            {
                string uniqueFileName = Guid.NewGuid().ToString();
                string fileExtension = Path.GetExtension(fuResumeUpload.FileName);
                string newFileName = uniqueFileName + fileExtension;
                string uploadFolder = Server.MapPath("~/images/");
                string fullPath = Path.Combine(uploadFolder, newFileName);
                _registrationBLL.Resume = "/images/" + newFileName;
                fuResumeUpload.SaveAs(fullPath);

                //string[] getExtenstion = fuResumeUpload.FileName.Split('.');
                //string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                //string FileNameForInsert = fuResumeUpload.FileName.Replace("." + oExtension, "");
                //var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                //var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _registrationBLL.MobileNo.ToString() + "-STU." + oExtension;

                //_registrationBLL.Resume = filePath;

                //_registrationBLL.PhotoName = _registrationBLL.MobileNo.ToString() + "-STU." + oExtension;
            }

            bool isValid = Validate();

            if (isValid == true)
            {
                _registrationBLL.Save();
                Response.Redirect("~/Guest/JobProfile.aspx");

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
                    Response.Redirect("~/Guest/JobProfile.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
        txtAadharNo.Focus();
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            if (fuPhoto.HasFile)
            {
                string uniqueFileName = Guid.NewGuid().ToString();
                string fileExtension = Path.GetExtension(fuPhoto.FileName);
                string newFileName = uniqueFileName + fileExtension;
                string uploadFolder = Server.MapPath("~/images/");
                string fullPath = Path.Combine(uploadFolder, newFileName);
                PhotoPath = "~/images/" + newFileName;
                fuPhoto.SaveAs(fullPath);
            }

            if (fuSelfintravideo.PostedFile != null && fuSelfintravideo.PostedFile.FileName != "")
            {
                string uniqueFileName = Guid.NewGuid().ToString();
                string fileExtension = Path.GetExtension(fuSelfintravideo.FileName);
                string newFileName = uniqueFileName + fileExtension;
                string uploadFolder = Server.MapPath("~/images/");
                string fullPath = Path.Combine(uploadFolder, newFileName);
                SelfIntroVideoPath = "~/images/" + newFileName;
                fuSelfintravideo.SaveAs(fullPath);
            }

            if (fuResumeUpload.PostedFile != null && fuResumeUpload.PostedFile.FileName != "")
            {
                string uniqueFileName = Guid.NewGuid().ToString();
                string fileExtension = Path.GetExtension(fuResumeUpload.FileName);
                string newFileName = uniqueFileName + fileExtension;
                string uploadFolder = Server.MapPath("~/images/");
                string fullPath = Path.Combine(uploadFolder, newFileName);
                Resume = "~/images/" + newFileName;
                fuResumeUpload.SaveAs(fullPath);
            }

            if (PhotoPath == "")
            {
                PhotoPath = lblPhotoPath.Text;
            }

            if (SelfIntroVideoPath == "")
            {
                SelfIntroVideoPath = lblSelfIntroPath.Text;
            }

            if (Resume == "")
            {
                Resume = lblResumePath.Text;
            }

            if (PhotoPath == "" || Resume == "")
            {
                if (Session["Language"].ToString() == "Gujarati")
                {
                    ShowErrors("err", "રિઝ્યુમ અને પાસપોર્ટ સાઇઝના ફોટા ફરજીયાત છે!!");
                }
                else if (Session["Language"].ToString() == "Gujarati")
                {
                    ShowErrors("err", "बायोडाटा और पासपोर्ट साइज फोटो अनिवार्य हैं!!");
                }
                else
                {
                    ShowErrors("err", "Resume and Passport Size Photos are Compulsory!!");
                }
            }
            else
            {

                SqlCommand sqlCmd = new SqlCommand();
                GeneralDAL objDal = new GeneralDAL();
                objDal.OpenSQLConnection();
                sqlCmd.Connection = objDal.ActiveSQLConnection();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "FitToJob_Master_Registration";
                sqlCmd.Parameters.AddWithValue("@AadharCardNo", txtAadharNo.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@FirstName", txtFirstname.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@MiddleName", txtMiddlename.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@LastName", txtLastname.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@City", txtCity.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Taluka", txtTaluka.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@District", txtDistrict.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@PhotoPath", PhotoPath);
                sqlCmd.Parameters.AddWithValue("@VideoPath", SelfIntroVideoPath);
                sqlCmd.Parameters.AddWithValue("@ResumePath", Resume);
                sqlCmd.Parameters.AddWithValue("@Action", "InsertRegistration");
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["IsSuccessful"].ToString() == "0")
                    {
                        ShowErrors("err", dataSet.Tables[0].Rows[0]["Message"].ToString());
                    }
                    else
                    {
                        Response.Redirect("~/Guest/JobProfile.aspx");
                    }
                }
                objDal.CloseSQLConnection();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
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

    private void GetUserRegistrationData()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            try
            {

                sqlCmd.CommandText = "FitToJob_Master_Registration";
                sqlCmd.Parameters.AddWithValue("@Action", "GetUserRegistrationData");
                sqlCmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["IsSuccessful"].ToString() == "1")
                    {
                        txtAadharNo.Text = dataSet.Tables[1].Rows[0]["AadharCardNo"].ToString();
                        txtFirstname.Text = dataSet.Tables[1].Rows[0]["FirstName"].ToString();
                        txtMiddlename.Text = dataSet.Tables[1].Rows[0]["MiddleName"].ToString();
                        txtLastname.Text = dataSet.Tables[1].Rows[0]["LastName"].ToString();
                        txtMobileNo.Text = dataSet.Tables[1].Rows[0]["MobileNo"].ToString();
                        txtCity.Text = dataSet.Tables[1].Rows[0]["City"].ToString();
                        txtTaluka.Text = dataSet.Tables[1].Rows[0]["Taluka"].ToString();
                        txtDistrict.Text = dataSet.Tables[1].Rows[0]["District"].ToString();
                        txtState.Text = dataSet.Tables[1].Rows[0]["State"].ToString();
                        txtAddress.Text = dataSet.Tables[1].Rows[0]["Address"].ToString();
                        //_registrationBLL = new Registration1BLL(dataSet.Tables[1].Rows[0]["RegistrationId"].ToString());
                        imgPhoto.ImageUrl = dataSet.Tables[1].Rows[0]["PhotoPath"].ToString();
                        lblPhotoPath.Text = dataSet.Tables[1].Rows[0]["PhotoPath"].ToString();
                        lblSelfIntroPath.Text = dataSet.Tables[1].Rows[0]["VideoPath"].ToString();
                        lblResumePath.Text = dataSet.Tables[1].Rows[0]["ResumePath"].ToString();

                        Registration1DTO _registrationDTO = new Registration1DTO();
                        _registrationDTO.IsNew = false;
                    }
                    else
                    {
                        Registration1DTO _registrationDTO = new Registration1DTO();
                        _registrationDTO.IsNew = true;
                    }

                }
                objDal.CloseSQLConnection();
            }
            catch (Exception)
            {

            }

            objDal.CloseSQLConnection();

        }
        catch (Exception ex)
        {
        }
    }

    //private void _registrationBLL(string p)
    //{
    //    throw new NotImplementedException();
    //}
}