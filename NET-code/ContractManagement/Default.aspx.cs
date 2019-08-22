//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  Default.aspx.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is the home page of Contract Management System.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Collections;
using log4net;
using System.Configuration;



namespace ContractManagement
{
    public partial class _Default : BasePage
    {

        # region "Variables declaration"

        Data.CMS_DMLUtil objDml = new Data.CMS_DMLUtil();
        Business.CMS_Common_Util objCommon = new Business.CMS_Common_Util();
        protected static readonly ILog Log = LogManager.GetLogger(typeof(_Default));


        //Accelerator
        public string NewDeliA = "0";
        public string InProgDeliA = "0";
        public string SubmitDeliA = "0";
        public string ApprovedDeliA = "0";
        public string ReopenDeliA = "0";
        public string OverdueDeliA = "0";
        public string ApprDefDeliA = "0";

        //Director's Office
        public string NewDeliD = "0";
        public string InProgDeliD = "0";
        public string SubmitDeliD = "0";
        public string ApprovedDeliD = "0";
        public string ReopenDeliD = "0";
        public string OverdueDeliD = "0";
        public string ApprDefDeliD = "0";

        //I&S
        public string NewDeliF = "0";
        public string InProgDeliF = "0";
        public string SubmitDeliF = "0";
        public string ApprovedDeliF = "0";
        public string ReopenDeliF = "0";
        public string OverdueDeliF = "0";
        public string ApprDefDeliF = "0";

        //LCLS
        public string NewDeliL = "0";
        public string InProgDeliL = "0";
        public string SubmitDeliL = "0";
        public string ApprovedDeliL = "0";
        public string ReopenDeliL = "0";
        public string OverdueDeliL = "0";
        public string ApprDefDeliL = "0";

        //Particle Physics & Astro
        public string NewDeliP = "0";
        public string InProgDeliP = "0";
        public string SubmitDeliP = "0";
        public string ApprovedDeliP = "0";
        public string ReopenDeliP= "0";
        public string OverdueDeliP = "0";
        public string ApprDefDeliP = "0";
       
        //Photon Science
        public string NewDeliX = "0";
        public string InProgDeliX = "0";
        public string SubmitDeliX = "0";
        public string ApprovedDeliX = "0";
        public string ReopenDeliX = "0";
        public string OverdueDeliX = "0";
        public string ApprDefDeliX = "0";

        //SSRL
        public string NewDeliS = "0";
        public string InProgDeliS = "0";
        public string SubmitDeliS= "0";
        public string ApprovedDeliS = "0";
        public string ReopenDeliS = "0";
        public string OverdueDeliS = "0";
        public string ApprDefDeliS = "0";

     

        //Owner/Subowner items
        public string NewDeliMy = "0";
        public string InProgDeliMy = "0";
        public string SubmitDeliMy = "0";
        public string ApprovedDeliMy = "0";
        public string ReopenDeliMy = "0";
        public string OverdueDeliMy = "0";
        public string ApprDefDeliMy = "0";

        //Due items
        public string OneDay = "0";
        public string TenDays="0";
        public string ThirtyDays="0";
        public string SixtyDays = "0";
        public string NinetyDays = "0";

        //SSO items
        public string PendSSO = "0";
        public string ApprSSO = "0";
        public string ApprDefSSO = "0";
        public string PendMySSO = "0";

      
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            string _fyDue = "";
            StringBuilder _curFY = new StringBuilder();

            if (!Page.IsPostBack)
            {

                try
                {
                    ChkFY.DataSourceID = "SDSFY";
                    ChkFY.DataTextField = "FYDUE";
                    ChkFY.DataValueField = "FYDUE";
                    ChkFY.DataBind();
                }
                catch(Exception ex)
                {
                    Log.Error(ex);
                    Response.Redirect("Error.aspx");
                }



                // get current FY year and select the current fiscal year
                //Display data based on current fiscal year
                _fyDue = Business.DateTimeExtension.ToFinancialYearShort(DateTime.Today).ToString();
                bool _selected = false;

                foreach (ListItem li in ChkFY.Items)
                {
                    if (li.Text.Equals(_fyDue))
                    {
                        li.Selected = true;
                        _selected = true;
                    }
                }

                if (_selected == false)
                {               
                    if (ChkFY.Items.FindByText("All") != null)
                    {
                        ChkFY.Items.FindByText("All").Selected = true;
                        ChkFY_SelectedIndexChanged(null, null);
                        _fyDue = "All";
                    }
                    
                }
              
                if (_fyDue != "All")
                {
                    _curFY.Append("'");
                    _curFY.Append(_fyDue);
                    _curFY.Append("'");
                    SetBasedOnUser(_curFY.ToString(), "");
                   
                }
                else { SetBasedOnUser(_fyDue, "") ; }              
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //To register postback controls inside an update panel. Controls that are registered
            // by using this method update a whole page instead of updating only the update panel contrl's content

            SMCB.RegisterPostBackControl(ChkFY);
            SMCB.RegisterPostBackControl(LstQtr);

        } 

