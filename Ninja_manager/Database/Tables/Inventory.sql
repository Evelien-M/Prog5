CREATE TABLE [dbo].[Inventory]
(
	[Id_Ninja] INT NOT NULL, 
    [Category] NCHAR(35) NOT NULL, 
    [Id_Gear] INT NULL, 
    PRIMARY KEY ([Id_Ninja], [Category]), 
    CONSTRAINT [FK_Inventory_Ninja] FOREIGN KEY ([Id_Ninja]) REFERENCES [Ninja]([Id]), 
    CONSTRAINT [FK_Inventory_Category] FOREIGN KEY ([Category]) REFERENCES [Category]([Name]), 
    CONSTRAINT [FK_Inventory_Gear] FOREIGN KEY ([Id_Gear]) REFERENCES [Gear]([Id])
)
