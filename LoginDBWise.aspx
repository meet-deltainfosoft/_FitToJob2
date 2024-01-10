<%@ Page Title="Online Exam Login" Language="C#" AutoEventWireup="true" CodeFile="LoginDBWise.aspx.cs"
    Inherits="LoginDBWise" %>

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
            <fieldset style="background-color: #F7F7F7; border-style: solid; box-shadow: 0px 1px 6px #009933;">
                <div style="text-align: center">
                    <%--<img src="images/SANKALP.jpg" width="180px" />--%>
                    <img src="images/msvidyamandir.png" width="200px" />
                    <%--<img src="images/SWASTIK.jpg" width="180px" />--%>
                </div>
                <p>
                    <asp:TextBox type="text" runat="server" ID="txtUserName" value="UserName" placeholder="UserName"
                        onfocus="if(this.value=='UserName')this.value='' " MaxLength="20"></asp:TextBox></p>
                <p style="text-align: center">
                    <asp:TextBox TextMode="password" runat="server" ID="txtPassword" placeholder="Password"
                        onfocus="if(this.value=='Password')this.value='' " MaxLength="15"></asp:TextBox>
                    <asp:Label ID="lblMessage" runat="server" Width="180px" Font-Bold="False" Font-Italic="True"
                        Font-Size="X-Small" ForeColor="#C00000"></asp:Label>
                </p>
                   <p>
                    <asp:DropDownList Visible="true" CssClass="uppercase" runat="server" ID="DdlDBNO"
                        Width="300px" Height="40px" >
                    </asp:DropDownList>
                </p>
                <p>
                    <asp:DropDownList Visible="false" CssClass="uppercase" runat="server" ID="ddlYear"
                        Width="300px" Height="40px" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </p>
                <asp:Button ID="cmdSubmit" runat="server" Text="Sign in" OnClick="cmdSubmit_Click">
                </asp:Button>
                <p style="text-align: center; font-size: x-small">
                    Version : 26.04.02 V2 Dated : 26-Apr-2020
                </p>
            </fieldset>
        </div>
        <div style="text-align: center" class="banner">
            <a href="http://www.ierp.in/" style="color: #555">
                <img src="images/DeltaIPL-T.png" height="58px" width="145px" /></a><br />
            <asp:Image runat="server" ID="imgflag" ImageUrl="~/images/Flag.png" Width="20px"
                Height="12px" />&nbsp;Design in India
        </div>
        <div class="banner" style="text-align: center; font-size: x-small">
            B-1010, Infinity Tower, S.G. Highway, Ahmedabad - 15, Gujarat.
            || <a href="http://SchooliERP.com/"  >www.SchooliERP.com</a>
        </div>
        <!-- end #content -->
        </form>
        <!-- end #footer -->
    </div>
</body>
</html>
