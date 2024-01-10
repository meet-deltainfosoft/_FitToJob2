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
/// Summary description for ChapterDAL
/// </summary>
public class ChapterDAL
{
    public GeneralDAL _generalDAL;
    public ChapterDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~ChapterDAL()
    {
        _generalDAL = null;
    }

    public void Insert(ChapterDTO chapterDTO, ArrayList alchapterLnDTO)
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
            sqlCmd.CommandText = " DECLARE  @ChapterId uniqueidentifier;" +
                                 " SET @ChapterId = NewId()" +
                                 " INSERT INTO Chapters(ChapterId, StandardTextListId, SubId, SrNo , ChapterName, Remarks,PeriodNo," +
                                 " InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " VALUES(@ChapterId" +
                                 "," + ((chapterDTO.StandardTextListId == null) ? "NULL" : "'" + chapterDTO.StandardTextListId.Replace("'", "''") + "'") +
                                 "," + ((chapterDTO.SubId == null) ? "NULL" : "'" + chapterDTO.SubId.Replace("'", "''") + "'") +
                                 "," + ((chapterDTO.SrNo == null) ? "NULL" : "'" + chapterDTO.SrNo.Replace("'", "''") + "'") +
                                 "," + ((chapterDTO.ChapterName == null) ? "NULL" : "N'" + chapterDTO.ChapterName.ToString().Replace("'", "''") + "'") +
                                 "," + ((chapterDTO.Remarks == null) ? "NULL" : "N'" + chapterDTO.Remarks.ToString().Replace("'", "''") + "'") +
                                 "," + ((chapterDTO.PeriodNo == null) ? "NULL" : "'" + chapterDTO.PeriodNo.ToString().Replace("'", "''") + "'") +
                                 ",GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                 ");" +
                                 " SELECT @ChapterId";

            ChapterId = sqlCmd.ExecuteScalar().ToString();

            if (alchapterLnDTO != null)
            {
                foreach (ChapterLnDTO chapterLnDTO in alchapterLnDTO)
                {
                    InsertChapterLn(chapterLnDTO, ChapterId, sqlCmd);
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
    private void InsertChapterLn(ChapterLnDTO chapterLnDTO, string ChapterId, SqlCommand sqlCmd)
    {
        //SO Other Details
        sqlCmd.CommandText = " INSERT INTO ChapterLns (ChapterLnId,ChapterId,LnNo,SubId,StandardTextListId,ChapterTextListId,PeriodNo" +
                             ",InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId, ChapterVideoId)" +
                             " VALUES (NEWID()" +
                             ",'" + ChapterId + "'," + chapterLnDTO.LnNo + ", " +
                             ((chapterLnDTO.SubId == null) ? "NULL" : "'" + chapterLnDTO.SubId.ToString() + "'") + "," +
                             ((chapterLnDTO.StandardTextListId == null) ? "NULL" : "'" + chapterLnDTO.StandardTextListId.ToString() + "'") + "," +
                             ((chapterLnDTO.ChapterTextListId == null) ? "NULL" : "(select Top 1 ChapterId From Chapters where ChapterId = N'" + chapterLnDTO.ChapterTextListId + "' and SubId = '" + chapterLnDTO.SubId.ToString() + "' " + ((chapterLnDTO.PeriodNo == null) ? "" : " and PeriodNo = '" + chapterLnDTO.PeriodNo.ToString() + "'") + ")") + "," +
                             ((chapterLnDTO.PeriodNo == null) ? "NULL" : "'" + chapterLnDTO.PeriodNo.ToString() + "'") + "," +
                             " GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                             ", " + ((chapterLnDTO.ChapterVideoId == null) ? "NULL" : "'" + chapterLnDTO.ChapterVideoId.ToString() + "'") +
                             ");";

        sqlCmd.ExecuteNonQuery();
        //SO Other Details
    }


    public ChapterDTO Select(string ChapterId, out ArrayList alChapterLnDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        ChapterDTO ChapterDTO = new ChapterDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "select * from Chapters a WHERE ChapterId='" + ChapterId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["ChapterId"] != DBNull.Value)
                ChapterDTO.ChapterId = sqlDr["ChapterId"].ToString();

            if (sqlDr["SrNo"] != DBNull.Value)
                ChapterDTO.SrNo = sqlDr["SrNo"].ToString();
            else
                ChapterDTO.SrNo = null;

            if (sqlDr["ChapterName"] != DBNull.Value)
                ChapterDTO.ChapterName = sqlDr["ChapterName"].ToString();
            else
                ChapterDTO.ChapterName = null;

            if (sqlDr["SubId"] != DBNull.Value)
                ChapterDTO.SubId = sqlDr["SubId"].ToString();
            else
                ChapterDTO.SubId = null;

            if (sqlDr["StandardTextListId"] != DBNull.Value)
                ChapterDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();
            else
                ChapterDTO.StandardTextListId = null;

            if (sqlDr["SubId"] != DBNull.Value)
                ChapterDTO.SubId = sqlDr["SubId"].ToString();

            if (sqlDr["Remarks"] != DBNull.Value)
                ChapterDTO.Remarks = sqlDr["Remarks"].ToString();
            else
                ChapterDTO.Remarks = null;

            if (sqlDr["PeriodNo"] != DBNull.Value)
                ChapterDTO.PeriodNo = Convert.ToDecimal(sqlDr["PeriodNo"].ToString());
            else
                ChapterDTO.PeriodNo = null;
        }

        sqlDr.Close();

        sqlCmd.CommandText = " select st.[Text] as 'StandardName',c.ChapterName as 'ChapterName',s.Name as 'SubjectName',a.*, cv.VideoName from ChapterLns a " +
                             " left join Subs s on s.SubId = a.SubId " +
                             " left join Chapters c on c.ChapterId = a.ChapterTextListId " +
                             " left join TextLists st on st.TextListId = a.StandardTextListId " +
                             " inner join ChapterVideos cv on cv.ChapterVideoId = a.ChapterVideoId " +
                             " WHERE a.ChapterId = '" + ChapterId + "' ";

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
        return ChapterDTO;
    }

