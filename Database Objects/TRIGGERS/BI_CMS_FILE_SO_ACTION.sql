--------------------------------------------------------
--  DDL for Trigger BI_CMS_FILE_SO_ACTION
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BI_CMS_FILE_SO_ACTION" 
BEFORE INSERT ON CMS_FILE_SO_ACTION
FOR EACH ROW
BEGIN
  IF INSERTING AND :NEW."SO_ACTION_ID" IS NULL THEN
    select "CMS_FILESO_ACTIONID_SEQ".nextval into :NEW."SO_ACTION_ID" from dual;
  end if;
END;

/
ALTER TRIGGER "APPS_ADMIN"."BI_CMS_FILE_SO_ACTION" ENABLE;
