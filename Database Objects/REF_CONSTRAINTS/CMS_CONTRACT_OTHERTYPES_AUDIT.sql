--------------------------------------------------------
--  Ref Constraints for Table CMS_CONTRACT_OTHERTYPES_AUDIT
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_CONTRACT_OTHERTYPES_AUDIT" ADD CONSTRAINT "CMS_CONTRACT_AUDIT_FK1" FOREIGN KEY ("CONTRACT_ID")
	  REFERENCES "APPS_ADMIN"."CMS_CONTRACT_OTHERTYPES" ("CONTRACT_ID") ENABLE;
