﻿<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ContractManagement._Default"  %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
     <link href="Styles/CMS.css" rel="Stylesheet" type="text/css" />
     <script type="text/javascript" src="Scripts/Jquery_common.js"></script>	
   
   
     <style type="text/css">
         .style1
         {
             width: 2%;
             height: 21px;
         }
         .style2
         {
             height: 21px;
         }
     </style>
  
   
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
 							 
     <div>
        <table style="width:100%;">
            <tr>
                <td> <h2> Deliverables Dashboard</h2></td>
            </tr>
            <tr>
                <td> <hr /></td>
            </tr>
        </table>
     </div>
     
      
     <div>
         
     <div class="Largetext">
    
      <asp:ScriptManager ID="SMCB" runat="server"></asp:ScriptManager>
      <asp:Panel ID="PnlFY" runat="server"  GroupingText ="To view other FY data, select from the following and click Refresh Data" >
      <asp:Label ID="LblfYview" runat="server" Text="Default view is current fiscal year. " Font-Italic="true" CssClass="formattext" ></asp:Label>
      

         <asp:UpdatePanel ID="UPCB" runat="server">
            <ContentTemplate>
            
                 <asp:CheckBoxList ID="ChkFY" runat="server" ondatabound="ChkFY_DataBound" 
             onselectedindexchanged="ChkFY_SelectedIndexChanged" 
             RepeatDirection="Horizontal" AutoPostBack="True" ValidationGroup="refresh"></asp:CheckBoxList>
                <br />
                <div style="display:inline-table;">
                       Fiscal Quarter: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   
                    
                    <div style="float:right;">
                        <asp:ListBox ID="LstQtr" runat="server" SelectionMode="Multiple" Height="90px" AutoPostBack="True" OnSelectedIndexChanged="LstQtr_SelectedIndexChanged" ValidationGroup="refresh" >
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
       <asp:Button ID="BtnRefresh" runat="server" Text="Refresh Data" 
             onclick="BtnRefresh_Click"  ValidationGroup="refresh"/> <asp:CustomValidator runat="server" ID="CVcbreq" EnableClientScript ="true"
               ClientValidationFunction="ValidateCheckBoxList" OnServerValidate ="CheckBoxRequired_ServerValidate" ErrorMessage="*Please select a FY and click Refresh Data" CssClass="errlabelsbig" ValidationGroup="refresh" ></asp:CustomValidator>
           
      </asp:Panel>
     
        
     
     
       
     </div>
     <table width="100%">

      
        <tr style="height:10px;">
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td>
            <div id="DivGeneral" runat="server">
                 <table class="tableborder"  cellpadding="3" cellspacing="2" width="100%">
                        <tr> 
                            <th class="thheaderhori" style="width:23%; background-color: #bfcbd6;"> All Directorates/Status </th>
                            <th class="thheaderhori" style="width:9%;"> New</th>
                            <th class="thheaderhori" style="width:12%;"> In Progress </th>
                            <th class="thheaderhori" style="width:12%;"> Submitted </th>
                            <th class="thheaderhori" style="width:12%;"> Approved </th>
                            <th class="thheaderhori" style="width:12%;"> Re-opened </th>
                            <th class="thheaderhori" style="width:9%;">Overdue</th>
                            <th class="thheaderhori" style="width:12%;" runat="server" id="THAD">Approved <br />by Default</th>
                        </tr>
                        <tr>
                            <td class="thheaderhori"> Accelerator </td>
                            <td class="tdlink"> 
                                <%if (NewDeliA == "0")
                                  { %>
                                    <asp:Label ID="LblNewDeliA" runat="server" Text="0"></asp:Label> <br />
                                <%} %>   <%else { %> 
                                    <asp:LinkButton ID="LnkNewDeliA" runat="server"  OnCommand="LinkButton_Command"  CommandArgument="A" CommandName="1"><%= NewDeliA %></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink">  
                                <%if (InProgDeliA == "0")
                                  { %>
                                    <asp:Label ID="LblInprogDeliA" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkInprogDeliA" runat="server" 
                                     OnCommand="LinkButton_Command"   CommandArgument="A" CommandName="2"><%= InProgDeliA%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                <%if (SubmitDeliA == "0")
                                  { %>
                                    <asp:Label ID="LblSubmitDeliA" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkSubmitDeliA" runat="server" 
                                    OnCommand="LinkButton_Command"   CommandArgument="A" CommandName="3" ><%= SubmitDeliA%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                  <%if (ApprovedDeliA == "0")
                                  { %>
                                    <asp:Label ID="LblApprDeliA" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDeliA" runat="server" 
                                       OnCommand="LinkButton_Command"   CommandArgument="A" CommandName="4" ><%= ApprovedDeliA%></asp:LinkButton>
                                     <%} %>
                             </td>
                            <td class="tdlink"> 
                                 <%if (ReopenDeliA == "0")
                                  { %>
                                    <asp:Label ID="LblReopenDeliA" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkReopenDeliA" runat="server" 
                                      OnCommand="LinkButton_Command"   CommandArgument="A" CommandName="5" ><%=ReopenDeliA%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                 <%if (OverdueDeliA == "0")
                                  { %>
                                    <asp:Label ID="LblOverdueDeliA" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkOverdueDeliA" runat="server" 
                                      OnCommand="LinkButton_Command"   CommandArgument="A" CommandName="0" ><%=OverdueDeliA%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink" runat="server" id="TDA"> 
                                 <%if (ApprDefDeliA == "0")
                                  { %>
                                    <asp:Label ID="LblApprDefDeliA" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDefDeliA" runat="server" 
                                     OnCommand="LinkButton_Command"   CommandArgument="A" CommandName="6" ><%=ApprDefDeliA%></asp:LinkButton>
                                     <%} %>
                            </td>
                        </tr>
                        <tr>
                            <td class="thheaderhori"> Director's Office </td>
                            <td class="tdlink"> 
                                 <%if (NewDeliD == "0")
                                  { %>
                                    <asp:Label ID="LblNewDeliD" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkNewDeliD" runat="server" OnCommand="LinkButton_Command" CommandArgument="D" CommandName="1" ><%= NewDeliD %></asp:LinkButton>
                                     <%} %>
                             </td>
                            <td class="tdlink">
                                 <%if (InProgDeliD == "0")
                                  { %>
                                    <asp:Label ID="LblInprogDeliD" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkInprogDeliD" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="D" CommandName="2" ><%= InProgDeliD%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                 <%if (SubmitDeliD == "0")
                                  { %>
                                    <asp:Label ID="LblSubmitDeliD" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkSubmitDeliD" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="D" CommandName="3" ><%= SubmitDeliD%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                 <%if (ApprovedDeliD == "0")
                                  { %>
                                    <asp:Label ID="LblApprDeliD" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDeliD" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="D" CommandName="4" ><%= ApprovedDeliD%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                 <%if (ReopenDeliD == "0")
                                  { %>
                                    <asp:Label ID="LblReopenDeliD" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkReopenDeliD" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="D" CommandName="5" ><%=ReopenDeliD%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink">
                                 <%if (OverdueDeliD == "0")
                                  { %>
                                    <asp:Label ID="LblOverdueDeliD" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkOvedueDeliD" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="D" CommandName="0" ><%=OverdueDeliD%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink" runat="server"  id="TDD">  
                                  <%if (ApprDefDeliD == "0")
                                  { %>
                                    <asp:Label ID="LblApprDefDeliD" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDefDeliD" runat="server" 
                                      OnCommand="LinkButton_Command" CommandArgument="D" CommandName="6" ><%=ApprDefDeliD%></asp:LinkButton>
                                     <%} %>
                            </td>
                        </tr>
                        <tr id="trIS" runat="server" visible ="false">
                            <td class="thheaderhori"> Infrastructure & Safety 
                           </td>
                            <td class="tdlink"> 
                                 <%if (NewDeliF == "0")
                                  { %>
                                    <asp:Label ID="LblNewDeliF" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkNewDeliF" runat="server" OnCommand="LinkButton_Command"   CommandArgument="F" CommandName="1" ><%= NewDeliF %></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                 <%if (InProgDeliF == "0")
                                  { %>
                                    <asp:Label ID="LblInprogDeliF" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkInprogDeliF" runat="server" 
                                     OnCommand="LinkButton_Command"   CommandArgument="F" CommandName="2" ><%= InProgDeliF%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink">
                                 <%if (SubmitDeliF == "0")
                                  { %>
                                    <asp:Label ID="LblSubmitDeliF" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkSubmitDeliF" runat="server" 
                                   OnCommand="LinkButton_Command"  CommandArgument="F" CommandName="3" ><%= SubmitDeliF%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                   <%if (ApprovedDeliF == "0")
                                     { %>
                                    <asp:Label ID="LblApprDeliF" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else
                                     { %> 
                                    <asp:LinkButton ID="LnkApprDeliF" runat="server" OnCommand="LinkButton_Command"   CommandArgument="F" CommandName="4" ><%= ApprovedDeliF%></asp:LinkButton>
                                    <%} %>
                             </td>
                            <td class="tdlink"> 
                                 <%if (ReopenDeliF == "0")
                                  { %>
                                    <asp:Label ID="LblReopenDeliF" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkReopenDeliF" runat="server" 
                                   OnCommand="LinkButton_Command"  CommandArgument="F" CommandName="5" ><%=ReopenDeliF%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                <%if (OverdueDeliF == "0")
                                  { %>
                                    <asp:Label ID="LblOverdueDeliF" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkOverdueDeliF" runat="server" 
                                  OnCommand="LinkButton_Command"   CommandArgument="F" CommandName="0" ><%=OverdueDeliF%></asp:LinkButton>
                                     <%} %>
                
                            </td>
                            <td class="tdlink" runat="server" id="TDIS"> 
                                 <%if (ApprDefDeliF == "0")
                                  { %>
                                    <asp:Label ID="LblApprDefDeliF" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDefDeliF" runat="server" 
                                   OnCommand="LinkButton_Command"   CommandArgument="F" CommandName="6" ><%=ApprDefDeliF%></asp:LinkButton>
                                     <%} %>
                            </td>
                        </tr>
                        <tr>
                            <td class="thheaderhori"> LCLS </td>
                            <td class="tdlink"> 
                                 <%if (NewDeliL == "0")
                                  { %>
                                    <asp:Label ID="LblNewDeliL" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkNewDeliL" runat="server" OnCommand="LinkButton_Command" CommandArgument="L" CommandName="1" ><%= NewDeliL %></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink">
                                 <%if (InProgDeliL == "0")
                                  { %>
                                    <asp:Label ID="LblInprogDeliL" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkInprogDeliL" runat="server" 
                                      OnCommand="LinkButton_Command" CommandArgument="L" CommandName="2" ><%= InProgDeliL%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink">
                                 <%if (SubmitDeliL == "0")
                                  { %>
                                    <asp:Label ID="LblSubmitDeliL" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkSubmitDeliL" runat="server" 
                                      OnCommand="LinkButton_Command" CommandArgument="L" CommandName="3" ><%= SubmitDeliL%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                  <%if (ApprovedDeliL == "0")
                                  { %>
                                    <asp:Label ID="LblApprDeliL" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDeliL" runat="server" 
                                       OnCommand="LinkButton_Command" CommandArgument="L" CommandName="4" ><%= ApprovedDeliL%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink">
                                  <%if (ReopenDeliL == "0")
                                  { %>
                                    <asp:Label ID="LblReopenDeliL" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkReopenDeliL" runat="server" 
                                       OnCommand="LinkButton_Command" CommandArgument="L" CommandName="5" ><%=ReopenDeliL%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                  <%if (OverdueDeliL == "0")
                                  { %>
                                    <asp:Label ID="LblOverdueDeliL" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkOverdueDeliL" runat="server" 
                                      OnCommand="LinkButton_Command" CommandArgument="L" CommandName="0" ><%=OverdueDeliL%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink" runat="server" id="TDL"> 
                                  <%if (ApprDefDeliL == "0")
                                  { %>
                                    <asp:Label ID="LblApprDefDeliL" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDefDeliL" runat="server" 
                                      OnCommand="LinkButton_Command" CommandArgument="L" CommandName="6" ><%=ApprDefDeliL%></asp:LinkButton>
                                     <%} %>
                            </td>
                        </tr>
                        <tr>
                            <td class="thheaderhori"> Particle Physics & Astro </td>
                            <td class="tdlink"> 
                                 <%if (NewDeliP == "0")
                                  { %>
                                    <asp:Label ID="LblNewDeliP" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkNewDeliP" runat="server"  OnCommand="LinkButton_Command" CommandArgument="P" CommandName="1" ><%= NewDeliP %></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                  <%if (InProgDeliP == "0")
                                  { %>
                                    <asp:Label ID="LblInprogDeliP" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkInprogDeliP" runat="server" 
                                       OnCommand="LinkButton_Command" CommandArgument="P" CommandName="2" ><%= InProgDeliP%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                 <%if (SubmitDeliP == "0")
                                  { %>
                                    <asp:Label ID="LblSubmitDeliP" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkSubmitDeliP" runat="server" 
                                      OnCommand="LinkButton_Command" CommandArgument="P" CommandName="3" ><%= SubmitDeliP%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                 <%if (ApprovedDeliP == "0")
                                  { %>
                                    <asp:Label ID="LblApprDeliP" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDeliP" runat="server"  OnCommand="LinkButton_Command" CommandArgument="P" CommandName="4"><%= ApprovedDeliP%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                  <%if (ReopenDeliP == "0")
                                  { %>
                                    <asp:Label ID="LblReopenDeliP" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkReopenDeliP" runat="server" 
                                       OnCommand="LinkButton_Command" CommandArgument="P" CommandName="5" ><%=ReopenDeliP%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                  <%if (OverdueDeliP == "0")
                                  { %>
                                    <asp:Label ID="LblOverdueDeliP" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkOverdueDeliP" runat="server" 
                                      OnCommand="LinkButton_Command" CommandArgument="P" CommandName="0" ><%=OverdueDeliP%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink" runat="server" id="TDP"> 
                                <%if (ApprDefDeliP == "0")
                                  { %>
                                    <asp:Label ID="LblApprDefDeliP" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDefDeliP" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="P" CommandName="6" ><%=ApprDefDeliP%></asp:LinkButton>
                                     <%} %>
                            </td>
                        </tr>
                        <tr>
                            <td class="thheaderhori"> Photon Science </td>
                            <td class="tdlink"> 
                                <%if (NewDeliX == "0")
                                  { %>
                                    <asp:Label ID="LblNewDeliX" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkNewDeliX" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="X" CommandName="1" ><%= NewDeliX %></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                  <%if (InProgDeliX == "0")
                                  { %>
                                    <asp:Label ID="LblInprogDeliX" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkInprogDeliX" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="X" CommandName="2" ><%= InProgDeliX%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                 <%if (SubmitDeliX == "0")
                                  { %>
                                    <asp:Label ID="LblSubmitDeliX" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkSubmitDeliX" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="X" CommandName="3" ><%= SubmitDeliX%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink">
                                  <%if (ApprovedDeliX == "0")
                                  { %>
                                    <asp:Label ID="LblApprDeliX" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDeliX" runat="server" 
                                      OnCommand="LinkButton_Command" CommandArgument="X" CommandName="4" ><%= ApprovedDeliX%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink">
                                 <%if (ReopenDeliX == "0")
                                  { %>
                                    <asp:Label ID="LblReopenDeliX" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkReopenDeliX" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="X" CommandName="5" ><%=ReopenDeliX%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink">
                                  <%if (OverdueDeliX == "0")
                                  { %>
                                    <asp:Label ID="LblOverdueDeliX" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkOverdueDeliX" runat="server" 
                                      OnCommand="LinkButton_Command" CommandArgument="X" CommandName="0" ><%=OverdueDeliX%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink" runat="server" id="TDPS">
                                  <%if (ApprDefDeliX == "0")
                                  { %>
                                    <asp:Label ID="LblApprDefDeliX" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDefDeliX" runat="server" 
                                      OnCommand="LinkButton_Command" CommandArgument="X" CommandName="6" ><%=ApprDefDeliX%></asp:LinkButton>
                                    <%} %>
                            </td>
                        </tr>
                        <tr>
                            <td class="thheaderhori"> SSRL</td>
                            <td class="tdlink">
                                 <%if (NewDeliS == "0")
                                  { %>
                                    <asp:Label ID="LblNewDeliS" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkNewDeliS" runat="server" OnCommand="LinkButton_Command" CommandArgument="S" CommandName="1" ><%= NewDeliS %></asp:LinkButton>
                                     <%} %>
                
                            </td>
                            <td class="tdlink">
                                 <%if (InProgDeliS == "0")
                                  { %>
                                    <asp:Label ID="LblInprogDeliS" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkInprogDeliS" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="S" CommandName="2" ><%= InProgDeliS%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink">
                                  <%if (SubmitDeliS == "0")
                                  { %>
                                    <asp:Label ID="LblSubmitDeliS" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkSubmitDeliS" runat="server" 
                                      OnCommand="LinkButton_Command" CommandArgument="S" CommandName="3" ><%= SubmitDeliS%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink">
                                  <%if (ApprovedDeliS == "0")
                                  { %>
                                    <asp:Label ID="LblApprDeliS" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDeliS" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="S" CommandName="4" ><%= ApprovedDeliS%></asp:LinkButton>
                                     <%} %>
                
                            </td>
                            <td class="tdlink">
                                  <%if (ReopenDeliS == "0")
                                  { %>
                                    <asp:Label ID="LblReopenDeliS" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkReopenDeliS" runat="server" 
                                      OnCommand="LinkButton_Command" CommandArgument="S" CommandName="5" ><%=ReopenDeliS%></asp:LinkButton>
                                     <%} %>
                
                            </td>
                            <td class="tdlink">
                                 <%if (OverdueDeliS == "0")
                                  { %>
                                    <asp:Label ID="LblOverdueDeliS" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkOverdueDeliS" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="S" CommandName="0" ><%=OverdueDeliS%></asp:LinkButton> 
                                     <%} %>
                            </td>
                            <td class="tdlink" runat="server" id="TDS">
                                  <%if (ApprDefDeliS == "0")
                                  { %>
                                    <asp:Label ID="LblApprDefDeliS" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDefDeliS" runat="server" 
                                     OnCommand="LinkButton_Command" CommandArgument="S" CommandName="6" ><%=ApprDefDeliS%></asp:LinkButton>
                                     <%} %>
                            </td>
                    </tr> 
                          
                 </table>
                  <br />
             </div>
            <div id="DivOwners" runat="server" visible="false">
                <div id="DivStatus" runat="server">
                <table class="tableborder" cellpadding="3" cellspacing="2"  width="95%">
                        <tr>
                            <th class="thheaderhori" style="width:10%;"> New</th>
                            <th class="thheaderhori" style="width:11%;"> In Progress </th>
                            <th class="thheaderhori" style="width:11%;"> Submitted </th>
                            <th class="thheaderhori" style="width:11%;"> Approved </th>
                            <th class="thheaderhori" style="width:11%;"> Re-opened </th>
                            <th class="thheaderhori" style="width:10%;">Overdue</th>
                            <!--<th class="thheaderhori" style="width:11%;">Approved <br />by Default</th>-->
                        </tr>
                        <tr>
                              <td class="tdlink"> 
                                <%if (NewDeliMy == "0")
                                  { %>
                                    <asp:Label ID="LblNewDeliMy" runat="server" Text="0"></asp:Label> <br />
                                <%} %>   <%else { %> 
                                    <asp:LinkButton ID="LnkNewDeliMy" runat="server"  OnCommand="LinkButtonMy_Command"  CommandName="1"><%= NewDeliMy %></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink">  
                                <%if (InProgDeliMy == "0")
                                  { %>
                                    <asp:Label ID="LblInprogDeliMy" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkInprogDeliMy" runat="server" 
                                     OnCommand="LinkButtonMy_Command"  CommandName="2"><%= InProgDeliMy%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                <%if (SubmitDeliMy == "0")
                                  { %>
                                    <asp:Label ID="LblSubmitDeliMy" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkSubmitDeliMy" runat="server" 
                                    OnCommand="LinkButtonMy_Command"  CommandName="3" ><%= SubmitDeliMy%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                  <%if (ApprovedDeliMy == "0")
                                  { %>
                                    <asp:Label ID="LblApprovedDeliMy" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprovedDeliMy" runat="server" 
                                       OnCommand="LinkButtonMy_Command"  CommandName="4" ><%= ApprovedDeliMy%></asp:LinkButton>
                                     <%} %>
                             </td>
                            <td class="tdlink"> 
                                 <%if (ReopenDeliMy == "0")
                                  { %>
                                    <asp:Label ID="LblReopenDeliMy" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkReopenDeliMy" runat="server" 
                                      OnCommand="LinkButtonMy_Command" CommandName="5" ><%=ReopenDeliMy%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink"> 
                                 <%if (OverdueDeliMy == "0")
                                  { %>
                                    <asp:Label ID="LblOverdueDeliMy" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkOverdueDeliMy" runat="server" 
                                      OnCommand="LinkButtonMy_Command" CommandName="0" ><%=OverdueDeliMy%></asp:LinkButton>
                                     <%} %>
                            </td>
                          <!--  <td class="tdlink"> 
                                 <%if (ApprDefDeliMy == "0")
                                  { %>
                                    <asp:Label ID="LblApprDefDeliMy" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprDefDeliMy" runat="server" 
                                     OnCommand="LinkButtonMy_Command"  CommandName="6" ><%=ApprDefDeliMy%></asp:LinkButton>
                                     <%} %>
                            </td>-->
                        
                        </tr>
                </table>
                </div>
                <br />
                <br />
                <br />
                <div id="Divdue" runat="server">
                         <table class="tableborder" cellpadding="3" cellspacing="2" width="95%">
                    <tr>
                        <th class="thheader" colspan="5" >Items Due in</th>
                    </tr>
                    <tr>
                        <td class="thheaderhori">1 day</td>
                        <td class="thheaderhori">Next 10 days</td>
                        <td class="thheaderhori">Next 30 days</td>
                        <td class="thheaderhori">Next 60 days</td>
                        <td class="thheaderhori">Next 90 days</td>
                    </tr>
                    <tr>
                        <td class="tdlink">
                            <%if (OneDay == "0")
                                  { %>
                                    <asp:Label ID="Lbl1day" runat="server" Text="0"></asp:Label> <br />
                                <%} %>   <%else { %> 
                                    <asp:LinkButton ID="Lnk1day" runat="server"  OnCommand="Lnk_Command"  CommandArgument="1"><%= OneDay %></asp:LinkButton>
                                     <%} %>
                        </td>
                        <td class="tdlink">
                             <%if (TenDays == "0")
                                  { %>
                                    <asp:Label ID="Lbl10days" runat="server" Text="0"></asp:Label> <br />
                                <%} %>   <%else { %> 
                                    <asp:LinkButton ID="Lnk10days" runat="server"  OnCommand="Lnk_Command"  CommandArgument="10"><%= TenDays %></asp:LinkButton>
                                     <%} %>                       
                        </td>
                        <td class="tdlink">
                             <%if (ThirtyDays == "0")
                                  { %>
                                    <asp:Label ID="Lbl30days" runat="server" Text="0"></asp:Label> <br />
                                <%} %>   <%else { %> 
                                    <asp:LinkButton ID="Lnk30days" runat="server"  OnCommand="Lnk_Command"  CommandArgument="30"><%= ThirtyDays %></asp:LinkButton>
                                     <%} %>                       
                        </td>
                        <td class="tdlink">
                             <%if (SixtyDays == "0")
                                  { %>
                                    <asp:Label ID="Lbl60days" runat="server" Text="0"></asp:Label> <br />
                                <%} %>   <%else { %> 
                                    <asp:LinkButton ID="Lnk60days" runat="server"  OnCommand="Lnk_Command"  CommandArgument="60"><%= SixtyDays %></asp:LinkButton>
                                     <%} %>                       
                        </td>
                         <td class="tdlink">
                             <%if (NinetyDays == "0")
                                  { %>
                                    <asp:Label ID="Lbl90days" runat="server" Text="0"></asp:Label> <br />
                                <%} %>   <%else { %> 
                                    <asp:LinkButton ID="Lnk90days" runat="server"  OnCommand="Lnk_Command"  CommandArgument="90"><%= NinetyDays %></asp:LinkButton>
                                     <%} %>                       
                        </td>
                    </tr>                
                </table>
                   <br />
                <br />
                </div>
           
               
            </div>
            
              
            <div id="DivSSO" runat="server" visible="false">
               <table class="tableborder" cellpadding="3" cellspacing="2" width="95%">
                    <tr>
                        <th class="thheader">Items that are pending your Approval</th>
                    </tr>
                    <tr>
                        <td class="tdlink">
                            <%if (PendMySSO == "0")
                                  { %>
                                    <asp:Label ID="LblPendMySSO" runat="server" Text="0"></asp:Label> <br />
                                <%} %>   <%else { %> 
                                    <asp:LinkButton ID="LnkMySSO" runat="server"  OnCommand="LnkMySSO_Command"  CommandName="3"><%= PendMySSO%></asp:LinkButton>
                                     <%} %>
                        </td>
    
                    </tr>                
                </table>
                  <br />
                <br />
              <table class="tableborder" cellpadding="3" cellspacing="2"  width="95%">
              <tr>
                        <th class="thheaderhori" colspan="2">All Deliverables that are</th>
              </tr>
                        <tr>
                            <td class="thheaderhori">Pending Approval by SSO</td>
                            <td class="thheaderhori">Approved </td>
                           <%-- <td class="thheaderhori">Approved by Default</td>--%>
                        </tr>
                        <tr>
                              <td class="tdlink"> 
                                <%if (PendSSO == "0")
                                  { %>
                                    <asp:Label ID="LblPendSSO" runat="server" Text="0"></asp:Label> <br />
                                <%} %>   <%else { %> 
                                    <asp:LinkButton ID="LnkPendSSO" runat="server"  OnCommand="LnkSSO_Command"  CommandName="3"><%= PendSSO%></asp:LinkButton>
                                     <%} %>
                            </td>
                            <td class="tdlink">  
                                <%if (ApprSSO == "0")
                                  { %>
                                    <asp:Label ID="LblApprSSO" runat="server" Text="0"></asp:Label> <br />
                                <%} %>  <%else { %> 
                                    <asp:LinkButton ID="LnkApprSSO" runat="server" 
                                     OnCommand="LnkSSO_Command"  CommandName="4"><%= ApprSSO%></asp:LinkButton>
                                     <%} %>
                            </td>
   
                        </tr>
                </table>
                
              
                   
                
            </div>
            </td>
            <td></td>
        </tr>
     </table>
      <asp:SqlDataSource ID="SDSFY" runat="server" ConnectionString="<%$ ConnectionStrings:SLAC_WEB %>" 
                                        
             ProviderName="<%$ ConnectionStrings:SLAC_WEB.ProviderName %>"  
             
             SelectCommand="SELECT DISTINCT(FYDUE) FROM VW_CMS_DELIVERABLE_DETAILS
           ORDER BY FYDUE ASC" 
             ></asp:SqlDataSource>
   
     </div>
   
</asp:Content>
