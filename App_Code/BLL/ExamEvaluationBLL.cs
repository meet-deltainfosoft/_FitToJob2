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

public class ExamEvaluationBLL
{
    private ExamEvaluationDTO _examEvaluationDTO;
    private ExamEvaluationDAL _examEvaluationDAL;

    public ExamEvaluationBLL()
    {
        _examEvaluationDAL = new ExamEvaluationDAL();
        _examEvaluationDTO = new ExamEvaluationDTO();

        _examEvaluationDTO.IsNew = true;
        _examEvaluationDTO.ExamEvaluationId = System.Guid.NewGuid().ToString();
    }
    public ExamEvaluationBLL(string ExamEvaluationId)
        : this()
    {
        _examEvaluationDTO.IsNew = false;
        Load(ExamEvaluationId);
    }

    ~ExamEvaluationBLL()
    {
        _examEvaluationDAL = null;
    }

    public string ExamEvaluationId
    {
        get
        {
            return _examEvaluationDTO.ExamEvaluationId;
        }
        set
        {
            _examEvaluationDTO.ExamEvaluationId = value;
        }
    }

    public string QueId
    {
        get
        {
            return _examEvaluationDTO.QueId;
        }
        set
        {
            _examEvaluationDTO.QueId = value;
        }
    }

    public string UserId
    {
        get
        {
            return _examEvaluationDTO.UserId;
        }
        set
        {
            _examEvaluationDTO.UserId = value;
        }
    }

    public string ExamId
    {
        get
        {
            return _examEvaluationDTO.ExamId;
        }
        set
        {
            _examEvaluationDTO.ExamId = value;
        }
    }

    public Int16? ImageNo
    {
        get
        {
            return _examEvaluationDTO.ImageNo;
        }
        set
        {
            _examEvaluationDTO.ImageNo = value;
        }
    }

    public string ImagePath
    {
        get
        {
            return _examEvaluationDTO.ImagePath;
        }
        set
        {
            _examEvaluationDTO.ImagePath = value;
        }
    }

    public string RotatedImagePath
    {
        get
        {
            return _examEvaluationDTO.RotatedImagePath;
        }
        set
        {
            _examEvaluationDTO.RotatedImagePath = value;
        }
    }

    public string TotalObtMark
    {
        get
        {
            return _examEvaluationDTO.TotalObtMark;
        }
        set
        {
            _examEvaluationDTO.TotalObtMark = value;
        }
    }

    public DataTable dtExamEvaluationLns
    {
        get
        {
            return _examEvaluationDTO.dtExamEvaluationLns;
        }
        set
        {
            _examEvaluationDTO.dtExamEvaluationLns = value;
        }
    }

    public string SubMarks
    {
        get
        {
            return _examEvaluationDTO.SubMarks;
        }
        set
        {
            _examEvaluationDTO.SubMarks = value;
        }
    }

    public string Remarks
    {
        get
        {
            return _examEvaluationDTO.Remarks;
        }
        set
        {
            _examEvaluationDTO.Remarks = value;
        }
    }

    public decimal? TotalMarks
    {
        get
        {
            return _examEvaluationDTO.TotalMarks;
        }
        set
        {
            _examEvaluationDTO.TotalMarks = value;
        }
    }

    public decimal? ObtainedMarks
    {
        get
        {
            return _examEvaluationDTO.ObtainedMarks;
        }
        set
        {
            _examEvaluationDTO.ObtainedMarks = value;
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_examEvaluationDTO.QueId == null)
        {
            sl.Add("QueId", "Question cannot be blank.");
        }

        if (_examEvaluationDTO.UserId == null)
        {
            sl.Add("UserId", "Employee name cannot be blank.");
        }

        if (_examEvaluationDTO.ExamId == null)
        {
            sl.Add("ExamId", "Exam id cannot be blank.");
        }

        if (_examEvaluationDTO.ImageNo == null)
        {
            sl.Add("ImageNo", "No of image cannot be blank.");
        }

        if (_examEvaluationDTO.ImagePath == null)
        {
            sl.Add("ImagePath", "Image cannot be blank.");
        }

        if (_examEvaluationDTO.SubMarks == null)
            sl.Add("SubMarks", "Sub Marks cannot be blank.");

        if (_examEvaluationDTO.TotalMarks != null && _examEvaluationDTO.ObtainedMarks != null)
        {
            if (_examEvaluationDTO.TotalMarks < _examEvaluationDTO.ObtainedMarks)
            {
                sl.Add("SubMarksT", "Obtained marks must be less then or equal to total marks.");
            }
        }

        return sl;
    }

    public void Save()
    {
        try
        {
            _examEvaluationDAL.Insert(_examEvaluationDTO);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void Load(string ExamEvaluationId)
    {
        //ArrayList alSubLns = new ArrayList();
        //_examEvaluationDTO = _examEvaluationDAL.Select(ExamEvaluationId);
    }

    public void Delete(string ExamId)
    {
        try
        {
            _examEvaluationDAL.Delete(ExamId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataSet LoadAnsData(string QueId, string ExamId, string RegistrationId, string ExamScheduleId, int ImageNo)
    {
        try
        {
            return _examEvaluationDAL.LoadAnsData(QueId, ExamId, RegistrationId, ExamScheduleId, ImageNo);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataSet LoadAnsDataForImageNo(string QueId, string ExamId, string RegistrationId, string ExamScheduleId, int ImageNo)
    {
        try
        {
            return _examEvaluationDAL.LoadAnsDataForImageNo(QueId, ExamId, RegistrationId, ExamScheduleId, ImageNo);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void UpdatePhotoPath()
    {
        try
        {
            _examEvaluationDAL.UpdatePhotoPath(_examEvaluationDTO);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
