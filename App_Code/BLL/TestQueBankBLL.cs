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

public class TestQueBankBLL
{
    private TestQueBankDAL _testQueBankDAL;
    private TestQueBankDTO _testQueBankDTO;

    public TestQueBankBLL()
    {
        _testQueBankDAL = new TestQueBankDAL();
        _testQueBankDTO.IsNew = true;
    }
    public TestQueBankBLL(string TestId)
        : this()
    {
        //  _examScheduleDTO.IsNew = false;
        Load(TestId);
    }
    ~TestQueBankBLL()
    {
        _testQueBankDAL = null;
    }
    public string TestId
    {
        get
        {
            return _testQueBankDTO.TestId;
        }
        set
        {
            _testQueBankDTO.TestId = value;
        }
    }
    public string Name
    {
        get
        {
            return _testQueBankDTO.Name;
        }
        set
        {
            _testQueBankDTO.Name = value;
        }
    }

    public string ChapterId
    {
        get
        {
            return _testQueBankDTO.ChapterId;
        }
        set
        {
            _testQueBankDTO.ChapterId = value;
        }
    }
    public string PeriodNo
    {
        get
        {
            return _testQueBankDTO.PeriodNo;
        }
        set
        {
            _testQueBankDTO.PeriodNo = value;
        }
    }
    public string StandardId
    {
        get
        {
            return _testQueBankDTO.StandardId;
        }
        set
        {
            _testQueBankDTO.StandardId = value;
        }
    }
    public string SubId
    {
        get
        {
            return _testQueBankDTO.SubId;
        }
        set
        {
            _testQueBankDTO.SubId = value;
        }
    }
    public int? Easy
    {
        get
        {
            return _testQueBankDTO.Easy;
        }
        set
        {
            _testQueBankDTO.Easy = value;
        }
    }
    public int? Medium
    {
        get
        {
            return _testQueBankDTO.Medium;
        }
        set
        {
            _testQueBankDTO.Medium = value;
        }
    }
    public int? Hard
    {
        get
        {
            return _testQueBankDTO.Hard;
        }
        set
        {
            _testQueBankDTO.Hard = value;
        }
    }
    public int? TotalQue
    {
        get
        {
            return _testQueBankDTO.TotalQue;
        }
        set
        {
            _testQueBankDTO.TotalQue = value;
        }
    }

    public string PatternId
    {
        get
        {
            return _testQueBankDTO.PatternId;
        }
        set
        {
            _testQueBankDTO.PatternId = value;
        }
    }
    public DataTable QueBankFoTest()
    {
        try
        {
            return _testQueBankDAL.QueBankFoTest(_testQueBankDTO.StandardId, _testQueBankDTO.SubId, _testQueBankDTO.ChapterId, _testQueBankDTO.Easy, _testQueBankDTO.Medium, _testQueBankDTO.Hard, _testQueBankDTO.LevelOfQue, _testQueBankDTO.PeriodNo);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
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
            return _testQueBankDAL.LoadSubjects(_testQueBankDTO.PatternId, _testQueBankDTO.TestId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable LoadPatterns()
    {
        try
        {
            return _testQueBankDAL.LoadPatterns(_testQueBankDTO.StandardId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void InsertTest()
    {
        try
        {
            _testQueBankDAL.InsertTest(_testQueBankDTO);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void Load(string TestId)
    {
        ArrayList alSubLns = new ArrayList();
        _testQueBankDTO = _testQueBankDAL.Select(TestId);
    }

    public DataTable dtDetaiils
    {
        get
        {
            return _testQueBankDTO.dtDetaiils;
        }
        set
        {
            _testQueBankDTO.dtDetaiils = value;
        }
    }

    public void Delete(string TestId)
    {
        try
        {
            _testQueBankDAL.Delete(TestId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public string LevelOfQue
    {
        get
        {
            return _testQueBankDTO.LevelOfQue;
        }
        set
        {
            _testQueBankDTO.LevelOfQue = value;
        }
    }

    public DataTable LoadLevelOfQue()
    {
        try
        {
            return _testQueBankDAL.LoadLevelOfQue();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public ArrayList alTest
    {
        get
        {
            return _testQueBankDTO.alTest;
        }
        set
        {
            _testQueBankDTO.alTest = value;
        }
    }
    public DataTable LoadChapter()
    {
        try
        {
            return _testQueBankDAL.LoadChapter(_testQueBankDTO.SubId);
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
            GeneralDAL _generalDAL = new GeneralDAL();
            return _generalDAL.LoadPeriodNo(_testQueBankDTO.SubId, _testQueBankDTO.ChapterId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_testQueBankDTO.Name == null)
        {
            sl.Add("Name", "Please Select Test");
        }
        else if (_testQueBankDTO.IsNew == true)
        {
            if (_testQueBankDAL.NamesExists(_testQueBankDTO.Name, _testQueBankDTO.StandardId, _testQueBankDTO.PatternId) == true)
            {
                sl.Add("Name", "Duplicate Name.");
            }
        }
        if (_testQueBankDTO.StandardId == null)
        {
            sl.Add("StandardId", "Please Select Standard");
        }
        if (_testQueBankDTO.SubId == null)
        {
            sl.Add("SubId", "Please Select Subject");
        }
        if (_testQueBankDTO.PatternId == null)
        {
            sl.Add("PatternId", "Please Select Pattern");
        }
        if (_testQueBankDTO.Type == null)
        {
            sl.Add("Type", "Please Select Type");
        }
        else
        {
            if (_testQueBankDTO.Type == "Auto")
            {
                if (_testQueBankDTO.Easy == null && _testQueBankDTO.Hard == null && _testQueBankDTO.Medium == null)
                    sl.Add("Type", "Easy ,Medium or Hard can not be blank.");
            }
        }
        return sl;
    }
    public string Type
    {
        get
        {
            return _testQueBankDTO.Type;
        }
        set
        {
            _testQueBankDTO.Type = value;
        }
    }

    public bool IsNew
    {
        get
        {
            return _testQueBankDTO.IsNew;
        }
        set
        {
            _testQueBankDTO.IsNew = value;
        }
    }

}

