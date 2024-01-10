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

public class RegistrationsDAL
{
    private GeneralDAL _generalDAL;

    public RegistrationsDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~RegistrationsDAL()
    {
        _generalDAL = null;
    }
    public DataTable Registrations(string RegistrationName, string MobileNo, string ExtraMobileNo, string StandardId, string DivisionTextListId, bool? IsDeActive, bool AllRecords, string SchoolName, string City)
    {
        if (RegistrationName != null)
            RegistrationName = RegistrationName.Replace("'", "''");

        if (MobileNo != null)
            MobileNo = MobileNo.Replace("'", "''");

        if (ExtraMobileNo != null)
            ExtraMobileNo = ExtraMobileNo.Replace("'", "''");

        string where;
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        where = "";

        //Name
        if (RegistrationName != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.FirstName Like '%" + RegistrationName + "%'";
        }

        if (MobileNo != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.MobileNo Like '%" + MobileNo + "%'";
        }
        if (ExtraMobileNo != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.ExtraMobileNo Like '%" + ExtraMobileNo + "%'";
        }
        if (SchoolName != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.SchoolName Like '%" + SchoolName.Replace("'", "''") + "%'";
        }
        if (City != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.City Like '%" + City.Replace("'", "''") + "%'";
        }
        if (StandardId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.StandardId = '" + StandardId + "'";
        }

        if (DivisionTextListId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.DivisionTextListId = '" + DivisionTextListId + "'";
        }

        if (IsDeActive != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.IsDeActive = '" + Convert.ToByte(IsDeActive) + "'";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }

        if (AllRecords == false)
        {
            sqlCmd.CommandText = "select TOP 30 a.RegistrationId,a.FirstName as StudentName,t.[text] as Standard,td.[text] as Division,a.MobileNo,a.ExtraMobileNo,a.SchoolName,a.City,case when a.IsDeactive = 1 then 'Yes' else 'No' end as IsDeActive," +
                                " a.InsertedOn,a.LastUpdatedOn,d.[Name] as JobProfile " +
                                " from Registration a " +
                                " inner join TextLists t on t.TextListId = a.StandardId" +
                                " left join TextLists td on td.TextListId = a.DivisionTextListId" +
                                " left join FitToJob..JobOfferings j on j.JobOfferingId = a.JobOfferingId   "+
                                " left join Subs d on d.SubId = j.DesignationId " +
                                where +
                                " Order BY InsertedOn desc";
        }
        else
        {
            sqlCmd.CommandText = "select a.RegistrationId,a.FirstName as StudentName,t.[text] as Standard,td.[text] as Division,a.MobileNo,a.ExtraMobileNo,a.SchoolName,a.City,case when a.IsDeactive = 1 then 'Yes' else 'No' end as IsDeActive" +
                                " ,a.StandardId,a.InsertedOn,a.LastUpdatedOn " +
                                " from Registration a " +
                                " inner join TextLists t on t.TextListId = a.StandardId" +
                                " left join TextLists td on td.TextListId = a.DivisionTextListId" +
                                where +
                                " Order BY InsertedOn desc";
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }


    public int CountStudent(string RegistrationName, string MobileNo, string StandardId, string DivisionTextListId, bool? IsDeActive, bool AllRecords, string SchoolName, string City)
    {
        if (RegistrationName != null)
            RegistrationName = RegistrationName.Replace("'", "''");

        if (MobileNo != null)
            MobileNo = MobileNo.Replace("'", "''");

        string where;
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        where = "";

        //Name
        if (RegistrationName != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.FirstName Like '%" + RegistrationName + "%'";
        }

        if (MobileNo != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.MobileNo Like '%" + MobileNo + "%'";
        }
        if (SchoolName != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.SchoolName Like '%" + SchoolName.Replace("'", "''") + "%'";
        }
        if (City != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.City Like '%" + City.Replace("'", "''") + "%'";
        }
        if (StandardId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.StandardId = '" + StandardId + "'";
        }

        if (DivisionTextListId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.DivisionTextListId = '" + DivisionTextListId + "'";
        }

        if (IsDeActive != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.IsDeActive = '" + Convert.ToByte(IsDeActive) + "'";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }


        sqlCmd.CommandText = "select count(*)  CountStudent " +
                            " from Registration a " +
                            " inner join TextLists t on t.TextListId = a.StandardId" +
                            where +
                            "  ";


        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        int Sr = 0;

        if (sqlCmd.ExecuteScalar() != null)
            Sr = Convert.ToInt16(sqlCmd.ExecuteScalar());

        _generalDAL.CloseSQLConnection();

        return Sr;

    }

    public DataTable Students(string StandardId, string RegistrationName, string SchoolName, string City, bool AllRecords)
    {
        string where;
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        where = "";

        if (RegistrationName != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.FirstName Like '%" + RegistrationName + "%'";
        }

        if (SchoolName != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.SchoolName Like '%" + SchoolName.Replace("'", "''") + "%'";
        }

        if (City != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.City Like '%" + City.Replace("'", "''") + "%'";
        }
        if (StandardId != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.StandardId = '" + StandardId + "'";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }

        if (AllRecords == false)
        {
            sqlCmd.CommandText = "select TOP 30 a.RegistrationId,a.FirstName as StudentName,t.[text] as Standard,a.MobileNo,a.SchoolName,a.City,case when a.IsDeactive = 1 then 'Yes' else 'No' end as IsDeActive," +
                                " a.InsertedOn,a.LastUpdatedOn " +
                                " from Registration a " +
                                " inner join TextLists t on t.TextListId = a.StandardId" +
                                where +
                                " Order BY InsertedOn desc";
        }
        else
        {
            sqlCmd.CommandText = "select a.RegistrationId,a.FirstName as StudentName,t.[text] as Standard,a.MobileNo,a.SchoolName,a.City,case when a.IsDeactive = 1 then 'Yes' else 'No' end as IsDeActive" +
                                " ,a.StandardId,a.InsertedOn,a.LastUpdatedOn " +
                                " from Registration a " +
                                " inner join TextLists t on t.TextListId = a.StandardId" +
                                where +
                                " Order BY InsertedOn desc";
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }
}
