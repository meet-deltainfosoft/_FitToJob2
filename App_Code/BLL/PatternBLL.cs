using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

/// <summary>
/// Summary description for PatternBLL
/// </summary>
public class PatternBLL
{
    GeneralDAL _generalDAL;
    PatternDAL _patternDAL;
    PatternDTO _patternDTO;
    PatternLnsBLL _PatternLnsBLL;
    public PatternBLL()
    {
        _generalDAL = new GeneralDAL();
        _patternDTO = new PatternDTO();
        _PatternLnsBLL = new PatternLnsBLL();
        _patternDAL = new PatternDAL();
        _patternDTO.IsNew = true;
    }

    public PatternBLL(string PatternId)
        : this()
    {
        _patternDTO.IsNew = false;
        Load(PatternId);
    }

    ~PatternBLL()
    {
        _generalDAL = null;
    }

    public string PatternId
    {
        get
        {
            return PatternId;
        }
        //set
        //{
        //    PatternId = value;


        //    //if (value != null)
        //    //{
        //    //    _lgrId = value;

        //    //    //Get Vendor Details
        //    //    GetVendorDetails();
        //    //}

        //}

    }

    public string StandardTextlistId
    {
        get
        {
            return _patternDTO.StandardTextlistId;
        }
        set
        {
            _patternDTO.StandardTextlistId = value;
        }
    }
    public string StandardName
    {
        get
        {
            return _patternDTO.StandardName;
        }
        set
        {
            _patternDTO.StandardName= value;
        }
    }
    public string PatternName
    {
        get
        {
            return _patternDTO.PatternName;
        }
        set
        {
            _patternDTO.PatternName = value;
        }
    }

    public PatternLnsBLL PatternLnsBLL
    {
        get
        {
            return _PatternLnsBLL;
        }
        set
        {
            _PatternLnsBLL = value;
        }
    }
    public bool IsNew
    {
        get
        {
            return _patternDTO.IsNew;
        }
    }

    public SortedList Validate()
    {
        try
        {
            SortedList sl = new SortedList();

            if (_patternDTO.StandardTextlistId == null)
            {
                sl.Add("StandardTextlistId", " Please Select Department. ");
            }
            if (_patternDTO.PatternName == null)
            {
                sl.Add("PatternName", "Pattern Name cannot be blank.");
            }
            else if (_patternDTO.IsNew ? _patternDAL.NameExist(_patternDTO.PatternName, _patternDTO.StandardTextlistId) : false == true)
            {
                sl.Add("PatternName", "Duplicate Name.");
            }

            if (_PatternLnsBLL.Count == 0)
            {
                sl.Add("PatternLnsBLL", "Pattern require at least one line detail.");
            }
            return sl;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Load(string PatternId)
    {
        try
        {
            ArrayList alPatternLns = new ArrayList();

            _patternDTO = _patternDAL.Select(PatternId, out alPatternLns);

            foreach (PatternLnsDTO PatternLnsDTO in alPatternLns)
            {
                PatternLnBLL PatternLnBLL = new PatternLnBLL();
                PatternLnBLL.SetState(PatternLnsDTO);
                _PatternLnsBLL.Add(PatternLnBLL);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string Save()
    {
        try
        {
            ArrayList alPatternLns = new ArrayList();

            foreach (PatternLnBLL PatternLnBLL in _PatternLnsBLL)
            {
                PatternLnsDTO PatternLnsDTO = new PatternLnsDTO();

                PatternLnsDTO = PatternLnBLL.GetState();

                alPatternLns.Add(PatternLnsDTO);
            }

            if (_patternDTO.IsNew == true)
            {
                return _patternDAL.Insert(_patternDTO, alPatternLns);
            }
            else
            {
                _patternDAL.Update(_patternDTO, alPatternLns, _PatternLnsBLL.deletedLgrLns);
                return _patternDTO.PatternId;

            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error while saving, Data cannot be Saved.");
        }
    }

    public void Delete(string PatternId)
    {
        string isReferenced = "";
        try
        {
            isReferenced = _patternDAL.IsReferenced(PatternId, null);
            if (isReferenced != "")
            {
                throw new Exception("Pattern is Referenced. Cannot Delete." + "\n" + isReferenced);
            }
            else
            {
                _patternDAL.Delete(PatternId);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message == "Pattern is Referenced. Cannot Delete." + "\n" + isReferenced)
                throw ex;
            else
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

    public DataTable LoadSubjects(string StandardTextListId)
    {
        try
        {
            return _patternDAL.LoadSubjects(StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

}