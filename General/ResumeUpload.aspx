<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true"
    CodeFile="ResumeUpload.aspx.cs" Inherits="General_ResumeUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <script type="text/javascript">
        var j = jQuery.noConflict();
    </script>
    <script type="text/javascript">

        function previewFile() {

            var preview = document.querySelector('#<%=Resume.ClientID %>');
         var file = document.querySelector('#<%=fuResume.ClientID %>').files[0];
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
            Resume Upload
 
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="false">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label ID="lblResume" runat="server" Text="Upload Resume : "></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="fuResume" runat="server" onchange="previewFile()" />
                    </td>
                    <td>
                        <asp:Image ID="Resume" runat="server" Height="158.4px" TabIndex="98" Visible="True"
                            Width="120px" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 90px" align="left">
                        <asp:Button ID="btnUpload" runat="server" Text="Upload Resume" OnClick="btnUpload_Click" />
                    </td>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

