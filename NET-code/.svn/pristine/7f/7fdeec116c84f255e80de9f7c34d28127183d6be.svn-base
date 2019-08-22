<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequirementList.ascx.cs" Inherits="ContractManagement.User_Controls.RequirementList" %>
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

 
 </script>
<div>
<div align="center"><asp:Label ID="Lblhead" runat="server"  Font-Size="Large"  Font-Bold="true" Text="List of all Requirements"></asp:Label></div>
  <br />
 
<br />
<table>
    <tr>
            <td>  <asp:Label ID="LblSearch" runat="server"  Font-Size="medium" Text="Search all Requirements on any keyword:"></asp:Label></td>
            <td></td>
            <td><asp:TextBox ID= "TxtReqName" runat="server"></asp:TextBox></td>
            <td></td>
            <td><asp:Button ID="cmdFind" runat="server" text="Search" onclick="cmdFind_Click" ClientIDMode="Static"/></td>          
    </tr>
</table>
<br />

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
                <asp:GridView ID="GvRequirement" runat="server" EmptyDataText="No Requirement found" 
                    AllowPaging = "true" SkinID="gridviewSkin" AutoGenerateColumns ="false" 
                    onrowdatabound="GvRequirement_RowDataBound" PageSize ="15" OnPageIndexChanging="GvRequirement_PageIndexChanging" >
                        <Columns>
                            <asp:BoundField DataField="REQUIREMENT_ID" HeaderText="Id"/>
                            <asp:BoundField DataField="REQUIREMENT" HeaderText="Requirement" />
                            <asp:BoundField DataField="CLAUSENUM" HeaderText="Clause" />
                            <asp:BoundField DataField="SUBCLAUSENUM" HeaderText="Subclause"  />
                            <asp:TemplateField HeaderText= "Deliverables?">
                                <ItemTemplate>
                                    <asp:HyperLink ID="HLKDeli" runat="server" Target="_self" ></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>                        
                        </Columns>         
                </asp:GridView>
            <asp:Label ID="LblFooter" runat="server"></asp:Label>
            </td>
            
        </tr>
        </table>     

</div>