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

public class SubsDAL
{
    private GeneralDAL _generalDAL;

    public SubsDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~SubsDAL()
    {
        _generalDAL = null;
    }
    public DataTable Subs(string Name, string StandardTextListId, bool AllRecords)
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
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.Name Like '" + Name + "%'";
        }

        if (StandardTextListId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.StandardTextListId = '" + StandardTextListId + "'";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }

        if (AllRecords == false)
        {
            sqlCmd.CommandText = "select TOP 30 a.SubId,a.Name as Subject,a.Remarks,a.InsertedOn,a.LastUpdatedOn " +
                                " from Subs a " +
                                where;
        }
        else
        {
            sqlCmd.CommandText = "select a.SubId,a.Name as Subject,a.Remarks,a.InsertedOn,a.LastUpdatedOn " +
                                " from Subs a " +
                                where;
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }
}
