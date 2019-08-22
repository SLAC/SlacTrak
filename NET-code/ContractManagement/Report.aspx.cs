//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  Report.aspx.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is for the customized report
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

namespace ContractManagement
{
    public partial class ReportPost : BasePage
    {
        Business.CMS_Common_Util objCommon = new Business.CMS_Common_Util();
        Data.CMS_DMLUtil objDml = new Data.CMS_DMLUtil();
        protected void Page_Load(object sender, EventArgs e)
        {
            string _page = "";

            if (!Page.IsPostBack)
            {
                _page = Request.QueryString["page"];
                HdnPage.Value = _page;
                if (_page == "cl")
                {
                    DivCl.Visible = true;
                    DivDeli.Visible = false;
                    DivReq.Visible = false;
                    DivReqFD.Visible = false;
                }
                else if (_page == "req")
                {
                    DivCl.Visible = false;
                    DivDeli.Visible = false;
                    DivReq.Visible = true;
                    DivReqFD.Visible = false;
                }
                else if (_page == "reqfd")
                {
                    DivCl.Visible = false;
                    DivDeli.Visible = false;
                    DivReq.Visible = false;
                    DivReqFD.Visible = true;
                }
                else if (_page == "deli")
                {
                    DivCl.Visible = false;
                    DivDeli.Visible = true;
                    DivReq.Visible = false;
                    DivReqFD.Visible = false;
                    trReq.Visible = true;
                    trCustom.Visible = false;
                    string _reqId = Request.QueryString["reqid"];
                    if (_reqId != "") { HdnReqId.Value = _reqId; }
                    BindGrid();
                }
            
                else
                {
                    DivCl.Visible = false;
                    DivDeli.Visible = true;
                    DivReq.Visible = false;
                    trReq.Visible = false;
                    trCustom.Visible = true;

                    if (_page == "fy")
                    {
                        string _fyDue = Business.DateTimeExtension.ToFinancialYearShort(DateTime.Today).ToString();
                        HdnFY.Value = _fyDue;
                        HdnStatus.Value = (int)Status.New + "," + (int)Status.InProgress + "," + (int)Status.Reopened;
                        trReq.Visible = true;
                        trCustom.Visible = false;
                    }
                    else
                    {
                        if ((!String.IsNullOrEmpty(_page)) && (_page.Equals("back")))
                        {
                            PopulateHiddenFields("back");
                        }
                        else
                        {
                            PopulateHiddenFields("");
                        }
                    }
                    BindGrid();
                 }
             }
        }
       
        private void PopulateHiddenFields(string page)
        {
            if (page == "back")
            {
                if (Session["alprevious"] != null)
                {
                    ArrayList _arraylst = new ArrayList();
                    _arraylst = (ArrayList)Session["alprevious"];
                    HdnTrackId.Value = (string)_arraylst[0];                  
                    HdnOwner.Value = (string)_arraylst[2];
                    HdnDesc.Value = (string)_arraylst[3];              
                    HdnFromDuedate.Value = (string)_arraylst[8];
                    HdnToDuedate.Value = (string)_arraylst[9];
                    HdnFromSubmit.Value = (string)_arraylst[10];
                    HdnToSubmit.Value = (string)_arraylst[11];
                    HdnSubowner.Value = (string)_arraylst[12];
                    HdnApprover.Value = (string)_arraylst[13];
                    HdnInfo.Value = (string)_arraylst[14];             
                    HdnStatus.Value = (string)_arraylst[17];             
                    HdnStatusName.Value = (string)_arraylst[19];
                    if (IsSpecialForReport()) 
                    {
                        HdnType.Value = (string)_arraylst[1];
                        HdnDrt.Value = (string)_arraylst[4];
                        HdnDept.Value = (string)_arraylst[5];
                        HdnDrtName.Value = (string)_arraylst[6];
                        HdnDeptName.Value = (string)_arraylst[7];
                        HdnNotify.Value = (string)_arraylst[15];
                        HdnNotifyDesc.Value = (string)_arraylst[16];
                        HdnTypeName.Value = (string)_arraylst[18];
                  }

                }
            }
            else
            {
                HdnTrackId.Value = Server.HtmlEncode(PreviousPage.TrackId);               
                HdnOwner.Value = Server.HtmlEncode(PreviousPage.Owner);
                HdnDesc.Value = Server.HtmlEncode(PreviousPage.Desc);               
                HdnFromDuedate.Value = Server.HtmlEncode(PreviousPage.DueDateFrom);
                HdnToDuedate.Value = Server.HtmlEncode(PreviousPage.DueDateTo);
                HdnFromSubmit.Value = Server.HtmlEncode(PreviousPage.SubmittedFrom);
                HdnToSubmit.Value = Server.HtmlEncode(PreviousPage.SubmittedTo);
                HdnSubowner.Value = Server.HtmlEncode(PreviousPage.Subowner);
                HdnApprover.Value = Server.HtmlEncode(PreviousPage.Approver);
                HdnInfo.Value = Server.HtmlEncode(PreviousPage.IsInfo);               
                HdnStatus.Value = PreviousPage.DeliStatus;                
                HdnStatusName.Value = Server.HtmlEncode(PreviousPage.StatusName);
                if (IsSpecial())
                {
                    HdnType.Value = PreviousPage.DeliType;
                    HdnDrt.Value = Server.HtmlEncode(PreviousPage.Drt);
                    HdnDept.Value = Server.HtmlEncode(PreviousPage.Dept);
                    HdnDrtName.Value = Server.HtmlEncode(PreviousPage.DrtName);
                    HdnDeptName.Value = Server.HtmlEncode(PreviousPage.DeptName);
                    HdnNotify.Value = Server.HtmlEncode(PreviousPage.NotifySchedule);
                    HdnNotifyDesc.Value = Server.HtmlEncode(PreviousPage.NotifyScheduleDesc);
                    HdnTypeName.Value = Server.HtmlEncode(PreviousPage.TypeName);
                }
                AddToArrayList(); //Needed for Backbutton functionality
            }
          
        }