        protected void SetBasedOnUser(string fyDue, string Quarter)
        {
            //CMA/Superadmin/ALD - Default sees everything
            CheckIfManager();

            if ((_cma) || (_admin) || (_ald) || (_diradmin))
            {
              
                ViewState["user"] = "cma";
            }
            else if (_ssosuper)
            {                
                string _errCode = objDml.AddSSOLog(0, "Viewed Dashboard Page", Convert.ToInt32(objCommon.GetUserID()), objCommon.GetUserID());             
                ViewState["user"] = "ssosuper";
            }
            //SSO gets a filtered view
            else if (_sso)
            {               
                string _errCode = objDml.AddSSOLog(0, "Viewed Dashboard Page", Convert.ToInt32(objCommon.GetUserID()), objCommon.GetUserID());
                ViewState["user"] = "sso";
            }

            //Others incl. Owner/subowner - only their items but same pattern as the above
            else
            {              
                ViewState["user"] = "other";
            }
            FillBasedonUser(fyDue, Quarter);
    
        }

        private void FillBasedonUser(string FyDue, string Quarter)
        {
            string _user = "";
            //Get User Type and based on that fill the dashboard
            _user = GetUserType();
            if (_user == "cma")
            {
                DivGeneral.Visible = true;
                DivOwners.Visible = true;
                DivSSO.Visible = false;
                FillCount(UserType.CMA, FyDue, Quarter);
                FillCount(UserType.OTHER, FyDue, Quarter);
            }
            else if (_user == "ssosuper")
            {
                DivGeneral.Visible = true;
                DivOwners.Visible = false;
                DivSSO.Visible = true;
                FillCount(UserType.SSO, FyDue, Quarter);
                FillCount(UserType.CMA, FyDue, Quarter);

            }
            else if (_user.Equals("sso"))
            {
                DivGeneral.Visible = false;
                DivOwners.Visible = false;
                DivSSO.Visible = true;
                FillCount(UserType.SSO, FyDue, Quarter); 
            }
            else
            {
                DivGeneral.Visible = false;
                DivOwners.Visible = true;
                DivSSO.Visible = false;
                FillCount(UserType.OTHER, FyDue, Quarter);
            }
            HideApprovedbyDefault(IsNotSSO());
        }

        protected void HideApprovedbyDefault(bool boolval)
        {
            THAD.Visible = boolval;
            TDA.Visible = boolval;
            TDD.Visible = boolval;
            TDIS.Visible = boolval;
            TDL.Visible = boolval;
            TDP.Visible = boolval;
            TDPS.Visible = boolval;
            TDS.Visible = boolval;

        }

        private string GetUserType()
        {
            string _user = "";

            if (ViewState["user"] != null)
            {
                _user = ViewState["user"].ToString();
            }
            else
            {
                _user = "other"; //give the least permission if it is null
            }
            return _user;

        }
            
