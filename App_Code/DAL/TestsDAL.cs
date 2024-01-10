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
using System.Collections;

public class TestsDAL
{
    private GeneralDAL _generalDAL;

    public TestsDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~TestsDAL()
    {
        _generalDAL = null;
    }
    public DataTable Tests(string Name, bool AllRecords)
    {
        if (Name != null)
            Name = Name.Replace("'", "''");

        string where;
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        where = "";

        //Name
        if (Name != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += " And a.TestName Like '" + Name + "%'";
        }

        //if (where != "")
        //{
        //    where = " WHERE " + where;
        //}

        if (AllRecords == false)
        {
            sqlCmd.CommandText = "select TOP 30 * " +
                                " from Tests a " +
                                " where SubId is not null " +
                                where;
        }
        else
        {
            sqlCmd.CommandText = "select * " +
                                " from Tests a " +
                                " where SubId is not null " +
                                where;
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }
}
