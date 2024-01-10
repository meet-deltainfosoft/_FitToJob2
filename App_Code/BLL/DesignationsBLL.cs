using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class DesignationsBLL
{
    #region Declaration
    private DesignationsDAL _designationsDAL;
    private string _Name;
    private string _DeptId;
    private bool _AllRecords;
    #endregion

    #region Constructor
    public DesignationsBLL()
    {
        _designationsDAL = new DesignationsDAL();
    }
    ~DesignationsBLL()
    {
        _designationsDAL = null;
    }
    #endregion

    #region Get Set Methods
    public string Name
    {
        set
        {
            _Name = value;
        }
    }
    public string DeptId
    {
        set
        {
            _DeptId = value;
        }
    }
    public bool AllRecords
    {
        set
        {
            _AllRecords = value;
        }
    }
    #endregion

    #region Get Data
    public DataTable Designations()
    {
        try
        {
            return _designationsDAL.Designations(_Name, _DeptId, _AllRecords);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region Load Dropdown
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
    #endregion
}