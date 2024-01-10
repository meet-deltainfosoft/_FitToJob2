using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Forms : System.Web.UI.Page
{
    FormsBLL _formsBLL = new FormsBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            LoadModules();
        }
    }

    private void LoadModules()
    {
        ListItem li = new ListItem();

        ddlModule.Items.Clear();

        li.Text = "<Select>";
        li.Value = "0";
        ddlModule.Items.Add(li);

        li = null;

        foreach (DataRow dtr in _formsBLL.Modules().Rows)
        {
            li = new ListItem();

            li.Text = dtr[1].ToString();
            li.Value = dtr[0].ToString();
            ddlModule.Items.Add(li);

            li = null;
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        //ModuleTextListId
        if (ddlModule.SelectedIndex > 0)
            _formsBLL.ModuleTextListId = ddlModule.SelectedValue;
        else
            _formsBLL.ModuleTextListId = null;

        //Name
        if (txtName.Text.Trim().Length > 0)
            _formsBLL.Name = txtName.Text;
        else
            _formsBLL.Name = null;

        //Bind indents to gridview
        gdvForms.DataSource = _formsBLL.Forms;
        gdvForms.DataBind();
    }

    protected void gdvForms_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[0].Controls[0];

            hl.NavigateUrl = "Form.aspx?FormId=" + drv[0].ToString();
        }
    }

}
