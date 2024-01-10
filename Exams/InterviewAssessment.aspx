<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InterviewAssessment.aspx.cs"
    MasterPageFile="~/Delta_MCQ.master" Inherits="Exams_InterviewAssessment" %>

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
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"
        integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"
        crossorigin="anonymous">
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css"
        integrity="sha512-jN53oRtrw3z+3iPYAK5QVrAJS3IdNTe8c0gkyaDA5ZIUVsm+Jb94ZvLNKLO5U+Q98s3UfcddoGezpKNBo1i5Hg=="
        crossorigin="anonymous" />
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();
    </script>
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
            border: 5px solid #dee2e6;
            padding: 20px;
            margin: 20px;
            border-radius: 5px;
            font-family: 'Arial' , sans-serif;
        }
        
        .formHeader
        {
            background-color: #37C1BB;
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 20px;
            color: #ffffff;
            font-family: 'Arial' , sans-serif;
        }
        
        .container
        {
            margin-right: 60px;
            margin-left: 25px;
            width: 100%;
            font-family: 'Arial' , sans-serif;
        }
        
        .gridview-style
        {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        
        .gridview-style th, .gridview-style td
        {
            border: 1px solid #dee2e6;
            padding: 8px;
        }
        .gridview-style th, .gridview-style td
        {
            border: 1px solid #dee2e6;
            padding: 4px;
            font-size: small;
            font-size: small;
            font-weight: normal;
            font-size: small !important;
        }
        
        .form-control
        {
            width: 200px; /* You can adjust the width as needed */
            display: block;
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            line-height: 1.5;
            color: #333; /* Set your dark color here */
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: 0.25rem;
            transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
        }
        
        .custom-button
        {
            background-color: #37C1BB;
            color: #ffffff;
        }
        
        .header-style, .header-style-remarks
        {
            background-color: #1c5e55 !important;
            color: #fff;
            font-size: medium !important;
        }
        
        .mt-3
        {
            margin-top: 1rem;
        }
        
        .mt-1
        {
            margin-top: 0.25rem;
        }
        
        .row
        {
            text-align: center;
        }
        
        .table
        {
            display: grid;
            grid-template-columns: 1fr 1fr 80%;
            max-width: 1500px;
            font-family: 'Arial' , sans-serif;
        }
        .status-dropdown
        {
            width: 150px;
        }
        label
        {
            font-family: Arial, sans-serif;
            font-size: 14 px;
            color: #333;
            font-weight: bold;
            display: block; /* This makes the label a block element, so it will start on a new line */
            margin-bottom: 5px; /* Adjust the margin as needed */
        }
        .custom-label
        {
            background-color: #f2f2f2;
            padding: 4px;
        }
        table tr th
        {
            background-color: #37C1BB;
            color: #ffff;
            font-weight: bold;
            font-family: Verdana;
        }
    </style>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        
    </script>
    <style type="text/css">
        .form-label
        {
            margin-right: 5px;
            font-family: 'Verdana' , sans-serif;
            font-weight: bold;
            font-size: medium !important;
            width: auto !important;
        }
        
        .form-label span
        {
            font-family: 'Verdana' , sans-serif;
            font-weight: bold;
            display: block;
            font-size: 13px;
        }
        .header-style
        {
            font-family: 'Verdana' , sans-serif !important;
            font-size: medium !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            <asp:Label ID="lblTitle" runat="server" Text=" Interview Assessment"></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="form-body m-3">
            <div class="row">
                <div class="col-lg-1">
                    <label style="font-family: Verdana; font-weight: bold; font-size: 13px;">
                        Candidate:</label>
                </div>
                <div class="col-lg-6">
                    <asp:DropDownList ID="DDLCandidate" runat="server" CssClass="form-control" Width="350px">
                    </asp:DropDownList>
                </div>
            </div>
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
                                        Style="width: 600px; display: inline-block;" align="left"></asp:Label>
                                    <%--  <asp:Label runat="server" ID="txtAssessmentDescription" CssClass="form-control" Text='<%# Eval("AssessmentDescription") %>'></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Points">
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtPoints" CssClass="form-control" Text='<%# Eval("Points") %>'
                                        Style="width: 80px" ReadOnly="true"></asp:TextBox>
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
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="row mt-2">
                <div class="col-lg-1">
                    <asp:Label ID="lblStatus" runat="server" Text="Status:-" class="radio-label"></asp:Label>
                </div>
                <div class="col-lg-1">
                    <asp:RadioButton runat="server" ID="rdbPass" Text="Pass" GroupName="Status" />
                    <label for="rdbPass" class="radio-label">
                    </label>
                </div>
                <div class="col-lg-1">
                    <asp:RadioButton runat="server" ID="rdbFail" Text="Fail" GroupName="Status" />
                    <label for="rdbFail" class="radio-label">
                    </label>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-2">
                    <asp:Label ID="Label1" runat="server" Text="Failed Remarks" class="radio-label d-flex align-items-left"></asp:Label>
                    <asp:TextBox TextMode="MultiLine" runat="server" ID="txtRemarks" CssClass="form-control d-flex align-items-left" Width="350px">
                    </asp:TextBox>
                </div>
            </div>
            <div class="row mt-2">
                <asp:Button ID="btnSubmit" runat="Server" Text="Submit" CssClass="btn btn-primary" style="height:30px;"
                    OnClick="lnkBtnSubmit_click" />
            </div>
        </div>
    </div>
</asp:Content>
