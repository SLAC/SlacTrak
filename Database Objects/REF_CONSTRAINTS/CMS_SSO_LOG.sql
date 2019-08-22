--------------------------------------------------------
--  Ref Constraints for Table CMS_SSO_LOG
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_SSO_LOG" ADD CONSTRAINT "CMS_SSO_LOGS_CMS_DELIVERA_FK1" FOREIGN KEY ("DELIVERABLE_ID")
	  REFERENCES "APPS_ADMIN"."CMS_DELIVERABLE" ("DELIVERABLE_ID") ENABLE;
