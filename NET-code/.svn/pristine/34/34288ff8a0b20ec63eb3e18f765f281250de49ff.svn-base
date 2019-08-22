<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReportCriteria.aspx.cs" Inherits="ContractManagement.ReportCriteria" %>
<asp:Content ID="ContentReportHead" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="css/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui-1.8.21.custom.min.js" type="text/javascript"></script>
<link href="Styles/CMS.css"  rel="Stylesheet" type="text/css"/>
<script type="text/javascript" src="Scripts/Jquery_common.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=TxtFromDueDate.ClientID%>").datepicker({
            showOn: 'button', buttonText: 'Choose Date', buttonImage: 'Images/cal.gif', buttonImageOnly: true, changeMonth: true, changeYear: true
        });
        $("#<%=TxtToDueDate.ClientID%>").datepicker({
            showOn: 'button', buttonText: 'Choose Date', buttonImage: 'Images/cal.gif', buttonImageOnly: true, changeMonth: true, changeYear: true
        });
        $("#<%=TxtFromSubmit.ClientID%>").datepicker({
            showOn: 'button', buttonText: 'Choose Date', buttonImage: 'Images/cal.gif', buttonImageOnly: true, changeMonth: true, changeYear: true
        });
        $("#<%=TxtToSubmit.ClientID%>").datepicker({
            showOn: 'button', buttonText: 'Choose Date', buttonImage: 'Images/cal.gif', buttonImageOnly: true, changeMonth: true, changeYear: true
        });
    });

    $(document).ready(function () {
        var id = "#<%=ChkLstType.ClientID %>_0";
        var checkboxlistid = "#<%=ChkLstType.ClientID %>";
        $(id).click(function () {
            $("#<%=ChkLstType.ClientID %> input:checkbox").attr('checked', this.checked);
        });
        $(checkboxlistid + " input:checkbox").click(function () {
            if ($(id).attr('checked') == true && this.checked == false) {
                $(id).attr('checked', false);
            }
            else
                CheckSelectAll();
        });

        var id1 = "#<%=ChkStatus.ClientID %>_0";
        var checkboxlistid1 = "#<%=ChkStatus.ClientID %>";
        $(id1).click(function () {
            $("#<%=ChkStatus.ClientID %> input:checkbox").attr('checked', this.checked);
        });
        $(checkboxlistid1 + " input:checkbox").click(function () {
            if ($(id1).attr('checked') == true && this.checked == false) {
                $(id1).attr('checked', false);
            }
            else
                CheckSelectAllstat();
        });

        function CheckSelectAll() {
            $(checkboxlistid + " input:checkbox").each(function () {
                var checkedcount = $(checkboxlistid + " input[type=checkbox]:checked").length;
                var checkcondition = $(checkboxlistid + " input[type=checkbox]:").length - 1
                if (checkedcount >= checkcondition)
                    $(id).attr('checked', true);
                else
                    $(id).attr('checked', false);
            });
        }

        function CheckSelectAllstat() {
            $(checkboxlistid1 + " input:checkbox").each(function () {
                var checkedcount1 = $(checkboxlistid1 + " input[type=checkbox]:checked").length;
                var checkcondition1 = $(checkboxlistid1 + " input[type=checkbox]:").length - 1
                if (checkedcount1 >= checkcondition1)
                    $(id1).attr('checked', true);
                else
                    $(id1).attr('checked', false);
            });
        }

    });

   
       
    
</script>
<style type="text/css">
    .ui-datepicker-trigger {
                               margin-left:5px;
                               margin-top: 8px;
                               margin-bottom: -3px;
                              }

  
    .style1
    {
        height: 43px;
    }

  
</style>
</asp:Content>
<asp:Content ID="ContentReport" ContentPlaceHolderID="MainContent" runat="server">
    <div id="dialogowner1" style="display:none;" >             
                <iframe id="modaldialogowner1" scrolling="no" frameborder="1" width="100%" height="100%">
                </iframe>
    </div>
    <div>
     <table width="100%">
        <tr>
            <td ><h2 style="font-variant:normal;"> <asp:Label ID="LblSubTitle" runat="Server" Text="Build your report from the following:" ></asp:Label></h2></td>
        </tr>
        <tr>
            <td><hr /></td>
        </tr>
    </table>
