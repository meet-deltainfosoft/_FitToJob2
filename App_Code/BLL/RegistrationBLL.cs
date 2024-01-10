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

public class RegistrationBLL
{
    private RegistrationDTO _RegistrationDTO;
    private RegistrationDAL _RegistrationDAL;
    private GeneralDAL _GeneralDAL;

    public RegistrationBLL()
    {
        _RegistrationDAL = new RegistrationDAL();
        _RegistrationDTO = new RegistrationDTO();
        _GeneralDAL = new GeneralDAL();
        _RegistrationDTO.IsNew = true;
    }
    public RegistrationBLL(string RegistrationId)
        : this()
    {
        _RegistrationDTO.IsNew = false;
        Load(RegistrationId);
    }

    ~RegistrationBLL()
    {
        _RegistrationDAL = null;
        _GeneralDAL = null;
    }

    public string RegistrationId
    {
        get
        {
            return _RegistrationDTO.RegistrationId;
        }
        set
        {
            _RegistrationDTO.RegistrationId = value;
        }
    }

    public string RegistrationName
    {
        get
        {
            return _RegistrationDTO.RegistrationName;
        }
        set
        {
            _RegistrationDTO.RegistrationName = value;
        }
    }

    public string StandardId
    {
        get
        {
            return _RegistrationDTO.StandardId;
        }
        set
        {
            _RegistrationDTO.StandardId = value;
        }
    }

    public string DivisionTextListId
    {
        get
        {
            return _RegistrationDTO.DivisionTextListId;
        }
        set
        {
            _RegistrationDTO.DivisionTextListId= value;
        }
    }

    public string MobileNo
    {
        get
        {
            return _RegistrationDTO.MobileNo;
        }
        set
        {
            _RegistrationDTO.MobileNo = value;
        }
    }

    public string ExtraMobileNo
    {
        get
        {
            return _RegistrationDTO.ExtraMobileNo;
        }
        set
        {
            _RegistrationDTO.ExtraMobileNo = value;
        }
    }

    public bool? IsDeActive
    {
        get
        {
            return _RegistrationDTO.IsDeActive;
        }
        set
        {
            _RegistrationDTO.IsDeActive = value;
        }
    }

    public string ExamNo
    {
        get
        {
            return _RegistrationDTO.ExamNo;
        }
        set
        {
            _RegistrationDTO.ExamNo = value;
        }
    }
    public string SchoolName
    {
        get
        {
            return _RegistrationDTO.SchoolName;
        }
        set
        {
            _RegistrationDTO.SchoolName = value;
        }
    }
    public string City
    {
        get
        {
            return _RegistrationDTO.City;
        }
        set
        {
            _RegistrationDTO.City = value;
        }
    }
    public string EmailId
    {
        get
        {
            return _RegistrationDTO.EmailId;
        }
        set
        {
            _RegistrationDTO.EmailId = value;
        }
    }
    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        //Name
        if (_RegistrationDTO.StandardId == null)
        {
            sl.Add("StandardId", " Please Select Department. ");
        }
        if (_RegistrationDTO.RegistrationName == null)
        {
            sl.Add("RegistrationName", "Student Name cannot be blank.");
        }
        if (_RegistrationDTO.MobileNo == null)
        {
            sl.Add("MobileNo", "Mobile No cannot be blank.");
        }
        //if (_RegistrationDTO.ExtraMobileNo == null)
        //{
        //    sl.Add("ExtraMobileNo", "Extra Mobile No cannot be blank.");
        //}
        if (_RegistrationDTO.StandardId != null && _RegistrationDTO.RegistrationName != null && _RegistrationDTO.MobileNo != null)
        {
            if (_RegistrationDTO.IsNew == true)
            {
                if (_RegistrationDAL.NamesExists(_RegistrationDTO.StandardId, _RegistrationDTO.RegistrationName, _RegistrationDTO.MobileNo) == true)
                {
                    sl.Add("RegistrationName", "Duplicate Student Name.");
                }
            }
            else if (_RegistrationDTO.IsNew == false)
            {
                if (_RegistrationDAL.NamesExists(_RegistrationDTO.StandardId, _RegistrationDTO.RegistrationName, _RegistrationDTO.MobileNo, _RegistrationDTO.RegistrationId) == true)
                {
                    sl.Add("RegistrationName", "Duplicate Student Name.");
                }
            }
        }
        ////Name
        return sl;
    }

    public void Save()
    {
        try
        {
            if (_RegistrationDTO.IsNew == true)
                _RegistrationDAL.Insert(_RegistrationDTO);
            else
                _RegistrationDAL.Update(_RegistrationDTO);
        }
        catch
        {
            throw new Exception("Error while saving, Data cannot be Saved.");
        }
    }
    public void Load(string RegistrationId)
    {
        ArrayList alRegistrationLns = new ArrayList();
        _RegistrationDTO = _RegistrationDAL.Select(RegistrationId);
    }

    public void Delete(string RegistrationId)
    {
        try
        {
            _RegistrationDAL.Delete(RegistrationId);
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
            return _GeneralDAL.TextList("Standard");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable LoadDivision()
    {
        try
        {
            return _GeneralDAL.TextList("Division");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void DeleteRegistration(ArrayList al)
    {
        try
        {
            _RegistrationDAL.DeleteRegistration(al);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}