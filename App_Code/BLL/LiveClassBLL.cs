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

public class LiveClassBLL
{
    private LiveClassDAL _liveClassDAL;
    private LiveClassDTO _liveClassDTO;

    public LiveClassBLL()
    {
        _liveClassDAL = new LiveClassDAL();
        _liveClassDTO = new LiveClassDTO();

        _liveClassDTO.IsNew = true;
    }
    public LiveClassBLL(string LiveClassId)
        : this()
    {
        _liveClassDTO.IsNew = false;
        Load(LiveClassId);
    }

    ~LiveClassBLL()
    {
        _liveClassDAL = null;
    }
    public string LiveClassId
    {
        get
        {
            return _liveClassDTO.LiveClassId;
        }
        set
        {
            _liveClassDTO.LiveClassId = value;
        }
    }
    public string Title
    {
        get
        {
            return _liveClassDTO.Title;
        }
        set
        {
            _liveClassDTO.Title = value;
        }
    }
    public string TopicName
    {
        get
        {
            return _liveClassDTO.TopicName;
        }
        set
        {
            _liveClassDTO.TopicName = value;
        }
    }
    public string Link
    {
        get
        {
            return _liveClassDTO.Link;
        }
        set
        {
            _liveClassDTO.Link = value;
        }
    }
    public string StandardTextListId
    {
        get
        {
            return _liveClassDTO.StandardTextListId;
        }
        set
        {
            _liveClassDTO.StandardTextListId = value;
        }
    }
    public string SubId
    {
        get
        {
            return _liveClassDTO.SubId;
        }
        set
        {
            _liveClassDTO.SubId = value;
        }
    }
    public string SubName
    {
        get
        {
            return _liveClassDTO.SubName;
        }
        set
        {
            _liveClassDTO.SubName = value;
        }
    }
    public DateTime? Date
    {
        get
        {
            return _liveClassDTO.Date;
        }
        set
        {
            _liveClassDTO.Date = value;
        }
    }
    public DateTime? FromTime
    {
        get
        {
            return _liveClassDTO.FromTime;
        }
        set
        {
            _liveClassDTO.FromTime = value;
        }
    }
    public DateTime? ToTime
    {
        get
        {
            return _liveClassDTO.ToTime;
        }
        set
        {
            _liveClassDTO.ToTime = value;
        }
    }
    public string Remarks
    {
        get
        {
            return _liveClassDTO.Remarks;
        }
        set
        {
            _liveClassDTO.Remarks = value;
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
            return _liveClassDAL.LoadSubjects(_liveClassDTO.StandardTextListId);
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
            if (_liveClassDTO.IsNew == true)
            {
                _liveClassDAL.Insert(_liveClassDTO);
            }
            else
            {
                _liveClassDAL.Update(_liveClassDTO);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Load(string LiveClassId)
    {
        ArrayList alSubLns = new ArrayList();
        _liveClassDTO = _liveClassDAL.Select(LiveClassId);
    }

    public void Delete(string ChapterVedioId)
    {
        try
        {
            _liveClassDAL.Delete(ChapterVedioId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_liveClassDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank.");
        }

        if (_liveClassDTO.SubId == null)
        {
            sl.Add("SubId", " Designation cannot be blank.");
        }

        if (_liveClassDTO.Title== null)
        {
            sl.Add("Title", "Title cannot be blank.");
        }

        if (_liveClassDTO.Link == null)
        {
            sl.Add("Link", "Link cannot be blank.");
        }

        if (_liveClassDTO.Date== null)
        {
            sl.Add("Date", "Date cannot be blank.");
        }

        if (_liveClassDTO.FromTime == null)
        {
            sl.Add("FromTime", "FromTime cannot be blank.");
        }

        if (_liveClassDTO.ToTime == null)
        {
            sl.Add("ToTime", "ToTime cannot be blank.");
        }

        if(_liveClassDTO.FromTime != null && _liveClassDTO.ToTime != null)
        {
            if(_liveClassDTO.ToTime < _liveClassDTO.FromTime)
            {
                sl.Add("ToTime1", "ToTime cannot be less than From Time.");
            }
        }

        return sl;
    }
}