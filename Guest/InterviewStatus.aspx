<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InterviewStatus.aspx.cs"
    Inherits="Guest_InterviewStatus" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">--%>
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
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <%--<link type="text/css" href="../css/Interviewstatus.css" />--%>
    <script type="text/javascript">

        $(document).ready(function () {
            $(".right-column-item").addClass("col-md-6 col-12");

        });

       

    </script>
    <style type="text/css">
         body
        {
            font-family: Verdana;
            font-size: small;
            color: #37C1BB;
            background-color: #37C1BB;
            border-radius-webkit: 20px !important;
            font-weight: bold;
        }
        ::-webkit-scrollbar
        {
            width: 0px;
            height: 0px;
        }
        
        ::-webkit-scrollbar-thumb
        {
            background: rgba(90, 90, 90);
        }
        
        ::-webkit-scrollbar-track
        {
            background: rgba(0, 0, 0, 0.2);
        }
        body
        {
            font-family: 'Arial' , sans-serif;
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
        .col-md-4
        {
            margin-top: 100px;
            display: flex;
            justify-content: center;
            align-items: top;
        }
        i.fa
        {
            display: inline-block;
            border-radius: 82px;
            box-shadow: 0 0 2px #888;
            padding: 0.5em 0.6em;
            border: 2px solid #37C1BB;
        }
        .card
        {
            border-left: 2px solid #37C1BB;
            height: 115px;
            width: 500px;
            border-radius: 21px;
            width: 270px;
        }
        span
        {
            display: flex;
            justify-content: center;
            align-items: center;
        }
        h3
        {
            text-decoration: underline;
            text-underline-offset: 8px;
            text-decoration-color: #37C1BB;
            font-size: 23px;
        }
        h5
        {
            margin-top: 29px;
            color: Grey;
            font-size: 15px;
        }
        
        .container
        {
            position: relative;
            padding-top: 27px;
        }
        
        .container .progress-bar
        {
           
        }
        .flex-container
        {
            display: flex;
            justify-content: space-between;
        }
        
        .right-column
        {
            flex: 1; /* Takes up remaining space */
        }
        .right-column-item
        {
        }
        
        .top-date
        {
            display: block;
            font-weight: bold;
            font-size: 20px;
            margin-top: 0; /* Reset top margin */
        }
        .lbldt
        {
            top: -56px;
        }
        .row
        {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            margin-right: -28px;
            margin-left: -3px;</style>
</head>
<body>
    <form id="formJobProfile" runat="server" method="get">
    <div class="form">
        <div class="formHeader">
            Interview Status
        </div>
        <asp:Panel runat="server" ID="pnlErr" Visible="false" CssClass="errors">
            <asp:BulletedList runat="server" ID="blErrs">
            </asp:BulletedList>
        </asp:Panel>
        <div class="flex-container">
            <div class="right-column row">
                <asp:Repeater ID="rptInterviewstatus" runat="server" OnItemDataBound="rptInterviewstatus_ItemDataBound">
                    <ItemTemplate>
                        <div class="right-column-item">
                            <div runat="server" id="rptheader">
                            </div>
                            <div class="row" runat="server" visible="false" id="divOffer">
                                <div class="col-md-4 lbldt">
                                    <asp:Label runat="server" ID="lblOfferDt" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2 container">
                                    <i class="fa fa-thumbs-up fa-2x icon" runat="server" id="iOffer" aria-hidden="true">
                                    </i>
                                    <div class="progress-bar" runat="server" id="divOfferProgressbar">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card" runat="server" id="divOfferCard">
                                        <div class="card-body">
                                            <h3>
                                                Offer & BGC</h3>
                                            <h5>
                                                BGC Scheduled</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2" runat="server" id="divInterview">
                                <div class="col-md-4 lbldt">
                                    <asp:Label runat="server" ID="lblInterviewDt" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2 container">
                                    <i class="fa fa-comments-o fa-2x icon" runat="server" id="iInterview" aria-hidden="true">
                                    </i>
                                    <div class="progress-bar" runat="server" id="divInterviewProgressbar">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card " runat="server" id="divInterviewCard">
                                        <div class="card-body">
                                            <h3>
                                                Interview</h3>
                                            <h5>
                                                Evaluated</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2" runat="server" id="divDateTime">
                                <div class="col-md-4 lbldt">
                                    <asp:Label runat="server" ID="lblInterviewconductDt" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2 container">
                                    <i class="fa fa-clock-o fa-2x icon" runat="server" id="iDateTime" aria-hidden="true">
                                    </i>
                                    <div class="progress-bar" runat="server" id="divDateTimeProgressbar">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card " runat="server" id="divDateTimecard">
                                        <div class="card-body">
                                           <h3>
                                                Interview  On</h3>
                                            <h5>
                                                <asp:Label runat="server" ID="lblAppoitmentTime" Text=""></asp:Label></h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2" runat="server" id="divProfile">
                                <div class="col-md-4 lbldt">
                                    <asp:Label runat="server" ID="lblProfileDt" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2 container">
                                    <i class="fa fa-user fa-2x icon" runat="server" id="iProfile" aria-hidden="true">
                                    </i>
                                    <div class="progress-bar" runat="server" id="divProfileProgressbar">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card" runat="server" id="divProfileCard">
                                        <div class="card-body">
                                            <h3>
                                                Profile Created</h3>
                                            <h5>
                                                Candidate Registered against Job</h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div class="formFooter">
            </div>
            <div>
                <div class="container mt-4">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="formHeader">
                            </div>
                            <div class="formBody" style="overflow: auto">
                                <div id="Div2" class="formFooter" runat="server" visible="true">
                                    <div class="row">
                                        <div class="col-md-12 text-right">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
