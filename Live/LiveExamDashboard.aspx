<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LiveExamDashboard.aspx.cs"
    Inherits="Live_LiveExamDashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Live Exam Dashboard | Developed by Delta Infosoft Private Limited</title>
    <link href="../css/cloud-admin.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" type="text/css" href="../css/themes/default.css">
    <link rel="stylesheet" type="text/css" href="../css/responsive.css">
    <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="../css/animatecss/animate.min.css" />
    <link rel="stylesheet" type="text/css" href="../js/bootstrap-daterangepicker/daterangepicker-bs3.css" />
    <link rel="stylesheet" type="text/css" href="../js/jquery-todo/css/styles.css" />
    <link rel="stylesheet" type="text/css" href="../js/fullcalendar/fullcalendar.min.css" />
    <link rel="stylesheet" type="text/css" href="../js/gritter/css/jquery.gritter.css" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700' rel='stylesheet'
        type='text/css'>
    <script type="text/javascript">
        function myfunction() {
            removejscssfile("Default.css", "css");

            App.setPage("index");  //Set current page
            App.init(); //Initialise plugins and elements
        }

        function removejscssfile(filename, filetype) {
            var targetelement = (filetype == "js") ? "script" : (filetype == "css") ? "link" : "none" //determine element type to create nodelist from
            var targetattr = (filetype == "js") ? "src" : (filetype == "css") ? "href" : "none" //determine corresponding attribute to test for
            var allsuspects = document.getElementsByTagName(targetelement)
            for (var i = allsuspects.length; i >= 0; i--) { //search backwards within nodelist for matching elements to remove
                if (allsuspects[i] && allsuspects[i].getAttribute(targetattr) != null && allsuspects[i].getAttribute(targetattr).indexOf(filename) != -1)
                    allsuspects[i].parentNode.removeChild(allsuspects[i]) //remove element by calling parentNode.removeChild()
            }
        }
    </script>
</head>
<body onload="myfunction();">
    <form id="form1" runat="server">
    <div class="navbar clearfix" id="header">
        <div class="navbar-brand">
            <a href="../General/Default.aspx">
                <img src="../images/DeltaIPL-T.jpg" alt="Delta Infosoft Logo" class="img-responsive">
            </a>
        </div>
    </div>
    <br />
    <div id="main-content">
        <div class="container">
            <div class="row">
                <div id="content" class="col-lg-12">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="page-header">
                                <ul class="breadcrumb">
                                    <li><i class="fa fa-home"></i><a href="../General/Default.aspx">Home</a> </li>
                                    <li>Live Exam Dashboard</li>
                                </ul>
                                <div class="clearfix">
                                    <h3 class="content-title pull-left">
                                        Live Exam Dashboard</h3>
                                </div>
                                <div class="description">
                                    Overview, Statistics and more</div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
                                <asp:BulletedList ID="blErrs" runat="server">
                                </asp:BulletedList>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="dashbox panel panel-default">
                                <div class="panel-body">
                                    <div class="panel-left blue">
                                        <i class="fa fa-users fa-3x"></i>
                                    </div>
                                    <div class="panel-right">
                                        <div class="number" runat="server" id="lblTotalGenerated">
                                            0</div>
                                        <div class="title">
                                            Total Generated</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="dashbox panel panel-default">
                                <div class="panel-body">
                                    <div class="panel-left blue">
                                        <i class="fa fa-thumb-tack fa-3x"></i>
                                    </div>
                                    <div class="panel-right">
                                        <div class="number" runat="server" id="lblPresent">
                                            0</div>
                                        <div class="title">
                                            Present</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="dashbox panel panel-default">
                                <div class="panel-body">
                                    <div class="panel-left blue">
                                        <i class="fa fa-check fa-3x"></i>
                                    </div>
                                    <div class="panel-right">
                                        <div class="number" runat="server" id="lblFinished">
                                            0</div>
                                        <div class="title">
                                            Finished</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="dashbox panel panel-default">
                                <div class="panel-body">
                                    <div class="panel-left red">
                                        <i class="fa fa-warning fa-3x"></i>
                                    </div>
                                    <div class="panel-right">
                                        <div class="number" runat="server" id="lblAbsent">
                                            0</div>
                                        <div class="title">
                                            Absent</div>
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
    <script type="text/javascript" src="../js/jquery/jquery-2.0.3.min.js"></script>
    <script type="text/javascript" src="../js/jquery-ui-1.10.3.custom/js/jquery-ui-1.10.3.custom.min.js"></script>
    <script type="text/javascript" src="../bootstrap-dist/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="../js/bootstrap-daterangepicker/moment.min.js"></script>
    <script type="text/javascript" src="../js/bootstrap-daterangepicker/daterangepicker.min.js"></script>
    <script type="text/javascript" src="../js/jQuery-slimScroll-1.3.0/jquery.slimscroll.min.js"></script>
    <script type="text/javascript" src="../js/jQuery-slimScroll-1.3.0/jquery.slimscroll.min.js"></script>
    <script type="text/javascript" src="../js/jQuery-slimScroll-1.3.0/slimScrollHorizontal.min.js"></script>
    <script type="text/javascript" src="../js/jQuery-BlockUI/jquery.blockUI.min.js"></script>
    <script type="text/javascript" src="../js/sparklines/jquery.sparkline.min.js"></script>
    <script type="text/javascript" src="../js/jquery-easing/jquery.easing.min.js"></script>
    <script type="text/javascript" src="../js/easypiechart/jquery.easypiechart.min.js"></script>
    <script type="text/javascript" src="../js/flot/jquery.flot.min.js"></script>
    <script type="text/javascript" src="../js/flot/jquery.flot.time.min.js"></script>
    <script type="text/javascript" src="../js/flot/jquery.flot.selection.min.js"></script>
    <script type="text/javascript" src="../js/flot/jquery.flot.resize.min.js"></script>
    <script type="text/javascript" src="../js/flot/jquery.flot.pie.min.js"></script>
    <script type="text/javascript" src="../js/flot/jquery.flot.stack.min.js"></script>
    <script type="text/javascript" src="../js/flot/jquery.flot.crosshair.min.js"></script>
    <script type="text/javascript" src="../js/jquery-todo/js/paddystodolist.js"></script>
    <script type="text/javascript" src="../js/timeago/jquery.timeago.min.js"></script>
    <script type="text/javascript" src="../js/fullcalendar/fullcalendar.min.js"></script>
    <script type="text/javascript" src="../js/jQuery-Cookie/jquery.cookie.min.js"></script>
    <script type="text/javascript" src="../js/gritter/js/jquery.gritter.min.js"></script>
    <script type="text/javascript" src="../js/script.js"></script>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            App.setPage("index");  //Set current page
            App.init(); //Initialise plugins and elements
        });
    </script>
</body>
</html>
