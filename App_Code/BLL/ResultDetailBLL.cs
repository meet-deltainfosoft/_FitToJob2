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

public class ResultDetailBLL
{
    public string _StandardId;
    public string _MobileNo;
    public string _SubjectId;
    public string _TestId;
    public string _examScheduleId;
    public DateTime? _FromScheduleDt;
    public DateTime? _ToScheduleDt;
    public string _StudentName;
    public bool _IsExcel;

    private ResultDetailDAL _ResultDetailDAL;

    public ResultDetailBLL()
    {
        _ResultDetailDAL = new ResultDetailDAL();
    }
    ~ResultDetailBLL()
    {
        _ResultDetailDAL = null;
    }

    public DateTime? FromScheduleDt
    {
        set
        {
            _FromScheduleDt = value;
        }
    }
    public DateTime? ToScheduleDt
    {
        set
        {
            _ToScheduleDt = value;
        }
    }
    public string MobileNo
    {
        set
        {
            _MobileNo = value;
        }
    }
    public string StandardId
    {
        set
        {
            _StandardId = value;
        }
    }
    public string SubjectId
    {
        get
        {
            return _SubjectId;
        }
        set
        {
            _SubjectId = value;
        }
    }
    public string TestId
    {
        get
        {
            return _TestId;
        }
        set
        {
            _TestId = value;
        }
    }
    public string StudentName
    {
        set
        {
            _StudentName = value;
        }
    }
    public string ExamScheduleId
    {
        get
        {
            return _examScheduleId;
        }
        set
        {
            _examScheduleId = value;
        }
    }

    public bool IsExcel
    {
        get
        {
            return _IsExcel;
        }
        set
        {
            _IsExcel = value;
        }
    }


    public DataTable LoadStandard()
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

    public DataTable LoadSChedule()
    {
        try
        {
            GeneralDAL _GeneralDAL = new GeneralDAL();
            return _ResultDetailDAL.LoadSchedule(_TestId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable LoadSubject()
    {
        try
        {
            return _ResultDetailDAL.LoadSubject(_StandardId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable LoadTest()
    {
        try
        {
            return _ResultDetailDAL.LoadTest(_SubjectId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetResultFinal(string RegistrationId, string ExamScheduleId)
    {
        try
        {
            return _ResultDetailDAL.GetResultFinal(RegistrationId, ExamScheduleId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetResultFinalAdv(string RegistrationId, string ExamScheduleId)
    {
        try
        {
            return _ResultDetailDAL.GetResultFinalAdv(RegistrationId, ExamScheduleId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable GetResultFinalMasterSheet(string TestId)
    {
        try
        {
            return _ResultDetailDAL.GetResultFinalMasterSheet(TestId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetExamDetails(string RegistrationId, string ExamScheduleId)
    {
        try
        {
            return _ResultDetailDAL.GetExamDetails(RegistrationId, ExamScheduleId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable GetExamDetailsAdv(string RegistrationId, string ExamScheduleId)
    {
        try
        {
            return _ResultDetailDAL.GetExamDetailsAdv(RegistrationId, ExamScheduleId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable GetExamDetailsMaster(string TestId)
    {
        try
        {
            return _ResultDetailDAL.GetExamDetailsMaster(TestId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable LoadSchedule()
    {
        try
        {
            return _ResultDetailDAL.LoadSchedule(_SubjectId, _TestId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable LiveResultDetail(string ResultType)
    {
        return _ResultDetailDAL.LiveResultDetail(_StandardId, _SubjectId, _TestId, _StudentName, _MobileNo, _examScheduleId, ResultType, _FromScheduleDt, _ToScheduleDt);
    }
    public DataTable ResultDetail()
    {
        return _ResultDetailDAL.ResultDetail(_FromScheduleDt, _ToScheduleDt, _StandardId, _SubjectId, _TestId, _StudentName, _MobileNo, _examScheduleId);
    }
    public DataTable ResultDetailAdv()
    {
        return _ResultDetailDAL.ResultDetailAdv(_FromScheduleDt, _ToScheduleDt, _StandardId, _SubjectId, _TestId, _StudentName, _MobileNo, _examScheduleId, _IsExcel);
    }
    public DataTable ERPExportData()
    {
        return _ResultDetailDAL.ERPExportData(_FromScheduleDt, _ToScheduleDt, _StandardId, _SubjectId, _TestId, _StudentName, _MobileNo, _examScheduleId, _IsExcel);
    }
    public DataTable ERPExportDataQueWise()
    {
        return _ResultDetailDAL.ERPExportDataQueWise(_FromScheduleDt, _ToScheduleDt, _StandardId, _SubjectId, _TestId, _StudentName, _MobileNo, _examScheduleId, _IsExcel);
    }
}