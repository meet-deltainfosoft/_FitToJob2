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
using System.IO;
using System.Data.OleDb;
using System.Collections;
using Microsoft.VisualBasic;
using System.Data.Common;
using System.Collections.Generic;

public class ImportQuestionBLL
{
    ImportQuestionDAL _ImportQuestionDAL;
    ImportQuestionDTO _ImportQuestionDTO;
    GeneralDAL _generalDAL;
    private HttpPostedFile _msExcelFile;
    private string _msExcelFileNameOnServer;
    private string _sheetName;

    public ImportQuestionBLL()
    {
        _generalDAL = new GeneralDAL();
        _ImportQuestionDAL = new ImportQuestionDAL();
        _ImportQuestionDTO = new ImportQuestionDTO();
    }
    ~ImportQuestionBLL()
    {
        if (_msExcelFileNameOnServer != null)
            DeleteMSExcelFileInTemp();

        _generalDAL = null;
        _ImportQuestionDAL = null;
        _ImportQuestionDTO = null;
    }

    public HttpPostedFile MSExcelFile
    {

        get
        {
            return _msExcelFile;
        }
        set
        {
            try
            {
                if (_msExcelFileNameOnServer != null)
                    DeleteMSExcelFileInTemp();

                if (value.ContentType.ToString() == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" || value.ContentType.ToString() == "application/vnd.ms-excel")
                {
                    _msExcelFile = value;
                    SaveMSExcelFileInTemp();
                }
                else
                {
                    throw new Exception("Invalid file, Only MS Excel file is allowed.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

    private void SaveMSExcelFileInTemp()
    {
        try
        {
            //This function is used to save the file on server

            string msExcelFileNameOnServer = Guid.NewGuid().ToString();
            string msExcelFileExtension = Path.GetExtension(_msExcelFile.FileName);
            string msExcelFilePathOnServer = HttpContext.Current.Server.MapPath("~/Temp/");

            _msExcelFile.SaveAs(msExcelFilePathOnServer + msExcelFileNameOnServer + msExcelFileExtension);

            _msExcelFileNameOnServer = msExcelFilePathOnServer + msExcelFileNameOnServer + "" + msExcelFileExtension;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public string SheetName
    {
        set
        {
            _sheetName = value;
        }
    }

    public string SubjectId
    {
        get
        {
            return _ImportQuestionDTO.SubjectId;
        }
        set
        {
            _ImportQuestionDTO.SubjectId = value;
        }
    }

    public string TestId
    {
        get
        {
            return _ImportQuestionDTO.TestId;
        }
        set
        {
            _ImportQuestionDTO.TestId = value;
        }
    }

    public string StandardTextListId
    {
        get
        {
            return _ImportQuestionDTO.StandardTextListId;
        }
        set
        {
            _ImportQuestionDTO.StandardTextListId = value;
        }
    }
    private OleDbConnection ExcelConnection()
    {
        try
        {
            string ConnnectionString = "";

            if (_msExcelFile.ContentType.ToString() == "application/vnd.ms-excel")
                ConnnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _msExcelFileNameOnServer + ";Extended Properties=Excel 8.0";
            else
                ConnnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + _msExcelFileNameOnServer + ";Extended Properties=Excel 12.0";

            OleDbConnection objConn = new OleDbConnection(ConnnectionString);
            objConn.Open();

            return objConn;
        }
        catch (Exception ex)
        {

            try
            {

                string ConnnectionString = "";

                if (_msExcelFile.ContentType.ToString() == "application/vnd.ms-excel")
                    ConnnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _msExcelFileNameOnServer + ";Extended Properties=Excel 8.0";
                else
                    ConnnectionString = "Provider=Microsoft.ACE.OLEDB.16.0;Data Source=" + _msExcelFileNameOnServer + ";Extended Properties=Excel 12.0";

                OleDbConnection objConn = new OleDbConnection(ConnnectionString);
                objConn.Open();

                return objConn;

            }
            catch (Exception ex1)
            { 
                throw new Exception(ex.Message);
            }
        }
    }
    public string[] GetSheetNames()
    {
        try
        {
            DataTable dt = null;

            dt = ExcelConnection().GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }

            String[] excelSheetNames = new String[dt.Rows.Count];
            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                excelSheetNames[i] = row["TABLE_NAME"].ToString().Substring(0, row["TABLE_NAME"].ToString().Length - 1);
                i++;
            }

            return excelSheetNames;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private bool SheetConfirmsToTemplateStd(DataTable dt)
    {
        try
        {
            bool valid = true;
            string a1, b1, c1, d1, e1, f1, g1, h1;

            if (dt.Columns.Count > 0)
            {
                a1 = dt.Columns[0].ColumnName;

                b1 = dt.Columns[1].ColumnName;

                c1 = dt.Columns[2].ColumnName;

                d1 = dt.Columns[3].ColumnName;

                e1 = dt.Columns[4].ColumnName;

                f1 = dt.Columns[5].ColumnName;

                g1 = dt.Columns[6].ColumnName;

                h1 = dt.Columns[7].ColumnName;

                if (a1.Replace("#", ".") != "Question No")
                {
                    valid = false;
                }
                else if (b1.Replace("#", ".") != "Question")
                {
                    valid = false;
                }
                else if (c1.Replace("#", ".") != "Opt. A")
                {
                    valid = false;
                }
                else if (d1.Replace("#", ".") != "Opt. B")
                {
                    valid = false;
                }
                else if (e1.Replace("#", ".") != "Opt. C")
                {
                    valid = false;
                }
                else if (f1.Replace("#", ".") != "Opt. D")
                {
                    valid = false;
                }
                else if (g1.Replace("#", ".") != "Correct Ans.")
                {
                    valid = false;
                }
                else if (h1 != "HashTag")
                {
                    valid = false;
                }
            }
            else
            {
                valid = false;
            }

            return valid;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private bool NameExists(string SubjectId, string TestId, string Question)
    {
        try
        {
            ImportQuestionDAL ImportQuestionDAL = new ImportQuestionDAL();

            return ImportQuestionDAL.NameExists(SubjectId, TestId, Question);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public SortedList ValidateSheetName()
    {
        SortedList sl = new SortedList();

        //Sheet Name
        if (_sheetName == null || _sheetName == "")
        {
            sl.Add("SheetName", "Sheet Name cannot be blank.");
        }

        //Month And Year
        if (_ImportQuestionDTO.SubjectId == null)
        {
            sl.Add("SubjectId", "Select Subject.");
        }

        if (_ImportQuestionDTO.TestId == null)
        {
            sl.Add("TestId", "Select Test Name.");
        }
        return sl;
    }
    public SortedList Validate()
    {
        OleDbCommand objCommand;
        SortedList sl = new SortedList();
        bool valid;
        string Que = "";
        string messages = "";

        try
        {
            objCommand = new OleDbCommand("SELECT * FROM [" + _sheetName + "$]", ExcelConnection());
            DataTable dtMSExcelSheetDataSource = new DataTable();
            dtMSExcelSheetDataSource.Load(objCommand.ExecuteReader(CommandBehavior.CloseConnection));
            objCommand = null;

            valid = SheetConfirmsToTemplateStd(dtMSExcelSheetDataSource);
            if (valid == false)
            {
                sl.Add("Template", "MS Excel Sheet Template is invalid.");
            }
            else
            {
                int rowNo;
                int QueNo;
                Dictionary<string, int> ds = new Dictionary<string, int>();

                for (rowNo = 0; rowNo <= dtMSExcelSheetDataSource.Rows.Count - 1; rowNo++)
                {
                    Que = dtMSExcelSheetDataSource.Rows[rowNo][1].ToString();
                    QueNo = Convert.ToInt16(dtMSExcelSheetDataSource.Rows[rowNo][0].ToString());
                    valid = ValidateMSExcelSheetRows(ref messages, rowNo, _ImportQuestionDTO.SubjectId, _ImportQuestionDTO.TestId, Que, QueNo, dtMSExcelSheetDataSource, ds);
                }
                if (Que == "" && rowNo == 0)
                    messages += "Data must be present in at least one row." + "|";

                if (messages.Trim() != "")
                {
                    sl.Add("Data", messages);

                    //Converting the messages into multiple lines for Bulleted List.
                    BeautifyErroMessages(ref sl);
                }
            }

            return sl;
        }
        catch (Exception ex)
        {
            return sl;
        }
    }

    public bool ValidateMSExcelSheetRows(ref string messages, int rowNo, string SubjectId, string TestId, string Question, int QueNo, DataTable dt, Dictionary<string, int> ds)
    {
        try
        {
            bool valid = true;

            if (NameExists(SubjectId, TestId, Question) == true)
            {
                messages += "Question Already Exist with Question No : " + (QueNo).ToString() + "." + "|";
                valid = false;
            }

            if (Question != "")
            {
                if (Question == "")
                {
                    messages += "Question Cannot Be Blank with Question No : " + (QueNo).ToString() + "." + "|";
                    valid = false;
                }

                int i = rowNo;

                for (; i <= rowNo; i++)
                {
                    string s = dt.Rows[i][1].ToString();
                    if (ds.ContainsKey(s))
                    {
                        messages += "Question Duplicate with Question No : " + (QueNo).ToString() + "." + "|";
                        valid = false;
                    }
                    else
                    {
                        ds.Add(s, 1);
                    }
                    // data.Add(s);   
                }
            }


            return valid;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void BeautifyErroMessages(ref SortedList sl)
    {
        try
        {
            string messages = (string)sl["Data"];

            if (messages != null)
            {
                string[] message = messages.Split(Convert.ToChar("|"));

                sl.Remove("Data");

                for (int count = 0; count < message.Length - 1; count++)
                {
                    sl.Add("Data" + count.ToString(), message[count]);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    private void CreateDataTable(ref System.Data.DataTable dtMSExcelSheetData)
    {
        try
        {
            dtMSExcelSheetData.Columns.Clear();
            dtMSExcelSheetData.Columns.Add("QueNo", typeof(string));
            dtMSExcelSheetData.Columns.Add("Question", typeof(string));
            dtMSExcelSheetData.Columns.Add("OptA", typeof(string));
            dtMSExcelSheetData.Columns.Add("OptB", typeof(string));
            dtMSExcelSheetData.Columns.Add("OptC", typeof(string));
            dtMSExcelSheetData.Columns.Add("OptD", typeof(string));
            dtMSExcelSheetData.Columns.Add("Ans", typeof(string));
            dtMSExcelSheetData.Columns.Add("Hashtag", typeof(string));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void Save()
    {
        DataTable dtMSExceSheetData = new DataTable();
        DataRow dr;

        OleDbCommand objCommand;

        try
        {
            objCommand = new OleDbCommand("SELECT * FROM [" + _sheetName + "$]", ExcelConnection());
            DataTable dtMSExcelSheetDataSource = new DataTable();
            dtMSExcelSheetDataSource.Load(objCommand.ExecuteReader(CommandBehavior.CloseConnection));
            objCommand = null;
            CreateDataTable(ref dtMSExceSheetData);
            for (int rowNo = 0; rowNo <= dtMSExcelSheetDataSource.Rows.Count - 1; rowNo++)
            {
                if (dtMSExcelSheetDataSource.Rows[rowNo][0].ToString() == "")
                    break;

                dr = dtMSExceSheetData.NewRow();

                if (dtMSExcelSheetDataSource.Rows[rowNo][0] != DBNull.Value)
                    dr["QueNo"] = dtMSExcelSheetDataSource.Rows[rowNo][0].ToString();
                else
                    dr["QueNo"] = DBNull.Value;

                if (dtMSExcelSheetDataSource.Rows[rowNo][1] != DBNull.Value)
                    dr["Question"] = dtMSExcelSheetDataSource.Rows[rowNo][1].ToString();
                else
                    dr["Question"] = DBNull.Value;

                if (dtMSExcelSheetDataSource.Rows[rowNo][2] != DBNull.Value)
                    dr["OptA"] = dtMSExcelSheetDataSource.Rows[rowNo][2].ToString();
                else
                    dr["OptA"] = DBNull.Value;

                if (dtMSExcelSheetDataSource.Rows[rowNo][3] != DBNull.Value)
                    dr["OptB"] = dtMSExcelSheetDataSource.Rows[rowNo][3].ToString();
                else
                    dr["OptB"] = DBNull.Value;

                if (dtMSExcelSheetDataSource.Rows[rowNo][4] != DBNull.Value)
                    dr["OptC"] = dtMSExcelSheetDataSource.Rows[rowNo][4].ToString();
                else
                    dr["OptC"] = DBNull.Value;

                if (dtMSExcelSheetDataSource.Rows[rowNo][5] != DBNull.Value)
                    dr["OptD"] = dtMSExcelSheetDataSource.Rows[rowNo][5].ToString();
                else
                    dr["OptD"] = DBNull.Value;

                if (dtMSExcelSheetDataSource.Rows[rowNo][6] != DBNull.Value)
                    switch (dtMSExcelSheetDataSource.Rows[rowNo][6].ToString().Trim())
                        { 
                            case "A": case "a":
                                dr["Ans"] = 1;
                                break;
                            case "B": case "b":
                                dr["Ans"] = 2;
                                break;
                            case "C":  case "c":
                                dr["Ans"] = 3;
                                break;
                            case "D":   case "d":
                                dr["Ans"] = 4;
                                break;
                            default:
                                dr["Ans"] = Convert.ToInt16(dtMSExcelSheetDataSource.Rows[rowNo][6].ToString().Trim());
                                break;
                        }
                else
                    dr["Ans"] = DBNull.Value;
                try
                {
                    if (dtMSExcelSheetDataSource.Rows[rowNo][7] != DBNull.Value)
                        dr["Hashtag"] = dtMSExcelSheetDataSource.Rows[rowNo][7].ToString();
                    else
                        dr["Hashtag"] = DBNull.Value;
                }
                catch
                {
                    dr["Hashtag"] = DBNull.Value;
                }
               

                dtMSExceSheetData.Rows.Add(dr);
            }

            dtMSExceSheetData.AcceptChanges();

            _ImportQuestionDAL.Save(dtMSExceSheetData, _ImportQuestionDTO);

            DeleteMSExcelFileInTemp();
        }
        catch
        {
            DeleteMSExcelFileInTemp();
            throw new Exception();
        }
    }
    private void DeleteMSExcelFileInTemp()
    {
        try
        {
            FileInfo fileInfo = new FileInfo(_msExcelFileNameOnServer);
            fileInfo.Delete();
        }
        catch
        {
        }
        finally
        {
            _msExcelFileNameOnServer = null;
            _sheetName = null;
            _msExcelFile = null;
        }
    }
    public DataTable LoadSubject()
    {
        try
        {
            return _ImportQuestionDAL.LoadSubject(_ImportQuestionDTO.StandardTextListId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public DataTable LoadTest()
    {
        try
        {
            return _ImportQuestionDAL.LoadTest(_ImportQuestionDTO.SubjectId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public void SaveData(DataTable dtt)
    {
        try
        {
            _ImportQuestionDAL.Save(dtt, _ImportQuestionDTO);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public DataTable LoadStandard()
    {
        try
        {
            GeneralDAL _generalDAL = new GeneralDAL();
            return _generalDAL.TextList("Standard");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}