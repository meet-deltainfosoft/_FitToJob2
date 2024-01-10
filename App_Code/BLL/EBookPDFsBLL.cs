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
/// Summary description for EBookPDFsBLL
/// </summary>
public class EBookPDFsBLL
{
    public string _StandardTextListId;
    public string _SubId;
    public string _EBookPDFId;
    public string _PeriodNo;
    public bool _AllRecord;

    private EBookPDFsDAL _EBookPDFsDAL;

    public EBookPDFsBLL()
    {
        _EBookPDFsDAL = new EBookPDFsDAL();
    }
    ~EBookPDFsBLL()
    {
        _EBookPDFsDAL = null;
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
            _EBookPDFId = value;
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
            return _EBookPDFsDAL.LoadSubjects(_StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable LoadEBookPDF()
    {
        try
        {
            return _EBookPDFsDAL.LoadEBookPDF(_SubId);
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
            return _EBookPDFsDAL.LoadPeriod(_EBookPDFId, _SubId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable EBookPDFList()
    {
        return _EBookPDFsDAL.EBookPDFList(_StandardTextListId, _SubId, _EBookPDFId, _PeriodNo, _AllRecord);
    }
}