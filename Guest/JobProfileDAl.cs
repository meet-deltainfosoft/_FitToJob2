using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for JobProfileDAl
/// </summary>
public class JobProfileDAl
{
    private GeneralDAL _generalDAL;

    #region Constructor Destructor
    public JobProfileDAl()
    {
        _generalDAL = new GeneralDAL();
    }
    ~JobProfileDAl()
    {
        _generalDAL = null;
    }
    #endregion


    #region "InsertFunction"

    public string Insert(JobProfileDTO JobProfileDTO)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            string JobOfferingId = "";

            _generalDAL.OpenSQLConnection();

            try
            {
                sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
                sqlCmd.CommandType = CommandType.Text;

                string k = "";
                if (JobProfileDTO.StaffCategoryTextListId != null)
                {
                    string[] Staff;
                    Staff = JobProfileDTO.StaffCategoryTextListId.ToString().Split(',');



                    foreach (string s in Staff)
                    {
                        //string StaffCategory = "";
                        //StaffCategory = Staff.ToString().Split('|')[0];

                        //if (s.ToString().ToUpper() == StaffCategory.ToString().ToUpper())
                        //{


                        //jobListDTO.JobNo = _generalDAL.VoucherNo((DateTime)jobListDTO.Dt, "D934E006-0184-454B-8AB4-E28B081EBECA", sqlCmd);

                        sqlCmd.CommandText = " DECLARE  @JobOfferingId uniqueidentifier;" +
                                             " SET @JobOfferingId = NewId()" +
                                             " INSERT INTO JobOfferings(JobOfferingId, DepartmentId, DivisionId, DesignationId, StaffCategoryTextListId, NoOfSeats, ValidFrom, ValidTo, InsertedOn, LastUpdatedOn)" +
                                             " VALUES(@JobOfferingId," +
                                             ((JobProfileDTO.DepartmentId == null) ? "NULL" : "'" + JobProfileDTO.DepartmentId.Replace("'", "''") + "'") + "," +
                                             ((JobProfileDTO.DivisionId == null) ? "NULL" : "'" + JobProfileDTO.DivisionId.Replace("'", "''") + "'") + "," +
                                             ((JobProfileDTO.DesignationId == null) ? "NULL" : "'" + JobProfileDTO.DesignationId.Replace("'", "''") + "'") + "," +
                                             ((s == null) ? "NULL" : "'" + s.Replace("'", "''") + "'") + "," +
                                             ((JobProfileDTO.NoOfSeats == null) ? "NULL" : "'" + JobProfileDTO.NoOfSeats.Replace("'", "''") + "'") + "," +
                                             ((JobProfileDTO.ValidFrom == null) ? "NULL" : "'" + Convert.ToDateTime(JobProfileDTO.ValidFrom).ToString("dd-MMM-yyyy") + "'") + "," +
                                             ((JobProfileDTO.ValidTo == null) ? "NULL" : "'" + Convert.ToDateTime(JobProfileDTO.ValidTo).ToString("dd-MMM-yyyy") + "'") + "," +
                                             " GETDATE(),GETDATE() " +
                                             ");" +
                                             " SELECT @JobOfferingId";

                        JobOfferingId = sqlCmd.ExecuteScalar().ToString();


                        //}
                    }
                    _generalDAL.CloseSQLConnection();
                }

            }
            catch
            {
                _generalDAL.CloseSQLConnection();
                throw new Exception();
            }
            return JobOfferingId;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    #endregion

