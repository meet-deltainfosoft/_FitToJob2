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

public class QuesDAL
{
    private GeneralDAL _generalDAL;

    public QuesDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~QuesDAL()
    {
        _generalDAL = null;
    }

    public DataTable Question(string Question, string StandardId, string SubId, string TestId, bool AllRecords)
    {
        if (Question != null)
            Question = Question.Replace("'", "''");

        string where;
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        where = "";

        if (StandardId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " b.StandardTextListId =  '" + StandardId + "'";
        }

        if (SubId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " b.SubId =  '" + SubId + "'";
        }

        if (TestId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.TestId =  '" + TestId + "'";
        }

        //Question
        if (Question != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.Que Like  N'" + Question + "%'";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }

        if (AllRecords == false)
        {
            sqlCmd.CommandText = " select Top 30 a.QueId,isnull(a.Que,a.ImageNameQus) as Question ,isnull(a.A1,ImageNameA1) as A1,isnull(a.A2,ImageNameA2) as A2, " +
                                 " isnull(a.A3,ImageNameA3) as A3,isnull(a.A4,ImageNameA4)  as A4,b.SubId,b.Name as Subject " +
                                 " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                 " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                 " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                 " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                 " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                 " , a.SrNo, 'R:'+convert(varchar, RightMarks) +  '|W:' + convert(varchar, WrongMarks) + '|S:' + convert(varchar, NonMarks) as 'Marks', t.[Text] as 'Standard' from Ques a " +
                                 " left join Subs b on b.SubId = a.SubId " +
                                 " left join Textlists t on t.Textlistid = b.StandardTextListId " +
                                 where +
                                 " Order By a.SrNo, a.InsertedOn ";
        }
        else
        {
            sqlCmd.CommandText = " select  a.QueId,isnull(a.Que,a.ImageNameQus) as Question ,isnull(a.A1,ImageNameA1) as A1,isnull(a.A2,ImageNameA2) as A2, " +
                                  " isnull(a.A3,ImageNameA3) as A3,isnull(a.A4,ImageNameA4)  as A4,b.SubId,b.Name as Subject " +
                                  " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                  " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                  " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                  " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                  " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                  " , a.SrNo, 'R:'+convert(varchar, RightMarks) +  '|W:' + convert(varchar, WrongMarks) + '|S:' + convert(varchar, NonMarks) as 'Marks', t.[Text] as 'Standard' from Ques a " +
                                  " left join Subs b on b.SubId = a.SubId " +
                                  " left join Textlists t on t.Textlistid = b.StandardTextListId " +
                                  where +
                                  " Order By a.SrNo, a.InsertedOn ";
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }
}
