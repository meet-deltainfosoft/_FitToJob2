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
/// Summary description for ChapterVedioDAL
/// </summary>
public class ChapterVedioDAL
{
    public GeneralDAL _generalDAL;

    public ChapterVedioDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~ChapterVedioDAL()
    {
        _generalDAL = null;
    }

    public ChapterVedioDTO Select(string ChapterVedioId, out ArrayList alChapterLnDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        ChapterVedioDTO ChapterVedioDTO = new ChapterVedioDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = " select a.*,c.ChapterName from ChapterVideos a inner join Chapters c on c.ChapterID = a.ChapterId " +
            " WHERE a.ChapterVideoId ='" + ChapterVedioId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["ChapterVideoId"] != DBNull.Value)
                ChapterVedioDTO.ChapterVedioId = sqlDr["ChapterVideoId"].ToString();

            if (sqlDr["ChapterId"] != DBNull.Value)
                ChapterVedioDTO.ChapterId = sqlDr["ChapterId"].ToString();
            else
                ChapterVedioDTO.ChapterId = null;

            if (sqlDr["PeriodNo"] != DBNull.Value)
                ChapterVedioDTO.PeriodNo = Convert.ToDecimal(sqlDr["PeriodNo"].ToString());
            else
                ChapterVedioDTO.PeriodNo = null;

            if (sqlDr["SrNo"] != DBNull.Value)
                ChapterVedioDTO.SrNo = sqlDr["SrNo"].ToString();
            else
                ChapterVedioDTO.SrNo = null;

            if (sqlDr["SubId"] != DBNull.Value)
                ChapterVedioDTO.SubId = sqlDr["SubId"].ToString();
            else
                ChapterVedioDTO.SubId = null;

            if (sqlDr["StandardTextListId"] != DBNull.Value)
                ChapterVedioDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();
            else
                ChapterVedioDTO.StandardTextListId = null;

            if (sqlDr["VideoName"] != DBNull.Value)
                ChapterVedioDTO.VedioFileName = sqlDr["VideoName"].ToString();
            else
                ChapterVedioDTO.VedioFileName = null;

            if (sqlDr["VideoLink"] != DBNull.Value)
                ChapterVedioDTO.VedioLink = sqlDr["VideoLink"].ToString();
            else
                ChapterVedioDTO.VedioLink = null;

            if (sqlDr["Remarks"] != DBNull.Value)
                ChapterVedioDTO.Remarks = sqlDr["Remarks"].ToString();
            else
                ChapterVedioDTO.Remarks = null;

