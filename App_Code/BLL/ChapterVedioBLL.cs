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
/// Summary description for ChapterVedioBLL
/// </summary>
public class ChapterVedioBLL
{
    private ChapterVedioDAL _ChapterVedioDAL;
    private ChapterVedioDTO _ChapterVedioDTO;
    private ChapterLnsBLL _ChapterLnsBLL;
    private ChapterLnBLL _ChapterLnBLL;

    public ChapterVedioBLL()
    {
        _ChapterVedioDAL = new ChapterVedioDAL();
        _ChapterVedioDTO = new ChapterVedioDTO();
        _ChapterLnsBLL = new ChapterLnsBLL();
        _ChapterLnBLL = new ChapterLnBLL();

        _ChapterVedioDTO.IsNew = true;
    }
    public ChapterVedioBLL(string ChapterVedioId)
        : this()
    {
        _ChapterVedioDTO.IsNew = false;
        Load(ChapterVedioId);
    }

    ~ChapterVedioBLL()
    {
        _ChapterVedioDAL = null;
        _ChapterLnsBLL = null;
        _ChapterLnBLL = null;
    }

    public string ChapterVedioId
    {
        get
        {
            return _ChapterVedioDTO.ChapterVedioId;
        }
        set
        {
            _ChapterVedioDTO.ChapterVedioId = value;
        }
    }
    //public string ChapterIdMain
    //{
    //    get
    //    {
    //        return _ChapterVedioDTO.ChapterIdMain;
    //    }
    //    set
    //    {
    //        _ChapterVedioDTO.ChapterIdMain = value;
    //        if (value != null && SubId != null && StandardTextListId != null && PeriodNo != null)
    //        {
    //            _ChapterVedioDTO.SrNo = _ChapterVedioDAL.GetSrNo(_ChapterVedioDTO.StandardTextListId, _ChapterVedioDTO.SubId, _ChapterVedioDTO.ChapterId, _ChapterVedioDTO.PeriodNo);
    //        }
    //    }
    //}

    public decimal? PeriodNo
    {
        get
        {
            return _ChapterVedioDTO.PeriodNo;
        }
        set
        {
            _ChapterVedioDTO.PeriodNo = value;
            if (value != null && SubId != null && StandardTextListId != null && ChapterId != null)
            {
                _ChapterVedioDTO.SrNo = _ChapterVedioDAL.GetSrNo(_ChapterVedioDTO.StandardTextListId, _ChapterVedioDTO.SubId, _ChapterVedioDTO.ChapterId, _ChapterVedioDTO.PeriodNo);
            }
        }
    }
    public string StandardTextListId
    {
        get
        {
            return _ChapterVedioDTO.StandardTextListId;
        }
        set
        {
            _ChapterVedioDTO.StandardTextListId = value;

            if (value != null && SubId != null && ChapterId != null && PeriodNo != null)
            {
                _ChapterVedioDTO.SrNo = _ChapterVedioDAL.GetSrNo(_ChapterVedioDTO.StandardTextListId, _ChapterVedioDTO.SubId, _ChapterVedioDTO.ChapterId, _ChapterVedioDTO.PeriodNo);
            }
        }
    }

    public string SubId
    {
        get
        {
            return _ChapterVedioDTO.SubId;
        }
        set
        {
            _ChapterVedioDTO.SubId = value;


            if (value != null && StandardTextListId != null && ChapterId != null && PeriodNo != null)
            {
                _ChapterVedioDTO.SrNo = _ChapterVedioDAL.GetSrNo(_ChapterVedioDTO.StandardTextListId, _ChapterVedioDTO.SubId, _ChapterVedioDTO.ChapterId, _ChapterVedioDTO.PeriodNo);
            }
        }
    }
    public string SrNo
    {
        get
        {
            return _ChapterVedioDTO.SrNo;
        }
        set
        {
            _ChapterVedioDTO.SrNo = value;

        }
    }
    public string ChapterId
    {
        get
        {
            return _ChapterVedioDTO.ChapterId;
        }
        set
        {
            _ChapterVedioDTO.ChapterId = value;

            if (value != null && SubId != null && StandardTextListId != null && PeriodNo != null)
            {
                _ChapterVedioDTO.SrNo = _ChapterVedioDAL.GetSrNo(_ChapterVedioDTO.StandardTextListId, _ChapterVedioDTO.SubId, _ChapterVedioDTO.ChapterId, _ChapterVedioDTO.PeriodNo);
            }
        }
    }
    public string Remarks
    {
        get
        {
            return _ChapterVedioDTO.Remarks;
        }
        set
        {
            _ChapterVedioDTO.Remarks = value;
        }
    }
    public string VedioLink
    {
        get
        {
            return _ChapterVedioDTO.VedioLink;
        }
        set
        {
            _ChapterVedioDTO.VedioLink = value;
        }
    }

