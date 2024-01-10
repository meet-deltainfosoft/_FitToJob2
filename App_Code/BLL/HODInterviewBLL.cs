using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for HODInterviewBLL
/// </summary>
public class HODInterviewBLL
{
	HODInterviewDAL _HODInterviewDAL;
    GeneralDAL _generalDAL;
    HODInterviewDTO _HODInterviewDTO;
    HODInterviewLnsBLL _HODInterviewLnsBLL;

    public HODInterviewBLL()
    {
        _HODInterviewDAL = new HODInterviewDAL();
        _generalDAL = new GeneralDAL();
        _HODInterviewDTO = new HODInterviewDTO();
        _HODInterviewLnsBLL = new HODInterviewLnsBLL();

        _HODInterviewDTO.IsNew = true;
    }
    public HODInterviewBLL(string InterviewId)
        : this()
    {
        _HODInterviewDTO.IsNew = false;
        Load(InterviewId);
    }
    ~HODInterviewBLL()
    {
        _HODInterviewDAL = null;
        _HODInterviewLnsBLL = null;
    }

    public string HODInterviewId
    {
        get
        {
            return _HODInterviewDTO.HODInterviewId;
        }
        set
        {
            _HODInterviewDTO.HODInterviewId = value;
        }
    }
    public string CareerId
    {
        get
        {
            return _HODInterviewDTO.CareerId;
        }
        set
        {
            _HODInterviewDTO.CareerId = value;
        }
    }
    public string Name
    {
        get
        {
            return _HODInterviewDTO.Name;
        }
        set
        {
            _HODInterviewDTO.Name = value;
        }
    }
    public string UserId
    {
        get
        {
            return _HODInterviewDTO.UserId;
        }
        set
        {
            _HODInterviewDTO.UserId = value;
        }
    }
    public string UserName
    {
        get
        {
            return _HODInterviewDTO.UserName;
        }
        set
        {
            _HODInterviewDTO.UserName = value;
        }
    }
    public string Status
    {
        get
        {
            return _HODInterviewDTO.Status;
        }
        set
        {
            _HODInterviewDTO.Status = value;
        }
    }
    public string Remarks
    {
        get
        {
            return _HODInterviewDTO.Remarks;
        }
        set
        {
            _HODInterviewDTO.Remarks = value;
        }
    }
    public DateTime? Dt
    {
        get
        {
            return _HODInterviewDTO.Dt;
        }
        set
        {
            _HODInterviewDTO.Dt = value;
        }
    }
    public bool IsNew
    {
        get
        {
            return _HODInterviewDTO.IsNew;
        }
    }
    public SortedList Validate()
    {
        try
        {
            SortedList sl = new SortedList();

            if (_HODInterviewDTO.Name == null)
            {
                sl.Add("Name", "Name cannot be blank.");
            }
            if (_HODInterviewDTO.Status == null)
            {
                sl.Add("Status", "Status cannot be blank.");
            }
            if (_HODInterviewDTO.Dt == null)
            {
                sl.Add("Dt", "Select Date.");
            }

            if (_HODInterviewLnsBLL.Count > 0)
            {
                foreach (HODInterviewLnBLL HODInterviewLnBLL in _HODInterviewLnsBLL)
                {
                    if (HODInterviewLnBLL.QueTextListId != null)
                    {
                        if (HODInterviewLnBLL.HODObtainedMarks != null)
                        {
                            if (HODInterviewLnBLL.HODObtainedMarks > HODInterviewLnBLL.ActualMarks)
                            {
                                sl.Add("ObtainedMarks" + HODInterviewLnBLL.LnNo + "", "You cannot take marks more than actual marks In Line No:" + HODInterviewLnBLL.LnNo + "");
                            }
                        }
                    }
                }
            }
            return sl;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void Load(string HODInterviewId)
    {
        try
        {
            ArrayList alLnsDTO = new ArrayList();

            _HODInterviewDTO = _HODInterviewDAL.Select(HODInterviewId, out alLnsDTO);

            foreach (HODInterviewLnDTO HODInterviewLnDTO in alLnsDTO)
            {
                HODInterviewLnBLL HODInterviewLnBLL = new HODInterviewLnBLL();
                HODInterviewLnBLL.SetState(HODInterviewLnDTO);
                _HODInterviewLnsBLL.Add(HODInterviewLnBLL);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public string Save()
    {
        try
        {
            ArrayList al = new ArrayList();

            foreach (HODInterviewLnBLL HODInterviewLnBLL in _HODInterviewLnsBLL)
            {
                HODInterviewLnDTO HODInterviewLnDTO = new HODInterviewLnDTO();
                HODInterviewLnDTO = HODInterviewLnBLL.GetState();
                al.Add(HODInterviewLnDTO);
            }

            if (_HODInterviewDTO.IsNew == true)
            {
                _HODInterviewDTO.HODInterviewId = _HODInterviewDAL.Insert(_HODInterviewDTO, al);
            }
            else
            {
                _HODInterviewDAL.Update(_HODInterviewDTO, al);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Authorized Interview cannot be updated.")
                throw ex;
            else
                throw new Exception(ex.Message);
        }

        return _HODInterviewDTO.HODInterviewId;
    }
    public void Delete(string HODInterviewId)
    {
        try
        {
            try
            {
                _HODInterviewDAL.Delete(HODInterviewId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "You do not have rights to delete this entry.")
                throw ex;
            else
                throw new Exception(ex.Message);
        }
    }
    //public DataTable LoadSalaryCalculationMethod()
    //{
    //    try
    //    {
    //        return _generalDAL.LoadSalaryCalculationMethod();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}
    public decimal? CTC
    {
        get
        {
            return _HODInterviewDTO.CTC;
        }
        set
        {
            _HODInterviewDTO.CTC = value;
        }
    }
    public decimal? ViewMonthlyGrossSalary
    {
        get
        {
            return _HODInterviewDTO.ViewMonthlyGrossSalary;
        }
        set
        {
            _HODInterviewDTO.ViewMonthlyGrossSalary = value;
        }
    }
    public decimal? ViewMonthlyBasic
    {
        get
        {
            return _HODInterviewDTO.ViewMonthlyBasic;
        }
        set
        {
            _HODInterviewDTO.ViewMonthlyBasic = value;
        }
    }
    public decimal? ViewMonthlyHRA
    {
        get
        {
            return _HODInterviewDTO.ViewMonthlyHRA;
        }
        set
        {
            _HODInterviewDTO.ViewMonthlyHRA = value;
        }
    }
    public decimal? Conveyance
    {
        get
        {
            return _HODInterviewDTO.Conveyance;
        }
        set
        {
            _HODInterviewDTO.Conveyance = value;
        }
    }
    public decimal? SpecialAllowances
    {
        get
        {
            return _HODInterviewDTO.SpecialAllowances;
        }
        set
        {
            _HODInterviewDTO.SpecialAllowances = value;
        }
    }
    public decimal? ViewMonthlyPFCmpnyShare13Point61Per
    {
        get
        {
            return _HODInterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per;
        }
        set
        {
            _HODInterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per = value;
        }
    }
    public decimal? ViewMonthlyESIEmpShare4Point75Per
    {
        get
        {
            return _HODInterviewDTO.ViewMonthlyESIEmpShare4Point75Per;
        }
        set
        {
            _HODInterviewDTO.ViewMonthlyESIEmpShare4Point75Per = value;
        }
    }
    public bool? IsDeductPF
    {
        get
        {
            return _HODInterviewDTO.IsDeductPF;
        }
        set
        {
            _HODInterviewDTO.IsDeductPF = value;
        }
    }
    public bool? IsDeductESI
    {
        get
        {
            return _HODInterviewDTO.IsDeductESI;
        }
        set
        {
            _HODInterviewDTO.IsDeductESI = value;
        }
    }
    public string SalStructureId
    {
        get
        {
            return _HODInterviewDTO.SalStructureId;
        }
        set
        {
            _HODInterviewDTO.SalStructureId = value;
        }
    }
    public DataTable GetMonthyBasicAndHRA(string Name, string SalStructureId, decimal? CTC, bool? DeductPF, bool? DeductESIC, decimal? MonthlyBasic)
    {
        try
        {
            return _HODInterviewDAL.GetMonthyBasicAndHRA(Name, SalStructureId, CTC, DeductPF, DeductESIC, MonthlyBasic);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public string GetSalStructureIdfromCTC(decimal? CTC)
    {
        try
        {
            return _HODInterviewDAL.GetSalStructureIdfromCTC(CTC);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetQuestion(string RegistrationId)
    {
        try
        {
            return _HODInterviewDAL.TextList("InterviewQuestion", "Selected", RegistrationId);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public HODInterviewLnsBLL HODInterviewLnsBLL
    {
        get
        {
            return _HODInterviewLnsBLL;
        }
        set
        {
            _HODInterviewLnsBLL = value;
        }
    }
}