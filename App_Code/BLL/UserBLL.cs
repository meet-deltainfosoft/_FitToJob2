using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

public class UserBLL
{
    UserDAL _userDAL;
    UserDTO _userDTO;
    ArrayList _userAppRoles;
    DataTable _dtAllAppRoles;

	public UserBLL()
	{
        _userDAL = new UserDAL();
        _userDTO = new UserDTO();
        _userAppRoles = new ArrayList();
        _dtAllAppRoles = new DataTable();

        _userDTO.IsNew = true;
    }

    public UserBLL(string userId):this()
    {
        _userDTO.IsNew = false;
        Load(userId);
    }

    ~UserBLL()
    {
        _userDAL = null;
        _userAppRoles = null;
        _dtAllAppRoles = null;
    }

    public string UserId
    {
        get
        {
            return _userDTO.UserId;
        }
    }

    public string FirstName
    {
        get
        {
            return _userDTO.FirstName;
        }
        set
        {
            _userDTO.FirstName = value;
        }
    }

    public string LastName
    {
        get
        {
            return _userDTO.LastName;
        }
        set
        {
            _userDTO.LastName = value;
        }
    }

    public string UserName
    {
        get
        {
            return _userDTO.UserName;
        }
        set
        {
            _userDTO.UserName = value;
        }
    }

    public string Password
    {
        get
        {
            return _userDTO.Password;
        }
        set
        {
            _userDTO.Password = value;
        }
    }
    //by kinnari
    public string DeptId
    {
        get
        {
            return _userDTO.DeptId;
        }
        set
        {
            _userDTO.DeptId = value;
        }
    }

    public bool IsDisabled
    {
        get
        {
            return _userDTO.IsDisabled;
        }
        set
        {
            _userDTO.IsDisabled = value;
        }
    }

    public ArrayList UserAppRoles
    {
        get
        {
            return _userAppRoles;
        }
        set
        {
            _userAppRoles = value;
        }
    }

    public DataTable AllAppRoles
    {
        get
        {
            return _dtAllAppRoles;
        }
    }
    //by kinnari
    public DataTable LoadDept()
    {
        return _userDAL.LoadDept();
    }
    //
    public bool IsNew
    {
        get
        {
            return _userDTO.IsNew;
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        //FirstName
        if (_userDTO.FirstName == null)
        {
            sl.Add("FirstName", "First Name cannot be blank.");
        }
        //else if (_userDTO.FirstName.Trim().Length > 20)
        //{
        //    sl.Add("FirstName", "First Name cannot be more than 20 characters.");
        //}
        //FirstName

        //LastName
        if (_userDTO.LastName == null)
        {
            sl.Add("LastName", "Last Name cannot be blank.");
        }
        //else if (_userDTO.LastName.Trim().Length > 20)
        //{
        //    sl.Add("LastName", "Last Name cannot be more than 20 characters.");
        //}
        //LastName

        //If the First Name and Last Name not Null, then check it for Duplicate
        if (_userDTO.FirstName != null && _userDTO.LastName != null)
        {
            if (IsNew ? _userDAL.FirstAndLastNameExists(_userDTO.FirstName, _userDTO.LastName) : _userDAL.FirstAndLastNameExists(_userDTO.FirstName, _userDTO.LastName, _userDTO.UserId) == true)
            {
                //First Name, if first name is not added in sorted list for error then add it
                if (sl.Contains("FirstName") == false)
                    sl.Add("FirstName", "Duplicate First Name.");

                //Last Name, if last name is not added in sorted list for error then add it
                if (sl.Contains("LastName") == false)
                    sl.Add("LastName", "Duplicate Last Name.");

            }
        }

        //UserName
        if (_userDTO.UserName == null)
        {
            sl.Add("UserName", "User Name cannot be blank.");
        }
        //else if (_userDTO.UserName.Trim().Length > 10)
        //{
        //    sl.Add("UserName", "User Name cannot be more than 10 characters.");
        //}
        else if (IsNew ? _userDAL.UserNameExists(_userDTO.UserName) : _userDAL.UserNameExists(_userDTO.UserName, _userDTO.UserId) == true)
        {
            sl.Add("UserName", "Duplicate User Name.");
        }
        //UserName


        //Password
        if (_userDTO.Password == null)
        {
            sl.Add("Password", "Password cannot be blank.");
        }
        //else if (_userDTO.Password.Trim().Length > 10)
        //{
        //    sl.Add("Password", "Password cannot be more than 10 characters.");
        //}
        //Password


        //AppRoles
        if (_userAppRoles.Count == 0)
        {
            sl.Add("UserAppRoles", "User must have at least one Application Role checked.");
        }
        //AppRole 

        return sl;
    }

    public void Load(string userId)
    {
        _userDTO = _userDAL.Select(userId, out _dtAllAppRoles);
    }

    public void GetAllAppRoles()
    {
        _dtAllAppRoles = _userDAL.GetAllAppRoles();
    }

    public void Save()
    {
        try
        {
            if (_userDTO.IsNew == true)
            {
                _userDAL.Insert(_userDTO, _userAppRoles);
            }
            else
            {
                _userDAL.Update(_userDTO, _userAppRoles);
            }
        }
        catch (Exception ex)
        {

            throw new Exception("Error while saving, Data cannot be saved.");
        }
    }

    public void Delete(string userId)
    {
        string isReferenced = _userDAL.IsReferenced(userId);
        try
        {            
            if (isReferenced !="")
            {
                throw new Exception("User is Referenced. Cannot Delete." + "\n" + isReferenced);
            }
            else
            {
                _userDAL.Delete(userId);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "User is Referenced. Cannot Delete." + "\n" + isReferenced)
                throw ex;
            else
                throw new Exception("Error while deleting, Data cannot be deleted.");
        }
    }
}
