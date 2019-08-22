//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  Deliverable.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//  This is the codebehind for all changes to the deliverable object
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
using System.Web.Services;
using System.Text;


namespace ContractManagement
{
    public partial class About : BasePage
    {
        Data.CMS_DataUtil objData = new Data.CMS_DataUtil();
        Data.CMS_DMLUtil objDml = new Data.CMS_DMLUtil();
        Business.CMS_Common_Util objCommon = new Business.CMS_Common_Util();
        public string Duedate;
     

        # region "Page setup"
        protected void Page_Load(object sender, EventArgs e)
        {
             string _mode;
            ImgbtnOwn.Attributes.Add("onClick", "OpenJQueryDialog('dialogowner','TxtOwner'); return false;");
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            string[] _modeArray = { " ", "list", "add", "edit", "view", "reviewadd", "reviewedit", "addset", "editset" };
            
            if (!Page.IsPostBack)
            {
                
                string _deliverId;
                string _reqId;
               

                _mode = Request.QueryString["mode"];
                _deliverId = Request.QueryString["id"];
                _reqId = Request.QueryString["reqid"];
                ViewState["reqid"] = _reqId;
                if (_mode != "edit")
                {
                    if (Request.UrlReferrer != null)
                    {
                        Session["previouspage"] = Request.UrlReferrer;
                    }
                    else { Session["previouspage"] = "Deliverable_List.aspx"; }
                }
               

                //Comments MS: Security - check if mode has only lowercase letters
                if (Regex.IsMatch(_mode, @"^[a-z]+$"))  
                {
                    //Comments MS: Security - Mode should have one of the items in string array
                    int pos = Array.IndexOf(_modeArray, _mode);

                    if (pos > -1)
                    {
                        CheckIfManager();
                        if ((_deliverId != "") && (_deliverId != null))   
                        {
                            //Comments MS:check if deliverable id has only numbers
                            if (Regex.IsMatch(_deliverId, "^[0-9]+$"))
                            {
                                HdnDeliverableId.Value = _deliverId;
                                CheckIfValidUser(_deliverId);
                                ViewState["owner"] = _owner;
                                ViewState["subowner"] = _subowner;
                                Session["so"] = _subowner;
                            }
                            else
                            {
                                //COMMENTS MS: For Security, if delverable doesn't match the regex, redirect to custom msg
                                RedirectInvalidParam();
                            }
                        }
                     

                        if ((_mode == "add") || (_mode == "edit"))
                        {
                            if ((!_admin) && (!_cma)) Response.Redirect("Permission.aspx?msg=nopermadd");

                        }
                        else if (_mode == "view")
                        {
                            if ((!_admin) && (!_cma) && (!_ald) && (!_ssoallow) && (!_diradmin) && (!_owner) && (!_subowner) && (!_ssosuper))
                                Response.Redirect("Permission.aspx?msg=nopermview");

                            if ((_sso) || (_ssosuper))
                            {
                                string _errCode = objDml.AddSSOLog(Convert.ToInt32(_deliverId), "Viewed deliverable details", Convert.ToInt32(objCommon.GetUserID()), objCommon.GetUserID());
                                if (_errCode != "0")
                                {
                                    //TODO:log into log4net
                                }
                            }
                        }
                        SetPage(_mode);
                    }
                    else { RedirectInvalidParam(); }
                    
                }

                else
                {
                    RedirectInvalidParam(); //COMMENTS MS: For Security, if mode doesn't match the regex, redirect to custom msg
                }

            }
            
        }


      

        protected void Page_Init(object sender, EventArgs e)
        {
            smDeliv.RegisterPostBackControl(BtnReview);
            smDeliv.RegisterPostBackControl(BtnReview2);
            smDeliv.RegisterAsyncPostBackControl(TB1);

        }

        private void ToggleVisibility(bool boolval)
        {
            //It doesn't work if done through Jquery so adding this thru code behind.
            DivTypeAdd.Visible = boolval;
            DivTypeView.Visible = !boolval;
            SpnType.Visible = boolval;                  
        }

        private void Togglespecial(bool boolval)
        {
            trId.Visible = false;
            trStatus.Visible = boolval;
            trTrack.Visible = boolval;
            trClauseDetails.Visible = boolval;
        }

        private void ToggleViewEdit(bool boolval)
        {
            DivDescAdd.Visible = boolval;
            DivDescView.Visible = !boolval;
            DivDirectorateAdd.Visible = boolval;
            DivDirectorateView.Visible = !boolval;
            DivDepartmentAdd.Visible = boolval;
            DivDepartmentView.Visible = !boolval;
            DivDueDateAdd.Visible = boolval;
            DivDueDateView.Visible = !boolval;
            DivApproversAdd.Visible = boolval;
            DivApproversView.Visible = !boolval;
            DivOwnerAdd.Visible = boolval;
            DivOwnerView.Visible = !boolval;
            DivSubOwnerAdd.Visible = boolval;
            DivSubOwnerView.Visible = !boolval;
            DivUploadReqAdd.Visible = boolval;
            DivUploadReqView.Visible = !boolval;
            DivNotifyMgrAdd.Visible = boolval;
            DivNotifyMgrView.Visible = !boolval;
            DivApproversAdd.Visible = boolval;
            DivApproversView.Visible = !boolval;
            DivNotifyScheduleAdd.Visible = boolval;
            DivNotifyScheduleView.Visible = !boolval;
            DivInfoOnlyAdd.Visible = boolval;
            DivInfoOnlyView.Visible = !boolval;
            Divreq.Visible = boolval;          
            divFile.Visible = !boolval;
         
            SpnAppr.Visible = boolval;
            SpnDept.Visible = boolval;
            SpnDrt.Visible = boolval;
            SpnDuedate.Visible = boolval;
            SpnOwn.Visible = boolval;
            SpnReq.Visible = boolval;
            SpnReceipt.Visible = boolval;
            Spnsched.Visible = boolval;          
            SpnDesc.Visible = false;
               DivDeliFreqAdd.Visible = boolval;
                DivDeliFreqView.Visible = !boolval;
                SpnDeliFreq.Visible = boolval;
 
        }

        private void SetPageForAdd(string reqid)
        {
            DivTypeView.Visible = true;
            DivTypeAdd.Visible = false;
            SpnType.Visible = false;
            SpnReq.Visible = false;
            trRequirement.Visible = true;
            DivReqAdd.Visible = false;
            DivReqView.Visible = true;
            LblRequirementVal.Text = "";
            trClauseDetails.Visible = true;
            LblClauseVal.Text = GetClauseNameNo(ViewState["reqid"].ToString());           
            SetOwnerDrtDept(ViewState["reqid"].ToString(),true);
           
        }

