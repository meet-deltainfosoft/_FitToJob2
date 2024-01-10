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

public class QuesBLL
{
    public string _TestId;
    public string _StandardId;
    public string _SubId;
    public string _Question;
    public bool _AllRecord;

    private QuesDAL _QuesDAL;

    public QuesBLL()
    {
        _QuesDAL = new QuesDAL();
    }
    ~QuesBLL()
    {
        _QuesDAL = null;
    }
    public string Question
    {
        set
        {
            _Question = value;
        }
    }
    public string TestId
    {
        set
        {
            _TestId = value;
        }
    }
    public string StandardId
    {
        set
        {
            _StandardId = value;
        }
    }
    public string SubId
    {
        set
        {
            _SubId = value;
        }
    }

    public bool AllRecord
    {
        set
        {
            _AllRecord = value;
        }
    }

    public DataTable Filter()
    {
        return _QuesDAL.Question(_Question, _StandardId, _SubId, _TestId, _AllRecord);
    }
}

