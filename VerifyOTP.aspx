<%@ Page Title="Online Exam Login" Language="C#" AutoEventWireup="true" CodeFile="VerifyOTP.aspx.cs"
    Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <link rel="shortcut icon" href="images/favicon.ico" type="image/x-icon" />
    <link rel="icon" href="images/favicon.ico" type="image/x-icon" />
    <meta name="keywords" content="" />
    <meta name="description" content="" />
    <link href="css/Login.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    </script>
    <link href="css/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="css/css/font-awesome1.min.css" rel="stylesheet" type="text/css" />
    <%--    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>--%>
    <style type="text/css">
        i
        {
            color: Black;
        }
        
        .right
        {
            padding-right: 0.5em;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            blinkeffect('#lblMessage');
        })
        function blinkeffect(selector) {
            $(selector).fadeOut('slow', function () {
                $(this).fadeIn('slow', function () {
                    blinkeffect(this);
                });
            });
        }
    </script>
</head>
<body style="padding: 2.5em 0">
    <!-- Updated version: 24th Nov 2014 -->
    <div id="Div1" class="div1">
        <form id="Form1" runat="server" method="get">
        <div id="login">
            <fieldset style="background-color: #FFFF; border-style: solid; box-shadow: 0px 1px 6px #37c1bb;
                border-radius: 20px;">
                <div style="text-align: center">
                    <img src="images/DukeLogo.png" width="200px" />
                </div>
                <p>
                    <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
                    <asp:TextBox type="password" runat="server" ID="txtPassword" placeholder="Password"></asp:TextBox>
                </p>
                <p>
                    <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
                    <asp:TextBox type="password" runat="server" ID="txtConfirmPassword" placeholder="Confirm Password"></asp:TextBox>
                </p>
                <p>
                    <asp:DropDownList Visible="false" CssClass="uppercase" runat="server" ID="ddlYear"
                        Width="300px" Height="40px" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </p>
                <p>
                    <asp:Label ID="lblMessage" runat="server" Style="color: Red;"></asp:Label>
                </p>
                <asp:Button ID="cmdSubmit" runat="server" Text="verify otp" OnClick="cmdSubmit_Click">
                </asp:Button>
            </fieldset>
        </div>
        </form>
    </div>
</body>
</html>
