using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for InterviewLnBLL
/// </summary>
public class InterviewLnBLL
{

    private InterviewLnDTO _InterviewLnDTO;
    GeneralDAL _generalDAL;
    private InterviewDAL _InterviewDAL;
    private GeneralBLL _generalBLL;

    public InterviewLnBLL()
    {
        _InterviewLnDTO = new InterviewLnDTO();
        _generalDAL = new GeneralDAL();
        _InterviewDAL = new InterviewDAL();
        _generalBLL = new GeneralBLL();
        _InterviewLnDTO.IsNew = true;
    }
    ~InterviewLnBLL()
    {
        _InterviewDAL = null;
        _generalBLL = null;
    }
    public string InterviewLnId
    {
        get
        {
            return _InterviewLnDTO.InterviewLnId;
        }
    }
    public string InterviewId
    {
        get
        {
            return _InterviewLnDTO.InterviewId;
        }
        set
        {
            _InterviewLnDTO.InterviewId = value;
        }
    }   
    public Int32? LnNo
    {
        get
        {
            return _InterviewLnDTO.LnNo;
        }
        set
        {
            _InterviewLnDTO.LnNo = value;
        }
    }
    public string QueTextListId
    {
        get
        {
            return _InterviewLnDTO.QueTextListId;
        }
        set
        {
            _InterviewLnDTO.QueTextListId = value;
        }
    }
    public string Text
    {
        get
        {
            return _InterviewLnDTO.Text;
        }
        set
        {
            _InterviewLnDTO.Text = value;
        }
    }
    public Int32? ActualMarks
    {
        get
        {
            return _InterviewLnDTO.ActualMarks;
        }
        set
        {
            _InterviewLnDTO.ActualMarks = value;
        }
    }
    public Int32? ObtainedMarks
    {
        get
        {
            return _InterviewLnDTO.ObtainedMarks;
        }
        set
        {
            _InterviewLnDTO.ObtainedMarks = value;
        }
    }
    public bool? IsNew
    {
        get
        {
            return _InterviewLnDTO.IsNew;
        }
    }
    public bool? IsDirty
    {
        get
        {
            return _InterviewLnDTO.IsDirty;
        }
        set
        {
            _InterviewLnDTO.IsDirty = value;
        }
    }
    public bool? IsDeleted
    {
        get
        {
            return _InterviewLnDTO.IsDeleted;
        }
        set
        {
            _InterviewLnDTO.IsDeleted = value;
        }
    }
    public SortedList Validate(InterviewLnsBLL InterviewLnsBLL)
    {
        SortedList sl = new SortedList();
        return sl;
    }
    public void SetState(InterviewLnDTO InterviewLnDTO)
    {
        _InterviewLnDTO = InterviewLnDTO;
        _InterviewLnDTO.IsNew = false;
    }
    public InterviewLnDTO GetState()
    {
        return _InterviewLnDTO;
    }
}