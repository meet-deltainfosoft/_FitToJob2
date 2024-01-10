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
/// Summary description for LiveClassDAL
/// </summary>
public class LiveClassDAL
{
    public GeneralDAL _generalDAL;

    public LiveClassDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~LiveClassDAL()
    {
        _generalDAL = null;
    }
    public LiveClassDTO Select(string LiveClassId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        LiveClassDTO LiveClassDTO = new LiveClassDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = " select * from LiveClasses a WHERE LiveClassId ='" + LiveClassId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["LiveClassId"] != DBNull.Value)
                LiveClassDTO.LiveClassId = sqlDr["LiveClassId"].ToString();

            if (sqlDr["SubId"] != DBNull.Value)
                LiveClassDTO.SubId = sqlDr["SubId"].ToString();
            else
                LiveClassDTO.SubId = null;

            if (sqlDr["SubName"] != DBNull.Value)
                LiveClassDTO.SubName = sqlDr["SubName"].ToString();
            else
                LiveClassDTO.SubName = null;

            if (sqlDr["StandardTextListId"] != DBNull.Value)
                LiveClassDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();
            else
                LiveClassDTO.StandardTextListId = null;

            if (sqlDr["Title"] != DBNull.Value)
                LiveClassDTO.Title = sqlDr["Title"].ToString();
            else
                LiveClassDTO.Title = null;

            if (sqlDr["TopicName"] != DBNull.Value)
                LiveClassDTO.TopicName = sqlDr["TopicName"].ToString();
            else
                LiveClassDTO.TopicName = null;

            if (sqlDr["Link"] != DBNull.Value)
                LiveClassDTO.Link = sqlDr["Link"].ToString();
            else
                LiveClassDTO.Link = null;

            if (sqlDr["Date"] != DBNull.Value)
                LiveClassDTO.Date = Convert.ToDateTime(sqlDr["Date"]);
            else
                LiveClassDTO.Date= null;

            if (sqlDr["FromTime"] != DBNull.Value)
                LiveClassDTO.FromTime = Convert.ToDateTime(sqlDr["FromTime"]);
            else
                LiveClassDTO.FromTime = null;

            if (sqlDr["ToTime"] != DBNull.Value)
                LiveClassDTO.ToTime = Convert.ToDateTime(sqlDr["ToTime"]);
            else
                LiveClassDTO.ToTime = null;

