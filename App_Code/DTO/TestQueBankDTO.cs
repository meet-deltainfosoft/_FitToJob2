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
using System.Collections;

public struct TestQueBankDTO
{
    public string TestId;
    public string Name;
    public string StandardId;
    public string PatternId;
    public string SubId;
    public string ChapterId;
    public string PeriodNo;
    public int? Easy;
    public int? Medium;
    public int? Hard;
    public int? TotalQue;
    public bool IsNew;
    public DataTable dtDetaiils;
    public ArrayList alTest;
    public string LevelOfQue;
    public string Type;
    
}