        private void SetPage(string mode)
        {
            string _previouspage = "";

            if (null != Session["previouspage"])
            { _previouspage = Session["previouspage"].ToString(); }
            ViewState["mode"] = mode;
            if (mode != "")
            {
               // ScriptManager.RegisterStartupScript(this, GetType(), "key", "togglecontrolvisibility('" + mode + "');", true);
                if (mode == "add") 
                {
                    LblSubTitle.Text = "New Deliverable";
                    LnkBack.Visible = true;
                    ToggleVisibility(true);
                    Togglespecial(false);
                    ToggleViewEdit(true);
                    DivReview.Visible = true;
                    DivReviewedit.Visible = false;
                   
                    DivSubmit.Visible = false;
                    DivUpdate.Visible = false;
                   
                    FillDropdownLists();

                    if (HdnDeliverableId.Value != "")
                    {
                        Business.DeliverableRecord objDeli = new Business.DeliverableRecord();

                        List<Business.DeliverableApprovers> approverList = new List<Business.DeliverableApprovers>();
                        List<Business.DeliverableNotification> notificationList = new List<Business.DeliverableNotification>();
                        List<Business.SubOwners> subOwnersList = new List<Business.SubOwners>();

                        objDeli = objDml.GetDeliverabledetails(HdnDeliverableId.Value);
                        if (objDeli.DeliverableId != 0)
                        {
                            approverList = objDml.GetApprovers(HdnDeliverableId.Value);
                            notificationList = objDml.GetNotificationSchedule(HdnDeliverableId.Value);
                        }
                        FillFormEdit(objDeli, subOwnersList, notificationList, approverList,"clone");
                      
                    }


                    if (ViewState["reqid"] != null)
                    {
                        if (ViewState["reqid"].ToString() != "")
                        {
                            SetPageForAdd(ViewState["reqid"].ToString());
                        }
                    }
                  
                }
                else if (mode == "addset")
                {
                    LnkBack.Visible = false;
                    LblSubTitle.Text = "New Deliverable";
                    if ((ViewState["reqid"] != null) && (ViewState["reqid"].ToString() != ""))
                    {
                        ToggleVisibility(false);
                    }
                    else
                    {
                        ToggleVisibility(true);
                    }
                    Togglespecial(false);
                    ToggleViewEdit(true);
                    DivReview.Visible = true;
                    DivReviewedit.Visible = false;
                    
                    int _groupId = objDml.GetGroupID(Convert.ToInt32(DdlType.SelectedValue));
                    if (_groupId == 2)
                    {
                        DivReqAdd.Visible = true;
                        DivReqView.Visible = false;
                        SpnReq.Visible = true;      
                    }
                    DivSubmit.Visible = false;
                    DivUpdate.Visible = false;
                   
                }
                else if (mode == "editset")
                {
                    LblSubTitle.Text = "Deliverable";
                    LnkBack.Visible = false;
                    ToggleVisibility(false);
                   Togglespecial(true);
                    ToggleViewEdit(true);
                    
                   DivReview.Visible = false;
                   DivReviewedit.Visible = true;
                    DivSubmit.Visible = false;
                    DivUpdate.Visible = false;
                    SpnReq.Visible = false;

                    if (!trRequirement.Visible)
                    {
                        trClauseDetails.Visible = false;
                    }
                }
                else if ((mode == "view") || (mode == "edit"))
                {
                    LblSubTitle.Text = "Deliverable";
                    LnkBack.Visible = true;
                    ToggleVisibility(false);
                    Togglespecial(true);
                    DivReview.Visible = false;
                   
                    DivSubmit.Visible = false;
                    DivUpdate.Visible = false;
                    Business.DeliverableRecord objDeli = new Business.DeliverableRecord();

                    List<Business.DeliverableApprovers> approverList = new List<Business.DeliverableApprovers>();
                    List<Business.DeliverableNotification> notificationList = new List<Business.DeliverableNotification>();
                    List<Business.SubOwners> subOwnersList = new List<Business.SubOwners>();

                    objDeli = objDml.GetDeliverabledetails(HdnDeliverableId.Value);

                    //COMMENTS MS: Validation - Check if the Id has a valid value
                    if (objDeli.DeliverableId != 0)
                    {
                        approverList = objDml.GetApprovers(HdnDeliverableId.Value);
                        notificationList = objDml.GetNotificationSchedule(HdnDeliverableId.Value);

                        //COMMENTS MS: Per Requirement, if it is a owner or subowner and status is new, Update status to inprogress
                        if (((_owner) || (_subowner)) && (objDeli.StatusId == (int)Status.New))
                        {
                            string _result;
                            _result = objDml.UpdateStatus(objDeli.DeliverableId, (int)Status.InProgress, "", objCommon.GetUserID(),"N");
                            if (_result == "0")
                            {
                                objDeli.StatusId = (int)Status.InProgress;
                                objDeli.Status = "In Progress";
                            }

                        }
                        if (mode == "view")
                        {
                            ToggleViewEdit(false);
                            DivReviewedit.Visible = false;
                            FillFormView(objDeli, subOwnersList, notificationList, approverList, mode);
                        }
                        else
                        {
                            ToggleViewEdit(true);
                            DivReviewedit.Visible = true;
                            FillDropdownLists();
                            FillFormEdit(objDeli, subOwnersList, notificationList, approverList,mode);

                        }
                    }
                    else { RedirectInvalidParam("noobj"); }
                }

                else
                {
                    LblSubTitle.Text = "Deliverable";
                    ToggleViewEdit(false);
                    LnkBack.Visible = false;
                    DivReview.Visible = false;
                    DivReviewedit.Visible = false;
                    if (mode == "reviewadd")
                    {
                        ToggleVisibility(false);
                        Togglespecial(false);
                       
                        DivSubmit.Visible = true;
                        DivUpdate.Visible = false;
                        if ((CheckIfEmailOn() == "Y") && ((bool)Session["cma"]))
                        {
                            BtnSave.Attributes.Add("onClick", "return OpenConfirmDialog('dialogconfirmemail');");
                        }
                    }
                    else if (mode == "reviewedit")
                    {
                        ToggleVisibility(false);
                        Togglespecial(true);
                        DivSubmit.Visible = false;
                        DivUpdate.Visible = true;
                        if ((CheckIfEmailOn() == "Y") && ((bool)Session["cma"]))
                        {
                            BtnUpdate.Attributes.Add("onClick", "return OpenConfirmDialog('dialogconfirmemailupdate');");
                        }
                    }
                     
                }
            }
            else
            {
               // ScriptManager.RegisterStartupScript(this, GetType(), "key", "togglecontrolvisibility('view');", true);
            }

            if (LnkBack.Visible)
            {
                if ((_previouspage.Contains("Report")) && (_previouspage.Contains("page=deli") || _previouspage.Contains("page=cl") || _previouspage.Contains("page=fy")))
                {
                    LnkBack.Attributes.Add("onClick", "javascript:history.back(); return false;");
                }
                else if (_previouspage.Contains("Report"))
                {
                   // LnkBack.Attributes.Add("onClick", "javascript:history.back(); return false;");
                    LnkBack.PostBackUrl = "Report.aspx?page=back";
                }
                else if (_previouspage.Contains("Deliverable_List"))
                {
                    LnkBack.PostBackUrl = "Deliverable_List.aspx?page=back";

                }
                else
                {
                    LnkBack.PostBackUrl = "Default.aspx";
                }
            }
                
          
        }

        private void SetOwnerDrtDept(string requirementId, Boolean IsFromReq = false )
        {
            //Comment MS: Per Requirement, Get the owner based on the clause. if subclause, get the owner of the clause under which it is subclause
            string _clauseOwner = "";
            string _frequency = "";
            string _uploadFileReq = "N";
            string _freqname = "";
            string _contractname = "";
            string _requirement = "";


            if ((requirementId != "") && (requirementId != "0"))
            {
                using (OracleDataReader _drClause = objDml.GetClauseInfo(requirementId))
                {
                    while (_drClause.Read())
                    {
                        _clauseOwner = objCommon.FixDBNull(_drClause["OWNERNAME"]);
                        _frequency = objCommon.FixDBNull(_drClause["FREQUENCY_ID"]);
                        _uploadFileReq = objCommon.FixDBNull(_drClause["UPLOAD_FILE_REQUIRED"]);   
                        _contractname = objCommon.FixDBNull(_drClause["CONTRACT_NAME"]);
                        _requirement = objCommon.FixDBNull(_drClause["REQUIREMENT"]);
                    }
                }
            }
            else
            {
                _clauseOwner = objCommon.GetEmpname(objCommon.GetUserID());
  
            }

            //set the owner           
            TxtOwner.Text = _clauseOwner;
            ChkUploadReq.Checked = (_uploadFileReq == "Y") ? true : false;
            SetOrgBasedOnUser(_clauseOwner);
            FillNotificationSchedule(_frequency);
  
            if (IsFromReq)
            {
                LblTypeVal.Text = _contractname;
                LblRequirementVal.Text = _requirement;

            }
            
        }

        private bool ValidateOnServerside()
        {
            if ((TB1.TBControlsCount + 1) >= 0)
            {

                bool result = ValidateAllSubowners(TB1.TBControlsCount + 1);
                if (!result)
                {
                    return false;

                }
            }
            if (trRequirement.Visible)
            {
                if (DivReqAdd.Visible)
                {
                    if (DdlRequirement.SelectedIndex.ToString() == "-1")
                    {
                        cvRequirment.IsValid = false;
                        return false;
                    }
                }
            }
            return true;

            //Add all other validation

        }
        protected void CvOwn_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (TxtOwner.Text != "")
            {
                bool _isValid = objDml.CheckValidName(TxtOwner.Text);
                if (!_isValid)
                {
                    args.IsValid = false;

                }
                else args.IsValid = true;
            }
            else { args.IsValid = true; }

        }


       

