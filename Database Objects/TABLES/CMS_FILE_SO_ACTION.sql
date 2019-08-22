--------------------------------------------------------
--  DDL for Table CMS_FILE_SO_ACTION
--------------------------------------------------------

  CREATE TABLE "APPS_ADMIN"."CMS_FILE_SO_ACTION" 
   (	"SO_ACTION_ID" NUMBER, 
	"DELIVERABLE_ID" NUMBER, 
	"DATE_ACTION" DATE, 
	"ACTION_BY" VARCHAR2(50 BYTE), 
	"EMAIL_SENT" CHAR(1 BYTE) DEFAULT 'N', 
	"DONE_UPLOAD" CHAR(1 BYTE) DEFAULT 'N'
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "APPS_ADMIN_DATA" ;