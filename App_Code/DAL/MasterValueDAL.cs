using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

public class MasterValueDAL
{
    private GeneralDAL _generalDAL;

    public MasterValueDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~MasterValueDAL()
    {
        _generalDAL = null;
    }

    public DataTable Groups()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = " SELECT DISTINCT [Group]'Group1',[Group]" +
                             " FROM TextLists " +
                             " UNION" +
                             " SELECT DISTINCT [Text]'Group1',[Text]" +
                             " FROM TextLists " +
                             " ORDER BY Group1";
        dt.Load(sqlCmd.ExecuteReader());

        _generalDAL.CloseSQLConnection();

        return dt;
    }

    // public DataTable TextList(string group, string SubGrpId)
    //{
    //    SqlCommand sqlCmd = new SqlCommand();
    //    DataTable dtItmNames = new DataTable();

    //    _generalDAL.OpenSQLConnection();

    //    sqlCmd.Connection = _generalDAL.ActiveSQLConnection();

    //    //Added by Pratik sir to load subgroups according to parent subgroup
    //    //sqlCmd.CommandText = " SELECT TextListId, Text FROM TextLists WHERE [Group] = '" + group + "' " +
    //    //                     " and TextListId in (select TextListId from TextLists where SubGrpId = '" + SubGrpId + "') ORDER BY [Text] ASC";


    //    //Added by hiral as subgrps were not loading according to Parent subgroup --for temparory
    //    sqlCmd.CommandText = "SELECT TextListId, Text FROM TextLists WHERE [Group] = '" + group + "' ORDER BY [Text] ASC";
    //    //end 

    //    dtItmNames.Load(sqlCmd.ExecuteReader());

    //    _generalDAL.CloseSQLConnection();

    //    return dtItmNames;
    //}

    public MasterValueDTO Select(string textListId)
    {
        try
        {
            string groupid;
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader sqlDr;
            MasterValueDTO masterValueDTO = new MasterValueDTO();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM TextLists WHERE TextListId='" + textListId + "'";
            sqlDr = sqlCmd.ExecuteReader();

            while (sqlDr.Read())
            {
                masterValueDTO.TextListId = sqlDr["TextListId"].ToString();
                masterValueDTO.Group = sqlDr["Group"].ToString();
                masterValueDTO.Text = sqlDr["Text"].ToString();

                if (sqlDr["Address"] != DBNull.Value)
                    masterValueDTO.Address = sqlDr["Address"].ToString();
                else
                    masterValueDTO.Address = null;
            }

            sqlDr.Close();
            _generalDAL.CloseSQLConnection();

            return masterValueDTO;
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public string Insert(MasterValueDTO masterValueDTO)
    {
        //Escape Single Quote
        masterValueDTO.Group = masterValueDTO.Group.Replace("'", "''");
        masterValueDTO.Text = masterValueDTO.Text.Replace("'", "''");
        //Escape Single Quote

        SqlCommand sqlCmd = new SqlCommand();
        string TextListId = "";

        _generalDAL.OpenSQLConnection();

        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " DECLARE  @TextListId uniqueidentifier;" +
                                 " SET @TextListId = NewId()" +
                                 " INSERT INTO TextLists(TextListId,[Group],[Text],InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId,Address)" +
                                 " VALUES(@TextListId,'" + masterValueDTO.Group + "',N'" + masterValueDTO.Text + "'" +
                                 " ,GETDATE(),GETDATE(),'" + MySession.UserUnique + "','" + MySession.UserUnique + "' " +
                                 " , " + ((masterValueDTO.Address == null) ? "NULL" : "'" + masterValueDTO.Address.ToString().Replace("'", "''") + "'") + " " +
                                 " );" +
                                 "SELECT @TextListId";

            TextListId = sqlCmd.ExecuteScalar().ToString();

            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
        return TextListId;
    }

    public void Update(MasterValueDTO masterValueDTO)
    {
        //Escape Single Quote
        masterValueDTO.Group = masterValueDTO.Group.Replace("'", "''");
        masterValueDTO.Text = masterValueDTO.Text.Replace("'", "''");
        //Escape Single Quote

        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " UPDATE TextLists SET" +
                                 " [Group]='" + masterValueDTO.Group + "'" +
                                 " ,[Text]=N'" + masterValueDTO.Text + "'" +
                                 " ,LastUpdatedOn=GETDATE()" +
                                 " ,LastUpdatedByUserId='" + MySession.UserUnique + "'" +
                                 " , Address = " + ((masterValueDTO.Address == null) ? "NULL" : "'" + masterValueDTO.Address.ToString().Replace("'", "''") + "'") + " " +
                                 " WHERE TextListId='" + masterValueDTO.TextListId + "'";
            sqlCmd.ExecuteNonQuery();
            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public void Delete(string textListId)
    {
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;


            string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..TextLists(TextListId, [Group], [Text], LastUpdatedOn, LastUpdatedByUserId, InsertedOn, InsertedByUserId, Address)" +
                                 " Select TextListId, [Group], [Text], LastUpdatedOn, LastUpdatedByUserId, getdate(),'" + MySession.UserUnique + "', Address " +
                                 " FROM TextLists WHERE TextListId='" + textListId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " DELETE FROM TextLists WHERE TextListId='" + textListId + "'";
            sqlCmd.ExecuteNonQuery();

            _generalDAL.CloseSQLConnection();
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public bool CheckIfExists(string group, string text, string textListId)
    {
        //Escape Single Quote
        group = group.Replace("'", "''");
        text = text.Replace("'", "''");
        //Escape Single Quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = " SELECT COUNT(*) FROM TextLists " +
                             " WHERE [Group]='" + group + "'" +
                             " AND [Text]='" + text + "'" +
                             ((textListId == null) ? "" : " AND TextListId<>'" + textListId + "'");

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

    public string IsReferenced(string textListId, string text)
    {
        string strRef = "";
        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        try
        {
            if (textListId != null)
            {
                strRef += _generalDAL.IsReferenced("TextLists", "CaptionTextListId", textListId, sqlCmd, null);
                strRef += _generalDAL.IsReferenced("TextLists", "CategoryTextListId", textListId, sqlCmd, null);
                strRef += _generalDAL.IsReferenced("TextLists", "RejectionReason1TextListId", textListId, sqlCmd, null);
                strRef += _generalDAL.IsReferenced("TextLists", "RejectionReason2TextListId", textListId, sqlCmd, null);
                strRef += _generalDAL.IsReferenced("TextLists", "ValueTextListId", textListId, sqlCmd, null);
                strRef += _generalDAL.IsReferenced("TextLists", "DivTextListId", textListId, sqlCmd, null);
                strRef += _generalDAL.IsReferenced("TextLists", "VoucherTypeId", textListId, sqlCmd, null);
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
