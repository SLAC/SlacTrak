--------------------------------------------------------
--  Ref Constraints for Table CMS_SOWN_DELI_MAP
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_SOWN_DELI_MAP" ADD CONSTRAINT "CMS_SOWN_DELI_MAP_FK1" FOREIGN KEY ("DELIVERABLE_ID")
	  REFERENCES "APPS_ADMIN"."CMS_DELIVERABLE" ("DELIVERABLE_ID") ENABLE;
