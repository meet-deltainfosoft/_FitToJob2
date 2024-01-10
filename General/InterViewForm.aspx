<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="InterViewForm.aspx.cs" Inherits="General_InterViewForm" %>

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
    <script type="text/javascript">
        $(document).ready(function () {
            CallPostBack();
        });

        function CallPostBack() {
            $("#<%=txtDOB.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true, maxDate: 0 });
            $("#<%=txtDOB.ClientID%>").attr('readonly', true);

            $("#<%=txtNomineeDOB.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true, maxDate: 0 });
            $("#<%=txtNomineeDOB.ClientID%>").attr('readonly', true);
        }

        
   
    </script>
    <style type="text/css">
        .style1
        {
            width: 101px;
        }
        .style2
        {
            width: 102px;
        }
        .style3
        {
            width: 186px;
        }
        .style4
        {
            width: 78px;
        }
        .style5
        {
            width: 126px;
        }
    .style6
    {
        width: 50px;
    }
    .style7
    {
        width: 113px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            InterView Form Entry - Value
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label></div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblFullName" Text="Full Name :<em>*</em>"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" 
                            TabIndex="4" Height="16px" ></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblPresentAddress" Text="Present Address :<em>*</em>"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtPresentAddress" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblPresentPost" Text="Present Post :<em>*</em>"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtPresentPost" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblPresentVillage" Text="Present Village :<em>*</em>"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtPresentVillage" CssClass="form-control" 
                            TabIndex="1" Width="127px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblPresentDistrict" Text="Present District :<em>*</em>"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtPresentDistrict" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblPresentPinCode" Text="Present PinCode :<em>*</em>"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtPresentPinCode" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblPresentMobileNo" Text="Present Mobile No :<em>*</em>"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtPresentMobileNo" CssClass="form-control" TabIndex="1"
                            MaxLength="10"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPresentMobileNo"
                            ErrorMessage="Please enter valid Mobile Number" Display="Dynamic" ForeColor="Red"
                            ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblEmail" Text="Email :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtEmail" MaxLength="50" TabIndex="11"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator24" Visible="true"
                            runat="server" ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic"
                            ErrorMessage="Please enter a valid email address" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblPermanentAddress" Text="Permanent Address :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtPermanentAddress" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblPermanentPost" Text="Permanent Post :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtPermanentPost" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblPermanentVillage" Text="Permanent Village :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtPermanentVillage" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblPermanentDistrict" Text="Permanent District :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtPermanentDistrict" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblPermanentPinCode" Text="Permanent PinCode :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtPermanentPinCode" CssClass="form-control" MaxLength="6"
                            TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblPermanentMobileNo" Text="Permanent Mobile No :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtPermanentMobileNo" CssClass="form-control" TabIndex="1"
                            MaxLength="10"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPermanentMobileNo"
                            ErrorMessage="Please enter valid Mobile Number" Display="Dynamic" ForeColor="Red"
                            ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblDOB" Text="DOB :<em>*</em>"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtDOB" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblBloodGroup" Text="BloodGroup :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtBloodGroup" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblAadharCardNo" Text="AadharCardNo :<em>*</em>"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtAadharCardNo" CssClass="form-control" MaxLength="12"
                            TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblPanCardNo" Text="PanCard No :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtPanCardNo" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblElectionCardNo" Text="ElectionCardNo :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtElectionCardNo" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblCategory" Text="Category :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtCategory" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblFatherName" Text="Father Name :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtFatherName" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblFatherOccupation" Text="Father Occupation :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtFatherOccupation" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblFatherEducation" Text="Father Education :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtFatherEducation" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblFatherMobileNo" Text="Father MobileNo :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtFatherMobileNo" CssClass="form-control" MaxLength="10"
                            TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblMotherName" Text="Mother Name :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtMotherName" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblMotherOccupation" Text="Mother Occupation :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtMotherOccupation" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblMotherEducation" Text="Mother Education :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtMotherEducation" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblMotherMobileNo" Text="Mother MobileNo :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtMotherMobileNo" CssClass="form-control" MaxLength="10"
                            TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblWifeName" Text="Wife Name :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtWifeName" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblWifeOccupation" Text="Wife Occupation :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtWifeOccupation" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblWifeEducation" Text="Wife Education :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtWifeEducation" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblWifeMobileNo" Text="Wife MobileNo :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtWifeMobileNo" CssClass="form-control" MaxLength="10"
                            TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblBrotherName" Text="Brother Name :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtBrotherName" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblBrotherOccupation" Text="Brother Occupation :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtBrotherOccupation" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblBrotherEducation" Text="Brother Education :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtBrotherEducation" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblBrotherMobileNo" Text="Brother MobileNo :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtBrotherMobileNo" CssClass="form-control" MaxLength="10"
                            TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
                <tr>
               
                    <td class="style3">
                        <asp:Label runat="server" ID="lblNomineeName" Text="Nominee Name :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtNomineeName" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblNomineeDOB" Text="Nominee DOB :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtNomineeDOB" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblNomineeRelation" Text="Nominee Relation :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtNomineeRelation" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblNomineeAge" Text="Nominee Age :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtNomineeAge" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
                <tr>
                
                    <td class="style3">
                        <asp:Label runat="server" ID="lblStandanrd10Subject" Text="Standanrd Ten Subject :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtStandanrd10Subject" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblStandanrd10PassingYear" Text="Standanrd Ten PassingYear :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtStandanrd10PassingYear" CssClass="form-control"
                            TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblStandanrd10Percentage" Text="Standanrd Ten Percentage :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtStandanrd10Percentage" CssClass="form-control"
                            TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
                <tr>
               
                    <td class="style3">
                        <asp:Label runat="server" ID="lblStandanrd12Subject" Text="Standanrd Twelve Subject :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtStandanrd12Subject" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblStandanrd12PassingYear" Text="Standanrd Twelve PassingYear :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtStandanrd12PassingYear" CssClass="form-control"
                            TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblStandanrd12Percentage" Text="Standanrd Twelve Percentage :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtStandanrd12Percentage" CssClass="form-control"
                            TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
                <tr>
                
                    <td class="style3">
                        <asp:Label runat="server" ID="lblGraduateSubject" Text="Graduate Subject :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtGraduateSubject" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblGraduatePassingYear" Text="Graduate PassingYear :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtGraduatePassingYear" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblGraduatePercentage" Text="Graduate Percentage :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtGraduatePercentage" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
                <tr>
               
                    <td class="style3">
                        <asp:Label runat="server" ID="lblPostGraduateSubject" Text="Post Graduate Subject :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtPostGraduateSubject" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblPostGraduatePassingYear" Text="Post Graduate PassingYear :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtPostGraduatePassingYear" CssClass="form-control"
                            TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblPostGraduatePercentage" Text="Post Graduate Percentage :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtPostGraduatePercentage" CssClass="form-control"
                            TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
                <tr>
               
                    <td class="style3">
                        <asp:Label runat="server" ID="lblOtherSubject" Text="Other Subject :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtOtherSubject" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblOtherPassingYear" Text="Other PassingYear :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtOtherPassingYear" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblOtherPercentage" Text="Other Percentage :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtOtherPercentage" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
                <tr>
               
                    <td class="style3">
                        <asp:Label runat="server" ID="lblCertificateCourseName" Text="Certificate Course Name :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtCertificateCourseName" CssClass="form-control"
                            TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblCertificateCourseYear" Text="Certificate CourseYear :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtCertificateCourseYear" CssClass="form-control"
                            TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblTrainingName" Text="Training Name :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtTrainingName" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblTrainingYear" Text="Training Year :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtTrainingYear" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
                <tr>
              
                    <td class="style3">
                        <asp:Label runat="server" ID="lblMedalName" Text="Medal Name :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtMedalName" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblMedalYear" Text="Medal Year :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtMedalYear" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblOtherExpNoExpDetails" Text="Other ExpNoExpDetails :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtOtherExpNoExpDetails" CssClass="form-control"
                            TabIndex="4"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
                
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblFirstCompanyName" Text="First CompanyName :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtFirstCompanyName" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblFirstCompanyDesignation" Text="First CompanyDesignation :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtFirstCompanyDesignation" CssClass="form-control"
                            TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblFirstCompanyExp" Text="First CompanyExp :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtFirstCompanyExp" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblFirstCompanySalary" Text="First Company Salary :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtFirstCompanySalary" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
               
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblSecondCompanyName" Text="Second CompanyName :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtSecondCompanyName" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblSecondCompanyDesignation" Text="Second CompanyDesignation :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtSecondCompanyDesignation" CssClass="form-control"
                            TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblSecondCompanyExp" Text="Second CompanyExp :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtSecondCompanyExp" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblSecondCompanySalary" Text="Second CompanySalary :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtSecondCompanySalary" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                <td></td>
                <td></td>
                </tr>
                
                <tr>
                    <td class="style3">
                        <asp:Label runat="server" ID="lblThirdCompanyName" Text="Third CompanyName :"></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:TextBox runat="server" ID="txtThirdCompanyName" CssClass="form-control" TabIndex="4"></asp:TextBox>
                    </td>
                    <td align="right" class="style5">
                        <asp:Label runat="server" ID="lblThirdCompanyDesignation" Text="Third CompanyDesignation :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:TextBox runat="server" ID="txtThirdCompanyDesignation" CssClass="form-control"
                            TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style7">
                        <asp:Label runat="server" ID="lblThirdCompanyExp" Text="Third CompanyExp :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtThirdCompanyExp" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                    <td align="right" class="style1">
                        <asp:Label runat="server" ID="lblThirdCompanySalary" Text="Third Company Salary :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:TextBox runat="server" ID="txtThirdCompanySalary" CssClass="form-control" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                <td></td>
                <td></td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="lbladharcard" runat="server" Text="Adhar card : "></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:FileUpload ID="fuadharcard" runat="server" onchange="previewFile()" />
                    </td>
                    <td align="right" class="style5">
                        <asp:Label ID="lblelectioncard" runat="server" Text="Election card :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:FileUpload ID="fuelectioncard" runat="server" onchange="previewFile1()" />
                    </td>
                    <td align="right" class="style7">
                        <asp:Label ID="lblrationcard1" runat="server" Text="Ration card1 : "></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:FileUpload ID="furationcard1" runat="server" onchange="previewFile2()" />
                    </td>
                    <td align="right" class="style1">
                        <asp:Label ID="lblrationcard2" runat="server" Text="Ration card2 :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:FileUpload ID="furationcard2" runat="server" onchange="previewFile3()" />
                    </td>
                </tr>
                 <tr>
                <td></td>
                <td></td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="lblpancard" runat="server" Text="Pan card : "></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:FileUpload ID="fupancard" runat="server" onchange="previewFile4()" />
                    </td>
                    <td align="right" class="style5">
                        <asp:Label ID="lblphoto" runat="server" Text="Photo :"></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:FileUpload ID="fuphoto" runat="server" onchange="previewFile5()" />
                    </td>
                    <td align="right" class="style7">
                        <asp:Label ID="lblmarksheet" runat="server" Text="Mark sheet : "></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:FileUpload ID="fumarksheet" runat="server" onchange="previewFile6()" />
                    </td>
                    <td align="right" class="style1">
                        <asp:Label ID="lblcertificate" runat="server" Text="Certificate :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:FileUpload ID="fucertificate" runat="server" onchange="previewFile7()" />
                    </td>
                </tr>
                 <tr>
                <td></td>
                <td></td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="lblleavingcertificate1" runat="server" Text="Leaving Certificate 1 : "></asp:Label>
                    </td>
                    <td class="style4">
                        <asp:FileUpload ID="fuleavingcertificate1" runat="server" onchange="previewFile8()" />
                    </td>
                    <td align="right" class="style5">
                        <asp:Label ID="lblleavingcertificate2" runat="server" Text="Leaving Certificate 2 : "></asp:Label>
                    </td>
                    <td class="style6">
                        <asp:FileUpload ID="fuleavingcertificate2" runat="server" onchange="previewFile9()" />
                    </td>
                    <td align="right" class="style7">
                        <asp:Label ID="lblsalaryslip" runat="server" Text="Salary Slip :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:FileUpload ID="fusalaryslip" runat="server" onchange="previewFile10()" />
                    </td>
                    <td align="right" class="style1">
                        <asp:Label ID="lblappointmentletter" runat="server" Text="Appointment letter :"></asp:Label>
                    </td>
                    <td class="style2">
                        <asp:FileUpload ID="fuappointmentletter" runat="server" onchange="previewFile11()" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 90px" align="left">
                        <asp:Button runat="server" ID="btnDelete" Enabled="false" Text="Delete" OnClientClick="return confirm('Do you Want to Delete');"
                            OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 686px" align="right">
                        <asp:Button runat="server" ID="btnOk" Text="OK" OnClick="btnOk_Click" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
