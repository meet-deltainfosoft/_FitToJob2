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


public partial class API_InsertExam : System.Web.UI.Page
{
    string RegistrationId;
    string SubId;
    string QueId;
    string Ans;
    string ExamScheduleId;
    string TestId;
    string InsertedByUserId;
    string QueType;
    bool IsJeeNeet;
    string AnsStatus;

    HttpPostedFile file1;
    HttpPostedFile file2;
    HttpPostedFile file3;
    HttpPostedFile file4;
    HttpPostedFile file5;
    HttpPostedFile file6;
    HttpPostedFile file7;
    HttpPostedFile file8;
    HttpPostedFile file9;
    HttpPostedFile file10;
    HttpPostedFile file11;
    HttpPostedFile file12;
    HttpPostedFile file13;
    HttpPostedFile file14;
    HttpPostedFile file15;
    HttpPostedFile file16;
    HttpPostedFile file17;
    HttpPostedFile file18;
    HttpPostedFile file19;
    HttpPostedFile file20;
    HttpPostedFile file21;
    HttpPostedFile file22;
    HttpPostedFile file23;
    HttpPostedFile file24;
    HttpPostedFile file25;
    HttpPostedFile file26;
    HttpPostedFile file27;
    HttpPostedFile file28;
    HttpPostedFile file29;
    HttpPostedFile file30;
    HttpPostedFile file31;
    HttpPostedFile file32;
    HttpPostedFile file33;
    HttpPostedFile file34;
    HttpPostedFile file35;
    HttpPostedFile file36;
    HttpPostedFile file37;
    HttpPostedFile file38;
    HttpPostedFile file39;
    HttpPostedFile file40;

    HttpPostedFile fileStudentPhoto;

    API_BLL _aPI_BLL = new API_BLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                RegistrationId = (((Request.Form["RegistrationId"] != null && Request.Form["RegistrationId"] != "")) ? Request.Form["RegistrationId"].ToString() : null);
                SubId = (((Request.Form["SubId"] != null && Request.Form["SubId"] != "")) ? Request.Form["SubId"].ToString() : null);
                QueId = (((Request.Form["QueId"] != null && Request.Form["QueId"] != "")) ? Request.Form["QueId"].ToString() : null);
                Ans = (((Request.Form["Ans"] != null && Request.Form["Ans"] != "")) ? Request.Form["Ans"].ToString() : null);
                ExamScheduleId = (((Request.Form["ExamScheduleId"] != null && Request.Form["ExamScheduleId"] != "")) ? Request.Form["ExamScheduleId"].ToString() : null);
                TestId = (((Request.Form["TestId"] != null && Request.Form["TestId"] != "")) ? Request.Form["TestId"].ToString() : null);
                InsertedByUserId = (((Request.Form["InsertedByUserId"] != null && Request.Form["InsertedByUserId"] != "")) ? Request.Form["InsertedByUserId"].ToString() : null);
                QueType = (((Request.Form["QueType"] != null && Request.Form["QueType"] != "")) ? Request.Form["QueType"].ToString() : null);