    public void Delete(string ChapterId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..chapters(ChapterId, SrNo, ChapterName, SubId, Remarks, StandardTextListId,PeriodNo, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
            " Select ChapterId, SrNo, ChapterName, SubId, Remarks, StandardTextListId,PeriodNo, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId " +
            " FROM chapters WHERE ChapterId='" + ChapterId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM chapters WHERE ChapterId='" + ChapterId + "'";
            sqlCmd.ExecuteNonQuery();


            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..chapterlns(ChapterLnId,ChapterId,LnNo,SubId,StandardTextListId,ChapterTextListId,PeriodNo,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId, ChapterVideoId) " +
            " Select ChapterLnId,ChapterId,LnNo,SubId,StandardTextListId,ChapterTextListId,PeriodNo, getdate(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, ChapterVideoId " +
            " FROM chapterlns WHERE ChapterId='" + ChapterId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM chapterlns WHERE ChapterId='" + ChapterId + "'";
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
    public DataTable LoadChapterLn(string Standardtextlistid, string subId)
    {
        try
        {

            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();
            if (Standardtextlistid != null && subId != null)
            {
                sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
                sqlCmd.CommandText = "select distinct ChapterId, ChapterName From Chapters where StandardTextListId='" + Standardtextlistid + "' and SubId='" + subId + "' order by ChapterName";
            }
            else
            {
                sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
                sqlCmd.CommandText = "select distinct ChapterId, ChapterName From Chapters order by ChapterName";
            }
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

    public void Update(ChapterDTO ChapterDTO, ArrayList alChapterLnDTO, ArrayList alDeletedChapterLnDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "UPDATE Chapters SET " +
                             " SubId = " + ((ChapterDTO.SubId == null) ? "NULL" : "'" + ChapterDTO.SubId + "'") + "" +
                             ",SrNo  =" + ((ChapterDTO.SrNo == null) ? "NULL" : "N'" + ChapterDTO.SrNo.ToString() + "'") +
                             ",ChapterName=" + ((ChapterDTO.ChapterName == null) ? "NULL" : "N'" + ChapterDTO.ChapterName.ToString().Replace("'", "''") + "'") + "" +
                             ",Remarks = " + ((ChapterDTO.Remarks == null) ? "NULL" : "N'" + ChapterDTO.Remarks + "'") + "" +
                             ",PeriodNo = " + ((ChapterDTO.PeriodNo == null) ? "NULL" : "'" + ChapterDTO.PeriodNo + "'") + "" +
                             ",StandardTextListId=" + ((ChapterDTO.StandardTextListId == null) ? "NULL" : "'" + ChapterDTO.StandardTextListId + "'") + "" +
                             ",LastUpdatedOn=GETDATE()" +
                             ",LastUpdatedByUserId=NULL" +
                             " WHERE ChapterId='" + ChapterDTO.ChapterId + "'";
        sqlCmd.ExecuteNonQuery();

        //Add New & Edit Lines
        foreach (ChapterLnDTO ChapterLnDTO in alChapterLnDTO)
        {
            if (ChapterLnDTO.IsNew == true)
            {
                InsertChapterLn(ChapterLnDTO, ChapterDTO.ChapterId, sqlCmd);
            }
            else if (ChapterLnDTO.IsDirty == true)
            {
                UpdateChapterLn(ChapterLnDTO, ChapterDTO.ChapterId, sqlCmd);
            }
        }
        foreach (ChapterLnDTO ChapterLnDTO in alDeletedChapterLnDTO)
        {
            DeleteChapterLnDTO(ChapterLnDTO.ChapterLnId, sqlCmd);
        }

        _generalDAL.CloseSQLConnection();
    }
    private void DeleteChapterLnDTO(string ChapterLnId, SqlCommand sqlcmd)
    {
        //Del Inquiry Other Details
        sqlcmd.CommandText = "DELETE FROM ChapterLns WHERE ChapterLnId='" + ChapterLnId + "'";
        sqlcmd.ExecuteNonQuery();
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

    public string GetSrNo(string StandardTextListd, string SubId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();

            _generalDAL.OpenSQLConnection();
            SqlDataReader sqlDR;
            string no;

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select (isnull(Max(SrNo),0) + 1) as No from Chapters where SubId ='" + SubId + "' and StandardTextListId = '" + StandardTextListd + "'";

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


    public DataTable LoadPeriodNo(string ChapterId, string SubId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select distinct convert(DOUBLE PRECISION,PeriodNo) as PeriodNo From Chapters where ChapterId = '" + ChapterId + "' and SubId = '" + SubId + "' order by PeriodNo";

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
                strRef += _generalDAL.IsReferenced("Chapters", "ChapterId", HomeWorkId, sqlCmd, "'ChapterLns'");
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
}