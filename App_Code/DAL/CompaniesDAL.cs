using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class CompaniesDAL
{
    private GeneralDAL _generalDAL;

    public CompaniesDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~CompaniesDAL()
    {
        _generalDAL = null;
    }

    public DataTable Companies(string name)
    {
        try
        {
            //Escape single quote
            if (name != null)
                name = name.Replace("'", "''");
            //Escape single quote

            string where;
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            where = "";

            //Name
            if (name != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " a.Name LIKE '" + name + "%'";
            }

            if (where != "")
            {
                where = " WHERE " + where;
            }


            _generalDAL.OpenSQLConnection();

            sqlCmd.CommandText = "SELECT a.CompanyId,a.Name,isnull(a.AddressLine1 + ',' ,'') + isnull(a.AddressLine2,'') as Address,a.City,a.State,a.Country " +
                                 ",a.InsertedOn,a.LastUpdatedOn FROM Company a" +
                                 where + " ORDER BY Name";

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

    public DataTable ExportToExcel(string name)
    {
        try
        {
            //Escape single quote
            if (name != null)
                name = name.Replace("'", "''");
            //Escape single quote

            string where;
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            where = "";

            //Name
            if (name != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " a.Name LIKE '" + name + "%'";
            }

            if (where != "")
            {
                where = " WHERE " + where;
            }


            _generalDAL.OpenSQLConnection();

            ////sqlCmd.CommandText = "SELECT a.CompanyId,a.Name,isnull(a.AddressLine1 + ',' ,'') + isnull(a.AddressLine2,'') as Address, " +
            ////                     "a.City,a.State,a.Country ,c.Name as Currency,a.PINCode,a.MobileNo,a.TelephoneNos,a.FaxNos,a.EmailIDs, " +
            ////                     "a.TINLSTNo,a.TINCSTNo,a.PanNo,a.VATNo,a.ExciseRegNo,a.ServiceTaxRegn,a.RangeDetail,a.CommissionRate, " +
            ////                     " a.ServiceEmailId,a.BankName,a.ACNo,a.BranchName,a.IFSCCode,a.Division,a.DefaultTextListId,a.CINNO, " +
            ////                     " lc.Name as DefaultLocation,l.Name as Ledger,t.[Text] as DivisionName, " +
            ////                     " a.InsertedOn,a.LastUpdatedOn,u1.FirstName + ' ' + u1.LastName as InsertedByUserName, " +
            ////                     " u2.FirstName + ' ' + u2.LastName as  LastUpdatedByUserName " +
            ////                     " FROM Company a " +
            ////                     " Left join Lgrs l on l.LgrId = a.LgrId " +
            ////                     " Left join TextLists t on t.TextListId = a.DivTextListId " +
            ////                     " Left join Crncys c on c.CrncyId = a.CrncyId " +
            ////                     " Left join Locs lc on lc.LocId = a.LocId " +
            ////                     " Left join Users u1 on u1.UserId = a.InsertedByUserId " +
            ////                     " Left join Users u2 on u2.UserId = a.LastUpdatedByUserID " +
            ////                     where + " ORDER BY a.Name ";

            sqlCmd.CommandText = " SELECT a.CompanyId,a.Name,isnull(a.AddressLine1 + ',' ,'') + isnull(a.AddressLine2,'') as Address, " +
                                 " a.City,a.State,a.Country ,a.PINCode,a.MobileNo,a.TelephoneNos,a.FaxNos,a.EmailIDs, " +
                                 " a.InsertedOn,a.LastUpdatedOn,u1.FirstName + ' ' + u1.LastName as InsertedByUserName, " +
                                 " u2.FirstName + ' ' + u2.LastName as  LastUpdatedByUserName " +
                                 " FROM Company a " +
                                 " Left join Users u1 on u1.UserId = a.InsertedByUserId " +
                                 " Left join Users u2 on u2.UserId = a.LastUpdatedByUserID " +
                                 where + " ORDER BY a.Name ";

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
