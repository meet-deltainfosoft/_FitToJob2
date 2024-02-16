<%@ Page Title="Import Question" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="ImportQuestion.aspx.cs" Inherits="Exams_ImportQuestion" %>

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
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/jQuery.UI/jquery.ui.core.js"></script>
    <script type="text/javascript">
        var h = jQuery.noConflict();
    </script>
    <script type="text/javascript">
        h(document).ready(function () {


            //File Upload 
            h("#<%=fuFileName.ClientID%>").change(function () {
                __doPostBack("SetMSExcelFile");
            });
            //File Upload
        });
    </script>
    <style>
        body
        {
            font-family: 'Arial' , sans-serif;
        }
        
        .form
        {
            background-color: #ffffff;
            border: 1px solid #dee2e6;
            padding: 10px;
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
            margin-top: 20px;
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
        .container
        {
            max-width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="formHeader">
            Import Question
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <div class="row">
                <div class="col-lg-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblStandard" Text="Department :<em>*</em>" CssClass="label"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlStandard" AutoPostBack="true" CssClass="form-control"
                            OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblMonth" Text="Designation :<em>*</em>" CssClass="label"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlSubject" AutoPostBack="true" CssClass="form-control"
                            OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblTest" Text="Test :<em>*</em>" CssClass="label"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlTest" AutoPostBack="true" CssClass="form-control"
                            OnSelectedIndexChanged="ddlTest_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-lg-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblSheets" Text="Sheet :<em>*</em>" CssClass="label"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlSheetName" AutoPostBack="true" CssClass="form-control"
                            OnSelectedIndexChanged="ddlSheetName_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-sm-12">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblFileName" Text="Import MS Excel File :<em>*</em>"
                            CssClass="label"></asp:Label>
                        <asp:FileUpload runat="server" ID="fuFileName" AutoPostBack="true" CssClass="form-control" />
                        <a href="../MSExcelTemplates/QuestionMaster.xlsx">Download MS Excel Template</a>
                    </div>
                </div>
                <br />
                <div class="col-sm-1">
                    <div class="form-group">
                        <asp:Button runat="server" ID="btnImport" Text="Import" TabIndex="18" OnClick="btnImport_Click"
                            CssClass="btn btn-primary" Enabled="false" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="header">
                    <h2>
                        <asp:Label runat="server" ID="Label1" Text="Incentive List"></asp:Label>
                        <asp:Label runat="server" ID="lblCount"></asp:Label>
                    </h2>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:Panel runat="server" ID="pnlGrid" Style="overflow: auto;">
                    <asp:GridView runat="server" ID="gdvList" OnRowDataBound="gdvList_RowDataBound" Width="100%"
                        CssClass="myGridClass">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Excel Row No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 2 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
