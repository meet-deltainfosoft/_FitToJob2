using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

public class DesignationBLL
{
    #region Declaration
    private DesignationDAL _designationDAL;
    private GeneralDAL _generalDAL;
    private DesignationDTO _designationDTO;
    #endregion

    #region Constructor
    public DesignationBLL()
    {
        _designationDAL = new DesignationDAL();
        _generalDAL = new GeneralDAL();
        _designationDTO = new DesignationDTO();
        _designationDTO.IsNew = true;
    }
    ~DesignationBLL()
    {
        _designationDAL = null;
        _generalDAL = null;
        _designationDTO = null;
    }
    public DesignationBLL(string DesignationId)
        : this()
    {
        _designationDTO.IsNew = false;
        Load(DesignationId);
    }
    #endregion

    #region Get Set Methods
    public string DesignationId
    {
        get
        {
            return _designationDTO.DesignationId;
        }
    }
    public string Name
    {
        set
        {
            _designationDTO.Name = value;
        }
        get
        {
            return _designationDTO.Name;
        }
    }
    public string DeptId
    {
        set
        {
            _designationDTO.DeptId = value;
        }
        get
        {
            return _designationDTO.DeptId;
        }
    }
    public string ReportDesignId
    {
        set
        {
            _designationDTO.ReportDesignId = value;
        }
        get
        {
            return _designationDTO.ReportDesignId;
        }
    }
    public string ReportDesignName
    {
        set
        {
            _designationDTO.ReportDesignName = value;
        }
        get
        {
            return _designationDTO.ReportDesignName;
        }
    }
    public bool IsNew
    {
        get
        {
            return _designationDTO.IsNew;
        }
    }
    #endregion

    #region Validation
    public SortedList Validate()
    {
        try
        {
            SortedList sl = new SortedList();

            if (_designationDTO.Name == null)
            {
                sl.Add("Name", "Designation Name cannot be blank.");
            }
            if (_designationDTO.DeptId == null)
            {
                sl.Add("DeptId", "Please Select Department.");
            }
            if (_designationDTO.Name != null && _designationDTO.ReportDesignName != null)
            {
                if (_designationDTO.Name == _designationDTO.ReportDesignName)
                {
                    sl.Add("ReportingDesign", "Designation Name and Reporting Designation cannot be same.");
                }
            }
            return sl;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region Save
    public void Save()
    {
        try
        {
            if (_designationDTO.IsNew == true)
            {
                _designationDAL.Insert(_designationDTO);
            }
            else
            {
                _designationDAL.Update(_designationDTO);
            }
        }
        catch
        {
            throw new Exception("Error while saving, Data cannot be Saved.");
        }
    }
    #endregion

    #region Load Function
    public void Load(string DesignationId)
    {
        try
        {
            _designationDTO = _designationDAL.Select(DesignationId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region Delete
    public void Delete(string DesignationId)
    {
        string IsReferenced = _designationDAL.IsReferenced(DesignationId);
        try
        {
            if (IsReferenced != "")
            {
                throw new Exception("Designation Name is Referenced. Cannot Delete." + "\n" + IsReferenced);
            }
            else
            {
                _designationDAL.Delete(DesignationId);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Designation Name is Referenced. Cannot Delete." + "\n" + IsReferenced)
                throw ex;
            else
                throw new Exception("Error while deleting, Data cannot be deleted.");
        }
    }
    #endregion

    #region Load DropDown
    public DataTable GetDepts()
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
    public DataTable GetReportingDesign()
    {
        try
        {
            return _designationDAL.GetReportingDesign();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion
    
}