using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Guest_ExperienceDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty((string)Session["MobileNo"]) || string.IsNullOrEmpty((string)Session["Language"]))
            {
                Response.Redirect("~/Guests/SelectLanguage.aspx");
            }
            else
            {
                if (Session["IsApproved"].ToString() != "" || Session["IsApproved"] != null)
                {
                    if (Convert.ToBoolean(Session["IsApproved"]) == true)
                    {
                        btnAddRow.Enabled = false;
                        gvExperienceyDetails.Enabled = false;
                        ShowErrors("error", "Your Profile is reviwed for change please contact administrator.");
                    }
                }
                else
                {
                    Response.Redirect("~/Guest/SelectLanguage.aspx");
                }


                if (Session["Language"] == "Gujarati")
                {
                    lblTitle.Text = "અનુભવની વિગત";
                    btnSubmit.Text = "સાચવો અને આગળ જાઓ";
                    lblNotes.InnerText = "વર્ષ અને મહિનામાં અનુભવ (દા.ત. 1.2)";
                }
                else if (Session["Language"] == "Hindi")
                {
                    lblTitle.Text = "अनुभव विवरण";
                    btnSubmit.Text = "सहेजें और आगे बढ़ें";
                    lblNotes.InnerText = "वर्ष और माह में अनुभव (उदाहरण 1.2)";
                }
            }
            BindGridView(Session["MobileNo"].ToString());
        }
    }

    protected void lnkBtnSubmit_click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToBoolean(Session["IsApproved"]) == true)
            {
                Response.Redirect("~/Guest/ThankYouPage.aspx");
            }
            else
            {

                bool IsValidated = true;
                DataTable ExperianceTable = new DataTable();
                ExperianceTable.Columns.Add("CompanyName", typeof(string));
                ExperianceTable.Columns.Add("CompanyAddress", typeof(string));
                ExperianceTable.Columns.Add("Designations", typeof(string));
                ExperianceTable.Columns.Add("Experience", typeof(string));
                ExperianceTable.Columns.Add("LastSalaryDetail", typeof(string));

                foreach (GridViewRow row in gvExperienceyDetails.Rows)
                {
                    TextBox txtCompanyName = (TextBox)row.FindControl("txtCompanyName");
                    TextBox txtCompanyAddress = (TextBox)row.FindControl("txtCompanyAddress");
                    TextBox txtDesignations = (TextBox)row.FindControl("txtDesignations");
                    TextBox txtExperience = (TextBox)row.FindControl("txtExperience");
                    TextBox txtLastSalaryDetail = (TextBox)row.FindControl("txtLastSalaryDetail");

                    if (txtCompanyName.Text == "" || txtCompanyAddress.Text.Trim() == "" || txtDesignations.Text.Trim() == "" || txtExperience.Text.Trim() == "" || txtLastSalaryDetail.Text.Trim() == "")
                    {
                        IsValidated = false;
                    }
                    else
                    {
                        DataRow dataRow = ExperianceTable.NewRow();
                        dataRow["CompanyName"] = txtCompanyName.Text;
                        dataRow["CompanyAddress"] = txtCompanyAddress.Text;
                        dataRow["Designations"] = txtDesignations.Text;
                        dataRow["Experience"] = txtExperience.Text;
                        dataRow["LastSalaryDetail"] = txtLastSalaryDetail.Text;

                        ExperianceTable.Rows.Add(dataRow);
                    }

                }
                if (IsValidated == false)
                {
                    lblMessage.Visible = true;
                    if (Session["Language"].ToString() == "Gujarati")
                    {

                        lblMessage.Text = "કૃપા કરીને ફરજિયાત મૂલ્ય દાખલ કરો";
                    }
                    else if (Session["Language"].ToString() == "Hindi")
                    {
                        lblMessage.Text = "कृपया अनिवार्य मान दर्ज करें";
                    }
                }
                else
                {
                    lblMessage.Visible = false;
                    SqlCommand sqlCmd = new SqlCommand();
                    GeneralDAL objDal = new GeneralDAL();
                    objDal.OpenSQLConnection();
                    sqlCmd.Connection = objDal.ActiveSQLConnection();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "FitToJob_Master_Registration";
                    sqlCmd.Parameters.AddWithValue("@Action", "UpdateExperience");
                    sqlCmd.Parameters.AddWithValue("@MobileNo", Session["MobileNo"]);
                    SqlParameter tvpParam = new SqlParameter("@UT_CandidateExperienceDetails", SqlDbType.Structured);
                    tvpParam.Value = ExperianceTable;
                    tvpParam.TypeName = "dbo.UT_CandidateExperienceDetails";
                    sqlCmd.Parameters.Add(tvpParam);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["IsSuccessful"].ToString() == "1")
                        {
                            string message = "Job Applied Successfully  ";
                            if (Session["Language"].ToString() == "Gujarati")
                            {
                                message = "જોબ સફળતાપૂર્વક લાગુ થઈ";
                            }
                            else if (Session["Language"].ToString() == "Hindi")
                            {
                                message = "नौकरी के लिए सफलतापूर्वक आवेदन किया गया";
                            }

                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append(message);
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                        }
                    }
                    objDal.CloseSQLConnection();
                    Response.Redirect("~/Guest/ThankYouPage.aspx");

                }
            }

        }
        catch (Exception)
        {
        }
    }

    private void AddEmptyRow()
    {
        List<ExperianceDetail> ExperianceDetail = GetExperianceDetailGrid();

        ExperianceDetail.Add(new ExperianceDetail { CompanyName = "", CompanyAddress = "", Designations = "", Experience = "0.0", LastSalaryDetail = "0.0" });
        gvExperienceyDetails.DataSource = ExperianceDetail;
        gvExperienceyDetails.DataBind();
    }

    public class ExperianceDetail
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string Designations { get; set; }
        public string Experience { get; set; }
        public string LastSalaryDetail { get; set; }
    }

    private List<ExperianceDetail> GetExperianceDetailGrid()
    {
        List<ExperianceDetail> experianceDetails = new List<ExperianceDetail>();

        foreach (GridViewRow row in gvExperienceyDetails.Rows)
        {
            ExperianceDetail detail = new ExperianceDetail();

            // Null checks for TextBoxes
            TextBox txtCompanyName = row.FindControl("txtCompanyName") as TextBox;
            TextBox txtCompanyAddress = row.FindControl("txtCompanyAddress") as TextBox;
            TextBox txtDesignations = row.FindControl("txtDesignations") as TextBox;
            TextBox txtExperience = row.FindControl("txtExperience") as TextBox;
            TextBox txtLastSalaryDetail = row.FindControl("txtLastSalaryDetail") as TextBox;

            // Assign values after null checks
            detail.CompanyAddress = txtCompanyAddress != null ? txtCompanyAddress.Text : string.Empty;
            detail.CompanyName = (txtCompanyName != null) ? txtCompanyName.Text : string.Empty;
            detail.Designations = (txtDesignations != null) ? txtDesignations.Text : string.Empty;
            detail.Experience = (txtExperience != null) ? txtExperience.Text : "0";
            detail.LastSalaryDetail = (txtLastSalaryDetail != null) ? txtLastSalaryDetail.Text : "0";


            experianceDetails.Add(detail);
        }

        return experianceDetails;
    }

    protected void gvExperienceDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            RemoveRow(rowIndex);
        }
    }

    private void RemoveRow(int rowIndex)
    {
        List<ExperianceDetail> experianceDetails = GetExperianceDetailGrid();

        if (experianceDetails.Count > rowIndex)
        {
            experianceDetails.RemoveAt(rowIndex);
            gvExperienceyDetails.DataSource = experianceDetails;
            gvExperienceyDetails.DataBind();
        }
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        AddEmptyRow();
    }

    private void BindGridView(string MobileNo)
    {
        try
        {
            if (Session["Language"] == "Gujarati")
            {
                gvExperienceyDetails.Columns[0].HeaderText = "કંપની નામ";
                gvExperienceyDetails.Columns[1].HeaderText = "કંપની સરનામું";
                gvExperienceyDetails.Columns[2].HeaderText = "પદનામ";
                gvExperienceyDetails.Columns[3].HeaderText = "અનુભવ";
                gvExperienceyDetails.Columns[4].HeaderText = "છેલ્લો પગાર વિગત";
                gvExperienceyDetails.Columns[5].HeaderText = "ક્રિયાઓ";

            }
            else if (Session["Language"] == "Hindi")
            {
                gvExperienceyDetails.Columns[0].HeaderText = "कंपनी का नाम";
                gvExperienceyDetails.Columns[1].HeaderText = "कम्पनी का पता";
                gvExperienceyDetails.Columns[2].HeaderText = "पदनाम";
                gvExperienceyDetails.Columns[3].HeaderText = "अनुभव";
                gvExperienceyDetails.Columns[4].HeaderText = "अंतिम वेतन विवरण";
                gvExperienceyDetails.Columns[5].HeaderText = "कार्रवाई";
            }

            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Master_Registration";
            sqlCmd.Parameters.AddWithValue("@Action", "GetExperienceById");
            sqlCmd.Parameters.AddWithValue("@MobileNo", Session["MobileNo"]);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                gvExperienceyDetails.DataSource = dataSet.Tables[0];
                gvExperienceyDetails.DataBind();
            }
            else
            {
                List<ExperianceDetail> experianceDetail = new List<ExperianceDetail>();
                experianceDetail.Add(new ExperianceDetail { CompanyName = "", CompanyAddress = "", Designations = "", Experience = "0.0", LastSalaryDetail = "0.0" });
                gvExperienceyDetails.DataSource = experianceDetail;
                gvExperienceyDetails.DataBind();
            }
        }
        catch
        {


        }
    }
    protected void lnkPrint_click(object sender, EventArgs e)
    {
        try
        {
            string MobileNo = Session["MobileNo"].ToString();

            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select Top 1 userId From Users_ Where UserName = '" + MobileNo + "'";
            string UserId = sqlCmd.ExecuteScalar().ToString();

            Response.Redirect("../Report/Exam/CRViewer.aspx?RptType=InterviewFormdetial&RptId=" + UserId.ToString());
        }
        catch (Exception)
        {
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
}