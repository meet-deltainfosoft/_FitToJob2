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
using System.Collections.Generic;
using System.Collections;
using System.Web.Script.Serialization;

public class ExamScheduleDAL
{
    public GeneralDAL _generalDAL;

    public ExamScheduleDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~ExamScheduleDAL()
    {
        _generalDAL = null;
    }

    public void Insert(ExamScheduleDTO examScheduleDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string ExamScheduleId;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = " DECLARE  @ExamScheduleId uniqueidentifier;" +
                                 " SET @ExamScheduleId = NewId()" +
                                 " INSERT INTO ExamSchedules(ExamScheduleId, StandardTextListId, SubId, TestId, TotalQuestions, ExamDate, ExamFromTime, ExamToTime " +
                                 " , TotalMins, PerQueMins, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, NegativeMarks, PerQuestionTime,ShowResult,MinsforResultShow,PatternId,IsDefaultTest,AllowReview)" +
                                 " VALUES(@ExamScheduleId" +
                                 "," + ((examScheduleDTO.StandardTextListId == null) ? "NULL" : "'" + examScheduleDTO.StandardTextListId.Replace("'", "''") + "'") +
                                 "," + ((examScheduleDTO.SubId == null) ? "NULL" : "'" + examScheduleDTO.SubId.Replace("'", "''") + "'") +
                                 "," + ((examScheduleDTO.TestId == null) ? "NULL" : "'" + examScheduleDTO.TestId.Replace("'", "''") + "'") +
                                 "," + ((examScheduleDTO.TotalQuestions == null) ? "NULL" : "'" + examScheduleDTO.TotalQuestions + "'") +
                                 "," + ((examScheduleDTO.ExamDate == null) ? "NULL" : "'" + Convert.ToDateTime(examScheduleDTO.ExamDate).ToString("dd-MMM-yyyy") + "'") +
                                 "," + ((examScheduleDTO.ExamFromTime == null) ? "NULL" : "'" + Convert.ToDateTime(examScheduleDTO.ExamFromTime).ToString("dd-MMM-yyyy hh:mm tt") + "'") +
                                 "," + ((examScheduleDTO.ExamToTime == null) ? "NULL" : "'" + Convert.ToDateTime(examScheduleDTO.ExamToTime).ToString("dd-MMM-yyyy hh:mm tt") + "'") +
                                 "," + ((examScheduleDTO.TotalMins == null) ? "NULL" : "'" + examScheduleDTO.TotalMins + "'") +
                                 "," + ((examScheduleDTO.PerQueMins == null) ? "NULL" : "'" + examScheduleDTO.PerQueMins + "'") +
                                 ",GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                 "," + ((examScheduleDTO.NegativeMarks == null) ? "NULL" : "'" + examScheduleDTO.NegativeMarks + "'") +
                                 "," + ((examScheduleDTO.PerQuestionTime == null) ? "NULL" : "'" + examScheduleDTO.PerQuestionTime + "'") +
                                 "," + ((examScheduleDTO.ShowResult == null) ? "NULL" : "'" + examScheduleDTO.ShowResult + "'") +
                                 "," + ((examScheduleDTO.MinsforResultShow == null) ? "NULL" : "'" + examScheduleDTO.MinsforResultShow + "'") +
                                 "," + ((examScheduleDTO.PatternId == null) ? "NULL" : "'" + examScheduleDTO.PatternId.ToString() + "'") +
                                 "," + ((examScheduleDTO.IsDefaultTest == null) ? "NULL" : "'" + examScheduleDTO.IsDefaultTest + "'") +
                                 "," + ((examScheduleDTO.AllowReview == null) ? "NULL" : "'" + examScheduleDTO.AllowReview + "'") +
                                 ");" +
                                 " SELECT @ExamScheduleId";

            ExamScheduleId = sqlCmd.ExecuteScalar().ToString();

            if (ExamScheduleId != null)
            {
                if (examScheduleDTO.dtRestrations != null)
                {
                    if (examScheduleDTO.dtRestrations.Rows.Count > 0)
                    {
                        for (int i = 0; i <= examScheduleDTO.dtRestrations.Rows.Count - 1; i++)
                        {
                            if (Convert.ToBoolean(examScheduleDTO.dtRestrations.Rows[i]["Tick"])) // != "-1"
                            {
                                sqlCmd.CommandText = " INSERT INTO ExamScheduleLns(ExamScheduleLnId, ExamScheduleId, LnNo, RegistrationId, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
                                                " VALUES(NEWID()" +
                                                ", '" + ExamScheduleId.ToString() + "' " +
                                                ", " + (i + 1) + " " +
                                                "," + ((examScheduleDTO.dtRestrations.Rows[i]["RegistrationId"] == DBNull.Value) ? "NULL" : "'" + examScheduleDTO.dtRestrations.Rows[i]["RegistrationId"].ToString().Replace("'", "''") + "'") +
                                                ",GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL" +
                                                "); ";

                                sqlCmd.ExecuteNonQuery();
                            }
                        }

                    }
                }
            }

            DataTable dtNotification = new DataTable(); 
            if (examScheduleDTO.SendNotification)
            {
                sqlCmd.CommandText = " Select e.ExamScheduleId,el.RegistrationId,e.TestId,e.SubId, e.StandardTextListId, " +
                                          " e.ExamDate, e.ExamFromTime as ExamFromTime,e.ExamToTime as ExamToTime, tt.[Text] as 'Standard', t.TestName, s.Name as 'SubJectName',r.IMEI,r.FCMId " +
                                          " ,ISNULL(e.IsSelfieAllowed,0) as IsSelfieAllowed,ISNULL(IsSignatureAllowed, 0) as IsSignatureAllowed, c.ChapterName " +
                                          " from ExamSchedules e " +
                                          " Inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
                                          " Inner join Registration r on r.RegistrationId = el.RegistrationId " +
                                          " Inner Join TextLists tt on tt.TextListId = e.StandardTextListId " +
                                          " Inner join Tests t on t.TestID = e.TestId " +
                                          " Inner Join Subs s on s.SubId = e.SubId " +
                                          " Left join Chapters c on c.SubId = s.SubId " +
                                          " Where e.ExamScheduleId = '" + ExamScheduleId.ToString() + "' ";

                dtNotification.Load(sqlCmd.ExecuteReader());

                string FCMID1 = "";
                if (dtNotification != null)
                {
                    if (dtNotification.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtNotification.Rows)
                        {
                            if (dr["FCMId"] != DBNull.Value)
                            {
                                try
                                {
                                    if (!FCMID1.Contains(dr["FCMId"].ToString()))
                                    {
                                        System.Net.HttpWebRequest request = System.Net.WebRequest.Create("https://fcm.googleapis.com/fcm/send") as System.Net.HttpWebRequest;
                                        request.Method = "POST";
                                        string AppId = "AAAABXxbF58:APA91bE63Q6waVyR2W7X7YFZ39CSBGxYe5ghWx77Y2G5WycAwtF8xMS-d6zVajRvBdVvZHFBw3pTTn5xfAnmKavuqrZ7UmC6DaUi73uAgawx8lC2VGp1kTpV7jCpXGf93u_i1fSKguVg";
                                        request.ContentType = " application/json";
                                        request.Headers.Add(string.Format("Authorization: key={0}", AppId));
                                        request.ContentType = "application/json";
                                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                                        using (var sw = new System.IO.StreamWriter(request.GetRequestStream()))
                                        {
                                            string json = "{" +
                                                " \"registration_ids\": [\"" + dr["FCMId"].ToString() + "\"]," +
                                                "\"data\": {" +
                                                "    \"TestId\": \"" + ((dr["TestId"] == DBNull.Value) ? "NULL" : "" + dr["TestId"].ToString() + "") + "\"," +
                                                "    \"TestName\": \"" + ((dr["TestName"] == DBNull.Value) ? "NULL" : "" + dr["TestName"].ToString() + "") + "\"," +
                                                "    \"SubId\": \"" + ((dr["SubId"] == DBNull.Value) ? "NULL" : "" + dr["SubId"].ToString() + "") + "\"," +
                                                "    \"SubJectName\": \"" + ((dr["SubJectName"] == DBNull.Value) ? "NULL" : "" + dr["SubJectName"].ToString() + "") + "\"," +
                                                "    \"Standard\": \"" + ((dr["Standard"] == DBNull.Value) ? "NULL" : "" + dr["Standard"].ToString() + "") + "\"," +
                                                "    \"ExamDate\": \"" + ((dr["ExamDate"] == DBNull.Value) ? "NULL" : "" + dr["ExamDate"].ToString() + "") + "\"," +
                                                "    \"ExamFromTime\": \"" + ((dr["ExamFromTime"] == DBNull.Value) ? "NULL" : "" + dr["ExamFromTime"].ToString() + "") + "\"," +
                                                "    \"ExamToTime\": \"" + ((dr["ExamToTime"] == DBNull.Value) ? "NULL" : "" + dr["ExamToTime"].ToString() + "") + "\"," +
                                                "    \"ExamScheduleId\": \"" + ((dr["ExamScheduleId"] == DBNull.Value) ? "NULL" : "" + dr["ExamScheduleId"].ToString() + "") + "\"," +
                                                "    \"RegistrationId\": \"" + ((dr["RegistrationId"] == DBNull.Value) ? "NULL" : "" + dr["RegistrationId"].ToString() + "") + "\"," +
                                                "    \"FCMId\": \"" + ((dr["FCMId"] == DBNull.Value) ? "NULL" : "" + dr["FCMId"].ToString() + "") + "\"," +
                                                "    \"ChapterName\": \"" + ((dr["ChapterName"] == DBNull.Value) ? "NULL" : "" + dr["ChapterName"].ToString() + "") + "\"," +
                                                "    \"IsSelfieAllowed\": \"" + ((dr["IsSelfieAllowed"] == DBNull.Value) ? "NULL" : "" + Convert.ToBoolean(dr["IsSelfieAllowed"]) + "") + "\"," +
                                                "    \"IsSignatureAllowed\": \"" + ((dr["IsSignatureAllowed"] == DBNull.Value) ? "NULL" : "" + Convert.ToBoolean(dr["IsSignatureAllowed"]) + "") + "\"," +
                                                "    \"Type\": \"Test\"" +
                                                "  }" +
                                                "}";

                                            sw.Write(json);
                                            // st.Append(json);
                                            sw.Flush();
                                        }
                                        System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                }
            }

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }

    public void UpdateExamSchedules(ExamScheduleDTO examScheduleDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string ExamScheduleId;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = " update ExamSchedules set " +
                                 " ExamDate = " + ((examScheduleDTO.ExamDate == null) ? "NULL" : "'" + Convert.ToDateTime(examScheduleDTO.ExamDate).ToString("dd-MMM-yyyy") + "'") +
                                 " , ExamFromTime = " + ((examScheduleDTO.ExamFromTime == null) ? "NULL" : "'" + Convert.ToDateTime(examScheduleDTO.ExamFromTime).ToString("dd-MMM-yyyy hh:mm tt") + "'") +
                                 " , ExamToTime = " + ((examScheduleDTO.ExamToTime == null) ? "NULL" : "'" + Convert.ToDateTime(examScheduleDTO.ExamToTime).ToString("dd-MMM-yyyy hh:mm tt") + "'") +
                                 " , AllowReview = " + ((examScheduleDTO.AllowReview == null) ? "NULL" : "'" + examScheduleDTO.AllowReview + "'") + " " +
                                 " , NegativeMarks = " + ((examScheduleDTO.NegativeMarks == null) ? "NULL" : "'" + examScheduleDTO.NegativeMarks + "'") + " " +
                                 " , PerQuestionTime = " + ((examScheduleDTO.PerQuestionTime == null) ? "NULL" : "'" + examScheduleDTO.PerQuestionTime + "'") + " " +
                                 " , ShowResult = " + ((examScheduleDTO.ShowResult == null) ? "NULL" : "'" + examScheduleDTO.ShowResult + "'") + " " +
                                 " , IsDefaultTest = " + ((examScheduleDTO.IsDefaultTest == null) ? "NULL" : "'" + examScheduleDTO.IsDefaultTest + "'") + " " +
                                 " where ExamScheduleId = '" + examScheduleDTO.ExamScheduleId.ToString() + "' ";

            sqlCmd.ExecuteNonQuery().ToString();

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }

    public ExamScheduleDTO Select(string ExamScheduleId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader sqlDr;
        ExamScheduleDTO ExamScheduleDTO = new ExamScheduleDTO();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "select * from ExamSchedules a WHERE ExamScheduleId='" + ExamScheduleId + "'";
        sqlDr = sqlCmd.ExecuteReader();

        while (sqlDr.Read())
        {
            if (sqlDr["ExamScheduleId"] != DBNull.Value)
                ExamScheduleDTO.ExamScheduleId = sqlDr["ExamScheduleId"].ToString();

            if (sqlDr["StandardTextListId"] != DBNull.Value)
                ExamScheduleDTO.StandardTextListId = sqlDr["StandardTextListId"].ToString();

            if (sqlDr["SubId"] != DBNull.Value)
                ExamScheduleDTO.SubId = sqlDr["SubId"].ToString();

            if (sqlDr["TestId"] != DBNull.Value)
                ExamScheduleDTO.TestId = sqlDr["TestId"].ToString();

            if (sqlDr["TotalQuestions"] != DBNull.Value)
                ExamScheduleDTO.TotalQuestions = Convert.ToInt16(sqlDr["TotalQuestions"].ToString());

            if (sqlDr["ExamDate"] != DBNull.Value)
                ExamScheduleDTO.ExamDate = Convert.ToDateTime(sqlDr["ExamDate"].ToString());

            if (sqlDr["ExamFromTime"] != DBNull.Value)
                ExamScheduleDTO.ExamFromTime = Convert.ToDateTime(sqlDr["ExamFromTime"].ToString());

            if (sqlDr["ExamToTime"] != DBNull.Value)
                ExamScheduleDTO.ExamToTime = Convert.ToDateTime(sqlDr["ExamToTime"].ToString());

            if (sqlDr["TotalMins"] != DBNull.Value)
                ExamScheduleDTO.TotalMins = Convert.ToInt16(sqlDr["TotalMins"].ToString());

            if (sqlDr["PerQueMins"] != DBNull.Value)
                ExamScheduleDTO.PerQueMins = Convert.ToDecimal(sqlDr["PerQueMins"].ToString());

            if (sqlDr["NegativeMarks"] != DBNull.Value)
                ExamScheduleDTO.NegativeMarks = Convert.ToBoolean(sqlDr["NegativeMarks"].ToString());

            if (sqlDr["PerQuestionTime"] != DBNull.Value)
                ExamScheduleDTO.PerQuestionTime = Convert.ToBoolean(sqlDr["PerQuestionTime"].ToString());

            if (sqlDr["AllowReview"] != DBNull.Value)
                ExamScheduleDTO.AllowReview = Convert.ToBoolean(sqlDr["AllowReview"].ToString());

            if (sqlDr["MinsforResultShow"] != DBNull.Value)
                ExamScheduleDTO.MinsforResultShow = Convert.ToInt16(sqlDr["MinsforResultShow"].ToString());

            if (sqlDr["ShowResult"] != DBNull.Value)
                ExamScheduleDTO.ShowResult = Convert.ToBoolean(sqlDr["ShowResult"].ToString());

            if (sqlDr["PatternId"] != DBNull.Value)
                ExamScheduleDTO.PatternId = sqlDr["PatternId"].ToString();

            if (sqlDr["IsDefaultTest"] != DBNull.Value)
                ExamScheduleDTO.IsDefaultTest = Convert.ToBoolean(sqlDr["IsDefaultTest"].ToString());
        }

        sqlDr.Close();

        sqlCmd.CommandText = " select el.*, r.*, t.[Text] as 'Standard',t2.[text] as 'Division' from ExamScheduleLns el " +
                             " inner join Registration r on r.RegistrationId = el.RegistrationId " +
                             " inner join Textlists t on t.TextListId = r.StandardId " +
                             " left join Textlists t2 on t2.TextListId = r.DivisionTextListId " +
                             " where el.ExamScheduleId='" + ExamScheduleId + "' ";

        DataTable dt = new DataTable();
        dt.Load(sqlCmd.ExecuteReader());

        ExamScheduleDTO.dtRestrations = dt;

        _generalDAL.CloseSQLConnection();

        return ExamScheduleDTO;
    }

    public void Delete(string ExamScheduleId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..ExamSchedules(ExamScheduleId, StandardTextListId, SubId, TestId, TotalQuestions, ExamDate, ExamFromTime, ExamToTime, TotalMins, PerQueMins, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, NegativeMarks, PerQuestionTime,PatternId,AllowReview)" +
            " Select ExamScheduleId, StandardTextListId, SubId, TestId, TotalQuestions, ExamDate, ExamFromTime, ExamToTime, TotalMins, PerQueMins, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, NegativeMarks, PerQuestionTime,PatternId,AllowReview,IsDefaultTest " +
            " FROM ExamSchedules WHERE ExamScheduleId='" + ExamScheduleId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM ExamSchedules WHERE ExamScheduleId='" + ExamScheduleId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..ExamScheduleLns(ExamScheduleLnId, ExamScheduleId, LnNo, RegistrationId, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId)" +
            " Select ExamScheduleLnId, ExamScheduleId, LnNo, RegistrationId, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId " +
            " FROM ExamScheduleLns WHERE ExamScheduleId='" + ExamScheduleId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM ExamScheduleLns WHERE ExamScheduleId='" + ExamScheduleId + "'";
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

    public bool NamesExists(string StandardTextListId, string SubId, string TestId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        try
        {
            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = " SELECT COUNT(*) FROM ExamSchedules WHERE [StandardTextListId] = '" + StandardTextListId + "' " +
                                 " and [SubId] = '" + SubId + "' and [TestId] = '" + TestId + "' ";

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
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }
    public bool NamesExists(string StandardTextListId, string SubId, string TestId, string ExamScheduleId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT COUNT(*) FROM ExamSchedules WHERE [StandardTextListId] = '" + StandardTextListId + "' " +
                                 " and [SubId] = '" + SubId + "' and [TestId] = '" + TestId + "' AND NOT ExamScheduleId='" + ExamScheduleId + "'";


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
        catch
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception();
        }
    }

    public string IsReferenced(string ExamScheduleId)
    {
        string strRef = "";

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        try
        {
            if (ExamScheduleId != null)
            {
                strRef += _generalDAL.IsReferenced("ExamSchedules", "ExamScheduleId", ExamScheduleId, sqlCmd, "'ExamScheduleLns'");
            }
            _generalDAL.CloseSQLConnection();

            return strRef;
        }
        catch
        {
            _generalDAL.CloseSQLConnection();
            return "";
            throw new Exception();
        }
    }

    //public DataTable LoadSubjects()
    //{
    //    try
    //    {
    //        SqlCommand sqlCmd = new SqlCommand();
    //        DataTable dt = new DataTable();

    //        _generalDAL.OpenSQLConnection();

    //        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
    //        sqlCmd.CommandText = "select * From Subs order by Name";

    //        dt.Load(sqlCmd.ExecuteReader());

    //        _generalDAL.CloseSQLConnection();

    //        return dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        _generalDAL.CloseSQLConnection();
    //        throw new Exception(ex.Message.ToString());
    //    }
    //}
    public DataTable LoadSubjects(string StandardTextListId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            //sqlCmd.CommandText = "select * From Subs where StandardTextListId = '" + StandardTextListId.ToString() + "' order by Name";


            if (ConfigurationManager.AppSettings["RightsBased"] != null)
            {
                if (Convert.ToBoolean(ConfigurationManager.AppSettings["RightsBased"]))
                {
                    if (MySession.UserID.ToString().ToUpper() == "aaa".ToString().ToUpper())
                    {
                        sqlCmd.CommandText = "select * From Subs where StandardTextListId = '" + StandardTextListId.ToString() + "' order by Name";
                    }
                    else
                    {
                        sqlCmd.CommandText = " select s.* From Subs s " +
                                             " inner join EmpCredentialDeptProjects ec on ec.TextListId = s.SubId " +
                                             " where s.StandardTextListId = '" + StandardTextListId.ToString() + "' " +
                                             " and ec.EmpId = '" + MySession.UserUnique.ToString() + "' order by s.Name";
                    }
                }
                else
                {
                    sqlCmd.CommandText = "select * From Subs where StandardTextListId = '" + StandardTextListId.ToString() + "' order by Name";
                }
            }
            else
            {
                sqlCmd.CommandText = "select * From Subs where StandardTextListId = '" + StandardTextListId.ToString() + "' order by Name";
            }

            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }

    public DataTable LoadTest(string SubId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select * from Tests where SubId = '" + SubId.ToString() + "' order by TestName";

            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }

    public DataTable LoadQuestions(string SubId, string TestId, string PatternId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            if (SubId != "" && SubId != null)
                sqlCmd.CommandText = "select count(*) from Ques where SubId = '" + SubId.ToString() + "' and TestId = '" + TestId.ToString() + "' ";
            else
                sqlCmd.CommandText = "select count(*) from Ques where SubId in (Select Distinct Subid from PatternLns where PatternId = '" + PatternId.ToString() + "' ) and TestId = '" + TestId.ToString() + "' ";

            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }

    public DataTable LoadExamsOnSameDate(DateTime? ExamDate, string StandardId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = " select t.[Text] as 'Standard', s.Name as 'Subject', ts.TestName, convert(varchar, e.ExamDate, 106) as ExamDate, " +
                                 " e.ExamFromTime, e.ExamToTime, e.TotalMins from ExamSchedules e " +
                                 " inner join TextLists t on t.TextListId = e.StandardTextListId " +
                                 " inner join Subs s on s.SubId = e.SubId " +
                                 " inner join Tests ts on ts.TestId = e.TestId " +
                                 " where e.ExamDate = '" + Convert.ToDateTime(ExamDate).ToString("dd-MMM-yyyy") + "' " +
                                 " and e.StandardTextListId = '" + StandardId.ToString() + "' order by ExamFromTime ";

            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }

    public DataTable LoadStudent(string StandardId, string DivisionTextListId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            string Append = "";

            if (DivisionTextListId != null)
                Append = " And r.DivisionTextListId = '" + DivisionTextListId + "'";

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();

            //sqlCmd.CommandText = " select 1 as 'Tick', r.*, t.[Text] as 'Standard', t2.[Text] as 'Division' from Registration r " +
            //    " inner join Textlists t on t.TextListId = r.StandardId " +
            //    " left join TextLists t2 on t2.TextListID = r.DivisionTextListId" +
            //    " where r.StandardId = '" + StandardId.ToString() + "' " + Append + " and isnull(r.IsDeActive, 0) = 0 order by r.FirstName  ";

            sqlCmd.CommandText = " select 1 as 'Tick', r.*,t.TextListId AS 'StandardId', t.[Text] as 'Standard', t2.[Text] as 'Division' ,a.FirstName ,a.MobileNo from RegistrationJobProfileLns r " +
                                 " left Join Registration a on a.RegistrationId = r.RegistrationId "+
                                 " left join Jobofferings J on J.JobOfferingId = r.JobOfferingId  "+
                                 " inner join Textlists t on t.TextListId = J.DepartmentId " +
                                 " left join TextLists t2 on t2.TextListID = J.DivisionId" +
                                 " where J.DepartmentId = '" + StandardId.ToString() + "' " + Append + "  order by a.FirstName  ";

            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }

    public DataTable LoadScheduleList(string StandardId, string SubId, string TestId, DateTime? fromExamDate, DateTime? toExamDate)
    {
        try
        {
            string where = "";
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            where = null;

            if (StandardId != null)
            {
                if (where != null)
                {
                    where += " AND ";
                }
                where += " e.StandardTextListId  = '" + StandardId + "'";
            }

            if (SubId != null)
            {
                if (where != null)
                {
                    where += " AND ";
                }
                where += " e.SubId  = '" + SubId + "'";
            }

            if (TestId != null)
            {
                if (where != null)
                {
                    where += " AND ";
                }
                where += " e.TestId  = '" + TestId + "'";
            }

            if (fromExamDate != null)
            {
                if (where != null)
                {
                    where += " AND ";
                }
                where += " e.ExamDate >= '" + Convert.ToDateTime(fromExamDate).ToString("dd-MMM-yyyy") + "'";
            }

            if (toExamDate != null)
            {
                if (where != null)
                {
                    where += " AND ";
                }
                where += " e.ExamDate <= '" + Convert.ToDateTime(toExamDate).ToString("dd-MMM-yyyy") + "'";
            }

            if (where != null)
            {
                where = " WHERE " + where;
            }

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = " select e.*, s.Name as 'SubjectName', ts.TestName, t.[Text] as 'Standard',p.PatternName From ExamSchedules e " +
                                 " left join Subs s on s.SubId = e.SubId " +
                                 " inner join Tests ts on ts.TestId = e.TestId " +
                                 " left join Patterns p on p.PatternId = e.PatternId " +
                                 " inner join TextLists t on t.TextListId = e.StandardTextListId " + where + " order by InsertedOn desc ";

            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }

    public DataTable CommonOTP()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = " select * from Facets where FacetName = 'CommonOTP'  ";


            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }

    public DataTable CommonOTPTeacher()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = " select * from Facets where FacetName = 'CommonOTPTeacher'  ";

            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }
    //BY BM For Update Minimum Passing Marks
    public DataTable MinimumMarks()
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = " select * from Facets where FacetName = 'MinPassMarks'  ";

            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }
    public void UpdateMinMarks(string MinMarks, string Id)
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
            sqlCmd.CommandText = " update Facets set FacetText = '" + MinMarks.ToString().Replace("'", "''") + "' where FacetsId = '" + Id.ToString() + "' ";
            sqlCmd.ExecuteNonQuery();

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }
    //End
    public void UpdateCommonOTP(string OTP, string Id)
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
            sqlCmd.CommandText = " update Facets set FacetText = '" + OTP.ToString().Replace("'", "''") + "' where FacetsId = '" + Id.ToString() + "' ";
            sqlCmd.ExecuteNonQuery();

            sqlTrans.Commit();
            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            sqlTrans.Rollback();
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }

    public DataTable LoadPatterns(string StandardId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = " select * from Patterns p " +
                                 " where p.StandardTextlistId = '" + StandardId.ToString() + "' order by p.PatternName ";

            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }

    public DataTable LoadTestFromPatterns(string PatternId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select * from Tests where PatternId = '" + PatternId.ToString() + "' order by TestName";

            dt.Load(sqlCmd.ExecuteReader());

            _generalDAL.CloseSQLConnection();

            return dt;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }
}
