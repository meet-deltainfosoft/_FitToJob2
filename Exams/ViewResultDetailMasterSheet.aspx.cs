using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Text;

public partial class General_ViewResultDetailMasterSheet : System.Web.UI.Page
{
    private ResultDetailBLL _ResultDetailBLL = new ResultDetailBLL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (MySession.UserUnique != null)
        {
            if (!Page.IsPostBack)
            {
                DataTable dt = new DataTable();
                dt = _ResultDetailBLL.GetResultFinalMasterSheet(Request.QueryString["TestId"].ToString());

                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["FirstName"] != DBNull.Value)
                        lblCandidateName.Text += dt.Rows[0]["FirstName"].ToString();

                    if (dt.Rows[0]["NoOFQuestions"] != DBNull.Value)
                        lblTotalQuestion.Text += Convert.ToDecimal(dt.Rows[0]["NoOFQuestions"].ToString());

                    if (dt.Rows[0]["NoOfTrueAnswers"] != DBNull.Value && dt.Rows[0]["NoOfWrongAnswers"] != DBNull.Value)
                        lblAttemptedAns.Text += Convert.ToDecimal(dt.Rows[0]["NoOfTrueAnswers"].ToString()) + Convert.ToDecimal(dt.Rows[0]["NoOfWrongAnswers"].ToString());

                    if (dt.Rows[0]["NoOfTrueAnswers"] != DBNull.Value)
                        lblCorrectAns.Text += dt.Rows[0]["NoOfTrueAnswers"].ToString();

                    if (dt.Rows[0]["NoOfWrongAnswers"] != DBNull.Value)
                        lblWrongAns.Text += dt.Rows[0]["NoOfWrongAnswers"].ToString();

                    if (dt.Rows[0]["TotalMarks"] != DBNull.Value)
                        TotalAchieved.Text += dt.Rows[0]["TotalMarks"].ToString();


                    if (Convert.ToDecimal(dt.Rows[0]["TotalMarks"].ToString()) > 33)
                    {
                        lblStatus.Text += "Pass";
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

                dtExamDetails = _ResultDetailBLL.GetExamDetailsMaster(Request.QueryString["TestId"].ToString());

                Literal ltCases = new Literal();
                StringBuilder strbuUser = new StringBuilder();

                string AltColor = "EFEFEF,FEFEFE";

                strbuUser.Append("<table style='width:100%;' class='table table-responsive'>");

                string PrevEmp = "";
                strbuUser.Append("<tr>");
                strbuUser.Append("<th style='border:1px solid gray;text-align:center;' colspan='9'> Detailed Report </th>");
                strbuUser.Append("</tr>");
                if (dtExamDetails.Rows.Count > 0)
                {
                    strbuUser.Append("<tr>");

                    strbuUser.Append("<th style='border:1px solid gray;text-align:center;' >Sr. No.</th>");
                    strbuUser.Append("<th style='border:1px solid gray;text-align:center;' >Subject</th>");
                    strbuUser.Append("<th style='border:1px solid gray;text-align:center;' >Question</th>");
                    strbuUser.Append("<th style='border:1px solid gray;'text-align:center; >A1</th>");
                    strbuUser.Append("<th style='border:1px solid gray;'text-align:center; >A2</th>");
                    strbuUser.Append("<th style='border:1px solid gray;'text-align:center; >A3</th>");
                    strbuUser.Append("<th style='border:1px solid gray;'text-align:center; >A4</th>");
                    strbuUser.Append("<th style='border:1px solid gray;text-align:center;' >ANS</th>");
                    strbuUser.Append("<th style='border:1px solid gray;'text-align:center; >ANS</th>");
                    strbuUser.Append("</tr>");
                    decimal? TotalCouponQty = 0;
                    decimal? TotalAmount = 0;
                    int ii = 0;
                    int dd = 0;
                    for (int i = 0; i <= dtExamDetails.Rows.Count - 1; i++)
                    {
                        //ii++;
                        //while ((dtExamDetails.Rows[i]["SrNo"].ToString() != (ii).ToString()) && dd <= 100)
                        //{
                        //    dd++;
                        //    strbuUser.Append("<tr>");

                        //    strbuUser.Append("<td style='border:1px solid gray;text-align:center;' >" + (ii).ToString() + "</td>");
                        //    strbuUser.Append("<td style='border:1px solid gray;text-align:center;' > </td>");
                        //    strbuUser.Append("<td style='border:1px solid gray;'text-align:center; > </td>");
                        //    strbuUser.Append("<td style='border:1px solid gray;'text-align:center; > </td>");
                        //    strbuUser.Append("<td style='border:1px solid gray;'text-align:center; > </td>");
                        //    strbuUser.Append("<td style='border:1px solid gray;'text-align:center; > </td>");
                        //    strbuUser.Append("<td style='border:1px solid gray;'text-align:center; > </td>");
                        //    strbuUser.Append("<td style='border:1px solid gray;text-align:center;' > </td>");
                        //    strbuUser.Append("</tr>");
                        //    ii++;
                        //}

                        if (AltColor == "EFEFEF") AltColor = "FEFEFE"; else AltColor = "EFEFEF";
                        {
                            strbuUser.Append("<tr >");
                            strbuUser.Append("<td style='border:1px solid gray;text-align:center;'>" + dtExamDetails.Rows[i]["SrNo"].ToString() + "</td>");

                            if (dtExamDetails.Rows[i]["Name"] != DBNull.Value)
                                strbuUser.Append("<td style='border:1px solid gray;text-align:center;'>" + dtExamDetails.Rows[i]["Name"].ToString() + "</td>");

                            if (dtExamDetails.Rows[i]["Que"] != DBNull.Value)
                            {
                                strbuUser.Append("<td style='border:1px solid gray;'>" + dtExamDetails.Rows[i]["Que"].ToString() + "");

                                if (dtExamDetails.Rows[i]["ImageNameQus"] != DBNull.Value)
                                {
                                    strbuUser.Append("<br /><img src='" + dtExamDetails.Rows[i]["ImageNameQus"].ToString() + "' width='500px' height='100px'>");
                                }
                                strbuUser.Append("</td>");
                            }
                            else
                            {
                                if (dtExamDetails.Rows[i]["ImageNameQus"] != DBNull.Value)
                                {
                                    strbuUser.Append("<td style='border:1px solid gray;text-align:center;'>");
                                    strbuUser.Append("<img src='" + dtExamDetails.Rows[i]["ImageNameQus"].ToString() + "' width='500px' height='100px'>");
                                    strbuUser.Append("</td>");
                                }
                                else
                                {
                                    strbuUser.Append("<td style='border:1px solid gray;'></td>");
                                }
                            }

                            int ai = 1;
                            if (dtExamDetails.Rows[i]["FilledAns" + ai] != DBNull.Value)
                            {
                                strbuUser.Append("<td style='border:1px solid gray;'>" + dtExamDetails.Rows[i]["FilledAns" + ai].ToString() + "</td>");
                            }
                            else
                            {
                                if (dtExamDetails.Rows[i]["FilledAnsImage" + ai] != DBNull.Value)
                                {
                                    strbuUser.Append("<td style='border:1px solid gray;'><img src='" + dtExamDetails.Rows[i]["FilledAnsImage" + ai].ToString() + "' width='100px' height='50px'></td>");
                                }
                                else
                                {
                                    strbuUser.Append("<td style='border:1px solid gray;'></td>");
                                }
                            }
                            ai++;
                            if (dtExamDetails.Rows[i]["FilledAns" + ai] != DBNull.Value)
                            {
                                strbuUser.Append("<td style='border:1px solid gray;'>" + dtExamDetails.Rows[i]["FilledAns" + ai].ToString() + "</td>");
                            }
                            else
                            {
                                if (dtExamDetails.Rows[i]["FilledAnsImage" + ai] != DBNull.Value)
                                {
                                    strbuUser.Append("<td style='border:1px solid gray;'><img src='" + dtExamDetails.Rows[i]["FilledAnsImage" + ai].ToString() + "' width='100px' height='50px'></td>");
                                }
                                else
                                {
                                    strbuUser.Append("<td style='border:1px solid gray;'></td>");
                                }
                            }
                            ai++;
                            if (dtExamDetails.Rows[i]["FilledAns" + ai] != DBNull.Value)
                            {
                                strbuUser.Append("<td style='border:1px solid gray;'>" + dtExamDetails.Rows[i]["FilledAns" + ai].ToString() + "</td>");
                            }
                            else
                            {
                                if (dtExamDetails.Rows[i]["FilledAnsImage" + ai] != DBNull.Value)
                                {
                                    strbuUser.Append("<td style='border:1px solid gray;'><img src='" + dtExamDetails.Rows[i]["FilledAnsImage" + ai].ToString() + "' width='100px' height='50px'></td>");
                                }
                                else
                                {
                                    strbuUser.Append("<td style='border:1px solid gray;'></td>");
                                }
                            }
                            ai++;
                            if (dtExamDetails.Rows[i]["FilledAns" + ai] != DBNull.Value)
                            {
                                strbuUser.Append("<td style='border:1px solid gray;'>" + dtExamDetails.Rows[i]["FilledAns" + ai].ToString() + "</td>");
                            }
                            else
                            {
                                if (dtExamDetails.Rows[i]["FilledAnsImage" + ai] != DBNull.Value)
                                {
                                    strbuUser.Append("<td style='border:1px solid gray;'><img src='" + dtExamDetails.Rows[i]["FilledAnsImage" + ai].ToString() + "' width='100px' height='50px'></td>");
                                }
                                else
                                {
                                    strbuUser.Append("<td style='border:1px solid gray;'></td>");
                                }
                            }
                            if (dtExamDetails.Rows[i]["Ans"].ToString() == "1")
                                strbuUser.Append("<td style='border:1px solid gray;text-align:center;'>A</td>");
                            else if (dtExamDetails.Rows[i]["Ans"].ToString() == "2")
                                strbuUser.Append("<td style='border:1px solid gray;text-align:center;'>B</td>");
                            else if (dtExamDetails.Rows[i]["Ans"].ToString() == "3")
                                strbuUser.Append("<td style='border:1px solid gray;text-align:center;'>C</td>");
                            else if (dtExamDetails.Rows[i]["Ans"].ToString() == "4")
                                strbuUser.Append("<td style='border:1px solid gray;text-align:center;'>D</td>");

                            strbuUser.Append("<td style='border:1px solid gray;text-align:center;'>" + dtExamDetails.Rows[i]["Ans"].ToString() + "</td>");

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