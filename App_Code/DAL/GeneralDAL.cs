using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Web.Script.Serialization;
using System.Security.AccessControl;
using System.Security.Cryptography;
//using System.Runtime.Caching;


public class GeneralDAL
{
    private SqlConnection _sqlConn;

    public GeneralDAL()
    {
        _sqlConn = new SqlConnection();
    }

    ~GeneralDAL()
    {
        _sqlConn = null;
    }

    public void OpenSQLConnection()
    {
            //_sqlConn.ConnectionString = @"Persist Security Info=False;User ID=sa;Password=ERP@1234;Initial Catalog=DukeFitToJobExam;Data Source=ERP\NEW2012"; // Live
        _sqlConn.ConnectionString = @"Persist Security Info=False;User ID=sa;Password=sqlserver@123;Initial Catalog=MyTime_Organogram;Data Source=DIPLWS1\SQLPLN"; // local

        _sqlConn.Open();
    }

    public SqlConnection ActiveSQLConnection()
    {
        return _sqlConn;
    }

    internal SqlTransaction BeginTransaction()
    {
        return _sqlConn.BeginTransaction();
    }

    public void CloseSQLConnection()
    {
        _sqlConn.Close();
    }

    #region "Design"

    public DataTable Villages()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtVillages = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT VillageId,Name FROM VillageMaster ORDER BY Name ASC";

