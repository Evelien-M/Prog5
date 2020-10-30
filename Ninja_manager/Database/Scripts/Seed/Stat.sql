MERGE INTO dbo.Stat AS Target  
USING (values 
	('Strength', '#E07E7E'),
	('Intelligence', '#8991E0'),
	('Agility', '#7EE09A')
) AS Source (Name, [Colour])  
ON Target.Name = Source.Name  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Name, [Colour])  
	VALUES (Name, [Colour])  
WHEN MATCHED THEN
	UPDATE SET
		[Colour] = Source.[Colour];