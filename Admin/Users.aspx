<%@ Page Title="Delta Web ERP - Users" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="Admin_Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="form">
        
        <div class="formHeader">User (Filter)</div> 
       
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                  <td style="width:125px"><asp:Label runat="server" ID="lblFirstName" Text="First Name Like:<em>*</em>"></asp:Label></td>
                  <td style="width:263px"><asp:TextBox runat="server" ID="txtFirstName" Width="257px" AutoCompleteType="None"></asp:TextBox></td> 
                  <td style="width:125px" align="right"><asp:Label runat="server" ID="lblLastName" Text="Last Name Like:<em>*</em>" ></asp:Label></td>
                  <td style="width:263px"><asp:TextBox runat="server" ID="txtLastName" Width="257px"  AutoCompleteType="None"></asp:TextBox></td> 
                </tr>
                <tr>
                  <td style="width:125px"><asp:Label runat="server" ID="lblUserName" Text="User Name Like:<em>*</em>"></asp:Label></td>
                  <td style="width:263px"><asp:TextBox runat="server" ID="txtUserName" Width="257px"  AutoCompleteType="None"></asp:TextBox></td> 
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
        <div class="formHeader">User (List)</div>   
        <div class="formBody" style="overflow:auto">
            <asp:GridView runat="server" ID="gdvUsers" AutoGenerateColumns="False" 
                SkinID="Lns" onrowdatabound="gdvUsers_RowDataBound" Width="900px">
                <Columns>
                    <asp:HyperLinkField DataTextField="UserName" HeaderText="User Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top"/>
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="FirstName" HeaderText="First Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top"/>
                    </asp:BoundField> 
                    <asp:BoundField DataField="LastName" HeaderText="Last Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top"/>
                    </asp:BoundField> 
                    <asp:BoundField DataField="IsEnabled" HeaderText="Account Status">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top"/>
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

