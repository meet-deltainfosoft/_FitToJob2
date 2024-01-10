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
/// Summary description for PatternsBLL
/// </summary>
public class PatternsBLL
{
    public string _StandardTextListId;
    public string _SubId;
    public string _PatternName;
    public bool _AllRecord;

    private PatternsDAL _patternsDAL;
    public PatternsBLL()
    {
        _patternsDAL = new PatternsDAL();
    }
    ~PatternsBLL()
    {
        _patternsDAL = null;
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
    public string PatternName
    {
        set
        {
            _PatternName = value;
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
            return _patternsDAL.LoadSubjects(_StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable GetData()
    {
        try
        {
            return _patternsDAL.GetData(_StandardTextListId, _SubId, _PatternName, _AllRecord);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}