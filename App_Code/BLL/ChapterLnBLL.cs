using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for ChapterLnBLL
/// </summary>
public class ChapterLnBLL
{
    private ChapterLnDTO _ChapterLnDTO;
    private ChapterDAL ChapterDAL;
    private GeneralBLL _generalBLL;


    public ChapterLnBLL()
    {
        _ChapterLnDTO = new ChapterLnDTO();
        ChapterDAL = new ChapterDAL();
        _generalBLL = new GeneralBLL();

        _ChapterLnDTO.IsNew = true;
    }

    ~ChapterLnBLL()
    {
        ChapterDAL = null;
        _generalBLL = null;
    }
    public string ChapterLnId
    {
        get
        {
            return _ChapterLnDTO.ChapterLnId;
        }
    }

    public string ChapterId
    {
        get
        {
            return _ChapterLnDTO.ChapterId;
        }
        set
        {
            _ChapterLnDTO.ChapterId = value;
        }
    }
    public Int32? LnNo
    {
        get
        {
            return _ChapterLnDTO.LnNo;
        }
        set
        {
            _ChapterLnDTO.LnNo = value;
        }
    }
    public string StandardTextListId
    {
        get
        {
            return _ChapterLnDTO.StandardTextListId;
        }
        set
        {
            _ChapterLnDTO.StandardTextListId = value;
        }
    }
    public string StandardName
    {
        get
        {
            return _ChapterLnDTO.StandardName;
        }
        set
        {
            _ChapterLnDTO.StandardName = value;
        }
    }

    public string ChapterTextListId
    {
        get
        {
            return _ChapterLnDTO.ChapterTextListId;
        }
        set
        {
            _ChapterLnDTO.ChapterTextListId = value;
        }
    }

    public string ChapterName
    {
        get
        {
            return _ChapterLnDTO.ChapterName;
        }
        set
        {
            _ChapterLnDTO.ChapterName = value;
        }
    }
    public string SubId
    {
        get
        {
            return _ChapterLnDTO.SubId;
        }
        set
        {
            _ChapterLnDTO.SubId = value;
        }
    }
    public string SubjectName
    {
        get
        {
            return _ChapterLnDTO.SubjectName;
        }
        set
        {
            _ChapterLnDTO.SubjectName = value;
        }
    }
    public decimal? PeriodNo
    {
        get
        {
            return _ChapterLnDTO.PeriodNo;
        }
        set
        {
            _ChapterLnDTO.PeriodNo = value;
        }
    }

    public bool IsNew
    {
        get
        {
            return _ChapterLnDTO.IsNew;
        }
    }

    public bool IsDirty
    {
        get
        {
            return _ChapterLnDTO.IsDirty;
        }
        set
        {
            _ChapterLnDTO.IsDirty = value;
        }
    }

    public bool IsDeleted
    {
        get
        {
            return _ChapterLnDTO.IsDeleted;
        }
        set
        {
            _ChapterLnDTO.IsDeleted = value;
        }
    }

    public string ChapterVideoId
    {
        get
        {
            return _ChapterLnDTO.ChapterVideoId;
        }
        set
        {
            _ChapterLnDTO.ChapterVideoId = value;
        }
    }

    public string ChapterVideoName
    {
        get
        {
            return _ChapterLnDTO.ChapterVideoName;
        }
        set
        {
            _ChapterLnDTO.ChapterVideoName = value;
        }
    }

    public string ChapterVideoHeaderId
    {
        get
        {
            return _ChapterLnDTO.ChapterVideoHeaderId;
        }
        set
        {
            _ChapterLnDTO.ChapterVideoHeaderId = value;
        }
    }

    public SortedList Validate(ChapterLnsBLL ChapterLnsBLL)
    {
        SortedList sl = new SortedList();

        try
        {
            if (_ChapterLnDTO.StandardTextListId == null)
            {
                sl.Add("StandardTextListId", " Department cannot be blank.");
            }

            if (_ChapterLnDTO.SubId == null)
            {
                sl.Add("SubId", " Designation cannot be blank.");
            }

            if (_ChapterLnDTO.LnNo == null)
            {
                sl.Add("SrNo", "Line number cannot be blank.");
            }

            if (_ChapterLnDTO.ChapterName == null)
            {
                sl.Add("ChapterName", "Chapter name cannot be blank.");
            }

            if (_ChapterLnDTO.PeriodNo == null)
            {
                sl.Add("PeriodNo", "Chapter No cannot be blank.");
            }

            if (_ChapterLnDTO.ChapterVideoId == null)
            {
                sl.Add("ChapterVideoId", "Chapter Video cannot be blank.");
            }

            if (_ChapterLnDTO.SubId != null && _ChapterLnDTO.StandardTextListId != null && _ChapterLnDTO.ChapterTextListId != null && _ChapterLnDTO.PeriodNo != null && _ChapterLnDTO.ChapterVideoId != null)
            {
                foreach (ChapterLnBLL ChapterLnBLL in ChapterLnsBLL)
                {
                    if (_ChapterLnDTO.SubId == ChapterLnBLL.SubId 
                        && _ChapterLnDTO.LnNo != ChapterLnBLL.LnNo 
                        && _ChapterLnDTO.StandardTextListId == ChapterLnBLL.StandardTextListId 
                        && _ChapterLnDTO.ChapterTextListId == ChapterLnBLL.ChapterTextListId
                        && _ChapterLnDTO.PeriodNo == ChapterLnBLL.PeriodNo
                        && _ChapterLnDTO.ChapterVideoId == ChapterLnBLL.ChapterVideoId)
                    {
                        sl.Add("Duplicate", "Line Detail already exists.");
                        break;
                    }
                }
            }

            return sl;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void SetState(ChapterLnDTO ChapterLnDTO)
    {
        _ChapterLnDTO = ChapterLnDTO;

        _ChapterLnDTO.IsNew = false;
    }

    public ChapterLnDTO GetState()
    {
        return _ChapterLnDTO;
    }

    private void SetContactDetails(DataTable dtContact)
    {
        //if (dtContact.Rows[0]["ContactPersonName"] != DBNull.Value)
        //    _ChapterLnDTO.ContactPersonName = dtContact.Rows[0]["ContactPersonName"].ToString();
        //else
        //    _ChapterLnDTO.ContactPersonName = null;

        //if (dtContact.Rows[0]["ContactPersonMobileNo"] != DBNull.Value)
        //    _ChapterLnDTO.ContactPersonMobileNo = Convert.ToDecimal(dtContact.Rows[0]["ContactPersonMobileNo"]);
    }
    public DataTable LoadSubjectsLn()
    {
        try
        {
            return ChapterDAL.LoadSubjects(_ChapterLnDTO.StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable LoadChapterLn()
    {
        try
        {
            return ChapterDAL.LoadChapterLn(_ChapterLnDTO.StandardTextListId, _ChapterLnDTO.SubId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}