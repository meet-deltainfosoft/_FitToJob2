<%@ Page Title="Exam Checking Allotment" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="ExamCheckAllotment.aspx.cs" Inherits="Exams_ExamCheckAllotment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <script src="../jQuery/jquery-1.12.4.js" type="text/javascript"></script>
    <link href="../jQuery/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="../jQuery/jquery-ui.js" type="text/javascript"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Exam Checking Allotment
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblStandard" Text="Department :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlStandard" OnSelectedIndexChanged="ddlStandard_SelectedIndexChanged"
                            Width="200px" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblSubject" runat="server" Text="Designation :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSubject" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged"
                            Width="200px" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTest" runat="server" Text="Test :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlTest" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="ddlTest_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblExamSchedule" runat="server" Text="Exam Schedule :"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlExamSchedule" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:Button runat="server" ID="btnFilter" Text="Get Question List" OnClick="btnFilter_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="formBody">
            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:Button runat="server" ID="btnOK" Text="Save" OnClick="btnOK_Click" />
                    </td>
                </tr>
            </table>
            <asp:Label runat="server" ID="lblCounter"></asp:Label><hr />
            <asp:GridView runat="server" ID="gdvResultDetail" SkinID="Lns" AutoGenerateColumns="False"
                Width="100%" EmptyDataText="No Records Found." OnRowDataBound="gdvResultDetail_RowDataBound">
                <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" Font-Size="Medium" HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateField HeaderText="No">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="5px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" Width="5px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Question">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblQues"></asp:Label>
                            <asp:HyperLink runat="server" ID="hlImageQus" Target="_blank">
                                <asp:Image runat="server" ID="imgqusPics" alt="PDF View" BorderWidth="2px" BorderColor="BurlyWood"
                                    Width="190px" Height="80px" Visible="true" />
                                <asp:LinkButton ID="lnkDownloadQues" runat="server" CausesValidation="False" Visible="false"
                                    Text="Download" OnClientClick="aspnetForm.target ='_blank';" />
                            </asp:HyperLink></ItemTemplate>
                        <ItemStyle VerticalAlign="Top" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee Name">
                        <ItemTemplate>
                            <asp:DropDownList runat="server" ID="ddlUserName" Width="200px">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="formFooter">
            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:Button runat="server" ID="btnDelete" Text="Delete" OnClientClick="return confirm('Do you Want to Delete');"
                            OnClick="btnDelete_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
