//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  CMS_DMLUtil.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
//  This is the class that has database related manipulations.
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using log4net;

namespace ContractManagement.Data
{
    public class CMS_DMLUtil
    {
        CMS_DataUtil objData = new CMS_DataUtil();
        Business.CMS_Common_Util objCommon = new Business.CMS_Common_Util();
        DataSet _listds = new DataSet();
        protected static readonly ILog Log = LogManager.GetLogger(typeof(CMS_DMLUtil));

        # region "reader"
        public OracleDataReader GetMatchingEmployees(string Owner)
        {

            string _sqlEmployee;

            Regex reCheck = new Regex("^[0-9]*$");

            using (OracleCommand _cmdEmp = new OracleCommand())
            {
                if (reCheck.IsMatch(Owner))
                {
                    //Employee id query
                    _sqlEmployee = @"SELECT PC.EMPLOYEE_NAME, PC.EMPLOYEE_ID ,PC.EPO,PC.ORG_LEVEL_1_DESC,ORG.DESCRIPTION FROM DW_PEOPLE_CURRENT PC
                                        LEFT JOIN SID.ORGANIZATIONS ORG  ON ORG.ORG_ID = PC.ORG_LEVEL_1_CODE WHERE EMPLOYEE_ID=:Owner";
                    _cmdEmp.Parameters.Add(":Owner", OracleType.VarChar).Value = Owner;
                }
                else
                {
                    //Employee name query
                    Owner = Owner.ToLower();
                    _sqlEmployee = @"SELECT PC.EMPLOYEE_NAME, PC.EMPLOYEE_ID ,PC.EPO,PC.ORG_LEVEL_1_DESC,ORG.DESCRIPTION FROM DW_PEOPLE_CURRENT PC
                                        LEFT JOIN SID.ORGANIZATIONS ORG  ON ORG.ORG_ID = PC.ORG_LEVEL_1_CODE WHERE LOWER(EMPLOYEE_NAME) LIKE :Owner ORDER BY EMPLOYEE_NAME";
                    _cmdEmp.Parameters.Add(":Owner", OracleType.VarChar).Value = Owner + "%";
                }
                OracleDataReader _drEmployees = objData.GetReader(_sqlEmployee, _cmdEmp);

                return _drEmployees;

            }
        }

        public OracleDataReader GetClauseInfo(string requirementId)
        {
            string _sqlClause;
            using (OracleCommand _ocmd = new OracleCommand())
            {
                _sqlClause = "SELECT * FROM VW_CMS_REQUIREMENT_DETAILS WHERE REQUIREMENT_ID = :RequirementId";
                _ocmd.Parameters.Add(":RequirementId", OracleType.VarChar).Value = requirementId;

                OracleDataReader _drClause = objData.GetReader(_sqlClause, _ocmd);
                return _drClause;
            }

        }

        public OracleDataReader GetRequirement(string typeId)
        {
            string _sqlReq;
            using (OracleCommand _cmdReq = new OracleCommand())
            {
                _sqlReq = "SELECT REQUIREMENT_ID, REQUIREMENT_ID  || ' - ' || SUBSTR(CLAUSE_NUMBER || ' - ' || SUBSTR(REQUIREMENT,1,55),1,60) || '...' AS REQUIREMENT FROM VW_CMS_REQUIREMENT_DETAILS WHERE CONTRACT_ID = :TypeId ORDER BY REQUIREMENT_ID";
                _cmdReq.Parameters.Add(":TypeId", OracleType.VarChar).Value = typeId;

                OracleDataReader _drReq = objData.GetReader(_sqlReq, _cmdReq);
                return _drReq;
            }

        }

        public OracleDataReader GetDirectorateDept(string userId)
        {
            OracleDataReader _drDirdept;
            using (OracleCommand _ocmdDirdept = new OracleCommand())
            {
                string _sqlDirdept = @"SELECT ORG.ORG_ID,ORG1.ORG_ID AS DRTID FROM DW_PEOPLE_CURRENT PC, SID.ORGANIZATIONS ORG ,
                                        SID.ORGANIZATIONS ORG1  WHERE PC.EMPLOYEE_ID= :UserId AND ORG.ORG_ID = PC.ORG_LEVEL_1_CODE AND 
                                                                ORG1.DIRECTORATE_CODE = ORG.DIRECTORATE_CODE AND ORG1.ORG_LEVEL=2 AND ORG1.STATUS='A'";
                _ocmdDirdept.Parameters.Add(":UserId", OracleType.VarChar).Value = userId;
                _drDirdept = objData.GetReader(_sqlDirdept, _ocmdDirdept);
                return _drDirdept;
            }
        }


        public OracleDataReader GetFileInfoById(int attachmentId)
        {
            string _sqlFileInfo = "";
            using (OracleCommand _cmdattach = new OracleCommand())
            {
                _sqlFileInfo = "SELECT * FROM CMS_ATTACHMENT WHERE ATTACHMENT_ID = :AttachmentId";
                _cmdattach.Parameters.Add(":AttachmentId", OracleType.Number).Value = attachmentId;
                OracleDataReader _drFileInfo = objData.GetReader(_sqlFileInfo, _cmdattach);
                return _drFileInfo;
            }

        }

        public OracleDataReader GetDeliInfo(int deliverableId)
        {

            string _sqlDeliInfo = "";
            using (OracleCommand _cmdDeli = new OracleCommand())
            {
                _sqlDeliInfo = "SELECT COMPOSITE_KEY FROM CMS_DELIVERABLE WHERE DELIVERABLE_ID = :DeliverableId";
                _cmdDeli.Parameters.Add(":DeliverableId", OracleType.Number).Value = deliverableId;
                OracleDataReader _drDeli = objData.GetReader(_sqlDeliInfo, _cmdDeli);
                return _drDeli;
            }

        }

      
        #endregion

        public string GetOrgId(string description, string orgLevel = "2")
        {
            //OrgLevel = 2 is for directorate, 3 division, 4 for dept based on sid.organizations
            string _sqlOrg = "";
            string _orgId = "";

            _sqlOrg = "SELECT ORG_ID FROM  SID.ORGANIZATIONS WHERE DESCRIPTION LIKE :Description and ORG_LEVEL = :OrgLevel AND STATUS='A'";
            using (OracleCommand _cmdOrg = new OracleCommand())
            {
                _cmdOrg.Parameters.Add(":Description", OracleType.VarChar).Value =  description + "%";
                _cmdOrg.Parameters.Add(":OrgLevel", OracleType.VarChar).Value = orgLevel;
                using (OracleDataReader _drOrg = objData.GetReader(_sqlOrg, _cmdOrg))
                {
                    while (_drOrg.Read())
                    {
                        _orgId = objCommon.FixDBNull(_drOrg[0]);
                    }
                    if (_orgId == "")
                    {
                        _orgId = GetOrgIdInactive(description, orgLevel);
                    }
                    return _orgId;
                }

            }

        }

        public string GetOrgIdInactive(string description, string orgLevel = "2")
        {
            //OrgLevel = 2 is for directorate, 3 division, 4 for dept based on sid.organizations
            string _sqlOrginact = "";
            string _orgId = "";

            _sqlOrginact = "SELECT * FROM  SID.ORGANIZATIONS WHERE DESCRIPTION LIKE :Description AND ORG_LEVEL = :OrgLevel AND DATE_CREATED = (SELECT MAX(DATE_CREATED) FROM SID.ORGANIZATIONS WHERE DESCRIPTION LIKE :Description AND ORG_LEVEL=:OrgLevel)";
            
            using (OracleCommand _cmdOrgInact = new OracleCommand())
            {
                _cmdOrgInact.Parameters.Add(":Description", OracleType.VarChar).Value = description + "%";
                _cmdOrgInact.Parameters.Add(":OrgLevel", OracleType.VarChar).Value = orgLevel;
                using (OracleDataReader _drOrg = objData.GetReader(_sqlOrginact, _cmdOrgInact))
                {
                    while (_drOrg.Read())
                    {
                        _orgId = objCommon.FixDBNull(_drOrg[0]);
                    }
                    return _orgId;
                }

            }

        }



        public int GetGroupID(int TypeId)
        {
            string _sqlType = "";
            int _typeId = 0;

            _sqlType = "SELECT GROUP_ID FROM CMS_CONTRACT_OTHERTYPES WHERE Contract_id=:ContractId";
            using (OracleCommand _cmdType = new OracleCommand())
            {
                _cmdType.Parameters.Add(":ContractId", OracleType.VarChar).Value = TypeId;

                using (OracleDataReader _drType = objData.GetReader(_sqlType, _cmdType))
                {
                    while (_drType.Read())
                    {
                        _typeId = Convert.ToInt32(objCommon.FixDBNull(_drType[0]));
                    }
                    return _typeId;
                }

            }

        }

        public int GetTypeID(string TypeName)
        {
            string _sqlType = "";
            int _typeId = 0;

            _sqlType = "SELECT CONTRACT_ID FROM CMS_CONTRACT_OTHERTYPES WHERE Contract_Name=:ContractName";
            using (OracleCommand _cmdTypeId = new OracleCommand())
            {
                _cmdTypeId.Parameters.Add(":ContractName", OracleType.VarChar).Value = TypeName;

                using (OracleDataReader _drTypeId = objData.GetReader(_sqlType, _cmdTypeId))
                {
                    while (_drTypeId.Read())
                    {
                        _typeId = Convert.ToInt32(objCommon.FixDBNull(_drTypeId[0]));
                    }
                    return _typeId;
                }

            }

        }

  
        #region "dataset"

        public DataSet GetDepartment(string drtId)
        {
            
            DataSet _listnewds = new DataSet();
            string _sqlDept = "";
            using (OracleCommand _cmdDept = new OracleCommand())
            {
   
                    _sqlDept = @"SELECT s1.org_id, s1.Department_code2 DEPARTMENT_CODE, s1.description from sid.organizations s1 join SID.organizations l2 on l2.Directorate_code=s1.directorate_code and l2.org_level = 2 
                          and l2.status = 'A'  where s1.org_level not in (1,2,3) and l2.org_id = :DrtId AND s1.STATUS='A' order by s1.description";
                _cmdDept.Parameters.Add(":DrtId", OracleType.VarChar).Value = drtId;
                _listnewds = objData.ReturnDataset(_sqlDept, "dept", _cmdDept);
                return _listnewds;
            }
        }

        public DataSet GetFileInfo(string objId)
        {

            DataSet _listfileds = new DataSet();
            string _sqlFile = "";
            using (OracleCommand _cmdFile = new OracleCommand())
            {
                _sqlFile = "SELECT ATTACHMENT_ID, DELIVERABLE_ID, FILE_NAME,FILE_DATA,UPLOADED_BY, UPLOADED_ON FROM CMS_ATTACHMENT WHERE IS_ACTIVE ='Y' AND DELIVERABLE_ID =:ObjId";
                _cmdFile.Parameters.Add(":ObjId", objId);
                _listfileds = objData.ReturnDataset(_sqlFile, "file", _cmdFile);
                return _listfileds;
            }
        }

        public DataSet GetDeliverableCount()
        {
            DataSet _dsCount = new DataSet();
            string _sqlCount = "0";
 
            using (OracleCommand _cmdCount = new OracleCommand())
            {
                _sqlCount = "SELECT * FROM VW_CMS_DELIVERABLE_DETAILS";
                _dsCount = objData.ReturnDataset(_sqlCount, "deli");
                 return _dsCount;
            }

        }

        public DataSet GetDeliverableInfo(string filter, OracleCommand cmdList)
        {
            string _sqlDeli = "";
            _sqlDeli = "SELECT * FROM VW_CMS_DELIVERABLE_DETAILS" + filter;
            _listds = objData.ReturnDataset(_sqlDeli, "deli", cmdList);
            return _listds;
        }

