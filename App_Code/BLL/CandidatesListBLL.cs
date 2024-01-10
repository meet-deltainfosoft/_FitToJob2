using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for CandidatesListBLL
/// </summary>
public class CandidatesListBLL
{
	public string _Name;
    public string _MobileNo;
    public string _DepartmentId;
    public string _DivisionId;
    public string _DesignationId;
    public bool _AllRecord;
    public string _City;
    public string _Taluka;
    public string _Status;

    private CandidatesListDAL _CandidatesListDAL;

    public CandidatesListBLL()
    {
        _CandidatesListDAL = new CandidatesListDAL();
    }
    ~CandidatesListBLL()
    {
        _CandidatesListDAL = null;
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
    public string Taluka
    {
        set
        {
            _Taluka = value;
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
    public DataTable CandidatesList()
    {
        return _CandidatesListDAL.CandidatesList(_Name, _MobileNo, _DepartmentId, _DivisionId, _DesignationId, _AllRecord, _City,_Taluka,_Status);
    }
    public DataTable CandidatesLists()
    {
        return _CandidatesListDAL.CandidatesLists(_Name, _MobileNo, _DepartmentId, _DivisionId, _DesignationId, _AllRecord, _City, _Taluka, _Status);
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
            return _CandidatesListDAL.Designation();
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
            _CandidatesListDAL.Update(alAccount);
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
            return _CandidatesListDAL.LoadSubjects(StandardId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


}