<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopupName.aspx.cs" Inherits="ATS.PopupName" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" language="javascript">

        function onKeypress() {
            if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {
                document.frmEmployee.cmdContinue.click();

                return false;
            }
            else
                return true;
        }

        function onKeypress1() {
            if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {
                document.frmEmployee.cmdOk.click();
                return false;
            }
            else
                return true;
        }
    
		
		</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          <asp:Panel ID="PnlPopup" runat="server">
             <table>
		        <tr>
		            <td>
		                <asp:label id="lblMessage" runat="server" Width="416px" Height="48px" Font-Size="9pt" Font-Names="Arial,Verdana,Helvetica">Please enter the first few characters of the employee last name.If <br /> you don't know the employee last name, you may enter the <br /> employee id. </asp:label>
				    </td>
		        </tr>
		        <tr>
		            <td>
		                <asp:textbox id="txtOwner" runat="server" Width="152px"></asp:textbox>
		                <asp:button id="cmdContinue" runat="server" Text="Continue" TabIndex="1" 
                            onclick="cmdContinue_Click"></asp:button>
		                <asp:button id="cmdCancel1" runat="server" Text="Cancel" TabIndex="2" 
                            onclick="cmdCancel1_Click"></asp:button>
		                
		            </td>
		        </tr>
		        <tr>
		            <td><asp:Label id="lblError" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="9pt" Font-Names="Arial,Verdana,Helvetica">Label</asp:Label></td>
		        </tr>
		        <tr>
		            <td>
		                <asp:label id="lblMessage2" runat="server" Width="120px" Visible="False" Font-Size="9pt" Font-Names="Arial,Verdana,Helvetica">Select from the list:</asp:label>
		            </td>
		        </tr>
		        <tr>
		            <td>
		                <asp:dropdownlist id="ddlEmplist" runat="server" Width="152px" Visible="False" TabIndex="3"></asp:dropdownlist>
		            </td>
		        </tr>
		        <tr>
		            <td>
		                <asp:button id="cmdOk" runat="server" Width="72px" Text="Ok" Visible="False" 
                            TabIndex="4" onclick="cmdOk_Click"></asp:button>
				        <asp:button id="cmdCancel" runat="server" Width="64px" Text="Cancel" 
                            CausesValidation="False" Visible="False" TabIndex="5" onclick="cmdCancel_Click"></asp:button>
		            </td>
		        </tr>
		        <tr>
		            <td><input id="hdntxtField" type="hidden" runat="server" />
                    <asp:HiddenField ID="HdnPopulate" runat="server" />
                    <asp:HiddenField ID="HdnComp" runat="server" />
                        </td>
		        </tr>    
		    </table>
        
        
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
