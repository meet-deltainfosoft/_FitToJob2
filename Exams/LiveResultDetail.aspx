<%@ Page Title="Live Exam Detail" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="LiveResultDetail.aspx.cs" Inherits="General_LiveResultDetail" %>

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

            $("#<%=txtFromExamDate.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true });
            $("#<%=txtFromExamDate.ClientID%>").attr('readonly', true);

            $("#<%=txtToExamDate.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true });
            $("#<%=txtToExamDate.ClientID%>").attr('readonly', true);

            $("#<%=txtExamFromTime.ClientID%>").timepicker({
                showPeriod: true,
                showLeadingZero: true
            });
            $("#<%=txtExamFromTime.ClientID%>").attr('readonly', true);

            $("#<%=txtExamToTime.ClientID%>").timepicker({
                showPeriod: true,
                showLeadingZero: true
            });
            $("#<%=txtExamToTime.ClientID%>").attr('readonly', true);

            $("#<%=hlFromDtClear.ClientID%>").click(function () {
                $("#<%=txtFromExamDate.ClientID%>").val('');
                $("#<%=txtExamFromTime.ClientID%>").val('');
            });

            $("#<%=hlToDtClear.ClientID%>").click(function () {
                $("#<%=txtToExamDate.ClientID%>").val('');
                $("#<%=txtExamToTime.ClientID%>").val('');
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Live Exam Detail
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
                    <td>
                        <asp:Label runat="server" ID="lblExamDate" Text="From Exam Date:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtFromExamDate" Width="86px"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtExamFromTime" Width="86px"></asp:TextBox>
                        <asp:HyperLink ID="hlFromDtClear" runat="server" Text="Clear" Font-Size="Small"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label1" Text="To Exam Date:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtToExamDate" Width="86px"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtExamToTime" Width="86px"></asp:TextBox>
                        <asp:HyperLink ID="hlToDtClear" runat="server" Text="Clear" Font-Size="Small"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblStudentName" Text="Employee Name :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtStudentName" Width="196px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtMobileNo"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Button runat="server" ID="btnPresent" Text="Present Live Employee" BackColor="#33CC33"
                            OnClick="btnPresent_Click" />
                        <asp:Button runat="server" ID="btnAbsent" Text="Absent Employee" BackColor="#FF3300"
                            OnClick="btnAbsent_Click" />
                        <asp:Button runat="server" ID="btnFilter" Text="All Employee" OnClick="btnFilter_Click"
                            BackColor="#CCCC00" />
                        <asp:Button runat="server" ID="btnExcel" Text="Excel" OnClick="btnExcel_Click" Visible="false" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formBody">
            <asp:Label runat="server" ID="lblCounter"></asp:Label><hr />
            <asp:GridView runat="server" ID="gdvResultDetail" SkinID="Lns" AutoGenerateColumns="False"
                EmptyDataText="No Records Found." OnRowDataBound="gdvResultDetail_RowDataBound">
                <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" Font-Size="Medium" HorizontalAlign="Center" />
                <Columns>
                    <asp:HyperLinkField DataTextField="FirstName" HeaderText="Employee Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="MobileNo" HeaderText="MobileNo">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Standard" HeaderText="Department">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Subject" HeaderText="Designation">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TestName" HeaderText="Test Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ExamFromTime" HeaderText="ExamFromTime" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ExamToTime" HeaderText="ExamToTime" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalQuestions" HeaderText="TotalQuestions">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalMins" HeaderText="TotalMins">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DoneQuestions" HeaderText="DoneQuestion">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PendingQuestion" HeaderText="PendingQuestion">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ExamStartTime" HeaderText="ExamStartTime" DataFormatString="{0:hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ExamStopTime" HeaderText="ExamStopTime" DataFormatString="{0:hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="formFooter">
        </div>
    </div>
</asp:Content>
