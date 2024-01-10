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

public class ResultDetailDAL
{
    private GeneralDAL _generalDAL;

    public ResultDetailDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~ResultDetailDAL()
    {
        _generalDAL = null;
    }
    public DataTable ResultDetail(DateTime? FromScheduleDt, DateTime? ToScheduleDt, string StandardId, string SubjectId, string TestId, string StudentName, string MobileNo, string ExamScheduleId)
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

        if (where != "")
        {
            where = " WHERE " + where;
        }
        if (ExamScheduleId != null)
        {

            sqlCmd.CommandText = " select r.RegistrationId,ess.ExamScheduleId, r.ExamNo, r.MobileNo,r.FirstName " +
                    " ,SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case when isnull(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) as TotalMarks " +
                    " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].[GetUniqueAnsInNumber](e.Ans) = q.Ans then 1 else 0 end) as NoOfTrueAnswers " +
                    " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].[GetUniqueAnsInNumber](e.Ans) <> q.Ans then 1 else 0 end) as NoOfWrongAnswers " +
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

            sqlCmd.CommandText = " select r.RegistrationId ,ess.ExamScheduleId, r.ExamNo, r.MobileNo,r.FirstName " +
                    " ,SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case when isnull(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) as TotalMarks " +
                    " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].[GetUniqueAnsInNumber](e.Ans) = q.Ans then 1 else 0 end) as NoOfTrueAnswers " +
                    " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].[GetUniqueAnsInNumber](e.Ans) <> q.Ans then 1 else 0 end) as NoOfWrongAnswers " +
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

        return dt;
    }

    public DataTable ResultDetailAdv(DateTime? FromScheduleDt, DateTime? ToScheduleDt, string StandardId, string SubjectId, string TestId, string StudentName, string MobileNo, string ExamScheduleId, bool isExcel)
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

        if (where != "")
        {
            where = " WHERE " + where;
        }
        if (ExamScheduleId != null)
        {
            sqlCmd.CommandText = "";
            if (isExcel)
            {
                sqlCmd.CommandText = "Select Standard,Subject,TestName ,ExamNo, MobileNo,FirstName ,NoOFQuestions,NoOfUnAttemptedQuestions,TotalMarks from (";
            }
            sqlCmd.CommandText += " select r.RegistrationId,ess.ExamScheduleId, r.ExamNo, r.MobileNo,r.FirstName " +
                //" ,SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case when isnull(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) as TotalMarks " +
                     " , SUM(q.RightMarks) as RightMarks " +
                     " ,SUM( case when q.QueType in ('FILE', 'PDF') then eex.TotalObtMark else ( " +
                                "  Case when e.Ans = '~SKIPPED~' then   " +
                                "          q.NonMarks   		 " +
                                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 1  then " + //"      when e.Ans = q.Ans  then " +
                                "          case when q.AnsSelection = 'SINGLE' then " +
                                "              q.RightMarks " +
                                "          when q.AnsSelection = 'MULTIPLE' then " +
                                "              q.RightMarks " +
                                "          else  " +
                                "              q.RightMarks " +
                                "          end " +
                                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 0  and  q.AnsSelection = 'MULTIPLE' then  " +
                                "           case when [dbo].[GetTrueCount](q.Ans,e.Ans)  = q.RightMarks    then q.RightMarks else case when [dbo].[GetFalseCount](q.Ans,e.Ans) > 0 then [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks) else [dbo].[GetTrueCount](q.Ans,e.Ans) * 1 end end    " +
                                "      when e.Ans <> q.Ans then " +
                                "          case when q.AnsSelection = 'SINGLE' then " +
                                "              case when isnull(ess.NegativeMarks, 0) = 1 then " +
                                "                  (q.WrongMarks) " +
                                "              else " +
                                "                  0  " +
                                "              end " +
                                "          when q.AnsSelection = 'MULTIPLE' then " +
                                "              [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks)    " +
                                "          Else  				   " +
                                "              (q.WrongMarks) " +
                                "          End  		 " +
                                "      end) end )    as TotalMarks   " +
                    " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].[GetUniqueAnsInNumber](e.Ans) = q.Ans then 1 else 0 end) as NoOfTrueAnswers " +
                    " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].[GetUniqueAnsInNumber](e.Ans) <> q.Ans then 1 else 0 end) as NoOfWrongAnswers " +
                    " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions" +
                    " ,(select Count(*) From Ques where SubId = q.SubId and TestId = q.TestId) as NoOFQuestions" +
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
                    " left join (select sum(TotalObtMark) as TotalObtMark, ExamId from ExamEvaluations group by ExamId) eex on eex.ExamId = e.ExamId " +
                    where +
                    " group by q.Subid,q.TestId,r.RegistrationId,ess.ExamScheduleId,r.ExamNo, r.MobileNo,r.FirstName,tst.[text],su.Name,ts.TestName, ess.ExamFromTime ";// +
            //" order by  TotalMarks desc  ";
            if (isExcel)
            {
                sqlCmd.CommandText += " ) as XX order by TestName,TotalMarks Desc";
            }
            else
            {
                sqlCmd.CommandText += " Order by TotalMarks desc ";
            }

        }
        else
        {

            sqlCmd.CommandText = "";
            if (isExcel)
            {
                sqlCmd.CommandText = "Select Standard,Subject,TestName ,ExamNo, MobileNo,FirstName ,NoOFQuestions,NoOfUnAttemptedQuestions,TotalMarks from (";
            }
            sqlCmd.CommandText += " select r.RegistrationId ,ess.ExamScheduleId, r.ExamNo, r.MobileNo,r.FirstName " +
                //" ,SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case when isnull(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) as TotalMarks " +
                    " , SUM(q.RightMarks) as RightMarks " +
                     " ,SUM( case when q.QueType in ('FILE', 'PDF') then eex.TotalObtMark else ( " +
                                "  Case when e.Ans = '~SKIPPED~' then   " +
                                "          q.NonMarks   		 " +
                                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 1  then " + //"      when e.Ans = q.Ans  then " +
                                "          case when q.AnsSelection = 'SINGLE' then " +
                                "              q.RightMarks " +
                                "          when q.AnsSelection = 'MULTIPLE' then " +
                                "              q.RightMarks " +
                                "          else  " +
                                "              q.RightMarks " +
                                "          end " +
                                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 0  and  q.AnsSelection = 'MULTIPLE' then  " +
                                "           case when [dbo].[GetTrueCount](q.Ans,e.Ans)  = q.RightMarks    then q.RightMarks else case when [dbo].[GetFalseCount](q.Ans,e.Ans) > 0 then [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks) else [dbo].[GetTrueCount](q.Ans,e.Ans) * 1 end end    " +
                                "      when e.Ans <> q.Ans then " +
                                "          case when q.AnsSelection = 'SINGLE' then " +
                                "              case when isnull(ess.NegativeMarks, 0) = 1 then " +
                                "                  (q.WrongMarks) " +
                                "              else " +
                                "                  0  " +
                                "              end " +
                                "          when q.AnsSelection = 'MULTIPLE' then " +
                                "              [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks)    " +
                                "          Else  				   " +
                                "              (q.WrongMarks) " +
                                "          End  		 " +
                                "      end) end )    as TotalMarks   " +
                    " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].[GetUniqueAnsInNumber](e.Ans) = q.Ans then 1 else 0 end) as NoOfTrueAnswers " +
                    " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].[GetUniqueAnsInNumber](e.Ans) <> q.Ans then 1 else 0 end) as NoOfWrongAnswers " +
                    " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions" +
                    " ,(select Count(*) From Ques where SubId = q.SubId and TestId = q.TestId) as NoOFQuestions" +
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
                    " left join (select sum(TotalObtMark) as TotalObtMark, ExamId from ExamEvaluations group by ExamId) eex on eex.ExamId = e.ExamId " +
                    where +
                    " group by q.Subid,q.TestId,r.RegistrationId,ess.ExamScheduleId,r.ExamNo, r.MobileNo,r.FirstName,tst.[text],su.Name,ts.TestName " +
                    "  ";
            if (isExcel)
            {
                sqlCmd.CommandText += " ) as XX order by TestName,TotalMarks Desc";
            }
            else
            {
                sqlCmd.CommandText += " Order by TotalMarks desc ";
            }

        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }

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
            //" ,SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case when isnull(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) as TotalMarks " +
            //" SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then  " +
            //                            " case when q.AnsSelection = 'SINGLE' then  " +
            //                            " q.RightMarks " +
            //                            " when q.AnsSelection = 'MULTIPLE' then  " +
            //                            "	[dbo].[GetTrueCount](q.Ans,e.Ans) - [dbo].[GetFalseCount](q.Ans,e.Ans) " +
            //                            " end " +
            //                            " when e.Ans <> q.Ans then case when isnull(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) as TotalMarks " +


