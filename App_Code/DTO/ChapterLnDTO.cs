using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ChapterLnDTO
/// </summary>
public class ChapterLnDTO
{
    public string ChapterLnId;
    public string ChapterId;
    public Int32? LnNo;
    public string StandardTextListId;
    public string ChapterTextListId;
    public string SubId;

    public string StandardName;
    public string ChapterName;
    public string SubjectName;

    public decimal? PeriodNo;

    public bool IsNew;
    public bool IsDirty;
    public bool IsDeleted;

    public string ChapterVideoId;
    public string ChapterVideoName;

    public string ChapterVideoHeaderId;
}