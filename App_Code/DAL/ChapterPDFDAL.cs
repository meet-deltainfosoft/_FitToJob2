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
/// Summary description for ChapterPDFDAL
/// </summary>
public class ChapterPDFDAL
{
    public GeneralDAL _generalDAL;

    public ChapterPDFDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~ChapterPDFDAL()
    {
        _generalDAL = null;
    }
    public void Insert(ChapterPDFDTO chapterPDFDTO)
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
            sqlCmd.CommandText = " INSERT INTO ChapterPdfs(ChapterId,PeriodNo,SrNo,FileName,FileLink,Remarks,StandardTextListId, SubId," +
                                 " InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId,ChapterVideoId)" +
                                 " VALUES(" +
                                 "" + ((chapterPDFDTO.ChapterId == null) ? "NULL" : "'" + chapterPDFDTO.ChapterId.Replace("'", "''") + "'") +
                                 "," + ((chapterPDFDTO.PeriodNo == null) ? "NULL" : "'" + chapterPDFDTO.PeriodNo.ToString().Replace("'", "''") + "'") +
                                 "," + ((chapterPDFDTO.SrNo == null) ? "NULL" : "'" + chapterPDFDTO.SrNo.Replace("'", "''") + "'") +
                                 "," + ((chapterPDFDTO.FileName == null) ? "NULL" : "N'" + chapterPDFDTO.FileName.Replace("'", "''") + "'") +
                                 "," + ((chapterPDFDTO.UploadphotoPath == null) ? "NULL" : "N'" + chapterPDFDTO.UploadphotoPath.Replace("'", "''") + "'") +
                                 "," + ((chapterPDFDTO.Remarks == null) ? "NULL" : "N'" + chapterPDFDTO.Remarks.ToString().Replace("'", "''") + "'") +
                                 "," + ((chapterPDFDTO.StandardTextListId == null) ? "NULL" : "'" + chapterPDFDTO.StandardTextListId.Replace("'", "''") + "'") +
                                 "," + ((chapterPDFDTO.SubId == null) ? "NULL" : "'" + chapterPDFDTO.SubId.Replace("'", "''") + "'") +
                                 ",GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" + "," + ((chapterPDFDTO.ChapterVideoId == null) ? "NULL" : "'" + chapterPDFDTO.ChapterVideoId.ToString().Replace("'", "''") + "'") +
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
    public ChapterPDFDTO Select(string ChapterPDFId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        ChapterPDFDTO ChapterPDFDTO = new ChapterPDFDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = " select *, replace(a.FileLink, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'NewPhoto' " +
                             " from ChapterPdfs a inner join Chapters c on c.ChapterId = a.ChapterId " +
                             " WHERE ChapterPDFId='" + ChapterPDFId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["ChapterPDFId"] != DBNull.Value)
                ChapterPDFDTO.ChapterPDFId = sqlDr["ChapterPDFId"].ToString();

            if (sqlDr["ChapterId"] != DBNull.Value)
                ChapterPDFDTO.ChapterId = sqlDr["ChapterId"].ToString();
            else
                ChapterPDFDTO.ChapterId = null;

            //if (sqlDr["ChapterName"] != DBNull.Value)
            //    ChapterPDFDTO.ChapterId = sqlDr["ChapterName"].ToString();
            //else
            //    ChapterPDFDTO.ChapterId = null;

            if (sqlDr["PeriodNo"] != DBNull.Value)
                ChapterPDFDTO.PeriodNo = Convert.ToDecimal(sqlDr["PeriodNo"].ToString());
            else
                ChapterPDFDTO.PeriodNo = null;

            if (sqlDr["SrNo"] != DBNull.Value)
                ChapterPDFDTO.SrNo = sqlDr["SrNo"].ToString();
            else
                ChapterPDFDTO.SrNo = null;

            if (sqlDr["SubId"] != DBNull.Value)
                ChapterPDFDTO.SubId = sqlDr["SubId"].ToString();
            else
                ChapterPDFDTO.SubId = null;

            if (sqlDr["StandardTextListId"] != DBNull.Value)
                ChapterPDFDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();
            else
                ChapterPDFDTO.StandardTextListId = null;
            if (sqlDr["SubId"] != DBNull.Value)
                ChapterPDFDTO.SubId = sqlDr["SubId"].ToString();

