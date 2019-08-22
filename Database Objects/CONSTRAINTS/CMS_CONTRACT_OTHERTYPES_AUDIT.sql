--------------------------------------------------------
--  Constraints for Table CMS_CONTRACT_OTHERTYPES_AUDIT
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_CONTRACT_OTHERTYPES_AUDIT" MODIFY ("CONTRACT_ID" NOT NULL ENABLE);
  ALTER TABLE "APPS_ADMIN"."CMS_CONTRACT_OTHERTYPES_AUDIT" MODIFY ("CONTRACT_AUDIT_ID" NOT NULL ENABLE);
  ALTER TABLE "APPS_ADMIN"."CMS_CONTRACT_OTHERTYPES_AUDIT" ADD CONSTRAINT "CMS_CONTRACT_AUDIT_PK" PRIMARY KEY ("CONTRACT_AUDIT_ID")
  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  TABLESPACE "APPS_ADMIN_DATA"  ENABLE;