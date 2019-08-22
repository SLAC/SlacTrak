--------------------------------------------------------
--  DDL for Trigger BI_CMS_EMAIL_HISTORY
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BI_CMS_EMAIL_HISTORY" 
  before insert on "CMS_EMAIL_HISTORY"
  for each row
begin
  if :NEW."EMAIL_HISTORY_ID" is null then
    select "CMS_EMAIL_HISTORY_ID_SEQ".nextval into :NEW."EMAIL_HISTORY_ID" from dual;
  END IF;
   if inserting and :new.created_by is null  then
        :new.created_by := NVL(V('APP_USER'),USER);
        :new.created_on := sysdate;
    END IF;

END;

/
ALTER TRIGGER "APPS_ADMIN"."BI_CMS_EMAIL_HISTORY" ENABLE;
