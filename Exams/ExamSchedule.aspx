<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="ExamSchedule.aspx.cs" Inherits="Exams_ExamSchedule" Title="Exam Schedule" %>

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
            $("#<%=txtExamDate.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true });
            $("#<%=txtExamDate.ClientID%>").attr('readonly', true);

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
            Exam Schedule
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
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
                                            <asp:Label runat="server" ID="lblDivision" Text="Division :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDivision" runat="server" Width="150px">
                                            </asp:DropDownList>
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
                    <td style="width: 40%" align="left" valign="top">
                        <div class="form">
                            <div class="formHeader">
                                Exam Schedule Detail
                            </div>
                            <div class="formBody">
                                <table>
                                    <tr>
                                        <td>
                                           <asp:Label runat="server" ID="lblIsDefaultTest" Text="IsDefaultTest :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="chkIsDefaultTest" Width="390px" TabIndex="8">
                                            </asp:CheckBox>
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
                                        </td>
                                        <td>
                                            <asp:Label runat="server" Text="OR" Font-Size="Medium"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblPatternId" Text="Pattern :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlPatternId" Width="200px" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlPatternId_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblTestId" Text="Test :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTestId" runat="server" Width="200px" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlTestId_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <asp:HyperLink runat="server" ID="hlTest" Text="View Questions"></asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblTotalQuestionlabel" Text="Total Question :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblTotalQuestion"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblExamDate" Text="Exam Date:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtExamDate" AutoPostBack="true" OnTextChanged="txtExamDate_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblExamFromTime" Text="Exam From Time:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtExamFromTime" AutoPostBack="true" OnTextChanged="txtExamFromTime_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblExamToTime" Text="Exam To Time:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtExamToTime" AutoPostBack="true" OnTextChanged="txtExamToTime_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblTotalExamMinuteslabel" Text="Exam Minutes :"></asp:Label>
                                        </td>   
                                        <td>
                                            <asp:Label runat="server" ID="lblTotalExamMinutes"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:CheckBox runat="server" ID="chkPerQuestionTime" Text="Check each question's time in exam."
                                                ForeColor="Red" BackColor="Lime" Checked="true" AutoPostBack="true" OnCheckedChanged="chkPerQuestionTime_CheckedChanged" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblPerQuestionMinuteslabel" Text="Per Question :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblPerQuestionMinutes"></asp:Label>
                                            <span runat="server" id="spanPerQuestionMinutes">Minutes</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:CheckBox runat="server" ID="chkNegativeMarks" Text="Negative Marks allow." ForeColor="Red"
                                                BackColor="Lime" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:CheckBox runat="server" ID="ChkShowResult" Text="Show Result Detail after Exam"
                                                ForeColor="Red" BackColor="#FFFFCC" Checked="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="Label1" Text="Result show after :"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtMinsforResultShow" AutoPostBack="true" Text="30"
                                                BackColor="#FFFFCC"></asp:TextBox>
                                            <asp:Label runat="server" ID="lblmins" Text=" Minute of Exam Finished"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" ID="lblAllowReview" Text="Allow Move Back/Forward Review:"
                                                Visible="true"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox runat="server" ID="chkAllowReview" Text="Allow Mark for Review" ForeColor="green"
                                                BackColor="#FFFFCC" Checked="true" Visible="true" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="formFooter">
                            </div>
                        </div>
                    </td>
                    <td style="width: 30%" align="left" valign="top">
                        <div class="form">
                            <div class="formHeader">
                                Exam History on same date and Subject
                            </div>
                            <div class="formBody">
                                <asp:PlaceHolder runat="server" ID="phHistory"></asp:PlaceHolder>
                            </div>
                            <div class="formFooter">
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
            <hr />
            <table width="100%" border="0">
                <tr>
                    <td>
                        <span>Employee List</span>&nbsp;
                        <asp:Label runat="server" ID="lblStudentCount"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView runat="server" ID="gdvRegistrations" AutoGenerateColumns="False" SkinID="Lns"
                            OnRowDataBound="gdvRegistrations_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSendPendingFeeSMSAll" runat="server" Checked="true" onclick="javascript:SelectheaderCheckboxes(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSendPendingFeeSMS" runat="server" Checked="true" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FirstName" HeaderText="Employee Name">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Mobile No" DataField="MobileNo">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Department" DataField="Standard">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Division" DataField="Division">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 10%" align="left">
                        <asp:Button runat="server" ID="btnDelete" OnClientClick="return confirm('Do you Want to Delete');"
                            Enabled="false" Text="Delete" OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 90%" align="right">
                        <asp:CheckBox runat="server" ID="chkSendNotification" Text="Send Mobile App Notification" />
                        <asp:Button runat="server" ID="btnOK" Text="OK - Schedule Exam for Selected Employee"
                            OnClick="btnOK_Click" TabIndex="8" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
