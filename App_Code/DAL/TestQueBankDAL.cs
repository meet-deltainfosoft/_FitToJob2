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

public class TestQueBankDAL
{
    private GeneralDAL _generalDAL;

    public TestQueBankDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~TestQueBankDAL()
    {
        _generalDAL = null;
    }
    public TestQueBankDTO Select(string TestId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        TestQueBankDTO TestQueBankDTO = new TestQueBankDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = " Select *,p.StandardTextlistId from Tests a Inner join Patterns p on p.PatternId = a.PatternId WHERE a.TestId='" + TestId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["TestId"] != DBNull.Value)
                TestQueBankDTO.TestId = sqlDr["TestId"].ToString();

            if (sqlDr["StandardTextlistId"] != DBNull.Value)
                TestQueBankDTO.StandardId = sqlDr["StandardTextlistId"].ToString();

            if (sqlDr["TestName"] != DBNull.Value)
                TestQueBankDTO.Name = sqlDr["TestName"].ToString();

            if (sqlDr["PatternId"] != DBNull.Value)
                TestQueBankDTO.PatternId = sqlDr["PatternId"].ToString();

            if (sqlDr["NoOfEasyQue"] != DBNull.Value)
                TestQueBankDTO.Easy = Convert.ToInt16(sqlDr["NoOfEasyQue"].ToString());

            if (sqlDr["NoOfMediumQue"] != DBNull.Value)
                TestQueBankDTO.Medium = Convert.ToInt16(sqlDr["NoOfMediumQue"].ToString());

