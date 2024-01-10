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
/// Summary description for EBookPDFBLL
/// </summary>
public class EBookPDFBLL
{
    private EBookPDFDTO _EBookPDFDTO;
    private EBookPDFDAL _EBookPDFDAL;

    public EBookPDFBLL()
    {
        _EBookPDFDAL = new EBookPDFDAL();
        _EBookPDFDTO = new EBookPDFDTO();

        _EBookPDFDTO.IsNew = true;
    }
    public EBookPDFBLL(string EBookPDFId)
        : this()
    {
        _EBookPDFDTO.IsNew = false;
        Load(EBookPDFId);
    }

    ~EBookPDFBLL()
    {
        _EBookPDFDAL = null;
    }
    //public string ChapterIdMain
    //{
    //    get
    //    {
    //        return _EBookPDFDTO.ChapterIdMain;
    //    }
    //    set
    //    {
    //        _EBookPDFDTO.ChapterIdMain = value;
    //        if (value != null && SubId != null && StandardTextListId != null && PeriodNo != null)
    //        {
    //            _EBookPDFDTO.SrNo = _ChapterPDFDAL.GetSrNo(_EBookPDFDTO.StandardTextListId, _EBookPDFDTO.SubId, _EBookPDFDTO.ChapterIdMain, _EBookPDFDTO.PeriodNo);
    //        }
    //    }
    //}

    public string StandardTextListId
    {
        get
        {
            return _EBookPDFDTO.StandardTextListId;
        }
        set
        {
            _EBookPDFDTO.StandardTextListId = value;

            //if (value != null && SubId != null && EBookPDFId != null )
            //{
            //    _EBookPDFDTO.SrNo = _EBookPDFDAL.GetSrNo(_EBookPDFDTO.StandardTextListId, _EBookPDFDTO.SubId, _EBookPDFDTO.EBookPDFId, _EBookPDFDTO.PeriodNo);
            //}
        }
    }

    public string SubId
    {
        get
        {
            return _EBookPDFDTO.SubId;
        }
        set
        {
            _EBookPDFDTO.SubId = value;


            //if (value != null && StandardTextListId != null && EBookPDFId != null )
            //{
            //    _EBookPDFDTO.SrNo = _EBookPDFDAL.GetSrNo(_EBookPDFDTO.StandardTextListId, _EBookPDFDTO.SubId, _EBookPDFDTO.EBookPDFId, _EBookPDFDTO.PeriodNo);
            //}
        }
    }
    public string SrNo
    {
        get
        {
            return _EBookPDFDTO.SrNo;
        }
        set
        {
            _EBookPDFDTO.SrNo = value;

        }
    }
    public string EBookPDFId
    {
        get
        {
            return _EBookPDFDTO.EBookPDFId;
        }
        set
        {
            _EBookPDFDTO.EBookPDFId = value;

            //if (value != null && SubId != null && StandardTextListId != null )
            //{
            //    _EBookPDFDTO.SrNo = _EBookPDFDAL.GetSrNo(_EBookPDFDTO.StandardTextListId, _EBookPDFDTO.SubId, _EBookPDFDTO.EBookPDFId, _EBookPDFDTO.PeriodNo);
            //}
        }
    }
    public string Remarks
    {
        get
        {
            return _EBookPDFDTO.Remarks;
        }
        set
        {
            _EBookPDFDTO.Remarks = value;
        }
    }
    public string UploadphotoPath
    {
        get
        {
            return _EBookPDFDTO.UploadphotoPath;
        }
        set
        {
            _EBookPDFDTO.UploadphotoPath = value;
        }
    }
    public string FileName
    {
        get
        {
            return _EBookPDFDTO.FileName;
        }
        set
        {
            _EBookPDFDTO.FileName = value;
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
            return _EBookPDFDAL.LoadSubjects(_EBookPDFDTO.StandardTextListId);
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
            if (_EBookPDFDTO.IsNew == true)
            {
                _EBookPDFDAL.Insert(_EBookPDFDTO);
            }
            else
            {
                _EBookPDFDAL.Update(_EBookPDFDTO);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Load(string EBookPDFId)
    {
        ArrayList alSubLns = new ArrayList();
        _EBookPDFDTO = _EBookPDFDAL.Select(EBookPDFId);
    }

    public void Delete(string EBookPDFId)
    {
        try
        {
            _EBookPDFDAL.Delete(EBookPDFId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_EBookPDFDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank.");
        }

        if (_EBookPDFDTO.SubId == null)
        {
            sl.Add("SubId", " Designation cannot be blank.");
        }

        if (_EBookPDFDTO.UploadphotoPath == null)
        {
            sl.Add("UploadphotoPath", "Upload Image cannot be blank.");
        }
        return sl;
    }
}