        dtVillages.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtVillages;
    }

    public DataTable Departments()
    {
        SqlCommand sqlcmd = new SqlCommand();
        DataTable dtDepartment = new DataTable();

        OpenSQLConnection();

        sqlcmd.Connection = ActiveSQLConnection();
        sqlcmd.CommandText = "SELECT DeptId,Convert(varchar(3),No) + ' ' + [Name] FROM Depts ORDER By No";

        dtDepartment.Load(sqlcmd.ExecuteReader());

        CloseSQLConnection();

        return dtDepartment;
    }

    public DataTable DrgRevReasons()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtDrgRevReasons = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT * FROM DrgRevReasons ORDER BY Name ASC";

        dtDrgRevReasons.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtDrgRevReasons;
    }

    public DataTable PumpModels()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtPumpModels = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT PumpModelId,[Name] FROM PumpModels ORDER BY Name ASC";

        dtPumpModels.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtPumpModels;
    }


    #endregion

    #region "MatlGrps"

    public DataTable MatlGrps()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtMatlGrps = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT * FROM MatlGrps ORDER BY Name ASC";

        dtMatlGrps.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtMatlGrps;
    }

    public DataTable MatlGrpsExcludingChildrenOf(string matlGrpId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "WITH MyCTE AS" +
                             " (" +
                             " SELECT a.MatlGrpId FROM MatlGrps a" +
                             " WHERE a.MatlGrpId='" + matlGrpId + "'" +
                             " UNION ALL" +
                             " SELECT b.MatlGrpId FROM MatlGrps b" +
                             " INNER JOIN MyCTE c ON c.MatlGrpId=b.ParentMatlGrpId" +
                             ")" +
                             " SELECT a.*" +
                             " FROM MatlGrps a" +
                             " WHERE MatlGrpId NOT IN (SELECT * FROM MyCTE)" +
                             " ORDER BY a.Name";
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }

    #endregion

    #region "MatlGrades"

    public DataTable MatlGrades()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtMatls = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT MatlGradeId,[Name] FROM MatlGrades ORDER BY Name ASC";

        dtMatls.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtMatls;
    }

    public DataTable MatlGrades(string MatlGrpId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtMatlGrades = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT MatlGradeId,[Name] FROM MatlGrades WHERE MatlGrpId='" + MatlGrpId + "' ORDER BY Name ASC";

        dtMatlGrades.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtMatlGrades;
    }

    #endregion

    #region "SubGrps"
    //'ElementItmGrps' are ItmGrps which contain/can contain Drg(s) or Itm(s). Meaning ItmGrps where 'DrgNoPrfx'
    //or 'ItmCodePrfx' is not null.

    //'DrgItmGrps' are ItmGrps which contain/can contain Drg(s). Meaning ItmGrps where 'DrgNoPrfx'
    //is not null.

    //'ItmItmGrps' are ItmGrps which contain/can contain Itms(s). Meaning ItmGrps where 'ItmCodePrfx'
    //is not null.

    //'NodeItmGrps' are ItmGrps which cannot contain Drgs(s) or Itm(s). Meaning ItmGrps where 'DrgNoPrfx'
    //and 'ItmCodePrfx' is null.

    public DataTable SubGrps()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtSubGrps = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT SubGrpId, [Name] FROM SubGrps ORDER BY Name ASC";

        dtSubGrps.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtSubGrps;
    }


    #endregion

    #region "Locs"

    public DataTable Locs()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtLocs = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT LocId,[Name] FROM Locs ORDER BY Name ASC";

        dtLocs.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtLocs;
    }

    public DataTable LocsExcludingChildrenOf(string locId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtLocs = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "WITH MyCTE AS" +
                             " (" +
                             " SELECT a.LocId FROM Locs a" +
                             " WHERE a.LocId='" + locId + "'" +
                             " UNION ALL" +
                             " SELECT b.LocId FROM Locs b" +
                             " INNER JOIN MyCTE c ON c.LocId=b.ParentLocId" +
                             ")" +
                             " SELECT a.*" +
                             " FROM Locs a" +
                             " WHERE LocId NOT IN (SELECT * FROM MyCTE)" +
                             " ORDER BY a.Name";

        dtLocs.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtLocs;
    }

    #endregion

    #region "UMs"

    public DataTable UMs()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtUMs = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT UMId,[Name] FROM UMs ORDER BY Name ASC";

        dtUMs.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtUMs;
    }

    #endregion

    #region "Itms"

    public DataTable Companies()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtCompanies = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT CompanyId,[Name] FROM Companies ORDER BY Name ASC";

        dtCompanies.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtCompanies;
    }

    public DataTable ItmNames()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtItmNames = new DataTable();

        //OpenSQLConnection();

        //sqlCmd.Connection = ActiveSQLConnection();
        //sqlCmd.CommandText = "SELECT ItmNameId, [Name] FROM ItmNames ORDER BY Name ASC";

        //dtItmNames.Load(sqlCmd.ExecuteReader());

        //CloseSQLConnection();

        return dtItmNames;
    }

    public DataTable DrgItmNames()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtDrgItmNames = new DataTable();

        //OpenSQLConnection();

        //sqlCmd.Connection = ActiveSQLConnection();
        //sqlCmd.CommandText = "SELECT ItmNameId, [Name] FROM ItmNames WHERE NOT DrgNoPrfx IS NULL ORDER BY [Name]";

        //dtDrgItmNames.Load(sqlCmd.ExecuteReader());

        //CloseSQLConnection();

        return dtDrgItmNames;
    }

    public DataTable GetItm(string itmId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtItm = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT * FROM vwItms" +
                            " WHERE ItmId='" + itmId + "'";

        dtItm.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtItm;
    }

    public DataTable ItmAtribs()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtItmAtribs = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT * FROM ItmAtribs ORDER BY Name ASC";

        dtItmAtribs.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtItmAtribs;
    }

    public DataTable ItmAtribs(string itmNameId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT a.ItmAtribId1,a.ItmAtribId2,a.ItmAtribId3,a.ItmAtribId4,a.ItmAtribId5,a.ItmAtribId6" +
                             ",b.Name AS ItmAtribName1, c.Name AS ItmAtribName2, d.Name AS ItmAtribName3, e.Name AS ItmAtribName4" +
                             ",f.Name AS ItmAtribName5, g.Name AS ItmAtribName6" +
                             " FROM ItmNames a " +
                             " LEFT JOIN ItmAtribs b ON a.ItmAtribId1 = b.ItmAtribId" +
                             " LEFT JOIN ItmAtribs c ON a.ItmAtribId2 = c.ItmAtribId" +
                             " LEFT JOIN ItmAtribs d ON a.ItmAtribId3 = d.ItmAtribId" +
                             " LEFT JOIN ItmAtribs e ON a.ItmAtribId4 = e.ItmAtribId" +
                             " LEFT JOIN ItmAtribs f ON a.ItmAtribId5 = f.ItmAtribId" +
                             " LEFT JOIN ItmAtribs g ON a.ItmAtribId6 = g.ItmAtribId" +
                             " WHERE ItmNameId='" + itmNameId + "'";

        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }

    public DataTable ItmAtribVals(string itmAtribId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT ItmAtribValId, Val FROM ItmAtribVals" +
                             " WHERE ItmAtribId='" + itmAtribId + "'";
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }

    public string ItmAtribValId(string itmAtribVal, string itmAtribId, SqlCommand sqlCmd)
    {
        string itmAtribValId;

        //Escape Single Quote
        //Commneted cause of already done
        //itmAtribVal = itmAtribVal.Replace("'", "''");
        //Escape Single Quote

        SqlDataReader sqlDr;

        sqlCmd.CommandText = "SELECT ItmAtribValId FROM ItmAtribVals" +
                             " WHERE ItmAtribId='" + itmAtribId + "' AND Val='" + itmAtribVal + "'";

        sqlDr = sqlCmd.ExecuteReader();

        if (sqlDr.Read())
        {
            if (sqlDr["ItmAtribValId"].ToString().Trim() != "")
                itmAtribValId = sqlDr["ItmAtribValId"].ToString();
            else
                itmAtribValId = "";
        }
        else
        {
            itmAtribValId = "";
        }

        sqlDr.Close();

        return itmAtribValId;
    }

    public string InsertItmAtribVal(string itmAtribVal, string itmAtribId, SqlCommand sqlCmd)
    {
        string itmAtribValId;

        ////Escape Single Quote
        //itmAtribVal = itmAtribVal.Replace("'", "''");
        ////Escape Single Quote

        itmAtribValId = Guid.NewGuid().ToString();

        sqlCmd.CommandText = "INSERT INTO ItmAtribVals(ItmAtribValId,ItmAtribId,Val)" +
                             " VALUES('" + itmAtribValId + "','" + itmAtribId + "','" + itmAtribVal + "')";

        sqlCmd.ExecuteNonQuery();

        return itmAtribValId;
    }


    #endregion

    #region "ExpGrps"
    public DataTable ExpGrps()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtExpGrps = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT ExpGrpId, [Name] FROM ExpGrps ORDER BY Name ASC";

        dtExpGrps.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtExpGrps;
    }

    public DataTable ExpGrpsExcludingChildrenOf(string ExpGrpId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtExpGrps = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "WITH MYCTE AS" +
                            " (" +
                            " SELECT ExpGrpId" +
                            " FROM ExpGrps a" +
                            " WHERE a.ExpGrpId ='" + ExpGrpId + "'" +
                            " UNION ALL" +
                            " SELECT b.ExpGrpId" +
                            " FROM ExpGrps b" +
                            " INNER JOIN MYCTE c ON b.ParentExpGrpId=c.ExpGrpId" +
                            " )" +
                            " SELECT a.*" +
                            " FROM ExpGrps a" +
                            " WHERE a.ExpGrpId NOT IN(SELECT * FROM MYCTE) ORDER BY a.Name";

        dtExpGrps.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtExpGrps;
    }
    #endregion

    #region "TextLists"

    public DataTable TextList(string group)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtItmNames = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();

        if (ConfigurationManager.AppSettings["RightsBased"] != null)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["RightsBased"]))
            {
                if (group.ToString().ToUpper() == "Standard".ToString().ToUpper())
                {
                    if (MySession.UserID.ToString().ToUpper() == "aaa".ToString().ToUpper())
                    {
                        sqlCmd.CommandText = "SELECT TextListId, Text FROM TextLists WHERE [Group]='" + group + "' ORDER BY [Text] ASC";
                    }
                    else
                    {
                        sqlCmd.CommandText = " SELECT t.TextListId, t.Text FROM TextLists t " +
                                             " inner join EmpCredentialCourseLns el on el.CourseLnId = t.TextListId " +
                                             " WHERE t.[Group]='" + group + "' and el.EmpId = '" + MySession.UserUnique.ToString() + "' " +
                                             " ORDER BY t.[Text] ASC";
                    }
                }
                else
                {
                    sqlCmd.CommandText = "SELECT TextListId, Text FROM TextLists WHERE [Group]='" + group + "' ORDER BY [Text] ASC";
                }
            }
            else
            {
                sqlCmd.CommandText = "SELECT TextListId, Text FROM TextLists WHERE [Group]='" + group + "' ORDER BY [Text] ASC";
            }
        }
        else
        {
            sqlCmd.CommandText = "SELECT TextListId, Text FROM TextLists WHERE [Group]='" + group + "' ORDER BY [Text] ASC";
        }

        dtItmNames.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtItmNames;
    }

    #endregion

    #region "Depts"

    public DataTable Depts()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtDepts = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT a.DeptId, a.Name" +
                             " FROM Depts a" +
                             " ORDER BY a.Name";

        dtDepts.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtDepts;
    }

    public DataTable Depts(string UserId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtDepts = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT a.DeptId, a.Name" +
                             " FROM Depts a LEFT JOIN Users b on b.DeptId = a.DeptId WHERE b.UserName = '" + UserId + "'" +
                             " ORDER BY a.Name";

        dtDepts.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtDepts;
    }

    public DataTable HODs()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtHODs = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT DeptId, b.[Text] +' | '+ a.Name +' | '+ a.HOD" +
                             " FROM Depts a" +
                             " INNER JOIN TextLists b ON a.DivTextListId = b.TextListId" +
                             " ORDER BY b.[Text],a.Name";

        dtHODs.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtHODs;
    }

    #endregion

    #region "Crncys"

    public DataTable Crncys()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtCrncys = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT * FROM Crncys ORDER BY Name ASC";

        dtCrncys.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtCrncys;
    }

    #endregion

    #region "Lgrs & lgrGrps"

    public DataTable LgrGrpAndItsChildrens(string lgrGrpId)
    {
        DataTable dt = new DataTable();

        return dt;
    }

    public DataTable CustomerLgrGrpAndItsChildrens()
    {
        DataTable dt = new DataTable();
        SqlCommand sqlCmd = new SqlCommand();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = " WITH MyCTE AS" +
                             " (" +
                             " SELECT a.LgrGrpId,a.Name FROM LgrGrps a" +
                             " WHERE a.LgrGrpId=(SELECT LgrGrpId FROM LgrGrps WHERE Name='Sundry Debtors')" +
                             " UNION ALL" +
                             " SELECT b.LgrGrpId,b.Name FROM LgrGrps b" +
                             " INNER JOIN MyCTE c ON c.LgrGrpId=b.ParentLgrGrpId" +
                             " )" +
                             " SELECT d.* FROM MyCTE d" +
                             " ORDER BY d.Name";
        dt.Load(sqlCmd.ExecuteReader());
        CloseSQLConnection();

        return dt;
    }

    public DataTable VendorLgrGrpAndItsChildrens()
    {
        DataTable dt = new DataTable();
        SqlCommand sqlCmd = new SqlCommand();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = " WITH MyCTE AS" +
                             " (" +
                             " SELECT a.LgrGrpId,a.Name FROM LgrGrps a" +
                             " WHERE a.LgrGrpId=(SELECT LgrGrpId FROM LgrGrps WHERE Name='Sundry Creditors')" +
                             " UNION ALL" +
                             " SELECT b.LgrGrpId,b.Name FROM LgrGrps b" +
                             " INNER JOIN MyCTE c ON c.LgrGrpId=b.ParentLgrGrpId" +
                             " )" +
                             " SELECT d.* FROM MyCTE d" +
                             " ORDER BY d.Name";
        dt.Load(sqlCmd.ExecuteReader());
        CloseSQLConnection();

        return dt;
    }
    public DataTable CustomerVendorDetails(string lgrId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        if (lgrId.Trim() != "")
        {
            sqlCmd.CommandText = " SELECT a.LgrId,b.LgrAddressId,a.Name" +
                                 " ,b.StreetAddress + ISNULL(', '+b.City,'') + ISNULL(', '+b.StateOrProvince,'') + ISNULL(', '+b.Country,'') + ISNULL(', '+ b.ZipOrPostalCode,'') AS [Address]" +
                                 " ,c.CrncyId, c.Symbol AS CrncySymbol, c.DecimalPlaces" +
                                 " FROM Lgrs a" +
                                 " INNER JOIN LgrAddresses b ON a.LgrId=b.LgrId" +
                                 " INNER JOIN Crncys c ON a.CrncyId = c.CrncyId" +
                                 " WHERE a.LgrId='" + lgrId + "'";
        }
        else
        {
            sqlCmd.CommandText = " SELECT a.LgrId,b.LgrAddressId,a.Name" +
                                " ,b.StreetAddress + ISNULL(', '+b.City,'') + ISNULL(', '+b.StateOrProvince,'') + ISNULL(', '+b.Country,'') + ISNULL(', '+ b.ZipOrPostalCode,'') AS [Address]" +
                                " ,c.CrncyId, c.Symbol AS CrncySymbol, c.DecimalPlaces" +
                                " FROM Lgrs a" +
                                " INNER JOIN LgrAddresses b ON a.LgrId=b.LgrId" +
                                " INNER JOIN Crncys c ON a.CrncyId = c.CrncyId";

        }
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }

    //Changed from LgrAddressId to LgrId By Birwa 18-11-2011 Ref SM
    public DataTable CustomerVendorDetail(string lgrId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = " SELECT a.LgrId,b.LgrAddressId,a.Name" +
                             " ,b.StreetAddress + ISNULL(', '+b.City,'') + ISNULL(', '+b.StateOrProvince,'') + ISNULL(', '+b.Country,'') + ISNULL(', '+ b.ZipOrPostalCode,'') AS [Address]" +
                             " ,c.CrncyId, c.Symbol AS CrncySymbol, c.DecimalPlaces" +
                             " FROM Lgrs a" +
                             " INNER JOIN LgrAddresses b ON a.LgrId=b.LgrId" +
                             " INNER JOIN Crncys c ON a.CrncyId = c.CrncyId" +
                             " WHERE b.LgrId='" + lgrId + "'";
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable CustomerVendorDetailIDWise(string lgrId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = " SELECT a.LgrId,b.LgrAddressId,a.Name" +
                             " ,b.StreetAddress + ISNULL(', '+b.City,'') + ISNULL(', '+b.StateOrProvince,'') + ISNULL(', '+b.Country,'') + ISNULL(', '+ b.ZipOrPostalCode,'') AS [Address]" +
                             " ,c.CrncyId, c.Symbol AS CrncySymbol, c.DecimalPlaces" +
                             " FROM Lgrs a" +
                             " INNER JOIN LgrAddresses b ON a.LgrId=b.LgrId" +
                             " INNER JOIN Crncys c ON a.CrncyId = c.CrncyId" +
                             " WHERE a.LgrId='" + lgrId + "'";
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    #endregion

    #region "App Params"

    public string AppParam(string name)
    {
        SqlCommand sqlCmd = new SqlCommand();
        string value;

        OpenSQLConnection();
        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT Value FROM AppParams WHERE Name='" + name + "'";

        value = Convert.ToString(sqlCmd.ExecuteScalar());

        CloseSQLConnection();

        return value;
    }

    public string AppParam(string name, SqlCommand sqlCmd)
    {
        string value;

        sqlCmd.CommandText = "SELECT Value FROM AppParams WHERE Name='" + name + "'";

        value = sqlCmd.ExecuteScalar().ToString();

        return value;
    }

    #endregion

    #region "Voucher Types"

    public DataTable VoucherTypes(string parentVoucherType)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtVoucherTypes = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT a.*" +
                             " FROM VoucherTypes a" +
                             " INNER JOIN TextLists b ON a.ParentVoucherTypeId = b.TextListId" +
                             " WHERE b.[Text]='" + parentVoucherType + "'" +
                             " ORDER BY a.Name ASC";

        dtVoucherTypes.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtVoucherTypes;

    }

    public string VoucherNo(DateTime date, string voucherTypeId, SqlCommand pSqlCmd)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDR;
        string no;

        //If passed psqlCmd is null means This function is called from UI while data entry, then it need to open connection and return no
        //If Passed pSqlCmd is not null means, this function is called at time of Insert , So it should return no and  increment the count
        if (pSqlCmd == null)
        {
            OpenSQLConnection();
            sqlCmd.Connection = ActiveSQLConnection();
        }
        else
        {
            sqlCmd = pSqlCmd;
        }

        //Sql Query to Return no
        string sql = "";
        if (voucherTypeId == "FA284E4D-A63B-4AF6-AF43-C75518D68DCB")
        {
            sql = "SELECT TOP 1 b.Prfx + CASE WHEN b.Ct >= 0 THEN RIGHT('0000'+Convert(VARCHAR,b.Ct+1),4) ELSE Convert(VARCHAR,b.StartFromCt) END + b.Sufx AS No" +
                        " FROM VoucherTypes a" +
                        " INNER JOIN VoucherNos b ON a.VoucherTypeId = b.VoucherTypeId" +
                        " WHERE a.VoucherTypeId='" + voucherTypeId + "'" +
                        " AND b.WEFDt<='" + date.ToString("dd-MMM-yyyy") + "'" +
                        " ORDER BY b.WEFDt DESC";
        }
        else
        {
            sql = "SELECT TOP 1 b.Prfx + CASE WHEN b.Ct >= 0 THEN  Convert(VARCHAR,b.Ct+1) ELSE Convert(VARCHAR,b.StartFromCt) END + b.Sufx AS No" +
                         " FROM VoucherTypes a" +
                         " INNER JOIN VoucherNos b ON a.VoucherTypeId = b.VoucherTypeId" +
                         " WHERE a.VoucherTypeId='" + voucherTypeId + "'" +
                         " AND b.WEFDt<='" + date.ToString("dd-MMM-yyyy") + "'" +
                         " ORDER BY b.WEFDt DESC";
        }

        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = sql;
        sqlDR = sqlCmd.ExecuteReader();

        if (sqlDR.Read())
        {
            no = sqlDR["No"].ToString();
        }
        else
        {
            no = null;
        }
        sqlDR.Close();
        //Sql Query to Return no

        //Increment Count if it is called from Insert function of DAL means pSqlCmd != null
        if (pSqlCmd != null)
        {
            sqlCmd.CommandText = "UPDATE VoucherNos " +
                                  " SET Ct= CASE WHEN Ct > 0 THEN Ct+1 ELSE StartFromCt+1 END" +
                                  " WHERE VoucherNoId =" +
                                  "        (SELECT TOP 1 VoucherNoId" +
                                  "         FROM VoucherTypes a" +
                                  "         INNER JOIN VoucherNos b ON a.VoucherTypeId = b.VoucherTypeId" +
                                  "         WHERE a.VoucherTypeId='" + voucherTypeId + "'" +
                                  "         AND b.WEFDt<='" + date.ToString("dd-MMM-yyyy") + "'" +
                                  "         ORDER BY b.WEFDt DESC)";
            sqlCmd.ExecuteNonQuery();
        }
        else
        {
            //If pSqlCmd is null, means This function is called at UI Entry time, so it should close the connection
            CloseSQLConnection();
        }

        return no;
    }

    #endregion

    #region "Company Details"

    public DataTable CompanyNameAndAddress()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtCompanyNameAndAddress = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT" +
                             " a.Name" +
                             " ,a.AddressLine1 + ISNULL(', '+a.AddressLine2,'') + ISNULL(', '+a.City,'') + ISNULL(', '+a.[State],'') + ISNULL(', '+a.PINCode,'')" +
                             " AS [Address]" +
                             " FROM Company a";

        dtCompanyNameAndAddress.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtCompanyNameAndAddress;
    }


    #endregion

    #region "Users"

    public DataTable Users()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtUsers = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT UserId,FirstName + ' ' + LastName 'Name' FROM Users ORDER BY FirstName";

        dtUsers.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtUsers;
    }

    #endregion

    #region "Used For Crystal Report DataSet"

    public DataSet GetDataSet(string sql)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataAdapter sqlDA = new SqlDataAdapter();
        DataSet ds = new DataSet();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = sql;
        sqlDA.SelectCommand = sqlCmd;

        sqlDA.Fill(ds);

        CloseSQLConnection();

        return ds;
    }

    #endregion

    #region "Indent"
    public DataTable GetPODetails(string ItemId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = "SELECT  top 3 Max(a.No) 'PONo',isnull(Max(c.POQty),0) as POQty, " +
                            " (isnull(Max(c.POQty),0) - Isnull(sum(d.QtyPriUM),0)) 'PendingQty', " +
                            " h.Name 'SuppName',isnull(Min(b.RatePerPriUM),0) as Rate FROM POs a INNER JOIN POLns b  " +
                            " ON a.POId = b.POId INNER JOIN (SELECT POLnId, SUM(QtyPriUM)'POQty', " +
                            " SUM(QtyAltUM)'POQtyAltUM' FROM POLnDelSchedules GROUP BY POLnId) as c  " +
                            " ON c.POLnId=b.POLnId  LEFT JOIN GRNLns d ON d.RefLnId=b.POLnId  " +
                            " INNER JOIN Lgrs h ON h.LgrId=a.VendorLgrId " +
                            " where b.ItmId= '" + ItemId + "'" +
                            " Group by b.POId,h.Name ";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetRCDetails(string ItemId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = "SELECT  top 3 a.No 'RCNo',h.Name 'SuppName',b.RatePerPriUM  as Rate " +
                            " FROM RCs a INNER JOIN RCLns b ON a.RCId = b.RCId " +
                            " INNER JOIN Lgrs h ON h.LgrId=a.VendorLgrId" +
                            " where b.ItmId= '" + ItemId + "'" +
                            " ORDER BY Dt DESC, [No] DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetDSDetails(string ItemId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = "SELECT  top 3 a.No 'DSNo',b.QtyPriUM'DSQty',(b.QtyPriUM - d.QtyPriUM) 'PendingQty',h.Name 'SuppName',RCLns.RatePerPriUM  as Rate" +
                            " FROM RCDelSchedules a INNER JOIN RCDelScheduleLns b ON a.RCDelScheduleId = b.RCDelScheduleId " +
                            " INNER JOIN RCLns  ON b.RCLnId = RCLns.RCLnId LEFT JOIN GRNLns d ON d.RefLnId=b.RCDelScheduleLnId INNER JOIN Lgrs h ON h.LgrId=a.VendorLgrId " +
                            " where b.ItmId= '" + ItemId + "'" +
                            " ORDER BY a.Dt DESC, [No] DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }

    public DataTable GetGRNDetails(string ItemId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = " SELECT top 3 a.No 'GRNNo',isnull(b.QtyPriUM,0) as GRNQty, " +
                            " h.Name 'SuppName',isnull(b.RatePerPriUM,0) as Rate " +
                            " FROM GRNs a " +
                            " INNER JOIN GRNLns  b   ON a.GRNId  = b.GRNId " +
                            " INNER JOIN Lgrs h ON h.LgrId=a.VendorLgrId " +
                            " where b.ItmId= '" + ItemId + "'" +
                            " ORDER BY a.Dt DESC, [No] DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetEDNDetails(string ItemId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = " SELECT top 3 a.No 'EDNNo',isnull(b.QtyPriUM,0) as EDNQty," +
                             " h.Name 'SuppName',isnull(b.RatePerPriUM,0) as Rate " +
                             " FROM EDNs a  INNER JOIN EDNLns  b   ON a.EDNId  = b.EDNId" +
                             " INNER JOIN Lgrs h ON h.LgrId=a.MfgProcessingPlaceVendorLgrId " +
                             " where b.ItmId= '" + ItemId + "'" +
                             " ORDER BY a.Dt DESC, [No] DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());
        CloseSQLConnection();


        return dt;
    }
    public DataTable GetPQDetails(string ItemId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = "SELECT  top 3 Max(a.No) 'PQNo',isnull(Max(c.PQQty),0) as PQQty, " +
                            " (isnull(Max(c.PQQty),0) - Isnull(sum(d.QtyPriUM),0)) 'PendingQty', " +
                            " h.Name 'SuppName',isnull(Min(b.RatePerPriUM),0) as Rate FROM PQs a INNER JOIN PQLns b  " +
                            " ON a.PQId = b.PQId INNER JOIN (SELECT PQLnId, SUM(QtyPriUM)'PQQty', " +
                            " SUM(QtyAltUM)'PQQtyAltUM' FROM PQLnDelSchedules GROUP BY PQLnId) as c  " +
                            " ON c.PQLnId=b.PQLnId  LEFT JOIN GRNLns d ON d.RefLnId=b.PQLnId  " +
                            " INNER JOIN Lgrs h ON h.LgrId=a.VendorLgrId " +
                            " where b.ItmId= '" + ItemId + "'" +
                            " Group by b.PQId,h.Name ";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetIndentDetails(string ItemId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = " SELECT top 3 a.No 'IndentNo',isnull(b.QtyPriUM,0) as IndentQty," +
                             " h.Name 'Department' " +
                             " FROM Indents  a " +
                             " INNER JOIN IndentLns  b   ON a.IndentId  = b.IndentId " +
                             " INNER JOIN Depts  h ON h.DeptId=a.DeptId " +
                             " where b.ItmId= '" + ItemId + "'" +
                             " ORDER BY a.Dt DESC, [No] DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetInquiryDetails(string ItemId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = " SELECT top 3 a.No 'InquiryNo',isnull(b.QtyPriUM,0) as InquiryQty," +
                             " h.Name 'SuppName'  FROM Inquiries  a  " +
                             " INNER JOIN InquiryLns  b  ON a.InquiryId = b.InquiryId  " +
                             " INNER JOIN Lgrs h ON h.LgrId =a.VendorLgrId " +
                             " where b.ItmId= '" + ItemId + "'" +
                             " ORDER BY a.Dt DESC, [No] DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetStkConvertSourceDetails(string ItemId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = " SELECT Top 3 a.No 'StkConvertNo',isnull(b.SourceQtyPriUM,0) as SourceQty, " +
                            " b.SourceRatePerPriUM as SourceRate," +
                            " c.Name as SourceLocation" +
                            " FROM StkConverts   a  " +
                            " INNER JOIN StkConvertLns  b  ON a.StkConvertId  = b.StkConvertId  " +
                            " inner join Locs c on c.LocId = b.SourceLocId " +
                            " where b.SourceItmId= '" + ItemId + "'" +
                            " ORDER BY a.Dt DESC, [No] DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetStkConvertDestinationDetails(string ItemId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = " SELECT Top 3 a.No 'StkConvertNo', " +
                             " isnull(b.DestinationQtyPriUM,0)as DestinationQty ," +
                             " b.DestinationRatePerPriUM as DestinationRate," +
                             " d.Name as DestinationLocation " +
                             " FROM StkConverts   a  " +
                             " INNER JOIN StkConvertLns  b  ON a.StkConvertId  = b.StkConvertId  " +
                             " inner join Locs d on d.LocId = b.DestinationLocId " +
                             " where b.DestinationItmId= '" + ItemId + "'" +
                             " ORDER BY a.Dt DESC, [No] DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetRODetails(string ItemId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = " SELECT  top 3 a.No 'RONo',h.Name 'SuppName'," +
                             " ISNULL(b.QtyPriUM,0) as ROQty,b.RatePerPriUM  as Rate " +
                             " FROM ROs a " +
                             " INNER JOIN ROLns b ON a.ROId  = b.ROId " +
                             " INNER JOIN Lgrs h ON h.LgrId=a.VendorLgrId " +
                             " where b.ItmId= '" + ItemId + "'" +
                             " ORDER BY Dt DESC, [No] DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetMISDetails(string ItemId, string voucherTypeId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = " SELECT  top 3 a.No 'MISNo',isnull(b.QtyPriUM,0) as MISQty," +
                              " b.RatePerPriUM as Rate," +
                              " c.Name as SourceLoc,d.Name as DestinationLoc " +
                              " FROM StkTransfers    a  " +
                              " INNER JOIN StkTransferLns  b  ON a.StkTransferId  = b.StkTransferId " +
                              " inner join Locs c on c.LocId = b.SourceLocId " +
                              " left join Locs d on d.LocId = b.DestinationLocId " +
            // " where a.VoucherTypeId = '8F434986-28B1-417F-ADED-D8CAB3ED9DF7' " +
                              " Where b.ItmId= '" + ItemId + "'" +
                              " AND a.VoucherTypeId = '" + voucherTypeId + "'" +
                              " ORDER BY Dt DESC, [No] DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetEUDetails(string ItemId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = " SELECT top 3 a.No 'EUNo',isnull(b.QtyPriUM,0) as EUQty, " +
                             " h.Name 'SuppName',isnull(b.RatePerPriUM,0) as Rate " +
                             " FROM StkJournals a  " +
                             " INNER JOIN StkJournalInLns b ON a.StkJournalId = b.StkJournalId " +
                             " INNER JOIN Lgrs h ON h.LgrId=a.VendorLgrId " +
                             " where b.ItmId= '" + ItemId + "'" +
                             " ORDER BY a.Dt DESC, [No] DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetStkInwardDetails(string ItemId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = " SELECT top 3 a.No 'SINo',isnull(b.QtyPriUM,0) as SIQty," +
                             " b.RatePerPriUM as Rate," +
                             " c.Name as Location " +
                             " FROM StkInwards a " +
                             " INNER JOIN StkInwardLns b ON a.StkInwardId = b.StkInwardId " +
                             " inner join Locs c on c.LocId = b.LocId " +
                             " where b.ItmId= '" + ItemId + "'" +
                             " ORDER BY a.Dt DESC, [No] DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetQCDetails(string ItemId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = " SELECT  top 3 a.No 'QCNo',isnull(a.QtyPriUMAccepted,0) as QCQty," +
                             " d.Name as SuppName " +
                             " FROM QCs a  " +
                             " INNER JOIN GRNLns b ON a.GRNLnId = b.GRNLnId " +
                             " inner join GRNs c on c.GRNId = b.GRNId " +
                             " inner join Lgrs d on d.LgrId  = c.VendorLgrId " +
                             " where b.ItmId= '" + ItemId + "'" +
                             " ORDER BY a.Dt DESC, a.[No] DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetStockDetails(string ItemId)
    {


        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = " SELECT top 1 b.Name as 'SuppLoc' " +
                             ",Stock= (SELECT SUM(ISNULL(e.QtyPriUMR,0)- " +
                             "         ISNULL(e.QtyPriUMI,0)) " +
                             "         FROM StkRIs e " +
                             "         WHERE a.ItmId = e.ItmId " +
                             "         AND a.LocId = e.LocId) " +
                             ",StockUnderApproval = (SELECT SUM(ISNULL(f.QtyPriUMR,0)- " +
                             "                       ISNULL(f.QtyPriUMI,0)) " +
                             "                       FROM StkRIs f " +
                             "                      where LocId in (  SELECT TOP 1 LocId FROM Locs WHERE Name='UnApproved')) " +
                             ",StockApproved = (SELECT SUM(ISNULL(g.QtyPriUMR,0)- " +
                             "                  ISNULL(g.QtyPriUMI,0)) " +
                             "                  FROM StkRIs g where LocId in (SELECT TOP 1 LocId FROM Locs WHERE Name='Stores' or Name = 'Main Location'))" +
                             ",StockWIP = (SELECT SUM(ISNULL(S.QtyAltUMR,0)-ISNULL(S.QtyPriUMI,0)) FROM StkRIs S " +
                             " where LocId in (SELECT TOP 1 LocId FROM Locs WHERE Name='WIP'))" +
                             ",StockPlanning = (SELECT SUM(ISNULL(S1.QtyAltUMR,0)-ISNULL(S1.QtyPriUMI,0)) FROM StkRIs S1 " +
                             " where LocId in (SELECT TOP 1 LocId FROM Locs WHERE Name='Planning'))" +
                             " FROM StkRIs a " +
                             " INNER JOIN Locs b on b.LocId = a.LocId  " +
                             " WHERE a.LocId in ( select LocId  from locs " +
                             " where ParentLocId = 'B558ECB4-23AF-4AA3-B7DC-77A143064EC3') " +
                             " AND a.ItmId= '" + ItemId + "'";
        //    " ORDER BY a.VoucherDt DESC, a.VoucherNo DESC";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }

    #endregion

    public bool GetUserRoles(string FormName, string UserName)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtUserRoles = new DataTable();
        string Str;
        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        Str = " select F.Formid,F.name,A.AppRoleId " +
            " from Forms F inner join AppRoleForms A on F.FormId=A.FormId " +
            " inner join UserAppRoles U on U.AppRoleId=A.AppRoleId " +
            " inner join Users Ur on Ur.UserId=U.UserId Where 1=1";

        if (FormName != "")
            Str += " AND Name = '" + FormName + "'";

        if (UserName != "")
            Str += " AND Ur.UserName= '" + UserName + "'";

        sqlCmd.CommandText = Str;
        dtUserRoles.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        if (dtUserRoles.Rows.Count > 0)
            return true;
        else
            return false;
    }

    public string IsReferenced(string TableName, string ColumnName, string Value, SqlCommand pSqlCmd, string OmitTables)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtRef = new DataTable();
        int Count = 0;
        string strRef = "";

        if (pSqlCmd == null)
        {
            OpenSQLConnection();
            sqlCmd.Connection = ActiveSQLConnection();
        }
        else
        {
            sqlCmd = pSqlCmd;
        }
        //if (MySession.IsAdmin == false)
        //{
        //    strRef = "You do not have Admin Rights, Contact your Administrator!!!!! ";
        //}
        //else
        {
            string str = "";
            str = " select a.Name as TableName,b.name as ColumnName from sys.objects a inner join sys.columns b on a.object_id = b.object_id " +
                " where a.type='U' and (b.name = '" + ColumnName + "') and (a.name <> '" + TableName + "')";
            if (OmitTables != null)
                str += " and a.Name not in (" + OmitTables + ")";

            sqlCmd.CommandText = str;
            dtRef.Load(sqlCmd.ExecuteReader());


            if (dtRef.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRef.Rows)
                {
                    str = " select count(*) from " + dr["TableName"] + " where  " + dr["ColumnName"] + "= '" + Value + "'";
                    sqlCmd.CommandText = str;
                    Count = Convert.ToInt16(sqlCmd.ExecuteScalar());
                    if (Count > 0)
                    {
                        strRef += " " + dr["TableName"] + " - " + Count + " Rows";
                    }
                }

            }
        }
        // CloseSQLConnection();
        return strRef;

    }
    /// <summary>
    /// Check Referance in Other Tables for Value you want to Delete If Status is Open then Any User Can Delete Record
    /// </summary>
    /// <param name="TableName"></param>
    /// <param name="ColumnName"></param>
    /// <param name="Value"></param>
    /// <param name="pSqlCmd"></param>
    /// <param name="OmitTables"></param>
    /// <param name="parentStatusColumn">"Default its ApprovedDisapproved"</param>
    /// <returns></returns>

    public string IsReferenced(string TableName, string ColumnName, string Value, SqlCommand pSqlCmd, string OmitTables, string parentStatusColumn)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtRef = new DataTable();
        int Count = 0;
        string strRef = "";

        if (pSqlCmd == null)
        {
            OpenSQLConnection();
            sqlCmd.Connection = ActiveSQLConnection();
        }
        else
        {
            sqlCmd = pSqlCmd;
        }
        if (MySession.IsAdmin == false)
        {
            if (parentStatusColumn == null) parentStatusColumn = "ApprovedDisapproved";
            string strStatus = "";
            strStatus = " select isnull(a." + parentStatusColumn + ",'') Status from " + TableName + " a " +
                " where a." + ColumnName + " = '" + Value + "'" +
                " And a." + parentStatusColumn + " is not null";
            sqlCmd.CommandText = strStatus;
            dtRef.Load(sqlCmd.ExecuteReader());
            if (dtRef.Rows.Count > 0)
            {
                strRef = "Record is not in Open Mode, Contact your Administrator!!!!! ";
            }
        }
        else
        {
            string str = "";
            str = " select a.Name as TableName,b.name as ColumnName from sys.objects a inner join sys.columns b on a.object_id = b.object_id " +
                " where a.type='U' and (b.name = '" + ColumnName + "') and (a.name <> '" + TableName + "')";
            if (OmitTables != null)
                str += " and a.Name not in (" + OmitTables + ")";

            sqlCmd.CommandText = str;
            dtRef.Load(sqlCmd.ExecuteReader());


            if (dtRef.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRef.Rows)
                {
                    str = " select count(*) from " + dr["TableName"] + " where  " + dr["ColumnName"] + "= '" + Value + "'";
                    sqlCmd.CommandText = str;
                    Count = Convert.ToInt16(sqlCmd.ExecuteScalar());
                    if (Count > 0)
                    {
                        strRef += " " + dr["TableName"] + " - " + Count + " Rows";
                    }
                }

            }
        }
        // CloseSQLConnection();
        return strRef;

    }
    /// <summary>
    /// Check Referance With Primary Tables Status, If Status is Open then Any User Can Delete Record
    /// </summary>
    /// <param name="TableName"></param>
    /// <param name="ColumnName"></param>
    /// <param name="Value"></param>
    /// <param name="pSqlCmd"></param>
    /// <param name="OmitTables"></param>
    /// <param name="parentStatusColumn"></param>
    /// <param name="parentTableName"></param>
    /// <param name="parentPrimaryColumn"></param>
    /// <returns></returns>
    public string IsReferenced(string TableName, string ColumnName, string Value, SqlCommand pSqlCmd, string OmitTables, string parentStatusColumn, string parentTableName, string parentPrimaryColumn)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtRef = new DataTable();
        int Count = 0;
        string strRef = "";

        if (pSqlCmd == null)
        {
            OpenSQLConnection();
            sqlCmd.Connection = ActiveSQLConnection();
        }
        else
        {
            sqlCmd = pSqlCmd;
        }
        if (MySession.IsAdmin == false)
        {
            string strStatus = "";
            strStatus = " select isnull(a." + parentStatusColumn + ",'') Status from " + parentTableName + " a " +
                " inner join " + TableName + " b on b." + parentPrimaryColumn + " = a." + parentPrimaryColumn + " where b." + ColumnName + " = '" + Value + "'" +
                " And a." + parentStatusColumn + " is not null";
            sqlCmd.CommandText = strStatus;
            dtRef.Load(sqlCmd.ExecuteReader());
            if (dtRef.Rows.Count > 0)
            {

                strRef = "Record is not in Open Mode, Contact your Administrator!!!!! ";
            }
        }
        else
        {
            string str = "";
            str = " select a.Name as TableName,b.name as ColumnName from sys.objects a inner join sys.columns b on a.object_id = b.object_id " +
                " where a.type='U' and (b.name = '" + ColumnName + "') and (a.name <> '" + TableName + "')";
            if (OmitTables != null)
                str += " and a.Name not in (" + OmitTables + ")";

            sqlCmd.CommandText = str;
            dtRef.Load(sqlCmd.ExecuteReader());


            if (dtRef.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRef.Rows)
                {
                    str = " select count(*) from " + dr["TableName"] + " where  " + dr["ColumnName"] + "= '" + Value + "'";
                    sqlCmd.CommandText = str;
                    Count = Convert.ToInt16(sqlCmd.ExecuteScalar());
                    if (Count > 0)
                    {
                        strRef += " " + dr["TableName"] + " - " + Count + " Rows";
                    }
                }

            }
        }
        // CloseSQLConnection();
        return strRef;

    }

    public bool SetFacets(string FacetName, SqlCommand pSqlCmd)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dtUserRoles = new DataTable();
            string Str;
            bool Reqd;
            if (pSqlCmd == null)
            {
                OpenSQLConnection();
                sqlCmd.Connection = ActiveSQLConnection();
            }
            else
            {
                sqlCmd = pSqlCmd;
            }

            //OpenSQLConnection();

            //sqlCmd.Connection = ActiveSQLConnection();
            Str = "select reqd from Facets where FacetName='" + FacetName + "'";

            sqlCmd.CommandText = Str;
            Reqd = Convert.ToBoolean(sqlCmd.ExecuteScalar());
            CloseSQLConnection();
            return Reqd;
        }
        catch
        {
            return false;
        }
    }

    public void GetLgrAddresses(string lgrIds)
    {
        //SqlCommand sqlCmd = new SqlCommand();
        //SqlDataReader dr;
        //CustomerVendorLgrDTO customerVendorLgrDTO = new CustomerVendorLgrDTO();

        //OpenSQLConnection();

        //sqlCmd.Connection = ActiveSQLConnection();
        //sqlCmd.CommandType = CommandType.Text;

        //sqlCmd.CommandText = " SELECT a.LgrId,b.LgrAddressId,a.Name" +
        //                         " ,b.StreetAddress + ISNULL(', '+b.City,'') + ISNULL(', '+b.StateOrProvince,'') + ISNULL(', '+b.Country,'') + ISNULL(', '+ b.ZipOrPostalCode,'') AS [Address]" +
        //                         " ,c.CrncyId, c.Symbol AS CrncySymbol, c.DecimalPlaces" +
        //                         " FROM Lgrs a" +
        //                         " INNER JOIN LgrAddresses b ON a.LgrId=b.LgrId" +
        //                         " INNER JOIN Crncys c ON a.CrncyId = c.CrncyId" +
        //                         " WHERE a.LgrId='" + lgrId + "'";

        //dr = sqlCmd.ExecuteReader();

        ////Loop in Lgraddresses and copy Data to CustomerVendorLgrAddressDTO
        //ArrayList al = new ArrayList();
        //string id;
        //string Name;

        //while (dr.Read())
        //{
        //    id = dr.GetGuid(dr.GetOrdinal("LgrAddressId")).ToString();
        //    Name = dr.GetString(dr.GetOrdinal("Address"));

        //    al.Add(new { Value = id, Display = Name});
        //}
        //dr.Close();
        ////Loop in Lgraddresses and copy Data to CustomerVendorLgrAddressDTO

        //CloseSQLConnection();

        //lgrAddresses = al;

    }

    //FOR DYNAMIC DIV BY BIRWA
    public DataTable ItmBOM(string ItmId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        sqlCmd.CommandText = " SELECT a.*,b.Name as ItmName,b.[Desc],b.MatlGradeName,b.MatlGrpName,b.CategoryTextListName as Category, " +
            " b.DrgNo,b.DrgRevNo,b.SubGrpName,  b.UMSymbolPri,b.UMDecimalPlacesPri,b.UMSymbolAlt, " +
            " b.UMDecimalPlacesAlt, b.UMConvVal AS DefaultUMConvVal " +
            " FROM BOM a  " +
            " INNER JOIN vwItms b ON b.ItmId=a.ChildItmId WHERE ParentItmId='" + ItmId + "' order by a.LnNo ";

        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }

    public DataTable ItmPlanning(string ItmId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();

        sqlCmd.CommandText = " select a.*,b.Name 'ItmName'" +
                             " ,(select SUM(ISNULL(QtyPriUMR,0)) - SUM(isnull(QtyPriUMI,0)) from StkRIs  " +
                             "   Where LocId='E8D0B307-4E7F-4168-8B68-50C9B693A7B9'   and ItmId=b.ItmId) as StkWIP " +
                             ",(select SUM(isnull(QtyPriUMR,0)) -SUM(ISNULL(QtyPriUMI,0))  from StkRIs " +
                             "   Where LocId='1D38D5F7-B95D-4589-BB35-0E319FD94EF1' and ItmId=b.ItmId) as StkPlanning  " +
                             ",Null as MainItmStock " +
                             " from RouteCards a " +
                             " inner join vwItms  b on a.ItmId = b.ItmId " +
                             " WHERE b.ItmId='" + ItmId + "'";

        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }

    public DataTable GetSODetails(string ItemId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();


        sqlCmd.CommandText = "SELECT  top 3 Max(a.No) 'SONo',isnull(Max(c.SOQty),0) as SOQty, " +
                            " (isnull(Max(c.SOQty),0) - Isnull(sum(d.QtyPriUM),0)) 'PendingQty', " +
                            " h.Name 'SuppName',isnull(Min(b.RatePerPriUM),0) as Rate FROM SOs a INNER JOIN SOLns b  " +
                            " ON a.SOId = b.SOId INNER JOIN (SELECT SOLnId, SUM(QtyPriUM)'SOQty', " +
                            " SUM(QtyAltUM)'SOQtyAltUM' FROM SOLnDelSchedules GROUP BY SOLnId) as c  " +
                            " ON c.SOLnId=b.SOLnId  LEFT JOIN GRNLns d ON d.RefLnId=b.SOLnId  " +
                            " INNER JOIN Lgrs h ON h.LgrId=a.VendorLgrId " +
                            " where b.ItmId= '" + ItemId + "'" +
                            " Group by b.SOId,h.Name ";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }

    //public DataTable Courses()
    //{
    //    SqlCommand sqlcmd = new SqlCommand();
    //    DataTable dtCourses = new DataTable();

    //    OpenSQLConnection();

    //    sqlcmd.Connection = ActiveSQLConnection();
    //    sqlcmd.CommandText = "SELECT CourseId,[Name] FROM Courses ORDER By Name ASC";

    //    dtCourses.Load(sqlcmd.ExecuteReader());

    //    CloseSQLConnection();

    //    return dtCourses;
    //}

    public DataTable Treatments()
    {
        SqlCommand sqlcmd = new SqlCommand();
        DataTable dt = new DataTable();

        OpenSQLConnection();

        sqlcmd.Connection = ActiveSQLConnection();
        sqlcmd.CommandText = "SELECT DeptLnId,TreatmentName FROM DeptLns ORDER By TreatmentName ASC";

        dt.Load(sqlcmd.ExecuteReader());

        CloseSQLConnection();

        return dt;
    }
    public DataTable GetAddDetails(string CaseId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        string where = "";


        if (CaseId != null)
        {
            where += " Where C.CaseId = '" + CaseId + "'";
        }

        sqlCmd.CommandText = "select p.Name'Patient Name',d.Name'Department',dl.TreatmentName,dl.Rate  " +
                             "from PatientTreatement T " +
                             " INNER JOIN Patients P on T.PatientId = P.PatientId " +
                             " INNER JOIN Cases c on c.PatientId=p.PatientId " +
                             " INNER JOIN Depts d on d.DeptId=T.DeptId  " +
                             " INNER JOIN DeptLns dl on dl.DeptLnId = T.DeptLnId " +
                             where +
                             " order by d.Name,dl.TreatmentName ";

        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());
        CloseSQLConnection();

        return dt;
    }
    public DataTable GetAddDetailsTreatment(string CaseId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dt = new DataTable();
        string where = "";


        if (CaseId != null)
        {
            where += " Where C.CaseId = '" + CaseId + "'";
        }


        sqlCmd.CommandText = " select p.Name'Patient Name',cl.LnNo,d.Name'Department',cl.TreatmentName from Patients p " +
                             " Inner join Cases c on c.PatientId=p.PatientId " +
                             " Inner join CaseLns cl on cl.CaseId=c.CaseId " +
                             " iNNER JOIN Depts d on d.DeptId=cl.DeptId  " + where +
                             " order by cl.LnNo  ";


        OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());
        CloseSQLConnection();

        return dt;
    }
    //BY KS 12102012
    public DataTable AcademicYear()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtItmNames = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT AcademicYearId, Name FROM AcademicYear ORDER BY [Name] ASC";

        dtItmNames.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtItmNames;
    }

    //By DD 13102012

    public DataTable Courses()
    {
        SqlCommand sqlCmd = new SqlCommand();
        DataTable dtItmNames = new DataTable();

        OpenSQLConnection();

        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "select CourseId ,Name from Courses";

        dtItmNames.Load(sqlCmd.ExecuteReader());

        CloseSQLConnection();

        return dtItmNames;
    }
    public DataTable getCity(string state)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dtLocs = new DataTable();

            OpenSQLConnection();

            sqlCmd.Connection = ActiveSQLConnection();


            sqlCmd.CommandText = "SELECT CityId,[Name] FROM Citys where stateId='" + state + "' ORDER BY Name ASC";

            dtLocs.Load(sqlCmd.ExecuteReader());

            CloseSQLConnection();

            return dtLocs;
        }
        catch (Exception ex)
        {
            CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
    public DataTable getCountry()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dtLocs = new DataTable();

            OpenSQLConnection();

            sqlCmd.Connection = ActiveSQLConnection();
            sqlCmd.CommandText = "SELECT countryId,[Name] FROM country ORDER BY Name ASC";

            dtLocs.Load(sqlCmd.ExecuteReader());

            CloseSQLConnection();

            return dtLocs;
        }
        catch (Exception ex)
        {
            CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
    public DataTable getState(string Country)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dtLocs = new DataTable();

            OpenSQLConnection();

            sqlCmd.Connection = ActiveSQLConnection();
            sqlCmd.CommandText = "SELECT stateId,[Name] FROM state where CountryId = '" + Country + "' ORDER BY Name ASC";

            dtLocs.Load(sqlCmd.ExecuteReader());

            CloseSQLConnection();

            return dtLocs;
        }
        catch (Exception ex)
        {
            CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }



    public DataTable LoadPeriodNo(string ChapterId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            OpenSQLConnection();

            sqlCmd.Connection = ActiveSQLConnection();
            sqlCmd.CommandText = "select Distinct convert(DOUBLE PRECISION,PeriodNo) as PeriodNo From Chapters where ChapterId = '" + ChapterId.ToString() + "' order by convert(DOUBLE PRECISION,PeriodNo)";

            dt.Load(sqlCmd.ExecuteReader());

            CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }
    public DataTable LoadPeriodNo(string SubId, string ChapterId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            OpenSQLConnection();

            sqlCmd.Connection = ActiveSQLConnection();
            sqlCmd.CommandText = "select Distinct convert(DOUBLE PRECISION,PeriodNo) as PeriodNo From Chapters " +
            " where ChapterId = '" + ChapterId.ToString() + "' and SubId = '" + SubId + "' order by convert(DOUBLE PRECISION,PeriodNo)";

            dt.Load(sqlCmd.ExecuteReader());

            CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }


    public string GetChapterId(string ChapterName, string PeriodNo, string ChapterId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            string ChapterId1;

            OpenSQLConnection();

            sqlCmd.Connection = ActiveSQLConnection();
            sqlCmd.CommandText = " select ChapterId from Chapters " +
                                 " where PeriodNo = '" + PeriodNo.ToString() + "' " +
                                 " and ChapterID = '" + ChapterId.ToString() + "' ";

            try
            {
                ChapterId1 = Convert.ToString(sqlCmd.ExecuteScalar());
            }
            catch
            {
                ChapterId1 = "";
            }

            CloseSQLConnection();

            return ChapterId1;
        }
        catch (Exception ex)
        {
            CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }
    public string GetAndroidKeyFromDB(string UserId)
    {
        //ObjectCache cache = MemoryCache.Default;
        string AndroidKey = "";
        AndroidKey = GetUsers(UserId);
        return AndroidKey;
    }

    public string GetUsers(string UserId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandType = CommandType.Text;

        OpenSQLConnection();
        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandText = "SELECT isnull(AndroidKeyId,'') as AndroidKeyId from Users Where isnull(AndroidKeyId,'') <> '' and UserId = '" + UserId + "' ";
        string AndroidKeyId = "";
        try
        {
            AndroidKeyId = sqlCmd.ExecuteScalar().ToString();
        }
        catch
        {
            AndroidKeyId = "NoKeyFound";
        }
        CloseSQLConnection();
        return AndroidKeyId;
    }
    public string HashHMACHex(string keyHex, string message)
    {
        byte[] hash = HashHMAC(StringEncode(keyHex), StringEncode(message));
        return HashEncode(hash);
    }
    private byte[] HashHMAC(byte[] key, byte[] message)
    {
        var hash = new HMACSHA256(key);
        return hash.ComputeHash(message);
    }

    private byte[] StringEncode(string text)
    {
        var encoding = new ASCIIEncoding();
        return encoding.GetBytes(text);
    }

    private string HashEncode(byte[] hash)
    {
        return BitConverter.ToString(hash).Replace("-", "").ToLower();
    }

    public string GetNEWID()
    {
        SqlCommand sqlCmd = new SqlCommand();
        string NewId = "";

        OpenSQLConnection();
        sqlCmd.Connection = ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;

        try
        {
            sqlCmd.CommandText = " Select NEWID() ";
            NewId = sqlCmd.ExecuteScalar().ToString();

            CloseSQLConnection();
        }
        catch
        {
            CloseSQLConnection();
        }
        return NewId;
    }
}
