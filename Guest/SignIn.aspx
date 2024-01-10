<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignIn.aspx.cs" Inherits="Guest_SignIn"
    UICulture="auto:gu-IN" Culture="auto:gu-IN" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.15.4/css/all.css">
    <style type="text/css">
        body
        {
            font-family: Verdana;
            font-size: small;
            color: #37C1BB;
            background-color: #37C1BB;
            border-radius-webkit: 20px !important;
            font-weight: bold;
        }
        .containerBorder
        {
            border: 3px solid #37C1BB;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); /* Adding a subtle box shadow */
            padding: 20px; /* Adding some padding for spacing inside the container */
            background-color: white; /* Setting a light background color */
            margin-top: 10% !important;
            height: auto;
        }
        
        .containerBorder:hover
        {
            border-color: #218838; /* Change border color on hover */
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.2); /* Enhance box shadow on hover */
        }
        .buttton
        {
            background-color: #37C1BB !important;
            width: 25vh !important;
            border-radius: 10px;
            padding: 10px;
        }
        .responsive-img
        {
            max-width: 100%;
            height: auto;
            max-height: 150px; /* Adjust this value based on your desired medium size */
        }
    </style>
    <script type="text/javascript">
        function validateNumericInput(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            return !(charCode > 31 && (charCode < 48 || charCode > 57));
        }
        function limitCharacters() {
            var textBox = document.getElementById('<%=txtMobileNo.ClientID%>');
            textBox.value = textBox.value.substring(0, 10);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container mt-2 containerBorder align-items-center">
        <div class="row">
            <div class="col-lg-4 col-sm-12 text-left">
                <img src="../images/DukeLogo.png" alt="Duke Logo" class="responsive-img" />
            </div>
            <div class="col-lg-4 col-sm-12 text-center" style="color: #37C1BB !important; text-decoration: underline;
                font-weight: bolder !important;">
                <%--<a href="" style="font-family: Verdana; font-weight: bold;">Sign In</a>--%>
                <h3>
                    <span runat="server" id="lblSignIn">Sign In</span></h3>
                <%--
                <h6>
                    
                </h6>--%>
            </div>
        </div>
        <div class="row justify-content-left">
            <div class="col-lg-12 col-sm-6 text-left">
                <h4>
                    Duke
                </h4>
            </div>
            <div class="col-lg-12 col-sm-12 text-left" style="color: #53504F !important">
                <h3>
                    Digital Interview
                </h3>
            </div>
        </div>
        <div class="row justify-content-left mt-4">
            <div class="col-lg-12 text-left">
                <%-- <asp:Label meta:resourcekey="lblShree" runat="server" ID="lblShree85" Text="abc"></asp:Label>--%>
                <asp:Label meta:resourcekey="lblMobileNo" runat="server" ID="lblMobileNo" Text="Enter Your Mobile No*"></asp:Label>
                <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control" MaxLength="12"
                    placeholder="xxxxxxxxxx" TabIndex="1" onkeypress="return validateNumericInput(event);"
                    oninput="limitCharacters()" Style="color: #37C1BB"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMobileNo"
                    ErrorMessage="Mobile number is required." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMobileNo"
                    ErrorMessage="Invalid mobile number. It should be 10 digits." Display="Dynamic"
                    ForeColor="Red" ValidationExpression="^\d{10}$"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="row justify-content-center mt-3">
            <div class="col-lg-12 col-sm-12 text-left">
                <asp:LinkButton runat="server" ID="lnkBtnGetOTP" CssClass="btn btn-info buttton"
                    OnClick="lnkBtnGetOTP_click">
                    <i class="fa fa-chevron-circle-right"></i><span id="lblGetOtp" runat="server">Get OTP
                    </span>
                </asp:LinkButton>
                <asp:Label ID="lblMessage" runat="server" Text="Incorrect Information" Visible="false"
                    ForeColor="Red"></asp:Label>
            </div>
        </div>
        <div class="mt-1">
        </div>
    </div>
    </form>
</body>
</html>
