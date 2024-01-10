<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="Test.aspx.cs" Inherits="Exams_Test" Title="Test Entry " %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/jQuery.UI/jquery.ui.core.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Test
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblStdId" runat="server" Text="Department :&lt;em&gt;*&lt;/em&gt;"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStdId" AutoPostBack="true" OnSelectedIndexChanged="ddlStdId_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSubId" runat="server" Text="Designation :&lt;em&gt;*&lt;/em&gt;"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSubId">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTestName" runat="server" Text="Test Name  :&lt;em&gt;*&lt;/em&gt;"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTestName" Width="390px" MaxLength="150"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblRemark" Text="Remark :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtRemark" MaxLength="500" Width="390px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 90px" align="left">
                        <asp:Button runat="server" ID="btnDelete" OnClientClick="return confirm('Do you Want to Delete');"
                            Enabled="false" Text="Delete" OnClick="btnDelete_Click" Visible="false" />
                    </td>
                    <td style="width: 686px" align="right">
                        <asp:Button runat="server" ID="btnOK" Text="OK" OnClick="btnOK_Click" TabIndex="8" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
