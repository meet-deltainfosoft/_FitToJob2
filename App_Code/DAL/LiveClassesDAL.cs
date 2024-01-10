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

public class LiveClassesDAL
{
    public GeneralDAL _generalDAL;

    public LiveClassesDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~LiveClassesDAL()
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

    public DataTable LiveClassList(string StandardTextListId, string SubId, bool AllRecords)
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

        if (where != "")
        {
            where = " WHERE " + where;
        }

        if (AllRecords == false)
        {
            sqlCmd.CommandText = " Select Top 30 a.LiveClassId, a.Title, a.TopicName ,t.Text as StandardName, s.Name as SubjectName, a.Link, " +
                                 " a.FromTime,a.ToTime,a.Date,a.InsertedOn,a.LastUpdatedOn " +
                                 " from LiveClasses a " +
                                 " left join TextLists t on t.TextListId= a.StandardTextListId " +
                                 " left join Subs s on s.SubId = a.SubId " +
                                where +
                                " order by a.insertedon desc ";
        }
        else
        {
            sqlCmd.CommandText = " Select a.LiveClassId, a.Title, a.TopicName ,t.Text as StandardName, s.Name as SubjectName, a.Link,  " +
                                 " a.FromTime,a.ToTime,a.Date,a.InsertedOn, a.LastUpdatedOn" +
                                 " from LiveClasses a " +
                                 " left join TextLists t on t.TextListId= a.StandardTextListId " +
                                 " left join Subs s on s.SubId = a.SubId " +
                                where +
                                " order by a.insertedon desc ";
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }
}