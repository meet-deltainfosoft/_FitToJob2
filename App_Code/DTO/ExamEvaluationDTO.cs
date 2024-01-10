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

public struct ExamEvaluationDTO
{
    public string ExamEvaluationId;
    public string QueId;
    public string UserId;
    public string ExamId;
    public Int16? ImageNo;
    public string ImagePath;
    public string TotalObtMark;

    public DataTable dtExamEvaluationLns;

    public string SubMarks;

    public string Remarks;
    public bool IsNew;


    public decimal? TotalMarks;
    public decimal? ObtainedMarks;

    public string RotatedImagePath;
}
