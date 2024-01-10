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
/// Summary description for ChapterVediosDAL
/// </summary>
public class ChapterVediosDAL
{
    public GeneralDAL _generalDAL;

    public ChapterVediosDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~ChapterVediosDAL()
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

    public DataTable ChapterVedioList(string StandardTextListId, string SubId, string ChapterId, string PeriodNo, bool AllRecords, bool IsDisabled)
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
            where += " a.PeriodNo = '" + PeriodNo + "'";
        }

        if (IsDisabled != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " isnull(a.IsDisabled, 0) = '" + IsDisabled + "' ";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }

        if (AllRecords == false)
        {
            sqlCmd.CommandText = " select Top 30 a.ChapterVideoId,c.ChapterName,a.PeriodNo,t.Text as StandardName , s.Name as SubjectName, a.SrNo,a.VideoName,a.VideoLink,a.Insertedon,a.LastUpdatedOn from ChapterVideos a " +
                                 " inner join Chapters c on c.ChapterId = a.chapterId " +
                                 " left join TextLists t on t.TextListid = a.StandardTextListid " +
                                 " left join Subs s on s.SubId = a.Subid " +
                                where +
                                " order by a.Insertedon desc ";
        }
        else
        {
            sqlCmd.CommandText = " select a.ChapterVideoId,c.ChapterName,a.PeriodNo,t.Text as StandardName , s.Name as SubjectName, a.SrNo,a.VideoName,a.VideoLink,a.Insertedon,a.LastUpdatedOn from ChapterVideos a " +
                                 " inner join Chapters c on c.ChapterId = a.chapterId " +
                                 " left join TextLists t on t.TextListid = a.StandardTextListid " +
                                 " left join Subs s on s.SubId = a.Subid " +
                                where +
                                " order by a.Insertedon desc ";
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
            sqlCmd.CommandText = "select Distinct convert(DOUBLE PRECISION,PeriodNo) as PeriodNo From Chapters where ChapterId = '" + ChapterId.ToString() + "' and SubId = '" + SubId + "' order by convert(DOUBLE PRECISION,PeriodNo)";

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