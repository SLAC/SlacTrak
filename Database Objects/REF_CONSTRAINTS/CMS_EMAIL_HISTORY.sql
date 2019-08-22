--------------------------------------------------------
--  Ref Constraints for Table CMS_EMAIL_HISTORY
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_EMAIL_HISTORY" ADD CONSTRAINT "CMS_EMAIL_TYPE_ID_FK" FOREIGN KEY ("EMAIL_TYPE_ID")
	  REFERENCES "APPS_ADMIN"."CMS_EMAIL_TYPE" ("EMAIL_TYPE_ID") ENABLE;
