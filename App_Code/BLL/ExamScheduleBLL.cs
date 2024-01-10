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

public class ExamScheduleBLL
{
    private ExamScheduleDTO _examScheduleDTO;
    private ExamScheduleDAL _examScheduleDAL;

    public ExamScheduleBLL()
    {
        _examScheduleDAL = new ExamScheduleDAL();
        _examScheduleDTO = new ExamScheduleDTO();

        _examScheduleDTO.IsNew = true;
    }
    public ExamScheduleBLL(string ExamScheduleId)
        : this()
    {
        _examScheduleDTO.IsNew = false;
        Load(ExamScheduleId);
    }

    ~ExamScheduleBLL()
    {
        _examScheduleDAL = null;
    }

    public string ExamScheduleId
    {
        get
        {
            return _examScheduleDTO.ExamScheduleId;
        }
        set
        {
            _examScheduleDTO.ExamScheduleId = value;
        }
    }

    public string StandardTextListId
    {
        get
        {
            return _examScheduleDTO.StandardTextListId;
        }
        set
        {
            _examScheduleDTO.StandardTextListId = value;
        }
    }

    public string DivisionTextListId
    {
        get
        {
            return _examScheduleDTO.DivisionTextListId;
        }
        set
        {
            _examScheduleDTO.DivisionTextListId = value;
        }
    }

    public string SubId
    {
        get
        {
            return _examScheduleDTO.SubId;
        }
        set
        {
            _examScheduleDTO.SubId = value;
        }
    }

    public string TestId
    {
        get
        {
            return _examScheduleDTO.TestId;
        }
        set
        {
            _examScheduleDTO.TestId = value;
        }
    }

    public string SkipMobile
    {
        get
        {
            return _examScheduleDTO.SkipMobile;
        }
        set
        {
            _examScheduleDTO.SkipMobile = value;
        }
    }

    public int? TotalQuestions
    {
        get
        {
            return _examScheduleDTO.TotalQuestions;
        }
        set
        {
            _examScheduleDTO.TotalQuestions = value;
        }
    }

    public DateTime? ExamDate
    {
        get
        {
            return _examScheduleDTO.ExamDate;
        }
        set
        {
            _examScheduleDTO.ExamDate = value;
        }
    }

    public DateTime? ExamFromTime
    {
        get
        {
            return _examScheduleDTO.ExamFromTime;
        }
        set
        {
            _examScheduleDTO.ExamFromTime = value;
        }
    }

    public DateTime? ExamToTime
    {
        get
        {
            return _examScheduleDTO.ExamToTime;
        }
        set
        {
            _examScheduleDTO.ExamToTime = value;
        }
    }

    public int? TotalMins
    {
        get
        {
            return _examScheduleDTO.TotalMins;
        }
        set
        {
            _examScheduleDTO.TotalMins = value;
        }
    }

    public decimal? PerQueMins
    {
        get
        {
            return _examScheduleDTO.PerQueMins;
        }
        set
        {
            _examScheduleDTO.PerQueMins = value;
        }
    }

    public DataTable dtRestrations
    {
        get
        {
            return _examScheduleDTO.dtRestrations;
        }
        set
        {
            _examScheduleDTO.dtRestrations = value;
        }
    }

    public bool IsDefaultTest
    {
        get
        {
            return _examScheduleDTO.IsDefaultTest;
        }
        set
        {
            _examScheduleDTO.IsDefaultTest = value;
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_examScheduleDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank.");
        }

        if (_examScheduleDTO.SubId == null && _examScheduleDTO.PatternId == null)
        {
            sl.Add("SubId", "Please Select Subject OR Pattern.");
        }

        if (_examScheduleDTO.TestId == null)
        {
            sl.Add("TestId", "TestId cannot be blank.");
        }

        if (_examScheduleDTO.TotalQuestions == null)
        {
            sl.Add("TotalQuestions", "TotalQuestions cannot be blank.");
        }

        if (_examScheduleDTO.ExamDate == null)
        {
            sl.Add("ExamDate", "ExamDate cannot be blank.");
        }

        if (_examScheduleDTO.ExamFromTime == null)
        {
            sl.Add("ExamFromTime", "ExamFromTime cannot be blank.");
        }

        if (_examScheduleDTO.ExamToTime == null)
        {
            sl.Add("ExamToTime", "ExamToTime cannot be blank.");
        }

        if (_examScheduleDTO.TotalMins == null)
        {
            sl.Add("TotalMins", "TotalMins cannot be blank.");
        }

        if (_examScheduleDTO.PerQueMins == null)
        {
            sl.Add("PerQueMins", "PerQueMins cannot be blank.");
        }

        //if (_examScheduleDTO.dtRestrations == null)
        //{
        //    sl.Add("dtRestrations", "You must have to select student");
        //}
        //else if (_examScheduleDTO.dtRestrations.Rows.Count == 0)
        //{
        //    sl.Add("dtRestrations", "You must have to select student");
        //}

        //if (_examScheduleDTO.IsNew)
        //{
        //    if (_examScheduleDTO.SkipMobile == null)
        //    {
        //        sl.Add("SkipMobile", "You must have to select alteast one student to generate this exam schedule.");
        //    }
        //}

        if (_examScheduleDTO.TotalQuestions != null && _examScheduleDTO.TotalMins != null)
        {
            if (_examScheduleDTO.PerQuestionTime == true)
            {
                if (_examScheduleDTO.TotalMins < _examScheduleDTO.TotalQuestions)
                {
                    sl.Add("QuestionsTime", "Exam minutes must be greater than or equal to Total Questions.");
                }
            }
        }

        if (_examScheduleDTO.StandardTextListId != null)
        {
            if (_examScheduleDTO.dtRestrations != null)
            {
                if (_examScheduleDTO.dtRestrations.Rows.Count > 0)
                {
                    for (int i = 0; i < _examScheduleDTO.dtRestrations.Rows.Count; i++)
                    {
                        if (_examScheduleDTO.dtRestrations.Rows[i]["StandardId"] != DBNull.Value)
                        {
                            if (_examScheduleDTO.StandardTextListId.ToString().ToUpper() != _examScheduleDTO.dtRestrations.Rows[i]["StandardId"].ToString().ToUpper())
                            {
                                sl.Add("StandardTextListId1", "Selected standard and selected student is not belongs from same standard.");
                                break;
                            }
                        }
                    }
                }
            }
        }

        //if (_examScheduleDTO.PerQuestionTime == true)
        //{
        //    _examScheduleDTO.AllowReview = false;
        //}
        return sl;
    }

