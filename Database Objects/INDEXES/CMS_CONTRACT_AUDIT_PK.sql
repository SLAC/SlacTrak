--------------------------------------------------------
--  DDL for Index CMS_CONTRACT_AUDIT_PK
--------------------------------------------------------

  CREATE UNIQUE INDEX "APPS_ADMIN"."CMS_CONTRACT_AUDIT_PK" ON "APPS_ADMIN"."CMS_CONTRACT_OTHERTYPES_AUDIT" ("CONTRACT_AUDIT_ID") 
  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
  TABLESPACE "APPS_ADMIN_DATA" ;