    #region Update
    public string Update(JobProfileDTO _jobProfileDTO)
    {
        try
        {
            string JobOfferingId = "";
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            if (_jobProfileDTO.StaffCategoryTextListId != null)
            {
                string[] Staff;
                Staff = _jobProfileDTO.StaffCategoryTextListId.ToString().Split(',');



                foreach (string s in Staff)
                {
                    sqlCmd.CommandText = " UPDATE JobOfferings SET" +
                                             " [DepartmentId] = " + ((_jobProfileDTO.DepartmentId == null) ? "NULL" : "'" + _jobProfileDTO.DepartmentId.Replace("'", "''") + "'") +
                                             " , [DivisionId]= " + ((_jobProfileDTO.DivisionId == null) ? "NULL" : "'" + _jobProfileDTO.DivisionId.Replace("'", "''") + "'") +
                                             " , [DesignationId]= " + ((_jobProfileDTO.DesignationId == null) ? "NULL" : "'" + _jobProfileDTO.DesignationId.Replace("'", "''") + "'") +
                                             " , [StaffCategoryTextListId]= " + ((s == null) ? "NULL" : "'" + s.Replace("'", "''") + "'") +
                                             " , [NoOfSeats]= " + ((_jobProfileDTO.NoOfSeats == null) ? "NULL" : "'" + _jobProfileDTO.NoOfSeats.Replace("'", "''") + "'") +
                                             " , [ValidFrom]= " + ((_jobProfileDTO.ValidFrom == null) ? "NULL" : "'" + Convert.ToDateTime(_jobProfileDTO.ValidTo).ToString("dd-MMM-yyyy") + "'") +
                                             " , [ValidTo]= " + ((_jobProfileDTO.ValidTo == null) ? "NULL" : "'" + Convert.ToDateTime(_jobProfileDTO.ValidTo).ToString("dd-MMM-yyyy") + "'") +
                                             " , LastUpdatedOn=GETDATE()" +
                                             " , LastUpdatedByUserId=NULL" +
                                             " WHERE JobOfferingId='" + _jobProfileDTO.JobOfferingId + "'";
                    sqlCmd.ExecuteNonQuery();
                    JobOfferingId = _jobProfileDTO.JobOfferingId;
                }
            }
            
            _generalDAL.CloseSQLConnection();
            return JobOfferingId;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
    #endregion



    #region Select
    public JobProfileDTO Select(string JobOfferingId)
    {
        JobProfileDTO _JobProfileDTO = new JobProfileDTO();
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader sqlDr;
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM JobOfferings where JobOfferingId = '" + JobOfferingId + "' ";

            sqlDr = sqlCmd.ExecuteReader();

            while (sqlDr.Read())
            {
                _JobProfileDTO.JobOfferingId = sqlDr["JobOfferingId"].ToString();


                if (sqlDr["DepartmentId"] != DBNull.Value)
                    _JobProfileDTO.DepartmentId = sqlDr["DepartmentId"].ToString();
                else
                    _JobProfileDTO.DepartmentId = null;

                if (sqlDr["DivisionId"] != DBNull.Value)
                    _JobProfileDTO.DivisionId = sqlDr["DivisionId"].ToString();
                else
                    _JobProfileDTO.DivisionId = null;

                if (sqlDr["DesignationId"] != DBNull.Value)
                    _JobProfileDTO.DesignationId = sqlDr["DesignationId"].ToString();
                else
                    _JobProfileDTO.DesignationId = null;

                if (sqlDr["StaffCategoryTextListId"] != DBNull.Value)
                    _JobProfileDTO.StaffCategoryTextListId = sqlDr["StaffCategoryTextListId"].ToString();
                else
                    _JobProfileDTO.StaffCategoryTextListId = null;

                if (sqlDr["NoOfSeats"] != DBNull.Value)
                    _JobProfileDTO.NoOfSeats = sqlDr["NoOfSeats"].ToString();
                else
                    _JobProfileDTO.NoOfSeats = null;

                if (sqlDr["ValidFrom"] != DBNull.Value)
                    _JobProfileDTO.ValidFrom = Convert.ToDateTime(sqlDr["ValidFrom"].ToString());
                else
                    _JobProfileDTO.ValidFrom = null;

                if (sqlDr["ValidTo"] != DBNull.Value)
                    _JobProfileDTO.ValidTo = Convert.ToDateTime(sqlDr["ValidTo"].ToString());
                else
                    _JobProfileDTO.ValidTo = null;
            }

            sqlDr.Close();
            _generalDAL.CloseSQLConnection();
            return _JobProfileDTO;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
    #endregion

    public string IsReferenced(string JobOfferingId)
    {
        string strRef = "";
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            if (JobOfferingId != null)
            {
                strRef += _generalDAL.IsReferenced("JobOfferings", "JobOfferingId", JobOfferingId, sqlCmd, "'JobOfferings'");
            }
            _generalDAL.CloseSQLConnection();
            return strRef;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            return "";
            throw new Exception(ex.Message);
        }
    }

    #region Delete
    public void Delete(string JobOfferingId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;

            string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');
            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..JobOfferings(JobOfferingId,DepartmentId,DivisionId,DesignationId,StaffCategoryTextListId,NoOfSeats,ValidFrom,ValidTo," +
                                 " LastUpdatedOn, LastUpdatedByUserId, InsertedOn, InsertedByUserId)" +
                                 " Select JobOfferingId,DepartmentId,DivisionId,DesignationId,StaffCategoryTextListId,NoOfSeats,ValidFrom,ValidTo," +
                                 " InsertedOn,LastUpdatedOn, LastUpdatedByUserId" +
                                 " , getdate(),'" + MySession.UserUnique + "'" +
                                 " FROM JobOfferings WHERE JobOfferingId='" + JobOfferingId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " DELETE FROM JobOfferings WHERE JobOfferingId='" + JobOfferingId + "'";
            sqlCmd.ExecuteNonQuery();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
    #endregion

    public DataTable Designations(string StandardTextListId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "select * From Subs where StandardTextListId = '" + StandardTextListId.ToString() + "' order by Name";
            //sqlCmd.CommandText = " select * from Designations order by Name ";
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

    public DataTable Stafcategory()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " select TextListId,Text from TextLists where [group]='StafCategory' order by [Text] desc; ";
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