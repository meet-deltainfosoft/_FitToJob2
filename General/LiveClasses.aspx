﻿<%@ Page Title="Live Class Filter" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="LiveClasses.aspx.cs" Inherits="General_LiveClasses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            AddTHEAD("<%=gdvLiveClass.ClientID%>");
            $("#<%=gdvLiveClass.ClientID%>").tablesorter({ locale: 'en', widgets: ['zebra'] });
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
            Live Class (Filter)
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblStandard" runat="server" Text="Department :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStandard" Width="200px" TabIndex="1" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="padding-left: 50px">
                        <asp:Label runat="server" ID="lblSubs" Text="Designation :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSubs" Width="200px" TabIndex="2">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 778px">
                        <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" />
                        <asp:Button runat="server" ID="btnExcel" Text="export to excel" OnClick="btnExcel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="form">
        <div class="formHeader">
            Live Class (List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus" Style="float: right"></asp:Label>
        </div>
        <div class="formBody" style="overflow: auto">
            <asp:GridView runat="server" ID="gdvLiveClass" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvLiveClass_RowDataBound" Width="100%">
                <Columns>
                    <asp:HyperLinkField DataTextField="Title" HeaderText="Title">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="Department Name" DataField="StandardName">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Designation Name" DataField="SubjectName">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:dd-MMM-yyyy}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="From Time" DataField="FromTime" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="To Time" DataField="ToTime" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Created On" DataField="InsertedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Last Updated On" DataField="LastUpdatedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="formFooter">
            <table>
                <tr>
                    <td width="776" align="right">
                        <asp:Button runat="server" ID="btnShowAllRecords" Text="Show All Records" OnClick="btnShowAllRecords_Click"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
