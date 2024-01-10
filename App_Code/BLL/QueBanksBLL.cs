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

public class QueBanksBLL
{
    public string _ChapterId;
    public string _StandardId;
    public string _SubId;
    public string _Que;
    public string _PeriodNo;
    public bool _AllRecord;

    private QueBanksDAL _QueBanksDAL;

    public QueBanksBLL()
    {
        _QueBanksDAL = new QueBanksDAL();
    }
    ~QueBanksBLL()
    {
        _QueBanksDAL = null;
    }

    public string Que
    {
        set
        {
            _Que = value;
        }
    }
    public string ChapterId
    {
        set
        {
            _ChapterId = value;
        }
    }
    public string StandardId
    {
        set
        {
            _StandardId = value;
        }
    }
    public string SubId
    {
        set
        {
            _SubId = value;
        }
    }

    public string PeriodNo
    {
        set
        {
            _PeriodNo = value;
        }
    }
    public bool AllRecord
    {
        set
        {
            _AllRecord = value;
        }
    }
    public DataTable Filter()
    {
        try
        {
            return _QueBanksDAL.QueBank(_Que, _StandardId, _SubId, _ChapterId, _PeriodNo, _AllRecord);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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

    public DataTable LoadPeriodNo()
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            return _generalDAL.LoadPeriodNo(_SubId, _ChapterId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}