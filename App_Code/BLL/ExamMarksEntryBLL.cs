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

public class ExamMarksEntryBLL
{
    #region Declaration
    public string _StandardId;
    public string _MobileNo;
    public string _SubjectId;
    public string _TestId;
    public string _examScheduleId;
    public DateTime? _FromScheduleDt;
    public DateTime? _ToScheduleDt;
    public string _StudentName;
    public string _top;
    public string _PrevStandardId;
    public string _PrevSubId;
    public string _userId;
    public string _queId;
    private ExamMarksEntryDAL _examMarksEntryDAL;
    #endregion

    public ExamMarksEntryBLL()
    {
        _examMarksEntryDAL = new ExamMarksEntryDAL();
    }
    ~ExamMarksEntryBLL()
    {
        _examMarksEntryDAL = null;
    }

    #region Getter Setter
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
    public string UserId
    {
        get
        {
            return _userId;
        }
        set
        {
            _userId = value;
        }
    }
    public string QueId
    {
        get
        {
            return _queId;
        }
        set
        {
            _queId = value;
        }
    }
    public string Top
    {
        get
        {
            return _top;
        }
        set
        {
            _top = value;
        }
    }
    #endregion

    #region Load DropDown
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
            return _examMarksEntryDAL.LoadSchedule(_TestId);
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
            return _examMarksEntryDAL.LoadSubject(_StandardId);
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
            return _examMarksEntryDAL.LoadTest(_SubjectId);
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
            return _examMarksEntryDAL.LoadSchedule(_SubjectId, _TestId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region Other Functions
    public DataTable GetResultFinal(string RegistrationId, string ExamScheduleId)
    {
        try
        {
            return _examMarksEntryDAL.GetResultFinal(RegistrationId, ExamScheduleId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable ResultDetail()
    {
        try
        {
            return _examMarksEntryDAL.ResultDetail(_FromScheduleDt, _ToScheduleDt, _StandardId, _SubjectId, _TestId, _StudentName, _MobileNo, _examScheduleId, _top);
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
            return _examMarksEntryDAL.GetExamDetails(RegistrationId, ExamScheduleId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //public DataTable GetResultFinalMasterSheet(string TestId)
    //{
    //    try
    //    {
    //        return _examMarksEntryDAL.GetResultFinalMasterSheet(TestId);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}

    //public DataTable GetExamDetailsMaster(string TestId)
    //{
    //    try
    //    {
    //        return _examMarksEntryDAL.GetExamDetailsMaster(TestId);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
    //public DataTable LiveResultDetail(string ResultType)
    //{
    //    try
    //    {
    //        return _examMarksEntryDAL.LiveResultDetail(_StandardId, _SubjectId, _TestId, _StudentName, _MobileNo, _examScheduleId, ResultType);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}

    #endregion

    #region Update Marks
    public void Update(ArrayList al)
    {
        try
        {
            _examMarksEntryDAL.Update(al);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    public DataTable LoadQuestions()
    {
        try
        {
            return _examMarksEntryDAL.LoadQuestions(_examScheduleId, _userId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadEmployee()
    {
        try
        {

            return _examMarksEntryDAL.LoadEmployee(_examScheduleId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadStudentWiseQuestionAns()
    {
        try
        {
            return _examMarksEntryDAL.LoadStudentWiseQuestionAns(_StandardId, _SubjectId, _TestId, _examScheduleId, _userId, _queId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}