                file1 = (((Request.Files["file1"] != null && Request.Files["file1"].FileName != "")) ? Request.Files["file1"] : null);
                file2 = (((Request.Files["file2"] != null && Request.Files["file2"].FileName != "")) ? Request.Files["file2"] : null);
                file3 = (((Request.Files["file3"] != null && Request.Files["file3"].FileName != "")) ? Request.Files["file3"] : null);
                file4 = (((Request.Files["file4"] != null && Request.Files["file4"].FileName != "")) ? Request.Files["file4"] : null);
                file5 = (((Request.Files["file5"] != null && Request.Files["file5"].FileName != "")) ? Request.Files["file5"] : null);
                file6 = (((Request.Files["file6"] != null && Request.Files["file6"].FileName != "")) ? Request.Files["file6"] : null);
                file7 = (((Request.Files["file7"] != null && Request.Files["file7"].FileName != "")) ? Request.Files["file7"] : null);
                file8 = (((Request.Files["file8"] != null && Request.Files["file8"].FileName != "")) ? Request.Files["file8"] : null);
                file9 = (((Request.Files["file9"] != null && Request.Files["file9"].FileName != "")) ? Request.Files["file9"] : null);
                file10 = (((Request.Files["file10"] != null && Request.Files["file10"].FileName != "")) ? Request.Files["file10"] : null);
                file11 = (((Request.Files["file11"] != null && Request.Files["file11"].FileName != "")) ? Request.Files["file11"] : null);
                file12 = (((Request.Files["file12"] != null && Request.Files["file12"].FileName != "")) ? Request.Files["file12"] : null);
                file13 = (((Request.Files["file13"] != null && Request.Files["file13"].FileName != "")) ? Request.Files["file13"] : null);
                file14 = (((Request.Files["file14"] != null && Request.Files["file14"].FileName != "")) ? Request.Files["file14"] : null);
                file15 = (((Request.Files["file15"] != null && Request.Files["file15"].FileName != "")) ? Request.Files["file15"] : null);
                file16 = (((Request.Files["file16"] != null && Request.Files["file16"].FileName != "")) ? Request.Files["file16"] : null);
                file17 = (((Request.Files["file17"] != null && Request.Files["file17"].FileName != "")) ? Request.Files["file17"] : null);
                file18 = (((Request.Files["file18"] != null && Request.Files["file18"].FileName != "")) ? Request.Files["file18"] : null);
                file19 = (((Request.Files["file19"] != null && Request.Files["file19"].FileName != "")) ? Request.Files["file19"] : null);
                file20 = (((Request.Files["file20"] != null && Request.Files["file20"].FileName != "")) ? Request.Files["file20"] : null);
                file21 = (((Request.Files["file21"] != null && Request.Files["file21"].FileName != "")) ? Request.Files["file21"] : null);
                file22 = (((Request.Files["file22"] != null && Request.Files["file22"].FileName != "")) ? Request.Files["file22"] : null);
                file23 = (((Request.Files["file23"] != null && Request.Files["file23"].FileName != "")) ? Request.Files["file23"] : null);
                file24 = (((Request.Files["file24"] != null && Request.Files["file24"].FileName != "")) ? Request.Files["file24"] : null);
                file25 = (((Request.Files["file25"] != null && Request.Files["file25"].FileName != "")) ? Request.Files["file25"] : null);
                file26 = (((Request.Files["file26"] != null && Request.Files["file26"].FileName != "")) ? Request.Files["file26"] : null);
                file27 = (((Request.Files["file27"] != null && Request.Files["file27"].FileName != "")) ? Request.Files["file27"] : null);
                file28 = (((Request.Files["file28"] != null && Request.Files["file28"].FileName != "")) ? Request.Files["file28"] : null);
                file29 = (((Request.Files["file29"] != null && Request.Files["file29"].FileName != "")) ? Request.Files["file29"] : null);
                file30 = (((Request.Files["file30"] != null && Request.Files["file30"].FileName != "")) ? Request.Files["file30"] : null);
                file31 = (((Request.Files["file31"] != null && Request.Files["file31"].FileName != "")) ? Request.Files["file31"] : null);
                file32 = (((Request.Files["file32"] != null && Request.Files["file32"].FileName != "")) ? Request.Files["file32"] : null);
                file33 = (((Request.Files["file33"] != null && Request.Files["file33"].FileName != "")) ? Request.Files["file33"] : null);
                file34 = (((Request.Files["file34"] != null && Request.Files["file34"].FileName != "")) ? Request.Files["file34"] : null);
                file35 = (((Request.Files["file35"] != null && Request.Files["file35"].FileName != "")) ? Request.Files["file35"] : null);
                file36 = (((Request.Files["file36"] != null && Request.Files["file36"].FileName != "")) ? Request.Files["file36"] : null);
                file37 = (((Request.Files["file37"] != null && Request.Files["file37"].FileName != "")) ? Request.Files["file37"] : null);
                file38 = (((Request.Files["file38"] != null && Request.Files["file38"].FileName != "")) ? Request.Files["file38"] : null);
                file39 = (((Request.Files["file39"] != null && Request.Files["file39"].FileName != "")) ? Request.Files["file39"] : null);
                file40 = (((Request.Files["file40"] != null && Request.Files["file40"].FileName != "")) ? Request.Files["file40"] : null);

                fileStudentPhoto = (((Request.Files["fileStudentPhoto"] != null && Request.Files["fileStudentPhoto"].FileName != "")) ? Request.Files["fileStudentPhoto"] : null);

