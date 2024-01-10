<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="RejectApproveds.aspx.cs" Inherits="Exams_RejectApproveds" %>

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
            Reject Approved (Filter)</div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
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
                <tr style="display: none;">
                    <td>
                        <asp:Label ID="lblStatus" runat="server" Text="Status :"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButton runat="server" ID="rdbALL" Text="ALL" GroupName="Status" />
                        <asp:RadioButton runat="server" ID="rdbSelected" Text="Approved" GroupName="Status" />
                        <asp:RadioButton runat="server" ID="rdbHold" Text="Disapproved" GroupName="Status" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table>
                <tr>
                    <td width="776" align="right">
                        <asp:Button runat="server" ID="btnFilter" CssClass="btn btn-success" Text="Search"
                             TabIndex="4"></asp:Button>
                        <asp:Button ID="btnUpdateStatus" runat="server" Text="Update"  />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