    public string VedioFileName
    {
        get
        {
            return _ChapterVedioDTO.VedioFileName;
        }
        set
        {
            _ChapterVedioDTO.VedioFileName = value;
        }
    }

    public bool IsDisabled
    {
        get
        {
            return _ChapterVedioDTO.IsDisabled;
        }
        set
        {
            _ChapterVedioDTO.IsDisabled = value;
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

    public DataTable LoadPeriodNo()
    {
        try
        {
            return _ChapterVedioDAL.LoadPeriodNo(_ChapterVedioDTO.ChapterId, _ChapterVedioDTO.SubId);
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
            return _ChapterVedioDAL.LoadSubjects(_ChapterVedioDTO.StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable LoadChapter()
    {
        try
        {
            return _ChapterVedioDAL.LoadChapter(_ChapterVedioDTO.SubId);
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

            if (_ChapterVedioDTO.IsNew == true)
            {
                _ChapterVedioDAL.Insert(_ChapterVedioDTO, alChapterLns);
            }
            else
            {
                _ChapterVedioDAL.Update(_ChapterVedioDTO, alChapterLns, ChapterLnsBLL.DeletedChapterLns);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Load(string ChapterVedioId)
    {
        ArrayList alSubLns = new ArrayList();
        ArrayList alChapterLnsDTO = new ArrayList();

        _ChapterVedioDTO = _ChapterVedioDAL.Select(ChapterVedioId, out alChapterLnsDTO);

        foreach (ChapterLnDTO ChapterLnDTO in alChapterLnsDTO)
        {
            ChapterLnBLL ChapterLnBLL = new ChapterLnBLL();

            ChapterLnBLL.SetState(ChapterLnDTO);

            _ChapterLnsBLL.Add(ChapterLnBLL);
        }
    }

    public void Delete(string ChapterVedioId)
    {
        string isReferenced = _ChapterVedioDAL.IsReferenced(ChapterVedioId);

        try
        {
            if (isReferenced != "")
            {
                throw new Exception("Chapter Video is Referenced. Cannot Delete." + "\n" + isReferenced);
            }
            else
            {
                _ChapterVedioDAL.Delete(ChapterVedioId);
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

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_ChapterVedioDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank.");
        }

        if (_ChapterVedioDTO.ChapterId == null)
        {
            sl.Add("ChapterId", "Chapter cannot be blank.");
        }

        if (_ChapterVedioDTO.SubId == null)
        {
            sl.Add("SubId", " Designation cannot be blank.");
        }

        if (_ChapterVedioDTO.SrNo == null)
        {
            sl.Add("SrNo", "Video No cannot be blank.");
        }

        if (_ChapterVedioDTO.VedioFileName == null)
        {
            sl.Add("VedioFileName", "Link Name cannot be blank.");
        }

        if (_ChapterVedioDTO.VedioLink == null)
        {
            sl.Add("VedioLink", "Vedio Link cannot be blank.");
        }

        if (_ChapterVedioDTO.PeriodNo == null)
        {
            sl.Add("PeriodNo", "Please Select Chapter No.");
        }

        return sl;
    }
    public string GetChapterId(string ChapterName, string PeriodNo, string ChapterId)
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            return _generalDAL.GetChapterId(ChapterName, PeriodNo, ChapterId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public DataTable LoadSubjectsLnClear()
    {
        try
        {
            return _ChapterVedioDAL.LoadSubjectsClear();
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
            return _ChapterVedioDAL.LoadPeriodNo(ChapterId, SubId);
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
            return _ChapterVedioDAL.LoadChapterVideo(ChapterId, SubId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}