//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  Report_Home.aspx.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is main report page 
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContractManagement
{
    public partial class Report_Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Btnpc_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report.aspx?page=cl");
        }

        protected void BtnCustom_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportCriteria.aspx");
        }

        protected void BtnReq_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report.aspx?page=req");
        }

        protected void BtnFy_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report.aspx?page=fy");
        }

        protected void BtnFlowdown_Click(object sender, EventArgs e)
        {
            Response.Redirect("Report.aspx?page=reqfd");
        }
    }
}