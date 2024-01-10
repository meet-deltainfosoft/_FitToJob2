using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ResumeUploadDAL
/// </summary>
public class ResumeUploadDAL
{

    private GeneralDAL _generalDAL;

    public ResumeUploadDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~ResumeUploadDAL()
    {
        _generalDAL = null;
    }



    public string GetRegistrationId(SqlCommand sqlCmd)
    {
        try
        {
            //SqlCommand sqlCmd = new SqlCommand();
            string RegistrationId = "";
            //_generalDAL.OpenSQLConnection();
            //sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            //sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT RegistrationId FROM Registrations where InsertedByUserId = '" + MySession.UserUnique + "' ";

            RegistrationId = sqlCmd.ExecuteScalar().ToString();
            return RegistrationId;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    public void UpdateResume(ResumeUploadDTO _ResumeUploadDTO)
    {
        try
        {
            if (_ResumeUploadDTO.ResumeName != null && _ResumeUploadDTO.ResumeName != null)
            {
                SqlCommand sqlCmd = new SqlCommand();
                _generalDAL.OpenSQLConnection();
                sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
                sqlCmd.CommandType = CommandType.Text;

                string RegistrationVerificationId = "";
                string RegistrationId = "";

                RegistrationId = GetRegistrationId(sqlCmd);
                
                sqlCmd.CommandText = " select  RegistrationVerificationId from RegistrationVerifications " +
                                            " Where RegistrationId = '" + RegistrationId + "'";

                RegistrationVerificationId = sqlCmd.ExecuteScalar().ToString();

                sqlCmd.CommandText = " update RegistrationVerifications set " +
                                " ResumeName = " + ((_ResumeUploadDTO.ResumeName == null) ? "NULL" : "'" + _ResumeUploadDTO.ResumeName.Replace("'", "''") + "'") +
                                ", UplaodResume = " + ((_ResumeUploadDTO.UplaodResume == null) ? "NULL" : "'" + _ResumeUploadDTO.UplaodResume.Replace("'", "''") + "'") +
                                ", LastUpdatedOn = getdate() " +
                                ", LastUpdatedByUserId = " + ((MySession.UserUnique == null) ? "NULL" : "'" + MySession.UserUnique + "'") +
                                " where RegistrationVerificationId = '" + RegistrationVerificationId + "' ";

                sqlCmd.ExecuteNonQuery();
            }
        }
        catch
        {

        }
    }

}