<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="ExamSchedules.aspx.cs" Inherits="Exams_ExamSchedules" Title="Exam Schedule Filter" %>

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
        });

        function SelectheaderCheckboxes(headerchk) {

            var gvcheck = document.getElementById("<%=gdvRegistrations.ClientID%>");

            var i;
            if (headerchk.checked) {
                for (i = 1; i < gvcheck.rows.length; i++) {
                    var inputs = gvcheck.rows[i].getElementsByTagName('input');
                    inputs[0].checked = true;
                }
            }
            else {
                for (i = 1; i < gvcheck.rows.length; i++) {
                    var inputs = gvcheck.rows[i].getElementsByTagName('input');
                    inputs[0].checked = false;
                }
            }
        }

        function Selectchildcheckboxes(header, gridviewid) {
            var ck = header;
            var count = 0;
            var gvcheck = document.getElementById(gridviewid);
            var headerchk = document.getElementById(header);
            var rowcount = gvcheck.rows.length;
            for (i = 1; i < gvcheck.rows.length; i++) {
                var inputs = gvcheck.rows[i].getElementsByTagName('input');
                if (inputs[1].checked) {
                    count++;
                }
            }
            if (count == rowcount - 1) {
                headerchk.checked = true;
            }
            else {
                headerchk.checked = false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Exam Schedule Filter
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]" Visible="false"></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table width="100%" border="0">
                <tr>
                    <td style="width: 20%" align="left" valign="top">
                        <div class="form">
                            <div class="formHeader">
                                Search Question and Employee
                            </div>
                            <div class="formBody">
                                <table border="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblStandard" Text="Department :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlStandard" runat="server" Width="150px" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblSubs" Text="Designation :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlSubs" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlSubs_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblTestId" Text="Test :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTestId" runat="server" Width="200px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblExamDate" Text="From Exam Date:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtFromExamDate"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="Label1" Text="To Exam Date:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtToExamDate"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="formFooter">
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <asp:Button runat="server" ID="btnSearch" Text="Search Employee and Question" OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <hr />
            <table width="100%" border="0">
                <tr>
                    <td>
                        <span>Exam Schedule List</span>&nbsp;
                        <asp:Label runat="server" ID="lblStudentCount"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView runat="server" ID="gdvRegistrations" AutoGenerateColumns="False" SkinID="Lns"
                            OnRowDataBound="gdvRegistrations_RowDataBound">
                            <Columns>
                                <asp:HyperLinkField DataTextField="TestName" HeaderText="TestName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle />
                                </asp:HyperLinkField>
                                <asp:BoundField HeaderText="Department" DataField="Standard">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SubjectName" HeaderText="DesignationName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle />
                                </asp:BoundField> 
                                <asp:BoundField DataField="PatternName" HeaderText="PatternName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle />
                                </asp:BoundField> 
                                <asp:BoundField DataField="ExamDate" HeaderText="ExamDate" DataFormatString="{0:dd-MMM-yyyy}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle />
                                </asp:BoundField>
                                <asp:BoundField DataField="ExamFromTime" HeaderText="ExamFromTime" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle />
                                </asp:BoundField>
                                <asp:BoundField DataField="ExamToTime" HeaderText="ExamToTime" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
        </div>
    </div>
</asp:Content>
