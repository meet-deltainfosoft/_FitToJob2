using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class ExamCheckAllotmentDAL
{
    private GeneralDAL _generalDAL;

    public ExamCheckAllotmentDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~ExamCheckAllotmentDAL()
    {
        _generalDAL = null;
    }

    public ExamCheckAllotmentDTO Select(string ExamCheckAllotmentId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader sqlDr;
            ExamCheckAllotmentDTO examCheckAllotmentDTO = new ExamCheckAllotmentDTO();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM ExamCheckAllotments WHERE ExamCheckAllotmentId = '" + ExamCheckAllotmentId + "'";
            sqlDr = sqlCmd.ExecuteReader();

            while (sqlDr.Read())
            {
                examCheckAllotmentDTO.ExamCheckAllotmentId = sqlDr["ExamCheckAllotmentId"].ToString();

                if (sqlDr["StandardTextListId"] != DBNull.Value)
                    examCheckAllotmentDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();

                if (sqlDr["SubId"] != DBNull.Value)
                    examCheckAllotmentDTO.SubId = sqlDr["SubId"].ToString();

                if (sqlDr["TestId"] != DBNull.Value)
                    examCheckAllotmentDTO.TestId = sqlDr["TestId"].ToString();

                if (sqlDr["ExamScheduleId"] != DBNull.Value)
                    examCheckAllotmentDTO.ExamScheduleId = sqlDr["ExamScheduleId"].ToString();
            }

            sqlDr.Close();

            //sqlCmd.CommandText = " select el.* from ExamCheckAllotmentLns el " +
            //                     " inner join Ques q on q.QueId = el.QueId " +
            //                     " where el.ExamCheckAllotmentId = '" + ExamCheckAllotmentId + "' ";

            sqlCmd.CommandText = " select eln.ExamCheckAllotmentLnId, eln.ExamCheckAllotmentId, " +
                                 " convert(uniqueidentifier, eln.UserId) as UserId, eln.QueId, eln.InsertedOn, eln.LastUpdatedOn, " +
                                 " eln.InsertedByUserId, eln.LastUpdatedByUserId, q.Que,  " +
                                 " replace(q.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                 " ,q.RightMarks,q.WrongMarks, " +
                                 " q.NonMarks,q.QueType, q.SrNo  from ExamCheckAllotments el " +
                                 " inner join ExamCheckAllotmentLns eln on eln.ExamCheckAllotmentId = el.ExamCheckAllotmentId   " +
                                 " inner join Ques q on q.QueId = eln.QueId   " +
                                 " WHERE el.ExamCheckAllotmentId = '" + ExamCheckAllotmentId + "' " +
                                 " union all " +
                                 " select NULL as ExamCheckAllotmentLnId, NULL as ExamCheckAllotmentId, " +
                                 " convert(uniqueidentifier, NULL) as UserId, QueId, convert(datetime, getdate()) as InsertedOn, convert(datetime, getdate()) as LastUpdatedOn, " +
                                 " convert(uniqueidentifier, NULL) as InsertedByUserId, convert(uniqueidentifier, NULL) as LastUpdatedByUserId, q.Que,  " +
                                 " replace(q.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                 " ,q.RightMarks,q.WrongMarks, " +
                                 " q.NonMarks,q.QueType, q.SrNo  from ExamSchedules e  " +
                                 " inner join Ques q on q.TestId = e.TestId WHERE  " +
                                 " e.ExamScheduleId = '" + examCheckAllotmentDTO.ExamScheduleId.ToString() + "' " +
                                 " and q.QueId not in (select QueId from ExamCheckAllotmentLns where ExamCheckAllotmentId = '" + ExamCheckAllotmentId + "') " +
                                 " order by q.SrNo asc  ";

            DataTable dt = new DataTable();
            dt.Load(sqlCmd.ExecuteReader());

            examCheckAllotmentDTO.dtExamCheckAllotmentLns = dt;

            _generalDAL.CloseSQLConnection();

            return examCheckAllotmentDTO;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }

    public string Insert(ExamCheckAllotmentDTO examCheckAllotmentDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();
        string ExamCheckAllotmentId = "";

        _generalDAL.OpenSQLConnection();

        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " DECLARE  @ExamCheckAllotmentId uniqueidentifier;" +
                                 " SET @ExamCheckAllotmentId = NewId()" +
                                 " INSERT INTO ExamCheckAllotments(ExamCheckAllotmentId, StandardTextListId, SubId, TestId, ExamScheduleId, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " VALUES(@ExamCheckAllotmentId,'" + examCheckAllotmentDTO.StandardTextListId.ToString() + "'" +
                                 ",'" + examCheckAllotmentDTO.SubId.ToString() + "'" +
                                 ",'" + examCheckAllotmentDTO.TestId.ToString() + "'" +
                                 ",'" + examCheckAllotmentDTO.ExamScheduleId.ToString() + "'" +
                                 ",GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL " +
                                 " );" +
                                 "SELECT @ExamCheckAllotmentId";

            ExamCheckAllotmentId = sqlCmd.ExecuteScalar().ToString();

            if (ExamCheckAllotmentId != null)
            {
                if (examCheckAllotmentDTO.dtExamCheckAllotmentLns != null)
                {
                    if (examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows.Count > 0)
                    {
                        for (int i = 0; i <= examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows.Count - 1; i++)
                        {
                            if (examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows[i]["UserId"] != DBNull.Value)
                            {
                                sqlCmd.CommandText = " INSERT INTO ExamCheckAllotmentLns(ExamCheckAllotmentLnId, ExamCheckAllotmentId, UserId, QueId, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                                    " VALUES(NEWID()" +
                                                    " , '" + ExamCheckAllotmentId.ToString() + "' " +
                                                    " , " + ((examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows[i]["UserId"] == DBNull.Value) ? "NULL" : "'" + examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows[i]["UserId"].ToString().Replace("'", "''") + "'") +
                                                    " , " + ((examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows[i]["QueId"] == DBNull.Value) ? "NULL" : "'" + examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows[i]["QueId"].ToString().Replace("'", "''") + "'") +
                                                    " ,GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                                    "); ";
                                sqlCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }

            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
        return ExamCheckAllotmentId;
    }

    public void Update(ExamCheckAllotmentDTO examCheckAllotmentDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;

            if (examCheckAllotmentDTO.ExamCheckAllotmentId != null)
            {
                if (examCheckAllotmentDTO.dtExamCheckAllotmentLns != null)
                {
                    if (examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows.Count > 0)
                    {
                        for (int i = 0; i <= examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows.Count - 1; i++)
                        {
                            if (examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows[i]["UserId"] != DBNull.Value)
                            {
                                sqlCmd.CommandText = " select count(*) from ExamCheckAllotmentLns where ExamCheckAllotmentId = '" + examCheckAllotmentDTO.ExamCheckAllotmentId + "' " +
                                                     " and QueId = '" + examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows[i]["QueId"].ToString() + "' ";

                                int cnt = Convert.ToInt16(sqlCmd.ExecuteScalar());

                                if (cnt == 0)
                                {
                                    sqlCmd.CommandText = " INSERT INTO ExamCheckAllotmentLns(ExamCheckAllotmentLnId, ExamCheckAllotmentId, UserId, QueId, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                                        " VALUES(NEWID()" +
                                                        " , '" + examCheckAllotmentDTO.ExamCheckAllotmentId.ToString() + "' " +
                                                        " , " + ((examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows[i]["UserId"] == DBNull.Value) ? "NULL" : "'" + examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows[i]["UserId"].ToString().Replace("'", "''") + "'") +
                                                        " , " + ((examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows[i]["QueId"] == DBNull.Value) ? "NULL" : "'" + examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows[i]["QueId"].ToString().Replace("'", "''") + "'") +
                                                        " ,GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                                        "); ";
                                    sqlCmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    sqlCmd.CommandText = " UPDATE ExamCheckAllotmentLns SET" +
                                                         " UserId = '" + examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows[i]["UserId"].ToString() + "'" +
                                                         " ,LastUpdatedOn=GETDATE()" +
                                                         " ,LastUpdatedByUserId='" + MySession.UserUnique + "'" +
                                                         " WHERE ExamCheckAllotmentLnId='" + examCheckAllotmentDTO.dtExamCheckAllotmentLns.Rows[i]["ExamCheckAllotmentLnId"].ToString() + "'";
                                    sqlCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }

            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public void Delete(string ExamCheckAllotmentId)
    {
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;


            string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..ExamCheckAllotments(ExamCheckAllotmentId, StandardTextListId, SubId, TestId, ExamScheduleId, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " Select ExamCheckAllotmentId, StandardTextListId, SubId, TestId, ExamScheduleId, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique.ToString() + "', LastUpdatedByUserId " +
                                 " FROM ExamCheckAllotments WHERE ExamCheckAllotmentId='" + ExamCheckAllotmentId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " DELETE FROM ExamCheckAllotments WHERE ExamCheckAllotmentId='" + ExamCheckAllotmentId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..ExamCheckAllotmentLns(ExamCheckAllotmentLnId, ExamCheckAllotmentId, UserId, QueId, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " Select ExamCheckAllotmentLnId, ExamCheckAllotmentId, UserId, QueId, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique.ToString() + "', LastUpdatedByUserId " +
                                 " FROM ExamCheckAllotmentLns WHERE ExamCheckAllotmentId='" + ExamCheckAllotmentId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " DELETE FROM ExamCheckAllotmentLns WHERE ExamCheckAllotmentId='" + ExamCheckAllotmentId + "'";
            sqlCmd.ExecuteNonQuery();

            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public DataTable LoadQuestion(string ExamScheduleId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = " select NULL as ExamCheckAllotmentLnId, NULL as ExamCheckAllotmentId, " +
                                 " convert(uniqueidentifier, NULL) as UserId, QueId, convert(datetime, getdate()) as InsertedOn, convert(datetime, getdate()) as LastUpdatedOn, " +
                                 " convert(datetime, getdate()) as InsertedByUserId, convert(datetime, getdate()) as LastUpdatedByUserId, q.Que,  " +
                                 " replace(q.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                 " ,q.RightMarks,q.WrongMarks, " +
                                 " q.NonMarks,q.QueType  from ExamSchedules e   inner join Ques q on q.TestId = e.TestId WHERE  " +
                                 "  e.ExamScheduleId = '" + ExamScheduleId.ToString() + "' order by q.SrNo asc  ";

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

    public DataTable LoadSubject(string StandardId)
    {
        string sql;
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

    public DataTable LoadEmployee()
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            sqlCmd.CommandText = " select UserId, FirstName + ' ' + LastName, UserName from Users where isnull(IsDisabled, 0) = 0 order by FirstName + ' ' + LastName ";

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

    public bool DataExists(string standardId, string subId, string testId, string examScheduleId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = " SELECT COUNT(*) FROM ExamCheckAllotments WHERE StandardTextListId = '" + standardId + "' " +
                                 " and SubId = '" + subId + "' and TestId = '" + testId + "' and ExamScheduleId = '" + examScheduleId + "' ";

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
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    public bool DataExists(string standardId, string subId, string testId, string examScheduleId, string examCheckAllotmentId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;

        sqlCmd.CommandText = " SELECT COUNT(*) FROM ExamCheckAllotments WHERE StandardTextListId = '" + standardId + "' " +
                             " and SubId = '" + subId + "' and TestId = '" + testId + "' and ExamScheduleId = '" + examScheduleId + "' " +
                             " AND NOT ExamCheckAllotmentId = '" + examCheckAllotmentId + "' ";

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

    public string IsReferenced(string ExamCheckAllotmentId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        string strRef = "";

        _generalDAL.OpenSQLConnection();

        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        try
        {
            if (ExamCheckAllotmentId != null)
            {
                strRef += _generalDAL.IsReferenced("ExamCheckAllotments", "ExamCheckAllotmentId", ExamCheckAllotmentId, sqlCmd, "'ExamCheckAllotmentLns'");
            }
            _generalDAL.CloseSQLConnection();
            return strRef;
        }
        //Add Catch
        catch
        {
            _generalDAL.CloseSQLConnection();
            return "";
            throw new Exception();
        }
        //

    }
}
