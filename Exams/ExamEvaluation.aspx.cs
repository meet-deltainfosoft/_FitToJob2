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
using System.Web.Services;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

public partial class Exams_ExamEvaluation : System.Web.UI.Page
{
    private ExamEvaluationBLL _examEvaluationBLL;
    bool isNextPreviousClick = false;
    int[] filledImage = new int[40];
    bool pageEntryValid = false;
    bool isEditMode = false;

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            //if (Request.QueryString["ExamEvaluationId"] == null)
            //{
            _examEvaluationBLL = new ExamEvaluationBLL();
            //}
            //else
            //{
            //    _examEvaluationBLL = new ExamEvaluationBLL(Request.QueryString["ExamEvaluationId"].ToString());
            //}
            Session["_examEvaluationBLL"] = _examEvaluationBLL;
        }
        else
        {
            _examEvaluationBLL = (ExamEvaluationBLL)Session["_examEvaluationBLL"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //if (Request.QueryString["ExamEvaluationId"] != null)
                //{
                //    lblTitle.Text = " - [Edit Mode]";
                //    LoadWebForm();
                //}
                //else
                //{
                if (Request.QueryString["QueId"] != null)
                {
                    LoadAnsData();
                }
                //}
            }
            else
            {
                HideErrors();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type=\"text/javascript\">saveImage();</script>", false);
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message);
        }
    }
    #endregion

    public void LoadAnsData()
    {
        try
        {
            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            DataTable dt = new DataTable();
            DataTable dtEvaluations = new DataTable();
            DataTable dtEvaluationLns = new DataTable();

            ds = _examEvaluationBLL.LoadAnsData(Request.QueryString["QueId"].ToString()
                                    , Request.QueryString["ExamId"].ToString()
                                    , Request.QueryString["RegistrationId"].ToString()
                                    , Request.QueryString["ExamScheduleId"].ToString()
                                    , Convert.ToInt16(Request.QueryString["ImageNo"].ToString()));

            dt = ds.Tables[0];

            hfQueId.Value = Request.QueryString["QueId"].ToString();
            hfExamId.Value = Request.QueryString["ExamId"].ToString();
            hfRegistrationId.Value = Request.QueryString["RegistrationId"].ToString();
            hfExamScheduleId.Value = Request.QueryString["ExamScheduleId"].ToString();
            hfUserId.Value = Request.QueryString["UserId"].ToString();

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Standard"] != DBNull.Value)
                    lblStandard.Text = dt.Rows[0]["Standard"].ToString();
                else
                    lblStandard.Text = "--";

                if (dt.Rows[0]["SubjectName"] != DBNull.Value)
                    lblSubject.Text = dt.Rows[0]["SubjectName"].ToString();
                else
                    lblSubject.Text = "--";

                if (dt.Rows[0]["TestName"] != DBNull.Value)
                    lblTest.Text = dt.Rows[0]["TestName"].ToString();
                else
                    lblTest.Text = "--";

                if (dt.Rows[0]["ExamFromTime"] != DBNull.Value)
                    lblExamSchedule.Text = Convert.ToDateTime(dt.Rows[0]["ExamFromTime"]).ToString("dd-MMM-yyyy hh:mm tt");
                else
                    lblExamSchedule.Text = "--";

                if (Request.QueryString["EmpName"] != null)
                    lblEmployeeName.Text = Request.QueryString["EmpName"].ToString();

                if (dt.Rows[0]["Question"] != DBNull.Value)
                {
                    lblQues.Text = dt.Rows[0]["Question"].ToString();
                    imgqusPics.Visible = false;
                    lnkImage.Visible = false;
                }
                else
                {
                    lblQues.Visible = false;
                    imgqusPics.Visible = true;
                    lnkImage.Visible = true;
                    if (dt.Rows[0]["ImageNameQus"] != DBNull.Value)
                    {
                        imgqusPics.ImageUrl = dt.Rows[0]["ImageNameQus"].ToString();
                        lnkImage.HRef = dt.Rows[0]["ImageNameQus"].ToString();
                    }
                }

                int totalImage = 0;

                StringBuilder sb = new StringBuilder();

                for (int i = 1; i <= 40; i++)
                {
                    if (dt.Rows[0]["AnsImage" + i + "Local"] != DBNull.Value)
                    {
                        sb.Append(" | IMAGE NO : " + i + "");

                        filledImage[totalImage] = i;
                        totalImage++;
                    }
                }

                Literal lt = new Literal();

                lt.Text = sb.ToString();

                plNavigate.Controls.Add(lt);

                lblCurrentImage.Text = filledImage[Convert.ToInt16(Convert.ToInt16(Request.QueryString["ImageNo"].ToString())) - 1].ToString();
                lblTotalImage.Text = Convert.ToInt16(totalImage).ToString();

                if (dt.Rows[0]["AnsImage" + filledImage[Convert.ToInt16(Request.QueryString["ImageNo"].ToString()) - 1] + "Local"] != DBNull.Value)
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(dt.Rows[0]["AnsImage" + filledImage[Convert.ToInt16(Request.QueryString["ImageNo"].ToString()) - 1] + "Local"].ToString());

                    paintcanvas.Attributes.Add("width", "" + img.Width + "");
                    paintcanvas.Attributes.Add("height", "" + img.Height + "");

                    paintcanvas.Style.Add("background-image", "url('" + dt.Rows[0]["AnsImage" + filledImage[Convert.ToInt16(Request.QueryString["ImageNo"].ToString()) - 1] + ""].ToString().Replace("\\", "/") + "')");
                    paintcanvas.Style.Add("background-repeat", "no-repeat");

                    tableEditMode.Visible = false;

                    hfImagePath.Value = dt.Rows[0]["AnsImage" + filledImage[Convert.ToInt16(Request.QueryString["ImageNo"].ToString()) - 1] + ""].ToString().Replace("\\", "/");
                    hfImageNo.Value = filledImage[Convert.ToInt16(Request.QueryString["ImageNo"].ToString()) - 1].ToString();
                }

                if (dt.Rows[0]["TotalMarks"] != DBNull.Value)
                {
                    lblTotal.Text = dt.Rows[0]["TotalMarks"].ToString();
                    _examEvaluationBLL.TotalMarks = Convert.ToDecimal(dt.Rows[0]["TotalMarks"].ToString());
                }
                else
                {
                    lblTotal.Text = "0";
                }

                if (dt.Rows[0]["ObtainedMarks"] != DBNull.Value)
                {
                    lblObtained.Text = dt.Rows[0]["ObtainedMarks"].ToString();
                    _examEvaluationBLL.ObtainedMarks = Convert.ToDecimal(dt.Rows[0]["ObtainedMarks"].ToString());
                }
                else
                    lblObtained.Text = "0";
            }

            ds1 = _examEvaluationBLL.LoadAnsDataForImageNo(Request.QueryString["QueId"].ToString()
                                    , Request.QueryString["ExamId"].ToString()
                                    , Request.QueryString["RegistrationId"].ToString()
                                    , Request.QueryString["ExamScheduleId"].ToString()
                                    , Convert.ToInt16(lblCurrentImage.Text.ToString()));

            dtEvaluations = ds1.Tables[0];
            dtEvaluationLns = ds1.Tables[1];

            if (dtEvaluations.Rows.Count > 0)
            {
                if (dtEvaluations.Rows[0]["TotalObtMark"] != DBNull.Value)
                {
                    lblTotalMark.Text = dtEvaluations.Rows[0]["TotalObtMark"].ToString();
                    hfTotalMarks.Value = dtEvaluations.Rows[0]["TotalObtMark"].ToString();
                }

                DataRow[] dtr = dtEvaluations.Select("ImageNo='" + lblCurrentImage.Text.Trim().ToString() + "'");

                if (dtr.Length > 0)
                {
                    tableEditMode.Visible = true;
                    tblCanvas.Visible = false;

                    imgShow.ImageUrl = dtr[0]["ImagePathLive"].ToString();
                    imgShow.Style.Add("background-image", "url('" + dt.Rows[0]["AnsImage" + filledImage[Convert.ToInt16(Request.QueryString["ImageNo"].ToString()) - 1] + ""].ToString().Replace("\\", "/") + "')");
                    imgShow.Style.Add("background-repeat", "no-repeat");
                }
                //btnEdit.Visible = true;

                if (dtEvaluations.Rows[0]["Remarks"] != DBNull.Value)
                    txtRemarks.Text = dtEvaluations.Rows[0]["Remarks"].ToString();
                else
                    txtRemarks.Text = "";

                isEditMode = true;
                hfEditMode.Value = "true";

                btnRotateRight.Visible = false;
            }

            if (dtEvaluationLns.Rows.Count > 0)
            {
                if (dtEvaluationLns.Rows.Count > 0)
                {
                    if (dtEvaluationLns.Rows[0]["ObtMark"] != DBNull.Value)
                        txtMark1.Text = Convert.ToDecimal(dtEvaluationLns.Rows[0]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 1)
                {
                    if (dtEvaluationLns.Rows[1]["ObtMark"] != DBNull.Value)
                        txtMark2.Text = Convert.ToDecimal(dtEvaluationLns.Rows[1]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 2)
                {
                    if (dtEvaluationLns.Rows[2]["ObtMark"] != DBNull.Value)
                        txtMark3.Text = Convert.ToDecimal(dtEvaluationLns.Rows[2]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 3)
                {
                    if (dtEvaluationLns.Rows[3]["ObtMark"] != DBNull.Value)
                        txtMark4.Text = Convert.ToDecimal(dtEvaluationLns.Rows[3]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 4)
                {
                    if (dtEvaluationLns.Rows[4]["ObtMark"] != DBNull.Value)
                        txtMark5.Text = Convert.ToDecimal(dtEvaluationLns.Rows[4]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 5)
                {
                    if (dtEvaluationLns.Rows[5]["ObtMark"] != DBNull.Value)
                        txtMark6.Text = Convert.ToDecimal(dtEvaluationLns.Rows[5]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 6)
                {
                    if (dtEvaluationLns.Rows[6]["ObtMark"] != DBNull.Value)
                        txtMark7.Text = Convert.ToDecimal(dtEvaluationLns.Rows[6]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 7)
                {
                    if (dtEvaluationLns.Rows[7]["ObtMark"] != DBNull.Value)
                        txtMark8.Text = Convert.ToDecimal(dtEvaluationLns.Rows[7]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 8)
                {
                    if (dtEvaluationLns.Rows[8]["ObtMark"] != DBNull.Value)
                        txtMark9.Text = Convert.ToDecimal(dtEvaluationLns.Rows[8]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 9)
                {
                    if (dtEvaluationLns.Rows[9]["ObtMark"] != DBNull.Value)
                        txtMark10.Text = Convert.ToDecimal(dtEvaluationLns.Rows[9]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 10)
                {
                    if (dtEvaluationLns.Rows[10]["ObtMark"] != DBNull.Value)
                        txtMark11.Text = Convert.ToDecimal(dtEvaluationLns.Rows[10]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 11)
                {
                    if (dtEvaluationLns.Rows[11]["ObtMark"] != DBNull.Value)
                        txtMark12.Text = Convert.ToDecimal(dtEvaluationLns.Rows[11]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 12)
                {
                    if (dtEvaluationLns.Rows[12]["ObtMark"] != DBNull.Value)
                        txtMark13.Text = Convert.ToDecimal(dtEvaluationLns.Rows[12]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 13)
                {
                    if (dtEvaluationLns.Rows[13]["ObtMark"] != DBNull.Value)
                        txtMark14.Text = Convert.ToDecimal(dtEvaluationLns.Rows[13]["ObtMark"]).ToString("0.##");
                }

                if (dtEvaluationLns.Rows.Count > 14)
                {
                    if (dtEvaluationLns.Rows[14]["ObtMark"] != DBNull.Value)
                        txtMark15.Text = Convert.ToDecimal(dtEvaluationLns.Rows[14]["ObtMark"]).ToString("0.##");
                }

                isEditMode = true;
                hfEditMode.Value = "true";
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message);
        }
    }

    #region "Subs Functions"

    private bool Validate()
    {
        HideErrors();

        SortedList sl = _examEvaluationBLL.Validate();

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
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();
    }

    private void Reset()
    {
        try
        {
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message);
        }
    }

    private void LoadWebForm()
    {
        try
        {

        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message);
        }
    }

    #endregion

    #region "Subs Events"
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            decimal totalmarksValidate = 0;
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type=\"text/javascript\">saveImage();</script>", false);

            if (hfQueId.Value.Trim().Length > 0)
                _examEvaluationBLL.QueId = hfQueId.Value.Trim().ToString();
            else
                _examEvaluationBLL.QueId = null;

            if (hfUserId.Value.Trim().Length > 0)
                _examEvaluationBLL.UserId = hfUserId.Value.Trim().ToString();
            else
                _examEvaluationBLL.UserId = null;

            if (hfExamId.Value.Trim().Length > 0)
                _examEvaluationBLL.ExamId = hfExamId.Value.Trim().ToString();
            else
                _examEvaluationBLL.ExamId = null;

            if (lblCurrentImage.Text.Trim().Length > 0)
                _examEvaluationBLL.ImageNo = Convert.ToInt16(lblCurrentImage.Text.Trim().ToString());
            else
                _examEvaluationBLL.ImageNo = null;

            if (_examEvaluationBLL.ExamId != null && _examEvaluationBLL.UserId != null && _examEvaluationBLL.ImageNo != null)
            {
                string msExcelFilePathOnServer = ConfigurationSettings.AppSettings["FolderPath"] + "" + Convert.ToDateTime(System.DateTime.Now).ToString("ddMMMyyyy") + "/";

                bool exists = System.IO.Directory.Exists(msExcelFilePathOnServer);

                if (!exists)
                    System.IO.Directory.CreateDirectory(msExcelFilePathOnServer);

                _examEvaluationBLL.ImagePath = msExcelFilePathOnServer + _examEvaluationBLL.ExamId + "_" + _examEvaluationBLL.UserId + "_" + _examEvaluationBLL.ImageNo + ".png";
            }
            else
                _examEvaluationBLL.ImagePath = null;

            if (hfTotalMarks.Value.Trim().Length > 0)
                _examEvaluationBLL.TotalObtMark = hfTotalMarks.Value.ToString();

            string SubMarks = null;

            if (txtMark1.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark1.Text.ToString();
                else
                    SubMarks += "," + txtMark1.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark1.Text);
            }

            if (txtMark2.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark2.Text.ToString();
                else
                    SubMarks += "," + txtMark2.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark2.Text);
            }

            if (txtMark3.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark3.Text.ToString();
                else
                    SubMarks += "," + txtMark3.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark3.Text);
            }

            if (txtMark4.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark4.Text.ToString();
                else
                    SubMarks += "," + txtMark4.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark4.Text);
            }

            if (txtMark5.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark5.Text.ToString();
                else
                    SubMarks += "," + txtMark5.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark5.Text);
            }

            if (txtMark6.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark6.Text.ToString();
                else
                    SubMarks += "," + txtMark6.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark6.Text);
            }

            if (txtMark7.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark7.Text.ToString();
                else
                    SubMarks += "," + txtMark7.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark7.Text);
            }

            if (txtMark8.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark8.Text.ToString();
                else
                    SubMarks += "," + txtMark8.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark8.Text);
            }

            if (txtMark9.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark9.Text.ToString();
                else
                    SubMarks += "," + txtMark9.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark9.Text);
            }

            if (txtMark10.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark10.Text.ToString();
                else
                    SubMarks += "," + txtMark10.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark10.Text);
            }

            if (txtMark11.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark11.Text.ToString();
                else
                    SubMarks += "," + txtMark11.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark11.Text);
            }

            if (txtMark12.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark12.Text.ToString();
                else
                    SubMarks += "," + txtMark12.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark12.Text);
            }

            if (txtMark13.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark13.Text.ToString();
                else
                    SubMarks += "," + txtMark13.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark13.Text);
            }

            if (txtMark14.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark14.Text.ToString();
                else
                    SubMarks += "," + txtMark14.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark14.Text);
            }

            if (txtMark15.Text.Trim().Length > 0)
            {
                if (SubMarks == null)
                    SubMarks = txtMark15.Text.ToString();
                else
                    SubMarks += "," + txtMark15.Text.ToString();

                totalmarksValidate += Convert.ToDecimal(txtMark15.Text);
            }

            if (totalmarksValidate != null)
            {
                _examEvaluationBLL.ObtainedMarks = totalmarksValidate;
            }

            if (lblTotal.Text.Trim().Length > 0)
            {
                _examEvaluationBLL.TotalMarks = Convert.ToDecimal(lblTotal.Text.ToString());
            }

            if (SubMarks != null)
                _examEvaluationBLL.SubMarks = SubMarks.ToString();
            else
                _examEvaluationBLL.SubMarks = null;

            if (txtRemarks.Text.Trim().Length > 0)
            {
                _examEvaluationBLL.Remarks = txtRemarks.Text.Trim().ToString();
            }
            else
            {
                _examEvaluationBLL.Remarks = null;
            }

            if (hfimage.Value.Trim().Length > 0)
            {
                string msExcelFilePathOnServer = ConfigurationSettings.AppSettings["FolderPath"] + "" + Convert.ToDateTime(System.DateTime.Now).ToString("ddMMMyyyy") + "/";

                bool exists = System.IO.Directory.Exists(msExcelFilePathOnServer);

                if (!exists)
                    System.IO.Directory.CreateDirectory(msExcelFilePathOnServer);

                string fileNameWitPath = msExcelFilePathOnServer + _examEvaluationBLL.ExamId + "_" + _examEvaluationBLL.UserId + "_" + _examEvaluationBLL.ImageNo + ".png";
                using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] data = Convert.FromBase64String(hfimage.Value.ToString());
                        bw.Write(data);
                        bw.Close();
                    }
                }
            }

            bool isValid = Validate();

            //if (!Convert.ToBoolean(hfEditMode.Value.ToString()))
            //{
            //    if (totalmarksValidate > 0)
            //    {
            //        if (lblObtained.Text.Trim().Length > 0)
            //        {
            //            if (Convert.ToDecimal(lblObtained.Text.Trim()) > 0)
            //            {
            //                totalmarksValidate += Convert.ToDecimal(lblObtained.Text.Trim());
            //            }
            //        }

            //        if (lblTotal.Text.Trim().Length > 0 && Convert.ToDecimal(totalmarksValidate) > 0)
            //        {
            //            if (Convert.ToDecimal(lblTotal.Text.Trim()) < Convert.ToDecimal(totalmarksValidate))
            //            {
            //                ShowErrors("err", "Obtained marks must be less then or equal to total marks.");
            //                isValid = false;
            //            }
            //        }
            //    }
            //}

            if (isValid == true)
            {
                _examEvaluationBLL.Save();

                if (Request.QueryString["ExamEvaluationId"] == null)
                {
                    if (!isNextPreviousClick)
                    {
                        Reset();
                        Session["_examEvaluationBLL"] = null;
                        Session["_examEvaluationBLL"] = new ExamEvaluationBLL();
                        _examEvaluationBLL = (ExamEvaluationBLL)Session["_examEvaluationBLL"];

                        Response.Redirect("ExamMarksEntryFilterRights.aspx");
                    }
                }
                else
                {
                    Session["_examEvaluationBLL"] = null;
                    Response.Redirect("ExamMarksEntryFilterRights.aspx");
                }

                pageEntryValid = true;
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
        Session["_examEvaluationBLL"] = null;

        //if (Request.QueryString["ExamEvaluationId"] == null)
        //{
        //    Response.Redirect("~/General/Default.aspx");
        //}
        //else
        //{
        Response.Redirect("ExamMarksEntryFilterRights.aspx");
        //}
    }

    #endregion

    static string path = ConfigurationManager.AppSettings["FolderPath"].ToString();
    [WebMethod]
    public static void UploadImage(string imageData, string examid, string userid, string photono)
    {
        //string fileNameWitPath = path + examid + "_" + userid + "_" + photono + ".png";

        //using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
        //{
        //    using (BinaryWriter bw = new BinaryWriter(fs))
        //    {
        //        byte[] data = Convert.FromBase64String(imageData);
        //        bw.Write(data);
        //        bw.Close();
        //    }
        //}
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            tblCanvas.Visible = true;
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void btnNext_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Convert.ToInt16(lblTotalImage.Text) > 1 && Convert.ToInt16(lblCurrentImage.Text) != Convert.ToInt16(lblTotalImage.Text))
            //{
            isNextPreviousClick = true;

            btnOK_Click(null, null);

            if (pageEntryValid)
                Response.Redirect("../Exams/ExamEvaluation.aspx?QueId=" + hfQueId.Value.ToString() +
                                               "&ExamId=" + hfExamId.Value.ToString() + "&RegistrationId=" + hfRegistrationId.Value.ToString() +
                                               "&ExamScheduleId=" + hfExamScheduleId.Value.ToString() + "&UserId=" + hfUserId.Value.ToString() +
                                               "&EmpName=" + lblEmployeeName.Text.ToString() + "&ImageNo=" + (Convert.ToInt16(Request.QueryString["ImageNo"].ToString()) + 1) + "");
            //}
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void btnPrevious_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToInt16(lblTotalImage.Text) > 1)
            {
                if ((Convert.ToInt16(lblCurrentImage.Text) - 1) > 0)
                {
                    isNextPreviousClick = true;

                    btnOK_Click(null, null);

                    if (pageEntryValid)
                        Response.Redirect("../Exams/ExamEvaluation.aspx?QueId=" + hfQueId.Value.ToString() +
                                                          "&ExamId=" + hfExamId.Value.ToString() + "&RegistrationId=" + hfRegistrationId.Value.ToString() +
                                                          "&ExamScheduleId=" + hfExamScheduleId.Value.ToString() + "&UserId=" + hfUserId.Value.ToString() +
                                                          "&EmpName=" + lblEmployeeName.Text.ToString() + "&ImageNo=" + (Convert.ToInt16(Request.QueryString["ImageNo"].ToString()) - 1) + "");
                }
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
            if (hfExamId.Value.Trim().Length > 0)
            {
                _examEvaluationBLL.Delete(hfExamId.Value.ToString());

                Session["_examEvaluationBLL"] = null;
                Session["_examEvaluationBLL"] = new ExamEvaluationBLL();
                _examEvaluationBLL = (ExamEvaluationBLL)Session["_examEvaluationBLL"];

                Response.Redirect("ExamMarksEntryFilterRights.aspx");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }

    protected void OnLinkClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        if (row != null)
        {
            Response.Write("Found it!");
        }
    }

    protected void btnRotateRight_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfExamId.Value.Trim().Length > 0)
                _examEvaluationBLL.ExamId = hfExamId.Value.Trim().ToString();
            else
                _examEvaluationBLL.ExamId = null;

            if (hfImageNo.Value.Trim().Length > 0)
                _examEvaluationBLL.ImageNo = Convert.ToInt16(hfImageNo.Value.Trim().ToString());
            else
                _examEvaluationBLL.ImageNo = null;

            String path = Server.MapPath(hfImagePath.Value.ToString().Replace(ConfigurationManager.AppSettings["FolderPathShow"].ToString(), "~/QusAnsImages/"));
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);

            string[] fileData = path.Split('.');

            string oExtension = fileData[fileData.Length - 1].ToString();

            string FileNameForInsert = path.Replace("." + oExtension, "");

            DateTime dt1 = new DateTime(1994, 6, 18, 14, 45, 44);
            DateTime dt2 = DateTime.Now;
            double totalminutes = (dt2 - dt1).TotalMinutes;

            string finaPath = FileNameForInsert + "_R90_" + totalminutes.ToString().Replace(".", "") + "." + oExtension;


            if (finaPath.Trim().Length > 240)
            {
                string[] fileString1 = finaPath.Split('_');

                if (fileString1.Length > 6)
                {
                    finaPath = fileString1[0].ToString() + "_" + fileString1[1].ToString() + "_" + fileString1[2].ToString() + "_" + fileString1[3].ToString() + "_" + fileString1[4].ToString() + "_" + fileString1[5].ToString() + "_R90_" + totalminutes.ToString().Replace(".", "") + "." + oExtension;
                }
            }

            if (finaPath != null)
                _examEvaluationBLL.RotatedImagePath = finaPath.ToString();
            else
                _examEvaluationBLL.RotatedImagePath = null;

            if (_examEvaluationBLL.RotatedImagePath != null)
            {
                img.Save(finaPath);

                _examEvaluationBLL.UpdatePhotoPath();

                Response.Redirect(Request.Url.AbsoluteUri);
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                ShowErrors("er", "System is busy !!! There is some issue in generate new image path please try after 10 to 20 seconds.");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message.ToString() + "... Try after 10 to 20 Seconds.");
        }
    }

    protected void btnRotateLeft_Click(object sender, EventArgs e)
    {
        try
        {
            String path = Server.MapPath(hfImagePath.Value.ToString().Replace(ConfigurationManager.AppSettings["FolderPathShow"].ToString(), "~/QusAnsImages/"));
            System.Drawing.Image img = System.Drawing.Image.FromFile(path);
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            img.Save(path);
            Response.Redirect(Request.Url.AbsoluteUri);
            Response.Redirect(Request.RawUrl);
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message.ToString() + "... Try after 10 to 20 Seconds.");
        }
    }
}
