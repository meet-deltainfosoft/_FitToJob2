using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public struct RegistrationDTO
{
    public string RegistrationId;
    public string StandardId;
    public string DivisionTextListId;
    public string RegistrationName;
    public string MobileNo;
    public string ExtraMobileNo;
    public bool? IsDeActive;
    public bool IsNew;
    public string ExamNo;
    public string SchoolName;
    public string City;
    public string EmailId;
}