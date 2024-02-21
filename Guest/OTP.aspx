﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OTP.aspx.cs" Inherits="Guest_OTP"
    MasterPageFile="~/Guest/Candidate.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title></title>
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.15.4/css/all.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
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
            border-color: #018881; /* Change border color on hover */
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.2); /* Enhance box shadow on hover */
        }
        .buttton
        {
            background-color: #37C1BB !important;
            width: 45vh !important;
            border-radius: 10px;
            padding: 10px;
        }
        
        
        .buttton1
        {
            background-color: #dc3545de !important;
            width: 45vh !important;
            border-radius: 10px;
            padding: 10px;
            border: 1px solid #dc3545de !important;
        }
        
        .responsive-img
        {
            max-width: 100%;
            height: auto;
            max-height: 150px; /* Adjust this value based on your desired medium size */
        }
        .name
        {
            font-size: 15px;
            margin-left: auto;
        }
    </style>
    <script type="text/javascript">
        function validateNumericInput(event) {
            var charCode = (event.which) ? event.which : event.keyCode;
            return !(charCode > 31 && (charCode < 48 || charCode > 57));
        }
        function limitCharacters() {
            var textBox = document.getElementById('<%=txtOTP.ClientID%>');
            textBox.value = textBox.value.substring(0, 6);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container mt-2 containerBorder align-items-center">
        <div class="row">
            <div class="col-lg-4 col-sm-12 text-left">
                <img src="../images/DukeLogo.png" alt="Duke Logo" class="responsive-img" />
            </div>
            <div class="col-lg-4 col-sm-12 text-center" style="color: #37C1BB !important; text-decoration: underline;
                font-weight: bolder !important;">
                <%--<a href="" style="font-family: Verdana; font-weight: bold;">Sign In</a>--%>
                <h3>
                    Interview Status</h3>
                <%--
                <h6>
                    
                </h6>--%>
            </div>
            <div class="name">
                <asp:Label ID="lblFullName" runat="server" Text="Candidate:"></asp:Label>
                  <asp:Label ID="lblFullNameValue" runat="server" Text=""></asp:Label>
                 <asp:Label ID="lblMobileNumber" runat="server" Text="|"></asp:Label>
                <asp:Label ID="lblMobileNumberValue" runat="server" Text=""></asp:Label>
              
               
            </div>
             
          
        </div>
          <div class="name">
               
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
        <div class="row justify-content-left mt-4" style="display: none;">
            <div class="col-lg-12 text-left">
                <asp:Label runat="server" ID="lblOTP" Text="OTP*"></asp:Label>
                <asp:TextBox runat="server" ID="txtOTP" CssClass="form-control" MaxLength="12" placeholder="xxxxxx"
                    TabIndex="1" onkeypress="return validateNumericInput(event);" oninput="limitCharacters()"
                    Style="color: #37C1BB"></asp:TextBox>
            </div>
        </div>
        <div class="mt-2">
            <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
        </div>
        <div class="row justify-content-left mt-4">
            <div class="col-lg-6 col-sm-6 text-left mt-2">
                <asp:LinkButton runat="server" ID="lnkBtnGetOTP" CssClass="btn btn-info buttton"
                    OnClick="lnkBtnGetOTP_click">
                    <div class="d-flex justify-content-center align-items-center">
                        <i class="fas fa-check-circle mr-2"></i><span id="lblVerifyOtp" runat="server">Go To
                            Next Page</span>
                    </div>
                </asp:LinkButton>
            </div>
            <%--<div class="col-lg-6 col-sm-6 text-left mt-2">
                <asp:LinkButton runat="server" ID="lnkResendOTP" CssClass="btn btn-info buttton"
                    OnClick="lnkResendOTP_click">
                    <div class="d-flex justify-content-center align-items-center">
                        <i class="fas fa-check-circle mr-2"></i><span id="Span2" runat="server">Resend OTP</span>
                    </div>
                </asp:LinkButton>
            </div>--%>
            <div class="col-lg-6 col-sm-6 text-left mt-2">
                <asp:LinkButton ID="btnprint" runat="server" CssClass="btn btn-info buttton" OnClick="lnkPrint_click"
                    Style="background-color: #37C1BB;">
        <div class="d-flex justify-content-center align-items-center">
            <i class="fas fa-print mr-2"></i><span>Print</span>
        </div>
                </asp:LinkButton>
            </div>
            <div class="col-lg-6 col-sm-6 text-left mt-2">
                <asp:LinkButton ID="lnkInterviewstatus" runat="server" CssClass="btn btn-info buttton"
                    OnClick="lnkInterviewstatus_click" Style="background-color: #37C1BB;">
        <div class="d-flex justify-content-center align-items-center">
            <i class="fas fa-eye mr-2"></i><span>View Interview Status</span>
        </div>
                </asp:LinkButton>
            </div>
            <div class="col-lg-6 col-sm-6 text-left mt-2">
                <asp:LinkButton ID="lnkbtnOffer" runat="server" CssClass="btn btn-info buttton1"
                    OnClick="lnkbtnOffer_click" Style="background-color: #37C1BB;">
        <div class="d-flex justify-content-center align-items-center" id="blinkContainer">
            <i class="fas fa-upload mr-2"></i><span>Offer Acceptance</span>
        </div>
                </asp:LinkButton>
            </div>
            <div class="col-lg-6 col-sm-6 text-left mt-2">
                <asp:LinkButton ID="lnkInterviewUploadDocuments" runat="server" CssClass="btn btn-info buttton"
                    OnClick="lnkInterviewUploadDocuments_click" Style="background-color: #37C1BB;
                    color: Red !important;">
        <div class="d-flex justify-content-center align-items-center">
            <i class="fas fa-upload mr-2"></i><span>Upload Documents</span>
        </div>
                </asp:LinkButton>
            </div>
        </div>
        <div class="row justify-content-left mt-3">
        </div>
    </div>
    <script type="text/javascript">
        function blink() {
            var blinkContainer = document.getElementById('blinkContainer');
            blinkContainer.style.opacity = (blinkContainer.style.opacity == '0' ? '1' : '0');
            blinkText.style.color = (blinkText.style.color == 'red' ? 'black' : 'red');
        }

        setInterval(blink, 600); // 2000 milliseconds = 2 seconds
    </script>
</asp:Content>
