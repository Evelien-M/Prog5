CREATE TABLE [dbo].[Inventory]
(
	[Id_Ninja] INT NOT NULL PRIMARY KEY, 
    [Id_Gear] INT NOT NULL PRIMARY KEY, 
    CONSTRAINT [FK_Inventory_Ninja] FOREIGN KEY ([Id_Ninja]) REFERENCES [Ninja]([Id]), 
    CONSTRAINT [FK_Inventory_Gear] FOREIGN KEY ([Id_Gear]) REFERENCES [Gear]([Id])
)
