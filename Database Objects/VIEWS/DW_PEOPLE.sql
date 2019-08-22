--------------------------------------------------------
--  DDL for View DW_PEOPLE
--------------------------------------------------------

  CREATE OR REPLACE FORCE EDITIONABLE VIEW "APPS_ADMIN"."DW_PEOPLE" ("EMPLOYEE_ID", "EMPLOYEE_NAME", "BUILDING", "ROOM", "WORK_PHONE", "MAIL_STOP", "EPO", "ORG_LEVEL_0_CODE", "ORG_LEVEL_1_CODE", "SUPERVISOR_ID", "EMPLOYEE_STATUS", "LAST_NAME", "FIRST_NAME", "DATA_AS_OF_DATE", "ORG_LEVEL_1_DESC", "ORG_CODE", "EMPLOYEE_CLASS", "HIRE_DATE", "GONET", "TERMINATION_DATE") AS 
  SELECT SUBSTR(TO_CHAR(KEY),1,7) employee_id,
    name employee_name,
    bldg building,
    room room,
    ext work_phone,
    bin mail_stop,
    maildisp epo,
    division org_level_0_code,
    dept_id org_level_1_code,
    SUBSTR(TO_CHAR(supervisor_id),1,7) supervisor_id,
    DECODE(status,'EMP', 'A',NULL) employee_status,
    lname last_name,
    fname first_name,
    updated data_as_of_date,
    dept_id org_level_1_desc,
    SUBSTR(' ',1,1) org_code,
    SUBSTR(' ',1,1) employee_class,
    to_date('01/01/1970','mm/dd/rrrr') hire_date,
    gonet,
    to_date(NULL) termination_date
  FROM persons.person
;
