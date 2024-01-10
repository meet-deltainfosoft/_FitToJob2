<%@ WebHandler Language="C#" Class="User" %>

using System;
using System.Web;
using System.Data.SqlClient;
using System.Data;


public class User : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";

        string likeParam = context.Request.QueryString["q"];
        string outputCode = context.Request.QueryString["outputCode"];

        SqlCommand sqlCmd = new SqlCommand();
        GeneralDAL generalDAL = new GeneralDAL();
        string retVal = "";

        sqlCmd.CommandText = " SELECT TOP 100 a.UserId,ISNULL(FirstName,'')  + ' ' + IsNULL(LastName,'') as Name " +
                             " FROM Users_ a " +
                             " ORDER BY ISNULL(FirstName,'')  + ' ' + IsNULL(LastName,'')";

        generalDAL.OpenSQLConnection();
        sqlCmd.Connection = generalDAL.ActiveSQLConnection();
        SqlDataReader sqlDR = sqlCmd.ExecuteReader();

        while (sqlDR.Read())
        {
            retVal += sqlDR[1].ToString() + "|" + sqlDR[0].ToString() + Environment.NewLine;
        }
        sqlDR.Close();

        generalDAL.CloseSQLConnection();
        sqlCmd = null;
        generalDAL = null;

        context.Response.Write(retVal);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}