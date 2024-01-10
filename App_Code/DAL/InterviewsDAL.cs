using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for InterviewsDAL
/// </summary>
public class InterviewsDAL
{
    private GeneralDAL _generalDAL;

    public InterviewsDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~InterviewsDAL()
    {
        _generalDAL = null;
    }
    public DataTable InterviewDetail(DateTime? FromDt, DateTime? ToDt, string Name, string UserId,string Status)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            string where;
            where = "";

            if (FromDt != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " a.Dt >='" + Convert.ToDateTime(FromDt).ToString("dd-MMM-yyyy") + "'";
            }
            if (ToDt != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " a.Dt <='" + Convert.ToDateTime(ToDt).ToString("dd-MMM-yyyy") + "'";
            }
            if (Name != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " a.Name Like '" + Name.Replace("'", "''") + "%'";
            }
            if (UserId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " a.UserId = '" + UserId.Replace("'", "''") + "'";
            }
            if (Status != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " a.Status = '" + Status.Replace("'", "''") + "'";
            }
            if (where != "")
            {
                where = " WHERE " + where;
            }
            sqlCmd.CommandText = " Select a.InterviewId,a.Name,a.Status,a.Remarks,a.Dt,IsNULL(b.FirstName,'') + ' ' + IsNULL(b.LastName,'') as InterviewBy,a.RegistrationId " +
                                 " from Interviews a" +
                                 " Inner Join Users_ b on b.UserId = a.UserId" +
                                 //" where a.Status = 'Selected' " +
                                 where +
                                 " Order BY a.Name";

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
            throw ex;
        }
    }
}