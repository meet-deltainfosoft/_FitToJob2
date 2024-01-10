using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for HODInterviewLnBLL
/// </summary>
public class HODInterviewLnBLL
{
	private HODInterviewLnDTO _HODInterviewLnDTO;
    GeneralDAL _generalDAL;
    private HODInterviewDAL _HODInterviewDAL;
    private GeneralBLL _generalBLL;

    public HODInterviewLnBLL()
    {
        _HODInterviewLnDTO = new HODInterviewLnDTO();
        _generalDAL = new GeneralDAL();
        _HODInterviewDAL = new HODInterviewDAL();
        _generalBLL = new GeneralBLL();
        _HODInterviewLnDTO.IsNew = true;
    }
    ~HODInterviewLnBLL()
    {
        _HODInterviewDAL = null;
        _generalBLL = null;
    }
    public string HODInterviewLnId
    {
        get
        {
            return _HODInterviewLnDTO.HODInterviewLnId;
        }
    }
    public string InterviewId
    {
        get
        {
            return _HODInterviewLnDTO.HODInterviewId;
        }
        set
        {
            _HODInterviewLnDTO.HODInterviewId = value;
        }
    }   
    public Int32? LnNo
    {
        get
        {
            return _HODInterviewLnDTO.LnNo;
        }
        set
        {
            _HODInterviewLnDTO.LnNo = value;
        }
    }
    public string QueTextListId
    {
        get
        {
            return _HODInterviewLnDTO.QueTextListId;
        }
        set
        {
            _HODInterviewLnDTO.QueTextListId = value;
        }
    }
    public string Text
    {
        get
        {
            return _HODInterviewLnDTO.Text;
        }
        set
        {
            _HODInterviewLnDTO.Text = value;
        }
    }
    public Int32? ActualMarks
    {
        get
        {
            return _HODInterviewLnDTO.ActualMarks;
        }
        set
        {
            _HODInterviewLnDTO.ActualMarks = value;
        }
    }
    public Int32? HRObtainedMarks
    {
        get
        {
            return _HODInterviewLnDTO.HRObtainedMarks;
        }
        set
        {
            _HODInterviewLnDTO.HRObtainedMarks = value;
        }
    }
    public Int32? HODObtainedMarks
    {
        get
        {
            return _HODInterviewLnDTO.HODObtainedMarks;
        }
        set
        {
            _HODInterviewLnDTO.HODObtainedMarks = value;
        }
    }
    public bool? IsNew
    {
        get
        {
            return _HODInterviewLnDTO.IsNew;
        }
    }
    public bool? IsDirty
    {
        get
        {
            return _HODInterviewLnDTO.IsDirty;
        }
        set
        {
            _HODInterviewLnDTO.IsDirty = value;
        }
    }
    public bool? IsDeleted
    {
        get
        {
            return _HODInterviewLnDTO.IsDeleted;
        }
        set
        {
            _HODInterviewLnDTO.IsDeleted = value;
        }
    }
    public SortedList Validate(HODInterviewLnsBLL HODInterviewLnsBLL)
    {
        SortedList sl = new SortedList();
        return sl;
    }
    public void SetState(HODInterviewLnDTO HODInterviewLnDTO)
    {
        _HODInterviewLnDTO = HODInterviewLnDTO;
        _HODInterviewLnDTO.IsNew = false;
    }
    public HODInterviewLnDTO GetState()
    {
        return _HODInterviewLnDTO;
    }
}