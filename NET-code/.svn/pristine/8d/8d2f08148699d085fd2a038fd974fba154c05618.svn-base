<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="Report_Home.aspx.cs" Inherits="ContractManagement.Report_Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="Styles/CMS.css" rel="Stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
        
        .buttonfont
        {
            font-size:21px;
        }
  

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br />
<br />

<div align="center">
<span style="font-size:20px;">Use the buttons below to generate reports specific to your responsibilities under the Prime Contract</span>
<br />
<br />
<br />
<asp:Panel  GroupingText="Choose One" HorizontalAlign="Left" runat="server" Width="80%" >
    <table align="center" cellpadding="4px" cellspacing="4px">
        <tr>
            <td class="style1"></td>
        </tr>
        <tr>
            <td><asp:Button ID="Btnpc" runat="server"  Text="Contract Ownership"  
                    onclick="Btnpc_Click"  CssClass="buttonfont" height="36px" width="342px" /></td>
            <td style="width:100px;"></td>
            <td><asp:Button ID="BtnReq" runat ="server" Text= "Requirements" 
                     onclick="BtnReq_Click" CssClass="buttonfont" height="36px" width="342px"/></td>
        </tr>
        <tr >
            <td style="height:20px;"></td>
           
        </tr>
        <tr>
            <td> 
                <asp:Button ID="BtnFy" runat="server" Text="Open Deliverables for Current FY" 
                     onclick="BtnFy_Click"   CssClass="buttonfont" Width="342px"/></td> 
            <td style="width:100px;"></td>
            <td><asp:Button ID="BtnCustom" runat="server" Text ="Custom Deliverables Report" 
                    onclick="BtnCustom_Click"   CssClass="buttonfont" height="36px" width="342px"/></td>
        </tr>
        <tr>
            <td style="height:20px;"></td>
        </tr>
        <tr>
             <td colspan="3" align="center"><asp:Button ID="BtnFlowdown" runat="server" text="SubK Flowdown Provisions Only" CssClass="buttonfont" OnClick="BtnFlowdown_Click" /></td>
        </tr>
    
    </table>
    </asp:Panel></div>
</asp:Content>
