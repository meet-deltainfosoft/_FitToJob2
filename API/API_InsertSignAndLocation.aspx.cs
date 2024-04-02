using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.Text;


public partial class API_InsertSignAndLocation : System.Web.UI.Page
{
    string RegistrationId;
    string ExamScheduleId;
    string StartLat;
    string StartLong;
    string StopLat;
    string StopLong;
    string InsertedByUserId;
    string Type;

    HttpPostedFile SelfiePhotoPath;
    HttpPostedFile SignaturePath;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                RegistrationId = (((Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "")) ? Request.Form["RegistrationId"].ToString() : null);
                ExamScheduleId = (((Request.Form["ExamScheduleId"] != null && Request.Form["ExamScheduleId"] != "")) ? Request.Form["ExamScheduleId"].ToString() : null);
                StartLat = (((Request.Form["StartLat"] != null && Request.Form["StartLat"] != "")) ? Request.Form["StartLat"].ToString() : null);
                StartLong = (((Request.Form["StartLong"] != null && Request.Form["StartLong"] != "")) ? Request.Form["StartLong"].ToString() : null);
                StopLat = (((Request.Form["StopLat"] != null && Request.Form["StopLat"] != "")) ? Request.Form["StopLat"].ToString() : null);
                StopLong = (((Request.Form["StopLong"] != null && Request.Form["StopLong"] != "")) ? Request.Form["StopLong"].ToString() : null);
                InsertedByUserId = (((Request.Form["InsertedByUserId"] != null && Request.Form["InsertedByUserId"] != "")) ? Request.Form["InsertedByUserId"].ToString() : null);

