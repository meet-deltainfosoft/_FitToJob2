using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;

public partial class General_InterViewForm : System.Web.UI.Page
{
    #region Declarations
    private InterViewFormBLL _InterViewFormBLL;
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["InterviewFormId"] == null)
                {
                    _InterViewFormBLL = new InterViewFormBLL();
                }
                else
                {
                    _InterViewFormBLL = new InterViewFormBLL(Request.QueryString["InterviewFormId"].ToString());
                }

                Session["_InterViewFormBLL"] = _InterViewFormBLL;
            }
            else
            {
                _InterViewFormBLL = (InterViewFormBLL)Session["_InterViewFormBLL"];
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
                if (Request.QueryString["InterviewFormId"] != null)
                {
                    lblTitle.Text = " - Edit Mode";
                    btnDelete.Enabled = true;
                    LoadWebForm();
                }
                else //New Mode
                {
                    btnDelete.Visible = false;
                    txtFullName.Focus();
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
            _InterViewFormBLL.Delete(Request.QueryString["InterviewFormId"]);
            Session["_InterViewFormBLL"] = null;
            Response.Redirect("InterViewForms.aspx");
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

            if (txtFullName.Text.Trim().Length > 0)
                _InterViewFormBLL.FullName = txtFullName.Text.Trim();
            else
                _InterViewFormBLL.AadharCardNo = null;

            if (txtEmail.Text.Trim().Length > 0)
                _InterViewFormBLL.mailto = txtEmail.Text.Trim();
            else
                _InterViewFormBLL.mailto = null;

            if (txtPresentAddress.Text.Trim().Length > 0)
                _InterViewFormBLL.PresentAddress = txtPresentAddress.Text.Trim();
            else
                _InterViewFormBLL.PresentAddress = null;

            if (txtPresentPost.Text.Trim().Length > 0)
                _InterViewFormBLL.PresentPost = txtPresentPost.Text.Trim();
            else
                _InterViewFormBLL.PresentPost = null;

            if (txtPresentVillage.Text.Trim().Length > 0)
                _InterViewFormBLL.PresentVillage = txtPresentVillage.Text.Trim();
            else
                _InterViewFormBLL.PresentVillage = null;

            if (txtPresentDistrict.Text.Trim().Length > 0)
                _InterViewFormBLL.PresentDistrict = txtPresentDistrict.Text.Trim();
            else
                _InterViewFormBLL.PresentDistrict = null;

            if (txtPresentPinCode.Text.Trim().Length > 0)
                _InterViewFormBLL.PresentPinCode = txtPresentPinCode.Text.Trim();
            else
                _InterViewFormBLL.PresentPinCode = null;

            if (txtPresentMobileNo.Text.Trim().Length > 0)
                _InterViewFormBLL.PresentMobileNo = txtPresentMobileNo.Text.Trim();
            else
                _InterViewFormBLL.PresentMobileNo = null;

            if (txtPermanentAddress.Text.Trim().Length > 0)
                _InterViewFormBLL.PermanentAddress = txtPermanentAddress.Text.Trim();
            else
                _InterViewFormBLL.PermanentAddress = null;

            if (txtPermanentPost.Text.Trim().Length > 0)
                _InterViewFormBLL.PermanentPost = txtPermanentPost.Text.Trim();
            else
                _InterViewFormBLL.PermanentPost = null;

            if (txtPermanentVillage.Text.Trim().Length > 0)
                _InterViewFormBLL.PermanentVillage = txtPermanentVillage.Text.Trim();
            else
                _InterViewFormBLL.PermanentVillage = null;



            if (txtPermanentDistrict.Text.Trim().Length > 0)
                _InterViewFormBLL.PermanentDistrict = txtPermanentDistrict.Text.Trim();
            else
                _InterViewFormBLL.PermanentDistrict = null;

            if (txtPermanentPinCode.Text.Trim().Length > 0)
                _InterViewFormBLL.PermanentPinCode = txtPermanentPinCode.Text.Trim();
            else
                _InterViewFormBLL.PermanentPinCode = null;

            if (txtPermanentMobileNo.Text.Trim().Length > 0)
                _InterViewFormBLL.PermanentMobileNo = txtPermanentMobileNo.Text.Trim();
            else
                _InterViewFormBLL.PermanentMobileNo = null;

            if (txtBloodGroup.Text.Trim().Length > 0)
                _InterViewFormBLL.BloodGroup = txtBloodGroup.Text.Trim();
            else
                _InterViewFormBLL.BloodGroup = null;

            if (txtDOB.Text.Trim().Length > 0)
                _InterViewFormBLL.DOB = Convert.ToDateTime(txtDOB.Text.Trim());
            else
                _InterViewFormBLL.DOB = null;

            if (txtAadharCardNo.Text.Trim().Length > 0)
                _InterViewFormBLL.AadharCardNo = txtAadharCardNo.Text.Trim();
            else
                _InterViewFormBLL.AadharCardNo = null;

            if (txtPanCardNo.Text.Trim().Length > 0)
                _InterViewFormBLL.PanCardNo = txtPanCardNo.Text.Trim();
            else
                _InterViewFormBLL.PanCardNo = null;

            if (txtElectionCardNo.Text.Trim().Length > 0)
                _InterViewFormBLL.ElectionCardNo = txtElectionCardNo.Text.Trim();
            else
                _InterViewFormBLL.ElectionCardNo = null;
            if (txtCategory.Text.Trim().Length > 0)
                _InterViewFormBLL.Category = txtCategory.Text.Trim();
            else
                _InterViewFormBLL.Category = null;

            if (txtFatherName.Text.Trim().Length > 0)
                _InterViewFormBLL.FatherName = txtFatherName.Text.Trim();
            else
                _InterViewFormBLL.FatherName = null;

            if (txtPermanentPost.Text.Trim().Length > 0)
                _InterViewFormBLL.PermanentPost = txtPermanentPost.Text.Trim();
            else
                _InterViewFormBLL.PermanentPost = null;

            if (txtFatherOccupation.Text.Trim().Length > 0)
                _InterViewFormBLL.FatherOccupation = txtFatherOccupation.Text.Trim();
            else
                _InterViewFormBLL.FatherOccupation = null;

            if (txtFatherEducation.Text.Trim().Length > 0)
                _InterViewFormBLL.FatherEducation = txtFatherEducation.Text.Trim();
            else
                _InterViewFormBLL.FatherEducation = null;

            if (txtFatherMobileNo.Text.Trim().Length > 0)
                _InterViewFormBLL.FatherMobileNo = txtFatherMobileNo.Text.Trim();
            else
                _InterViewFormBLL.FatherMobileNo = null;
            if (txtMotherName.Text.Trim().Length > 0)
                _InterViewFormBLL.MotherName = txtMotherName.Text.Trim();
            else
                _InterViewFormBLL.MotherName = null;

            if (txtMotherOccupation.Text.Trim().Length > 0)
                _InterViewFormBLL.MotherOccupation = txtMotherOccupation.Text.Trim();
            else
                _InterViewFormBLL.MotherOccupation = null;

            if (txtMotherEducation.Text.Trim().Length > 0)
                _InterViewFormBLL.MotherEducation = txtMotherEducation.Text.Trim();
            else
                _InterViewFormBLL.MotherEducation = null;

            if (txtMotherMobileNo.Text.Trim().Length > 0)
                _InterViewFormBLL.MotherMobileNo = txtMotherMobileNo.Text.Trim();
            else
                _InterViewFormBLL.MotherMobileNo = null;

            if (txtWifeName.Text.Trim().Length > 0)
                _InterViewFormBLL.WifeName = txtWifeName.Text.Trim();
            else
                _InterViewFormBLL.WifeName = null;

            if (txtWifeOccupation.Text.Trim().Length > 0)
                _InterViewFormBLL.WifeOccupation = txtWifeOccupation.Text.Trim();
            else
                _InterViewFormBLL.WifeOccupation = null;

            if (txtWifeEducation.Text.Trim().Length > 0)
                _InterViewFormBLL.WifeEducation = txtWifeEducation.Text.Trim();
            else
                _InterViewFormBLL.WifeEducation = null;

            if (txtWifeMobileNo.Text.Trim().Length > 0)
                _InterViewFormBLL.WifeMobileNo = txtWifeMobileNo.Text.Trim();
            else
                _InterViewFormBLL.WifeMobileNo = null;

            if (txtBrotherName.Text.Trim().Length > 0)
                _InterViewFormBLL.BrotherName = txtBrotherName.Text.Trim();
            else
                _InterViewFormBLL.BrotherName = null;

            if (txtBrotherOccupation.Text.Trim().Length > 0)
                _InterViewFormBLL.BrotherOccupation = txtBrotherOccupation.Text.Trim();
            else
                _InterViewFormBLL.BrotherOccupation = null;

            if (txtBrotherEducation.Text.Trim().Length > 0)
                _InterViewFormBLL.BrotherEducation = txtBrotherEducation.Text.Trim();
            else
                _InterViewFormBLL.BrotherEducation = null;

            if (txtBrotherMobileNo.Text.Trim().Length > 0)
                _InterViewFormBLL.BrotherMobileNo = txtBrotherMobileNo.Text.Trim();
            else
                _InterViewFormBLL.BrotherMobileNo = null;
            if (txtNomineeName.Text.Trim().Length > 0)
                _InterViewFormBLL.NomineeName = txtNomineeName.Text.Trim();
            else
                _InterViewFormBLL.NomineeName = null;


            if (txtNomineeDOB.Text.Trim().Length > 0)
                _InterViewFormBLL.NomineeDOB = Convert.ToDateTime(txtNomineeDOB.Text.Trim());
            else
                _InterViewFormBLL.NomineeDOB = null;

            if (txtNomineeRelation.Text.Trim().Length > 0)
                _InterViewFormBLL.NomineeRelation = txtNomineeRelation.Text.Trim();
            else
                _InterViewFormBLL.NomineeRelation = null;

            if (txtNomineeAge.Text.Trim().Length > 0)
                _InterViewFormBLL.NomineeAge = Convert.ToInt16(txtNomineeAge.Text.Trim());
            else
                _InterViewFormBLL.NomineeAge = null;

            if (txtStandanrd10Subject.Text.Trim().Length > 0)
                _InterViewFormBLL.Standanrd10Subject = txtStandanrd10Subject.Text.Trim();
            else
                _InterViewFormBLL.Standanrd10Subject = null;

            if (txtStandanrd10PassingYear.Text.Trim().Length > 0)
                _InterViewFormBLL.Standanrd10PassingYear = Convert.ToInt16(txtStandanrd10PassingYear.Text.Trim());
            else
                _InterViewFormBLL.Standanrd10PassingYear = null;

            if (txtStandanrd10Percentage.Text.Trim().Length > 0)
                _InterViewFormBLL.Standanrd10Percentage = Convert.ToDecimal(txtStandanrd10Percentage.Text);
            else
                _InterViewFormBLL.Standanrd10Percentage = null;

            if (txtStandanrd12Subject.Text.Trim().Length > 0)
                _InterViewFormBLL.Standanrd12Subject = txtStandanrd12Subject.Text.Trim();
            else
                _InterViewFormBLL.Standanrd12Subject = null;

            if (txtStandanrd12PassingYear.Text.Trim().Length > 0)
                _InterViewFormBLL.Standanrd12PassingYear = Convert.ToInt16(txtStandanrd12PassingYear.Text.Trim());
            else
                _InterViewFormBLL.Standanrd12PassingYear = null;

            if (txtStandanrd12Percentage.Text.Trim().Length > 0)
                _InterViewFormBLL.Standanrd12Percentage = Convert.ToDecimal(txtStandanrd12Percentage.Text.Trim());
            else
                _InterViewFormBLL.Standanrd12Percentage = null;

            if (txtGraduateSubject.Text.Trim().Length > 0)
                _InterViewFormBLL.GraduateSubject = txtGraduateSubject.Text.Trim();
            else
                _InterViewFormBLL.GraduateSubject = null;

            if (txtGraduatePassingYear.Text.Trim().Length > 0)
                _InterViewFormBLL.GraduatePassingYear = Convert.ToInt16(txtGraduatePassingYear.Text.Trim());
            else
                _InterViewFormBLL.GraduatePassingYear = null;

            if (txtGraduatePercentage.Text.Trim().Length > 0)
                _InterViewFormBLL.GraduatePercentage = Convert.ToDecimal(txtGraduatePercentage.Text);
            else
                _InterViewFormBLL.GraduatePercentage = null;

            if (txtPostGraduateSubject.Text.Trim().Length > 0)
                _InterViewFormBLL.PostGraduateSubject = txtPostGraduateSubject.Text.Trim();
            else
                _InterViewFormBLL.PostGraduateSubject = null;

            if (txtPostGraduatePassingYear.Text.Trim().Length > 0)
                _InterViewFormBLL.PostGraduatePassingYear = Convert.ToInt16(txtPostGraduatePassingYear.Text.Trim());
            else
                _InterViewFormBLL.PostGraduatePassingYear = null;

            if (txtPostGraduatePercentage.Text.Trim().Length > 0)
                _InterViewFormBLL.PostGraduatePercentage = Convert.ToDecimal(txtPostGraduatePercentage.Text.Trim());
            else
                _InterViewFormBLL.PostGraduatePercentage = null;

            if (txtOtherSubject.Text.Trim().Length > 0)
                _InterViewFormBLL.OtherSubject = txtOtherSubject.Text.Trim();
            else
                _InterViewFormBLL.OtherSubject = null;

            if (txtOtherPassingYear.Text.Trim().Length > 0)
                _InterViewFormBLL.OtherPassingYear = Convert.ToInt16(txtOtherPassingYear.Text.Trim());
            else
                _InterViewFormBLL.OtherPassingYear = null;

            if (txtOtherPercentage.Text.Trim().Length > 0)
                _InterViewFormBLL.OtherPercentage = Convert.ToInt16(txtOtherPercentage.Text.Trim());
            else
                _InterViewFormBLL.OtherPercentage = null;

            if (txtCertificateCourseName.Text.Trim().Length > 0)
                _InterViewFormBLL.CertificateCourseName = txtCertificateCourseName.Text.Trim();
            else
                _InterViewFormBLL.CertificateCourseName = null;

            if (txtCertificateCourseYear.Text.Trim().Length > 0)
                _InterViewFormBLL.CertificateCourseYear = Convert.ToInt16(txtCertificateCourseYear.Text.Trim());
            else
                _InterViewFormBLL.CertificateCourseYear = null;

            if (txtTrainingName.Text.Trim().Length > 0)
                _InterViewFormBLL.TrainingName = txtTrainingName.Text.Trim();
            else
                _InterViewFormBLL.TrainingName = null;

            if (txtTrainingYear.Text.Trim().Length > 0)
                _InterViewFormBLL.TrainingYear = Convert.ToInt16(txtTrainingYear.Text.Trim());
            else
                _InterViewFormBLL.TrainingYear = null;

            if (txtMedalName.Text.Trim().Length > 0)
                _InterViewFormBLL.MedalName = txtMedalName.Text.Trim();
            else
                _InterViewFormBLL.MedalName = null;

            if (txtMedalYear.Text.Trim().Length > 0)
                _InterViewFormBLL.MedalYear = Convert.ToInt16(txtMedalYear.Text.Trim());
            else
                _InterViewFormBLL.MedalYear = null;

            if (txtFirstCompanyName.Text.Trim().Length > 0)
                _InterViewFormBLL.FirstCompanyName = txtFirstCompanyName.Text.Trim();
            else
                _InterViewFormBLL.FirstCompanyName = null;

            if (txtFirstCompanyDesignation.Text.Trim().Length > 0)
                _InterViewFormBLL.FirstCompanyDesignation = txtFirstCompanyDesignation.Text.Trim();
            else
                _InterViewFormBLL.FirstCompanyDesignation = null;

            if (txtFirstCompanyExp.Text.Trim().Length > 0)
                _InterViewFormBLL.FirstCompanyExp = Convert.ToDecimal(txtFirstCompanyExp.Text.Trim());
            else
                _InterViewFormBLL.FirstCompanyExp = null;

            if (txtFirstCompanySalary.Text.Trim().Length > 0)
                _InterViewFormBLL.FirstCompanySalary = Convert.ToDecimal(txtFirstCompanySalary.Text.Trim());
            else
                _InterViewFormBLL.FirstCompanySalary = null;

            if (txtSecondCompanyName.Text.Trim().Length > 0)
                _InterViewFormBLL.SecondCompanyName = txtSecondCompanyName.Text.Trim();
            else
                _InterViewFormBLL.SecondCompanyName = null;

            if (txtSecondCompanyDesignation.Text.Trim().Length > 0)
                _InterViewFormBLL.SecondCompanyDesignation = txtSecondCompanyDesignation.Text.Trim();
            else
                _InterViewFormBLL.SecondCompanyDesignation = null;

            if (txtSecondCompanyExp.Text.Trim().Length > 0)
                _InterViewFormBLL.SecondCompanyExp = Convert.ToDecimal(txtSecondCompanyExp.Text.Trim());
            else
                _InterViewFormBLL.SecondCompanyExp = null;

            if (txtSecondCompanySalary.Text.Trim().Length > 0)
                _InterViewFormBLL.SecondCompanySalary = Convert.ToDecimal(txtSecondCompanySalary.Text.Trim());
            else
                _InterViewFormBLL.SecondCompanySalary = null;

            if (txtThirdCompanyName.Text.Trim().Length > 0)
                _InterViewFormBLL.ThirdCompanyName = txtThirdCompanyName.Text.Trim();
            else
                _InterViewFormBLL.ThirdCompanyName = null;

            if (txtThirdCompanyDesignation.Text.Trim().Length > 0)
                _InterViewFormBLL.ThirdCompanyDesignation = txtThirdCompanyDesignation.Text.Trim();
            else
                _InterViewFormBLL.ThirdCompanyDesignation = null;

            if (txtThirdCompanyExp.Text.Trim().Length > 0)
                _InterViewFormBLL.ThirdCompanyExp = Convert.ToDecimal(txtThirdCompanyExp.Text.Trim());
            else
                _InterViewFormBLL.ThirdCompanyExp = null;

            if (txtThirdCompanySalary.Text.Trim().Length > 0)
                _InterViewFormBLL.ThirdCompanySalary = Convert.ToDecimal(txtThirdCompanySalary.Text.Trim());
            else
                _InterViewFormBLL.ThirdCompanySalary = null;

            if (txtOtherExpNoExpDetails.Text.Trim().Length > 0)
                _InterViewFormBLL.OtherExpNoExpDetails = txtOtherExpNoExpDetails.Text.Trim();
            else
                _InterViewFormBLL.OtherExpNoExpDetails = null;

            if (fuadharcard.PostedFile != null && fuadharcard.PostedFile.FileName != "")
            {
                string[] getExtenstion = fuadharcard.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = fuadharcard.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _InterViewFormBLL.PresentMobileNo.ToString() + "-STU." + oExtension;

                _InterViewFormBLL.adharcard = filePath;

            }

            if (fuelectioncard.PostedFile != null && fuelectioncard.PostedFile.FileName != "")
            {
                string[] getExtenstion = fuelectioncard.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = fuelectioncard.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _InterViewFormBLL.PresentMobileNo.ToString() + "-F." + oExtension;

                _InterViewFormBLL.electioncard = filePath;

            }

            if (furationcard1.PostedFile != null && furationcard1.PostedFile.FileName != "")
            {
                string[] getExtenstion = furationcard1.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = furationcard1.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _InterViewFormBLL.PresentMobileNo.ToString() + "-STU." + oExtension;

                _InterViewFormBLL.rationcard1 = filePath;

            }

            if (furationcard2.PostedFile != null && furationcard2.PostedFile.FileName != "")
            {
                string[] getExtenstion = furationcard2.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = furationcard2.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _InterViewFormBLL.PresentMobileNo.ToString() + "-STU." + oExtension;

                _InterViewFormBLL.rationcard2 = filePath;

            }

            if (fupancard.PostedFile != null && fupancard.PostedFile.FileName != "")
            {
                string[] getExtenstion = fupancard.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = fupancard.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _InterViewFormBLL.PresentMobileNo.ToString() + "-STU." + oExtension;

                _InterViewFormBLL.pancard = filePath;

            }

            if (fuphoto.PostedFile != null && fuphoto.PostedFile.FileName != "")
            {
                string[] getExtenstion = fuphoto.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = fuphoto.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _InterViewFormBLL.PresentMobileNo.ToString() + "-STU." + oExtension;

                _InterViewFormBLL.photo = filePath;

            }

            if (fumarksheet.PostedFile != null && fumarksheet.PostedFile.FileName != "")
            {
                string[] getExtenstion = fumarksheet.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = fumarksheet.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _InterViewFormBLL.PresentMobileNo.ToString() + "-STU." + oExtension;

                _InterViewFormBLL.marksheet = filePath;

            }

            if (fucertificate.PostedFile != null && fucertificate.PostedFile.FileName != "")
            {
                string[] getExtenstion = fucertificate.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = fucertificate.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _InterViewFormBLL.PresentMobileNo.ToString() + "-STU." + oExtension;

                _InterViewFormBLL.certificate = filePath;

            }

            if (fuleavingcertificate1.PostedFile != null && fuleavingcertificate1.PostedFile.FileName != "")
            {
                string[] getExtenstion = fuleavingcertificate1.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = fuleavingcertificate1.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _InterViewFormBLL.PresentMobileNo.ToString() + "-STU." + oExtension;

                _InterViewFormBLL.leavingcertificate1 = filePath;

            }

            if (fuleavingcertificate2.PostedFile != null && fuleavingcertificate2.PostedFile.FileName != "")
            {
                string[] getExtenstion = fuleavingcertificate2.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = fuleavingcertificate2.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _InterViewFormBLL.PresentMobileNo.ToString() + "-STU." + oExtension;

                _InterViewFormBLL.leavingcertificate2 = filePath;

            }

            if (fusalaryslip.PostedFile != null && fusalaryslip.PostedFile.FileName != "")
            {
                string[] getExtenstion = fusalaryslip.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = fusalaryslip.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _InterViewFormBLL.PresentMobileNo.ToString() + "-STU." + oExtension;

                _InterViewFormBLL.salaryslip = filePath;

            }

            if (fuappointmentletter.PostedFile != null && fuappointmentletter.PostedFile.FileName != "")
            {
                string[] getExtenstion = fuleavingcertificate2.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = fuleavingcertificate2.FileName.Replace("." + oExtension, "");
                var filePathWithOutFileName = ConfigurationManager.AppSettings["FolderPath"].ToString() + "";
                var filePath = ConfigurationManager.AppSettings["FolderPath"].ToString() + "" + _InterViewFormBLL.PresentMobileNo.ToString() + "-STU." + oExtension;

                _InterViewFormBLL.appointmentletter = filePath;

            }

            bool isValid = Validate();

            if (isValid == true)
            {
                _InterViewFormBLL.Save();

                if (fuadharcard.PostedFile != null && fuadharcard.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = fuadharcard.PostedFile;
                    uploadedImage.SaveAs(_InterViewFormBLL.adharcard);
                }

                if (fuelectioncard.PostedFile != null && fuelectioncard.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = fuelectioncard.PostedFile;
                    uploadedImage.SaveAs(_InterViewFormBLL.electioncard);
                }

                if (furationcard1.PostedFile != null && furationcard1.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = furationcard1.PostedFile;
                    uploadedImage.SaveAs(_InterViewFormBLL.rationcard1);
                }

                if (furationcard2.PostedFile != null && furationcard2.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = furationcard2.PostedFile;
                    uploadedImage.SaveAs(_InterViewFormBLL.rationcard2);
                }

                if (fupancard.PostedFile != null && fupancard.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = fupancard.PostedFile;
                    uploadedImage.SaveAs(_InterViewFormBLL.pancard);
                }

                if (fuphoto.PostedFile != null && fuphoto.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = fuphoto.PostedFile;
                    uploadedImage.SaveAs(_InterViewFormBLL.photo);
                }

                if (fumarksheet.PostedFile != null && fumarksheet.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = fumarksheet.PostedFile;
                    uploadedImage.SaveAs(_InterViewFormBLL.marksheet);
                }

                if (fucertificate.PostedFile != null && fucertificate.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = fucertificate.PostedFile;
                    uploadedImage.SaveAs(_InterViewFormBLL.certificate);
                }

                if (fuleavingcertificate1.PostedFile != null && fuleavingcertificate1.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = fuleavingcertificate1.PostedFile;
                    uploadedImage.SaveAs(_InterViewFormBLL.leavingcertificate1);
                }

                if (fuleavingcertificate2.PostedFile != null && fuleavingcertificate2.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = fuleavingcertificate2.PostedFile;
                    uploadedImage.SaveAs(_InterViewFormBLL.leavingcertificate2);
                }

                if (fusalaryslip.PostedFile != null && fusalaryslip.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = fusalaryslip.PostedFile;
                    uploadedImage.SaveAs(_InterViewFormBLL.salaryslip);
                }

                if (fuappointmentletter.PostedFile != null && fuappointmentletter.PostedFile.FileName.ToString().Trim().Length > 0)
                {
                    HttpPostedFile uploadedImage = fuappointmentletter.PostedFile;
                    uploadedImage.SaveAs(_InterViewFormBLL.appointmentletter);
                }

                if (Request.QueryString["InterviewFormId"] == null)
                {
                    ShowErrors("Success", "Record Saved Succsessfully.");
                    Session["_InterViewFormBLL"] = null;
                    Session["_InterViewFormBLL"] = new InterViewFormBLL();
                    _InterViewFormBLL = (InterViewFormBLL)Session["_InterViewFormBLL"];
                    Reset();
                }
                else
                {
                    Session["_InterViewFormBLL"] = null;
                    Response.Redirect("InterViewForms.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
        txtFullName.Focus();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Session["_InterViewFormBLL"] = null;

            if (Request.QueryString["InterviewFormId"] == null)
                Response.Redirect("Default.aspx");
            else
                Response.Redirect("InterViewForm.aspx");
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

            if (key == "FullName")
            {
                lblFullName.CssClass = "";
                txtFullName.CssClass = "error form-control";
            }

            if (key == "PresentAddress")
            {
                lblPresentAddress.CssClass = "";
                txtPresentAddress.CssClass = "error form-control";
            }

            if (key == "PresentMobileNo")
            {
                lblPresentMobileNo.CssClass = "";
                txtPresentMobileNo.CssClass = "error form-control";
            }

            if (key == "DOB")
            {
                lblDOB.CssClass = "";
                txtDOB.CssClass = "error form-control";
            }

            if (key == "AadharCardNo")
            {
                lblAadharCardNo.CssClass = "";
                txtAadharCardNo.CssClass = "error form-control";
            }

            if (key == "PresentVillage")
            {
                lblPresentVillage.CssClass = "";
                txtPresentVillage.CssClass = "error form-control";
            }

            if (key == "PresentDistrict")
            {
                lblPresentDistrict.CssClass = "";
                txtPresentDistrict.CssClass = "error form-control";
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

            lblFullName.CssClass = "";
            txtFullName.CssClass = "form-control";

            lblPresentAddress.CssClass = "";
            txtPresentAddress.CssClass = "form-control";

            lblPresentMobileNo.CssClass = "";
            txtPresentMobileNo.CssClass = "form-control";

            lblDOB.CssClass = "";
            txtDOB.CssClass = "form-control";

            lblAadharCardNo.CssClass = "";
            txtAadharCardNo.CssClass = "form-control";

            lblPresentVillage.CssClass = "";
            txtPresentVillage.CssClass = "form-control";

            lblPresentAddress.CssClass = "";
            txtPresentDistrict.CssClass = "form-control";

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
            txtFullName.Text = "";
            txtPresentAddress.Text = "";
            txtPresentPost.Text = "";
            txtPresentVillage.Text = "";
            txtPresentDistrict.Text = "";
            txtPresentPinCode.Text = "";
            txtPresentMobileNo.Text = "";
            txtPermanentAddress.Text = "";
            txtPermanentPost.Text = "";
            txtPermanentVillage.Text = "";
            txtPermanentDistrict.Text = "";
            txtPermanentPinCode.Text = "";
            txtPermanentMobileNo.Text = "";
            txtDOB.Text = "";
            txtBloodGroup.Text = "";
            txtAadharCardNo.Text = "";
            txtPanCardNo.Text = "";
            txtElectionCardNo.Text = "";
            txtCategory.Text = "";
            txtEmail.Text = "";
            txtFatherName.Text = "";
            txtFatherOccupation.Text = "";
            txtFatherEducation.Text = "";
            txtFatherMobileNo.Text = "";
            txtMotherName.Text = "";
            txtMotherOccupation.Text = "";
            txtMotherEducation.Text = "";
            txtMotherMobileNo.Text = "";
            txtWifeName.Text = "";
            txtWifeOccupation.Text = "";
            txtWifeEducation.Text = "";
            txtWifeMobileNo.Text = "";
            txtBrotherName.Text = "";
            txtBrotherOccupation.Text = "";
            txtBrotherEducation.Text = "";
            txtBrotherMobileNo.Text = "";
            txtNomineeName.Text = "";
            txtNomineeDOB.Text = "";
            txtNomineeRelation.Text = "";
            txtNomineeAge.Text = "";
            txtStandanrd10Subject.Text = "";
            txtStandanrd10PassingYear.Text = "";
            txtStandanrd10Percentage.Text = "";
            txtStandanrd12Subject.Text = "";
            txtStandanrd12PassingYear.Text = "";
            txtStandanrd12Percentage.Text = "";
            txtGraduateSubject.Text = "";
            txtGraduatePassingYear.Text = "";
            txtGraduatePercentage.Text = "";
            txtPostGraduateSubject.Text = "";
            txtPostGraduatePassingYear.Text = "";
            txtPostGraduatePercentage.Text = "";
            txtOtherSubject.Text = "";
            txtOtherPassingYear.Text = "";
            txtOtherPercentage.Text = "";
            txtCertificateCourseName.Text = "";
            txtCertificateCourseYear.Text = "";
            txtTrainingName.Text = "";
            txtTrainingYear.Text = "";
            txtMedalName.Text = "";
            txtMedalYear.Text = "";
            txtFirstCompanyName.Text = "";
            txtFirstCompanyDesignation.Text = "";
            txtFirstCompanyExp.Text = "";
            txtFirstCompanySalary.Text = "";
            txtSecondCompanyName.Text = "";
            txtSecondCompanyDesignation.Text = "";
            txtSecondCompanyExp.Text = "";
            txtSecondCompanySalary.Text = "";
            txtThirdCompanyName.Text = "";
            txtThirdCompanyDesignation.Text = "";
            txtThirdCompanyExp.Text = "";
            txtThirdCompanySalary.Text = "";
            txtOtherExpNoExpDetails.Text = "";

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
            if (_InterViewFormBLL.FullName != null)
                txtFullName.Text = _InterViewFormBLL.FullName;

            if (_InterViewFormBLL.mailto != null)
                txtEmail.Text = _InterViewFormBLL.mailto;

            if (_InterViewFormBLL.PresentAddress != null)
                txtPresentAddress.Text = _InterViewFormBLL.PresentAddress;

            if (_InterViewFormBLL.PresentPost != null)
                txtPresentPost.Text = _InterViewFormBLL.PresentPost;

            if (_InterViewFormBLL.PresentVillage != null)
                txtPresentVillage.Text = _InterViewFormBLL.PresentVillage;

            if (_InterViewFormBLL.PresentDistrict != null)
                txtPresentDistrict.Text = _InterViewFormBLL.PresentDistrict;

            if (_InterViewFormBLL.PresentPinCode != null)
                txtPresentPinCode.Text = _InterViewFormBLL.PresentPinCode;

            if (_InterViewFormBLL.PresentMobileNo != null)
                txtPresentMobileNo.Text = _InterViewFormBLL.PresentMobileNo;

            if (_InterViewFormBLL.PermanentAddress != null)
                txtPermanentAddress.Text = _InterViewFormBLL.PermanentAddress;

            if (_InterViewFormBLL.PermanentPost != null)
                txtPermanentPost.Text = _InterViewFormBLL.PermanentPost;

            if (_InterViewFormBLL.PermanentVillage != null)
                txtPermanentVillage.Text = _InterViewFormBLL.PermanentVillage;

            if (_InterViewFormBLL.PermanentDistrict != null)
                txtPermanentDistrict.Text = _InterViewFormBLL.PermanentDistrict;

            if (_InterViewFormBLL.PermanentPinCode != null)
                txtPermanentPinCode.Text = _InterViewFormBLL.PermanentPinCode;

            if (_InterViewFormBLL.PermanentMobileNo != null)
                txtPermanentMobileNo.Text = _InterViewFormBLL.PermanentMobileNo;

            if (_InterViewFormBLL.DOB != null)
                txtDOB.Text = Convert.ToDateTime(_InterViewFormBLL.DOB).ToString("dd-MMM-yyyy");

            if (_InterViewFormBLL.BloodGroup != null)
                txtBloodGroup.Text = _InterViewFormBLL.BloodGroup;


            if (_InterViewFormBLL.AadharCardNo != null)
                txtAadharCardNo.Text = _InterViewFormBLL.AadharCardNo;

            if (_InterViewFormBLL.PanCardNo != null)
                txtPanCardNo.Text = _InterViewFormBLL.PanCardNo;

            if (_InterViewFormBLL.ElectionCardNo != null)
                txtElectionCardNo.Text = _InterViewFormBLL.ElectionCardNo;

            if (_InterViewFormBLL.FullName != null)
                txtFullName.Text = _InterViewFormBLL.FullName;

            if (_InterViewFormBLL.Category != null)
                txtCategory.Text = _InterViewFormBLL.Category;

            if (_InterViewFormBLL.FatherName != null)
                txtFatherName.Text = _InterViewFormBLL.FatherName;

            if (_InterViewFormBLL.FatherOccupation != null)
                txtFatherOccupation.Text = _InterViewFormBLL.FatherOccupation;

            if (_InterViewFormBLL.FatherEducation != null)
                txtFatherEducation.Text = _InterViewFormBLL.FatherEducation;

            if (_InterViewFormBLL.FatherMobileNo != null)
                txtFatherMobileNo.Text = _InterViewFormBLL.FatherMobileNo;

            if (_InterViewFormBLL.MotherName != null)
                txtMotherName.Text = _InterViewFormBLL.MotherName;

            if (_InterViewFormBLL.MotherOccupation != null)
                txtMotherOccupation.Text = _InterViewFormBLL.MotherOccupation;

            if (_InterViewFormBLL.MotherEducation != null)
                txtMotherEducation.Text = _InterViewFormBLL.MotherEducation;


            if (_InterViewFormBLL.MotherMobileNo != null)
                txtMotherMobileNo.Text = _InterViewFormBLL.MotherMobileNo;

            if (_InterViewFormBLL.WifeName != null)
                txtWifeName.Text = _InterViewFormBLL.WifeName;

            if (_InterViewFormBLL.WifeOccupation != null)
                txtWifeOccupation.Text = _InterViewFormBLL.WifeOccupation;

            if (_InterViewFormBLL.WifeEducation != null)
                txtWifeEducation.Text = _InterViewFormBLL.WifeEducation;

            if (_InterViewFormBLL.WifeMobileNo != null)
                txtWifeMobileNo.Text = _InterViewFormBLL.WifeMobileNo;

            if (_InterViewFormBLL.BrotherName != null)
                txtBrotherName.Text = _InterViewFormBLL.BrotherName;

            if (_InterViewFormBLL.BrotherOccupation != null)
                txtBrotherOccupation.Text = _InterViewFormBLL.BrotherOccupation;

            if (_InterViewFormBLL.BrotherEducation != null)
                txtBrotherEducation.Text = _InterViewFormBLL.BrotherEducation;

            if (_InterViewFormBLL.BrotherMobileNo != null)
                txtBrotherMobileNo.Text = _InterViewFormBLL.BrotherMobileNo;

            if (_InterViewFormBLL.NomineeName != null)
                txtNomineeName.Text = _InterViewFormBLL.NomineeName;

            if (_InterViewFormBLL.NomineeDOB != null)
                txtNomineeDOB.Text = Convert.ToDateTime(_InterViewFormBLL.NomineeDOB).ToString("dd-MMM-yyyy");

            if (_InterViewFormBLL.NomineeRelation != null)
                txtNomineeRelation.Text = _InterViewFormBLL.NomineeRelation;

            if (_InterViewFormBLL.NomineeAge != null)
                txtNomineeAge.Text = Convert.ToInt16(_InterViewFormBLL.NomineeAge).ToString();

            if (_InterViewFormBLL.Standanrd10Subject != null)
                txtStandanrd10Subject.Text = _InterViewFormBLL.Standanrd10Subject;

            if (_InterViewFormBLL.Standanrd10PassingYear != null)
                txtStandanrd10PassingYear.Text = Convert.ToInt16(_InterViewFormBLL.Standanrd10PassingYear).ToString();

            if (_InterViewFormBLL.Standanrd10Percentage != null)
                txtStandanrd10Percentage.Text = Convert.ToInt16(_InterViewFormBLL.Standanrd10Percentage).ToString();

            if (_InterViewFormBLL.Standanrd12Subject != null)
                txtStandanrd12Subject.Text = _InterViewFormBLL.Standanrd12Subject;

            if (_InterViewFormBLL.Standanrd12PassingYear != null)
                txtStandanrd12PassingYear.Text = Convert.ToInt16(_InterViewFormBLL.Standanrd12PassingYear).ToString();

            if (_InterViewFormBLL.Standanrd12Percentage != null)
                txtStandanrd12Percentage.Text = Convert.ToInt16(_InterViewFormBLL.Standanrd12Percentage).ToString();

            if (_InterViewFormBLL.GraduateSubject != null)
                txtGraduateSubject.Text = _InterViewFormBLL.GraduateSubject;

            if (_InterViewFormBLL.GraduatePassingYear != null)
                txtGraduatePassingYear.Text = Convert.ToInt16(_InterViewFormBLL.GraduatePassingYear).ToString();

            if (_InterViewFormBLL.GraduatePercentage != null)
                txtGraduatePercentage.Text = Convert.ToInt16(_InterViewFormBLL.GraduatePercentage).ToString();

            if (_InterViewFormBLL.PostGraduateSubject != null)
                txtPostGraduateSubject.Text = _InterViewFormBLL.PostGraduateSubject;

            if (_InterViewFormBLL.PostGraduatePassingYear != null)
                txtPostGraduatePassingYear.Text = Convert.ToInt16(_InterViewFormBLL.PostGraduatePassingYear).ToString();

            if (_InterViewFormBLL.PostGraduatePercentage != null)
                txtPostGraduatePercentage.Text = Convert.ToInt16(_InterViewFormBLL.PostGraduatePercentage).ToString();

            if (_InterViewFormBLL.OtherSubject != null)
                txtOtherSubject.Text = _InterViewFormBLL.OtherSubject;

            if (_InterViewFormBLL.OtherPassingYear != null)
                txtOtherPassingYear.Text = Convert.ToInt16(_InterViewFormBLL.OtherPassingYear).ToString();

            if (_InterViewFormBLL.OtherPercentage != null)
                txtOtherPercentage.Text = Convert.ToInt16(_InterViewFormBLL.OtherPercentage).ToString();

            if (_InterViewFormBLL.CertificateCourseName != null)
                txtCertificateCourseName.Text = _InterViewFormBLL.CertificateCourseName;

            if (_InterViewFormBLL.CertificateCourseYear != null)
                txtCertificateCourseYear.Text = Convert.ToInt16(_InterViewFormBLL.CertificateCourseYear).ToString();

            if (_InterViewFormBLL.TrainingName != null)
                txtTrainingName.Text = _InterViewFormBLL.TrainingName;

            if (_InterViewFormBLL.TrainingYear != null)
                txtTrainingYear.Text = Convert.ToInt16(_InterViewFormBLL.TrainingYear).ToString();

            if (_InterViewFormBLL.MedalName != null)
                txtMedalName.Text = _InterViewFormBLL.MedalName;

            if (_InterViewFormBLL.MedalYear != null)
                txtMedalYear.Text = Convert.ToInt16(_InterViewFormBLL.MedalYear).ToString();

            if (_InterViewFormBLL.FirstCompanyName != null)
                txtFirstCompanyName.Text = _InterViewFormBLL.FirstCompanyName;

            if (_InterViewFormBLL.FirstCompanyDesignation != null)
                txtFirstCompanyDesignation.Text = _InterViewFormBLL.FirstCompanyDesignation;

            if (_InterViewFormBLL.FirstCompanyExp != null)
                txtFirstCompanyExp.Text = Convert.ToDecimal(_InterViewFormBLL.FirstCompanyExp).ToString();

            if (_InterViewFormBLL.FirstCompanySalary != null)
                txtFirstCompanySalary.Text = Convert.ToDecimal(_InterViewFormBLL.FirstCompanySalary).ToString();

            if (_InterViewFormBLL.SecondCompanyName != null)
                txtSecondCompanyName.Text = _InterViewFormBLL.SecondCompanyName;

            if (_InterViewFormBLL.SecondCompanyDesignation != null)
                txtSecondCompanyDesignation.Text = _InterViewFormBLL.SecondCompanyDesignation;

            if (_InterViewFormBLL.SecondCompanyExp != null)
                txtSecondCompanyExp.Text = Convert.ToDecimal(_InterViewFormBLL.SecondCompanyExp).ToString();

            if (_InterViewFormBLL.SecondCompanySalary != null)
                txtSecondCompanySalary.Text = Convert.ToDecimal(_InterViewFormBLL.SecondCompanySalary).ToString();

            if (_InterViewFormBLL.ThirdCompanyName != null)
                txtThirdCompanyName.Text = _InterViewFormBLL.ThirdCompanyName;

            if (_InterViewFormBLL.ThirdCompanyDesignation != null)
                txtThirdCompanyDesignation.Text = _InterViewFormBLL.ThirdCompanyDesignation;

            if (_InterViewFormBLL.ThirdCompanyExp != null)
                txtThirdCompanyExp.Text = Convert.ToDecimal(_InterViewFormBLL.ThirdCompanyExp).ToString();

            if (_InterViewFormBLL.ThirdCompanySalary != null)
                txtThirdCompanySalary.Text = Convert.ToDecimal(_InterViewFormBLL.ThirdCompanySalary).ToString();

            if (_InterViewFormBLL.OtherExpNoExpDetails != null)
                txtOtherExpNoExpDetails.Text = _InterViewFormBLL.OtherExpNoExpDetails;

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
            SortedList sl = _InterViewFormBLL.Validate();

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