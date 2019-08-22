--------------------------------------------------------
--  DDL for Trigger BI_CMS_BULK_DATA
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BI_CMS_BULK_DATA" 
BEFORE INSERT ON CMS_BULK_DATA
FOR EACH ROW
BEGIN
  IF :NEW."BULK_ID" IS NULL THEN
    SELECT "CMS_BULK_ID_SEQ".NEXTVAL INTO :NEW."BULK_ID" FROM DUAL;
  END IF;

END;

/
ALTER TRIGGER "APPS_ADMIN"."BI_CMS_BULK_DATA" ENABLE;