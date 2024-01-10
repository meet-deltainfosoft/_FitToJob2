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
/// Summary description for VideoNoDAL
/// </summary>
public class VideoNoDAL
{
    public GeneralDAL _generalDAL;

    public VideoNoDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~VideoNoDAL()
    {
        _generalDAL = null;
    }
    public VideoNoDTO Select(string VideoNoId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        VideoNoDTO VideoNoDTO = new VideoNoDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = " select a.*,c.ChapterName from VideoNos a inner join Chapters c on c.ChapterId = a.ChapterId WHERE VideoNoId ='" + VideoNoId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["VideoNoId"] != DBNull.Value)
                VideoNoDTO.VideoNoId = sqlDr["VideoNoId"].ToString();

            if (sqlDr["ChapterName"] != DBNull.Value)
                VideoNoDTO.ChapterId = sqlDr["ChapterName"].ToString();
            else
                VideoNoDTO.ChapterId = null;

            if (sqlDr["SubId"] != DBNull.Value)
                VideoNoDTO.SubId = sqlDr["SubId"].ToString();
            else
                VideoNoDTO.SubId = null;

            if (sqlDr["PeriodNo"] != DBNull.Value)
                VideoNoDTO.PeriodNo = Convert.ToDecimal(sqlDr["PeriodNo"].ToString());
            else
                VideoNoDTO.PeriodNo = null;

            if (sqlDr["StandardTextListId"] != DBNull.Value)
                VideoNoDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();
            else
                VideoNoDTO.StandardTextListId = null;

            if (sqlDr["PersonName1"] != DBNull.Value)
                VideoNoDTO.PersonName1 = sqlDr["PersonName1"].ToString();
            else
                VideoNoDTO.PersonName1 = null;

            if (sqlDr["PersonName2"] != DBNull.Value)
                VideoNoDTO.PersonName2 = sqlDr["PersonName2"].ToString();
            else
                VideoNoDTO.PersonName2 = null;

            if (sqlDr["PersonName3"] != DBNull.Value)
                VideoNoDTO.PersonName3 = sqlDr["PersonName3"].ToString();
            else
                VideoNoDTO.PersonName3 = null;

            if (sqlDr["PersonName4"] != DBNull.Value)
                VideoNoDTO.PersonName4 = sqlDr["PersonName4"].ToString();
            else
                VideoNoDTO.PersonName4 = null;

            if (sqlDr["PersonName5"] != DBNull.Value)
                VideoNoDTO.PersonName5 = sqlDr["PersonName5"].ToString();
            else
                VideoNoDTO.PersonName5 = null;

            if (sqlDr["Ratio1"] != DBNull.Value)
                VideoNoDTO.Ratio1 = sqlDr["Ratio1"].ToString();
            else
                VideoNoDTO.Ratio1 = null;

            if (sqlDr["Ratio2"] != DBNull.Value)
                VideoNoDTO.Ratio2 = sqlDr["Ratio2"].ToString();
            else
                VideoNoDTO.Ratio2 = null;

            if (sqlDr["Ratio3"] != DBNull.Value)
                VideoNoDTO.Ratio3 = sqlDr["Ratio3"].ToString();
            else
                VideoNoDTO.Ratio3 = null;

            if (sqlDr["Ratio4"] != DBNull.Value)
                VideoNoDTO.Ratio4 = sqlDr["Ratio4"].ToString();
            else
                VideoNoDTO.Ratio4 = null;

