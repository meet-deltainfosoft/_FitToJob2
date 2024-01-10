<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="ExamEvaluation.aspx.cs" Inherits="Exams_ExamEvaluation" Title="Exam Evaluation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/jQuery.UI/jquery.ui.core.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
    <script type="text/javascript">
        window.onload = function () {
            // Definitions
            var canvas = document.getElementById("<%=paintcanvas.ClientID%>");
            var context = canvas.getContext("2d");
            var boundings = canvas.getBoundingClientRect();

            // Specifications
            var mouseX = 0;
            var mouseY = 0;
            context.strokeStyle = '#f00000'; // initial brush color
            context.lineWidth = 1; // initial brush width
            var isDrawing = false;

            var points = [];

            // Handle Colors
            var colors = document.getElementsByClassName('colors')[0];

            colors.addEventListener('click', function (event) {
                context.strokeStyle = event.target.value || '#f00000';
                saveImage();
            });

            // Handle Brushes
            var brushes = document.getElementsByClassName('brushes')[0];

            brushes.addEventListener('click', function (event) {
                context.lineWidth = event.target.value || 1;
                saveImage();
            });

            // Mouse Down Event
            canvas.addEventListener('mousedown', function (event) {
                setMouseCoordinates(event);
                isDrawing = true;

                // Start Drawing
                context.beginPath();
                context.moveTo(mouseX, mouseY);

                points.push({
                    x: mouseX,
                    y: mouseY,
                    size: context.lineWidth,
                    color: context.strokeStyle,
                    mode: "begin"
                });


                saveImage();
            });

            // Mouse Move Event
            canvas.addEventListener('mousemove', function (event) {
                setMouseCoordinates(event);

                if (isDrawing) {
                    context.lineTo(mouseX, mouseY);
                    context.stroke();

                    points.push({
                        x: mouseX,
                        y: mouseY,
                        size: context.lineWidth,
                        color: context.strokeStyle,
                        mode: "draw"
                    });
                }

                saveImage();
            });

            // Mouse Up Event
            canvas.addEventListener('mouseup', function (event) {
                setMouseCoordinates(event);
                isDrawing = false;

                points.push({
                    x: mouseX,
                    y: mouseY,
                    size: context.lineWidth,
                    color: context.strokeStyle,
                    mode: "end"
                });

                saveImage();
            });

            // Handle Mouse Coordinates
            function setMouseCoordinates(event) {
                mouseX = event.clientX - boundings.left;
                mouseY = (event.clientY - boundings.top) + $(document).scrollTop();
            }

            // Handle Clear Button
            var clearButton = document.getElementById('clear');

            clearButton.addEventListener('click', function () {
                context.clearRect(0, 0, canvas.width, canvas.height);

                saveImage();
            });


            function redrawAll() {

                if (points.length == 0) {
                    return;
                }

                context.clearRect(0, 0, canvas.width, canvas.height);

                for (var i = 0; i < points.length; i++) {

                    var pt = points[i];

                    var begin = false;

                    if (context.lineWidth != pt.size) {
                        context.lineWidth = pt.size;
                        begin = true;
                    }
                    if (context.strokeStyle != pt.color) {
                        context.strokeStyle = pt.color;
                        begin = true;
                    }
                    if (pt.mode == "begin" || begin) {
                        context.beginPath();
                        context.moveTo(pt.x, pt.y);
                    }
                    context.lineTo(pt.x, pt.y);
                    if (pt.mode == "end" || (i == points.length - 1)) {
                        context.stroke();
                    }
                }
                context.stroke();
            }

            function undoLast() {
                points.pop();
                redrawAll();
            }

            var interval;
            $("#undo").mousedown(function () {
                interval = setInterval(undoLast, 50);
            }).mouseup(function () {
                clearInterval(interval);
            });
        };



        function saveImage() {

            var image = document.getElementById("<%=paintcanvas.ClientID%>").toDataURL("image/png");

            image = image.replace('data:image/png;base64,', '');

            $("#<%=hfimage.ClientID%>").val(image);

            //            var examid = $("#<%=hfExamId.ClientID%>").val();
            //            var userid = $("#<%=hfUserId.ClientID%>").val();
            //            var photono = $("#<%=lblCurrentImage.ClientID%>").text();

            //            $.ajax({
            //                type: 'POST',
            //                url: 'ExamEvaluation.aspx/UploadImage',
            //                data: '{ "imageData" : "' + image + '", "examid" : "' + examid + '", "userid" : "' + userid + '", "photono" : "' + photono + '" }',
            //                contentType: 'application/json; charset=utf-8',
            //                dataType: 'json',
            //                success: function (msg) {
            //                    alert('Image saved successfully !');
            //                }
            //            });
        }


        function subofMarks() {

            var mark1 = $("#<%=txtMark1.ClientID%>").val();
            var mark2 = $("#<%=txtMark2.ClientID%>").val();
            var mark3 = $("#<%=txtMark3.ClientID%>").val();
            var mark4 = $("#<%=txtMark4.ClientID%>").val();
            var mark5 = $("#<%=txtMark5.ClientID%>").val();
            var mark6 = $("#<%=txtMark6.ClientID%>").val();
            var mark7 = $("#<%=txtMark7.ClientID%>").val();
            var mark8 = $("#<%=txtMark8.ClientID%>").val();
            var mark9 = $("#<%=txtMark9.ClientID%>").val();
            var mark10 = $("#<%=txtMark10.ClientID%>").val();
            var mark11 = $("#<%=txtMark11.ClientID%>").val();
            var mark12 = $("#<%=txtMark12.ClientID%>").val();
            var mark13 = $("#<%=txtMark13.ClientID%>").val();
            var mark14 = $("#<%=txtMark14.ClientID%>").val();
            var mark15 = $("#<%=txtMark15.ClientID%>").val();

            if (mark1.trim().length == 0) {
                mark1 = 0;
            }
            if (mark2.trim().length == 0) {
                mark2 = 0;
            }
            if (mark3.trim().length == 0) {
                mark3 = 0;
            }
            if (mark4.trim().length == 0) {
                mark4 = 0;
            }
            if (mark5.trim().length == 0) {
                mark5 = 0;
            }
            if (mark6.trim().length == 0) {
                mark6 = 0;
            }
            if (mark7.trim().length == 0) {
                mark7 = 0;
            }
            if (mark8.trim().length == 0) {
                mark8 = 0;
            }
            if (mark9.trim().length == 0) {
                mark9 = 0;
            }
            if (mark10.trim().length == 0) {
                mark10 = 0;
            }
            if (mark11.trim().length == 0) {
                mark11 = 0;
            }
            if (mark12.trim().length == 0) {
                mark12 = 0;
            }
            if (mark13.trim().length == 0) {
                mark13 = 0;
            }
            if (mark14.trim().length == 0) {
                mark14 = 0;
            }
            if (mark15.trim().length == 0) {
                mark15 = 0;
            }


            var totalMark = parseFloat(mark1) + parseFloat(mark2) + parseFloat(mark3) + parseFloat(mark4) + parseFloat(mark5) + parseFloat(mark6) + parseFloat(mark7) + parseFloat(mark8) + parseFloat(mark9) + parseFloat(mark10) + parseFloat(mark11) + parseFloat(mark12) + parseFloat(mark13) + parseFloat(mark14) + parseFloat(mark15);

            $("#<%=lblTotalMark.ClientID%>").text(totalMark);
            $("#<%=hfTotalMarks.ClientID%>").val(totalMark);
        }

    </script>
    <style type="text/css">
    
        * {
            box-sizing: border-box;
        }

        main {
            width: 800px;
            border: 1px solid #e0e0e0;            
            display: flex;
            flex-grow: 1;
        }

        .left-block {
            width: 160px;
            border-right: 1px solid #e0e0e0;
        }

        .colors {
            background-color: #ece8e8;
            text-align: center;
            padding-bottom: 5px;
            padding-top: 10px;
        }

        .colors button {
            display: inline-block;
            border: 1px solid #00000026;
            border-radius: 0;
            outline: none;
            cursor: pointer;
            width: 20px;
            height: 20px;
            margin-bottom: 5px
        }

        .colors button:nth-of-type(1) {
            background-color: #0000ff;
        }

        .colors button:nth-of-type(2) {
            background-color: #009fff;
        }

        .colors button:nth-of-type(3) {
            background-color: #0fffff;
        }

        .colors button:nth-of-type(4) {
            background-color: #bfffff;
        }

        .colors button:nth-of-type(5) {
            background-color: #000000;
        }

        .colors button:nth-of-type(6) {
            background-color: #333333;
        }

        .colors button:nth-of-type(7) {
            background-color: #666666;
        }

        .colors button:nth-of-type(8) {
            background-color: #999999;
        }

        .colors button:nth-of-type(9) {
            background-color: #ffcc66;
        }

        .colors button:nth-of-type(10) {
            background-color: #ffcc00;
        }

        .colors button:nth-of-type(11) {
            background-color: #ffff00;
        }

        .colors button:nth-of-type(12) {
            background-color: #ffff99;
        }

        .colors button:nth-of-type(13) {
            background-color: #003300;
        }

        .colors button:nth-of-type(14) {
            background-color: #555000;
        }

        .colors button:nth-of-type(15) {
            background-color: #00ff00;
        }

        .colors button:nth-of-type(16) {
            background-color: #99ff99;
        }

        .colors button:nth-of-type(17) {
            background-color: #f00000;
        }

        .colors button:nth-of-type(18) {
            background-color: #ff6600;
        }

        .colors button:nth-of-type(19) {
            background-color: #ff9933;
        }

        .colors button:nth-of-type(20) {
            background-color: #f5deb3;
        }

        .colors button:nth-of-type(21) {
            background-color: #330000;
        }

        .colors button:nth-of-type(22) {
            background-color: #663300;
        }

        .colors button:nth-of-type(23) {
            background-color: #cc6600;
        }

        .colors button:nth-of-type(24) {
            background-color: #deb887;
        }

        .colors button:nth-of-type(25) {
            background-color: #aa0fff;
        }

        .colors button:nth-of-type(26) {
            background-color: #cc66cc;
        }

        .colors button:nth-of-type(27) {
            background-color: #ff66ff;
        }

        .colors button:nth-of-type(28) {
            background-color: #ff99ff;
        }

        .colors button:nth-of-type(29) {
            background-color: #e8c4e8;
        }

        .colors button:nth-of-type(30) {
            background-color: #ffffff;
        }

        .brushes {
            //background-color: purple;
            padding-top: 5px
        }

        .brushes button {
            display: block;
            width: 100%;
            border: 0;
            border-radius: 0;
            background-color: #ece8e8;
            margin-bottom: 5px;
            padding: 5px;
            height: 30px;
            outline: none;
            position: relative;
            cursor: pointer;
        }

        .brushes button:after {
            height: 1px;
            display: block;
            background: #808080;
            content: '';
        }

        .brushes button:nth-of-type(1):after {
            height: 1px;
        }

        .brushes button:nth-of-type(2):after {
            height: 2px;
        }

        .brushes button:nth-of-type(3):after {
            height: 3px;
        }

        .brushes button:nth-of-type(4):after {
            height: 4px;
        }

        .brushes button:nth-of-type(5):after {
            height: 5px;
        }

        .buttons {
            height: 80px;
            padding-top: 10px;
        }

        .buttons button {
            display: block;
            width: 100%;
            border: 0;
            border-radius: 0;
            background-color: #ece8e8;
            margin-bottom: 5px;
            padding: 5px;
            height: 30px;
            outline: none;
            position: relative;
            cursor: pointer;
            font-size: 16px;
        }

        .right-block {
            width: 640px;
        }

        #paintcanvas {
            cursor:crosshair;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Exam Evaluation
            <asp:Label ID="lblTitle" runat="server" Text=" - [EVALUATION MODE]"></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table width="100%">
                <tr>
                    <td>
                        <span>Standard:
                            <asp:Label runat="server" ID="lblStandard" ForeColor="Red" BackColor="Yellow"></asp:Label></span>
                        <span>Subject :
                            <asp:Label runat="server" ID="lblSubject" ForeColor="Red" BackColor="Yellow"></asp:Label></span>
                        <span>Test :
                            <asp:Label runat="server" ID="lblTest" ForeColor="Red" BackColor="Yellow"></asp:Label></span>
                        <span>Exam Schedule :
                            <asp:Label runat="server" ID="lblExamSchedule" ForeColor="Red" BackColor="Yellow"></asp:Label></span>
                        <span>Employee Name :
                            <asp:Label runat="server" ID="lblEmployeeName" ForeColor="Red" BackColor="Yellow"></asp:Label></span>
                        <span>Marks :
                            <asp:Label runat="server" ID="lblObtained" ForeColor="Red" BackColor="Yellow"></asp:Label>/<asp:Label
                                runat="server" ID="lblTotal" ForeColor="Red" BackColor="Yellow"></asp:Label></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Question :</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblQues" ForeColor="Red" BackColor="Yellow" Style="font-size: 14px"></asp:Label>
                        <asp:Image runat="server" ID="imgqusPics" BorderWidth="1px" BorderColor="Gray" Width="500px"
                            Height="100px" Visible="false" />
                        <a runat="server" id="lnkImage" visible="false" title="View File" target="_blank">View
                            File </a>
                        <asp:HiddenField ID="hfQueId" runat="server" />
                        <asp:HiddenField ID="hfExamId" runat="server" />
                        <asp:HiddenField ID="hfRegistrationId" runat="server" />
                        <asp:HiddenField ID="hfExamScheduleId" runat="server" />
                        <asp:HiddenField ID="hfUserId" runat="server" />
                        <asp:HiddenField ID="hfEditMode" runat="server" Value="false" />
                        <asp:HiddenField ID="hfImagePath" runat="server" Value="false" />
                        <asp:HiddenField ID="hfImageNo" runat="server" Value="false" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:PlaceHolder runat="server" ID="plNavigate"></asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span style="font-size: large; background-color: Yellow; color: Red">TOTAL FILE :
                            <asp:Label runat="server" ID="lblCurrentImage" ForeColor="Red" BackColor="Yellow"
                                Font-Size="Large"></asp:Label>&nbsp;OF
                            <asp:Label runat="server" ID="lblTotalImage" ForeColor="Red" BackColor="Lime" Font-Size="Large"></asp:Label></span><br />
                        <asp:Button runat="server" ID="btnPrevious" Text="<< Previous File" OnClick="btnPrevious_Click" />
                        <asp:Button runat="server" ID="btnNext" Text="Next File >>" OnClick="btnNext_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>ENTER SECTION WISE OBTAIN MARKS :</span><asp:Label runat="server" ID="lblTotalMark"
                            Text="0"></asp:Label>
                        <asp:HiddenField runat="server" ID="hfTotalMarks" />
                        <br />
                        <asp:TextBox runat="server" ID="txtMark1" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark2" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark3" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark4" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark5" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark6" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark7" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark8" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark9" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark10" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark11" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark12" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark13" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark14" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtMark15" Width="75px" placeholder="enter marks"
                            Height="20px" onkeyup="javascript:subofMarks();"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <span>Enter Remakrs :</span><br />
                        <asp:TextBox runat="server" ID="txtRemarks" placeholder="enter remarks here" TextMode="MultiLine"
                            Width="600px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button runat="server" ID="btnOK" Text="OK" OnClick="btnOK_Click" TabIndex="8" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
                        <asp:Button runat="server" ID="btnEdit" Visible="false" Text="Edit Marks" OnClick="btnEdit_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button runat="server" ID="btnRotateLeft" Text="Rotate left" OnClick="btnRotateLeft_Click"
                            Visible="false" />
                        <asp:Button runat="server" ID="btnRotateRight" Text="Rotate Right" OnClick="btnRotateRight_Click"
                            Visible="true" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Image ID="Image1" runat="server" />
                    </td>
                </tr>
            </table>
            <table width="100%" runat="server" id="tblCanvas">
                <tr>
                    <td>
                        <main>
                            <div class="left-block">
                                <div class="colors">
                                    <button type="button" value="#0000ff">
                                    </button>
                                    <button type="button" value="#009fff">
                                    </button>
                                    <button type="button" value="#0fffff">
                                    </button>
                                    <button type="button" value="#bfffff">
                                    </button>
                                    <button type="button" value="#000000">
                                    </button>
                                    <button type="button" value="#333333">
                                    </button>
                                    <button type="button" value="#666666">
                                    </button>
                                    <button type="button" value="#999999">
                                    </button>
                                    <button type="button" value="#ffcc66">
                                    </button>
                                    <button type="button" value="#ffcc00">
                                    </button>
                                    <button type="button" value="#ffff00">
                                    </button>
                                    <button type="button" value="#ffff99">
                                    </button>
                                    <button type="button" value="#003300">
                                    </button>
                                    <button type="button" value="#555000">
                                    </button>
                                    <button type="button" value="#00ff00">
                                    </button>
                                    <button type="button" value="#99ff99">
                                    </button>
                                    <button type="button" value="#f00000">
                                    </button>
                                    <button type="button" value="#ff6600">
                                    </button>
                                    <button type="button" value="#ff9933">
                                    </button>
                                    <button type="button" value="#f5deb3">
                                    </button>
                                    <button type="button" value="#330000">
                                    </button>
                                    <button type="button" value="#663300">
                                    </button>
                                    <button type="button" value="#cc6600">
                                    </button>
                                    <button type="button" value="#deb887">
                                    </button>
                                    <button type="button" value="#aa0fff">
                                    </button>
                                    <button type="button" value="#cc66cc">
                                    </button>
                                    <button type="button" value="#ff66ff">
                                    </button>
                                    <button type="button" value="#ff99ff">
                                    </button>
                                    <button type="button" value="#e8c4e8">
                                    </button>
                                    <button type="button" value="#ffffff">
                                    </button>
                                </div>
                                <div class="brushes">
                                    <button type="button" value="1">
                                    </button>
                                    <button type="button" value="2">
                                    </button>
                                    <button type="button" value="3">
                                    </button>
                                    <button type="button" value="4">
                                    </button>
                                    <button type="button" value="5">
                                    </button>
                                </div>
                                <div class="buttons">                               
                                    <button id="clear" type="button">
                                        Clear</button><br />
                                        <button id="undo" type="button">Hold to Undo</button>                                
                                </div>
                            </div>
                            <div class="right-block">
                                <canvas id="paintcanvas" runat="server"></canvas>
                                <asp:HiddenField runat="server" ID="hfimage" />
                            </div>
                        </main>
                    </td>
                </tr>
            </table>
            <table runat="server" id="tableEditMode" visible="false">
                <tr>
                    <td>
                        <label style="background-color: Yellow; color: Red">
                            Image edit is not possible once paper is already checked, you need to delete this
                            checked paper and revalidate it again.
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Image runat="server" ID="imgShow" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0" width="100%">
                <tr>
                    <td align="right">
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_Click"
                            OnClientClick="return confirm('All marks and checking will be removed for this question and student, are you sure want to delete this ?');" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
