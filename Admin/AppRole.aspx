<%@ Page Title="Delta Web ERP - Application Role" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true" CodeFile="AppRole.aspx.cs" Inherits="Admin_AppRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="form">
        <div class="formHeader">Application Role <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label></div>
        
        <asp:Panel runat="server" ID="pnlErr" Visible="false" CssClass="errors">
           <asp:BulletedList runat="server" ID="blErrs">
           </asp:BulletedList>
        </asp:Panel>   
        
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                  <td style="width:90px"><asp:Label runat="server" ID="lblName" Text="Name:<em>*</em>"></asp:Label></td>
                  <td style="width:686px"><asp:TextBox runat="server" ID="txtName" Width="680px"></asp:TextBox></td> 
                </tr>
                <tr>
                    <td style="width:90px" valign="top"><asp:Label runat="server" ID="lblDesc" Text="Description:<em>*</em>"></asp:Label></td>
                    <td style="width:686px"><asp:TextBox runat="server" ID="txtDesc" Width="680px" TextMode="MultiLine"></asp:TextBox></td>   
                </tr>
            </table>  
        </div>
        
        <div class="form">
            <div class="formHeader">Forms</div>
            
             <div class="formBody" style="overflow:auto">
                <asp:Panel ID="pnlForms" runat="server" ScrollBars="Vertical" Width="766px" Height="500px">
                    <asp:GridView ID="gdvForms" runat="server" SkinID="Lns" AutoGenerateColumns="False" Width="746px" DataKeyNames="FormId" TabIndex="2">
                        <Columns>
                            <asp:TemplateField>
                                <ItemStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Center" Width="40px" />
                                <HeaderStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelectForm" runat="server" Checked='<%# Bind("Checked") %>'/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ModuleName" HeaderText="Module">
                                <ItemStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Left" Width="120px" />
                                <HeaderStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Left"/>
                            </asp:BoundField>
                              <asp:BoundField DataField="FormName" HeaderText="Form">
                                <ItemStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Left" Width="210px" />
                                <HeaderStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Left" />
                            </asp:BoundField>
                              <asp:BoundField DataField="Desc" HeaderText="Description">
                                <ItemStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Left" Width="330px" />
                                <HeaderStyle BorderColor="#999999" VerticalAlign="Top" HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <PagerTemplate></PagerTemplate>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>    

        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width:90px" align="left">
                        <asp:Button runat="server" ID="btnDelete" 
                            Text="Delete" Enabled="False"  OnClientClick="return confirm('Do you Want to Delete');" onclick="btnDelete_Click" /></td>
                    <td style="width:686px" align="right">
                            <asp:Button runat="server" ID="btnOK" Text="OK" onclick="btnOK_Click"/>
                            <asp:Button runat="server" ID="btnCancel" Text="Cancel" onclick="btnCancel_Click"/>
                    </td>   
                </tr>
            </table>  
        </div>    
    </div>
</asp:Content>


