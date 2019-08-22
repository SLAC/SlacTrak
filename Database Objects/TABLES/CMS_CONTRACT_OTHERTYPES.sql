--------------------------------------------------------
--  DDL for Table CMS_CONTRACT_OTHERTYPES
--------------------------------------------------------

  CREATE TABLE "APPS_ADMIN"."CMS_CONTRACT_OTHERTYPES" 
   (	"CONTRACT_ID" NUMBER, 
	"PARENT_ID" NUMBER, 
	"CONTRACT_NAME" VARCHAR2(50 BYTE), 
	"IS_ACTIVE" CHAR(1 BYTE) DEFAULT 'Y', 
	"CREATED_BY" VARCHAR2(50 BYTE), 
	"CREATED_ON" DATE, 
	"MODIFIED_BY" VARCHAR2(50 BYTE), 
	"MODIFIED_DATE" DATE, 
	"GROUP_ID" NUMBER, 
	"SHORT_NAME" VARCHAR2(6 BYTE)
   ) SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 
 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1
  BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  TABLESPACE "APPS_ADMIN_DATA" ;
