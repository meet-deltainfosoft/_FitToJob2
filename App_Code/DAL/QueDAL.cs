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
using System.IO;
using System.Collections;

/// <summary>
/// Summary description for QueDAL
/// </summary>
public class QueDAL
{
    private GeneralDAL _generalDAL;

    public QueDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~QueDAL()
    {
        _generalDAL = null;
    }
    public QueDTO Select(string QueId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        SqlDataReader dr;
        QueDTO QueDTO = new QueDTO();
        //SqlDataReader reader = sqlCmd.ExecuteReader();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT a.Que,a.QueId,a.A1,a.A2,a.A3,a.A4,a.Ans,b.SubId,b.Name " +
                             " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                             " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                             " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                             " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                             " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                             " , b.StandardTextListId, a.TestId, a.SrNo, a.QueType, a.QueDataType, a.RightMarks, a.WrongMarks, a.NonMarks, a.NoOfFile " +
                             " , replace(a.SampleAns1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns1' " +
                             " , replace(a.SampleAns2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns2' " +
                             " , replace(a.SampleAns3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns3' " +
                             " , replace(a.SampleAns4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'SampleAns4' " +
                             " , a.AnsSelection, a.NoOfSubQues " +
                             " FROM Ques a " +
                             " inner join Subs b on b.SubId= a.SubId " +
                             " WHERE QueId='" + QueId + "'";
        dr = sqlCmd.ExecuteReader();

        while (dr.Read())
        {
            if (dr["QueId"] != DBNull.Value)
                QueDTO.QueId = dr["QueId"].ToString();

            if (dr["SubId"] != DBNull.Value)
                QueDTO.SubId = dr["SubId"].ToString();

            if (dr["Que"] != DBNull.Value)
                QueDTO.Que = dr["Que"].ToString();

            if (dr["A1"] != DBNull.Value)
                QueDTO.A1 = dr["A1"].ToString();

            if (dr["A2"] != DBNull.Value)
                QueDTO.A2 = dr["A2"].ToString();

            if (dr["A3"] != DBNull.Value)
                QueDTO.A3 = dr["A3"].ToString();

            if (dr["A4"] != DBNull.Value)
                QueDTO.A4 = dr["A4"].ToString();

            if (dr["Ans"] != DBNull.Value)
                QueDTO.Ans = (dr["Ans"].ToString());
            else
                QueDTO.Ans = null;

            if (dr["Name"] != DBNull.Value)
                QueDTO.Subject = dr["Name"].ToString();


            if (dr["ImageNameQus"] != DBNull.Value)
                QueDTO.ImageNameQus = dr["ImageNameQus"].ToString();
            else
                QueDTO.ImageNameQus = null;


            if (dr["ImageNameA1"] != DBNull.Value)
                QueDTO.ImageNameA1 = dr["ImageNameA1"].ToString();
            else
                QueDTO.ImageNameA1 = null;

            if (dr["ImageNameA2"] != DBNull.Value)
                QueDTO.ImageNameA2 = dr["ImageNameA2"].ToString();
            else
                QueDTO.ImageNameA2 = null;

            if (dr["ImageNameA3"] != DBNull.Value)
                QueDTO.ImageNameA3 = dr["ImageNameA3"].ToString();
            else
                QueDTO.ImageNameA3 = null;

            if (dr["ImageNameA4"] != DBNull.Value)
                QueDTO.ImageNameA4 = dr["ImageNameA4"].ToString();
            else
                QueDTO.ImageNameA4 = null;

            if (dr["StandardTextListId"] != DBNull.Value)
                QueDTO.StandardTextListId = dr["StandardTextListId"].ToString();
            else
                QueDTO.StandardTextListId = null;

            if (dr["TestId"] != DBNull.Value)
                QueDTO.TestId = dr["TestId"].ToString();
            else
                QueDTO.TestId = null;

            if (dr["SrNo"] != DBNull.Value)
                QueDTO.SrNo = Convert.ToInt16(dr["SrNo"].ToString());
            else
                QueDTO.SrNo = null;

            if (dr["QueType"] != DBNull.Value)
                QueDTO.QueType = dr["QueType"].ToString();
            else
                QueDTO.QueType = null;

            if (dr["QueDataType"] != DBNull.Value)
                QueDTO.QueDataType = dr["QueDataType"].ToString();
            else
                QueDTO.QueDataType = null;

            if (dr["RightMarks"] != DBNull.Value)
                QueDTO.RightMarks = Convert.ToDecimal(dr["RightMarks"].ToString());
            else
                QueDTO.RightMarks = null;

            if (dr["WrongMarks"] != DBNull.Value)
                QueDTO.WrongMarks = Convert.ToDecimal(dr["WrongMarks"].ToString());
            else
                QueDTO.WrongMarks = null;

            if (dr["NonMarks"] != DBNull.Value)
                QueDTO.NonMarks = Convert.ToDecimal(dr["NonMarks"].ToString());
            else
                QueDTO.NonMarks = null;

            if (dr["NoOfFile"] != DBNull.Value)
                QueDTO.NoOfFile = Convert.ToInt16(dr["NoOfFile"].ToString());
            else
                QueDTO.NoOfFile = null;

            if (dr["SampleAns1"] != DBNull.Value)
                QueDTO.SampleAns1 = dr["SampleAns1"].ToString();
            else
                QueDTO.SampleAns1 = null;

            if (dr["SampleAns2"] != DBNull.Value)
                QueDTO.SampleAns2 = dr["SampleAns2"].ToString();
            else
                QueDTO.SampleAns2 = null;

            if (dr["SampleAns3"] != DBNull.Value)
                QueDTO.SampleAns3 = dr["SampleAns3"].ToString();
            else
                QueDTO.SampleAns3 = null;

            if (dr["SampleAns4"] != DBNull.Value)
                QueDTO.SampleAns4 = dr["SampleAns4"].ToString();
            else
                QueDTO.SampleAns4 = null;

            if (dr["AnsSelection"] != DBNull.Value)
                QueDTO.AnsSelection = dr["AnsSelection"].ToString();
            else
                QueDTO.AnsSelection = null;

            if (dr["NoOfSubQues"] != DBNull.Value)
                QueDTO.NoOfSubQues = Convert.ToInt16(dr["NoOfSubQues"]);
            else
                QueDTO.NoOfSubQues = null;
        }
        dr.Close();

        _generalDAL.CloseSQLConnection();

        return QueDTO;
    }
    public void Insert(QueDTO QueDTO)
    {
        //Escape single quote
        if (QueDTO.Que != null)
            QueDTO.Que = QueDTO.Que.Replace("'", "''");

        if (QueDTO.A1 != null)
            QueDTO.A1 = QueDTO.A1.Replace("'", "''");

        if (QueDTO.A2 != null)
            QueDTO.A2 = QueDTO.A2.Replace("'", "''");

        if (QueDTO.A3 != null)
            QueDTO.A3 = QueDTO.A3.Replace("'", "''");

        if (QueDTO.A4 != null)
            QueDTO.A4 = QueDTO.A4.Replace("'", "''");

        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();

        try
        {
            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO Ques(QueId,SubId,Que,A1,A2,A3,A4,Ans,ImageNameQus,ImageNameA1,ImageNameA2,ImageNameA3,ImageNameA4,InsertedOn,LastUpdatedOn," +
                                 "InsertedByUserId,LastUpdatedByUserId, TestId, SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, SampleAns1, " +
                                 "SampleAns2, SampleAns3, SampleAns4, AnsSelection, NoOfSubQues)" +
                                 " VALUES(NewID()" +
                                 "," + ((QueDTO.SubId == null) ? "NULL" : "'" + QueDTO.SubId + "'") +
                                 "," + ((QueDTO.Que == null) ? "NULL" : "N'" + QueDTO.Que.ToString() + "'") +
                                 "," + ((QueDTO.A1 == null) ? "NULL" : "N'" + QueDTO.A1 + "'") +
                                 "," + ((QueDTO.A2 == null) ? "NULL" : "N'" + QueDTO.A2 + "'") +
                                 "," + ((QueDTO.A3 == null) ? "NULL" : "N'" + QueDTO.A3 + "'") +
                                 "," + ((QueDTO.A4 == null) ? "NULL" : "N'" + QueDTO.A4 + "'") +
                                 "," + ((QueDTO.Ans == null) ? "NULL" : "N'" + QueDTO.Ans.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueDTO.ImageNameQus == null) ? "NULL" : "'" + QueDTO.ImageNameQus.ToString() + "'") +
                                 "," + ((QueDTO.ImageNameA1 == null) ? "NULL" : "'" + QueDTO.ImageNameA1.ToString() + "'") +
                                 "," + ((QueDTO.ImageNameA2 == null) ? "NULL" : "'" + QueDTO.ImageNameA2.ToString() + "'") +
                                 "," + ((QueDTO.ImageNameA3 == null) ? "NULL" : "'" + QueDTO.ImageNameA3.ToString() + "'") +
                                 "," + ((QueDTO.ImageNameA4 == null) ? "NULL" : "'" + QueDTO.ImageNameA4.ToString() + "'") +
                                 " ,GETDATE(),GETDATE(),NULL,NULL" +
                                 "," + ((QueDTO.TestId == null) ? "NULL" : "'" + QueDTO.TestId.ToString() + "'") +
                                 "," + ((QueDTO.SrNo == null) ? "NULL" : "'" + QueDTO.SrNo.ToString() + "'") +
                                 "," + ((QueDTO.QueType == null) ? "NULL" : "'" + QueDTO.QueType.ToString() + "'") +
                                 "," + ((QueDTO.QueDataType == null) ? "NULL" : "'" + QueDTO.QueDataType.ToString() + "'") +
                                 "," + ((QueDTO.RightMarks == null) ? "NULL" : "'" + QueDTO.RightMarks.ToString() + "'") +
                                 "," + ((QueDTO.WrongMarks == null) ? "NULL" : "'" + QueDTO.WrongMarks.ToString() + "'") +
                                 "," + ((QueDTO.NonMarks == null) ? "NULL" : "'" + QueDTO.NonMarks.ToString() + "'") +
                                 "," + ((QueDTO.NoOfFile == null) ? "NULL" : "'" + QueDTO.NoOfFile.ToString() + "'") +
                                 "," + ((QueDTO.SampleAns1 == null) ? "NULL" : "'" + QueDTO.SampleAns1.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueDTO.SampleAns2 == null) ? "NULL" : "'" + QueDTO.SampleAns2.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueDTO.SampleAns3 == null) ? "NULL" : "'" + QueDTO.SampleAns3.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueDTO.SampleAns4 == null) ? "NULL" : "'" + QueDTO.SampleAns4.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueDTO.AnsSelection == null) ? "NULL" : "'" + QueDTO.AnsSelection.ToString().Replace("'", "''") + "'") +
                                 "," + ((QueDTO.NoOfSubQues == null) ? "NULL" : "'" + QueDTO.NoOfSubQues.ToString().Replace("'", "''") + "'") +
                                 ")";
            sqlCmd.ExecuteNonQuery();

            _generalDAL.CloseSQLConnection();
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw ex;
        }
    }
    public void Update(QueDTO QueDTO)
    {
        //Escape single quote
        if (QueDTO.Que != null)
            QueDTO.Que = QueDTO.Que.Replace("'", "''");

        if (QueDTO.A1 != null)
            QueDTO.A1 = QueDTO.A1.Replace("'", "''");

        if (QueDTO.A2 != null)
            QueDTO.A2 = QueDTO.A2.Replace("'", "''");

        if (QueDTO.A3 != null)
            QueDTO.A3 = QueDTO.A3.Replace("'", "''");

        if (QueDTO.A4 != null)
            QueDTO.A4 = QueDTO.A4.Replace("'", "''");
        //Escape single quote

        SqlCommand sqlCmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "UPDATE Ques SET " +
                             " SubId = " + ((QueDTO.SubId == null) ? "NULL" : "'" + QueDTO.SubId + "'") + "" +
                             ",Que=" + ((QueDTO.Que == null) ? "NULL" : "N'" + QueDTO.Que + "'") + "" +
                             ",A1=" + ((QueDTO.A1 == null) ? "NULL" : "N'" + QueDTO.A1 + "'") + "" +
                             ",A2=" + ((QueDTO.A2 == null) ? "NULL" : "N'" + QueDTO.A2 + "'") + "" +
                             ",A3=" + ((QueDTO.A3 == null) ? "NULL" : "N'" + QueDTO.A3 + "'") + "" +
                             ",A4=" + ((QueDTO.A4 == null) ? "NULL" : "N'" + QueDTO.A4 + "'") + "" +
                             ",Ans=" + ((QueDTO.Ans == null) ? "NULL" : "N'" + QueDTO.Ans.ToString().Replace("'", "''") + "'") + "" +
                             ",LastUpdatedOn=GETDATE()" +
                             ",LastUpdatedByUserId=NULL" +
                             ",TestId  =" + ((QueDTO.TestId == null) ? "NULL" : "'" + QueDTO.TestId.ToString() + "'") +
                             ",SrNo  =" + ((QueDTO.SrNo == null) ? "NULL" : "'" + QueDTO.SrNo.ToString() + "'") +
                             ",QueType  =" + ((QueDTO.QueType == null) ? "NULL" : "'" + QueDTO.QueType.ToString() + "'") +
                             ",QueDataType  =" + ((QueDTO.QueDataType == null) ? "NULL" : "'" + QueDTO.QueDataType.ToString() + "'") +
                             ",RightMarks  =" + ((QueDTO.RightMarks == null) ? "NULL" : "'" + QueDTO.RightMarks.ToString() + "'") +
                             ",WrongMarks  =" + ((QueDTO.WrongMarks == null) ? "NULL" : "'" + QueDTO.WrongMarks.ToString() + "'") +
                             ",NonMarks  =" + ((QueDTO.NonMarks == null) ? "NULL" : "'" + QueDTO.NonMarks.ToString() + "'") +
                             ",NoOfFile  =" + ((QueDTO.NoOfFile == null) ? "NULL" : "'" + QueDTO.NoOfFile.ToString() + "'") +
                             ",AnsSelection  =" + ((QueDTO.AnsSelection == null) ? "NULL" : "'" + QueDTO.AnsSelection.ToString() + "'") +
                             ",NoOfSubQues  =" + ((QueDTO.NoOfSubQues == null) ? "NULL" : "'" + QueDTO.NoOfSubQues.ToString() + "'") +
                             " WHERE QueId='" + QueDTO.QueId + "'";
        sqlCmd.ExecuteNonQuery();

        if (QueDTO.IsQueChanged == true)
        {
            sqlCmd.CommandText = " update Ques set ImageNameQus  =" + ((QueDTO.ImageNameQus == null) ? "NULL" : "'" + QueDTO.ImageNameQus.ToString() + "'") +
                                 " where QueId='" + QueDTO.QueId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueDTO.IsA1Changed == true)
        {
            sqlCmd.CommandText = " update Ques set ImageNameA1  =" + ((QueDTO.ImageNameA1 == null) ? "NULL" : "'" + QueDTO.ImageNameA1.ToString() + "'") +
                                 " where QueId='" + QueDTO.QueId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueDTO.IsA2Changed == true)
        {
            sqlCmd.CommandText = " update Ques set ImageNameA2  =" + ((QueDTO.ImageNameA2 == null) ? "NULL" : "'" + QueDTO.ImageNameA2.ToString() + "'") +
                                 " where QueId='" + QueDTO.QueId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueDTO.IsA3Changed == true)
        {
            sqlCmd.CommandText = " update Ques set ImageNameA3  =" + ((QueDTO.ImageNameA3 == null) ? "NULL" : "'" + QueDTO.ImageNameA3.ToString() + "'") +
                                 " where QueId='" + QueDTO.QueId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueDTO.IsA4Changed == true)
        {
            sqlCmd.CommandText = " update Ques set ImageNameA4  =" + ((QueDTO.ImageNameA4 == null) ? "NULL" : "'" + QueDTO.ImageNameA4.ToString() + "'") +
                                 " where QueId='" + QueDTO.QueId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueDTO.IsSampleAns1Changed == true)
        {
            sqlCmd.CommandText = " update Ques set SampleAns1  = " + ((QueDTO.SampleAns1 == null) ? "NULL" : "'" + QueDTO.SampleAns1.ToString() + "'") +
                                 " where QueId='" + QueDTO.QueId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueDTO.IsSampleAns2Changed == true)
        {
            sqlCmd.CommandText = " update Ques set SampleAns2  = " + ((QueDTO.SampleAns2 == null) ? "NULL" : "'" + QueDTO.SampleAns2.ToString() + "'") +
                                 " where QueId='" + QueDTO.QueId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueDTO.IsSampleAns3Changed == true)
        {
            sqlCmd.CommandText = " update Ques set SampleAns3  = " + ((QueDTO.SampleAns3 == null) ? "NULL" : "'" + QueDTO.SampleAns3.ToString() + "'") +
                                 " where QueId='" + QueDTO.QueId + "'";
            sqlCmd.ExecuteNonQuery();
        }

        if (QueDTO.IsSampleAns4Changed == true)
        {
            sqlCmd.CommandText = " update Ques set SampleAns4  = " + ((QueDTO.SampleAns4 == null) ? "NULL" : "'" + QueDTO.SampleAns4.ToString() + "'") +
                                 " where QueId='" + QueDTO.QueId + "'";
            sqlCmd.ExecuteNonQuery();
        }
    }

    public void Delete(string QueId)
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

            sqlCmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..Ques(QueId, SubId, Que, A1, A2, A3, A4, Ans, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3, ImageNameA4, TestId, Hashtag, SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection, NoOfSubQues)" +
            " Select QueId, SubId, Que, A1, A2, A3, A4, Ans, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3, ImageNameA4, TestId, Hashtag, SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection, NoOfSubQues " +
            " FROM Ques WHERE QueId='" + QueId + "'";
            sqlCmd.ExecuteNonQuery();

            sqlCmd.CommandText = "DELETE FROM Ques WHERE QueId='" + QueId + "'";
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
    public string IsReferenced(string QueId)
    {
        SqlCommand sqlCmd = new SqlCommand();
        string strRef = "";

        _generalDAL.OpenSQLConnection();

        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        try
        {
            if (QueId != null)
            {
                strRef += _generalDAL.IsReferenced("Ques", "QueId", QueId, sqlCmd, null);
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

    public int getSrNo(string SubId, string TestId)
    {
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            int Sr;

            _generalDAL.OpenSQLConnection();

            sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
            sqlCmd.CommandText = "select count(*) + 1  from Ques where SubId = '" + SubId.ToString() + "' and TestId = '" + TestId.ToString() + "'";

            if (sqlCmd.ExecuteScalar() != null)
                Sr = Convert.ToInt16(sqlCmd.ExecuteScalar());
            else
                Sr = 1;

            _generalDAL.CloseSQLConnection();

            return Sr;
        }
        catch (Exception ex)
        {
            _generalDAL.CloseSQLConnection();
            throw new Exception(ex.Message.ToString());
        }
    }
    public void DeleteQuestion(ArrayList al)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlcmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlcmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlcmd.CommandType = CommandType.Text;
        sqlcmd.Transaction = sqlTrans;

        string[] dbname = _generalDAL.ActiveSQLConnection().ConnectionString.ToString().Split(';');

        try
        {
            foreach (string id in al)
            {
                sqlcmd.CommandText = " Insert Into " + dbname[2].Replace("Initial Catalog=", "") + "Deleted..Ques(QueId, SubId, Que, A1, A2, A3, A4, Ans, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3, ImageNameA4, TestId, Hashtag, SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection, NoOfSubQues)" +
                " Select QueId, SubId, Que, A1, A2, A3, A4, Ans, GETDATE(), LastUpdatedOn, '" + MySession.UserUnique + "', LastUpdatedByUserId, ImageNameQus, ImageNameA1, ImageNameA2, ImageNameA3, ImageNameA4, TestId, Hashtag, SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection, NoOfSubQues " +
                " FROM Ques WHERE QueId='" + id + "'";
                sqlcmd.ExecuteNonQuery();

                sqlcmd.CommandText = "DELETE FROM Ques WHERE QueId='" + id + "'";
                sqlcmd.ExecuteNonQuery();
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

    public void PasteQuestionFrom(ArrayList al, QueDTO QueDTO)
    {
        SqlTransaction sqlTrans;
        SqlCommand sqlcmd = new SqlCommand();

        _generalDAL.OpenSQLConnection();
        sqlTrans = _generalDAL.BeginTransaction();
        sqlcmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlcmd.CommandType = CommandType.Text;
        sqlcmd.Transaction = sqlTrans;


        try
        {
            foreach (string id in al)
            {

                sqlcmd.CommandText = " INSERT INTO Ques(QueId,SubId,Que,A1,A2,A3,A4,Ans,ImageNameQus,ImageNameA1,ImageNameA2,ImageNameA3,ImageNameA4,InsertedOn,LastUpdatedOn," +
                                 "InsertedByUserId,LastUpdatedByUserId, TestId, SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection, NoOfSubQues)" +
                                 " select  NewID() QueId," + ((QueDTO.SubIdTo == null) ? "NULL" : "'" + QueDTO.SubIdTo + "'") + " SubId,Que,A1,A2,A3,A4,Ans,ImageNameQus,ImageNameA1,ImageNameA2,ImageNameA3,ImageNameA4,Getdate() InsertedOn,Getdate() LastUpdatedOn," +
                                 "InsertedByUserId,LastUpdatedByUserId, " + ((QueDTO.TestIdTo == null) ? "NULL" : "'" + QueDTO.TestIdTo.ToString() + "'") +
                                 ", (select count(*) + 1  from Ques where SubId = '" + QueDTO.SubIdTo.ToString() + "' and TestId = '" + QueDTO.TestIdTo.ToString() + "') SrNo, QueType, QueDataType, RightMarks, WrongMarks, NonMarks, NoOfFile, SampleAns1, SampleAns2, SampleAns3, SampleAns4, AnsSelection, NoOfSubQues " +
                                 " FROM Ques WHERE QueId='" + id + "'";
                sqlcmd.ExecuteNonQuery();
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

    public bool NameExists(string Que)
    {
        //Escape Single Quote
        Que = Que.Trim().Replace("'", "''");
        //Escape single Quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.CommandText = "SELECT COUNT(*) FROM Ques WHERE Que='" + Que + "'";

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

    public bool NameExists(string Que, string QueId ,string TestId)
    {
        //Escape Single Quote
        Que = Que.Trim().Replace("'", "''");
        //Escape Single Quote

        SqlCommand sqlCmd = new SqlCommand();
        _generalDAL.OpenSQLConnection();

        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        if (QueId != null)
        {
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Ques WHERE Que='" + Que + "' AND  QueId='" + QueId + "'";
        }
        else
        {
            sqlCmd.CommandText = "SELECT COUNT(*) FROM Ques WHERE Que='" + Que + "' And TestId='"+ TestId +"'" ;
        }

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
}
