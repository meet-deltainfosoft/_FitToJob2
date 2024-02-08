<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Division.aspx.cs" Inherits="Guest_Division" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <link type="text/css" href="../jquery/timepicker/jquery.ui.timepicker.css" />
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"
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
            padding: 10px;
            margin: 25px;
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
            background-color: white;
            height: auto;
            margin: 20px;
        }
        
        .containerBorder:hover
        {
            border-color: #018881; 
            box-shadow: 0 0 15px rgba(0, 0, 0, 0.2); 
            
        }
        
        /* Add more custom styles as needed */
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class=" containerBorder ">
        <div class="form">
            <div class="formHeader">
                Division
                <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label></div>
            <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
                <asp:BulletedList ID="blErrs" runat="server">
                </asp:BulletedList>
            </asp:Panel>
            <div class="formBody">
                <div class="row">
                    <div class="col-lg-12 col-sm-12">
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblDivisionId" Text="Selected Divisions :" Font-Bold="true"></asp:Label>
                            <span style="color: red">*</span>
                            <br />
                            <asp:CheckBox ID="chkallDivision" runat="server" AutoPostBack="True" Text="Select All"
                                OnCheckedChanged="chkallDivision_CheckedChanged" Font-Bold="true" Visible="false" />
                            <asp:CheckBoxList runat="server" ID="chkDivision" TabIndex="4" RepeatLayout="Table"
                                RepeatDirection="Horizontal" RepeatColumns="1">
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <div class="formFooter">
                    <div class="row">
                        <div class="col-lg-8 m-1">
                            <asp:Button runat="server" ID="btnOk" Text="OK" OnClick="btnOk_Click" class="btn custom-button"
                                Style="height: 30px; width: 200px; font-size: 15px;" />
                            <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click"
                                class="btn custom-button" Style="height: 30px; width: 80px; font-size: 15px;" />
                        </div>
                        <div class="col-lg-12  m-1">
                            <asp:Label ID="lblMessage" runat="Server" Text="Please Select at Least one Division"
                                ForeColor="Red" Visible="false"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
