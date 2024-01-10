using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Exams_ExamSummaryDetailRpt : System.Web.UI.Page
{
    private ExamSummaryRptBLL _ExamSummaryRptBLL = new ExamSummaryRptBLL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["StandardId"] != null)
                _ExamSummaryRptBLL.StandardId = Request.QueryString["StandardId"].ToString();
            else
                _ExamSummaryRptBLL.StandardId = null;

            if (Request.QueryString["SubjectId"] != null)
                _ExamSummaryRptBLL.SubjectId = Request.QueryString["SubjectId"].ToString();
            else
                _ExamSummaryRptBLL.SubjectId = null;

            if (Request.QueryString["TestId"] != null)
                _ExamSummaryRptBLL.TestId = Request.QueryString["TestId"].ToString();
            else
                _ExamSummaryRptBLL.TestId = null;

            if (Request.QueryString["Standard"] != null)
                lblStdName.Text = Request.QueryString["Standard"].ToString();
            else
                lblStdName.Text = "";

            if (Request.QueryString["Subject"] != null)
                lblSubName.Text = Request.QueryString["Subject"].ToString();
            else
                lblSubName.Text = "";

            if (Request.QueryString["TestName"] != null)
                lblTestName.Text = Request.QueryString["TestName"].ToString();
            else
                lblTestName.Text = "";

            if (Request.QueryString["ExamScheduleId"] != null)
                _ExamSummaryRptBLL.ExamScheduleId = Request.QueryString["ExamScheduleId"].ToString();
            else
                _ExamSummaryRptBLL.ExamScheduleId = null;

            if (Request.QueryString["Schedule"] != null)
                lblScheduleName.Text = Request.QueryString["Schedule"].ToString();
            else
                lblScheduleName.Text += "";

            DataTable dt = new DataTable();
            dt = _ExamSummaryRptBLL.ExamDetail();
            gdvExamDetail.DataSource = dt;
            lblRecordStatus.Text = "Total No. Of Records : [ " + dt.Rows.Count.ToString() + " ]";
            gdvExamDetail.DataBind();
        }
    }

    #region Grid Row Data Bound
    protected void gdvExamDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    #endregion
}