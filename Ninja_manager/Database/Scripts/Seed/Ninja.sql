/* Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
MERGE INTO dbo.Ninja AS Target  
USING (values 
	(1, 'Henk', 500)
) AS Source (Id, Name, Gold)  
ON Target.Id = Source.Id  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Id, Name, Gold)  
	VALUES (Id, Name, Gold)  
WHEN MATCHED THEN
	UPDATE SET
		Name = Source.Name,
		Gold = Source.Gold;
