using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for PatternLnBLL
/// </summary>
public class PatternLnBLL
{
    private PatternLnsDTO _PatternLnDTO;
    private PatternDAL _PatternDAL;
    private GeneralDAL _generalDAL;
    private PatternLnsBLL _PatternLnsBLL;

	public PatternLnBLL()
	{
        _PatternLnDTO = new PatternLnsDTO();
        _PatternDAL = new PatternDAL();
        _generalDAL = new GeneralDAL();
        _PatternLnDTO.IsNew = true;
	}

    public string PatternLnId
    {
        get
        {
            return _PatternLnDTO.PatternLnId;
        }
    }

    public string PatternId
    {
        get
        {
            return _PatternLnDTO.PatternId;
        }
        set
        {
            _PatternLnDTO.PatternId = value;
        }
    }

    public Int32? LnNo
    {
        get
        {
            return _PatternLnDTO.LnNo;
        }
        set
        {
            _PatternLnDTO.LnNo = value;
        }
    }
    public string SubId
    {
        get
        {
            return _PatternLnDTO.SubId;
        }
        set
        {
            _PatternLnDTO.SubId = value;
        }
    }
    public string Subject
    {
        get
        {
            return _PatternLnDTO.Subject;
        }
        set
        {
            _PatternLnDTO.Subject = value;
        }
    }
    public int? NoOfMCQ
    {
        get
        {
            return _PatternLnDTO.NoOfMCQ;
        }
        set
        {
            _PatternLnDTO.NoOfMCQ = value;
        }
    }
    public decimal? MCQRightMarks
    {
        get
        {
            return _PatternLnDTO.MCQRightMarks;
        }
        set
        {
            _PatternLnDTO.MCQRightMarks = value;
        }
    }

    public decimal? MCQWrongMarks
    {
        get
        {
            return _PatternLnDTO.MCQWrongMarks;
        }
        set
        {
            _PatternLnDTO.MCQWrongMarks = value;
        }
    }

    public decimal? MCQSkippedMarks
    {
        get
        {
            return _PatternLnDTO.MCQSkippedMarks;
        }
        set
        {
            _PatternLnDTO.MCQSkippedMarks = value;
        }
    }
    public int? NoOfNonMCQ
    {
        get
        {
            return _PatternLnDTO.NoOfNonMCQ;
        }
        set
        {
            _PatternLnDTO.NoOfNonMCQ = value;
        }
    }
    public decimal? NonMCQRightMarks
    {
        get
        {
            return _PatternLnDTO.NonMCQRightMarks;
        }
        set
        {
            _PatternLnDTO.NonMCQRightMarks = value;
        }
    }

    public decimal? NonMCQWrongMarks
    {
        get
        {
            return _PatternLnDTO.NonMCQWrongMarks;
        }
        set
        {
            _PatternLnDTO.NonMCQWrongMarks = value;
        }
    }
    public decimal? NonMCQSkippedMarks
    {
        get
        {
            return _PatternLnDTO.NonMCQSkippedMarks;
        }
        set
        {
            _PatternLnDTO.NonMCQSkippedMarks = value;
        }
    }

    public bool IsDeleted
    {
        set
        {
            _PatternLnDTO.IsDeleted = value;
        }
        get
        {
            return _PatternLnDTO.IsDeleted;
        }
    }

    public bool IsNew
    {
        set
        {
            _PatternLnDTO.IsNew = value;
        }
        get
        {
            return _PatternLnDTO.IsNew;
        }
    }

    public bool IsDirty
    {
        set
        {
            _PatternLnDTO.IsDirty = value;
        }
        get
        {
            return _PatternLnDTO.IsDirty;
        }
    }

    public PatternLnsBLL PatternLnsBLL
    {
        get
        {
            return _PatternLnsBLL;
        }
        set
        {
            _PatternLnsBLL = value;
        }
    }

    public SortedList Validate(PatternLnBLL PatternLnBLL)
    {
        SortedList sl = new SortedList();

        if (_PatternLnDTO.SubId == null)
        {
            sl.Add("SubId", "Please Select Designation.");
        }
        
        return sl;
    }

    public void SetState(PatternLnsDTO _PatternLnsDTO)
    {
        _PatternLnDTO = _PatternLnsDTO;
        _PatternLnDTO.IsNew = false;
    }

    public PatternLnsDTO GetState()
    {
        return _PatternLnDTO;
    }

    public void CheckIfReferenced()
    {
        if (_PatternLnDTO.IsNew == false)
        {
            string isReferenced = _PatternDAL.IsReferenced(null, PatternLnId);

            if (isReferenced != "")
            {
                throw new Exception("Pattern Line is Referenced. Cannot Remove/Modify." + "\n" + isReferenced);
            }
        }
    }

   

}