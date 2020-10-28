MERGE INTO dbo.Gear AS Target  
USING (values 
	(1, 'Muts', NULL, 30, NULL, 10, 5, 'Head'),
	(2, 'Helm', NULL, 60, 20, NULL, 5, 'Head'),
	(3, 'Mondkapje', NULL, 20, 10, 10, 10, 'Head'),
	(4, 'Cape', NULL, 100, NULL, NULL, 5, 'Shoulders'),
	(5, 'Sjaal', NULL, 30, NULL, 10, NULL, 'Shoulders'),
	(6, 'Ketting', NULL, 120, NULL, 15, 3, 'Shoulders'),
	(7, 'Jas', NULL, 140, 40, 4, 15, 'Chest'),
	(8, 'Hoodie', NULL, 60, 2, 4, 20, 'Chest'),
	(9, 'T-shirt', NULL, 30, NULL, NULL, 10, 'Chest'),
	(10, 'Gele riem', NULL, 15, 1, 1, 1, 'Belt'),
	(11, 'Rode riem', NULL, 20, 2, 2, 2, 'Belt'),
	(12, 'Groene riem', NULL, 15, 1, 1, 1, 'Belt'),
	(13, 'Jeans', NULL, 90, NULL, 10, NULL, 'Legs'),
	(14, 'Legging', NULL, 20, NULL, NULL, NULL, 'Legs'),
	(15, 'Broek', NULL, 40, 15, 12, NULL, 'Legs'),
	(16, 'Vans', NULL, 50, NULL, NULL, 15, 'Boots'),
	(17, 'Converse', NULL, 45, NULL, 13, NULL, 'Boots'),
	(18, 'Adidas', NULL, 60, 3, NULL, 10, 'Boots')

) AS Source (Id, Name, Image, Price, Strength, Intelligence, Agility, Category)  
ON Target.Id = Source.Id  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Id, Name, Image, Price, Strength, Intelligence, Agility, Category)  
	VALUES (Id, Name, Image, Price, Strength, Intelligence, Agility, Category)  
WHEN MATCHED THEN
	UPDATE SET
		Name = Source.Name,
		Image = Source.Image,
		Price = Source.Price,
		Strength = Source.Strength,
		Intelligence = Source.Intelligence,
		Agility = Source.Agility,
		Category = Source.Category;