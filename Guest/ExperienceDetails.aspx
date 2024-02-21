<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExperienceDetails.aspx.cs"
    Inherits="Guest_ExperienceDetails" MasterPageFile="~/Guest/Candidate.master" %>

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
    <%-- <link type="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
     <script type="https://ajax.googleapis.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>
    <script type="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"
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
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <script src="//cdn.rawgit.com/Eonasdan/bootstrap-datetimepicker/e8bddc60e73c1ec2475f827be36e1957af72e2ea/src/js/bootstrap-datetimepicker.js"></script>
    <script src="//code.jquery.com/jquery-2.1.1.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.4/jquery.min.js"></script>
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.11.0/jquery-ui.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css"
        integrity="sha512-jN53oRtrw3z+3iPYAK5QVrAJS3IdNTe8c0gkyaDA5ZIUVsm+Jb94ZvLNKLO5U+Q98s3UfcddoGezpKNBo1i5Hg=="
        crossorigin="anonymous" />
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();
    </script>
    <style type="text/css">
        .header-style
        {
            background-color: #3498db !important; /* Header background color */
            color: #fff; /* Header text color */
        }
        
        .gridview-style
        {
            margin-left: 2vh; /* Margin-left for the entire GridView */
            margin-right: 1vh;
        }
    </style>
    <style type="text/css">
        /* Custom CSS styles for your form */
        body
        {
            font-family: 'Arial' , sans-serif;
            background-color: #37C1BB;
        }
        
        .form
        {
            background-color: #ffffff;
            border: 1px solid #dee2e6;
            padding: 20px;
            margin: 20px;
            border-radius: 5px;
        }
        
        .formHeader
        {
            background-color: #37C1BB;
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
            color: #ffffff;
        }
        
        .form-group
        {
            margin-bottom: 15px;
        }
        
        .custom-button
        {
            background-color: #37C1BB;
            color: #ffffff; /* Text color, you can adjust it based on your preference */
        }
        
        .containerBorder
        {
            border: 3px solid #37C1BB;
            border-radius: 10px;
            margin: 20px;
            background-color: white;
            height: auto;
        }
        
        .containerBorder:hover
        {
            border-color: #018881;
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.2);
        }
        /* Add more custom styles as needed */
    </style>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class=" containerBorder ">
        <div class="form">
            <div class="formHeader">
                <asp:Label ID="lblTitle" runat="server" Text="Experience Detail"></asp:Label>
            </div>
            <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
                <asp:BulletedList ID="blErrs" runat="server">
                </asp:BulletedList>
            </asp:Panel>
            <asp:Button runat="server" ID="btnAddRow" Text="+ Add Experience" CssClass="btn btn-primary m-2"
                OnClick="btnAddRow_Click" ToolTip="Add New Records" />
            <label id="lblNotes" runat="server" style="font-family: Verdana; font-weight: bold;">
                Experience In Year and Month(E.g 1.2)</label>
            <div class="row" style="overflow: scroll;">
                <asp:GridView runat="server" ID="gvExperienceyDetails" AutoGenerateColumns="False"
                    CssClass="gridview-style" CellPadding="4" ForeColor="#333333" GridLines="None"
                    OnRowCommand="gvExperienceDetails_RowCommand" Width="1350px">
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#1C5E55" ForeColor="White" Font-Bold="True" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Company Name">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtCompanyName" CssClass="form-control" Text='<%# Eval("CompanyName") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Address">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtCompanyAddress" CssClass="form-control" Text='<%# Eval("CompanyAddress") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Designations">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtDesignations" CssClass="form-control" Text='<%# Eval("Designations") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Experience">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtExperience" CssClass="form-control" Text='<%# Eval("Experience") %>'
                                    onkeypress="return isNumber(event)"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last CTC Per Month">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtLastSalaryDetail" CssClass="form-control" Text='<%# Eval("LastSalaryDetail") %>'
                                    onkeypress="return isNumber(event)"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lnkRemove" CssClass="btn btn-danger" Text="X"
                                    ToolTip="Remove Records" CommandName="RemoveRow" CommandArgument='<%# Container.DataItemIndex %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#E3EAEB" />
                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                </asp:GridView>
            </div>
            <div class="row">
                <%--<div class="mt-3">--%>
                <%--<div class="class-lg-3 col-sm-6">
                    <asp:Button ID="btnSubmit" runat="Server" Text="Submit" class="btn btn-primary" OnClick="lnkBtnSubmit_click" />
                </div>--%>
                <div class="col-lg-1 col-sm-12 mt-3">
                    <asp:Button ID="btnSubmit" runat="Server" Text="Submit" class="btn btn-primary" OnClick="lnkBtnSubmit_click" />
                </div>
                <div class="col-lg-1 col-sm-12 mt-3">
                    <asp:Button ID="btnprint" runat="Server" Text="Print" class="btn btn-primary" OnClick="lnkPrint_click"
                        OnClientClick="target ='_blank';" />
                </div>
                <%--</div>--%>
                <div class="mt-2">
                    <asp:Label ID="lblMessage" runat="server" Text="Please Enter Mandatory Value" Style="color: Red;"
                        Visible="false"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
