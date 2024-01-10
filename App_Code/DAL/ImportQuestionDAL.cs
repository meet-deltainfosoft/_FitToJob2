using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public class ImportQuestionDAL
{
    private GeneralDAL _generalDAL;

    public void Save(DataTable Itm, ImportQuestionDTO ImportQuestionDTO)
    {
        GeneralDAL _generalDAL = new GeneralDAL();
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;
        try
        {
            string QueNo, Question, OptA, OptB, OptC, OptD, Ans, Hashtag;

            foreach (DataRow dr in Itm.Rows)
            {
                if (dr["QueNo"] != DBNull.Value)
                {
                    QueNo = dr["QueNo"].ToString();
                }
                else
                {
                    QueNo = null;
                }
                if (dr["Question"] != DBNull.Value)
                {
                    Question = dr["Question"].ToString();
                }
                else
                {
                    Question = null;
                }

                if (dr["OptA"] != DBNull.Value)
                {
                    OptA = dr["OptA"].ToString();
                }
                else
                {
                    OptA = null;
                }

                if (dr["OptB"] != DBNull.Value)
                {
                    OptB = dr["OptB"].ToString();
                }
                else
                {
                    OptB = null;
                }

                if (dr["OptC"] != DBNull.Value)
                {
                    OptC = dr["OptC"].ToString();
                }
                else
                {
                    OptC = null;
                }

                if (dr["OptD"] != DBNull.Value)
                {
                    OptD = dr["OptD"].ToString();
                }
                else
                {
                    OptD = null;
                }

                if (dr["Ans"] != DBNull.Value)
                {
                    Ans = dr["Ans"].ToString();

                    if (Ans.ToString() == "A")
                        Ans = "1";
                    else if (Ans.ToString() == "B")
                        Ans = "2";
                    else if (Ans.ToString() == "C")
                        Ans = "3";
                    else if (Ans.ToString() == "D")
                        Ans = "4";
                }
                else
                {
                    Ans = null;
                }

                if (dr["Hashtag"] != DBNull.Value)
                {
                    Hashtag = dr["Hashtag"].ToString();
                }
                else
                {
                    Hashtag = null;
                }

                insert(sqlCmd, ImportQuestionDTO, QueNo, Question, OptA, OptB, OptC, OptD, Ans, Hashtag);
            }
            sqlTrans.Commit();
        }
        catch
        {
            sqlTrans.Rollback();
            throw new Exception();
        }
        _generalDAL.CloseSQLConnection();
    }

    private void insert(SqlCommand sqlcmd, ImportQuestionDTO ImportQuestionDTO, string QueNo, string Question, string OptA, string OptB, string OptC, string OptD, string Ans, string Hashtag)
    {
        try
        {
            sqlcmd.CommandText = " DECLARE  @QueId uniqueidentifier;" +
                         " SET @QueId = NewId()" +
                         " INSERT INTO Ques (QueId, SubId, Que, A1, A2, A3,A4, Ans, InsertedOn,LastUpdatedOn,InsertedByUserId,LastUpdatedByUserId,TestId,Hashtag, SrNo " +
                         " , QueType,QueDataType, RightMarks , WrongMarks  , NonMarks  ) " +
                         " VALUES (@QueId " +
                         "," + ((ImportQuestionDTO.SubjectId == null) ? "NULL" : "'" + ImportQuestionDTO.SubjectId + "'") +
                         "," + ((Question == null) ? "NULL" : "N'" + Question.Replace("'", "''") + "'") +
                         "," + ((OptA == null) ? "NULL" : "N'" + OptA.Replace("'", "''") + "'") +
                         "," + ((OptB == null) ? "NULL" : "N'" + OptB.Replace("'", "''") + "'") +
                         "," + ((OptC == null) ? "NULL" : "N'" + OptC.Replace("'", "''") + "'") +
                         "," + ((OptD == null) ? "NULL" : "N'" + OptD.Replace("'", "''") + "'") +
                         "," + ((Ans == null) ? "NULL" : "'" + Ans.ToString() + "'") +
                         ", GETDATE(), GETDATE(), '" + MySession.UserUnique + "', NULL" +
                         "," + ((ImportQuestionDTO.TestId == null) ? "NULL" : "'" + ImportQuestionDTO.TestId + "'") +
                         "," + ((Hashtag == null) ? "NULL" : "N'" + Hashtag.Replace("'", "''") + "'") + "" +
                         "," + ((QueNo == null) ? "NULL" : "'" + QueNo.ToString() + "'") + "" +
                         ",'MCQ',NULL,'1','0','0')";
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public bool NameExists(string SubjectId, string TestId, string Question)
    {
        try
        {
            _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " select Count(*) from Ques " +
                                 " where SubId='" + SubjectId + "' and TestId='" + TestId + "' and Que = N'" + Question.Trim().Replace("'", "''") + "'";

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
            throw new Exception();
        }
    }
    public DataTable LoadSubject(string StandardTextListId)
    {
        string sql, Str;
        DataTable dt = new DataTable();
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataSet ds = new DataSet();

            sql = " Select SubId,Name From Subs where StandardTextlistId = '" + StandardTextListId.ToString() + "' order by Name Asc";

            sqlCmd.CommandText = sql;
            _generalDAL.OpenSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            dt.Load(sqlCmd.ExecuteReader());
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message);
        }
        return dt;
    }
    public DataTable LoadTest(string SubjectId)
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            sqlCmd.CommandText = " select TestId,TestName from Tests where SubId = '" + SubjectId + "' Order by TestName Asc ";

            _generalDAL.OpenSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
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