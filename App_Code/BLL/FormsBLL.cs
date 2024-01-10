using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class FormsBLL
{
    private string _moduleTextListId;
    private string _name;

    private FormsDAL _formsDAL;
    private GeneralDAL _generalDAL;

	public FormsBLL()
	{
        _formsDAL = new FormsDAL();
        _generalDAL = new GeneralDAL();
	}

    ~FormsBLL()
    {
        _formsDAL = null;
        _generalDAL = null;
    }

    public string ModuleTextListId
    {
        get
        {
            return _moduleTextListId;
        }
        set 
        {
            _moduleTextListId = value;
        }
    }

    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }

    public DataTable Modules()
    {
        return _generalDAL.TextList("Module");
    }

    public DataTable Forms
    {
        get
        {
            return _formsDAL.Forms(_moduleTextListId, _name); 
        }
    }
}
