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
using System.Web.UI.HtmlControls;
using System.Net;

public partial class Guest_DocumentUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HideErrors();
            //if (string.IsNullOrEmpty((string)Session["MobileNo"]) || string.IsNullOrEmpty((string)Session["Language"]))
            //{
            //    Response.Redirect("~/Guests/SelectLanguage.aspx");
            //}
            //else
            //{

            if (Session["MobileNo"] != null && !string.IsNullOrEmpty(Session["MobileNo"].ToString()))
            {
                string MobileNo = Session["MobileNo"].ToString();

                CheckUser(MobileNo);
                CheckUserDocuments(MobileNo);
                DataSet dt = CheckUserDocuments(MobileNo);
                DataSet dtset = new DataSet();

                if (dt.Tables[0].Rows.Count > 0)
                {

                    gdvIdentification.DataSource = dt.Tables[0];
                    gdvIdentification.DataBind();
                }
                if (dt.Tables[1].Rows.Count > 0)
                {
                    gdvPhotograph.DataSource = dt.Tables[1];
                    gdvPhotograph.DataBind();
                }
                if (dt.Tables[2].Rows.Count > 0)
                {
                    gdvEducation.DataSource = dt.Tables[2];
                    gdvEducation.DataBind();
                }

                if (dt.Tables[3].Rows.Count > 0)
                {
                    gdvWorkExperience.DataSource = dt.Tables[3];
                    gdvWorkExperience.DataBind();
                }



                // ShowErrors("error", "Your Profile is reviwed for change please contact administrator.");
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

    protected void lnkBtnSubmit_click(object sender, EventArgs e)
    {
        try
        {
            DataTable CandidatePhotoPath = new DataTable();
            CandidatePhotoPath.Columns.Add("CandidateId", typeof(string));
            CandidatePhotoPath.Columns.Add("PhotoPath", typeof(string));

            foreach (GridViewRow row in gdvPhotograph.Rows)
            {
                FileUpload fuPhotograph = (FileUpload)row.FindControl("fuPhotograph");
                HiddenField hfCandidateId = (HiddenField)row.FindControl("hfCandidateId");
                Label hfPhotographPath = (Label)row.FindControl("hfPhotographPath");

                DataRow dataRow = CandidatePhotoPath.NewRow();

                string PhotographPath = "";

                if (fuPhotograph.HasFile)
                {


                    string uniqueFileName = Guid.NewGuid().ToString();
                    string fileExtension = Path.GetExtension(fuPhotograph.FileName);
                    string newFileName = uniqueFileName + fileExtension;
                    string uploadFolder = Server.MapPath("~/images/");
                    string fullPath = Path.Combine(uploadFolder, newFileName);
                    PhotographPath = "~/images/" + newFileName;
                    dataRow["PhotoPath"] = PhotographPath;
                    fuPhotograph.SaveAs(fullPath);

                }
                if (PhotographPath == "")
                {
                    dataRow["PhotoPath"] = hfPhotographPath.Text.Trim();
                }
                dataRow["CandidateId"] = hfCandidateId.Value;
                CandidatePhotoPath.Rows.Add(dataRow);
                PhotographPath = "";
            }


            DataTable Identification = new DataTable();
            Identification.Columns.Add("EducationId", typeof(string));
            Identification.Columns.Add("Label", typeof(string));
            Identification.Columns.Add("DocumentPath", typeof(string));

            foreach (GridViewRow row in gdvIdentification.Rows)
            {
                FileUpload fuEducationLevel = (FileUpload)row.FindControl("fuIdentification");
                HtmlGenericControl spanControl = (HtmlGenericControl)row.FindControl("CardName");
                HiddenField hfEducationId = (HiddenField)row.FindControl("hfRegistrationId");
                Label hfIdentificationPath = (Label)row.FindControl("hfIdentificationPath");

                string IdentificationPath = "";

                DataRow dataRow = Identification.NewRow();
                if (fuEducationLevel.HasFile)
                {

                    string uniqueFileName = Guid.NewGuid().ToString();
                    string fileExtension = Path.GetExtension(fuEducationLevel.FileName);
                    string newFileName = uniqueFileName + fileExtension;
                    string uploadFolder = Server.MapPath("~/images/");
                    string fullPath = Path.Combine(uploadFolder, newFileName);
                    IdentificationPath = "~/images/" + newFileName;
                    dataRow["DocumentPath"] = IdentificationPath;
                    fuEducationLevel.SaveAs(fullPath);
                    dataRow["Label"] = spanControl.InnerText.Trim();
                    dataRow["EducationId"] = hfEducationId.Value;

                }
                if (IdentificationPath == "")
                {
                    dataRow["DocumentPath"] = hfIdentificationPath.Text.Trim();
                }
                Identification.Rows.Add(dataRow);
                IdentificationPath = "";
            }

            DataTable Educationlavel = new DataTable();
            Educationlavel.Columns.Add("EducationId", typeof(string));
            Educationlavel.Columns.Add("Label", typeof(string));
            Educationlavel.Columns.Add("DocumentPath", typeof(string));

            foreach (GridViewRow row in gdvEducation.Rows)
            {
                FileUpload fuEducationLevel = (FileUpload)row.FindControl("fuEducationLevel");
                HtmlGenericControl spanControl = (HtmlGenericControl)row.FindControl("EducationLevel");
                HiddenField hfEducationId = (HiddenField)row.FindControl("hfEducationId");
                Label hfEducationPath = (Label)row.FindControl("hfEducationPath");

                DataRow dataRow = Educationlavel.NewRow();
                string EducationPath = "";

                if (fuEducationLevel.HasFile)
                {



                    string uniqueFileName = Guid.NewGuid().ToString();
                    string fileExtension = Path.GetExtension(fuEducationLevel.FileName);
                    string newFileName = uniqueFileName + fileExtension;
                    string uploadFolder = Server.MapPath("~/images/");
                    string fullPath = Path.Combine(uploadFolder, newFileName);
                    EducationPath = "~/images/" + newFileName;
                    dataRow["DocumentPath"] = EducationPath;
                    fuEducationLevel.SaveAs(fullPath);
                    dataRow["Label"] = spanControl.InnerText.Trim();
                    dataRow["EducationId"] = hfEducationId.Value;

                }
                if (EducationPath == "")
                {
                    dataRow["DocumentPath"] = hfEducationPath.Text.Trim();
                }
                Educationlavel.Rows.Add(dataRow);
                EducationPath = "";
            }

            DataTable WorkExperience = new DataTable();
            WorkExperience.Columns.Add("RegistrationId", typeof(string));
            WorkExperience.Columns.Add("CompanyName", typeof(string));
            WorkExperience.Columns.Add("DocumentPath", typeof(string));


            DataTable LastCompanyDetail = new DataTable();
            LastCompanyDetail.Columns.Add("RegistrationId", typeof(string));
            LastCompanyDetail.Columns.Add("DocumentPath", typeof(string));

            foreach (GridViewRow row in gdvWorkExperience.Rows)
            {
                FileUpload fuWorkExperience = (FileUpload)row.FindControl("fuWorkExperience");
                HtmlGenericControl spanControl = (HtmlGenericControl)row.FindControl("Label");
                HiddenField hfRegistrationId = (HiddenField)row.FindControl("hfRegistrationId");
                Label hfDocumentPath = (Label)row.FindControl("hfDocumentPath");

                if (spanControl.InnerText.Trim() != "Last Salary Slip" && spanControl.InnerText.Trim() != "Last Appointment Letter Path")
                {
                    string DocumentPath = "";
                    DataRow dataRow = WorkExperience.NewRow();

                    if (fuWorkExperience.HasFile)
                    {



                        string uniqueFileName = Guid.NewGuid().ToString();
                        string fileExtension = Path.GetExtension(fuWorkExperience.FileName);
                        string newFileName = uniqueFileName + fileExtension;
                        string uploadFolder = Server.MapPath("~/images/");
                        string fullPath = Path.Combine(uploadFolder, newFileName);
                        DocumentPath = "~/images/" + newFileName;
                        dataRow["DocumentPath"] = DocumentPath;
                        fuWorkExperience.SaveAs(fullPath);

                        dataRow["CompanyName"] = spanControl.InnerText.Trim();
                        dataRow["RegistrationId"] = hfRegistrationId.Value;

                    }
                    if (DocumentPath == "")
                    {
                        dataRow["DocumentPath"] = hfDocumentPath.Text.Trim();
                    }
                    WorkExperience.Rows.Add(dataRow);
                    DocumentPath = "";
                }
                else
                {
                    string DocumentPath = "";
                    DataRow dataRow = LastCompanyDetail.NewRow();
                    if (fuWorkExperience.HasFile)
                    {
                        string uniqueFileName = Guid.NewGuid().ToString();
                        string fileExtension = Path.GetExtension(fuWorkExperience.FileName);
                        string newFileName = uniqueFileName + fileExtension;
                        string uploadFolder = Server.MapPath("~/images/");
                        string fullPath = Path.Combine(uploadFolder, newFileName);
                        DocumentPath = "~/images/" + newFileName;
                        dataRow["DocumentPath"] = DocumentPath;
                        fuWorkExperience.SaveAs(fullPath);
                        dataRow["RegistrationId"] = hfRegistrationId.Value;

                    }
                    if (DocumentPath == "")
                    {
                        dataRow["DocumentPath"] = hfDocumentPath.Text.Trim();
                    }
                    LastCompanyDetail.Rows.Add(dataRow);
                    DocumentPath = "";
                }
            }

            string LastSalarySlipPath = "";
            string LastAppointmentLetterPath = "";
            string PhotoPath = "";
            string CandidateId = "";
            string UserId = "";

            if (LastCompanyDetail.Rows.Count > 0)
            {
                for (int i = 0; i < LastCompanyDetail.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        LastSalarySlipPath = LastCompanyDetail.Rows[i]["DocumentPath"].ToString();
                    }
                    else if (i == 1)
                    {
                        LastAppointmentLetterPath = LastCompanyDetail.Rows[i]["DocumentPath"].ToString();
                    }
                }
            }

            if (CandidatePhotoPath.Rows.Count > 0)
            {
                for (int i = 0; i < CandidatePhotoPath.Rows.Count; i++)
                {
                    PhotoPath = CandidatePhotoPath.Rows[i]["PhotoPath"].ToString();
                    CandidateId = CandidatePhotoPath.Rows[i]["CandidateId"].ToString();
                }
            }


            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "Update_CandidateDocuments";
            sqlCmd.Parameters.AddWithValue("@Action", "UploadDocumentation");
            sqlCmd.Parameters.AddWithValue("@CandidateId", CandidateId);
            sqlCmd.Parameters.AddWithValue("@PhotoPath", PhotoPath);
            sqlCmd.Parameters.AddWithValue("@LastSalarySlipPath", LastSalarySlipPath);
            sqlCmd.Parameters.AddWithValue("@LastAppointmentLetterPath", LastAppointmentLetterPath);


            SqlParameter tvpEducationsDetailPaths = new SqlParameter("@UT_EducationsDetailPaths", SqlDbType.Structured);
            tvpEducationsDetailPaths.Value = Educationlavel;
            tvpEducationsDetailPaths.TypeName = "dbo.UT_EducationsDetailPaths";
            sqlCmd.Parameters.Add(tvpEducationsDetailPaths);

            SqlParameter tvpExperienceDetailPaths = new SqlParameter("@UT_ExperienceDetailPaths", SqlDbType.Structured);
            tvpExperienceDetailPaths.Value = WorkExperience;
            tvpExperienceDetailPaths.TypeName = "dbo.UT_ExperienceDetailPaths";
            sqlCmd.Parameters.Add(tvpExperienceDetailPaths);

            SqlParameter tvpIdentification = new SqlParameter("@UT_Identification", SqlDbType.Structured);
            tvpIdentification.Value = Identification;
            tvpIdentification.TypeName = "dbo.UT_Identification";
            sqlCmd.Parameters.Add(tvpIdentification);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                if (dataSet.Tables[0].Rows[0]["IsSuccessful"].ToString() == "1")
                {
                    string message = dataSet.Tables[0].Rows[0]["Message"].ToString();
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(message);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                }
            }
            objDal.CloseSQLConnection();
        }
        catch (Exception ex)
        {
        }
    }

    private int CheckUser(string MobileNo)
    {
        int count = 0;
        try
        {
            //SqlCommand sqlCmd = new SqlCommand();
            //GeneralDAL objDal = new GeneralDAL();
            //objDal.OpenSQLConnection();
            //sqlCmd.Connection = objDal.ActiveSQLConnection();
            //sqlCmd.CommandType = CommandType.Text;

            //sqlCmd.CommandText = "select Count(b.CandidateId) from CandidateDocumentations a " +
            //                     " inner join Registrations b on a.CandidateId = a.CandidateId " +
            //                     " where b.MobileNo = @MobileNumber";
            //sqlCmd.Parameters.AddWithValue("@MobileNumber", MobileNo);
            //SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            //DataSet dataSet = new DataSet();
            //dataAdapter.Fill(dataSet);
            //count = (dataSet.Tables[0].Rows.Count > 0) ? 1 : 0;
        }
        catch (Exception)
        {

            count = 0;
        }
        return count;
    }

    private DataSet CheckUserDocuments(string MobileNo)
    {
        DataSet dtset = new DataSet();
        try
        {

            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "Duke_Upload_Documents";
            sqlCmd.Parameters.AddWithValue("@Action", "GetAllDocumentsByCandidateId");
            sqlCmd.Parameters.AddWithValue("@PhoneNumber", MobileNo);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            dtset = dataSet;

        }
        catch (Exception)
        {


        }
        return dtset;
    }

    protected void gdvPhotograph_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FileUpload fuPhotograph = e.Row.FindControl("fuPhotograph") as FileUpload;
            HiddenField hfCandidateId = e.Row.FindControl("hfCandidateId") as HiddenField;
            Label hfPhotographPath = e.Row.FindControl("hfPhotographPath") as Label;
            LinkButton lnkDownload = e.Row.FindControl("lnkDownload") as LinkButton; 
            
            if (hfPhotographPath != null && !string.IsNullOrEmpty(hfPhotographPath.Text))
            {
                string imagePath = hfPhotographPath.Text;
                string fileExtension = Path.GetExtension(imagePath);
                lnkDownload.Visible = true;
            }
            else
            {

                Label lblNoDataFound = e.Row.FindControl("lblNoDataFound") as Label;
                lnkDownload.Visible = false;

                if (lblNoDataFound != null)
                {
                    lblNoDataFound.Font.Size = FontUnit.Point(10);
                    lblNoDataFound.ForeColor = System.Drawing.Color.Red;
                    lblNoDataFound.Visible = true;
                    lblNoDataFound.Text = "File not Uploaded";
                }
            }

        }
    }

    protected void gdvEducation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FileUpload fuEducationLevel = e.Row.FindControl("fuEducationLevel") as FileUpload;
            HiddenField hfEducationId = e.Row.FindControl("hfEducationId") as HiddenField;
            Label hfEducationPath = e.Row.FindControl("hfEducationPath") as Label;
            LinkButton lnkDownload = e.Row.FindControl("lnkDownload") as LinkButton; 

            if (hfEducationPath != null && !string.IsNullOrEmpty(hfEducationPath.Text))
            {
                string imagePath = hfEducationPath.Text;
                string fileExtension = Path.GetExtension(imagePath);
                lnkDownload.Visible = true;
            }
            else
            {
                
                Label lblNoDataFound = e.Row.FindControl("lblNoDataFound") as Label;
                lnkDownload.Visible = false;

                if (lblNoDataFound != null)
                {
                    lblNoDataFound.Font.Size = FontUnit.Point(10);
                    lblNoDataFound.ForeColor = System.Drawing.Color.Red;
                    lblNoDataFound.Visible = true;
                    lblNoDataFound.Text = "File not Uploaded";
                }
            }
        }
    }

    

    protected void gdvIdentification_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FileUpload fuIdentification = e.Row.FindControl("fuIdentification") as FileUpload;
            HiddenField hfRegistrationId = e.Row.FindControl("hfRegistrationId") as HiddenField;
            Label hfIdentificationPath = e.Row.FindControl("hfIdentificationPath") as Label;
            LinkButton lnkDownload = e.Row.FindControl("lnkDownload") as LinkButton;

            if (hfIdentificationPath != null && !string.IsNullOrEmpty(hfIdentificationPath.Text))
            {
                string imagePath = hfIdentificationPath.Text;
                string fileExtension = Path.GetExtension(imagePath);
                lnkDownload.Visible = true;
            }
            else
            {

                Label lblNoDataFound = e.Row.FindControl("lblNoDataFound") as Label;
                lnkDownload.Visible = false;

                if (lblNoDataFound != null)
                {
                    lblNoDataFound.Font.Size = FontUnit.Point(10);
                    lblNoDataFound.ForeColor = System.Drawing.Color.Red;
                    lblNoDataFound.Visible = true;
                    lblNoDataFound.Text = "File not Uploaded";
                }
            }
        }
    }

    protected void gdvWorkExperience_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            FileUpload fuWorkExperience = e.Row.FindControl("fuWorkExperience") as FileUpload;
            HiddenField hfRegistrationId = e.Row.FindControl("hfRegistrationId") as HiddenField;
            Label hfDocumentPath = e.Row.FindControl("hfDocumentPath") as Label;
            LinkButton lnkDownload = e.Row.FindControl("lnkDownload") as LinkButton;

            if (hfDocumentPath != null && !string.IsNullOrEmpty(hfDocumentPath.Text))
            {
                string imagePath = hfDocumentPath.Text;
                string fileExtension = Path.GetExtension(imagePath);
                lnkDownload.Visible = true;
            }
            else
            {

                Label lblNoDataFound = e.Row.FindControl("lblNoDataFound") as Label;
                lnkDownload.Visible = false;

                if (lblNoDataFound != null)
                {
                    lblNoDataFound.Font.Size = FontUnit.Point(10);
                    lblNoDataFound.ForeColor = System.Drawing.Color.Red;
                    lblNoDataFound.Visible = true;
                    lblNoDataFound.Text = "File not Uploaded";
                }
            }
        }
    }


    protected void gdvPhotograph_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            string Path = e.CommandArgument.ToString();
            if (Path != string.Empty)
            {
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                string filePath = Path;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.ContentType = "image/jpeg";
                response.AddHeader("Content-Disposition", "attachment;filename=image.jpg");
                byte[] data = req.DownloadData(Server.MapPath(filePath));
                response.BinaryWrite(data);
                response.End();
            }
        }
    }
   

    protected void gdvIdentification_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            string filePath = e.CommandArgument.ToString();

            if (!string.IsNullOrEmpty(filePath))
            {
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;

                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;

                // Determine the file extension
                string fileExtension = Path.GetExtension(filePath);
                string contentType = "";

                switch (fileExtension.ToLower())
                {
                    case ".pdf":
                        contentType = "application/pdf";
                        break;
                    case ".doc":
                        contentType = "application/msword";
                        break;
                    case ".docx":
                        contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        break;
                    case ".jpeg":
                        contentType = "attachment;filename=image.jpeg";
                        break;
                    case ".jpg":
                        contentType = "attachment;filename=image.jpg";
                        break;
                    case ".png":
                        contentType = "attachment;filename=image.png";
                        break;
                    default:
                        // Add more cases as needed for other file types
                        break;
                }

                if (!string.IsNullOrEmpty(contentType))
                {
                    response.ContentType = contentType;
                    string fileName = Path.GetFileName(filePath);
                    response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

                    byte[] data = req.DownloadData(Server.MapPath(filePath));
                    response.BinaryWrite(data);
                    response.End();
                }
                else
                {
                    // Handle unsupported file types
                    // You can add custom logic or display a message to the user
                }
            }
        }
    }
    
    protected void gdvEducation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            string filePath = e.CommandArgument.ToString();

            if (!string.IsNullOrEmpty(filePath))
            {
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;

                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;

                // Determine the file extension
                string fileExtension = Path.GetExtension(filePath);
                string contentType = "";

                switch (fileExtension.ToLower())
                {
                    case ".pdf":
                        contentType = "application/pdf";
                        break;
                    case ".doc":
                        contentType = "application/msword";
                        break;
                    case ".docx":
                        contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        break;
                    case ".jpeg":
                        contentType = "attachment;filename=image.jpeg";
                        break;
                    case ".jpg":
                        contentType = "attachment;filename=image.jpg";
                        break;
                    case ".png":
                        contentType = "attachment;filename=image.png";
                        break;
                    default:
                        // Add more cases as needed for other file types
                        break;
                }

                if (!string.IsNullOrEmpty(contentType))
                {
                    response.ContentType = contentType;
                    string fileName = Path.GetFileName(filePath);
                    response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

                    byte[] data = req.DownloadData(Server.MapPath(filePath));
                    response.BinaryWrite(data);
                    response.End();
                }
                else
                {
                    // Handle unsupported file types
                    // You can add custom logic or display a message to the user
                }
            }
        }
    }
    protected void gdvWorkExperience_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            string filePath = e.CommandArgument.ToString();

            if (!string.IsNullOrEmpty(filePath))
            {
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;

                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;

                // Determine the file extension
                string fileExtension = Path.GetExtension(filePath);
                string contentType = "";

                switch (fileExtension.ToLower())
                {
                    case ".pdf":
                        contentType = "application/pdf";
                        break;
                    case ".doc":
                        contentType = "application/msword";
                        break;
                    case ".docx":
                        contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                        break;
                    case ".jpeg":
                        contentType = "attachment;filename=image.jpeg";
                        break;
                    case ".jpg":
                        contentType = "attachment;filename=image.jpg";
                        break;
                    case ".png":
                        contentType = "attachment;filename=image.png";
                        break;
                    default:
                        // Add more cases as needed for other file types
                        break;
                }

                if (!string.IsNullOrEmpty(contentType))
                {
                    response.ContentType = contentType;
                    string fileName = Path.GetFileName(filePath);
                    response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

                    byte[] data = req.DownloadData(Server.MapPath(filePath));
                    response.BinaryWrite(data);
                    response.End();
                }
                else
                {
                    // Handle unsupported file types
                    // You can add custom logic or display a message to the user
                }
            }
        }
    }


   
}