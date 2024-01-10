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

public class ImportStudentsBLL
{
    ImportStudentsDAL _ImportStudentsDAL;
    ImportStudentsDTO _ImportStudentsDTO;
    GeneralDAL _generalDAL;
    private HttpPostedFile _msExcelFile;
    private string _msExcelFileNameOnServer;
    private string _sheetName;

    public ImportStudentsBLL()
    {
        _generalDAL = new GeneralDAL();
        _ImportStudentsDAL = new ImportStudentsDAL();
        _ImportStudentsDTO = new ImportStudentsDTO();
    }
    ~ImportStudentsBLL()
    {
        if (_msExcelFileNameOnServer != null)
            DeleteMSExcelFileInTemp();

        _generalDAL = null;
        _ImportStudentsDAL = null;
        _ImportStudentsDTO = null;
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

    public string StandardId
    {
        get
        {
            return _ImportStudentsDTO.StandardId;
        }
        set
        {
            _ImportStudentsDTO.StandardId = value;
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
            throw new Exception(ex.Message);
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

                if (a1.Replace("#", ".") != "ExamNo")
                {
                    valid = false;
                }
                else if (b1.Replace("#", ".") != "Student Name")
                {
                    valid = false;
                }
                else if (c1.Replace("#", ".") != "Mobile No")
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

    private bool NameExists(string StandardId, string StudentName, string MobileNo)
    {
        try
        {
            ImportStudentsDAL ImportStudentsDAL = new ImportStudentsDAL();

            return ImportStudentsDAL.NameExists(StandardId, StudentName, MobileNo);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public SortedList ValidateSheetName()
    {
        SortedList sl = new SortedList();

        if (_sheetName == null || _sheetName == "")
        {
            sl.Add("SheetName", "Sheet Name cannot be blank.");
        }

        if (_ImportStudentsDTO.StandardId == null)
        {
            sl.Add("StandardId", "Select Standard.");
        }
        return sl;
    }
    public SortedList Validate()
    {
        OleDbCommand objCommand;
        SortedList sl = new SortedList();
        bool valid;
        string StudentName = "";
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
                string MobileNo = "";
                Dictionary<string, int> ds = new Dictionary<string, int>();

                for (rowNo = 0; rowNo <= dtMSExcelSheetDataSource.Rows.Count - 1; rowNo++)
                {
                    try
                    {
                        StudentName = dtMSExcelSheetDataSource.Rows[rowNo][0].ToString();
                    }
                    catch
                    {
                    }
                    try
                    {
                        MobileNo = dtMSExcelSheetDataSource.Rows[rowNo][1].ToString();
                    }
                    catch
                    {
                    }
                    valid = ValidateMSExcelSheetRows(ref messages, rowNo, _ImportStudentsDTO.StandardId, StudentName, MobileNo, dtMSExcelSheetDataSource, ds);
                }
                if (StudentName == "" && rowNo == 0)
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

    public bool ValidateMSExcelSheetRows(ref string messages, int rowNo, string StandardId, string StudentName, string MobileNo, DataTable dt, Dictionary<string, int> ds)
    {
        try
        {
            bool valid = true;

            if (NameExists(StandardId, StudentName, MobileNo) == true)
            {
                messages += "Student Already Exist with Row No : " + (rowNo).ToString() + "" + "|";
                valid = false;
            }

            if (StudentName != "")
            {
                if (StudentName == "")
                {
                    messages += "Student Name Blank with Row No : " + (rowNo).ToString() + "" + "|";
                    valid = false;
                }

                int i = rowNo;

                for (; i <= rowNo; i++)
                {
                    string s = dt.Rows[i][0].ToString();
                    if (ds.ContainsKey(s))
                    {
                        messages += "Students Duplicate with Row No : " + (rowNo).ToString() + "" + "|";
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
            dtMSExcelSheetData.Columns.Add("ExamNo", typeof(string));
            dtMSExcelSheetData.Columns.Add("StudentName", typeof(string));
            dtMSExcelSheetData.Columns.Add("MobileNo", typeof(string));
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
                    dr["ExamNo"] = dtMSExcelSheetDataSource.Rows[rowNo][0].ToString();
                else
                    dr["ExamNo"] = DBNull.Value;

                if (dtMSExcelSheetDataSource.Rows[rowNo][1] != DBNull.Value)
                    dr["StudentName"] = dtMSExcelSheetDataSource.Rows[rowNo][1].ToString();
                else
                    dr["StudentName"] = DBNull.Value;

                if (dtMSExcelSheetDataSource.Rows[rowNo][2] != DBNull.Value)
                    dr["MobileNo"] = dtMSExcelSheetDataSource.Rows[rowNo][2].ToString();
                else
                    dr["MobileNo"] = DBNull.Value;

                dtMSExceSheetData.Rows.Add(dr);
            }

            dtMSExceSheetData.AcceptChanges();

            _ImportStudentsDAL.Save(dtMSExceSheetData, _ImportStudentsDTO);

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
            //FileInfo fileInfo = new FileInfo(_msExcelFileNameOnServer);
            //fileInfo.Delete();
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
    public DataTable LoadStandard()
    {
        try
        {
            return _generalDAL.TextList("Standard");
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
            _ImportStudentsDAL.Save(dtt, _ImportStudentsDTO);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}