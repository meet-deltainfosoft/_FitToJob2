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


public partial class General_ChapterLink : System.Web.UI.Page
{
    ChapterLinkBLL _ChapterLinkBLL = new ChapterLinkBLL();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["ChapterLinkId"] == null)
                {
                    _ChapterLinkBLL = new ChapterLinkBLL();
                }
                else
                {
                    _ChapterLinkBLL = new ChapterLinkBLL(Request.QueryString["ChapterLinkId"].ToString());
                }
                Session["_ChapterLinkBLL"] = _ChapterLinkBLL;
            }
            else
            {
                _ChapterLinkBLL = (ChapterLinkBLL)Session["_ChapterLinkBLL"];
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
                if (Request.QueryString["ChapterLinkId"] != null)
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
                        txtSrNo.Text = _ChapterLinkBLL.SrNo;
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

        if (key == "PeriodNo")
        {
            lblPeriodNo.CssClass = "error";
            ddlPeriodNo.CssClass = "error";
        }

        if (key == "ChapterId")
        {
            lblChapter.CssClass = "error";
            ddlChapter.CssClass = "error";
        }

        if (key == "SrNo")
        {
            lblSrNo.CssClass = "error";
            txtSrNo.CssClass = "error";
        }

        if (key == "Link")
        {
            lblFileLink.CssClass = "error";
            txtFileLink.CssClass = "error";
        }

        if (key == "LinkName")
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
            dt = _ChapterLinkBLL.LoadStandard();

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
            dt = _ChapterLinkBLL.LoadSubjects();

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
            dt = _ChapterLinkBLL.LoadChapter();

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
            dt = _ChapterLinkBLL.LoadPeriodNo();

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
                _ChapterLinkBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _ChapterLinkBLL.StandardTextListId = null;

            if (ddlSubs.SelectedIndex > 0)
                _ChapterLinkBLL.SubId = ddlSubs.SelectedValue;
            else
                _ChapterLinkBLL.SubId = null;

            if (ddlChapter.SelectedIndex > 0)
                _ChapterLinkBLL.ChapterId = ddlChapter.SelectedValue;
            else
                _ChapterLinkBLL.ChapterId = null;

            if (ddlPeriodNo.SelectedIndex > 0)
                _ChapterLinkBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);
            else
                _ChapterLinkBLL.PeriodNo = null;

            if (txtSrNo.Text.Trim().Length > 0)
                _ChapterLinkBLL.SrNo = txtSrNo.Text.Trim();
            else
                _ChapterLinkBLL.SrNo = null;

            if (txtRemarks.Text.Trim().Length > 0)
                _ChapterLinkBLL.Remarks = txtRemarks.Text.Trim();
            else
                _ChapterLinkBLL.Remarks = null;

            if (txtFileName.Text.Trim().Length > 0)
                _ChapterLinkBLL.LinkName = txtFileName.Text.Trim();
            else
                _ChapterLinkBLL.LinkName = null;

            if (txtFileLink.Text.Trim().Length > 0)
                _ChapterLinkBLL.Link = txtFileLink.Text.Trim();
            else
                _ChapterLinkBLL.Link = null;

            if (ddlChapterVideoId.SelectedIndex > 0)
                _ChapterLinkBLL.ChapterVideoId = Convert.ToInt16(ddlChapterVideoId.SelectedValue.ToString());
            else
                _ChapterLinkBLL.ChapterVideoId = null;

            //if (_ChapterLinkBLL.ChapterIdMain == null)
            //    if (hfChapterId.Value.Trim().Length > 0)
            //        _ChapterLinkBLL.ChapterIdMain = hfChapterId.Value.Trim();
            //    else
            //        _ChapterLinkBLL.ChapterIdMain = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _ChapterLinkBLL.Save();

                if (Request.QueryString["ChapterLinkId"] == null)
                {
                    Reset(false);
                    Session["_ChapterLinkBLL"] = null;
                    Session["_ChapterLinkBLL"] = new ChapterLinkBLL();
                    _ChapterLinkBLL = (ChapterLinkBLL)Session["_ChapterLinkBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_ChapterLinkBLL"] = null;
                    Response.Redirect("ChapterLinks.aspx");
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
            Session["_ChapterLinkBLL"] = null;

            if (Request.QueryString["ChapterLinkId"] == null)
            {
                Response.Redirect("~/General/Default.aspx");
            }
            else
            {
                Response.Redirect("ChapterLinks.aspx");
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
            _ChapterLinkBLL.Delete(Request.QueryString["ChapterLinkId"]);
            Session["_ChapterLinkBLL"] = null;
            Response.Redirect("ChapterLinks.aspx");
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

            SortedList sl = _ChapterLinkBLL.Validate();

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
                txtRemarks.Text = "";
                txtFileLink.Text = "";
                txtFileName.Text = "";


                _ChapterLinkBLL.StandardTextListId = ddlStandard.SelectedValue;
                ddlStandard_SelectedIndexChanged(ddlStandard, null);

                ddlSubs.SelectedValue = _ChapterLinkBLL.SubId;
                _ChapterLinkBLL.SubId = ddlSubs.SelectedValue;
                ddlSubs_SelectedIndexChanged(ddlSubs, null);

                ddlChapter.SelectedValue = _ChapterLinkBLL.ChapterId;
                _ChapterLinkBLL.ChapterId = ddlChapter.SelectedValue;
                ddlChapter_SelectedIndexChanged(ddlChapter, null);


                ddlPeriodNo.SelectedValue = Convert.ToDecimal(_ChapterLinkBLL.PeriodNo).ToString("#.##");
                _ChapterLinkBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);


                //hfChapterId.Value = _ChapterLinkBLL.ChapterIdMain;
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
                ddlChapterVideoId.SelectedIndex = 0;
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
                _ChapterLinkBLL.StandardTextListId = ddlStandard.SelectedValue.ToString();
                LoadSubjects();
            }
            else
            {
                _ChapterLinkBLL.StandardTextListId = null;

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
            if (_ChapterLinkBLL.StandardTextListId != null)
            {
                ddlStandard.SelectedValue = _ChapterLinkBLL.StandardTextListId.ToString();
                LoadSubjects();
                //ddlStandard_SelectedIndexChanged(null, null);
                //ddlStandard.Enabled = false;
            }
            if (_ChapterLinkBLL.SubId != null)
            {
                ddlSubs.SelectedValue = _ChapterLinkBLL.SubId.ToString();
                LoadChapter();
                //ddlSubs_SelectedIndexChanged(null, null);
            }
            if (_ChapterLinkBLL.SrNo != null)
            {
                txtSrNo.Text = _ChapterLinkBLL.SrNo;
            }
            if (_ChapterLinkBLL.ChapterId != null)
            {
                ddlChapter.SelectedValue = _ChapterLinkBLL.ChapterId.ToString();
                //LoadChapter();
                ddlChapter_SelectedIndexChanged(null, null);
            }

            if (_ChapterLinkBLL.PeriodNo != null)
            {
                ddlPeriodNo.SelectedValue = Convert.ToDecimal(_ChapterLinkBLL.PeriodNo).ToString("#.##");
                //LoadChapter();
                ddlPeriodNo_SelectedIndexChanged(null, null);
            }
            if (_ChapterLinkBLL.Remarks != null)
                txtRemarks.Text = _ChapterLinkBLL.Remarks.ToString();
            else
                txtRemarks.Text = null;

            if (_ChapterLinkBLL.LinkName != null)
                txtFileName.Text = _ChapterLinkBLL.LinkName.ToString();
            else
                txtFileName.Text = null;

            if (_ChapterLinkBLL.Link != null)
                txtFileLink.Text = _ChapterLinkBLL.Link.ToString();
            else
                txtFileLink.Text = null;

            if (_ChapterLinkBLL.ChapterVideoId != null)
                ddlChapterVideoId.SelectedValue = _ChapterLinkBLL.ChapterVideoId.ToString();

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
                _ChapterLinkBLL.SubId = ddlSubs.SelectedValue.ToString();
                LoadChapter();

            }
            else
            {
                _ChapterLinkBLL.SubId = null;
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
            if (ddlChapter.SelectedIndex > 0 && ddlSubs.SelectedIndex > 0)
            {
                _ChapterLinkBLL.ChapterId = ddlChapter.SelectedValue.ToString();
                _ChapterLinkBLL.SubId = ddlSubs.SelectedValue.ToString();

                LoadPeriodNo();

                if (_ChapterLinkBLL.SrNo != null)
                {
                    if (Request.QueryString["ChapterLinkId"] == null)
                        txtSrNo.Text = _ChapterLinkBLL.SrNo;
                }
            }
            else
            {
                _ChapterLinkBLL.ChapterId = null;
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
                _ChapterLinkBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _ChapterLinkBLL.StandardTextListId = null;

            if (ddlSubs.SelectedIndex > 0)
                _ChapterLinkBLL.SubId = ddlSubs.SelectedValue;
            else
                _ChapterLinkBLL.SubId = null;

            if (ddlChapter.SelectedIndex > 0)
                _ChapterLinkBLL.ChapterId = ddlChapter.SelectedValue;
            else
                _ChapterLinkBLL.ChapterId = null;

            if (ddlPeriodNo.SelectedIndex > 0)
                _ChapterLinkBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);
            else
                _ChapterLinkBLL.PeriodNo = null;

            if (txtSrNo.Text.Trim().Length > 0)
                _ChapterLinkBLL.SrNo = txtSrNo.Text.Trim();
            else
                _ChapterLinkBLL.SrNo = null;

            if (txtRemarks.Text.Trim().Length > 0)
                _ChapterLinkBLL.Remarks = txtRemarks.Text.Trim();
            else
                _ChapterLinkBLL.Remarks = null;

            if (txtFileName.Text.Trim().Length > 0)
                _ChapterLinkBLL.LinkName = txtFileName.Text.Trim();
            else
                _ChapterLinkBLL.LinkName = null;

            if (txtFileLink.Text.Trim().Length > 0)
                _ChapterLinkBLL.Link = txtFileLink.Text.Trim();
            else
                _ChapterLinkBLL.Link = null;

            //if (_ChapterLinkBLL.ChapterIdMain == null)
            //    if (hfChapterId.Value.Trim().Length > 0)
            //        _ChapterLinkBLL.ChapterIdMain = hfChapterId.Value.Trim();
            //    else
            //        _ChapterLinkBLL.ChapterIdMain = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _ChapterLinkBLL.Save();

                if (Request.QueryString["ChapterLinkId"] == null)
                {
                    Reset(true);
                    Session["_ChapterLinkBLL"] = null;
                    Session["_ChapterLinkBLL"] = new ChapterLinkBLL();
                    _ChapterLinkBLL = (ChapterLinkBLL)Session["_ChapterLinkBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_ChapterLinkBLL"] = null;
                    Response.Redirect("ChapterLinks.aspx");
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
                _ChapterLinkBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue.ToString());

                _ChapterLinkBLL.ChapterId = ddlChapter.SelectedValue;

                //string ChapterId = "";

                //if (_ChapterLinkBLL.PeriodNo != null)
                //{
                //    ChapterId = _ChapterLinkBLL.GetChapterId(_ChapterLinkBLL.ChapterId, Convert.ToDecimal(_ChapterLinkBLL.PeriodNo).ToString("#.##"), ddlChapter.SelectedValue.ToString());

                //    _ChapterLinkBLL.ChapterIdMain = ChapterId;
                //}
                //else
                //{
                //    ChapterId = "";
                //    _ChapterLinkBLL.ChapterIdMain = null;
                //}
                LoadChapterVideos();
                if (_ChapterLinkBLL.SrNo != null)
                {
                    if (Request.QueryString["ChapterLinkId"] == null)
                        txtSrNo.Text = _ChapterLinkBLL.SrNo;
                }
            }
            else
            {
                _ChapterLinkBLL.PeriodNo = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    private void LoadChapterVideos()
    {
        try
        {
            ListItem li = new ListItem();

            ddlChapterVideoId.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlChapterVideoId.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _ChapterLinkBLL.LoadChapterVideo();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();

                ddlChapterVideoId.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    protected void ddlChapterVideoId_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["ChapterLinkId"] == null)
            {
                if (ddlStandard.SelectedIndex > 0)
                    _ChapterLinkBLL.StandardTextListId = ddlStandard.SelectedValue;
                else
                    _ChapterLinkBLL.StandardTextListId = null;

                if (ddlSubs.SelectedIndex > 0)
                    _ChapterLinkBLL.SubId = ddlSubs.SelectedValue;
                else
                    _ChapterLinkBLL.SubId = null;

                if (ddlChapter.SelectedIndex > 0)
                {
                    _ChapterLinkBLL.ChapterId = ddlChapter.SelectedValue;
                    hlTest.NavigateUrl = "../Exams/ViewResultDetailMasterSheet.aspx?ChapterId=" + _ChapterLinkBLL.ChapterId;
                    hlTest.Target = "_blank";
                }
                else
                    _ChapterLinkBLL.ChapterId = null;

                if (ddlChapterVideoId.SelectedIndex > 0)
                    _ChapterLinkBLL.ChapterVideoId = Convert.ToInt16(ddlChapterVideoId.SelectedValue);
                else
                    _ChapterLinkBLL.ChapterVideoId = null;

                if (_ChapterLinkBLL.SubId != null && _ChapterLinkBLL.ChapterId != null && _ChapterLinkBLL.ChapterVideoId != null)
                {
                    txtSrNo.Text = _ChapterLinkBLL.getSrNo(_ChapterLinkBLL.ChapterId.ToString(), _ChapterLinkBLL.ChapterVideoId.ToString()).ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("er", ex.Message);
        }
    }
}