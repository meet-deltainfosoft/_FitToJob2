using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Collections;
using System.Configuration;

public partial class Exams_ExamResultPublish : System.Web.UI.Page
{
    private ExamResultPublishBLL _examResultPublishBLL;

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["ExamResultPublishId"] == null)
            {
                _examResultPublishBLL = new ExamResultPublishBLL();
            }
            else
            {
                _examResultPublishBLL = new ExamResultPublishBLL(Request.QueryString["ExamResultPublishId"].ToString());
            }
            Session["_examResultPublishBLL"] = _examResultPublishBLL;
        }
        else
        {
            _examResultPublishBLL = (ExamResultPublishBLL)Session["_examResultPublishBLL"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                LoadStandard();

                if (Request.QueryString["ExamResultPublishId"] != null)
                {
                    lblTitle.Text = " - [Edit Mode]";
                    LoadWebForm();
                    btnDelete.Enabled = true;
                }
                else
                {
                    chkIsResultPublished.Checked = true;
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message);
        }
    }

    #endregion

    #region Load Dropdown
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

            foreach (DataRow dtr in _examResultPublishBLL.LoadStandard().Rows)
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
    private void LoadSubject()
    {
        try
        {
            ListItem li = new ListItem();

            ddlSubject.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlSubject.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _examResultPublishBLL.LoadSubject().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSubject.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    private void LoadTest()
    {
        try
        {
            ListItem li = new ListItem();

            ddlTest.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlTest.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _examResultPublishBLL.LoadTest().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlTest.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    private void LoadSChedule()
    {
        try
        {
            ListItem li = new ListItem();

            ddlSchedule.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlSchedule.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _examResultPublishBLL.LoadSChedule().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSchedule.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    #endregion

    #region Change Events
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject.SelectedIndex > 0)
            {
                _examResultPublishBLL.SubId = ddlSubject.SelectedValue;
                LoadTest();
            }
            else
            {
                _examResultPublishBLL.SubId = null;
                ddlTest.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
            {
                _examResultPublishBLL.StandardTextListId = ddlStandard.SelectedValue;
                LoadSubject();
            }
            else
            {
                _examResultPublishBLL.StandardTextListId = null;
                ddlSubject.Items.Clear();
                ddlTest.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlTest.SelectedIndex > 0)
            {
                _examResultPublishBLL.TestId = ddlTest.SelectedValue;
                LoadSChedule();
            }
            else
            {
                _examResultPublishBLL.TestId = null;
                ddlSchedule.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    #endregion

    #region Other Functions
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
            ShowErrors("err", ex.Message);
        }
    }
    private void HideErrors()
    {
        try
        {
            pnlErr.Visible = false;
            blErrs.Items.Clear();
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private bool Validate()
    {
        HideErrors();

        SortedList sl = _examResultPublishBLL.Validate();

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

    private void Reset()
    {
        ddlStandard.SelectedIndex = 0;
        ddlSubject.SelectedIndex = 0;
        ddlTest.SelectedIndex = 0;
        ddlSchedule.SelectedIndex = 0;
        chkIsResultPublished.Checked = false;
    }

    private void LoadWebForm()
    {
        if (_examResultPublishBLL.StandardTextListId != null)
        {
            ddlStandard.SelectedValue = _examResultPublishBLL.StandardTextListId;
            LoadSubject();
            ddlStandard.Enabled = false;
        }

        if (_examResultPublishBLL.SubId != null)
        {
            ddlSubject.SelectedValue = _examResultPublishBLL.SubId;
            LoadTest();
            ddlSubject.Enabled = false;
        }

        if (_examResultPublishBLL.TestId != null)
        {
            ddlTest.SelectedValue = _examResultPublishBLL.TestId;
            LoadSChedule();
            ddlTest.Enabled = false;
        }

        if (_examResultPublishBLL.ExamScheduleId != null)
        {
            ddlSchedule.SelectedValue = _examResultPublishBLL.ExamScheduleId;
            ddlSchedule.Enabled = false;
        }

        if (_examResultPublishBLL.IsResultPublished != null)
            chkIsResultPublished.Checked = Convert.ToBoolean(_examResultPublishBLL.IsResultPublished);

        if (_examResultPublishBLL.AnsKeyFilePathShow != null)
        {
            trShow.Visible = true;
            iframepdf.Attributes.Add("src", _examResultPublishBLL.AnsKeyFilePathShow);
        }
    }
    #endregion

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
                _examResultPublishBLL.StandardTextListId = ddlStandard.SelectedValue.ToString();
            else
                _examResultPublishBLL.StandardTextListId = null;

            if (ddlSubject.SelectedIndex > 0)
                _examResultPublishBLL.SubId = ddlSubject.SelectedValue.ToString();
            else
                _examResultPublishBLL.SubId = null;

            if (ddlTest.SelectedIndex > 0)
                _examResultPublishBLL.TestId = ddlTest.SelectedValue.ToString();
            else
                _examResultPublishBLL.TestId = null;

            if (ddlSchedule.SelectedIndex > 0)
                _examResultPublishBLL.ExamScheduleId = ddlSchedule.SelectedValue.ToString();
            else
                _examResultPublishBLL.ExamScheduleId = null;

            _examResultPublishBLL.IsResultPublished = chkIsResultPublished.Checked;

            if (fuAnsKeyFilePath.PostedFile != null && fuAnsKeyFilePath.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuAnsKeyFilePath.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuAnsKeyFilePath.PostedFile;

                string[] getExtenstion = fuAnsKeyFilePath.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuAnsKeyFilePath.PostedFile.ContentLength);

                string msExcelFilePathOnServer = ConfigurationSettings.AppSettings["FolderPath"] + "" + Convert.ToDateTime(System.DateTime.Now).ToString("ddMMMyyyy") + "/";

                bool exists = System.IO.Directory.Exists(msExcelFilePathOnServer);

                if (!exists)
                    System.IO.Directory.CreateDirectory(msExcelFilePathOnServer);

                _examResultPublishBLL.AnsKeyFilePath = msExcelFilePathOnServer + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_ANSKEY" + "." + oExtension;
                _examResultPublishBLL.IsChangeFile = true;
            }

            bool isValid = Validate();

            if (isValid == true)
            {
                _examResultPublishBLL.Save();

                if (fuAnsKeyFilePath.PostedFile != null && fuAnsKeyFilePath.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuAnsKeyFilePath.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuAnsKeyFilePath.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_examResultPublishBLL.AnsKeyFilePath);
                }

                if (Request.QueryString["ExamResultPublishId"] == null)
                {
                    Reset();
                    Session["_examResultPublishBLL"] = null;
                    Session["_examResultPublishBLL"] = new ExamResultPublishBLL();
                    _examResultPublishBLL = (ExamResultPublishBLL)Session["_examResultPublishBLL"];
                }
                else
                {
                    Session["_examResultPublishBLL"] = null;
                    Response.Redirect("ExamResultPublishs.aspx");
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
        Session["_examResultPublishBLL"] = null;

        if (Request.QueryString["ExamResultPublishId"] == null)
        {
            Response.Redirect("~/General/Default.aspx");
        }
        else
        {
            Response.Redirect("ExamResultPublishs.aspx");
        }

    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _examResultPublishBLL.Delete(Request.QueryString["ExamResultPublishId"]);
            Session["_examResultPublishBLL"] = null;
            Response.Redirect("ExamResultPublishs.aspx");
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }
}