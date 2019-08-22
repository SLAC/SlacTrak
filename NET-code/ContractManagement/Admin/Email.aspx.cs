//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  Email.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is the code to set Email notifications

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.OracleClient;


namespace ContractManagement.Admin
{
    public partial class Email : BasePage
    {
        Business.CMS_Common_Util objCommon = new Business.CMS_Common_Util();
        Data.CMS_DMLUtil objDml = new Data.CMS_DMLUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckIfManager();
            if ((!_admin) && (!_cma)) Response.Redirect("~/Permission.aspx?msg=gen");

            if (!Page.IsPostBack)
                {
                    GetEmailSetting();
                 }
            }

        protected void GetEmailSetting()
        {
            Business.EmailSetting objEmail = new Business.EmailSetting();
            objEmail = objDml.GetEmailSetting();
            if (objEmail != null)
            {
                LblCurVal.Text = (objEmail.SendEmail == "Y") ? " Yes " : ((objEmail.SendEmail == "N") ? " No " : "None specified");
                if (objEmail.ModifiedName == "")
                {
                    LblModifiedby.Visible = false;
                    LblModbyVal.Visible = false;
                }
                else
                {
                    LblModifiedby.Visible = true;
                    LblModbyVal.Visible = true;
                    LblModbyVal.Text = (objEmail.ModifiedName == "") ? " NA " : objEmail.ModifiedName;
                }

                if (objEmail.ModifiedOn == DateTime.MinValue)
                {
                    LblModifiedOn.Visible = false;
                    LblModOnVal.Visible = false;
                }
                else
                {
                    LblModifiedOn.Visible = true;
                    LblModOnVal.Visible = true;
                    LblModOnVal.Text = (objEmail.ModifiedOn == DateTime.MinValue) ? string.Empty : Convert.ToDateTime(objEmail.ModifiedOn).ToShortDateString();
                }
               
                
            }
        }

        protected void BtnChange_Click(object sender, EventArgs e)
        {
            string _result;
            string _msg;
            //Toggle update, if email is off, it will turn it on and vice versa
            _result = objDml.UpdateEmailSetting();
            if (_result == "0")
            {
                _msg = "Email setting updated successfully";
            }
            else
            {
                _msg = "Error in updating the email setting. Please try later!";
            }
            objCommon.CreateMessageAlertSM(this, _msg, "info", false);
            GetEmailSetting();
        }

      


    }
}