        public DataSet GetClauseInfo(string filter, OracleCommand cmdList)
        {
            string _sqlClausereq = "";
            _sqlClausereq = "SELECT CL.*,  (SELECT COUNT(*)  FROM CMS_REQUIREMENT REQ  LEFT JOIN CMS_CLAUSE CL2  ON REQ.CLAUSE_ID = CL2.CLAUSE_ID   WHERE (REQ.CLAUSE_ID   = CL.CLAUSE_ID)   OR CL2.PARENT_ID = CL.CLAUSE_ID   ) REQCOUNT FROM VW_CMS_CLAUSE_DETAILS_2 CL " + filter;
            _listds = objData.ReturnDataset(_sqlClausereq, "clause", cmdList);
            return _listds;
        }

        public DataTable GetRequirementInfo(string reqId)
        {
            DataSet _dsReq = new DataSet();
            string _sqlReq = "";
            _sqlReq = "SELECT * FROM VW_CMS_REQUIREMENT_DETAILS WHERE REQUIREMENT_ID = :ReqId";

            using (OracleCommand _cmdReq = new OracleCommand())
            {
                _cmdReq.Parameters.AddWithValue(":reqId", reqId);
                _dsReq = objData.ReturnDataset(_sqlReq, "req", _cmdReq);
                return _dsReq.Tables["req"];
            }
        }

        public DataSet GetRequirementInfo(string filter, OracleCommand cmdList)
        {
            string _sqlClausereq = "";
            _sqlClausereq = "SELECT * FROM VW_CMS_REQUIREMENT_DETAILS" + filter;
            _listds = objData.ReturnDataset(_sqlClausereq, "reqinfo", cmdList);
            return _listds;
        }

        public DataSet GetRequirementsFlowdown(string filter, OracleCommand cmdfd)
        {
            string _sqlfd = "";
            _sqlfd = @"SELECT R.REQUIREMENT_ID, R.REQUIREMENT, R.NOTES, L.LOOKUP_DESC AS FREQUENCY , R.UPLOAD_FILE_REQUIRED, R.START_DATE,
                        R.IS_ACTIVE, R.IS_CM_NOTIFIED, R.NOTIFIED_DATE, R.CLAUSE_ID, cl.clause_name, cl.clause_number,R.SCFLOWN_PROVISION FROM CMS_REQUIREMENT  R
                        LEFT JOIN CMS_LOOKUP L ON L.LOOKUP_ID = R.FREQUENCY_ID AND L.LOOKUP_GROUP = 'Frequency'
                        left join cms_clause cl on cl.clause_id = r.clause_id
                        where scflown_provision ='Y'" + filter;
            _listds = objData.ReturnDataset(_sqlfd, "reqin", cmdfd);
            return _listds;

        }

        public DataSet GetContractTypeInfo(string filter)
        {
            string _sqlContract = "";

            _sqlContract = "SELECT * FROM VW_CMS_CONTRACT_DETAILS " + filter + " ORDER BY CONTRACT_ID";
            _listds = objData.ReturnDataset(_sqlContract, "contract");
            return _listds;

        }

        public DataSet GetUserInfo(string filter)
        {
            string _sqlUser = "";
            _sqlUser = "SELECT * FROM VW_CMS_MANAGER WHERE IS_ACTIVE='Y'" + filter;
            _listds = objData.ReturnDataset(_sqlUser, "user");
            return _listds;
        }

        public DataSet GetOrgIdNotAdded(string managerType)
        {
            string _sqlOrg = "";

            if (managerType != "")
            {
                _sqlOrg = "SELECT ORG_ID,DESCRIPTION FROM SID.ORGANIZATIONS WHERE STATUS='A' AND ORG_LEVEL = 2   AND ORG_ID NOT IN (SELECT ORG_ID FROM CMS_MANAGER WHERE LOWER(MANAGER_TYPE) = :mgrType AND IS_ACTIVE='Y')";


                using (OracleCommand _cmdOrg = new OracleCommand())
                {
                    _cmdOrg.Parameters.AddWithValue(":mgrType", managerType.ToLower());
                    _listds = objData.ReturnDataset(_sqlOrg, "orgnotin", _cmdOrg);
                }
                return _listds;
            }
            else return null;
        }
        

        public DataSet GetOrg2List()
        {
            string _sqlOrg = "";

           _sqlOrg = "SELECT ORG_ID,DESCRIPTION FROM SID.ORGANIZATIONS WHERE STATUS='A' AND ORG_LEVEL = 2 ";
           
            _listds = objData.ReturnDataset(_sqlOrg, "org2list");
            
            return _listds;
        }

        public DataSet GetApproversDeli(int deliverableId)
        {

            string _sqlDeliApprvs = "";
            using (OracleCommand _cmdDeliApp = new OracleCommand())
            {
                _sqlDeliApprvs = "SELECT EMPLOYEE_NAME FROM VW_CMS_DELIAPPRVR_MAP_DETAILS WHERE DELIVERABLE_ID=:DeliverableId";
                _cmdDeliApp.Parameters.Add(":DeliverableId", OracleType.Number).Value = deliverableId;
                _listds = objData.ReturnDataset(_sqlDeliApprvs, "deliapprvs", _cmdDeliApp);
            }
            return _listds;
        }
        #endregion

        # region "List"

        public List<Business.NotificationSchedule> GetNotifySchedule(string objId, string mode = "new")
        {
            string _sqlNotify = "";

            if (mode == "new")
            {
                _sqlNotify = "SELECT * FROM CMS_FREQ_NOTIFYSCHED_MAP WHERE FREQUENCY_ID = :objId";
            }
            else
            {
                _sqlNotify = "SELECT * FROM CMS_DELI_NOTIFY_MAP WHERE DELIVERABLE_ID = :objId";
            }
            List<Business.NotificationSchedule> notificationList = new List<Business.NotificationSchedule>();
            using (OracleCommand _cmdNotify = new OracleCommand())
            {
                _cmdNotify.CommandType = CommandType.Text;
                _cmdNotify.Parameters.AddWithValue("objId", objId);
                _cmdNotify.CommandText = _sqlNotify;
                using (DataSet _dsNotify = objData.ReturnDataset(_sqlNotify, "schedule", _cmdNotify))
                {

                    if (_dsNotify != null)
                    {
                        Business.NotificationSchedule objNotifySchedule;
                        foreach (DataRow row in _dsNotify.Tables["schedule"].Rows)
                        {
                            objNotifySchedule = new Business.NotificationSchedule();
                            objNotifySchedule.FreqNotifyId = Convert.ToInt32(row["FREQ_NOTIFYSCHED_ID"]);
                            objNotifySchedule.FrequencyId = Convert.ToInt32(row["FREQUENCY_ID"]);
                            objNotifySchedule.NotifyScheduleId = Convert.ToInt32(row["NOTIFICATION_SCHED_ID"]);
                            notificationList.Add(objNotifySchedule);
                            objNotifySchedule = null;
                        }

                    }
                }
            }
            return notificationList;
        }

        public Business.DeliverableRecord GetDeliverabledetails(string deliverableId)
        {
            string _sqlDeli = "";
            Business.DeliverableRecord objDeliverable = new Business.DeliverableRecord();

            _sqlDeli = "SELECT * FROM VW_CMS_DELIVERABLE_DETAILS WHERE DELIVERABLE_ID = :deliverableId";

            using (OracleCommand _cmdDeli = new OracleCommand())
            {
                _cmdDeli.Parameters.Add(":DeliverableId", OracleType.VarChar).Value = deliverableId;

                using (OracleDataReader _drDeli = objData.GetReader(_sqlDeli, _cmdDeli))
                {
                    while (_drDeli.Read())
                    {
                        objDeliverable.DeliverableId = Convert.ToInt32(objCommon.FixDBNull(_drDeli["DELIVERABLE_ID"]));
                        objDeliverable.CompositeKey = objCommon.FixDBNull(_drDeli["COMPOSITE_KEY"]);
                        objDeliverable.TypeId = (_drDeli["TYPE_ID"] != System.DBNull.Value) ? Convert.ToInt32(objCommon.FixDBNull(_drDeli["TYPE_ID"])) : 0;
                        objDeliverable.DueDate = (_drDeli["DUE_DATE"] != System.DBNull.Value) ? Convert.ToDateTime(_drDeli["DUE_DATE"]) : DateTime.MinValue;
                        objDeliverable.TypeName = objCommon.FixDBNull(_drDeli["TYPENAME"]);
                        objDeliverable.DrtId = (_drDeli["DIRECTORATE_ID"] != System.DBNull.Value) ? objCommon.FixDBNull(_drDeli["DIRECTORATE_ID"]) : "";
                        objDeliverable.DeptId = (_drDeli["DEPARTMENT_ID"] != System.DBNull.Value) ? objCommon.FixDBNull(_drDeli["DEPARTMENT_ID"]) : "0";
                        objDeliverable.DrtName = objCommon.FixDBNull(_drDeli["DIRECTORATE"]);
                        objDeliverable.DeptName = objCommon.FixDBNull(_drDeli["DEPTNAME"]);
                        objDeliverable.StatusId = (_drDeli["STATUS_ID"] != System.DBNull.Value) ? Convert.ToInt32(objCommon.FixDBNull(_drDeli["STATUS_ID"])) : 0;
                        objDeliverable.Status = objCommon.FixDBNull(_drDeli["STATUS_DESC"]);
                        objDeliverable.UploadFileRequired = objCommon.FixDBNull(_drDeli["UPLOAD_FILE_REQUIRED"]);
                        objDeliverable.Description = objCommon.FixDBNull(_drDeli["DESCRIPTION"]);
                        objDeliverable.IsInformationOnly = objCommon.FixDBNull(_drDeli["IS_INFORMATION_ONLY"]);
                        objDeliverable.NotifyManager = objCommon.FixDBNull(_drDeli["NOTIFY_MANAGER"]);
                        objDeliverable.ReasonForRejection = objCommon.FixDBNull(_drDeli["REASON_FOR_REJECTION"]);
                        objDeliverable.RequirementId = (_drDeli["REQUIREMENT_ID"] != System.DBNull.Value) ? Convert.ToInt32(objCommon.FixDBNull(_drDeli["REQUIREMENT_ID"])) : 0;
                        objDeliverable.Requirement = objCommon.FixDBNull(_drDeli["REQUIREMENT"]);
                        objDeliverable.DateSubmitted = (_drDeli["DATE_SUBMITTED"] != System.DBNull.Value) ? Convert.ToDateTime(_drDeli["DATE_SUBMITTED"]) : DateTime.MinValue;
                        objDeliverable.DateApproved = (_drDeli["DATE_APPROVED"] != System.DBNull.Value) ? Convert.ToDateTime(_drDeli["DATE_APPROVED"]) : DateTime.MinValue;
                        objDeliverable.Frequency = objCommon.FixDBNull(_drDeli["FREQNAME"]);
                        objDeliverable.FrequencyId = (_drDeli["FREQUENCY_ID"] != System.DBNull.Value)? Convert.ToInt32(objCommon.FixDBNull(_drDeli["FREQUENCY_ID"])) : 0;
                    }
                    return objDeliverable;
                }
            }
        }

        public List<Business.SubOwners> GetSubowners(string deliverableId, bool soonly=false)
        {
            List<Business.SubOwners> subownerList = new List<Business.SubOwners>();
            string _strSO = "SELECT * FROM VW_CMS_SOWNDELI_MAP_DETAILS WHERE DELIVERABLE_ID =:DeliverableId";
            if (soonly)
            {
                _strSO += " AND ISOWNER='N' ";
            }
            using (OracleCommand _cmdSO = new OracleCommand())
            {
                _cmdSO.CommandType = CommandType.Text;
                _cmdSO.Parameters.AddWithValue("DeliverableId", deliverableId);
                _cmdSO.CommandText = _strSO;
                using (DataSet _dsSO = objData.ReturnDataset(_strSO, "so", _cmdSO))
                {
                    if (_dsSO != null)
                    {
                        Business.SubOwners objSubowner;
                        foreach (DataRow row in _dsSO.Tables["so"].Rows)
                        {
                            objSubowner = new Business.SubOwners();
                            objSubowner.DeliverableId = Convert.ToInt32(row["DELIVERABLE_ID"]);
                            objSubowner.SlacId = Convert.ToInt32(row["SLAC_ID"]);
                            objSubowner.IsOwner = row["ISOWNER"].ToString();
                            objSubowner.Name = row["EMPLOYEE_NAME"].ToString();
                            subownerList.Add(objSubowner);
                            objSubowner = null;
                        }

                    }
                    return subownerList;
                }
            }


        }

