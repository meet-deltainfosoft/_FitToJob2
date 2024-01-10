<%@ Page Title="Delta Web ERP - User" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="Admin_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            User
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label></div>
        <asp:Panel runat="server" ID="pnlErr" Visible="false" CssClass="errors">
            <asp:BulletedList runat="server" ID="blErrs">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 125px">
                        <asp:Label runat="server" ID="lblFirstName" Text="First Name:<em>*</em>"></asp:Label>
                    </td>
                    <td style="width: 263px">
                        <asp:TextBox runat="server" ID="txtFirstName" Width="257px" AutoCompleteType="None"></asp:TextBox>
                    </td>
                    <td style="width: 125px" align="right">
                        <asp:Label runat="server" ID="lblLastName" Text="Last Name:<em>*</em>"></asp:Label>
                    </td>
                    <td style="width: 263px">
                        <asp:TextBox runat="server" ID="txtLastName" Width="260px" AutoCompleteType="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 125px">
                        <asp:Label runat="server" ID="lblUserName" Text="User Name:<em>*</em>"></asp:Label>
                    </td>
                    <td style="width: 263px">
                        <asp:TextBox runat="server" ID="txtUserName" Width="257px" AutoCompleteType="None"></asp:TextBox>
                    </td>
                    <td style="width: 125px" align="right">
                        <asp:Label runat="server" ID="lblDepartment" Text="Department:" Visible="false"></asp:Label>
                    </td>
                    <td style="width: 263px">
                        <asp:DropDownList ID="ddlDept" runat="server" Width="260px" Visible="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 125px">
                        <asp:Label runat="server" ID="lblPassword" Text="Password:<em>*</em>"></asp:Label>
                    </td>
                    <td style="width: 263px">
                        <asp:TextBox runat="server" ID="txtPassword" Width="257px" TextMode="Password" AutoCompleteType="None"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 125px">
                        <asp:Label runat="server" ID="lblIsDisabled" Text="Disable Account:<em>*</em>"></asp:Label>
                    </td>
                    <td style="width: 263px">
                        <asp:CheckBox runat="server" ID="chkIsDisabled" Width="257px"></asp:CheckBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="form">
            <div class="formHeader">
                Application Roles</div>
            <div class="formBody" style="overflow: auto">
                <asp:Panel ID="pnlAppRoles" runat="server" ScrollBars="Vertical" Width="766px" Height="450px">
                    <asp:GridView ID="gdvAppRoles" runat="server" SkinID="Lns" AutoGenerateColumns="False"
                        Width="746px" DataKeyNames="AppRoleId" TabIndex="2">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Center" Width="40px" />
                                <HeaderStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelectAppRole" runat="server" Checked='<%# Bind("Checked") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="AppRoleName" HeaderText="Application Role">
                                <ItemStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Left" Width="210px" />
                                <HeaderStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Desc" HeaderText="Description">
                                <ItemStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Left" Width="446px" />
                                <HeaderStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <PagerTemplate>
                        </PagerTemplate>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 90px" align="left">
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" Enabled="False" OnClientClick="return confirm('Do you Want to Delete');"
                            OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 686px" align="right">
                        <asp:Button runat="server" ID="btnOK" Text="OK" OnClick="btnOK_Click" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
