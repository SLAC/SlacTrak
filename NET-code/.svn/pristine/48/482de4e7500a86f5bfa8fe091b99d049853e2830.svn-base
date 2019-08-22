<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"  StylesheetTheme="CMS_Theme" CodeBehind="Clause.aspx.cs" Inherits="ContractManagement.Admin.Clause" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.8.21.custom.min.js" type="text/javascript"></script>
    <style type="text/css">
        .style2
        {
            width: 129px;
        }
        .style3
        {
            width: 22px;
        }
        .style4
        {
            width: 631px;
        }
        .style17
        {
            width: 37px;
        }
        .style20
        {
            width: 474px;
        }
        .style21
        {
            font-weight: bold;
            color: #696969;
            font-size: 1.0 em;
            text-align: right;
            width: 182px;
        }
        .style22
        {
            width: 182px;
        }
        .style23
        {
            width: 502px;
        }
    </style>
     <link href="../css/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/CMS.css" rel="Stylesheet" type="text/css" />
    <script src="../Scripts/CMS_Common.js"  type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/Jquery_common.js"></script>	
    <script type="text/javascript">
         function DisableSubmit(mode) {

             if (mode == "new") {
                 $("#BtnSave").attr('disabled', 'true');
                 $("#BtnChange").attr('disabled', 'true');
                 $("#BtnSaveAddSC").attr('disabled', 'true');
                 $("#BtnSaveAddReq").attr('disabled', 'true');
             }
             else if (mode == "edit") {
                 $("#BtnUpdate").attr('disabled', 'true');
                 $("#BtnCancel").attr('disabled', 'true');
                 $("#BtnMoreChange").attr('disabled', 'true');
             }

         }

     
         
    </script>							 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="smClause" runat="server"  EnablePartialRendering="true">
        
    </asp:ScriptManager>
    <asp:Panel ID="PnlClause" runat="server">
      <asp:UpdatePanel ID="UpdClaus" runat="server" >
        <ContentTemplate>
          <div>
        <table width="100%">
            <tr>
                <td> <h2><asp:Label ID="LblSubTitle" runat="server"></asp:Label></h2></td>
                <td align="right"> 
                    <table>
                        <tr>
                            <td> <div id="DivEdit" runat="server" visible="false"> <asp:Button  ID="BtnEdit" runat="server"  Text="Edit" onclick="BtnEdit_Click" Width="129px"/> </div></td>
                            <td><div id="DivDelete" runat="server" visible="false"><asp:Button ID="BtnDelete" 
                                runat="server" Text="Delete"   Width="129px" onclick="BtnDelete_Click" OnClientClick="return confirm('Are you certain you want to delete this?');" CausesValidation="false"/></div></td>
                        </tr>
                    </table>
                
               </td>
                        
            </tr>
        </table>
    
    </div>
   

    <div id="divClauseList" runat="server" visible="true" >
    <br />
         <table>

        <tr>
            <td> Search Clauses on any keyword:  </td>
            <td></td>
            <td><asp:TextBox ID= "TxtClauseName" runat="server"></asp:TextBox></td>
            <td></td>
            <td><asp:Button ID="cmdFind" runat="server" text="Search" onclick="cmdFind_Click" ClientIDMode="Static"/></td>
            <td>&nbsp;</td>
            <td><asp:Button ID="cmdAdd" runat="server" Text="Add a new Clause" 
                    onclick="cmdAdd_Click" /></td>
          
        </tr>
    </table>
   <br />
    <hr />
    <asp:Panel ID="PnlClauseList" runat="server" Width="100%">
          <table style="width:100%;">
        <tr>
            <td>
                <asp:Label ID="LblInfo" runat="server" Text="Click on Id to open the item" style="font-size:medium;"></asp:Label>

            </td>
        </tr>
       
        <tr>
            <td>
                <asp:GridView ID="GvClause" runat="server" EmptyDataText="No Clause found" 
                    onpageindexchanging="GvClause_PageIndexChanging" 
                    onrowcommand="GvClause_RowCommand" onsorting="GvClause_Sorting" 
                    SkinID="gridviewSkin" onrowdatabound="GvClause_RowDataBound" 
                    onsorted="GvClause_Sorted" Width="100%">
                        <Columns>
                            
                            <asp:HyperLinkField DataNavigateUrlFields="CLAUSE_ID" DataNavigateUrlFormatString="Clause.aspx?mode=view&id={0}" SortExpression="CLAUSE_ID" HeaderText="Id" DataTextField="CLAUSE_ID" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="CONTRACT_NAME" HeaderText= "Contract" SortExpression="CONTRACT_NAME"  ItemStyle-Width="10%" />
                            <asp:BoundField DataField="CLAUSE_NAME" HeaderText="Clause Name" SortExpression="CLAUSE_NAME"  ItemStyle-Width="15%"/>
                            <asp:BoundField DataField="CLAUSE_NUMBER" HeaderText="Clause Number" SortExpression="CLAUSE_NUMBER" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="SUBCLAUSENAME" HeaderText="Subclause Name" SortExpression="SUBCLAUSENAME" ItemStyle-Width="15%" />
                            <asp:BoundField DataField="SUBCLAUSENUM" HeaderText="Subclause Number" SortExpression="SUBCLAUSENUM" ItemStyle-Width="10%" />                                                        
                            <asp:BoundField DataField="OWNERNAME" HeaderText="Owner" SortExpression="OWNERNAME"  ItemStyle-Width="10%"/>
                            <asp:BoundField DataField="CLAUSETYPE" HeaderText="Clause Type" SortExpression="CLAUSETYPE" ItemStyle-Width="10%" />

                          
                            <asp:TemplateField HeaderText="Add" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkSubclause" Text="Subclause"  runat="server" Visible="false" CommandName="sc" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CLAUSE_ID") %>'></asp:LinkButton>
                                    <asp:LinkButton ID="LnkRequirement" Text="Requirement"  runat="server" CommandName="req" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "CLAUSE_ID") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>   
                        
                        </Columns>
                       
                
                </asp:GridView>
            <asp:Label ID="LblFooter" runat="server"></asp:Label>
            </td>
            
        </tr>
    </table>
    </asp:Panel>
  
    
    </div>
   
        <div id="dialogowneradmin" style="display:none;" >             
                <iframe id="modaldialogowneradmin" scrolling="no" frameborder="1" width="100%" height="100%">
                </iframe>
          </div>

    <div id="divClauseNew" runat="server" visible="false">
        <hr />
        <table>
            <tr>
                <td style="width:25%;"></td>
                <td class="style4">
                       <table style="width: 606px">
                       <tr>
                       <td colspan="2"></td>
                            <td align="left" class="style23"><div id="Divreq" runat="server"><span  class="spanrequired"><span  class ="spanred">*</span> Required Fields </span></div> </td>
                            
                           
                        </tr>
                        <tr>
                            <td class="style22">&nbsp;</td>
                        </tr>
                            <tr id="trId" runat="server" visible="false" class="constantheight">
                                <td class="style21"  valign="top"><asp:Label ID="LblClauseId" runat="server" Text="Clause Id:"></asp:Label></td>
                                <td class="style3">&nbsp;</td>
                                <td class="style23"  valign="top"><asp:Label ID="LblClauseIdVal" runat="server"></asp:Label></td>
                                <td class="style17"></td>
                            </tr>
                            <tr class="constantheight">
                                <td class="style21"  valign="top"><span id="SpnName" class="spanred" runat="server">*</span><asp:Label ID="LblName" runat="server" Text="Name:"></asp:Label></td>
                                <td class="style3"></td>
                                <td class="style23"  valign="top">
                                    <div id="DivNameView" runat="server">
                                        <asp:Label ID="LblNameVal" runat="server" onprerender="LblNameVal_PreRender"></asp:Label>
                                    </div>
                                    <div id="DivName" runat="server">
                                        <table>
                                            <tr>
                                                <td> <asp:TextBox ID="TxtName" runat="server" TextMode="MultiLine"  
                                            ClientIDMode="Static" onkeypress="return textboxMultilineMaxNumber(this,380)" 
                                            Height="55px" Width="249px"></asp:TextBox></td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td><asp:RegularExpressionValidator ID="RegExReqmnt" runat="server" ControlToValidate="TxtName" ValidationExpression="(?:[\r\n]*.[\r\n]*){0,380}"  ErrorMessage="Exceeded 380 chars" CssClass="errlabels" SetFocusOnError="true" ValidationGroup="addedit" ></asp:RegularExpressionValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:RequiredFieldValidator ID="RVName" runat="server" CssClass="errlabels" ControlToValidate="TxtName" ErrorMessage="Clause Name is required" ValidationGroup="addedit"></asp:RequiredFieldValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span class="formattext">(max. 380 chars)</span></td>
                                                    </tr>
                                                </table>
                                            </td>
                                            </tr>

                                        </table>
                                       
                                        
                                    </div>
                                </td>   
                                <td class="style17"></td>      
                            </tr> 
                            <tr class="constantheight">
                                <td class="style21"  valign="top"><span id="SpanClauseNum" class="spanred" runat="server">*</span><asp:Label ID="LblClauseNum" runat="server" Text="Number:"></asp:Label></td>
                                <td class="style3"></td>
                                <td class="style23"  valign="top">
                                    <div id="DivClauseNumView" runat="server">
                                        <asp:Label ID="LblClauseNumVal" runat="server"></asp:Label>
                                    </div>
                                    <div id="DivClauseNum" runat="server">
                                        <asp:Textbox ID="TxtClauseNum" runat="server" MaxLength="100" Width="242px"></asp:Textbox>
                                        <asp:RequiredFieldValidator ID="RVNum" runat="server" CssClass="errlabels" ControlToValidate="TxtClauseNum" ErrorMessage="Number is required" ValidationGroup="addedit"></asp:RequiredFieldValidator>
                                    </div>
                                </td>
                                <td class="style17"></td>
                            </tr>
                            <tr class="constantheight" id="trClausename" runat="server">
                                <td class="style21"  valign="top"><span id="SpanClauseName" class="spanred" runat="server" visible="false">*</span><asp:Label ID="LblClauseName" runat="server" Text="Clause Name:"></asp:Label></td>
                                <td class="style3"></td>
                                <td class="style23"  valign="top">
                                    <div id="DivClauseNameView" runat="server">
                                        <asp:Label ID="LblClauseNameVal" runat="server"></asp:Label>
                                    </div>
                                    
                                </td>    
                                <td class="style17"></td>     
                            </tr> 
                              <tr class="constantheight" id="trparentno" runat="server">
                                <td class="style21"  valign="top"><asp:Label ID="LblParentNo" runat="server" Text="Clause Number:"></asp:Label></td>
                                <td class="style3"></td>
                                <td class="style23"  valign="top">
                                    <div id="DivParentnoView" runat="server">
                                        <asp:Label ID="LblParentnoVal" runat="server"></asp:Label>
                                    </div>
                                    
                                </td>  
                                <td class="style17"></td>       
                            </tr> 
                            <tr class="constantheight">
                                <td  class="style21"  valign="top"><span id="SpanContract" class="spanred" runat="server">*</span><asp:Label ID="LblContract" runat="server" Text="Contract:"></asp:Label></td>
                                <td class="style3"></td>
                                <td class="style23"  valign="top">
                                    <div id="DivContractView" runat="server">
                                        <asp:Label ID="LblTypeVal" runat="server"></asp:Label>
                                    </div>
                                    <div id="DivContract" runat="server">
                                        <asp:DropdownList ID="DdlType" runat="server" DataSourceID="SDSContract" 
                                            DataTextField="CONTRACT_NAME" DataValueField="CONTRACT_ID" 
                                            ondatabound="DdlType_DataBound">
                                            
                                        </asp:DropdownList>
                                        &nbsp;<asp:CustomValidator ID="cvType" runat="server" EnableClientScript="true" Display="Dynamic"
                                         ClientValidationFunction="DropdownValidation" ControlToValidate="DdlType" ErrorMessage="Contract is required" SetFocusOnError="true" CssClass="errlabels" ValidationGroup="addedit"></asp:CustomValidator>
                                        <asp:SqlDataSource ID="SDSContract" runat="server" ConnectionString="<%$ ConnectionStrings:SLAC_WEB %>" 
                                        ProviderName="System.Data.OracleClient"  SelectCommand="SELECT CONTRACT_ID,CONTRACT_NAME FROM CMS_CONTRACT_OTHERTYPES WHERE IS_ACTIVE='Y' AND GROUP_ID=2"></asp:SqlDataSource>
                                    </div>
                                </td>  
                                <td class="style17">          
                            </tr>                           
                            <tr class="constantheight">
                                <td class="style21"  valign="top"><span id="SpanOwner" class="spanred" runat="server">*</span><asp:Label ID="LblOwner" runat="server" Text="Owner:"></asp:Label></td>
                                <td class="style3"></td>
                                <td class="style23"  valign="top">
                                    <div id="DivOwnerView" runat="server">
                                        <asp:Label ID="LblOwnerVal" runat="server"></asp:Label>
                                    </div>
                                    <div id="DivOwner" runat="server">
                                        <table>
                                        <tr>
                                            <td style="vertical-align:top;"><asp:TextBox ID="TxtOwneradmin"  runat="server" ClientIDMode="Static" MaxLength="30" 
                                                    CssClass="txtnamewidth" AutoPostBack="True"></asp:TextBox></td>
                                             <td style="vertical-align:top;">
                                             <asp:ImageButton ID="ImgbtnOwn" runat="server"  ImageUrl="~/Images/find.gif"/>
                                           
                                            </td>
                                            <td class="style2" style="vertical-align:top;"><asp:Label ID="Lblformat" runat="server" CssClass="formattext" Text="(Lastname, firstname)"></asp:Label></td>
                                            <td style="vertical-align:top;">
                                                <table>
                                                    <tr>
                                                        <td  style="vertical-align:top;"><asp:RequiredFieldValidator ID="RVOwn" runat="server" CssClass="errlabels" ControlToValidate="TxtOwneradmin"  ErrorMessage="Owner is required" SetFocusOnError="true" ValidationGroup="addedit"></asp:RequiredFieldValidator></td>
                                                        
                                                    </tr>
                                                    <tr>
                                                       <td  style="vertical-align:top;">
                                                            <asp:CustomValidator ID="CvOwn"  CssClass="errlabels"  OnServerValidate= "CvOwn_ServerValidate"  runat="server" ControlToValidate= "TxtOwneradmin"  ValidateEmptyText="true" ErrorMessage="Not a Valid Name/Format" SetFocusOnError="true"  ValidationGroup="addedit"></asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                       
                                    </div>
                                </td>
                            </tr>
                            <tr style="height:20px;">
                                <td class="style22"></td>
                            </tr>
                            <tr>
                            <td colspan="2"></td>
                            <td  align="left" class="style23">
                                <div id="DivReview" runat="server" >
                                    <asp:Button ID="BtnReview" runat="server" 
                                        Text="Review" onclick="BtnReview_Click"  ValidationGroup="addedit"/></div>   
                                <div id="DivReviewedit" runat="server">
                                    <asp:Button ID="BtnReview2" runat="server" 
                                        Text="Review" onclick="BtnReview2_Click"  ValidationGroup="addedit"/></div>             
                            </td>
                        </tr>
                        <tr>
                            
                            <td  colspan="4" align="center" >
                                <div id="DivSubmit" runat="server"  visible="false"><asp:Button ID="BtnSave" runat="server" 
                                        Text="Submit" onclick="BtnSave_Click"  CausesValidation="false" ToolTip="Submit Clause and Stay on the Clause screen"/>
                                &nbsp;&nbsp;<asp:Button ID="BtnChange" runat="server" 
                                        Text="Make Changes" onclick="BtnChange_Click"  CausesValidation="false" ToolTip="Go Back and make changes"/>
                                         &nbsp;&nbsp;<asp:Button ID="BtnSaveAddSC" runat="server" 
                                        Text="Add Subclause" onclick="BtnSaveAddSC_Click"  CausesValidation="false" ToolTip ="Submit and go to Add Subclause screen"/>
                                          &nbsp;&nbsp;<asp:Button ID="BtnSaveAddReq" runat="server" 
                                        Text="Add Requirement" onclick="BtnSaveAddReq_Click"  CausesValidation="false" ToolTip="Submit and go to Add Requirement Screen"/>
                                </div>
                                 <div id="DivUpdate" runat="server" visible="false">
                                     <asp:Button ID="BtnUpdate" runat="server" Text="Update" 
                                         onclick="BtnUpdate_Click"  CausesValidation="false"/>
                                 &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnCancel" runat="server" 
                                         Text="Cancel Changes" onclick="BtnCancel_Click"  CausesValidation="false"/>
                                  &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnMoreChange" runat="server" 
                                         Text="Make more Changes" onclick="BtnMoreChange_Click"  CausesValidation="false"/>
                                </div>
                            </td>
                        </tr>
                              
                          
                        
                        </table>
                </td>
            </tr>
        </table>
      
    <asp:HiddenField ID="HdnMode" runat="server" />
    <asp:HiddenField ID="HdnType" runat="server" />
    <asp:HiddenField ID="HdnClauseId" runat="server" />
    <asp:HiddenField ID="HdnFilter" runat="server" />
    <asp:HiddenField ID="HdnDesc" runat="server" />
    </div>
        
        </ContentTemplate>
       
    </asp:UpdatePanel>
   
    </asp:Panel>
  
   
</asp:Content>
