--------------------------------------------------------
--  DDL for Table CMS_ATTACHMENT
--------------------------------------------------------

  CREATE TABLE "APPS_ADMIN"."CMS_ATTACHMENT" 
   (	"ATTACHMENT_ID" NUMBER, 
	"DELIVERABLE_ID" NUMBER, 
	"FILE_NAME" VARCHAR2(100 BYTE), 
	"FILE_SIZE" NUMBER, 
	"FILE_CONTENT_TYPE" VARCHAR2(200 BYTE), 
	"FILE_DATA" BLOB, 
	"UPLOADED_BY" VARCHAR2(50 BYTE), 
	"UPLOADED_ON" DATE, 
	"IS_ACTIVE" CHAR(1 BYTE) DEFAULT 'Y', 
	"CHANGED_BY" VARCHAR2(50 BYTE), 
	"CHANGED_ON" DATE
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "APPS_ADMIN_DATA" 
 LOB ("FILE_DATA") STORE AS BASICFILE (
  TABLESPACE "APPS_ADMIN_DATA" ENABLE STORAGE IN ROW CHUNK 8192 RETENTION 
  NOCACHE LOGGING 
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)) ;
