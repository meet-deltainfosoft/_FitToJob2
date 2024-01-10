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
/// Summary description for EBookPDFsDAL
/// </summary>
public class EBookPDFsDAL
{
    public GeneralDAL _generalDAL;

    public EBookPDFsDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~EBookPDFsDAL()
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

    public DataTable LoadEBookPDF(string SubId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select Distinct EBookPDFId, EBookPDFName  From EBookPDFs where SubId = '" + SubId.ToString() + "' order by EBookPDFName";

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

    public DataTable LoadPeriod(string EBookPDFId, string SubId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select Distinct convert(DOUBLE PRECISION,PeriodNo) as PeriodNo From Chapters" +
                " where ChapterId = '" + EBookPDFId.ToString() + "' and SubId = '" + SubId + "'" +
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

    public DataTable EBookPDFList(string StandardTextListId, string SubId, string EBookPDFId, string PeriodNo, bool AllRecords)
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
        if (EBookPDFId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " c.EBookPDFId = '" + EBookPDFId + "'";
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
            sqlCmd.CommandText = " select Top 30 cp.*, t.[text] as 'StandardName', s.Name as SubName from EBookPDFs cp " +
                                 " left join TextLists t on t.TextListId = cp.StandardTextListId " +
                                 " left join Subs s on s.SubId = cp.SubId  " +
                                where +
                                " order by cp.insertedon desc";
        }
        else
        {
            sqlCmd.CommandText = " select cp.*, t.[text] as 'StandardName', s.Name as SubName  from EBookPDFs cp " +
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