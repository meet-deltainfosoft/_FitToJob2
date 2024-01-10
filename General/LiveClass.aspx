<%@ Page Title="LiveClass Entry" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true" CodeFile="LiveClass.aspx.cs" Inherits="General_LiveClass" %>

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

            $("#<%=txtDate.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true });
            $("#<%=txtDate.ClientID%>").attr('readonly', true);

            $("#<%=txtFromTime.ClientID%>").timepicker({
                showPeriod: true,
                showLeadingZero: true
            });
            $("#<%=txtFromTime.ClientID%>").attr('readonly', true);

            $("#<%=txtToTime.ClientID%>").timepicker({
                showPeriod: true,
                showLeadingZero: true
            });
            $("#<%=txtToTime.ClientID%>").attr('readonly', true);
        });


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Live Class
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
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
                                                OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged" TabIndex="1">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" ID="lblSubs" Text="Designation :<em>*</em>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlSubs" Width="200px" TabIndex="2">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblTitle1" Text="Title:<em>*</em>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtTitle" Width="194px" TabIndex="3"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" ID="lblTopicName" Text="Topic Name:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtTopicName" Width="194px" TabIndex="4"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblDt" Text="Date:<em>*</em>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtDate" Width="100px" TabIndex="5"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblFromTime" Text="From Time:<em>*</em>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtFromTime" Width="100px" TabIndex="6"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" ID="lblToTime" Text="To Time:<em>*</em>"></asp:Label>
                                        </td>
                                        <td style="width: 268px">
                                            <asp:TextBox runat="server" ID="txtToTime" Width="100px" TabIndex="7"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblLink" Text="Link:<em>*</em>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtLink" TextMode="MultiLine" Style="width: 375px" TabIndex="8"></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" ID="lblRemarks" Text="Remarks :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Style="width: 375px" TabIndex="9"></asp:TextBox>
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
                        <asp:Button runat="server" ID="btnOK" Text="OK" OnClick="btnOK_Click"  TabIndex="10"/>
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" TabIndex="11" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

