using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

public class FormBLL
{
    FormDAL _formDAL;
    GeneralDAL _generalDAL;
    FormDTO _formDTO;
    FormDTO _formDTOPersist;

    public FormBLL()
    {
        _formDAL = new FormDAL();
        _generalDAL = new GeneralDAL();
        _formDTO = new FormDTO();

        _formDTO.IsNew = true;
    }

    public FormBLL(string formId) : this()
    {
        _formDTO.IsNew = false;
        Load(formId);
    }

    ~FormBLL()
    {
        _formDAL = null;
        _generalDAL = null;
    }

    public string FormId
    {
        get
        {
            return _formDTO.FormId;
        }
    }

    public string ModuleTextListId
    {
        get
        {
            return _formDTO.ModuleTextListId;
        }
        set
        {
            _formDTO.ModuleTextListId = value;
        }
    }

    public string Name
    {
        get
        {
            return _formDTO.Name;
        }
        set
        {
            _formDTO.Name = value;
        }
    }

    public string Desc
    {
        get
        {
            return _formDTO.Desc;
        }
        set
        {
            _formDTO.Desc = value;
        }
    }

    public string ModuleName
    {
        get
        {
            return _formDTO.ModuleName;
        }
    }

    public bool IsNew
    {
        get
        {
            return _formDTO.IsNew;
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();
        string isReferenced = "";

        //Get If reference detail
        if (_formDTO.IsNew == false)
        {
            isReferenced = _formDAL.IsReferenced(_formDTO.FormId);
        }

        //ModuleTextListId
        if (_formDTO.ModuleTextListId == null)
        {
            sl.Add("ModuleTextListId", "Select Module.");
        }
        else if (isReferenced !="" && _formDTO.ModuleTextListId != _formDTOPersist.ModuleTextListId)
        {
            sl.Add("ModuleTextListId", "Form is Referenced, Module cannot be changed." + "\n" + isReferenced);
        }
        //ModuleTextListId

        //Name
        if (_formDTO.Name == null)
        {
            sl.Add("Name", "Name cannot be blank.");
        }
        else if (_formDTO.Name.ToString().Trim().Length > 60)
        {
            sl.Add("Name", "Name cannot be more than 60 characters.");
        }
        else if (_formDTO.IsNew ? _formDAL.NameExists(_formDTO.Name) : _formDAL.NameExists(_formDTO.Name, _formDTO.FormId) == true)
        {
            sl.Add("Name", "Duplicate Name.");
        }
        else if (isReferenced != "" && _formDTO.Name != _formDTOPersist.Name)
        {
            sl.Add("Name", "Form is Referenced, Name cannot be changed." + "\n" + isReferenced);
        }
        //Name

        //Desc
        if (_formDTO.Desc != null)
        {
            if (_formDTO.Desc.Length > 500)
            {
                sl.Add("Desc", "Description cannot be more than 500 characters.");
            }
        }
        //Desc

        return sl;
    }

    public void Load(string formId)
    {
        ArrayList alIndentLnsDTO = new ArrayList();

        _formDTO = _formDAL.Select(formId);
        _formDTOPersist = _formDTO;
    }

    public void Save()
    {
        try
        {
            if (_formDTO.IsNew == true)
            {
                _formDAL.Insert(_formDTO);
            }
            else
            {
                _formDAL.Update(_formDTO);
            }
        }
        catch
        {
            throw new Exception("Error while saving, Data cannot be saved.");
        }
    }

    public void Delete(string formId)
    {
        string isReferenced = _formDAL.IsReferenced(formId);
        try
        {            
            if (isReferenced !="")
            {
                throw new Exception("Form is Referenced. Cannot Delete." + "\n" + isReferenced);
            }
            else
            {
                _formDAL.Delete(formId);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Form is Referenced. Cannot Delete." + "\n" + isReferenced)
                throw ex;
            else
                throw new Exception("Error while deleting, Data cannot be deleted.");
        }
    }

    public DataTable Modules()
    {
        return _generalDAL.TextList("Module");
    }
}