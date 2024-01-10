<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ThankYouPage.aspx.cs" Inherits="Guest_ThankYouPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <style type="text/css">
        img
        {
            max-width: 100%;
            height: auto;
        }
        a
        {
            display: inline-block;
            padding: 10px 20px;
            background-color: #4CAF50;
            color: #ffffff;
            text-decoration: none;
            font-size: 16px;
            border-radius: 5px;
            margin-top: 20px;
        }
        
        a:hover
        {
            background-color: #45a049;
        }
        .centered
        {
            text-align: center;</style>
    <title>Thank You Page</title>
</head>
<body>
    <div class="centered">
        <h1>
        </h1>
        <img src="~/images/ThankYou.jpg" alt="" />
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/_JobApply12-1.jpg" />
        <a href="SelectLanguage.aspx" style="">Go to Main page</a>
    </div>
</body>
</html>