        private string BindEachCell(Directorates drt, int statId, DataTable _dtDel,  string FYDue, string Quarter, int duedays = 0,string flagSSO="N")
        {

            DataView _dvGen = new DataView(_dtDel);
            string _drt = drt.ToString();
            string _count = "";
            //string _rowfilter = "";
            StringBuilder _rowfilter = new StringBuilder();

          
            if (drt.Equals(Directorates.Accelerator) || drt.Equals(Directorates.Director) || drt.Equals(Directorates.Infrastructure) || drt.Equals(Directorates.LCLS) 
                    || drt.Equals(Directorates.Particle) || drt.Equals(Directorates.Photon) || drt.Equals(Directorates.SSRL) || drt.Equals(Directorates.Other))
            {
                if (statId != 0)
                {
                    if ((!IsNotSSO()) && (statId == 4))
                    {
                        _rowfilter.Append(" STATUS_ID IN (4,6) AND DIRECTORATE LIKE '");
                        _rowfilter.Append(drt );
                        _rowfilter.Append("%'") ;
                    }
                    else 
                    {
                        _rowfilter.Append(" STATUS_ID = ") ;
                        _rowfilter.Append(statId );
                        _rowfilter.Append(" AND DIRECTORATE LIKE '");
                        _rowfilter.Append( drt);
                        _rowfilter.Append("%'");
                    }
                }
                else
                {
                    _rowfilter.Append(" STATUS_ID IN (1,2) AND DAYS < 0 AND DIRECTORATE LIKE '");
                    _rowfilter.Append(drt);
                    _rowfilter.Append("%'");
                }

               
                _rowfilter = IfFYDUENotAll(_rowfilter, FYDue);
                _rowfilter = IfQtrNotAll(_rowfilter, Quarter);
                
                _dvGen.RowFilter = _rowfilter.ToString();
                _count = _dvGen.Count.ToString();
            }
            else if (drt.Equals(Directorates.NA))
            {
                int _startdays = -1;
                //Get the list of deliverable ids where current user is either owner or subowner
                string _duedays = "";
                string _stat = "";
                string _user = "";

                if (statId != 0)
                {
                    if ((statId == (int)Status.InProgress) && (duedays != 0))
                    {
                        // COMMENTS MS: Per User feedback, refer DEV-3144

                        _startdays = ReturnStartDays(duedays);
                        _duedays = " AND DAYS > " + _startdays + " AND DAYS <= " + duedays;
                        _stat = " STATUS_ID IN (1,2,5) ";       //Days due based on status New, Inprogress or Reopened
                    }
                    else if  (statId == (int)Status.Approved)
                    {
                      _stat =  " STATUS_ID IN (4,6) "; 
                    }
                  
                    else { _stat = " STATUS_ID = " + statId; }
                    _rowfilter.Append(_stat);
                    _rowfilter.Append(_duedays);
                }
                else
                {
                    _rowfilter.Append(" STATUS_ID IN (1,2,5) AND DAYS < 0 ") ;
                }
               
                    _rowfilter = IfFYDUENotAll(_rowfilter, FYDue);
                    _rowfilter = IfQtrNotAll(_rowfilter, Quarter);

                _user = GetUserType();

                if (_user != "cma")
                {
                    if (null != ViewState["delids"])
                    {
                        string _deliIds = ViewState["delids"].ToString();

                        _rowfilter.Append(" AND ");
                        _rowfilter.Append(_deliIds);

                    }
                }

                _dvGen.RowFilter = _rowfilter.ToString();
                _count = _dvGen.Count.ToString();
           
            }
            else if (drt.Equals(Directorates.All))
            {
                if (flagSSO == "Y")
                {
                    if (null != ViewState["delidsso"])
                    {
                        string _deliIdsso = ViewState["delidsso"].ToString();

                        _rowfilter.Append(_deliIdsso);
                        _rowfilter.Append(" AND STATUS_ID = 3 ");
                     
                         _rowfilter = IfFYDUENotAll(_rowfilter, FYDue);
                         _rowfilter = IfQtrNotAll(_rowfilter, Quarter);         
                       _dvGen.RowFilter = _rowfilter.ToString();
                        _count = _dvGen.Count.ToString();

                    }
                    else _count = "0";
                }
                else
                {
                    //Based on DEV-4240, For SSO view, Approved by default is combined with Approved status
                    if (statId == 4) {_rowfilter.Append(" STATUS_ID IN (4,6) "); }
                    else { _rowfilter.Append(" STATUS_ID = ");
                        _rowfilter.Append(statId); }

                       _rowfilter = IfFYDUENotAll(_rowfilter, FYDue);
                       _rowfilter = IfQtrNotAll(_rowfilter, Quarter);
                     _dvGen.RowFilter = _rowfilter.ToString();
                    _count = _dvGen.Count.ToString();
                }

            }
            return _count;
        }