        public Business.ClauseRecord GetClauseDetails(string clauseId)
        {
            string _sqlClause = "";
            Business.ClauseRecord objClause = new Business.ClauseRecord();

            _sqlClause = "SELECT * FROM VW_CMS_CLAUSE_DETAILS WHERE CLAUSE_ID = :ClauseId";

            using (OracleCommand _cmdClause = new OracleCommand())
            {

                _cmdClause.Parameters.Add(":ClauseId", OracleType.VarChar).Value = clauseId;
                using (OracleDataReader _drClause = objData.GetReader(_sqlClause, _cmdClause))
                {
                    while (_drClause.Read())
                    {
                        objClause.ClauseId = Convert.ToInt32(objCommon.FixDBNull(_drClause["CLAUSE_ID"]));
                        objClause.ClauseName = objCommon.FixDBNull(_drClause["CLAUSE_NAME"]);
                        objClause.ClauseNumber = (_drClause["CLAUSE_NUMBER"] != System.DBNull.Value) ? objCommon.FixDBNull(_drClause["CLAUSE_NUMBER"]) : "";
                        objClause.ContractId = (_drClause["CONTRACT_ID"] != System.DBNull.Value) ? Convert.ToInt32(objCommon.FixDBNull(_drClause["CONTRACT_ID"])) : 0;
                        objClause.Contract = (_drClause["CONTRACT_NAME"] != System.DBNull.Value) ? objCommon.FixDBNull(_drClause["CONTRACT_NAME"]) : "";
                        objClause.Owner = (_drClause["OWNER"] != System.DBNull.Value) ? Convert.ToInt32(objCommon.FixDBNull(_drClause["OWNER"])) : 0;
                        objClause.OwnerName = (_drClause["OWNERNAME"] != System.DBNull.Value) ? objCommon.FixDBNull(_drClause["OWNERNAME"]) : "";
                        objClause.ClauseType = (_drClause["CLAUSETYPE"] != System.DBNull.Value) ? objCommon.FixDBNull(_drClause["CLAUSETYPE"]) : "";
                        objClause.ParentClause = (_drClause["PARENTCLAUSE"] != System.DBNull.Value) ? objCommon.FixDBNull(_drClause["PARENTCLAUSE"]) : "";
                        objClause.ParentId = (_drClause["PARENT_ID"] != System.DBNull.Value) ? Convert.ToInt32(objCommon.FixDBNull(_drClause["PARENT_ID"])) : 0;
                        objClause.ParentOwner = (_drClause["PARENTOWNER"] != System.DBNull.Value) ? Convert.ToInt32(objCommon.FixDBNull(_drClause["PARENTOWNER"])) : 0;
                        objClause.ParentContract = (_drClause["PARENTCONTRACT"] != System.DBNull.Value) ? Convert.ToInt32(objCommon.FixDBNull(_drClause["PARENTCONTRACT"])) : 0;
                        objClause.ParentContractName = (_drClause["PARENTCONTRACTNAME"] != System.DBNull.Value) ? objCommon.FixDBNull(_drClause["PARENTCONTRACTNAME"]) : "";
                        objClause.ParentClauseNum = (_drClause["PARENTCLAUSENUM"] != System.DBNull.Value) ? objCommon.FixDBNull(_drClause["PARENTCLAUSENUM"]) : "";
                    }
                    return objClause;

                }

            }
        }

        public Business.RequirementRecord GetRequirementDetails(string reqId)
        {
            string _sqlReq = "";
            Business.RequirementRecord objReq = new Business.RequirementRecord();

            _sqlReq = "SELECT * FROM VW_CMS_REQUIREMENT_DETAILS WHERE REQUIREMENT_ID  = :ReqId";
            using (OracleCommand _cmdReq = new OracleCommand())
            {
                _cmdReq.Parameters.Add(":ReqId", OracleType.VarChar).Value = reqId;
                using (OracleDataReader _drReq = objData.GetReader(_sqlReq, _cmdReq))
                {
                    while (_drReq.Read())
                    {
                        objReq.ReqId = Convert.ToInt32(objCommon.FixDBNull(_drReq["REQUIREMENT_ID"]));
                        objReq.Req = objCommon.FixDBNull(_drReq["REQUIREMENT"]);
                        objReq.Notes = objCommon.FixDBNull(_drReq["NOTES"]);
                        if (_drReq["FREQUENCY_ID"] != null) 
                        {
                            if (_drReq["FREQUENCY_ID"].ToString() != "")
                            {
                                objReq.FrequencyId = Convert.ToInt32(objCommon.FixDBNull(_drReq["FREQUENCY_ID"]));
                            }
                            else { objReq.FrequencyId = 0; }
                        }
                        else { objReq.FrequencyId = 0; }
                        objReq.UploadFileReq = objCommon.FixDBNull(_drReq["UPLOAD_FILE_REQUIRED"]);
                        objReq.StartDate = (_drReq["START_DATE"] != System.DBNull.Value) ? Convert.ToDateTime(_drReq["START_DATE"]) : DateTime.MinValue;
                        objReq.IsCMNotified = objCommon.FixDBNull(_drReq["IS_CM_NOTIFIED"]);
                        objReq.NotifiedDate = (_drReq["NOTIFIED_DATE"] != System.DBNull.Value) ? Convert.ToDateTime(_drReq["NOTIFIED_DATE"]) : DateTime.MinValue;
                        objReq.Frequency = objCommon.FixDBNull(_drReq["FREQUENCY"]);
                        objReq.ClauseId = Convert.ToInt32(objCommon.FixDBNull(_drReq["CLAUSE_ID"]));
                        objReq.Clause = objCommon.FixDBNull(_drReq["CLAUSE_NAME"]);
                        objReq.Contract = objCommon.FixDBNull(_drReq["CONTRACT_NAME"]);
                        objReq.ClauseNum = objCommon.FixDBNull(_drReq["CLAUSE_NUMBER"]);
                        objReq.Owner = objCommon.FixDBNull(_drReq["OWNERNAME"]);
                        objReq.ClauseNumber = objCommon.FixDBNull(_drReq["CLAUSENUM"]);
                        objReq.Subclause = objCommon.FixDBNull(_drReq["SUBCLAUSENAME"]);
                        objReq.SubclauseNumber = objCommon.FixDBNull(_drReq["SUBCLAUSENUM"]);
                       objReq.SubContractorFlownProvision = objCommon.FixDBNull(_drReq["SCFLOWN_PROVISION"]);  
                    }

                    return objReq;
                }
            }

        }



        public List<Business.DeliverableApprovers> GetApprovers(string deliverableId)
        {
            List<Business.DeliverableApprovers> approverList = new List<Business.DeliverableApprovers>();
            string _strApprovers = "SELECT * FROM VW_CMS_DELIAPPRVR_MAP_DETAILS WHERE DELIVERABLE_ID =:DeliverableId";
            using (OracleCommand _cmdApprovers = new OracleCommand())
            {
                _cmdApprovers.CommandType = CommandType.Text;
                _cmdApprovers.Parameters.AddWithValue("DeliverableId", deliverableId);
                _cmdApprovers.CommandText = _strApprovers;
                using (DataSet _dsApprovers = objData.ReturnDataset(_strApprovers, "approver", _cmdApprovers))
                {
                    if (_dsApprovers != null)
                    {
                        Business.DeliverableApprovers objApprovers;
                        foreach (DataRow row in _dsApprovers.Tables["approver"].Rows)
                        {
                            objApprovers = new Business.DeliverableApprovers();
                            objApprovers.DeliverableId = Convert.ToInt32(row["DELIVERABLE_ID"]);
                            objApprovers.ApproverId = Convert.ToInt32(row["APPROVER_ID"]);
                            objApprovers.SlacId = Convert.ToInt32(row["SLAC_ID"]);
                            objApprovers.ApproverName = row["EMPLOYEE_NAME"].ToString();
                            approverList.Add(objApprovers);
                            objApprovers = null;
                        }
                    }
                    return approverList;
                }
            }


        }

        public List<Business.DeliverableNotification> GetNotificationSchedule(string deliverableId)
        {
            List<Business.DeliverableNotification> notificationList = new List<Business.DeliverableNotification>();
            string _strNotification = "SELECT * FROM VW_CMS_NOTIFY_MAP_DETAILS WHERE DELIVERABLE_ID =:DeliverableId";
            using (OracleCommand _cmdNotification = new OracleCommand())
            {
                _cmdNotification.CommandType = CommandType.Text;
                _cmdNotification.Parameters.AddWithValue("DeliverableId", deliverableId);
                _cmdNotification.CommandText = _strNotification;
                using (DataSet _dsNotification = objData.ReturnDataset(_strNotification, "notify", _cmdNotification))
                {
                    if (_dsNotification != null)
                    {
                        Business.DeliverableNotification objNotification;
                        foreach (DataRow row in _dsNotification.Tables["notify"].Rows)
                        {
                            objNotification = new Business.DeliverableNotification();
                            objNotification.DeliverableId = Convert.ToInt32(row["DELIVERABLE_ID"]);
                            objNotification.LookupId = Convert.ToInt32(row["LOOKUP_ID"]);
                            objNotification.LookupDesc = row["LOOKUP_DESC"].ToString();
                            notificationList.Add(objNotification);
                            objNotification = null;
                        }
                    }
                    return notificationList;
                }
            }
        }

        public List<string> GetDeliverableIdForUser(string userId, string userType = "all")
        {
            string _sqlDelId = "";
            List<string> _listId = new List<string>();

            _sqlDelId = @"SELECT DISTINCT DELIVERABLE_ID FROM VW_CMS_SOWNDELI_MAP_dETAILS WHERE IS_ACTIVE ='Y' AND SLAC_ID=:UserId";

            if (userType == "so")
            {
                _sqlDelId += " AND ISOWNER = 'N'";
            }

            using (OracleCommand _cmdDelId = new OracleCommand())
            {
                _cmdDelId.Parameters.Add(":userId", OracleType.VarChar).Value = userId;

                using (OracleDataReader _drDelId = objData.GetReader(_sqlDelId, _cmdDelId))
                {
                    while (_drDelId.Read())
                    {
                        _listId.Add(objCommon.FixDBNull(_drDelId[0]));
                    }
                }
                return _listId;
            }
        }
        public List<string> GetDeliverableIdForSO(string soname)
        {
            string _sqlDelId = "";
            List<string> _listId = new List<string>();

            _sqlDelId = @"SELECT DISTINCT DELIVERABLE_ID FROM VW_CMS_SOWNDELI_MAP_dETAILS WHERE IS_ACTIVE ='Y' AND LOWER(EMPLOYEE_NAME) LIKE :Subowner AND ISOWNER = 'N'";

         
            using (OracleCommand _cmdDelId = new OracleCommand())
            {
                _cmdDelId.Parameters.Add(":Subowner", OracleType.VarChar).Value = "%" + soname.ToLower() + "%";

                using (OracleDataReader _drDelId = objData.GetReader(_sqlDelId, _cmdDelId))
                {
                    while (_drDelId.Read())
                    {
                        _listId.Add(objCommon.FixDBNull(_drDelId[0]));
                    }
                }
                return _listId;
            }
        }

        public List<string> GetDeliverableIdForApprovers(string userId)
        {
            string _sqlDelId = "";
            List<string> _listId = new List<string>();

            _sqlDelId = @"SELECT DISTINCT DELIVERABLE_ID FROM VW_CMS_DELIAPPRVR_MAP_DETAILS WHERE SLAC_ID=:UserId and IS_ACTIVE='Y'";

            using (OracleCommand _cmdDeli = new OracleCommand())
            {
                _cmdDeli.Parameters.Add(":UserId", OracleType.VarChar).Value = userId;

                using (OracleDataReader _drDel = objData.GetReader(_sqlDelId, _cmdDeli))
                {
                    while (_drDel.Read())
                    {
                        _listId.Add(objCommon.FixDBNull(_drDel[0]));
                    }
                }
                return _listId;


            }
        }

