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
/// Summary description for VideoNoBLL
/// </summary>
public class VideoNoBLL
{
    private VideoNoDAL _VideoNoDAL;
    private VideoNoDTO _VideoNoDTO;
    private GeneralDAL _generalDAL;
    public VideoNoBLL()
    {
        _VideoNoDAL = new VideoNoDAL();
        _VideoNoDTO = new VideoNoDTO();
        _generalDAL = new GeneralDAL();

        _VideoNoDTO.IsNew = true;
    }
    public VideoNoBLL(string VideoNoId)
        : this()
    {
        _VideoNoDTO.IsNew = false;
        Load(VideoNoId);
    }

    ~VideoNoBLL()
    {
        _VideoNoDAL = null;
        _generalDAL = null;
    }
    public string VideoNoId
    {
        get
        {
            return _VideoNoDTO.VideoNoId;
        }
        set
        {
            _VideoNoDTO.VideoNoId = value;
        }
    }
    public string ChapterId
    {
        get
        {
            return _VideoNoDTO.ChapterId;
        }
        set
        {
            _VideoNoDTO.ChapterId = value;
        }
    }
    public decimal? PeriodNo
    {
        get
        {
            return _VideoNoDTO.PeriodNo;
        }
        set
        {
            _VideoNoDTO.PeriodNo = value;
        }
    }
    public string StandardTextListId
    {
        get
        {
            return _VideoNoDTO.StandardTextListId;
        }
        set
        {
            _VideoNoDTO.StandardTextListId = value;
        }
    }

    public string SubId
    {
        get
        {
            return _VideoNoDTO.SubId;
        }
        set
        {
            _VideoNoDTO.SubId = value;
        }
    }
    public string PersonName1
    {
        get
        {
            return _VideoNoDTO.PersonName1;
        }
        set
        {
            _VideoNoDTO.PersonName1 = value;
        }
    }
    public string PersonName2
    {
        get
        {
            return _VideoNoDTO.PersonName2;
        }
        set
        {
            _VideoNoDTO.PersonName2 = value;
        }
    }

    public string PersonName3
    {
        get
        {
            return _VideoNoDTO.PersonName3;
        }
        set
        {
            _VideoNoDTO.PersonName3 = value;
        }
    }

    public string PersonName4
    {
        get
        {
            return _VideoNoDTO.PersonName4;
        }
        set
        {
            _VideoNoDTO.PersonName4 = value;
        }
    }

    public string PersonName5
    {
        get
        {
            return _VideoNoDTO.PersonName5;
        }
        set
        {
            _VideoNoDTO.PersonName5 = value;
        }
    }

    public string Ratio1
    {
        get
        {
            return _VideoNoDTO.Ratio1;
        }
        set
        {
            _VideoNoDTO.Ratio1 = value;
        }
    }

    public string Ratio2
    {
        get
        {
            return _VideoNoDTO.Ratio2;
        }
        set
        {
            _VideoNoDTO.Ratio2 = value;
        }
    }

    public string Ratio3
    {
        get
        {
            return _VideoNoDTO.Ratio3;
        }
        set
        {
            _VideoNoDTO.Ratio3 = value;
        }
    }

    public string Ratio4
    {
        get
        {
            return _VideoNoDTO.Ratio4;
        }
        set
        {
            _VideoNoDTO.Ratio4 = value;
        }
    }

    public string Ratio5
    {
        get
        {
            return _VideoNoDTO.Ratio5;
        }
        set
        {
            _VideoNoDTO.Ratio5 = value;
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
            return _VideoNoDAL.LoadSubjects(_VideoNoDTO.StandardTextListId);
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
            return _VideoNoDAL.LoadChapter(_VideoNoDTO.SubId);
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
            return _generalDAL.LoadPeriodNo(_VideoNoDTO.SubId, _VideoNoDTO.ChapterId);
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
            if (_VideoNoDTO.IsNew == true)
            {
                _VideoNoDAL.Insert(_VideoNoDTO);
            }
            else
            {
                _VideoNoDAL.Update(_VideoNoDTO);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Load(string VideoNoId)
    {
        ArrayList alSubLns = new ArrayList();
        _VideoNoDTO = _VideoNoDAL.Select(VideoNoId);
    }

    public void Delete(string ChapterVedioId)
    {
        try
        {
            _VideoNoDAL.Delete(ChapterVedioId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_VideoNoDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank.");
        }

        if (_VideoNoDTO.ChapterId == null)
        {
            sl.Add("ChapterId", "Chapter cannot be blank.");
        }

        if (_VideoNoDTO.SubId == null)
        {
            sl.Add("SubId", " Designation cannot be blank.");
        }

        if (_VideoNoDTO.PeriodNo == null)
        {
            sl.Add("PeriodNo", "Please Select Chapter No.");
        }

        if (_VideoNoDTO.PersonName1 == null)
        {
            sl.Add("PersonName1", "Person Name (1) cannot be blank.");
        }

        if (_VideoNoDTO.Ratio1 == null)
        {
            sl.Add("Ratio1", "Ratio (1) cannot be blank.");
        }

        return sl;
    }
}