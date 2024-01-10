using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for HODInterviewDAL
/// </summary>
public class HODInterviewDAL
{
	 private GeneralDAL _generalDAL;

    public HODInterviewDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~HODInterviewDAL()
    {
        _generalDAL = null;
    }

    public HODInterviewDTO Select(string HODInterviewId, out ArrayList alLns)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader sqlDr;
            HODInterviewDTO HODInterviewDTO = new HODInterviewDTO();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = " Select a.*,IsNULL(b.FirstName,'') + ' ' + IsNULL(b.LastName,'') as UserName" +
                                 " from HODInterViews a " +
                                 " Inner Join Users b on b.UserId = a.UserId " +
                                 " Where a.HODInterviewId='" + HODInterviewId + "'";
            sqlDr = sqlCmd.ExecuteReader();

            while (sqlDr.Read())
            {
                if (sqlDr["HODInterviewId"] != DBNull.Value)
                    HODInterviewDTO.HODInterviewId = sqlDr["HODInterviewId"].ToString();
                else
                    HODInterviewDTO.HODInterviewId = null;

                if (sqlDr["CareerId"] != DBNull.Value)
                    HODInterviewDTO.CareerId = sqlDr["CareerId"].ToString();
                else
                    HODInterviewDTO.CareerId = null;

                if (sqlDr["Name"] != DBNull.Value)
                    HODInterviewDTO.Name = sqlDr["Name"].ToString();
                else
                    HODInterviewDTO.Name = null;

                if (sqlDr["UserId"] != DBNull.Value)
                    HODInterviewDTO.UserId = sqlDr["UserId"].ToString();
                else
                    HODInterviewDTO.UserId = null;

                if (sqlDr["UserName"] != DBNull.Value)
                    HODInterviewDTO.UserName = sqlDr["UserName"].ToString();
                else
                    HODInterviewDTO.UserName = null;

                if (sqlDr["Status"] != DBNull.Value)
                    HODInterviewDTO.Status = sqlDr["Status"].ToString();
                else
                    HODInterviewDTO.Status = null;

                if (sqlDr["Remarks"] != DBNull.Value)
                    HODInterviewDTO.Remarks = sqlDr["Remarks"].ToString();
                else
                    HODInterviewDTO.Remarks = null;

                if (sqlDr["Dt"] != DBNull.Value)
                    HODInterviewDTO.Dt = Convert.ToDateTime(sqlDr["Dt"]);
                else
                    HODInterviewDTO.Dt = null;

                if (sqlDr["CTC"] != DBNull.Value)
                    HODInterviewDTO.CTC = Convert.ToDecimal(sqlDr["CTC"]);
                else
                    HODInterviewDTO.CTC = null;

                if (sqlDr["ViewMonthlyGrossSalary"] != DBNull.Value)
                    HODInterviewDTO.ViewMonthlyGrossSalary = Convert.ToDecimal(sqlDr["ViewMonthlyGrossSalary"]);
                else
                    HODInterviewDTO.ViewMonthlyGrossSalary = null;

                if (sqlDr["ViewMonthlyBasic"] != DBNull.Value)
                    HODInterviewDTO.ViewMonthlyBasic = Convert.ToDecimal(sqlDr["ViewMonthlyBasic"]);
                else
                    HODInterviewDTO.ViewMonthlyBasic = null;

                if (sqlDr["ViewMonthlyHRA"] != DBNull.Value)
                    HODInterviewDTO.ViewMonthlyHRA = Convert.ToDecimal(sqlDr["ViewMonthlyHRA"]);
                else
                    HODInterviewDTO.ViewMonthlyHRA = null;

                if (sqlDr["Conveyance"] != DBNull.Value)
                    HODInterviewDTO.Conveyance = Convert.ToDecimal(sqlDr["Conveyance"]);
                else
                    HODInterviewDTO.Conveyance = null;

                if (sqlDr["SpecialAllowances"] != DBNull.Value)
                    HODInterviewDTO.SpecialAllowances = Convert.ToDecimal(sqlDr["SpecialAllowances"]);
                else
                    HODInterviewDTO.SpecialAllowances = null;

                if (sqlDr["ViewMonthlyPFCmpnyShare13Point61Per"] != DBNull.Value)
                    HODInterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per = Convert.ToDecimal(sqlDr["ViewMonthlyPFCmpnyShare13Point61Per"]);
                else
                    HODInterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per = null;

                if (sqlDr["ViewMonthlyESIEmpShare4Point75Per"] != DBNull.Value)
                    HODInterviewDTO.ViewMonthlyESIEmpShare4Point75Per = Convert.ToDecimal(sqlDr["ViewMonthlyESIEmpShare4Point75Per"]);
                else
                    HODInterviewDTO.ViewMonthlyESIEmpShare4Point75Per = null;

                if (sqlDr["SalStructureId"] != DBNull.Value)
                    HODInterviewDTO.SalStructureId = sqlDr["SalStructureId"].ToString();
                else
                    HODInterviewDTO.SalStructureId = null;

                if (sqlDr["IsDeductESI"] != DBNull.Value)
                    HODInterviewDTO.IsDeductESI = Convert.ToBoolean(sqlDr["IsDeductESI"]);
                else
                    HODInterviewDTO.IsDeductESI = null;

                if (sqlDr["IsDeductPF"] != DBNull.Value)
                    HODInterviewDTO.IsDeductPF = Convert.ToBoolean(sqlDr["IsDeductPF"]);
                else
                    HODInterviewDTO.IsDeductPF = null;
            }
            sqlDr.Close();

