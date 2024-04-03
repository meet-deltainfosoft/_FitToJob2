using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.IO;

/// <summary>
/// Summary description for Registration1BLL
/// </summary>
public class Registration1BLL
{
    #region Declarations
    private Registration1DAL _registrationDAL;
    private GeneralDAL _generalDAL;
    private Registration1DTO _registrationDTO;
    #endregion

    #region Constructor
    public Registration1BLL()
    {
        _registrationDAL = new Registration1DAL();
        _generalDAL = new GeneralDAL();
        _registrationDTO = new Registration1DTO();
        _registrationDTO.IsNew = true;
        _registrationDTO.RegistrationId = _generalDAL.GetNEWID();
    }
    ~Registration1BLL()
    {
        _registrationDAL = null;
        _generalDAL = null;
        _registrationDTO = null;
    }
    public Registration1BLL(string RegistrationId)
        : this()
    {
        _registrationDTO.IsNew = false;
        Load(RegistrationId);
    }
    #endregion

    #region Load
    public void Load(string RegistrationId)
    {
        try
        {
            _registrationDTO = _registrationDAL.Select(RegistrationId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    public string RegistrationId
    {
        set
        {
            _registrationDTO.RegistrationId = value;
        }
        get
        {
            return _registrationDTO.RegistrationId;
        }
    }
    public string AadharCardNo
    {
        set
        {
            _registrationDTO.AadharCardNo = value;
        }
        get
        {
            return _registrationDTO.AadharCardNo;
        }
    }
    public string FirstName
    {
        set
        {
            _registrationDTO.FirstName = value;
        }
        get
        {
            return _registrationDTO.FirstName;
        }
    }
    public string MiddleName
    {
        set
        {
            _registrationDTO.MiddleName = value;
        }
        get
        {
            return _registrationDTO.MiddleName;
        }
    }
    public string LastName
    {
        set
        {
            _registrationDTO.LastName = value;
        }
        get
        {
            return _registrationDTO.LastName;
        }
    }
    public string MobileNo
    {
        set
        {
            _registrationDTO.MobileNo = value;
        }
        get
        {
            return _registrationDTO.MobileNo;
        }
    }
    public string City
    {
        set
        {
            _registrationDTO.City = value;
        }
        get
        {
            return _registrationDTO.City;
        }
    }
    public string Taluka
    {
        set
        {
            _registrationDTO.Taluka = value;
        }
        get
        {
            return _registrationDTO.Taluka;
        }
    }
    public string District
    {
        set
        {
            _registrationDTO.District = value;
        }
        get
        {
            return _registrationDTO.District;
        }
    }

    public string State
    {
        set
        {
            _registrationDTO.State = value;
        }
        get
        {
            return _registrationDTO.State;
        }
    }
    public string Address
    {
        set
        {
            _registrationDTO.Address = value;
        }
        get
        {
            return _registrationDTO.Address;
        }
    }

    public string PhotoPath
    {
        set
        {
            _registrationDTO.PhotoPath = value;
        }
        get
        {
            return _registrationDTO.PhotoPath;
        }
    }

    public string SelfIntroVideoPath
    {
        set
        {
            _registrationDTO.SelfIntroVideoPath = value;
        }
        get
        {
            return _registrationDTO.SelfIntroVideoPath;
        }
    }

    public string Resume
    {
        set
        {
            _registrationDTO.Resume = value;
        }
        get
        {
            return _registrationDTO.Resume;
        }
    }

    public string permanentPinCode
    {
        set
        {
            _registrationDTO.permanentPinCode = value;
        }
        get
        {
            return _registrationDTO.permanentPinCode;
        }
    }

    public bool IsNew
    {
        get
        {
            return _registrationDTO.IsNew;
        }
    }

    public SortedList Validate()
    {
        try
        {
            SortedList sl = new SortedList();
            if (_registrationDTO.AadharCardNo == null)
            {
                sl.Add("AadharNo", "AadharCard No. cannot be blank.");
            }
            else if (_registrationDTO.AadharCardNo != null)
            {
                if (_registrationDTO.IsNew ? _registrationDAL.NameExists(_registrationDTO.AadharCardNo) : _registrationDAL.NameExists(_registrationDTO.AadharCardNo, _registrationDTO.RegistrationId) == true)
                {
                    sl.Add("AadharNo", "Already Exits AadharCard No. ");
                }
            }

            if (_registrationDTO.FirstName == null)
            {
                sl.Add("Firstname", "FirstName cannot be blank.");
            }
            if (_registrationDTO.MiddleName == null)
            {
                sl.Add("Middlename", "Middle Name cannot be blank.");
            }
            if (_registrationDTO.LastName == null)
            {
                sl.Add("LastName", "Last Name cannot be blank.");
            }

            if (_registrationDTO.MobileNo == null)
            {
                sl.Add("MobileNo", "Mobile No. cannot be blank.");
            }
            else if (_registrationDTO.MobileNo != null)
            {
                if (_registrationDTO.IsNew ? _registrationDAL.NameExist(_registrationDTO.MobileNo) : _registrationDAL.NameExist(_registrationDTO.MobileNo, _registrationDTO.RegistrationId) == true)
                {
                    sl.Add("MobileNo", "Already Exits Mobile No. ");
                }
            }

            if (_registrationDTO.City == null)
            {
                sl.Add("City", "City cannot be blank.");
            }

            if (_registrationDTO.Taluka == null)
            {
                sl.Add("Taluka", "Taluka cannot be blank.");
            }

            if (_registrationDTO.District == null)
            {
                sl.Add("District", "District cannot be blank.");
            }

            if (_registrationDTO.State == null)
            {
                sl.Add("State", "State cannot be blank.");
            }
            if (_registrationDTO.Address == null)
            {
                sl.Add("Address", "Address cannot be blank.");
            }
            else if (_registrationDTO.Address != null)
            {
                if (_registrationDTO.Address.Length > 500)
                    sl.Add("Address", "Address can not be greater then 500 characters.");
            }
            if (_registrationDTO.permanentPinCode == null)
            {
                sl.Add("Pincode", "Pincode cannot be blank.");

            }
            //else if (_registrationDTO.permanentPinCode != null)
            //{
            //    if (_registrationDTO.permanentPinCode.Length > 6)
            //        sl.Add("Pincode", "Pincode can not be greater then 6 characters.");
            //}

            if (_registrationDTO.Resume != null)
            {
                string fileExtension = Path.GetExtension(_registrationDTO.Resume).ToLower();
                string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".jpg", ".jpeg", ".png" };
                if (Array.IndexOf(allowedExtensions, fileExtension) != -1)
                {
                }
                else
                {
                    sl.Add("Resume", "Invalid file type. Please upload Resume.");
                }
            }

            return sl;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #region Save
    public string Save()
    {
        try
        {
            if (_registrationDTO.IsNew == true)
            {
                _registrationDTO.RegistrationId = _registrationDAL.Insert(_registrationDTO);
            }
            else
            {
                _registrationDTO.RegistrationId = _registrationDAL.Update(_registrationDTO);
            }
            return _registrationDTO.RegistrationId;
        }
        catch
        {
            throw new Exception("Error while saving, Data cannot be Saved.");
        }
    }
    #endregion

    #region Delete
    public void Delete(string RegisteationId)
    {
        string IsReferenced = _registrationDAL.IsReferenced(RegisteationId);
        try
        {
            if (IsReferenced != "")
            {
                throw new Exception("Employee is Referenced. Cannot Delete." + "\n" + IsReferenced);
            }
            else
            {
                _registrationDAL.Delete(RegisteationId);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Employee is Referenced. Cannot Delete." + "\n" + IsReferenced)
                throw new Exception(ex.Message);
            else
                throw new Exception("Error while deleting, Data cannot be deleted.");
        }
    }
    #endregion

}