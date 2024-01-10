using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Guest_SubCategory : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Session["MobileNo"].ToString() != "" && Session["Language"].ToString() != "")
            {
                if (Session["Language"].ToString() == "Gujarati")
                {
                    lblTitle.Text = "જોબ પ્રોફાઇલ એન્ટ્રી - [નવો મોડ]";
                    lblStaffCategoryId.Text = "સ્ટાફ કેટેગરી";
                    chkallStaffCategory.Text = "બધા પસંદ કરો";
                    btnOk.Text = "સાચવો";
                    btnCancel.Text = "રદ કરો";
                }
                else if (Session["Language"].ToString() == "Hindi")
                {
                    lblTitle.Text = "जॉब प्रोफ़ाइल एंट्री - [नया मोड]";
                    lblStaffCategoryId.Text = "कर्मचारी वर्ग";
                    chkallStaffCategory.Text = "सबका चयन करें";
                    btnOk.Text = "सहेजें";
                    btnCancel.Text = "रद्द करें";
                }
            }
            else
            {
                Response.Redirect("~/Guest/SignIn.aspx");
            }
        }
        catch
        {
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Session["MobileNo"].ToString() != "")
                {
                    if (Session["IsApproved"].ToString() != "" || Session["IsApproved"] != null)
                    {
                        if (Convert.ToBoolean(Session["IsApproved"]) == true)
                        {
                            chkallStaffCategory.Enabled = false;
                            chkStaffCategory.Enabled = false;
                            ShowErrors("error", "Your Profile is reviwed for change please contact administrator.");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Guest/SignIn.aspx");
                    }

                    GetSubCategory(Session["MobileNo"].ToString());
                    GetSubCategoryById(Session["MobileNo"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
        }
    }

    private void GetSubCategory(string MobileNo)
    {
        try
        {
            ListItem li = new ListItem();

            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Master_SubJobOfferings";
            sqlCmd.Parameters.AddWithValue("@Action", "GetSubCategory");
            sqlCmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dtr in dataSet.Tables[0].Rows)
                {
                    li = new ListItem();

                    li.Text = dtr[1].ToString();
                    li.Value = dtr[0].ToString();
                    chkStaffCategory.Items.Add(li);

                    li = null;
                }
            }
            objDal.CloseSQLConnection();
        }
        catch
        {
        }
    }

    protected void chkallStaffCategory_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkallStaffCategory.Checked == true)
            {
                foreach (ListItem chkall in chkStaffCategory.Items)
                {
                    chkall.Selected = true;
                    string k = "";
                    for (int i = 0; i < chkStaffCategory.Items.Count; i++)
                    {
                        if (chkStaffCategory.Items[i].Selected)
                        {
                            if (k != null && k != "")
                                k = k + "," + chkStaffCategory.Items[i].Value;
                            else
                                k = k + chkStaffCategory.Items[i].Value;
                        }
                    }


                }
            }
            else
            {
                foreach (ListItem chkall in chkStaffCategory.Items)
                {
                    chkall.Selected = false;
                }
            }
        }
        catch
        {
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            // Add any necessary cleanup or redirect logic here
            Response.Redirect("~/General/Default.aspx");
        }
        catch (Exception ex)
        {
            // Handle exceptions as needed
            ShowErrors("Error", ex.Message);
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            if (Convert.ToBoolean(Session["IsApproved"]) == true)
            {
                Response.Redirect("~/Guest/InterviewForms.aspx");
            }
            else
            {

                string StaffCategoryId = null;

                if (chkStaffCategory.Items.Count > 0)
                {
                    for (int i = 0; i < chkStaffCategory.Items.Count; i++)
                    {
                        if (chkStaffCategory.Items[i].Selected)
                        {

                            if (StaffCategoryId != null && StaffCategoryId != "")
                                StaffCategoryId = StaffCategoryId + "," + chkStaffCategory.Items[i].Value;
                            else
                                StaffCategoryId = StaffCategoryId + chkStaffCategory.Items[i].Value;
                        }
                    }

                    if (StaffCategoryId == null)
                    {
                        if (Session["Language"].ToString() == "Gujarati")
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "કૃપા કરીને ઓછામાં ઓછી એક શ્રેણી પસંદ કરો";
                            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "કૃપા કરીને ઓછામાં ઓછી એક શ્રેણી પસંદ કરો", true);
                        }
                        else if (Session["Language"].ToString() == "Hindi")
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "कृपया कम से कम एक श्रेणी चुनें";
                        }
                        else if (Session["Language"].ToString() == "English")
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Please Select at Least one Category";
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
                        sqlCmd.CommandText = "FitToJob_Master_SubJobOfferings";
                        sqlCmd.Parameters.AddWithValue("@Action", "InsertSubJobOfferings");
                        sqlCmd.Parameters.AddWithValue("@MobileNo", Session["MobileNo"].ToString());
                        sqlCmd.Parameters.AddWithValue("@SubJobOfferingIds", StaffCategoryId);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                        DataSet dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows[0]["IsSuccessful"].ToString() == "1")
                            {
                                Response.Redirect("~/Guest/InterviewForms.aspx");
                            }
                        }
                    }

                }
            }
        }
        catch
        {
        }
    }

    private void GetSubCategoryById(string MobileNo)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Master_SubJobOfferings";
            sqlCmd.Parameters.AddWithValue("@Action", "GetSubCategoryById");
            sqlCmd.Parameters.AddWithValue("@MobileNo", MobileNo);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            string SubCategoryId = null;
            dataAdapter.Fill(dataSet);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                for (int i = 0; i < chkStaffCategory.Items.Count; i++)
                {
                    string columnName = "SubCategoryId";
                    if (chkStaffCategory.Items[i].Value == row[columnName].ToString())
                    {
                        chkStaffCategory.Items[i].Selected = true;

                        if (SubCategoryId != null && SubCategoryId != "")
                            SubCategoryId = SubCategoryId + "," + chkStaffCategory.Items[i].Value;
                        else
                            SubCategoryId = SubCategoryId + chkStaffCategory.Items[i].Value;
                    }
                }
            }

        }
        catch (Exception ex)
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