using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for JobProfilesDAL
/// </summary>
public class JobProfilesDAL
{
	#region Declaration
    private GeneralDAL _generalDAL;
    #endregion

    #region Constructor Destructor
    public JobProfilesDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~JobProfilesDAL()
    {
        _generalDAL = null;
    }
    #endregion

    #region Get Data
    public DataTable JobProfiles(string DepartmentId, string DivisionId, string DesignationId, string StaffCategoryTextListId, bool AllRecords)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            string where = "";
            string Top = "";

            if (DepartmentId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += "  a.DepartmentId = '" + DepartmentId + "'";
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
                where += "  a.DesignationId = '" + DesignationId + "'";
            }

            if (StaffCategoryTextListId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += "  a.StaffCategoryTextListId = '" + StaffCategoryTextListId + "'";
            }

            if (where != "")
            {
                where = " WHERE " + where;
            }

            if (AllRecords == false)
                Top = " Top 30 ";
            else
                Top = "";

            sqlCmd.CommandText = " Select " + Top + "  d.[Text] as Department, d1.[Text] as Division, d2.[Name] as Designation, t.[text] as StafCategory,a.JobOfferingId, a.NoOfSeats, " +
                                 "  a.ValidFrom, a.ValidTo ," +
                                 " a.InsertedOn, a.LastUpdatedOn " +
                                 " from JobOfferings a " +
                                 " left join Textlists d on d.TextListId = a.DepartmentId  " +
                                 //" left join Depts d on d.DeptId = a.DepartmentId "+
                                  " left join Textlists d1 on d1.TextListId = a.DivisionId " +
                                 " left join Designations d2 on d2.DesignationId = a.DesignationId "+
                                 " left join Textlists t on t.TextListId = a.StaffCategoryTextListId"+
                                 where +
                                 " Order By a.InsertedOn Desc ";

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