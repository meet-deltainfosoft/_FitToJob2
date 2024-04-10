using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Web.Configuration;
using System.Configuration;

public partial class Guest_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //divData.Style.Add("display", "none");
            if (string.IsNullOrEmpty((string)Session["MobileNo"]) || string.IsNullOrEmpty((string)Session["Language"]))
            {
                Response.Redirect("~/Guests/SelectLanguage.aspx");
            }
            else
            {
                GetDocumentsDropDown();
            }

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

    protected void btnSearch_click(object sender, EventArgs e)
    {
        if (ddlDocuments.SelectedItem.Text == "Select Documents")
        {
            lblErrorMessage.Text = "Please Choose Document For Upload";
        }
        else
        {
            lblErrorMessage.Text = "";
            lblDropDownValue.Text = ddlDocuments.SelectedItem.Text;
        }
    }


    protected void btnSubmit_click(object sender, EventArgs e)
    {
        try
        {
            string DocumentPath = "";
            if (ddlDocuments.SelectedItem.Text == "Select Documents")
            {
                lblErrorMessage.Text = "Please Choose Document For Upload";
            }
            else
            {
                if (Filupload.HasFile)
                {
                    string uniqueFileName = Guid.NewGuid().ToString();
                    string fileExtension = Path.GetExtension(Filupload.FileName);
                    string newFileName = uniqueFileName + fileExtension;
                    string uploadFolder = ConfigurationManager.AppSettings["UploadDirectory"];
                    string fullPath = Path.Combine(uploadFolder, newFileName);
                    DocumentPath = newFileName;
                    Filupload.SaveAs(fullPath);

                    SqlCommand sqlCmd = new SqlCommand();
                    GeneralDAL objDal = new GeneralDAL();
                    objDal.OpenSQLConnection();
                    sqlCmd.Connection = objDal.ActiveSQLConnection();
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandText = "FitToJob_Master_Registration";
                    sqlCmd.Parameters.AddWithValue("@Action", "UploadDocuments");
                    sqlCmd.Parameters.AddWithValue("@MobileNo", Session["MobileNo"]);
                    sqlCmd.Parameters.AddWithValue("@DocumentPath", DocumentPath);
                    sqlCmd.Parameters.AddWithValue("@DocumentId", ddlDocuments.SelectedValue);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    if (dataSet.Tables[0].Rows.Count > 0)
                    {
                        if (dataSet.Tables[0].Rows[0]["IsSuccessful"].ToString() == "1")
                        {
                            lblErrorMessage.Style.Add("color", "Green");
                            lblErrorMessage.Text = dataSet.Tables[0].Rows[0]["Message"].ToString();
                            //Response.Redirect("~/Guest/ThankYouPage.aspx");
                        }
                    }
                    objDal.CloseSQLConnection();
                    GetAllUploadedDocuments();
                }
                else
                {
                    lblErrorMessage.Text = "Please Upload Doduments For Upload";
                }
            }


        }
        catch (Exception)
        {

        }
    }

    private void GetAllUploadedDocuments()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Master_Registration";
            sqlCmd.Parameters.AddWithValue("@Action", "GetAllUploadedDocuments");
            sqlCmd.Parameters.AddWithValue("@MobileNo", Session["MobileNo"]);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {

                gvEducationDetails.DataSource = dataSet.Tables[0];
                gvEducationDetails.DataBind();
            }
            objDal.CloseSQLConnection();
        }
        catch (Exception)
        {
        }

    }



}