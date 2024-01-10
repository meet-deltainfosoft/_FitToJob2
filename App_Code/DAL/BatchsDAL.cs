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

public class BatchsDAL
{
    public GeneralDAL _generalDAL;

	public BatchsDAL()
	{
        _generalDAL = new GeneralDAL();
	}
    ~BatchsDAL()
    {
        _generalDAL = null;
    }


   
    public DataTable BatchList(string StandardTextListId, bool AllRecords)
    {
        
        string where;
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        where = "";

        //Name
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
            sqlCmd.CommandText = "select Top 30 a.BatchId , a.BatchName,t.Text as StandardName, a.Insertedon, a.LastUpdatedon  from Batchs a inner join TextLists t on t.TextListId = a.StandardTextListId " +
                                where +
                                " order by a.insertedon desc";
        }
        else
        {
            sqlCmd.CommandText = "select a.BatchId , a.BatchName,t.Text as StandardName, a.Insertedon, a.LastUpdatedon  from Batchs a inner join TextLists t on t.TextListId = a.StandardTextListId " +
                                where +
                                " order by a.insertedon desc";
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }

}