            if (sqlDr["NewPhoto"] != DBNull.Value)
                ChapterPDFDTO.UploadphotoPath = sqlDr["NewPhoto"].ToString();
            else
                ChapterPDFDTO.UploadphotoPath = null;

            if (sqlDr["FileName"] != DBNull.Value)
                ChapterPDFDTO.FileName = sqlDr["FileName"].ToString();
            else
                ChapterPDFDTO.FileName = null;

            if (sqlDr["Remarks"] != DBNull.Value)
                ChapterPDFDTO.Remarks = sqlDr["Remarks"].ToString();
            else
                ChapterPDFDTO.Remarks = null;

            if (sqlDr["ChapterVideoId"] != DBNull.Value)
                ChapterPDFDTO.ChapterVideoId = Convert.ToInt16(sqlDr["ChapterVideoId"].ToString());
            else
                ChapterPDFDTO.ChapterVideoId = null;
        }

        sqlDr.Close();
        _generalDAL.CloseSQLConnection();
        return ChapterPDFDTO;
    }

    public void Delete(string ChapterPDFId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..ChapterPdfs(ChapterId,PeriodNo, SrNo, FileName, FileLink, Remarks, SubId, StandardTextListId, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ChapterVideoId)" +
            " Select ChapterId,PeriodNo, SrNo, FileName, FileLink, Remarks, SubId, StandardTextListId, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, ChapterVideoId " +
            " FROM ChapterPdfs WHERE ChapterPDFId='" + ChapterPDFId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM ChapterPdfs WHERE ChapterPDFId='" + ChapterPDFId + "'";
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

    public DataTable LoadPeriodNo(string ChapterId, string SubId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select Distinct convert(DOUBLE PRECISION,PeriodNo) as PeriodNo From Chapters where ChapterId = '" + ChapterId.ToString() + "' and SubId = '" + SubId + "' order by convert(DOUBLE PRECISION,PeriodNo)";

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

    public void Update(ChapterPDFDTO ChapterPDFDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "UPDATE ChapterPdfs SET " +
                             " SubId = " + ((ChapterPDFDTO.SubId == null) ? "NULL" : "'" + ChapterPDFDTO.SubId + "'") + "" +
                             ",PeriodNo  =" + ((ChapterPDFDTO.PeriodNo == null) ? "NULL" : "'" + ChapterPDFDTO.PeriodNo.ToString() + "'") +
                             ",SrNo  =" + ((ChapterPDFDTO.SrNo == null) ? "NULL" : "N'" + ChapterPDFDTO.SrNo.ToString() + "'") +
                             ",ChapterId=" + ((ChapterPDFDTO.ChapterId == null) ? "NULL" : "'" + ChapterPDFDTO.ChapterId.ToString() + "'") + "" +
                             ",Remarks=" + ((ChapterPDFDTO.Remarks == null) ? "NULL" : "N'" + ChapterPDFDTO.Remarks + "'") + "" +
                             ",StandardTextListId=" + ((ChapterPDFDTO.StandardTextListId == null) ? "NULL" : "'" + ChapterPDFDTO.StandardTextListId + "'") + "" +
                             ",FileName = " + ((ChapterPDFDTO.FileName == null) ? "NULL" : "N'" + ChapterPDFDTO.FileName.Replace("'", "''") + "'") + "" +
                             ",FileLink = " + ((ChapterPDFDTO.UploadphotoPath == null) ? "NULL" : "N'" + ChapterPDFDTO.UploadphotoPath.Replace("'", "''") + "'") + "" +
                             ",LastUpdatedOn=GETDATE()" +
                             ",LastUpdatedByUserId=NULL" +
                             ",ChapterVideoId  =" + ((ChapterPDFDTO.ChapterVideoId == null) ? "NULL" : "'" + ChapterPDFDTO.ChapterVideoId.ToString() + "'") +
                             " WHERE ChapterPDFId='" + ChapterPDFDTO.ChapterPDFId + "'";
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
            sqlCmd.CommandText = "select (isnull(Max(SrNo),0) + 1) as No from ChapterPdfs where SubId ='" + SubId + "' and StandardTextListId = '" + StandardTextListd + "' and ChapterId = '" + ChapterId + "' and PeriodNo = '" + PeriodNo + "'";

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
            sqlCmd.CommandText = " select count(*) + 1  from ChapterPdfs where SubId = '" + SubId.ToString() + "' " +
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