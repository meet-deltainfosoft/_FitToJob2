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

public class BatchsBLL
{
    public string _StandardTextListId;
    public bool _AllRecord;

    private BatchsDAL _BatchsDAL;

    public BatchsBLL()
    {
        _BatchsDAL = new BatchsDAL();
    }
    ~BatchsBLL()
    {
        _BatchsDAL = null;
    }
    public string StandardId
    {
        set
        {
            _StandardTextListId = value;
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

    public DataTable BatchList()
    {
        return _BatchsDAL.BatchList(_StandardTextListId, _AllRecord);
    }

}