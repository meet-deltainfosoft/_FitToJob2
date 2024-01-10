using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

public partial class Exams_ImportQuestion : System.Web.UI.Page
{
    ImportQuestionBLL _ImportQuestionBLL = new ImportQuestionBLL();
    GeneralDAL _generalDAL = new GeneralDAL();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                _ImportQuestionBLL = new ImportQuestionBLL();
                Session["_ImportQuestionBLL"] = _ImportQuestionBLL;
            }
            else
            {
                _ImportQuestionBLL = (ImportQuestionBLL)Session["_ImportQuestionBLL"];
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                LoadStandard();
                Label1.Visible = false;
                lblCount.Visible = false;
                btnImport.Enabled = false;
            }
            else
            {
                btnImport.Enabled = false;
            }
            if (Request.Form["__EVENTTARGET"] == "SetMSExcelFile")
            {
                SetMSExcelFile();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    private bool ValidateSheetName()
    {
        try
        {
            HideErrors();

            SortedList sl = _ImportQuestionBLL.ValidateSheetName();

            if (sl.Count > 0)
            {
                for (int i = 0; i < sl.Count; i++)
                {
                    string key = (string)sl.GetKey(i);
                    string value = (string)sl[key];

                    ShowErrors(key, value);
                }
            }

            return (sl.Count == 0) ? true : false;
        }
        catch (Exception ex)
        {
            return false;
            ShowErrors("err", ex.Message);
        }
    }
    private bool Validate()
    {
        try
        {
            HideErrors();

            SortedList sl = _ImportQuestionBLL.Validate();

            if (sl.Count > 0)
            {
                for (int i = 0; i < sl.Count; i++)
                {
                    string key = (string)sl.GetKey(i);
                    string value = (string)sl[key];

                    ShowErrors(key, value);
                }
            }

            return (sl.Count == 0) ? true : false;
        }
        catch (Exception ex)
        {
            return false;
            ShowErrors("err", ex.Message);
        }
    }

    private void ShowErrors(string key, string value)
    {
        try
        {
            if (key == "Success")
                pnlErr.CssClass = "errors alert alert-success";
            else
                pnlErr.CssClass = "errors alert alert-danger";

            if (key == "Template")
            {
                btnImport.Enabled = false;
            }

            pnlErr.Visible = true;
            blErrs.Items.Add(new ListItem(value));
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    private void HideErrors()
    {
        try
        {
            pnlErr.Visible = false;
            blErrs.Items.Clear();
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void SetMSExcelFile()
    {
        HideErrors();

        try
        {
            _ImportQuestionBLL.MSExcelFile = fuFileName.PostedFile;

            LoadMSExcelSheetNames(_ImportQuestionBLL.GetSheetNames());
        }
        catch (Exception ex)
        {
            Reset();
            ShowErrors("File", ex.Message);
        }
    }
    private void Reset()
    {
        try
        {
            HideErrors();
            ddlSheetName.Items.Clear();
            btnImport.Enabled = false;
        }
        catch
        {

        }
    }
    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubject.SelectedIndex > 0)
            {
                _ImportQuestionBLL.SubjectId = ddlSubject.SelectedValue;
                LoadTest();
            }
            else
            {
                _ImportQuestionBLL.SubjectId = null;
                ddlTest.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlSheetName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSheetName.SelectedIndex > 0)
            {
                _ImportQuestionBLL.SheetName = ddlSheetName.SelectedItem.Text;

                if (ddlSubject.SelectedIndex > 0)
                {
                    _ImportQuestionBLL.SubjectId = ddlSubject.SelectedValue;
                }
                else
                {
                    _ImportQuestionBLL.SubjectId = null;
                }

                if (ddlTest.SelectedIndex > 0)
                {
                    _ImportQuestionBLL.TestId = ddlTest.SelectedValue;
                }
                else
                {
                    _ImportQuestionBLL.TestId = null;
                }
                string b;

                if (ImportValidate())
                {
                    btnImport.Enabled = true;
                    b = "1";
                    Session["b"] = b;
                }
                else
                {
                    //btnImport.Enabled = true;
                    b = "2";
                    Session["b"] = b;
                }

                pnlGrid.Visible = true;
                //Set Datasource to Grid from BLL

                //DataTable dt = new DataTable();
                //dt = _ImportQuestionBLL.LoadExcel();
                //Session["dt"] = dt;

                //gdvList.DataSource = dt;
                //gdvList.DataBind();

                //Label1.Visible = true;
                //lblCount.Visible = true;
                //lblCount.Text = " Records : [" + dt.Rows.Count + "]";
            }
            else
            {
                _ImportQuestionBLL.SheetName = null;
                btnImport.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadMSExcelSheetNames(string[] msExcelSheetNames)
    {
        try
        {
            ListItem li = new ListItem();

            ddlSheetName.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlSheetName.Items.Add(li);

            li = null;

            for (int i = 1; i <= msExcelSheetNames.Length; i++)
            {
                li = new ListItem();

                li.Text = msExcelSheetNames[i - 1];
                li.Value = i.ToString();

                if (li.Text.Contains('$'))
                {

                }
                else
                {
                    ddlSheetName.Items.Add(li);
                }

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            HideErrors();
            pnlGrid.Visible = false;
            Label1.Visible = false;
            lblCount.Visible = false;

            if (ddlSubject.SelectedIndex > 0)
            {
                _ImportQuestionBLL.SubjectId = ddlSubject.SelectedValue;
            }
            else
            {
                _ImportQuestionBLL.SubjectId = null;
            }

            if (ddlTest.SelectedIndex > 0)
            {
                _ImportQuestionBLL.TestId = ddlTest.SelectedValue;
            }
            else
            {
                _ImportQuestionBLL.TestId = null;
            }

            if (ddlSheetName.SelectedIndex > 0)
                _ImportQuestionBLL.SheetName = ddlSheetName.SelectedItem.Text;

            bool isValid = ValidateSheetName();
            if (isValid == true)
            {
                _ImportQuestionBLL.Save();
                Reset();

                btnImport.Enabled = false;

                ShowErrors("Data", "Question Imported Successfully..");

            }
            else
            {
                ShowErrors("Data", "You are Selected Wrong MS Excel File...");
                btnImport.Enabled = false;
            }
        }
        catch
        {
            Reset();
            ShowErrors("Data", "Error occured while importing data.");
        }
    }
    private bool ImportValidate()
    {
        HideErrors();

        SortedList sl = _ImportQuestionBLL.Validate();

        if (sl.Count > 0)
        {
            for (int i = 0; i < sl.Count; i++)
            {
                string key = (string)sl.GetKey(i);
                string value = (string)sl[key];

                ShowErrors(key, value);
            }
        }

        return (sl.Count == 0) ? true : false;
    }
    private void LoadSubject()
    {
        try
        {
            ListItem li = new ListItem();

            ddlSubject.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlSubject.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _ImportQuestionBLL.LoadSubject().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSubject.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    private void LoadTest()
    {
        try
        {
            ListItem li = new ListItem();

            ddlTest.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlTest.Items.Add(li);

            li = null;

            foreach (DataRow dtr in _ImportQuestionBLL.LoadTest().Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlTest.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    protected void gdvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


            }
            else
            {
                btnImport.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void gdvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //gdvList.PageIndex = e.NewPageIndex;
            //gdvList.DataSource = _ImportQuestionBLL.IncentiveList();
            //gdvList.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlTest_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubject.SelectedIndex > 0)
        {
            fuFileName.Enabled = true;
        }
        else
        {
            fuFileName.Enabled = false;
        }
    }
    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
            {
                _ImportQuestionBLL.StandardTextListId = ddlStandard.SelectedValue.ToString();
                LoadSubject();
            }
            else
            {
                _ImportQuestionBLL.StandardTextListId = null;
                ShowErrors("", "Please select standard to generate schedule");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadStandard()
    {
        try
        {
            ListItem li = new ListItem();

            ddlStandard.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlStandard.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ImportQuestionBLL.LoadStandard();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlStandard.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
}