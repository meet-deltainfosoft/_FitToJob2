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

/// <summary>
/// Summary description for ChapterBLL
/// </summary>
public class ChapterBLL
{
    private ChapterDTO _ChapterDTO;
    private ChapterDAL _ChapterDAL;
    private ChapterLnsBLL _ChapterLnsBLL;
    private ChapterLnBLL _ChapterLnBLL;

    public ChapterBLL()
    {
        _ChapterDAL = new ChapterDAL();
        _ChapterDTO = new ChapterDTO();
        _ChapterLnsBLL = new ChapterLnsBLL();
        _ChapterLnBLL = new ChapterLnBLL();

        _ChapterDTO.IsNew = true;
    }
    public ChapterBLL(string ChapterId)
        : this()
    {
        _ChapterDTO.IsNew = false;
        Load(ChapterId);
    }

    ~ChapterBLL()
    {
        _ChapterDAL = null;
        _ChapterLnsBLL = null;
        _ChapterLnBLL = null;
    }

    public string ChapterId
    {
        get
        {
            return _ChapterDTO.ChapterId;
        }
        set
        {
            _ChapterDTO.ChapterId = value;
        }
    }

    public string StandardTextListId
    {
        get
        {
            return _ChapterDTO.StandardTextListId;
        }
        set
        {
            _ChapterDTO.StandardTextListId = value;

            if (value != null && SubId != null)
            {
                _ChapterDTO.SrNo = _ChapterDAL.GetSrNo(_ChapterDTO.StandardTextListId, _ChapterDTO.SubId);
            }
        }
    }

    public string SubId
    {
        get
        {
            return _ChapterDTO.SubId;
        }
        set
        {
            _ChapterDTO.SubId = value;


            if (value != null && StandardTextListId != null)
            {
                _ChapterDTO.SrNo = _ChapterDAL.GetSrNo(_ChapterDTO.StandardTextListId, _ChapterDTO.SubId);
            }
        }
    }
    public string SrNo
    {
        get
        {
            return _ChapterDTO.SrNo;
        }
        set
        {
            _ChapterDTO.SrNo = value;

        }
    }
    public decimal? PeriodNo
    {
        get
        {
            return _ChapterDTO.PeriodNo;
        }
        set
        {
            _ChapterDTO.PeriodNo = value;
        }
    }
    public string ChapterName
    {
        get
        {
            return _ChapterDTO.ChapterName;
        }
        set
        {
            _ChapterDTO.ChapterName = value;
        }
    }
    public string Remarks
    {
        get
        {
            return _ChapterDTO.Remarks;
        }
        set
        {
            _ChapterDTO.Remarks = value;
        }
    }
    public DataTable LoadStandard()
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            return _generalDAL.TextList("Standard");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadSubjects()
    {
        try
        {
            return _ChapterDAL.LoadSubjects(_ChapterDTO.StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Save()
    {
        try
        {
            ArrayList alChapterLns = new ArrayList();
            foreach (ChapterLnBLL ChapterLnBLL in _ChapterLnsBLL)
            {
                ChapterLnDTO ChapterLnDTO = new ChapterLnDTO();

                ChapterLnDTO = ChapterLnBLL.GetState();

                alChapterLns.Add(ChapterLnDTO);
            }
            if (_ChapterDTO.IsNew == true)
            {
                _ChapterDAL.Insert(_ChapterDTO, alChapterLns);
            }
            else
            {
                _ChapterDAL.Update(_ChapterDTO, alChapterLns, ChapterLnsBLL.DeletedChapterLns);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Load(string ChapterId)
    {
        ArrayList alSubLns = new ArrayList();
        ArrayList alChapterLnsDTO = new ArrayList();

        _ChapterDTO = _ChapterDAL.Select(ChapterId, out alChapterLnsDTO);

        foreach (ChapterLnDTO ChapterLnDTO in alChapterLnsDTO)
        {
            ChapterLnBLL ChapterLnBLL = new ChapterLnBLL();

            ChapterLnBLL.SetState(ChapterLnDTO);

            _ChapterLnsBLL.Add(ChapterLnBLL);
        }
    }

    public void Delete(string ChapterId)
    {
        string isReferenced = _ChapterDAL.IsReferenced(ChapterId);

        try
        {
            if (isReferenced != "")
            {
                throw new Exception("Chapter is Referenced. Cannot Delete." + "\n" + isReferenced);
            }
            else
            {
                _ChapterDAL.Delete(ChapterId);
            }
        }
        catch (Exception ex)
        {
            //if (ex.Message == "Application Role is Referenced. Cannot Delete." + "\n" + isReferenced)
            //    throw ex;
            //else
            throw new Exception(ex.Message);
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_ChapterDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank.");
        }

        if (_ChapterDTO.SubId == null)
        {
            sl.Add("SubId", " Designation cannot be blank.");
        }

        if (_ChapterDTO.SrNo == null)
        {
            sl.Add("SrNo", "Serial number cannot be blank.");
        }

        if (_ChapterDTO.ChapterName == null)
        {
            sl.Add("ChapterName", "Chapter name cannot be blank.");
        }

        if (_ChapterDTO.PeriodNo == null)
        {
            sl.Add("PeriodNo", "Chapter No cannot be blank.");
        }

        return sl;
    }
    public ChapterLnsBLL ChapterLnsBLL
    {
        get
        {
            return _ChapterLnsBLL;
        }
        set
        {
            _ChapterLnsBLL = value;
        }
    }
    //public DataTable LoadChapterLn()
    //{
    //    try
    //    {
    //        //return _ChapterDAL.LoadChapterLn();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}
    public DataTable LoadSubjectsLnClear()
    {
        try
        {
            return _ChapterDAL.LoadSubjectsClear();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable LoadPeriodNo(string ChapterId, string SubId)
    {
        try
        {
            return _ChapterDAL.LoadPeriodNo(ChapterId, SubId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable LoadChapterVideo(string ChapterId, string SubId)
    {
        try
        {
            return _ChapterDAL.LoadChapterVideo(ChapterId, SubId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}