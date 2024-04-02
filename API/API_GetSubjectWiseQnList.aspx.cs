using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

public partial class API_GetSubjectWiseQnList : System.Web.UI.Page
{
    string TestId;
    string ExamScheduleId;
    string RegistrationId;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //if (Request.Form["TestId"] != null && Request.Form["TestId"] != "")
                //    TestId = Request.Form["TestId"].ToString();
                //else
                //    TestId = null;

                if (Request.Form["ExamScheduleId"] != null && Request.Form["ExamScheduleId"] != "")
                    ExamScheduleId = Request.Form["ExamScheduleId"].ToString();
                else
                    ExamScheduleId = null;

                if (Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "")
                    RegistrationId = Request.Form["RegistrationId"].ToString();
                else
                    RegistrationId = null;

                Response.ContentType = "application/json";
                Response.Write(selectdata());
            }
            catch (Exception ex)
            {
                string sw = "";
                StringBuilder s = new StringBuilder();
                s.Append(ex.Message);
                sw = GetReturnValue("209", "Data Get Issue", s);
                Response.ContentType = "application/json";
                Response.Write(sw.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]"));
            }
        }
    }

    public class ResponseData
    {
        public int? status { get; set; }
        public string message { get; set; }
        public ArrayList Result { get; set; }
    }

    public string DataTableToJsonObj(DataTable dt)
    {
        DataSet ds = new DataSet();
        ds.Merge(dt);
        StringBuilder JsonString = new StringBuilder();
        if (ds != null && ds.Tables[0].Rows.Count > 0)
        {
            JsonString.Append("[");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                JsonString.Append("{");
                for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                {
                    if (j < ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "'").Replace("\\", "\\/") + "\",");
                    }
                    else if (j == ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString().Replace("\"", "'").Replace("\\", "\\/") + "\"");
                    }
                }
                if (i == ds.Tables[0].Rows.Count - 1)
                {
                    JsonString.Append("}");
                }
                else
                {
                    JsonString.Append("},");
                }
            }
            JsonString.Append("]");
            return JsonString.ToString();
        }
        else
        {
            return null;
        }
    }

    public string selectdata()
    {
        string ReturnVal = "";
        DataTable da = new DataTable();
        StringBuilder st = new StringBuilder();
        try
        {
            SqlCommand sqlCmd = new SqlCommand();
            GeneralDAL objDal = new GeneralDAL();
            objDal.OpenSQLConnection();
            sqlCmd.Connection = objDal.ActiveSQLConnection();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandText = "FitToJob_Exam_Module";
            sqlCmd.Parameters.AddWithValue("@Action", "API_GetSubjectWiseQnList");
            sqlCmd.Parameters.AddWithValue("@RegistrationId", RegistrationId);
            sqlCmd.Parameters.AddWithValue("@ExamScheduleId", ExamScheduleId);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCmd);
            //Da dataSet = new DataSet();
            dataAdapter.Fill(da);
            st.Append(DataTableToJsonObj(da));

            if (da.Rows.Count > 0)
            {
                ReturnVal = GetReturnValue("200", "Data Get", st);
            }

            if (st.ToString() != "[]")
                return ReturnVal.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]");
            else
                return ReturnVal.Replace("\\", "").Replace("\"[]\"", "[]");
        }
        catch (Exception ex)
        {
            StringBuilder s = new StringBuilder();
            s.Append(ex.Message);
            ReturnVal = GetReturnValue("209", "Data Get Issue", s);
            return ReturnVal.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]");
        }
    }


    public string selectdata_old()
    {

        DataTable da = new DataTable();
        StringBuilder st = new StringBuilder();
        string ReturnVal = "";
        try
        {

           


            da = _aPI_BLL.returnDataTable(  "Declare @RegistrationId uniqueidentifier = '" + RegistrationId + "', " +                                      "@ExamScheduleId uniqueidentifier = '" + ExamScheduleId + "' " +                                                                            " Select S.SubjectName,T.TestName,Q.Que,  " +                                      " Isnull(Q.A1,'')A1, " +                                      " Isnull(Q.A2,'')A2,  " +                                      " Isnull(Q.A3,'')A3,  " +                                      " Isnull(Q.A4,'')A4,  " +                                      " Isnull(Q.ImageNameA1,'')ImageNameA1,  " +                                      " Isnull(Q.ImageNameA2,'')ImageNameA2,  " +                                      " Isnull(Q.ImageNameA3,'')ImageNameA3,  " +                                      " Isnull(Q.ImageNameA4,'')ImageNameA4,  " +                                      " e.PerQueMins as QnTime,e.TotalMins,  " +                                      " q.QueType, q.QueDataType, q.RightMarks, q.WrongMarks, q.NonMarks, q.NoOfFile,'False' as PerQuestionTime,  " +
                                      " q.Ans as TrueAns,s.subjectId, q.QueId, Row_Number()Over(Order by (Select 1)) as queNo,t.TestId, q.ImageNameQus, " +                                      " Isnull(EE.AnsStatus,'')AnsStatus  " +                                      " From ExamSchedules E Join ExamScheduleLns EL on E.ExamScheduleId = EL.ExamScheduleId  " +                                      " Join Subjects S  on S.subjectId = E.SubId  " +                                      " Join Tests T on T.TestId = E.TestId  " +                                      " Join Ques Q on Q.SubId = S.subjectId and T.TestId = Q.TestId  " +                                       " Left JOIN Exams ee ON ee.QueId = q.QueId AND ee.RegistrationId = el.RegistrationId  " +
                                      "     AND ee.ExamScheduleId = e.ExamScheduleId AND ee.TestId = T.TestId  " +                                      " Where E.ExamScheduleId = @ExamScheduleId and EL.RegistrationId = @RegistrationId  " +
                                      " order by q.SrNo  " );

            st.Append(DataTableToJsonObj(da));

            if (da == null)
            {
                ReturnVal = GetReturnValue("209", "No Record Found", st);
            }

            if (st.ToString() == "[]" || st.ToString() == "")
            {
                ReturnVal = GetReturnValue("209", "No Record Found", st);
            }

            if (da.Rows.Count > 0)
            {
                ReturnVal = GetReturnValue("200", "Data Get", st);
            }

            if (st.ToString() != "[]")
                return ReturnVal.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]");
            else
                return ReturnVal.Replace("\\", "").Replace("\"[]\"", "[]");
        }
        catch (Exception ex)
        {
            StringBuilder s = new StringBuilder();
            s.Append(ex.Message);
            ReturnVal = GetReturnValue("209", "Data Get Issue", s);
            return ReturnVal.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]");
        }
    }

    public string GetReturnValue(string Status, string Message, StringBuilder PassStringDataTable)
    {
        var r = new ReturnValue
        {
            status = Status,
            message = Message,
            result = PassStringDataTable.ToString()
        };
        return new JavaScriptSerializer().Serialize(r);
    }
}