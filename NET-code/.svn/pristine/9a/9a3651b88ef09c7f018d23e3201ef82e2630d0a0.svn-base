 <%@ Page Title="Deliverable" Language="C#" MasterPageFile="~/Site.master"  AutoEventWireup="true" CodeBehind="Deliverable.aspx.cs" Inherits="ContractManagement.About" %>
<%@ Register Src="~/User Controls/DynamicTextBox.ascx" TagName="DynamicTB" TagPrefix="uc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
 
      <link href="css/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />
      <link href="Styles/CMS.css" rel="Stylesheet" type="text/css" />
 

      <style type="text/css">
      
      .ui-datepicker-trigger {
                               margin-left:5px;
                               margin-top: 8px;
                               margin-bottom: -3px;
                              }

          .style1
          {
              width: 129px;
          }

     

      </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:ScriptManager ID="smDeliv" runat="server" EnablePartialRendering="true">
        <Scripts>
            <asp:ScriptReference Path="~/Scripts/jquery-ui-1.8.21.custom.min.js" ScriptMode="Release" />
          </Scripts>
    </asp:ScriptManager>
    <script type="text/javascript">
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);

        function BeginRequestHandler(sender, args) {
            xPos = $get('<%= Panelpb1.ClientID %>').scrollLeft;
            yPos = $get('<%= Panelpb1.ClientID %>').scrollTop;
        }
        function EndRequestHandler(sender, args) {
            $get('<%= Panelpb1.ClientID %>').scrollLeft = xPos;
            $get('<%= Panelpb1.ClientID %>').scrollTop = yPos;
            $("#dialogowner").dialog(
             {
                 minWidth: 500,
                 minHeight: 200,
                 autoOpen: false,
                 modal: true
             });
             $("#dialogfile").dialog(
             {
                 minWidth: 500,
                 minHeight: 400,
                 autoOpen: false,
                 modal: true
             });
             $("#dialog-confirm").dialog(
             {
                 minWidth: 300,
                 minHeight: 250,
                 autoOpen: false,
                 modal: true
             });
              $("#dialogwarn").dialog(
             {
                 minWidth: 400,
                 minHeight: 250,
                 autoOpen: false,
                 modal: true
             });
               $("#dialogconfirmfile").dialog(
             {
                 minWidth: 400,
                 minHeight: 250,
                 autoOpen: false,
                 modal: true
             });
             $("#dialogconfirmemail").dialog(
             {
                 minWidth: 400,
                 minHeight: 250,
                 autoOpen: false,
                 modal: true
             });
              $("#dialogconfirmemailupdate").dialog(
             {
                 minWidth: 400,
                 minHeight: 250,
                 autoOpen: false,
                 modal: true
             });
         
 
         }

        
        
         $(function () {
            $("#dialog-confirm").dialog({
                autoOpen: false,
                modal: true,
                 height: 250,
                 width: 350,
                buttons: {
                    "Yes": function (e) {
                        $(this).dialog("close");
                         $("#dialogwarn").dialog('open');
                    },
                    "No": function () {
                        $(this).dialog("close");
                        return false;
                    }
                }
            });

           
        });

       

        $(function () {
            $("#dialogwarn").dialog({
                autoOpen: false,
                modal: true,
                 height: 250,
                 width: 400,
                buttons: {
                    "Yes": function (e) {
                        $(this).dialog("close");
                         <%= ClientScript.GetPostBackEventReference(new PostBackOptions(this.BtnSubmitDeli))%>;                                                           
                    },
                    "No": function () {
                        $(this).dialog("close");
                        return false;
                    }
                }
            });

           
        });

      
         $(function () {
            $("#dialogconfirmfile").dialog({
                autoOpen: false,
                modal: true,
                 height: 350,
                 width: 350,
                buttons: {
                    "Yes": function (e) {
                        $(this).dialog("close");
                        var _delid = $('#<%= HdnDeliverableId.ClientID %>').val();
                         OpenJQueryFileDialog('dialogfile',_delid);
                    },
                    "No": function () {
                        $(this).dialog("close");
                        return false;
                    }
                }
            });

           
        });

       $(function() {
             $("#dialogconfirmemail").dialog({
                autoOpen: false,
                modal: true,
                 height: 250,
                 width: 350,
                buttons: {
                    "Yes": function (e) {
                        $('#<%= HdnEmail.ClientID %>').val("yes");
                        $(this).dialog("close");
                        
                         <%= ClientScript.GetPostBackEventReference(new PostBackOptions(this.BtnSave))%>;
                        },
                    "No": function () {
                         $('#<%= HdnEmail.ClientID %>').val("no");
                        $(this).dialog("close");
                         <%= ClientScript.GetPostBackEventReference(new PostBackOptions(this.BtnSave))%>;
                    }
                }
            });  
       });

        $(function() {
             $("#dialogconfirmemailupdate").dialog({
                autoOpen: false,
                modal: true,
                 height: 250,
                 width: 350,
                buttons: {
                    "Yes": function (e) {
                        $('#<%= HdnEmail.ClientID %>').val("yes");
                        $(this).dialog("close");
                        <%= ClientScript.GetPostBackEventReference(new PostBackOptions(this.BtnUpdate))%>;
                         
                        },
                    "No": function () {
                         $('#<%= HdnEmail.ClientID %>').val("no");
                        $(this).dialog("close");
                         <%= ClientScript.GetPostBackEventReference(new PostBackOptions(this.BtnUpdate))%>;
                    }
                }
            });  
       });

      

        function pageLoad() {
         
            $("#<%=TxtDueDate.ClientID%>").datepicker({
                showOn: 'button', buttonText: 'Choose Date', buttonImage: 'Images/cal.gif', buttonImageOnly: true, changeMonth: true, changeYear: true
            });
        }
        function subownervisibilty() {
            $('#MainContent_DivSubOwnerView').hide();
            $('#MainContent_DivSubOwnerAdd').show();
        }



        function RefreshGrid() {

            window.location.reload();
            $("#dialogfile").dialog("close");

        }

        function RedirectHome() {
         window.location.reload();
            //alert("Your document has been added successfully and made available to the owner");
           // window.location.href = 'Default.aspx';
        }

        function ResetDrtDept() {
           
           $('#DdlDirectorate').trigger('change');
           
        }



        function DisableSubmit(mode) {

            if (mode == "add") {
                $("#BtnSave").attr('disabled', 'true');
                $("#BtnChange").attr('disabled', 'true');
            }
            else if (mode == "edit") {
                $("#BtnUpdate").attr('disabled','true');
                $("#BtnCancel").attr('disabled', 'true');
                $("#BtnMoreChange").attr('disabled', 'true');

            }

        }

        function textboxMultilineMaxNumber(txt, maxLen) {
            if (txt.value.length > (maxLen - 1)) return false;
        }

        function DropdownValidation(source, arguments) {
            if (arguments.Value != "0") {
                if ((arguments.Value != "-1") || (arguments.Value != "")) {
                    arguments.IsValid = true;
                }
                else { arguments.IsValid = false; }
            }
            else { arguments.IsValid = false; }
           
        }


        function RequireDesc(source, arguments) {
          
                 var dd = document.getElementById('DdlType');
                 var tb = document.getElementById('TxtDescription');
                 if (dd != null) {
                     var sel = dd.selectedIndex;
                     if (sel != "0") {
                         var seltext = dd.options[sel].value;
                         if ((seltext == "4") || (seltext == "5")) {
                             if (tb.value.length <= 0) {
                                 arguments.IsValid = false;

                             } else { arguments.IsValid = true; }
                         }
                     } else { arguments.IsValid = true; }
                 }
                 else { arguments.IsValid = true; }
            }

             function OpenDialogForReject() {
            $('#rejectdialog').dialog({ autoOpen: false, bgiframe: true, modal: true,title: "Reject Reason", width:500, height:400 });
            $('#rejectdialog').dialog('open');
            $('#rejectdialog').parent().appendTo($("form:first"))

            return false;
        }

        function CloseRejectDialog() {
          
            $('#rejectdialog').dialog('close');

            return false;
        }

     

    
      
                  
       

    </script>
    
        <script type="text/javascript" src="Scripts/Jquery_common.js"></script>		

 
    <asp:UpdatePanel ID="UPDeliv" runat="server" RenderMode="Inline">
    <ContentTemplate>
        <asp:Panel ID="Panelpb1" runat="server">
          <div>
        <table width="100%">
             <tr>
                <td>  <h2> <asp:Label ID="LblSubTitle" runat="Server"></asp:Label> </h2></td>
                <td align="right"> 
                <table>
                    <tr>
                        <td> <div id="DivEdit" runat="server" visible="false"> <asp:Button  ID="BtnEdit" runat="server"  Text="Edit" onclick="BtnEdit_Click" Width="129px"/> </div></td>
                        <td><div id="DivDelete" runat="server" visible="false"><asp:Button ID="BtnDelete" 
                                runat="server" Text="Delete"   Width="129px" onclick="BtnDelete_Click" OnClientClick="return confirm('Are you certain you want to delete this Deliverable?');" CausesValidation="false"/></div></td>
                        <td> <div id="DivClone" runat="server" visible="false"><asp:Button ID="BtnClone" 
                                runat="server" Text="Clone"  Width="129px" onclick="BtnClone_Click"/></div>    </td>
                         <td><div id="DivApprove" runat="server" visible="false">
                             <asp:Button ID="BtnApprove"
                         runat="server" Text="Approve" Width="129px" onclick="BtnApprove_Click" /></div></td>
                          <td><div id="DivReject" runat="server" visible="false"><asp:Button ID="BtnReject"
                         runat="server" Text="Reject" Width="129px" /></div></td>
                    </tr>
                </table>
               
                     
                 </td>
            </tr>
            <tr>
                <td colspan="2"><hr/></td>
            </tr>
            <tr>
                <asp:LinkButton ID="LnkBack" runat="server" Text="Back" onclick="LnkBack_Click"></asp:LinkButton>
            </tr>
        </table>     
     </div> 
   
     <div>
        <div id="dialogowner" style="display:none;" >             
                <iframe id="modaldialogowner" scrolling="no" frameborder="1" width="100%" height="100%">
                </iframe>
    </div>
    <div id="dialog-confirm" title="Are you Sure?" style="display:none;">
        <p style="color:Black;">This deliverable is not due until <%=Duedate %>. Are you sure you want to submit it now?</p>
    </div>
    <div id="dialogwarn" title="Warning!" style="display:none;">
        <p style="color:Black;">Are you sure that you have reviewed all attachments and verified that this Deliverable is ready for final submittal?</p>
    </div>
    <div id="dialogconfirmfile" title="Are you Sure?" style="display:none;">
        <p style="color:Black;">You are attaching a document to a deliverable that is not due until <%=Duedate %>. Are you sure you want to continue?</p>
    </div>
    <div id="dialogconfirmemail" title="Send Email?" style="display:none;">
        <p style="color:Black;">Do you want to trigger email? </p>
    </div> 
     <div id="dialogconfirmemailupdate" title="Send Email?" style="display:none;">
        <p style="color:Black;">Do you want to send email? </p>
    </div>
     <div id="dialogconfirmemailapprove" title="Send Email?" style="display:none;">
        <p style="color:Black;">Do you want to send email? </p>
    </div>
     <div id="dialogconfirmemailreject" title="Send Email?" style="display:none;">
        <p style="color:Black;">Do you want to send email? </p>
    </div>
    <div id="dialogconfirmemailsubmitdeli" title="Send Email?" style="display:none;">
        <p style="color:Black;">Do you want to send email? </p>
    </div>

   
    <div id="rejectdialog"  style="display:none;">             
               
                    <table>
                        <tr>
                            <td><asp:Label ID="LblInstruct" runat="server" Text="Please enter the reason for rejection"  ForeColor="Black"></asp:Label> <span class="formattext">(max. 480 chars)</span></td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Textbox  ID="TxtReason" TextMode="MultiLine" runat="server" Height="200px" Width="450px"></asp:Textbox> 
                            </td>
                          </tr>
                          <tr>
                            <td> 
                            <asp:RequiredFieldValidator ID="RfvReason" runat="server"  SetFocusOnError="true" ErrorMessage="Reason is required" CssClass="errlabels" ControlToValidate="TxtReason"  ValidationGroup="reject"></asp:RequiredFieldValidator>
                             &nbsp;&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TxtReason" ValidationExpression="(?:[\r\n]*.[\r\n]*){0,480}"  ErrorMessage="Exceeded 480 chars" CssClass="errlabels" SetFocusOnError="true" ValidationGroup="reject"></asp:RegularExpressionValidator>
                               </td>
                          </tr>
                         <tr>
                          <td>&nbsp;</td>
                         </tr>
                        <tr>
                            <td align = "center">
                                <asp:Button ID="BtnReject1" runat="server" Text="Reject"  ClientIDMode="Static" 
                                    CausesValidation="true"  ValidationGroup="reject" 
                                    onclick="BtnReject1_Click" />
                                &nbsp;&nbsp;
                                <asp:Button ID="BtnCancel1" runat="server" Text="Cancel"  ClientIDMode="Static" 
                                    CausesValidation="false" onclick="BtnCancel1_Click"/>
                            </td>
                          </tr>                    
                    </table>              
               </div>   

        <table>
            <tr>
                <td style="width:15%;"> </td>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="right"><div id="Divreq" runat="server"><span  class="spanrequired"><span  class ="spanred">*</span> Required Fields </span></div> </td>
                            <td></td>
                            <td>                                   
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr id="trId" runat="server" class="constantheight"  visible="false">
                            <td class="tdsubtitle" valign ="top"><asp:Label ID="LblId" runat="server" Text="Deliverable Id:"></asp:Label></td>
                            <td></td>
                            <td valign ="top">
                                <div id="DivIdView" runat="server"><asp:Label ID="LblIdVal" runat="server"></asp:Label></div>
                           </td>                            
                        </tr>
                        <tr id="trTrack" runat="server" class="constantheight" visible="false">
                            <td  class="tdsubtitle" valign ="top"><asp:Label ID="LblTrackId" runat="server" Text="Track Id:"></asp:Label></td>
                            <td>&nbsp;</td>
                            <td valign ="top">
                                <div id="DivTrackIdView" runat="server"><asp:Label ID="LblTrackIdVal" runat="server" ></asp:Label></div>
                            </td>
                        </tr>
                        <tr class="constantheight">
                            <td  class="tdsubtitle" valign ="top"><span id="SpnType" class ="spanred" runat="server">*</span><asp:Label ID="LblType" runat="server" Text="Type:"></asp:Label></td>
                            <td>&nbsp;</td>
                            <td valign ="top">
                                <div id="DivTypeView" runat="server"  visible="false"><asp:Label ID="LblTypeVal" runat="server"></asp:Label></div> 
                                <div id="DivTypeAdd" runat="server">
                                <table>
                                    <tr>
                                        <td> <asp:DropDownList ID="DdlType" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="DdlType_SelectedIndexChanged" ClientIDMode="Static">                                       
                                    </asp:DropDownList></td>
                                        <td>
                                             &nbsp;<asp:CustomValidator ID="CvType" runat="server" EnableClientScript="true"  Display="Dynamic"
                                             ClientValidationFunction="DropdownValidation" ControlToValidate="DdlType" ErrorMessage="Type is required" SetFocusOnError="true" CssClass="errlabels" ValidationGroup="addedit"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                </table>
                                   
                                   
                                 </div>                           
                            </td>                        
                        </tr>
                        <tr id="trClauseDetails" runat="server" visible="false"  class="constantheight">
                            <td class="tdsubtitle" valign ="top"><asp:Label ID="LblClause" runat="server" Text="Clause:"></asp:Label></td>
                            <td></td>
                            <td valign ="top">
                                <div id="DivClauseView" runat="server">
                                    <asp:Label ID="LblClauseVal" runat="server"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr id="trRequirement" runat="server" visible="true" class="constantheight">
                            <td  class="tdsubtitle" valign="top"><span id="SpnReq" class ="spanred" runat="server">*</span><asp:Label ID="LblRequirement" runat="server" Text="Requirement:"></asp:Label></td>
                            <td>&nbsp;</td>
                            <td valign="top">
                                <div id="DivReqView" runat="server" ><asp:Label ID="LblRequirementVal" runat="server"></asp:Label></div>
                                <div id="DivReqAdd" runat="server">
                                    <asp:DropDownList ID="DdlRequirement" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="DdlRequirement_SelectedIndexChanged" >                           
                                    </asp:DropDownList>
                                      &nbsp;<asp:CustomValidator ID="cvRequirment" runat="server" EnableClientScript="true" Display="Dynamic"
                                         ClientValidationFunction="DropdownValidation" ControlToValidate="DdlRequirement" ErrorMessage="Requirement is required" SetFocusOnError="true" CssClass="errlabels" ValidationGroup="addedit"></asp:CustomValidator>
                                  
                                </div>
                            </td>
                        </tr> 
                        <tr class="constantheight" id="trFrequency"  runat="server">
                            <td class="tdsubtitle" valign ="top"><span id="SpnDeliFreq" class ="spanred" runat="server">*</span><asp:Label ID="LblFrequency" runat="server" Text="Frequency:"></asp:Label></td>
                            <td>&nbsp;</td>
                            <td valign ="top">
                                <div id="DivDeliFreqView" runat="server"> <asp:Label ID="LblFrequencyVal" runat="server"></asp:Label> </div>
                                <div id="DivDeliFreqAdd" runat="server">
                                    <asp:DropDownList ID="DdlDeliFrequency" runat="server"  DataSourceID="SDSFrequency" 
                                            DataTextField="LOOKUP_DESC" DataValueField="LOOKUP_ID" 
                                            ondatabound ="DdlDeliFrequency_DataBound"></asp:DropDownList>
                                    &nbsp; <asp:CustomValidator ID="CvDeliFreq" runat="server" EnableClientScript="true" Display="Dynamic"
                                             ClientValidationFunction="DropdownValidation"  ControlToValidate="DdlDeliFrequency" ErrorMessage="Frequency is required" SetFocusOnError="true" CssClass="errlabels" ValidationGroup="addedit"></asp:CustomValidator>
                                    <asp:SqlDataSource ID="SDSFrequency" runat="server" ConnectionString="<%$ ConnectionStrings:SLAC_WEB %>" 
                                        ProviderName="System.Data.OracleClient"  SelectCommand="SELECT LOOKUP_ID,LOOKUP_DESC FROM CMS_LOOKUP WHERE IS_ACTIVE='Y' AND LOOKUP_GROUP='Frequency' AND LOOKUP_ID <> 16  ORDER BY DISPLAY_ORDER"></asp:SqlDataSource>
                                </div>
                               </td>
                        </tr>
                        <tr class="constantheight">
                            <td  class="tdsubtitle" valign ="top"><span id="SpnDesc" class ="spanred" runat="server" visible="false">*</span><asp:Label ID="LblDescDeliverable" runat="server" Text="Deliverable:"></asp:Label></td>
                            <td>&nbsp;</td>
                            <td valign ="top">
                                <div id="DivDescView" runat="server" visible="false" >
                                    <asp:Label ID="LblDescriptionVal" runat="server" 
                                        onprerender="LblDescriptionVal_PreRender" ></asp:Label></div>
                                <div id="DivDescAdd" runat="server">
                                <table>
                                    <tr>
                                        <td><asp:TextBox ID="TxtDescription" runat="server" TextMode="MultiLine"  CssClass="txtmulti"  ClientIDMode="Static" onkeypress="return textboxMultilineMaxNumber(this,3800)"></asp:TextBox></td>
                                        <td>
                                            <table>
                                                <tr><td> <asp:RegularExpressionValidator ID="RegExDesc" runat="server" ControlToValidate="TxtDescription" ValidationExpression="(?:[\r\n]*.[\r\n]*){0,3800}"  ErrorMessage="Exceeded 3800 chars" CssClass="errlabels" SetFocusOnError="true" ValidationGroup="addedit" ></asp:RegularExpressionValidator></td></tr>
                                                
                                                <tr><td><asp:customvalidator id="cvDesc" runat="server" EnableClientScript="true"  Display="Dynamic"
													    ClientValidationFunction="RequireDesc"   ValidateEmptyText="true" ControlToValidate ="TxtDescription" ErrorMessage="Deliverable required"  CssClass="errlabels" ValidationGroup="addedit"></asp:customvalidator>									 
                                                </td></tr>
                                                <tr><td><span class="formattext">(max. 3800 chars)</span></td></tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                    
                                 
                                  
                                </div>
                            </td>
                        </tr>
                        <tr class="constantheight">
                            <!--  Defaulted to Owner from Requirement -->
                            <td  class="tdsubtitle" valign ="top"><span id="SpnOwn" class ="spanred" runat="server">*</span><asp:Label ID="LblOwner" runat="server" Text="Owner:"></asp:Label></td>
                            <td>&nbsp;</td>
                            <td valign ="top">
                                <div id="DivOwnerView" runat="server" visible="false"><asp:Label ID="LblOwnerVal" runat="server" ></asp:Label></div>
                                <div id="DivOwnerAdd" runat="server">
                                         <table>
                                        <tr>
                                            <td><asp:TextBox ID="TxtOwner"  runat="server" ClientIDMode="Static" MaxLength="30" 
                                                    CssClass="txtnamewidth" AutoPostBack="True" 
                                                    ontextchanged="TxtOwner_TextChanged"></asp:TextBox></td>
                                             <td>
                                             <asp:ImageButton ID="ImgbtnOwn" runat="server"  ImageUrl="~/Images/find.gif"/>
                                           
                                            </td>
                                            <td class="style1"><asp:Label ID="Lblformat" runat="server" CssClass="formattext" Text="(Lastname, firstname)"></asp:Label></td>
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td><asp:RequiredFieldValidator ID="RVOwn" runat="server" CssClass="errlabels" ControlToValidate="TxtOwner"  ErrorMessage="Owner is required" SetFocusOnError="true" ValidationGroup="addedit"></asp:RequiredFieldValidator></td>
                                                        <td><asp:CustomValidator ID="CvOwn"  CssClass="errlabels" OnServerValidate= "CvOwn_ServerValidate" runat="server" ControlToValidate= "TxtOwner" ErrorMessage="Not a Valid Name/Format" SetFocusOnError="true" ValidationGroup="addedit"></asp:CustomValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                   
                                   
                                   
                               
                            </td>
                        </tr> 
                        <tr class="constantheight" id="trDrt" runat="server">
                            <td  class="tdsubtitle" valign ="top"><span id="SpnDrt" class ="spanred" runat="server">*</span><asp:Label ID="LblDirectorate" runat="server" Text="Directorate:"></asp:Label></td>
                            <td></td>
                            <td valign ="top">
                                <div id="DivDirectorateView" runat="server" visible="false"><asp:Label ID="LblDirectorateVal" runat="server" ></asp:Label></div>
                                <div id="DivDirectorateAdd" runat="server" >
                                <table>
                                    <tr>
                                        <td> <asp:DropDownList ID="DdlDirectorate" runat="server" ClientIDMode="Static" onselectedindexchanged="DdlDirectorate_SelectedIndexChanged" AutoPostBack="True">                                     
                                                </asp:DropDownList></td>
                                         <td>
                                                 &nbsp;<asp:CustomValidator ID="CvDirectorate" runat="server" EnableClientScript="true" Display="Dynamic"
                                                        ClientValidationFunction="DropdownValidation" ControlToValidate="DdlDirectorate" ErrorMessage="Directorate is required" SetFocusOnError="true" CssClass="errlabels" ValidationGroup="addedit"></asp:CustomValidator>
                                         </td>
                                    </tr>
                                </table>
                                   
                                   
                                </div>
                            </td>
                        </tr>
                        <tr class="constantheight" id="trDept" runat="server">
                            <td  class="tdsubtitle" valign ="top" ><span id="SpnDept" class ="spanred" runat="server">*</span><asp:Label ID="LblDepartment" runat="server" Text="Department:"></asp:Label></td>
                            <td></td>
                            <td valign ="top">
                                <div id="DivDepartmentView" runat="server" visible="false"><asp:Label ID="LblDepartmentVal" runat="server"></asp:Label></div>
                                <div id="DivDepartmentAdd" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                 <asp:DropDownList ID="DdlDepartment" runat="server">                                
                                    </asp:DropDownList>
                                            </td>
                                            <td>
                                             &nbsp;<asp:CustomValidator ID="CvDept" runat="server" EnableClientScript="true" Display="Dynamic"
                                         ClientValidationFunction="DropdownValidation" ControlToValidate="DdlDepartment" ErrorMessage="Department is required" SetFocusOnError="true" CssClass="errlabels" ValidationGroup="addedit"></asp:CustomValidator>  
                                            </td>
                                        </tr>
                                    </table>
                                   
                                    
                                </div>
                             </td>
                        </tr>          
                        <tr class="constantheight">
                            <td  class="tdsubtitle" valign ="top"><asp:Label ID="LblSubOwner" runat="server" Text="Sub Owner:"></asp:Label></td>
                            <td>&nbsp;</td>
                            <td valign ="top">
                                <div id="DivSubOwnerView" runat="server" visible="false">
                                    <table>
                                        <tr>
                                            <td><asp:Label ID="LblSubOwnerVal" runat="server" ></asp:Label></td>
                                            <td>&nbsp;</td>
                                            <td valign="top"><asp:LinkButton ID="LnkEdit" runat="server" Text="Edit/Add" 
                                                    onclick="LnkEdit_Click" Visible="false"></asp:LinkButton></td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="DivSubOwnerAdd" runat="server">
                                    <table>
                                        <tr>
                                            <td> <div><uc1:DynamicTB ID="TB1" runat="server" /></div>
                                                
                                            </td>
                                            
                                            <td valign="top">
                                                <asp:LinkButton ID="LnkUpdate" runat="server" Text="Update" 
                                                    onclick="LnkUpdate_Click" Visible="false"></asp:LinkButton>
                                                &nbsp;&nbsp;&nbsp;
                                                <asp:LinkButton ID="LnkCancel" runat="server" Text="Cancel" 
                                                    onclick="LnkCancel_Click" Visible="false">
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                   
                                </div>
                            </td>
                        </tr>
                      
                        <tr class="constantheight">
                            <td  class="tdsubtitle" valign ="top"><span id="SpnDuedate" class ="spanred" runat="server">*</span><asp:Label ID="LblDueDate" runat="server" Text="Due Date:"></asp:Label></td>
                            <td>&nbsp;</td>
                            <td valign ="top">
                                <div id="DivDueDateView" runat="server" visible="false"><asp:Label ID="LblDueDateVal" runat="server" ></asp:Label></div>
                                <div id="DivDueDateAdd" runat="server">
                                    <table>
                                        <tr>
                                            <td><asp:TextBox ID="TxtDueDate" runat="server"></asp:TextBox><span id="spnFormat" class="formattext">(mm/dd/yyyy)</span></td>
                                            <td><asp:RequiredFieldValidator ID="RvDuedate" runat="server" ControlToValidate="TxtDueDate"  ErrorMessage="Due Date is required" ValidationGroup="addedit" CssClass="errlabels"></asp:RequiredFieldValidator></td>
                                            <td>
                                                <table>
                                                     <tr><td><asp:CompareValidator ID="cvDueDate" runat="server" Type="Date" Operator="DataTypeCheck"
                                                                ControlToValidate="TxtDueDate" ErrorMessage="Not a valid date!" CssClass="errlabels" ValidationGroup="addedit"></asp:CompareValidator>
                                                    </td></tr>
                                                   
                                                </table>
                                            </td>
                                        </tr>
                                    </table>    
                                    
                                   
                                    
                                </div>
                            </td>
                        </tr>
                        
                        <tr class="constantheight" id="trNotify" runat="server">
                        
                            <td class="tdsubtitle" valign ="top"><asp:Label ID="LblNotifySchedule" runat="server" Text="Notification Schedule:"></asp:Label>
                            <br /> <span id="Spnsched" runat="server" class="formattext">  (Hold down the CTRL key to
													<br />
													select multiple items)</span></td>
                            <td>&nbsp;</td>
                            <td valign ="top">
                                <div id="DivNotifyScheduleView" runat="server" visible="false"><asp:Label ID="LblNotifySchedVal" runat="server" ></asp:Label></div>
                                <div id="DivNotifyScheduleAdd" runat="server">
                                    <asp:ListBox ID="LstNotify" runat="server" SelectionMode="Multiple" 
                                        Height="95px" Width="175px">
                                     
                                    </asp:ListBox>
                                    <asp:CheckBox ID="ChkDeselect" runat="server" Text="Deselect All" 
                                        AutoPostBack="True" oncheckedchanged="ChkDeselect_CheckedChanged" />
                                </div>
                            </td>
                        </tr>
                        <tr class="constantheight" id="trNotifyMgr" runat="server">
                            <td  class="tdsubtitle" valign ="top">Notify Manager on 1 day Reminder?</td>
                            <td>&nbsp;</td>
                            <td valign ="top">
                                <div id="DivNotifyMgrView" runat="server" display="false"><asp:Label ID="LblNotifyMgrVal" runat="server" ></asp:Label></div>
                                <div id="DivNotifyMgrAdd" runat="server" ><asp:CheckBox id="ChkNotifyMgr" runat="server" Checked="true" /></div>
                            </td>
                        </tr>
                        <tr class="constantheight">
                         <!-- From User list table of type SSO - allow multiselct -->
                            <td  class="tdsubtitle" valign ="top"><span id="SpnAppr" class ="spanred" runat="server">*</span><asp:Label ID="LblApprovers" runat="server" Text="Recipients / Approvers:"></asp:Label>
                             <br /> <span id="SpnReceipt" runat="server" class="formattext">  (Hold down the CTRL key to
													<br />
													select multiple items)</span></td>
                            <td>&nbsp;</td>
                            <td valign ="top">
                                <div id="DivApproversView" runat="server" visible="false"><asp:Label ID="LblApproverVal" runat="server"></asp:Label>
                                </div>
                                <div id="DivApproversAdd" runat="server" >
                                    <asp:ListBox ID="LstApprovers" runat="server" SelectionMode="Multiple" 
                                        Height="86px" Width="174px">
                                     
                                    </asp:ListBox>
                                   <asp:RequiredFieldValidator ID="RVList" runat="server" ErrorMessage="Only Approvers (atleast one) must be selected" ControlToValidate="LstApprovers"  CssClass="errlabels" InitialValue="0" ValidationGroup="addedit"></asp:RequiredFieldValidator>
                                    
                                </div>
                            </td>
                        </tr>
                         <tr class="constantheight" id="trUploadReq" runat="server">
                            <td  class="tdsubtitle" valign ="top"><asp:Label ID="LblUploadReq" runat="server" Text="Upload File Required?"></asp:Label></td>
                            <td>&nbsp;</td>
                            <td valign ="top">
                                <div id="DivUploadReqView" runat="server" visible="false"><asp:Label ID="LblUploadReqVal" runat="server" ></asp:Label></div>
                                <div id="DivUploadReqAdd" runat="server"><asp:CheckBox id="ChkUploadReq" runat="server" /></div>
                            </td>
                        </tr>
                        <tr class="constantheight">
                             <td class="tdsubtitle" valign ="top"><asp:Label ID="LblInfoOnly" runat="server" Text="Is for Information Only?"></asp:Label></td>
                            <td>&nbsp;</td>
                            <td valign ="top">
                                <div id="DivInfoOnlyView" runat="server" visible="false"><asp:Label ID="LblInfoOnlyVal" runat="server" ></asp:Label></div>
                                <div id="DivInfoOnlyAdd" runat="server"><asp:CheckBox id="ChkInfoOnly" runat="server" /></div>
                            </td>
                         </tr>
                       
                          <tr id="trStatus" runat="server"  style="height:30px;" visible="false">
                            <td class="tdsubtitle" valign ="top"><asp:Label ID="LblStatus" runat="server" Text="Status:"></asp:Label></td>
                            <td></td>
                            <td valign ="top">
                                <div id="DivStatusView" runat="server"><asp:Label ID="LblStatusVal" runat="server"></asp:Label></div>
                           </td>                            
                        </tr>             
                         <tr id="trDateSubmit" runat="server"  style="height:30px;" visible="false">
                            <td class="tdsubtitle" valign ="top"><asp:Label ID="LblDateSubmit" runat="server" Text="Date Submitted:"></asp:Label></td>
                            <td></td>
                            <td valign ="top">
                                <div id="DivDateSubmitView" runat="server"><asp:Label ID="LblDateSubmitVal" runat="server"></asp:Label></div>
                           </td>                            
                        </tr>
                         <tr id="trDateApproved" runat="server"  style="height:30px;" visible="false">
                            <td class="tdsubtitle" valign ="top"><asp:Label ID="LblDateApproved" runat="server" Text="Date Approved:"></asp:Label></td>
                            <td></td>
                            <td valign ="top">
                                <div id="DivDateApprView" runat="server"><asp:Label ID="LblDateApprovedVal" runat="server"></asp:Label></div>
                           </td>                            
                        </tr>
                         <tr id="trRejectReason" runat="server"  style="height:30px;" visible="false">
                            <td class="tdsubtitle" valign ="top"><asp:Label ID="LblReasonReject" runat="server" Text="Reason for Rejection:"></asp:Label></td>
                            <td></td>
                            <td valign ="top">
                                <div id="DivRejectReason" runat="server"><asp:Label ID="LblReasonRejectVal" runat="server"></asp:Label></div>
                           </td>                            
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                <div id="DivSubmitDeli" runat="server" visible="false"> 
                            <asp:Button ID="BtnSubmitDeli" runat="server" Text="Submit Final Deliverable" 
                                        Width="268px"  Font-Bold="true" ForeColor="Red"
                                onclick="BtnSubmitDeli_Click" /> </div>
                                <br />
                                <div id="DivNotifyOwners" runat="server" visible="false">
                                    <asp:Button ID="BtnNotifyOwners" runat="server" 
                                        Text="Notify Owner that this item is ready for their review and submittal" 
                                        onclick="BtnNotifyOwners_Click" />
                                </div>
                            </td>
                        </tr>
                         <tr class="constantheight">
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
                            <td colspan="3" align="center" >
                                <div id="DivSubmit" runat="server"  visible="false"><asp:Button ID="BtnSave" runat="server" 
                                        Text="Submit" onclick="BtnSave_Click"  CausesValidation="false"/>
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnChange" runat="server" 
                                        Text="Go Back and Make Changes" onclick="BtnChange_Click"  CausesValidation="false"/>
                                </div>
                                 <div id="DivUpdate" runat="server" visible="false">
                                     <asp:Button ID="BtnUpdate" runat="server" Text="Update" 
                                         onclick="BtnUpdate_Click"  CausesValidation="false"/>
                                 &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnCancel" runat="server" 
                                         Text="Cancel Changes" onclick="BtnCancel_Click"  CausesValidation="false" />
                                  &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="BtnMoreChange" runat="server" 
                                         Text="Make more Changes" onclick="BtnMoreChange_Click"  CausesValidation="false"/>
                                </div>
                            </td>
                        </tr>
                              
                          </table>     
                            </td>
                         </tr>

                    </table>
                
                
            
        <asp:HiddenField ID="HdnOwner" runat="server" />  
        <asp:HiddenField ID="HdnDeliverableId" runat="server" />
        <asp:HiddenField ID="HdnTypeId" runat="server" />
        <asp:HiddenField id="HdnSOCount" runat="server" />
        <asp:HiddenField ID="HdnReqId" runat="server" />
        <asp:HiddenField ID="HdnEmail" runat="server" />
 
    </div>

     <div id="dialogfile" style="display:none;" >             
                <iframe id="modaldialogfile" scrolling="no" frameborder="1" width="100%" height="100%">
                </iframe>
    </div>
    <div id="divFile" runat="server" visible="false">  
    <table width="100%">
        <tr>
            <td>  <h2><asp:Label ID="LblFile" runat="server" Text= "Documents"></asp:Label></h2></td>
            <td align="right"> <span class="spanrequired" id="spnPdfmsg" runat="server">* PDFed files recommended</span> &nbsp;&nbsp;&nbsp;<asp:Button  
                    ID="BtnUpload" runat="server"  Text="Add a Document" 
                    onclick="BtnUpload_Click"/></td>
        </tr>
        <tr>
            <td colspan="2"><hr/></td>
        </tr>
  
            <tr>
                <td colspan="2">
                    <asp:updatePanel ID="updFile" runat="server">
                        <ContentTemplate>
                                   <asp:GridView ID="gvFile" runat="server" AutoGenerateColumns="false" 
                        EmptyDataText="No files are attached to this deliverable" 
                        onrowdatabound="gvFile_RowDataBound" Width="100%" 
                        onrowcommand="gvFile_RowCommand" onrowdeleting="gvFile_RowDeleting">
                        <columns>
                            <asp:BoundField DataField="ATTACHMENT_ID"  Visible="false"  HeaderStyle-HorizontalAlign="Left"/>
                            <asp:TemplateField HeaderText="File" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkDownload"  runat="server"  CommandArgument='<%# Eval("ATTACHMENT_ID") %>' CommandName="download" Text ='<%# Eval("FILE_NAME") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:BoundField DataField="UPLOADED_BY" HeaderText="Uploaded by"  HeaderStyle-HorizontalAlign="Left"/>
                            <asp:BoundField DataField="UPLOADED_ON" HeaderText="Uploaded On" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Left"/>
                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="Lbldelinfo" runat="server" Text="Not allowed"  Visible="false"></asp:Label>
                                    <asp:ImageButton ID="ImgBtnDelete" runat="server" ImageUrl="~/Images/deleteicon.gif"  CommandArgument='<%# Eval("ATTACHMENT_ID") %>' CommandName="delete" OnClientClick = "return confirm('Warning! Are you sure you want to delete this attachment?');"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </columns>
                       <HeaderStyle BackColor="LightGray" />
                    </asp:GridView>
                    
                        
                        </ContentTemplate>
                       <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="TxtOwner" EventName="TextChanged" />
                       </Triggers>
                    </asp:updatePanel>
                 
                </td>
            </tr>
   
    </table>
    </div>
          
           
        </asp:Panel>
    </ContentTemplate>  
      
    </asp:UpdatePanel>

    						 

   

</asp:Content>
