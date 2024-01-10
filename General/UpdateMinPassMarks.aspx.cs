using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class General_UpdateMinPassMarks : System.Web.UI.Page
{
    private ExamScheduleBLL _examScheduleBLL = new ExamScheduleBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = new DataTable();

            dt = _examScheduleBLL.MinimumMarks();

            if (dt.Rows.Count > 0)
            {
                hfID.Value = dt.Rows[0]["FacetsId"].ToString();
                txtMinPassMarks.Text = dt.Rows[0]["FacetText"].ToString();
            }
            else
            {
                Response.Write("no Minimum Marks found");
            }
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (txtMinPassMarks.Text.Trim().Length > 0 && hfID.Value.Trim().Length > 0)
        {
            _examScheduleBLL.UpdateMinMarks(txtMinPassMarks.Text.Trim().ToString(), hfID.Value.ToString().Trim());
            Response.Redirect("~/General/UpdateMinPassmarks.aspx");
        }
        else
        {
            Response.Write("parameter is not ok as per requirement");
        }
    }
}