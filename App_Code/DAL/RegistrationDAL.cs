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

public class RegistrationDAL
{
    public GeneralDAL _generalDAL;

    public RegistrationDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~RegistrationDAL()
    {
        _generalDAL = null;
    }

    public void Insert(RegistrationDTO RegistrationDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string RegistrationId;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = "DECLARE  @RegistrationId uniqueidentifier;" +
                                 " SET @RegistrationId = NewId()" +
                                 "INSERT INTO Registration(RegistrationId,FirstName,MobileNo,ExtraMobileNo,StandardId,IsDeActive,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId, ExamNo,SchoolName,City,EmailId)" +
                                 " VALUES(@RegistrationId," + ((RegistrationDTO.RegistrationName == null) ? "NULL" : "'" + RegistrationDTO.RegistrationName.Replace("'", "''") + "'") + "," +
                                 "" + ((RegistrationDTO.MobileNo == null) ? "NULL" : "'" + RegistrationDTO.MobileNo.Replace("'", "''") + "'") + "," +
                                 "" + ((RegistrationDTO.ExtraMobileNo == null) ? "NULL" : "'" + RegistrationDTO.ExtraMobileNo.Replace("'", "''") + "'") + "," +
                                 " '" + RegistrationDTO.StandardId + "'," +
                                 " '" + Convert.ToByte(RegistrationDTO.IsDeActive) + "',GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                 " , " + ((RegistrationDTO.ExamNo == null) ? "NULL" : "'" + RegistrationDTO.ExamNo + "'") + " " +
                                 " , " + ((RegistrationDTO.SchoolName == null) ? "NULL" : "'" + RegistrationDTO.SchoolName.Replace("'", "''") + "'") + " " +
                                 " , " + ((RegistrationDTO.City == null) ? "NULL" : "'" + RegistrationDTO.City.Replace("'", "''") + "'") + " " +
                                 " , " + ((RegistrationDTO.EmailId == null) ? "NULL" : "'" + RegistrationDTO.EmailId.Replace("'", "''") + "'") + " " +
                                 ");" +
                                 " SELECT @RegistrationId";

            RegistrationId = sqlCmd.ExecuteScalar().ToString();

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

    public RegistrationDTO Select(string RegistrationId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        RegistrationDTO RegistrationDTO = new RegistrationDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "select a.RegistrationId,a.FirstName,a.MobileNo,a.ExtraMobileNo,a.StandardId,a.IsDeActive, a.ExamNo,a.SchoolName,a.City,a.EmailId,a.DivisionTextListId from Registration a WHERE RegistrationId='" + RegistrationId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["RegistrationId"] != DBNull.Value)
                RegistrationDTO.RegistrationId = sqlDr["RegistrationId"].ToString();
            else
                RegistrationDTO.RegistrationId = null;

            if (sqlDr["FirstName"] != DBNull.Value)
                RegistrationDTO.RegistrationName = sqlDr["FirstName"].ToString();
            else
                RegistrationDTO.RegistrationName = null;

            if (sqlDr["MobileNo"] != DBNull.Value)
                RegistrationDTO.MobileNo = sqlDr["MobileNo"].ToString();
            else
                RegistrationDTO.MobileNo = null;

            if (sqlDr["ExtraMobileNo"] != DBNull.Value)
                RegistrationDTO.ExtraMobileNo = sqlDr["ExtraMobileNo"].ToString();
            else
                RegistrationDTO.ExtraMobileNo = null;

            if (sqlDr["StandardId"] != DBNull.Value)
                RegistrationDTO.StandardId = sqlDr["StandardId"].ToString();
            else
                RegistrationDTO.StandardId = null;

            if (sqlDr["DivisionTextListId"] != DBNull.Value)
                RegistrationDTO.DivisionTextListId = sqlDr["DivisionTextListId"].ToString();
            else
                RegistrationDTO.DivisionTextListId = null;

            if (sqlDr["IsDeActive"] != DBNull.Value)
                RegistrationDTO.IsDeActive = Convert.ToBoolean(sqlDr["IsDeActive"].ToString());
            else
                RegistrationDTO.IsDeActive = null;

            if (sqlDr["ExamNo"] != DBNull.Value)
                RegistrationDTO.ExamNo = (sqlDr["ExamNo"].ToString());
            else
                RegistrationDTO.ExamNo = null;

            if (sqlDr["SchoolName"] != DBNull.Value)
                RegistrationDTO.SchoolName = sqlDr["SchoolName"].ToString();
            else
                RegistrationDTO.SchoolName = null;

            if (sqlDr["City"] != DBNull.Value)
                RegistrationDTO.City = sqlDr["City"].ToString();
            else
                RegistrationDTO.City = null;

            if (sqlDr["EmailId"] != DBNull.Value)
                RegistrationDTO.EmailId = sqlDr["EmailId"].ToString();
            else
                RegistrationDTO.EmailId = null;
        }