            sqlCmd.CommandText = " select a.*,b.[Text] " +
                                 " from HODInterviewLns a " +
                                 " Inner Join TextLists b on b.TextListId = a.QueTextListId" +
                                 " WHERE a.HODInterviewId = '" + HODInterviewId + "'" +
                                 " ORDER By a.LnNO";
            sqlDr = sqlCmd.ExecuteReader();

            ArrayList alLnsDTO = new ArrayList();
            while (sqlDr.Read())
            {
                HODInterviewLnDTO HODInterviewLnDTO = new HODInterviewLnDTO();

                HODInterviewLnDTO.HODInterviewLnId = sqlDr["HODInterviewLnId"].ToString();
                HODInterviewLnDTO.HODInterviewId = sqlDr["HODInterviewId"].ToString();
                HODInterviewLnDTO.LnNo = Convert.ToInt32(sqlDr["LnNo"]);

                if (sqlDr["QueTextListId"] != DBNull.Value)
                    HODInterviewLnDTO.QueTextListId = sqlDr["QueTextListId"].ToString();
                else
                    HODInterviewLnDTO.QueTextListId = null;

                if (sqlDr["Text"] != DBNull.Value)
                    HODInterviewLnDTO.Text = sqlDr["Text"].ToString();
                else
                    HODInterviewLnDTO.Text = null;

                if (sqlDr["ActualMarks"] != DBNull.Value)
                    HODInterviewLnDTO.ActualMarks = Convert.ToInt32(sqlDr["ActualMarks"]);
                else
                    HODInterviewLnDTO.ActualMarks = null;

                if (sqlDr["HRObtainedMarks"] != DBNull.Value)
                    HODInterviewLnDTO.HRObtainedMarks = Convert.ToInt32(sqlDr["HRObtainedMarks"]);
                else
                    HODInterviewLnDTO.HRObtainedMarks = null;

                if (sqlDr["HODObtainedMarks"] != DBNull.Value)
                    HODInterviewLnDTO.HODObtainedMarks = Convert.ToInt32(sqlDr["HODObtainedMarks"]);
                else
                    HODInterviewLnDTO.HODObtainedMarks = null;

                alLnsDTO.Add(HODInterviewLnDTO);
            }
            sqlDr.Close();

            _generalDAL.CloseSQLConnection();

