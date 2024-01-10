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

public class ExamResultPublishDAL
{
    public GeneralDAL _generalDAL;

    public ExamResultPublishDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~ExamResultPublishDAL()
    {
        _generalDAL = null;
    }

    public void Insert(ExamResultPublishDTO ExamResultPublishDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string ExamResultPublishId;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = " DECLARE  @ExamResultPublishId uniqueidentifier;" +
                                 " SET @ExamResultPublishId = NewId()" +
                                 " INSERT INTO ExamResultPublish(ExamResultPublishId, StandardTextListId, SubId, TestId, ExamScheduleId, AnsKeyFilePath, " +
                                 " IsResultPublished, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " VALUES(@ExamResultPublishId " +
                                 " ,  " + ((ExamResultPublishDTO.StandardTextListId == null) ? "NULL" : "'" + ExamResultPublishDTO.StandardTextListId.ToString() + "'") +
                                 " ,  " + ((ExamResultPublishDTO.SubId == null) ? "NULL" : "'" + ExamResultPublishDTO.SubId.ToString() + "'") +
                                 " ,  " + ((ExamResultPublishDTO.TestId == null) ? "NULL" : "'" + ExamResultPublishDTO.TestId.ToString() + "'") +
                                 " ,  " + ((ExamResultPublishDTO.ExamScheduleId == null) ? "NULL" : "'" + ExamResultPublishDTO.ExamScheduleId.ToString() + "'") +
                                 " ,  " + ((ExamResultPublishDTO.AnsKeyFilePath == null) ? "NULL" : "'" + ExamResultPublishDTO.AnsKeyFilePath.ToString() + "'") +
                                 " ,  " + ((ExamResultPublishDTO.IsResultPublished == null) ? "NULL" : "'" + Convert.ToBoolean(ExamResultPublishDTO.IsResultPublished) + "'") + " " +
                                 " , GETDATE(), GETDATE(), '" + MySession.UserUnique.ToString() + "', NULL " +
                                 " );" +
                                 " SELECT @ExamResultPublishId";

            ExamResultPublishId = sqlCmd.ExecuteScalar().ToString();

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }

    public ExamResultPublishDTO Select(string ExamResultPublishId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        ExamResultPublishDTO ExamResultPublishDTO = new ExamResultPublishDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "select a.* " +
                             " , replace(a.AnsKeyFilePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsKeyFilePathShow' " +
                             " from ExamResultPublish a WHERE ExamResultPublishId='" + ExamResultPublishId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["ExamResultPublishId"] != DBNull.Value)
                ExamResultPublishDTO.ExamResultPublishId = sqlDr["ExamResultPublishId"].ToString();

            if (sqlDr["StandardTextListId"] != DBNull.Value)
                ExamResultPublishDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();

            if (sqlDr["SubId"] != DBNull.Value)
                ExamResultPublishDTO.SubId = sqlDr["SubId"].ToString();

            if (sqlDr["TestId"] != DBNull.Value)
                ExamResultPublishDTO.TestId = sqlDr["TestId"].ToString();

            if (sqlDr["ExamScheduleId"] != DBNull.Value)
                ExamResultPublishDTO.ExamScheduleId = sqlDr["ExamScheduleId"].ToString();

            if (sqlDr["AnsKeyFilePath"] != DBNull.Value)
                ExamResultPublishDTO.AnsKeyFilePath = sqlDr["AnsKeyFilePath"].ToString();

            if (sqlDr["AnsKeyFilePathShow"] != DBNull.Value)
                ExamResultPublishDTO.AnsKeyFilePathShow = sqlDr["AnsKeyFilePathShow"].ToString();

            if (sqlDr["IsResultPublished"] != DBNull.Value)
                ExamResultPublishDTO.IsResultPublished = Convert.ToBoolean(sqlDr["IsResultPublished"]);
        }

        sqlDr.Close();

        _generalDAL.CloseSQLConnection();

