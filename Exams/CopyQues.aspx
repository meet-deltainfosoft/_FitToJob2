<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="CopyQues.aspx.cs" Inherits="Exams_CopyQues" Title="Copy Question Filter" %>

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
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.0.8/dist/umd/popper.min.js"
        integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"
        integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous"></script>
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
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
            margin: 5px 5px 5px 5px;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="formHeader">
            Question (Filter)</div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        
            <div class="formBody">
                <div>
                    <div class="row ">
                        <div class="col-lg-6 col-sm-12">
                            <div class="form-group" style="background-color: #FFFFCC;">
                                <asp:Label ID="lblCopyFrom" runat="server" Text="Copy From" CssClass="label"></asp:Label>
                                <h4>
                                    Filter Data from Here and tick Selected Questions
                                </h4>
                            </div>
                        </div>
                        <div class="col-lg-6 col-sm-12">
                            <div class="form-group" style="background-color: #59c072;">
                                <asp:Label ID="lblCopy" runat="server" Text="Copy To" CssClass="label"></asp:Label>
                                <h4>
                                    Select Standard Subject and click on Paste
                                </h4>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblStandardTextListId" Text="Department :<em>*</em>"
                                    CssClass="label"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlStandardTextListId" AutoPostBack="true" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="Label5" Text="Department :<em>*</em>" CssClass="label"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlStandardTextListIdTo" AutoPostBack="true"
                                    CssClass="form-control" OnSelectedIndexChanged="ddlStandardTextListIdTo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblSubject" Text="Designation :<em>*</em>" CssClass="label"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlSubId" AutoPostBack="true" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlSubId_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="Label1" Text="Designation :<em>*</em>" CssClass="label"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlSubIdTo" AutoPostBack="true" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlSubIdTo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblTestId" Text="Test :<em>*</em>" CssClass="label"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlTestId" AutoPostBack="true" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label runat="server" ID="Label2" Text="Designation :<em>*</em>" CssClass="label"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlTestIdTo" AutoPostBack="true" CssClass="form-control"
                                    OnSelectedIndexChanged="ddlTestIdTo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="lblQue" runat="server" Text="Question Like" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtQue" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6 col-sm-12">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" Text="Question Like" CssClass="label"></asp:Label>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-sm-12">
                            <button type="button" onclick="btnFilter_Click" class="btn btn-primary">
                                Search</button>
                        </div>
                        <div class="col-lg-6 col-sm-12" >
                            <asp:Button runat="server" ID="BtnCopyTo" Text="Paste Selected Ques" class="btn btn-primary"
                                OnClientClick="return confirm('Are you sure want to Copy all this selected questions?');"
                                OnClick="BtnCopyTo_Click" />
                        </div>
                    </div>
                    <%--<td class="style1">
                        <table border="0" cellspacing="0">
                             <tr bgcolor="#FFFFCC">
                                <td>
                                    <asp:Label ID="lblCopyFrom" runat="server" Text="Copy From"></asp:Label>
                                </td>
                                <td>
                                    Filter Data from Here and tick Selected Questions
                                </td>
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
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btnFilter" Text="Search" OnClick="btnFilter_Click"
                                        BackColor="#FFFFCC" ForeColor="Blue" />
                                </td>
                            </tr>
                        </table>
                    </td>--%>
                   
                    <%--  <td class="style1">
                        <table border="0" cellspacing="0">
                            <tr bgcolor="#33CC33">
                                <td>
                                    <asp:Label ID="lblCopy" runat="server" Text="Copy To"></asp:Label>
                                </td>
                                <td>
                                    Select Standard Subject and click on Paste
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Department :&lt;em&gt;*&lt;/em&gt;"></asp:Label>
                                </td>
                                <td colspan="5">
                                    <asp:DropDownList runat="server" ID="ddlStandardTextListIdTo" AutoPostBack="true"
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
                                    <asp:DropDownList ID="ddlTestIdTo" runat="server" Width="200px" OnSelectedIndexChanged="ddlTestIdTo_SelectedIndexChanged">
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
                                    &nbsp;
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="BtnCopyTo" Text="Paste Selected Ques" BackColor="#33CC33"
                                        ForeColor="#000066" OnClientClick="return confirm('Are you sure want to Copy all this selected questions?');"
                                        OnClick="BtnCopyTo_Click" />
                                </td>
                            </tr>
                        </table>
                    </td>--%>
                </div>
            </div>
       
    </div>
    <div class="formFooter">
        <table border="0" cellspacing="0">
            <tr>
                <td style="width: 778px">
                </td>
            </tr>
        </table>
    </div>
    <div class="form">
        <div class="formHeader">
           
                Question (List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"></asp:Label>
           
            <div class="mt-1">
            </div>
        </div>
        <div class="formBody">
            <div class="row" style="overflow: scroll;">
                <asp:GridView runat="server" ID="gdvQues" AutoGenerateColumns="False" SkinID="Lns"
                    OnRowDataBound="gdvQues_RowDataBound" DataKeyNames="QueId" CssClass="table table-responsive w-500">
                    <Columns>
                        <asp:HyperLinkField DataTextField="Question" HeaderText="Question">
                            <HeaderStyle HorizontalAlign="Left" CssClass="custom-header-style"/>
                            <ItemStyle Width="300px"/>
                        </asp:HyperLinkField>
                        <asp:BoundField HeaderText="Designation" DataField="Subject">
                            <HeaderStyle HorizontalAlign="Left" CssClass="custom-header-style"/>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:HyperLinkField HeaderText="A1" DataTextField="A1">
                            <HeaderStyle HorizontalAlign="Left" CssClass="custom-header-style"/>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:HyperLinkField HeaderText="A2" DataTextField="A2">
                            <HeaderStyle HorizontalAlign="Left" CssClass="custom-header-style"/>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:HyperLinkField HeaderText="A3" DataTextField="A3">
                            <HeaderStyle HorizontalAlign="Left" CssClass="custom-header-style"/>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:HyperLinkField HeaderText="A4" DataTextField="A4">
                            <HeaderStyle HorizontalAlign="Left" CssClass="custom-header-style"/>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:BoundField HeaderText="SrNo" DataField="SrNo">
                            <HeaderStyle HorizontalAlign="Left" CssClass="custom-header-style"/>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" CssClass="custom-header-style"/>
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
