//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  Admin.master.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is the master Admin home page

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ContractManagement.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HighlightSelectedItem();
        }

        //Method to select the top Navigation Items based on what page the user is in
        private void HighlightSelectedItem()
        {
            string MyURL = Request.Url.AbsoluteUri.ToLower();
            //COMMENTS MS: Logic - If the url contains no aspx, default.aspx is appended to the absolute url
            bool _containsaspx = MyURL.Contains(".aspx"); ;
            if (!_containsaspx)
            {
                MyURL = Request.Url.AbsoluteUri + "default.aspx";
            }
            MyURL = MyURL.Substring(MyURL.LastIndexOf("/"));
            foreach (MenuItem mi in AdminMenu.Items)
            {
                string _navurl = mi.NavigateUrl;
                string _navurl1 = _navurl.Substring(_navurl.LastIndexOf("/")).ToLower();
                if (_navurl1.IndexOf("_") >= 0)
                {
                    string _navurl2 = _navurl1.Substring(0, 9);
                    _navurl1 = _navurl2;
                }
                if (!string.IsNullOrEmpty(_navurl1))
                {
                    if (MyURL.Contains(_navurl1.Substring(0,4)))
                    {
                        mi.Selected = true;
                    }
                }
            }

        }
    }
}