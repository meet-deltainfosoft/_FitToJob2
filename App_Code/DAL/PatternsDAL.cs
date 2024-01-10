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

/// <summary>
/// Summary description for PatternsDAL
/// </summary>
public class PatternsDAL
{
    public GeneralDAL _generalDAL;

    public PatternsDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~PatternsDAL()
    {
        _generalDAL = null;
    }
    public DataTable LoadSubjects(string StandardTextListId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select * From Subs where StandardTextListId = '" + StandardTextListId.ToString() + "' order by Name";

            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }

    public DataTable GetData(string StandardTextListId, string SubId, string PatternName, bool AllRecords)
    {

        try
        {
            string where;
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();
            where = "";

            if (StandardTextListId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " p.StandardTextListId = '" + StandardTextListId + "'";
            }

            if (SubId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " pl.SubId = '" + SubId + "'";
            }
            if (PatternName != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " p.PatternName = '" + PatternName + "'";
            }

            if (where != "")
            {
                where = " WHERE " + where;
            }

            if (AllRecords == false)
            {
                sqlCmd.CommandText = " Select top 30 p.PatternId,t.[Text] as 'StandardName',s.Name,P.PatternName,pl.* " +
                                     " from Patterns p " +
                                     " Inner join PatternLns pl on pl.PatternId = p.PatternId " +
                                     " Inner join TextLists t on t.TextListId = p.StandardTextListID " +
                                     " Inner join Subs s on s.SubID = pl.SubId " +
                                     where +
                                     " Order by p.InsertedOn desc ";
            }
            else
            {
                sqlCmd.CommandText = " Select p.PatternId,t.[Text] as 'StandardName',s.Name,P.PatternName,pl.* " +
                                     " from Patterns p " +
                                     " Inner join PatternLns pl on pl.PatternId = p.PatternId " +
                                     " Inner join TextLists t on t.TextListId = p.StandardTextListID " +
                                     " Inner join Subs s on s.SubID = pl.SubId " +
                                     where +
                                     " Order by p.InsertedOn desc ";
            }

            _generalDAL.OpenSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            dt.Load(sqlCmd.ExecuteReader());

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }
}