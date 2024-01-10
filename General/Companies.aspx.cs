using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;

public partial class General_Companies : System.Web.UI.Page
{
    CompaniesBLL _companiesBLL = new CompaniesBLL();
    private GeneralBLL _GeneralBLL = new GeneralBLL();

    private StringBuilder sbjQueryNumeric = new StringBuilder();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HideErrors();
            //MySession.EFDR = 2;

            if (!Page.IsPostBack)
            {
                string[] path = Request.AppRelativeCurrentExecutionFilePath.Split('/');
                _GeneralBLL.FormName = path[path.Length - 1];

                txtName.Focus();
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }

   
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        //Set Null or Clear Existing Details in BLL
        _companiesBLL.Name = null;
      
        //Set Filter details to BLL
        //Name
        if (txtName.Text.Trim().Length > 0)
            _companiesBLL.Name = txtName.Text.Trim();

        DataTable dt = new DataTable();
        dt = _companiesBLL.Companies();
        gdvCompanies.DataSource = dt;

        lblRecordStatus.Text = "Total No. Of Companys : [ " + dt.Rows.Count.ToString() + " ]";

        //gdvCompanies.DataSource = _companiesBLL.Companies();

        sbjQueryNumeric.AppendLine("<script type='text/javascript'>");
        sbjQueryNumeric.AppendLine(" $(document).ready(function() {");

        gdvCompanies.DataBind();

        sbjQueryNumeric.AppendLine(" });");
        sbjQueryNumeric.AppendLine("</script>");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "A", sbjQueryNumeric.ToString());
    }

    protected void gdvCompanies_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[1].Controls[0];

            hl.NavigateUrl = "Company.aspx?CompanyId=" + drv[0].ToString();
            HyperLink hlViewHistory = (HyperLink)e.Row.FindControl("hlviewHistory");


            string Url = "";

            Url = "../General/MasterHistory.aspx?FormName=Company&TableName=Company&PrimaryKey=CompanyId&RecordId=" + drv[0].ToString() + "&IsMaster=true";

            sbjQueryNumeric.AppendLine(" $(\"#" + hlViewHistory.ClientID + "\").click(function(e) {");
            sbjQueryNumeric.AppendLine("myFunc(e,'" + Url + "');");
            sbjQueryNumeric.AppendLine("});");
        }
    }
    protected void gdvCompanies_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //FilterCriteria();
        //_DealersBLL.AllRecords = true;

        gdvCompanies.PageIndex = e.NewPageIndex;
        gdvCompanies.DataSource = _companiesBLL.Companies();
       
        sbjQueryNumeric.AppendLine("<script type='text/javascript'>");
        sbjQueryNumeric.AppendLine(" $(document).ready(function() {");

        gdvCompanies.DataBind();

        sbjQueryNumeric.AppendLine(" });");
        sbjQueryNumeric.AppendLine("</script>");
        ClientScript.RegisterClientScriptBlock(this.GetType(), "A", sbjQueryNumeric.ToString());
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

    protected void imgExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            HideErrors();
            if (txtName.Text.Trim().Length > 0)
                _companiesBLL.Name = txtName.Text.Trim();

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
            HideErrors();
            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            DataGrid dgGrid = new DataGrid();

            DataTable dtExport = new DataTable();
            //_bomSubGrpsBLL.AllRecords = true;
            dtExport = _companiesBLL.ExportToExcel();

            DataView dv = new DataView(dtExport);
            dgGrid.DataSource = dv;

            hw.WriteLine("<b><u><font size='4'>School Details</font></u></b></br>");

            hw.WriteLine("<b><u><font size='3'> Report Date: " + DateTime.Today.ToString("dd-MMM-yyyy") + " </font></br>");

            hw.WriteLine("<b><u><font size='3'> Report Criteria </font></br>");


            if (txtName.Text.Trim().Length > 0)
                hw.WriteLine("<b><u><font size='3'>Name: " + txtName.Text + " </font></br>");

            dgGrid.DataBind();
            dgGrid.RenderControl(hw);
            string attachment = "attachment; filename=Schools.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            Response.Write(tw.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }

}
