<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NameFinder.aspx.cs" Inherits="ContractManagement.NameFinder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Find Employee</title>
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <link href="Styles/Site.css"  rel="Stylesheet"/>
    
    <script type="text/javascript" language="javascript">

        function toggleSelectionGrid(source) {
            var isChecked = source.checked;
            $("#GridTable input[id*='ChkSelect']").each(function (index) {
                $(this).attr('checked', false);
            });
            source.checked = isChecked;
        }

       

        function JQueryClose(selectedvalue) {
            var dialog = $('#HdnDialog').val();
            var control = $('#HdnControl').val();

            if (selectedvalue == ' ') {
                window.parent.$('#' + control).val(' ');
            }
            else if (selectedvalue == 'na') {

            }
            else {
                selectedvalue = htmlDecode(selectedvalue);
                window.parent.$('#' + control).val(selectedvalue);

            }
              
                window.parent.$("#" + dialog).dialog('close');
                if ((control == 'TxtOwner') && (selectedvalue != 'na'))
                {
                    parent.ResetDrtDept();
                }
           return false;
        }

        function htmlDecode(value) {
            return $('<div/>').html(value).text();
        }




     </script>
</head>
<body style="background-color:White;">
    <form id="form1" runat="server" defaultbutton="CmdContinue">
    <div>
        <asp:Panel ID="PnlName"  runat="server" >
            <table id="GridTable">
                <tr id="trSO" runat="server" visible="false">
                    <td>
                        <table border="black 1px solid">
                            <tr><td>
                                <asp:Label ID="LblInfo" runat="server" Font-Size="Medium" Text="Subowners may either upload the deliverable to you for submittal, or upload other documents for background" Font-Bold="true"></asp:Label>
                            </td></tr>
                        </table>
                    </td>                
                </tr>
                <tr>
                    <td><asp:Label ID="LblMsg" runat="server" Text="Please enter the first few characters of the employee last name. If you don't know the employee last name, you may enter the  employee id."></asp:Label></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="TxtOwner" runat="server"></asp:TextBox>
                        <asp:Button ID="CmdContinue" runat="server"  Text="Continue"  ClientIDMode="Static"
                            onclick="CmdContinue_Click"/>
                        <asp:Button ID="CmdCancel" runat="server" Text="Cancel" 
                            onclick="CmdCancel_Click" />                    
                    </td>
                </tr>
                  <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td><asp:Label ID="LblError" runat="server" Visible="false"></asp:Label></td>
                </tr>
                <tr id="trMsg2" runat="server" visible="false">
                    <td><asp:Label ID="LblMsg2" runat="server" Text="Select a Name from the list below:"></asp:Label></td>
                </tr>
              
                <tr id="trGrid" runat="server" visible="false">
                    <td>
                    <asp:GridView ID="GvNameList" runat="server" PageSize="5" ShowFooter="true" 
                        EmptyDataText="No Employees found with the text entered. Please try again!" 
                        EnableModelValidation="true" onrowcommand="GvNameList_RowCommand" 
                            AutoGenerateColumns="false" AllowPaging="True" 
                            onpageindexchanging="GvNameList_PageIndexChanging" >
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:CheckBox id="ChkSelect" runat="server"  onclick = "toggleSelectionGrid(this);"/>
                                    <asp:Label runat="server" ID="LblName"  Visible="false" Text='<%#Eval("EMPLOYEE_NAME") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Name"  DataField="EMPLOYEE_NAME"/>
                            <asp:BoundField HeaderText="Department" DataField="DESCRIPTION" />
                            <asp:BoundField HeaderText="Id" datafield="EMPLOYEE_ID"/>
                            <asp:BoundField HeaderText="Email" DataField="EPO" />
                        </Columns>
                    
                    
                        <SelectedRowStyle BackColor="Orange" />
                        <HeaderStyle BackColor="LightGray" />
                    

                    </asp:GridView>
                    </td>
                </tr>
                <tr>
                <td>&nbsp;</td>
            </tr>
            <tr id="trButtons"  Visible="false" runat="server">
                <td> <asp:Button ID="CmdSelect" runat="server" Text="Select" 
                        onclick="CmdSelect_Click" />
                    <asp:Button ID="CmdBack" runat="server" Text="Cancel & Exit" 
                        onclick="CmdBack_Click" />
                </td>
            </tr>
            </table>
        <asp:HiddenField ID="HdnDialog" runat="server" />
        <asp:HiddenField ID="HdnControl" runat="server" />
        <asp:HiddenField ID="HdnItemval" runat="server" />
       
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
