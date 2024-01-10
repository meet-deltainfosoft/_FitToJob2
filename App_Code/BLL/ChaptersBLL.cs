﻿using System;
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
/// Summary description for ChaptersBLL
/// </summary>
public class ChaptersBLL
{
    public string _StandardTextListId;
    public string _SubId;
    public string _PeriodNo;
    public bool _AllRecord;

    private ChaptersDAL _ChaptersDAL;

    public ChaptersBLL()
    {
        _ChaptersDAL = new ChaptersDAL();
    }
    ~ChaptersBLL()
    {
        _ChaptersDAL = null;
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
            return _ChaptersDAL.LoadSubjects(_StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable ChapterList()
    {
        return _ChaptersDAL.ChapterList(_StandardTextListId, _SubId, _PeriodNo, _AllRecord);
    }

}