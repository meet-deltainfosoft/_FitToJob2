using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class UsersBLL
{
    private string _firstName;
    private string _lastName;
    private string _userName;

    private UsersDAL _usersDAL;

	public UsersBLL()
	{
        _usersDAL = new UsersDAL();
    }

    ~UsersBLL()
    {
        _usersDAL = null;
    }

    public string FirstName
    {
        get
        {
            return _firstName;
        }
        set
        {
            _firstName = value;
        }
    }

    public string LastName
    {
        get
        {
            return _lastName;
        }
        set
        {
            _lastName = value;
        }
    }

    public string UserName
    {
        get
        {
            return _userName;
        }
        set
        {
            _userName = value;
        }
    }

    public DataTable Users
    {
        get
        {
            return _usersDAL.Users(_firstName, _lastName, _userName);
        }
    }

}
