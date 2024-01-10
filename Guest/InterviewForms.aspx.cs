using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;
using System.Xml.Linq;
using System.Net.Mail;
using System.Drawing;
using System.IO;
using System.Web.Configuration;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;

public partial class Guest_InterviewForms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HideErrors();
            if (string.IsNullOrEmpty((string)Session["MobileNo"]) || string.IsNullOrEmpty((string)Session["Language"]))
            {
                Response.Redirect("~/Guests/SelectLanguage.aspx");
            }
            else
            {
                if (Session["IsApproved"].ToString() != "" || Session["IsApproved"] != null)
                {
                    if (Convert.ToBoolean(Session["IsApproved"]) == true)
                    {
                        pnlErr.Visible = true;

                        txtFullName.Enabled = false;
                        txtDOB.Enabled = false;
                        txtPF.Enabled = false;
                        ddlBloodGroup.Enabled = false;
                        txtresidentialAddress.Enabled = false;
                        txtresidentialPost.Enabled = false;
                        txtresidentialVillage.Enabled = false;
                        txtresidentialDistrict.Enabled = false;
                        txtresidentialPinCode.Enabled = false;
                        txtresidentialMobileNo.Enabled = false;
                        chkSameAsAbove.Enabled = false;
                        txtPermanentAddress.Enabled = false;
                        txtPermanentPost.Enabled = false;
                        txtPermanentVillage.Enabled = false;
                        txtPermanentDistrict.Enabled = false;
                        txtPermanentPinCode.Enabled = false;
                        txtPermanentMobileNo.Enabled = false;
                        rblCategory.Enabled = false;
                        txtAadharCard.Enabled = false;
                        txtPanCard.Enabled = false;
                        txtElectionCard.Enabled = false;
                        rblMaritalStatus.Enabled = false;
                        txtNomineeName.Enabled = false;
                        txtNomineeDOB.Enabled = false;
                        txtNomineeAge.Enabled = false;
                        txtRelationWithNominee.Enabled = false;
                        txtEmail.Enabled = false;
                        fuDigitalSignature.Enabled = false;
                        gvEducationDetails.Enabled = false;
                        txtDukeReferences.Enabled = false;
                        txtVillageNotableRefernces.Enabled = false;
                        txtDukeReferencesMobileNo.Enabled = false;
                        txtVillageNotableReferncesMobileNo.Enabled = false;

                        ShowErrors("error", "Your Profile is reviwed for change please contact administrator.");
                    }
                }
                else
                {
                    Response.Redirect("~/Guests/SelectLanguage.aspx");
                }


                if (Session["Language"] == "Gujarati")
                {
                    gvEducationDetails.Columns[0].HeaderText = "શિક્ષણ નું સ્તર";
                    gvEducationDetails.Columns[1].HeaderText = "બોર્ડ/યુનિવર્સિટીનું નામ";
                    gvEducationDetails.Columns[2].HeaderText = "શિક્ષણ પાસ થવાનું વર્ષ";
                    gvEducationDetails.Columns[3].HeaderText = "ટકાવારી";
                }
                else if (Session["Language"] == "Hindi")
                {
                    gvEducationDetails.Columns[0].HeaderText = "शिक्षा स्तर";
                    gvEducationDetails.Columns[1].HeaderText = "बोर्ड/विश्वविद्यालय का नाम";
                    gvEducationDetails.Columns[2].HeaderText = "शिक्षा पास करने का वर्ष";
                    gvEducationDetails.Columns[3].HeaderText = "प्रतिशत";
                }

                gvEducationDetails.DataBind();
                GetRegistrationById(Session["MobileNo"].ToString());

                if (Session["Language"] == "Gujarati")
                {
                    lblTitle.Text = "નોંધણી એન્ટ્રી - મૂલ્ય - [નવો મોડ]";
                    lblFullName.Text = "પૂરું નામ";
                    lblDOB.Text = "જન્મ તારીખ";
                    lblPF.Text = "પીએફ નંબર";
                    lblBloodGroup.Text = "બ્લડ ગ્રુપ";
                    lblresidentialAddress.Text = "રહેઠાણનું સરનામું";
                    lblresidentialPost.Text = "પોસ્ટ";
                    lblresidentialVillage.Text = "ગામ";
                    lblresidentialDistrict.Text = "જિલ્લો";
                    lblresidentialPinCode.Text = "પીન કોડ";
                    lblresidentialMobileNo.Text = "મોબાઈલ નમ્બર";
                    lblCategory.Text = "શ્રેણી";
                    lblAadharCard.Text = "આધાર કાર્ડ";
                    lblMaritalStatus.Text = "વૈવાહિક સ્થિતિ";
                    lblRelationWithNominee.Text = "નોમિની સાથે સંબંધ";
                    lblPanCard.Text = "પાન કાર્ડ";
                    lblElectionCard.Text = "ચૂંટણી કાર્ડ";
                    lblNomineeName.Text = "વારસદાર નામ";
                    lblNomineeDOB.Text = "વારસદાર જન્મ તારીખ";
                    lblNomineeAge.Text = "વારસદાર ઉંમર";
                    lblPermanentAddress.Text = "કાયમી સરનામુ";
                    lblPermanentPost.Text = "કાયમી પોસ્ટ";
                    lblPermanentVillage.Text = "કાયમી ગામ";
                    lblPermanentPost.Text = "જિલ્લો";
                    lblPermanentPinCode.Text = "પીન કોડ";
                    lblPermanentMobileNo.Text = "મોબાઈલ નમ્બર";
                    lblEducationHeader.Text = "શિક્ષણની વિગત";
                    lblEmail.Text = "ઈમેલ";
                    btnSubmit.Text = "સાચવો અને આગળ જાઓ";
                    lblSameasAbove.Text = "ઉપરની જેમ સમાન";
                    lblDukeReferences.Text = "ડ્યુક મેનેજમેન્ટ સંદર્ભો";
                    lblVillageNotableRefernces.Text = "ગામ નોંધપાત્ર સંદર્ભો";
                    lblDukeReferencesMobileNo.Text = "ડ્યુક મેનેજમેન્ટ સંદર્ભો મોબાઇલ નંબર";
                    lblVillageNotableReferncesMobileNo.Text = "ગામ નોંધપાત્ર સંદર્ભો મોબાઇલ નંબર";
                }
                else if (Session["Language"] == "Hindi")
                {
                    lblTitle.Text = "नोटिफिकेशन एंट्री - मूल्य - [नया मोड]";
                    lblFullName.Text = "पूरा नाम";
                    lblDOB.Text = "जन्म तिथि";
                    lblPF.Text = "पीएफ नंबर";
                    lblBloodGroup.Text = "रक्त समूह";
                    lblresidentialAddress.Text = "निवास पता";
                    lblresidentialPost.Text = "पोस्ट";
                    lblresidentialVillage.Text = "गाँव";
                    lblresidentialDistrict.Text = "जिला";
                    lblresidentialPinCode.Text = "पिन कोड";
                    lblresidentialMobileNo.Text = "मोबाइल नंबर";
                    lblCategory.Text = "श्रेणी";
                    lblAadharCard.Text = "आधार कार्ड";
                    lblMaritalStatus.Text = "वैवाहिक स्थिति";
                    lblRelationWithNominee.Text = "नामांकन से संबंध";
                    lblPanCard.Text = "पैन कार्ड";
                    lblElectionCard.Text = "चुनाव कार्ड";
                    lblNomineeName.Text = "नामांकन करने वाले का नाम";
                    lblNomineeDOB.Text = "नामांकन करने वाले की जन्म तिथि";
                    lblNomineeAge.Text = "नामांकन करने वाले की आयु";
                    lblPermanentAddress.Text = "स्थायी पता";
                    lblPermanentPost.Text = "स्थायी पोस्ट";
                    lblPermanentVillage.Text = "स्थायी गाँव";
                    lblPermanentDistrict.Text = "जिला";
                    lblPermanentPinCode.Text = "पिन कोड";
                    lblPermanentMobileNo.Text = "स्थायी मोबाइल नंबर";
                    lblEducationHeader.Text = "शिक्षा का विवरण";
                    lblEmail.Text = "ईमेल";
                    btnSubmit.Text = "सहेजें और आगे बढ़ें";
                    lblSameasAbove.Text = "ऊपर की तरह";
                    lblDukeReferences.Text = "ड्यूक सन्दर्भ";
                    lblVillageNotableRefernces.Text = "गाँव के उल्लेखनीय सन्दर्भ";
                    lblDukeReferencesMobileNo.Text = "ड्यूक प्रबंधन संदर्भ मोबाइल नंबर";
                    lblVillageNotableReferncesMobileNo.Text = "गाँव उल्लेखनीय संदर्भ मोबाइल नंबर";
                }
            }
        }
    }
    private void GetRegistrationById(string MobileNo)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Master_Registration";
            sqlCmd.Parameters.AddWithValue("@Action", "GetRegistrationById");
            sqlCmd.Parameters.AddWithValue("@MobileNo", Session["MobileNo"].ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                txtFullName.Text = dataSet.Tables[0].Rows[0]["FullName"].ToString();
                txtDOB.Text = dataSet.Tables[0].Rows[0]["DOB"].ToString();
                txtPF.Text = dataSet.Tables[0].Rows[0]["PFNo"].ToString();
                txtresidentialAddress.Text = dataSet.Tables[0].Rows[0]["ResidentAddress"].ToString();
                txtresidentialPinCode.Text = dataSet.Tables[0].Rows[0]["ResidentPinCode"].ToString();
                txtresidentialVillage.Text = dataSet.Tables[0].Rows[0]["ResidentVillage"].ToString();
                txtresidentialDistrict.Text = dataSet.Tables[0].Rows[0]["ResidentDistrict"].ToString();
                txtresidentialMobileNo.Text = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
                txtPermanentAddress.Text = dataSet.Tables[0].Rows[0]["permanentAddress"].ToString();
                txtPermanentPinCode.Text = dataSet.Tables[0].Rows[0]["permanentPinCode"].ToString();
                txtPermanentVillage.Text = dataSet.Tables[0].Rows[0]["permanentVillage"].ToString();
                txtPermanentDistrict.Text = dataSet.Tables[0].Rows[0]["permanentDistrict"].ToString();
                txtPermanentMobileNo.Text = dataSet.Tables[0].Rows[0]["MobileNo"].ToString();
                ddlBloodGroup.SelectedValue = dataSet.Tables[0].Rows[0]["BloodGroup"].ToString();
                rblCategory.SelectedValue = dataSet.Tables[0].Rows[0]["Category"].ToString();
                txtAadharCard.Text = dataSet.Tables[0].Rows[0]["AadharCardNo"].ToString();
                txtPanCard.Text = dataSet.Tables[0].Rows[0]["PANCard"].ToString();
                txtElectionCard.Text = dataSet.Tables[0].Rows[0]["ElectionCard"].ToString();
                txtNomineeName.Text = dataSet.Tables[0].Rows[0]["NomineeName"].ToString();
                txtNomineeDOB.Text = dataSet.Tables[0].Rows[0]["NomineeDBO"].ToString();
                txtNomineeAge.Text = dataSet.Tables[0].Rows[0]["NomineeAGE"].ToString();
                txtRelationWithNominee.Text = dataSet.Tables[0].Rows[0]["NomineeWithRelation"].ToString();
                rblMaritalStatus.SelectedValue = dataSet.Tables[0].Rows[0]["MaritalStatus"].ToString();
                txtresidentialPost.Text = dataSet.Tables[0].Rows[0]["Post"].ToString();
                txtPermanentPost.Text = dataSet.Tables[0].Rows[0]["Post"].ToString();
                txtEmail.Text = dataSet.Tables[0].Rows[0]["Email"].ToString();
                lblDigitalSignature.Text = dataSet.Tables[0].Rows[0]["DigitSignaturePath"].ToString();
                txtDukeReferences.Text = dataSet.Tables[0].Rows[0]["DukeReferences"].ToString();
                txtVillageNotableRefernces.Text = dataSet.Tables[0].Rows[0]["VillagePersonReferences"].ToString();
                txtDukeReferencesMobileNo.Text = dataSet.Tables[0].Rows[0]["DukeReferencesMobileNo"].ToString();
                txtVillageNotableReferncesMobileNo.Text = dataSet.Tables[0].Rows[0]["VillagePersonReferencesMobileNo"].ToString();
            }

            if (dataSet.Tables[1].Rows.Count > 0)
            {
                gvEducationDetails.DataSource = dataSet.Tables[1];
                gvEducationDetails.DataBind();
            }
            else
            {
                List<EducationDetail> educationDetails = new List<EducationDetail>
            {
                new EducationDetail { EducationLevel = "STD.10" },
                new EducationDetail { EducationLevel = "STD.12" },
                new EducationDetail { EducationLevel = "Graduate/Diploma" },
                new EducationDetail { EducationLevel = "Post Graduate/Degree" },
                new EducationDetail { EducationLevel = "ITI/Others" }
            };


                gvEducationDetails.DataSource = educationDetails;
                gvEducationDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
        }
    }
    protected void lnkBtnSubmit_click(object sender, EventArgs e)
    {
        try
        {

            if (Convert.ToBoolean(Session["IsApproved"]) == true)
            {
                Response.Redirect("~/Guest/FamilyDetail.aspx");
            }
            else
            {
                string DigitalSignature = "";

                if (fuDigitalSignature.HasFile)
                {
                    string uniqueFileName = Guid.NewGuid().ToString();
                    string fileExtension = Path.GetExtension(fuDigitalSignature.FileName);
                    string newFileName = uniqueFileName + fileExtension;
                    string uploadFolder = Server.MapPath("~/images/");
                    string fullPath = Path.Combine(uploadFolder, newFileName);
                    DigitalSignature = "~/images/" + newFileName;
                    fuDigitalSignature.SaveAs(fullPath);
                }

                if (DigitalSignature == "")
                {
                    DigitalSignature = lblDigitalSignature.Text;
                }

                if (DigitalSignature == "")
                {
                    if (Session["Language"].ToString() == "Gujarati")
                    {
                        ShowErrors("err", "ડિજિટલ હસ્તાક્ષર જરૂરી!!");
                    }
                    else if (Session["Language"].ToString() == "Gujarati")
                    {
                        ShowErrors("err", "डिजिटल हस्ताक्षर आवश्यक!!");
                    }
                    else
                    {
                        ShowErrors("err", "Digital Signature Required!!");
                    }
                }

                DataTable educationTable = new DataTable();
                educationTable.Columns.Add("EducationLevel", typeof(string));
                educationTable.Columns.Add("BoardUniversityName", typeof(string));
                educationTable.Columns.Add("PassingYear", typeof(string));
                educationTable.Columns.Add("Percentage", typeof(decimal));

                List<string[]> educationDetailsList = new List<string[]>();

                bool isFirstRow = true;

                foreach (GridViewRow row in gvEducationDetails.Rows)
                {

                    Label lblEducationLevel = (Label)row.FindControl("lblEducationLevel");
                    TextBox txtBoardUniversity = (TextBox)row.FindControl("txtBoardUniversity");
                    TextBox txtPassingYear = (TextBox)row.FindControl("txtPassingYear");
                    TextBox txtPercentage = (TextBox)row.FindControl("txtPercentage");


                    if (isFirstRow)
                    {
                        if (string.IsNullOrEmpty(txtBoardUniversity.Text) || string.IsNullOrEmpty(txtPassingYear.Text) || string.IsNullOrEmpty(txtPercentage.Text))
                        {
                            lblMessage.Text = "Please fill in all education details for the first row.";
                            lblMessage.Visible = true;
                            return; // Stop processing if validation fails for the first row
                        }
                        else
                        {
                            // If validation passes for the first row, hide the error message
                            lblMessage.Visible = false;
                        }

                        // Reset the flag after validating the first row
                        isFirstRow = false;
                    }

                    string educationLevel = lblEducationLevel.Text;
                    string boardUniversityName = txtBoardUniversity.Text;
                    string passingYear = txtPassingYear.Text;
                    if (txtPercentage.Text == "")
                    {
                        txtPercentage.Text = "0.0";
                    }
                    string percentage = txtPercentage.Text;

                    DataRow dataRow = educationTable.NewRow();
                    dataRow["EducationLevel"] = educationLevel;
                    dataRow["BoardUniversityName"] = boardUniversityName;
                    dataRow["PassingYear"] = passingYear;
                    dataRow["Percentage"] = percentage;

                    educationTable.Rows.Add(dataRow);
                }

                SqlCommand sqlCmd = new SqlCommand();
                GeneralDAL objDal = new GeneralDAL();
                objDal.OpenSQLConnection();
                sqlCmd.Connection = objDal.ActiveSQLConnection();
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = "FitToJob_Master_Registration";
                sqlCmd.Parameters.AddWithValue("@Action", "updateRegistrationDetails");
                sqlCmd.Parameters.AddWithValue("@MobileNo", Session["MobileNo"]);

                sqlCmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                sqlCmd.Parameters.AddWithValue("@BirthDate", DateTime.ParseExact(txtDOB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
                sqlCmd.Parameters.AddWithValue("@PFNumber", txtPF.Text);
                sqlCmd.Parameters.AddWithValue("@BloodGroup", ddlBloodGroup.SelectedValue.ToString());

                sqlCmd.Parameters.AddWithValue("@ResidentialAddress", txtresidentialAddress.Text);
                sqlCmd.Parameters.AddWithValue("@ResidentialPincode", txtresidentialPinCode.Text);
                sqlCmd.Parameters.AddWithValue("@ResidentialVillage", txtresidentialVillage.Text);
                sqlCmd.Parameters.AddWithValue("@ResidentialDistrict", txtresidentialDistrict.Text);
                sqlCmd.Parameters.AddWithValue("@ResidentialMobileNumber", txtresidentialMobileNo.Text);

                sqlCmd.Parameters.AddWithValue("@permanentAddress", txtPermanentAddress.Text);
                sqlCmd.Parameters.AddWithValue("@permanentPinCode", txtPermanentPinCode.Text);
                sqlCmd.Parameters.AddWithValue("@permanentVillage", txtPermanentVillage.Text);
                sqlCmd.Parameters.AddWithValue("@permanentDistrict", txtPermanentDistrict.Text);
                sqlCmd.Parameters.AddWithValue("@PermanentMobileNumber", txtPermanentMobileNo.Text);


                sqlCmd.Parameters.AddWithValue("@PanCardNumber", txtPanCard.Text);
                sqlCmd.Parameters.AddWithValue("@ElectionCard", txtElectionCard.Text);
                sqlCmd.Parameters.AddWithValue("@Category", rblCategory.SelectedValue);
                sqlCmd.Parameters.AddWithValue("@AadharCardNo", txtAadharCard.Text);

                sqlCmd.Parameters.AddWithValue("@MaritalStatus", rblMaritalStatus.SelectedValue);
                sqlCmd.Parameters.AddWithValue("@NomineeName", txtNomineeName.Text);

                sqlCmd.Parameters.AddWithValue("@Post", txtPermanentPost.Text);


                string nomineeDOB = txtNomineeDOB.Text.Trim();

                if (txtNomineeDOB.Text.Trim() != "")
                {
                    sqlCmd.Parameters.AddWithValue("@NomineeBirthDate", DateTime.ParseExact(txtDOB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture));
                }
                else
                {
                    sqlCmd.Parameters.AddWithValue("@NomineeBirthDate", DBNull.Value);
                }


                sqlCmd.Parameters.AddWithValue("@NomineeAge", txtNomineeAge.Text);
                sqlCmd.Parameters.AddWithValue("@RelationWithNominee", txtRelationWithNominee.Text);
                sqlCmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                sqlCmd.Parameters.AddWithValue("@DukeReferences", txtDukeReferences.Text);
                sqlCmd.Parameters.AddWithValue("@VillagePersonReferences", txtVillageNotableRefernces.Text);
                sqlCmd.Parameters.AddWithValue("@DukeReferencesMobileNo", txtDukeReferencesMobileNo.Text);
                sqlCmd.Parameters.AddWithValue("@VillagePersonReferencesMobileNo", txtVillageNotableReferncesMobileNo.Text);

                sqlCmd.Parameters.AddWithValue("@DigitSignaturePath", DigitalSignature);
                SqlParameter tvpParam = new SqlParameter("@UT_EducationDetail", SqlDbType.Structured);
                tvpParam.Value = educationTable;
                tvpParam.TypeName = "dbo.UT_EducationDetail"; // Adjust the schema and type name accordingly

                // Add the TVP parameter to the SqlCommand
                sqlCmd.Parameters.Add(tvpParam);

                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    if (dataSet.Tables[0].Rows[0]["IsSuccessful"].ToString() == "1")
                    {
                        Session["MaritalStatus"] = rblMaritalStatus.SelectedItem.Text;
                        Response.Redirect("~/Guest/FamilyDetail.aspx");
                    }
                }
                objDal.CloseSQLConnection();
            }
        }
        catch (Exception ex)
        {

            ShowErrors("err", "An error occurred while processing the request.");
        }
    }
    protected void chkSameAsAbove_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkSameAsAbove.Checked == true)
            {
                txtPermanentAddress.Text = txtresidentialAddress.Text;
                txtPermanentDistrict.Text = txtresidentialDistrict.Text;
                txtPermanentMobileNo.Text = txtresidentialMobileNo.Text;
                txtPermanentVillage.Text = txtresidentialVillage.Text;
                txtPermanentPost.Text = txtresidentialPost.Text;
                txtPermanentPinCode.Text = txtresidentialPinCode.Text;
            }
        }
        catch
        {
            // throw;
        }

    }
    public class EducationDetail
    {
        public string EducationLevel { get; set; }
        public string BoardUniversityName { get; set; }
        public string Percentage { get; set; }
        public string PassingYear { get; set; }
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
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

    }

    protected void txtDOB_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string dateString = txtDOB.Text;


            DateTime date = DateTime.ParseExact(txtDOB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

            DateTime birthDate = Convert.ToDateTime(date);
            DateTime currentDate = DateTime.Now;
            int age = currentDate.Year - birthDate.Year;


            txtNomineeAge.Text = age.ToString();
        }
        catch
        {
        }

    }

}