using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class MasterValuesDAL
{
    private GeneralDAL _generalDAL;

    public MasterValuesDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~MasterValuesDAL()
    {
        _generalDAL = null;
    }

    public DataTable Groups()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        sqlCmd.CommandText = "SELECT DISTINCT  [Group] 'Group1',[Group] FROM TextLists Order BY [GROUP]";

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        _generalDAL.CloseSQLConnection();

        return dt;
    }

    public DataTable MasterValues(string Group)
    {
        //Escape single quote
        if (Group != null)
            Group = Group.Replace("'", "''");
        //Escape single quote

        string where;
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        where = "";

        //Group
        if (Group != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.[Group] ='" + Group + "'";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }



        sqlCmd.CommandText = "SELECT a.* FROM TextLists a" +
                             where + " ORDER BY [Group],[Text]";


        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        _generalDAL.CloseSQLConnection();

        return dt;
    }


    public DataTable ExportToExcel(string Group)
    {
     
        if (Group != null)
            Group = Group.Replace("'", "''");
    

        string where;
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        where = "";

        //Group
        if (Group != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.[Group] ='" + Group + "'";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }

        sqlCmd.CommandText = " SELECT a.*," +
                             " u1.FirstName + ' ' + u1.LastName as InsertedByUserName,u2.FirstName + ' ' + u2.LastName as  LastUpdatedByUserName " +
                             " FROM TextLists a" +
                             " Left join Users u1 on u1.UserId = a.InsertedByUserId " +
                             " Left join Users u2 on u2.UserId = a.LastUpdatedByUserID " +
                             where + " ORDER BY [Group],[Text]";


        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        _generalDAL.CloseSQLConnection();

        return dt;
    }

    

}