        private StringBuilder IfFYDUENotAll(StringBuilder rowFilter, string FYDue)
        {
            if (FYDue != "All")
            {
                rowFilter.Append(" AND FYDUE IN (");
                rowFilter.Append(FYDue);
                rowFilter.Append(")");
            }
            return rowFilter;
        }

        private StringBuilder IfQtrNotAll(StringBuilder rowFilter, string Quarter)
        {
            if (Quarter != "")
            {
                rowFilter.Append(" AND QUARTER IN (");
                rowFilter.Append(Quarter);
                rowFilter.Append(")");
            }
            return rowFilter;
        }

        private void FillCount(UserType userType, string FYDue, string Quarter)
        {
            DataSet _dsDeliCount = new DataSet();
            Directorates _drt ;

            _dsDeliCount = objDml.GetDeliverableCount();
            DataTable _dtDeli = _dsDeliCount.Tables["deli"] as DataTable;

            //COMMENTS MS: Although it says CMA, it is common for Admin,ALD,Diradmin
            if (userType.Equals(UserType.CMA)) 
            {
                _drt = Directorates.Accelerator;
                NewDeliA = BindEachCell(_drt, (int)Status.New, _dtDeli, FYDue, Quarter);
                InProgDeliA = BindEachCell(_drt, (int)Status.InProgress, _dtDeli, FYDue, Quarter);
                SubmitDeliA = BindEachCell(_drt, (int)Status.submitted, _dtDeli, FYDue, Quarter);
                ApprovedDeliA = BindEachCell(_drt, (int)Status.Approved, _dtDeli, FYDue, Quarter);
                ReopenDeliA = BindEachCell(_drt, (int)Status.Reopened, _dtDeli, FYDue, Quarter);
                OverdueDeliA = BindEachCell(_drt, (int)Status.Overdue, _dtDeli, FYDue, Quarter);
                ApprDefDeliA = BindEachCell(_drt, (int)Status.ApprovedbyDefault, _dtDeli, FYDue, Quarter);

                _drt =Directorates.Director;
                NewDeliD = BindEachCell(_drt, (int)Status.New, _dtDeli, FYDue, Quarter);
                InProgDeliD = BindEachCell(_drt, (int)Status.InProgress, _dtDeli, FYDue, Quarter);
                SubmitDeliD = BindEachCell(_drt, (int)Status.submitted, _dtDeli, FYDue, Quarter);
                ApprovedDeliD = BindEachCell(_drt, (int)Status.Approved, _dtDeli, FYDue, Quarter);
                ReopenDeliD = BindEachCell(_drt, (int)Status.Reopened, _dtDeli, FYDue, Quarter);
                OverdueDeliD = BindEachCell(_drt, (int)Status.Overdue, _dtDeli, FYDue, Quarter);
                ApprDefDeliD = BindEachCell(_drt, (int)Status.ApprovedbyDefault, _dtDeli, FYDue, Quarter);

                _drt = Directorates.Infrastructure;
                NewDeliF = BindEachCell(_drt, (int)Status.New, _dtDeli, FYDue, Quarter);
                InProgDeliF = BindEachCell(_drt, (int)Status.InProgress, _dtDeli, FYDue, Quarter);
                SubmitDeliF = BindEachCell(_drt, (int)Status.submitted, _dtDeli, FYDue, Quarter);
                ApprovedDeliF = BindEachCell(_drt, (int)Status.Approved, _dtDeli, FYDue, Quarter);
                ReopenDeliF = BindEachCell(_drt, (int)Status.Reopened, _dtDeli, FYDue, Quarter);
                OverdueDeliF = BindEachCell(_drt, (int)Status.Overdue, _dtDeli, FYDue, Quarter);
                ApprDefDeliF = BindEachCell(_drt, (int)Status.ApprovedbyDefault, _dtDeli, FYDue, Quarter);

                _drt =Directorates.Particle;
                NewDeliP = BindEachCell(_drt, (int)Status.New, _dtDeli, FYDue, Quarter);
                InProgDeliP = BindEachCell(_drt, (int)Status.InProgress, _dtDeli, FYDue, Quarter);
                SubmitDeliP = BindEachCell(_drt, (int)Status.submitted, _dtDeli, FYDue, Quarter);
                ApprovedDeliP = BindEachCell(_drt, (int)Status.Approved, _dtDeli, FYDue, Quarter);
                ReopenDeliP = BindEachCell(_drt, (int)Status.Reopened, _dtDeli, FYDue, Quarter);
                OverdueDeliP = BindEachCell(_drt, (int)Status.Overdue, _dtDeli, FYDue, Quarter);
                ApprDefDeliP = BindEachCell(_drt, (int)Status.ApprovedbyDefault, _dtDeli, FYDue, Quarter);

                _drt = Directorates.LCLS;
                NewDeliL = BindEachCell(_drt, (int)Status.New, _dtDeli, FYDue, Quarter);
                InProgDeliL = BindEachCell(_drt, (int)Status.InProgress, _dtDeli, FYDue, Quarter);
                SubmitDeliL = BindEachCell(_drt, (int)Status.submitted, _dtDeli, FYDue, Quarter);
                ApprovedDeliL = BindEachCell(_drt, (int)Status.Approved, _dtDeli, FYDue, Quarter);
                ReopenDeliL = BindEachCell(_drt, (int)Status.Reopened, _dtDeli, FYDue, Quarter);
                OverdueDeliL = BindEachCell(_drt, (int)Status.Overdue, _dtDeli, FYDue, Quarter);
                ApprDefDeliL = BindEachCell(_drt, (int)Status.ApprovedbyDefault, _dtDeli, FYDue, Quarter);

                _drt = Directorates.Photon;
                NewDeliX = BindEachCell(_drt, (int)Status.New, _dtDeli, FYDue, Quarter);
                InProgDeliX = BindEachCell(_drt, (int)Status.InProgress, _dtDeli, FYDue, Quarter);
                SubmitDeliX = BindEachCell(_drt, (int)Status.submitted, _dtDeli, FYDue, Quarter);
                ApprovedDeliX = BindEachCell(_drt, (int)Status.Approved, _dtDeli, FYDue, Quarter);
                ReopenDeliX = BindEachCell(_drt, (int)Status.Reopened, _dtDeli, FYDue, Quarter);
                OverdueDeliX = BindEachCell(_drt, (int)Status.Overdue, _dtDeli, FYDue, Quarter);
                ApprDefDeliX = BindEachCell(_drt, (int)Status.ApprovedbyDefault, _dtDeli, FYDue, Quarter);


                _drt = Directorates.SSRL;
                NewDeliS = BindEachCell(_drt, (int)Status.New, _dtDeli, FYDue, Quarter);
                InProgDeliS = BindEachCell(_drt, (int)Status.InProgress, _dtDeli, FYDue, Quarter);
                SubmitDeliS = BindEachCell(_drt, (int)Status.submitted, _dtDeli, FYDue, Quarter);
                ApprovedDeliS = BindEachCell(_drt, (int)Status.Approved, _dtDeli, FYDue, Quarter);
                ReopenDeliS = BindEachCell(_drt, (int)Status.Reopened, _dtDeli, FYDue, Quarter);
                OverdueDeliS = BindEachCell(_drt, (int)Status.Overdue, _dtDeli, FYDue, Quarter);
                ApprDefDeliS = BindEachCell(_drt, (int)Status.ApprovedbyDefault, _dtDeli, FYDue, Quarter);

               
            }
            else if (userType.Equals(UserType.OTHER))
            {
                _drt=Directorates.NA;
                //check if user is cma, then 
                string _user = "";

                _user = GetUserType();
                if (_user == "cma")
                {
                    Divdue.Visible = true;
                    DivStatus.Visible = false;
                    FillOwnerDueIn(_drt, _dtDeli, FYDue, Quarter);
                }
                else
                {
                    string _deliIds = "";
                    string _deliList = "";
                    ViewState["delids"] = null;
                    _deliList = GetDeliverablesAsList(userId: objCommon.GetUserID());

                    if (_deliList != "")
                    {

                        _deliIds = " DELIVERABLE_ID IN (" + _deliList + ")";
                        ViewState["delids"] = _deliIds;

                        NewDeliMy = BindEachCell(_drt, (int)Status.New, _dtDeli, FYDue, Quarter);
                        InProgDeliMy = BindEachCell(_drt, (int)Status.InProgress, _dtDeli, FYDue, Quarter);
                        SubmitDeliMy = BindEachCell(_drt, (int)Status.submitted, _dtDeli, FYDue, Quarter);
                        ApprovedDeliMy = BindEachCell(_drt, (int)Status.Approved, _dtDeli, FYDue, Quarter);
                        ReopenDeliMy = BindEachCell(_drt, (int)Status.Reopened, _dtDeli, FYDue, Quarter);
                        OverdueDeliMy = BindEachCell(_drt, (int)Status.Overdue, _dtDeli, FYDue, Quarter);
                        ApprDefDeliMy = BindEachCell(_drt, (int)Status.ApprovedbyDefault, _dtDeli, FYDue, Quarter);
                        Divdue.Visible = true;

                        FillOwnerDueIn(_drt, _dtDeli, FYDue, Quarter);

                    }

                    else
                    {
                        NewDeliMy = InProgDeliMy = SubmitDeliMy = ApprovedDeliMy = ReopenDeliMy = OverdueDeliMy = ApprDefDeliMy = "0";
                        // Lnk1.Visible = Lnk10.Visible = Lnk30.Visible = Lnk60.Visible = Lnk90.Visible = false;
                        Divdue.Visible = false;
                    }
                }

            }
            else if (userType.Equals(UserType.SSO))
            {
                _drt = Directorates.All;

                string _deliIds = "";
                string _deliList = "";
                ViewState["delidsso"] = null;
                _deliList = GetDeliverablesAsList(objCommon.GetUserID(), userType: "appvr");

                if (_deliList != "")
                {                  
                    _deliIds = " DELIVERABLE_ID IN (" + _deliList + ")";
                    ViewState["delidsso"] = _deliIds;
                }
                PendSSO = BindEachCell(_drt, (int)Status.submitted, _dtDeli, FYDue, Quarter);
                ApprSSO = BindEachCell(_drt, (int)Status.Approved, _dtDeli, FYDue, Quarter);
                 
                PendMySSO = BindEachCell(_drt, (int)Status.submitted, _dtDeli, FYDue, Quarter,flagSSO: "Y"); //Flagging it to distinguish it from the Pending all SSO deliverables
            }
         
        }

