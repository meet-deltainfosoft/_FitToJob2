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
/// Summary description for EBookPDFDAL
/// </summary>
public class EBookPDFDAL
{
    public GeneralDAL _generalDAL;

    public EBookPDFDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~EBookPDFDAL()
    {
        _generalDAL = null;
    }
    public void Insert(EBookPDFDTO EBookPDFDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string EBookPDFId;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = " INSERT INTO EBookPDFs(EBookPDFId,FileName,FileLink,Remarks,StandardTextListId, SubId," +
                                 " InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " VALUES(newid() " +
                //"" + ((EBookPDFDTO.EBookPDFId == null) ? "NULL" : "'" + EBookPDFDTO.EBookPDFId.Replace("'", "''") + "'") +
                                 "," + ((EBookPDFDTO.FileName == null) ? "NULL" : "N'" + EBookPDFDTO.FileName.Replace("'", "''") + "'") +
                                 "," + ((EBookPDFDTO.UploadphotoPath == null) ? "NULL" : "N'" + EBookPDFDTO.UploadphotoPath.Replace("'", "''") + "'") +
                                 "," + ((EBookPDFDTO.Remarks == null) ? "NULL" : "N'" + EBookPDFDTO.Remarks.ToString().Replace("'", "''") + "'") +
                                 "," + ((EBookPDFDTO.StandardTextListId == null) ? "NULL" : "'" + EBookPDFDTO.StandardTextListId.Replace("'", "''") + "'") +
                                 "," + ((EBookPDFDTO.SubId == null) ? "NULL" : "'" + EBookPDFDTO.SubId.Replace("'", "''") + "'") +
                                  ",GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                 ");";


            sqlCmd.ExecuteNonQuery().ToString();

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
    public EBookPDFDTO Select(string EBookPDFId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        EBookPDFDTO EBookPDFDTO = new EBookPDFDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = " select *, replace(a.FileLink, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'NewPhoto' " +
                             " from EBookPDFs a  " +
                             " WHERE EBookPDFId='" + EBookPDFId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["EBookPDFId"] != DBNull.Value)
                EBookPDFDTO.EBookPDFId = sqlDr["EBookPDFId"].ToString();
            else
                EBookPDFDTO.EBookPDFId = null;

            if (sqlDr["SubId"] != DBNull.Value)
                EBookPDFDTO.SubId = sqlDr["SubId"].ToString();
            else
                EBookPDFDTO.SubId = null;

            if (sqlDr["StandardTextListId"] != DBNull.Value)
                EBookPDFDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();
            else
                EBookPDFDTO.StandardTextListId = null;

            if (sqlDr["NewPhoto"] != DBNull.Value)
                EBookPDFDTO.UploadphotoPath = sqlDr["NewPhoto"].ToString();
            else
                EBookPDFDTO.UploadphotoPath = null;

            if (sqlDr["FileName"] != DBNull.Value)
                EBookPDFDTO.FileName = sqlDr["FileName"].ToString();
            else
                EBookPDFDTO.FileName = null;

            if (sqlDr["Remarks"] != DBNull.Value)
                EBookPDFDTO.Remarks = sqlDr["Remarks"].ToString();
            else
                EBookPDFDTO.Remarks = null;
        }

        sqlDr.Close();
        _generalDAL.CloseSQLConnection();
        return EBookPDFDTO;
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

    public void Update(EBookPDFDTO EBookPDFDTO)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = " UPDATE EBookPDFs SET " +
                                 " SubId = " + ((EBookPDFDTO.SubId == null) ? "NULL" : "'" + EBookPDFDTO.SubId + "'") + "" +
                                 " , Remarks=" + ((EBookPDFDTO.Remarks == null) ? "NULL" : "N'" + EBookPDFDTO.Remarks + "'") + "" +
                                 " , StandardTextListId=" + ((EBookPDFDTO.StandardTextListId == null) ? "NULL" : "'" + EBookPDFDTO.StandardTextListId + "'") + "" +
                                 " , FileName = " + ((EBookPDFDTO.FileName == null) ? "NULL" : "N'" + EBookPDFDTO.FileName.Replace("'", "''") + "'") + "" +
                                 " , FileLink = " + ((EBookPDFDTO.UploadphotoPath == null) ? "NULL" : "N'" + EBookPDFDTO.UploadphotoPath.Replace("'", "''") + "'") + "" +
                                 " , LastUpdatedOn=GETDATE()" +
                                 " , LastUpdatedByUserId='" + MySession.UserUnique + "'" +
                                 " WHERE EBookPDFId='" + EBookPDFDTO.EBookPDFId + "'";
            sqlCmd.ExecuteNonQuery();

            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }

    public void Delete(string EBookPDFId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..EBookPDFs(EBookPDFId, StandardTextListId, SubId, FileName, FileLink, Remarks, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " Select EBookPDFId, StandardTextListId, SubId, FileName, FileLink, Remarks, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId " +
                                 " FROM EBookPDFs WHERE EBookPDFId='" + EBookPDFId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM EBookPDFs WHERE EBookPDFId='" + EBookPDFId + "'";
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
}