﻿//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  Requirement.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is the page with all manipulations related to the requirement object
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;

namespace ContractManagement.Admin
{
    public partial class Requirement : BasePage
    {
        # region "Object instance"
        Data.CMS_DMLUtil objDml = new Data.CMS_DMLUtil();
        Business.CMS_Common_Util objCommon = new Business.CMS_Common_Util();
        #endregion

        #region "Intial Page setup"
        protected void Page_Load(object sender, EventArgs e)
        {
            string _mode;
            string _clid;
            string _id;
            string[] _modeArray = { " ", "list", "new", "edit", "view", "reviewadd", "reviewedit", "changeadd", "changeedit" };
            TxtReqName.Attributes.Add("onkeydown", "return onKeypress('cmdFind');");
            if (!Page.IsPostBack)
            {
                CheckIfManager();
                if ((!_admin) && (!_cma)) Response.Redirect("~/Permission.aspx?msg=gen");


                _mode = Request.QueryString["mode"];

                //Comments MS: Security - Mode should have one of the items in string array
                int pos = Array.IndexOf(_modeArray, _mode);


                if (pos > -1)
                {
                    if (!string.IsNullOrEmpty(_mode))
                    {
                        if (Regex.IsMatch(_mode, @"^[a-z]+$"))
                        {
                            HdnMode.Value = _mode;
                        }
                        else { RedirectInvalidParam(); }
                    }


                    _clid = Request.QueryString["clid"];
                    if (!string.IsNullOrEmpty(_clid))
                    {
                        if (Regex.IsMatch(_clid, "^[0-9]+$"))
                        {
                            HdnClauseid.Value = _clid;
                            ViewState["qsclid"] = _clid;
                        }
                        else { RedirectInvalidParam(); }
                    }


                    _id = Request.QueryString["id"];
                    if (!string.IsNullOrEmpty(_id))
                    {
                        if (Regex.IsMatch(_id, "^[0-9]+$"))
                        {
                            HdnReqid.Value = _id;
                        }
                        else { RedirectInvalidParam(); }
                    }


                    if (_mode == "")
                    {
                        _mode = "list";
                    }
                    SetPage(_mode);
                }
                else {
                    //COMMENTS MS: For Security, if Requirement doesn't match the regex, redirect to custom msg
                    RedirectInvalidParam();
                }
            }
        }

        private void ToggleClauseName(bool boolval)
        {
            DivClauseView.Visible = boolval;
            DivClause.Visible = !boolval;
            SpnClause.Visible = !boolval;
            trOwner.Visible = boolval;            
        }

