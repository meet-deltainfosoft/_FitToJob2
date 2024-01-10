<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="Registrations1.aspx.cs"
    Inherits="General_Registrations1" %>

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
    <link type="text/css" href="../jQuery/Timepicker/jquery.ui.timepicker.css" />
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
    <%--</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">--%>
    <style>
        /* Custom CSS styles for your form */
        body {
            font-family: 'Arial', sans-serif;
         <%--   background-color: #37C1BB;--%>
        }

        .form {
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

        .form-group {
            margin-bottom: 15px;
        }
        
   .custom-button {
        background-color: #37C1BB;
        color: #ffffff; /* Text color, you can adjust it based on your preference */
    }

       
        /* Add more custom styles as needed */
    </style>
</head>
<body>
    <form id="Registrations1" runat="server" method="get">
    <div class="form">
        <div class="formHeader">
            Registration - Values (Filter)
        </div>
        <asp:Panel runat="server" ID="pnlErr" Visible="false" CssClass="errors">
            <asp:BulletedList runat="server" ID="blErrs">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <div class="row">
                <div class="col-lg-4 col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblAadharNo" Text="AadharCard No :"></asp:Label>
                         <span style="color:red">*</span>
                        <asp:TextBox runat="server" ID="txtAadharNo" CssClass="form-control" MaxLength="12"
                            TabIndex="1" AutoPostBack="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4 col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblFirstname" Text="First Name :"></asp:Label>
                         <span style="color:red">*</span>
                        <asp:TextBox runat="server" ID="txtFirstname" CssClass="form-control" TabIndex="2"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4 col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblMiddlename" Text="Middle Name :"></asp:Label>
                         <span style="color:red">*</span>
                        <asp:TextBox runat="server" ID="txtMiddlename" CssClass="form-control" TabIndex="3"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4 col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblLastName" Text="Last Name :"></asp:Label>
                         <span style="color:red">*</span>
                        <asp:TextBox runat="server" ID="txtLastname" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4 col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblMobileNo" Text="Mobile No :"></asp:Label>
                         <span style="color:red">*</span>
                        <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control" TabIndex="5"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMobileNo"
                            ErrorMessage="Please enter valid Mobile Number" Display="Dynamic" ForeColor="Red"
                            ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
        </div>
        <div class="formFooter">
            <div class="row">
                <div class="col-lg-4">
                    <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click"
                        class="btn custom-button" Style="height: 30px; width: 70px; font-size: 15px;" />
                    <asp:Button runat="server" ID="btnExcel" Text="Export To Excel" Style="height: 30px;
                        width: 150px; font-size: 15px;" class="btn custom-button" OnClick="btnExcel_Click" />
                </div>
            </div>
        </div>
        <div>
            <div class="container mt-4">
                <div class="row">
                    <div class="col-md-12">
                        <div class="formHeader">
                            Registration - Values (List)
                            <asp:Label runat="server" ID="lblRecordStatus" Style="float: right;"></asp:Label></div>
                    </div>
                    <div class="formBody" style="overflow: auto">
                        <asp:GridView ID="gdvEmps" runat="server" AutoGenerateColumns="False" SkinID="Lns"
                            CssClass="table table-bordered table-striped" OnRowDataBound="gdvEmps_RowDataBound"
                            Width="100%" OnPageIndexChanging="gdvEmps_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="3%" />
                                    <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" Width="3%" />
                                </asp:TemplateField>
                                <asp:HyperLinkField DataTextField="AadharCardNo" HeaderText="Aadharcard No." SortExpression="AadharCardNo">
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                    <ItemStyle HorizontalAlign="Left" Width="7%" />
                                </asp:HyperLinkField>
                                <asp:BoundField HeaderText="FirstName" DataField="FirstName" SortExpression="FirstName">
                                    <HeaderStyle HorizontalAlign="left" Width="10%" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="MiddleName" DataField="MiddleName" SortExpression="MiddleName">
                                    <HeaderStyle HorizontalAlign="left" Width="15%" />
                                    <ItemStyle HorizontalAlign="left" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="LastName" DataField="LastName" SortExpression="LastName">
                                    <HeaderStyle HorizontalAlign="left" Width="15%" />
                                    <ItemStyle HorizontalAlign="left" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="MobileNo" DataField="MobileNo" SortExpression="MobileNo">
                                    <HeaderStyle HorizontalAlign="left" Width="10%" />
                                    <ItemStyle HorizontalAlign="left" Width="10%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Created On" DataField="InsertedOn" DataFormatString="{0:dd-MMM-yy hh:mm tt}">
                                    <HeaderStyle HorizontalAlign="left" Width="20%" />
                                    <ItemStyle HorizontalAlign="left" Width="20%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Updated On" DataField="LastUpdatedOn" DataFormatString="{0:dd-MMM-yy hh:mm tt}">
                                    <HeaderStyle HorizontalAlign="left" Width="20%" />
                                    <ItemStyle HorizontalAlign="left" Width="20%" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <i class="float-right">You are viewing page
                            <%=gdvEmps.PageIndex + 1%>
                            of
                            <%=gdvEmps.PageCount%></i>
                        <div id="Div1" class="formFooter" runat="server" visible="true">
                            <div class="row">
                                <div class="col-md-12 text-right">
                                    <asp:Button runat="server" ID="btnShowAllRecords" Text="Show All Records" class="btn custom-button"
                                        OnClick="btnShowAllRecords_Click" CssClass="btn btn-primary"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
