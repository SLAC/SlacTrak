--------------------------------------------------------
--  DDL for Trigger BI_CMS_DELI_NOTIFY_MAP
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BI_CMS_DELI_NOTIFY_MAP" 
BEFORE INSERT ON CMS_DELI_NOTIFY_MAP
FOR EACH ROW
BEGIN
  IF INSERTING AND :NEW."SCHEDULE_ID" IS NULL THEN
    select "CMS_SCHEDULE_ID_SEQ".nextval into :NEW."SCHEDULE_ID" from dual;
  end if;
 if inserting and :new.created_by is null  then
        :new.created_by := NVL(V('APP_USER'),USER);
        :new.created_on := sysdate;
    end if;
END;

/
ALTER TRIGGER "APPS_ADMIN"."BI_CMS_DELI_NOTIFY_MAP" ENABLE;
