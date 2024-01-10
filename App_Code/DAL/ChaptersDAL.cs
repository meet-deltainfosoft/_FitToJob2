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
/// Summary description for ChaptersDAL
/// </summary>
public class ChaptersDAL
{
    public GeneralDAL _generalDAL;

    public ChaptersDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~ChaptersDAL()
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
    public DataTable ChapterList(string StandardTextListId, string SubId, string PeriodNo, bool AllRecords)
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

        if (SubId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.SubId = '" + SubId + "'";
        }


        if (PeriodNo != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.PeriodNo = '" + PeriodNo + "'";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }

        if (AllRecords == false)
        {
            sqlCmd.CommandText = "select Top 30 a.ChapterId , a.ChapterName,a.PeriodNo,a.SrNo,s.Name as SubName,t.Text as StandardName, a.Insertedon, a.LastUpdatedon  from Chapters a inner join Subs s on s.SubId = a.SubId inner join TextLists t on t.TextListId = a.StandardTextListId " +
                                where +
                                " order by a.insertedon desc";
        }
        else
        {
            sqlCmd.CommandText = "select a.ChapterId , a.ChapterName,a.PeriodNo,a.SrNo,s.Name as SubName,t.Text as StandardName, a.Insertedon, a.LastUpdatedon  from Chapters a inner join Subs s on s.SubId = a.SubId inner join TextLists t on t.TextListId = a.StandardTextListId " +
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