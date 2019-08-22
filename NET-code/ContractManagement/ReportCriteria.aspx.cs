﻿//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  ReportCriteria.aspx.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is for report page
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace ContractManagement
{
    public partial class ReportCriteria : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ImgbtnOwn.Attributes.Add("onClick", "OpenJQueryDialog('dialogowner1','TxtOwnerrep'); return false;");
            ImgBtnSO.Attributes.Add("onClick", "OpenJQueryDialog('dialogowner1','TxtSubowner'); return false;");
            ImgBtnApp.Attributes.Add("onClick", "OpenJQueryDialog('dialogowner1','TxtApprover'); return false;");
          
            if (!Page.IsPostBack)
            {


                if (!(IsSpecial()))
                {
                    SDSStatus.SelectCommand = "SELECT STATUS_ID,STATUS_DESC FROM CMS_STATUS WHERE IS_ACTIVE='Y' AND LOWER(STATUS_DESC) NOT IN ('approved by default')";
                    ChkStatus.DataBind();
                //    Response.Redirect("Permission.aspx?msg=nopermview");
                    trContract.Visible = false;
                    trDrt.Visible = false;
                    trDept.Visible = false;
                    trNotify.Visible = false;
                }
                //else
                //{
                    SetDept();
                //}
            }
        }

    
        protected void DdlDirectorate_DataBound(object sender, EventArgs e)
        {
            DdlDirectorate.Items.Insert(0, new ListItem("All", "0"));
        }

     

        protected void ChkStatus_DataBound(object sender, EventArgs e)
        {
            ChkStatus.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void DdlNotification_DataBound(object sender, EventArgs e)
        {
            DdlNotification.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void ChkLstType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void DdlDirectorate_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDepartment();
            SetDept();
        }

        private void SetDept()
        {
            AddRemoveItemsInList("DdlDepartment", "0", "--Choose One--","remove");
            AddRemoveItemsInList("DdlDepartment", "0", "All");

        }

        private string ConvertToStringList(string field,string item="value")
        {
            StringBuilder _sbList = new StringBuilder();
            CheckBoxList _chkList = new CheckBoxList();

            if (field == "type")
            {
                _chkList = (CheckBoxList)FindControlRecursive("ChkLstType");
            }
            else if (field == "status")
            {
                _chkList = (CheckBoxList)FindControlRecursive("ChkStatus");
            }
            foreach (ListItem li in _chkList.Items)
            {
                if ((li.Selected) && (li.Value == "0"))
                {
                    if (item == "name")
                    {                       
                        _sbList.Append(li.Text);
                    }
                    else
                    {
                        _sbList.Append(li.Value);
                                     }
                    return _sbList.ToString();
                }
                else if (li.Selected)
                {
                    if (item == "name")
                    {
                        _sbList.Append(li.Text);
                    }
                    else
                    {
                        _sbList.Append(li.Value);
                                             }
                    _sbList.Append(",");
                }
            }
            if (!(IsSpecial()) && (field == "status"))
            {
                if (_sbList.ToString().Contains("4"))
                {
                    _sbList.Append("6,");
                }
            }

            if (_sbList.Length != 0)
            {
                _sbList.Remove(_sbList.Length - 1, 1);
               
            }
            return _sbList.ToString();
            
        }

      

        protected void ChkLstType_DataBound(object sender, EventArgs e)
        {
            ChkLstType.Items.Insert(0, new ListItem("All", "0"));
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            Server.Transfer("Report.aspx");
           
                  }

        public string TrackId
        {
            get
            {
                return TxtTrackId.Text;
            }
        }

        public string DeliType
        {          
            get
            {
                return ConvertToStringList("type");
            }
        }


        public string Desc
        {
            get
            {
                return TxtReqDesc.Text;
            }
        }

        public string Owner
        {
            get
            {
                return TxtOwnerrep.Text;
            }
        }

        public string Drt
        {
            get
            {
                return DdlDirectorate.SelectedValue;
            }

        }

        public string DrtName
        {
            get { return DdlDirectorate.SelectedItem.Text; }
        }

        public string Dept
        {
            get { return DdlDepartment.SelectedValue; }
        }

        public string DeptName
        {
            get { 
                
                return DdlDepartment.SelectedItem.Text; }
        }

        public string Subowner
        {
            get { return TxtSubowner.Text; }
        }

        public string DueDateFrom
        {
            get { return TxtFromDueDate.Text; }
        }

        public string DueDateTo
        {
            get { return TxtToDueDate.Text; }
        }

        public string SubmittedFrom
        {
            get { return TxtFromSubmit.Text; }
        }

        public string SubmittedTo
        {
            get { return TxtToSubmit.Text; }
        }

        public string DeliStatus
        {
            get { return ConvertToStringList("status"); }
        }

        public string Approver
        {
            get { return TxtApprover.Text; }
        }

        public string IsInfo
        {
            get
            {
                if (ChkInfo.Checked)
                {
                    return "Y";
                }
                else
                {
                    return "N";
                }
            }
        }
        
        public string NotifySchedule
        {
            get
            {
                return DdlNotification.SelectedValue;
            }
        }

        public string NotifyScheduleDesc
        {
            get
            {
                return DdlNotification.SelectedItem.Text;
            }
        }

        public string TypeName
        {
            get { return ConvertToStringList("type","name"); }
        }

        public string StatusName
        {
            get { return ConvertToStringList("status","name"); }
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            TxtTrackId.Text = "";

            foreach (ListItem li in ChkLstType.Items)
            {
                if (li.Selected)
                {
                    li.Selected = false;
                }
            }

            TxtReqDesc.Text = "";
            TxtOwnerrep.Text = "";
            DdlDirectorate.SelectedValue = "0";
            DdlDepartment.SelectedValue = "0";
            TxtSubowner.Text = "";
            TxtFromDueDate.Text = "";
            TxtFromSubmit.Text = "";
            TxtToDueDate.Text = "";
            TxtToSubmit.Text = "";
             
            foreach(ListItem li2 in ChkStatus.Items)
            {
                if (li2.Selected)
                {
                    li2.Selected = false;
                }
            }

            TxtApprover.Text = "";
            ChkInfo.Checked = false;
            DdlNotification.SelectedValue = "0";

        }

    }
}