//$Header:$
//
//  PopupName.aspx.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2011 SLAC. All rights reserved.
//
//  This is the pop up page to find and enter an employee
//


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using Microsoft.VisualBasic;
using System.Text.RegularExpressions;


namespace ATS
{
    public partial class PopupName : System.Web.UI.Page
    {
        OracleConnection oConn = new OracleConnection();
        //OracleCommand oCmd = null;
        //OracleDataReader oDr = null;

        Business.ATS_Common_Util objCommon = new Business.ATS_Common_Util();
        Data.ATS_DML_Util objDml = new Data.ATS_DML_Util();
        Data.ATS_DataUtil objData = new Data.ATS_DataUtil();
        //string AdminSchema = ConfigurationManager.AppSettings["AdminSchema"];

        string strTextField = "";
        string _populatedir = "";
        string _comp = "";

        //Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        //    Dim errorpage As String = ""

        //    errorpage = HttpUtility.UrlEncode(Server.GetLastError.Message.ToString())
        //    Response.Redirect("Error.aspx?error=" + errorpage)

        //End Sub
        
        private void Page_Load(System.Object sender, System.EventArgs e)
        {
            //Put user code to initialize the page here
            txtOwner.Attributes.Add("onkeydown", "return onKeypress();");
            ddlEmplist.Attributes.Add("onkeydown", "return onKeypress1();");
            if (!Page.IsPostBack)
            {
                strTextField = Request.QueryString["src"];
                hdntxtField.Value = strTextField;
                _populatedir = Request.QueryString["populate"];
                HdnPopulate.Value = _populatedir;
                _comp = Request.QueryString["comp"];
                HdnComp.Value = _comp;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>document.getElementById('" + txtOwner.ClientID + "').focus();</script>");
            }

        }
        //public void CreateMessageAlert(System.Web.UI.Page senderpage, string alertMsg, string alertKey)
        //{
        //    string strScript = null;
        //    strScript = "<script language=JavaScript>alert('" + alertMsg + "')</script>";
        //    if (!(senderpage.ClientScript.IsStartupScriptRegistered(alertKey)))
        //    {
        //        senderpage.ClientScript.RegisterStartupScript(senderpage.GetType(), alertKey, strScript);
        //    }
        //}

      


       



        private void OpenConnection()
        {

            oConn.ConnectionString = objData.GetConnectionString("cats");
            oConn.Open();

        }

        private void CloseConnection()
        {
            oConn.Close();
        }

       
        private void CloseWindow()
        {
            StringBuilder sbScript = new StringBuilder();
            sbScript.Append("<script language='javascript'>");
            sbScript.Append(Environment.NewLine);
            sbScript.Append("window.close();");
            sbScript.Append(Environment.NewLine);
            sbScript.Append("</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CloseWindow", sbScript.ToString());
        }

        

        protected void cmdContinue_Click(object sender, EventArgs e)
        {
           // OracleConnection oConncats = new OracleConnection();

            OracleCommand oCmdcats = new OracleCommand();//default(OracleCommand);
            OracleDataReader oDrcats = null;
            string alertmessage = "";
            string strtxtOwner = "";
            string strSingleQuote = "";
            string strSQLEmployee = "";
            int intPosition = 0;
            int intLen = 0;



            if (string.IsNullOrEmpty(txtOwner.Text))
            {
                alertmessage = "Please enter the first few characters to start your search";
                objCommon.CreateMessageAlert(Page, alertmessage, "alertKey",false);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>document.getElementById('" + txtOwner.ClientID + "').focus();</script>");
            }
            else
            {
                ddlEmplist.Visible = true;
                lblMessage2.Visible = true;
                cmdOk.Visible = true;
                cmdCancel.Visible = true;
                strtxtOwner = txtOwner.Text.Trim().ToLower();
                strSingleQuote = "'";
                intPosition = strtxtOwner.IndexOf(strSingleQuote);
            
                if (intPosition > 0)
                {
                    string txtb4 = strtxtOwner.Substring(0, intPosition);
                    string txtafter = strtxtOwner.Substring(intPosition, strtxtOwner.Length - intPosition);
                    strtxtOwner = txtb4 + strSingleQuote + txtafter;
                }
               
                intLen = strtxtOwner.Length;
                //TODO: Change the sql query to use parametrized sql
                //if (int.TryParse(strtxtOwner.Substring (1, 1)) , true)
                Regex reCheck = new Regex("^[0-9]*$");

              
                try
                {
                    
                     if (reCheck.IsMatch(strtxtOwner))
                    {
                        //Employee id query
                        strSQLEmployee = "SELECT EMPLOYEE_NAME, EMPLOYEE_ID FROM DW_PEOPLE_CURRENT WHERE EMPLOYEE_ID=:Owner";
                        oCmdcats.Parameters.Add(":Owner", OracleType.VarChar).Value = strtxtOwner;
                    }
                    else
                    {
                        //Employee name query
                        strSQLEmployee = "SELECT EMPLOYEE_NAME, EMPLOYEE_ID FROM DW_PEOPLE_CURRENT WHERE LOWER(EMPLOYEE_NAME) LIKE '" + strtxtOwner + "%' ORDER BY EMPLOYEE_NAME";
                       // oCmdcats.Parameters.Add(":Owner", OracleType.VarChar).Value = strtxtOwner + "%";
                    }
                    oDrcats = objData.GetReader(strSQLEmployee,oCmdcats); 
                    ddlEmplist.Items.Clear();
                    if (oDrcats.HasRows)
                    {
                        lblMessage2.Visible = true;
                        ddlEmplist.Visible = true;
                        cmdOk.Visible = true;
                        cmdCancel.Visible = true;
                        lblError.Visible = false;
                        while (oDrcats.Read())
                        {
                            ListItem NewItem = new ListItem();
                            NewItem.Text = Convert.ToString(oDrcats["EMPLOYEE_NAME"]);
                            NewItem.Value = Convert.ToString(oDrcats["EMPLOYEE_ID"]);
                            ddlEmplist.Items.Add(NewItem);
                        }
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>document.getElementById('" + ddlEmplist.ClientID + "').focus();</script>");
                    }
                    else
                    {
                        lblError.Visible = true;
                        lblError.Text = "No such employee found in the directory. Please check the <br> spelling and re-enter.";
                        lblMessage2.Visible = false;
                        ddlEmplist.Visible = false;
                        cmdOk.Visible = false;
                        cmdCancel.Visible = false;
                    }

                }
                catch 
                {
                     lblError.Visible = true;
                     lblError.Text = "Something went wrong. Please try again later";
                }
                finally
                {
                    oDrcats.Close();
                    oCmdcats = null;
                }

            }
        }