        protected void AddToArrayList()
        {
            ArrayList _alPreviouspage = new ArrayList();
            _alPreviouspage.Add(HdnTrackId.Value);
            _alPreviouspage.Add(HdnType.Value);
            _alPreviouspage.Add(HdnOwner.Value);
            _alPreviouspage.Add(HdnDesc.Value);
            _alPreviouspage.Add(HdnDrt.Value);
            _alPreviouspage.Add(HdnDept.Value);
            _alPreviouspage.Add(HdnDrtName.Value);
            _alPreviouspage.Add(HdnDeptName.Value);
            _alPreviouspage.Add(HdnFromDuedate.Value);
            _alPreviouspage.Add(HdnToDuedate.Value);
            _alPreviouspage.Add(HdnFromSubmit.Value);
            _alPreviouspage.Add(HdnToSubmit.Value);
            _alPreviouspage.Add(HdnSubowner.Value);
            _alPreviouspage.Add(HdnApprover.Value);
            _alPreviouspage.Add(HdnInfo.Value);
            _alPreviouspage.Add(HdnNotify.Value);
            _alPreviouspage.Add(HdnNotifyDesc.Value);
            _alPreviouspage.Add(HdnStatus.Value);
            _alPreviouspage.Add(HdnTypeName.Value);
            _alPreviouspage.Add(HdnStatusName.Value);
            Session["alprevious"] = _alPreviouspage;

        }

        protected void GVDeli_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVDeli.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void GVDeli_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           // Hide Directorate and Department for Owners/Subowners
            //Display approved by default as approved for owners/subowners
            if (!(IsSpecial()))
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowview = (DataRowView)e.Row.DataItem;
                    string _status = Convert.ToString(rowview["STATUS_DESC"]);

