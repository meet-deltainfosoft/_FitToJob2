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

public class ExamMarksEntryDAL
{
    private GeneralDAL _generalDAL;

    public ExamMarksEntryDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~ExamMarksEntryDAL()
    {
        _generalDAL = null;
    }

    #region Load Drop Down
    public DataTable LoadSubject(string StandardId)
    {
        string sql, Str;
        DataTable dt = new DataTable();
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataSet ds = new DataSet();

            sql = " Select SubId,Name From Subs where StandardTextListId = '" + StandardId + "' order by Name Asc";

            sqlCmd.CommandText = sql;
            _generalDAL.OpenSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            dt.Load(sqlCmd.ExecuteReader());
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
        return dt;
    }
    public DataTable LoadTest(string SubjectId)
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            sqlCmd.CommandText = " select TestId,TestName from Tests where SubId = '" + SubjectId + "' Order by TestName Asc ";

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
    public DataTable LoadSchedule(string TestId)
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            sqlCmd.CommandText = " select ExamScheduleId, convert(varchar(50), ExamFromTime,100) as Schedule from ExamSchedules where TestId = '" + TestId + "' Order by ExamFromTime ";

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
    public DataTable LoadSchedule(string SubjectId, string TestId)
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            sqlCmd.CommandText = " select es.*, s.Name as 'SubName', t.TestName From ExamSchedules es " +
                                 " inner join Subs s on s.SubId = es.SubId " +
                                 " inner join Tests t on t.TestId = es.TestId " +
                                 "  where es.SubId = '" + SubjectId + "' and es.TestId = '" + TestId + "' order by es.ExamFromTime desc ";

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
    #endregion

