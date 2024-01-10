using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for ExamSummaryRptDAL
/// </summary>
public class ExamSummaryRptDAL
{
    private GeneralDAL _generalDAL;

    public ExamSummaryRptDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~ExamSummaryRptDAL()
    {
        _generalDAL = null;
    }
    public DataTable ExamSummaryDetail(DateTime? FromScheduleDt, DateTime? ToScheduleDt, string StandardId, string SubjectId, string TestId, bool AllRecords)
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
            where += " a.StandardId = '" + StandardId + "'";
        }
        if (SubjectId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " e.subId = '" + SubjectId + "'";
        }
        if (TestId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " f.TestId = '" + TestId + "'";
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
        if (where != "")
        {
            where = " WHERE " + where;
        }
        string TopStr = "";
        if (AllRecords == false)
        {
            TopStr = " TOP 10 ";
        }

        sqlCmd.CommandText = " Select " + TopStr + "  a.standardId,e.SubId,f.TestId,d.ExamScheduleId,b.[text] as Standard,e.Name as Subject,f.TestName," +
                             " convert(varchar(50),d.ExamFromTime,100) as Schedule, " +
                             " RegStu=Count(a.RegistrationId),convert(varchar(10),d.ExamDate,103) as ExamDate,AtteStu=Count(z.AttmStu),Count(x.PassCnt) as PassStu" +
                             ",Count(y.FailCnt) as FailStu ,MAX(y1.HighMarks) as HighMarks,AVG(y2.AvgMarks) as AvgMarks" +
                             " from Registration a" +
                             " left join Textlists b on b.textListId = a.standardId" +
                             " Inner Join ExamScheduleLns c on c.RegistrationId= a.RegistrationId" +
                             " inner join ExamSchedules d on d.ExamScheduleId = c.ExamScheduleId" +
                             " left join Subs e on e.SubId = d.SubId " +
                             " left join Tests f on f.TestId = d.TestId" +
                             " left join (select max(StartDt) as StartDt, max(EndDt) as EndDt, RegistrationId, ExamScheduleId " +
                             " from ExamStartStopTimes group by RegistrationId, ExamScheduleId) s on s.RegistrationId = a.RegistrationId" +
                             " and s.ExamScheduleId = d.ExamScheduleId   " +

                              //Attempt Student--
                             " Left join (Select e.RegistrationId,COUNT(e.RegistrationId) as AttmStu,e.ExamScheduleId from Exams e " +
                             " Group By e.RegistrationId,e.ExamScheduleId) z on z.RegistrationId= a.RegistrationId  and z.ExamScheduleId = d.ExamScheduleId " +
            //End

                             //Passed Student---
                             " Left Join" +
                             " (Select a.RegistrationId,SUM(Case when a.Ans = '~SKIPPED~' then b.NonMarks when a.Ans = b.Ans then b.RightMarks when a.Ans <> b.Ans then case when isnull(es.NegativeMarks, 0) = 1 then (b.WrongMarks) else 0 end end) as PassCnt ,a.ExamScheduleId   " +
                             " from Exams a" +
                             " Inner Join Ques b on b.QueId= a.QueId" +
                             " Inner Join Registration c on c.RegistrationId= a.RegistrationId" +
                             " INNER JOIN ExamSchedules es on es.ExamScheduleId = a.ExamScheduleId " +
                             " Group BY a.RegistrationId,a.ExamScheduleId" +
                             " Having (                 SUM(Case when a.Ans = '~SKIPPED~' then b.NonMarks when a.Ans = b.Ans then b.RightMarks when a.Ans <> b.Ans then case when isnull(es.NegativeMarks, 0) = 1 then (b.WrongMarks) else 0 end end)) >= (Select FacetText From Facets where FacetName='MinPassMarks')" +
                             " ) x on x.RegistrationId= a.RegistrationId  and x.ExamScheduleId = d.ExamScheduleId " +
            //End---

                             //Failed Student--
                             " Left Join" +
                             " (Select a.RegistrationId,SUM(Case when a.Ans = '~SKIPPED~' then b.NonMarks when a.Ans =b.Ans then b.RightMarks when a.Ans <> b.Ans then case when isnull(es.NegativeMarks, 0) = 1 then (b.WrongMarks) else 0 end end) as FailCnt,a.ExamScheduleId " +
                             " from Exams a" +
                             " Inner Join Ques b on b.QueId= a.QueId" +
                             " Inner Join Registration c on c.RegistrationId= a.RegistrationId" +
                             " INNER JOIN ExamSchedules es on es.ExamScheduleId = a.ExamScheduleId " +
                             " Group BY a.RegistrationId ,a.ExamScheduleId" +
                             " Having (SUM(Case when a.Ans = '~SKIPPED~' then b.NonMarks when a.Ans =b.Ans then b.RightMarks when a.Ans <> b.Ans then case when isnull(es.NegativeMarks, 0) = 1 then (b.WrongMarks) else 0 end end)) < (Select FacetText From Facets where FacetName='MinPassMarks')" +
                             " ) y on y.RegistrationId= a.RegistrationId  and y.ExamScheduleId = d.ExamScheduleId " +
            //End--

                             //Highest Marks--
                             " Left Join" +
                             " (Select a.RegistrationId,SUM(Case when a.Ans = '~SKIPPED~' then b.NonMarks when a.Ans = b.Ans then b.RightMarks when a.Ans <> b.Ans then case when isnull(es.NegativeMarks, 0) = 1 then (b.WrongMarks) else 0 end end) as HighMarks,a.ExamScheduleId " +
                             " from Exams a" +
                             " Inner Join Ques b on b.QueId= a.QueId" +
                             " Inner Join Registration c on c.RegistrationId= a.RegistrationId" +
                             " INNER JOIN ExamSchedules es on es.ExamScheduleId = a.ExamScheduleId " +
                             " Group BY a.RegistrationId,a.ExamScheduleId " +
                             " ) y1 on y1.RegistrationId= a.RegistrationId  and y1.ExamScheduleId = d.ExamScheduleId " +
            //End--

                             //Average Marks--
                             " Left Join" +
                             " (Select a.RegistrationId,SUM(Case when a.Ans = '~SKIPPED~' then b.NonMarks when a.Ans =b.Ans then b.RightMarks when a.Ans <> b.Ans then case when isnull(es.NegativeMarks, 0) = 1 then (b.WrongMarks) else 0 end end) as AvgMarks,a.ExamScheduleId " +
                             " from Exams a" +
                             " Inner Join Ques b on b.QueId= a.QueId" +
                             " Inner Join Registration c on c.RegistrationId= a.RegistrationId" +
                             " INNER JOIN ExamSchedules es on es.ExamScheduleId = a.ExamScheduleId " +
                             " Group BY a.RegistrationId,a.ExamScheduleId" +
                             " ) y2 on y2.RegistrationId= a.RegistrationId  and y2.ExamScheduleId = d.ExamScheduleId " +
            //End--

                             where +
                             " Group BY b.[text],e.Name,f.TestName,d.ExamDate,a.standardId,e.SubId,f.TestId,d.ExamScheduleId, d.ExamFromTime " +
                             " Order By d.ExamDate,d.ExamFromTime ";
        //}
        //else
        //{
        //    sqlCmd.CommandText = " select r.standardId,ess.SubId,ess.TestId,tst.[text] as Standard,su.Name as Subject,ts.TestName,RegStu=Count(r.RegistrationId)" +
        //                         ",ess.ExamDate,AtteStu=Count(e.RegistrationId),0 as PassStu,0 as FailStu,0 as HighMarks,0 as AvgMarks,tst.[text] as Standard,su.Name as Subject,ts.TestName" +
        //                         " from Registration r " +
        //                         " inner join ExamScheduleLns es on es.RegistrationId = r.RegistrationId " +
        //                         " inner join ExamSchedules ess on ess.ExamScheduleId = es.ExamScheduleId " +
        //                         " left join Exams e  on e.RegistrationId = r.RegistrationId and e.ExamScheduleId = ess.ExamScheduleId " +
        //                         " left join Ques q on q.QueId = e.QueId  " +
        //                         " left join Subs su on su.SubId = ess.SubId " +
        //                         " left join Tests ts on ts.TestId = ess.TestId " +
        //                         " left join Textlists tst on tst.textListId = r.standardId" +
        //                         where +
        //                         " group by tst.[text],su.Name,ts.TestName,ess.ExamDate,r.standardId,ess.SubId,ess.TestId " +
        //                         " order by ess.ExamDate desc ";
        //}

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }

    public DataTable ExamDetail(string StandardId, string SubjectId, string TestId, string ExamScheduleId)
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

        if (where != "")
        {
            where = " WHERE " + where;
        }
        sqlCmd.CommandText = " select r.RegistrationId, r.ExamNo, r.MobileNo,r.FirstName " +
                " ,SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case " +
                " when isnull(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) + ISNULL((Select SUM(ISNULL(Marks,0)) " +
                " from ExamMarks where RegistrationId = r.RegistrationId and ExamScheduleId = ess.ExamScheduleId),0) "+
                " as TotalMarks" +
                " ,SUM(Case when e.Ans <> '~SKIPPED~' and e.Ans = q.Ans then 1 else 0 end) as NoOfTrueAnswers " +
                " ,SUM(Case when e.Ans <> '~SKIPPED~' and e.Ans <> q.Ans then 1 else 0 end) as NoOfWrongAnswers " +
                " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions" +
                " ,tst.[text] as Standard,su.Name as Subject,ts.TestName " +
                " , convert(varchar(50),ess.ExamFromTime,100) as Schedule ,MinPassMarks=(Select FacetText from Facets where FacetName='MinPassMarks')" +
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
                " group by r.RegistrationId,r.ExamNo, r.MobileNo,r.FirstName,tst.[text],su.Name,ts.TestName,ess.ExamScheduleId,ess.ExamFromTime " +
                " order by TotalMarks desc  ";
        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }

    #region Load Dropdown Data
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
    #endregion
}