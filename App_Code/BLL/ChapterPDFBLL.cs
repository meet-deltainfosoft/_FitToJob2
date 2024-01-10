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
/// Summary description for ChapterPDFBLL
/// </summary>
public class ChapterPDFBLL
{
    private ChapterPDFDTO _ChapterPDFDTO;
    private ChapterPDFDAL _ChapterPDFDAL;

    public ChapterPDFBLL()
    {
        _ChapterPDFDAL = new ChapterPDFDAL();
        _ChapterPDFDTO = new ChapterPDFDTO();

        _ChapterPDFDTO.IsNew = true;
    }
    public ChapterPDFBLL(string ChapterPDFId)
        : this()
    {
        _ChapterPDFDTO.IsNew = false;
        Load(ChapterPDFId);
    }

    ~ChapterPDFBLL()
    {
        _ChapterPDFDAL = null;
    }
    //public string ChapterIdMain
    //{
    //    get
    //    {
    //        return _ChapterPDFDTO.ChapterIdMain;
    //    }
    //    set
    //    {
    //        _ChapterPDFDTO.ChapterIdMain = value;
    //        if (value != null && SubId != null && StandardTextListId != null && PeriodNo != null)
    //        {
    //            _ChapterPDFDTO.SrNo = _ChapterPDFDAL.GetSrNo(_ChapterPDFDTO.StandardTextListId, _ChapterPDFDTO.SubId, _ChapterPDFDTO.ChapterIdMain, _ChapterPDFDTO.PeriodNo);
    //        }
    //    }
    //}


    public decimal? PeriodNo
    {
        get
        {
            return _ChapterPDFDTO.PeriodNo;
        }
        set
        {
            _ChapterPDFDTO.PeriodNo = value;

            if (value != null && SubId != null && StandardTextListId != null && ChapterId != null)
            {
                _ChapterPDFDTO.SrNo = _ChapterPDFDAL.GetSrNo(_ChapterPDFDTO.StandardTextListId, _ChapterPDFDTO.SubId, _ChapterPDFDTO.ChapterId, _ChapterPDFDTO.PeriodNo);
            }
        }
    }

    public string StandardTextListId
    {
        get
        {
            return _ChapterPDFDTO.StandardTextListId;
        }
        set
        {
            _ChapterPDFDTO.StandardTextListId = value;

            if (value != null && SubId != null && ChapterId != null && PeriodNo != null)
            {
                _ChapterPDFDTO.SrNo = _ChapterPDFDAL.GetSrNo(_ChapterPDFDTO.StandardTextListId, _ChapterPDFDTO.SubId, _ChapterPDFDTO.ChapterId, _ChapterPDFDTO.PeriodNo);
            }
        }
    }

    public string SubId
    {
        get
        {
            return _ChapterPDFDTO.SubId;
        }
        set
        {
            _ChapterPDFDTO.SubId = value;


            if (value != null && StandardTextListId != null && ChapterId != null && PeriodNo != null)
            {
                _ChapterPDFDTO.SrNo = _ChapterPDFDAL.GetSrNo(_ChapterPDFDTO.StandardTextListId, _ChapterPDFDTO.SubId, _ChapterPDFDTO.ChapterId, _ChapterPDFDTO.PeriodNo);
            }
        }
    }
    public string SrNo
    {
        get
        {
            return _ChapterPDFDTO.SrNo;
        }
        set
        {
            _ChapterPDFDTO.SrNo = value;

        }
    }
    public string ChapterId
    {
        get
        {
            return _ChapterPDFDTO.ChapterId;
        }
        set
        {
            _ChapterPDFDTO.ChapterId = value;

            if (value != null && SubId != null && StandardTextListId != null && PeriodNo != null)
            {
                _ChapterPDFDTO.SrNo = _ChapterPDFDAL.GetSrNo(_ChapterPDFDTO.StandardTextListId, _ChapterPDFDTO.SubId, _ChapterPDFDTO.ChapterId, _ChapterPDFDTO.PeriodNo);
            }
        }
    }
    public string Remarks
    {
        get
        {
            return _ChapterPDFDTO.Remarks;
        }
        set
        {
            _ChapterPDFDTO.Remarks = value;
        }
    }
    public string UploadphotoPath
    {
        get
        {
            return _ChapterPDFDTO.UploadphotoPath;
        }
        set
        {
            _ChapterPDFDTO.UploadphotoPath = value;
        }
    }
    public string FileName
    {
        get
        {
            return _ChapterPDFDTO.FileName;
        }
        set
        {
            _ChapterPDFDTO.FileName = value;
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
            return _ChapterPDFDAL.LoadSubjects(_ChapterPDFDTO.StandardTextListId);
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
            return _ChapterPDFDAL.LoadChapter(_ChapterPDFDTO.SubId);
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
            return _ChapterPDFDAL.LoadPeriodNo(_ChapterPDFDTO.ChapterId, _ChapterPDFDTO.SubId);
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
            if (_ChapterPDFDTO.IsNew == true)
            {
                _ChapterPDFDAL.Insert(_ChapterPDFDTO);
            }
            else
            {
                _ChapterPDFDAL.Update(_ChapterPDFDTO);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Load(string ChapterPDFId)
    {
        ArrayList alSubLns = new ArrayList();
        _ChapterPDFDTO = _ChapterPDFDAL.Select(ChapterPDFId);
    }

    public void Delete(string ChapterPDFId)
    {
        //string isReferenced = _ChapterDAL.IsReferenced(ChapterId);

        try
        {
            //if (isReferenced != "")
            //{
            //    throw new Exception("Exam Schedule is Referenced. Cannot Delete." + "\n" + isReferenced);
            //}
            //else
            //{
            _ChapterPDFDAL.Delete(ChapterPDFId);
            //}
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

        if (_ChapterPDFDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank.");
        }

        if (_ChapterPDFDTO.SubId == null)
        {
            sl.Add("SubId", " Designation cannot be blank.");
        }

        if (_ChapterPDFDTO.ChapterId == null)
        {
            sl.Add("ChapterId", "Chapter cannot be blank.");
        }

        if (_ChapterPDFDTO.PeriodNo == null)
        {
            sl.Add("PeriodNo", "Please Select Chapter No.");
        }

        if (_ChapterPDFDTO.SrNo == null)
        {
            sl.Add("SrNo", "SrNo cannot be blank.");
        }

        if (_ChapterPDFDTO.UploadphotoPath == null)
        {
            sl.Add("UploadphotoPath", "Upload Image cannot be blank.");
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
            return _ChapterPDFDAL.getSrNo(_ChapterPDFDTO.SubId, ChapterId, ChapterVideoId);
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
            return _ChapterPDFDTO.ChapterVideoId;
        }
        set
        {
            _ChapterPDFDTO.ChapterVideoId = value;
        }
    }

    public DataTable LoadChapterVideo()
    {
        try
        {
            return _ChapterPDFDAL.LoadChapterVideo(_ChapterPDFDTO.ChapterId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}