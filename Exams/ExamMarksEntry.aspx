<%@ Page Title="Exam Marks Entry" Language="C#" MasterPageFile="~/Delta_MCQ.master"
    AutoEventWireup="true" CodeFile="ExamMarksEntry.aspx.cs" Inherits="Exams_ExamMarksEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/jQuery.UI/jquery.ui.core.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

        });
        function CalcTotalMarks(txtgdvObtianedMarks, txtgdvMarks1, txtgdvMarks2, txtgdvMarks3, txtgdvMarks4, txtgdvMarks5, txtgdvMarks6) {
            debugger;
            var Marks1 = 0;
            var Marks2 = 0;
            var Marks3 = 0;
            var Marks4 = 0;
            var Marks5 = 0;
            var Marks6 = 0;

            if (txtgdvMarks1.value != "" && txtgdvMarks1.value != null) {
                Marks1 = txtgdvMarks1.value;
            }
            else {
                Marks1 = 0;
            }
            if (txtgdvMarks2.value != "" && txtgdvMarks2.value != null) {
                Marks2 = txtgdvMarks2.value;
            }
            else {
                Marks2 = 0;
            }
            if (txtgdvMarks3.value != "" && txtgdvMarks3.value != null) {
                Marks3 = txtgdvMarks3.value;
            }
            else {
                Marks3 = 0;
            }
            if (txtgdvMarks4.value != "" && txtgdvMarks4.value != null) {
                Marks4 = txtgdvMarks4.value;
            }
            else {
                Marks4 = 0;
            }
            if (txtgdvMarks5.value != "" && txtgdvMarks5.value != null) {
                Marks5 = txtgdvMarks5.value;
            }
            else {
                Marks5 = 0;
            }
            if (txtgdvMarks6.value != "" && txtgdvMarks6.value != null) {
                Marks6 = txtgdvMarks6.value;
            }
            else {
                Marks6 = 0;
            }

            var Total = parseFloat(Marks1) + parseFloat(Marks2) + parseFloat(Marks3) + parseFloat(Marks4) + parseFloat(Marks5) + parseFloat(Marks6);
            txtgdvObtianedMarks.value = Total.toFixed(2);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form">
        <div class="formHeader">
            Exam Marks Entry
        </div>
        <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
            <asp:BulletedList ID="blErrs" runat="server">
            </asp:BulletedList>
        </asp:Panel>
        <div class="formBody">
            <table border="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:Label runat="server" Text="Employee Name : "></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblStudentName" Font-Bold="true" Font-Size="Medium" ForeColor="BlueViolet"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" Text="Mobile No : "></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblMobileNo" Font-Bold="true" Font-Size="Medium" ForeColor="BlueViolet"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" Text="Department : "></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblStandard" Font-Bold="true" Font-Size="Medium" ForeColor="BlueViolet"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" Text="Designation : "></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblSubject" Font-Bold="true" Font-Size="Medium" ForeColor="BlueViolet"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" Text="Test Name : "></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblTest" Bold="true" Font-Size="Medium" ForeColor="BlueViolet"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="formFooter">
        </div>
        <div class="formHeader">
            Exam Marks (List) &nbsp;&nbsp;&nbsp;<asp:Label runat="server" ID="lblRecordStatus"></asp:Label>
        </div>
        <div class="formBody" style="overflow: auto;">
            <asp:GridView runat="server" ID="gdvMarks" SkinID="Lns" AutoGenerateColumns="False" OnRowCommand="gdvMarks_RowCommand"
                EmptyDataText="No Records Found." OnRowDataBound="gdvMarks_RowDataBound" DataKeyNames="RegistrationId,ExamScheduleId,QueId,ImageNameQus">
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
                                <asp:Image runat="server" ID="imgqusPics" alt="" BorderWidth="2px" BorderColor="BurlyWood"
                                    Width="190px" Height="80px" Visible="true" />
                                <asp:LinkButton ID="lnkDownloadQues" runat="server" CausesValidation="False" Visible="false"
                                    Text="Download" OnClientClick="aspnetForm.target ='_blank';" />
                            </asp:HyperLink>
                            <%--<asp:HyperLink runat="server" ID="hldwnld" Text="Download" Target="_blank"></asp:HyperLink>--%>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="300px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Right Answer">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblRightAns"></asp:Label>
                            <asp:HyperLink runat="server" ID="hlImageAnsRight" Target="_blank">
                                <asp:Image runat="server" ID="imgqusPicsRight" alt="" BorderWidth="2px" BorderColor="BurlyWood"
                                    Width="190px" Height="80px" Visible="true" />
                                <asp:LinkButton ID="lnkDownloadRightAns" runat="server" CausesValidation="False" Visible="false"
                                    Text="Download" OnClientClick="aspnetForm.target ='_blank';" />
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="300px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Attempted Answer">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblAttemptedAns"></asp:Label>
                            <asp:HyperLink runat="server" ID="hlImageAnsAttempted" Target="_blank">
                                <asp:Image runat="server" ID="imgqusPicsAttempted" alt="" BorderWidth="2px" BorderColor="BurlyWood"
                                    Width="190px" Height="80px" Visible="true" />
                                <asp:LinkButton ID="lnkDownloadAttemptedAns" runat="server" CausesValidation="False" Visible="false"
                                    Text="Download" OnClientClick="aspnetForm.target ='_blank';" />
                            </asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="300px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="RightMarks" HeaderText="Right Marks">
                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="WrongMarks" HeaderText="Wrong Marks">
                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NonMarks" HeaderText="Skipped Marks">
                        <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="S1 Q1">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtgdvMarks1" Style="text-align: Right" Width="60px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="50px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="S1 Q2">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtgdvMarks2" Style="text-align: Right" Width="60px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="50px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="S1 Q3">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtgdvMarks3" Style="text-align: Right" Width="60px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="50px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="S2 Q1">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtgdvMarks4" Style="text-align: Right" Width="60px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="50px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="S2 Q2">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtgdvMarks5" Style="text-align: Right" Width="60px"> </asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="50px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="S2 Q3">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtgdvMarks6" Style="text-align: Right" Width="60px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="50px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Obtained Marks">
                        <ItemTemplate>
                            <asp:TextBox runat="server" ID="txtgdvObtianedMarks" Style="text-align: Right" Width="60px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Width="50px" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td style="width: 686px" align="right">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnSaveAndNext" runat="server" Text="Save & Next" OnClick="btnSaveAndNext_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
