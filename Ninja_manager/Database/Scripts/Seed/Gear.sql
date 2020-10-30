MERGE INTO dbo.Gear AS Target  
USING (values 
	(1, 'Muts', NULL, 30, 'Head'),
	(2, 'Helm', NULL, 60, 'Head'),
	(3, 'Mondkapje', NULL, 20, 'Head'),
	(4, 'Cape', NULL, 100, 'Shoulders'),
	(5, 'Sjaal', NULL, 30, 'Shoulders'),
	(6, 'Ketting', NULL, 120, 'Shoulders'),
	(7, 'Jas', NULL, 140, 'Chest'),
	(8, 'Hoodie', NULL, 60, 'Chest'),
	(9, 'T-shirt', NULL, 30, 'Chest'),
	(10, 'Gele riem', NULL, 15, 'Belt'),
	(11, 'Rode riem', NULL, 20, 'Belt'),
	(12, 'Groene riem', NULL, 15, 'Belt'),
	(13, 'Jeans', NULL, 90, 'Legs'),
	(14, 'Legging', NULL, 20, 'Legs'),
	(15, 'Broek', NULL, 40, 'Legs'),
	(16, 'Vans', NULL, 50, 'Boots'),
	(17, 'Converse', NULL, 45, 'Boots'),
	(18, 'Adidas', NULL, 60, 'Boots')

) AS Source (Id, Name, Image, Price, Category)  
ON Target.Id = Source.Id  
WHEN NOT MATCHED BY TARGET THEN  
	INSERT (Id, Name, Image, Price, Category)  
	VALUES (Id, Name, Image, Price, Category)  
WHEN MATCHED THEN
	UPDATE SET
		Name = Source.Name,
		Image = Source.Image,
		Price = Source.Price,
		Category = Source.Category;