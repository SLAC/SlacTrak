<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileAttach.aspx.cs" Inherits="ContractManagement.FileAttach" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Attach File</title>
     <link href="css/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <link href="Styles/CMS.css" rel="Stylesheet" type="text/css" />
    <script src="Scripts/jquery-ui-1.8.21.custom.min.js" type="text/javascript"></script>
  
     <script type="text/javascript" src="Scripts/Jquery_common.js">  </script>

     <script type="text/javascript">
         function RefreshParentGrid(isso) {

             if (isso == 'so') {
                 window.parent.$('#dialogfile').dialog('close');
                 
                 parent.RedirectHome();
             }
             else {
                 parent.RefreshGrid();
             }
         }

                  function OpenDialogEmail() {
                    
                      $("#DlgEmail").dialog({
                          autoOpen: false,
                          modal: true,
                          height: 275,
                          width: 350,
                          buttons: {
                              "Yes": function (e) {
                               $('#<%= HdnEmail.ClientID %>').val("yes");
                                  $(this).dialog("close");
                                   <%= ClientScript.GetPostBackEventReference(new PostBackOptions(this.BtnAttach)) %>;
                                      },
                              "No": function () {
                                 $('#<%= HdnEmail.ClientID %>').val("no");
                                  $(this).dialog("close");
                                  <%= ClientScript.GetPostBackEventReference(new PostBackOptions(this.BtnAttach)) %>;
                                  return false;
                              }
                         }                
                      });
                      PrevDefault(event);
                      $('#DlgEmail').dialog('open');
                     
                  }



       </script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
   <div id="DlgEmail" style="display:none; color:Black;">
            Do you want to send Email?
          </div>
    <div>

        <table>
            <tr style="height:20%;"><td>&nbsp;</td></tr>
            <tr>
               
                <td>
                         <table>
                            <tr><td colspan="4" align="center"><asp:Label ID="Lblinstruct" runat="server"  Font-Bold ="true" Font-Size="Small" Text="**Note :- File Upload size limited to 10MB <br/> (doc, docx, jpg, bmp, pdf, xls, xlsx, png, txt, gif)"></asp:Label></td></tr>
                            <tr><td>&nbsp;</td></tr>
                           
                            <tr>
                                <td style="width:5px;">&nbsp;</td>
                                <td>
                                     <asp:Label ID="LblAttach" runat="server" Text="Attachment:" Font-Size="Large" ></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                                <td> <asp:FileUpload ID="FUDocument" runat="server"/></td>
                            </tr>
                         
                                                              
                            <tr><td>&nbsp;</td></tr>
                            <tr><td colspan="4" align ="center" >
                                <asp:button ID="BtnAttach" runat="server" text="Attach" 
                                    onclick="BtnAttach_Click" ClientIDMode="Static"/> &nbsp;&nbsp; 
                                <asp:Button ID="BtnCancel" runat="server"  Text="Cancel" ClientIDMode="Static"  />
                            </td></tr>
                        </table>
                </td>
            </tr>
        </table>
       
          <asp:HiddenField ID="HdnId" runat="server" />
          <asp:HiddenField ID="HdnEmail" runat="server" />

     </div>

    </form>

</body>
</html>
