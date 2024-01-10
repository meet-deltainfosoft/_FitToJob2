using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MySession
/// </summary>
public class MySession
{
	public MySession()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string UserID
    {
        get
        {
            if ((HttpContext.Current.Session["USERID"] == null))
            {
                return null;
            }
            return Convert.ToString(HttpContext.Current.Session["USERID"]);
        }
        set { HttpContext.Current.Session["USERID"] = value; }
    }

    public static bool IsAdmin
    {
        get
        {
            if ((HttpContext.Current.Session["IsAdmin"] == null))
            {
                return false;
            }
            return Convert.ToBoolean(HttpContext.Current.Session["IsAdmin"]);
        }
        set { HttpContext.Current.Session["IsAdmin"] = value; }
    }

    public static string UserUnique
    {
        get
        {
            if ((HttpContext.Current.Session["UserUnique"] == null))
            {
                return null;
            }
            return Convert.ToString(HttpContext.Current.Session["UserUnique"]);
        }
        set { HttpContext.Current.Session["UserUnique"] = value; }
    }

    //by kinnari 25012012 for department 
    public static string DeptId
    {
        get
        {
            if ((HttpContext.Current.Session["DeptId"] == null))
            {
                return null;
            }
            return Convert.ToString(HttpContext.Current.Session["DeptId"]);
        }
        set { HttpContext.Current.Session["DeptId"] = value; }
    }

    public static string LgrId
    {
        get
        {
            if ((HttpContext.Current.Session["LgrId"] == null))
            {
                return null;
            }
            return Convert.ToString(HttpContext.Current.Session["LgrId"]);
        }
        set { HttpContext.Current.Session["LgrId"] = value; }
    }

    public static string DeptName
    {
        get
        {
            if ((HttpContext.Current.Session["DeptName"] == null))
            {
                return null;
            }
            return Convert.ToString(HttpContext.Current.Session["DeptName"]);
        }
        set
        {
            HttpContext.Current.Session["DeptName"] = value;
        }
    }
    public static string DBNo
    {
        get
        {
            if ((HttpContext.Current.Session["DBNo"] == null))
            {
                return null;
            }
            return Convert.ToString(HttpContext.Current.Session["DBNo"]);
        }
        set
        {
            HttpContext.Current.Session["DBNo"] = value;
        }
    }
    public static string AcademicYearId
    {
        get
        {
            if ((HttpContext.Current.Session["AcademicYearId"] == null))
            {
                return null;
            }
            return Convert.ToString(HttpContext.Current.Session["AcademicYearId"]);
        }
        set { HttpContext.Current.Session["AcademicYearId"] = value; }
    }
    public static DateTime? FromDate
    {
        get
        {
            if ((HttpContext.Current.Session["FromDate"] == null))
            {
                return null;
            }
            return Convert.ToDateTime(HttpContext.Current.Session["FromDate"]);
        }
        set { HttpContext.Current.Session["FromDate"] = value; }
    }
    public static DateTime? ToDate
    {
        get
        {
            if ((HttpContext.Current.Session["ToDate"] == null))
            {
                return null;
            }
            return Convert.ToDateTime(HttpContext.Current.Session["ToDate"]);
        }
        set { HttpContext.Current.Session["ToDate"] = value; }
    }

    public static string IsDelete
    {
        get
        {
            if ((HttpContext.Current.Session["IsDelete"] == null))
            {
                return null;
            }
            return Convert.ToString(HttpContext.Current.Session["IsDelete"]);
        }
        set { HttpContext.Current.Session["IsDelete"] = value; }
    }
    public static string DivTextListId
    {
        get
        {
            if ((HttpContext.Current.Session["DivTextListId"] == null))
            {
                return null;
            }
            return Convert.ToString(HttpContext.Current.Session["DivTextListId"]);
        }
        set { HttpContext.Current.Session["DivTextListId"] = value; }
    }
}

