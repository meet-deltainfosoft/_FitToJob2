<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InterviewAssessment.aspx.cs"
    Inherits="Guest_InterviewAssessment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        .formHeader
        {
            background-color: #ffc000;
            height: 50px;
            color: #7530a0;
            font-weight: bold;
        }
        .radio-label
        {
            margin-right: 10px;
            color: #7530a0;
            font-weight: bold;
        }
        .radio-button
        {
            /* Add your custom styling for the radio buttons */
            margin-right: 5px;
            color: #7530a0;
            font-weight: bold;
        }
        .btnSubmit
        {
            font-weight: bold;
            color: white;
            background-color: #00a19b;
        }
    </style>
</head>
<body>
    <form id="InterviewAssessment" runat="server">
    <div class="formHeader" style="background-color: #ffc000;">
        Interview Assessment
        <asp:Label ID="lblTitle" runat="server"></asp:Label>
    </div>
    <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
        <asp:BulletedList ID="blErrs" runat="server">
        </asp:BulletedList>
    </asp:Panel>
    <div class="form-body m-3">
        <div class="row">
            <div class="co-md-12">
                <asp:GridView runat="server" ID="gridPoints" AutoGenerateColumns="False" CssClass="gridview-style"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sr No">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblsrNo" Text='<%# Eval("srNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Attributes to be Assessed">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblAssessmentDescription" Text='<%# Eval("AssessmentDescription") %>'
                                    Style="width: 600px; display: inline-block;"></asp:Label>
                                <%--  <asp:Label runat="server" ID="txtAssessmentDescription" CssClass="form-control" Text='<%# Eval("AssessmentDescription") %>'></asp:Label>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Points">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtPoints" CssClass="form-control" Text='<%# Eval("Points") %>'
                                    Style="width: 80px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HR">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtHR" CssClass="form-control" Text='<%# Eval("HR") %>'
                                    Style="width: 80px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="HOD">
                            <ItemTemplate>
                                <asp:TextBox runat="server" ID="txtHOD" CssClass="form-control" Text='<%# Eval("HOD") %>'
                                    Style="width: 80px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                      <%--  <editrowstyle backcolor="#7C6F57" />
                        <footerstyle backcolor="#ffc000" font-bold="True" forecolor="#7530a0" />
                        <headerstyle backcolor="#ffc000" font-bold="True" forecolor="#7530a0" />
                        <pagerstyle backcolor="#666666" forecolor="White" horizontalalign="Center" />
                        <rowstyle backcolor="#E3EAEB" />
                        <selectedrowstyle backcolor="#C5BBAF" font-bold="True" forecolor="#333333" />
                        <sortedascendingcellstyle backcolor="#F8FAFA" />
                        <sortedascendingheaderstyle backcolor="#246B61" />
                        <sorteddescendingcellstyle backcolor="#D4DFE1" />
                        <sorteddescendingheaderstyle backcolor="#15524A" />--%>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row">
            <div class="co-md-12 rediobutton">
                <asp:Label ID="lblStatus" runat="server" Text="Status:-" class="radio-label"></asp:Label>
                <asp:RadioButton runat="server" ID="rdbPass" Text="Pass" GroupName="Status" />
                <label for="rdbPass" class="radio-label">
                </label>
                <asp:RadioButton runat="server" ID="rdbFail" Text="Fail" GroupName="Status" />
                <label for="rdbFail" class="radio-label">
                </label>
            </div>
        </div>
        <div class="row">
            <asp:Button ID="btnSubmit" runat="Server" Text="Submit" CssClass="btnSubmit" />
        </div>
    </div>
    </form>
</body>
</html>
