//$Header:$
//
// U.S. Department of Energy under contract number DE-AC02-76SF00515
// DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy(DOE) contracted obligations, each contractor is required to manage scientific and technical information(STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).
//  DeliverableRecord.cs
//  Developed by Madhu Swaminathan
//  Copyright (c) 2013 SLAC. All rights reserved.
//
// All objects used by this application

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContractManagement.Business
{
    public class DeliverableRecord
    {
        private int _deliverableId;
        private int _sequenceNo;
        private string _compositeKey;
        private int _typeId;
        private DateTime _dueDate;
        private string _drtId;
        private string _deptId;
        private int _statusId;
        private int _requirementId;
        private string _uploadFileRequired;
        private string _isInfoOnly;
        private string _reasonRejection;
        private string _notifyManager;
        private string _description;
        private string _drtName;
        private string _deptName;
        private string _createdBy;
        private string _status;
        private string _modifiedBy;
        private DateTime _dateSubmitted;
        private DateTime _dateApproved;
        private string _frequency;
        private int _frequencyId;
        
            

        private string _requirement;
        private string _typeName;

        public int DeliverableId
        {
            get { return _deliverableId; }
            set { _deliverableId = value; }
        }

        public int SequenceNo
        {
            get { return _sequenceNo; }
            set { _sequenceNo = value; }
        }

        public string CompositeKey
        {
            get { return _compositeKey; }
            set { _compositeKey = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public int TypeId
        {
            get { return _typeId; }
            set { _typeId = value; }
        }

        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

        public string DrtId
        {
            get { return _drtId; }
            set { _drtId = value; }
        }

        public string DeptId
        {
            get { return _deptId; }
            set { _deptId = value; }
        }

        public int StatusId
        {
            get { return _statusId; }
            set { _statusId = value; }
        }

        public int RequirementId
        {
            get { return _requirementId; }
            set { _requirementId = value; }
        }

        public string UploadFileRequired
        {
            get { return _uploadFileRequired; }
            set { _uploadFileRequired = value; }
        }

        public string IsInformationOnly
        {
            get { return _isInfoOnly; }
            set { _isInfoOnly = value; }
        }

        public string ReasonForRejection
        {
            get { return _reasonRejection; }
            set { _reasonRejection = value; }
        }

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        public string NotifyManager
        {
            get { return _notifyManager; }
            set { _notifyManager = value; }
        }

        public string Requirement
        {
            get { return _requirement; }
            set { _requirement = value; }
        }

        public string DrtName
        {
            get { return _drtName; }
            set { _drtName = value; }
        }

        public string DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }

        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }

        }

        public DateTime DateSubmitted
        {
            get { return _dateSubmitted; }
            set { _dateSubmitted = value; }
        }

        public DateTime DateApproved
        {
            get { return _dateApproved; }
            set { _dateApproved = value; }
        }

        public string Frequency
        {
            get { return _frequency; }
            set { _frequency = value; }
        }

        public int FrequencyId
        {
            get { return _frequencyId; }
            set { _frequencyId = value; }
        }
    }

    public class SubOwners
    {
        private int _subOwnerId;
        private int _deliverableId;
        private int _slacId;
        private string _isOwner;
        private string _name;
        private string _createdBy;


        public int SubOwnerId
        {
            get { return _subOwnerId; }
            set { _subOwnerId = value; }
        }

        public int DeliverableId
        {
            get { return _deliverableId; }
            set { _deliverableId = value; }
        }

        public int SlacId
        {
            get { return _slacId; }
            set { _slacId = value; }
        }

        public string IsOwner
        {
            get { return _isOwner; }
            set { _isOwner = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

    }

    public class DeliverableNotification
    {
        private int _scheduleId;
        private int _deliverableId;
        private int _lookupId;
        private string _lookupDesc;
        private string _createdBy;

        public int ScheduleId
        {
            get { return _scheduleId; }
            set { _scheduleId = value; }
        }

        public int DeliverableId
        {
            get { return _deliverableId; }
            set { _deliverableId = value; }
        }

        public int LookupId
        {
            get { return _lookupId;}
            set { _lookupId = value; }
        }

        public string LookupDesc
        {
            get { return _lookupDesc; }
            set { _lookupDesc = value; }

        }

        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }

        }
    }

    public class DeliverableApprovers
    {
        private int _deliverableappId;
        private int _deliverableId;
        private int _approverId;
        private string _approverName;
        private string _createdBy;
        private int _slacId;
        


        public int DeliverableAppId
        {
            get { return _deliverableappId; }
            set { _deliverableappId = value; }
        }

        public int DeliverableId
        {
            get { return _deliverableId; }
            set { _deliverableId = value; }
        }

        public int ApproverId
        {
            get { return _approverId; }
            set { _approverId = value; }
        }

        public string ApproverName
        {
            get { return _approverName; }
            set { _approverName = value; }
        }

        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public int SlacId
        {
            get { return _slacId; }
            set { _slacId = value; }
        }
    }

    public class ClauseRecord
    {
        private int _clauseId;
        private string _clauseName;
        private string _clauseNum;
        private int _owner;
        private int _contractId;
        private string _contractName;
        private string _ownerName;
        private string _clauseType;
        private int _parentId;
        private string _createdBy;
        private DateTime _createdOn;
        private string _modifiedBy;
        private string _parentClause;
        private int _parentOwner;
        private int _parentContract;
        private string _parentContractName;
        private string _parentClausenum;

        public int ClauseId
        {
            get { return _clauseId; }
            set { _clauseId = value; }
        }

        public string ClauseName
        {
            get { return _clauseName; }
            set { _clauseName = value; }
        }

        public string ClauseNumber
        {
            get { return _clauseNum; }
            set { _clauseNum = value; }
        }

        public int Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public int ContractId
        {
            get { return _contractId; }
            set { _contractId = value; }
        }

        public string Contract
        {
            get { return _contractName; }
            set { _contractName = value; }
        }

        public string OwnerName
        {
            get { return _ownerName; }
            set { _ownerName = value; }
        }

        public string ClauseType
        {
            get { return _clauseType; }
            set { _clauseType = value; }
        }

        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }

        }

        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public DateTime CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }

        }
        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        public string ParentClause
        {
            get { return _parentClause; }
            set { _parentClause = value; }
        }

        public int ParentOwner
        {
            get { return _parentOwner; }
            set { _parentOwner = value; }
        }

        public int ParentContract
        {
            get { return _parentContract; }
            set { _parentContract = value; }
        }

        public string ParentContractName
        {
            get { return _parentContractName; }
            set { _parentContractName = value; }

        }

        public string ParentClauseNum
        {
            get { return _parentClausenum; }
            set { _parentClausenum = value; }
        }

    }

    public class RequirementRecord
    {
        private int _reqId;
        private string _req;
        private string _notes;
        private int _freqId;
        private string _uploadFileReq;
        private DateTime _startDate;
        private DateTime _notifiedDate;
        private string _createdBy;
        private string _isCMNotified;
        private int _clauseId;
        private string _modifiedBy;
        private string _contract;
        private string _frequency;
        private string _clause;
        private string _clausenum;
        private string _owner;
        private string _clausenumonly;
        private string _subclause;
        private string _subclausenum;
        private string _scflownprovision;

        public int ReqId
        {
            get { return _reqId; }
            set { _reqId = value; }
        }

        public string Req
        {
            get { return _req; }
            set { _req = value; }
        }

        public string Notes
        {
            get { return _notes; }
            set { _notes = value; }
        }

        public int FrequencyId
        {
            get { return _freqId; }
            set { _freqId = value; }
        }

        public string UploadFileReq
        {
            get { return _uploadFileReq; }
            set { _uploadFileReq = value; }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }

        public DateTime NotifiedDate
        {
            get { return _notifiedDate; }
            set { _notifiedDate = value; }
        }

        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }

        }

        public string IsCMNotified
        {
            get { return _isCMNotified; }
            set { _isCMNotified = value; }
        }

        public int ClauseId
        {
            get { return _clauseId; }
            set { _clauseId = value; }
        }

        public string Contract
        {
            get { return _contract; }
            set { _contract = value; }
        }

        public string Frequency
        {

            get { return _frequency; }
            set { _frequency = value; }
        }

        public string Clause
        {
            get { return _clause; }
            set { _clause = value; }
        }

        public string ClauseNum
        {
            get { return _clausenum; }
            set { _clausenum = value; }
        }

        public string Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public string ClauseNumber
        {
            get { return _clausenumonly; }
            set { _clausenumonly = value; }
        }

        public string Subclause
        {
            get { return _subclause; }
            set { _subclause = value; }
        }

        public string SubclauseNumber
        {
            get { return _subclausenum; }
            set { _subclausenum = value; }
        }

        public string SubContractorFlownProvision
        {
            get { return _scflownprovision; }
            set { _scflownprovision = value; }
        }

    }

    public class ContractTypes
    {
        private int _contractId;
        private int _parentId;
        private string _contractName;
        private string _createdBy;
        private string _modifiedBy;
        private int _groupId;
        private string _contractType;
        private string _parentContract;
        private string _shortName;

        public int ContractId
        {
            get { return _contractId; }
            set { _contractId = value; }
        }

        public int ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        public string ContractName
        {
            get { return _contractName; }
            set { _contractName = value; }
        }

        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        public int GroupID
        {
            get { return _groupId; }
            set { _groupId = value; }
        }

        public string ContractType
        {
            get { return _contractType; }
            set { _contractType = value; }
        }

        public string ParentContract
        {
            get { return _parentContract; }
            set { _parentContract = value; }
        }

        public string ShortName
        {
            get { return _shortName; }
            set { _shortName = value; }
        }

    }

    public class UserRecord
    {
        private string _drtCode;
        private string _mgrType;
        private int _empId;
        private string _createdBy;
        private string _modifiedBy;
        private string _orgId;
        private string _desc;
        private string _empName;
        private int _mgrId;

        public string DrtCode
        {
            get { return _drtCode; }
            set { _drtCode = value; }
        }

        public string MgrType
        {
            get { return _mgrType; }
            set { _mgrType = value; }
        }

        public int EmpId
        {
            get { return _empId; }
            set { _empId = value; }
        }
        public string CreatedBy
        {
            get { return _createdBy; }
            set { _createdBy = value; }
        }

        public string ModifiedBy
        {
            get { return _modifiedBy; }
            set { _modifiedBy = value; }
        }

        public string OrgId
        {
            get { return _orgId; }
            set { _orgId = value; }
        }

        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        public string EmpName
        {
            get { return _empName; }
            set { _empName = value; }
        }

        public int MgrId
        {
            get { return _mgrId; }
            set { _mgrId = value; }
        }

    }

    public class EmailSetting
    {
        public string SendEmail  { get;   set; }
        public string ModifiedBy { get; set; }
        public string ModifiedName { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}