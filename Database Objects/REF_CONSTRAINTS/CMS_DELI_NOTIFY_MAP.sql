--------------------------------------------------------
--  Ref Constraints for Table CMS_DELI_NOTIFY_MAP
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_DELI_NOTIFY_MAP" ADD CONSTRAINT "CMS_DELI_NOTIFY_DELI_FK1" FOREIGN KEY ("DELIVERABLE_ID")
	  REFERENCES "APPS_ADMIN"."CMS_DELIVERABLE" ("DELIVERABLE_ID") ENABLE;
  ALTER TABLE "APPS_ADMIN"."CMS_DELI_NOTIFY_MAP" ADD CONSTRAINT "CMS_DELI_NOTIFY_LOOKUP_FK2" FOREIGN KEY ("LOOKUP_ID")
	  REFERENCES "APPS_ADMIN"."CMS_LOOKUP" ("LOOKUP_ID") ENABLE;
