<%@ Page Title="Exam Summary Report" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="ExamSummaryRpt.aspx.cs" Inherits="Exams_ExamSummaryRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <script src="../jQuery/jquery-1.12.4.js" type="text/javascript"></script>
    <link href="../jQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../jQuery/jquery-ui.js" type="text/javascript"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtScheduleFromDt.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true });
            $("#<%=txtScheduleFromDt.ClientID%>").attr('readonly', true);

            $("#<%=txtScheduleToDt.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true });
            $("#<%=txtScheduleToDt.ClientID%>").attr('readonly', true);
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Exam Summary Report
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 220px;">
                        <asp:Label runat="server" ID="lblStandard" Text="Department :"></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:DropDownList runat="server" ID="ddlStandard" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 200px;">
                        <asp:Label ID="lblSubject" runat="server" Text="Designation :"></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:DropDownList runat="server" ID="ddlSubject" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 100px;">
                        <asp:Label ID="lblTest" runat="server" Text="Test :"></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:DropDownList runat="server" ID="ddlTest">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 220px;">
                        <asp:Label runat="server" ID="lblScheduleFromDt" Text="Schedule Date (From) :"></asp:Label>
                    </td>
                    <td style="width: 400px" height="50px">
                        <asp:TextBox runat="server" ID="txtScheduleFromDt"></asp:TextBox>
                    </td>
                    <td style="width: 200px;">
                        <asp:Label ID="lblScheuleToDt" runat="server" Text="Schedule Date (To) :"></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox runat="server" ID="txtScheduleToDt"></asp:TextBox>
                    </td>
                    <td style="width: 100px;">
                    </td>
                    <td style="width: 400px">
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 778px">
                        <asp:Button runat="server" ID="btnExamRpt" Text="Exam Summary Report" OnClick="btnExamRpt_Click" />
                        <%-- <asp:Button runat="server" ID="btnExcel" Text="Excel" OnClick="btnExcel_Click" />--%>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formHeader">
            Exam Summary (List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"></asp:Label></div>
        <div class="formBody">
            <asp:GridView runat="server" ID="gdvExamSummary" SkinID="Lns" AutoGenerateColumns="False"
                EmptyDataText="No Records Found." OnRowDataBound="gdvExamSummary_RowDataBound">
                <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" Font-Size="Medium" HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="30px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Standard" HeaderText="Department">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="70px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Subject" HeaderText="Designation">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="70px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TestName" HeaderText="Test">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="180px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField> 
                    <asp:BoundField DataField="ExamDate" HeaderText="ExamDt" DataFormatString="{0:dd-MMM-yyy hh:mm:ss tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="40px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Schedule" HeaderText="Schedule">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RegStu" HeaderText="No Of Reg.Employee">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="80px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                   
                    <asp:BoundField DataField="AtteStu" HeaderText="No Of Attempt Employee">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PassStu" HeaderText="No Of Passed Employee">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FailStu" HeaderText="No Of Fail Employee">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="HighMarks" HeaderText="HighestMarks">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="AvgMarks" HeaderText="Average Marks">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:HyperLinkField Text="ViewDetail" HeaderText="" Target="_blank">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5px" />
                    </asp:HyperLinkField>
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
