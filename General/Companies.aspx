<%@ Page Title="Company Filter" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="Companies.aspx.cs" Inherits="General_Companies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>

    <link rel="stylesheet" type="text/css" href="../jquery/speechbubbles.css" />

    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>

    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />

    <script src="../jquery/speechbubbles.js" type="text/javascript"></script>

    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>

    <script type="text/javascript">
         var j = jQuery.noConflict();
    </script>

    <script type="text/javascript">
        j(document).ready(function () {
            AddTHEAD("<%=gdvCompanies.ClientID%>");
            j("#<%=gdvCompanies.ClientID%>").tablesorter({ locale: 'en', widgets: ['zebra'], headers: { 2: { sorter: 'DD-MMM-YY' }, 3: { sorter: 'DD-MMM-YY'}} });

        });

        function showpopupN(url, name, no) {
            var left = (screen.width / 2) - (800 / 2);
            var top = (screen.height / 2) - (300 / 2);
            newwindow = window.open(url, 'Company', 'height=400px,width=800px,menubar=no,toolbar=no,scrollbars=no,titlebar=no;resizable=no,top=' + top + ', left=' + left);
            if (window.focus) { newwindow.focus() }
        }
        function showUpload(url, name) {
            newwindow = window.open(url, 'Company');
            if (window.focus) { newwindow.focus() }
        }

        function AddTHEAD(tableName) {
            var table = document.getElementById(tableName);
            if (table != null) {
                var head = document.createElement("THEAD");
                head.appendChild(table.rows[0]);
                table.insertBefore(head, table.childNodes[0]);
            }
        }
        function myFunc(event, Url) {
            if (false == j("#DivInfo").is(':visible')) {
                $("#DivInfo").css({
                    top: '30%',
                    left: '35%'
                }).show();

                $('#<%=ifHistory.ClientID%>').attr('src', Url);
            }
            else {
                $("#DivInfo").hide();
            }
        }
    </script>

    <style type="text/css">
        .style1
        {
            width: 132px;
        }
    </style>
    <style type="text/css">
        .btnfld img
        {
            height: 20px;
            width: 20px;
        }
        #DivInfo
        {
            display: none;
            left: 0;
            top: 0;
            position: fixed;
            width: 50%;
            height: 50%;
            box-shadow: 0px 5px 54px 18px gray;
            border: 0px solid gray;
            background: white;
            border-radius: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Company (Filter)</div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 80px">
                        <asp:Label runat="server" ID="lblName" Text="Name Like:"></asp:Label>
                    </td>
                    <td style="width: 308px">
                        <asp:TextBox runat="server" ID="txtName" Width="500px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 779px" align="right">
                        <asp:Button runat="server" ID="btnFilter" Text="Filter" OnClick="btnFilter_Click" />
                    </td>
                    <td align="right" style="width: 39%">
                        <asp:ImageButton ID="imgExportToExcel" runat="server" OnClick="imgExportToExcel_Click"
                            TabIndex="10" ImageUrl="../images/Excel.png" Style="padding-top: 8px; padding-bottom: 4px" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="form">
        <div class="formHeader">
            Company (List)
            <asp:Label runat="server" ID="lblRecordStatus" Style="float: right;"></asp:Label>
        </div>
        <div class="formBody" style="overflow: auto; height: auto">
            <asp:GridView runat="server" ID="gdvCompanies" AutoGenerateColumns="False" SkinID="Lns"
                OnRowDataBound="gdvCompanies_RowDataBound" EmptyDataText="No Records Found."
                EnableModelValidation="True" PageSize="50" AllowPaging="True" OnPageIndexChanging="gdvCompanies_PageIndexChanging">
                <PagerStyle BackColor="#F7F3EF" ForeColor="Red" HorizontalAlign="Right" Font-Size="16px">
                </PagerStyle>
                <PagerSettings Mode="NumericFirstLast" FirstPageText="First" LastPageText="Last"
                    NextPageText="Next" PreviousPageText="Prev" Position="TopAndBottom" PageButtonCount="5" />
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="50px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:HyperLinkField DataTextField="Name" HeaderText="Name">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="150px" />
                    </asp:HyperLinkField>
                    <asp:BoundField DataField="Address" HeaderText="Address">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="220px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="City" HeaderText="City">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="100px" />
                    </asp:BoundField>
                    <%-- <asp:BoundField HeaderText="Created on" DataField="InsertedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Last Modified on" DataField="LastUpdatedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle Width="100px" HorizontalAlign="Left" />
                    </asp:BoundField>--%>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HyperLink ID="hlviewHistory" runat="server" CssClass="btnfld" ImageUrl="~/images/History1.png"
                                ToolTip="View History">
                            </asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                        <ItemStyle Width="20px" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <i style="float: right;">You are viewing page
                <%=gdvCompanies.PageIndex + 1%>
                of
                <%=gdvCompanies.PageCount%>
            </i>
        </div>
    </div>
    <div id="DivInfo">
        <div class="container" style="height: 100%">
            <asp:HyperLink href="" class="js-modal-close close" onclick="myFunc(event,'');">×</asp:HyperLink>
            <iframe id="ifHistory" runat="server" frameborder="0" title="Last Call history."
                width="100%" height="90%" scrolling="no"></iframe>
        </div>
    </div>
</asp:Content>
