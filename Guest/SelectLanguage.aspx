<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SelectLanguage.aspx.cs" Inherits="Guest_SelectLanguage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            color: #37C1BB !important;
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
                    Select Language</h3>
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
        <div class="row">
            <div class="col-lg-12 col-sm-12">
                <asp:Label runat="server" ID="lblLanguage" Text="Select Language*"></asp:Label>
                <asp:DropDownList ID="ddlSelectLanguage" runat="server" class="form-control" color="#37C1BB" style="padding:4px;">
                    <asp:ListItem Value="English" Text="English"></asp:ListItem>
                    <asp:ListItem Value="Gujarati" Text="ગુજરાતી"></asp:ListItem>
                    <asp:ListItem Value="Hindi" Text="हिंदी"></asp:ListItem>
                    
                </asp:DropDownList>
            </div>
        </div>
        <div class="row justify-content-center mt-3">
            <div class="col-lg-4 col-sm-12">
                <asp:LinkButton runat="server" ID="lnkBtnLanguage" CssClass="btn btn-info buttton" OnClick="lnkBtnLanguage_Click">
                        <i class="fa fa-chevron-circle-right" ></i> Select Language
                </asp:LinkButton>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
