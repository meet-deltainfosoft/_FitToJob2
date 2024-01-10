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
/// Summary description for ChapterLinkDAL
/// </summary>
public class ChapterLinkDAL
{
    public GeneralDAL _generalDAL;

    public ChapterLinkDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~ChapterLinkDAL()
    {
        _generalDAL = null;
    }
    public ChapterLinkDTO Select(string ChapterLinkId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        ChapterLinkDTO ChapterLinkDTO = new ChapterLinkDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = " select a.*,c.ChapterName from ChapterLinks a inner join Chapters c on c.ChapterId = a.ChapterId " +
            " WHERE ChapterLinkId ='" + ChapterLinkId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["ChapterLinkId"] != DBNull.Value)
                ChapterLinkDTO.ChapterLinkId = sqlDr["ChapterLinkId"].ToString();

            if (sqlDr["ChapterId"] != DBNull.Value)
                ChapterLinkDTO.ChapterId = sqlDr["ChapterId"].ToString();
            else
                ChapterLinkDTO.ChapterId = null;

            //if (sqlDr["ChapterName"] != DBNull.Value)
            //    ChapterLinkDTO.ChapterId = sqlDr["ChapterName"].ToString();
            //else
            //    ChapterLinkDTO.ChapterId = null;

            if (sqlDr["SrNo"] != DBNull.Value)
                ChapterLinkDTO.SrNo = sqlDr["SrNo"].ToString();
            else
                ChapterLinkDTO.SrNo = null;

            if (sqlDr["SubId"] != DBNull.Value)
                ChapterLinkDTO.SubId = sqlDr["SubId"].ToString();
            else
                ChapterLinkDTO.SubId = null;

            if (sqlDr["PeriodNo"] != DBNull.Value)
                ChapterLinkDTO.PeriodNo = Convert.ToDecimal(sqlDr["PeriodNo"].ToString());
            else
                ChapterLinkDTO.PeriodNo = null;

            if (sqlDr["StandardTextListId"] != DBNull.Value)
                ChapterLinkDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();
            else
                ChapterLinkDTO.StandardTextListId = null;

            if (sqlDr["LinkName"] != DBNull.Value)
                ChapterLinkDTO.LinkName = sqlDr["LinkName"].ToString();
            else
                ChapterLinkDTO.LinkName = null;

            if (sqlDr["Link"] != DBNull.Value)
                ChapterLinkDTO.Link = sqlDr["Link"].ToString();
            else
                ChapterLinkDTO.Link = null;

            if (sqlDr["Remarks"] != DBNull.Value)
                ChapterLinkDTO.Remarks = sqlDr["Remarks"].ToString();
            else
                ChapterLinkDTO.Remarks = null;