        private void FillFormView(Business.DeliverableRecord objDeli, List<Business.SubOwners> subOwnersList, List<Business.DeliverableNotification> notificationList, List<Business.DeliverableApprovers> approversList, string mode)
        {
            CheckIfManager();
            if (mode != "reviewadd")
            {
                LblIdVal.Text = objDeli.DeliverableId.ToString();
                LblTrackIdVal.Text = objDeli.CompositeKey;
                //check if the user is CMA
                if ((mode == "view") && (_cma || _admin))      //Comments MS: Per Requirement, Only CMA or superadmin can edit when status is open (New,inprogress,reopen)
                {
                    if ((objDeli.StatusId == (int)Status.New) || (objDeli.StatusId == (int)Status.InProgress) || (objDeli.StatusId == (int)Status.Reopened))
                    {
                        DivEdit.Visible = true;
                        DivDelete.Visible = true;
                    }
                    else { DivEdit.Visible = false; DivDelete.Visible = false; }
                    DivClone.Visible = true;
                    DivApprove.Visible = false; DivReject.Visible = false;
                }
                else { DivEdit.Visible = false; DivDelete.Visible = false; DivClone.Visible = false; 
                //DEV-4338 - Move Approve/Reject to Individual Deliverable page
                //If sso approver for this deliverable, approve and reject buttons are visible
                if ((objDml.IsSSOApprover (objDeli.DeliverableId.ToString(), objCommon.GetUserID ())) && (objDeli.StatusId == (int)Status.submitted))
                {
                    //check if the user is sso approver
                    DivApprove.Visible = true;
                    DivReject.Visible = true;
                    BtnReject.Attributes.Add("onClick", "OpenDialogForReject(); return false;");
                   
                }
                else { DivApprove.Visible = false; DivReject.Visible = false; }
                
                }
            }
            else { DivEdit.Visible = false; DivDelete.Visible = false; DivClone.Visible = false; DivApprove.Visible = false; DivReject.Visible = false; }

            // SSO can see only certain items in the list 
            LblTypeVal.Text = objDeli.TypeName;
            int _groupId = objDml.GetGroupID(objDeli.TypeId);
            if (_groupId == 3)
            {
                trRequirement.Visible = false;
            }
            else
            {
                trRequirement.Visible = true;
                DivReqAdd.Visible = false;
                DivReqView.Visible = true;
                LblRequirementVal.Text = objDeli.Requirement;
             }
            LblFrequencyVal.Text = objDeli.Frequency;
            if ((trRequirement.Visible) && (mode != "reviewadd"))
            {
                trClauseDetails.Visible = true;
                LblClauseVal.Text = GetClauseNameNo(objDeli.RequirementId.ToString());
              
            }
            else { trClauseDetails.Visible = false;  }

            LblDescriptionVal.Text = objDeli.Description;
          
            LblDueDateVal.Text = objDeli.DueDate.ToShortDateString();
            if ((bool)Session["sso"])
            {
                trNotifyMgr.Visible = false;
                trUploadReq.Visible = false;
            }
            else
            {
                trNotifyMgr.Visible = true;
                trUploadReq.Visible = true;
                LblUploadReqVal.Text = objDeli.UploadFileRequired;
                LblNotifyMgrVal.Text = objDeli.NotifyManager;
            }
           
            ViewState["uploadfile"] = objDeli.UploadFileRequired;           
            LblInfoOnlyVal.Text = objDeli.IsInformationOnly;
            ViewState["info"] = objDeli.IsInformationOnly;
            //IF SSO or SSOSuper and owners/subowners, Approved by Default should be displayed as Approved
            //Isspecial() checks if the user is cma,admin, diradmin, ald
            if ((!IsSpecial())  && (objDeli.StatusId == 6))
            {
                LblStatusVal.Text = Status.Approved.ToString();
            }
            else
            {
                LblStatusVal.Text = objDeli.Status;
            }
            ViewState["status"] = objDeli.StatusId;
            ToggleOnStatus(true, objDeli.StatusId);
            if (objDeli.DateSubmitted != null)
            {
                if (objDeli.DateSubmitted == DateTime.MinValue)
                {
                    LblDateSubmitVal.Text = "NA";
                }
                else
                {
                    LblDateSubmitVal.Text = objDeli.DateSubmitted.ToShortDateString();
                }
            }
            if (objDeli.DateApproved != null)
            {
                if (objDeli.DateApproved == DateTime.MinValue)
                {
                    LblDateApprovedVal.Text = "NA";
                                    }
                else
                {
                    LblDateApprovedVal.Text = objDeli.DateApproved.ToShortDateString();
                    ViewState["dateapproved"] = objDeli.DateApproved.ToShortDateString();
                }
               
            }
           
            LblReasonRejectVal.Text = objDeli.ReasonForRejection;

            LblOwnerVal.Text = "";
            if (mode == "reviewedit")
            {
                SubOwners("reviewedit", subOwnersList);              
            }
            else if (mode == "reviewadd")
            {
                SubOwners("reviewadd", subOwnersList);
            }
            else { SubOwners("view", subOwnersList); ; }

            
            LblApproverVal.Text = "";
            foreach (Business.DeliverableApprovers objApprover in approversList)
            {
                if (LblApproverVal.Text == "") { LblApproverVal.Text = objApprover.ApproverName; }
                else
                {
                    LblApproverVal.Text += "<br />" + objApprover.ApproverName;
                }
            }

            //~~~~~~~~~~~~~~~~~~~~~~~~~Change Request -UAT~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //Comments MS: CMA doesn't want Directorate, Department and Notification Schedule
            // to be visible to all other Users 
            if ((!(bool)Session["cma"]) && (!(bool)Session["admin"]))
            {
               
                trDrt.Visible = false;
                trDept.Visible = false;
                trNotify.Visible = false;
            }
            else
            {
                trDrt.Visible = true;
                trDept.Visible = true;
                trNotify.Visible = true;
            }
            if (trNotify.Visible)
            {
                LblNotifySchedVal.Text = "";
                foreach (Business.DeliverableNotification objNotification in notificationList)
                {
                    if (LblNotifySchedVal.Text == "") { LblNotifySchedVal.Text = objNotification.LookupDesc; }
                    else { LblNotifySchedVal.Text += "<br />" + objNotification.LookupDesc; }
                }
                if (LblNotifySchedVal.Text == "")
                {
                    LblNotifySchedVal.Text = "Not Set";
                }
            }
            if (trDrt.Visible)
            {
                LblDirectorateVal.Text = objDeli.DrtName;
            }
            if (trDept.Visible)
            {
                LblDepartmentVal.Text = objDeli.DeptName;
            }
            //~~~~~~~~~~~~~~~~~~~~~~~~~ End  - Change Request -UAT~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~


            //File upload
            //if owner, subowner,cma,admin - add document is visible
            //if above list or ald, diradmin, sso - can see list of documents uploaded

            if ((objDeli.StatusId == (int)Status.New) || (objDeli.StatusId == (int)Status.InProgress) || (objDeli.StatusId == (int)Status.Reopened))
            {
                if (((bool)ViewState["owner"]) || ((bool)ViewState["subowner"]) || (_cma) || (_admin))
                {
                    BtnUpload.Visible = true;
                    spnPdfmsg.Visible = true;

                    if ((bool)ViewState["owner"] && (!_cma) && (!_admin))
                    {
                        LnkEdit.Visible = true;
                        if (HdnSOCount.Value != "")
                        {
                            if (Convert.ToInt32(HdnSOCount.Value) > 1)
                            {
                                LnkEdit.Text = "Edit/Add";
                            }
                            else { LnkEdit.Text = "Add"; }
                        }
                        
                      

                    }
                    else { LnkEdit.Visible = false; }

                    //Comments MS: Per Requirement, if the status is inprogress or reopened and the user is the owner, then allow them to submit the deliverable
                    if ((objDeli.StatusId == (int)Status.InProgress) || (objDeli.StatusId == (int)Status.Reopened))
                    {
                        if ((bool)ViewState["owner"] || (_cma) || (_admin))
                        {
                            DivSubmitDeli.Visible = true;
                        }
                        else { DivSubmitDeli.Visible = false; }

                        if ((bool)ViewState["subowner"])
                        {
                            DivNotifyOwners.Visible = true;
                        }
                        else
                        {
                            DivNotifyOwners.Visible = false;
                        }
                    }
                }
                else
                {
                    BtnUpload.Visible = false;
                    spnPdfmsg.Visible = false;
                }
            }
            else { BtnUpload.Visible = false; spnPdfmsg.Visible = false; }

            if (divFile.Visible)
            {
                BindFileGrid();
            }

            //Comments MS: Per DEV-4128, Show popup message if the deliverable is not due over 30 days from now for frequency < annual
            double diffDays = (objDeli.DueDate - DateTime.Now).TotalDays;
            if ((diffDays > 30) && ((objDeli.FrequencyId == 3) || (objDeli.FrequencyId == 4) || (objDeli.FrequencyId == 5) || (objDeli.FrequencyId == 6)))
            {
                BtnSubmitDeli.Attributes.Add("onClick", "return OpenConfirmDialog('dialog-confirm');");
                if (BtnUpload.Visible)
                {
                   BtnUpload.Attributes.Add("onClick", "return OpenConfirmDialog('dialogconfirmfile');");
                 }
                Duedate = objDeli.DueDate.ToShortDateString();
            }
            else
            {
                BtnSubmitDeli.Attributes.Add("onClick", "return OpenConfirmDialog('dialogwarn');");
                if (BtnUpload.Visible)
                    BtnUpload.Attributes.Add("onClick", "OpenJQueryFileDialog('dialogfile', '" + HdnDeliverableId.Value + "'); return false;");
            }
            
         }
        private void ToggleOnStatus(bool boolval, int statusId)
        {
            if (boolval)
            {
                switch (statusId)
                {
                    case (int)Status.submitted:
                        trDateSubmit.Visible = boolval;
                        trDateApproved.Visible = !boolval;
                        trRejectReason.Visible = !boolval;
                        break;
                    case (int)Status.Approved: case (int)Status.ApprovedbyDefault:
                        trDateSubmit.Visible =  boolval;
                        trDateApproved.Visible = boolval;
                        trRejectReason.Visible = !boolval;
                        break;
                    case (int)Status.Reopened:
                        trDateSubmit.Visible = boolval;
                        trDateApproved.Visible = !boolval;
                        trRejectReason.Visible = boolval;
                        break;
                      default:
                        trDateSubmit.Visible = !boolval;
                        trDateApproved.Visible = !boolval;
                        trRejectReason.Visible = !boolval;
                        break;
                }
            }
            else
            {
                trDateSubmit.Visible = boolval;
                trDateApproved.Visible = boolval;
                trRejectReason.Visible = boolval;
            }

        }

