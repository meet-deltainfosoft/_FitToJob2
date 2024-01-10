<%@ Page Title="Exam Result Publish Filter" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="ExamResultPublishs.aspx.cs" Inherits="Exams_ExamResultPublishs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <script src="../jQuery/jquery-1.12.4.js" type="text/javascript"></script>
    <link href="../jQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../jQuery/jquery-ui.js" type="text/javascript"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

        });        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Exam Result Publish Filter
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblStandard" Text="Department :"></asp:Label>
                    </td>
                    <td style="width: 300px">
                        <asp:DropDownList runat="server" ID="ddlStandard" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged"
                            AutoPostBack="true" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblSubject" runat="server" Text="Designation :"></asp:Label>
                    </td>
                    <td style="width: 300px">
                        <asp:DropDownList runat="server" ID="ddlSubject" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"
                            AutoPostBack="true" Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTest" runat="server" Text="Test :"></asp:Label>
                    </td>
                    <td style="width: 300px">
                        <asp:DropDownList runat="server" ID="ddlTest" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged"
                            AutoPostBack="true" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Schedule :"></asp:Label>
                    </td>
                    <td style="width: 300px">
                        <asp:DropDownList runat="server" ID="ddlSchedule" Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="right">
                        <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formBody">
            <div class="formHeader">
                Exam Result Publish (List)<asp:Label runat="server" ID="lblRecordStatus" Style="float: right;"></asp:Label></div>
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
        </div>
        <div class="formFooter">
            <i style="float: right;">You are viewing page
                <%=gdvMasterValues.PageIndex + 1%>
                of
                <%=gdvMasterValues.PageCount%>
            </i>
        </div>
    </div>
</asp:Content>
