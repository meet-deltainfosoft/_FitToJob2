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

public class SubDAL
{
    public GeneralDAL _generalDAL;

    public SubDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~SubDAL()
    {
        _generalDAL = null;
    }

    public void Insert(SubDTO SubDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string SubId;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = "DECLARE  @SubId uniqueidentifier;" +
                                 " SET @SubId = NewId()" +
                                 "INSERT INTO Subs(SubId,Name,Remarks,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId, StandardTextListId,IsStudyMaterialAllowed,ImagePhoto)" +
                                 " VALUES(@SubId," + ((SubDTO.Name == null) ? "NULL" : "N'" + SubDTO.Name.Replace("'", "''") + "'") + "," +
                                 "" + ((SubDTO.Remarks == null) ? "NULL" : "N'" + SubDTO.Remarks.Replace("'", "''") + "'") + ",GETDATE(),GETDATE(),NULL,NULL" +
                                 " ,  " + ((SubDTO.StandardTextListId == null) ? "NULL" : "'" + SubDTO.StandardTextListId.ToString() + "'") + " " +
                                 " ,  " + ((SubDTO.IsStudyMaterialAllowed == null) ? "NULL" : "'" + Convert.ToBoolean(SubDTO.IsStudyMaterialAllowed) + "'") + " " +
                                 "," + ((SubDTO.ImagePhoto == null) ? "NULL" : "'" + SubDTO.ImagePhoto.ToString() + "'") +
                                 ");" +
                                 " SELECT @SubId";

            SubId = sqlCmd.ExecuteScalar().ToString();

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

    public SubDTO Select(string SubId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        SubDTO SubDTO = new SubDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "select a.SubId,a.Name,a.Remarks, a.StandardTextListId,a.IsStudyMaterialAllowed "+
                             " , replace(a.ImagePhoto, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImagePhoto' " +
                             " from Subs a WHERE SubId='" + SubId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["SubId"] != DBNull.Value)
                SubDTO.SubId = sqlDr["SubId"].ToString();

            if (sqlDr["Name"] != DBNull.Value)
                SubDTO.Name = sqlDr["Name"].ToString();

            if (sqlDr["Remarks"] != DBNull.Value)
                SubDTO.Remarks = sqlDr["Remarks"].ToString();

            if (sqlDr["StandardTextListId"] != DBNull.Value)
                SubDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();

            if (sqlDr["IsStudyMaterialAllowed"] != DBNull.Value)
                SubDTO.IsStudyMaterialAllowed = Convert.ToBoolean(sqlDr["IsStudyMaterialAllowed"]);

            if (sqlDr["ImagePhoto"] != DBNull.Value)
                SubDTO.ImagePhoto = sqlDr["ImagePhoto"].ToString();
            else
                SubDTO.ImagePhoto = null;
        }

        sqlDr.Close();

        _generalDAL.CloseSQLConnection();

        return SubDTO;
    }

    public void Update(SubDTO SubDTO)
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
            sqlCmd.CommandText = " UPDATE Subs SET" +
                                 " Name = " + ((SubDTO.Name == null) ? "NULL" : "N'" + SubDTO.Name.Replace("'", "''") + "'") + "" +
                                 " ,Remarks = " + ((SubDTO.Remarks == null) ? "NULL" : "N'" + SubDTO.Remarks.Replace("'", "''") + "'") + "" +
                                 " ,LastUpdatedOn = GETDATE()" +
                                 " ,StandardTextListId = " + ((SubDTO.StandardTextListId == null) ? "NULL" : "'" + SubDTO.StandardTextListId.Replace("'", "''") + "'") + "" +
                                 " ,IsStudyMaterialAllowed = " + ((SubDTO.IsStudyMaterialAllowed == null) ? "NULL" : "'" + Convert.ToBoolean(SubDTO.IsStudyMaterialAllowed) + "'") + "" +
                                 " ,ImagePhoto = " + ((SubDTO.ImagePhoto == null) ? "NULL" : "'" + SubDTO.ImagePhoto.ToString() + "'") +
                                 " WHERE SubId = '" + SubDTO.SubId + "'";
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

    public void Delete(string SubId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..Subs(SubId,Name,Remarks,InsertedOn,LastUpdatedOn,StandardTextListId,IsStudyMaterialAllowed,ImagePhoto)" +
            " Select SubId,Name,Remarks, getdate(), getdate(),StandardTextListId,IsStudyMaterialAllowed,ImagePhoto " +
            " FROM Subs WHERE SubId='" + SubId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM Subs WHERE SubId='" + SubId + "'";
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

    public bool NamesExist(string Name, string StandardId)
    {
        //Escape single quote
        Name = Name.Replace("'", "''");
        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Subs WHERE [Name] = '" + Name + "' and StandardTextlistId = '" + StandardId + "'";

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
    public bool NamesExists(string Name, string SubId)
    {
        try
        {
            //Escape single quote
            Name = Name.Replace("'", "''");
            //Escape single quote

            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Subs WHERE Name='" + Name + "' AND NOT SubId='" + SubId + "'";


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
}
