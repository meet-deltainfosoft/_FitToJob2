using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

public partial class General_MasterValues : System.Web.UI.Page
{
    MasterValuesBLL masterValuesBLL = new MasterValuesBLL();
    private StringBuilder sbjQueryNumeric = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            LoadGroups();
            ddlGroup.Focus();
        }
    }

    private void LoadGroups()
    {
        ListItem li = new ListItem();

        ddlGroup.Items.Clear();

        li.Text = "<Select>";
        li.Value = "0";
        ddlGroup.Items.Add(li);

        li = null;

        foreach (DataRow dtr in masterValuesBLL.Groups().Rows)
        {
            li = new ListItem();

            li.Text = dtr[1].ToString();
            li.Value = dtr[0].ToString();
            ddlGroup.Items.Add(li);

            li = null;
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        divMasterValues.Visible = true;
        //Set Null or Clear Existing Details in BLL
        masterValuesBLL.Group = null;

        //Set Filter details to BLL
        //Group
        if (ddlGroup.SelectedIndex > 0)
            masterValuesBLL.Group = ddlGroup.SelectedValue;
        //Set Filter details to BLL
        DataTable dt = new DataTable();
        dt = masterValuesBLL.MasterValues();
        gdvMasterValues.DataSource = dt;

        //Set Datasource to Grid from BLL
        //  gdvMasterValues.DataSource = masterValuesBLL.MasterValues();

        lblRecordStatus.Text = "Total No. Of Master Values : [ " + dt.Rows.Count.ToString() + " ]";

        sbjQueryNumeric.AppendLine("<script type='text/javascript'>");
        sbjQueryNumeric.AppendLine(" $(document).ready(function() {");

        gdvMasterValues.DataBind();

        sbjQueryNumeric.AppendLine(" });");
        sbjQueryNumeric.AppendLine("</script>");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "A", sbjQueryNumeric.ToString());
    }

    protected void gdvMasterValues_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[1].Controls[0];

            hl.NavigateUrl = "MasterValue.aspx?TextListId=" + drv[0].ToString();


            HyperLink hlViewHistory = (HyperLink)e.Row.FindControl("hlviewHistory");

            string Url = "";

            Url = "../General/MasterHistory.aspx?FormName=TextLists &TableName=TextLists&PrimaryKey=TextListId &RecordId=" + drv[0].ToString() + "&IsMaster=true";
            sbjQueryNumeric.AppendLine(" $(\"#" + hlViewHistory.ClientID + "\").click(function(e) {");
            sbjQueryNumeric.AppendLine("myFunc(e,'" + Url + "');");
            sbjQueryNumeric.AppendLine("});");
        }
    }

    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnFilter_Click(null, null);
    }

    protected void gdvMasterValues_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            btnFilter_Click(null, null);
            gdvMasterValues.PageIndex = e.NewPageIndex;
            gdvMasterValues.DataSource = masterValuesBLL.MasterValues();

            sbjQueryNumeric.AppendLine("<script type='text/javascript'>");
            sbjQueryNumeric.AppendLine(" $(document).ready(function() {");

            gdvMasterValues.DataBind();

            sbjQueryNumeric.AppendLine(" });");
            sbjQueryNumeric.AppendLine("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "A", sbjQueryNumeric.ToString());
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlGroup.SelectedIndex > 0)
                masterValuesBLL.Group = ddlGroup.SelectedValue;

            //    masterValuesBLL.AllRecords = true;
            ExportToExcel();
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }

    protected void ExportToExcel()
    {
        try
        {
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();

            DataTable dtExport = new DataTable();

            dtExport = masterValuesBLL.ExportToExcel();
            DataView dv = new DataView(dtExport);
            //dgGrid.DataSource = dtExport;
            dgGrid.DataSource = dv;

            hw.WriteLine("<b><u><font size='5'> Master Values Report </font></u></b>");
            dgGrid.DataBind();
            dgGrid.RenderControl(hw);
            dgGrid.CssClass = "formBody";
            dgGrid.HeaderStyle.Font.Bold = true;
            dgGrid.ItemStyle.CssClass = "formBody";
            string attachment = "attachment; filename=MasterValues.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dgGrid.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();
    }
    private void ShowErrors(string key, string value)
    {
        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));
    }
    protected void btnShowAllRecords_Click(object sender, EventArgs e)
    {
        try
        {
            divMasterValues.Visible = true;
            //Set Null or Clear Existing Details in BLL
            masterValuesBLL.Group = null;

            //Set Filter details to BLL
            //Group
            if (ddlGroup.SelectedIndex > 0)
                masterValuesBLL.Group = ddlGroup.SelectedValue;
            //Set Filter details to BLL
            DataTable dt = new DataTable();
            dt = masterValuesBLL.MasterValues();
            gdvMasterValues.DataSource = dt;

            //Set Datasource to Grid from BLL
            //  gdvMasterValues.DataSource = masterValuesBLL.MasterValues();

            lblRecordStatus.Text = "Total No. Of Master Values : [ " + dt.Rows.Count.ToString() + " ]";

            sbjQueryNumeric.AppendLine("<script type='text/javascript'>");
            sbjQueryNumeric.AppendLine(" $(document).ready(function() {");

            gdvMasterValues.DataBind();

            sbjQueryNumeric.AppendLine(" });");
            sbjQueryNumeric.AppendLine("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "A", sbjQueryNumeric.ToString());
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }
}
