<%@ Page Title="Video No || List" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="VideoNos.aspx.cs" Inherits="General_VideoNos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            AddTHEAD("<%=gdvChapterLink.ClientID%>");
            $("#<%=gdvChapterLink.ClientID%>").tablesorter({ locale: 'en', widgets: ['zebra'] });
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
            Video No (Filter)</div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblStandard" runat="server" Text="Department :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStandard" Width="200px" TabIndex="1" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="padding-left: 50px">
                        <asp:Label runat="server" ID="lblSubs" Text="Designation :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSubs" Width="200px" TabIndex="2" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlSubs_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="padding-left: 50px">
                        <asp:Label runat="server" ID="lblChapter" Text="Chapter :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlChapter" Width="200px" TabIndex="3" OnSelectedIndexChanged="ddlChapter_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="padding-left: 50px">
                        <asp:Label runat="server" ID="lblPeriodNo" Text="Chapter No :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlPeriodNo" Width="100px" TabIndex="4">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 778px">
                        <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" />
                        <asp:Button runat="server" ID="btnExcel" Text="Excel" OnClick="btnExcel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="form">
        <div class="formHeader">
            Video No (List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"></asp:Label></div>
        <div class="formBody" style="overflow: auto">
            <asp:GridView runat="server" ID="gdvChapterLink" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvChapterLink_RowDataBound" Width="100%">
                <Columns>
                    <asp:HyperLinkField DataTextField="ChapterName" HeaderText="Chapter Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="Department Name" DataField="StandardName">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Designation Name" DataField="SubjectName">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Chapter No" DataField="PeriodNo" DataFormatString="{0:N0}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Created On" DataField="InsertedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Last Updated On" DataField="LastUpdatedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="formFooter">
            <table>
                <tr>
                    <td width="776" align="right">
                        <asp:Button runat="server" ID="btnShowAllRecords" Text="Show All Records" OnClick="btnShowAllRecords_Click">
                        </asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
