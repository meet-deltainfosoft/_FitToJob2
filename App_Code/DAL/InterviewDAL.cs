using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

/// <summary>
/// Summary description for InterviewDAL
/// </summary>
public class InterviewDAL
{
    private GeneralDAL _generalDAL;

    public InterviewDAL()
    {
        _generalDAL = new GeneralDAL();
    }
    ~InterviewDAL()
    {
        _generalDAL = null;
    }

    public InterviewDTO Select(string InterviewId, out ArrayList alLns)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader sqlDr;
            InterviewDTO InterviewDTO = new InterviewDTO();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;

            sqlCmd.CommandText = " Select a.*,IsNULL(b.FirstName,'') + ' ' + IsNULL(b.LastName,'') as UserName" +
                                 " from Interviews a " +
                                 " Inner Join Users_ b on b.UserId = a.UserId " +
                                 " Where a.InterviewId='" + InterviewId + "'";
            sqlDr = sqlCmd.ExecuteReader();

            while (sqlDr.Read())
            {
                if (sqlDr["InterviewId"] != DBNull.Value)
                    InterviewDTO.InterviewId = sqlDr["InterviewId"].ToString();
                else
                    InterviewDTO.InterviewId = null;

                //if (sqlDr["CareerId"] != DBNull.Value)
                //    InterviewDTO.CareerId = sqlDr["CareerId"].ToString();
                //else
                //    InterviewDTO.CareerId = null;

                if (sqlDr["Name"] != DBNull.Value)
                    InterviewDTO.Name = sqlDr["Name"].ToString();
                else
                    InterviewDTO.Name = null;

                if (sqlDr["UserId"] != DBNull.Value)
                    InterviewDTO.UserId = sqlDr["UserId"].ToString();
                else
                    InterviewDTO.UserId = null;

                if (sqlDr["UserName"] != DBNull.Value)
                    InterviewDTO.UserName = sqlDr["UserName"].ToString();
                else
                    InterviewDTO.UserName = null;

                if (sqlDr["Status"] != DBNull.Value)
                    InterviewDTO.Status = sqlDr["Status"].ToString();
                else
                    InterviewDTO.Status = null;

                if (sqlDr["Remarks"] != DBNull.Value)
                    InterviewDTO.Remarks = sqlDr["Remarks"].ToString();
                else
                    InterviewDTO.Remarks = null;

                if (sqlDr["Dt"] != DBNull.Value)
                    InterviewDTO.Dt = Convert.ToDateTime(sqlDr["Dt"]);
                else
                    InterviewDTO.Dt = null;

                if (sqlDr["CTC"] != DBNull.Value)
                    InterviewDTO.CTC = Convert.ToDecimal(sqlDr["CTC"]);
                else
                    InterviewDTO.CTC = null;

                if (sqlDr["ViewMonthlyGrossSalary"] != DBNull.Value)
                    InterviewDTO.ViewMonthlyGrossSalary = Convert.ToDecimal(sqlDr["ViewMonthlyGrossSalary"]);
                else
                    InterviewDTO.ViewMonthlyGrossSalary = null;

                if (sqlDr["ViewMonthlyBasic"] != DBNull.Value)
                    InterviewDTO.ViewMonthlyBasic = Convert.ToDecimal(sqlDr["ViewMonthlyBasic"]);
                else
                    InterviewDTO.ViewMonthlyBasic = null;

                if (sqlDr["ViewMonthlyHRA"] != DBNull.Value)
                    InterviewDTO.ViewMonthlyHRA = Convert.ToDecimal(sqlDr["ViewMonthlyHRA"]);
                else
                    InterviewDTO.ViewMonthlyHRA = null;

                if (sqlDr["Conveyance"] != DBNull.Value)
                    InterviewDTO.Conveyance = Convert.ToDecimal(sqlDr["Conveyance"]);
                else
                    InterviewDTO.Conveyance = null;

                if (sqlDr["SpecialAllowances"] != DBNull.Value)
                    InterviewDTO.SpecialAllowances = Convert.ToDecimal(sqlDr["SpecialAllowances"]);
                else
                    InterviewDTO.SpecialAllowances = null;

                if (sqlDr["ViewMonthlyPFCmpnyShare13Point61Per"] != DBNull.Value)
                    InterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per = Convert.ToDecimal(sqlDr["ViewMonthlyPFCmpnyShare13Point61Per"]);
                else
                    InterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per = null;

                if (sqlDr["ViewMonthlyESIEmpShare4Point75Per"] != DBNull.Value)
                    InterviewDTO.ViewMonthlyESIEmpShare4Point75Per = Convert.ToDecimal(sqlDr["ViewMonthlyESIEmpShare4Point75Per"]);
                else
                    InterviewDTO.ViewMonthlyESIEmpShare4Point75Per = null;

                if (sqlDr["SalStructureId"] != DBNull.Value)
                    InterviewDTO.SalStructureId = sqlDr["SalStructureId"].ToString();
                else
                    InterviewDTO.SalStructureId = null;

                if (sqlDr["IsDeductESI"] != DBNull.Value)
                    InterviewDTO.IsDeductESI = Convert.ToBoolean(sqlDr["IsDeductESI"]);
                else
                    InterviewDTO.IsDeductESI = null;

                if (sqlDr["IsDeductPF"] != DBNull.Value)
                    InterviewDTO.IsDeductPF = Convert.ToBoolean(sqlDr["IsDeductPF"]);
                else
                    InterviewDTO.IsDeductPF = null;
            }
            sqlDr.Close();

