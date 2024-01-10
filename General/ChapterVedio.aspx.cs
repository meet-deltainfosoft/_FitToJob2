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

public partial class General_ChapterVedio : System.Web.UI.Page
{
    ChapterVedioBLL _ChapterVedioBLL = new ChapterVedioBLL();


    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["ChapterVedioId"] == null)
                {
                    _ChapterVedioBLL = new ChapterVedioBLL();
                }
                else
                {
                    _ChapterVedioBLL = new ChapterVedioBLL(Request.QueryString["ChapterVedioId"].ToString());
                }
                Session["_ChapterVedioBLL"] = _ChapterVedioBLL;
            }
            else
            {
                _ChapterVedioBLL = (ChapterVedioBLL)Session["_ChapterVedioBLL"];
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
                if (Request.QueryString["ChapterVedioId"] != null)
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
                        txtSrNo.Text = _ChapterVedioBLL.SrNo;
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

        lblChapter.CssClass = "";
        ddlChapter.CssClass = "";

        lblSrNo.CssClass = "";
        txtSrNo.CssClass = "";

        lblFileLink.CssClass = "";
        txtFileLink.CssClass = "";

        lblFileName.CssClass = "";
        txtFileName.CssClass = "";

        lblPeriodNo.CssClass = "";
        ddlPeriodNo.CssClass = "";
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

        if (key == "ChapterId")
        {
            lblChapter.CssClass = "error";
            ddlChapter.CssClass = "error";
        }

        if (key == "PeriodNo")
        {
            lblPeriodNo.CssClass = "error";
            ddlPeriodNo.CssClass = "error";
        }

        if (key == "SrNo")
        {
            lblSrNo.CssClass = "error";
            txtSrNo.CssClass = "error";
        }

        if (key == "VedioLink")
        {
            lblFileLink.CssClass = "error";
            txtFileLink.CssClass = "error";
        }

        if (key == "VedioFileName")
        {
            lblFileName.CssClass = "error";
            txtFileName.CssClass = "error";
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
            dt = _ChapterVedioBLL.LoadStandard();

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
            dt = _ChapterVedioBLL.LoadSubjects();

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

    private void LoadChapter()
    {
        try
        {
            ListItem li = new ListItem();

            ddlChapter.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlChapter.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterVedioBLL.LoadChapter();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlChapter.Items.Add(li);

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

            ddlPeriodNo.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlPeriodNo.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterVedioBLL.LoadPeriodNo();

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
                ddlPeriodNo_SelectedIndexChanged(null, null);
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
                _ChapterVedioBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _ChapterVedioBLL.StandardTextListId = null;

            if (ddlSubs.SelectedIndex > 0)
                _ChapterVedioBLL.SubId = ddlSubs.SelectedValue;
            else
                _ChapterVedioBLL.SubId = null;

            if (ddlChapter.SelectedIndex > 0)
                _ChapterVedioBLL.ChapterId = ddlChapter.SelectedValue;
            else
                _ChapterVedioBLL.ChapterId = null;

            if (ddlPeriodNo.SelectedIndex > 0)
                _ChapterVedioBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);
            else
                _ChapterVedioBLL.PeriodNo = null;

            if (txtSrNo.Text.Trim().Length > 0)
                _ChapterVedioBLL.SrNo = txtSrNo.Text.Trim();
            else
                _ChapterVedioBLL.SrNo = null;

            if (txtRemarks.Text.Trim().Length > 0)
                _ChapterVedioBLL.Remarks = txtRemarks.Text.Trim();
            else
                _ChapterVedioBLL.Remarks = null;

            if (txtFileName.Text.Trim().Length > 0)
                _ChapterVedioBLL.VedioFileName = txtFileName.Text.Trim();
            else
                _ChapterVedioBLL.VedioFileName = null;

            if (txtFileLink.Text.Trim().Length > 0)
                _ChapterVedioBLL.VedioLink = txtFileLink.Text.Trim();
            else
                _ChapterVedioBLL.VedioLink = null;

            _ChapterVedioBLL.IsDisabled = chkIsDisabled.Checked;

            //if (_ChapterVedioBLL.ChapterIdMain == null)
            //    if (hfChapterId.Value.Trim().Length > 0)
            //        _ChapterVedioBLL.ChapterIdMain = hfChapterId.Value.Trim();
            //    else
            //        _ChapterVedioBLL.ChapterIdMain = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _ChapterVedioBLL.Save();

                if (Request.QueryString["ChapterVedioId"] == null)
                {
                    Reset(false);
                    Session["_ChapterVedioBLL"] = null;
                    Session["_ChapterVedioBLL"] = new ChapterVedioBLL();
                    _ChapterVedioBLL = (ChapterVedioBLL)Session["_ChapterVedioBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_ChapterPDFBLL"] = null;
                    Response.Redirect("ChapterVedios.aspx");
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
            Session["_ChapterPDFBLL"] = null;

            if (Request.QueryString["ChapterVedioId"] == null)
            {
                Response.Redirect("~/General/Default.aspx");
            }
            else
            {
                Response.Redirect("ChapterVedios.aspx");
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
            _ChapterVedioBLL.Delete(Request.QueryString["ChapterVedioId"]);
            Session["_ChapterVedioBLL"] = null;
            Response.Redirect("ChapterVedios.aspx");
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

            SortedList sl = _ChapterVedioBLL.Validate();

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
                ddlChapter.SelectedIndex = 0;
                txtSrNo.Text = "";
                txtRemarks.Text = "";
                txtFileLink.Text = "";
                txtFileName.Text = "";

                _ChapterVedioBLL.StandardTextListId = ddlStandard.SelectedValue;
                ddlStandard_SelectedIndexChanged(ddlStandard, null);

                ddlSubs.SelectedValue = _ChapterVedioBLL.SubId;
                _ChapterVedioBLL.SubId = ddlSubs.SelectedValue;
                ddlSubs_SelectedIndexChanged(ddlSubs, null);

                ddlChapter.SelectedValue = _ChapterVedioBLL.ChapterId;
                _ChapterVedioBLL.ChapterId = ddlChapter.SelectedValue;
                ddlChapter_SelectedIndexChanged(ddlChapter, null);


                ddlPeriodNo.SelectedValue = Convert.ToDecimal(_ChapterVedioBLL.PeriodNo).ToString("#.##");
                _ChapterVedioBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);

                //hfChapterId.Value = _ChapterVedioBLL.ChapterIdMain;

                gdvChapterLn.DataSource = null;
                gdvChapterLn.DataBind();
            }
            else
            {
                ddlStandard.SelectedIndex = 0;
                ddlSubs.SelectedIndex = 0;
                ddlChapter.SelectedIndex = 0;
                ddlPeriodNo.Items.Clear();
                txtSrNo.Text = "";
                txtRemarks.Text = "";
                txtFileLink.Text = "";
                txtFileName.Text = "";

                gdvChapterLn.DataSource = null;
                gdvChapterLn.DataBind();
            }
            chkIsDisabled.Checked = false;
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
                _ChapterVedioBLL.StandardTextListId = ddlStandard.SelectedValue.ToString();
                LoadSubjects();
            }
            else
            {
                _ChapterVedioBLL.StandardTextListId = null;

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
            if (_ChapterVedioBLL.SrNo != null)
            {
                txtSrNo.Text = _ChapterVedioBLL.SrNo;
            }

            if (_ChapterVedioBLL.StandardTextListId != null)
            {
                ddlStandard.SelectedValue = _ChapterVedioBLL.StandardTextListId.ToString();
                LoadSubjects();
                //ddlStandard_SelectedIndexChanged(null, null);
                //ddlStandard.Enabled = false;
            }
            if (_ChapterVedioBLL.SubId != null)
            {
                ddlSubs.SelectedValue = _ChapterVedioBLL.SubId.ToString();
                LoadChapter();
                //ddlSubs_SelectedIndexChanged(null, null);
            }
            if (_ChapterVedioBLL.ChapterId != null)
            {
                ddlChapter.SelectedValue = _ChapterVedioBLL.ChapterId.ToString();
                //LoadChapter();
                ddlChapter_SelectedIndexChanged(null, null);
            }

            if (_ChapterVedioBLL.PeriodNo != null)
            {
                ddlPeriodNo.SelectedValue = Convert.ToDecimal(_ChapterVedioBLL.PeriodNo).ToString("#.##");
                //LoadChapter();
                ddlPeriodNo_SelectedIndexChanged(null, null);
            }

            if (_ChapterVedioBLL.Remarks != null)
                txtRemarks.Text = _ChapterVedioBLL.Remarks.ToString();
            else
                txtRemarks.Text = null;

            if (_ChapterVedioBLL.VedioFileName != null)
                txtFileName.Text = _ChapterVedioBLL.VedioFileName.ToString();
            else
                txtFileName.Text = null;

            if (_ChapterVedioBLL.VedioLink != null)
                txtFileLink.Text = _ChapterVedioBLL.VedioLink.ToString();
            else
                txtFileLink.Text = null;

            chkIsDisabled.Checked = _ChapterVedioBLL.IsDisabled;

            gdvChapterLn.Enabled = true;
            gdvChapterLn.DataSource = _ChapterVedioBLL.ChapterLnsBLL;
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
                _ChapterVedioBLL.SubId = ddlSubs.SelectedValue.ToString();
                LoadChapter();

            }
            else
            {
                _ChapterVedioBLL.SubId = null;
                //ShowErrors("", "Please select standard to generate schedule");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlChapter_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlChapter.SelectedIndex > 0)
            {
                _ChapterVedioBLL.ChapterId = ddlChapter.SelectedValue.ToString();

                if (ddlSubs.SelectedIndex > 0)
                    _ChapterVedioBLL.SubId = ddlSubs.SelectedValue;
                else
                    _ChapterVedioBLL.SubId = null;

                LoadPeriodNo();

                if (_ChapterVedioBLL.SrNo != null)
                {
                    if (Request.QueryString["ChapterVedioId"] == null)
                        txtSrNo.Text = _ChapterVedioBLL.SrNo;
                }
            }
            else
            {
                _ChapterVedioBLL.ChapterId = null;
                //ShowErrors("", "Please select standard to generate schedule");
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
                _ChapterVedioBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _ChapterVedioBLL.StandardTextListId = null;

            if (ddlSubs.SelectedIndex > 0)
                _ChapterVedioBLL.SubId = ddlSubs.SelectedValue;
            else
                _ChapterVedioBLL.SubId = null;

            if (ddlChapter.SelectedIndex > 0)
                _ChapterVedioBLL.ChapterId = ddlChapter.SelectedValue;
            else
                _ChapterVedioBLL.ChapterId = null;

            if (ddlPeriodNo.SelectedIndex > 0)
                _ChapterVedioBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);
            else
                _ChapterVedioBLL.PeriodNo = null;

            if (txtSrNo.Text.Trim().Length > 0)
                _ChapterVedioBLL.SrNo = txtSrNo.Text.Trim();
            else
                _ChapterVedioBLL.SrNo = null;

            if (txtRemarks.Text.Trim().Length > 0)
                _ChapterVedioBLL.Remarks = txtRemarks.Text.Trim();
            else
                _ChapterVedioBLL.Remarks = null;

            if (txtFileName.Text.Trim().Length > 0)
                _ChapterVedioBLL.VedioFileName = txtFileName.Text.Trim();
            else
                _ChapterVedioBLL.VedioFileName = null;

            if (txtFileLink.Text.Trim().Length > 0)
                _ChapterVedioBLL.VedioLink = txtFileLink.Text.Trim();
            else
                _ChapterVedioBLL.VedioLink = null;

            //if (_ChapterVedioBLL.ChapterIdMain == null)
            //    if (hfChapterId.Value.Trim().Length > 0)
            //        _ChapterVedioBLL.ChapterIdMain = hfChapterId.Value.Trim();
            //    else
            //        _ChapterVedioBLL.ChapterIdMain = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _ChapterVedioBLL.Save();

                if (Request.QueryString["ChapterVedioId"] == null)
                {
                    Reset(true);
                    Session["_ChapterVedioBLL"] = null;
                    Session["_ChapterVedioBLL"] = new ChapterVedioBLL();
                    _ChapterVedioBLL = (ChapterVedioBLL)Session["_ChapterVedioBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_ChapterPDFBLL"] = null;
                    Response.Redirect("ChapterVedios.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void ddlPeriodNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPeriodNo.SelectedIndex > 0 && ddlChapter.SelectedIndex > 0)
            {
                _ChapterVedioBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue.ToString());

                _ChapterVedioBLL.ChapterId = ddlChapter.SelectedValue;

                //string ChapterId = "";

                //if (_ChapterVedioBLL.PeriodNo != null)
                //{
                //    ChapterId = _ChapterVedioBLL.GetChapterId(_ChapterVedioBLL.ChapterId, Convert.ToDecimal(_ChapterVedioBLL.PeriodNo).ToString("#.##"), ddlChapter.SelectedValue.ToString());

                //    _ChapterVedioBLL.ChapterIdMain = ChapterId;
                //}
                //else
                //{
                //    ChapterId = "";
                //    _ChapterVedioBLL.ChapterIdMain = null;
                //}

                if (_ChapterVedioBLL.SrNo != null)
                {
                    if (Request.QueryString["ChapterVedioId"] == null)
                        txtSrNo.Text = _ChapterVedioBLL.SrNo;
                }
            }
            else
            {
                _ChapterVedioBLL.PeriodNo = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }

    #region Chapter Lns
    protected void btnChapterAdd_Click(object sender, EventArgs e)
    {
        HideChapterLnErrors();
        ChapterLnBLL _ChapterLnBLL = new ChapterLnBLL();

        Session["_ChapterLnBLL"] = _ChapterLnBLL;

        gdvChapterLn.Enabled = false;

        txtLnNo.Text = (_ChapterVedioBLL.ChapterLnsBLL.Count + 1).ToString(); ;
        //Clear Child Item Details
        ClearChapterLn();

        //pnlChapterLnErr.Visible = true;
        pnlChaptertLn.Visible = true;

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

        if (ddlPeriodNoLn.SelectedIndex > 0)
            _ChapterLnBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNoLn.SelectedValue);
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
                _ChapterVedioBLL.ChapterLnsBLL.Add(_ChapterLnBLL);
            }
            else//Edit
            {
                _ChapterLnBLL.IsDirty = true;
                _ChapterVedioBLL.ChapterLnsBLL[gdvChapterLn.SelectedIndex] = _ChapterLnBLL;
                gdvChapterLn.SelectedIndex = -1;
            }

            Session["_ChapterVedioBLL"] = _ChapterVedioBLL;

            //Bind to Data to GridView
            gdvChapterLn.Enabled = true;
            gdvChapterLn.DataSource = _ChapterVedioBLL.ChapterLnsBLL;
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
            ChapterLnBLL ChapterLnBLL = _ChapterVedioBLL.ChapterLnsBLL[e.RowIndex];

            _ChapterVedioBLL.ChapterLnsBLL.Remove(e.RowIndex);

            Session["_ChapterVedioBLL"] = _ChapterVedioBLL;

            //ChapterLnBLL.LnNo = (_ChapterVedioBLL.ChapterLnsBLL.Count);

            gdvChapterLn.DataSource = _ChapterVedioBLL.ChapterLnsBLL;
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
        pnlChaptertLn.Visible = true;

        //Get Inquiry Line BLL object
        ChapterLnBLL _ChapterLnBLL = _ChapterVedioBLL.ChapterLnsBLL[gdvChapterLn.SelectedIndex];
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
            dt = _ChapterVedioBLL.LoadStandard();

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
                ddlPeriodNoLn.SelectedValue = Convert.ToDecimal(ChapterLnBLL.PeriodNo).ToString("#.##");

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
                _ChapterVedioBLL.StandardTextListId = null;
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
            dt = _ChapterVedioBLL.LoadSubjectsLnClear();

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

            ddlPeriodNoLn.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlPeriodNoLn.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterVedioBLL.LoadPeriodNo(ddlChapterLn.SelectedValue, ddlSubjectLn.SelectedValue);

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[0].ToString();
                li.Value = dtr[0].ToString();
                ddlPeriodNoLn.Items.Add(li);

                li = null;
            }

            if (ddlPeriodNoLn.Items.Count == 2)
            {
                ddlPeriodNoLn.SelectedIndex = 1;
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
            dt = _ChapterVedioBLL.LoadChapterVideo(ddlChapterLn.SelectedValue, ddlSubjectLn.SelectedValue);

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
            _ChapterLnBLL.LnNo = _ChapterVedioBLL.ChapterLnsBLL.Count + 1;
            Session["_ChapterLnBLL"] = _ChapterLnBLL;
        }
        else
        {
            _ChapterLnBLL = (ChapterLnBLL)Session["_ChapterLnBLL"];
            //_ChapterLnBLL.LnNo = _ChapterVedioBLL.ChapterLnsBLL.Count + 1;
        }

        return _ChapterLnBLL;
    }
    private void ClearChapterLn()
    {
        LoadSubjectsLnsClear();
        LoadChapterLns();
        try
        {
            ddlChapterLn.SelectedIndex = 0;
        }
        catch
        {

        }

        try
        {
            ddlStandardLn.SelectedIndex = 0;
        }
        catch
        {

        }
        try
        {
            ddlSubjectLn.SelectedIndex = 0;
        }
        catch
        {

        }
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
                _ChapterVedioBLL.StandardTextListId = null;
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

        SortedList sl = ChapterLnBLL.Validate(_ChapterVedioBLL.ChapterLnsBLL);

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
                ddlPeriodNoLn.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion
}