        public List<string> GetDeliverableIdonNotification(string lookupId)
        {
            string _sqlDelId = "";
            List<string> _listId = new List<string>();

            _sqlDelId = @"SELECT DISTINCT DELIVERABLE_ID FROM VW_CMS_NOTIFY_MAP_DETAILS WHERE LOOKUP_ID=:LookupId and IS_ACTIVE='Y'";

            using (OracleCommand _cmdDeli = new OracleCommand())
            {
                _cmdDeli.Parameters.Add(":LookupId", OracleType.VarChar).Value = lookupId;

                using (OracleDataReader _drDel = objData.GetReader(_sqlDelId, _cmdDeli))
                {
                    while (_drDel.Read())
                    {
                        _listId.Add(objCommon.FixDBNull(_drDel[0]));
                    }
                }
                return _listId;


            }
        }

        public Business.EmailSetting GetEmailSetting()
        {
            string _sqlSetting = "";
            _sqlSetting = "SELECT es.SEND_EMAIL, es.MODIFIED_BY, es.MODIFIED_ON, pc.employee_name FROM CMS_EMAILSETTINGS es Left Join DW_PEOPLE PC on es.modified_by = pc.employee_id WHERE es.IS_ACTIVE='Y'";

            Business.EmailSetting ObjSetting = new Business.EmailSetting();
           
            using (OracleDataReader _drSetting = objData.GetReader(_sqlSetting))
            {
                while (_drSetting.Read())
                {
                    ObjSetting.SendEmail = objCommon.FixDBNull(_drSetting["SEND_EMAIL"]);
                    ObjSetting.ModifiedBy = objCommon.FixDBNull(_drSetting["MODIFIED_BY"]);
                    ObjSetting.ModifiedName = objCommon.FixDBNull(_drSetting["EMPLOYEE_NAME"]);
                    ObjSetting.ModifiedOn = (_drSetting["MODIFIED_ON"] != System.DBNull.Value) ? Convert.ToDateTime(_drSetting["MODIFIED_ON"]) : DateTime.MinValue; 
                }
            }
            return ObjSetting;
        }
        #endregion


        # region "New Deliverable"

