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
/// Summary description for HomeWorkDAL
/// </summary>
public class HomeWorkDAL
{
    private GeneralDAL _generalDAL;

    public HomeWorkDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~HomeWorkDAL()
    {
        _generalDAL = null;
    }
    public HomeWorkDTO Select(string HomeWorkId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader dr;
        HomeWorkDTO HomeWorkDTO = new HomeWorkDTO();
        //SqlDataReader reader = sqlCmd.ExecuteReader();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT a.HomeWork,a.HomeWorkId,a.A1,a.A2,a.A3,a.A4,a.Ans,b.SubId,b.Name " +
                             " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                             " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                             " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                             " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                             " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                             " , b.StandardTextListId, a.ChapterId, a.SrNo, a.HomeWorkType, a.HomeWorkDataType,a.NoOfFile " +
                             " , replace(a.SampleAns1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns1' " +
                             " , replace(a.SampleAns2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns2' " +
                             " , replace(a.SampleAns3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns3' " +
                             " , replace(a.SampleAns4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns4' " +
                             " , a.AnsSelection,a.Dt, a.ChapterVideoId " +
                             " FROM HomeWorks a " +
                             " inner join Subs b on b.SubId= a.SubId " +
                             " WHERE HomeWorkId='" + HomeWorkId + "'";
        dr = sqlCmd.ExecuteReader();

        while (dr.Read())
        {
            if (dr["HomeWorkId"] != DBNull.Value)
                HomeWorkDTO.HomeWorkId = dr["HomeWorkId"].ToString();

            if (dr["SubId"] != DBNull.Value)
                HomeWorkDTO.SubId = dr["SubId"].ToString();

            if (dr["HomeWork"] != DBNull.Value)
                HomeWorkDTO.HomeWork = dr["HomeWork"].ToString();

            if (dr["A1"] != DBNull.Value)
                HomeWorkDTO.A1 = dr["A1"].ToString();

            if (dr["A2"] != DBNull.Value)
                HomeWorkDTO.A2 = dr["A2"].ToString();

            if (dr["A3"] != DBNull.Value)
                HomeWorkDTO.A3 = dr["A3"].ToString();

            if (dr["A4"] != DBNull.Value)
                HomeWorkDTO.A4 = dr["A4"].ToString();

            if (dr["Ans"] != DBNull.Value)
                HomeWorkDTO.Ans = (dr["Ans"].ToString());
            else
                HomeWorkDTO.Ans = null;

            if (dr["Name"] != DBNull.Value)
                HomeWorkDTO.Subject = dr["Name"].ToString();


            if (dr["ImageNameQus"] != DBNull.Value)
                HomeWorkDTO.ImageNameQus = dr["ImageNameQus"].ToString();
            else
                HomeWorkDTO.ImageNameQus = null;


            if (dr["ImageNameA1"] != DBNull.Value)
                HomeWorkDTO.ImageNameA1 = dr["ImageNameA1"].ToString();
            else
                HomeWorkDTO.ImageNameA1 = null;

            if (dr["ImageNameA2"] != DBNull.Value)
                HomeWorkDTO.ImageNameA2 = dr["ImageNameA2"].ToString();
            else
                HomeWorkDTO.ImageNameA2 = null;

            if (dr["ImageNameA3"] != DBNull.Value)
                HomeWorkDTO.ImageNameA3 = dr["ImageNameA3"].ToString();
            else
                HomeWorkDTO.ImageNameA3 = null;

            if (dr["ImageNameA4"] != DBNull.Value)
                HomeWorkDTO.ImageNameA4 = dr["ImageNameA4"].ToString();
            else
                HomeWorkDTO.ImageNameA4 = null;

            if (dr["StandardTextListId"] != DBNull.Value)
                HomeWorkDTO.StandardTextListId = dr["StandardTextListId"].ToString();
            else
                HomeWorkDTO.StandardTextListId = null;

            if (dr["ChapterId"] != DBNull.Value)
                HomeWorkDTO.ChapterId = dr["ChapterId"].ToString();
            else
                HomeWorkDTO.ChapterId = null;

            if (dr["SrNo"] != DBNull.Value)
                HomeWorkDTO.SrNo = Convert.ToInt16(dr["SrNo"].ToString());
            else
                HomeWorkDTO.SrNo = null;

            if (dr["HomeWorkType"] != DBNull.Value)
                HomeWorkDTO.HomeWorkType = dr["HomeWorkType"].ToString();
            else
                HomeWorkDTO.HomeWorkType = null;

            if (dr["HomeWorkDataType"] != DBNull.Value)
                HomeWorkDTO.HomeWorkDataType = dr["HomeWorkDataType"].ToString();
            else
                HomeWorkDTO.HomeWorkDataType = null;

            if (dr["NoOfFile"] != DBNull.Value)
                HomeWorkDTO.NoOfFile = Convert.ToInt16(dr["NoOfFile"].ToString());
            else
                HomeWorkDTO.NoOfFile = null;

            if (dr["SampleAns1"] != DBNull.Value)
                HomeWorkDTO.SampleAns1 = dr["SampleAns1"].ToString();
            else
                HomeWorkDTO.SampleAns1 = null;

            if (dr["SampleAns2"] != DBNull.Value)
                HomeWorkDTO.SampleAns2 = dr["SampleAns2"].ToString();
            else
                HomeWorkDTO.SampleAns2 = null;

            if (dr["SampleAns3"] != DBNull.Value)
                HomeWorkDTO.SampleAns3 = dr["SampleAns3"].ToString();
            else
                HomeWorkDTO.SampleAns3 = null;

            if (dr["SampleAns4"] != DBNull.Value)
                HomeWorkDTO.SampleAns4 = dr["SampleAns4"].ToString();
            else
                HomeWorkDTO.SampleAns4 = null;

            if (dr["AnsSelection"] != DBNull.Value)
                HomeWorkDTO.AnsSelection = dr["AnsSelection"].ToString();
            else
                HomeWorkDTO.AnsSelection = null;

            if (dr["Dt"] != DBNull.Value)
                HomeWorkDTO.Dt = Convert.ToDateTime(dr["Dt"]);
            else
                HomeWorkDTO.Dt = null;

            if (dr["ChapterVideoId"] != DBNull.Value)
                HomeWorkDTO.ChapterVideoId = Convert.ToInt16(dr["ChapterVideoId"]);
            else
                HomeWorkDTO.ChapterVideoId = null;
        }
        dr.Close();

        _generalDAL.CloseSQLConnection();

        return HomeWorkDTO;
    }
    public void Insert(HomeWorkDTO HomeWorkDTO)
    {
        //Escape single quote
        if (HomeWorkDTO.HomeWork != null)
            HomeWorkDTO.HomeWork = HomeWorkDTO.HomeWork.Replace("'", "''");

        if (HomeWorkDTO.A1 != null)
            HomeWorkDTO.A1 = HomeWorkDTO.A1.Replace("'", "''");

        if (HomeWorkDTO.A2 != null)
            HomeWorkDTO.A2 = HomeWorkDTO.A2.Replace("'", "''");

        if (HomeWorkDTO.A3 != null)
            HomeWorkDTO.A3 = HomeWorkDTO.A3.Replace("'", "''");

        if (HomeWorkDTO.A4 != null)
            HomeWorkDTO.A4 = HomeWorkDTO.A4.Replace("'", "''");

        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();

        try
        {
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO HomeWorks(HomeWorkId,SubId,HomeWork,A1,A2,A3,A4,Ans,ImageNameQus,ImageNameA1,ImageNameA2,ImageNameA3,ImageNameA4,InsertedOn,LastUpdatedOn," +
                                 "InsertedByUserId,LastUpdatedByUserId, ChapterId, SrNo, HomeWorkType, HomeWorkDataType, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection,Dt, ChapterVideoId)" +
                                 " VALUES(NewID()" +
                                 "," + ((HomeWorkDTO.SubId == null) ? "NULL" : "'" + HomeWorkDTO.SubId + "'") +
                                 "," + ((HomeWorkDTO.HomeWork == null) ? "NULL" : "N'" + HomeWorkDTO.HomeWork.ToString() + "'") +
                                 "," + ((HomeWorkDTO.A1 == null) ? "NULL" : "N'" + HomeWorkDTO.A1 + "'") +
                                 "," + ((HomeWorkDTO.A2 == null) ? "NULL" : "N'" + HomeWorkDTO.A2 + "'") +
                                 "," + ((HomeWorkDTO.A3 == null) ? "NULL" : "N'" + HomeWorkDTO.A3 + "'") +
                                 "," + ((HomeWorkDTO.A4 == null) ? "NULL" : "N'" + HomeWorkDTO.A4 + "'") +
                                 "," + ((HomeWorkDTO.Ans == null) ? "NULL" : "N'" + HomeWorkDTO.Ans.ToString().Replace("'", "''") + "'") +
                                 "," + ((HomeWorkDTO.ImageNameQus == null) ? "NULL" : "'" + HomeWorkDTO.ImageNameQus.ToString() + "'") +
                                 "," + ((HomeWorkDTO.ImageNameA1 == null) ? "NULL" : "'" + HomeWorkDTO.ImageNameA1.ToString() + "'") +
                                 "," + ((HomeWorkDTO.ImageNameA2 == null) ? "NULL" : "'" + HomeWorkDTO.ImageNameA2.ToString() + "'") +
                                 "," + ((HomeWorkDTO.ImageNameA3 == null) ? "NULL" : "'" + HomeWorkDTO.ImageNameA3.ToString() + "'") +
                                 "," + ((HomeWorkDTO.ImageNameA4 == null) ? "NULL" : "'" + HomeWorkDTO.ImageNameA4.ToString() + "'") +
                                 " ,GETDATE(),GETDATE(),NULL,NULL" +
                                 "," + ((HomeWorkDTO.ChapterId == null) ? "NULL" : "'" + HomeWorkDTO.ChapterId.ToString() + "'") +
                                 "," + ((HomeWorkDTO.SrNo == null) ? "NULL" : "'" + HomeWorkDTO.SrNo.ToString() + "'") +
                                 "," + ((HomeWorkDTO.HomeWorkType == null) ? "NULL" : "'" + HomeWorkDTO.HomeWorkType.ToString() + "'") +
                                 "," + ((HomeWorkDTO.HomeWorkDataType == null) ? "NULL" : "'" + HomeWorkDTO.HomeWorkDataType.ToString() + "'") +
                                 "," + ((HomeWorkDTO.NoOfFile == null) ? "NULL" : "'" + HomeWorkDTO.NoOfFile.ToString() + "'") +
                                 "," + ((HomeWorkDTO.SampleAns1 == null) ? "NULL" : "'" + HomeWorkDTO.SampleAns1.ToString().Replace("'", "''") + "'") +
                                 "," + ((HomeWorkDTO.SampleAns2 == null) ? "NULL" : "'" + HomeWorkDTO.SampleAns2.ToString().Replace("'", "''") + "'") +
                                 "," + ((HomeWorkDTO.SampleAns3 == null) ? "NULL" : "'" + HomeWorkDTO.SampleAns3.ToString().Replace("'", "''") + "'") +
                                 "," + ((HomeWorkDTO.SampleAns4 == null) ? "NULL" : "'" + HomeWorkDTO.SampleAns4.ToString().Replace("'", "''") + "'") +
                                 "," + ((HomeWorkDTO.AnsSelection == null) ? "NULL" : "'" + HomeWorkDTO.AnsSelection.ToString().Replace("'", "''") + "'") +
                                 "," + ((HomeWorkDTO.Dt == null) ? "NULL" : "'" + Convert.ToDateTime(HomeWorkDTO.Dt).ToString("dd-MMM-yyyy") + "'") +
                                 "," + ((HomeWorkDTO.ChapterVideoId == null) ? "NULL" : "'" + (HomeWorkDTO.ChapterVideoId).ToString() + "'") +
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
    public void Update(HomeWorkDTO HomeWorkDTO)
    {
        //Escape single quote
        if (HomeWorkDTO.HomeWork != null)
            HomeWorkDTO.HomeWork = HomeWorkDTO.HomeWork.Replace("'", "''");

        if (HomeWorkDTO.A1 != null)
            HomeWorkDTO.A1 = HomeWorkDTO.A1.Replace("'", "''");

        if (HomeWorkDTO.A2 != null)
            HomeWorkDTO.A2 = HomeWorkDTO.A2.Replace("'", "''");

        if (HomeWorkDTO.A3 != null)
            HomeWorkDTO.A3 = HomeWorkDTO.A3.Replace("'", "''");

        if (HomeWorkDTO.A4 != null)
            HomeWorkDTO.A4 = HomeWorkDTO.A4.Replace("'", "''");
        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "UPDATE HomeWorks SET " +
                             " SubId = " + ((HomeWorkDTO.SubId == null) ? "NULL" : "'" + HomeWorkDTO.SubId + "'") + "" +
                             ",HomeWork=" + ((HomeWorkDTO.HomeWork == null) ? "NULL" : "N'" + HomeWorkDTO.HomeWork + "'") + "" +
                             ",A1=" + ((HomeWorkDTO.A1 == null) ? "NULL" : "N'" + HomeWorkDTO.A1 + "'") + "" +
                             ",A2=" + ((HomeWorkDTO.A2 == null) ? "NULL" : "N'" + HomeWorkDTO.A2 + "'") + "" +
                             ",A3=" + ((HomeWorkDTO.A3 == null) ? "NULL" : "N'" + HomeWorkDTO.A3 + "'") + "" +
                             ",A4=" + ((HomeWorkDTO.A4 == null) ? "NULL" : "N'" + HomeWorkDTO.A4 + "'") + "" +
                             ",Ans=" + ((HomeWorkDTO.Ans == null) ? "NULL" : "N'" + HomeWorkDTO.Ans.ToString().Replace("'", "''") + "'") + "" +
                             ",LastUpdatedOn=GETDATE()" +
                             ",LastUpdatedByUserId=NULL" +
                             ",ChapterId  =" + ((HomeWorkDTO.ChapterId == null) ? "NULL" : "'" + HomeWorkDTO.ChapterId.ToString() + "'") +
                             ",SrNo  =" + ((HomeWorkDTO.SrNo == null) ? "NULL" : "'" + HomeWorkDTO.SrNo.ToString() + "'") +
                             ",HomeWorkType  =" + ((HomeWorkDTO.HomeWorkType == null) ? "NULL" : "'" + HomeWorkDTO.HomeWorkType.ToString() + "'") +
                             ",HomeWorkDataType  =" + ((HomeWorkDTO.HomeWorkDataType == null) ? "NULL" : "'" + HomeWorkDTO.HomeWorkDataType.ToString() + "'") +
                             ",NoOfFile  =" + ((HomeWorkDTO.NoOfFile == null) ? "NULL" : "'" + HomeWorkDTO.NoOfFile.ToString() + "'") +
                             ",AnsSelection  =" + ((HomeWorkDTO.AnsSelection == null) ? "NULL" : "'" + HomeWorkDTO.AnsSelection.ToString() + "'") +
                             ",Dt =" + ((HomeWorkDTO.Dt == null) ? "NULL" : "'" + Convert.ToDateTime(HomeWorkDTO.Dt).ToString("dd-MMM-yyyy") + "'") +
                             ",ChapterVideoId =" + ((HomeWorkDTO.ChapterVideoId == null) ? "NULL" : "'" + HomeWorkDTO.ChapterVideoId.ToString() + "'") +
                             " WHERE HomeWorkId='" + HomeWorkDTO.HomeWorkId + "'";
        sqlCmd.ExecuteNonQuery();

        if (HomeWorkDTO.IsHomeWorkChanged == true)
        {
            sqlCmd.CommandText = " update HomeWorks set ImageNameQus  =" + ((HomeWorkDTO.ImageNameQus == null) ? "NULL" : "'" + HomeWorkDTO.ImageNameQus.ToString() + "'") +
                                 " where HomeWorkId='" + HomeWorkDTO.HomeWorkId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (HomeWorkDTO.IsA1Changed == true)
        {
            sqlCmd.CommandText = " update HomeWorks set ImageNameA1  =" + ((HomeWorkDTO.ImageNameA1 == null) ? "NULL" : "'" + HomeWorkDTO.ImageNameA1.ToString() + "'") +
                                 " where HomeWorkId='" + HomeWorkDTO.HomeWorkId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (HomeWorkDTO.IsA2Changed == true)
        {
            sqlCmd.CommandText = " update HomeWorks set ImageNameA2  =" + ((HomeWorkDTO.ImageNameA2 == null) ? "NULL" : "'" + HomeWorkDTO.ImageNameA2.ToString() + "'") +
                                 " where HomeWorkId='" + HomeWorkDTO.HomeWorkId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (HomeWorkDTO.IsA3Changed == true)
        {
            sqlCmd.CommandText = " update HomeWorks set ImageNameA3  =" + ((HomeWorkDTO.ImageNameA3 == null) ? "NULL" : "'" + HomeWorkDTO.ImageNameA3.ToString() + "'") +
                                 " where HomeWorkId='" + HomeWorkDTO.HomeWorkId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (HomeWorkDTO.IsA4Changed == true)
        {
            sqlCmd.CommandText = " update HomeWorks set ImageNameA4  =" + ((HomeWorkDTO.ImageNameA4 == null) ? "NULL" : "'" + HomeWorkDTO.ImageNameA4.ToString() + "'") +
                                 " where HomeWorkId='" + HomeWorkDTO.HomeWorkId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (HomeWorkDTO.IsSampleAns1Changed == true)
        {
            sqlCmd.CommandText = " update HomeWorks set SampleAns1  = " + ((HomeWorkDTO.SampleAns1 == null) ? "NULL" : "'" + HomeWorkDTO.SampleAns1.ToString() + "'") +
                                 " where HomeWorkId='" + HomeWorkDTO.HomeWorkId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (HomeWorkDTO.IsSampleAns2Changed == true)
        {
            sqlCmd.CommandText = " update HomeWorks set SampleAns2  = " + ((HomeWorkDTO.SampleAns2 == null) ? "NULL" : "'" + HomeWorkDTO.SampleAns2.ToString() + "'") +
                                 " where HomeWorkId='" + HomeWorkDTO.HomeWorkId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (HomeWorkDTO.IsSampleAns3Changed == true)
        {
            sqlCmd.CommandText = " update HomeWorks set SampleAns3  = " + ((HomeWorkDTO.SampleAns3 == null) ? "NULL" : "'" + HomeWorkDTO.SampleAns3.ToString() + "'") +
                                 " where HomeWorkId='" + HomeWorkDTO.HomeWorkId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (HomeWorkDTO.IsSampleAns4Changed == true)
        {
            sqlCmd.CommandText = " update HomeWorks set SampleAns4  = " + ((HomeWorkDTO.SampleAns4 == null) ? "NULL" : "'" + HomeWorkDTO.SampleAns4.ToString() + "'") +
                                 " where HomeWorkId='" + HomeWorkDTO.HomeWorkId + "'";
            sqlCmd.ExecuteNonQuery();
        }
    }

    public void Delete(string HomeWorkId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..HomeWorks(HomeWorkId, SubId, HomeWork, A1, A2, A3, A4, Ans, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3, ImageNameA4, ChapterId, Hashtag, SrNo, HomeWorkType, HomeWorkDataType, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection,Dt, ChapterVideoId)" +
            " Select HomeWorkId, SubId, HomeWork, A1, A2, A3, A4, Ans, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3, ImageNameA4, ChapterId, Hashtag, SrNo, HomeWorkType, HomeWorkDataType, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection,Dt, ChapterVideoId " +
            " FROM HomeWorks WHERE HomeWorkId='" + HomeWorkId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM HomeWorks WHERE HomeWorkId='" + HomeWorkId + "'";
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
                strRef += _generalDAL.IsReferenced("HomeWorks", "HomeWorkId", HomeWorkId, sqlCmd, null);
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

    public DataTable LoadChapters(string SubId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select * from Chapters where SubId = '" + SubId.ToString() + "' order by ChapterName";

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

    public int getSrNo(string SubId, string ChapterId, int? ChapterVideoId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            int Sr;

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select count(*) + 1  from HomeWorks where SubId = '" + SubId.ToString() + "' and ChapterId = '" + ChapterId.ToString() + "' and ChapterVideoId = '" + ChapterVideoId.ToString() + "'";

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
    public void DeleteHomeWork(ArrayList al)
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
                sqlcmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..HomeWorks(HomeWorkId, SubId, HomeWork, A1, A2, A3, A4, Ans, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3, ImageNameA4, ChapterId, Hashtag, SrNo, HomeWorkType, HomeWorkDataType, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection,Dt)" +
                " Select HomeWorkId, SubId, HomeWork, A1, A2, A3, A4, Ans, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3, ImageNameA4, ChapterId, Hashtag, SrNo, HomeWorkType, HomeWorkDataType, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection,Dt " +
                " FROM HomeWorks WHERE HomeWorkId='" + id + "'";
                sqlcmd.ExecuteNonQuery();

                sqlcmd.CommandText = "DELETE FROM HomeWorks WHERE HomeWorkId='" + id + "'";
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

    public void PasteHomeWorkFrom(ArrayList al, HomeWorkDTO HomeWorkDTO)
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
            foreach (string id in al)
            {

                sqlcmd.CommandText = " INSERT INTO HomeWorks(HomeWorkId,SubId,HomeWork,A1,A2,A3,A4,Ans,ImageNameQus,ImageNameA1,ImageNameA2,ImageNameA3,ImageNameA4,InsertedOn,LastUpdatedOn," +
                                 "InsertedByUserId,LastUpdatedByUserId, ChapterId, SrNo, HomeWorkType, HomeWorkDataType, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection,Dt, ChapterVideoId)" +
                                 " select  NewID() HomeWorkId," + ((HomeWorkDTO.SubIdTo == null) ? "NULL" : "'" + HomeWorkDTO.SubIdTo + "'") + " SubId,HomeWork,A1,A2,A3,A4,Ans,ImageNameQus,ImageNameA1,ImageNameA2,ImageNameA3,ImageNameA4,Getdate() InsertedOn,Getdate() LastUpdatedOn," +
                                 "InsertedByUserId,LastUpdatedByUserId, " + ((HomeWorkDTO.TestIdTo == null) ? "NULL" : "'" + HomeWorkDTO.TestIdTo.ToString() + "'") +
                                 ", (select count(*) + 1  from HomeWorks where SubId = '" + HomeWorkDTO.SubIdTo.ToString() + "' and ChapterId = '" + HomeWorkDTO.TestIdTo.ToString() + "') SrNo, HomeWorkType, HomeWorkDataType, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection,Dt, ChapterVideoId " +
                                 " FROM HomeWorks WHERE HomeWorkId='" + id + "'";
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
}
