<%@ Page Title="Exam Result Publish" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="ExamResultPublish.aspx.cs" Inherits="Exams_ExamResultPublish" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <script src="../jQuery/jquery-1.12.4.js" type="text/javascript"></script>
    <link href="../jQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../jQuery/jquery-ui.js" type="text/javascript"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

        });        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Exam Result Publish
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblStandard" Text="Department :<em>*</em>"></asp:Label>
                    </td>
                    <td style="width: 300px">
                        <asp:DropDownList runat="server" ID="ddlStandard" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged"
                            AutoPostBack="true" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblSubject" runat="server" Text="Designation :<em>*</em>"></asp:Label>
                    </td>
                    <td style="width: 300px">
                        <asp:DropDownList runat="server" ID="ddlSubject" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"
                            AutoPostBack="true" Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTest" runat="server" Text="Test :<em>*</em>"></asp:Label>
                    </td>
                    <td style="width: 300px">
                        <asp:DropDownList runat="server" ID="ddlTest" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged"
                            AutoPostBack="true" Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Schedule :<em>*</em>"></asp:Label>
                    </td>
                    <td style="width: 300px">
                        <asp:DropDownList runat="server" ID="ddlSchedule" Width="150px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblAnsKeyFilePath" runat="server" Text="Ans Key File PDF:<em>*</em>"></asp:Label>
                    </td>
                    <td colspan="3">
                        <asp:FileUpload runat="server" ID="fuAnsKeyFilePath" />
                    </td>
                </tr>
                <tr runat="server" id="trShow" visible="false">
                    <td>
                        <iframe runat="server" visible="true" id="iframepdf" style="display: inline-block;
                            width: 100%; height: 400px;"></iframe>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:CheckBox runat="server" ID="chkIsResultPublished" Text="Publish result" />
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
                        <asp:Button runat="server" ID="btnOK" Text="OK" OnClick="btnOK_Click" />
                        <asp:Button runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
