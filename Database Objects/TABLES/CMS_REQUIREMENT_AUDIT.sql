--------------------------------------------------------
--  DDL for Table CMS_REQUIREMENT_AUDIT
--------------------------------------------------------

  CREATE TABLE "APPS_ADMIN"."CMS_REQUIREMENT_AUDIT" 
   (	"REQUIREMENT_ID" NUMBER, 
	"REQUIREMENT" VARCHAR2(4000 BYTE), 
	"NOTES" VARCHAR2(4000 BYTE), 
	"FREQUENCY_ID" NUMBER, 
	"UPLOAD_FILE_REQUIRED" CHAR(1 BYTE), 
	"START_DATE" DATE, 
	"CREATED_BY" VARCHAR2(50 BYTE), 
	"CREATED_ON" DATE, 
	"IS_CM_NOTIFIED" CHAR(1 BYTE), 
	"NOTIFIED_DATE" DATE, 
	"CLAUSE_ID" NUMBER, 
	"REQUIREMENT_AUDIT_ID" NUMBER, 
	"MODIFIED_BY" VARCHAR2(50 BYTE), 
	"MODIFIED_DATE" DATE, 
	"SCFLOWN_PROVISION" CHAR(1 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "APPS_ADMIN_DATA" ;
