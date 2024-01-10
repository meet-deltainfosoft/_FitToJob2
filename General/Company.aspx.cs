using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.IO;
using System.Configuration;

public partial class General_Company : System.Web.UI.Page
{
    CompanyBLL _CompanyBLL;
    private GeneralBLL _GeneralBLL = new GeneralBLL();

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            if (Request.QueryString["CompanyId"] == null)
            {
                _CompanyBLL = new CompanyBLL();
            }
            else
            {
                _CompanyBLL = new CompanyBLL(Request.QueryString["CompanyId"].ToString());
            }

            Session["_CompanyBLL"] = _CompanyBLL;
        }
        else
        {
            _CompanyBLL = (CompanyBLL)Session["_CompanyBLL"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            HideErrors();

            //if (Request.QueryString["CompanyId"] != null) //Edit Mode
            //    MySession.EFDR = 2;
            //else
            //    MySession.EFDR = 1;

            if (Page.IsPostBack == false)
            {
                HideErrors();
                string[] path = Request.AppRelativeCurrentExecutionFilePath.Split('/');
                _GeneralBLL.FormName = path[path.Length - 1];

                //Load Ddl
                loadCrncys();
                // LoadLocs();
                //  LoadDivision();
                LoadCountry();
                LoadAccountType();

                if (Request.QueryString["CompanyId"] != null) //Edit Mode
                {
                    lblTitle.Text = " - [Edit Mode]";
                    LoadWebForm();
                    btnDelete.Text = "Delete";
                    btnOK.Text = "Edit";
                    if (MySession.UserID == "aaa")
                    {
                        btnDelete.Enabled = true;
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnDelete.Enabled = false;
                        btnDelete.Visible = false;
                    }
                }
                else //New Mode
                {
                    if (MySession.DivTextListId != null)
                    {
                        ddlDiv.SelectedValue = MySession.DivTextListId.ToString();    // ToLower() Remove by jk
                    }
                    //txtDt.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                    txtName.Focus();
                    btnDelete.Visible = false;
                    btnOK.Text = "Add";
                    ddlCrncy.SelectedValue = "23003084-6f0c-495c-9a35-4ca1ae6cd0ce";
                }
            }
            // ddlDiv.Enabled = false;
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("error", ex.Message);
        }
    }

    #endregion

    #region "Company Events"

    //protected void txtItmName_TextChanged(object sender, EventArgs e)
    //{
    //    //Set user entered Itmname to LnBLL, It will fetch the details if valid Item Name otherwise throw exception of the code is Invalid
    //    if (txtItmName.Text.Trim().Length > 0)
    //    {
    //        _CompanyBLL.ItmName = txtItmName.Text.Trim();
    //        // hfItmId.Value = _QuotationLnBLL.ItmId;

    //        //Set the details to controls

    //        SetCustomerComplainItmDetails(_CompanyBLL);

    //    }
    //    else
    //    {
    //        _CompanyBLL.ItmName = null;
    //        hfItmId.Value = "";
    //    }
    //}

    // protected void hfVendorCode_OnValueChanged(object sender, EventArgs e)
    //     {
    //     if (hfVendorCode.Value.Length > 0)
    //     {
    //         _CompanyBLL.VendorLgrId = hfVendorCode.Value;
    //         LoadContactPersonNames();
    //     }
    //     else
    //     {
    //         _CompanyBLL.VendorLgrId = null;
    //     }

    //     //Set Vendor Name 
    //     _CompanyBLL.VendorName = txtVendorName.Text;
    //}

    // protected void hfLgrAddressId_OnValueChanged(object sender, EventArgs e)
    // {
    //     if (hfLgrAddressId.Value == "")
    //     {
    //        // loadVendorLgrAddresses();
    //     }
    //     if (hfLgrAddressId.Value.Length > 0)
    //     {
    //         _CompanyBLL.VendorLgrId = hfVendorCode.Value;
    //     LoadContactPersonNames();
    // }
    //     else
    //         _CompanyBLL.VendorLgrId = null;      
    //}

    // protected void ddlContactPerson_SelectedIndexChanged(object sender, EventArgs e)
    // {
    //     if (ddlContactPerson.SelectedIndex > 0)
    //     {
    //         _CompanyBLL.VendorContactPerson = ddlContactPerson.SelectedItem.Text;
    //     }
    //     else
    //     {
    //         _CompanyBLL.VendorContactPerson = null;
    //     }
    //     txtContactPerson.Text = _CompanyBLL.VendorContactPerson;

    //     ddlContactPerson.Focus();
    // }
    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            //Name
            if (txtName.Text.Trim() != "")
                _CompanyBLL.Name = txtName.Text.Trim();
            else
                _CompanyBLL.Name = null;


            //Addressline1
            if (txtAddressLine1.Text.ToString().Length > 0)
                _CompanyBLL.AddressLine1 = txtAddressLine1.Text.Trim();
            else
                _CompanyBLL.AddressLine1 = null;

            //Addressline2
            if (txtAddressLine2.Text.ToString().Length > 0)
                _CompanyBLL.AddressLine2 = txtAddressLine2.Text.Trim();
            else
                _CompanyBLL.AddressLine2 = null;

            //City
            if (ddlCity.SelectedIndex > 0)
            {
                _CompanyBLL.City = ddlCity.SelectedValue;
                _CompanyBLL.CityName = ddlCity.SelectedItem.Text;
            }
            else
            {
                _CompanyBLL.City = null;
                _CompanyBLL.CityName = null;
            }
            if (ddlState.SelectedIndex > 0)
            {
                _CompanyBLL.State = ddlState.SelectedValue;
                _CompanyBLL.StateName = ddlState.SelectedItem.Text;
            }
            else
            {
                _CompanyBLL.State = null;
                _CompanyBLL.StateName = null;
            }

            //CrncyId
            if (ddlCrncy.SelectedIndex > -1)
                _CompanyBLL.CrncyId = ddlCrncy.SelectedValue;
            else
                _CompanyBLL.CrncyId = null;

            //PINCode
            if (txtPinCode.Text.ToString().Length > 0)
                _CompanyBLL.PINCode = txtPinCode.Text.Trim();
            else
                _CompanyBLL.PINCode = null;


            //TelephoneNos
            if (txtTelephoneNos.Text.ToString().Length > 0)
                _CompanyBLL.TelephoneNos = txtTelephoneNos.Text.Trim();
            else
                _CompanyBLL.TelephoneNos = null;


            //FaxNos
            if (txtFaxNos.Text.ToString().Length > 0)
                _CompanyBLL.FaxNos = txtFaxNos.Text.Trim();
            else
                _CompanyBLL.FaxNos = null;

            //EmailId
            if (txtEmailId.Text.ToString().Length > 0)
                _CompanyBLL.EmailId = txtEmailId.Text.Trim();
            else
                _CompanyBLL.EmailId = null;

            //PanNo
            if (txtPanNo.Text.ToString().Length > 0)
                _CompanyBLL.PanNo = txtPanNo.Text.Trim();
            else
                _CompanyBLL.PanNo = null;

            //TINLSTNo
            if (txtTINLSTNo.Text.ToString().Length > 0)
                _CompanyBLL.TINLSTNo = txtTINLSTNo.Text.Trim();
            else
                _CompanyBLL.TINLSTNo = null;


            //TINLSTNo
            if (txtTINCSTNo.Text.ToString().Length > 0)
                _CompanyBLL.TINCSTNo = txtTINCSTNo.Text.Trim();
            else
                _CompanyBLL.TINCSTNo = null;

            //VATNo
            if (txtVATNo.Text.ToString().Length > 0)
                _CompanyBLL.VATNo = txtVATNo.Text.Trim();
            else
                _CompanyBLL.VATNo = null;

            //Website
            if (txtWebsite.Text.ToString().Length > 0)
                _CompanyBLL.Website = txtWebsite.Text.Trim();
            else
                _CompanyBLL.Website = null;

            //MobileNo
            if (txtMobileNos.Text.ToString().Length > 0)
                _CompanyBLL.MobileNo = txtMobileNos.Text.Trim();
            else
                _CompanyBLL.MobileNo = null;

            if (ddlCountry.SelectedIndex > 0)
            {
                _CompanyBLL.Country = ddlCountry.SelectedValue;
                _CompanyBLL.CountryName = ddlCountry.SelectedItem.Text;
            }
            else
            {
                _CompanyBLL.Country = null;
                _CompanyBLL.CountryName = null;
            }

            if (fuLogo.PostedFile != null && fuLogo.PostedFile.FileName != "")
            {
                byte[] imageSize = new byte[fuLogo.PostedFile.ContentLength];
                HttpPostedFile uploadedImage = fuLogo.PostedFile;
                uploadedImage.InputStream.Read(imageSize, 0, (int)fuLogo.PostedFile.ContentLength);
                string imgName = fuLogo.PostedFile.FileName;

                string[] getExtenstion = fuLogo.PostedFile.FileName.Split('.');
                string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

                _CompanyBLL.Logo = imageSize;
                _CompanyBLL.LogoName = imgName;
                _CompanyBLL.LogoPath = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "." + oExtension;
                uploadedImage.SaveAs(_CompanyBLL.LogoPath);
            }
            else
            {
                _CompanyBLL.Logo = null;
                _CompanyBLL.LogoName = null;
                _CompanyBLL.LogoPath = null;
            }

            if (txtExciseRegNo.Text.Trim().Length > 0)
                _CompanyBLL.ExciseRegNo = txtExciseRegNo.Text.Trim();
            else
                _CompanyBLL.ExciseRegNo = null;

            if (txtServiceTaxRegn.Text.Trim().Length > 0)
                _CompanyBLL.ServiceTaxRegn = txtServiceTaxRegn.Text.Trim();
            else
                _CompanyBLL.ServiceTaxRegn = null;


            if (txtRange.Text.Trim().Length > 0)
                _CompanyBLL.RangeDetail = txtRange.Text.Trim();
            else
                _CompanyBLL.RangeDetail = null;


            if (txtDivision.Text.Trim().Length > 0)
                _CompanyBLL.Division = txtDivision.Text.Trim();
            else
                _CompanyBLL.Division = null;


            if (txtCommisionRate.Text.Trim().Length > 0)
                _CompanyBLL.CommissionRate = txtCommisionRate.Text.Trim();
            else
                _CompanyBLL.CommissionRate = null;

            if (txtServiceEmailId.Text.ToString().Length > 0)
                _CompanyBLL.ServiceEmailId = txtServiceEmailId.Text.Trim();
            else
                _CompanyBLL.ServiceEmailId = null;

            if (txtBankName.Text.ToString().Length > 0)
                _CompanyBLL.BankName = txtBankName.Text.Trim();
            else
                _CompanyBLL.BankName = null;

            if (txtACNo.Text.ToString().Length > 0)
                _CompanyBLL.ACNo = txtACNo.Text.Trim();
            else
                _CompanyBLL.ACNo = null;

            if (txtBranchName.Text.ToString().Length > 0)
                _CompanyBLL.BranchName = txtBranchName.Text.Trim();
            else
                _CompanyBLL.BranchName = null;

            if (txtIFSCCode.Text.ToString().Length > 0)
                _CompanyBLL.IFSCCode = txtIFSCCode.Text.Trim();
            else
                _CompanyBLL.IFSCCode = null;

            if (ddlDiv.SelectedIndex > 0)
            {
                _CompanyBLL.DivTextListId = ddlDiv.SelectedValue;
            }
            else
            {
                _CompanyBLL.DivTextListId = null;
            }
            if (ddlLocId.SelectedIndex > 0)
            {
                _CompanyBLL.LocId = ddlLocId.SelectedValue;
            }
            else
            {
                _CompanyBLL.LocId = null;
            }
            if (hfLgrId.Value != "")
                _CompanyBLL.LgrId = hfLgrId.Value;
            else
                _CompanyBLL.LgrId = null;

            if (txtGSTNo.Text.Trim().Length > 0)
                _CompanyBLL.GSTNo = txtGSTNo.Text;
            else
                _CompanyBLL.GSTNo = null;

            if (txtCINNo.Text.ToString().Length > 0)
                _CompanyBLL.CINNO = txtCINNo.Text.Trim();
            else
                _CompanyBLL.CINNO = null;

            if (ddlAccountTypeId.SelectedIndex > 0)
                _CompanyBLL.AccountTypeTextListId = ddlAccountTypeId.SelectedValue;
            else
                _CompanyBLL.AccountTypeTextListId = null;

            bool isValid = Validate();

            if (isValid == true)
            {
                string CompanyId;

                _CompanyBLL.Save();

                if (Request.QueryString["CompanyId"] == null)
                {
                    Reset();
                    ShowErrors("Success", "Record Saved Succsessfully.");
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "A", "<script type='text/javascript'>ShowStatus('Record Saved Succsessfully...');</script>", false);
                    Session["_CompanyBLL"] = null;
                    Session["_CompanyBLL"] = new CompanyBLL();

                }
                else
                {
                    Session["_CompanyBLL"] = null;
                    Response.Redirect("Companies.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Session["_CompanyBLL"] = null;

        if (Request.QueryString["CompanyId"] == null)
        {
            Response.Redirect("~/General/Default.aspx");
        }
        else
        {
            Response.Redirect("Companies.aspx");
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            _CompanyBLL.Delete(Request.QueryString["CompanyId"]);
            Session["_CompanyBLL"] = null;
            Response.Redirect("Companies.aspx");
        }
        catch (Exception ex)
        {
            HideErrors();
            ShowErrors("Error", ex.Message);
        }
    }

    #endregion

    #region "CustomerComplain Functions"

    private void loadCrncys()
    {
        ListItem li;

        ddlCrncy.Items.Clear();

        foreach (DataRow dtr in _CompanyBLL.Crncys().Rows)
        {
            li = new ListItem();

            li.Text = dtr[1].ToString();
            li.Value = dtr[0].ToString();
            ddlCrncy.Items.Add(li);

            li = null;
        }
    }

    private void LoadWebForm()
    {

        //Name
        txtName.Text = _CompanyBLL.Name;

        //AddressLine1
        txtAddressLine1.Text = _CompanyBLL.AddressLine1;


        //AddressLine2
        if (_CompanyBLL.AddressLine2 != null)
            txtAddressLine2.Text = _CompanyBLL.AddressLine2;


        //CrncyId
        if (_CompanyBLL.CrncyId != null)
            ddlCrncy.SelectedValue = _CompanyBLL.CrncyId;


        if (_CompanyBLL.Country != null)
        {
            ddlCountry.SelectedValue = _CompanyBLL.Country;
            LoadState();
        }

        if (_CompanyBLL.State != null)
        {
            ddlState.SelectedValue = _CompanyBLL.State;
            LoadCity();
        }

        if (_CompanyBLL.City != null)
        {
            ddlCity.SelectedValue = _CompanyBLL.City;
        }


        //PINCode
        if (_CompanyBLL.PINCode != null)
            txtPinCode.Text = _CompanyBLL.PINCode;

        //TelephoneNos
        if (_CompanyBLL.TelephoneNos != null)
            txtTelephoneNos.Text = _CompanyBLL.TelephoneNos.ToString();

        //FaxNos
        if (_CompanyBLL.FaxNos != null)
            txtFaxNos.Text = _CompanyBLL.FaxNos.ToString();

        //EmailId
        if (_CompanyBLL.EmailId != null)
            txtEmailId.Text = _CompanyBLL.EmailId.ToString();

        //PanNo
        if (_CompanyBLL.PanNo != null)
            txtPanNo.Text = _CompanyBLL.PanNo.ToString();

        //TINLSTNo
        if (_CompanyBLL.TINLSTNo != null)
            txtTINLSTNo.Text = _CompanyBLL.TINLSTNo.ToString();


        //TINCSTNo
        if (_CompanyBLL.TINCSTNo != null)
            txtTINCSTNo.Text = _CompanyBLL.TINCSTNo.ToString();

        //VATNo
        if (_CompanyBLL.VATNo != null)
            txtVATNo.Text = _CompanyBLL.VATNo.ToString();

        //Website
        if (_CompanyBLL.Website != null)
            txtWebsite.Text = _CompanyBLL.Website.ToString();

        //MobileNo
        if (_CompanyBLL.MobileNo != null)
            txtMobileNos.Text = _CompanyBLL.MobileNo.ToString();


        if (_CompanyBLL.LogoName != null)
            txtLogo.Text = _CompanyBLL.LogoName.ToString();

        if (fuLogo != null)
        {
            if (_CompanyBLL.Logo != null)
            {
                MemoryStream ms = new MemoryStream(_CompanyBLL.Logo, 0, _CompanyBLL.Logo.Length);
                BinaryReader br = new BinaryReader(ms);
                Byte[] bytes = br.ReadBytes((Int32)ms.Length);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                imgMemberPics.ImageUrl = "data:image/png;base64," + base64String;
            }
        }

        if (_CompanyBLL.ExciseRegNo != null)
            txtExciseRegNo.Text = _CompanyBLL.ExciseRegNo.ToString();

        if (_CompanyBLL.ServiceTaxRegn != null)
            txtServiceTaxRegn.Text = _CompanyBLL.ServiceTaxRegn.ToString();

        if (_CompanyBLL.RangeDetail != null)
            txtRange.Text = _CompanyBLL.RangeDetail.ToString();

        if (_CompanyBLL.Division != null)
            txtDivision.Text = _CompanyBLL.Division.ToString();

        if (_CompanyBLL.CommissionRate != null)
            txtCommisionRate.Text = _CompanyBLL.CommissionRate.ToString();

        if (_CompanyBLL.ServiceEmailId != null)
            txtServiceEmailId.Text = _CompanyBLL.ServiceEmailId.ToString();

        if (_CompanyBLL.BankName != null)
            txtBankName.Text = _CompanyBLL.BankName.ToString();

        if (_CompanyBLL.ACNo != null)
            txtACNo.Text = _CompanyBLL.ACNo.ToString();

        if (_CompanyBLL.BranchName != null)
            txtBranchName.Text = _CompanyBLL.BranchName.ToString();

        if (_CompanyBLL.IFSCCode != null)
            txtIFSCCode.Text = _CompanyBLL.IFSCCode.ToString();

        if (_CompanyBLL.LgrId != null)
            hfLgrId.Value = _CompanyBLL.LgrId;

        if (_CompanyBLL.LgrName != null)
            txtLgrName.Text = _CompanyBLL.LgrName;

        if (_CompanyBLL.LocId != null)
            ddlLocId.SelectedValue = _CompanyBLL.LocId;

        if (_CompanyBLL.DivTextListId != null)
            ddlDiv.SelectedValue = _CompanyBLL.DivTextListId;

        if (_CompanyBLL.GSTNo != null)
            txtGSTNo.Text = _CompanyBLL.GSTNo;

        if (_CompanyBLL.CINNO != null)
            txtCINNo.Text = _CompanyBLL.CINNO.ToString();

        if (_CompanyBLL.AccountTypeTextListId != null)
            ddlAccountTypeId.SelectedValue = _CompanyBLL.AccountTypeTextListId;
    }

    private bool Validate()
    {
        HideErrors();

        SortedList sl = _CompanyBLL.Validate();

        if (sl.Count > 0)
        {
            for (int i = 0; i < sl.Count; i++)
            {
                string a = (string)sl.GetKey(i).ToString().Substring(1);
                string key = (string)sl.GetKey(i);
                string value = (string)sl[key];

                ShowErrors(a, value);
            }
        }
        return (sl.Count == 0) ? true : false;
    }

    private void ShowErrors(string key, string value)
    {
        if (key == "Success")
            pnlErr.CssClass = "errors alert alert-success";
        else
            pnlErr.CssClass = "errors alert alert-danger";

        pnlErr.Visible = true;
        blErrs.Items.Add(new ListItem(value));

        //Name
        if (key == "Name")
        {
            lblName.CssClass = "error";
            txtName.CssClass = "error";
        }
        //Name

        //AddressLine1
        if (key == "AddressLine1")
        {
            lblAddressLine1.CssClass = "error";
            txtAddressLine1.CssClass = "error";
        }
        //AddressLine1

        //AddressLine2
        if (key == "AddressLine2")
        {
            lblAddress2.CssClass = "error";
            txtAddressLine2.CssClass = "error";
        }
        //AddressLine2

        //City
        if (key == "City")
        {
            lblCity.CssClass = "error";
            ddlCity.CssClass = "error";
        }
        //City

        //txtState
        if (key == "State")
        {
            lblState.CssClass = "error";
            ddlState.CssClass = "error";
        }
        //txtState

        //Country
        if (key == "Country")
        {
            lblCountry.CssClass = "error";
            ddlCountry.CssClass = "error";
        }
        //Country  

        //lgrname
        if (key == "LgrId")
        {
            lblLgrId.CssClass = "error";
            txtLgrName.CssClass = "error";
        }
        //lgrname

        //Division                    // Uncomment by jk
        if (key == "DivTextListId")
        {
            lblDiv.CssClass = "error";
            ddlDiv.CssClass = "error";
        }
        //Division

        //GST No
        if (key == "GSTNo")
        {
            lblGSTNo.CssClass = "error";
            txtGSTNo.CssClass = "error";
        }
        //GST No
    }

    private void HideErrors()
    {
        pnlErr.Visible = false;
        blErrs.Items.Clear();

        //Name
        lblName.CssClass = "";
        txtName.CssClass = "";
        //Name

        //AddressLine1
        lblAddressLine1.CssClass = "";
        txtAddressLine1.CssClass = "";
        //AddressLine1

        lblAddress2.CssClass = "";
        txtAddressLine2.CssClass = "";

        //City
        lblCity.CssClass = "";
        ddlCity.CssClass = "";
        //City

        //State
        lblState.CssClass = "";
        ddlState.CssClass = "";
        //State

        //Country
        lblCountry.CssClass = "";
        ddlCountry.CssClass = "";
        //Country
        lblLgrId.CssClass = "";
        txtLgrName.CssClass = "";
        //Division                      // UnComment by jk
        lblDiv.CssClass = "";
        ddlDiv.CssClass = "";
        //Division

        //GST No
        lblGSTNo.CssClass = "";
        txtGSTNo.CssClass = "";
        //GST No
    }

    private void Reset()
    {
        txtName.Text = "";
        txtAddressLine1.Text = "";
        txtAddressLine2.Text = "";
        try
        { ddlCrncy.SelectedIndex = 0; }
        catch { }

        txtPinCode.Text = "";
        txtTelephoneNos.Text = "";
        txtFaxNos.Text = "";
        txtEmailId.Text = "";
        txtPanNo.Text = "";
        txtTINLSTNo.Text = "";
        txtTINCSTNo.Text = "";
        txtVATNo.Text = "";
        txtMobileNos.Text = "";
        txtWebsite.Text = "";
        txtExciseRegNo.Text = "";
        txtServiceTaxRegn.Text = "";
        txtRange.Text = "";
        txtDivision.Text = "";
        txtCommisionRate.Text = "";
        txtServiceEmailId.Text = "";
        txtBankName.Text = "";
        txtACNo.Text = "";
        txtBranchName.Text = "";
        txtIFSCCode.Text = "";
        hfLgrId.Value = "";
        txtLgrName.Text = "";
        ddlCity.SelectedIndex = 0;
        ddlState.SelectedIndex = 0;
        ddlCountry.SelectedIndex = 0;
        ddlAccountTypeId.SelectedIndex = 0;

        try
        {
            ddlDiv.SelectedValue = MySession.DivTextListId;
        }
        catch
        {
        }
        txtGSTNo.Text = ""; 
        txtCINNo.Text = "";
    }

    private void RegisterAnchorScript()
    {
        string script = "";
        script += "<script language=JavaScript id='BookMarkScript'>" + Environment.NewLine;
        script += "var hashValue='#tabs-2';" + Environment.NewLine;
        script += "if(location.hash!=hashValue)" + Environment.NewLine;
        script += "location.hash=hashValue;" + Environment.NewLine;
        script += "<" + "/script>" + Environment.NewLine;

        if (!ClientScript.IsStartupScriptRegistered("BookMarkScript"))
            ClientScript.RegisterStartupScript(this.GetType(), "BookMarkScript", script);

    }

    #endregion

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

        if (fuLogo.PostedFile != null && fuLogo.PostedFile.FileName != "")
        {
            byte[] imageSize = new byte[fuLogo.PostedFile.ContentLength];
            HttpPostedFile uploadedImage = fuLogo.PostedFile;
            uploadedImage.InputStream.Read(imageSize, 0, (int)fuLogo.PostedFile.ContentLength);
            string imgName = fuLogo.PostedFile.FileName;
            string[] getExtenstion = fuLogo.PostedFile.FileName.Split('.');
            string oExtension = getExtenstion[getExtenstion.Length - 1].ToString();

            _CompanyBLL.Logo = imageSize;
            _CompanyBLL.LogoName = imgName;
            _CompanyBLL.LogoPath = ConfigurationSettings.AppSettings["FolderPath"] + uploadedImage.FileName.Replace("." + oExtension, "") + "." + oExtension;
            uploadedImage.SaveAs(_CompanyBLL.LogoPath);

            _CompanyBLL.UpdateLogo();
        }
        else
        {
            _CompanyBLL.Logo = null;
            _CompanyBLL.LogoName = null;
            _CompanyBLL.LogoPath = null;

            ShowErrors("Logo", "Select Logo.");
        }
    }

    //private void LoadDivision()
    //{
    //    ListItem li = new ListItem();

    //    ddlDiv.Items.Clear();
    //    // Remove comment by jk start
    //    //li.Text = "<Select>";
    //    //li.Value = "0";
    //    //ddlDiv.Items.Add(li);

    //    li = null;
    //    // Remove comment by jk end
    //    //foreach (DataRow dtr in _GeneralBLL.GetRoleWiseDivision().Rows)
    //    //{
    //    //    if (dtr[1].ToString() != "")
    //    //    {
    //    //        li = new ListItem();

    //    //        li.Text = dtr[1].ToString().ToUpper();   // ToUpper() Add by jk
    //    //        li.Value = dtr[0].ToString().ToUpper();  // ToUpper() Add by jk
    //    //        ddlDiv.Items.Add(li);

    //    //        li = null;
    //    //    }
    //    //}
    //}

    //private void LoadLocs()
    //{
    //    ListItem li = new ListItem();

    //    ddlLocId.Items.Clear();

    //    foreach (DataRow dtr in _CompanyBLL.Locs().Rows)
    //    {
    //        if (dtr[1].ToString() != "")
    //        {
    //            li = new ListItem();

    //            li.Text = dtr[1].ToString();
    //            li.Value = dtr[0].ToString();
    //            ddlLocId.Items.Add(li);

    //            li = null;
    //        }
    //    }
    //}

    private void LoadState()
    {
        ListItem li = new ListItem();

        ddlState.Items.Clear();

        li.Text = "<SELECT>";
        li.Value = "0";
        ddlState.Items.Add(li);

        li = null;

        foreach (DataRow dtr in _CompanyBLL.getState().Rows)
        {
            li = new ListItem();

            li.Text = dtr[1].ToString();
            li.Value = dtr[0].ToString();
            ddlState.Items.Add(li);

            li = null;
        }
    }

    private void LoadCity()
    {
        ListItem li = new ListItem();

        ddlCity.Items.Clear();

        li.Text = "<SELECT>";
        li.Value = "0";
        ddlCity.Items.Add(li);

        li = null;

        foreach (DataRow dtr in _CompanyBLL.getCity().Rows)
        {
            li = new ListItem();

            li.Text = dtr[1].ToString();
            li.Value = dtr[0].ToString();
            ddlCity.Items.Add(li);

            li = null;
        }
    }

    private void LoadCountry()
    {
        ListItem li = new ListItem();

        ddlCountry.Items.Clear();

        li.Text = "<SELECT>";
        li.Value = "0";
        ddlCountry.Items.Add(li);

        li = null;

        foreach (DataRow dtr in _CompanyBLL.getCountry().Rows)
        {
            li = new ListItem();

            li.Text = dtr[1].ToString();
            li.Value = dtr[0].ToString();
            ddlCountry.Items.Add(li);

            li = null;
        }
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex > 0)
        {
            _CompanyBLL.Country = ddlCountry.SelectedValue;
            LoadState();
        }
        ddlState.Focus();
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlState.SelectedIndex > 0)
        {
            _CompanyBLL.State = ddlState.SelectedValue;
            LoadCity();
        }
        ddlCity.Focus();
    }
    private void LoadAccountType()
    {
        ListItem li = new ListItem();

        ddlAccountTypeId.Items.Clear();

        li.Text = "<SELECT>";
        li.Value = "0";
        ddlAccountTypeId.Items.Add(li);

        li = null;

        foreach (DataRow dtr in _CompanyBLL.TextList("Account Type").Rows)
        {
            li = new ListItem();

            li.Text = dtr[1].ToString();
            li.Value = dtr[0].ToString();
            ddlAccountTypeId.Items.Add(li);

            li = null;
        }
    }
}