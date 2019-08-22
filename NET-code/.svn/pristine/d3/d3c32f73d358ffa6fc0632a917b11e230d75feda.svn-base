<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"  StylesheetTheme="CMS_Theme" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ContractManagement.ReportPost" %>
<%@ PreviousPageType VirtualPath="~/ReportCriteria.aspx" %>
<%@ Register Src="~/User Controls/ClauseList.ascx"  TagName="MultiCl" TagPrefix="uc2" %>
<%@ Register Src="~/User Controls/RequirementList.ascx" TagName="MultiReq" TagPrefix = "uc3" %>
<%@ Register Src="~/User Controls/ReqFlowdown.ascx" TagName="MultiReqFD" TagPrefix="uc4" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
    function RefreshPage() {

        window.location.reload();

    }

   
</script>
<div id="DivCl" runat="server" visible="false">
    <uc2:MultiCl id="MultiCl1" runat="server"></uc2:MultiCl>
</div>

<div id="DivReq" runat = "server" visible="false">
    <uc3:MultiReq id="MultiReq1" runat="Server"></uc3:MultiReq>
</div>

<div id="DivReqFD" runat="server" visible="false">
  <uc4:MultiReqFD ID="MultiReqFD1" runat="server" />
</div>
   
    
<div id="DivDeli" runat="server" visible="false">
  <div>
     <table width="100%">
        <tr>
            <td style="width:8%;"><h2> <asp:Label ID="LblSubTitle" runat="Server" Text="Deliverables List"></asp:Label></h2></td>
            <td valign="bottom"><asp:Label ID="LblInfo" runat="server" visible="false"></asp:Label>
            <asp:Label ID="LblFiltertext" runat="server"  Font-Italic="true"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2"><hr /></td>
        </tr>
        <tr id="trCustom" runat="server" visible="false">
            <td>  <a href='ReportCriteria.aspx' id="LnkBack" runat="server">Back to Search</a></td>
        </tr>
        <tr id="trReq" runat="server" visible ="false">
            <td> <asp:LinkButton ID="LkBBack" runat="server"  OnClientClick="javascript:history.back(); return false;">Back</asp:LinkButton></td>
        </tr>
    </table>
</div>
<p></p>
<div>
    
<asp:ImageButton ID="ImgBtnExport" runat="server" 
        ImageUrl="~/Images/ExportToExcel.gif" onclick="ImgBtnExport_Click" />
<br />
   <asp:GridView ID="GVDeli" runat="server" AllowPaging="true" AllowSorting="true" 
                                    PageSize="15"  AutoGenerateColumns="false" 
                                    onpageindexchanging="GVDeli_PageIndexChanging" 
                                     onsorting="GVDeli_Sorting" EmptyDataText="No Deliverables found" 
                                    onrowdatabound="GVDeli_RowDataBound" 
        CellPadding="3" onsorted="GVDeli_Sorted"  >
                                    <Columns>
                                        <asp:BoundField DataField="DELIVERABLE_ID" HeaderText="Id"  SortExpression="DELIVERABLE_ID" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>
                                        <asp:HyperLinkField DataNavigateUrlFields="DELIVERABLE_ID"  DataNavigateUrlFormatString="Deliverable.aspx?mode=view&id={0}" SortExpression="COMPOSITE_KEY"
                                              HeaderText="Track Id" DataTextField="COMPOSITE_KEY"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>
                                        <asp:BoundField  DataField="TYPENAME" HeaderText = "Type" SortExpression="TYPENAME"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>
                                        <asp:BoundField DataField="DUE_DATE" HeaderText="Due Date" SortExpression="DUE_DATE" DataFormatString="{0:MM/dd/yyyy}"  HtmlEncode="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>
                                        <asp:BoundField  DataField="REQUIREMENT" HeaderText ="Requirement" SortExpression="REQUIREMENT"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>
                                        <asp:BoundField DataField="STATUS_DESC" HeaderText="Status" SortExpression="STATUS_DESC"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>
                                        <asp:BoundField DataField="OWNER" HeaderText="Owner" SortExpression="OWNER"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>
                                        <asp:BoundField DataField="DIRECTORATE" HeaderText="Directorate" SortExpression="DIRECTORATE"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>
                                        <asp:BoundField DataField="DEPTNAME" HeaderText="Department" SortExpression="DEPTNAME"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false"/>
                                        <asp:BoundField datafield="IS_INFORMATION_ONLY" HeaderText="Info Only"  SortExpression="IS_INFORMATION_ONLY" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" /> 
                                        <asp:BoundField DataField="DESCRIPTION" Visible="false" />                                           
                                    </Columns>
                                      <HeaderStyle BackColor="Gray" ForeColor="White" Font-Bold="true" Font-Size="Medium" />        
                                      <RowStyle BackColor="LightGray" ForeColor ="Black" />
                                      <AlternatingRowStyle BackColor="White" ForeColor="Black" />
    </asp:GridView>  
    <br />
    <asp:Label ID="SortInformationLabel" runat="server" Font-Size="Small"></asp:Label>
    </div>
    </div>
<asp:HiddenField ID="HdnTrackId" runat="server" />
<asp:HiddenField ID="HdnType" runat="server" />
<asp:HiddenField ID="HdnDesc" runat="server" />
<asp:HiddenField ID="HdnOwner" runat="server" />
<asp:HiddenField ID="HdnDrt" runat="server" />
<asp:HiddenField ID="HdnDrtName" runat="server" />
<asp:HiddenField ID="HdnDept" runat="server" />
<asp:HiddenField ID="HdnDeptName" runat="server" />
<asp:HiddenField ID="HdnFromDuedate" runat="server" />
<asp:HiddenField ID="HdnToDuedate" runat="server" />
<asp:HiddenField ID="HdnFromSubmit" runat="server" />
<asp:HiddenField ID="HdnToSubmit" runat="server" />
<asp:HiddenField ID="HdnStatus" runat="server" />
<asp:HiddenField ID="HdnApprover" runat="server" />
<asp:HiddenField ID="HdnInfo" runat="server" />
<asp:HiddenField ID="HdnNotify" runat="server" />
<asp:HiddenField ID="HdnNotifyDesc" runat="server" />
<asp:HiddenField ID="HdnSubowner" runat="server" />
<asp:HiddenField  ID="HdnTypeName" runat="server" />
<asp:HiddenField ID="HdnStatusName" runat="server" />
<asp:HiddenField ID="HdnReqId" runat="server" />
<asp:HiddenField ID="HdnFY" runat="server" />
<asp:HiddenField ID="HdnPage" runat="server" />
</asp:Content>
