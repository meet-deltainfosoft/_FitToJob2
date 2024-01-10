﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Data;
using System.Text;

public class API_BLL
{
    API_DAL _aPI_DAL = new API_DAL();

    public DataTable returnDataTable(string sql)
    {
        try
        {
            return _aPI_DAL.returnDataTable(sql);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void InsertUpdateNonQuery(string sql)
    {
        try
        {
            _aPI_DAL.InsertUpdateNonQuery(sql);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string InsertUpdateWithReturnIdentity(string sql)
    {
        try
        {
            return _aPI_DAL.InsertUpdateWithReturnIdentity(sql);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string SMSString(string DBName)
    {
        try
        {
            return _aPI_DAL.SMSString(DBName);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}