        private void FillOwnerDueIn(Directorates drt,  DataTable _dtDeli, string FYDue, string Quarter)
        {
            OneDay = BindEachCell(drt, (int)Status.InProgress, _dtDeli, FYDue, Quarter, 1);
            TenDays = BindEachCell(drt, (int)Status.InProgress, _dtDeli, FYDue, Quarter,10);
            ThirtyDays = BindEachCell(drt, (int)Status.InProgress, _dtDeli, FYDue, Quarter, 30);
            SixtyDays = BindEachCell(drt, (int)Status.InProgress, _dtDeli, FYDue,Quarter, 60);
            NinetyDays = BindEachCell(drt, (int)Status.InProgress, _dtDeli, FYDue,Quarter, 90);
        }

        private string ReturnUrl(string drt, string statId, int days, string ssomy, string fylist, string qtr)
        {
           
            StringBuilder _sbURL = new StringBuilder();

            _sbURL.Append("Deliverable_List.aspx?");

            if (drt != "na")
            {
                _sbURL.Append("drt=" + drt);
                _sbURL.Append("&");
            }
            if (statId != ""){           
                _sbURL.Append("stat=" + statId);
                _sbURL.Append("&");
            }
            if (days != 0)
            {
                _sbURL.Append("days=" + days);
                _sbURL.Append("&");
            }

            if (ssomy == "Y")
            {
                _sbURL.Append("ssomy=" + ssomy);
                _sbURL.Append("&");
            }

            if (fylist != "")
            {
                _sbURL.Append("fy=" + fylist);
                _sbURL.Append("&");
            }

            if (qtr != "")
            {
                _sbURL.Append("qtr=" + qtr);
                _sbURL.Append("&");
            }
           _sbURL.Remove(_sbURL.Length -1,1);
         
            return _sbURL.ToString();
        }

