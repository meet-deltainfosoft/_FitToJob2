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

public partial class General_Batch : System.Web.UI.Page
{
    BatchBLL _BatchBLL = new BatchBLL();

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsPostBack == false)
            {
                if (Request.QueryString["BatchId"] == null)
                {
                    
                    _BatchBLL= new BatchBLL();
                }
                else
                {
                    _BatchBLL = new BatchBLL(Request.QueryString["BatchId"].ToString());
                }
                Session["_BatchBLL"] = _BatchBLL;
            }
            else
            {
                _BatchBLL = (BatchBLL)Session["_BatchBLL"];
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsPostBack)
            {
                LoadStandard();
                if (Request.QueryString["BatchId"] != null)
                {
                    lblTitle.Text = " - [Edit Mode]";
                    LoadWebForm();
                    btnDelete.Enabled = true;
                    btnOK.Visible = true;
                }
                else
                {
                   

                }
            }
            else
            {
                HideErrors();
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        lblStandard.CssClass = "";
        ddlStandard.CssClass = "";

    }

    private void ShowErrors(string key, string value)
    {
        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));

        if (key == "StandardTextListId")
        {
            lblStandard.CssClass = "error";
            ddlStandard.CssClass = "error";
        }
        
    }
    private void LoadStandard()
    {
        try
        {
            ListItem li = new ListItem();

            ddlStandard.Items.Clear();

            li.Text = "<Select>";
            li.Value = "0";
            ddlStandard.Items.Add(li);

            li = null;

            DataTable dt = new DataTable();
            dt = _BatchBLL.LoadStandard();

            foreach (DataRow dtr in dt.Rows)
            {
                li = new ListItem();

                li.Text = dtr[1].ToString();
                li.Value = dtr[0].ToString();
                ddlStandard.Items.Add(li);

                li = null;
            }
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message);
        }
    }
    
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlStandard.SelectedIndex > 0)
                _BatchBLL.StandardTextListId = ddlStandard.SelectedValue;
            else
                _BatchBLL.StandardTextListId = null;

            if (txtBatchName.Text.Trim().Length > 0)
                _BatchBLL.BatchName = txtBatchName.Text.Trim();
            else
                _BatchBLL.BatchName = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                _BatchBLL.Save();

                if (Request.QueryString["BatchId"] == null)
                {
                    Reset();
                    Session["_BatchBLL"] = null;
                    Session["_BatchBLL"] = new BatchBLL();
                    _BatchBLL = (BatchBLL)Session["_BatchBLL"];
                    ShowErrors("Success", "Last Record Saved Successfully.");
                }
                else
                {
                    Session["_BatchBLL"] = null;
                    Response.Redirect("Batchs.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            Session["_BatchBLL"] = null;

            if (Request.QueryString["BatchId"] == null)
            {
                Response.Redirect("~/General/Default.aspx");
            }
            else
            {
                Response.Redirect("Batchs.aspx");
            }
        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _BatchBLL.Delete(Request.QueryString["BatchId"]);
            Session["_BatchBLL"] = null;
            Response.Redirect("Batchs.aspx");
        }
        catch (Exception ex)
        {

            ShowErrors("Error", ex.Message);
        }
    }

    private bool Validate()
    {
        try
        {
            HideErrors();

            SortedList sl = _BatchBLL.Validate();

            if (sl.Count > 0)
            {
                for (int i = 0; i < sl.Count; i++)
                {
                    string key = (string)sl.GetKey(i);
                    string value = (string)sl[key];

                    ShowErrors(key, value);
                }
            }
            return (sl.Count == 0) ? true : false;
        }
        catch (Exception ex)
        {
            return false;
            ShowErrors("", ex.Message.ToString());
        }
    }

    private void Reset()
    {
        try
        {
            ddlStandard.SelectedIndex = 0;
            txtBatchName.Text = "";
        }
        catch (Exception ex)
        {
            ShowErrors("err", ex.Message.ToString());
        }
    }

 
    private void LoadWebForm()
    {
        try
        {
            if (_BatchBLL.StandardTextListId != null)
            {
                ddlStandard.SelectedValue = _BatchBLL.StandardTextListId.ToString();
            }

            if (_BatchBLL.BatchName != null)
                txtBatchName.Text = _BatchBLL.BatchName.ToString();

        }
        catch (Exception ex)
        {
            ShowErrors("", ex.Message.ToString());
        }
    }
}