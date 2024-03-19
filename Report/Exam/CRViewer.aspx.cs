using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Runtime.Serialization;
using System.IO;
using System.Net;

public partial class Report_Exam_CRViewer : System.Web.UI.Page
{
    ReportDocument crRpt = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["RptType"] != null)
            {
                GeneralDAL generalDAL = new GeneralDAL();
                DataSet ds = new DataSet();

                LoadReport();
                switch (Request.QueryString["RptType"].ToString())
                {
                    default:
                        ds = generalDAL.GetDataSet(GetSqlQuery());
                        RenameDataSetTables(ref ds);
                        break;
                }
                crRpt.SetDataSource(ds);
                SetParameters();

                if (Request.QueryString["Export"] != null)
                {
                    CrystalDecisions.Shared.ExportOptions crExportOptions;
                    DiskFileDestinationOptions crDiskFileDestinationOptions;
                    string Fname;
                    if (Request.QueryString["Export"] != "word")
                        Fname = Server.MapPath("~\\Tmp.xls");
                    else
                        Fname = Server.MapPath("~\\Tmp.Xls");


                    crDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    crDiskFileDestinationOptions.DiskFileName = Fname;
                    crExportOptions = crRpt.ExportOptions;
                    crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
                    crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    if (Request.QueryString["Export"] != "word")
                        crExportOptions.ExportFormatType = ExportFormatType.Excel;
                    else
                        crExportOptions.ExportFormatType = ExportFormatType.WordForWindows;

                    crRpt.Export();
                    crRpt.Close();
                    crRpt.Dispose();
                    Response.Clear();

                    Response.Cache.SetNoStore();
                    Response.AddHeader("Pragma", "no-cache");
                    Response.Expires = -1;
                    string attachment = "";
                    if (Request.QueryString["Export"] != null)
                    {
                        if (Request.QueryString["ExcelName"] == "Target")
                            attachment = "attachment; filename=TargetDelivery.xls";
                        else if (Request.QueryString["ExcelName"] == "Delv/sDis")
                            attachment = "attachment; filename=DeliveryV/SDespatch.xls";
                        else if (Request.QueryString["ExcelName"] == "DisAch")
                            attachment = "attachment; filename=DispatchAchieved.xls";
                        else
                            attachment = "attachment; filename=MsExcel.xls";
                    }
                    else
                        attachment = "attachment; filename=MsExcel.xls";

                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    EnableViewState = false;
                    if (Request.QueryString["Export"] != "word")
                        Response.ContentType = "application/vnd.ms-excel";
                    else
                        Response.ContentType = "application/vnd.ms-word";
                    Response.WriteFile(Fname);
                    Response.End();
                }
                else
                {
                    CrystalDecisions.Shared.ExportOptions crExportOptions;
                    DiskFileDestinationOptions crDiskFileDestinationOptions;
                    string Fname;

                    Fname = Server.MapPath("~\\Tmp.pdf");
                    crDiskFileDestinationOptions = new DiskFileDestinationOptions();
                    crDiskFileDestinationOptions.DiskFileName = Fname;
                    crExportOptions = crRpt.ExportOptions;
                    crExportOptions.DestinationOptions = crDiskFileDestinationOptions;
                    crExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    crExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;

                    crRpt.Export();
                    crRpt.Close();
                    crRpt.Dispose();
                    Response.Clear();

                    Response.Cache.SetNoStore();
                    Response.AddHeader("Pragma", "no-cache");
                    Response.Expires = -1;
                    Response.ContentType = "application/pdf";
                    Response.WriteFile(Fname);

                    if (Request.QueryString["RptType"].ToString() == "InterviewFormdetial")
                    {
                        string pdfFilePath = Server.MapPath("~/Tmp.pdf"); // Use virtual path here
                        WebClient req = new WebClient();
                        HttpResponse response = HttpContext.Current.Response;
                        response.Clear();
                        response.ClearContent();
                        response.ClearHeaders();
                        response.Buffer = true;
                        response.AddHeader("Content-Disposition", "attachment;filename=InterviewForm.PDF");
                        byte[] data = req.DownloadData(pdfFilePath); // Pass virtual path here
                        response.BinaryWrite(data);
                        response.End();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }

    private string GetSqlQuery()
    {
        string sql = "";
        string sql1 = "";
        string sql2 = "";
        string sql3 = "";
        string sql4 = "";
        string sql5 = "";
        string sql6 = "";

        string sqlWhere1 = "";
        string sqlWhere2 = "";
        //string sqlCompany = "";

        //sqlCompany = "SELECT * FROM vwCompany where CompanyId='2ede8b01-ba8e-422a-a484-5a89c34937d8'";


        if (Request.QueryString["RptType"].ToString() == "OfferLatter")
        {
            if (Request.QueryString["RptId"] != null)
            {
                sqlWhere1 = " WHERE JobId='" + Request.QueryString["RptId"].ToString() + "'";
            }
            sql1 = "SELECT Top 1 * FROM vwInterviewForms" + sqlWhere1;
            //Final Sql Query
            sql = sql1 + ";";
        }

        if (Request.QueryString["RptType"].ToString() == "AppointmentLatter")
        {
            if (Request.QueryString["RptId"] != null)
            {
                sqlWhere1 = " WHERE RegistrationId='" + Request.QueryString["RptId"].ToString() + "'";
            }
            sql1 = "SELECT Top 1 * FROM vwInterviewForms" + sqlWhere1;
            //Final Sql Query
            sql = sql1 + ";";
        }

        if (Request.QueryString["RptType"].ToString() == "InterViewForm")
        {
            if (Request.QueryString["RptId"] != null)
            {
                sqlWhere1 = " WHERE RegistrationId='" + Request.QueryString["RptId"].ToString() + "'";
            }
            sql1 = "SELECT Top 1 * FROM vwInterviewFormDetails" + sqlWhere1;
            //Final Sql Query
            sql = sql1 + ";";
        }

        if (Request.QueryString["RptType"].ToString() == "InterviewFormdetial")
        {
            if (Request.QueryString["RptId"] != null)
            {
                sqlWhere1 = " WHERE CandidateId='" + Request.QueryString["RptId"].ToString() + "'";
            }
            sql1 = " select * from View_Candidate_Experience_Details" + sqlWhere1;
            sql2 = " select * from View_Candidate_Family_Details " + sqlWhere1;
            sql3 = " select * from View_Candidate_Educational_Detail" + sqlWhere1;
            sql4 = " select * from View_Candidate_Personal_Detail" + sqlWhere1;
            sql5 = " select * from VwCandidateDocuments" + sqlWhere1;
            sql6 = " select * from VwCompany";
            //Final Sql Query
            sql = sql1 + sql2 + sql3 + sql4 + sql5 + sql6 + ";";
        }
        return sql;
    }

    private void LoadReport()
    {
        GeneralDAL generalDAL = new GeneralDAL();

        if (Request.QueryString["RptType"].ToString() == "OfferLatter")
        {
            crRpt.Load(Server.MapPath("~/Report/Exam/OfferLatter.rpt"));
        }
        if (Request.QueryString["RptType"].ToString() == "InterViewForm")
        {
            crRpt.Load(Server.MapPath("~/Report/Exam/InterViewForm.rpt"));
        }
        if (Request.QueryString["RptType"].ToString() == "AppointmentLatter")
        {
            crRpt.Load(Server.MapPath("~/Report/Exam/AppointmentLatter.rpt"));
        }
        if (Request.QueryString["RptType"].ToString() == "InterviewFormdetial")
        {

            crRpt.Load(Server.MapPath("~/Report/Exam/InterviewFormdetial.rpt"));
            //string pdfFilePath = Server.MapPath("~/Tmp.pdf"); // Use virtual path here
            //WebClient req = new WebClient();
            //HttpResponse response = HttpContext.Current.Response;
            //response.Clear();
            //response.ClearContent();
            //response.ClearHeaders();
            //response.Buffer = true;
            //response.AddHeader("Content-Disposition", "attachment;filename=Filename.PDF");
            //byte[] data = req.DownloadData(pdfFilePath); // Pass virtual path here
            //response.BinaryWrite(data);
            //response.End();

            //filepath = Server.MapPath(filepath);
            //string filepath = "~/"+"Tmp.pdf";
            //filepath = Server.MapPath(filepath);
            //crRpt.ExportToDisk(ExportFormatType.PortableDocFormat, filepath);
            //FileInfo fileinfo = new FileInfo(filepath);
            //Response.AddHeader("Content-Disposition", "inline;filenam=demo.pdf");
            //Response.ContentType = "application/pdf";
            //Response.WriteFile(fileinfo.FullName); 
        }
    }

    private void RenameDataSetTables(ref DataSet ds)
    {

        if (Request.QueryString["RptType"].ToString() == "OfferLatter")
        {
            ds.Tables[0].TableName = "vwInterviewForms";
            // ds.Tables[1].TableName = "vwCompany";
        }
        if (Request.QueryString["RptType"].ToString() == "InterViewForm")
        {
            ds.Tables[0].TableName = "vwInterviewFormDetails";
            // ds.Tables[1].TableName = "vwCompany";
        }
        if (Request.QueryString["RptType"].ToString() == "AppointmentLatter")
        {
            ds.Tables[0].TableName = "vwInterviewForms";
            // ds.Tables[1].TableName = "vwCompany";
        }
        if (Request.QueryString["RptType"].ToString() == "InterviewFormdetial")
        {
            ds.Tables[0].TableName = "View_Candidate_Experience_Details";
            ds.Tables[1].TableName = "View_Candidate_Family_Details";
            ds.Tables[2].TableName = "View_Candidate_Educational_Detail";
            ds.Tables[3].TableName = "View_Candidate_Personal_Detail";
            ds.Tables[4].TableName = "VwCandidateDocuments";
            ds.Tables[5].TableName = "vwCompany";
        }
    }

    private void SetParameters()
    {
        if (Request.QueryString["RptType"].ToString() == "OfferLatter")
        {
            if (Request.QueryString["Date"] != null)
            {
                crRpt.SetParameterValue("Date", Request.QueryString["Date"].ToString().Replace("-", " "));
            }
            else
            {
                crRpt.SetParameterValue("Date", DateTime.Today.ToString("dd-MMM-yyyy"));
            }

            if (Request.QueryString["JobProfile"] != null)
            {
                crRpt.SetParameterValue("JobProfile", Request.QueryString["JobProfile"].ToString().Replace("-", " "));
            }
            else
            {
                crRpt.SetParameterValue("JobProfile", "");
            }
        }
        if (Request.QueryString["RptType"].ToString() == "AppointmentLatter")
        {
            if (Request.QueryString["Date"] != null)
            {
                crRpt.SetParameterValue("Date", Request.QueryString["Date"].ToString().Replace("-", " "));
            }
            else
            {
                crRpt.SetParameterValue("Date", DateTime.Today.ToString("dd-MMM-yyyy"));
            }


        }
    }

    private void ShowErrors(string key, string value)
    {
        pnlErr.Visible = true;
        if (value.IndexOf("Division by zero") >= 0)
            value = "No Record Found";

        blErrs.Items.Add(new ListItem(value));
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();
    }
}
