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
/// <summary>
/// Summary description for ChapterDTO
/// </summary>
public class ChapterDTO
{
    public string ChapterPDFId;
    public string ChapterId;
    public string StandardTextListId;
    public string SubId;
    public string SrNo;
    public string ChapterName;
    public string Remarks;

    public decimal? PeriodNo;
   
    public bool IsNew;

}