<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="Test.aspx.cs" Inherits="Exams_Test" Title="Test Entry " %>

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
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/jQuery.UI/jquery.ui.core.js"></script>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"
        integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"
        crossorigin="anonymous">
    <!-- Bootstrap JS and Popper.js (required for Bootstrap dropdowns, modals, etc.) -->
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.0.8/dist/umd/popper.min.js"
        integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"
        integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
    <style>
        body
        {
            font-family: 'Arial' , sans-serif;
        }
        
        .form
        {
            background-color: #ffffff;
            border: 1px solid #dee2e6;
            padding: 10px;
            margin: 20px;
            border-radius: 5px;
        }
        .formHeader
        {
            background-color: #37C1BB;
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
            color: #ffffff;
            margin-top: 20px;
        }
        .form-group
        {
            margin-bottom: 15px;
        }
        .custom-button
        {
            background-color: #37C1BB;
            color: #ffffff;
        }
        .container
        {
             max-width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="formHeader">
            Test
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <div class="row ">
                <div class="col-lg-3 col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblStdId" Text="Department :<em>*</em>" CssClass="label"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlStdId" AutoPostBack="true" CssClass="form-control"
                            OnSelectedIndexChanged="ddlStdId_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-3 col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblSubId" Text="Designation :<em>*</em>" CssClass="label"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlSubId" AutoPostBack="true" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-3 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblTestName" runat="server" Text="Test name" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtTestName" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-9 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblRemark" runat="server" Text="Remark:"></asp:Label>
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="row m-2">
        </div>
        <div class="formFooter">
            <div class="row justify-content-center align-items-center">
                <div class="col-lg-4 m-2 text-center">
                    <button type="button" onclick="btnOK_Click" class="btn btn-primary">
                        OK</button>
                    <button type="button" onclick="btnCancel_Click" class="btn btn-primary">
                        Cancel</button>
                </div>
                <div class="col-lg-2 m-2">
                    <asp:Button runat="server" ID="btnDelete" Visible="false" Text="Delete" class="btn btn-danger"
                        OnClientClick="return confirm('Do you Want to Delete');" OnClick="btnDelete_Click"
                        Style="display: none;" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
