<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="EBookPDF.aspx.cs" Inherits="General_EBookPDF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="../jQuery/jQuery.UI/Datepicker/CSS/redmond/jquery-ui-1.8.1.custom.css"
        rel="Stylesheet" />
    <script type="text/javascript" src="../jQuery/jQuery.UI/jquery.ui.core.js"></script>
    <script type="text/javascript" src="../jQuery/jQuery.UI/Datepicker/JS/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <script src="../jQuery/jQuery.UI/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="../jQuery/jQuery.UI/Tabs/JS/jquery.ui.tabs.js" type="text/javascript"></script>
    <link href="../jQuery/jQuery.UI/Tabs/CSS/redmond/jquery-ui-1.8.2.custom.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="../jQuery/Timepicker/jquery.ui.timepicker.js"></script>
    <link type="text/css" href="../jQuery/Timepicker/jquery-ui-1.8.14.custom.css" />
    <link type="text/css" href="../jQuery/Timepicker/jquery.ui.timepicker.css" />
    <script type="text/javascript">
        $(document).ready(function () {

        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            EBookPDF
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <asp:HiddenField ID="hfEBookPDFId" runat="server" />
        <div class="formBody">
            <table width="100%" border="0">
                <tr>
                    <td style="width: 100%" align="left" valign="top">
                        <div class="form">
                            <div class="formBody">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblStandard" Text="Department :<em>*</em>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlStandard" Width="200px" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" ID="lblSubs" Text="Designation :<em>*</em>"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:DropDownList runat="server" ID="ddlSubs" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlSubs_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 125px">
                                            <asp:Label runat="server" ID="lblFileUpload" Text="Upload Image :<em>*</em>"></asp:Label>
                                        </td>
                                        <td style="width: 268px">
                                            <asp:FileUpload ID="fuFileUpload" runat="server" TabIndex="20" />
                                            <%--<asp:Image ID="imgFileUpload" runat="server" Height="158.4px" TabIndex="98" Visible="false"
                                                Width="120px" />--%>
                                            <iframe runat="server" visible="true" id="iframepdf" style="display: inline-block;
                                                width: 100%; height: 400px;"></iframe>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblRemarks" Text="Remarks :"></asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Style="width: 195px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 10%" align="left">
                        <asp:Button runat="server" ID="btnDelete" OnClientClick="return confirm('Do you Want to Delete');"
                            Enabled="false" Text="Delete" OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 90%" align="right">
                        <asp:Button runat="server" ID="Button1" Text="OK and Add" OnClick="btnOKAndAdd_Click"
                            TabIndex="8" />
                        <asp:Button runat="server" ID="btnOK" Text="OK" OnClick="btnOK_Click" TabIndex="9" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
