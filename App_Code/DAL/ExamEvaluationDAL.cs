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

public class ExamEvaluationDAL
{
    public GeneralDAL _generalDAL;

    public ExamEvaluationDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~ExamEvaluationDAL()
    {
        _generalDAL = null;
    }

    public void Insert(ExamEvaluationDTO ExamEvaluationDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string ExamEvaluationId;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            int cntExist = 0;

            sqlCmd.CommandText = " select count(*) from ExamEvaluations where ExamId = '" + ExamEvaluationDTO.ExamId + "' and ImageNo = '" + ExamEvaluationDTO.ImageNo + "' ";
            cntExist = Convert.ToInt16(sqlCmd.ExecuteScalar());

            if (cntExist == 0)
            {
                sqlCmd.CommandText = " DECLARE  @ExamEvaluationId uniqueidentifier;" +
                                     " SET @ExamEvaluationId = NewId()" +
                                     " INSERT INTO ExamEvaluations(ExamEvaluationId, QueId, UserId, ExamId, ImageNo, ImagePath, TotalObtMark, " +
                                     " InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, Remarks)" +
                                     " VALUES(@ExamEvaluationId," +
                                     ((ExamEvaluationDTO.QueId == null) ? "NULL" : "'" + ExamEvaluationDTO.QueId.Replace("'", "''") + "'") + "," +
                                     ((ExamEvaluationDTO.UserId == null) ? "NULL" : "'" + ExamEvaluationDTO.UserId.ToString() + "'") + "," +
                                     ((ExamEvaluationDTO.ExamId == null) ? "NULL" : "'" + ExamEvaluationDTO.ExamId.Replace("'", "''") + "'") + "," +
                                     ((ExamEvaluationDTO.ImageNo == null) ? "NULL" : "'" + ExamEvaluationDTO.ImageNo + "'") + "," +
                                     ((ExamEvaluationDTO.ImagePath == null) ? "NULL" : "'" + ExamEvaluationDTO.ImagePath.Replace("'", "''") + "'") + "," +
                                     ((ExamEvaluationDTO.TotalObtMark == null) ? "NULL" : "'" + ExamEvaluationDTO.TotalObtMark.Replace("'", "''") + "'") + "," +
                                     " GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL, " +
                                     ((ExamEvaluationDTO.Remarks == null) ? "NULL" : "N'" + ExamEvaluationDTO.Remarks.Replace("'", "''") + "'") +
                                     " );" +
                                     " SELECT @ExamEvaluationId";

                ExamEvaluationId = sqlCmd.ExecuteScalar().ToString();

                if (ExamEvaluationDTO.SubMarks != null)
                {
                    string[] marks = ExamEvaluationDTO.SubMarks.Split(',');

                    if (marks.Length > 0)
                    {
                        for (int i = 0; i <= marks.Length - 1; i++)
                        {
                            sqlCmd.CommandText = " INSERT INTO ExamEvaluationLns(ExamEvaluationLnId, ExamEvaluationId, LnNo, ObtMark, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ExamId)" +
                                                 " VALUES(NEWID()," +
                                                 ((ExamEvaluationId == null) ? "NULL" : "'" + ExamEvaluationId.Replace("'", "''") + "'") + "," +
                                                 (((i + 1) == null) ? "NULL" : "'" + (i + 1) + "'") + "," +
                                                 ((marks[i] == null) ? "NULL" : "'" + marks[i].ToString().Replace("'", "''") + "'") + "," +
                                                 " GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL, " +
                                                 ((ExamEvaluationDTO.ExamId == null) ? "NULL" : "'" + ExamEvaluationDTO.ExamId.Replace("'", "''") + "'") +
                                                 " ); ";
                            sqlCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            else
            {
                DataTable dtEdit = new DataTable();
                sqlCmd.CommandText = " select * from ExamEvaluations where ExamId = '" + ExamEvaluationDTO.ExamId + "' and ImageNo = '" + ExamEvaluationDTO.ImageNo + "' ";
                dtEdit.Load(sqlCmd.ExecuteReader());

                if (dtEdit.Rows.Count > 0)
                {
                    ExamEvaluationId = dtEdit.Rows[0]["ExamEvaluationId"].ToString();

                    if (ExamEvaluationDTO.TotalObtMark != null || ExamEvaluationDTO.Remarks != null)
                    {
                        if ((Convert.ToDecimal(ExamEvaluationDTO.TotalObtMark) != ((dtEdit.Rows[0]["TotalObtMark"] == DBNull.Value) ? 0 : Convert.ToDecimal(dtEdit.Rows[0]["TotalObtMark"])))
                            || ((ExamEvaluationDTO.Remarks == null) ? true : (ExamEvaluationDTO.Remarks.ToString().ToUpper() != Convert.ToString(dtEdit.Rows[0]["Remarks"]).ToUpper())))
                        {
                            sqlCmd.CommandText = " Insert Into ExamEvaluations_History(ExamEvaluationId, QueId, UserId, ExamId, ImageNo, ImagePath, TotalObtMark, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, Remarks) " +
                                                 " Select ExamEvaluationId, QueId, UserId, ExamId, ImageNo, ImagePath, TotalObtMark, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, Remarks " +
                                                 " FROM ExamEvaluations WHERE ExamEvaluationId = '" + ExamEvaluationId + "' ";
                            sqlCmd.ExecuteNonQuery();

                            sqlCmd.CommandText = " Insert Into ExamEvaluationLns_History(ExamEvaluationLnId, ExamEvaluationId, LnNo, ObtMark, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ExamId) " +
                                                 " Select ExamEvaluationLnId, ExamEvaluationId, LnNo, ObtMark, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, ExamId " +
                                                 " FROM ExamEvaluationLns WHERE ExamEvaluationId = '" + ExamEvaluationId + "' ";
                            sqlCmd.ExecuteNonQuery();

                            sqlCmd.CommandText = " delete ExamEvaluationLns WHERE ExamEvaluationId = '" + ExamEvaluationId + "' ";
                            sqlCmd.ExecuteNonQuery();

                            sqlCmd.CommandText = " update ExamEvaluations set " +
                                                 " TotalObtMark = " + ((ExamEvaluationDTO.TotalObtMark == null) ? "NULL" : "'" + ExamEvaluationDTO.TotalObtMark.Replace("'", "''") + "'") + " " +
                                                 " , Remarks = " + ((ExamEvaluationDTO.Remarks == null) ? "NULL" : "N'" + ExamEvaluationDTO.Remarks.Replace("'", "''") + "'") + " " +
                                                 " where ExamEvaluationId = '" + ExamEvaluationId.ToString() + "' ";
                            sqlCmd.ExecuteNonQuery();

                            if (ExamEvaluationDTO.SubMarks != null)
                            {
                                string[] marks = ExamEvaluationDTO.SubMarks.Split(',');

                                if (marks.Length > 0)
                                {
                                    for (int i = 0; i <= marks.Length - 1; i++)
                                    {
                                        sqlCmd.CommandText = " INSERT INTO ExamEvaluationLns(ExamEvaluationLnId, ExamEvaluationId, LnNo, ObtMark, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ExamId)" +
                                                             " VALUES(NEWID()," +
                                                             ((ExamEvaluationId == null) ? "NULL" : "'" + ExamEvaluationId.Replace("'", "''") + "'") + "," +
                                                             (((i + 1) == null) ? "NULL" : "'" + (i + 1) + "'") + "," +
                                                             ((marks[i] == null) ? "NULL" : "'" + marks[i].ToString().Replace("'", "''") + "'") + "," +
                                                             " GETDATE(),GETDATE(),'" + MySession.UserUnique + "',NULL, " +
                                                             ((ExamEvaluationDTO.ExamId == null) ? "NULL" : "'" + ExamEvaluationDTO.ExamId.Replace("'", "''") + "'") +
                                                             " ); ";
                                        sqlCmd.ExecuteNonQuery();
                                    }
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

    public void Delete(string ExamId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..ExamEvaluations(ExamEvaluationId, QueId, UserId, ExamId, ImageNo, ImagePath, TotalObtMark, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId) " +
            " Select ExamEvaluationId, QueId, UserId, ExamId, ImageNo, ImagePath, TotalObtMark, getdate(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId " +
            " FROM ExamEvaluations WHERE ExamId='" + ExamId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM ExamEvaluations WHERE ExamId='" + ExamId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..ExamEvaluationLns(ExamEvaluationLnId, ExamEvaluationId, LnNo, ObtMark, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ExamId) " +
            " Select ExamEvaluationLnId, ExamEvaluationId, LnNo, ObtMark, getdate(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, ExamId " +
            " FROM ExamEvaluationLns WHERE ExamId='" + ExamId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM ExamEvaluationLns WHERE ExamId='" + ExamId + "'";
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
            throw new Exception(ex.Message.ToString());
        }
    }

    public DataSet LoadAnsData(string QueId, string ExamId, string RegistrationId, string ExamScheduleId, int ImageNo)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataSet ds = new DataSet();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();

            sqlCmd.CommandText = " select q.SubId,q.QueId,q.Que as Question ,q.A1,q.A2,q.A3,q.A4,ee.Ans,ee.AnsStatus " +
                                 " , q.QueType " +
                                 " , replace(q.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                 " , q.ImageNameQus as ImageNameQusLocal " +
                                 " , replace(ee.AnsImage1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage1' " +
                                 " , replace(ee.AnsImage2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage2' " +
                                 " , replace(ee.AnsImage3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage3' " +
                                 " , replace(ee.AnsImage4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage4' " +
                                 " , replace(ee.AnsImage5, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage5' " +
                                 " , replace(ee.AnsImage6, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage6' " +
                                 " , replace(ee.AnsImage7, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage7' " +
                                 " , replace(ee.AnsImage8, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage8' " +
                                 " , replace(ee.AnsImage9, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage9' " +
                                 " , replace(ee.AnsImage10, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage10' " +
                                 " , replace(ee.AnsImage11, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage11' " +
                                 " , replace(ee.AnsImage12, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage12' " +
                                 " , replace(ee.AnsImage13, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage13' " +
                                 " , replace(ee.AnsImage14, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage14' " +
                                 " , replace(ee.AnsImage15, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage15' " +
                                 " , replace(ee.AnsImage16, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage16' " +
                                 " , replace(ee.AnsImage17, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage17' " +
                                 " , replace(ee.AnsImage18, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage18' " +
                                 " , replace(ee.AnsImage19, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage19' " +
                                 " , replace(ee.AnsImage20, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage20' " +
                                 " , replace(ee.AnsImage21, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage21' " +
                                 " , replace(ee.AnsImage22, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage22' " +
                                 " , replace(ee.AnsImage23, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage23' " +
                                 " , replace(ee.AnsImage24, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage24' " +
                                 " , replace(ee.AnsImage25, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage25' " +
                                 " , replace(ee.AnsImage26, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage26' " +
                                 " , replace(ee.AnsImage27, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage27' " +
                                 " , replace(ee.AnsImage28, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage28' " +
                                 " , replace(ee.AnsImage29, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage29' " +
                                 " , replace(ee.AnsImage30, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage30' " +
                                 " , replace(ee.AnsImage31, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage31' " +
                                 " , replace(ee.AnsImage32, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage32' " +
                                 " , replace(ee.AnsImage33, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage33' " +
                                 " , replace(ee.AnsImage34, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage34' " +
                                 " , replace(ee.AnsImage35, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage35' " +
                                 " , replace(ee.AnsImage36, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage36' " +
                                 " , replace(ee.AnsImage37, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage37' " +
                                 " , replace(ee.AnsImage38, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage38' " +
                                 " , replace(ee.AnsImage39, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage39' " +
                                 " , replace(ee.AnsImage40, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage40' " +
                                 " , s.Name as 'SubjectName', tx.[Text] as 'Standard', t.TestName " +
                                 " , e.ExamFromTime " +
                                 " , ee.AnsImage1 as AnsImage1Local " +
                                 " , ee.AnsImage2 as AnsImage2Local " +
                                 " , ee.AnsImage3 as AnsImage3Local " +
                                 " , ee.AnsImage4 as AnsImage4Local " +
                                 " , ee.AnsImage5 as AnsImage5Local " +
                                 " , ee.AnsImage6 as AnsImage6Local " +
                                 " , ee.AnsImage7 as AnsImage7Local " +
                                 " , ee.AnsImage8 as AnsImage8Local " +
                                 " , ee.AnsImage9 as AnsImage9Local " +
                                 " , ee.AnsImage10 as AnsImage10Local " +
                                 " , ee.AnsImage11 as AnsImage11Local " +
                                 " , ee.AnsImage12 as AnsImage12Local " +
                                 " , ee.AnsImage13 as AnsImage13Local " +
                                 " , ee.AnsImage14 as AnsImage14Local " +
                                 " , ee.AnsImage15 as AnsImage15Local " +
                                 " , ee.AnsImage16 as AnsImage16Local " +
                                 " , ee.AnsImage17 as AnsImage17Local " +
                                 " , ee.AnsImage18 as AnsImage18Local " +
                                 " , ee.AnsImage19 as AnsImage19Local " +
                                 " , ee.AnsImage20 as AnsImage20Local " +
                                 " , ee.AnsImage21 as AnsImage21Local " +
                                 " , ee.AnsImage22 as AnsImage22Local " +
                                 " , ee.AnsImage23 as AnsImage23Local " +
                                 " , ee.AnsImage24 as AnsImage24Local " +
                                 " , ee.AnsImage25 as AnsImage25Local " +
                                 " , ee.AnsImage26 as AnsImage26Local " +
                                 " , ee.AnsImage27 as AnsImage27Local " +
                                 " , ee.AnsImage28 as AnsImage28Local " +
                                 " , ee.AnsImage29 as AnsImage29Local " +
                                 " , ee.AnsImage30 as AnsImage30Local " +
                                 " , ee.AnsImage31 as AnsImage31Local " +
                                 " , ee.AnsImage32 as AnsImage32Local " +
                                 " , ee.AnsImage33 as AnsImage33Local " +
                                 " , ee.AnsImage34 as AnsImage34Local " +
                                 " , ee.AnsImage35 as AnsImage35Local " +
                                 " , ee.AnsImage36 as AnsImage36Local " +
                                 " , ee.AnsImage37 as AnsImage37Local " +
                                 " , ee.AnsImage38 as AnsImage38Local " +
                                 " , ee.AnsImage39 as AnsImage39Local " +
                                 " , ee.AnsImage40 as AnsImage40Local " +
                                 " , q.RightMarks as 'TotalMarks' " +
                                 " , (select sum(TotalObtMark) from ExamEvaluations x where x.ExamId = ee.ExamId) as 'ObtainedMarks' " +
                                 " From ExamSchedules e " +
                                 " inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
                                 " left join Exams ee on ee.ExamScheduleId = e.ExamScheduleId and ee.ExamId = '" + ExamId.ToString() + "' " +
                                 " inner join Ques q on ee.QueId = q.QueId " +
                                 " inner join Subs s on s.SubId = q.SubId " +
                                 " inner join TextLists tx on tx.TextListId = s.StandardTextListId " +
                                 " inner join Tests t on t.TestId = q.TestId " +
                                 " where ee.ExamId = '" + ExamId.ToString() + "' and ee.RegistrationId = '" + RegistrationId.ToString() + "' " +
                                 " and ee.ExamScheduleId = '" + ExamScheduleId.ToString() + "' and q.QueId  ='" + QueId.ToString() + "' ";

            DataTable dt = new DataTable();
            dt.Load(sqlCmd.ExecuteReader());

            ds.Tables.Add(dt);

            //sqlCmd.CommandText = " select * " +
            //                     " , replace(ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImagePathLive' " +
            //                     " from ExamEvaluations where ExamId = '" + ExamId.ToString() + "' and QueId  ='" + QueId.ToString() + "' and ImageNo = '" + ImageNo + "' order by InsertedOn desc ";
            //DataTable dt1 = new DataTable();
            //dt1.Load(sqlCmd.ExecuteReader());

            //ds.Tables.Add(dt1);

            //sqlCmd.CommandText = " select * " +
            //                     " from ExamEvaluationLns where ExamEvaluationId in (select ExamEvaluationId from ExamEvaluations where ExamId = '" + ExamId.ToString() + "' and QueId  ='" + QueId.ToString() + "' and ImageNo = '" + ImageNo + "') order by LnNo ";
            //DataTable dt2 = new DataTable();
            //dt2.Load(sqlCmd.ExecuteReader());

            //ds.Tables.Add(dt2);

            _generalDAL.CloseSQLConnection();

            return ds;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }

    public DataSet LoadAnsDataForImageNo(string QueId, string ExamId, string RegistrationId, string ExamScheduleId, int ImageNo)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataSet ds = new DataSet();

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();

            sqlCmd.CommandText = " select * " +
                                 " , replace(ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImagePathLive' " +
                                 " from ExamEvaluations where ExamId = '" + ExamId.ToString() + "' and QueId  ='" + QueId.ToString() + "' and ImageNo = '" + ImageNo + "' order by InsertedOn desc ";
            DataTable dt1 = new DataTable();
            dt1.Load(sqlCmd.ExecuteReader());

            ds.Tables.Add(dt1);

            sqlCmd.CommandText = " select * " +
                                 " from ExamEvaluationLns where ExamEvaluationId in (select ExamEvaluationId from ExamEvaluations where ExamId = '" + ExamId.ToString() + "' and QueId  ='" + QueId.ToString() + "' and ImageNo = '" + ImageNo + "') order by LnNo ";
            DataTable dt2 = new DataTable();
            dt2.Load(sqlCmd.ExecuteReader());

            ds.Tables.Add(dt2);

            _generalDAL.CloseSQLConnection();

            return ds;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }

    public void UpdatePhotoPath(ExamEvaluationDTO ExamEvaluationDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlCmd = new SqlCommand();
        string ExamEvaluationId;

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Transaction = sqlTrans;

        try
        {
            sqlCmd.CommandText = " Insert Into Exams_History(ExamId, RegistrationId, SubId, QueId, Ans, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ExamScheduleId, TestId, QueType, AnsImage1, AnsImage2, AnsImage3, AnsImage4, AnsSelection, AnsStatus, AnsImage5, AnsImage6, AnsImage7, AnsImage8, AnsImage9, AnsImage10, AnsImage11, AnsImage12, AnsImage13, AnsImage14, AnsImage15, AnsImage16, AnsImage17, AnsImage18, AnsImage19, AnsImage20, AnsImage21, AnsImage22, AnsImage23, AnsImage24, AnsImage25, AnsImage26, AnsImage27, AnsImage28, AnsImage29, AnsImage30, AnsImage31, AnsImage32, AnsImage33, AnsImage34, AnsImage35, AnsImage36, AnsImage37, AnsImage38, AnsImage39, AnsImage40) " +
                                 " Select ExamId, RegistrationId, SubId, QueId, Ans, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, ExamScheduleId, TestId, QueType, AnsImage1, AnsImage2, AnsImage3, AnsImage4, AnsSelection, AnsStatus, AnsImage5, AnsImage6, AnsImage7, AnsImage8, AnsImage9, AnsImage10, AnsImage11, AnsImage12, AnsImage13, AnsImage14, AnsImage15, AnsImage16, AnsImage17, AnsImage18, AnsImage19, AnsImage20, AnsImage21, AnsImage22, AnsImage23, AnsImage24, AnsImage25, AnsImage26, AnsImage27, AnsImage28, AnsImage29, AnsImage30, AnsImage31, AnsImage32, AnsImage33, AnsImage34, AnsImage35, AnsImage36, AnsImage37, AnsImage38, AnsImage39, AnsImage40 " +
                                 " FROM Exams WHERE ExamId = '" + ExamEvaluationDTO.ExamId + "' ";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = " update Exams set " +
                                 " AnsImage" + ExamEvaluationDTO.ImageNo + " = " + ((ExamEvaluationDTO.RotatedImagePath == null) ? "NULL" : "'" + ExamEvaluationDTO.RotatedImagePath.Replace("'", "''") + "'") + " " +
                                 " where ExamId = '" + ExamEvaluationDTO.ExamId + "' ";
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
}
