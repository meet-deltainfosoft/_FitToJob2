<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="Registration1.aspx.cs" Inherits="General_Registration1" %>

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
    <script type="text/javascript">
        var j = jQuery.noConflict();
    </script>
    <script type="text/javascript">

        function previewFile() {

            var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
            var file = document.querySelector('#<%=fuPhoto.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
       
    </script>
    <script type="text/javascript">
        function previewFile1() {

            var preview = document.querySelector('#<%=imgSelfintravideo.ClientID %>');
            var file = document.querySelector('#<%=fuSelfintravideo.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }
       
    </script>
    <script type="text/javascript">

        function previewFile3() {

            var preview = document.querySelector('#<%=imgResumeUpload.ClientID %>');
            var file = document.querySelector('#<%=fuResumeUpload.ClientID %>').files[0];
            var reader = new FileReader();

            reader.onloadend = function () {
                preview.src = reader.result;
            }

            if (file) {
                reader.readAsDataURL(file);
            } else {
                preview.src = "";
            }
        }

       
       
    </script>
    <script type="text/javascript">
        j(document).ready(function () {

            j("#<%=txtMobileNo.ClientID%>").numeric();
            j("#<%=txtAadharNo.ClientID%>").numeric();

        });
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="form">
            <div class="formHeader">
                Registration Entry - Value
                <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
            </div>
            <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
                <asp:BulletedList ID="blErrs" runat="server">
                </asp:BulletedList>
            </asp:Panel>
            <div class="formBody">
                <div class="row">
                    <div class="col-lg-4 col-sm-12">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblAadharNo" Text="AadharCard No :<em>*</em>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtAadharNo" CssClass="form-control" MaxLength="12"
                                TabIndex="1" AutoPostBack="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-12">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblFirstname" Text="First Name :<em>*</em>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtFirstname" CssClass="form-control" TabIndex="2"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-12">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblMiddlename" Text="Middle Name :<em>*</em>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtMiddlename" CssClass="form-control" TabIndex="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-12">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblLastName" Text="Last Name :<em>*</em>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtLastname" CssClass="form-control" TabIndex="4"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-12">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblMobileNo" Text="Mobile No :<em>*</em>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtMobileNo" CssClass="form-control" TabIndex="5"
                                MaxLength="10"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobileNo"
                                ErrorMessage="Please enter valid Mobile Number" Display="Dynamic" ForeColor="Red"
                                ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-12">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblCity" Text="City / Village :<em>*</em>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtCity" CssClass="form-control" TabIndex="6"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-12">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblTaluka" Text="Taluka :<em>*</em>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtTaluka" CssClass="form-control" TabIndex="7"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-12">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblDistrict" Text="District :<em>*</em>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtDistrict" CssClass="form-control" TabIndex="8"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-4 col-sm-12">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblState" Text="State :<em>*</em>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtState" CssClass="form-control" TabIndex="9"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-12 col-sm-12">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblAddress" Text="Address :<em>*</em>"></asp:Label>
                            <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" TabIndex="10" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-lg-2">
                        <asp:Label ID="lblPhoto" runat="server" Text="Photo : "></asp:Label>
                        <asp:FileUpload ID="fuPhoto" runat="server" onchange="previewFile()" class="form-control" />
                    </div>
                    <div class="col-lg-3">
                        <asp:Image ID="imgPhoto" runat="server" Height="158.4px" TabIndex="11" Visible="True"
                            Width="120px" />
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="lblSelfintravideo" runat="server" Text="Self Intro Video :"></asp:Label>
                        <asp:FileUpload ID="fuSelfintravideo" runat="server" onchange="previewFile1()" />
                    </div>
                    <div class="col-lg-3">
                        <asp:Label ID="lblResume" runat="server" Text="Resume Upload :"></asp:Label>
                        <asp:FileUpload ID="fuResumeUpload" runat="server" onchange="previewFile3()" />
                    </div>
                    <table style="display: none">
                        <tr>
                            <td align="left">
                                <asp:Image ID="imgSelfintravideo" runat="server" Height="158.4px" TabIndex="12" Visible="True"
                                    Width="120px" />
                            </td>
                            <td align="right">
                            </td>
                            <td>
                            </td>
                            <td align="left">
                                <asp:Image ID="imgResumeUpload" runat="server" Height="158.4px" TabIndex="13" Visible="True"
                                    Width="120px" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="formFooter">
                <div class="row">
                    <div class="col-lg-2 m-1">
                        <asp:Button runat="server" ID="btnOk" Text="OK" OnClick="btnOk_Click" class="btn btn-success" style="height: 30px; width: 50px; font-size: 15px;" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" 
                            class="btn btn-danger" style="height: 30px; width: 80px; font-size: 15px;" />
                    </div>
                    <div class="col-lg-3">
                        <asp:Button runat="server" ID="btnDelete" Enabled="false" Text="Delete" class="btn btn-danger"
                            OnClientClick="return confirm('Do you Want to Delete');" OnClick="btnDelete_Click" />
                    </div>
                </div>
            </div>
            <div class="row m-2">
            </div>
        </div>
    </div>
</asp:Content>
