using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Default2 : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["SubjectId"] != "0")
                {
                    GetSubjectById(Request.QueryString["SubjectId"].ToString());
                }
            }
        }
        catch (Exception)
        {

            throw;
        }

    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Master_Subjects";
            if (Request.QueryString["SubjectId"] != "0")
            {
                sqlCmd.Parameters.AddWithValue("@Action", "UpdateSubject");
                sqlCmd.Parameters.AddWithValue("@subjectId", Request.QueryString["SubjectId"].ToString());
                sqlCmd.Parameters.AddWithValue("@SubjectName", txtSubject.Text);
                sqlCmd.Parameters.AddWithValue("@UserId", MySession.UserUnique);
            }
            else {
                sqlCmd.Parameters.AddWithValue("@Action", "AddSubject");
                sqlCmd.Parameters.AddWithValue("@SubjectName", txtSubject.Text);
                sqlCmd.Parameters.AddWithValue("@UserId", MySession.UserUnique);
            }


            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                lblMessage.Visible = true;
                lblMessage.CssClass = "success-message";
                lblMessage.Text = dataSet.Tables[0].Rows[0]["Message"].ToString();
            }
            objDal.CloseSQLConnection();
        }
        catch (Exception)
        {
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e) { }

    private void GetSubjectById(string SubjectId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Master_Subjects";
            sqlCmd.Parameters.AddWithValue("@Action", "GetSubjectById");
            sqlCmd.Parameters.AddWithValue("@subjectId", SubjectId);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                txtSubject.Text = dataSet.Tables[0].Rows[0]["SubjectName"].ToString();
            }
            objDal.CloseSQLConnection();
        }
        catch (Exception)
        {

            throw;
        }
    }
}