<%@ Page Title="Pattern Entry" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="Pattern.aspx.cs" Inherits="General_Pattern" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../jQuery/jQuery.UI/Datepicker/CSS/redmond/jquery-ui-1.8.1.custom.css"
        rel="stylesheet" type="text/css" />
    <script src="../jQuery/jQuery.UI/Datepicker/JS/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../jQuery/jQuery.UI/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../jQuery/Numeric/jquery.numeric.pack.js" type="text/javascript"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <script type="text/javascript">
        var j = jQuery.noConflict();
    </script>
    <script type="text/javascript">
        j(document).ready(function () {

            j("#<%=txtLnNo.ClientID%>").numeric();
            j("#<%=txtNoOfMCQ.ClientID%>").numeric();
            j("#<%=txtMCQRightMarks.ClientID%>").numeric();
            j("#<%=txtMCQWrongMarks.ClientID%>").numeric();
            j("#<%=txtMCQSkippedMarks.ClientID%>").numeric();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Pattern
            <asp:Label ID="lblTitle" runat="server" ></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblStandardId" runat="server">Department:<em>*</em></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStandardTextListId" runat="server" Width="190px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPatternName" runat="server">Pattern Name:<em>*</em></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPatternName" runat="server" Width="183px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <asp:Panel ID="divGridLn" runat="server" Visible="true">
            <div class="form">
                <div class="formHeader">
                    Lines
                </div>
                <div class="formBody">
                    <asp:GridView runat="server" ID="gdvPatternLn" SkinID="Lns" AutoGenerateColumns="False"
                        Width="100%" OnSelectedIndexChanged="gdvPatternLn_SelectedIndexChanged"
                        OnRowDeleting="gdvPatternLn_RowDeleting">
                        <Columns>
                            <asp:ButtonField CommandName="Select" DataTextField="LnNo" HeaderText="Ln. No.">
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Center" VerticalAlign="Top" Width="40px" />
                                <HeaderStyle BorderColor="#999999" HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="Subject" HeaderText="Designation">
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:BoundField>
                             <asp:BoundField DataField="NoOfMCQ" HeaderText="No.Of MCQ">
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:BoundField>
                             <asp:BoundField DataField="NoOfNonMCQ" HeaderText="No.Of NonMCQ">
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                                <HeaderStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:ButtonField CommandName="Delete" Text="Remove">
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Right" VerticalAlign="Top" Width="40px" />
                                <HeaderStyle BorderColor="#999999" HorizontalAlign="Right" VerticalAlign="Top" />
                            </asp:ButtonField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="formFooter">
                    <table>
                        <tr>
                            <td style="width: 764px">
                                <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlPatternLn" Visible="false">
            <div class="form">
                <div class="formHeader">
                    Subject Line
                </div>
                <asp:Panel ID="pnlLnErrs" CssClass="errors" runat="server" Visible="False">
                    <asp:BulletedList ID="blLnErrs" runat="server">
                    </asp:BulletedList>
                </asp:Panel>
                <div class="formBody">
                    <table border="0" cellspacing="0">
                        <tr>
                            <td width="100px">
                                <asp:Label ID="lblLnNo" runat="server">Ln No:</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLnNo" runat="server" ReadOnly="true" Width="50px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSubId" runat="server" Text="Designation:<em>*</em>"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlSubId" runat="server" Width="180px"></asp:DropDownList>
                            </td>
                        </tr>
                        
                         <tr bgcolor="#FFFFCC">
                             <td>
                                 <asp:Label ID="lblNoOfMCQ" runat="server" Text="No.of MCQ:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtNoOfMCQ" runat="server" Width="175px"></asp:TextBox>
                             </td>
                             <td colspan="2"></td>
                         </tr>
                        <tr bgcolor="#FFFFCC">
                            <td>
                                <asp:Label ID="lblMarks" runat="server" Text="Marks:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblRightMarks" Text="Right"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblWrong" runat="server" Text="Wrnog"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblSkipped" runat="server" Text="Skipped"></asp:Label>
                            </td>
                        </tr>
                        <tr bgcolor="#FFFFCC">
                            <td></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtMCQRightMarks" Width="175px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMCQWrongMarks" runat="server" Width="175px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMCQSkippedMarks" runat="server" Width="175px"></asp:TextBox>
                            </td>
                        </tr>

                         <tr bgcolor="#ADD8E6">
                             <td>
                                 <asp:Label ID="lblNoOfNonMCQ" runat="server" Text="No.of NonMCQ:"></asp:Label>
                             </td>
                             <td>
                                 <asp:TextBox ID="txtNoOfNonMCQ" runat="server" Width="175px"></asp:TextBox>
                             </td>
                             <td colspan="2"></td>
                         </tr>
                        <tr bgcolor="#ADD8E6">
                            <td>
                                <asp:Label ID="lblNonMCQMarks" runat="server" Text="Marks:"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNonMCQRightMarks" Text="Right"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNonMCQWrongMarks" runat="server" Text="Wrnog"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNonMCQSkippedMarks" runat="server" Text="Skipped"></asp:Label>
                            </td>
                        </tr>
                        <tr bgcolor="#ADD8E6">
                            <td></td>
                            <td>
                                <asp:TextBox runat="server" ID="txtNonMCQRightMarks" Width="175px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNonMCQWrongMarks" runat="server" Width="175px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNonMCQSkippedMarks" runat="server" Width="175px"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                </div>
                <div class="formFooter">
                    <table border="0" cellspacing="0">
                        <tr>
                            <td style="width: 770px">
                                <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button runat="server" ID="btnSaveAdd" Text="Save &amp; Add" OnClick="btnSaveAdd_Click" />
                                <asp:Button runat="server" ID="btnClose" Text="Close" OnClick="btnClose_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
        <div class="formFooter">
            <table>
                <tr>
                    <td style="width: 90px" align="left">
                        <asp:Button runat="server" ID="btnDelete" Enabled="false" Text="Delete" OnClientClick="return confirm('Do you Want to Delete');"
                            OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 686px">
                        <asp:Button runat="server" ID="btnOK" Text="Submit" OnClick="btnOK_Click" />&nbsp;
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
