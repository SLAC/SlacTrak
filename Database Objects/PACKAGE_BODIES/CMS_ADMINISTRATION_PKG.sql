--------------------------------------------------------
--  DDL for Package Body CMS_ADMINISTRATION_PKG
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE BODY "APPS_ADMIN"."CMS_ADMINISTRATION_PKG" AS

  PROCEDURE PROC_INS_REQUIREMENT
  (
      PI_REQUIREMENT IN CMS_REQUIREMENT.REQUIREMENT%TYPE,
      PI_NOTES IN CMS_REQUIREMENT.NOTES%TYPE,
      PI_FREQUENCY_ID IN CMS_REQUIREMENT.FREQUENCY_ID%TYPE,
      PI_UPLOAD_FILE_REQUIRED IN CMS_REQUIREMENT.UPLOAD_FILE_REQUIRED%TYPE,
      PI_START_DATE IN CMS_REQUIREMENT.START_DATE%TYPE,
      PI_CREATED_BY IN CMS_REQUIREMENT.CREATED_BY%TYPE,
      PI_CLAUSE_ID IN CMS_REQUIREMENT.CLAUSE_ID%TYPE,
      PI_SCFLOWN_PROVISION IN CMS_REQUIREMENT.SCFLOWN_PROVISION%TYPE,
      PO_REQUIREMENT_ID OUT NUMBER
  ) AS
  L_REQUIREMENT_ID NUMBER;


  BEGIN

      Insert Into Cms_Requirement(Requirement,Notes,Frequency_Id,Upload_File_Required,Start_Date,Created_By,
        Created_On,Clause_Id,Scflown_Provision) Values
        (PI_REQUIREMENT,PI_NOTES,PI_FREQUENCY_ID,PI_UPLOAD_FILE_REQUIRED,PI_START_DATE,PI_CREATED_BY,SYSDATE,PI_CLAUSE_ID,PI_SCFLOWN_PROVISION)
        RETURNING REQUIREMENT_ID INTO L_REQUIREMENT_ID;

          PO_REQUIREMENT_ID := L_REQUIREMENT_ID;
  END PROC_INS_REQUIREMENT;

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
  ) AS
  BEGIN
      UPDATE CMS_REQUIREMENT SET
          REQUIREMENT = PI_REQUIREMENT,
          NOTES = PI_NOTES,
          FREQUENCY_ID = PI_FREQUENCY_ID,
          UPLOAD_FILE_REQUIRED = PI_UPLOAD_FILE_REQUIRED,
          START_DATE = PI_START_DATE,
          Modified_By = Pi_Modified_By,
          Modified_Date = Sysdate,
          SCFLOWN_PROVISION = PI_SCFLOWN_PROVISION
          WHERE REQUIREMENT_ID = PI_REQUIREMENT_ID;
          PO_RETURN_CODE := 0;
  EXCEPTION
        WHEN OTHERS THEN
          PO_RETURN_CODE := SQLCODE;
  END PROC_UPD_REQUIREMENT;

  PROCEDURE PROC_INS_CLAUSE
  (
      PI_NAME IN CMS_CLAUSE.CLAUSE_NAME%TYPE,
      PI_CLAUSE_NUMBER IN CMS_CLAUSE.CLAUSE_NUMBER%TYPE,
      PI_PARENT_ID IN CMS_CLAUSE.PARENT_ID%TYPE,
      PI_OWNER IN CMS_CLAUSE.OWNER%TYPE,
      PI_CREATED_BY IN CMS_CLAUSE.CREATED_BY%TYPE,
      PI_CONTRACT_ID IN CMS_CLAUSE.CONTRACT_ID%TYPE,
      PO_CLAUSE_ID OUT NUMBER
  ) AS
  L_CLAUSE_ID NUMBER;
  BEGIN
      INSERT INTO CMS_CLAUSE (CLAUSE_NAME,CLAUSE_NUMBER,PARENT_ID,OWNER,CREATED_BY,CREATED_ON,CONTRACT_ID)
        VALUES(PI_NAME,PI_CLAUSE_NUMBER,PI_PARENT_ID,PI_OWNER,PI_CREATED_BY,SYSDATE,PI_CONTRACT_ID)
        RETURNING CLAUSE_ID INTO L_CLAUSE_ID;

        PO_CLAUSE_ID := L_CLAUSE_ID;
  END PROC_INS_CLAUSE;

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
  ) AS
  BEGIN
    UPDATE CMS_CLAUSE SET
        CLAUSE_NAME = PI_NAME,
        CLAUSE_NUMBER = PI_CLAUSE_NUMBER,
        PARENT_ID = PI_PARENT_ID,
        OWNER = PI_OWNER,
        CONTRACT_ID = PI_CONTRACT_ID,
        MODIFIED_BY = PI_MODIFIED_BY,
        MODIFIED_DATE = SYSDATE
        WHERE CLAUSE_ID = PI_CLAUSE_ID;
        PO_RETURN_CODE := 0;
  EXCEPTION
        WHEN OTHERS THEN
            PO_RETURN_CODE := SQLCODE;

  END PROC_UPD_CLAUSE;

  PROCEDURE PROC_INS_CONTRACT
  (
    PI_CONTRACT_NAME IN CMS_CONTRACT_OTHERTYPES.CONTRACT_NAME%TYPE,
    PI_PARENT_ID IN CMS_CONTRACT_OTHERTYPES.PARENT_ID%TYPE,
    PI_GROUP_ID IN CMS_CONTRACT_OTHERTYPES.GROUP_ID%TYPE,
    PI_SHORT_NAME IN CMS_CONTRACT_OTHERTYPES.SHORT_NAME%TYPE,
    PI_CREATED_BY IN CMS_CONTRACT_OTHERTYPES.CREATED_BY%TYPE,
    PO_CONTRACT_ID OUT NUMBER
  ) AS
  L_CONTRACT_ID NUMBER;
  BEGIN
      INSERT INTO CMS_CONTRACT_OTHERTYPES(CONTRACT_NAME,PARENT_ID,CREATED_BY,CREATED_ON,GROUP_ID,SHORT_NAME)
      VALUES (PI_CONTRACT_NAME,PI_PARENT_ID,PI_CREATED_BY,SYSDATE,PI_GROUP_ID,PI_SHORT_NAME)
      RETURNING CONTRACT_ID INTO L_CONTRACT_ID;

      PO_CONTRACT_ID := L_CONTRACT_ID;
  END PROC_INS_CONTRACT;

  PROCEDURE PROC_UPD_CONTRACT
  (
    PI_CONTRACT_ID IN CMS_CONTRACT_OTHERTYPES.CONTRACT_ID%TYPE,
    PI_CONTRACT_NAME IN CMS_CONTRACT_OTHERTYPES.CONTRACT_NAME%TYPE,
    PI_PARENT_ID IN CMS_CONTRACT_OTHERTYPES.PARENT_ID%TYPE,
    PI_GROUP_ID IN CMS_CONTRACT_OTHERTYPES.GROUP_ID%TYPE,
    PI_SHORT_NAME IN CMS_CONTRACT_OTHERTYPES.SHORT_NAME%TYPE,
    PI_MODIFIED_BY IN CMS_CONTRACT_OTHERTYPES.MODIFIED_BY%TYPE,
    PO_RETURN_CODE OUT NUMBER
  ) AS
  BEGIN
    UPDATE CMS_CONTRACT_OTHERTYPES SET
        CONTRACT_NAME = PI_CONTRACT_NAME,
        PARENT_ID = PI_PARENT_ID,
        GROUP_ID = PI_GROUP_ID,
        SHORT_NAME = PI_SHORT_NAME,
        MODIFIED_BY = PI_MODIFIED_BY,
        MODIFIED_DATE = SYSDATE
        WHERE CONTRACT_ID = PI_CONTRACT_ID;
        PO_RETURN_CODE := 0;
 EXCEPTION
         WHEN OTHERS THEN
          PO_RETURN_CODE := SQLCODE;
  END PROC_UPD_CONTRACT;

  PROCEDURE PROC_INS_USER
  (
    PI_MANAGER_TYPE IN CMS_MANAGER.MANAGER_TYPE%TYPE,
    PI_SLAC_ID IN CMS_MANAGER.SLAC_ID%TYPE,
    PI_CREATED_BY IN CMS_MANAGER.CREATED_BY%TYPE,
    PI_ORG_ID IN CMS_MANAGER.ORG_ID%TYPE,
    PO_MANAGER_ID OUT NUMBER
  ) AS
  L_DIRECTORATE_CODE VARCHAR2(10);
  L_MAX_DRT_CODE VARCHAR2(25);
  L_SEQ_NO NUMBER;
  L_MANAGER_TYPE VARCHAR2(10);
  L_MANAGER_ID NUMBER;
  L_SUBSEQ_NO VARCHAR2(6);
  L_MAX_MGRID NUMBER;
  L_COUNT NUMBER;
  BEGIN

   IF ((PI_MANAGER_TYPE = 'DIRADMIN') OR (PI_MANAGER_TYPE = 'ALD')) THEN
    /* BEGIN
        IF (PI_ORG_ID <> NULL) THEN*/
          SELECT ORG_CODE INTO L_DIRECTORATE_CODE FROM SID.ORGANIZATIONS WHERE ORG_ID = PI_ORG_ID;
      /*  END IF;
      END;*/
    ELSE
     BEGIN
        IF (PI_MANAGER_TYPE = 'ADMIN') THEN
          L_MANAGER_TYPE := 'SUP';
        ELSIF (PI_MANAGER_TYPE ='SSOSUPER') THEN
          L_MANAGER_TYPE := 'SSOS';
        ELSE
          L_MANAGER_TYPE := PI_MANAGER_TYPE;
        END IF;

        SELECT COUNT(*) INTO L_COUNT FROM CMS_MANAGER WHERE MANAGER_TYPE = PI_MANAGER_TYPE;

        IF L_COUNT > 0 THEN
            SELECT MAX(MANAGER_ID) INTO L_MAX_MGRID FROM CMS_MANAGER WHERE MANAGER_TYPE =PI_MANAGER_TYPE;
            SELECT DIRECTORATE_CODE INTO L_MAX_DRT_CODE FROM CMS_MANAGER WHERE MANAGER_ID=L_MAX_MGRID;
            IF PI_MANAGER_TYPE = 'SSOSUPER' THEN
              L_SEQ_NO := SUBSTR(L_MAX_DRT_CODE,5,3) + 1;
            ELSE
              L_SEQ_NO := SUBSTR(L_MAX_DRT_CODE,4,3) + 1;
            END IF;
        ELSE
            L_SEQ_NO := 0;
        END IF;
         L_SUBSEQ_NO := TO_CHAR(L_SEQ_NO,'000');
         L_DIRECTORATE_CODE := L_MANAGER_TYPE || TRIM(L_SUBSEQ_NO) ;
     END;
    END IF;
    --DBMS_OUTPUT.PUT_LINE(L_DIRECTORATE_CODE);
    INSERT INTO CMS_MANAGER (
    DIRECTORATE_CODE,
    MANAGER_TYPE,
    SLAC_ID,
    CREATED_BY,
    CREATED_ON,
    ORG_ID) VALUES
    (
    L_DIRECTORATE_CODE,
    PI_MANAGER_TYPE,
    PI_SLAC_ID,
    PI_CREATED_BY,
    SYSDATE,
    PI_ORG_ID) RETURNING MANAGER_ID INTO L_MANAGER_ID;
    PO_MANAGER_ID := L_MANAGER_ID;


  END PROC_INS_USER;

  PROCEDURE PROC_DEL_USER
  (
    PI_MANAGER_ID IN CMS_MANAGER.MANAGER_ID%TYPE,
    PI_MODIFIED_BY IN CMS_MANAGER.MODIFIED_BY%TYPE,
    PO_RETURN_CODE OUT NUMBER
  ) AS
  BEGIN
    UPDATE CMS_MANAGER SET IS_ACTIVE='N',MODIFIED_BY=PI_MODIFIED_BY, MODIFIED_DATE=SYSDATE WHERE MANAGER_ID = PI_MANAGER_ID;
    PO_RETURN_CODE := 0;
  EXCEPTION
    WHEN OTHERS THEN
      PO_RETURN_CODE := SQLCODE;
  END;

  PROCEDURE PROC_DEL_CLAUSE
  (
    PI_CLAUSE_ID IN CMS_CLAUSE.CLAUSE_ID%TYPE,
    PI_MODIFIED_BY IN CMS_CLAUSE.MODIFIED_BY%TYPE,
    PO_RETURN_CODE OUT NUMBER
  ) AS
  L_COUNT NUMBER;
  BEGIN
      SELECT COUNT(*) INTO L_COUNT  FROM CMS_REQUIREMENT WHERE CLAUSE_ID = PI_CLAUSE_ID AND IS_ACTIVE='Y';

      IF L_COUNT = 0 THEN
        UPDATE CMS_CLAUSE SET IS_ACTIVE='N', MODIFIED_BY = PI_MODIFIED_BY, MODIFIED_DATE=SYSDATE WHERE CLAUSE_ID = PI_CLAUSE_ID;
        PO_RETURN_CODE := 0;
      ELSE
        PO_RETURN_CODE := -1;
      END IF;
  EXCEPTION
     WHEN OTHERS THEN
      PO_RETURN_CODE := SQLCODE;
  END;

  PROCEDURE PROC_DEL_REQUIREMENT
  (
    PI_REQUIREMENT_ID IN CMS_REQUIREMENT.REQUIREMENT_ID%TYPE,
    PI_MODIFIED_BY IN CMS_REQUIREMENT.MODIFIED_BY%TYPE,
    PO_RETURN_CODE OUT NUMBER
  ) AS
  L_COUNT NUMBER;
  BEGIN

  SELECT COUNT(*) INTO L_COUNT FROM CMS_DELIVERABLE WHERE REQUIREMENT_ID = PI_REQUIREMENT_ID AND IS_ACTIVE='Y';
  IF L_COUNT = 0 THEN
    UPDATE CMS_REQUIREMENT SET IS_ACTIVE='N', MODIFIED_BY = PI_MODIFIED_BY, MODIFIED_DATE= SYSDATE WHERE REQUIREMENT_ID = PI_REQUIREMENT_ID;
    PO_RETURN_CODE := 0;
    ELSE
      PO_RETURN_CODE := -1;
    END IF;
  EXCEPTION
    WHEN OTHERS THEN
      PO_RETURN_CODE := SQLCODE;
  End;

  Procedure Proc_Toggle_Email
  (
     pi_modified_by in Cms_Emailsettings.MODIFIED_BY%TYPE,
     Po_Return_Code Out Number
  ) As
 L_SEND VARCHAR2(2);
  Begin
   Select Send_Email Into L_Send From Cms_Emailsettings Where Is_Active='Y';

   If L_SEND = 'Y' Then
      UPDATE CMS_EMAILSETTINGS SET SEND_EMAIL = 'N', MODIFIED_BY=PI_MODIFIED_BY, MODIFIED_ON=SYSDATE WHERE IS_ACTIVE='Y';
   Else
      Update Cms_Emailsettings Set Send_Email='Y', MODIFIED_BY=PI_MODIFIED_BY, MODIFIED_ON=SYSDATE Where Is_Active='Y';
   End If;
   Po_Return_Code := 0;
   EXCEPTION
    When Others Then
      PO_RETURN_CODE := SQLCODE;
  END;

END CMS_ADMINISTRATION_PKG;

/