        private void SubOwners(string mode, List<Business.SubOwners> subOwnersList,bool special=false )
        {
            //Comments MS: Dont get the subowner if reviewadd or reviewedit as it has to be the one on the page before
            if (HdnDeliverableId.Value != "")
            {
                if ((mode != "reviewedit") && (mode != "reviewadd"))
                {
                    subOwnersList = objDml.GetSubowners(HdnDeliverableId.Value);
                }
            }
            HdnSOCount.Value = subOwnersList.Count.ToString();
            if ((mode == "view") || (mode == "reviewedit") || (mode == "reviewadd"))
            {
                DivSubOwnerView.Visible = true;
                DivSubOwnerAdd.Visible = false;
                LblSubOwnerVal.Text = "";
                foreach (Business.SubOwners objSubowner in subOwnersList)
                {
                    if (objSubowner.IsOwner == "Y")
                    {
                        LblOwnerVal.Text = objSubowner.Name;
                    }

                    else
                    {

                        LblSubOwnerVal.Text += objSubowner.Name + "<br/>";

                    }
                }
            }
            else if (mode == "edit")
            {
                Session["solist"] = null;
                DivSubOwnerView.Visible = false;
                DivSubOwnerAdd.Visible = true;
                TextBox TxtSubOwner;
                Panel pnlsubowner = new Panel();
                ContentPlaceHolder cphContent = (ContentPlaceHolder)Page.Master.FindControl("MainContent");
                User_Controls.DynamicTextBox ucdb = (User_Controls.DynamicTextBox)cphContent.FindControl("TB1");
                pnlsubowner = (Panel)ucdb.FindControl("pnlsubowner");

                int cnt = -1;
                StringBuilder _sbOwnerlist = new StringBuilder();
                if (subOwnersList.Count < 2)
                    {
                        TB1.TBControlsCount = 0;
                    }
                    else { TB1.TBControlsCount = (subOwnersList.Count - 2); }
                    if (TB1.TBControlsCount > 0)
                    {
                        TB1.CreateTextBox();
                    }
                    foreach (Business.SubOwners objSubowner in subOwnersList)
                    {
                   
                            if (objSubowner.IsOwner == "Y")
                            {
                                if (special)        //COMMENTS MS: Workaround, if the entry point is Edit subowner link, it is a special case
                                {
                                    if (((bool)ViewState["owner"]) && (!_cma))  //COMMENTS MS: For cma, since edit is allowed, no need to allow to edit the subowner
                                    {
                                        LblOwnerVal.Text = objSubowner.Name;
                                    }
                                    else
                                    {
                                        TxtOwner.Text = objSubowner.Name;
                                    }
                                }
                                else { TxtOwner.Text = objSubowner.Name; }
                                ViewState["prevowner"] = objSubowner.SlacId;
                            }
                            else
                            {
                                cnt = cnt + 1;
                                
                                ucdb = (User_Controls.DynamicTextBox)cphContent.FindControl("TB1");
                                TxtSubOwner = (TextBox)pnlsubowner.FindControl("txt" + cnt.ToString());
                                TxtSubOwner.Text = objSubowner.Name;
                                _sbOwnerlist.Append(objSubowner.SlacId);
                                _sbOwnerlist.Append(",");
                            }

                           

                }
                    if (_sbOwnerlist.Length > 0)
                    {
                        _sbOwnerlist.Remove(_sbOwnerlist.Length - 1, 1);
                        Session["solist"] = _sbOwnerlist.ToString();
                    }
            }
        }

        private void FillFormEdit(Business.DeliverableRecord objDeli,  List<Business.SubOwners> subOwnersList, List<Business.DeliverableNotification> notificationList, List<Business.DeliverableApprovers> approversList,string mode)
        {
            int _groupId = objDml.GetGroupID(objDeli.TypeId);
            if (mode == "clone")
            {
                if (DdlType.Items.FindByValue(objDeli.TypeId.ToString()) != null)
                {
                    DdlType.ClearSelection();
                    DdlType.Items.FindByValue(objDeli.TypeId.ToString()).Selected = true;
                }
                else { DdlType.SelectedIndex = 0; }
                if (_groupId == 2)
                {
                    trRequirement.Visible = true;
                    DivReqAdd.Visible = true;
                    DivReqView.Visible = false;
                    SpnReq.Visible = true;
                    FillRequirement();
                    if (DdlRequirement.Items.FindByValue(objDeli.RequirementId.ToString()) != null)
                    {
                        DdlRequirement.ClearSelection();
                        DdlRequirement.Items.FindByValue(objDeli.RequirementId.ToString()).Selected = true;
                    }
                 }
                else
                {
                    trRequirement.Visible = false;
 
                }   
                TxtDueDate.Text = "";
            }
            else
            {
                LblIdVal.Text = objDeli.DeliverableId.ToString();
                LblTrackIdVal.Text = objDeli.CompositeKey;
                LblTypeVal.Text = objDeli.TypeName;
                HdnTypeId.Value = objDeli.TypeId.ToString();
                LblStatusVal.Text = objDeli.Status;
                ToggleOnStatus(false, objDeli.StatusId);
                TxtDueDate.Text = objDeli.DueDate.ToShortDateString();
               
                if (_groupId == 3)
                {
                    trRequirement.Visible = false;
                }
                else
                {
                    trRequirement.Visible = true;
                    DivReqAdd.Visible = false;
                    DivReqView.Visible = true;
                    LblRequirementVal.Text = objDeli.Requirement;
                    SpnReq.Visible = false;
                    HdnReqId.Value = objDeli.RequirementId.ToString();
                }
                if (trRequirement.Visible)
                {
                    trClauseDetails.Visible = true;
                    LblClauseVal.Text = GetClauseNameNo(objDeli.RequirementId.ToString());

                }
                else { trClauseDetails.Visible = false; }
            }


                int count = DdlDeliFrequency.Items.Count;
                if (count == 0)
                {
                    DdlDeliFrequency.DataSourceID = "SDSFrequency";
                    DdlDeliFrequency.DataTextField = "LOOKUP_DESC";
                    DdlDeliFrequency.DataValueField = "LOOKUP_ID";
                    DdlDeliFrequency.DataBind();

                }
                if (!string.IsNullOrEmpty(objDeli.FrequencyId.ToString()))
                {
                    if (DdlDeliFrequency.Items.FindByValue(objDeli.FrequencyId.ToString()) != null)
                    {
                        DdlDeliFrequency.ClearSelection();
                        DdlDeliFrequency.Items.FindByValue(objDeli.FrequencyId.ToString()).Selected = true;
                    }
                    else
                    {
                        DdlDeliFrequency.SelectedIndex = 0;
                    }
                }
            
            TxtDescription.Text = objDeli.Description;
            SetOrg(objDeli.DrtId.ToString(), objDeli.DeptId.ToString());
           
            ChkUploadReq.Checked = (objDeli.UploadFileRequired == "Y") ? true : false;
            ChkNotifyMgr.Checked = (objDeli.NotifyManager == "Y") ? true : false;
            ChkInfoOnly.Checked = (objDeli.IsInformationOnly == "Y") ? true : false;
            //TODO: status change for owners


            SubOwners("edit",subOwnersList);
            
            LstApprovers.ClearSelection();
           

            foreach (Business.DeliverableApprovers objApprovers in approversList)
            {

                for (int i = 0; i < LstApprovers.Items.Count; i++)
                {
                    if (LstApprovers.Items.FindByValue(objApprovers.ApproverId.ToString()) != null)
                    {
                        LstApprovers.Items.FindByValue(objApprovers.ApproverId.ToString()).Selected = true;
                    }
                    else { LstApprovers.SelectedIndex = 0; }

                }

            }
            if (approversList.Count == 0) { LstApprovers.SelectedIndex = 0; }

            LstNotify.ClearSelection();
            if (notificationList.Count == 0) { ChkDeselect.Checked = false; ChkDeselect.Text = "Select All"; } else { ChkDeselect.Checked = false; ChkDeselect.Text = "Deselect All"; }
            foreach (Business.DeliverableNotification objNotification in notificationList)
            {
                for (int i = 0; i < LstNotify.Items.Count; i++)
                {
                    if (LstNotify.Items.FindByValue(objNotification.LookupId.ToString()) != null)
                    {
                        LstNotify.Items.FindByValue(objNotification.LookupId.ToString()).Selected = true;
                    }
                }
            }
            

            if (divFile.Visible)
            {
                BindFileGrid();
            }
            BtnUpload.Visible = false;


        }

