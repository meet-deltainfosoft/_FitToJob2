using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;

public class CompanyBLL
{
    CompanyDAL _CompanyDAL;
    GeneralDAL _generalDAL;
    CompanyDTO _CompanyDTO;

    public CompanyBLL()
    {
        _CompanyDAL = new CompanyDAL();
        _generalDAL = new GeneralDAL();
        _CompanyDTO = new CompanyDTO();


        _CompanyDTO.IsNew = true;
    }

    public CompanyBLL(string CompanyId)
        : this()
    {
        _CompanyDTO.IsNew = false;
        Load(CompanyId);
    }

    ~CompanyBLL()
    {
        _CompanyDAL = null;

    }

    public string CompanyId
    {
        get
        {
            return _CompanyDTO.CompanyId;
        }
    }
    public string Name
    {
        get
        {
            return _CompanyDTO.Name;
        }
        set
        {
            _CompanyDTO.Name = value;
        }
    }

    public string AddressLine1
    {
        get
        {
            return _CompanyDTO.AddressLine1;
        }
        set
        {
            _CompanyDTO.AddressLine1 = value;
        }
    }

    public string AddressLine2
    {
        get
        {
            return _CompanyDTO.AddressLine2;
        }
        set
        {
            _CompanyDTO.AddressLine2 = value;

        }
    }

    public string City
    {
        get
        {
            return _CompanyDTO.City;
        }
        set
        {
            _CompanyDTO.City = value;

        }
    }


    public string State
    {
        get
        {
            return _CompanyDTO.State;
        }
        set
        {
            _CompanyDTO.State = value;
        }
    }

    public string PINCode
    {
        get
        {
            return _CompanyDTO.PINCode;
        }
        set
        {
            _CompanyDTO.PINCode = value;
        }
    }

    public string TelephoneNos
    {
        get
        {
            return _CompanyDTO.TelephoneNos;
        }
        set
        {
            _CompanyDTO.TelephoneNos = value;
        }
    }

    public string FaxNos
    {
        get
        {
            return _CompanyDTO.FaxNos;
        }
        set
        {
            _CompanyDTO.FaxNos = value;
        }
    }

    public string EmailId
    {
        get
        {
            return _CompanyDTO.EmailId;
        }
        set
        {
            _CompanyDTO.EmailId = value;
        }
    }

    public string CrncyId
    {
        get
        {
            return _CompanyDTO.CrncyId;
        }
        set
        {
            _CompanyDTO.CrncyId = value;
        }
    }
    public string PanNo
    {
        get
        {
            return _CompanyDTO.PanNo;
        }
        set
        {
            _CompanyDTO.PanNo = value;
        }
    }

    public string TINLSTNo
    {
        get
        {
            return _CompanyDTO.TINLSTNo;
        }
        set
        {
            _CompanyDTO.TINLSTNo = value;
        }
    }
    public string TINCSTNo
    {
        get
        {
            return _CompanyDTO.TINCSTNo;
        }
        set
        {
            _CompanyDTO.TINCSTNo = value;
        }
    }
    public string VATNo
    {
        get
        {
            return _CompanyDTO.VATNo;
        }
        set
        {
            _CompanyDTO.VATNo = value;
        }
    }
    public string Website
    {
        get
        {
            return _CompanyDTO.Website;
        }
        set
        {
            _CompanyDTO.Website = value;
        }
    }
    public string MobileNo
    {
        get
        {
            return _CompanyDTO.MobileNo;
        }
        set
        {
            _CompanyDTO.MobileNo = value;
        }
    }


