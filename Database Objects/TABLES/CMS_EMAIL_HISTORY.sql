--------------------------------------------------------
--  DDL for Table CMS_EMAIL_HISTORY
--------------------------------------------------------

  CREATE TABLE "APPS_ADMIN"."CMS_EMAIL_HISTORY" 
   (	"EMAIL_HISTORY_ID" NUMBER, 
	"EMAIL_TYPE_ID" NUMBER, 
	"SEQ_NOTE" VARCHAR2(100 BYTE), 
	"ORIGINAL_TABLE" VARCHAR2(50 BYTE), 
	"ORIGINAL_ID" NUMBER, 
	"CREATED_ON" DATE, 
	"QMAIL_ID" NUMBER, 
	"CREATED_BY" VARCHAR2(50 BYTE), 
	"MODIFIED_BY" VARCHAR2(50 BYTE), 
	"MODIFIED_DATE" DATE
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "APPS_ADMIN_DATA" ;
