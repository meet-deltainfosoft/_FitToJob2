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

public class QueBanksDAL
{
    private GeneralDAL _generalDAL;

    public QueBanksDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~QueBanksDAL()
    {
        _generalDAL = null;
    }

    public DataTable QueBank(string Que, string StandardId, string SubId, string ChapterId, string PeriodNo, bool AllRecords)
    {
        try
        {
            if (Que != null)
                Que = Que.Replace("'", "''");

            string where;
            SqlCommand sqlCmd = new SqlCommand();
            DataTable dt = new DataTable();
            where = "";

            if (StandardId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " b.StandardTextListId =  '" + StandardId + "'";
            }

            if (SubId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " b.SubId =  '" + SubId + "'";
            }

            if (ChapterId != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " c.ChapterId = '" + ChapterId + "'";
            }

            //QueBank
            if (Que != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " a.Que Like  N'" + Que + "%'";
            }

            if (PeriodNo != null)
            {
                if (where != "")
                {
                    where += " AND ";
                }
                where += " a.PeriodNo =  '" + PeriodNo + "'";
            }

            if (where != "")
            {
                where = " WHERE " + where;
            }

            if (AllRecords == false)
            {
                sqlCmd.CommandText = " select Top 30 a.QueBankId,isnull(a.Que,a.ImageNameQus) as Question ,isnull(a.A1,ImageNameA1) as A1,isnull(a.A2,ImageNameA2) as A2, " +
                                     " isnull(a.A3,ImageNameA3) as A3,isnull(a.A4,ImageNameA4)  as A4,b.SubId,b.Name as Subject " +
                                     " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                     " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                     " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                     " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                     " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                     " , a.SrNo,a.PeriodNo from QueBanks a " +
                                     " left join Subs b on b.SubId = a.SubId " +
                                     " left join Chapters c on a.ChapterID = c.ChapterId " +
                                     where +
                                     " Order By a.SrNo, a.InsertedOn ";
            }
            else
            {
                sqlCmd.CommandText = " select a.QueBankId,isnull(a.Que,a.ImageNameQus) as Question  ,isnull(a.A1,ImageNameA1) as A1,isnull(a.A2,ImageNameA2) as A2, " +
                                      " isnull(a.A3,ImageNameA3) as A3,isnull(a.A4,ImageNameA4)  as A4,b.SubId,b.Name as Subject " +
                                      " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                      " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                      " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                      " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                      " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                      " , a.SrNo,a.PeriodNo from QueBanks a " +
                                      " left join Subs b on b.SubId = a.SubId " +
                                     " left join Chapters c on a.ChapterID = c.ChapterId " +
                                      where +
                                      " Order By a.SrNo, a.InsertedOn ";
            }

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