            if (sqlDr["IsDisabled"] != DBNull.Value)
                ChapterVedioDTO.IsDisabled = Convert.ToBoolean(sqlDr["IsDisabled"]);
            else
                ChapterVedioDTO.IsDisabled = false;
        }

        sqlDr.Close();

        sqlCmd.CommandText = " select st.[Text] as 'StandardName',c.ChapterName as 'ChapterName',s.Name as 'SubjectName',a.*, cv.VideoName from ChapterLns a " +
                             " left join Subs s on s.SubId = a.SubId " +
                             " left join Chapters c on c.ChapterId = a.ChapterTextListId " +
                             " left join TextLists st on st.TextListId = a.StandardTextListId " +
                             " inner join ChapterVideos cv on cv.ChapterVideoId = a.ChapterVideoId " +
                             " WHERE a.ChapterVideoHeaderId = '" + ChapterVedioId + "' ";

        sqlDr = sqlCmd.ExecuteReader();

        ArrayList alChapterLns = new ArrayList();

        while (sqlDr.Read())
        {
            ChapterLnDTO ChapterLnDTO = new ChapterLnDTO();

            if (sqlDr["SubId"] != DBNull.Value)
                ChapterLnDTO.SubId = sqlDr["SubId"].ToString();

            if (sqlDr["ChapterLnId"] != DBNull.Value)
                ChapterLnDTO.ChapterLnId = sqlDr["ChapterLnId"].ToString();

            if (sqlDr["StandardTextListId"] != DBNull.Value)
                ChapterLnDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();

            if (sqlDr["LnNo"] != DBNull.Value)
                ChapterLnDTO.LnNo = Convert.ToInt32(sqlDr["LnNo"]);

            if (sqlDr["ChapterTextListId"] != DBNull.Value)
                ChapterLnDTO.ChapterTextListId = sqlDr["ChapterTextListId"].ToString();

            if (sqlDr["StandardName"] != DBNull.Value)
                ChapterLnDTO.StandardName = sqlDr["StandardName"].ToString();

            if (sqlDr["ChapterName"] != DBNull.Value)
                ChapterLnDTO.ChapterName = sqlDr["ChapterName"].ToString();

            if (sqlDr["SubjectName"] != DBNull.Value)
                ChapterLnDTO.SubjectName = sqlDr["SubjectName"].ToString();

            if (sqlDr["PeriodNo"] != DBNull.Value)
                ChapterLnDTO.PeriodNo = Convert.ToDecimal(sqlDr["PeriodNo"].ToString());
            else
                ChapterLnDTO.PeriodNo = null;

            if (sqlDr["ChapterVideoId"] != DBNull.Value)
                ChapterLnDTO.ChapterVideoId = (sqlDr["ChapterVideoId"].ToString());
            else
                ChapterLnDTO.ChapterVideoId = null;

            if (sqlDr["VideoName"] != DBNull.Value)
                ChapterLnDTO.ChapterVideoName = (sqlDr["VideoName"].ToString());
            else
                ChapterLnDTO.ChapterVideoName = null;

            alChapterLns.Add(ChapterLnDTO);
        }

        _generalDAL.CloseSQLConnection();

        alChapterLnDTO = alChapterLns;

        return ChapterVedioDTO;
    }
    public void Insert(ChapterVedioDTO chapterVedioDTO, ArrayList alchapterLnDTO)
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
            sqlCmd.CommandText = " INSERT INTO ChapterVideos(ChapterId,SrNo,VideoName,VideoLink,PeriodNo,Remarks,StandardTextListId, SubId," +
                                 " InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, IsDisabled)" +
                                 " VALUES(" +
                                 "" + ((chapterVedioDTO.ChapterId == null) ? "NULL" : "'" + chapterVedioDTO.ChapterId.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.SrNo == null) ? "NULL" : "'" + chapterVedioDTO.SrNo.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.VedioFileName == null) ? "NULL" : "N'" + chapterVedioDTO.VedioFileName.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.VedioLink == null) ? "NULL" : "'" + chapterVedioDTO.VedioLink.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.PeriodNo == null) ? "NULL" : "'" + chapterVedioDTO.PeriodNo.ToString().Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.Remarks == null) ? "NULL" : "N'" + chapterVedioDTO.Remarks.ToString().Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.StandardTextListId == null) ? "NULL" : "'" + chapterVedioDTO.StandardTextListId.Replace("'", "''") + "'") +
                                 "," + ((chapterVedioDTO.SubId == null) ? "NULL" : "'" + chapterVedioDTO.SubId.Replace("'", "''") + "'") +
                                 ",GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                 "," + ((chapterVedioDTO.IsDisabled == null) ? "NULL" : "'" + chapterVedioDTO.IsDisabled + "'") +
                                 ");select @@IDENTITY";


            string ChapterVideoId = sqlCmd.ExecuteScalar().ToString();

            if (alchapterLnDTO != null)
            {
                foreach (ChapterLnDTO chapterLnDTO in alchapterLnDTO)
                {
                    InsertChapterLn(chapterLnDTO, ChapterVideoId, sqlCmd);
                }
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

    private void InsertChapterLn(ChapterLnDTO chapterLnDTO, string ChapterVideoId, SqlCommand sqlCmd)
    {
        //SO Other Details
        sqlCmd.CommandText = " INSERT INTO ChapterLns (ChapterLnId,ChapterId,LnNo,SubId,StandardTextListId,ChapterTextListId,PeriodNo" +
                             ",InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId, ChapterVideoId, ChapterVideoHeaderId)" +
                             " VALUES (NEWID()" +
                             ",'" + chapterLnDTO.ChapterTextListId + "'," + chapterLnDTO.LnNo + ", " +
                             ((chapterLnDTO.SubId == null) ? "NULL" : "'" + chapterLnDTO.SubId.ToString() + "'") + "," +
                             ((chapterLnDTO.StandardTextListId == null) ? "NULL" : "'" + chapterLnDTO.StandardTextListId.ToString() + "'") + "," +
                             ((chapterLnDTO.ChapterTextListId == null) ? "NULL" : "(select Top 1 ChapterId From Chapters where ChapterId = N'" + chapterLnDTO.ChapterTextListId + "' and SubId = '" + chapterLnDTO.SubId.ToString() + "' " + ((chapterLnDTO.PeriodNo == null) ? "" : " and PeriodNo = '" + chapterLnDTO.PeriodNo.ToString() + "'") + ")") + "," +
                             ((chapterLnDTO.PeriodNo == null) ? "NULL" : "'" + chapterLnDTO.PeriodNo.ToString() + "'") + "," +
                             " GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                             ", " + ((chapterLnDTO.ChapterVideoId == null) ? "NULL" : "'" + chapterLnDTO.ChapterVideoId.ToString() + "'") +
                             ", '" + ChapterVideoId + "' " +
                             ");";

        sqlCmd.ExecuteNonQuery();
        //SO Other Details
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

    public void Update(ChapterVedioDTO ChapterVedioDTO, ArrayList alChapterLnDTO, ArrayList alDeletedChapterLnDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "UPDATE ChapterVideos SET " +
                             " ChapterId=" + ((ChapterVedioDTO.ChapterId == null) ? "NULL" : "'" + ChapterVedioDTO.ChapterId.ToString() + "'") + "" +
                             ",SubId = " + ((ChapterVedioDTO.SubId == null) ? "NULL" : "'" + ChapterVedioDTO.SubId + "'") + "" +
                             ",SrNo  =" + ((ChapterVedioDTO.SrNo == null) ? "NULL" : "N'" + ChapterVedioDTO.SrNo.ToString() + "'") +
                             ",Remarks=" + ((ChapterVedioDTO.Remarks == null) ? "NULL" : "N'" + ChapterVedioDTO.Remarks + "'") + "" +
                             ",StandardTextListId=" + ((ChapterVedioDTO.StandardTextListId == null) ? "NULL" : "'" + ChapterVedioDTO.StandardTextListId + "'") + "" +
                             ",VideoName = " + ((ChapterVedioDTO.VedioFileName == null) ? "NULL" : "N'" + ChapterVedioDTO.VedioFileName.Replace("'", "''") + "'") + "" +
                             ",VideoLink = " + ((ChapterVedioDTO.VedioLink == null) ? "NULL" : "'" + ChapterVedioDTO.VedioLink.Replace("'", "''") + "'") + "" +
                             ",PeriodNo = " + ((ChapterVedioDTO.PeriodNo == null) ? "NULL" : "'" + ChapterVedioDTO.PeriodNo.ToString().Replace("'", "''") + "'") + "" +
                             ",LastUpdatedOn=GETDATE()" +
                             ",LastUpdatedByUserId=NULL" +
                             ",IsDisabled = " + ((ChapterVedioDTO.IsDisabled == null) ? "NULL" : "'" + ChapterVedioDTO.IsDisabled + "'") + "" +
                             " WHERE ChapterVideoId='" + ChapterVedioDTO.ChapterVedioId + "'";
        sqlCmd.ExecuteNonQuery();

        //Add New & Edit Lines
        foreach (ChapterLnDTO ChapterLnDTO in alChapterLnDTO)
        {
            if (ChapterLnDTO.IsNew == true)
            {
                InsertChapterLn(ChapterLnDTO, ChapterVedioDTO.ChapterVedioId, sqlCmd);
            }
            else if (ChapterLnDTO.IsDirty == true)
            {
                UpdateChapterLn(ChapterLnDTO, ChapterVedioDTO.ChapterVedioId, sqlCmd);
            }
        }
        foreach (ChapterLnDTO ChapterLnDTO in alDeletedChapterLnDTO)
        {
            DeleteChapterLnDTO(ChapterLnDTO.ChapterLnId, sqlCmd);
        }

        _generalDAL.CloseSQLConnection();
    }

    private void UpdateChapterLn(ChapterLnDTO ChapterLnDTO, string ChapterId, SqlCommand sqlCmd)
    {
        sqlCmd.CommandText = " UPDATE ChapterLns SET" +
                             " LnNo = " + ChapterLnDTO.LnNo + "" +
                             ",SubId = " + ((ChapterLnDTO.SubId == null) ? "NULL" : "'" + ChapterLnDTO.SubId + "'") +
                             ",StandardTextListId = " + ((ChapterLnDTO.StandardTextListId == null) ? "NULL" : "'" + ChapterLnDTO.StandardTextListId + "'") +
                             ",ChapterTextListId = " + ((ChapterLnDTO.ChapterTextListId == null) ? "NULL" : "(select Top 1 ChapterId From Chapters where ChapterId = N'" + ChapterLnDTO.ChapterTextListId + "' and SubId = '" + ChapterLnDTO.SubId.ToString() + "' " + ((ChapterLnDTO.PeriodNo == null) ? "" : " and PeriodNo = '" + ChapterLnDTO.PeriodNo.ToString() + "'") + ")") +
                             ",PeriodNo = " + ((ChapterLnDTO.PeriodNo == null) ? "NULL" : "'" + ChapterLnDTO.PeriodNo.ToString() + "'") +
                             ",LastUpdatedOn=GETDATE()" +
                             ",LastUpdatedByUserId='" + MySession.UserUnique + "'" +
                             ",ChapterVideoId = " + ((ChapterLnDTO.ChapterVideoId == null) ? "NULL" : "'" + ChapterLnDTO.ChapterVideoId.ToString() + "'") +
                             " WHERE ChapterLnId='" + ChapterLnDTO.ChapterLnId + "'";
        sqlCmd.ExecuteNonQuery();
    }

    private void DeleteChapterLnDTO(string ChapterLnId, SqlCommand sqlcmd)
    {
        //Del Inquiry Other Details
        sqlcmd.CommandText = "DELETE FROM ChapterLns WHERE ChapterLnId='" + ChapterLnId + "'";
        sqlcmd.ExecuteNonQuery();
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
            sqlCmd.CommandText = "select (isnull(Max(SrNo),0) + 1) as No from ChapterVideos where SubId ='" + SubId + "' and StandardTextListId = '" + StandardTextListd + "' and ChapterId = '" + ChapterId + "' and PeriodNo = '" + PeriodNo + "'";

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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..ChapterVideos(ChapterVideoId, ChapterId, SrNo, VideoName, VideoLink, Remarks, SubId, StandardTextListId,PeriodNo, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, IsDisabled)" +
            " Select ChapterVideoId, ChapterId, SrNo, VideoName, VideoLink, Remarks, SubId, StandardTextListId,PeriodNo, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, IsDisabled " +
            " FROM ChapterVideos WHERE ChapterVideoId='" + ChapterLinkId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM ChapterVideos WHERE ChapterVideoId='" + ChapterLinkId + "'";
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
                strRef += _generalDAL.IsReferenced("ChapterVideos", "ChapterVideoId", HomeWorkId, sqlCmd, null);
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

    public DataTable LoadSubjectsClear()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select * From Subs order by Name";

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


    //public DataTable LoadPeriodNo(string ChapterId, string SubId)
    //{
    //    try
    //    {
    //        SqlCommand sqlCmd = new SqlCommand();
    //        DataTable dt = new DataTable();

    //        _generalDAL.OpenSQLConnection();

    //        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
    //        sqlCmd.CommandText = "select distinct convert(DOUBLE PRECISION,PeriodNo) as PeriodNo From Chapters where ChapterId = '" + ChapterId + "' and SubId = '" + SubId + "' order by PeriodNo";

    //        dt.Load(sqlCmd.ExecuteReader());

    //        _generalDAL.CloseSQLConnection();

    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        _generalDAL.CloseSQLConnection();
    //        throw new Exception(ex.Message.ToString());
    //    }
    //}

    public DataTable LoadChapterVideo(string ChapterId, string SubId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = " select ChapterVideoId, VideoName from ChapterVideos where ChapterId = '" + ChapterId + "' and SubId = '" + SubId + "' order by SrNo";

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
}