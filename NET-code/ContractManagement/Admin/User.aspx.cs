//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  Email.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is the code to manager users of this application

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
    public partial class User1 : BasePage
    {
        Business.CMS_Common_Util objCommon = new Business.CMS_Common_Util();
        Data.CMS_DMLUtil objDml = new Data.CMS_DMLUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckIfManager();
            if ((!_admin) && (!_cma)) Response.Redirect("~/Permission.aspx?msg=gen");

            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }

        protected void GvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvUsers.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void DdlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            GridViewRow row = (GridViewRow)ddl.Parent.Parent;

            DropDownList ddlDrt = (DropDownList)row.Cells[2].FindControl("DdlDrt");
            Label LblNodrt = (Label)row.Cells[2].FindControl("LblNoDrt");
            RequiredFieldValidator Rfv = (RequiredFieldValidator)row.Cells[2].FindControl("RfvDesc");

            if ((ddl.SelectedValue == UserType.ALD.ToString()) || (ddl.SelectedValue == UserType.DIRADMIN.ToString()))
            {
                DataTable _dtOrg = null;
                if (ddl.SelectedValue == UserType.ALD.ToString())
                {
                    _dtOrg = ViewState["orgnotinald"] as DataTable;
                }
                else if (ddl.SelectedValue == UserType.DIRADMIN.ToString())
                {
                    _dtOrg = ViewState["org2"] as DataTable;
                }
                ddlDrt.Visible = true;
                if (_dtOrg.Rows.Count > 0)
                {
                    ddlDrt.DataSource = _dtOrg;
                    ddlDrt.DataTextField = "DESCRIPTION";
                    ddlDrt.DataValueField = "ORG_ID";
                    ddlDrt.DataBind();
                }
                ddlDrt.Items.Insert(0, new ListItem("--Choose One--", "0"));
                LblNodrt.Visible = false;
                Rfv.Visible = true;
            }
            else
            {
                ddlDrt.Visible = false;
                LblNodrt.Visible = true;
                Rfv.Visible = false;
            }
        }

       
        private void BindGrid()
        {
            FillUserDetails();
            //Check if ALD is present for all directorates, if so, remove ALD from dropdown
            DataSet _dsOrg = new DataSet();
           _dsOrg = objDml.GetOrgIdNotAdded(UserType.ALD.ToString());
            ViewState["orgnotinald"] = _dsOrg.Tables["orgnotin"];
            if (_dsOrg.Tables["orgnotin"].Rows.Count == 0)
            {
                AddRemoveItemsInList("DdlUserType", UserType.ALD.ToString(), UserType.ALD.ToString(),"remove");
            }
            //Check if DIRADMIN is added for all directorates, if so, remove dIRADMIN from list
  
            _dsOrg = objDml.GetOrg2List();
            ViewState["org2"] = _dsOrg.Tables["org2list"];

        }

        private void FillUserDetails()
        {
            DataSet _dsUser = new DataSet();
            string filter = " ORDER BY " + SortExpression + " " + SortDirect;
            _dsUser = objDml.GetUserInfo(filter);

            if (_dsUser.Tables["user"].Rows.Count > 0)
            {
                GvUsers.DataSource = _dsUser.Tables["user"];
                GvUsers.DataBind();
            }
            else
            {
                GvUsers.DataSource = null;
                GvUsers.DataBind();
            }

        }

        protected void GvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "add")
            {
                if (!Page.IsValid)
                {
                    return;
                }
                TextBox TxtName = (TextBox)GvUsers.FooterRow.FindControl("TxtName");
                DropDownList DdlUserType = (DropDownList)GvUsers.FooterRow.FindControl("DdlUserType");

                string _orgId = "";
                string _drt = "";
                if ((DdlUserType.SelectedValue == UserType.ALD.ToString()) || (DdlUserType.SelectedValue == UserType.DIRADMIN.ToString()))
                {
                    DropDownList DdlDrt = (DropDownList)GvUsers.FooterRow.FindControl("DdlDrt");
                    _orgId = DdlDrt.SelectedValue;
                    _drt = DdlDrt.SelectedItem.Text;
                }
                else
                {
                    _orgId = "0";
                    _drt = "";
                }
                FillUserObject(TxtName.Text, DdlUserType.SelectedItem.Text, _orgId, _drt);
                 
            }
            else if (e.CommandName == "del")
            {
                int _mgrId = Convert.ToInt32(e.CommandArgument);
                string _result = objDml.DeleteUser(_mgrId, objCommon.GetUserID());
                if (_result == "0")
                {
                    objCommon.CreateMessageAlertSM(this, "User Deleted", "info", false);
                    BindGrid();
                }
                else
                {
                    objCommon.CreateMessageAlertSM(this, "Error! User not deleted", "error", false);
                }

            }
        }

        private void FillUserObject(string name, string userType, string orgId, string drt)
        {
            Business.UserRecord objUser = new Business.UserRecord();

            objUser.EmpName = name;
            objUser.MgrType = userType;
            objUser.OrgId = orgId;
            objUser.EmpId = objCommon.GetEmpid(name);
            objUser.CreatedBy = objCommon.GetUserID();

            string _userId = objDml.CreateUser(objUser.MgrType, objUser.EmpId, objUser.OrgId, objUser.CreatedBy);
            if (_userId != "0")
            {
                objCommon.CreateMessageAlertSM(this, "User Added", "info", false);
                BindGrid();
            }
            else
            {
                objCommon.CreateMessageAlertSM(this, "Error! User not added", "error", false);
            }
        }

        protected void GvUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string _drt;

                _drt = rowView["DESCRIPTION"].ToString();
                if (string.IsNullOrEmpty(_drt))
                {
                   Label LblDrt = (Label)e.Row.FindControl("LblDrt");
                   LblDrt.Text = "NA"; 
                }
               
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ImageButton ImgBtn = (ImageButton)e.Row.FindControl("ImgbtnOwn");
                TextBox TxtName = (TextBox)e.Row.FindControl("TxtName");
                if (TxtName != null)
                {
                    ImgBtn.Attributes.Add("onClick", "OpenJQueryDialog('dialogowneradmin', 'TxtName'); return false;");
                }

            }
        }

        protected void GvUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            this.SortExpression = e.SortExpression;
            if (ViewState["sortdirection"].ToString() == "ASC")
            {
                this.SortDirect = "DESC";
            }
            else
            {
                this.SortDirect = "ASC";
            }
            BindGrid();
        }

        protected string SortExpression
        {
            get
            {
                if (null == ViewState["sort"])
                {
                    ViewState["sort"] = "MANAGER_ID";
                }
                return ViewState["sort"].ToString();
            }

            set { ViewState["sort"] = value; }
        }

        protected string SortDirect
        {
            get
            {
                if (null == ViewState["sortdirection"])
                {
                    ViewState["sortdirection"] = "DESC";
                }
                return ViewState["sortdirection"].ToString();
            }
            set
            {
                ViewState["sortdirection"] = value;
            }
        }

        protected void CvOwn_ServerValidate(object source, ServerValidateEventArgs args)
        {
            TextBox TxtName = (TextBox)GvUsers.FooterRow.FindControl("TxtName");
            if (TxtName.Text != "")
            {
                bool _isValid = objDml.CheckValidName(TxtName.Text);
                if (!_isValid)
                {
                    args.IsValid = false;

                }
                else args.IsValid = true;
            }
            else { args.IsValid = true; }

        }
        protected void CvDuplicate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            TextBox TxtName = (TextBox)GvUsers.FooterRow.FindControl("TxtName");
            DropDownList DdlUserType = (DropDownList)GvUsers.FooterRow.FindControl("DdlUserType");
            DropDownList DdlOrg = (DropDownList)GvUsers.FooterRow.FindControl("DdlDrt");
            string _org = (DdlOrg != null)? DdlOrg.SelectedValue.ToString():"";
            if (TxtName.Text != "")
            {
                bool _isDuplicate = objDml.CheckIfDuplicateUser(objCommon.GetEmpid(TxtName.Text).ToString(), DdlUserType.SelectedValue,DdlOrg.SelectedValue);
                if (_isDuplicate)
                {
                    args.IsValid = false;

                }
                else args.IsValid = true;
            }
            else { args.IsValid = true; }

        }

    }
}