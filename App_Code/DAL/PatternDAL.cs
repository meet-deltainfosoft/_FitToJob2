using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for PatternDAL
/// </summary>
public class PatternDAL
{
    private GeneralDAL _generalDAL;

    public PatternDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~PatternDAL()
    {
        _generalDAL = null;
    }

    public PatternDTO Select(string PatternId, out ArrayList alPatternDTO)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataReader sqlDr;
            PatternDTO PatternDTO = new PatternDTO();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " select a.*,t.[Text] as 'StandardName' from Patterns a " +
                                 " Inner join TextLists t on t.TextListId = a.StandardTextListID " +
                                 " WHERE a.PatternId='" + PatternId + "'";
            sqlDr = sqlCmd.ExecuteReader();

            while (sqlDr.Read())
            {
                PatternDTO.PatternId = sqlDr["PatternId"].ToString();

                if (sqlDr["StandardTextlistId"] != DBNull.Value)
                    PatternDTO.StandardTextlistId = sqlDr["StandardTextlistId"].ToString();
                else
                    PatternDTO.StandardTextlistId = null;

                if (sqlDr["StandardName"] != DBNull.Value)
                    PatternDTO.StandardName = sqlDr["StandardName"].ToString();
                else
                    PatternDTO.StandardName = null;

                if (sqlDr["PatternName"] != DBNull.Value)
                    PatternDTO.PatternName = (sqlDr["PatternName"]).ToString();
                else
                    PatternDTO.PatternName = null;
            }

            sqlDr.Close();

            sqlCmd.CommandText = " select ptn.*,s.Name as 'Subject' from PatternLns ptn " +
                                 " Inner join subs s on s.SubId = ptn.SubId " +
                                 " WHERE PatternId='" + PatternId + "' order by LnNo";

            sqlDr = sqlCmd.ExecuteReader();

            ArrayList alPatternLns = new ArrayList();

            while (sqlDr.Read())
            {
                PatternLnsDTO PatternLnsDTO = new PatternLnsDTO();

                if (sqlDr["PatternLnId"] != DBNull.Value)
                    PatternLnsDTO.PatternLnId = sqlDr["PatternLnId"].ToString();
                else
                    PatternLnsDTO.PatternLnId = null;

                if (sqlDr["LnNo"] != DBNull.Value)
                    PatternLnsDTO.LnNo = Convert.ToInt32(sqlDr["LnNo"]);
                else
                    PatternLnsDTO.LnNo = null;

                if (sqlDr["SubId"] != DBNull.Value)
                    PatternLnsDTO.SubId = sqlDr["SubId"].ToString();
                else
                    PatternLnsDTO.SubId = null;

                if (sqlDr["Subject"] != DBNull.Value)
                    PatternLnsDTO.Subject = sqlDr["Subject"].ToString();
                else
                    PatternLnsDTO.Subject = null;

                if (sqlDr["NoOfMCQ"] != DBNull.Value)
                    PatternLnsDTO.NoOfMCQ = Convert.ToInt32(sqlDr["NoOfMCQ"]);
                else
                    PatternLnsDTO.NoOfMCQ = null;

                if (sqlDr["MCQRightMarks"] != DBNull.Value)
                    PatternLnsDTO.MCQRightMarks = Convert.ToDecimal(sqlDr["MCQRightMarks"]);
                else
                    PatternLnsDTO.MCQRightMarks = null;

                if (sqlDr["MCQWrongMarks"] != DBNull.Value)
                    PatternLnsDTO.MCQWrongMarks = Convert.ToDecimal(sqlDr["MCQWrongMarks"]);
                else
                    PatternLnsDTO.MCQWrongMarks = null;

                if (sqlDr["MCQSkippedMarks"] != DBNull.Value)
                    PatternLnsDTO.MCQSkippedMarks = Convert.ToDecimal(sqlDr["MCQSkippedMarks"]);
                else
                    PatternLnsDTO.MCQSkippedMarks = null;

                if (sqlDr["NoOfNonMCQ"] != DBNull.Value)
                    PatternLnsDTO.NoOfNonMCQ = Convert.ToInt32(sqlDr["NoOfNonMCQ"]);
                else
                    PatternLnsDTO.NoOfNonMCQ = null;

                if (sqlDr["NonMCQRightMarks"] != DBNull.Value)
                    PatternLnsDTO.NonMCQRightMarks = Convert.ToDecimal(sqlDr["NonMCQRightMarks"]);
                else
                    PatternLnsDTO.NonMCQRightMarks = null;

                if (sqlDr["NonMCQWrongMarks"] != DBNull.Value)
                    PatternLnsDTO.NonMCQWrongMarks = Convert.ToDecimal(sqlDr["NonMCQWrongMarks"]);
                else
                    PatternLnsDTO.NonMCQWrongMarks = null;

                if (sqlDr["NonMCQSkippedMarks"] != DBNull.Value)
                    PatternLnsDTO.NonMCQSkippedMarks = Convert.ToDecimal(sqlDr["NonMCQSkippedMarks"]);
                else
                    PatternLnsDTO.NonMCQSkippedMarks = null;

                alPatternLns.Add(PatternLnsDTO);
            }
            sqlDr.Close();

