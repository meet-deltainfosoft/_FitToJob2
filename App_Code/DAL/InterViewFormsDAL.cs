using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for InterViewFormsDAL
/// </summary>
public class InterViewFormsDAL
{
	
	 #region Declaration
    private GeneralDAL _generalDAL;
    #endregion

    #region Constructor Destructor
    public InterViewFormsDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~InterViewFormsDAL()
    {
        _generalDAL = null;
    }
    #endregion

    #region Get Data
    public DataTable InterView(string AadharNo, string FirstName, string PanCardNo, DateTime? DOB, string MobileNo, bool AllRecords)
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
                where += "  a.FullName Like '%" + FirstName.Replace("'", "''") + "%'";
            }
            if (PanCardNo != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += "  a.PanCardNo Like '%" + PanCardNo.Replace("'", "''") + "%'";
            }

            if (DOB != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " a.DOB ='" + Convert.ToDateTime(DOB).ToString("dd-MMM-yyyy") + "'";
            }

            if (MobileNo != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += "  a.PresentMobileNo Like '%" + MobileNo.Replace("'", "''") + "%'";
            }

            if (where != "")
            {
                where = " WHERE " + where;
            }

            if (AllRecords == false)
                Top = " Top 30 ";
            else
                Top = "";

            sqlCmd.CommandText = " Select " + Top + " a.InterviewFormId,a.FullName, a.PresentAddress, a.PresentMobileNo, a.DOB, a.AadharCardNo, " +
                                 "  a.PanCardNo," +
                                 " a.InsertedOn, a.LastUpdatedOn ,a.RegistrationId " +
                                 " from InterviewForms a " +
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