﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Net.Mail;
using System.Configuration;
using System.Text;
//using System.Windows.Forms;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CandidateFinalBll
/// </summary>
public class CandidateFinalBll
{
    private DateTime? _FromDt;
    private DateTime? _ToDt;
    private string _Name;
    private string _UserId;
    private string _Status;

    private CandidateFinalDAL _candidateFinalDAL;

    public CandidateFinalBll()
    {
        _candidateFinalDAL = new CandidateFinalDAL();
    }
    ~CandidateFinalBll()
    {
        _candidateFinalDAL = null;
    }

    public DateTime? FromDt
    {
        set
        {
            _FromDt = value;
        }
    }
    public DateTime? ToDt
    {
        set
        {
            _ToDt = value;
        }
    }
    public string Name
    {
        set
        {
            _Name = value;
        }
    }
    public string UserId
    {
        set
        {
            _UserId = value;
        }
    }
    public string Status
    {
        set
        {
            _Status = value;
        }
    }
    public DataTable CandidateFinal()
    {
        return _candidateFinalDAL.CandidateFinal(_FromDt, _ToDt, _Name, _UserId, _Status);
    }
    public DataTable CandidateFinalNot()
    {
        return _candidateFinalDAL.CandidateFinalNot(_FromDt, _ToDt, _Name, _UserId);
    }
    public DataTable LoadDepartment()
    {
        try
        {
            GeneralDAL _GeneralDAL = new GeneralDAL();
            return _GeneralDAL.TextList("Standard");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadDesignation()
    {
        try
        {
            return _candidateFinalDAL.Designation();
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
            GeneralDAL _GeneralDAL = new GeneralDAL();
            return _GeneralDAL.TextList("Division");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Update(ArrayList alAccount)
    {
        try
        {
            _candidateFinalDAL.Update(alAccount);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    

}