using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Text;

public partial class General_ViewResultDetailAdv : System.Web.UI.Page
{
    private ResultDetailBLL _ResultDetailBLL = new ResultDetailBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (MySession.UserUnique != null)
        {
            if (!Page.IsPostBack)
            {
                //lblErrorMessage.Text = "Result will be informed to you later...!";
                //divHeaer.Visible = false;
                //divHeaer2.Visible = false;
                bool showresult = false;
                Int64 isShowResult = 0;

                if (1 == 1)
                {
                    DataTable dt = new DataTable();
                    dt = _ResultDetailBLL.GetResultFinalAdv(Request.QueryString["RegistrationId"].ToString(), Request.QueryString["ExamScheduleId"].ToString());

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["FirstName"] != DBNull.Value)
                            lblCandidateName.Text += dt.Rows[0]["FirstName"].ToString();

                        if (dt.Rows[0]["NoOFQuestions"] != DBNull.Value)
                            lblTotalQuestion.Text += Convert.ToDecimal(dt.Rows[0]["NoOFQuestions"].ToString());

                        if (dt.Rows[0]["NoOfTrueAnswers"] != DBNull.Value && dt.Rows[0]["NoOfWrongAnswers"] != DBNull.Value)
                            lblAttemptedAns.Text += Convert.ToDecimal(dt.Rows[0]["NoOfTrueAnswers"].ToString()) + Convert.ToDecimal(dt.Rows[0]["NoOfWrongAnswers"].ToString());

                        //if (dt.Rows[0]["NoOfTrueAnswers"] != DBNull.Value)
                        //    lblCorrectAns.Text += dt.Rows[0]["NoOfTrueAnswers"].ToString();

                        //if (dt.Rows[0]["NoOfWrongAnswers"] != DBNull.Value)
                        //    lblWrongAns.Text += dt.Rows[0]["NoOfWrongAnswers"].ToString();

                        if (dt.Rows[0]["NoOfUnAttemptedQuestions"] != DBNull.Value)
                            lblNoOfUnAttemptedQuestions.Text += dt.Rows[0]["NoOfUnAttemptedQuestions"].ToString();
                        if (dt.Rows[0]["TotalMarks"] != DBNull.Value)
                            TotalAchieved.Text += dt.Rows[0]["TotalMarks"].ToString();

                        if (dt.Rows[0]["iSShowResult"] != DBNull.Value)
                            isShowResult = Convert.ToInt64(dt.Rows[0]["iSShowResult"].ToString());

                        if (dt.Rows[0]["TotalMarks"] != DBNull.Value)
                        {
                            if (Convert.ToDecimal(dt.Rows[0]["TotalMarks"].ToString()) > 33)
                            {
                                lblStatus.Text += "Pass";
                            }
                            else
                            {
                                lblStatus.Text += "Fail";
                            }
                        }
                        else
                        {
                            lblStatus.Text += "Fail";
                        }

                        if (dt.Rows[0]["MobileNo"] != DBNull.Value)
                            lblMobileNo.Text += dt.Rows[0]["MobileNo"].ToString();

                        if (dt.Rows[0]["Standard"] != DBNull.Value)
                            lblStandard.Text += dt.Rows[0]["Standard"].ToString();

                        if (dt.Rows[0]["Subject"] != DBNull.Value)
                            lblSubject.Text += dt.Rows[0]["Subject"].ToString();

                        if (dt.Rows[0]["TestName"] != DBNull.Value)
                            lblTest.Text += dt.Rows[0]["TestName"].ToString();

                        //if (dt.Rows[0]["CollegeRespectiveSir"] != DBNull.Value)
                        //    lblCollegeRespectiveSir.Text += "<i>" + dt.Rows[0]["CollegeRespectiveSir"].ToString() + "</i>";

                        //if (dt.Rows[0]["CollegeRespectiveSirCoNo"] != DBNull.Value)
                        //    lblCollegeRespectiveSirCoNo.Text += "<i>" + dt.Rows[0]["CollegeRespectiveSirCoNo"].ToString() + "</i>";


                        //if (dt.Rows[0]["IdProofImageName"] != DBNull.Value)
                        //{
                        //    imgIdProof.ImageUrl = ConfigurationSettings.AppSettings["FileRetrivePath"].ToString() + dt.Rows[0]["IdProofImageName"].ToString();
                        //}
                        //if (dt.Rows[0]["CollegeIdProofImageName"] != DBNull.Value)
                        //{
                        //    imgIdCollegeId.ImageUrl = ConfigurationSettings.AppSettings["FileRetrivePath"].ToString() + dt.Rows[0]["CollegeIdProofImageName"].ToString();
                        //}
                    }


                    DataTable dtExamDetails = new DataTable();
                    //if (isShowResult >= 1)
                    //{
                    dtExamDetails = _ResultDetailBLL.GetExamDetailsAdv(Request.QueryString["RegistrationId"].ToString(), Request.QueryString["ExamScheduleId"].ToString());

                    Literal ltCases = new Literal();
                    StringBuilder strbuUser = new StringBuilder();

                    string AltColor = "EFEFEF,FEFEFE";

                    strbuUser.Append("<table style='width:100%;' class='table table-responsive'>");

                    string PrevEmp = "";
                    strbuUser.Append("<tr>");
                    strbuUser.Append("<th style='border:1px solid gray;text-align:center;' colspan='5'> Detailed Report </th>");
                    strbuUser.Append("</tr>");
                    if (dtExamDetails.Rows.Count > 0)
                    {
                        strbuUser.Append("<tr>");

                        strbuUser.Append("<th style='border:1px solid gray;text-align:center;' >Sr. No.</th>");
                        strbuUser.Append("<th style='border:1px solid gray;text-align:center;' >Question</th>");
                        strbuUser.Append("<th style='border:1px solid gray;'text-align:center; >Right Answer</th>");
                        strbuUser.Append("<th style='border:1px solid gray;'text-align:center; >Attempted Answer</th>");
                        strbuUser.Append("<th style='border:1px solid gray;text-align:center;' >Result</th>");
                        strbuUser.Append("</tr>");
                        decimal? TotalCouponQty = 0;
                        decimal? TotalAmount = 0;
                        for (int i = 0; i <= dtExamDetails.Rows.Count - 1; i++)
                        {
                            if (AltColor == "EFEFEF") AltColor = "FEFEFE"; else AltColor = "EFEFEF";
                            {
                                strbuUser.Append("<tr >");
                                strbuUser.Append("<td style='border:1px solid gray;text-align:center;'>" + (i + 1) + "</td>");

                                if (dtExamDetails.Rows[i]["Que"] != DBNull.Value)
                                {
                                    strbuUser.Append("<td style='border:1px solid gray;'>" + dtExamDetails.Rows[i]["Que"].ToString() + "");

                                    if (dtExamDetails.Rows[i]["ImageNameQus"] != DBNull.Value)
                                    {
                                        strbuUser.Append("<br /><img src='" + dtExamDetails.Rows[i]["ImageNameQus"].ToString() + "' >");//width='500px' height='100px'
                                    }
                                    strbuUser.Append("</td>");
                                }
                                else
                                {
                                    if (dtExamDetails.Rows[i]["ImageNameQus"] != DBNull.Value)
                                    {
                                        strbuUser.Append("<td style='border:1px solid gray;'>");

                                        strbuUser.Append("<img src='" + dtExamDetails.Rows[i]["ImageNameQus"].ToString() + "' >"); //width='500px' height='100px'

                                        strbuUser.Append("</td>");
                                    }
                                    else
                                    {
                                        strbuUser.Append("<td style='border:1px solid gray;'></td>");
                                    }
                                }

                                if (dtExamDetails.Rows[i]["OriginalAns"] != DBNull.Value)
                                {
                                    strbuUser.Append("<td style='border:1px solid gray;'>" + dtExamDetails.Rows[i]["OriginalAns"].ToString() + "</td>");
                                }
                                else
                                {
                                    if (dtExamDetails.Rows[i]["OriginalAnsImage"] != DBNull.Value)
                                    {
                                        strbuUser.Append("<td style='border:1px solid gray;'><img src='" + dtExamDetails.Rows[i]["OriginalAnsImage"].ToString() + "' width='100px' height='50px'></td>");
                                    }
                                    else
                                    {
                                        strbuUser.Append("<td style='border:1px solid gray;'></td>");
                                    }
                                }

                                if (dtExamDetails.Rows[i]["FilledAns"] != DBNull.Value)
                                {
                                    strbuUser.Append("<td style='border:1px solid gray;'>" + dtExamDetails.Rows[i]["FilledAns"].ToString() + "</td>");
                                }
                                else
                                {
                                    if (dtExamDetails.Rows[i]["FilledAnsImage"] != DBNull.Value)
                                    {
                                        strbuUser.Append("<td style='border:1px solid gray;'><img src='" + dtExamDetails.Rows[i]["FilledAnsImage"].ToString() + "' width='100px' height='50px'></td>");
                                    }
                                    else
                                    {
                                        strbuUser.Append("<td style='border:1px solid gray;'></td>");
                                    }
                                }

                                if (dtExamDetails.Rows[i]["FilledAns"] != DBNull.Value)
                                {
                                    //if (dtExamDetails.Rows[i]["FilledAns"].ToString().ToUpper() == "Skipped".ToString().ToUpper())
                                    //    strbuUser.Append("<td style='border:1px solid gray;'>N/A</td>");
                                    //else if (dtExamDetails.Rows[i]["FilledAns"].ToString().ToUpper() == dtExamDetails.Rows[i]["OriginalAns"].ToString().ToUpper())
                                    //    strbuUser.Append("<td style='border:1px solid gray;background-color: greenyellow;'>" + dtExamDetails.Rows[i]["TotalMarks"].ToString() + "</td>");
                                    //else
                                    //    strbuUser.Append("<td style='border:1px solid gray;background-color: lightcoral;'>" + dtExamDetails.Rows[i]["TotalMarks"].ToString() + "</td>");
                                    try
                                    {
                                        if (dtExamDetails.Rows[i]["FilledAns"].ToString().ToUpper() == "Skipped".ToString().ToUpper())
                                            strbuUser.Append("<td style='border:1px solid gray;'>N/A</td>");
                                        else if (Convert.ToDouble(dtExamDetails.Rows[i]["TotalMarks"].ToString()) > 0) // Consider True Ans
                                            strbuUser.Append("<td style='border:1px solid gray;background-color: greenyellow;'>" + dtExamDetails.Rows[i]["TotalMarks"].ToString() + "</td>");
                                        else
                                            strbuUser.Append("<td style='border:1px solid gray;background-color: lightcoral;'>" + dtExamDetails.Rows[i]["TotalMarks"].ToString() + "</td>");
                                    }
                                    catch
                                    {
                                        strbuUser.Append("<td style='border:1px solid gray;'>-</td>");
                                    }
                                }
                                else
                                {
                                    if (dtExamDetails.Rows[i]["FilledAnsImage"].ToString().ToUpper() == "Skipped".ToString().ToUpper())
                                        strbuUser.Append("<td style='border:1px solid gray;'>N/A</td>");
                                    else if (dtExamDetails.Rows[i]["FilledAnsImage"].ToString().ToUpper() == dtExamDetails.Rows[i]["OriginalAnsImage"].ToString().ToUpper())
                                        strbuUser.Append("<td style='border:1px solid gray;background-color: greenyellow;'>" + dtExamDetails.Rows[i]["TotalMarks"].ToString() + "</td>");
                                    else if (dtExamDetails.Rows[i]["FilledAnsImage"].ToString().ToUpper() != dtExamDetails.Rows[i]["OriginalAnsImage"].ToString().ToUpper())
                                        strbuUser.Append("<td style='border:1px solid gray;background-color: lightcoral;'>" + dtExamDetails.Rows[i]["TotalMarks"].ToString() + "</td>");
                                    else
                                        strbuUser.Append("<td style='border:1px solid gray;'></td>");
                                }

                                strbuUser.Append("</tr>");
                            }
                        }
                    }
                    else
                    {
                        strbuUser.Append("<tr>");
                        strbuUser.Append("<td colspan='5' style='border:1px solid gray;'> No Records Found..! </td>");
                        strbuUser.Append("</tr>");
                    }

                    strbuUser.Append("</table>");

                    ltCases.Text = strbuUser.ToString();
                    plRpt.Controls.Clear();
                    plRpt.Controls.Add(ltCases);
                    //}
                }
            }
        }
        else
        {
            Response.Redirect("~/Logout.aspx");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/General/CertificateFormDetail.aspx");

    }
}