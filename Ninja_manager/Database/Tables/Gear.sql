CREATE TABLE [dbo].[Gear]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Image] TEXT NULL, 
    [Price] INT NOT NULL, 
    [Strength] INT NULL, 
    [Intelligence] INT NULL, 
    [Agility] INT NULL, 
    [Category] VARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Gear_Category] FOREIGN KEY ([Category]) REFERENCES [Category]([Name])
)
