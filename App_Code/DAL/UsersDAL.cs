using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient; 
public class UsersDAL
{
    private GeneralDAL _generalDAL;

    public UsersDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~UsersDAL()
    {
        _generalDAL = null;
    }

    public DataTable Users(string firstName, string lastName, string userName)
    {
        //Escape Single Qoute
        //FirstName
        if (firstName != null)
            firstName = firstName.Trim().Replace("'", "''");

        //LastName
        if (lastName != null)
            lastName = lastName.Trim().Replace("'", "''");

        //UserName
        if (userName != null)
            userName = userName.Trim().Replace("'", "''");
        //Escape Single Qoute

        string where;
        SqlCommand sqlcmd = new SqlCommand();
        DataTable dt = new DataTable();

        try
        {
            where = null;

            //FirstName
            if (firstName != null)
            {
                if (where != null)
                {
                    where += " AND ";
                }
                where += "a.FirstName Like '" + firstName + "%'";
            }


            //LastName
            if (lastName != null)
            {
                if (where != null)
                {
                    where += " AND ";
                }
                where += "a.LastName Like '" + lastName + "%'";
            }

            //UserName
            if (userName != null)
            {
                if (where != null)
                {
                    where += " AND ";
                }
                where += "a.UserName Like '" + userName + "%'";
            }

            if (where != null)
            {
                where = " WHERE " + where;
            }
            sqlcmd.CommandText = "SELECT a.*,CASE WHEN a.IsDisabled='False' THEN 'Enabled' ELSE 'Disabled' END 'IsEnabled'" +
                                 " FROM Users a" +
                                 where +
                                 "  ORDER BY a.UserName";


            _generalDAL.OpenSQLConnection();
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.Connection = _generalDAL.ActiveSQLConnection();
            dt.Load(sqlcmd.ExecuteReader());

            return dt;
        }
        catch
        {
            throw new Exception();
        }
    }

}
