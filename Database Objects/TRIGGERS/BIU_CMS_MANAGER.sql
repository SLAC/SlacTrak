--------------------------------------------------------
--  DDL for Trigger BIU_CMS_MANAGER
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BIU_CMS_MANAGER" 
  before insert or update on "CMS_MANAGER"
  for each row
begin
  if :NEW."MANAGER_ID" is null then
    select "CMS_MANAGER_ID_SEQ".nextval into :NEW."MANAGER_ID" from dual;
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
ALTER TRIGGER "APPS_ADMIN"."BIU_CMS_MANAGER" ENABLE;
