--------------------------------------------------------
--  Ref Constraints for Table CMS_FILE_SO_ACTION
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_FILE_SO_ACTION" ADD CONSTRAINT "DELIVERABLE_ID_FK" FOREIGN KEY ("DELIVERABLE_ID")
	  REFERENCES "APPS_ADMIN"."CMS_DELIVERABLE" ("DELIVERABLE_ID") ENABLE;
