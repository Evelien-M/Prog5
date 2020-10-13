CREATE TABLE [dbo].[Gear]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(35) NOT NULL, 
    [Price] INT NOT NULL, 
    [Strength] INT NULL, 
    [Intelligence] INT NULL, 
    [Agility] INT NULL, 
    [Category] NCHAR(35) NOT NULL, 
    CONSTRAINT [FK_Gear_Category] FOREIGN KEY ([Category]) REFERENCES [Category]([Name])
)