            if (sqlDr["Ratio5"] != DBNull.Value)
                VideoNoDTO.Ratio5 = sqlDr["Ratio5"].ToString();
            else
                VideoNoDTO.Ratio5 = null;
        }

        sqlDr.Close();
        _generalDAL.CloseSQLConnection();
        return VideoNoDTO;
    }
    public void Insert(VideoNoDTO chapterVedioDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string ChapterId;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = " INSERT INTO VideoNos(ChapterId,PeriodNo,StandardTextListId," +
                                 " SubId,PersonName1,PersonName2,PersonName3,PersonName4,PersonName5,Ratio1,Ratio2,Ratio3,Ratio4,Ratio5," +
                                 " InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " VALUES(" +
                                 ((chapterVedioDTO.ChapterId == null) ? "NULL" : "(select Top 1 ChapterId From Chapters where ChapterId = '" + chapterVedioDTO.ChapterId + "' and SubId = '" + chapterVedioDTO.SubId.ToString() + "' " + ((chapterVedioDTO.PeriodNo == null) ? "" : " and PeriodNo = '" + chapterVedioDTO.PeriodNo.ToString() + "'") + ")") +
                                 "," + ((chapterVedioDTO.PeriodNo == null) ? "NULL" : "'" + chapterVedioDTO.PeriodNo.ToString().Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.StandardTextListId == null) ? "NULL" : "'" + chapterVedioDTO.StandardTextListId.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.SubId == null) ? "NULL" : "'" + chapterVedioDTO.SubId.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.PersonName1 == null) ? "NULL" : "'" + chapterVedioDTO.PersonName1.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.PersonName2 == null) ? "NULL" : "'" + chapterVedioDTO.PersonName2.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.PersonName3 == null) ? "NULL" : "'" + chapterVedioDTO.PersonName3.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.PersonName4 == null) ? "NULL" : "'" + chapterVedioDTO.PersonName4.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.PersonName5 == null) ? "NULL" : "'" + chapterVedioDTO.PersonName5.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.Ratio1 == null) ? "NULL" : "'" + chapterVedioDTO.Ratio1.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.Ratio2 == null) ? "NULL" : "'" + chapterVedioDTO.Ratio2.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.Ratio3 == null) ? "NULL" : "'" + chapterVedioDTO.Ratio3.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.Ratio4 == null) ? "NULL" : "'" + chapterVedioDTO.Ratio4.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.Ratio5 == null) ? "NULL" : "'" + chapterVedioDTO.Ratio5.Replace("'", "''") + "'") +
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
    public DataTable LoadChapter(string SubId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select distinct ChapterId, ChapterName From Chapters where SubId = '" + SubId.ToString() + "' order by ChapterName";

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

    public void Update(VideoNoDTO VideoNoDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "UPDATE VideoNos SET " +
                             " ChapterId=" + ((VideoNoDTO.ChapterId == null) ? "NULL" : "(select Top 1 ChapterId From Chapters where ChapterId = '" + VideoNoDTO.ChapterId + "' and SubId = '" + VideoNoDTO.SubId.ToString() + "' " + ((VideoNoDTO.PeriodNo == null) ? "" : " and PeriodNo = '" + VideoNoDTO.PeriodNo.ToString() + "'") + ")") +
                             ",SubId = " + ((VideoNoDTO.SubId == null) ? "NULL" : "'" + VideoNoDTO.SubId + "'") + "" +
                             ",PeriodNo  =" + ((VideoNoDTO.PeriodNo == null) ? "NULL" : "'" + VideoNoDTO.PeriodNo.ToString() + "'") +
                             ",StandardTextListId=" + ((VideoNoDTO.StandardTextListId == null) ? "NULL" : "'" + VideoNoDTO.StandardTextListId + "'") + "" +
                             ",PersonName1  =" + ((VideoNoDTO.PersonName1 == null) ? "NULL" : "'" + VideoNoDTO.PersonName1.ToString() + "'") +
                             ",PersonName2  =" + ((VideoNoDTO.PersonName2 == null) ? "NULL" : "'" + VideoNoDTO.PersonName2.ToString() + "'") +
                             ",PersonName3  =" + ((VideoNoDTO.PersonName3 == null) ? "NULL" : "'" + VideoNoDTO.PersonName3.ToString() + "'") +
                             ",PersonName4  =" + ((VideoNoDTO.PersonName4 == null) ? "NULL" : "'" + VideoNoDTO.PersonName4.ToString() + "'") +
                             ",PersonName5  =" + ((VideoNoDTO.PersonName5 == null) ? "NULL" : "'" + VideoNoDTO.PersonName5.ToString() + "'") +
                             ",Ratio1  =" + ((VideoNoDTO.Ratio1 == null) ? "NULL" : "'" + VideoNoDTO.Ratio1.ToString() + "'") +
                             ",Ratio2  =" + ((VideoNoDTO.Ratio2 == null) ? "NULL" : "'" + VideoNoDTO.Ratio2.ToString() + "'") +
                             ",Ratio3  =" + ((VideoNoDTO.Ratio3 == null) ? "NULL" : "'" + VideoNoDTO.Ratio3.ToString() + "'") +
                             ",Ratio4  =" + ((VideoNoDTO.Ratio4 == null) ? "NULL" : "'" + VideoNoDTO.Ratio4.ToString() + "'") +
                             ",Ratio5  =" + ((VideoNoDTO.Ratio5 == null) ? "NULL" : "'" + VideoNoDTO.Ratio5.ToString() + "'") +
                             ",LastUpdatedOn=GETDATE()" +
                             ",LastUpdatedByUserId=NULL" +
                             " WHERE VideoNoId='" + VideoNoDTO.VideoNoId + "'";
        sqlCmd.ExecuteNonQuery();

    }

    public string GetSrNo(string StandardTextListd, string SubId, string ChapterId, decimal? PeriodNo)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();

            _generalDAL.OpenSQLConnection();
            SqlDataReader sqlDR;
            string no;

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select (isnull(Max(SrNo),0) + 1) as No from VideoNos where SubId ='" + SubId + "' and StandardTextListId = '" + StandardTextListd + "' and ChapterId = '" + ChapterId + "' and PeriodNo = '" + PeriodNo + "'";

            sqlDR = sqlCmd.ExecuteReader();

            if (sqlDR.Read())
            {
                no = sqlDR["No"].ToString();
            }
            else
            {
                no = null;
            }
            sqlDR.Close();
            _generalDAL.CloseSQLConnection();
            return no;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }

    }

    public void Delete(string VideoNoId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..VideoNos(VideoNoId, ChapterId, SubId,PeriodNo, StandardTextListId,PersonName1,PersonName2,PersonName3,PersonName4,PersonName5,Ratio1,Ratio2,Ratio3,Ratio4,Ratio5, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
            " Select VideoNoId, ChapterId, SubId,PeriodNo, StandardTextListId,PersonName1,PersonName2,PersonName3,PersonName4,PersonName5,Ratio1,Ratio2,Ratio3,Ratio4,Ratio5, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId " +
            " FROM VideoNos WHERE VideoNoId='" + VideoNoId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM VideoNos WHERE VideoNoId='" + VideoNoId + "'";
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