using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for JobProfileBLL
/// </summary>
public class JobProfileBLL
{
    #region Declarations
    private JobProfileDAl _jobProfileDAl;
    private GeneralDAL _generalDAL;
    private JobProfileDTO _jobProfileDTO;
    #endregion

    #region Constructor
    public JobProfileBLL()
    {
        _jobProfileDAl = new JobProfileDAl();
        _generalDAL = new GeneralDAL();
        _jobProfileDTO = new JobProfileDTO();
        _jobProfileDTO.IsNew = true;
        _jobProfileDTO.JobOfferingId = _generalDAL.GetNEWID();
    }
    ~JobProfileBLL()
    {
        _jobProfileDAl = null;
        _generalDAL = null;
        _jobProfileDTO = null;
    }
    public JobProfileBLL(string JobOfferingId)
        : this()
    {
        _jobProfileDTO.IsNew = false;
        Load(JobOfferingId);
    }
    #endregion

    #region Load
    public void Load(string JobOfferingId)
    {
        try
        {
            _jobProfileDTO = _jobProfileDAl.Select(JobOfferingId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    public string JobOfferingId
    {
        set
        {
            _jobProfileDTO.JobOfferingId = value;
        }
        get
        {
            return _jobProfileDTO.JobOfferingId;
        }
    }
    public string DepartmentId
    {
        set
        {
            _jobProfileDTO.DepartmentId = value;
        }
        get
        {
            return _jobProfileDTO.DepartmentId;
        }
    }
    public string DivisionId
    {
        set
        {
            _jobProfileDTO.DivisionId = value;
        }
        get
        {
            return _jobProfileDTO.DivisionId;
        }
    }
    public string DesignationId
    {
        set
        {
            _jobProfileDTO.DesignationId = value;
        }
        get
        {
            return _jobProfileDTO.DesignationId;
        }
    }
    public string StaffCategoryTextListId
    {
        set
        {
            _jobProfileDTO.StaffCategoryTextListId = value;
        }
        get
        {
            return _jobProfileDTO.StaffCategoryTextListId;
        }
    }

    public string NoOfSeats
    {
        set
        {
            _jobProfileDTO.NoOfSeats = value;
        }
        get
        {
            return _jobProfileDTO.NoOfSeats;
        }
    }

    public DateTime? ValidFrom
    {
        get { return _jobProfileDTO.ValidFrom; }
        set { _jobProfileDTO.ValidFrom = value; }
    }

    public DateTime? ValidTo
    {
        get { return _jobProfileDTO.ValidTo; }
        set { _jobProfileDTO.ValidTo = value; }
    }

    public bool IsNew
    {
        get
        {
            return _jobProfileDTO.IsNew;
        }
    }

    public SortedList Validate()
    {
        try
        {
            SortedList sl = new SortedList();
            if (_jobProfileDTO.DepartmentId == null)
            {
                sl.Add("DepartmentId", "Department cannot be blank.");
            }

            //if (_jobProfileDTO.DivisionId == null)
            //{
            //    sl.Add("DivisionId", "Division cannot be blank.");
            //}
            if (_jobProfileDTO.DesignationId == null)
            {
                sl.Add("DesignationId", "Designation cannot be blank.");
            }
            if (_jobProfileDTO.StaffCategoryTextListId == null)
            {
                sl.Add("StaffCategoryId", "StaffCategory cannot be blank.");
            }

            return sl;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #region Save
    public string Save()
    {
        try
        {
            if (_jobProfileDTO.IsNew == true)
            {
                _jobProfileDTO.JobOfferingId = _jobProfileDAl.Insert(_jobProfileDTO);
            }
            else
            {
                _jobProfileDTO.JobOfferingId = _jobProfileDAl.Update(_jobProfileDTO);
            }
            return _jobProfileDTO.JobOfferingId;
        }
        catch
        {
            throw new Exception("Error while saving, Data cannot be Saved.");
        }
    }
    #endregion

    #region Delete
    public void Delete(string JobOfferingId)
    {
        string IsReferenced = _jobProfileDAl.IsReferenced(JobOfferingId);
        try
        {
            if (IsReferenced != "")
            {
                throw new Exception("Employee is Referenced. Cannot Delete." + "\n" + IsReferenced);
            }
            else
            {
                _jobProfileDAl.Delete(JobOfferingId);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Employee is Referenced. Cannot Delete." + "\n" + IsReferenced)
                throw new Exception(ex.Message);
            else
                throw new Exception("Error while deleting, Data cannot be deleted.");
        }
    }
    #endregion

    #region  "Load Dropdowns"

    public DataTable GetDesignations(string StandardId)
    {
        try
        {
            return _jobProfileDAl.Designations(StandardId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetDivision()
    {
        try
        {
            GeneralDAL _g = new GeneralDAL();
            return _g.TextList("Division");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetDepartment()
    {
        try
        {
            GeneralDAL _g = new GeneralDAL();
            return _g.TextList("Standard");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetStafcategory()
    {
        try
        {
            return _jobProfileDAl.Stafcategory();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    #endregion


    
}