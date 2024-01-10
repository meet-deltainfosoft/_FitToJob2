using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class ExamCheckAllotmentsDAL
{
    private GeneralDAL _generalDAL;

    public ExamCheckAllotmentsDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~ExamCheckAllotmentsDAL()
    {
        _generalDAL = null;
    }

    public DataTable ExamCheckAllotments(string standardId, string subId, string testId, string examScheduleId)
    {
        try
        {
            string where;
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            where = "";

            if (standardId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " el.[StandardTextListId] ='" + standardId + "'";
            }

            if (subId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " el.[SubId] ='" + subId + "'";
            }

            if (testId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " el.[TestId] ='" + testId + "'";
            }

            if (examScheduleId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " el.[ExamScheduleId] ='" + examScheduleId + "'";
            }

            if (where != "")
            {
                where = " WHERE " + where;
            }

            sqlCmd.CommandText = " select el.*, t.[Text] as 'Standard', ts.TestName, es.ExamFromTime, s.Name as 'SubName' from ExamCheckAllotments el " +
                                 " inner join TextLists t on t.TextListId = el.[StandardTextListId] " +
                                 " inner join Subs s on s.SubId = el.SubId " +
                                 " inner join Tests ts on ts.TestId = el.TestId " +
                                 " inner join ExamSchedules es on es.ExamScheduleId = el.ExamScheduleId " +
                                 where + " order by el.InsertedOn desc ";

            _generalDAL.OpenSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
}
