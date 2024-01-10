using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for InterviewLnDTO
/// </summary>
public class InterviewLnDTO
{

    public string InterviewLnId;
    public string InterviewId;
    public Int32? LnNo;
    public string QueTextListId;
    public string Text;
    public Int32? ActualMarks;
    public Int32? ObtainedMarks;

    public bool? IsNew;
    public bool? IsDirty;
    public bool? IsDeleted;
}