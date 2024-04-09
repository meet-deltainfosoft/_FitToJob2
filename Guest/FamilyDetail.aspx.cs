using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

public partial class Guest_FamilyDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Bind the GridView with data
            if (string.IsNullOrEmpty((string)Session["MobileNo"]) || string.IsNullOrEmpty((string)Session["Language"]))
            {
                Response.Redirect("~/Guest/SelectLanguage.aspx");
            }

            else
            {
                if (Session["IsApproved"].ToString() != "" || Session["IsApproved"] != null)
                {
                    if (Convert.ToBoolean(Session["IsApproved"]) == true)
                    {
                        btnAddRow.Enabled = false;
                        gvFamilyDetails.Enabled = false;
                        ShowErrors("error", "Your Profile is reviwed for change please contact administrator.");
                    }
                }
                else
                {
                    Response.Redirect("~/Guest/SelectLanguage.aspx");
                }

                if (Session["Language"] == "Gujarati")
                {
                    btnSubmit.Text = "સાચવો અને આગળ જાઓ";
                }
                else if (Session["Language"] == "Hindi")
                {
                    btnSubmit.Text = "सहेजें और आगे बढ़ें";
                }
                BindGridView(Session["MobileNo"].ToString());
            }

        }
    }

    private void BindGridView(string MobileNo)
    {
        try
        {
            if (Session["Language"] == "Gujarati")
            {
                gvFamilyDetails.Columns[0].HeaderText = "સંબંધ";
                gvFamilyDetails.Columns[1].HeaderText = "નામ";
                gvFamilyDetails.Columns[2].HeaderText = "વેપાર";
                gvFamilyDetails.Columns[3].HeaderText = "શૈક્ષણિક લાયકાત";
                gvFamilyDetails.Columns[4].HeaderText = "મોબાઇલ નંબર";
                gvFamilyDetails.Columns[5].HeaderText = "ક્રિયાઓ";
                lblTitle.Text = "કૌટુંબિક વિગત (રાશન કાર્ડ મુજબ)";
            }
            else if (Session["Language"] == "Hindi")
            {
                gvFamilyDetails.Columns[0].HeaderText = "शिक्षा स्तर";
                gvFamilyDetails.Columns[1].HeaderText = "बोर्ड/विश्वविद्यालय का नाम";
                gvFamilyDetails.Columns[2].HeaderText = "व्यापार";
                gvFamilyDetails.Columns[3].HeaderText = "शैक्षणिक योग्यता";
                gvFamilyDetails.Columns[4].HeaderText = "मोबाइल नंबर";
                gvFamilyDetails.Columns[5].HeaderText = "कार्रवाई";
                lblTitle.Text = "पारिवारिक विवरण (राशन कार्ड के अनुसार)";
            }
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.CommandText = "FitToJob_Master_Registration";
            sqlCmd.Parameters.AddWithValue("@Action", "CandidateFamilyDetailById");
            sqlCmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                gvFamilyDetails.DataSource = dataSet.Tables[0];
                gvFamilyDetails.DataBind();

            }
            else
            {
                if (dataSet.Tables[1].Rows.Count > 0)
                {
                    List<FamilyDetail> familyDetails = new List<FamilyDetail>();
                    if (dataSet.Tables[1].Rows[0]["MaritalStatus"].ToString() == "Married")
                    {
                        familyDetails.Add(new FamilyDetail { Relation = "Father", Name = "", Business = "", Education = "", Mobile = "" });
                        familyDetails.Add(new FamilyDetail { Relation = "Mother", Name = "", Business = "", Education = "", Mobile = "" });
                        familyDetails.Add(new FamilyDetail { Relation = "Spouse", Name = "", Business = "", Education = "", Mobile = "" });
                    }
                    else
                    {
                        familyDetails.Add(new FamilyDetail { Relation = "Father", Name = "", Business = "", Education = "", Mobile = "" });
                        familyDetails.Add(new FamilyDetail { Relation = "Mother", Name = "", Business = "", Education = "", Mobile = "" });
                    }

                    gvFamilyDetails.DataSource = familyDetails;
                    gvFamilyDetails.DataBind();
                }
            }
            objDal.CloseSQLConnection();
        }
        catch (Exception)
        {
        }
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        AddEmptyRow();
    }

    private void AddEmptyRow()
    {
        List<FamilyDetail> familyDetails = GetFamilyDetailsFromGrid();

        familyDetails.Add(new FamilyDetail { Relation = "Father", Name = "", DOB = "", Business = "", Education = "", Mobile = "" });
        gvFamilyDetails.DataSource = familyDetails;
        gvFamilyDetails.DataBind();
    }

    protected void gvFamilyDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "RemoveRow")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);


            List<FamilyDetail> familyDetails = GetFamilyDetailsFromGrid();


            if (rowIndex == 0 && familyDetails.Count > 1)
            {
                FamilyDetail familyDetail = familyDetails[rowIndex];

                if (Action(familyDetail))
                {
                    familyDetails.RemoveAt(1);

                    // Bind the updated data source to the GridView
                    gvFamilyDetails.DataSource = familyDetails;
                    gvFamilyDetails.DataBind();
                }
                else
                {

                    lblMessage.Text = "Validation failed. Cannot remove the row.";
                    lblMessage.Visible = true;
                }
            }
            else if (rowIndex == 1)
            {
                // Remove the second row (index 1) from the underlying data source
                if (familyDetails.Count > 1)
                {
                    familyDetails.RemoveAt(1);
                }

                // Bind the updated data source to the GridView
                gvFamilyDetails.DataSource = familyDetails;
                gvFamilyDetails.DataBind();
            }
        }
    }

    private bool Action(FamilyDetail familyDetail)
    {

        if (string.IsNullOrWhiteSpace(familyDetail.Name))
        {
            return false;
        }

        return true;
    }

    private void RemoveRow(int rowIndex)
    {
        List<FamilyDetail> familyDetails = GetFamilyDetailsFromGrid();

        if (familyDetails.Count > rowIndex)
        {
            familyDetails.RemoveAt(rowIndex);
            gvFamilyDetails.DataSource = familyDetails;
            gvFamilyDetails.DataBind();
        }
    }

    private List<FamilyDetail> GetFamilyDetailsFromGrid()
    {
        List<FamilyDetail> familyDetails = new List<FamilyDetail>();

        foreach (GridViewRow row in gvFamilyDetails.Rows)
        {
            FamilyDetail detail = new FamilyDetail();

            // Null checks for TextBoxes
            DropDownList ddlRelation = row.FindControl("ddlRelation") as DropDownList;
            TextBox txtName = row.FindControl("txtName") as TextBox;
            TextBox txtBusiness = row.FindControl("txtBusiness") as TextBox;
            TextBox txtEducation = row.FindControl("txtEducation") as TextBox;
            TextBox txtMobile = row.FindControl("txtMobile") as TextBox;
            TextBox txtDOB = row.FindControl("txtDOB") as TextBox;

            // Assign values after null checks
            detail.Relation = ddlRelation != null ? ddlRelation.SelectedValue : "Father";
            detail.Name = (txtName != null) ? txtName.Text : string.Empty;
            detail.Business = (txtBusiness != null) ? txtBusiness.Text : string.Empty;
            detail.Education = (txtEducation != null) ? txtEducation.Text : string.Empty;
            detail.Mobile = (txtMobile != null) ? txtMobile.Text : string.Empty;
            detail.DOB = (txtDOB != null) ? txtDOB.Text : string.Empty;

            familyDetails.Add(detail);
        }

        return familyDetails;
    }

    public class FamilyDetail
    {
        public string Relation { get; set; }
        public string Name { get; set; }
        public string Business { get; set; }
        public string Education { get; set; }
        public string Mobile { get; set; }
        public string DOB { get; set; }
    }

    protected void lnkBtnSubmit_click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToBoolean(Session["IsApproved"]) == true)
            {
                Response.Redirect("~/Guest/ExperienceDetails.aspx");
            }
            else
            {

                bool IsValidated = true;
                DataTable familyTable = new DataTable();
                familyTable.Columns.Add("Relation", typeof(string));
                familyTable.Columns.Add("DOB", typeof(string));
                familyTable.Columns.Add("Name", typeof(string));
                familyTable.Columns.Add("Business", typeof(string));
                familyTable.Columns.Add("Education", typeof(string));
                familyTable.Columns.Add("Mobile", typeof(string));
                

                foreach (GridViewRow row in gvFamilyDetails.Rows)
                {
                    DropDownList ddlRelation = (DropDownList)row.FindControl("ddlRelation");
                    TextBox txtName = (TextBox)row.FindControl("txtName");
                    TextBox txtBusiness = (TextBox)row.FindControl("txtBusiness");
                    TextBox txtEducation = (TextBox)row.FindControl("txtEducation");
                    TextBox txtMobile = (TextBox)row.FindControl("txtMobile");
                    TextBox txtDOB = (TextBox)row.FindControl("txtDOB");

                    if (ddlRelation.SelectedValue == "" || txtName.Text.Trim() == "" || txtBusiness.Text.Trim() == "" || txtEducation.Text.Trim() == "" || txtMobile.Text.Trim() == "")
                    {
                        IsValidated = false;
                    }
                    DataRow dataRow = familyTable.NewRow();
                    dataRow["Relation"] = ddlRelation.SelectedValue;
                    dataRow["Name"] = txtName.Text;
                    dataRow["Business"] = txtBusiness.Text;
                    dataRow["Education"] = txtEducation.Text;
                    dataRow["Mobile"] = txtMobile.Text;
                    dataRow["DOB"] = Convert.ToDateTime(txtDOB.Text).ToString("dd-MMM-yyyy");

                    







                    familyTable.Rows.Add(dataRow);

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
                    sqlCmd.Parameters.AddWithValue("@Action", "CandidateFamilyDetails");
                    sqlCmd.Parameters.AddWithValue("@MobileNo", Session["MobileNo"].ToString());
                    SqlParameter tvpParam = new SqlParameter("@UT_CandidateFamilyDetails", SqlDbType.Structured);
                    tvpParam.Value = familyTable;
                    tvpParam.TypeName = "dbo.UT_CandidateFamilyDetails"; // Adjust the schema and type name accordingly

                    // Add the TVP parameter to the SqlCommand
                    sqlCmd.Parameters.Add(tvpParam);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["IsSuccessful"].ToString() == "1")
                        {
                            Response.Redirect("~/Guest/ExperienceDetails.aspx");
                        }
                    }
                    objDal.CloseSQLConnection();

                }
            }

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