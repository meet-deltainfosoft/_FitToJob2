using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public class FormDAL
{
    private GeneralDAL _generalDAL;

	public FormDAL()
	{
        _generalDAL = new GeneralDAL();
    }

    ~FormDAL()
    {
        _generalDAL = null;
    }

    public FormDTO Select(string formId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        FormDTO formDTO = new FormDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT a.*,b.[Text] 'ModuleName'" +
                             " FROM Forms a" +
                             " LEFT JOIN TextLists b ON a.ModuleTextListId = b.TextListId" +
                             " WHERE a.FormId='" + formId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            formDTO.FormId = sqlDr["FormId"].ToString();
            formDTO.ModuleTextListId = sqlDr["ModuleTextListId"].ToString();
            formDTO.ModuleName = sqlDr["ModuleName"].ToString(); 
            formDTO.Name = sqlDr["Name"].ToString();

            //Desc
            if (sqlDr["Desc"] != DBNull.Value)
                formDTO.Desc = sqlDr["Desc"].ToString();
            else
                formDTO.Desc = null;
        }

        sqlDr.Close();

        _generalDAL.CloseSQLConnection();

        return formDTO;
    }

    public void Insert(FormDTO formDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;

        try
        {
            //Escape Single Quote
            //Name
            formDTO.Name = formDTO.Name.Replace("'", "''");

            //Desc
            if (formDTO.Desc != null)
                formDTO.Desc = formDTO.Desc.Replace("'", "''");
            //Escape Single Quote

            sqlCmd.CommandText = "INSERT INTO Forms (FormId,ModuleTextListId,Name,[Desc])" +
                                 " VALUES (NewId(),'" + formDTO.ModuleTextListId + "','" + formDTO.Name + "'," + 
                                 ((formDTO.Desc == null) ? "NULL" : "'" + formDTO.Desc + "'") + ")";

            sqlCmd.ExecuteNonQuery();

            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public void Update(FormDTO formDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;

        try
        {
            //Escape Single Quote
            //Name
            formDTO.Name = formDTO.Name.Replace("'", "''");

            //Desc
            if (formDTO.Desc != null)
                formDTO.Desc = formDTO.Desc.Replace("'", "''");
            //Escape Single Quote

            sqlCmd.CommandText = " UPDATE Forms SET " +
                                 " ModuleTextListId='" + formDTO.ModuleTextListId + "'" +
                                 ",Name='" + formDTO.Name + "'" +
                                 ",[Desc]=" + ((formDTO.Desc == null) ? "NULL" : "'" + formDTO.Desc + "'") + 
                                 " WHERE FormId='" + formDTO.FormId + "'";

            sqlCmd.ExecuteNonQuery();

            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public void Delete(string formId)
    {
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        try
        {
            sqlCmd.CommandText = "DELETE FROM Forms WHERE FormId='" + formId + "'";
            sqlCmd.ExecuteNonQuery();

            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public bool NameExists(string name)
    {
        //Escape Single Quote
        name = name.Replace("'", "''");
        //Escape single Quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM Forms WHERE Name='" + name + "'";

        if (Convert.ToInt32(sqlCmd.ExecuteScalar()) > 0)
        {
            _generalDAL.CloseSQLConnection();
            return true;
        }
        else
        {
            _generalDAL.CloseSQLConnection();
            return false;
        }
    }

    public bool NameExists(string name, string excludeFormId)
    {
        //Escape Single Quote
        name = name.Replace("'", "''");
        //Escape Single Quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM Forms WHERE Name='" + name + "' AND NOT FormId='" + excludeFormId + "'";

        if (Convert.ToInt32(sqlCmd.ExecuteScalar()) > 0)
        {
            _generalDAL.CloseSQLConnection();
            return true;
        }
        else
        {
            _generalDAL.CloseSQLConnection();
            return false;
        }
    }

    public string IsReferenced(string formId)
    {
        string strRef = "";
        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        try
        {
            if (formId != null)
            {
                strRef += _generalDAL.IsReferenced("Forms", "FormId", formId, sqlCmd, null);
            }
            _generalDAL.CloseSQLConnection();
            return strRef;
            }
            //Add Catch
        catch
        {
            _generalDAL.CloseSQLConnection();
            return "";
            throw new Exception();
        }
        //

    }
}
