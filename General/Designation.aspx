<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="Designation.aspx.cs" Inherits="General_Designation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Designation - Value
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label></div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td class="style1">
                        <asp:Label runat="server" ID="lblName" Text="Designation Name:"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtName"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style1">
                        <asp:Label runat="server" ID="lblDept" Text="Department:"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:DropDownList runat="server" ID="ddlDeptId" CssClass="form-control" 
                            Height="16px" Width="146px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblReportingDesign" Text="Reporting Designation:"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlReportingDesignId" 
                            CssClass="form-control" Height="16px" Width="146px">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 90px" align="left">
                        <asp:Button runat="server" ID="btnDelete" Enabled="false" Text="Delete" OnClientClick="return confirm('Do you Want to Delete');"
                            OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 686px" align="right">
                        <asp:Button runat="server" ID="btnOk" Text="OK" OnClick="btnOk_Click" />
                                            <asp:Button runat="server" ID="btnCancel" Text="Cancel"  OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