        public String CreateDeliverable(int requirementId, int typeId, DateTime dueDate, string drtId, string deptId,
            string uploadFileRequired, string description, string isInfoOnly, string notifyMgr,
            string createdBy, int frequencyId)
        {

            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_DELIVERABLE_PKG.PROC_INS_DELIVERABLE";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("pi_REQUIREMENT_ID", OracleType.Number).Value = requirementId;
                    _ocmd.Parameters["pi_REQUIREMENT_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("pi_TYPE_ID", OracleType.Number).Value = typeId;
                    _ocmd.Parameters["pi_TYPE_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_DUE_DATE", OracleType.DateTime).Value = dueDate;
                    _ocmd.Parameters["PI_DUE_DATE"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_DIRECTORATE_ID", OracleType.VarChar).Value = drtId;
                    _ocmd.Parameters["PI_DIRECTORATE_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_DEPARTMENT_ID", OracleType.VarChar).Value = deptId;
                    _ocmd.Parameters["PI_DEPARTMENT_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_UPLOAD_FILE_REQUIRED", OracleType.Char).Value = uploadFileRequired;
                    _ocmd.Parameters["PI_UPLOAD_FILE_REQUIRED"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_DESCRIPTION", OracleType.VarChar).Value = description;
                    _ocmd.Parameters["PI_DESCRIPTION"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_IS_INFORMATION_ONLY", OracleType.Char).Value = isInfoOnly;
                    _ocmd.Parameters["PI_IS_INFORMATION_ONLY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_NOTIFY_MANAGER", OracleType.Char).Value = notifyMgr;
                    _ocmd.Parameters["PI_NOTIFY_MANAGER"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_FREQUENCY_ID", OracleType.Number).Value = frequencyId;
                    _ocmd.Parameters["PI_FREQUENCY_ID"].Direction = ParameterDirection.Input;
 
                    _ocmd.Parameters.Add("pi_CREATED_BY", OracleType.VarChar).Value = createdBy;
                    _ocmd.Parameters["pi_CREATED_BY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PO_DELIVERABLE_ID", OracleType.Number);
                    _ocmd.Parameters["PO_DELIVERABLE_ID"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    int _deliverableId;

                    if (_ocmd.Parameters["PO_DELIVERABLE_ID"].Value != null)
                    {
                        _deliverableId = Convert.ToInt32(_ocmd.Parameters["PO_DELIVERABLE_ID"].Value);

                        if (_deliverableId <= 0)
                        {
                            _deliverableId = 0;
                        }
                    }
                    else
                    {
                        _deliverableId = 0;
                    } // returning 0 is considered failure for Inserts as it expects a number greater than 0

                    return _deliverableId.ToString();
                }
            }
        }

        public string CreateClause(int contractId, string name, string clauseNum, int parentId, int owner, string createdBy)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_ADMINISTRATION_PKG.PROC_INS_CLAUSE";
                    _ocmd.CommandType = CommandType.StoredProcedure;



                    _ocmd.Parameters.Add("PI_NAME", OracleType.VarChar).Value = name;
                    _ocmd.Parameters["PI_NAME"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_CLAUSE_NUMBER", OracleType.VarChar).Value = clauseNum;
                    _ocmd.Parameters["PI_CLAUSE_NUMBER"].Direction = ParameterDirection.Input;

                    if (parentId != 0)
                    {
                        _ocmd.Parameters.Add("PI_PARENT_ID", OracleType.Number).Value = parentId;
                    }
                    else
                    {
                        _ocmd.Parameters.Add("PI_PARENT_ID", OracleType.Number).Value = System.DBNull.Value;
                    }
                    _ocmd.Parameters["PI_PARENT_ID"].Direction = ParameterDirection.Input;

                    if (owner != 0)
                    {
                        _ocmd.Parameters.Add("PI_OWNER", OracleType.Number).Value = owner;
                    }
                    else
                    {
                        _ocmd.Parameters.Add("PI_OWNER", OracleType.Number).Value = System.DBNull.Value;
                    }
                    _ocmd.Parameters["PI_OWNER"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("pi_CREATED_BY", OracleType.VarChar).Value = createdBy;
                    _ocmd.Parameters["pi_CREATED_BY"].Direction = ParameterDirection.Input;

                    if (contractId != 0)
                    {
                        _ocmd.Parameters.Add("PI_CONTRACT_ID", OracleType.Number).Value = contractId;
                    }
                    else
                    {
                        _ocmd.Parameters.Add("PI_CONTRACT_ID", OracleType.Number).Value = System.DBNull.Value;
                    }
                    _ocmd.Parameters["PI_CONTRACT_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PO_CLAUSE_ID", OracleType.Number);
                    _ocmd.Parameters["PO_CLAUSE_ID"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    int _clauseId;

                    if (_ocmd.Parameters["PO_CLAUSE_ID"].Value != null)
                    {
                        _clauseId = Convert.ToInt32(_ocmd.Parameters["PO_CLAUSE_ID"].Value);
                        if (_clauseId <= 0)
                        {
                            _clauseId = 0;
                        }
                    }
                    else
                    {
                        _clauseId = 0;
                    }

                    return _clauseId.ToString();
                }
            }
        }

        public string CreateRequirement(string req, string notes, int freqId, string uploadFileReq, DateTime startDate, string createdBy, int clauseId, string scFlownProvision)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_ADMINISTRATION_PKG.PROC_INS_REQUIREMENT";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("PI_REQUIREMENT", OracleType.VarChar).Value = req;
                    _ocmd.Parameters["PI_REQUIREMENT"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_NOTES", OracleType.VarChar).Value = notes;
                    _ocmd.Parameters["PI_NOTES"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_FREQUENCY_ID", OracleType.Number).Value = freqId;
                    _ocmd.Parameters["PI_FREQUENCY_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_UPLOAD_FILE_REQUIRED", OracleType.VarChar).Value = uploadFileReq;
                    _ocmd.Parameters["PI_UPLOAD_FILE_REQUIRED"].Direction = ParameterDirection.Input;

                    if (startDate == DateTime.MinValue)
                    {
                        _ocmd.Parameters.Add("PI_START_DATE", OracleType.DateTime).Value = System.DBNull.Value;
                    }
                    else
                    {
                        _ocmd.Parameters.Add("PI_START_DATE", OracleType.DateTime).Value = startDate;
                    }
                    _ocmd.Parameters["PI_START_DATE"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("pi_CREATED_BY", OracleType.VarChar).Value = createdBy;
                    _ocmd.Parameters["pi_CREATED_BY"].Direction = ParameterDirection.Input;

                    if (clauseId != 0)
                    {
                        _ocmd.Parameters.Add("PI_CLAUSE_ID", OracleType.Number).Value = clauseId;
                    }
                    else
                    {
                        _ocmd.Parameters.Add("PI_CLAUSE_ID", OracleType.Number).Value = System.DBNull.Value;
                    }
                    _ocmd.Parameters["PI_CLAUSE_ID"].Direction = ParameterDirection.Input;

                    if (scFlownProvision != "")
                    {
                        _ocmd.Parameters.Add("PI_SCFLOWN_PROVISION", OracleType.VarChar).Value = scFlownProvision;
                        _ocmd.Parameters["PI_SCFLOWN_PROVISION"].Direction = ParameterDirection.Input;
                    }
                  

                    _ocmd.Parameters.Add("PO_REQUIREMENT_ID", OracleType.Number);
                    _ocmd.Parameters["PO_REQUIREMENT_ID"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    int _reqId;

                    if (_ocmd.Parameters["PO_REQUIREMENT_ID"].Value != null)
                    {
                        _reqId = Convert.ToInt32(_ocmd.Parameters["PO_REQUIREMENT_ID"].Value);
                        if (_reqId <= 0)
                        {
                            _reqId = 0;
                        }
                    }
                    else
                    {
                        _reqId = 0;
                    }

                    return _reqId.ToString();
                }
            }
        }

        public string CreateContract(string name, int parentId, int groupId, string shortName, string createdBy)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_ADMINISTRATION_PKG.PROC_INS_CONTRACT";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("PI_CONTRACT_NAME", OracleType.VarChar).Value = name;
                    _ocmd.Parameters["PI_CONTRACT_NAME"].Direction = ParameterDirection.Input;

                    if (parentId != 0)
                    {
                        _ocmd.Parameters.Add("PI_PARENT_ID", OracleType.Number).Value = parentId;
                    }
                    else
                    {
                        _ocmd.Parameters.Add("PI_PARENT_ID", OracleType.Number).Value = System.DBNull.Value;
                    }
                    _ocmd.Parameters["PI_PARENT_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_GROUP_ID", OracleType.Number).Value = groupId;
                    _ocmd.Parameters["PI_GROUP_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_SHORT_NAME", OracleType.VarChar).Value = shortName;
                    _ocmd.Parameters["PI_SHORT_NAME"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_CREATED_BY", OracleType.VarChar).Value = createdBy;
                    _ocmd.Parameters["PI_CREATED_BY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PO_CONTRACT_ID", OracleType.Number);
                    _ocmd.Parameters["PO_CONTRACT_ID"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    int _contractId;

                    if (_ocmd.Parameters["PO_CONTRACT_ID"].Value != null)
                    {
                        _contractId = Convert.ToInt32(_ocmd.Parameters["PO_CONTRACT_ID"].Value);
                        if (_contractId <= 0)
                        {
                            _contractId = 0;
                        }
                    }
                    else
                    {
                        _contractId = 0;
                    }

                    return _contractId.ToString();
                }

            }

        }

        public string CreateUser(string mgrType, int slacId, string orgId, string createdBy)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_ADMINISTRATION_PKG.PROC_INS_USER";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("PI_MANAGER_TYPE", OracleType.VarChar).Value = mgrType;
                    _ocmd.Parameters["PI_MANAGER_TYPE"].Direction = ParameterDirection.Input;


                    _ocmd.Parameters.Add("PI_SLAC_ID", OracleType.Number).Value = slacId;
                    _ocmd.Parameters["PI_SLAC_ID"].Direction = ParameterDirection.Input;

                    if (orgId != "0")
                    {
                        _ocmd.Parameters.Add("PI_ORG_ID", OracleType.VarChar).Value = orgId;
                    }
                    else
                    {
                        _ocmd.Parameters.Add("PI_ORG_ID", OracleType.VarChar).Value = System.DBNull.Value;

                    }
                    _ocmd.Parameters["PI_ORG_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_CREATED_BY", OracleType.VarChar).Value = createdBy;
                    _ocmd.Parameters["PI_CREATED_BY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PO_MANAGER_ID", OracleType.Number);
                    _ocmd.Parameters["PO_MANAGER_ID"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    int _mgrId;

                    if (_ocmd.Parameters["PO_MANAGER_ID"].Value != null)
                    {
                        _mgrId = Convert.ToInt32(_ocmd.Parameters["PO_MANAGER_ID"].Value);
                        if (_mgrId <= 0)
                        {
                            _mgrId = 0;
                        }
                    }
                    else
                    {
                        _mgrId = 0;
                    }

                    return _mgrId.ToString();
                }

            }

        }

        public void AddSubOwnerCol(List<Business.SubOwners> objSubownercoll, string _deliverableId, string isNew,string compositeKey, string ownerId, string sendEmail)
        {
            string _solist = "";
                       
            //COMMENTS MS: While adding more SO, Email will be sent only to the newly added Subowner
          
                if (null != HttpContext.Current.Session["solist"])
                {
                    _solist = HttpContext.Current.Session["solist"].ToString();
                }
            
            string _email = "N";
            int _emailId =0;
            try
            {
                foreach (Business.SubOwners objSubowner in objSubownercoll)
                {
                    //check only subowners
                    if (objSubowner.IsOwner == "N")
                    {
                       
                            if (!string.IsNullOrEmpty(_solist))
                            {
                                _email = CheckForNewSubowner(_solist, objSubowner.SlacId.ToString());
                                _emailId = 2;
                            }
                            
                            else
                            {
                                    _email = "Y";
                                    _emailId = 2;
                            }
                        }
                                         
                    else {

                        if (isNew == "N")
                        {
                            _email = "Y"; _emailId = 3;
                        }
                        else { _email = "N"; }
                                             
                    } //If owner, Send an email
                    objSubowner.DeliverableId = Convert.ToInt32(_deliverableId);
                    AddSubOwner(objSubowner.DeliverableId, objSubowner.SlacId, objSubowner.IsOwner, objSubowner.CreatedBy);
                    if ((_email == "Y") && (objSubowner.IsOwner == "Y"))
                    {
                        if (sendEmail == "Y")
                        {
                            SendEmail(objSubowner.DeliverableId, _emailId, objSubowner.SlacId.ToString());
                        }
                    }
                    else if ((_email == "Y") && (objSubowner.IsOwner == "N"))
                    {
                        if (sendEmail == "Y")
                        {
                            SendEmail(objSubowner.DeliverableId, _emailId, objSubowner.SlacId.ToString(), compositeKey, ownerId);
                        }
                    }
                }
            }
            finally { }
        }

        public void AddSubOwner(int deliverableId, int slacId, string isOwner, string createdBy)
        {
            using (OracleConnection _conSO = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _cmdSO = new OracleCommand())
                {
                    _cmdSO.Connection = _conSO;
                    _conSO.Open();
                    _cmdSO.CommandText = "CMS_DELIVERABLE_PKG.PROC_INS_SUBOWNERS";
                    _cmdSO.CommandType = CommandType.StoredProcedure;

                    _cmdSO.Parameters.Add("PI_DELIVERABLE_ID", OracleType.Number).Value = deliverableId;
                    _cmdSO.Parameters["PI_DELIVERABLE_ID"].Direction = ParameterDirection.Input;

                    _cmdSO.Parameters.Add("PI_SLAC_ID", OracleType.Number).Value = slacId;
                    _cmdSO.Parameters["PI_SLAC_ID"].Direction = ParameterDirection.Input;

                    _cmdSO.Parameters.Add("PI_ISOWNER", OracleType.Char).Value = isOwner;
                    _cmdSO.Parameters["PI_ISOWNER"].Direction = ParameterDirection.Input;

                    _cmdSO.Parameters.Add("PI_CREATED_BY", OracleType.VarChar).Value = createdBy;
                    _cmdSO.Parameters["PI_CREATED_BY"].Direction = ParameterDirection.Input;

                    _cmdSO.Parameters.Add("po_RETURN_CODE", OracleType.Number);
                    _cmdSO.Parameters["po_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _cmdSO.ExecuteNonQuery();

                    string _errCode;

                    if (_cmdSO.Parameters["po_RETURN_CODE"].Value != null)
                    {
                        _errCode = _cmdSO.Parameters["po_RETURN_CODE"].Value.ToString();
                    }
                    else
                    {
                        _errCode = "";
                    }
                }
            }

        }

        public void AddApproverCol(List<Business.DeliverableApprovers> objApprovercoll, string _deliverableId)
        {
            try
            {
                foreach (Business.DeliverableApprovers objApprover in objApprovercoll)
                {
                    objApprover.DeliverableId = Convert.ToInt32(_deliverableId);

                    AddApprover(objApprover.DeliverableId, objApprover.ApproverId, objApprover.CreatedBy);
                }

            }
            finally { }
        }

        public void AddApprover(int deliverableId, int approverId, string createdBy)
        {
            using (OracleConnection _conSO = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _cmdSO = new OracleCommand())
                {
                    _cmdSO.Connection = _conSO;
                    _conSO.Open();
                    _cmdSO.CommandText = "CMS_DELIVERABLE_PKG.PROC_INS_APPROVER";
                    _cmdSO.CommandType = CommandType.StoredProcedure;

                    _cmdSO.Parameters.Add("PI_DELIVERABLE_ID", OracleType.Number).Value = deliverableId;
                    _cmdSO.Parameters["PI_DELIVERABLE_ID"].Direction = ParameterDirection.Input;

                    _cmdSO.Parameters.Add("PI_APPROVER_ID", OracleType.Number).Value = approverId;
                    _cmdSO.Parameters["PI_APPROVER_ID"].Direction = ParameterDirection.Input;

                    _cmdSO.Parameters.Add("PI_CREATED_BY", OracleType.VarChar).Value = createdBy;
                    _cmdSO.Parameters["PI_CREATED_BY"].Direction = ParameterDirection.Input;

                    _cmdSO.Parameters.Add("po_RETURN_CODE", OracleType.Number);
                    _cmdSO.Parameters["po_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _cmdSO.ExecuteNonQuery();

                    string _errCode;

                    if (_cmdSO.Parameters["po_RETURN_CODE"].Value != null)
                    {
                        _errCode = _cmdSO.Parameters["po_RETURN_CODE"].Value.ToString();
                    }
                    else
                    {
                        _errCode = "";
                    }
                }
            }

        }

        public void AddNotificationCol(List<Business.DeliverableNotification> objNotificationcoll, string _deliverableId)
        {
            try
            {
                foreach (Business.DeliverableNotification objNotification in objNotificationcoll)
                {
                    objNotification.DeliverableId = Convert.ToInt32(_deliverableId);
                    AddNotification(objNotification.DeliverableId, objNotification.LookupId, objNotification.CreatedBy);
                }
            }
            finally { }
        }

        public void AddNotification(int deliverableId, int lookupId, string createdBy)
        {
            using (OracleConnection _conNotify = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _cmdNotify = new OracleCommand())
                {
                    _cmdNotify.Connection = _conNotify;
                    _conNotify.Open();
                    _cmdNotify.CommandText = "CMS_DELIVERABLE_PKG.PROC_INS_NOTIFICATION_SCHEDULE";
                    _cmdNotify.CommandType = CommandType.StoredProcedure;

                    _cmdNotify.Parameters.Add("PI_DELIVERABLE_ID", OracleType.Number).Value = deliverableId;
                    _cmdNotify.Parameters["PI_DELIVERABLE_ID"].Direction = ParameterDirection.Input;

                    _cmdNotify.Parameters.Add("PI_LOOKUP_ID", OracleType.Number).Value = lookupId;
                    _cmdNotify.Parameters["PI_LOOKUP_ID"].Direction = ParameterDirection.Input;

                    _cmdNotify.Parameters.Add("PI_CREATED_BY", OracleType.VarChar).Value = createdBy;
                    _cmdNotify.Parameters["PI_CREATED_BY"].Direction = ParameterDirection.Input;

                    _cmdNotify.Parameters.Add("po_RETURN_CODE", OracleType.Number);
                    _cmdNotify.Parameters["po_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _cmdNotify.ExecuteNonQuery();

                    string _errCode;

                    if (_cmdNotify.Parameters["po_RETURN_CODE"].Value != null)
                    {
                        _errCode = _cmdNotify.Parameters["po_RETURN_CODE"].Value.ToString();
                    }
                    else
                    {
                        _errCode = "";
                    }
                }
            }

        }

        public string AddSSOLog(int deliverableId, string desc, int slacId, string createdBy)
        {
            using (OracleConnection _conLog = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _cmdLog = new OracleCommand())
                {
                    _cmdLog.Connection = _conLog;
                    _conLog.Open();
                    _cmdLog.CommandText = "CMS_DELIVERABLE_PKG.PROC_INS_SSOLOG";
                    _cmdLog.CommandType = CommandType.StoredProcedure;

                    _cmdLog.Parameters.Add("PI_DESCRIPTION", OracleType.VarChar).Value = desc;
                    _cmdLog.Parameters["PI_DESCRIPTION"].Direction = ParameterDirection.Input;

                    if (deliverableId != 0)
                    {
                        _cmdLog.Parameters.Add("PI_DELIVERABLE_ID", OracleType.Number).Value = deliverableId;
                    }
                    else { _cmdLog.Parameters.Add("PI_DELIVERABLE_ID", OracleType.Number).Value = System.DBNull.Value; }
                    _cmdLog.Parameters["PI_DELIVERABLE_ID"].Direction = ParameterDirection.Input;

                    _cmdLog.Parameters.Add("PI_SLAC_ID", OracleType.Number).Value = slacId;
                    _cmdLog.Parameters["PI_SLAC_ID"].Direction = ParameterDirection.Input;

                    _cmdLog.Parameters.Add("PI_CREATED_BY", OracleType.VarChar).Value = createdBy;
                    _cmdLog.Parameters["PI_CREATED_BY"].Direction = ParameterDirection.Input;

                    _cmdLog.Parameters.Add("po_RETURN_CODE", OracleType.Number);
                    _cmdLog.Parameters["po_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _cmdLog.ExecuteNonQuery();

                    string _errCode;

                    if (_cmdLog.Parameters["po_RETURN_CODE"].Value != null)
                    {
                        _errCode = _cmdLog.Parameters["po_RETURN_CODE"].Value.ToString();

                    }
                    else
                    {
                        _errCode = "";
                    }
                    //Log here if there is an error
                    return _errCode;
                }
            }

        }

        public string AddSOFileAction(int deliverableId, string actionBy, string emailSent, string isDone)
        {
            using (OracleConnection _conSOAction = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _cmdSOAction = new OracleCommand())
                {
                    _cmdSOAction.Connection = _conSOAction;
                    _conSOAction.Open();
                    _cmdSOAction.CommandText = "CMS_DELIVERABLE_PKG.PROC_INS_SOFILEACTION";
                    _cmdSOAction.CommandType = CommandType.StoredProcedure;

                    if (deliverableId != 0)
                    {
                        _cmdSOAction.Parameters.Add("PI_DELIVERABLE_ID", OracleType.Number).Value = deliverableId;
                    }
                    else { _cmdSOAction.Parameters.Add("PI_DELIVERABLE_ID", OracleType.Number).Value = System.DBNull.Value; }
                    _cmdSOAction.Parameters["PI_DELIVERABLE_ID"].Direction = ParameterDirection.Input;

                    _cmdSOAction.Parameters.Add("PI_ACTION_BY", OracleType.VarChar).Value = actionBy;
                    _cmdSOAction.Parameters["PI_ACTION_BY"].Direction = ParameterDirection.Input;

                    _cmdSOAction.Parameters.Add("PI_EMAIL_SENT", OracleType.Char).Value = emailSent;
                    _cmdSOAction.Parameters["PI_EMAIL_SENT"].Direction = ParameterDirection.Input;

                    _cmdSOAction.Parameters.Add("PI_DONE_UPLOAD", OracleType.Char).Value = isDone;
                    _cmdSOAction.Parameters["PI_DONE_UPLOAD"].Direction = ParameterDirection.Input;

                    _cmdSOAction.Parameters.Add("po_RETURN_CODE", OracleType.Number);
                    _cmdSOAction.Parameters["po_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _cmdSOAction.ExecuteNonQuery();

                    string _errCode;

                    if (_cmdSOAction.Parameters["po_RETURN_CODE"].Value != null)
                    {
                        _errCode = _cmdSOAction.Parameters["po_RETURN_CODE"].Value.ToString();

                    }
                    else
                    {
                        _errCode = "";
                    }
                    //Log here if there is an error
                    return _errCode;
                }
            }

        }


        public string InsertFileData(int objId, string fileName, int fileSize, string contentType, Byte[] fileData, string uploadedBy)
        {
            string _attachmentId = "";
            string _sqlInsert = "";

            _sqlInsert = "Insert into CMS_ATTACHMENT(DELIVERABLE_ID,FILE_NAME,FILE_SIZE,FILE_CONTENT_TYPE,FILE_DATA,UPLOADED_BY,UPLOADED_ON,IS_ACTIVE) VALUES" +
                    "(:id,:Filename,:Filesize,:Contenttype,:Filedata,:Uploadedby,:Uploadedon,:Active) Returning ATTACHMENT_ID INTO :Fileid";
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand(_sqlInsert, _ocon))
                {
                    _ocmd.CommandType = CommandType.Text;

                    OracleParameter prmrid = new OracleParameter();
                    prmrid.ParameterName = ":id";
                    prmrid.OracleType = OracleType.VarChar;
                    prmrid.Value = objId;


                    OracleParameter prmfilename = new OracleParameter();

                    prmfilename.ParameterName = ":Filename";
                    prmfilename.OracleType = OracleType.VarChar;
                    prmfilename.Value = fileName;


                    OracleParameter prmfilesize = new OracleParameter();

                    prmfilesize.ParameterName = ":Filesize";
                    prmfilesize.OracleType = OracleType.Number;
                    prmfilesize.Value = fileSize;


                    OracleParameter prmcontent = new OracleParameter();
                    prmcontent.ParameterName = ":Contenttype";
                    prmcontent.OracleType = OracleType.VarChar;
                    prmcontent.Value = contentType;


                    OracleParameter prmfiledata = new OracleParameter();

                    prmfiledata.ParameterName = ":Filedata";
                    prmfiledata.OracleType = OracleType.Blob;
                    prmfiledata.Value = fileData;

                    OracleParameter prmuploadby = new OracleParameter();
                    prmuploadby.ParameterName = ":Uploadedby";
                    prmuploadby.OracleType = OracleType.VarChar;
                    prmuploadby.Value = uploadedBy;

                    OracleParameter prmuploadon = new OracleParameter();
                    prmuploadon.ParameterName = ":Uploadedon";
                    prmuploadon.OracleType = OracleType.DateTime;
                    prmuploadon.Value = DateTime.Now;

                    OracleParameter prmactive = new OracleParameter();
                    prmactive.ParameterName = ":active";
                    prmactive.OracleType = OracleType.Char;
                    prmactive.Value = 'Y';

                    OracleParameter prmfileid = new OracleParameter();
                    prmfileid.ParameterName = ":Fileid";
                    prmfileid.OracleType = OracleType.Number;
                    prmfileid.Direction = ParameterDirection.Output;


                    _ocmd.Parameters.Add(prmrid);
                    _ocmd.Parameters.Add(prmfilename);
                    _ocmd.Parameters.Add(prmfilesize);
                    _ocmd.Parameters.Add(prmcontent);
                    _ocmd.Parameters.Add(prmfiledata);
                    _ocmd.Parameters.Add(prmuploadby);
                    _ocmd.Parameters.Add(prmuploadon);
                    _ocmd.Parameters.Add(prmactive);
                    _ocmd.Parameters.Add(prmfileid);

                    _ocon.Open();
                    _ocmd.ExecuteNonQuery();

                    try
                    {
                        _attachmentId = _ocmd.Parameters[":Fileid"].Value.ToString();

                    }
                    catch
                    {
                        _attachmentId = "-1";
                    }

                    return _attachmentId;

                }


            }


        }

