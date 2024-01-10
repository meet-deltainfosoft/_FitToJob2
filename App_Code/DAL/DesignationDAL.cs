using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

public class DesignationDAL
{
    #region Declaration
    private GeneralDAL _generalDAL;
    #endregion

    #region Constructor
    public DesignationDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~DesignationDAL()
    {
        _generalDAL = null;
    }
    #endregion

    #region Insert
    public void Insert(DesignationDTO _designationDTO)
    {
        string DesignationId;
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " DECLARE  @DesignationId uniqueidentifier;" +
                                 " SET @DesignationId = NewId()" +
                                 " INSERT INTO Designations(DesignationId,Name,DeptId,ReportDesignId,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId)" +
                                 " VALUES(@DesignationId," +
                                 ((_designationDTO.Name == null) ? "NULL" : "N'" + _designationDTO.Name.Replace("'", "''") + "'") + "," +
                                 ((_designationDTO.DeptId == null) ? "NULL" : "'" + _designationDTO.DeptId + "'") + "," +
                                 ((_designationDTO.ReportDesignId == null) ? "NULL" : "'" + _designationDTO.ReportDesignId + "'") + "," +
                                 " GETDATE(),GETDATE()," +
                                 " '" + MySession.UserUnique + "','" + MySession.UserUnique + "');" +
                                 " Select @DesignationId";
            DesignationId = sqlCmd.ExecuteScalar().ToString();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region Update
    public void Update(DesignationDTO _designationDTO)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " UPDATE Designations SET " +
                                 " Name=" + ((_designationDTO.Name == null) ? "NULL" : "N'" + _designationDTO.Name.Replace("'", "''") + "'") +
                                 " ,DeptId=" + ((_designationDTO.DeptId == null) ? "NULL" : "'" + _designationDTO.DeptId + "'") +
                                 " ,ReportDesignId=" + ((_designationDTO.ReportDesignId == null) ? "NULL" : "'" + _designationDTO.ReportDesignId + "'") +
                                 " ,LastUpdatedOn=GETDATE()" +
                                 " ,LastUpdatedByUserId='" + MySession.UserUnique + "'" +
                                 " WHERE DesignationId='" + _designationDTO.DesignationId + "'";
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

    #region Functions
    public string IsReferenced(string DesignationId)
    {
        string strRef = "";
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            if (DesignationId != null)
            {
                strRef += _generalDAL.IsReferenced("Designations", "DesignationId", DesignationId, sqlCmd, "'Designations'");
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
    #endregion

    #region Load Dropdown

    public DataTable GetDepts()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "Select Distinct DeptId,Name FROM Depts ORDER BY Name ASC";
            //sqlCmd.CommandText = "select * from TextLists where [Group]='StafCategory' order by Text";
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

    public DataTable GetReportingDesign()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "Select Distinct DesignationId,Name from Designations ORDER BY Name ASC";
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

    #region Delete
    public void Delete(string DesignationId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;

            string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..Designations(DesignationId, Name, DeptId,ReportDesignId ,LastUpdatedOn, LastUpdatedByUserId, InsertedOn, InsertedByUserId)" +
                                 " Select DesignationId, Name, DeptId,ReportDesignId , LastUpdatedOn, LastUpdatedByUserId, getdate(),'" + MySession.UserUnique + "'" +
                                 " FROM Designations WHERE DesignationId='" + DesignationId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " DELETE FROM Designations WHERE DesignationId='" + DesignationId + "'";
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

    #region Select
    public DesignationDTO Select(string DesignationId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader sqlDr;
            DesignationDTO _designationDTO = new DesignationDTO();
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM Designations WHERE DesignationId='" + DesignationId + "'";
            sqlDr = sqlCmd.ExecuteReader();

            while (sqlDr.Read())
            {
                _designationDTO.DesignationId = sqlDr["DesignationId"].ToString();

                if (sqlDr["Name"] != DBNull.Value)
                    _designationDTO.Name = sqlDr["Name"].ToString();
                else
                    _designationDTO.Name = null;

                if (sqlDr["DeptId"] != DBNull.Value)
                    _designationDTO.DeptId = sqlDr["DeptId"].ToString();
                else
                    _designationDTO.DeptId = null;

                if (sqlDr["ReportDesignId"] != DBNull.Value)
                    _designationDTO.ReportDesignId = sqlDr["ReportDesignId"].ToString();
                else
                    _designationDTO.ReportDesignId = null;
            }

            sqlDr.Close();
            _generalDAL.CloseSQLConnection();
            return _designationDTO;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    #endregion
}