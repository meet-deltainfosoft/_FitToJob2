<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true" CodeFile="Firstinterview.aspx.cs" Inherits="Exams_First_interview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="form">
        <div class="formHeader">
            First Interview (Filter)</div>
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
                <tr>
                    <td>
                        <asp:Label ID="lblCity" runat="server" Text="City :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtCity" Width="390px" TabIndex="2" MaxLength="150"></asp:TextBox>
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
                        <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" />
                        <asp:Button runat="server" ID="btnExcel" Text="Export to Excel" OnClick="btnExcel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="form">
        <div class="formHeader">
            First Interview (List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"></asp:Label></div>
        <div class="formBody" style="overflow: auto">
            <asp:GridView runat="server" ID="gdvFirstInterview" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvFirstInterview_RowDataBound" Width="100%" DataKeyNames="RegistrationId">
                <Columns>
                    
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="3%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" Width="3%" />
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Aadhar CardNo." DataField="AadharCardNo">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
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
                    <asp:BoundField HeaderText="Last Company Name" DataField="LastCompany">
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
                    <asp:HyperLinkField Text="InterView" HeaderText="InterView Assessment" Target="_blank">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
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

