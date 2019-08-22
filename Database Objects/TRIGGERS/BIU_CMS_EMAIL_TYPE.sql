--------------------------------------------------------
--  DDL for Trigger BIU_CMS_EMAIL_TYPE
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BIU_CMS_EMAIL_TYPE" 
  before insert or update on "CMS_EMAIL_TYPE"
  for each row
begin
  if :NEW."EMAIL_TYPE_ID" is null then
    select "CMS_EMAIL_TYPE_ID_SEQ".nextval into :NEW."EMAIL_TYPE_ID" from dual;
  end if;
 if inserting and :new.created_by is null  then
        :new.created_by := NVL(V('APP_USER'),USER);
        :new.created_on := sysdate;
    end if;
      if inserting and :new.is_HTML is null  then
        :new.is_html := 'Y';
    END IF;
    IF UPDATING AND :NEW.MODIFIED_BY IS NULL THEN
        :NEW.MODIFIED_BY := NVL(V('APP_USER'),USER);
        :new.MODIFIED_DATE := sysdate;
    end if;
END;

/
ALTER TRIGGER "APPS_ADMIN"."BIU_CMS_EMAIL_TYPE" ENABLE;
