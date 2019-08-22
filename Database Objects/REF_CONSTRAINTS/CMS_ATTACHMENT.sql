--------------------------------------------------------
--  Ref Constraints for Table CMS_ATTACHMENT
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_ATTACHMENT" ADD CONSTRAINT "CMS_ATTACHMENT_FK1" FOREIGN KEY ("DELIVERABLE_ID")
	  REFERENCES "APPS_ADMIN"."CMS_DELIVERABLE" ("DELIVERABLE_ID") ENABLE;
