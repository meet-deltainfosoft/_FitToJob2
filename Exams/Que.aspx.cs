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

public partial class Exams_Que : System.Web.UI.Page
{
    private QueBLL _queBLL;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["QueId"] == null)
            {
                _queBLL = new QueBLL();
            }
            else
            {
                _queBLL = new QueBLL(Request.QueryString["QueId"].ToString());
            }

            //Session["_queBLL"] = _queBLL;

            for (int i = 0; i < 100; i++)
            {
                if (Session["_queBLL" + i + ""] == null)
                {
                    Session["_queBLL" + i + ""] = _queBLL;
                    hfMySessionHeaderValue.Value = "_queBLL" + i;
                    break;
                }
            }
        }
        else
        {
            Session[""] = null;
            //_queBLL = (QueBLL)Session["_queBLL"];
            _queBLL = (QueBLL)Session[hfMySessionHeaderValue.Value];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadStandard();
            //LoadSubjects();
            txtSubject.Focus();
            ShowUnicodeChar();
            if (Request.QueryString["QueId"] != null)
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

            //    _queBLL.ImageNameQus = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_Q" + "." + oExtension;

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
            _queBLL = (QueBLL)Session[hfMySessionHeaderValue.Value];
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
            dt = _queBLL.LoadStandard();

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
    //private void LoadSubjects()
    //{
    //    try
    //    {
    //        ListItem li = new ListItem();

    //        ddlSubId.Items.Clear();

    //        li.Text = "<Select>";
    //        li.Value = "0";
    //        ddlSubId.Items.Add(li);

    //        li = null;

    //        DataTable dt = new DataTable();
    //        dt = _queBLL.LoadSubjects();

    //        foreach (DataRow dtr in dt.Rows)
    //        {
    //            li = new ListItem();

    //            li.Text = dtr[1].ToString();
    //            li.Value = dtr[0].ToString();
    //            ddlSubId.Items.Add(li);

    //            li = null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowErrors("err", ex.Message);
    //    }
    //}
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
            dt = _queBLL.LoadSubjects(ddlStandardTextListId.SelectedValue.ToString());

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

    //private void LoadSubjects()
    //{
    //    try
    //    {
    //        ListItem li = new ListItem();

    //        ddlSubId.Items.Clear();

    //        li.Text = "<Select>";
    //        li.Value = "0";
    //        ddlSubId.Items.Add(li);

    //        li = null;

    //        DataTable dt = new DataTable();
    //        dt = _queBLL.LoadSubjects(ddlStandardTextListId.SelectedValue.ToString());

    //        foreach (DataRow dtr in dt.Rows)
    //        {
    //            li = new ListItem();

    //            li.Text = dtr[1].ToString();
    //            li.Value = dtr[0].ToString();
    //            ddlSubId.Items.Add(li);

    //            li = null;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowErrors("err", ex.Message);
    //    }
    //}
    private void LoadTest()
    {
        try
        {
            ListItem li = new ListItem();

            ddlTestId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlTestId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _queBLL.LoadTest();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlTestId.Items.Add(li);

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
                _queBLL.StandardTextListId = ddlStandardTextListId.SelectedValue;
            }
            else
            {
                _queBLL.StandardTextListId = null;
            }

            if (ddlSubId.SelectedIndex > 0)
            {
                _queBLL.SubId = ddlSubId.SelectedValue.ToString();
                _queBLL.Subject = ddlSubId.SelectedItem.Text.Trim().ToString();
            }
            else
            {
                _queBLL.SubId = null;
                _queBLL.Subject = null;
            }
            //Question ---  QueImg
            if (txtQue.Text.Trim().Length > 0)
            {
                _queBLL.Que = txtQue.Text.Trim();
            }
            else
            {
                _queBLL.Que = null;
                _queBLL.QueImg = null;
            }


            //A1 --- A1Img
            if (txtA1.Text.Trim().Length > 0)
            {
                _queBLL.A1 = txtA1.Text.Trim();
            }
            else
            {
                _queBLL.A1 = null;
                _queBLL.A1Img = null;
            }

            //A2-- A2Img
            if (txtA2.Text.Trim().Length > 0)
            {
                _queBLL.A2 = txtA2.Text.Trim();
            }
            else
            {
                _queBLL.A2 = null;
                _queBLL.A2Img = null;
            }

            //A3-- A3Img
            if (txtA3.Text.Trim().Length > 0)
            {
                _queBLL.A3 = txtA3.Text.Trim();
            }
            else
            {
                _queBLL.A3 = null;
                _queBLL.A3Img = null;
            }

            //A4-- A4Img
            if (txtA4.Text.Trim().Length > 0)
            {
                _queBLL.A4 = txtA4.Text.Trim();
            }
            else
            {
                _queBLL.A4 = null;
                _queBLL.A4Img = null;
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

                _queBLL.ImageNameQus = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_Q" + "." + oExtension;
                _queBLL.IsQueChanged = true;
            }
            else if (fuUploadPDF.PostedFile != null && fuUploadPDF.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuUploadPDF.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuUploadPDF.PostedFile;

                string[] getExtenstion = fuUploadPDF.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuUploadPDF.PostedFile.ContentLength);

                _queBLL.ImageNameQus = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_Q" + "." + oExtension;
                _queBLL.IsQueChanged = true;
            }
            else if (textarea1.Value.ToString().Trim().Length > 50)
            {
                _queBLL.ImageNameQus = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_Q" + ".png";
                _queBLL.IsQueChanged = true;
            }

            if (fileUploadA1Img.PostedFile != null && fileUploadA1Img.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadA1Img.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadA1Img.PostedFile;

                string[] getExtenstion = fileUploadA1Img.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadA1Img.PostedFile.ContentLength);

                _queBLL.ImageNameA1 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_A1" + "." + oExtension;
                _queBLL.IsA1Changed = true;
            }
            else if (textarea2.Value.ToString().Trim().Length > 50)
            {
                _queBLL.ImageNameA1 = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_A1" + ".png";
                _queBLL.IsA1Changed = true;
            }
            if (fileUploadA2Img.PostedFile != null && fileUploadA2Img.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadA2Img.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadA2Img.PostedFile;

                string[] getExtenstion = fileUploadA2Img.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadA2Img.PostedFile.ContentLength);

                _queBLL.ImageNameA2 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_A2" + "." + oExtension;
                _queBLL.IsA2Changed = true;
            }
            else if (textarea3.Value.ToString().Trim().Length > 50)
            {
                _queBLL.ImageNameA2 = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_A2" + ".png";
                _queBLL.IsA2Changed = true;
            }
            if (fileUploadA3Img.PostedFile != null && fileUploadA3Img.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadA3Img.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadA3Img.PostedFile;

                string[] getExtenstion = fileUploadA3Img.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadA3Img.PostedFile.ContentLength);

                _queBLL.ImageNameA3 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_A3" + "." + oExtension;
                _queBLL.IsA3Changed = true;
            }
            else if (textarea4.Value.ToString().Trim().Length > 50)
            {
                _queBLL.ImageNameA3 = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_A3" + ".png";
                _queBLL.IsA3Changed = true;
            }
            if (fileUploadA4Img.PostedFile != null && fileUploadA4Img.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fileUploadA4Img.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fileUploadA4Img.PostedFile;

                string[] getExtenstion = fileUploadA4Img.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fileUploadA4Img.PostedFile.ContentLength);

                _queBLL.ImageNameA4 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_A4" + "." + oExtension;
                _queBLL.IsA4Changed = true;
            }
            else if (textarea5.Value.ToString().Trim().Length > 50)
            {
                _queBLL.ImageNameA4 = ConfigurationSettings.AppSettings["FolderPath"] + "_" + txtSrNo.Text.Trim() + "_" + NewGUID + "_A4" + ".png";
                _queBLL.IsA4Changed = true;
            }

            if (ddlTestId.SelectedIndex > 0)
                _queBLL.TestId = ddlTestId.SelectedValue;
            else
                _queBLL.TestId = null;

            if (txtSrNo.Text.Trim().Length > 0)
                _queBLL.SrNo = Convert.ToInt16(txtSrNo.Text.Trim().ToString());
            else
                _queBLL.SrNo = null;

            if (rbQueTypeMCQ.Checked)
            {
                _queBLL.QueType = "MCQ";

                if (rbAnsSelectionSingle.Checked)
                {
                    if (ddlAnswer.SelectedIndex > 0)
                    {
                        _queBLL.Ans = (ddlAnswer.SelectedValue).ToString();
                    }
                    else
                    {
                        _queBLL.Ans = null;
                    }

                    _queBLL.AnsSelection = "SINGLE";
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
                        _queBLL.Ans = Id1;
                    else
                        _queBLL.Ans = null;

                    _queBLL.AnsSelection = "MULTIPLE";
                }
                else
                {
                    _queBLL.Ans = null;
                    _queBLL.AnsSelection = null;
                }
            }
            else if (rbQueTypeNONMCQ.Checked)
            {
                _queBLL.QueType = "NONMCQ";

                if (txtAns.Text.Trim().Length > 0)
                    _queBLL.Ans = txtAns.Text.ToString();
                else
                    _queBLL.Ans = null;

                _queBLL.AnsSelection = null;
            }
            else if (rbQueTypeFILE.Checked)
            {
                _queBLL.QueType = "FILE";
                _queBLL.Ans = null;
                _queBLL.AnsSelection = null;
            }
            else if (rbQueTypeWholeQPaper.Checked)
            {
                _queBLL.QueType = "PDF";
                _queBLL.Ans = null;
                _queBLL.AnsSelection = null;
            }
            else
            {
                _queBLL.Ans = null;
                _queBLL.QueType = null;
                _queBLL.AnsSelection = null;
            }

            if (rbQueDataTypeNum.Checked)
            {
                _queBLL.QueDataType = "NUM";
            }
            else if (rbQueDataTypeChar.Checked)
            {
                _queBLL.QueDataType = "CHAR";
            }
            else
            {
                _queBLL.QueDataType = null;
            }

            if (txtRightMarks.Text.Trim().Length > 0)
                _queBLL.RightMarks = Convert.ToDecimal(txtRightMarks.Text);
            else
                _queBLL.RightMarks = null;

            if (txtWrongMarks.Text.Trim().Length > 0)
                _queBLL.WrongMarks = Convert.ToDecimal(txtWrongMarks.Text);
            else
                _queBLL.WrongMarks = null;

            if (txtNonMarks.Text.Trim().Length > 0)
                _queBLL.NonMarks = Convert.ToDecimal(txtNonMarks.Text);
            else
                _queBLL.NonMarks = null;

            if (ddlNoOfFile.Visible)
                _queBLL.NoOfFile = Convert.ToInt16(ddlNoOfFile.SelectedValue.ToString());
            else
                _queBLL.NoOfFile = null;

            if (fuSampleAns1.PostedFile != null && fuSampleAns1.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuSampleAns1.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuSampleAns1.PostedFile;

                string[] getExtenstion = fuSampleAns1.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuSampleAns1.PostedFile.ContentLength);

                _queBLL.SampleAns1 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_S1" + "." + oExtension;
                _queBLL.IsSampleAns1Changed = true;
            }

            if (fuSampleAns2.PostedFile != null && fuSampleAns2.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuSampleAns2.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuSampleAns2.PostedFile;

                string[] getExtenstion = fuSampleAns2.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuSampleAns2.PostedFile.ContentLength);

                _queBLL.SampleAns2 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_S2" + "." + oExtension;
                _queBLL.IsSampleAns2Changed = true;
            }

            if (fuSampleAns3.PostedFile != null && fuSampleAns3.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuSampleAns3.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuSampleAns3.PostedFile;

                string[] getExtenstion = fuSampleAns3.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuSampleAns3.PostedFile.ContentLength);

                _queBLL.SampleAns3 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_S3" + "." + oExtension;
                _queBLL.IsSampleAns3Changed = true;
            }

            if (fuSampleAns4.PostedFile != null && fuSampleAns4.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuSampleAns4.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuSampleAns4.PostedFile;

                string[] getExtenstion = fuSampleAns4.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                uploadedImage.InputStream.Read(imageSize, 0, (int)fuSampleAns4.PostedFile.ContentLength);

                _queBLL.SampleAns4 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName + "_" + System.Guid.NewGuid().ToString() + "_S4" + "." + oExtension;
                _queBLL.IsSampleAns4Changed = true;
            }

            if (txtNoOfSubQues.Text.Trim().Length > 0)
                _queBLL.NoOfSubQues = Convert.ToInt16(txtNoOfSubQues.Text.Trim());
            else
                _queBLL.NoOfSubQues = null;
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
                _queBLL.Save();

                if (fileUploadQusImg.PostedFile != null && fileUploadQusImg.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadQusImg.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadQusImg.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.ImageNameQus);
                }
                else if (fuUploadPDF.PostedFile != null && fuUploadPDF.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuUploadPDF.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuUploadPDF.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.ImageNameQus);
                }
                else if (textarea1.Value.ToString().Trim().Length > 50 && _queBLL.ImageNameQus != null)
                {
                    string imgPath = _queBLL.ImageNameQus;

                    byte[] imageBytes = Convert.FromBase64String(textarea1.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }

                if (fileUploadA1Img.PostedFile != null && fileUploadA1Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA1Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA1Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.ImageNameA1);
                }
                else if (textarea2.Value.ToString().Trim().Length > 50 && _queBLL.ImageNameA1 != null)
                {
                    string imgPath = _queBLL.ImageNameA1;

                    byte[] imageBytes = Convert.FromBase64String(textarea2.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA2Img.PostedFile != null && fileUploadA2Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA2Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA2Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.ImageNameA2);
                }
                else if (textarea3.Value.ToString().Trim().Length > 50 && _queBLL.ImageNameA2 != null)
                {
                    string imgPath = _queBLL.ImageNameA2;

                    byte[] imageBytes = Convert.FromBase64String(textarea3.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA3Img.PostedFile != null && fileUploadA3Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA3Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA3Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.ImageNameA3);
                }
                else if (textarea4.Value.ToString().Trim().Length > 50 && _queBLL.ImageNameA3 != null)
                {
                    string imgPath = _queBLL.ImageNameA3;

                    byte[] imageBytes = Convert.FromBase64String(textarea4.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA4Img.PostedFile != null && fileUploadA4Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA4Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA4Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.ImageNameA4);
                }
                else if (textarea5.Value.ToString().Trim().Length > 50 && _queBLL.ImageNameA4 != null)
                {
                    string imgPath = _queBLL.ImageNameA4;

                    byte[] imageBytes = Convert.FromBase64String(textarea5.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }

                if (fuSampleAns1.PostedFile != null && fuSampleAns1.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuSampleAns1.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuSampleAns1.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.SampleAns1);
                }

                if (fuSampleAns2.PostedFile != null && fuSampleAns2.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuSampleAns2.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuSampleAns2.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.SampleAns2);
                }

                if (fuSampleAns3.PostedFile != null && fuSampleAns3.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuSampleAns3.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuSampleAns3.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.SampleAns3);
                }

                if (fuSampleAns4.PostedFile != null && fuSampleAns4.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuSampleAns4.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuSampleAns4.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.SampleAns4);
                }

                if (Request.QueryString["QueId"] == null)
                {
                    Reset();
                    //Session["_queBLL"] = null;
                    //Session["_queBLL"] = new QueBLL();
                    //_queBLL = (QueBLL)Session["_queBLL"];

                    Session[hfMySessionHeaderValue.Value] = null;
                    for (int i = 0; i < 100; i++)
                    {
                        if (Session["_queBLL" + i + ""] == null)
                        {
                            Session["_queBLL" + i + ""] = _queBLL;
                            hfMySessionHeaderValue.Value = "_queBLL" + i;
                            break;
                        }
                    }

                    Session[hfMySessionHeaderValue.Value] = new QueBLL();

                    _queBLL = (QueBLL)Session[hfMySessionHeaderValue.Value];

                    txtSubject.Focus();
                }
                else
                {
                    //Session["_queBLL"] = null;
                    Session[hfMySessionHeaderValue.Value] = null;
                    Response.Redirect("Ques.aspx");
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
                _queBLL.Save();

                if (fileUploadQusImg.PostedFile != null && fileUploadQusImg.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadQusImg.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadQusImg.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.ImageNameQus);
                }
                else if (fuUploadPDF.PostedFile != null && fuUploadPDF.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fuUploadPDF.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fuUploadPDF.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.ImageNameQus);
                }
                else if (textarea1.Value.ToString().Trim().Length > 50 && _queBLL.ImageNameQus != null)
                {
                    string imgPath = _queBLL.ImageNameQus;

                    byte[] imageBytes = Convert.FromBase64String(textarea1.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }


                if (fileUploadA1Img.PostedFile != null && fileUploadA1Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA1Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA1Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.ImageNameA1);
                }
                else if (textarea2.Value.ToString().Trim().Length > 50 && _queBLL.ImageNameA1 != null)
                {
                    string imgPath = _queBLL.ImageNameA1;

                    byte[] imageBytes = Convert.FromBase64String(textarea2.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA2Img.PostedFile != null && fileUploadA2Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA2Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA2Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.ImageNameA2);
                }
                else if (textarea3.Value.ToString().Trim().Length > 50 && _queBLL.ImageNameA2 != null)
                {
                    string imgPath = _queBLL.ImageNameA2;

                    byte[] imageBytes = Convert.FromBase64String(textarea3.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA3Img.PostedFile != null && fileUploadA3Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA3Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA3Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.ImageNameA3);
                }
                else if (textarea4.Value.ToString().Trim().Length > 50 && _queBLL.ImageNameA3 != null)
                {
                    string imgPath = _queBLL.ImageNameA3;

                    byte[] imageBytes = Convert.FromBase64String(textarea4.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }
                if (fileUploadA4Img.PostedFile != null && fileUploadA4Img.PostedFile.FileName != "")
                {
                    byte[] imageSize = new byte[fileUploadA4Img.PostedFile.ContentLength];
                    HttpPostedFile uploadedImage = fileUploadA4Img.PostedFile;

                    if (imageSize.Length > 0)
                        uploadedImage.SaveAs(_queBLL.ImageNameA4);
                }
                else if (textarea5.Value.ToString().Trim().Length > 50 && _queBLL.ImageNameA4 != null)
                {
                    string imgPath = _queBLL.ImageNameA4;

                    byte[] imageBytes = Convert.FromBase64String(textarea5.Value.Split(',')[1]);

                    File.WriteAllBytes(imgPath, imageBytes);
                }

                if (Request.QueryString["QueId"] == null)
                {
                    string str_ddlStandardTextListId, str_ddlSubId, str_ddlTestId;
                    str_ddlStandardTextListId = ddlStandardTextListId.SelectedValue;
                    str_ddlSubId = ddlSubId.SelectedValue.ToString();
                    str_ddlTestId = ddlTestId.SelectedValue;
                    string txtRightMarksLast, txtWrongMarksLast, txtNonMarksLast;
                    txtRightMarksLast = txtRightMarks.Text;
                    txtWrongMarksLast = txtWrongMarks.Text;
                    txtNonMarksLast = txtNonMarks.Text;
                    //Reset();
                    ResetWithOutCheckBox();

                    //Session["_queBLL"] = null;
                    //Session["_queBLL"] = new QueBLL();
                    //_queBLL = (QueBLL)Session["_queBLL"];

                    Session[hfMySessionHeaderValue.Value] = null;
                    for (int i = 0; i < 100; i++)
                    {
                        if (Session["_queBLL" + i + ""] == null)
                        {
                            Session["_queBLL" + i + ""] = _queBLL;
                            hfMySessionHeaderValue.Value = "_queBLL" + i;
                            break;
                        }
                    }

                    Session[hfMySessionHeaderValue.Value] = new QueBLL();

                    _queBLL = (QueBLL)Session[hfMySessionHeaderValue.Value];

                    ddlStandardTextListId.SelectedValue = str_ddlStandardTextListId;
                    ddlStandardTextListId_SelectedIndexChanged(sender, new EventArgs());

                    ddlSubId.SelectedValue = str_ddlSubId;
                    ddlSubId_SelectedIndexChanged(sender, new EventArgs());
                    ddlTestId.SelectedValue = str_ddlTestId;
                    ddlTestId_SelectedIndexChanged(sender, new EventArgs());


                    txtRightMarks.Text = txtRightMarksLast;
                    txtWrongMarks.Text = txtWrongMarksLast;
                    txtNonMarks.Text = txtNonMarksLast;


                    visibleQueTypeData(true);
                    txtQue.Focus();
                }
                else
                {
                    //Session["_queBLL"] = null;
                    Session[hfMySessionHeaderValue.Value] = null;
                    Response.Redirect("Ques.aspx");
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

        SortedList sl = _queBLL.Validate();

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

        //NoOfSubQues
        if (key == "NoOfSubQues")
        {
            lblNoOfSubQues.CssClass = "error";
            txtNoOfSubQues.CssClass = "error";
        }
        //NoOfSubQues
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

        lblNoOfSubQues.CssClass = "";
        txtNoOfSubQues.CssClass = "";
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

        ddlTestId.SelectedIndex = 0;
        ddlSubId.SelectedIndex = 0;

        txtSrNo.Text = "";

        rbQueTypeMCQ.Checked = false;
        rbQueTypeNONMCQ.Checked = false;
        rbQueTypeFILE.Checked = false;

        rbQueDataTypeNum.Checked = false;
        rbQueDataTypeChar.Checked = false;

        txtAns.Text = "";

        //txtRightMarks.Text = "";
        //txtWrongMarks.Text = "";
        //txtNonMarks.Text = "";

        ddlNoOfFile.SelectedIndex = 0;

        rbAnsSelectionSingle.Checked = true;
        chklAns.ClearSelection();
        txtNoOfSubQues.Text = "";
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

        ddlTestId.SelectedIndex = 0;
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
        txtNoOfSubQues.Text = "";
    }

    private void LoadWebForm()
    {
        trFramePaper.Visible = false;
        if (_queBLL.StandardTextListId != null)
        {
            ddlStandardTextListId.SelectedValue = _queBLL.StandardTextListId;
            ddlStandardTextListId_SelectedIndexChanged(null, null);
        }

        //Subject
        if (_queBLL.SubId != null)
        {
            //hfSubId.Value = _queBLL.SubId;
            //txtSubject.Text = _queBLL.Subject;

            ddlSubId.SelectedValue = _queBLL.SubId;
            ddlSubId_SelectedIndexChanged(null, null);
        }

        if (_queBLL.TestId != null)
        {
            ddlTestId.SelectedValue = _queBLL.TestId;
        }

        //Question
        if (_queBLL.Que != null)
            txtQue.Text = _queBLL.Que;

        //A1
        if (_queBLL.A1 != null)
            txtA1.Text = _queBLL.A1;

        //A2
        if (_queBLL.A2 != null)
            txtA2.Text = _queBLL.A2;

        //A3
        if (_queBLL.A3 != null)
            txtA3.Text = _queBLL.A3;

        //A4
        if (_queBLL.A4 != null)
            txtA4.Text = _queBLL.A4;

        //Answer

        if (_queBLL.QueType != null)
        {
            if (_queBLL.QueType.ToString().ToUpper() == "MCQ".ToString().ToUpper())
            {
                rbQueTypeMCQ.Checked = true;
                rbQueTypeNONMCQ.Checked = false;
                rbQueTypeMCQ_CheckedChanged(null, null);

                if (_queBLL.AnsSelection != null)
                {
                    if (_queBLL.AnsSelection.ToString().ToUpper() == "SINGLE".ToString().ToUpper())
                    {
                        rbAnsSelectionSingle.Checked = true;
                        rbAnsSelectionMulti.Checked = false;

                        rbAnsSelectionSingle_CheckedChanged(null, null);
                    }
                    else if (_queBLL.AnsSelection.ToString().ToUpper() == "MULTIPLE".ToString().ToUpper())
                    {
                        rbAnsSelectionSingle.Checked = false;
                        rbAnsSelectionMulti.Checked = true;

                        rbAnsSelectionMulti_CheckedChanged(null, null);
                    }
                }

                if (rbAnsSelectionSingle.Checked)
                {
                    if (_queBLL.Ans != null)
                    {
                        ddlAnswer.SelectedValue = (_queBLL.Ans).ToString();
                    }
                }
                else
                {
                    if (_queBLL.Ans != null)
                    {
                        string[] answer = _queBLL.Ans.ToString().Split(',');

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
                                if (li.Value.ToString() == _queBLL.Ans.ToString().ToUpper())
                                {
                                    li.Selected = true;
                                }
                            }
                        }
                    }
                }
            }
            else if (_queBLL.QueType.ToString().ToUpper() == "NONMCQ".ToString().ToUpper())
            {
                rbQueTypeMCQ.Checked = false;
                rbQueTypeNONMCQ.Checked = true;
                rbQueTypeMCQ_CheckedChanged(null, null);

                if (_queBLL.Ans != null)
                {
                    txtAns.Text = (_queBLL.Ans).ToString();
                }
            }
            else if (_queBLL.QueType.ToString().ToUpper() == "FILE".ToString().ToUpper())
            {
                rbQueTypeFILE.Checked = true;
                rbQueTypeFILE_CheckedChanged(null, null);
            }
            else if (_queBLL.QueType.ToString().ToUpper() == "PDF".ToString().ToUpper())
            {
                rbQueTypeWholeQPaper.Checked = true;
                rbQueTypeWholeQPaper_CheckedChanged(null, null);

                if (_queBLL.ImageNameQus != null)
                {
                    trFramePaper.Visible = true;
                    iframepdf.Attributes.Add("src", _queBLL.ImageNameQus);
                }
            }
        }

        if (_queBLL.ImageNameQus != null)
        {
            imgqusPics.ImageUrl = _queBLL.ImageNameQus;
        }
        if (_queBLL.ImageNameA1 != null)
        {
            imga1Pics.ImageUrl = _queBLL.ImageNameA1;
        }
        if (_queBLL.ImageNameA2 != null)
        {
            imga2Pics.ImageUrl = _queBLL.ImageNameA2;
        }
        if (_queBLL.ImageNameA3 != null)
        {
            imga3Pics.ImageUrl = _queBLL.ImageNameA3;
        }
        if (_queBLL.ImageNameA4 != null)
        {
            imga4Pics.ImageUrl = _queBLL.ImageNameA4;
        }

        if (_queBLL.SrNo != null)
        {
            txtSrNo.Text = Convert.ToString(_queBLL.SrNo);
        }

        if (_queBLL.QueDataType != null)
        {
            if (_queBLL.QueDataType.ToString().ToUpper() == "NUM".ToString().ToUpper())
            {
                rbQueDataTypeNum.Checked = true;
                rbQueDataTypeChar.Checked = false;
            }
            else if (_queBLL.QueDataType.ToString().ToUpper() == "CHAR".ToString().ToUpper())
            {
                rbQueDataTypeNum.Checked = false;
                rbQueDataTypeChar.Checked = true;
            }
        }

        if (_queBLL.RightMarks != null)
        {
            txtRightMarks.Text = Convert.ToDecimal(_queBLL.RightMarks).ToString("0.##");
        }

        if (_queBLL.WrongMarks != null)
        {
            txtWrongMarks.Text = Convert.ToDecimal(_queBLL.WrongMarks).ToString("0.##");
        }

        if (_queBLL.NonMarks != null)
        {
            txtNonMarks.Text = Convert.ToDecimal(_queBLL.NonMarks).ToString("0.##");
        }

        if (_queBLL.NoOfFile != null)
        {
            ddlNoOfFile.SelectedValue = _queBLL.NoOfFile.ToString();
        }

        if (_queBLL.SampleAns1 != null)
        {
            hlSampleAns1.NavigateUrl = _queBLL.SampleAns1.ToString();
            hlSampleAns1.Target = "_blank";
        }

        if (_queBLL.SampleAns2 != null)
        {
            hlSampleAns2.NavigateUrl = _queBLL.SampleAns2.ToString();
            hlSampleAns2.Target = "_blank";
        }

        if (_queBLL.SampleAns3 != null)
        {
            hlSampleAns3.NavigateUrl = _queBLL.SampleAns3.ToString();
            hlSampleAns3.Target = "_blank";
        }

        if (_queBLL.SampleAns4 != null)
        {
            hlSampleAns4.NavigateUrl = _queBLL.SampleAns4.ToString();
            hlSampleAns4.Target = "_blank";
        }

        if (_queBLL.NoOfSubQues != null)
        {
            txtNoOfSubQues.Text = Convert.ToDecimal(_queBLL.NoOfSubQues).ToString("0.##");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _queBLL.Delete(Request.QueryString["QueId"].ToString());
            HideErrors();
            //Session["_queBLL"] = null;
            Session[hfMySessionHeaderValue.Value] = null;
            Reset();
            Response.Redirect("Ques.aspx");
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        //Session["_queBLL"] = null;
        Session[hfMySessionHeaderValue.Value] = null;

        if (Request.QueryString["QueId"] == null)
        {
            Response.Redirect("~/General/Default.aspx");
        }
        else
        {
            Response.Redirect("Ques.aspx");
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
                _queBLL.SubId = ddlSubId.SelectedValue;
                LoadTest();
            }
            else
            {
                _queBLL.SubId = null;
                ShowErrors("err", "You have to select Subject");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }
    protected void ddlTestId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["QueId"] == null)
            {
                if (ddlStandardTextListId.SelectedIndex > 0)
                    _queBLL.StandardTextListId = ddlStandardTextListId.SelectedValue;
                else
                    _queBLL.StandardTextListId = null;

                if (ddlSubId.SelectedIndex > 0)
                    _queBLL.SubId = ddlSubId.SelectedValue;
                else
                    _queBLL.SubId = null;

                if (ddlTestId.SelectedIndex > 0)
                {
                    _queBLL.TestId = ddlTestId.SelectedValue;
                    hlTest.NavigateUrl = "../Exams/ViewResultDetailMasterSheet.aspx?TestId=" + _queBLL.TestId;
                    hlTest.Target = "_blank";
                }
                else
                {
                    _queBLL.TestId = null;
                }

                if (_queBLL.SubId != null && _queBLL.TestId != null)
                {
                    txtSrNo.Text = _queBLL.getSrNo().ToString();
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
    protected void rbQueTypeMCQ_CheckedChanged(object sender, EventArgs e)
    {
        visibleQueTypeData(false);
    }
    protected void rbQueTypeNONMCQ_CheckedChanged(object sender, EventArgs e)
    {
        visibleQueTypeData(false);
    }
    protected void rbQueTypeFILE_CheckedChanged(object sender, EventArgs e)
    {
        visibleQueTypeData(false);
    }
    protected void rbQueTypeWholeQPaper_CheckedChanged(object sender, EventArgs e)
    {
        visibleQueTypeData(false);
    }

    public void visibleQueTypeData(bool isChange)
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

            if (!isChange)
            {
                txtRightMarks.Text = "1";
                txtWrongMarks.Text = "-0.25";
                txtNonMarks.Text = "0";
            }

            trAnsSelection.Visible = true;
            trNoOfSubQues.Visible = false;
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
            if (!isChange)
            {
                txtRightMarks.Text = "4";
                txtWrongMarks.Text = "-1";
                txtNonMarks.Text = "0";
            }
            if (rbQueDataTypeNum.Checked == false || rbQueDataTypeChar.Checked == false)
                rbQueDataTypeNum.Checked = true;

            trAnsSelection.Visible = false;
            trNoOfSubQues.Visible = false;
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
            //trNoOfFileUpload.Visible = true; // by pm, change for 10 image upload from app and web answer time

            trAnsSample1.Visible = true;
            trAnsSample2.Visible = true;
            trAnsSample3.Visible = true;
            trAnsSample4.Visible = true;
            if (!isChange)
            {
                txtRightMarks.Text = "4";
                txtWrongMarks.Text = "-1";
                txtNonMarks.Text = "0";
            }
            trAnsSelection.Visible = false;
            trNoOfSubQues.Visible = true;
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

            if (!isChange)
            {
                txtRightMarks.Text = "0";
                txtWrongMarks.Text = "0";
                txtNonMarks.Text = "0";
            }
            trAnsSelection.Visible = false;
            trQueTest.Visible = false;
            trTextarea1.Visible = false;

            lblBabyPics.Text = "Select Question Paper as PDF File :";

            fileUploadQusImg.Visible = false;
            fuUploadPDF.Visible = true;
            trNoOfSubQues.Visible = false;
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
                    dt = _queBLL.TextList(Symbols[i]);
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
}

