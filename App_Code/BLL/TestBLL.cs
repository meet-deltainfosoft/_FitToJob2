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

public class TestBLL
{
    private TestDTO _testDTO;
    private TestDAL _testDAL;

    public TestBLL()
    {
        _testDAL = new TestDAL();
        _testDTO = new TestDTO();

        _testDTO.IsNew = true;
    }
    public TestBLL(string TestId)
        : this()
    {
        _testDTO.IsNew = false;
        Load(TestId);
    }

    ~TestBLL()
    {
        _testDAL = null;
    }

    public string TestId
    {
        get
        {
            return _testDTO.TestId;
        }
        set
        {
            _testDTO.TestId = value;
        }
    }

    public string TestName
    {
        get
        {
            return _testDTO.TestName;
        }
        set
        {
            _testDTO.TestName = value;
        }
    }

    public string SubId
    {
        get
        {
            return _testDTO.SubId;
        }
        set
        {
            _testDTO.SubId = value;
        }
    }

    public string Remarks
    {
        get
        {
            return _testDTO.Remarks;
        }
        set
        {
            _testDTO.Remarks = value;
        }
    }

    public string StandardTextListId
    {
        get
        {
            return _testDTO.StandardTextListId;
        }
        set
        {
            _testDTO.StandardTextListId = value;
        }
    }

    public SortedList Validate()
    {
        SortedList sl = new SortedList();

        if (_testDTO.SubId == null)
        {
            sl.Add("StandardTextListId", " Department cannot be blank.");
        }

        if (_testDTO.TestName == null)
        {
            sl.Add("Names", "Name cannot be blank.");
        }
        else if (_testDTO.IsNew == true)
        {
            if (_testDAL.NamesExists(_testDTO.TestName, _testDTO.SubId) == true)
            {
                sl.Add("Names", "Duplicate Name.");
            }
        }
        else if (_testDTO.IsNew == false)
        {
            if (_testDAL.NamesExists(_testDTO.TestName, _testDTO.SubId, _testDTO.TestId) == true)
            {
                sl.Add("Names", "Duplicate Name.");
            }
        }
        //Name
        return sl;
    }

    public void Save()
    {
        try
        {
            if (_testDTO.IsNew == true)
                _testDAL.Insert(_testDTO);
            else
                _testDAL.Update(_testDTO);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public void Load(string TestId)
    {
        ArrayList alSubLns = new ArrayList();
        _testDTO = _testDAL.Select(TestId);
    }

    public void Delete(string TestId)
    {
        try
        {
            _testDAL.Delete(TestId);
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
            GeneralDAL _g = new GeneralDAL();
            return _g.TextList("Standard");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    //public DataTable LoadSubjects()
    //{
    //    try
    //    {
    //        return _testDAL.LoadSubjects();
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //}

    public DataTable LoadSubjects(string StandardId)
    {
        try
        {
            return _testDAL.LoadSubjects(StandardId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
