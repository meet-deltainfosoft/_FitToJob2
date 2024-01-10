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

public class BatchBLL
{
    private BatchDTO _BatchDTO;
    private BatchDAL _BatchDAL;

	public BatchBLL()
	{
        _BatchDAL = new BatchDAL();
        _BatchDTO = new BatchDTO();

        _BatchDTO.IsNew = true;
	}
    public BatchBLL(string BatchId)
        : this()
    {
        _BatchDTO.IsNew = false;
        Load(BatchId);
    }

    ~BatchBLL()
    {
        _BatchDAL = null;
    }

    public string BatchId
    {
        get
        {
            return _BatchDTO.BatchId;
        }
        set
        {
            _BatchDTO.BatchId = value;
        }
    }

    public string StandardTextListId
    {
        get
        {
            return _BatchDTO.StandardTextListId;
        }
        set
        {
            _BatchDTO.StandardTextListId = value;
        }
    }

    public string BatchName
    {
        get
        {
            return _BatchDTO.BatchName;
        }
        set
        {
            _BatchDTO.BatchName = value;
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

    public void Save()
    {
        try
        {
            if (_BatchDTO.IsNew == true)
            {
                _BatchDAL.Insert(_BatchDTO);
            }
            else
            {
                _BatchDAL.Update(_BatchDTO);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Load(string BatchId)
    {
        ArrayList alSubLns = new ArrayList();
        _BatchDTO = _BatchDAL.Select(BatchId);
    }

    public void Delete(string BatchId)
    {
        //string isReferenced = _BatchDAL.IsReferenced(BatchId);

        try
        {
            //if (isReferenced != "")
            //{
            //    throw new Exception("Exam Schedule is Referenced. Cannot Delete." + "\n" + isReferenced);
            //}
            //else
            //{
                _BatchDAL.Delete(BatchId);
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

        if (_BatchDTO.StandardTextListId == null)
        {
            sl.Add("StandardTextListId", "Please select Standard.");
        }
        return sl;
    }
}