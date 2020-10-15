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
	('Head', 0),
	('Shoulders', 1),
	('Chest', 2),
	('Belt', 3),
	('Legs', 4),
	('Boots', 5)
) AS Source (Name, [Order])  
ON Target.Name = Source.Name  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Name, [Order])  
	VALUES (Name, [Order])  
WHEN MATCHED THEN
	UPDATE SET
		[Order] = Source.[Order];
