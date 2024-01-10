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

public partial class General_ChapterPDF : System.Web.UI.Page
{
    ChapterPDFBLL _ChapterPDFBLL = new ChapterPDFBLL();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["ChapterPDFId"] == null)
                {
                    _ChapterPDFBLL = new ChapterPDFBLL();
                }
                else
                {
                    _ChapterPDFBLL = new ChapterPDFBLL(Request.QueryString["ChapterPDFId"].ToString());
                }
                Session["_ChapterPDFBLL"] = _ChapterPDFBLL;
            }
            else
            {
                _ChapterPDFBLL = (ChapterPDFBLL)Session["_ChapterPDFBLL"];
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
                if (Request.QueryString["ChapterPDFId"] != null)
                {
                    lblTitle.Text = " - [Edit Mode]";
                    LoadWebForm();
                    btnDelete.Enabled = true;
                    btnOK.Visible = true;
                }
                else
                {
                    if (ddlStandard.SelectedIndex > 0 && ddlSubs.SelectedIndex > 0)
                    {
                        txtSrNo.Text = _ChapterPDFBLL.SrNo;
                    }

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

        lblChapter.CssClass = "";
        ddlChapter.CssClass = "";

        lblPeriodNo.CssClass = "";
        ddlPeriodNo.CssClass = "";

        lblSrNo.CssClass = "";
        txtSrNo.CssClass = "";

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

        if (key == "ChapterId")
        {
            lblChapter.CssClass = "error";
            ddlChapter.CssClass = "error";
        }


        if (key == "PeriodNo")
        {
            lblPeriodNo.CssClass = "error";
            ddlPeriodNo.CssClass = "error";
        }

        if (key == "SrNo")
        {
            lblSrNo.CssClass = "error";
            txtSrNo.CssClass = "error";
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
            dt = _ChapterPDFBLL.LoadStandard();

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
            dt = _ChapterPDFBLL.LoadSubjects();

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

    private void LoadChapter()
    {
        try
        {
            ListItem li = new ListItem();

            ddlChapter.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlChapter.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterPDFBLL.LoadChapter();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlChapter.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }


    private void LoadPeriodNo()
    {
        try
        {
            ListItem li = new ListItem();

            ddlPeriodNo.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlPeriodNo.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterPDFBLL.LoadPeriodNo();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[0].ToString();
                li.Value = dtr[0].ToString();
                ddlPeriodNo.Items.Add(li);

                li = null;
            }

            if (ddlPeriodNo.Items.Count == 2)
            {
                ddlPeriodNo.SelectedIndex = 1;
                ddlPeriodNo_SelectedIndexChanged(null, null);
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
                _ChapterPDFBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _ChapterPDFBLL.StandardTextListId = null;

            if (ddlSubs.SelectedIndex > 0)
                _ChapterPDFBLL.SubId = ddlSubs.SelectedValue;
            else
                _ChapterPDFBLL.SubId = null;

            if (ddlChapter.SelectedIndex > 0)
                _ChapterPDFBLL.ChapterId = ddlChapter.SelectedValue;
            else
                _ChapterPDFBLL.ChapterId = null;

            if (ddlPeriodNo.SelectedIndex > 0)
                _ChapterPDFBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);
            else
                _ChapterPDFBLL.PeriodNo = null;

            if (txtSrNo.Text.Trim().Length > 0)
                _ChapterPDFBLL.SrNo = txtSrNo.Text.Trim();
            else
                _ChapterPDFBLL.SrNo = null;

            if (txtRemarks.Text.Trim().Length > 0)
                _ChapterPDFBLL.Remarks = txtRemarks.Text.Trim();
            else
                _ChapterPDFBLL.Remarks = null;

            //if (_ChapterPDFBLL.ChapterIdMain == null)
            //    if (hfChapterId.Value.Trim().Length > 0)
            //        _ChapterPDFBLL.ChapterIdMain = hfChapterId.Value.Trim();
            //    else
            //        _ChapterPDFBLL.ChapterIdMain = null;

            if (ddlChapterVideoId.SelectedIndex > 0)
                _ChapterPDFBLL.ChapterVideoId = Convert.ToInt16(ddlChapterVideoId.SelectedValue.ToString());
            else
                _ChapterPDFBLL.ChapterVideoId = null;

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

                _ChapterPDFBLL.UploadphotoPath = msExcelFilePathOnServer + FileNameForInsert + "-" + System.Guid.NewGuid().ToString() + "." + oExtension;

                _ChapterPDFBLL.FileName = fuFileUpload.PostedFile.FileName;
            }

            bool isValid = Validate();

            if (isValid == true)
            {
                _ChapterPDFBLL.Save();

                if (fuFileUpload.PostedFile != null)
                {
                    HttpPostedFile uploadedImage = fuFileUpload.PostedFile;

                    try
                    {
                        uploadedImage.SaveAs(_ChapterPDFBLL.UploadphotoPath);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.ToString().ToUpper().Contains("The SaveAs method is configured to require a rooted path".ToString().ToUpper()))
                        {
                        }
                        else
                        {
                            ShowErrors("err", ex.Message);
                        }
                    }

                    //imgFileUpload.ImageUrl = _ChapterPDFBLL.UploadphotoPath;
                }

                if (Request.QueryString["ChapterPDFId"] == null)
                {
                    Reset(false);
                    Session["_ChapterPDFBLL"] = null;
                    Session["_ChapterPDFBLL"] = new ChapterPDFBLL();
                    _ChapterPDFBLL = (ChapterPDFBLL)Session["_ChapterPDFBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_ChapterPDFBLL"] = null;
                    Response.Redirect("ChapterPDFs.aspx");
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
            Session["_ChapterPDFBLL"] = null;

            if (Request.QueryString["ChapterPDFId"] == null)
            {
                Response.Redirect("~/General/Default.aspx");
            }
            else
            {
                Response.Redirect("ChapterPDFs.aspx");
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
            _ChapterPDFBLL.Delete(Request.QueryString["ChapterPDFId"]);
            Session["_ChapterPDFBLL"] = null;
            Response.Redirect("ChapterPDFs.aspx");
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

            SortedList sl = _ChapterPDFBLL.Validate();

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
                ddlChapter.SelectedIndex = 0;
                txtSrNo.Text = "";
                txtRemarks.Text = "";

                _ChapterPDFBLL.StandardTextListId = ddlStandard.SelectedValue;
                ddlStandard_SelectedIndexChanged(ddlStandard, null);

                ddlSubs.SelectedValue = _ChapterPDFBLL.SubId;
                _ChapterPDFBLL.SubId = ddlSubs.SelectedValue;
                ddlSubs_SelectedIndexChanged(ddlSubs, null);

                ddlChapter.SelectedValue = _ChapterPDFBLL.ChapterId;
                _ChapterPDFBLL.ChapterId = ddlChapter.SelectedValue;
                ddlChapter_SelectedIndexChanged(ddlChapter, null);

                ddlPeriodNo.SelectedValue = Convert.ToDecimal(_ChapterPDFBLL.PeriodNo).ToString("#.##");
                _ChapterPDFBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);

                //hfChapterId.Value = _ChapterPDFBLL.ChapterIdMain;
            }
            else
            {
                ddlStandard.SelectedIndex = 0;
                ddlSubs.SelectedIndex = 0;
                ddlChapter.SelectedIndex = 0;
                ddlPeriodNo.Items.Clear();
                txtSrNo.Text = "";
                txtRemarks.Text = "";
                ddlChapterVideoId.SelectedIndex = 0;
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
                _ChapterPDFBLL.StandardTextListId = ddlStandard.SelectedValue.ToString();
                LoadSubjects();
            }
            else
            {
                _ChapterPDFBLL.StandardTextListId = null;
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
            if (_ChapterPDFBLL.StandardTextListId != null)
            {
                ddlStandard.SelectedValue = _ChapterPDFBLL.StandardTextListId.ToString();
                LoadSubjects();
                //ddlStandard_SelectedIndexChanged(null, null);
                //ddlStandard.Enabled = false;
            }
            if (_ChapterPDFBLL.SubId != null)
            {
                ddlSubs.SelectedValue = _ChapterPDFBLL.SubId.ToString();
                LoadChapter();
                //ddlSubs_SelectedIndexChanged(null, null);
            }
            if (_ChapterPDFBLL.SrNo != null)
            {
                txtSrNo.Text = _ChapterPDFBLL.SrNo;
            }
            if (_ChapterPDFBLL.ChapterId != null)
            {
                ddlChapter.SelectedValue = _ChapterPDFBLL.ChapterId.ToString();
                //LoadChapter();
                ddlChapter_SelectedIndexChanged(null, null);
            }

            if (_ChapterPDFBLL.PeriodNo != null)
            {
                ddlPeriodNo.SelectedValue = Convert.ToDecimal(_ChapterPDFBLL.PeriodNo).ToString("#.##");
                //LoadChapter();
                ddlPeriodNo_SelectedIndexChanged(null, null);
            }


            if (_ChapterPDFBLL.Remarks != null)
                txtRemarks.Text = _ChapterPDFBLL.Remarks.ToString();

            if (_ChapterPDFBLL.UploadphotoPath != null)
            {
                //imgFileUpload.ImageUrl = _ChapterPDFBLL.UploadphotoPath;
                //imgFileUpload.Visible = true;
                iframepdf.Attributes.Add("src", _ChapterPDFBLL.UploadphotoPath);
            }
            if (_ChapterPDFBLL.ChapterVideoId != null)
            {
                ddlChapterVideoId.SelectedValue = _ChapterPDFBLL.ChapterVideoId.ToString();
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
                _ChapterPDFBLL.SubId = ddlSubs.SelectedValue.ToString();
                LoadChapter();

            }
            else
            {
                _ChapterPDFBLL.SubId = null;
                //ShowErrors("", "Please select standard to generate schedule");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlChapter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlChapter.SelectedIndex > 0 && ddlSubs.SelectedIndex > 0)
            {
                _ChapterPDFBLL.ChapterId = ddlChapter.SelectedValue.ToString();
                _ChapterPDFBLL.SubId = ddlSubs.SelectedValue.ToString();

                LoadPeriodNo();
                if (_ChapterPDFBLL.SrNo != null)
                {
                    if (Request.QueryString["ChapterPDFId"] == null)
                        txtSrNo.Text = _ChapterPDFBLL.SrNo;
                }
            }
            else
            {
                ddlPeriodNo.Items.Clear();
                _ChapterPDFBLL.SubId = null;
                // ShowErrors("", "Please select standard to generate schedule");
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
                _ChapterPDFBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _ChapterPDFBLL.StandardTextListId = null;

            if (ddlSubs.SelectedIndex > 0)
                _ChapterPDFBLL.SubId = ddlSubs.SelectedValue;
            else
                _ChapterPDFBLL.SubId = null;

            if (ddlChapter.SelectedIndex > 0)
                _ChapterPDFBLL.ChapterId = ddlChapter.SelectedValue;
            else
                _ChapterPDFBLL.ChapterId = null;

            if (ddlPeriodNo.SelectedIndex > 0)
                _ChapterPDFBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);
            else
                _ChapterPDFBLL.PeriodNo = null;

            if (txtSrNo.Text.Trim().Length > 0)
                _ChapterPDFBLL.SrNo = txtSrNo.Text.Trim();
            else
                _ChapterPDFBLL.SrNo = null;

            if (txtRemarks.Text.Trim().Length > 0)
                _ChapterPDFBLL.Remarks = txtRemarks.Text.Trim();
            else
                _ChapterPDFBLL.Remarks = null;

            //if (_ChapterPDFBLL.ChapterIdMain == null)
            //    if (hfChapterId.Value.Trim().Length > 0)
            //        _ChapterPDFBLL.ChapterIdMain = hfChapterId.Value.Trim();
            //    else
            //        _ChapterPDFBLL.ChapterIdMain = null;

            if (fuFileUpload.PostedFile != null && fuFileUpload.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuFileUpload.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuFileUpload.PostedFile;
                uploadedImage.InputStream.Read(imageSize, 0, (int)fuFileUpload.PostedFile.ContentLength);
                string[] getExtenstion = uploadedImage.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();
                string FileNameForInsert = uploadedImage.FileName.Replace("." + oExtension, "");

                _ChapterPDFBLL.UploadphotoPath = ConfigurationSettings.AppSettings["FolderPath"] + FileNameForInsert + "-" + System.DateTime.Now.Millisecond.ToString() + "." + oExtension;

                _ChapterPDFBLL.FileName = fuFileUpload.PostedFile.FileName;
            }

            bool isValid = Validate();

            if (isValid == true)
            {
                _ChapterPDFBLL.Save();

                if (fuFileUpload.PostedFile != null)
                {
                    HttpPostedFile uploadedImage = fuFileUpload.PostedFile;

                    uploadedImage.SaveAs(_ChapterPDFBLL.UploadphotoPath);

                    //imgFileUpload.ImageUrl = _ChapterPDFBLL.UploadphotoPath;
                }

                if (Request.QueryString["ChapterPDFId"] == null)
                {
                    Reset(true);
                    Session["_ChapterPDFBLL"] = null;
                    Session["_ChapterPDFBLL"] = new ChapterPDFBLL();
                    _ChapterPDFBLL = (ChapterPDFBLL)Session["_ChapterPDFBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_ChapterPDFBLL"] = null;
                    Response.Redirect("ChapterPDFs.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void ddlPeriodNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPeriodNo.SelectedIndex > 0 && ddlChapter.SelectedIndex > 0)
            {
                _ChapterPDFBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue.ToString());

                _ChapterPDFBLL.ChapterId = ddlChapter.SelectedValue;

                //string ChapterId = "";

                //if (_ChapterPDFBLL.PeriodNo != null)
                //{
                //    ChapterId = _ChapterPDFBLL.GetChapterId(_ChapterPDFBLL.ChapterId, Convert.ToDecimal(_ChapterPDFBLL.PeriodNo).ToString("#.##"), ddlChapter.SelectedValue.ToString());

                //    _ChapterPDFBLL.ChapterIdMain = ChapterId;
                //}
                //else
                //{
                //    ChapterId = "";
                //    _ChapterPDFBLL.ChapterIdMain = null;
                //}
                LoadChapterVideos();
                if (_ChapterPDFBLL.SrNo != null)
                {
                    if (Request.QueryString["ChapterPDFId"] == null)
                        txtSrNo.Text = _ChapterPDFBLL.SrNo;
                }
            }
            else
            {
                _ChapterPDFBLL.PeriodNo = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadChapterVideos()
    {
        try
        {
            ListItem li = new ListItem();

            ddlChapterVideoId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlChapterVideoId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterPDFBLL.LoadChapterVideo();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();

                ddlChapterVideoId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlChapterVideoId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["ChapterLinkId"] == null)
            {
                if (ddlStandard.SelectedIndex > 0)
                    _ChapterPDFBLL.StandardTextListId = ddlStandard.SelectedValue;
                else
                    _ChapterPDFBLL.StandardTextListId = null;

                if (ddlSubs.SelectedIndex > 0)
                    _ChapterPDFBLL.SubId = ddlSubs.SelectedValue;
                else
                    _ChapterPDFBLL.SubId = null;

                if (ddlChapter.SelectedIndex > 0)
                {
                    _ChapterPDFBLL.ChapterId = ddlChapter.SelectedValue;
                    hlTest.NavigateUrl = "../Exams/ViewResultDetailMasterSheet.aspx?ChapterId=" + _ChapterPDFBLL.ChapterId;
                    hlTest.Target = "_blank";
                }
                else
                    _ChapterPDFBLL.ChapterId = null;

                if (ddlChapterVideoId.SelectedIndex > 0)
                    _ChapterPDFBLL.ChapterVideoId = Convert.ToInt16(ddlChapterVideoId.SelectedValue);
                else
                    _ChapterPDFBLL.ChapterVideoId = null;

                if (_ChapterPDFBLL.SubId != null && _ChapterPDFBLL.ChapterId != null && _ChapterPDFBLL.ChapterVideoId != null)
                    txtSrNo.Text = _ChapterPDFBLL.getSrNo(_ChapterPDFBLL.ChapterId.ToString(), _ChapterPDFBLL.ChapterVideoId.ToString()).ToString();

            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }
}