--------------------------------------------------------
--  DDL for Trigger BI_CMS_CONTRACT_AUDIT
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BI_CMS_CONTRACT_AUDIT" 
BEFORE INSERT ON CMS_CONTRACT_OTHERTYPES_AUDIT
FOR EACH ROW
BEGIN
   IF INSERTING AND :NEW."CONTRACT_AUDIT_ID" IS NULL THEN
    select "CMS_CONTRACT_AUDIT_ID_SEQ".nextval into :NEW."CONTRACT_AUDIT_ID" from dual;
  end if;
 if inserting and :new.created_by is null  then
        :new.created_by := NVL(V('APP_USER'),USER);
        :new.created_on := sysdate;
    end if;

    IF UPDATING AND :NEW.MODIFIED_BY IS NULL THEN
        :NEW.MODIFIED_BY := NVL(V('APP_USER'),USER);
        :new.MODIFIED_DATE := sysdate;
    end if;
END;

/
ALTER TRIGGER "APPS_ADMIN"."BI_CMS_CONTRACT_AUDIT" ENABLE;