        private void FillRequirement()
        {
            ListItem li = new ListItem("--Choose one--", "0");
            using (OracleDataReader _drReq = objDml.GetRequirement(DdlType.SelectedValue.ToString()))
            {
                if (_drReq.HasRows)
                {
                    DdlRequirement.Items.Clear();
                  
                    DdlRequirement.DataValueField = "REQUIREMENT_ID";
                    DdlRequirement.DataTextField = "REQUIREMENT";
                    DdlRequirement.DataSource = _drReq;
                    DdlRequirement.DataBind();
                    DdlRequirement.Items.Insert(0, li);
                }
                else
                {
                    string _msg = "There are no Requirements available for the selected Type. Please make sure you add a requirement to the selected type or select another type";
                    objCommon.CreateMessageAlertSM(this, _msg, "info", false);
                    DdlRequirement.Items.Clear();
                    DdlRequirement.Items.Insert(0, li);
                    SetFocus(DdlType.ClientID);
                }
            }
        }

        private void ClearAllFields()
        {
        }

        private void FillDeliverableObject(string mode)
        {
            Business.DeliverableRecord objDeli = new Business.DeliverableRecord();

            if ((mode != "add") && (mode != "reviewadd"))
            {
                objDeli.DeliverableId = Convert.ToInt32(LblIdVal.Text);
                objDeli.CompositeKey = LblTrackIdVal.Text;
                objDeli.Status = LblStatusVal.Text;
            }

            objDeli.DrtId = (DdlDirectorate.SelectedIndex != 0) ? DdlDirectorate.SelectedValue : "0";
            objDeli.DrtName = (DdlDirectorate.SelectedIndex != 0) ? DdlDirectorate.SelectedItem.Text : "";
            objDeli.DeptId = (DdlDepartment.SelectedIndex != 0) ? DdlDepartment.SelectedValue : "0";
            objDeli.DeptName = (DdlDepartment.SelectedIndex != 0) ? DdlDepartment.SelectedItem.Text : "";

            objDeli.IsInformationOnly = (ChkInfoOnly.Checked) ? "Y" : "N";
            objDeli.UploadFileRequired = (ChkUploadReq.Checked) ? "Y" : "N";
            objDeli.NotifyManager = (ChkNotifyMgr.Checked) ? "Y" : "N";
            objDeli.DueDate = (TxtDueDate.Text != "") ? Convert.ToDateTime(TxtDueDate.Text) : DateTime.MinValue;
            objDeli.Description = Business.WordCharExtension.ReplaceWordChars(TxtDescription.Text);
            objDeli.FrequencyId = (DdlDeliFrequency.SelectedIndex != 0) ? Convert.ToInt32(DdlDeliFrequency.SelectedValue) : 0;
            objDeli.Frequency = (DdlDeliFrequency.SelectedIndex != 0) ? DdlDeliFrequency.SelectedItem.Text : "";
  
            if ((mode == "add") || (mode == "reviewadd"))
            {
                objDeli.CreatedBy = objCommon.GetUserID();
                if (ViewState["reqid"] != null && ViewState["reqid"].ToString() != "")
                {
                    objDeli.TypeId = objDml.GetTypeID(LblTypeVal.Text);
                    objDeli.TypeName = LblTypeVal.Text;
                    objDeli.RequirementId = Convert.ToInt32(ViewState["reqid"]);
                     objDeli.Requirement = LblRequirementVal.Text;
                }
                else
                {
                    objDeli.TypeId = (DdlType.SelectedIndex != 0) ? Convert.ToInt32(DdlType.SelectedValue) : 0;
                    objDeli.TypeName = (DdlType.SelectedIndex != 0) ? DdlType.SelectedItem.Text : "";
                    objDeli.RequirementId = trRequirement.Visible?((DdlRequirement.SelectedIndex != 0) ? Convert.ToInt32(DdlRequirement.SelectedValue) : 0):0;
                    objDeli.Requirement = trRequirement.Visible ?((DdlRequirement.SelectedIndex != 0) ? (DdlRequirement.SelectedItem.Text) : ""):"";
                }

                if (HdnDeliverableId.Value != "") { divFile.Visible = false; }
               
           
            }
            if ((mode == "edit") || (mode == "reviewedit"))
            {

                objDeli.ModifiedBy = objCommon.GetUserID();
                objDeli.TypeId = Convert.ToInt32(HdnTypeId.Value);
                objDeli.TypeName = LblTypeVal.Text;
                objDeli.RequirementId = trRequirement.Visible ? ((HdnReqId.Value!="")?Convert.ToInt32(HdnReqId.Value):0):0;
                objDeli.Requirement = trRequirement.Visible ?  LblRequirementVal.Text:"";           
            }
            //Sub Owners list
            List<Business.SubOwners> subOwnersList = new List<Business.SubOwners>();

            AddSubOwners(subOwnersList);
            //Owner - add it to subowner collection
            if (TxtOwner.Text != "")
            {
                Business.SubOwners objOwner = new Business.SubOwners();
                objOwner.IsOwner = "Y";
                objOwner.SlacId = objCommon.GetEmpid(TxtOwner.Text);
                objOwner.Name = TxtOwner.Text;
                objOwner.CreatedBy = objCommon.GetUserID();
                subOwnersList.Add(objOwner);
                ViewState["newowner"] = objOwner.SlacId; 
            }

            //Approvers
            List<Business.DeliverableApprovers> approverList = new List<Business.DeliverableApprovers>();

            foreach (ListItem li in LstApprovers.Items)
            {
                if (li.Selected == true)
                {
                    if (li.Value != "0")
                    {
                        Business.DeliverableApprovers objApprover = new Business.DeliverableApprovers();
                        objApprover.ApproverName = li.Text;
                        objApprover.ApproverId = Convert.ToInt32(li.Value);
                        objApprover.CreatedBy = objCommon.GetUserID();
                        approverList.Add(objApprover);
                        objApprover = null;
                    }
                }
            }

            //Notification Schedule
            List<Business.DeliverableNotification> notificationList = new List<Business.DeliverableNotification>();

            foreach (ListItem li in LstNotify.Items)
            {

                if (li.Selected == true)
                {

                    if (li.Value != "0")
                    {
                        Business.DeliverableNotification objNotification = new Business.DeliverableNotification();
                        objNotification.LookupDesc = li.Text;
                        objNotification.LookupId = Convert.ToInt32(li.Value);
                        objNotification.CreatedBy = objCommon.GetUserID();
                        notificationList.Add(objNotification);
                        objNotification = null;

                    }

                }
            }


            if ((mode == "reviewadd") || (mode == "reviewedit"))
            {
                FillFormView(objDeli, subOwnersList, notificationList, approverList, mode);
            }
            else if (mode == "add")
            {
                SaveDeliverable(objDeli, subOwnersList, notificationList, approverList, mode);
            }
            else if (mode == "edit")
            {
                UpdateDeliverable(objDeli, subOwnersList, notificationList, approverList);
            }


        }

