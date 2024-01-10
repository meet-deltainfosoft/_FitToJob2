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

public struct ExamResultPublishDTO
{
    public string ExamResultPublishId;
    public string StandardTextListId;
    public string SubId;
    public string TestId;
    public string ExamScheduleId;
    public string AnsKeyFilePath;
    public string AnsKeyFilePathShow;
    public bool IsResultPublished;

    public bool IsNew;

    public bool IsChangeFile; 
}
