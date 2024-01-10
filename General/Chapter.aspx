<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="Chapter.aspx.cs" Inherits="General_Chapter" %>

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
        var j = jQuery.noConflict();
    </script>
    <script type="text/javascript">
        j(document).ready(function () {

            j("#<%=txtLnNo.ClientID%>").attr('readonly', true);
            j("#<%=txtSrNo.ClientID%>").numeric();

            //Autocomplete with Postback
            j("#<%=txtChapterName.ClientID%>").autocomplete("../General/Chapter.ashx?StdId=" + j("#<%=ddlStandard.ClientID%>").val() + "&SubId=" + j("#<%=ddlSubs.ClientID%>").val(), { autoFill: true, mustMatch: false, minChars: 1 });
            j("#<%=txtChapterName.ClientID%>").result(function (evt, data, formatted) {
                if (data != null) {
                    if (data[0] == "No Record Founds.") {

                        j("#<%=txtChapterName.ClientID%>").val(data[1]);
                    }
                    else {
                        j("#<%=txtChapterName.ClientID%>").val(data[1]);
                    }
                }
                else {
                    j("#<%=txtChapterName.ClientID%>").val("");
                }
            });
        });
    </script>
    <style type="text/css">
        .style1
        {
            width: 720px;
        }
        .style2
        {
            height: 27px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Chapter
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
                                                OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right">
                                            <asp:Label runat="server" ID="lblSubs" Text="Designation :<em>*</em>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlSubs" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlSubs_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblSrNo" Text="Chapter No. :<em>*</em>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtSrNo"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblChapterName" Text="Chapter Name :<em>*</em>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtChapterName" Style="width: 97%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <%--<td>
                                            <asp:Label runat="server" ID="lblPeriodNo" Visible="false" Text="Video No :<em>*</em>"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtPeriodNo" Visible="false" MaxLength="10"></asp:TextBox>
                                        </td>--%>
                                        <td>
                                            <asp:Label runat="server" ID="lblRemarks" Text="Remarks :"></asp:Label>
                                        </td>
                                        <td>
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
        <div id="Div1" class="form" runat="server" visible="false">
            <div class="formHeader">
                Line Detail</div>
            <div class="formBody" style="overflow: auto">
                <asp:GridView runat="server" ID="gdvChapterLn" SkinID="Lns" AutoGenerateColumns="False"
                    Width="1060px" OnRowDeleting="gdvChapterLn_RowDeleting" OnSelectedIndexChanged="gdvChapterLn_SelectedIndexChanged">
                    <Columns>
                        <asp:ButtonField CommandName="Select" DataTextField="LnNo" HeaderText="Ln. No.">
                            <ItemStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" Width="10px" />
                            <HeaderStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:ButtonField>
                        <asp:BoundField DataField="StandardName" HeaderText="Department">
                            <ItemStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                            <HeaderStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SubjectName" HeaderText="Designation">
                            <ItemStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                            <HeaderStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ChapterName" HeaderText="Chapter">
                            <ItemStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                            <HeaderStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PeriodNo" HeaderText="Chapter No" DataFormatString="{0:N0}">
                            <ItemStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                            <HeaderStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ChapterVideoName" HeaderText="Chapter Video">
                            <ItemStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                            <HeaderStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:BoundField>
                        <asp:ButtonField CommandName="Delete" Text="Remove">
                            <ItemStyle BorderColor="#999999" HorizontalAlign="Right" VerticalAlign="Top" />
                            <HeaderStyle BorderColor="#999999" HorizontalAlign="Right" VerticalAlign="Top" />
                        </asp:ButtonField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="formFooter">
                <table border="0" cellspacing="0">
                    <tr>
                        <td style="width: 774px" align="right">
                            <asp:Button runat="server" ID="btnChapterAdd" Text="Add" OnClick="btnChapterAdd_Click"
                                TabIndex="25" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlChaptertLn" Visible="false">
            <div class="form">
                <div class="formHeader">
                    Line Detail</div>
                <asp:Panel runat="server" ID="pnlChapterLnErr" Visible="false" CssClass="errors">
                    <asp:BulletedList runat="server" ID="blChapterLnErrs">
                    </asp:BulletedList>
                </asp:Panel>
                <div class="formBody">
                    <table border="0" cellspacing="0">
                        <tr>
                            <td class="style2">
                                <asp:Label runat="server" ID="lblLnNo" Text="Line No: <em>*</em>"></asp:Label>
                            </td>
                            <td class="style2">
                                <asp:TextBox runat="server" ID="txtLnNo" Width="50px" CssClass="disabledCtrls"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 155px" valign="top">
                                <asp:Label runat="server" ID="lblStrandardLn" Text="Department : <em>*</em>"></asp:Label>
                            </td>
                            <td style="width: 130px">
                                <asp:DropDownList runat="server" ID="ddlStandardLn" Width="200px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlStandardLn_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 155px" valign="top">
                                <asp:Label runat="server" ID="lblSubjectLn" Text="Designation: <em>*</em>"></asp:Label>
                            </td>
                            <td style="width: 639px">
                                <asp:DropDownList runat="server" ID="ddlSubjectLn" Width="200px" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSubjectLn_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 155px" valign="top">
                                <asp:Label runat="server" ID="lblChapter" Text="Chapter : <em>*</em>"></asp:Label>
                            </td>
                            <td style="width: 639px">
                                <asp:DropDownList runat="server" ID="ddlChapterLn" Width="200px" OnSelectedIndexChanged="ddlChapterLn_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 155px" valign="top">
                                <asp:Label runat="server" ID="lblPeriodNoLn" Text="Chapter No : <em>*</em>"></asp:Label>
                            </td>
                            <td style="width: 639px">
                                <asp:DropDownList runat="server" ID="ddlPeriodNo" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 155px" valign="top">
                                <asp:Label runat="server" ID="Label1" Text="Chapter Video No : <em>*</em>"></asp:Label>
                            </td>
                            <td style="width: 639px">
                                <asp:DropDownList runat="server" ID="ddlChapterVideo" Width="200px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="formFooter">
                    <table border="0" cellspacing="0">
                        <tr>
                            <td align="left">
                                <asp:Button runat="server" ID="btnCloseChapterLn" OnClick="btnCloseChapterLn_Click"
                                    Text="Close" />
                            </td>
                            <td align="right" class="style1">
                                <asp:Button runat="server" ID="btnSaveChapterLn" OnClick="btnSaveChapterLn_Click"
                                    Text="Save" />
                                <asp:Button runat="server" ID="btnSaveAdd" Text="Save & Add" OnClick="btnSaveAdd_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
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
