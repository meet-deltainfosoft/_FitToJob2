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

public class RegistrationsBLL
{
    public string _RegistrationName;
    public string _MobileNo;
    public string _ExtraMobileNo;
    public string _StandardId;
    public string _DivisionTextListId;
    public bool? _IsDeActive;
    public bool _AllRecord;
    public string _SchoolName;
    public string _City;

    private RegistrationsDAL _RegistrationsDAL;

    public RegistrationsBLL()
    {
        _RegistrationsDAL = new RegistrationsDAL();
    }
    ~RegistrationsBLL()
    {
        _RegistrationsDAL = null;
    }

    public string RegistrationName
    {
        set
        {
            _RegistrationName = value;
        }
    }
    public string StandardId
    {
        set
        {
            _StandardId = value;
        }
    }
    public string DivisionTextListId
    {
        set
        {
            _DivisionTextListId = value;
        }
    }
    public string MobileNo
    {
        set
        {
            _MobileNo = value;
        }
    }
    public string ExtraMobileNo
    {
        set
        {
            _ExtraMobileNo = value;
        }
    }
    public string SchoolName
    {
        set
        {
            _SchoolName = value;
        }
    }
    public string City
    {
        set
        {
            _City = value;
        }
    }
    public bool? IsDeActive
    {
        set
        {
            _IsDeActive = value;
        }
    }
    public bool AllRecord
    {
        set
        {
            _AllRecord = value;
        }
    }
    public DataTable Registrations()
    {
        return _RegistrationsDAL.Registrations(_RegistrationName, _MobileNo, _ExtraMobileNo, _StandardId, _DivisionTextListId, _IsDeActive, _AllRecord, _SchoolName, _City);
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

    public DataTable LoadDivision()
    {
        try
        {
            GeneralDAL _GeneralDAL = new GeneralDAL();
            return _GeneralDAL.TextList("Division");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public int CountStudent()
    {
        try
        {
            return _RegistrationsDAL.CountStudent(_RegistrationName, _MobileNo, _StandardId, _DivisionTextListId, _IsDeActive, _AllRecord, _SchoolName, _City);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable Filter()
    {
        return _RegistrationsDAL.Students(_StandardId, _RegistrationName, _SchoolName, _City, _AllRecord);
    }
}