            sqlCmd.CommandText = " select e.RegistrationId,r.MobileNo, r.SchoolName as 'College Name',r.FirstName ," +
                                 " SUM( " +
                                "  Case when e.Ans = '~SKIPPED~' then   " +
                                "          q.NonMarks   		 " +
                                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 1  then " + //"      when e.Ans = q.Ans  then " +
                                "          case when q.AnsSelection = 'SINGLE' then " +
                                "              q.RightMarks " +
                                "          when q.AnsSelection = 'MULTIPLE' then " +
                                "              [dbo].[GetTrueCount](q.Ans,e.Ans)  * q.RightMarks " +
                                "          else  " +
                                "              q.RightMarks " +
                                "          end " +
                                "      when e.Ans <> q.Ans then " +
                                "          case when q.AnsSelection = 'SINGLE' then " +
                                "              case when isnull(ess.NegativeMarks, 0) = 1 then " +
                                "                  (q.WrongMarks) " +
                                "              else " +
                                "                  0  " +
                                "              end " +
                                "          when q.AnsSelection = 'MULTIPLE' then " +
                                "              [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks)    " +
                                "          Else  				   " +
                                "              (q.WrongMarks) " +
                                "          End  		 " +
                                "      end )    as TotalMarks   " +
                             " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,[dbo].[GetUniqueAnsInNumber](e.Ans) )= 1 then 1 else 0 end) as NoOfTrueAnswers " +
                             " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,[dbo].[GetUniqueAnsInNumber](e.Ans) )= 0 then 1 else 0 end) as NoOfWrongAnswers " +
                             " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions" +
                             " ,tst.[text] as Standard,su.Name as Subject,ts.TestName,r.IdProofImageName,r.CollegeIdProofImageName," +
                             " (select Count(*) From Ques where SubId = q.SubId and TestId = q.TestId) as NoOFQuestions" +
                             " ,CASE WHEN " +
                             " ISNULL(ess.ShowResult,0) = 0 THEN -1 " +
                             " ELSE " +
                             " datediff(mi,dateadd(mi,ISNULL(ess.MinsforResultShow,0),ess.ExamToTime),getdATE()) END iSShowResult " +
                             " from Exams e " +
                             " inner join (select max(StartDt) as StartDt, max(EndDt) as EndDt, RegistrationId, ExamScheduleId " +
                             " from ExamStartStopTimes group by RegistrationId, ExamScheduleId) s on s.RegistrationId = e.RegistrationId" +
                             " and s.ExamScheduleId = e.ExamScheduleId " +
                             " inner join ExamSchedules ess on ess.ExamScheduleId = e.ExamScheduleId" +
                             " inner join Ques q on q.QueId = e.QueId " +
                             " inner join Registration r on r.RegistrationId = e.RegistrationId " +
                             " inner join Subs su on su.SubId = q.SubId" +
                             " inner join Tests ts on ts.TestId = q.TestId" +
                             " inner join Textlists tst on tst.textListId = r.standardId " +
                             "   " +
                             " " + where + "   " +
                             " group by e.RegistrationId,r.FirstName,r.MobileNo,q.SubId,q.TestId,r.SchoolName,tst.[text],su.Name,ts.TestName,r.IdProofImageName,r.CollegeIdProofImageName" +
                             " ,ess.ShowResult,ISNULL(ess.MinsforResultShow,0),ess.ExamToTime ";

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

