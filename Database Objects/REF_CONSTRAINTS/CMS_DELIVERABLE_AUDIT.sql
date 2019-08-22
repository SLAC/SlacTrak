--------------------------------------------------------
--  Ref Constraints for Table CMS_DELIVERABLE_AUDIT
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_DELIVERABLE_AUDIT" ADD CONSTRAINT "CMS_DELIVERABLE_AUDIT_FK1" FOREIGN KEY ("DELIVERABLE_ID")
	  REFERENCES "APPS_ADMIN"."CMS_DELIVERABLE" ("DELIVERABLE_ID") ENABLE;
