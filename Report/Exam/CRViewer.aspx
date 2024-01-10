<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CRViewer.aspx.cs" Inherits="Report_Exam_CRViewer" %>

<%@ Register assembly="CrystalDecisions.Web, Version=12.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> Report</title>
</head>
<body>
    <form id="form1" class="form" runat="server" >
    <div id="div1" >
    <div class="formHeader">
            Reports
            </div>
        <asp:Panel runat="server" ID="pnlErr" Visible="false" CssClass="errors">
            <asp:BulletedList runat="server" ID="blErrs">
            </asp:BulletedList>
        </asp:Panel>
                <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="true" 
                    HasCrystalLogo="False" HasDrillUpButton="False" HasGotoPageButton="False" 
                    HasSearchButton="False" HasToggleGroupTreeButton="False" 
                    HasToggleParameterPanelButton="False" ToolPanelView="None" />
          
    </div>
    </form>
</body>
</html>
