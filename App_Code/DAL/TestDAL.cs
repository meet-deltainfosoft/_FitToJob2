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

public class TestDAL
{
    public GeneralDAL _generalDAL;

    public TestDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~TestDAL()
    {
        _generalDAL = null;
    }

    public void Insert(TestDTO TestDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string TestId;
        string ExamScheduleId="";

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = "DECLARE  @TestId uniqueidentifier;" +
                                 " SET @TestId = NewId()" +
                                 "INSERT INTO Tests(TestId, TestName, SubId,StandardTextListId, Remarks, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " VALUES(@TestId," + ((TestDTO.TestName == null) ? "NULL" : "N'" + TestDTO.TestName.Replace("'", "''") + "'") + "," +
                                 ((TestDTO.SubId == null) ? "NULL" : "'" + TestDTO.SubId.ToString() + "'") + "," +
                                 ((TestDTO.StandardTextListId == null) ? "NULL" : "'" + TestDTO.StandardTextListId.ToString() + "'") + "," +
                                 "" + ((TestDTO.Remarks == null) ? "NULL" : "N'" + TestDTO.Remarks.Replace("'", "''") + "'") + ",GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                 ");" +
                                 " SELECT @TestId";
            TestId = sqlCmd.ExecuteScalar().ToString();

            if (TestId != null)
            {
                
                string TotalQuestions = null;
                string ExamDate = DateTime.Today.ToString("dd-MMM-yyyy");
                string ExamDate1 = DateTime.Today.ToString("dd-MMM-yyyy ");
                DateTime now = DateTime.Now;
                DateTime time = DateTime.Now;

                //string FromTimes = string.Format("{0}:{1}:{2} {3}", "24",

                //    "00", "00", Convert.ToInt16("12") > 12 ? "PM" : "AM");

                //string ToTimes = string.Format("{0}:{1}:{2} {3}", "11",

                //   "59", "59", Convert.ToInt16("11") > 12 ? "PM" : "AM");

                string ExamFromTime = ExamDate1 +"12:00 AM"; // + FromTimes
                string ExamToTime = ExamDate1 + "11:59 PM"; //+ ToTimes
                string TotalMins = "600";
                string PerQueMins = "600";
                string IsDefaultTest = "1";
                string NegativeMarks = "1";
                string AllowReview = "1";
                string PerQuestionTime = "1";
                string MinsforResultShow = "30";
                string ShowResult = "1";

                sqlCmd.CommandText = "DECLARE  @ExamScheduleId uniqueidentifier;" +
                                     " SET @ExamScheduleId = NewId()" +
                                     "INSERT INTO ExamSchedules(ExamScheduleId,StandardTextListId,SubId,TestId,TotalQuestions,ExamDate,ExamFromTime,ExamToTime, " +
                                     "TotalMins,PerQueMins,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId,IsDefaultTest,NegativeMarks,PerQuestionTime,AllowReview,MinsforResultShow,ShowResult)" +
                                     " VALUES(@ExamScheduleId," + ((TestDTO.StandardTextListId == null) ? "NULL" : "'" + TestDTO.StandardTextListId.ToString() + "'") + "," +
                                     ((TestDTO.SubId == null) ? "NULL" : "'" + TestDTO.SubId.ToString() + "'") + "," +
                                     " '" + TestId + "' ," +
                                     " '" + TotalQuestions + "' , " +
                                     " '" + ExamDate + "' ," +
                                     " '" + ExamFromTime + "' ," +
                                     " '" + ExamToTime + "' ," +
                                     " '" + TotalMins + "' ," +
                                     " '" + PerQueMins + "' ," +
                                     " GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL ," +
                                     "  '" + IsDefaultTest + "'," +
                                     " '" + NegativeMarks + "' ," +
                                     " '" + PerQuestionTime + "' ," +
                                     " '" + AllowReview + "' ," +
                                     " '" + MinsforResultShow + "' ," +
                                     " '" + ShowResult + "' " +
                                     ");" +
                                     " SELECT @ExamScheduleId";

                ExamScheduleId = sqlCmd.ExecuteScalar().ToString();
                // sqlCmd.ExecuteNonQuery();
            }

            // loop create for registation data get for same standard and add in examschedulelns
            if (ExamScheduleId != null)
            {
                DataTable dtRestrations = new DataTable();

                sqlCmd.CommandText = "select * from Registration where StandardId = '" + TestDTO.StandardTextListId.ToString() + "'";
                //sqlCmd.CommandText = " select * from RegistrationJobProfileLns R " +
                //                     " left Join Registration a on a.RegistrationId = R.RegistrationId " +
                //                     " Inner join Jobofferings J on J.JobOfferingId = R.JobOfferingId  " +
                //                     " Inner join Textlists t on t.TextListId = J.DepartmentId " +
                //                     " where J.DepartmentId = '" + TestDTO.StandardTextListId.ToString() + "'";

                dtRestrations.Load(sqlCmd.ExecuteReader());

                int j = 1;
                if (dtRestrations.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtRestrations.Rows.Count - 1; i++)
                    {
                        //string ExamScheduleLnId;
                        sqlCmd.CommandText = "DECLARE  @ExamScheduleLnId uniqueidentifier;" +
                                             " SET @ExamScheduleLnId = NewId()" +
                                             "INSERT INTO ExamScheduleLns(ExamScheduleLnId,ExamScheduleId,RegistrationId,LnNo,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId)" +
                                             " VALUES(@ExamScheduleLnId," +
                                             " '" + ExamScheduleId + "' ," +
                                             " '" + dtRestrations.Rows[i]["RegistrationId"] + "' ," +
                                             " '" + j + "'"+
                                             " ,GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL " +
                                             ");" +
                                             " SELECT @ExamScheduleLnId";
                        
                        sqlCmd.ExecuteNonQuery();
                        j++;
                    }
                   
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

    public TestDTO Select(string TestId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        TestDTO TestDTO = new TestDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "select a.*, s.StandardTextListId from Tests a inner join Subs s on s.SubId = a.SubId WHERE TestId='" + TestId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["TestId"] != DBNull.Value)
                TestDTO.TestId = sqlDr["TestId"].ToString();

            if (sqlDr["TestName"] != DBNull.Value)
                TestDTO.TestName = sqlDr["TestName"].ToString();

            if (sqlDr["SubId"] != DBNull.Value)
                TestDTO.SubId = sqlDr["SubId"].ToString();

            if (sqlDr["Remarks"] != DBNull.Value)
                TestDTO.Remarks = sqlDr["Remarks"].ToString();

            if (sqlDr["StandardTextListId"] != DBNull.Value)
                TestDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();
        }

        sqlDr.Close();

        _generalDAL.CloseSQLConnection();

        return TestDTO;
    }

