using System;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;
using System.Configuration;

public partial class API_FCM : System.Web.UI.Page
{
    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                Response.ContentType = "application/json";
                selectdata();
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

    public void selectdata()
    {
        DataTable da = new DataTable();
        StringBuilder st = new StringBuilder();
        string ReturnVal = "";
        try
        {
            da = _aPI_BLL.returnDataTable(" Select e.ExamScheduleId,el.RegistrationId,e.TestId,e.SubId, e.StandardTextListId, " +  //r.RegistrationId as RegistrationId
                                          " e.ExamDate, e.ExamFromTime as ExamFromTime,e.ExamToTime as ExamToTime, tt.[Text] as 'Standard', t.TestName, s.Name as 'SubJectName',r.IMEI,r.FCMId " +
                                          " ,ISNULL(e.IsSelfieAllowed,0) as IsSelfieAllowed,ISNULL(IsSignatureAllowed, 0) as IsSignatureAllowed, c.ChapterName " +
                                          " from ExamSchedules e " +
                                          " Inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
                //" Inner join ExamScheduleLns el on el.ExamScheduleId = e.ExamScheduleId " +
                                          " Inner join Registration r on r.StandardId = el.RegistrationId " +  //e.StandardTextListId
                                          " Inner Join TextLists tt on tt.TextListId = e.StandardTextListId " +
                                          " Inner join Tests t on t.TestID = e.TestId " +
                                          " Inner Join Subs s on s.SubId = e.SubId " +
                                          " Left join Chapters c on c.SubId = s.SubId " +
                                          " Where convert(date, e.ExamDate) = convert(date, getdate()) "); //Where e.ExamDate = '30-May-2020'

            string FCMID1 = "";
            if (da != null)
            {
                if (da.Rows.Count > 0)
                {
                    foreach (DataRow dr in da.Rows)
                    {
                        if (dr["FCMId"] != DBNull.Value)
                        {
                            try
                            {
                                if (!FCMID1.Contains(dr["FCMId"].ToString()))
                                {
                                    System.Net.HttpWebRequest request = System.Net.WebRequest.Create("https://fcm.googleapis.com/fcm/send") as System.Net.HttpWebRequest;
                                    request.Method = "POST";
                                    string AppId = "AAAABXxbF58:APA91bE63Q6waVyR2W7X7YFZ39CSBGxYe5ghWx77Y2G5WycAwtF8xMS-d6zVajRvBdVvZHFBw3pTTn5xfAnmKavuqrZ7UmC6DaUi73uAgawx8lC2VGp1kTpV7jCpXGf93u_i1fSKguVg";
                                    request.ContentType = " application/json";
                                    request.Headers.Add(string.Format("Authorization: key={0}", AppId));
                                    request.ContentType = "application/json";
                                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                                    using (var sw = new System.IO.StreamWriter(request.GetRequestStream()))
                                    {
                                        string json = "{" +
                                            " \"registration_ids\": [\"" + dr["FCMId"].ToString() + "\"]," +
                                            "\"data\": {" +
                                            "    \"TestId\": \"" + ((dr["TestId"] == DBNull.Value) ? "NULL" : "" + dr["TestId"].ToString() + "") + "\"," +
                                            "    \"TestName\": \"" + ((dr["TestName"] == DBNull.Value) ? "NULL" : "" + dr["TestName"].ToString() + "") + "\"," +
                                            "    \"SubId\": \"" + ((dr["SubId"] == DBNull.Value) ? "NULL" : "" + dr["SubId"].ToString() + "") + "\"," +
                                            "    \"SubJectName\": \"" + ((dr["SubJectName"] == DBNull.Value) ? "NULL" : "" + dr["SubJectName"].ToString() + "") + "\"," +
                                            "    \"Standard\": \"" + ((dr["Standard"] == DBNull.Value) ? "NULL" : "" + dr["Standard"].ToString() + "") + "\"," +
                                            "    \"ExamDate\": \"" + ((dr["ExamDate"] == DBNull.Value) ? "NULL" : "" + dr["ExamDate"].ToString() + "") + "\"," +
                                            "    \"ExamFromTime\": \"" + ((dr["ExamFromTime"] == DBNull.Value) ? "NULL" : "" + dr["ExamFromTime"].ToString() + "") + "\"," +
                                            "    \"ExamToTime\": \"" + ((dr["ExamToTime"] == DBNull.Value) ? "NULL" : "" + dr["ExamToTime"].ToString() + "") + "\"," +
                                            "    \"ExamScheduleId\": \"" + ((dr["ExamScheduleId"] == DBNull.Value) ? "NULL" : "" + dr["ExamScheduleId"].ToString() + "") + "\"," +
                                            "    \"RegistrationId\": \"" + ((dr["RegistrationId"] == DBNull.Value) ? "NULL" : "" + dr["RegistrationId"].ToString() + "") + "\"," +
                                            "    \"FCMId\": \"" + ((dr["FCMId"] == DBNull.Value) ? "NULL" : "" + dr["FCMId"].ToString() + "") + "\"," +
                                            "    \"ChapterName\": \"" + ((dr["ChapterName"] == DBNull.Value) ? "NULL" : "" + dr["ChapterName"].ToString() + "") + "\"," +
                                            "    \"IsSelfieAllowed\": \"" + ((dr["IsSelfieAllowed"] == DBNull.Value) ? "NULL" : "" + Convert.ToBoolean(dr["IsSelfieAllowed"]) + "") + "\"," +
                                            "    \"IsSignatureAllowed\": \"" + ((dr["IsSignatureAllowed"] == DBNull.Value) ? "NULL" : "" + Convert.ToBoolean(dr["IsSignatureAllowed"]) + "") + "\"," +
                                            "    \"Type\": \"Test\"" +
                                            "  }" +
                                            "}";

                                        sw.Write(json);
                                        st.Append(json);
                                        sw.Flush();
                                    }
                                    System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            if (da == null)
            {
                ReturnVal = GetReturnValue("209", "No Record Found", st);
                Response.Write(ReturnVal.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]"));
            }
        }
        catch (Exception ex)
        {
            StringBuilder s = new StringBuilder();
            s.Append(ex.Message);
            ReturnVal = GetReturnValue("209", "Data Get Issue", s);
            Response.Write(ReturnVal.Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]"));
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