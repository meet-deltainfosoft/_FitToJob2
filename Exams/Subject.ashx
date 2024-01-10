<%@ WebHandler Language="C#" Class="Subject" %>

using System;
using System.Web;
using System.Data.SqlClient;

public class Subject : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string Subject = context.Request.QueryString["Subject"];          
      
        string likeParam = context.Request.QueryString["q"];       
    

        SqlCommand sqlCmd = new SqlCommand();
        GeneralDAL generalDAL = new GeneralDAL();
        string retVal = "";

        if (Subject == "SubId")
        {

            sqlCmd.CommandText = " select a.SubId,a.Name from Subs a " +
                                " where a.Name like '%" + likeParam + "%'";
        }
        
        generalDAL.OpenSQLConnection();
        sqlCmd.Connection = generalDAL.ActiveSQLConnection();
        SqlDataReader sqlDR = sqlCmd.ExecuteReader();

        while (sqlDR.Read())
        {
            retVal += sqlDR[1].ToString() + "|" + sqlDR[0].ToString()+ Environment.NewLine;
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