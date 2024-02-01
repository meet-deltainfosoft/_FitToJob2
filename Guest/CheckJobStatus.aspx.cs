using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;
using System.Xml.Linq;
using System.Net.Mail;
using System.Drawing;
using System.IO;
using System.Web.Configuration;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Data.SqlClient;

public partial class Guest_CheckJobStatus : System.Web.UI.Page
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
                BindGridView(Session["MobileNo"].ToString());
            }

        }
    }

    private void ShowGridView()
    {
        gdvJobOfferLatter.Visible = true;

    }

    private DataTable GetDataFromDataSource()
    {
        List<CheckJobStatus> checkJobStatusList = new List<CheckJobStatus>
        {
            new CheckJobStatus { CandidateId = "", RegistrationId = "", Name = "", JobProfile = "",  Salary = 0.0m ,JoiningDate = "",WorkPlace = ""},
        
            // Add more entries as needed
        };


        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("CandidateId");
        dataTable.Columns.Add("RegistrationId");
        dataTable.Columns.Add("FirstName");
        dataTable.Columns.Add("JobProfile");
        dataTable.Columns.Add("Salary");
        dataTable.Columns.Add("JoiningDate");
        dataTable.Columns.Add("WorkPlace");
        //dataTable.Columns.Add("Remarks");

        foreach (var form in checkJobStatusList)
        {
            DataRow row = dataTable.NewRow();
            //row["No"] = form.No;
            row["CandidateId"] = form.CandidateId;
            row["RegistrationId"] = form.RegistrationId;
            row["FirstName"] = form.Name;
            row["JobProfile"] = form.JobProfile;
            row["Salary"] = form.Salary;
            row["JoiningDate"] = form.JoiningDate;
            row["WorkPlace"] = form.WorkPlace;
            // row["Remarks"] = form.Remarks;


            dataTable.Rows.Add(row);
        }
        return dataTable;
    }

    public class CheckJobStatus
    {
        public string CandidateId { get; set; }
        public string RegistrationId { get; set; }
        public string Name { get; set; }
        public string JobProfile { get; set; }
        public decimal Salary { get; set; }
        public string JoiningDate { get; set; }
        public string WorkPlace { get; set; }
        // public string Remarks { get; set; }
        public bool IsRejected { get; set; }
    }

    private List<CheckJobStatus> GetDigitalInterviewFormGrid()
    {
        List<CheckJobStatus> CheckJobStatus = new List<CheckJobStatus>();

        foreach (GridViewRow row in gdvJobOfferLatter.Rows)
        {
            CheckJobStatus detail = new CheckJobStatus();

            TextBox txtName = row.FindControl("txtName") as TextBox;
            TextBox txtJobProfile = row.FindControl("txtJobProfile") as TextBox;
            TextBox txtSalary = row.FindControl("txtSalary") as TextBox;
            TextBox txtJoiningDate = row.FindControl("txtJoiningDate") as TextBox;
            TextBox txtWorkPlace = row.FindControl("txtWorkPlace") as TextBox;
            TextBox txtRemarks = row.FindControl("txtRemarks") as TextBox;


            detail.Name = (txtName != null) ? txtName.Text : string.Empty;
            detail.JobProfile = (txtJobProfile != null) ? txtJobProfile.Text : string.Empty;
            //detail.Salary = (txtSalary != null)? txtSalary.Text : string.Empty;
            detail.JoiningDate = (txtJoiningDate != null) ? txtJoiningDate.Text : string.Empty;
            detail.WorkPlace = (txtWorkPlace != null) ? txtWorkPlace.Text : string.Empty;
            //detail.Remarks = (txtRemarks != null) ? txtRemarks.Text : string.Empty;

            CheckJobStatus.Add(detail);
        }

        return CheckJobStatus;
    }

    protected void gdvJobOfferLatter_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtAcceptance = (TextBox)e.Row.FindControl("txtAcceptance");
            TextBox txtremarks = (TextBox)e.Row.FindControl("txtremarks");

            Button btnAccept = (Button)e.Row.FindControl("btnAccept");
            Button btnReject = (Button)e.Row.FindControl("btnReject");

            if (txtAcceptance != null && btnAccept != null && btnReject != null)
            {
                string acceptanceStatus = txtAcceptance.Text;
                if (acceptanceStatus.Equals("Acceptance") || acceptanceStatus.Equals("Rejected"))
                {
                    btnAccept.Enabled = false;
                    btnReject.Enabled = false;
                    txtremarks.ReadOnly = true;
                    txtAcceptance.ReadOnly = true;
                }
                else
                {
                    btnAccept.Enabled = true;
                    btnReject.Enabled = true;
                    txtremarks.ReadOnly = false;
                    txtAcceptance.ReadOnly = false;
                }
            }
        }
    }

    private void FitToJob_Android_Application(string CandidateJobAcceptance)
    {
        SqlCommand sqlCmd = new SqlCommand();
        GeneralDAL objDal = new GeneralDAL();
        objDal.OpenSQLConnection();
        sqlCmd.Connection = objDal.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.CommandText = "FitToJob_Android_Application";
        sqlCmd.Parameters.AddWithValue("@Action", CandidateJobAcceptance);
        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet);

        if (dataSet.Tables[0].Rows.Count > 0)
        {
            gdvJobOfferLatter.DataSource = dataSet.Tables[0];
            gdvJobOfferLatter.DataBind();
        }
        objDal.CloseSQLConnection();
    }

    private void BindGridView(string MobileNo)
    {
        GeneralDAL objDal = new GeneralDAL();
        try
        {
            SqlCommand sqlCmd = new SqlCommand();

            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Android_Application";
            sqlCmd.Parameters.AddWithValue("@Action", "CandidateJobAcceptance");
            sqlCmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                gdvJobOfferLatter.DataSource = dataSet.Tables[0];
                gdvJobOfferLatter.DataBind();
            }
            else
            {
                List<CheckJobStatus> CheckJobStatus = new List<CheckJobStatus>();
                CheckJobStatus.Add(new CheckJobStatus { CandidateId = "", RegistrationId = "", Name = "", JobProfile = "", Salary = 0.0m, JoiningDate = "", WorkPlace = "" });
                gdvJobOfferLatter.DataSource = CheckJobStatus;
                gdvJobOfferLatter.DataBind();
                //ShowGridView();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            objDal.CloseSQLConnection();
        }
    }

    protected void btnOffer_Click(object sender, EventArgs e)
    {
        string sqlWhere1 = "";
        SqlCommand sqlCmd = new SqlCommand();
        GeneralDAL objDal = new GeneralDAL();
        objDal.OpenSQLConnection();
        sqlCmd.Connection = objDal.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT Top 1 JobId  FROM View_Offer_Latter_Report" + sqlWhere1;

        string JobId = sqlCmd.ExecuteScalar().ToString();
        Response.Redirect("../Report/Exam/CRViewer.aspx?RptType=OfferLatter&RptId=" + JobId.ToUpper());
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        TextBox txtremarks = gdvJobOfferLatter.Rows[gdvJobOfferLatter.EditIndex].FindControl("txtremarks") as TextBox;

        if (txtremarks != null)
        {
            // Enable the Remarks textbox
            txtremarks.ReadOnly = false;
        }
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {

        string jobId = gdvJobOfferLatter.DataKeys[0].Values["JobId"].ToString();
        //string candidateId = GetCandidateId(); 
        TextBox txtAcceptanceStatus = gdvJobOfferLatter.Rows[0].FindControl("txtAcceptanceStatus") as TextBox;
        string acceptanceStatus = (txtAcceptanceStatus != null) ? txtAcceptanceStatus.Text : string.Empty;

        SaveAcceptanceStatus(jobId, acceptanceStatus);
        //UpdateAcceptRejectButtonText(acceptanceStatus);
        BindGridView("6353364866");

    }
    private void SaveAcceptanceStatus(string jobId, string acceptanceStatus)
    {
        GeneralDAL objDal = new GeneralDAL();
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            //GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "SaveJobAcceptance";
            sqlCmd.Parameters.AddWithValue("@JobId", jobId);
            //sqlCmd.Parameters.AddWithValue("@CandidateId", candidateId);
            sqlCmd.Parameters.AddWithValue("@AcceptanceStatus", acceptanceStatus);


            if (acceptanceStatus.ToLower() == "rejected")
            {
                TextBox txtRemarks = gdvJobOfferLatter.Rows[0].FindControl("txtRemarks") as TextBox;
                if (txtRemarks != null)
                {
                    sqlCmd.Parameters.AddWithValue("@Remarks", txtRemarks.Text);
                }
            }

            sqlCmd.ExecuteNonQuery();
            Response.Write("Job acceptance status saved successfully!");
        }
        catch (Exception ex)
        {
            Response.Write("Error saving job acceptance status: " + ex.Message);
        }
        finally
        {
            objDal.CloseSQLConnection();
        }
    }
    protected void gdvJobOfferLatter_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AcceptRow")
        {
            string rowIndex = e.CommandArgument.ToString();
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();

            // Set the connection for the SqlCommand
            sqlCmd.Connection = objDal.ActiveSQLConnection();

            sqlCmd.CommandText = "FitToJob_Android_Application";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@Action", "CandidateApproveJob");
            sqlCmd.Parameters.AddWithValue("@CandidateApprovalStatus", 1);
            sqlCmd.Parameters.AddWithValue("@CandidateOfferRejectionRemark", "");
            sqlCmd.Parameters.AddWithValue("@JobId", rowIndex);
            sqlCmd.Parameters.AddWithValue("@MobileNo", Session["MobileNo"].ToString());

            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                Response.Write("<script>alert('Congratulatios !!! You Have Accepted The Job')</script>");
                gdvJobOfferLatter.DataSource = dataSet.Tables[1];
                gdvJobOfferLatter.DataBind();
            }

            objDal.CloseSQLConnection();
        }
        else if (e.CommandName == "RejectRow")
        {


            GridViewRow row = (GridViewRow)(((Control)e.CommandSource).NamingContainer);
            //string name = ((TextBox)selectedRow.FindControl("txttartim")).Text;

            TextBox txtremarks = row.FindControl("txtremarks") as TextBox;

            //if (txtRejectRemarks.Text == "")
            //{
            //    Response.Write("<Script>alert('Please Enter Rejection Remarks')</script>");
            //    return;
            //}
            // TextBox txtremarks = gdvJobOfferLatter.FindControl("txtremarks") as TextBox;

            string RejectionRemark = txtremarks.Text;
            string rowIndex = e.CommandArgument.ToString();
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();

            sqlCmd.CommandText = "FitToJob_Android_Application";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@Action", "CandidateApproveJob");
            sqlCmd.Parameters.AddWithValue("@CandidateApprovalStatus", 0);
            sqlCmd.Parameters.AddWithValue("@CandidateOfferRejectionRemark", RejectionRemark);
            sqlCmd.Parameters.AddWithValue("@JobId", rowIndex);
            sqlCmd.Parameters.AddWithValue("@MobileNo", Session["MobileNo"].ToString());

            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                Response.Write("<script>alert('Thanks For The Confirmation, We are Rejection Offer!!')</script>");
                gdvJobOfferLatter.DataSource = dataSet.Tables[1];
                gdvJobOfferLatter.DataBind();
            }   

            objDal.CloseSQLConnection();
        }
        else if (e.CommandName == "ViewOfferLatter")
        {
            Session["JobId"] = e.CommandArgument.ToString();
            Response.Redirect("../Guest/OfferApprove.aspx");
        }
    }
}