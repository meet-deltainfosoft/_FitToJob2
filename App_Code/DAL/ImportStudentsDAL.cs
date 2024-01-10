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

public class ImportStudentsDAL
{
    private GeneralDAL _generalDAL;

    public void Save(DataTable Itm, ImportStudentsDTO ImportStudentsDTO)
    {
        GeneralDAL _generalDAL = new GeneralDAL();
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;
        try
        {
            string ExamNo, StudentName, MobileNo;

            foreach (DataRow dr in Itm.Rows)
            {
                if (dr["ExamNo"] != DBNull.Value)
                {
                    ExamNo = dr["ExamNo"].ToString();
                }
                else
                {
                    ExamNo = null;
                }
                if (dr["StudentName"] != DBNull.Value)
                {
                    StudentName = dr["StudentName"].ToString();
                }
                else
                {
                    StudentName = null;
                }
                if (dr["MobileNo"] != DBNull.Value)
                {
                    MobileNo = dr["MobileNo"].ToString();
                }
                else
                {
                    MobileNo = null;
                }
                insert(sqlCmd, ImportStudentsDTO, StudentName, MobileNo, ExamNo);
            }
            sqlTrans.Commit();
        }
        catch
        {
            sqlTrans.Rollback();
            throw new Exception();
        }
        _generalDAL.CloseSQLConnection();
    }

    private void insert(SqlCommand sqlcmd, ImportStudentsDTO ImportStudentsDTO, string StudentName, string MobileNo, string ExamNo)
    {
        try
        {
            sqlcmd.CommandText = " DECLARE  @RegistrationId uniqueidentifier;" +
                         " SET @RegistrationId = NewId()" +
                         " INSERT INTO Registration (RegistrationId, FirstName, MobileNo, StandardId, InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId, ExamNo) " +
                         " VALUES (@RegistrationId " +
                         "," + ((StudentName == null) ? "NULL" : "'" + StudentName.Replace("'", "''") + "'") +
                         "," + ((MobileNo == null) ? "NULL" : "'" + MobileNo.Replace("'", "''") + "'") +
                         "," + ((ImportStudentsDTO.StandardId == null) ? "NULL" : "'" + ImportStudentsDTO.StandardId + "'") +
                         ", GETDATE(), GETDATE(), '" + MySession.UserUnique + "',NULL" +
                         "," + ((ExamNo == null) ? "NULL" : "'" + ExamNo + "'") +
                         ")";
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public bool NameExists(string StandardId, string StudentName, string MobileNo)
    {
        try
        {
            _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " select Count(*) from Registration " +
                                 " where StandardId='" + StandardId + "' and MobileNo='" + MobileNo.Trim().Replace("'", "''") + "' and FirstName = '" + StudentName.Trim().Replace("'", "''") + "'";

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
            throw new Exception();
        }
    }
    public DataTable LoadSubject()
    {
        string sql, Str;
        DataTable dt = new DataTable();
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataSet ds = new DataSet();

            sql = " Select SubId,Name From Subs order by Name Asc";

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

}