using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.Collections;

public partial class API_API_TraningAfterShowTest : System.Web.UI.Page
{
    DateTime? date;
    string RegistrationId;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Form["date"] != null && Request.Form["date"] != "")
                    date = Convert.ToDateTime(Request.Form["date"].ToString());
                else
                    date = null;

                RegistrationId = ((Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "") ? Request.Form["RegistrationId"].ToString() : null);

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
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                    }
                    else if (j == ds.Tables[0].Columns.Count - 1)
                    {
                        JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
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
        DataTable da1 = new DataTable();
        DataTable da = new DataTable();
        StringBuilder st = new StringBuilder();
        string ReturnVal = "";
        try
        {
            da1 = _aPI_BLL.returnDataTable(" select * from HODInterViews "+
                                          " where ApprovedDisapproved ='TP' and "+
                                          " RegistrationId = '" + RegistrationId + "' ");
            if (da1 != null)
            {
                da = _aPI_BLL.returnDataTable(" Select t.TestId,t.TestName,t.SubId,e.ExamDate,e.ExamscheduleId  " +
                                                     " , ISNULL(e.IsSelfieAllowed,0) as 'IsSelfieAllowed', " +
                                                     " ISNULL(e.IsSignatureAllowed,0) as 'IsSignatureAllowed' , " +
                                                     " FORMAT(e.ExamFromTime, 'hh:mm tt') as ExamFromTime,FORMAT(e.ExamToTime, 'hh:mm tt') as ExamToTime " +
                                                     " ,ss.Name as SubName,q.LevelofQue As 'Test Level' " +
                                                     " from Tests t " +
                                                     " Inner join ExamSchedules e on e.TestId = t.TestId and e.SubId = t.SubId " +
                                                     " inner join Subs ss on t.SubId = ss.SubId " +
                                                     " left join QueBanks q on q.SubId = ss.SubId " +
                                                     " Where t.SubId in (select SubId from Subs where StandardTextListId in " +
                                                     " (select J.DepartmentId As 'StandardId' from RegistrationJobProfileLns R inner join Jobofferings J on J.JobOfferingId = R.JobOfferingId where R.RegistrationId = '" + RegistrationId + "')) " +
                                                     " and e.ExamDate = '" + Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + "' " +
                                                     " Union " +
                                                     " Select t.TestId,t.TestName,t.PatternId,e.ExamDate,e.ExamscheduleId  " +
                                                     " , ISNULL(e.IsSelfieAllowed,0) as 'IsSelfieAllowed', " +
                                                     " ISNULL(e.IsSignatureAllowed,0) as 'IsSignatureAllowed' , " +
                                                     " FORMAT(e.ExamFromTime, 'hh:mm tt') as ExamFromTime,FORMAT(e.ExamToTime, 'hh:mm tt') as ExamToTime " +
                                                     " ,ss.Name as SubName ,q.LevelofQue As 'Test Level' " +
                                                     " from Tests t " +
                                                     " Inner join ExamSchedules e on e.TestId = t.TestId and e.PatternId = t.PatternId " +
                                                     " inner join Subs ss on t.SubId = ss.SubId " +
                                                     " left join QueBanks q on q.SubId = ss.SubId " +
                                                     " and e.ExamDate = '" + Convert.ToDateTime(date).ToString("dd-MMM-yyyy") + "' " +
                                                     " and isnull(IsDefaultTest, 0) = 1 " +
                                                     " union " +
                                                     " Select t.TestId,t.TestName,t.SubId,e.ExamDate,e.ExamscheduleId  " +
                                                     " , ISNULL(e.IsSelfieAllowed,0) as 'IsSelfieAllowed', " +
                                                     " ISNULL(e.IsSignatureAllowed,0) as 'IsSignatureAllowed' , " +
                                                     " FORMAT(e.ExamFromTime, 'hh:mm tt') as ExamFromTime,FORMAT(e.ExamToTime, 'hh:mm tt') as ExamToTime " +
                                                     " ,ss.Name as SubName,q.LevelofQue As 'Test Level' " +
                                                     " from Tests t " +
                                                     " Inner join ExamSchedules e on e.TestId = t.TestId and e.SubId = t.SubId " +
                                                     " inner join Subs ss on t.SubId = ss.SubId " +
                                                     " left join QueBanks q on q.SubId = ss.SubId " +
                                                     " Where t.SubId in (select SubId from Subs where StandardTextListId in " +
                                                     " (select J.DepartmentId As 'StandardId' from RegistrationJobProfileLns V inner join Jobofferings J on J.JobOfferingId = V.JobOfferingId  where V.RegistrationId = '" + RegistrationId + "')) " +
                                                     " and isnull(IsDefaultTest, 0) = 1 " +
                                                     " order by TestName ");
            }
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