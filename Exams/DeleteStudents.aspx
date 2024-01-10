<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="DeleteStudents.aspx.cs" Inherits="Exams_DeleteStudents" Title="Delete Employee Filter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

        });
        function SelectheaderCheckboxes(headerchk) {

            var gvcheck = document.getElementById("<%=gdvStudents.ClientID%>");

            var i;
            if (headerchk.checked) {
                for (i = 1; i < gvcheck.rows.length; i++) {
                    var inputs = gvcheck.rows[i].getElementsByTagName('input');
                    inputs[0].checked = true;
                }
            }
            else {
                for (i = 1; i < gvcheck.rows.length; i++) {
                    var inputs = gvcheck.rows[i].getElementsByTagName('input');
                    inputs[0].checked = false;
                }
            }
        }

        function Selectchildcheckboxes(header, gridviewid) {
            var ck = header;
            var count = 0;
            var gvcheck = document.getElementById(gridviewid);
            var headerchk = document.getElementById(header);
            var rowcount = gvcheck.rows.length;
            for (i = 1; i < gvcheck.rows.length; i++) {
                var inputs = gvcheck.rows[i].getElementsByTagName('input');
                if (inputs[1].checked) {
                    count++;
                }
            }
            if (count == rowcount - 1) {
                headerchk.checked = true;
            }
            else {
                headerchk.checked = false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Employee (Filter)
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblStandardTextListId" runat="server" Text="Department :&lt;em&gt;*&lt;/em&gt;"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:DropDownList runat="server" ID="ddlStandardTextListId" Width="394px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRegistrationName" runat="server" Text="Employee Name :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtRegistrationName" Width="390px" MaxLength="150"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSchoolName" runat="server" Text="Company Name Like :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSchoolName" Width="390px" MaxLength="150"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCity" runat="server" Text="City Like  :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCity" Width="390px" MaxLength="150"></asp:TextBox>
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
        <div class="formHeader" style="height: 40px;">
            <table width="100%">
                <tr>
                    <td>Employee (List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Button runat="server" ID="btnDeleteAll" Text="Delete Select Employee" Style="background-color: Red;"
                            OnClientClick="return confirm('Are you sure want to delete all this selected Employee?');"
                            OnClick="btnDeleteAll_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formBody">
            <asp:GridView runat="server" ID="gdvStudents" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvStudents_RowDataBound" DataKeyNames="RegistrationId">
                <Columns>
                    <asp:HyperLinkField DataTextField="StudentName" HeaderText="Employee Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="Department" DataField="Standard">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Mobile No" DataField="MobileNo">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="CompanyName" DataField="SchoolName">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="City" DataField="City">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Is Deactive" DataField="IsDeActive">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Created On" DataField="InsertedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Last Updated On" DataField="LastUpdatedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSendPendingFeeSMSAll" runat="server" Checked="true" onclick="javascript:SelectheaderCheckboxes(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSendPendingFeeSMS" runat="server" Checked="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="formFooter">
            <table>
                <tr>
                    <td width="776" align="right">
                        <asp:Button runat="server" ID="btnShowAllRecords" Text="Show All Records" OnClick="btnShowAllRecords_Click"
                            Visible="false"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
