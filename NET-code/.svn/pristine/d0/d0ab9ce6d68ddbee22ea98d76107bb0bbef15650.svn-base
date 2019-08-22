<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ClauseList.ascx.cs"   Inherits="ContractManagement.User_Controls.ClauseList" %>
 <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
 <script src="../Scripts/jquery-ui-1.8.21.custom.min.js" type="text/javascript"></script>
 <link href="../Styles/CMS.css" type="text/css" rel="Stylesheet" />
 <script  type="text/javascript">
     function onKeypress(btnid) {
         if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {
             $('#' + btnid).click();

             return false;
         }
         else
             return true;
     }

     $(function () {
         $("[id*=ImgReqShow]").each(function () {
             if ($(this)[0].src.indexOf("collapsebig") != -1) {
                 $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                 $(this).next().remove();
             }
         });
         $("[id*=ImgDeliShow]").each(function () {
             if ($(this)[0].src.indexOf("collapsebig") != -1) {
                 $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>");
                 $(this).next().remove();
             }
         });
     });
    
 </script>

<div id="divClause" runat="server">
  <div align="center"><asp:Label ID="Lblhead" runat="server"  Font-Size="Large"  Font-Bold="true" Text="Top level list of all Clauses"></asp:Label></div>
  <br />
   <br />
        <table id="TblSearch" runat="server">

            <tr>
                <td> <asp:Label ID="LblSearch" runat="server"  Font-Size="medium" Text="Search all Clauses on any keyword:"></asp:Label>  </td>
                <td></td>
                <td><asp:TextBox ID= "TxtClauseName" runat="server"></asp:TextBox></td>
                <td></td>
                <td><asp:Button ID="cmdFind" runat="server" text="Search" onclick="cmdFind_Click" ClientIDMode="Static"/></td>
                      
            </tr>
        </table>
       
        <br />
        <asp:ImageButton ID="ImgBtnExport" runat="server" ImageUrl="~/Images/ExportToExcel.gif" onclick="ImgBtnExport_Click" /> &nbsp;
        <span style=" font-size:medium;" id="spnOwntext">(View the full text of your clauses in the Prime Contract: <a href="http://www-group.slac.stanford.edu/legal/docs/ProformaSLACContract.pdf" target ="_blank">http://www-group.slac.stanford.edu/legal/docs/ProformaSLACContract.pdf</a>)</span>
        <table style="width:100%">
        <tr>
            <td>
                 <asp:GridView ID="GvClause" runat="server" skinID="gridviewSkin" Width="100%" AutoGenerateColumns="false" AllowPaging="True"  AllowSorting="true" OnRowDataBound = "GvClause_RowDataBound" DataKeyNames="CLAUSE_ID" PageSize="20" OnPageIndexChanging= "GvClause_PageIndexChanging" OnSorting="GvClause_Sorting">
                        <Columns>  
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImgReqShow" runat="server" OnClick ="Toggle_ReqsGrid" ImageUrl="~/Images/expandbig.gif" CommandArgument="Show" />
                                    <asp:Panel ID="PnlRequirement" runat="server">
                                        <asp:GridView ID="GvRequirement" runat="server" SkinID="gridviewSkin" Width="100%" AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound = "GvRequirement_RowDataBound" DataKeyNames="REQUIREMENT_ID" PageSize="10" OnPageIndexChanging="GvRequirement_PageIndexChanging" OnSorting="GvRequirement_Sorting">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgDeliShow" runat="server" OnClick ="Toggle_DeliGrid" ImageUrl="~/Images/expandbig.gif" CommandArgument="Show" />
                                                        <asp:Panel ID="PnlDeli" runat="server">
                                                            <asp:GridView ID="GvDeli" runat="server" SkinID="gridviewSkin" Width="100%" AutoGenerateColumns="false" AllowPaging="true" DataKeyNames="DELIVERABLE_ID" OnRowDataBound = "GvDeli_RowDataBound" PageSize="10" OnPageIndexChanging="GvDeli_PageIndexChanging" OnSorting="GvDeli_Sorting">
                                                                <Columns>
                                                                     <asp:BoundField DataField ="DELIVERABLE_ID" HeaderText ="Deliverable Id" visible="false"/>
                                                                      <asp:HyperLinkField DataNavigateUrlFields="DELIVERABLE_ID"  DataNavigateUrlFormatString="~/Deliverable.aspx?mode=view&id={0}" SortExpression="COMPOSITE_KEY"
                                                                        HeaderText="Track Id" DataTextField="COMPOSITE_KEY" />                              
                                                                    <asp:BoundField DataField ="DUE_DATE" HeaderText ="Due Date" HtmlEncode ="false" DataFormatString = "{0:MM/dd/yyyy}"  SortExpression="DUE_DATE"/>
                                                                    <asp:BoundField DataField ="OWNER" HeaderText ="Owner"  Visible="true" SortExpression="OWNER" /> 
                                                                    <asp:TemplateField HeaderText="SubOwners">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblSO" runat="server" ></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                                                               
                                                                </Columns>
                                                           
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                    
                                                </asp:TemplateField>
                                                <asp:BoundField DataField ="REQUIREMENT_ID" HeaderText ="Id"  Visible="false"/>
                                                <asp:BoundField DataField ="REQUIREMENT" HeaderText ="Requirement" SortExpression ="REQUIREMENT" />
                                                <asp:BoundField DataField ="CLAUSE_NUMBER" HeaderText ="Clause"  SortExpression ="CLAUSE_NUMBER"/>
                                                <asp:BoundField DataField ="OWNERNAME" HeaderText = "Owner" visible="false" />
                                                <asp:BoundField DataField = "FREQUENCY" HeaderText ="Frequency"  SortExpression="FREQUENCY"/>
                                                <asp:BoundField DataField = "DELCOUNT" Visible ="false" />
                                            
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:TemplateField>  
                            <asp:BoundField datafield="CLAUSE_ID" HeaderText= "Clause Id"  Visible="false"/>                                                   
                            <asp:BoundField DataField="CLAUSE_NUMBER" HeaderText="Clause Number"  SortExpression="CLAUSE_NUMBER" />  
                            <asp:BoundField DataField="CLAUSE_NAME" HeaderText="Clause Name"  SortExpression="CLAUSE_NAME" />                     
                            <asp:BoundField DataField ="OWNERNAME" HeaderText ="Owner" Visible="true" SortExpression="OWNERNAME" />
                            <asp:BoundField DataField ="REQCOUNT" Visible="false" />
                        </Columns>
                       
                
               </asp:GridView>
            
           </td>
        
        </tr>
    
   </table>



</div>

