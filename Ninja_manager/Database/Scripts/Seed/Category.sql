/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
MERGE INTO dbo.Category AS Target  
USING (values 
	('Head'),
	('Shoulders'),
	('Chest'),
	('Belt'),
	('Legs'),
	('Boots')
) AS Source (Name)  
ON Target.Name = Source.Name  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Name)  
	VALUES (Name)  
WHEN MATCHED THEN
	UPDATE SET
		Name = Source.Name;
