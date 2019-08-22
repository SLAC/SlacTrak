//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  RequirementList.ascx.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
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
using System.IO;


namespace ContractManagement.User_Controls
{
    public partial class RequirementList : System.Web.UI.UserControl
    {
        #region "Object Instance"
        Data.CMS_DMLUtil objDml = new Data.CMS_DMLUtil();
        Business.CMS_Common_Util objCommon = new Business.CMS_Common_Util();
        #endregion

        # region "Pageevents"
        protected void Page_Load(object sender, EventArgs e)
        {
            TxtReqName.Attributes.Add("onkeydown", "return onKeypress('cmdFind');");
            if (!Page.IsPostBack)
            {
                if (!IsSpecialForReport())
                {
                    Lblhead.Text = Lblhead.Text.Replace("all", "your");
                    LblSearch.Text = LblSearch.Text.Replace("all", "your");
                }
                BindGrid();
            }
        }
        #endregion

        #region "UserFunctions"
        private bool IsSpecial()
        {
            if (ViewState["isspecial"] != null)
            {
                return (bool)ViewState["isspecial"];
            }
            else
            {
                bool _isspecial = false;
                (this.Page as BasePage).CheckIfManager();
                if ((!(this.Page as BasePage)._admin) && (!(this.Page as BasePage)._cma) && (!(this.Page as BasePage)._ald) && (!(this.Page as BasePage)._diradmin))
                    _isspecial = false;
                else _isspecial = true;
                ViewState["isspecial"] = _isspecial;
                return _isspecial;
            }
        }

        private bool IsSpecialForReport()
        {
            if (ViewState["isspecialRep"] != null)
            {
                return (bool)ViewState["isspecialRep"];
            }
            else
            {
                bool _isspecialRep = false;
                (this.Page as BasePage).CheckIfManager();
                if ((!(this.Page as BasePage)._admin) && (!(this.Page as BasePage)._cma) && (!(this.Page as BasePage)._ald) && (!(this.Page as BasePage)._diradmin)
                    && (!(this.Page as BasePage)._sso) && (!(this.Page as BasePage)._ssosuper))
                    _isspecialRep = false;
                else _isspecialRep = true;
                ViewState["isspecialRep"] = _isspecialRep;
                return _isspecialRep;
            }
        }

        private void BindGrid()
        {
            StringBuilder _sbFilter = new StringBuilder();
    
            using (OracleCommand _cmdList = new OracleCommand())
            {
                if (TxtReqName.Text != "")
                {
                    _sbFilter = objCommon.SetSBFilter(_sbFilter);
                    if (!IsSpecialForReport()) _sbFilter.Append(" (");
                    _sbFilter.Append(" LOWER(REQUIREMENT) LIKE :Reqmnt OR LOWER(CONTRACT_NAME) LIKE :Reqmnt OR LOWER(CLAUSE_NAME) LIKE :Reqmnt OR LOWER(CLAUSE_NUMBER) LIKE :Reqmnt");
                    _sbFilter.Append("  OR LOWER(FREQUENCY) LIKE :Reqmnt OR LOWER(SUBCLAUSENUM) LIKE :Reqmnt OR LOWER(SUBCLAUSENAME) LIKE :Reqmnt OR LOWER(NOTES) LIKE :Reqmnt");
                    if (!IsSpecialForReport())
                    {
                        _sbFilter.Append(" ) AND");
                        _sbFilter.Append(" LOWER(OWNERNAME) LIKE :Empname");
                        _cmdList.Parameters.Add(":Empname", OracleType.VarChar).Value = "%" + objCommon.GetEmpname(objCommon.GetUserID()).ToLower() + "%"; ;
                    }
                    else
                    {
                        _sbFilter.Append(" OR LOWER(OWNERNAME) LIKE :Reqmnt ");
                    }
                    _cmdList.Parameters.Add(":Reqmnt", OracleType.VarChar).Value = "%" + Server.HtmlEncode(TxtReqName.Text.ToLower()) + "%";
                }
                else
                {
                    if (!IsSpecialForReport())
                    {
                        _sbFilter = objCommon.SetSBFilter(_sbFilter);
                        _sbFilter.Append(" LOWER(OWNERNAME) LIKE :Empname");
                        _cmdList.Parameters.Add(":Empname", OracleType.VarChar).Value = "%" + objCommon.GetEmpname(objCommon.GetUserID()).ToLower() + "%";
                    }
                }

                FillReqDetails(_sbFilter.ToString(), _cmdList);
            }
        }

        protected void FillReqDetails(string filter, OracleCommand cmdList)
        {
            DataSet _dsReq = new DataSet();
            filter += " ORDER BY REQUIREMENT_ID";

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
                
                LblFooter.Visible = false;
                GvRequirement.DataSource = null;
                GvRequirement.DataBind();
            }

        }
        #endregion

        #region "Controls' events"
        protected void cmdFind_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
        #endregion

        #region "Gridevents"
        protected void GvRequirement_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (objCommon.IsVersion("new"))
            //{
            //    e.Row.Cells[3].Visible = false;
            //}
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowview = (DataRowView)e.Row.DataItem;
                string _reqId = Convert.ToString(rowview["REQUIREMENT_ID"]);
                bool _isspecial = false;
                //if owner or subowner, add criteria to filter the query accordingly
                if (!(this.Page as BasePage).IsSpecialForReport())
                {
                    _isspecial = false;
                }
                else _isspecial = true;

                bool _delpresent = objDml.HasDeliverables(_reqId, _isspecial);
                HyperLink HLKDeli = (HyperLink)e.Row.FindControl("HLKDeli");
                if (_delpresent)
                {
                    HLKDeli.Enabled = true;
                    HLKDeli.Text = "Yes";
                    HLKDeli.NavigateUrl = "~/Report.aspx?page=deli&reqid=" + _reqId;
                }
                else
                {
                    HLKDeli.Enabled = false;
                    HLKDeli.Text = "No";
                }
             
            }
        }

        protected void GvRequirement_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView GvRequirement = (sender as GridView);
            GvRequirement.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        #endregion

        protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
            string fileName = "RequirementList.xls";
            const string m_Http_Attachment = "attachment;filename=";
            const string m_Http_Content = "content-disposition";

            Response.ClearContent();
            Response.AddHeader(m_Http_Content, m_Http_Attachment + fileName);
            Response.ContentType = "application/excel";

            StringWriter m_StringWriter = new StringWriter();
            HtmlTextWriter m_HtmlWriter = new HtmlTextWriter(m_StringWriter);

            GvRequirement.AllowPaging = false;
            GvRequirement.HeaderStyle.Font.Bold = true;
            GvRequirement.AllowSorting = false;

            BindGrid();
            (this.Page as BasePage).RemoveHyperLink(GvRequirement);
            GvRequirement.RenderControl(m_HtmlWriter);

            string m_gridViewText = m_StringWriter.ToString();
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Write(m_gridViewText);
            Response.Flush();
            Response.End();
        }

        
    }
 }

