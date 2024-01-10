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

public class HomeWorksBLL
{
    public string _ChapterId;
    public string _StandardId;
    public string _SubId;
    public string _Question;
    public bool _AllRecord;

    public string _studentName;
    public string _mobileNumber;

    private HomeWorksDAL _homeWorksDAL;

    public HomeWorksBLL()
    {
        _homeWorksDAL = new HomeWorksDAL();
    }
    ~HomeWorksBLL()
    {
        _homeWorksDAL = null;
    }
    public string Question
    {
        set
        {
            _Question = value;
        }
    }
    public string ChapterId
    {
        set
        {
            _ChapterId = value;
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
    public string StudentName
    {
        set
        {
            _studentName = value;
        }
    }
    public string MobileNumber
    {
        set
        {
            _mobileNumber = value;
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
        return _homeWorksDAL.HomeWorks(_Question, _StandardId, _SubId, _ChapterId, _AllRecord);
    }

    public DataTable FilterForStudentHomework()
    {
        return _homeWorksDAL.FilterForStudentHomework(_Question, _StandardId, _SubId, _ChapterId, _studentName, _mobileNumber);
    }
}

