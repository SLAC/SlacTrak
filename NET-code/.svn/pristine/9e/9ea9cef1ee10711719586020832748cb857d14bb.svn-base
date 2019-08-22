<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReqFlowdown.ascx.cs" Inherits="ContractManagement.User_Controls.ReqFlowdown" %>
<script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
<script src="../Scripts/jquery-ui-1.8.21.custom.min.js" type="text/javascript"></script>
<link href="../Styles/CMS.css" type="text/css" rel="stylesheet" />
<script  type="text/javascript">
     function onKeypress(btnid) {
         if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {
             $('#' + btnid).click();

             return false;
         }
         else
             return true;
     }

 
 </script>
<div>
    <asp:Label ID="LblHead" runat="server"  Font-Size="Large" Font-Bold="true" Text="List of Flown-down requirements"></asp:Label>
    <br /><br />
<table>
    <tr>
            <td>  <asp:Label ID="LblSearch" runat="server"  Font-Size="medium" Text="Search Flown-down Requirements on any keyword:"></asp:Label></td>
            <td></td>
            <td><asp:TextBox ID= "TxtReqName" runat="server"></asp:TextBox></td>
            <td></td>
            <td><asp:Button ID="cmdFind" runat="server" text="Search" onclick="cmdFind_Click" ClientIDMode="Static"/></td>
        </tr>
</table>

    <table style="width:100%;">
         <tr>
            <td>
                &nbsp;</td>
        </tr>
            <tr>
                <td> <asp:ImageButton ID="ImgBtnExport" runat="server" ImageUrl="~/Images/ExportToExcel.gif" onclick="ImgBtnExport_Click" /></td>
            </tr>
        <tr>
            <td>
                <asp:GridView id="GVReq" runat="server" EmptyDataText="No requirement found" AllowPaging="true" AllowSorting="true"  
                     SkinID="gridviewSkin" AutoGenerateColumns="false" PageSize="15"  OnPageIndexChanging="GVReq_PageIndexChanging" OnSorting="GVReq_Sorting" >
                    <Columns>
                        <asp:BoundField DataField="REQUIREMENT_ID" HeaderText="Id" SortExpression="REQUIREMENT_ID"/>
                            <asp:BoundField DataField="REQUIREMENT" HeaderText="Requirement"  SortExpression="REQUIREMENT"/>
                            <asp:BoundField DataField="NOTES" HeaderText="Notes" SortExpression="NOTES" />
                            <asp:BoundField DataField="FREQUENCY" HeaderText="Frequency"  SortExpression="FREQUENCY" />
                            <asp:BoundField DataField="UPLOAD_FILE_REQUIRED" HeaderText="Upload File Required" SortExpression="UPLOAD_FILE_REQUIRED" />
                            <asp:BoundField DataField="START_DATE" HeaderText="Start Date" HtmlEncode="false" DataFormatString = "{0:MM/dd/yyyy}"  SortExpression="START_DATE"/>
                            <asp:BoundField DataField="CLAUSE_ID" HeaderText="Clause Id" SortExpression="CLAUSE_ID" />
                            <asp:BoundField DataField="CLAUSE_NUMBER" HeaderText="Clause Number" SortExpression="CLAUSE_NUMBER" />
                            <asp:BoundField DataField="SCFLOWN_PROVISION" HeaderText="SC Flown provision" />                                        
                    </Columns>                                    
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>