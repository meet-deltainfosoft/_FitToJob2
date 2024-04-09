<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InterviewForms.aspx.cs" MasterPageFile="~/Guest/Candidate.master"
    Inherits="Guest_InterviewForms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <%--<head runat="server">--%>
    <title></title>
    <link type="text/css" href="../jQuery/jQuery.UI/Datepicker/CSS/redmond/jquery-ui-1.8.1.custom.css"
        rel="Stylesheet" />
    <script type="text/javascript" src="../jQuery/jQuery.UI/jquery.ui.core.js"></script>
    <script type="text/javascript" src="../jQuery/jQuery.UI/Datepicker/JS/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <script src="../jQuery/jQuery.UI/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../jQuery/jQuery.UI/Tabs/JS/jquery.ui.tabs.js" type="text/javascript"></script>
    <link href="../jQuery/jQuery.UI/Tabs/CSS/redmond/jquery-ui-1.8.2.custom.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="../jQuery/Timepicker/jquery.ui.timepicker.js"></script>
    <link type="text/css" href="../jQuery/Timepicker/jquery-ui-1.8.14.custom.css" />
    <link type="text/css" href="../jQuery/Timepicker/jquery.ui.timepicker.css" />
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <%-- <link type="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
     <script type="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script type="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"
        integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"
        crossorigin="anonymous">
    <!-- Bootstrap JS and Popper.js (required for Bootstrap dropdowns, modals, etc.) -->
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.0.8/dist/umd/popper.min.js"
        integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"
        integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous"></script>
    <%--<script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>--%>
    <script src="//cdn.rawgit.com/Eonasdan/bootstrap-datetimepicker/e8bddc60e73c1ec2475f827be36e1957af72e2ea/src/js/bootstrap-datetimepicker.js"></script>
    <script src="//code.jquery.com/jquery-2.1.1.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.11.0/jquery-ui.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js" integrity="sha256-oP6HI/tZ1a1BLjhjW2Xp7Jd9I9g0CUsdK1Qa5OE24gM="
        crossorigin="anonymous"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();


    </script>
    <script type="text/javascript">
        j(document).ready(function () {
            j("#<%=txtVillageNotableReferncesMobileNo.ClientID%>").numeric();
            j("#<%=txtDukeReferencesMobileNo.ClientID%>").numeric();
        });
    </script>
    <style type="text/css">
        .header-style
        {
            background-color: #3498db !important; /* Header background color */
            color: #fff; /* Header text color */
        }
        
        .gridview-style
        {
            margin-left: 2vh; /* Margin-left for the entire GridView */
            margin-right: 1vh;
        }
    </style>
    <style>
        /* Custom CSS styles for your form */
        body
        {
            font-family: 'Arial' , sans-serif;
            background-color: #37C1BB;
        }
        
        .form
        {
            background-color: #ffffff;
            border: 1px solid #dee2e6;
            padding: 10px;
            margin: 20px;
            border-radius: 5px;
        }
        
        .formHeader
        {
            background-color: #37C1BB;
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
            color: #ffffff;
        }
        
        .header-divider
        {
            border-bottom: 2px solid #000000; /* Border color for the header divider line */
            margin-bottom: 10px;
        }
        
        .form-group
        {
            margin-bottom: 15px;
        }
        
        .custom-button
        {
            background-color: #37C1BB;
            color: #ffffff;
        }
        .containerBorder
        {
            border: 3px solid #37C1BB;
            border-radius: 10px;
            margin: 25px;
            background-color: white;
            height: auto;
        }
        
        .containerBorder:hover
        {
            border-color: #018881;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
        }
        
        /* Add more custom styles as needed */
    </style>
    <script type="text/javascript">
        $('#txtDOB').datetimepicker({
            todayHighlight: true,
            format: 'dd/mm/yyyy',
            startDate: new Date()
        });
    </script>
    <script type="text/javascript">
        document.getElementById("openButton").addEventListener("click", function () {
            // Create a new TextBox
            var textBox = document.createElement("input");
            textBox.type = "text";
            textBox.id = "dynamicTextBox";

            // Append the TextBox to the body or another container
            document.body.appendChild(textBox);

            // Optionally, focus on the TextBox for user convenience
            textBox.focus();
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Attach the datepicker to the TextBox with the "datepicker" class
            $(".datepicker").datepicker({
                dateFormat: "dd/mm/yy"
            })
        });
    </script>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function isNumberInteger(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
    <%-- </head>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="containerBorder ">
        <div class="wrapper">
            <div class="form">
                <div class="formHeader" class="formHeader header-divider">
                    Registration Entry - Value
                    <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
                </div>
                <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
                    <asp:BulletedList ID="blErrs" runat="server">
                    </asp:BulletedList>
                </asp:Panel>
                <div class="formBody">
                    <div class="row">
                        <div class="col-lg-3 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblFullName" Text="Full Name*"></asp:Label>
                                <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" MaxLength="12"
                                    TabIndex="1">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName"
                                    ErrorMessage="Full Name is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblDOB" Text="Date Of Birth*"></asp:Label>
                                <asp:TextBox runat="server" ID="txtDOB" CssClass="form-control datepicker" MaxLength="12"
                                    TabIndex="1">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDOB"
                                    ErrorMessage="Date Of Birth is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblPF" Text="UAN Number"></asp:Label>
                                <asp:TextBox runat="server" ID="txtPF" CssClass="form-control" MaxLength="12" TabIndex="1">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblBloodGroup" Text="Blood Group*"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlBloodGroup" CssClass="form-control" TabIndex="2">
                                    <asp:ListItem Text="Select Blood Group" Value="" />
                                    <asp:ListItem Text="A+" Value="A+" />
                                    <asp:ListItem Text="A-" Value="A-" />
                                    <asp:ListItem Text="B+" Value="B+" />
                                    <asp:ListItem Text="B-" Value="B-" />
                                    <asp:ListItem Text="AB+" Value="AB+" />
                                    <asp:ListItem Text="AB-" Value="AB-" />
                                    <asp:ListItem Text="O+" Value="O+" />
                                    <asp:ListItem Text="O-" Value="O-" />
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ID="rfvBloodGroup" ControlToValidate="ddlBloodGroup"
                                    InitialValue="" ErrorMessage="Please select a Blood Group." ForeColor="Red" Display="Dynamic" />
                                <!-- Add ValidationSummary to display all validation messages at once -->
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-sm-12">
                            <div class="row m-2" style="border: 1px solid #3498db; padding-bottom: 8px;">
                                <div class="col-lg-8 col-sm-12">
                                    <asp:Label ID="lblPermanentAddress" runat="server" Text="Permanent Address"></asp:Label>
                                    <asp:TextBox ID="txtPermanentAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-sm-12">
                                    <asp:Label ID="lblPermanentPost" runat="server" Text="Permanent Taluka"></asp:Label>
                                    <asp:TextBox ID="txtPermanentPost" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-sm-12">
                                    <asp:Label ID="lblPermanentVillage" runat="server" Text="Permanent Village"></asp:Label>
                                    <asp:TextBox ID="txtPermanentVillage" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-sm-12">
                                    <asp:Label ID="lblPermanentDistrict" runat="server" Text="District"></asp:Label>
                                    <asp:TextBox ID="txtPermanentDistrict" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-sm-12">
                                    <asp:Label ID="lblPermanentPinCode" runat="server" Text="PinCode"></asp:Label>
                                    <asp:TextBox ID="txtPermanentPinCode" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revPermanentPinCode" runat="server" ControlToValidate="txtPermanentPinCode"
                                        ValidationExpression="^\d{6}$" ErrorMessage="PIN code must be exactly 6 digits."
                                        Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-lg-6 col-sm-12">
                                    <asp:Label ID="lblPermanentState" runat="server" Text="State"></asp:Label>
                                    <asp:TextBox ID="txtPermanentState" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-sm-12">
                                    <asp:Label ID="lblPermanentMobileNo" runat="server" Text="Mobile No"></asp:Label>
                                    <asp:TextBox ID="txtPermanentMobileNo" runat="server" CssClass="form-control" onkeypress="return isNumber(event)"
                                        MaxLength="10" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 col-sm-12">
                            <div class="row m-2" style="border: 1px solid #3498db; padding-bottom: 8px;">
                                <div class="col-lg-4 col-sm-12">
                                    <asp:Label ID="lblSameasAbove" runat="server" Text="Same As Above"></asp:Label>
                                    <asp:CheckBox ID="chkSameAsAbove" runat="server" AutoPostBack="true" OnCheckedChanged="chkSameAsAbove_OnCheckedChanged" />
                                </div>
                                <div class="col-lg-8 col-sm-12">
                                    <asp:Label ID="lblresidentialAddress" runat="server" Text="Residential Address"></asp:Label>
                                    <asp:TextBox ID="txtresidentialAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-sm-12">
                                    <asp:Label ID="lblresidentialPost" runat="server" Text="Taluka"></asp:Label>
                                    <asp:TextBox ID="txtresidentialPost" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-sm-12">
                                    <asp:Label ID="lblresidentialVillage" runat="server" Text="Village"></asp:Label>
                                    <asp:TextBox ID="txtresidentialVillage" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-sm-12">
                                    <asp:Label ID="lblresidentialDistrict" runat="server" Text="District"></asp:Label>
                                    <asp:TextBox ID="txtresidentialDistrict" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-sm-12">
                                    <asp:Label ID="lblresidentialPinCode" runat="server" Text="PinCode"></asp:Label>
                                    <asp:TextBox ID="txtresidentialPinCode" runat="server" CssClass="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtresidentialPinCode"
                                        ValidationExpression="^\d{6}$" ErrorMessage="PIN code must be exactly 6 digits."
                                        Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                                <div class="col-lg-6 col-sm-12">
                                    <asp:Label ID="lblresidentialState" runat="server" Text="State"></asp:Label>
                                    <asp:TextBox ID="txtresidentialState" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-lg-6 col-sm-12">
                                    <asp:Label ID="lblresidentialMobileNo" runat="server" Text="Mobile No"></asp:Label>
                                    <asp:TextBox ID="txtresidentialMobileNo" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
                            <asp:RadioButtonList runat="server" ID="rblCategory" RepeatDirection="Horizontal"
                                CssClass="radio-inline">
                                <asp:ListItem Text="General" Value="General" Selected="True" />
                                <asp:ListItem Text="S.C" Value="S.C" />
                                <asp:ListItem Text="S.T" Value="S.T" />
                                <asp:ListItem Text="OBC" Value="OBC" />
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblAadharCard" runat="server" Text="Aadhar Card"></asp:Label>
                            <asp:TextBox ID="txtAadharCard" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblPanCard" runat="server" Text="PAN Card"></asp:Label>
                            <asp:TextBox ID="txtPanCard" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblElectionCard" runat="server" Text="Election Card"></asp:Label>
                            <asp:TextBox ID="txtElectionCard" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblMaritalStatus" runat="server" Text="Marital Status"></asp:Label>
                            <asp:RadioButtonList runat="server" ID="rblMaritalStatus" RepeatDirection="Horizontal"
                                CssClass="radio-inline">
                                <asp:ListItem Text="Single" Value="Single" Selected="True" />
                                <asp:ListItem Text="Married" Value="Married" />
                                <asp:ListItem Text="Divorced" Value="Divorced" />
                            </asp:RadioButtonList>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblNomineeName" runat="server" Text="Nominee Name"></asp:Label>
                            <asp:TextBox ID="txtNomineeName" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblNomineeDOB" runat="server" Text="Nominee Birth Date"></asp:Label>
                            <asp:TextBox ID="txtNomineeDOB" runat="server" CssClass="form-control datepicker"
                                AutoPostBack="true" OnTextChanged="txtNomineeDOB_TextChanged"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblNomineeAge" runat="server" Text="Nominee Age"></asp:Label>
                            <asp:TextBox ID="txtNomineeAge" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblRelationWithNominee" runat="server" Text="Relation With Nominee"></asp:Label>
                            <asp:TextBox ID="txtRelationWithNominee" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblEmail" runat="server" Text="Email*"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmail"
                                ErrorMessage="Email Id Required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$" ErrorMessage="Enter a valid email address."
                                Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblDukeReferences" runat="server" Text="Company Reference Name"></asp:Label>
                            <asp:TextBox ID="txtDukeReferences" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblDukeReferencesMobileNo" runat="server" Text="Company Reference Mobile Number"></asp:Label>
                            <asp:TextBox ID="txtDukeReferencesMobileNo" runat="server" CssClass="form-control"
                                onkeypress="return isNumber(event)" MaxLength="10"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblVillageNotableRefernces" runat="server" Text="Village References Name"></asp:Label>
                            <asp:TextBox ID="txtVillageNotableRefernces" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 col-sm-12">
                            <asp:Label ID="lblVillageNotableReferncesMobileNo" runat="server" Text="Village References Number"></asp:Label>
                            <asp:TextBox ID="txtVillageNotableReferncesMobileNo" runat="server" CssClass="form-control"
                                onkeypress="return isNumber(event)" MaxLength="10"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 col-sm-12" runat="server" visible="false">
                            <asp:Label ID="Label1" runat="server" Text="Upload Digital Signature"></asp:Label>
                            <%--<asp:FileUpload ID="fuDigitalSignature" runat="server" />--%>
                            <asp:FileUpload ID="fuDigitalSignature" runat="server" class="form-control" />
                        </div>
                    </div>
                </div>
                <div class="mt-3">
                </div>
                <div class="formHeader" class="formHeader header-divider">
                    <%--Registration Entry - Value
                <asp:Label ID="Label1" runat="server" Text=" - [New Mode]"></asp:Label>--%>
                    <asp:Label ID="lblEducationHeader" runat="server" Text="Education Detail"></asp:Label>
                </div>
                <div class="mt-1">
                </div>
                <div class="row" style="overflow: scroll;">
                    <asp:GridView runat="server" ID="gvEducationDetails" AutoGenerateColumns="false"
                        CssClass="gridview-style" CellPadding="4" ForeColor="#333333" GridLines="None"
                        Width="993px">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Education Level">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblEducationLevel" Text='<%# Eval("EducationLevel") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Board/University Name">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtBoardUniversity" CssClass="form-control" Text='<%# Eval("BoardUniversityName") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Passing Year">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtPassingYear" CssClass="form-control" Text='<%# Eval("PassingYear") %>'
                                        onkeypress="return isNumberInteger(event)" MaxLength="4"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Percentage">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtPercentage" CssClass="form-control decimalInput"
                                        onkeypress="return isNumber(event)" Text='<%# Eval("Percentage") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                </div>
                <div class="row mt-1">
                    <div class="class-lg-3 col-sm-6">
                        <asp:Button ID="btnSubmit" runat="Server" Text="Save & Next" class="btn btn-primary"
                            Style="font-family: Verdana !important; font-size: 13px" OnClick="lnkBtnSubmit_click"
                            CausesValidation="true" />
                        <asp:Label ID="lblDigitalSignature" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="mt-2">
                    <asp:Label ID="lblMessage" runat="server" Style="color: Red;" Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
