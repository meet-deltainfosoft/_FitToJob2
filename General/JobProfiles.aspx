<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="JobProfiles.aspx.cs" Inherits="General_JobProfiles" %>

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


        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Job Profile - Values (Filter)
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
                        <asp:Label runat="server" ID="lblDepartmentId" Text="Department :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:DropDownList runat="server" ID="ddlDepartmentId" AutoPostBack="true" OnSelectedIndexChanged="ddlStdId_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td class="style1">
                        <asp:Label runat="server" ID="lblDesignationId" Text="Designation :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:DropDownList runat="server" ID="ddlDesignationId" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="display: none;">
                    <td align="left" class="style1">
                        <asp:Label runat="server" ID="lblDivisionId" Text="Division :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:DropDownList runat="server" ID="ddlDivisionId">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblStaffCategoryId" Text="StaffCategory :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStaffCategoryId">
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
                        <asp:Button runat="server" ID="btnExcel" Text="Export To Excel" OnClick="btnExcel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="form" runat="server" id="divDesignation">
        <div class="formHeader">
            Job Profile - Values (List)
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
                    <asp:HyperLinkField DataTextField="Department" HeaderText="Department">
                        <HeaderStyle HorizontalAlign="Left" Width="7%" />
                        <ItemStyle HorizontalAlign="Left" Width="7%" />
                    </asp:HyperLinkField>
                    
                    <asp:BoundField HeaderText="Designation" DataField="Designation">
                        <HeaderStyle HorizontalAlign="left" Width="15%" />
                        <ItemStyle HorizontalAlign="left" Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="StaffCategory" DataField="StafCategory">
                        <HeaderStyle HorizontalAlign="left" Width="15%" />
                        <ItemStyle HorizontalAlign="left" Width="15%" />
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
