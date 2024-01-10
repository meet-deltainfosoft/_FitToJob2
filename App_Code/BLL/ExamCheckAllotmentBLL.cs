using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

public class ExamCheckAllotmentBLL
{
    private ExamCheckAllotmentDTO _examCheckAllotmentDTO;
    private ExamCheckAllotmentDAL _examCheckAllotmentDAL;
    private GeneralDAL _generalDAL;

    public ExamCheckAllotmentBLL()
    {
        _examCheckAllotmentDAL = new ExamCheckAllotmentDAL();
        _examCheckAllotmentDTO = new ExamCheckAllotmentDTO();
        _generalDAL = new GeneralDAL();

        _examCheckAllotmentDTO.IsNew = true;
    }

    public ExamCheckAllotmentBLL(string textListId)
        : this()
    {
        _examCheckAllotmentDTO.IsNew = false;
        Load(textListId);
    }

    ~ExamCheckAllotmentBLL()
    {
        _examCheckAllotmentDAL = null;
        _examCheckAllotmentDTO = null;
        _generalDAL = null;
    }

    public string ExamCheckAllotmentId
    {
        get
        {
            return _examCheckAllotmentDTO.ExamCheckAllotmentId;
        }
        set
        {
            _examCheckAllotmentDTO.ExamCheckAllotmentId = value;
        }
    }

    public string StandardTextListId
    {
        get
        {
            return _examCheckAllotmentDTO.StandardTextListId;
        }
        set
        {
            _examCheckAllotmentDTO.StandardTextListId = value;
        }
    }

    public string SubId
    {
        get
        {
            return _examCheckAllotmentDTO.SubId;
        }
        set
        {
            _examCheckAllotmentDTO.SubId = value;
        }
    }

    public string TestId
    {
        get
        {
            return _examCheckAllotmentDTO.TestId;
        }
        set
        {
            _examCheckAllotmentDTO.TestId = value;
        }
    }

    public string ExamScheduleId
    {
        get
        {
            return _examCheckAllotmentDTO.ExamScheduleId;
        }
        set
        {
            _examCheckAllotmentDTO.ExamScheduleId = value;
        }
    }

    public DataTable dtExamCheckAllotmentLns
    {
        get
        {
            return _examCheckAllotmentDTO.dtExamCheckAllotmentLns;
        }
        set
        {
            _examCheckAllotmentDTO.dtExamCheckAllotmentLns = value;
        }
    }

    public bool IsNew
    {
        get
        {
            return _examCheckAllotmentDTO.IsNew;
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_examCheckAllotmentDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank.");
        }

        if (_examCheckAllotmentDTO.SubId == null)
        {
            sl.Add("SubId", " Designation cannot be blank.");
        }

        if (_examCheckAllotmentDTO.TestId == null)
        {
            sl.Add("TestId", "Test cannot be blank.");
        }

        if (_examCheckAllotmentDTO.ExamScheduleId == null)
        {
            sl.Add("ExamScheduleId", "Exam schedule cannot be blank.");
        }

        if (_examCheckAllotmentDTO.StandardTextListId != null
            && _examCheckAllotmentDTO.SubId != null
            && _examCheckAllotmentDTO.TestId != null
            && _examCheckAllotmentDTO.ExamScheduleId != null)
        {
            if (_examCheckAllotmentDTO.IsNew ? _examCheckAllotmentDAL.DataExists(_examCheckAllotmentDTO.StandardTextListId,
                _examCheckAllotmentDTO.SubId,
                _examCheckAllotmentDTO.TestId,
                _examCheckAllotmentDTO.ExamScheduleId) : _examCheckAllotmentDAL.DataExists(_examCheckAllotmentDTO.StandardTextListId,
                _examCheckAllotmentDTO.SubId,
                _examCheckAllotmentDTO.TestId,
                _examCheckAllotmentDTO.ExamScheduleId, _examCheckAllotmentDTO.ExamCheckAllotmentId) == true)
            {
                sl.Add("Data", "Exam Checking Allotment is already exists.");
            }
        }

        return sl;
    }

    public string Save()
    {
        try
        {

            if (_examCheckAllotmentDTO.IsNew == true)
            {
                _examCheckAllotmentDTO.ExamCheckAllotmentId = _examCheckAllotmentDAL.Insert(_examCheckAllotmentDTO);
            }
            else
            {
                _examCheckAllotmentDAL.Update(_examCheckAllotmentDTO);
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
        return _examCheckAllotmentDTO.ExamCheckAllotmentId;
    }

    public void Load(string textListId)
    {
        _examCheckAllotmentDTO = _examCheckAllotmentDAL.Select(textListId);
    }

    //public void Delete(string textListId)
    //{
    //    try
    //    {
    //        _examCheckAllotmentDAL.Delete(textListId);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}

    public DataTable LoadQuestions()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = _examCheckAllotmentDAL.LoadQuestion(_examCheckAllotmentDTO.ExamScheduleId);

            _examCheckAllotmentDTO.dtExamCheckAllotmentLns = dt;

            return _examCheckAllotmentDTO.dtExamCheckAllotmentLns;
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
            return _examCheckAllotmentDAL.LoadSchedule(_examCheckAllotmentDTO.SubId, _examCheckAllotmentDTO.TestId);
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
            return _examCheckAllotmentDAL.LoadSubject(_examCheckAllotmentDTO.StandardTextListId);
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
            return _examCheckAllotmentDAL.LoadTest(_examCheckAllotmentDTO.SubId);
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
            return _examCheckAllotmentDAL.LoadEmployee();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Delete(string ExamCheckAllotmentId)
    {
        string isReferenced = _examCheckAllotmentDAL.IsReferenced(ExamCheckAllotmentId);
        try
        {
            if (isReferenced != "")
            {
                throw new Exception("Exam checking allotment is Referenced. Cannot Delete." + "\n" + isReferenced);
            }
            else
            {
                _examCheckAllotmentDAL.Delete(ExamCheckAllotmentId);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Exam checking allotment is Referenced. Cannot Delete." + "\n" + isReferenced)
                throw ex;
            else
                throw new Exception(ex.Message);
        }
    }
}
