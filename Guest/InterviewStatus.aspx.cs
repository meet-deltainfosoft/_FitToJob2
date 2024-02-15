using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net;
using System.IO;
using System.Web.UI.HtmlControls;

public partial class Guest_InterviewStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty((string)Session["MobileNo"]) || string.IsNullOrEmpty((string)Session["Language"]))
            {
                Response.Redirect("~/Guest/SelectLanguage.aspx");
            }   
            else
            {
                string CandidateId = GetCandidateIdByMobileNo(Session["MobileNo"].ToString());
                DataTable dt = new DataTable();
                dt = Interview_Status(CandidateId);

                rptInterviewstatus.DataSource = dt;
                rptInterviewstatus.DataBind();
            }
        }
    }

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
            ShowErrors("Error", ex.Message);
        }
    }

    private DataTable GetProfileDetail(string MobileNo)
    {
        DataTable dt = new DataTable();
        if (MobileNo != null && MobileNo != "")
        {

            GeneralDAL objDal = new GeneralDAL();
            SqlCommand Cmd = new SqlCommand();
            objDal.OpenSQLConnection();
            Cmd.Connection = objDal.ActiveSQLConnection();
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = "SELECT SJ.JobId,R.RegistrationId,R.CandidateId,R.Status, " +
                              " SC.SubCategory JobProfile,Isnull(SJ.RejectionRemark,'')Remarks, " +
                              " Format(SJ.InsertedOn,'dd-MMM-yyyy hh:mm') InterviewDateTime, " +
                              " Format(GetDate(),'dd/MM/yyyy hh:mm') ScheduledTime, " +
                              " format(R.ApprovedDisapprovedOn,'dd-MMM-yyyy') ApprovedDisapprovedOn, " +
                              " format(R.Insertedon,'dd-MMM-yyyy')InsertedOn " +
                              " FROM Registrations R  " +
                              " Join SubJobOfferings SJ on SJ.CandidateId = R.CandidateId " +
                              " Join SubCategory SC on SC.SubCategoryId = SJ.SubCategoryId " +
                              " where R.MobileNo = '" + MobileNo + "'";
            dt.Load(Cmd.ExecuteReader());

        }
        return dt;
    }

    protected void rptInterviewstatus_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView dataRowView = (DataRowView)e.Item.DataItem;

            //Profile Created
            HtmlGenericControl divProfileCreated = (HtmlGenericControl)e.Item.FindControl("divProfileCreated");
            HtmlGenericControl divSubOfferCard = (HtmlGenericControl)e.Item.FindControl("divSubOfferCard");
            Label lblProfileCreatedDate = (Label)e.Item.FindControl("lblProfileCreatedDate");
            HtmlGenericControl iProfileCreated = (HtmlGenericControl)e.Item.FindControl("iProfileCreated");

            //Profile Short List
            HtmlGenericControl divProfileShortList = (HtmlGenericControl)e.Item.FindControl("divProfileShortList");
            HtmlGenericControl divProfileShortSubList = (HtmlGenericControl)e.Item.FindControl("divProfileShortSubList");
            Label lblProfileShortList = (Label)e.Item.FindControl("lblProfileShortList");
            Label lblProfileShortListDate = (Label)e.Item.FindControl("lblProfileShortListDate");
            HtmlGenericControl iProfileShortList = (HtmlGenericControl)e.Item.FindControl("iProfileShortList");

            //InterViewOn
            HtmlGenericControl divInterViewOn = (HtmlGenericControl)e.Item.FindControl("divInterViewOn");
            Label labelInterViewOnDate = (Label)e.Item.FindControl("labelInterViewOnDate");
            HtmlGenericControl divSubInterViewOn = (HtmlGenericControl)e.Item.FindControl("divSubInterViewOn");
            Label lblInterViewOn = (Label)e.Item.FindControl("lblInterViewOn");

            //HtmlGenericControl divSubInterViewOn = (HtmlGenericControl)e.Item.FindControl("divSubInterViewOn");
            HtmlGenericControl iInterViewOn = (HtmlGenericControl)e.Item.FindControl("iInterViewOn");

            //HR Assessment
            HtmlGenericControl divHRAssessment = (HtmlGenericControl)e.Item.FindControl("divHRAssessment");
            HtmlGenericControl divsubHRAssessment = (HtmlGenericControl)e.Item.FindControl("divsubHRAssessment");

            Label lblHRAssessmentDate = (Label)e.Item.FindControl("lblHRAssessmentDate");
            Label lblHRAssessment = (Label)e.Item.FindControl("lblHRAssessment");
            HtmlGenericControl iHRAssessment = (HtmlGenericControl)e.Item.FindControl("iHRAssessment");

            //HOD Assessment
            HtmlGenericControl divHODAssessment = (HtmlGenericControl)e.Item.FindControl("divHODAssessment");
            HtmlGenericControl divSubHODAssessment = (HtmlGenericControl)e.Item.FindControl("divSubHODAssessment");
            Label lblHODAssessmentDate = (Label)e.Item.FindControl("lblHODAssessmentDate");
            Label lblHODAssessment = (Label)e.Item.FindControl("lblHODAssessment");
            HtmlGenericControl iHODAssessment = (HtmlGenericControl)e.Item.FindControl("iHODAssessment");


            //Offer Latter
            HtmlGenericControl divOfferLatter = (HtmlGenericControl)e.Item.FindControl("divOfferLatter");
            Label lblOfferLatterDate = (Label)e.Item.FindControl("lblOfferLatterDate");
            Label lblOfferLatter = (Label)e.Item.FindControl("lblOfferLatter");

            HtmlGenericControl divSubOfferLatter = (HtmlGenericControl)e.Item.FindControl("divSubOfferLatter");
            HtmlGenericControl iOfferLatter = (HtmlGenericControl)e.Item.FindControl("iOfferLatter");


            //Appoitment Latter
            HtmlGenericControl divAppoitmentLatter = (HtmlGenericControl)e.Item.FindControl("divAppoitmentLatter");
            Label lblAppoitmentLatterDate = (Label)e.Item.FindControl("lblAppoitmentLatterDate");
            Label lblAppoitmentLatter = (Label)e.Item.FindControl("lblAppoitmentLatter");
            HtmlGenericControl divSubAppoitmentLatter = (HtmlGenericControl)e.Item.FindControl("divSubAppoitmentLatter");
            HtmlGenericControl iAppoitmentLatter = (HtmlGenericControl)e.Item.FindControl("iAppoitmentLatter");


            //Final Joining

            HtmlGenericControl divFianlJoining = (HtmlGenericControl)e.Item.FindControl("divFianlJoining");
            Label lblFianlJoiningDate = (Label)e.Item.FindControl("lblFianlJoiningDate");
            Label lblFianlJoining = (Label)e.Item.FindControl("lblFianlJoining");
            HtmlGenericControl iFianlJoining = (HtmlGenericControl)e.Item.FindControl("iFianlJoining");

            divProfileCreated.Visible = true;
            divProfileShortSubList.Attributes["style"] = "background-color:#119d97;color:white";
            divSubOfferCard.Attributes["style"] = "background-color:#119d97;color:white";
            lblProfileCreatedDate.Text = dataRowView["ProfileCreatedOn"].ToString();
            iProfileCreated.Attributes["style"] = "background-color:#119d97;color:white";


            if (dataRowView["IsProfileShortList"].ToString() == "0")
            {
                divProfileShortList.Visible = true;
                divProfileShortSubList.Attributes["style"] = "background-color:red;color:white";
                lblProfileShortList.Attributes["style"] = "background-color:red;color:white";
                iProfileShortList.Attributes["style"] = "background-color:red;color:white";
                lblProfileShortListDate.Attributes["style"] = "color:red";
                lblProfileShortListDate.Text = dataRowView["ProfileShortListOn"].ToString();
                
            }
            else
            {
                divProfileShortList.Visible = true;
                divProfileShortSubList.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
                lblProfileShortList.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
                iProfileShortList.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
                lblProfileShortListDate.Text = dataRowView["ProfileShortListOn"].ToString();
            }



            if (dataRowView["IsInterViewScheduled"].ToString() == "0")
            {
                divInterViewOn.Visible = true;
                divSubInterViewOn.Attributes["style"] = "background-color:red;color:white";
                lblInterViewOn.Attributes["style"] = "background-color:red;color:white";
                iInterViewOn.Attributes["style"] = "background-color:red;color:white";
                labelInterViewOnDate.Attributes["style"] = "color:red";
                labelInterViewOnDate.Text = dataRowView["InterViewOn"].ToString();
                lblInterViewOn.Text = dataRowView["InterviewTime"].ToString();

            }
            else
            {
                divInterViewOn.Visible = true;
                divSubInterViewOn.Attributes["style"] = "background-color:#119d97;color:white";
                lblInterViewOn.Attributes["style"] = "background-color:#119d97;color:white";
                iInterViewOn.Attributes["style"] = "background-color:#119d97;color:white";
                labelInterViewOnDate.Attributes["style"] = "color:#119d97";
                labelInterViewOnDate.Text = dataRowView["InterViewOn"].ToString();
                lblInterViewOn.Text = dataRowView["InterviewTime"].ToString();
            }


            if (dataRowView["IsHRAssessment"].ToString() == "0")
            {
                divHRAssessment.Visible = true;
                divsubHRAssessment.Attributes["style"] = "background-color:red;color:white";
                lblHRAssessment.Attributes["style"] = "background-color:red;color:white";
                iHRAssessment.Attributes["style"] = "background-color:red;color:white";
                lblHRAssessmentDate.Attributes["style"] = "color:red";
                lblHRAssessmentDate.Text = dataRowView["HRAssessmentOn"].ToString();
                //lblInterViewOn.Text = dataRowView["InterviewTime"].ToString();

            }
            else
            {
                divHRAssessment.Visible = true;
                divsubHRAssessment.Attributes["style"] = "background-color:#119d97;color:white";
                lblHRAssessment.Attributes["style"] = "background-color:#119d97;color:white";
                iHRAssessment.Attributes["style"] = "background-color:#119d97;color:white";
                lblHRAssessmentDate.Attributes["style"] = "color:#119d97";
                lblHRAssessmentDate.Text = dataRowView["InterViewOn"].ToString();
                //lblHRAssessmentDate.Text = dataRowView["HRAssessmentOn"].ToString();
            }


            if (dataRowView["IsHODAssessment"].ToString() == "0")
            {
                divHODAssessment.Visible = true;
                divSubHODAssessment.Attributes["style"] = "background-color:red;color:white";
                lblHODAssessment.Attributes["style"] = "background-color:red;color:white";
                iHODAssessment.Attributes["style"] = "background-color:red;color:white";
                lblHODAssessment.Attributes["style"] = "color:red";
                lblHODAssessmentDate.Text = dataRowView["HODAssessmentOn"].ToString();
            }
            else
            {
                divHODAssessment.Visible = true;
                divSubHODAssessment.Attributes["style"] = "background-color:#119d97;color:white";
                lblHODAssessment.Attributes["style"] = "background-color:#119d97;color:white";
                iHODAssessment.Attributes["style"] = "background-color:#119d97;color:white";
                lblHODAssessment.Attributes["style"] = "color:#119d97";
                lblHODAssessmentDate.Text = dataRowView["InterViewOn"].ToString();
            }



            if (dataRowView["IsOfferLattergenerate"].ToString() == "0")
            {
                divOfferLatter.Visible = true;
                divSubOfferLatter.Attributes["style"] = "background-color:red;color:white";
                lblHODAssessment.Attributes["style"] = "background-color:red;color:white";
                iOfferLatter.Attributes["style"] = "background-color:red;color:white";
                lblOfferLatter.Attributes["style"] = "color:red";
                lblOfferLatterDate.Text = dataRowView["HODAssessmentOn"].ToString();
            }
            else
            {
                divOfferLatter.Visible = true;
                divSubOfferLatter.Attributes["style"] = "background-color:#119d97;color:white";
                lblHODAssessment.Attributes["style"] = "background-color:#119d97;color:white";
                iOfferLatter.Attributes["style"] = "background-color:#119d97;color:white";
                lblOfferLatter.Attributes["style"] = "color:#119d97";
                lblOfferLatterDate.Text = dataRowView["InterViewOn"].ToString();
            }



            if (dataRowView["IsAppoitmentLatter"].ToString() == "0")
            {
                divAppoitmentLatter.Visible = true;
                divSubAppoitmentLatter.Attributes["style"] = "background-color:red;color:white";
                lblAppoitmentLatter.Attributes["style"] = "background-color:red;color:white";
                iAppoitmentLatter.Attributes["style"] = "background-color:red;color:white";
                lblAppoitmentLatter.Attributes["style"] = "color:red";
                lblAppoitmentLatterDate.Text = dataRowView["HODAssessmentOn"].ToString();
            }
            else
            {
                divAppoitmentLatter.Visible = true;
                divSubAppoitmentLatter.Attributes["style"] = "background-color:#119d97;color:white";
                lblAppoitmentLatter.Attributes["style"] = "background-color:#119d97;color:white";
                iAppoitmentLatter.Attributes["style"] = "background-color:#119d97;color:white";
                lblAppoitmentLatter.Attributes["style"] = "color:#119d97";
                lblAppoitmentLatterDate.Text = dataRowView["InterViewOn"].ToString();
            }


            Label lblJobHeader = (Label)e.Item.FindControl("lblJobHeader");
            lblJobHeader.Text = dataRowView["Job"].ToString();
            //    {




            //lblProfileCreatedDate.Attributes["style"] = "background-color:#d7eedc;";


            //if (dataRowView["AssementStatus"].ToString().ToUpper() == "PENDING")
            //{
            //    divProfileShortList.Visible = false;
            //}
            //else
            //{
            //    divProfileShortList.Visible = true;
            //    if (dataRowView["AssementStatus"].ToString().ToUpper() == "PASS")
            //    {
            //        lblProfileShortList.Attributes["class"] = "background-color:#119d97;color:white";
            //        divProfileShortSubList.Attributes["style"] = "background-color:#119d97;color:white";
            //        iProfileShortList.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
            //        //lblProfileShortListDate.Attributes["class"] = "success";
            //        lblProfileShortListDate.Text = dataRowView["RegistrationDate"].ToString();
            //    }
            //    else
            //    {
            //        lblProfileShortList.Attributes["class"] = "failure";
            //        divProfileShortSubList.Attributes["style"] = "background-color:#FF7F50;color:white;border-color:#FF7F50;";
            //        iProfileShortList.Attributes["style"] = "background-color:#FF7F50;color:white;border-color:#FF7F50;";
            //        //  lblProfileShortListDate.Attributes["style"] = "background-color:#f1aeb5;color:white";
            //        //lblProfileShortListDate.Attributes["class"] = "failure";
            //        lblProfileShortListDate.Text = dataRowView["RegistrationDate"].ToString();
            //    }
            //}



            //if (dataRowView["AssementStatus"].ToString() == "PENDING")
            //{
            //    divInterViewOn.Visible = false;
            //}
            //else
            //{
            //    divInterViewOn.Visible = true;
            //    if (dataRowView["AssementStatus"].ToString() == "PASS")
            //    {
            //        labelInterViewOnDate.Attributes["class"] = "success";
            //        divSubInterViewOn.Attributes["style"] = "background-color:#119d97;color:white";
            //        iInterViewOn.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
            //        lblInterViewOn.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
            //        lblInterViewOn.Text = dataRowView["AppointmentDate"].ToString();
            //        labelInterViewOnDate.Text = dataRowView["RegistrationDate"].ToString();
            //    }
            //    else
            //    {
            //        lblProfileShortList.Attributes["class"] = "failure";
            //        divSubInterViewOn.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        divProfileShortSubList.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        iProfileShortList.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        lblInterViewOn.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        lblInterViewOn.Text = dataRowView["RegistrationDate"].ToString();
            //    }
            //}


            //if (dataRowView["AssessmentStatus"].ToString() == "PENDING")
            //{
            //    divHRAssessment.Visible = false;
            //}
            //else
            //{
            //    divHRAssessment.Visible = true;
            //    if (dataRowView["AssementStatus"].ToString() == "PASS")
            //    {
            //        labelInterViewOnDate.Attributes["class"] = "success";
            //        divsubHRAssessment.Attributes["style"] = "background-color:#119d97;color:white";
            //        iHRAssessment.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
            //        //lblHRAssessmentDate.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
            //        lblHRAssessment.Text = dataRowView["AssessmentStatus"].ToString();
            //        lblHRAssessment.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
            //        lblHRAssessmentDate.Text = dataRowView["RegistrationDate"].ToString();
            //    }
            //    else
            //    {
            //        lblProfileShortList.Attributes["class"] = "failure";
            //        divsubHRAssessment.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        divProfileShortSubList.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        iProfileShortList.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        lblInterViewOn.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        lblInterViewOn.Text = dataRowView["RegistrationDate"].ToString();
            //    }
            //}

            //if (dataRowView["AssessmentStatus"].ToString() == "PENDING")
            //{
            //    divHODAssessment.Visible = false;
            //    //divSubHODAssessment.Visible = false;
            //}
            //else
            //{
            //    divHODAssessment.Visible = true;
            //    if (dataRowView["AssementStatus"].ToString() == "PASS")
            //    {
            //        labelInterViewOnDate.Attributes["class"] = "success";
            //        divSubHODAssessment.Attributes["style"] = "background-color:#119d97;color:white";
            //        iHODAssessment.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
            //        //lblHRAssessmentDate.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
            //        lblHODAssessment.Text = dataRowView["AssessmentStatus"].ToString();
            //        lblHODAssessment.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
            //        lblHODAssessmentDate.Text = dataRowView["RegistrationDate"].ToString();
            //    }
            //    else
            //    {
            //        lblProfileShortList.Attributes["class"] = "failure";
            //        divSubHODAssessment.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        divProfileShortSubList.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        iHODAssessment.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        lblHODAssessment.Text = dataRowView["AssessmentStatus"].ToString();
            //        lblHODAssessment.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        lblHODAssessmentDate.Text = dataRowView["RegistrationDate"].ToString();
            //    }
            //}

            //if (dataRowView["IsOfferLatterReleased"].ToString() == "0")
            //{
            //    divOfferLatter.Visible = false;
            //    //divSubHODAssessment.Visible = false;
            //}
            //else
            //{
            //    divOfferLatter.Visible = true;
            //    if (dataRowView["IsOfferLatterReleased"].ToString() == "1")
            //    {
            //        lblOfferLatter.Attributes["class"] = "success";
            //        divSubOfferLatter.Attributes["style"] = "background-color:#119d97;color:white";
            //        iOfferLatter.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
            //        lblOfferLatter.Text = dataRowView["AssessmentStatus"].ToString();
            //        lblOfferLatter.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
            //        lblOfferLatterDate.Text = dataRowView["OfferGenerateDateTime"].ToString();
            //    }
            //    else
            //    {
            //        lblOfferLatter.Attributes["class"] = "failure";
            //        divSubOfferLatter.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        iOfferLatter.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        lblOfferLatter.Text = dataRowView["AssessmentStatus"].ToString();
            //        lblOfferLatter.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        lblOfferLatterDate.Text = dataRowView["OfferGenerateDateTime"].ToString();
            //    }
            //}


            //if (dataRowView["AppointmentLatter"].ToString() == "0")
            //{
            //    divAppoitmentLatter.Visible = false;
            //}
            //else
            //{
            //    divAppoitmentLatter.Visible = true;
            //    if (dataRowView["AppointmentLatter"].ToString() == "1")
            //    {

            //        divSubAppoitmentLatter.Attributes["style"] = "background-color:#119d97;color:white";
            //        iAppoitmentLatter.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
            //        lblAppoitmentLatter.Text = dataRowView["AssessmentStatus"].ToString();
            //        lblAppoitmentLatter.Attributes["style"] = "background-color:#119d97;color:white;border-color:#119d97;";
            //        lblAppoitmentLatterDate.Text = dataRowView["AppointmentLatterDate"].ToString();
            //    }
            //    else
            //    {
            //        divSubAppoitmentLatter.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        iAppoitmentLatter.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        lblAppoitmentLatter.Text = dataRowView["AssessmentStatus"].ToString();
            //        lblAppoitmentLatter.Attributes["style"] = "background-color:#f1aeb5;color:white;border-color:#f1aeb5;";
            //        lblAppoitmentLatterDate.Text = dataRowView["AppointmentLatterDate"].ToString();
            //    }
            //}


            //Label lblJobHeader = (Label)e.Item.FindControl("lblJobHeader");
            //lblJobHeader.Text = dataRowView["JobProfile"].ToString();




        }

        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    DataRowView dataRowView = (DataRowView)e.Item.DataItem;

        //    HtmlGenericControl myDiv = (HtmlGenericControl)e.Item.FindControl("divProfileCard");
        //    HtmlGenericControl divOfferProgressbar = (HtmlGenericControl)e.Item.FindControl("divOfferProgressbar");
        //    HtmlGenericControl divProfileProgressbar = (HtmlGenericControl)e.Item.FindControl("divProfileProgressbar");
        //    HtmlGenericControl divInterviewCard = (HtmlGenericControl)e.Item.FindControl("divInterviewCard");
        //    HtmlGenericControl divDateTimeProgressbar = (HtmlGenericControl)e.Item.FindControl("divDateTimeProgressbar");
        //    HtmlGenericControl divDateTimecard = (HtmlGenericControl)e.Item.FindControl("divDateTimecard");
        //    HtmlGenericControl rptheader = (HtmlGenericControl)e.Item.FindControl("rptheader");
        //    HtmlGenericControl iProfile = (HtmlGenericControl)e.Item.FindControl("iProfile");
        //    HtmlGenericControl iDateTime = (HtmlGenericControl)e.Item.FindControl("iDateTime");
        //    HtmlGenericControl iInterview = (HtmlGenericControl)e.Item.FindControl("iInterview");
        //    HtmlGenericControl divInterviewProgressbar = (HtmlGenericControl)e.Item.FindControl("divInterviewProgressbar");
        //    HtmlGenericControl divOfferCard = (HtmlGenericControl)e.Item.FindControl("divOfferCard");
        //    HtmlGenericControl iOffer = (HtmlGenericControl)e.Item.FindControl("iOffer");
        //    HtmlGenericControl divOffer = (HtmlGenericControl)e.Item.FindControl("divOffer");

        //    Label lblOfferDt = (Label)e.Item.FindControl("lblOfferDt");
        //    //lblOfferDt


        //    if (dataRowView["JobProfile"].ToString() != "")
        //    {
        //        rptheader.InnerText = dataRowView["JobProfile"].ToString();
        //        rptheader.Attributes["style"] = "display:flex;justify-content: center;font-size: 40px;";
        //    }

        //    Label lblInterviewDt = (Label)e.Item.FindControl("lblInterviewDt");
        //    Label lblInterviewconductDt = (Label)e.Item.FindControl("lblInterviewconductDt");

        //    Label lblAppoitmentDate = (Label)e.Item.FindControl("lblAppoitmentDate");
        //    Label lblAppoitmentTime = (Label)e.Item.FindControl("lblAppoitmentTime");

        //    lblAppoitmentDate.Text = "Interview On";
        //    lblAppoitmentTime.Text = dataRowView["AppointmentDate"].ToString();


        //    if (dataRowView["InsertedOn"].ToString() != "")
        //    {
        //        myDiv.Attributes["style"] = "background-color:#d7eedc;";
        //        iProfile.Attributes["style"] = "background-color:#d7eedc;";
        //    }


        //    if (dataRowView["Status"].ToString() != "")
        //    {

        //        if (dataRowView["Status"].ToString() == "A")
        //        {
        //            divInterviewCard.Attributes["style"] = "background-color:#d7eedc";
        //            iInterview.Attributes["style"] = "background-color:#d7eedc";
        //        }
        //        else if (dataRowView["Status"].ToString() == "R")
        //        {
        //            divInterviewCard.Attributes["style"] = "background-color:#f1aeb5";
        //            iInterview.Attributes["style"] = "background-color:#f1aeb5";
        //        }

        //        if (dataRowView["ApprovedDisapprovedOn"].ToString() != "")
        //        {
        //            lblInterviewDt.Text = Convert.ToDateTime(dataRowView["ApprovedDisapprovedOn"].ToString()).ToString("dd-MMM-yyyy");
        //            lblInterviewDt.Attributes["style"] = "font-weight: bold;font-size: 20px;";
        //            lblInterviewDt.CssClass = "top-date";
        //        }

        //        divDateTimecard.Attributes["style"] = "background-color:#d7eedc";
        //        lblInterviewconductDt.Text = dataRowView["hrInsertedOn"].ToString();
        //        iDateTime.Attributes["style"] = "background-color:#d7eedc;";
        //        lblInterviewconductDt.Attributes["style"] = "font-weight: bold;font-size: 20px;";

        //    }

        //    if (dataRowView["AssessmentStatus"].ToString() == "PASS")
        //    {
        //        divInterviewProgressbar.Attributes["style"] = "background-color:#d7eedc;";
        //        divInterviewCard.Attributes["style"] = "background-color:#d7eedc;";
        //        iInterview.Attributes["style"] = "background-color:#d7eedc;";
        //    }

        //    if (dataRowView["AssessmentStatus"].ToString() == "FAILED")
        //    {
        //        divInterviewProgressbar.Attributes["style"] = "background-color:#f1aeb5;";
        //        divInterviewCard.Attributes["style"] = "background-color:#f1aeb5;";
        //        iInterview.Attributes["style"] = "background-color:#f1aeb5;";
        //    }

        //    if (dataRowView["IsOfferGenerate"].ToString() == "1")
        //    {
        //        divOfferCard.Attributes["style"] = "background-color:#d7eedc;";
        //        divOfferCard.Attributes["style"] = "background-color:#d7eedc;";
        //        iOffer.Attributes["style"] = "background-color:#d7eedc;";
        //        lblOfferDt.Text = dataRowView["OfferGenerateDateTime"].ToString();
        //        divOffer.Visible = true;
        //        lblOfferDt.Attributes["style"] = "font-weight: bold;font-size: 20px;";
        //    }
        //    else if (dataRowView["IsOfferGenerate"].ToString() == "0")
        //    {
        //        divOfferCard.Attributes["style"] = "background-color:#f1aeb5;";
        //        divOfferCard.Attributes["style"] = "background-color:#f1aeb5;";
        //        iOffer.Attributes["style"] = "background-color:#f1aeb5;";
        //        lblOfferDt.Text = dataRowView["OfferGenerateDateTime"].ToString();
        //        divOffer.Visible = true;
        //        lblOfferDt.Attributes["style"] = "font-weight: bold;font-size: 20px;";
        //    }

        //    Label myLabel = (Label)e.Item.FindControl("lblProfileDt");

        //    myLabel.Text = dataRowView["InsertedOn"].ToString();
        //    myLabel.Attributes["style"] = "font-weight: bold;font-size: 20px;";
        //}

    }

    public class MyDataClass
    {
        public string LabelText { get; set; }

    }

    public DataTable Interview_Status(string MobileNo)
    {
        SqlCommand sqlCmd = new SqlCommand();
        GeneralDAL objDal = new GeneralDAL();
        objDal.OpenSQLConnection();
        sqlCmd.Connection = objDal.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.CommandText = "FitToJob_Android_Application";
        sqlCmd.Parameters.AddWithValue("@Action", "GetInterViewStatusById");
        sqlCmd.Parameters.AddWithValue("@CandidateId", MobileNo);
        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet);
        objDal.CloseSQLConnection();
        return dataSet.Tables[0];
    }

    public string GetCandidateIdByMobileNo(string MobileNo)
    {
        SqlCommand sqlCmd = new SqlCommand();
        GeneralDAL objDal = new GeneralDAL();
        objDal.OpenSQLConnection();
        sqlCmd.Connection = objDal.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.CommandText = "FitToJob_Android_Application";
        sqlCmd.Parameters.AddWithValue("@Action", "GetCandidateIdByMobileNo");
        sqlCmd.Parameters.AddWithValue("@MobileNo", MobileNo);
        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet);
        objDal.CloseSQLConnection();
        return dataSet.Tables[0].Rows[0]["CandidateId"].ToString();
    }
        
}