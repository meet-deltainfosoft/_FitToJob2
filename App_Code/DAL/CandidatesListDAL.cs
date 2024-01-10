using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

/// <summary>
/// Summary description for CandidatesListDAL
/// </summary>
public class CandidatesListDAL
{
    private GeneralDAL _generalDAL;

    public CandidatesListDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~CandidatesListDAL()
    {
        _generalDAL = null;
    }

    public DataTable CandidatesLists(string Name, string MobileNo, string DepartmentId, string DivisionId, string DesignationId, bool AllRecords, string City, string Taluka, string Status)
    {
        if (Name != null)
            Name = Name.Replace("'", "''");

        if (MobileNo != null)
            MobileNo = MobileNo.Replace("'", "''");



        string where;
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        where = "";

        //Name
        if (Name != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += " AND a.FirstName Like '%" + Name + "%'";
        }

        if (MobileNo != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += " AND a.MobileNo Like '%" + MobileNo + "%'";
        }

        if (City != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += " AND a.City Like '%" + City.Replace("'", "''") + "%'";
        }
        if (DepartmentId != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += " AND j.DepartmentId = '" + DepartmentId + "'";
        }

        if (DivisionId != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += " AND a.DivisionId = '" + DivisionId + "'";
        }

        if (DesignationId != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += " AND j.DesignationId = '" + DesignationId + "'";
        }

        if (Taluka != null)
        {
            //if (where != "")
            //{
            //    where += " AND ";
            //}
            where += " AND a.City Like '%" + Taluka.Replace("'", "''") + "%'";
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

        if (AllRecords == false)
        {
            sqlCmd.CommandText = "select DISTINCT a.RegistrationId,a.FirstName as Name,a.LastName as Surname,a.City  ,a.MobileNo,a.InsertedOn,a.LastUpdatedOn "+
                                 ",b.PhotoPath as Photo , b.SelfIntroVideoPath as SelfIntro,b.UplaodResume as Resume "+
                                 ", i.ThirdCompanyName as LastCompany , i.ThirdCompanySalary as LastSalary,i.ThirdCompanyExp as ExpectSalary "+
                                 ", i.OtherExpNoExpDetails As Eaperience , null as Distance , " +
                                 "SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case  when isnull "+
                                 "(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) + ISNULL((Select SUM(ISNULL(Marks,0))  from ExamMarks where "+
                                 "RegistrationId = a.RegistrationId),0)  as TotalMarks, "+
                                 "case when a.ApprovedDisapproved='A' then 'Approved' when a.ApprovedDisapproved = 'D' then 'Disapproved' else 'Open' end as ApprovedDisapproved,a.ApprovedDisapprovedOn ,a.ApprovedDisapprovedByUserId,a.ApprovedDisapprovedRemarks,j.DepartmentId,j.DivisionId,j.DesignationId,d.[Name] as JobProfile   "+
                                 " from Registration a  "+
                                 " left join RegistrationJobProfileLns R on R.RegistrationId =  a.RegistrationId " +
                                 " left join " + ConfigurationSettings.AppSettings["ERPDBName1"].ToString() + "..JobOfferings j on j.JobOfferingId = R.JobOfferingId    " +
                                 " left join Subs d on d.SubId = j.DesignationId  "+
                                 //" left Join FitToJob..RegistrationVerifications b on b.RegistrationId = a.RegistrationId  "+
                                 " left join (Select RegistrationId,Max(PhotoPath) as PhotoPath, Max(SelfIntroVideoPath) as SelfIntroVideoPath ,MAX(UplaodResume) as UplaodResume from "+ ConfigurationSettings.AppSettings["ERPDBName1"].ToString() +"..RegistrationVerifications Group BY RegistrationId) b on b.RegistrationId = a.RegistrationId "+
                                 " inner Join " + ConfigurationSettings.AppSettings["ERPDBName1"].ToString() + "..InterviewForms i on i.RegistrationId = a.RegistrationId  " +
                                 " inner join ExamScheduleLns es on es.RegistrationId = a.RegistrationId  "+
                                 " inner join ExamSchedules ess on ess.ExamScheduleId = es.ExamScheduleId  "+
                                 " inner join Exams e  on e.RegistrationId = a.RegistrationId  and e.ExamScheduleId = ess.ExamScheduleId  "+
                                 " left join Ques q on q.QueId = e.QueId "+
                                 " Where (a.ApprovedDisapproved in ('D') or a.ApprovedDisapproved is null) " +
                                   where +
                                 " Group by a.RegistrationId,a.FirstName,a.LastName,a.City,a.MobileNo,a.InsertedOn,a.LastUpdatedOn ,b.PhotoPath , b.SelfIntroVideoPath, b.UplaodResume, i.ThirdCompanyName,i.ThirdCompanySalary,i.ThirdCompanyExp, i.OtherExpNoExpDetails,a.ApprovedDisapproved,a.ApprovedDisapprovedOn ,a.ApprovedDisapprovedByUserId,a.ApprovedDisapprovedRemarks,j.DepartmentId,j.DivisionId,j.DesignationId,d.[Name] " +
                                 " Order BY a.InsertedOn desc ";
        }
        else
        {
            sqlCmd.CommandText = "select DISTINCT a.RegistrationId,a.FirstName as Name,a.LastName as Surname,a.City  ,a.MobileNo,a.InsertedOn,a.LastUpdatedOn " +
                                 //",b.PhotoPath as Photo , b.SelfIntroVideoPath as SelfIntro,b.UplaodResume as Resume " +
                                 ", i.ThirdCompanyName as LastCompany , i.ThirdCompanySalary as LastSalary,i.ThirdCompanyExp as ExpectSalary " +
                                 ", i.OtherExpNoExpDetails As Eaperience , null as Distance , " +
                                 "SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case  when isnull " +
                                 "(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) + ISNULL((Select SUM(ISNULL(Marks,0))  from ExamMarks where " +
                                 "RegistrationId = a.RegistrationId),0)  as TotalMarks, " +
                                 "case when a.ApprovedDisapproved='A' then 'Approved' when a.ApprovedDisapproved = 'D' then 'Disapproved' else 'Open' end as ApprovedDisapproved,a.ApprovedDisapprovedOn ,a.ApprovedDisapprovedByUserId,a.ApprovedDisapprovedRemarks,d.[Name] as JobProfile   " +
                                 " from Registration a  " +
                                 " left join RegistrationJobProfileLns R on R.RegistrationId =  a.RegistrationId " +
                                 " left join " + ConfigurationSettings.AppSettings["ERPDBName1"].ToString() + "..JobOfferings j on j.JobOfferingId = R.JobOfferingId    " +
                                 " left join Subs d on d.SubId = j.DesignationId  " +
                //" left Join FitToJob..RegistrationVerifications b on b.RegistrationId = a.RegistrationId  "+
                                 " left join (Select RegistrationId,Max(PhotoPath) as PhotoPath, Max(SelfIntroVideoPath) as SelfIntroVideoPath ,MAX(UplaodResume) as UplaodResume from " + ConfigurationSettings.AppSettings["ERPDBName1"].ToString() + "..RegistrationVerifications Group BY RegistrationId) b on b.RegistrationId = a.RegistrationId " +
                                 " Inner Join " + ConfigurationSettings.AppSettings["ERPDBName1"].ToString() + "..InterviewForms i on i.RegistrationId = a.RegistrationId  " +
                                 " inner join ExamScheduleLns es on es.RegistrationId = a.RegistrationId  " +
                                 " inner join ExamSchedules ess on ess.ExamScheduleId = es.ExamScheduleId  " +
                                 " inner join Exams e  on e.RegistrationId = a.RegistrationId  and e.ExamScheduleId = ess.ExamScheduleId  " +
                                 " left join Ques q on q.QueId = e.QueId " +
                                 " Where (a.ApprovedDisapproved in ('D') or a.ApprovedDisapproved is null) " +
                                   where +
                                 " Group by a.RegistrationId,a.FirstName,a.LastName,a.City,a.MobileNo,a.InsertedOn,a.LastUpdatedOn ,b.PhotoPath , b.SelfIntroVideoPath, b.UplaodResume, i.ThirdCompanyName,i.ThirdCompanySalary,i.ThirdCompanyExp, i.OtherExpNoExpDetails,a.ApprovedDisapproved,a.ApprovedDisapprovedOn ,a.ApprovedDisapprovedByUserId,a.ApprovedDisapprovedRemarks,j.DepartmentId,j.DivisionId,j.DesignationId,d.[Name] " +
                                 " Order BY a.InsertedOn desc ";
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }
    public DataTable CandidatesList(string Name, string MobileNo, string DepartmentId, string DivisionId, string DesignationId, bool AllRecords, string City, string Taluka, string Status)
    {
        if (Name != null)
            Name = Name.Replace("'", "''");

        if (MobileNo != null)
            MobileNo = MobileNo.Replace("'", "''");



        string where;
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        where = "";

        //Name
        if (Name != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += "  a.FirstName Like '%" + Name + "%'";
        }

        if (MobileNo != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += "  a.MobileNo Like '%" + MobileNo + "%'";
        }

        if (City != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += "  a.City Like '%" + City.Replace("'", "''") + "%'";
        }
        if (DepartmentId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += "  j.DepartmentId = '" + DepartmentId + "'";
        }

        if (DivisionId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += "  a.DivisionId = '" + DivisionId + "'";
        }

        if (DesignationId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += "  j.DesignationId = '" + DesignationId + "'";
        }

        if (Taluka != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += "  a.City Like '%" + Taluka.Replace("'", "''") + "%'";
        }

        if (Status != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.ApprovedDisapproved = '" + Status.Replace("'", "''") + "'";
        }
        if (where != "")
        {
            where = " WHERE " + where;
        }

        if (AllRecords == false)
        {
            sqlCmd.CommandText = "select DISTINCT a.RegistrationId,a.FirstName as Name,a.LastName as Surname,a.City  ,a.MobileNo,a.InsertedOn,a.LastUpdatedOn " +
                                 ",b.PhotoPath as Photo , b.SelfIntroVideoPath as SelfIntro,b.UplaodResume as Resume " +
                                 ", i.ThirdCompanyName as LastCompany , i.ThirdCompanySalary as LastSalary,i.ThirdCompanyExp as ExpectSalary " +
                                 ", i.OtherExpNoExpDetails As Eaperience , null as Distance , " +
                                 "SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case  when isnull " +
                                 "(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) + ISNULL((Select SUM(ISNULL(Marks,0))  from ExamMarks where " +
                                 "RegistrationId = a.RegistrationId),0)  as TotalMarks, " +
                                 "case when a.ApprovedDisapproved='A' then 'Approved' when a.ApprovedDisapproved = 'D' then 'Disapproved' else 'Open' end as ApprovedDisapproved,a.ApprovedDisapprovedOn ,a.ApprovedDisapprovedByUserId,a.ApprovedDisapprovedRemarks,j.DepartmentId,j.DivisionId,j.DesignationId,d.[Name] as JobProfile   " +
                                 " from Registration a  " +
                                 " left join RegistrationJobProfileLns R on R.RegistrationId =  a.RegistrationId " +
                                 " left join " + ConfigurationSettings.AppSettings["ERPDBName1"].ToString() + "..JobOfferings j on j.JobOfferingId = R.JobOfferingId    " +
                                 " left join Subs d on d.SubId = j.DesignationId  " +
                //" left Join FitToJob..RegistrationVerifications b on b.RegistrationId = a.RegistrationId  "+
                                 " left join (Select RegistrationId,Max(PhotoPath) as PhotoPath, Max(SelfIntroVideoPath) as SelfIntroVideoPath ,MAX(UplaodResume) as UplaodResume from " + ConfigurationSettings.AppSettings["ERPDBName1"].ToString() + "..RegistrationVerifications Group BY RegistrationId) b on b.RegistrationId = a.RegistrationId " +
                                 " inner Join " + ConfigurationSettings.AppSettings["ERPDBName1"].ToString() + "..InterviewForms i on i.RegistrationId = a.RegistrationId  " +
                                 " inner join ExamScheduleLns es on es.RegistrationId = a.RegistrationId  " +
                                 " inner join ExamSchedules ess on ess.ExamScheduleId = es.ExamScheduleId  " +
                                 " inner join Exams e  on e.RegistrationId = a.RegistrationId  and e.ExamScheduleId = ess.ExamScheduleId  " +
                                 " left join Ques q on q.QueId = e.QueId " +
                                // " Where (a.ApprovedDisapproved in ('D') or a.ApprovedDisapproved is null) " +
                                   where +
                                 " Group by a.RegistrationId,a.FirstName,a.LastName,a.City,a.MobileNo,a.InsertedOn,a.LastUpdatedOn ,b.PhotoPath , b.SelfIntroVideoPath, b.UplaodResume, i.ThirdCompanyName,i.ThirdCompanySalary,i.ThirdCompanyExp, i.OtherExpNoExpDetails,a.ApprovedDisapproved,a.ApprovedDisapprovedOn ,a.ApprovedDisapprovedByUserId,a.ApprovedDisapprovedRemarks,j.DepartmentId,j.DivisionId,j.DesignationId,d.[Name] " +
                                 " Order BY a.InsertedOn desc ";


        }
        else
        {
            sqlCmd.CommandText = "select DISTINCT a.RegistrationId,a.FirstName as Name,a.LastName as Surname,a.City  ,a.MobileNo,a.InsertedOn,a.LastUpdatedOn " +
                //",b.PhotoPath as Photo , b.SelfIntroVideoPath as SelfIntro,b.UplaodResume as Resume " +
                                 ", i.ThirdCompanyName as LastCompany , i.ThirdCompanySalary as LastSalary,i.ThirdCompanyExp as ExpectSalary " +
                                 ", i.OtherExpNoExpDetails As Eaperience , null as Distance , " +
                                 "SUM(Case when e.Ans = '~SKIPPED~' then q.NonMarks when e.Ans = q.Ans then q.RightMarks when e.Ans <> q.Ans then case  when isnull " +
                                 "(ess.NegativeMarks, 0) = 1 then (q.WrongMarks) else 0 end end) + ISNULL((Select SUM(ISNULL(Marks,0))  from ExamMarks where " +
                                 " RegistrationId = a.RegistrationId),0)  as TotalMarks, " +
                                 "case when a.ApprovedDisapproved='A' then 'Approved' when a.ApprovedDisapproved = 'D' then 'Disapproved' else 'Open' end as ApprovedDisapproved,a.ApprovedDisapprovedOn ,a.ApprovedDisapprovedRemarks,d.[Name] as JobProfile   " +
                                 " from Registration a  " +
                                 " left join RegistrationJobProfileLns R on R.RegistrationId =  a.RegistrationId " +
                                 " left join " + ConfigurationSettings.AppSettings["ERPDBName1"].ToString() + "..JobOfferings j on j.JobOfferingId = R.JobOfferingId    " +
                                 " left join Subs d on d.SubId = j.DesignationId  " +
                //" left Join FitToJob..RegistrationVerifications b on b.RegistrationId = a.RegistrationId  "+
                                 " left join (Select RegistrationId,Max(PhotoPath) as PhotoPath, Max(SelfIntroVideoPath) as SelfIntroVideoPath ,MAX(UplaodResume) as UplaodResume from " + ConfigurationSettings.AppSettings["ERPDBName1"].ToString() + "..RegistrationVerifications Group BY RegistrationId) b on b.RegistrationId = a.RegistrationId " +
                                 " Inner Join " + ConfigurationSettings.AppSettings["ERPDBName1"].ToString() + "..InterviewForms i on i.RegistrationId = a.RegistrationId  " +
                                 " inner join ExamScheduleLns es on es.RegistrationId = a.RegistrationId  " +
                                 " inner join ExamSchedules ess on ess.ExamScheduleId = es.ExamScheduleId  " +
                                 " inner join Exams e  on e.RegistrationId = a.RegistrationId  and e.ExamScheduleId = ess.ExamScheduleId  " +
                                 " left join Ques q on q.QueId = e.QueId " +
                                // " Where (a.ApprovedDisapproved in ('D') or a.ApprovedDisapproved is null) " +
                                   where +
                                 " Group by a.RegistrationId,a.FirstName,a.LastName,a.City,a.MobileNo,a.InsertedOn,a.LastUpdatedOn ,b.PhotoPath , b.SelfIntroVideoPath, b.UplaodResume, i.ThirdCompanyName,i.ThirdCompanySalary,i.ThirdCompanyExp, i.OtherExpNoExpDetails,a.ApprovedDisapproved,a.ApprovedDisapprovedOn ,a.ApprovedDisapprovedByUserId,a.ApprovedDisapprovedRemarks,j.DepartmentId,j.DivisionId,j.DesignationId,d.[Name] " +
                                 " Order BY a.InsertedOn desc ";
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

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


        string RegistrationId = "";
        string approvedDisapproved = "";
        string remarks = "";



        try
        {
            foreach (string Account in alAccount)
            {
                approvedAccount = Account.Split('|');

                RegistrationId = "";
                approvedDisapproved = "";
                remarks = "";


                RegistrationId = approvedAccount[0];
                approvedDisapproved = approvedAccount[1];
                remarks = approvedAccount[2];


                sqlcmd.CommandText = " UPDATE Registration SET " +
                                     " ApprovedDisapproved ='" + approvedDisapproved + "'" +
                                     ",ApprovedDisapprovedOn=GetDate() " +
                                     ",ApprovedDisapprovedByUserId='" + MySession.UserUnique + "'" +
                                     ",ApprovedDisapprovedRemarks = " + ((remarks == null || remarks == "") ? "NULL" : "'" + remarks.Replace("'", "''") + "'") + "" +
                                     " WHERE RegistrationId = '" + RegistrationId + "'";
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