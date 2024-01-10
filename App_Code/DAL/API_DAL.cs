using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Text;

/// <summary>
/// Summary description for API_DAL
/// </summary>
public class API_DAL
{
    private GeneralDAL _generalDAL;

    public API_DAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~API_DAL()
    {
        _generalDAL = null;
    }

    public DataTable returnDataTable(string sql)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dtItmNames = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = sql;

            dtItmNames.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dtItmNames;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    public void InsertUpdateNonQuery(string query)
    {
        SqlTransaction sqlTrans = null;
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();

            sqlTrans = _generalDAL.BeginTransaction();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Transaction = sqlTrans;
            sqlCmd.CommandTimeout = 300;

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = query;

            sqlCmd.ExecuteNonQuery();

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    public string InsertUpdateWithReturnIdentity(string query)
    {
        SqlTransaction sqlTrans = null;
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();
            string id;

            sqlTrans = _generalDAL.BeginTransaction();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Transaction = sqlTrans;
            sqlCmd.CommandTimeout = 300;

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = query;

            id = sqlCmd.ExecuteScalar().ToString();

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();

            return id;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    public string SMSString(string DBName)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            string SMSString;
            _generalDAL.OpenSQLConnection();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            if (DBName != null && DBName != "")
                sqlCmd.CommandText = " select FacetText from " + DBName + "..Facets where FacetName = 'SMSString' ";
            else
                sqlCmd.CommandText = " select FacetText from Facets where FacetName = 'SMSString' ";
            SMSString = Convert.ToString(sqlCmd.ExecuteScalar());
            _generalDAL.CloseSQLConnection();
            return SMSString;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
}