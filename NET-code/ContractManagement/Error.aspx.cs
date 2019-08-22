﻿//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  NotAuthorized.aspx.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2011 SLAC. All rights reserved.
//
//  This is the page that has customized not authorized messages.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContractManagement
{
   

    public partial class Error : System.Web.UI.Page
    {
         public String _url;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["urlhost"] != null)
            {
                _url = Session["urlhost"].ToString() + "/ContractManagement/";
            }
          
            if (!Page.IsPostBack)
            {
                string _msg;
                _msg = Request.QueryString["msg"];
                if (_msg == "file")
                {
                   
                    LblMsg.Text = "File Upload error";
                       
                }
                else if (_msg == "pwd")
                {
                    LblMsg.Text = "Password has either expired or locked";
                }
                else if (_msg == "sc")
                {
                    LblMsg.Text = " Error due to HttpRequestValidation Exception. One reason might be that Text contains special characters like left angle bracket. ";
                }
                else if (_msg == "pminvld")
                {
                    LblMsg.Text = "You have an invalid URL request." ;
                }
                else if (_msg == "noobj")
                {
                    LblMsg.Text = "No record found.";
                }
                else
                {

                    LblMsg.Text = "   We're sorry. Something unexpected happened.";
                }
               
            }
        }
    }
}