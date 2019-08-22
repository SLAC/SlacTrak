--------------------------------------------------------
--  Ref Constraints for Table CMS_FREQ_NOTIFYSCHED_MAP
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_FREQ_NOTIFYSCHED_MAP" ADD CONSTRAINT "CMS_FREQ_NOTIFYSCHED_MAP__FK1" FOREIGN KEY ("FREQUENCY_ID")
	  REFERENCES "APPS_ADMIN"."CMS_LOOKUP" ("LOOKUP_ID") ENABLE;
  ALTER TABLE "APPS_ADMIN"."CMS_FREQ_NOTIFYSCHED_MAP" ADD CONSTRAINT "CMS_FREQ_NOTIFYSCHED_MAP__FK2" FOREIGN KEY ("NOTIFICATION_SCHED_ID")
	  REFERENCES "APPS_ADMIN"."CMS_LOOKUP" ("LOOKUP_ID") ENABLE;
