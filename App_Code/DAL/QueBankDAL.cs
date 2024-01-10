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
using System.IO;
using System.Collections;

/// <summary>
/// Summary description for QueDAL
/// </summary>
public class QueBankDAL
{
    private GeneralDAL _generalDAL;

    public QueBankDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~QueBankDAL()
    {
        _generalDAL = null;
    }
    public QueBankDTO Select(string QueBankId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader dr;
        QueBankDTO QueBankDTO = new QueBankDTO();
        //SqlDataReader reader = sqlCmd.ExecuteReader();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT a.Que,a.QueBankId,a.A1,a.A2,a.A3,a.A4,a.Ans,b.SubId,b.Name " +
                             " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                             " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                             " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                             " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                             " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                             " , b.StandardTextListId, a.ChapterId,c.ChapterName,a.PeriodNo, a.SrNo, a.QueType, a.QueDataType, a.RightMarks, a.WrongMarks, a.NonMarks, a.NoOfFile " +
                             " , replace(a.SampleAns1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns1' " +
                             " , replace(a.SampleAns2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns2' " +
                             " , replace(a.SampleAns3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns3' " +
                             " , replace(a.SampleAns4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns4' " +
                             " , a.AnsSelection,a.HashTag,a.LevelofQue, a.ChapterVideoId " +
                             " FROM QueBanks a " +
                             " left join Chapters c on c.ChapterId = a.ChapterID" +
                             " inner join Subs b on b.SubId= a.SubId " +
                             " WHERE QueBankId='" + QueBankId + "'";
        dr = sqlCmd.ExecuteReader();

        while (dr.Read())
        {
            if (dr["QueBankId"] != DBNull.Value)
                QueBankDTO.QueBankId = dr["QueBankId"].ToString();

            if (dr["SubId"] != DBNull.Value)
                QueBankDTO.SubId = dr["SubId"].ToString();

            if (dr["Que"] != DBNull.Value)
                QueBankDTO.Que = dr["Que"].ToString();

            if (dr["A1"] != DBNull.Value)
                QueBankDTO.A1 = dr["A1"].ToString();

            if (dr["A2"] != DBNull.Value)
                QueBankDTO.A2 = dr["A2"].ToString();

            if (dr["A3"] != DBNull.Value)
                QueBankDTO.A3 = dr["A3"].ToString();

            if (dr["A4"] != DBNull.Value)
                QueBankDTO.A4 = dr["A4"].ToString();

            if (dr["Ans"] != DBNull.Value)
                QueBankDTO.Ans = (dr["Ans"].ToString());
            else
                QueBankDTO.Ans = null;

            if (dr["Name"] != DBNull.Value)
                QueBankDTO.Subject = dr["Name"].ToString();


            if (dr["ImageNameQus"] != DBNull.Value)
                QueBankDTO.ImageNameQus = dr["ImageNameQus"].ToString();
            else
                QueBankDTO.ImageNameQus = null;


            if (dr["ImageNameA1"] != DBNull.Value)
                QueBankDTO.ImageNameA1 = dr["ImageNameA1"].ToString();
            else
                QueBankDTO.ImageNameA1 = null;

            if (dr["ImageNameA2"] != DBNull.Value)
                QueBankDTO.ImageNameA2 = dr["ImageNameA2"].ToString();
            else
                QueBankDTO.ImageNameA2 = null;

            if (dr["ImageNameA3"] != DBNull.Value)
                QueBankDTO.ImageNameA3 = dr["ImageNameA3"].ToString();
            else
                QueBankDTO.ImageNameA3 = null;

            if (dr["ImageNameA4"] != DBNull.Value)
                QueBankDTO.ImageNameA4 = dr["ImageNameA4"].ToString();
            else
                QueBankDTO.ImageNameA4 = null;

            if (dr["StandardTextListId"] != DBNull.Value)
                QueBankDTO.StandardTextListId = dr["StandardTextListId"].ToString();
            else
                QueBankDTO.StandardTextListId = null;

            if (dr["ChapterId"] != DBNull.Value)
                QueBankDTO.ChapterId = dr["ChapterId"].ToString();
            else
                QueBankDTO.ChapterId = null;

            //if (dr["ChapterIdMain"] != DBNull.Value)
            //    QueBankDTO.ChapterIdMain = dr["ChapterIdMain"].ToString();
            //else
            //    QueBankDTO.ChapterIdMain = null;

            if (dr["PeriodNo"] != DBNull.Value)
                QueBankDTO.PeriodNo = Convert.ToDecimal(dr["PeriodNo"].ToString());
            else
                QueBankDTO.PeriodNo = null;

            if (dr["SrNo"] != DBNull.Value)
                QueBankDTO.SrNo = Convert.ToInt16(dr["SrNo"].ToString());
            else
                QueBankDTO.SrNo = null;

            if (dr["QueType"] != DBNull.Value)
                QueBankDTO.QueType = dr["QueType"].ToString();
            else
                QueBankDTO.QueType = null;

            if (dr["QueDataType"] != DBNull.Value)
                QueBankDTO.QueDataType = dr["QueDataType"].ToString();
            else
                QueBankDTO.QueDataType = null;

            if (dr["RightMarks"] != DBNull.Value)
                QueBankDTO.RightMarks = Convert.ToDecimal(dr["RightMarks"].ToString());
            else
                QueBankDTO.RightMarks = null;

            if (dr["WrongMarks"] != DBNull.Value)
                QueBankDTO.WrongMarks = Convert.ToDecimal(dr["WrongMarks"].ToString());
            else
                QueBankDTO.WrongMarks = null;

            if (dr["NonMarks"] != DBNull.Value)
                QueBankDTO.NonMarks = Convert.ToDecimal(dr["NonMarks"].ToString());
            else
                QueBankDTO.NonMarks = null;

            if (dr["NoOfFile"] != DBNull.Value)
                QueBankDTO.NoOfFile = Convert.ToInt16(dr["NoOfFile"].ToString());
            else
                QueBankDTO.NoOfFile = null;

            if (dr["SampleAns1"] != DBNull.Value)
                QueBankDTO.SampleAns1 = dr["SampleAns1"].ToString();
            else
                QueBankDTO.SampleAns1 = null;

            if (dr["SampleAns2"] != DBNull.Value)
                QueBankDTO.SampleAns2 = dr["SampleAns2"].ToString();
            else
                QueBankDTO.SampleAns2 = null;

            if (dr["SampleAns3"] != DBNull.Value)
                QueBankDTO.SampleAns3 = dr["SampleAns3"].ToString();
            else
                QueBankDTO.SampleAns3 = null;

            if (dr["SampleAns4"] != DBNull.Value)
                QueBankDTO.SampleAns4 = dr["SampleAns4"].ToString();
            else
                QueBankDTO.SampleAns4 = null;

            if (dr["AnsSelection"] != DBNull.Value)
                QueBankDTO.AnsSelection = dr["AnsSelection"].ToString();
            else
                QueBankDTO.AnsSelection = null;

            if (dr["HashTag"] != DBNull.Value)
                QueBankDTO.HashTag = dr["HashTag"].ToString();
            else
                QueBankDTO.HashTag = null;

            if (dr["LevelofQue"] != DBNull.Value)
                QueBankDTO.LevelofQue = dr["LevelofQue"].ToString();
            else
                QueBankDTO.LevelofQue = null;

            if (dr["ChapterVideoId"] != DBNull.Value)
                QueBankDTO.ChapterVideoId = Convert.ToInt16(dr["ChapterVideoId"].ToString());
            else
                QueBankDTO.ChapterVideoId = null;
        }
        dr.Close();

        _generalDAL.CloseSQLConnection();

        return QueBankDTO;
    }
    public void Insert(QueBankDTO QueBankDTO)
    {
        //Escape single quote
        if (QueBankDTO.Que != null)
            QueBankDTO.Que = QueBankDTO.Que.Replace("'", "''");

        if (QueBankDTO.A1 != null)
            QueBankDTO.A1 = QueBankDTO.A1.Replace("'", "''");

        if (QueBankDTO.A2 != null)
            QueBankDTO.A2 = QueBankDTO.A2.Replace("'", "''");

        if (QueBankDTO.A3 != null)
            QueBankDTO.A3 = QueBankDTO.A3.Replace("'", "''");

        if (QueBankDTO.A4 != null)
            QueBankDTO.A4 = QueBankDTO.A4.Replace("'", "''");

        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();

        try
        {
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO QueBanks(QueBankId,SubId,Que,A1,A2,A3,A4,Ans,ImageNameQus,ImageNameA1,ImageNameA2,ImageNameA3,ImageNameA4,InsertedOn,LastUpdatedOn," +
                                 "InsertedByUserId,LastUpdatedByUserId, ChapterId,PeriodNo, SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection,HashTag,LevelofQue, ChapterVideoId)" +
                                 " VALUES(NewID()" +
                                 "," + ((QueBankDTO.SubId == null) ? "NULL" : "'" + QueBankDTO.SubId + "'") +
                                 "," + ((QueBankDTO.Que == null) ? "NULL" : "N'" + QueBankDTO.Que.ToString() + "'") +
                                 "," + ((QueBankDTO.A1 == null) ? "NULL" : "N'" + QueBankDTO.A1 + "'") +
                                 "," + ((QueBankDTO.A2 == null) ? "NULL" : "N'" + QueBankDTO.A2 + "'") +
                                 "," + ((QueBankDTO.A3 == null) ? "NULL" : "N'" + QueBankDTO.A3 + "'") +
                                 "," + ((QueBankDTO.A4 == null) ? "NULL" : "N'" + QueBankDTO.A4 + "'") +
                                 "," + ((QueBankDTO.Ans == null) ? "NULL" : "N'" + QueBankDTO.Ans.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueBankDTO.ImageNameQus == null) ? "NULL" : "'" + QueBankDTO.ImageNameQus.ToString() + "'") +
                                 "," + ((QueBankDTO.ImageNameA1 == null) ? "NULL" : "'" + QueBankDTO.ImageNameA1.ToString() + "'") +
                                 "," + ((QueBankDTO.ImageNameA2 == null) ? "NULL" : "'" + QueBankDTO.ImageNameA2.ToString() + "'") +
                                 "," + ((QueBankDTO.ImageNameA3 == null) ? "NULL" : "'" + QueBankDTO.ImageNameA3.ToString() + "'") +
                                 "," + ((QueBankDTO.ImageNameA4 == null) ? "NULL" : "'" + QueBankDTO.ImageNameA4.ToString() + "'") +
                                 " ,GETDATE(),GETDATE(),NULL,NULL" +
                                 "," + ((QueBankDTO.ChapterId == null) ? "NULL" : "'" + QueBankDTO.ChapterId.ToString() + "'") +
                                 "," + ((QueBankDTO.PeriodNo == null) ? "NULL" : "'" + QueBankDTO.PeriodNo.ToString() + "'") +
                                 "," + ((QueBankDTO.SrNo == null) ? "NULL" : "'" + QueBankDTO.SrNo.ToString() + "'") +
                                 "," + ((QueBankDTO.QueType == null) ? "NULL" : "'" + QueBankDTO.QueType.ToString() + "'") +
                                 "," + ((QueBankDTO.QueDataType == null) ? "NULL" : "'" + QueBankDTO.QueDataType.ToString() + "'") +
                                 "," + ((QueBankDTO.RightMarks == null) ? "NULL" : "'" + QueBankDTO.RightMarks.ToString() + "'") +
                                 "," + ((QueBankDTO.WrongMarks == null) ? "NULL" : "'" + QueBankDTO.WrongMarks.ToString() + "'") +
                                 "," + ((QueBankDTO.NonMarks == null) ? "NULL" : "'" + QueBankDTO.NonMarks.ToString() + "'") +
                                 "," + ((QueBankDTO.NoOfFile == null) ? "NULL" : "'" + QueBankDTO.NoOfFile.ToString() + "'") +
                                 "," + ((QueBankDTO.SampleAns1 == null) ? "NULL" : "'" + QueBankDTO.SampleAns1.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueBankDTO.SampleAns2 == null) ? "NULL" : "'" + QueBankDTO.SampleAns2.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueBankDTO.SampleAns3 == null) ? "NULL" : "'" + QueBankDTO.SampleAns3.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueBankDTO.SampleAns4 == null) ? "NULL" : "'" + QueBankDTO.SampleAns4.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueBankDTO.AnsSelection == null) ? "NULL" : "'" + QueBankDTO.AnsSelection.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueBankDTO.HashTag == null) ? "NULL" : "N'" + QueBankDTO.HashTag.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueBankDTO.LevelofQue == null) ? "NULL" : "'" + QueBankDTO.LevelofQue.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueBankDTO.ChapterVideoId == null) ? "NULL" : "'" + QueBankDTO.ChapterVideoId.ToString().Replace("'", "''") + "'") +
                                 ")";
            sqlCmd.ExecuteNonQuery();

            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }
    public void Update(QueBankDTO QueBankDTO)
    {
        //Escape single quote
        if (QueBankDTO.Que != null)
            QueBankDTO.Que = QueBankDTO.Que.Replace("'", "''");

        if (QueBankDTO.A1 != null)
            QueBankDTO.A1 = QueBankDTO.A1.Replace("'", "''");

        if (QueBankDTO.A2 != null)
            QueBankDTO.A2 = QueBankDTO.A2.Replace("'", "''");

        if (QueBankDTO.A3 != null)
            QueBankDTO.A3 = QueBankDTO.A3.Replace("'", "''");

        if (QueBankDTO.A4 != null)
            QueBankDTO.A4 = QueBankDTO.A4.Replace("'", "''");
        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "UPDATE QueBanks SET " +
                             " SubId = " + ((QueBankDTO.SubId == null) ? "NULL" : "'" + QueBankDTO.SubId + "'") + "" +
                             ",Que=" + ((QueBankDTO.Que == null) ? "NULL" : "N'" + QueBankDTO.Que + "'") + "" +
                             ",A1=" + ((QueBankDTO.A1 == null) ? "NULL" : "N'" + QueBankDTO.A1 + "'") + "" +
                             ",A2=" + ((QueBankDTO.A2 == null) ? "NULL" : "N'" + QueBankDTO.A2 + "'") + "" +
                             ",A3=" + ((QueBankDTO.A3 == null) ? "NULL" : "N'" + QueBankDTO.A3 + "'") + "" +
                             ",A4=" + ((QueBankDTO.A4 == null) ? "NULL" : "N'" + QueBankDTO.A4 + "'") + "" +
                             ",Ans=" + ((QueBankDTO.Ans == null) ? "NULL" : "N'" + QueBankDTO.Ans.ToString().Replace("'", "''") + "'") + "" +
                             ",LastUpdatedOn=GETDATE()" +
                             ",LastUpdatedByUserId=NULL" +
                             ",ChapterId  =" + ((QueBankDTO.ChapterId == null) ? "NULL" : "'" + QueBankDTO.ChapterId.ToString() + "'") +
                             ",PeriodNo  =" + ((QueBankDTO.PeriodNo == null) ? "NULL" : "'" + QueBankDTO.PeriodNo.ToString() + "'") +
                             ",SrNo  =" + ((QueBankDTO.SrNo == null) ? "NULL" : "'" + QueBankDTO.SrNo.ToString() + "'") +
                             ",QueType  =" + ((QueBankDTO.QueType == null) ? "NULL" : "'" + QueBankDTO.QueType.ToString() + "'") +
                             ",QueDataType  =" + ((QueBankDTO.QueDataType == null) ? "NULL" : "'" + QueBankDTO.QueDataType.ToString() + "'") +
                             ",RightMarks  =" + ((QueBankDTO.RightMarks == null) ? "NULL" : "'" + QueBankDTO.RightMarks.ToString() + "'") +
                             ",WrongMarks  =" + ((QueBankDTO.WrongMarks == null) ? "NULL" : "'" + QueBankDTO.WrongMarks.ToString() + "'") +
                             ",NonMarks  =" + ((QueBankDTO.NonMarks == null) ? "NULL" : "'" + QueBankDTO.NonMarks.ToString() + "'") +
                             ",NoOfFile  =" + ((QueBankDTO.NoOfFile == null) ? "NULL" : "'" + QueBankDTO.NoOfFile.ToString() + "'") +
                             ",AnsSelection  =" + ((QueBankDTO.AnsSelection == null) ? "NULL" : "'" + QueBankDTO.AnsSelection.ToString() + "'") +
                             ",HashTag  =" + ((QueBankDTO.HashTag == null) ? "NULL" : "N'" + QueBankDTO.HashTag.ToString().Replace("'", "''") + "'") +
                             ",LevelofQue  =" + ((QueBankDTO.LevelofQue == null) ? "NULL" : "'" + QueBankDTO.LevelofQue.ToString() + "'") +
                             ",ChapterVideoId  =" + ((QueBankDTO.ChapterVideoId == null) ? "NULL" : "'" + QueBankDTO.ChapterVideoId.ToString() + "'") +
                             " WHERE QueBankId='" + QueBankDTO.QueBankId + "'";
        sqlCmd.ExecuteNonQuery();

        if (QueBankDTO.IsQueChanged == true)
        {
            sqlCmd.CommandText = " update QueBanks set ImageNameQus  =" + ((QueBankDTO.ImageNameQus == null) ? "NULL" : "'" + QueBankDTO.ImageNameQus.ToString() + "'") +
                                 " where QueBankId='" + QueBankDTO.QueBankId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueBankDTO.IsA1Changed == true)
        {
            sqlCmd.CommandText = " update QueBanks set ImageNameA1  =" + ((QueBankDTO.ImageNameA1 == null) ? "NULL" : "'" + QueBankDTO.ImageNameA1.ToString() + "'") +
                                 " where QueBankId='" + QueBankDTO.QueBankId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueBankDTO.IsA2Changed == true)
        {
            sqlCmd.CommandText = " update QueBanks set ImageNameA2  =" + ((QueBankDTO.ImageNameA2 == null) ? "NULL" : "'" + QueBankDTO.ImageNameA2.ToString() + "'") +
                                 " where QueBankId='" + QueBankDTO.QueBankId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueBankDTO.IsA3Changed == true)
        {
            sqlCmd.CommandText = " update QueBanks set ImageNameA3  =" + ((QueBankDTO.ImageNameA3 == null) ? "NULL" : "'" + QueBankDTO.ImageNameA3.ToString() + "'") +
                                 " where QueBankId='" + QueBankDTO.QueBankId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueBankDTO.IsA4Changed == true)
        {
            sqlCmd.CommandText = " update QueBanks set ImageNameA4  =" + ((QueBankDTO.ImageNameA4 == null) ? "NULL" : "'" + QueBankDTO.ImageNameA4.ToString() + "'") +
                                 " where QueBankId='" + QueBankDTO.QueBankId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueBankDTO.IsSampleAns1Changed == true)
        {
            sqlCmd.CommandText = " update QueBanks set SampleAns1  = " + ((QueBankDTO.SampleAns1 == null) ? "NULL" : "'" + QueBankDTO.SampleAns1.ToString() + "'") +
                                 " where QueBankId='" + QueBankDTO.QueBankId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueBankDTO.IsSampleAns2Changed == true)
        {
            sqlCmd.CommandText = " update QueBanks set SampleAns2  = " + ((QueBankDTO.SampleAns2 == null) ? "NULL" : "'" + QueBankDTO.SampleAns2.ToString() + "'") +
                                 " where QueBankId='" + QueBankDTO.QueBankId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueBankDTO.IsSampleAns3Changed == true)
        {
            sqlCmd.CommandText = " update QueBanks set SampleAns3  = " + ((QueBankDTO.SampleAns3 == null) ? "NULL" : "'" + QueBankDTO.SampleAns3.ToString() + "'") +
                                 " where QueBankId='" + QueBankDTO.QueBankId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueBankDTO.IsSampleAns4Changed == true)
        {
            sqlCmd.CommandText = " update QueBanks set SampleAns4  = " + ((QueBankDTO.SampleAns4 == null) ? "NULL" : "'" + QueBankDTO.SampleAns4.ToString() + "'") +
                                 " where QueBankId='" + QueBankDTO.QueBankId + "'";
            sqlCmd.ExecuteNonQuery();
        }
    }

    public void Delete(string QueBankId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..QueBanks(QueBankId, SubId, Que, A1, A2, A3, A4, Ans, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3, ImageNameA4, ChapterId,PeriodNo, Hashtag, SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection,LevelofQue, ChapterVideoId)" +
            " Select QueBankId, SubId, Que, A1, A2, A3, A4, Ans, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3, ImageNameA4, ChapterId,PeriodNo, Hashtag, SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection,LevelofQue, ChapterVideoId " +
            " FROM QueBanks WHERE QueBankId='" + QueBankId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM QueBanks WHERE QueBankId='" + QueBankId + "'";
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
    public string IsReferenced(string QueBankId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        string strRef = "";

        _generalDAL.OpenSQLConnection();

        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        try
        {
            if (QueBankId != null)
            {
                strRef += _generalDAL.IsReferenced("QueBanks", "QueBankId", QueBankId, sqlCmd, null);
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
            sqlCmd.CommandText = "select distinct ChapterId, ChapterName from Chapters where SubId = '" + SubId.ToString() + "' order by ChapterName";

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
            sqlCmd.CommandText = " select count(*) + 1  from QueBanks where SubId = '" + SubId.ToString() + "' " +
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
    public void DeleteQuestion(ArrayList al)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlcmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlcmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlcmd.CommandType = CommandType.Text;
        sqlcmd.Transaction = sqlTrans;

        string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');

        try
        {
            foreach (string id in al)
            {
                sqlcmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..QueBanks(QueBankId, SubId, Que, A1, A2, A3, A4, Ans, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3, ImageNameA4, ChapterId,PeriodNo, Hashtag, SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection,HashTag,LevelofQue)" +
                " Select QueBankId, SubId, Que, A1, A2, A3, A4, Ans, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3, ImageNameA4, ChapterId,PeriodNo, Hashtag, SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection,HashTag,LevelofQue " +
                " FROM QueBanks WHERE QueBankId='" + id + "'";
                sqlcmd.ExecuteNonQuery();

                sqlcmd.CommandText = "DELETE FROM QueBanks WHERE QueBankId='" + id + "'";
                sqlcmd.ExecuteNonQuery();
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

    public void PasteQuestionFrom(DataTable Dt, QueBankDTO QueBankDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlcmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlcmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlcmd.CommandType = CommandType.Text;
        sqlcmd.Transaction = sqlTrans;


        try
        {
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                sqlcmd.CommandText = " INSERT INTO Ques " +
                                     " (QueId,SubId,Que,A1,A2,A3,A4,Ans,ImageNameQus,ImageNameA1,ImageNameA2,ImageNameA3,ImageNameA4,InsertedOn,LastUpdatedOn," +
                                     " InsertedByUserId,LastUpdatedByUserId, TestId, SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile,   " +
                                     " SampleAns1,   SampleAns2, SampleAns3, SampleAns4, AnsSelection,HashTag,LevelofQue)" +
                                     " select  NewID(),SubId,Que,A1,A2,A3,A4,Ans,ImageNameQus,ImageNameA1,ImageNameA2,ImageNameA3,ImageNameA4,Getdate(),Getdate() " +
                                     ",InsertedByUserId,LastUpdatedByUserId, " + ((QueBankDTO.TestId == null) ? "NULL" : "'" + QueBankDTO.TestId + "'") + "  , SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks,NoOfFile, " +
                                     " SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection,HashTag,LevelofQue " +
                                     " FROM QueBanks WHERE QueBankId='" + Dt.Rows[i]["QueId"].ToString() + "'";
                sqlcmd.ExecuteNonQuery();
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

    public string GetNewID()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            string ID;
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = " Select NewID() ";

            if (sqlCmd.ExecuteScalar() != null)
                ID = sqlCmd.ExecuteScalar().ToString();
            else
                ID = null;

            _generalDAL.CloseSQLConnection();
            return ID;
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
}