        private void AddSubOwners(List<Business.SubOwners> subOwnersList)
        {

            int _count = TB1.TBControlsCount;
            TextBox TxtSubOwner = null;

            for (int i = 0; i <= _count; i++)
            {

                Panel pnlsubowner = new Panel();
                ContentPlaceHolder cphContent = (ContentPlaceHolder)Page.Master.FindControl("MainContent");
                User_Controls.DynamicTextBox ucdb = (User_Controls.DynamicTextBox)cphContent.FindControl("TB1");
                pnlsubowner = (Panel)ucdb.FindControl("pnlsubowner");
                ucdb = (User_Controls.DynamicTextBox)cphContent.FindControl("TB1");

                TxtSubOwner = (TextBox)pnlsubowner.FindControl("txt" + i.ToString());


                if (TxtSubOwner.Text != "")
                {
                    Business.SubOwners objSubowner = new Business.SubOwners();
                    objSubowner.IsOwner = "N";
                    objSubowner.SlacId = objCommon.GetEmpid(TxtSubOwner.Text);
                    objSubowner.Name = TxtSubOwner.Text;
                    objSubowner.CreatedBy = objCommon.GetUserID();
                    subOwnersList.Add(objSubowner);
                    objSubowner = null;
                }

            }
        }

        # endregion

        # region "Dropdowns/listbox"

        private void FillDropdownLists()
        {
            OracleDataReader _multiSetdr = null;

            DataSet _multiSetds = new DataSet();
            ListItem li = new ListItem("--Choose one--", "0");

            try
            {
                objData.SPName = "CMS_LOOKUP_PKG.GetDeliverableLookupValues";
                _multiSetdr = objData.GetMultiresultDeliverable();
                while (_multiSetdr.Read())
                {

                    DdlType.DataSource = _multiSetdr;
                    DdlType.DataValueField = "CONTRACT_ID";
                    DdlType.DataTextField = "CONTRACT_NAME";
                    DdlType.DataBind();

                    DdlType.Items.Insert(0, li);
                    _multiSetdr.NextResult();

                    DdlDirectorate.DataSource = _multiSetdr;
                    DdlDirectorate.DataValueField = "ORG_ID";
                    DdlDirectorate.DataTextField = "DESCRIPTION";
                    DdlDirectorate.DataBind();
                    AddRemoveItemsInList("DdlDirectorate", "0", "--Choose One--");
                    _multiSetdr.NextResult();

                    LstNotify.DataSource = _multiSetdr;
                    LstNotify.DataTextField = "LOOKUP_DESC";
                    LstNotify.DataValueField = "LOOKUP_ID";
                    LstNotify.DataBind();
                 
                  
                   _multiSetdr.NextResult();

                    LstApprovers.DataSource = _multiSetdr;
                    LstApprovers.DataTextField = "EMPLOYEE_NAME";
                    LstApprovers.DataValueField = "MANAGER_ID";

                    
                    LstApprovers.DataBind();
                    LstApprovers.Items.Insert(0, new ListItem("--Select--","0"));
                    LstApprovers.SelectedValue = "0";



                }
            }
            finally
            {
                _multiSetdr.Close();
                objData.DisconnectDB();
            }
        }

        private void FillNotificationSchedule(string objId, string mode = "new")
        {
            LstNotify.ClearSelection();
            List<Business.NotificationSchedule> _notifySchedule = objDml.GetNotifySchedule(objId, mode);
            foreach (Business.NotificationSchedule objNotifySchedule in _notifySchedule)
            {
                for (int i = 0; i < LstNotify.Items.Count; i++)
                {
                    LstNotify.Items.FindByValue(objNotifySchedule.NotifyScheduleId.ToString()).Selected = true;
                }

            }

        }

        protected void DdlRequirement_SelectedIndexChanged(object sender, EventArgs e)
        {

            SetOwnerDrtDept(DdlRequirement.SelectedValue.ToString());
           
        }

