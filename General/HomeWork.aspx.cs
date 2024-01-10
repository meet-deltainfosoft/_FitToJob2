using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Data;

public partial class General_HomeWork : System.Web.UI.Page
{
    private HomeWorkBLL _homeWorkBLL;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["HomeWorkId"] == null)
            {
                _homeWorkBLL = new HomeWorkBLL();
            }
            else
            {
                _homeWorkBLL = new HomeWorkBLL(Request.QueryString["HomeWorkId"].ToString());
            }

            //Session["_homeWorkBLL"] = _homeWorkBLL;

            for (int i = 0; i < 100; i++)
            {
                if (Session["_homeWorkBLL" + i + ""] == null)
                {
                    Session["_homeWorkBLL" + i + ""] = _homeWorkBLL;
                    hfMySessionHeaderValue.Value = "_homeWorkBLL" + i;
                    break;
                }
            }
        }
        else
        {
            Session[""] = null;
            //_homeWorkBLL = (HomeWorkBLL)Session["_homeWorkBLL"];
            _homeWorkBLL = (HomeWorkBLL)Session[hfMySessionHeaderValue.Value];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadStandard();
            txtSubject.Focus();

            if (Request.QueryString["HomeWorkId"] != null)
            {
                lblTitle.Text = " - [Edit Mode]";
                LoadWebForm();
                btnDelete.Enabled = true;
                btnOKAndAddNew.Enabled = false;
            }
            else
            {
                rbHomeWorkTypeMCQ.Checked = true;
                rbHomeWorkTypeMCQ_CheckedChanged(null, null);
                txtDt.Text = Convert.ToDateTime(DateTime.Today).ToString("dd-MMM-yyyy");
            }
        }
        else
        {
            //HideErrors();
            ////image rebind code

            //if (fileUploadQusImg.PostedFile != null && fileUploadQusImg.PostedFile.FileName != "")
            //{
            //    byte[] imageSize = new byte[fileUploadQusImg.PostedFile.ContentLength];
            //    HttpPostedFile uploadedImage = fileUploadQusImg.PostedFile;

            //    string[] getExtenstion = fileUploadQusImg.FileName.Split('.');
            //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

            //    uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadQusImg.PostedFile.ContentLength);

            //    _homeWorkBLL.ImageNameQus = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_Q" + "." + oExtension;

            //    System.IO.Stream stream = fileUploadQusImg.PostedFile.InputStream;
            //    System.Drawing.Image image = System.Drawing.Image.FromStream(stream);

            //    float height = image.PhysicalDimension.Height;
            //    float width = image.PhysicalDimension.Width;

            //    if (width > 500)
            //    {
            //        ShowErrors("er", "HomeWork image width size must be in range of 500 pixel.");
            //    }

            //    imgqusPics.ImageUrl = fileUploadQusImg.PostedFile;
            //}

            ////image rebind code
            ///
            btnOKAndAddNew.Enabled = true;
            if (textarea1.Value.ToString().Trim().Length > 50)
            {
                HomeWorkTest.Style.Add("background", "url(" + textarea1.Value.Trim().Replace("\"", "") + ")");
            }
            if (textarea2.Value.ToString().Trim().Length > 50)
            {
                //if (textarea2.Value.ToString().Trim().Substring(0, 3).Contains("\""))
                A1Test.Style.Add("background", "url(" + textarea2.Value.ToString().Trim().Replace("\"", "") + ")");
            }
            if (textarea3.Value.ToString().Trim().Length > 50)
            {
                A2Test.Style.Add("background", "url(" + textarea3.Value.Trim().Replace("\"", "") + ")");
            }
            if (textarea4.Value.ToString().Trim().Length > 50)
            {
                A3Test.Style.Add("background", "url(" + textarea4.Value.Trim().Replace("\"", "") + ")");
            }
            if (textarea5.Value.ToString().Trim().Length > 50)
            {
                A4Test.Style.Add("background", "url(" + textarea5.Value.Trim().Replace("\"", "") + ")");
            }
            HideErrors();

            Session[""] = null;
            _homeWorkBLL = (HomeWorkBLL)Session[hfMySessionHeaderValue.Value];
        }
    }

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
            dt = _homeWorkBLL.LoadStandard();

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

    private void LoadSubjects()
    {
        try
        {
            ListItem li = new ListItem();

            ddlSubId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlSubId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _homeWorkBLL.LoadSubjects(ddlStandardTextListId.SelectedValue.ToString());

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSubId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadChapters()
    {
        try
        {
            ListItem li = new ListItem();

            ddlChapterId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlChapterId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _homeWorkBLL.LoadChapters();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[2].ToString();
                li.Value = dtr[0].ToString();
                ddlChapterId.Items.Add(li);

                li = null;
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
            dt = _homeWorkBLL.LoadChapterVideo();

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

    public void setDataToBLLfromObject()
    {
        try
        {
            if (ddlStandardTextListId.SelectedIndex > 0)
            {
                _homeWorkBLL.StandardTextListId = ddlStandardTextListId.SelectedValue;
            }
            else
            {
                _homeWorkBLL.StandardTextListId = null;
            }

            if (ddlSubId.SelectedIndex > 0)
            {
                _homeWorkBLL.SubId = ddlSubId.SelectedValue.ToString();
                _homeWorkBLL.Subject = ddlSubId.SelectedItem.Text.Trim().ToString();
            }
            else
            {
                _homeWorkBLL.SubId = null;
                _homeWorkBLL.Subject = null;
            }
            //HomeWork ---  HomeWorkImg
            if (txtHomeWork.Text.Trim().Length > 0)
            {
                _homeWorkBLL.HomeWork = txtHomeWork.Text.Trim();
            }
            else
            {
                _homeWorkBLL.HomeWork = null;
                _homeWorkBLL.HomeWorkImg = null;
            }


            //A1 --- A1Img
            if (txtA1.Text.Trim().Length > 0)
            {
                _homeWorkBLL.A1 = txtA1.Text.Trim();
            }
            else
            {
                _homeWorkBLL.A1 = null;
                _homeWorkBLL.A1Img = null;
            }

            //A2-- A2Img
            if (txtA2.Text.Trim().Length > 0)
            {
                _homeWorkBLL.A2 = txtA2.Text.Trim();
            }
            else
            {
                _homeWorkBLL.A2 = null;
                _homeWorkBLL.A2Img = null;
            }

            //A3-- A3Img
            if (txtA3.Text.Trim().Length > 0)
            {
                _homeWorkBLL.A3 = txtA3.Text.Trim();
            }
            else
            {
                _homeWorkBLL.A3 = null;
                _homeWorkBLL.A3Img = null;
            }

            //A4-- A4Img
            if (txtA4.Text.Trim().Length > 0)
            {
                _homeWorkBLL.A4 = txtA4.Text.Trim();
            }
            else
            {
                _homeWorkBLL.A4 = null;
                _homeWorkBLL.A4Img = null;
            }

            string NewGUID = System.Guid.NewGuid().ToString();

            try
            {
                String path = ConfigurationSettings.AppSettings["FolderPath"]; //Path

                //Check if directory exist
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                }
            }
            catch
            {
            }
            if (fileUploadQusImg.PostedFile != null && fileUploadQusImg.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadQusImg.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadQusImg.PostedFile;

                string[] getExtenstion = fileUploadQusImg.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadQusImg.PostedFile.ContentLength);

                _homeWorkBLL.ImageNameQus = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_Q" + "." + oExtension;
                _homeWorkBLL.IsHomeWorkChanged = true;
            }
            else if (fuUploadPDF.PostedFile != null && fuUploadPDF.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuUploadPDF.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuUploadPDF.PostedFile;

                string[] getExtenstion = fuUploadPDF.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuUploadPDF.PostedFile.ContentLength);

                _homeWorkBLL.ImageNameQus = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_Q" + "." + oExtension;
                _homeWorkBLL.IsHomeWorkChanged = true;
            }
            else if (textarea1.Value.ToString().Trim().Length > 50)
            {
                _homeWorkBLL.ImageNameQus = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_Q" + ".png";
                _homeWorkBLL.IsHomeWorkChanged = true;
            }

            if (fileUploadA1Img.PostedFile != null && fileUploadA1Img.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadA1Img.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadA1Img.PostedFile;

                string[] getExtenstion = fileUploadA1Img.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadA1Img.PostedFile.ContentLength);

                _homeWorkBLL.ImageNameA1 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_A1" + "." + oExtension;
                _homeWorkBLL.IsA1Changed = true;
            }
            else if (textarea2.Value.ToString().Trim().Length > 50)
            {
                _homeWorkBLL.ImageNameA1 = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_A1" + ".png";
                _homeWorkBLL.IsA1Changed = true;
            }
            if (fileUploadA2Img.PostedFile != null && fileUploadA2Img.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadA2Img.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadA2Img.PostedFile;

                string[] getExtenstion = fileUploadA2Img.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadA2Img.PostedFile.ContentLength);

                _homeWorkBLL.ImageNameA2 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_A2" + "." + oExtension;
                _homeWorkBLL.IsA2Changed = true;
            }
            else if (textarea3.Value.ToString().Trim().Length > 50)
            {
                _homeWorkBLL.ImageNameA2 = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_A2" + ".png";
                _homeWorkBLL.IsA2Changed = true;
            }
            if (fileUploadA3Img.PostedFile != null && fileUploadA3Img.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadA3Img.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadA3Img.PostedFile;

                string[] getExtenstion = fileUploadA3Img.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadA3Img.PostedFile.ContentLength);

                _homeWorkBLL.ImageNameA3 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_A3" + "." + oExtension;
                _homeWorkBLL.IsA3Changed = true;
            }
            else if (textarea4.Value.ToString().Trim().Length > 50)
            {
                _homeWorkBLL.ImageNameA3 = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_A3" + ".png";
                _homeWorkBLL.IsA3Changed = true;
            }
            if (fileUploadA4Img.PostedFile != null && fileUploadA4Img.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadA4Img.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadA4Img.PostedFile;

                string[] getExtenstion = fileUploadA4Img.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadA4Img.PostedFile.ContentLength);

                _homeWorkBLL.ImageNameA4 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_A4" + "." + oExtension;
                _homeWorkBLL.IsA4Changed = true;
            }
            else if (textarea5.Value.ToString().Trim().Length > 50)
            {
                _homeWorkBLL.ImageNameA4 = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_A4" + ".png";
                _homeWorkBLL.IsA4Changed = true;
            }

            if (ddlChapterId.SelectedIndex > 0)
                _homeWorkBLL.ChapterId = ddlChapterId.SelectedValue;
            else
                _homeWorkBLL.ChapterId = null;

            if (txtSrNo.Text.Trim().Length > 0)
                _homeWorkBLL.SrNo = Convert.ToInt16(txtSrNo.Text.Trim().ToString());
            else
                _homeWorkBLL.SrNo = null;

            if (rbHomeWorkTypeMCQ.Checked)
            {
                _homeWorkBLL.HomeWorkType = "MCQ";

                if (rbAnsSelectionSingle.Checked)
                {
                    if (ddlAnswer.SelectedIndex > 0)
                    {
                        _homeWorkBLL.Ans = (ddlAnswer.SelectedValue).ToString();
                    }
                    else
                    {
                        _homeWorkBLL.Ans = null;
                    }

                    _homeWorkBLL.AnsSelection = "SINGLE";
                }
                else if (rbAnsSelectionMulti.Checked)
                {
                    string Id1 = null;

                    foreach (ListItem li in chklAns.Items)
                    {
                        if (li.Selected == true)
                        {
                            if (Id1 != null)
                            {
                                Id1 = Id1 + ",";
                            }
                            Id1 = Id1 + "" + li.Value + "";
                        }
                    }

                    if (Id1.Trim().Length > 0)
                        _homeWorkBLL.Ans = Id1;
                    else
                        _homeWorkBLL.Ans = null;

                    _homeWorkBLL.AnsSelection = "MULTIPLE";
                }
                else
                {
                    _homeWorkBLL.Ans = null;
                    _homeWorkBLL.AnsSelection = null;
                }
            }
            else if (rbHomeWorkTypeNONMCQ.Checked)
            {
                _homeWorkBLL.HomeWorkType = "NONMCQ";

                if (txtAns.Text.Trim().Length > 0)
                    _homeWorkBLL.Ans = txtAns.Text.ToString();
                else
                    _homeWorkBLL.Ans = null;

                _homeWorkBLL.AnsSelection = null;
            }
            else if (rbHomeWorkTypeFILE.Checked)
            {
                _homeWorkBLL.HomeWorkType = "FILE";
                _homeWorkBLL.Ans = null;
                _homeWorkBLL.AnsSelection = null;
            }
            else if (rbQueTypeWholeQPaper.Checked)
            {
                _homeWorkBLL.HomeWorkType = "PDF";
                _homeWorkBLL.Ans = null;
                _homeWorkBLL.AnsSelection = null;
            }
            else
            {
                _homeWorkBLL.Ans = null;
                _homeWorkBLL.HomeWorkType = null;
                _homeWorkBLL.AnsSelection = null;
            }

            if (rbHomeWorkDataTypeNum.Checked)
            {
                _homeWorkBLL.HomeWorkDataType = "NUM";
            }
            else if (rbHomeWorkDataTypeChar.Checked)
            {
                _homeWorkBLL.HomeWorkDataType = "CHAR";
            }
            else
            {
                _homeWorkBLL.HomeWorkDataType = null;
            }

            if (ddlNoOfFile.Visible)
                _homeWorkBLL.NoOfFile = Convert.ToInt16(ddlNoOfFile.SelectedValue.ToString());
            else
                _homeWorkBLL.NoOfFile = null;

            if (fuSampleAns1.PostedFile != null && fuSampleAns1.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuSampleAns1.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuSampleAns1.PostedFile;

                string[] getExtenstion = fuSampleAns1.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuSampleAns1.PostedFile.ContentLength);

                _homeWorkBLL.SampleAns1 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_S1" + "." + oExtension;
                _homeWorkBLL.IsSampleAns1Changed = true;
            }

            if (fuSampleAns2.PostedFile != null && fuSampleAns2.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuSampleAns2.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuSampleAns2.PostedFile;

                string[] getExtenstion = fuSampleAns2.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuSampleAns2.PostedFile.ContentLength);

                _homeWorkBLL.SampleAns2 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_S2" + "." + oExtension;
                _homeWorkBLL.IsSampleAns2Changed = true;
            }

            if (fuSampleAns3.PostedFile != null && fuSampleAns3.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuSampleAns3.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuSampleAns3.PostedFile;

                string[] getExtenstion = fuSampleAns3.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuSampleAns3.PostedFile.ContentLength);

                _homeWorkBLL.SampleAns3 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_S3" + "." + oExtension;
                _homeWorkBLL.IsSampleAns3Changed = true;
            }

            if (fuSampleAns4.PostedFile != null && fuSampleAns4.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuSampleAns4.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuSampleAns4.PostedFile;

                string[] getExtenstion = fuSampleAns4.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuSampleAns4.PostedFile.ContentLength);

                _homeWorkBLL.SampleAns4 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_S4" + "." + oExtension;
                _homeWorkBLL.IsSampleAns4Changed = true;
            }

            if (txtDt.Text.Trim().Length > 0)
                _homeWorkBLL.Dt = Convert.ToDateTime(txtDt.Text.Trim());
            else
                _homeWorkBLL.Dt = null;

            if (ddlChapterVideoId.SelectedIndex > 0)
                _homeWorkBLL.ChapterVideoId = Convert.ToInt16(ddlChapterVideoId.SelectedValue);
            else
                _homeWorkBLL.ChapterVideoId = null;
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message);
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            setDataToBLLfromObject();

            bool? IsFilePDF = false;

            if (rbQueTypeWholeQPaper.Checked)
            {
                string[] validFileTypes = { "pdf" };

                string ext = System.IO.Path.GetExtension(fuUploadPDF.PostedFile.FileName);

                bool isValidFile = false;

                for (int i = 0; i < validFileTypes.Length; i++)
                {

                    if (ext == "." + validFileTypes[i])
                    {
                        isValidFile = true;

                        break;
                    }

                }

                if (!isValidFile)
                {
                    IsFilePDF = false;
                }

                else
                {
                    IsFilePDF = true;
                }

                if (IsFilePDF == false)
                {
                    ShowErrors("err", "Please Select PDF File of Question Paper.");
                    return;
                }
            }

            bool isValid = Validate();

            if (isValid == true)
            {
                _homeWorkBLL.Save();

                if (fileUploadQusImg.PostedFile != null && fileUploadQusImg.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadQusImg.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadQusImg.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.ImageNameQus);
                }
                else if (fuUploadPDF.PostedFile != null && fuUploadPDF.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuUploadPDF.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuUploadPDF.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.ImageNameQus);
                }
                else if (textarea1.Value.ToString().Trim().Length > 50 && _homeWorkBLL.ImageNameQus != null)
                {
                    string imgPath = _homeWorkBLL.ImageNameQus;

                    byte[] imageBytes = Convert.FromBase64String(textarea1.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }

                if (fileUploadA1Img.PostedFile != null && fileUploadA1Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA1Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA1Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.ImageNameA1);
                }
                else if (textarea2.Value.ToString().Trim().Length > 50 && _homeWorkBLL.ImageNameA1 != null)
                {
                    string imgPath = _homeWorkBLL.ImageNameA1;

                    byte[] imageBytes = Convert.FromBase64String(textarea2.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA2Img.PostedFile != null && fileUploadA2Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA2Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA2Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.ImageNameA2);
                }
                else if (textarea3.Value.ToString().Trim().Length > 50 && _homeWorkBLL.ImageNameA2 != null)
                {
                    string imgPath = _homeWorkBLL.ImageNameA2;

                    byte[] imageBytes = Convert.FromBase64String(textarea3.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA3Img.PostedFile != null && fileUploadA3Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA3Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA3Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.ImageNameA3);
                }
                else if (textarea4.Value.ToString().Trim().Length > 50 && _homeWorkBLL.ImageNameA3 != null)
                {
                    string imgPath = _homeWorkBLL.ImageNameA3;

                    byte[] imageBytes = Convert.FromBase64String(textarea4.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA4Img.PostedFile != null && fileUploadA4Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA4Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA4Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.ImageNameA4);
                }
                else if (textarea5.Value.ToString().Trim().Length > 50 && _homeWorkBLL.ImageNameA4 != null)
                {
                    string imgPath = _homeWorkBLL.ImageNameA4;

                    byte[] imageBytes = Convert.FromBase64String(textarea5.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }

                if (fuSampleAns1.PostedFile != null && fuSampleAns1.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuSampleAns1.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuSampleAns1.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.SampleAns1);
                }

                if (fuSampleAns2.PostedFile != null && fuSampleAns2.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuSampleAns2.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuSampleAns2.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.SampleAns2);
                }

                if (fuSampleAns3.PostedFile != null && fuSampleAns3.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuSampleAns3.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuSampleAns3.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.SampleAns3);
                }

                if (fuSampleAns4.PostedFile != null && fuSampleAns4.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuSampleAns4.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuSampleAns4.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.SampleAns4);
                }

                if (Request.QueryString["HomeWorkId"] == null)
                {
                    Reset();
                    //Session["_homeWorkBLL"] = null;
                    //Session["_homeWorkBLL"] = new HomeWorkBLL();
                    //_homeWorkBLL = (HomeWorkBLL)Session["_homeWorkBLL"];

                    Session[hfMySessionHeaderValue.Value] = null;
                    for (int i = 0; i < 100; i++)
                    {
                        if (Session["_homeWorkBLL" + i + ""] == null)
                        {
                            Session["_homeWorkBLL" + i + ""] = _homeWorkBLL;
                            hfMySessionHeaderValue.Value = "_homeWorkBLL" + i;
                            break;
                        }
                    }

                    Session[hfMySessionHeaderValue.Value] = new HomeWorkBLL();

                    _homeWorkBLL = (HomeWorkBLL)Session[hfMySessionHeaderValue.Value];

                    txtSubject.Focus();
                }
                else
                {
                    //Session["_homeWorkBLL"] = null;
                    Session[hfMySessionHeaderValue.Value] = null;
                    Response.Redirect("HomeWorks.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    protected void btnOKAndAddNew_Click(object sender, EventArgs e)
    {
        try
        {
            setDataToBLLfromObject();

            bool isValid = Validate();

            if (isValid == true)
            {
                _homeWorkBLL.Save();

                if (fileUploadQusImg.PostedFile != null && fileUploadQusImg.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadQusImg.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadQusImg.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.ImageNameQus);
                }
                else if (fuUploadPDF.PostedFile != null && fuUploadPDF.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuUploadPDF.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuUploadPDF.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.ImageNameQus);
                }
                else if (textarea1.Value.ToString().Trim().Length > 50 && _homeWorkBLL.ImageNameQus != null)
                {
                    string imgPath = _homeWorkBLL.ImageNameQus;

                    byte[] imageBytes = Convert.FromBase64String(textarea1.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }

                if (fileUploadA1Img.PostedFile != null && fileUploadA1Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA1Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA1Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.ImageNameA1);
                }
                else if (textarea2.Value.ToString().Trim().Length > 50 && _homeWorkBLL.ImageNameA1 != null)
                {
                    string imgPath = _homeWorkBLL.ImageNameA1;

                    byte[] imageBytes = Convert.FromBase64String(textarea2.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA2Img.PostedFile != null && fileUploadA2Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA2Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA2Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.ImageNameA2);
                }
                else if (textarea3.Value.ToString().Trim().Length > 50 && _homeWorkBLL.ImageNameA2 != null)
                {
                    string imgPath = _homeWorkBLL.ImageNameA2;

                    byte[] imageBytes = Convert.FromBase64String(textarea3.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA3Img.PostedFile != null && fileUploadA3Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA3Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA3Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.ImageNameA3);
                }
                else if (textarea4.Value.ToString().Trim().Length > 50 && _homeWorkBLL.ImageNameA3 != null)
                {
                    string imgPath = _homeWorkBLL.ImageNameA3;

                    byte[] imageBytes = Convert.FromBase64String(textarea4.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA4Img.PostedFile != null && fileUploadA4Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA4Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA4Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_homeWorkBLL.ImageNameA4);
                }
                else if (textarea5.Value.ToString().Trim().Length > 50 && _homeWorkBLL.ImageNameA4 != null)
                {
                    string imgPath = _homeWorkBLL.ImageNameA4;

                    byte[] imageBytes = Convert.FromBase64String(textarea5.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }

                if (Request.QueryString["HomeWorkId"] == null)
                {
                    string str_ddlStandardTextListId, str_ddlSubId, str_ddlTestId;
                    str_ddlStandardTextListId = ddlStandardTextListId.SelectedValue;
                    str_ddlSubId = ddlSubId.SelectedValue.ToString();
                    str_ddlTestId = ddlChapterId.SelectedValue;

                    //Reset();
                    ResetWithOutCheckBox();

                    //Session["_homeWorkBLL"] = null;
                    //Session["_homeWorkBLL"] = new HomeWorkBLL();
                    //_homeWorkBLL = (HomeWorkBLL)Session["_homeWorkBLL"];

                    Session[hfMySessionHeaderValue.Value] = null;
                    for (int i = 0; i < 100; i++)
                    {
                        if (Session["_homeWorkBLL" + i + ""] == null)
                        {
                            Session["_homeWorkBLL" + i + ""] = _homeWorkBLL;
                            hfMySessionHeaderValue.Value = "_homeWorkBLL" + i;
                            break;
                        }
                    }

                    Session[hfMySessionHeaderValue.Value] = new HomeWorkBLL();

                    _homeWorkBLL = (HomeWorkBLL)Session[hfMySessionHeaderValue.Value];

                    ddlStandardTextListId.SelectedValue = str_ddlStandardTextListId;
                    ddlStandardTextListId_SelectedIndexChanged(sender, new EventArgs());

                    ddlSubId.SelectedValue = str_ddlSubId;
                    ddlSubId_SelectedIndexChanged(sender, new EventArgs());
                    ddlChapterId.SelectedValue = str_ddlTestId;
                    ddlChapterId_SelectedIndexChanged(sender, new EventArgs());
                    visibleHomeWorkTypeData();
                    txtHomeWork.Focus();
                }
                else
                {
                    //Session["_homeWorkBLL"] = null;
                    Session[hfMySessionHeaderValue.Value] = null;
                    Response.Redirect("HomeWorks.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    private bool Validate()
    {
        HideErrors();

        SortedList sl = _homeWorkBLL.Validate();

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

        //Subject
        if (key == "Subject")
        {
            lblSubject.CssClass = "error";
            txtSubject.CssClass = "error";
        }
        //Subject

        //HomeWork
        if (key == "HomeWork")
        {
            lblHomeWork.CssClass = "error";
            txtHomeWork.CssClass = "error";
        }
        //HomeWork

        //A1
        if (key == "A1")
        {
            lblA1.CssClass = "error";
            txtA1.CssClass = "error";
        }
        //A1

        //A2
        if (key == "A2")
        {
            lblA2.CssClass = "error";
            txtA2.CssClass = "error";
        }
        //A2

        //A3
        if (key == "A3")
        {
            lblA3.CssClass = "error";
            txtA3.CssClass = "error";
        }
        //A3

        //A4
        if (key == "A4")
        {
            lblA4.CssClass = "error";
            txtA4.CssClass = "error";
        }
        //A4

        //Answer
        if (key == "Answer")
        {
            lblAnswer.CssClass = "error";
            ddlAnswer.CssClass = "error";
        }
        //Answer
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        lblSubject.CssClass = "";
        txtSubject.CssClass = "";

        lblHomeWork.CssClass = "";
        txtHomeWork.CssClass = "";

        lblA1.CssClass = "";
        txtA1.CssClass = "";

        lblA2.CssClass = "";
        txtA2.CssClass = "";

        lblA3.CssClass = "";
        txtA3.CssClass = "";

        lblA4.CssClass = "";
        txtA4.CssClass = "";
    }

    private void Reset()
    {
        txtSubject.Text = "";
        txtHomeWork.Text = "";
        txtA1.Text = "";
        txtA2.Text = "";
        txtA3.Text = "";
        txtA4.Text = "";
        ddlAnswer.SelectedIndex = 0;
        txtHomeWork.Focus();

        ddlChapterId.SelectedIndex = 0;
        ddlSubId.SelectedIndex = 0;

        txtSrNo.Text = "";

        rbHomeWorkTypeMCQ.Checked = false;
        rbHomeWorkTypeNONMCQ.Checked = false;
        rbHomeWorkTypeFILE.Checked = false;

        rbHomeWorkDataTypeNum.Checked = false;
        rbHomeWorkDataTypeChar.Checked = false;

        txtAns.Text = "";

        ddlNoOfFile.SelectedIndex = 0;

        rbAnsSelectionSingle.Checked = true;
        chklAns.ClearSelection();
        txtDt.Text = Convert.ToDateTime(DateTime.Today).ToString("dd-MMM-yyyy");
        ddlChapterVideoId.SelectedIndex = 0;
    }

    private void ResetWithOutCheckBox()
    {
        txtSubject.Text = "";
        txtHomeWork.Text = "";
        txtA1.Text = "";
        txtA2.Text = "";
        txtA3.Text = "";
        txtA4.Text = "";
        ddlAnswer.SelectedIndex = 0;
        txtHomeWork.Focus();

        ddlChapterId.SelectedIndex = 0;
        ddlSubId.SelectedIndex = 0;

        txtSrNo.Text = "";

        //rbHomeWorkTypeMCQ.Checked = false;
        //rbHomeWorkTypeNONMCQ.Checked = false;
        //rbHomeWorkTypeFILE.Checked = false;

        //rbHomeWorkDataTypeNum.Checked = false;
        //rbHomeWorkDataTypeChar.Checked = false;

        txtAns.Text = "";
        ddlNoOfFile.SelectedIndex = 0;
        rbAnsSelectionSingle.Checked = true;
        chklAns.ClearSelection();
    }

    private void LoadWebForm()
    {
        if (_homeWorkBLL.StandardTextListId != null)
        {
            ddlStandardTextListId.SelectedValue = _homeWorkBLL.StandardTextListId;
            ddlStandardTextListId_SelectedIndexChanged(null, null);
        }

        //Subject
        if (_homeWorkBLL.SubId != null)
        {
            //hfSubId.Value = _homeWorkBLL.SubId;
            //txtSubject.Text = _homeWorkBLL.Subject;

            ddlSubId.SelectedValue = _homeWorkBLL.SubId;
            ddlSubId_SelectedIndexChanged(null, null);
        }

        if (_homeWorkBLL.ChapterId != null)
        {
            ddlChapterId.SelectedValue = _homeWorkBLL.ChapterId;
            ddlChapterId_SelectedIndexChanged(null, null);
        }

        if (_homeWorkBLL.ChapterVideoId != null)
            ddlChapterVideoId.SelectedValue = _homeWorkBLL.ChapterVideoId.ToString();

        //HomeWork
        if (_homeWorkBLL.HomeWork != null)
            txtHomeWork.Text = _homeWorkBLL.HomeWork;

        //A1
        if (_homeWorkBLL.A1 != null)
            txtA1.Text = _homeWorkBLL.A1;

        //A2
        if (_homeWorkBLL.A2 != null)
            txtA2.Text = _homeWorkBLL.A2;

        //A3
        if (_homeWorkBLL.A3 != null)
            txtA3.Text = _homeWorkBLL.A3;

        //A4
        if (_homeWorkBLL.A4 != null)
            txtA4.Text = _homeWorkBLL.A4;

        //Answer

        if (_homeWorkBLL.HomeWorkType != null)
        {
            if (_homeWorkBLL.HomeWorkType.ToString().ToUpper() == "MCQ".ToString().ToUpper())
            {
                rbHomeWorkTypeMCQ.Checked = true;
                rbHomeWorkTypeNONMCQ.Checked = false;
                rbHomeWorkTypeMCQ_CheckedChanged(null, null);

                if (_homeWorkBLL.AnsSelection != null)
                {
                    if (_homeWorkBLL.AnsSelection.ToString().ToUpper() == "SINGLE".ToString().ToUpper())
                    {
                        rbAnsSelectionSingle.Checked = true;
                        rbAnsSelectionMulti.Checked = false;

                        rbAnsSelectionSingle_CheckedChanged(null, null);
                    }
                    else if (_homeWorkBLL.AnsSelection.ToString().ToUpper() == "MULTIPLE".ToString().ToUpper())
                    {
                        rbAnsSelectionSingle.Checked = false;
                        rbAnsSelectionMulti.Checked = true;

                        rbAnsSelectionMulti_CheckedChanged(null, null);
                    }
                }

                if (rbAnsSelectionSingle.Checked)
                {
                    if (_homeWorkBLL.Ans != null)
                    {
                        ddlAnswer.SelectedValue = (_homeWorkBLL.Ans).ToString();
                    }
                }
                else
                {
                    if (_homeWorkBLL.Ans != null)
                    {
                        string[] answer = _homeWorkBLL.Ans.ToString().Split(',');

                        if (answer.Length > 0)
                        {
                            for (int i = 0; i < answer.Length; i++)
                            {
                                foreach (ListItem li in chklAns.Items)
                                {
                                    if (li.Value.ToString() == answer[i].ToString().ToUpper())
                                    {
                                        li.Selected = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (ListItem li in chklAns.Items)
                            {
                                if (li.Value.ToString() == _homeWorkBLL.Ans.ToString().ToUpper())
                                {
                                    li.Selected = true;
                                }
                            }
                        }
                    }
                }
            }
            else if (_homeWorkBLL.HomeWorkType.ToString().ToUpper() == "NONMCQ".ToString().ToUpper())
            {
                rbHomeWorkTypeMCQ.Checked = false;
                rbHomeWorkTypeNONMCQ.Checked = true;
                rbHomeWorkTypeMCQ_CheckedChanged(null, null);

                if (_homeWorkBLL.Ans != null)
                {
                    txtAns.Text = (_homeWorkBLL.Ans).ToString();
                }
            }
            else if (_homeWorkBLL.HomeWorkType.ToString().ToUpper() == "FILE".ToString().ToUpper())
            {
                rbHomeWorkTypeFILE.Checked = true;
                rbHomeWorkTypeFILE_CheckedChanged(null, null);
            }
            else if (_homeWorkBLL.HomeWorkType.ToString().ToUpper() == "PDF".ToString().ToUpper())
            {
                rbQueTypeWholeQPaper.Checked = true;
                rbQueTypeWholeQPaper_CheckedChanged(null, null);

                if (_homeWorkBLL.ImageNameQus != null)
                {
                    trFramePaper.Visible = true;
                    iframepdf.Attributes.Add("src", _homeWorkBLL.ImageNameQus);
                }
            }
        }

        if (_homeWorkBLL.ImageNameQus != null)
        {
            imgqusPics.ImageUrl = _homeWorkBLL.ImageNameQus;
        }
        if (_homeWorkBLL.ImageNameA1 != null)
        {
            imga1Pics.ImageUrl = _homeWorkBLL.ImageNameA1;
        }
        if (_homeWorkBLL.ImageNameA2 != null)
        {
            imga2Pics.ImageUrl = _homeWorkBLL.ImageNameA2;
        }
        if (_homeWorkBLL.ImageNameA3 != null)
        {
            imga3Pics.ImageUrl = _homeWorkBLL.ImageNameA3;
        }
        if (_homeWorkBLL.ImageNameA4 != null)
        {
            imga4Pics.ImageUrl = _homeWorkBLL.ImageNameA4;
        }

        if (_homeWorkBLL.SrNo != null)
        {
            txtSrNo.Text = Convert.ToString(_homeWorkBLL.SrNo);
        }

        if (_homeWorkBLL.HomeWorkDataType != null)
        {
            if (_homeWorkBLL.HomeWorkDataType.ToString().ToUpper() == "NUM".ToString().ToUpper())
            {
                rbHomeWorkDataTypeNum.Checked = true;
                rbHomeWorkDataTypeChar.Checked = false;
            }
            else if (_homeWorkBLL.HomeWorkDataType.ToString().ToUpper() == "CHAR".ToString().ToUpper())
            {
                rbHomeWorkDataTypeNum.Checked = false;
                rbHomeWorkDataTypeChar.Checked = true;
            }
        }


        if (_homeWorkBLL.NoOfFile != null)
        {
            ddlNoOfFile.SelectedValue = _homeWorkBLL.NoOfFile.ToString();
        }

        if (_homeWorkBLL.SampleAns1 != null)
        {
            hlSampleAns1.NavigateUrl = _homeWorkBLL.SampleAns1.ToString();
            hlSampleAns1.Target = "_blank";
        }

        if (_homeWorkBLL.SampleAns2 != null)
        {
            hlSampleAns2.NavigateUrl = _homeWorkBLL.SampleAns2.ToString();
            hlSampleAns2.Target = "_blank";
        }

        if (_homeWorkBLL.SampleAns3 != null)
        {
            hlSampleAns3.NavigateUrl = _homeWorkBLL.SampleAns3.ToString();
            hlSampleAns3.Target = "_blank";
        }

        if (_homeWorkBLL.SampleAns4 != null)
        {
            hlSampleAns4.NavigateUrl = _homeWorkBLL.SampleAns4.ToString();
            hlSampleAns4.Target = "_blank";
        }

        if (_homeWorkBLL.Dt != null)
            txtDt.Text = Convert.ToDateTime(_homeWorkBLL.Dt).ToString("dd-MMM-yyyy");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _homeWorkBLL.Delete(Request.QueryString["HomeWorkId"].ToString());
            HideErrors();
            //Session["_homeWorkBLL"] = null;
            Session[hfMySessionHeaderValue.Value] = null;
            Reset();
            Response.Redirect("HomeWorks.aspx");
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Session["_homeWorkBLL"] = null;
        Session[hfMySessionHeaderValue.Value] = null;

        if (Request.QueryString["HomeWorkId"] == null)
        {
            Response.Redirect("~/General/Default.aspx");
        }
        else
        {
            Response.Redirect("HomeWorks.aspx");
        }
    }

    protected void ddlStandardTextListId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandardTextListId.SelectedIndex > 0)
            {
                LoadSubjects();
            }
            else
            {
                ShowErrors("err", "You have to select Standard");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }
    protected void ddlSubId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubId.SelectedIndex > 0)
            {
                _homeWorkBLL.SubId = ddlSubId.SelectedValue;
                LoadChapters();
            }
            else
            {
                _homeWorkBLL.SubId = null;
                ShowErrors("err", "You have to select Subject");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }

    protected void ddlChapterId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (Request.QueryString["HomeWorkId"] == null)
            //{
            if (ddlStandardTextListId.SelectedIndex > 0)
                _homeWorkBLL.StandardTextListId = ddlStandardTextListId.SelectedValue;
            else
                _homeWorkBLL.StandardTextListId = null;

            if (ddlSubId.SelectedIndex > 0)
                _homeWorkBLL.SubId = ddlSubId.SelectedValue;
            else
                _homeWorkBLL.SubId = null;

            if (ddlChapterId.SelectedIndex > 0)
            {
                _homeWorkBLL.ChapterId = ddlChapterId.SelectedValue;
                hlTest.NavigateUrl = "../Exams/ViewResultDetailMasterSheet.aspx?ChapterId=" + _homeWorkBLL.ChapterId;
                hlTest.Target = "_blank";
            }
            else
            {
                _homeWorkBLL.ChapterId = null;
            }

            LoadChapterVideos();

            //if (_homeWorkBLL.SubId != null && _homeWorkBLL.ChapterId != null)
            //{
            //    txtSrNo.Text = _homeWorkBLL.getSrNo().ToString();
            //}
            //else
            //{
            //}
            //}
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }

    protected void ddlChapterVideoId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["HomeWorkId"] == null)
            {
                if (ddlStandardTextListId.SelectedIndex > 0)
                    _homeWorkBLL.StandardTextListId = ddlStandardTextListId.SelectedValue;
                else
                    _homeWorkBLL.StandardTextListId = null;

                if (ddlSubId.SelectedIndex > 0)
                    _homeWorkBLL.SubId = ddlSubId.SelectedValue;
                else
                    _homeWorkBLL.SubId = null;

                if (ddlChapterId.SelectedIndex > 0)
                {
                    _homeWorkBLL.ChapterId = ddlChapterId.SelectedValue;
                    hlTest.NavigateUrl = "../Exams/ViewResultDetailMasterSheet.aspx?ChapterId=" + _homeWorkBLL.ChapterId;
                    hlTest.Target = "_blank";
                }
                else
                {
                    _homeWorkBLL.ChapterId = null;
                }

                if (ddlChapterVideoId.SelectedIndex > 0)
                {
                    _homeWorkBLL.ChapterVideoId = Convert.ToInt16(ddlChapterVideoId.SelectedValue);
                }
                else
                {
                    _homeWorkBLL.ChapterVideoId = null;
                }

                if (_homeWorkBLL.SubId != null && _homeWorkBLL.ChapterId != null && _homeWorkBLL.ChapterVideoId != null)
                {
                    txtSrNo.Text = _homeWorkBLL.getSrNo().ToString();
                }
                else
                {
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }
    protected void rbHomeWorkTypeMCQ_CheckedChanged(object sender, EventArgs e)
    {
        visibleHomeWorkTypeData();
    }
    protected void rbHomeWorkTypeNONMCQ_CheckedChanged(object sender, EventArgs e)
    {
        visibleHomeWorkTypeData();
    }
    protected void rbHomeWorkTypeFILE_CheckedChanged(object sender, EventArgs e)
    {
        visibleHomeWorkTypeData();
    }
    protected void rbQueTypeWholeQPaper_CheckedChanged(object sender, EventArgs e)
    {
        visibleHomeWorkTypeData();
    }

    //public void visibleHomeWorkTypeData()
    //{
    //    if (rbHomeWorkTypeMCQ.Checked)
    //    {
    //        trQuestion.Visible = true;
    //        trQuestion1.Visible = true;
    //        trQuestion2.Visible = true;
    //        trAns1_1.Visible = true;
    //        trAns1_2.Visible = true;
    //        trAns1_3.Visible = true;
    //        trAns2_1.Visible = true;
    //        trAns2_2.Visible = true;
    //        trAns2_3.Visible = true;
    //        trAns3_1.Visible = true;
    //        trAns3_2.Visible = true;
    //        trAns3_3.Visible = true;
    //        trAns4_1.Visible = true;
    //        trAns4_2.Visible = true;
    //        trAns4_3.Visible = true;
    //        trAnsDropdown.Visible = true;

    //        trQueDataType.Visible = false;
    //        trAnsTextBox.Visible = false;
    //        trNoOfFileUpload.Visible = false;

    //        trAnsSample1.Visible = false;
    //        trAnsSample2.Visible = false;
    //        trAnsSample3.Visible = false;
    //        trAnsSample4.Visible = false;

    //        trAnsSelection.Visible = true;
    //    }
    //    else if (rbHomeWorkTypeNONMCQ.Checked)
    //    {
    //        trQuestion.Visible = true;
    //        trQuestion1.Visible = true;
    //        trQuestion2.Visible = true;
    //        trAns1_1.Visible = false;
    //        trAns1_2.Visible = false;
    //        trAns1_3.Visible = false;
    //        trAns2_1.Visible = false;
    //        trAns2_2.Visible = false;
    //        trAns2_3.Visible = false;
    //        trAns3_1.Visible = false;
    //        trAns3_2.Visible = false;
    //        trAns3_3.Visible = false;
    //        trAns4_1.Visible = false;
    //        trAns4_2.Visible = false;
    //        trAns4_3.Visible = false;
    //        trAnsDropdown.Visible = false;

    //        trQueDataType.Visible = true;
    //        trAnsTextBox.Visible = true;
    //        trNoOfFileUpload.Visible = false;

    //        trAnsSample1.Visible = false;
    //        trAnsSample2.Visible = false;
    //        trAnsSample3.Visible = false;
    //        trAnsSample4.Visible = false;

    //        if (rbHomeWorkDataTypeNum.Checked == false || rbHomeWorkDataTypeChar.Checked == false)
    //            rbHomeWorkDataTypeNum.Checked = true;

    //        trAnsSelection.Visible = false;
    //    }
    //    else if (rbHomeWorkTypeFILE.Checked)
    //    {
    //        trQuestion.Visible = true;
    //        trQuestion1.Visible = true;
    //        trQuestion2.Visible = true;
    //        trAns1_1.Visible = false;
    //        trAns1_2.Visible = false;
    //        trAns1_3.Visible = false;
    //        trAns2_1.Visible = false;
    //        trAns2_2.Visible = false;
    //        trAns2_3.Visible = false;
    //        trAns3_1.Visible = false;
    //        trAns3_2.Visible = false;
    //        trAns3_3.Visible = false;
    //        trAns4_1.Visible = false;
    //        trAns4_2.Visible = false;
    //        trAns4_3.Visible = false;
    //        trAnsDropdown.Visible = false;

    //        trQueDataType.Visible = false;
    //        trAnsTextBox.Visible = false;
    //        trNoOfFileUpload.Visible = true;

    //        trAnsSample1.Visible = true;
    //        trAnsSample2.Visible = true;
    //        trAnsSample3.Visible = true;
    //        trAnsSample4.Visible = true;

    //        trAnsSelection.Visible = false;
    //    }
    //    else
    //    {
    //    }
    //}

    public void visibleHomeWorkTypeData()
    {
        trAnsSelection.Visible = true;
        fileUploadQusImg.Visible = true;
        fuUploadPDF.Visible = false;
        lblBabyPics.Text = "Select Question Image: <span style='color:red;background-color:yellow'>[500 X 100 PIXEL]</span>";

        if (rbHomeWorkTypeMCQ.Checked)
        {
            trQuestion.Visible = true;
            trQuestion1.Visible = true;
            trQuestion2.Visible = true;
            trAns1_1.Visible = true;
            trAns1_2.Visible = true;
            trAns1_3.Visible = true;
            trAns2_1.Visible = true;
            trAns2_2.Visible = true;
            trAns2_3.Visible = true;
            trAns3_1.Visible = true;
            trAns3_2.Visible = true;
            trAns3_3.Visible = true;
            trAns4_1.Visible = true;
            trAns4_2.Visible = true;
            trAns4_3.Visible = true;
            trAnsDropdown.Visible = true;

            trQueDataType.Visible = false;
            trAnsTextBox.Visible = false;
            trNoOfFileUpload.Visible = false;

            trAnsSample1.Visible = false;
            trAnsSample2.Visible = false;
            trAnsSample3.Visible = false;
            trAnsSample4.Visible = false;

            trAnsSelection.Visible = true;
        }
        else if (rbHomeWorkTypeNONMCQ.Checked)
        {
            trQuestion.Visible = true;
            trQuestion1.Visible = true;
            trQuestion2.Visible = true;
            trAns1_1.Visible = false;
            trAns1_2.Visible = false;
            trAns1_3.Visible = false;
            trAns2_1.Visible = false;
            trAns2_2.Visible = false;
            trAns2_3.Visible = false;
            trAns3_1.Visible = false;
            trAns3_2.Visible = false;
            trAns3_3.Visible = false;
            trAns4_1.Visible = false;
            trAns4_2.Visible = false;
            trAns4_3.Visible = false;
            trAnsDropdown.Visible = false;

            trQueDataType.Visible = true;
            trAnsTextBox.Visible = true;
            trNoOfFileUpload.Visible = false;

            trAnsSample1.Visible = false;
            trAnsSample2.Visible = false;
            trAnsSample3.Visible = false;
            trAnsSample4.Visible = false;

            trAnsSelection.Visible = false;
        }
        else if (rbHomeWorkTypeFILE.Checked)
        {
            trQuestion.Visible = true;
            trQuestion1.Visible = true;
            trQuestion2.Visible = true;
            trAns1_1.Visible = false;
            trAns1_2.Visible = false;
            trAns1_3.Visible = false;
            trAns2_1.Visible = false;
            trAns2_2.Visible = false;
            trAns2_3.Visible = false;
            trAns3_1.Visible = false;
            trAns3_2.Visible = false;
            trAns3_3.Visible = false;
            trAns4_1.Visible = false;
            trAns4_2.Visible = false;
            trAns4_3.Visible = false;
            trAnsDropdown.Visible = false;

            trQueDataType.Visible = false;
            trAnsTextBox.Visible = false;

            trAnsSample1.Visible = true;
            trAnsSample2.Visible = true;
            trAnsSample3.Visible = true;
            trAnsSample4.Visible = true;

            trAnsSelection.Visible = false;
        }
        else if (rbQueTypeWholeQPaper.Checked)
        {
            trQuestion.Visible = false;
            trQuestion1.Visible = false;
            trQuestion2.Visible = true;
            trAns1_1.Visible = false;
            trAns1_2.Visible = false;
            trAns1_3.Visible = false;
            trAns2_1.Visible = false;
            trAns2_2.Visible = false;
            trAns2_3.Visible = false;
            trAns3_1.Visible = false;
            trAns3_2.Visible = false;
            trAns3_3.Visible = false;
            trAns4_1.Visible = false;
            trAns4_2.Visible = false;
            trAns4_3.Visible = false;
            trAnsDropdown.Visible = false;

            trQueDataType.Visible = false;
            trAnsTextBox.Visible = false;
            trNoOfFileUpload.Visible = false;

            trAnsSample1.Visible = false;
            trAnsSample2.Visible = false;
            trAnsSample3.Visible = false;
            trAnsSample4.Visible = false;

            trAnsSelection.Visible = false;

            lblBabyPics.Text = "Select Question Paper as PDF File :";

            fileUploadQusImg.Visible = false;
            fuUploadPDF.Visible = true;
        }
        else
        {
        }
    }

    protected void rbAnsSelectionSingle_CheckedChanged(object sender, EventArgs e)
    {
        if (rbAnsSelectionSingle.Checked)
        {
            ddlAnswer.Visible = true;
            chklAns.Visible = false;
        }
        else
        {
            ddlAnswer.Visible = false;
            chklAns.Visible = true;
        }
    }
    protected void rbAnsSelectionMulti_CheckedChanged(object sender, EventArgs e)
    {
        if (rbAnsSelectionMulti.Checked)
        {
            ddlAnswer.Visible = false;
            chklAns.Visible = true;
        }
        else
        {
            ddlAnswer.Visible = true;
            chklAns.Visible = false;
        }
    }
}

