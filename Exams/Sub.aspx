<%@ Page Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="Sub.aspx.cs" Inherits="Exams_Sub" Title="Subject Entry " %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/jQuery.UI/jquery.ui.core.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var _URL = window.URL || window.webkitURL;
            $("#<%=fileUploadPhoto.ClientID %>").change(function (e) {
                var file, img;
                debugger;

                if ((file = this.files[0])) {
                    img = new Image();
                    img.onload = function () {
                        if (this.width > 500) {
                            alert("uploaded image size is big. kindly upload image within 500 pixel width image.");
                            alert("current image size is " + this.width + ". reduce " + (this.width - 500) + " pixel.")
                            alert(this.width);
                            
                        }
                    };
                    img.onerror = function () {
                        alert("not a valid file: " + file.type);
                    };
                    img.src = _URL.createObjectURL(file);
                }
                previewFile();
            });
        });
    </script>
     <script type="text/javascript">
         function previewFile() {
             var preview = document.querySelector('#<%=imgPhoto.ClientID %>');
            var file = document.querySelector('#<%=fileUploadPhoto.ClientID %>').files[0];
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Subjects
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblStandardTextListId" runat="server" Text="Department :&lt;em&gt;*&lt;/em&gt;"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStandardTextListId">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblName" runat="server" Text="Designation Name  :&lt;em&gt;*&lt;/em&gt;"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtName" Width="390px" TabIndex="1" MaxLength="150"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblRemark" Text="Remark :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtRemark" MaxLength="500" Width="390px" TextMode="MultiLine"
                            TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblIsStudyMaterialAllowed" Text="Is Study Material Allowed:"></asp:Label>
                    </td>
                    <td>
                        <asp:CheckBox runat="server" ID="chkIsStudyMaterialAllowed"
                            TabIndex="3"></asp:CheckBox>
                    </td>
                </tr>
                <tr runat="server" id="trQuestion2">
                    <td valign="top" class="part">
                        <asp:Label ID="lblAddPhoto" runat="server" Text="Add Photo: <span style='color:red;background-color:yellow'>[500 X 500 PIXEL]</span>"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="fileUploadPhoto" runat="server" AutoPostBack="true" Visible="true" />
                    </td>
                    <td class="part" valign="top" runat="server">
                        <asp:Image runat="server" ID="imgPhoto" alt="" BorderWidth="2px" BorderColor="BurlyWood"
                            Width="500px" Height="200px" Visible="true" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 90px" align="left">
                        <asp:Button runat="server" ID="btnDelete" OnClientClick="return confirm('Do you Want to Delete');"
                            Enabled="false" Text="Delete" OnClick="btnDelete_Click" />
                    </td>
                    <td style="width: 686px" align="right">
                        <asp:Button runat="server" ID="btnOK" Text="OK" OnClick="btnOK_Click" TabIndex="8" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