            _generalDAL.CloseSQLConnection();

            alPatternDTO = alPatternLns;
            return PatternDTO;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    public string Insert(PatternDTO PatternDTO, ArrayList PatternLns)
    {
        string PatternId;
        SqlTransaction sqlTrans = null;
        SqlCommand sqlCmd = new SqlCommand();
        bool saved;
        try
        {
            _generalDAL.OpenSQLConnection();
            sqlTrans = _generalDAL.BeginTransaction();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Transaction = sqlTrans;

            sqlCmd.CommandText = " DECLARE @NewId uniqueidentifier;" +
                                 " SET @NewId = newid() " +
                                 " INSERT INTO Patterns(PatternId,StandardTextlistId,PatternName,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId)" +
                                 " Values(@NewId," +
                                 ((PatternDTO.StandardTextlistId == null) ? "NULL" : "'" + PatternDTO.StandardTextlistId.Replace("'", "''") + "'") + "," +
                                 ((PatternDTO.PatternName == null) ? "NULL" : "N'" + PatternDTO.PatternName.Replace("'", "''") + "'") + "," +
                                 " GETDATE(),GETDATE(),'" + MySession.UserUnique + "','" + MySession.UserUnique + "');" +
                                 " SELECT @NewId";
            PatternId = sqlCmd.ExecuteScalar().ToString();

            foreach (PatternLnsDTO PatternLnsDTO in PatternLns)
            {
                InsertPatternLns(PatternLnsDTO, PatternId, sqlCmd);
            }
            sqlTrans.Commit();
            saved = true;
            return PatternId;
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            saved = false;
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    private void InsertPatternLns(PatternLnsDTO PatternLnsDTO, string PatternId, SqlCommand sqlcmd)
    {
        sqlcmd.CommandText = " Insert into PatternLns " +
                             " (PatternLnId,PatternId,LnNo,SubId,NoOfMCQ,MCQRightMarks,MCQWrongMarks,MCQSkippedMarks,NoOfNonMCQ,NonMCQRightMarks,NonMCQWrongMarks, " +
                             " NonMCQSkippedMarks,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId)" +
                             " Values(NEWID(),'" + PatternId + "', '" + PatternLnsDTO.LnNo + "' , " +
                             ((PatternLnsDTO.SubId == null) ? "NULL" : "'" + PatternLnsDTO.SubId + "'") + "," +
                             ((PatternLnsDTO.NoOfMCQ == null) ? "NULL" : "'" + Convert.ToInt32(PatternLnsDTO.NoOfMCQ) + "'") + "," +
                             ((PatternLnsDTO.MCQRightMarks == null) ? "NULL" : "'" + Convert.ToDecimal(PatternLnsDTO.MCQRightMarks) + "'") + "," +
                             ((PatternLnsDTO.MCQWrongMarks == null) ? "NULL" : "'" + Convert.ToDecimal(PatternLnsDTO.MCQWrongMarks) + "'") + "," +
                             ((PatternLnsDTO.MCQSkippedMarks == null) ? "NULL" : "'" + Convert.ToDecimal(PatternLnsDTO.MCQSkippedMarks) + "'") + "," +
                             ((PatternLnsDTO.NoOfNonMCQ == null) ? "NULL" : "'" + Convert.ToInt32(PatternLnsDTO.NoOfNonMCQ) + "'") + "," +
                             ((PatternLnsDTO.NonMCQRightMarks == null) ? "NULL" : "'" + Convert.ToDecimal(PatternLnsDTO.NonMCQRightMarks) + "'") + "," +
                             ((PatternLnsDTO.NonMCQWrongMarks == null) ? "NULL" : "'" + Convert.ToDecimal(PatternLnsDTO.NonMCQWrongMarks) + "'") + "," +
                             ((PatternLnsDTO.NonMCQSkippedMarks == null) ? "NULL" : "'" + Convert.ToDecimal(PatternLnsDTO.NonMCQSkippedMarks) + "'") + "," +
                              " GETDATE(),GETDATE(),'" + MySession.UserUnique + "','" + MySession.UserUnique + "');";
        sqlcmd.ExecuteNonQuery();
    }

    public void Update(PatternDTO PatternDTO, ArrayList PatternLnsDTO, ArrayList _deletedPatternLns)
    {
        DataTable dt = new DataTable();
        SqlTransaction sqlTrans = null;
        SqlCommand sqlCmd = new SqlCommand();
        try
        {
            _generalDAL.OpenSQLConnection();
            sqlTrans = _generalDAL.BeginTransaction();
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Transaction = sqlTrans;

            sqlCmd.CommandText = " UPDATE Patterns SET" +
                                 " StandardTextlistId = " + ((PatternDTO.StandardTextlistId == null) ? "NULL" : "'" + PatternDTO.StandardTextlistId.ToString() + "'") + "" +
                                 ",PatternName = " + ((PatternDTO.PatternName == null) ? "NULL" : "N'" + PatternDTO.PatternName.ToString() + "'") + "" +
                                 ",LastUpdatedOn=GETDATE()" +
                                 ",LastUpdatedByUserId='" + MySession.UserUnique + "'" +
                                 " WHERE PatternId ='" + PatternDTO.PatternId + "'";
            sqlCmd.ExecuteNonQuery();

            //  Delete Lines
            foreach (PatternLnsDTO PatternLnDTO in _deletedPatternLns)
            {
                DeletePatternLns(PatternLnDTO.PatternLnId, sqlCmd);
            }
            // Add New & Edit Lines
            foreach (PatternLnsDTO PatternLnDTO in PatternLnsDTO)
            {
                if (PatternLnDTO.IsNew == true)
                {

                    InsertPatternLns(PatternLnDTO, PatternLnDTO.PatternId, sqlCmd);

                }
                else if (PatternLnDTO.IsDirty == true)
                {
                    UpdatePatternLns(PatternLnDTO, sqlCmd);
                }
            }
            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    private void UpdatePatternLns(PatternLnsDTO PatternLnDTO, SqlCommand sqlCmd)
    {
        sqlCmd.CommandText = " UPDATE PatternLns SET " +
                             //  " LnNo = " + PatternLnDTO.LnNo + "" +
                             " SubId = " + ((PatternLnDTO.SubId == null) ? "NULL" : "'" + PatternLnDTO.SubId.ToString() + "'") + "" +
                             ",NoOfMCQ = " + ((PatternLnDTO.NoOfMCQ == null) ? "NULL" : "" + Convert.ToInt32(PatternLnDTO.NoOfMCQ) + "") + "" +
                             ",MCQRightMarks = " + ((PatternLnDTO.MCQRightMarks == null) ? "NULL" : "" + Convert.ToDecimal(PatternLnDTO.MCQRightMarks) + "") + "" +
                             ",MCQWrongMarks = " + ((PatternLnDTO.MCQWrongMarks == null) ? "NULL" : "" + Convert.ToDecimal(PatternLnDTO.MCQWrongMarks) + "") + "" +
                             ",MCQSkippedMarks = " + ((PatternLnDTO.MCQSkippedMarks == null) ? "NULL" : "" + Convert.ToDecimal(PatternLnDTO.MCQSkippedMarks) + "") + "" +
                             ",NoOfNonMCQ = " + ((PatternLnDTO.NoOfNonMCQ == null) ? "NULL" : "" + Convert.ToInt32(PatternLnDTO.NoOfNonMCQ) + "") + "" +
                             ",NonMCQRightMarks = " + ((PatternLnDTO.NonMCQRightMarks == null) ? "NULL" : "" + Convert.ToDecimal(PatternLnDTO.NonMCQRightMarks) + "") + "" +
                             ",NonMCQWrongMarks = " + ((PatternLnDTO.NonMCQWrongMarks == null) ? "NULL" : "" + Convert.ToDecimal(PatternLnDTO.NonMCQWrongMarks) + "") + "" +
                             ",NonMCQSkippedMarks = " + ((PatternLnDTO.NonMCQSkippedMarks == null) ? "NULL" : "" + Convert.ToDecimal(PatternLnDTO.NonMCQSkippedMarks) + "") + "" +
                             " WHERE PatternLnId='" + PatternLnDTO.PatternLnId + "'";
        sqlCmd.ExecuteNonQuery();
    }

    private void DeletePatternLns(string PatternLnId, SqlCommand sqlcmd)
    {
        string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');
        try
        {
            sqlcmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..PatternLns(PatternLnId,PatternId,LnNo,SubId," +
                                 " NoOfMCQ,MCQRightMarks,MCQWrongMarks,MCQSkippedMarks,NoOfNonMCQ,NonMCQRightMarks,NonMCQWrongMarks, " +
                                 " NonMCQSkippedMarks,InsertedOn,InsertedByUserId,LastUpdatedOn,LastUpdatedByUserId)" +
                                 " Select PatternLnId,PatternId,LnNo,SubId," +
                                 " NoOfMCQ,MCQRightMarks,MCQWrongMarks,MCQSkippedMarks,NoOfNonMCQ,NonMCQRightMarks,NonMCQWrongMarks, " +
                                 " NonMCQSkippedMarks,Getdate(),'" + MySession.UserUnique + "',LastUpdatedOn,LastUpdatedByUserId" +
                                 " FROM PatternLns WHERE PatternLnId = '" + PatternLnId + "'";
            sqlcmd.ExecuteNonQuery();

            sqlcmd.CommandText = "DELETE FROM PatternLns WHERE PatternLnId='" + PatternLnId + "'";
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    public void Delete(string PatternId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();
        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');
            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..PatternLns(PatternLnId,PatternId,LnNo,SubId," +
                                 " NoOfMCQ,MCQRightMarks,MCQWrongMarks,MCQSkippedMarks,NoOfNonMCQ,NonMCQRightMarks,NonMCQWrongMarks, " +
                                 " NonMCQSkippedMarks,InsertedOn,InsertedByUserId,LastUpdatedOn,LastUpdatedByUserId)" +
                                 " Select PatternLnId,PatternId,LnNo,SubId," +
                                 " NoOfMCQ,MCQRightMarks,MCQWrongMarks,MCQSkippedMarks,NoOfNonMCQ,NonMCQRightMarks,NonMCQWrongMarks, " +
                                 " NonMCQSkippedMarks,Getdate(),'" + MySession.UserUnique + "',LastUpdatedOn,LastUpdatedByUserId" +
                                 " FROM PatternLns WHERE PatternId = '" + PatternId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " DELETE FROM PatternLns WHERE PatternId='" + PatternId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..Patterns" +
                                 " (PatternId,StandardTextlistId,PatternName,InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId)" +
                                 " Select PatternId,StandardTextlistId,PatternName,Getdate(),LastUpdatedOn,'" + MySession.UserUnique + "',LastUpdatedByUserId" +
                                 " FROM Patterns WHERE PatternId = '" + PatternId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " DELETE FROM Patterns WHERE PatternId='" + PatternId + "'";
            sqlCmd.ExecuteNonQuery();

            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }

    public string IsReferenced(string PatternId, string PatternLnId)
    {
        string strRef = "";
        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        try
        {
            if (PatternId != null)
            {
                strRef += _generalDAL.IsReferenced("Patterns", "PatternId", PatternId, sqlCmd, "'PatternLns'");
            }
            if (PatternLnId != null)
            {
                strRef += _generalDAL.IsReferenced("PatternLns", "PatternLnId", PatternLnId, sqlCmd, "'PatternLns'");
            }
            _generalDAL.CloseSQLConnection();
            return strRef;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            return "";
            throw new Exception(ex.Message);
        }

    }

    public bool NameExist(string Name, string StandardId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM Patterns WHERE PatternName='" + Name+ "' AND NOT StandardTextlistId='" + StandardId.ToString() + "'";

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

    public DataTable LoadSubjects(string StandardTextListId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select * From Subs where StandardTextListId = '" + StandardTextListId.ToString() + "' order by Name";

            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
    }
}