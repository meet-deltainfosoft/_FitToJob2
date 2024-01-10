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

public partial class General_VideoNo : System.Web.UI.Page
{
    VideoNoBLL _VideoNoBLL = new VideoNoBLL();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["VideoNoId"] == null)
                {
                    _VideoNoBLL = new VideoNoBLL();
                }
                else
                {
                    _VideoNoBLL = new VideoNoBLL(Request.QueryString["VideoNoId"].ToString());
                }
                Session["_VideoNoBLL"] = _VideoNoBLL;
            }
            else
            {
                _VideoNoBLL = (VideoNoBLL)Session["_VideoNoBLL"];
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
                if (Request.QueryString["VideoNoId"] != null)
                {
                    lblTitle.Text = " - [Edit Mode]";
                    LoadWebForm();
                    btnDelete.Enabled = true;
                    btnOK.Visible = true;
                }
                else
                {
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

        lblPeriodNo.CssClass = "";
        ddlPeriodNo.CssClass = "";

        txtPeronNm1.CssClass = "";
        lblPersonNm1.CssClass = "";

        lblRatio1.CssClass = "";
        txtRatio1.CssClass = "";
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

        if (key == "PersonName1")
        {
            lblPersonNm1.CssClass = "error";
            txtPeronNm1.CssClass = "error";
        }

        if (key == "Ratio1")
        {
            lblRatio1.CssClass = "error";
            txtRatio1.CssClass = "error";
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
            dt = _VideoNoBLL.LoadStandard();

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
            dt = _VideoNoBLL.LoadSubjects();

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
            dt = _VideoNoBLL.LoadChapter();

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
            dt = _VideoNoBLL.LoadPeriodNo();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[0].ToString();
                li.Value = dtr[0].ToString();
                ddlPeriodNo.Items.Add(li);

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
                _VideoNoBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _VideoNoBLL.StandardTextListId = null;

            if (ddlSubs.SelectedIndex > 0)
                _VideoNoBLL.SubId = ddlSubs.SelectedValue;
            else
                _VideoNoBLL.SubId = null;

            if (ddlChapter.SelectedIndex > 0)
                _VideoNoBLL.ChapterId = ddlChapter.SelectedValue;
            else
                _VideoNoBLL.ChapterId = null;

            if (ddlPeriodNo.SelectedIndex > 0)
                _VideoNoBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);
            else
                _VideoNoBLL.PeriodNo = null;

            if (txtPeronNm1.Text.Trim().Length > 0)
                _VideoNoBLL.PersonName1 = txtPeronNm1.Text.Trim();
            else
                _VideoNoBLL.PersonName1 = null;

            if (txtPeronNm2.Text.Trim().Length > 0)
                _VideoNoBLL.PersonName2 = txtPeronNm2.Text.Trim();
            else
                _VideoNoBLL.PersonName2 = null;

            if (txtPeronNm3.Text.Trim().Length > 0)
                _VideoNoBLL.PersonName3 = txtPeronNm3.Text.Trim();
            else
                _VideoNoBLL.PersonName3 = null;

            if (txtPeronNm4.Text.Trim().Length > 0)
                _VideoNoBLL.PersonName4 = txtPeronNm4.Text.Trim();
            else
                _VideoNoBLL.PersonName4 = null;

            if (txtPeronNm5.Text.Trim().Length > 0)
                _VideoNoBLL.PersonName5 = txtPeronNm5.Text.Trim();
            else
                _VideoNoBLL.PersonName5 = null;

            if (txtRatio1.Text.Trim().Length > 0)
                _VideoNoBLL.Ratio1 = txtRatio1.Text.Trim();
            else
                _VideoNoBLL.Ratio1 = null;

            if (txtRatio2.Text.Trim().Length > 0)
                _VideoNoBLL.Ratio2 = txtRatio2.Text.Trim();
            else
                _VideoNoBLL.Ratio2 = null;

            if (txtRatio3.Text.Trim().Length > 0)
                _VideoNoBLL.Ratio3 = txtRatio3.Text.Trim();
            else
                _VideoNoBLL.Ratio3 = null;

            if (txtRatio4.Text.Trim().Length > 0)
                _VideoNoBLL.Ratio4 = txtRatio4.Text.Trim();
            else
                _VideoNoBLL.Ratio4 = null;

            if (txtRatio5.Text.Trim().Length > 0)
                _VideoNoBLL.Ratio5 = txtRatio5.Text.Trim();
            else
                _VideoNoBLL.Ratio5 = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _VideoNoBLL.Save();

                if (Request.QueryString["VideoNoId"] == null)
                {
                    Reset(false);
                    Session["_VideoNoBLL"] = null;
                    Session["_VideoNoBLL"] = new VideoNoBLL();
                    _VideoNoBLL = (VideoNoBLL)Session["_VideoNoBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_VideoNoBLL"] = null;
                    Response.Redirect("VideoNos.aspx");
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
            Session["_VideoNoBLL"] = null;

            if (Request.QueryString["VideoNoId"] == null)
            {
                Response.Redirect("~/General/Default.aspx");
            }
            else
            {
                Response.Redirect("VideoNos.aspx");
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
            _VideoNoBLL.Delete(Request.QueryString["VideoNoId"]);
            Session["_VideoNoBLL"] = null;
            Response.Redirect("VideoNos.aspx");
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

            SortedList sl = _VideoNoBLL.Validate();

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
                _VideoNoBLL.StandardTextListId = ddlStandard.SelectedValue;
                ddlStandard_SelectedIndexChanged(ddlStandard, null);

                ddlSubs.SelectedValue = _VideoNoBLL.SubId;
                _VideoNoBLL.SubId = ddlSubs.SelectedValue;
                ddlSubs_SelectedIndexChanged(ddlSubs, null);

                ddlChapter.SelectedValue = _VideoNoBLL.ChapterId;
                _VideoNoBLL.ChapterId = ddlChapter.SelectedValue;
                ddlChapter_SelectedIndexChanged(ddlChapter, null);


                ddlPeriodNo.SelectedValue = Convert.ToDecimal(_VideoNoBLL.PeriodNo).ToString("#.##");
                _VideoNoBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);

                txtPeronNm1.Text = "";
                txtPeronNm2.Text = "";
                txtPeronNm3.Text = "";
                txtPeronNm4.Text = "";
                txtPeronNm5.Text = "";
                txtRatio1.Text = "";
                txtRatio2.Text = "";
                txtRatio3.Text = "";
                txtRatio4.Text = "";
                txtRatio5.Text = "";
            }
            else
            {
                ddlStandard.SelectedIndex = 0;
                ddlSubs.SelectedIndex = 0;
                ddlChapter.SelectedIndex = 0;
                ddlPeriodNo.Items.Clear();

                txtPeronNm1.Text = "";
                txtPeronNm2.Text = "";
                txtPeronNm3.Text = "";
                txtPeronNm4.Text = "";
                txtPeronNm5.Text = "";
                txtRatio1.Text = "";
                txtRatio2.Text = "";
                txtRatio3.Text = "";
                txtRatio4.Text = "";
                txtRatio5.Text = "";
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
                _VideoNoBLL.StandardTextListId = ddlStandard.SelectedValue.ToString();
                LoadSubjects();
            }
            else
            {
                _VideoNoBLL.StandardTextListId = null;

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
            if (_VideoNoBLL.StandardTextListId != null)
            {
                ddlStandard.SelectedValue = _VideoNoBLL.StandardTextListId.ToString();
                LoadSubjects();
                //ddlStandard_SelectedIndexChanged(null, null);
                //ddlStandard.Enabled = false;
            }
            if (_VideoNoBLL.SubId != null)
            {
                ddlSubs.SelectedValue = _VideoNoBLL.SubId.ToString();
                LoadChapter();
                //ddlSubs_SelectedIndexChanged(null, null);
            }
            if (_VideoNoBLL.ChapterId != null)
            {
                ddlChapter.SelectedValue = _VideoNoBLL.ChapterId.ToString();
                //LoadChapter();
                ddlChapter_SelectedIndexChanged(null, null);
            }

            if (_VideoNoBLL.PeriodNo != null)
            {
                ddlPeriodNo.SelectedValue = Convert.ToDecimal(_VideoNoBLL.PeriodNo).ToString("#.##");
            }

            if (_VideoNoBLL.PersonName1 != null)
                txtPeronNm1.Text = _VideoNoBLL.PersonName1;

            if (_VideoNoBLL.PersonName2 != null)
                txtPeronNm2.Text = _VideoNoBLL.PersonName2;

            if (_VideoNoBLL.PersonName3 != null)
                txtPeronNm3.Text = _VideoNoBLL.PersonName3;

            if (_VideoNoBLL.PersonName4 != null)
                txtPeronNm4.Text = _VideoNoBLL.PersonName4;

            if (_VideoNoBLL.PersonName5 != null)
                txtPeronNm5.Text = _VideoNoBLL.PersonName5;

            if (_VideoNoBLL.Ratio1 != null)
                txtRatio1.Text = _VideoNoBLL.Ratio1;

            if (_VideoNoBLL.Ratio2 != null)
                txtRatio2.Text = _VideoNoBLL.Ratio2;

            if (_VideoNoBLL.Ratio3 != null)
                txtRatio3.Text = _VideoNoBLL.Ratio3;

            if (_VideoNoBLL.Ratio4 != null)
                txtRatio4.Text = _VideoNoBLL.Ratio4;

            if (_VideoNoBLL.Ratio5 != null)
                txtRatio5.Text = _VideoNoBLL.Ratio5;
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
                _VideoNoBLL.SubId = ddlSubs.SelectedValue.ToString();
                LoadChapter();

            }
            else
            {
                _VideoNoBLL.SubId = null;
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
                _VideoNoBLL.ChapterId = ddlChapter.SelectedValue.ToString();

                _VideoNoBLL.SubId = ddlSubs.SelectedValue.ToString();
                LoadPeriodNo();
            }
            else
            {
                ddlPeriodNo.Items.Clear();
                _VideoNoBLL.ChapterId = null;
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
                _VideoNoBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _VideoNoBLL.StandardTextListId = null;

            if (ddlSubs.SelectedIndex > 0)
                _VideoNoBLL.SubId = ddlSubs.SelectedValue;
            else
                _VideoNoBLL.SubId = null;

            if (ddlChapter.SelectedIndex > 0)
                _VideoNoBLL.ChapterId = ddlChapter.SelectedValue;
            else
                _VideoNoBLL.ChapterId = null;

            if (ddlPeriodNo.SelectedIndex > 0)
                _VideoNoBLL.PeriodNo = Convert.ToDecimal(ddlPeriodNo.SelectedValue);
            else
                _VideoNoBLL.PeriodNo = null;

            if (txtPeronNm1.Text.Trim().Length > 0)
                _VideoNoBLL.PersonName1 = txtPeronNm1.Text.Trim();
            else
                _VideoNoBLL.PersonName1 = null;

            if (txtPeronNm2.Text.Trim().Length > 0)
                _VideoNoBLL.PersonName2 = txtPeronNm2.Text.Trim();
            else
                _VideoNoBLL.PersonName2 = null;

            if (txtPeronNm3.Text.Trim().Length > 0)
                _VideoNoBLL.PersonName3 = txtPeronNm3.Text.Trim();
            else
                _VideoNoBLL.PersonName3 = null;

            if (txtPeronNm4.Text.Trim().Length > 0)
                _VideoNoBLL.PersonName4 = txtPeronNm4.Text.Trim();
            else
                _VideoNoBLL.PersonName4 = null;

            if (txtPeronNm5.Text.Trim().Length > 0)
                _VideoNoBLL.PersonName5 = txtPeronNm5.Text.Trim();
            else
                _VideoNoBLL.PersonName5 = null;

            if (txtRatio1.Text.Trim().Length > 0)
                _VideoNoBLL.Ratio1 = txtRatio1.Text.Trim();
            else
                _VideoNoBLL.Ratio1 = null;

            if (txtRatio2.Text.Trim().Length > 0)
                _VideoNoBLL.Ratio2 = txtRatio2.Text.Trim();
            else
                _VideoNoBLL.Ratio2 = null;

            if (txtRatio3.Text.Trim().Length > 0)
                _VideoNoBLL.Ratio3 = txtRatio3.Text.Trim();
            else
                _VideoNoBLL.Ratio3 = null;

            if (txtRatio4.Text.Trim().Length > 0)
                _VideoNoBLL.Ratio4 = txtRatio4.Text.Trim();
            else
                _VideoNoBLL.Ratio4 = null;

            if (txtRatio5.Text.Trim().Length > 0)
                _VideoNoBLL.Ratio5 = txtRatio5.Text.Trim();
            else
                _VideoNoBLL.Ratio5 = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _VideoNoBLL.Save();

                if (Request.QueryString["VideoNoId"] == null)
                {
                    Reset(true);
                    Session["_VideoNoBLL"] = null;
                    Session["_VideoNoBLL"] = new VideoNoBLL();
                    _VideoNoBLL = (VideoNoBLL)Session["_VideoNoBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_VideoNoBLL"] = null;
                    Response.Redirect("VideoNos.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
}