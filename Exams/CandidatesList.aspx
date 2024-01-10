<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="CandidatesList.aspx.cs" Inherits="Exams_CandidatesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            CandidatesList (Filter)</div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblDepartment" runat="server" Text="Department :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDepartment" Width="396px" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlStdId_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <%--<td rowspan="6">
                        <asp:Label ID="Label1" runat="server" Font-Size="X-Large" 
                            Text="No. of Students :"></asp:Label>
                    </td>
                    <td rowspan="6">
                        <asp:Label ID="lblCountStudents" runat="server" Font-Bold="True" 
                            Font-Size="XX-Large" ForeColor="Red" Text="1000"></asp:Label>
                    </td>--%>
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
                        <asp:Label ID="lblDesignation" runat="server" Text="Designation :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDesignation" Width="396px" TabIndex="1">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblName" runat="server" Text="Name :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" Width="390px" TabIndex="2" MaxLength="150"></asp:TextBox>
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
                <tr style="display: none;">
                    <td>
                        <asp:Label ID="lblCity" runat="server" Text="City :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCity" Width="390px" TabIndex="2" MaxLength="150"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTaluka" runat="server" Text="Taluka :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTaluka" Width="390px" TabIndex="2" MaxLength="150"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStatus" runat="server" Text="Status :"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton runat="server" ID="rdbALL" Text="ALL" GroupName="Status" />
                        <asp:RadioButton runat="server" ID="rdbSelected" Text="Approved" GroupName="Status" />
                        <asp:RadioButton runat="server" ID="rdbHold" Text="Disapproved" GroupName="Status" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 778px">
                        <asp:CheckBox runat="server" ID="chkSelectAll1" AutoPostBack="true" OnCheckedChanged="chkSelectAll1_CheckedChanged" />
                        <asp:Label runat="server" ID="Label5" Text="Select All" BackColor="#FFCCD9" ForeColor="#3333FF"></asp:Label>
                        <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" />
                        <asp:Button runat="server" ID="btnExcel" Text="Export to Excel" OnClick="btnExcel_Click" />
                        <asp:Button ID="btnUpdateStatus" runat="server" Text="Update" OnClick="btnUpdateStatus_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="form">
        <div class="formHeader">
            CandidatesList (List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"></asp:Label></div>
        <div class="formBody" style="overflow: auto">
            <asp:GridView runat="server" ID="gdvCandidatesList" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvCandidatesList_RowDataBound" Width="100%" DataKeyNames="RegistrationId">
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="3%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" Width="3%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" Width="80px" />
                        <HeaderStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlApprovedDisapproved" OnSelectedIndexChanged="ddlApprovedDisapproved_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="<Select>"></asp:ListItem>
                                <asp:ListItem Value="A" Text="Approved"></asp:ListItem>
                                <asp:ListItem Value="D" Text="Disapproved"></asp:ListItem>
                                
                            </asp:DropDownList>
                            <%--<asp:Label ID="lblStatus" Text="" runat="server"/>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Name" DataField="Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Surname" DataField="Surname">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Taluka" DataField="City">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Job Profile" DataField="JobProfile">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Exam" DataField="TotalMarks">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Distance" DataField="Distance">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Last Company" DataField="LastCompany">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Last Salary" DataField="LastSalary">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Expect Salary" DataField="ExpectSalary">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Exprience Year" DataField="Eaperience">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:HyperLinkField Text="Click" HeaderText="Photo" Target="_blank">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField Text="Click" HeaderText="Self Intro." Target="_blank">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField Text="Download" HeaderText="View/Print" Target="_blank">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                        <HeaderStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                        <ItemTemplate>
                            <asp:TextBox runat="server" placeholder="Reasons should be Compulsory" ID="txtRemarks"
                                MaxLength="1200"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
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
