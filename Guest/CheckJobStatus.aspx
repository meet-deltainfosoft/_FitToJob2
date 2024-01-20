﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckJobStatus.aspx.cs" Inherits="Guest_CheckJobStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <script type="text/javascript">
        function showRejectPopup() {
            $('#rejectPopup').modal('show');
        }

        function rejectConfirmed() {
            // Get the value of remarks textbox
            var remarks = $('#txtRejectRemarks').val();

            // Check if remarks are provided
            //if (remarks.trim() !== '') {
            // Close the popup
            $('#rejectPopup').modal('hide');

            // Open the popup window
            window.open("empchoosefoldertosavepopup.aspx", 'popUpWindow', 'height=350,width=650,left=100,top=30,resizable=No,scrollbars=No,toolbar=no,menubar=no,location=no,directories=no,status=No');
            //} else {
            // Alert user to provide remarks
            //alert("Please provide remarks before closing the popup.");
            //}
        }
    </script>
    <style type="text/css">
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
            width: auto;
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
            width: 100%;
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
            width: 120px;
        }
        label
        {
            font-family: Arial, sans-serif;
            font-size: 18px;
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
        .button
        {
            color: White;
            background-color: #37C1BB;
            margin-left: auto;
        }
        .Remarks
        {
            width: inherit;
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
    <link rel="stylesheet" type="text/css" href="styles.css">
</head>
<body>
    <form id="form1" runat="server">
    <div class="form">
        <div class="formHeader">
            <asp:Label ID="lblTitle" runat="server" Text="Offer Latter"></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <%--  <div class="container">--%>
        <div class="row">
            <div class="col-lg-12 col-sm-12" style="overflow: auto;">
                <div class="table">
                    <asp:GridView runat="server" ID="gdvJobOfferLatter" AutoGenerateColumns="False" CssClass="gridview-style"
                        SkinID="Lns" EmptyDataText="No Records Found." OnRowCommand="gdvJobOfferLatter_RowCommand" OnRowDataBound="gdvJobOfferLatter_RowDataBound"
                        DataKeyNames="CandidateId">
                        <Columns>
                            <%-- <asp:Button runat="server" ID="btnOffer" Text="View Offer latter" CssClass="btn btn-primary" />--%>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <div class="form-control">
                                        <%# Eval("FirstName")%>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Job Profile">
                                <ItemTemplate>
                                    <div class="form-control">
                                        <%# Eval("JobProfile")%>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salary Per Anum">
                                <ItemTemplate>
                                    <div class="form-control">
                                        <%# Eval("Salary")%>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Joining Date">
                                <ItemTemplate>
                                    <div class="form-control">
                                        <%# Eval("JoiningDate")%>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Work Place">
                                <ItemTemplate>
                                    <div class="form-control">
                                        <%# Eval("WorkPlace")%>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <div style="width: 350px;">
                                        <asp:Button runat="server" ID="btnOffer" Text="View Offer Latter" CssClass="btn btn-primary mr-2"
                                            CommandName="ViewOfferLatter" CommandArgument='<%# Eval("JobId") %>' />
                                        <asp:Button runat="server" ID="btnAccept" Text="Accept" CssClass="btn btn-success mr-2"
                                            CommandName="AcceptRow" CommandArgument='<%# Eval("JobId") %>' />
                                        <asp:Button runat="server" ID="btnReject" Text="Reject" CssClass="btn btn-danger"
                                            CommandName="RejectRow" CommandArgument='<%# Eval("JobId") %>' />
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rejection Remarks" HeaderStyle-CssClass="header-style-remarks">
                                <ItemTemplate>
                                    <headerstyle horizontalalign="Left" verticalalign="Top" cssclass="header-style" />
                                    <asp:TextBox runat="server" ID="txtremarks" Enabled="true" CssClass="form-control"
                                        Text='<%# Eval("RejectionRemarks") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acceptance Status" HeaderStyle-CssClass="header-style-remarks">
                                <ItemTemplate>
                                    <headerstyle horizontalalign="Left" verticalalign="Top" cssclass="header-style" />
                                    <asp:TextBox runat="server" ID="txtAcceptance" Enabled="true" CssClass="form-control"
                                        Text='<%# Eval("CandidateApprovalStatus") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <%--  </div>--%>
    <div class="modal" id="myModal">
        <div class="modal-dialog">
            <div class="modal-content">
                <!-- Modal Header -->
                <div class="modal-header">
                    <h4 class="modal-title">
                        Are you sure you want to reject the offer?</h4>
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                </div>
                <!-- Modal Body -->
                <div class="modal-body">
                    <!-- Your popup content goes here -->
                    <div class="form-group row">
                        <div class="col-12">
                            <asp:TextBox runat="server" ID="txtRejectRemarks" CssClass="form-control Remarks"
                                TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <!-- Modal Footer -->
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal" onclick="SaveAcceptanceStatus()">
                        Yes</button>
                    <button type="button" class="btn btn-secondary" onclick="SaveAcceptanceStatus()">
                        No</button>
                </div>
            </div>
        </div>
    </div>
    <div class="mt-1">
    </div>
    </form>
</body>
</html>