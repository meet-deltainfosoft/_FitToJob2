using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for JobProfilesBLL
/// </summary>
public class JobProfilesBLL
{
    #region Declaration
    private JobProfilesDAL _jobProfilesDAL;
    private string _DepartmentId;
    private string _DivisionId;
    private string _DesignationId;
    private string _StaffCategoryTextListId;
    private bool _allRecords;
    #endregion

    #region Constructor Destructor
    public JobProfilesBLL()
    {
        _jobProfilesDAL = new JobProfilesDAL();
    }
    ~JobProfilesBLL()
    {
        _jobProfilesDAL = null;
    }
    #endregion

     #region Get Set Methods

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
    public string StaffCategoryTextListId
    {
        set
        {
            _StaffCategoryTextListId = value;
        }
    }
    public bool AllRecords
    {
        set
        {
            _allRecords = value;
        }
    }
  
    #endregion

    #region Functions
    public DataTable JobProfiles()
    {
        try
        {
            return _jobProfilesDAL.JobProfiles(_DepartmentId, _DivisionId, _DesignationId, _StaffCategoryTextListId, _allRecords);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion


    #region  "Load Dropdowns"

    public DataTable GetDesignations(string StandardId)
    {
        try
        {
            return _jobProfilesDAL.Designations(StandardId);
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
            return _jobProfilesDAL.Stafcategory();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    #endregion
}