            if (sqlDr["NoOfHardQue"] != DBNull.Value)
                TestQueBankDTO.Hard = Convert.ToInt16(sqlDr["NoOfHardQue"].ToString());
        }

        sqlDr.Close();

        sqlCmd.CommandText = "Select  s.Name + '_'+ Convert(varchar(50),ISNULL(pl.NoOfMCQ,''))  +' Questions' as Name " +
                             " from TEsts t " +
                             " Inner join Patterns p on p.PatternId = t.PatternId " +
                             " Inner  join Patternlns pl on pl.PatternId = p.PatternId " +
                             " Inner Join Subs s on s.subid = pl.subid " +
                             " Where t.TestID = '" + TestId.ToString() + "'";

        DataTable dt = new DataTable();
        dt.Load(sqlCmd.ExecuteReader());

        TestQueBankDTO.dtDetaiils = dt;

        _generalDAL.CloseSQLConnection();

        return TestQueBankDTO;
    }
    public DataTable QueBankFoTest(string StandardId, string SubId, string ChapterId, int? Easy, int? Medium, int? Hard, string LevelOfQue, string PeriodNo)
    {
        try
        {
            string where;
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();
            where = "";

            if (StandardId != null)
            {
                where += " And b.StandardTextListId =  '" + StandardId + "'";
            }
            if (SubId != null)
            {
                where += " And b.SubId =  '" + SubId + "'";
            }
            if (ChapterId != null)
            {
                where += " And c.ChapterId =  '" + ChapterId + "'";
            }
            if (PeriodNo != null)
            {
                where += " And a.PeriodNo in (" + PeriodNo + ")";
            }

            //if (LevelOfQue.ToString().ToUpper() == "EASY")
            //    sqlCmd.CommandText = " Select " + ((Easy != null) ? "Top " + Easy : "") + " a.QueBankId,isnull(a.Que,a.ImageNameQus) as Question  ,isnull(a.A1,ImageNameA1) as A1,isnull(a.A2,ImageNameA2) as A2, " +
            //                          " isnull(a.A3,ImageNameA3) as A3,isnull(a.A4,ImageNameA4)  as A4,b.SubId,b.Name as Subject " +
            //                          " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
            //                          " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
            //                          " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
            //                          " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
            //                          " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
            //                          " , a.SrNo, a.InsertedOn,a.LevelofQue from QueBanks a " +
            //                          " left join Subs b on b.SubId = a.SubId " +
            //                          " Where a.LevelofQue = 'Easy' " +
            //                          where;
            //if(LevelOfQue.ToString().ToUpper() == "MEDIUM")
            //    sqlCmd.CommandText += " Select " + ((Medium != null) ? "Top " + Medium : "") + " a.QueBankId,isnull(a.Que,a.ImageNameQus) as Question  ,isnull(a.A1,ImageNameA1) as A1,isnull(a.A2,ImageNameA2) as A2, " +
            //                          " isnull(a.A3,ImageNameA3) as A3,isnull(a.A4,ImageNameA4)  as A4,b.SubId,b.Name as Subject " +
            //                          " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
            //                          " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
            //                          " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
            //                          " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
            //                          " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
            //                          " , a.SrNo, a.InsertedOn,a.LevelofQue from QueBanks a " +
            //                          " left join Subs b on b.SubId = a.SubId " +
            //                          " Where a.LevelofQue = 'Medium' " +
            //                          where;
            if (Easy == null && Medium == null && Hard == null && LevelOfQue != null)
            {
                sqlCmd.CommandText = " Select a.QueBankId,isnull(a.Que,a.ImageNameQus) as Question  ,isnull(a.A1,ImageNameA1) as A1,isnull(a.A2,ImageNameA2) as A2, " +
                                     " isnull(a.A3,ImageNameA3) as A3,isnull(a.A4,ImageNameA4)  as A4,b.SubId,b.Name as Subject " +
                                     " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                     " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                     " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                     " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                     " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                     " , a.SrNo, a.InsertedOn,a.LevelofQue from QueBanks a " +
                                     " left join Chapters c on c.ChapterId = a.ChapterId" +
                                     " left join Subs b on b.SubId = a.SubId " +
                                     " Where a.LevelofQue = '" + LevelOfQue.ToString() + "' " +
                                     where +
                                     " Order By a.SrNo, a.InsertedOn ";
            }
            else
            {
                sqlCmd.CommandText = " Select " + ((Easy != null) ? "Top " + Easy : "") + " a.QueBankId,isnull(a.Que,a.ImageNameQus) as Question  ,isnull(a.A1,ImageNameA1) as A1,isnull(a.A2,ImageNameA2) as A2, " +
                                      " isnull(a.A3,ImageNameA3) as A3,isnull(a.A4,ImageNameA4)  as A4,b.SubId,b.Name as Subject " +
                                      " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                      " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                      " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                      " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                      " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                      " , a.SrNo, a.InsertedOn,a.LevelofQue from QueBanks a " +
                                     " left join Chapters c on c.ChapterId = a.ChapterId" +
                                      " left join Subs b on b.SubId = a.SubId " +
                                      " Where a.LevelofQue = 'Easy' " +
                                      where +
                                      " Union " +
                                      " Select " + ((Medium != null) ? "Top " + Medium : "") + " a.QueBankId,isnull(a.Que,a.ImageNameQus) as Question  ,isnull(a.A1,ImageNameA1) as A1,isnull(a.A2,ImageNameA2) as A2, " +
                                      " isnull(a.A3,ImageNameA3) as A3,isnull(a.A4,ImageNameA4)  as A4,b.SubId,b.Name as Subject " +
                                      " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                      " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                      " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                      " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                      " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                      " , a.SrNo, a.InsertedOn,a.LevelofQue from QueBanks a " +
                                     " left join Chapters c on c.ChapterId = a.ChapterId" +
                                      " left join Subs b on b.SubId = a.SubId " +
                                      " Where a.LevelofQue = 'Medium' " +
                                      where +
                                      " Union " +
                                      " Select " + ((Hard != null) ? "Top " + Hard : "") + " a.QueBankId,isnull(a.Que,a.ImageNameQus) as Question  ,isnull(a.A1,ImageNameA1) as A1,isnull(a.A2,ImageNameA2) as A2, " +
                                      " isnull(a.A3,ImageNameA3) as A3,isnull(a.A4,ImageNameA4)  as A4,b.SubId,b.Name as Subject " +
                                      " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                      " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                      " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                      " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                      " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                      " , a.SrNo, a.InsertedOn,a.LevelofQue from QueBanks a " +
                                     " left join Chapters c on c.ChapterId = a.ChapterId" +
                                      " left join Subs b on b.SubId = a.SubId " +
                                      " Where a.LevelofQue = 'Hard' " +
                                      where +
                                      " Order By a.LevelofQue, a.InsertedOn ";
            }

            _generalDAL.OpenSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            dt.Load(sqlCmd.ExecuteReader());
            _generalDAL.CloseSQLConnection();
            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
    public DataTable LoadPatterns(string StandardId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = " select * from Patterns p " +
                                 " where p.StandardTextlistId = '" + StandardId.ToString() + "' order by p.PatternName ";

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
    public DataTable LoadSubjects(string PatternId, string TestId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            string where = "";
            if (TestId != null && TestId != "")
            {
                where = " and s.SubId not in ( Select SubId from QUes where TestId ='" + TestId.ToString() + "')";
            }
            sqlCmd.CommandText = " Select s.SubId, s.Name + '__MCQ: '+ Convert(varchar(50),ISNULL(pl.NoOfMCQ,''))  +' NonMCQ: '+ Convert(varchar(50),ISNULL(pl.NoOfNonMCQ,'')) as Name " +
                                 " From Patterns p " +
                                 " Inner join PatternLns pl on pl.PatternId = p.PatternId  " +
                                 " Inner Join Subs s on s.SubID = pl.SubId " +
                                 " where pl.PatternId = '" + PatternId.ToString() + "' " + where + " order by s.Name";

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
    public void InsertTest(TestQueBankDTO testQueBankDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;
        int count = 0;
        try
        {
            sqlCmd.CommandText = " Select Count(*) from Tests where TestId = '" + testQueBankDTO.TestId.ToString() + "'";
            count = Convert.ToInt16(sqlCmd.ExecuteScalar());

            if (count == 0)
            {
                sqlCmd.CommandText = " DECLARE  @TestId uniqueidentifier;" +
                                     " SET @TestId = NewId()" +
                                     " INSERT INTO Tests(TestId, TestName, PatternId,NoOfEasyQue,NoOfMediumQue,NoOfHardQue, " +
                                     " InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                     " VALUES(" + ((testQueBankDTO.TestId == null) ? "NULL" : "'" + testQueBankDTO.TestId.Replace("'", "''") + "'") + "," +
                                     ((testQueBankDTO.Name == null) ? "NULL" : "N'" + testQueBankDTO.Name.Replace("'", "''") + "'") + "," +
                                     ((testQueBankDTO.PatternId == null) ? "NULL" : "'" + testQueBankDTO.PatternId.ToString() + "'") + "," +
                                     ((testQueBankDTO.Easy == null) ? "NULL" : "" + Convert.ToInt32(testQueBankDTO.Easy) + "") + "," +
                                     ((testQueBankDTO.Medium == null) ? "NULL" : "" + Convert.ToInt32(testQueBankDTO.Medium) + "") + "," +
                                     ((testQueBankDTO.Hard == null) ? "NULL" : "" + Convert.ToInt32(testQueBankDTO.Hard) + "") + "," +
                                     " GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                     ");" +
                                     " SELECT @TestId";

                sqlCmd.ExecuteNonQuery();
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

    public void Delete(string TestId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..Tests(TestId, TestName, SubId, Remarks, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
            " Select TestId, TestName, SubId, Remarks, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId" +
            " FROM Tests WHERE TestId='" + TestId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM Tests WHERE TestId='" + TestId + "'";
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

    public DataTable LoadLevelOfQue()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = " Select distinct LevelOfQue from quebanks Order by LevelOfQue ";
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
    public bool NamesExists(string Name, string StandardId, string PatternId)
    {
        Name = Name.Replace("'", "''");

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " SELECT COUNT(*) FROM Tests a " +
                                 " Inner join Patterns p on p.PatternId = a.PatternId " +
                                 " WHERE a.PatternId = '" + PatternId.ToString() + "' and a.TestName = '" + Name.ToString() + "' " +
                                 " and p.StandardTextlistId = '" + StandardId.ToString() + "'";

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
