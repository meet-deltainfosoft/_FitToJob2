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
        if (!IsPostBack)
        {
            GetAllSubject();
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            //SqlCommand sqlCmd = new SqlCommand();
            //GeneralDAL objDal = new GeneralDAL();
            //objDal.OpenSQLConnection();
            //sqlCmd.Connection = objDal.ActiveSQLConnection();
            //sqlCmd.CommandType = CommandType.StoredProcedure;
            //sqlCmd.CommandText = "FitToJob_Master_Subjects";
            //sqlCmd.Parameters.AddWithValue("@Action", "AddSubject");
            //sqlCmd.Parameters.AddWithValue("@SubjectName", txtSubject.Text);
            //sqlCmd.Parameters.AddWithValue("@UserId", MySession.UserUnique);
            //SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            //DataSet dataSet = new DataSet();
            //dataAdapter.Fill(dataSet);
            //if (dataSet.Tables[0].Rows.Count > 0)
            //{
            //    lblMessage.Visible = true;
            //    lblMessage.CssClass = "success-message";
            //    lblMessage.Text = dataSet.Tables[0].Rows[0]["Message"].ToString();
            //}
            //objDal.CloseSQLConnection();
        }
        catch (Exception)
        {
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e) { }

    private void GetAllSubject()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Master_Subjects";
            sqlCmd.Parameters.AddWithValue("@Action", "GetAllSubject");
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                gvSubjectEdit.DataSource = dataSet.Tables[0];
                gvSubjectEdit.DataBind();
            }
            objDal.CloseSQLConnection();


        }
        catch (Exception)
        {
        }
    }

    protected void gvSubjectEdit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string subjectId = e.CommandArgument.ToString();
            Response.Redirect("Subject.aspx?SubjectId=" + subjectId);

            // Extract the command argument
            //Guid index = e.CommandArgument.ToString();

            // Retrieve data from the grid view
            //GridViewRow row = gvSubjectEdit.Rows[index];
            //string srNo = row.Cells[0].Text; // Access data in the first cell (SrNo)

            //// You can now perform any action you want based on the row clicked
            //// For example, redirect to another page:
            //Response.Redirect("YourPage.aspx?SrNo=" + srNo);
        }
    }

    protected void addButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Subject.aspx?SubjectId=" + "0");
    }


}