        protected void cmdCancel1_Click(object sender, EventArgs e)
        {
            CloseWindow();
        }
        //protected string GetDirectorate(string userId)
        //{
        //    OracleDataReader _drDir = null;

        //    string _dirCode = "";
        //    try
        //    {
        //        _drDir = objDml.GetDirectorateDept(userId);
        //        while (_drDir.Read())
        //        {
        //            _dirCode = objCommon.FixDBNull(_drDir[2]);
        //        }
        //        return _dirCode;
        //    }
        //    finally
        //    {
        //        _drDir.Close();
        //    }

        //}

        //   private string SetDirDivisionDept(string userId)
        //{
        //    string _deptCode, _dirCode, _divCode;
        //    _deptCode = " ";
        //    _dirCode = " ";
        //    _divCode = " ";
          
        //    OracleDataReader _drDirdept = null;
        //    try
        //    {
        //        _drDirdept = objDml.GetDirectorateDept(userId);
        //        while (_drDirdept.Read())
        //        {

        //            _deptCode = objCommon.FixDBNull(_drDirdept[0]);
        //            _dirCode = objCommon.FixDBNull(_drDirdept[2]);
        //            _divCode = objCommon.FixDBNull(_drDirdept[3]);
        //        }
        //    }
        //    finally
        //    {
        //        _drDirdept.Close();
        //    }
           
        //    string _divOrgId = objDml.GetDivisionId(_dirCode, _divCode);
        //       return _dirCode + "-" + _divOrgId + "-" + _deptCode;
           
        //}
        protected void cmdOk_Click(object sender, EventArgs e)
        {
            StringBuilder sbScript = new StringBuilder();
            //string[] dirdivdept;
            //string sTest;

            string sempname = null;
            //string sdirCode = null;
            //string sdivCode = null;
            //string sdeptCode = null;

            //Added on 03/06/09 - quotes in names give javascript error, so replace single quotes with \'
            //sTest = SetDirDivisionDept(ddlEmplist.SelectedValue);

            //dirdivdept = Regex.Split(sTest,"-");

            sempname = ddlEmplist.SelectedItem.Text.Replace("'", "\\'");
            //sdirCode = dirdivdept[0];//GetDirectorate(ddlEmplist.SelectedValue);
            //sdivCode = dirdivdept[1];
            //sdeptCode = dirdivdept[2];
            sbScript.Append("<script language='javascript'>");
            sbScript.Append(Environment.NewLine);
            if (HdnComp.Value == "ias")
            {
                sbScript.Append("window.opener.document.aspnetForm.ctl00$cphContentIAS$");
            }
            else
            {
                sbScript.Append("window.opener.document.aspnetForm.ctl00$cphContent$");
            }
            sbScript.Append(hdntxtField.Value);
            sbScript.Append(".value = '");
            sbScript.Append(sempname);
            sbScript.Append("';");
            sbScript.Append(Environment.NewLine);
            if (HdnPopulate.Value == "yes")
            {
                sbScript.Append("window.opener.document.aspnetForm.submit();");



                //sbScript.Append("windowere.opener.document.aspnetForm.ctl00$cphContent$");
                //sbScript.Append("DdlDirectorate.value='");

                //sbScript.Append(sdirCode);
                //sbScript.Append("';");
                //window.opener.document.aspnetForm.ctl00$cphContent$ DdlDirectorate_SelectedIndexChanged(null, null);
                //sbScript.Append("window.opener.document.aspnetForm.ctl00$cphContent$");
                //sbScript.Append("DdlDivision.value='");
                //if (sdivCode != "")
                //{
                //    sbScript.Append(sdivCode);
                //}
                //else { sbScript.Append("0"); }
                //sbScript.Append("';");
                //sbScript.Append("window.opener.document.aspnetForm.ctl00$cphContent$");
                //sbScript.Append("DdlDept.value='");

                //sbScript.Append(sdeptCode);
                //sbScript.Append("';");

            }
            else
            {
                if (HdnComp.Value == "ias")
                {
                    sbScript.Append("window.opener.document.aspnetForm.ctl00$cphContentIAS$");
                    sbScript.Append(hdntxtField.Value);
                    sbScript.Append(".focus();");
                }
 
            }
                 
            sbScript.Append("window.close();");
            sbScript.Append(Environment.NewLine);
            sbScript.Append("</script>");
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "CloseWindow", sbScript.ToString());
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            CloseWindow();
        }

       

    }
}