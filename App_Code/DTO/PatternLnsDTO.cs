using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class PatternLnsDTO
{
    public string PatternLnId;
    public string PatternId;
    public int? LnNo;
    public string SubId;
    public string Subject;
    public int? NoOfMCQ;
    public decimal? MCQRightMarks;
    public decimal? MCQWrongMarks;
    public decimal? MCQSkippedMarks;
    public int? NoOfNonMCQ;
    public decimal? NonMCQRightMarks;
    public decimal? NonMCQWrongMarks;
    public decimal? NonMCQSkippedMarks;
    public bool IsNew;
    public bool IsDirty;
    public bool IsDeleted;

}