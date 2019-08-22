//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  Namefinder.aspx.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is used for finding an Employee 
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Text.RegularExpressions;
using System.Text;

namespace ContractManagement
{
    public partial class NameFinder : BasePage
    {
        Data.CMS_DataUtil objData = new Data.CMS_DataUtil();
        Business.CMS_Common_Util objCommon = new Business.CMS_Common_Util();
        Data.CMS_DMLUtil objDml = new Data.CMS_DMLUtil();

        protected void Page_Load(object sender, EventArgs e)
        {
            string _dialogName = "";
            string _contrl = "";
            string _isso = "";
           
                
            _dialogName = Request.QueryString["dialog"];
            _contrl = Request.QueryString["field"];
            _isso = Request.QueryString["isso"];
            HdnDialog.Value = _dialogName;
            HdnControl.Value = Request.QueryString["field"];
            if (_isso == "y")
            {
                trSO.Visible = true;
            }
            else
            {
                trSO.Visible = false;
            }

        }

      

        protected void GvNameList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected void CmdContinue_Click(object sender, EventArgs e)
        {
           
           
            BindGrid();
        }

        protected void BindGrid()
        {

            string alertmessage = "";
            string strtxtOwner = "";
            string strSingleQuote = "";
 
            int intPosition = 0;


            if (string.IsNullOrEmpty(TxtOwner.Text))
            {
                alertmessage = "Please enter the first few characters to start your search";
                objCommon.CreateMessageAlert(Page, alertmessage, "alertKey", false);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "SetFocus", "<script>document.getElementById('" + TxtOwner.ClientID + "').focus();</script>");
            }
            else
            {
                
                strtxtOwner = TxtOwner.Text.Trim();
 
                using (OracleDataReader _drEmployee = objDml.GetMatchingEmployees(strtxtOwner))
                {
                    trMsg2.Visible = true;
                    if (_drEmployee.HasRows)
                    {
                        DataTable _dtPeople = new DataTable();
                        _dtPeople.Load(_drEmployee);
                        
                        trGrid.Visible = true;
                        trButtons.Visible = true;

                        GvNameList.DataSource = _dtPeople;
                        GvNameList.DataBind();
                    }
                    else
                    {
                        LblMsg2.Text = "No such employee found in the directory. Please check the <br> spelling and re-enter.";
                    }

                }
              
            }


        }

        protected void CmdSelect_Click(object sender, EventArgs e)
        {
            string SelectedValue = "";
            //Session["lstvalue"] = null;
            foreach (GridViewRow gr in GvNameList.Rows)
            {
                CheckBox chk = (CheckBox)gr.FindControl("chkSelect");
                if (chk.Checked == true)
                {

                    Label lblName = (Label)gr.FindControl("LblName");
                    string ItemValue = lblName.Text.ToString();
                    SelectedValue = ItemValue.ToString();
                }
              
            }
            if (SelectedValue != "")
            {
                HdnItemval.Value = objCommon.GetEmpid(SelectedValue).ToString();
                 ScriptManager.RegisterStartupScript(this, GetType(), "key", "JQueryClose('" + Server.HtmlEncode(SelectedValue) + "');", true);
            }
            else
            {
                objCommon.CreateMessageAlertSM(this, "Please select a name", "error", false);
            }
           
           
        }


        protected void CmdBack_Click(object sender, EventArgs e)
        {       
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "JQueryClose('na');", true);

        }

        protected void CmdCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "JQueryClose('na');", true);
        }

        protected void GvNameList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvNameList.PageIndex = e.NewPageIndex;
            BindGrid();
        }

 
    }
}