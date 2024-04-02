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

using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

public partial class Exams_CandidatesList : System.Web.UI.Page
{
    private CandidatesListBLL _candidatesListBLL = new CandidatesListBLL();
    private StringBuilder sbjQueryNumeric = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        HideErrors();
        if (!IsPostBack)
        {
            GetCategory();
            CandidateLists();
            BindGridView();
        }
    }

    protected void btnGetSelectedValues_Click(object sender, EventArgs e)
    {

    }

    private void GetCategory()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Report_Candidate_Details";
            sqlCmd.Parameters.AddWithValue("@Action", "GetCategory");
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                //lstCategory.DataSource = dataSet.Tables[0];
                //lstCategory.DataTextField = "Category";
                //lstCategory.DataValueField = "CategoryId";
                //lstCategory.DataBind();
            }
            objDal.CloseSQLConnection();
        }
        catch (Exception ex)
        {
        }
    }

    private void GetSubCategory(string CategoryIds)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Report_Candidate_Details";
            sqlCmd.Parameters.AddWithValue("@Action", "GetSubCategory");
            sqlCmd.Parameters.AddWithValue("@SubCategoryId", CategoryIds);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                //lstSecondaryPosition.DataSource = dataSet.Tables[0];
                //lstSecondaryPosition.DataTextField = "SubCategory";
                //lstSecondaryPosition.DataValueField = "SubCategoryId";
                //lstSecondaryPosition.DataBind();
            }
            else
            {
                //lstSecondaryPosition.DataSource = null;
                //lstSecondaryPosition.DataBind();
            }
        }
        catch (Exception)
        {
        }
    }
    protected void lstCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            //string selectedValues = string.Empty;
            //foreach (ListItem li in lstCategory.Items)
            //{
            //    if (li.Selected == true)
            //    {
            //        selectedValues += li.Value + ",";
            //    }
            //}
            //if (selectedValues != "")
            //{
            //    selectedValues = selectedValues.Substring(0, selectedValues.Length - 1);
            //    GetSubCategory(selectedValues);
            //}
        }
        catch (Exception)
        {

        }
    }

    private void CandidateLists()
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            // Add JavaScript for column filtering
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                var input = new HtmlGenericControl("input");
                input.Attributes["type"] = "text";
                input.Attributes["class"] = "form-control";
                input.Attributes["placeholder"] = "Filter " + e.Row.Cells[i].Text;
                input.Attributes["oninput"] = "filterColumn(" + i + ")";

                e.Row.Cells[i].Controls.Add(input);
            }
        }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        //for (int i = 0; i < GridView1.Columns.Count; i++)
        //{
        //    TableHeaderCell cell = new TableHeaderCell();
        //    TextBox txtSearch = new TextBox();
        //    txtSearch.Attributes["placeholder"] = GridView1.Columns[i].HeaderText;
        //    txtSearch.CssClass = "search_textbox";
        //    cell.Controls.Add(txtSearch);
        //    row.Controls.Add(cell);
        //}
        //GridView1.HeaderRow.Parent.Controls.AddAt(1, row);
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void gvDigitalInterviewForms_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

        }
    }

    protected void ddlApproved_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlApproved = (DropDownList)sender;

        GridViewRow row = (GridViewRow)ddlApproved.NamingContainer;
        TextBox txtremarks = (TextBox)row.FindControl("txtremarks");

        //int id = Convert.ToInt32(gdvDigitalInterview.DataKeys[row.RowIndex].Values["ddlApproved"]);

        string selectedCategory = ddlApproved.SelectedValue;

        if (selectedCategory == "R")
        {
            txtremarks.Enabled = true;

        }
        else
        {
            txtremarks.Enabled = false;
        }

    }

    protected void gdvDigitalInterview_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataRowView drv = (DataRowView)e.Row.DataItem;
            SqlCommand sqlCmd = new SqlCommand();

            HyperLink hlPhoto = e.Row.Cells[12].Controls[0] as HyperLink;
            HyperLink hlVideo = e.Row.Cells[13].Controls[0] as HyperLink;
            HyperLink hlResume = e.Row.Cells[14].Controls[0] as HyperLink;
            HyperLink hlViewPrint = e.Row.Cells[15].Controls[0] as HyperLink;
            //HyperLink hlSendMail = e.Row.Cells[16].Controls[0] as HyperLink;

            TextBox txtremarks = e.Row.FindControl("txtremarks") as TextBox;
            DropDownList ddlApproved = e.Row.FindControl("ddlApproved") as DropDownList;

            string photoPath = (e.Row.DataItem as DataRowView)["PhotoPath"].ToString();
            string videoPath = (e.Row.DataItem as DataRowView)["VideoPath"].ToString();
            string ResumePath = (e.Row.DataItem as DataRowView)["ResumePath"].ToString();
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

            if (ddlApproved != null && txtremarks != null)
            {
                string selectedCategory = ddlApproved.SelectedValue;
                txtremarks.Enabled = (selectedCategory == "R");
            }
            if (hlResume != null)
            {
                hlResume.NavigateUrl = ResumePath;
                hlResume.Text = "View";
            }

            //if (hlSendMail != null)
            //{
                
            //    hlSendMail.Text = "Send Mail";
            //}


        }
    }

    protected void gdvDigitalInterview_SelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedIndex = gdvDigitalInterview.SelectedIndex;
        string columnName = gdvDigitalInterview.Rows[selectedIndex].Cells[0].Text;
      
    }

    private void BindGridView()
    {
        try
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
            objDal.CloseSQLConnection();
        }
        catch (Exception)
        {
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
                    string Remarks = txtRemarks.Text;
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
        //ReportDocument crystalReport = new ReportDocument();
        StringBuilder strbuSummary = new StringBuilder();
        Literal disp = new Literal();
        string fname = "";

        try
        {
            //crystalReport.Load(HttpContext.Current.Server.MapPath("~/Reports/Exam/Offerlater1.rpt"));

            DataSet ds = new DataSet();

            ds = GetDataSet(GetSqlQuery(Session["MobileNo"].ToString()));

            RenameDataSetTables(ref ds);

            //crystalReport.SetDataSource(ds);

            //ExportOptions crExportOptions;
            //DiskFileDestinationOptions crDiskFileDestinationOptions;

            fname = HttpContext.Current.Server.MapPath("~\\Export\\" + "Offerlater" + ".pdf");

            //crDiskFileDestinationOptions = new DiskFileDestinationOptions();
            //crDiskFileDestinationOptions.DiskFileName = fname;
            //crExportOptions = crystalReport.ExportOptions;
            //crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
            //crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            //crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;

            //crystalReport.Export();

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
    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();
    }
}