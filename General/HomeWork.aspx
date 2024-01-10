<%@ Page Title="HomeWork" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="HomeWork.aspx.cs" Inherits="General_HomeWork" %>

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
    <script type="text/javascript">


        $(document).ready(function () {

            $("#<%=txtDt.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true });
            $("#<%=txtDt.ClientID%>").attr('readonly', true);

           <%-- //AutoComplete With Postback 
            $("#<%=txtSubject.ClientID%>").autocomplete("../Exams/Subject.ashx?Subject=SubId", { autoFill: true, mustMatch: false, minChars: 1, width: 250 });
            $("#<%=txtSubject.ClientID%>").result(function (evt, data, formatted) {
                if (data != null) {
                    $("#<%=hfSubId.ClientID%>").val(data[1]);
                }
                else {
                    $("#<%=hfSubId.ClientID%>").val("");
                }
            });
            //AutoComplete With Postback--%>

            var _URL = window.URL || window.webkitURL;
            $("#<%=fileUploadQusImg.ClientID %>").change(function (e) {
                var file, img;
                debugger;
                if ((file = this.files[0])) {
                    img = new Image();
                    img.onload = function () {
                        if (this.width > 500) {
                            alert("uploaded image size is big. kindly upload image within 500 pixel width image.");
                            alert("current image size is " + this.width + ". reduce " + (this.width - 500) + " pixel.")
                        }
                    };
                    img.onerror = function () {
                        alert("not a valid file: " + file.type);
                    };
                    img.src = _URL.createObjectURL(file);
                }

                previewFile();
            });
            $("#<%=fileUploadA1Img.ClientID %>").change(function (e) {
                var file, img;
                debugger;
                if ((file = this.files[0])) {
                    img = new Image();
                    img.onload = function () {
                        if (this.width > 100) {
                            alert("uploaded image size is big. kindly upload image within 100 pixel width image.");
                            alert("current image size is " + this.width + ". reduce " + (this.width - 100) + " pixel.")
                        }
                    };
                    img.onerror = function () {
                        alert("not a valid file: " + file.type);
                    };
                    img.src = _URL.createObjectURL(file);
                }

                previewFileAns1();
            });
            $("#<%=fileUploadA2Img.ClientID %>").change(function (e) {
                var file, img;
                debugger;
                if ((file = this.files[0])) {
                    img = new Image();
                    img.onload = function () {
                        if (this.width > 100) {
                            alert("uploaded image size is big. kindly upload image within 100 pixel width image.");
                            alert("current image size is " + this.width + ". reduce " + (this.width - 100) + " pixel.")
                        }
                    };
                    img.onerror = function () {
                        alert("not a valid file: " + file.type);
                    };
                    img.src = _URL.createObjectURL(file);
                }

                previewFileAns2();
            });
            $("#<%=fileUploadA3Img.ClientID %>").change(function (e) {
                var file, img;
                debugger;
                if ((file = this.files[0])) {
                    img = new Image();
                    img.onload = function () {
                        if (this.width > 100) {
                            alert("uploaded image size is big. kindly upload image within 100 pixel width image.");
                            alert("current image size is " + this.width + ". reduce " + (this.width - 100) + " pixel.")
                        }
                    };
                    img.onerror = function () {
                        alert("not a valid file: " + file.type);
                    };
                    img.src = _URL.createObjectURL(file);
                }

                previewFileAns3();
            });
            $("#<%=fileUploadA4Img.ClientID %>").change(function (e) {
                var file, img;
                debugger;
                if ((file = this.files[0])) {
                    img = new Image();
                    img.onload = function () {
                        if (this.width > 100) {
                            alert("uploaded image size is big. kindly upload image within 100 pixel width image.");
                            alert("current image size is " + this.width + ". reduce " + (this.width - 100) + " pixel.")
                        }
                    };
                    img.onerror = function () {
                        alert("not a valid file: " + file.type);
                    };
                    img.src = _URL.createObjectURL(file);
                }

                previewFileAns4();
            });


            (function ($) {
                var defaults;
                var ClickType;
                $.event.fix = (function (originalFix) {
                    return function (event) {
                        event = originalFix.apply(this, arguments);
                        if (event.type.indexOf("copy") === 0 || event.type.indexOf("paste") === 0) {
                            event.clipboardData = event.originalEvent.clipboardData;
                        }
                        return event;
                    };
                })($.event.fix);
                defaults = {
                    callback: $.noop,
                    matchType: /image.*/
                };
                return ($.fn.pasteImageReader = function (options) {
                    if (typeof options === "function") {
                        options = {
                            callback: options
                        };
                    }
                    options = $.extend({}, defaults, options);
                    return this.each(function () {
                        var $this, element;
                        element = this;
                        $this = $(this);
                        return $this.bind("paste", function (event) {
                            var clipboardData, found;
                            found = false;
                            clipboardData = event.clipboardData;
                            return Array.prototype.forEach.call(clipboardData.types, function (type, i) {
                                var file, reader;
                                if (found) {
                                    return;
                                }
                                if (
                                    type.match(options.matchType) ||
                                    clipboardData.items[i].type.match(options.matchType)
                                ) {
                                    file = clipboardData.items[i].getAsFile();
                                    reader = new FileReader();
                                    reader.onload = function (evt) {
                                        return options.callback.call(element, {
                                            dataURL: evt.target.result,
                                            event: evt,
                                            file: file,
                                            name: file.name
                                        });
                                    };
                                    reader.readAsDataURL(file);
                                    snapshoot();
                                    return (found = true);
                                }
                            });
                        });
                    });
                });
            })(jQuery);

            var dataURL, filename;

            $("html").pasteImageReader(function (results) {
                filename = results.filename, dataURL = results.dataURL;
                //alert(ClickType);
                if (ClickType == "que") {
                    $data.text(dataURL);
                }
                else if (ClickType == "A1") {
                    $dataA1.text(dataURL);
                }
                else if (ClickType == "A2") {
                    $dataA2.text(dataURL);
                }
                else if (ClickType == "A3") {
                    $dataA3.text(dataURL);
                }
                else if (ClickType == "A4") {
                    $dataA4.text(dataURL);
                }
                $size.val(results.file.size);
                $type.val(results.file.type);
                var img = document.createElement("img");

                img.src = dataURL;
                var w = img.width;
                var h = img.height;
                $width.val(w);
                $height.val(h);
                return $(".active")

                    .css({

                        backgroundImage: "url(" + dataURL + ")"

                    })

                    .$data({ width: w, height: h });

            });

            var $data, $size, $type, $width, $height;
            $(function () {
                $data = $(".data");
                $size = $(".size");
                $type = $(".type");
                $width = $("#width");
                $height = $("#height");
                $(".que").on("click", function () {
                    ClickType = "que";
                    var $this = $(this);
                    var bi = $this.css("background-image");
                    if (bi != "none") {
                        $data.text(bi.substr(4, bi.length - 6));
                    }

                    $(".active").removeClass("active");
                    $this.addClass("active");

                    $this.toggleClass("contain");

                    $width.val($this.data("width"));
                    $height.val($this.data("height"));
                    if ($this.hasClass("contain")) {
                        $this.css({
                            width: $this.data("width"),
                            height: $this.data("height"),
                            "z-index": "10"
                        });
                    } else {
                        $this.css({ width: "", height: "", "z-index": "" });
                    }
                });
            });

            var $dataA1, $size, $type, $width, $height;
            $(function () {
                $dataA1 = $(".dataA1");
                $size = $(".size");
                $type = $(".type");
                $width = $("#width");
                $height = $("#height");
                $(".A1").on("click", function () {
                    ClickType = "A1";
                    var $this = $(this);
                    var bi = $this.css("background-image");
                    if (bi != "none") {
                        $dataA1.text(bi.substr(4, bi.length - 6));
                    }

                    $(".active").removeClass("active");
                    $this.addClass("active");

                    $this.toggleClass("contain");

                    $width.val($this.data("width"));
                    $height.val($this.data("height"));
                    if ($this.hasClass("contain")) {
                        $this.css({
                            width: $this.data("width"),
                            height: $this.data("height"),
                            "z-index": "10"
                        });
                    } else {
                        $this.css({ width: "", height: "", "z-index": "" });
                    }
                });
            });

            var $dataA2, $size, $type, $width, $height;
            $(function () {
                $dataA2 = $(".dataA2");
                $size = $(".size");
                $type = $(".type");
                $width = $("#width");
                $height = $("#height");
                $(".A2").on("click", function () {
                    ClickType = "A2";
                    var $this = $(this);
                    var bi = $this.css("background-image");
                    if (bi != "none") {
                        $dataA2.text(bi.substr(4, bi.length - 6));
                    }

                    $(".active").removeClass("active");
                    $this.addClass("active");

                    $this.toggleClass("contain");

                    $width.val($this.data("width"));
                    $height.val($this.data("height"));
                    if ($this.hasClass("contain")) {
                        $this.css({
                            width: $this.data("width"),
                            height: $this.data("height"),
                            "z-index": "10"
                        });
                    } else {
                        $this.css({ width: "", height: "", "z-index": "" });
                    }
                });
            });

            var $dataA3, $size, $type, $width, $height;
            $(function () {
                $dataA3 = $(".dataA3");
                $size = $(".size");
                $type = $(".type");
                $width = $("#width");
                $height = $("#height");
                $(".A3").on("click", function () {
                    ClickType = "A3";
                    var $this = $(this);
                    var bi = $this.css("background-image");
                    if (bi != "none") {
                        $dataA3.text(bi.substr(4, bi.length - 6));
                    }

                    $(".active").removeClass("active");
                    $this.addClass("active");

                    $this.toggleClass("contain");

                    $width.val($this.data("width"));
                    $height.val($this.data("height"));
                    if ($this.hasClass("contain")) {
                        $this.css({
                            width: $this.data("width"),
                            height: $this.data("height"),
                            "z-index": "10"
                        });
                    } else {
                        $this.css({ width: "", height: "", "z-index": "" });
                    }
                });
            });

            var $dataA4, $size, $type, $width, $height;
            $(function () {
                $dataA4 = $(".dataA4");
                $size = $(".size");
                $type = $(".type");
                $width = $("#width");
                $height = $("#height");
                $(".A4").on("click", function () {
                    ClickType = "A4";
                    var $this = $(this);
                    var bi = $this.css("background-image");
                    if (bi != "none") {
                        $dataA4.text(bi.substr(4, bi.length - 6));
                    }

                    $(".active").removeClass("active");
                    $this.addClass("active");

                    $this.toggleClass("contain");

                    $width.val($this.data("width"));
                    $height.val($this.data("height"));
                    if ($this.hasClass("contain")) {
                        $this.css({
                            width: $this.data("width"),
                            height: $this.data("height"),
                            "z-index": "10"
                        });
                    } else {
                        $this.css({ width: "", height: "", "z-index": "" });
                    }
                });
            });
        });
    </script>
    <script type="text/javascript">
        function previewFile() {
            var preview = document.querySelector('#<%=imgqusPics.ClientID %>');
            var file = document.querySelector('#<%=fileUploadQusImg.ClientID %>').files[0];
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
        function previewFileAns1() {
            var preview = document.querySelector('#<%=imga1Pics.ClientID %>');
            var file = document.querySelector('#<%=fileUploadA1Img.ClientID %>').files[0];
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
        function previewFileAns2() {
            var preview = document.querySelector('#<%=imga2Pics.ClientID %>');
            var file = document.querySelector('#<%=fileUploadA2Img.ClientID %>').files[0];
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
        function previewFileAns3() {
            var preview = document.querySelector('#<%=imga3Pics.ClientID %>');
            var file = document.querySelector('#<%=fileUploadA3Img.ClientID %>').files[0];
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
        function previewFileAns4() {
            var preview = document.querySelector('#<%=imga4Pics.ClientID %>');
            var file = document.querySelector('#<%=fileUploadA4Img.ClientID %>').files[0];
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
    <style type="text/css">
        .que
        {
            border: solid 1px #aaa;
            min-height: 50px;
            width: 250px;
            margin-top: 1em;
            border-radius: 5px;
            cursor: pointer;
            transition: 300ms all;
            position: relative;
        }
        
        .A1, .A2, .A3, .A4
        {
            border: solid 1px #aaa;
            min-height: 50px;
            width: 100px;
            margin-top: 1em;
            border-radius: 5px;
            cursor: pointer;
            transition: 300ms all;
            position: relative;
        }
        
        .contain
        {
            background-size: cover;
            position: relative;
            z-index: 10;
            top: 0px;
            left: 0px;
        }
        
        textarea
        {
            background-color: white;
        }
        
        .active
        {
            box-shadow: 0px 0px 10px 10px rgba(0,0,255,.4);
        }
        
        *
        {
            outline: medium none !important;
        }
        
        *
        {
            box-sizing: border-box;
        }
        
        .part
        {
            border-bottom: 1px solid black;
            padding-bottom: 1em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            HomeWork
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
            <asp:HiddenField runat="server" ID="hfMySessionHeaderValue" />
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0" cellpadding="5">
                <tr>
                    <td>
                        <asp:Label ID="lblStandardTextListId" runat="server" Text="Department :&lt;em&gt;*&lt;/em&gt;"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStandardTextListId" AutoPostBack="true" OnSelectedIndexChanged="ddlStandardTextListId_SelectedIndexChanged"
                            Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblSubject" Text="Designation:<em>*</em>"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSubId" OnSelectedIndexChanged="ddlSubId_SelectedIndexChanged"
                            Width="200px" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:TextBox runat="server" ID="txtSubject" Width="269px" Height="20px" TabIndex="1"
                            Visible="false"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="hfSubId" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblChapterId" Text="Chapter :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlChapterId" runat="server" Width="200px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlChapterId_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblChapterVideoId" Text="Chapter Video :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlChapterVideoId" runat="server" Width="200px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlChapterVideoId_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Label ID="lblSrNo" runat="server" Text="HomeWork No :<em>*</em>"></asp:Label>
                    </td>
                    <td valign="top">
                        <asp:TextBox ID="txtSrNo" runat="server" Width="100px" TabIndex="2" Height="20px"></asp:TextBox>
                        &nbsp;
                        <asp:HyperLink runat="server" ID="hlTest" Text="View Questions" Visible="false"></asp:HyperLink>
                    </td>
                    <td valign="top">
                        <asp:Label ID="lblDt" runat="server" Text="Date :<em>*</em>"></asp:Label>
                    </td>
                    <td valign="top" colspan="2">
                        <asp:TextBox ID="txtDt" runat="server" Width="100px" TabIndex="3" Height="20px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                        <asp:Label ID="lblHomeWorkType" runat="server" Text="HomeWork Type :<em>*</em>"></asp:Label>
                    </td>
                    <td valign="top" colspan="4">
                        <asp:RadioButton runat="server" ID="rbHomeWorkTypeMCQ" Text="MCQ" GroupName="HomeWorkType"
                            AutoPostBack="true" OnCheckedChanged="rbHomeWorkTypeMCQ_CheckedChanged" />
                        <asp:RadioButton runat="server" ID="rbHomeWorkTypeNONMCQ" Text="Non - MCQ" GroupName="HomeWorkType"
                            AutoPostBack="true" OnCheckedChanged="rbHomeWorkTypeNONMCQ_CheckedChanged" />
                        <asp:RadioButton runat="server" ID="rbHomeWorkTypeFILE" Text="File Upload" GroupName="HomeWorkType"
                            AutoPostBack="true" OnCheckedChanged="rbHomeWorkTypeFILE_CheckedChanged" />
                        <asp:RadioButton runat="server" ID="rbQueTypeWholeQPaper" Text="Whole Question Paper"
                            GroupName="HomeWorkType" AutoPostBack="true" OnCheckedChanged="rbQueTypeWholeQPaper_CheckedChanged" />
                    </td>
                </tr>
                <tr runat="server" id="trAnsSelection">
                    <td valign="top">
                        <asp:Label ID="lblAnsSelection" runat="server" Text="Answer Selection : <em>*</em>"></asp:Label>
                    </td>
                    <td valign="top" colspan="4">
                        <asp:RadioButton runat="server" ID="rbAnsSelectionSingle" Text="Single" GroupName="AnsSelection"
                            Checked="true" OnCheckedChanged="rbAnsSelectionSingle_CheckedChanged" AutoPostBack="true" />
                        <asp:RadioButton runat="server" ID="rbAnsSelectionMulti" Text="Multiple" GroupName="AnsSelection"
                            AutoPostBack="true" OnCheckedChanged="rbAnsSelectionMulti_CheckedChanged" />
                    </td>
                </tr>
                <tr runat="server" id="trQueDataType">
                    <td valign="top">
                        <asp:Label ID="lblHomeWorkDataType" runat="server" Text="HomeWork Data Type : <em>*</em>"></asp:Label>
                    </td>
                    <td valign="top" colspan="4">
                        <asp:RadioButton runat="server" ID="rbHomeWorkDataTypeNum" Text="NUMERIC ONLY" GroupName="HomeWorkDataType" />
                        <asp:RadioButton runat="server" ID="rbHomeWorkDataTypeChar" Text="CHARACTER + NUMARIC ONLY"
                            GroupName="HomeWorkDataType" />
                    </td>
                </tr>
                <tr runat="server" id="trNoOfFileUpload">
                    <td valign="top">
                        <asp:Label ID="lblNoOfFile" runat="server" Text="HomeWork Data Type : <em>*</em>"></asp:Label>
                    </td>
                    <td valign="top" colspan="4">
                        <asp:DropDownList runat="server" ID="ddlNoOfFile">
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr runat="server" id="trQuestion">
                    <td style="border-top: 1px black solid" valign="top">
                        <asp:Label ID="lblHomeWork" runat="server" Text="HomeWork:<em>*</em>"></asp:Label>
                    </td>
                    <td style="border-top: 1px black solid" valign="top" colspan="4">
                        <asp:TextBox ID="txtHomeWork" runat="server" Width="400px" TabIndex="2" Height="20px"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" id="trQuestion1">
                    <td valign="top" colspan="6" align="left">
                        <asp:Label ID="Label1" runat="server" Text="AND" Visible="true" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="trQuestion2">
                    <td valign="top" class="part">
                        <asp:Label ID="lblBabyPics" runat="server" Text="Select HomeWork Image: <span style='color:red;background-color:yellow'>[500 X 100 PIXEL]</span>"
                            Visible="true"></asp:Label>
                        <br />
                        <br />
                        <%--<span style='color:red;background-color:yellow'>[500 X 100 PIXEL]</span>--%>
                        <asp:FileUpload ID="fileUploadQusImg" runat="server" AutoPostBack="true" Visible="true" />
                        <asp:FileUpload ID="fuUploadPDF" runat="server" AutoPostBack="true" Visible="true" />
                        <br />
                        <br />
                        <asp:Label ID="Label6" runat="server" Text="AND" Visible="true" Font-Size="Large"></asp:Label>
                        <br />
                        <br />
                        <%--onchange="previewFile()"--%>
                        <asp:Label ID="Label9" runat="server" Text="Paste Copied HomeWork Image Here" Visible="true"></asp:Label>
                        <div class="row">
                            <div class="que" runat="server" id="HomeWorkTest">
                            </div>
                        </div>
                    </td>
                    <td colspan="4" class="part" valign="top">
                        <div class="span4">
                            <br />
                            <textarea id="textarea1" style="visibility: hidden;" runat="server" class="data"></textarea>
                        </div>
                        <asp:Image runat="server" ID="imgqusPics" alt="" BorderWidth="2px" BorderColor="BurlyWood"
                            Width="500px" Height="100px" Visible="true" />
                    </td>
                    <td colspan="6" runat="server" id="trFramePaper" visible="false">
                        <div style="width: 100%; height: 450px">
                            <iframe runat="server" visible="true" id="iframepdf" style="display: inline-block;
                                width: 100%; height: 400px;"></iframe>
                        </div>
                    </td>
                </tr>
                <tr runat="server" id="trAnsTextBox">
                    <td valign="top">
                        <asp:Label ID="Label16" runat="server">Answer : <em>*</em></asp:Label>
                    </td>
                    <td colspan="4">
                        <asp:TextBox runat="server" ID="txtAns" TextMode="MultiLine" Width="400px"></asp:TextBox>
                    </td>
                </tr>
                <tr runat="server" id="trAns1_1">
                    <td valign="top">
                        <asp:Label ID="lblA1" runat="server">A1:<em>*</em></asp:Label>
                    </td>
                    <td colspan="4" valign="top">
                        <asp:TextBox ID="txtA1" runat="server" Width="400px" TabIndex="3" BackColor="PowderBlue"
                            Height="20px" />
                    </td>
                </tr>
                <tr runat="server" id="trAns1_2">
                    <td valign="top" colspan="6" align="left">
                        <asp:Label ID="Label2" runat="server" Text="OR" Visible="true" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="trAns1_3">
                    <td valign="top" class="part">
                        <asp:Label ID="lblA1Img" runat="server" Text="Select A1 Image: <span style='color:red;background-color:yellow'>[WIDTH 100 PIXEL]</span>"
                            Visible="true"></asp:Label><br />
                        <br />
                        <%--<span style='color:red;background-color:yellow'>[WIDTH 100 PIXEL]</span>--%>
                        <asp:FileUpload ID="fileUploadA1Img" runat="server" AutoPostBack="true" onchange="previewFileAns1()"
                            Visible="true" />
                        <br />
                        <br />
                        <asp:Label ID="Label7" runat="server" Text="OR" Visible="true" Font-Size="Large"></asp:Label>
                        <br />
                        <br />
                        <%--onchange="previewFile()"--%>
                        <asp:Label ID="Label10" runat="server" Text="Paste Copied ANS 1 Image Here" Visible="true"></asp:Label>
                        <div class="span4 row">
                            <div class="A1" runat="server" id="A1Test">
                            </div>
                        </div>
                    </td>
                    <td colspan="4" valign="top" class="part">
                        <div class="span4">
                            <br />
                            <textarea id="textarea2" style="visibility: hidden;" runat="server" class="dataA1"></textarea>
                        </div>
                        <asp:Image runat="server" ID="imga1Pics" alt="" BorderWidth="2px" BorderColor="BurlyWood"
                            Width="100px" Height="50px" Visible="true" />
                    </td>
                </tr>
                <tr runat="server" id="trAns2_1">
                    <td valign="top">
                        <asp:Label ID="lblA2" runat="server">A2:<em>*</em></asp:Label>
                    </td>
                    <td colspan="4" valign="top">
                        <asp:TextBox ID="txtA2" runat="server" Width="400px" TabIndex="4" BackColor="PowderBlue"
                            Height="20px" />
                    </td>
                </tr>
                <tr runat="server" id="trAns2_2">
                    <td valign="top" colspan="6" align="left">
                        <asp:Label ID="Label3" runat="server" Text="OR" Visible="true" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="trAns2_3">
                    <td valign="top" class="part">
                        <asp:Label ID="lblA2Img" runat="server" Text="Select A2 Image: <span style='color:red;background-color:yellow'>[WIDTH 100 PIXEL]</span>"
                            Visible="true"></asp:Label><br />
                        <br />
                        <asp:FileUpload ID="fileUploadA2Img" runat="server" AutoPostBack="true" onchange="previewFileAns2()"
                            Visible="true" />
                        <br />
                        <br />
                        <asp:Label ID="Label8" runat="server" Text="OR" Visible="true" Font-Size="Large"></asp:Label>
                        <br />
                        <br />
                        <%--onchange="previewFile()"--%>
                        <asp:Label ID="Label11" runat="server" Text="Paste Copied ANS 2 Image Here" Visible="true"></asp:Label>
                        <div class="span4 row">
                            <div class="A2" runat="server" id="A2Test">
                            </div>
                        </div>
                    </td>
                    <td colspan="4" valign="top" class="part">
                        <div class="span4">
                            <br />
                            <textarea id="textarea3" style="visibility: hidden;" runat="server" class="dataA2"></textarea>
                        </div>
                        <asp:Image runat="server" ID="imga2Pics" alt="" BorderWidth="2px" BorderColor="BurlyWood"
                            Width="100px" Height="50px" Visible="true" />
                    </td>
                </tr>
                <tr runat="server" id="trAns3_1">
                    <td valign="top">
                        <asp:Label ID="lblA3" runat="server">A3:<em>*</em></asp:Label>
                    </td>
                    <td colspan="4" valign="top">
                        <asp:TextBox ID="txtA3" runat="server" Width="400px" TabIndex="5" BackColor="PowderBlue"
                            Height="20px" />
                    </td>
                </tr>
                <tr runat="server" id="trAns3_2">
                    <td valign="top" colspan="6" align="left">
                        <asp:Label ID="Label4" runat="server" Text="OR" Visible="true" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="trAns3_3">
                    <td valign="top" class="part">
                        <asp:Label ID="lblA3Img" runat="server" Text="Select A3 Image: <span style='color:red;background-color:yellow'>[WIDTH 100 PIXEL]</span>"
                            Visible="true"></asp:Label><br />
                        <br />
                        <asp:FileUpload ID="fileUploadA3Img" runat="server" AutoPostBack="true" onchange="previewFileAns3()"
                            Visible="true" />
                        <br />
                        <br />
                        <asp:Label ID="Label13" runat="server" Text="OR" Visible="true" Font-Size="Large"></asp:Label>
                        <br />
                        <br />
                        <%--onchange="previewFile()"--%>
                        <asp:Label ID="Label12" runat="server" Text="Paste Copied ANS 3 Image Here" Visible="true"></asp:Label>
                        <div class="span4 row">
                            <div class="A3" runat="server" id="A3Test">
                            </div>
                        </div>
                    </td>
                    <td colspan="4" valign="top" class="part">
                        <div class="span4">
                            <br />
                            <textarea id="textarea4" style="visibility: hidden;" runat="server" class="dataA3"></textarea>
                        </div>
                        <asp:Image runat="server" ID="imga3Pics" alt="" BorderWidth="2px" BorderColor="BurlyWood"
                            Width="100px" Height="50px" Visible="true" />
                    </td>
                </tr>
                <tr runat="server" id="trAns4_1">
                    <td valign="top">
                        <asp:Label ID="lblA4" runat="server">A4:<em>*</em></asp:Label>
                    </td>
                    <td colspan="4" valign="top">
                        <asp:TextBox ID="txtA4" runat="server" Width="400px" TabIndex="6" BackColor="PowderBlue"
                            Height="20px" />
                    </td>
                </tr>
                <tr runat="server" id="trAns4_2">
                    <td valign="top" colspan="6" align="left">
                        <asp:Label ID="Label5" runat="server" Text="OR" Visible="true" Font-Size="Large"></asp:Label>
                    </td>
                </tr>
                <tr runat="server" id="trAns4_3">
                    <td valign="top" class="part">
                        <asp:Label ID="lblA4Img" runat="server" Text="Select A4 Image: <span style='color:red;background-color:yellow'>[WIDTH 100 PIXEL]</span>"
                            Visible="true"></asp:Label><br />
                        <br />
                        <asp:FileUpload ID="fileUploadA4Img" runat="server" AutoPostBack="true" onchange="previewFileAns4()"
                            Visible="true" /><br />
                        <br />
                        <asp:Label ID="Label14" runat="server" Text="OR" Visible="true" Font-Size="Large"></asp:Label>
                        <br />
                        <br />
                        <%--onchange="previewFile()"--%>
                        <asp:Label ID="Label15" runat="server" Text="Paste Copied ANS 4 Image Here" Visible="true"></asp:Label>
                        <div class="span4 row">
                            <div class="A4" runat="server" id="A4Test">
                            </div>
                        </div>
                    </td>
                    <td colspan="4" valign="top" class="part">
                        <div class="span4">
                            <br />
                            <textarea id="textarea5" style="visibility: hidden;" runat="server" class="dataA4"></textarea>
                        </div>
                        <asp:Image runat="server" ID="imga4Pics" alt="" BorderWidth="2px" BorderColor="BurlyWood"
                            Width="100px" Height="50px" Visible="true" />
                    </td>
                </tr>
                <tr runat="server" id="trAnsDropdown">
                    <td valign="top">
                        <asp:Label ID="lblAnswer" runat="server">Answer:<em>*</em></asp:Label>
                    </td>
                    <td colspan="4">
                        <asp:DropDownList runat="server" ID="ddlAnswer" TabIndex="7" Width="150px">
                            <asp:ListItem Value="0">&lt;Select&gt;</asp:ListItem>
                            <asp:ListItem Value="1">1 - A</asp:ListItem>
                            <asp:ListItem Value="2">2 - B</asp:ListItem>
                            <asp:ListItem Value="3">3 - C</asp:ListItem>
                            <asp:ListItem Value="4">4 - D</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CheckBoxList runat="server" ID="chklAns" Visible="false">
                            <asp:ListItem Value="1">1 - A</asp:ListItem>
                            <asp:ListItem Value="2">2 - B</asp:ListItem>
                            <asp:ListItem Value="3">3 - C</asp:ListItem>
                            <asp:ListItem Value="4">4 - D</asp:ListItem>
                        </asp:CheckBoxList>
                    </td>
                </tr>
                <tr runat="server" id="trAnsSample1">
                    <td valign="top">
                        <asp:Label ID="lblSampleAns1" runat="server">Answer Sample 1 :</asp:Label>
                    </td>
                    <td colspan="4">
                        <asp:FileUpload runat="server" ID="fuSampleAns1" />
                        <asp:HyperLink runat="server" ID="hlSampleAns1" Text="View Uploaded Photo"></asp:HyperLink>
                    </td>
                </tr>
                <tr runat="server" id="trAnsSample2">
                    <td valign="top">
                        <asp:Label ID="lblSampleAns2" runat="server">Answer Sample 2 :</asp:Label>
                    </td>
                    <td colspan="4">
                        <asp:FileUpload runat="server" ID="fuSampleAns2" />
                        <asp:HyperLink runat="server" ID="hlSampleAns2" Text="View Uploaded Photo"></asp:HyperLink>
                    </td>
                </tr>
                <tr runat="server" id="trAnsSample3">
                    <td valign="top">
                        <asp:Label ID="lblSampleAns3" runat="server">Answer Sample 3 :</asp:Label>
                    </td>
                    <td colspan="4">
                        <asp:FileUpload runat="server" ID="fuSampleAns3" />
                        <asp:HyperLink runat="server" ID="hlSampleAns3" Text="View Uploaded Photo"></asp:HyperLink>
                    </td>
                </tr>
                <tr runat="server" id="trAnsSample4">
                    <td valign="top">
                        <asp:Label ID="lblSampleAns4" runat="server">Answer Sample 4 :</asp:Label>
                    </td>
                    <td colspan="4">
                        <asp:FileUpload runat="server" ID="fuSampleAns4" />
                        <asp:HyperLink runat="server" ID="hlSampleAns4" Text="View Uploaded Photo"></asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 90px" align="left">
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" TabIndex="10" OnClientClick="return confirm('Do you Want to Delete');"
                            Enabled="False" OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 686px" align="right">
                        <asp:Button ID="btnOK" runat="server" Text="OK" OnClick="btnOK_Click" TabIndex="8" />
                        <asp:Button ID="btnOKAndAddNew" runat="server" Text="OK & Add New" OnClick="btnOKAndAddNew_Click"
                            TabIndex="9" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="9" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="row">
            <div class="span12" runat="server" visible="false">
                <br />
                <div class="input-prepend">
                    <span class="add-on">size</span>
                    <input class="span2 size" id="size" type="text" placeholder="size of pasted image">
                </div>
                <div class="input-prepend">
                    <span class="add-on">type</span>
                    <input class="span2 type" id="type" type="text" placeholder="Image type pasted">
                </div>
                <div class="input-prepend">
                    <span class="add-on">width</span>
                    <input class="span2 type" id="width" type="text" placeholder="Width">
                </div>
                <div class="input-prepend">
                    <span class="add-on">height</span>
                    <input class="span2 type" id="height" type="text" placeholder="Height">
                </div>
            </div>
        </div>
    </div>
</asp:Content>