            sqlCmd.CommandText = " select a.*,b.[Text] " +
                                 " from InterviewLns a " +
                                 " Inner Join TextLists b on b.TextListId = a.QueTextListId" +
                                 " WHERE a.InterviewId = '" + InterviewId + "'" +
                                 " ORDER By a.LnNO";
            sqlDr = sqlCmd.ExecuteReader();

            ArrayList alLnsDTO = new ArrayList();
            while (sqlDr.Read())
            {
                InterviewLnDTO InterviewLnDTO = new InterviewLnDTO();

                InterviewLnDTO.InterviewLnId = sqlDr["InterviewLnId"].ToString();
                InterviewLnDTO.InterviewId = sqlDr["InterviewId"].ToString();
                InterviewLnDTO.LnNo = Convert.ToInt32(sqlDr["LnNo"]);

                if (sqlDr["QueTextListId"] != DBNull.Value)
                    InterviewLnDTO.QueTextListId = sqlDr["QueTextListId"].ToString();
                else
                    InterviewLnDTO.QueTextListId = null;

                if (sqlDr["Text"] != DBNull.Value)
                    InterviewLnDTO.Text = sqlDr["Text"].ToString();
                else
                    InterviewLnDTO.Text = null;

                if (sqlDr["ActualMarks"] != DBNull.Value)
                    InterviewLnDTO.ActualMarks = Convert.ToInt32(sqlDr["ActualMarks"]);
                else
                    InterviewLnDTO.ActualMarks = null;

                if (sqlDr["ObtainedMarks"] != DBNull.Value)
                    InterviewLnDTO.ObtainedMarks = Convert.ToInt32(sqlDr["ObtainedMarks"]);
                else
                    InterviewLnDTO.ObtainedMarks = null;

                alLnsDTO.Add(InterviewLnDTO);
            }
            sqlDr.Close();

            _generalDAL.CloseSQLConnection();

