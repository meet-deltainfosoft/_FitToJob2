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

public partial class Guest_DigitalInterviewForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = GetDataFromDataSource();
            //ddlApproved_SelectedIndexChanged(null, null);
            gdvDigitalInterview.DataSource = dt;
            gdvDigitalInterview.DataBind();

            string defaultStatus = "P";

            FitToJob_Master_InterviewDetail(defaultStatus);
        }
    }

    private void ShowGridView()
    {
        gvDigitalInterviewForms.Visible = true;

    }
    protected void gvDigitalInterviewForms_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

        }
    }
    private DataTable GetDataFromDataSource()
    {
        List<DigitalInterviewForm> digitalInterviewForms = new List<DigitalInterviewForm>
        {
            new DigitalInterviewForm {  CandidateId ="",RegistrationId ="",Name = "", Surname = "", Taluka = "", JobProfile = "", Exam = "", Distance = "", LastCompanyName = "", LastSalary = "", SalaryExpect = "", Experiance = "" , PhotoUrl = "" ,SelfIntrourl = "" ,PrintUrl = "",Status ="P",Remarks ="" }
        };

        DataTable dataTable = new DataTable();

        dataTable.Columns.Add("CandidateId");
        dataTable.Columns.Add("RegistrationId");
        dataTable.Columns.Add("FirstName");
        dataTable.Columns.Add("LastName");
        dataTable.Columns.Add("Taluka");
        dataTable.Columns.Add("JobProfile");
        dataTable.Columns.Add("Exam");
        dataTable.Columns.Add("Distance");
        dataTable.Columns.Add("CompanyName");
        dataTable.Columns.Add("LastSalaryDetail");
        dataTable.Columns.Add("ExpectSalary");
        dataTable.Columns.Add("Experiance");
        dataTable.Columns.Add("PhotoPath");
        dataTable.Columns.Add("VideoPath");
        dataTable.Columns.Add("ViewPrint");
        dataTable.Columns.Add("Status");

        foreach (var form in digitalInterviewForms)
        {
            DataRow row = dataTable.NewRow();
            //row["No"] = form.No;
            row["CandidateId"] = form.CandidateId;
            row["RegistrationId"] = form.RegistrationId;
            row["FirstName"] = form.Name;
            row["LastName"] = form.Surname;
            row["Taluka"] = form.Taluka;
            row["JobProfile"] = form.JobProfile;
            row["Exam"] = form.Exam;
            row["Distance"] = form.Distance;
            row["CompanyName"] = form.LastCompanyName;
            row["LastSalaryDetail"] = form.LastSalary;
            row["ExpectSalary"] = form.SalaryExpect;
            row["Experiance"] = form.Experiance;
            row["PhotoPath"] = form.Experiance;
            row["VideoPath"] = form.Experiance;
            row["ViewPrint"] = form.Experiance;
            row["Status"] = form.Status;

            dataTable.Rows.Add(row);
        }
        return dataTable;
    }
    public class DigitalInterviewForm
    {
        public string RegistrationId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Taluka { get; set; }
        public string JobProfile { get; set; }
        public string Exam { get; set; }
        public string Distance { get; set; }
        public string LastCompanyName { get; set; }
        public string LastSalary { get; set; }
        public string SalaryExpect { get; set; }
        public string Experiance { get; set; }
        public string PhotoUrl { get; set; }
        public string SelfIntrourl { get; set; }
        public string PrintUrl { get; set; }
        public string Status { get; set; }
        public object Remarks { get; set; }
        public string CandidateId { get; set; }
    }

    private List<DigitalInterviewForm> GetDigitalInterviewFormGrid()
    {
        List<DigitalInterviewForm> familyDetails = new List<DigitalInterviewForm>();

        foreach (GridViewRow row in gvDigitalInterviewForms.Rows)
        {
            DigitalInterviewForm detail = new DigitalInterviewForm();

            TextBox txtName = row.FindControl("txtName") as TextBox;
            TextBox txtSurname = row.FindControl("txtSurname") as TextBox;
            TextBox txtTaluka = row.FindControl("txtTaluka") as TextBox;
            TextBox txtJobProfile = row.FindControl("txtJobProfile") as TextBox;
            TextBox txtExam = row.FindControl("txtExam") as TextBox;
            TextBox txtDistance = row.FindControl("txtDistance") as TextBox;
            TextBox txtLastCompanyName = row.FindControl("txtLastCompanyName") as TextBox;
            TextBox txtLastSalary = row.FindControl("txtLastSalary") as TextBox;
            TextBox txtSalaryExpect = row.FindControl("txtSalaryExpect") as TextBox;
            TextBox txtExperiance = row.FindControl("txtExperiance") as TextBox;
            HyperLink Photourl = row.FindControl("Photourl") as HyperLink;
            HyperLink SelfIntrourl = row.FindControl("SelfIntrourl") as HyperLink;
            HyperLink Printurl = row.FindControl("Printurl") as HyperLink;
            //DropDownList ddlStatus = row.FindControl("ddlStatus") as DropDownList;


            detail.Name = (txtName != null) ? txtName.Text : string.Empty;
            detail.Surname = (txtSurname != null) ? txtSurname.Text : string.Empty;
            detail.Taluka = (txtTaluka != null) ? txtTaluka.Text : string.Empty;
            detail.JobProfile = (txtJobProfile != null) ? txtJobProfile.Text : string.Empty;
            detail.Exam = (txtExam != null) ? txtExam.Text : string.Empty;
            detail.Distance = (txtDistance != null) ? txtDistance.Text : string.Empty;
            detail.LastCompanyName = (txtLastCompanyName != null) ? txtLastCompanyName.Text : string.Empty;
            detail.LastSalary = (txtLastSalary != null) ? txtLastSalary.Text : string.Empty;
            detail.SalaryExpect = (txtSalaryExpect != null) ? txtSalaryExpect.Text : string.Empty;
            detail.Experiance = (txtExperiance != null) ? txtExperiance.Text : string.Empty;
            detail.PhotoUrl = (Photourl != null) ? Photourl.NavigateUrl : string.Empty;
            detail.SelfIntrourl = (SelfIntrourl != null) ? SelfIntrourl.NavigateUrl : string.Empty;
            detail.PrintUrl = (Printurl != null) ? Printurl.NavigateUrl : string.Empty;
            //detail.Status = (ddlStatus != null) ? ddlStatus.SelectedValue : string.Empty;
            familyDetails.Add(detail);
        }

        return familyDetails;
    }

    protected void ddlApproved_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlApproved = (DropDownList)sender;

        GridViewRow row = (GridViewRow)ddlApproved.NamingContainer;
        TextBox txtremarks = (TextBox)row.FindControl("txtremarks");

        //int id = Convert.ToInt32(gdvDigitalInterview.DataKeys[row.RowIndex].Values["ddlApproved"]);

        string selectedCategory = ddlApproved.SelectedValue;
        txtremarks.Enabled = (selectedCategory == "R");
        //if (selectedCategory == "R")
        //{
        //    txtremarks.Enabled = true;

        //}
        //else
        //{
        //    txtremarks.Enabled = false;
        //}

    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {


        string selectedvalue = ddlFilter.SelectedValue.ToString();
        FitToJob_Master_InterviewDetail(selectedvalue);

        SqlCommand sqlCmd = new SqlCommand();
        GeneralDAL objDal = new GeneralDAL();
        objDal.OpenSQLConnection();
        sqlCmd.Connection = objDal.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.CommandText = "FitToJob_Master_InterviewDetail";


        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet);
        if (dataSet.Tables[0].Rows.Count > 0)
        {
            if (selectedvalue == "ALL")
            {
                gdvDigitalInterview.DataSource = dataSet.Tables[0];
                gdvDigitalInterview.DataBind();
            }
            else
            {
                DataRow[] filteredRows = dataSet.Tables[0].Select("Status = '" + selectedvalue + "'");

                DataTable filteredTable = dataSet.Tables[0].Clone();

                foreach (DataRow row in filteredRows)
                {
                    filteredTable.ImportRow(row);
                }
                gdvDigitalInterview.DataSource = filteredTable;
                gdvDigitalInterview.DataBind();
            }

        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            //List<DigitalInterviewForm> submittedData = GetDigitalInterviewFormGrid();

            //UpdateDatabase(submittedData);
            UpdateDatabase();


           BindGridView();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }
    }

    private void UpdateDatabase()
    {
        GeneralDAL objDal = new GeneralDAL();
        objDal.OpenSQLConnection();

        try
        {
          foreach (GridViewRow row in gdvDigitalInterview.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    string RegistrationId = gdvDigitalInterview.DataKeys[row.RowIndex].Value.ToString();

                    DropDownList ddlApproved = (DropDownList)row.FindControl("ddlApproved");
                    TextBox txtRemarks = (TextBox)row.FindControl("txtremarks");

                    string Status = ddlApproved.SelectedValue;
                    string Remarks = (Status == "A") ? txtRemarks.Text : null ;


                    using (SqlCommand sqlCmd = new SqlCommand())
                    {
                        sqlCmd.Connection = objDal.ActiveSQLConnection();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "[FitToJob_Master_InterviewDetailUpdate]";

                        sqlCmd.Parameters.AddWithValue("@Status", Status);
                        sqlCmd.Parameters.AddWithValue("@Remarks", Remarks);
                        sqlCmd.Parameters.AddWithValue("@RegistrationId", RegistrationId);
                        sqlCmd.ExecuteNonQuery();
                    }

                   

                }
            }

        }
       
        finally
        {
            objDal.CloseSQLConnection();
        }
    }

    private void FilterData(string selectedStatus)
    {
    }
    protected void gdvDigitalInterview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlApproved = e.Row.FindControl("ddlApproved") as DropDownList;
            TextBox txtremarks = e.Row.FindControl("txtremarks") as TextBox;

            DataRowView drv = (DataRowView)e.Row.DataItem;
            SqlCommand sqlCmd = new SqlCommand();

            HyperLink hlPhoto = e.Row.Cells[12].Controls[0] as HyperLink;
            HyperLink hlVideo = e.Row.Cells[13].Controls[0] as HyperLink;
            HyperLink hlViewPrint = e.Row.Cells[14].Controls[0] as HyperLink;
            HyperLink hlSendMail = e.Row.Cells[15].Controls[0] as HyperLink;


            string photoPath = (e.Row.DataItem as DataRowView)["PhotoPath"].ToString();
            string videoPath = (e.Row.DataItem as DataRowView)["VideoPath"].ToString();
            string viewPrintPath = "../Report/Exam/CRViewer.aspx?RptType=InterviewFormdetial&RptId=" + drv[1].ToString();



            if (hlPhoto != null)
            {
                hlPhoto.NavigateUrl = photoPath;
                hlPhoto.Text = "Click";
            }

            if (hlVideo != null)
            {
                hlVideo.NavigateUrl = videoPath;
                hlVideo.Text = "Click";
            }

            if (hlViewPrint != null)
            {
                hlViewPrint.NavigateUrl = viewPrintPath;
                hlViewPrint.Text = "Download";
            }

            if (hlSendMail != null)
            {
                hlSendMail.NavigateUrl = viewPrintPath;
                hlSendMail.Text = "Send";
            }

            if (ddlApproved != null && txtremarks != null)
            {
                string selectedCategory = ddlApproved.SelectedValue;
                txtremarks.Enabled = (selectedCategory == "R");
            }

        }
    }
    private void FitToJob_Master_InterviewDetail(string status)
    {
        SqlCommand sqlCmd = new SqlCommand();
        GeneralDAL objDal = new GeneralDAL();
        objDal.OpenSQLConnection();
        sqlCmd.Connection = objDal.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.CommandText = "FitToJob_Master_InterviewDetail";

        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet);

        if (dataSet.Tables[0].Rows.Count > 0)
        {
            gdvDigitalInterview.DataSource = dataSet.Tables[0];
            gdvDigitalInterview.DataBind();
        }
    }

    private void BindGridView()
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("FitToJob_Master_InterviewDetail", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvDigitalInterviewForms.DataSource = dt;
                    gvDigitalInterviewForms.DataBind();
                }
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

    private void SendMail(string No, DateTime? Dt, string CustomerName)
    {
        ReportDocument crystalReport = new ReportDocument();
        StringBuilder strbuSummary = new StringBuilder();
        Literal disp = new Literal();
        string fname = "";

        try
        {
            crystalReport.Load(HttpContext.Current.Server.MapPath("~/Reports/Exam/Offerlater1.rpt"));

            DataSet ds = new DataSet();

            ds = GetDataSet(GetSqlQuery(Session["MobileNo"].ToString()));

            RenameDataSetTables(ref ds);

            crystalReport.SetDataSource(ds);

            ExportOptions crExportOptions;
            DiskFileDestinationOptions crDiskFileDestinationOptions;

            fname = HttpContext.Current.Server.MapPath("~\\Export\\" + "Offerlater" + ".pdf");

            crDiskFileDestinationOptions = new DiskFileDestinationOptions();
            crDiskFileDestinationOptions.DiskFileName = fname;
            crExportOptions = crystalReport.ExportOptions;
            crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
            crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;

            crystalReport.Export();

            DataTable DtCompany = new DataTable();
            DtCompany = GetCompanyDetails();

            DataTable dtTO = new DataTable();
            dtTO = GetToMailIds();

            string To = "";

            for (int j = 0; j < dtTO.Rows.Count; j++)
            {
                try
                {
                    if (To != "")
                        To += "," + dtTO.Rows[j]["Email"].ToString().Trim();
                    else
                        To += dtTO.Rows[j]["Email"].ToString().Trim();
                }
                catch
                {
                }
            }
            #region Other Greetings
            strbuSummary.Append("<tr>");
            strbuSummary.Append("<td> Offert Latter</td>");
            strbuSummary.Append("</tr><br>");

            strbuSummary.Append("<tr>");
            strbuSummary.Append("<td>Name: " + CustomerName + "</td>");
            strbuSummary.Append("</tr><br>");


            if (Dt != null)
            {
                strbuSummary.Append("<tr>");
                strbuSummary.Append("<td>Date: " + Convert.ToDateTime(Dt).ToString("dd-MMM-yyyy") + "</td>");
                strbuSummary.Append("</tr><br>");
            }
            if (No != null && No != "")
            {
                strbuSummary.Append("<tr>");
                strbuSummary.Append("<td>No: " + No + "</td>");
                strbuSummary.Append("</tr><br>");
            }

            strbuSummary.Append("</table>");

            strbuSummary.Append("<br />");
            strbuSummary.Append("<br />");
            strbuSummary.Append("Thanks & Regards,<br />");
            strbuSummary.Append("<br />");
            strbuSummary.Append("" + DtCompany.Rows[0]["Name"].ToString());
            strbuSummary.Append("<br/>");
            strbuSummary.Append("<br/>");
            strbuSummary.Append("<br/>");

            strbuSummary.Append("<span style='text-align:left;color:#3300CC;font-size:8px;font-family:Tahoma,Arial;'>*This is system generated mail. Please do not reply here.</span>");
            disp.Text = strbuSummary.ToString();

            disp.Text += "</div>";
            #endregion
            EmailClass.SendEmail("smtp.office365.com", 25, ConfigurationManager.AppSettings["FromEmailId"].ToString(), ConfigurationManager.AppSettings["FromEmailIdPsw"].ToString(), To, "", "", "New SO Created.", disp.Text, System.Web.Mail.MailFormat.Html, fname);
        }
        catch
        {
        }
    }
    private string GetSqlQuery(string sOId)
    {
        string sqlWhere1 = "";
        string sqlWhere2 = "";
        string sql1 = "";
        string sql2 = "";
        string sql = "";
        string sqlCompany = "";


        sqlCompany = "SELECT * FROM vwCompany";


        if (sOId != null)
        {
            sqlWhere1 = " WHERE SOId='" + sOId.ToString() + "'";
            sqlWhere2 = " WHERE SOId='" + sOId.ToString() + "'";
        }
        sql1 = "SELECT * FROM vwSOs" + sqlWhere1;
        //Final Sql Query
        sql = sql1 + ";" + sqlCompany;


        return sql;
    }
    private void RenameDataSetTables(ref DataSet ds)
    {
        ds.Tables[0].TableName = "vwSOs";
        ds.Tables[1].TableName = "vwSOLns";
        ds.Tables[2].TableName = "vwCompany";
    }
    public DataSet GetDataSet(string sql)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataAdapter sqlDA = new SqlDataAdapter();
        DataSet ds = new DataSet();
        GeneralDAL objDal = new GeneralDAL();

        objDal.OpenSQLConnection();

        sqlCmd.Connection = objDal.ActiveSQLConnection();
        sqlCmd.CommandText = sql;
        sqlDA.SelectCommand = sqlCmd;

        sqlDA.Fill(ds);

        objDal.CloseSQLConnection();

        return ds;
    }


    public DataTable GetToMailIds()
    {
        SqlCommand sqlcmd = new SqlCommand();
        DataTable dt = new DataTable();
        GeneralDAL objDal = new GeneralDAL();

        objDal.OpenSQLConnection();
        try
        {
            sqlcmd.Connection = objDal.ActiveSQLConnection();

            sqlcmd.CommandText = " Select Email from Registrations where MobileNo = '6353364866'";
            dt.Load(sqlcmd.ExecuteReader());

            objDal.CloseSQLConnection();
            return dt;
        }
        catch (Exception ex)
        {
            objDal.CloseSQLConnection();
            throw ex;
        }
    }
    public DataTable GetCompanyDetails()
    {
        SqlCommand sqlcmd = new SqlCommand();
        DataTable dt = new DataTable();
        GeneralDAL objDal = new GeneralDAL();

        objDal.OpenSQLConnection();
        try
        {
            sqlcmd.Connection = objDal.ActiveSQLConnection();

            sqlcmd.CommandText = " Select * from Company";
            dt.Load(sqlcmd.ExecuteReader());

            objDal.CloseSQLConnection();
            return dt;
        }
        catch (Exception ex)
        {
            objDal.CloseSQLConnection();
            throw ex;
        }
    }
}