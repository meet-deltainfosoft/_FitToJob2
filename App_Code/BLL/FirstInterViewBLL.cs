using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for FirstInterViewBLL
/// </summary>
public class FirstInterViewBLL
{
	public string _Name;
    public string _MobileNo;
    public string _DepartmentId;
    public string _DivisionId;
    public string _DesignationId;
    public bool _AllRecord;
    public string _City;
    public string _Status;

    private FirstInterViewDAL _firstInterViewDAL;

    public FirstInterViewBLL()
    {
        _firstInterViewDAL = new FirstInterViewDAL();
    }
    ~FirstInterViewBLL()
    {
        _firstInterViewDAL = null;
    }

    public string Name
    {
        set
        {
            _Name = value;
        }
    }
    public string DepartmentId
    {
        set
        {
            _DepartmentId = value;
        }
    }
    public string DivisionId
    {
        set
        {
            _DivisionId = value;
        }
    }
    public string DesignationId
    {
        set
        {
            _DesignationId = value;
        }
    }
    public string MobileNo
    {
        set
        {
            _MobileNo = value;
        }
    }
    
    public string City
    {
        set
        {
            _City = value;
        }
    }
    public string Status
    {
        set
        {
            _Status = value;
        }
    }
    public bool AllRecord
    {
        set
        {
            _AllRecord = value;
        }
    }
    public DataTable FirstInterview()
    {
        return _firstInterViewDAL.FirstInterview(_Name, _MobileNo, _DepartmentId, _DivisionId, _DesignationId, _AllRecord, _City, _Status);
    }

    public DataTable LoadDepartment()
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

    public DataTable LoadDesignation()
    {
        try
        {
            return _firstInterViewDAL.Designation();
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

    public void Update(ArrayList alAccount)
    {
        try
        {
            _firstInterViewDAL.Update(alAccount);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable LoadSubjects(string StandardId)
    {
        try
        {
            return _firstInterViewDAL.LoadSubjects(StandardId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }



}