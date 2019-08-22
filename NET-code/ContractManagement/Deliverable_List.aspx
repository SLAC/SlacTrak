<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"  StylesheetTheme="CMS_Theme" AutoEventWireup="true" CodeBehind="Deliverable_List.aspx.cs" Inherits="ContractManagement.Deliverable_List" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.8.21.custom.min.js" type="text/javascript"></script>
    <link href="css/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="Styles/CMS.css" rel="Stylesheet" type="text/css" />
     <script type="text/javascript" src="Scripts/Jquery_common.js"></script>	
    <style type="text/css">
        .style1
        {
            width: 113px;
        }
        .style2
        {
            width: 91px;
        }
        .style3
        {
            width: 88px;
        }
        .style4
        {
            width: 74px;
        }
        
   


    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Deliverables List</h2>
    <br />
  <div  id="DivFY" runat="server">
     <asp:ScriptManager ID="SMCB" runat="server"></asp:ScriptManager>
      <asp:Panel ID="PnlFY" runat="server" GroupingText ="To view other FY data, select from the following and click Refresh List">
      <asp:Label ID="LblfYview" runat="server" Text="Default view is current fiscal year" Font-Italic="true" CssClass="formattext"></asp:Label>
       <asp:UpdatePanel ID="UPCB" runat="server">
            <ContentTemplate>
         <asp:CheckBoxList ID="ChkFY" runat="server" 
             RepeatDirection="Horizontal" AutoPostBack="True" ValidationGroup="refresh" 
              ondatabound="ChkFY_DataBound" 
              onselectedindexchanged="ChkFY_SelectedIndexChanged"></asp:CheckBoxList>
              <br />
                <div style="display:inline-table;">
                       Fiscal Quarter: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   
                    
                    <div style="float:right;">
                        <asp:ListBox ID="LstQtr" runat="server" SelectionMode="Multiple" Height="85px" AutoPostBack="True" OnSelectedIndexChanged="LstQtr_SelectedIndexChanged">
                                         <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                         <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                         <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                         <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                         <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                      </asp:ListBox>
                    </div>
                    <br /><asp:Label ID="LblQtrguide" runat="server" Text= "(Hold down the CTRL Key  <br> to select multiple quarters)" CssClass="errlabels"></asp:Label>
                </div>
           </ContentTemplate>
        
        </asp:UpdatePanel>
       <br />
       <asp:Button ID="BtnRefresh" runat="server" Text="Refresh List" 
             onclick="BtnRefresh_Click"  ValidationGroup="refresh"/> <asp:CustomValidator runat="server" ID="CVcbreq" EnableClientScript ="true"
               ClientValidationFunction="ValidateCheckBoxList" OnServerValidate ="CheckBoxRequired_ServerValidate" ErrorMessage="*Please select a FY and click Refresh Data" CssClass="errlabelsbig" ValidationGroup="refresh" ></asp:CustomValidator>
           </asp:Panel>       
                         
             
      </div>
    <div id="DivDeliverableList" runat="server">
        <table>
            <tr id="trSearch" runat="server" visible="true">
                <td> <asp:Label ID="LblSearch" runat="server" Text="Search Deliverables by any keyword:"></asp:Label></td>
                <td></td>
                <td><asp:TextBox ID="txtDeliverable" runat="server"></asp:TextBox></td>
                <td></td>
                <td><asp:Button ID="cmdFind" runat="server" Text ="Search" 
                        onclick="cmdFind_Click"  ClientIDMode="Static"/></td>
                <td>&nbsp;</td>
                <td><asp:button ID="cmdAdd" runat="server" Text="Add a new Deliverable" 
                        onclick="cmdAdd_Click" /></td>
            </tr>
        </table>

        <br />
            <hr />
             
            <asp:Panel id="PnlList" runat="server"  Width="100%">
                <table>
                    <tr><td> </td></tr>
                    <tr><td><asp:Label ID="LblInfo" runat="server" Text="Click on Track Id to open the item - " style="font-size:medium ;"></asp:Label><asp:Label ID="LblSubtitle" runat="server"  Font-Bold="true" Font-Size="Medium" Text="Deliverables"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                                <asp:GridView ID="GVDeli" runat="server"
                                    onpageindexchanging="GVDeli_PageIndexChanging" 
                                     onsorting="GVDeli_Sorting" EmptyDataText="No Deliverables found" 
                                    onrowdatabound="GVDeli_RowDataBound" 
                                    SkinID="gridviewSkin" 
                                    onrowcreated="GVDeli_RowCreated" >
                                    <Columns>
                                        <asp:BoundField DataField="DELIVERABLE_ID" HeaderText="Id"  SortExpression="DELIVERABLE_ID" Visible="false" />
                                        <asp:HyperLinkField DataNavigateUrlFields="DELIVERABLE_ID"  DataNavigateUrlFormatString="Deliverable.aspx?mode=view&id={0}" SortExpression="COMPOSITE_KEY"
                                              HeaderText="Track Id" DataTextField="COMPOSITE_KEY"  HeaderStyle-HorizontalAlign="Left"  ItemStyle-Width="5%"/>
                                        <asp:BoundField  DataField="TYPENAME" HeaderText = "Type" SortExpression="TYPENAME"  HeaderStyle-HorizontalAlign="Left"  ItemStyle-Width="5%"/>
                                        <asp:BoundField DataField="DUE_DATE" HeaderText="Due Date" SortExpression="DUE_DATE" DataFormatString="{0:MM/dd/yyyy}"  HtmlEncode="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Width="5%"/>
                                
                                        <asp:BoundField DataField="CLAUSENUM" HeaderText="Clause" SortExpression="CLAUSENUM" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                                         <asp:BoundField  DataField="REQUIREMENT" HeaderText ="Requirement" SortExpression="REQUIREMENT"  HeaderStyle-HorizontalAlign="Left"  ItemStyle-Width="20%"/>
                                         <asp:BoundField DataField="FREQNAME" HeaderText="Frequency" SortExpression="FREQNAME" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="5%" />
                                         <asp:BoundField  DataField="DESCRIPTION" HeaderText ="Deliverable" SortExpression="DESCRIPTION"  HeaderStyle-HorizontalAlign="Left"  ItemStyle-Width="20%"/>
                                        <asp:BoundField DataField="STATUS_DESC" HeaderText="Status" SortExpression="STATUS_DESC"  HeaderStyle-HorizontalAlign="Left"  ItemStyle-Width="5%"/>
                                        <asp:BoundField DataField="OWNER" HeaderText="Owner" SortExpression="OWNER"  HeaderStyle-HorizontalAlign="Left"  ItemStyle-Width="5%"/>
                                        <asp:BoundField datafield="IS_INFORMATION_ONLY" HeaderText="Info Only"  SortExpression="IS_INFORMATION_ONLY" HeaderStyle-HorizontalAlign="Left"  ItemStyle-Width="5%"/> 
                                        <asp:TemplateField HeaderText="Approvers" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:GridView ID="GVAppr" runat="server" AutoGenerateColumns="false" GridLines="None">
                                                <Columns>
                                                    <asp:BoundField DataField ="EMPLOYEE_NAME" />
                                                 </Columns>
                                            </asp:GridView>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                   
                                        <asp:BoundField DataField= "REQUIREMENT_ID" Visible="false" />                                       
                                    </Columns>
                                   
                                </asp:GridView>                           
                        </td>                    
                    </tr>
                </table>                        
            </asp:Panel>
    
        <asp:HiddenField ID="HdnDesc" runat="server" />
        <asp:HiddenField ID="HdnFilter" runat="server" />
        <asp:HiddenField ID="HdnDrt" runat="server" />
        <asp:HiddenField ID="HdnStat" runat="server" />
        <asp:HiddenField ID="HdnDays" runat="server" />
        <asp:HiddenField ID="HdnmySSO" runat="server" />
        <asp:HiddenField ID="Hdnfylist" runat="server" />
        <asp:HiddenField ID="HdnQtr" runat="server" />
    </div>

     <asp:SqlDataSource ID="SDSFY" runat="server" ConnectionString="<%$ ConnectionStrings:SLAC_WEB %>" 
                                        
             ProviderName="<%$ ConnectionStrings:SLAC_WEB.ProviderName %>"  
             
             SelectCommand="SELECT DISTINCT(FYDUE) FROM VW_CMS_DELIVERABLE_DETAILS ORDER BY FYDUE ASC" 
             ></asp:SqlDataSource>
</asp:Content>