                SelfiePhotoPath = (((Request.Files["SelfiePhotoPath"] != null && Request.Files["SelfiePhotoPath"].FileName != "")) ? Request.Files["SelfiePhotoPath"] : null);
                SignaturePath = (((Request.Files["SignaturePath"] != null && Request.Files["SignaturePath"].FileName != "")) ? Request.Files["SignaturePath"] : null);
                Type = (((Request.Form["Type"] != null && Request.Form["Type"] != "")) ? Request.Form["Type"].ToString() : null);

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
        DataTable da = new DataTable();
        StringBuilder st = new StringBuilder();
        string ReturnVal = "";
        string ExamStartStopId = "";
        try
        {
            if (Type == "Start")
            {
                #region Update ExamToTime in Previous Record
                string PrevExamStartStopId = "";
                try
                {
                    //PrevExamStartStopId = _aPI_BLL.InsertUpdateWithReturnIdentity(" WITH CTE AS " +
                    //                                                          " ( SELECT ExamScheduleId, ROW_NUMBER() OVER(ORDER BY InsertedOn desc) AS RowNum " +
                    //                                                          "   FROM ExamSchedules " +
                    //                                                          "   WHERE RegistrationId = '" + RegistrationId.ToString() + "' " +
                    //                                                          "   and ExamScheduleId = '" + ExamScheduleId.ToString() + "' " +
                    //                                                          "   and ExamToTime is null and ExamToTime is null ) " +
                    //                                                          " SELECT ExamScheduleId FROM CTE WHERE RowNum = 2");

                    //PrevExamStartStopId = _aPI_BLL.InsertUpdateWithReturnIdentity(" SELECT STUFF((SELECT ',' + Convert(varchar(50),ExamScheduleId) " +
                    //                                                              " FROM ExamSchedules " +
                    //                                                              " WHERE RegistrationId = '" + RegistrationId.ToString() + "' " +
                    //                                                              " and ExamScheduleId = '" + ExamScheduleId.ToString() + "' " +
                    //                                                              " and ExamToTime is null and ExamToTime is null and ExamFromTime < Getdate() "+
                    //                                                              " FOR XML PATH ('')),1,1,'') AS ExamScheduleId " );

                    PrevExamStartStopId = _aPI_BLL.InsertUpdateWithReturnIdentity(" SELECT STUFF((SELECT ',' + Convert(varchar(50),a.ExamScheduleId)  FROM ExamSchedules  a "+
				                                                                 " inner join ExamScheduleLns b on a.ExamScheduleId = b.ExamScheduleId "+
                                                                                 " WHERE b.RegistrationId = '" + RegistrationId.ToString() + "'   " +
                                                                                 " and a.ExamScheduleId = '" + ExamScheduleId.ToString() + "'   " +
                                                                                 " and a.ExamToTime is null and a.ExamToTime is null  " +
                                                                                 " and a.ExamFromTime < Getdate()  FOR XML PATH ('')),1,1,'') AS ExamScheduleId ");
                }
                catch
                {
                    PrevExamStartStopId = "";
                }


                if (PrevExamStartStopId != null && PrevExamStartStopId != "")
                {
                    _aPI_BLL.InsertUpdateNonQuery(" UPDATE ExamSchedules SET " +
                                                  " ExamToTime=GETDATE() " +
                                                  ",LastUpdatedOn=GETDATE() " +
                                                  ",LastUpdatedByUserId = '" + InsertedByUserId + "'" +
                                                  " WHERE ExamScheduleId in ('" + PrevExamStartStopId.Replace(",","','") + "')");

                }
                #endregion

                string path1 = null, path2 = null;

                //if (SelfiePhotoPath != null && SelfiePhotoPath.FileName != "")
                //{
                //    byte[] imageSize = new byte[SelfiePhotoPath.ContentLength];
                //    HttpPostedFile uploadedImage = SelfiePhotoPath;

                //    string[] getExtenstion = SelfiePhotoPath.FileName.Split('.');
                //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                //    string msExcelFilePathOnServer = ConfigurationSettings.AppSettings["FolderPath"] + "" + Convert.ToDateTime(System.DateTime.Now).ToString("ddMMMyyyy") + "/";

                //    bool exists = System.IO.Directory.Exists(msExcelFilePathOnServer);

                //    if (!exists)
                //        System.IO.Directory.CreateDirectory(msExcelFilePathOnServer);

                //    path1 = msExcelFilePathOnServer + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + RegistrationId.ToString() + "_F1" + "." + oExtension;
                //    uploadedImage.SaveAs(path1);
                //}

                //if (SignaturePath != null && SignaturePath.FileName != "")
                //{
                //    byte[] imageSize = new byte[SignaturePath.ContentLength];
                //    HttpPostedFile uploadedImage = SignaturePath;

                //    string[] getExtenstion = SignaturePath.FileName.Split('.');
                //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                //    string msExcelFilePathOnServer = ConfigurationSettings.AppSettings["FolderPath"] + "" + Convert.ToDateTime(System.DateTime.Now).ToString("ddMMMyyyy") + "/";

                //    bool exists = System.IO.Directory.Exists(msExcelFilePathOnServer);

                //    if (!exists)
                //        System.IO.Directory.CreateDirectory(msExcelFilePathOnServer);

                //    path2 = msExcelFilePathOnServer + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + RegistrationId.ToString() + "_F2" + "." + oExtension;
                //    uploadedImage.SaveAs(path2);
                //}
                //ExamStartStopId = _aPI_BLL.InsertUpdateWithReturnIdentity(" DECLARE @ExamScheduleId as uniqueidentifier" +
                //                                             " SET @ExamScheduleId = NEWID() " +
                //                                             " insert into ExamSchedules (ExamScheduleId, RegistrationId, ExamScheduleId, " +
                //                                             " ExamFromTime,ExamToTime,ExamFromTime,ExamToTime," +
                //                                             " InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId) " +
                //                                             " values (@ExamScheduleId " +
                //                                             " , " + ((RegistrationId == null) ? "NULL" : "'" + RegistrationId.ToString().Replace("'", "''") + "'") + " " +
                //                                             " , " + ((ExamScheduleId == null) ? "NULL" : "'" + ExamScheduleId.ToString().Replace("'", "''") + "'") + " " +
                //                                             " , GETDATE(), NULL,GETDATE() " +
                //                                             " , NULL " +
                //                                             " , GETDATE(), GETDATE(), '" + InsertedByUserId + "', NULL " +
                //                                             "); Select @ExamScheduleId;");

                //_aPI_BLL.InsertUpdateNonQuery("DECLARE @ExamScheduleLnId as uniqueidentifier"+
                //                               " SET @ExamScheduleLnId = NEWID() " +
                //                               " insert into ExamScheduleLns (ExamScheduleLnId, ExamScheduleId, LnNo, RegistrationId, InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId) " +
                //                               " values (@ExamScheduleLnId " +

                //                                                                            );


                da = _aPI_BLL.returnDataTable(" select ExamScheduleId from ExamSchedules where ExamScheduleId = '" + ExamScheduleId.ToString() + "'  ");
                st.Append(DataTableToJsonObj(da));
            }
            else if (Type == "Stop")
            {


                da = _aPI_BLL.returnDataTable(" select Top 1 a.ExamScheduleId from ExamSchedules a inner join ExamScheduleLns b on a.ExamScheduleId = b.ExamScheduleId where Isnull(a.ExamToTime,'1900-01-01')!='1900-01-01' "+
                                             " and Isnull(a.ExamToTime,'1900-01-01') !='1900-01-01' " +
                                              " and b.RegistrationId = '" + RegistrationId.ToString() + "' " +
                                              " and a.ExamScheduleId = '" + ExamScheduleId.ToString() + "'" +
                                              " Order by a.InsertedOn desc ");
                if (da.Rows.Count > 0)
                {
                    _aPI_BLL.InsertUpdateNonQuery(" UPDATE ExamSchedules SET " +
                                                  " ExamToTime=GETDATE() " +
                                                  //",StopLat =" + ((StopLat == null) ? "NULL" : "'" + StopLat.ToString() + "'") +
                                                  //",StopLong=" + ((StopLong == null) ? "NULL" : "'" + StopLong.ToString() + "'") + "" +
                                                  ",LastUpdatedOn=GETDATE() " +
                                                  ",LastUpdatedByUserId = '" + InsertedByUserId + "'" +
                                                  " WHERE ExamScheduleId='" + da.Rows[0]["ExamScheduleId"].ToString() + "'");

                    da = _aPI_BLL.returnDataTable(" select ExamScheduleId from ExamSchedules where ExamScheduleId = '" + da.Rows[0]["ExamScheduleId"].ToString() + "'  ");
                    st.Append(DataTableToJsonObj(da));
                }
                else
                {
                    da = null;
                }
            }
            else
            {
                da = null;
            }

            if (da == null)
            {
                ReturnVal = GetReturnValue("209", "No Records Found To Stop Exam", st);
            }

            if (st.ToString() == "[]" || st.ToString() == "")
            {
                ReturnVal = GetReturnValue("209", "No Records Found", st);
            }

            if (da.Rows.Count > 0)
            {
                ReturnVal = GetReturnValue("200", "Ok", st);
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