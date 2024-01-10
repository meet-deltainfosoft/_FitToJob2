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

public class SubsBLL
{
    public string _Name;
    public string _StandardTextListId;
    public bool _AllRecord;

    private SubsDAL _SubsDAL;

    public SubsBLL()
    {
        _SubsDAL = new SubsDAL();
    }
    ~SubsBLL()
    {
        _SubsDAL = null;
    }

    public string Name
    {
        set
        {
            _Name = value;
        }
    }
    public string StandardTextListId
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
    public DataTable Subs()
    {
        return _SubsDAL.Subs(_Name, _StandardTextListId, _AllRecord);
    }
    public DataTable LoadStandard()
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            return _generalDAL.TextList("Standard");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}