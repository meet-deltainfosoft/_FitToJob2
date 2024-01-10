using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class General_Pattern : System.Web.UI.Page
{
    private PatternBLL _PatternBLL;

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["PatternId"] == null)
                {
                    _PatternBLL = new PatternBLL();
                }
                else
                {
                    _PatternBLL = new PatternBLL(Request.QueryString["PatternId"].ToString());
                }

                Session["_PatternBLL"] = _PatternBLL;
            }
            else
            {
                _PatternBLL = (PatternBLL)Session["_PatternBLL"];
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type=\"text/javascript\">CallPostBack();</script>", false);
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            try
            {
                if (Request.QueryString["PatternId"] != null)
                {
                    lblTitle.Text = " - [Edit Mode]";
                    LoadWebForm();
                    btnDelete.Visible = true;
                    btnDelete.Enabled = true;
                }
                else
                {
                    lblTitle.Text = " - [New Mode]";
                    LoadStandard();
                    btnDelete.Visible = false;
                    btnAdd.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                ShowErrors("error", ex.Message);
            }
        }
        else
        {
            try
            {
                HideErrors();
            }
            catch (Exception ex)
            {
                ShowErrors("err", ex.Message);
            }
        }
        ddlStandardTextListId.Focus();
    }

    private bool Validate()
    {
        try
        {
            HideErrors();

            SortedList sl = _PatternBLL.Validate();

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
            ShowErrors("Error", ex.Message);
            return false;
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

            pnlErr.Visible = true;
            blErrs.Items.Add(new ListItem(value));

            if (key == "StandardTextlistId")
            {
                lblStandardId.CssClass = "error";
                ddlStandardTextListId.CssClass = "error";
            }
            if (key == "PatternName")
            {
                lblPatternName.CssClass = "error";
                txtPatternName.CssClass = "error";
            }
            btnAdd.Enabled = true;
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    private void HideErrors()
    {

        try
        {
            pnlErr.Visible = false;
            blErrs.Items.Clear();

            lblStandardId.CssClass = "";
            ddlStandardTextListId.CssClass = "";

            lblPatternName.CssClass = "";
            txtPatternName.CssClass = "";
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    private void Reset()
    {
        try
        {
            ddlStandardTextListId.SelectedIndex = 0;
            txtPatternName.Text = "";
            gdvPatternLn.DataSource = null;
            gdvPatternLn.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandardTextListId.SelectedIndex > 0)
                _PatternBLL.StandardTextlistId = ddlStandardTextListId.SelectedValue;
            else
                _PatternBLL.StandardTextlistId = null;

            if (txtPatternName.Text.ToString().Length > 0)
                _PatternBLL.PatternName = txtPatternName.Text;
            else
                _PatternBLL.PatternName = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _PatternBLL.Save();

                if (Request.QueryString["PatternId"] == null)
                {
                    ShowErrors("Success", "Record Saved Successfully...");
                    Session["_PatternBLL"] = null;
                    Session["_PatternBLL"] = new PatternBLL();
                    _PatternBLL = (PatternBLL)Session["_PatternBLL"];
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>ShowStatus('Record Saved Succsessfully...');</script>", false);
                    Reset();

                }
                else
                {
                    Session["_PatternBLL"] = null;
                    Response.Redirect("Patterns.aspx");
                }
            }
            pnlPatternLn.Visible = false;
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["PatternId"] == null)
            {
                Response.Redirect("../Default.aspx");
            }
            else
            {
                Response.Redirect("Pattern.aspx");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _PatternBLL.Delete(Request.QueryString["PatternId"]);
            Session["_PatternBLL"] = null;
            Response.Redirect("Patterns.aspx");
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }


    #region Line Detail Code

    private bool ValidatePatternLn(PatternLnBLL PatternLnBLL)
    {
        try
        {
            HidePatternLnErrors();

            SortedList sl = PatternLnBLL.Validate(PatternLnBLL);

            if (sl.Count > 0)
            {
                for (int i = 0; i < sl.Count; i++)
                {
                    string key = (string)sl.GetKey(i);
                    string value = (string)sl[key];

                    ShowPatternLnErrors(key, value);
                }
            }

            return (sl.Count == 0) ? true : false;
        }
        catch (Exception ex)
        {
            ShowPatternLnErrors("Error", ex.Message);
            return false;
        }
    }

    private void ShowPatternLnErrors(string key, string value)
    {
        try
        {

            if (key == "Success")
                pnlLnErrs.CssClass = "errors alert alert-success";
            else
                pnlLnErrs.CssClass = "errors alert alert-danger";

            pnlLnErrs.Visible = true;
            blLnErrs.Items.Add(new ListItem(value));


            if (key == "SubId")
            {
                lblSubId.CssClass = "error";
                ddlSubId.CssClass = "error";
            }
        }
        catch (Exception ex)
        {
            ShowPatternLnErrors("Error", ex.Message);
        }
    }

    private void HidePatternLnErrors()
    {
        try
        {
            pnlLnErrs.Visible = false;
            blLnErrs.Items.Clear();

            lblSubId.CssClass = "";
            ddlSubId.CssClass = "";
        }
        catch (Exception ex)
        {
            ShowPatternLnErrors("Error", ex.Message);
        }
    }

    private void ResetPatternLn()
    {
        try
        {
            ddlSubId.SelectedIndex = 0;
            txtNoOfMCQ.Text = "";
            txtMCQRightMarks.Text = "";
            txtMCQWrongMarks.Text = "";
            txtMCQSkippedMarks.Text = "";
            txtNoOfNonMCQ.Text = "";
            txtNonMCQRightMarks.Text = "";
            txtNonMCQWrongMarks.Text = "";
            txtNonMCQSkippedMarks.Text = "";
        }
        catch (Exception ex)
        {
            ShowPatternLnErrors("Error", ex.Message);
        }
    }

    #endregion

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandardTextListId.SelectedIndex > 0)
            {
                PatternLnBLL _PatternLnBLL = new PatternLnBLL();
                LoadSubjects();
                gdvPatternLn.Enabled = false;
                btnAdd.Enabled = false;
                ResetPatternLn();
                txtLnNo.Text = (_PatternBLL.PatternLnsBLL.Count + 1).ToString();

                if (Request.QueryString["PatternId"] != null)
                    _PatternLnBLL.PatternId = Request.QueryString["PatternId"].ToString();

                pnlPatternLn.Visible = true;

                gdvPatternLn.DataSource = _PatternBLL.PatternLnsBLL;
                gdvPatternLn.DataBind();
                Session["_PatternLnBLL"] = _PatternLnBLL;
                txtLnNo.Focus();
            }
            else
            {
                ShowErrors("Error", " Please Select Department. ");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    protected void btnClose_Click(object sender, EventArgs e)
    {
        try
        {
            if (gdvPatternLn.SelectedIndex >= 0) gdvPatternLn.SelectedIndex = -1;
            ResetPatternLn();

            pnlPatternLn.Visible = false;
            btnAdd.Enabled = true;
            Session["_PatternLnBLL"] = null;

            gdvPatternLn.Enabled = true;
            gdvPatternLn.DataSource = _PatternBLL.PatternLnsBLL;
            gdvPatternLn.DataBind();
            btnOK.Focus();
        }
        catch (Exception ex)
        {
            ShowPatternLnErrors("Error", ex.Message);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PatternLnBLL PatternLnBLL = (PatternLnBLL)Session["_PatternLnBLL"];
        try
        {
            if (gdvPatternLn.SelectedIndex < 0)//New
            {
                PatternLnBLL = (PatternLnBLL)Session["_PatternLnBLL"];
                PatternLnBLL.IsNew = true;
            }
            else//Edit
            {
                PatternLnBLL = _PatternBLL.PatternLnsBLL[gdvPatternLn.SelectedIndex];
                PatternLnBLL.IsDirty = true;
            }

            PatternLnBLL.LnNo = Convert.ToInt32(txtLnNo.Text);

            if (ddlSubId.SelectedIndex > 0)
            {
                PatternLnBLL.SubId = ddlSubId.SelectedValue;
                PatternLnBLL.Subject = ddlSubId.SelectedItem.Text;
            }
            else
            {
                PatternLnBLL.SubId = null;
                PatternLnBLL.Subject = null;
            }

            if (txtNoOfMCQ.Text.Trim().Length > 0)
                PatternLnBLL.NoOfMCQ = Convert.ToInt32(txtNoOfMCQ.Text);
            else
                PatternLnBLL.NoOfMCQ = null;

            if (txtMCQRightMarks.Text.Trim().Length > 0)
                PatternLnBLL.MCQRightMarks = Convert.ToDecimal(txtMCQRightMarks.Text);
            else
                PatternLnBLL.MCQRightMarks = null;

            if (txtMCQWrongMarks.Text.Trim().Length > 0)
                PatternLnBLL.MCQWrongMarks = Convert.ToDecimal(txtMCQWrongMarks.Text);
            else
                PatternLnBLL.MCQWrongMarks = null;

            if (txtMCQSkippedMarks.Text.Trim().Length > 0)
                PatternLnBLL.MCQSkippedMarks = Convert.ToDecimal(txtMCQSkippedMarks.Text);
            else
                PatternLnBLL.MCQSkippedMarks = null;

            if (txtNoOfNonMCQ.Text.Trim().Length > 0)
                PatternLnBLL.NoOfNonMCQ = Convert.ToInt32(txtNoOfNonMCQ.Text);
            else
                PatternLnBLL.NoOfNonMCQ = null;

            if (txtNonMCQRightMarks.Text.Trim().Length > 0)
                PatternLnBLL.NonMCQRightMarks = Convert.ToDecimal(txtNonMCQRightMarks.Text);
            else
                PatternLnBLL.NonMCQRightMarks = null;

            if (txtNonMCQWrongMarks.Text.Trim().Length > 0)
                PatternLnBLL.NonMCQWrongMarks = Convert.ToDecimal(txtNonMCQWrongMarks.Text);
            else
                PatternLnBLL.NonMCQWrongMarks = null;

            if (txtNonMCQSkippedMarks.Text.Trim().Length > 0)
                PatternLnBLL.NonMCQSkippedMarks = Convert.ToDecimal(txtNonMCQSkippedMarks.Text);
            else
                PatternLnBLL.NonMCQSkippedMarks = null;

            bool isValid = ValidatePatternLn(PatternLnBLL);

            if (isValid == true)
            {
                if (gdvPatternLn.SelectedIndex < 0)//New
                {
                    _PatternBLL.PatternLnsBLL.Add(PatternLnBLL);
                }
                else//Edit
                {
                    PatternLnBLL.IsDirty = true;
                    _PatternBLL.PatternLnsBLL[gdvPatternLn.SelectedIndex] = PatternLnBLL;
                    gdvPatternLn.SelectedIndex = -1;
                }

                Session["_PatternBLL"] = _PatternBLL;
                ResetPatternLn();

                gdvPatternLn.Enabled = true;
                gdvPatternLn.DataSource = _PatternBLL.PatternLnsBLL;
                gdvPatternLn.DataBind();

                btnAdd.Enabled = true;
                pnlPatternLn.Visible = false;
                btnDelete.Enabled = true;
                btnOK.Enabled = true;
                btnCancel.Enabled = true;
                divGridLn.Visible = true;
                btnOK.Focus();
            }

        }
        catch (Exception ex)
        {
            ShowPatternLnErrors("Error", ex.Message);
        }
    }

    protected void btnSaveAdd_Click(object sender, EventArgs e)
    {
        try
        {
            btnSave_Click(null, null);
            if (pnlErr.Visible == false)
            {
                btnAdd_Click(null, null);
            }
        }
        catch (Exception ex)
        {
            ShowPatternLnErrors("Error", ex.Message);
        }
    }

    private void LoadWebForm()
    {
        try
        {
            LoadStandard();
            if (_PatternBLL.StandardTextlistId != null)
                ddlStandardTextListId.SelectedValue = _PatternBLL.StandardTextlistId;

            if (_PatternBLL.PatternName != null)
                txtPatternName.Text = _PatternBLL.PatternName.ToString();

            gdvPatternLn.DataSource = _PatternBLL.PatternLnsBLL;
            gdvPatternLn.DataBind();
        }
        catch (Exception ex)
        {
            ShowErrors("Error", ex.Message);
        }
    }

    protected void gdvPatternLn_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gdvPatternLn.Enabled = false;
            btnAdd.Enabled = false;
            pnlPatternLn.Visible = true;

            PatternLnBLL _PatternLnBLL = _PatternBLL.PatternLnsBLL[gdvPatternLn.SelectedIndex];
            Session["_PatternLnBLL"] = _PatternLnBLL;
            SetPatternLn(_PatternLnBLL);
        }
        catch (Exception ex)
        {
            ShowPatternLnErrors("Error", ex.Message);
        }
    }

    private void SetPatternLn(PatternLnBLL PatternLnBLL)
    {
        try
        {
            if (PatternLnBLL.LnNo != null)
                txtLnNo.Text = (PatternLnBLL.LnNo).ToString();

            LoadSubjects();

            if (PatternLnBLL.SubId != null)
                ddlSubId.SelectedValue = PatternLnBLL.SubId.ToString();

            if (PatternLnBLL.NoOfMCQ != null)
                txtNoOfMCQ.Text = Convert.ToInt32(PatternLnBLL.NoOfMCQ).ToString();

            if (PatternLnBLL.MCQRightMarks != null)
                txtMCQRightMarks.Text = Convert.ToDecimal(PatternLnBLL.MCQRightMarks).ToString("0.##");

            if (PatternLnBLL.MCQWrongMarks != null)
                txtMCQWrongMarks.Text = Convert.ToDecimal(PatternLnBLL.MCQWrongMarks).ToString("0.##");

            if (PatternLnBLL.MCQSkippedMarks != null)
                txtMCQSkippedMarks.Text = Convert.ToDecimal(PatternLnBLL.MCQSkippedMarks).ToString("0.##");

            if (PatternLnBLL.NoOfNonMCQ != null)
                txtNoOfNonMCQ.Text = Convert.ToInt32(PatternLnBLL.NoOfNonMCQ).ToString();

            if (PatternLnBLL.NonMCQRightMarks != null)
                txtNonMCQRightMarks.Text = Convert.ToDecimal(PatternLnBLL.NonMCQRightMarks).ToString("0.##");

            if (PatternLnBLL.NonMCQWrongMarks != null)
                txtNonMCQWrongMarks.Text = Convert.ToDecimal(PatternLnBLL.NonMCQWrongMarks).ToString("0.##");

            if (PatternLnBLL.NonMCQSkippedMarks != null)
                txtNonMCQSkippedMarks.Text = Convert.ToDecimal(PatternLnBLL.NonMCQSkippedMarks).ToString("0.##");
        }
        catch (Exception ex)
        {
            ShowPatternLnErrors("Error", ex.Message);
        }
    }

    protected void gdvPatternLn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            _PatternBLL.PatternLnsBLL.Remove(e.RowIndex);
            Session["_PatternBLL"] = _PatternBLL;
            gdvPatternLn.DataSource = _PatternBLL.PatternLnsBLL;
            gdvPatternLn.DataBind();
        }
        catch (Exception ex)
        {
            ShowPatternLnErrors("Error", ex.Message);
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
            dt = _PatternBLL.LoadStandard();

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
            ShowErrors("Error", ex.Message);
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
            dt = _PatternBLL.LoadSubjects(ddlStandardTextListId.SelectedValue.ToString());

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
            ShowPatternLnErrors("Error", ex.Message);
        }
    }
}