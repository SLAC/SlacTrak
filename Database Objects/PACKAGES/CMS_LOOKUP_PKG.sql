--------------------------------------------------------
--  DDL for Package CMS_LOOKUP_PKG
--------------------------------------------------------

  CREATE OR REPLACE EDITIONABLE PACKAGE "APPS_ADMIN"."CMS_LOOKUP_PKG" AS

 TYPE DELIVERABLECUR IS REF CURSOR;
 PROCEDURE GetDeliverableLookupValues (
                            TYPECUR OUT DeliverableCur,
                            DIRECTORATECUR OUT DELIVERABLECUR,
                            NOTIFICATIONCUR OUT DELIVERABLECUR,
                            SSORECEIPIENTSCUR OUT DELIVERABLECUR
                             );

END CMS_LOOKUP_PKG;

/
