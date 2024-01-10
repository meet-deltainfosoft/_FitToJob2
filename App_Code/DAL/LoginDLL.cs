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
using System.Data.SqlClient;
using System.Collections.Generic;

/// <summary>
/// Summary description for LoginDLL
/// </summary>
public class LoginDLL
{
    private GeneralDAL _generalDAL;
    public LoginDLL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~LoginDLL()
    {
        _generalDAL = null;
    }

    public string GetUniqueCode(string Login)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        string Valid;

        Login = Login.Replace("'", "''");
        Login = Login.Replace(";", "");
        Login = Login.Replace("=", "");


        sqlCmd.CommandText = "Select UserId from Users_ Where UserName  ='" + Login + "'";


        _generalDAL.OpenSQLConnection();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        if (sqlCmd.ExecuteScalar() == null)
        {
            Valid = "";
        }
        else
        {
            Valid = sqlCmd.ExecuteScalar().ToString();
        }

        _generalDAL.CloseSQLConnection();
        return Valid;
    }

    public bool GetIsAdmin(string Login)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        bool Valid;

        Login = Login.Replace("'", "''");
        Login = Login.Replace(";", "");
        Login = Login.Replace("=", "");


        sqlCmd.CommandText = "Select isnull(IsAdmin,0) from Users_ Where UserName  ='" + Login + "'";


        _generalDAL.OpenSQLConnection();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        if (sqlCmd.ExecuteScalar().ToString() == false.ToString())
        {
            Valid = false;
        }
        else
        {
            Valid = true;
        }

        _generalDAL.CloseSQLConnection();
        return Valid;
    }

    //by kinnari for DeptId 25012012
    public string GetDeptId(string Login)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        string Valid;

        Login = Login.Replace("'", "''");
        Login = Login.Replace(";", "");
        Login = Login.Replace("=", "");


        sqlCmd.CommandText = "Select DeptId from Users Where UserName  ='" + Login + "'";


        _generalDAL.OpenSQLConnection();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        if (sqlCmd.ExecuteScalar() == null)
        {
            Valid = "";
        }
        else
        {
            Valid = sqlCmd.ExecuteScalar().ToString();
        }

        _generalDAL.CloseSQLConnection();
        return Valid;
    }
    //
    public Boolean ValidateUser(string Login, string Password)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        string Valid;

        Login = Login.Replace("'", "''");
        Login = Login.Replace(";", "");
        Login = Login.Replace("=", "");

        Password = Password.Replace("'", "''");
        Password = Password.Replace(";", "");
        Password = Password.Replace("=", "");


        sqlCmd.CommandText = "Select Password from Users_ Where UserName  ='" + Login + "' COLLATE SQL_Latin1_General_Cp1_CS_AS ";


        _generalDAL.OpenSQLConnection();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        if (sqlCmd.ExecuteScalar() == null)
        {
            Valid = "";
        }
        else
        {
            Valid = sqlCmd.ExecuteScalar().ToString();
        }

        _generalDAL.CloseSQLConnection();

        if (Valid == "")
        {
            return false;
        }
        else
        {
            if (Valid == Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public DataTable GetYear()
    {
        SqlCommand sqlcmd = new SqlCommand();
        DataTable dt = new DataTable();

        _generalDAL.OpenSQLConnection();


        sqlcmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlcmd.CommandType = CommandType.Text;
        sqlcmd.CommandText = "SELECT YearId ,[YEAR] from FiscalYear Order By Prefix desc ";
        dt.Load(sqlcmd.ExecuteReader());

        _generalDAL.CloseSQLConnection();

        return dt;
    }
    public DataTable getFrmDateToDate(string year)
    {
        SqlCommand sqlcmd = new SqlCommand();
        DataTable dt = new DataTable();

        _generalDAL.OpenSQLConnection();


        sqlcmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlcmd.CommandType = CommandType.Text;
        sqlcmd.CommandText = "select f.FromDt,f.ToDt from FiscalYear f where f.[Year]='" + year + "'";
        dt.Load(sqlcmd.ExecuteReader());

        _generalDAL.CloseSQLConnection();

        return dt;
    }

    public Boolean ValidateRegisterUser(string Login, string Password)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        string Valid;

        Login = Login.Replace("'", "''");
        Login = Login.Replace(";", "");
        Login = Login.Replace("=", "");

        Password = Password.Replace("'", "''");
        Password = Password.Replace(";", "");
        Password = Password.Replace("=", "");


        sqlCmd.CommandText = "Select Password from Registration Where EmailId  = '" + Login + "' COLLATE SQL_Latin1_General_Cp1_CS_AS ";


        _generalDAL.OpenSQLConnection();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        if (sqlCmd.ExecuteScalar() == null)
        {
            Valid = "";
        }
        else
        {
            Valid = sqlCmd.ExecuteScalar().ToString();
        }

        _generalDAL.CloseSQLConnection();

        if (Valid == "")
        {
            return false;
        }
        else
        {
            if (Valid == Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public string GetRegestrationId(string Login)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        string Valid;

        Login = Login.Replace("'", "''");
        Login = Login.Replace(";", "");
        Login = Login.Replace("=", "");


        sqlCmd.CommandText = "Select RegistrationId from Registration Where EmailId  = '" + Login + "'";


        _generalDAL.OpenSQLConnection();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        if (sqlCmd.ExecuteScalar() == null)
        {
            Valid = "";
        }
        else
        {
            Valid = sqlCmd.ExecuteScalar().ToString();
        }

        _generalDAL.CloseSQLConnection();
        return Valid;
    }
}