            if (sqlDr["Remarks"] != DBNull.Value)
                LiveClassDTO.Remarks = sqlDr["Remarks"].ToString();
            else
                LiveClassDTO.Remarks = null;
        }

        sqlDr.Close();
        _generalDAL.CloseSQLConnection();
        return LiveClassDTO;
    }
    public void Insert(LiveClassDTO liveClassDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string LiveClassId;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = " DECLARE  @LiveClassId uniqueidentifier;" +
                                 " SET @LiveClassId = NewId() " +
                                 " INSERT INTO LiveClasses (LiveClassId,StandardTextListId,SubId,SubName,Title,TopicName,Link,Date,FromTime,ToTime,Remarks," +
                                 " InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId)" +
                                 " VALUES (@LiveClassId," +
                                 ((liveClassDTO.StandardTextListId == null) ? "NULL" : "'" + liveClassDTO.StandardTextListId + "'") + "," +
                                 ((liveClassDTO.SubId == null) ? "NULL" : "'" + liveClassDTO.SubId + "'") + "," +
                                 ((liveClassDTO.SubName == null) ? "NULL" : "N'" + liveClassDTO.SubName.Replace("'","''") + "'") + "," +
                                 ((liveClassDTO.Title == null) ? "NULL" : "N'" + liveClassDTO.Title.Replace("'", "''") + "'") + "," +
                                 ((liveClassDTO.TopicName == null) ? "NULL" : "N'" + liveClassDTO.TopicName.Replace("'", "''") + "'") + "," +
                                 ((liveClassDTO.Link == null) ? "NULL" : "N'" + liveClassDTO.Link.Replace("'", "''") + "'") + "," +
                                 ((liveClassDTO.Date == null) ? "NULL" : "'" + Convert.ToDateTime(liveClassDTO.Date).ToString("dd-MMM-yyyy") + "'") + "," +
                                 ((liveClassDTO.FromTime == null) ? "NULL" : "'" + Convert.ToDateTime(liveClassDTO.FromTime).ToString("dd-MMM-yyyy hh:mm tt") + "'") + "," +
                                 ((liveClassDTO.ToTime == null) ? "NULL" : "'" + Convert.ToDateTime(liveClassDTO.ToTime).ToString("dd-MMM-yyyy hh:mm tt") + "'") + "," +
                                 ((liveClassDTO.Remarks == null) ? "NULL" : "N'" + liveClassDTO.Remarks.Replace("'", "''") + "'") + "," +
                                 "GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                 ");" +
                                 " SELECT @LiveClassId";

            LiveClassId = sqlCmd.ExecuteScalar().ToString();

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

    public void Update(LiveClassDTO LiveClassDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = " UPDATE LiveClasses SET " +
                             " StandardTextListId=" + ((LiveClassDTO.StandardTextListId == null) ? "NULL" : "'" + LiveClassDTO.StandardTextListId.ToString() + "'") + "" +
                             ",SubId = " + ((LiveClassDTO.SubId == null) ? "NULL" : "'" + LiveClassDTO.SubId + "'") + "" +
                             ",SubName = " + ((LiveClassDTO.SubName == null) ? "NULL" : "N'" + LiveClassDTO.SubName.Replace("'", "''") + "'") + "" +
                             ",Title  =" + ((LiveClassDTO.Title == null) ? "NULL" : "N'" + LiveClassDTO.Title.Replace("'", "''") + "'") +
                             ",TopicName  =" + ((LiveClassDTO.TopicName == null) ? "NULL" : "N'" + LiveClassDTO.TopicName.Replace("'", "''") + "'") +
                             ",Link  =" + ((LiveClassDTO.Link == null) ? "NULL" : "N'" + LiveClassDTO.Link.Replace("'", "''") + "'") +
                             ",Date =" + ((LiveClassDTO.Date == null) ? "NULL" : "'" + Convert.ToDateTime(LiveClassDTO.Date).ToString("dd-MMM-yyyy") + "'") +
                             ",FromTime  =" + ((LiveClassDTO.FromTime == null) ? "NULL" : "'" + Convert.ToDateTime(LiveClassDTO.FromTime).ToString("dd-MMM-yyyy hh:mm tt") + "'") +
                             ",ToTime  =" + ((LiveClassDTO.ToTime == null) ? "NULL" : "'" + Convert.ToDateTime(LiveClassDTO.ToTime).ToString("dd-MMM-yyyy hh:mm tt") + "'") +
                             ",Remarks=" + ((LiveClassDTO.Remarks == null) ? "NULL" : "N'" + LiveClassDTO.Remarks.ToString().Replace("'", "''") + "'") + "" +
                             ",LastUpdatedOn=GETDATE()" +
                             ",LastUpdatedByUserId='" + MySession.UserUnique + "'" +
                             " WHERE LiveClassId='" + LiveClassDTO.LiveClassId + "'";
        sqlCmd.ExecuteNonQuery();

    }


    public void Delete(string LiveClassId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..LiveClasses" +
                                 " (LiveClassId,StandardTextListId,SubId,SubName,Title,TopicName,Link,Date,FromTime,ToTime,Remarks," +
                                 " InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId)" +
                                 " Select LiveClassId, StandardTextListId,SubId,SubName,Title,TopicName,Link,Date,FromTime,ToTime,Remarks, " +
                                 " GETDATE(),LastUpdatedOn,'" + MySession.UserUnique + "', LastUpdatedByUserId " +
                                 " FROM LiveClasses WHERE LiveClassId='" + LiveClassId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM LiveClasses WHERE LiveClassId='" + LiveClassId + "'";
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