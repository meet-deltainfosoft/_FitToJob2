using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Guest_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDocumentsDropDown();
        }
    }

    public void GetDocumentsDropDown()
    {
        SqlCommand sqlCmd = new SqlCommand();
        GeneralDAL objDal = new GeneralDAL();
        objDal.OpenSQLConnection();
        sqlCmd.Connection = objDal.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.StoredProcedure;
        sqlCmd.CommandText = "FitToJob_Master_Registration";
        sqlCmd.Parameters.AddWithValue("@Action", "GetDocumentsDropDown");
        SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
        DataSet dataSet = new DataSet();
        dataAdapter.Fill(dataSet);
        if (dataSet.Tables[0].Rows.Count > 0)
        {
            ddlDocuments.DataSource = dataSet.Tables[0];
            ddlDocuments.DataTextField = "Documents";
            ddlDocuments.DataValueField = "DocumentId";
            ddlDocuments.DataBind();

        }
    }



}