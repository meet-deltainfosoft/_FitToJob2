using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;

/// <summary>
/// Summary description for Registration1DAL
/// </summary>
public class Registration1DAL
{
	private GeneralDAL _generalDAL;

    #region Constructor Destructor
    public Registration1DAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~Registration1DAL()
    {
        _generalDAL = null;
    }
    #endregion

    #region Insert
    public string Insert(Registration1DTO _registrationDTO)
    {
        string RegistrationId = "";
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " INSERT INTO Registrations(RegistrationId,AadharCardNo,FirstName,MiddleName,LastName,MobileNo,City,Taluka,District,State," +
                                 " InsertedOn,LastUpdatedOn,Address)" +
                                 //" InsertedByUserId,LastUpdatedByUserId,Address)" +
                                 " VALUES('" + _registrationDTO.RegistrationId + "', " +
                                 ((_registrationDTO.AadharCardNo == null) ? "NULL" : "'" + _registrationDTO.AadharCardNo.Replace("'", "''") + "'") + "," +
                                 ((_registrationDTO.FirstName == null) ? "NULL" : "'" + _registrationDTO.FirstName.Replace("'", "''") + "'") + "," +
                                 ((_registrationDTO.MiddleName == null) ? "NULL" : "'" + _registrationDTO.MiddleName.Replace("'", "''") + "'") + "," +
                                 ((_registrationDTO.LastName == null) ? "NULL" : "'" + _registrationDTO.LastName.Replace("'", "''") + "'") + "," +
                                 ((_registrationDTO.MobileNo == null) ? "NULL" : "'" + _registrationDTO.MobileNo.Replace("'", "''") + "'") + "," +
                                 ((_registrationDTO.City == null) ? "NULL" : "'" + _registrationDTO.City.Replace("'", "''") + "'") + "," +
                                 ((_registrationDTO.Taluka == null) ? "NULL" : "'" + _registrationDTO.Taluka.Replace("'", "''") + "'") + "," +
                                 ((_registrationDTO.District == null) ? "NULL" : "'" + _registrationDTO.District.Replace("'", "''") + "'") + "," +
                                 ((_registrationDTO.State == null) ? "NULL" : "'" + _registrationDTO.State.Replace("'", "''") + "'") + "," +
                                 " GETDATE(),GETDATE()," +
                                 //" '" + MySession.UserUnique + "','" + MySession.UserUnique + "'," +
                                 ((_registrationDTO.Address == null) ? "NULL" : "'" + _registrationDTO.Address + "'") +
                                 " );" +
                                 " Select '" + _registrationDTO.RegistrationId + "'";

            RegistrationId = sqlCmd.ExecuteScalar().ToString();

            InsertUPLOADDOCUMENTS(_registrationDTO, RegistrationId, sqlCmd);

            _generalDAL.CloseSQLConnection();
            return RegistrationId;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
    #endregion

    private void InsertUPLOADDOCUMENTS(Registration1DTO _registrationDTO, string RegistrationId, SqlCommand sqlCmd)
    {
        try
        {
            //int TrNo;
            if (_registrationDTO.PhotoPath != null && _registrationDTO.PhotoPath != null && _registrationDTO.SelfIntroVideoPath != null && _registrationDTO.SelfIntroVideoPath != null && _registrationDTO.Resume != null && _registrationDTO.Resume != null)
            {
                //sqlCmd.CommandText = "select Isnull(Max(TrNo),0) from Fu";
                //TrNo = Convert.ToInt32(sqlCmd.ExecuteScalar());

                sqlCmd.CommandText = " DECLARE @RegistrationVerificationId as uniqueidentifier" +
                                       " SET @RegistrationVerificationId = NEWID() " +
                                       " INSERT INTO RegistrationVerifications (RegistrationVerificationId,RegistrationId,PhotoPath,SelfIntroVideoPath,UplaodResume,InsertedOn, " +
                                       " LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId) " +
                     //" VALUES (NewId(),'" + ApplicationId + "','" + _applicationDTO.FormName + "','" + _applicationDTO.BirthCirtificate + "'," + 1 + ",'" + _applicationDTO.BirthCirtificateName + "','" + _applicationDTO.FileType + "'," +
                                       " VALUES (NewId(),'" + RegistrationId + "','" + _registrationDTO.PhotoPath + "','" + _registrationDTO.SelfIntroVideoPath + "','" + _registrationDTO.Resume + "'," +
                                       " GETDATE(),GETDATE()," +
                                       ((MySession.UserUnique == null) ? "NULL" : "'" + MySession.UserUnique.Replace("'", "''") + "'") + "," +
                                       ((MySession.UserUnique == null) ? "NULL" : "'" + MySession.UserUnique.Replace("'", "''") + "'") + " " +
                                       
                                       " )";
                sqlCmd.ExecuteNonQuery();
            }
        }
        catch
        {

        }
    }

    #region Update
    public string Update(Registration1DTO _registrationDTO)
    {
        try
        {
            string RegistrationId = "";
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " UPDATE Registrations SET " +
                                 "  AadharCardNo=" + ((_registrationDTO.AadharCardNo == null) ? "NULL" : "'" + _registrationDTO.AadharCardNo.Replace("'", "''") + "'") +
                                 " ,FirstName=" + ((_registrationDTO.FirstName == null) ? "NULL" : "'" + _registrationDTO.FirstName.Replace("'", "''") + "'") +
                                 " ,MiddleName=" + ((_registrationDTO.MiddleName == null) ? "NULL" : "'" + _registrationDTO.MiddleName.Replace("'", "''") + "'") +
                                 " ,LastName=" + ((_registrationDTO.LastName == null) ? "NULL" : "'" + _registrationDTO.LastName.Replace("'", "''") + "'") +
                                 " ,MobileNo=" + ((_registrationDTO.MobileNo == null) ? "NULL" : "'" + _registrationDTO.MobileNo.Replace("'", "''") + "'") +
                                 " ,City=" + ((_registrationDTO.City == null) ? "NULL" : "'" + _registrationDTO.City.Replace("'", "''") + "'") +
                                 " ,Taluka=" + ((_registrationDTO.Taluka == null) ? "NULL" : "'" + _registrationDTO.Taluka.Replace("'", "''") + "'") +
                                 " ,District=" + ((_registrationDTO.District == null) ? "NULL" : "'" + _registrationDTO.District.Replace("'", "''") + "'") +
                                 " ,State=" + ((_registrationDTO.State == null) ? "NULL" : "'" + _registrationDTO.State.Replace("'", "''") + "'") +
                                 " ,LastUpdatedOn=GETDATE()" +
                                 //" ,LastUpdatedByUserId='" + MySession.UserUnique + "'" +
                                 " ,Address =" + ((_registrationDTO.Address == null) ? "NULL" : "'" + _registrationDTO.Address.Replace("'", "''") + "'") +
                                 " WHERE RegistrationId='" + _registrationDTO.RegistrationId + "'";

            sqlCmd.ExecuteNonQuery();
            RegistrationId = _registrationDTO.RegistrationId;

            UpdateUPLOADDOCUMENTS(_registrationDTO, RegistrationId, sqlCmd);

            _generalDAL.CloseSQLConnection();
            return RegistrationId;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
    #endregion

    private void UpdateUPLOADDOCUMENTS(Registration1DTO _registrationDTO, string RegistrationId, SqlCommand sqlCmd)
    {
        try
        {
            if (_registrationDTO.PhotoPath != null && _registrationDTO.PhotoPath != null && _registrationDTO.SelfIntroVideoPath != null && _registrationDTO.SelfIntroVideoPath != null && _registrationDTO.Resume != null && _registrationDTO.Resume != null)
            {
                sqlCmd.CommandText = " update RegistrationVerifications set " +
                                " PhotoPath = " + ((_registrationDTO.PhotoPath == null) ? "NULL" : "'" + _registrationDTO.PhotoPath.Replace("'", "''") + "'") +
                                ", SelfIntroVideoPath = " + ((_registrationDTO.SelfIntroVideoPath == null) ? "NULL" : "'" + _registrationDTO.SelfIntroVideoPath.Replace("'", "''") + "'") +
                                ", UplaodResume = " + ((_registrationDTO.Resume == null) ? "NULL" : "'" + _registrationDTO.Resume.Replace("'", "''") + "'") +
                                ", LastUpdatedOn = getdate() " +
                                ", LastUpdatedByUserId = " + ((MySession.UserUnique == null) ? "NULL" : "'" + MySession.UserUnique + "'") +
                                " where RegistrationId = '" + _registrationDTO.RegistrationId + "' ";

                sqlCmd.ExecuteNonQuery();
            }
        }
        catch
        {

        }
    }

    #region Select
    public Registration1DTO Select(string RegistrationId)
    {
        Registration1DTO _registrationDTO = new Registration1DTO();
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader sqlDr;
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM Registrations where RegistrationId = '"+ RegistrationId + "' " ;
               
            sqlDr = sqlCmd.ExecuteReader();

            while (sqlDr.Read())
            {
                _registrationDTO.RegistrationId = sqlDr["RegistrationId"].ToString();


                if (sqlDr["AadharCardNo"] != DBNull.Value)
                    _registrationDTO.AadharCardNo = sqlDr["AadharCardNo"].ToString();
                else
                    _registrationDTO.AadharCardNo = null;

                if (sqlDr["FirstName"] != DBNull.Value)
                    _registrationDTO.FirstName = sqlDr["FirstName"].ToString();
                else
                    _registrationDTO.FirstName = null;

                if (sqlDr["MiddleName"] != DBNull.Value)
                    _registrationDTO.MiddleName = sqlDr["MiddleName"].ToString();
                else
                    _registrationDTO.MiddleName = null;

                if (sqlDr["LastName"] != DBNull.Value)
                    _registrationDTO.LastName = sqlDr["LastName"].ToString();
                else
                    _registrationDTO.LastName = null;

                if (sqlDr["MobileNo"] != DBNull.Value)
                    _registrationDTO.MobileNo = sqlDr["MobileNo"].ToString();
                else
                    _registrationDTO.MobileNo = null;

                if (sqlDr["City"] != DBNull.Value)
                    _registrationDTO.City = sqlDr["City"].ToString();
                else
                    _registrationDTO.City = null;

                if (sqlDr["Taluka"] != DBNull.Value)
                    _registrationDTO.Taluka = sqlDr["Taluka"].ToString();
                else
                    _registrationDTO.Taluka = null;

                if (sqlDr["District"] != DBNull.Value)
                    _registrationDTO.District = sqlDr["District"].ToString();
                else
                    _registrationDTO.District = null;

                if (sqlDr["State"] != DBNull.Value)
                    _registrationDTO.State = sqlDr["State"].ToString();
                else
                    _registrationDTO.State = null;

                if (sqlDr["Address"] != DBNull.Value)
                    _registrationDTO.Address = sqlDr["Address"].ToString();
                else
                    _registrationDTO.Address = null;
            }

            sqlDr.Close();



            sqlCmd.CommandText = " select  replace(a.PhotoPath, '" + ConfigurationSettings.AppSettings["PhotoPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'PhotoPath' " +
                " , replace(a.SelfIntroVideoPath, '" + ConfigurationSettings.AppSettings["SelfIntroVideoPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SelfIntroVideoPath' " +
                " from RegistrationVerifications a where RegistrationId = '" + RegistrationId + "' ";

            sqlDr = sqlCmd.ExecuteReader();

            while (sqlDr.Read())
            {

                if (sqlDr["PhotoPath"] != DBNull.Value)
                    _registrationDTO.PhotoPath = sqlDr["PhotoPath"].ToString();
                else
                    _registrationDTO.PhotoPath = null;

                if (sqlDr["SelfIntroVideoPath"] != DBNull.Value)
                    _registrationDTO.SelfIntroVideoPath = sqlDr["SelfIntroVideoPath"].ToString();
                else
                    _registrationDTO.SelfIntroVideoPath = null;
            }

            sqlDr.Close();

            

            _generalDAL.CloseSQLConnection();
            return _registrationDTO;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region Delete
    public void Delete(string RegistrationId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;

            string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');
            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..Registrations(RegistrationId,AadharCardNo,FirstName,MiddleName,LastName,MobileNo,City,Taluka,District,State,Address," +
                                 " OTP,OTPGeneratedOn,FCMId,LastUpdatedOn, LastUpdatedByUserId, InsertedOn, InsertedByUserId)" +
                                 " Select RegistrationId,AadharCardNo,FirstName,MiddleName,LastName,MobileNo,City,Taluka,District,State,Address," +
                                 " OTP,OTPGeneratedOn,FCMId,InsertedOn,LastUpdatedOn, LastUpdatedByUserId" +
                                 " , getdate(),'" + MySession.UserUnique + "'" +
                                 " FROM Registrations WHERE RegistrationId='" + RegistrationId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " DELETE FROM Registrations WHERE RegistrationId='" + RegistrationId + "'";
            sqlCmd.ExecuteNonQuery();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
    #endregion

    public bool NameExist(string MobileNo)
    {
        //Escape Single Quote
        MobileNo = MobileNo.Trim().Replace("'", "''");
        //Escape single Quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM Registrations WHERE MobileNo='" + MobileNo + "'";

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

    public bool NameExist(string MobileNo, string RegistrationId)
    {
        //Escape Single Quote
        MobileNo = MobileNo.Trim().Replace("'", "''");
        //Escape Single Quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        if (RegistrationId != null)
        {
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Registrations WHERE MobileNo='" + MobileNo + "' AND NOT RegistrationId='" + RegistrationId + "'";
        }
        else
        {
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Registrations WHERE MobileNo='" + MobileNo + "'";
        }

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

    public bool NameExists(string AadharCardNo)
    {
        //Escape Single Quote
        AadharCardNo = AadharCardNo.Trim().Replace("'", "''");
        //Escape single Quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM Registrations WHERE AadharCardNo='" + AadharCardNo + "'";

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

    public bool NameExists(string AadharCardNo, string RegistrationId)
    {
        //Escape Single Quote
        AadharCardNo = AadharCardNo.Trim().Replace("'", "''");
        //Escape Single Quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        if (RegistrationId != null)
        {
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Registrations WHERE AadharCardNo='" + AadharCardNo + "' AND NOT RegistrationId='" + RegistrationId + "'";
        }
        else
        {
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Registrations WHERE AadharCardNo='" + AadharCardNo + "'";
        }

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

    public string IsReferenced(string RegistrationId)
    {
        string strRef = "";
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            if (RegistrationId != null)
            {
                strRef += _generalDAL.IsReferenced("Registartion", "RegistrationId", RegistrationId, sqlCmd, "'Registartion'");
            }
            _generalDAL.CloseSQLConnection();
            return strRef;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            return "";
            throw new Exception(ex.Message);
        }
    }
}