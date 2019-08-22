--------------------------------------------------------
--  DDL for View VW_CMS_SOWNDELI_MAP_DETAILS
--------------------------------------------------------

  CREATE OR REPLACE FORCE EDITIONABLE VIEW "APPS_ADMIN"."VW_CMS_SOWNDELI_MAP_DETAILS" ("SUBOWNER_ID", "DELIVERABLE_ID", "SLAC_ID", "IS_ACTIVE", "CREATED_BY", "CREATED_ON", "ISOWNER", "EMPLOYEE_NAME") AS 
  SELECT SOWN."SUBOWNER_ID",SOWN."DELIVERABLE_ID",SOWN."SLAC_ID",SOWN."IS_ACTIVE",SOWN."CREATED_BY",SOWN."CREATED_ON",SOWN."ISOWNER",PC.EMPLOYEE_NAME
    FROM CMS_SOWN_DELI_MAP SOWN, DW_PEOPLE PC WHERE
	SOWN.SLAC_ID  = PC.EMPLOYEE_ID (+) AND SOWN.IS_ACTIVE='Y'
;
