--------------------------------------------------------
--  DDL for Trigger BIU_CMS_STATUS
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BIU_CMS_STATUS" 
BEFORE INSERT OR UPDATE ON CMS_STATUS
FOR EACH ROW
BEGIN
   IF :NEW."STATUS_ID" IS NULL THEN
    select "CMS_STATUS_ID_SEQ".nextval into :NEW."STATUS_ID" from dual;
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
ALTER TRIGGER "APPS_ADMIN"."BIU_CMS_STATUS" ENABLE;
