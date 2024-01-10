<%@ Page Title="Exam Summary Detail Report" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="ExamSummaryDetailRpt.aspx.cs" Inherits="Exams_ExamSummaryDetailRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <script src="../jQuery/jquery-1.12.4.js" type="text/javascript"></script>
    <link href="../jQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../jQuery/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            AddTHEAD("<%=gdvExamDetail.ClientID%>");
            $("#<%=gdvExamDetail.ClientID%>").tablesorter({ locale: 'en', widgets: ['zebra'] });
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
            Exam Summary Detail Report
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" Text="Department Name:" ID="lblStd"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblStdName" Font-Bold="true" Font-Size="Medium" ForeColor="BlueViolet"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" Text="Designation Name:" ID="lblSub"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblSubName" Font-Bold="true" Font-Size="Medium" ForeColor="BlueViolet"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" Text="Test Name:" ID="lblTest"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblTestName" Font-Bold="true" Font-Size="Medium" ForeColor="BlueViolet"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label runat="server" Text="Schedule:" ID="lblSchedule"></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblScheduleName" Font-Bold="true" Font-Size="Medium" ForeColor="BlueViolet"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formHeader">
            Exam Summary Details (List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"></asp:Label></div>
        <div class="formBody">
            <asp:GridView runat="server" ID="gdvExamDetail" SkinID="Lns" AutoGenerateColumns="False"
                EmptyDataText="No Records Found." OnRowDataBound="gdvExamDetail_RowDataBound">
                <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" Font-Size="Medium" HorizontalAlign="Center" />
                <Columns>
                 <asp:TemplateField HeaderText="Rank">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="50px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="FirstName" HeaderText="Student Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="150px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MobileNo" HeaderText="MobileNo">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalMarks" HeaderText="Obtained Marks">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NoOfTrueAnswers" HeaderText="No Of True Answers">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NoOfWrongAnswers" HeaderText="No Of Wrong Answers">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NoOfUnAttemptedQuestions" HeaderText="No Of UnAttempted Questions">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
