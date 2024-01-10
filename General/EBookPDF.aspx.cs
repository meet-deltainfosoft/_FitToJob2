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

public partial class General_EBookPDF : System.Web.UI.Page
{
    EBookPDFBLL _EBookPDFBLL = new EBookPDFBLL();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["EBookPDFId"] == null)
                {
                    _EBookPDFBLL = new EBookPDFBLL();
                }
                else
                {
                    _EBookPDFBLL = new EBookPDFBLL(Request.QueryString["EBookPDFId"].ToString());
                }
                Session["_EBookPDFBLL"] = _EBookPDFBLL;
            }
            else
            {
                _EBookPDFBLL = (EBookPDFBLL)Session["_EBookPDFBLL"];
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
                LoadStandard();
                if (Request.QueryString["EBookPDFId"] != null)
                {
                    lblTitle.Text = " - [Edit Mode]";
                    LoadWebForm();
                    btnDelete.Enabled = true;
                    btnOK.Visible = true;
                }
                else
                {


                }
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
    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        lblStandard.CssClass = "";
        ddlStandard.CssClass = "";

        lblSubs.CssClass = "";
        ddlSubs.CssClass = "";


        lblFileUpload.CssClass = "";
        fuFileUpload.CssClass = "";
    }

    private void ShowErrors(string key, string value)
    {
        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));

        if (key == "StandardTextListId")
        {
            lblStandard.CssClass = "error";
            ddlStandard.CssClass = "error";
        }

        if (key == "SubId")
        {
            lblSubs.CssClass = "error";
            ddlSubs.CssClass = "error";
        }

        if (key == "UploadphotoPath")
        {
            lblFileUpload.CssClass = "error";
            fuFileUpload.CssClass = "error";
        }

    }
    private void LoadStandard()
    {
        try
        {
            ListItem li = new ListItem();

            ddlStandard.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlStandard.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _EBookPDFBLL.LoadStandard();

            foreach (DataRow dtr in dt.Rows)
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
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadSubjects()
    {
        try
        {
            ListItem li = new ListItem();

            ddlSubs.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlSubs.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _EBookPDFBLL.LoadSubjects();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSubs.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
                _EBookPDFBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _EBookPDFBLL.StandardTextListId = null;

            if (ddlSubs.SelectedIndex > 0)
                _EBookPDFBLL.SubId = ddlSubs.SelectedValue;
            else
                _EBookPDFBLL.SubId = null;

            if (txtRemarks.Text.Trim().Length > 0)
                _EBookPDFBLL.Remarks = txtRemarks.Text.Trim();
            else
                _EBookPDFBLL.Remarks = null;

            if (fuFileUpload.PostedFile != null && fuFileUpload.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuFileUpload.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuFileUpload.PostedFile;
                uploadedImage.InputStream.Read(imageSize, 0, (int)fuFileUpload.PostedFile.ContentLength);
                string[] getExtenstion = uploadedImage.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = uploadedImage.FileName.Replace("." + oExtension, "");

                string msExcelFilePathOnServer = ConfigurationSettings.AppSettings["FolderPath"] + "" + Convert.ToDateTime(System.DateTime.Now).ToString("ddMMMyyyy") + "/";

                bool exists = System.IO.Directory.Exists(msExcelFilePathOnServer);

                if (!exists)
                    System.IO.Directory.CreateDirectory(msExcelFilePathOnServer);

                _EBookPDFBLL.UploadphotoPath = msExcelFilePathOnServer + FileNameForInsert + "-" + System.Guid.NewGuid().ToString() + "." + oExtension;

                _EBookPDFBLL.FileName = fuFileUpload.PostedFile.FileName;
            }

            bool isValid = Validate();

            if (isValid == true)
            {
                _EBookPDFBLL.Save();

                if (fuFileUpload.PostedFile != null)
                {
                    try
                    {
                        HttpPostedFile uploadedImage = fuFileUpload.PostedFile;

                        uploadedImage.SaveAs(_EBookPDFBLL.UploadphotoPath);

                        //imgFileUpload.ImageUrl = _EBookPDFBLL.UploadphotoPath;
                    }
                    catch
                    {
                    }
                }

                if (Request.QueryString["EBookPDFId"] == null)
                {
                    Reset(false);
                    Session["_EBookPDFBLL"] = null;
                    Session["_EBookPDFBLL"] = new EBookPDFBLL();
                    _EBookPDFBLL = (EBookPDFBLL)Session["_EBookPDFBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_EBookPDFBLL"] = null;
                    Response.Redirect("EBookPDFs.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Session["_EBookPDFBLL"] = null;

            if (Request.QueryString["EBookPDFId"] == null)
            {
                Response.Redirect("~/General/Default.aspx");
            }
            else
            {
                Response.Redirect("EBookPDFs.aspx");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _EBookPDFBLL.Delete(Request.QueryString["EBookPDFId"]);
            Session["_EBookPDFBLL"] = null;
            Response.Redirect("EBookPDFs.aspx");
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

            SortedList sl = _EBookPDFBLL.Validate();

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
            return false;
            ShowErrors("", ex.Message.ToString());
        }
    }

    private void Reset(bool FromOkAndAddClick)
    {
        try
        {
            if (FromOkAndAddClick == true)
            {
                txtRemarks.Text = "";

                _EBookPDFBLL.StandardTextListId = ddlStandard.SelectedValue;
                ddlStandard_SelectedIndexChanged(ddlStandard, null);

                ddlSubs.SelectedValue = _EBookPDFBLL.SubId;
                _EBookPDFBLL.SubId = ddlSubs.SelectedValue;
                ddlSubs_SelectedIndexChanged(ddlSubs, null);
            }
            else
            {
                ddlStandard.SelectedIndex = 0;
                ddlSubs.SelectedIndex = 0;

                txtRemarks.Text = "";
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }

    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
            {
                _EBookPDFBLL.StandardTextListId = ddlStandard.SelectedValue.ToString();
                LoadSubjects();
            }
            else
            {
                _EBookPDFBLL.StandardTextListId = null;
                //ShowErrors("", "Please select standard to generate schedule");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadWebForm()
    {
        try
        {
            if (_EBookPDFBLL.StandardTextListId != null)
            {
                ddlStandard.SelectedValue = _EBookPDFBLL.StandardTextListId.ToString();
                LoadSubjects();
                //ddlStandard_SelectedIndexChanged(null, null);
                //ddlStandard.Enabled = false;
            }
            if (_EBookPDFBLL.SubId != null)
            {
                ddlSubs.SelectedValue = _EBookPDFBLL.SubId.ToString();
                //ddlSubs_SelectedIndexChanged(null, null);
            }

            if (_EBookPDFBLL.Remarks != null)
                txtRemarks.Text = _EBookPDFBLL.Remarks.ToString();

            if (_EBookPDFBLL.UploadphotoPath != null)
            {
                //imgFileUpload.ImageUrl = _EBookPDFBLL.UploadphotoPath;
                //imgFileUpload.Visible = true;
                iframepdf.Attributes.Add("src", _EBookPDFBLL.UploadphotoPath);
            }

        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void ddlSubs_SelectedIndexChanged(object sender, EventArgs e)
    {

        try
        {
            if (ddlSubs.SelectedIndex > 0)
            {
                _EBookPDFBLL.SubId = ddlSubs.SelectedValue.ToString();
            }
            else
            {
                _EBookPDFBLL.SubId = null;
                //ShowErrors("", "Please select standard to generate schedule");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void btnOKAndAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
                _EBookPDFBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _EBookPDFBLL.StandardTextListId = null;

            if (ddlSubs.SelectedIndex > 0)
                _EBookPDFBLL.SubId = ddlSubs.SelectedValue;
            else
                _EBookPDFBLL.SubId = null;

            if (txtRemarks.Text.Trim().Length > 0)
                _EBookPDFBLL.Remarks = txtRemarks.Text.Trim();
            else
                _EBookPDFBLL.Remarks = null;

            if (fuFileUpload.PostedFile != null && fuFileUpload.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuFileUpload.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuFileUpload.PostedFile;
                uploadedImage.InputStream.Read(imageSize, 0, (int)fuFileUpload.PostedFile.ContentLength);
                string[] getExtenstion = uploadedImage.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = uploadedImage.FileName.Replace("." + oExtension, "");

                _EBookPDFBLL.UploadphotoPath = ConfigurationSettings.AppSettings["FolderPath"] + FileNameForInsert + "-" + System.DateTime.Now.Millisecond.ToString() + "." + oExtension;

                _EBookPDFBLL.FileName = fuFileUpload.PostedFile.FileName;
            }

            bool isValid = Validate();

            if (isValid == true)
            {
                _EBookPDFBLL.Save();

                if (fuFileUpload.PostedFile != null)
                {
                    HttpPostedFile uploadedImage = fuFileUpload.PostedFile;

                    uploadedImage.SaveAs(_EBookPDFBLL.UploadphotoPath);

                    //imgFileUpload.ImageUrl = _EBookPDFBLL.UploadphotoPath;
                }

                if (Request.QueryString["EBookPDFId"] == null)
                {
                    Reset(true);
                    Session["_EBookPDFBLL"] = null;
                    Session["_EBookPDFBLL"] = new EBookPDFBLL();
                    _EBookPDFBLL = (EBookPDFBLL)Session["_EBookPDFBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_EBookPDFBLL"] = null;
                    Response.Redirect("EBookPDFs.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }

}