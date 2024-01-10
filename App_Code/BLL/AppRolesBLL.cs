using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class AppRolesBLL
{
    private string _name;

    private AppRolesDAL _appRolesDAL;

	public AppRolesBLL()
	{
        _appRolesDAL = new AppRolesDAL();
    }

    ~AppRolesBLL()
    {
        _appRolesDAL = null;
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

    public DataTable AppRoles
    {
        get
        {
            return _appRolesDAL.AppRoles(_name);
        }
    }
}
