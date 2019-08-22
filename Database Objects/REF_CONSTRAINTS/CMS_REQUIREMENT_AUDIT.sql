--------------------------------------------------------
--  Ref Constraints for Table CMS_REQUIREMENT_AUDIT
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_REQUIREMENT_AUDIT" ADD CONSTRAINT "CMS_REQUIREMENT_AUDIT_CMS_FK1" FOREIGN KEY ("REQUIREMENT_ID")
	  REFERENCES "APPS_ADMIN"."CMS_REQUIREMENT" ("REQUIREMENT_ID") ENABLE;
