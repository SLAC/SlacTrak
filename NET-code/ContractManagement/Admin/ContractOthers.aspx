<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"  StylesheetTheme="CMS_Theme" CodeBehind="ContractOthers.aspx.cs" Inherits="ContractManagement.Admin.ContractOthers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/CMS.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="smContract" runat="server" EnablePartialRendering="true">
</asp:ScriptManager>

<asp:UpdatePanel ID="UpdContract" runat="server">
    <ContentTemplate>
    <div>
        <table width="100%">
            <tr>
                <td> <h2><asp:Label ID="LblSubTitle" runat="server" Text="Contract"></asp:Label></h2></td>
             </tr>
             <tr>
                <td><hr /></td>
             </tr>
        </table>   
    </div>
    <div>
      <asp:ValidationSummary ID="VSSummary" runat="server" ValidationGroup="Add" HeaderText="Please correct the following errors and then click Add"  CssClass="errlabels"/>
      <br />
        <asp:GridView ID="GvContract" runat="server" EmptyDataText="No Contract found" 
            AutoGenerateColumns="false" onpageindexchanging="GvContract_PageIndexChanging" 
            onrowcommand="GvContract_RowCommand" ShowFooter="true" SkinID="gridviewSkin" PageSize="10" AllowSorting="false" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="Id">
                    <ItemTemplate>
                    <asp:Label ID="LblId" runat="server" Text='<%#Bind("CONTRACT_ID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="LblName" runat="server" Text='<%# Eval("CONTRACT_NAME")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="TxtName" runat="server" MaxLength="50"></asp:TextBox><span class ="formattext">(max 50 chars.)</span>
                        
                        <asp:RequiredFieldValidator ID="RfvName" runat="server" ControlToValidate="TxtName" ValidationGroup="Add" ErrorMessage="Contract Name required" CssClass="errlabels" Text="*"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Contract Type">
                    <ItemTemplate>
                        <asp:Label ID="LblConType" runat="server" Text='<%#Eval("CONTRACTTYPE") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Dropdownlist runat="server" ID="DdlConType" AutoPostBack="true" OnSelectedIndexChanged= "DdlConType_SelectedIndexChanged">
                            <asp:ListItem Text="Contract" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Subcontract" Value="1"></asp:ListItem>
                        </asp:Dropdownlist>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Parent Contract">
                    <ItemTemplate>
                        <asp:Label ID="TxtParent" runat="server" Text='<%#Eval("PARENTCONTRACT") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                    <asp:Label ID="LblParentName" runat="server" text=""></asp:Label>
                        <asp:DropDownList ID="DdlParentName" runat="server" Visible="false" AppendDataBoundItems="true" DataSourceID="SDSParent" DataTextField="CONTRACT_NAME" DataValueField="CONTRACT_ID">
                             <asp:ListItem Value="0" Text="--Choose One--"></asp:ListItem>
                        </asp:DropDownList>
                        
                        <asp:RequiredFieldValidator ID="RfvParent" ControlToValidate= "DdlParentName" InitialValue="0" Text="*"   Visible="false" ErrorMessage="Contract required if subcontract" runat="server" ValidationGroup="Add" CssClass="errlabels"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
               

                 <asp:TemplateField HeaderText="Short Name">
                    <ItemTemplate>
                        <asp:Label ID="LblShortName" runat="server" Text='<%# Eval("SHORT_NAME")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="TxtShortName" runat="server" MaxLength="6"></asp:TextBox><span class ="formattext">(max 6 chars.)
                        
                        <asp:RequiredFieldValidator ID="RfvShortName" runat="server" ControlToValidate="TxtShortName" ValidationGroup="Add" ErrorMessage="Short Name required" CssClass="errlabels" Text="*"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="New">
                    <FooterTemplate>
                        <asp:Button ID="BtnAdd" runat="server" CommandName="add" CausesValidation="true" Text="Add New" ValidationGroup="Add" />
                    </FooterTemplate>
                </asp:TemplateField>
             
            </Columns>
            
        </asp:GridView>

         <asp:SqlDataSource ID="SDSParent" runat="server" ConnectionString="<%$ ConnectionStrings:SLAC_WEB %>" 
                                        ProviderName="System.Data.OracleClient"  SelectCommand="SELECT CONTRACT_ID,CONTRACT_NAME FROM VW_CMS_CONTRACT_DETAILS WHERE IS_ACTIVE='Y' AND PARENT_ID IS NULL AND CONTRACTTYPE='Contract'"></asp:SqlDataSource>
        
    </div>
    <br />
     <div>
        <table width="100%">
            <tr>
                <td> <h2><asp:Label ID="LblSubtitle2" runat="server" Text=" Deliverable Type"></asp:Label></h2></td>
             </tr>
             <tr>
                <td><hr /></td>
             </tr>
        </table>   
    </div>
    <div>
      <asp:ValidationSummary ID="VSType" runat="server" ValidationGroup="AddType" HeaderText="Please correct the following errors and then click Add"  CssClass="errlabels"/>
      <br />
        <asp:GridView ID="GVType" runat="server" EmptyDataText="No Type found" 
            AutoGenerateColumns="false" onpageindexchanging="GVType_PageIndexChanging" 
            onrowcommand="GVType_RowCommand" ShowFooter="true" SkinID="gridviewSkin" PageSize="10" AllowSorting="false">
            <Columns>
                <asp:TemplateField HeaderText="Id">
                    <ItemTemplate>
                    <asp:Label ID="LblTypeId" runat="server" Text='<%#Bind("CONTRACT_ID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label ID="LblTypeName" runat="server" Text='<%# Eval("CONTRACT_NAME")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="TxtTypeName" runat="server" MaxLength="50"></asp:TextBox><span class ="formattext">(max 50 chars.)</span>
                        
                        <asp:RequiredFieldValidator ID="RfvTypeName" runat="server" ControlToValidate="TxtTypeName" ValidationGroup="AddType" ErrorMessage="Type Name required" CssClass="errlabels" Text="*"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
         
               

                 <asp:TemplateField HeaderText="Short Name">
                    <ItemTemplate>
                        <asp:Label ID="LblTypeShortName" runat="server" Text='<%# Eval("SHORT_NAME")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="TxtTypeShortName" runat="server" MaxLength="6"></asp:TextBox><span class ="formattext">(max 6 chars.)
                        
                        <asp:RequiredFieldValidator ID="RfvTypeShortName" runat="server" ControlToValidate="TxtTypeShortName" ValidationGroup="AddType" ErrorMessage="Short Name required" CssClass="errlabels" Text="*"></asp:RequiredFieldValidator>
                    </FooterTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="New">
                    <FooterTemplate>
                        <asp:Button ID="BtnAddType" runat="server" CommandName="add" CausesValidation="true" Text="Add New" ValidationGroup="AddType" />
                    </FooterTemplate>
                </asp:TemplateField>
             
            </Columns>
            
        </asp:GridView>

    </div>
    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