</div>
<p></p>
<div>
    <asp:Panel ID="PnlReprt" runat="server" DefaultButton="BtnSearch">
    <table width="100%">
        <tr class="constantheight">
            <td align="right">Track Id:</td>
            <td style="width:15px;">&nbsp;</td>
            <td colspan="3">
                <table>
                    <tr>
                        <td> Contains</td>
                        <td style="width:5px;"></td>
                        <td> <asp:TextBox ID="TxtTrackId" runat="server"></asp:TextBox></td>
                    </tr>
                </table>           
            </td>            
        </tr> 
        <tr id="trContract" runat = "server">
            <td align="right">Type:</td>
            <td></td>
            <td>
                <table>
                    <tr>
                        <td>
                            
                            <asp:CheckBoxList ID="ChkLstType" runat="server" DataTextField="CONTRACT_NAME" 
                                DataValueField="CONTRACT_ID" DataSourceID="SDSLstType" 
                                onselectedindexchanged="ChkLstType_SelectedIndexChanged" 
                                ondatabound="ChkLstType_DataBound"  >                    
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan="2">
                <asp:SqlDataSource ID="SDSLstType" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:SLAC_WEB %>" 
                    ProviderName="System.Data.OracleClient" 
                    SelectCommand="SELECT CONTRACT_ID,CONTRACT_NAME FROM CMS_CONTRACT_OTHERTYPES  WHERE IS_ACTIVE='Y' AND GROUP_ID IN (2,3)"></asp:SqlDataSource>
           </td>
        </tr>     
        <tr class="constantheight">
            <td align="right">Requirement or Description:</td>
            <td></td>
            <td colspan="3">
                <table>
                        <tr>
                            <td> Contains</td>
                            <td style="width:5px;"></td>
                            <td> <asp:TextBox ID="TxtReqDesc" runat="server"></asp:TextBox></td>
                        </tr>
                </table>    
              </td>
        </tr>
        <tr class="constantheight">
            <td align="right">Owner:</td>
            <td></td>
            <td colspan="3">
                <table>
                    <tr>
                        <td> <asp:TextBox ID="TxtOwnerrep" runat="server" ClientIDMode="Static"></asp:TextBox></td>
                        <td></td>
                        <td><asp:ImageButton ID="ImgbtnOwn" runat="server"  ImageUrl="~/Images/find.gif"/></td>
                        <td><asp:Label ID="Lblformat" runat="server" CssClass="formattext" Text="(Lastname, firstname)"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr class="constantheight" id="trDrt" runat="server" >
            <td align="right">Directorate:</td>
            <td></td>
            <td>
                <table>
                    <tr>
                        <td> <asp:DropDownList ID="DdlDirectorate" runat="server" DataSourceID="SDSDrt" 
                                DataTextField="DESCRIPTION" DataValueField="ORG_ID" 
                    ondatabound="DdlDirectorate_DataBound" AutoPostBack="True" 
                                onselectedindexchanged="DdlDirectorate_SelectedIndexChanged"></asp:DropDownList></td>
                    </tr>
                </table>           
             </td>
             <td colspan="2">
                <asp:SqlDataSource ID="SDSDrt" runat="server"  ConnectionString="<%$ ConnectionStrings:SLAC_WEB %>" 
                    ProviderName="System.Data.OracleClient" 
                    SelectCommand="SELECT ORG_ID,DESCRIPTION FROM SID.ORGANIZATIONS WHERE ORG_LEVEL = 2 AND STATUS='A' ORDER BY DESCRIPTION"></asp:SqlDataSource>           
             </td>            
        </tr>
        <tr class="constantheight" id="trDept" runat="server" >
            <td align="right">Department:</td>
            <td></td>
            <td colspan="3">
                <table>
                    <tr>
                        <td><asp:DropDownList ID="DdlDepartment" runat="server"></asp:DropDownList></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="constantheight">
             <td align="right">Subowner: 
            
             </td>
            <td></td>
            <td colspan="3">
                <table>
                    <tr>
                        <td> <asp:TextBox ID="TxtSubowner" runat="server" ClientIDMode="Static"></asp:TextBox></td>
                        <td></td>
                        <td><asp:ImageButton ID="ImgBtnSO" runat="server" ImageUrl="~/Images/find.gif" /></td>
                        <td> <asp:Label ID="Lblformat1" runat="server" CssClass="formattext" Text="(Lastname, firstname)"></asp:Label></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="constantheight">
            <td align="right">Due Date From:</td>
            <td></td>
            <td colspan="3">
                <table>
                    <tr>
                        <td><asp:TextBox ID="TxtFromDueDate" runat="server" Width="85px"></asp:TextBox></td>
                        <td>&nbsp;<asp:CompareValidator ID="cvFromDate" runat="server" Type="Date" Operator="DataTypeCheck"
                        ControlToValidate="txtFromDueDate" ErrorMessage="Not Valid!" CssClass="errlabels"></asp:CompareValidator></td>
                        <td> Due Date To:</td>
                        <td><asp:TextBox ID="TxtToDueDate" runat="server" Width="85px"></asp:TextBox></td>
                        <td>&nbsp;<asp:CompareValidator ID="cvToDate" runat="server" Type="Date" Operator="DataTypeCheck"
                        ControlToValidate="txtToDueDate" ErrorMessage="Not Valid!" CssClass="errlabels"></asp:CompareValidator></td>
                    </tr>
                </table> 
             </td>
        </tr>
        <tr class="constantheight">
            <td align="right">Date Submitted From:</td>
            <td></td>
            <td colspan="3">
                <table>
                    <tr>
                        <td><asp:TextBox ID="TxtFromSubmit" runat="server" Width="85px"></asp:TextBox></td>
                        <td>&nbsp; <asp:CompareValidator ID="CvFromSubmit" runat="server" Type="Date" Operator="DataTypeCheck"
                        ControlToValidate="TxtFromSubmit" ErrorMessage="Not Valid!" CssClass="errlabels"></asp:CompareValidator></td>
                        <td>Date Submitted To:</td>
                        <td> <asp:TextBox ID="TxtToSubmit" runat="server" Width="85px"></asp:TextBox></td>
                        <td>&nbsp;<asp:CompareValidator ID="CvToSubmit" runat="server" Type="Date" Operator="DataTypeCheck"
                        ControlToValidate="TxtToSubmit" ErrorMessage="Not Valid!" CssClass="errlabels"></asp:CompareValidator></td>
                    </tr>
                </table>          
              </td>
        </tr>
        <tr class="constantheight">
            <td align="right">Status:</td>
            <td></td>
            <td>
                <table>
                    <tr>
                        <td><asp:CheckBoxList ID="ChkStatus" runat="server" DataSourceID="SDSStatus" DataTextField="STATUS_DESC" DataValueField="STATUS_ID" ondatabound="ChkStatus_DataBound">              
                             </asp:CheckBoxList> </td>
                    </tr>
                </table>                           
            </td>
            <td colspan="2">
                <asp:SqlDataSource ID="SDSStatus" runat="server"  ConnectionString="<%$ ConnectionStrings:SLAC_WEB %>" 
                    ProviderName="System.Data.OracleClient" 
                    SelectCommand="SELECT STATUS_ID,STATUS_DESC FROM CMS_STATUS WHERE IS_ACTIVE='Y'"></asp:SqlDataSource> 
            </td>
        </tr>
        <tr class="constantheight">
            <td align="right">Approver:</td>
            <td></td>
            <td colspan="3">
                <table>
                    <tr>
                        <td> <asp:TextBox ID="TxtApprover" runat="server" ClientIDMode="Static"></asp:TextBox></td>
                        <td></td>
                        <td> <asp:ImageButton ID="ImgBtnApp" runat="server" ImageUrl="~/Images/find.gif" /></td>
                    </tr>
                </table>
        </td>
        </tr>
        <tr class="constantheight" id="trIsinfo" runat="server"  >
            <td align="right">Is Information Only?</td>
            <td></td>
            <td colspan="3">
                <table>
                    <tr>
                        <td> <asp:CheckBox ID="ChkInfo" runat="server" /></td>
                    </tr>
                </table>
           </td>
        </tr>
        <tr class="constantheight" id="trNotify" runat="server">
            <td align="right">Notification Schedule:</td>
            <td></td>
            <td>
                <table>
                    <tr>
                        <td>has</td>
                        <td></td>
                        <td> <asp:DropDownList ID="DdlNotification" runat="server" DataSourceID="SDSNotify" DataTextField="LOOKUP_DESC" 
                                DataValueField="LOOKUP_ID" ondatabound="DdlNotification_DataBound">
                                </asp:DropDownList></td>
                    </tr>
                </table>
           </td>
            <td colspan="2"><asp:SqlDataSource ID="SDSNotify" runat="server"  ConnectionString="<%$ ConnectionStrings:SLAC_WEB %>" 
                    ProviderName="System.Data.OracleClient" 
                    SelectCommand="SELECT LOOKUP_ID,LOOKUP_DESC FROM CMS_LOOKUP WHERE IS_ACTIVE='Y' AND LOOKUP_GROUP='Notification'"></asp:SqlDataSource> </td>
        </tr>
        <tr>
            <td class="style1"></td>
        </tr>
        <tr>
            <td colspan="5" align="center"><asp:Button ID="BtnSearch" runat="server" 
                    Text="Search" onclick="BtnSearch_Click" /> 
            &nbsp;&nbsp;<asp:Button ID="BtnClear" runat="server" Text="Clear" 
                    onclick="BtnClear_Click" />
            </td>
        </tr>
    </table>
    </asp:Panel>
</div>
   

</asp:Content>
