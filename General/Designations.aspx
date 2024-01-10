<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="Designations.aspx.cs" Inherits="General_Designations" %>

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
    <script type="text/javascript">
        $(document).ready(function () {
            AddTHEAD("<%=gdvDesignations.ClientID%>");
            $("#<%=gdvDesignations.ClientID%>").tablesorter({ locale: 'en', widgets: ['zebra'] });

            $("#<%=txtName.ClientID%>").numeric();
        });
        function AddTHEAD(tableName) {
            var table = document.getElementById(tableName);
            if (table != null) {
                var head = document.createElement("THEAD");
                head.appendChild(table.rows[0]);
                table.insertBefore(head, table.childNodes[0]);
            }
        } 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Designation - Values (Filter)
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
                    <td style="width: 130px">
                        <asp:Label runat="server" ID="lblName" Text="Desigantion Name:"></asp:Label>
                    </td>
                    <td style="width: 308px">
                        <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        <asp:Label runat="server" ID="lblDeptId" Text="Department:"></asp:Label>
                    </td>
                    <td style="width: 308px">
                        <asp:DropDownList runat="server" ID="ddlDeptId" CssClass="form-control">
                        </asp:DropDownList>
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
                        <asp:Button runat="server" ID="btnExcel" Text="Export To Excel" class="btn btn-info"
                            OnClick="btnExcel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="form" runat="server" id="divDesignation" >
        <div class="formHeader">
            Designation - Values (List)
            <asp:Label runat="server" ID="lblRecordStatus" Style="float: right;"></asp:Label></div>
        <div class="formBody" style="overflow: auto">
            <asp:GridView runat="server" ID="gdvDesignations" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvDesignations_RowDataBound" Width="100%" OnPageIndexChanging="gdvDesignations_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="5%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" Width="5%" />
                    </asp:TemplateField>
                    <asp:HyperLinkField DataTextField="Name" HeaderText="Name">
                        <HeaderStyle HorizontalAlign="Left" Width="10%" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="Department" DataField="DeptName">
                        <HeaderStyle HorizontalAlign="left" Width="15%" />
                        <ItemStyle HorizontalAlign="left" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Reporting Designation" DataField="ReportingDesign">
                        <HeaderStyle HorizontalAlign="left" Width="15%" />
                        <ItemStyle HorizontalAlign="left" Width="15" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Created On" DataField="InsertedOn" DataFormatString="{0:dd-MMM-yy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="left" Width="10%" />
                        <ItemStyle HorizontalAlign="left" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Updated On" DataField="LastUpdatedOn" DataFormatString="{0:dd-MMM-yy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="left" Width="10%" />
                        <ItemStyle HorizontalAlign="left" Width="10%" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <%--   </asp:Panel>--%>
            <i style="float: right;">You are viewing page
                <%=gdvDesignations.PageIndex + 1%>
                of
                <%=gdvDesignations.PageCount%>
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
