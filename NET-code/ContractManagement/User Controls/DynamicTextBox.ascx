<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DynamicTextBox.ascx.cs" Inherits="ContractManagement.User_Controls.DynamicTextBox" %>
 <link href="../Styles/CMS.css" type="text/css" rel="Stylesheet" />

   <asp:UpdatePanel ID="updSO" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
    <ContentTemplate>
     <asp:TextBox ID="txt0" runat="server"  
                                  ToolTip ="Use Find icon to add a subowner!" Width="150px" ClientIDMode="Static"></asp:TextBox>
                              <asp:ImageButton ID="img0" runat="server" 
                                  ImageUrl ="~/Images/find.gif"  ToolTip="Click to Find" 
                                  CausesValidation="False"/>
                               &nbsp; <asp:Button ID="cmdAddSubOwner" runat="server" Text="+ More"  ToolTip="Click to add more Subowners" OnClick = "cmdAddSubOwner_Click" CausesValidation ="true"  ValidationGroup="subowner"/>
                               <asp:CustomValidator ID="cv0" runat="server" ErrorMessage="Not a Valid name/format"  CssClass="errlabels" ControlToValidate="txt0" ValidationGroup="subowner" ></asp:CustomValidator>
   
   

      <asp:Panel ID="pnlsubowner" runat="server">
                                </asp:Panel>
    
    </ContentTemplate>
   
   </asp:UpdatePanel>
             



                              
 