        private void SetPage(string mode)
        {
           
            if (mode == "list")
            {
                LblSubTitle.Text = "Requirement";
                DivRequirementList.Visible = true;
                divRequirementNew.Visible = false;
                HdnDesc.Value = TxtReqName.Text;
                BindGrid();
                cmdAdd.Visible = true; 
            }
            else
            {
                DivRequirementList.Visible = false;
                divRequirementNew.Visible = true;

                if ((mode == "new") || (mode == "changeadd"))
                {
                    DivReview.Visible = true;
                    DivReviewedit.Visible = false;
                    DivSubmit.Visible = false;
                    DivUpdate.Visible = false;
                    ToggleVisibility(true);
                    ToggleSpecial(false);
                    LblSubTitle.Text = "New Requirement";
                    //Check if it came from Clause list or Clicking New requirement button
                    if (ViewState["qsclid"] != null)
                    {
                        if (ViewState["qsclid"].ToString() != "")
                        {
                            ToggleClauseName(true);
                            if (mode == "new")
                            {
                                Business.ClauseRecord objClause =  Clause(HdnClauseid.Value);
                                LblClauseVal.Text = objClause.ClauseName;
                                trClauseno.Visible = false;
                                                               
                                if (objClause.ClauseType.Equals("Subclause"))
                                {
                                    trSubclause.Visible = true;
                                    LblSubclauseVal.Text = ReturnSubclause(objClause.ClauseNumber, objClause.ClauseName);
                                    LblClauseVal.Text = objClause.ParentClauseNum + ", " + objClause.ParentClause;
                                    LblOwnerVal.Text = objCommon.GetEmpname(objClause.ParentOwner.ToString());
                                }
                                else
                                {
                                    trSubclause.Visible = false;
                                    LblClauseVal.Text = objClause.ClauseNumber + ", " + objClause.ClauseName;
                                    LblOwnerVal.Text = objClause.OwnerName;
                                }
                            }
                           
                        }
                        else
                        {
                            ToggleClauseName(false);
                            if (mode == "changeadd") DdlClause_SelectedIndexChanged(null, null);
                        }
                        
                    }
                    else 
                    { 
                        ToggleClauseName(false);
                        if (mode == "changeadd") DdlClause_SelectedIndexChanged(null, null);                  
                    }
                    ChkSCFlownProv.Enabled = true;
                }              
                else if (mode == "changeedit")
                {
                    DivReview.Visible = false;
                    DivReviewedit.Visible = true;
                    DivSubmit.Visible = false;
                    DivUpdate.Visible = false;
                    ToggleVisibility(true);
                    ToggleSpecial(true);
                    LblSubTitle.Text = "Requirement";
                    ToggleClauseName(true);
                    ChkSCFlownProv.Enabled = true;
                }
                else if ((mode == "reviewadd") || (mode == "reviewedit"))
                {
                    DivReview.Visible = false;
                    DivReviewedit.Visible = false;
                    ToggleVisibility(false);
                    LblSubTitle.Text = "Requirement";
                    ToggleClauseName(true);
                    if (mode == "reviewadd")
                    {
                        DivSubmit.Visible = true;
                        DivUpdate.Visible = false;
                        ToggleSpecial(false);
                        trOwner.Visible = true;
                     }
                    else
                    {
                        DivSubmit.Visible = false;
                        DivUpdate.Visible = true;
                        ToggleSpecial(true);
                    }
                    ChkSCFlownProv.Enabled = false;
                    
                }
                else if ((mode == "view") || (mode == "edit"))
                {
                    DivReview.Visible = false;
                    DivSubmit.Visible = false;
                    DivUpdate.Visible = false;
                    LblSubTitle.Text = "Requirement";
                    ToggleSpecial(true);
                    ToggleClauseName(true);
                    Business.RequirementRecord objReq = new Business.RequirementRecord();

                    objReq = objDml.GetRequirementDetails(HdnReqid.Value);

                    //COMMENTS MS: Validation - Check if the Id has a valid value
                    if (objReq.ReqId != 0)
                    {
                        if (mode == "view")
                        {
                            DivReviewedit.Visible = false;
                            ToggleVisibility(false);
                            FillFormView(objReq, mode);
                            ChkSCFlownProv.Enabled = false;
                        }

                        else if (mode == "edit")
                        {
                            DivReviewedit.Visible = true;
                            ToggleVisibility(true);
                            FillFormEdit(objReq);
                            ChkSCFlownProv.Enabled = true;

                        }
                    }
                    else { RedirectInvalidParam("noobj"); }
                   
                }

            }


        }

        private void ToggleVisibility(bool boolval)
        {
            DivReqmnt.Visible = boolval;
            DivReqmntView.Visible = !boolval;
            DivFrequency.Visible = boolval;
            DivFrequencyView.Visible = !boolval;
            DivStartDate.Visible = boolval;
            DivStartDateView.Visible = !boolval;
            DivUploadReq.Visible = boolval;
            DivUploadReqView.Visible = !boolval;
            DivNotes.Visible = boolval;
            DivNotesView.Visible = !boolval;

            SpanFrequency.Visible = boolval;
            SpnRequirement.Visible = boolval;
            Divreq.Visible = boolval;

        }

        private void ToggleSpecial(bool boolval)
        {
            //Only on the View and Edit pages
            trId.Visible = boolval;
            trCMNotify.Visible = boolval;
            trNotifiedDate.Visible = boolval;
        }

        private void SubclauseDetails(Business.RequirementRecord objReq)
        {

            if ((objReq.Subclause != null) && (objReq.Subclause.ToString() != ""))
            {
                trSubclause.Visible = true;
                LblSubclauseVal.Text = ReturnSubclause(objReq.SubclauseNumber, objReq.Subclause);
            }
            else
            {
                trSubclause.Visible = false;

            }
        }

        private void FillFormView(Business.RequirementRecord objReq, string mode)
        {
            CommonForViewEdit(objReq, mode);
            LblReqmntVal.Text = objCommon.WrapNeat(objReq.Req);
            LblNotesVal.Text = objCommon.WrapNeat(objReq.Notes);
            LblFrequencyVal.Text = objReq.Frequency;
            LblStartDateVal.Text = (objReq.StartDate != DateTime.MinValue) ? objReq.StartDate.ToShortDateString() : "-";
            LblUploadReqVal.Text = objReq.UploadFileReq;
            if (mode == "view")
            {
                DivEdit.Visible = true;
                DivDelete.Visible = true;
            }
            else { DivEdit.Visible = false; DivDelete.Visible = false; }
          

        }

