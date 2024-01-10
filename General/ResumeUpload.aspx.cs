using System;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;

public partial class General_ResumeUpload : System.Web.UI.Page
{
    private ResumeUploadBLL _ResumeUploadBLL = new ResumeUploadBLL();


    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                //if (Request.QueryString["ChapterVedioId"] == null)
                //{
                //    _ResumeUploadBLL = new ResumeUploadBLL();
                //}
                //else
                //{
                //    ResumeUploadBLL = new _ResumeUploadBLL(Request.QueryString["ChapterVedioId"].ToString());
                //}
                Session["_ChapterVedioBLL"] = _ResumeUploadBLL;
            }
            else
            {
                _ResumeUploadBLL = (ResumeUploadBLL)Session["_ChapterVedioBLL"];
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                ////if (Request.QueryString["ChapterVedioId"] != null)
                ////{
                ////    lblTitle.Text = " - [Edit Mode]";
                ////    LoadWebForm();
                ////    btnDelete.Enabled = true;
                ////    btnOK.Visible = true;
                ////}
                ////else
                ////{
                //    if (ddlStandard.SelectedIndex > 0 && ddlSubs.SelectedIndex > 0)
                //    {
                //        txtSrNo.Text = _ChapterVedioBLL.SrNo;
                //    }

                //}
            }
            else
            {
                HideErrors();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        try
        {

            //if (ResumeUpload != null && ResumeUpload.FileName != "")
            //{
            //    UploadFileName = ResumeUpload.FileName;
            //    int lastSlash = UploadFileName.LastIndexOf("\\");
            //    string trailingPath = UploadFileName.Substring(lastSlash + 1);
            //    fullPath2 = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath;
            //    ResumeUpload.SaveAs(fullPath2);
            //}

            string UploadFileName = "";
            string fullPath2 = "";

            if (fuResume.PostedFile != null && fuResume.PostedFile.FileName != "")
            {
                UploadFileName = fuResume.PostedFile.FileName;
                int lastSlash = UploadFileName.LastIndexOf("\\");
                string trailingPath = UploadFileName.Substring(lastSlash + 1);
                fullPath2 = ConfigurationSettings.AppSettings["FolderPath"] + trailingPath;
                fuResume.SaveAs(fullPath2);
                _ResumeUploadBLL.UplaodResume = fullPath2;
                _ResumeUploadBLL.ResumeName = UploadFileName;
                _ResumeUploadBLL.Save();
                ShowErrors("Success", "Resume Uploaded.");
            }
            else
            {
                ShowErrors("Error", "Please select both a document and a photo.");
                //lblMessage.Text = "Please select both a document and a photo";
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error: " + ex.Message;
        }
    }
    private void ShowErrors(string key, string value)
    {
        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();
    }
}