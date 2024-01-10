<%@ Page Title="Delta Web ERP - Forms" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true" CodeFile="Forms.aspx.cs" Inherits="Admin_Forms" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="form">
        <div class="formHeader">
            Form (Filter)
        </div> 
        <asp:Panel runat="server" ID="pnlErr" Visible="false" Enabled="false" CssClass="errors">
            <asp:BulletedList runat="server" ID="blErrs">
            </asp:BulletedList> 
        </asp:Panel>   
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width:80px"><asp:Label runat="server" ID="lblModule" Text="Module:"></asp:Label></td>
                    <td style="width:308px"><asp:DropDownList runat="server" ID="ddlModule" Width="306px"></asp:DropDownList></td> 
                    <td style="width:80px" align="right"><asp:Label runat="server" ID="lblName" Text="Name Like:"></asp:Label></td>
                    <td style="width:308px"><asp:TextBox runat="server" ID="txtName" Width="302px"></asp:TextBox></td>                      
                </tr>
            </table>  
        </div> 
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width:776px" align="right"><asp:Button runat="server" ID="btnFilter" Text="Filter" onclick="btnFilter_Click"/></td>
                </tr>
            </table>  
        </div>  
    </div> 
    
    <div class="form">
        <div class="formHeader">Form (List)</div>   
        <div class="formBody">
            <asp:GridView runat="server" ID="gdvForms" AutoGenerateColumns="False" 
                SkinID="Lns" onrowdatabound="gdvForms_RowDataBound" Width="776px">
                <Columns>
                    <asp:HyperLinkField DataTextField="Name" HeaderText="Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle Width="200px" HorizontalAlign="Left" VerticalAlign="Top"/>
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="ModuleName" HeaderText="Module">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle Width="200px" HorizontalAlign="Left" VerticalAlign="Top"/>
                    </asp:BoundField>                    
                    <asp:BoundField DataField="Desc" HeaderText="Description">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle Width="476px" HorizontalAlign="Left" VerticalAlign="Top"/>
                    </asp:BoundField> 
                </Columns>
            </asp:GridView>
        </div> 
    </div> 
</asp:Content>

