<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OfferApprove.aspx.cs" Inherits="Guest_OfferApprove" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Offer Approval Page</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css"
        integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"
        crossorigin="anonymous" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css"
        integrity="sha384-GLhlTQ8iKu1r6jpon6n9kiR9+OgUhT9Y3Uk9P1z7JCAF4FpiDkMP7aaNi4+qL2xpw"
        crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.0.8/dist/umd/popper.min.js"
        integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"
        integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous"></script>
    <style type="text/css">
        body
        {
            font-family: 'Arial' , sans-serif;
            background-color: #37C1BB;
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
        
        .form
        {
            background-color: #ffffff;
            border: 1px solid #dee2e6;
            padding: 10px; /*margin: 20px;*/
            border-radius: 5px;
        }
        
        .formHeader
        {
            background-color: #37C1BB;
            font-size: 24px;
            font-weight: bold;
            margin-bottom: 0px;
            color: #ffffff;
        }
        .topcard
        {
            justify-content: center;
            align-items: top;
        }
        h3
        {
            /*text-decoration: underline;*/
            text-underline-offset: 8px;
            text-decoration-color: #37C1BB;
            font-size: 15px;
            font-family: Verdana;
            color: #37C1BB;
        }
        h5
        {
            margin-top: 29px;
            color: black;
            font-size: 16px;
        }
        h6
        {
            margin-top: 29px;
            color: black;
            font-size: 14px;
        }
        /* .card
        {
            height: 130px;
            width: 250px;
        }*/
        .flex-container
        {
            background-color: #ffffff;
        }
        .cardline
        {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap; /*margin-right: -15px;*/ /*margin-left: 162px;*/
        }
        i.fa
        {
            display: inline-block;
            border-radius: 50%;
            box-shadow: 0 0 2px #888;
            padding: 0.3em 0.4em;
            border: 2px solid #37C1BB;
            color: #37C1BB;
        }
        /* .icontact
        {
            height: 140px;
            width: 500px;
        }*/
        .joining
        {
            height: 140px;
            width: 350px;
        }
        .col-sm-3
        {
            width: 90%;
        }
        .col-sm-5
        {
            width: 90%;
        }
        .card-body
        {
            -webkit-box-flex: 1;
            -ms-flex: 1 1 auto;
            flex: 1 1 auto;
            padding: 1.25rem;
            border: 2px solid #37c1bb;
        }
        h3
        {
            font-weight: bold;
        }
        .fontbold
        {
            font-weight: bolder;
        }
        .form
        {
            width: 90% !important;
            margin: 0 auto;
        }
    </style>
</head>
<body>
    <form id="formJobApprove" runat="server">
    <div class="form mt-3   ">
        <div class="formHeader">
            Offer Approve
        </div>
        <asp:Panel runat="server" ID="pnlErr" Visible="false" CssClass="errors">
            <asp:BulletedList runat="server" ID="blErrs">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formbody">
            <div class="flex-container">
                <div runat="server" id="rptheader">
                </div>
                <div class="row ml-1">
                    <div runat="server" visible="true" id="divOffer" class="col-lg-3 col-sm-11 mt-3 ">
                        <div class="card" runat="server" id="divJoiningDtCard">
                            <div class="card-body">
                                <h3>
                                    Joining Date</h3>
                                <h5 id="JoiningDate" runat="server" class="fontbold">
                                    05-01-2024</h5>
                            </div>
                        </div>
                    </div>
                    <div runat="server" id="divReportingTime" class="col-lg-3 col-sm-11 mt-3 ">
                        <div class="card" runat="server" id="divReportingTimeCard">
                            <div class="card-body">
                                <h3>
                                    Reporting Time</h3>
                                <h5 id="ReportingTime" runat="server" class="fontbold">
                                </h5>
                            </div>
                        </div>
                    </div>
                    <div runat="server" id="divRemainingDays" class="col-lg-3 col-sm-11 mt-3">
                        <div class="card" runat="server" id="divRemainingDaysCard">
                            <div class="card-body">
                                <h3>
                                    Days Remaining</h3>
                                <h5 id="DaysRemaining" runat="server" class="fontbold">
                                    00 Days</h5>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-3 col-sm-12 mt-3">
                        <asp:Button runat="server" ID="btnViewOffer" Text="View Offer Latter" CssClass="btn btn-primary" OnClick="btnViewOffer_click" />
                    </div>
                </div>
                <div class="row mt-3 ml-1">
                    <div runat="server" visible="true" id="divDayOneJoining" class="col-lg-3 col-sm-11 mt-2 h-60 ">
                        <div class="card " runat="server" id="divDayOneJoiningCard" style="height: 140px;">
                            <div class="card-body">
                                <h3>
                                    Day One Joining Location</h3>
                                <h5 id="JoiningLocation" runat="server" class="fontbold">
                                    NA</h5>
                            </div>
                        </div>
                    </div>
                    <div runat="server" visible="true" id="divWorkLocation" class="col-lg-3 col-sm-11 mt-2 h-60">
                        <div class="card " runat="server" id="divWorkLocationCard" style="height: 140px;">
                            <div class="card-body">
                                <h3>
                                    Work Location</h3>
                                <h5 id="WorkLocation" runat="server" class="fontbold">
                                    Add Location</h5>
                            </div>
                        </div>
                    </div>
                    <div runat="server" visible="true" id="divJoiningTips" class="col-lg-5 col-sm-11 mt-2 h-60">
                        <div class="card " runat="server" id="divJoiningTipsCard" style="height: 140px;">
                            <div class="card-body">
                                <h3>
                                    Joining Tips</h3>
                                <span class="fontbold">1. You have to join within seven days </span>
                                <br>
                                <span class="fontbold">2. Wait for an email invite before visiting the office</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row ml-1">
                    <div runat="server" visible="true" id="divContact" class="col-lg-5 col-sm-11 mt-4 ">
                        <div class="card icontact" runat="server" id="divContactCard">
                            <div class="card-body">
                                <h3>
                                    Point Of Contact</h3>
                                <div class="row align-items-center">
                                    <div class="col-auto">
                                        <i class="fa fa-user fa-2x icon" runat="server" id="i1" aria-hidden="true"></i>
                                    </div>
                                    <div class="col">
                                        <span class="d-inline-block fontbold" id="hrManager" runat="server">Irshad Mansiya</span>
                                        <span class="d-block mt-0 fontbold" id="hrEmailId" runat="server">irshadmansiya.dukeplasto@gmail.com</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div runat="server" visible="true" id="divContact2" class="col-sm-5 mt-4 invisible">
                        <div class="card icontact" runat="server" id="divContact2Card">
                            <div class="card-body">
                                <h3>
                                    Point Of Contact</h3>
                                <div class="row align-items-center">
                                    <div class="col-auto">
                                        <i class="fa fa-user fa-2x icon" runat="server" id="iContact2" aria-hidden="true">
                                        </i>
                                    </div>
                                    <div class="col">
                                        <span class="d-inline-block ">Vikas Dave</span> <span class="d-block mt-0">vikasdave.dukeplasto@gmail.com</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="mt-4">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
