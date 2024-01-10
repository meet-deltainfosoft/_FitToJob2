using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

public class CompanyDAL
{
    private GeneralDAL _generalDAL;

    public CompanyDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~CompanyDAL()
    {
        _generalDAL = null;
    }

    public CompanyDTO Select(string CompanyId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        CompanyDTO CompanyDTO = new CompanyDTO();
        DataTable dt = new DataTable();
        try
        {
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            //sqlCmd.CommandText = " SELECT a.*,b.Name as Currency,l.Name as LgrName " +
            //                     " FROM Company a " +
            //                     " Left JOIN Crncys b ON b.CrncyId=a.CrncyId" +
            //                     " left JOIN Lgrs l ON l.LgrId=a.LgrId" +
            //                     " Left Join TextLists t on t.TextListId =a.AccountTypeTextListId " +
            //                     " WHERE a.CompanyId='" + CompanyId + "'";

            sqlCmd.CommandText = " SELECT a.*,null as Currency,null as LgrName " +
                                 " FROM Company a " +
                                 " WHERE a.CompanyId='" + CompanyId + "'";
            sqlDr = sqlCmd.ExecuteReader();

            while (sqlDr.Read())
            {
                //CompanyId
                CompanyDTO.CompanyId = sqlDr["CompanyId"].ToString();

                // Name
                CompanyDTO.Name = sqlDr["Name"].ToString();

                // AddressLine1 
                if (sqlDr["AddressLine1"] == DBNull.Value)
                    CompanyDTO.AddressLine1 = null;
                else
                    CompanyDTO.AddressLine1 = sqlDr["AddressLine1"].ToString();

                // AddressLine2
                if (sqlDr["AddressLine2"] == DBNull.Value)
                    CompanyDTO.AddressLine2 = null;
                else
                    CompanyDTO.AddressLine2 = sqlDr["AddressLine2"].ToString();

                // City
                if (sqlDr["City"] == DBNull.Value)
                    CompanyDTO.CityName = null;
                else
                    CompanyDTO.CityName = sqlDr["City"].ToString();

                if (sqlDr["cityId"] == DBNull.Value)
                    CompanyDTO.City = null;
                else
                    CompanyDTO.City = sqlDr["cityId"].ToString();

                if (sqlDr["State"] == DBNull.Value)
                    CompanyDTO.StateName = null;
                else
                    CompanyDTO.StateName = sqlDr["State"].ToString();

                if (sqlDr["stateId"] == DBNull.Value)
                    CompanyDTO.State = null;
                else
                    CompanyDTO.State = sqlDr["stateId"].ToString();

                //PINCode
                if (sqlDr["PINCode"] == DBNull.Value)
                    CompanyDTO.PINCode = null;
                else
                    CompanyDTO.PINCode = sqlDr["PINCode"].ToString();


                //TelephoneNos
                if (sqlDr["TelephoneNos"] != DBNull.Value)
                    CompanyDTO.TelephoneNos = sqlDr["TelephoneNos"].ToString();

                //FaxNos
                if (sqlDr["FaxNos"] != DBNull.Value)
                    CompanyDTO.FaxNos = sqlDr["FaxNos"].ToString();

                //EmailIDs
                if (sqlDr["EmailIDs"] != DBNull.Value)
                    CompanyDTO.EmailId = sqlDr["EmailIDs"].ToString();

                //CrncyId
                if (sqlDr["CrncyId"] != DBNull.Value)
                    CompanyDTO.CrncyId = sqlDr["CrncyId"].ToString();

                //PanNo
                if (sqlDr["PanNo"] != DBNull.Value)
                    CompanyDTO.PanNo = sqlDr["PanNo"].ToString();

                //TINLSTNo
                if (sqlDr["TINLSTNo"] != DBNull.Value)
                    CompanyDTO.TINLSTNo = sqlDr["TINLSTNo"].ToString();


                //TINCSTNo
                if (sqlDr["TINCSTNo"] != DBNull.Value)
                    CompanyDTO.TINCSTNo = sqlDr["TINCSTNo"].ToString();


                //VATNo
                if (sqlDr["VATNo"] != DBNull.Value)
                    CompanyDTO.VATNo = sqlDr["VATNo"].ToString();


                //Website
                if (sqlDr["Website"] != DBNull.Value)
                    CompanyDTO.Website = sqlDr["Website"].ToString();

                //MobileNo
                if (sqlDr["MobileNo"] != DBNull.Value)
                    CompanyDTO.MobileNo = sqlDr["MobileNo"].ToString();

                //MobileNo
                if (sqlDr["countryId"] != DBNull.Value)
                    CompanyDTO.Country = sqlDr["countryId"].ToString();

                if (sqlDr["Country"] != DBNull.Value)
                    CompanyDTO.CountryName = sqlDr["Country"].ToString();

                if (sqlDr["LogoName"] != DBNull.Value)
                    CompanyDTO.LogoName = sqlDr["LogoName"].ToString();

                if (sqlDr["Logo"] != DBNull.Value)
                    CompanyDTO.Logo = (byte[])sqlDr["Logo"];
                else
                    CompanyDTO.Logo = null;

                if (sqlDr["LogoPath"] != DBNull.Value)
                    CompanyDTO.LogoPath = sqlDr["LogoPath"].ToString();

                if (sqlDr["ExciseRegNo"] != DBNull.Value)
                    CompanyDTO.ExciseRegNo = sqlDr["ExciseRegNo"].ToString();

                if (sqlDr["ServiceTaxRegn"] != DBNull.Value)
                    CompanyDTO.ServiceTaxRegn = sqlDr["ServiceTaxRegn"].ToString();

                if (sqlDr["Division"] != DBNull.Value)
                    CompanyDTO.Division = sqlDr["Division"].ToString();

                if (sqlDr["RangeDetail"] != DBNull.Value)
                    CompanyDTO.RangeDetail = sqlDr["RangeDetail"].ToString();

                if (sqlDr["CommissionRate"] != DBNull.Value)
                    CompanyDTO.CommissionRate = sqlDr["CommissionRate"].ToString();

                if (sqlDr["ServiceEmailId"] != DBNull.Value)
                    CompanyDTO.ServiceEmailId = sqlDr["ServiceEmailId"].ToString();

                if (sqlDr["BankName"] != DBNull.Value)
                    CompanyDTO.BankName = sqlDr["BankName"].ToString();
                else
                    CompanyDTO.BankName = null;

                if (sqlDr["ACNo"] != DBNull.Value)
                    CompanyDTO.ACNo = sqlDr["ACNo"].ToString();
                else
                    CompanyDTO.ACNo = null;

                if (sqlDr["BranchName"] != DBNull.Value)
                    CompanyDTO.BranchName = sqlDr["BranchName"].ToString();
                else
                    CompanyDTO.BranchName = null;

                if (sqlDr["IFSCCode"] != DBNull.Value)
                    CompanyDTO.IFSCCode = sqlDr["IFSCCode"].ToString();
                else
                    CompanyDTO.IFSCCode = null;

                if (sqlDr["DivTextListId"] != DBNull.Value)
                    CompanyDTO.DivTextListId = sqlDr["DivTextListId"].ToString();
                else
                    CompanyDTO.DivTextListId = null;

                if (sqlDr["LocId"] != DBNull.Value)
                    CompanyDTO.LocId = sqlDr["LocId"].ToString();
                else
                    CompanyDTO.LocId = null;

                if (sqlDr["LgrId"] != DBNull.Value)
                    CompanyDTO.LgrId = sqlDr["LgrId"].ToString();
                else
                    CompanyDTO.LgrId = null;

                if (sqlDr["LgrName"] != DBNull.Value)
                    CompanyDTO.LgrName = sqlDr["LgrName"].ToString();
                else
                    CompanyDTO.LgrName = null;

                if (sqlDr["GSTNo"] != DBNull.Value)
                    CompanyDTO.GSTNo = sqlDr["GSTNo"].ToString();
                else
                    CompanyDTO.GSTNo = null;

                if (sqlDr["CINNO"] != DBNull.Value)
                    CompanyDTO.CINNO = sqlDr["CINNO"].ToString();
                else
                    CompanyDTO.CINNO = null;

                if (sqlDr["AccountTypeTextListId"] != DBNull.Value)
                    CompanyDTO.AccountTypeTextListId = sqlDr["AccountTypeTextListId"].ToString();
                else
                    CompanyDTO.AccountTypeTextListId = null;
            }

            sqlDr.Close();

            _generalDAL.CloseSQLConnection();


            return CompanyDTO;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }

    public void Insert(CompanyDTO CompanyDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string CompanyId;
        _generalDAL.OpenSQLConnection();

        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            //Escape Single Quote

            //Name
            CompanyDTO.Name = CompanyDTO.Name.Replace("'", "''");
            //Name

            //AddressLine1
            if (CompanyDTO.AddressLine1 != null)
                CompanyDTO.AddressLine1 = CompanyDTO.AddressLine1.Replace("'", "''");

            //AddressLine2
            if (CompanyDTO.AddressLine2 != null)
                CompanyDTO.AddressLine2 = CompanyDTO.AddressLine2.Replace("'", "''");

            //City
            if (CompanyDTO.City != null)
                CompanyDTO.City = CompanyDTO.City.Replace("'", "''");

            //State
            if (CompanyDTO.State != null)
                CompanyDTO.State = CompanyDTO.State.Replace("'", "''");

            //PINCode
            if (CompanyDTO.PINCode != null)
                CompanyDTO.PINCode = CompanyDTO.PINCode.Replace("'", "''");

            //TelephoneNos
            if (CompanyDTO.TelephoneNos != null)
                CompanyDTO.TelephoneNos = CompanyDTO.TelephoneNos.Replace("'", "''");

            //MobileNo
            if (CompanyDTO.MobileNo != null)
                CompanyDTO.MobileNo = CompanyDTO.MobileNo.Replace("'", "''");

            //Country
            if (CompanyDTO.Country != null)
                CompanyDTO.Country = CompanyDTO.Country.Replace("'", "''");

            //Escape Single Quote

            sqlCmd.CommandText = " DECLARE  @CompanyId uniqueidentifier;" +
                                  " SET @CompanyId = " + ((CompanyDTO.DivTextListId == null) ? "NewId()" : "'" + CompanyDTO.DivTextListId + "'") + "" +
                                  " INSERT INTO Company (CompanyId ,Name,AddressLine1,AddressLine2,City,State,PINCode,TelephoneNos,FaxNos," +
                                  " EmailIDs, CrncyId,PanNo,TINLSTNo,TINCSTNo,VATNo,Website,MobileNo,Country,InsertedOn,LastUpdatedOn," +
                                  " InsertedByUserId,LastUpdatedByUserId,LogoName,ExciseRegNo,ServiceTaxRegn,RangeDetail,Division," +
                                  " CommissionRate,ServiceEmailId,BankName,ACNo,BranchName,IFSCCode,DivTextListId,LocId,LgrId,GSTNo,cityId,stateId,countryId,CINNO,AccountTypeTextListId)" +
                                  " VALUES (@CompanyId,'" + CompanyDTO.Name + "','" + CompanyDTO.AddressLine1 + "'," +
                                  ((CompanyDTO.AddressLine2 == null) ? "NULL" : "'" + CompanyDTO.AddressLine2 + "'") + "," +
                                  ((CompanyDTO.CityName == null) ? "NULL" : "'" + CompanyDTO.CityName + "'") + "," +
                                  ((CompanyDTO.StateName == null) ? "NULL" : "'" + CompanyDTO.StateName + "'") + "," +
                                  ((CompanyDTO.PINCode == null) ? "NULL" : "'" + CompanyDTO.PINCode + "'") + "," +
                                  ((CompanyDTO.TelephoneNos == null) ? "NULL" : "'" + CompanyDTO.TelephoneNos + "'") + "," +
                                  ((CompanyDTO.FaxNos == null) ? "NULL" : "'" + CompanyDTO.FaxNos + "'") + "," +
                                  ((CompanyDTO.EmailId == null) ? "NULL" : "'" + CompanyDTO.EmailId + "'") + "," +
                                  ((CompanyDTO.CrncyId == null) ? "NULL" : "'" + CompanyDTO.CrncyId + "'") + "," +
                                  ((CompanyDTO.PanNo == null) ? "NULL" : "'" + CompanyDTO.PanNo + "'") + "," +
                                  ((CompanyDTO.TINLSTNo == null) ? "NULL" : "'" + CompanyDTO.TINLSTNo + "'") + "," +
                                  ((CompanyDTO.TINCSTNo == null) ? "NULL" : "'" + CompanyDTO.TINCSTNo + "'") + "," +
                                  ((CompanyDTO.VATNo == null) ? "NULL" : "'" + CompanyDTO.VATNo + "'") + "," +
                                  ((CompanyDTO.Website == null) ? "NULL" : "'" + CompanyDTO.Website + "'") + "," +
                                  ((CompanyDTO.MobileNo == null) ? "NULL" : "'" + CompanyDTO.MobileNo + "'") + "," +
                                  ((CompanyDTO.CountryName == null) ? "NULL" : "'" + CompanyDTO.CountryName + "'") + "," +
                                  "GETDATE(),GETDATE(),NULL,NULL," +
                                  ((CompanyDTO.LogoName == null) ? "NULL" : "'" + CompanyDTO.LogoName + "'") + "," +
                                  ((CompanyDTO.ExciseRegNo == null) ? "NULL" : "'" + CompanyDTO.ExciseRegNo.Replace("'", "''") + "'") + "," +
                                  ((CompanyDTO.ServiceTaxRegn == null) ? "NULL" : "'" + CompanyDTO.ServiceTaxRegn.Replace("'", "''") + "'") + "," +
                                  ((CompanyDTO.RangeDetail == null) ? "NULL" : "'" + CompanyDTO.RangeDetail.Replace("'", "''") + "'") + "," +
                                  ((CompanyDTO.Division == null) ? "NULL" : "'" + CompanyDTO.Division.Replace("'", "''") + "'") + "," +
                                  ((CompanyDTO.CommissionRate == null) ? "NULL" : "'" + CompanyDTO.CommissionRate.Replace("'", "''") + "'") + "," +
                                  ((CompanyDTO.ServiceEmailId == null) ? "NULL" : "'" + CompanyDTO.ServiceEmailId.Replace("'", "''") + "'") + "," +
                                  ((CompanyDTO.BankName == null) ? "NULL" : "'" + CompanyDTO.BankName.Replace("'", "''") + "'") + "," +
                                  ((CompanyDTO.ACNo == null) ? "NULL" : "'" + CompanyDTO.ACNo.Replace("'", "''") + "'") + "," +
                                  ((CompanyDTO.BranchName == null) ? "NULL" : "'" + CompanyDTO.BranchName.Replace("'", "''") + "'") + "," +
                                  ((CompanyDTO.IFSCCode == null) ? "NULL" : "'" + CompanyDTO.IFSCCode.Replace("'", "''") + "'") + "," +
                                  ((CompanyDTO.DivTextListId == null) ? "NULL" : "'" + CompanyDTO.DivTextListId + "'") + "," +
                                  ((CompanyDTO.LocId == null) ? "NULL" : "'" + CompanyDTO.LocId + "'") + "," +
                                  ((CompanyDTO.LgrId == null) ? "NULL" : "'" + CompanyDTO.LgrId + "'") + "," +
                                  ((CompanyDTO.GSTNo == null) ? "NULL" : "'" + CompanyDTO.GSTNo.Replace("'", "''") + "'") + "," +
                                  ((CompanyDTO.City == null) ? "NULL" : "'" + CompanyDTO.City + "'") + "," +
                                  ((CompanyDTO.State == null) ? "NULL" : "'" + CompanyDTO.State + "'") + "," +
                                  ((CompanyDTO.Country == null) ? "NULL" : "'" + CompanyDTO.Country + "'") + "," +
                                  ((CompanyDTO.CINNO == null) ? "NULL" : "'" + CompanyDTO.CINNO + "'") + "," +
                                  ((CompanyDTO.AccountTypeTextListId == null) ? "NULL" : "'" + CompanyDTO.AccountTypeTextListId + "'") + "" +
                                  ");" +
                                  " SELECT @CompanyId";

            CompanyId = sqlCmd.ExecuteScalar().ToString();

            if (CompanyDTO.LogoName != null)
            {
                sqlCmd.CommandText = " UPDATE Company SET " +
                                     " Logo = @Logo" +
                                     " ,LogoName = '" + CompanyDTO.LogoName + "'" +
                                     " ,LogoPath = "+ ((CompanyDTO.LogoPath == null) ? "NULL" : "'" + CompanyDTO.LogoPath + "'") +
                                     " WHERE CompanyId = '" + CompanyId + "'";

                sqlCmd.Parameters.AddWithValue("@logo", CompanyDTO.Logo);

                sqlCmd.ExecuteNonQuery();
            }

            string[] s = CompanyDTO.Name.Split(' ');

            if (s[0] != null)
            {
                sqlCmd.CommandText = "INSERT INTO TextLists(TextListId,[Group],[Text],InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId)" +
                                    " VALUES('" + CompanyId + "','Div','" + s[0].ToString() + "',GETDATE(),GETDATE(),'" + MySession.UserUnique + "','" + MySession.UserUnique + "')";
                sqlCmd.ExecuteNonQuery();
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

    public void Update(CompanyDTO CompanyDTO)
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
            //Name
            CompanyDTO.Name = CompanyDTO.Name.Replace("'", "''");
            //Name

            //AddressLine1
            if (CompanyDTO.AddressLine1 != null)
                CompanyDTO.AddressLine1 = CompanyDTO.AddressLine1.Replace("'", "''");

            //AddressLine2
            if (CompanyDTO.AddressLine2 != null)
                CompanyDTO.AddressLine2 = CompanyDTO.AddressLine2.Replace("'", "''");

            //City
            if (CompanyDTO.City != null)
                CompanyDTO.City = CompanyDTO.City.Replace("'", "''");

            //State
            if (CompanyDTO.State != null)
                CompanyDTO.State = CompanyDTO.State.Replace("'", "''");

            //PINCode
            if (CompanyDTO.PINCode != null)
                CompanyDTO.PINCode = CompanyDTO.PINCode.Replace("'", "''");

            //TelephoneNos
            if (CompanyDTO.TelephoneNos != null)
                CompanyDTO.TelephoneNos = CompanyDTO.TelephoneNos.Replace("'", "''");

            //MobileNo
            if (CompanyDTO.MobileNo != null)
                CompanyDTO.MobileNo = CompanyDTO.MobileNo.Replace("'", "''");

            //Country
            if (CompanyDTO.Country != null)
                CompanyDTO.Country = CompanyDTO.Country.Replace("'", "''");

            //Escape Single Quote

            sqlCmd.CommandText = " UPDATE Company SET " +
                                 " Name='" + CompanyDTO.Name + "'" +
                                 ",AddressLine1=" + ((CompanyDTO.AddressLine1 == null) ? "NULL" : "'" + CompanyDTO.AddressLine1 + "'") +
                                 ",AddressLine2= " + ((CompanyDTO.AddressLine2 == null) ? "NULL" : "'" + CompanyDTO.AddressLine2 + "'") +
                                 ",City = " + ((CompanyDTO.CityName == null) ? "NULL" : "'" + CompanyDTO.CityName + "'") +
                                 ",State= " + ((CompanyDTO.StateName == null) ? "NULL" : "'" + CompanyDTO.StateName + "'") +
                                 ",PINCode=" + ((CompanyDTO.PINCode == null) ? "NULL" : "'" + CompanyDTO.PINCode + "'") +
                                 ",TelephoneNos=" + ((CompanyDTO.TelephoneNos == null) ? "NULL" : "'" + CompanyDTO.TelephoneNos + "'") +
                                 ",FaxNos=" + ((CompanyDTO.FaxNos == null) ? "NULL" : "'" + CompanyDTO.FaxNos + "'") +
                                 ",EmailIDs=" + ((CompanyDTO.EmailId == null) ? "NULL" : "'" + CompanyDTO.EmailId + "'") +
                                 ",CrncyId=" + ((CompanyDTO.CrncyId == null) ? "NULL" : "'" + CompanyDTO.CrncyId + "'") +
                                 ",PanNo=" + ((CompanyDTO.PanNo == null) ? "NULL" : "'" + CompanyDTO.PanNo + "'") +
                                 ",TINLSTNo=" + ((CompanyDTO.TINLSTNo == null) ? "NULL" : "'" + CompanyDTO.TINLSTNo + "'") +
                                 ",TINCSTNo=" + ((CompanyDTO.TINCSTNo == null) ? "NULL" : "'" + CompanyDTO.TINCSTNo + "'") +
                                 ",VATNo = " + ((CompanyDTO.VATNo == null) ? "NULL" : "'" + CompanyDTO.VATNo + "'") +
                                 ",Website = " + ((CompanyDTO.Website == null) ? "NULL" : "'" + CompanyDTO.Website + "'") +
                                 ",MobileNo = " + ((CompanyDTO.MobileNo == null) ? "NULL" : "'" + CompanyDTO.MobileNo + "'") +
                                 ",Country = " + ((CompanyDTO.CountryName == null) ? "NULL" : "'" + CompanyDTO.CountryName + "'") +
                                 ",ExciseRegNo = " + ((CompanyDTO.ExciseRegNo == null) ? "NULL" : "'" + CompanyDTO.ExciseRegNo.Replace("'", "''") + "'") +
                                 ",ServiceTaxRegn = " + ((CompanyDTO.ServiceTaxRegn == null) ? "NULL" : "'" + CompanyDTO.ServiceTaxRegn.Replace("'", "''") + "'") +
                                 ",RangeDetail = " + ((CompanyDTO.RangeDetail == null) ? "NULL" : "'" + CompanyDTO.RangeDetail.Replace("'", "''") + "'") +
                                 ",Division = " + ((CompanyDTO.Division == null) ? "NULL" : "'" + CompanyDTO.Division.Replace("'", "''") + "'") +
                                 ",CommissionRate = " + ((CompanyDTO.CommissionRate == null) ? "NULL" : "'" + CompanyDTO.CommissionRate.Replace("'", "''") + "'") +
                                 ",LastUpdatedOn=GETDATE()" +
                                 ",ServiceEmailId = " + ((CompanyDTO.ServiceEmailId == null) ? "NULL" : "'" + CompanyDTO.ServiceEmailId.Replace("'", "''") + "'") +
                                 ",BankName = " + ((CompanyDTO.BankName == null) ? "NULL" : "'" + CompanyDTO.BankName.Replace("'", "''") + "'") +
                                 ",ACNo = " + ((CompanyDTO.ACNo == null) ? "NULL" : "'" + CompanyDTO.ACNo.Replace("'", "''") + "'") +
                                 ",BranchName = " + ((CompanyDTO.BranchName == null) ? "NULL" : "'" + CompanyDTO.BranchName.Replace("'", "''") + "'") +
                                 ",IFSCCode = " + ((CompanyDTO.IFSCCode == null) ? "NULL" : "'" + CompanyDTO.IFSCCode.Replace("'", "''") + "'") +
                                 ",DivTextListId = " + ((CompanyDTO.DivTextListId == null) ? "NULL" : "'" + CompanyDTO.DivTextListId + "'") +
                                 ",LocId = " + ((CompanyDTO.LocId == null) ? "NULL" : "'" + CompanyDTO.LocId + "'") +
                                 ",LgrId = " + ((CompanyDTO.LgrId == null) ? "NULL" : "'" + CompanyDTO.LgrId + "'") + "" +
                                 ",GSTNo = " + ((CompanyDTO.GSTNo == null) ? "NULL" : "'" + CompanyDTO.GSTNo.Replace("'", "''") + "'") + "" +
                                 ",cityId = " + ((CompanyDTO.City == null) ? "NULL" : "'" + CompanyDTO.City + "'") + "" +
                                 ",stateId = " + ((CompanyDTO.State == null) ? "NULL" : "'" + CompanyDTO.State + "'") + "" +
                                 ",countryId = " + ((CompanyDTO.Country == null) ? "NULL" : "'" + CompanyDTO.Country + "'") + "" +
                                 ",CINNO = " + ((CompanyDTO.CINNO == null) ? "NULL" : "'" + CompanyDTO.CINNO + "'") + "" +
                                 ",AccountTypeTextListId = " + ((CompanyDTO.AccountTypeTextListId == null) ? "NULL" : "'" + CompanyDTO.AccountTypeTextListId + "'") + "" +
                                 " WHERE CompanyId='" + CompanyDTO.CompanyId + "'";

            sqlCmd.ExecuteNonQuery();

            if (CompanyDTO.LogoName != null)
            {
                sqlCmd.CommandText = " UPDATE Company SET " +
                                     " Logo = @Logo" +
                                     " ,LogoName = '" + CompanyDTO.LogoName + "'" +
                                     " ,LogoPath = " + ((CompanyDTO.LogoPath == null) ? "NULL" : "'" + CompanyDTO.LogoPath + "'") +
                                     " WHERE CompanyId = '" + CompanyDTO.CompanyId + "'";

                sqlCmd.Parameters.AddWithValue("@logo", CompanyDTO.Logo);

                sqlCmd.ExecuteNonQuery();
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

    public void Delete(string CompanyId)
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
            string[] dbFirstName = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');

            sqlCmd.CommandText = " Insert Into " + dbFirstName[2].Replace("Initial Catalog=", "") + "Deleted..Company(CompanyId, Name, AddressLine1, " +
                            "  AddressLine2, City, State, PINCode, TelephoneNos, FaxNos, EmailIDs, CrncyId, PanNo, TINLSTNo, TINCSTNo, VATNo, " +
                            " LastUpdatedOn, LastUpdatedByUserID, InsertedOn, DivTextListId, Website, MobileNo, InsertedByUserId, " +
                            " Country, Logo, LogoName, GSTNo, ExciseRegNo, ServiceTaxRegn, RangeDetail, Division, CommissionRate, BankName, ACNo,  " +
                            " BranchName, IFSCCode, cityId, stateId, countryId, LgrId, ServiceEmailId, LocId, CINNO ,AccountTypeTextListId,LogoPath  )" +
                            " Select CompanyId,Name, AddressLine1, AddressLine2, City, State, PINCode, TelephoneNos, FaxNos, EmailIDs, CrncyId, PanNo, TINLSTNo, TINCSTNo, VATNo, " +
                            " getdate(), '" + MySession.UserUnique + "', getdate(), DivTextListId, Website, MobileNo, '" + MySession.UserUnique + "', Country, Logo, LogoName, " +
                            " GSTNo, ExciseRegNo, ServiceTaxRegn, RangeDetail, Division, CommissionRate, BankName, ACNo, BranchName, IFSCCode,  cityId,stateId,countryId,lgrId,ServiceEmailId,LocId,CINNO,AccountTypeTextListId,LogoPath " +
                            " FROM Company WHERE CompanyId='" + CompanyId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM Company WHERE CompanyId='" + CompanyId + "'";
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



    public string IsReferenced(string CompanyId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        string strRef = "";
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        try
        {
            if (CompanyId != null)
            {
                //For Check Reference In Relative Table As OmitTable (Reference As Deltaweberp)
                strRef += _generalDAL.IsReferenced("Company", "CompanyId", CompanyId, sqlCmd, null);
            }
            _generalDAL.CloseSQLConnection();

            //if (strRef.LastIndexOf("ItmHistory") > 0)
            //    return "";
            //else
            return strRef;
        }
        //Add Catch
        catch
        {
            _generalDAL.CloseSQLConnection();
            return "";
            throw new Exception();
        }
    }
    public bool IsAllowToDeleted(string CompanyId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        try
        {
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;

            string innerJoin = "";
            string where = "";

            if (MySession.IsDelete == "0")
            {
                where = " AND 1 = 2";
            }
            //else if (MySession.IsDelete == "1")
            //{
            //    innerJoin = " INNER JOIN (Select UserId,EmpId " +
            //                " From Emps where EmpId = " + MySession.UserID + ") " +
            //                " iu on (iu.UserId = d.InsertedByUserId or iu.EmpId = d.EmpId)";
            //}
            //else if (MySession.IsDelete == "2")
            //{
            //    innerJoin = " INNER JOIN (Select UserId,EmpId " +
            //                " From Emps where EmpId = " + MySession.UserID + " " +
            //                " UNION " +
            //                " Select b.UserId,b.EmpId from EmpSabUserLns a " +
            //                " inner join Emps b on b.EmpId = a.SubEmpId " +
            //                " where a.EmpId = " + MySession.UserID + ") " +
            //                " iu on (iu.UserId = d.InsertedByUserId or iu.EmpId = d.EmpId)";
            //}
            else if (MySession.IsDelete == "3")
            {
                innerJoin = " INNER JOIN EmpDivLns ed on ed.DivTextListId = d.DivTextListId and ed.EmpId = " + MySession.UserID + "";
            }
            else if (MySession.IsDelete == "4")
            {
                innerJoin = "";
            }
            else
            {
                innerJoin = "";
            }

            sqlCmd.CommandText = " SELECT COUNT(*) FROM Company d " +
                                 innerJoin +
                                 " WHERE d.CompanyId = '" + CompanyId + "'" +
                                 where;

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
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }

    public bool NameExists(string name)
    {
        //Escape Single Quote
        name = name.Trim().Replace("'", "''");
        //Escape single Quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM Company WHERE Name='" + name + "'";

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
    public bool NameExists(string name, string excludeCompanyId)
    {
        //Escape Single Quote
        name = name.Replace("'", "''");
        //Escape Single Quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM Company WHERE Name='" + name + "' AND NOT CompanyId='" + excludeCompanyId + "'";

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
    public void UpdateLogo(CompanyDTO CompanyDTO)
    {
        SqlCommand sqlCmd = new SqlCommand();
        try
        {
            //LogoName
            if (CompanyDTO.LogoName != null)
                CompanyDTO.LogoName = CompanyDTO.LogoName.Replace("'", "''");

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = " UPDATE Company SET " +
                                 " Logo = @Logo" +
                                 ",LogoName = '" + CompanyDTO.LogoName + "'" +
                                 ",LogoPath = " + ((CompanyDTO.LogoPath == null) ? "NULL" : "'" + CompanyDTO.LogoPath + "'") +
                                 " WHERE CompanyId = '" + CompanyDTO.CompanyId + "'";

            sqlCmd.Parameters.AddWithValue("@logo", CompanyDTO.Logo);

            sqlCmd.ExecuteNonQuery();

            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }
}
