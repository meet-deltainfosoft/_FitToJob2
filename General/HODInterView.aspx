<%@ Page Title="" Language="C#" MasterPageFile="~/Delta_MCQ.master" AutoEventWireup="true" CodeFile="HODInterView.aspx.cs" Inherits="General_HODInterView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <link type="text/css" href="../jQuery/jQuery.UI/Datepicker/CSS/redmond/jquery-ui-1.8.1.custom.css"
        rel="Stylesheet" />
    <script type="text/javascript" src="../jQuery/jQuery.UI/Datepicker/JS/jquery.ui.datepicker.js"></script>
    <script type="text/javascript" src="../jQuery/jQuery.UI/jquery.ui.core.js"></script>
    <script type="text/javascript" src="../jQuery/Numeric/jquery.numeric.pack.js"></script>
    <script type="text/javascript" src="../jQuery/Autocomplete/jquery.autocomplete.pack.js"></script>
    <link type="text/css" href="../jQuery/Autocomplete/jquery.autocomplete.css" rel="Stylesheet" />
    <script type="text/javascript" src="../jquery.tablesorter/jquery.tablesorter-update.js"></script>
    <script type="text/javascript">
        var j = jQuery.noConflict();
    </script>
    <script type="text/javascript">
        j(document).ready(function () {
            j("#<%=txtDt.ClientID%>").datepicker({ dateFormat: 'dd-M-yy', changeMonth: true, changeYear: true });
            j("#<%=txtDt.ClientID%>").attr('readonly', true);

            j("#<%=txtCTC.ClientID%>").numeric();
            j("#<%=lblViewMonthlyGrossSalary.ClientID%>").numeric();
            j("#<%=lblViewMonthlyBasic.ClientID%>").numeric();
            j("#<%=lblViewMonthlyHRA.ClientID%>").numeric();
            j("#<%=txtConveyance.ClientID%>").numeric();
            j("#<%=txtSpecialAllowances.ClientID%>").numeric();
            j("#<%=lblViewMonthlyPFCmpnyShare13Point61Per.ClientID%>").numeric();
            j("#<%=lblViewMonthlyESIEmpShare4Point75Per.ClientID%>").numeric();

            j("#<%=txtUser.ClientID%>").autocomplete("../General/User.ashx", { autoFill: true, mustMatch: false, minChars: 1 });
            j("#<%=txtUser.ClientID%>").result(function (evt, data, formatted) {
                if (data != null) {
                    j("#<%=hfUserId.ClientID%>").val(data[1]);
                    __doPostBack("hfUserId");
                }
                else {
                    j("#<%=hfUserId.ClientID%>").val("");
                    __doPostBack("hfUserId");
                }
            });

            //            var IsWarReq = j("#<%=rdbSelected.ClientID%>").attr('checked') ? true : false;
            //            if (IsWarReq == true) {
            //                j("#<%=divsalary.ClientID%>").show();
            //            }
            //            else {
            //                j("#<%=divsalary.ClientID%>").hide();
            //            }
        });
        function CheckSelected() {
            var IsWarReq = j("#<%=rdbSelected.ClientID%>").attr('checked') ? true : false;

            if (IsWarReq == true) {
                j("#<%=divsalary.ClientID%>").show();
            }
            else {
                j("#<%=divsalary.ClientID%>").hide();
            }
        }
        function validateFloatKeyPress(el, evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var number = el.value.split('.');
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            //just one dot
            if (number.length > 1 && charCode == 46) {
                return false;
            }
            //get the carat position
            var caratPos = getSelectionStart(el);
            var dotPos = el.value.indexOf(".");
            if (caratPos > dotPos && dotPos > -1 && (number[1].length > 1)) {
                return false;
            }
            return true;
        }
        function getSelectionStart(o) {
            if (o.createTextRange) {
                var r = document.selection.createRange().duplicate()
                r.moveEnd('character', o.value.length)
                if (r.text == '') return o.value.length
                return o.value.lastIndexOf(r.text)
            } else return o.selectionStart
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="form">
        <div class="formHeader">
           HOD Interview Detail
            <asp:Label ID="lblTitle" runat="server" Text=" - [New Mode]"></asp:Label>
        </div>
        <div class="form-body">
            <asp:Panel ID="pnlErr" CssClass="errors" runat="server" Visible="False">
                <asp:BulletedList ID="blErrs" runat="server">
                </asp:BulletedList>
            </asp:Panel>
            <table>
                <tr>
                    <td width="120px">
                        <asp:Label ID="lblName" runat="server">Candidate Name:<em>*</em></asp:Label>
                    </td>
                    <td width="228px" colspan="3">
                        <asp:TextBox ID="txtName" runat="server" TabIndex="1" Width="350px">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="120px">
                        <asp:Label ID="lblUserId" runat="server">Interview BY:<em>*</em></asp:Label>
                    </td>
                    <td width="228px" colspan="3">
                        <asp:TextBox ID="txtUser" runat="server" TabIndex="2" Width="350px">
                        </asp:TextBox>
                        <asp:HiddenField runat="server" ID="hfUserId" />
                    </td>
                </tr>
                <tr>
                    <td width="120px">
                        <asp:Label ID="lblRemarks" runat="server">Remarks:</asp:Label>
                    </td>
                    <td width="228px" colspan="3">
                        <asp:TextBox ID="txtRemarks" runat="server" TabIndex="3" Width="350px" TextMode="MultiLine">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="120px">
                        <asp:Label ID="lblStatus" runat="server" Text="Status :<em>*</em>"></asp:Label>
                    </td>
                    <td width="228px" colspan="3">
                        <asp:RadioButton runat="server" ID="rdbSelected" Text="Selected" GroupName="Status"
                             />
                        <asp:RadioButton runat="server" ID="rdbHold" Text="Hold" GroupName="Status"  />
                        <asp:RadioButton runat="server" ID="rdbRejected" Text="Rejected" GroupName="Status"
                             />
                    </td>
                </tr>
                <tr>
                    <td width="120px">
                        <asp:Label ID="lblDt" runat="server" Text="Date :<em>*</em>"></asp:Label>
                    </td>
                    <td width="228px" colspan="3">
                        <asp:TextBox ID="txtDt" runat="server" TabIndex="5" Width="84px">
                        </asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <div class="form" runat="server" id="divsalary" style="display:none">
            <div class="formHeader">
                Salary Calculation
            </div>
            <div class="form-body">
                <table border="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 80%" valign="top">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblCTC" Text="CTC:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCTC" Style="text-align: right" Width="100px" AutoPostBack="true"
                                            OnTextChanged="txtCTC_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblSalaryCalculation" Text="Salary Calculation Method:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList runat="server" ID="ddlSalaryCalculation" Width="200px" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSalaryCalculation_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblMonthlyGrossSalary" Text="Monthly Gross Salary:"></asp:Label>
                                    </td>
                                    <td colspan="3">
                                        <asp:TextBox runat="server" ID="lblViewMonthlyGrossSalary" ForeColor="Maroon" Style="text-align: right"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblMonthlyBasic" Text="Monthly Basic:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="lblViewMonthlyBasic" ForeColor="Maroon" Style="text-align: right"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label runat="server" ID="lblMonthlyHRA" Text="Monthly HRA:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="lblViewMonthlyHRA" ForeColor="Maroon" Style="text-align: right"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="lblConveyance" Text="Conveyance:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtConveyance" AutoPostBack="true" Style="text-align: right"
                                            TabIndex="88" OnTextChanged="txtConveyance_TextChanged"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label runat="server" ID="lblSpecialAllowances" Text="Special Allowances"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtSpecialAllowances" TabIndex="88" Style="text-align: right"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label runat="server" ID="Label16" Text="Monthly PF Employer Share:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="lblViewMonthlyPFCmpnyShare13Point61Per" ForeColor="Maroon"
                                            Style="text-align: right"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label runat="server" ID="Label18" Text="Monthly ESIC Employer Share:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="lblViewMonthlyESIEmpShare4Point75Per" ForeColor="Maroon"
                                            Style="text-align: right"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20%" valign="top">
                            <div class="form">
                                <div class="formBody" style="overflow: auto">
                                    <table border="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblIsDeductESI" Text="Deduct ESI :  ">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chkIsDeductESI" AutoPostBack="true" OnCheckedChanged="chkIsDeductESI_CheckedChanged"
                                                    TabIndex="94" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label runat="server" ID="lblIsDeductPF" Text="Deduct PF :  ">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:CheckBox runat="server" ID="chkIsDeductPF" AutoPostBack="true" OnCheckedChanged="chkIsDeductPF_CheckedChanged"
                                                    TabIndex="96" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="formFooter">
            <table border="0" cellspacing="0">
                <tr>
                    <td align="right" width="650">
                        <asp:Button runat="server" ID="btnQueAdd" Text="Question Add" OnClick="btnQueAdd_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:Panel runat="server" ID="pnlLn" Visible="false">
            <div class="form">
                <div class="formHeader">
                    Question Line
                </div>
                <div class="formBody" style="overflow: auto">
                    <asp:GridView ID="gdvLns" runat="server" SkinID="Lns" AutoGenerateColumns="False"
                        OnRowDataBound="gdvLns_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No.">
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                <HeaderStyle BorderColor="#999999" HorizontalAlign="Center" VerticalAlign="Top" Width="100px" />
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber1" Text='<%# Container.DataItemIndex + 1 %>' runat="server"
                                        Width="100px" />
                                    <asp:Label ID="lblRowNumber" Visible="false" Text='<%# Eval("LnNo") %>' runat="server"
                                        Width="100px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Attributes to be assessed">
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" Width="900px" />
                                <HeaderStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Label ID="lblText" Text='<%# Eval("Text") %>' runat="server" Width="500px" Font-Bold="True"></asp:Label>
                                    <asp:Label ID="lblTextListId" Visible="false" Text='<%# Eval("QueTextListId") %>'
                                        runat="server" Font-Bold="True"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Points">
                                <ItemStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" Width="150px" />
                                <HeaderStyle BorderColor="#999999" HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Label ID="lblActualMarks" Text='<%# Eval("ActualMarks") %>' runat="server" Width="150px"
                                        Font-Bold="True"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HR">
                                <ItemStyle BorderColor="#999999" HorizontalAlign="left" VerticalAlign="Top" Width="180px" />
                                <HeaderStyle BorderColor="#999999" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtMarks" Width="150px" Text='<%# Eval("ObtainedMarks") %>'
                                        onkeypress="return validateFloatKeyPress(this,event);" Style="text-align: right;">
                                    </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="HOD">
                                <ItemStyle BorderColor="#999999" HorizontalAlign="left" VerticalAlign="Top" Width="180px" />
                                <HeaderStyle BorderColor="#999999" HorizontalAlign="left" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtHODMarks" Width="150px" Text='<%# Eval("HODObtainedMarks") %>'
                                        onkeypress="return validateFloatKeyPress(this,event);" Style="text-align: right;">
                                    </asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                   <%-- <div class="form-body">
                        <table border="0" cellspacing="0" width="100%">
                            <tr>
                                <td width="120px">
                                    <asp:Label ID="lblStatusPassFail" runat="server" Text="Status :"></asp:Label>
                                </td>
                                <td width="228px" colspan="3">
                                    <asp:RadioButton runat="server" ID="rdbpass" Text="Pass" GroupName="Pass" />
                                    <asp:RadioButton runat="server" ID="rdbfail" Text="Fail" GroupName="Pass" />
                                </td>
                            </tr>
                            <tr>
                                <td width="120px">
                                    <asp:Label ID="lblFailedRemark" runat="server" Text="Failed Remark :"></asp:Label>
                                </td>
                                <td width="228px" colspan="3">
                                    <asp:TextBox ID="TextBox1" runat="server" TabIndex="5" Width="84px">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="PnlOk">
            <div class="formFooter">
                <table>
                    <tr>
                        <td>
                            <asp:Button runat="server" ID="btnDelete" CssClass="btn btn-danger" Enabled="false"
                                Text="Delete" OnClientClick="return confirm('Do you Want to Delete');" TabIndex="5"
                                OnClick="btnDelete_Click" Visible="false" />
                        </td>
                        <td width="776" align="right">
                            <asp:CheckBox runat="server" ID="chkPrint" Text="View/Print" />
                            <asp:Button runat="server" ID="btnOK" Text="Submit" CssClass="btn btn-success" OnClick="btnOK_Click"
                                TabIndex="6" />
                            <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger" Text="Cancel"
                                OnClick="btnCancel_Click" TabIndex="7" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
</asp:Content>

