<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="Tests.aspx.cs" Inherits="Exams_Tests" Title="Test Filter" %>

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
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/jQuery.UI/jquery.ui.core.js"></script>
    <link href="../StyleSheet.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"
        integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"
        crossorigin="anonymous">
    <!-- Bootstrap JS and Popper.js (required for Bootstrap dropdowns, modals, etc.) -->
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.0.8/dist/umd/popper.min.js"
        integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"
        integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            AddTHEAD("<%=gdvSubs.ClientID%>");
            $("#<%=gdvSubs.ClientID%>").tablesorter({ locale: 'en', widgets: ['zebra'] });
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
    <script type="text/javascript">
        function btnFilter_Click() {
            var a = "TechFunda is a sister concern of DotNetFunda";
            var b = /Fun/g;
            var r = a.match(b);
            document.getElementById("myId").innerHTML = r;
        }
    </script>
    <style type="text/css">
        body
        {
            font-family: 'Arial' , sans-serif;
        }
        
        .form
        {
            background-color: #ffffff;
           
            padding: 10px;
            margin: 20px;
            border-radius: 5px;
            margin: 5px 5px 5px 5px 
        }
        .formHeader
        {
            background-color: #37C1BB;
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
            color: #ffffff;
            margin-top: 20px;
        }
        .form-group
        {
            margin-bottom: 15px;
        }
        .container
        {
            max-width: 100%;
        }
        .myInput
        {
            background-image: url('/css/searchicon.png');
            background-position: 10px 10px;
            background-repeat: no-repeat;
            width: 100%;
            font-size: 16px;
            padding: 12px 20px 12px 40px;
            border: 1px solid #ddd;
            margin-bottom: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="formHeader">
            Tests (Filter)
            <asp:Label ID="lblTitle" runat="server" Text=" "></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <div class="row">
                <div class="col-lg-8 col-sm-12">
                    <div class="form-group">
                        <asp:Label ID="lblName" runat="server" Text="Test name :" CssClass="label"></asp:Label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                </div>
                <div class="col-lg-4 col-sm-12">
                    <button type="button" onclick="btnFilter_Click" class="btn btn-primary">
                        Search</button>
                </div>
            </div>
           
        </div>
        <div class="formFooter">
        </div>
        <div class="mt-3">
        </div>
        <div class="form">
            <div class="formHeader" >
                Tests (List) &nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" ID="lblRecordStatus"></asp:Label></div>
            <div class="mt-1">
            </div>
            <div class="formBody">
                <div class="row" style="overflow: scroll;">
                    <asp:GridView runat="server" ID="gdvSubs" AutoGenerateColumns="False" SkinID="Lns"
                        OnRowDataBound="gdvSubs_RowDataBound" CssClass="table table-responsive w-500">
                        <Columns>
                            <asp:HyperLinkField DataTextField="TestName" HeaderText="TestName">
                                <HeaderStyle CssClass="custom-header-style" HorizontalAlign="Left" />
                                <ItemStyle Width="300px" />
                            </asp:HyperLinkField>
                            <asp:BoundField HeaderText="Remarks" DataField="Remarks">
                                <HeaderStyle CssClass="custom-header-style" HorizontalAlign="Left" BackColor="#37C1BB" />
                                <ItemStyle Width="300px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Created On" DataField="InsertedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                                <HeaderStyle CssClass="custom-header-style" HorizontalAlign="Left" BackColor="#37C1BB" />
                                <ItemStyle Width="300px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Last Updated On" DataField="LastUpdatedOn" DataFormatString="{0:dd-MMM-yyyy hh:mm tt}">
                                <HeaderStyle CssClass="custom-header-style" HorizontalAlign="Left" BackColor="#37C1BB" />
                                <ItemStyle Width="300px" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:HyperLinkField Text="View Questions" HeaderText="View Question">
                                <HeaderStyle CssClass="custom-header-style" HorizontalAlign="Left" BackColor="#37C1BB" />
                                <ItemStyle Width="300px" />
                            </asp:HyperLinkField>
                        </Columns>
                    </asp:GridView>
                </div>
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
    </div>
</asp:Content>