    public DataTable GetResultFinalAdv(string RegistrationId, string ExamScheduleId)
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
            //" ,SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case when isnull(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) as TotalMarks " +
            //" SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then  " +
            //                            " case when q.AnsSelection = 'SINGLE' then  " +
            //                            " q.RightMarks " +
            //                            " when q.AnsSelection = 'MULTIPLE' then  " +
            //                            "	[dbo].[GetTrueCount](q.Ans,e.Ans) - [dbo].[GetFalseCount](q.Ans,e.Ans) " +
            //                            " end " +
            //                            " when e.Ans <> q.Ans then case when isnull(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) as TotalMarks " +


            sqlCmd.CommandText = " select e.RegistrationId,r.MobileNo, r.SchoolName as 'College Name',r.FirstName ," +
                                  " SUM( " +
                                " case when q.QueType in ('FILE', 'PDF') then eex.TotalObtMark else ( Case when e.Ans = '~SKIPPED~' then   " +
                                "          q.NonMarks   		 " +
                                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 1  then " + //"      when e.Ans = q.Ans  then " +
                                "          case when q.AnsSelection = 'SINGLE' then " +
                                "              q.RightMarks " +
                                "          when q.AnsSelection = 'MULTIPLE' then " +
                                "              q.RightMarks " +
                                "          else  " +
                                "              q.RightMarks " +
                                "          end " +
                                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 0  and  q.AnsSelection = 'MULTIPLE' then  " +
                                "           case when [dbo].[GetTrueCount](q.Ans,e.Ans)  = q.RightMarks    then q.RightMarks else case when [dbo].[GetFalseCount](q.Ans,e.Ans) > 0 then [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks) else [dbo].[GetTrueCount](q.Ans,e.Ans) * 1 end end    " +
                                "      when e.Ans <> q.Ans then " +
                                "          case when q.AnsSelection = 'SINGLE' then " +
                                "              case when isnull(ess.NegativeMarks, 0) = 1 then " +
                                "                  (q.WrongMarks) " +
                                "              else " +
                                "                  0  " +
                                "              end " +
                                "          when q.AnsSelection = 'MULTIPLE' then " +
                                "              [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks)    " +
                                "          Else  				   " +
                                "              (q.WrongMarks) " +
                                "          End  		 " +
                                "      end )   end ) as TotalMarks   " +
                             " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,[dbo].[GetUniqueAnsInNumber](e.Ans) )= 1 then 1 else 0 end) as NoOfTrueAnswers " +
                             " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,[dbo].[GetUniqueAnsInNumber](e.Ans) )= 0 then 1 else 0 end) as NoOfWrongAnswers " +
                             " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions" +
                             " ,tst.[text] as Standard,su.Name as Subject,ts.TestName,r.IdProofImageName,r.CollegeIdProofImageName," +
                             " (select Count(*) From Ques where SubId = q.SubId and TestId = q.TestId) as NoOFQuestions" +
                             " ,CASE WHEN " +
                             " ISNULL(ess.ShowResult,0) = 0 THEN -1 " +
                             " ELSE " +
                             " datediff(mi,dateadd(mi,ISNULL(ess.MinsforResultShow,0),ess.ExamToTime),getdATE()) END iSShowResult " +
                             " from Exams e " +
                             " inner join (select max(StartDt) as StartDt, max(EndDt) as EndDt, RegistrationId, ExamScheduleId " +
                             " from ExamStartStopTimes group by RegistrationId, ExamScheduleId) s on s.RegistrationId = e.RegistrationId" +
                             " and s.ExamScheduleId = e.ExamScheduleId " +
                             " inner join ExamSchedules ess on ess.ExamScheduleId = e.ExamScheduleId" +
                             " inner join Ques q on q.QueId = e.QueId " +
                             " inner join Registration r on r.RegistrationId = e.RegistrationId " +
                             " inner join Subs su on su.SubId = q.SubId" +
                             " inner join Tests ts on ts.TestId = q.TestId" +
                             " inner join Textlists tst on tst.textListId = r.standardId " +
                             " left join (select sum(TotalObtMark) as TotalObtMark, ExamId from ExamEvaluations group by ExamId) eex on eex.ExamId = e.ExamId " +
                             "   " +
                             " " + where + "   " +
                             " group by e.RegistrationId,r.FirstName,r.MobileNo,q.SubId,q.TestId,r.SchoolName,tst.[text],su.Name,ts.TestName,r.IdProofImageName,r.CollegeIdProofImageName" +
                             " ,ess.ShowResult,ISNULL(ess.MinsforResultShow,0),ess.ExamToTime ";

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
    public DataTable GetResultFinalMasterSheet(string TestId)
    {
        try
        {
            string where, where2;
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();
            where = "";
            where2 = "";

            if (TestId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                    where2 += " AND ";
                }
                where += " e.TestId = '" + TestId + "'";
                where2 += " qq.TestId = '" + TestId + "'";
            }


            if (where != "")
            {
                where = " WHERE " + where;
                where2 = " WHERE " + where2;
            }




            sqlCmd.CommandText = "  select e.RegistrationId,'9090909090' MobileNo, '' as 'College Name','demo' FirstName " +
                            " ,SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case when isnull(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) as TotalMarks " +
                            " ,SUM(Case when e.Ans <> '~SKIPPED~' and e.Ans = q.Ans then 1 else 0 end) as NoOfTrueAnswers " +
                            " ,SUM(Case when e.Ans <> '~SKIPPED~' and e.Ans <> q.Ans then 1 else 0 end) as NoOfWrongAnswers " +
                            " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions  " +
                            " ,tst.[text] as Standard,su.Name as Subject,ts.TestName, " +
                            " (select Count(*) From Ques where SubId = q.SubId and TestId = q.TestId) as NoOFQuestions " +
                            " from (select TestId as ExamId,TestId as RegistrationId,qq.SubId,qq.QueId,qq.Ans,getdate() as StartDt,getdate() as EndDt,TestId as ExamScheduleId,qq.TestID from Ques qq " +
                            " " + where2 + "  ) as e    " +
                            " inner join ExamSchedules ess on ess.ExamScheduleId = e.ExamScheduleId" +
                            " inner join Ques q on q.QueId = e.QueId " +
                            " inner join Subs su on su.SubId = q.SubId " +
                            " inner join Tests ts on ts.TestId = q.TestId " +
                            " inner join Textlists tst on tst.textListId = su.StandardTextListId " +
                             " " + where + "   " +
                             " group by e.RegistrationId,q.SubId,q.TestId,tst.[text],su.Name,ts.TestName ";

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

            sqlCmd.CommandText = " select distinct q.Que,case when q.QueType = 'MCQ' then case when Q.Ans = '1' then q.A1 when Q.Ans = '2' then q.A2 when Q.Ans = '3' then q.A3  when Q.Ans ='4' then q.A4 else Q.Ans end when q.QueType = 'NONMCQ' then q.Ans else q.Ans End as OriginalAns," +
                " case when q.QueType = 'MCQ' then  " +
                " case when q.AnsSelection = 'SINGLE' then case when e.Ans = '1' then q.A1 when e.Ans = '2' then q.A2 when e.Ans = '3' then q.A3  when e.Ans ='4' then q.A4 when e.Ans ='~SKIPPED~' then 'Skipped' END  " +
                " when q.AnsSelection = 'MULTIPLE' then e.Ans End " +
                " when q.QueType = 'NONMCQ' then  case when e.Ans ='~SKIPPED~'  then 'Skipped' else [dbo].[GetUniqueAnsInNumber](e.Ans) end   end as FilledAns " +
                " ,replace(q.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                " ,case when Q.Ans = '1' then replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when Q.Ans = '2' then replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when Q.Ans = '3' then replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when Q.Ans ='4' then replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " end as OriginalAnsImage " +
                " ,case when [dbo].[GetUniqueAnsInNumber](e.Ans) = '1' then replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when [dbo].[GetUniqueAnsInNumber](e.Ans) = '2' then replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "')  " +
                " when [dbo].[GetUniqueAnsInNumber](e.Ans) = '3' then replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "')  " +
                " when [dbo].[GetUniqueAnsInNumber](e.Ans) ='4' then replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "')  " +
                " when [dbo].[GetUniqueAnsInNumber](e.Ans) ='~SKIPPED~' then 'Skipped' end as FilledAnsImage " +
                " , replace(q.SampleAns1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as SampleAns1 " +
                " , replace(q.SampleAns2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as SampleAns2 " +
                " , replace(q.SampleAns3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as SampleAns3 " +
                " , replace(q.SampleAns4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as SampleAns4 " +
                " , replace(e.AnsImage1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as AnsImage1 " +
                " , replace(e.AnsImage2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as AnsImage2 " +
                " , replace(e.AnsImage3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as AnsImage3 " +
                " , replace(e.AnsImage4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as AnsImage4 " +
                "  ,   ( " +
                "  Case when e.Ans = '~SKIPPED~' then   " +
                "          q.NonMarks   		 " +
                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 1  then " +
                "          case when q.AnsSelection = 'SINGLE' then " +
                "              q.RightMarks " +
                "          when q.AnsSelection = 'MULTIPLE' then " +
                "              [dbo].[GetTrueCount](q.Ans,e.Ans)  * q.RightMarks " +
                "          else  " +
                "              q.RightMarks " +
                "          end " +
                "      when e.Ans <> q.Ans then " +
                "          case when q.AnsSelection = 'SINGLE' then " +
                "              case when isnull(ess.NegativeMarks, 0) = 1 then " +
                "                  (q.WrongMarks) " +
                "              else " +
                "                  0  " +
                "              end " +
                "          when q.AnsSelection = 'MULTIPLE' then " +
                "              [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks)    " +
                "          Else  				   " +
                "              (q.WrongMarks) " +
                "          End  		 " +
                "      end )    as TotalMarks,q.Srno  " +
                 " from Registration r " +
                " left join Exams e on r.RegistrationId = e.RegistrationId " +
                " inner join ExamSchedules ess on ess.ExamScheduleId = e.ExamScheduleId " +
                " inner join Ques q on q.QueId = e.QueId" + where + " order by q.Srno ";

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
    public DataTable GetExamDetailsAdv(string RegistrationId, string ExamScheduleId)
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

            sqlCmd.CommandText = " select distinct q.Que,case when q.QueType = 'MCQ' then case when Q.Ans = '1' then q.A1 when Q.Ans = '2' then q.A2 when Q.Ans = '3' then q.A3  when Q.Ans ='4' then q.A4 else Q.Ans end when q.QueType = 'NONMCQ' then q.Ans else q.Ans End as OriginalAns," +
                " case when q.QueType = 'MCQ' then  " +
                " case when q.AnsSelection = 'SINGLE' then case when e.Ans = '1' then q.A1 when e.Ans = '2' then q.A2 when e.Ans = '3' then q.A3  when e.Ans ='4' then q.A4 when e.Ans ='~SKIPPED~' then 'Skipped' END  " +
                " when q.AnsSelection = 'MULTIPLE' then e.Ans End " +
                " when q.QueType = 'NONMCQ' then  case when e.Ans ='~SKIPPED~'  then 'Skipped' else e.Ans end   end as FilledAns " +
                " ,replace(q.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                " ,case when Q.Ans = '1' then replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when Q.Ans = '2' then replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when Q.Ans = '3' then replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when Q.Ans ='4' then replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " end as OriginalAnsImage " +
                " ,case when e.Ans = '1' then replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when e.Ans = '2' then replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "')  " +
                " when e.Ans = '3' then replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "')  " +
                " when e.Ans ='4' then replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "')  " +
                " when e.Ans ='~SKIPPED~' then 'Skipped' end as FilledAnsImage " +
                " , replace(q.SampleAns1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as SampleAns1 " +
                " , replace(q.SampleAns2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as SampleAns2 " +
                " , replace(q.SampleAns3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as SampleAns3 " +
                " , replace(q.SampleAns4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as SampleAns4 " +
                " , replace(e.AnsImage1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as AnsImage1 " +
                " , replace(e.AnsImage2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as AnsImage2 " +
                " , replace(e.AnsImage3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as AnsImage3 " +
                " , replace(e.AnsImage4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as AnsImage4 " +
                "  ,   case when q.QueType in ('FILE', 'PDF') then eex.TotalObtMark else ( " +
                "  Case when e.Ans = '~SKIPPED~' then   " +
                "          q.NonMarks   		 " +
                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 1  then " +
                "          case when q.AnsSelection = 'SINGLE' then " +
                "              q.RightMarks " +
                "          when q.AnsSelection = 'MULTIPLE' then " +
                "                q.RightMarks " +
                "          else  " +
                "              q.RightMarks " +
                "          end " +
                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 0  and  q.AnsSelection = 'MULTIPLE' then  " +
                "           case when [dbo].[GetTrueCount](q.Ans,e.Ans)  = q.RightMarks    then q.RightMarks else case when [dbo].[GetFalseCount](q.Ans,e.Ans) > 0 then [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks) else [dbo].[GetTrueCount](q.Ans,e.Ans) * 1 end end     " +
                "      when e.Ans <> q.Ans then " +
                "          case when q.AnsSelection = 'SINGLE' then " +
                "              case when isnull(ess.NegativeMarks, 0) = 1 then " +
                "                  (q.WrongMarks) " +
                "              else " +
                "                  0  " +
                "              end " +
                "          when q.AnsSelection = 'MULTIPLE' then " +
                "              [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks)    " +
                "          Else  				   " +
                "              (q.WrongMarks) " +
                "          End  		 " +
                "      end )  end  as TotalMarks,q.Srno  " +
                 " from Registration r " +
                " left join Exams e on r.RegistrationId = e.RegistrationId " +
                " inner join ExamSchedules ess on ess.ExamScheduleId = e.ExamScheduleId " +
                " inner join Ques q on q.QueId = e.QueId " +
                " left join (select sum(TotalObtMark) as TotalObtMark, ExamId from ExamEvaluations group by ExamId) eex on eex.ExamId = e.ExamId " +
                where + " order by q.Srno ";
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
    public DataTable GetExamDetailsMaster(string TestId)
    {
        try
        {
            string where;
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();
            where = "";

            if (TestId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " q.TestId = '" + TestId + "'";
            }


            if (where != "")
            {
                where = " WHERE " + where;
            }

            sqlCmd.CommandText = " select	  q.Que,case when Q.Ans = '1' then q.A1 when Q.Ans = '2' then q.A2 when Q.Ans = '3' then q.A3 " +
                " when Q.Ans = '4' then q.A4 end as OriginalAns " +
                " ,q.A1 as FilledAns1 " +
                " ,q.A2 as FilledAns2 " +
                " ,q.A3 as FilledAns3 " +
                " ,q.A4 as FilledAns4 " +
                " , replace(q.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                "  ,case when Q.Ans = '1' then replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when Q.Ans = '2' then replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when Q.Ans = '3' then replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " when Q.Ans = '4' then replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') " +
                " end as OriginalAnsImage " +
                " ,replace(q.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as FilledAnsImage1 " +
                " ,replace(q.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as FilledAnsImage2 " +
                " ,replace(q.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as FilledAnsImage3 " +
                " ,replace(q.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as FilledAnsImage4 " +
                " , q.SrNo ,q.Ans,s.Name " +
                " from Ques q  " +
                " Left join Subs s on s.subId = q.SubId " +
                where + "   order by q.SrNo ";
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

    public DataTable LiveResultDetail(string StandardId, string SubjectId, string TestId, string StudentName, string MobileNo, string ExamScheduleId, string ResultType, DateTime? FromScheduleDt, DateTime? ToScheduleDt)
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
            where += " e.StandardTextListId = '" + StandardId + "'";
        }

        if (SubjectId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " e.SubId = '" + SubjectId + "'";
        }

        if (TestId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " e.TestId = '" + TestId + "'";
        }

        if (ExamScheduleId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " e.ExamScheduleId = '" + ExamScheduleId + "'";
        }

        if (FromScheduleDt != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " convert(Datetime, e.ExamFromTime) >= convert(datetime, '" + Convert.ToDateTime(FromScheduleDt).ToString("dd-MMM-yyyy hh:mm tt") + "')";
        }

        if (ToScheduleDt != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " convert(Datetime, e.ExamToTime) <= convert(datetime, '" + Convert.ToDateTime(ToScheduleDt).ToString("dd-MMM-yyyy hh:mm tt") + "')";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }

        string sql = "";
        sql = " select * from ( select e.*, t.[Text] as 'Standard', s.Name as 'Subject', ts.TestName, u.FirstName + ' ' + u.LastName as 'GeneratedBy' " +
                                 " , case  when getdate() > e.ExamToTime THEN 9 when getdate() > e.ExamFromTime and getdate() < e.ExamToTime THEN 1 else 2 end as 'ExamRunning' " +
                                 " , (select count(*) from ExamStartStopTimes x where x.ExamScheduleId = e.ExamScheduleId " +
                                 " and x.RegistrationId = el.RegistrationId and StartDt is not null and EndDt is not null) as 'IsCompleted' " +
                                 " , datediff(minute, getdate(), e.ExamFromTime) as 'DiffMins' " +
                                 " , (select count(*) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId) as 'DoneQuestions' " +
                                 " , (select min(InsertedOn) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId) as 'ExamStartTime' " +
                                 " , (select max(InsertedOn) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId) as 'ExamStopTime' " +
                                 " , r.FirstName, r.MobileNo " +
                                 " , e.TotalQuestions - (select count(*) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId) as 'PendingQuestion'  " +
                                 " From ExamSchedules e " +
                                 " inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
                                 " inner join TextLists t on t.TextListId = e.StandardTextListId " +
                                 " inner join Subs s on s.SubId = e.SubId " +
                                 " inner join Tests ts on ts.TestId = e.TestId " +
                                 " inner join Users u on u.UserId = e.InsertedByUserId " +
                                 " inner join Registration r on r.RegistrationId = el.RegistrationId " +
                                 where +
                                 "  ) as zz   ";
        switch (ResultType)
        {
            case "Present":
                sql += " where zz.DoneQuestions <> 0 ";
                break;
            case "Absent":
                sql += " where zz.DoneQuestions = 0 ";
                break;
            default:
                // code block
                break;
        }

        sql += " order by FirstName  ";

        sqlCmd.CommandText = sql;
        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        _generalDAL.CloseSQLConnection();

        return dt;
    }

    public DataTable LiveExamDetail()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            string sql = "";
            sql = " select * from ( select e.*, t.[Text] as 'Standard', s.Name as 'Subject', ts.TestName, u.FirstName + ' ' + u.LastName as 'GeneratedBy' " +
                                     " , case  when getdate() > e.ExamToTime THEN 9 when getdate() > e.ExamFromTime and getdate() < e.ExamToTime THEN 1 else 2 end as 'ExamRunning' " +
                                     " , (select count(*) from ExamStartStopTimes x where x.ExamScheduleId = e.ExamScheduleId " +
                                     " and x.RegistrationId = el.RegistrationId and StartDt is not null and EndDt is not null) as 'IsCompleted' " +
                                     " , datediff(minute, getdate(), e.ExamFromTime) as 'DiffMins' " +
                                     " , (select count(*) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId) as 'DoneQuestions' " +
                                     " , (select min(InsertedOn) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId) as 'ExamStartTime' " +
                                     " , (select max(InsertedOn) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId) as 'ExamStopTime' " +
                                     " , r.FirstName, r.MobileNo " +
                                     " , e.TotalQuestions - (select count(*) from Exams x where x.ExamScheduleId = e.ExamScheduleId and x.RegistrationId = el.RegistrationId) as 'PendingQuestion'  " +
                                     " From ExamSchedules e " +
                                     " inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
                                     " inner join TextLists t on t.TextListId = e.StandardTextListId " +
                                     " inner join Subs s on s.SubId = e.SubId " +
                                     " inner join Tests ts on ts.TestId = e.TestId " +
                                     " inner join Users u on u.UserId = e.InsertedByUserId " +
                                     " inner join Registration r on r.RegistrationId = el.RegistrationId " +
                                     "  ) as zz   ";


            sql += " order by FirstName  ";

            sqlCmd.CommandText = sql;
            _generalDAL.OpenSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            dt.Load(sqlCmd.ExecuteReader());

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    public DataTable ERPExportData(DateTime? FromScheduleDt, DateTime? ToScheduleDt, string StandardId, string SubjectId, string TestId, string StudentName, string MobileNo, string ExamScheduleId, bool isExcel)
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

        if (where != "")
        {
            where = " WHERE " + where;
        }
        if (ExamScheduleId != null)
        {
            sqlCmd.CommandText = "";

            sqlCmd.CommandText = "Select ERPStd as 'Standard', Division, GRNo, RollNo, FirstName, TotalMarks from (";

            sqlCmd.CommandText += " select r.RegistrationId,ess.ExamScheduleId, stu.GRNO, stu.RollNo, stu.division, stu.Standard as 'ERPStd', r.ExamNo, r.MobileNo,r.FirstName " +
                     " , SUM(q.RightMarks) as RightMarks " +
                     " ,SUM( case when q.QueType in ('FILE', 'PDF') then eex.TotalObtMark else ( " +
                                "  Case when e.Ans = '~SKIPPED~' then   " +
                                "          q.NonMarks   		 " +
                                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 1  then " + //"      when e.Ans = q.Ans  then " +
                                "          case when q.AnsSelection = 'SINGLE' then " +
                                "              q.RightMarks " +
                                "          when q.AnsSelection = 'MULTIPLE' then " +
                                "              q.RightMarks " +
                                "          else  " +
                                "              q.RightMarks " +
                                "          end " +
                                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 0  and  q.AnsSelection = 'MULTIPLE' then  " +
                                "           case when [dbo].[GetTrueCount](q.Ans,e.Ans)  = q.RightMarks    then q.RightMarks else case when [dbo].[GetFalseCount](q.Ans,e.Ans) > 0 then [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks) else [dbo].[GetTrueCount](q.Ans,e.Ans) * 1 end end    " +
                                "      when e.Ans <> q.Ans then " +
                                "          case when q.AnsSelection = 'SINGLE' then " +
                                "              case when isnull(ess.NegativeMarks, 0) = 1 then " +
                                "                  (q.WrongMarks) " +
                                "              else " +
                                "                  0  " +
                                "              end " +
                                "          when q.AnsSelection = 'MULTIPLE' then " +
                                "              [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks)    " +
                                "          Else  				   " +
                                "              (q.WrongMarks) " +
                                "          End  		 " +
                                "      end) end )    as TotalMarks   " +
                    " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].[GetUniqueAnsInNumber](e.Ans) = q.Ans then 1 else 0 end) as NoOfTrueAnswers " +
                    " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].[GetUniqueAnsInNumber](e.Ans) <> q.Ans then 1 else 0 end) as NoOfWrongAnswers " +
                    " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions" +
                    " ,(select Count(*) From Ques where SubId = q.SubId and TestId = q.TestId) as NoOFQuestions" +
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
                    " left join (select sum(TotalObtMark) as TotalObtMark, ExamId from ExamEvaluations group by ExamId) eex on eex.ExamId = e.ExamId " +
                    " inner join " + ConfigurationManager.AppSettings["ERPDBName"].ToString() + "..vwAllStudentDetails stu on stu.StudentId = r.RegistrationId " +
                    where +
                    " group by q.Subid,q.TestId,r.RegistrationId,ess.ExamScheduleId,r.ExamNo, r.MobileNo,r.FirstName,tst.[text],su.Name,ts.TestName, ess.ExamFromTime, stu.GRNO  , stu.RollNo, stu.division, stu.Standard ";// +

            sqlCmd.CommandText += " ) as XX order by Division, convert(int, RollNo) Asc";
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }

    public DataTable ERPExportDataQueWise(DateTime? FromScheduleDt, DateTime? ToScheduleDt, string StandardId, string SubjectId, string TestId, string StudentName, string MobileNo, string ExamScheduleId, bool isExcel)
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

        if (where != "")
        {
            where = " WHERE " + where;
        }
        if (ExamScheduleId != null)
        {
            sqlCmd.CommandText = "";

            sqlCmd.CommandText = "Select ERPStd as 'Standard', Division, GRNo, RollNo, FirstName, TotalMarks, TotalMarksQue " +
                                 " into #Tmp from ( ";

            sqlCmd.CommandText += " select r.RegistrationId,ess.ExamScheduleId, stu.GRNO, stu.RollNo, stu.division, stu.Standard as 'ERPStd', r.ExamNo, r.MobileNo,r.FirstName " +
                     " , SUM(q.RightMarks) as RightMarks " +
                     " ,SUM( case when q.QueType in ('FILE', 'PDF') then eex.TotalObtMark else ( " +
                                "  Case when e.Ans = '~SKIPPED~' then   " +
                                "          q.NonMarks   		 " +
                                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 1  then " + //"      when e.Ans = q.Ans  then " +
                                "          case when q.AnsSelection = 'SINGLE' then " +
                                "              q.RightMarks " +
                                "          when q.AnsSelection = 'MULTIPLE' then " +
                                "              q.RightMarks " +
                                "          else  " +
                                "              q.RightMarks " +
                                "          end " +
                                "      when [dbo].NumericOrStringCompare(q.QueDataType,q.Ans,e.Ans )= 0  and  q.AnsSelection = 'MULTIPLE' then  " +
                                "           case when [dbo].[GetTrueCount](q.Ans,e.Ans)  = q.RightMarks    then q.RightMarks else case when [dbo].[GetFalseCount](q.Ans,e.Ans) > 0 then [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks) else [dbo].[GetTrueCount](q.Ans,e.Ans) * 1 end end    " +
                                "      when e.Ans <> q.Ans then " +
                                "          case when q.AnsSelection = 'SINGLE' then " +
                                "              case when isnull(ess.NegativeMarks, 0) = 1 then " +
                                "                  (q.WrongMarks) " +
                                "              else " +
                                "                  0  " +
                                "              end " +
                                "          when q.AnsSelection = 'MULTIPLE' then " +
                                "              [dbo].[GetFalseCount](q.Ans,e.Ans) * (q.WrongMarks)    " +
                                "          Else  				   " +
                                "              (q.WrongMarks) " +
                                "          End  		 " +
                                "      end) end )    as TotalMarks   " +
                    " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].[GetUniqueAnsInNumber](e.Ans) = q.Ans then 1 else 0 end) as NoOfTrueAnswers " +
                    " ,SUM(Case when e.Ans <> '~SKIPPED~' and [dbo].[GetUniqueAnsInNumber](e.Ans) <> q.Ans then 1 else 0 end) as NoOfWrongAnswers " +
                    " ,SUM(Case when e.Ans = '~SKIPPED~' then 1 else 0 end) as NoOfUnAttemptedQuestions" +
                    " ,(select Count(*) From Ques where SubId = q.SubId and TestId = q.TestId) as NoOFQuestions" +
                    " ,tst.[text] as Standard,su.Name as Subject,ts.TestName + ' Schd.-' +  convert(varchar(50), ess.ExamFromTime,100) as TestName, 'TotalMarksQue'+convert(varchar, q.SrNo) as TotalMarksQue " +
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
                    " left join (select sum(TotalObtMark) as TotalObtMark, ExamId from ExamEvaluations group by ExamId) eex on eex.ExamId = e.ExamId " +
                    " inner join " + ConfigurationManager.AppSettings["ERPDBName"].ToString() + "..vwAllStudentDetails stu on stu.StudentId = r.RegistrationId " +
                    where +
                    " group by q.Subid,q.TestId,r.RegistrationId,ess.ExamScheduleId,r.ExamNo, r.MobileNo,r.FirstName,tst.[text],su.Name,ts.TestName, ess.ExamFromTime, stu.GRNO  , stu.RollNo, stu.division, stu.Standard, 'TotalMarksQue'+convert(varchar, q.SrNo) ";// +

            sqlCmd.CommandText += " ) as XX order by Division, convert(int, RollNo) Asc " +
                    " DECLARE @columns NVARCHAR(MAX),@columns2 NVARCHAR(MAX), @sql NVARCHAR(MAX),@columnsTotal NVARCHAR(MAX);    " +
                    " SET @columns = N'';  SET @columns2 = N'' ;  SET @columnsTotal = N'';   " +
                    " SELECT @columns += N', ' + QUOTENAME(TotalMarksQue) + ' '  FROM (SELECT TOP 300000000 isnull(p.TotalMarksQue,'')  as TotalMarksQue FROM #Tmp AS p GROUP BY p.TotalMarksQue ORDER BY p.TotalMarksQue) AS x; " +
                    " SELECT @columns2 += N', isnull(p.' + QUOTENAME(TotalMarksQue) + ', 0) as '+QUOTENAME(TotalMarksQue)+''  FROM (SELECT TOP 300000000 isnull(p.TotalMarksQue,'')  as Dy,p.TotalMarksQue FROM #Tmp AS p  GROUP BY p.TotalMarksQue  ORDER BY p.TotalMarksQue) AS x;   " +
                    " SELECT @columnsTotal += N'+ isnull(p.' + QUOTENAME(TotalMarksQue) + ', 0) '  FROM (SELECT TOP 300000000 isnull(p.TotalMarksQue,'')  as Dy,p.TotalMarksQue FROM #Tmp AS p  GROUP BY p.TotalMarksQue  ORDER BY p.TotalMarksQue) AS x;   " +
                    " SET @sql = N'  SELECT Standard, Division, GRNo, RollNo, FirstName,' + STUFF(@columns2, 1, 2, '') + ' ,' + STUFF(@columnsTotal, 1, 2, '') + ' as ''TotalMarks'' FROM  (  SELECT Standard, Division, GRNo, RollNo, FirstName, TotalMarks, TotalMarksQue  FROM #Tmp AS p ) AS j  PIVOT  (  SUM( TotalMarks ) FOR TotalMarksQue IN (' + STUFF(REPLACE(@columns, ', p.[', ',['), 1, 1, '')  + ') ) AS p order by Division, convert(int, RollNo) Asc;';    " +
                    " print @sql; " +
                    " EXEC sp_executesql @sql; " +
                    " drop table #Tmp ";
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }
}
