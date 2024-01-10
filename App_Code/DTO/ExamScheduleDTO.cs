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

public struct ExamScheduleDTO
{
    public string ExamScheduleId;
    public string StandardTextListId;
    public string DivisionTextListId;
    public string SubId;
    public string TestId;
    public string SkipMobile;
    public int? TotalQuestions;
    public DateTime? ExamDate;
    public DateTime? ExamFromTime;
    public DateTime? ExamToTime;
    public int? TotalMins;
    public decimal? PerQueMins;

    public bool IsNew;

    public DataTable dtRestrations;

    public bool NegativeMarks;
    public bool PerQuestionTime;
    public bool AllowReview;
    public bool ShowResult;
    public int? MinsforResultShow;
    public string PatternId;

    public bool SendNotification;
    public bool IsDefaultTest;
}
