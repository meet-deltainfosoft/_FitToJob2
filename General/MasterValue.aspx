<%@ Page Title="Master Value Entry" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="MasterValue.aspx.cs" Inherits="General_MasterValue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function SetRowValue(eleName, val, txtName, hfTextListId, hfTextListName) {
            debugger;

            window.opener.document.getElementById(hfTextListId).value = val;
            window.opener.document.getElementById(hfTextListName).value = txtName;
            window.opener.document.forms[0].submit();
            window.close();
        }

        function SetRowValue2(eleName, val, txtName, RowIndex, hfTextListId, hfTextListName, hfRowIndex) {
            debugger;

            window.opener.document.getElementById(hfTextListId).value = val;
            window.opener.document.getElementById(hfTextListName).value = txtName;
            window.opener.document.getElementById(hfRowIndex).value = RowIndex;
            window.opener.document.forms[0].submit();
            window.close();
        } 
    </script>
    <style type="text/css">
        .style1
        {
            width: 89px;
        }
        .style2
        {
            width: 700px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Master - Value
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label></div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td class="style1">
                        <asp:Label runat="server" ID="lblGroup" Text="Master For :<em>*</em>"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:DropDownList runat="server" ID="ddlGroup" Width="316px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" />
                    </td>
                </tr>
                <tr>
                    <td align="left" class="style1">
                        <asp:Label runat="server" ID="lblText" Text="Value:<em>*</em>"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtText" Width="316px" TextMode="MultiLine" MaxLength="300"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblAddress" Text="Mobile No : "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtAddress" MaxLength="300"></asp:TextBox>
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
                        <asp:Button runat="server" ID="btnOK" Text="Save" OnClick="btnOK_Click" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