    #region Update
    public void Update(ArrayList al)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlcmd = new SqlCommand();
        string[] MarksList;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlcmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlcmd.CommandType = CommandType.Text;
        sqlcmd.Transaction = sqlTrans;
        int cnt;
        try
        {
            foreach (string str in al)
            {
                MarksList = str.Split('|');

                string RegistrationId = MarksList[0];
                string ExamScheduleId = MarksList[1];
                string QueId = MarksList[2];
                decimal? Marks = Convert.ToDecimal(MarksList[3]);

                decimal? Marks1;
                decimal? Marks2;
                decimal? Marks3;
                decimal? Marks4;
                decimal? Marks5;
                decimal? Marks6;

                if (MarksList[4] != null && MarksList[4] != "")
                    Marks1 = Convert.ToDecimal(MarksList[4]);
                else
                    Marks1 = null;

                if (MarksList[5] != null && MarksList[5] != "")
                    Marks2 = Convert.ToDecimal(MarksList[5]);
                else
                    Marks2 = null;

                if (MarksList[6] != null && MarksList[6] != "")
                    Marks3 = Convert.ToDecimal(MarksList[6]);
                else
                    Marks3 = null;

                if (MarksList[7] != null && MarksList[7] != "")
                    Marks4 = Convert.ToDecimal(MarksList[7]);
                else
                    Marks4 = null;

                if (MarksList[8] != null && MarksList[8] != "")
                    Marks5 = Convert.ToDecimal(MarksList[8]);
                else
                    Marks5 = null;

                if (MarksList[9] != null && MarksList[9] != "")
                    Marks6 = Convert.ToDecimal(MarksList[9]);
                else
                    Marks6 = null;

                cnt = 0;
                sqlcmd.CommandText = "Select Count(*) as cnt from ExamMarks where RegistrationId = '" + RegistrationId + "' and ExamScheduleId = '" + ExamScheduleId + "'" +
                                    " and QueId = '" + QueId.ToString() + "'";
                cnt = Convert.ToInt16(sqlcmd.ExecuteScalar());
                if (cnt == 0)
                {
                    sqlcmd.CommandText = " INSERT INTO ExamMarks(ExamMarksId,RegistrationId,ExamScheduleId,QueId,Marks,Marks1,Marks2,Marks3,Marks4,Marks5,Marks6,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId)" +
                                     " VALUES(NewID()" +
                                     "," + ((RegistrationId == null) ? "NULL" : "'" + RegistrationId + "'") +
                                     "," + ((ExamScheduleId == null) ? "NULL" : "'" + ExamScheduleId + "'") +
                                     "," + ((QueId == null) ? "NULL" : "'" + QueId + "'") +
                                     "," + ((Marks == null) ? "NULL" : "" + Convert.ToDecimal(Marks) + "") +
                                     "," + ((Marks1 == null) ? "NULL" : "" + Convert.ToDecimal(Marks1) + "") +
                                     "," + ((Marks2 == null) ? "NULL" : "" + Convert.ToDecimal(Marks2) + "") +
                                     "," + ((Marks3 == null) ? "NULL" : "" + Convert.ToDecimal(Marks3) + "") +
                                     "," + ((Marks4 == null) ? "NULL" : "" + Convert.ToDecimal(Marks4) + "") +
                                     "," + ((Marks5 == null) ? "NULL" : "" + Convert.ToDecimal(Marks5) + "") +
                                     "," + ((Marks6 == null) ? "NULL" : "" + Convert.ToDecimal(Marks6) + "") +
                                     ", GETDATE(),GETDATE(),'" + MySession.UserUnique + "','" + MySession.UserUnique + "'" +
                                     ")";
                    sqlcmd.ExecuteNonQuery();
                }
                else
                {
                    sqlcmd.CommandText = " UPDATE ExamMarks SET " +
                                         " Marks = " + ((Marks == null) ? "NULL" : "" + Convert.ToDecimal(Marks) + "") + "" +
                                         ",Marks1 = " + ((Marks1 == null) ? "NULL" : "" + Convert.ToDecimal(Marks1) + "") + "" +
                                         ",Marks2 = " + ((Marks2 == null) ? "NULL" : "" + Convert.ToDecimal(Marks2) + "") + "" +
                                         ",Marks3 = " + ((Marks3 == null) ? "NULL" : "" + Convert.ToDecimal(Marks3) + "") + "" +
                                         ",Marks4 = " + ((Marks4 == null) ? "NULL" : "" + Convert.ToDecimal(Marks4) + "") + "" +
                                         ",Marks5 = " + ((Marks5 == null) ? "NULL" : "" + Convert.ToDecimal(Marks5) + "") + "" +
                                         ",Marks6 = " + ((Marks6 == null) ? "NULL" : "" + Convert.ToDecimal(Marks6) + "") + "" +
                                         ",LastUpdatedOn=GETDATE()" +
                                         ",LastUpdatedByUserId='" + MySession.UserUnique + "'" +
                                         " WHERE RegistrationId = '" + RegistrationId + "' and ExamScheduleId = '" + ExamScheduleId + "'" +
                                         " and QueId = '" + QueId.ToString() + "'";
                    sqlcmd.ExecuteNonQuery();
                }
            }
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
    #endregion

    #region Get Data
    public DataTable ResultDetail(DateTime? FromScheduleDt, DateTime? ToScheduleDt, string StandardId, string SubjectId, string TestId, string StudentName, string MobileNo, string ExamScheduleId, string Top)
    {
        try
        {
            if (StudentName != null)
                StudentName = StudentName.Replace("'", "''");

            if (MobileNo != null)
                MobileNo = MobileNo.Replace("'", "''");

            string where;
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();
            where = "";

            //Name
            if (StudentName != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " r.FirstName Like '%" + StudentName + "%'";
            }

            if (MobileNo != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " r.MobileNo Like '%" + MobileNo + "%'";
            }

            if (StandardId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " r.StandardId = '" + StandardId + "'";
            }

            if (SubjectId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " q.subId = '" + SubjectId + "'";
            }

            if (TestId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " q.TestId = '" + TestId + "'";
            }

            if (ExamScheduleId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " ess.ExamScheduleId = '" + ExamScheduleId + "'";
            }
            if (FromScheduleDt != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " Convert(date,s.StartDt) >= '" + Convert.ToDateTime(FromScheduleDt).ToString("dd-MMM-yyyy") + "'";
            }

            if (ToScheduleDt != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " Convert(date,s.StartDt) <= '" + Convert.ToDateTime(ToScheduleDt).ToString("dd-MMM-yyyy") + "'";
            }

            if (Top != null && Top != "")
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " r.RegistrationId not in (Select RegistrationId from ExamMarks )";
            }


            if (where != "")
            {
                where = " WHERE " + where;
            }
            if (ExamScheduleId != null)
            {

                sqlCmd.CommandText = " select r.RegistrationId,ess.ExamScheduleId, r.ExamNo, r.MobileNo,r.FirstName " +
                        " ,SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case when isnull" +
                        " (ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) + ISNULL((Select SUM(ISNULL(Marks,0)) " +
                        " from ExamMarks where RegistrationId = r.RegistrationId and ExamScheduleId = ess.ExamScheduleId),0) as TotalMarks " +
                        " ,SUM(Case when e.Ans <> '~SKIPPED~' and e.Ans = q.Ans then 1 else 0 end) as NoOfTrueAnswers " +
                        " ,SUM(Case when e.Ans <> '~SKIPPED~' and e.Ans <> q.Ans then 1 else 0 end) as NoOfWrongAnswers " +
                        " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions" +
                        " ,tst.[text] as Standard,su.Name as Subject,ts.TestName + ' Schd.-' +  convert(varchar(50), ess.ExamFromTime,100) as TestName " +
                        " from Registration r" +
                        " inner join ExamScheduleLns es on es.RegistrationId = r.RegistrationId" +
                        " inner join ExamSchedules ess on ess.ExamScheduleId = es.ExamScheduleId" +
                        " left join Exams e  on e.RegistrationId = r.RegistrationId and e.ExamScheduleId = ess.ExamScheduleId" +
                        " left join (select max(StartDt) as StartDt, max(EndDt) as EndDt, RegistrationId, ExamScheduleId " +
                        " from ExamStartStopTimes group by RegistrationId, ExamScheduleId) s on s.RegistrationId = r.RegistrationId" +
                        " and s.ExamScheduleId = ess.ExamScheduleId" +
                        " left join Ques q on q.QueId = e.QueId " +
                        " left join Subs su on su.SubId = ess.SubId" +
                        " left join Tests ts on ts.TestId = ess.TestId" +
                        " left join Textlists tst on tst.textListId = r.standardId " +
                        where +
                        " group by r.RegistrationId,ess.ExamScheduleId,r.ExamNo, r.MobileNo,r.FirstName,tst.[text],su.Name,ts.TestName, ess.ExamFromTime " +
                        " order by  TotalMarks desc  ";
            }
            else
            {

                sqlCmd.CommandText = " select " + Top + " r.RegistrationId ,ess.ExamScheduleId, r.ExamNo, r.MobileNo,r.FirstName " +
                        " ,SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case when isnull(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) + ISNULL((Select SUM(ISNULL(Marks,0)) " +
                        " from ExamMarks where RegistrationId = r.RegistrationId and ExamScheduleId = ess.ExamScheduleId),0) as TotalMarks " +
                        " ,SUM(Case when e.Ans <> '~SKIPPED~' and e.Ans = q.Ans then 1 else 0 end) as NoOfTrueAnswers " +
                        " ,SUM(Case when e.Ans <> '~SKIPPED~' and e.Ans <> q.Ans then 1 else 0 end) as NoOfWrongAnswers " +
                        " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions" +
                        " ,tst.[text] as Standard,su.Name as Subject,ts.TestName" +
                        " from Registration r" +
                        " inner join ExamScheduleLns es on es.RegistrationId = r.RegistrationId" +
                        " inner join ExamSchedules ess on ess.ExamScheduleId = es.ExamScheduleId" +
                        " left join Exams e  on e.RegistrationId = r.RegistrationId and e.ExamScheduleId = ess.ExamScheduleId" +
                        " left join (select max(StartDt) as StartDt, max(EndDt) as EndDt, RegistrationId, ExamScheduleId " +
                        " from ExamStartStopTimes group by RegistrationId, ExamScheduleId) s on s.RegistrationId = r.RegistrationId" +
                        " and s.ExamScheduleId = ess.ExamScheduleId" +
                        " left join Ques q on q.QueId = e.QueId " +
                        " left join Subs su on su.SubId = ess.SubId" +
                        " left join Tests ts on ts.TestId = ess.TestId" +
                        " left join Textlists tst on tst.textListId = r.standardId " +
                        where +
                        " group by r.RegistrationId,ess.ExamScheduleId,r.ExamNo, r.MobileNo,r.FirstName,tst.[text],su.Name,ts.TestName " +
                        " order by  TotalMarks desc  ";

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
    public DataTable GetResultFinal(string RegistrationId, string ExamScheduleId)
    {
        try
        {
            string where;
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();
            where = "";

            if (RegistrationId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " e.RegistrationId = '" + RegistrationId + "'";
            }

            if (ExamScheduleId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " e.ExamScheduleId = '" + ExamScheduleId + "'";
            }
            if (where != "")
            {
                where = " WHERE " + where;
            }

            //sqlCmd.CommandText = " select e.RegistrationId,r.MobileNo, t.[Text] as 'College Name',r.FirstName " +
            //                 " ,SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case when isnull(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) as TotalMarks " +
            //                 " ,SUM(Case when e.Ans <> '~SKIPPED~' and e.Ans = q.Ans then 1 else 0 end) as NoOfTrueAnswers " +
            //                 " ,SUM(Case when e.Ans <> '~SKIPPED~' and e.Ans <> q.Ans then 1 else 0 end) as NoOfWrongAnswers " +
            //                 " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions" +
            //                 " ,tst.[text] as Standard,su.Name as Subject,ts.TestName,r.IdProofImageName,r.CollegeIdProofImageName," +
            //                 " (select Count(*) From Ques where SubId = q.SubId and TestId = q.TestId) as NoOFQuestions ,r.standardId,su.SubId" +
            //                 " from Exams e " +
            //                 " inner join (select max(StartDt) as StartDt, max(EndDt) as EndDt, RegistrationId, ExamScheduleId " +
            //                 " from ExamStartStopTimes group by RegistrationId, ExamScheduleId) s on s.RegistrationId = e.RegistrationId" +
            //                 " and s.ExamScheduleId = e.ExamScheduleId " +
            //                 " inner join ExamSchedules ess on ess.ExamScheduleId = e.ExamScheduleId" +
            //                 " inner join Ques q on q.QueId = e.QueId " +
            //                 " inner join Registration r on r.RegistrationId = e.RegistrationId " +
            //                 " inner join Subs su on su.SubId = q.SubId" +
            //                 " inner join Tests ts on ts.TestId = q.TestId" +
            //                 " inner join Textlists tst on tst.textListId = r.standardId " +
            //                 " left join Textlists t on t.textListId = r.CollegeId " +
            //                 " " + where + "   " +
            //                 " group by e.RegistrationId,r.FirstName,r.MobileNo,q.SubId,q.TestId,t.[Text],tst.[text],su.Name,ts.TestName,r.IdProofImageName,r.CollegeIdProofImageName, r.standardId,su.SubId";

            sqlCmd.CommandText = " Select e.RegistrationId,r.standardId,su.SubId,r.FirstName,r.MobileNo,t.[Text] as 'College Name', " +
                                  " tst.[text] as Standard,su.Name as " +
                                  " Subject,ts.TestName " +
                                  " from Exams e " +
                                  " inner join ExamSchedules ess on ess.ExamScheduleId = e.ExamScheduleId " +
                                  " inner join Ques q on q.QueId = e.QueId " +
                                  " inner join Registration r on r.RegistrationId = e.RegistrationId " +
                                  " inner join Subs su on su.SubId = q.SubId " +
                                  " inner join Tests ts on ts.TestId = q.TestId " +
                                  " inner join Textlists tst on tst.textListId = r.standardId " +
                                  " left join Textlists t on t.textListId = r.CollegeId " +
                                  where +
                                  " group by e.RegistrationId,r.standardId,r.FirstName,r.MobileNo,su.SubId,q.TestId,t.[Text], " +
                                  " tst.[text],su.Name,ts.TestName ";
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
    public DataTable GetExamDetails(string RegistrationId, string ExamScheduleId)
    {
        try
        {
            string where;
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();
            where = "";

            if (RegistrationId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " e.RegistrationId = '" + RegistrationId + "'";
            }

            if (ExamScheduleId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " e.ExamScheduleId = '" + ExamScheduleId + "'";
            }
            if (where != "")
            {
                where = " WHERE " + where;
            }

            sqlCmd.CommandText = " select distinct r.FirstName,q.Que,case when Q.Ans = '1' then q.A1 when Q.Ans = '2' then q.A2 when Q.Ans = '3' then q.A3 " +
                " when Q.Ans ='4' then q.A4 else Q.Ans end as OriginalAns," +
                " case when q.QueType = 'MCQ' then " +
                " case when e.Ans = '1' then q.A1 when e.Ans = '2' then q.A2 when e.Ans = '3' then q.A3 " +
                " when e.Ans ='4' then q.A4 when e.Ans ='~SKIPPED~' then 'Skipped' END " +
                " when q.QueType = 'NONMCQ' then " +
                " e.Ans end as FilledAns " +
                " ,replace(q.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                "  ,case when Q.Ans = '1' then replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when Q.Ans = '2' then replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when Q.Ans = '3' then replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when Q.Ans ='4' then replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " end as OriginalAnsImage " +
                " ,case when e.Ans = '1' then replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when e.Ans = '2' then replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "')  " +
                " when e.Ans = '3' then replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "')  " +
                " when e.Ans ='4' then replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "')  " +
                " when e.Ans ='~SKIPPED~' then 'Skipped' end as FilledAnsImage,q.RightMarks,q.WrongMarks,q.NonMarks,r.RegistrationId,e.ExamScheduleId,q.QueId,q.QueType " +
                " from Registration r " +
                " left join Exams e on r.RegistrationId = e.RegistrationId " +
                " inner join Ques q on q.QueId = e.QueId" + where + " order by q.Que ";
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

    //public DataTable GetResultFinalMasterSheet(string TestId)
    //{
    //    try
    //    {
    //        string where, where2;
    //        SqlCommand sqlCmd = new SqlCommand();
    //        DataTable dt = new DataTable();
    //        where = "";
    //        where2 = "";

    //        if (TestId != null)
    //        {
    //            if (where != "")
    //            {
    //                where += " AND ";
    //                where2 += " AND ";
    //            }
    //            where += " e.TestId = '" + TestId + "'";
    //            where2 += " qq.TestId = '" + TestId + "'";
    //        }


    //        if (where != "")
    //        {
    //            where = " WHERE " + where;
    //            where2 = " WHERE " + where2;
    //        }




    //        sqlCmd.CommandText = "  select e.RegistrationId,'9090909090' MobileNo, '' as 'College Name','demo' FirstName " +
    //                        " ,SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case when isnull(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) as TotalMarks " +
    //                        " ,SUM(Case when e.Ans <> '~SKIPPED~' and e.Ans = q.Ans then 1 else 0 end) as NoOfTrueAnswers " +
    //                        " ,SUM(Case when e.Ans <> '~SKIPPED~' and e.Ans <> q.Ans then 1 else 0 end) as NoOfWrongAnswers " +
    //                        " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions  " +
    //                        " ,tst.[text] as Standard,su.Name as Subject,ts.TestName, " +
    //                        " (select Count(*) From Ques where SubId = q.SubId and TestId = q.TestId) as NoOFQuestions " +
    //                        " from (select TestId as ExamId,TestId as RegistrationId,qq.SubId,qq.QueId,qq.Ans,getdate() as StartDt,getdate() as EndDt,TestId as ExamScheduleId,qq.TestID from Ques qq " +
    //                        " " + where2 + "  ) as e    " +
    //                        " inner join ExamSchedules ess on ess.ExamScheduleId = e.ExamScheduleId" +
    //                        " inner join Ques q on q.QueId = e.QueId " +
    //                        " inner join Subs su on su.SubId = q.SubId " +
    //                        " inner join Tests ts on ts.TestId = q.TestId " +
    //                        " inner join Textlists tst on tst.textListId = su.StandardTextListId " +
    //                         " " + where + "   " +
    //                         " group by e.RegistrationId,q.SubId,q.TestId,tst.[text],su.Name,ts.TestName ";

    //        _generalDAL.OpenSQLConnection();
    //        sqlCmd.CommandType = CommandType.Text;
    //        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
    //        dt.Load(sqlCmd.ExecuteReader());
    //        _generalDAL.CloseSQLConnection();
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        _generalDAL.CloseSQLConnection();
    //        throw new Exception(ex.Message);
    //    }
    //}
    //public DataTable GetExamDetailsMaster(string TestId)
    //{
    //    try
    //    {
    //        string where;
    //        SqlCommand sqlCmd = new SqlCommand();
    //        DataTable dt = new DataTable();
    //        where = "";

    //        if (TestId != null)
    //        {
    //            if (where != "")
    //            {
    //                where += " AND ";
    //            }
    //            where += " q.TestId = '" + TestId + "'";
    //        }


    //        if (where != "")
    //        {
    //            where = " WHERE " + where;
    //        }

    //        sqlCmd.CommandText = " select	  q.Que,case when Q.Ans = '1' then q.A1 when Q.Ans = '2' then q.A2 when Q.Ans = '3' then q.A3 " +
    //            " when Q.Ans = '4' then q.A4 end as OriginalAns " +
    //            " ,q.A1 as FilledAns1 " +
    //            " ,q.A2 as FilledAns2 " +
    //            " ,q.A3 as FilledAns3 " +
    //            " ,q.A4 as FilledAns4 " +
    //            " , replace(q.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
    //            "  ,case when Q.Ans = '1' then replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
    //            " when Q.Ans = '2' then replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
    //            " when Q.Ans = '3' then replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
    //            " when Q.Ans = '4' then replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
    //            " end as OriginalAnsImage " +
    //            " ,replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as FilledAnsImage1 " +
    //            " ,replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as FilledAnsImage2 " +
    //            " ,replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as FilledAnsImage3 " +
    //            " ,replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as FilledAnsImage4 " +
    //            " , q.SrNo ,q.Ans " +
    //            " from Ques q  " + where + "   order by q.SrNo ";
    //        _generalDAL.OpenSQLConnection();
    //        sqlCmd.CommandType = CommandType.Text;
    //        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
    //        dt.Load(sqlCmd.ExecuteReader());
    //        _generalDAL.CloseSQLConnection();
    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        _generalDAL.CloseSQLConnection();
    //        throw new Exception(ex.Message);
    //    }
    //}
    //public DataTable LiveResultDetail(string StandardId, string SubjectId, string TestId, string StudentName, string MobileNo, string ExamScheduleId, string ResultType)
    //{
    //    try
    //    {
    //        if (StudentName != null)
    //            StudentName = StudentName.Replace("'", "''");

    //        if (MobileNo != null)
    //            MobileNo = MobileNo.Replace("'", "''");

    //        string where;
    //        SqlCommand sqlCmd = new SqlCommand();
    //        DataTable dt = new DataTable();
    //        where = "";

    //        //Name


    //        //Name
    //        if (StudentName != null)
    //        {
    //            if (where != "")
    //            {
    //                where += " AND ";
    //            }
    //            where += " r.FirstName Like '%" + StudentName + "%'";
    //        }

    //        if (MobileNo != null)
    //        {
    //            if (where != "")
    //            {
    //                where += " AND ";
    //            }
    //            where += " r.MobileNo Like '%" + MobileNo + "%'";
    //        }

    //        if (StandardId != null)
    //        {
    //            if (where != "")
    //            {
    //                where += " AND ";
    //            }
    //            where += " e.StandardTextListId = '" + StandardId + "'";
    //        }

    //        if (SubjectId != null)
    //        {
    //            if (where != "")
    //            {
    //                where += " AND ";
    //            }
    //            where += " e.SubId = '" + SubjectId + "'";
    //        }

    //        if (TestId != null)
    //        {
    //            if (where != "")
    //            {
    //                where += " AND ";
    //            }
    //            where += " e.TestId = '" + TestId + "'";
    //        }

    //        if (ExamScheduleId != null)
    //        {
    //            if (where != "")
    //            {
    //                where += " AND ";
    //            }
    //            where += " e.ExamScheduleId = '" + ExamScheduleId + "'";
    //        }

    //        if (where != "")
    //        {
    //            where = " WHERE " + where;
    //        }

    //        string sql = "";
    //        sql = " select * from ( select e.*, t.[Text] as 'Standard', s.Name as 'Subject', ts.TestName, u.FirstName + ' ' + u.LastName as 'GeneratedBy' " +
    //                                 " , case  when getdate() > e.ExamToTime THEN 9 when getdate() > e.ExamFromTime and getdate() < e.ExamToTime THEN 1 else 2 end as 'ExamRunning' " +
    //                                 " , (select count(*) from ExamStartStopTimes x where x.ExamScheduleId = e.ExamScheduleId " +
    //                                 " and x.RegistrationId = el.RegistrationId and StartDt is not null and EndDt is not null) as 'IsCompleted' " +
    //                                 " , datediff(minute, getdate(), e.ExamFromTime) as 'DiffMins' " +
    //                                 " , (select count(*) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId) as 'DoneQuestions' " +
    //                                 " , (select min(InsertedOn) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId) as 'ExamStartTime' " +
    //                                 " , (select max(InsertedOn) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId) as 'ExamStopTime' " +
    //                                 " , r.FirstName, r.MobileNo " +
    //                                 " , e.TotalQuestions - (select count(*) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId) as 'PendingQuestion'  " +
    //                                 " From ExamSchedules e " +
    //                                 " inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
    //                                 " inner join TextLists t on t.TextListId = e.StandardTextListId " +
    //                                 " inner join Subs s on s.SubId = e.SubId " +
    //                                 " inner join Tests ts on ts.TestId = e.TestId " +
    //                                 " inner join Users u on u.UserId = e.InsertedByUserId " +
    //                                 " inner join Registration r on r.RegistrationId = el.RegistrationId " +
    //                                 where +
    //                                 "  ) as zz   ";
    //        switch (ResultType)
    //        {
    //            case "Present":
    //                sql += " where zz.DoneQuestions <> 0 ";
    //                break;
    //            case "Absent":
    //                sql += " where zz.DoneQuestions = 0 ";
    //                break;
    //            default:
    //                // code block
    //                break;
    //        }

    //        sql += " order by FirstName  ";

    //        sqlCmd.CommandText = sql;
    //        _generalDAL.OpenSQLConnection();
    //        sqlCmd.CommandType = CommandType.Text;
    //        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
    //        dt.Load(sqlCmd.ExecuteReader());

    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        _generalDAL.CloseSQLConnection();
    //        throw new Exception(ex.Message);
    //    }
    //}
    #endregion

    public DataTable LoadQuestions(string ExamScheduleId, string UserId)
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();

            sqlCmd.CommandText = " select UserName from Users where UserId = '" + UserId.ToString() + "' ";
            string UserName = sqlCmd.ExecuteScalar().ToString();

            if (UserName.ToString().ToUpper() == "aaa".ToString().ToUpper())
            {
                sqlCmd.CommandText = " select q.* from Ques q " +
                                        " inner join ExamSchedules e on e.TestId = q.TestId " +
                                        " inner join ExamCheckAllotments ea on ea.ExamScheduleId = e.ExamScheduleId " +
                                        " inner join ExamCheckAllotmentLns eal on eal.ExamCheckAllotmentId = ea.ExamCheckAllotmentId and q.QueId = eal.QueId " +
                                        " where e.ExamScheduleId = '" + ExamScheduleId.ToString() + "'  " +
                                        " order by q.SrNo   ";
            }
            else
            {
                sqlCmd.CommandText = " select q.* from Ques q " +
                                     " inner join ExamSchedules e on e.TestId = q.TestId " +
                                     " inner join ExamCheckAllotments ea on ea.ExamScheduleId = e.ExamScheduleId " +
                                     " inner join ExamCheckAllotmentLns eal on eal.ExamCheckAllotmentId = ea.ExamCheckAllotmentId and q.QueId = eal.QueId " +
                                     " where e.ExamScheduleId = '" + ExamScheduleId.ToString() + "' and eal.UserId = '" + UserId.ToString() + "' " +
                                     " order by q.SrNo   ";
            }
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

    public DataTable LoadEmployee(string ExamScheduleId)
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            sqlCmd.CommandText = " select u.UserId, u.FirstName + ' ' + u.LastName, u.UserName from Users u " +
                                 " inner join ExamCheckAllotments ea on ea.ExamScheduleId = '" + ExamScheduleId.ToString() + "' " +
                                 " inner join ExamCheckAllotmentLns eal on eal.ExamCheckAllotmentId = ea.ExamCheckAllotmentId and u.UserId = eal.UserId " +
                                 " where isnull(u.IsDisabled, 0) = 0 " +
                                 ((MySession.UserID.ToString().ToUpper() == "aaa".ToString().ToUpper()) ? " union all select u.UserId, " +
                                 " u.FirstName + ' ' + u.LastName, u.UserName from Users u where UserName = 'aaa' " : "") +
                                 " order by u.FirstName + ' ' + u.LastName ";

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

    public DataTable LoadStudentWiseQuestionAns(string StandardId, string SubjectId, string TestId, string ExamScheduleId, string UserId, string QueId)
    {
        try
        {
            string where;
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();
            where = "";

            if (StandardId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " es.StandardTextListId = '" + StandardId + "'";
            }

            if (SubjectId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " es.SubId = '" + SubjectId + "'";
            }

            if (TestId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " es.TestId = '" + TestId + "'";
            }

            if (ExamScheduleId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " es.ExamScheduleId = '" + ExamScheduleId + "'";
            }

            if (QueId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " q.QueId = '" + QueId + "'";
            }

            if (where != "")
            {
                where = " WHERE " + where;
            }


            sqlCmd.CommandText = " select 'Student ' + convert(varchar, row_number()over(order by r.FirstName)) + '' + " + (("aaa".ToString().ToUpper() == "aaa".ToString().ToUpper()) ? " + '_' + r.FirstName " : "''") + " as 'StudentName', q.QueId, e.ExamId, r.RegistrationId, r.FirstName " +
                                 " , r.MobileNo, es.ExamScheduleId, 'Question No : ' + convert(varchar, q.SrNo) as 'QuestionNo' " +
                                 " , RightMarks as TotalMarks  " +
                                 " , (select sum(TotalObtMark) from ExamEvaluations x where x.ExamId = e.ExamId) as 'ObtainedMarks' " +
                                 " , st.GRNo, converT(int, st.RollNo) as RollNo, st.[Standard], st.[Division] " +
                                 " from ExamSchedules es " +
                                 " inner join ExamScheduleLns els on els.ExamScheduleId = es.ExamScheduleId " +
                                 " inner join Registration r on r.Registrationid = els.RegistrationId " +
                                 " inner join Exams e on e.RegistrationId = r.RegistrationId and es.ExamScheduleId = e.ExamScheduleId " +
                                 " inner join Ques q on q.QueId = e.QueId " +
                                 " left join " + ConfigurationManager.AppSettings["ERPDBName"].ToString() + "..vwAllStudentDetails st on st.StudentId = r.Registrationid " +
                                 where +
                                 " order by st.[Division], converT(int, st.RollNo) ";

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
}
