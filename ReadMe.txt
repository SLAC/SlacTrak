U.S. Department of Energy under contract number DE-AC02-76SF00515
DOE O 241.1B, SCIENTIFIC AND TECHNICAL INFORMATION MANAGEMENT In the performance of Department of Energy (DOE) contracted obligations, each contractor is required to manage scientific and technical information (STI) produced under the contract as a direct and integral part of the work and ensure its broad availability to all customer segments by making STI available to DOE's central STI coordinating office, the Office of Scientific and Technical Information (OSTI).


Technical Details:

Database - Oracle 11.2
.NET Framework - 4.0 , ASP.NET web form, Jquery 1.4.1, Jquery ui 1.8 and log4net for logging

Should be compatable with latest versions 


External dependencies:
1. Need a Person table/View with People data ( Provided schema for creating a person table - Database Objects\INSTITUTIONAL_OBJECTS_SAMPLE)
	All references to Persons.person need to be replaced with respective table with corresponding field names.
	Views DW_PEOPLE need to be changed which is commonly used all over the application.

2. Need an Organization table with org details (Provided schema for creating an org table -Database Objects\INSTITUTIONAL_OBJECTS_SAMPLE)
	All references to SID.Organizations need to be replaced with respective org table 
	Dashboard  needs this info for displaying various counts by Directorates on the Home page

3. Need a But table with user details like logon name (Provided sample data - Database Objects\INSTITUTIONAL_OBJECTS_SAMPLE)
	This is referenced only one time in the code to get the user information based on log on

4. Need a QMAIL Pkg to send emails as in CMS_EMAIL_TYPE - Database Objects\TABLES.
	QMAIL_PKG.SEND_EMAIL in mainly Database Objects\PACKAGES\CMS_EMAIL_NOTIFICATION_PKG
	can be replaced with your custom email modules or can be directly replaced to send email or can be commented out if emails are not required,


All references to the above objects needs to be updated in the .NET code, Packages (Combination of many stored procedures and functions)
and Views.

SetUp:

In addition to creating all necessary database objects, the following has to be changed
	
1. Connection String in Web.config file under ContractManagement folder needs to be updated with the corresponding details 
that pertain to your organization

2. All references to OracleClient needs to be replaced with corresponding database client and the project needs to be recompiled

3. IIS needs to be configured on the web server that runs this application
	1. Copy all necessary files on to a folder on the webserver
	2. Create an application Pool
	3. Convert the folder to application and configure it to use Windows Authentication/Integrated. Anonymous needs to be turned off
	4. Configure the application to use the pool created in #2




