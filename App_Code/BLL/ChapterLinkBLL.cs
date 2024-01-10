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
/// Summary description for ChapterLinkBLL
/// </summary>
public class ChapterLinkBLL
{
    private ChapterLinkDAL _ChapterLinkDAL;
    private ChapterLinkDTO _ChapterLinkDTO;
    private GeneralDAL _generalDAL;
    public ChapterLinkBLL()
    {
        _ChapterLinkDAL = new ChapterLinkDAL();
        _ChapterLinkDTO = new ChapterLinkDTO();
        _generalDAL = new GeneralDAL();

        _ChapterLinkDTO.IsNew = true;
    }
    public ChapterLinkBLL(string ChapterLinkId)
        : this()
    {
        _ChapterLinkDTO.IsNew = false;
        Load(ChapterLinkId);
    }

    ~ChapterLinkBLL()
    {
        _ChapterLinkDAL = null;
        _generalDAL = null;
    }
    public string ChapterLinkId
    {
        get
        {
            return _ChapterLinkDTO.ChapterLinkId;
        }
        set
        {
            _ChapterLinkDTO.ChapterLinkId = value;
        }
    }
    //public string ChapterIdMain
    //{
    //    get
    //    {
    //        return _ChapterLinkDTO.ChapterIdMain;
    //    }
    //    set
    //    {
    //        _ChapterLinkDTO.ChapterIdMain = value;
    //        if (value != null && SubId != null && StandardTextListId != null && PeriodNo != null)
    //        {
    //            _ChapterLinkDTO.SrNo = _ChapterLinkDAL.GetSrNo(_ChapterLinkDTO.StandardTextListId, _ChapterLinkDTO.SubId, _ChapterLinkDTO.ChapterIdMain, _ChapterLinkDTO.PeriodNo);
    //        }
    //    }
    //}
    public decimal? PeriodNo
    {
        get
        {
            return _ChapterLinkDTO.PeriodNo;
        }
        set
        {
            _ChapterLinkDTO.PeriodNo = value;
            if (value != null && SubId != null && StandardTextListId != null && ChapterId != null)
            {
                _ChapterLinkDTO.SrNo = _ChapterLinkDAL.GetSrNo(_ChapterLinkDTO.StandardTextListId, _ChapterLinkDTO.SubId, _ChapterLinkDTO.ChapterId, _ChapterLinkDTO.PeriodNo);
            }
        }
    }
    public string StandardTextListId
    {
        get
        {
            return _ChapterLinkDTO.StandardTextListId;
        }
        set
        {
            _ChapterLinkDTO.StandardTextListId = value;

            if (value != null && SubId != null && ChapterId != null && PeriodNo != null)
            {
                _ChapterLinkDTO.SrNo = _ChapterLinkDAL.GetSrNo(_ChapterLinkDTO.StandardTextListId, _ChapterLinkDTO.SubId, _ChapterLinkDTO.ChapterId, _ChapterLinkDTO.PeriodNo);
            }
        }
    }

    public string SubId
    {
        get
        {
            return _ChapterLinkDTO.SubId;
        }
        set
        {
            _ChapterLinkDTO.SubId = value;


            if (value != null && StandardTextListId != null && ChapterId != null && PeriodNo != null)
            {
                _ChapterLinkDTO.SrNo = _ChapterLinkDAL.GetSrNo(_ChapterLinkDTO.StandardTextListId, _ChapterLinkDTO.SubId, _ChapterLinkDTO.ChapterId, _ChapterLinkDTO.PeriodNo);
            }
        }
    }
    public string SrNo
    {
        get
        {
            return _ChapterLinkDTO.SrNo;
        }
        set
        {
            _ChapterLinkDTO.SrNo = value;

        }
    }
    public string Remarks
    {
        get
        {
            return _ChapterLinkDTO.Remarks;
        }
        set
        {
            _ChapterLinkDTO.Remarks = value;
        }
    }
    public string ChapterId
    {
        get
        {
            return _ChapterLinkDTO.ChapterId;
        }
        set
        {
            _ChapterLinkDTO.ChapterId = value;

            if (value != null && SubId != null && StandardTextListId != null && PeriodNo != null)
            {
                _ChapterLinkDTO.SrNo = _ChapterLinkDAL.GetSrNo(_ChapterLinkDTO.StandardTextListId, _ChapterLinkDTO.SubId, _ChapterLinkDTO.ChapterId, _ChapterLinkDTO.PeriodNo);
            }
        }
    }
    public string Link
    {
        get
        {
            return _ChapterLinkDTO.Link;
        }
        set
        {
            _ChapterLinkDTO.Link = value;
        }
    }

    public string LinkName
    {
        get
        {
            return _ChapterLinkDTO.LinkName;
        }
        set
        {
            _ChapterLinkDTO.LinkName = value;
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
            return _ChapterLinkDAL.LoadSubjects(_ChapterLinkDTO.StandardTextListId);
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
            return _ChapterLinkDAL.LoadChapter(_ChapterLinkDTO.SubId);
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
            return _generalDAL.LoadPeriodNo(_ChapterLinkDTO.SubId, _ChapterLinkDTO.ChapterId);
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
            if (_ChapterLinkDTO.IsNew == true)
            {
                _ChapterLinkDAL.Insert(_ChapterLinkDTO);
            }
            else
            {
                _ChapterLinkDAL.Update(_ChapterLinkDTO);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Load(string ChapterLinkId)
    {
        ArrayList alSubLns = new ArrayList();
        _ChapterLinkDTO = _ChapterLinkDAL.Select(ChapterLinkId);
    }

    public void Delete(string ChapterVedioId)
    {
        string isReferenced = _ChapterLinkDAL.IsReferenced(ChapterVedioId);

        try
        {
            if (isReferenced != "")
            {
                throw new Exception("Chapter Link is Referenced. Cannot Delete." + "\n" + isReferenced);
            }
            else
            {
                _ChapterLinkDAL.Delete(ChapterVedioId);
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

        if (_ChapterLinkDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank.");
        }

        if (_ChapterLinkDTO.ChapterId == null)
        {
            sl.Add("ChapterId", "Chapter cannot be blank.");
        }

        if (_ChapterLinkDTO.SubId == null)
        {
            sl.Add("SubId", " Designation cannot be blank.");
        }

        if (_ChapterLinkDTO.SrNo == null)
        {
            sl.Add("SrNo", "SrNo cannot be blank.");
        }

        if (_ChapterLinkDTO.Link == null)
        {
            sl.Add("Link", "Link Name cannot be blank.");
        }

        if (_ChapterLinkDTO.LinkName == null)
        {
            sl.Add("LinkName", "Vedio Link cannot be blank.");
        }

        if (_ChapterLinkDTO.PeriodNo == null)
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
    public int getSrNo(string ChapterId, string ChapterVideoId)
    {
        try
        {
            return _ChapterLinkDAL.getSrNo(_ChapterLinkDTO.SubId, ChapterId, ChapterVideoId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public int? ChapterVideoId
    {
        get
        {
            return _ChapterLinkDTO.ChapterVideoId;
        }
        set
        {
            _ChapterLinkDTO.ChapterVideoId = value;
        }
    }

    public DataTable LoadChapterVideo()
    {
        try
        {
            return _ChapterLinkDAL.LoadChapterVideo(_ChapterLinkDTO.ChapterId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}