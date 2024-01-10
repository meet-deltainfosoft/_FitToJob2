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

public class BatchDAL
{
    public GeneralDAL _generalDAL;
	public BatchDAL()
	{
        _generalDAL = new GeneralDAL();
	}
    ~BatchDAL()
    {
        _generalDAL = null;
    }

    public void Insert(BatchDTO batchDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string BatchId;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = " DECLARE  @BatchId uniqueidentifier;" +
                                 " SET @BatchId = NewId()" +
                                 " INSERT INTO Batchs(BatchId, StandardTextListId, BatchName, " +
                                 " InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " VALUES(@BatchId" +
                                 "," + ((batchDTO.StandardTextListId == null) ? "NULL" : "'" + batchDTO.StandardTextListId.Replace("'", "''") + "'") +
                                 "," + ((batchDTO.BatchName == null) ? "NULL" : "N'" + batchDTO.BatchName.ToString().Replace("'","''") + "'") +
                                 ",GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                 ");" +
                                 " SELECT @BatchId";

            BatchId = sqlCmd.ExecuteScalar().ToString();

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


    public BatchDTO Select(string BatchId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        BatchDTO BatchDTO = new BatchDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "select * from Batchs a WHERE BatchId='" + BatchId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["BatchId"] != DBNull.Value)
                BatchDTO.BatchId = sqlDr["BatchId"].ToString();
           
            if (sqlDr["BatchName"] != DBNull.Value)
                BatchDTO.BatchName = sqlDr["BatchName"].ToString();
            else
                BatchDTO.BatchName = null;

            if (sqlDr["StandardTextListId"] != DBNull.Value)
                BatchDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();
            else
                BatchDTO.StandardTextListId = null;
        }

        sqlDr.Close();
        _generalDAL.CloseSQLConnection();
        return BatchDTO;
    }

    public void Delete(string BatchId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..Batchs(BatchId, BatchName, StandardTextListId, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
            " Select BatchId,  BatchName, StandardTextListId, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId " +
            " FROM Batchs WHERE BatchId='" + BatchId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM Batchs WHERE BatchId='" + BatchId + "'";
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

    public void Update(BatchDTO BatchDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = " UPDATE Batchs SET " +
                             " BatchName=" + ((BatchDTO.BatchName == null) ? "NULL" : "N'" + BatchDTO.BatchName.ToString().Replace("'","''") + "'") + "" +
                             ",StandardTextListId=" + ((BatchDTO.StandardTextListId == null) ? "NULL" : "'" + BatchDTO.StandardTextListId + "'") + "" +
                             ",LastUpdatedOn=GETDATE()" +
                             ",LastUpdatedByUserId=NULL" + 
                             " WHERE BatchId='" + BatchDTO.BatchId + "'";
        sqlCmd.ExecuteNonQuery();

    }
}