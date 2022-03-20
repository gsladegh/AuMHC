Notes 

Of Interest
VS 2019 was the only version with a .net core command line template that had .net core 3.1 as an option, probably saving some time.
Never realized you could skip the first line of a file read using linq - https://stackoverflow.com/questions/43048819/skip-first-line-using-system-io-file-readalllinesfil-in-c-sharp

Needed to run in nuget: 
Install-Package System.Configuration.ConfigurationManager -Version 6.0.0 (https://www.nuget.org/packages/system.configuration.configurationmanager/)
Install-Package System.Data.SqlClient (https://www.nuget.org/packages/System.Data.SqlClient)

For Later: 
	1) Could add choices to:
		a) tell whether file has a header
		b) enter the filepath

	2) Check out Patterns (also ado vs dao)
	https://social.msdn.microsoft.com/Forums/vstudio/en-US/0246c0dd-cc50-47ab-a9b8-91ba3521f244/data-access-object-pattern-c?forum=csharpgeneral
	https://www.tutorialspoint.com/design_pattern/data_access_object_pattern.htm

	3) Elegantize it more

Issues: 
Had not originally configured sql server 2016 dev edition for this kind of task.  User originally couldn't connect.  Used info from following: 
https://stackoverflow.com/questions/12774827/cant-connect-to-localhost-on-sql-server-express-2012-2016
https://stackoverflow.com/questions/27267658/no-process-is-on-the-other-end-of-the-pipe-sql-server-2012