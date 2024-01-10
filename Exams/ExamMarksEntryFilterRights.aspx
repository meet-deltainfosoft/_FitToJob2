<%@ Page Title="Exam Question List For Evaluation" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="ExamMarksEntryFilterRights.aspx.cs" Inherits="Exams_ExamMarksEntryFilterRights" %>

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
            AddTHEAD("<%=gdvResultDetail.ClientID%>");
            $("#<%=gdvResultDetail.ClientID%>").tablesorter({ locale: 'en', widgets: ['zebra'] });
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
            Exam Question List For Evaluation
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblStandard" Text="Department :<em>*</em>"></asp:Label>
                    </td>
                    <td style="width: 300px">
                        <asp:DropDownList runat="server" ID="ddlStandard" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged"
                            AutoPostBack="true" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblSubject" runat="server" Text="Designation :<em>*</em>"></asp:Label>
                    </td>
                    <td style="width: 300px">
                        <asp:DropDownList runat="server" ID="ddlSubject" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"
                            AutoPostBack="true" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTest" runat="server" Text="Test :<em>*</em>"></asp:Label>
                    </td>
                    <td style="width: 300px">
                        <asp:DropDownList runat="server" ID="ddlTest" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged"
                            AutoPostBack="true" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Schedule :<em>*</em>"></asp:Label>
                    </td>
                    <td style="width: 300px">
                        <asp:DropDownList runat="server" ID="ddlSchedule" AutoPostBack="true" Width="150px"
                            OnSelectedIndexChanged="ddlSchedule_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUserId" runat="server" Text="Employee Name :<em>*</em>"></asp:Label>
                    </td>
                    <td colspan="4">
                        <asp:DropDownList runat="server" ID="ddlUserId" AutoPostBack="true" Width="300px"
                            OnSelectedIndexChanged="ddlUserId_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblQue" runat="server" Text="Questions :<em>*</em>"></asp:Label>
                    </td>
                    <td colspan="4">
                        <asp:DropDownList runat="server" ID="ddlQue" Width="300px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="right">
                        <asp:Button runat="server" ID="btnSearch" Text="Search" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formBody">
            <asp:GridView runat="server" ID="gdvResultDetail" SkinID="Lns" AutoGenerateColumns="False"
                EmptyDataText="No Records Found." OnRowDataBound="gdvResultDetail_RowDataBound">
                <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" Font-Size="Medium" HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="30px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="StudentName" HeaderText="Employee Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Standard" HeaderText="Department">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Division" HeaderText="Division">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GRNo" HeaderText="GRNo">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RollNo" HeaderText="RollNo">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QuestionNo" HeaderText="Question">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalMarks" HeaderText="TotalMarks">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ObtainedMarks" HeaderText="ObtainedMarks">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="50px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:HyperLinkField Text="MarksEntry" HeaderText="Marks Entry">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5px" />
                    </asp:HyperLinkField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="formFooter">
        </div>
    </div>
</asp:Content>
