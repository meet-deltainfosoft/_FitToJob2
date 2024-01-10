using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

public class AppRoleBLL
{
    AppRoleDAL _appRoleDAL;
    AppRoleDTO _appRoleDTO;
    ArrayList _appRoleForms;
    DataTable _dtAllForms;

	public AppRoleBLL()
	{
        _appRoleDAL = new AppRoleDAL();
        _appRoleDTO = new AppRoleDTO();
        _dtAllForms = new DataTable();
        _appRoleForms = new ArrayList();

        _appRoleDTO.IsNew = true;
    }

    public AppRoleBLL(string appRoleId):this()
    {
        _appRoleDTO.IsNew = false;
        Load(appRoleId);
    }

    ~AppRoleBLL()
    {
        _appRoleDAL = null;
        _dtAllForms = null;
        _appRoleForms = null;
    }

    public string AppRoleId
    {
        get
        {
            return _appRoleDTO.AppRoleId;
        }
    }

    public string Name
    {
        get
        {
            return _appRoleDTO.Name;
        }
        set
        {
            _appRoleDTO.Name = value;
        }
    }

    public string Desc
    {
        get
        {
            return _appRoleDTO.Desc;
        }
        set
        {
            _appRoleDTO.Desc = value;
        }
   }

    public ArrayList AppRoleForms
    {
        get
        {
            return _appRoleForms;
        }
        set
        {
            _appRoleForms = value;
        }
    }

    public DataTable AllForms
    {
        get
        {
            return _dtAllForms;
        }
    }

    public bool IsNew
    {
        get
        {
            return _appRoleDTO.IsNew;
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        //Name
        if (_appRoleDTO.Name == null)
        {
            sl.Add("Name", "Name cannot be blank.");
        }
        else if (_appRoleDTO.Name.Trim().Length > 50)
        {
            sl.Add("Name", "Name cannot be more than 50 characters.");
        }
        else if (IsNew ? _appRoleDAL.NameExists(_appRoleDTO.Name) : _appRoleDAL.NameExists(_appRoleDTO.Name, _appRoleDTO.AppRoleId) == true)
        {
            sl.Add("Name", "Duplicate Name.");
        }
        //Name

        //Desc
        if (_appRoleDTO.Desc == null)
        {
            sl.Add("Desc", "Description cannot be blank.");
        }
        else if (_appRoleDTO.Desc.Trim().Length > 500)
        {
            sl.Add("Desc", "Description cannot be more than 500 characters.");
        }
        //Desc

        //AppRole Forms
        if (_appRoleForms.Count == 0)
        {
            sl.Add("AppRoleForm", "Application Role must have at least one form checked.");
        }
        //AppRole Forms

        return sl;
    }

    public void Load(string appRoleId)
    {
        _appRoleDTO = _appRoleDAL.Select(appRoleId, out _dtAllForms);
    }

    public void GetAllForms()
    {
        _dtAllForms = _appRoleDAL.GetAllForms();
    }

    public void Save()
    {
        try
        {
            if (_appRoleDTO.IsNew == true)
            {
                _appRoleDAL.Insert(_appRoleDTO, _appRoleForms);
            }
            else
            {
                _appRoleDAL.Update(_appRoleDTO, _appRoleForms);
            }
        }
        catch (Exception ex)
        {

            throw new Exception("Error while saving, Data cannot be saved.");
        }
    }    

    public void Delete(string indentId)
    {
        string isReferenced = _appRoleDAL.IsReferenced(indentId);

        try
        {

            if (isReferenced != "")
            {
                throw new Exception("Application Role is Referenced. Cannot Delete." + "\n" + isReferenced);
            }
            else
            {
                _appRoleDAL.Delete(indentId);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Application Role is Referenced. Cannot Delete." + "\n" + isReferenced)
                throw ex;
            else
                throw new Exception("Error while deleting, Data cannot be deleted.");
        }
    }

}
