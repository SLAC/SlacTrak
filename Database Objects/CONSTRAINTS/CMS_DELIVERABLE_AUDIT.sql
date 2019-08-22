--------------------------------------------------------
--  Constraints for Table CMS_DELIVERABLE_AUDIT
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_DELIVERABLE_AUDIT" MODIFY ("DELIVERABLE_ID" NOT NULL ENABLE);
  ALTER TABLE "APPS_ADMIN"."CMS_DELIVERABLE_AUDIT" MODIFY ("DELIVERABLE_AUDIT_ID" NOT NULL ENABLE);
  ALTER TABLE "APPS_ADMIN"."CMS_DELIVERABLE_AUDIT" ADD CONSTRAINT "CMS_DELIVERABLE_AUDIT_CHK1" CHECK (UPLOAD_FILE_REQUIRED IN ('Y', 'N')) ENABLE;
  ALTER TABLE "APPS_ADMIN"."CMS_DELIVERABLE_AUDIT" ADD CONSTRAINT "CMS_DELIVERABLE_AUDIT_PK" PRIMARY KEY ("DELIVERABLE_AUDIT_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "APPS_ADMIN_DATA"  ENABLE;
