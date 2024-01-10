<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="DigitalInterviewForm.aspx.cs" Inherits="Exams_CandidatesList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"
        rel="stylesheet" integrity="sha384-Zg3/QznJ60z6p7sTIfxk6UEqGB5KtFTo+5YF/Jg1iNTnJU6Boxx5Q8ILx4QMWL3NW"
        crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-Vgt/pz0U6XkW5qVd5NS5VFE1n5tE7Vq6GnvwhBcFkT5XgYGZ8ybk6IHTtPXTtXbl"
        crossorigin="anonymous"></script>--%>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css"
        rel="stylesheet" integrity="sha384-Zg3/QznJ60z6p7sTIfxk6UEqGB5KtFTo+5YF/Jg1iNTnJU6Boxx5Q8ILx4QMWL3NW"
        crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-Vgt/pz0U6XkW5qVd5NS5VFE1n5tE7Vq6GnvwhBcFkT5XgYGZ8ybk6IHTtPXTtXbl"
        crossorigin="anonymous"></script>
    <!-- Bootstrap DateTimePicker CSS and JS -->
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-datetimepicker@4.17.47/build/css/bootstrap-datetimepicker.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap-datetimepicker@4.17.47/build/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();
    </script>
    <style type="text/css">
        /* Custom CSS styles for your form */
        body
        {
            font-family: 'Arial' , sans-serif;
            background-color: #FFF;
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
    <script type="text/javascript">
        // Initialize the DateTimePicker
        $(function () {
            $('#dtPicker').datetimepicker({
                format: 'MM/DD/YYYY',
                useCurrent: false
            });
        });
    </script>
    <style type="text/css">
        .Button
        {
            height: 35px !important;
            width: 200px;
            font-family: Verdana !important;
            font-size: small !important;
        }
        .control-label
        {
            font-family: Verdana !important;
            font-size: small !important;
        }
        .formHeader
        {
            width: 102% !important;
            height: 30px !important;
            margin-top: -15px;
            margin-left: -17px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="form">
        <div class="formHeader">
            <asp:Label ID="lblTitle" runat="server" Text="Digital Interview Form List" Style="font-family: Verdana;
                font-size: medium; font-weight: bold;"></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="row">
            <div class="col-sm-3">
                <asp:Label ID="lblStatus" runat="server">Status</asp:Label>
            </div>
            <div class="col-sm-3">
            </div>
            <%--<div class="col-lg-4 col-md-6 mb-2">
                    <asp:Button runat="server" ID="btnFilter" Text="Search" CssClass="btn btn-primary Button"
                        OnClick="btnFilter_Click" />
                </div>--%>
        </div>
        <div class="row">
            <div class="col-lg-3">
                Status
            </div>
            <div class="col-lg-3">
                <asp:DropDownList runat="server" ID="ddlFilter" AutoPostBack="true" CssClass="form-control">
                    <asp:ListItem Value="ALL" Text="All"></asp:ListItem>
                    <asp:ListItem Value="P" Text="Pending"></asp:ListItem>
                    <asp:ListItem Value="A" Text="Approved"></asp:ListItem>
                    <asp:ListItem Value="R" Text="Reject"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="row mt-3" style="overflow: scroll;">
        <div class="table">
            <asp:GridView runat="server" ID="gdvDigitalInterview" AutoGenerateColumns="False"
                CssClass="gridview-style" SkinID="Lns" EmptyDataText="No Records Found." OnRowDataBound="gdvDigitalInterview_RowDataBound"
                DataKeyNames="RegistrationId" OnSelectedIndexChanged="gdvDigitalInterview_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlApproved" AutoPostBack="true" CssClass="form-control status-dropdown"
                                OnSelectedIndexChanged="ddlApproved_SelectedIndexChanged" SelectedValue='<%# Eval("Status") %>'>
                                <asp:ListItem Value="A" Text="Approved"></asp:ListItem>
                                <asp:ListItem Value="P" Text="Pending" Style="color: Blue;"></asp:ListItem>
                                <asp:ListItem Value="R" Text="Reject" Style="color: red;"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <div class="form-label">
                                <span>
                                    <%# Eval("FirstName") %></span>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Surname">
                        <ItemTemplate>
                            <div class="form-label">
                                <span>
                                    <%# Eval("LastName") %></span>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Taluka">
                        <ItemTemplate>
                            <div class="form-label">
                                <span>
                                    <%# Eval("Taluka") %></span>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Job Profile">
                        <ItemTemplate>
                            <div class="form-label" style="width: 150px !important;">
                                <span>
                                    <%# Eval("JobProfile") %></span>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Exam">
                        <ItemTemplate>
                            <div class="form-label">
                                <span>
                                    <%# Eval("Exam")%></span>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Distance">
                        <ItemTemplate>
                            <div class="form-label">
                                <span>
                                    <%# Eval("Distance")%></span>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Company Name">
                        <ItemTemplate>
                            <div class="form-label" style="width: 200px !important;">
                                <span>
                                    <%# Eval("CompanyName")%></span>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Salary">
                        <ItemTemplate>
                            <div class="form-label" style="width: 150px !important;">
                                <span>
                                    <%# Eval("LastSalaryDetail")%>
                                </span>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salary Expect">
                        <ItemTemplate>
                            <div class="form-label" style="width: 150px !important;">
                                <span>
                                    <%# Eval("ExpectSalary")%></span>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Experiance">
                        <ItemTemplate>
                            <div class="form-label">
                                <span>
                                    <%# Eval("Experiance")%></span>
                            </div>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="header-style-remarks">
                        <ItemTemplate>
                            <headerstyle horizontalalign="Left" verticalalign="Top" cssclass="header-style" />
                            <asp:TextBox runat="server" ID="txtremarks" Enabled="false" CssClass="form-control"
                                Text='<%# Eval("Remarks")%>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField HeaderText="PhotoPath" Target="_blank">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="VideoPath">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="Resume" Target="_blank">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
                    <asp:HyperLinkField HeaderText="ViewPrint" Target="_blank">
                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:HyperLinkField>
                    <%--<asp:HyperLinkField HeaderText="Send Mail">
                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" CssClass="header-style" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:HyperLinkField>--%>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="row mt-3">
        <div class="col-lg-3" style="text-align: left">
            <asp:Button runat="server" ID="btnSubmit" Text="Submit" CssClass="btn btn-primary Button"
                OnClick="btnSubmit_Click" />
        </div>
    </div>
    </div>

    <script src="https://code.jquery.com/jquery-3.6.0.min.js" integrity="sha384-oJhIZV22Bqp6SN79b9llgl3R/MK9EtF7Lk/jW40G1d8Hu0z1e7F2Qqezw5DPcZ3g" crossorigin="anonymous"></script>
    
</asp:Content>