        private void CommonForViewEdit(Business.RequirementRecord objReq, string mode)
        {
            if (mode != "reviewadd")
            {
                LblReqIdVal.Text = objReq.ReqId.ToString();
                LblCMNotifyVal.Text = (objReq.IsCMNotified != "") ? objReq.IsCMNotified.ToString() : "N";
                LblNotifiedDateVal.Text = (objReq.NotifiedDate != DateTime.MinValue) ? objReq.NotifiedDate.ToShortDateString() : "-";
                trClauseno.Visible = false;
                if (mode != "reviewedit") SubclauseDetails(objReq);
            }
            //COMMENTS MS: Need this to be here and not in Common for view /edit as it needed for reviewadd also
            LblClauseVal.Text = ((objReq.ClauseNumber != "") && (objReq.ClauseNumber != null)) ? (objReq.ClauseNumber + ", " + objReq.Clause) : objReq.Clause;
            HdnClauseid.Value = objReq.ClauseId.ToString();
            trOwner.Visible = true;
            LblOwnerVal.Text = objReq.Owner;
            trSCFlown.Visible = true;
            ChkSCFlownProv.Checked = (objReq.SubContractorFlownProvision == "Y") ? true : false;
       }

        private void FillFormEdit(Business.RequirementRecord objReq)
        {
            CommonForViewEdit(objReq, "edit");
            LblClauseVal.Text = (objReq.ClauseNumber != "") ? (objReq.ClauseNumber + ", " + objReq.Clause) : objReq.Clause;
            HdnClauseid.Value = objReq.ClauseId.ToString();
            TxtReqmnt.Text = objReq.Req;
            TxtNotes.Text = objReq.Notes;

            int count = DdlFrequency.Items.Count;
            if (count == 0)
            {
                DdlFrequency.DataSourceID = "SDSFrequency";
                DdlFrequency.DataTextField = "LOOKUP_DESC";
                DdlFrequency.DataValueField = "LOOKUP_ID";
                DdlFrequency.DataBind();

            }
            if (!string.IsNullOrEmpty(objReq.FrequencyId.ToString()))
            {
                if (DdlFrequency.Items.FindByValue(objReq.FrequencyId.ToString()) != null)
                {
                    DdlFrequency.ClearSelection();
                    DdlFrequency.Items.FindByValue(objReq.FrequencyId.ToString()).Selected = true;
                }
                else
                {
                    DdlFrequency.SelectedIndex = 0;
                }
            }
            TxtStartDate.Text = (objReq.StartDate != DateTime.MinValue) ? objReq.StartDate.ToShortDateString() : "";
            ChkUploadReq.Checked = (objReq.UploadFileReq == "Y") ? true : false;

        }

