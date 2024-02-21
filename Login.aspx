<%@ Page Title="Online Exam Login" Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs"
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
                    <%--<img src="images/Genius.png" width="200px" />--%>
                    <%--<img src="images/SWASTIK.jpg" width="180px" />--%>
                </div>
                <p>
                    <asp:TextBox type="text" runat="server" ID="txtUserName" value="UserName" placeholder="UserName"
                        onfocus="if(this.value=='UserName')this.value='' "></asp:TextBox>
                </p>
                <p style="text-align: center">
                    <asp:TextBox TextMode="password" runat="server" ID="txtPassword" placeholder="Password"
                        onfocus="if(this.value=='Password')this.value='' "></asp:TextBox>
                    <%--<asp:Label ID="lblMessage" runat="server" Width="180px" Font-Bold="False" Font-Italic="True"
                        Font-Size="X-Small" ForeColor="#C00000"></asp:Label>--%>
                </p>
                <p>
                    <asp:DropDownList Visible="false" CssClass="uppercase" runat="server" ID="ddlYear"
                        Width="300px" Height="40px" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:Label ID="lblMessage" runat="server" Visible="false" Style="color: Red;"></asp:Label>
                </p>
                <asp:Button ID="cmdSubmit" runat="server" Text="Sign in" OnClick="cmdSubmit_Click">
                </asp:Button>
                <div style="text-align: center; font-family: Verdana; font-weight: bold;">
                    <asp:LinkButton ID="lnlForgotPassoword" runat="server" Text="Forgot Pasword/Set Password"
                        OnClick="lnlForgotPassoword_Click"></asp:LinkButton>
                </div>
                <%--Version : 17.07.01 V1 Dated : 17-Jul-2020 DT+QB--%>
                <%--10.07.01 V3 Dated : 10-Jul-2020 for 100--%>
                <%--Version : 11.05.03 V3 Dated : 12-May-2020--%>
                <p style="text-align: center; font-size: x-small">
                    <asp:Label ID="lblVersion" runat="server" Text=""></asp:Label></p>
            </fieldset>
        </div>
        <div style="text-align: center" class="banner">
            <a href="http://www.ierp.in/" style="color: #555">
                <img src="images/DeltaIPL-T.png" height="58px" width="145px" /></a><br />
            <asp:Image runat="server" ID="imgflag" ImageUrl="~/images/Flag.png" Width="20px"
                Height="12px" />&nbsp;Design in India
        </div>
        <div class="banner" style="text-align: center; font-size: x-small">
            B-1010, Infinity Tower, S.G. Highway, Ahmedabad - 15, Gujarat</a>
        </div>
        <!-- end #content -->
        </form>
        <!-- end #footer -->
    </div>
</body>
</html>
