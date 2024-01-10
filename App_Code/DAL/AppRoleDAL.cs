using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public class AppRoleDAL
{
    private GeneralDAL _generalDAL;

	public AppRoleDAL()
	{
        _generalDAL = new GeneralDAL();
    }

    ~AppRoleDAL()
    {
        _generalDAL = null;
    }

    public AppRoleDTO Select(string appRoleId, out DataTable dtAppRoleForms)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        AppRoleDTO appRoleDTO = new AppRoleDTO();
        DataTable dt = new DataTable();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT a.* " +
                             " FROM AppRoles a" +
                             " WHERE a.AppRoleId='" + appRoleId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            appRoleDTO.AppRoleId = sqlDr["AppRoleId"].ToString();
            appRoleDTO.Name = sqlDr["Name"].ToString();
            appRoleDTO.Desc = sqlDr["Desc"].ToString();
        }

        sqlDr.Close();

        //Get App Role Form
        //Following Query Returns ALL Forms from App Roles and not in App Roles
        //First Query only returns the Forms which are in App Role Forms
        //Second Query only return the Forms which are not in App Role Form
        sqlCmd.CommandText = "SELECT Convert(bit,'True') 'Checked', a.FormId, b.Name 'FormName', b.[Desc]" +
                             ",c.[Text] 'ModuleName'" +
                             " FROM AppRoleForms a" +
                             " INNER JOIN Forms b ON a.FormId = b.FormId" +
                             " INNER JOIN TextLists c ON b.ModuleTextListId = c.TextListId" +
                             " WHERE a.AppRoleId ='" + appRoleId + "'  and b.[Desc] <> ''" +
                             " UNION" +
                             " SELECT Convert(bit,'False') 'Checked', a.FormId, a.Name 'FormName', a.[Desc]" +
                             ",b.[Text] 'ModuleName'" +
                             " FROM Forms a" +
                             " INNER JOIN TextLists b ON a.ModuleTextListId = b.TextListId" +
                             " WHERE a.FormId NOT IN" +
                             "      (SELECT FormId FROM AppRoleForms WHERE AppRoleId ='" + appRoleId + "')" +
                             " and a.[Desc] <> '' ORDER BY c.[Text],b.Name"; 

        dt.Load(sqlCmd.ExecuteReader());

        _generalDAL.CloseSQLConnection();

        dtAppRoleForms = dt;
        return appRoleDTO;
    }

    public void Insert(AppRoleDTO appRoleDTO, ArrayList appRoleLns)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string appRoleId;
        _generalDAL.OpenSQLConnection();

        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            //Escape Single Quote
            appRoleDTO.Name = appRoleDTO.Name.Replace("'", "''");
            appRoleDTO.Desc = appRoleDTO.Desc.Replace("'", "''");
            //Escape Single Quote

            sqlCmd.CommandText = "DECLARE  @AppRoleId uniqueidentifier;" +
                                 " SET @AppRoleId = NewId()" +
                                 " INSERT INTO AppRoles (AppRoleId,Name,[Desc],InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId)" +
                                 " VALUES (@AppRoleId,'" + appRoleDTO.Name + "','" + appRoleDTO.Desc + "'" +
                                 ",GETDATE(),GETDATE(),NULL,NULL);" +
                                 " SELECT @AppRoleId";

            appRoleId = sqlCmd.ExecuteScalar().ToString();

            foreach (string formId in appRoleLns)
            {
                InsertAppRoleForm(formId, appRoleId, sqlCmd);
            }

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    private void InsertAppRoleForm(string formId, string appRoleId, SqlCommand sqlCmd)
    {
        sqlCmd.CommandText = "INSERT INTO AppRoleForms (AppRoleFormId,AppRoleId,FormId,InsertedOn)" +
                             " VALUES (NEWID()" +
                              ",'" + appRoleId + "','" + formId + "'" +
                              ",GETDATE())";
        sqlCmd.ExecuteNonQuery();
    }

    public void Update(AppRoleDTO appRoleDTO, ArrayList appRoleLns)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            //Escape Single Quote
            appRoleDTO.Name = appRoleDTO.Name.Replace("'", "''");
            appRoleDTO.Desc = appRoleDTO.Desc.Replace("'", "''");
            //Escape Single Quote

            sqlCmd.CommandText = " UPDATE AppRoles SET " +
                                 " Name='" + appRoleDTO.Name + "'" +
                                 ",[Desc]='" + appRoleDTO.Desc + "'" +
                                 ",LastUpdatedOn=GETDATE()" +
                                 " WHERE AppRoleId='" + appRoleDTO.AppRoleId + "'";

            sqlCmd.ExecuteNonQuery();

            //Delete All forms from AppRoleForms
            DeleteAppRoleForms(appRoleDTO.AppRoleId, sqlCmd);

            //Insert App Role Forms
            foreach (string formId in appRoleLns)
            {
                InsertAppRoleForm(formId, appRoleDTO.AppRoleId, sqlCmd);
            }

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public void Delete(string appRoleId)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            //Delete App Role Forms
            DeleteAppRoleForms(appRoleId, sqlCmd);

            //Delete App Role
            sqlCmd.CommandText = "DELETE FROM AppRoles WHERE appRoleId='" + appRoleId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    private void DeleteAppRoleForms(string appRoleId, SqlCommand sqlcmd)
    {
        sqlcmd.CommandText = "DELETE FROM AppRoleForms WHERE AppRoleId='" + appRoleId + "'";
        sqlcmd.ExecuteNonQuery();
    }

    public DataTable GetAllForms()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = " SELECT Convert(bit,'False') 'Checked', a.FormId, a.Name 'FormName', a.[Desc]" +
                             ",b.[Text] 'ModuleName'" +
                             " FROM Forms a" +
                             " INNER JOIN TextLists b ON a.ModuleTextListId = b.TextListId" +
                             " where a.[Desc] <> ''" +
                             " ORDER BY b.[Text],a.Name"; 

        dt.Load(sqlCmd.ExecuteReader());
        _generalDAL.CloseSQLConnection();

        return dt;
    }

    public bool NameExists(string name)
    {
        //Escape single quote
        name = name.Replace("'", "''");
        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM AppRoles WHERE Name='" + name + "'";

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

    public bool NameExists(string name, string appRoleId)
    {
        //Escape single quote
        name = name.Replace("'", "''");
        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM AppRoles WHERE Name='" + name + "' AND NOT AppRoleId='" + appRoleId + "'";

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

    public string IsReferenced(string appRoleId)
    {
        string strRef = "";
        
        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        //Add Try 
        try
        {
            //
            if (appRoleId != null)
            {
                strRef += _generalDAL.IsReferenced("AppRoles", "AppRoleId", appRoleId, sqlCmd, "'AppRoleForms'");
            }
            //add Conn Close
            _generalDAL.CloseSQLConnection();
            //

            return strRef;
            //
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
