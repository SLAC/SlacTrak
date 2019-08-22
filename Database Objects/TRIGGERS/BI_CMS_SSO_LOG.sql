--------------------------------------------------------
--  DDL for Trigger BI_CMS_SSO_LOG
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BI_CMS_SSO_LOG" 
BEFORE INSERT ON CMS_SSO_LOG
FOR EACH ROW
BEGIN
 IF INSERTING AND :NEW."SSO_LOG_ID" IS NULL THEN
    select "CMS_SSO_LOG_ID_SEQ".nextval into :NEW."SSO_LOG_ID" from dual;
  end if;
 if inserting and :new.created_by is null  then
        :new.created_by := NVL(V('APP_USER'),USER);
        :new.created_on := sysdate;
    end if;
END;

/
ALTER TRIGGER "APPS_ADMIN"."BI_CMS_SSO_LOG" ENABLE;
