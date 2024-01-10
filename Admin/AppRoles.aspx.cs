using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_AppRoles : System.Web.UI.Page
{
    AppRolesBLL _appRolesBLL = new AppRolesBLL();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        //Name
        if (txtName.Text.Trim().Length > 0)
            _appRolesBLL.Name = txtName.Text;
        else
            _appRolesBLL.Name = null;

        //Bind indents to gridview
        gdvAppRoles.DataSource = _appRolesBLL.AppRoles;
        gdvAppRoles.DataBind();

    }
    protected void gdvAppRoles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[0].Controls[0];

            hl.NavigateUrl = "AppRole.aspx?AppRoleId=" + drv[0].ToString();
        }
    }
}
