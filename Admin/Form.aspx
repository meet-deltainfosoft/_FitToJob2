<%@ Page Title="Delta Web ERP - Form" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Admin_Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="form">
        <div class="formHeader">
            Form <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
        </div>
        <asp:Panel runat="server" ID="pnlErr" Visible="false" CssClass="errors">
           <asp:BulletedList runat="server" ID="blErrs">
           </asp:BulletedList>
        </asp:Panel>   
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                  <td style="width:80px"><asp:Label runat="server" ID="lblName" Text="Name:"></asp:Label></td>
                  <td style="width:308px">
                  <asp:TextBox runat="server" ID="txtName" Width="302px"></asp:TextBox>
                  </td> 
                  <td style="width:80px" align="right"><asp:Label runat="server" ID="lblModule" Text="Module:"></asp:Label> </td>
                  <td style="width:308px">
                  <asp:DropDownList ID="ddlModuleName" runat="server" Width="306px"></asp:DropDownList>
                  <%--<asp:TextBox runat="server" ID="txtModuleName" Width="306px" ReadOnly="true"></asp:TextBox>--%>
                  </td> 
                </tr>
                <tr>
                    <td style="width:80px" valign="top"><asp:Label runat="server" ID="lblDesc" Text="Description:"></asp:Label></td>
                    <td style="width:696px" colspan="3"><asp:TextBox runat="server" ID="txtDesc" Width="690px" TextMode="MultiLine"></asp:TextBox></td>   
                </tr>
            </table>  
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width:90px" align="left">
                        &nbsp;</td>
                    <td style="width:686px" align="right">
                            <asp:Button runat="server" ID="btnOK" Text="OK" onclick="btnOK_Click"/>
                            <asp:Button runat="server" ID="btnCancel" Text="Cancel" onclick="btnCancel_Click"/>
                    </td>   
                </tr>
            </table>  
        </div>    
    </div>
</asp:Content>

