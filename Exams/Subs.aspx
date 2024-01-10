<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="Subs.aspx.cs" Inherits="Exams_Subs" Title="Subject Filter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            AddTHEAD("<%=gdvSubs.ClientID%>");
            $("#<%=gdvSubs.ClientID%>").tablesorter({ locale: 'en', widgets: ['zebra'] });
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
            Subjects (Filter)</div>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblStandardTextListId" runat="server" Text="Department :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStandardTextListId" Width="306px" Height="18px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblName" Text="Designation Like:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" Width="500px"></asp:TextBox>
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
                </tr>
            </table>
        </div>
    </div>
    <div class="form">
        <div class="formHeader">
            Subjects (List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"></asp:Label></div>
        <div class="formBody">
            <asp:GridView runat="server" ID="gdvSubs" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvSubs_RowDataBound">
                <Columns>
                    <asp:HyperLinkField DataTextField="Subject" HeaderText="Designation">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="50px" />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="Remarks" DataField="Remarks">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Created On" DataField="InsertedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Last Updated On" DataField="LastUpdatedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
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
