﻿<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="QueBanks.aspx.cs" Inherits="Exams_QueBanks" Title="Question Bank Filter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            AddTHEAD("<%=gdvQues.ClientID%>");
            $("#<%=gdvQues.ClientID%>").tablesorter({ locale: 'en', widgets: ['zebra'] });
        });
        function AddTHEAD(tableName) {
            var table = document.getElementById(tableName);
            if (table != null) {
                var head = document.createElement("THEAD");
                head.appendChild(table.rows[0]);
                table.insertBefore(head, table.childNodes[0]);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Question Bank(Filter)
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblStandardTextListId" runat="server" Text="Department :&lt;em&gt;*&lt;/em&gt;"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStandardTextListId" AutoPostBack="true" OnSelectedIndexChanged="ddlStandardTextListId_SelectedIndexChanged"
                            Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="padding-left: 50px">
                        <asp:Label runat="server" ID="lblSubject" Text="Designation:<em>*</em>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSubId" OnSelectedIndexChanged="ddlSubId_SelectedIndexChanged"
                            AutoPostBack="true" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="padding-left: 50px">
                        <asp:Label runat="server" ID="lblChapterId" Text="Chapter :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlChapterId" runat="server" Width="200px" OnSelectedIndexChanged="ddlChapterId_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="padding-left: 50px">
                        <asp:Label runat="server" ID="lblPeriodNo" Text="Chapter No :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPeriodNo" runat="server" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblQue" Text="Question Like :"></asp:Label>
                    </td>
                    <td colspan="7">
                        <asp:TextBox runat="server" ID="txtQue" Width="505px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 778px">
                        <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="form">
        <div class="formHeader">
            Question Bank(List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"
                Style="float: right;"></asp:Label>
        </div>
        <div class="formBody">
            <asp:GridView runat="server" ID="gdvQues" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvQues_RowDataBound">
                <Columns>
                    <asp:HyperLinkField DataTextField="Question" HeaderText="Question">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="Designation" DataField="Subject">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:HyperLinkField HeaderText="A1" DataTextField="A1">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="A2" DataTextField="A2">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="A3" DataTextField="A3">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="A4" DataTextField="A4">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="SrNo" DataField="SrNo">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="formFooter">
            <table>
                <tr>
                    <td width="776" align="right">
                        <asp:Button runat="server" ID="btnShowAllRecords" Text="Show All Records" OnClick="btnShowAllRecords_Click">
                        </asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