                IsJeeNeet = (((Request.Form["IsJeeNeet"] != null && Request.Form["IsJeeNeet"] != "")) ? Convert.ToBoolean(Request.Form["IsJeeNeet"]) : false);
                AnsStatus = (((Request.Form["AnsStatus"] != null && Request.Form["AnsStatus"] != "")) ? Request.Form["AnsStatus"].ToString() : null);

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
        string ExamId = "";
        try
        {
            da = _aPI_BLL.returnDataTable(" select * from Exams where RegistrationId = '" + RegistrationId.ToString() + "' " +
                                          " and SubId = '" + SubId.ToString() + "' and TestId = '" + TestId.ToString() + "'  " +
                                          " and ExamScheduleId = '" + ExamScheduleId.ToString() + "' " +
                                          " and QueId = '" + QueId.ToString() + "'");
            if (da.Rows.Count == 0)
            {
                if (Ans != null)
                {
                    ExamId = _aPI_BLL.InsertUpdateWithReturnIdentity(" DECLARE @ExamId as uniqueidentifier" +
                                                                     " SET @ExamId = NEWID() " +
                                                                     " insert into Exams (ExamId, RegistrationId, SubId, QueId, Ans, ExamScheduleId, TestId, " +
                                                                     " InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, QueType, AnsStatus) " +
                                                                     " values (@ExamId " +
                                                                     " , " + ((RegistrationId == null) ? "NULL" : "'" + RegistrationId.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((SubId == null) ? "NULL" : "'" + SubId.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((QueId == null) ? "NULL" : "'" + QueId.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((Ans == null) ? "NULL" : "'" + Ans.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((ExamScheduleId == null) ? "NULL" : "'" + ExamScheduleId.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((TestId == null) ? "NULL" : "'" + TestId.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , GETDATE(), GETDATE(), '" + InsertedByUserId + "', NULL " +
                                                                     " , " + ((QueType == null) ? "NULL" : "'" + QueType.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((AnsStatus == null) ? "NULL" : "'" + AnsStatus.ToString().Replace("'", "''") + "'") + " " +
                                                                     "); Select @ExamId;");

                    da = _aPI_BLL.returnDataTable(" select ExamId from Exams where ExamId = '" + ExamId.ToString() + "'  ");
                    st.Append(DataTableToJsonObj(da));
                }
                else
                {
                    string path1 = null, path2 = null, path3 = null, path4 = null, path5 = null, path6 = null, path7 = null, path8 = null, path9 = null, path10 = null;
                    string path11 = null;
                    string path12 = null;
                    string path13 = null;
                    string path14 = null;
                    string path15 = null;
                    string path16 = null;
                    string path17 = null;
                    string path18 = null;
                    string path19 = null;
                    string path20 = null;
                    string path21 = null;
                    string path22 = null;
                    string path23 = null;
                    string path24 = null;
                    string path25 = null;
                    string path26 = null;
                    string path27 = null;
                    string path28 = null;
                    string path29 = null;
                    string path30 = null;
                    string path31 = null;
                    string path32 = null;
                    string path33 = null;
                    string path34 = null;
                    string path35 = null;
                    string path36 = null;
                    string path37 = null;
                    string path38 = null;
                    string path39 = null;
                    string path40 = null;
                    string StudentPhoto = null;

                    #region "old logic for uploads"

                    //if (file1 != null && file1.FileName != "")
                    //{
                    //    byte[] imageSize = new byte[file1.ContentLength];
                    //    HttpPostedFile uploadedImage = file1;

                    //    string[] getExtenstion = file1.FileName.Split('.');
                    //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                    //    path1 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F1" + "." + oExtension;
                    //    uploadedImage.SaveAs(path1);
                    //}

                    //if (file2 != null && file2.FileName != "")
                    //{
                    //    byte[] imageSize = new byte[file2.ContentLength];
                    //    HttpPostedFile uploadedImage = file2;

                    //    string[] getExtenstion = file2.FileName.Split('.');
                    //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                    //    path2 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F2" + "." + oExtension;
                    //    uploadedImage.SaveAs(path2);
                    //}

                    //if (file3 != null && file3.FileName != "")
                    //{
                    //    byte[] imageSize = new byte[file3.ContentLength];
                    //    HttpPostedFile uploadedImage = file3;

                    //    string[] getExtenstion = file3.FileName.Split('.');
                    //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                    //    path3 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F3" + "." + oExtension;
                    //    uploadedImage.SaveAs(path3);
                    //}

                    //if (file4 != null && file4.FileName != "")
                    //{
                    //    byte[] imageSize = new byte[file4.ContentLength];
                    //    HttpPostedFile uploadedImage = file4;

                    //    string[] getExtenstion = file4.FileName.Split('.');
                    //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                    //    path4 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F4" + "." + oExtension;
                    //    uploadedImage.SaveAs(path4);
                    //}

                    //if (file5 != null && file5.FileName != "")
                    //{
                    //    byte[] imageSize = new byte[file5.ContentLength];
                    //    HttpPostedFile uploadedImage = file5;

                    //    string[] getExtenstion = file5.FileName.Split('.');
                    //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                    //    path5 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F5" + "." + oExtension;
                    //    uploadedImage.SaveAs(path5);
                    //}

                    //if (file6 != null && file6.FileName != "")
                    //{
                    //    byte[] imageSize = new byte[file6.ContentLength];
                    //    HttpPostedFile uploadedImage = file6;

                    //    string[] getExtenstion = file6.FileName.Split('.');
                    //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                    //    path6 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F6" + "." + oExtension;
                    //    uploadedImage.SaveAs(path6);
                    //}

                    //if (file7 != null && file7.FileName != "")
                    //{
                    //    byte[] imageSize = new byte[file7.ContentLength];
                    //    HttpPostedFile uploadedImage = file7;

                    //    string[] getExtenstion = file7.FileName.Split('.');
                    //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                    //    path7 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F7" + "." + oExtension;
                    //    uploadedImage.SaveAs(path7);
                    //}

                    //if (file8 != null && file8.FileName != "")
                    //{
                    //    byte[] imageSize = new byte[file8.ContentLength];
                    //    HttpPostedFile uploadedImage = file8;

                    //    string[] getExtenstion = file8.FileName.Split('.');
                    //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                    //    path8 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F8" + "." + oExtension;
                    //    uploadedImage.SaveAs(path8);
                    //}

                    //if (file9 != null && file9.FileName != "")
                    //{
                    //    byte[] imageSize = new byte[file9.ContentLength];
                    //    HttpPostedFile uploadedImage = file9;

                    //    string[] getExtenstion = file9.FileName.Split('.');
                    //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                    //    path9 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F9" + "." + oExtension;
                    //    uploadedImage.SaveAs(path9);
                    //}

                    //if (file10 != null && file10.FileName != "")
                    //{
                    //    byte[] imageSize = new byte[file10.ContentLength];
                    //    HttpPostedFile uploadedImage = file10;

                    //    string[] getExtenstion = file10.FileName.Split('.');
                    //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                    //    path10 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F10" + "." + oExtension;
                    //    uploadedImage.SaveAs(path10);
                    //}

                    #endregion

                    path1 = saveFile(file1, 1);
                    path2 = saveFile(file2, 2);
                    path3 = saveFile(file3, 3);
                    path4 = saveFile(file4, 4);
                    path5 = saveFile(file5, 5);
                    path6 = saveFile(file6, 6);
                    path7 = saveFile(file7, 7);
                    path8 = saveFile(file8, 8);
                    path9 = saveFile(file9, 9);
                    path10 = saveFile(file10, 10);
                    path11 = saveFile(file11, 11);
                    path12 = saveFile(file12, 12);
                    path13 = saveFile(file13, 13);
                    path14 = saveFile(file14, 14);
                    path15 = saveFile(file15, 15);
                    path16 = saveFile(file16, 16);
                    path17 = saveFile(file17, 17);
                    path18 = saveFile(file18, 18);
                    path19 = saveFile(file19, 19);
                    path20 = saveFile(file20, 20);
                    path21 = saveFile(file21, 21);
                    path22 = saveFile(file22, 22);
                    path23 = saveFile(file23, 23);
                    path24 = saveFile(file24, 24);
                    path25 = saveFile(file25, 25);
                    path26 = saveFile(file26, 26);
                    path27 = saveFile(file27, 27);
                    path28 = saveFile(file28, 28);
                    path29 = saveFile(file29, 29);
                    path30 = saveFile(file30, 30);
                    path31 = saveFile(file31, 31);
                    path32 = saveFile(file32, 32);
                    path33 = saveFile(file33, 33);
                    path34 = saveFile(file34, 34);
                    path35 = saveFile(file35, 35);
                    path36 = saveFile(file36, 36);
                    path37 = saveFile(file37, 37);
                    path38 = saveFile(file38, 38);
                    path39 = saveFile(file39, 39);
                    path40 = saveFile(file40, 40);

                    StudentPhoto = saveFile(fileStudentPhoto, 41);

                    ExamId = _aPI_BLL.InsertUpdateWithReturnIdentity(" DECLARE @ExamId as uniqueidentifier" +
                                                                     " SET @ExamId = NEWID() " +
                                                                     " insert into Exams (ExamId, RegistrationId, SubId, QueId, Ans, ExamScheduleId, TestId, " +
                                                                     " InsertedOn, LastUpdatedOn, InsertedByUserId, LastUpdatedByUserId, QueType, AnsImage1, AnsImage2, AnsImage3, AnsImage4, AnsStatus " +
                                                                     " , AnsImage5, AnsImage6, AnsImage7, AnsImage8, AnsImage9, AnsImage10 " +
                                                                     " , AnsImage11, AnsImage12, AnsImage13, AnsImage14, AnsImage15, AnsImage16 " +
                                                                     " , AnsImage17, AnsImage18, AnsImage19, AnsImage20, AnsImage21, AnsImage22 " +
                                                                     " , AnsImage23, AnsImage24, AnsImage25, AnsImage26, AnsImage27, AnsImage28 " +
                                                                     " , AnsImage29, AnsImage30, AnsImage31, AnsImage32, AnsImage33, AnsImage34 " +
                                                                     " , AnsImage35, AnsImage36, AnsImage37, AnsImage38, AnsImage39, AnsImage40 " +
                                                                     " , StudentPhoto) " +
                                                                     " values (@ExamId " +
                                                                     " , " + ((RegistrationId == null) ? "NULL" : "'" + RegistrationId.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((SubId == null) ? "NULL" : "'" + SubId.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((QueId == null) ? "NULL" : "'" + QueId.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((Ans == null) ? "NULL" : "'" + Ans.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((ExamScheduleId == null) ? "NULL" : "'" + ExamScheduleId.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((TestId == null) ? "NULL" : "'" + TestId.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , GETDATE(), GETDATE(), '" + InsertedByUserId + "', NULL " +
                                                                     " , " + ((QueType == null) ? "NULL" : "'" + QueType.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path1 == null) ? "NULL" : "'" + path1.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path2 == null) ? "NULL" : "'" + path2.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path3 == null) ? "NULL" : "'" + path3.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path4 == null) ? "NULL" : "'" + path4.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((AnsStatus == null) ? "NULL" : "'" + AnsStatus.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path5 == null) ? "NULL" : "'" + path5.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path6 == null) ? "NULL" : "'" + path6.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path7 == null) ? "NULL" : "'" + path7.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path8 == null) ? "NULL" : "'" + path8.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path9 == null) ? "NULL" : "'" + path9.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path10 == null) ? "NULL" : "'" + path10.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path11 == null) ? "NULL" : "'" + path11.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path12 == null) ? "NULL" : "'" + path12.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path13 == null) ? "NULL" : "'" + path13.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path14 == null) ? "NULL" : "'" + path14.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path15 == null) ? "NULL" : "'" + path15.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path16 == null) ? "NULL" : "'" + path16.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path17 == null) ? "NULL" : "'" + path17.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path18 == null) ? "NULL" : "'" + path18.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path19 == null) ? "NULL" : "'" + path19.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path20 == null) ? "NULL" : "'" + path20.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path21 == null) ? "NULL" : "'" + path21.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path22 == null) ? "NULL" : "'" + path22.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path23 == null) ? "NULL" : "'" + path23.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path24 == null) ? "NULL" : "'" + path24.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path25 == null) ? "NULL" : "'" + path25.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path26 == null) ? "NULL" : "'" + path26.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path27 == null) ? "NULL" : "'" + path27.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path28 == null) ? "NULL" : "'" + path28.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path29 == null) ? "NULL" : "'" + path29.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path30 == null) ? "NULL" : "'" + path30.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path31 == null) ? "NULL" : "'" + path31.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path32 == null) ? "NULL" : "'" + path32.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path33 == null) ? "NULL" : "'" + path33.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path34 == null) ? "NULL" : "'" + path34.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path35 == null) ? "NULL" : "'" + path35.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path36 == null) ? "NULL" : "'" + path36.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path37 == null) ? "NULL" : "'" + path37.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path38 == null) ? "NULL" : "'" + path38.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path39 == null) ? "NULL" : "'" + path39.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((path40 == null) ? "NULL" : "'" + path40.ToString().Replace("'", "''") + "'") + " " +
                                                                     " , " + ((StudentPhoto == null) ? "NULL" : "'" + StudentPhoto.ToString().Replace("'", "''") + "'") + " " +
                                                                     "); Select @ExamId;");

                    da = _aPI_BLL.returnDataTable(" select ExamId from Exams where ExamId = '" + ExamId.ToString() + "'  ");
                    st.Append(DataTableToJsonObj(da));
                }
            }
            else if (da.Rows.Count > 0)
            {
                if (IsJeeNeet == true)
                {
                    if (Ans != null)
                    {
                        _aPI_BLL.InsertUpdateNonQuery(" UPDATE Exams SET " +
                                                      " Ans = " + ((Ans == null) ? "NULL" : "'" + Ans.ToString().Replace("'", "''") + "'") +
                                                      ",AnsStatus = " + ((AnsStatus == null) ? "NULL" : "'" + AnsStatus.ToString().Replace("'", "''") + "'") +
                                                      ",LastUpdatedOn=GETDATE() " +
                                                      ",LastUpdatedByUserId = '" + InsertedByUserId + "'" +
                                                      " WHERE ExamId = '" + da.Rows[0]["ExamId"].ToString() + "' ");

                        da = _aPI_BLL.returnDataTable(" select ExamId from Exams where ExamId = '" + da.Rows[0]["ExamId"].ToString() + "'  ");
                        st.Append(DataTableToJsonObj(da));
                    }
                    else
                    {
                        string path1 = null, path2 = null, path3 = null, path4 = null, path5 = null, path6 = null, path7 = null, path8 = null, path9 = null, path10 = null;
                        string path11 = null;
                        string path12 = null;
                        string path13 = null;
                        string path14 = null;
                        string path15 = null;
                        string path16 = null;
                        string path17 = null;
                        string path18 = null;
                        string path19 = null;
                        string path20 = null;
                        string path21 = null;
                        string path22 = null;
                        string path23 = null;
                        string path24 = null;
                        string path25 = null;
                        string path26 = null;
                        string path27 = null;
                        string path28 = null;
                        string path29 = null;
                        string path30 = null;
                        string path31 = null;
                        string path32 = null;
                        string path33 = null;
                        string path34 = null;
                        string path35 = null;
                        string path36 = null;
                        string path37 = null;
                        string path38 = null;
                        string path39 = null;
                        string path40 = null;
                        string StudentPhoto = null;

                        #region "old code for file upload"

                        //if (file1 != null && file1.FileName != "")
                        //{
                        //    byte[] imageSize = new byte[file1.ContentLength];
                        //    HttpPostedFile uploadedImage = file1;

                        //    string[] getExtenstion = file1.FileName.Split('.');
                        //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                        //    path1 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F1" + "." + oExtension;
                        //    uploadedImage.SaveAs(path1);
                        //}

                        //if (file2 != null && file2.FileName != "")
                        //{
                        //    byte[] imageSize = new byte[file2.ContentLength];
                        //    HttpPostedFile uploadedImage = file2;

                        //    string[] getExtenstion = file2.FileName.Split('.');
                        //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                        //    path2 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F2" + "." + oExtension;
                        //    uploadedImage.SaveAs(path2);
                        //}

                        //if (file3 != null && file3.FileName != "")
                        //{
                        //    byte[] imageSize = new byte[file3.ContentLength];
                        //    HttpPostedFile uploadedImage = file3;

                        //    string[] getExtenstion = file3.FileName.Split('.');
                        //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                        //    path3 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F3" + "." + oExtension;
                        //    uploadedImage.SaveAs(path3);
                        //}

                        //if (file4 != null && file4.FileName != "")
                        //{
                        //    byte[] imageSize = new byte[file4.ContentLength];
                        //    HttpPostedFile uploadedImage = file4;

                        //    string[] getExtenstion = file4.FileName.Split('.');
                        //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                        //    path4 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F4" + "." + oExtension;
                        //    uploadedImage.SaveAs(path4);
                        //}

                        //if (file5 != null && file5.FileName != "")
                        //{
                        //    byte[] imageSize = new byte[file5.ContentLength];
                        //    HttpPostedFile uploadedImage = file5;

                        //    string[] getExtenstion = file5.FileName.Split('.');
                        //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                        //    path5 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F5" + "." + oExtension;
                        //    uploadedImage.SaveAs(path5);
                        //}

                        //if (file6 != null && file6.FileName != "")
                        //{
                        //    byte[] imageSize = new byte[file6.ContentLength];
                        //    HttpPostedFile uploadedImage = file6;

                        //    string[] getExtenstion = file6.FileName.Split('.');
                        //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                        //    path6 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F6" + "." + oExtension;
                        //    uploadedImage.SaveAs(path6);
                        //}

                        //if (file7 != null && file7.FileName != "")
                        //{
                        //    byte[] imageSize = new byte[file7.ContentLength];
                        //    HttpPostedFile uploadedImage = file7;

                        //    string[] getExtenstion = file7.FileName.Split('.');
                        //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                        //    path7 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F7" + "." + oExtension;
                        //    uploadedImage.SaveAs(path7);
                        //}

                        //if (file8 != null && file8.FileName != "")
                        //{
                        //    byte[] imageSize = new byte[file8.ContentLength];
                        //    HttpPostedFile uploadedImage = file8;

                        //    string[] getExtenstion = file8.FileName.Split('.');
                        //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                        //    path8 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F8" + "." + oExtension;
                        //    uploadedImage.SaveAs(path8);
                        //}

                        //if (file9 != null && file9.FileName != "")
                        //{
                        //    byte[] imageSize = new byte[file9.ContentLength];
                        //    HttpPostedFile uploadedImage = file9;

                        //    string[] getExtenstion = file9.FileName.Split('.');
                        //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                        //    path9 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F9" + "." + oExtension;
                        //    uploadedImage.SaveAs(path9);
                        //}

                        //if (file10 != null && file10.FileName != "")
                        //{
                        //    byte[] imageSize = new byte[file10.ContentLength];
                        //    HttpPostedFile uploadedImage = file10;

                        //    string[] getExtenstion = file10.FileName.Split('.');
                        //    string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                        //    path10 = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F10" + "." + oExtension;
                        //    uploadedImage.SaveAs(path10);
                        //}

                        #endregion

                        path1 = saveFile(file1, 1);
                        path2 = saveFile(file2, 2);
                        path3 = saveFile(file3, 3);
                        path4 = saveFile(file4, 4);
                        path5 = saveFile(file5, 5);
                        path6 = saveFile(file6, 6);
                        path7 = saveFile(file7, 7);
                        path8 = saveFile(file8, 8);
                        path9 = saveFile(file9, 9);
                        path10 = saveFile(file10, 10);
                        path11 = saveFile(file11, 11);
                        path12 = saveFile(file12, 12);
                        path13 = saveFile(file13, 13);
                        path14 = saveFile(file14, 14);
                        path15 = saveFile(file15, 15);
                        path16 = saveFile(file16, 16);
                        path17 = saveFile(file17, 17);
                        path18 = saveFile(file18, 18);
                        path19 = saveFile(file19, 19);
                        path20 = saveFile(file20, 20);
                        path21 = saveFile(file21, 21);
                        path22 = saveFile(file22, 22);
                        path23 = saveFile(file23, 23);
                        path24 = saveFile(file24, 24);
                        path25 = saveFile(file25, 25);
                        path26 = saveFile(file26, 26);
                        path27 = saveFile(file27, 27);
                        path28 = saveFile(file28, 28);
                        path29 = saveFile(file29, 29);
                        path30 = saveFile(file30, 30);
                        path31 = saveFile(file31, 31);
                        path32 = saveFile(file32, 32);
                        path33 = saveFile(file33, 33);
                        path34 = saveFile(file34, 34);
                        path35 = saveFile(file35, 35);
                        path36 = saveFile(file36, 36);
                        path37 = saveFile(file37, 37);
                        path38 = saveFile(file38, 38);
                        path39 = saveFile(file39, 39);
                        path40 = saveFile(file40, 40);
                        StudentPhoto = saveFile(fileStudentPhoto, 41);

                        //if (path1 != null || path2 != null || path3 != null || path4 != null)
                        //{
                        _aPI_BLL.InsertUpdateNonQuery(" UPDATE Exams SET " +
                                                      " LastUpdatedOn=GETDATE() " +
                                                      " , LastUpdatedByUserId = '" + InsertedByUserId + "'" +
                                                      " , Ans = " + ((Ans == null) ? "NULL" : "'" + Ans.ToString().Replace("'", "''") + "'") +
                                                      " , AnsStatus = " + ((AnsStatus == null) ? "NULL" : "'" + AnsStatus.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path1 == null) ? "" : ",AnsImage1 = '" + path1.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path2 == null) ? "" : ",AnsImage2 = '" + path2.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path3 == null) ? "" : ",AnsImage3 = '" + path3.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path4 == null) ? "" : ",AnsImage4 = '" + path4.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path5 == null) ? "" : ",AnsImage5 = '" + path5.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path6 == null) ? "" : ",AnsImage6 = '" + path6.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path7 == null) ? "" : ",AnsImage7 = '" + path7.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path8 == null) ? "" : ",AnsImage8 = '" + path8.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path9 == null) ? "" : ",AnsImage9 = '" + path9.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path10 == null) ? "" : ",AnsImage10 = '" + path10.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path11 == null) ? "" : ",AnsImage11 = '" + path11.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path12 == null) ? "" : ",AnsImage12 = '" + path12.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path13 == null) ? "" : ",AnsImage13 = '" + path13.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path14 == null) ? "" : ",AnsImage14 = '" + path14.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path15 == null) ? "" : ",AnsImage15 = '" + path15.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path16 == null) ? "" : ",AnsImage16 = '" + path16.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path17 == null) ? "" : ",AnsImage17 = '" + path17.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path18 == null) ? "" : ",AnsImage18 = '" + path18.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path19 == null) ? "" : ",AnsImage19 = '" + path19.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path20 == null) ? "" : ",AnsImage20 = '" + path20.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path21 == null) ? "" : ",AnsImage21 = '" + path21.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path22 == null) ? "" : ",AnsImage22 = '" + path22.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path23 == null) ? "" : ",AnsImage23 = '" + path23.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path24 == null) ? "" : ",AnsImage24 = '" + path24.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path25 == null) ? "" : ",AnsImage25 = '" + path25.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path26 == null) ? "" : ",AnsImage26 = '" + path26.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path27 == null) ? "" : ",AnsImage27 = '" + path27.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path28 == null) ? "" : ",AnsImage28 = '" + path28.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path29 == null) ? "" : ",AnsImage29 = '" + path29.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path30 == null) ? "" : ",AnsImage30 = '" + path30.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path31 == null) ? "" : ",AnsImage31 = '" + path31.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path32 == null) ? "" : ",AnsImage32 = '" + path32.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path33 == null) ? "" : ",AnsImage33 = '" + path33.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path34 == null) ? "" : ",AnsImage34 = '" + path34.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path35 == null) ? "" : ",AnsImage35 = '" + path35.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path36 == null) ? "" : ",AnsImage36 = '" + path36.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path37 == null) ? "" : ",AnsImage37 = '" + path37.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path38 == null) ? "" : ",AnsImage38 = '" + path38.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path39 == null) ? "" : ",AnsImage39 = '" + path39.ToString().Replace("'", "''") + "'") +
                                                      " " + ((path40 == null) ? "" : ",AnsImage40 = '" + path40.ToString().Replace("'", "''") + "'") +
                                                      " " + ((StudentPhoto == null) ? "" : ",StudentPhoto = '" + StudentPhoto.ToString().Replace("'", "''") + "'") +
                                                      " WHERE ExamId = '" + da.Rows[0]["ExamId"].ToString() + "' ");

                        da = _aPI_BLL.returnDataTable(" select ExamId from Exams where ExamId = '" + da.Rows[0]["ExamId"].ToString() + "'  ");
                        st.Append(DataTableToJsonObj(da));
                        //}
                    }
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
                ReturnVal = GetReturnValue("209", "Entry Exist", st);
            }

            if (st.ToString() == "[]" || st.ToString() == "")
            {
                ReturnVal = GetReturnValue("209", "Entry Exist", st);
            }

            if (da != null)
            {
                if (da.Rows.Count > 0)
                {
                    ReturnVal = GetReturnValue("200", "Ok", st);
                }
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

    public string saveFile(HttpPostedFile fu, int srno)
    {
        string path = null;

        if (fu != null && fu.FileName != "")
        {
            byte[] imageSize = new byte[fu.ContentLength];
            HttpPostedFile uploadedImage = fu;

            string[] getExtenstion = fu.FileName.Split('.');
            string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

            string msExcelFilePathOnServer = ConfigurationSettings.AppSettings["FolderPath"] + "" + Convert.ToDateTime(System.DateTime.Now).ToString("ddMMMyyyy") + "/";

            bool exists = System.IO.Directory.Exists(msExcelFilePathOnServer);

            if (!exists)
                System.IO.Directory.CreateDirectory(msExcelFilePathOnServer);

            path = msExcelFilePathOnServer + uploadedImage.FileName.Replace("." + oExtension, "") + "_" + InsertedByUserId.ToString() + "_" + QueId.ToString() + "_F" + srno + "" + "." + oExtension;
            uploadedImage.SaveAs(path);
        }

        return path;
    }
}