        return ExamResultPublishDTO;
    }

    public void Update(ExamResultPublishDTO ExamResultPublishDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = " UPDATE ExamResultPublish SET " +
                                 " LastUpdatedOn = GETDATE()" +
                                 " ,LastUpdatedByUserId = '" + MySession.UserUnique + "' " +
                                 " ,IsResultPublished = " + ((ExamResultPublishDTO.IsResultPublished == null) ? "NULL" : "'" + Convert.ToBoolean(ExamResultPublishDTO.IsResultPublished) + "'") + "" +
                                 " WHERE ExamResultPublishId = '" + ExamResultPublishDTO.ExamResultPublishId + "'";
            sqlCmd.ExecuteNonQuery();

            if (ExamResultPublishDTO.IsChangeFile == true)
            {
                sqlCmd.CommandText = " update ExamResultPublish set AnsKeyFilePath = " + ((ExamResultPublishDTO.AnsKeyFilePath == null) ? "NULL" : "'" + ExamResultPublishDTO.AnsKeyFilePath.ToString() + "'") +
                                     " where ExamResultPublishId = '" + ExamResultPublishDTO.ExamResultPublishId + "'";
                sqlCmd.ExecuteNonQuery();
            }

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }

    public void Delete(string ExamResultPublishId)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;
        try
        {
            string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..ExamResultPublish(ExamResultPublishId, StandardTextListId, SubId, TestId, ExamScheduleId, AnsKeyFilePath, IsResultPublished, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " Select ExamResultPublishId, StandardTextListId, SubId, TestId, ExamScheduleId, AnsKeyFilePath, IsResultPublished, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId " +
                                 " FROM ExamResultPublish WHERE ExamResultPublishId = '" + ExamResultPublishId + "' ";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM ExamResultPublish WHERE ExamResultPublishId = '" + ExamResultPublishId + "' ";
            sqlCmd.ExecuteNonQuery();

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public bool NamesExist(string StandardTextListId, string SubId, string TestId, string ExamScheduleId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " SELECT COUNT(*) FROM ExamResultPublish " +
                                 " WHERE StandardTextListId = '" + StandardTextListId + "' and SubId = '" + SubId + "' " +
                                 " and TestId = '" + TestId + "' and ExamScheduleId = '" + ExamScheduleId + "'";

            if (Convert.ToInt32(sqlCmd.ExecuteScalar()) > 0)
            {
                _generalDAL.CloseSQLConnection();
                return true;
            }
            else
            {
                _generalDAL.CloseSQLConnection();
                return false;
            }
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public bool NamesExists(string StandardTextListId, string SubId, string TestId, string ExamScheduleId, string ExamResultPublishId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " SELECT COUNT(*) FROM ExamResultPublish " +
                                 " WHERE StandardTextListId = '" + StandardTextListId + "' and SubId = '" + SubId + "' " +
                                 " and TestId = '" + TestId + "' and ExamScheduleId = '" + ExamScheduleId + "' AND NOT SubId='" + SubId + "'";

            if (Convert.ToInt32(sqlCmd.ExecuteScalar()) > 0)
            {
                _generalDAL.CloseSQLConnection();
                return true;
            }
            else
            {
                _generalDAL.CloseSQLConnection();
                return false;
            }
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    #region Load Drop Down
    public DataTable LoadSubject(string StandardId)
    {
        string sql, Str;
        DataTable dt = new DataTable();
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataSet ds = new DataSet();

            sql = " Select SubId,Name From Subs where StandardTextListId = '" + StandardId + "' order by Name Asc";

            sqlCmd.CommandText = sql;
            _generalDAL.OpenSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            dt.Load(sqlCmd.ExecuteReader());
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
        return dt;
    }
    public DataTable LoadTest(string SubjectId)
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            sqlCmd.CommandText = " select TestId,TestName from Tests where SubId = '" + SubjectId + "' Order by TestName Asc ";

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
    public DataTable LoadSchedule(string TestId)
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            sqlCmd.CommandText = " select ExamScheduleId, convert(varchar(50), ExamFromTime,100) as Schedule from ExamSchedules where TestId = '" + TestId + "' Order by ExamFromTime ";

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
    public DataTable LoadSchedule(string SubjectId, string TestId)
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            sqlCmd.CommandText = " select es.*, s.Name as 'SubName', t.TestName From ExamSchedules es " +
                                 " inner join Subs s on s.SubId = es.SubId " +
                                 " inner join Tests t on t.TestId = es.TestId " +
                                 "  where es.SubId = '" + SubjectId + "' and es.TestId = '" + TestId + "' order by es.ExamFromTime desc ";

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
    #endregion

    public DataTable ExamResultPublish(string standardId, string subId, string testId, string examScheduleId)
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

            sqlCmd.CommandText = " select el.*, t.[Text] as 'Standard', ts.TestName, es.ExamFromTime, s.Name as 'SubName' from ExamResultPublish el " +
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
