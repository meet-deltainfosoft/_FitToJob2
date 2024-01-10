<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="TestQueBank.aspx.cs" Inherits="Exams_TestQueBank" Title="Test Create from Question Bank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <link href="../jQuery/jQuery.UI/Datepicker/CSS/redmond/jquery-ui-1.8.1.custom.css"
        rel="stylesheet" type="text/css" />
    <script src="../jQuery/jQuery.UI/Datepicker/JS/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="../jQuery/jQuery.UI/jquery.ui.core.js" type="text/javascript"></script>
    <script src="../jQuery/Numeric/jquery.numeric.pack.js" type="text/javascript"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=txtEasy.ClientID%>").numeric();
            $("#<%=txtMedium.ClientID%>").numeric();
            $("#<%=txtHard.ClientID%>").numeric();
            $("#<%=txtTotalQue.ClientID%>").numeric();

            $("#<%=ddlLevelOfQue.ClientID%>").change(function () {
                $("#<%=txtEasy.ClientID%>").val("");
                $("#<%=txtMedium.ClientID%>").val("");
                $("#<%=txtHard.ClientID%>").val("");
                $("#<%=txtTotalQue.ClientID%>").val("");
            });
        });
        function SelectheaderCheckboxes(headerchk) {

            var gvcheck = document.getElementById("<%=gdvQues.ClientID%>");

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

        function calc() {
            var Easy = 0;
            var Medium = 0;
            var Hard = 0;
            var TotalQue = 0;
            if ($("#<%=txtEasy.ClientID%>").val() != "" && $("#<%=txtEasy.ClientID%>").val() != null) {
                Easy = $("#<%=txtEasy.ClientID%>").val();
            }
            if ($("#<%=txtMedium.ClientID%>").val() != "" && $("#<%=txtMedium.ClientID%>").val() != null) {
                Medium = $("#<%=txtMedium.ClientID%>").val();
            }
            if ($("#<%=txtHard.ClientID%>").val() != "" && $("#<%=txtHard.ClientID%>").val() != null) {
                Hard = $("#<%=txtHard.ClientID%>").val();
            }

            TotalQue = parseFloat(Easy) + parseFloat(Medium) + parseFloat(Hard);
            $("#<%=txtTotalQue.ClientID%>").val(TotalQue);
        }

    </script>
    <style type="text/css">
        .style1
        {
            height: 130px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Test
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table width="100%">
                <tr>
                    <td width="60%">
                        <table border="0" cellspacing="0">
                            <tr>
                                <td width="120px">
                                    <asp:Label ID="lblTestName" runat="server" Text="Test Name:<em>*</em>"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox runat="server" ID="txtTestName" Width="300px" Height="18px" OnTextChanged="txtTestName_TextChanged"
                                        AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblStandardTextListId" runat="server" Text="Department :&lt;em&gt;*&lt;/em&gt;"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList runat="server" ID="ddlStandardTextListId" AutoPostBack="true" Width="306px"
                                        Height="18px" OnSelectedIndexChanged="ddlStandardTextListId_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblPattern" Text="Pattern:<em>*</em>"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList runat="server" ID="ddlPatternId" OnSelectedIndexChanged="ddlPatternId_SelectedIndexChanged"
                                        AutoPostBack="true" Width="245px" Height="20px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblSubject" Text="Designation:<em>*</em>"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList runat="server" ID="ddlSubId" Width="245px" Height="20px" OnSelectedIndexChanged="ddlSubId_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblChapterId" Text="Chapter:"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList runat="server" ID="ddlChapterId" Width="245px" Height="20px" OnSelectedIndexChanged="ddlChapterId_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblPeriodNo" Text="Chapter No :"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:CheckBoxList runat="server" ID="chkPeriodNo" Width="245px" RepeatColumns="3"
                                        RepeatDirection="Vertical">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblType" Text="Type:<em>*</em>"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:RadioButton runat="server" ID="rbTypeAuto" Text="Auto" GroupName="Type" AutoPostBack="true"
                                        OnCheckedChanged="rbTypeAuto_CheckedChanged" Checked="true" />
                                    <asp:RadioButton runat="server" ID="rbTypeManual" Text="Manual" GroupName="Type"
                                        AutoPostBack="true" OnCheckedChanged="rbTypeAuto_CheckedChanged" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Level of Question:" Visible="false"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList runat="server" ID="ddlLevelOfQue" Height="20px" Visible="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblLevelOfQue" runat="server" Text="Level of Question:<em>*</em>"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text="Easy"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label runat="server" Text="Medium"></asp:Label>
                                </td>
                                <td style="padding-left: 30px">
                                    <asp:Label runat="server" Text="Hard"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtEasy" Width="50px" onchange="javascript:calc()"
                                        Height="18px"> </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtMedium" Width="50px" onchange="javascript:calc()"
                                        Height="18px"> </asp:TextBox>
                                </td>
                                <td style="padding-left: 30px">
                                    <asp:TextBox runat="server" ID="txtHard" Width="50px" onchange="javascript:calc()"
                                        Height="18px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="lblTotalQue" Text="Total Question:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtTotalQue" Width="50px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" width="40%">
                        <table>
                            <asp:Panel ID="Panel1" runat="server" Visible="False">
                                <asp:BulletedList ID="BulletedList1" runat="server" Font-Bold="true" ForeColor="Blue">
                                </asp:BulletedList>
                            </asp:Panel>
                            <asp:PlaceHolder ID="plRpt" runat="server"></asp:PlaceHolder>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table width="100%">
                <tr>
                    <td style="width: 20%" align="left">
                        <asp:Button runat="server" ID="btnDelete" OnClientClick="return confirm('Do you Want to Delete');"
                            Enabled="false" Text="Delete" OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 40%" align="right">
                        <asp:Button runat="server" ID="btnFilter" Text="Search" OnClick="btnFilter_Click"
                            BackColor="#FFFFCC" ForeColor="Blue" />
                        <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                    </td>
                    <td style="width: 40%" align="left">
                        <asp:Button runat="server" ID="btnAddToTest" Text="Add To Test" BackColor="#33CC33"
                            ForeColor="#000066" OnClick="btnAddToTest_Click" Visible="false" OnClientClick="return confirm('Are you sure want to Create Test with all this selected questions?');" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="form">
        <div class="formHeader" style="height: 40px;">
            <table width="100%">
                <tr>
                    <td>
                        Test Question (List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"
                            Style="float: right;"></asp:Label>
                    </td>
                    <td align="right">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div class="formBody">
            <asp:GridView runat="server" ID="gdvQues" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvQues_RowDataBound" DataKeyNames="QueBankId,SubId,LevelofQue">
                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="30px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:HyperLinkField DataTextField="Question" HeaderText="Question">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="Designation" DataField="Subject">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:HyperLinkField HeaderText="A1" DataTextField="A1">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="A2" DataTextField="A2">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="A3" DataTextField="A3">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="A4" DataTextField="A4">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>
                    <asp:BoundField HeaderText="SrNo" DataField="SrNo">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="LevelofQue" DataField="LevelofQue">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAll" runat="server" Checked="true" onclick="javascript:SelectheaderCheckboxes(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelect" runat="server" Checked="true" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
