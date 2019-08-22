//$Header:$
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  Site.Master.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is the codebehind for Master Page
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace ContractManagement
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public string UserName;
        Business.CMS_Common_Util objCommon = new Business.CMS_Common_Util();
        Business.UserRoles objUser = new Business.UserRoles();

        protected void Page_Load(object sender, EventArgs e)
        {
            string _userId = "";
            string _isProd = "Y";
    
            //Get User ID. If it is not valid, redirect to error
            _userId = objCommon.GetUserID();
            if (_userId == "err") { Response.Redirect("Error.aspx"); }
            if (_userId != "")
            {
                UserName = objCommon.GetFullName(objCommon.GetEmpname(_userId));
                bool _isCMA = false;
                bool _isAdmin = false;
                bool _isALD = false;
                bool _isDiradmin = false;
                bool _isSSO = false;
                bool _isSSOSuper = false;

                //Check the role of the person logged in
                objUser.GetUserRole(_userId);
                _isCMA = (bool)Session["cma"];
                _isAdmin = (bool)Session["admin"];
                _isALD = (bool)Session["ald"];
                _isDiradmin = (bool)Session["diradmin"];
                _isSSO = (bool)Session["sso"];
                _isSSOSuper = (bool)Session["ssosuper"];

                //Admin page is visible only if the role is CMA (contract Management) or Admin
                if ((!_isCMA) && (!_isAdmin))
                {
                    this.NavigationMenu.Items.Remove(NavigationMenu.Items[2]);
                }

                if (!Page.IsPostBack)
                {
                    //Check if it is production, it will display Test message if it is non-prod
                    _isProd = ConfigurationManager.AppSettings["prodServer"];
                    if (_isProd == "N")
                    {
                        LblInfo.Visible = true;

                    }
                    else
                    {
                        LblInfo.Visible = false;

                    }


                }
            }
            else
            {
                Response.Redirect("Permission.aspx?msg=noperm");
            }

            //COMMENTS MS: Workaround - Page.Header.DataBind() is added to resolve <%# %> header in the head section with the combination of ResolveUrl
            //....Added to make the js path work within all folders and nested masterpage
            this.Page.Header.DataBind();
           HighlightSelectedMenuItem();
        }

        private void HighlightSelectedMenuItem()
        {
            string MyURL = Request.Url.AbsoluteUri.ToLower();
            //COMMENTS MS: Logic - If the url contains no aspx, default.aspx is appended to the absolute url
            bool _containsaspx = MyURL.Contains(".aspx");;
            if (!_containsaspx)
            {
                MyURL = Request.Url.AbsoluteUri + "default.aspx";
            }
            foreach (MenuItem mi in NavigationMenu.Items)
            {
                string _navurl = mi.NavigateUrl;
                string _navurl1 = _navurl.Substring(_navurl.IndexOf("/")).ToLower();
                if (_navurl1.IndexOf("_") >= 0)
                {
                    string _navurl2 = _navurl1.Substring(0,9);
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

        // COMMENTS MS: Workaround - Adding this override so that the asp:Menu control renders properly in Safari and Chrome
        // See http://geekswithblogs.net/bullpit/archive/2009/07/08/aspmenu-rendering-problems-in-ie8-safari-and-chrome.aspx
        protected override void AddedControl(Control control, int index)
        {
            string str = Request.ServerVariables["http_user_agent"].ToLower();
            if (str.Contains("safari") || str.Contains("chrome"))
            {
                this.Page.ClientTarget = "uplevel";
            }
            base.AddedControl(control, index);
        }

        protected void NavigationMenu_MenuItemDataBound(object sender, MenuEventArgs e)
        {
 
        }

      

   
       

    }
}
