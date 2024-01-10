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

/// <summary>
/// Summary description for LoginBLL
/// </summary>
public class LoginBLL
{
    private string _Login;
    private string _Password;
    private LoginDLL _LoginDAL;
    private Boolean _ValidateUser;
    private GeneralDAL _generalDAL;

	public LoginBLL()
	{
        _LoginDAL = new LoginDLL();
        _generalDAL = new GeneralDAL();
	}

    ~LoginBLL()
    {
        _LoginDAL = null;
        _generalDAL = null;
    }
    public string Login
    {
        set
        {
            _Login = value;
        }
        get
        {
            return _Login;
        }
    }

    public string Password
    {
        set
        {
            _Password = value;
        }
        get
        {
            return _Password;
        }
    }

    public Boolean ValidateUser
    { 
        get
        {
            return _LoginDAL.ValidateUser(_Login,_Password);
        }
    }

    public Boolean ValidateRegisterUser
    { 
        get
        {
            return _LoginDAL.ValidateRegisterUser(_Login, _Password);
        }
    }

    public string GetUniqueCode
    {
        get
        {
            return _LoginDAL.GetUniqueCode(_Login);
        }
    }

    public string GetRegestrationId
    {
        get
        {
            return _LoginDAL.GetRegestrationId(_Login);
        }
    }

    public string GetDeptId
    {
        get
        {
            return _LoginDAL.GetDeptId(_Login);
        }
    }
    public bool GetIsAdmin
    {
        get
        {
            return _LoginDAL.GetIsAdmin(_Login);
        }
    }


    public DataTable DeptName()
    {
        return _generalDAL.Depts(_Login);
    }

    public DataTable GetYear()
    {
        return _LoginDAL.GetYear();
    }
    public DataTable getFrmDateToDate(string year)
    {
        return _LoginDAL.getFrmDateToDate(year);
    }
}