                    if (_status.Equals("Approved by Default"))
                    {
                        e.Row.Cells[5].Text = "Approved";
                    }
                }
            }

        }

        protected void GVDeli_Sorting(object sender, GridViewSortEventArgs e)
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

        private void BindGrid()
        {

            StringBuilder _sbFilter = new StringBuilder();
            StringBuilder _sbText = new StringBuilder();

            using (OracleCommand _cmdList = new OracleCommand())
            {
               


                if (HdnTrackId.Value != "")
                {
                    _sbFilter = SetSBFilter(_sbFilter);
                    _sbFilter.Append(" LOWER(COMPOSITE_KEY) LIKE :TrackId");
                    _cmdList.Parameters.Add(":TrackId", OracleType.VarChar).Value = "%" + Server.HtmlEncode(HdnTrackId.Value.ToLower()) + "%";
                    _sbText.Append(" Track ID contains " + Server.HtmlDecode(HdnTrackId.Value.ToString()) + ",");
                }

                if (HdnType.Value != "")
                {
                   
                    if (HdnType.Value != "0")
                    {
                        _sbFilter = SetSBFilter(_sbFilter);
                       string _typeList = ValidateListForNumbers(HdnType.Value);

                       //COMMENTS MS: Issue - Was not able to use command parameters for string list. Tried few workarounds but couldn't make it work.
                        //TODO: check the input parameter thoroughly using regex so that there is no sql injection
                       _sbFilter.Append(" TYPE_ID IN (" + _typeList + ")");
                        _sbText.Append(" Type in (" + Server.HtmlDecode(HdnTypeName.Value.ToString()) + "),");
                   }                    
                }
                if (HdnOwner.Value != "")
                {                  
                    string _ownerId = objCommon.GetEmpid(HdnOwner.Value).ToString();
                    _sbFilter = SetSBFilter(_sbFilter);
                    _sbFilter.Append(" OWNERID = :OwnerId");
                    _cmdList.Parameters.Add(":OwnerId", OracleType.VarChar).Value = _ownerId;
                    _sbText.Append(" Owner is " + Server.HtmlDecode(HdnOwner.Value) + ",");
                }
                if (HdnDesc.Value != "")
                {
                    _sbFilter = SetSBFilter(_sbFilter);
                    _sbFilter.Append(" (LOWER(REQUIREMENT) LIKE :Reqdesc OR LOWER(DESCRIPTION) LIKE :Reqdesc)");
                    _cmdList.Parameters.Add(":Reqdesc", OracleType.VarChar).Value = "%" + Server.HtmlEncode(HdnDesc.Value.ToLower()) + "%";
                    _sbText.Append(" Requirement or Deliverable description contains " + Server.HtmlDecode(HdnDesc.Value) + "," );
                }
                if ((HdnDrt.Value != "") && (HdnDrt.Value != "0"))
                {
                    _sbFilter = SetSBFilter(_sbFilter);
                    _sbFilter.Append(" DIRECTORATE_ID = :DrtId ");
                    _cmdList.Parameters.Add(":DrtId", OracleType.VarChar).Value = Server.HtmlEncode(HdnDrt.Value);
                    _sbText.Append(" Directorate is " + Server.HtmlDecode(HdnDrtName.Value) + ",");
                }

                if ((HdnDept.Value != "") && (HdnDept.Value != "0"))
                {
                    _sbFilter = SetSBFilter(_sbFilter);
                    _sbFilter.Append(" DEPARTMENT_ID = :DeptId ");
                    _cmdList.Parameters.Add(":DeptId", OracleType.VarChar).Value = Server.HtmlEncode(HdnDept.Value);
                    _sbText.Append(" Department is " + Server.HtmlDecode(HdnDeptName.Value) + ",");
                }

                if (HdnFromDuedate.Value != "")
                {
                    _sbFilter = SetSBFilter(_sbFilter);
                    _sbFilter.Append(" TO_DATE(TO_CHAR(DUE_DATE,'mm/dd/yyyy'),'mm/dd/yyyy') >= TO_DATE(:FromDueDate, 'mm/dd/yyyy') ");
                    _cmdList.Parameters.Add(":FromDueDate", OracleType.VarChar).Value = HdnFromDuedate.Value;
                    _sbText.Append(" Due date from " + HdnFromDuedate.Value + ",");
                }

                if (HdnToDuedate.Value != "")
                {
                    _sbFilter = SetSBFilter(_sbFilter);
                    _sbFilter.Append("  TO_DATE(TO_CHAR(DUE_DATE,'mm/dd/yyyy'),'mm/dd/yyyy') <= TO_DATE(:ToDueDate,'mm/dd/yyyy') ");
                    _cmdList.Parameters.Add(":ToDueDate", OracleType.VarChar).Value = HdnToDuedate.Value;
                    _sbText.Append(" Due date to " + HdnToDuedate.Value + ",");
                }

                if (HdnFromSubmit.Value != "")
                {
                    _sbFilter = SetSBFilter(_sbFilter);
                    _sbFilter.Append(" TO_DATE(TO_CHAR(DATE_SUBMITTED,'mm/dd/yyyy'),'mm/dd/yyyy') >= TO_DATE(:FromDate, 'mm/dd/yyyy') ");
                    _cmdList.Parameters.Add(":FromDate", OracleType.VarChar).Value = HdnFromSubmit.Value;
                    _sbText.Append(" Submitted from " + HdnFromSubmit.Value + ",");
                }

                if (HdnToSubmit.Value != "")
                {
                    _sbFilter = SetSBFilter(_sbFilter);
                    _sbFilter.Append("  TO_DATE(TO_CHAR(DATE_SUBMITTED,'mm/dd/yyyy'),'mm/dd/yyyy') <= TO_DATE(:ToDate,'mm/dd/yyyy') ");
                    _cmdList.Parameters.Add(":ToDate", OracleType.VarChar).Value = HdnToSubmit.Value;
                    _sbText.Append(" Submitted to " + HdnToSubmit.Value + ",");
                }

                if (HdnSubowner.Value != "")
                {
                    
                    string _deliIdforUser = GetDeliverablesAsList(objCommon.GetEmpid(Server.HtmlEncode(HdnSubowner.Value)).ToString(), userType:"so");
                    _sbFilter = SetSBFilter(_sbFilter);
                    if (_deliIdforUser == "")
                    {
                        _deliIdforUser = "0";
                    }
                        _sbFilter.Append(" DELIVERABLE_ID IN (" + _deliIdforUser + ")");
                        _sbText.Append(" Subowner is " + Server.HtmlDecode(HdnSubowner.Value) + ",");
                   
                 
                    
                }

                if (HdnApprover.Value != "")
                {
                   
                    string _deliIdApprover = GetDeliverablesAsList(objCommon.GetEmpid(Server.HtmlEncode(HdnApprover.Value)).ToString(), userType:"appvr");
                    _sbFilter = SetSBFilter(_sbFilter);
                    if (_deliIdApprover != "")
                    {

                        _sbFilter.Append(" DELIVERABLE_ID IN (" + _deliIdApprover + ")");
                    }
                    else
                    {
                        _sbFilter.Append(" DELIVERABLE_ID = 0 ");
                    }

                    _sbText.Append(" Approver is " + Server.HtmlDecode(HdnApprover.Value) + ",");
                }

                if ((HdnNotify.Value != "") && (HdnNotify.Value !="0"))
                {
                   
                    string _deliIdNotify = GetDeliverablesAsList("",lookupId: Server.HtmlEncode(HdnNotify.Value));
                    if (_deliIdNotify != "")
                    {
                        _sbFilter = SetSBFilter(_sbFilter);
                        _sbFilter.Append(" DELIVERABLE_ID IN (" + _deliIdNotify + ")");
                    }
                    _sbText.Append(" Notification Schedule is " + Server.HtmlDecode(HdnNotifyDesc.Value) + ",");
                }
                if (HdnInfo.Value != "")
                {
                    
                    if (HdnInfo.Value == "Y")
                    {
                        _sbFilter = SetSBFilter(_sbFilter);
                        _sbFilter.Append(" IS_INFORMATION_ONLY = 'Y'");
                        _sbText.Append(" Information Only is " + Server.HtmlDecode(HdnInfo.Value) + ",");
                    }
                   
                }
                if (HdnStatus.Value != "")
                {
                   //Pull in approved and approved by default together for Owner/subowners
                    if (HdnStatus.Value != "0")
                    {
                        _sbFilter = SetSBFilter(_sbFilter);
                        string _statuslist = ValidateListForNumbers(HdnStatus.Value);
                        _sbFilter.Append("  STATUS_ID IN (" + _statuslist + ")");
                        //_cmdList.Parameters.Add(":StatusId", OracleType.VarChar).Value = "(" + Server.HtmlEncode(HdnStatus.Value) + ")";
                    }
                    if (HdnStatusName.Value != "")
                    _sbText.Append(" Status in " + Server.HtmlDecode(HdnStatusName.Value) + ",");
                }

                if (HdnReqId.Value != "")
                {
                    _sbFilter = SetSBFilter(_sbFilter);
                    _sbFilter.Append(" REQUIREMENT_ID = :ReqId");
                    _cmdList.Parameters.Add(":ReqId", OracleType.VarChar).Value = Server.HtmlEncode(HdnReqId.Value);
                    _sbText.Append(" Requirement Id " + Server.HtmlDecode(HdnReqId.Value.ToString()));
                }

                if (HdnFY.Value != "")
                {
                    _sbFilter = SetSBFilter(_sbFilter);
                    _sbFilter.Append(" FYDUE = :FYdue");
                    _cmdList.Parameters.Add(":FYdue", OracleType.VarChar).Value = Server.HtmlEncode(HdnFY.Value);
                   
                }

                _sbFilter = AddtoQueryifOwner(_sbFilter);

                ViewState["criteria"] = _sbFilter.ToString();
                if (_sbText.Length > 1)
                {
                    ViewState["criteriatext"] = (_sbText.Remove(_sbText.Length - 1, 1)).ToString();
                }
                FillDeliDetails(_sbFilter.ToString(), _cmdList);
            }

          
        }

        protected StringBuilder AddtoQueryifOwner(StringBuilder sbFilter)
        {
            if (!IsSpecialForReport())
            {
                sbFilter = SetSBFilter(sbFilter);
                string _deliIdforUser = GetDeliverablesAsList(objCommon.GetUserID().ToString(), userType:"so");
                sbFilter.Append(" ( (OWNERID = '");
                sbFilter.Append(objCommon.GetUserID());
                sbFilter.Append("'");
                if (_deliIdforUser != "")
                {
                    sbFilter.Append(")");
                    sbFilter.Append(" OR (");
                    sbFilter.Append(" DELIVERABLE_ID IN (" + _deliIdforUser + ")");
                }
                sbFilter.Append("))");
            }
            return sbFilter;

        }
  
        protected void FillDeliDetails(string filter, OracleCommand cmdList)
        {
            DataSet _dsDeli = new DataSet();
            filter += " ORDER BY " + SortExpression + " " + SortDirect;

            _dsDeli = objDml.GetDeliverableInfo(filter, cmdList);
            ViewState["deli"] = _dsDeli.Tables["deli"];


            DataView _newdv = new DataView(ViewState["deli"] as DataTable);
            int _count = 0;

          _count = _newdv.Count; 
            
            if (_count > 0)
            {
                LblInfo.Visible = true;         
                GVDeli.DataSource = _newdv;
                ImgBtnExport.Visible = true;
            }
            else
            {
                 LblInfo.Visible = false;
                 GVDeli.DataSource = null;
                 ImgBtnExport.Visible = false;
            }

            string _appendText="";
            string _defaultText = "";
            if (HdnPage.Value == "fy")
            {
                _defaultText = "You have";
                _appendText = "remaining due this FY";
                
            }
            else
            {
                _defaultText = "Found";
                _appendText = "that matched your criteria:";
                
            }
            LblInfo.Text = _defaultText +  "<b> " + _count + " </b> items " + _appendText;
            GVDeli.DataBind();
            if (ViewState["criteriatext"] != null)
            {
                LblFiltertext.Text = ViewState["criteriatext"].ToString();
            }
        }

        public string SortExpression
        {
            get
            {
                if (null == ViewState["sort"])
                {
                    ViewState["sort"] = "DELIVERABLE_ID";
                }
                return ViewState["sort"].ToString();
            }

            set { ViewState["sort"] = value; }
        }

        public string SortDirect
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

        protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
            DataTable _dtTemp = new DataTable();
            _dtTemp = AppendToTable(_dtTemp);
            DataGrid dg = new DataGrid();
            dg.DataSource = _dtTemp;
            dg.DataBind();
            ExportToExcel("Deliverable-Report.xls", dg);
            dg = null;
            dg.Dispose();
        }

        private DataTable AppendToTable(DataTable dtTemp)
        {
            DataTable _dtOriginal = new DataTable();
            _dtOriginal = ViewState["deli"] as DataTable;

            DataTable _dtRequirement = new DataTable();
            dtTemp.Columns.Add("<b>Deliverable Id</b>");
            dtTemp.Columns.Add("<b>Track Id</b>");
            dtTemp.Columns.Add("<b>Description</b>");
            dtTemp.Columns.Add("<b>Type Name</b>");
            dtTemp.Columns.Add("<b>Due Date</b>");
            dtTemp.Columns.Add("<b>Owner</b>");
            dtTemp.Columns.Add("<b>Directorate</b>");
            dtTemp.Columns.Add("<b>Department</b>");
            dtTemp.Columns.Add("<b>Status</b>");
            dtTemp.Columns.Add("<b>Subowners</b>");
            dtTemp.Columns.Add("<b>Date Submitted</b>");
            dtTemp.Columns.Add("<b>Approvers</b>");
            dtTemp.Columns.Add("<b>Date Approved</b>");
            dtTemp.Columns.Add("<b>Notification Schedule</b>");
            dtTemp.Columns.Add("<b>Upload File Required</b>");
            dtTemp.Columns.Add("<b>Info only?</b>");
            dtTemp.Columns.Add("<b>Notify Manager</b>");
            dtTemp.Columns.Add("<b>Reason for Rejection</b>");
            dtTemp.Columns.Add("<b>Requirement Id<b>");
            dtTemp.Columns.Add("<b>Requirement</b>");
            dtTemp.Columns.Add("<b>Notes</b>");
            dtTemp.Columns.Add("<b>Frequency</b>");
            dtTemp.Columns.Add("<b>Start Date</b>");        
            dtTemp.Columns.Add("<b>Clause Number</b>");
            dtTemp.Columns.Add("<b>Clause Name</b>");
            dtTemp.Columns.Add("<b>Clause Owner</b>");
            dtTemp.Columns.Add("<b>Contract Name</b>");
           
            DataRow _drowAddItem;
            DateTime _dtDuedate;
            DateTime _dtDateApp;
            DateTime _dtDateSub;
            DateTime _dtStartdate;

            for (int i = 0; i < _dtOriginal.Rows.Count; i++)
            {
                _drowAddItem = dtTemp.NewRow();

               
                _drowAddItem[0] = _dtOriginal.Rows[i]["DELIVERABLE_ID"].ToString();
                _drowAddItem[1] = _dtOriginal.Rows[i]["COMPOSITE_KEY"].ToString();
                _drowAddItem[2] = _dtOriginal.Rows[i]["DESCRIPTION"].ToString();
                _drowAddItem[3] = _dtOriginal.Rows[i]["TYPENAME"].ToString();
                _dtDuedate = Convert.ToDateTime(_dtOriginal.Rows[i]["DUE_DATE"].ToString());
                _drowAddItem[4] = _dtDuedate.ToShortDateString();
                _drowAddItem[5] = _dtOriginal.Rows[i]["OWNER"].ToString();
                _drowAddItem[6] = _dtOriginal.Rows[i]["DIRECTORATE"].ToString();
                _drowAddItem[7] = _dtOriginal.Rows[i]["DEPTNAME"].ToString();
                _drowAddItem[8] = _dtOriginal.Rows[i]["STATUS_DESC"].ToString();
                _drowAddItem[9] = ConvertListToStrArr("so", _dtOriginal.Rows[i]["DELIVERABLE_ID"].ToString());
                if (_dtOriginal.Rows[i]["DATE_SUBMITTED"] != null)
                {
                    if (_dtOriginal.Rows[i]["DATE_SUBMITTED"].ToString() != "")
                    {
                        _dtDateSub = Convert.ToDateTime(_dtOriginal.Rows[i]["DATE_SUBMITTED"]);
                        _drowAddItem[10] = _dtDateSub.ToShortDateString();
                    }
                    else _drowAddItem[10] = "";
                }
                else _drowAddItem[10] = "";
                _drowAddItem[11] = ConvertListToStrArr("appr", _dtOriginal.Rows[i]["DELIVERABLE_ID"].ToString());
                if (_dtOriginal.Rows[i]["DATE_APPROVED"] != null) 
                {
                    if (_dtOriginal.Rows[i]["DATE_APPROVED"].ToString() != "")
                    {
                        _dtDateApp = Convert.ToDateTime(_dtOriginal.Rows[i]["DATE_APPROVED"]);
                        _drowAddItem[12] = _dtOriginal.Rows[i]["DATE_APPROVED"].ToString();
                    }
                    else  _drowAddItem[12] = "";
                }
                else _drowAddItem[12] = "";
                _drowAddItem[13] = ConvertListToStrArr("notify", _dtOriginal.Rows[i]["DELIVERABLE_ID"].ToString());
                _drowAddItem[14] = _dtOriginal.Rows[i]["UPLOAD_FILE_REQUIRED"].ToString();
                _drowAddItem[15] = _dtOriginal.Rows[i]["IS_INFORMATION_ONLY"].ToString();
                _drowAddItem[16] = _dtOriginal.Rows[i]["NOTIFY_MANAGER"].ToString();
                _drowAddItem[17] = _dtOriginal.Rows[i]["REASON_FOR_REJECTION"].ToString();
                _drowAddItem[18] = _dtOriginal.Rows[i]["REQUIREMENT_ID"].ToString();

                //get the requirement details based on requirement id
                if (_dtOriginal.Rows[i]["REQUIREMENT_ID"] != null)
                {
                    if (_dtOriginal.Rows[i]["REQUIREMENT_ID"].ToString() != "")
                    {
                        _dtRequirement = objDml.GetRequirementInfo(_dtOriginal.Rows[i]["REQUIREMENT_ID"].ToString());
                        _drowAddItem[19] = _dtRequirement.Rows[0]["REQUIREMENT"].ToString();
                        _drowAddItem[20] = _dtRequirement.Rows[0]["NOTES"].ToString();
                        _drowAddItem[21] = _dtRequirement.Rows[0]["FREQUENCY"].ToString();
                        if (_dtRequirement.Rows[0]["START_DATE"] != null)
                        {
                            if (_dtRequirement.Rows[0]["START_DATE"].ToString() != "")
                            {
                                _dtStartdate = Convert.ToDateTime(_dtRequirement.Rows[0]["START_DATE"]);
                                _drowAddItem[22] = _dtStartdate.ToShortDateString();
                            }
                            else _drowAddItem[22] = "";
                        }
                        else _drowAddItem[22] = "";
                        _drowAddItem[23] = _dtRequirement.Rows[0]["CLAUSE_NUMBER"].ToString();
                        _drowAddItem[24] = _dtRequirement.Rows[0]["CLAUSE_NAME"].ToString();
                        _drowAddItem[25] = _dtRequirement.Rows[0]["OWNERNAME"].ToString();
                        _drowAddItem[26] = _dtRequirement.Rows[0]["CONTRACT_NAME"].ToString();
                       
                    }
                    else
                    {
                        _drowAddItem[19] = "";
                        _drowAddItem[20] = "";
                        _drowAddItem[21] = "";
                        _drowAddItem[22] = "";
                        _drowAddItem[23] = "";
                        _drowAddItem[24] = "";
                        _drowAddItem[25] = "";
                        _drowAddItem[26] = "";
                       
                    }
                }
                else
                {
                    _drowAddItem[19] = "";
                    _drowAddItem[20] = "";
                    _drowAddItem[21] = "";
                    _drowAddItem[22] = "";
                    _drowAddItem[23] = "";
                    _drowAddItem[24] = "";
                    _drowAddItem[25] = "";
                    _drowAddItem[26] = "";
                  
                }
               
                dtTemp.Rows.Add(_drowAddItem);
            }
            return dtTemp;

        }

        private void ExportToExcel(string strFileName, DataGrid dg)
        {
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment; filename=" + strFileName);
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();

        }

        protected void GVDeli_Sorted(object sender, EventArgs e)
        {
            string _sortExpressionLabel = "";
            string _sortDirectionLabel = "";

            switch (ViewState["sort"].ToString().ToUpper())
            {
                case "DELIVERABLE_ID":
                    _sortExpressionLabel = "Deliverable Id";
                    break;
                case "COMPOSITE_KEY":
                    _sortExpressionLabel = "Track Id";
                    break;
                case "TYPENAME":
                    _sortExpressionLabel = "Type";
                    break;
                case "DUE_DATE":
                    _sortExpressionLabel = "Due date";
                    break;
                case "REQUIREMENT":
                    _sortExpressionLabel = "Requirement";
                    break;
                case "STATUS_DESC":
                    _sortExpressionLabel = "Status";
                    break;
                case "OWNER":
                    _sortExpressionLabel = "Owner";
                    break;
                case "DIRECTORATE":
                    _sortExpressionLabel = "Directorate";
                    break;
                case "DEPTNAME":
                    _sortExpressionLabel = "Department";
                    break;
                case "IS_INFORMATION_ONLY":
                    _sortExpressionLabel = "Information only";
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
                SortInformationLabel.Text = "Sorted by " + _sortExpressionLabel +
                            " in " + _sortDirectionLabel + " order.";
            }


        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

     
    }

}