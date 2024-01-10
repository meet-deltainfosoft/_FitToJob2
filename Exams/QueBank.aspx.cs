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
using System.Text;

public partial class Exams_QueBank : System.Web.UI.Page
{
    private QueBankBLL _queBankBLL;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["QueBankId"] == null)
            {
                _queBankBLL = new QueBankBLL();
            }
            else
            {
                _queBankBLL = new QueBankBLL(Request.QueryString["QueBankId"].ToString());
            }

            //Session["_queBankBLL"] = _queBankBLL;

            for (int i = 0; i < 100; i++)
            {
                if (Session["_queBankBLL" + i + ""] == null)
                {
                    Session["_queBankBLL" + i + ""] = _queBankBLL;
                    hfMySessionHeaderValue.Value = "_queBankBLL" + i;
                    break;
                }
            }
        }
        else
        {
            Session[""] = null;
            //_queBankBLL = (QueBankBLL)Session["_queBankBLL"];
            _queBankBLL = (QueBankBLL)Session[hfMySessionHeaderValue.Value];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (MySession.UserID.ToString().ToUpper() == "aaa".ToString().ToUpper())
            {
                btnForceDelete.Visible = true;
            }
            else
            {
                btnForceDelete.Visible = false;
            }

            LoadStandard();
            txtSubject.Focus();
            ShowUnicodeChar();
            if (Request.QueryString["QueBankId"] != null)
            {
                lblTitle.Text = " - [Edit Mode]";
                LoadWebForm();
                btnDelete.Enabled = true;
                btnOKAndAddNew.Enabled = false;
            }
            else
            {
                rbQueTypeMCQ.Checked = true;
                rbQueTypeMCQ_CheckedChanged(null, null);
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

            //    _queBankBLL.ImageNameQus = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_Q" + "." + oExtension;

            //    System.IO.Stream stream = fileUploadQusImg.PostedFile.InputStream;
            //    System.Drawing.Image image = System.Drawing.Image.FromStream(stream);

            //    float height = image.PhysicalDimension.Height;
            //    float width = image.PhysicalDimension.Width;

            //    if (width > 500)
            //    {
            //        ShowErrors("er", "Question image width size must be in range of 500 pixel.");
            //    }

            //    imgqusPics.ImageUrl = fileUploadQusImg.PostedFile;
            //}

            ////image rebind code
            ///
            btnOKAndAddNew.Enabled = true;
            if (textarea1.Value.ToString().Trim().Length > 50)
            {
                QueTest.Style.Add("background", "url(" + textarea1.Value.Trim().Replace("\"", "") + ")");
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
            _queBankBLL = (QueBankBLL)Session[hfMySessionHeaderValue.Value];
            ShowUnicodeChar();
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
            dt = _queBankBLL.LoadStandard();

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
            dt = _queBankBLL.LoadSubjects(ddlStandardTextListId.SelectedValue.ToString());

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
    private void LoadChapter()
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
            dt = _queBankBLL.LoadChapter();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
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
            dt = _queBankBLL.LoadPeriodNo();

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
    public void setDataToBLLfromObject()
    {
        try
        {
            if (ddlStandardTextListId.SelectedIndex > 0)
            {
                _queBankBLL.StandardTextListId = ddlStandardTextListId.SelectedValue;
            }
            else
            {
                _queBankBLL.StandardTextListId = null;
            }

            if (ddlSubId.SelectedIndex > 0)
            {
                _queBankBLL.SubId = ddlSubId.SelectedValue.ToString();
                _queBankBLL.Subject = ddlSubId.SelectedItem.Text.Trim().ToString();
            }
            else
            {
                _queBankBLL.SubId = null;
                _queBankBLL.Subject = null;
            }
            //Question ---  QueImg
            if (txtQue.Text.Trim().Length > 0)
            {
                _queBankBLL.Que = txtQue.Text.Trim();
            }
            else
            {
                _queBankBLL.Que = null;
                _queBankBLL.QueImg = null;
            }


            //A1 --- A1Img
            if (txtA1.Text.Trim().Length > 0)
            {
                _queBankBLL.A1 = txtA1.Text.Trim();
            }
            else
            {
                _queBankBLL.A1 = null;
                _queBankBLL.A1Img = null;
            }

            //A2-- A2Img
            if (txtA2.Text.Trim().Length > 0)
            {
                _queBankBLL.A2 = txtA2.Text.Trim();
            }
            else
            {
                _queBankBLL.A2 = null;
                _queBankBLL.A2Img = null;
            }

            //A3-- A3Img
            if (txtA3.Text.Trim().Length > 0)
            {
                _queBankBLL.A3 = txtA3.Text.Trim();
            }
            else
            {
                _queBankBLL.A3 = null;
                _queBankBLL.A3Img = null;
            }

            //A4-- A4Img
            if (txtA4.Text.Trim().Length > 0)
            {
                _queBankBLL.A4 = txtA4.Text.Trim();
            }
            else
            {
                _queBankBLL.A4 = null;
                _queBankBLL.A4Img = null;
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

                _queBankBLL.ImageNameQus = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_Q" + "." + oExtension;
                _queBankBLL.IsQueChanged = true;
            }
            else if (fuUploadPDF.PostedFile != null && fuUploadPDF.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuUploadPDF.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuUploadPDF.PostedFile;

                string[] getExtenstion = fuUploadPDF.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuUploadPDF.PostedFile.ContentLength);

                _queBankBLL.ImageNameQus = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_Q" + "." + oExtension;
                _queBankBLL.IsQueChanged = true;
            }
            else if (textarea1.Value.ToString().Trim().Length > 50)
            {
                _queBankBLL.ImageNameQus = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_Q" + ".png";
                _queBankBLL.IsQueChanged = true;
            }

            if (fileUploadA1Img.PostedFile != null && fileUploadA1Img.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadA1Img.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadA1Img.PostedFile;

                string[] getExtenstion = fileUploadA1Img.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadA1Img.PostedFile.ContentLength);

                _queBankBLL.ImageNameA1 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_A1" + "." + oExtension;
                _queBankBLL.IsA1Changed = true;
            }
            else if (textarea2.Value.ToString().Trim().Length > 50)
            {
                _queBankBLL.ImageNameA1 = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_A1" + ".png";
                _queBankBLL.IsA1Changed = true;
            }
            if (fileUploadA2Img.PostedFile != null && fileUploadA2Img.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadA2Img.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadA2Img.PostedFile;

                string[] getExtenstion = fileUploadA2Img.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadA2Img.PostedFile.ContentLength);

                _queBankBLL.ImageNameA2 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_A2" + "." + oExtension;
                _queBankBLL.IsA2Changed = true;
            }
            else if (textarea3.Value.ToString().Trim().Length > 50)
            {
                _queBankBLL.ImageNameA2 = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_A2" + ".png";
                _queBankBLL.IsA2Changed = true;
            }
            if (fileUploadA3Img.PostedFile != null && fileUploadA3Img.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadA3Img.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadA3Img.PostedFile;

                string[] getExtenstion = fileUploadA3Img.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadA3Img.PostedFile.ContentLength);

                _queBankBLL.ImageNameA3 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_A3" + "." + oExtension;
                _queBankBLL.IsA3Changed = true;
            }
            else if (textarea4.Value.ToString().Trim().Length > 50)
            {
                _queBankBLL.ImageNameA3 = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_A3" + ".png";
                _queBankBLL.IsA3Changed = true;
            }
            if (fileUploadA4Img.PostedFile != null && fileUploadA4Img.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadA4Img.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadA4Img.PostedFile;

                string[] getExtenstion = fileUploadA4Img.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadA4Img.PostedFile.ContentLength);

                _queBankBLL.ImageNameA4 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_A4" + "." + oExtension;
                _queBankBLL.IsA4Changed = true;
            }
            else if (textarea5.Value.ToString().Trim().Length > 50)
            {
                _queBankBLL.ImageNameA4 = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_A4" + ".png";
                _queBankBLL.IsA4Changed = true;
            }

            if (ddlChapterId.SelectedIndex > 0)
                _queBankBLL.ChapterId = ddlChapterId.SelectedValue;
            else
                _queBankBLL.ChapterId = null;

            if (ddlPeriodNo.SelectedIndex > 0)
                _queBankBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);
            else
                _queBankBLL.PeriodNo = null;

            if (txtSrNo.Text.Trim().Length > 0)
                _queBankBLL.SrNo = Convert.ToInt16(txtSrNo.Text.Trim().ToString());
            else
                _queBankBLL.SrNo = null;

            if (rbQueTypeMCQ.Checked)
            {
                _queBankBLL.QueType = "MCQ";

                if (rbAnsSelectionSingle.Checked)
                {
                    if (ddlAnswer.SelectedIndex > 0)
                    {
                        _queBankBLL.Ans = (ddlAnswer.SelectedValue).ToString();
                    }
                    else
                    {
                        _queBankBLL.Ans = null;
                    }

                    _queBankBLL.AnsSelection = "SINGLE";
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
                        _queBankBLL.Ans = Id1;
                    else
                        _queBankBLL.Ans = null;

                    _queBankBLL.AnsSelection = "MULTIPLE";
                }
                else
                {
                    _queBankBLL.Ans = null;
                    _queBankBLL.AnsSelection = null;
                }
            }
            else if (rbQueTypeNONMCQ.Checked)
            {
                _queBankBLL.QueType = "NONMCQ";

                if (txtAns.Text.Trim().Length > 0)
                    _queBankBLL.Ans = txtAns.Text.ToString();
                else
                    _queBankBLL.Ans = null;

                _queBankBLL.AnsSelection = null;
            }
            else if (rbQueTypeFILE.Checked)
            {
                _queBankBLL.QueType = "FILE";
                _queBankBLL.Ans = null;
                _queBankBLL.AnsSelection = null;
            }
            else if (rbQueTypeWholeQPaper.Checked)
            {
                _queBankBLL.QueType = "PDF";
                _queBankBLL.Ans = null;
                _queBankBLL.AnsSelection = null;
            }
            else
            {
                _queBankBLL.Ans = null;
                _queBankBLL.QueType = null;
                _queBankBLL.AnsSelection = null;
            }

            if (rbQueDataTypeNum.Checked)
            {
                _queBankBLL.QueDataType = "NUM";
            }
            else if (rbQueDataTypeChar.Checked)
            {
                _queBankBLL.QueDataType = "CHAR";
            }
            else
            {
                _queBankBLL.QueDataType = null;
            }

            if (txtRightMarks.Text.Trim().Length > 0)
                _queBankBLL.RightMarks = Convert.ToDecimal(txtRightMarks.Text);
            else
                _queBankBLL.RightMarks = null;

            if (txtWrongMarks.Text.Trim().Length > 0)
                _queBankBLL.WrongMarks = Convert.ToDecimal(txtWrongMarks.Text);
            else
                _queBankBLL.WrongMarks = null;

            if (txtNonMarks.Text.Trim().Length > 0)
                _queBankBLL.NonMarks = Convert.ToDecimal(txtNonMarks.Text);
            else
                _queBankBLL.NonMarks = null;

            if (ddlNoOfFile.Visible)
                _queBankBLL.NoOfFile = Convert.ToInt16(ddlNoOfFile.SelectedValue.ToString());
            else
                _queBankBLL.NoOfFile = null;

            if (fuSampleAns1.PostedFile != null && fuSampleAns1.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuSampleAns1.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuSampleAns1.PostedFile;

                string[] getExtenstion = fuSampleAns1.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuSampleAns1.PostedFile.ContentLength);

                _queBankBLL.SampleAns1 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_S1" + "." + oExtension;
                _queBankBLL.IsSampleAns1Changed = true;
            }

            if (fuSampleAns2.PostedFile != null && fuSampleAns2.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuSampleAns2.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuSampleAns2.PostedFile;

                string[] getExtenstion = fuSampleAns2.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuSampleAns2.PostedFile.ContentLength);

                _queBankBLL.SampleAns2 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_S2" + "." + oExtension;
                _queBankBLL.IsSampleAns2Changed = true;
            }

            if (fuSampleAns3.PostedFile != null && fuSampleAns3.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuSampleAns3.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuSampleAns3.PostedFile;

                string[] getExtenstion = fuSampleAns3.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuSampleAns3.PostedFile.ContentLength);

                _queBankBLL.SampleAns3 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_S3" + "." + oExtension;
                _queBankBLL.IsSampleAns3Changed = true;
            }

            if (fuSampleAns4.PostedFile != null && fuSampleAns4.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuSampleAns4.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuSampleAns4.PostedFile;

                string[] getExtenstion = fuSampleAns4.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuSampleAns4.PostedFile.ContentLength);

                _queBankBLL.SampleAns4 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_S4" + "." + oExtension;
                _queBankBLL.IsSampleAns4Changed = true;
            }

            if (txtHashTag.Text.Trim().Length > 0)
                _queBankBLL.HashTag = txtHashTag.Text.Trim();
            else
                _queBankBLL.HashTag = null;

            if (rbLevelOfQueEasy.Checked)
                _queBankBLL.LevelofQue = "Easy";
            else if (rbLevelOfQueMedium.Checked)
                _queBankBLL.LevelofQue = "Medium";
            else if (rbLevelOfQueHard.Checked)
                _queBankBLL.LevelofQue = "Hard";
            else
                _queBankBLL.LevelofQue = "Challenging";

            if (ddlChapterVideoId.SelectedIndex > 0)
            {
                _queBankBLL.ChapterVideoId = Convert.ToInt16(ddlChapterVideoId.SelectedValue.ToString());
            }
            else
            {
                _queBankBLL.ChapterVideoId = null;
            }
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
                _queBankBLL.Save();

                if (fileUploadQusImg.PostedFile != null && fileUploadQusImg.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadQusImg.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadQusImg.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.ImageNameQus);
                }
                else if (fuUploadPDF.PostedFile != null && fuUploadPDF.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuUploadPDF.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuUploadPDF.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.ImageNameQus);
                }
                else if (textarea1.Value.ToString().Trim().Length > 50 && _queBankBLL.ImageNameQus != null)
                {
                    string imgPath = _queBankBLL.ImageNameQus;

                    byte[] imageBytes = Convert.FromBase64String(textarea1.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }

                if (fileUploadA1Img.PostedFile != null && fileUploadA1Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA1Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA1Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.ImageNameA1);
                }
                else if (textarea2.Value.ToString().Trim().Length > 50 && _queBankBLL.ImageNameA1 != null)
                {
                    string imgPath = _queBankBLL.ImageNameA1;

                    byte[] imageBytes = Convert.FromBase64String(textarea2.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA2Img.PostedFile != null && fileUploadA2Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA2Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA2Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.ImageNameA2);
                }
                else if (textarea3.Value.ToString().Trim().Length > 50 && _queBankBLL.ImageNameA2 != null)
                {
                    string imgPath = _queBankBLL.ImageNameA2;

                    byte[] imageBytes = Convert.FromBase64String(textarea3.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA3Img.PostedFile != null && fileUploadA3Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA3Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA3Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.ImageNameA3);
                }
                else if (textarea4.Value.ToString().Trim().Length > 50 && _queBankBLL.ImageNameA3 != null)
                {
                    string imgPath = _queBankBLL.ImageNameA3;

                    byte[] imageBytes = Convert.FromBase64String(textarea4.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA4Img.PostedFile != null && fileUploadA4Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA4Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA4Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.ImageNameA4);
                }
                else if (textarea5.Value.ToString().Trim().Length > 50 && _queBankBLL.ImageNameA4 != null)
                {
                    string imgPath = _queBankBLL.ImageNameA4;

                    byte[] imageBytes = Convert.FromBase64String(textarea5.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }

                if (fuSampleAns1.PostedFile != null && fuSampleAns1.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuSampleAns1.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuSampleAns1.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.SampleAns1);
                }

                if (fuSampleAns2.PostedFile != null && fuSampleAns2.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuSampleAns2.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuSampleAns2.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.SampleAns2);
                }

                if (fuSampleAns3.PostedFile != null && fuSampleAns3.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuSampleAns3.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuSampleAns3.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.SampleAns3);
                }

                if (fuSampleAns4.PostedFile != null && fuSampleAns4.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuSampleAns4.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuSampleAns4.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.SampleAns4);
                }

                if (Request.QueryString["QueBankId"] == null)
                {
                    Reset();
                    //Session["_queBankBLL"] = null;
                    //Session["_queBankBLL"] = new QueBankBLL();
                    //_queBankBLL = (QueBankBLL)Session["_queBankBLL"];

                    Session[hfMySessionHeaderValue.Value] = null;
                    for (int i = 0; i < 100; i++)
                    {
                        if (Session["_queBankBLL" + i + ""] == null)
                        {
                            Session["_queBankBLL" + i + ""] = _queBankBLL;
                            hfMySessionHeaderValue.Value = "_queBankBLL" + i;
                            break;
                        }
                    }

                    Session[hfMySessionHeaderValue.Value] = new QueBankBLL();

                    _queBankBLL = (QueBankBLL)Session[hfMySessionHeaderValue.Value];

                    txtSubject.Focus();
                }
                else
                {
                    //Session["_queBankBLL"] = null;
                    Session[hfMySessionHeaderValue.Value] = null;
                    Response.Redirect("QueBanks.aspx");
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
                _queBankBLL.Save();

                if (fileUploadQusImg.PostedFile != null && fileUploadQusImg.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadQusImg.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadQusImg.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.ImageNameQus);
                }
                else if (fuUploadPDF.PostedFile != null && fuUploadPDF.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuUploadPDF.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuUploadPDF.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.ImageNameQus);
                }
                else if (textarea1.Value.ToString().Trim().Length > 50 && _queBankBLL.ImageNameQus != null)
                {
                    string imgPath = _queBankBLL.ImageNameQus;

                    byte[] imageBytes = Convert.FromBase64String(textarea1.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }


                if (fileUploadA1Img.PostedFile != null && fileUploadA1Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA1Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA1Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.ImageNameA1);
                }
                else if (textarea2.Value.ToString().Trim().Length > 50 && _queBankBLL.ImageNameA1 != null)
                {
                    string imgPath = _queBankBLL.ImageNameA1;

                    byte[] imageBytes = Convert.FromBase64String(textarea2.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA2Img.PostedFile != null && fileUploadA2Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA2Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA2Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.ImageNameA2);
                }
                else if (textarea3.Value.ToString().Trim().Length > 50 && _queBankBLL.ImageNameA2 != null)
                {
                    string imgPath = _queBankBLL.ImageNameA2;

                    byte[] imageBytes = Convert.FromBase64String(textarea3.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA3Img.PostedFile != null && fileUploadA3Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA3Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA3Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.ImageNameA3);
                }
                else if (textarea4.Value.ToString().Trim().Length > 50 && _queBankBLL.ImageNameA3 != null)
                {
                    string imgPath = _queBankBLL.ImageNameA3;

                    byte[] imageBytes = Convert.FromBase64String(textarea4.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA4Img.PostedFile != null && fileUploadA4Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA4Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA4Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBankBLL.ImageNameA4);
                }
                else if (textarea5.Value.ToString().Trim().Length > 50 && _queBankBLL.ImageNameA4 != null)
                {
                    string imgPath = _queBankBLL.ImageNameA4;

                    byte[] imageBytes = Convert.FromBase64String(textarea5.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }

                if (Request.QueryString["QueBankId"] == null)
                {
                    string str_ddlStandardTextListId, str_ddlSubId, str_ddlChapterId;
                    str_ddlStandardTextListId = ddlStandardTextListId.SelectedValue;
                    str_ddlSubId = ddlSubId.SelectedValue.ToString();
                    str_ddlChapterId = ddlChapterId.SelectedValue;

                    //Reset();
                    ResetWithOutCheckBox();

                    //Session["_queBankBLL"] = null;
                    //Session["_queBankBLL"] = new QueBankBLL();
                    //_queBankBLL = (QueBankBLL)Session["_queBankBLL"];

                    Session[hfMySessionHeaderValue.Value] = null;
                    for (int i = 0; i < 100; i++)
                    {
                        if (Session["_queBankBLL" + i + ""] == null)
                        {
                            Session["_queBankBLL" + i + ""] = _queBankBLL;
                            hfMySessionHeaderValue.Value = "_queBankBLL" + i;
                            break;
                        }
                    }

                    Session[hfMySessionHeaderValue.Value] = new QueBankBLL();

                    _queBankBLL = (QueBankBLL)Session[hfMySessionHeaderValue.Value];

                    ddlStandardTextListId.SelectedValue = str_ddlStandardTextListId;
                    ddlStandardTextListId_SelectedIndexChanged(sender, new EventArgs());

                    ddlSubId.SelectedValue = str_ddlSubId;
                    ddlSubId_SelectedIndexChanged(sender, new EventArgs());
                    ddlChapterId.SelectedValue = str_ddlChapterId;
                    ddlChapterId_SelectedIndexChanged(sender, new EventArgs());
                    visibleQueTypeData();
                    txtQue.Focus();
                }
                else
                {
                    //Session["_queBankBLL"] = null;
                    Session[hfMySessionHeaderValue.Value] = null;
                    Response.Redirect("QueBanks.aspx");
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

        SortedList sl = _queBankBLL.Validate();

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

        //Question
        if (key == "Question")
        {
            lblQue.CssClass = "error";
            txtQue.CssClass = "error";
        }
        //Question

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

        lblQue.CssClass = "";
        txtQue.CssClass = "";

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
        txtQue.Text = "";
        txtA1.Text = "";
        txtA2.Text = "";
        txtA3.Text = "";
        txtA4.Text = "";
        ddlAnswer.SelectedIndex = 0;
        txtQue.Focus();

        ddlChapterId.SelectedIndex = 0;
        ddlPeriodNo.Items.Clear();
        ddlSubId.SelectedIndex = 0;

        txtSrNo.Text = "";

        rbQueTypeMCQ.Checked = false;
        rbQueTypeNONMCQ.Checked = false;
        rbQueTypeFILE.Checked = false;

        rbQueDataTypeNum.Checked = false;
        rbQueDataTypeChar.Checked = false;

        txtAns.Text = "";

        txtRightMarks.Text = "";
        txtWrongMarks.Text = "";
        txtNonMarks.Text = "";

        ddlNoOfFile.SelectedIndex = 0;

        rbAnsSelectionSingle.Checked = true;
        chklAns.ClearSelection();
        rbLevelOfQueEasy.Checked = true;
        txtHashTag.Text = "";

        ddlChapterVideoId.SelectedIndex = 0;
    }

    private void ResetWithOutCheckBox()
    {
        txtSubject.Text = "";
        txtQue.Text = "";
        txtA1.Text = "";
        txtA2.Text = "";
        txtA3.Text = "";
        txtA4.Text = "";
        ddlAnswer.SelectedIndex = 0;
        txtQue.Focus();

        ddlChapterId.SelectedIndex = 0;
        ddlPeriodNo.Items.Clear();
        ddlSubId.SelectedIndex = 0;

        txtSrNo.Text = "";

        //rbQueTypeMCQ.Checked = false;
        //rbQueTypeNONMCQ.Checked = false;
        //rbQueTypeFILE.Checked = false;

        //rbQueDataTypeNum.Checked = false;
        //rbQueDataTypeChar.Checked = false;

        txtAns.Text = "";

        txtRightMarks.Text = "";
        txtWrongMarks.Text = "";
        txtNonMarks.Text = "";

        ddlNoOfFile.SelectedIndex = 0;
        rbAnsSelectionSingle.Checked = true;
        chklAns.ClearSelection();
        rbLevelOfQueEasy.Checked = true;
        txtHashTag.Text = "";
    }

    private void LoadWebForm()
    {
        trFramePaper.Visible = false;
        if (_queBankBLL.StandardTextListId != null)
        {
            ddlStandardTextListId.SelectedValue = _queBankBLL.StandardTextListId;
            ddlStandardTextListId_SelectedIndexChanged(null, null);
        }

        //Subject
        if (_queBankBLL.SubId != null)
        {
            //hfSubId.Value = _queBankBLL.SubId;
            //txtSubject.Text = _queBankBLL.Subject;

            ddlSubId.SelectedValue = _queBankBLL.SubId;
            ddlSubId_SelectedIndexChanged(null, null);
        }

        if (_queBankBLL.ChapterId != null)
        {
            ddlChapterId.SelectedValue = _queBankBLL.ChapterId;
            ddlChapterId_SelectedIndexChanged(null, null);
        }


        if (_queBankBLL.PeriodNo != null)
        {
            ddlPeriodNo.SelectedValue = Convert.ToDecimal(_queBankBLL.PeriodNo).ToString("#.##");
            ddlPeriodNo_SelectedIndexChanged(null, null);
        }

        if (_queBankBLL.ChapterVideoId != null)
        {
            ddlChapterVideoId.SelectedValue = _queBankBLL.ChapterVideoId.ToString();
        }

        //Question
        if (_queBankBLL.Que != null)
            txtQue.Text = _queBankBLL.Que;

        //A1
        if (_queBankBLL.A1 != null)
            txtA1.Text = _queBankBLL.A1;

        //A2
        if (_queBankBLL.A2 != null)
            txtA2.Text = _queBankBLL.A2;

        //A3
        if (_queBankBLL.A3 != null)
            txtA3.Text = _queBankBLL.A3;

        //A4
        if (_queBankBLL.A4 != null)
            txtA4.Text = _queBankBLL.A4;

        //Answer

        if (_queBankBLL.QueType != null)
        {
            if (_queBankBLL.QueType.ToString().ToUpper() == "MCQ".ToString().ToUpper())
            {
                rbQueTypeMCQ.Checked = true;
                rbQueTypeNONMCQ.Checked = false;
                rbQueTypeMCQ_CheckedChanged(null, null);

                if (_queBankBLL.AnsSelection != null)
                {
                    if (_queBankBLL.AnsSelection.ToString().ToUpper() == "SINGLE".ToString().ToUpper())
                    {
                        rbAnsSelectionSingle.Checked = true;
                        rbAnsSelectionMulti.Checked = false;

                        rbAnsSelectionSingle_CheckedChanged(null, null);
                    }
                    else if (_queBankBLL.AnsSelection.ToString().ToUpper() == "MULTIPLE".ToString().ToUpper())
                    {
                        rbAnsSelectionSingle.Checked = false;
                        rbAnsSelectionMulti.Checked = true;

                        rbAnsSelectionMulti_CheckedChanged(null, null);
                    }
                }

                if (rbAnsSelectionSingle.Checked)
                {
                    if (_queBankBLL.Ans != null)
                    {
                        ddlAnswer.SelectedValue = (_queBankBLL.Ans).ToString();
                    }
                }
                else
                {
                    if (_queBankBLL.Ans != null)
                    {
                        string[] answer = _queBankBLL.Ans.ToString().Split(',');

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
                                if (li.Value.ToString() == _queBankBLL.Ans.ToString().ToUpper())
                                {
                                    li.Selected = true;
                                }
                            }
                        }
                    }
                }
            }
            else if (_queBankBLL.QueType.ToString().ToUpper() == "NONMCQ".ToString().ToUpper())
            {
                rbQueTypeMCQ.Checked = false;
                rbQueTypeNONMCQ.Checked = true;
                rbQueTypeMCQ_CheckedChanged(null, null);

                if (_queBankBLL.Ans != null)
                {
                    txtAns.Text = (_queBankBLL.Ans).ToString();
                }
            }
            else if (_queBankBLL.QueType.ToString().ToUpper() == "FILE".ToString().ToUpper())
            {
                rbQueTypeFILE.Checked = true;
                rbQueTypeFILE_CheckedChanged(null, null);
            }
            else if (_queBankBLL.QueType.ToString().ToUpper() == "PDF".ToString().ToUpper())
            {
                rbQueTypeWholeQPaper.Checked = true;
                rbQueTypeWholeQPaper_CheckedChanged(null, null);

                if (_queBankBLL.ImageNameQus != null)
                {
                    trFramePaper.Visible = true;
                    iframepdf.Attributes.Add("src", "http://" + _queBankBLL.ImageNameQus);
                }
            }
        }

        if (_queBankBLL.ImageNameQus != null)
        {
            imgqusPics.ImageUrl = _queBankBLL.ImageNameQus;
        }
        if (_queBankBLL.ImageNameA1 != null)
        {
            imga1Pics.ImageUrl = _queBankBLL.ImageNameA1;
        }
        if (_queBankBLL.ImageNameA2 != null)
        {
            imga2Pics.ImageUrl = _queBankBLL.ImageNameA2;
        }
        if (_queBankBLL.ImageNameA3 != null)
        {
            imga3Pics.ImageUrl = _queBankBLL.ImageNameA3;
        }
        if (_queBankBLL.ImageNameA4 != null)
        {
            imga4Pics.ImageUrl = _queBankBLL.ImageNameA4;
        }

        if (_queBankBLL.SrNo != null)
        {
            txtSrNo.Text = Convert.ToString(_queBankBLL.SrNo);
        }

        if (_queBankBLL.QueDataType != null)
        {
            if (_queBankBLL.QueDataType.ToString().ToUpper() == "NUM".ToString().ToUpper())
            {
                rbQueDataTypeNum.Checked = true;
                rbQueDataTypeChar.Checked = false;
            }
            else if (_queBankBLL.QueDataType.ToString().ToUpper() == "CHAR".ToString().ToUpper())
            {
                rbQueDataTypeNum.Checked = false;
                rbQueDataTypeChar.Checked = true;
            }
        }

        if (_queBankBLL.RightMarks != null)
        {
            txtRightMarks.Text = Convert.ToDecimal(_queBankBLL.RightMarks).ToString("0.##");
        }

        if (_queBankBLL.WrongMarks != null)
        {
            txtWrongMarks.Text = Convert.ToDecimal(_queBankBLL.WrongMarks).ToString("0.##");
        }

        if (_queBankBLL.NonMarks != null)
        {
            txtNonMarks.Text = Convert.ToDecimal(_queBankBLL.NonMarks).ToString("0.##");
        }

        if (_queBankBLL.NoOfFile != null)
        {
            ddlNoOfFile.SelectedValue = _queBankBLL.NoOfFile.ToString();
        }

        if (_queBankBLL.SampleAns1 != null)
        {
            hlSampleAns1.NavigateUrl = _queBankBLL.SampleAns1.ToString();
            hlSampleAns1.Target = "_blank";
        }

        if (_queBankBLL.SampleAns2 != null)
        {
            hlSampleAns2.NavigateUrl = _queBankBLL.SampleAns2.ToString();
            hlSampleAns2.Target = "_blank";
        }

        if (_queBankBLL.SampleAns3 != null)
        {
            hlSampleAns3.NavigateUrl = _queBankBLL.SampleAns3.ToString();
            hlSampleAns3.Target = "_blank";
        }

        if (_queBankBLL.SampleAns4 != null)
        {
            hlSampleAns4.NavigateUrl = _queBankBLL.SampleAns4.ToString();
            hlSampleAns4.Target = "_blank";
        }

        if (_queBankBLL.HashTag != null)
            txtHashTag.Text = _queBankBLL.HashTag.ToString();

        if (_queBankBLL.LevelofQue != null)
        {
            if (_queBankBLL.LevelofQue.ToString().ToUpper() == "Easy".ToString().ToUpper())
            {
                rbLevelOfQueEasy.Checked = true;
                rbLevelOfQueMedium.Checked = false;
                rbLevelOfQueHard.Checked = false;
                rbLevelOfQueChallenging.Checked = false;
            }
            else if (_queBankBLL.LevelofQue.ToString().ToUpper() == "Medium".ToString().ToUpper())
            {
                rbLevelOfQueEasy.Checked = false;
                rbLevelOfQueMedium.Checked = true;
                rbLevelOfQueHard.Checked = false;
                rbLevelOfQueChallenging.Checked = false;
            }
            else if (_queBankBLL.LevelofQue.ToString().ToUpper() == "Hard".ToString().ToUpper())
            {
                rbLevelOfQueEasy.Checked = false;
                rbLevelOfQueMedium.Checked = false;
                rbLevelOfQueHard.Checked = true;
                rbLevelOfQueChallenging.Checked = false;
            }
            else if (_queBankBLL.LevelofQue.ToString().ToUpper() == "Challenging".ToString().ToUpper())
            {
                rbLevelOfQueEasy.Checked = false;
                rbLevelOfQueMedium.Checked = false;
                rbLevelOfQueHard.Checked = false;
                rbLevelOfQueChallenging.Checked = true;
            }
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _queBankBLL.Delete(Request.QueryString["QueBankId"].ToString());
            HideErrors();
            //Session["_queBankBLL"] = null;
            Session[hfMySessionHeaderValue.Value] = null;
            Reset();
            Response.Redirect("QueBanks.aspx");
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Session["_queBankBLL"] = null;
        Session[hfMySessionHeaderValue.Value] = null;

        if (Request.QueryString["QueBankId"] == null)
        {
            Response.Redirect("~/General/Default.aspx");
        }
        else
        {
            Response.Redirect("QueBanks.aspx");
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
                _queBankBLL.SubId = ddlSubId.SelectedValue;
                LoadChapter();
            }
            else
            {
                _queBankBLL.SubId = null;
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
            if (Request.QueryString["QueBankId"] == null)
            {
                if (ddlStandardTextListId.SelectedIndex > 0)
                    _queBankBLL.StandardTextListId = ddlStandardTextListId.SelectedValue;
                else
                    _queBankBLL.StandardTextListId = null;

                if (ddlSubId.SelectedIndex > 0)
                    _queBankBLL.SubId = ddlSubId.SelectedValue;
                else
                    _queBankBLL.SubId = null;

                //if (ddlChapterId.SelectedIndex > 0)
                //{
                //    _queBankBLL.ChapterId = ddlChapterId.SelectedValue;
                //    hlTest.NavigateUrl = "../Exams/ViewResultDetailMasterSheet.aspx?ChapterId=" + _queBankBLL.ChapterId;
                //    hlTest.Target = "_blank";
                //}
                //else
                //{
                //    _queBankBLL.ChapterId = null;
                //}

                //if (_queBankBLL.SubId != null && _queBankBLL.ChapterId != null)
                //{
                //    txtSrNo.Text = _queBankBLL.getSrNo().ToString();
                //}
                //else
                //{
                //}
            }

            if (ddlChapterId.SelectedIndex > 0)
            {
                _queBankBLL.ChapterId = ddlChapterId.SelectedValue;
                LoadPeriodNo();
            }
            else
                ddlPeriodNo.Items.Clear();
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }
    protected void rbQueTypeMCQ_CheckedChanged(object sender, EventArgs e)
    {
        visibleQueTypeData();
    }
    protected void rbQueTypeNONMCQ_CheckedChanged(object sender, EventArgs e)
    {
        visibleQueTypeData();
    }
    protected void rbQueTypeFILE_CheckedChanged(object sender, EventArgs e)
    {
        visibleQueTypeData();
    }
    protected void rbQueTypeWholeQPaper_CheckedChanged(object sender, EventArgs e)
    {
        visibleQueTypeData();
    }

    public void visibleQueTypeData()
    {
        trFramePaper.Visible = false;
        trAnsSelection.Visible = true;
        trQueTest.Visible = true;
        trTextarea1.Visible = true;
        fileUploadQusImg.Visible = true;
        fuUploadPDF.Visible = false;
        lblBabyPics.Text = "Select Question Image: <span style='color:red;background-color:yellow'>[500 X 100 PIXEL]</span>";
        if (rbQueTypeMCQ.Checked)
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

            //txtRightMarks.Text = "1";
            //txtWrongMarks.Text = "-0.25";
            //txtNonMarks.Text = "0";

            trAnsSelection.Visible = true;
        }
        else if (rbQueTypeNONMCQ.Checked)
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

            //txtRightMarks.Text = "4";
            //txtWrongMarks.Text = "-1";
            //txtNonMarks.Text = "0";
            if (rbQueDataTypeNum.Checked == false || rbQueDataTypeChar.Checked == false)
                rbQueDataTypeNum.Checked = true;

            trAnsSelection.Visible = false;
        }
        else if (rbQueTypeFILE.Checked)
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
            trNoOfFileUpload.Visible = true;

            trAnsSample1.Visible = true;
            trAnsSample2.Visible = true;
            trAnsSample3.Visible = true;
            trAnsSample4.Visible = true;

            //txtRightMarks.Text = "4";
            //txtWrongMarks.Text = "-1";
            //txtNonMarks.Text = "0";

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

            //txtRightMarks.Text = "0";
            //txtWrongMarks.Text = "0";
            //txtNonMarks.Text = "0";

            trAnsSelection.Visible = false;
            trQueTest.Visible = false;
            trTextarea1.Visible = false;

            lblBabyPics.Text = "Select Question Paper :";

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

    protected void ShowUnicodeChar()
    {
        try
        {
            Literal ltCases = new Literal();
            StringBuilder strbuUser = new StringBuilder();
            DataTable dt = new DataTable();
            string[] Symbols = { "Maths Symbols", "Roots", "SETS", "SubScript", "SubSet", "SuperScript" };
            if (Symbols != null)
            {
                for (int i = 0; i < Symbols.Length; i++)
                {
                    dt = _queBankBLL.TextList(Symbols[i]);
                    if (dt.Rows.Count > 0)
                    {
                        strbuUser.Append("<br />");
                        strbuUser.Append("<table style='width:100%;' class='table table-responsive' border=1>");
                        strbuUser.Append("<tr>");
                        strbuUser.Append("<th style='border:0px solid gray;text-align:left;font-size:13px;' colspan='" + dt.Rows.Count + "'> " + Symbols[i].ToString() + "</th>");
                        strbuUser.Append("</tr>");
                        strbuUser.Append("<tr>");
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            strbuUser.Append("<td style='border:0px solid;text-align:center;font-size:20px;'> " + dt.Rows[j]["Text"].ToString() + "</td>");
                            if (j == 13)
                            {
                                strbuUser.Append("</tr>");
                                strbuUser.Append("<tr>");
                            }
                        }
                        strbuUser.Append("</tr>");
                        strbuUser.Append("</table>");
                    }
                }
            }
            ltCases.Text = strbuUser.ToString();
            phdUnicode.Controls.Clear();
            phdUnicode.Controls.Add(ltCases);
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }
    protected void ddlPeriodNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (Request.QueryString["QueBankId"] == null)
            //{
            string ChapterId = "";

            if (ddlPeriodNo.SelectedIndex > 0)
                _queBankBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);
            else
                _queBankBLL.PeriodNo = null;

            if (_queBankBLL.PeriodNo != null)
            {
                ChapterId = _queBankBLL.GetChapterId(_queBankBLL.ChapterId, Convert.ToDecimal(_queBankBLL.PeriodNo).ToString("#.##"), ddlChapterId.SelectedValue.ToString());
            }
            else
            {
                ChapterId = "";
            }

            if (ChapterId.Length > 0)
            {
                //_queBankBLL.ChapterId = ddlChapterId.SelectedValue;
                hlTest.NavigateUrl = "../Exams/ViewResultDetailMasterSheet.aspx?ChapterId=" + ChapterId;
                hlTest.Target = "_blank";
            }
            else
            {
                //_queBankBLL.ChapterId = null;
            }

            LoadChapterVideos();

            //if (_queBankBLL.SubId != null && ChapterId.ToString().Length > 0)
            //{
            //    txtSrNo.Text = _queBankBLL.getSrNo(ChapterId).ToString();
            //    _queBankBLL.ChapterId = ChapterId;
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
            dt = _queBankBLL.LoadChapterVideo();

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
            if (Request.QueryString["QueBankId"] == null)
            {
                if (ddlStandardTextListId.SelectedIndex > 0)
                    _queBankBLL.StandardTextListId = ddlStandardTextListId.SelectedValue;
                else
                    _queBankBLL.StandardTextListId = null;

                if (ddlSubId.SelectedIndex > 0)
                    _queBankBLL.SubId = ddlSubId.SelectedValue;
                else
                    _queBankBLL.SubId = null;

                if (ddlChapterId.SelectedIndex > 0)
                {
                    _queBankBLL.ChapterId = ddlChapterId.SelectedValue;
                    hlTest.NavigateUrl = "../Exams/ViewResultDetailMasterSheet.aspx?ChapterId=" + _queBankBLL.ChapterId;
                    hlTest.Target = "_blank";
                }
                else
                {
                    _queBankBLL.ChapterId = null;
                }

                if (ddlChapterVideoId.SelectedIndex > 0)
                {
                    _queBankBLL.ChapterVideoId = Convert.ToInt16(ddlChapterVideoId.SelectedValue);
                }
                else
                {
                    _queBankBLL.ChapterVideoId = null;
                }

                if (_queBankBLL.SubId != null && _queBankBLL.ChapterId != null && _queBankBLL.ChapterVideoId != null)
                {
                    txtSrNo.Text = _queBankBLL.getSrNo(_queBankBLL.ChapterId.ToString(), _queBankBLL.ChapterVideoId.ToString()).ToString();
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

    protected void btnForceDelete_Click(object sender, EventArgs e)
    {
        _queBankBLL.DeleteForce(Request.QueryString["QueBankId"].ToString());
        HideErrors();
        //Session["_queBankBLL"] = null;
        Session[hfMySessionHeaderValue.Value] = null;
        Reset();
        Response.Redirect("QueBanks.aspx");
    }
}