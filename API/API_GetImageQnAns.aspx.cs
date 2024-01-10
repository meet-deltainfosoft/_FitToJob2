using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.Configuration;

public partial class API_GetImageQnAns : System.Web.UI.Page
{
    string TestId;
    string ExamScheduleId;
    string QueId;
    string RegistrationId;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.Form["TestId"] != null && Request.Form["TestId"] != "")
                    TestId = Request.Form["TestId"].ToString();
                else
                    TestId = null;

                if (Request.Form["ExamScheduleId"] != null && Request.Form["ExamScheduleId"] != "")
                    ExamScheduleId = Request.Form["ExamScheduleId"].ToString();
                else
                    ExamScheduleId = null;

                if (Request.Form["QueId"] != null && Request.Form["QueId"] != "")
                    QueId = Request.Form["QueId"].ToString();
                else
                    QueId = null;

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

        DataTable da = new DataTable();
        StringBuilder st = new StringBuilder();
        string ReturnVal = "";
        try
        {

            da = _aPI_BLL.returnDataTable(" select q.SubId,q.QueId,q.Que as Question ,q.A1,q.A2,q.A3,q.A4 ,ee.Ans,ee.AnsStatus " +
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
                                          " , replace(ex1.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage1' " +
                                          " , replace(ex2.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage2' " +
                                          " , replace(ex3.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage3' " +
                                          " , replace(ex4.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage4' " +
                                          " , replace(ex5.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage5' " +
                                          " , replace(ex6.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage6' " +
                                          " , replace(ex7.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage7' " +
                                          " , replace(ex8.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage8' " +
                                          " , replace(ex9.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage9' " +
                                          " , replace(ex10.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage10' " +
                                          " , replace(ex11.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage11' " +
                                          " , replace(ex12.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage12' " +
                                          " , replace(ex13.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage13' " +
                                          " , replace(ex14.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage14' " +
                                          " , replace(ex15.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage15' " +
                                          " , replace(ex16.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage16' " +
                                          " , replace(ex17.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage17' " +
                                          " , replace(ex18.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage18' " +
                                          " , replace(ex19.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage19' " +
                                          " , replace(ex20.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage20' " +
                                          " , replace(ex21.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage21' " +
                                          " , replace(ex22.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage22' " +
                                          " , replace(ex23.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage23' " +
                                          " , replace(ex24.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage24' " +
                                          " , replace(ex25.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage25' " +
                                          " , replace(ex26.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage26' " +
                                          " , replace(ex27.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage27' " +
                                          " , replace(ex28.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage28' " +
                                          " , replace(ex29.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage29' " +
                                          " , replace(ex30.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage30' " +
                                          " , replace(ex31.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage31' " +
                                          " , replace(ex32.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage32' " +
                                          " , replace(ex33.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage33' " +
                                          " , replace(ex34.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage34' " +
                                          " , replace(ex35.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage35' " +
                                          " , replace(ex36.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage36' " +
                                          " , replace(ex37.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage37' " +
                                          " , replace(ex38.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage38' " +
                                          " , replace(ex39.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage39' " +
                                          " , replace(ex40.ImagePath, '" + ConfigurationSettings.AppSettings["FolderPath"] + "', '" + ConfigurationSettings.AppSettings["FolderPathShow"] + "') as 'CheckedAnsImage40' " +
                                          " , (select sum(TotalObtMark) from ExamEvaluations x where x.ExamId = ee.ExamID) as TotalObtMark " +
                                          " From Ques q " +
                                          " left join Exams ee on ee.QueId = q.QueId  " +
                                          " left join ExamEvaluations ex1 on ex1.ExamId = ee.ExamId and ex1.ImageNo = '1'" +
                                          " left join ExamEvaluations ex2 on ex2.ExamId = ee.ExamId and ex2.ImageNo = '2'" +
                                          " left join ExamEvaluations ex3 on ex3.ExamId = ee.ExamId and ex3.ImageNo = '3'" +
                                          " left join ExamEvaluations ex4 on ex4.ExamId = ee.ExamId and ex4.ImageNo = '4'" +
                                          " left join ExamEvaluations ex5 on ex5.ExamId = ee.ExamId and ex5.ImageNo = '5'" +
                                          " left join ExamEvaluations ex6 on ex6.ExamId = ee.ExamId and ex6.ImageNo = '6'" +
                                          " left join ExamEvaluations ex7 on ex7.ExamId = ee.ExamId and ex7.ImageNo = '7'" +
                                          " left join ExamEvaluations ex8 on ex8.ExamId = ee.ExamId and ex8.ImageNo = '8'" +
                                          " left join ExamEvaluations ex9 on ex9.ExamId = ee.ExamId and ex9.ImageNo = '9'" +
                                          " left join ExamEvaluations ex10 on ex10.ExamId = ee.ExamId and ex10.ImageNo = '10'" +
                                          " left join ExamEvaluations ex11 on ex11.ExamId = ee.ExamId and ex11.ImageNo = '11'" +
                                          " left join ExamEvaluations ex12 on ex12.ExamId = ee.ExamId and ex12.ImageNo = '12'" +
                                          " left join ExamEvaluations ex13 on ex13.ExamId = ee.ExamId and ex13.ImageNo = '13'" +
                                          " left join ExamEvaluations ex14 on ex14.ExamId = ee.ExamId and ex14.ImageNo = '14'" +
                                          " left join ExamEvaluations ex15 on ex15.ExamId = ee.ExamId and ex15.ImageNo = '15'" +
                                          " left join ExamEvaluations ex16 on ex16.ExamId = ee.ExamId and ex16.ImageNo = '16'" +
                                          " left join ExamEvaluations ex17 on ex17.ExamId = ee.ExamId and ex17.ImageNo = '17'" +
                                          " left join ExamEvaluations ex18 on ex18.ExamId = ee.ExamId and ex18.ImageNo = '18'" +
                                          " left join ExamEvaluations ex19 on ex19.ExamId = ee.ExamId and ex19.ImageNo = '19'" +
                                          " left join ExamEvaluations ex20 on ex20.ExamId = ee.ExamId and ex20.ImageNo = '20'" +
                                          " left join ExamEvaluations ex21 on ex21.ExamId = ee.ExamId and ex21.ImageNo = '21'" +
                                          " left join ExamEvaluations ex22 on ex22.ExamId = ee.ExamId and ex22.ImageNo = '22'" +
                                          " left join ExamEvaluations ex23 on ex23.ExamId = ee.ExamId and ex23.ImageNo = '23'" +
                                          " left join ExamEvaluations ex24 on ex24.ExamId = ee.ExamId and ex24.ImageNo = '24'" +
                                          " left join ExamEvaluations ex25 on ex25.ExamId = ee.ExamId and ex25.ImageNo = '25'" +
                                          " left join ExamEvaluations ex26 on ex26.ExamId = ee.ExamId and ex26.ImageNo = '26'" +
                                          " left join ExamEvaluations ex27 on ex27.ExamId = ee.ExamId and ex27.ImageNo = '27'" +
                                          " left join ExamEvaluations ex28 on ex28.ExamId = ee.ExamId and ex28.ImageNo = '28'" +
                                          " left join ExamEvaluations ex29 on ex29.ExamId = ee.ExamId and ex29.ImageNo = '29'" +
                                          " left join ExamEvaluations ex30 on ex30.ExamId = ee.ExamId and ex30.ImageNo = '30'" +
                                          " left join ExamEvaluations ex31 on ex31.ExamId = ee.ExamId and ex31.ImageNo = '31'" +
                                          " left join ExamEvaluations ex32 on ex32.ExamId = ee.ExamId and ex32.ImageNo = '32'" +
                                          " left join ExamEvaluations ex33 on ex33.ExamId = ee.ExamId and ex33.ImageNo = '33'" +
                                          " left join ExamEvaluations ex34 on ex34.ExamId = ee.ExamId and ex34.ImageNo = '34'" +
                                          " left join ExamEvaluations ex35 on ex35.ExamId = ee.ExamId and ex35.ImageNo = '35'" +
                                          " left join ExamEvaluations ex36 on ex36.ExamId = ee.ExamId and ex36.ImageNo = '36'" +
                                          " left join ExamEvaluations ex37 on ex37.ExamId = ee.ExamId and ex37.ImageNo = '37'" +
                                          " left join ExamEvaluations ex38 on ex38.ExamId = ee.ExamId and ex38.ImageNo = '38'" +
                                          " left join ExamEvaluations ex39 on ex39.ExamId = ee.ExamId and ex39.ImageNo = '39'" +
                                          " left join ExamEvaluations ex40 on ex40.ExamId = ee.ExamId and ex40.ImageNo = '40'" +
                                          " where ee.TestId = '" + TestId.ToString() + "' and ee.RegistrationId = '" + RegistrationId.ToString() + "' " +
                                          " and ee.ExamScheduleId = '" + ExamScheduleId.ToString() + "' and q.QueId  ='" + QueId.ToString() + "' ");

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