        private string GetSelectedQuarter()
        {
            StringBuilder _sbQtr = new StringBuilder();
            foreach(ListItem li in LstQtr.Items)
            {
                if (li.Selected)
                {
                    if (li.Value != "0")
                    {
                         _sbQtr.Append(li.Text);
                         _sbQtr.Append(",");
                    }
                 }

            }
            if (!string.IsNullOrEmpty(_sbQtr.ToString()))
            {
                _sbQtr.Remove(_sbQtr.Length - 1, 1);
                return _sbQtr.ToString();
            }
            else return "";
        
        }
        protected void LinkButton_Command(object sender, CommandEventArgs e)
        {
            string fylist = GetChecklistItems();
            Response.Redirect(ReturnUrl(e.CommandArgument.ToString(), e.CommandName.ToString(), 0, "N", fylist, GetSelectedQuarter()));
        }

        protected void LinkButtonMy_Command(object sender, CommandEventArgs e)
        {
            Session["view"] = "owner";
            string fylist = GetChecklistItems();
            Response.Redirect(ReturnUrl("na", e.CommandName.ToString(), 0, "N", fylist,GetSelectedQuarter()));
        }

        protected void Lnk_Command(object sender, CommandEventArgs e)
        {
            //COMMENTS MS: Per Requirement, Only Owner/SubOwners' dashboard can have items that are due
            string _days;
            _days = e.CommandArgument.ToString();
            Session["view"] = "owner";
            string fylist = GetChecklistItems();
            Response.Redirect(ReturnUrl("na", "", Convert.ToInt32(_days), "N", fylist,GetSelectedQuarter()));
        }

