<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewResultDetail.aspx.cs"
    Inherits="General_ViewResultDetail" Title="View Result Detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Result Detail</title>
    <link href="../css/cloud-admin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <script src="../jQuery/jquery-1.12.4.js" type="text/javascript"></script>
    <link href="../jQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../jQuery/jquery-ui.js" type="text/javascript"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700' rel='stylesheet'
        type='text/css' />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <br />
        <div class="row text-center">
            <div class="col-md-6 col-md-offset-3">
                <h4>
                    Result Detail</h4>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-md-offset-3">
                <h4>
                    <asp:Label runat="server" ID="lblCandidateName" Text="Dear "></asp:Label></h4>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-md-offset-3">
                <div class="row">
                    <div class="col-md-6">
                        <h4>
                            <asp:Label runat="server" CssClass="text-primary" ID="lblTotalQuestion" Text="Total Questions : "></asp:Label></h4>
                        <h4>
                            <asp:Label runat="server" CssClass="text-primary" ID="lblAttemptedAns" Text="Total attempted answers : "></asp:Label></h4>
                        <h4>
                            <asp:Label runat="server" CssClass="text-primary" ID="lblCorrectAns" Text="Total attempted Correct answers : "></asp:Label></h4>
                        <h4>
                            <asp:Label runat="server" CssClass="text-danger" ID="lblWrongAns" Text="Total attempted Wrong answers : "></asp:Label></h4>
                    </div>
                    <div class="col-md-6">
                        <h2 class="text-center">
                            <asp:Label runat="server" CssClass="text-green" ID="TotalAchieved" Text="Result is : "></asp:Label></h2>
                        <h2 class="text-center">
                            <asp:Label runat="server" CssClass="text-green" ID="lblStatus" Text="You're " Visible="false"></asp:Label></h2>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 col-md-offset-3">
                <h4>
                    <asp:Label runat="server" ID="lblMobileNo" Text="Mobile No : "></asp:Label></h4>
                <h4>
                    <asp:Label runat="server" ID="lblStandard" Text="Department : "></asp:Label></h4>
                <h4>
                    <asp:Label runat="server" ID="lblSubject" Text="Designation : "></asp:Label></h4>
                <h4>
                    <asp:Label runat="server" ID="lblTest" Text="Test Name : "></asp:Label></h4>
            </div>
        </div>
        <br />
        <div class="row">
            <asp:PlaceHolder ID="plRpt" runat="server"></asp:PlaceHolder>
        </div>
        <br />
        <div class="row text-center">
            <div class="col-md-6 col-md-offset-3">
                <asp:Button ID="btnBack" CssClass="btn btn-primary" runat="server" Text="GO Back"
                    Visible="false" OnClick="btnBack_Click" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