        private void FillReqObject(string mode, string redrt="")
        {
            Business.RequirementRecord objReqmt = new Business.RequirementRecord();

            if ((mode != "new") && (mode != "reviewadd"))
            {
                objReqmt.ReqId = Convert.ToInt32(LblReqIdVal.Text);
                objReqmt.NotifiedDate = (LblNotifiedDateVal.Text != "-") ? Convert.ToDateTime(LblNotifiedDateVal.Text) : DateTime.MinValue;
                objReqmt.IsCMNotified = LblCMNotifyVal.Text;

            }

            if ((mode == "new") || (mode == "reviewadd"))
            {
                objReqmt.CreatedBy = objCommon.GetUserID();
                objReqmt.ClauseId = (ViewState["qsclid"] != null) ? ((HdnClauseid.Value != "") ? Convert.ToInt32(HdnClauseid.Value) : 0) : ((DdlClause.SelectedIndex != 0) ? Convert.ToInt32(DdlClause.SelectedValue) : 0);

                if (ViewState["qsclid"] != null)
                {
                    string[] _clause = LblClauseVal.Text.Split(',');
                    objReqmt.ClauseNumber = _clause[0];
                    objReqmt.Clause = _clause[1];

                }
                else
                {
                    objReqmt.Clause = (DdlClause.SelectedIndex != 0) ? DdlClause.SelectedItem.Text : "";
                }
                if (trSubclause.Visible)
                {
                    string[] _subclause = LblSubclauseVal.Text.Split(',');
                    objReqmt.SubclauseNumber = _subclause[0];
                    if ((objReqmt.Subclause != "n/a") && (_subclause.Length > 1))
                    { objReqmt.Subclause = _subclause[1]; }
                    else { objReqmt.Subclause = ""; }
                }
                else
                {
                    objReqmt.SubclauseNumber = "";
                    objReqmt.Subclause = "";
                }
                if (trOwner.Visible)
                {
                    objReqmt.Owner = LblOwnerVal.Text;
                }
             
            }

            if ((mode == "edit") || (mode == "reviewedit"))
            {
                objReqmt.ModifiedBy = objCommon.GetUserID();
                objReqmt.ClauseId = (HdnClauseid.Value != "") ? Convert.ToInt32(HdnClauseid.Value) : 0;
                objReqmt.ClauseNum = LblClausenoVal.Text;
                objReqmt.Owner = LblOwnerVal.Text;
                string[] _clause = LblClauseVal.Text.Split(',');
                objReqmt.ClauseNumber = _clause[0];
                objReqmt.Clause = _clause[1];
                if (trSubclause.Visible)
                {
                    string[] _subclause = LblSubclauseVal.Text.Split(',');
                    objReqmt.SubclauseNumber = _subclause[0];
                    if (_subclause.Length > 1) objReqmt.Subclause = _subclause[1];
                }
                else
                {
                    objReqmt.SubclauseNumber = "";
                    objReqmt.Subclause = "";
                }
             

            }
            objReqmt.Req = Business.WordCharExtension.ReplaceWordChars(TxtReqmnt.Text);
            objReqmt.Notes = Business.WordCharExtension.ReplaceWordChars(TxtNotes.Text);
            objReqmt.FrequencyId = (DdlFrequency.SelectedIndex != 0) ? Convert.ToInt32(DdlFrequency.SelectedValue) : 0;
            objReqmt.Frequency = (DdlFrequency.SelectedIndex != 0) ? DdlFrequency.SelectedItem.Text : "";
            objReqmt.StartDate = (TxtStartDate.Text != "") ? Convert.ToDateTime(TxtStartDate.Text) : DateTime.MinValue;
            objReqmt.UploadFileReq = (ChkUploadReq.Checked) ? "Y" : "N";
            objReqmt.SubContractorFlownProvision = (ChkSCFlownProv.Checked) ? "Y" : "N";
  
           
            if ((mode == "reviewadd") || (mode == "reviewedit"))
            {
                FillFormView(objReqmt, mode);

            }
            else if (mode == "new")
            {
                SaveRequirement(objReqmt,redrt);
            }
            else if (mode == "edit")
            {
                UpdateRequirement(objReqmt,redrt);
            }
        }

        #endregion

        #region "List related"
        protected void GvRequirement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvRequirement.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void GvRequirement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Combine clause name, clause no and subclause no, subclause together
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;
                string _clausename;
                string _subclausename;

                _clausename = rowView["CLAUSE_NAME"].ToString();
                _subclausename = rowView["SUBCLAUSENAME"].ToString();

