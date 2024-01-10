using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Admin_Users : System.Web.UI.Page
{
    UsersBLL _usersBLL = new UsersBLL();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        //FirstName
        if (txtFirstName.Text.Trim().Length > 0)
            _usersBLL.FirstName = txtFirstName.Text;
        else
            _usersBLL.FirstName = null;

        //LastName
        if (txtLastName.Text.Trim().Length > 0)
            _usersBLL.LastName = txtLastName.Text;
        else
            _usersBLL.LastName = null;

        //UserName
        if (txtUserName.Text.Trim().Length > 0)
            _usersBLL.UserName = txtUserName.Text;
        else
            _usersBLL.UserName = null;

        //Bind indents to gridview
        gdvUsers.DataSource = _usersBLL.Users;
        gdvUsers.DataBind();
    }
    protected void gdvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            HyperLink hl = (HyperLink)e.Row.Cells[0].Controls[0];

            hl.NavigateUrl = "User.aspx?UserId=" + drv[0].ToString();
        }
    }
}
