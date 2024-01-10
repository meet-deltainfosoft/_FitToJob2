using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Net.Mail;
using System.Configuration;
using System.Text;
//using System.Windows.Forms;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for GenerateCallBLL
/// </summary>
public class GenerateCallBLL
{
    public string _Name;
    public string _MobileNo;
    public string _DepartmentId;
    public string _DivisionId;
    public string _DesignationId;
    public bool _AllRecord;
    public string _City;

    private GenerateCallDAL _generateCallDAL;

    public GenerateCallBLL()
    {
        _generateCallDAL = new GenerateCallDAL();
    }
    ~GenerateCallBLL()
    {
        _generateCallDAL = null;
    }

    public string Name
    {
        set
        {
            _Name = value;
        }
    }
    public string DepartmentId
    {
        set
        {
            _DepartmentId = value;
        }
    }
    public string DivisionId
    {
        set
        {
            _DivisionId = value;
        }
    }
    public string DesignationId
    {
        set
        {
            _DesignationId = value;
        }
    }
    public string MobileNo
    {
        set
        {
            _MobileNo = value;
        }
    }

    public string City
    {
        set
        {
            _City = value;
        }
    }

    public bool AllRecord
    {
        set
        {
            _AllRecord = value;
        }
    }
    public DataTable GenerateCall()
    {
        return _generateCallDAL.GenerateCall(_Name, _MobileNo, _DepartmentId, _DivisionId, _DesignationId, _AllRecord, _City);
    }

    public DataTable LoadDepartment()
    {
        try
        {
            GeneralDAL _GeneralDAL = new GeneralDAL();
            return _GeneralDAL.TextList("Standard");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadDesignation()
    {
        try
        {
            return _generateCallDAL.Designation();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadDivision()
    {
        try
        {
            GeneralDAL _GeneralDAL = new GeneralDAL();
            return _GeneralDAL.TextList("Division");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Update(string RegistrationId)
    {
        try
        {
            _generateCallDAL.Update(RegistrationId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void SendEmail(string RegistrationId)
    {
        Literal disp = new Literal();
        MailMessage mm = new MailMessage();
        try
        {
            DataTable dtExport = new DataTable();
            dtExport = _generateCallDAL.GetMail(RegistrationId);

            string To = "";



            To += dtExport.Rows[0]["EmailId"].ToString().Trim();
                

            GeneralDAL generalDAL = new GeneralDAL();
            string copyString = "";
            StringBuilder strbu = new StringBuilder();

            strbu.Append("<H3>Hello " + dtExport.Rows[0]["Name"].ToString().ToUpper() + "</H3></br>");
            strbu.Append("<H3><u><b></b></u></H3></br>");

            strbu.Append("<tr>");
            strbu.Append("<th>You're selected as a " + dtExport.Rows[0]["JobProfile"].ToString().ToUpper() + " at "+ dtExport.Rows[0]["CompanyName"].ToString().ToUpper() +".</th>");
            strbu.Append("</tr>");

            strbu.Append("<tr>");
            strbu.Append("<th>Kindly find this attached Call letter with reference, and come for HR round on " + System.DateTime.Now.ToString("dd-MMM-yyyy") + ".</th>");
            strbu.Append("</tr>");

            strbu.Append("</table>");
            strbu.Append("<H3>Thanks & Regards,</H3>");
            strbu.Append("<H3 style='color:Teal'>" + dtExport.Rows[0]["CompanyName"].ToString().ToUpper() + "</H3>");
            strbu.Append("<span style='text-align:left;color:#3300CC;font-size:8px;font-family:Tahoma,Arial;'>*This is system generated mail. Please do not reply here.</span>");
            disp.Text = strbu.ToString();

            disp.Text += "</div>";
            copyString += strbu.ToString();
            EmailClass.SendEmail("smtp.office365.com", 25, ConfigurationManager.AppSettings["FromEmailId"].ToString(), ConfigurationManager.AppSettings["FromEmailIdPsw"].ToString(), To, "", "", "New Created.", disp.Text, System.Web.Mail.MailFormat.Html, "");
           // MailAddress Sender = new MailAddress(ConfigurationManager.AppSettings["smtpUser"]);

            //mm.From = new MailAddress("'info@deltainfosoft.com'");

            
            //mm.To.Add(new MailAddress("'" + dtExport.Rows[0]["EmailId"].ToString().ToUpper() + "'"));

            //mm.Subject = "Your Offer Latter On ( " + System.DateTime.Now.ToString("dd-MMM-yyyy") + " )";
            //mm.Body = copyString;

            //mm.IsBodyHtml = true;

            //SmtpClient smtp = new SmtpClient();
           
            //smtp.Send(mm);

            //if (mm.Attachments.Count > 0)
            //{
            //    mm.Attachments.Dispose();
            //}

            //mm.To.Clear();
            //mm.CC.Clear();
        }
        catch (Exception ex)
        {
            if (mm.Attachments.Count > 0)
            {
                mm.Attachments.Dispose();
            }
            throw new Exception(ex.Message);
        }
    }
    public DataTable LoadSubjects(string StandardId)
    {
        try
        {
            return _generateCallDAL.LoadSubjects(StandardId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}