    public void Update(TestDTO TestDTO)
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
            sqlCmd.CommandText = " UPDATE Tests SET" +
                                 " TestName = " + ((TestDTO.TestName == null) ? "NULL" : "N'" + TestDTO.TestName.Replace("'", "''") + "'") + "" +
                                 " ,Remarks = " + ((TestDTO.Remarks == null) ? "NULL" : "N'" + TestDTO.Remarks.Replace("'", "''") + "'") + "" +
                                 " ,LastUpdatedOn = GETDATE()" +
                                 " WHERE TestId = '" + TestDTO.TestId + "'";
            sqlCmd.ExecuteNonQuery();

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

    public bool NamesExists(string Name, string SubId)
    {
        //Escape single quote
        Name = Name.Replace("'", "''");
        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Tests WHERE [TestName] = '" + Name + "'and SubID = '" + SubId + "'";

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
    public bool NamesExists(string Name, string SubId, string TestId)
    {
        try
        {
            //Escape single quote
            Name = Name.Replace("'", "''");
            //Escape single quote

            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Tests WHERE TestName='" + Name + "' and SubId = '" + SubId + "' AND NOT TestId='" + TestId + "'";


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

    //public DataTable LoadSubjects()
    //{
    //    try
    //    {
    //        SqlCommand sqlCmd = new SqlCommand();
    //        DataTable dt = new DataTable();

    //        _generalDAL.OpenSQLConnection();

    //        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
    //        sqlCmd.CommandText = "select * From Subs order by Name";

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
}
