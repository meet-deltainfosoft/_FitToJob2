﻿<%@ Page Title="Result Detail Advance" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="ResultDetailAdv.aspx.cs" Inherits="General_ResultDetailAdv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <script src="../jQuery/jquery-1.12.4.js" type="text/javascript"></script>
    <link href="../jQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../jQuery/jquery-ui.js" type="text/javascript"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <%--<link href="../jQuery/jQuery.UI/Datepicker/CSS/redmond/jquery-ui-1.8.1.custom.css"
        rel="stylesheet" type="text/css" />--%>
    <%--<script src="../jQuery/jQuery.UI/Datepicker/JS/jquery.ui.datepicker.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtScheduleFromDt.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true });
            $("#<%=txtScheduleFromDt.ClientID%>").attr('readonly', true);

            $("#<%=txtScheduleToDt.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true });
            $("#<%=txtScheduleToDt.ClientID%>").attr('readonly', true);

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
            Result Detail
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
                            Width="300px" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 200px;">
                        <asp:Label ID="lblSubject" runat="server" Text="Designation :"></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:DropDownList runat="server" ID="ddlSubject" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"
                            Width="300px" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 100px;">
                    </td>
                    <%--  <td>
                    </td>--%>
                </tr>
                <tr>
                    <td style="width: 100px;">
                        <asp:Label ID="lblTest" runat="server" Text="Test :"></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:DropDownList runat="server" ID="ddlTest" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged"
                            Width="300px" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 200px;">
                        <asp:Label ID="Label2" runat="server" Text="Schedule :"></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:DropDownList runat="server" ID="ddlSchedule" AutoPostBack="true" Width="300px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 100px;">
                    </td>
                    <%--  <td style="width: 400px"> 
                    </td>--%>
                </tr>
                <tr>
                    <td style="width: 220px;">
                        <asp:Label runat="server" ID="lblScheduleFromDt" Text="Schedule Date (From) :"></asp:Label>
                    </td>
                    <td style="width: 400px">
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
                    <%--   <td style="width: 400px">
                    </td>--%>
                </tr>
                <tr>
                    <td style="width: 220px;">
                        <asp:Label runat="server" ID="lblStudentName" Text="Employee Name :"></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox runat="server" ID="txtStudentName"></asp:TextBox>
                    </td>
                    <td style="width: 200px;">
                        <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No :"></asp:Label>
                    </td>
                    <td style="width: 400px">
                        <asp:TextBox runat="server" ID="txtMobileNo"></asp:TextBox>
                    </td>
                    <td style="width: 100px;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="right">
                        <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" />
                        <asp:Button runat="server" ID="btnExcel" Text="export to excel" OnClick="btnExcel_Click" />
                        <asp:Button runat="server" ID="btnERPExcel" Text="ERP Excel Export" OnClick="btnERPExcel_Click"
                            Style="background-color: Green" />
                        <asp:Button runat="server" ID="btnERPExcelQuestionWise" Text="ERP Excel Export - Question Wise"
                            Style="background-color: Green" onclick="btnERPExcelQuestionWise_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formBody">
            <asp:GridView runat="server" ID="gdvResultDetail" SkinID="Lns" AutoGenerateColumns="False"
                EmptyDataText="No Records Found." OnRowDataBound="gdvResultDetail_RowDataBound">
                <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" Font-Size="Medium" HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="Rank">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="30px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:HyperLinkField DataTextField="FirstName" HeaderText="Employee Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="110px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="MobileNo" HeaderText="MobileNo">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="40px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ExamNo" HeaderText="ExamNo">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="30px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Standard" HeaderText="Department">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="30px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Subject" HeaderText="Designation">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="40px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TestName" HeaderText="Test Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="120px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalMarks" HeaderText="Obtained Marks">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="70px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NoOFQuestions" HeaderText="Total Questions">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="70px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NoOfTrueAnswers" HeaderText="No Of True Answers">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="70px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                    <%--<asp:BoundField DataField="NoOfWrongAnswers" HeaderText="No Of Wrong Answers">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="70px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="NoOfUnAttemptedQuestions" HeaderText="No Of UnAttempted Questions">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle Width="70px" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="formFooter">
        </div>
    </div>
</asp:Content>