            alLns = alLnsDTO;
            return InterviewDTO;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    public string Insert(InterviewDTO InterviewDTO, ArrayList alLns)
    {

        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string InterviewId;
        _generalDAL.OpenSQLConnection();

        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {

            sqlCmd.CommandText = " DECLARE @InterviewId uniqueidentifier;" +
                                 " SET @InterviewId = NewId()" +
                                 " INSERT INTO Interviews (InterviewId,RegistrationId, Name, UserId, Status, Remarks, Dt, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId" +
                                 " ,CTC,ViewMonthlyGrossSalary,ViewMonthlyBasic,ViewMonthlyHRA,Conveyance,SpecialAllowances,ViewMonthlyPFCmpnyShare13Point61Per,ViewMonthlyESIEmpShare4Point75Per,SalStructureId,IsDeductESI,IsDeductPF)" +
                                 " VALUES (@InterviewId, "+
                                 ((InterviewDTO.CareerId == null) ? "NULL" : "'" + InterviewDTO.CareerId.Replace("'", "''") + "'") + "," +
                                 ((InterviewDTO.Name == null) ? "NULL" : "'" + InterviewDTO.Name.Replace("'", "''") + "'") + "," +
                                 ((InterviewDTO.UserId == null) ? "NULL" : "'" + InterviewDTO.UserId + "'") + "," +
                                 ((InterviewDTO.Status == null) ? "NULL" : "'" + InterviewDTO.Status + "'") + "," +
                                 ((InterviewDTO.Remarks == null) ? "NULL" : "'" + InterviewDTO.Remarks.Replace("'", "''") + "'") + "," +
                                 ((InterviewDTO.Dt == null) ? "NULL" : "'" + Convert.ToDateTime(InterviewDTO.Dt).ToString("dd-MMM-yyyy") + "'") + "," +
                                 " GETDATE(),GETDATE(),'" + MySession.UserUnique + "','" + MySession.UserUnique + "'," +
                                 ((InterviewDTO.CTC == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.CTC) + "") + "," +
                                 ((InterviewDTO.ViewMonthlyGrossSalary == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.ViewMonthlyGrossSalary) + "") + "," +
                                 ((InterviewDTO.ViewMonthlyBasic == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.ViewMonthlyBasic) + "") + "," +
                                 ((InterviewDTO.ViewMonthlyHRA == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.ViewMonthlyHRA) + "") + "," +
                                 ((InterviewDTO.Conveyance == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.Conveyance) + "") + "," +
                                 ((InterviewDTO.SpecialAllowances == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.SpecialAllowances) + "") + "," +
                                 ((InterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per) + "") + "," +
                                 ((InterviewDTO.ViewMonthlyESIEmpShare4Point75Per == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.ViewMonthlyESIEmpShare4Point75Per) + "") + "," +
                                 ((InterviewDTO.SalStructureId == null) ? "NULL" : "'" + InterviewDTO.SalStructureId + "'") + "," +
                                 ((InterviewDTO.IsDeductESI == null) ? "NULL" : "'" + Convert.ToBoolean(InterviewDTO.IsDeductESI) + "'") + "," +
                                 ((InterviewDTO.IsDeductPF == null) ? "NULL" : "'" + Convert.ToBoolean(InterviewDTO.IsDeductPF) + "'") + "" +
                                 ");" +
                                 " SELECT @InterviewId";
            InterviewId = sqlCmd.ExecuteScalar().ToString();

            foreach (InterviewLnDTO InterviewLnDTO in alLns)
            {
                InsertLn(InterviewLnDTO, InterviewId, sqlCmd);
            }

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
            return InterviewId;
        }
        catch (Exception ex)
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
    private void InsertLn(InterviewLnDTO InterviewLnDTO, string InterviewId, SqlCommand sqlCmd)
    {
        sqlCmd.CommandText = " INSERT INTO InterviewLns (InterviewLnId, InterviewId, LnNo, QueTextListId,ActualMarks, ObtainedMarks, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                             " VALUES (NEWID()" + ",'" + InterviewId + "'," + InterviewLnDTO.LnNo + "," +
                             ((InterviewLnDTO.QueTextListId == null) ? "NULL" : "'" + InterviewLnDTO.QueTextListId + "'") + "," +
                             ((InterviewLnDTO.ActualMarks == null) ? "NULL" : "" + Convert.ToInt32(InterviewLnDTO.ActualMarks) + "") + "," +
                             ((InterviewLnDTO.ObtainedMarks == null) ? "NULL" : "" + Convert.ToInt32(InterviewLnDTO.ObtainedMarks) + "") + "," +
                             " GETDATE(),GETDATE(),'" + MySession.UserUnique + "','" + MySession.UserUnique + "')";
        sqlCmd.ExecuteNonQuery();
    }
    public void Update(InterviewDTO InterviewDTO, ArrayList alLns)
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
            sqlCmd.CommandText = " UPDATE Interviews SET " +
                                 " Name =" + ((InterviewDTO.Name == null) ? "NULL" : "'" + InterviewDTO.Name.Replace("'", "''") + "'") +
                                 ",UserId =" + ((InterviewDTO.UserId == null) ? "NULL" : "'" + InterviewDTO.UserId + "'") +
                                 ",Status =" + ((InterviewDTO.Status == null) ? "NULL" : "'" + InterviewDTO.Status + "'") +
                                 ",Remarks =" + ((InterviewDTO.Remarks == null) ? "NULL" : "'" + InterviewDTO.Remarks.Replace("'", "''") + "'") +
                                 ",Dt =" + ((InterviewDTO.Dt == null) ? "NULL" : "'" + Convert.ToDateTime(InterviewDTO.Dt).ToString("dd-MMM-yyyy") + "'") +
                                 ",LastUpdatedOn=GETDATE()" +
                                 ",LastUpdatedByUserId='" + MySession.UserUnique + "'" +
                                 ",CTC=" + ((InterviewDTO.CTC == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.CTC) + "") +
                                 ",ViewMonthlyGrossSalary=" + ((InterviewDTO.ViewMonthlyGrossSalary == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.ViewMonthlyGrossSalary) + "") +
                                 ",ViewMonthlyBasic=" + ((InterviewDTO.ViewMonthlyBasic == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.ViewMonthlyBasic) + "") +
                                 ",ViewMonthlyHRA=" + ((InterviewDTO.ViewMonthlyHRA == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.ViewMonthlyHRA) + "") +
                                 ",Conveyance=" + ((InterviewDTO.Conveyance == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.Conveyance) + "") +
                                 ",SpecialAllowances=" + ((InterviewDTO.SpecialAllowances == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.SpecialAllowances) + "") +
                                 ",ViewMonthlyPFCmpnyShare13Point61Per=" + ((InterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.ViewMonthlyPFCmpnyShare13Point61Per) + "") +
                                 ",ViewMonthlyESIEmpShare4Point75Per=" + ((InterviewDTO.ViewMonthlyESIEmpShare4Point75Per == null) ? "NULL" : "" + Convert.ToDecimal(InterviewDTO.ViewMonthlyESIEmpShare4Point75Per) + "") + "" +
                                 ",SalStructureId =" + ((InterviewDTO.SalStructureId == null) ? "NULL" : "'" + InterviewDTO.SalStructureId + "'") +
                                 ",IsDeductESI =" + ((InterviewDTO.IsDeductESI == null) ? "NULL" : "'" + Convert.ToBoolean(InterviewDTO.IsDeductESI) + "'") +
                                 ",IsDeductPF =" + ((InterviewDTO.IsDeductPF == null) ? "NULL" : "'" + Convert.ToBoolean(InterviewDTO.IsDeductPF) + "'") +
                                 " WHERE InterviewId='" + InterviewDTO.InterviewId + "'";
            sqlCmd.ExecuteNonQuery();

            DeleteLns(InterviewDTO.InterviewId, sqlCmd);
            foreach (InterviewLnDTO InterviewLnDTO in alLns)
            {
                InsertLn(InterviewLnDTO, InterviewDTO.InterviewId, sqlCmd);
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
    private void DeleteLns(string InterviewId, SqlCommand sqlcmd)
    {
        try
        {
            string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');
            sqlcmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..InterviewLns(InterviewLnId, InterviewId, LnNo, QueTextListId,ActualMarks, ObtainedMarks, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " Select InterviewLnId, InterviewId, LnNo, QueTextListId,ActualMarks, ObtainedMarks, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId" +
                                 " FROM InterviewLns WHERE InterviewId='" + InterviewId + "'";
            sqlcmd.ExecuteNonQuery();
        }
        catch
        { }

        sqlcmd.CommandText = " DELETE FROM InterviewLns WHERE InterviewId='" + InterviewId + "'";
        sqlcmd.ExecuteNonQuery();
    }
    public void Delete(string InterviewId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..InterviewLns(InterviewLnId, InterviewId, LnNo, QueTextListId,ActualMarks, ObtainedMarks, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                 " Select InterviewLnId, InterviewId, LnNo, QueTextListId,ActualMarks, ObtainedMarks, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId" +
                                 " FROM InterviewLns WHERE InterviewId='" + InterviewId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " DELETE FROM InterviewLns WHERE InterviewId='" + InterviewId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..Interviews(InterviewId,RegistrationId, Name, UserId, Status, Remarks, Dt, InsertedOn" +
                                 ",LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId,CTC,ViewMonthlyGrossSalary,ViewMonthlyBasic,ViewMonthlyHRA,Conveyance,SpecialAllowances" +
                                 ",ViewMonthlyPFCmpnyShare13Point61Per,ViewMonthlyESIEmpShare4Point75Per,SalStructureId,IsDeductESI,IsDeductPF)" +
                                 " Select InterviewId,RegistrationId, Name, UserId, Status, Remarks, Dt,getdate(),getDate(),'" + MySession.UserUnique + "','" + MySession.UserUnique + "'" +
                                 ",CTC,ViewMonthlyGrossSalary,ViewMonthlyBasic,ViewMonthlyHRA,Conveyance,SpecialAllowances,ViewMonthlyPFCmpnyShare13Point61Per,ViewMonthlyESIEmpShare4Point75Per,SalStructureId,IsDeductESI,IsDeductPF" +
                                 " FROM Interviews WHERE InterviewId='" + InterviewId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " DELETE FROM Interviews WHERE InterviewId='" + InterviewId + "'";
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
    public DataTable TextList(string Group)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = " SELECT a.TextListId,a.[Text],a.o1 FROM TextLists a where a.[Group]='" + Group + "' And IsDisabled=0 ORDER BY a.[Text] ASC";

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