                e.Row.Cells[5].Text = rowView["CLAUSENUM"] + ", " + _clausename;
                e.Row.Cells[6].Text = ReturnSubclause(rowView["SUBCLAUSENUM"].ToString(), _subclausename);
 
            }
        }
        protected string ReturnSubclause(string _subclausenum, string _subclausename)
        {
            //If subclause name is not there, during input it has to be substituted with "n/a"
            if ((_subclausename != "n/a") && (_subclausenum != "n/a"))
            {
                return _subclausenum + ", " + _subclausename;
            }
            else if (_subclausenum != "n/a")
                return _subclausenum;
            else if (_subclausename != "n/a")
                return _subclausename;
            else
                return _subclausenum;
        }

        protected void GvRequirement_Sorted(object sender, EventArgs e)
        {
            string _sortExpressionLabel = "";
            string _sortDirectionLabel = "";
            
          
            switch (ViewState["sort"].ToString().ToUpper())
            {
                case "REQUIREMENT_ID":
                    _sortExpressionLabel = "Requirement Id";
                    break;
                case "REQUIREMENT":
                    _sortExpressionLabel = "Requirement";
                    break;
                case "FREQUENCY":
                    _sortExpressionLabel = "Frequency";
                    break;
                case "START_DATE":
                    _sortExpressionLabel = "Start Date";
                    break;
                case "CLAUSENUM":
                    _sortExpressionLabel = "Clause";
                    break;
                case "CONTRACT":
                    _sortExpressionLabel = "Contract";
                    break;
                case "SUBCLAUSENUM":
                    _sortExpressionLabel = "Subclause";
                    break;
                default:
                    _sortExpressionLabel = "";
                    break;
            }
            switch (ViewState["sortdirection"].ToString())
            {
                case "ASC":
                    _sortDirectionLabel = "Ascending";
                    break;
                case "DESC":
                    _sortDirectionLabel = "Descending";
                    break;
                default:
                    _sortDirectionLabel = "";
                    break;

            }
            if ((_sortExpressionLabel != "") && (_sortDirectionLabel != ""))
            {
                LblFooter.Text = "Sorted by " + _sortExpressionLabel +
                            " in " + _sortDirectionLabel + " order.";
            }

      
        }

        protected void GvRequirement_Sorting(object sender, GridViewSortEventArgs e)
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
                    ViewState["sort"] = "REQUIREMENT_ID";
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

        private void BindGrid()
        {
            StringBuilder _sbFilter = new StringBuilder();

            using (OracleCommand _cmdList = new OracleCommand())
            {
                if (HdnDesc.Value != "")
                {
                    _sbFilter = SetSBFilter(_sbFilter);
                    _sbFilter.Append(" LOWER(REQUIREMENT) LIKE :Reqmnt OR LOWER(CONTRACT_NAME) LIKE :Reqmnt OR LOWER(CLAUSE_NAME) LIKE :Reqmnt OR LOWER(CLAUSE_NUMBER) LIKE :Reqmnt");
                    _sbFilter.Append(" OR LOWER(OWNERNAME) LIKE :Reqmnt OR LOWER(FREQUENCY) LIKE :Reqmnt OR LOWER(SUBCLAUSENUM) LIKE :Reqmnt OR LOWER(SUBCLAUSENAME) LIKE :Reqmnt OR LOWER(NOTES) LIKE :Reqmnt");
                    _cmdList.Parameters.Add(":Reqmnt", OracleType.VarChar).Value = "%" + Server.HtmlEncode(HdnDesc.Value.ToLower()) + "%";
                }

                FillReqDetails(_sbFilter.ToString(), _cmdList);
            }
        }

        protected void FillReqDetails(string filter, OracleCommand cmdList)
        {
            DataSet _dsReq = new DataSet();
            filter += " ORDER BY " + SortExpression + " " + SortDirect;

            _dsReq = objDml.GetRequirementInfo(filter, cmdList);
            ViewState["reqinfo"] = _dsReq.Tables["reqinfo"];

            if (_dsReq.Tables["reqinfo"].Rows.Count > 0)
            {
                LblFooter.Visible = true;
                GvRequirement.DataSource = _dsReq.Tables["reqinfo"];
                GvRequirement.DataBind();
            }
            else
            {
                LblInfo.Visible = false;
                LblFooter.Visible = false;
                GvRequirement.DataSource = null;
                GvRequirement.DataBind();
            }


        }

        protected void cmdFind_Click(object sender, EventArgs e)
        {
            HdnDesc.Value = TxtReqName.Text.Trim();
            BindGrid();
        }
        #endregion

        # region "Database manipulations"
        private void SaveRequirement(Business.RequirementRecord objReq,string redrt)
        {
            string _reqId = objDml.CreateRequirement(objReq.Req, objReq.Notes, objReq.FrequencyId, objReq.UploadFileReq, objReq.StartDate, objReq.CreatedBy, objReq.ClauseId, objReq.SubContractorFlownProvision);
            HdnReqid.Value = _reqId;

            if (_reqId != "0")
            {
                if (redrt == "")
                {
                    objCommon.CreateMessageAlertSM(this, "Requirement Added", "Info", "Requirement.aspx?mode=view&id=" + HdnReqid.Value);
                }
                else if (redrt == "deli")
                {
                    objCommon.CreateMessageAlertSM(this, "Requirement Added", "Info", "../Deliverable.aspx?mode=add&reqid=" + HdnReqid.Value);
                }

            }
            else
            {
                objCommon.CreateMessageAlertSM(this, "Error! Requirement could not be added", "error", false);
            }
        }

        private void UpdateRequirement(Business.RequirementRecord objReq, string redrt)
        {
            string _result = objDml.UpdateRequirement(objReq.ReqId, objReq.Req, objReq.Notes, objReq.FrequencyId, objReq.UploadFileReq, objReq.StartDate, objReq.ModifiedBy, objReq.SubContractorFlownProvision);
            if (_result == "0")
            {
                if (redrt == "")
                {
                    objCommon.CreateMessageAlertSM(this, "Requirement Updated", "info", "Requirement.aspx?mode=view&id=" + HdnReqid.Value);
                }
                else if (redrt == "deli")
                {
                    objCommon.CreateMessageAlertSM(this, "Requirement Updated", "info", "../Deliverable.aspx?mode=add&reqid=" + HdnReqid.Value);
                }
            }
            else
            {
                objCommon.CreateMessageAlertSM(this, "Error! Requirement could not be updated", "error", false);
            }
        }

        protected Business.ClauseRecord Clause(string clauseId)
        {
            Business.ClauseRecord objClause = new Business.ClauseRecord();
            objClause = objDml.GetClauseDetails(clauseId);
            return objClause;

        }
        #endregion

        # region "control events"

        protected void DdlFrequency_DataBound(object sender, EventArgs e)
        {
            DdlFrequency.Items.Insert(0, new ListItem("--Choose One--", "0"));
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("Requirement.aspx?mode=new");
        }

        protected void BtnEdit_Click(object sender, EventArgs e)
        {
            DivEdit.Visible = false;
            Response.Redirect("Requirement.aspx?mode=edit&id=" + HdnReqid.Value);
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            DivDelete.Visible = false;
            string _errCode = "";
            string _objChild = "";
            string _objParent = "";

            bool _isChildExists = true;

            _isChildExists = objDml.CheckIfChildExists(HdnReqid.Value, "req");
            _objChild = "Deliverable";
            _objParent = "Requirement";

            if (!_isChildExists)
            {
                _errCode = objDml.DeleteRequirement(Convert.ToInt32(HdnReqid.Value), objCommon.GetUserID());
            }
            else
            {
                _errCode = "notvalid";
            }

            if (_errCode == "err")
            {
                objCommon.CreateMessageAlertSM(this, "Error! " + _objParent + " could not be deleted. //n Please try later", "error", false);
                SetPage("view");
            }
            else if (_errCode == "notvalid")
            {
                objCommon.CreateMessageAlertSM(this, _objChild+ "/s exists under this " + _objParent + ". Please delete the " + _objChild + "/s first and try again. ", "error", false);
                SetPage("view");
            }
            else
            { objCommon.CreateMessageAlertSM(this, _objParent + " marked as deleted successfully", "msg", true); }
       

        }

        protected void BtnReview_Click(object sender, EventArgs e)
        {
            SetPage("reviewadd");
            FillReqObject("reviewadd");

        }

        protected void BtnReview2_Click(object sender, EventArgs e)
        {
            SetPage("reviewedit");
            FillReqObject("reviewedit");
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "DisableSubmit('new');", true);
            FillReqObject("new");
        }

        protected void BtnChange_Click(object sender, EventArgs e)
        {
            SetPage("changeadd");
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "DisableSubmit('edit');", true);
            FillReqObject("edit");
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            SetPage("view");
            BtnEdit.Visible = true;
        }

        protected void BtnMoreChange_Click(object sender, EventArgs e)
        {
            SetPage("changeedit");
        }

        protected void DdlClause_DataBound(object sender, EventArgs e)
        {
            DdlClause.Items.Insert(0, new ListItem("--Choose One--", "0"));
        }

        protected void LblReqmntVal_PreRender(object sender, EventArgs e)
        {
            objCommon.WrapNeat(LblReqmntVal.Text);

        }

        protected void LblNotesVal_PreRender(object sender, EventArgs e)
        {
            objCommon.WrapNeat(LblNotesVal.Text);
        }

        protected void DdlClause_SelectedIndexChanged(object sender, EventArgs e)
        {
            Business.ClauseRecord objClause = Clause(DdlClause.SelectedValue);
            trOwner.Visible = true;
            if (objClause.ClauseType.Equals("Subclause"))
            {
                trSubclause.Visible = true;
                LblSubclauseVal.Text = ReturnSubclause(objClause.ClauseNumber, objClause.ClauseName);
                LblOwnerVal.Text = objCommon.GetEmpname(objClause.ParentOwner.ToString());
            }
            else
            {
                trSubclause.Visible = false;
                LblOwnerVal.Text = objClause.OwnerName;
            }
        }

        #endregion

        protected void BtnSaveAddDeli_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "DisableSubmit('new');", true);
            FillReqObject("new","deli");
        }

        protected void BtnUpdateAddDeli_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "key", "DisableSubmit('edit');", true);
            FillReqObject("edit","deli");
        }

  
  
    }
}