<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DocumentUpload.aspx.cs" Inherits="Guest_DocumentUpload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
    <script src="https://code.jquery.com/jquery-3.6.4.min.js" integrity="sha256-oP6HI/tZ1a1BLjhjW2Xp7Jd9I9g0CUsdK1Qa5OE24gM="
        crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script type="text/javascript">
        var j = jQuery.noConflict();
        
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            //            $("#content1").hide();
            //            $("#content2").hide();
            //            $("#content3").hide();
            //            $("#content4").hide();
        });
    </script>
    <script type="text/javascript">
        function validateFile(inputField) {
            var fileInput = document.getElementById(inputField.id);
            var filePath = fileInput.value;
            var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;

            if (!allowedExtensions.exec(filePath)) {
                alert('Please upload file having extensions .jpg/.jpeg/.png/.gif only.');
                fileInput.value = '';
                return false;
            } else {
                var file = document.querySelector(inputField.id).files[0];
                var reader = new FileReader();
                return true;
            }


            console.log(inputField.id);

        }
    </script>
    <script>
        function toggleContent(icon, divid) {
            //            var content = document.getElementById(divid);

            //            if (content.style.display === "none") {
            //                content.style.display = "block";
            //                icon.classList.remove("fa-arrow-up");
            //                icon.classList.add("fa-arrow-down");
            //            } else {
            //                content.style.display = "none";
            //                icon.classList.remove("fa-arrow-down");
            //                icon.classList.add("fa-arrow-up");
            //            }
        }
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
    <style>
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
        
        .header-divider
        {
            border-bottom: 2px solid #000000; /* Border color for the header divider line */
            margin-bottom: 10px;
        }
        
        .form-group
        {
            margin-bottom: 15px;
        }
        
        .custom-button
        {
            background-color: #37C1BB;
            color: #ffffff;
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
            font-weight: bold;
            font-size: 1em !important;
            
        }
        
        
        /* Add more custom styles as needed */
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container">
        <div class="form">
            <div class="formHeader" class="formHeader header-divider">
                Document Upload
                <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
            </div>
            <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
                <asp:BulletedList ID="blErrs" runat="server">
                </asp:BulletedList>
            </asp:Panel>
            <div class="formBody">
                <div class="form">
                    <div class="formHeader">
                        <div class="row">
                            <div class="col-md-6 col-12">
                                Photograph
                            </div>
                            <div class="col-md-6 col-12 d-flex justify-content-end">
                               <%-- <i class="fa fa-arrow-up" aria-hidden="true" onclick="toggleContent(this,'content1')">
                                </i>--%>
                            </div>
                        </div>
                    </div>
                    <div class="formBody" runat="server" id="content1">
                        <div class="row">
                            <div class="col-md-12 col-12">
                                <asp:GridView runat="server" ID="gdvPhotograph" AutoGenerateColumns="False" CssClass="gridview-style"
                                    SkinID="Lns" EmptyDataText="No Records Found." OnRowDataBound="gdvPhotograph_RowDataBound"
                                    ShowHeader="false" BorderStyle="None" CellPadding="0" CellSpacing="0">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <div class="form-label">
                                                    <span>Photo </span>
                                                    <asp:HiddenField ID="hfCandidateId" runat="server" Value='<%# Eval("CandidateId") %>' />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Upload Photo">
                                            <ItemTemplate>
                                                <asp:FileUpload ID="fuPhotograph" runat="server" />
                                                
                                                <asp:Label runat="server" ID="hfPhotographPath" Text='<%# Eval("PhotoPath")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Photo">
                                            <ItemTemplate>
                                                <a href='<%# Eval("PhotoPath") %>' download>Download Document</a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form">
                    <div class="formHeader">
                        <div class="row">
                            <div class="col-md-6 col-12">
                                Identification
                            </div>
                            <div class="col-md-6 col-12 d-flex justify-content-end">
                               <%-- <i class="fa fa-arrow-up" aria-hidden="true" onclick="toggleContent(this,'content2')">
                                </i>--%>
                            </div>
                        </div>
                    </div>
                    <div class="formBody" runat="server" id="content2">
                        <div class="row">
                            <div class="col-md-12 col-12">
                                <asp:GridView runat="server" ID="gdvIdentification" AutoGenerateColumns="False" CssClass="gridview-style"
                                    SkinID="Lns" EmptyDataText="No Records Found." OnRowDataBound="gdvIdentification_RowDataBound"
                                    ShowHeader="false" BorderStyle="None" CellPadding="0" CellSpacing="0">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <div class="form-label">
                                                    <span runat="server" id="CardName">
                                                        <%# Eval("CardName")%>
                                                    </span>
                                                    <asp:HiddenField ID="hfRegistrationId" runat="server" Value='<%# Eval("RegistrationId") %>' />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Upload Photo">
                                            <ItemTemplate>
                                                <asp:FileUpload ID="fuIdentification"  runat="server" />
                                                
                                                <asp:Label runat="server" ID="hfIdentificationPath" Text='<%# Eval("CardPath")%>'  Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Photo">
                                            <ItemTemplate>
                                                <a href='<%# Eval("CardPath") %>' download>Download Document</a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form">
                    <div class="formHeader">
                        <div class="row">
                            <div class="col-md-6 col-12">
                                Education
                            </div>
                            <div class="col-md-6 col-12 d-flex justify-content-end">
                               <%-- <i class="fa fa-arrow-up" aria-hidden="true" onclick="toggleContent(this,'content3')">
                                </i>--%>
                            </div>
                        </div>
                    </div>
                    <div class="formBody" runat="server" id="content3">
                        <div class="row">
                            <div class="col-md-12 col-12">
                                <asp:GridView runat="server" ID="gdvEducation" AutoGenerateColumns="False" CssClass="gridview-style"
                                    SkinID="Lns" EmptyDataText="No Records Found." OnRowDataBound="gdvEducation_RowDataBound"
                                    ShowHeader="false" BorderStyle="None" CellPadding="0" CellSpacing="0">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <div class="form-label">
                                                    <span runat="server" id="EducationLevel">
                                                        <%# Eval("EducationLevel")%>
                                                    </span>
                                                    <asp:HiddenField ID="hfEducationId" runat="server" Value='<%# Eval("EducationId") %>' />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Upload Photo">
                                            <ItemTemplate>
                                                <asp:FileUpload ID="fuEducationLevel" runat="server" />
                                                <asp:Label ID="hfEducationPath" runat="server" Text='<%# Eval("DocumentPath")%>'  Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Photo">
                                            <ItemTemplate>
                                                <a  href='<%# Eval("DocumentPath") %>' download>Download Document</a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form">
                    <div class="formHeader">
                        <div class="row">
                            <div class="col-md-6 col-12">
                                Work Experience
                            </div>
                            <div class="col-md-6 col-12 d-flex justify-content-end">
                              <%--  <i class="fa fa-arrow-up" aria-hidden="true" onclick="toggleContent(this,'content4')">
                                </i>--%>
                            </div>
                        </div>
                    </div>
                    <div class="formBody" runat="server" id="content4">
                        <div class="row">
                            <asp:GridView runat="server" ID="gdvWorkExperience" AutoGenerateColumns="False" CssClass="gridview-style"
                                SkinID="Lns" EmptyDataText="No Records Found." OnRowDataBound="gdvWorkExperience_RowDataBound"
                                ShowHeader="false" BorderStyle="None" CellPadding="0" CellSpacing="0">
                                <Columns>
                                 <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <div class="form-label">
                                                    <span runat="server" id="Label">
                                                        <%# Eval("Label")%>
                                                    </span>
                                                    <asp:HiddenField ID="hfRegistrationId" runat="server" Value='<%# Eval("RegistrationId") %>' />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Upload Photo">
                                            <ItemTemplate>
                                                <asp:FileUpload ID="fuWorkExperience" runat="server" />
                                                <asp:Label runat="server" ID="hfDocumentPath" Text='<%# Eval("DocumentPath")%>'  Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Photo">
                                            <ItemTemplate>
                                                <a href='<%# Eval("DocumentPath") %>' download>Download Document</a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="row mt-1">
                    <div class="class-lg-3 col-sm-6">
                        <asp:Button ID="lnkBtnSubmit" runat="Server" Text="Save & Next" class="btn btn-primary"
                            OnClick="lnkBtnSubmit_click" CausesValidation="true" />
                        <asp:Label ID="lblDigitalSignature" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
