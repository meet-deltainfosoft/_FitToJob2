<%@ Page Title="Master Value Filter" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="MasterValues.aspx.cs" Inherits="General_MasterValues" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <%-- <script src="../js/ScrollableGridPlugin.js" type="text/javascript"></script>--%>
    <%--  <script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        var j = jQuery.noConflict();
    </script>
    <%-- <script type = "text/javascript">
        j(document).ready(function () {  // Datagrid Header Freeze
            j('#<%=GridView1.ClientID %>').Scrollable({
                ScrollHeight: 500
            });
            j('#<%=gdvMasterValues.ClientID %>').Scrollable({
                ScrollHeight: 450
            });
        });
</script>--%>
    <script type="text/javascript">
        j(document).ready(function () {  // Datagrid Header Freeze
            //          
            j('#<%=gdvMasterValues.ClientID %>').Scrollable({
                ScrollHeight: 500
            });
        });
    </script>
    <script type="text/javascript">
        j(document).ready(function () {  // Datagrid Header Freeze
            //          
            j('#<%=gdvMasterValues.ClientID %>').Scrollable({
                ScrollHeight: 450
            });
        });
    </script>
    <script type="text/javascript">
        j(document).ready(function () {

            AddTHEAD("<%=gdvMasterValues.ClientID%>");
            j("#<%=gdvMasterValues.ClientID%>").tablesorter({ locale: 'en', widgets: ['zebra'], headers: { 2: { sorter: 'DD-MMM-YY' }, 3: { sorter: 'DD-MMM-YY'}} });
        });

        function AddTHEAD(tableName) {
            var table = document.getElementById(tableName);
            if (table != null) {
                var head = document.createElement("THEAD");
                head.appendChild(table.rows[0]);
                table.insertBefore(head, table.childNodes[0]);
            }
        }
        function myFunc(event, Url) {
            if (false == j("#DivInfo").is(':visible')) {
                j("#DivInfo").css({
                    top: '30%',
                    left: '35%'
                }).show();

                j('#<%=ifHistory.ClientID%>').attr('src', Url);
            }
            else {
                j("#DivInfo").hide();
            }
        }     
    </script>
    <%-- <script type="text/javascript">
        var GridId = "<%=gdvMasterValues.ClientID %>";
        var ScrollHeight = 300;

        window.onload = function () {
            debugger;
            var grid = document.getElementById(GridId);
            var gridWidth = grid.offsetWidth;
            var gridHeight = grid.offsetHeight;
            var headerCellWidths = new Array();
            for (var i = 0; i < grid.getElementsByTagName("TH").length; i++) {
                headerCellWidths[i] = grid.getElementsByTagName("TH")[i].offsetWidth;
            }
            grid.parentNode.appendChild(document.createElement("div"));
            var parentDiv = grid.parentNode;

            var table = document.createElement("table");
            for (i = 0; i < grid.attributes.length; i++) {
                if (grid.attributes[i].specified && grid.attributes[i].name != "id") {
                    table.setAttribute(grid.attributes[i].name, grid.attributes[i].value);
                }
            }
            table.style.cssText = grid.style.cssText;
            table.style.width = gridWidth + "px";
            table.appendChild(document.createElement("tbody"));
            table.getElementsByTagName("tbody")[0].appendChild(grid.getElementsByTagName("TR")[0]);
            var cells = table.getElementsByTagName("TH");

            var gridRow = grid.getElementsByTagName("TR")[0];
            for (var i = 0; i < cells.length; i++) {
                var width;
                if (headerCellWidths[i] > gridRow.getElementsByTagName("TD")[i].offsetWidth) {
                    width = headerCellWidths[i];
                }
                else {
                    width = gridRow.getElementsByTagName("TD")[i].offsetWidth;
                }
                cells[i].style.width = parseInt(width - 3) + "px";
                gridRow.getElementsByTagName("TD")[i].style.width = parseInt(width - 3) + "px";
            }
            parentDiv.removeChild(grid);

            var dummyHeader = document.createElement("div");
            dummyHeader.appendChild(table);
            parentDiv.appendChild(dummyHeader);
            var scrollableDiv = document.createElement("div");
            if (parseInt(gridHeight) > ScrollHeight) {
                gridWidth = parseInt(gridWidth) + 17;
            }
            scrollableDiv.style.cssText = "overflow:auto;height:" + ScrollHeight + "px;width:" + gridWidth + "px";
            scrollableDiv.appendChild(grid);
            parentDiv.appendChild(scrollableDiv);
        }
    </script>--%>
    <style type="text/css">
        .btnfld img
        {
            height: 20px;
            width: 20px;
        }
        #DivInfo
        {
            display: none;
            left: 0;
            top: 0;
            position: fixed;
            width: 50%;
            height: 50%;
            box-shadow: 0px 5px 54px 18px gray;
            border: 0px solid gray;
            background: white;
            border-radius: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Master - Values (Filter)
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
                    <td style="width: 80px">
                        <asp:Label runat="server" ID="lblGroup" Text="Master For :"></asp:Label>
                    </td>
                    <td style="width: 308px">
                        <asp:DropDownList runat="server" ID="ddlGroup" Width="306px" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
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
                    <td align="right" style="width: 40%">
                        <asp:ImageButton ID="imgExportToExcel" runat="server" OnClick="imgExportToExcel_Click"
                            TabIndex="3" ImageUrl="../images/Excel.png" Style="padding-top: 8px; padding-bottom: 4px" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="form" runat="server" id="divMasterValues" visible="false">
        <div class="formHeader">
            Master - Values (List)<asp:Label runat="server" ID="lblRecordStatus" Style="float: right;"></asp:Label></div>
        <%--<table style="width: 100%;" class="gridview">
            <tr>
                <td style="width: 5%;" class="gridviewHeader">
                    No
                </td>
                <td style="width: 30%;" class="gridviewHeader">
                    Value
                </td>
                <td style="width: 30%;" class="gridviewHeader">
                    &nbsp;Group
                </td>
                <td style="width: 30%;" class="gridviewHeader">
                    &nbsp;Other
                </td>
                <td style="width: 10%;" class="gridviewHeader">
                    InsertedOn
                </td>
                <td style="width: 20%;" class="gridviewHeader">
                    LastUpdatedOn
                </td>
            </tr>
        </table>--%>
        <div class="formBody">
            <asp:GridView runat="server" ID="gdvMasterValues" AutoGenerateColumns="False" SkinID="Lns"
                EmptyDataText="No Records Founds." OnRowDataBound="gdvMasterValues_RowDataBound"
                PageSize="50" AllowPaging="true" OnPageIndexChanging="gdvMasterValues_PageIndexChanging"
                EnableModelValidation="True" ShowHeader="true">
                <PagerStyle BackColor="#F7F3EF" ForeColor="Red" HorizontalAlign="Right" Font-Size="16px">
                </PagerStyle>
                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last"
                    NextPageText="Next" PreviousPageText="Prev" Position="TopAndBottom" PageButtonCount="5" />
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="5px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:HyperLinkField DataTextField="Text" HeaderText="Value">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField DataTextField="Group" HeaderText="Group">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField DataTextField="Address" HeaderText="Other">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="Created on" DataField="InsertedOn" DataFormatString="{0:dd-MMM-yy}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Last Modified on" DataField="LastUpdatedOn" DataFormatString="{0:dd-MMM-yy}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ID="hlviewHistory" runat="server" CssClass="btnfld" ImageUrl="~/images/History.png"
                                ToolTip="View History">
                            </asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        <ItemStyle Width="20px" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <%--   </asp:Panel>--%>
            <i style="float: right;">You are viewing page
                <%=gdvMasterValues.PageIndex + 1%>
                of
                <%=gdvMasterValues.PageCount%>
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
    <div id="DivInfo">
        <div class="container" style="height: 100%">
            <asp:HyperLink href="" class="js-modal-close close" onclick="myFunc(event,'');">×</asp:HyperLink>
            <iframe id="ifHistory" runat="server" frameborder="0" title="Last Call history."
                width="100%" height="90%" scrolling="no"></iframe>
        </div>
    </div>
</asp:Content>
