using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Exams_TestQueBank : System.Web.UI.Page
{
    private TestQueBankBLL _testQueBankBLL;
    private QueBankBLL _queBankBLL;

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["TestId"] == null)
                {
                    _testQueBankBLL = new TestQueBankBLL();
                    _queBankBLL = new QueBankBLL();
                    _testQueBankBLL.dtDetaiils = CreateNewDataTable(_testQueBankBLL.dtDetaiils);
                    _testQueBankBLL.alTest = new ArrayList();
                }
                else
                {
                    _testQueBankBLL = new TestQueBankBLL(Request.QueryString["TestId"].ToString());
                }
                Session["_testQueBankBLL"] = _testQueBankBLL;
                Session["_queBankBLL"] = _queBankBLL;
            }
            else
            {
                _testQueBankBLL = (TestQueBankBLL)Session["_testQueBankBLL"];
                _queBankBLL = (QueBankBLL)Session["_queBankBLL"];
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                txtTestName.Focus();
                LoadStandard();
                LoadLevelOfQue();
                if (Request.QueryString["TestId"] != null)
                {
                    lblTitle.Text = " - [Edit Mode]";
                    LoadWebForm();
                    btnDelete.Enabled = true;
                    ShowErrors("err", "You cannot edit this entry. You have to delete this Test and generate again.");
                    btnAddToTest.Visible = false;
                }
                else
                {
                    btnDelete.Visible = false;
                }
            }
            else
            {
                HideErrors();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type=\"text/javascript\">CallPostBack();</script>", false);
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandardTextListId.SelectedIndex > 0)
                _testQueBankBLL.StandardId = ddlStandardTextListId.SelectedValue;
            else
                _testQueBankBLL.StandardId = null;

            if (ddlSubId.SelectedIndex > 0)
                _testQueBankBLL.SubId = ddlSubId.SelectedValue;
            else
                _testQueBankBLL.SubId = null;

            if (ddlPatternId.SelectedIndex > 0)
                _testQueBankBLL.PatternId = ddlPatternId.SelectedValue;
            else
                _testQueBankBLL.PatternId = null;

            if (txtEasy.Text.Trim().Length > 0)
                _testQueBankBLL.Easy = Convert.ToInt32(txtEasy.Text.Trim());
            else
                _testQueBankBLL.Easy = null;

            if (txtMedium.Text.Trim().Length > 0)
                _testQueBankBLL.Medium = Convert.ToInt32(txtMedium.Text.Trim());
            else
                _testQueBankBLL.Medium = null;

            if (txtHard.Text.Trim().Length > 0)
                _testQueBankBLL.Hard = Convert.ToInt32(txtHard.Text.Trim());
            else
                _testQueBankBLL.Hard = null;

            if (txtTotalQue.Text.Trim().Length > 0)
                _testQueBankBLL.TotalQue = Convert.ToInt32(txtTotalQue.Text.Trim());
            else
                _testQueBankBLL.TotalQue = null;

            if (ddlLevelOfQue.SelectedIndex > 0)
                _testQueBankBLL.LevelOfQue = ddlLevelOfQue.SelectedValue;
            else
                _testQueBankBLL.LevelOfQue = null;

            if (txtTestName.Text.Trim().Length > 0)
                _testQueBankBLL.Name = txtTestName.Text.Trim();
            else
                _testQueBankBLL.Name = null;

            if (rbTypeAuto.Checked)
                _testQueBankBLL.Type = "Auto";
            else
                _testQueBankBLL.Type = "Manual";

            if (ddlChapterId.SelectedIndex > 0)
                _testQueBankBLL.ChapterId = ddlChapterId.SelectedValue;
            else
                _testQueBankBLL.ChapterId = null;

            string PeriodNos = "";

            foreach (ListItem li in chkPeriodNo.Items)
            {
                if (li.Selected == true)
                {
                    if (PeriodNos == "")
                        PeriodNos += li.Value;
                    else
                        PeriodNos += "," + li.Value;
                }
            }

            if (PeriodNos != "")
                _testQueBankBLL.PeriodNo = PeriodNos;
            else
                _testQueBankBLL.PeriodNo = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                DataTable dt = new DataTable();
                dt = _testQueBankBLL.QueBankFoTest();
                gdvQues.DataSource = dt;
                gdvQues.DataBind();
                lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";
            }
            //else
            //{
            //    ShowErrors("err", "Select mandatory field for filter data.");
            //}

            ShowDetails(_testQueBankBLL.dtDetaiils);
            btnAddToTest.Focus();
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }

    protected void gdvQues_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;
                HyperLink hl = (HyperLink)e.Row.Cells[1].Controls[0];

                HyperLink hlA1 = (HyperLink)e.Row.Cells[3].Controls[0];
                HyperLink hlA2 = (HyperLink)e.Row.Cells[4].Controls[0];
                HyperLink hlA3 = (HyperLink)e.Row.Cells[5].Controls[0];
                HyperLink hlA4 = (HyperLink)e.Row.Cells[6].Controls[0];

                hl.NavigateUrl = "Que.aspx?QueId=" + drv[0].ToString();

                if (drv["ImageNameA1"] != DBNull.Value)
                {
                    hlA1.NavigateUrl = drv["ImageNameA1"].ToString();
                    hlA1.Target = "_blank";
                }

                if (drv["ImageNameA2"] != DBNull.Value)
                {
                    hlA2.NavigateUrl = drv["ImageNameA2"].ToString();
                    hlA2.Target = "_blank";
                }

                if (drv["ImageNameA3"] != DBNull.Value)
                {
                    hlA3.NavigateUrl = drv["ImageNameA3"].ToString();
                    hlA3.Target = "_blank";
                }

                if (drv["ImageNameA4"] != DBNull.Value)
                {
                    hlA4.NavigateUrl = drv["ImageNameA4"].ToString();
                    hlA4.Target = "_blank";
                }

                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                CheckBox chkSelectAll = (CheckBox)gdvQues.HeaderRow.FindControl("chkSelectAll");

                chkSelect.Attributes.Add("onclick", "javascript:Selectchildcheckboxes('" + chkSelectAll.ClientID + "','" + gdvQues.ClientID + "')");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }

    protected void ddlStandardTextListId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandardTextListId.SelectedIndex > 0)
            {
                _testQueBankBLL.StandardId = ddlStandardTextListId.SelectedValue;
                LoadPatterns();
                HideErrors();
                ddlPatternId.Focus();
            }
            else
            {
                _testQueBankBLL.StandardId = null;
                ShowErrors("err", "You have to select Standard");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }

    private void LoadSubjects()
    {
        try
        {
            ListItem li = new ListItem();

            ddlSubId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlSubId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _testQueBankBLL.LoadSubjects();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSubId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    private void LoadPatterns()
    {
        try
        {
            ListItem li = new ListItem();

            ddlPatternId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlPatternId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _testQueBankBLL.LoadPatterns();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[2].ToString();
                li.Value = dtr[0].ToString();
                ddlPatternId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    private void ShowErrors(string key, string value)
    {
        try
        {
            pnlErr.Visible = true;
            blErrs.Items.Add(new ListItem(value));

            if (key == "Name")
            {
                lblTestName.CssClass = "error";
                txtTestName.CssClass = "error";
            }

            if (key == "StandardId")
            {
                lblStandardTextListId.CssClass = "error";
                ddlStandardTextListId.CssClass = "error";
            }

            if (key == "SubId")
            {
                lblSubject.CssClass = "error";
                ddlSubId.CssClass = "error";
            }

            if (key == "PatternId")
            {
                lblPattern.CssClass = "error";
                ddlPatternId.CssClass = "error";
            }
            if (key == "Type")
            {
                lblType.CssClass = "error";
                rbTypeAuto.CssClass = "error";
                rbTypeManual.CssClass = "error";
            }

        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }

    private void HideErrors()
    {
        try
        {
            pnlErr.Visible = false;
            blErrs.Items.Clear();

            lblTestName.CssClass = "";
            txtTestName.CssClass = "";

            lblStandardTextListId.CssClass = "";
            ddlStandardTextListId.CssClass = "";

            lblSubject.CssClass = "";
            ddlSubId.CssClass = "";

            lblPattern.CssClass = "";
            ddlPatternId.CssClass = "";

            lblType.CssClass = "";
            rbTypeAuto.CssClass = "";
            rbTypeManual.CssClass = "";

        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
        }
    }
    private void LoadStandard()
    {
        try
        {
            ListItem li = new ListItem();

            ddlStandardTextListId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlStandardTextListId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _testQueBankBLL.LoadStandard();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlStandardTextListId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    //protected void ddlSubId_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (ddlSubId.SelectedIndex > 0)
    //        {
    //            _queBankBLL.SubId = ddlSubId.SelectedValue;
    //            LoadPatterns();
    //        }
    //        else
    //        {
    //            _queBankBLL.SubId = null;
    //            ShowErrors("err", "You have to select Subject");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ShowErrors("er", ex.Message);
    //    }
    //}
    protected void ddlPatternId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPatternId.SelectedIndex > 0)
            {
                _testQueBankBLL.PatternId = ddlPatternId.SelectedValue;
                LoadSubjects();
                ddlSubId.Focus();
            }
            else
            {
                _testQueBankBLL.PatternId = null;
                ShowErrors("err", "You have to select Pattern");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }



    private void LoadWebForm()
    {
        try
        {
            if (_testQueBankBLL.Name != null)
                txtTestName.Text = _testQueBankBLL.Name;

            if (_testQueBankBLL.Easy != null)
                txtEasy.Text = Convert.ToInt32(_testQueBankBLL.Easy).ToString();

            if (_testQueBankBLL.Medium != null)
                txtMedium.Text = Convert.ToInt32(_testQueBankBLL.Medium).ToString();

            if (_testQueBankBLL.Hard != null)
                txtHard.Text = Convert.ToInt32(_testQueBankBLL.Hard).ToString();
            LoadStandard();
            if (_testQueBankBLL.StandardId != null)
            {
                ddlStandardTextListId.SelectedValue = _testQueBankBLL.StandardId;
                ddlStandardTextListId_SelectedIndexChanged(null, null);
                ddlStandardTextListId.Enabled = false;
            }
            if (_testQueBankBLL.PatternId != null)
            {
                ddlPatternId.SelectedValue = _testQueBankBLL.PatternId;
                ddlPatternId_SelectedIndexChanged(null, null);
                ddlPatternId.Enabled = false;
            }
            if (_testQueBankBLL.SubId != null)
            {
                ddlSubId.SelectedValue = _testQueBankBLL.SubId;
                //ddlSubId_SelectedIndexChanged(null, null);
                ddlSubId.Enabled = false;
            }

            if (_testQueBankBLL.Easy != null && _testQueBankBLL.Medium != null && _testQueBankBLL.Hard != null)
                txtTotalQue.Text = Convert.ToInt32(_testQueBankBLL.Easy + _testQueBankBLL.Medium + _testQueBankBLL.Hard).ToString();

            if (_testQueBankBLL.dtDetaiils != null)
            {
                if (_testQueBankBLL.dtDetaiils.Rows.Count > 0)
                {
                    ShowDetails(_testQueBankBLL.dtDetaiils);
                }
            }
            if (_testQueBankBLL.LevelOfQue != null)
                ddlLevelOfQue.SelectedValue = _testQueBankBLL.LevelOfQue;
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _testQueBankBLL.Delete(Request.QueryString["TestId"]);
            Session["_testQueBankBLL"] = null;
            Response.Redirect("TestQueBanks.aspx");
        }
        catch (Exception ex)
        {

            ShowErrors("Error", ex.Message);
        }
    }

    protected void ShowDetails(DataTable Dt)
    {
        Literal ltCases = new Literal();
        StringBuilder strbuUser = new StringBuilder();
        if (Dt != null)
        {
            if (Dt.Rows.Count > 0)
            {
                var newDt = Dt.AsEnumerable()
               .GroupBy(r => r.Field<string>("Name"))
               .Select(g =>
               {
                   var row = Dt.NewRow();
                   row["Name"] = g.Key;
                   row["Easy"] = g.Count(x => x.Field<string>("LevelOfQue") == "Easy");
                   row["Medium"] = g.Count(x => x.Field<string>("LevelOfQue") == "Medium");
                   row["Hard"] = g.Count(x => x.Field<string>("LevelOfQue") == "Hard");
                   return row;
               }).CopyToDataTable();

                strbuUser.Append("<table style='width:100%;border-collapse:collapse;' class='table table-responsive' >");
                strbuUser.Append("<tr>");
                strbuUser.Append("<th style='border:1px solid gray;text-align:center;' >Subject</th>");
                strbuUser.Append("<th style='border:1px solid gray;text-align:center;' >Easy</th>");
                strbuUser.Append("<th style='border:1px solid gray;text-align:center;' >Medium</th>");
                strbuUser.Append("<th style='border:1px solid gray;text-align:center;' >Hard</th>");

                if (newDt != null)
                {
                    if (newDt.Rows.Count > 0)
                    {
                        strbuUser.Append("</tr>");
                        for (int i = 0; i < newDt.Rows.Count; i++)
                        {
                            strbuUser.Append("<tr>");
                            strbuUser.Append("<td style='border:1px solid gray;text-align:center;' >" + newDt.Rows[i]["Name"].ToString() + "</td>");
                            strbuUser.Append("<td style='border:1px solid gray;text-align:center;' >" + newDt.Rows[i]["Easy"].ToString() + "</td>");
                            strbuUser.Append("<td style='border:1px solid gray;text-align:center;' >" + newDt.Rows[i]["Medium"].ToString() + "</td>");
                            strbuUser.Append("<td style='border:1px solid gray;text-align:center;' >" + newDt.Rows[i]["Hard"].ToString() + "</td>");
                            strbuUser.Append("</tr>");
                        }
                    }
                }
                strbuUser.Append("</table>");
                ltCases.Text = strbuUser.ToString();
                plRpt.Controls.Clear();
                plRpt.Controls.Add(ltCases);
            }
        }
    }

    protected void Reset()
    {
        ddlStandardTextListId.SelectedIndex = 0;
        try
        {
            ddlPatternId.SelectedIndex = 0;
        }
        catch { }
        try
        {
            ddlSubId.SelectedIndex = 0;
        }
        catch { }
        try
        {
            ddlChapterId.SelectedIndex = 0;
        }
        catch { }
        chkPeriodNo.Items.Clear();
        txtEasy.Text = "";
        txtMedium.Text = "";
        txtHard.Text = "";
        txtTotalQue.Text = "";
        ddlLevelOfQue.SelectedIndex = 0;
        txtTestName.Enabled = true;
        ddlStandardTextListId.Enabled = true;
        ddlPatternId.Enabled = true;
        ddlSubId.Enabled = true;
    }

    protected void txtTestName_TextChanged(object sender, EventArgs e)
    {
        if (ddlSubId.Items.Count == 1)
        {
            _testQueBankBLL = new TestQueBankBLL();
            _queBankBLL = new QueBankBLL();
            Session["_testQueBankBLL"] = _testQueBankBLL;
            Session["_queBankBLL"] = _queBankBLL;
            _testQueBankBLL.dtDetaiils = CreateNewDataTable(_testQueBankBLL.dtDetaiils);
            Reset();
            ddlPatternId.Enabled = true;
            ddlStandardTextListId.Enabled = true;
        }
        ddlStandardTextListId.Focus();
    }
    private void LoadLevelOfQue()
    {
        try
        {
            ListItem li = new ListItem();

            ddlLevelOfQue.Items.Clear();

            li.Text = "<ALL>";
            li.Value = "0";
            ddlLevelOfQue.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _testQueBankBLL.LoadLevelOfQue();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[0].ToString();
                li.Value = dtr[0].ToString();
                ddlLevelOfQue.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    protected void btnAddToTest_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTestName.Text.Trim().Length > 0)
                _testQueBankBLL.Name = txtTestName.Text.Trim();
            else
                _testQueBankBLL.Name = null;

            ddlPatternId.Enabled = false;
            ddlStandardTextListId.Enabled = false;
            txtTestName.Enabled = false;

            if (_testQueBankBLL.dtDetaiils.Rows.Count > 0 && _testQueBankBLL.Name != null)
            {
                _testQueBankBLL.TestId = _queBankBLL.TestId;
                _testQueBankBLL.InsertTest();
                _queBankBLL.PasteQuestionFrom(_testQueBankBLL.dtDetaiils);

                ShowErrors("err", "Selected questions are Added To Test successfully.");
                gdvQues.DataSource = null;
                gdvQues.DataBind();
                _testQueBankBLL.IsNew = false;
                LoadSubjects();
                ShowDetails(_testQueBankBLL.dtDetaiils);
                _testQueBankBLL.dtDetaiils = CreateNewDataTable(_testQueBankBLL.dtDetaiils);
                if (ddlSubId.Items.Count == 1)
                {
                    //_testQueBankBLL = new TestQueBankBLL();
                    //_testQueBankBLL.dtDetaiils = new DataTable();
                    txtTestName.Enabled = true;
                    Reset();
                    _testQueBankBLL.IsNew = true;
                }
            }
            else
            {
                ShowErrors("err", "Please select atleast one question to Create Test or Test name cann not be blank.");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtTotalQue.Text.Trim().Length > 0)
                _testQueBankBLL.TotalQue = Convert.ToInt32(txtTotalQue.Text.Trim());
            else
                _testQueBankBLL.TotalQue = null;

            if (txtTestName.Text.Trim().Length > 0)
                _testQueBankBLL.Name = txtTestName.Text.Trim();
            else
                _testQueBankBLL.Name = null;

            ArrayList al = new ArrayList();

            DataTable dtNew = new DataTable();
            dtNew = CreateNewDataTable(dtNew);

            ddlPatternId.Enabled = false;
            ddlStandardTextListId.Enabled = false;
            txtTestName.Enabled = false;
            foreach (GridViewRow gvr in gdvQues.Rows)
            {
                CheckBox chkSelect = (CheckBox)gvr.FindControl("chkSelect");
                if (chkSelect.Checked)
                {
                    string SubId = gdvQues.DataKeys[gvr.RowIndex].Values[1].ToString();
                    string QueId = gdvQues.DataKeys[gvr.RowIndex].Values[0].ToString();
                    string LevelOfQue = gdvQues.DataKeys[gvr.RowIndex].Values[2].ToString();
                    string Name = ddlSubId.SelectedItem.Text.Split('_')[0];
                    int? TotalE = (txtEasy.Text.Length > 0 ? Convert.ToInt16(txtEasy.Text.Trim()) : 0);
                    int? TotalM = (txtMedium.Text.Length > 0 ? Convert.ToInt16(txtMedium.Text.Trim()) : 0);
                    int? TotalH = (txtHard.Text.Length > 0 ? Convert.ToInt16(txtHard.Text.Trim()) : 0);
                    dtNew.Rows.Add(SubId, QueId, LevelOfQue, Name, TotalE, TotalM, TotalH);
                }
            }
            if (dtNew.Rows.Count > 0 && _testQueBankBLL.Name != null && _testQueBankBLL.TotalQue != null)
            {
                if (dtNew.Rows.Count == _testQueBankBLL.TotalQue)
                {
                    if (_testQueBankBLL.dtDetaiils != null)
                    {
                        if (_testQueBankBLL.dtDetaiils.Rows.Count > 0)
                        {
                            DataRow[] foundRows = _testQueBankBLL.dtDetaiils.Select("SubId='" + ddlSubId.SelectedValue + "' and LevelOfQue='" + ddlLevelOfQue.SelectedValue + "'");
                            if (foundRows.Length > 0)
                            {
                                for (int i = 0; i < _testQueBankBLL.dtDetaiils.Rows.Count; i++)
                                {
                                    if (_testQueBankBLL.dtDetaiils.Rows[i]["SubId"].ToString() == ddlSubId.SelectedValue && _testQueBankBLL.dtDetaiils.Rows[i]["LevelOfQue"].ToString() == ddlLevelOfQue.SelectedValue)
                                    {
                                        _testQueBankBLL.dtDetaiils.Rows[i].Delete();
                                        if (i < 0)
                                            i = 0;
                                        else
                                            i--;
                                    }

                                }
                                _testQueBankBLL.dtDetaiils.Merge(dtNew);
                            }
                            else
                            {
                                _testQueBankBLL.dtDetaiils.Merge(dtNew);
                            }
                        }
                        else
                        {
                            _testQueBankBLL.dtDetaiils.Merge(dtNew);
                        }
                        ShowDetails(_testQueBankBLL.dtDetaiils);
                        gdvQues.DataSource = null;
                        gdvQues.DataBind();
                        lblRecordStatus.Text = "";
                        btnAddToTest.Visible = true;
                    }
                }
                else
                {
                    ShowErrors("err", "Question List must be equal to Total Questions.");
                }
            }
            else
            {
                ShowErrors("err", "Please select atleast one question to Create Test or Test name cann not be blank.");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }

    protected DataTable CreateNewDataTable(DataTable Dt)
    {
        Dt = new DataTable();
        DataColumn dtSubId = new DataColumn();
        dtSubId.ColumnName = "SubId";
        dtSubId.DataType = typeof(string);
        dtSubId.ReadOnly = false;
        Dt.Columns.Add(dtSubId);

        DataColumn dtQueId = new DataColumn();
        dtQueId.ColumnName = "QueId";
        dtQueId.DataType = typeof(string);
        dtQueId.ReadOnly = false;
        Dt.Columns.Add(dtQueId);

        DataColumn dtLevelOfQue = new DataColumn();
        dtLevelOfQue.ColumnName = "LevelOfQue";
        dtLevelOfQue.DataType = typeof(string);
        dtLevelOfQue.ReadOnly = false;
        Dt.Columns.Add(dtLevelOfQue);

        DataColumn dtAssignQty = new DataColumn();
        dtAssignQty.ColumnName = "Name";
        dtAssignQty.DataType = typeof(string);
        dtAssignQty.ReadOnly = false;
        Dt.Columns.Add(dtAssignQty);

        DataColumn dtEasy = new DataColumn();
        dtEasy.ColumnName = "Easy";
        dtEasy.DataType = typeof(string);
        dtEasy.ReadOnly = false;
        Dt.Columns.Add(dtEasy);

        DataColumn dtMedium = new DataColumn();
        dtMedium.ColumnName = "Medium";
        dtMedium.DataType = typeof(string);
        dtMedium.ReadOnly = false;
        Dt.Columns.Add(dtMedium);

        DataColumn dtHard = new DataColumn();
        dtHard.ColumnName = "Hard";
        dtHard.DataType = typeof(string);
        dtHard.ReadOnly = false;
        Dt.Columns.Add(dtHard);

        return Dt;
    }

    protected void rbTypeAuto_CheckedChanged(object sender, EventArgs e)
    {
        if (rbTypeAuto.Checked)
        {
            Label1.Visible = false;
            ddlLevelOfQue.Visible = false;
            lblLevelOfQue.Text = "Level of Question:*";
        }
        else
        {
            Label1.Visible = true;
            ddlLevelOfQue.Visible = true;
            lblLevelOfQue.Text = "Level of Question:";
            ddlLevelOfQue.Focus();
        }
    }
    private void LoadChapter()
    {
        try
        {
            ListItem li = new ListItem();

            ddlChapterId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlChapterId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _testQueBankBLL.LoadChapter();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlChapterId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    private void LoadPeriodNo()
    {
        try
        {
            ListItem li = new ListItem();

            chkPeriodNo.Items.Clear();

            li = null;

            DataTable dt = new DataTable();
            dt = _testQueBankBLL.LoadPeriodNo();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[0].ToString();
                li.Value = dtr[0].ToString();
                chkPeriodNo.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlSubId_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubId.SelectedIndex > 0)
        {
            _testQueBankBLL.SubId = ddlSubId.SelectedValue;
            LoadChapter();
            ShowDetails(_testQueBankBLL.dtDetaiils);
            ddlChapterId.Focus();
        }
        else
            _testQueBankBLL.SubId = null;
    }

    private bool Validate()
    {
        HideErrors();

        SortedList sl = _testQueBankBLL.Validate();

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
    protected void ddlChapterId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlChapterId.SelectedIndex > 0 && ddlSubId.SelectedIndex > 0)
            {
                _testQueBankBLL.ChapterId = ddlChapterId.SelectedValue;
                LoadPeriodNo();
            }
            else
            {
                _testQueBankBLL.ChapterId = null;
                chkPeriodNo.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
}