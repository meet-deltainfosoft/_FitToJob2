<%@ Page Title="Exam Checking Allotment Filter" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="ExamCheckAllotments.aspx.cs" Inherits="Exams_ExamCheckAllotments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();
    </script>
    <script type="text/javascript">
        j(document).ready(function () {  // Datagrid Header Freeze

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Exam Checking Allotment Filter
        </div>
        <asp:Panel runat="server" ID="pnlErr" Visible="false" CssClass="errors">
            <asp:BulletedList runat="server" ID="blErrs">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblStandard" Text="Department :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStandard" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged"
                            Width="200px" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSubject" runat="server" Text="Designation :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSubject" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"
                            Width="200px" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTest" runat="server" Text="Test :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlTest" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblExamSchedule" runat="server" Text="Exam Schedule :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlExamSchedule" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Button runat="server" ID="Button1" Text="Get Question List" OnClick="btnFilter_Click" />
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
            Exam Checking Allotment (List)<asp:Label runat="server" ID="lblRecordStatus" Style="float: right;"></asp:Label></div>
        <div class="formBody">
            <asp:GridView runat="server" ID="gdvMasterValues" AutoGenerateColumns="False" SkinID="Lns"
                EmptyDataText="No Records Founds." OnRowDataBound="gdvMasterValues_RowDataBound"
                PageSize="50" AllowPaging="true" OnPageIndexChanging="gdvMasterValues_PageIndexChanging"
                EnableModelValidation="True">
                <PagerStyle BackColor="#F7F3EF" ForeColor="Red" HorizontalAlign="Right" Font-Size="16px">
                </PagerStyle>
                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last"
                    NextPageText="Next" PreviousPageText="Prev" Position="TopAndBottom" PageButtonCount="5" />
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="5px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:HyperLinkField DataTextField="Standard" HeaderText="Department">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="SubName" HeaderText="SubName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle />
                    </asp:BoundField>
                    <asp:BoundField DataField="TestName" HeaderText="TestName">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle />
                    </asp:BoundField>
                    <asp:BoundField DataField="ExamFromTime" HeaderText="ExamFromTime" DataFormatString="{0:dd-MMM-yy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Created on" DataField="InsertedOn" DataFormatString="{0:dd-MMM-yy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Last Modified on" DataField="LastUpdatedOn" DataFormatString="{0:dd-MMM-yy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Center" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            <i style="float: right;">You are viewing page
                <%=gdvMasterValues.PageIndex + 1%>
                of
                <%=gdvMasterValues.PageCount%>
            </i>
        </div>
    </div>
</asp:Content>