        # endregion

        # region "Emails"
        public string SendEmail(int objectId, int emailId, string soId)
         {
             
              using (OracleConnection _oconEmail = new OracleConnection(objData.GetConnectionString()))
              {
                  using (OracleCommand _ocmdEmail = new OracleCommand())
                  {
                         _ocmdEmail.Connection = _oconEmail;
                         _oconEmail.Open();
                         _ocmdEmail.CommandText = "CMS_EMAIL_NOTIFICATION_PKG.CMS_EMAIL_PROC";
                         _ocmdEmail.CommandType = CommandType.StoredProcedure;

                         _ocmdEmail.Parameters.Add("PI_OBJECT_ID", OracleType.Number).Value = objectId;
                         _ocmdEmail.Parameters["PI_OBJECT_ID"].Direction = ParameterDirection.Input;

                         _ocmdEmail.Parameters.Add("PI_EMAIL_ID", OracleType.Number).Value = emailId;
                         _ocmdEmail.Parameters["PI_EMAIL_ID"].Direction = ParameterDirection.Input;
                 
                          _ocmdEmail.Parameters.Add("PI_SO_ID", OracleType.VarChar).Value = soId;
                         _ocmdEmail.Parameters["PI_SO_ID"].Direction = ParameterDirection.Input;

                         _ocmdEmail.Parameters.Add("PO_RETURN_CODE", OracleType.Number);
                         _ocmdEmail.Parameters["PO_RETURN_CODE"].Direction = ParameterDirection.Output;

                
                        _ocmdEmail.ExecuteNonQuery();

                         string _errCode;

                         if (_ocmdEmail.Parameters["PO_RETURN_CODE"].Value != null)
                         {
                             if (_ocmdEmail.Parameters["PO_RETURN_CODE"].Value.ToString() != "0")
                             {
                                 _errCode = _ocmdEmail.Parameters["PO_RETURN_CODE"].Value.ToString();
                             }
                             else
                             {
                                 _errCode = "0";
                             }
                         }
                         else
                         {
                             _errCode = "";
                         }

                         return _errCode;
                  }
               }
            }

        public string SendEmail(int objectId, int emailId, string soId, string trackId, string ownerId)
        {

            using (OracleConnection _oconEmail = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmdEmail = new OracleCommand())
                {
                    _ocmdEmail.Connection = _oconEmail;
                    _oconEmail.Open();
                    _ocmdEmail.CommandText = "CMS_EMAIL_NOTIFICATION_PKG.CMS_EMAIL_SUBOWNERS_PROC";
                    _ocmdEmail.CommandType = CommandType.StoredProcedure;

                    _ocmdEmail.Parameters.Add("PI_OBJECT_ID", OracleType.Number).Value = objectId;
                    _ocmdEmail.Parameters["PI_OBJECT_ID"].Direction = ParameterDirection.Input;

                    _ocmdEmail.Parameters.Add("PI_EMAIL_ID", OracleType.Number).Value = emailId;
                    _ocmdEmail.Parameters["PI_EMAIL_ID"].Direction = ParameterDirection.Input;

                    _ocmdEmail.Parameters.Add("PI_SO_ID", OracleType.VarChar).Value = soId;
                    _ocmdEmail.Parameters["PI_SO_ID"].Direction = ParameterDirection.Input;

                    _ocmdEmail.Parameters.Add("PI_COMPOSITE_KEY", OracleType.VarChar).Value = trackId;
                    _ocmdEmail.Parameters["PI_COMPOSITE_KEY"].Direction = ParameterDirection.Input;

                    _ocmdEmail.Parameters.Add("PI_OWNERID", OracleType.VarChar).Value = ownerId;
                    _ocmdEmail.Parameters["PI_OWNERID"].Direction = ParameterDirection.Input;


                    _ocmdEmail.Parameters.Add("PO_RETURN_CODE", OracleType.Number);
                    _ocmdEmail.Parameters["PO_RETURN_CODE"].Direction = ParameterDirection.Output;


                   _ocmdEmail.ExecuteNonQuery();

                    string _errCode;

                    if (_ocmdEmail.Parameters["PO_RETURN_CODE"].Value != null)
                    {
                        if (_ocmdEmail.Parameters["PO_RETURN_CODE"].Value.ToString() != "0")
                        {
                            _errCode = _ocmdEmail.Parameters["PO_RETURN_CODE"].Value.ToString();
                        }
                        else
                        {
                            _errCode = "0";
                        }
                    }
                    else
                    {
                        _errCode = "";
                    }

                    return _errCode;
                }
            }
        }
        #endregion

        # region "Update"

