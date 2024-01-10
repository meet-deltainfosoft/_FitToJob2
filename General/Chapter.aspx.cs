using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class General_Chapter : System.Web.UI.Page
{
    ChapterBLL _ChapterBLL = new ChapterBLL();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["ChapterId"] == null)
                {

                    _ChapterBLL = new ChapterBLL();
                }
                else
                {
                    _ChapterBLL = new ChapterBLL(Request.QueryString["ChapterId"].ToString());
                }
                Session["_ChapterBLL"] = _ChapterBLL;
            }
            else
            {
                _ChapterBLL = (ChapterBLL)Session["_ChapterBLL"];
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
            if (!Page.IsPostBack)
            {
                LoadStandard();
                LoadStandardLn();
                //LoadChapterLns();
                //LoadSubjectsLns();
                if (Request.QueryString["ChapterId"] != null)
                {
                    lblTitle.Text = " - [Edit Mode]";
                    LoadWebForm();
                    btnDelete.Enabled = true;
                    btnOK.Visible = true;
                }
                else
                {
                    if (ddlStandard.SelectedIndex > 0 && ddlSubs.SelectedIndex > 0)
                    {
                        txtSrNo.Text = _ChapterBLL.SrNo;
                    }

                }
            }
            else
            {
                HideErrors();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        lblStandard.CssClass = "";
        ddlStandard.CssClass = "";

        lblSubs.CssClass = "";
        ddlSubs.CssClass = "";

    }

    private void ShowErrors(string key, string value)
    {
        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));

        if (key == "StandardTextListId")
        {
            lblStandard.CssClass = "error";
            ddlStandard.CssClass = "error";
        }

        if (key == "SubId")
        {
            lblSubs.CssClass = "error";
            ddlSubs.CssClass = "error";
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
            dt = _ChapterBLL.LoadStandard();

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
    private void LoadSubjects()
    {
        try
        {
            ListItem li = new ListItem();

            ddlSubs.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlSubs.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterBLL.LoadSubjects();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSubs.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
                _ChapterBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _ChapterBLL.StandardTextListId = null;

            if (ddlSubs.SelectedIndex > 0)
                _ChapterBLL.SubId = ddlSubs.SelectedValue;
            else
                _ChapterBLL.SubId = null;

            if (txtSrNo.Text.Trim().Length > 0)
            {
                _ChapterBLL.SrNo = txtSrNo.Text.Trim();
                _ChapterBLL.PeriodNo = Convert.ToDecimal(txtSrNo.Text.Trim());
            }
            else
            {
                _ChapterBLL.SrNo = null;
                _ChapterBLL.PeriodNo = null;
            }

            if (txtChapterName.Text.Trim().Length > 0)
                _ChapterBLL.ChapterName = txtChapterName.Text.Trim();
            else
                _ChapterBLL.ChapterName = null;

            if (txtRemarks.Text.Trim().Length > 0)
                _ChapterBLL.Remarks = txtRemarks.Text.Trim();
            else
                _ChapterBLL.Remarks = null;

            //if (txtPeriodNo.Text.Trim().Length > 0)
            //{
            //    _ChapterBLL.PeriodNo = Convert.ToDecimal(txtPeriodNo.Text.Trim());
            //    _ChapterBLL.PeriodNo = Convert.ToDecimal(txtPeriodNo.Text.Trim());
            //}
            //else
            //{
            //    _ChapterBLL.PeriodNo = null;
            //}

            bool isValid = Validate();

            if (isValid == true)
            {
                _ChapterBLL.Save();

                if (Request.QueryString["ChapterId"] == null)
                {
                    Reset(false);
                    Session["_ChapterBLL"] = null;
                    Session["_ChapterBLL"] = new ChapterBLL();
                    _ChapterBLL = (ChapterBLL)Session["_ChapterBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_ChapterBLL"] = null;
                    Response.Redirect("Chapters.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Session["_ChapterBLL"] = null;

            if (Request.QueryString["ChapterId"] == null)
            {
                Response.Redirect("~/General/Default.aspx");
            }
            else
            {
                Response.Redirect("Chapters.aspx");
            }
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
            _ChapterBLL.Delete(Request.QueryString["ChapterId"]);
            Session["_ChapterBLL"] = null;
            Response.Redirect("Chapters.aspx");
        }
        catch (Exception ex)
        {

            ShowErrors("Error", ex.Message);
        }
    }

    private bool Validate()
    {
        try
        {
            HideErrors();

            SortedList sl = _ChapterBLL.Validate();

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
            ShowErrors("", ex.Message.ToString());
        }
    }

    private void Reset(bool FromOkAndAddClick)
    {
        try
        {
            if (FromOkAndAddClick == true)
            {
                txtSrNo.Text = "";
                txtChapterName.Text = "";
                txtRemarks.Text = "";
                //txtPeriodNo.Text = "";
                gdvChapterLn.DataSource = null;
                gdvChapterLn.DataBind();

                _ChapterBLL.StandardTextListId = ddlStandard.SelectedValue;
                ddlStandard_SelectedIndexChanged(null, null);

                LoadSubjects();
                ddlSubs.SelectedValue = _ChapterBLL.SubId;
                ddlSubs_SelectedIndexChanged(null, null);

            }
            else
            {

                ddlStandard.SelectedIndex = 0;
                ddlSubs.SelectedIndex = 0;
                txtSrNo.Text = "";
                txtChapterName.Text = "";
                txtRemarks.Text = "";
                //txtPeriodNo.Text = "";
                gdvChapterLn.DataSource = null;
                gdvChapterLn.DataBind();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }

    protected void ddlStandard_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
            {
                _ChapterBLL.StandardTextListId = ddlStandard.SelectedValue.ToString();
                LoadSubjects();
            }
            else
            {
                _ChapterBLL.StandardTextListId = null;
                ShowErrors("", "Please select standard to generate schedule");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadWebForm()
    {
        try
        {
            if (_ChapterBLL.StandardTextListId != null)
            {
                ddlStandard.SelectedValue = _ChapterBLL.StandardTextListId.ToString();
                LoadSubjects();
                //ddlStandard_SelectedIndexChanged(null, null);
                //ddlStandard.Enabled = false;
            }

            if (_ChapterBLL.SubId != null)
            {
                ddlSubs.SelectedValue = _ChapterBLL.SubId.ToString();
                //ddlSubs_SelectedIndexChanged(null, null);
            }

            if (_ChapterBLL.SrNo != null)
            {
                txtSrNo.Text = _ChapterBLL.SrNo;
            }

            if (_ChapterBLL.ChapterName != null)
                txtChapterName.Text = _ChapterBLL.ChapterName.ToString();

            if (_ChapterBLL.Remarks != null)
                txtRemarks.Text = _ChapterBLL.Remarks.ToString();

            //if (_ChapterBLL.PeriodNo != null)
            //    txtPeriodNo.Text = Convert.ToDecimal(_ChapterBLL.PeriodNo).ToString("#.##");

            gdvChapterLn.Enabled = true;
            gdvChapterLn.DataSource = _ChapterBLL.ChapterLnsBLL;
            gdvChapterLn.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void ddlSubs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSubs.SelectedIndex > 0)
            {
                _ChapterBLL.SubId = ddlSubs.SelectedValue.ToString();
                //LoadSubjects();
                if (_ChapterBLL.SrNo != null)
                {
                    txtSrNo.Text = _ChapterBLL.SrNo;
                }
            }
            else
            {
                _ChapterBLL.SubId = null;
                ShowErrors("", "Please select standard to generate schedule");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void btnOKAndAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
                _ChapterBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _ChapterBLL.StandardTextListId = null;

            if (ddlSubs.SelectedIndex > 0)
                _ChapterBLL.SubId = ddlSubs.SelectedValue;
            else
                _ChapterBLL.SubId = null;

            if (txtSrNo.Text.Trim().Length > 0)
            {
                _ChapterBLL.SrNo = txtSrNo.Text.Trim();
                _ChapterBLL.PeriodNo = Convert.ToDecimal(txtSrNo.Text.Trim());
            }
            else
            {
                _ChapterBLL.SrNo = null;
                _ChapterBLL.PeriodNo = null;
            }

            if (txtChapterName.Text.Trim().Length > 0)
                _ChapterBLL.ChapterName = txtChapterName.Text.Trim();
            else
                _ChapterBLL.ChapterName = null;

            if (txtRemarks.Text.Trim().Length > 0)
                _ChapterBLL.Remarks = txtRemarks.Text.Trim();
            else
                _ChapterBLL.Remarks = null;

            //if (txtPeriodNo.Text.Trim().Length > 0)
            //    _ChapterBLL.PeriodNo = Convert.ToDecimal(txtPeriodNo.Text.Trim());
            //else
            //    _ChapterBLL.PeriodNo = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _ChapterBLL.Save();

                if (Request.QueryString["ChapterId"] == null)
                {
                    Reset(true);
                    Session["_ChapterBLL"] = null;
                    Session["_ChapterBLL"] = new ChapterBLL();
                    _ChapterBLL = (ChapterBLL)Session["_ChapterBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_ChapterBLL"] = null;
                    Response.Redirect("Chapters.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    #region Chapter Lns
    protected void btnChapterAdd_Click(object sender, EventArgs e)
    {
        HideChapterLnErrors();
        ChapterLnBLL _ChapterLnBLL = new ChapterLnBLL();

        Session["_ChapterLnBLL"] = _ChapterLnBLL;

        gdvChapterLn.Enabled = false;

        txtLnNo.Text = (_ChapterBLL.ChapterLnsBLL.Count + 1).ToString(); ;
        //Clear Child Item Details
        ClearChapterLn();

        //pnlChapterLnErr.Visible = true;
        //pnlChaptertLn.Visible = true;

    }
    protected void btnSaveChapterLn_Click(object sender, EventArgs e)
    {
        HideChapterLnErrors();
        ChapterLnBLL _ChapterLnBLL = (ChapterLnBLL)Session["_ChapterLnBLL"];

        _ChapterLnBLL.LnNo = Convert.ToInt32(txtLnNo.Text);

        if (ddlStandardLn.SelectedIndex > 0)
        {
            _ChapterLnBLL.StandardTextListId = ddlStandardLn.SelectedValue;
            _ChapterLnBLL.StandardName = ddlStandardLn.SelectedItem.ToString();
        }

        if (ddlSubjectLn.SelectedIndex > 0)
        {
            _ChapterLnBLL.SubId = ddlSubjectLn.SelectedValue;
            _ChapterLnBLL.SubjectName = ddlSubjectLn.SelectedItem.ToString();
        }

        if (ddlChapterLn.SelectedIndex > 0)
        {
            _ChapterLnBLL.ChapterTextListId = ddlChapterLn.SelectedValue;
            _ChapterLnBLL.ChapterName = ddlChapterLn.SelectedItem.ToString();
        }

        if (ddlPeriodNo.SelectedIndex > 0)
            _ChapterLnBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);
        else
            _ChapterLnBLL.PeriodNo = null;

        if (ddlChapterVideo.SelectedIndex > 0)
        {
            _ChapterLnBLL.ChapterVideoId = ddlChapterVideo.SelectedValue.ToString();
            _ChapterLnBLL.ChapterVideoName = ddlChapterVideo.SelectedItem.Text.ToString();
        }
        else
        {
            _ChapterLnBLL.ChapterVideoId = null;
            _ChapterLnBLL.ChapterVideoName = null;
        }

        bool isInquiryContactLnValid = ValidateChapterLn(_ChapterLnBLL);
        //bool isInquiryContactLnValid = true;

        if (isInquiryContactLnValid == true)
        {
            if (gdvChapterLn.SelectedIndex < 0)//New
            {
                _ChapterBLL.ChapterLnsBLL.Add(_ChapterLnBLL);
            }
            else//Edit
            {
                _ChapterLnBLL.IsDirty = true;
                _ChapterBLL.ChapterLnsBLL[gdvChapterLn.SelectedIndex] = _ChapterLnBLL;
                gdvChapterLn.SelectedIndex = -1;
            }

            Session["_ChapterBLL"] = _ChapterBLL;

            //Bind to Data to GridView
            gdvChapterLn.Enabled = true;
            gdvChapterLn.DataSource = _ChapterBLL.ChapterLnsBLL;
            gdvChapterLn.DataBind();

            btnChapterAdd.Enabled = true;
            pnlChaptertLn.Visible = false;

        }

    }
    protected void btnCloseChapterLn_Click(object sender, EventArgs e)
    {
        HideChapterLnErrors();
        gdvChapterLn.Enabled = true;
        if (gdvChapterLn.SelectedIndex >= 0)
        {
            gdvChapterLn.SelectedIndex = -1;
        }

        ClearChapterLn();
        pnlChaptertLn.Visible = false;
    }
    protected void gdvChapterLn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            HideChapterLnErrors();
            ChapterLnBLL ChapterLnBLL = _ChapterBLL.ChapterLnsBLL[e.RowIndex];

            _ChapterBLL.ChapterLnsBLL.Remove(e.RowIndex);

            Session["_ChapterBLL"] = _ChapterBLL;

            //ChapterLnBLL.LnNo = (_ChapterBLL.ChapterLnsBLL.Count);

            gdvChapterLn.DataSource = _ChapterBLL.ChapterLnsBLL;
            gdvChapterLn.DataBind();
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }

    }
    protected void gdvChapterLn_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideChapterLnErrors();
        gdvChapterLn.Enabled = false;

        //btnChapterAdd.Enabled = false;
        //pnlChaptertLn.Visible = true;

        //Get Inquiry Line BLL object
        ChapterLnBLL _ChapterLnBLL = _ChapterBLL.ChapterLnsBLL[gdvChapterLn.SelectedIndex];
        Session["_ChapterLnBLL"] = _ChapterLnBLL;

        //Set Item details to Controls for edit
        SetContactInquiry(_ChapterLnBLL);

    }
    private void LoadStandardLn()
    {
        try
        {
            ListItem li = new ListItem();

            ddlStandardLn.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlStandardLn.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterBLL.LoadStandard();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlStandardLn.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void SetContactInquiry(ChapterLnBLL ChapterLnBLL)
    {
        try
        {
            txtLnNo.Text = ChapterLnBLL.LnNo.ToString();
            LoadSubjectsLns();
            LoadChapterLns();

            if (ChapterLnBLL.StandardTextListId != null)
                ddlStandardLn.SelectedValue = ChapterLnBLL.StandardTextListId;

            if (ChapterLnBLL.SubId != null)
                ddlSubjectLn.SelectedValue = ChapterLnBLL.SubId;

            if (ChapterLnBLL.ChapterTextListId != null)
                ddlChapterLn.SelectedValue = ChapterLnBLL.ChapterTextListId;

            ddlChapterLn_SelectedIndexChanged(null, null);

            if (ChapterLnBLL.PeriodNo != null)
                ddlPeriodNo.SelectedValue = Convert.ToDecimal(ChapterLnBLL.PeriodNo).ToString("#.##");

            if (ChapterLnBLL.ChapterVideoId != null)
                ddlChapterVideo.SelectedValue = (ChapterLnBLL.ChapterVideoId).ToString();
        }
        catch
        {

        }
    }
    protected void ddlStandardLn_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideChapterLnErrors();
        ChapterLnBLL _ChapterLnBLL = (ChapterLnBLL)Session["_ChapterLnBLL"];
        try
        {
            if (ddlStandardLn.SelectedIndex > 0)
            {
                _ChapterLnBLL.StandardTextListId = ddlStandardLn.SelectedValue.ToString();
                LoadSubjectsLns();
            }
            else
            {
                _ChapterBLL.StandardTextListId = null;
                ShowErrors("", "Please select standard to Subjects");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }

    }
    private void LoadSubjectsLns()
    {
        ChapterLnBLL _ChapterLnBLL = (ChapterLnBLL)Session["_ChapterLnBLL"];
        try
        {
            ListItem li = new ListItem();

            ddlSubjectLn.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlSubjectLn.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterLnBLL.LoadSubjectsLn();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSubjectLn.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadSubjectsLnsClear()
    {
        try
        {
            ListItem li = new ListItem();

            ddlSubjectLn.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlSubjectLn.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterBLL.LoadSubjectsLnClear();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlSubjectLn.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadChapterLns()
    {
        ChapterLnBLL _ChapterLnBLL = (ChapterLnBLL)Session["_ChapterLnBLL"];
        try
        {
            ListItem li = new ListItem();

            ddlChapterLn.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlChapterLn.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterLnBLL.LoadChapterLn();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlChapterLn.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadPeriodNoLn()
    {
        try
        {
            ListItem li = new ListItem();

            ddlPeriodNo.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlPeriodNo.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterBLL.LoadPeriodNo(ddlChapterLn.SelectedValue, ddlSubjectLn.SelectedValue);

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[0].ToString();
                li.Value = dtr[0].ToString();
                ddlPeriodNo.Items.Add(li);

                li = null;
            }

            if (ddlPeriodNo.Items.Count == 2)
            {
                ddlPeriodNo.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadChapterVideo()
    {
        try
        {
            ListItem li = new ListItem();

            ddlChapterVideo.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlChapterVideo.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterBLL.LoadChapterVideo(ddlChapterLn.SelectedValue, ddlSubjectLn.SelectedValue);

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlChapterVideo.Items.Add(li);

                li = null;
            }

            if (ddlChapterVideo.Items.Count == 2)
            {
                ddlChapterVideo.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private ChapterLnBLL GetChapterLnBLL()
    {
        //Here we are checking if the ChapterLnBLL instance is stored in session or not, if it stored in seesion then we need to
        //Retrive it otherwise we need to create and Store it in session. 
        //Here we not using Add & save method, after clicking on save button we are creating instance and saving the record in collection

        ChapterLnBLL _ChapterLnBLL;

        if (Session["_ChapterLnBLL"] == null)
        {
            _ChapterLnBLL = new ChapterLnBLL();
            _ChapterLnBLL.LnNo = _ChapterBLL.ChapterLnsBLL.Count + 1;
            Session["_ChapterLnBLL"] = _ChapterLnBLL;
        }
        else
        {
            _ChapterLnBLL = (ChapterLnBLL)Session["_ChapterLnBLL"];
            //_ChapterLnBLL.LnNo = _ChapterBLL.ChapterLnsBLL.Count + 1;
        }

        return _ChapterLnBLL;
    }
    private void ClearChapterLn()
    {
        LoadSubjectsLnsClear();
        LoadChapterLns();
        ddlChapterLn.SelectedIndex = 0;
        ddlStandardLn.SelectedIndex = 0;
        ddlSubjectLn.SelectedIndex = 0;
        ddlChapterLn_SelectedIndexChanged(null, null);

        try
        {
            ddlChapterVideo.SelectedIndex = 0;
        }
        catch
        {
        }
    }

    protected void ddlSubjectLn_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideChapterLnErrors();
        ChapterLnBLL _ChapterLnBLL = (ChapterLnBLL)Session["_ChapterLnBLL"];
        try
        {
            if (ddlStandardLn.SelectedIndex > 0 && ddlSubjectLn.SelectedIndex > 0)
            {
                _ChapterLnBLL.StandardTextListId = ddlStandardLn.SelectedValue.ToString();
                _ChapterLnBLL.SubId = ddlSubjectLn.SelectedValue;
                LoadChapterLns();
            }
            else
            {
                _ChapterBLL.StandardTextListId = null;
                ShowErrors("", "Please select standard to Subjects");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void btnSaveAdd_Click(object sender, EventArgs e)
    {
        try
        {
            HideErrors();

            btnSaveChapterLn_Click(btnSaveChapterLn, new EventArgs());
            if (pnlChapterLnErr.Visible == false)
            {
                btnChapterAdd_Click(btnChapterAdd, new EventArgs());
            }
            else
            {
                ShowChapterLnErrors(null, null);
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    private bool ValidateChapterLn(ChapterLnBLL ChapterLnBLL)
    {
        HideChapterLnErrors();

        SortedList sl = ChapterLnBLL.Validate(_ChapterBLL.ChapterLnsBLL);

        if (sl.Count > 0)
        {
            for (int i = 0; i < sl.Count; i++)
            {
                string key = (string)sl.GetKey(i);
                string value = (string)sl[key];

                ShowChapterLnErrors(key, value);
            }
        }

        return (sl.Count == 0) ? true : false;

    }
    private void HideChapterLnErrors()
    {
        blChapterLnErrs.Items.Clear();
        pnlChapterLnErr.Visible = false;

        ddlSubjectLn.CssClass = "";
        ddlChapterLn.CssClass = "";
        ddlStandardLn.CssClass = "";
        lblSubjectLn.CssClass = "";
        lblChapter.CssClass = "";
        lblStrandardLn.CssClass = "";
    }
    private void ShowChapterLnErrors(string key, string value)
    {
        pnlChapterLnErr.Visible = true;
        blChapterLnErrs.Items.Add(new ListItem(value));

        //Part No
        if (key == "Duplicate")
        {
            lblStrandardLn.CssClass = "error";
            ddlStandardLn.CssClass = "error";

            lblSubjectLn.CssClass = "error";
            ddlSubjectLn.CssClass = "error";

            lblChapter.CssClass = "error";
            ddlChapterLn.CssClass = "error";
        }
    }
    protected void ddlChapterLn_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlChapterLn.SelectedIndex > 0 && ddlSubjectLn.SelectedIndex > 0)
            {
                LoadPeriodNoLn();
                LoadChapterVideo();
            }
            else
            {
                ddlPeriodNo.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion
}