    public bool IsNew
    {
        get
        {
            return _CompanyDTO.IsNew;
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        //Name
        if (_CompanyDTO.Name == null)
        {
            sl.Add("AName", "Company Name cannot be blank.");
        }
        else if (_CompanyDTO.Name.ToString().Trim().Length > 100)
        {
            sl.Add("AName", "Name cannot be more than 100 characters.");
        }
        else if (_CompanyDTO.IsNew ? _CompanyDAL.NameExists(_CompanyDTO.Name) : _CompanyDAL.NameExists(_CompanyDTO.Name, _CompanyDTO.CompanyId) == true)
        {
            sl.Add("AName", "Duplicate CompanyName.");
        }

        //Name

        //AddressLine1
        if (_CompanyDTO.AddressLine1 == null)
        {
            sl.Add("BAddressLine1", "Address Line 1 is Invalid or cannot be blank.");
        }
        else
        {
            if (_CompanyDTO.AddressLine1.Length > 100)
            {
                sl.Add("BAddressLine1", "AddressLine1 cannot be more than 100 characters.");
            }
        }
        //AddressLine1

        //AddressLine2
        if (_CompanyDTO.AddressLine2 != null)
        {
            if (_CompanyDTO.AddressLine2.Length > 100)
            {
                sl.Add("CAddressLine2", "AddressLine2 cannot be more than 100 characters.");
            }
        }
        //AddressLine2

        //City
        if (_CompanyDTO.City == null)
        {
            sl.Add("DCity", "City is Invalid or cannot be blank.");
        }
        //City


        //State
        if (_CompanyDTO.State == null)
        {
            sl.Add("EState", "State is Invalid or cannot be blank.");
        }
        //State

        //Country
        if (_CompanyDTO.Country == null)
        {
            sl.Add("FCountry", "Country cannot be blank.");
        }
        //Country
        // LgrName 
        //if (_CompanyDTO.LgrId == null)
        //{
        //    sl.Add("GLgrId", "LgrName cannot be blank.");
        //}
        // LgrName 

        //GST No
        if (_CompanyDTO.GSTNo != null)
        {
            if (_CompanyDTO.GSTNo.ToString().Length < 15)
            {
                sl.Add("HGSTNo", "GST No cannot be less than 15 characters.");
            }
        }
        //GST No
        //Division
        //if (_CompanyDTO.DivTextListId == null)               // Add by jk
        //{
        //    sl.Add("IDivTextListId", "Select Division.");
        //}
        //Division
        return sl;
    }

    public void Load(string CompanyId)
    {
        try
        {
            _CompanyDTO = _CompanyDAL.Select(CompanyId);
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }


    public void Save()
    {
        try
        {
            if (_CompanyDTO.IsNew == true)
            {
                _CompanyDAL.Insert(_CompanyDTO);
            }
            else
            {
                _CompanyDAL.Update(_CompanyDTO);
            }
        }
        catch (Exception ex)
        {

            throw new Exception("Error while saving, Data cannot be saved.");
        }
    }

    public void Delete(string CompanyId)
    {
        //For Cheak Reference in relative Table
        //bool isReferenced = _CompanyDAL.IsReferenced(CompanyId);
        bool isAllowToDelete = _CompanyDAL.IsAllowToDeleted(CompanyId);

        try
        {
            if (isAllowToDelete)
            {
                string isReferenced = _CompanyDAL.IsReferenced(CompanyId);
                try
                {
                    if (isReferenced != "")
                    {
                        throw new Exception("Company is Referenced. Cannot Delete." + "\n" + isReferenced);
                    }
                    else
                    {
                        _CompanyDAL.Delete(CompanyId);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message == "Company is Referenced. Cannot Delete." + "\n" + isReferenced)
                        throw ex;
                    else
                        throw new Exception("Error while deleting, Data cannot be deleted.");
                }
            }
            else
            {
                throw new Exception("You do not have rights to delete this entry.");
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "You do not have rights to delete this entry.")
                throw ex;
            else
                throw ex;
        }
    }
    public DataTable Crncys()
    {
        try
        {
            return _generalDAL.Crncys();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string Country
    {
        get
        {
            return _CompanyDTO.Country;
        }
        set
        {
            _CompanyDTO.Country = value;
        }
    }
    // for Logo
    public byte[] Logo
    {
        get
        {
            return _CompanyDTO.Logo;
        }
        set
        {
            _CompanyDTO.Logo = value;
        }
    }

    public string LogoName
    {
        get
        {
            return _CompanyDTO.LogoName;
        }
        set
        {
            _CompanyDTO.LogoName = value;
        }
    }

    public string LogoPath
    {
        get
        {
            return _CompanyDTO.LogoPath;
        }
        set
        {
            _CompanyDTO.LogoPath = value;
        }
    }
    public void UpdateLogo()
    {
        if (_CompanyDTO.IsNew == false)
        {
            _CompanyDAL.UpdateLogo(_CompanyDTO);
        }
    }

    public string ExciseRegNo
    {
        get
        {
            return _CompanyDTO.ExciseRegNo;
        }
        set
        {
            _CompanyDTO.ExciseRegNo = value;
        }
    }

    public string ServiceTaxRegn
    {
        get
        {
            return _CompanyDTO.ServiceTaxRegn;
        }
        set
        {
            _CompanyDTO.ServiceTaxRegn = value;
        }
    }

    public string RangeDetail
    {
        get
        {
            return _CompanyDTO.RangeDetail;
        }
        set
        {
            _CompanyDTO.RangeDetail = value;
        }
    }

    public string Division
    {
        get
        {
            return _CompanyDTO.Division;
        }
        set
        {
            _CompanyDTO.Division = value;
        }
    }

    public string CommissionRate
    {
        get
        {
            return _CompanyDTO.CommissionRate;
        }
        set
        {
            _CompanyDTO.CommissionRate = value;
        }
    }
    public string ServiceEmailId
    {
        get
        {
            return _CompanyDTO.ServiceEmailId;
        }
        set
        {
            _CompanyDTO.ServiceEmailId = value;
        }
    }
    public string BankName
    {
        get
        {
            return _CompanyDTO.BankName;
        }
        set
        {
            _CompanyDTO.BankName = value;
        }
    }
    public string ACNo
    {
        get
        {
            return _CompanyDTO.ACNo;
        }
        set
        {
            _CompanyDTO.ACNo = value;
        }
    }
    public string BranchName
    {
        get
        {
            return _CompanyDTO.BranchName;
        }
        set
        {
            _CompanyDTO.BranchName = value;
        }
    }
    public string IFSCCode
    {
        get
        {
            return _CompanyDTO.IFSCCode;
        }
        set
        {
            _CompanyDTO.IFSCCode = value;
        }
    }
    public DataTable Locs()
    {
        try
        {
            return _generalDAL.Locs();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string LgrId
    {
        get
        {
            return _CompanyDTO.LgrId;
        }
        set
        {
            _CompanyDTO.LgrId = value;
        }
    }

    public string LgrName
    {
        get
        {
            return _CompanyDTO.LgrName;
        }
        set
        {
            _CompanyDTO.LgrName = value;
        }
    }

    public string LocId
    {
        get
        {
            return _CompanyDTO.LocId;
        }
        set
        {
            _CompanyDTO.LocId = value;
        }
    }

    public string DivTextListId
    {
        get
        {
            return _CompanyDTO.DivTextListId;
        }
        set
        {
            _CompanyDTO.DivTextListId = value;
        }
    }

    public string GSTNo
    {
        get
        {
            return _CompanyDTO.GSTNo;
        }
        set
        {
            _CompanyDTO.GSTNo = value;
        }
    }
    public string CountryName
    {
        get
        {
            return _CompanyDTO.CountryName;
        }
        set
        {
            _CompanyDTO.CountryName = value;
        }
    }
    public string CityName
    {
        get
        {
            return _CompanyDTO.CityName;
        }
        set
        {
            _CompanyDTO.CityName = value;
        }
    }
    public string StateName
    {
        get
        {
            return _CompanyDTO.StateName;
        }
        set
        {
            _CompanyDTO.StateName = value;
        }
    }
    public DataTable getState()
    {
        try
        {
            return _generalDAL.getState(_CompanyDTO.Country);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getCity()
    {
        try
        {
            return _generalDAL.getCity(_CompanyDTO.State);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable getCountry()
    {
        try
        {
            return _generalDAL.getCountry();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string CINNO
    {
        get
        {
            return _CompanyDTO.CINNO;
        }
        set
        {
            _CompanyDTO.CINNO = value;
        }
    }
    public string AccountTypeTextListId
    {
        get
        {
            return _CompanyDTO.AccountTypeTextListId;
        }
        set
        {
            _CompanyDTO.AccountTypeTextListId = value;
        }
    }

    public DataTable TextList(string Group)
    {
        try
        {
            return _generalDAL.TextList(Group);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
