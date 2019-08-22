<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"  StylesheetTheme="CMS_Theme" CodeBehind="User.aspx.cs" Inherits="ContractManagement.Admin.User1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/CMS.css" rel="Stylesheet" type="text/css" />
     <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui-1.8.21.custom.min.js" type="text/javascript"></script>
    <script src="../Scripts/CMS_Common.js"  type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/Jquery_common.js"></script>
    <link href="../css/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <asp:ScriptManager ID="SMUser" runat="server" EnablePartialRendering = "true">
   </asp:ScriptManager>
   <asp:UpdatePanel ID="UpdUser" runat="server">
    <ContentTemplate>
        <div>
        <table width="100%">
            <tr>
                <td> <h2><asp:Label ID="LblSubTitle" runat="server" Text="User"></asp:Label></h2></td>
             </tr>
             <tr>
                <td><hr /></td>
             </tr>
        </table>   
    </div>
     <div id="dialogowneradmin" style="display:none;" >             
                <iframe id="modaldialogowneradmin" scrolling="no" frameborder="1" width="100%" height="100%">
                </iframe>
          </div>
     <div>
      <asp:ValidationSummary ID="VSSummary" runat="server" ValidationGroup="Add" HeaderText="Please correct the following errors and then click Add"  CssClass="errlabels"/>
      <br />
        <asp:GridView ID="GvUsers" runat="server" EmptyDataText="No User found" 
            AutoGenerateColumns="false" onpageindexchanging="GvUsers_PageIndexChanging" 
            onrowcommand="GvUsers_RowCommand" ShowFooter="true" SkinID="gridviewSkin" 
             PageSize="15" onrowdatabound="GvUsers_RowDataBound" 
             onsorting="GvUsers_Sorting">
            <Columns>
                <asp:TemplateField HeaderText="Id" SortExpression="MANAGER_ID">
                    <ItemTemplate>
                    <asp:Label ID="LblId" runat="server" Text='<%#Bind("MANAGER_ID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="User Type" SortExpression="MANAGER_TYPE">
                    <ItemTemplate>
                        <asp:Label ID="LblUserType" runat="server" Text='<%#Eval("MANAGER_TYPE") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Dropdownlist runat="server" ID="DdlUserType" AutoPostBack="true" OnSelectedIndexChanged= "DdlUserType_SelectedIndexChanged" AppendDataBoundItems="true"
                         DataSourceID="SDSMGRTYPE" DataValueField="MGRVAL" DataTextField="MANAGER_TYPE">
                           <asp:ListItem Text="--Choose One--" Value="0"></asp:ListItem>
                        </asp:Dropdownlist>
                        <asp:RequiredFieldValidator ID="RfvUser" runat="server" ControlToValidate="DdlUserType" Text="*" ErrorMessage="User Type is required" ValidationGroup="Add" CssClass="errlabels" InitialValue="0"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Directorate" SortExpression="DESCRIPTION">
                    <ItemTemplate>
                        <asp:Label ID="LblDrt" runat="server" Text='<%#Eval("DESCRIPTION") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="LblNoDrt" runat="server" Text="NA"></asp:Label>
                        <asp:Dropdownlist runat="server" ID="DdlDrt" AutoPostBack="true" 
                         Visible="false" >
                          
                        </asp:Dropdownlist>
                        <asp:RequiredFieldValidator ID="RfvDesc" runat="server" ControlToValidate="DdlDrt" Text="*" ErrorMessage="Directorate is required" ValidationGroup="Add" CssClass="errlabels" Visible="false" InitialValue="0"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name" SortExpression="EMPLOYEE_NAME">
                    <ItemTemplate>
                        <asp:Label ID="LblName" runat="server" Text='<%# Eval("EMPLOYEE_NAME")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="TxtName" runat="server" MaxLength="50" ClientIDMode="Static"></asp:TextBox>
                        <asp:ImageButton ID="ImgbtnOwn" runat="server"  ImageUrl="~/Images/find.gif" CssClass="imageButtonFinderClass"/>
                        <span class ="formattext">(Last Name, First Name)</span>
                        
                        <asp:RequiredFieldValidator ID="RfvName" runat="server" ControlToValidate="TxtName" ValidationGroup="Add" ErrorMessage="User Name required" CssClass="errlabels" Text="*"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CvOwn"  CssClass="errlabels"  ValidateEmptyText="false" OnServerValidate= "CvOwn_ServerValidate"  runat="server" ControlToValidate= "TxtName" ErrorMessage="Not a Valid Name/Format" SetFocusOnError="true" ValidationGroup="Add" Text="*"></asp:CustomValidator>
                        <asp:CustomValidator ID="CvDuplicate"  CssClass="errlabels"  ValidateEmptyText="false" OnServerValidate= "CvDuplicate_ServerValidate"  runat="server" ControlToValidate= "TxtName" ErrorMessage="User already has that role" SetFocusOnError="true" ValidationGroup="Add" Text="*"></asp:CustomValidator>

                    </FooterTemplate>
                </asp:TemplateField>
               
               
                 <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="BtnDelete" runat="server" CommandName="del" CausesValidation="false" Text="Delete" OnClientClick="return confirm('Are you certain you want to delete this User?');" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "MANAGER_ID") %>'/>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Button ID="BtnAdd" runat="server" CommandName="add" CausesValidation="true" Text="Add New" ValidationGroup="Add" />
                    </FooterTemplate>
                </asp:TemplateField>
             
            </Columns>
            
        </asp:GridView>

         <asp:SqlDataSource ID="SDSDRT" runat="server" ConnectionString="<%$ ConnectionStrings:SLAC_WEB %>" 
                                        ProviderName="System.Data.OracleClient"  SelectCommand="SELECT ORG_ID,DESCRIPTION FROM SID.ORGANIZATIONS WHERE ORG_LEVEL =2 AND STATUS='A'"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SDSMGRTYPE" runat="server" ConnectionString="<%$ ConnectionStrings:SLAC_WEB %>" 
                                        ProviderName="System.Data.OracleClient"  SelectCommand="SELECT DISTINCT MANAGER_TYPE,MANAGER_TYPE AS MGRVAL FROM CMS_MANAGER"></asp:SqlDataSource>
        
    </div>
    
    </ContentTemplate>
   
   </asp:UpdatePanel>
</asp:Content>
