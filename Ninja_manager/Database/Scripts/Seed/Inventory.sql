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
MERGE INTO dbo.Inventory AS Target  
USING (values 
	(1, 'Head', NULL),
	(1, 'Shoulders', NULL),
	(1, 'Chest', NULL),
	(1, 'Belt', NULL),
	(1, 'Legs', NULL),
	(1, 'Boots', NULL)
) AS Source (Id_Ninja, Category, Id_Gear)  
ON Target.Id_Ninja = Source.Id_Ninja AND Target.Category = Source.Category
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Id_Ninja, Category, Id_Gear)  
	VALUES (Id_Ninja, Category, Id_Gear)  
WHEN MATCHED THEN
	UPDATE SET
		Id_Gear = Source.Id_Gear;