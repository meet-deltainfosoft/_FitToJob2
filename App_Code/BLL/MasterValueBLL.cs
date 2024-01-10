using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

public class MasterValueBLL
{
    private MasterValueDTO _masterValueDTO;
    private MasterValueDAL _masterValueDAL;
    private GeneralDAL _generalDAL;

    public MasterValueBLL()
    {
        _masterValueDAL = new MasterValueDAL();
        _masterValueDTO = new MasterValueDTO();
        _generalDAL = new GeneralDAL();

        _masterValueDTO.IsNew = true;
    }

    public MasterValueBLL(string textListId)
        : this()
    {
        _masterValueDTO.IsNew = false;
        Load(textListId);
    }

    ~MasterValueBLL()
    {
        _masterValueDAL = null;
        _masterValueDTO = null;
        _generalDAL = null;
    }

    public DataTable Groups()
    {
        return _masterValueDAL.Groups();
    }

    public string TextListId
    {
        get
        {
            return _masterValueDTO.TextListId;
        }
    }

    public string Group
    {
        get
        {
            return _masterValueDTO.Group;
        }
        set
        {
            _masterValueDTO.Group = value;
        }
    }

    public string Text
    {
        get
        {
            return _masterValueDTO.Text;
        }
        set
        {
            _masterValueDTO.Text = value;
        }
    }

    public string Address
    {
        get
        {
            return _masterValueDTO.Address;
        }
        set
        {
            _masterValueDTO.Address = value;
        }
    }
    
    public bool IsNew
    {
        get
        {
            return _masterValueDTO.IsNew;
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        //Group
        if (_masterValueDTO.Group == null)
        {
            sl.Add("Group", "Group cannot be blank.");
        }
        else if (_masterValueDTO.Group.Length > 100)
        {
            sl.Add("Group", "Group cannot be more than 100 characters.");
        }

        //Text
        if (_masterValueDTO.Text == null)
        {
            sl.Add("Text", "Value cannot be blank.");
        }
        else if (_masterValueDTO.Text.Length > 100)
        {
            sl.Add("Text", "Value cannot be more than 100 characters.");
        }

        //Check for duplicate Grop and value
        if (_masterValueDTO.Group != null && _masterValueDTO.Text != null)
        {
            if (_masterValueDAL.CheckIfExists(_masterValueDTO.Group, _masterValueDTO.Text, _masterValueDTO.TextListId) == true)
            {
                sl.Add("Name", "Duplicate Group and Value.");
            }
        }
        
        return sl;
    }

    public string Save()
    {
        try
        {

            if (_masterValueDTO.IsNew == true)
            {
                _masterValueDTO.TextListId = _masterValueDAL.Insert(_masterValueDTO);
            }
            else
            {
                _masterValueDAL.Update(_masterValueDTO);
            }

        }
        catch
        {
            throw new Exception("Error while saving, Data cannot be Saved.");
        }
        return _masterValueDTO.TextListId;
    }

    public void Load(string textListId)
    {
        _masterValueDTO = _masterValueDAL.Select(textListId);
    }

    public void Delete(string textListId)
    {
        string IsReferenced = _masterValueDAL.IsReferenced(textListId, _masterValueDTO.Text);
        try
        {
            if (IsReferenced != "")
            {
                throw new Exception("Value is Referenced. Cannot Delete." + "\n" + IsReferenced);
            }
            else
            {
                _masterValueDAL.Delete(textListId);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Value is Referenced. Cannot Delete." + "\n" + IsReferenced)
                throw ex;
            else
                throw new Exception("Error while deleting, Data cannot be deleted.");
        }
    }
}
