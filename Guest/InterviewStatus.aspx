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
            border: 2px solid #119d97;
            background-color: #ccf5f3;
        }
        .card
        {
            border-left: 3px solid #119d97;
            height: 78px;
            width: 500px;
            border-radius: 21px;
            width: 247px;
            background-color: #ccf5f3;
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
            text-decoration-color: #119d97;
            font-size: 23px;
            text-align: center;
        }
        h5
        {
            margin-top: 21px;
            color: Grey;
            font-size: 15px;
        }
        
        .container
        {
            position: relative;
            padding-top: 16px;
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
        .card-body
        {
            -webkit-box-flex: 1;
            -ms-flex: 1 1 auto;
            flex: 1 1 auto;
            padding: 0.25rem;
        }
        .mt-2, .my-2
        {
            margin-top: -1rem !important;
        }
        .row
        {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            margin-right: -28px;
            margin-left: -3px;
        }
    </style>
    <style type="text/css">
        .cardsuccess
        {
            border-left: 3px solid #119d97;
            height: 78px;
            width: 500px;
            border-radius: 21px;
            width: 247px;
            background-color: #d7eedc;
        }
        .cardfailure
        {
            border-left: 3px solid #119d97;
            height: 78px;
            width: 500px;
            border-radius: 21px;
            width: 247px;
            background-color: #f1aeb5;
        }
    </style>
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
                            <div class="row" runat="server" visible="false" id="divFianlJoining">
                                <div class="col-md-4 lbldt">
                                    <asp:Label runat="server" ID="lblFianlJoiningDate" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2 container">
                                    <i class="fa fa-thumbs-up fa-2x icon" runat="server" id="iFianlJoining" aria-hidden="true">
                                    </i>
                                    <div class="progress-bar" runat="server" id="div15">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card" runat="server" id="div16">
                                        <div class="card-body">
                                            <h3>
                                                Final Joining</h3>
                                            <h5>
                                                <asp:Label ID="lblFianlJoining" runat="server" Text=""></asp:Label></h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" visible="false" id="divAppoitmentLatter">
                                <div class="col-md-4 lbldt">
                                    <asp:Label runat="server" ID="lblAppoitmentLatterDate" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2 container">
                                    <i class="fa fa-thumbs-up fa-2x icon" runat="server" id="iAppoitmentLatter" aria-hidden="true">
                                    </i>
                                    <div class="progress-bar" runat="server" id="div13">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card" runat="server" id="div14">
                                        <div class="card-body">
                                            <h3>
                                                Appoitment Latter</h3>
                                            <h5>
                                                <asp:Label ID="lblAppoitmentLatter" runat="server" Text=""></asp:Label></h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" visible="false" id="divOfferLatter">
                                <div class="col-md-4 lbldt">
                                    <asp:Label runat="server" ID="lblOfferLatterDate" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2 container">
                                    <i class="fa fa-thumbs-up fa-2x icon" runat="server" id="iOfferLatter" aria-hidden="true">
                                    </i>
                                    <div class="progress-bar" runat="server" id="div11">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card" runat="server" id="div12">
                                        <div class="card-body">
                                            <h3>
                                                Offer Latter</h3>
                                            <h5>
                                                <asp:Label ID="lblOfferLatter" runat="server" Text=""></asp:Label></h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" visible="false" id="divHODAssessment">
                                <div class="col-md-4 lbldt">
                                    <asp:Label runat="server" ID="lblHODAssessmentDate" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2 container">
                                    <i class="fa fa-thumbs-up fa-2x icon" runat="server" id="iHODAssessment" aria-hidden="true">
                                    </i>
                                    <div class="progress-bar" runat="server" id="div9">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card" runat="server" id="divSubHODAssessment">
                                        <div class="card-body">
                                            <h3>
                                                HOD Assessment</h3>
                                            <h5>
                                                <asp:Label ID="lblHODAssessment" runat="server" Text=""></asp:Label></h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" visible="false" id="divHRAssessment">
                                <div class="col-md-4 lbldt">
                                    <asp:Label runat="server" ID="lblHRAssessmentDate" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2 container">
                                    <i class="fa fa-thumbs-up fa-2x icon" runat="server" id="iHRAssessment" aria-hidden="true">
                                    </i>
                                    <div class="progress-bar" runat="server" id="div7">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card" runat="server" id="divsubHRAssessment">
                                        <div class="card-body">
                                            <h3>
                                                HR Assessment</h3>
                                            <h5>
                                                <asp:Label ID="lblHRAssessment" runat="server" Text=""></asp:Label></h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" visible="false" id="divInterViewOn">
                                <div class="col-md-4 lbldt">
                                    <asp:Label runat="server" ID="labelInterViewOnDate" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2 container">
                                    <i class="fa fa-thumbs-up fa-2x icon" runat="server" id="iInterViewOn" aria-hidden="true">
                                    </i>
                                    <div class="progress-bar" runat="server">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card" runat="server" id="divSubInterViewOn">
                                        <div class="card-body">
                                            <h3>
                                                Interview On</h3>
                                            <h5>
                                                <asp:Label ID="lblInterViewOn" runat="server" Text=""></asp:Label></h5>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row" runat="server" visible="false" id="divProfileShortList">
                                <div class="col-md-4 lbldt">
                                    <asp:Label runat="server" ID="lblProfileShortListDate" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2 container">
                                    <i class="fa fa-thumbs-up fa-2x icon" runat="server" id="iProfileShortList" aria-hidden="true">
                                    </i>
                                    <div class="progress-bar" runat="server" id="div3">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card" runat="server" id="divProfileShortSubList">
                                        <div class="card-body">
                                            <h3>
                                                Profile ShortList</h3>
                                            <h5>
                                                <asp:Label ID="lblProfileShortList" runat="server" Text=""></asp:Label></h5>
                                        </div>
                                    </div>
                            </div>
                            </div>
                            <div class="row" runat="server" visible="false" id="divProfileCreated">
                                <div class="col-md-4 lbldt">
                                    <asp:Label runat="server" ID="lblProfileCreatedDate" Text=""></asp:Label>
                                </div>
                                <div class="col-md-2 container">
                                    <i class="fa fa-thumbs-up fa-2x icon" runat="server" id="iProfileCreated" aria-hidden="true">
                                    </i>
                                    <div class="progress-bar" runat="server" id="divOfferProgressbar">
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="card" runat="server" id="divSubOfferCard">
                                        <div class="card-body">
                                            <h3>
                                                Profile Created</h3>
                                            <%-- <h5>
                                                BGC Scheduled</h5>--%>
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
