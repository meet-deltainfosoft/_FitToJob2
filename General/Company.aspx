<%@ Page Title="Company Entry" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="Company.aspx.cs" Inherits="General_Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="../jQuery/jQuery.UI/Datepicker/CSS/redmond/jquery-ui-1.8.1.custom.css"
        rel="Stylesheet" />
    <script type="text/javascript" src="../jQuery/jQuery.UI/jquery.ui.core.js"></script>
    <script type="text/javascript" src="../jQuery/jQuery.UI/Datepicker/JS/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <script src="../jQuery/jQuery.UI/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../jQuery/jQuery.UI/Tabs/JS/jquery.ui.tabs.js" type="text/javascript"></script>
    <link href="../jQuery/jQuery.UI/Tabs/CSS/redmond/jquery-ui-1.8.2.custom.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript">
        var j = jQuery.noConflict();
    </script>
    <script type="text/javascript">
        j(document).ready(function () {
            j("#<%=txtPinCode.ClientID%>").numeric();


            //Autocomplete with Postback
            j("#<%=txtLgrName.ClientID%>").autocomplete("../General/CustomerVendorNames.ashx?LgrType=ALL&address=false", { autoFill: true, mustMatch: false, minChars: 1 });
            j("#<%=txtLgrName.ClientID%>").result(function (evt, data, formatted) {
                if (data != null) {
                    if (data[0] == "No Record Founds.") {

                        j("#<%=hfLgrId.ClientID%>").val("");
                        j("#<%=txtLgrName.ClientID%>").val(data[1]);
                    }
                    else {
                        j("#<%=hfLgrId.ClientID%>").val(data[1]);
                    }
                }
                else {
                    j("#<%=hfLgrId.ClientID%>").val("");
                }
            });
        });


    </script>
    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=imgMemberPics.ClientID %>');
            var file = document.querySelector('#<%=fuLogo.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Company Master 
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 75%">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblName" Text="Name :<em>*</em>"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="txtName" Width="537px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblAddressLine1" Text="Address 1:<em>*</em>"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="txtAddressLine1" Width="537px" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblAddress2" Text="Address 2:"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="txtAddressLine2" Width="537px" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblCountry" Text="Country :<em>*</em>"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlCountry" Width="200px" MaxLength="100" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" width="130px">
                                    <asp:Label runat="server" ID="lblState" Text="State :<em>*</em>"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlState" Width="200px" MaxLength="100" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblCity" Text="City :<em>*</em>"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" ID="ddlCity" Width="200px" MaxLength="50">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblEmailId" Text="Email ID:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEmailId" Width="200px" MaxLength="100"></asp:TextBox><br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmailId"
                                        ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter a valid email address"
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblPinCode" Text="Zip/PostalCode :"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPinCode" Width="200px" MaxLength="20"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblTelephoneNo" Text="Telephone Nos.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTelephoneNos" Width="200px" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblMobileNos" Text="Mobile No.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtMobileNos" Width="200px" MaxLength="50"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblFaxNos" Text="Fax Nos.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtFaxNos" Width="200px" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblWebSite" Text="WebSite:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtWebsite" Width="200px" MaxLength="50"></asp:TextBox><br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtWebsite"
                                        ForeColor="Red" Display="Dynamic" ErrorMessage="Please enter a valid Website URL"
                                        ValidationExpression="^[a-zA-Z0-9\-\.]+\.[a-zA-Z]*$" />
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblGSTNo" Text="GST No:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtGSTNo" Width="200px" MaxLength="15"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblPanNo" Text="Pan No.:" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtPanNo" Width="200px" MaxLength="50" Visible="false"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblCrncy" runat="server" Text="Currency:<em>*</em>" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCrncy" runat="server" Width="200px" Visible="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td>
                                    <asp:Label runat="server" ID="lblTINLSTNo" Text="TIN-LST No.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTINLSTNo" Width="200px" MaxLength="50"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblTINCSTNo" Text="TIN-CST No.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTINCSTNo" Width="200px" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td>
                                    <asp:Label runat="server" ID="lblExciseRegnNo" Text="Excise Reg No.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtExciseRegNo" Width="200px" MaxLength="50"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblServiceTaxRegion" Text="Service Tax Regn.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtServiceTaxRegn" Width="200px" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td>
                                    <asp:Label runat="server" ID="lblRange" Text="Range.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtRange" Width="200px" MaxLength="50"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblVATNo" Text="VAT No.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtVATNo" Width="200px" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td>
                                    <asp:Label runat="server" ID="lblComisionRate" Text="Commission Rate:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCommisionRate"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblServiceEmailId" Text="Service EmailID:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtServiceEmailId" Width="200px" MaxLength="100"></asp:TextBox><br />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtServiceEmailId"
                                        ForeColor="Red" Display="Dynamic" ErrorMessage="Please Enter a Valid Email Address"
                                        ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                    </asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td>
                                    <asp:Label runat="server" ID="lblBankName" Text="Bank Name:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtBankName" MaxLength="100" Width="200px"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblACNo" Text="A/C No.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtACNo" Width="200px" MaxLength="20"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td style="width: 125px">
                                    <asp:Label runat="server" ID="lblBranchName" Text="Branch Name:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtBranchName" MaxLength="100" Width="200px"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblIFSCCode" Text="IFSC Code:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtIFSCCode" Width="200px" MaxLength="11"></asp:TextBox><br />
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td style="width: 125px">
                                    <asp:Label runat="server" ID="lblCINNo" Text="CIN No.:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCINNo" MaxLength="22" Width="200px"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="lblAccountType" Text="Account Type:"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAccountTypeId" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblDiv" runat="server" Text="Division:<em>*</em>"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlDiv" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Label ID="lblLocId" runat="server" Text="Default Location:<em>*</em>"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlLocId" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr runat="server" visible="false">
                                <td>
                                    <asp:Label ID="lblLgrId" runat="server" Text="Ledger:<em>*</em>"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtLgrName" runat="server" MaxLength="100" Width="537px"></asp:TextBox>
                                    <asp:HiddenField ID="hfLgrId" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label runat="server" ID="lblDivision" Text="Division.:" Visible="false"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtDivision" Width="200px" MaxLength="50" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" style="width: 25%">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblLogo" Text="Select Company Logo"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:FileUpload ID="fuLogo" runat="server" onchange="previewFile()" />
                                    <asp:TextBox ID="txtLogo" runat="server" Width="200px" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 150px;">
                                <td colspan="2">
                                    <asp:Image runat="server" ID="imgMemberPics" alt="" Style="width: 300px; height: 150px;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 90px" align="left">
                        <asp:Button runat="server" ID="btnDelete" Enabled="false" Text="Delete" OnClick="btnDelete_Click"
                            OnClientClick="return confirm('Do you Want to Delete?');" CssClass="btnDelete" />
                    </td>
                    <td style="width: 619px" align="right">
                        <asp:Button runat="server" ID="btnUpdate" Text="Update Logo" OnClick="btnUpdate_Click"
                            Visible="false" />
                        <asp:Button runat="server" ID="btnOK" Text="Save" margin="50px" OnClick="btnOK_Click"
                            CssClass="Finalbutton" />
                        <%--<asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />--%>
                        <asp:LinkButton runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
