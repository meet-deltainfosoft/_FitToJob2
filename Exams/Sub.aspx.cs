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

public partial class Exams_Sub : System.Web.UI.Page
{
    private SubBLL _SubBLL;

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["SubId"] == null)
            {
                _SubBLL = new SubBLL();
            }
            else
            {
                _SubBLL = new SubBLL(Request.QueryString["SubId"].ToString());
            }
            Session["_SubBLL"] = _SubBLL;
        }
        else
        {
            _SubBLL = (SubBLL)Session["_SubBLL"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadStandard();

            if (Request.QueryString["SubId"] != null)
            {
                lblTitle.Text = " - [Edit Mode]";
                LoadWebForm();
                btnDelete.Enabled = true;
            }
            else
            {

            }
            txtName.Focus();
        }
    }
    #endregion

    #region "Subs Functions"

    private bool Validate()
    {
        HideErrors();

        SortedList sl = _SubBLL.Validate();

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
        if (key == "Names")
        {
            lblName.CssClass = "error";
            txtName.CssClass = "error";
        }
        //Name

        if (key == "StandardTextListId")
        {
            lblStandardTextListId.CssClass = "error";
            ddlStandardTextListId.CssClass = "error";
        }
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        lblName.CssClass = "";
        txtName.CssClass = "";

        lblStandardTextListId.CssClass = "";
        ddlStandardTextListId.CssClass = "";
    }

    private void Reset()
    {
        txtRemark.Text = "";
        txtName.Text = "";
        ddlStandardTextListId.SelectedIndex = 0;
        chkIsStudyMaterialAllowed.Checked = false;
    }
    private void LoadWebForm()
    {
        if (_SubBLL.Name != null)
            txtName.Text = _SubBLL.Name;

        if (_SubBLL.Remarks != null)
            txtRemark.Text = _SubBLL.Remarks;

        if (_SubBLL.StandardTextListId != null)
            ddlStandardTextListId.SelectedValue = _SubBLL.StandardTextListId;

        if (_SubBLL.IsStudyMaterialAllowed != null)
            chkIsStudyMaterialAllowed.Checked = Convert.ToBoolean(_SubBLL.IsStudyMaterialAllowed);

        if (_SubBLL.ImagePhoto != null)
        {
            imgPhoto.ImageUrl = _SubBLL.ImagePhoto;
        }
    }

    #endregion

    #region "Subs Events"
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtName.Text.Trim().Length > 0)
                _SubBLL.Name = txtName.Text.Trim();
            else
                _SubBLL.Name = null;

            if (txtRemark.Text.Trim().Length > 0)
                _SubBLL.Remarks = txtRemark.Text.Trim();
            else
                _SubBLL.Remarks = null;

            if (ddlStandardTextListId.SelectedIndex > 0)
                _SubBLL.StandardTextListId = ddlStandardTextListId.SelectedValue.ToString();
            else
                _SubBLL.StandardTextListId = null;

            _SubBLL.IsStudyMaterialAllowed = chkIsStudyMaterialAllowed.Checked;

            if (fileUploadPhoto.PostedFile != null && fileUploadPhoto.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadPhoto.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadPhoto.PostedFile;

                string[] getExtenstion = fileUploadPhoto.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadPhoto.PostedFile.ContentLength);

                _SubBLL.ImagePhoto= ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_Sub" + "." + oExtension;
            }

            bool isValid = Validate();

            if (isValid == true)
            {
                _SubBLL.Save();

                if (fileUploadPhoto.PostedFile != null && fileUploadPhoto.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadPhoto.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadPhoto.PostedFile;
                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_SubBLL.ImagePhoto);
                }

                if (Request.QueryString["SubId"] == null)
                {
                    Reset();
                    Session["_SubBLL"] = null;
                    Session["_SubBLL"] = new SubBLL();
                    _SubBLL = (SubBLL)Session["_SubBLL"];
                }
                else
                {
                    Session["_SubBLL"] = null;
                    Response.Redirect("Subs.aspx");
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
        Session["_SubBLL"] = null;

        if (Request.QueryString["SubId"] == null)
        {
            Response.Redirect("~/General/Default.aspx");
        }
        else
        {
            Response.Redirect("Subs.aspx");
        }

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _SubBLL.Delete(Request.QueryString["SubId"]);
            Session["_SubBLL"] = null;
            Response.Redirect("Subs.aspx");
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

            ddlStandardTextListId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlStandardTextListId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _SubBLL.LoadStandard();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlStandardTextListId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
}
