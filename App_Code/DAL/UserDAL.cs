using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public class UserDAL
{
    private GeneralDAL _generalDAL;

    public UserDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~UserDAL()
    {
        _generalDAL = null;
    }

    public UserDTO Select(string userId, out DataTable dtUserAppRoles)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        UserDTO userDTO = new UserDTO();
        DataTable dt = new DataTable();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT a.* " +
                             " FROM Users a" +
                             " WHERE a.UserId='" + userId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            userDTO.UserId = sqlDr["UserId"].ToString();
            userDTO.UserName = sqlDr["UserName"].ToString();
            userDTO.FirstName = sqlDr["FirstName"].ToString();
            userDTO.LastName = sqlDr["LastName"].ToString();
            userDTO.Password = sqlDr["Password"].ToString();
            userDTO.DeptId = sqlDr["DeptId"].ToString();
            userDTO.IsDisabled = Convert.ToBoolean(sqlDr["IsDisabled"]);
        }

        sqlDr.Close();

        //Get App Role
        //Following Query Returns ALL App Roles from UserAppRoles and not in UserAppRoles
        //First Query only returns the AppRoles which are in UserAppRoles
        //Second Query only return the AppRoles which are not in UserAppRoles
        sqlCmd.CommandText = " SELECT Convert(bit,'True') 'Checked', a.AppRoleId, b.Name 'AppRoleName', b.[Desc]" +
                             " FROM UserAppRoles a" +
                             " INNER JOIN AppRoles b ON a.AppRoleId = b.AppRoleId" +
                             " WHERE a.UserId ='" + userId + "'" +
                             " UNION" +
                             " SELECT Convert(bit,'False') 'Checked', a.AppRoleId, a.Name 'AppRoleName', a.[Desc]" +
                             " FROM AppRoles a" +
                             " WHERE a.AppRoleId NOT IN" +
                             "      (SELECT AppRoleId FROM UserAppRoles WHERE UserId ='" + userId + "')" +
                             " ORDER BY b.Name;";

        dt.Load(sqlCmd.ExecuteReader());

        _generalDAL.CloseSQLConnection();

        dtUserAppRoles = dt;
        return userDTO;
    }

    public void Insert(UserDTO userDTO, ArrayList userLns)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string userId;
        _generalDAL.OpenSQLConnection();

        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            //Escape Single Quote
            userDTO.FirstName = userDTO.FirstName.Replace("'", "''");
            userDTO.LastName = userDTO.LastName.Replace("'", "''");
            userDTO.UserName = userDTO.UserName.Replace("'", "''");
            userDTO.Password = userDTO.Password.Replace("'", "''");

            if (userDTO.DeptId != null)
                userDTO.DeptId = userDTO.DeptId.Replace("'", "''");
            //Escape Single Quote

            sqlCmd.CommandText = "DECLARE  @UserId uniqueidentifier;" +
                                 " SET @UserId = NewId()" +
                                 " INSERT INTO Users_ (UserId,DeptId,FirstName,LastName,UserName,[Password],IsDisabled,InsertedOn,LastUpdatedOn," +
                                 " InsertedByUserId,LastUpdatedByUserId)" +
                                 " VALUES (@UserId," + ((userDTO.DeptId != null) ? "'" + userDTO.DeptId + "'" : "NULL") + ",'" + userDTO.FirstName + "','" + userDTO.LastName + "','" + userDTO.UserName + "'" +
                                 ",'" + userDTO.Password + "','" + userDTO.IsDisabled + "'" +
                                 ",GETDATE(),GETDATE(),NULL,NULL);" +
                                 " SELECT @UserId";

            userId = sqlCmd.ExecuteScalar().ToString();

            //Insert User AppRoles
            foreach (string appRoleId in userLns)
            {
                InsertUserAppRole(userId, appRoleId, sqlCmd);
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

    private void InsertUserAppRole(string userId, string appRoleId, SqlCommand sqlCmd)
    {
        sqlCmd.CommandText = "INSERT INTO UserAppRoles (UserAppRoleId,UserId,AppRoleId,InsertedOn)" +
                             " VALUES (NEWID()" +
                              ",'" + userId + "','" + appRoleId + "'" +
                              ",GETDATE())";
        sqlCmd.ExecuteNonQuery();
    }

    public void Update(UserDTO userDTO, ArrayList userLns)
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
            userDTO.FirstName = userDTO.FirstName.Replace("'", "''");
            userDTO.LastName = userDTO.LastName.Replace("'", "''");
            userDTO.UserName = userDTO.UserName.Replace("'", "''");
            userDTO.Password = userDTO.Password.Replace("'", "''");

            if (userDTO.DeptId != null)
                userDTO.DeptId = userDTO.DeptId.Replace("'", "''");
            //Escape Single Quote

            sqlCmd.CommandText = " UPDATE Users_ SET " +
                                 " FirstName='" + userDTO.FirstName + "'" +
                                 ",LastName='" + userDTO.LastName + "'" +
                                 ",DeptId = " + ((userDTO.DeptId != null && userDTO.DeptId != "") ? "'" + userDTO.DeptId + "'" : "NULL") + "" +
                                 ",[Password]='" + userDTO.Password + "'" +
                                 ",IsDisabled='" + userDTO.IsDisabled + "'" +
                                 ",LastUpdatedOn=GETDATE()" +
                                 " WHERE UserId='" + userDTO.UserId + "'";

            sqlCmd.ExecuteNonQuery();

            //Delete AppRoles
            DeleteUserAppRoles(userDTO.UserId, sqlCmd);

            //Insert App Roles
            foreach (string appRoleId in userLns)
            {
                InsertUserAppRole(userDTO.UserId, appRoleId, sqlCmd);
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

    public void Delete(string userId)
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
            //Delete App Roles
            DeleteUserAppRoles(userId, sqlCmd);

            //Delete User
            sqlCmd.CommandText = "DELETE FROM Users_ WHERE UserId='" + userId + "'";
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

    private void DeleteUserAppRoles(string userId, SqlCommand sqlcmd)
    {
        sqlcmd.CommandText = "DELETE FROM UserAppRoles WHERE UserId='" + userId + "'";
        sqlcmd.ExecuteNonQuery();
    }

    public DataTable GetAllAppRoles()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = " SELECT Convert(bit,'False') 'Checked', a.AppRoleId, a.Name 'AppRoleName', a.[Desc]" +
                             " FROM AppRoles a" +
                              " ORDER BY a.Name;";


        dt.Load(sqlCmd.ExecuteReader());
        _generalDAL.CloseSQLConnection();

        return dt;
    }

    public bool UserNameExists(string userName)
    {
        //Escape single quote
        userName = userName.Replace("'", "''");
        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM Users_ WHERE UserName='" + userName + "'";

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

    public bool UserNameExists(string userName, string userId)
    {
        //Escape single quote
        userName = userName.Replace("'", "''");
        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM Users_ WHERE UserName='" + userName + "' AND NOT UserId='" + userId + "'";

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

    public bool FirstAndLastNameExists(string firstName, string lastName)
    {
        //Escape single quote
        firstName = firstName.Replace("'", "''");
        lastName = lastName.Replace("'", "''");
        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM Users_ WHERE FirstName='" + firstName + "' AND  LastName='" + lastName + "'";

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

    public bool FirstAndLastNameExists(string firstName, string lastName, string userId)
    {
        //Escape single quote
        firstName = firstName.Replace("'", "''");
        lastName = lastName.Replace("'", "''");
        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM Users_ WHERE FirstName='" + firstName + "' AND  LastName='" + lastName + "' AND NOT UserId='" + userId + "'";

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

    public string IsReferenced(string userId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        string strRef = "";
        _generalDAL.OpenSQLConnection();

        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        try
        {
            if (userId != null)
            {
                strRef += _generalDAL.IsReferenced("Users", "UserId", userId, sqlCmd, null);
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

    //by kinnari
    public DataTable LoadDept()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "select DeptId,Name from depts Order By Name";

        dt.Load(sqlCmd.ExecuteReader());
        _generalDAL.CloseSQLConnection();

        return dt;
    }



}
