<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master"  StylesheetTheme="CMS_Theme" AutoEventWireup="true" CodeBehind="Requirement.aspx.cs" Inherits="ContractManagement.Admin.Requirement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../css/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/CMS.css" rel="Stylesheet" type="text/css" />
    <script src="../Scripts/CMS_Common.js"  type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/Jquery_common.js"></script>	
    <script src="../Scripts/jquery-ui-1.8.21.custom.min.js" type="text/javascript"></script>
    <script type="text/javascript">
         function DisableSubmit(mode) {

             if (mode == "new") {
                 $("#BtnSave").attr('disabled', 'true');
                 $("#BtnChange").attr('disabled', 'true');
             
             }
             else if (mode == "edit") {
                 $("#BtnUpdate").attr('disabled', 'true');
                 $("#BtnCancel").attr('disabled', 'true');
                 $("#BtnMoreChange").attr('disabled', 'true');
             }

         }

         function pageLoad() {

             $("#<%=TxtStartDate.ClientID%>").datepicker({
                 showOn: 'button', buttonText: 'Choose Date', buttonImage: '../Images/cal.gif', buttonImageOnly: true, changeMonth: true, changeYear: true
             });
         }
        </script>
    <style type="text/css">
        .style1
        {
            width: 173px;
        }
       
      
      .ui-datepicker-trigger {
                               margin-left:5px;
                               margin-top: 8px;
                               margin-bottom: -3px;
                              }


        .style2
        {
            width: 611px;
        }


      </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="smRequirement" runat="server" EnablePartialRendering="true">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdRequirement" runat="server">
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
                                runat="server" Text="Delete"   Width="129px" onclick="BtnDelete_Click" OnClientClick="return confirm('Are you certain you want to delete this requirement?');" CausesValidation="false"/></div></td>
                        </tr>
                    </table>               
               </td>
              </tr>
        </table>   
    </div>
    <div id ="DivRequirementList" runat="server" visible="true">
    <br />
    <table>
            <tr>
                <td> Search Requirements on any keyword:  </td>
                <td></td>
                <td><asp:TextBox ID= "TxtReqName" runat="server"></asp:TextBox></td>
                <td></td>
                <td><asp:Button ID="cmdFind" runat="server" text="Search" onclick="cmdFind_Click" ClientIDMode="Static"/></td>
                <td>&nbsp;</td>
                <td><asp:Button ID="cmdAdd" runat="server" Text="Add a new Requirement" onclick="cmdAdd_Click" /></td>          
            </tr>
     </table>
     <br />
     <hr />
     <asp:Panel ID="PnlRequirementList" runat="server" Width="100%">
        <table style="width:100%;">
         <tr>
            <td>
                <asp:Label ID="LblInfo" runat="server" Text="Click on Requirement to open the item" style="font-size:medium;"></asp:Label>

            </td>
        </tr>
            <tr>
            <td>
                <asp:GridView ID="GvRequirement" runat="server" EmptyDataText="No Requirement found" 
                    onpageindexchanging="GvRequirement_PageIndexChanging" onsorting="GvRequirement_Sorting" 
                    SkinID="gridviewSkin" onrowdatabound="GvRequirement_RowDataBound" 
                    onsorted="GvRequirement_Sorted">
                        <Columns>
                            <asp:BoundField DataField="REQUIREMENT_ID" HeaderText="Requirement Id" SortExpression="REQUIREMENT_ID"/>
                            <asp:HyperLinkField DataNavigateUrlFields="REQUIREMENT_ID" DataNavigateUrlFormatString="Requirement.aspx?mode=view&id={0}" SortExpression="REQUIREMENT" HeaderText="Requirement" DataTextField="REQUIREMENT" />
                            <asp:BoundField DataField="FREQUENCY" HeaderText= "Frequency" SortExpression="FREQUENCY" />                            
                            <asp:BoundField DataField="START_DATE" HeaderText="Start Date" SortExpression="START_DATE" DataFormatString="{0:MM/dd/yyyy}"  HtmlEncode="false"/>
                            <asp:BoundField DataField="OWNERNAME" HeaderText="Owner" SortExpression="OWNERNAME" />
                            <asp:BoundField DataField="CLAUSENUM" HeaderText="Clause" SortExpression="CLAUSENUM" />
                            <asp:BoundField DataField="SUBCLAUSENUM" HeaderText="Subclause" SortExpression="SUBCLAUSENUM" />
                            <asp:BoundField DataField="CONTRACT_NAME" HeaderText="Contract" SortExpression="CONTRACT_NAME" />
                            <asp:BoundField DataField="CLAUSE_NAME" Visible="false" />
                            <asp:BoundField DataField="SUBCLAUSENAME" Visible="false" />
                        </Columns>
                
                
                </asp:GridView>
            <asp:Label ID="LblFooter" runat="server"></asp:Label>
            </td>
            
        </tr>
        </table>     
     </asp:Panel>
    </div>

    <div id="divRequirementNew" runat="server" visible="false">
        <hr />
        <table>
            <tr>
                <td id="tdLeft" style="width:15%;" runat="server"></td>
                <td>
                       <table>
                       <tr>
                       <td colspan="2"></td>
                            <td align="left" class="style2"><div id="Divreq" runat="server"><span  class="spanrequired"><span  class ="spanred">*</span> Required Fields </span></div> </td>
                            
                           
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                            <tr class="constantheight" id="trSCFlown" runat="server">
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td class="style2" valign="top">
                                <asp:CheckBox ID="ChkSCFlownProv" runat="server"  TextAlign="Right" Text=" - This is a subcontractor flow-down provision"/>
                            </td>
                        </tr>
                            <tr id="trId" runat="server" visible="false" class="constantheight">
                                <td class="tdsubtitle"  valign="top"><asp:Label ID="LblReqId" runat="server" Text="Requirement Id:"></asp:Label></td>
                                <td>&nbsp;</td>
                                <td class="style2"  valign="top"><asp:Label ID="LblReqIdVal" runat="server"></asp:Label></td>
                            </tr>
                          
                            <tr id="trClauseno" runat="server" visible="false" class="constantheight">
                                <td class="tdsubtitle"  valign="top"><asp:Label ID="LblClauseno" runat="server" Text="Clause Number:"></asp:Label></td>
                                <td>&nbsp;</td>
                                <td  valign="top"><asp:Label id="LblClausenoVal" runat="server"></asp:Label></td>
                            </tr>

                            <tr class="constantheight">
                                <td class="tdsubtitle"  valign="top">
                                    <span id="SpnClause" class="spanred" runat="server">*</span>
                                    <asp:Label ID="LblClause" runat="server" Text="Clause:"></asp:Label>
                                </td> 
                                <td></td>                           
                                <td class="style2"  valign="top">
                                    <div id="DivClauseView" runat="server">
                                        <asp:Label ID="LblClauseVal" runat="server"></asp:Label>
                                    </div>
                                    <div id="DivClause" runat="server">
                                        <asp:DropDownList ID="DdlClause" runat="server" DataSourceID="SDSClause" 
                                            DataTextField="CLAUSE_NAME" DataValueField="CLAUSE_ID" 
                                            ondatabound="DdlClause_DataBound" AutoPostBack="True" 
                                            onselectedindexchanged="DdlClause_SelectedIndexChanged">
                                        
                                        </asp:DropDownList>
                                         &nbsp;<asp:CustomValidator ID="CVClause" runat="server" EnableClientScript="true"  Display="Dynamic"
                                             ClientValidationFunction="DropdownValidation" ControlToValidate="DdlClause" ErrorMessage="Clause is required" SetFocusOnError="true" CssClass="errlabels" ValidationGroup="addedit"></asp:CustomValidator>
                                         <asp:SqlDataSource ID="SDSClause" runat="server" ConnectionString="<%$ ConnectionStrings:SLAC_WEB %>" 
                                        ProviderName="System.Data.OracleClient"  SelectCommand="SELECT CLAUSE_ID, SUBSTR(CLAUSE_NUMBER || ' ' || DECODE(SUBCLAUSENUM,NULL,'',' ' || SUBCLAUSENUM) || ' - ' || SUBSTR(CLAUSE_NAME,1,60),1,65) AS CLAUSE_NAME FROM VW_CMS_CLAUSE_DETAILS_2 ORDER BY CLAUSE_NUMBER"></asp:SqlDataSource>
                                    </div>
                                    
                                </td>
                            </tr>
                            <tr class="constantheight" runat="server" id="trSubclause" visible="false">
                                <td class="tdsubtitle"  valign="top"><asp:Label ID="LblSubclause" runat="server" Text="Subclause:"></asp:Label></td>
                                <td>&nbsp;</td>
                                <td valign="top"><asp:Label id="LblSubclauseVal" runat="server"></asp:Label></td>
                            
                            </tr>
                            <tr class="constantheight" runat="server" id="trOwner" visible="false">
                                <td class="tdsubtitle" valign="top"><asp:Label ID="LblOwner" runat="server" Text="Clause Owner:"></asp:Label></td>
                                <td>&nbsp;</td>
                                <td valign="top"><asp:Label id="LblOwnerVal" runat="server"></asp:Label></td>
                            
                            </tr>
                            <tr class="constantheight">
                                <td class="tdsubtitle" valign="top"><span id="SpnRequirement" class="spanred" runat="server">*</span><asp:Label ID="LblReqmnt" runat="server" Text="Requirement:"></asp:Label></td>
                                <td></td>
                                <td class="style2" valign="top">
                                    <div id="DivReqmntView" runat="server">
                                        <asp:Label ID="LblReqmntVal" runat="server" 
                                            onprerender="LblReqmntVal_PreRender"></asp:Label>
                                    </div>
                                    <div id="DivReqmnt" runat="server">
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="TxtReqmnt" runat="server"  TextMode="MultiLine" CssClass="txtmulti" ClientIDMode="Static" onkeypress="return textboxMultilineMaxNumber(this,3800)"></asp:TextBox></td>
                                            <td class="style1">
                                                <table>
                                                    <tr><td><asp:RegularExpressionValidator ID="RegExReqmnt" runat="server" ControlToValidate="TxtReqmnt" ValidationExpression="(?:[\r\n]*.[\r\n]*){0,3800}"  ErrorMessage="Exceeded 3800 chars" CssClass="errlabels" SetFocusOnError="true" ValidationGroup="addedit" ></asp:RegularExpressionValidator></td></tr>
                                                    <tr><td> <asp:RequiredFieldValidator ID="RVReqmnt" runat="server" CssClass="errlabels" ControlToValidate="TxtReqmnt" ErrorMessage="Requirement required" ValidationGroup="addedit"></asp:RequiredFieldValidator> </td></tr>
                                                    <tr><td><span class="formattext">(max. 3800 chars)</span></td></tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                        
                                       
                                    </div>
                                </td>         
                            </tr> 
                            <tr class="constantheight">
                                <td class="tdsubtitle" valign="top"><asp:Label ID="LblNotes" runat="server" Text="Notes:"></asp:Label></td>
                                <td></td>
                                <td class="style2" valign="top">
                                    <div id="DivNotesView" runat="server">
                                        <asp:Label ID="LblNotesVal" runat="server" onprerender="LblNotesVal_PreRender"></asp:Label>
                                    </div>
                                    <div id="DivNotes" runat="server">
                                    <table>
                                        <tr>
                                            <td><asp:Textbox ID="TxtNotes" runat="server"  TextMode="MultiLine" CssClass="txtmulti" ClientIDMode="Static" onkeypress="return textboxMultilineMaxNumber(this,3800)"></asp:Textbox></td>
                                            <td class="style1">
                                                <table>
                                                    <tr><td><asp:RegularExpressionValidator ID="RegExNotes" runat="server" ControlToValidate="TxtNotes" ValidationExpression="(?:[\r\n]*.[\r\n]*){0,3800}"  ErrorMessage="Exceeded 3800 chars" CssClass="errlabels" SetFocusOnError="true" ValidationGroup="addedit" ></asp:RegularExpressionValidator></td></tr>
                                                    <tr><td> </td></tr>
                                                    <tr><td><span class="formattext">(max. 3800 chars)</span></td></tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                        
                                        
                                    </div>
                                </td>
                            </tr>
                            <tr class="constantheight">
                                <td class="tdsubtitle" valign="top"><span id="SpanFrequency" class="spanred" runat="server">*</span><asp:Label ID="LblFrequency" runat="server" Text="Frequency:"></asp:Label></td>
                                <td></td>
                                <td class="style2" valign="top">
                                    <div id="DivFrequencyView" runat="server">
                                        <asp:Label ID="LblFrequencyVal" runat="server"></asp:Label>
                                    </div>
                                    <div id="DivFrequency" runat="server">
                                        <asp:DropDownList ID="DdlFrequency" runat="server" DataSourceID="SDSFrequency" 
                                            DataTextField="LOOKUP_DESC" DataValueField="LOOKUP_ID" 
                                            ondatabound="DdlFrequency_DataBound">
                                        
                                        </asp:DropDownList>
                                         &nbsp;<asp:CustomValidator ID="CvFrequency" runat="server" EnableClientScript="true"  Display="Dynamic"
                                             ClientValidationFunction="DropdownValidation" ControlToValidate="DdlFrequency" ErrorMessage="Frequency is required" SetFocusOnError="true" CssClass="errlabels" ValidationGroup="addedit"></asp:CustomValidator>
                                           <asp:SqlDataSource ID="SDSFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:SLAC_WEB %>" 
                                        ProviderName="System.Data.OracleClient"  SelectCommand="SELECT LOOKUP_ID,LOOKUP_DESC FROM CMS_LOOKUP WHERE IS_ACTIVE='Y' AND LOOKUP_GROUP='Frequency' ORDER BY DISPLAY_ORDER"></asp:SqlDataSource>
                                    </div>
                                </td>         
                            </tr> 
                              <tr class="constantheight">
                            <td  class="tdsubtitle" valign="top"><asp:Label ID="LblStartDate" runat="server" Text="Start Date:"></asp:Label></td>
                            <td>&nbsp;</td>
                            <td class="style2" valign="top">
                                <div id="DivStartDateView" runat="server" visible="false"><asp:Label ID="LblStartDateVal" runat="server" ></asp:Label></div>
                                <div id="DivStartDate" runat="server">
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="TxtStartDate" runat="server"></asp:TextBox><span id="spnFormat" class="formattext">(mm/dd/yyyy)</span></td>
                                            <td></td>
                                            <td>
                                                <table>
                                                     <tr><td><asp:CompareValidator ID="cvStartDate" runat="server" Type="Date" Operator="DataTypeCheck"
                                                                ControlToValidate="TxtStartDate" ErrorMessage="Not a valid date!" CssClass="errlabels" ValidationGroup="addedit"></asp:CompareValidator>
                                                    </td></tr>
                                                    <tr><td> </td></tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>    
                                    
                                   
                                    
                                </div>
                            </td>
                        </tr>
                         <tr class="constantheight">
                            <td  class="tdsubtitle" valign="top"><asp:Label ID="LblUploadReq" runat="server" Text="Upload File Required?"></asp:Label></td>
                            <td>&nbsp;</td>
                            <td class="style2" valign="top">
                                <div id="DivUploadReqView" runat="server" visible="false"><asp:Label ID="LblUploadReqVal" runat="server" ></asp:Label></div>
                                <div id="DivUploadReq" runat="server"><asp:CheckBox id="ChkUploadReq" runat="server" /></div>
                            </td>
                        </tr>
                        
                         <tr id="trCMNotify" runat="server" visible="false" class="constantheight">
                                <td class="tdsubtitle" valign="top"><asp:Label ID="LblCMNotify" runat="server" Text="Is CM Notified?"></asp:Label></td>
                                <td>&nbsp;</td>
                                <td class="style2" valign="top"><asp:Label ID="LblCMNotifyVal" runat="server"></asp:Label></td>
                         </tr>
                         <tr id="trNotifiedDate" runat="server" visible="false" class="constantheight">
                                <td class="tdsubtitle" valign="top"><asp:Label ID="LblNotifiedDate" runat="server" Text="Notified Date:"></asp:Label></td>
                                <td>&nbsp;</td>
                                <td class="style2" valign="top"><asp:Label ID="LblNotifiedDateVal" runat="server"></asp:Label></td>
                         </tr>
                     
                            <tr style="height:20px;">
                                <td></td>
                            </tr>
                            <tr>
                           
                            <td colspan="3" align="center">
                                <div id="DivReview" runat="server" >
                                    <asp:Button ID="BtnReview" runat="server" 
                                        Text="Review" onclick="BtnReview_Click"  ValidationGroup="addedit"/></div>   
                                <div id="DivReviewedit" runat="server">
                                    <asp:Button ID="BtnReview2" runat="server" 
                                        Text="Review" onclick="BtnReview2_Click"  ValidationGroup="addedit"/></div>             
                            </td>
                        </tr>
                        <tr>
                            
                            <td  colspan="3" align="center" >
                                <div id="DivSubmit" runat="server"  visible="false"><asp:Button ID="BtnSave" runat="server" 
                                        Text="Submit" onclick="BtnSave_Click"  CausesValidation="false" ToolTip="Submit Requirement and Stay on Requirement Screen"/>
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnChange" runat="server" 
                                        Text="Make Changes" onclick="BtnChange_Click"  CausesValidation="false" ToolTip = "Go Back and Make changes"/>
                                         &nbsp;&nbsp;<asp:Button ID="BtnSaveAddDeli" runat="server" 
                                        Text="Add Deliverable" onclick="BtnSaveAddDeli_Click"  CausesValidation="false" ToolTip="Submit and go to Add Deliverable Screen"/>
                                         </div>
                                 <div id="DivUpdate" runat="server" visible="false">
                                     <asp:Button ID="BtnUpdate" runat="server" Text="Update" 
                                         onclick="BtnUpdate_Click"  CausesValidation="false"/>
                                 &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnCancel" runat="server" 
                                         Text="Cancel Changes" onclick="BtnCancel_Click"  CausesValidation="false"/>
                                  &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnMoreChange" runat="server" 
                                         Text="Make more Changes" onclick="BtnMoreChange_Click"  CausesValidation="false"/>
                                          &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnUpdateAddDeli" runat="server" 
                                         Text="Add Deliverable"  CausesValidation="false" 
                                         onclick="BtnUpdateAddDeli_Click"/>
                                </div>
                            </td>
                        </tr>
                              
                          
                        
                        </table>
                </td>
            </tr>
        </table>
      
   
    
    
    </div>

    <asp:HiddenField ID ="HdnMode" runat="server" />
    <asp:HiddenField ID ="HdnClauseid" runat="server" />
    <asp:HiddenField ID ="HdnReqid" runat="server" />
    <asp:HiddenField ID="HdnFilter" runat="server" />
    <asp:HiddenField ID="HdnDesc" runat="server" />
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
