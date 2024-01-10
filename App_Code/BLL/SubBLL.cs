using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;

public class SubBLL
{
    private SubDTO _SubDTO;
    private SubDAL _SubDAL;

    public SubBLL()
    {
        _SubDAL = new SubDAL();
        _SubDTO = new SubDTO();

        _SubDTO.IsNew = true;
    }
    public SubBLL(string SubId)
        : this()
    {
        _SubDTO.IsNew = false;
        Load(SubId);
    }

    ~SubBLL()
    {
        _SubDAL = null;
    }

    public string SubId
    {
        get
        {
            return _SubDTO.SubId;
        }
        set
        {
            _SubDTO.SubId = value;
        }
    }

    public string Name
    {
        get
        {
            return _SubDTO.Name;
        }
        set
        {
            _SubDTO.Name = value;
        }
    }

    public string Remarks
    {
        get
        {
            return _SubDTO.Remarks;
        }
        set
        {
            _SubDTO.Remarks = value;
        }
    }

    public string StandardTextListId
    {
        get
        {
            return _SubDTO.StandardTextListId;
        }
        set
        {
            _SubDTO.StandardTextListId = value;
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_SubDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank.");
        }

        //Name
        if (_SubDTO.Name == null)
        {
            sl.Add("Names", "Name cannot be blank.");
        }
        else if (_SubDTO.Name.Trim().Length > 150)
        {
            sl.Add("Names", "Name cannot be more than 150 characters.");
        }

        else if (_SubDTO.IsNew == true)
        {
            if (_SubDAL.NamesExist(_SubDTO.Name,_SubDTO.StandardTextListId) == true)
            {
                sl.Add("Names", "Duplicate Name.");
            }
        }
        else if (_SubDTO.IsNew == false)
        {
            if (_SubDAL.NamesExists(_SubDTO.Name, _SubDTO.SubId) == true)
            {
                //sl.Add("Names", "Duplicate Name.");
            }
        }
        //Name
        return sl;
    }

    public void Save()
    {
        try
        {
            if (_SubDTO.IsNew == true)
                _SubDAL.Insert(_SubDTO);
            else
                _SubDAL.Update(_SubDTO);
        }
        catch
        {
            throw new Exception("Error while saving, Data cannot be Saved.");
        }
    }
    public void Load(string SubId)
    {
        ArrayList alSubLns = new ArrayList();
        _SubDTO = _SubDAL.Select(SubId);
    }

    public void Delete(string SubId)
    {
        try
        {
            _SubDAL.Delete(SubId);
        }
        catch (Exception)
        {
            throw new Exception("Error while deleting, Data cannot be deleted.");
        }
    }

    public DataTable LoadStandard()
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            return _generalDAL.TextList("Standard");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public bool? IsStudyMaterialAllowed
    {
        get
        {
            return _SubDTO.IsStudyMaterialAllowed;
        }
        set
        {
            _SubDTO.IsStudyMaterialAllowed = value;
        }
    }

    public string ImagePhoto
    {
        get
        {
            return _SubDTO.ImagePhoto;
        }
        set
        {
            _SubDTO.ImagePhoto = value;
        }
    }
}
