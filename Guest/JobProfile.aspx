<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="JobProfile.aspx.cs"
    Inherits="General_JobProfile" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">--%>
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
    <link type="text/css" href="../jquery/timepicker/jquery.ui.timepicker.css" />
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"
        integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"
        crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.0.8/dist/umd/popper.min.js"
        integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"
        integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous"></script>
    <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"
        integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"
        crossorigin="anonymous"
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.0.8/dist/umd/popper.min.js"
        integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"
        integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous"></script>--%>
    <%--  <script type="text/javascript">
        $(document).ready(function () {




        });
    </script>--%>
    <%--</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    --%>
    <style>
        /* Custom CSS styles for your form */
        body
        {
            font-family: 'Arial' , sans-serif;
            background-color: #37C1BB;
        }
        
        .form
        {
            background-color: #ffffff;
            border: 1px solid #dee2e6;
            padding: 20px;
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
        }
        
        .form-group
        {
            margin-bottom: 15px;
        }
        
        .custom-button
        {
            background-color: #37C1BB;
            color: #ffffff; /* Text color, you can adjust it based on your preference */
        }
        
        
        /* Add more custom styles as needed */
    </style>
</head>
<body>
    <form id="formJobProfile" runat="server">
    <div class="form">
        <div class="formHeader">
            Job Profile Entry
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label></div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <div class="row">
                <div class="col-lg-4 col-sm-12" style="display: none;">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblDepartmentId" Text="Department :"></asp:Label>
                         <span style="color:red">*</span>
                        <asp:DropDownList runat="server" ID="ddlDepartmentId" AutoPostBack="true" TabIndex="1"
                            OnSelectedIndexChanged="ddlStdId_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-4 col-sm-12" style="display: none;">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblDesignationId" Text="Designation :"></asp:Label>
                         <span style="color:red">*</span>
                        <asp:DropDownList runat="server" ID="ddlDesignationId" TabIndex="2" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-4 col-sm-12">
                    <div class="form-group" style="display: none;">
                        <asp:Label runat="server" ID="lblDivisionId" Text="Division :"></asp:Label>
                         <span style="color:red">*</span>
                        <asp:DropDownList runat="server" ID="ddlDivisionId" TabIndex="3" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-12 col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblStaffCategoryId" Text="Staff Category :"
                            Font-Bold="true"></asp:Label>
                             <span style="color:red">*</span>
                            <br />
                        <asp:DropDownList runat="server" Visible="false" ID="ddlStaffCategoryId">
                        </asp:DropDownList>
                        <asp:CheckBox ID="chkallStaffCategory" runat="server" AutoPostBack="True" OnCheckedChanged="chkallStaffCategory_CheckedChanged"
                            Text="Select All" Font-Bold="true" />
                        <asp:CheckBoxList runat="server" Style="margin-left: 15px" ID="chkStaffCategory"
                            TabIndex="4" AutoPostBack="True">
                        </asp:CheckBoxList>
                    </div>
                </div>
                <div class="col-lg-4 col-sm-12">
                    <div class="form-group" style="display: none;">
                        <asp:Label runat="server" ID="lblNoOfSeats" Text="NoOfSeats :"></asp:Label>
                        <asp:TextBox runat="server" ID="txtNoOfSeats" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4 col-sm-12">
                    <div class="form-group" style="display: none;">
                        <asp:Label runat="server" ID="lblValidfrom" Text="Valid From :"></asp:Label>
                        <asp:TextBox runat="server" ID="txtValidfrom" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4 col-sm-12">
                    <div class="form-group" style="display: none;">
                        <asp:Label runat="server" ID="lblValidto" Text="Valid To :"></asp:Label>
                        <asp:TextBox runat="server" ID="txtValidto" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <div class="formFooter">
            <div class="row">
                <div class="col-lg-8 m-1">
                    <asp:Button runat="server" ID="btnOk" Text="OK" OnClick="btnOk_Click" class="btn custom-button"
                        Style="height: 30px; width: 200px; font-size: 15px;" />
                    <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"
                        class="btn custom-button" Style="height: 30px; width: 80px; font-size: 15px;" />
                </div>
                <div class="col-lg-2 m-1">
                    <asp:Button runat="server" ID="btnDelete" Enabled="false" Text="Delete" class="btn btn-danger"
                        OnClientClick="return confirm('Do you Want to Delete');" OnClick="btnDelete_Click"
                        Style="height: 30px; width: 80px; font-size: 15px;" />
                </div>
            </div>
            <div class="row m-2">
                <asp:label id="lblMessage" runat="Server" text="Please Select at Least one Category"
                    forecolor="Red" visible="false"></asp:label>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
<%--</asp:Content>
--%>