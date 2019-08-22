--------------------------------------------------------
--  Ref Constraints for Table CMS_CONTRACT_OTHERTYPES
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_CONTRACT_OTHERTYPES" ADD CONSTRAINT "CMS_CONTRACT_PARENT_FK1" FOREIGN KEY ("PARENT_ID")
	  REFERENCES "APPS_ADMIN"."CMS_CONTRACT_OTHERTYPES" ("CONTRACT_ID") ENABLE;
  ALTER TABLE "APPS_ADMIN"."CMS_CONTRACT_OTHERTYPES" ADD CONSTRAINT "CMS_CONTRACT_OTHERTYPES_C_FK1" FOREIGN KEY ("GROUP_ID")
	  REFERENCES "APPS_ADMIN"."CMS_CONTRACT_FILTER" ("FILTER_ID") ENABLE;
