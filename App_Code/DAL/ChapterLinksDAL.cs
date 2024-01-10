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
/// Summary description for ChapterLinksDAL
/// </summary>
public class ChapterLinksDAL
{
    public GeneralDAL _generalDAL;

    public ChapterLinksDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~ChapterLinksDAL()
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

    public DataTable LoadChepter(string SubId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select distinct ChapterId, ChapterName  From Chapters where SubId = '" + SubId.ToString() + "' order by ChapterName";

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

    public DataTable ChapterVedioList(string StandardTextListId, string SubId, string ChapterId, string PeriodNo, bool AllRecords)
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
            where += " cl.StandardTextListId = '" + StandardTextListId + "'";
        }

        if (SubId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " cl.SubId = '" + SubId + "'";
        }
        if (ChapterId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " c.ChapterId = '" + ChapterId + "'";
        }

        if (PeriodNo != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " cl.PeriodNo = '" + PeriodNo + "'";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }

        if (AllRecords == false)
        {
            sqlCmd.CommandText = " Select Top 30 cl.chapterLinkId, c.ChapterName, t.Text as StandardName, s.Name as SubjectName,cl.PeriodNo, cl.SrNo, cl.LinkName, cl.Link, cl.InsertedOn, cl.LastUpdatedOn from ChapterLinks cl " +
                                 " left join TextLists t on t.TextListId= cl.StandardTextListId " +
                                 " left join Subs s on s.SubId = cl.SubId " +
                                 " inner join Chapters c on c.chapterid = cl.ChapterId " +
                                where +
                                " order by cl.insertedon desc ";
        }
        else
        {
            sqlCmd.CommandText = " Select cl.chapterLinkId, c.ChapterName, t.Text as StandardName, s.Name as SubjectName, cl.SrNo,cl.PeriodNo, cl.LinkName, cl.Link, cl.InsertedOn, cl.LastUpdatedOn from ChapterLinks cl " +
                                 " left join TextLists t on t.TextListId= cl.StandardTextListId " +
                                 " left join Subs s on s.SubId = cl.SubId " +
                                 " inner join Chapters c on c.chapterid = cl.ChapterId " +
                                where +
                                " order by cl.insertedon desc ";
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }

    public DataTable LoadPeriod(string ChapterId, string SubId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select Distinct convert(DOUBLE PRECISION,PeriodNo) as PeriodNo From Chapters where ChapterId = '" + ChapterId.ToString() + "' and SubId = '" + SubId.ToString() + "' order by convert(DOUBLE PRECISION,PeriodNo)";

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
}