        protected void DdlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DdlType.SelectedValue != "0")
            {
                int _typeId = Convert.ToInt32(DdlType.SelectedValue);
                int _groupId = objDml.GetGroupID(_typeId);
                if (_groupId == 2)
                {
                    SpnDesc.Visible = false;
                    trRequirement.Visible = true;
                    DivReqAdd.Visible = true;
                    DivReqView.Visible = false;
                    FillRequirement();
                    LblFrequencyVal.Text = "";
                }
                else
                {
                    SpnDesc.Visible = true;
                    trRequirement.Visible = false;
                    SetOwnerDrtDept("");
                }
            }
            else
            {
                CvType.IsValid = false;

            }
            //if ((DdlType.SelectedValue == ((int)Type.PrimaryContract).ToString()) || (DdlType.SelectedValue == ((int)Type.DOEDirective).ToString()))
            //{
            //    SpnDesc.Visible = false;
            //    trRequirement.Visible = true;
            //    DivReqAdd.Visible = true;
            //    DivReqView.Visible = false;
            //    FillRequirement();
            //}
            //else
            //{
            //    SpnDesc.Visible = true;     //Comment MS: If Type is Data call or DOE request, the requirement should be entered in Description Field
            //    if (DdlType.SelectedValue != "0")
            //    {
            //        trRequirement.Visible = false;
            //        SetOwnerDrtDept("");
                   
            //    }
               
            //}
        }

        protected void DdlDirectorate_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillDepartment();
            if (DdlDirectorate.SelectedValue == "0")
            {
               CvDirectorate.IsValid = false;      //Comment MS: This is to trigger the validation on selected change itself
            }
        }

         # endregion

        # region "control events"
       
        protected void BtnReview_Click(object sender, EventArgs e)
        {
            bool _pagevalidsvr = false;
            if (Page.IsValid)                       //Comments MS - Verifies if all the clientside validation is valid
            {
                _pagevalidsvr = ValidateOnServerside();     //Comments MS - Verifies if all the ctrls are validated on server along with those sneaked in after clientside validation
                if (_pagevalidsvr)
                {
                    SetPage("reviewadd");
                    FillDeliverableObject("reviewadd");
                }
            }
            //assign the values to readonly controls
        }

        protected void BtnReview2_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SetPage("reviewedit");
                FillDeliverableObject("reviewedit");
            }
        }

        protected void BtnChange_Click(object sender, EventArgs e)
        {
            
            SetPage("addset");
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
          
           SetPage("view");
            BtnEdit.Visible = true;
        }

        protected void BtnMoreChange_Click(object sender, EventArgs e)
        {           
            SetPage("editset");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "DisableSubmit('add');", true);          
            FillDeliverableObject("add");
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "DisableSubmit('edit');", true);
            FillDeliverableObject("edit");
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            DivEdit.Visible = false;
            Response.Redirect("Deliverable.aspx?mode=edit&id=" + HdnDeliverableId.Value);

        }

        protected void TxtOwner_TextChanged(object sender, EventArgs e)
        {
            bool _isValid;
            if (TxtOwner.Text != "")
            {
                _isValid = objDml.CheckValidName(TxtOwner.Text);
                if (_isValid)
                {
                    SetOrgBasedOnUser(TxtOwner.Text.ToString());
                }
                else
                {
                    CvOwn.IsValid = false;
                   // DdlDirectorate.SelectedIndex = 0;

                }
            }
            else
            {
                RVOwn.IsValid = false;
            }

        }

        protected void LnkEdit_Click(object sender, EventArgs e)
        {
            List<Business.SubOwners> subOwnersList = new List<Business.SubOwners>();
            SetPage("view");
            SubOwners("edit", subOwnersList, true);
            LnkUpdate.Visible = true;
            LnkCancel.Visible = true;            
        }

        protected void LnkUpdate_Click(object sender, EventArgs e)
        {
            string _compositeKey = "";
            string _ownerId = "";
           
            GetDelInfo(Convert.ToInt32(HdnDeliverableId.Value));
            if (ViewState["trackid"] != null)
            {
                _compositeKey = ViewState["trackid"].ToString();
            }
            
            if (LblOwnerVal.Text  != "")
            {
                _ownerId = objCommon.GetEmpid(LblOwnerVal.Text).ToString();
            }
            objDml.DeleteSubowners(Convert.ToInt32(HdnDeliverableId.Value), "N", objCommon.GetUserID());
            List<Business.SubOwners> subOwnersList = new List<Business.SubOwners>();
            AddSubOwners(subOwnersList);
            objDml.AddSubOwnerCol(subOwnersList, HdnDeliverableId.Value,"N",_compositeKey,_ownerId, SendEmail());
            SetPage("view");
        }

        protected void LnkCancel_Click(object sender, EventArgs e)
        {
            SetPage("view");
        }

        protected void BtnSubmitDeli_Click(object sender, EventArgs e)
        {
            string _result = "-1";
            int _status;
            string _uploadfilereq;
            int _filesattached = 0;
            StringBuilder _sbmsg = new StringBuilder();

            _uploadfilereq = ViewState["uploadfile"].ToString();
            _filesattached = Convert.ToInt32(ViewState["files"]);
            //TODO: Add a check to verify if it is owner before proceeding with status change

            if ((_uploadfilereq == "N") || ((_uploadfilereq == "Y") && (_filesattached > 0)))
            {

                _status = (ViewState["info"].ToString() == "Y") ? (int)Status.ApprovedbyDefault : (int)Status.submitted;

                _result = objDml.UpdateStatus(Convert.ToInt32(HdnDeliverableId.Value), _status, "", objCommon.GetUserID(), SendEmail());
                if (_result == "0")
                {
                    objCommon.CreateMessageAlertSM(this, "Deliverable submitted", "info", "Deliverable.aspx?mode=view&id=" + Server.HtmlEncode(HdnDeliverableId.Value));
                }
                else
                {
                    objCommon.CreateMessageAlertSM(this, "Error in submitting the deliverable!", "err", false);

                }
            }
            else
            {
                _sbmsg.Append(" Error! This deliverable cannot be submitted. \\n");
                _sbmsg.Append(" Reason: " + " Requires at least one file to fulfil the requirement. \\n");
                _sbmsg.Append(" Action: " + " Attach at least one document and submit again. ");
                objCommon.CreateMessageAlertSM(this, _sbmsg.ToString(), "err", false);
            }

        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            DivDelete.Visible = false;
            string _errCode;

            _errCode = objDml.DeleteDeliverable(Convert.ToInt32(HdnDeliverableId.Value), objCommon.GetUserID());
            if (_errCode != "err")
            { objCommon.CreateMessageAlertSM(this, "Deliverable marked as deleted", "msg", true); }
            else
            {
                objCommon.CreateMessageAlertSM(this, "Error! Deliverable could not be deleted. //n Please try later", "error", false);
                SetPage("view");
            }

        }


        protected void ChkDeselect_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkDeselect.Text == "Deselect All")
            {
                if (ChkDeselect.Checked)
                {
                    foreach (ListItem li in LstNotify.Items)
                    {

                        if (li.Selected == true)
                        {
                            li.Selected = false;
                        }
                    }
                    ChkDeselect.Checked = false;
                    ChkDeselect.Text = "Select All";
                }
            }
            else
            {
                foreach (ListItem li in LstNotify.Items)
                {

                    if (li.Selected == false)
                    {
                        li.Selected = true;
                    }
                }
                ChkDeselect.Text = "Deselect All";
                ChkDeselect.Checked = false;
            }
        }

        protected void LblDescriptionVal_PreRender(object sender, EventArgs e)
        {
            LblDescriptionVal.Text = objCommon.WrapNeat(LblDescriptionVal.Text);
        }

       
        #endregion

        #region "database Manipulations"

        private void SaveDeliverable(Business.DeliverableRecord objDeli, List<Business.SubOwners> subownersList, List<Business.DeliverableNotification> notificationList, List<Business.DeliverableApprovers> approversList, string mode)
        {
            string _compositeKey = "";
            string _ownerId = "";
            string _deliverableId = objDml.CreateDeliverable(objDeli.RequirementId, objDeli.TypeId, objDeli.DueDate, objDeli.DrtId, objDeli.DeptId, objDeli.UploadFileRequired, objDeli.Description, objDeli.IsInformationOnly, objDeli.NotifyManager, objDeli.CreatedBy, objDeli.FrequencyId);
            HdnDeliverableId.Value = _deliverableId;
           

            if (_deliverableId != "0")
            {
                GetDelInfo(Convert.ToInt32(_deliverableId));
                if (ViewState["trackid"] != null)
                {
                    _compositeKey = ViewState["trackid"].ToString();
                }
             
                if (TxtOwner.Text != "")
                {
                    _ownerId = objCommon.GetEmpid(TxtOwner.Text).ToString();
                }
                objDml.AddSubOwnerCol(subownersList, _deliverableId,"Y",_compositeKey,_ownerId, SendEmail());
                objDml.AddNotificationCol(notificationList, _deliverableId);
                objDml.AddApproverCol(approversList, _deliverableId);
                if (SendEmail() == "Y")
                {
                objDml.SendEmail(Convert.ToInt32(_deliverableId), 1, "");
                }
            }
            objCommon.CreateMessageAlertSM(this, "Deliverable Added", "info", "Deliverable.aspx?mode=view&id=" + Server.HtmlEncode(HdnDeliverableId.Value));

        }

        void GetDelInfo(int deliId)
        {
            string _compositeKey = "";
            using (OracleDataReader _drDeliverable = objDml.GetDeliInfo(Convert.ToInt32(deliId)))
            {
                if (_drDeliverable.HasRows)
                {
                    while (_drDeliverable.Read())
                    {
                        _compositeKey = objCommon.FixDBNull(_drDeliverable["COMPOSITE_KEY"]);
                     }
                }
            }
            ViewState["trackid"] = _compositeKey;
        }

        private void UpdateDeliverable(Business.DeliverableRecord objDeli, List<Business.SubOwners> subownersList, List<Business.DeliverableNotification> notificationList, List<Business.DeliverableApprovers> approversList)
        {
            int _oldOwner, _newOwner;
            string _sendEmail = "N";
            string _compositeKey = "";
            string _ownerId = "";
            
            //COMMENTS MS - Per DEV-3135 - Email should not be sent for deliverable modification if owner is changed
            _oldOwner = _newOwner = 0;
            if (ViewState["prevowner"] != null)
            {
                _oldOwner = (int)ViewState["prevowner"];
            }

            if (ViewState["newowner"] != null)
            {
                _newOwner = (int)ViewState["newowner"];
            }

            if ((_oldOwner != 0) && (_newOwner != 0) &&
                    (_oldOwner == _newOwner))
            {
               _sendEmail = SendEmail();  
            }

            string _result = objDml.UpdateDeliverable(objDeli.DeliverableId, objDeli.DueDate, objDeli.DrtId, objDeli.DeptId, objDeli.UploadFileRequired, objDeli.Description, objDeli.IsInformationOnly, objDeli.NotifyManager, objDeli.ModifiedBy, _sendEmail, objDeli.FrequencyId);
            if (_result == "0")
            {
                GetDelInfo(objDeli.DeliverableId);
                if (ViewState["trackid"] != null)
                {
                    _compositeKey = ViewState["trackid"].ToString();
                }
               
                if (ViewState["newowner"] != null)
                {
                    _ownerId = ViewState["newowner"].ToString();
                }
                //Delete subowner
                //Addsubowner
                objDml.DeleteSubowners(objDeli.DeliverableId, "", objCommon.GetUserID());
                //Get all subowners currently active
                //Check if the subowner is added in the deleted list. if not send email
                objDml.AddSubOwnerCol(subownersList, objDeli.DeliverableId.ToString(),"N",_compositeKey,_ownerId, SendEmail());

                //deletenotification

                //Addnotification
                objDml.DeleteNotification(objDeli.DeliverableId, objCommon.GetUserID());
                objDml.AddNotificationCol(notificationList, objDeli.DeliverableId.ToString());

                //Deleteapprover
                //Addapprover
                objDml.DeleteApprover(objDeli.DeliverableId, objCommon.GetUserID());
                objDml.AddApproverCol(approversList, objDeli.DeliverableId.ToString());
                
                objCommon.CreateMessageAlertSM(this, "Deliverable Updated", "info", "Deliverable.aspx?mode=view&id=" + Server.HtmlEncode(HdnDeliverableId.Value));
            }
            else
            {
                objCommon.CreateMessageAlertSM(this, "Error! Deliverable not Updated", "info", false);
            }
            
        }
        #endregion

        #region "File related"

        private void BindFileGrid()
        {
            DataSet _dsFile = new DataSet();
            _dsFile = objDml.GetFileInfo(HdnDeliverableId.Value);
            if (_dsFile.Tables["file"].Rows.Count > 0)
            {
                gvFile.DataSource = _dsFile.Tables["file"];
                gvFile.DataBind();
                bool _allowDelete = CheckPermissionToDelete();
                if (_allowDelete == false)
                {
                    gvFile.HeaderRow.Cells[4].Visible = false;
                    foreach (GridViewRow gvr in gvFile.Rows)
                    {
                        gvr.Cells[4].Visible = false;
                    }
                }
            }
            else
            {
                gvFile.DataSource = null;
                gvFile.DataBind();
            }
            ViewState["files"] = _dsFile.Tables["file"].Rows.Count;

        }

        private bool CheckPermissionToDelete()
        {
            bool _allowDelete = false;
            double diffDaysApp = 0;

            //DEV-4334 - Prevent cma or owners from deleting the items after 10 days from the date it is approved by default
            if ((ViewState["dateapproved"] != null) && (ViewState["dateapproved"].ToString() != ""))
            {
                diffDaysApp = (Convert.ToDateTime(ViewState["dateapproved"]) - DateTime.Now).TotalDays;
            }
            else { diffDaysApp = -1; }
            string _currentuser = objCommon.GetUserID();

            CheckIfManager();
            if ((_cma) || (_admin))
            {
                if (((diffDaysApp > 10) || (diffDaysApp == -1) ) && ((int)ViewState["status"] == (int)Status.ApprovedbyDefault))
                {
                    _allowDelete = false;
                }
                else _allowDelete = true;
            }
            else if (((int)ViewState["status"] == (int)Status.New) || ((int)ViewState["status"] == (int)Status.InProgress) || ((int)ViewState["status"] == (int)Status.Reopened))
            {
                if ((bool)ViewState["owner"])
                {
                    _allowDelete =  true;
                }
                else if ((bool)ViewState["subowner"])
                {
                    _allowDelete = true;
                }
                else
                {
                    _allowDelete = false;
                }

            }
            else if ((((diffDaysApp > 10) || (diffDaysApp == -1)) && ((int)ViewState["status"] == (int)Status.ApprovedbyDefault)) && (bool)ViewState["owner"])
            { _allowDelete = false; }
            else
            {
                _allowDelete = false;
            }

            return _allowDelete;


        }

        protected void gvFile_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //COMMENT MS: Per KP, deleting a file follows the rule below
            // 1. CMA can delete at any time  - taken care at the time of databind
            // 2. Owner can delete a file until it is submitted - taken care at the time of databind
            // 3. subowner can delete a file only if it is uploaded by that person
            
            
            string _currentuser= objCommon.GetUserID();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //COMMENT MS: ! - Below two lines are for registering the Link button on Page postback for Update Panel
                //This is workaround to avoid Sys.WebForms.PageRequestManagerParserErrorException: The message received from the server could not be parsed
                LinkButton LnkFile = (LinkButton)e.Row.FindControl("LnkDownload");
                ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(LnkFile);

                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string _uploadedBy;

                _uploadedBy = rowView["UPLOADED_BY"].ToString();

                if (_uploadedBy != "")
                {

                    e.Row.Cells[2].Text = objCommon.GetEmpname(_uploadedBy);
                }

                if ((bool)ViewState["subowner"])
                {
                    if (_currentuser == _uploadedBy)
                    {

                        ToggleEnableDeleteButton(true, e);
                    }
                    else
                    {
                        ToggleEnableDeleteButton(false,e);
                    }
                }
                
            }


        }

        private void ToggleEnableDeleteButton(bool boolval, GridViewRowEventArgs e)
        {
            ImageButton _imgBtn;
            Label _lbldel;

            _imgBtn = (ImageButton)e.Row.FindControl("ImgBtnDelete");
            _lbldel = (Label)e.Row.FindControl("Lbldelinfo");

            _imgBtn.Enabled = boolval;
            if (!boolval)
            {
                _imgBtn.Visible = false;
                _lbldel.Visible = true;
            }
            else {
                _imgBtn.Visible = true; _lbldel.Visible = false; }
        }

        protected void gvFile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int _attachmentId = Convert.ToInt32(e.CommandArgument.ToString());
            string _filename = "";
            if (e.CommandName == "download")
            {              
                FileData(_attachmentId);
                
            }

            if (e.CommandName == "delete")
            {
                string _result = "";
                _result = objDml.DeleteAttachment(_attachmentId, objCommon.GetUserID());
                BindFileGrid();
                SetPage("view");
            }
        }

        protected void gvFile_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           
        }
        #endregion

        protected void BtnClone_Click(object sender, EventArgs e)
        {
            Response.Redirect("Deliverable.aspx?mode=add&id=" + HdnDeliverableId.Value);
        }

        protected void BtnCancel1_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "CloseRejectDialog();", true);
        }

     
        protected void BtnReject1_Click(object sender, EventArgs e)
        {
            int _status = (int)Status.Reopened;
            string _result = "";
            string _redirectpage="Default.aspx";
            if (HdnDeliverableId.Value != null)
            {
                if (HdnDeliverableId.Value.ToString() != "")
                {
                    _result = objDml.UpdateStatus(Convert.ToInt32(HdnDeliverableId.Value), _status, Business.WordCharExtension.ReplaceWordChars(TxtReason.Text), objCommon.GetUserID(), SendEmail());

                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "CloseRejectDialog();", true);
            
            if (_result == "0")
            {
                objCommon.CreateMessageAlertSM(this, "Deliverable reopened and Owner sent your rejection notice", "info", GetRedirectPage());

            }
            else
            {
                objCommon.CreateMessageAlertSM(this, "Error in re-opening the deliverable!", "err", false);

            }
        }

        protected string GetRedirectPage()
        {
            string _redirectPage = "";
            //Per Requirement DEV-3164 - Check if there are any items for the current SSO's approval, if not go to dashboard
            if (objDml.CheckIfForSSOAppr(objCommon.GetUserID ()))
            {
                    _redirectPage = "Deliverable_List.aspx?stat=3&ssomy=Y";
            }
            else _redirectPage = "Default.aspx";
            return _redirectPage;

        }
        protected string SendEmail()
        {
            string _sendEmail;
            if ((HdnEmail.Value == "yes") || (HdnEmail.Value == ""))
            {
                _sendEmail = "Y";
            }
            else { _sendEmail = "N"; }
            return _sendEmail;

        }
        protected void BtnApprove_Click(object sender, EventArgs e)
        {
            int _status = (int)Status.Approved;

            string _result = objDml.UpdateStatus(Convert.ToInt32(HdnDeliverableId.Value), _status, "", objCommon.GetUserID(), SendEmail());
            string _redirectpage = GetRedirectPage();
            if (_result == "0")
            {
                objCommon.CreateMessageAlertSM(this, "Deliverable Approved", "info", _redirectpage);
                          }
            else
            {
                objCommon.CreateMessageAlertSM(this, "Error in approving the deliverable!", "err", false);

            }
        }

        protected void LnkBack_Click(object sender, EventArgs e)
        {
            
          
        }

        protected void BtnUpload_Click(object sender, EventArgs e)
        {

        }

        protected void BtnNotifyOwners_Click(object sender, EventArgs e)
        {
            BtnNotifyOwners.Enabled = false;
            int _filesattached = 0;
            StringBuilder _sbmsg = new StringBuilder();

            _filesattached = Convert.ToInt32(ViewState["files"]);

            if (_filesattached > 0)
            {
                //update the subowner action
                //send email
                string bResult = "";
                bResult = objDml.SendEmail(Convert.ToInt32(HdnDeliverableId.Value), 13, "");
                InsertSubownerFileAction(true, bResult, Convert.ToInt32(HdnDeliverableId.Value));
                objCommon.CreateMessageAlertSM(this, "You have successfully made your document(s) available to the Owner. The Owner has been notified and has been prompted to review  and submit this Deliverable.", "info", false);
            }
            else
            {
                //give error that there should be atleast one file uploaded 
                _sbmsg.Append(" Error! Owner cannot be notified. \\n");
                _sbmsg.Append(" Reason: " + " Requires at least one file to fulfil the requirement. \\n");
                _sbmsg.Append(" Action: " + " Attach at least one document and click 'Notify Owner...'  again. ");
                objCommon.CreateMessageAlertSM(this, _sbmsg.ToString(), "err", false);
                BtnNotifyOwners.Enabled = true;

            }
        }

     
        protected void DdlDeliFrequency_DataBound(object sender, EventArgs e)
        {
            DdlDeliFrequency.Items.Insert(0, new ListItem("--Choose One--", "0"));
        }

       
      
     
    }
}
