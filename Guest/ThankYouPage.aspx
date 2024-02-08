<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ThankYouPage.aspx.cs" Inherits="Guest_ThankYouPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <title>Thank You Page</title>
    <link rel="shortcut icon" href="images/favicon.ico" type="image/x-icon" />
    <link rel="icon" href="images/favicon.ico" type="image/x-icon" />
    <link href="CSS\Pictures.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript"></script>
    <link href="css/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="css/css/font-awesome1.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.0/jquery.min.js"></script>
    <style type="text/css">
        @media screen and (max-width: 768px)
        {
            .MainPage
            {
                margin-top: 0;
            }
        }
        body
        {
            overflow-x: hidden;
        }
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
            text-align: center;
        }
        .MainPage
        {
            margin-top: -5%;
            font-family: Verdana;
        }
         </style>
</head>
<body>
    <div class="centered">
       
        <img src="~/images/_JobApply12-1.jpg" alt="" />
        <asp:Image ID="Image1" runat="server" ImageUrl="~/images/_JobApply12-1.jpg" />
        <a href="SelectLanguage.aspx" class="MainPage" style="">Go to Main page</a>
    </div>
</body>
</html>
