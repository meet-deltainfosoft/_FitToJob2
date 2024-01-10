using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public struct UserDTO
{
    public string UserId;
    public string FirstName;
    public string LastName;
    public string UserName;
    public string Password;
    public bool IsDisabled;
    public string DeptId; // add by kinnari for department dropdown list

    public bool IsNew;
}
