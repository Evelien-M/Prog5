MERGE INTO dbo.GearStat AS Target  
USING (values 
	(1, 'Intelligence', 10),
	(1, 'Agility', 5),
	(2, 'Strength', 20),
	(2, 'Agility', 5),
	(3, 'Strength', 10),
	(3, 'Intelligence', 10),
	(3, 'Agility', 10),
	(4, 'Agility', 5),
	(5, 'Intelligence', 10),
	(6, 'Intelligence', 15),
	(6, 'Agility', 3),
	(7, 'Strength', 40),
	(7, 'Intelligence', 4),
	(7, 'Agility', 15),
	(8, 'Strength', 2),
	(8, 'Intelligence', 4),
	(8, 'Agility', 20),
	(9, 'Agility', 10),
	(10, 'Strength', 1),
	(10, 'Intelligence', 1),
	(10, 'Agility', 1),
	(11, 'Strength', 2),
	(11, 'Intelligence', 2),
	(11, 'Agility', 2),
	(12, 'Strength', 1),
	(12, 'Intelligence', 1),
	(12, 'Agility', 1),
	(13, 'Intelligence', 10),
	(15, 'Strength', 15),
	(15, 'Intelligence', 12),
	(16, 'Agility', 15),
	(17, 'Intelligence', 13),
	(18, 'Strength', 3),
	(18, 'Agility', 10)

) AS Source (Id_Gear, Stat_Name, Amount)  
ON Target.Id_Gear = Source.Id_Gear AND Target.Stat_Name = Source.Stat_Name  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Id_Gear, Stat_Name, Amount)  
	VALUES (Id_Gear, Stat_Name, Amount)  
WHEN MATCHED THEN
	UPDATE SET
		Amount = Source.Amount;