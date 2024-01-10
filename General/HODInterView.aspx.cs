using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.Text;

public partial class General_HODInterView : System.Web.UI.Page
{
    HODInterviewBLL _HODInterviewBLL;
    private GeneralBLL _GeneralBLL = new GeneralBLL();
    private StringBuilder sbjQueryNumeric = new StringBuilder();
    public decimal Total = 0;

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["HODHODInterviewId"] == null)
                {
                    _HODInterviewBLL = new HODInterviewBLL();
                }
                else
                {
                    _HODInterviewBLL = new HODInterviewBLL(Request.QueryString["HODInterviewId"].ToString());
                }
                Session["_HODInterviewBLL"] = _HODInterviewBLL;
            }
            else
            {
                _HODInterviewBLL = (HODInterviewBLL)Session["_HODInterviewBLL"];
            }
        }
        catch (Exception ex)
        {
            ShowErrors("error", ex.Message);
            HideErrors();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HideErrors();

            if (Page.IsPostBack == false)
            {
                string[] path = Request.AppRelativeCurrentExecutionFilePath.Split('/');
                _GeneralBLL.FormName = path[path.Length - 1];

                //LoadSalaryCalculationMethod();
                if (Request.QueryString["HODInterviewId"] != null) //Edit Mode
                {
                    lblTitle.Text = " - [Edit Mode]";
                    LoadWebForm();

                    btnDelete.Enabled = true;
                    btnDelete.Visible = true;
                }
                else //new mode
                {
                    txtName.Text = Request.QueryString["CandidateName"].ToString();
                    if (Request.QueryString["RegistrationId"] != null)
                    {
                        btnQueAdd.Visible = true;
                        btnQueAdd_Click(null, null);
                        _HODInterviewBLL.CareerId = Request.QueryString["RegistrationId"].ToString();
                        if (Request.QueryString["Name"] != null)
                        {
                            txtName.Text = Request.QueryString["Name"].ToString();
                            txtName.Enabled = false;

                            txtDt.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                        }
                    }
                    btnDelete.Visible = false;
                }
            }
            txtRemarks.Focus();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type=\"text/javascript\">CallPostBack();</script>", false);
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    #endregion

    #region "Events"
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtName.Text.Trim().Length > 0)
                _HODInterviewBLL.Name = txtName.Text.Trim();
            else
                _HODInterviewBLL.Name = null;

            if (txtUser.Text.Trim().Length > 0)
            {
                if (hfUserId.Value != null && hfUserId.Value != "")
                    _HODInterviewBLL.UserId = hfUserId.Value;
                else
                    _HODInterviewBLL.UserId = null;
            }
            else
            {
                _HODInterviewBLL.UserId = null;
            }

            if (rdbSelected.Checked == true)
                _HODInterviewBLL.Status = rdbSelected.Text;
            else if (rdbRejected.Checked == true)
                _HODInterviewBLL.Status = rdbRejected.Text;
            else if (rdbHold.Checked == true)
                _HODInterviewBLL.Status = rdbHold.Text;

            if (txtRemarks.Text.Trim().Length > 0)
                _HODInterviewBLL.Remarks = txtRemarks.Text.Trim();
            else
                _HODInterviewBLL.Remarks = null;

            if (txtDt.Text.Trim().Length > 0)
                _HODInterviewBLL.Dt = Convert.ToDateTime(txtDt.Text);
            else
                _HODInterviewBLL.Dt = null;

            if (ddlSalaryCalculation.SelectedIndex > 0)
                _HODInterviewBLL.SalStructureId = ddlSalaryCalculation.SelectedValue;
            else
                _HODInterviewBLL.SalStructureId = null;

            if (txtCTC.Text.Trim().Length > 0)
                _HODInterviewBLL.CTC = Convert.ToDecimal(txtCTC.Text);
            else
                _HODInterviewBLL.CTC = null;

            if (lblViewMonthlyGrossSalary.Text.Trim().Length > 0)
                _HODInterviewBLL.ViewMonthlyGrossSalary = Convert.ToDecimal(lblViewMonthlyGrossSalary.Text);
            else
                _HODInterviewBLL.ViewMonthlyGrossSalary = null;

            if (lblViewMonthlyBasic.Text.Trim().Length > 0)
                _HODInterviewBLL.ViewMonthlyBasic = Convert.ToDecimal(lblViewMonthlyBasic.Text);
            else
                _HODInterviewBLL.ViewMonthlyBasic = null;

            if (lblViewMonthlyHRA.Text.Trim().Length > 0)
                _HODInterviewBLL.ViewMonthlyHRA = Convert.ToDecimal(lblViewMonthlyHRA.Text);
            else
                _HODInterviewBLL.ViewMonthlyHRA = null;

            if (txtConveyance.Text.Trim().Length > 0)
                _HODInterviewBLL.Conveyance = Convert.ToDecimal(txtConveyance.Text);
            else
                _HODInterviewBLL.Conveyance = null;

            if (txtSpecialAllowances.Text.Trim().Length > 0)
                _HODInterviewBLL.SpecialAllowances = Convert.ToDecimal(txtSpecialAllowances.Text);
            else
                _HODInterviewBLL.SpecialAllowances = null;

            if (lblViewMonthlyPFCmpnyShare13Point61Per.Text.Trim().Length > 0)
                _HODInterviewBLL.ViewMonthlyPFCmpnyShare13Point61Per = Convert.ToDecimal(lblViewMonthlyPFCmpnyShare13Point61Per.Text);
            else
                _HODInterviewBLL.ViewMonthlyPFCmpnyShare13Point61Per = null;

            if (lblViewMonthlyESIEmpShare4Point75Per.Text.Trim().Length > 0)
                _HODInterviewBLL.ViewMonthlyESIEmpShare4Point75Per = Convert.ToDecimal(lblViewMonthlyESIEmpShare4Point75Per.Text);
            else
                _HODInterviewBLL.ViewMonthlyESIEmpShare4Point75Per = null;

            _HODInterviewBLL.IsDeductESI = chkIsDeductESI.Checked;
            _HODInterviewBLL.IsDeductPF = chkIsDeductPF.Checked;

            SetGridToBLL();

            bool isValid = Validate();

            if (isValid == true)
            {
                string HODInterviewId;
                HODInterviewId = _HODInterviewBLL.Save();

                if (Request.QueryString["HODInterviewId"] == null)
                {
                    ShowErrors("Success", "Record Saved Successfully...");
                    Session["_HODInterviewBLL"] = null;
                    Session["_HODInterviewBLL"] = new HODInterviewBLL();
                    _HODInterviewBLL = (HODInterviewBLL)Session["_HODInterviewBLL"];
                    Reset();

                    if (chkPrint.Checked == true)
                    {
                        string criteria;
                        string RegistrationId;
                        RegistrationId = Request.QueryString["RegistrationId"];
                        criteria = "?RptType=OfferLatter";
                        criteria += "&RptId=" + RegistrationId;
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../Report/Exam/CRViewer.aspx" + criteria + "', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                        //string criteria;

                        //criteria = "?RptId=" + HODInterviewId;
                        //criteria += "&RptType=OfferLatter";
                        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '../Reports/Career/CRViewer.aspx" + criteria + "', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
                    }
                }
                else
                {
                    Session["_HODInterviewBLL"] = null;
                    if (chkPrint.Checked == true)
                    {
                        string criteria;

                        criteria = "?RptId=" + HODInterviewId;
                        criteria += "&RptType=OfferLatter";
                        Response.Redirect("Interviews.aspx" + criteria);
                    }
                    else
                    {
                        Response.Redirect("Interviews.aspx");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["_HODInterviewBLL"] = null;

        if (Request.QueryString["HODInterviewId"] == null)
        {
            if (Request.QueryString["FormType"] != null)
            {
                if (Request.QueryString["FormType"].ToString() == "Recruiter")
                {
                    Response.Redirect("CarrierList.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }
        else
        {
            Response.Redirect("Interviews.aspx");
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _HODInterviewBLL.Delete(Request.QueryString["HODInterviewId"]);
            Session["_HODInterviewBLL"] = null;
            Response.Redirect("Interviews.aspx");
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }
    #endregion

    #region Validation, HideError, SHow Error, Load Data
    private void LoadWebForm()
    {
        if (_HODInterviewBLL.Name != null)
            txtName.Text = _HODInterviewBLL.Name;

        if (_HODInterviewBLL.UserId != null)
        {
            txtUser.Text = _HODInterviewBLL.UserName;
            hfUserId.Value = _HODInterviewBLL.UserId;
        }

        if (_HODInterviewBLL.Status != null)
        {
            if (_HODInterviewBLL.Status == "Selected")
                rdbSelected.Checked = true;
            else if (_HODInterviewBLL.Status == "Rejected")
                rdbRejected.Checked = true;
            else if (_HODInterviewBLL.Status == "Hold")
                rdbHold.Checked = true;
        }

        if (_HODInterviewBLL.Remarks != null)
            txtRemarks.Text = _HODInterviewBLL.Remarks;

        if (_HODInterviewBLL.Dt != null)
            txtDt.Text = Convert.ToDateTime(_HODInterviewBLL.Dt).ToString("dd-MMM-yyyy");

        if (_HODInterviewBLL.CTC != null)
            txtCTC.Text = Convert.ToDecimal(_HODInterviewBLL.CTC).ToString("0.00");

        if (_HODInterviewBLL.ViewMonthlyGrossSalary != null)
            lblViewMonthlyGrossSalary.Text = Convert.ToDecimal(_HODInterviewBLL.ViewMonthlyGrossSalary).ToString("0.00");

        if (_HODInterviewBLL.ViewMonthlyBasic != null)
            lblViewMonthlyBasic.Text = Convert.ToDecimal(_HODInterviewBLL.ViewMonthlyBasic).ToString("0.00");

        if (_HODInterviewBLL.ViewMonthlyHRA != null)
            lblViewMonthlyHRA.Text = Convert.ToDecimal(_HODInterviewBLL.ViewMonthlyHRA).ToString("0.00");

        if (_HODInterviewBLL.Conveyance != null)
            txtConveyance.Text = Convert.ToDecimal(_HODInterviewBLL.Conveyance).ToString("0.00");

        if (_HODInterviewBLL.SpecialAllowances != null)
            txtSpecialAllowances.Text = Convert.ToDecimal(_HODInterviewBLL.SpecialAllowances).ToString("0.00");

        if (_HODInterviewBLL.ViewMonthlyPFCmpnyShare13Point61Per != null)
            lblViewMonthlyPFCmpnyShare13Point61Per.Text = Convert.ToDecimal(_HODInterviewBLL.ViewMonthlyPFCmpnyShare13Point61Per).ToString("0.00");

        if (_HODInterviewBLL.ViewMonthlyESIEmpShare4Point75Per != null)
            lblViewMonthlyESIEmpShare4Point75Per.Text = Convert.ToDecimal(_HODInterviewBLL.ViewMonthlyESIEmpShare4Point75Per).ToString("0.00");

        if (_HODInterviewBLL.IsDeductPF != null)
        {
            if (_HODInterviewBLL.IsDeductPF == true)
            {
                chkIsDeductPF.Checked = true;
            }
        }

        if (_HODInterviewBLL.IsDeductESI != null)
        {
            if (_HODInterviewBLL.IsDeductESI == true)
            {
                chkIsDeductESI.Checked = true;
            }
        }

        if (_HODInterviewBLL.SalStructureId != null)
            ddlSalaryCalculation.SelectedValue = _HODInterviewBLL.SalStructureId;

        sbjQueryNumeric.AppendLine("<script type=\"text/javascript\">");
        sbjQueryNumeric.AppendLine("$(document).ready(function() {");

        pnlLn.Visible = true;
        gdvLns.Visible = true;
        gdvLns.Enabled = true;
        gdvLns.DataSource = _HODInterviewBLL.HODInterviewLnsBLL;
        gdvLns.DataBind();

        sbjQueryNumeric.AppendLine(" });");
        sbjQueryNumeric.AppendLine("</script>");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "A", sbjQueryNumeric.ToString());
    }
    private bool Validate()
    {
        HideErrors();

        SortedList sl = _HODInterviewBLL.Validate();

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
    private void ShowErrors(string key, string value)
    {
        if (key == "Success")
            pnlErr.CssClass = "errors alert alert-success";
        else
            pnlErr.CssClass = "errors alert alert-danger";

        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));

        if (key == "Name")
        {
            lblName.CssClass = "errrShow";
            txtName.CssClass = "errrShow";
        }
        if (key == "Status")
        {
            lblStatus.CssClass = "errrShow";
            rdbSelected.CssClass = "errrShow";
            rdbRejected.CssClass = "errrShow";
            rdbHold.CssClass = "errrShow";
        }
        if (key == "Dt")
        {
            lblDt.CssClass = "errrShow";
            txtDt.CssClass = "errrShow";
        }
    }
    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        lblName.CssClass = "";
        txtName.CssClass = "";

        lblStatus.CssClass = "";
        rdbSelected.CssClass = "";
        rdbRejected.CssClass = "";
        rdbHold.CssClass = "";

        lblDt.CssClass = "";
        txtDt.CssClass = "";
    }
    private void Reset()
    {
        txtName.Text = "";
        txtUser.Text = "";
        hfUserId.Value = "";
        rdbSelected.Checked = false;
        rdbRejected.Checked = false;
        rdbHold.Checked = false;
        txtRemarks.Text = "";
        txtDt.Text = "";
        ddlSalaryCalculation.SelectedValue = "";
        txtCTC.Text = "";
        lblViewMonthlyGrossSalary.Text = "";
        lblViewMonthlyBasic.Text = "";
        lblViewMonthlyHRA.Text = "";
        txtConveyance.Text = "";
        txtSpecialAllowances.Text = "";
        lblViewMonthlyPFCmpnyShare13Point61Per.Text = "";
        lblViewMonthlyESIEmpShare4Point75Per.Text = "";
        chkIsDeductPF.Checked = false;
        chkIsDeductESI.Checked = false;

        gdvLns.DataSource = null;
        gdvLns.DataBind();
    }
    #endregion

    #region Salary
    protected void ddlSalaryCalculation_SelectedIndexChanged(object sender, EventArgs e)
    {
        decimal CalcAmount = 0;

        if (txtCTC.Text.Trim().Length > 0)
        {
            CalcAmount = Convert.ToDecimal(txtCTC.Text.Trim());
        }
        else
        {
            CalcAmount = 0;
        }
        if (ddlSalaryCalculation.SelectedIndex > 0 && CalcAmount > 0)
        {
            DataTable dt = new DataTable();
            dt = _HODInterviewBLL.GetMonthyBasicAndHRA(txtName.Text, ddlSalaryCalculation.SelectedValue, Convert.ToDecimal(CalcAmount), chkIsDeductPF.Checked, chkIsDeductESI.Checked, ((lblViewMonthlyBasic.Text == "") ? 0 : Convert.ToDecimal(lblViewMonthlyBasic.Text)));

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Salary"] != DBNull.Value)
                    lblViewMonthlyGrossSalary.Text = Convert.ToDecimal(dt.Rows[0]["Salary"]).ToString("0.##");
                else
                    lblViewMonthlyGrossSalary.Text = "";

                if (dt.Rows[0]["BasicSalary"] != DBNull.Value)
                {
                    lblViewMonthlyBasic.Text = Convert.ToDecimal(dt.Rows[0]["BasicSalary"]).ToString("0.##");
                    if (lblViewMonthlyBasic.Text.Trim().Length > 0)
                    {
                        if (Convert.ToDecimal(lblViewMonthlyBasic.Text) != Convert.ToDecimal(dt.Rows[0]["BasicSalary"]))
                        {
                        }
                        else
                        {
                            lblViewMonthlyBasic.Text = Convert.ToDecimal(dt.Rows[0]["BasicSalary"]).ToString("0.##");
                        }
                    }
                }
                else
                {
                    lblViewMonthlyBasic.Text = "";
                }

                if (dt.Rows[0]["HRAandCon"] != DBNull.Value)
                    lblViewMonthlyHRA.Text = Convert.ToDecimal(dt.Rows[0]["HRAandCon"]).ToString("0.##");
                else
                    lblViewMonthlyHRA.Text = "";

                if (dt.Rows[0]["PFCmpnyShare13Point61Per"] != DBNull.Value)
                    lblViewMonthlyPFCmpnyShare13Point61Per.Text = Convert.ToDecimal(dt.Rows[0]["PFCmpnyShare13Point61Per"]).ToString("0.##");
                else
                    lblViewMonthlyPFCmpnyShare13Point61Per.Text = "";

                if (dt.Rows[0]["ESIEmpShare4Point75Per"] != DBNull.Value)
                    lblViewMonthlyESIEmpShare4Point75Per.Text = Convert.ToDecimal(dt.Rows[0]["ESIEmpShare4Point75Per"]).ToString("0.##");
                else
                    lblViewMonthlyESIEmpShare4Point75Per.Text = "";

                if (!chkIsDeductPF.Checked)
                    lblViewMonthlyPFCmpnyShare13Point61Per.Text = "0";

                if (!chkIsDeductESI.Checked)
                    lblViewMonthlyESIEmpShare4Point75Per.Text = "0";

                if (txtConveyance.Text.Trim().Length > 0)
                    Total += Convert.ToDecimal(txtConveyance.Text);

                if (lblViewMonthlyBasic.Text.Trim().Length > 0)
                    Total += Convert.ToDecimal(lblViewMonthlyBasic.Text);

                if (lblViewMonthlyHRA.Text.Trim().Length > 0)
                    Total += Convert.ToDecimal(lblViewMonthlyHRA.Text);

                if (lblViewMonthlyPFCmpnyShare13Point61Per.Text.Trim().Length > 0)
                    Total += Convert.ToDecimal(lblViewMonthlyPFCmpnyShare13Point61Per.Text);

                if (lblViewMonthlyESIEmpShare4Point75Per.Text.Trim().Length > 0)
                    Total += Convert.ToDecimal(lblViewMonthlyESIEmpShare4Point75Per.Text);

                if (Total > 0)
                {
                    if (CalcAmount == Convert.ToDecimal(lblViewMonthlyGrossSalary.Text))
                    {
                        txtSpecialAllowances.Text = "0";
                    }
                    else
                    {
                        txtSpecialAllowances.Text = Convert.ToDecimal(Convert.ToDecimal(txtCTC.Text) - Convert.ToDecimal(Total)).ToString("0.##");
                    }
                }
            }
        }
    }
    protected void txtCTC_TextChanged(object sender, EventArgs e)
    {
        if (txtCTC.Text.Trim().Length > 0)
        {
            HideErrors();

            try
            {
                string SalStructureId = _HODInterviewBLL.GetSalStructureIdfromCTC(Convert.ToDecimal(txtCTC.Text));

                if (SalStructureId != "")
                {
                    ddlSalaryCalculation.SelectedValue = SalStructureId;
                    ddlSalaryCalculation_SelectedIndexChanged(null, null);
                }
            }
            catch
            {
                ShowErrors("Error", "Value Does Not Match in FromCTC To ToCTC in SalStructures.");
            }

            lblMonthlyGrossSalary.Text = lblMonthlyGrossSalary.Text.Replace("Daily", "Monthly").ToString();
            lblMonthlyBasic.Text = lblMonthlyBasic.Text.Replace("Daily", "Monthly").ToString();
            lblMonthlyHRA.Text = lblMonthlyHRA.Text.Replace("Daily", "Monthly").ToString();
            Label16.Text = Label16.Text.Replace("Daily", "Monthly").ToString();
            Label18.Text = Label18.Text.Replace("Daily", "Monthly").ToString();
        }
        else
        {
            ddlSalaryCalculation.SelectedIndex = 0;
            ddlSalaryCalculation_SelectedIndexChanged(null, null);
        }
    }
    protected void txtConveyance_TextChanged(object sender, EventArgs e)
    {
        if (txtConveyance.Text.Trim().Length > 0)
        {
            ddlSalaryCalculation_SelectedIndexChanged(ddlSalaryCalculation, new EventArgs());
        }
        else
        {
            ddlSalaryCalculation_SelectedIndexChanged(ddlSalaryCalculation, new EventArgs());
        }
    }
    protected void chkIsDeductESI_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlSalaryCalculation.SelectedIndex > 0 && txtCTC.Text.Trim().Length > 0)
        {
            lblViewMonthlyESIEmpShare4Point75Per.Text = "";
            ddlSalaryCalculation_SelectedIndexChanged(null, null);
        }
        chkIsDeductESI.Focus();
    }
    protected void chkIsDeductPF_CheckedChanged(object sender, EventArgs e)
    {
        if (ddlSalaryCalculation.SelectedIndex > 0 && txtCTC.Text.Trim().Length > 0)
        {
            lblViewMonthlyPFCmpnyShare13Point61Per.Text = "";
            ddlSalaryCalculation_SelectedIndexChanged(null, null);
        }
        chkIsDeductPF.Focus();
    }
    //private void LoadSalaryCalculationMethod()
    //{
    //    ListItem li = new ListItem();

    //    ddlSalaryCalculation.Items.Clear();

    //    li.Text = "<Select>";
    //    li.Value = "0";
    //    ddlSalaryCalculation.Items.Add(li);

    //    li = null;

    //    foreach (DataRow dtr in _HODInterviewBLL.LoadSalaryCalculationMethod().Rows)
    //    {
    //        li = new ListItem();

    //        li.Text = dtr[1].ToString();
    //        li.Value = dtr[0].ToString();
    //        ddlSalaryCalculation.Items.Add(li);

    //        li = null;
    //    }
    //}
    #endregion

    #region Question Add
    protected void btnQueAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = _HODInterviewBLL.GetQuestion(Request.QueryString["RegistrationId"].ToString());


        DataTable dtMultipleItem = new DataTable();
        dtMultipleItem.Clear();
        dtMultipleItem.Columns.Add("LnNo");
        dtMultipleItem.Columns.Add("QueTextListId");
        dtMultipleItem.Columns.Add("Text");
        dtMultipleItem.Columns.Add("ActualMarks");
        dtMultipleItem.Columns.Add("ObtainedMarks");
        dtMultipleItem.Columns.Add("HODObtainedMarks");


        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dtMultipleItem.NewRow();
            dr["LnNo"] = i + 1;
            dr["QueTextListId"] = dt.Rows[i]["TextListId"];
            dr["Text"] = dt.Rows[i]["Text"];
            dr["ActualMarks"] = dt.Rows[i]["o1"];
            dr["ObtainedMarks"] = dt.Rows[i]["HRObtainedMarks"];
            dr["HODObtainedMarks"] = string.Empty;
            dtMultipleItem.Rows.Add(dr);
        }
        Session["MultipleItem"] = dtMultipleItem;

        sbjQueryNumeric.AppendLine("<script type=\"text/javascript\">");
        sbjQueryNumeric.AppendLine("$(document).ready(function() {");

        gdvLns.Enabled = true;
        gdvLns.DataSource = dtMultipleItem;
        gdvLns.DataBind();

        sbjQueryNumeric.AppendLine(" });");
        sbjQueryNumeric.AppendLine("</script>");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "A", sbjQueryNumeric.ToString());

        pnlLn.Visible = true;
    }
    protected void gdvLns_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HODInterviewLnBLL _HODInterviewLnBLL = new HODInterviewLnBLL();

            Label lblText = (Label)e.Row.FindControl("lblText");
            Label lblTextListId = (Label)e.Row.FindControl("lblTextListId");
            Label lblActualMarks = (Label)e.Row.FindControl("lblActualMarks");
            TextBox txtMarks = (TextBox)e.Row.FindControl("txtMarks");
            TextBox txtHODMarks = (TextBox)e.Row.FindControl("txtHODMarks");

            if (_HODInterviewLnBLL.QueTextListId != null)
                lblTextListId.Text = _HODInterviewLnBLL.QueTextListId;

            if (_HODInterviewLnBLL.Text != null)
                lblText.Text = _HODInterviewLnBLL.Text;

            if (_HODInterviewLnBLL.ActualMarks != null)
                lblActualMarks.Text = Convert.ToInt32(_HODInterviewLnBLL.ActualMarks).ToString("#.##");

            if (_HODInterviewLnBLL.HRObtainedMarks != null)
                txtMarks.Text = Convert.ToInt32(_HODInterviewLnBLL.HRObtainedMarks).ToString("#.##");

            if (_HODInterviewLnBLL.HODObtainedMarks != null)
                txtHODMarks.Text = Convert.ToInt32(_HODInterviewLnBLL.HODObtainedMarks).ToString("#.##");

            sbjQueryNumeric.AppendLine("j(\"#" + txtMarks.ClientID + "\").numeric();");
            sbjQueryNumeric.AppendLine("j(\"#" + txtHODMarks.ClientID + "\").numeric();");
        }
    }
    protected void SetGridToBLL()
    {
        int i = 1;
        if (gdvLns.Rows.Count > 0)
        {
            _HODInterviewBLL.HODInterviewLnsBLL.Clear();
            foreach (GridViewRow gvrIn in gdvLns.Rows)
            {
                HODInterviewLnBLL _HODInterviewLnBLL = new HODInterviewLnBLL();

                Label lblRowNumber = (Label)gvrIn.FindControl("lblRowNumber");
                Label lblText = (Label)gvrIn.FindControl("lblText");
                Label lblTextListId = (Label)gvrIn.FindControl("lblTextListId");
                Label lblActualMarks = (Label)gvrIn.FindControl("lblActualMarks");
                TextBox txtMarks = (TextBox)gvrIn.FindControl("txtMarks");
                TextBox txtHODMarks = (TextBox)gvrIn.FindControl("txtHODMarks");

                _HODInterviewLnBLL.LnNo = i;
                _HODInterviewLnBLL.QueTextListId = lblTextListId.Text.Trim();

                if (lblActualMarks.Text.Trim().Length > 0)
                    _HODInterviewLnBLL.ActualMarks = Convert.ToInt32(lblActualMarks.Text);
                else
                    _HODInterviewLnBLL.ActualMarks = null;

                if (txtMarks.Text.Trim().Length > 0)
                    _HODInterviewLnBLL.HRObtainedMarks = Convert.ToInt32(txtMarks.Text);
                else
                    _HODInterviewLnBLL.HRObtainedMarks = null;

                if (txtHODMarks.Text.Trim().Length > 0)
                    _HODInterviewLnBLL.HODObtainedMarks = Convert.ToInt32(txtHODMarks.Text);
                else
                    _HODInterviewLnBLL.HODObtainedMarks = null;

                _HODInterviewBLL.HODInterviewLnsBLL.Add(_HODInterviewLnBLL);
                i++;
            }
        }
    }
    #endregion
}