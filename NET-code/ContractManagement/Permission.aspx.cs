//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  Permission.aspx.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is used for checking user permissions
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContractManagement
{
    public partial class Permission : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string _msg;
                _msg = Request.QueryString["msg"];
                
                if (_msg == "noperm")
                {
                    LblMsg.Text = "You do not have permission to the Contract Management System." +
                        "<br />" + "";
                }
                else if (_msg == "nopermview")
                {
                    LblMsg.Text = "You do not have permission to view this Item." +
                        "<br />" + "";
                }
                else if (_msg == "nopermadd")
                {
                    LblMsg.Text = "You do not have permission to add/Edit a Deliverable Item." +
                        "<br />" + "";
                }
                else if (_msg == "gen")
                {
                    LblMsg.Text = "You do not have permission to this page." +
                       "<br />" + "";
                }
               
                else
                {
                    Response.Redirect("Error.aspx");
                }
               
            }
        }
    }
}