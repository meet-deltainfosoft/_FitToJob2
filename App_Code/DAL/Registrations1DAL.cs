using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for Registrations1DAL
/// </summary>
public class Registrations1DAL
{
	 #region Declaration
    private GeneralDAL _generalDAL;
    #endregion

    #region Constructor Destructor
    public Registrations1DAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~Registrations1DAL()
    {
        _generalDAL = null;
    }
    #endregion

    #region Get Data
    public DataTable Registration(string AadharNo, string FirstName, string LastName, string MiddleName ,string MobileNo, bool AllRecords)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            string where = "";
            string Top = "";


            if (AadharNo != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += "  a.AadharCardNo Like '%" + AadharNo.Replace("'", "''") + "%'";
            }
            if (FirstName != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += "  a.FirstName Like '%" + FirstName.Replace("'", "''") + "%'";
            }
            if (LastName != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += "  a.LastName Like '%" + LastName.Replace("'", "''") + "%'";
            }

            if (MiddleName != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += "  a.MiddleName Like '%" + MiddleName.Replace("'", "''") + "%'";
            }

            if (MobileNo != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += "  a.MobileNo Like '%" + MobileNo.Replace("'", "''") + "%'";
            }

            if (where != "")
            {
                where = " WHERE " + where;
            }

            if (AllRecords == false)
                Top = " Top 30 ";
            else
                Top = "";

            sqlCmd.CommandText = " Select " + Top + " a.RegistrationId,a.AadharCardNo, a.FirstName, a.MiddleName, a.LastName, a.MobileNo, " +
                                 "  a.City, a.Taluka, a.District, a.State, a.Address ," +
                                 " a.InsertedOn, a.LastUpdatedOn " +
                                 " from Registrations a " +
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
}