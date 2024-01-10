<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="Registrations.aspx.cs" Inherits="Exams_Registrations" Title="Registration Filter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            AddTHEAD("<%=gdvRegistrations.ClientID%>");
            $("#<%=gdvRegistrations.ClientID%>").tablesorter({ locale: 'en', widgets: ['zebra'] });
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
            Registration (Filter)</div>
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
                        <asp:DropDownList runat="server" ID="ddlStandard" Width="396px" TabIndex="1">
                        </asp:DropDownList>
                    </td>
                    <td rowspan="6">
                        <asp:Label ID="Label1" runat="server" Font-Size="X-Large" 
                            Text="No. of Employee :"></asp:Label>
                    </td>
                    <td rowspan="6">
                        <asp:Label ID="lblCountStudents" runat="server" Font-Bold="True" 
                            Font-Size="XX-Large" ForeColor="Red" Text="1000"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDivision" runat="server" Text="Division :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDivision" Width="396px" TabIndex="1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblRegistrationName" runat="server" Text="Employee Name :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtRegistrationName" Width="390px" TabIndex="2" MaxLength="150"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSchoolName" runat="server" Text="Company Name Like :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtSchoolName" Width="390px" TabIndex="2" MaxLength="150"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCity" runat="server" Text="City Like  :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCity" Width="390px" TabIndex="2" MaxLength="150"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblMobileNo" Text="Mobile No :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMobileNo" MaxLength="15" Width="150px" TabIndex="3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblExtraMobileNo" Text="Extra Mobile No :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtExtraMobileNo" MaxLength="15" Width="150px" TabIndex="3"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkIsDeactive" Width="390px" TabIndex="4" Text=" Is De-Active ?">
                        </asp:CheckBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 778px">
                        <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" />
                        <asp:Button runat="server" ID="btnExcel" Text="Export to Excel" OnClick="btnExcel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="form">
        <div class="formHeader">
            Registration (List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"></asp:Label></div>
        <div class="formBody" style="overflow: auto">
            <asp:GridView runat="server" ID="gdvRegistrations" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvRegistrations_RowDataBound" Width="100%">
                <Columns>
                    <asp:HyperLinkField DataTextField="StudentName" HeaderText="Employee Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="Department" DataField="Standard">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Job Profile" DataField="JobProfile">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
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