        public string UpdateDeliverable(int deliverableId, DateTime dueDate, string drtId, string deptId,
           string uploadFileRequired, string description, string isInfoOnly, string notifyMgr,
           string modifiedBy, string sendEmail, int frequencyId)
        {

            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_DELIVERABLE_PKG.PROC_UPD_DELIVERABLE";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("pi_DELIVERABLE_ID", OracleType.Number).Value = deliverableId;
                    _ocmd.Parameters["pi_DELIVERABLE_ID"].Direction = ParameterDirection.Input;


                    _ocmd.Parameters.Add("PI_DUE_DATE", OracleType.DateTime).Value = dueDate;
                    _ocmd.Parameters["PI_DUE_DATE"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_DIRECTORATE_ID", OracleType.VarChar).Value = drtId;
                    _ocmd.Parameters["PI_DIRECTORATE_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_DEPARTMENT_ID", OracleType.VarChar).Value = deptId;
                    _ocmd.Parameters["PI_DEPARTMENT_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_UPLOAD_FILE_REQUIRED", OracleType.Char).Value = uploadFileRequired;
                    _ocmd.Parameters["PI_UPLOAD_FILE_REQUIRED"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_DESCRIPTION", OracleType.VarChar).Value = description;
                    _ocmd.Parameters["PI_DESCRIPTION"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_IS_INFORMATION_ONLY", OracleType.Char).Value = isInfoOnly;
                    _ocmd.Parameters["PI_IS_INFORMATION_ONLY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_NOTIFY_MANAGER", OracleType.Char).Value = notifyMgr;
                    _ocmd.Parameters["PI_NOTIFY_MANAGER"].Direction = ParameterDirection.Input;

                    //if (objCommon.IsVersion("new"))
                    //{
                        _ocmd.Parameters.Add("PI_FREQUENCY_ID", OracleType.Number).Value = frequencyId;
                        _ocmd.Parameters["PI_FREQUENCY_ID"].Direction = ParameterDirection.Input;
                    //}
                

                    _ocmd.Parameters.Add("pi_MODIFIED_BY", OracleType.VarChar).Value = modifiedBy;
                    _ocmd.Parameters["pi_MODIFIED_BY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("pi_SEND_EMAIL", OracleType.VarChar).Value = sendEmail;
                    _ocmd.Parameters["pi_SEND_EMAIL"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PO_RETURN_CODE", OracleType.Number);
                    _ocmd.Parameters["PO_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    string _result;

                    if (_ocmd.Parameters["PO_RETURN_CODE"].Value != null)
                    {
                        if (_ocmd.Parameters["PO_RETURN_CODE"].Value.ToString() != "0")
                        {
                            _result = _ocmd.Parameters["PO_RETURN_CODE"].Value.ToString();
                        }
                        else
                        {
                            _result = "0";
                        }
                    }
                    else
                    {
                        _result = "";
                    }

                    return _result;
                }
            }
        }

        public string UpdateStatus(int deliverableId, int statusId, string reason, string modifiedBy, string sendEmail)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_DELIVERABLE_PKG.PROC_UPD_STATUS";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("PI_DELIVERABLE_ID", OracleType.Number).Value = deliverableId;
                    _ocmd.Parameters["PI_DELIVERABLE_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_STATUS_ID", OracleType.Number).Value = statusId;
                    _ocmd.Parameters["PI_STATUS_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_REASON_FOR_REJECTION", OracleType.VarChar).Value = reason;
                    _ocmd.Parameters["PI_REASON_FOR_REJECTION"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_MODIFIED_BY", OracleType.VarChar).Value = modifiedBy;
                    _ocmd.Parameters["PI_MODIFIED_BY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_SENDEMAIL", OracleType.VarChar).Value = sendEmail;
                    _ocmd.Parameters["PI_SENDEMAIL"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PO_RETURN_CODE", OracleType.Number);
                    _ocmd.Parameters["PO_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    string _errorCode;
                    if (_ocmd.Parameters["PO_RETURN_CODE"].Value != null)
                    {
                        if (_ocmd.Parameters["PO_RETURN_CODE"].Value.ToString() != "0")
                        {
                            _errorCode = _ocmd.Parameters["PO_RETURN_CODE"].Value.ToString();
                        }
                        else
                        {
                            _errorCode = "0";
                        }
                    }
                    else
                    {
                        _errorCode = "";
                    }

                    return _errorCode;
                }
            }
        }

        public string UpdateClause(int clauseId, string name, string clauseNum, int parentId, int owner, string modifiedBy, int contractId)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_ADMINISTRATION_PKG.PROC_UPD_CLAUSE";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("PI_CLAUSE_ID", OracleType.Number).Value = clauseId;
                    _ocmd.Parameters["PI_CLAUSE_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_NAME", OracleType.VarChar).Value = name;
                    _ocmd.Parameters["PI_NAME"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_CLAUSE_NUMBER", OracleType.VarChar).Value = clauseNum;
                    _ocmd.Parameters["PI_CLAUSE_NUMBER"].Direction = ParameterDirection.Input;

                    if (parentId != 0)
                    {
                        _ocmd.Parameters.Add("PI_PARENT_ID", OracleType.Number).Value = parentId;
                    }
                    else { _ocmd.Parameters.Add("PI_PARENT_ID", OracleType.Number).Value = System.DBNull.Value; }
                    _ocmd.Parameters["PI_PARENT_ID"].Direction = ParameterDirection.Input;

                    if (owner != 0)
                    {
                        _ocmd.Parameters.Add("PI_OWNER", OracleType.Number).Value = owner;
                    }
                    else
                    {
                        _ocmd.Parameters.Add("PI_OWNER", OracleType.Number).Value = System.DBNull.Value;
                    }
                    _ocmd.Parameters["PI_OWNER"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_MODIFIED_BY", OracleType.VarChar).Value = modifiedBy;
                    _ocmd.Parameters["PI_MODIFIED_BY"].Direction = ParameterDirection.Input;
                    if (contractId != 0)
                    {
                        _ocmd.Parameters.Add("PI_CONTRACT_ID", OracleType.Number).Value = contractId;
                    }
                    else
                    {
                        _ocmd.Parameters.Add("PI_CONTRACT_ID", OracleType.Number).Value = System.DBNull.Value;
                    }
                    _ocmd.Parameters["PI_CONTRACT_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PO_RETURN_CODE", OracleType.Number);
                    _ocmd.Parameters["PO_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    string _errorCode;

                    if (_ocmd.Parameters["PO_RETURN_CODE"].Value != null)
                    {
                        if (_ocmd.Parameters["PO_RETURN_CODE"].Value.ToString() != "0")
                        {
                            _errorCode = _ocmd.Parameters["PO_RETURN_CODE"].Value.ToString();
                        }
                        else
                        {
                            _errorCode = "0";
                        }
                    }
                    else
                    {
                        _errorCode = "";
                    }


                    return _errorCode;
                }
            }

        }

        public string UpdateRequirement(int reqId, string req, string notes, int frequencyId, string uploadFileReq, DateTime startDate, string modifiedBy, string scFlownProvision)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_ADMINISTRATION_PKG.PROC_UPD_REQUIREMENT";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("PI_REQUIREMENT_ID", OracleType.Number).Value = reqId;
                    _ocmd.Parameters["PI_REQUIREMENT_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_REQUIREMENT", OracleType.VarChar).Value = req;
                    _ocmd.Parameters["PI_REQUIREMENT"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_NOTES", OracleType.VarChar).Value = notes;
                    _ocmd.Parameters["PI_NOTES"].Direction = ParameterDirection.Input;


                    _ocmd.Parameters.Add("PI_FREQUENCY_ID", OracleType.Number).Value = frequencyId;
                    _ocmd.Parameters["PI_FREQUENCY_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_UPLOAD_FILE_REQUIRED", OracleType.VarChar).Value = uploadFileReq;
                    _ocmd.Parameters["PI_UPLOAD_FILE_REQUIRED"].Direction = ParameterDirection.Input;

                    if (startDate == DateTime.MinValue)
                    {
                        _ocmd.Parameters.Add("PI_START_DATE", OracleType.DateTime).Value = System.DBNull.Value;
                    }
                    else
                    {
                        _ocmd.Parameters.Add("PI_START_DATE", OracleType.DateTime).Value = startDate;
                    }
                    _ocmd.Parameters["PI_START_DATE"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_MODIFIED_BY", OracleType.VarChar).Value = modifiedBy;
                    _ocmd.Parameters["PI_MODIFIED_BY"].Direction = ParameterDirection.Input;

                    if (scFlownProvision != "")
                    { 
                     _ocmd.Parameters.Add("PI_SCFLOWN_PROVISION", OracleType.VarChar).Value = scFlownProvision;
                    _ocmd.Parameters["PI_SCFLOWN_PROVISION"].Direction = ParameterDirection.Input;
                    }
                    _ocmd.Parameters.Add("PO_RETURN_CODE", OracleType.Number);
                    _ocmd.Parameters["PO_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    string _errorCode;

                    if (_ocmd.Parameters["PO_RETURN_CODE"].Value != null)
                    {
                        if (_ocmd.Parameters["PO_RETURN_CODE"].Value.ToString() != "0")
                        {
                            _errorCode = _ocmd.Parameters["PO_RETURN_CODE"].Value.ToString();
                        }
                        else
                        {
                            _errorCode = "0";
                        }
                    }
                    else
                    {
                        _errorCode = "";
                    }


                    return _errorCode;
                }
            }
        }

        public string UpdateEmailSetting()
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_ADMINISTRATION_PKG.PROC_TOGGLE_EMAIL";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("pi_modified_by", OracleType.VarChar).Value = objCommon.GetUserID();
                    _ocmd.Parameters["PI_MODIFIED_BY"].Direction = ParameterDirection.Input;

                     _ocmd.Parameters.Add("PO_RETURN_CODE", OracleType.Number);
                    _ocmd.Parameters["PO_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    string _errorCode;

                    if (_ocmd.Parameters["PO_RETURN_CODE"].Value != null)
                    {
                        if (_ocmd.Parameters["PO_RETURN_CODE"].Value.ToString() != "0")
                        {
                            _errorCode = _ocmd.Parameters["PO_RETURN_CODE"].Value.ToString();
                        }
                        else
                        {
                            _errorCode = "0";
                        }
                    }
                    else
                    {
                        _errorCode = "";
                    }


                    return _errorCode;
                }
                
            }
        }
        #endregion

        #region "Delete ops"

        public string DeleteSubowners(int deliverableId, string isOwner, string modifiedBy)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_DELIVERABLE_PKG.PROC_DEL_SUBOWNERS";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("PI_DELIVERABLE_ID", OracleType.Number).Value = deliverableId;
                    _ocmd.Parameters["PI_DELIVERABLE_ID"].Direction = ParameterDirection.Input;

                    if (isOwner != "")
                    {
                        _ocmd.Parameters.Add("PI_ISOWNER", OracleType.Char).Value = isOwner;
                    }
                    else
                    {
                        _ocmd.Parameters.Add("PI_ISOWNER", OracleType.Char).Value = System.DBNull.Value;
                    }
                    _ocmd.Parameters["PI_ISOWNER"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_MODIFIED_BY", OracleType.VarChar).Value = modifiedBy;
                    _ocmd.Parameters["PI_MODIFIED_BY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("po_RETURN_CODE", OracleType.Number);
                    _ocmd.Parameters["po_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    string _errorCode;

                    if (_ocmd.Parameters["po_RETURN_CODE"].Value != "0")
                    {
                        _errorCode = _ocmd.Parameters["po_RETURN_CODE"].Value.ToString();
                    }
                    else
                    {
                        _errorCode = "0";
                    }

                    return _errorCode;
                }
            }
        }

        public string DeleteNotification(int deliverableId, string modifiedBy)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_DELIVERABLE_PKG.PROC_DEL_NOTIFICATION_SCHEDULE";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("PI_DELIVERABLE_ID", OracleType.Number).Value = deliverableId;
                    _ocmd.Parameters["PI_DELIVERABLE_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_MODIFIED_BY", OracleType.VarChar).Value = modifiedBy;
                    _ocmd.Parameters["PI_MODIFIED_BY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("po_RETURN_CODE", OracleType.Number);
                    _ocmd.Parameters["po_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    string _errorCode;

                    if (_ocmd.Parameters["po_RETURN_CODE"].Value != "0")
                    {
                        _errorCode = _ocmd.Parameters["po_RETURN_CODE"].Value.ToString();
                    }
                    else
                    {
                        _errorCode = "0";
                    }

                    return _errorCode;
                }
            }
        }

        public string DeleteApprover(int deliverableId, string modifiedBy)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_DELIVERABLE_PKG.PROC_DEL_APPROVER";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("PI_DELIVERABLE_ID", OracleType.Number).Value = deliverableId;
                    _ocmd.Parameters["PI_DELIVERABLE_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_MODIFIED_BY", OracleType.VarChar).Value = modifiedBy;
                    _ocmd.Parameters["PI_MODIFIED_BY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("po_RETURN_CODE", OracleType.Number);
                    _ocmd.Parameters["po_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    string _errorCode;

                    if (_ocmd.Parameters["po_RETURN_CODE"].Value != "0")
                    {
                        _errorCode = _ocmd.Parameters["po_RETURN_CODE"].Value.ToString();
                    }
                    else
                    {
                        _errorCode = "0";
                    }

                    return _errorCode;
                }
            }
        }

        public string DeleteAttachment(int attachmentId, string changedBy)
        {
            using (OracleConnection _conAttach = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _cmdAttach = new OracleCommand())
                {
                    _cmdAttach.Connection = _conAttach;
                    _conAttach.Open();
                    _cmdAttach.CommandText = "CMS_FILE_UPLOAD_PKG.PROC_DEL_FILEDATA";
                    _cmdAttach.CommandType = CommandType.StoredProcedure;

                    _cmdAttach.Parameters.Add("PI_ATTACHMENT_ID", OracleType.Number).Value = attachmentId;
                    _cmdAttach.Parameters["PI_ATTACHMENT_ID"].Direction = ParameterDirection.Input;

                    _cmdAttach.Parameters.Add("PI_CHANGED_BY", OracleType.VarChar).Value = changedBy;
                    _cmdAttach.Parameters["PI_CHANGED_BY"].Direction = ParameterDirection.Input;

                    _cmdAttach.Parameters.Add("po_RETURN_CODE", OracleType.Number);
                    _cmdAttach.Parameters["po_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _cmdAttach.ExecuteNonQuery();

                    string _errorCode;

                    if (_cmdAttach.Parameters["po_RETURN_CODE"].Value != "0")
                    {
                        _errorCode = _cmdAttach.Parameters["po_RETURN_CODE"].Value.ToString();
                    }
                    else
                    {
                        _errorCode = "0";
                    }

                    return _errorCode;
                }

            }

        }