        protected void LnkSSO_Command(object sender, CommandEventArgs e)
        {
            string fylist = GetChecklistItems();
            Response.Redirect(ReturnUrl("na", e.CommandName.ToString(), 0, "N", fylist, GetSelectedQuarter()));
        }

        protected void LnkMySSO_Command(object sender, CommandEventArgs e)
        {
            string fylist = GetChecklistItems();
            Response.Redirect(ReturnUrl("na", e.CommandName.ToString(), 0, "Y", fylist, GetSelectedQuarter()));
        }

      #  region "Checkbox List events/related"
        protected void ChkFY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChkFY.Items.FindByText("All").Selected)
            {
                for (int i = 0; i < ChkFY.Items.Count - 1; i++)
                {
                    ChkFY.Items[i].Selected = true;
                }
              
            }
          
          
        }

       
        protected void ChkFY_DataBound(object sender, EventArgs e)
        {
                ChkFY.Items.Insert(ChkFY.Items.Count, new ListItem("All", "0"));
        }

        protected void BtnRefresh_Click(object sender, EventArgs e)
        {
            string _fylist = "";
            string _quarter = "";
            if (Page.IsValid)
            {
                _fylist = GetChecklistItems();
                if ((_fylist.Contains("All")))
                {
                    trIS.Visible = true;
                }
                else
                {
                    trIS.Visible = false;
                }

                _quarter = GetSelectedQuarter();
                FillBasedonUser(_fylist, _quarter);
            }
        }

        protected string GetChecklistItems()
        {
            StringBuilder _sbfy = new StringBuilder();

            foreach (ListItem li in ChkFY.Items)
            {
                if (li.Selected)
                {
                    if (li.Text != "All")
                    {
                        _sbfy.Append("'");
                        _sbfy.Append(li.Text.Substring(0,4));
                        _sbfy.Append("'");
                        _sbfy.Append(",");
                    }
                    else {
                        _sbfy.Clear();
                        _sbfy.Append("All"); _sbfy.Append(","); }
                }
            }
            _sbfy.Remove(_sbfy.Length - 1, 1);
             return _sbfy.ToString();

        }

        protected void CheckBoxRequired_ServerValidate(object source, ServerValidateEventArgs args)
        {
            int counter = 0;
            for (int i = 0; i < ChkFY.Items.Count; i++)
            {
                if (ChkFY.Items[i].Selected)
                {
                    counter++;
                }
                args.IsValid = (counter == 0) ? false : true;
            }
        }
      # endregion

        protected void LstQtr_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                foreach(ListItem li in LstQtr.Items)
                 {
                     if ((li.Value == "0") && (li.Selected))
                     {
                         li.Selected = false;
                     }
                 }
                 if (LstQtr.GetSelectedIndices().Count() == 0)
                 {
                     LstQtr.SelectedValue = "0";
                 }

        }

    


    }
}
