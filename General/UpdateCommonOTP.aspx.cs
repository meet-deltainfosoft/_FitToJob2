using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class General_UpdateCommonOTP : System.Web.UI.Page
{
    private ExamScheduleBLL _examScheduleBLL = new ExamScheduleBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();

            dt = _examScheduleBLL.CommonOTP();

            if (dt.Rows.Count > 0)
            {
                hfID.Value = dt.Rows[0]["FacetsId"].ToString();
                txtOTP.Text = dt.Rows[0]["FacetText"].ToString();
            }
            else
            {
                Response.Write("no otp found");
            }

            dt = new DataTable();

            dt = _examScheduleBLL.CommonOTPTeacher();

            if (dt.Rows.Count > 0)
            {
                hfOTPTeacher.Value = dt.Rows[0]["FacetsId"].ToString();
                txtOTPTeacher.Text = dt.Rows[0]["FacetText"].ToString();
            }
            else
            {
                Response.Write("no otp found");
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtOTP.Text.Trim().Length > 0 && hfID.Value.Trim().Length > 0)
        {
            _examScheduleBLL.UpdateCommonOTP(txtOTP.Text.Trim().ToString(), hfID.Value.ToString().Trim());
        }
        else
        {
            Response.Write("parameter is not ok as per requirement");
        }

        if (txtOTPTeacher.Text.Trim().Length > 0 && hfOTPTeacher.Value.Trim().Length > 0)
        {
            _examScheduleBLL.UpdateCommonOTP(txtOTPTeacher.Text.Trim().ToString(), hfOTPTeacher.Value.ToString().Trim());
        }
        else
        {
            Response.Write("parameter is not ok as per requirement");
        }

        Response.Redirect("~/General/UpdateCommonOTP.aspx");
    }
}