        public string DeleteDeliverable(int deliverableId, string modifiedBy)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {

                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_DELIVERABLE_PKG.PROC_DEL_DELIVERABLE";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("PI_DELIVERABLE_ID", OracleType.Number).Value = deliverableId;
                    _ocmd.Parameters["PI_DELIVERABLE_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_MODIFIED_BY", OracleType.VarChar).Value = modifiedBy;
                    _ocmd.Parameters["PI_MODIFIED_BY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("po_RETURN_CODE", OracleType.Number);
                    _ocmd.Parameters["po_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    string _errorCode;

                    if (_ocmd.Parameters["po_RETURN_CODE"].Value.ToString() == "0")
                    {
                        _errorCode = _ocmd.Parameters["po_RETURN_CODE"].Value.ToString();

                    }
                    else
                    {
                        _errorCode = "err";
                    }
                    return _errorCode;
                }
            }

        }
        public string DeleteClause(int clauseId, string modifiedBy)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {

                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_ADMINISTRATION_PKG.PROC_DEL_CLAUSE";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("PI_CLAUSE_ID", OracleType.Number).Value = clauseId ;
                    _ocmd.Parameters["PI_CLAUSE_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_MODIFIED_BY", OracleType.VarChar).Value = modifiedBy;
                    _ocmd.Parameters["PI_MODIFIED_BY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("po_RETURN_CODE", OracleType.Number);
                    _ocmd.Parameters["po_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    string _errorCode;

                    if (_ocmd.Parameters["po_RETURN_CODE"].Value.ToString() == "0")
                    {
                        _errorCode = _ocmd.Parameters["po_RETURN_CODE"].Value.ToString();
                    }
                    else
                    {
                        _errorCode = "err";
                    }
                    return _errorCode;
                }
            }

        }
        public string DeleteRequirement(int reqId, string modifiedBy)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {

                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_ADMINISTRATION_PKG.PROC_DEL_REQUIREMENT";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("PI_REQUIREMENT_ID", OracleType.Number).Value = reqId;
                    _ocmd.Parameters["PI_REQUIREMENT_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_MODIFIED_BY", OracleType.VarChar).Value = modifiedBy;
                    _ocmd.Parameters["PI_MODIFIED_BY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("po_RETURN_CODE", OracleType.Number);
                    _ocmd.Parameters["po_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    string _errorCode;

                    if (_ocmd.Parameters["po_RETURN_CODE"].Value.ToString() == "0")
                    {
                        _errorCode = _ocmd.Parameters["po_RETURN_CODE"].Value.ToString();
                    }
                    else
                    {
                        _errorCode = "err";
                    }
                    return _errorCode;
                }
            }

        }
        public string DeleteUser(int mgrId, string modifiedBy)
        {
            using (OracleConnection _ocon = new OracleConnection(objData.GetConnectionString()))
            {
                using (OracleCommand _ocmd = new OracleCommand())
                {
                    _ocmd.Connection = _ocon;
                    _ocon.Open();
                    _ocmd.CommandText = "CMS_ADMINISTRATION_PKG.PROC_DEL_USER";
                    _ocmd.CommandType = CommandType.StoredProcedure;

                    _ocmd.Parameters.Add("PI_MANAGER_ID", OracleType.Number).Value = mgrId;
                    _ocmd.Parameters["PI_MANAGER_ID"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PI_MODIFIED_BY", OracleType.VarChar).Value = modifiedBy;
                    _ocmd.Parameters["PI_MODIFIED_BY"].Direction = ParameterDirection.Input;

                    _ocmd.Parameters.Add("PO_RETURN_CODE", OracleType.Number);
                    _ocmd.Parameters["PO_RETURN_CODE"].Direction = ParameterDirection.Output;

                    _ocmd.ExecuteNonQuery();

                    string _errorCode;

                    if (_ocmd.Parameters["PO_RETURN_CODE"].Value != "0")
                    {
                        _errorCode = _ocmd.Parameters["PO_RETURN_CODE"].Value.ToString();
                    }
                    else
                    {
                        _errorCode = "0";
                    }

                    return _errorCode;
                }
            }
        }

        # endregion

        # region "Check fns"
        protected string CheckForNewSubowner(string stringList, string soId)
        {
            string[] _list = stringList.Split(',');
            StringBuilder _sbList = new StringBuilder();
            foreach (string _item in _list)
            {
                if (_item.Equals(soId))
                {
                    return "N";
                }
            }

            return "Y";


        }

        public bool IsOwner(string objId, bool checkown)
        {
            StringBuilder _strOwn = new StringBuilder();
            int _count = 0;

            _strOwn.Append("SELECT COUNT(*) FROM CMS_SOWN_DELI_MAP WHERE IS_ACTIVE = 'Y' AND SLAC_ID = :UserId AND DELIVERABLE_ID = :ObjId AND ");

            if (checkown)
                _strOwn.Append("ISOWNER = 'Y'");
            else
                _strOwn.Append("ISOWNER = 'N'");

            using (OracleCommand _cmdOwn = new OracleCommand())
            {
                _cmdOwn.Parameters.Add(":ObjId", OracleType.VarChar).Value = objId;
                _cmdOwn.Parameters.Add(":UserId", OracleType.VarChar).Value = objCommon.GetUserID();

                using (OracleDataReader _drOwn = objData.GetReader(_strOwn.ToString(), _cmdOwn))
                {
                    if (_drOwn.HasRows)
                    {
                        while (_drOwn.Read())
                        {
                            _count = Convert.ToInt32(objCommon.FixDBNull(_drOwn[0]));
                        }
                    }
                }

            }
            if (_count > 0) return true;
            else return false;

        }

        public bool IsSSOApprover(string objId, string userId)
        {
            string _sqlAppr = "";
            int _count = 0;

            _sqlAppr ="SELECT COUNT(*) FROM VW_CMS_DELIAPPRVR_MAP_DETAILS WHERE IS_ACTIVE = 'Y' AND SLAC_ID = :UserId AND DELIVERABLE_ID = :ObjId ";

           
            using (OracleCommand _cmdAppr = new OracleCommand())
            {
                _cmdAppr.Parameters.Add(":ObjId", OracleType.VarChar).Value = objId;
                _cmdAppr.Parameters.Add(":UserId", OracleType.VarChar).Value = objCommon.GetUserID();

                using (OracleDataReader _drAppr = objData.GetReader(_sqlAppr.ToString(), _cmdAppr))
                {
                    if (_drAppr.HasRows)
                    {
                        while (_drAppr.Read())
                        {
                            _count = Convert.ToInt32(objCommon.FixDBNull(_drAppr[0]));
                        }
                    }
                }

            }
            if (_count > 0) return true;
            else return false;

        }

        public bool AllowSSO(string objId)
        {
            int _count = 0;
            string _sqlSSO = "SELECT COUNT(*) FROM CMS_DELIVERABLE WHERE DELIVERABLE_ID = : ObjId AND STATUS_ID IN (3,4,6)";  //Only items that are submitted or Approved
            using (OracleCommand _cmdSSO = new OracleCommand())
            {
                _cmdSSO.Parameters.Add(":ObjId", OracleType.VarChar).Value = objId;

                using (OracleDataReader _drSSO = objData.GetReader(_sqlSSO, _cmdSSO))
                {
                    if (_drSSO.HasRows)
                    {
                        while (_drSSO.Read())
                        {
                            _count = Convert.ToInt32(objCommon.FixDBNull(_drSSO[0]));
                        }
                    }
                    else { _count = 0; }
                }
            }
            if (_count > 0) return true;
            else return false;
        }

        public bool HasDeliverables(string reqId, bool isspecial)
        {
            int _count = 0;
            StringBuilder sbDeli = new StringBuilder();

            sbDeli.Append("SELECT COUNT(*) FROM VW_CMS_DELIVERABLE_DETAILS WHERE REQUIREMENT_ID = :reqId AND IS_ACTIVE='Y'");
             if (!isspecial)
            {
                sbDeli.Append(" AND OWNERID=");
                sbDeli.Append(objCommon.GetUserID());
            }

             string _sqlDeli = sbDeli.ToString();
           


            using (OracleCommand _cmdDeli = new OracleCommand ())
            {
                _cmdDeli.Parameters.Add(":reqId", OracleType.VarChar).Value = reqId;
                using (OracleDataReader  _drDeli = objData.GetReader (_sqlDeli,_cmdDeli))
                {
                    if (_drDeli.HasRows)
                    {
                        while (_drDeli.Read())
                        {
                            _count = Convert.ToInt32(objCommon.FixDBNull(_drDeli[0]));
                        }
                        
                    } else { _count = 0;}
                }
            }

            if (_count > 0) return true;
            else return false;
        }

        public bool CheckValidName(string owner)
        {
            int _owner;

            _owner = objCommon.GetEmpid(owner);
            if (_owner == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckIfDuplicateUser(string userId, string mgrType,string orgId)
        {
            int _count = 0;
            string _sqlUser = "SELECT COUNT(*) FROM CMS_MANAGER WHERE SLAC_ID = :UserId AND MANAGER_TYPE= :MgrType AND IS_ACTIVE='Y'";
            if (orgId != "")
            {
                _sqlUser += "  AND ORG_ID = :OrgId";
            }
            using (OracleCommand _cmdUser = new OracleCommand())
            {
                _cmdUser.Parameters.Add(":UserId", OracleType.VarChar).Value = userId;
                _cmdUser.Parameters.Add(":MgrType", OracleType.VarChar).Value = mgrType;
                if (orgId != "")
                {
                    _cmdUser.Parameters.Add(":OrgId", OracleType.VarChar).Value = orgId;
                }

                using (OracleDataReader _drUser = objData.GetReader(_sqlUser, _cmdUser))
                {
                    if (_drUser.HasRows)
                    {
                        while (_drUser.Read())
                        {
                            _count = Convert.ToInt32(objCommon.FixDBNull(_drUser[0]));
                        }
                    }
                    else { _count = 0; }
                }
            }
            if (_count > 0) return true;
            else return false;

        }

        public bool CheckIfChildExists(string objId, string objType)
        {
            string _viewname = "";
            string _where = "";
            int _count = 0;
            StringBuilder _sqlChild = new StringBuilder();

            switch(objType)
            {
                case "cl": case "sc":
                    _viewname = " VW_CMS_REQUIREMENT_DETAILS ";
                    _where = " CLAUSE_ID = :objId ";
                    break;
                case "cl1":
                    _viewname = " VW_CMS_CLAUSE_DETAILS ";
                    _where = " PARENT_ID = :objId ";
                    break;
                case "req":
                    _viewname = " VW_CMS_DELIVERABLE_DETAILS ";
                    _where = " REQUIREMENT_ID = :objId ";
                    break;                

            }
          

            if ((_viewname != "") && (_where != ""))
            {
                _sqlChild.Append(" SELECT COUNT(*) FROM ");
                _sqlChild.Append(_viewname);
                _sqlChild.Append(" WHERE ");
                _sqlChild.Append(_where);
            }

            using (OracleCommand _cmdChild = new OracleCommand())
            {
                _cmdChild.Parameters.Add(":objId", OracleType.VarChar).Value = objId;
                using (OracleDataReader _drChild = objData.GetReader(_sqlChild.ToString() , _cmdChild))
                {
                    if (_drChild.HasRows)
                    {
                        while (_drChild.Read())
                        {
                            _count = Convert.ToInt32(objCommon.FixDBNull(_drChild[0]));
                        }
                       
                    }
                  }
            }
            if (_count > 0) return true; else return false;          

        }

        public bool CheckIfForSSOAppr(string userId)
        {
            int _count = 0;
            string _sqlcount = "";

            _sqlcount = "SELECT COUNT(*) FROM VW_CMS_DELIVERABLE_DETAILS WHERE DELIVERABLE_ID IN (SELECT DISTINCT DELIVERABLE_ID FROM VW_CMS_DELIAPPRVR_MAP_DETAILS WHERE SLAC_ID= :userId and IS_ACTIVE='Y') AND STATUS_ID=3 AND IS_ACTIVE='Y'";

            using (OracleCommand _cmdCount = new OracleCommand())
            {
                _cmdCount.Parameters.Add(":UserId", OracleType.VarChar).Value = objCommon.GetUserID();

                using (OracleDataReader _drcount = objData.GetReader(_sqlcount.ToString(), _cmdCount))
                {
                    if (_drcount.HasRows)
                    {
                        while (_drcount.Read())
                        {
                            _count = Convert.ToInt32(objCommon.FixDBNull(_drcount[0]));
                        }
                    }
                }
            }

            if (_count > 0) return true;
            else return false;
            
        }

        #endregion    


        }
       
    }

