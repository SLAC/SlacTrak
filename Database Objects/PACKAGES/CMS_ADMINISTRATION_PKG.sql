--------------------------------------------------------
--  DDL for Package CMS_ADMINISTRATION_PKG
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "APPS_ADMIN"."CMS_ADMINISTRATION_PKG" AS

  /*
    Name........: CMS_ADMINISTRATION_PKG
    Description.:  Contains admin operations, including data operation on requirement,clause and contract
    Developed by..: Madhu Swaminathan April, 2013
  */
  /* Clause object */
L_MUT_CLAUSE_ID NUMBER;
L_MUT_CLAUSE_NAME VARCHAR2(400);
L_MUT_CLAUSE_NUMBER VARCHAR2(120);
L_MUT_PARENT_ID NUMBER;
L_MUT_OWNER NUMBER;
L_MUT_CREATED_BY VARCHAR2(50);
L_MUT_CREATED_ON DATE;
L_MUT_CONTRACT_ID NUMBER;
L_MUT_MODIFIED_BY VARCHAR2(50);
L_MUT_MODIFIED_DATE DATE;
/* Requirement Object */
L_MUT_REQUIREMENT_ID NUMBER;
L_MUT_REQUIREMENT VARCHAR2(4000);
L_MUT_NOTES VARCHAR2(4000);
L_MUT_FREQUENCY_ID NUMBER;
L_MUT_UPLOAD_FILE_REQUIRED CHAR(1);
L_MUT_START_DATE DATE;
L_MUT_REQ_CREATED_BY VARCHAR2(50);
L_MUT_REQ_CREATED_ON DATE;
L_MUT_IS_CM_NOTIFIED CHAR(1);
L_MUT_NOTIFIED_DATE DATE;
L_MUT_REQ_CLAUSE_ID NUMBER;
L_MUT_REQ_MODIFIED_BY VARCHAR2(50);
L_MUT_REQ_MODIFIED_DATE DATE;
L_MUT_REQ_SCFLOWN_PROVISION CHAR(1);
/*Contract object*/
L_MUT_CNT_CONTRACT_ID NUMBER;
L_MUT_CNT_PARENT_ID NUMBER;
L_MUT_CONTRACT_NAME VARCHAR2(50);
L_MUT_CNT_CREATED_BY VARCHAR2(50);
L_MUT_CNT_CREATED_ON DATE;
L_MUT_CNT_MODIFIED_BY VARCHAR2(50);
L_MUT_CNT_MODIFIED_DATE DATE;
L_MUT_GROUP_ID NUMBER;
L_MUT_SHORT_NAME VARCHAR2(6);



  PROCEDURE PROC_INS_REQUIREMENT
  (
      PI_REQUIREMENT IN CMS_REQUIREMENT.REQUIREMENT%TYPE,
      PI_NOTES IN CMS_REQUIREMENT.NOTES%TYPE,
      PI_FREQUENCY_ID IN CMS_REQUIREMENT.FREQUENCY_ID%TYPE,
      PI_UPLOAD_FILE_REQUIRED IN CMS_REQUIREMENT.UPLOAD_FILE_REQUIRED%TYPE,
      PI_START_DATE IN CMS_REQUIREMENT.START_DATE%TYPE,
      PI_CREATED_BY IN CMS_REQUIREMENT.CREATED_BY%TYPE,
      Pi_Clause_Id In Cms_Requirement.Clause_Id%Type,
       PI_SCFLOWN_PROVISION IN CMS_REQUIREMENT.SCFLOWN_PROVISION%TYPE,
      PO_REQUIREMENT_ID OUT NUMBER
  );

  PROCEDURE PROC_UPD_REQUIREMENT
  (
      PI_REQUIREMENT_ID IN CMS_REQUIREMENT.REQUIREMENT_ID%TYPE,
      PI_REQUIREMENT IN CMS_REQUIREMENT.REQUIREMENT%TYPE,
      PI_NOTES IN CMS_REQUIREMENT.NOTES%TYPE,
      PI_FREQUENCY_ID IN CMS_REQUIREMENT.FREQUENCY_ID%TYPE,
      PI_UPLOAD_FILE_REQUIRED IN CMS_REQUIREMENT.UPLOAD_FILE_REQUIRED%TYPE,
      PI_START_DATE IN CMS_REQUIREMENT.START_DATE%TYPE,
      Pi_Modified_By In Cms_Requirement.Modified_By%Type,
       PI_SCFLOWN_PROVISION IN CMS_REQUIREMENT.SCFLOWN_PROVISION%TYPE,
      PO_RETURN_CODE OUT NUMBER
  );

  PROCEDURE PROC_INS_CLAUSE
  (
      PI_NAME IN CMS_CLAUSE.CLAUSE_NAME%TYPE,
      PI_CLAUSE_NUMBER IN CMS_CLAUSE.CLAUSE_NUMBER%TYPE,
      PI_PARENT_ID IN CMS_CLAUSE.PARENT_ID%TYPE,
      PI_OWNER IN CMS_CLAUSE.OWNER%TYPE,
      PI_CREATED_BY IN CMS_CLAUSE.CREATED_BY%TYPE,
      PI_CONTRACT_ID IN CMS_CLAUSE.CONTRACT_ID%TYPE,
      PO_CLAUSE_ID OUT NUMBER
  );

  PROCEDURE PROC_UPD_CLAUSE
  (
      PI_CLAUSE_ID IN CMS_CLAUSE.CLAUSE_ID%TYPE,
      PI_NAME IN CMS_CLAUSE.CLAUSE_NAME%TYPE,
      PI_CLAUSE_NUMBER IN CMS_CLAUSE.CLAUSE_NUMBER%TYPE,
      PI_PARENT_ID IN CMS_CLAUSE.PARENT_ID%TYPE,
      PI_OWNER IN CMS_CLAUSE.OWNER%TYPE,
      PI_MODIFIED_BY IN CMS_CLAUSE.MODIFIED_BY%TYPE,
      PI_CONTRACT_ID IN CMS_CLAUSE.CONTRACT_ID%TYPE,
      PO_RETURN_CODE OUT NUMBER
  );

  PROCEDURE PROC_INS_CONTRACT
  (
    PI_CONTRACT_NAME IN CMS_CONTRACT_OTHERTYPES.CONTRACT_NAME%TYPE,
    PI_PARENT_ID IN CMS_CONTRACT_OTHERTYPES.PARENT_ID%TYPE,
    PI_GROUP_ID IN CMS_CONTRACT_OTHERTYPES.GROUP_ID%TYPE,
   PI_SHORT_NAME IN CMS_CONTRACT_OTHERTYPES.SHORT_NAME%TYPE,
    PI_CREATED_BY IN CMS_CONTRACT_OTHERTYPES.CREATED_BY%TYPE,
    PO_CONTRACT_ID OUT NUMBER
  );

  PROCEDURE PROC_UPD_CONTRACT
  (
    PI_CONTRACT_ID IN CMS_CONTRACT_OTHERTYPES.CONTRACT_ID%TYPE,
    PI_CONTRACT_NAME IN CMS_CONTRACT_OTHERTYPES.CONTRACT_NAME%TYPE,
    PI_PARENT_ID IN CMS_CONTRACT_OTHERTYPES.PARENT_ID%TYPE,
    PI_GROUP_ID IN CMS_CONTRACT_OTHERTYPES.GROUP_ID%TYPE,
    PI_SHORT_NAME IN CMS_CONTRACT_OTHERTYPES.SHORT_NAME%TYPE,
    PI_MODIFIED_BY IN CMS_CONTRACT_OTHERTYPES.MODIFIED_BY%TYPE,
    PO_RETURN_CODE OUT NUMBER
  );

  PROCEDURE PROC_INS_USER
  (
    PI_MANAGER_TYPE IN CMS_MANAGER.MANAGER_TYPE%TYPE,
    PI_SLAC_ID IN CMS_MANAGER.SLAC_ID%TYPE,
    PI_CREATED_BY IN CMS_MANAGER.CREATED_BY%TYPE,
    PI_ORG_ID IN CMS_MANAGER.ORG_ID%TYPE,
    PO_MANAGER_ID OUT NUMBER
  );

  PROCEDURE PROC_DEL_USER
  (
    PI_MANAGER_ID IN CMS_MANAGER.MANAGER_ID%TYPE,
    PI_MODIFIED_BY IN CMS_MANAGER.MODIFIED_BY%TYPE,
    PO_RETURN_CODE OUT NUMBER
  );

  PROCEDURE PROC_DEL_CLAUSE
  (
    PI_CLAUSE_ID IN CMS_CLAUSE.CLAUSE_ID%TYPE,
    PI_MODIFIED_BY IN CMS_CLAUSE.MODIFIED_BY%TYPE,
    PO_RETURN_CODE OUT NUMBER
  );

  PROCEDURE PROC_DEL_REQUIREMENT
  (
    PI_REQUIREMENT_ID IN CMS_REQUIREMENT.REQUIREMENT_ID%TYPE,
    PI_MODIFIED_BY IN CMS_REQUIREMENT.MODIFIED_BY%TYPE,
    PO_RETURN_CODE OUT NUMBER
  );
  
  Procedure Proc_Toggle_Email
  (
   pi_modified_by in Cms_Emailsettings.MODIFIED_BY%TYPE,
     Po_Return_Code Out Number
  );
  
END CMS_ADMINISTRATION_PKG;

/