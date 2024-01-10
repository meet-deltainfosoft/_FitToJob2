<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="CopyQues.aspx.cs" Inherits="Exams_CopyQues" Title="Copy Question Filter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

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
            Question (Filter)</div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table>
            <tr>
            <td class="style1">
            
            <table border="0" cellspacing="0">
            <tr bgcolor="#FFFFCC">
                <td>
                <asp:Label ID="lblCopyFrom" runat="server" Text="Copy From"></asp:Label>
                </td>
                <td>
                    Filter Data from Here and tick Selected Questions</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStandardTextListId" runat="server" Text="Department :&lt;em&gt;*&lt;/em&gt;"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:DropDownList runat="server" ID="ddlStandardTextListId" AutoPostBack="true" OnSelectedIndexChanged="ddlStandardTextListId_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblSubject" Text="Designation:<em>*</em>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSubId" OnSelectedIndexChanged="ddlSubId_SelectedIndexChanged"
                            AutoPostBack="true">
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
                        <asp:Label runat="server" ID="lblQue" Text="Question Like :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtQue" Width="500px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td>
                    &nbsp;</td>
                <td>
                <asp:Button runat="server" ID="btnFilter" Text="Search" OnClick="btnFilter_Click" 
                        BackColor="#FFFFCC" ForeColor="Blue" />
                </td>
                </tr>
            </table>
            
            </td>
            <td class="style1">
            <table border="0" cellspacing="0">
            <tr bgcolor="#33CC33">
                <td>
                <asp:Label ID="lblCopy" runat="server" Text="Copy To"></asp:Label>
                </td>
                <td>
                    Select Standard Subject and click on Paste</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Department :&lt;em&gt;*&lt;/em&gt;"></asp:Label>
                    </td>
                    <td colspan="5">
                        <asp:DropDownList runat="server" ID="ddlStandardTextListIdTo" 
                            AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlStandardTextListIdTo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label2" Text="Designation:<em>*</em>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSubIdTo" OnSelectedIndexChanged="ddlSubIdTo_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label3" Text="Test :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTestIdTo" runat="server" Width="200px" 
                            onselectedindexchanged="ddlTestIdTo_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="Label4" Text="Question Like :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="TextBox1" Width="500px"></asp:TextBox>
                    </td>
                </tr>

                  <tr>
                <td>
                    &nbsp;</td>
                <td>
                <asp:Button runat="server" ID="BtnCopyTo" Text="Paste Selected Ques" 
                        BackColor="#33CC33" ForeColor="#000066" 
                        
                        onclientclick="return confirm('Are you sure want to Copy all this selected questions?');" 
                        onclick="BtnCopyTo_Click" />
                </td>
                </tr>
            </table>
            
            </td>
            </tr>

            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 778px">
                        
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
                        Question (List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"></asp:Label>
                    </td>
                    <td align="right">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div class="formBody">
            <asp:GridView runat="server" ID="gdvQues" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvQues_RowDataBound" DataKeyNames="QueId">
                <Columns>
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
                </Columns>
            </asp:GridView>
        </div>
        <div class="formFooter">
            <table>
                <tr>
                    <td width="776" align="right">
                        <asp:Button runat="server" ID="btnShowAllRecords" Text="Show All Records" OnClick="btnShowAllRecords_Click"
                            Visible="false"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
