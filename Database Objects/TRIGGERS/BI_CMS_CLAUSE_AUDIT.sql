--------------------------------------------------------
--  DDL for Trigger BI_CMS_CLAUSE_AUDIT
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BI_CMS_CLAUSE_AUDIT" 
BEFORE INSERT ON CMS_CLAUSE_AUDIT
FOR EACH ROW
BEGIN
  IF INSERTING AND :NEW."CLAUSE_AUDIT_ID" IS NULL THEN
    select "CMS_CLAUSE_AUDIT_ID_SEQ".nextval into :NEW."CLAUSE_AUDIT_ID" from dual;
  end if;
 if inserting and :new.created_by is null  then
        :new.created_by := NVL(V('APP_USER'),USER);
        :new.created_on := sysdate;
    end if;
END;

/
ALTER TRIGGER "APPS_ADMIN"."BI_CMS_CLAUSE_AUDIT" ENABLE;
