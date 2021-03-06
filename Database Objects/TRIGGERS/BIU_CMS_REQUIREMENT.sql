--------------------------------------------------------
--  DDL for Trigger BIU_CMS_REQUIREMENT
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BIU_CMS_REQUIREMENT" 
BEFORE INSERT OR UPDATE ON CMS_REQUIREMENT
FOR EACH ROW
BEGIN
 IF INSERTING AND :NEW."REQUIREMENT_ID" IS NULL THEN
    select "CMS_REQUIREMENT_ID_SEQ".nextval into :NEW."REQUIREMENT_ID" from dual;
  end if;
 if inserting and :new.created_by is null  then
        :new.created_by := NVL(V('APP_USER'),USER);
        :new.created_on := sysdate;
    end if;

    IF UPDATING AND :NEW.MODIFIED_BY IS NULL THEN
        :NEW.MODIFIED_BY := NVL(V('APP_USER'),USER);
        :NEW.MODIFIED_DATE := SYSDATE;
    end if;

  IF UPDATING THEN
    CMS_ADMINISTRATION_PKG.L_MUT_REQUIREMENT_ID := :NEW.REQUIREMENT_ID;
    CMS_ADMINISTRATION_PKG.L_MUT_REQUIREMENT := :OLD.REQUIREMENT;
    CMS_ADMINISTRATION_PKG.L_MUT_NOTES := :OLD.NOTES;
    CMS_ADMINISTRATION_PKG.L_MUT_FREQUENCY_ID := :OLD.FREQUENCY_ID;
    CMS_ADMINISTRATION_PKG.L_MUT_UPLOAD_FILE_REQUIRED := :OLD.UPLOAD_FILE_REQUIRED;
    CMS_ADMINISTRATION_PKG.L_MUT_START_DATE := :OLD.START_DATE;
    CMS_ADMINISTRATION_PKG.L_MUT_REQ_CREATED_BY := :OLD.CREATED_BY;
    CMS_ADMINISTRATION_PKG.L_MUT_REQ_CREATED_ON := :OLD.CREATED_ON;
    CMS_ADMINISTRATION_PKG.L_MUT_IS_CM_NOTIFIED := :OLD.IS_CM_NOTIFIED;
    CMS_ADMINISTRATION_PKG.L_MUT_NOTIFIED_DATE := :OLD.NOTIFIED_DATE;
    CMS_ADMINISTRATION_PKG.L_MUT_REQ_CLAUSE_ID := :OLD.CLAUSE_ID;
    CMS_ADMINISTRATION_PKG.L_MUT_REQ_MODIFIED_BY := :OLD.MODIFIED_BY;
    CMS_ADMINISTRATION_PKG.L_MUT_REQ_MODIFIED_DATE := :OLD.MODIFIED_DATE;
    CMS_ADMINISTRATION_PKG.L_MUT_REQ_SCFLOWN_PROVISION := :OLD.SCFLOWN_PROVISION;
    IF (:NEW.MODIFIED_BY IS NULL) THEN
      :NEW.MODIFIED_DATE := SYSDATE;
    END IF;
    END IF;
END;

/
ALTER TRIGGER "APPS_ADMIN"."BIU_CMS_REQUIREMENT" ENABLE;
