using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for InterviewBLL
/// </summary>
public class InterviewBLL
{
    InterviewDAL _InterviewDAL;
    GeneralDAL _generalDAL;
    InterviewDTO _InterviewDTO;
    InterviewLnsBLL _InterviewLnsBLL;

    public InterviewBLL()
    {
        _InterviewDAL = new InterviewDAL();
        _generalDAL = new GeneralDAL();
        _InterviewDTO = new InterviewDTO();
        _InterviewLnsBLL = new InterviewLnsBLL();

        _InterviewDTO.IsNew = true;
    }
    public InterviewBLL(string InterviewId)
        : this()
    {
        _InterviewDTO.IsNew = false;
        Load(InterviewId);
    }
    ~InterviewBLL()
    {
        _InterviewDAL = null;
        _InterviewLnsBLL = null;
    }

    public string InterviewId
    {
        get
        {
            return _InterviewDTO.InterviewId;
        }
        set
        {
            _InterviewDTO.InterviewId = value;
        }
    }
    public string CareerId
    {
        get
        {
            return _InterviewDTO.CareerId;
        }
        set
        {
            _InterviewDTO.CareerId = value;
        }
    }
    public string Name
    {
        get
        {
            return _InterviewDTO.Name;
        }
        set
        {
            _InterviewDTO.Name = value;
        }
    }
    public string UserId
    {
        get
        {
            return _InterviewDTO.UserId;
        }
        set
        {
            _InterviewDTO.UserId = value;
        }
    }
    public string UserName
    {
        get
        {
            return _InterviewDTO.UserName;
        }
        set
        {
            _InterviewDTO.UserName = value;
        }
    }
    public string Status
    {
        get
        {
            return _InterviewDTO.Status;
        }
        set
        {
            _InterviewDTO.Status = value;
        }
    }
    public string Remarks
    {
        get
        {
            return _InterviewDTO.Remarks;
        }
        set
        {
            _InterviewDTO.Remarks = value;
        }
    }
    public DateTime? Dt
    {
        get
        {
            return _InterviewDTO.Dt;
        }
        set
        {
            _InterviewDTO.Dt = value;
        }
    }
    public bool IsNew
    {
        get
        {
            return _InterviewDTO.IsNew;
        }
    }
    public SortedList Validate()
    {
        try
        {
            SortedList sl = new SortedList();

            if (_InterviewDTO.Name == null)
            {
                sl.Add("Name", "Name cannot be blank.");
            }
            if (_InterviewDTO.Status == null)
            {
                sl.Add("Status", "Status cannot be blank.");
            }
            if (_InterviewDTO.Dt == null)
            {
                sl.Add("Dt", "Select Date.");
            }

            if (_InterviewLnsBLL.Count > 0)
            {
                foreach (InterviewLnBLL InterviewLnBLL in _InterviewLnsBLL)
                {
                    if (InterviewLnBLL.QueTextListId != null)
                    {
                        if (InterviewLnBLL.ObtainedMarks != null)
                        {
                            if (InterviewLnBLL.ObtainedMarks > InterviewLnBLL.ActualMarks)
                            {
                                sl.Add("ObtainedMarks" + InterviewLnBLL.LnNo + "", "You cannot take marks more than actual marks In Line No:" + InterviewLnBLL.LnNo + "");
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
    public void Load(string InterviewId)
    {
        try
        {
            ArrayList alLnsDTO = new ArrayList();

            _InterviewDTO = _InterviewDAL.Select(InterviewId, out alLnsDTO);

            foreach (InterviewLnDTO InterviewLnDTO in alLnsDTO)
            {
                InterviewLnBLL InterviewLnBLL = new InterviewLnBLL();
                InterviewLnBLL.SetState(InterviewLnDTO);
                _InterviewLnsBLL.Add(InterviewLnBLL);
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

            foreach (InterviewLnBLL InterviewLnBLL in _InterviewLnsBLL)
            {
                InterviewLnDTO InterviewLnDTO = new InterviewLnDTO();
                InterviewLnDTO = InterviewLnBLL.GetState();
                al.Add(InterviewLnDTO);
            }

            if (_InterviewDTO.IsNew == true)
            {
                _InterviewDTO.InterviewId = _InterviewDAL.Insert(_InterviewDTO, al);
            }
            else
            {
                _InterviewDAL.Update(_InterviewDTO, al);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Authorized Interview cannot be updated.")
                throw ex;
            else
                throw new Exception(ex.Message);
        }

        return _InterviewDTO.InterviewId;
    }
    public void Delete(string InterviewId)
    {
        try
        {
            try
            {
                _InterviewDAL.Delete(InterviewId);
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
   
    public decimal? CTC
    {
        get
        {
            return _InterviewDTO.CTC;
        }
        set
        {
            _InterviewDTO.CTC = value;
        }
    }
    public decimal? ViewMonthlyGrossSalary
    {
        get
        {
            return _InterviewDTO.ViewMonthlyGrossSalary;
        }
        set
        {
            _InterviewDTO.ViewMonthlyGrossSalary = value;
        }
    }
    public decimal? ViewMonthlyBasic
    {
        get
        {
            return _InterviewDTO.ViewMonthlyBasic;
        }
        set
        {
            _InterviewDTO.ViewMonthlyBasic = value;
        }
    }
    public decimal? ViewMonthlyHRA
    {
        get
        {
            return _InterviewDTO.ViewMonthlyHRA;
        }
        set
        {
            _InterviewDTO.ViewMonthlyHRA = value;
        }
    }
    public decimal? Conveyance
    {
        get
        {
            return _InterviewDTO.Conveyance;
        }
        set
        {
            _InterviewDTO.Conveyance = value;
        }
    }
    public decimal? SpecialAllowances
    {
        get
        {
            return _InterviewDTO.SpecialAllowances;
        }
        set
        {
            _InterviewDTO.SpecialAllowances = value;
        }
    }
    public decimal? ViewMonthlyPFCmpnyShare13Point61Per
    {
        get
        {
            return _InterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per;
        }
        set
        {
            _InterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per = value;
        }
    }
    public decimal? ViewMonthlyESIEmpShare4Point75Per
    {
        get
        {
            return _InterviewDTO.ViewMonthlyESIEmpShare4Point75Per;
        }
        set
        {
            _InterviewDTO.ViewMonthlyESIEmpShare4Point75Per = value;
        }
    }
    public bool? IsDeductPF
    {
        get
        {
            return _InterviewDTO.IsDeductPF;
        }
        set
        {
            _InterviewDTO.IsDeductPF = value;
        }
    }
    public bool? IsDeductESI
    {
        get
        {
            return _InterviewDTO.IsDeductESI;
        }
        set
        {
            _InterviewDTO.IsDeductESI = value;
        }
    }
    public string SalStructureId
    {
        get
        {
            return _InterviewDTO.SalStructureId;
        }
        set
        {
            _InterviewDTO.SalStructureId = value;
        }
    }
    public DataTable GetMonthyBasicAndHRA(string Name, string SalStructureId, decimal? CTC, bool? DeductPF, bool? DeductESIC, decimal? MonthlyBasic)
    {
        try
        {
            return _InterviewDAL.GetMonthyBasicAndHRA(Name, SalStructureId, CTC, DeductPF, DeductESIC, MonthlyBasic);
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
            return _InterviewDAL.GetSalStructureIdfromCTC(CTC);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public DataTable GetQuestion()
    {
        try
        {
            return _InterviewDAL.TextList("InterviewQuestion");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public InterviewLnsBLL InterviewLnsBLL
    {
        get
        {
            return _InterviewLnsBLL;
        }
        set
        {
            _InterviewLnsBLL = value;
        }
    }
}