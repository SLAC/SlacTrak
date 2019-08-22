--------------------------------------------------------
--  DDL for Trigger BIU_CMS_LOOKUPS
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BIU_CMS_LOOKUPS" 
BEFORE INSERT OR UPDATE ON CMS_LOOKUP
FOR EACH ROW
BEGIN
  IF INSERTING AND :NEW."LOOKUP_ID" IS NULL THEN
    select "CMS_LOOKUPS_ID_SEQ".nextval into :NEW."LOOKUP_ID" from dual;
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
ALTER TRIGGER "APPS_ADMIN"."BIU_CMS_LOOKUPS" ENABLE;
