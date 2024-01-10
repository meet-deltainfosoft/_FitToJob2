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
/// Summary description for ChapterPDFsDAL
/// </summary>
public class ChapterPDFsDAL
{
    public GeneralDAL _generalDAL;

    public ChapterPDFsDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~ChapterPDFsDAL()
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
            sqlCmd.CommandText = "select Distinct ChapterId, ChapterName  From Chapters where SubId = '" + SubId.ToString() + "' order by ChapterName";

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

    public DataTable LoadPeriod(string ChapterId, string SubId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select Distinct convert(DOUBLE PRECISION,PeriodNo) as PeriodNo From Chapters" +
                " where ChapterId = '" + ChapterId.ToString() + "' and SubId = '" + SubId + "'" +
                " order by convert(DOUBLE PRECISION,PeriodNo)";

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

    public DataTable ChapterList(string StandardTextListId, string SubId, string ChapterId, string PeriodNo, bool AllRecords)
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
            where += " cp.StandardTextListId = '" + StandardTextListId + "'";
        }

        if (SubId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " cp.SubId = '" + SubId + "'";
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
            where += " cp.PeriodNo= '" + PeriodNo + "'";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }

        if (AllRecords == false)
        {
            sqlCmd.CommandText = " select Top 30 cp.ChapterPDFId, c.ChapterName,cp.PeriodNo,t.text as StandardName , s.Name as SubName,cp.SrNo, cp.FileLink,cp.Filename,cp.insertedon,cp.Lastupdatedon  from ChapterPDFs cp " +
                                 " inner join Chapters c on c.chapterId = cp.ChapterId " +
                                 " left join TextLists t on t.TextListId = cp.StandardTextListId " +
                                 " left join Subs s on s.SubId = cp.SubId  " +
                                where +
                                " order by cp.insertedon desc";
        }
        else
        {
            sqlCmd.CommandText = " select cp.ChapterPDFId, c.ChapterName,cp.PeriodNo,t.text as StandardName , s.Name as SubName,cp.SrNo, cp.FileLink,cp.Filename,cp.insertedon,cp.Lastupdatedon  from ChapterPDFs cp " +
                                 " inner join Chapters c on c.chapterId = cp.ChapterId " +
                                 " left join TextLists t on t.TextListId = cp.StandardTextListId " +
                                 " left join Subs s on s.SubId = cp.SubId  " +
                                where +
                                " order by cp.insertedon desc";
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }
}