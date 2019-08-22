//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  ReqFlowdown.ascx.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
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
    public partial class ReqFlowdown : System.Web.UI.UserControl
    {
        Data.CMS_DMLUtil objDml = new Data.CMS_DMLUtil();
        Business.CMS_Common_Util objCommon = new Business.CMS_Common_Util();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            
            StringBuilder _sbFilter = new StringBuilder();

            using (OracleCommand _cmdList = new OracleCommand())
            {
                if (TxtReqName.Text != "")
                {
                    _sbFilter.Append(" AND (");
                    _sbFilter.Append(" LOWER(REQUIREMENT) LIKE :Reqmnt OR LOWER(NOTES) LIKE :Reqmnt   OR LOWER(CLAUSE_NAME) LIKE :Reqmnt OR LOWER(CLAUSE_NUMBER) LIKE :Reqmnt");
                    _sbFilter.Append("  OR LOWER(LOOKUP_DESC) LIKE :Reqmnt ) ");

                    _cmdList.Parameters.Add(":Reqmnt", OracleType.VarChar).Value = "%" + Server.HtmlEncode(TxtReqName.Text.ToLower()) + "%";

                }
                _sbFilter.Append(" ORDER BY ");
                _sbFilter.Append(SortExpression);
                _sbFilter.Append(" ");
                _sbFilter.Append(SortDirection);
                FillReqDetails(_sbFilter.ToString(), _cmdList);
            }
           
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

        protected string SortDirection
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

        protected void FillReqDetails(string filter, OracleCommand cmdList)
        {
            
            DataSet _dsReq = new DataSet();
          
            _dsReq = objDml.GetRequirementsFlowdown(filter, cmdList);
            ViewState["reqin"] = _dsReq.Tables["reqin"];

            if (_dsReq.Tables["reqin"].Rows.Count > 0)
            {
                GVReq.DataSource = _dsReq.Tables["reqin"];
                GVReq.DataBind();                 
            }
            else
            {
                GVReq.DataSource = null;
                GVReq.DataBind();
            }

        }

        protected void cmdFind_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void GVReq_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVReq.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void GVReq_Sorting(object sender, GridViewSortEventArgs e)
        {
            this.SortExpression = e.SortExpression;
            if (SortDirection.Equals("ASC"))
            {
                this.SortDirection = "DESC";
            }
            else
            {
                this.SortDirection = "ASC";
            }
            BindGrid();

        }

        protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
            string fileName = "RequirementList-Flowndown.xls";
            const string m_Http_Attachment = "attachment;filename=";
            const string m_Http_Content = "content-disposition";

            Response.ClearContent();
            Response.AddHeader(m_Http_Content, m_Http_Attachment + fileName);
            Response.ContentType = "application/excel";

            StringWriter m_StringWriter = new StringWriter();
            HtmlTextWriter m_HtmlWriter = new HtmlTextWriter(m_StringWriter);

            GVReq.AllowPaging = false;
            GVReq.HeaderStyle.Font.Bold = true;
            GVReq.AllowSorting = false;

            BindGrid();
            GVReq.RenderControl(m_HtmlWriter);
       
            string m_gridViewText = m_StringWriter.ToString();
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Write(m_gridViewText);
            Response.Flush();
            Response.End();
        }

       
    }
}