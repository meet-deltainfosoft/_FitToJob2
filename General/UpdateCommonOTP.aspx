<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="UpdateCommonOTP.aspx.cs" Inherits="General_UpdateCommonOTP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtOTP.ClientID%>").numeric();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Update Common OTP
        </div>
        <div class="formBody">
            Common OTP - Emplyoee :
            <asp:TextBox runat="server" ID="txtOTP" MaxLength="4"></asp:TextBox>
            <asp:HiddenField ID="hfID" runat="server" />
            <br />
            <br />
            Common OTP - Emplyoee :
            <asp:TextBox runat="server" ID="txtOTPTeacher" MaxLength="4"></asp:TextBox>
            <asp:HiddenField ID="hfOTPTeacher" runat="server" />
        </div>
        <div class="formFooter">
            <asp:Button runat="server" ID="btnUpdate" Text="Update OTP" OnClick="btnUpdate_Click" />
        </div>
    </div>
</asp:Content>
