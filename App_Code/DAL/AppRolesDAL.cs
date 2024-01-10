using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient; 

public class AppRolesDAL
{
    private GeneralDAL _generalDAL;

	public AppRolesDAL()
	{
        _generalDAL = new GeneralDAL();
    }

    ~AppRolesDAL()
    {
        _generalDAL = null;
    }

    public DataTable AppRoles(string name)
    {
        //Escape Single Qoute
        if (name != null)
            name = name.Trim().Replace("'", "''");
        //Escape Single Qoute

        string where;
        SqlCommand sqlcmd = new SqlCommand();
        DataTable dt = new DataTable();

        try
        {
            where = null;

            if (name != null)
            {
                if (where != null)
                {
                    where += " AND ";
                }
                where += "a.Name Like '" + name + "%'";
            }

            if (where != null)
            {
                where = " WHERE " + where;
            }
            sqlcmd.CommandText = "SELECT a.*" +
                                 " FROM AppRoles a" +
                                 where +
                                 "  ORDER BY a.Name";


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
