using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ExamSummaryRptBLL
/// </summary>
public class ExamSummaryRptBLL
{
	public string _StandardId;
    public string _SubjectId;
    public string _TestId;
    public string _examScheduleId;
    public DateTime? _FromScheduleDt;
    public DateTime? _ToScheduleDt;
    public bool _AllRecords;

    private ExamSummaryRptDAL _ExamSummaryRptDAL;

    public ExamSummaryRptBLL()
    {
        _ExamSummaryRptDAL = new ExamSummaryRptDAL();
    }
    ~ExamSummaryRptBLL()
    {
        _ExamSummaryRptDAL = null;
    }

    #region Getter Setter
    public DateTime? FromScheduleDt
    {
        get
        {
            return _FromScheduleDt;
        }
        set
        {
            _FromScheduleDt = value;
        }
    }
    public DateTime? ToScheduleDt
    {
        get
        {
            return _ToScheduleDt;
        }
        set
        {
            _ToScheduleDt = value;
        }
    }
    public string StandardId
    {
        get
        {
            return _StandardId;
        }
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
    public bool AllRecords
    {
        get
        {
            return _AllRecords;
        }
        set
        {
            _AllRecords = value;
        }
    }
    #endregion

    #region Load Dropdown
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
    public DataTable LoadSubject()
    {
        try
        {
            return _ExamSummaryRptDAL.LoadSubject(_StandardId);
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
            return _ExamSummaryRptDAL.LoadTest(_SubjectId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region Get Exam Summary Details
    public DataTable ExamSummaryDetail()
    {
        return _ExamSummaryRptDAL.ExamSummaryDetail(_FromScheduleDt, _ToScheduleDt, _StandardId, _SubjectId, _TestId, _AllRecords);
    }
    #endregion

    #region Get Exam  Details
    public DataTable ExamDetail()
    {
        return _ExamSummaryRptDAL.ExamDetail(_StandardId, _SubjectId, _TestId, _examScheduleId);
    }
    #endregion
}