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

public class ExamResultPublishBLL
{
    private ExamResultPublishDTO _examResultPublishDTO;
    private ExamResultPublishDAL _examResultPublishDAL;

    public ExamResultPublishBLL()
    {
        _examResultPublishDAL = new ExamResultPublishDAL();
        _examResultPublishDTO = new ExamResultPublishDTO();

        _examResultPublishDTO.IsNew = true;
    }
    public ExamResultPublishBLL(string SubId)
        : this()
    {
        _examResultPublishDTO.IsNew = false;
        Load(SubId);
    }

    ~ExamResultPublishBLL()
    {
        _examResultPublishDAL = null;
    }

    public string ExamResultPublishId
    {
        get
        {
            return _examResultPublishDTO.ExamResultPublishId;
        }
        set
        {
            _examResultPublishDTO.ExamResultPublishId = value;
        }
    }

    public string StandardTextListId
    {
        get
        {
            return _examResultPublishDTO.StandardTextListId;
        }
        set
        {
            _examResultPublishDTO.StandardTextListId = value;
        }
    }

    public string SubId
    {
        get
        {
            return _examResultPublishDTO.SubId;
        }
        set
        {
            _examResultPublishDTO.SubId = value;
        }
    }

    public string TestId
    {
        get
        {
            return _examResultPublishDTO.TestId;
        }
        set
        {
            _examResultPublishDTO.TestId = value;
        }
    }

    public string ExamScheduleId
    {
        get
        {
            return _examResultPublishDTO.ExamScheduleId;
        }
        set
        {
            _examResultPublishDTO.ExamScheduleId = value;
        }
    }

    public string AnsKeyFilePath
    {
        get
        {
            return _examResultPublishDTO.AnsKeyFilePath;
        }
        set
        {
            _examResultPublishDTO.AnsKeyFilePath = value;
        }
    }

    public string AnsKeyFilePathShow
    {
        get
        {
            return _examResultPublishDTO.AnsKeyFilePathShow;
        }
        set
        {
            _examResultPublishDTO.AnsKeyFilePathShow = value;
        }
    }

    public bool IsResultPublished
    {
        get
        {
            return _examResultPublishDTO.IsResultPublished;
        }
        set
        {
            _examResultPublishDTO.IsResultPublished = value;
        }
    }

    public bool IsNew
    {
        get
        {
            return _examResultPublishDTO.IsNew;
        }
        set
        {
            _examResultPublishDTO.IsNew = value;
        }
    }

    public bool IsChangeFile
    {
        get
        {
            return _examResultPublishDTO.IsChangeFile;
        }
        set
        {
            _examResultPublishDTO.IsChangeFile = value;
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_examResultPublishDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank.");
        }

        if (_examResultPublishDTO.SubId == null)
        {
            sl.Add("SubId", " Designation cannot be blank.");
        }

        if (_examResultPublishDTO.TestId == null)
        {
            sl.Add("TestId", "Test cannot be blank.");
        }

        if (_examResultPublishDTO.ExamScheduleId == null)
        {
            sl.Add("ExamScheduleId", "Exam schedule cannot be blank.");
        }

        if (_examResultPublishDTO.StandardTextListId != null
            && _examResultPublishDTO.SubId != null
            && _examResultPublishDTO.TestId != null
            && _examResultPublishDTO.ExamScheduleId != null)
        {
            if (_examResultPublishDTO.IsNew ? _examResultPublishDAL.NamesExist(_examResultPublishDTO.StandardTextListId,
                _examResultPublishDTO.SubId,
                _examResultPublishDTO.TestId,
                _examResultPublishDTO.ExamScheduleId) : _examResultPublishDAL.NamesExists(_examResultPublishDTO.StandardTextListId,
                _examResultPublishDTO.SubId,
                _examResultPublishDTO.TestId,
                _examResultPublishDTO.ExamScheduleId, _examResultPublishDTO.ExamResultPublishId) == true)
            {
                sl.Add("Data", "Exam Result Publish is already exists.");
            }
        }

        return sl;
    }

    public void Save()
    {
        try
        {
            if (_examResultPublishDTO.IsNew == true)
                _examResultPublishDAL.Insert(_examResultPublishDTO);
            else
                _examResultPublishDAL.Update(_examResultPublishDTO);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString());
        }
    }
    public void Load(string SubId)
    {
        ArrayList alSubLns = new ArrayList();
        _examResultPublishDTO = _examResultPublishDAL.Select(SubId);
    }

    public void Delete(string SubId)
    {
        try
        {
            _examResultPublishDAL.Delete(SubId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

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
            return _examResultPublishDAL.LoadSchedule(_examResultPublishDTO.TestId);
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
            return _examResultPublishDAL.LoadSubject(_examResultPublishDTO.StandardTextListId);
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
            return _examResultPublishDAL.LoadTest(_examResultPublishDTO.SubId);
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
            return _examResultPublishDAL.LoadSchedule(_examResultPublishDTO.SubId, _examResultPublishDTO.TestId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    public DataTable ExamResultPublish()
    {
        try
        {
            return _examResultPublishDAL.ExamResultPublish(_examResultPublishDTO.StandardTextListId, _examResultPublishDTO.SubId, _examResultPublishDTO.TestId, _examResultPublishDTO.ExamScheduleId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
