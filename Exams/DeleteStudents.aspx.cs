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

public partial class Exams_DeleteStudents : System.Web.UI.Page
{
    private RegistrationsBLL _registrationsBLL = new RegistrationsBLL();
    private RegistrationBLL _registrationBLL = new RegistrationBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadStandard();
            btnShowAllRecords.Visible = false;
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {

        if (ddlStandardTextListId.SelectedIndex > 0)
            _registrationsBLL.StandardId = ddlStandardTextListId.SelectedValue;
        else
            _registrationsBLL.StandardId = null;

        if (txtRegistrationName.Text.Trim().Length > 0)
            _registrationsBLL.RegistrationName = txtRegistrationName.Text.Trim();
        else
            _registrationsBLL.RegistrationName = null;

        if (txtSchoolName.Text.Trim().Length > 0)
            _registrationsBLL.SchoolName = txtSchoolName.Text.Trim();
        else
            _registrationsBLL.SchoolName = null;

        if (txtCity.Text.Trim().Length > 0)
            _registrationsBLL.City = txtCity.Text.Trim();
        else
            _registrationsBLL.City = null;

        _registrationsBLL.AllRecord = true;

        if (ddlStandardTextListId.SelectedIndex > 0)
        {
            DataTable dt = new DataTable();
            dt = _registrationsBLL.Filter();
            gdvStudents.DataSource = dt;

            if (dt.Rows.Count >= 30)
            {
                btnShowAllRecords.Visible = true;
                lblRecordStatus.Text = "Top  [ " + dt.Rows.Count.ToString() + " ]" + " Records ";
            }
            else
            {
                btnShowAllRecords.Visible = false;
                lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";
            }

            gdvStudents.DataBind();
        }
        else
        {
            ShowErrors("err", "Select mandatory field for filter data.");
        }
    }

    protected void btnShowAllRecords_Click(object sender, EventArgs e)
    {
        if (ddlStandardTextListId.SelectedIndex > 0)
            _registrationsBLL.StandardId = ddlStandardTextListId.SelectedValue;
        else
            _registrationsBLL.StandardId = null;

        if (txtRegistrationName.Text.Trim().Length > 0)
            _registrationsBLL.RegistrationName = txtRegistrationName.Text.Trim();
        else
            _registrationsBLL.RegistrationName = null;

        if (txtSchoolName.Text.Trim().Length > 0)
            _registrationsBLL.SchoolName = txtSchoolName.Text.Trim();
        else
            _registrationsBLL.SchoolName = null;

        if (txtCity.Text.Trim().Length > 0)
            _registrationsBLL.City = txtCity.Text.Trim();
        else
            _registrationsBLL.City = null;

        _registrationsBLL.AllRecord = true;

        DataTable dt = new DataTable();
        dt = _registrationsBLL.Filter();
        gdvStudents.DataSource = dt;

        lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";

        gdvStudents.DataBind();

    }

    protected void gdvStudents_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[0].Controls[0];

            hl.NavigateUrl = "Registration.aspx?RegistrationId=" + drv[0].ToString();
            CheckBox chkSendPendingFeeSMS = (CheckBox)e.Row.FindControl("chkSendPendingFeeSMS");
            CheckBox chkSendPendingFeeSMSAll = (CheckBox)gdvStudents.HeaderRow.FindControl("chkSendPendingFeeSMSAll");

            chkSendPendingFeeSMS.Attributes.Add("onclick", "javascript:Selectchildcheckboxes('" + chkSendPendingFeeSMSAll.ClientID + "','" + gdvStudents.ClientID + "')");
        }
    }

    private void ShowErrors(string key, string value)
    {
        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();
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
            dt = _registrationsBLL.LoadStandard();

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
  
    protected void btnDeleteAll_Click(object sender, EventArgs e)
    {
        try
        {
            ArrayList al = new ArrayList();

            foreach (GridViewRow gvr in gdvStudents.Rows)
            {
                CheckBox chkSendPendingFeeSMS = (CheckBox)gvr.FindControl("chkSendPendingFeeSMS");

                if (chkSendPendingFeeSMS.Checked)
                    al.Add(gdvStudents.DataKeys[gvr.RowIndex].Values[0].ToString());
            }

            if (al.Count > 0)
            {
                _registrationBLL.DeleteRegistration(al);
                ShowErrors("err", "Selected Student is deleted successfully.");

                btnFilter_Click(null, null);
            }
            else
            {
                ShowErrors("err", "Please select atleast one Student to delete record.");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }
}

