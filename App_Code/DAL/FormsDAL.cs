using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient; 

public class FormsDAL
{
    private GeneralDAL _generalDAL;

	public FormsDAL()
	{
        _generalDAL = new GeneralDAL(); 
	}

    ~FormsDAL()
    {
        _generalDAL = null;
    }

    public DataTable Forms(string moduleTextListId, string name)
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
            where = "";

            if (moduleTextListId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += "a.ModuleTextListId ='" + moduleTextListId + "'";
            }

            if (name != null)
            {
                if (where != null)
                {
                    where += " AND ";
                }
                where += "a.Name Like '" + name + "%'";
            }

            if (where != "")
            {
                where = " WHERE " + where;
            }
            sqlcmd.CommandText = "SELECT a.*,b.[Text] 'ModuleName'" +
                                 " FROM Forms a" + 
                                 " LEFT JOIN TextLists b ON a.ModuleTextListId = b.TextListId" +
                                 where +
                                 "  ORDER BY b.[Text],a.Name";


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
