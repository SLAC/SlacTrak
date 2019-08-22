--------------------------------------------------------
--  DDL for Trigger BI_CMS_DELIVERABLE_AUDIT
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BI_CMS_DELIVERABLE_AUDIT" 
BEFORE INSERT ON CMS_DELIVERABLE_AUDIT
FOR EACH ROW
BEGIN
   IF INSERTING AND :NEW."DELIVERABLE_AUDIT_ID" IS NULL THEN
    select "CMS_DELI_AUDIT_ID_SEQ".nextval into :NEW."DELIVERABLE_AUDIT_ID" from dual;
  end if;
 if inserting and :new.created_by is null  then
        :new.created_by := NVL(V('APP_USER'),USER);
        :NEW.CREATED_ON := SYSDATE;
    end if;
END;

/
ALTER TRIGGER "APPS_ADMIN"."BI_CMS_DELIVERABLE_AUDIT" ENABLE;