    public void Save()
    {
        try
        {
            if (_examScheduleDTO.IsNew)
                _examScheduleDAL.Insert(_examScheduleDTO);
            else
                _examScheduleDAL.UpdateExamSchedules(_examScheduleDTO);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Load(string ExamScheduleId)
    {
        ArrayList alSubLns = new ArrayList();
        _examScheduleDTO = _examScheduleDAL.Select(ExamScheduleId);
    }

    public void Delete(string ExamScheduleId)
    {
        string isReferenced = _examScheduleDAL.IsReferenced(ExamScheduleId);

        try
        {
            if (isReferenced != "")
            {
                throw new Exception("Exam Schedule is Referenced. Cannot Delete." + "\n" + isReferenced);
            }
            else
            {
                _examScheduleDAL.Delete(ExamScheduleId);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Application Role is Referenced. Cannot Delete." + "\n" + isReferenced)
                throw ex;
            else
                throw new Exception(ex.Message);
        }
    }

    public DataTable LoadStandard()
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            return _generalDAL.TextList("Standard");
            //return _generalDAL.TextList("StafCategory");
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
            GeneralDAL _generalDAL = new GeneralDAL();
            return _generalDAL.TextList("Division");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //public DataTable LoadSubjects()
    //{
    //    try
    //    {
    //        return _examScheduleDAL.LoadSubjects();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
    public DataTable LoadSubjects()
    {
        try
        {
            return _examScheduleDAL.LoadSubjects(_examScheduleDTO.StandardTextListId);
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
            return _examScheduleDAL.LoadTest(_examScheduleDTO.SubId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable LoadQuestions()
    {
        try
        {
            return _examScheduleDAL.LoadQuestions(_examScheduleDTO.SubId, _examScheduleDTO.TestId, _examScheduleDTO.PatternId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadExamsOnSameDate()
    {
        try
        {
            return _examScheduleDAL.LoadExamsOnSameDate(_examScheduleDTO.ExamDate, _examScheduleDTO.StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadStudent()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = _examScheduleDAL.LoadStudent(_examScheduleDTO.StandardTextListId, _examScheduleDTO.DivisionTextListId);

            _examScheduleDTO.dtRestrations = dt;

            return _examScheduleDTO.dtRestrations;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadScheduleList(string StandardId, string SubId, string TestId, DateTime? fromExamDate, DateTime? toExamDate)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = _examScheduleDAL.LoadScheduleList(StandardId, SubId, TestId, fromExamDate, toExamDate);
            return dt;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable CommonOTP()
    {
        //dtRestrations.Rows[i]["RegistrationId"] = RegistrationId
        return _examScheduleDAL.CommonOTP();
    }

    public DataTable CommonOTPTeacher()
    {
        return _examScheduleDAL.CommonOTPTeacher();
    }
    //BY BM For Update Minimum Passing Marks
    public DataTable MinimumMarks()
    {
        return _examScheduleDAL.MinimumMarks();
    }
    public void UpdateMinMarks(string MinMarks, string id)
    {
        _examScheduleDAL.UpdateMinMarks(MinMarks, id);
    }
    //End
    public void UpdateCommonOTP(string otp, string id)
    {
        _examScheduleDAL.UpdateCommonOTP(otp, id);
    }


    public bool NegativeMarks
    {
        get
        {
            return _examScheduleDTO.NegativeMarks;
        }
        set
        {
            _examScheduleDTO.NegativeMarks = value;
        }
    }

    public bool PerQuestionTime
    {
        get
        {
            return _examScheduleDTO.PerQuestionTime;
        }
        set
        {
            _examScheduleDTO.PerQuestionTime = value;
        }
    }

    public bool AllowReview
    {
        get
        {
            return _examScheduleDTO.AllowReview;
        }
        set
        {
            _examScheduleDTO.AllowReview = value;
        }
    }

    public bool ShowResult
    {
        get
        {
            return _examScheduleDTO.ShowResult;
        }
        set
        {
            _examScheduleDTO.ShowResult = value;
        }
    }

    public int? MinsforResultShow
    {
        get
        {
            return _examScheduleDTO.MinsforResultShow;
        }
        set
        {
            _examScheduleDTO.MinsforResultShow = value;
        }
    }
    public string PatternId
    {
        get
        {
            return _examScheduleDTO.PatternId;
        }
        set
        {
            _examScheduleDTO.PatternId = value;
        }
    }
    public DataTable LoadPatterns()
    {
        try
        {
            return _examScheduleDAL.LoadPatterns(_examScheduleDTO.StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadTestFromPatterns()
    {
        try
        {
            return _examScheduleDAL.LoadTestFromPatterns(_examScheduleDTO.PatternId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public bool SendNotification
    {
        get
        {
            return _examScheduleDTO.SendNotification;
        }
        set
        {
            _examScheduleDTO.SendNotification = value;
        }
    }
}
