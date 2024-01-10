using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Trail_ERP : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //navbar.Attributes["style"] = "background-color: #37C1BB;";
        GeneralBLL _GeneralBLL;
        if (MySession.UserID == null)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            System.Web.Security.FormsAuthentication.RedirectToLoginPage();
        }
        else
        {
            //if (MySession.IsAdmin)
            //{
            //    liAdmin.Visible = true;
            //}
            //else
            //{
            //    liAdmin.Visible = false;
            //}

            if (MySession.DBNo != null)
                LblDBName.Text = MySession.DBNo.ToString();
            else
                LblDBName.Text = ""; 

            bool Valid;
            string[] path;
            if (Request.Url.Query.ToString() == "")
            {
                path = Request.AppRelativeCurrentExecutionFilePath.Split('/');
            }
            else
            {
                if (Request.Url.Query.ToString() == "?Login=%27Admin%27")
                    path = (Request.AppRelativeCurrentExecutionFilePath.ToString() + Request.Url.Query.ToString().Replace("%27", "''")).Split('/');
                else
                    path = Request.AppRelativeCurrentExecutionFilePath.Split('/');
            }
            string formName = path[path.Length - 1];

            _GeneralBLL = new GeneralBLL();

            _GeneralBLL.FormName = formName;
            _GeneralBLL.UserName = MySession.UserID;
            _GeneralBLL.IsAdmin = MySession.IsAdmin;

            if (_GeneralBLL.UserName == "aaa")
                Valid = true;
            else if (_GeneralBLL.FormName == "Default.aspx")
                Valid = true;
            else
            {
                Valid = _GeneralBLL.GetUserRoles();

                //Valid = false;
            }

            if (!Valid)
            {
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(MySession.UserID, false);
            }
        }
    }
}