        sqlDr.Close();

        _generalDAL.CloseSQLConnection();

        return RegistrationDTO;
    }

    public void Update(RegistrationDTO RegistrationDTO)
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
            sqlCmd.CommandText = " UPDATE Registration SET" +
                                 " FirstName = " + ((RegistrationDTO.RegistrationName == null) ? "NULL" : "'" + RegistrationDTO.RegistrationName.Replace("'", "''") + "'") + "" +
                                 " ,MobileNo = " + ((RegistrationDTO.MobileNo == null) ? "NULL" : "'" + RegistrationDTO.MobileNo.Replace("'", "''") + "'") + "" +
                                 " ,ExtraMobileNo = " + ((RegistrationDTO.ExtraMobileNo == null) ? "NULL" : "'" + RegistrationDTO.ExtraMobileNo.Replace("'", "''") + "'") + "" +
                                 " ,StandardId = " + ((RegistrationDTO.StandardId == null) ? "NULL" : "'" + RegistrationDTO.StandardId + "'") + "" +
                                 " ,IsDeActive = " + ((RegistrationDTO.IsDeActive == null) ? "NULL" : "'" + Convert.ToByte(RegistrationDTO.IsDeActive) + "'") + "" +
                                 " ,LastUpdatedOn = GETDATE(),LastUpdatedByUserId = '" + MySession.UserUnique + "'" +
                                 " ,ExamNo = " + ((RegistrationDTO.ExamNo == null) ? "NULL" : "'" + RegistrationDTO.ExamNo + "'") + "" +
                                 " ,SchoolName = " + ((RegistrationDTO.SchoolName == null) ? "NULL" : "'" + RegistrationDTO.SchoolName.Replace("'", "''") + "'") + "" +
                                 " ,City = " + ((RegistrationDTO.City == null) ? "NULL" : "'" + RegistrationDTO.City.Replace("'", "''") + "'") + "" +
                                 " ,EmailId = " + ((RegistrationDTO.EmailId == null) ? "NULL" : "'" + RegistrationDTO.EmailId.Replace("'", "''") + "'") + "" +
                                 " WHERE RegistrationId = '" + RegistrationDTO.RegistrationId + "'";
            sqlCmd.ExecuteNonQuery();

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

    public void Delete(string RegistrationId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..Registration(RegistrationId,FirstName,MobileNo,ExtraMobileNo,StandardId,Schoolname,City,EmailId,IsDeActive,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId, ExamNo)" +
            " Select RegistrationId,FirstName,MobileNo,ExtraMobileNo,StandardId,Schoolname,City,EmailId,IsDeActive,getdate(),LastUpdatedOn,'" + MySession.UserUnique + "',LastUpdatedByUserId, ExamNo" +
            " FROM Registration WHERE RegistrationId='" + RegistrationId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM Registration WHERE RegistrationId='" + RegistrationId + "'";
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

    public bool NamesExists(string StandardId, string StudentName, string MobileNo)
    {
        //Escape single quote
        StudentName = StudentName.Replace("'", "''");
        MobileNo = MobileNo.Replace("'", "''");
        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Registration WHERE [FirstName] = '" + StudentName + "' and StandardId = '" + StandardId + "' and MobileNo = '" + MobileNo + "'";

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
    public bool NamesExists(string StandardId, string StudentName, string MobileNo, string RegistrationId)
    {
        try
        {
            //Escape single quote
            StudentName = StudentName.Replace("'", "''");
            MobileNo = MobileNo.Replace("'", "''");
            //Escape single quote

            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Registration WHERE [FirstName] = '" + StudentName + "' and StandardId = '" + StandardId + "' and MobileNo = '" + MobileNo + "' AND NOT RegistrationId='" + RegistrationId + "'";

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

    public void DeleteRegistration(ArrayList al)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlcmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlcmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlcmd.CommandType = CommandType.Text;
        sqlcmd.Transaction = sqlTrans;

        string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');

        try
        {
            foreach (string id in al)
            {
                sqlcmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..Registration(RegistrationId,FirstName,MobileNo,ExtraMobileNo,StandardId,Schoolname,City,EmailId,IsDeActive,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId, ExamNo)" +
            " Select RegistrationId,FirstName,MobileNo,ExtraMobileNo,StandardId,Schoolname,City,EmailId,IsDeActive,getdate(),LastUpdatedOn,'" + MySession.UserUnique + "',LastUpdatedByUserId, ExamNo" +
            " FROM Registration WHERE RegistrationId='" + id + "'";
                sqlcmd.ExecuteNonQuery();

                sqlcmd.CommandText = "DELETE FROM Registration WHERE RegistrationId='" + id + "'";
                sqlcmd.ExecuteNonQuery();
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
}
