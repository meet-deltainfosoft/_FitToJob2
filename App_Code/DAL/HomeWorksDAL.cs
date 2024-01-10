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

public class HomeWorksDAL
{
    private GeneralDAL _generalDAL;

    public HomeWorksDAL()
    {
        _generalDAL = new GeneralDAL();
    }

    ~HomeWorksDAL()
    {
        _generalDAL = null;
    }

    public DataTable HomeWorks(string Question, string StandardId, string SubId, string ChapterId, bool AllRecords)
    {
        if (Question != null)
            Question = Question.Replace("'", "''");

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
            where += " a.ChapterId =  '" + ChapterId + "'";
        }

        //Question
        if (Question != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.HomeWork Like  N'" + Question + "%'";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }

        if (AllRecords == false)
        {
            sqlCmd.CommandText = " select Top 30 a.HomeWorkId,isnull(a.HomeWork,a.ImageNameQus) as HomeWork ,isnull(a.A1,ImageNameA1) as A1,isnull(a.A2,ImageNameA2) as A2, " +
                                 " isnull(a.A3,ImageNameA3) as A3,isnull(a.A4,ImageNameA4)  as A4,b.SubId,b.Name as Subject,a.ChapterId,c.ChapterName as 'Chapter'  " +
                                 " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                 " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                 " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                 " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                 " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                 " , a.SrNo,a.HashTag,a.HomeWorkType " +
                                 " from HomeWorks a " +
                                 " inner join Subs b on b.SubId = a.SubId " +
                                 " inner join Chapters c on c.ChapterId = a.ChapterId  " +
                                 where +
                                 " Order By a.SrNo, a.InsertedOn ";
        }
        else
        {
            sqlCmd.CommandText = " select   a.HomeWorkId,isnull(a.HomeWork,a.ImageNameQus) as HomeWork ,isnull(a.A1,ImageNameA1) as A1,isnull(a.A2,ImageNameA2) as A2, " +
                                 " isnull(a.A3,ImageNameA3) as A3,isnull(a.A4,ImageNameA4)  as A4,b.SubId,b.Name as Subject,a.ChapterId,c.ChapterName as 'Chapter'  " +
                                 " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                                 " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                                 " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                                 " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                                 " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                                 " , a.SrNo,a.HashTag,a.HomeWorkType " +
                                 " from HomeWorks a " +
                                 " inner join Subs b on b.SubId = a.SubId " +
                                 " inner join Chapters c on c.ChapterId = a.ChapterId  " +
                                 where +
                                 " Order By a.SrNo, a.InsertedOn ";
        }

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }

    public DataTable FilterForStudentHomework(string Question, string StandardId, string SubId, string ChapterId, string StudentName, string MobileNo)
    {
        if (Question != null)
            Question = Question.Replace("'", "''");

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
            where += " a.ChapterId =  '" + ChapterId + "'";
        }

        //Question
        if (Question != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " a.HomeWork Like  N'" + Question + "%'";
        }

        //StudentName
        if (StudentName != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " st.StudentFullName Like  '%" + StudentName + "%'";
        }

        //MobileNo
        if (MobileNo != null)
        {
            if (where != "")
            {
                where += " AND ";
            }
            where += " r.MobileNo = '" + MobileNo + "'";
        }

        if (where != "")
        {
            where = " WHERE " + where;
        }

