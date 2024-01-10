<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="Registrations1.aspx.cs" Inherits="General_Registrations1" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Registration - Values (Filter)
            <%-- <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" />
            <asp:SiteMapPath ID="SiteMap1" runat="server">
            </asp:SiteMapPath>--%>
        </div>
        <asp:Panel runat="server" ID="pnlErr" Visible="false" CssClass="errors">
            <asp:BulletedList runat="server" ID="blErrs">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td class="style1">
                        <asp:Label runat="server" ID="lblAadharNo" Text="AadharCard No :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtAadharNo" CssClass="form-control" MaxLength="12"
                            TabIndex="4" AutoPostBack="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style1">
                        <asp:Label runat="server" ID="lblFirstname" Text="First Name :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtFirstname" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="left" class="style1">
                        <asp:Label runat="server" ID="lblMiddlename" Text="Middle Name :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtMiddlename" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="left" class="style1">
                        <asp:Label runat="server" ID="lblLastName" Text="Last Name :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtLastname" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblMobileNo" Text="Mobile No :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control" TabIndex="1"
                            MaxLength="10"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtMobileNo"
                            ErrorMessage="Please enter valid Mobile Number" Display="Dynamic" ForeColor="Red"
                            ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 778px">
                        <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" />
                    </td>
                    <td align="right">
                        <asp:Button runat="server" ID="btnExcel" Text="Export To Excel" OnClick="btnExcel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="form" runat="server" id="divDesignation">
        <div class="formHeader">
            Registration - Values (List)
            <asp:Label runat="server" ID="lblRecordStatus" Style="float: right;"></asp:Label></div>
        <div class="formBody" style="overflow: auto">
            <asp:GridView runat="server" ID="gdvEmps" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvEmps_RowDataBound" Width="100%" OnPageIndexChanging="gdvEmps_PageIndexChanging">
                <Columns>
                                        <asp:TemplateField HeaderText="No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="3%" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" Width="3%" />
                                        </asp:TemplateField>
                                        <asp:HyperLinkField DataTextField="AadharCardNo" HeaderText="Aadharcard No.">
                                            <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                            <ItemStyle HorizontalAlign="Left" Width="7%" />
                                        </asp:HyperLinkField>
                                        <asp:BoundField HeaderText="FirstName" DataField="FirstName">
                                            <HeaderStyle HorizontalAlign="left" Width="10%" />
                                            <ItemStyle HorizontalAlign="left" Width="10%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="MiddleName" DataField="MiddleName">
                                            <HeaderStyle HorizontalAlign="left" Width="15%" />
                                            <ItemStyle HorizontalAlign="left" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="LastName" DataField="LastName">
                                            <HeaderStyle HorizontalAlign="left" Width="15%" />
                                            <ItemStyle HorizontalAlign="left" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="MobileNo" DataField="MobileNo">
                                            <HeaderStyle HorizontalAlign="left" Width="10%" />
                                            <ItemStyle HorizontalAlign="left" Width="10%" />
                                        </asp:BoundField>
                                       <%-- <asp:BoundField HeaderText="Designation" DataField="Designation">
                                            <HeaderStyle HorizontalAlign="left" Width="10%" />
                                            <ItemStyle HorizontalAlign="left" Width="10%" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField HeaderText="Created On" DataField="InsertedOn" DataFormatString="{0:dd-MMM-yy hh:mm tt}">
                                            <HeaderStyle HorizontalAlign="left" Width="15%" />
                                            <ItemStyle HorizontalAlign="left" Width="15%" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Updated On" DataField="LastUpdatedOn" DataFormatString="{0:dd-MMM-yy hh:mm tt}">
                                            <HeaderStyle HorizontalAlign="left" Width="15%" />
                                            <ItemStyle HorizontalAlign="left" Width="15%" />
                                        </asp:BoundField>
                                        
                                    </Columns>
            </asp:GridView>
            <%--   </asp:Panel>--%>
            <i style="float: right;">You are viewing page
                <%=gdvEmps.PageIndex + 1%>
                of
                <%=gdvEmps.PageCount%>
            </i>
        </div>
        <div id="Div1" class="formFooter" runat="server" visible="true">
            <table>
                <tr>
                    <td width="776" align="right">
                        <asp:Button runat="server" ID="btnShowAllRecords" Text="Show All Records" OnClick="btnShowAllRecords_Click"
                            Visible="true"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
