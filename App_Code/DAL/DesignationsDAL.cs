using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class DesignationsDAL
{
    #region Declaration
    private GeneralDAL _generalDAL;
    #endregion

    #region Constructor
    public DesignationsDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~DesignationsDAL()
    {
        _generalDAL = null;
    }

    #endregion

    #region Getdata
    public DataTable Designations(string Name, string DeptId, bool AllRecords)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            string where = "";
            string Top = "";

            if (Name != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += "  Name Like '" + Name.Replace("'", "''") + "%'";
            }
            if (DeptId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += "  TextListId ='" + DeptId + "'";
            }

            if (where != "")
            {
                where = " WHERE " + where;
            }

            if (AllRecords == false)
                Top = " Top 30 ";
            else
                Top = "";

            sqlCmd.CommandText = " Select " + Top + " a.DesignationId,a.Name,d.[Text] as'DeptName',dd.Name as 'ReportingDesign',a.InsertedOn,a.LastUpdatedOn " +
                                 " from Designations a" +
                                 //" Inner Join Depts d on d.DeptId = a.DeptId " +
                                 " Inner Join TextLists d on d.TextListId = a.DeptId  "+
                                 " Left Join Designations dd on dd.DesignationId = a.ReportDesignId " +
                                 where +
                                 "Order By a.InsertedOn Desc ";

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
    #endregion
}