            alLns = alLnsDTO;
            return HODInterviewDTO;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    public string Insert(HODInterviewDTO HODInterviewDTO, ArrayList alLns)
    {

        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string HODInterviewId;
        _generalDAL.OpenSQLConnection();

        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {

            sqlCmd.CommandText = " DECLARE @HODInterviewId uniqueidentifier;" +
                                 " SET @HODInterviewId = NewId()" +
                                 " INSERT INTO HODInterViews (HODInterviewId,RegistrationId, Name, UserId, Status, Remarks, Dt, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId" +
                                 " ,CTC,ViewMonthlyGrossSalary,ViewMonthlyBasic,ViewMonthlyHRA,Conveyance,SpecialAllowances,ViewMonthlyPFCmpnyShare13Point61Per,ViewMonthlyESIEmpShare4Point75Per,SalStructureId,IsDeductESI,IsDeductPF)" +
                                 " VALUES (@HODInterviewId, " +
                                 ((HODInterviewDTO.CareerId == null) ? "NULL" : "'" + HODInterviewDTO.CareerId.Replace("'", "''") + "'") + "," +
                                 ((HODInterviewDTO.Name == null) ? "NULL" : "'" + HODInterviewDTO.Name.Replace("'", "''") + "'") + "," +
                                 ((HODInterviewDTO.UserId == null) ? "NULL" : "'" + HODInterviewDTO.UserId + "'") + "," +
                                 ((HODInterviewDTO.Status == null) ? "NULL" : "'" + HODInterviewDTO.Status + "'") + "," +
                                 ((HODInterviewDTO.Remarks == null) ? "NULL" : "'" + HODInterviewDTO.Remarks.Replace("'", "''") + "'") + "," +
                                 ((HODInterviewDTO.Dt == null) ? "NULL" : "'" + Convert.ToDateTime(HODInterviewDTO.Dt).ToString("dd-MMM-yyyy") + "'") + "," +
                                 " GETDATE(),GETDATE(),'" + MySession.UserUnique + "','" + MySession.UserUnique + "'," +
                                 ((HODInterviewDTO.CTC == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.CTC) + "") + "," +
                                 ((HODInterviewDTO.ViewMonthlyGrossSalary == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.ViewMonthlyGrossSalary) + "") + "," +
                                 ((HODInterviewDTO.ViewMonthlyBasic == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.ViewMonthlyBasic) + "") + "," +
                                 ((HODInterviewDTO.ViewMonthlyHRA == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.ViewMonthlyHRA) + "") + "," +
                                 ((HODInterviewDTO.Conveyance == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.Conveyance) + "") + "," +
                                 ((HODInterviewDTO.SpecialAllowances == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.SpecialAllowances) + "") + "," +
                                 ((HODInterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per) + "") + "," +
                                 ((HODInterviewDTO.ViewMonthlyESIEmpShare4Point75Per == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.ViewMonthlyESIEmpShare4Point75Per) + "") + "," +
                                 ((HODInterviewDTO.SalStructureId == null) ? "NULL" : "'" + HODInterviewDTO.SalStructureId + "'") + "," +
                                 ((HODInterviewDTO.IsDeductESI == null) ? "NULL" : "'" + Convert.ToBoolean(HODInterviewDTO.IsDeductESI) + "'") + "," +
                                 ((HODInterviewDTO.IsDeductPF == null) ? "NULL" : "'" + Convert.ToBoolean(HODInterviewDTO.IsDeductPF) + "'") + "" +
                                 ");" +
                                 " SELECT @HODInterviewId";
            HODInterviewId = sqlCmd.ExecuteScalar().ToString();

            foreach (HODInterviewLnDTO HODInterviewLnDTO in alLns)
            {
                InsertLn(HODInterviewLnDTO, HODInterviewId, sqlCmd);
            }

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
            return HODInterviewId;
        }
        catch (Exception ex)
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
    private void InsertLn(HODInterviewLnDTO HODInterviewLnDTO, string HODInterviewId, SqlCommand sqlCmd)
    {
        sqlCmd.CommandText = " INSERT INTO HODInterviewLns (HODInterviewLnId, HODInterviewId, LnNo, QueTextListId,ActualMarks, HRObtainedMarks,HODObtainedMarks, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                             " VALUES (NEWID()" + ",'" + HODInterviewId + "'," + HODInterviewLnDTO.LnNo + "," +
                             ((HODInterviewLnDTO.QueTextListId == null) ? "NULL" : "'" + HODInterviewLnDTO.QueTextListId + "'") + "," +
                             ((HODInterviewLnDTO.ActualMarks == null) ? "NULL" : "" + Convert.ToInt32(HODInterviewLnDTO.ActualMarks) + "") + "," +
                             ((HODInterviewLnDTO.HRObtainedMarks == null) ? "NULL" : "" + Convert.ToInt32(HODInterviewLnDTO.HRObtainedMarks) + "") + "," +
                             ((HODInterviewLnDTO.HODObtainedMarks == null) ? "NULL" : "" + Convert.ToInt32(HODInterviewLnDTO.HODObtainedMarks) + "") + "," +
                             " GETDATE(),GETDATE(),'" + MySession.UserUnique + "','" + MySession.UserUnique + "')";
        sqlCmd.ExecuteNonQuery();
    }
    public void Update(HODInterviewDTO HODInterviewDTO, ArrayList alLns)
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
            sqlCmd.CommandText = " UPDATE HODInterViews SET " +
                                 " Name =" + ((HODInterviewDTO.Name == null) ? "NULL" : "'" + HODInterviewDTO.Name.Replace("'", "''") + "'") +
                                 ",UserId =" + ((HODInterviewDTO.UserId == null) ? "NULL" : "'" + HODInterviewDTO.UserId + "'") +
                                 ",Status =" + ((HODInterviewDTO.Status == null) ? "NULL" : "'" + HODInterviewDTO.Status + "'") +
                                 ",Remarks =" + ((HODInterviewDTO.Remarks == null) ? "NULL" : "'" + HODInterviewDTO.Remarks.Replace("'", "''") + "'") +
                                 ",Dt =" + ((HODInterviewDTO.Dt == null) ? "NULL" : "'" + Convert.ToDateTime(HODInterviewDTO.Dt).ToString("dd-MMM-yyyy") + "'") +
                                 ",LastUpdatedOn=GETDATE()" +
                                 ",LastUpdatedByUserId='" + MySession.UserUnique + "'" +
                                 ",CTC=" + ((HODInterviewDTO.CTC == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.CTC) + "") +
                                 ",ViewMonthlyGrossSalary=" + ((HODInterviewDTO.ViewMonthlyGrossSalary == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.ViewMonthlyGrossSalary) + "") +
                                 ",ViewMonthlyBasic=" + ((HODInterviewDTO.ViewMonthlyBasic == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.ViewMonthlyBasic) + "") +
                                 ",ViewMonthlyHRA=" + ((HODInterviewDTO.ViewMonthlyHRA == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.ViewMonthlyHRA) + "") +
                                 ",Conveyance=" + ((HODInterviewDTO.Conveyance == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.Conveyance) + "") +
                                 ",SpecialAllowances=" + ((HODInterviewDTO.SpecialAllowances == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.SpecialAllowances) + "") +
                                 ",ViewMonthlyPFCmpnyShare13Point61Per=" + ((HODInterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per) + "") +
                                 ",ViewMonthlyESIEmpShare4Point75Per=" + ((HODInterviewDTO.ViewMonthlyESIEmpShare4Point75Per == null) ? "NULL" : "" + Convert.ToDecimal(HODInterviewDTO.ViewMonthlyESIEmpShare4Point75Per) + "") + "" +
                                 ",SalStructureId =" + ((HODInterviewDTO.SalStructureId == null) ? "NULL" : "'" + HODInterviewDTO.SalStructureId + "'") +
                                 ",IsDeductESI =" + ((HODInterviewDTO.IsDeductESI == null) ? "NULL" : "'" + Convert.ToBoolean(HODInterviewDTO.IsDeductESI) + "'") +
                                 ",IsDeductPF =" + ((HODInterviewDTO.IsDeductPF == null) ? "NULL" : "'" + Convert.ToBoolean(HODInterviewDTO.IsDeductPF) + "'") +
                                 " WHERE HODInterviewId='" + HODInterviewDTO.HODInterviewId + "'";
            sqlCmd.ExecuteNonQuery();

            DeleteLns(HODInterviewDTO.HODInterviewId, sqlCmd);
            foreach (HODInterviewLnDTO HODInterviewLnDTO in alLns)
            {
                InsertLn(HODInterviewLnDTO, HODInterviewDTO.HODInterviewId, sqlCmd);
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
    private void DeleteLns(string HODInterviewId, SqlCommand sqlcmd)
    {
        try
        {
            string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');
            sqlcmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..HODInterviewLns(HODInterviewLnId, HODInterviewId, LnNo, QueTextListId,ActualMarks, HRObtainedMarks,HODObtainedMarks, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " Select HODInterviewLnId, HODInterviewId, LnNo, QueTextListId,ActualMarks, HRObtainedMarks,HODObtainedMarks, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId" +
                                 " FROM HODInterviewLns WHERE HODInterviewId='" + HODInterviewId + "'";
            sqlcmd.ExecuteNonQuery();
        }
        catch
        { }

        sqlcmd.CommandText = " DELETE FROM HODInterviewLns WHERE HODInterviewId='" + HODInterviewId + "'";
        sqlcmd.ExecuteNonQuery();
    }
    public void Delete(string HODInterviewId)
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
            string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..HODInterviewLns(HODInterviewLnId, HODInterviewId, InterviewId, LnNo, QueTextListId,ActualMarks, HRObtainedMarks,HODObtainedMarks, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " Select HODInterviewLnId, HODInterviewId, LnNo, QueTextListId,ActualMarks,HRObtainedMarks,HODObtainedMarks, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId" +
                                 " FROM InterviewLns WHERE HODInterviewId='" + HODInterviewId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " DELETE FROM HODInterviewLns WHERE HODInterviewId='" + HODInterviewId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..HODInterViews(HODInterviewId,CareerId, Name, UserId, Status, Remarks, Dt, InsertedOn" +
                                 ",LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId,CTC,ViewMonthlyGrossSalary,ViewMonthlyBasic,ViewMonthlyHRA,Conveyance,SpecialAllowances" +
                                 ",ViewMonthlyPFCmpnyShare13Point61Per,ViewMonthlyESIEmpShare4Point75Per,SalStructureId,IsDeductESI,IsDeductPF)" +
                                 " Select HODInterviewId,CareerId, Name, UserId, Status, Remarks, Dt,getdate(),getDate(),'" + MySession.UserUnique + "','" + MySession.UserUnique + "'" +
                                 ",CTC,ViewMonthlyGrossSalary,ViewMonthlyBasic,ViewMonthlyHRA,Conveyance,SpecialAllowances,ViewMonthlyPFCmpnyShare13Point61Per,ViewMonthlyESIEmpShare4Point75Per,SalStructureId,IsDeductESI,IsDeductPF" +
                                 " FROM Interviews WHERE HODInterviewId='" + HODInterviewId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " DELETE FROM HODInterViews WHERE HODInterviewId='" + HODInterviewId + "'";
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
    public DataTable GetMonthyBasicAndHRA(string Name, string SalStructureId, decimal? CTC, bool? DeductPF, bool? DeductESIC, decimal? BasicSalary)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dtStruct = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();

            sqlCmd.CommandText = " truncate table CalculateTempSalary;";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " select b.SalStructureLnId,a.SalStructureId,b.FormulaFor,b.Formula,FromCTC,ToCTC,MeanCTC from SalStructures a " +
                                 " inner join SalStructureLns b on b.SalStructureId=a.SalStructureId " +
                                 " where a.SalStructureId='" + SalStructureId + "' and  FormulaFor in ('Salary','BasicSalary','HRAandCon','PFCmpnyShare13Point61Per','ESIEmpShare4Point75Per')" +
                                 " Order by b.LnNo ";
            dtStruct.Load(sqlCmd.ExecuteReader());

            sqlCmd.CommandText = " insert into CalculateTempSalaryForCandidate values('" + Name + "','" + CTC + "',null," + BasicSalary + ",null,null,null,'" + dtStruct.Rows[0]["FromCTC"] + "','" + dtStruct.Rows[0]["ToCTC"] + "','" + dtStruct.Rows[0]["MeanCTC"] + "'," + ((DeductESIC == true) ? 1 : 0) + "," + ((DeductPF == true) ? 1 : 0) + ", 0, 0, 0, 0, 0, 0, '" + CTC + "', 1, 1)";
            sqlCmd.ExecuteNonQuery();

            foreach (DataRow dr in dtStruct.Rows)
            {
                try
                {
                    if (BasicSalary != null && BasicSalary > 0)
                    {
                        if (dr["FormulaFor"].ToString() == "BasicSalary")
                        {
                            sqlCmd.CommandText = " Update CalculateTempSalaryForCandidate set " + dr["FormulaFor"].ToString() + " =  ( " + BasicSalary + " )";
                        }
                        else
                        {
                            sqlCmd.CommandText = " Update CalculateTempSalaryForCandidate set " + dr["FormulaFor"].ToString() + " =  ( " + dr["Formula"].ToString().Replace("GrossSalary", "Salary") + " )";
                        }
                    }
                    else
                    {
                        sqlCmd.CommandText = " Update CalculateTempSalaryForCandidate set " + dr["FormulaFor"].ToString() + " =  ( " + dr["Formula"].ToString().Replace("GrossSalary", "Salary") + " )";
                    }
                    sqlCmd.ExecuteNonQuery();
                }
                catch //(Exception ex)
                {
                    //break;
                    //throw ex;
                }
            }
            DataTable dtTempSal = new DataTable();
            sqlCmd.CommandText = " Select * from CalculateTempSalaryForCandidate ";
            dtTempSal.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dtTempSal;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }
    public string GetSalStructureIdfromCTC(decimal? CTC)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            string SalStructureId;
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();

            sqlCmd.CommandText = " select Top 1 SalStructureId from SalStructures where '" + CTC + "' between FromCTC and ToCTC order by FromCTC";

            SalStructureId = sqlCmd.ExecuteScalar().ToString();

            _generalDAL.CloseSQLConnection();

            return SalStructureId;
        }
        catch (Exception e)
        {
            _generalDAL.CloseSQLConnection();
            throw e;
        }
    }
    public DataTable TextList(string Group, string Status, string RegistrationId)
    {
        try
        {

            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            //sqlCmd.CommandText = " SELECT a.TextListId,a.[Text],a.o1 FROM TextLists a where a.[Group]='" + Group + "' And IsDisabled=0 ORDER BY a.[Text] ASC";

            sqlCmd.CommandText = " SELECT distinct a.TextListId,a.[Text],a.o1 ,i.ObtainedMarks AS HRObtainedMarks " +
                                 " FROM TextLists a "+
                                 " inner join InterViewLns i on a.TextListId = i.QueTextListId " +
                                 " Inner Join InterViews c on c.InterviewId= i.InterviewId "+
                                 " where a.[Group]='" + Group + "' And IsDisabled=0 AND c.Status='" + Status + "' AND c.RegistrationId='" + RegistrationId + "' " +
                                 " ORDER BY a.[Text] ASC" ;

            dt.Load(sqlCmd.ExecuteReader());
            _generalDAL.CloseSQLConnection();
            return dt;
        }
        catch (Exception e)
        {
            _generalDAL.CloseSQLConnection();
            throw e;
        }
    }
}