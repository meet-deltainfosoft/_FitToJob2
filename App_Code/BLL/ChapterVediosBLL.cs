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
/// Summary description for ChapterVediosBLL
/// </summary>
public class ChapterVediosBLL
{
    public string _StandardTextListId;
    public string _SubId;
    public string _ChapterId;
    public string _PeriodNo;
    public bool _AllRecord;
    public bool _isDisabled;

    private ChapterVediosDAL _ChapterVediosDAL;

    public ChapterVediosBLL()
    {
        _ChapterVediosDAL = new ChapterVediosDAL();
    }
    ~ChapterVediosBLL()
    {
        _ChapterVediosDAL = null;
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
    public bool IsDisabled
    {
        set
        {
            _isDisabled = value;
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
            return _ChapterVediosDAL.LoadSubjects(_StandardTextListId);
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
            return _ChapterVediosDAL.LoadChepter(_SubId);
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
            return _ChapterVediosDAL.LoadPeriod(_ChapterId, _SubId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable ChapterVedioList()
    {
        return _ChapterVediosDAL.ChapterVedioList(_StandardTextListId, _SubId, _ChapterId, _PeriodNo, _AllRecord, _isDisabled);
    }
}