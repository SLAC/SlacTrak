--------------------------------------------------------
--  Ref Constraints for Table CMS_CLAUSE_AUDIT
--------------------------------------------------------

  ALTER TABLE "APPS_ADMIN"."CMS_CLAUSE_AUDIT" ADD CONSTRAINT "CMS_CLAUSE_AUDIT_CLAUS_FK1" FOREIGN KEY ("CLAUSE_ID")
	  REFERENCES "APPS_ADMIN"."CMS_CLAUSE" ("CLAUSE_ID") ENABLE;
