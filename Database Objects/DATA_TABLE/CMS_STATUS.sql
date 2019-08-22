--------------------------------------------------------
--  File created - Tuesday-August-20-2019   
--------------------------------------------------------
REM INSERTING into CMS_STATUS
SET DEFINE OFF;
Insert into CMS_STATUS (STATUS_ID,STATUS_DESC,IS_ACTIVE) values (1,'New','Y');
Insert into CMS_STATUS (STATUS_ID,STATUS_DESC,IS_ACTIVE) values (2,'In Progress','Y');
Insert into CMS_STATUS (STATUS_ID,STATUS_DESC,IS_ACTIVE) values (3,'Submitted','Y');
Insert into CMS_STATUS (STATUS_ID,STATUS_DESC,IS_ACTIVE) values (4,'Approved','Y');
Insert into CMS_STATUS (STATUS_ID,STATUS_DESC,IS_ACTIVE) values (5,'Re-opened','Y');
Insert into CMS_STATUS (STATUS_ID,STATUS_DESC,IS_ACTIVE) values (6,'Approved by Default','Y');
COMMIT;
