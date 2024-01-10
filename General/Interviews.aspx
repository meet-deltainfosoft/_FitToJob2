<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="Interviews.aspx.cs" Inherits="General_Interviews" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" href="../jQuery/jQuery.UI/Datepicker/CSS/redmond/jquery-ui-1.8.1.custom.css"
        rel="Stylesheet" />
    <script type="text/javascript" src="../jQuery/jQuery.UI/Datepicker/JS/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../jQuery/jQuery.UI/jquery.ui.core.js"></script>
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();
    </script>
    <script type="text/javascript">
        j(document).ready(function () {

            j("#<%=txtFromDt.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true });
            j("#<%=txtFromDt.ClientID%>").attr('readonly', true);

            j("#<%=txtToDt.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true });
            j("#<%=txtToDt.ClientID%>").attr('readonly', true);

            j("#<%=hlFromDtClear.ClientID%>").click(function () {
                j("#<%=txtFromDt.ClientID%>").val('');
            });

            j("#<%=hlToDtClear.ClientID%>").click(function () {
                j("#<%=txtToDt.ClientID%>").val('');
            });

            j("#<%=txtUser.ClientID%>").autocomplete("../General/User.ashx", { autoFill: true, mustMatch: false, minChars: 1 });
            j("#<%=txtUser.ClientID%>").result(function (evt, data, formatted) {
                if (data != null) {
                    j("#<%=hfUserId.ClientID%>").val(data[1]);
                    __doPostBack("hfUserId");
                }
                else {
                    j("#<%=hfUserId.ClientID%>").val("");
                    __doPostBack("hfUserId");
                }
            });

            AddTHEAD("ctl00_ContentPlaceHolder1_gdvInterview");
            j("#<%=gdvInterview.ClientID%>").tablesorter({ locale: 'en', widgets: ['zebra'], headers: { 4: { sorter: 'DD-MMM-YY'}} });
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
            Interview List
        </div>
        <asp:Panel ID="Panel1" CssClass="errors" runat="server" Visible="false">
            <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
                <asp:BulletedList ID="blErrs" runat="server">
                </asp:BulletedList>
            </asp:Panel>
        </asp:Panel>
        <div class="formBody">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblFromDt" runat="server">From Date:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFromDt" runat="server" TabIndex="2" Width="84px">  </asp:TextBox>
                        <asp:HyperLink runat="server" ID="hlFromDtClear" Text="Clear" Font-Size="Small" NavigateUrl="#"
                            Style="text-align: right"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblToDt" runat="server">To Date:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtToDt" runat="server" TabIndex="2" Width="84px">  </asp:TextBox>
                        <asp:HyperLink runat="server" ID="hlToDtClear" Text="Clear" Font-Size="Small" NavigateUrl="#"
                            Style="text-align: right"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblName" runat="server">Candidate Name:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" TabIndex="1" Width="250px">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblUserId" runat="server">Interview BY:</asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUser" runat="server" TabIndex="2" Width="250px">
                        </asp:TextBox>
                        <asp:HiddenField runat="server" ID="hfUserId" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblStatus" runat="server" Text="Status :"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton runat="server" ID="rdbALL" Text="ALL" GroupName="Status" />
                        <asp:RadioButton runat="server" ID="rdbSelected" Text="Selected" GroupName="Status" />
                        <asp:RadioButton runat="server" ID="rdbHold" Text="Hold" GroupName="Status" />
                        <asp:RadioButton runat="server" ID="rdbRejected" Text="Rejected" GroupName="Status" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table>
                <tr>
                    <td width="776" align="right">
                        <asp:Button runat="server" ID="btnFilter" CssClass="btn btn-success" Text="Search"
                            OnClick="btnFilter_Click" TabIndex="4"></asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="form">
        <div class="formHeader">
            Interview (list) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"></asp:Label></div>
        <div class="formBody" style="overflow: auto; height: auto">
            <asp:GridView runat="server" ID="gdvInterview" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvInterview_RowDataBound" EmptyDataText="No Records Found."
                EnableModelValidation="True" PageSize="10" AllowPaging="True" OnPageIndexChanging="gdvInterview_PageIndexChanging">
                <PagerStyle BackColor="#F7F3EF" ForeColor="Red" HorizontalAlign="Right" Font-Size="16px">
                </PagerStyle>
                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last"
                    NextPageText="Next" PreviousPageText="Prev" Position="Bottom" PageButtonCount="5" />
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="100px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <%--<asp:HyperLinkField Text="Candidate Details Report" HeaderText="View/Print" Target="_blank">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField Text="Offer Letter" HeaderText="" Target="_blank">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>--%>
                    <asp:HyperLinkField DataTextField="Name" HeaderText="Name">
                       <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                    </asp:HyperLinkField>
                   <%-- <asp:BoundField DataField="Name" HeaderText="Name">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="InterviewBy" HeaderText="InterviewBy">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Status" HeaderText="Status">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Date" DataField="Dt" DataFormatString="{0:dd-MMM-yy}">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Remarks" DataField="Remarks">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                    </asp:BoundField>
                    <asp:HyperLinkField Text="HOD InterView" HeaderText="HOD InterView Assessment" Target="_blank">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="300px" />
                    </asp:HyperLinkField>
                </Columns>
            </asp:GridView>
            <i style="float: right;">You are viewing page
                <%=gdvInterview.PageIndex + 1%>
                of
                <%=gdvInterview.PageCount%>
            </i>
        </div>
    </div>
</asp:Content>
