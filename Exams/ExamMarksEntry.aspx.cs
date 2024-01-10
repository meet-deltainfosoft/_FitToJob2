using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Collections;

public partial class Exams_ExamMarksEntry : System.Web.UI.Page
{
    private StringBuilder sbjQueryNumeric = new StringBuilder();
    private ExamMarksEntryBLL _examMarksEntryBLL = new ExamMarksEntryBLL();

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (MySession.UserUnique != null)
            {
                if (!Page.IsPostBack)
                {
                    ShowDetails(Request.QueryString["RegistrationId"].ToString(), Request.QueryString["ExamScheduleId"].ToString());
                }
            }
            else
            {
                Response.Redirect("~/Logout.aspx");
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("err", ex.Message.ToString());
        }
    }
    #endregion

    #region Other Functions
    protected void ShowDetails(string RegistrationId, string ExamScheduleId)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = _examMarksEntryBLL.GetResultFinal(RegistrationId, ExamScheduleId);

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["MobileNo"] != DBNull.Value)
                    lblMobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
                else
                    lblMobileNo.Text = "";

                if (dt.Rows[0]["Standard"] != DBNull.Value)
                    lblStandard.Text = dt.Rows[0]["Standard"].ToString();
                else
                    lblStandard.Text = "";

                if (dt.Rows[0]["Subject"] != DBNull.Value)
                    lblSubject.Text = dt.Rows[0]["Subject"].ToString();
                else
                    lblSubject.Text = "";

                if (dt.Rows[0]["TestName"] != DBNull.Value)
                    lblTest.Text = dt.Rows[0]["TestName"].ToString();
                else
                    lblTest.Text = "";

                if (dt.Rows[0]["FirstName"] != DBNull.Value)
                    lblStudentName.Text = dt.Rows[0]["FirstName"].ToString();
                else
                    lblStudentName.Text = "";

                if (dt.Rows[0]["StandardId"] != DBNull.Value)
                    _examMarksEntryBLL._PrevStandardId = dt.Rows[0]["StandardId"].ToString();

                if (dt.Rows[0]["SubId"] != DBNull.Value)
                    _examMarksEntryBLL._PrevSubId = dt.Rows[0]["SubId"].ToString();
            }
            else
            {
                lblMobileNo.Text = "";
                lblStandard.Text = "";
                lblSubject.Text = "";
                lblTest.Text = "";
                lblStudentName.Text = "";
            }

            DataTable dtExamDetails = new DataTable();
            dtExamDetails = _examMarksEntryBLL.GetExamDetails(RegistrationId, ExamScheduleId);
            sbjQueryNumeric.AppendLine("<script type=\"text/javascript\">");
            sbjQueryNumeric.AppendLine("$(document).ready(function() {");
            gdvMarks.DataSource = dtExamDetails;
            gdvMarks.DataBind();
            sbjQueryNumeric.AppendLine(" });");
            sbjQueryNumeric.AppendLine("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "" + RegistrationId + "", sbjQueryNumeric.ToString());

        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("err", ex.Message.ToString());
        }
    }
    public void FilterCriteria()
    {
        try
        {
            if (Request.QueryString["StandardId"] != null && Request.QueryString["StandardId"] != "")
                _examMarksEntryBLL.StandardId = Request.QueryString["StandardId"].ToString();
            else
                _examMarksEntryBLL.StandardId = null;

            if (Request.QueryString["SubjectId"] != null && Request.QueryString["SubjectId"] != "")
                _examMarksEntryBLL.SubjectId = Request.QueryString["SubjectId"].ToString();
            else
                _examMarksEntryBLL.SubjectId = null;

            if (Request.QueryString["TestId"] != null && Request.QueryString["TestId"] != "")
                _examMarksEntryBLL.TestId = Request.QueryString["TestId"].ToString();
            else
                _examMarksEntryBLL.TestId = null;

            _examMarksEntryBLL.Top = " Top 1";
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    #endregion

    #region Grid Row Data Bound
    protected void gdvMarks_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                Label lblQues = (Label)e.Row.FindControl("lblQues");
                Image imgqusPics = (Image)e.Row.FindControl("imgqusPics");
                HyperLink hlImageQus = (HyperLink)e.Row.FindControl("hlImageQus");
                Label lblRightAns = (Label)e.Row.FindControl("lblRightAns");
                Image imgqusPicsRight = (Image)e.Row.FindControl("imgqusPicsRight");
                HyperLink hlImageAnsRight = (HyperLink)e.Row.FindControl("hlImageAnsRight");
                Label lblAttemptedAns = (Label)e.Row.FindControl("lblAttemptedAns");
                Image imgqusPicsAttempted = (Image)e.Row.FindControl("imgqusPicsAttempted");
                HyperLink hlImageAnsAttempted = (HyperLink)e.Row.FindControl("hlImageAnsAttempted");
                TextBox txtgdvObtianedMarks = (TextBox)e.Row.FindControl("txtgdvObtianedMarks");
                LinkButton lnkDownloadQues = (LinkButton)e.Row.FindControl("lnkDownloadQues");
                LinkButton lnkDownloadRightAns = (LinkButton)e.Row.FindControl("lnkDownloadRightAns");
                LinkButton lnkDownloadAttemptedAns = (LinkButton)e.Row.FindControl("lnkDownloadAttemptedAns");
                //HyperLink hldwnld = (HyperLink)e.Row.FindControl("hldwnld");

                TextBox txtgdvMarks1 = (TextBox)e.Row.FindControl("txtgdvMarks1");
                TextBox txtgdvMarks2 = (TextBox)e.Row.FindControl("txtgdvMarks2");
                TextBox txtgdvMarks3 = (TextBox)e.Row.FindControl("txtgdvMarks3");
                TextBox txtgdvMarks4 = (TextBox)e.Row.FindControl("txtgdvMarks4");
                TextBox txtgdvMarks5 = (TextBox)e.Row.FindControl("txtgdvMarks5");
                TextBox txtgdvMarks6 = (TextBox)e.Row.FindControl("txtgdvMarks6");


                //hldwnld.NavigateUrl = "http://110.227.253.77:90/iExamAPI/Temp/FinalADCPaperstyle.pdf";
                if (drv["Que"] != DBNull.Value)
                    lblQues.Text = drv["Que"].ToString();
                else
                {
                    if (drv["ImageNameQus"] != DBNull.Value)
                    {
                        hlImageQus.NavigateUrl = drv["ImageNameQus"].ToString();
                        imgqusPics.ImageUrl = drv["ImageNameQus"].ToString();
                        if(drv["QueType"] != null )
                        {
                            if (drv["QueType"].ToString().ToUpper() == "PDF")
                            {
                                lnkDownloadQues.CommandArgument = drv["ImageNameQus"].ToString();
                                lnkDownloadQues.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        imgqusPics.ImageUrl = "";
                        hlImageQus.NavigateUrl = "";
                        lnkDownloadQues.Visible = false;
                    }
                }

                if (drv["OriginalAns"] != DBNull.Value)
                    lblRightAns.Text = drv["OriginalAns"].ToString();
                else
                {
                    if (drv["OriginalAnsImage"] != DBNull.Value)
                    {
                        imgqusPicsRight.ImageUrl = drv["OriginalAnsImage"].ToString();
                        hlImageAnsRight.NavigateUrl = drv["OriginalAnsImage"].ToString();
                        if (drv["QueType"] != null)
                        {
                            if (drv["QueType"].ToString().ToUpper() == "PDF")
                            {
                                lnkDownloadRightAns.CommandArgument = drv["OriginalAnsImage"].ToString();
                                lnkDownloadRightAns.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        imgqusPicsRight.ImageUrl = "";
                        hlImageAnsRight.NavigateUrl = "";
                        lnkDownloadRightAns.Visible = false;
                    }
                }

                if (drv["FilledAns"] != DBNull.Value)
                {
                    lblAttemptedAns.Text = drv["FilledAns"].ToString();
                }
                else
                {
                    if (drv["FilledAnsImage"] != DBNull.Value)
                    {
                        imgqusPicsAttempted.ImageUrl = drv["FilledAnsImage"].ToString();
                        hlImageAnsAttempted.NavigateUrl = drv["OriginalAnsImage"].ToString();
                        if (drv["QueType"] != null)
                        {
                            if (drv["QueType"].ToString().ToUpper() == "PDF")
                            {
                                lnkDownloadAttemptedAns.CommandArgument = drv["OriginalAnsImage"].ToString();
                                lnkDownloadAttemptedAns.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        imgqusPicsAttempted.ImageUrl = "";
                        hlImageAnsAttempted.NavigateUrl = "";
                        lnkDownloadAttemptedAns.Visible = false;
                    }
                }

                sbjQueryNumeric.AppendLine("$(\"#" + txtgdvObtianedMarks.ClientID + "\").numeric();");

                sbjQueryNumeric.AppendLine("$(\"#" + txtgdvMarks1.ClientID + "\").numeric();");
                sbjQueryNumeric.AppendLine("$(\"#" + txtgdvMarks2.ClientID + "\").numeric();");
                sbjQueryNumeric.AppendLine("$(\"#" + txtgdvMarks3.ClientID + "\").numeric();");
                sbjQueryNumeric.AppendLine("$(\"#" + txtgdvMarks4.ClientID + "\").numeric();");
                sbjQueryNumeric.AppendLine("$(\"#" + txtgdvMarks5.ClientID + "\").numeric();");
                sbjQueryNumeric.AppendLine("$(\"#" + txtgdvMarks6.ClientID + "\").numeric();");

                sbjQueryNumeric.AppendLine("$(\"#" + txtgdvMarks1.ClientID + "\").change(function() {");
                sbjQueryNumeric.AppendLine("CalcTotalMarks(" + txtgdvObtianedMarks.ClientID + "," + txtgdvMarks1.ClientID + "," + txtgdvMarks2.ClientID + "," + txtgdvMarks3.ClientID + "," + txtgdvMarks4.ClientID + "," + txtgdvMarks5.ClientID + "," + txtgdvMarks6.ClientID + ");");
                sbjQueryNumeric.AppendLine("});");

                sbjQueryNumeric.AppendLine("$(\"#" + txtgdvMarks2.ClientID + "\").change(function() {");
                sbjQueryNumeric.AppendLine("CalcTotalMarks(" + txtgdvObtianedMarks.ClientID + "," + txtgdvMarks1.ClientID + "," + txtgdvMarks2.ClientID + "," + txtgdvMarks3.ClientID + "," + txtgdvMarks4.ClientID + "," + txtgdvMarks5.ClientID + "," + txtgdvMarks6.ClientID + ");");
                sbjQueryNumeric.AppendLine("});");

                sbjQueryNumeric.AppendLine("$(\"#" + txtgdvMarks3.ClientID + "\").change(function() {");
                sbjQueryNumeric.AppendLine("CalcTotalMarks(" + txtgdvObtianedMarks.ClientID + "," + txtgdvMarks1.ClientID + "," + txtgdvMarks2.ClientID + "," + txtgdvMarks3.ClientID + "," + txtgdvMarks4.ClientID + "," + txtgdvMarks5.ClientID + "," + txtgdvMarks6.ClientID + ");");
                sbjQueryNumeric.AppendLine("});");

                sbjQueryNumeric.AppendLine("$(\"#" + txtgdvMarks4.ClientID + "\").change(function() {");
                sbjQueryNumeric.AppendLine("CalcTotalMarks(" + txtgdvObtianedMarks.ClientID + "," + txtgdvMarks1.ClientID + "," + txtgdvMarks2.ClientID + "," + txtgdvMarks3.ClientID + "," + txtgdvMarks4.ClientID + "," + txtgdvMarks5.ClientID + "," + txtgdvMarks6.ClientID + ");");
                sbjQueryNumeric.AppendLine("});");

                sbjQueryNumeric.AppendLine("$(\"#" + txtgdvMarks5.ClientID + "\").change(function() {");
                sbjQueryNumeric.AppendLine("CalcTotalMarks(" + txtgdvObtianedMarks.ClientID + "," + txtgdvMarks1.ClientID + "," + txtgdvMarks2.ClientID + "," + txtgdvMarks3.ClientID + "," + txtgdvMarks4.ClientID + "," + txtgdvMarks5.ClientID + "," + txtgdvMarks6.ClientID + ");");
                sbjQueryNumeric.AppendLine("});");

                sbjQueryNumeric.AppendLine("$(\"#" + txtgdvMarks6.ClientID + "\").change(function() {");
                sbjQueryNumeric.AppendLine("CalcTotalMarks(" + txtgdvObtianedMarks.ClientID + "," + txtgdvMarks1.ClientID + "," + txtgdvMarks2.ClientID + "," + txtgdvMarks3.ClientID + "," + txtgdvMarks4.ClientID + "," + txtgdvMarks5.ClientID + "," + txtgdvMarks6.ClientID + ");");
                sbjQueryNumeric.AppendLine("});");
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("err", ex.Message.ToString());
        }
    }
    #endregion

    #region Show and Hide Error
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
            HideErrors();
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
            HideErrors();
            ShowErrors("err", ex.Message);
        }
    }
    #endregion

    #region Click event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            ArrayList al = new ArrayList();
            foreach (GridViewRow gvr in gdvMarks.Rows)
            {
                TextBox txtgdvObtianedMarks = (TextBox)gvr.FindControl("txtgdvObtianedMarks");
                TextBox txtgdvMarks1 = (TextBox)gvr.FindControl("txtgdvMarks1");
                TextBox txtgdvMarks2 = (TextBox)gvr.FindControl("txtgdvMarks2");
                TextBox txtgdvMarks3 = (TextBox)gvr.FindControl("txtgdvMarks3");
                TextBox txtgdvMarks4 = (TextBox)gvr.FindControl("txtgdvMarks4");
                TextBox txtgdvMarks5 = (TextBox)gvr.FindControl("txtgdvMarks5");
                TextBox txtgdvMarks6 = (TextBox)gvr.FindControl("txtgdvMarks6");

                if (txtgdvObtianedMarks.Text.Trim().Length > 0)
                    al.Add(gdvMarks.DataKeys[gvr.RowIndex].Values[0].ToString() + "|" + gdvMarks.DataKeys[gvr.RowIndex].Values[1].ToString() + "|" + gdvMarks.DataKeys[gvr.RowIndex].Values[2].ToString() + "|" + txtgdvObtianedMarks.Text.Trim() + "|" + txtgdvMarks1.Text.Trim() + "|" + txtgdvMarks2.Text.Trim() + "|" + txtgdvMarks3.Text.Trim() + "|" + txtgdvMarks4.Text.Trim() + "|" + txtgdvMarks5.Text.Trim() + "|" + txtgdvMarks6.Text.Trim());
            }

            if (al.Count > 0)
            {
                _examMarksEntryBLL.Update(al);
                ShowErrors("err", "Marks Inserted successfully.");
                Response.Redirect("ExamMarksEntryFilter.aspx");
            }
            else
            {
                ShowErrors("err", "Please enter Marks for atleast one Record.");
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("err", ex.Message);
        }
    }
    protected void btnSaveAndNext_Click(object sender, EventArgs e)
    {
        try
        {
            HideErrors();
            ArrayList al = new ArrayList();
            foreach (GridViewRow gvr in gdvMarks.Rows)
            {
                TextBox txtgdvObtianedMarks = (TextBox)gvr.FindControl("txtgdvObtianedMarks");
                TextBox txtgdvMarks1 = (TextBox)gvr.FindControl("txtgdvMarks1");
                TextBox txtgdvMarks2 = (TextBox)gvr.FindControl("txtgdvMarks2");
                TextBox txtgdvMarks3 = (TextBox)gvr.FindControl("txtgdvMarks3");
                TextBox txtgdvMarks4 = (TextBox)gvr.FindControl("txtgdvMarks4");
                TextBox txtgdvMarks5 = (TextBox)gvr.FindControl("txtgdvMarks5");
                TextBox txtgdvMarks6 = (TextBox)gvr.FindControl("txtgdvMarks6");

                if (txtgdvObtianedMarks.Text.Trim().Length > 0)
                    al.Add(gdvMarks.DataKeys[gvr.RowIndex].Values[0].ToString() + "|" + gdvMarks.DataKeys[gvr.RowIndex].Values[1].ToString() + "|" + gdvMarks.DataKeys[gvr.RowIndex].Values[2].ToString() + "|" + txtgdvObtianedMarks.Text.Trim() + "|" + txtgdvMarks1.Text.Trim() + "|" + txtgdvMarks2.Text.Trim() + "|" + txtgdvMarks3.Text.Trim() + "|" + txtgdvMarks4.Text.Trim() + "|" + txtgdvMarks5.Text.Trim() + "|" + txtgdvMarks6.Text.Trim());
            }

            if (al.Count > 0)
            {
                _examMarksEntryBLL.Update(al);
                ShowErrors("err", "Marks Inserted successfully.");
                FilterCriteria();
                DataTable dt = _examMarksEntryBLL.ResultDetail();
                if (dt.Rows.Count > 0)
                {
                    ShowDetails(dt.Rows[0]["RegistrationId"].ToString(), dt.Rows[0]["ExamScheduleId"].ToString());
                }
                else
                    Response.Redirect("ExamMarksEntryFilter.aspx");
            }
            else
            {
                ShowErrors("err", "Please enter Marks for atleast one Record.");
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("err", ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("ExamMarksEntryFilter.aspx");
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("err", ex.Message.ToString());
        }
    }
    #endregion

    protected void gdvMarks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AppendHeader("Content-Disposition", "filename=" + e.CommandArgument);
        //Response.TransmitFile("" + e.CommandArgument);
       Response.Redirect("" + e.CommandArgument);
        //Response.Redirect("http://110.227.253.77:90/iExamAPI/Temp/FinalADCPaperstyle.pdf");
        //Response.TransmitFile("E:\\Delta_Work\\Temp\\" + e.CommandArgument);
        Response.End();
    }
}