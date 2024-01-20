using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Guest_OfferApprove : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty((string)Session["MobileNo"]) || string.IsNullOrEmpty((string)Session["JobId"]))
            {

                Response.Redirect("~/Guest/SignIn.aspx");
            }
            else
            {
                GetOfferApprove(Session["JobId"].ToString());
            }
        }
    }

    public void GetOfferApprove(string JobId)
    {
        try
        {
            GeneralDAL objDal = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Android_Application";
            sqlCmd.Parameters.AddWithValue("@Action", "GetOfferApprove");
            sqlCmd.Parameters.AddWithValue("@JobId", JobId);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                JoiningDate.InnerText = dataSet.Tables[0].Rows[0]["JoiningDate"].ToString();
                ReportingTime.InnerText = dataSet.Tables[0].Rows[0]["ReportingTime"].ToString();
                DaysRemaining.InnerText = dataSet.Tables[0].Rows[0]["DaysRemaning"].ToString();
                JoiningLocation.InnerText = dataSet.Tables[0].Rows[0]["DayLocation"].ToString();
                WorkLocation.InnerText = dataSet.Tables[0].Rows[0]["WorkLocation"].ToString();
                hrManager.InnerText = dataSet.Tables[0].Rows[0]["HRName"].ToString();
                hrEmailId.InnerText = dataSet.Tables[0].Rows[0]["HrEmailId"].ToString();
            }
            objDal.CloseSQLConnection();
        }
        catch (Exception ex)
        {
        }
    }

    protected void btnViewOffer_click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("../Report/Exam/CRViewer.aspx?RptType=OfferLatter&RptId=" + Session["JobId"].ToString());
        }
        catch (Exception)
        {
        }
    }
}