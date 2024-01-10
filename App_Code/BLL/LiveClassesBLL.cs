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

public class LiveClassesBLL
{
    public string _StandardTextListId;
    public string _SubId;
    public bool _AllRecord;

    private LiveClassesDAL _LiveClassesDAL;
	public LiveClassesBLL()
	{
        _LiveClassesDAL = new LiveClassesDAL();
	}
    ~LiveClassesBLL()
    {
        _LiveClassesDAL = null;
    }
    public string StandardId
    {
        set
        {
            _StandardTextListId = value;
        }
    }
    public string SubId
    {
        set
        {
            _SubId = value;
        }
    }
   
    public bool AllRecord
    {
        set
        {
            _AllRecord = value;
        }
    }
    public DataTable LoadStandard()
    {
        try
        {
            GeneralDAL _GeneralDAL = new GeneralDAL();
            return _GeneralDAL.TextList("Standard");
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
            return _LiveClassesDAL.LoadSubjects(_StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
   
    public DataTable LiveClassList()
    {
        return _LiveClassesDAL.LiveClassList(_StandardTextListId, _SubId, _AllRecord);
    }
}