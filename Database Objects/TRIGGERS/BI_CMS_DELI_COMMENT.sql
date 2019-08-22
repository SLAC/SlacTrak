--------------------------------------------------------
--  DDL for Trigger BI_CMS_DELI_COMMENT
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE TRIGGER "APPS_ADMIN"."BI_CMS_DELI_COMMENT" 
  before insert on "CMS_DELI_COMMENT"
  for each row
begin
  if :NEW."COMMENT_ID" is null then
    select "CMS_DELI_COMMENT_ID_SEQ".nextval into :NEW."COMMENT_ID" from dual;
  end if;
 if inserting and :new.created_by is null  then
        :new.created_by := NVL(V('APP_USER'),USER);
        :new.created_on := sysdate;
    end if;

END;

/
ALTER TRIGGER "APPS_ADMIN"."BI_CMS_DELI_COMMENT" ENABLE;