        sqlCmd.CommandText = " select a.HomeWorkId,isnull(a.HomeWork,a.ImageNameQus) as HomeWork ,isnull(a.A1,ImageNameA1) as A1,isnull(a.A2,ImageNameA2) as A2, " +
                             " isnull(a.A3,ImageNameA3) as A3,isnull(a.A4,ImageNameA4)  as A4,b.SubId,b.Name as Subject,a.ChapterId,c.ChapterName as 'Chapter'  " +
                             " , replace(a.ImageNameQus, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameQus' " +
                             " , replace(a.ImageNameA1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA1' " +
                             " , replace(a.ImageNameA2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA2' " +
                             " , replace(a.ImageNameA3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA3' " +
                             " , replace(a.ImageNameA4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'ImageNameA4' " +
                             " , a.SrNo,a.HashTag,a.HomeWorkType " +
                             " , ha.HomeWorkAnsId, ha.RegistrationId, ha.SubId, ha.HomeWorkId, ha.Ans, ha.ChapterId, ha.InsertedOn " +
                             " , ha.LastUpdatedOn, ha.InsertedByUserId, ha.LastUpdatedByUserId, ha.HomeWorkType " +
                             " , replace(ha.AnsImage1, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage1' " +
                             " , replace(ha.AnsImage2, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage2' " +
                             " , replace(ha.AnsImage3, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage3' " +
                             " , replace(ha.AnsImage4, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage4' " +
                             " , replace(ha.AnsImage5, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage5' " +
                             " , replace(ha.AnsImage6, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage6' " +
                             " , replace(ha.AnsImage7, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage7' " +
                             " , replace(ha.AnsImage8, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage8' " +
                             " , replace(ha.AnsImage9, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage9' " +
                             " , replace(ha.AnsImage10, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage10' " +
                             " , replace(ha.AnsImage11, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage11' " +
                             " , replace(ha.AnsImage12, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage12' " +
                             " , replace(ha.AnsImage13, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage13' " +
                             " , replace(ha.AnsImage14, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage14' " +
                             " , replace(ha.AnsImage15, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage15' " +
                             " , replace(ha.AnsImage16, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage16' " +
                             " , replace(ha.AnsImage17, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage17' " +
                             " , replace(ha.AnsImage18, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage18' " +
                             " , replace(ha.AnsImage19, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage19' " +
                             " , replace(ha.AnsImage20, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage20' " +
                             " , replace(ha.AnsImage21, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage21' " +
                             " , replace(ha.AnsImage22, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage22' " +
                             " , replace(ha.AnsImage23, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage23' " +
                             " , replace(ha.AnsImage24, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage24' " +
                             " , replace(ha.AnsImage25, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage25' " +
                             " , replace(ha.AnsImage26, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage26' " +
                             " , replace(ha.AnsImage27, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage27' " +
                             " , replace(ha.AnsImage28, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage28' " +
                             " , replace(ha.AnsImage29, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage29' " +
                             " , replace(ha.AnsImage30, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage30' " +
                             " , replace(ha.AnsImage31, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage31' " +
                             " , replace(ha.AnsImage32, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage32' " +
                             " , replace(ha.AnsImage33, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage33' " +
                             " , replace(ha.AnsImage34, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage34' " +
                             " , replace(ha.AnsImage35, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage35' " +
                             " , replace(ha.AnsImage36, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage36' " +
                             " , replace(ha.AnsImage37, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage37' " +
                             " , replace(ha.AnsImage38, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage38' " +
                             " , replace(ha.AnsImage39, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage39' " +
                             " , replace(ha.AnsImage40, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'AnsImage40' " +
                             " , ha.ChapterVideoId " +
                             " , st.StudentFullName, st.GRNo, st.InstituteName, st.[Std-Div], r.MobileNo " +
                             " from HomeWorks a " +
                             " inner join Subs b on b.SubId = a.SubId " +
                             " inner join Chapters c on c.ChapterId = a.ChapterId  " +
                             " inner join HomeWorkAns ha on ha.HomeWorkId = a.HomeWorkId " +
                             " inner join Registration r on r.RegistrationId = ha.RegistrationId " +
                             " left join " + ConfigurationSettings.AppSettings["ERPDBName"].ToString() + "..vwStudentDetails st on st.StudentId = r.RegistrationId " +
                             where +
                             " Order By a.SrNo, a.InsertedOn ";

        _generalDAL.OpenSQLConnection();
        sqlCmd.CommandType = CommandType.Text;
        sqlCmd.Connection = _generalDAL.ActiveSQLConnection();
        dt.Load(sqlCmd.ExecuteReader());

        return dt;
    }
}
