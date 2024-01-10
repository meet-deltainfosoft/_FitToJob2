<%@ WebHandler Language="C#" Class="Chapter" %>

using System;
using System.Web;
using System.Data.SqlClient;

public class Chapter : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Subject = context.Request.QueryString["Subject"];

        string likeParam = context.Request.QueryString["q"];
        string StdId = "";
        string SubId = "";

        StdId = context.Request.QueryString["StdId"];
        SubId = context.Request.QueryString["SubId"];

        SqlCommand sqlCmd = new SqlCommand();
        GeneralDAL generalDAL = new GeneralDAL();
        string retVal = "";

        string where = "";

        if (StdId != "null")
            if (StdId != "" && StdId != "0")
            {
                where += " And a.StandardTextListId = '" + StdId + "'";
            }

        if (SubId != "null")
            if (SubId != "" && SubId != "0")
            {
                where += " And a.SubId = '" + SubId + "'";
            }

        sqlCmd.CommandText = " select distinct a.ChapterName from Chapters a " +
                                " where a.ChapterName like '%" + likeParam + "%' " + where + "";

        generalDAL.OpenSQLConnection();
        sqlCmd.Connection = generalDAL.ActiveSQLConnection();
        SqlDataReader sqlDR = sqlCmd.ExecuteReader();

        while (sqlDR.Read())
        {
            retVal += sqlDR[0].ToString() + Environment.NewLine;
        }
        sqlDR.Close();

        generalDAL.CloseSQLConnection();
        sqlCmd = null;
        generalDAL = null;

        context.Response.Write(retVal);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}