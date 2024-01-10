<%@ Page Title="Delta Web ERP - Application Roles" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true" CodeFile="AppRoles.aspx.cs" Inherits="Admin_AppRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="form">
        
        <div class="formHeader">Application Role (Filter)</div> 
       
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width:80px"><asp:Label runat="server" ID="lblName" Text="Name Like:"></asp:Label></td>
                    <td style="width:696px"><asp:TextBox runat="server" ID="txtName" Width="690px"></asp:TextBox></td>                      
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
        <div class="formHeader">Application Role (List)</div>   
        <div class="formBody">
            <asp:GridView runat="server" ID="gdvAppRoles" AutoGenerateColumns="False" 
                SkinID="Lns" onrowdatabound="gdvAppRoles_RowDataBound" Width="776px">
                <Columns>
                    <asp:HyperLinkField DataTextField="Name" HeaderText="Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top"/>
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="Desc" HeaderText="Description">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle Width="326px" HorizontalAlign="Left" VerticalAlign="Top"/>
                    </asp:BoundField> 
                    <asp:BoundField HeaderText="Created on" DataField="InsertedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" >
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top"/>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Last Modified on" DataField="LastUpdatedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}" >
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top"/>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div> 
    </div> 
</asp:Content>

