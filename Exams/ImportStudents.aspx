<%@ Page Title="Import Employee" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="ImportStudents.aspx.cs" Inherits="Exams_ImportStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/jQuery.UI/jquery.ui.core.js"></script>
    <script type="text/javascript">
        var h = jQuery.noConflict();
    </script>
    <script type="text/javascript">
        h(document).ready(function () {


            //File Upload 
            h("#<%=fuFileName.ClientID%>").change(function () {
                __doPostBack("SetMSExcelFile");
            });
            //File Upload
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Import Employee
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblStandard" Text="Department :&lt;em&gt;*&lt;/em&gt;"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlStandard" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div class="row">
                <div class="col-sm-4">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblFileName" Text="Import MS Excel File: &lt;em&gt;*&lt;/em&gt;"></asp:Label>
                        <asp:FileUpload ID="fuFileName" CssClass="form-control" runat="server" Enabled="false" />
                        <a href="../MSExcelTemplates/Registration List.xlsx">Download MS Excel Template</a>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblSheets" Text="Sheet:<em>*</em>"></asp:Label>
                        <asp:DropDownList ID="ddlSheetName" CssClass="form-control" runat="server" Width="155px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlSheetName_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="col-sm-1">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnImport" Text="Import" TabIndex="18" OnClick="btnImport_Click"
                            CssClass="btn btn-primary" Enabled="false" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="header">
                    <h2>
                        <asp:Label runat="server" ID="Label1" Text="Incentive List"></asp:Label>
                        <asp:Label runat="server" ID="lblCount"></asp:Label>
                    </h2>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:Panel runat="server" ID="pnlGrid" Style="overflow: auto;">
                    <asp:GridView runat="server" ID="gdvList" OnRowDataBound="gdvList_RowDataBound" Width="100%"
                        CssClass="myGridClass">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Excel Row No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 2 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
