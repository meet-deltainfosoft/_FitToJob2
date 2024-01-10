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
using System.Collections.Generic;

/// <summary>
/// Summary description for ChapterPDFsBLL
/// </summary>
public class ChapterPDFsBLL
{
    public string _StandardTextListId;
    public string _SubId;
    public string _ChapterId;
    public string _PeriodNo;
    public bool _AllRecord;

    private ChapterPDFsDAL _ChapterPDFsDAL;

    public ChapterPDFsBLL()
    {
        _ChapterPDFsDAL = new ChapterPDFsDAL();
    }
    ~ChapterPDFsBLL()
    {
        _ChapterPDFsDAL = null;
    }
    public string StandardId
    {
        set
        {
            _StandardTextListId = value;
        }
    }
    public string SubId
    {
        set
        {
            _SubId = value;
        }
    }
    public string ChapterId
    {
        set
        {
            _ChapterId = value;
        }
    }
    public string PeriodNo
    {
        set
        {
            _PeriodNo = value;
        }
    }
    public bool AllRecord
    {
        set
        {
            _AllRecord = value;
        }
    }
    public DataTable LoadStandard()
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
    public DataTable LoadSubjects()
    {
        try
        {
            return _ChapterPDFsDAL.LoadSubjects(_StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable LoadChepter()
    {
        try
        {
            return _ChapterPDFsDAL.LoadChepter(_SubId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable LoadPeriod()
    {
        try
        {
            return _ChapterPDFsDAL.LoadPeriod(_ChapterId, _SubId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable ChapterPDFList()
    {
        return _ChapterPDFsDAL.ChapterList(_StandardTextListId, _SubId, _ChapterId, _PeriodNo, _AllRecord);
    }
}