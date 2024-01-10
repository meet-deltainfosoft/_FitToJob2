using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for InterViewFormsBLL
/// </summary>
public class InterViewFormsBLL
{
	#region Declaration
    private InterViewFormsDAL _InterViewFormsDAL;
    private string _AadharNo;
    private string _firstName;
    private string _PanCardNo;
    private DateTime? _DOB;
    private string _MobileNo;
    private bool _allRecords;
    #endregion

    #region Constructor Destructor
    public InterViewFormsBLL()
    {
        _InterViewFormsDAL = new InterViewFormsDAL();
    }
    ~InterViewFormsBLL()
    {
        _InterViewFormsDAL = null;
    }
    #endregion

    #region Get Set Methods

    public string AadharNo
    {
        set
        {
            _AadharNo = value;
        }
    }
    public string FirstName
    {
        set
        {
            _firstName = value;
        }
    }
    public string PanCardNo
    {
        set
        {
            _PanCardNo = value;
        }
    }
    public DateTime? DOB
    {
        set
        {
            _DOB = value;
        }
    }
    public string MobileNo
    {
        set
        {
            _MobileNo = value;
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
    public DataTable InterView()
    {
        try
        {
            return _InterViewFormsDAL.InterView(_AadharNo, _firstName, _PanCardNo, _DOB, _MobileNo, _allRecords);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

}