<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Registration1.aspx.cs"
    Inherits="General_Registration1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">--%>
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
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();
    </script>
    <script type="text/javascript">
        function validateNumericInput(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            return !(charCode > 31 && (charCode < 48 || charCode > 57));
        }
       
    </script>
    <script type="text/javascript">


        function previewFile() {

            var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
            var file = document.querySelector('#<%=fuPhoto.ClientID %>').files[0];
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
    <script type="text/javascript">
        function validateFile() {
            var fileInput = document.getElementById('<%= fuPhoto.ClientID %>');
            var filePath = fileInput.value;
            var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;

            if (!allowedExtensions.exec(filePath)) {
                alert('Please upload file having extensions .jpg/.jpeg/.png/.gif only.');
                fileInput.value = '';
                return false;
            } else {
                var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
                var file = document.querySelector('#<%=fuPhoto.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }
                return true;
            }
        }
    </script>
    <script type="text/javascript">
        function previewFile1() {

            var preview = document.querySelector('#<%=imgSelfintravideo.ClientID %>');
            var file = document.querySelector('#<%=fuSelfintravideo.ClientID %>').files[0];
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
    <script type="text/javascript">

        function previewFile3() {

            var preview = document.querySelector('#<%=imgResumeUpload.ClientID %>');
            var file = document.querySelector('#<%=fuResumeUpload.ClientID %>').files[0];
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
    <script type="text/javascript">
        function validateVideo() {
            debugger;
            var fileInput = document.getElementById('<%= fuSelfintravideo.ClientID %>');
            var filePath = fileInput.value;
            var allowedExtensions = /(\.mp4|\.avi|\.mov)$/i;

            if (!allowedExtensions.exec(filePath)) {
                alert('Please upload a video file with extensions .mp4, .avi, or .mov only.');
                fileInput.value = '';
                return false;
            }
            else {
                var preview = document.querySelector('#<%=imgSelfintravideo.ClientID %>');
                var file = document.querySelector('#<%=fuSelfintravideo.ClientID %>').files[0];
                var reader = new FileReader();

                reader.onloadend = function () {
                    preview.src = reader.result;
                }

                if (file) {
                    reader.readAsDataURL(file);
                } else {
                    preview.src = "";
                }
                return true;
            }

            // Add video length validation logic here if needed


        }
    </script>
    <script type="text/javascript">
        function validateResume() {
            var fileInput = document.getElementById('<%= fuResumeUpload.ClientID %>');
            var filePath = fileInput.value;
            var allowedExtensions = /(\.pdf|\.doc|\.docx|\.jpg|\.jpeg|\.png|\.gif)$/i;

            if (!allowedExtensions.exec(filePath)) {
                alert('Please upload files with extensions .pdf, .doc, .docx, .jpg, .jpeg, .png, or .gif only.');
                fileInput.value = '';
                return false;
            }

            // Add additional validation logic here if needed

            return true;
        }
    </script>
    <script type="text/javascript">
        j(document).ready(function () {

            j("#<%=txtMobileNo.ClientID%>").numeric();
            j("#<%=txtAadharNo.ClientID%>").numeric();

        });
      
    </script>
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
</head>
<body>
    <form id="Registration1" runat="server">
    <div class="container mt-2 containerBorder align-items-center">
        <%--</asp:Content>--%>
        <%--<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">--%>
        <div class="container">
            <div class="form">
                <div class="formHeader" class="formHeader header-divider">
                    <asp:Label ID="lblTitle" runat="server" Text="Registration Entry (New Mode)"></asp:Label>
                </div>
                <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
                    <asp:BulletedList ID="blErrs" runat="server">
                    </asp:BulletedList>
                </asp:Panel>
                <div class="formBody">
                    <div class="row">
                        <div class="col-lg-4 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblAadharNo" Text="AadharCard No :"></asp:Label>
                                <span style="color: red">*</span>
                                <asp:TextBox runat="server" ID="txtAadharNo" CssClass="form-control" MaxLength="12"
                                    onkeypress="return validateNumericInput(event);" TabIndex="1" AutoPostBack="false"
                                    AutoCompleteType="Disabled">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvAadharNo" ControlToValidate="txtAadharNo"
                                    ErrorMessage="Aadhar number is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revAadharNo" ControlToValidate="txtAadharNo"
                                    ValidationExpression="^\d{12}$" ErrorMessage="Aadhar number must be 12 digits."
                                    Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-lg-4 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblFirstname" Text="First Name :"></asp:Label>
                                <span style="color: red">*</span>
                                <asp:TextBox runat="server" ID="txtFirstname" CssClass="form-control" TabIndex="2"
                                    AutoCompleteType="Disabled">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtFirstname"
                                    ErrorMessage="First name is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-4 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblMiddlename" Text="Middle Name :"></asp:Label>
                                <span style="color: red">*</span>
                                <asp:TextBox runat="server" ID="txtMiddlename" CssClass="form-control" TabIndex="3"
                                    AutoCompleteType="Disabled">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtMiddlename"
                                    ErrorMessage="Middle name is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-4 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblLastName" Text="Last Name :"></asp:Label>
                                <span style="color: red">*</span>
                                <asp:TextBox runat="server" ID="txtLastname" CssClass="form-control" TabIndex="4"
                                    AutoCompleteType="Disabled">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtLastname"
                                    ErrorMessage="Last name is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-4 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblMobileNo" Text="Mobile No :"></asp:Label>
                                <span style="color: red">*</span>
                                <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control" TabIndex="5"
                                    AutoCompleteType="Disabled" MaxLength="10">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="rfvMobileNo" ControlToValidate="txtMobileNo"
                                    ErrorMessage="Mobile number is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator runat="server" ID="revMobileNo" ControlToValidate="txtMobileNo"
                                    ValidationExpression="^\d{10}$" ErrorMessage="Mobile number must be 10 digits."
                                    Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-lg-4 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblCity" Text="City / Village :"></asp:Label>
                                <span style="color: red">*</span>
                                <asp:TextBox runat="server" ID="txtCity" CssClass="form-control" TabIndex="6">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtCity"
                                    AutoCompleteType="Disabled" ErrorMessage="City is required." Display="Dynamic"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-4 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblTaluka" Text="Taluka :"></asp:Label>
                                <span style="color: red">*</span>
                                <asp:TextBox runat="server" ID="txtTaluka" CssClass="form-control" TabIndex="7">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtTaluka"
                                    AutoCompleteType="Disabled" ErrorMessage="Taluka is required." Display="Dynamic"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-4 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblDistrict" Text="District :"></asp:Label>
                                <span style="color: red">*</span>
                                <asp:TextBox runat="server" ID="txtDistrict" CssClass="form-control" TabIndex="8">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtDistrict"
                                    AutoCompleteType="Disabled" ErrorMessage="District is required." Display="Dynamic"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-4 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblState" Text="State :"></asp:Label>
                                <span style="color: red">*</span>
                                <asp:TextBox runat="server" ID="txtState" CssClass="form-control" TabIndex="9">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtState"
                                    AutoCompleteType="Disabled" ErrorMessage="State is required." Display="Dynamic"
                                    ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-12 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblAddress" Text="Address :"></asp:Label>
                                <span style="color: red">*</span>
                                <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" TabIndex="10" CssClass="form-control"
                                    AutoCompleteType="Disabled">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtAddress"
                                    ErrorMessage="Address is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="lblPhoto" runat="server" Text="Passport Photo : "></asp:Label>
                            <span style="color: red">*</span>
                            <asp:FileUpload ID="fuPhoto" runat="server" class="form-control" onchange="return validateFile();" />
                        </div>
                        <div class="col-lg-3">
                            <asp:Image ID="imgPhoto" runat="server" Height="158.4px" TabIndex="11" Visible="True"
                                Width="120px" />
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="lblSelfintravideo" runat="server" Text="Self Intro Video :"></asp:Label>
                            <asp:FileUpload ID="fuSelfintravideo" runat="server" class="form-control" onchange="return validateVideo();" />
                        </div>
                        <div class="col-lg-3">
                            <asp:Label ID="lblResume" runat="server" Text="Resume Upload :"></asp:Label>
                            <span style="color: red">*</span>
                            <asp:FileUpload ID="fuResumeUpload" runat="server" onchange="return validateResume();"
                                class="form-control" />
                        </div>
                        <table style="display: none">
                            <tr>
                                <td align="left">
                                    <asp:Image ID="imgSelfintravideo" runat="server" Height="158.4px" TabIndex="12" Visible="True"
                                        Width="120px" />
                                </td>
                                <td align="right">
                                </td>
                                <td>
                                </td>
                                <td align="left">
                                    <asp:Image ID="imgResumeUpload" runat="server" Height="158.4px" TabIndex="13" Visible="True"
                                        Width="120px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <div class="formFooter">
                    <div class="row">
                        <div class="col-lg-8 m-1">
                            <asp:Button runat="server" ID="btnOk" Text="OK" OnClick="btnOk_Click" class="btn custom-button"
                                Style="height: 30px; width: 200px; font-size: 15px;" CausesValidation="true" />
                            <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"
                                class="btn custom-button" Style="height: 30px; width: 80px; font-size: 15px;" />
                        </div>
                        <div class="col-lg-2 m-1">
                            <asp:Button runat="server" ID="btnDelete" Enabled="false" Text="Delete" class="btn btn-danger"
                                OnClientClick="return confirm('Do you Want to Delete');" OnClick="btnDelete_Click"
                                Style="height: 30px; width: 80px; font-size: 15px; display: none;" />
                            <asp:Label ID="lblPhotoPath" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblSelfIntroPath" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblResumePath" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row m-1">
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
<%--</asp:Content>--%>
