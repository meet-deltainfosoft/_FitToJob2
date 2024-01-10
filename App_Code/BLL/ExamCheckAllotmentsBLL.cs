using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class ExamCheckAllotmentsBLL
{
    private string _standardTextListId;
    private string _subId;
    private string _testId;
    private string _examScheduleId;

    private ExamCheckAllotmentsDAL _examCheckAllotmentsDAL;
    private ExamCheckAllotmentDAL _examCheckAllotmentDAL;

    private GeneralDAL _generalDAL;

    public ExamCheckAllotmentsBLL()
    {
        _examCheckAllotmentsDAL = new ExamCheckAllotmentsDAL();
        _examCheckAllotmentDAL = new ExamCheckAllotmentDAL();
        _generalDAL = new GeneralDAL();
    }

    ~ExamCheckAllotmentsBLL()
    {
        _examCheckAllotmentsDAL = null;
        _examCheckAllotmentDAL = null;
        _generalDAL = null;
    }

    public string StandardTextListId
    {
        set
        {
            _standardTextListId = value;
        }
    }

    public string SubId
    {
        set
        {
            _subId = value;
        }
    }

    public string TestId
    {
        set
        {
            _testId = value;
        }
    }

    public string ExamScheduleId
    {
        set
        {
            _examScheduleId = value;
        }
    }

    public DataTable ExamCheckAllotments()
    {
        try
        {
            return _examCheckAllotmentsDAL.ExamCheckAllotments(_standardTextListId, _subId, _testId, _examScheduleId);
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
            GeneralDAL _GeneralDAL = new GeneralDAL();
            return _GeneralDAL.TextList("Standard");
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
            return _examCheckAllotmentDAL.LoadSchedule(_subId, _testId);
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
            return _examCheckAllotmentDAL.LoadSubject(_standardTextListId);
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
            return _examCheckAllotmentDAL.LoadTest(_subId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