            if (sqlDr["ChapterVideoId"] != DBNull.Value)
                ChapterLinkDTO.ChapterVideoId = Convert.ToInt16(sqlDr["ChapterVideoId"].ToString());
            else
                ChapterLinkDTO.ChapterVideoId = null;
        }

        sqlDr.Close();
        _generalDAL.CloseSQLConnection();
        return ChapterLinkDTO;
    }
    public void Insert(ChapterLinkDTO chapterLinkDTO)
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
            sqlCmd.CommandText = " INSERT INTO ChapterLinks(ChapterId,SrNo,PeriodNo,LinkName,Link,Remarks,StandardTextListId, SubId," +
                                 " InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ChapterVideoId)" +
                                 " VALUES(" +
                                 "" + ((chapterLinkDTO.ChapterId == null) ? "NULL" : "'" + chapterLinkDTO.ChapterId.Replace("'", "''") + "'") +
                                 "," + ((chapterLinkDTO.SrNo == null) ? "NULL" : "'" + chapterLinkDTO.SrNo.Replace("'", "''") + "'") +
                                 "," + ((chapterLinkDTO.PeriodNo == null) ? "NULL" : "'" + chapterLinkDTO.PeriodNo.ToString().Replace("'", "''") + "'") +
                                 "," + ((chapterLinkDTO.LinkName == null) ? "NULL" : "N'" + chapterLinkDTO.LinkName.Replace("'", "''") + "'") +
                                 "," + ((chapterLinkDTO.Link == null) ? "NULL" : "N'" + chapterLinkDTO.Link.Replace("'", "''") + "'") +
                                 "," + ((chapterLinkDTO.Remarks == null) ? "NULL" : "N'" + chapterLinkDTO.Remarks.ToString().Replace("'", "''") + "'") +
                                 "," + ((chapterLinkDTO.StandardTextListId == null) ? "NULL" : "'" + chapterLinkDTO.StandardTextListId.Replace("'", "''") + "'") +
                                 "," + ((chapterLinkDTO.SubId == null) ? "NULL" : "'" + chapterLinkDTO.SubId.Replace("'", "''") + "'") +
                                 ",GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" + "," + ((chapterLinkDTO.ChapterVideoId == null) ? "NULL" : "'" + chapterLinkDTO.ChapterVideoId.ToString().Replace("'", "''") + "'") +
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
            sqlCmd.CommandText = "select Distinct ChapterId, ChapterName From Chapters where SubId = '" + SubId.ToString() + "' order by ChapterName";

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

    public void Update(ChapterLinkDTO ChapterLinkDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "UPDATE ChapterLinks SET " +
                             " ChapterId=" + ((ChapterLinkDTO.ChapterId == null) ? "NULL" : "'" + ChapterLinkDTO.ChapterId.ToString() + "'") + "" +
                             ",SubId = " + ((ChapterLinkDTO.SubId == null) ? "NULL" : "'" + ChapterLinkDTO.SubId + "'") + "" +
                             ",SrNo  =" + ((ChapterLinkDTO.SrNo == null) ? "NULL" : "N'" + ChapterLinkDTO.SrNo.ToString() + "'") +
                             ",PeriodNo  =" + ((ChapterLinkDTO.PeriodNo == null) ? "NULL" : "'" + ChapterLinkDTO.PeriodNo.ToString() + "'") +
                             ",Remarks=" + ((ChapterLinkDTO.Remarks == null) ? "NULL" : "N'" + ChapterLinkDTO.Remarks.ToString().Replace("'", "''") + "'") + "" +
                             ",StandardTextListId=" + ((ChapterLinkDTO.StandardTextListId == null) ? "NULL" : "'" + ChapterLinkDTO.StandardTextListId + "'") + "" +
                             ",LinkName = " + ((ChapterLinkDTO.LinkName == null) ? "NULL" : "N'" + ChapterLinkDTO.LinkName.Replace("'", "''") + "'") + "" +
                             ",Link = " + ((ChapterLinkDTO.Link == null) ? "NULL" : "N'" + ChapterLinkDTO.Link.Replace("'", "''") + "'") + "" +
                             ",LastUpdatedOn=GETDATE()" +
                             ",LastUpdatedByUserId=NULL" +
                             ",ChapterVideoId  =" + ((ChapterLinkDTO.ChapterVideoId == null) ? "NULL" : "'" + ChapterLinkDTO.ChapterVideoId.ToString() + "'") +
                             " WHERE ChapterLinkId='" + ChapterLinkDTO.ChapterLinkId + "'";
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
            sqlCmd.CommandText = "select (isnull(Max(SrNo),0) + 1) as No from ChapterLinks where SubId ='" + SubId + "' and StandardTextListId = '" + StandardTextListd + "' and ChapterId = '" + ChapterId + "' and PeriodNo = '" + PeriodNo + "'";

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

    public void Delete(string ChapterLinkId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..ChapterLinks(ChapterLinkId, ChapterId, SrNo, LinkName, Link, Remarks, SubId,PeriodNo, StandardTextListId, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ChapterVideoId)" +
            " Select ChapterLinkId, ChapterId, SrNo, LinkName, Link, Remarks, SubId,PeriodNo, StandardTextListId, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId , ChapterVideoId" +
            " FROM ChapterLinks WHERE ChapterLinkId='" + ChapterLinkId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM ChapterLinks WHERE ChapterLinkId='" + ChapterLinkId + "'";
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

    public string IsReferenced(string HomeWorkId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        string strRef = "";

        _generalDAL.OpenSQLConnection();

        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        try
        {
            if (HomeWorkId != null)
            {
                strRef += _generalDAL.IsReferenced("ChapterLinks", "ChapterLinkId", HomeWorkId, sqlCmd, null);
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
    public DataTable LoadChapterVideo(string ChapterID)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select ChapterVideoId, VideoName, SrNo from ChapterVideos where ChapterID = '" + ChapterID.ToString() + "' order by SrNo ";

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
    public int getSrNo(string SubId, string ChapterId, string ChapterVideoID)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            int Sr;

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = " select count(*) + 1  from ChapterLinks where SubId = '" + SubId.ToString() + "' " +
                                 " and ChapterId = '" + ChapterId.ToString() + "' and ChapterVideoId = '" + ChapterVideoID.ToString() + "'";

            if (sqlCmd.ExecuteScalar() != null)
                Sr = Convert.ToInt16(sqlCmd.ExecuteScalar());
            else
                Sr = 1;

            _generalDAL.CloseSQLConnection();

            return Sr;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }
}