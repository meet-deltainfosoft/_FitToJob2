using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Registrations1BLL
/// </summary>
public class Registrations1BLL
{
	 #region Declaration
    private Registrations1DAL _registrationsDAL;
    private string _AadharNo;
    private string _firstName;
    private string _lastName;
    private string _MiddleName;
    private string _MobileNo;
    private bool _allRecords;
    #endregion

    #region Constructor Destructor
    public Registrations1BLL()
    {
        _registrationsDAL = new Registrations1DAL();
    }
    ~Registrations1BLL()
    {
        _registrationsDAL = null;
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
    public string LastName
    {
        set
        {
            _lastName = value;
        }
    }
    public string MiddleName
    {
        set
        {
            _MiddleName = value;
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
    public DataTable Registration()
    {
        try
        {
            return _registrationsDAL.Registration(_AadharNo, _firstName, _lastName, _MiddleName, _MobileNo, _allRecords);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

}