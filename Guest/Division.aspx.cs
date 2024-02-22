using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Guest_Division : System.Web.UI.Page
{

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Session["MobileNo"].ToString() != "" && Session["Language"].ToString() != "")
            {
                if (Session["Language"].ToString() == "Gujarati")
                {
                    lblTitle.Text = "વિભાગ એન્ટ્રી - [નવો મોડ]";
                    lblDivisionId.Text = "વિભાગ";
                    chkallDivision.Text = "બધા પસંદ કરો";
                    btnOk.Text = "સાચવો";
                    btnCancel.Text = "રદ કરો";
                }
                else if (Session["Language"].ToString() == "Hindi")
                {
                    lblTitle.Text = "विभाजन एंट्री - [नया मोड]";
                    lblDivisionId.Text = "विभाजन वर्ग";
                    chkallDivision.Text = "सबका चयन करें";
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
                            chkallDivision.Enabled = false;
                            chkDivision.Enabled = false;
                            ShowErrors("error", "Your Profile is reviwed for change please contact administrator.");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/Guest/SignIn.aspx");
                    }

                    GetAllUtilities(Session["MobileNo"].ToString());
                    GetDivisionById(Session["MobileNo"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    private void GetAllUtilities(string MobileNo)
    {
        try
        {
            ListItem li = new ListItem();

            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Android_Application";
            sqlCmd.Parameters.AddWithValue("@Action", "GetAllUtilities");
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
                    chkDivision.Items.Add(li);

                    li = null;
                }
            }
            objDal.CloseSQLConnection();
        }
        catch
        {
        }
    }
    protected void chkallDivision_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkallDivision.Checked == true)
            {
                foreach (ListItem chkall in chkDivision.Items)
                {
                    chkall.Selected = true;
                    string k = "";
                    for (int i = 0; i < chkDivision.Items.Count; i++)
                    {
                        if (chkDivision.Items[i].Selected)
                        {
                            if (k != null && k != "")
                                k = k + "," + chkDivision.Items[i].Value;
                            else
                                k = k + chkDivision.Items[i].Value;
                        }
                    }


                }
            }
            else
            {
                foreach (ListItem chkall in chkDivision.Items)
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
            //  Response.Redirect("~/General/Default.aspx");
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
                Response.Redirect("~/Guest/JobProfile.aspx");
            }
            else
            {

                string CandidateId = "579F63B0-A86F-46EA-97DB-51DE75E7ABC7";
                string UtilitieIds = null;
                string userId = null;

                if (chkDivision.Items.Count > 0)
                {
                    for (int i = 0; i < chkDivision.Items.Count; i++)
                    {
                        if (chkDivision.Items[i].Selected)
                        {

                            if (UtilitieIds != null && UtilitieIds != "")
                                UtilitieIds = UtilitieIds + "," + chkDivision.Items[i].Value;
                            else
                                UtilitieIds = UtilitieIds + chkDivision.Items[i].Value;
                        }
                    }

                    if (CandidateId == null)
                    {
                        if (Session["Language"].ToString() == "Gujarati")
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "કૃપા કરીને ઓછામાં ઓછી એક વિભાગ પસંદ કરો";
                            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "કૃપા કરીને ઓછામાં ઓછી એક શ્રેણી પસંદ કરો", true);
                        }
                        else if (Session["Language"].ToString() == "Hindi")
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "कृपया कम से कम एक विभाजन चुनें";
                        }
                        else if (Session["Language"].ToString() == "English")
                        {
                            lblMessage.Visible = true;
                            lblMessage.Text = "Please Select at Least one Division";
                        }
                    }
                    else
                    {
                        string A_CandidateId = GetCandidateId(Session["MobileNo"].ToString());
                        lblMessage.Visible = false;
                        SqlCommand sqlCmd = new SqlCommand();
                        GeneralDAL objDal = new GeneralDAL();
                        objDal.OpenSQLConnection();
                        sqlCmd.Connection = objDal.ActiveSQLConnection();
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.CommandText = "FitToJob_Android_Application";
                        sqlCmd.Parameters.AddWithValue("@Action", "Insertdivisions");
                        sqlCmd.Parameters.AddWithValue("@MobileNo", Session["MobileNo"].ToString());
                        sqlCmd.Parameters.AddWithValue("@UtilitieIds", UtilitieIds);
                        //string subQuery = "SELECT TOP 1 userId FROM Users_ WHERE username = @Username";

                        //sqlCmd.Parameters.AddWithValue("@CandidateId", subQuery);
                        //sqlCmd.Parameters.AddWithValue("@userId", "579F63B0-A86F-46EA-97DB-51DE75E7ABC7");
                        sqlCmd.Parameters.AddWithValue("@CandidateId", A_CandidateId);
                        sqlCmd.Parameters.AddWithValue("@userId", A_CandidateId);
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                        DataSet dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        if (dataSet.Tables[0].Rows.Count > 0)
                        {
                            if (dataSet.Tables[0].Rows[0]["IsSuccessful"].ToString() == "1")
                            {
                                Response.Redirect("~/Guest/JobProfile.aspx");
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
    private void GetDivisionById    (string MobileNo)
    {
        try
        {
            string CandidateId = GetCandidateId(MobileNo);

            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Android_Application";
            sqlCmd.Parameters.AddWithValue("@Action", "GetAllUtilities");
            sqlCmd.Parameters.AddWithValue("@CandidateId", CandidateId);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            //string CandidateId = null;
            dataAdapter.Fill(dataSet);
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                for (int i = 0; i < chkDivision.Items.Count; i++)
                {
                    string columnName = "UtilitieId";
                    if (chkDivision.Items[i].Value == row[columnName].ToString())
                    {
                        bool isChecked = Convert.ToBoolean(row["IsChecked"]);
                        chkDivision.Items[i].Selected = isChecked;

                        if (CandidateId != null && CandidateId != "")
                            CandidateId = CandidateId + "," + chkDivision.Items[i].Value;
                        else
                            CandidateId = CandidateId + chkDivision.Items[i].Value;
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

    public string GetCandidateId(string MobileNo)
    {
        string CandidateId = "";
        SqlCommand sqlCmd = new SqlCommand();
        GeneralDAL objDal = new GeneralDAL();
        objDal.OpenSQLConnection();
        sqlCmd.Connection = objDal.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT TOP 1 userId FROM Users_ WHERE username = '" + Session["MobileNo"].ToString() + "'";
        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet);
        if (dataSet.Tables[0].Rows.Count > 0)
        {
            CandidateId = dataSet.Tables[0].Rows[0]["userId"].ToString();
        }
        objDal.CloseSQLConnection();
        return CandidateId;
    }

}