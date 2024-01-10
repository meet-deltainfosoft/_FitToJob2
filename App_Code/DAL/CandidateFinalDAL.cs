using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

/// <summary>
/// Summary description for CandidateFinalDAL
/// </summary>
public class CandidateFinalDAL
{
	private GeneralDAL _generalDAL;

    public CandidateFinalDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~CandidateFinalDAL()
    {
        _generalDAL = null;
    }

    public DataTable CandidateFinal(DateTime? FromDt, DateTime? ToDt, string Name, string UserId, string Status)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        string where;
        where = "";

        if (FromDt != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += "AND a.Dt >='" + Convert.ToDateTime(FromDt).ToString("dd-MMM-yyyy") + "'";
        }
        if (ToDt != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += "AND a.Dt <='" + Convert.ToDateTime(ToDt).ToString("dd-MMM-yyyy") + "'";
        }
        if (Name != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += " AND a.Name Like '" + Name.Replace("'", "''") + "%'";
        }
        if (UserId != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += " AND a.UserId = '" + UserId.Replace("'", "''") + "'";
        }
        if (Status != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += " AND a.ApprovedDisapproved = '" + Status.Replace("'", "''") + "'";
        }
        //if (where != "")
        //{
        //    where = " WHERE " + where;
        //}
        sqlCmd.CommandText = " Select DISTINCT a.HODInterviewId,a.Name AS Name,a.Status,a.Remarks,a.RegistrationId,R.LastName ,i.ThirdCompanyName as LastCompany , " +
                             " i.OtherExpNoExpDetails As Eaperience , null As Distance ,d.[Name] as JobProfile "+ 
                             " , SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case  when isnull (ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) + ISNULL((Select SUM(ISNULL(Marks,0))  from ExamMarks where RegistrationId = a.RegistrationId),0)  as TotalMarks "+
                             " from HODInterViews a" +
                             " Inner Join Registration R on R.RegistrationId = a.RegistrationId " +
                             " left join RegistrationJobProfileLns RL on RL.RegistrationId =  a.RegistrationId " +
                             " left Join Interviews T on T.RegistrationId = R.RegistrationId "+
                             " left join " + ConfigurationSettings.AppSettings["ERPDBName1"].ToString() + "..JobOfferings j on j.JobOfferingId = RL.JobOfferingId    " +
                             " left join Subs d on d.SubId = j.DesignationId  " +
                             " Left Join InterviewForms i on i.RegistrationId = a.RegistrationId "+
                             " inner join ExamScheduleLns es on es.RegistrationId = R.RegistrationId  "+
                             " inner join ExamSchedules ess on ess.ExamScheduleId = es.ExamScheduleId  "+
                             " inner join Exams e  on e.RegistrationId = R.RegistrationId  and e.ExamScheduleId = ess.ExamScheduleId  "+
                             " left join Ques q on q.QueId = e.QueId "+
                             " Where (a.Status in ('Selected'))  " +
                             where +
                             " Group by a.HODInterviewId,a.Name ,a.Status,a.Remarks,a.RegistrationId,R.LastName ,i.ThirdCompanyName,i.OtherExpNoExpDetails,d.[Name] " +
                             " Order BY a.Name";

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());
        _generalDAL.CloseSQLConnection();

        return dt;
    }


    public DataTable CandidateFinalNot(DateTime? FromDt, DateTime? ToDt, string Name, string UserId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        string where;
        where = "";

        if (FromDt != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += "AND a.Dt >='" + Convert.ToDateTime(FromDt).ToString("dd-MMM-yyyy") + "'";
        }
        if (ToDt != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += "AND a.Dt <='" + Convert.ToDateTime(ToDt).ToString("dd-MMM-yyyy") + "'";
        }
        if (Name != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += "AND a.Name Like '" + Name.Replace("'", "''") + "%'";
        }
        if (UserId != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += "AND a.UserId = '" + UserId.Replace("'", "''") + "'";
        }

        //if (where != "")
        //{
        //    where = " WHERE " + where;
        //}
        sqlCmd.CommandText = " Select DISTINCT a.HODInterviewId,a.Name AS Name,a.Status,a.Remarks,a.RegistrationId,R.LastName ,i.ThirdCompanyName as LastCompany , " +
                             " i.OtherExpNoExpDetails As Eaperience , null As Distance ,d.[Name] as JobProfile " +
                             " , SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case  when isnull (ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) + ISNULL((Select SUM(ISNULL(Marks,0))  from ExamMarks where RegistrationId = a.RegistrationId),0)  as TotalMarks " +
                             " from HODInterViews a" +
                             " Inner Join Registration R on R.RegistrationId = a.RegistrationId " +
                             " left join " + ConfigurationSettings.AppSettings["ERPDBName1"].ToString() + "..JobOfferings j on j.JobOfferingId = R.JobOfferingId    " +
                             " left join Subs d on d.SubId = j.DesignationId  " +
                             " Left Join FitToJob..InterviewForms i on i.RegistrationId = a.RegistrationId " +
                             " inner join ExamScheduleLns es on es.RegistrationId = R.RegistrationId  " +
                             " inner join ExamSchedules ess on ess.ExamScheduleId = es.ExamScheduleId  " +
                             " left join Exams e  on e.RegistrationId = R.RegistrationId  and e.ExamScheduleId = ess.ExamScheduleId  " +
                             " left join Ques q on q.QueId = e.QueId " +
                             " Where (a.ApprovedDisapproved is null) " +
                             where +
                             " Group by a.HODInterviewId,a.Name ,a.Status,a.Remarks,a.RegistrationId,R.LastName ,i.ThirdCompanyName,i.OtherExpNoExpDetails,d.[Name] " +
                             " Order BY a.Name";

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());
        _generalDAL.CloseSQLConnection();

        return dt;
    }

    public DataTable Department()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " select * from Depts order by Name ";
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

    public DataTable Designation()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " select * from Subs order by Name ";
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

    public DataTable Division()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " select * from Divisions order by Name ";
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
    public void Update(ArrayList alAccount)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlcmd = new SqlCommand();
        string[] approvedAccount;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlcmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlcmd.CommandType = CommandType.Text;
        sqlcmd.Transaction = sqlTrans;


        string HODInterviewId = "";
        string approvedDisapproved = "";
        string remarks = "";



        try
        {
            foreach (string Account in alAccount)
            {
                approvedAccount = Account.Split('|');

                HODInterviewId = "";
                approvedDisapproved = "";
                remarks = "";


                HODInterviewId = approvedAccount[0];
                approvedDisapproved = approvedAccount[1];
                remarks = approvedAccount[2];


                sqlcmd.CommandText = " UPDATE HODInterViews SET " +
                                     " ApprovedDisapproved ='" + approvedDisapproved + "'" +
                                     ",ApprovedDisapprovedOn=GetDate() " +
                                     ",ApprovedDisapprovedByUserId='" + MySession.UserUnique + "'" +
                                     ",ApprovedDisapprovedRemarks = " + ((remarks == null || remarks == "") ? "NULL" : "'" + remarks.Replace("'", "''") + "'") + "" +
                                     " WHERE HODInterviewId = '" + HODInterviewId + "'";
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